using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LogicaDeNegocio.Convertidores
{
	public class ConvertidorDeFlotanteADinero : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string cadenaResultado = string.Empty;

			string numero;
			if(value is string valueCadena)
			{
				numero = (double.Parse(valueCadena)).ToString("0.00");
			}
			else
			{
				numero = ((double)value).ToString("0.00");
			}

			cadenaResultado = "$ " + numero;

			return cadenaResultado;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
