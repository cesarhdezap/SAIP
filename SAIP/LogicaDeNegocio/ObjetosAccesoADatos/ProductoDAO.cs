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

		public Clases.Producto ConvertirDeDbALogica(AccesoADatos.Producto ProductoDb)
		{
			Clases.Producto ingredienteConvertido = new Clases.Producto()
			{
				Id = ProductoDb.Id,
				CantidadEnInventario = ProductoDb.CantidadEnInventario,
				Nombre = ProductoDb.Nombre,
				CostoDeVenta = ProductoDb.Costo,
				CodigoDeBarras = ProductoDb.CodigoDeBarras,
				Creador = ProductoDb.NombreCreador,
				Activo = ProductoDb.Activo,
				Costo = ProductoDb.Costo
			};
			return ingredienteConvertido;
		}

		private List<Clases.Producto> ConvertirListaDeDbAListaDeLogica(List<Producto> productosDb)
		{
			List<Clases.Producto> productosResultado = new List<Clases.Producto>();

			foreach (Producto producto in productosDb)
			{
				productosResultado.Add(ConvertirDeDbALogica(producto));
			}

			return productosResultado;
		}
	}
}
