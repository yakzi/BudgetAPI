using BudgetAPI.Data.Configuration;
using BudgetAPI.Models;

namespace BudgetAPI.Services
{

    public interface IExpenseService
    {
        Task<Expense> CreateExpense(decimal amount, string userId, string desc, CancellationToken cancellationToken);
        //Task<List<Expense>> GetExpensesForUser(string userId, CancellationToken cancellationToken);

    }
    internal class ExpenseService : IExpenseService
    {
        public async Task<Expense> CreateExpense(decimal amount, string userId, string desc, CancellationToken cancellationToken)
        {
            var Expense = new Expense(amount, Guid.Parse(userId), desc);
            using (var context = new ApplicationDbContext())
            {
               await context.AddAsync(Expense, cancellationToken);
               await context.SaveChangesAsync();
            }
            return Expense;
        }
    }
}
