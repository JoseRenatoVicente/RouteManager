using Identity.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteManager.Domain.Entities.Identity;
using RouteManager.WebAPI.Core.Controllers;
using RouteManager.WebAPI.Core.Notifications;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        public AccountController(INotifier notifier) : base(notifier)
        {
        }

        /*    private readonly SignInManager<Usuario> _signInManager;
   private readonly UserManager<Usuario> _userManager;
   private readonly ILogger _logger;

   private readonly IEmailService _emailService;

   public ContaController(SignInManager<Usuario> signInManager,
                         IEmailService emailService,
   UserManager<Usuario> userManager, ILogger<AutenticacaoController> logger)
   {
       _signInManager = signInManager;
       _userManager = userManager;
       _logger = logger;
       _emailService = emailService;
   }

        
        [HttpGet]
        public async Task<IActionResult> ObterDados()
        {
            var data = await (await _pessoaRepository.GetAllAsync())
            .ProjectTo<PessoaUpdateViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.id_usuario == _user.ObterUserId());

            return data is null ? NotFound() : CustomResponseAsync(data);
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarDados(AtualizarPessoaCommand atualizarPessoaCommand)
        {
            atualizarPessoaCommand.UsuarioId = _user.ObterUserId();
            return !ModelState.IsValid ? CustomResponseAsync(ModelState) : CustomResponseAsync(await _mediator.Send(atualizarPessoaCommand));
        }

        /// <summary>
        /// Requisição usada para mudar a senha de um usuário que esta logado
        /// </summary>
        /// <param name="mudarSenha">É necessário enviar a senha atual e a nova senha</param>
        /// <returns></returns>
        [HttpPost("MudarSenha")]
        public async Task<ActionResult> MudarSenha(MudarSenhaViewModel mudarSenha)
        {
            if (!ModelState.IsValid) return CustomResponseAsync(ModelState);
            var user = await _userManager.FindByIdAsync(_user.ObterUserId().ToString());
            var result = await _userManager.ChangePasswordAsync(user, mudarSenha.SenhaAtual, mudarSenha.NovaSenha);

            if (result.Succeeded)
            {
                await _emailService.SendEmailAsync(user.Email, "Sua senha foi alterada", "proteja sua conta");
                return CustomResponseAsync();
            }
            foreach (var error in result.Errors)
            {
                AddError(error.Description);
            }

            return CustomResponseAsync();

        }

        /// <summary>
        /// Requisição usada para recuperação da conta
        /// </summary>
        /// <param name="email">É necessário enviar o email da conta</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("EsqueceuSenha")]
        public async Task<ActionResult> EsqueceuSenha(string email)
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
        public async Task<ActionResult> ResetSenha(ResetPasswordViewModel resetPassword)
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
        public async Task<ActionResult> ConfirmarEmail([Required] string userId, [Required] string code)
        {
            if (!ModelState.IsValid) return await CustomResponseAsync(ModelState);

            var result = await _userManager.ConfirmEmailAsync(new User { Id = userId }, code);

            if (!result.Succeeded) await AddError("Link expirado");

            return await CustomResponseAsync();
        }

        */



    }
}
