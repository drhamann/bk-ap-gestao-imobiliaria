using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCliente;
using Academia.Programador.Bk.Gestao.Imobiliaria.Web.Controllers;
using Academia.Programador.Bk.Gestao.Imobiliaria.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Web.Views
{
    [Authorize(Roles = "Administrador,Cliente")]
    public class ClientesController : BaseController
    {
        private readonly IServiceCliente _serviceCliente;

        public ClientesController(IServiceCliente serviceCliente)
        {
            _serviceCliente = serviceCliente;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var clientesVo = _serviceCliente.TragaTodosClientes();

            return View(clientesVo.ToClientesViewModel());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = _serviceCliente.TragaClientePorId(id.Value);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente.ToClienteViewModel());
        }

        // GET: Clientes/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateClienteViewModel cliente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _serviceCliente.CriarCliente(cliente.ToClienteVo());
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Erro na criação : {ex.Message}");
                }
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientesVo = _serviceCliente.TragaTodosClientes();
            var cliente = clientesVo.FirstOrDefault(m => m.ClienteId == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente.ToClienteViewModel());
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClienteViewModel cliente)
        {
            if (id != cliente.ClienteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _serviceCliente.SalvarCliente(cliente.ToClienteVo());
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Erro no editar : {ex.Message}");
                }
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = _serviceCliente.TragaClientePorId(id.Value);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente.ToClienteViewModel());
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                _serviceCliente.Remover(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro no deletar : {ex.Message}");
            }

            return View(_serviceCliente.TragaClientePorId(id).ToClienteViewModel());
        }

    }
}
