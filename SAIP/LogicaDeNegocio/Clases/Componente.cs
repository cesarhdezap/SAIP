using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.Clases
{
	public class Componente
	{
		public Ingrediente Compuesto { get; set; }
		public Ingrediente Ingrediente { get; set; }
		public Double Cantidad { get; set; }

		public double CalcularCosto()
		{
			return Ingrediente.CalcularCosto() * Cantidad;
		}
	}
}
