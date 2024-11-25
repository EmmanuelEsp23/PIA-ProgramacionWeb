using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIA_PWEB.Models.dbModels;
using System.Linq;
using System.Threading.Tasks;

//Controlador para los features de busqueda y filtrado de las peliculas
namespace PIA_PWEB.Controllers
{
    public class BusquedaController : Controller
    {
        private readonly PiaDbContext _context;

        public BusquedaController(PiaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Buscar(string query)
        {
            var peliculas = await _context.Peliculas
                .Where(p => p.NombrePelicula.Contains(query) || p.Descripcion.Contains(query))
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdStreamingNavigation)
                .ToListAsync();

            return View("ResultadosBusqueda", peliculas);
        }
        public async Task<IActionResult> Filtros(
            int? idCategoria,
            int? idStreaming,
            int? anioLanzamiento,
            decimal? puntuacionMinima,
            decimal? puntuacionMaxima,
            string ordenAlfabetico
                                                )
        {
            var peliculas = _context.Peliculas.AsQueryable();

            if (idCategoria.HasValue)
            {
                peliculas = peliculas.Where(p => p.IdCategoria == idCategoria);
            }

            if (idStreaming.HasValue)
            {
                peliculas = peliculas.Where(p => p.IdStreaming == idStreaming);
            }

            if (anioLanzamiento.HasValue)
            {
                peliculas = peliculas.Where(p => p.FechaLanzamiento.Year == anioLanzamiento.Value);
            }

            if (puntuacionMinima.HasValue)
            {
                peliculas = peliculas.Where(p =>
                    p.Calificaciones.Any(c => c.Puntuacion >= puntuacionMinima.Value));
            }

            if (puntuacionMaxima.HasValue)
            {
                peliculas = peliculas.Where(p =>
                    p.Calificaciones.Any(c => c.Puntuacion <= puntuacionMaxima.Value));
            }

            if (!string.IsNullOrEmpty(ordenAlfabetico))
            {
                peliculas = ordenAlfabetico.ToLower() switch
                {
                    "asc" => peliculas.OrderBy(p => p.NombrePelicula),
                    "desc" => peliculas.OrderByDescending(p => p.NombrePelicula),
                    _ => peliculas
                };
            }

            return View("ResultadosBusqueda", await peliculas.Include(p => p.IdCategoriaNavigation).Include(p => p.IdStreamingNavigation)
            .ToListAsync());
        }
    }
}



