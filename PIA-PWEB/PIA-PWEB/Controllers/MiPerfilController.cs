using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PIA_PWEB.Models.dbModels;
using PIA_PWEB.Models.ViewModels;
using System.Threading.Tasks;

namespace PIA_PWEB.Controllers
{
    [Authorize()]
    public class MiPerfilController : Controller
    {
        private readonly PiaDbContext _context;

        public MiPerfilController(PiaDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userId = User.Identity.Name;
            var user = _context.Users
                .FirstOrDefault(u => u.UserName == userId);

            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(ApplicationUser model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.Find(model.Id);
                if (user == null) return NotFound();

                user.Apellidos = model.Apellidos;
                user.FechaNacimiento = model.FechaNacimiento;
                user.PhoneNumber = model.PhoneNumber;
                user.FotoPerfil = model.FotoPerfil;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index", model);
        }
    }
}
