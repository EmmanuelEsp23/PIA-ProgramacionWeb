using Microsoft.AspNetCore.Mvc;

namespace PIA_PWEB.Models.ViewModels
{
    public class ReseñaViewModel
    {
        public int IdUsuario { get; set; }
        public int IdPelicula { get; set; }
        public string? Contenido { get; set; }
        public DateOnly? FechaPublicacion { get; set; }
        public string UserName { get; set; } // Nombre del usuario
        public string NombrePelicula { get; set; } // Nombre de la película
    }
}
