using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIA_PWEB.Models.dbModels;
using PIA_PWEB.Data;

namespace PIA_PWEB.Controllers
{
    public class GestionStreamingController : Controller
    {
        private readonly PiaDbContext _context;

        public GestionStreamingController(PiaDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var serviciosStreaming = _context.Streamings.ToList();
            return View(serviciosStreaming);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CrearStreaming(Streaming streaming)
        {
            if (ModelState.IsValid)
            {
                _context.Streamings.Add(streaming);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(streaming);
        }

        // GET: Muestra un modal (datos cargados dinámicamente en la vista)
        public IActionResult EditarStreaming(int id)
        {
            var streaming = _context.Streamings.Find(id);
            if (streaming == null)
            {
                return NotFound();
            }
            return View(streaming);
        }

        // POST: Actualiza el servicio
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarStreaming(Streaming streaming)
        {
            if (ModelState.IsValid)
            {
                var streamingExistente = _context.Streamings.Find(streaming.IdStreaming);
                if (streamingExistente != null)
                {
                    streamingExistente.NombreStreaming = streaming.NombreStreaming;
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(streaming);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EliminarStreaming(int id)
        {
            var streaming = _context.Streamings.Find(id);
            if (streaming != null)
            {
                _context.Streamings.Remove(streaming);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
