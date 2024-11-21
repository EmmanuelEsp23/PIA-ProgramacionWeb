using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PIA_PWEB.Models.dbModels;

[Table("listas")]
public partial class Lista
{
    [Key]
    [Column("idLista")]
    public int IdLista { get; set; }

    [Column("idUsuario")]
    public int IdUsuario { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("Lista")]
    public virtual ApplicationUser IdUsuarioNavigation { get; set; } = null!;

    [ForeignKey("IdLista")]
    [InverseProperty("IdLista")]
    public virtual ICollection<Pelicula> IdPeliculas { get; set; } = new List<Pelicula>();
}
