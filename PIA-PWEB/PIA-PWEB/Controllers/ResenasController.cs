using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIA_PWEB.Models.dbModels;
using System.Threading.Tasks;

//Controlador encargado de la creacion de reseñas en para las peliculas
namespace PIA_PWEB.Controllers
{
    public class ResenasController : Controller
    {
        private readonly PiaDbContext _context;

        public ResenasController(PiaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Resenas(int idPelicula)
        {
            var reseñas = await _context.Reseñas
                .Where(r => r.IdPelicula == idPelicula)
                .Include(r => r.IdUsuarioNavigation)
                .ToListAsync();

            return View(reseñas);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarResena(Reseña nuevaReseña)
        {
            if (ModelState.IsValid)
            {
                nuevaReseña.FechaPublicacion = DateOnly.FromDateTime(DateTime.Now);
                _context.Reseñas.Add(nuevaReseña);
                await _context.SaveChangesAsync();
                return RedirectToAction("Detalles", "Peliculas", new { id = nuevaReseña.IdPelicula });
            }
            return View(nuevaReseña);
        }
    }
}
