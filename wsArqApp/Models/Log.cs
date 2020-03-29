using System;

namespace wsArqApp.Models
{
    public class Log
    {		
		public string Title { get; set; }
		public string Message { get; set; }
		public DateTime FechaRegistro { get; set; }
		public string StackTrace { get; set; }
		public string Source { get; set; }
	}
}