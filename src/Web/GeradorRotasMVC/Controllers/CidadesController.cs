using GeradorRotas.Application.Services.Interfaces;
using GeradorRotas.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GeradorRotasMVC.Controllers
{
    public class CidadesController : Controller
    {

        private readonly ICidadeService _cidadeService;

        public CidadesController(ICidadeService cidadeService)
        {
            _cidadeService = cidadeService;
        }


        // GET: Cidades
        public async Task<IActionResult> Index()
        {
            return View(await _cidadeService.GetCidadesAsync());
        }

        // GET: Cidades/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cidade = await _cidadeService.GetCidadeByIdAsync(id);
            if (cidade == null)
            {
                return NotFound();
            }

            return View(cidade);
        }

        // GET: Cidades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cidades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Estado,Id")] Cidade cidade)
        {
            if (ModelState.IsValid)
            {
                await _cidadeService.AddCidadeAsync(cidade);
                return RedirectToAction(nameof(Index));
            }
            return View(cidade);
        }

        // GET: Cidades/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cidade = await _cidadeService.GetCidadeByIdAsync(id);
            if (cidade == null)
            {
                return NotFound();
            }
            return View(cidade);
        }

        // POST: Cidades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Nome,Estado,Id")] Cidade cidade)
        {
            if (id != cidade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _cidadeService.UpdateCidadeAsync(cidade);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CidadeExists(cidade.Id))
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
            return View(cidade);
        }

        // GET: Cidades/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cidade = await _cidadeService.GetCidadeByIdAsync(id);
            if (cidade == null)
            {
                return NotFound();
            }

            return View(cidade);
        }

        // POST: Cidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cidade = await _cidadeService.GetCidadeByIdAsync(id);

            await _cidadeService.RemoveCidadeAsync(cidade.Id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CidadeExists(string id)
        {
            return await _cidadeService.GetCidadeByIdAsync(id) != null;
        }
    }
}
