using Dot.Net.WebApi.Data;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using Dot.Net.WebApi.Domain;

namespace WebApi.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly LocalDbContext _dbContext;
        private readonly ILogger<RatingRepository> _logger;

        public RatingRepository(LocalDbContext dbContext, ILogger<RatingRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <inheritdoc/> 
        public async Task<IReadOnlyCollection<Rating>> Add(Rating rating)
        {
            if (rating == null) throw new ArgumentNullException(nameof(rating));

            _logger.LogInformation("Adding new rating {@Rating}", rating);
            _dbContext.Ratings.Add(rating);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Rating Saved {@Rating}", rating);

            return await _dbContext.Ratings.ToListAsync();
        }

        /// <inheritdoc/> 
        public async Task<IReadOnlyCollection<Rating>> Update(int id, Rating rating)
        {
            if (rating == null) throw new ArgumentNullException(nameof(rating));

            var ratingItem = await _dbContext.Ratings.FirstOrDefaultAsync(b => b.Id == id);
            if (ratingItem == null) throw new EntityNotFoundException();

            _logger.LogInformation("Updating rating {@Point} to {@Rating}", ratingItem, rating);

            ratingItem.MoodysRating = rating.MoodysRating;
            rating.SandPRating = rating.SandPRating;
            rating.FitchRating = rating.FitchRating;
            rating.OrderNumber = rating.OrderNumber;

            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Rating Saved {@Rating}", rating);

            return await _dbContext.Ratings.ToListAsync();
        }

        /// <inheritdoc/> 
        public async Task<IReadOnlyCollection<Rating>> GetAll()
        {
            _logger.LogInformation("Listing ratings in the databaase");
            var ratings = await _dbContext.Ratings.ToListAsync();
            return ratings;
        }

        /// <inheritdoc/> 
        public async Task<Rating> Get(int id)
        {
            _logger.LogInformation("Getting rating with id \"{Id}\"", id);

            var rating = await _dbContext.Ratings.FirstOrDefaultAsync(b => b.Id == id);
            return rating == null ? throw new EntityNotFoundException() : rating;
        }

        /// <inheritdoc/> 
        public async Task<IReadOnlyCollection<Rating>> Delete(int id)
        {
            _logger.LogInformation("Deleting rating with id \"{Id}\"", id);

            var rating = await _dbContext.Ratings.FirstOrDefaultAsync(b => b.Id == id);
            if (rating == null) throw new EntityNotFoundException();

            _dbContext.Ratings.Remove(rating);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Bid Deleted {@Point}", rating);

            return await _dbContext.Ratings.ToListAsync();
        }
    }
}
