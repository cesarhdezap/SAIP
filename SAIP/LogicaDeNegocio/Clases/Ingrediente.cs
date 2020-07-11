using LogicaDeNegocio.Enumeradores;
using LogicaDeNegocio.ObjetosAccesoADatos;
using System;
using System.Collections.Generic;
using static LogicaDeNegocio.Servicios.ServiciosDeValidacion;

namespace LogicaDeNegocio.Clases
{
    public class Ingrediente
    {
        public int Id { get; set; }
        public UnidadDeMedida UnidadDeMedida { get; set; }
        public string Nombre { get; set; }
        public double CantidadEnInventario { get; set; }
        public string CodigoDeBarras { get; set; }
        public double Costo { get; set; }
        public DateTime FechaDeCreacion { get; set; }
        public DateTime FechaDeModificacion { get; set; }
        public string Creador { get; set; }
        public string Codigo { get; set; }
        public bool Activo { get; set; }
        public List<Componente> Componentes { get; set; } = new List<Componente>();

        public double CalcularCosto()
        {
            double resultado;
            if (Componentes.Count > 0)
            {
                Costo = 0;
                foreach (Componente componente in Componentes)
                {
                    double cantidad = componente.Cantidad;
                    Costo += cantidad * componente.Ingrediente.CalcularCosto();
                }
            }

            resultado = Costo;
            return resultado;
        }

        public bool ValidarParaGuardar()
        {
            bool resultado = false;
            IngredienteDAO ingredienteDAO = new IngredienteDAO();

            if (ValidarNombre(Nombre)
                && ValidarNumeroDecimal(CantidadEnInventario.ToString())
                && ValidarNumeroDecimal(Costo.ToString())
                && ValidarCadena(Codigo)
                && ValidarCadena(CodigoDeBarras)
                && !ingredienteDAO.ValidarCodigoExistente(Codigo)
                && !ingredienteDAO.ValidarCodigoExistente(CodigoDeBarras))
            {
                resultado = true;

                foreach (Componente componente in Componentes)
                {
                    if (!componente.Validar())
                    {
                        resultado = false;
                        break;
                    }
                }
            }
            return resultado;
        }

        internal void DescontarDeInventario(double cantidad)
        {
            if (Componentes.Count > 0)
            {
                foreach (Componente componente in Componentes)
                {
                    componente.Ingrediente.DescontarDeInventario(cantidad * componente.Cantidad);
                }
            }
            else
            {
                CantidadEnInventario -= cantidad;
                IngredienteDAO ingredienteDAO = new IngredienteDAO();
                ingredienteDAO.ActualizarIngrediente(this);
            }
        }

        public void AumentarEnInventario(double cantidad)
        {
            if(Componentes.Count > 0)
            {
                foreach(Componente componente in Componentes)
                {
                    componente.Ingrediente.AumentarEnInventario(cantidad * componente.Cantidad);
                }
            }
            else
            {
                CantidadEnInventario += cantidad;
                IngredienteDAO ingredienteDAO = new IngredienteDAO();
                ingredienteDAO.ActualizarIngrediente(this);
            }
        }
    }
}

