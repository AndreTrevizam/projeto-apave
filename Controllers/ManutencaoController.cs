using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projeto_apave.Data;
using System.Security.Claims;
using projeto_apave.Models;

public class ManutencaoController : Controller
{
  private readonly DbApave _db;

  public ManutencaoController(DbApave db) {
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
  public async Task<IActionResult> SolicitarManutencao(string descricao)
  {
    var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

    var solicitacao = new SolicitacaoManutencao
    {
      UsuarioId = usuarioId,
      Descricao = descricao
    };

    _db.SolicitacaoManutencao.Add(solicitacao);
    await _db.SaveChangesAsync();
    
    return RedirectToAction("SolicitarManutencao");
  }

  public IActionResult ManutencaoInfo() {
    return View();
  }

  public async Task<IActionResult> Atender(int id) 
  {
    var solicitacao = await _db.SolicitacaoManutencao.FindAsync(id);
    if (solicitacao == null) return NotFound();

    solicitacao.StatusManutencao = StatusManutencao.Atendida;

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
}