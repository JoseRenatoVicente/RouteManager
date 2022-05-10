using AutoMapper;
using Logging.API.Services;
using Logging.Domain.Commands.v1.CreateLogging;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteManager.WebAPI.Core.Controllers;
using RouteManager.WebAPI.Core.Notifications;
using System.Threading.Tasks;
using Logging.Infra.Data.Queries.v1.GetLoggingById;
using Logging.Infra.Data.Queries.v1.GetLoggings;

namespace Logging.API.Controllers.v1;

[Route("api/v1/[controller]")]
public class LogsController : BaseController
{
    private readonly IMapper _mapper;
    private readonly ILogService _logsService;

    public LogsController(ILogService logsService, IMediator mediator, INotifier notifier) : base(mediator, notifier)
    {
        _logsService = logsService;
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