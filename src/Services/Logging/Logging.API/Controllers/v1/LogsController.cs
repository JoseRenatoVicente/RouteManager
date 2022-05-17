using Logging.Domain.Commands.v1.CreateLogging;
using Logging.Infra.Data.Queries.v1.GetLoggingById;
using Logging.Infra.Data.Queries.v1.GetLoggings;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteManager.WebAPI.Core.Controllers;
using RouteManager.WebAPI.Core.Notifications;
using System.Threading.Tasks;

namespace Logging.API.Controllers.v1;

[Route("api/v1/[controller]")]
public class LogsController : BaseController
{
    public LogsController(IMediator mediator, INotifier notifier) : base(mediator, notifier)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetLog()
    {
        return await CustomResponseAsync(new GetLoggingsQuery());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLog([FromRoute] GetLoggingByIdQuery getLoggingByIdQuery)
    {
        return await CustomResponseAsync(getLoggingByIdQuery);
    }

    [Authorize(Roles = "Logs")]
    [HttpPost]
    public async Task<IActionResult> PostLog(CreateLogCommand createLogCommand)
    {
        return await CustomResponseAsync(createLogCommand);
    }
}