using System.Collections.Generic;

namespace wsArqApp.Models.Respuestas
{
    public class RespuestaConsultarEmpleados : RespuestaBase
    {
        public List<Empleado> ListaEmpleados { get; set; }
    }
}