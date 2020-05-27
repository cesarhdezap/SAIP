using LogicaDeNegocio.Clases;
using LogicaDeNegocio.Clases.ClasesAsociativas;
using LogicaDeNegocio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime FechaDeCreacion { get; set; }
        public List<CantidadAlimento> CantidadAlimentos { get; set; } = new List<CantidadAlimento>();
        public double PrecioTotal { get; set; }
        public double Iva { get; set; }
        public string Comentario { get; set; }
        public EstadoPedido Estado { get; set; }
        public Cuenta Cuenta { get; set; }
        public string Creador { get; set; }

        private bool AlimentoYaAñadido(Alimento alimento)
        {
            bool resultado = false;
            foreach (CantidadAlimento cantidad in CantidadAlimentos)
            {
                if (cantidad is CantidadProducto cantidadProducto)
                {
                    if (alimento is Producto producto)
                    {
                        if (cantidadProducto.Alimento.Id == alimento.Id)
                        {
                            resultado = true;
                        }
                    }
                } 
                else if (cantidad is CantidadPlatillo cantidadPlatillo)
                {
                    if (alimento is Platillo platillo)
                    {
                        if (cantidadPlatillo.Alimento.Id == alimento.Id)
                        {
                            resultado = true;
                        }
                    }
                }
            }
            return resultado;
        }

        public void AñadirAlimento(Alimento alimento)
        {
            if (!AlimentoYaAñadido(alimento))
            {
                if (alimento is Producto producto)
                {
                    CantidadAlimentos.Add(new CantidadProducto
                    {
                        Alimento = producto,
                        Cantidad = 1
                    });
                }
                else if (alimento is Platillo platillo)
                {
                    CantidadAlimentos.Add(new CantidadPlatillo
                    {
                        Alimento = platillo,
                        Cantidad = 1
                    });
                }
            }
            else
            {
                if (alimento is Producto producto)
                {
                    for (int i = 0; i < CantidadAlimentos.Count; i++)
                    {
                        if (CantidadAlimentos.ElementAt(i) is CantidadProducto cantidadProducto)
                        {
                            if (cantidadProducto.Alimento.Id == producto.Id)
                            {
                                CantidadAlimentos.ElementAt(i).Cantidad++;
                            }
                        }
                    }
                }
                else if (alimento is Platillo platillo)
                {
                    for (int i = 0; i < CantidadAlimentos.Count; i++)
                    {
                        if (CantidadAlimentos.ElementAt(i) is CantidadPlatillo cantidadPlatillo)
                        {
                            if (cantidadPlatillo.Alimento.Id == platillo.Id)
                            {
                                CantidadAlimentos.ElementAt(i).Cantidad++;
                            }
                        }
                    }
                }
            }
        }

        public void EliminarAlimento(Alimento alimento)
        {

            if (alimento is Producto producto)
            {
                for (int i = 0; i < CantidadAlimentos.Count; i++)
                {
                    if(CantidadAlimentos.ElementAt(i) is CantidadProducto cantidadProducto)
                    {
                        if (cantidadProducto.Alimento.Id == producto.Id)
                        {
                            CantidadAlimentos.RemoveAt(i);
                        }
                    }
                }
            }
            else if (alimento is Platillo platillo)
            {
                for (int i = 0; i < CantidadAlimentos.Count; i++)
                {
                    if (CantidadAlimentos.ElementAt(i) is CantidadPlatillo cantidadPlatillo)
                    {
                        if (cantidadPlatillo.Alimento.Id == platillo.Id)
                        {
                            CantidadAlimentos.RemoveAt(i);
                        }
                    }
                }
            }

            
        }

        public void DescontarIngredientes()
        {
            foreach(CantidadAlimento proporcion in CantidadAlimentos)
            {
                if(proporcion is CantidadPlatillo proporcionPlatillo)
                {
                    proporcionPlatillo.Alimento.DescontarIngredientes(proporcionPlatillo.Cantidad);
                }
                else if(proporcion is CantidadProducto proporcionProducto)
                {
                    proporcionProducto.Alimento.DescontarIngredientesDeInventario(proporcionProducto.Cantidad);
                }
            }
        }

        public void CalcularPrecioTotal()
        {
            double precioTotal = 0;
            foreach (CantidadAlimento cantidadAlimento in CantidadAlimentos)
            {
                if (cantidadAlimento is CantidadProducto cantidadProducto)
                {
                    precioTotal += cantidadProducto.Alimento.Precio * cantidadProducto.Cantidad;
                }
                else if (cantidadAlimento is CantidadPlatillo cantidadPlatillo)
                {
                    precioTotal += cantidadPlatillo.Alimento.Precio * cantidadPlatillo.Cantidad;
                }
            }

            PrecioTotal = precioTotal;
        }
    }
}
