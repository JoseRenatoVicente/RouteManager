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
    private readonly IMediator _mediator;

    private readonly ICollection<string> _errors = new List<string>();
    private readonly INotifier _notifier;
    protected BaseController(IMediator mediator, INotifier notifier)
    {
        _mediator = mediator;
        _notifier = notifier;
    }

    protected async Task<IActionResult> CustomResponseAsync(object result = null)
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

    protected async Task<IActionResult> CustomResponseAsync<TRequest>(IRequest<TRequest> request)
    {
        var response = await ExecuteAsync(request);
        response.Errors = await GetErrors();

        if (await IsOperationValid())
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    private async Task<Response> ExecuteAsync<TRequest>(IRequest<TRequest> request)
    {
        return await _mediator.Send(request) as Response ?? new Response();
    }


    private Task<bool> IsOperationValid()
    {
        return Task.Run(() => !_notifier.IsNotified());
    }

    private async Task<IEnumerable<string>> GetErrors()
    {
        foreach (var item in _notifier.GetNotifications())
        {
            await AddError(item);
        }
        _notifier.Clear();
        return _errors;
    }
    protected Task AddError(string error)
    {
        return Task.Run(() => _errors.Add(error));
    }
}