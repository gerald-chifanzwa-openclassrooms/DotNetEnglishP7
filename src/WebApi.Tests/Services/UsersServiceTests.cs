using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using WebApi.Models;
using WebApi.Services;
using Xunit;

namespace WebApi.Tests.Services
{
    public class UsersServiceTests
    {
        private readonly Mock<IUserRepository> _mockRepository;
        private readonly Mock<IPasswordHashService> _mockPasswordHasher;

        public UsersServiceTests()
        {
            _mockPasswordHasher = new Mock<IPasswordHashService>();
            _mockRepository = new Mock<IUserRepository>();
        }

        [Fact]
        public void AddUser_ShouldSaveUserInRepository()
        {
            _mockRepository.Setup(r => r.Add(It.IsNotNull<User>())).Verifiable();
            _mockPasswordHasher.Setup(p => p.Hash(It.IsNotNull<string>())).Returns((string password) => password);
            var userService = GetUserService();
            var user = new UserModel
            {
                FullName = "Test User",
                Password = "Password@123",
                Role = "TEST",
                UserName = "test"
            };
            var result = userService.AddUser(user).GetAwaiter().GetResult();

            result.Should().NotBeNull();
            _mockRepository.Verify();
        }

        private UsersService GetUserService()
        {
            var mapper = new AutoMapper.MapperConfiguration(config => config.CreateMap<User, UserViewModel>()).CreateMapper();
            return new UsersService(_mockRepository.Object, _mockPasswordHasher.Object, new NullLogger<UsersService>(), mapper);
        }
    }
}
