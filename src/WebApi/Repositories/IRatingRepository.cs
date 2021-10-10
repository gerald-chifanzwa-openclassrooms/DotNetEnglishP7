using Dot.Net.WebApi.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Repositories
{
    public interface IRatingRepository
    {
        Task<IReadOnlyCollection<Rating>> Add(Rating rating);
        Task<IReadOnlyCollection<Rating>> Update(int id, Rating rating);
        Task<IReadOnlyCollection<Rating>> GetAll();
        Task<Rating> Get(int id);
        Task<IReadOnlyCollection<Rating>> Delete(int id);
    }
}
