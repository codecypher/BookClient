using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using FluentValidation;
using FluentValidation.Results;
using FreshMvvm;

namespace BookClient.PageModels
{
    // Realtime Validation in Xamarin Forms with FluentValidation
    // https://chaseflorell.github.io/xamarin/2017/10/04/realtime-validation-in-xamarin-forms-with-fluentvalidation/
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public abstract class BasePageModel : FreshBasePageModel, INotifyDataErrorInfo
    {
        protected readonly Dictionary<string, IList<string>> Errors = new Dictionary<string, IList<string>>();
        protected readonly IValidator _validator;

        protected BasePageModel(IValidator validator) : this()
        {
            _validator = validator;
        }

        protected BasePageModel()
        {
            // setup anything else
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            Errors.TryGetValue(propertyName, out var errorsForName);
            return errorsForName;
        }

        public bool IsValid => !HasErrors;

        public bool HasErrors => Errors.Any(kv => kv.Value != null && kv.Value.Count > 0);

        public virtual ValidationResult ValidateProperty([CallerMemberName] string propertyName = null)
        {
            if (_validator == null)
                throw new NullReferenceException("An instance of IValidator must be passed into the constructor of your ViewModel in order to call ValidateProperty");
            var result = _validator.Validate(this);
            HandleValidationResultForProperty(result, propertyName);
            return result;
        }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = null, Action propertyChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;
            backingStore = value;
            ValidateProperty(propertyName);
            //RaisePropertyChanged(propertyName);
            return true;
        }

        private void HandleValidationResultForProperty(ValidationResult result, string propertyName)
        {
            var parts = propertyName.Split('.');
            var validationPropertyName = parts.Length < 2 ? propertyName : string.Join(".", parts.Skip(1));
            var isPropertyValid = result.Errors.All(err => err.PropertyName != validationPropertyName);
            if (!isPropertyValid)
            {
                var errors = result.Errors.Where(e => e.PropertyName == validationPropertyName).Select(error => error.ErrorMessage).ToList();
                Errors[propertyName] = errors;
            }
            else
            {
                if (Errors.ContainsKey(propertyName))
                    Errors.Remove(propertyName);
            }

            RaiseErrorsChanged(propertyName);
        }

        private void RaiseErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            RaisePropertyChanged(nameof(HasErrors));
            RaisePropertyChanged(nameof(IsValid));
        }

        //private void RaisePropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
