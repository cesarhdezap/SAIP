using LogicaDeNegocio.ObjetosAccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.Clases
{
	public class Platillo
	{
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public double CostoDeIngredientes { get; set; }
        public string Codigo { get; set; }
        public DateTime FechaDeCreacion { get; set; }
        public DateTime FechaDeModificacion { get; set; }
        public bool Activo { get; set; }
        public List<Proporcion> Proporciones { get; set; }
        
        public void CalcularCostoDeIngredientes()
        {
            CostoDeIngredientes = 0;
            List<Proporcion> proporcionesNuevas = new List<Proporcion>();
            foreach (Proporcion proporcion in Proporciones)
            {
                Proporcion nuevaProporcion = proporcion;
                IngredienteDAO ingredienteDAO = new IngredienteDAO();
                nuevaProporcion.Ingrediente = ingredienteDAO.CargarIngredientePorId(proporcion.Ingrediente.Id);

                proporcionesNuevas.Add(nuevaProporcion);
            }

            Proporciones = proporcionesNuevas;

            foreach (Proporcion proporcion in Proporciones)
            {
                CostoDeIngredientes += proporcion.Ingrediente.Costo * proporcion.Ingrediente.Costo;
            }

        }
    }
}
