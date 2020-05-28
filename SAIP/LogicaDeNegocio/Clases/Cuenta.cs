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
        public List<Cliente> Clientes { get; set; } = new List<Cliente>();
        public EstadoCuenta Estado { get; set; }
        public Mesa Mesa { get; set; }
        public Empleado Empleado { get; set; }

        public List<Pedido> Pedidos = new List<Pedido>();

        public void AñadirPedido(Pedido pedido)
        {
            Pedidos.Add(pedido);

        }

        public void TerminarCuenta()
        {
            Estado = EstadoCuenta.Terminada;
            //Calcular PrecioTotal
        }

        public void CalcularPrecioTotal()
        {
            double precioTotal = 0;

            foreach (Pedido pedido in Pedidos)
            {
                pedido.CalcularPrecioTotal();
                precioTotal += pedido.PrecioTotal;
            }
        }
    }
}