using BookClient.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookClient.Validators
{
    // Fluent Validation With MVVM In Xamarin Forms Application
    // https://www.c-sharpcorner.com/article/fluent-validation-with-mvvm-in-xamarin-forms-application/
    // Fluent Validation: How to validate Registration Page fields in Xamarin Forms
    // https://bsubramanyamraju.blogspot.com/2018/03/fluent-validation-how-to-validate.html
    //[FluentValidation.Attributes.Validator(typeof(UserValidator))]
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName).NotNull().Length(10, 20);
            RuleFor(x => x.Password).NotNull().WithMessage("Password is required.");
            RuleFor(x => x.ConfirmPassword).NotNull().Equal(x => x.Password).WithMessage("Passwords do not match.");
            RuleFor(x => x.Email).NotNull().EmailAddress().WithMessage("Invalid Email.");
        }
    }
}
