using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIA_PWEB.Models.dbModels;
using PIA_PWEB.Models.ViewModels;

namespace PIA_PWEB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GestionAdminController : Controller
    {
        private readonly PiaDbContext _context;

        public GestionAdminController(PiaDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var usuarios = _context.Users
        .Select(u => new UsuarioViewModel
        {
            Id = u.Id.ToString(),
            UserName = u.UserName,
            Apellidos = u.Apellidos,
            Email = u.Email,
            PhoneNumber = u.PhoneNumber,
            Rol = _context.UserRoles
            .Where(ur => ur.UserId == u.Id)
            .Join(_context.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => r.Name)
            .Contains("Admin") ? "Administrador" : "Cliente"
        })
            .ToList();

            var viewModel = new GestionAdminViewModel
            {
                TotalUsuarios = usuarios.Count,
                Usuarios = usuarios
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EliminarUsuario(int id)
        {
            var usuario = _context.Users.Find(id);
            if (usuario != null)
            {
                _context.Users.Remove(usuario);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult HacerAdministrador(int id)
        {
            var usuario = _context.Users.Find(id);
            if (usuario != null)
            {
                var adminRole = _context.Roles.FirstOrDefault(r => r.Name == "Admin");
                if (adminRole != null)
                {
                    _context.UserRoles.Add(new IdentityUserRole<int> { UserId = usuario.Id, RoleId = adminRole.Id }); ;
                    _context.SaveChanges();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoverAdministrador(int id)
        {
            var usuario = _context.Users.Find(id);
            if (usuario != null)
            {
                var adminRole = _context.Roles.FirstOrDefault(r => r.Name == "Admin");
                if (adminRole != null)
                {

                    var userRole = _context.UserRoles.FirstOrDefault(ur => ur.UserId == id && ur.RoleId == adminRole.Id);
                    if (userRole != null)
                    {
                        _context.UserRoles.Remove(userRole);
                        _context.SaveChanges();
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }


    }
}

