using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PIA_PWEB.Models.dbModels;

[PrimaryKey("IdUsuario", "IdPelicula")]
public partial class Reseña
{
    [Key]
    [Column("idUsuario")]
    public int IdUsuario { get; set; }

    [Key]
    [Column("idPelicula")]
    public int IdPelicula { get; set; }

    [Column(TypeName = "text")]
    public string? Contenido { get; set; }

    public DateOnly? FechaPublicacion { get; set; }

    [ForeignKey("IdPelicula")]
    [InverseProperty("Reseñas")]
    public virtual Pelicula IdPeliculaNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("Reseñas")]
    public virtual ApplicationUser IdUsuarioNavigation { get; set; } = null!;

    public string NombreUsuario { get; set; } // Nombre del usuario
    public string NombrePelicula { get; set; } // Nombre de la película
}
