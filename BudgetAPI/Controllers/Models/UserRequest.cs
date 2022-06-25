namespace BudgetAPI.Controllers.Models
{
    public record CreateUserRequest(string email, string password, string name);
    public record LoginUserRequest(string guid, string password);
}
