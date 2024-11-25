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

        public async Task<IActionResult> Resenas()
        {
            var reseñas = await _context.Reseñas
                .Include(r => r.IdUsuarioNavigation)
                .Include(r => r.IdPeliculaNavigation)
                .ToListAsync();

            return View(reseñas);
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int idUsuario, int idPelicula)
        {
            var reseña = await _context.Reseñas
                .FirstOrDefaultAsync(r => r.IdUsuario == idUsuario && r.IdPelicula == idPelicula);

            if (reseña == null)
            {
                return NotFound();
            }

            _context.Reseñas.Remove(reseña);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Resenas));
        }
    }
}
