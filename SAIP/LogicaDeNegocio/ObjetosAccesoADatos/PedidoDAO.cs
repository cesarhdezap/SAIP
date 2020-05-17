using AccesoADatos;
using LogicaDeNegocio.Clases;
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

        public List<Pedido> RecuperarPedidoPorId(int idPedido) 
        {
            List<Pedido> pedidos = new List<Pedido>();
            AccesoADatos.Pedido pedido;
            using(ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
               pedido = context.Pedidos.Find(idPedido);
            }
            Pedido pedidoLogico = new Pedido();
            if(pedido != null)
            {
                pedidoLogico = ConvertirPedidoDeDatosALogica(pedido);
                pedidos.Add(pedidoLogico);
            }

            return pedidos;
        }

        private AccesoADatos.Pedido ConvertirPedidoLogicaADatos(Pedido pedidoLogica)
        {
            AccesoADatos.Pedido pedidoDatos = new AccesoADatos.Pedido
            {
                Id = pedidoLogica.Id,
                FechaDeCreacion = pedidoLogica.FechaDeCreacion,
                PrecioTotal = pedidoLogica.PrecioTotal,
                Iva = pedidoLogica.Iva,
                Estado = (short)pedidoLogica.Estado
            };
            return pedidoDatos;
        }

        public Pedido ConvertirPedidoDeDatosALogica(AccesoADatos.Pedido pedidoDatos)
        {
            Pedido pedidoLogica = new Pedido()
            {
                Id = pedidoDatos.Id,
                FechaDeCreacion = pedidoDatos.FechaDeCreacion,
                PrecioTotal = pedidoDatos.PrecioTotal,
                Iva = pedidoDatos.Iva,
                Estado = (EstadoPedido)pedidoDatos.Estado,
            };

            return pedidoLogica;
        }
    }
}
