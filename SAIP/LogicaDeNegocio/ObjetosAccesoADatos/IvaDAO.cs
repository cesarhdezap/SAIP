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
		private Clases.Iva ConvertirDeDatosALogica(AccesoADatos.Iva IvaDb)
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
			AccesoADatos.Iva ivaEncontrado;
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				ivaEncontrado = context.Ivas.FirstOrDefault(iva => iva.Activo == true);
			}
			Clases.Iva ivaLogico = new Clases.Iva();
			if (ivaEncontrado != null)
			{
				ivaLogico = ConvertirDeDatosALogica(ivaEncontrado);
			}
			else
			{
				throw new InvalidOperationException("No hay ningun IVA activo.");
			}

			return ivaLogico;
		}

        public List<Clases.Iva> CargarTodos()
        {
			List<Iva> ivasDb = new List<Iva>();
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				ivasDb = context.Ivas.ToList();
			}
			return ConvertirListaDatosALogica(ivasDb);
        }

		public void Guardar(Clases.Iva iva)
		{
			Iva ivaDb = new Iva()
			{
				FechaDeInicio = iva.FechaDeInicio,
				FechaDeCreacion = iva.FechaDeCreacion,
				Activo = iva.Activo,
				Creador = iva.Creador,
				Valor = iva.Valor
			};
			

			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				if (ivaDb.Activo)
				{
					context.Ivas.ToList().ForEach(i => i.Activo = false);
					context.SaveChanges();
				}
				context.Ivas.Add(ivaDb);
				context.SaveChanges();
			}
		}

		public List<Clases.Iva> ConvertirListaDatosALogica(List<Iva> ivasDb)
		{
			List<Clases.Iva> ivas = new List<Clases.Iva>();

			foreach(Iva iva in ivasDb)
			{
				ivas.Add(ConvertirDeDatosALogica(iva));
			}

			return ivas;
		}
    }
}