using Microsoft.AspNetCore.Mvc;
using PIA_PWEB.Models;
using System.Diagnostics;
using PIA_PWEB.Data;


namespace PIA_PWEB.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult MiPerfil()
        {
            return View("MiPerfil");
        }
        public IActionResult GestionPeli()
        {
            return View("GestionPeli");
        }

        public IActionResult GestionAdmin()
        {
            return View("GestionAdmin");
        }

        public IActionResult GestionCategorias()
        {
            return View("GestionCategorias");
        }

        public IActionResult GestionComent()
        {
            return View("GestionComent");
        }

        public IActionResult Estadisticas()
        {
            return View("Estadisticas");
        }

        public IActionResult Contactanos()
        {
            return View("Contactanos");
        }

        public IActionResult Listas()
        {
            return View("Listas");
        }

        public IActionResult Buscar()
        {
            return View("Buscar");
        }
    }
      
}
