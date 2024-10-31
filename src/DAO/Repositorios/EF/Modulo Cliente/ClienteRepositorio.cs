using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCliente;
using Microsoft.EntityFrameworkCore;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.DAO.Repositorios.EF
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly ImobiliariaDbContext _context;

        public ClienteRepositorio(ImobiliariaDbContext context)
        {
            _context = context;
        }

        public void CriarCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
        }

        public List<Cliente> TragaTodosClientes()
        {
            return _context.Clientes.Include(x => x.Usuario).AsNoTracking().ToList();
        }

        public void SalvarCliente(Cliente cliente)
        {
            var clienteExistente = _context.Clientes.Local.FirstOrDefault(c => c.ClienteId == cliente.ClienteId);
            if (clienteExistente != null)
            {
                _context.Entry(clienteExistente).State = EntityState.Detached;
            }
            _context.Update(cliente);
            _context.SaveChanges();
        }

        public Cliente TragaClientePorId(int? id)
        {
            return _context.Clientes.AsNoTracking().FirstOrDefault(cliente => cliente.ClienteId == id);
        }

        public void Remover(int id)
        {
            var clienteExistente = _context.Clientes.Local.FirstOrDefault(c => c.ClienteId == id);
            if (clienteExistente != null)
            {
                _context.Entry(clienteExistente).State = EntityState.Detached;
            }
            _context.Clientes.Remove(clienteExistente);
            _context.SaveChanges();
        }

        public bool ClientePorCpf(string clienteCpf, int clienteClienteId)
        {
            return _context.Clientes.AsNoTracking().FirstOrDefault(cliente => string.Compare(cliente.Cpf, clienteCpf) == 0 && cliente.ClienteId != clienteClienteId) != null;
        }

        public bool ClientePorEmail(string? clienteEmail, int clienteClienteId)
        {
            return _context.Clientes.AsNoTracking().FirstOrDefault(cliente => string.Compare(cliente.Email, clienteEmail) == 0 && cliente.ClienteId != clienteClienteId) != null;
        }
    }
}
