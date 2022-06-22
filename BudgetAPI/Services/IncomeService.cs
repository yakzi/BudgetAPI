using BudgetAPI.Data.Configuration;
using BudgetAPI.Models;
using BudgetAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

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

        public IncomeService(IIncomeRepository incomeRepository, IUserService userService)
        {
            this.incomeRepository = incomeRepository ?? throw new ArgumentNullException(nameof(incomeRepository));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }
        public async Task<ActionResult<Income>> CreateIncome(decimal amount, string desc, string token, CancellationToken cancellationToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = new JwtSecurityToken();
            string? x = null;
            SecurityToken validatedToken;
            if (handler.CanReadToken(token))
            {
                try
                {
                    jwtSecurityToken = handler.ReadJwtToken(token);
                    x = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "name").Value;
                }
                catch 
                { 
                    return new BadRequestResult(); 
                }

                if (x is not null)
                {
                    if(await userService.GetUserTry(x, cancellationToken))
                    {
                        var Income = new Income(amount, Guid.Parse(x), desc);
                        await incomeRepository.InsertNewIncome(Income, cancellationToken);
                        return Income;
                    }
                }
                return new EmptyResult();
            }
            
            else return new BadRequestResult();
        }

        public async Task<List<Income>> GetIncomesForUser(string userId, CancellationToken cancellationToken)
        {
            return await incomeRepository.LoadIncomesForUser(userId, cancellationToken);
        }
    }
}