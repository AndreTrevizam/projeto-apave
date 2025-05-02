namespace projeto_apave.Models
{
    public class PainelPecaForm
    {
        // Dados do Painel
        public int PainelId { get; set; }
        public string? PainelNome { get; set; }

        // Opção 1: Nova peça
        public string? NovaPecaNome { get; set; }
        public string? NovaPecaDescricao { get; set; }

        // Opção 2: Peça existente
        public int? PecaExistenteId { get; set; }
        public List<Peca>? PecasExistentes { get; set; }

        // Campos específicos da relação (PainelPeca)
        public int Quantidade { get; set; } = 1;
        public DateTime DataInstalacao { get; set; } = DateTime.Today;
        public StatusPainelPeca Status { get; set; } = StatusPainelPeca.Ativo;
    }
}