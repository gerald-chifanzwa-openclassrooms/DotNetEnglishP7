using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IUsersService
    {
        Task<UserViewModel> AddUser(UserModel userModel);
        Task<UserViewModel> DeleteUser(int id);
        Task<UserViewModel> GetUser(int id);
        Task<UserViewModel> GetUserByUsername(string username);
        Task<IReadOnlyCollection<UserViewModel>> GetUsers();
        Task<UserViewModel> UpdateUser(int id, UserModel userModel);
    }
}