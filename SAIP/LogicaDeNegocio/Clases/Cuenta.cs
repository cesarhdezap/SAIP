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
        public int PrecioTotal { get; set;}
        public Cliente Cliente { get; set;}

        public void AñadirPedido()
        {
            throw new NotImplementedException();
        }
    }

}
