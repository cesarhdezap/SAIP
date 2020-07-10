using AccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.ObjetosAccesoADatos
{
	public class ComponenteDAO
	{
		private List<Clases.Componente> ConvertirListaDeDatosALogica(List<AccesoADatos.IngredienteIngrediente> componentesDb)
		{
			List<Clases.Componente> componentesConvertidos = new List<Clases.Componente>();
			foreach(IngredienteIngrediente componente in componentesDb)
			{
				componentesConvertidos.Add(ConvertirDeDatosALogica(componente));
			}
			return componentesConvertidos;
		}

		private Clases.Componente ConvertirDeDatosALogica(AccesoADatos.IngredienteIngrediente componenteDb)
		{
			Clases.Componente componenteConvertido = new Clases.Componente()
			{
				Cantidad = componenteDb.Cantidad
			};
			IngredienteDAO ingredienteDAO = new IngredienteDAO();
			componenteConvertido.Ingrediente = ingredienteDAO.ConvertirDeDatosALogica(componenteDb.IngredienteComponente);
			return componenteConvertido;
		}

		public AccesoADatos.IngredienteIngrediente ConvertirDeLogicaADatos(Clases.Componente componente)
		{
			IngredienteIngrediente componenteDb = new IngredienteIngrediente();
			IngredienteDAO ingredienteDAO = new IngredienteDAO();
			componenteDb.IngredienteComponente = ingredienteDAO.ConvertirDeLogicaADb(componente.Ingrediente);
			componenteDb.Cantidad = componente.Cantidad;
			componenteDb.IngredienteCompuesto = ingredienteDAO.ConvertirDeLogicaADb(componente.Compuesto);
			return componenteDb;
		}

		public List<AccesoADatos.IngredienteIngrediente> ConvertirlistaDeLogicaADatos(List<Clases.Componente> componentes)
		{
			List<AccesoADatos.IngredienteIngrediente> componentesDb = new List<IngredienteIngrediente>();
			foreach (Clases.Componente componente in componentes)
			{
				componentesDb.Add(ConvertirDeLogicaADatos(componente));
			}

			return componentesDb;
		}

		public List<Clases.Componente> ObtenerComponentesPorIdDeIngredienteCompuesto(int id)
		{
			List<IngredienteIngrediente> componentes = new List<IngredienteIngrediente>();
			List<Clases.Componente> componentesResultado = new List<Clases.Componente>();
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				componentes = context.IngredienteIngrediente.ToList().TakeWhile(objeto => objeto.IngredienteCompuesto.Id == id).ToList();
				IngredienteDAO ingredienteDAO = new IngredienteDAO();
				componentesResultado = ConvertirListaDeDatosALogica(componentes);
			}

			return componentesResultado;
		}
	}
}
