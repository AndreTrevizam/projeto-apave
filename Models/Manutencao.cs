using System.ComponentModel.DataAnnotations;
namespace projeto_apave.Models;
public class Manutencao {
  [Key]
  public int Id { get; set; }
  public DateTime dataManutencao { get; set; } = DateTime.Now;

  [Required]
  public string Descricao { get; set; }

  public DateTime DataCriacao { get; set; } = DateTime.Now;
  public DateTime? DataAlteracao { get; set; }

  // Foreign keys
  public int PainelId { get; set; }
  public Painel Painel { get; set; }
}