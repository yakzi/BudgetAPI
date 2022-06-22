using BudgetAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BudgetAPI.Controllers
{
    public class IncomeController : Controller
    {
        private readonly IIncomeService incomeService;

        public IncomeController(IIncomeService incomeService)
        {
            this.incomeService = incomeService ?? throw new ArgumentNullException(nameof(incomeService));
        }

        [HttpPost]
        [Route("CreateIncome", Name = nameof(CreateIncome))]
        public async Task<ActionResult> CreateIncome(decimal amount, string desc, string token, CancellationToken cancellationToken)
        {
            try
            {
                var expense = await incomeService.CreateIncome(amount, desc, token, cancellationToken);
                return Ok(expense);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        [Route("GetIncomesForUser", Name = nameof(GetIncomesForUser))]
        public async Task<ActionResult> GetIncomesForUser(string userId, CancellationToken cancellationToken)
        {
            try
            {
                var expense = await incomeService.GetIncomesForUser(userId, cancellationToken);
                return Ok(expense);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
