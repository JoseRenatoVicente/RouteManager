using Identity.API.Services;
using Identity.Domain.Commands.v1.Roles.Create;
using Identity.Domain.Commands.v1.Roles.Delete;
using Identity.Domain.Commands.v1.Roles.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteManager.WebAPI.Core.Controllers;
using RouteManager.WebAPI.Core.Notifications;
using System.Threading.Tasks;

namespace Identity.API.Controllers.v1;

[Route("api/v1/[controller]")]
public class RolesController : BaseController
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService, IMediator mediator, INotifier notifier) : base(mediator, notifier)
    {
        _roleService = roleService;
    }

    [HttpGet("Claims")]
    public async Task<IActionResult> GetCurrentClaims()
    {
        return Ok(await _roleService.GetCurrentClaimsAsync());
    }

    [HttpGet]
    public async Task<IActionResult> GetRole()
    {
        return Ok(await _roleService.GetRolesAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRole(string id)
    {
        var role = await _roleService.GetRoleByIdAsync(id);

        if (role == null) return NotFound();

        return Ok(role);
    }

    [Authorize(Roles = "Funções")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRole(string id, UpdateRoleCommand roleCommand)
    {
        if (id != roleCommand.Id) return BadRequest();

        return await CustomResponseAsync(roleCommand);
    }

    [Authorize(Roles = "Funções")]
    [HttpPost]
    public async Task<IActionResult> PostRole(CreateRoleCommand createRoleCommand)
    {
        return await CustomResponseAsync(createRoleCommand);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole([FromRoute] DeleteRoleCommand deleteRoleCommand)
    {
        return await CustomResponseAsync(deleteRoleCommand);
    }
}