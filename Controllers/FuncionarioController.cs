using projeto_apave.Models;
using projeto_apave.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        [Authorize(Roles = "Funcionario")]
        public async Task<IActionResult> AdicionarPeca()
        {
            var paineis = await _db.Painel.ToListAsync();
            var pecas = await _db.Peca.ToListAsync();

            ViewBag.Paineis = paineis;
            ViewBag.Pecas = pecas;

            return View();
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

            // Atualiza o status para Aprovado
            solicitacao.Status = StatusSolicitacao.Aprovado;

            // Cria o novo painel usando os dados da solicitação
            var novoPainel = new Painel
            {
                Nome = solicitacao.NomePainel,
                Descricao = solicitacao.Descricao,
                DataCriacao = DateTime.Now,
                UsuarioId = solicitacao.UsuarioId
            };

            _db.Painel.Add(novoPainel);

            await _db.SaveChangesAsync();

            return RedirectToAction("AprovarSolicitacoes");
        }

        [HttpPost]
        public async Task<IActionResult> Recusar(int id)
        {
            var solicitacao = await _db.SolicitacaoPainel.FindAsync(id);
            if (solicitacao == null)
                return NotFound();

            // Atualiza o status para Recusado
            solicitacao.Status = StatusSolicitacao.Recusado;

            // Deletar a solicitação recusada:
            _db.SolicitacaoPainel.Remove(solicitacao);

            await _db.SaveChangesAsync();

            return RedirectToAction("AprovarSolicitacoes");
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarPeca([FromBody] AdicionarPecaPainelDto dto)
        {
            var painel = await _db.Painel.FindAsync(dto.PainelId);
            var peca = await _db.Peca.FindAsync(dto.PecaId);

            if (painel == null || peca == null)
            {
                return NotFound("Painel ou Peça não encontrados.");
            }

            var painelPeca = new PainelPeca
            {
                PainelId = dto.PainelId,
                PecaId = dto.PecaId,
                Quantidade = dto.Quantidade,
                DataInstalacao = dto.DataInstalacao,
                Status = PainelPeca.StatusPainelPeca.Ativo
            };

            _db.PainelPeca.Add(painelPeca);
            await _db.SaveChangesAsync();

            return Ok(new { mensagem = "Peça adicionada ao painel com sucesso!" });
        }
    }
}
