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
		public List<Clases.Producto> CargarProductosActivos()
		{
			List<AccesoADatos.Producto> productosDb = new List<AccesoADatos.Producto>();
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				productosDb = context.Productos.ToList().TakeWhile(p => p.Activo == true).ToList();
			}

			List<Clases.Producto> productosResultado = new List<Clases.Producto>();
			productosResultado = ConvertirListaDeDbAListaDeLogica(productosDb);
			return productosResultado;
		}

		public Clases.Producto ConvertirProductoDatosALogica(AccesoADatos.Producto ProductoDb)
		{
			Clases.Producto productoConvertido = new Clases.Producto()
			{
				Id = ProductoDb.Id,
				CantidadEnInventario = ProductoDb.CantidadEnInventario,
				Nombre = ProductoDb.Nombre,
				CodigoDeBarras = ProductoDb.CodigoDeBarras,
				Codigo = ProductoDb.Codigo,
				Creador = ProductoDb.NombreCreador,
				Activo = ProductoDb.Activo,
				Costo = ProductoDb.Costo
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
				context.SaveChanges();
			}
		}
	}
}
