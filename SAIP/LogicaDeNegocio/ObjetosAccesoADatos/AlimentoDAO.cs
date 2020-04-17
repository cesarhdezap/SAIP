using AccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.ObjetosAccesoADatos
{
	public class AlimentoDAO
	{
		private Clases.Alimento ConvertirAlimentoDeAccesoADatosAAlimentoDeLogica(AccesoADatos.Alimento AlimentoDb)
		{
			Clases.Alimento alimentoConvertido = new Clases.Alimento()
			{
				Id = AlimentoDb.Id,
				Nombre = AlimentoDb.Nombre,
				FechaDeCreacion = AlimentoDb.FechaDeCreacion,
				FechaDeModificacion = AlimentoDb.FechaDeModificacion,
				Activo = AlimentoDb.Activo,
				Precio = AlimentoDb.Precio,
				Codigo = AlimentoDb.Codigo
			};

			ProporcionDAO proporcionDAO = new ProporcionDAO();
			alimentoConvertido.Proporciones = proporcionDAO.CargarProporcionesPorIdAlimento(AlimentoDb.Id);

			return alimentoConvertido;
		}



		private List<Clases.Alimento> ConvertirListaDeAlimentosDeAccesoADatosAListaDeAlimentosDeLogica(List<Alimento> AlimentosDb)
		{
			List<Clases.Alimento> alimentosResultado = new List<Clases.Alimento>();

			foreach (Alimento alimento in AlimentosDb)
			{
				alimentosResultado.Add(ConvertirAlimentoDeAccesoADatosAAlimentoDeLogica(alimento));
			}

			return alimentosResultado;
		}

		public List<Clases.Alimento> CargarTodos()
		{
			List<Alimento> alimentosDb = new List<Alimento>();
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				alimentosDb = context.Alimentoes.ToList();
			}

			List<Clases.Alimento> alimentosResultado = new List<Clases.Alimento>();
			alimentosResultado = ConvertirListaDeAlimentosDeAccesoADatosAListaDeAlimentosDeLogica(alimentosDb);
			return alimentosResultado;
		}

		public Clases.Alimento CargarAlimentoPorId(int Id)
		{
			Alimento alimentoDb = new Alimento();
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				alimentoDb = context.Alimentoes.Find(Id);
			}
			Clases.Alimento ingredienteResultado = ConvertirAlimentoDeAccesoADatosAAlimentoDeLogica(alimentoDb);

			return ingredienteResultado;
		}
	}
}
