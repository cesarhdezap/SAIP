 using AccesoADatos;
using LogicaDeNegocio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.ObjetosAccesoADatos
{
	public class ProductoDAO
	{
		public void Guardar(Clases.Producto producto)
		{
			Producto productoDB = new Producto
			{
				Activo = true,
				CantidadEnInventario = producto.CantidadEnInventario,
				Codigo = producto.Codigo,
				Precio = producto.Precio,
				CodigoDeBarras = producto.CodigoDeBarras,
				Costo = producto.Costo,
				FechaDeCreacion = DateTime.Now,
				Nombre = producto.Nombre,
				NombreCreador = producto.Creador,
				FechaDeModificacion = DateTime.Now,
				Imagen = producto.Imagen
			};

			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				context.Productos.Add(productoDB);
				context.SaveChanges();
			}
		}

		public void Depuracion_Eliminar(string nombre)
		{
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				Producto productoDb = context.Productos.FirstOrDefault(p => p.Nombre == nombre);
				context.Productos.Remove(productoDb);
				context.SaveChanges();
			}
		}



		public Clases.Producto CargarPorID(int id)
		{
			Producto producto = new Producto();
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				producto = context.Productos.Find(id);
			}
			return ConvertirProductoDatosALogica(producto);
		}

		public List<Clases.Producto> CargarProductosActivos()
		{
			List<AccesoADatos.Producto> productosDb = new List<AccesoADatos.Producto>();
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				productosDb = context.Productos.ToList().Where(p => p.Activo == true).ToList();
			}

			List<Clases.Producto> productosResultado = new List<Clases.Producto>();
			productosResultado = ConvertirListaDeDbAListaDeLogica(productosDb);
			return productosResultado;
		}

		public List<Clases.Producto> CargarTodos()
		{
			List<AccesoADatos.Producto> productosDb = new List<AccesoADatos.Producto>();
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				productosDb = context.Productos.ToList();
			}

			List<Clases.Producto> productosResultado = new List<Clases.Producto>();
			productosResultado = ConvertirListaDeDbAListaDeLogica(productosDb);
			return productosResultado;
		}

		public Clases.Producto ConvertirProductoDatosALogica(AccesoADatos.Producto productoDb)
		{
			Clases.Producto productoConvertido = new Clases.Producto()
			{
				Id = productoDb.Id,
				CantidadEnInventario = productoDb.CantidadEnInventario,
				Nombre = productoDb.Nombre,
				Precio = productoDb.Precio,
				CodigoDeBarras = productoDb.CodigoDeBarras,
				Creador = productoDb.NombreCreador,
				Activo = productoDb.Activo,
				Costo = productoDb.Costo,
				Codigo = productoDb.Codigo,
				Imagen = productoDb.Imagen
			};
			return productoConvertido;
		}

		private List<Clases.Producto> ConvertirListaDeDbAListaDeLogica(List<AccesoADatos.Producto> productosDb)
		{
			List<Clases.Producto> productosResultado = new List<Clases.Producto>();

			foreach (AccesoADatos.Producto producto in productosDb)
			{
				productosResultado.Add(ConvertirProductoDatosALogica(producto));
			}

			return productosResultado;
		}

		public Clases.Producto CargarProductoPorID(int id)
		{
			Producto productoDb = new Producto();
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				productoDb = context.Productos.Find(id);

			}
			Clases.Producto productoResultado = ConvertirProductoDatosALogica(productoDb);

			return productoResultado;
		}

		public void ActualizarProducto(Clases.Producto producto)
		{
			Producto productoDb;
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				productoDb = context.Productos.Find(producto.Id);
				productoDb.FechaDeModificacion = DateTime.Now;
				productoDb.Nombre = producto.Nombre;
				productoDb.Codigo = producto.Codigo;
				productoDb.CantidadEnInventario = producto.CantidadEnInventario;
				productoDb.CodigoDeBarras = producto.CodigoDeBarras;
				productoDb.Costo = producto.Costo;
				productoDb.Activo = producto.Activo;
				productoDb.Precio = producto.Precio;
				productoDb.Imagen = producto.Imagen;
				context.SaveChanges();
			}
		}
	}
}
