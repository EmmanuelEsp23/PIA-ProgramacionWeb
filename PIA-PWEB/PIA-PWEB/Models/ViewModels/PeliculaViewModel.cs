using Microsoft.AspNetCore.Mvc;
using PIA_PWEB.Models.dbModels;
using System.Collections.Generic;

namespace PIA_PWEB.Models.ViewModels
{
    public class PeliculaViewModel : Controller
    {
        public Pelicula Pelicula { get; set; }
        public List<Reseña>? Reseñas { get; set; }
        public decimal? PromedioCalificacion { get; set; }
        public int TotalLikes { get; set; }
        public Reseña? NuevaReseña { get; set; }
    }
}
