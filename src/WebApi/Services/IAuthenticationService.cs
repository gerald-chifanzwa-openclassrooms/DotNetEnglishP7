using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> SignIn(string username, string password);
    }
}