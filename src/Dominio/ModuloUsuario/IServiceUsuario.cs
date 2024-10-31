using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.Compartilhada;
using Microsoft.AspNetCore.Identity;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloUsuario
{
    public interface IServiceUsuario : IServiceModel<Usuario> { }

    public interface IUsuarioRepositorio : IRepositorio<Usuario> { }

    public class ServiceUsuario : IServiceUsuario
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IPasswordHasher<Usuario> _passwordHasher;

        public ServiceUsuario(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _passwordHasher = new PasswordHasher<Usuario>(); ;
        }

        public void Criar(Usuario model)
        {
            //TODO: Fazer hash da senha
            var hashDaSenha = _passwordHasher.HashPassword(model, model.SenhaHash);
            model.SenhaHash = hashDaSenha;

            _usuarioRepositorio.Criar(model);
        }

        public List<Usuario> TragaTodos()
        {
            return _usuarioRepositorio.TragaTodos();
        }

        public void Salvar(Usuario model)
        {
            _usuarioRepositorio.Salvar(model);
        }

        public Usuario TragaPorId(int id)
        {
            return _usuarioRepositorio.TragaPorId(id);
        }

        public void Remover(int id)
        {
            _usuarioRepositorio.Remover(id);
        }
    }
}
