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
    public class CurvePointRepositoryTests : IDisposable
    {
        private readonly LocalDbContext _mockDbContext;
        private readonly Faker<CurvePoint> _curvePointFaker;

        public CurvePointRepositoryTests()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<LocalDbContext>();
            dbOptionsBuilder.UseInMemoryDatabase(nameof(CurvePointRepositoryTests));
            _mockDbContext = new LocalDbContext(dbOptionsBuilder.Options);
            _curvePointFaker = new Faker<CurvePoint>();
        }

        [Fact]
        public void GetAll_ShouldReturnCurvePointsInTheDatabase()
        {
            // Arrange
            InitializeDatabase();
            CurvePointRepository repository = new(_mockDbContext, new NullLogger<CurvePointRepository>());

            // Act
            var results = repository.GetAll().GetAwaiter().GetResult();

            // Assert
            results.Should().HaveCount(10);
        }

        [Fact]
        public void Add_WhenCurvePointIsAdded_ShouldReturnCurvePointsCollectionIncludingAddedItem()
        {
            // Arrange
            InitializeDatabase();
            var testCurvePoint = _curvePointFaker.Generate();
            CurvePointRepository repository = new(_mockDbContext, new NullLogger<CurvePointRepository>());

            // Act
            var results = repository.Add(testCurvePoint).GetAwaiter().GetResult();

            // Assert
            results.Should().HaveCount(11);
            results.Should().Contain(testCurvePoint);
        }

        [Fact]
        public void Update_WhenCurvePointExists_ShouldUpdateExistingRecord()
        {
            // Arrange
            InitializeDatabase();
            var testCurvePoint = _curvePointFaker.Generate();
            var randomId = new Random().Next(1, 10);
            CurvePointRepository repository = new(_mockDbContext, new NullLogger<CurvePointRepository>());

            // Act
            var results = repository.Update(randomId, testCurvePoint).GetAwaiter().GetResult();

            // Assert
            results.Should().HaveCount(10);
            results.Should().Contain(c => c.Id == randomId);
            var curvePoint = results.FirstOrDefault(c => c.Id == randomId);

            foreach (var property in typeof(CurvePoint).GetProperties().Where(p => p.Name != nameof(CurvePoint.Id)))
            {
                var expectedValue = property.GetValue(testCurvePoint);
                var actualValue = property.GetValue(curvePoint);

                expectedValue.Should().Be(actualValue);
            }
        }

        [Fact]
        public void Delete_WhenCurvePointExists_ShouldRemoveFromList()
        {
            // Arrange
            InitializeDatabase();
            var randomId = new Random().Next(1, 10);
            CurvePointRepository repository = new(_mockDbContext, new NullLogger<CurvePointRepository>());

            // Act
            var results = repository.Delete(randomId).GetAwaiter().GetResult();

            // Assert
            results.Should().HaveCount(9);
            results.Should().NotContain(bid => bid.Id == randomId);
        }


        private void InitializeDatabase()
        {
            var fakeCurvePoints = _curvePointFaker.Generate(10);
            _mockDbContext.Database.EnsureCreated();
            _mockDbContext.CurvePoints.AddRange(fakeCurvePoints);
            _mockDbContext.SaveChanges();
        }

        public void Dispose()
        {
            _mockDbContext.Database.EnsureDeleted();
            _mockDbContext.Dispose();
        }
    }
}
