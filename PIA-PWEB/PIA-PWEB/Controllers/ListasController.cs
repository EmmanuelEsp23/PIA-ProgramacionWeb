using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PIA_PWEB.Controllers
{
    public class ListasController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
