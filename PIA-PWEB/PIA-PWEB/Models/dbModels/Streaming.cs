using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PIA_PWEB.Models.dbModels;

[Table("Streaming")]
public partial class Streaming
{
    [Key]
    [Column("idStreaming")]
    public int IdStreaming { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string NombreStreaming { get; set; } = null!;

    [InverseProperty("IdStreamingNavigation")]
    public virtual ICollection<Pelicula> Peliculas { get; set; } = new List<Pelicula>();
}
