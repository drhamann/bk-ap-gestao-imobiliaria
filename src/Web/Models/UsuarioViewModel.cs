using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloUsuario;
using System.ComponentModel.DataAnnotations;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Web.Models
{
    public class UsuarioViewModel
    {
        [Required]
        public string Nome { get; set; } = null!;

        [Required]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        public string Senha { get; set; } = null!;

        public int? ClienteId { get; set; }

        public int? CorretorId { get; set; }
        public int? PerfilId { get; set; }
        public int? UsuarioId { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }

    public static class UsuarioViewModelExtensions
    {
        public static Usuario ToUsuario(this UsuarioViewModel createUsuarioViewModel)
        {
            // Mapeamento para a entidade Usuario
            var user = new Usuario
            {
                Nome = createUsuarioViewModel.Nome,
                Email = createUsuarioViewModel.Email,
                SenhaHash = createUsuarioViewModel.Senha,
                DataCriacao = createUsuarioViewModel.DataCriacao,
                PerfilId = createUsuarioViewModel.ClienteId.HasValue ? 1 : (createUsuarioViewModel.CorretorId.HasValue ? 2 : 3) // 1: Cliente, 2: Corretor, 3: Administrador,

            };

            user.ClienteId = createUsuarioViewModel.ClienteId.HasValue
                ? createUsuarioViewModel.ClienteId.Value
                : user.ClienteId;

            user.CorretorId = createUsuarioViewModel.CorretorId.HasValue
                ? createUsuarioViewModel.CorretorId.Value
                : user.CorretorId;

            user.UsuarioId = createUsuarioViewModel.UsuarioId.HasValue
                ? createUsuarioViewModel.UsuarioId.Value
                : user.UsuarioId;

            return user;
        }
        public static UsuarioViewModel ToUsuarioViewModel(this Usuario usuario)
        {
            // Mapeamento de Usuario para UsuarioViewModel
            return new UsuarioViewModel
            {
                UsuarioId = usuario.UsuarioId,
                Nome = usuario.Nome,
                Email = usuario.Email,
                PerfilId = usuario.PerfilId,
                ClienteId = usuario.ClienteId,
                CorretorId = usuario.CorretorId,
                DataCriacao = usuario.DataCriacao
            };
        }

        public static List<UsuarioViewModel> ToUsuariosViewModel(this List<Usuario> usuarios)
        {
            var usuarioViewModels = new List<UsuarioViewModel>();
            usuarios.ForEach(usuario => usuarioViewModels.Add(usuario.ToUsuarioViewModel()));
            return usuarioViewModels;
        }
    }
}
