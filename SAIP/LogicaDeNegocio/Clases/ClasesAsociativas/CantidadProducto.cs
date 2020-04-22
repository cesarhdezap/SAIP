using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.Clases.ClasesAsociativas
{
    public class CantidadProducto
    {
        public int Cantidad { get; set;}
        public Producto Producto { get; set; }
        public Pedido Orden { get; set;  }

    }
}
