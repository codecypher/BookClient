using System;
using System.Collections.Generic;
using System.Linq;
using BookClient.Models;
using BookClient.Services;
using FluentValidation;
using FreshMvvm;

namespace BookClient.PageModels
{
    /// <summary>
    /// The view model implements properties and commands to which the view can bind to,
    /// and notifies the view of any state changes through change notification events.
    /// The properties and commands that the view model provides define the functionality
    /// to be offered by the UI, but the view determines how that functionality is to be displayed.
    /// Keep the UI responsive with asynchronous operations.
    /// Use asynchronous methods for I/O operations and raise events to asynchronously
    /// notify views of property changes.
    /// </summary>
    ///
    // Getting Started With Xamarin Forms FreshMVVM Framework
    // http://bsubramanyamraju.blogspot.com/2018/03/getting-started-with-xamarin-forms.html
    // FreshMvvm Quick Start Guide
    // http://michaelridland.com/xamarin/freshmvvm-quick-start-guide/
    //
    // The reason to say FreshMVVM is designed for Xamarin.Forms is because it plays on Xamarin.Forms
    // strengths and fills in only the missing parts. It has a requirement for Xamarin.Forms so it is
    // smart and can do things such as wiring up the BindingContext and Page events.
    //
    // IValidator: To validate contact object.
    // IContactRepository: To access ContactInfo Table operations.
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class BaseBookPageModel : FreshBasePageModel
    {
        protected Book _book;
        protected IBookManager _repository;
        protected IValidator _validator;

        public BaseBookPageModel(IBookManager bookManager, IValidator bookValidator)
        {
            //_book = new Book();
            _repository = bookManager;
            _validator = bookValidator;
        }

        protected static IList<Book> _books;
        public IList<Book> Books
        {
            get => _books;

            set
            {
                _books = value;
                RaisePropertyChanged(nameof(Books));
            }
        }

        public string ISBN
        {
            get => _book.ISBN;
            set
            {
                _book.ISBN = value;
                RaisePropertyChanged();
            }
        }

        public string Title
        {
            get => _book.Title;
            set
            {
                _book.Title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }

        public string Genre
        {
            get => _book.Genre;
            set
            {
                _book.Genre = value;
                RaisePropertyChanged();
            }
        }

        public DateTime PublishDate
        {
            get => _book.PublishDate;
            set
            {
                _book.PublishDate = value;
                RaisePropertyChanged();
            }
        }

        public IList<string> Authors
        {
            get => _book.Authors;
            set
            {
                _book.Authors = value;
                RaisePropertyChanged();
            }
        }

        public string Author
        {
            get => _book.Authors.FirstOrDefault();
            set
            {
                if (_book.Authors.Count == 0)
                    _book.Authors.Add(value);

                if (_book.Authors[0] != value)
                {
                    _book.Authors[0] = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
