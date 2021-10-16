using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApi.Domain;
using WebApi.Models;

namespace WebApi.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHashService _passwordHasher;
        private readonly IOptions<AuthenticationOptions> _authenticationOptions;

        public AuthenticationService(IUserRepository repository,
                                     IPasswordHashService passwordHasher,
                                     IOptions<AuthenticationOptions> authenticationOptions)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _authenticationOptions = authenticationOptions;
        }

        public async Task<AuthenticationResult> SignIn(string username, string password)
        {
            var user = await _repository.FindByUserName(username);
            if (user == null)
                return AuthenticationResult.Failure("Invalid username or password");

            var hash = _passwordHasher.Hash(password);

            if (!string.Equals(hash, user.Password, StringComparison.InvariantCulture))
                return AuthenticationResult.Failure("Invalid username or password");

            var token = GenerateToken(user);
            return AuthenticationResult.Success(token);

        }

        private string GenerateToken(User user)
        {
            var authenticationOptions = _authenticationOptions.Value;
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authenticationOptions.Key));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserName),
                    new Claim(ClaimTypes.Name, user.FullName),
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = authenticationOptions.Issuer,
                Audience = authenticationOptions.Audience,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
