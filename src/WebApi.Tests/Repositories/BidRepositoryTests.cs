using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class BidRepositoryTests : IDisposable
    {
        private readonly LocalDbContext _mockDbContext;

        public BidRepositoryTests()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<LocalDbContext>();
            dbOptionsBuilder.UseInMemoryDatabase(nameof(BidRepositoryTests));
            _mockDbContext = new LocalDbContext(dbOptionsBuilder.Options);
        }

        [Fact]
        public void Add_WhenBidListIsAdded_ShouldReturnBidsCollectionIncludingAddedItem()
        {
            // Arrange
            var bidFaker = new Faker<BidList>();
            var fakeBids = bidFaker.Generate(10);
            _mockDbContext.Database.EnsureCreated();
            _mockDbContext.Bids.AddRange(fakeBids);
            _mockDbContext.SaveChanges();
            BidRepository repository = new(_mockDbContext, new NullLogger<BidRepository>());

            var testBid = bidFaker.Generate();
            // Act
            var results = repository.Add(testBid).GetAwaiter().GetResult();

            // Assert
            results.Should().HaveCount(11);
            results.Should().Contain(testBid);
        }


        public void Dispose()
        {
            _mockDbContext.Database.EnsureDeleted();
            _mockDbContext.Dispose();
        }
    }
}
