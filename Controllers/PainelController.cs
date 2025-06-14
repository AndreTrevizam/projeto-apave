using Microsoft.AspNetCore.Mvc;
using projeto_apave.Data;
using Microsoft.AspNetCore.Authorization;
using projeto_apave.Models;
using Microsoft.EntityFrameworkCore;

namespace projeto_apave.Controllers
{
  public class PainelController : Controller
  {
    private readonly DbApave _db;

    public PainelController(DbApave db)
    {
      _db = db;
    }

    [Authorize(Roles = "Funcionario")]
    public IActionResult EditarPainel(int id)
    {
      var painel = _db.Painel.FirstOrDefault(p => p.Id == id);
      if (painel == null)
      {
        return NotFound();
      }

      return View("EditarPainel", painel);
    }

    [HttpPost]
    [Authorize(Roles = "Funcionario")]
    public IActionResult EditarPainel(Painel painel)
    {
      var painelExistente = _db.Painel.FirstOrDefault(p => p.Id == painel.Id);
      if (painelExistente == null)
      {
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

    [Authorize(Roles = "Funcionario")]
    public IActionResult Remover(int id)
    {
      var painel = _db.Painel.FirstOrDefault(p => p.Id == id);
      if (painel == null)
      {
        return NotFound();
      }

      return View(painel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Funcionario")]
    public IActionResult RemoverPainel(int id)
    {
      Console.WriteLine("uepa");
      var painel = _db.Painel.Find(id);
      if (painel == null)
      {
        return NotFound();
      }

      _db.Painel.Remove(painel);
      _db.SaveChanges();

      return RedirectToAction("GerenciarPaineis", "Funcionario");
    }

    [HttpGet]
    [Authorize(Roles = "Funcionario")]
    public IActionResult AdicionarPeca(int painelId)
    {
      var painel = _db.Painel.Find(painelId);
      if (painel == null) return NotFound();

      var model = new PainelPecaForm
      {
        PainelId = painelId,
        PainelNome = painel.Nome,
        PecasExistentes = _db.Peca.ToList(),
        DataInstalacao = DateTime.Today // Valor padrão
      };

      return View(model);
    }

    [HttpPost]
    [Authorize(Roles = "Funcionario")]
    public async Task<ActionResult> AdicionarPeca(PainelPecaForm form)
    {
      if (string.IsNullOrEmpty(form.NovaPecaNome) && !form.PecaExistenteId.HasValue)
      {
        ModelState.AddModelError(string.Empty, "Você deve cadastrar uma nova peça OU selecionar uma existente.");
      }

      bool pecaJaExiste = await _db.PainelPeca
       .AnyAsync(pp => pp.PainelId == form.PainelId &&
                      (form.PecaExistenteId.HasValue
                          ? pp.PecaId == form.PecaExistenteId.Value
                          : pp.Peca.Nome == form.NovaPecaNome));

      if (pecaJaExiste)
      {
        ModelState.AddModelError(string.Empty, "Esta peça já está associada ao painel.");
      }

      if (ModelState.IsValid)
      {
        PainelPeca painelPeca = new();

        // Opção 1: Nova peça
        if (!string.IsNullOrEmpty(form.NovaPecaNome))
        {
          var novaPeca = new Peca
          {
            Nome = form.NovaPecaNome,
            Descricao = form.NovaPecaDescricao,
            DataCriacao = DateTime.Now
          };

          _db.Peca.Add(novaPeca);
          _db.SaveChanges();

          painelPeca.PecaId = novaPeca.Id;
        }
        // Opção 2: Peça existente
        else if (form.PecaExistenteId.HasValue)
        {
          painelPeca.PecaId = form.PecaExistenteId.Value;
        }

        // Preenche os dados da relação
        painelPeca.PainelId = form.PainelId;
        painelPeca.Quantidade = form.Quantidade;
        painelPeca.DataInstalacao = form.DataInstalacao;
        painelPeca.Status = form.Status;

        _db.PainelPeca.Add(painelPeca);
        _db.SaveChanges();

        return RedirectToAction("EditarPainel", new { id = form.PainelId });
      }

      // Recarrega dados se houver erro
      form.PecasExistentes = _db.Peca.ToList();
      return View(form);
    }

    [Authorize(Roles = "Funcionario, Cliente")]
    public IActionResult ObterInformacoesPainel(int id)
    {
      var painel = _db.Painel
          .Include(p => p.Manutencoes)
          .Include(p => p.Usuario)
          .Include(p => p.Pecas)
              .ThenInclude(pp => pp.Peca)
          .FirstOrDefault(p => p.Id == id);

      if (painel == null)
      {
        return Content("<div class='alert alert-danger'>Painel não encontrado</div>");
      }

      return PartialView("_InformacoesPainel", painel);
    }

    // GET: Mostra confirmação para remover a peça
    [HttpGet]
    public IActionResult RemoverPeca(int painelId)
    {
      var painel = _db.Painel
          .Include(p => p.Pecas)
              .ThenInclude(pp => pp.Peca)
          .FirstOrDefault(p => p.Id == painelId);

      if (painel == null)
        return NotFound();

      return View(painel);
    }

    // POST: Realiza a exclusão da peça
    [HttpPost]
    public async Task<IActionResult> RemoverPeca(int painelId, int pecaId)
    {
      var painelPeca = await _db.PainelPeca
          .FirstOrDefaultAsync(pp => pp.PainelId == painelId && pp.PecaId == pecaId);

      if (painelPeca == null)
        return NotFound();

      _db.PainelPeca.Remove(painelPeca);
      await _db.SaveChangesAsync();

      return RedirectToAction("EditarPainel", new { id = painelId });
    }
  }
}