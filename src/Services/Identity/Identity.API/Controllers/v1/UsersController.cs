using Identity.API.Services;
using Identity.Domain.Commands.v1.Users.Create;
using Identity.Domain.Commands.v1.Users.Delete;
using Identity.Domain.Commands.v1.Users.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteManager.WebAPI.Core.Controllers;
using RouteManager.WebAPI.Core.Notifications;
using System.Threading.Tasks;

namespace Identity.API.Controllers.v1;

[Route("api/v1/[controller]")]
public class UsersController : BaseController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService, IMediator mediator, INotifier notifier) : base(mediator, notifier)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUser()
    {
        return await CustomResponseAsync(await _userService.GetUsersAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return await CustomResponseAsync(user);
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
    public async Task<IActionResult> PostUser(CreateUserCommand createUserCommand)
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