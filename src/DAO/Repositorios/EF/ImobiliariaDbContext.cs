using Academia.Programador.Bk.Gestao.Imobiliaria.DAO.Repositorios.EF.Modulo_Cliente;
using Academia.Programador.Bk.Gestao.Imobiliaria.DAO.Repositorios.EF.Modulo_Corretor;
using Academia.Programador.Bk.Gestao.Imobiliaria.DAO.Repositorios.EF.Modulo_Imovel;
using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCliente;
using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCorretor;
using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloImovel;
using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloUsuario;
using Academia.Programador.Bk.Gestao.Imobiliaria.Web;
using Microsoft.EntityFrameworkCore;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.DAO.Repositorios.EF;


// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

public partial class ImobiliariaDbContext : DbContext
{
    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Corretor> Corretores { get; set; }

    public virtual DbSet<Favorito> Favoritos { get; set; }

    public virtual DbSet<Imovel> Imoveis { get; set; }

    public virtual DbSet<MensagensContato> MensagensContatos { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<Perfil> Perfis { get; set; }

    public ImobiliariaDbContext(DbContextOptions<ImobiliariaDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ClienteEntityConfiguration clienteEntityConfiguration = new();
        CorretorEntityConfiguration corretorEntityConfiguration = new();
        ImovelEntityConfiguration imovelEntityConfiguration = new();
        UsuarioEntityConfiguration usuarioEntityConfiguration = new();

        modelBuilder.ApplyConfiguration(clienteEntityConfiguration);
        modelBuilder.ApplyConfiguration(corretorEntityConfiguration);
        modelBuilder.ApplyConfiguration(imovelEntityConfiguration);
        modelBuilder.ApplyConfiguration(usuarioEntityConfiguration);


        modelBuilder.Entity<Favorito>(entity =>
        {
            entity.HasKey(e => e.FavoritoId).HasName("PK__Favorito__CFF711E53A94905C");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Favoritos)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Favoritos__Clien__45F365D3");

            entity.HasOne(d => d.Imovel).WithMany(p => p.Favoritos)
                .HasForeignKey(d => d.ImovelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Favoritos__Imove__46E78A0C");
        });

        modelBuilder.Entity<MensagensContato>(entity =>
        {
            entity.HasKey(e => e.MensagemId).HasName("PK__Mensagen__7C0322C6DF8C64FE");

            entity.ToTable("MensagensContato");

            entity.Property(e => e.DataEnvio)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Cliente).WithMany(p => p.MensagensContatos)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Mensagens__Clien__4BAC3F29");

            entity.HasOne(d => d.Corretor).WithMany(p => p.MensagensContatos)
                .HasForeignKey(d => d.CorretorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Mensagens__Corre__4CA06362");

            entity.HasOne(d => d.Imovel).WithMany(p => p.MensagensContatos)
                .HasForeignKey(d => d.ImovelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Mensagens__Imove__4AB81AF0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public void Seed()
    {
        //Database.EnsureCreated();

        if (Corretores.Count() == 0)
        {
            Corretor corretor = new()
            {
                Cpf = "12345678915",
                Creci = "123",
                Email = "john@devolta.com",
                Nome = "John 4",
                Telefone = "666 99996669"
            };
            Corretores.Add(corretor);
            SaveChanges();
        }

        if (Clientes.Count() == 0)
        {
            Cliente cliente = new()
            {
                Cpf = "12345678915",
                Email = "john@devolta.com",
                Nome = "John 4",
                Telefone = "666 99996669"
            };
            Clientes.Add(cliente);
            SaveChanges();
        }

        if (Imoveis.Count() == 0)
        {
            Imovel imovel = new Imovel()
            {
                Area = 50,
                ClienteDonoId = Clientes.First().ClienteId,
                CorretorGestorId = Corretores.First().CorretorId,
                Descricao = "descricao",
                Endereco = "na nuvem de poeira",
                Disponivel = true,
                Negocio = 1,
                Tipo = 1,
                Valor = 2300000
            };
            this.Imoveis.Add(imovel);
            SaveChanges();
        }

        if (Usuarios.Count() == 0)
        {
            Usuarios.Add(new()
            {
                Email = "john2@wick.com",
                Nome = "John2",
                SenhaHash = "AQAAAAIAAYagAAAAEAcwH8ucYtATRdWjLP2Rz6CXgDRW7w6I2q15wZcuyWkPa2QwIEM43l6cCdfwx1edOw==",
                Perfil = new Perfil() { Nome = "Cliente" }//1
            });
            Usuarios.Add(new()
            {
                Email = "john3@wick.com",
                Nome = "John3",
                SenhaHash = "AQAAAAIAAYagAAAAEAcwH8ucYtATRdWjLP2Rz6CXgDRW7w6I2q15wZcuyWkPa2QwIEM43l6cCdfwx1edOw==",
                Perfil = new Perfil() { Nome = "Corretor" }//2
            });
            Usuarios.Add(new()
            {
                Email = "john@wick.com",
                Nome = "John",
                SenhaHash = "AQAAAAIAAYagAAAAEAcwH8ucYtATRdWjLP2Rz6CXgDRW7w6I2q15wZcuyWkPa2QwIEM43l6cCdfwx1edOw==",
                Perfil = new Perfil() { Nome = "Administrador" }//3
            });
            SaveChanges();
        }

        if (Perfis.Count() == 0)
        {
            Perfis.Add(new Perfil() { Nome = "Cliente" });
            Perfis.Add(new Perfil() { Nome = "Corretor" });
            Perfis.Add(new Perfil() { Nome = "Administrador" });
            SaveChanges();
        }

        Perfil.Perfis = Perfis.ToList();
    }
}
