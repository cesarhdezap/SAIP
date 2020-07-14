using AccesoADatos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LogicaDeNegocio.Convertidores
{
    public class ConversorDeCuentaClientes : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
			string resultado = string.Empty;

            if(value is List<Cliente> clientes)
            {
                resultado = clientes.FirstOrDefault().ToString();
            }

            return resultado;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
