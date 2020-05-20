using LogicaDeNegocio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.Clases
{
    public class Cuenta
    {
        public int Id { get; set;}
        public string Direccion { get; set;}
        public double PrecioTotal { get; set;}
        public List<Cliente> Cliente { get; set;}
        public EstadoCuenta Estado { get; set; }
        public Mesa Mesa { get; set; }

        public List<Pedido> Pedidos = new List<Pedido>();

        public void AñadirPedido(Pedido pedido)
        {
            Pedidos.Add(pedido);

        }
    }
}