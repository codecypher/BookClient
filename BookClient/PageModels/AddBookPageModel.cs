using System.Threading.Tasks;
using System.Windows.Input;
using BookClient.Models;
using BookClient.Services;
using FluentValidation;
using Xamarin.Forms;

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
    //
    // AddContactCommand(Save Contact Button click)
    // ViewAllContactsCommand(View All Contacts Label tap).
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class AddBookPageModel : BaseBookPageModel
    {
        public ICommand AddCommand { get; private set; }

        public AddBookPageModel(IBookManager bookManager, IValidator bookValidator) : base(bookManager, bookValidator)
        {
            AddCommand = new Command(async () => await OnAddBook());
        }

        public override void Init(object initData)
        {
            _book = (Book)initData;
            if (_book == null)  _book = new Book();
        }

        // Add a book
        async Task OnAddBook()
        {
            var context = new ValidationContext<Book>(_book);
            var validationResult = _validator.Validate(context);

            if (validationResult.IsValid)
            {
                //bool isUserAccept = await CoreMethods.DisplayAlert("Add Book", "Do you want to save Book details?", "OK", "Cancel");
                //if (isUserAccept)
                //{
                //}
                // This should return the new book but it doesn't
                Book book = await _repository.AddAsync(_book);
                //_books.Add(book);
                _books.Add(_book);
                await CoreMethods.PopPageModel();
            }
            else
            {
                await CoreMethods.DisplayAlert("Add Book", validationResult.Errors[0].ErrorMessage, "Ok");
            }
        }

        // Add a book
        public Command SaveCommand
        {
            get
            {
                return new Command(async() =>
                {
                    var context = new ValidationContext<Book>(_book);
                    var validationResult = _validator.Validate(context);

                    if (validationResult.IsValid)
                    {
                        //bool isUserAccept = await CoreMethods.DisplayAlert("Add Book", "Do you want to save Book details?", "OK", "Cancel");
                        //if (isUserAccept)
                        //{
                        //}
                        _books.Add(_book);
                        await _repository.AddAsync(_book);
                        await CoreMethods.PopPageModel();
                    }
                    else
                    {
                        await CoreMethods.DisplayAlert("Add Book", validationResult.Errors[0].ErrorMessage, "Ok");
                    }
                });
            }
        }
    }
}
