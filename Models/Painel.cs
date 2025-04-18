using System.ComponentModel.DataAnnotations;

public class Painel {
  [Key]
  public int Id { get; set; }

  public string Descricao { get; set; }

  [Required]
  public double Altura { get; set; }
  public double Largura { get; set; }
  public double Comprimento { get; set; }

  public DateTime DataCriacao { get; set; } = DateTime.Now;
  public DateTime? DataAlteracao { get; set; }
  public List<Manutencao> Manutencoes {get; set;}
}