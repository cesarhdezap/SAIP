using AccesoADatos;
using LogicaDeNegocio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LogicaDeNegocio.ObjetosAccesoADatos
{
	public class IngredienteDAO
	{
		public void GuardarIngrediente(Clases.Ingrediente ingrediente)
		{
			AccesoADatos.Ingrediente ingredienteDb = new AccesoADatos.Ingrediente();
			ingrediente.FechaDeCreacion = DateTime.Now;
			ingrediente.FechaDeModificacion = DateTime.Now;
			ingrediente.Activo = true;

			ingredienteDb = ConvertirDeLogicaADb(ingrediente);

			if (!ValidarExistenciaDeIngrediente(ingredienteDb))
			{
				using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
				{
					context.Ingredientes.Add(ingredienteDb);
					context.SaveChanges();
				}
			}
			else
			{
				throw new InvalidOperationException("Se ha detectado que el código o el código de barras a ingresar se encuentran repetidos, porfavor ingrese uno distinto");
			}
		}
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

			List<Clases.Componente> componentes = new List<Clases.Componente>();

			foreach (IngredienteIngrediente ingredienteIngrediente in ingredienteDb.IngredienteIngredienteComponente)
			{
				componentes.Add(new Clases.Componente 
				{
					Cantidad = ingredienteIngrediente.Cantidad,
					Compuesto = ingredienteConvertido,
					Ingrediente = ConvertirDeDatosALogica(ingredienteIngrediente.IngredienteComponente)
				});
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

		public Clases.Ingrediente RecuperarIngredientePorCodigo(string codigo)
		{
			Ingrediente ingredienteDb = new Ingrediente();

			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				ingredienteDb = context.Ingredientes.Include(i => i.IngredienteIngredienteComponente).FirstOrDefault(i => i.Codigo == codigo);
			}
			Clases.Ingrediente ingredienteResultado = ConvertirDeDatosALogica(ingredienteDb);

			return ingredienteResultado;
		}

		public bool ValidarCodigoExistente(string codigo)
		{
			bool codigoRepetido = false;

			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				codigoRepetido = context.Ingredientes.ToList().Exists(i => i.Codigo == codigo);
			}

			return codigoRepetido;
		}
		private bool ValidarExistenciaDeIngrediente(Ingrediente ingredienteDb)
		{
			bool resultado = false;

			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				if (context.Ingredientes.ToList().Exists(i => i.Codigo == ingredienteDb.Codigo) && context.Ingredientes.ToList().Exists(i => i.Codigo == ingredienteDb.CodigoDeBarras))
				{
					resultado = true;
				}
				else
				{
					throw new InvalidOperationException("El código no existe en la base de datos");
				}
			}
			return resultado;
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
