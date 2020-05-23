using AccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.ObjetosAccesoADatos
{
	public class ProporcionDAO
	{
		public List<Clases.Proporcion> CargarProporcionesPorIdPlatillo(int AlimentoId)
		{
			List<Clases.Proporcion> proporciones = new List<Clases.Proporcion>();
			List<PlatilloIngrediente> proporcionesDb = new List<PlatilloIngrediente>();
			IngredienteDAO ingredienteDAO = new IngredienteDAO();
			PlatilloDAO alimentoDAO = new PlatilloDAO();
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				proporcionesDb = context.PlatilloIngrediente.ToList().FindAll(alimento => alimento.Alimento.Id == AlimentoId).ToList();
				proporciones = ConvertirListaDeProporcionesDeAccesoADatosAListaDeProporcionesDeLogica(proporcionesDb);
			}

			return proporciones;
		}
		public List<AccesoADatos.PlatilloIngrediente> ConvertirListaLogicaAListaDeDB(List<Clases.Proporcion> Proporciones)
		{
			List<AccesoADatos.PlatilloIngrediente> proporcionesConvertidas = new List<PlatilloIngrediente>();

			foreach (Clases.Proporcion proporcion in Proporciones)
			{
				proporcionesConvertidas.Add(ConvertirLogicaADb(proporcion));
			}

			return proporcionesConvertidas;
		}
		
		public List<AccesoADatos.PlatilloIngrediente> ConvertirListaDeProporcionesDeLogicaAListaDeProporcionesDeAccesoADatosParaEdicion(List<Clases.Proporcion> Proporciones)
		{
			List<AccesoADatos.PlatilloIngrediente> proporcionesConvertidas = new List<PlatilloIngrediente>();

			foreach (Clases.Proporcion proporcion in Proporciones)
			{
				proporcionesConvertidas.Add(ConvertirProporcionDeLogicaAProporcionDeAccesoADatosParaEdicion(proporcion));
			}

			return proporcionesConvertidas;
		}

		private List<Clases.Proporcion> ConvertirListaDeProporcionesDeAccesoADatosAListaDeProporcionesDeLogica(List<AccesoADatos.PlatilloIngrediente> ProporcionesDb)
		{
			List<Clases.Proporcion> proporcionesConvertidas = new List<Clases.Proporcion>();
			foreach (PlatilloIngrediente proporcion in ProporcionesDb)
			{
				Clases.Proporcion proporcionConvertida = ConvertirProporcionDeAccesoADatosAProporcionDeLogica(proporcion);
				proporcionesConvertidas.Add(proporcionConvertida);
			}

			return proporcionesConvertidas;
		}

		private Clases.Proporcion ConvertirProporcionDeAccesoADatosAProporcionDeLogica(AccesoADatos.PlatilloIngrediente ProporcionDb)
		{
			Clases.Proporcion proporcionConvertida = new Clases.Proporcion
			{
				Id = ProporcionDb.Id,
				Cantidad = ProporcionDb.Cantidad
			};
			IngredienteDAO ingredienteDAO = new IngredienteDAO();
			PlatilloDAO platilloDAO = new PlatilloDAO();
			proporcionConvertida.Ingrediente = ingredienteDAO.ConvertirDeDatosAlogica(ProporcionDb.Ingredientes);
			return proporcionConvertida;
		}

		public AccesoADatos.PlatilloIngrediente ConvertirLogicaADb(Clases.Proporcion Proporcion)
		{
			AccesoADatos.PlatilloIngrediente proporcionConvertida = new PlatilloIngrediente()
			{
				Id = Proporcion.Id,
				Cantidad = Proporcion.Cantidad,
			};
			IngredienteDAO ingredienteDAO = new IngredienteDAO();
			proporcionConvertida.Ingredientes = ingredienteDAO.ConvertirDeLogicaADb(Proporcion.Ingrediente);
			proporcionConvertida.Ingredientes.AlimentoIngrediente = new List<PlatilloIngrediente>() { proporcionConvertida };
			
			return proporcionConvertida;
		}

		private AccesoADatos.PlatilloIngrediente ConvertirProporcionDeLogicaAProporcionDeAccesoADatosParaEdicion(Clases.Proporcion Proporcion)
		{
			PlatilloIngrediente proporcionConvertida = new PlatilloIngrediente();
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				proporcionConvertida = context.PlatilloIngrediente.Find(Proporcion.Id);
			}

			proporcionConvertida.Cantidad = Proporcion.Cantidad;

			IngredienteDAO ingredienteDAO = new IngredienteDAO();
			proporcionConvertida.Ingredientes = ingredienteDAO.ConvertirDeLogicaADb(Proporcion.Ingrediente);
			proporcionConvertida.Ingredientes.AlimentoIngrediente = new List<PlatilloIngrediente>() { proporcionConvertida };

			return proporcionConvertida;
		}

		public void ClonarProporcionDeAccesoDatos(PlatilloIngrediente Proporcion, PlatilloIngrediente resultado)
		{
			resultado.Id = Proporcion.Id;
			resultado.Alimento = Proporcion.Alimento;
			resultado.Cantidad = Proporcion.Cantidad;
		}
	}
}
