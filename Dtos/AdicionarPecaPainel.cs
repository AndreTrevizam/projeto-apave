public class AdicionarPecaPainelDto
{
    public int PainelId { get; set; }
    public int PecaId { get; set; }
    public int Quantidade { get; set; }
    public DateTime DataInstalacao { get; set; } = DateTime.Now;
}
