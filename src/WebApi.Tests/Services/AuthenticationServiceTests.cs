using System;
using Dot.Net.WebApi.Domain;
using WebApi.Services;
using Moq;
using Dot.Net.WebApi.Repositories;
using Microsoft.Extensions.Options;
using WebApi.Domain;
using FluentAssertions;
using WebApi.Models;
using Xunit;

namespace WebApi.Tests.Services
{
    public class AuthenticationServiceTests
    {
        private readonly Mock<IUserRepository> _mockRepository;
        private readonly Mock<IPasswordHashService> _mockPasswordHasher;
        
        public AuthenticationServiceTests()
        {
            _mockPasswordHasher = new Mock<IPasswordHashService>();
            _mockRepository = new Mock<IUserRepository>();
        }

        [Fact]
        public void SignIn_GivenValidCredentials_ShouldGenerateAccessToken()
        {
            // Arrange
            var user = new User
            {
                FullName = "Test User",
                Id = 1,
                UserName = "test",
                Password = "password",
                Role = "TEST_ROLE"
            };

            var options = new OptionsWrapper<AuthenticationOptions>(new()
            {
                TokenLifespan = TimeSpan.FromHours(1),
                Audience = "http://test.test",
                Issuer = "http://test.test",
                Key = "E658866B-0873-4FF2-ACE1-71708CEDCB94"
            });

            _mockRepository.Setup(r => r.FindByUserName(It.IsNotNull<string>())).ReturnsAsync(user);
            _mockPasswordHasher.Setup(p => p.Hash(It.IsNotNull<string>())).Returns((string password) => password);
            AuthenticationService authenticationService = new(_mockRepository.Object, _mockPasswordHasher.Object, options);

            // Act
            var result = authenticationService.SignIn("test", "password").GetAwaiter().GetResult();

            // Assert
            result.Should().NotBeNull();
            result.Succeeded.Should().BeTrue();
            result.As<AuthenticationResult.SuccessAuthenticationResult>()
                  .AccessToken.Should().NotBeNullOrEmpty();
        }
    }
}
