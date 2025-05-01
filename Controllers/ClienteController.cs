using projeto_apave.Models;
using projeto_apave.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace projeto_apave.Controllers
{
    [Authorize(Roles = "Cliente")]
    public class ClienteController : Controller
    {
        private readonly DbApave _db;

        public ClienteController(DbApave db)
        {
            _db = db;
        }

        public IActionResult Painel()
        {
            return View();
        }

        public IActionResult SolicitarPainel()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SolicitarPainel(string nomePainel, string descricao)
        {
            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var solicitacao = new SolicitacaoPainel
            {
                UsuarioId = usuarioId,
                NomePainel = nomePainel,
                Descricao = descricao
            };

            _db.SolicitacaoPainel.Add(solicitacao);
            await _db.SaveChangesAsync();

            TempData["Mensagem"] = "Solicitação enviada com sucesso!";
            return RedirectToAction("MinhasSolicitacoes");
        }

        public async Task<IActionResult> MinhasSolicitacoes()
        {
            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var solicitacoes = await _db.SolicitacaoPainel
                .Where(s => s.UsuarioId == usuarioId)
                .ToListAsync();

            return View(solicitacoes);
        }
    }
}