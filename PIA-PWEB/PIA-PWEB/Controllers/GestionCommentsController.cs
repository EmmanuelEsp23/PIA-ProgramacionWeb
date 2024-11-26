using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIA_PWEB.Models.dbModels;

//Controlador que se encarga de cargar todas las resenas y permite a los administradores eliminarlas de ser necesario
namespace PIA_PWEB.Controllers
{

    public class GestionCommentsController : Controller
    {
        private readonly PiaDbContext _context;

        public GestionCommentsController(PiaDbContext context)
        {
            _context = context;
        }
        // GET: GestionComent
        public IActionResult Index()
        {
            // Cargar las reseñas con los nombres del usuario y película
            var reseñas = (from r in _context.Reseñas
                           join u in _context.Users on r.IdUsuario equals u.Id
                           join p in _context.Peliculas on r.IdPelicula equals p.IdPelicula
                           select new Reseña
                           {
                               IdUsuario = r.IdUsuario,
                               IdPelicula = r.IdPelicula,
                               Contenido = r.Contenido,
                               FechaPublicacion = r.FechaPublicacion,
                               NombreUsuario = u.UserName, // Agregado para mostrar en la vista
                               NombrePelicula = p.NombrePelicula // Agregado para mostrar en la vista
                           }).ToList();

            return View(reseñas);
        }

        // POST: GestionComent/Eliminar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(int idUsuario, int idPelicula)
        {
            var reseña = _context.Reseñas.FirstOrDefault(r => r.IdUsuario == idUsuario && r.IdPelicula == idPelicula);
            if (reseña == null)
            {
                return NotFound();
            }

            _context.Reseñas.Remove(reseña);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
