using AccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.ObjetosAccesoADatos
{
	public class PlatilloDAO
	{
		private Clases.Platillo ConvertirPlatilloDeAccesoADatosAPlatilloDeLogica(AccesoADatos.Platillo PlatilloDb)
		{
			Clases.Platillo alimentoConvertido = new Clases.Platillo()
			{
				Id = PlatilloDb.Id,
				Nombre = PlatilloDb.Nombre,
				FechaDeCreacion = PlatilloDb.FechaDeCreacion,
				FechaDeModificacion = PlatilloDb.FechaDeModificacion,
				Activo = PlatilloDb.Activo,
				Precio = PlatilloDb.Precio,
				Codigo = PlatilloDb.Codigo
			};

			ProporcionDAO proporcionDAO = new ProporcionDAO();
			alimentoConvertido.Proporciones = proporcionDAO.CargarProporcionesPorIdAlimento(PlatilloDb.Id);

			return alimentoConvertido;
		}



		private List<Clases.Platillo> ConvertirListaDePlatillosDeAccesoADatosAListaDePLatillosDeLogica(List<Platillo> AlimentosDb)
		{
			List<Clases.Platillo> alimentosResultado = new List<Clases.Platillo>();

			foreach (Platillo platillo in AlimentosDb)
			{
				alimentosResultado.Add(ConvertirPlatilloDeAccesoADatosAPlatilloDeLogica(platillo));
			}

			return alimentosResultado;
		}

		public List<Clases.Platillo> CargarTodos()
		{
			List<Platillo> alimentosDb = new List<Platillo>();
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				alimentosDb = context.Platillos.ToList();
			}

			List<Clases.Platillo> alimentosResultado = ConvertirListaDePlatillosDeAccesoADatosAListaDePLatillosDeLogica(alimentosDb);
			return alimentosResultado;
		}

		public Clases.Platillo CargarPlatilloPorId(int IdPlatillo)
		{
			Platillo platilloDb = new Platillo();
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				platilloDb = context.Platillos.Find(IdPlatillo);
			}
			Clases.Platillo platilloResultado = ConvertirPlatilloDeAccesoADatosAPlatilloDeLogica(platilloDb);

			return platilloResultado;
		}

		public List<Clases.Platillo> CargarListaDeIdsDePlatilloPorIdDePedido(int IdPedido)
		{
			throw new NotImplementedException();
		}
	}
}
