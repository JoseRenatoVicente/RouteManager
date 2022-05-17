using Identity.API.Models;
using Identity.API.Services;
using Identity.Domain.Entities.v1;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RouteManager.Domain.Core.Identity.Extensions;
using RouteManager.WebAPI.Core.Controllers;
using RouteManager.WebAPI.Core.Notifications;
using System.Threading.Tasks;

namespace Identity.API.Controllers.v1;

[Route("api/v1/[controller]")]
public class AccountController : BaseController
{
    private readonly IUserService _userService;
    private readonly IAspNetUser _aspNetUser;

    public AccountController(IUserService userService, IAspNetUser aspNetUser, IMediator mediator, INotifier notifier) : base(mediator, notifier)
    {
        _userService = userService;
        _aspNetUser = aspNetUser;
    }

    [HttpGet]
    public async Task<IActionResult> GetCurrentUser()
    {
        return await CustomResponseAsync(await _userService.GetUserByIdAsync(_aspNetUser.GetUserId()));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCurrentUser(User user)
    {
        user.Id = _aspNetUser.GetUserId();
        return await CustomResponseAsync(await _userService.UpdateUserAsync(user));
    }

    /// <summary>
    /// Requisição usada para mudar a senha de um usuário que esta logado
    /// </summary>
    /// <param name="changePassword">É necessário enviar a senha atual e a nova senha</param>
    /// <returns></returns>
    [HttpPost("ChangePassword")]
    public async Task<IActionResult> ChangePassword(ChangePasswordCurrentUserViewModel changePassword)
    {
        changePassword.UserId = _aspNetUser.GetUserId();
        return await CustomResponseAsync(await _userService.ChangePasswordCurrentUser(changePassword));
    }
    

}