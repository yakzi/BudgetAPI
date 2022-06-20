using BudgetApi.Services;
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
        public async Task<ActionResult> CreateUser(string email, string password, string name, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userService.CreateUser(email, password, name, cancellationToken);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
