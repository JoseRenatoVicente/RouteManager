using GeradorRotas.Application.Services.Interfaces;
using GeradorRotas.Application.ViewModels;
using GeradorRotas.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace GeradorRotasMVC.Controllers
{
    public class RotasController : Controller
    {
        private readonly IRotaService _rotaService;

        public RotasController(IRotaService rotaService)
        {
            _rotaService = rotaService;
        }

        [HttpGet]
        public IActionResult BatchRotaUpload()
        {
            return View();
        }

        public async Task<ActionResult> BatchRotaUpload(IFormFile file)
        {
            return View(await _rotaService.RotaUpload(file));
        }

        public async Task<ActionResult> ExportToDocx(ReportRotaViewModel reportRota)
        {
            return File(await _rotaService.ExportToDocx(reportRota), "application/octet-stream", "Rotas" + DateTime.Now.ToString("dd_MM_yyyy")+".docx");
        }


        public async Task<IActionResult> Index()
        {
            return View(await _rotaService.GetRotasAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null) return NotFound();

            var rota = await _rotaService.GetRotaByIdAsync(id);

            if (rota == null) return NotFound();

            return View(rota);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OS,Base,Servico,Id")] Rota rota)
        {
            if (ModelState.IsValid)
            {
                await _rotaService.AddRotaAsync(rota);

                return RedirectToAction(nameof(Index));
            }
            return View(rota);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rota = await _rotaService.GetRotaByIdAsync(id);
            if (rota == null)
            {
                return NotFound();
            }
            return View(rota);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("OS,Base,Servico,Id")] Rota rota)
        {
            if (id != rota.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _rotaService.UpdateRotaAsync(rota);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await RotaExists(rota.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(rota);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rota = await _rotaService.GetRotaByIdAsync(id);

            if (rota == null)
            {
                return NotFound();
            }

            return View(rota);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var rota = await _rotaService.GetRotaByIdAsync(id);
            await _rotaService.RemoveRotaAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> RotaExists(string id)
        {
            return await _rotaService.GetRotaByIdAsync(id) != null;
        }
    }
}
