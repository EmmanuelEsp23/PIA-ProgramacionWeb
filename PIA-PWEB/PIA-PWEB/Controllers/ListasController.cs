using Microsoft.AspNetCore.Mvc;

namespace PIA_PWEB.Controllers
{
    public class ListasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
