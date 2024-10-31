using System.ComponentModel.DataAnnotations;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Web.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Senha { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email invalido")]
        public string Email { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
