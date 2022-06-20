using BudgetAPI.Data.Configuration;
using BudgetAPI.Models;

namespace BudgetAPI.Services
{

    public interface IUserService
    {
        Task<User> CreateUser(string email, string password, string name, CancellationToken cancellationToken);
    }
    internal class UserService : IUserService
    {
        public async Task<User> CreateUser(string email, string password, string name, CancellationToken cancellationToken)
        {
            var User = new User(email, password, name);
            using (var context = new ApplicationDbContext())
            {
               await context.AddAsync(User, cancellationToken);
               await context.SaveChangesAsync();
            }
            return User;
        }
    }
}
