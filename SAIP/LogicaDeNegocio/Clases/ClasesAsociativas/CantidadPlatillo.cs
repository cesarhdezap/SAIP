using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.Clases.ClasesAsociativas
{
    public class CantidadPlatillo
    {
        public int Cantidad { get; set;}
        public Platillo Alimento { get; set;}
        public Pedido Orden { get; set; }
    }
}
