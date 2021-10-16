using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Dot.Net.WebApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        public LocalDbContext DbContext { get; }

        public UserRepository(LocalDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public Task<User> FindByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName)) 
                throw new ArgumentException("Username cannot be empty", nameof(userName));

            return DbContext.Users.FirstOrDefaultAsync(user => user.UserName == userName);
        }

        public Task<User[]> FindAll()
        {
            return DbContext.Users.ToArrayAsync();
        }

        public async Task Add(User user)
        {
            DbContext.Users.Add(user);
            await DbContext.SaveChangesAsync();
        }

        public Task<User> FindById(int id)
        {
            return DbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task Update(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            DbContext.Users.Update(user);
            await DbContext.SaveChangesAsync();
        }
        public async Task Remove(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            DbContext.Users.Remove(user);
            await DbContext.SaveChangesAsync();
        }
    }
}