using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BookClient.Helpers;
using BookClient.Models;
using BookClient.Services;
using FluentValidation;
using FreshMvvm;
using Xamarin.Forms;

namespace BookClient.PageModels
{
    // Xamarin Forms with MVVM Light
    // https://mobileprogrammerblog.wordpress.com/2017/01/21/xamarin-forms-with-mvvm-light/
    // XAM320 - Design an MVVM ViewModel in Xamarin.Forms 
    // https://elearning.xamarin.com/forms/xam320/
    public class BookListPageModel : BaseBookPageModel
    {
        public BookListPageModel(IBookManager bookManager, IValidator bookValidator) : base(bookManager, bookValidator)
        {
            //_repository = bookManager;
            //_bookValidator = bookValidator;

            //Books = new ObservableCollection<BookPageModel>(LoadBooks()
            //.Select(b => new BookPageModel(b)));
            //Books = new TrulyObservableCollection<Book>(LoadBooks());

            //SelectedBook = Books.FirstOrDefault();
        }

        public override void Init(object initData)
        {
            Books = new ObservableCollection<Book>(_repository.GetBooks());
        }

        // Methods are automatically wired up to page
        protected override void ViewIsAppearing(object sender, System.EventArgs e)
        {
            //CoreMethods.DisplayAlert("Page is appearing", "", "Ok");
            base.ViewIsAppearing(sender, e);
        }

        protected override void ViewIsDisappearing(object sender, System.EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);
        }

        // This is called when a page is popped
        public override void ReverseInit(object value)
        {
            var newBook = (Book)value;
            if (!_books.Contains(newBook))
            {
                _books.Add(newBook);
            }
        }

        public async Task<bool> LoadBooksAsync()
        {
            Books = new ObservableCollection<Book>(await _repository.GetAllAsync());

            //SelectedBook = Books.FirstOrDefault();

            return true;
        }

        Book _selectedBook;
        public Book SelectedBook
        {
            get => _selectedBook;

            set
            {
                _selectedBook = value;
                if (value != null)
                {
                    BookSelected.Execute(value);
                    //ShowEditBook(value);
                    //RaisePropertyChanged(nameof(SelectedBook));
                }
            }
        }

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                RaisePropertyChanged(nameof(IsRefreshing));
            }
        }

        public Command<Book> BookSelected
        {
            get
            {
                return new Command<Book>(async (book) =>
                {
                   await CoreMethods.PushPageModel<EditBookPageModel>(book);
                });
            }
        }

        //async void ShowEditBook(Book book)
        //{
        //    await CoreMethods.PushPageModel<EditBookPageModel>(book);
        //}

        public ICommand AddCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<AddBookPageModel>();
                });
            }
        }

        public ICommand TapCommand
        {
            get
            {
                return new Command(async (book) =>
                {
                    _selectedBook = (Book)book;
                    await CoreMethods.PushPageModel<EditBookPageModel>(book);
                });
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new Command(async (selectedBook) =>
                {
                    if (selectedBook != null)
                    {
                        Book book = (Book)selectedBook;
                        //if (await this.DisplayAlert("Delete Book?",
                        //    "Are you sure you want to delete the book '"
                        //        + book.Title + "'?", "Yes", "Cancel") == true)
                        //{
                        //}
                        int pos = _books.IndexOf(book);
                        await _repository.DeleteAsync(book.ISBN);
                        _books.Remove(book);
                        if (_selectedBook == book)
                        {
                            if (pos > Books.Count - 1)
                                pos = Books.Count - 1;
                            _selectedBook = Books[pos];
                        }
                    }
                });
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;

                    await LoadBooksAsync();

                    IsRefreshing = false;
                });
            }
        }
    }
}
