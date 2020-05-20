using AccesoADatos;
using LogicaDeNegocio.Clases.ClasesAsociativas;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.ObjetosAccesoADatos
{
    public class CantidadProductoDAO
    {
        public List<CantidadProducto> RecuperarCantidadProductoPorIDPedido(int idPedido)
        {
            List<AccesoADatos.ProductoPedido> productoPedido = new List<AccesoADatos.ProductoPedido>();

            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                productoPedido = context.ProductoPedido.Where(p => p.Id == idPedido)
                    .Include(p => p.Productos).ToList();
            }
            return ConvertirListaDatosALogica(productoPedido);
        }

        public List<CantidadProducto> ConvertirListaDatosALogica(List<AccesoADatos.ProductoPedido> productoPedidos)
        {
            List<CantidadProducto> cantidadProductos = new List<CantidadProducto>();

            foreach (ProductoPedido productoPedido in productoPedidos)
            {
                ProductoDAO productoDAO = new ProductoDAO();
                CantidadProducto cantidadPlatillo = new CantidadProducto
                {
                    Cantidad = productoPedido.Cantidad,
                    Producto = productoDAO.ConvertirProductoDatosALogica(productoPedido.Productos)
                };
                cantidadProductos.Add(cantidadPlatillo);
            }

            return cantidadProductos;
        }
    }
}
