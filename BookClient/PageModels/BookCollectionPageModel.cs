using BookClient.Models;
using FreshMvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookClient.PageModels
{
    // The Xamarin.Forms Command Interface
    // https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/data-binding/commanding
    // Simplifying Events with Commanding
    // https://blog.xamarin.com/simplifying-events-with-commanding/
    // Win Application Framework (WAF)
    // https://github.com/jbe2277/waf
    public class BookCollectionPageModel : ObservableObject
    {
        //BookPageModel _bookEdit;
        //ObservableCollection<BookPageModel> _books;
        bool _isEditing;

        public BookCollectionPageModel()
        {
            //_books = new ObservableCollection<BookPageModel>();

            //NewCommand = new Command(
            //    execute: () =>
            //    {
            //        BookEdit = new BookViewModel();
            //        //BookEdit.PropertyChanged += OnBookEditPropertyChanged;
            //        BookEdit.Book.PropertyChanged += OnBookEditPropertyChanged;
            //        IsEditing = true;
            //        RefreshCanExecutes();
            //    },
            //    canExecute: () =>
            //    {
            //        return !IsEditing;
            //    });

            //SubmitCommand = new Command(
            //    execute: () =>
            //    {
            //        _books.Add(BookEdit);
            //        //BookEdit.PropertyChanged -= OnBookEditPropertyChanged;
            //        _bookEdit.Book.PropertyChanged -= OnBookEditPropertyChanged;
            //        BookEdit = null;
            //        IsEditing = false;
            //        RefreshCanExecutes();
            //    },
            //    canExecute: () =>
            //    {
            //        return _bookEdit != null &&
            //               !_bookEdit.Book.Title.IsNullOrEmpty() &&
            //               !_bookEdit.Book.Genre.IsNullOrEmpty() &&
            //               !_bookEdit.Book.Author.IsNullOrEmpty();
            //    });

            //CancelCommand = new Command(
                //execute: () =>
                //{
                //    //BookEdit.PropertyChanged -= OnBookEditPropertyChanged;
                //    _bookEdit.Book.PropertyChanged -= OnBookEditPropertyChanged;
                //    BookEdit = null;
                //    IsEditing = false;
                //    RefreshCanExecutes();
                //},
                //canExecute: () =>
                //{
                //    return IsEditing;
                //});
        }

        public bool IsEditing
        {
            get { return _isEditing; }
            private set { SetProperty(ref _isEditing, value); }
        }

        //public IList<BookPageModel> Books { get { return _books; } }

        //public BookPageModel BookEdit
        //{
        //    get { return _bookEdit; }
        //    private set { SetProperty(ref _bookEdit, value); }
        //}

        //public int Count => _books.Count;

        public ICommand NewCommand { private set; get; }

        public ICommand SubmitCommand { private set; get; }

        public ICommand CancelCommand { private set; get; }

        void OnBookEditPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            (SubmitCommand as Command).ChangeCanExecute();
        }

        void RefreshCanExecutes()
        {
            (NewCommand as Command).ChangeCanExecute();
            (SubmitCommand as Command).ChangeCanExecute();
            (CancelCommand as Command).ChangeCanExecute();
        }
    }
}
