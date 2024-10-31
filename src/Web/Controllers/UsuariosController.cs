using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCliente;
using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCorretor;
using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloUsuario;
using Academia.Programador.Bk.Gestao.Imobiliaria.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Web.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UsuariosController : Controller
    {
        private readonly IServiceUsuario _serviceUsuario;
        private readonly IServiceCliente _serviceCliente;
        private readonly IServiceCorretor _serviceCorretor;
        public UsuariosController(
            IServiceUsuario serviceUsuario,
            IServiceCliente serviceCliente,
            IServiceCorretor serviceCorretor)
        {
            _serviceUsuario = serviceUsuario;
            _serviceCliente = serviceCliente;
            _serviceCorretor = serviceCorretor;
        }

        // GET: Usuarios
        public IActionResult Index()
        {
            var usuarios = _serviceUsuario.TragaTodos();

            return View(usuarios.ToUsuariosViewModel());
        }

        // GET: Usuarios/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = _serviceUsuario.TragaPorId(id.Value);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario.ToUsuarioViewModel());
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            ViewBag.Clientes = _serviceCliente.TragaTodosClientes().FindAll(x => x.Usuario == null).ToClientesViewModel();
            ViewBag.Corretores = _serviceCorretor.TragaTodosCorretores().FindAll(x => x.Usuario == null).ToCorretoresViewModel();

            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                _serviceUsuario.Criar(usuario.ToUsuario());
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Clientes = _serviceCliente.TragaTodosClientes().FindAll(x => x.Usuario == null).ToClientesViewModel();
            ViewBag.Corretores = _serviceCorretor.TragaTodosCorretores().FindAll(x => x.Usuario == null).ToCorretoresViewModel();
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = _serviceUsuario.TragaPorId(id.Value);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewBag.Clientes = _serviceCliente.TragaTodosClientes().FindAll(x => x.Usuario == null).ToClientesViewModel();
            ViewBag.Corretores = _serviceCorretor.TragaTodosCorretores().FindAll(x => x.Usuario == null).ToCorretoresViewModel();
            return View(usuario.ToUsuarioViewModel());
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UsuarioViewModel usuario)
        {
            if (id != usuario.UsuarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _serviceUsuario.Salvar(usuario.ToUsuario());

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            ViewBag.Clientes = _serviceCliente.TragaTodosClientes().FindAll(x => x.Usuario == null).ToClientesViewModel();
            ViewBag.Corretores = _serviceCorretor.TragaTodosCorretores().FindAll(x => x.Usuario == null).ToCorretoresViewModel();
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = _serviceUsuario.TragaPorId(id.Value);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario.ToUsuarioViewModel());
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _serviceUsuario.Remover(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
