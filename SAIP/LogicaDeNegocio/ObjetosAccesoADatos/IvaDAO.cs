using AccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.ObjetosAccesoADatos
{
	public class IvaDAO
	{
		private Clases.Iva ConvertireIvaDeAccesoADatosAIvaDeLogica(AccesoADatos.Iva IvaDb)
		{
			Clases.Iva ivaConvertido = new Clases.Iva()
			{
				Id = IvaDb.Id,
				Creador = IvaDb.Creador,
				Activo = IvaDb.Activo,
				FechaDeCreacion = IvaDb.FechaDeCreacion,
				FechaDeInicio = IvaDb.FechaDeInicio,
				Valor = IvaDb.Valor,
			};

			return ivaConvertido;
		}

		public Clases.Iva CargarIvaActual()
		{
			List<AccesoADatos.Iva> ivaContext;
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				ivaContext = context.Ivas.ToList();
			}
			AccesoADatos.Iva ivaEncontrado = ivaContext.FirstOrDefault(iva => iva.Activo == true);
			Clases.Iva ivaLogico = ConvertireIvaDeAccesoADatosAIvaDeLogica(ivaEncontrado);
			return ivaLogico;
		}
	}
}