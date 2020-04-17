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
		public List<Clases.Proporcion> CargarProporcionesPorIdAlimento(int AlimentoId)
		{
			List<Clases.Proporcion> proporciones = new List<Clases.Proporcion>();
			List<AlimentoIngrediente> proporcionDb = new List<AlimentoIngrediente>();
			IngredienteDAO ingredienteDAO = new IngredienteDAO();
			AlimentoDAO alimentoDAO = new AlimentoDAO();
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				proporcionDb = context.AlimentoIngredientes.ToList().TakeWhile(alimento => alimento.Alimento.Id == AlimentoId).ToList();
				foreach (AlimentoIngrediente proporcion in proporcionDb)
				{

					proporciones.Add(
						new Clases.Proporcion
						{
							Ingrediente = new Clases.Ingrediente{
								Id = proporcion.Ingredientes.Id},
							Alimento = new Clases.Alimento
							{
								Id = proporcion.Alimento.Id
							},
							Cantidad = proporcion.Cantidad
						}
						);
				}

			}

			return proporciones;
		}
	}
}
