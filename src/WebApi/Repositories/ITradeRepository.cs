using System.Collections.Generic;
using System.Threading.Tasks;
using Dot.Net.WebApi.Domain;

namespace WebApi.Repositories
{
    public interface ITradeRepository
    {
        /// <summary>
        /// Adds a trade to the database
        /// </summary>
        /// <returns>A collection of all trades in the database</returns>
        Task<IReadOnlyCollection<Trade>> Add(Trade trade);
        /// <summary>
        /// Removes a trade in the database
        /// </summary>
        /// <returns>A collection of the trades in the database</returns>
        Task<IReadOnlyCollection<Trade>> Delete(int id);
        /// <summary>
        /// Gets a single trade with the given <paramref name="id"/>
        /// </summary>
        /// <returns>The trade whose id is the <paramref name="id"/> from the database, or null if not found</returns>
        Task<Trade> Get(int id);
        /// <summary>
        /// Gets all the trades from the databse
        /// </summary>
        /// <returns>A collection of the trades in the database</returns>
        Task<IReadOnlyCollection<Trade>> GetAll();
        /// <summary>
        /// Updates a trade in the database
        /// </summary>
        /// <returns>A collection of the trades in the database<</returns>
        Task<IReadOnlyCollection<Trade>> Update(int id, Trade trade);
    }
}