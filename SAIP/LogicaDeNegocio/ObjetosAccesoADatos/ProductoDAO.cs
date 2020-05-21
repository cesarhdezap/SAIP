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
			List<Producto> productosDb = new List<Producto>();
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				productosDb = context.Productos.ToList().TakeWhile(p => p.Activo == true).ToList();
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
				Codigo = productoDb.Codigo
			};
			return productoConvertido;
		}

		private List<Clases.Producto> ConvertirListaDeDbAListaDeLogica(List<Producto> productosDb)
		{
			List<Clases.Producto> productosResultado = new List<Clases.Producto>();

			foreach (Producto producto in productosDb)
			{
				productosResultado.Add(ConvertirProductoDatosALogica(producto));
			}

			return productosResultado;
		}
	}
}
