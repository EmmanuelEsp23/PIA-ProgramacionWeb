using Microsoft.AspNetCore.Mvc;

namespace PIA_PWEB.Models.ViewModels
{
    public class UserProfileViewModel : Controller
    {
            public int Id { get; set; }
            public string FotoPerfil { get; set; } // URL de la foto
            public string Apellidos { get; set; }
            public DateOnly FechaNacimiento { get; set; }
            public string UserName { get; set; }
            public string PhoneNumber { get; set; }
        
    }
}
