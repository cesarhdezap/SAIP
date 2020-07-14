using System;
using LogicaDeNegocio.Clases;
using LogicaDeNegocio.ObjetosAccesoADatos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PruebasUnitarias.PruebasDAO
{
    [TestClass]
    public class PruebaPedidoDAO
    {
        Pedido pedido = new Pedido()
        {
            Id = 1231,
            Estado = LogicaDeNegocio.Enumeradores.EstadoPedido.EnEspera
        };

        [TestMethod]
        public void ProbarCambiarEstadoPedidoACancelar()
        {
            PedidoDAO pedidoDAO = new PedidoDAO();
            pedidoDAO.CambiarEstadoPedidoCancelar(pedido);
            Assert.AreEqual(pedido.Estado = LogicaDeNegocio.Enumeradores.EstadoPedido.Cancelado, pedido);

        }
    }
}
