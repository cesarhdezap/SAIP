using LogicaDeNegocio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.Clases.ClasesCompuestas
{
	public class ObjetoDeInventario
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string CodigoDeBarras { get; set; }
		public string Codigo { get; set; }
		public double Cantidad { get; set; }
		public double Costo { get; set; }
		public UnidadDeMedida UnidadDeMedida { get; set; }
		public TipoDeProducto TipoDeProducto { get; set; }
	}
}
