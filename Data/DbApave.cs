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

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<PainelPeca>().HasNoKey();
  }
}