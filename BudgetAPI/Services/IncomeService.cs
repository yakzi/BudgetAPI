using BudgetAPI.Data.Configuration;
using BudgetAPI.Models;
using BudgetAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace BudgetAPI.Services
{

    public interface IIncomeService
    {
        Task<ActionResult<Income>> CreateIncome(decimal amount, string desc, string token, CancellationToken cancellationToken);
        Task<List<Income>> GetIncomesForUser(string userId, CancellationToken cancellationToken);

    }
    internal class IncomeService : IIncomeService
    {
        private readonly IIncomeRepository incomeRepository;
        private readonly IUserService userService;

        public IncomeService(IIncomeRepository incomeRepository)
        {
            this.incomeRepository = incomeRepository ?? throw new ArgumentNullException(nameof(incomeRepository));
        }
        public async Task<ActionResult<Income>> CreateIncome(decimal amount, string desc, string token, CancellationToken cancellationToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var x = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
            var isUser = await userService.GetUserTry(x, cancellationToken);
            if (isUser)
            {
                var Income = new Income(amount, Guid.Parse(x), desc);
                await incomeRepository.InsertNewIncome(Income, cancellationToken);
                return Income;
            }
            else return new EmptyResult();
        }

        public async Task<List<Income>> GetIncomesForUser(string userId, CancellationToken cancellationToken)
        {
            return await incomeRepository.LoadIncomesForUser(userId, cancellationToken);
        }
    }
}