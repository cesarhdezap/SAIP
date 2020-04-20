using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.Clases
{
	public class Ingrediente
	{
        public int Id { get; set; }
        public string UnidadDeMedida { get; set; }
        public string Nombre { get; set; }
        public double CantidadEnInventario { get; set; }
        public string CodigoDeBarras { get; set; }
        public double Costo { get; set; }
        public DateTime FechaDeCreacion { get; set; }
        public DateTime FechaDeModificacion { get; set; }
        public string Creador { get; set; }
        public string Codigo { get; set; }
        public bool Activo { get; set; }
        public List<Ingrediente> Componentes { get; set; }

        public void CalcularCosto()
        {
            if (Componentes.Count > 0)
            {
                Costo = 0;
                foreach(Ingrediente componente in Componentes)
                {
                    componente.CalcularCosto();
                    Costo += componente.Costo;
                }
            }
        }
    }
}

