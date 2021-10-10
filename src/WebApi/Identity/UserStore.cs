using System;
using System.Threading;
using System.Threading.Tasks;
using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Repositories;
using Microsoft.AspNetCore.Identity;

namespace WebApi.Identity
{
    public class UserStore : IUserStore<ApplicationUser>
    {
        private readonly IUserRepository _userRepository;

        public UserStore(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            try
            {
                User domainUser = FromApplicationUser(user);
                await _userRepository.Add(domainUser);
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = ex.GetType().Name,
                    Description = ex.Message
                });
            }
        }
        public async Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            try
            {
                User domainUser = FromApplicationUser(user);
                await _userRepository.Remove(domainUser);
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = ex.GetType().Name,
                    Description = ex.Message
                });
            }
        }

        public void Dispose() { }

        public async Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            int id = int.Parse(userId);
            var user = await _userRepository.FindById(id);
            return user == null ? null : ToApplicationUser(user);
        }

        public async Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByUserName(normalizedUserName);
            return user == null ? null : ToApplicationUser(user);
        }
        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken) => Task.FromResult(user.NormalizedUserName ?? user.UserName.ToUpper());

        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken) => Task.FromResult(user.Id.ToString());
        public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken) => Task.FromResult(user.UserName);
        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }
        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }
        public async Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            try
            {
                var domainUser = FromApplicationUser(user);
                await _userRepository.Update(domainUser);
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = ex.GetType().Name,
                    Description = ex.Message
                });
            }
        }

        private User FromApplicationUser(ApplicationUser user)
        {
            return new User
            {
                FullName = user.FullName,
                Id = user.Id,
                UserName = user.UserName,
            };
        }
        private ApplicationUser ToApplicationUser(User user)
        {
            return new ApplicationUser
            {
                FullName = user.FullName,
                Id = user.Id,
                UserName = user.UserName,
                NormalizedUserName = user.UserName.ToUpper()
            };
        }

    }
}
