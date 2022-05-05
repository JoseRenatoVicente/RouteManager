using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteManager.WebAPI.Core.Notifications;
using RouteManagerMVC.Controllers.Base;
using RouteManagerMVC.Models;
using RouteManagerMVC.Services;
using System.Threading.Tasks;

namespace RouteManagerMVC.Controllers;

public class AuthController : MvcBaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService, INotifier notifier) : base(notifier)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(UserLogin userLogin)
    {
        var accessToken = await _authService.LoginAsync(userLogin);
        if (accessToken != null)
        {
            return RedirectToAction("Index", "ReportRoutes");
        }
        else
        {
            return await CustomResponseAsync(userLogin, "Login");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutAsync();
        return RedirectToAction("Login");
    }

    public IActionResult Login()
    {
        return View();
    }
}