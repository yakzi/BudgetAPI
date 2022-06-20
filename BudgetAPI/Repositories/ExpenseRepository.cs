using BudgetAPI.Data.Configuration;
using BudgetAPI.Models;

namespace BudgetAPI.Repositories
{
    public interface IExpenseRepository
    {
        Task InsertNewExpense(Expense expense, CancellationToken cancellationToken);
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
    }
}
