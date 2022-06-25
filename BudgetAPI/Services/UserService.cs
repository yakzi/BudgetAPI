using BudgetAPI.Controllers.Models;
using BudgetAPI.Data.Configuration;
using BudgetAPI.Models;
using BudgetAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BudgetAPI.Services
{

    public interface IUserService
    {
        Task<User> CreateUser(CreateUserRequest createUserRequest, CancellationToken cancellationToken);
        Task<User?> LoadUser(LoginUserRequest loginUserRequest, CancellationToken cancellationToken);
        Task<string> Login(LoginUserRequest loginUserRequest, CancellationToken cancellationToken);
        Task<bool> TryGetUser(string guid, CancellationToken cancellationToken);
    }
    internal class UserService : IUserService
    {

        private readonly IUserRepository userRepository;
        private readonly IConfiguration configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public async Task<User> CreateUser(CreateUserRequest createUserRequest, CancellationToken cancellationToken)
        {

            var User = new User(createUserRequest.email, createUserRequest.password, createUserRequest.name);

            await userRepository.InsertNewUser(User, cancellationToken);

            return User;
        }
        public async Task<User?> LoadUser(LoginUserRequest loginUserRequest, CancellationToken cancellationToken)
        {
           return await userRepository.LoadUser(loginUserRequest.guid, loginUserRequest.password, cancellationToken);
        }

        public async Task<string> Login(LoginUserRequest loginUserRequest, CancellationToken cancellationToken)
        {
            var currUser = await userRepository.LoadUser(loginUserRequest.guid, loginUserRequest.password, cancellationToken);
            var token = CreateToken(currUser);
            return token;
        }

        public async Task<bool> TryGetUser(string guid, CancellationToken cancellationToken)
        {
            return await userRepository.CheckUser(guid, cancellationToken);
        }

        private string CreateToken(User? user)
        {
            if (user != null)
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim("name", user.Id.ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("SecKey").Value));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(365),
                    signingCredentials: creds);

                var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                return jwt;
            }
            else return string.Empty;
        }
    }
}
