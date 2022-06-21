using BudgetAPI.Data.Configuration;
using BudgetAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace BudgetAPI.Repositories
{
    public interface IExpenseRepository
    {
        Task InsertNewExpense(Expense expense, CancellationToken cancellationToken);
        Task<List<Expense>> LoadExpensesForUser(string userId, CancellationToken cancellationToken);
        Task<List<Expense>> LoadExpensesForUserWithSpecificDesc(string userId, string search, CancellationToken cancellationToken);
        Task<List<Expense>> LoadExpensesForUserFromCurrentMonth(string userId, CancellationToken cancellationToken);
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
            var tasks = await context.Expenses.Where(e => e.UserId == Guid.Parse(userId)).ToListAsync();
            return tasks;
            }
        }

        public async Task<List<Expense>> LoadExpensesForUserWithSpecificDesc(string userId, string search, CancellationToken cancellationToken)
        {
            using (var context = new ApplicationDbContext())
            {
                var tasks = await context.Expenses.Where(e => e.UserId == Guid.Parse(userId) && e.Description.Contains(search)).ToListAsync();
                return tasks;
            }
        }

        public async Task<List<Expense>> LoadExpensesForUserFromCurrentMonth(string userId, CancellationToken cancellationToken)
        {
            using (var context = new ApplicationDbContext())
            {
                var tasks = await context.Expenses.Where(e => e.UserId == Guid.Parse(userId) && e.CreatedDate.Month == DateTime.Now.Month).ToListAsync();
                return tasks;
            }
        }
    }
}
