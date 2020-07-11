using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.Servicios
{
	public static class ServiciosDeIO
	{
		public static byte[] CargarBytesDeArchivo(string direccionDeArchivo)
		{
			byte[] resultado = null;

			if (!string.IsNullOrEmpty(direccionDeArchivo) && File.Exists(direccionDeArchivo))
			{
				resultado = File.ReadAllBytes(direccionDeArchivo);
			}

			return resultado;
		}
	}
}
