using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PIA_PWEB.Models.dbModels;

[PrimaryKey("IdUsuario", "IdPelicula")]
public partial class Like
{
    [Key]
    [Column("idUsuario")]
    public int IdUsuario { get; set; }

    [Key]
    [Column("idPelicula")]
    public int IdPelicula { get; set; }

    public DateOnly? FechaLike { get; set; }

    [ForeignKey("IdPelicula")]
    [InverseProperty("Likes")]
    public virtual Pelicula IdPeliculaNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("Likes")]
    public virtual ApplicationUser IdUsuarioNavigation { get; set; } = null!;
}
