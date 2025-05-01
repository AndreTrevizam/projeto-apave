namespace projeto_apave.Models;
public class PainelPeca
{
    public int PainelId { get; set; }
    public Painel Painel { get; set; }

    public int PecaId { get; set; }
    public Peca Peca { get; set; }

    public int Quantidade { get; set; }

    public DateTime DataInstalacao { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public DateTime? DataAtualizacao { get; set; }

    public StatusPainelPeca Status { get; set; } = StatusPainelPeca.Ativo;
}

public enum StatusPainelPeca
{
    Ativo,
    Inativo
}
