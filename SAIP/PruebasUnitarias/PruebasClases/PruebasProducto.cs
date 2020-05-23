using System;
using LogicaDeNegocio.Clases;
using LogicaDeNegocio.ObjetosAccesoADatos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PruebasUnitarias.PruebasClases
{
    [TestClass]
    public class PruebasProducto
    {
        [DataTestMethod]
        [DataRow(3, 4)]
        public void ProbarValidarCantidadAlimento_CantidadInsuficiente_RegresaFalse(int cantidadEnInventario, int cantidadMayor)
        {
            ProductoDAO productoDAO = new ProductoDAO();
            Producto producto = new Producto
            {
                Creador = this.ToString(),
                CantidadEnInventario = cantidadEnInventario,
                Codigo = "UT",
                CodigoDeBarras = "2211",
                Costo = 12,
                Nombre = Guid.NewGuid().ToString(),
                Precio = 20
            };

            productoDAO.Guardar(producto);
            bool resultadoValidacion = producto.ValidarCantidadAlimento(cantidadMayor);
            productoDAO.Depuracion_Eliminar(producto.Nombre);

            Assert.IsFalse(resultadoValidacion);
        }


    }
}
