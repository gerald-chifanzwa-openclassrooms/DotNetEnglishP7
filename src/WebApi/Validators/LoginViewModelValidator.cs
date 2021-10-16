using FluentValidation;
using WebApi.Models;

namespace WebApi.Validators
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(vm => vm.UserName).NotEmpty();
            RuleFor(vm => vm.Password).NotEmpty();
        }
    }
}
