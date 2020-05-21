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
    public class ConversorDeListaCantidadProductos : IValueConverter
	{
		public object Convert(object valor, Type targetType, object parameter, CultureInfo culture)
		{
			string cadenaResultado = string.Empty;
			List<CantidadProducto> cantidadProductos = (List<CantidadProducto>)valor;
			foreach(CantidadProducto cantidadProducto in cantidadProductos)
			{
				cadenaResultado += cantidadProducto.Alimento.ToString();
				cadenaResultado += Environment.NewLine;
				cadenaResultado = "Cantidad: " + cantidadProducto.Cantidad;
			}

			return cadenaResultado;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
