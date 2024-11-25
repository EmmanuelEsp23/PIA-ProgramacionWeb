using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIA_PWEB.Models.dbModels;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace PIA_PWEB.Controllers
{
    [Authorize(Roles = "Admin")]  
    public class GestionStreamingController : Controller
    {
        private readonly PiaDbContext _context;

        public GestionStreamingController(PiaDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CrearStreaming(Streaming streaming)
        {
            if (ModelState.IsValid)
            {
               
                var plataformaExistente = _context.Streamings
                    .FirstOrDefault(s => s.NombreStreaming == streaming.NombreStreaming);

                if (plataformaExistente != null)
                {
                    
                    ModelState.AddModelError("", "Ya existe una plataforma con ese nombre.");
                    return View(streaming);
                }

                _context.Streamings.Add(streaming);
                _context.SaveChanges();
                TempData["Success"] = "Plataforma creada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(streaming);
        }

        public IActionResult EditarStreaming(int id)
        {
            var streaming = _context.Streamings.Find(id);
            if (streaming == null)
            {
                return NotFound();
            }
            return View(streaming);
        }

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
                    TempData["Success"] = "Plataforma actualizada correctamente.";
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
                TempData["Success"] = "Plataforma eliminada correctamente.";
            }
            else
            {
                TempData["Error"] = "Plataforma no encontrada.";
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index()
        {
            var plataformas = await _context.Streamings.ToListAsync();
            return View(plataformas);
        }
    }
}
