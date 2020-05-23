using AccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

namespace LogicaDeNegocio.ObjetosAccesoADatos
{
	public class PlatilloDAO
	{
		public Clases.Platillo ConvertirDatosALogica(AccesoADatos.Platillo PlatilloDb)
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
			alimentoConvertido.Proporciones = proporcionDAO.CargarProporcionesPorIdPlatillo(PlatilloDb.Id);

			return alimentoConvertido;
		}

		public AccesoADatos.Platillo ConvertirPlatilloDeLogicaAPlatilloDeAccesoADatosParaGuardado(Clases.Platillo Platillo)
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
			platilloDb.AlimentoIngrediente = (proporcionDAO.ConvertirListaLogicaAListaDeDB(Platillo.Proporciones));

			return platilloDb;
		}

		private List<Clases.Platillo> ConvertirListaDePlatillosDeAccesoADatosAListaDePLatillosDeLogica(List<Platillo> AlimentosDb)
		{
			List<Clases.Platillo> alimentosResultado = new List<Clases.Platillo>();

			foreach (Platillo platillo in AlimentosDb)
			{
				alimentosResultado.Add(ConvertirDatosALogica(platillo));
			}

			return alimentosResultado;
		}

		public void GuardarPlatillo(Clases.Platillo Platillo)
		{
			Platillo.Activo = true;
			Platillo.FechaDeCreacion = DateTime.Now;
			Platillo.FechaDeModificacion = DateTime.Now;
			AccesoADatos.Platillo platilloAGuardar = ConvertirPlatilloDeLogicaAPlatilloDeAccesoADatosParaGuardado(Platillo);

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

		public void ClonarPlatilloDeAccesoADatos(Platillo Platillo, Platillo resultado)
		{
			resultado.Nombre = Platillo.Nombre;
			resultado.Precio = Platillo.Precio;
			resultado.FechaDeCreacion = Platillo.FechaDeCreacion;
			resultado.FechaDeModificacion = Platillo.FechaDeModificacion;
			resultado.Activo = Platillo.Activo;
			resultado.Codigo = Platillo.Codigo;
			resultado.Notas = Platillo.Notas;
			resultado.Descripcion = Platillo.Descripcion;
			resultado.AlimentoIngrediente = new List<PlatilloIngrediente>();
			foreach (PlatilloIngrediente proporcion in Platillo.AlimentoIngrediente)
			{
				resultado.AlimentoIngrediente.Add(proporcion);
			}
		}

		public void EditarPlatillo(Clases.Platillo Platillo)
		{
			Platillo.FechaDeModificacion = DateTime.Now;
			Platillo platilloDb;
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				platilloDb = context.Platillos.Include(x => x.AlimentoIngrediente.Select(i => i.Ingredientes)).FirstOrDefault(p => p.Id == Platillo.Id);

				platilloDb.Nombre = Platillo.Nombre;
				platilloDb.Precio = Platillo.Precio;
				platilloDb.FechaDeCreacion = Platillo.FechaDeCreacion;
				platilloDb.FechaDeModificacion = Platillo.FechaDeModificacion;
				platilloDb.Activo = Platillo.Activo;
				platilloDb.Codigo = Platillo.Codigo;
				platilloDb.Notas = Platillo.Notas;
				platilloDb.Descripcion = Platillo.Descripcion;

				List<Clases.Proporcion> proporcionesAAñadir = new List<Clases.Proporcion>();
				//Obtener proporciones a añadir
				foreach (Clases.Proporcion proporcion in Platillo.Proporciones)
				{
					if (proporcion.Id == 0)
					{
						proporcionesAAñadir.Add(proporcion);
					}
					else
					{
						foreach (PlatilloIngrediente proporcionDB in platilloDb.AlimentoIngrediente)
						{
							if (proporcion.Id == proporcionDB.Id)
							{
								proporcionDB.Cantidad = proporcion.Cantidad;
							}
						}
					}
				}

				//Añadir proporciones obtenidas a db
				if (proporcionesAAñadir.Count > 0)
				{
					ProporcionDAO proporcionDAO = new ProporcionDAO();
					foreach(Clases.Proporcion proporcion in proporcionesAAñadir)
					{
						platilloDb.AlimentoIngrediente.Add(proporcionDAO.ConvertirLogicaADb(proporcion));
					}
					
					//platilloDb.AlimentoIngrediente = platilloDb.AlimentoIngrediente
					//	.Concat(proporcionDAO.ConvertirListaLogicaAListaDeDB(proporcionesAAñadir)).ToList();
				}

				// Vincular ingrediente con platillo
				foreach (PlatilloIngrediente proporcion in platilloDb.AlimentoIngrediente)
				{
					proporcion.Alimento = platilloDb;
					proporcion.Ingredientes = context.Ingredientes.Attach(context.Ingredientes.Find(proporcion.Ingredientes.Id));
				}

				List<PlatilloIngrediente> proporcionesAEliminar = platilloDb.AlimentoIngrediente.Where(p => !Platillo.Proporciones.Any(p2 => p2.Id == p.Id)).ToList();
				foreach (PlatilloIngrediente proporcion in proporcionesAEliminar)
				{
					context.PlatilloIngrediente.Remove(proporcion);
				}
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
			Clases.Platillo platilloResultado = ConvertirDatosALogica(platilloDb);

			return platilloResultado;
		}

		public List<Clases.Platillo> CargarListaDeIdsDePlatilloPorIdDePedido(int IdPedido)
		{
			throw new NotImplementedException();
		}
	}
}
