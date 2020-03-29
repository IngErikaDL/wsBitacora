using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsArqApp.Models
{
	public class EmpleadoAsistencia
	{
		public string IdEmpleado { get; set; }
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public string Categoria { get; set; }
		public string Imss { get; set; }
		public decimal SueldoSemanal { get; set; }
		public bool Baja { get; set; }
		public string MotivoBaja { get; set; }
		public string FechaIngreso { get; set; }
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
		public int DiasTrabajados { get; set; }
	}
}