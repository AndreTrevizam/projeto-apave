using System.ComponentModel.DataAnnotations;

namespace projeto_apave.Models
{
  public class SolicitacaoManutencao
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public int UsuarioId { get; set; }  // Quem fez a solicitação
    public Usuario Usuario { get; set; }

    [Required]
    public int PainelId { get; set; }
    public Painel Painel { get; set; }

    [Required(ErrorMessage = "A descrição da manutenção é obrigatória.")]
    public string Descricao { get; set; }

    public DateTime DataSolicitacao { get; set; } = DateTime.Now;

    public StatusManutencao StatusManutencao { get; set; } = StatusManutencao.Pendente;
  }

  public enum StatusManutencao
  {
    Pendente,
    Atendida,
    Finalizada
  }
}
