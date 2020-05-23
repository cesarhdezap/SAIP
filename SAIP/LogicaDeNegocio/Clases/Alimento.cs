using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.Clases
{
    public abstract class Alimento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public string Codigo { get; set; }

        public abstract bool ValidarCantidadAlimento(int cantidadAValidar);
    }
}
