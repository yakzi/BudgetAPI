using BudgetAPI.Data.Configuration;
using BudgetAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetAPI.Repositories
{
    public interface IUserRepository
    {
        Task InsertNewUser(User user, CancellationToken cancellationToken);
        Task<User> LoadUser(string guid, string password, CancellationToken cancellationToken);
    }
    internal class SQLUserRepository : IUserRepository
    {
        public async Task InsertNewUser(User user, CancellationToken cancellationToken)
        {
            using (var context = new ApplicationDbContext())
            {
                await context.AddAsync(user, cancellationToken);
                await context.SaveChangesAsync();
            }
        }

        public async Task<User> LoadUser(string guid, string password, CancellationToken cancellationToken)
        {
            using (var context = new ApplicationDbContext())
            {
                return await context.Users.FirstOrDefaultAsync(o => o.Id.Equals(Guid.Parse(guid)) && o.Password.Equals(password), cancellationToken);
            }
        }
    }
}
