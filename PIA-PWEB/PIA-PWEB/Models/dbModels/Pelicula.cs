using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PIA_PWEB.Models.dbModels;

[Table("Pelicula")]
public partial class Pelicula
{
    [Key]
    [Column("idPelicula")]
    public int IdPelicula { get; set; }

    [Column("idUsuario")]
    public int IdUsuario { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string NombrePelicula { get; set; } = null!;

    [Column("idCategoria")]
    public int IdCategoria { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Portada { get; set; }

    [Column(TypeName = "text")]
    public string Descripcion { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Director { get; set; } = null!;

    public DateOnly FechaLanzamiento { get; set; }

    [Column("idStreaming")]
    public int IdStreaming { get; set; }


    [InverseProperty("IdPeliculaNavigation")]
    public virtual ICollection<Calificacione> Calificaciones { get; set; } = new List<Calificacione>();

    [ForeignKey("IdCategoria")]
    [InverseProperty("Peliculas")]
    public virtual Categorium IdCategoriaNavigation { get; set; } = null!;

    
    [ForeignKey("IdStreaming")]
    [InverseProperty("Peliculas")]
    public virtual Streaming IdStreamingNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("Peliculas")]
    public virtual ApplicationUser IdUsuarioNavigation { get; set; } = null!;

    [InverseProperty("IdPeliculaNavigation")]
    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    [InverseProperty("IdPeliculaNavigation")]
    public virtual ICollection<Reseña> Reseñas { get; set; } = new List<Reseña>();

    [ForeignKey("IdPelicula")]
    [InverseProperty("IdPeliculas")]
    public virtual ICollection<Lista> IdLista { get; set; } = new List<Lista>();
}
