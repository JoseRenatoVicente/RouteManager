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

    /*
    /// <summary>
    /// Requisição usada para recuperação da conta
    /// </summary>
    /// <param name="email">É necessário enviar o email da conta</param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("EsqueceuSenha")]
    public async Task<IActionResult> EsqueceuSenha(string email)
    {
        if (!ModelState.IsValid) return await CustomResponseAsync(ModelState);
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            // Não revelar se o usuario nao existe ou se nao esta confirmado
            await AddError("O Usuário já tem um email enviado para confirmação");
            return await CustomResponseAsync();
        }

        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
        var callbackUrl = "https://www.domain.com/Account/reset-senha?userId=" + user.Id + "&token=" + code;

        await _emailService.SendEmailAsync(user.Email, "Esqueci minha senha", "Por favor altere sua senha clicando aqui: " + callbackUrl);
        return Ok();


    }

    /// <summary>
    /// Requisição usada para trocar a senha após receber um link de reset de senha
    /// </summary>
    /// <param name="resetPassword">É necessário enviar o userId, code e uma nova senha</param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("ResetSenha")]
    public async Task<IActionResult> ResetSenha(ResetPasswordViewModel resetPassword)
    {
        if (!ModelState.IsValid) return await CustomResponseAsync(ModelState);

        var user = await _userManager.FindByIdAsync(resetPassword.UserId);
        var result = await _userManager.ResetPasswordAsync(user, resetPassword.Token.Replace(" ", "+"), resetPassword.Password);
        if (result.Succeeded)
        {
            return Ok();
        }
        foreach (var error in result.Errors)
        {
            AddError(error.Description);
        }

        return await CustomResponseAsync();

    }



    /// <summary>
    /// Requisição usada para confirmar o email
    /// </summary>
    /// <param name="userId">É necessário enviar o userId e code </param>
    /// <param name="code">É necessário enviar o userId e code </param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("ConfirmarEmail")]
    public async Task<IActionResult> ConfirmarEmail([Required] string userId, [Required] string code)
    {
        if (!ModelState.IsValid) return await CustomResponseAsync(ModelState);

        var result = await _userManager.ConfirmEmailAsync(new UserId { Id = userId }, code);

        if (!result.Succeeded) await AddError("Link expirado");

        return await CustomResponseAsync();
    }
    */


}