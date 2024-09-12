using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Web.Controllers
{
    public class ImoveisController : Controller
    {
        private readonly ImobiliariaDbContext _context;

        public ImoveisController(ImobiliariaDbContext context)
        {
            _context = context;
        }

        // GET: Imovels
        public async Task<IActionResult> Index()
        {
            var imobiliariaDbContext = _context.Imoveis.Include(i => i.ClienteDono).Include(i => i.CorretorGestor).Include(i => i.CorretorNegocio);
            return View(await imobiliariaDbContext.ToListAsync());
        }

        // GET: Imovels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imovel = await _context.Imoveis
                .Include(i => i.ClienteDono)
                .Include(i => i.CorretorGestor)
                .Include(i => i.CorretorNegocio)
                .FirstOrDefaultAsync(m => m.ImovelId == id);
            if (imovel == null)
            {
                return NotFound();
            }

            return View(imovel);
        }

        // GET: Imovels/Create
        public IActionResult Create()
        {
            ViewData["ClienteDonoId"] = new SelectList(_context.Clientes, "ClienteId", "Cpf");
            ViewData["CorretorGestorId"] = new SelectList(_context.Corretores, "CorretorId", "Cpf");
            ViewData["CorretorNegocioId"] = new SelectList(_context.Corretores, "CorretorId", "Cpf");
            return View();
        }

        // POST: Imovels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImovelId,Endereco,Tipo,Area,Valor,Descricao,Negocio,CorretorNegocioId,CorretorGestorId,ClienteDonoId,Disponivel,Fotos")] Imovel imovel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(imovel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteDonoId"] = new SelectList(_context.Clientes, "ClienteId", "Cpf", imovel.ClienteDonoId);
            ViewData["CorretorGestorId"] = new SelectList(_context.Corretores, "CorretorId", "Cpf", imovel.CorretorGestorId);
            ViewData["CorretorNegocioId"] = new SelectList(_context.Corretores, "CorretorId", "Cpf", imovel.CorretorNegocioId);
            return View(imovel);
        }

        // GET: Imovels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imovel = await _context.Imoveis.FindAsync(id);
            if (imovel == null)
            {
                return NotFound();
            }
            ViewData["ClienteDonoId"] = new SelectList(_context.Clientes, "ClienteId", "Cpf", imovel.ClienteDonoId);
            ViewData["CorretorGestorId"] = new SelectList(_context.Corretores, "CorretorId", "Cpf", imovel.CorretorGestorId);
            ViewData["CorretorNegocioId"] = new SelectList(_context.Corretores, "CorretorId", "Cpf", imovel.CorretorNegocioId);
            return View(imovel);
        }

        // POST: Imovels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImovelId,Endereco,Tipo,Area,Valor,Descricao,Negocio,CorretorNegocioId,CorretorGestorId,ClienteDonoId,Disponivel,Fotos")] Imovel imovel)
        {
            if (id != imovel.ImovelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(imovel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImovelExists(imovel.ImovelId))
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
            ViewData["ClienteDonoId"] = new SelectList(_context.Clientes, "ClienteId", "Cpf", imovel.ClienteDonoId);
            ViewData["CorretorGestorId"] = new SelectList(_context.Corretores, "CorretorId", "Cpf", imovel.CorretorGestorId);
            ViewData["CorretorNegocioId"] = new SelectList(_context.Corretores, "CorretorId", "Cpf", imovel.CorretorNegocioId);
            return View(imovel);
        }

        // GET: Imovels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imovel = await _context.Imoveis
                .Include(i => i.ClienteDono)
                .Include(i => i.CorretorGestor)
                .Include(i => i.CorretorNegocio)
                .FirstOrDefaultAsync(m => m.ImovelId == id);
            if (imovel == null)
            {
                return NotFound();
            }

            return View(imovel);
        }

        // POST: Imovels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var imovel = await _context.Imoveis.FindAsync(id);
            if (imovel != null)
            {
                _context.Imoveis.Remove(imovel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImovelExists(int id)
        {
            return _context.Imoveis.Any(e => e.ImovelId == id);
        }
    }
}
