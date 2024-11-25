using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIA_PWEB.Models.dbModels;
using System.Linq;
using System.Threading.Tasks;

//Controlador que monitorea las peliculas mas populares y mejor calificadas para recomendar
namespace PIA_PWEB.Controllers
{
    public class EstadisticasPeliculasController : Controller
    {
        private readonly PiaDbContext _context;

        public EstadisticasPeliculasController(PiaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Populares()
        {
            var populares = await _context.Peliculas
                .OrderByDescending(p => p.Likes.Count)
                .Take(10)
                .ToListAsync();

            return View(populares);
        }

        public async Task<IActionResult> MejorCalificadas()
        {
            var mejorCalificadas = await _context.Peliculas
                .OrderByDescending(p => p.Calificaciones.Average(c => c.Puntuacion))
                .Take(10)
                .ToListAsync();

            return View(mejorCalificadas);
        }
    }
}

