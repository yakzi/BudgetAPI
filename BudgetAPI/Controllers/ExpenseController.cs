using BudgetAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BudgetAPI.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IExpenseService expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            this.expenseService = expenseService ?? throw new ArgumentNullException(nameof(expenseService));
        }

        [HttpPost]
        [Route("CreateExpense", Name = nameof(CreateExpense))]
        public async Task<ActionResult> CreateExpense(decimal amount, string userId, string desc, CancellationToken cancellationToken)
        {
            try
            {
                var expense = await expenseService.CreateExpense(amount, userId, desc, cancellationToken);
                return Ok(expense);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        [Route("GetExpensesForUser", Name = nameof(GetExpensesForUser))]
        public async Task<ActionResult> GetExpensesForUser(string userId, CancellationToken cancellationToken)
        {
            try
            {
                var expense = await expenseService.GetExpensesForUser(userId, cancellationToken);
                return Ok(expense);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
