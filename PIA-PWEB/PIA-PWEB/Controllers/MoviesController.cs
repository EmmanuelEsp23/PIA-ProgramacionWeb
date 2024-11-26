using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIA_PWEB.Models.dbModels;
using PIA_PWEB.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace PIA_PWEB.Controllers
{
    public class MoviesController : Controller
    {
        private readonly PiaDbContext _context;

        public MoviesController(PiaDbContext context)
        {
            _context = context;
        }

        // Pantalla de inicio
        public async Task<IActionResult> Index()
        {
            var peliculas = await _context.Peliculas
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdStreamingNavigation)
                .ToListAsync();

            return View(peliculas);
        }

        // Pantalla de detalles de una película
        public async Task<IActionResult> Pelicula(int id)
        {
            var pelicula = await _context.Peliculas
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdStreamingNavigation)
                .Include(p => p.Reseñas)
                .Include(p => p.Likes)
                .Include(p => p.Calificaciones)
                .FirstOrDefaultAsync(p => p.IdPelicula == id);

            if (pelicula == null)
            {
                return NotFound();
            }

            var viewModel = new PeliculaViewModel
            {
                Pelicula = pelicula,
                Reseñas = pelicula.Reseñas.ToList(),
                PromedioCalificacion = pelicula.Calificaciones.Any() ? pelicula.Calificaciones.Average(c => c.Puntuacion) : (decimal?)null,
                TotalLikes = pelicula.Likes.Count
            };

            return View(viewModel);
        }

        // Acción para dejar una reseña
        [HttpPost]
        public async Task<IActionResult> DejarReseña(int id, Reseña nuevaReseña)
        {
            if (ModelState.IsValid)
            {
                nuevaReseña.IdPelicula = id;
                _context.Reseñas.Add(nuevaReseña);
                await _context.SaveChangesAsync();
                return RedirectToAction("Pelicula", new { id });
            }

            var pelicula = await _context.Peliculas
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdStreamingNavigation)
                .Include(p => p.Reseñas)
                .Include(p => p.Likes)
                .Include(p => p.Calificaciones)
                .FirstOrDefaultAsync(p => p.IdPelicula == id);

            if (pelicula == null)
            {
                return NotFound();
            }

            var viewModel = new PeliculaViewModel
            {
                Pelicula = pelicula,
                Reseñas = pelicula.Reseñas.ToList(),
                PromedioCalificacion = pelicula.Calificaciones.Any() ? pelicula.Calificaciones.Average(c => c.Puntuacion) : (decimal?)null,
                TotalLikes = pelicula.Likes.Count
            };

            return View("Pelicula", viewModel);
        }

        //Accion para dar Like
        [HttpPost]
        public async Task<IActionResult> DarLike(int id)
        {
            // Obtener el usuario autenticado
            var usuarioActual = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            if (usuarioActual == null)
            {
                return Unauthorized();
            }

            int idUsuario = usuarioActual.Id;

            // Verificar si el usuario ya dio like
            var existeLike = await _context.Likes
                .AnyAsync(l => l.IdPelicula == id && l.IdUsuario == idUsuario);

            if (existeLike)
            {
                return BadRequest("El usuario ya ha dado like a esta película.");
            }

            // Crear el objeto Like
            var like = new Like
            {
                IdPelicula = id,
                IdUsuario = idUsuario,
                FechaLike = DateOnly.FromDateTime(DateTime.Now)
            };

            // Agregar y guardar
            _context.Likes.Add(like);
            await _context.SaveChangesAsync();

            return RedirectToAction("Pelicula", new { id });
        }

        // Acción para calificar
        [HttpPost]
        public async Task<IActionResult> Calificar(int id, decimal puntuacion)
        {
            var calificacion = new Calificacione { IdPelicula = id, Puntuacion = puntuacion };
            _context.Calificaciones.Add(calificacion);
            await _context.SaveChangesAsync();
            return RedirectToAction("Pelicula", new { id });
        }
    }
}