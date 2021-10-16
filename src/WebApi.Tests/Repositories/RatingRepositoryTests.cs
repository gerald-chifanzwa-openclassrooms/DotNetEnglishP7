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
    public class RatingRepositoryTests : IDisposable
    {
        private readonly LocalDbContext _mockDbContext;
        private readonly Faker<Rating> _ratingFaker;

        public RatingRepositoryTests()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<LocalDbContext>();
            dbOptionsBuilder.UseInMemoryDatabase(nameof(RatingRepositoryTests));
            _mockDbContext = new LocalDbContext(dbOptionsBuilder.Options);
            _ratingFaker = new Faker<Rating>();
        }

        [Fact]
        public void GetAll_ShouldReturnRatingsInTheDatabase()
        {
            // Arrange
            InitializeDatabase();
            RatingRepository repository = new(_mockDbContext, new NullLogger<RatingRepository>());

            // Act
            var results = repository.GetAll().GetAwaiter().GetResult();

            // Assert
            results.Should().HaveCount(10);
        }

        [Fact]
        public void Add_WhenRatingIsAdded_ShouldReturnRatingsCollectionIncludingAddedItem()
        {
            // Arrange
            InitializeDatabase();
            var testRating = _ratingFaker.Generate();
            RatingRepository repository = new(_mockDbContext, new NullLogger<RatingRepository>());

            // Act
            var results = repository.Add(testRating).GetAwaiter().GetResult();

            // Assert
            results.Should().HaveCount(11);
            results.Should().Contain(testRating);
        }

        [Fact]
        public void Update_WhenRatingExists_ShouldUpdateExistingRecord()
        {
            // Arrange
            InitializeDatabase();
            var testRating = _ratingFaker.Generate();
            var randomId = new Random().Next(1, 10);
            RatingRepository repository = new(_mockDbContext, new NullLogger<RatingRepository>());

            // Act
            var results = repository.Update(randomId, testRating).GetAwaiter().GetResult();

            // Assert
            results.Should().HaveCount(10);
            results.Should().Contain(x => x.Id == randomId);
            var rating = results.FirstOrDefault(x => x.Id == randomId);

            foreach (var property in typeof(Rating).GetProperties().Where(p => p.Name != nameof(Rating.Id)))
            {
                var expectedValue = property.GetValue(testRating);
                var actualValue = property.GetValue(rating);

                expectedValue.Should().Be(actualValue);
            }
        }

        [Fact]
        public void Delete_WhenRatingExists_ShouldRemoveFromList()
        {
            // Arrange
            InitializeDatabase();
            var randomId = new Random().Next(1, 10);
            RatingRepository repository = new(_mockDbContext, new NullLogger<RatingRepository>());

            // Act
            var results = repository.Delete(randomId).GetAwaiter().GetResult();

            // Assert
            results.Should().HaveCount(9);
            results.Should().NotContain(bid => bid.Id == randomId);
        }


        private void InitializeDatabase()
        {
            var fakeRatings = _ratingFaker.Generate(10);
            _mockDbContext.Database.EnsureCreated();
            _mockDbContext.Ratings.AddRange(fakeRatings);
            _mockDbContext.SaveChanges();
        }

        public void Dispose()
        {
            _mockDbContext.Database.EnsureDeleted();
            _mockDbContext.Dispose();
        }
    }
}
