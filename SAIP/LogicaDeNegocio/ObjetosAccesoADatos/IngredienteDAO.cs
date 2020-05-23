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
		public Clases.Ingrediente ConvertirDeDatosALogica(AccesoADatos.Ingrediente ingredienteDb)
		{
			Clases.Ingrediente ingredienteConvertido = new Clases.Ingrediente()
			{
				Id = ingredienteDb.Id,
				UnidadDeMedida = (UnidadDeMedida)ingredienteDb.UnidadDeMedida,
				CantidadEnInventario = ingredienteDb.CantidadEnInventario,
				Nombre = ingredienteDb.Nombre,
				FechaDeCreacion = ingredienteDb.FechaDeCreacion,
				FechaDeModificacion = ingredienteDb.FechaDeModiciacion,
				Codigo = ingredienteDb.Codigo,
				CodigoDeBarras = ingredienteDb.CodigoDeBarras,
				Creador = ingredienteDb.NombreCreador,
				Activo = ingredienteDb.Activo,
				Costo = ingredienteDb.Costo
			};

			foreach(IngredienteIngrediente ingredienteIngrediente in ingredienteDb.IngredienteIngredienteComponente)
			{
				throw new NotImplementedException();
			}

			ComponenteDAO componenteDAO = new ComponenteDAO();
			ingredienteConvertido.Componentes = componenteDAO.ObtenerComponentesPorIdDeIngredienteCompuesto(ingredienteDb.Id);
			return ingredienteConvertido;
		}

		public AccesoADatos.Ingrediente ConvertirDeLogicaADb(Clases.Ingrediente ingrediente)
		{
			AccesoADatos.Ingrediente ingredienteConvertido = new AccesoADatos.Ingrediente()
			{
				Id = ingrediente.Id,
				UnidadDeMedida = (short)ingrediente.UnidadDeMedida,
				CantidadEnInventario = ingrediente.CantidadEnInventario,
				Nombre = ingrediente.Nombre,
				FechaDeCreacion = ingrediente.FechaDeCreacion,
				FechaDeModiciacion = ingrediente.FechaDeModificacion,
				Codigo = ingrediente.Codigo,
				CodigoDeBarras = ingrediente.CodigoDeBarras,
				NombreCreador = ingrediente.Creador,
				Activo = ingrediente.Activo,
				Costo = ingrediente.Costo
				
			};
			ComponenteDAO componenteDAO = new ComponenteDAO();
			if (ingrediente.Componentes.Count > 0)
			{
				ingredienteConvertido.IngredienteIngredienteComponente = componenteDAO.ConvertirlistaDeLogicaADatos(ingrediente.Componentes);
			}
			return ingredienteConvertido;
		}


		private List<Clases.Ingrediente> ConvertirListaDeDbAListaDeLogica(List<Ingrediente> IngredientesDeDb)
		{
			List<Clases.Ingrediente> ingredientesResultado = new List<Clases.Ingrediente>();

			foreach (Ingrediente ingrediente in IngredientesDeDb)
			{
				ingredientesResultado.Add(ConvertirDeDatosALogica(ingrediente));
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
			Clases.Ingrediente ingredienteResultado = ConvertirDeDatosALogica(ingredienteDb);

			return ingredienteResultado;
		}

		public void ActualizarIngrediente(Clases.Ingrediente ingrediente)
		{
			Ingrediente ingredienteDb;
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				 ingredienteDb = context.Ingredientes.Find(ingrediente.Id);
				ingredienteDb.FechaDeModiciacion = DateTime.Now;
				ingredienteDb.Nombre = ingrediente.Nombre;
				ingredienteDb.CantidadEnInventario = ingrediente.CantidadEnInventario;
				ingredienteDb.UnidadDeMedida = (short)ingrediente.UnidadDeMedida;
				ingredienteDb.Codigo = ingrediente.Codigo;
				ingredienteDb.CodigoDeBarras = ingrediente.CodigoDeBarras;
				ingredienteDb.Costo = ingrediente.Costo;
				ingredienteDb.Activo = ingrediente.Activo;
				context.SaveChanges();
			}
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
