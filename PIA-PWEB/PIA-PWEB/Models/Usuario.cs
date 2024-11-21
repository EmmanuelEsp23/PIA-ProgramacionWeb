namespace PIA_WEB.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string? Nombre { get; set; } // Nullable
        public string? Contrasena { get; set; } // Nullable
        public string? FotoPerfil { get; set; } // Nullable
        public int? IdRol { get; set; } // Nullable
        public string? Apellidos { get; set; } // Nullable
        public DateTime? FechaNacimiento { get; set; } // Nullable
        public string? Tel { get; set; } // Nullable
        public string? Email { get; set; } // Nullable
    }
}

