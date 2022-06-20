using BudgetAPI.Data.Configuration;
using BudgetAPI.Models;
using System.Collections.Concurrent;

namespace BudgetAPI.Repositories
{
    public interface IExpenseRepository
    {
        Task InsertNewExpense(Expense expense, CancellationToken cancellationToken);
        Task<List<Expense>> LoadExpensesForUser(string userId, CancellationToken cancellationToken);
    }
    internal class SQLExpenseRepository : IExpenseRepository
    {
        public async Task InsertNewExpense(Expense expense, CancellationToken cancellationToken)
        {
            using (var context = new ApplicationDbContext())
            {
                await context.AddAsync(expense, cancellationToken);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Expense>> LoadExpensesForUser(string userId, CancellationToken cancellationToken)
        {
            using (var context = new ApplicationDbContext())
            {
            var tasks = context.Expenses.Where(e => e.UserId == Guid.Parse(userId)).ToList();
            return tasks;
            }
        }
    }
}
