using MediatR;
using Microsoft.AspNetCore.Mvc;
using RouteManager.WebAPI.Core.Configuration;
using RouteManager.WebAPI.Core.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RouteManager.WebAPI.Core.Controllers;

[SecurityHeaders]
[ApiController]
public abstract class BaseController : ControllerBase
{

    protected readonly IMediator Mediator;

    protected readonly ICollection<string> Errors = new List<string>();
    protected readonly INotifier Notifier;
    protected BaseController(IMediator mediator, INotifier notifier)
    {
        Mediator = mediator;
        Notifier = notifier;
    }

    protected async Task<ActionResult> CustomResponseAsync(object result = null)
    {
        if (await IsOperationValid())
        {
            return Ok(result);
        }

        return BadRequest(new
        {
            success = false,
            errors = await GetErrors(),
        });
    }

    protected async Task<ActionResult> CustomResponseAsync<TRequest>(IRequest<TRequest> request)
    {
        var response = await ExecuteAsync(request);
        response.Errors = await GetErrors();

        if (await IsOperationValid())
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    protected async Task<Response> ExecuteAsync<TRequest>(IRequest<TRequest> request)
    {
        return await Mediator.Send(request) as Response ?? new Response();
    }


    protected Task<bool> IsOperationValid()
    {
        return Task.Run(() => !Notifier.IsNotified());
    }

    protected async Task<IEnumerable<string>> GetErrors()
    {
        foreach (var item in Notifier.GetNotifications())
        {
            await AddError(item);
        }
        Notifier.Clear();
        return Errors;
    }
    protected Task AddError(string error)
    {
        return Task.Run(() => Errors.Add(error));
    }
}