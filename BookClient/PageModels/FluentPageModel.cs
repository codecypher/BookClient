using BookClient.Models;
using BookClient.Validators;
using FluentValidation;
using Xamarin.Forms;

namespace BookClient.PageModels
{
    // Fluent Validation With MVVM In Xamarin Forms Application
    // https://www.c-sharpcorner.com/article/fluent-validation-with-mvvm-in-xamarin-forms-application/

    // Fluent Validation: How to validate Registration Page fields in Xamarin Forms
    // https://bsubramanyamraju.blogspot.com/2018/03/fluent-validation-how-to-validate.html

    // Realtime Validation in Xamarin Forms with FluentValidation
    // https://chaseflorell.github.io/xamarin/2017/10/04/realtime-validation-in-xamarin-forms-with-fluentvalidation/
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class FluentPageModel : BasePageModel
    {
        public User User { get; set; }

        private string _username;
        private string _password;
        private string _confirmPassword;
        private string _email;
        private string _color = "Green";
        private Command _signupCommand;

        //private readonly IValidator _validator;

        public FluentPageModel(FluentPageValidator validator) : base(validator)
        {
            //_validator = validator;
            //_signupCommand = new Command(p => { ExecuteSignUpCommand(); }, p => !HasErrors);
        }

        //public FluentPageModel(FluentPageValidator validator) : this()
        //{
        //    _validator = validator;
        //}

        public FluentPageModel()
        {
            //_validator = new UserValidator();
        }

        public string UserName
        {
            get => _username;
            //set => _username = value;
            set => SetProperty(ref _username, value, propertyChanged: () => ValidateProperty());
        }

        public string Password
        {
            get => _password;
            //set => _password = value;
            set => SetProperty(ref _password, value, propertyChanged: () => ValidateProperty());
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            //set => _confirmPassword = value;
            set => SetProperty(ref _confirmPassword, value, propertyChanged: () => ValidateProperty());
        }

        public string Email
        {
            get => _email;
            //set => _email = value;
            set => SetProperty(ref _email, value, propertyChanged: () => ValidateProperty());
        }

        public string Color
        {
            get => _color;
            set => _color = value;
        }

        public Command SignUpCommand
        {
            get
            {
                return _signupCommand ?? (_signupCommand = new Command(p => { ExecuteSignUpCommand(); }, p => !HasErrors));
                //return _signupCommand ?? (_signupCommand = new Command(ExecuteSignUpCommand));
            }
            set { _signupCommand = value; }
        }

        private string _validateMessage;
        public string ValidateMessage
        {
            get
            {
                return _validateMessage;
            }
            set
            {
                _validateMessage = value;
                //this.Set(ref this._validateMessage, value, "ValidateMessage");
            }
        }

        protected void ExecuteSignUpCommand()
        {
            //this.User = new User
            //{
            //    UserName = _username,
            //    Password = _password,
            //    ConfirmPassword = _confirmPassword,
            //    Email = _email
            //};

            var context = new ValidationContext<FluentPageModel>(this);
            var validationResult = _validator.Validate(context);

            if (validationResult.IsValid)
            {
                this.ValidateMessage = "Validation Success!!";
                this.Color = "Green";
            }
            else
            {
                this.ValidateMessage = string.Empty;
                foreach (var error in validationResult.Errors)
                {
                    this.ValidateMessage += error.ErrorMessage + "\n";
                    this.Color = "Red";
                }
            }
            Application.Current.MainPage.DisplayAlert("FluentValidation: ", this.ValidateMessage, "Ok");
        }
    }
}
