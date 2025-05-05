using Microsoft.EntityFrameworkCore;
using projeto_apave.Models;

namespace projeto_apave.Data;

public class DbApave : DbContext
{
  public DbApave(DbContextOptions options) : base(options) { }

  public DbSet<Manutencao> Manutencao { get; set; }
  public DbSet<Painel> Painel { get; set; }
  public DbSet<PainelPeca> PainelPeca { get; set; }
  public DbSet<Peca> Peca { get; set; }
  public DbSet<Usuario> Usuario { get; set; }
  public DbSet<SolicitacaoPainel> SolicitacaoPainel { get; set; }
  public DbSet<SolicitacaoManutencao> SolicitacaoManutencao { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Usuario>()
      .HasIndex(u => u.Email)
      .IsUnique();
  
    // Chave composta
    modelBuilder.Entity<PainelPeca>()
      .HasKey(pp => new { pp.PainelId, pp.PecaId });

    // Relacionamento
    modelBuilder.Entity<PainelPeca>()
      .HasOne(pp => pp.Painel)
      .WithMany(p => p.Pecas)
      .HasForeignKey(pp => pp.PainelId);

    modelBuilder.Entity<PainelPeca>()
      .HasOne(pp => pp.Peca)
      .WithMany()
      .HasForeignKey(pp => pp.PecaId);

    modelBuilder.Entity<Painel>()
      .HasOne(p => p.Usuario)
      .WithMany()
      .HasForeignKey(p => p.UsuarioId)
      .OnDelete(DeleteBehavior.ClientSetNull); 

    modelBuilder.Entity<Manutencao>()
      .HasOne(m => m.Painel)
      .WithMany(p => p.Manutencoes)
      .HasForeignKey(m => m.PainelId)
      .OnDelete(DeleteBehavior.Cascade);

    base.OnModelCreating(modelBuilder);
  }
}