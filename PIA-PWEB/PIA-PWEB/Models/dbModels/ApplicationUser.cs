using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PIA_PWEB.Models.dbModels
{
    public class ApplicationUser : IdentityUser<int>
    {

        [StringLength(500)]
        public string FotoPerfil { get; set; } = null!;

        [StringLength(100)]
        public string Apellidos { get; set; } = null!;

        public DateOnly FechaNacimiento { get; set; }

        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Calificacione> Calificaciones { get; set; } = new List<Calificacione>();

        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Lista> Lista { get; set; } = new List<Lista>();

        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Pelicula> Peliculas { get; set; } = new List<Pelicula>();

        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Reseña> Reseñas { get; set; } = new List<Reseña>();
    }
}
