using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class BidRepository : IBidRepository
    {
        private readonly LocalDbContext _dbContext;
        private readonly ILogger<BidRepository> _logger;

        public BidRepository(LocalDbContext dbContext, ILogger<BidRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IReadOnlyCollection<BidList>> Add(BidList bidList)
        {
            if (bidList == null) throw new ArgumentNullException(nameof(bidList));

            _logger.LogInformation("Adding new bid list {@BidList}", bidList);
            _dbContext.Bids.Add(bidList);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("BidList Saved {@BidList}", bidList);

            return await _dbContext.Bids.ToListAsync();
        }

        public async Task<IReadOnlyCollection<BidList>> GetAll()
        {
            _logger.LogInformation("Listing bids in the databaase");
            var bids = await _dbContext.Bids.ToListAsync();
            return bids;
        }

    }
}
