using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.Clases
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double CantidadEnInventario { get; set; }
        public string CodigoDeBarras { get; set; }
        public double Costo { get; set; }
        public double CostoDeVenta { get; set; }
        public string Creador { get; set; }
        public bool Activo { get; set; }

        public bool DescontarIngredientesDeInventario(int CantidadADescontar)
        {
            throw new NotImplementedException();
        }
    }
}
