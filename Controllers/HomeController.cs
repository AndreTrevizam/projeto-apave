using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[Authorize(AuthenticationSchemes = "Autentificacao")]
public class HomeController : Controller {
  public IActionResult Index() {
    var nome = User.Identity.Name;
    var role = User.FindFirst(ClaimTypes.Role)?.Value;

    if (role == "Funcionario") {
      return RedirectToAction("Funcionario");
    } else if (role == "Cliente") {
      return RedirectToAction("Cliente");
    }

    return View("Erro");
  }

  [Authorize(Roles = "Cliente")]
  public ActionResult Cliente() {
    return View();
  }

  [Authorize(Roles = "Funcionario")]
  public ActionResult Funcionario() {
    return View();
  }
}