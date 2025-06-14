using Microsoft.AspNetCore.Mvc;
using projeto_apave.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;


public class UsuarioController : Controller {
  private readonly DbApave _db;

  public UsuarioController(DbApave db) {
    _db = db;
  }

  [HttpGet]
  public IActionResult Cadastrar(TipoUsuario? tipo = null) {
    return View();
  }

  [HttpPost]
  public async Task<IActionResult> Cadastrar(Usuario usuario) {
    if (await _db.Usuario.AnyAsync(u => u.Email == usuario.Email)) {
      ModelState.AddModelError("Email", "Este e-mail já está em uso.");
      return View(usuario);
    }

    if (ModelState.IsValid) {
      usuario.Tipo = usuario.EhFuncionario ? TipoUsuario.Funcionario : TipoUsuario.Cliente;
      usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
      _db.Usuario.Add(usuario);
      await _db.SaveChangesAsync();
      return RedirectToAction("Login");
    }

    return View(usuario);
  }

  [HttpGet]
  public IActionResult Login() {
    return View();
  }

  [HttpPost]

  public async Task<IActionResult> Login(string email, string senha) {
    var usuario = await _db.Usuario.FirstOrDefaultAsync(u => u.Email == email);

    if (usuario != null && BCrypt.Net.BCrypt.Verify(senha, usuario.Senha)) {
      var claims = new List<Claim> {
        new Claim(ClaimTypes.Name, usuario.Nome),
        new Claim(ClaimTypes.Email, usuario.Email),
        new Claim(ClaimTypes.Role, usuario.Tipo.ToString()),
        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())
      };

      var identity = new ClaimsIdentity(claims, "Autentificacao");
      var principal = new ClaimsPrincipal(identity);

      await HttpContext.SignInAsync("Autentificacao", principal);

      return RedirectToAction("Index", "Home");
    }

    ViewBag.Erro = "Email ou senha inválidos.";
    return View();
  }

  public IActionResult Logout() {
    HttpContext.Session.Clear();
    return RedirectToAction("Login");
  }
}