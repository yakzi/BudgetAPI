using BudgetAPI.Data.Configuration;
using BudgetAPI.Models;
using BudgetAPI.Repositories;

namespace BudgetAPI.Services
{

    public interface IExpenseService
    {
        Task<Expense> CreateExpense(decimal amount, string userId, string desc, CancellationToken cancellationToken);
        Task<List<Expense>> GetExpensesForUser(string userId, CancellationToken cancellationToken);
        Task<List<Expense>> GetExpensesForUserWithSpecificDesc(string userId, string search, CancellationToken cancellationToken);
        Task<List<Expense>> GetExpensesForUserFromCurrentMonth(string userId, CancellationToken cancellationToken);
    }
    internal class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository expenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository)
        {
            this.expenseRepository = expenseRepository ?? throw new ArgumentNullException(nameof(expenseRepository));
        }
        public async Task<Expense> CreateExpense(decimal amount, string userId, string desc, CancellationToken cancellationToken)
        {
            var Expense = new Expense(amount, Guid.Parse(userId), desc);
            await expenseRepository.InsertNewExpense(Expense, cancellationToken);
            return Expense;
        }

        public async Task<List<Expense>> GetExpensesForUser(string userId, CancellationToken cancellationToken)
        {
            return await expenseRepository.LoadExpensesForUser(userId, cancellationToken);
        }

        public async Task<List<Expense>> GetExpensesForUserWithSpecificDesc(string userId, string search, CancellationToken cancellationToken)
        {
            return await expenseRepository.LoadExpensesForUserWithSpecificDesc(userId, search, cancellationToken);
        }

        public async Task<List<Expense>> GetExpensesForUserFromCurrentMonth(string userId, CancellationToken cancellationToken)
        {
            return await expenseRepository.LoadExpensesForUserFromCurrentMonth(userId, cancellationToken);
        }
    }
}
