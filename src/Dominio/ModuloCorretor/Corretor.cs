using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloImovel;
using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloUsuario;
using Academia.Programador.Bk.Gestao.Imobiliaria.Web;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCorretor;

public partial class Corretor
{
    public int CorretorId { get; set; }

    public string Nome { get; set; } = null!;

    public string Cpf { get; set; } = null!;

    public string Creci { get; set; } = null!;

    public string? Telefone { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Imovel> ImoveiCorretorGestors { get; set; } = new List<Imovel>();

    public virtual ICollection<Imovel> ImoveiCorretorNegocios { get; set; } = new List<Imovel>();

    public virtual ICollection<MensagensContato> MensagensContatos { get; set; } = new List<MensagensContato>();
    public Usuario? Usuario { get; set; }
}