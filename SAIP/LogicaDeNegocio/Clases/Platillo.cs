using AccesoADatos;
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
        public void AñadirIngredientePorId(int id)
        {
            Ingrediente ingrediente = new Ingrediente();
            IngredienteDAO ingredienteDAO = new IngredienteDAO();
            if (!IngredienteYaAñadido(id))
            {   
                ingrediente = ingredienteDAO.CargarIngredientePorId(id);
                Proporciones.Add(new Proporcion
                {
                    Ingrediente = ingrediente,
                    Cantidad = 1
                });
            }
        }

        public void EliminarIngredientePorId(int id)
        {
            for (int i = 0;  i < Proporciones.Count; i++)
            {
                if (Proporciones.ElementAt(i).Ingrediente.Id == id)
                {
                    Proporciones.RemoveAt(i);
                }
            }
        }

        private bool IngredienteYaAñadido(int id)
        {
            bool resultado = false;
            foreach (Proporcion proporcion in Proporciones)
            {
                if (proporcion.Ingrediente.Id == id)
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
            ProporcionDAO proporcionDAO = new ProporcionDAO();
            Proporciones = proporcionDAO.CargarProporcionesPorIdPlatillo(Id);

            bool resultado = false;
            bool cantidadSuficiente = false;
            foreach(Proporcion proporcion in Proporciones)
            {
                if (proporcion.Ingrediente.Componentes.Count > 0)
                {
                    //Cantidad IngredienteComponente * Cantidad proporcion >= cantidad en base de ingrediente componente
                }
                else if(proporcion.Ingrediente.CantidadEnInventario >= cantidadAValidar * proporcion.Cantidad)
                {
                    cantidadSuficiente = true;
                }
            }

            if (cantidadSuficiente)
                resultado = true;

            return resultado;
        }

		internal void DescontarIngredientes(int cantidad)
		{
			foreach(Proporcion proporcion in Proporciones)
            {
                proporcion.Ingrediente.DescontarDeInventario(proporcion.Cantidad * cantidad);
            }
		}

        public void AumentarIngrediente(int cantidad)
        {
            foreach (Proporcion proporcion in Proporciones)
            {
                proporcion.Ingrediente.AumentarEnInventario(proporcion.Cantidad * cantidad);
            }
        }
	}
}
