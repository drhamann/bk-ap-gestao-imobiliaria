﻿using Academia.Programador.Bk.Gestao.Imobiliaria.DAO.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Web;


// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

public partial class ImobiliariaDbContext : DbContext
{
    private string ConnectionString { get; set; }
    public ImobiliariaDbContext(IOptions<ConnectionStrings> options)
    {
        ConnectionStrings conexoes = options.Value;

        ConnectionString = conexoes.Master;
    }

    public ImobiliariaDbContext(IOptions<ConnectionStrings> optionsObject, DbContextOptions<ImobiliariaDbContext> options)
        : base(options)
    {
        ConnectionStrings conexoes = optionsObject.Value;

        ConnectionString = conexoes.Master;
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Corretore> Corretores { get; set; }

    public virtual DbSet<Favorito> Favoritos { get; set; }

    public virtual DbSet<Imovel> Imoveis { get; set; }

    public virtual DbSet<MensagensContato> MensagensContatos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("PK__Clientes__71ABD0871A158499");

            entity.HasIndex(e => e.Cpf, "UQ__Clientes__C1F897319F8FF376").IsUnique();

            entity.Property(e => e.Cpf)
                .HasMaxLength(11)
                .HasColumnName("CPF");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Nome).HasMaxLength(100);
            entity.Property(e => e.Telefone).HasMaxLength(20);
        });

        modelBuilder.Entity<Corretore>(entity =>
        {
            entity.HasKey(e => e.CorretorId).HasName("PK__Corretor__4878C58FFBA660A3");

            entity.HasIndex(e => e.Cpf, "UQ__Corretor__C1F89731960F9C31").IsUnique();

            entity.HasIndex(e => e.Creci, "UQ__Corretor__C46674094787D8EB").IsUnique();

            entity.Property(e => e.Cpf)
                .HasMaxLength(11)
                .HasColumnName("CPF");
            entity.Property(e => e.Creci)
                .HasMaxLength(20)
                .HasColumnName("CRECI");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Nome).HasMaxLength(100);
            entity.Property(e => e.Telefone).HasMaxLength(20);
        });

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

        modelBuilder.Entity<Imovel>(entity =>
        {
            entity.ToTable("Imoveis");

            entity.HasKey(e => e.ImovelId).HasName("PK__Imoveis__68DA341CB2529FCE");

            entity.Property(e => e.Area).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Disponivel).HasDefaultValue(true);
            entity.Property(e => e.Endereco).HasMaxLength(255);
            entity.Property(e => e.Negocio).HasDefaultValue(1);
            entity.Property(e => e.Tipo).HasDefaultValue(1);
            entity.Property(e => e.Valor).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.ClienteDono).WithMany(p => p.Imoveis)
                .HasForeignKey(d => d.ClienteDonoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Imoveis__Cliente__4316F928");

            entity.HasOne(d => d.CorretorGestor).WithMany(p => p.ImoveiCorretorGestors)
                .HasForeignKey(d => d.CorretorGestorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Imoveis__Correto__412EB0B6");

            entity.HasOne(d => d.CorretorNegocio).WithMany(p => p.ImoveiCorretorNegocios)
                .HasForeignKey(d => d.CorretorNegocioId)
                .HasConstraintName("FK__Imoveis__Correto__4222D4EF");
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
        this.Database.EnsureCreated();

    }
}
