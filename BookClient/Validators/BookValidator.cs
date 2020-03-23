using System;
using BookClient.Models;
using FluentValidation;

namespace BookClient.Validators
{
    // Fluent Validation With MVVM In Xamarin Forms Application
    // https://www.c-sharpcorner.com/article/fluent-validation-with-mvvm-in-xamarin-forms-application/
    // Fluent Validation: How to validate Registration Page fields in Xamarin Forms
    // https://bsubramanyamraju.blogspot.com/2018/03/fluent-validation-how-to-validate.html
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(x => x.Title).Must(n => ValidateStringEmpty(n)).WithMessage("Title should not be empty");
            RuleFor(x => x.Genre).Must(n => ValidateStringEmpty(n)).WithMessage("Genre should not be empty");

            //RuleFor(c => c.Name).Must(n => ValidateStringEmpty(n)).WithMessage("Contact name should not be empty.");
            //RuleFor(c => c.MobileNumber).NotNull().Length(10);
            //RuleFor(c => c.Age).Must(a => ValidateStringEmpty(a)).WithMessage("Contact Age should not be empty.");
            //RuleFor(c => c.Gender).Must(g => ValidateStringEmpty(g)).WithMessage("Contact Gender should not be empty.");
            //RuleFor(c => c.DOB).Must(d => ValidateStringEmpty(d.ToString())).WithMessage("Contact DOB should not be empty.");
            //RuleFor(c => c.Address).Must(a => ValidateStringEmpty(a)).WithMessage("Contact Adress should not be empty.");

            //RuleFor(x => x.UserName).NotNull().Length(10, 20);
            //RuleFor(x => x.Password).NotNull().WithMessage("Password is required.");
            //RuleFor(x => x.ConfirmPassword).NotNull().Equal(x => x.Password).WithMessage("Passwords do not match.");
            //RuleFor(x => x.Email).NotNull().EmailAddress().WithMessage("Invalid Email.");
        }

        bool ValidateStringEmpty(string stringValue)
        {
            if (!string.IsNullOrEmpty(stringValue))
                return true;
            return false;
        }
    }
}
