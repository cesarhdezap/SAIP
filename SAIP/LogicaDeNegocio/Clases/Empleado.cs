using LogicaDeNegocio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.Clases
{
	public class Empleado
	{
		public int Id { get; set; }
		public string NombreDeUsuario { get; set; }
		public string Contraseña { get; set; }
		public string CorreoElectronico { get; set; }
		public string Nombre { get; set; }
		public DateTime FechaDeCreacion { get; set; }
		public DateTime FechaDeModicacion { get; set; }
		public string Creador { get; set; }
		public bool Activo { get; set; }
		public TipoDeEmpleado TipoDeEmpleado { get; set; }



	}
}
