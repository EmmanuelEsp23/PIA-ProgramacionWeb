using PIA_PWEB.Models.dbModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIA_PWEB.Models.ViewModels
{
    public class GestionPeliViewModel
    {
        [Key]
        public int IdPelicula { get; set; }
        public string NombrePelicula { get; set; }
        public DateOnly FechaLanzamiento { get; set; }
        public string Director { get; set; }
        public string Portada { get; set; }

        public int IdCategoria { get; set; }
        public string CategoriaNombre { get; set; } // Nombre de la categoría

        public int IdStreaming { get; set; }
        public string StreamingNombre { get; set; } // Nombre de la plataforma

        public int IdUsuario { get; set; }

        public string Descripcion { get; set; }

        [ForeignKey("IdCategoria")]
        [InverseProperty("Peliculas")]
        public virtual Categorium IdCategoriaNavigation { get; set; } = null!;


        [ForeignKey("IdStreaming")]
        [InverseProperty("Peliculas")]
        public virtual Streaming IdStreamingNavigation { get; set; } = null!;

        [ForeignKey("IdUsuario")]
        [InverseProperty("Peliculas")]
        public virtual ApplicationUser IdUsuarioNavigation { get; set; } = null!;

    }
}
