using Dot.Net.WebApi.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Repositories
{
    public interface ICurvePointRepository
    {
        /// <summary>
        /// Add curvepoint to the database.
        /// </summary>
        /// <returns>An updated collection of all the curvepoints in the database</returns>
        Task<IReadOnlyCollection<CurvePoint>> Add(CurvePoint curvePoint);

        /// <summary>
        /// Updates a curvepoint to the database.
        /// </summary>
        /// <returns>An updated collection of all the curvepoints in the database</returns>
        Task<IReadOnlyCollection<CurvePoint>> Update(int id, CurvePoint curvePoint);

        /// <summary>
        /// Gets all the curvepoints in the database.
        /// </summary>
        /// <returns>A collection of all the curvepoints in the database</returns>
        Task<IReadOnlyCollection<CurvePoint>> GetAll();

        /// <summary>
        /// Gets a curvepoint in the database with a given <paramref name="id"/>.
        /// </summary>
        /// <returns>The curvepoints with the given id from the database</returns>
        Task<CurvePoint> Get(int id);

        /// <summary>
        /// Removes a curvepoint to the database.
        /// </summary>
        /// <returns>An updated collection of all the curvepoints in the database</returns>
        Task<IReadOnlyCollection<CurvePoint>> Delete(int id);
    }
}
