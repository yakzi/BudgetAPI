namespace BudgetAPI.Controllers.Models
{
    public record CreateExpenseRequest(decimal amount, string userId, string desc);
    public record GetExpensesForUserRequest(string userId);
    public record GetExpensesForUserWithSpecificDescRequest(string userId, string text);
}
