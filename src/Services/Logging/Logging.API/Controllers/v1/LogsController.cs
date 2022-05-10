using Logging.API.Services;
using Logging.Domain.Entities.v1;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteManager.WebAPI.Core.Controllers;
using RouteManager.WebAPI.Core.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;
using Logging.Domain.Commands.v1.CreateLogging;

namespace Logging.API.Controllers.v1;

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
    public async Task<IActionResult> PostLog(CreateLogCommand createLogCommand)
    {
        return await CustomResponseAsync(createLogCommand);
    }
}