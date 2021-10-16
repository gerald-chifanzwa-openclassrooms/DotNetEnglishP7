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
    public class TradeRepositoryTests : IDisposable
    {
        private readonly LocalDbContext _mockDbContext;
        private readonly Faker<Trade> _tradeFaker;

        public TradeRepositoryTests()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<LocalDbContext>();
            dbOptionsBuilder.UseInMemoryDatabase(nameof(TradeRepositoryTests));
            _mockDbContext = new LocalDbContext(dbOptionsBuilder.Options);
            _tradeFaker = new Faker<Trade>();
        }

        [Fact]
        public void GetAll_ShouldReturnTradesInTheDatabase()
        {
            // Arrange
            InitializeDatabase();
            TradeRepository repository = new(_mockDbContext, new NullLogger<TradeRepository>());

            // Act
            var results = repository.GetAll().GetAwaiter().GetResult();

            // Assert
            results.Should().HaveCount(10);
        }

        [Fact]
        public void Add_WhenTradeIsAdded_ShouldReturnTradesCollectionIncludingAddedItem()
        {
            // Arrange
            InitializeDatabase();
            var testTrade = _tradeFaker.Generate();
            TradeRepository repository = new(_mockDbContext, new NullLogger<TradeRepository>());

            // Act
            var results = repository.Add(testTrade).GetAwaiter().GetResult();

            // Assert
            results.Should().HaveCount(11);
            results.Should().Contain(testTrade);
        }

        [Fact]
        public void Update_WhenTradeExists_ShouldUpdateExistingRecord()
        {
            // Arrange
            InitializeDatabase();
            var testRule = _tradeFaker.Generate();
            var randomId = new Random().Next(1, 10);
            TradeRepository repository = new(_mockDbContext, new NullLogger<TradeRepository>());

            // Act
            var results = repository.Update(randomId, testRule).GetAwaiter().GetResult();

            // Assert
            results.Should().HaveCount(10);
            results.Should().Contain(t => t.Id == randomId);
            var trade = results.FirstOrDefault(t => t.Id == randomId);

            foreach (var property in typeof(Trade).GetProperties().Where(p => p.Name != nameof(Trade.Id)))
            {
                var expectedValue = property.GetValue(testRule);
                var actualValue = property.GetValue(trade);

                expectedValue.Should().Be(actualValue);
            }
        }

        [Fact]
        public void Delete_WhenTradeExists_ShouldRemoveFromList()
        {
            // Arrange
            InitializeDatabase();
            var randomId = new Random().Next(1, 10);
            TradeRepository repository = new(_mockDbContext, new NullLogger<TradeRepository>());

            // Act
            var results = repository.Delete(randomId).GetAwaiter().GetResult();

            // Assert
            results.Should().HaveCount(9);
            results.Should().NotContain(t => t.Id == randomId);
        }


        private void InitializeDatabase()
        {
            var fakeTrades = _tradeFaker.Generate(10);
            _mockDbContext.Database.EnsureCreated();
            _mockDbContext.Trades.AddRange(fakeTrades);
            _mockDbContext.SaveChanges();
        }

        public void Dispose()
        {
            _mockDbContext.Database.EnsureDeleted();
            _mockDbContext.Dispose();
        }
    }
}
