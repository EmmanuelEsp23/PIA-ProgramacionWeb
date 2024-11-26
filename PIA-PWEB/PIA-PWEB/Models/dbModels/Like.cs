using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PIA_PWEB.Models.dbModels;

// Declarar clave primaria compuesta
[PrimaryKey(nameof(IdUsuario), nameof(IdPelicula))]
public partial class Like
{
    [Column("idUsuario")]
    public int IdUsuario { get; set; }

    [Column("idPelicula")]
    public int IdPelicula { get; set; }

    [Column("fechaLike")]
    public DateOnly FechaLike { get; set; }

    // Propiedades de navegación
    [ForeignKey(nameof(IdPelicula))]
    [InverseProperty(nameof(Pelicula.Likes))]
    public virtual Pelicula IdPeliculaNavigation { get; set; } = null!;

    [ForeignKey(nameof(IdUsuario))]
    [InverseProperty(nameof(ApplicationUser.Likes))]
    public virtual ApplicationUser IdUsuarioNavigation { get; set; } = null!;
}
