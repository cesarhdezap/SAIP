using LogicaDeNegocio.Clases;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LogicaDeNegocio.Convertidores
{
	public class ConversorDeAlimentoAGanancia : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string resultado = string.Empty;

			if (value is Platillo platillo)
			{
				platillo.CalcularCostoDeIngredientes();
				resultado = (platillo.Precio - platillo.CostoDeIngredientes).ToString();
			}
			else if (value is Producto producto)
			{
				resultado = (producto.Precio - producto.Costo).ToString();
			}

			ConvertidorDeFlotanteADinero convertidor = new ConvertidorDeFlotanteADinero();
			resultado = (string)convertidor.Convert(resultado, typeof(string), null, null);

			return resultado;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
