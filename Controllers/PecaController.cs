using Microsoft.AspNetCore.Mvc;
using projeto_apave.Data;
using Microsoft.AspNetCore.Authorization;
using projeto_apave.Models;
using Microsoft.EntityFrameworkCore;

namespace projeto_apave.Controllers
{
    public class PecaController : Controller
    {
        private readonly DbApave _db;

        public PecaController(DbApave db)
        {
            _db = db;
        }

        [Authorize(Roles = "Funcionario")]
        public IActionResult CadastrarPeca()
        {
            return View("CadastrarPeca");
        }

        [HttpPost]
        [Authorize(Roles = "Funcionario")]
        public IActionResult CadastrarPeca(Peca peca)
        {
            var pecaNova = new Peca
            {
                Nome = peca.Nome,
                Descricao = peca.Descricao,
                DataCriacao = peca.DataCriacao,
                DataAlteracao = DateTime.Now
            };
            _db.Peca.Add(pecaNova);
            _db.SaveChanges();
            return RedirectToAction("GerenciarPecas", "Funcionario");
        }

        [Authorize(Roles = "Funcionario")]
        public IActionResult EditarPeca(int id)
        {
            var peca = _db.Peca.FirstOrDefault(p => p.Id == id);
            if (peca == null)
            {
                return NotFound();
            }

            return View("EditarPeca", peca);
        }
        [HttpPost]
        [Authorize(Roles = "Funcionario")]
        public IActionResult EditarPeca(Peca peca)
        {
            var pecaExistente = _db.Peca.FirstOrDefault(p => p.Id == peca.Id);
            if (pecaExistente == null)
            {
                return NotFound();
            }

            pecaExistente.Nome = peca.Nome;
            pecaExistente.Descricao = peca.Descricao;
            pecaExistente.DataAlteracao = DateTime.Now;

            _db.SaveChanges();
            return RedirectToAction("GerenciarPecas", "Funcionario");
        }

        [Authorize(Roles = "Funcionario")]
        public IActionResult Remover(int id)
        {
            var peca = _db.Peca.FirstOrDefault(p => p.Id == id);
            if (peca == null)
            {
                return NotFound();
            }

            return View(peca);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Funcionario")]
        public IActionResult RemoverPeca(int id)
        {
            Console.WriteLine("APAGUE");
            var peca = _db.Peca.Find(id);
            if (peca == null)
            {
                return NotFound();
            }

            _db.Peca.Remove(peca);
            _db.SaveChanges();

            return RedirectToAction("GerenciarPecas", "Funcionario");
        }
    }
}