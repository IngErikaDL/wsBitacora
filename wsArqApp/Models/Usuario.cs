
namespace wsArqApp.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string IdUsuario { get; set; }
        public string Contrasena { get; set; }
        public string NombreUsuario { get; set; }
        public string Correo { get; set; }
        public bool Login { get; set; }
        public string Exception { get; set; }
    }
}