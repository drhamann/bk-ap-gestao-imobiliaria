using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCorretor;
using Microsoft.EntityFrameworkCore;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.DAO.Repositorios.EF.Modulo_Corretor
{
    public class CorretorRepositorio : ICorretorRepositorio
    {
        private readonly ImobiliariaDbContext _context;

        public CorretorRepositorio(ImobiliariaDbContext context)
        {
            _context = context;
        }

        public void CriarCorretor(Corretor corretor)
        {
            _context.Corretores.Add(corretor);
            _context.SaveChanges();
        }

        public List<Corretor> TragaTodosCorretores()
        {
            return _context.Corretores.Include(x => x.Usuario).AsNoTracking().ToList();
        }

        public void SalvarCorretor(Corretor Corretor)
        {
            var CorretorExistente = TragaCorretorPorId(Corretor.CorretorId);
            if (CorretorExistente != null)
            {
                _context.Entry(CorretorExistente).State = EntityState.Detached;
            }

            _context.Update(Corretor);
            _context.SaveChanges();
        }

        public Corretor TragaCorretorPorId(int? id)
        {
            return _context.Corretores.AsNoTracking().FirstOrDefault(Corretor => Corretor.CorretorId == id);
        }

        public void Remover(int id)
        {
            var CorretorExistente = TragaCorretorPorId(id);
            if (CorretorExistente != null)
            {
                _context.Entry(CorretorExistente).State = EntityState.Detached;
            }

            _context.Corretores.Remove(CorretorExistente);
            _context.SaveChanges();
        }

        public bool CorretorPorCpf(string CorretorCpf, int CorretorCorretorId)
        {
            return _context.Corretores.AsNoTracking().FirstOrDefault(Corretor =>
                string.Compare(Corretor.Cpf, CorretorCpf) == 0 && Corretor.CorretorId != CorretorCorretorId) != null;
        }

        public bool CorretorPorEmail(string? CorretorEmail, int CorretorCorretorId)
        {
            return _context.Corretores.AsNoTracking().FirstOrDefault(Corretor =>
                       string.Compare(Corretor.Email, CorretorEmail) == 0 &&
                       Corretor.CorretorId != CorretorCorretorId) !=
                   null;
        }

        public bool CorretorPorCreci(string creci, int corretorId)
        {
            return _context.Corretores.AsNoTracking().FirstOrDefault(Corretor =>
                       string.Compare(Corretor.Creci, creci) == 0 &&
                       Corretor.CorretorId != corretorId) !=
                   null;

        }
    }
}