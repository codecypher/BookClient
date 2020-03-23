using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookClient.Models;

namespace BookClient.Services
{
    public interface IBookManager
    {
        Task<IList<Book>> GetAllAsync();
        IList<Book> GetBooks();
        Task<Book> GetBookAsync(string isbn);
        Task<Book> AddAsync(string title, string author, string genre);
        Task<Book> AddAsync(Book book);
        Task<bool> UpdateAsync(Book book);
        Task<bool> DeleteAsync(string isbn);
    }
}
