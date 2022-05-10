using MediatR;
using Microsoft.AspNetCore.Mvc;
using RouteManager.WebAPI.Core.Controllers;
using RouteManager.WebAPI.Core.Notifications;
using System;
using System.Threading.Tasks;

namespace Identity.API.Controllers.v1;

[Route("api/v1/[controller]")]
public class OperationsController : BaseController
{
    public OperationsController(IMediator mediator, INotifier notifier) : base(mediator, notifier)
    {
    }

    [HttpGet]
    [Route("ForceException")]
    public IActionResult ForceException()
    {
        _ = 1 / Convert.ToInt32("Teste");
        return BadRequest();
    }

    [HttpGet("Ping")]
    public IActionResult Ping()
    {
        var result = new
        {
            ServerNow = DateTime.Now,
            ServerToday = DateTime.Today
        };
        return Ok(result);
    }

    [HttpPost("EmailTest")]
    public async Task<IActionResult> EnviarEmailTest(string nome, string email)
    {
        try
        {
            //await _emailService.Test(email, nome);
            return await CustomResponseAsync("Email enviado com sucesso");
        }
        catch (Exception e)
        {
            await AddError("erro ao enviar email, tente novamente mais tarde " + e);
            return await CustomResponseAsync();
        }

    }
}