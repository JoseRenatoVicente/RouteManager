using Logging.API.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteManager.Domain.Core.Models;
using RouteManager.WebAPI.Core.Controllers;
using RouteManager.WebAPI.Core.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;
using Logging.Domain.Commands.Create;
using Logging.Domain.Entities.v1;

namespace Logging.API.Controllers;

[Route("api/v1/[controller]")]
public class LogsController : BaseController
{
    private readonly ILogService _logsService;

    public LogsController(ILogService logsService, IMediator mediator, INotifier notifier) : base(mediator, notifier)
    {
        _logsService = logsService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Log>>> GetLog()
    {
        return Ok(await _logsService.GetLogsAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLog(string id)
    {
        var log = await _logsService.GetLogByIdAsync(id);

        if (log == null)
        {
            return NotFound();
        }

        return Ok(log);
    }

    [Authorize(Roles = "Logs")]
    [HttpPost]
    public async Task<ActionResult<LogRequest>> PostLog(CreateLogCommand createLogCommand)
    {
        return await CustomResponseAsync(createLogCommand);
    }
}