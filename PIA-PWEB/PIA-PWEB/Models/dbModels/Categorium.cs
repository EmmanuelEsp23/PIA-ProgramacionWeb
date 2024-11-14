using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PIA_PWEB.Models.dbModels;

public partial class Categorium
{
    [Key]
    [Column("idCategoria")]
    public int IdCategoria { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string NombreCategoria { get; set; } = null!;

    [InverseProperty("IdCategoriaNavigation")]
    public virtual ICollection<Pelicula> Peliculas { get; set; } = new List<Pelicula>();
}
