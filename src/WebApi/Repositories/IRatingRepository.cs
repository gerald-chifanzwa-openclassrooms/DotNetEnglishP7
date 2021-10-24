using Dot.Net.WebApi.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Repositories
{
    public interface IRatingRepository
    {
        /// <summary>
        /// Add rating to the database.
        /// </summary>
        /// <returns>An updated collection of all the ratings in the database</returns>
        Task<IReadOnlyCollection<Rating>> Add(Rating rating);
        
        /// <summary>
        /// Updates a rating in the database.
        /// </summary>
        /// <returns>An updated collection of all the ratings in the database</returns>
        Task<IReadOnlyCollection<Rating>> Update(int id, Rating rating);

        /// <summary>
        /// Gets all ratings in the database.
        /// </summary>
        /// <returns>A collection of all the ratings in the database</returns>
        Task<IReadOnlyCollection<Rating>> GetAll();

        /// <summary>
        /// Gets a rating with a give <paramref name="id"/>.
        /// </summary>
        /// <returns>A rating whose id is the given  <paramref name="id"/> from the database</returns>
        Task<Rating> Get(int id);

        /// <summary>
        /// Deletes a rating from the database.
        /// </summary>
        /// <returns>An updated collection of all the ratings in the database</returns>
        Task<IReadOnlyCollection<Rating>> Delete(int id);
    }
}
