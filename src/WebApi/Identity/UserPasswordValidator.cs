using System.Threading.Tasks;
using Dot.Net.WebApi.Repositories;
using Microsoft.AspNetCore.Identity;

namespace WebApi.Identity
{
    public class UserPasswordValidator : IPasswordValidator<ApplicationUser>
    {
        private readonly IUserRepository _userRepository;

        public UserPasswordValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user, string password)
        {
            var domainUser = await _userRepository.FindById(user.Id);
            var hashedPassword = manager.PasswordHasher.HashPassword(user, password);
            return domainUser != null && domainUser.Password == hashedPassword ? IdentityResult.Success : IdentityResult.Failed();
        }
    }
}
