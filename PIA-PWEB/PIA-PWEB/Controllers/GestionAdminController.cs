using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PIA_PWEB.Models.dbModels;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace PIA_PWEB.Controllers
{
    [Authorize(Roles = "Admin")] 
    public class GestionAdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly PiaDbContext _context;

        public GestionAdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, PiaDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public IActionResult Index()
        {
            var usuarios = _userManager.Users.ToList();
            return View(usuarios);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AsignarAdministrador(string userId)
        {
            var usuario = await _userManager.FindByIdAsync(userId);
            if (usuario == null)
            {
                return NotFound();
            }

            if (!await _userManager.IsInRoleAsync(usuario, "Admin"))
            {
                var result = await _userManager.AddToRoleAsync(usuario, "Admin");
                if (result.Succeeded)
                {
                    TempData["Success"] = "El usuario es ahora administrador.";
                }
                else
                {
                    TempData["Error"] = "Hubo un error al asignar el rol.";
                }
            }
            else
            {
                TempData["Error"] = "El usuario ya es administrador.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoverAdministrador(string userId)
        {
            var usuario = await _userManager.FindByIdAsync(userId);
            if (usuario == null)
            {
                return NotFound();
            }

            // Verificar si el usuario tiene el rol de administrador
            if (await _userManager.IsInRoleAsync(usuario, "Admin"))
            {
                var result = await _userManager.RemoveFromRoleAsync(usuario, "Admin");
                if (result.Succeeded)
                {
                    TempData["Success"] = "El usuario ya no tiene el rol de administrador.";
                }
                else
                {
                    TempData["Error"] = "Hubo un error al remover el rol de administrador.";
                }
            }
            else
            {
                TempData["Error"] = "El usuario no tiene el rol de administrador.";
            }

            return RedirectToAction(nameof(Index));
        }

        // Eliminar un usuario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarUsuario(string userId)
        {
            var usuario = await _userManager.FindByIdAsync(userId);
            if (usuario == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(usuario);
            if (result.Succeeded)
            {
                TempData["Success"] = "El usuario ha sido eliminado correctamente.";
            }
            else
            {
                TempData["Error"] = "Hubo un error al eliminar el usuario.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
