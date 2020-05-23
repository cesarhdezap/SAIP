using LogicaDeNegocio.ObjetosAccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.Clases
{
	public class Platillo : Alimento
	{
        public double CostoDeIngredientes { get; set; }
        public string Notas { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaDeCreacion { get; set; }
        public DateTime FechaDeModificacion { get; set; }
        public bool Activo { get; set; }
        public List<Proporcion> Proporciones { get; set; } = new List<Proporcion>();
        
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
        public void AñadirIngredientePorId(int Id)
        {
            Ingrediente ingrediente = new Ingrediente();
            IngredienteDAO ingredienteDAO = new IngredienteDAO();
            if (!IngredienteYaAñadido(Id))
            {
                ingrediente = ingredienteDAO.CargarIngredientePorId(Id);
                Proporciones.Add(new Proporcion
                {
                    Ingrediente = ingrediente,
                    Cantidad = 1
                });
            }
        }

        public void EliminarIngredientePorId(int Id)
        {
            for (int i = 0;  i < Proporciones.Count; i++)
            {
                if (Proporciones.ElementAt(i).Ingrediente.Id == Id)
                {
                    Proporciones.RemoveAt(i);
                }
            }
        }

        private bool IngredienteYaAñadido(int Id)
        {
            bool resultado = false;
            foreach (Proporcion proporcion in Proporciones)
            {
                if (proporcion.Ingrediente.Id == Id)
                {
                    resultado = true;
                }
            }
            return resultado;
        }

        public bool Validar()
        {
            bool resultado = false;

            if (Proporciones.Count > 0)
            {
                resultado = true;
            }

            return resultado;
        }

        public override bool ValidarCantidadAlimento(int cantidadAValidar)
        {
            throw new NotImplementedException();
        }
    }
}
