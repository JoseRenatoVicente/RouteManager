using Identity.API.Services;
using Identity.Domain.Entities.v1;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteManager.WebAPI.Core.Controllers;
using RouteManager.WebAPI.Core.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Domain.Commands.Users.Create;
using Identity.Domain.Commands.Users.Delete;
using Identity.Domain.Commands.Users.Update;

namespace Identity.API.Controllers;

[Route("api/v1/[controller]")]
public class UsersController : BaseController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService, IMediator mediator, INotifier notifier) : base(mediator, notifier)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUser()
    {
        return Ok(await _userService.GetUsersAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(string id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return user;
    }

    [Authorize(Roles = "Usuários")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(string id, UpdateUserCommand updateUserCommand)
    {
        if (id != updateUserCommand.Id)
        {
            return BadRequest();
        }
        return await CustomResponseAsync(updateUserCommand);
    }

    [Authorize(Roles = "Usuários")]
    [HttpPost]
    public async Task<ActionResult<User>> PostUser(CreateUserCommand createUserCommand)
    {
        return await CustomResponseAsync(createUserCommand);
    }

    [Authorize(Roles = "Usuários")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DisableUser([FromRoute] DeleteUserCommand deleteUserCommand)
    {
        return await CustomResponseAsync(deleteUserCommand);
    }
}