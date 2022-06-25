using BudgetAPI.Controllers.Models;
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
        public async Task<ActionResult> CreateIncome(CreateIncomeRequest createIncomeRequest, CancellationToken cancellationToken)
        {
            try
            {
                var expense = await incomeService.CreateIncome(createIncomeRequest, cancellationToken);
                if(expense.Result == null)
                {
                    return Ok(expense);
                }
               else return expense.Result;
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        [Route("GetIncomesForUser", Name = nameof(GetIncomesForUser))]
        public async Task<ActionResult> GetIncomesForUser(GetIncomesForUserRequest getIncomesForUserRequest, CancellationToken cancellationToken)
        {
            try
            {
                var expense = await incomeService.GetIncomesForUser(getIncomesForUserRequest, cancellationToken);
                return Ok(expense);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
