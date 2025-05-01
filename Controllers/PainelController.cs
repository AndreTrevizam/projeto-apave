using Microsoft.AspNetCore.Mvc;
using projeto_apave.Data;
using Microsoft.AspNetCore.Authorization;
using projeto_apave.Models;

namespace projeto_apave.Controllers
{
  [Authorize(Roles = "Funcionario")]
  public class PainelController : Controller
  {
    private readonly DbApave _db;

    public PainelController(DbApave db)
    {
      _db = db;
    }

    public IActionResult EditarPainel(int id)
    {
      var painel = _db.Painel.FirstOrDefault(p => p.Id == id);
      if (painel == null) {
        return NotFound();
      }
      
      return View("EditarPainel", painel);
    }

    [HttpPost]
    public IActionResult EditarPainel(Painel painel)
    {
      var painelExistente = _db.Painel.FirstOrDefault(p => p.Id == painel.Id);
      if (painelExistente == null) {
        return NotFound();
      }

      painelExistente.Nome = painel.Nome;
      painelExistente.Descricao = painel.Descricao;
      painelExistente.Altura = painel.Altura;
      painelExistente.Largura = painel.Largura;
      painelExistente.Comprimento = painel.Comprimento;
      
      _db.SaveChanges();
      return RedirectToAction("GerenciarPaineis", "Funcionario");

    }

    public IActionResult Remover(int id) {
      var painel = _db.Painel.FirstOrDefault(p => p.Id == id);
      if (painel == null) {
        return NotFound();
      }

      return View(painel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult RemoverPainel(int id) {
      var painel = _db.Painel.Find(id);
      if (painel == null) {
        return NotFound();
      }

      _db.Painel.Remove(painel);
      _db.SaveChanges();

      return RedirectToAction("GerenciarPaineis", "Funcionario");
    }
  }
}