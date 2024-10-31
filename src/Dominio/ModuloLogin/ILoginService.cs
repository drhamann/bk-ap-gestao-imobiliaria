using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloUsuario;
using Microsoft.AspNetCore.Identity;
using System.Security.Authentication;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloLogin;

public interface ILoginService
{
    Usuario Autenticar(string email, string senha);
}

public class LoginService : ILoginService
{
    private readonly IPasswordHasher<Usuario> _passwordHasher;
    private readonly IServiceUsuario _serviceUsuario;
    public LoginService(IServiceUsuario serviceUsuario)
    {
        _serviceUsuario = serviceUsuario;
        _passwordHasher = new PasswordHasher<Usuario>();
    }
    public Usuario Autenticar(string email, string senha)
    {
        Usuario usuario = _serviceUsuario.TragaTodos().Find(user => user.Email == email);

        if (usuario != null)
        {
            if (_passwordHasher.VerifyHashedPassword(usuario, usuario.SenhaHash, senha) ==
                PasswordVerificationResult.Success)
            {
                return usuario;
            }
        }

        throw new AuthenticationException("Dados incorretos");
    }
}