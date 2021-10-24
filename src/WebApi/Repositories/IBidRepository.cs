using System.Collections.Generic;
using System.Threading.Tasks;
using Dot.Net.WebApi.Domain;

namespace WebApi.Repositories
{
    public interface IBidRepository
    {
        /// <summary>
        /// Add bid list to the database.
        /// </summary>
        /// <param name="bidList"></param>
        /// <returns>An updated collection of all the bidlists in the database</returns>
        Task<IReadOnlyCollection<BidList>> Add(BidList bidList);
        /// <summary>
        /// Updates a Bidlist in the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bidList"></param>
        /// <returns>An updated collection of all the bidlists in the database</returns>
        Task<IReadOnlyCollection<BidList>> Update(int id, BidList bidList);
        /// <summary>
        /// Gets all bidlists in the database
        /// </summary>
        /// <returns>A collection of all the bidlists in the database</returns>
        Task<IReadOnlyCollection<BidList>> GetAll();
        /// <summary>
        /// Gets a single bidlist with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The Bidlist with the given Id</returns>
        Task<BidList> Get(int id);
        /// <summary>
        /// Deletes a bidlist from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A collection of all the remaining bidlists in the database</returns>
        Task<IReadOnlyCollection<BidList>> Delete(int id);
    }
}