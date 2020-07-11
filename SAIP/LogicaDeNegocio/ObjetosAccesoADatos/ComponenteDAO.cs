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
		private List<Clases.Componente> ConvertirListaDeDatosALogica(List<AccesoADatos.RelacionIngrediente> componentesDb)
		{
			List<Clases.Componente> componentesConvertidos = new List<Clases.Componente>();
			foreach(RelacionIngrediente componente in componentesDb)
			{
				componentesConvertidos.Add(ConvertirDeDatosALogica(componente));
			}
			return componentesConvertidos;
		}

		private Clases.Componente ConvertirDeDatosALogica(AccesoADatos.RelacionIngrediente componenteDb)
		{
			Clases.Componente componenteConvertido = new Clases.Componente()
			{
				Cantidad = componenteDb.Cantidad,
			};
			IngredienteDAO ingredienteDAO = new IngredienteDAO();
			componenteConvertido.Ingrediente = ingredienteDAO.ConvertirDeDatosALogica(componenteDb.IngredienteHijo);
			return componenteConvertido;
		}

		public AccesoADatos.RelacionIngrediente ConvertirDeLogicaADatos(Clases.Componente componente)
		{
			RelacionIngrediente componenteDb = new RelacionIngrediente();
			IngredienteDAO ingredienteDAO = new IngredienteDAO();

			componenteDb.Cantidad = componente.Cantidad;
			componenteDb.IngredienteHijo = ingredienteDAO.ConvertirDeLogicaADb(componente.Ingrediente);

			return componenteDb;
		}

		public List<AccesoADatos.RelacionIngrediente> ConvertirlistaDeLogicaADatos(List<Clases.Componente> componentes)
		{
			List<AccesoADatos.RelacionIngrediente> componentesDb = new List<RelacionIngrediente>();

			foreach (Clases.Componente componente in componentes)
			{
				RelacionIngrediente componenteDb = ConvertirDeLogicaADatos(componente);
				componentesDb.Add(componenteDb);
			}

			return componentesDb;
		}

		public List<Clases.Componente> ObtenerComponentesPorIdDeIngredienteCompuesto(int id)
		{
			List<RelacionIngrediente> componentes = new List<RelacionIngrediente>();
			List<Clases.Componente> componentesResultado = new List<Clases.Componente>();

			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				componentes = context.RelacionIngredientes.ToList().TakeWhile(objeto => objeto.IngredienteHijo.Id == id).ToList();

				IngredienteDAO ingredienteDAO = new IngredienteDAO();

				componentesResultado = ConvertirListaDeDatosALogica(componentes);
			}

			return componentesResultado;
		}
	}
}
