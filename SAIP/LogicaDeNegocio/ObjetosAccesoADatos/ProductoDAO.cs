using AccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.ObjetosAccesoADatos
{
    public class ProductoDAO
    {
        public Clases.Producto ConvertirProductoDatosALogica(AccesoADatos.Producto productoDB)
        {
            Clases.Producto producto = new Clases.Producto()
            {
                Id = productoDB.Id,
                CantidadEnInventario = productoDB.CantidadEnInventario,
                CodigoDeBarras = productoDB.CodigoDeBarras,
                Costo = productoDB.Costo,
                Precio = productoDB.Precio,
                Creador = productoDB.NombreCreador,
                Nombre = productoDB.Nombre
            };
            return producto;
        }
    }
}
