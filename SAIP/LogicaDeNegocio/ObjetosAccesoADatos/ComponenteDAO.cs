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
		private List<Clases.Componente> ConvertirListaDeComponentesDeAccesoADatosAListaDeComponentesDeLogica(List<AccesoADatos.IngredienteIngrediente> ComponentesDb)
		{
			List<Clases.Componente> componentesConvertidos = new List<Clases.Componente>();
			foreach(IngredienteIngrediente componente in ComponentesDb)
			{
				componentesConvertidos.Add(ConvertirComponenteDeAccesoADatosAComponenteDelogica(componente));
			}
			return componentesConvertidos;
		}

		private Clases.Componente ConvertirComponenteDeAccesoADatosAComponenteDelogica(AccesoADatos.IngredienteIngrediente ComponenteDb)
		{
			Clases.Componente componenteConvertido = new Clases.Componente()
			{
				Cantidad = ComponenteDb.Cantidad
			};
			IngredienteDAO ingredienteDAO = new IngredienteDAO();
			componenteConvertido.Ingrediente = ingredienteDAO.ConvertirIngredienteDeAccesoADatosAIngredienteDeLogica(ComponenteDb.IngredienteComponente);
			return componenteConvertido;
		}

		public AccesoADatos.IngredienteIngrediente ConvertirComponenteDeLogicaAComponenteDeAccesoADatos(Clases.Componente Componente)
		{
			IngredienteIngrediente componenteDb = new IngredienteIngrediente();
			IngredienteDAO ingredienteDAO = new IngredienteDAO();
			componenteDb.IngredienteComponente = ingredienteDAO.ConvertirIngredienteDeLogicaAIngredienteDeAccesoADatos(Componente.Ingrediente);
			componenteDb.Cantidad = Componente.Cantidad;
			componenteDb.IngredienteCompuesto = ingredienteDAO.ConvertirIngredienteDeLogicaAIngredienteDeAccesoADatos(Componente.Compuesto);
			return componenteDb;
		}

		public List<AccesoADatos.IngredienteIngrediente> ConvertirListaDeComponentesDeLogicaAListaDeComponentesDeAccesoADatos(List<Clases.Componente> Componentes)
		{
			List<AccesoADatos.IngredienteIngrediente> componentesDb = new List<IngredienteIngrediente>();
			foreach (Clases.Componente componente in Componentes)
			{
				componentesDb.Add(ConvertirComponenteDeLogicaAComponenteDeAccesoADatos(componente));
			}

			return componentesDb;
		}

		public List<Clases.Componente> ObtenerComponentesPorIdDeIngredienteCompuesto(int Id)
		{
			List<IngredienteIngrediente> componentes = new List<IngredienteIngrediente>();
			List<Clases.Componente> componentesResultado = new List<Clases.Componente>();
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				componentes = context.IngredienteIngrediente.ToList().TakeWhile(objeto => objeto.IngredienteCompuesto.Id == Id).ToList();
				IngredienteDAO ingredienteDAO = new IngredienteDAO();
				componentesResultado = ConvertirListaDeComponentesDeAccesoADatosAListaDeComponentesDeLogica(componentes);
			}

			return componentesResultado;
		}
	}
}
