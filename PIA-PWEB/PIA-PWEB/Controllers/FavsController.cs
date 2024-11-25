using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIA_PWEB.Models.dbModels;
using System.Threading.Tasks;

//Controlador de las acciones relacionadas con el feature de favoritos o likes
namespace PIA_PWEB.Controllers
{
    public class FavsController : Controller
    {
        private readonly PiaDbContext _context;

        public FavsController(PiaDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> MarcarFavorito(int idPelicula, int idUsuario)
        {
            var like = new Like { IdPelicula = idPelicula, IdUsuario = idUsuario, FechaLike = DateOnly.FromDateTime(DateTime.Now) };
            _context.Likes.Add(like);
            await _context.SaveChangesAsync();
            return RedirectToAction("Detalles", "Peliculas", new { id = idPelicula });
        }
        public async Task<IActionResult> Favoritos(int idUsuario)
        {
            var favoritos = await _context.Likes
                .Where(l => l.IdUsuario == idUsuario)
                .Include(l => l.IdPeliculaNavigation)
                .ToListAsync();

            return View(favoritos);
        }
    }
}

