using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloUsuario;
using Microsoft.EntityFrameworkCore;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.DAO.Repositorios.EF.ModuloUsuario
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ImobiliariaDbContext _context;

        public UsuarioRepositorio(ImobiliariaDbContext context)
        {
            _context = context;
        }

        public void Criar(Usuario model)
        {
            _context.Usuarios.Add(model);
            _context.SaveChanges();
        }

        public List<Usuario> TragaTodos()
        {
            return _context.Usuarios.Include(user => user.Perfil).ToList();
        }

        public void Salvar(Usuario model)
        {
            var usuarioExistente = _context.Usuarios.FirstOrDefault(c => c.UsuarioId == model.UsuarioId);
            if (usuarioExistente != null)
            {
                _context.Entry(usuarioExistente).State = EntityState.Detached;
            }
            _context.Usuarios.Update(model);
            _context.SaveChanges();
        }

        public Usuario TragaPorId(int id)
        {
            var usuarioExistente = _context.Usuarios.FirstOrDefault(c => c.UsuarioId == id);
            return usuarioExistente;
        }

        public void Remover(int id)
        {
            var usuarioExistente = _context.Usuarios.FirstOrDefault(c => c.UsuarioId == id);
            if (usuarioExistente != null)
            {
                _context.Entry(usuarioExistente).State = EntityState.Detached;
            }

            _context.Usuarios.Remove(usuarioExistente);
            _context.SaveChanges();
        }
    }
}
