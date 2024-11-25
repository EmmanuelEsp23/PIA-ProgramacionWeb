using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIA_PWEB.Models.dbModels;

namespace PIA_PWEB.Controllers
{
    [Authorize(Roles = "Usuario")]
    public class ListasController : Controller
    {
        private readonly PiaDbContext _context;

        public ListasController(PiaDbContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            var UsuarioId = User.Identity.Name; 
            var listas = _context.Listas
                .Where(l => l.IdUsuarioNavigation.UserName == UsuarioId)
                .Include(l => l.IdPeliculas)
                .ToList();
            return View(listas);
        }

        public IActionResult CrearLista()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CrearLista(Lista lista)
        {
            if (ModelState.IsValid)
            {
  
                var usuario = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                if (usuario == null)
                {
                    return Unauthorized();
                }

                lista.IdUsuario = usuario.Id;
                _context.Listas.Add(lista);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(lista);
        }

        public IActionResult EditarLista(int id)
        {
            var lista = _context.Listas
                .Include(l => l.IdPeliculas)
                .FirstOrDefault(l => l.IdLista == id && l.IdUsuarioNavigation.UserName == User.Identity.Name);
            if (lista == null)
            {
                return NotFound();
            }
            return View(lista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarLista(Lista lista)
        {
            if (ModelState.IsValid)
            {
                var listaExistente = _context.Listas
                    .Include(l => l.IdPeliculas)
                    .FirstOrDefault(l => l.IdLista == lista.IdLista && l.IdUsuarioNavigation.UserName == User.Identity.Name);

                if (listaExistente == null)
                {
                    return NotFound();
                }

                listaExistente.Nombre = lista.Nombre;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(lista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EliminarLista(int id)
        {
            var lista = _context.Listas.FirstOrDefault(l => l.IdLista == id && l.IdUsuarioNavigation.UserName == User.Identity.Name);
            if (lista != null)
            {
                _context.Listas.Remove(lista);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AgregarPelicula(int listaId, int peliculaId)
        {
            var lista = _context.Listas
                .Include(l => l.IdPeliculas)
                .FirstOrDefault(l => l.IdLista == listaId && l.IdUsuarioNavigation.UserName == User.Identity.Name);
            if (lista != null)
            {
                var pelicula = _context.Peliculas.FirstOrDefault(p => p.IdPelicula == peliculaId);
                if (pelicula != null && !lista.IdPeliculas.Contains(pelicula))
                {
                    lista.IdPeliculas.Add(pelicula);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction(nameof(EditarLista), new { id = listaId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EliminarPelicula(int listaId, int peliculaId)
        {
            var lista = _context.Listas
                .Include(l => l.IdPeliculas)
                .FirstOrDefault(l => l.IdLista == listaId && l.IdUsuarioNavigation.UserName == User.Identity.Name);
            if (lista != null)
            {
                var pelicula = lista.IdPeliculas.FirstOrDefault(p => p.IdPelicula == peliculaId);
                if (pelicula != null)
                {
                    lista.IdPeliculas.Remove(pelicula);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction(nameof(EditarLista), new { id = listaId });
        }

        public IActionResult VerLista(int id)
        {
            var lista = _context.Listas
                .Include(l => l.IdPeliculas)
                .FirstOrDefault(l => l.IdLista == id && l.IdUsuarioNavigation.UserName == User.Identity.Name);
            if (lista == null)
            {
                return NotFound();
            }
            return View(lista);
        }
    }
}
