using System.Collections.Generic;
using System.Threading.Tasks;
using Dot.Net.WebApi.Domain;

namespace WebApi.Repositories
{
    public interface ITradeRepository
    {
        Task<IReadOnlyCollection<Trade>> Add(Trade trade);
        Task<IReadOnlyCollection<Trade>> Delete(int id);
        Task<Trade> Get(int id);
        Task<IReadOnlyCollection<Trade>> GetAll();
        Task<IReadOnlyCollection<Trade>> Update(int id, Trade trade);
    }
}