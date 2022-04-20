using Identity.API.Models;
using Identity.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteManager.Domain.Identity.Extensions;
using RouteManager.WebAPI.Core.Controllers;
using RouteManager.WebAPI.Core.Notifications;
using System.Threading.Tasks;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IAspNetUser _aspNetUser;
        private readonly AuthenticationService _authService;

        public AuthController(AuthenticationService authService, IAspNetUser aspNetUser, INotifier notifier) : base(notifier)
        {
            _authService = authService;
            _aspNetUser = aspNetUser;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRegister userRegister)
        {
            return await CustomResponseAsync(await _authService.CreateAsync(userRegister));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLogin usuarioLogin)
        {
            return await CustomResponseAsync(await _authService.LoginAsync(usuarioLogin));
        }

        [HttpPost("Checar-Login")]
        public ActionResult CheckLogin()
        {
            return Ok(_aspNetUser.GetUserId());
        }

    }
}
