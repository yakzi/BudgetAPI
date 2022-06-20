using BudgetAPI.Data.Configuration;
using BudgetAPI.Models;
using BudgetAPI.Repositories;

namespace BudgetAPI.Services
{

    public interface IUserService
    {
        Task<User> CreateUser(string email, string password, string name, CancellationToken cancellationToken);
    }
    internal class UserService : IUserService
    {

        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
        public async Task<User> CreateUser(string email, string password, string name, CancellationToken cancellationToken)
        {

            var User = new User(email, password, name);

            await userRepository.InsertNewUser(User, cancellationToken);

            return User;
        }
    }
}
