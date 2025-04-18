using System.ComponentModel.DataAnnotations;

public class Peca {
  [Key]
  public int Id { get; set; }

  [Required]
  public string Nome { get; set; }
  public string Descricao { get; set; }

  public DateTime DataCriacao { get; set; } = DateTime.Now;
  public DateTime? DataAlteracao { get; set; }
}