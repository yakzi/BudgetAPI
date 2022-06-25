using BudgetAPI.Controllers.Models;
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
        public async Task<ActionResult> CreateExpense(CreateExpenseRequest createExpenseRequest, CancellationToken cancellationToken)
        {
            try
            {
                var expense = await expenseService.CreateExpense(createExpenseRequest, cancellationToken);
                return Ok(expense);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        [Route("GetExpensesForUser", Name = nameof(GetExpensesForUser))]
        public async Task<ActionResult> GetExpensesForUser(GetExpensesForUserRequest getExpensesForUserRequest, CancellationToken cancellationToken)
        {
            try
            {
                var expense = await expenseService.GetExpensesForUser(getExpensesForUserRequest, cancellationToken);
                return Ok(expense);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        [Route("GetExpensesForUserWithSpecificDesc", Name = nameof(GetExpensesForUserWithSpecificDesc))]
        public async Task<ActionResult> GetExpensesForUserWithSpecificDesc(GetExpensesForUserWithSpecificDescRequest getExpensesForUserWithSpecificDescRequest, CancellationToken cancellationToken)
        {
            try
            {
                var expense = await expenseService.GetExpensesForUserWithSpecificDesc(getExpensesForUserWithSpecificDescRequest, cancellationToken);
                return Ok(expense);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        [Route("GetExpensesForUserFromCurrentMonth", Name = nameof(GetExpensesForUserFromCurrentMonth))]
        public async Task<ActionResult> GetExpensesForUserFromCurrentMonth(GetExpensesForUserRequest getExpensesForUserRequest, CancellationToken cancellationToken)
        {
            try
            {
                var expense = await expenseService.GetExpensesForUserFromCurrentMonth(getExpensesForUserRequest, cancellationToken);
                return Ok(expense);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
