using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCorretor;
using Academia.Programador.Bk.Gestao.Imobiliaria.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Web.Controllers
{
    [Authorize(Roles = "Corretor")]
    public class CorretoresController : BaseController
    {
        private readonly IServiceCorretor _serviceCorretor;
        private readonly ILogger<CorretoresController> _logger;

        public CorretoresController(IServiceCorretor serviceCorretor, ILogger<CorretoresController> logger)
        {
            _serviceCorretor = serviceCorretor;
            _logger = logger;
        }

        // GET: Corretores
        public async Task<IActionResult> Index()
        {
            var corretoresVO = _serviceCorretor.TragaTodosCorretores();

            _logger.LogInformation($"Hora que aconteceu uma visita {DateTime.Now.SomaDaHora()}");


            return View(corretoresVO.ToCorretoresViewModel());
        }

        // GET: Corretores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CorretorViewModel corretor = _serviceCorretor.TragaCorretorPorId(id.Value).ToCorretorViewModel();

            if (corretor == null)
            {
                return NotFound();
            }

            return View(corretor);
        }

        // GET: Corretores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Corretores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CriarCorretorViewModel corretor)
        {
            if (ModelState.IsValid)
            {
                _serviceCorretor.CriarCorretor(corretor.ToCorretor());
                return RedirectToAction(nameof(Index));
            }
            return View(corretor);
        }

        // GET: Corretores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EditarCorretorViewModel corretor = _serviceCorretor.TragaCorretorPorId(id.Value).ToEditarCorretorViewModel();

            if (corretor == null)
            {
                return NotFound();
            }
            return View(corretor);
        }

        // POST: Corretores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditarCorretorViewModel corretor)
        {
            if (id != corretor.CorretorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _serviceCorretor.SalvarCorretor(corretor.ToCorretor());
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", $"Erro ao salvar: {e.Message}");
                }
            }
            return View(corretor);
        }

        // GET: Corretores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CorretorViewModel corretor = _serviceCorretor.TragaCorretorPorId(id.Value).ToCorretorViewModel();

            if (corretor == null)
            {
                return NotFound();
            }

            return View(corretor);
        }

        // POST: Corretores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _serviceCorretor.Remover(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
