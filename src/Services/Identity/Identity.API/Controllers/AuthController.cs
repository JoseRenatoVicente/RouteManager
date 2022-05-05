using Identity.API.Models;
using Identity.API.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteManager.Domain.Core.Identity.Extensions;
using RouteManager.WebAPI.Core.Controllers;
using RouteManager.WebAPI.Core.Notifications;
using System.Threading.Tasks;

namespace Identity.API.Controllers;

[Route("api/v1/[controller]")]
public class AuthController : BaseController
{
    private readonly IAspNetUser _aspNetUser;
    private readonly AuthService _authService;

    public AuthController(AuthService authService, IAspNetUser aspNetUser, IMediator mediator, INotifier notifier) : base(mediator, notifier)
    {
        _authService = authService;
        _aspNetUser = aspNetUser;
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login(UserLogin usuarioLogin)
    {
        return await CustomResponseAsync(await _authService.LoginAsync(usuarioLogin));
    }

    [HttpPost("Check-Login")]
    public ActionResult CheckLogin()
    {
        return Ok(_aspNetUser.GetUserId());
    }

}