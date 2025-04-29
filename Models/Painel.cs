using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class Painel
{
  [Key]
  public int Id { get; set; }

  public string Nome { get; set; }

  public string Descricao { get; set; }

  [Required]
  public double Altura { get; set; }
  public double Largura { get; set; }
  public double Comprimento { get; set; }

  public DateTime DataCriacao { get; set; } = DateTime.Now;
  public DateTime? DataAlteracao { get; set; }
  public List<Manutencao> Manutencoes { get; set; }

  public ICollection<PainelPeca> Pecas { get; set; } = new List<PainelPeca>();

   // RELACIONAMENTO COM USUÁRIO
  [Required]
  public int UsuarioId { get; set; }   // FK (chave estrangeira)

  [ForeignKey("UsuarioId")]
  public Usuario Usuario { get; set; } // Navegação
}
