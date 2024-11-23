using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PIA_PWEB.Data;
using PIA_PWEB.Models.dbModels;

namespace PIA_PWEB.Controllers
{
    public class GestionCategoriasController : Controller
    {

        private readonly PiaDbContext _context;

        public GestionCategoriasController(PiaDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            // Recupera las categorías desde la base de datos
            var categorias = _context.Categoria.ToList();

            if (categorias == null)
            {
                categorias = new List<Categorium>();
            }

            // Pasa las categorías a la vista
            return View(categorias);
        }

        // GET: Categorias/Crear
        public IActionResult CrearCategoria()
        {
            return View();
        }

        // POST: Categorias/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CrearCategoria(Categorium categoria)
        {
            if (ModelState.IsValid)
            {
                _context.Categoria.Add(categoria);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarCategoria(Categorium categoria)
        {
            if (ModelState.IsValid)
            {
                var categoriaExistente = _context.Categoria.Find(categoria.IdCategoria);
                if (categoriaExistente != null)
                {
                    categoriaExistente.NombreCategoria = categoria.NombreCategoria;
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return NotFound();
            }
            return View(categoria); // O manejar errores si no es válido.
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EliminarCategoria(int id)
        {
            var categoria = _context.Categoria.Find(id);
            if (categoria != null)
            {
                _context.Categoria.Remove(categoria);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }


    }

}
