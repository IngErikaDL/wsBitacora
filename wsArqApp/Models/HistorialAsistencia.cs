
namespace wsArqApp.Models
{
    public class HistorialAsistencia
    {
        public int Id { get; set; }
        public string IdEmpleado { get; set; }
        public int NoSemana { get; set; }
        public bool Lun { get; set; }
        public bool Mar { get; set; }
        public bool Mie { get; set; }
        public bool Jue { get; set; }
        public bool Vie { get; set; }
        public bool Sab { get; set; }
        public bool SemanaCompleta { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFinal { get; set; }
    }
}