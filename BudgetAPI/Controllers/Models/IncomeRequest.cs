namespace BudgetAPI.Controllers.Models
{
    public record CreateIncomeRequest(decimal amount, string desc, string token);
    public record GetIncomesForUserRequest(string userId);
}
