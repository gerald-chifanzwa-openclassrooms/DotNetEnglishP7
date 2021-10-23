using WebApi.Services;
using FluentAssertions;
using Xunit;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace WebApi.Tests.Services
{
    public class PasswordHasherTests
    {
        [Fact]
        public void Hash_ShouldEncryptPassword()
        {
            Dictionary<string, string> config = new()
            { { "Security:EncryptionKey", "TEST" } };

            var configurationRoot = new ConfigurationBuilder().AddInMemoryCollection(config).Build();
            var hasher = new PasswordHashService(configurationRoot);

            var hash = hasher.Hash("password");

            hash.Should().NotBeNullOrEmpty();
            hash.Should().NotBe("password");
        }
    }
}
