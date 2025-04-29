using System.ComponentModel.DataAnnotations;

namespace projeto_apave.Models
{
    public class SolicitacaoPainel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UsuarioId { get; set; }  // Quem fez a solicitação
        public Usuario Usuario { get; set; }

        [Required(ErrorMessage = "O nome do painel é obrigatório.")]
        public string NomePainel { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Descricao { get; set; }

        public DateTime DataSolicitacao { get; set; } = DateTime.Now;

        public StatusSolicitacao Status { get; set; } = StatusSolicitacao.Pendente;
    }

    public enum StatusSolicitacao
    {
        Pendente,
        Aprovado,
        Recusado
    }
}
