using AccesoADatos;
using LogicaDeNegocio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.ObjetosAccesoADatos
{
	public class IngredienteDAO
	{
		public Clases.Ingrediente ConvertirDeDbALogica(AccesoADatos.Ingrediente IngredienteDb)
		{
			Clases.Ingrediente ingredienteConvertido = new Clases.Ingrediente()
			{
				Id = IngredienteDb.Id,
				UnidadDeMedida = (UnidadDeMedida)IngredienteDb.UnidadDeMedida,
				CantidadEnInventario = IngredienteDb.CantidadEnInventario,
				Nombre = IngredienteDb.Nombre,
				FechaDeCreacion = IngredienteDb.FechaDeCreacion,
				FechaDeModificacion = IngredienteDb.FechaDeModiciacion,
				Codigo = IngredienteDb.Codigo,
				CodigoDeBarras = IngredienteDb.CodigoDeBarras,
				Creador = IngredienteDb.NombreCreador,
				Activo = IngredienteDb.Activo,
				Costo = IngredienteDb.Costo
			};
			ComponenteDAO componenteDAO = new ComponenteDAO();
			ingredienteConvertido.Componentes = componenteDAO.ObtenerComponentesPorIdDeIngredienteCompuesto(IngredienteDb.Id);
			return ingredienteConvertido;
		}

		public AccesoADatos.Ingrediente ConvertirDeLogicaADb(Clases.Ingrediente Ingrediente)
		{
			AccesoADatos.Ingrediente ingredienteConvertido = new AccesoADatos.Ingrediente()
			{
				Id = Ingrediente.Id,
				UnidadDeMedida = (short)Ingrediente.UnidadDeMedida,
				CantidadEnInventario = Ingrediente.CantidadEnInventario,
				Nombre = Ingrediente.Nombre,
				FechaDeCreacion = Ingrediente.FechaDeCreacion,
				FechaDeModiciacion = Ingrediente.FechaDeModificacion,
				Codigo = Ingrediente.Codigo,
				CodigoDeBarras = Ingrediente.CodigoDeBarras,
				NombreCreador = Ingrediente.Creador,
				Activo = Ingrediente.Activo,
				Costo = Ingrediente.Costo
				
			};
			ComponenteDAO componenteDAO = new ComponenteDAO();
			if (Ingrediente.Componentes.Count > 0)
			{
				ingredienteConvertido.IngredienteIngrediente = componenteDAO.ConvertirListaDeComponentesDeLogicaAListaDeComponentesDeAccesoADatos(Ingrediente.Componentes);
			}
			return ingredienteConvertido;
		}


		private List<Clases.Ingrediente> ConvertirListaDeDbAListaDeLogica(List<Ingrediente> IngredientesDeDb)
		{
			List<Clases.Ingrediente> ingredientesResultado = new List<Clases.Ingrediente>();

			foreach (Ingrediente ingrediente in IngredientesDeDb)
			{
				ingredientesResultado.Add(ConvertirDeDbALogica(ingrediente));
			}

			return ingredientesResultado;
		}

		public List<Ingrediente> ConvertirListaDeIngredientesDeLogicaAListaDeIngredientesDeAccesoADatos(List<Clases.Ingrediente> Ingredientes)
		{
			List<Ingrediente> ingredientesResultado = new List<Ingrediente>();

			foreach (Clases.Ingrediente ingrediente in Ingredientes)
			{
				ingredientesResultado.Add(ConvertirDeLogicaADb(ingrediente));
			}

			return ingredientesResultado;
		}

		public List<Clases.Ingrediente> CargarTodos()
		{
			List<Ingrediente> ingredientesDb = new List<Ingrediente>();
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				ingredientesDb = context.Ingredientes.ToList();
			}

			List<Clases.Ingrediente> ingredientesResultado = new List<Clases.Ingrediente>();
			ingredientesResultado = ConvertirListaDeDbAListaDeLogica(ingredientesDb);
			return ingredientesResultado;
		}

		public List<Clases.Ingrediente> CargarIngredientesActivos()
		{
			List<Ingrediente> ingredientesDb = new List<Ingrediente>();
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				ingredientesDb = context.Ingredientes.ToList().TakeWhile(ingredienteCargado => ingredienteCargado.Activo == true).ToList();
			}

			List<Clases.Ingrediente> ingredientesResultado = new List<Clases.Ingrediente>();
			ingredientesResultado = ConvertirListaDeDbAListaDeLogica(ingredientesDb);
			return ingredientesResultado;
		}

		public Clases.Ingrediente CargarIngredientePorId(int Id)
		{
			Ingrediente ingredienteDb = new Ingrediente();
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				ingredienteDb = context.Ingredientes.Find(Id);
			
			}
			Clases.Ingrediente ingredienteResultado = ConvertirDeDbALogica(ingredienteDb);

			return ingredienteResultado;
		}

		

		public List<Clases.Ingrediente> ObtenerListaDeIngredientesPorIdDePlatillo(int Id)
		{
			List<IngredienteIngrediente> componentes = new List<IngredienteIngrediente>();
			List<Clases.Ingrediente> ingredientesResultado = new List<Clases.Ingrediente>();
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				componentes = context.IngredienteIngrediente.ToList().TakeWhile(ingrediente => ingrediente.IngredienteCompuesto.Id == Id).ToList();

				foreach (IngredienteIngrediente ingrediente in componentes)
				{
					ingredientesResultado.Add(CargarIngredientePorId(ingrediente.IngredienteComponente.Id));
				}
			}

			return ingredientesResultado;
		}


	}
}
