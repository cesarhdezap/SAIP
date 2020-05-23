using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.Clases.ClasesAsociativas
{
    public class CantidadProducto : CantidadAlimento
    {
        public Producto Alimento { get; set; }
    }
}
