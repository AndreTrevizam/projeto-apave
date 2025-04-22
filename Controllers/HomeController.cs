using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Authorize(AuthenticationSchemes = "Autentificacao")]
public class HomeController : Controller {
  public IActionResult Index() {
    var nome = User.Identity.Name;
    ViewBag.Nome = nome;
    return View();
  }
}