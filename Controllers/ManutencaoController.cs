using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projeto_apave.Data;
using System.Security.Claims;
using projeto_apave.Models;

public class ManutencaoController : Controller
{
  private readonly DbApave _db;

  public ManutencaoController(DbApave db)
  {
    _db = db;
  }

  public async Task<IActionResult> SolicitarManutencao()
  {
    var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
    var paineis = await _db.Painel
      .Where(p => p.UsuarioId == usuarioId)
      .ToListAsync();

    return View(paineis);
  }

  [HttpPost]
  public async Task<IActionResult> SolicitarManutencaoPost(int painelId, string descricao)
  {
    var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
    var painel = await _db.Painel
      .FindAsync(painelId);

    if (painel == null)
    {
      TempData["Erro"] = "Painel não encontrado.";
      return RedirectToAction("SolicitarManutencao");  // Redireciona de volta para a tabela
    }

    var solicitacao = new SolicitacaoManutencao
    {
      PainelId = painel.Id,
      UsuarioId = usuarioId,
      Descricao = descricao
    };

    _db.SolicitacaoManutencao.Add(solicitacao);
    await _db.SaveChangesAsync();

    return RedirectToAction("SolicitarManutencao");
  }


  [HttpGet]
  public async Task<IActionResult> ManutencaoInfo(int painelId)
  {
    var painel = await _db.Painel
        .FirstOrDefaultAsync(p => p.Id == painelId);

    if (painel == null)
    {
      return RedirectToAction("SolicitarManutencao");
    }

    return View(painel);
  }

  public async Task<IActionResult> Atender(int id)
  {
    var solicitacao = await _db.SolicitacaoManutencao.FindAsync(id);
    if (solicitacao == null) return NotFound();

    solicitacao.StatusManutencao = StatusManutencao.Atendida;

    var novaManutencao = new Manutencao
    {
      PainelId = solicitacao.PainelId,
      Descricao = solicitacao.Descricao,
    };

    _db.Manutencao.Add(novaManutencao);
    await _db.SaveChangesAsync();

    return RedirectToAction("GerenciarManutencoes", "Funcionario");
  }

  public async Task<IActionResult> Finalizar(int id)
  {
    var solicitacao = await _db.SolicitacaoManutencao.FindAsync(id);
    if (solicitacao == null) return NotFound();

    solicitacao.StatusManutencao = StatusManutencao.Finalizada;

    await _db.SaveChangesAsync();

    return RedirectToAction("GerenciarManutencoes", "Funcionario");
  }

  public IActionResult ListarManutencao()
  {
    // var painel = _db.Painel
    //   .Include(p => p.Manutencoes);

    // if (painel == null)
    // {
    //   return Content("<div class='alert alert-danger'>Painel não encontrado</div>");
    // }

    return View("ListarManutencao");
  }
}