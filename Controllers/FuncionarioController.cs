using projeto_apave.Models;
using projeto_apave.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace projeto_apave.Controllers
{
    [Authorize(Roles = "Funcionario")]
    public class FuncionarioController : Controller
    {
        private readonly DbApave _db;

        public FuncionarioController(DbApave db)
        {
            _db = db;
        }

        public async Task<IActionResult> GerenciarPaineis()
        {
            var paineis = await _db.Painel
                .Include(p => p.Usuario)
                .ToListAsync();
            return View(paineis);
        }

        public async Task<IActionResult> GerenciarManutencoes()
        {
            var manutencoes = await _db.SolicitacaoManutencao
                .Include(m => m.Usuario)
                .ToListAsync();

            return View(manutencoes);
        }

        public async Task<IActionResult> AprovarSolicitacoes()
        {
            var solicitacoesPendentes = await _db.SolicitacaoPainel
                .Where(s => s.Status == StatusSolicitacao.Pendente)
                .Include(s => s.Usuario)
                .ToListAsync();

            return View(solicitacoesPendentes);
        }

        [HttpPost]
        public async Task<IActionResult> Aprovar(int id)
        {
            var solicitacao = await _db.SolicitacaoPainel.FindAsync(id);
            if (solicitacao == null)
                return NotFound();

            solicitacao.Status = StatusSolicitacao.Aprovado;

            var novoPainel = new Painel
            {
                Nome = solicitacao.NomePainel,
                Descricao = solicitacao.Descricao,
                DataCriacao = DateTime.Now,
                UsuarioId = solicitacao.UsuarioId
            };

            _db.Painel.Add(novoPainel);
            await _db.SaveChangesAsync();

            return RedirectToAction("GerenciarPaineis");
        }

        [HttpPost]
        public async Task<IActionResult> Recusar(int id)
        {
            var solicitacao = await _db.SolicitacaoPainel.FindAsync(id);
            if (solicitacao == null)
                return NotFound();

            solicitacao.Status = StatusSolicitacao.Recusado;
            await _db.SaveChangesAsync();

            return RedirectToAction("AprovarSolicitacoes");
        }


    }
}
