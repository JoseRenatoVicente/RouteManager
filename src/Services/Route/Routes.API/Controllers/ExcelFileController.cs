using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RouteManager.Domain.Entities;
using RouteManager.WebAPI.Core.Controllers;
using RouteManager.WebAPI.Core.Notifications;
using Routes.API.DTO;
using Routes.API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Routes.API.Controllers
{
    [Route("api/[controller]")]
    public class ExcelFileController : BaseController
    {
        private readonly IExcelFileService _excelFileService;


        public ExcelFileController(IExcelFileService excelFileService, INotifier notifier) : base(notifier)
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
        public async Task<ActionResult<ExcelFile>> UploadExcelFileAsync(IFormFile file)
        {
            return await CustomResponseAsync(await _excelFileService.AddExcelFileAsync(file));
        }

        [Authorize(Roles = "Relátorio Rotas")]
        [HttpPost("report")]
        public async Task<ActionResult<byte[]>> ReportRoute(ReportRouteRequest reportRoute)
        {
            return await CustomResponseAsync(await _excelFileService.ReportRoutesToDocx(reportRoute));
        }

        [Authorize(Roles = "Relátorio Rotas")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoute(string id)
        {
            var route = await GetRoute(id);
            if (route == null)
            {
                return NotFound();
            }
            await _excelFileService.RemoveExcelFileAsync(id);

            return NoContent();
        }

    }
}
