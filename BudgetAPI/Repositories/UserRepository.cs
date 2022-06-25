using BudgetAPI.Data.Configuration;
using BudgetAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetAPI.Repositories
{
    public interface IUserRepository
    {
        Task InsertNewUser(User user, CancellationToken cancellationToken);
        Task<User?> LoadUser(string guid, string password, CancellationToken cancellationToken);
        Task<bool> CheckUser(string guid, CancellationToken cancellationToken);
    }
    internal class SQLUserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _context;

        public SQLUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task InsertNewUser(User user, CancellationToken cancellationToken)
        {
                await _context.AddAsync(user, cancellationToken);
                await _context.SaveChangesAsync();
            
        }

        public async Task<User?> LoadUser(string guid, string password, CancellationToken cancellationToken)
        {
            
                return await _context.Users.FirstOrDefaultAsync(o => o.Id.Equals(Guid.Parse(guid)) && o.Password.Equals(password), cancellationToken);
            
        }

        public async Task<bool> CheckUser(string guid, CancellationToken cancellationToken)
        {
            
                return await _context.Users.AnyAsync(o => o.Id.Equals(Guid.Parse(guid)), cancellationToken);
            
        }
    }
}
