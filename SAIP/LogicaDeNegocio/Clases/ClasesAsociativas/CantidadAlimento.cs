using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.Clases.ClasesAsociativas
{
    public abstract class CantidadAlimento
    {
        public int Cantidad { get; set; }
        public Pedido Pedido { get; set; }
    }
}
