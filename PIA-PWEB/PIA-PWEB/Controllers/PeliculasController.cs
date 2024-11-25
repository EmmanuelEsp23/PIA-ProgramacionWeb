using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PIA_PWEB.Models.dbModels;
using System.Threading.Tasks;

//Controlador de acciones CRUD con peliculas
namespace PIA_PWEB.Controllers
{
    public class PeliculasController : Controller
    {
        private readonly PiaDbContext _context;

        public PeliculasController(PiaDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Index()
        {
            var peliculas = await _context.Peliculas
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdStreamingNavigation)
                .Include(p => p.IdUsuarioNavigation)
                .ToListAsync();
            return View(peliculas);
        }

        public async Task<IActionResult> Detalles(int id)
        {
            var pelicula = await _context.Peliculas
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdStreamingNavigation)
                .Include(p => p.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdPelicula == id);

            if (pelicula == null)
            {
                return NotFound();
            }
            return View(pelicula);
        }

        public IActionResult Crear()
        {
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "NombreCategoria");
            ViewData["IdStreaming"] = new SelectList(_context.Streamings, "IdStreaming", "NombreStreaming");
            ViewData["IdUsuario"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Pelicula pelicula)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pelicula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "NombreCategoria", pelicula.IdCategoria);
            ViewData["IdStreaming"] = new SelectList(_context.Streamings, "IdStreaming", "NombreStreaming", pelicula.IdStreaming);
            ViewData["IdUsuario"] = new SelectList(_context.Users, "Id", "UserName", pelicula.IdUsuario);
            return View(pelicula);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "NombreCategoria", pelicula.IdCategoria);
            ViewData["IdStreaming"] = new SelectList(_context.Streamings, "IdStreaming", "NombreStreaming", pelicula.IdStreaming);
            ViewData["IdUsuario"] = new SelectList(_context.Users, "Id", "UserName", pelicula.IdUsuario);
            return View(pelicula);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Pelicula pelicula)
        {
            if (id != pelicula.IdPelicula)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pelicula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeliculaExists(pelicula.IdPelicula))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "NombreCategoria", pelicula.IdCategoria);
            ViewData["IdStreaming"] = new SelectList(_context.Streamings, "IdStreaming", "NombreStreaming", pelicula.IdStreaming);
            ViewData["IdUsuario"] = new SelectList(_context.Users, "Id", "UserName", pelicula.IdUsuario);
            return View(pelicula);
        }

        public async Task<IActionResult> Eliminar(int id)
        {
            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula == null)
            {
                return NotFound();
            }

            _context.Peliculas.Remove(pelicula);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeliculaExists(int id)
        {
            return _context.Peliculas.Any(e => e.IdPelicula == id);
        }
    }
}
