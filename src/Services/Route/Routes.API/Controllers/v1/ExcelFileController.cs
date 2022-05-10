using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteManager.WebAPI.Core.Controllers;
using RouteManager.WebAPI.Core.Notifications;
using Routes.API.Services;
using System.Threading.Tasks;
using Routes.Domain.Commands.v1.ExcelFiles.Delete;
using Routes.Domain.Commands.v1.ExcelFiles.Report;
using Routes.Domain.Commands.v1.ExcelFiles.Upload;

namespace Routes.API.Controllers.v1;

[Route("api/v1/[controller]")]
public class ExcelFileController : BaseController
{
    private readonly IExcelFileService _excelFileService;

    public ExcelFileController(IExcelFileService excelFileService, IMediator mediator, INotifier notifier) : base(mediator, notifier)
    {
        _excelFileService = excelFileService;
    }

    [HttpGet]
    public async Task<IActionResult> GetRoute()
    {
        return Ok(await _excelFileService.GetExcelFilesAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoute(string id)
    {
        var excelFile = await _excelFileService.GetExcelFileByIdAsync(id);

        if (excelFile == null)
        {
            return NotFound();
        }

        return Ok(excelFile);
    }


    [Authorize(Roles = "Relátorio Rotas")]
    [HttpPost]
    public async Task<IActionResult> UploadExcelFileAsync([FromForm] UploadExcelFileCommand excelFileCommand)
    {
        return await CustomResponseAsync(excelFileCommand);
    }

    [Authorize(Roles = "Relátorio Rotas")]
    [HttpPost("report")]
    public async Task<IActionResult> ReportRoute(ReportRouteCommand reportRouteCommand)
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