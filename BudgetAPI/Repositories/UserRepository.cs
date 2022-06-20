using BudgetAPI.Data.Configuration;
using BudgetAPI.Models;

namespace BudgetAPI.Repositories
{
    public interface IUserRepository
    {
        Task InsertNewUser(User user, CancellationToken cancellationToken);
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
    }
}
