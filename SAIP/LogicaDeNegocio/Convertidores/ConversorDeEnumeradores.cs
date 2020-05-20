using LogicaDeNegocio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LogicaDeNegocio.Convertidores
{
    public class ConversorDeEnumeradores : IValueConverter
    {
		public object Convert(object valor, Type targetType, object parameter, CultureInfo culture)
		{
			string cadenaResultado = string.Empty;
			if (valor is EstadoPedido)
			{
				cadenaResultado = ((EstadoPedido)valor).ToString();
			}

			return cadenaResultado;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
