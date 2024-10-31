using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloImovel;
using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloUsuario;
using Academia.Programador.Bk.Gestao.Imobiliaria.Web;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCliente;

public partial class Cliente
{
    public int ClienteId { get; set; }

    public string Nome { get; set; } = null!;

    public string Cpf { get; set; } = null!;

    public string? Telefone { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Favorito> Favoritos { get; set; } = new List<Favorito>();

    public virtual ICollection<Imovel> Imoveis { get; set; } = new List<Imovel>();

    public virtual ICollection<MensagensContato> MensagensContatos { get; set; } = new List<MensagensContato>();

    public Usuario? Usuario { get; set; }
}
