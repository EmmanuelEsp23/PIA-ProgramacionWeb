using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PIA_PWEB.Controllers
{
    public class MiPerfilController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
