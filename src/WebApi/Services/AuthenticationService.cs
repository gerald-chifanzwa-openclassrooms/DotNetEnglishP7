﻿using System;
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
            // First find the user by the supplied username
            var user = await _repository.FindByUserName(username);
            
            // If user cannot be found, return authentication failure
            if (user == null)
                return AuthenticationResult.Failure("Invalid username or password");

            // Hash the given password and compare with the one from the database
            var hash = _passwordHasher.Hash(password);

            // If the hashes match, authenticate user, otherwise authentication failure
            if (!string.Equals(hash, user.Password, StringComparison.InvariantCulture))
                return AuthenticationResult.Failure("Invalid username or password");

            var token = GenerateToken(user);
            return AuthenticationResult.Success(token);

        }

        private string GenerateToken(User user)
        {
            // Load token generation parameters from configuration
            var authenticationOptions = _authenticationOptions.Value;
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authenticationOptions.Key));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenLifeSpan = (authenticationOptions.TokenLifespan ?? TimeSpan.Zero) > TimeSpan.Zero ?
                authenticationOptions.TokenLifespan.Value :
                TimeSpan.FromHours(1);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Add user details to be part of the token
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserName),
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.Add(tokenLifeSpan),
                Issuer = authenticationOptions.Issuer,
                Audience = authenticationOptions.Audience,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
