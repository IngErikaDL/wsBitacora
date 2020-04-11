
namespace wsArqApp.Models
{
    public class Imagen
    {
        public int Id { get; set; }
        public int IdEmpleado { get; set; }
        public int IdTipoImagen { get; set; }
        public string NombreImagen { get; set; }
        public string RutaImagen { get; set; }
        public byte[] ImagenBytes { get; set; }
    }
}