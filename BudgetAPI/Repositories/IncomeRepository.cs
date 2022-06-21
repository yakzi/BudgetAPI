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
        public async Task InsertNewIncome(Income income, CancellationToken cancellationToken)
        {
            using (var context = new ApplicationDbContext())
            {
                await context.AddAsync(income, cancellationToken);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Income>> LoadIncomesForUser(string userId, CancellationToken cancellationToken)
        {
            using (var context = new ApplicationDbContext())
            {
            var tasks = await context.Incomes.Where(e => e.UserId == Guid.Parse(userId)).ToListAsync();
            return tasks;
            }
        }
    }
}
