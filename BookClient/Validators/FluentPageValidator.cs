using BookClient.PageModels;
using FluentValidation;

namespace BookClient.Validators
{
    public class FluentPageValidator : AbstractValidator<FluentPageModel>
    {
        public FluentPageValidator()
        {
            RuleFor(x => x.UserName).NotNull().Length(10, 20);
            RuleFor(x => x.Password).NotNull().WithMessage("Password is required.");
            RuleFor(x => x.ConfirmPassword).NotNull().Equal(x => x.Password).WithMessage("Passwords do not match.");
            RuleFor(x => x.Email).NotNull().EmailAddress().WithMessage("Invalid Email.");
        }
    }
}
