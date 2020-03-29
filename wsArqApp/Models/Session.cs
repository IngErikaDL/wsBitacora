
namespace wsArqApp.Models
{
    public class Session
    {
        public int Id { get; set; }
        public string Oficina { get; set; }
        public string Rfc { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public string Prefijo { get; set; }
        public string VersionApp { get; set; }
        public int FolioAlta { get; set; }
        public int FolioEmpleado { get; set; }
        public string CorreoJefe { get; set; }
        public string CorreoContador { get; set; }
    }
}