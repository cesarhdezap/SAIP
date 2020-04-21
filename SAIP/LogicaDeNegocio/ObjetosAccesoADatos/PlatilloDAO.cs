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
				Codigo = PlatilloDb.Codigo,
				Notas = PlatilloDb.Notas,
				Descripcion = PlatilloDb.Descripcion
			};

			ProporcionDAO proporcionDAO = new ProporcionDAO();
			alimentoConvertido.Proporciones = proporcionDAO.CargarProporcionesPorIdAlimento(PlatilloDb.Id);

			return alimentoConvertido;
		}

		private AccesoADatos.Platillo ConvertirPlatilloDeLogicaAPlatilloDeAccesoADatos(Clases.Platillo Platillo)
		{
			AccesoADatos.Platillo platilloDb = new AccesoADatos.Platillo()
			{
				Id = Platillo.Id,
				Nombre = Platillo.Nombre,
				Precio = Platillo.Precio,
				FechaDeCreacion = Platillo.FechaDeCreacion,
				FechaDeModificacion = Platillo.FechaDeModificacion,
				Activo = Platillo.Activo,
				Codigo = Platillo.Codigo,
				Notas = Platillo.Notas,
				Descripcion = Platillo.Descripcion
			};
			ProporcionDAO proporcionDAO = new ProporcionDAO();
			platilloDb.AlimentoIngrediente = (proporcionDAO.ConvertirListaDeProporcionesDeLogicaAListaDeProporcionesDeAccesoADatos(Platillo.Proporciones));

			return platilloDb;
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

		public void GuardarPlatillo(Clases.Platillo Platillo)
		{
			Platillo.Activo = true;
			Platillo.FechaDeCreacion = DateTime.Now;
			Platillo.FechaDeModificacion = DateTime.Now;
			AccesoADatos.Platillo platilloAGuardar = ConvertirPlatilloDeLogicaAPlatilloDeAccesoADatos(Platillo);

			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				foreach (PlatilloIngrediente proporcion in platilloAGuardar.AlimentoIngrediente)
				{
					proporcion.Alimento = platilloAGuardar;
					proporcion.Ingredientes = context.Ingredientes.Find(proporcion.Ingredientes.Id);
				}
				
				context.Platillos.Add(platilloAGuardar);
				context.SaveChanges();
			}
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
