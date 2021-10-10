using Dot.Net.WebApi.Controllers;
using Dot.Net.WebApi.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Repositories
{
    public interface IRuleRepository
    {
        Task<IReadOnlyCollection<RuleName>> Add(RuleName bidList);
        Task<IReadOnlyCollection<RuleName>> Update(int id, RuleName bidList);
        Task<IReadOnlyCollection<RuleName>> GetAll();
        Task<RuleName> Get(int id);
        Task<IReadOnlyCollection<RuleName>> Delete(int id);
    }
}
