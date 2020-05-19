using LogicaDeNegocio.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime FechaDeCreacion { get; set; }
        public double PrecioTotal { get; set; }
        public double Iva { get; set; }
        public List<string> Comentarios { get; set; }
        public EstadoPedido Estado { get; set; }
        public int EmpleadoId { get; set; }
        public int CuentaId { get; set; }
        public string Creador { get; set; }


        public void AñadirProducto(Producto producto)
        {
            throw new NotImplementedException();
        }

        public string EstadoToString()
        {
            return Estado.ToString();
        }
    }

    public enum EstadoPedido
    {
        Registrado = 1,
        EnEspera = 2,
        Realizado = 3,
        Entregado = 4,
        Completado = 5,
        Cancelar = 6
    }
}
