using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PIA_PWEB.Models.dbModels;

[PrimaryKey("IdUsuario", "IdPelicula")]
public partial class Calificacione
{
    [Key]
    [Column("idUsuario")]
    public int IdUsuario { get; set; }

    [Key]
    [Column("idPelicula")]
    public int IdPelicula { get; set; }

    [Column(TypeName = "decimal(3, 1)")]
    public decimal? Puntuacion { get; set; }

    public DateOnly? FechaCalificacion { get; set; }

    [ForeignKey("IdPelicula")]
    [InverseProperty("Calificaciones")]
    public virtual Pelicula IdPeliculaNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("Calificaciones")]
    public virtual ApplicationUser IdUsuarioNavigation { get; set; } = null!;
}
