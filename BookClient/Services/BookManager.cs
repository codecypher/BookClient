using BookClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookClient.Services
{
    public class BookManager : IBookManager
    {
        private string _authKey = "28236d8ec201df516d0f6472d516d72b";  // authorization key
        private string _siteUrl = "";

        public string GetSiteUrl()
        {
            string url = string.Empty;
#if DEBUG
            url = App.Settings.SiteUrl;
#else
            url = App.Settings.SiteUrl;
#endif
            return url;
        }

        // Ignore SSL errors on Android for local secure web services.
        // The resulting HttpClientHandler object should be passed as an argument to the HttpClient constructor.
        public HttpClientHandler GetInsecureHandler()
        {
            var handler = new HttpClientHandler();

            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };

            return handler;
        }

        private async Task<HttpClient> GetClientAsync()
        {
            var handler = GetInsecureHandler();

            _siteUrl = GetSiteUrl();

#if DEBUG
            HttpClient client = new HttpClient(handler);
#else
            HttpClient client = new HttpClient();
#endif
            if (string.IsNullOrEmpty(_authKey))
            {
                _authKey = await client.GetStringAsync(_siteUrl);
                // The key will have quotes around it that need to be removed.
                //_authKey = JsonConvert.DeserializeObject<string>(_authKey);
            }

            client.DefaultRequestHeaders.Add("Authorization", _authKey);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        private HttpClient GetClient()
        {
            var handler = GetInsecureHandler();

            _siteUrl = GetSiteUrl();

#if DEBUG
            HttpClient client = new HttpClient(handler);
#else
            HttpClient client = new HttpClient();
#endif
            // if (string.IsNullOrEmpty(_authKey))
            // {
            //     _authKey = client.GetStringAsync(_bookUrl+ "login").Result;
            //     // The key will have quotes around it that need to be removed.
            //     _authKey = JsonConvert.DeserializeObject<string>(_authKey);
            // }
            //

            client.DefaultRequestHeaders.Add("Authorization", _authKey);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        public async Task<IList<Book>> GetAllAsync()
        {
            // TODO: use GET to retrieve books
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(_siteUrl);
            return JsonConvert.DeserializeObject<List<Book>>(result);
        }

        public async Task<Book> GetBookAsync(string isbn)
        {
            // TODO: use GET to retrieve books
            HttpClient client = await GetClientAsync();
            string result = await client.GetStringAsync(_siteUrl + "/" + isbn);
            return JsonConvert.DeserializeObject<Book>(result);
        }

        public IList<Book> GetBooks()
        {
            // TODO: use GET to retrieve books
            HttpClient client = GetClient();
            string result = client.GetStringAsync(_siteUrl).Result;
            return JsonConvert.DeserializeObject<List<Book>>(result);
        }

        public async Task<Book> AddAsync(string title, string author, string genre)
        {
            // TODO: use POST to add a book
            Book book = new Book()
            {
                ISBN = string.Empty,
                Title = title,
                //Authors = new ObservableCollection<string>(new[] { author }),
                Genre = genre,
                PublishDate = DateTime.Now
            };

            HttpClient client = await GetClientAsync();
            var response = await client.PostAsync(_siteUrl, new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json"));

            book = JsonConvert.DeserializeObject<Book>(await response.Content.ReadAsStringAsync());
            return book;
        }

        public async Task<Book> AddAsync(Book book)
        {
            // TODO: use POST to add a book
            HttpClient client = await GetClientAsync();
            var response = await client.PostAsync(_siteUrl, new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json"));

            book = JsonConvert.DeserializeObject<Book>(await response.Content.ReadAsStringAsync());
            return book;
        }

        public async Task<bool> UpdateAsync(Book book)
        {
            // TODO: use PUT to update a book
            HttpClient client = await GetClientAsync();
            string json = JsonConvert.SerializeObject(book);
            await client.PutAsync(_siteUrl + "/" + book.ISBN, new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json"));
            return true;
        }

        public async Task<bool> DeleteAsync(string isbn)
        {
            // TODO: use DELETE to delete a book
            HttpClient client = await GetClientAsync();
            await client.DeleteAsync(_siteUrl + "/" + isbn);
            return true;
        }
    }
}

