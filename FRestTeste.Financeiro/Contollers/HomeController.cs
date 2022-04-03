using Microsoft.AspNetCore.Mvc;

namespace FRestTeste.Financeiro.Contollers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
