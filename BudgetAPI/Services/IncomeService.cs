using BudgetAPI.Data.Configuration;
using BudgetAPI.Models;
using BudgetAPI.Repositories;

namespace BudgetAPI.Services
{

    public interface IIncomeService
    {
        Task<Income> CreateIncome(decimal amount, string userId, string desc, CancellationToken cancellationToken);
        Task<List<Income>> GetIncomesForUser(string userId, CancellationToken cancellationToken);

    }
    internal class IncomeService : IIncomeService
    {
        private readonly IIncomeRepository incomeRepository;

        public IncomeService(IIncomeRepository incomeRepository)
        {
            this.incomeRepository = incomeRepository ?? throw new ArgumentNullException(nameof(incomeRepository));
        }
        public async Task<Income> CreateIncome(decimal amount, string userId, string desc, CancellationToken cancellationToken)
        {
            var Income = new Income(amount, Guid.Parse(userId), desc);
            await incomeRepository.InsertNewIncome(Income, cancellationToken);
            return Income;
        }

        public async Task<List<Income>> GetIncomesForUser(string userId, CancellationToken cancellationToken)
        {
            return await incomeRepository.LoadIncomesForUser(userId, cancellationToken);
        }
    }
}
