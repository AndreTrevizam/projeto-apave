using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto_apave.Models;
public class Painel
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Nome { get; set; }

    [Required]
    public string Descricao { get; set; }

    [Required]
    [Range(0.01, double.MaxValue)]
    public double Altura { get; set; }

    [Range(0.01, double.MaxValue)]
    public double Largura { get; set; }

    [Range(0.01, double.MaxValue)]
    public double Comprimento { get; set; }

    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public DateTime? DataAlteracao { get; set; }

    public List<Manutencao> Manutencoes { get; set; } = new List<Manutencao>();

    public ICollection<PainelPeca> Pecas { get; set; } = new List<PainelPeca>();

    public int UsuarioId { get; set; }

    [ForeignKey("UsuarioId")]
    public Usuario Usuario { get; set; }
}
