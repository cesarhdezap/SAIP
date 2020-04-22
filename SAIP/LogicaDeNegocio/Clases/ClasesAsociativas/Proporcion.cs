using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.Clases
{
	public class Proporcion
	{

		public int Id { get; set; }
		public Platillo Alimento { get; set; }
		public Ingrediente Ingrediente { get; set; }
		public double Cantidad { get; set; }

		public double CalcularCosto()
		{
			return Ingrediente.Costo * Cantidad;
		}
	}
}
