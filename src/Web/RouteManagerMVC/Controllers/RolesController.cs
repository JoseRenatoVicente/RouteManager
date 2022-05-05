using Microsoft.AspNetCore.Mvc;
using RouteManager.WebAPI.Core.Notifications;
using RouteManagerMVC.Controllers.Base;
using RouteManagerMVC.Models;
using RouteManagerMVC.Services;
using System.Linq;
using System.Threading.Tasks;

namespace RouteManagerMVC.Controllers;

public class RolesController : MvcBaseController
{
    private readonly IRoleService _roleService;
    public RolesController(IRoleService roleService, INotifier notifier) : base(notifier)
    {
        _roleService = roleService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _roleService.GetRolesAsync());
    }

    public async Task<IActionResult> Details(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var role = await _roleService.GetRoleByIdAsync(id);
        if (role == null)
        {
            return NotFound();
        }

        return View(role);
    }

    public async Task<IActionResult> Create()
    {
        return View(new RoleViewModel { GetClaims = await _roleService.GetCurrentClaimsAsync() });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("RoleRequest,NameClaims")] RoleViewModel role)
    {
        role.RoleRequest.Claims = role.NameClaims.Select(c => new ClaimViewModel(c));
        return await CustomResponseAsync(await _roleService.AddRoleAsync(role.RoleRequest));
    }

    public async Task<IActionResult> Edit(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var role = await _roleService.GetRoleByIdAsync(id);
        if (role == null)
        {
            return NotFound();
        }


        return View(new RoleViewModel { GetClaims = await _roleService.GetCurrentClaimsAsync(), RoleRequest = role });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("RoleRequest,NameClaims")] RoleViewModel role)
    {
        role.RoleRequest.Id = id;

        role.RoleRequest.Claims = role.NameClaims.Select(c => new ClaimViewModel(c));
        return await CustomResponseAsync(await _roleService.UpdateRoleAsync(role.RoleRequest));
    }

    public async Task<IActionResult> Delete(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var role = await _roleService.GetRoleByIdAsync(id);
        if (role == null)
        {
            return NotFound();
        }
        return View(role);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var role = await _roleService.GetRoleByIdAsync(id);

        await _roleService.RemoveRoleAsync(id);

        return await CustomResponseAsync(role);
    }
}