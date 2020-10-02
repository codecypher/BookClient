using FreshMvvm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BookClient.Models
{
    // The model represents the domain model which includes
    // a data model along with business and validation logic.
    [DebuggerDisplay("Book: {Title} {Genre}")]
    public class Book : FreshBasePageModel
    {
        public Book()
        {
            //Id = new Guid.NewGuid();
            Authors = new ObservableCollection<string>();
        }

        public Book(string isbn, string title, string genre, DateTime publishDate)
        {
            //Id = new Guid.NewGuid();
            ISBN = isbn;
            Title = title;
            Genre = genre;
            PublishDate = publishDate;
            Authors = new ObservableCollection<string>();
        }

        //public Guid Id {get; private set; }

        private string _isbn;
        public string ISBN
        {
            get { return _isbn; }
            set
            {
                _isbn = value;
                RaisePropertyChanged();
            }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        private string _genre;
        public string Genre
        {
            get { return _genre; }
            set
            {
                _genre = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _publishDate;
        public DateTime PublishDate
        {
            get { return _publishDate; }
            set
            {
                _publishDate = value;
                RaisePropertyChanged();
            }
        }

        private IList<string> _authors;
        public IList<string> Authors
        {
            get { return _authors; }
            set
            {
                _authors = value;
                RaisePropertyChanged();
            }
        }


        //public override bool Equals(object obj)
        //{
        //    var book = obj as Book;

        //    if (book == null || this.GetType() != obj.GetType())
        //        return false;

        //    return book.ISBN == this.ISBN;
        //}

        //public override int GetHashCode()
        //{
        //    return _isbn.GetHashCode();
        //}
    }
}

