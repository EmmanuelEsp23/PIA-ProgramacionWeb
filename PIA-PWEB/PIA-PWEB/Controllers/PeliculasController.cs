using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PIA_PWEB.Models.dbModels;
using PIA_PWEB.Models.ViewModels;
using System.Threading.Tasks;

//Controlador de acciones CRUD con peliculas
namespace PIA_PWEB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PeliculasController : Controller
    {
        private readonly PiaDbContext _context;

        public PeliculasController(PiaDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var peliculas = _context.Peliculas
    .Include(p => p.IdCategoriaNavigation) // Usar la propiedad de navegación restante
    .Include(p => p.IdStreamingNavigation) // Usar la propiedad de navegación restante
    .Select(p => new GestionPeliViewModel
    {
        IdPelicula = p.IdPelicula,
        NombrePelicula = p.NombrePelicula,
        FechaLanzamiento = p.FechaLanzamiento,
        Director = p.Director,
        Portada = p.Portada,
        IdCategoria = p.IdCategoria,
        CategoriaNombre = p.IdCategoriaNavigation.NombreCategoria, // Cambiado
        IdStreaming = p.IdStreaming,
        StreamingNombre = p.IdStreamingNavigation.NombreStreaming  // Cambiado
    }).ToList();

            ViewBag.Categorias = _context.Categoria.ToList();
            ViewBag.Streamings = _context.Streamings.ToList();
            return View("Index", peliculas);
        }

        public IActionResult AgregarPelicula()
        {
            ViewBag.Usuario = _context.Users.ToList();
            ViewBag.Categorias = _context.Categoria.ToList();
            ViewBag.Streamings = _context.Streamings.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AgregarPelicula(GestionPeliViewModel model)
        {

            Console.WriteLine("La acción AgregarPelicula fue llamada");

            if (ModelState.IsValid)
            {
                var pelicula = new Pelicula
                {
                    NombrePelicula = model.NombrePelicula,
                    FechaLanzamiento = model.FechaLanzamiento,
                    Director = model.Director,
                    IdCategoria = model.IdCategoria,
                    Descripcion = model.Descripcion,
                    IdStreaming = model.IdStreaming,
                    Portada = model.Portada,
                    IdUsuario = model.IdUsuario
                };

                _context.Peliculas.Add(pelicula);
                Console.WriteLine($"Nueva Película: {pelicula.NombrePelicula}, ID: {pelicula.IdPelicula}");
                _context.SaveChanges();
                return RedirectToAction("Index");

            }

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
            }

            ViewBag.Usuario = _context.Users.ToList();
            ViewBag.Categorias = _context.Categoria.ToList();
            ViewBag.Streamings = _context.Streamings.ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult EditarPelicula(GestionPeliViewModel model)
        {
            if (ModelState.IsValid)
            {
                var pelicula = _context.Peliculas
    .Include(p => p.IdCategoriaNavigation)
    .Include(p => p.IdStreamingNavigation)
    .FirstOrDefault(p => p.IdPelicula == model.IdPelicula);

                if (pelicula != null)
                {
                    pelicula.NombrePelicula = model.NombrePelicula;
                    pelicula.FechaLanzamiento = model.FechaLanzamiento;
                    pelicula.Director = model.Director;
                    pelicula.IdCategoria = model.IdCategoria;
                    pelicula.IdStreaming = model.IdStreaming;
                    pelicula.Portada = model.Portada;

                    _context.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            var pelicula = _context.Peliculas.Find(id);
            if (pelicula != null)
            {
                _context.Peliculas.Remove(pelicula);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
