using LogicaDeNegocio.Clases.ClasesAsociativas;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LogicaDeNegocio.Convertidores
{
	public class ConversorDeCantidadAsubtotal : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string resultado = string.Empty;

			if(value is CantidadPlatillo cantidadPlatillo)
			{
				resultado = (cantidadPlatillo.Cantidad * cantidadPlatillo.Alimento.Precio).ToString();
			} 
			else if (value is CantidadProducto cantidadProducto)
			{
				resultado = (cantidadProducto.Cantidad * cantidadProducto.Alimento.Precio).ToString();
			}

			return resultado;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
