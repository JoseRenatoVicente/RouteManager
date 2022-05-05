using Identity.API.Services;
using Identity.Domain.Entities.v1;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteManager.WebAPI.Core.Controllers;
using RouteManager.WebAPI.Core.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Domain.Commands.Roles.Create;
using Identity.Domain.Commands.Roles.Delete;
using Identity.Domain.Commands.Roles.Update;

namespace Identity.API.Controllers;

[Route("api/v1/[controller]")]
public class RolesController : BaseController
{
    private readonly IRoleService _roleService;
    public RolesController(IRoleService roleService, IMediator mediator, INotifier notifier) : base(mediator, notifier)
    {
        _roleService = roleService;
    }

    [HttpGet("Claims")]
    public async Task<ActionResult<IEnumerable<Role>>> GetCurrentClaims()
    {
        return Ok(await _roleService.GetCurrentClaimsAsync());
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Role>>> GetRole()
    {
        return Ok(await _roleService.GetRolesAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Role>> GetRole(string id)
    {
        var role = await _roleService.GetRoleByIdAsync(id);

        if (role == null)
        {
            return NotFound();
        }

        return role;
    }

    [Authorize(Roles = "Funções")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRole(string id, UpdateRoleCommand roleCommand)
    {
        if (id != roleCommand.Id)
        {
            return BadRequest();
        }

        return await CustomResponseAsync(roleCommand);
    }

    [Authorize(Roles = "Funções")]
    [HttpPost]
    public async Task<ActionResult<Role>> PostRole(CreateRoleCommand createRoleCommand)
    {
        return await CustomResponseAsync(createRoleCommand);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole([FromRoute] DeleteRoleCommand deleteRoleCommand)
    {
        return await CustomResponseAsync(deleteRoleCommand);
    }
}