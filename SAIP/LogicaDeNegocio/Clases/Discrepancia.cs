using LogicaDeNegocio.Clases.ClasesCompuestas;
using LogicaDeNegocio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.Clases
{
	public class Discrepancia
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string Codigo { get; set; }
		public double CantidadRegistrada { get; set; }
		public double CantidadEsperada { get; set; }
		public double Costo { get; set; }
		public UnidadDeMedida UnidadDeMedida { get; set; }
		public TipoDeProducto TipoDeProducto { get; set; }
	}
}
