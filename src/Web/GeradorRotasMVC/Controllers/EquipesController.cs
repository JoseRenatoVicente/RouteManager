using GeradorRotas.Application.Services.Interfaces;
using GeradorRotas.Application.ViewModels;
using GeradorRotas.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GeradorRotasMVC.Controllers
{
    public class EquipesController : Controller
    {
        private readonly IEquipeService _equipeService;

        private readonly IPessoaRepository _pessoaRepository;
        private readonly ICidadeRepository _cidadeRepository;

        public EquipesController(IEquipeService equipeService, IPessoaRepository pessoaRepository, ICidadeRepository cidadeRepository)
        {
            _equipeService = equipeService;
            _pessoaRepository = pessoaRepository;
            _cidadeRepository = cidadeRepository;
        }


        // GET: Equipes
        public async Task<IActionResult> Index()
        {
            return View(await _equipeService.GetEquipesAsync());
        }

        // GET: Equipes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipe = await _equipeService.GetEquipeByIdAsync(id);
            if (equipe == null)
            {
                return NotFound();
            }

            return View(equipe);
        }

        // GET: Equipes/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new EquipeViewModel
            {
                Cidades = await _cidadeRepository.GetAllAsync(),
                Pessoas = await _pessoaRepository.GetAllAsync()
            };
            return View(viewModel);
        }

        // POST: Equipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Equipe")] EquipeViewModel equipeViewModel)
        {
            if (ModelState.IsValid)
            {
                equipeViewModel.Equipe.Cidade = await _cidadeRepository.GetByIdAsync(equipeViewModel.Equipe.Cidade.Id);

                await _equipeService.AddEquipeAsync(equipeViewModel.Equipe);

                return RedirectToAction(nameof(Index));
            }
            return View(equipeViewModel);
        }

        // GET: Equipes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cidades = await _cidadeRepository.GetAllAsync();
            var viewModel = new EquipeViewModel { Cidades = cidades };

            viewModel.Equipe = await _equipeService.GetEquipeByIdAsync(id);

            if (viewModel == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        // POST: Equipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(string id, EquipeViewModel equipeViewModel)
        {

            if (ModelState.IsValid)
            {
                equipeViewModel.Equipe.Id = id;
                try
                {
                    equipeViewModel.Equipe.Cidade = await _cidadeRepository.GetByIdAsync(equipeViewModel.Equipe.Cidade.Id);

                    await _equipeService.UpdateEquipeAsync(equipeViewModel.Equipe);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await EquipeExists(equipeViewModel.Equipe.Id))
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
            return View(equipeViewModel);
        }

        // GET: Equipes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipe = await _equipeService.GetEquipeByIdAsync(id);
            if (equipe == null)
            {
                return NotFound();
            }

            return View(equipe);
        }

        // POST: Equipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _equipeService.RemoveEquipeAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> EquipeExists(string id)
        {
            return await _equipeService.GetEquipeByIdAsync(id) != null;
        }
    }
}
