using AccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.ObjetosAccesoADatos
{
    public class PedidoDAO
    {
        public void Guardar(Pedido pedido)
        {
            
            AccesoADatos.Pedido pedidoAGuardar = ConvertirPedidoLogicaADatos(pedido);
            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                context.Pedidos.Add(pedidoAGuardar);
                context.SaveChanges();
            }
        }

        private AccesoADatos.Pedido ConvertirPedidoLogicaADatos(Pedido pedido)
        {
            throw new NotImplementedException();
        }

        public Pedido ConvertirPedidoDeDatosALogica(AccesoADatos.Pedido pedido)
        {
            return new Pedido
            {
                Id = pedido.Id,
                FechaDeCreacion = pedido.FechaDeCreacion,
                PrecioTotal = pedido.PrecioTotal,
                Iva = pedido.Iva,
                Estado = (EstadoPedido)pedido.Estado,
            };
        }
    }
}
