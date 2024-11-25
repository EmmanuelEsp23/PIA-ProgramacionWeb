using Microsoft.AspNetCore.Mvc;
using PIA_PWEB.Models;
using System.Diagnostics;


namespace PIA_PWEB.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Dashboard()
        {
            return View("dashboard");
        }
    }
}
