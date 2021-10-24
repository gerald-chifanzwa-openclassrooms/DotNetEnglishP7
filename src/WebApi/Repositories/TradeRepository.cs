using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Repositories
{
    public class TradeRepository : ITradeRepository
    {
        private readonly LocalDbContext _dbContext;
        private readonly ILogger<TradeRepository> _logger;

        public TradeRepository(LocalDbContext dbContext, ILogger<TradeRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <inheritdoc/> 
        public async Task<IReadOnlyCollection<Trade>> Add(Trade trade)
        {
            if (trade == null) throw new ArgumentNullException(nameof(trade));

            _logger.LogInformation("Adding new trade {@Trade}", trade);
            _dbContext.Trades.Add(trade);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Trade Saved {@Trade}", trade);

            return await _dbContext.Trades.ToListAsync();
        }

        /// <inheritdoc/> 
        public async Task<IReadOnlyCollection<Trade>> Update(int id, Trade trade)
        {
            if (trade == null) throw new ArgumentNullException(nameof(trade));

            var tradeItem = await _dbContext.Trades.FirstOrDefaultAsync(t => t.Id == id);
            if (tradeItem == null) throw new EntityNotFoundException();

            _logger.LogInformation("Updating trade {@TradeItem} to {@Trade}", tradeItem, trade);

            tradeItem.Account = trade.Account;
            tradeItem.Type = trade.Type;
            tradeItem.BuyQuantity = trade.BuyQuantity;
            tradeItem.BuyPrice = trade.BuyPrice;
            tradeItem.SellQuantity = trade.SellQuantity;
            tradeItem.SellPrice = trade.SellPrice;
            tradeItem.Benchmark = trade.Benchmark;
            tradeItem.TradeDate = trade.TradeDate;
            tradeItem.Security = trade.Security;
            tradeItem.Status = trade.Status;
            tradeItem.Trader = trade.Trader;
            tradeItem.Book = trade.Book;
            tradeItem.CreationName = trade.CreationName;
            tradeItem.CreationDate = trade.CreationDate;
            tradeItem.RevisionName = trade.RevisionName;
            tradeItem.DealName = trade.DealName;
            tradeItem.DealType = trade.DealType;
            tradeItem.SourceListId = trade.SourceListId;
            tradeItem.Side = trade.Side;

            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Trade Saved {@Trade}", trade);

            return await _dbContext.Trades.ToListAsync();
        }

        /// <inheritdoc/> 
        public async Task<IReadOnlyCollection<Trade>> GetAll()
        {
            _logger.LogInformation("Listing trades in the databaase");
            var rules = await _dbContext.Trades.ToListAsync();
            return rules;
        }

        /// <inheritdoc/> 
        public async Task<Trade> Get(int id)
        {
            _logger.LogInformation("Getting trade with id \"{Id}\"", id);

            var trade = await _dbContext.Trades.FirstOrDefaultAsync(b => b.Id == id);
            return trade == null ? throw new EntityNotFoundException() : trade;
        }

        /// <inheritdoc/> 
        public async Task<IReadOnlyCollection<Trade>> Delete(int id)
        {
            _logger.LogInformation("Deleting trade with id \"{Id}\"", id);

            var trade = await _dbContext.Trades.FirstOrDefaultAsync(b => b.Id == id);
            if (trade == null) throw new EntityNotFoundException();

            _dbContext.Trades.Remove(trade);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Bid Deleted {@Trade}", trade);

            return await _dbContext.Trades.ToListAsync();
        }
    }
}
