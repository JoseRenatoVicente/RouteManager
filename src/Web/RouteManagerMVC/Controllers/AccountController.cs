using Microsoft.AspNetCore.Mvc;
using RouteManager.WebAPI.Core.Notifications;
using RouteManagerMVC.Controllers.Base;
using RouteManagerMVC.Models;
using RouteManagerMVC.Services;
using System.Threading.Tasks;

namespace RouteManagerMVC.Controllers;

public class AccountController : MvcBaseController
{
    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService, INotifier notifier) : base(notifier)
    {
        _accountService = accountService;
    }

    public async Task<IActionResult> Profile()
    {
        return View(await _accountService.GetCurrentUser());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Profile([Bind("Name,UserName,Email")] UserUpdate user)
    {
        return await CustomResponseAsync(await _accountService.UpdateCurrentUserAsync(user), "Profile");
    }

    public IActionResult ChangePassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePassword)
    {
        return await CustomResponseAsync(await _accountService.ChangePasswordAsync(changePassword), "ChangePassword");
    }

}