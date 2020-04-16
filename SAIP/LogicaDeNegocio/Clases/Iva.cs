using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.Clases
{
	public class Iva
	{
        public int Id { get; set; }
        public DateTime FechaDeInicio { get; set; }
        public double Valor { get; set; }
        public string Creador { get; set; }
        public DateTime FechaDeCreacion { get; set; }
        public bool Activo { get; set; }
    }
}
