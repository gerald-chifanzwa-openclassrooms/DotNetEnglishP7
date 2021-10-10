using System.Threading.Tasks;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;

namespace Dot.Net.WebApi.Repositories
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task Update(User user);
        Task<User[]> FindAll();
        Task<User> FindById(int id);
        Task<User> FindByUserName(string userName);
        Task Remove(User user);
    }
}