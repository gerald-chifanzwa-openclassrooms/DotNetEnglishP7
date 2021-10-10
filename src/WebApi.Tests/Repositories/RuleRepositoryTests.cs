using System;
using System.Linq;
using Bogus;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using WebApi.Repositories;
using Xunit;

namespace WebApi.Tests.Repositories
{
    public class RuleRepositoryTests : IDisposable
    {
        private readonly LocalDbContext _mockDbContext;
        private readonly Faker<RuleName> _ruleFaker;

        public RuleRepositoryTests()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<LocalDbContext>();
            dbOptionsBuilder.UseInMemoryDatabase(nameof(RuleRepositoryTests));
            _mockDbContext = new LocalDbContext(dbOptionsBuilder.Options);
            _ruleFaker = new Faker<RuleName>();
        }

        [Fact]
        public void GetAll_ShouldReturnRulesInTheDatabase()
        {
            // Arrange
            InitializeDatabase();
            RuleRepository repository = new(_mockDbContext, new NullLogger<RuleRepository>());

            // Act
            var results = repository.GetAll().GetAwaiter().GetResult();

            // Assert
            results.Should().HaveCount(10);
        }

        [Fact]
        public void Add_WhenRuleIsAdded_ShouldReturnRulesCollectionIncludingAddedItem()
        {
            // Arrange
            InitializeDatabase();
            var testRule = _ruleFaker.Generate();
            RuleRepository repository = new(_mockDbContext, new NullLogger<RuleRepository>());

            // Act
            var results = repository.Add(testRule).GetAwaiter().GetResult();

            // Assert
            results.Should().HaveCount(11);
            results.Should().Contain(testRule);
        }

        [Fact]
        public void Update_WhenRuleExists_ShouldUpdateExistingRecord()
        {
            // Arrange
            InitializeDatabase();
            var testRule = _ruleFaker.Generate();
            var randomId = new Random().Next(1, 10);
            RuleRepository repository = new(_mockDbContext, new NullLogger<RuleRepository>());

            // Act
            var results = repository.Update(randomId, testRule).GetAwaiter().GetResult();

            // Assert
            results.Should().HaveCount(10);
            results.Should().Contain(bid => bid.Id == randomId);
            var bid = results.FirstOrDefault(bid => bid.Id == randomId);

            foreach (var property in typeof(RuleName).GetProperties().Where(p => p.Name != nameof(RuleName.Id)))
            {
                var expectedValue = property.GetValue(testRule);
                var actualValue = property.GetValue(bid);

                expectedValue.Should().Be(actualValue);
            }
        }

        [Fact]
        public void Delete_WhenRuleExists_ShouldRemoveFromList()
        {
            // Arrange
            InitializeDatabase();
            var randomId = new Random().Next(1, 10);
            RuleRepository repository = new(_mockDbContext, new NullLogger<RuleRepository>());

            // Act
            var results = repository.Delete(randomId).GetAwaiter().GetResult();

            // Assert
            results.Should().HaveCount(9);
            results.Should().NotContain(bid => bid.Id == randomId);
        }


        private void InitializeDatabase()
        {
            var fakeRules = _ruleFaker.Generate(10);
            _mockDbContext.Database.EnsureCreated();
            _mockDbContext.Rules.AddRange(fakeRules);
            _mockDbContext.SaveChanges();
        }

        public void Dispose()
        {
            _mockDbContext.Database.EnsureDeleted();
            _mockDbContext.Dispose();
        }
    }
}
