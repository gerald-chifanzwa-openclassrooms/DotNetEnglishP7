using FluentValidation;
using WebApi.Models;

namespace WebApi.Validators
{
    public class UserModelValidator : AbstractValidator<UserModel>
    {
        public UserModelValidator()
        {
            RuleFor(u => u.UserName).NotEmpty()
                                            .Matches("^[a-zA-Z]+[a-zA-Z0-9-]+$")
                                            .WithMessage("{PropertyName} must contain alphanumeric characters only")
                                            .MinimumLength(3);

            RuleFor(u => u.Password).NotEmpty()
                .MinimumLength(8)
                .Matches("[0-9]+").WithMessage("{PropertyName} must contain at least one digit")
                .Matches("[A-Z]+").WithMessage("{PropertyName} must contain at least one uppercase letter")
                .Matches("[^a-zA-Z0-9\\s]+").WithMessage("{PropertyName} must contain at least one special character");
            //at least one uppercase letter, minimum eight characters, at least one number, and one symbol

            RuleFor(u => u.FullName).NotEmpty();
            RuleFor(u => u.Role).NotEmpty();
        }

    }
}
