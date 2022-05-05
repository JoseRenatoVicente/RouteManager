using Microsoft.AspNetCore.Mvc;
using RouteManager.WebAPI.Core.Notifications;
using RouteManagerMVC.Controllers.Base;
using RouteManagerMVC.Models;
using RouteManagerMVC.Services;
using System.Threading.Tasks;

namespace RouteManagerMVC.Controllers;

public class UsersController : MvcBaseController
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;

    public UsersController(IUserService userService, IRoleService roleService, INotifier notifier) : base(notifier)
    {
        _userService = userService;
        _roleService = roleService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _userService.GetUsersAsync());
    }

    public async Task<IActionResult> Details(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }
    public async Task<IActionResult> Create()
    {
        return View(new UserRegister { Roles = await _roleService.GetRolesAsync() });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,UserName,Email,Password,ConfirmPassword,Role")] UserRegister user)
    {
        user.Roles = await _roleService.GetRolesAsync();
        return await CustomResponseAsync(await _userService.AddUserAsync(user));
    }

    public async Task<IActionResult> Edit(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        user.Roles = await _roleService.GetRolesAsync();
        return View(user);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, UserViewModel user)
    {
        if (id != user.Id)
        {
            return NotFound();
        }
        user.Roles = await _roleService.GetRolesAsync();

        return await CustomResponseAsync(await _userService.UpdateUserAsync(user));
    }

    public async Task<IActionResult> Delete(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        await _userService.DisableUserAsync(user.Id);

        return await CustomResponseAsync(user);
    }
}