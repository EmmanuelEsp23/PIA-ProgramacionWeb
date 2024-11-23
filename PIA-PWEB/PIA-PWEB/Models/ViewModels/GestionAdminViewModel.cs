namespace PIA_PWEB.Models.ViewModels
{
    public class GestionAdminViewModel
    {
        public int TotalUsuarios { get; set; }
        public List<UsuarioViewModel> Usuarios { get; set; }
    }

    public class UsuarioViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Rol { get; set; }
    }
}
