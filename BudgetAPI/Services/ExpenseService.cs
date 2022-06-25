using BudgetAPI.Controllers.Models;
using BudgetAPI.Data.Configuration;
using BudgetAPI.Models;
using BudgetAPI.Repositories;

namespace BudgetAPI.Services
{

    public interface IExpenseService
    {
        Task<Expense> CreateExpense(CreateExpenseRequest createExpenseRequest, CancellationToken cancellationToken);
        Task<List<Expense>> GetExpensesForUser(GetExpensesForUserRequest getExpensesForUserRequest, CancellationToken cancellationToken);
        Task<List<Expense>> GetExpensesForUserWithSpecificDesc(GetExpensesForUserWithSpecificDescRequest getExpensesForUserWithSpecificDescRequest, CancellationToken cancellationToken);
        Task<List<Expense>> GetExpensesForUserFromCurrentMonth(GetExpensesForUserRequest getExpensesForUserRequest, CancellationToken cancellationToken);
    }
    internal class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository expenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository)
        {
            this.expenseRepository = expenseRepository ?? throw new ArgumentNullException(nameof(expenseRepository));
        }
        public async Task<Expense> CreateExpense(CreateExpenseRequest createExpenseRequest, CancellationToken cancellationToken)
        {
            var Expense = new Expense(createExpenseRequest.amount, Guid.Parse(createExpenseRequest.userId), createExpenseRequest.desc);
            await expenseRepository.InsertNewExpense(Expense, cancellationToken);
            return Expense;
        }

        public async Task<List<Expense>> GetExpensesForUser(GetExpensesForUserRequest getExpensesForUserRequest, CancellationToken cancellationToken)
        {
            return await expenseRepository.LoadExpensesForUser(getExpensesForUserRequest.userId, cancellationToken);
        }

        public async Task<List<Expense>> GetExpensesForUserWithSpecificDesc(GetExpensesForUserWithSpecificDescRequest getExpensesForUserWithSpecificDescRequest, CancellationToken cancellationToken)
        {
            return await expenseRepository.LoadExpensesForUserWithSpecificDesc(getExpensesForUserWithSpecificDescRequest.userId, getExpensesForUserWithSpecificDescRequest.text, cancellationToken);
        }

        public async Task<List<Expense>> GetExpensesForUserFromCurrentMonth(GetExpensesForUserRequest getExpensesForUserRequest, CancellationToken cancellationToken)
        {
            return await expenseRepository.LoadExpensesForUserFromCurrentMonth(getExpensesForUserRequest.userId, cancellationToken);
        }
    }
}
