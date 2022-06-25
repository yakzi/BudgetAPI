using BudgetAPI.Controllers.Models;
using BudgetAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BudgetAPI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost]
        [Route("CreateUser", Name = nameof(CreateUser))]
        public async Task<ActionResult> CreateUser(CreateUserRequest createUserRequest, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userService.CreateUser(createUserRequest, cancellationToken);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        [Route("Login", Name = nameof(Login))]
        public async Task<ActionResult> Login(LoginUserRequest loginUserRequest, CancellationToken cancellationToken)
        {
            if(!string.IsNullOrEmpty(loginUserRequest.guid) && !string.IsNullOrEmpty(loginUserRequest.password))
            {
               var token = await userService.Login(loginUserRequest, cancellationToken);
               if (token != null)
                {
                    return Ok(token);
                }
            }
            return NotFound(loginUserRequest.guid);
        }
    }
}
