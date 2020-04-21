using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.Clases
{
    public class Mesa
    {
        public EstadoMesa Estado;
        public int NumeroDeMesa;
    }

    public enum EstadoMesa
    {
        Disponible,
        Ocupada
    }
}
