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
			List<PlatilloIngrediente> proporcionDb = new List<PlatilloIngrediente>();
			IngredienteDAO ingredienteDAO = new IngredienteDAO();
			PlatilloDAO alimentoDAO = new PlatilloDAO();
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				proporcionDb = context.PlatilloIngrediente.ToList().TakeWhile(alimento => alimento.Alimento.Id == AlimentoId).ToList();
				foreach (PlatilloIngrediente proporcion in proporcionDb)
				{

					proporciones.Add(
						new Clases.Proporcion
						{
							Ingrediente = new Clases.Ingrediente{
								Id = proporcion.Ingredientes.Id},
							Alimento = new Clases.Platillo
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
