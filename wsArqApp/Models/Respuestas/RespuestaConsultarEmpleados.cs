using System.Collections.Generic;

namespace wsArqApp.Models.Respuestas
{
    public class RespuestaConsultarEmpleados
    {
        public RespuestaBase Respuesta { get; set; }
        public List<Empleado> ListaEmpleados { get; set; }
    }
}