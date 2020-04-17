using AccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.ObjetosAccesoADatos
{
	public class IngredienteDAO
	{
		private Clases.Ingrediente ConvertireIngredienteDeAccesoADatosAIngredienteDeLogica(AccesoADatos.Ingrediente IngredienteDb)
		{
			Clases.Ingrediente ingredienteConvertido = new Clases.Ingrediente()
			{
				Id = IngredienteDb.Id,
				UnidadDeMedida = IngredienteDb.UnidadDeMedida,
				CantidadEnInventario = IngredienteDb.CantidadEnInventario,
				Nombre = IngredienteDb.Nombre,
				FechaDeCreacion = IngredienteDb.FechaDeCreacion,
				FechaDeModificacion = IngredienteDb.FechaDeModiciacion,
				Codigo = IngredienteDb.Codigo,
				CodigoDeBarras = IngredienteDb.CodigoDeBarras,
				Creador = IngredienteDb.Creador,
				Activo = IngredienteDb.Activo,
				Costo = IngredienteDb.Costo,
				Componentes = ObtenerComponentesPorIdDeIngredienteCompuesto(IngredienteDb.Id)
			};

			return ingredienteConvertido;
		}

		private List<Clases.Ingrediente> ConvertirListaDeIngredientesDeAccesoADatosAListaDeIngredientesDeLogica(List<Ingrediente> IngredientesDeDb)
		{
			List<Clases.Ingrediente> ingredientesResultado = new List<Clases.Ingrediente>();

			foreach (Ingrediente ingrediente in IngredientesDeDb)
			{
				ingredientesResultado.Add(ConvertireIngredienteDeAccesoADatosAIngredienteDeLogica(ingrediente));
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
			ingredientesResultado = ConvertirListaDeIngredientesDeAccesoADatosAListaDeIngredientesDeLogica(ingredientesDb);
			return ingredientesResultado;
		}

		public Clases.Ingrediente CargarIngredientePorId(int Id)
		{
			Ingrediente ingredienteDb = new Ingrediente();
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				ingredienteDb = context.Ingredientes.Find(Id);
			}
			Clases.Ingrediente ingredienteResultado = ConvertireIngredienteDeAccesoADatosAIngredienteDeLogica(ingredienteDb);

			return ingredienteResultado;
		}

		private List<Clases.Ingrediente> ObtenerComponentesPorIdDeIngredienteCompuesto(int Id)
		{
			List<IngredienteIngrediente> componentes = new List<IngredienteIngrediente>();
			List<Clases.Ingrediente> ingredientesResultado = new List<Clases.Ingrediente>();
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				componentes = context.IngredienteIngredientes.ToList().TakeWhile(objeto => objeto.IngredienteCompuesto.Id == Id).ToList();
				
				foreach (IngredienteIngrediente ingrediente in componentes)
				{
					ingredientesResultado.Add(CargarIngredientePorId(ingrediente.IngredienteComponente.Id));
				}
			}
			
			return ingredientesResultado;
		}

		public List<Clases.Ingrediente> ObtenerListaDeIngredientesPorIdDeAlimento(int Id)
		{
			List<IngredienteIngrediente> componentes = new List<IngredienteIngrediente>();
			List<Clases.Ingrediente> ingredientesResultado = new List<Clases.Ingrediente>();
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				componentes = context.IngredienteIngredientes.ToList().TakeWhile(ingrediente => ingrediente.IngredienteCompuesto.Id == Id).ToList();

				foreach (IngredienteIngrediente ingrediente in componentes)
				{
					ingredientesResultado.Add(CargarIngredientePorId(ingrediente.IngredienteComponente.Id));
				}
			}

			return ingredientesResultado;
		}


	}
}
