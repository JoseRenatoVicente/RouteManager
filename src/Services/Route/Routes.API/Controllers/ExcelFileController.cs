using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RouteManager.WebAPI.Core.Controllers;
using RouteManager.WebAPI.Core.Notifications;
using Routes.API.DTO;
using Routes.API.Services;
using Routes.Domain.Entities.v1;
using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Domain.Commands.Roles.Delete;
using Routes.Domain.Commands.ExcelFiles.Report;
using Routes.Domain.Commands.ExcelFiles.Upload;

namespace Routes.API.Controllers;

[Route("api/v1/[controller]")]
public class ExcelFileController : BaseController
{
    private readonly IExcelFileService _excelFileService;

    public ExcelFileController(IExcelFileService excelFileService, IMediator mediator, INotifier notifier) : base(mediator, notifier)
    {
        _excelFileService = excelFileService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExcelFile>>> GetRoute()
    {
        return Ok(await _excelFileService.GetExcelFilesAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ExcelFile>> GetRoute(string id)
    {
        var excelFile = await _excelFileService.GetExcelFileByIdAsync(id);

        if (excelFile == null)
        {
            return NotFound();
        }

        return excelFile;
    }


    [Authorize(Roles = "Relátorio Rotas")]
    [HttpPost]
    public async Task<ActionResult<ExcelFile>> UploadExcelFileAsync([FromForm] UploadExcelFileCommand excelFileCommand)
    {
        return await CustomResponseAsync(excelFileCommand);
    }

    [Authorize(Roles = "Relátorio Rotas")]
    [HttpPost("report")]
    public async Task<ActionResult<byte[]>> ReportRoute(ReportRouteCommand reportRouteCommand)
    {
        return await CustomResponseAsync(reportRouteCommand);
    }

    [Authorize(Roles = "Relátorio Rotas")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoute([FromRoute] DeleteExcelFileCommand deleteExcelFileCommand)
    {
        return await CustomResponseAsync(deleteExcelFileCommand);
    }

}