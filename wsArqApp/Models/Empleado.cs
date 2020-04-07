
namespace wsArqApp.Models
{
    public class Empleado
    {
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Categoria { get; set; }
        public string Imss { get; set; }
        public decimal SueldoSemanal { get; set; }
        public bool Baja { get; set; }
        public string MotivoBaja { get; set; }
        public string FechaIngreso { get; set; }
    }
}