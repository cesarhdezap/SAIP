using LogicaDeNegocio.ObjetosAccesoADatos;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.Clases
{
    public class Producto : Alimento
    {
        public int CantidadEnInventario { get; set; }
        public string CodigoDeBarras { get; set; }
        public double Costo { get; set; }
        public string Creador { get; set; }
        public bool Activo { get; set; }

        public override string ToString()
        {
            string resultado = string.Empty;
            resultado += "Nombre: " + Nombre;
            resultado += Environment.NewLine;
            resultado += "Codigo: " + Codigo;
            resultado += Environment.NewLine;
            resultado += "Precio: " + Precio;

            return resultado;
        }

        public override bool ValidarCantidadAlimento(int cantidadAValidar)
        {
            bool resultado = false;
            ProductoDAO productoDAO = new ProductoDAO();
            Producto productoActualizado = productoDAO.CargarPorID(Id);
            if (productoActualizado.CantidadEnInventario >= cantidadAValidar)
            {
                resultado = true;
            }

            return resultado;
        }

		internal void DescontarIngredientesDeInventario(int cantidad)
		{
            if (this.ValidarCantidadAlimento(cantidad))
            {
                ProductoDAO productoDAO = new ProductoDAO();
                this.CantidadEnInventario -= cantidad;
                productoDAO.ActualizarProducto(this);
            }
            else
            {
                throw new ArgumentException("No hay suficientes existencias para realizar el descuento");
            }
		}

        public void AumentarIngredienteInventario(int cantidad)
        {
            if (Activo)
            {
                ProductoDAO productoDAO = new ProductoDAO();
                this.CantidadEnInventario += cantidad;
                productoDAO.ActualizarProducto(this);
            }
            else
            {
                throw new ArgumentException("No se ha podido realizar el aumento");
            }
        }
       
	}
}
