using System.Threading.Tasks;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;

namespace Dot.Net.WebApi.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Adds a user to the database
        /// </summary>
        Task Add(User user);
        /// <summary>
        /// Updates a user in the database
        /// </summary>
        Task Update(User user);
        /// <summary>
        /// Gets a list of all users in the database
        /// </summary>
        /// <returns></returns>
        Task<User[]> FindAll();
        /// <summary>
        /// Gets a single user from the database
        /// </summary>
        /// <returns></returns>
        Task<User> FindById(int id);
        /// <summary>
        /// Gets a single user with a given <paramref name="userName"/>
        /// </summary>
        /// <returns></returns>
        Task<User> FindByUserName(string userName);
        /// <summary>
        /// Removes a user from the database
        /// </summary> 
        Task Remove(User user);
    }
}