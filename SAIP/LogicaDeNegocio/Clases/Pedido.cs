using LogicaDeNegocio.Clases;
using LogicaDeNegocio.Clases.ClasesAsociativas;
using LogicaDeNegocio.Enumeradores;
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
        public List<CantidadAlimento> CantidadAlimentos { get; set; }
        public double PrecioTotal { get; set; }
        public double Iva { get; set; }
        public List<string> Comentarios { get; set; }
        public EstadoPedido Estado { get; set; }
        public Empleado Empleado { get; set; }
        public Cuenta Cuenta { get; set; }
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

    
}
