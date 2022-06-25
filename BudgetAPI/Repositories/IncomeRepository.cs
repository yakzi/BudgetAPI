using BudgetAPI.Data.Configuration;
using BudgetAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace BudgetAPI.Repositories
{
    public interface IIncomeRepository
    {
        Task InsertNewIncome(Income income, CancellationToken cancellationToken);
        Task<List<Income>> LoadIncomesForUser(string userId, CancellationToken cancellationToken);
    }
    internal class SQLIncomeRepository : IIncomeRepository
    {

        private readonly ApplicationDbContext _context;

        public SQLIncomeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task InsertNewIncome(Income income, CancellationToken cancellationToken)
        {
            
                await _context.AddAsync(income, cancellationToken);
                await _context.SaveChangesAsync();
            
        }

        public async Task<List<Income>> LoadIncomesForUser(string userId, CancellationToken cancellationToken)
        {
            var tasks = await _context.Incomes.Where(e => e.UserId == Guid.Parse(userId)).ToListAsync();
            return tasks;
            
        }
    }
}
