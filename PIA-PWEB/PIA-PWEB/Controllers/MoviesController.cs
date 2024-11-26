using Microsoft.AspNetCore.Authorization;
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
        [Authorize()]
        public async Task<IActionResult> DejarReseña(int id, string Contenido)
        {
            var pelicula = await _context.Peliculas.FindAsync(id);
            var usuarioActual = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            if (pelicula == null || usuarioActual == null)
            {
                return NotFound("La película o el usuario no existen.");
            }

            var reseña = new Reseña
            {
                IdPelicula = id,
                IdUsuario = usuarioActual.Id,
                Contenido = Contenido,
                FechaPublicacion = DateOnly.FromDateTime(DateTime.Now),
                IdPeliculaNavigation = pelicula,
                IdUsuarioNavigation = usuarioActual
            };
            _context.Reseñas.Add(reseña);
            await _context.SaveChangesAsync();
            return RedirectToAction("Pelicula", new { id });
        }

        //Accion para dar Like
        [HttpPost]
        [Authorize()]
        public async Task<IActionResult> DarLike(int id)
        {
            var usuarioActual = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            if (usuarioActual == null)
            {
                return Unauthorized();
            }

            int idUsuario = usuarioActual.Id;
            var existeLike = await _context.Likes
                .AnyAsync(l => l.IdPelicula == id && l.IdUsuario == idUsuario);

            if (existeLike)
            {
                return BadRequest("El usuario ya ha dado like a esta película.");
            }

            var pelicula = await _context.Peliculas.FindAsync(id);

            if (pelicula == null)
            {
                return NotFound("La película no existe.");
            }

            var like = new Like
            {
                IdPelicula = id,
                IdUsuario = idUsuario,
                FechaLike = DateOnly.FromDateTime(DateTime.Now),
                IdPeliculaNavigation = pelicula, 
                IdUsuarioNavigation = usuarioActual 
            };

            _context.Likes.Add(like);
            await _context.SaveChangesAsync();

            return RedirectToAction("Pelicula", new { id });
        }

        // Acción para calificar
        [HttpPost]
        [Authorize()]
        public async Task<IActionResult> Calificar(int id, decimal puntuacion)
        {
            var pelicula = await _context.Peliculas.FindAsync(id);
            var usuarioActual = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            if (pelicula == null || usuarioActual == null)
            {
                return NotFound("La película o el usuario no existen.");
            }

            var calificacion = new Calificacione
            {
                IdPelicula = id,
                IdUsuario = usuarioActual.Id,
                Puntuacion = puntuacion,
                FechaCalificacion = DateOnly.FromDateTime(DateTime.Now),
                IdPeliculaNavigation = pelicula, 
                IdUsuarioNavigation = usuarioActual 
            };
            _context.Calificaciones.Add(calificacion);
            await _context.SaveChangesAsync();
            return RedirectToAction("Pelicula", new { id });
        }
    }
}