using AccesoADatos;
using LogicaDeNegocio.Clases;
using LogicaDeNegocio.Clases.ClasesAsociativas;
using LogicaDeNegocio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.ObjetosAccesoADatos
{
    public class PedidoDAO
    {
        public void Guardar(Pedido pedido)
        {
            if (ValidarCuentaParaGuardado(pedido) && ValidarCantidadDeCantidadAlimentos(pedido.CantidadAlimentos))
            {
                pedido.FechaDeCreacion = DateTime.Now;
                IvaDAO ivaDAO = new IvaDAO();
                pedido.Iva = ivaDAO.CargarIvaActual().Valor;
                pedido.PrecioTotal = CalcularPrecioTotal(pedido);
            }
            else
            {
                throw new ArgumentException("Pedido no tiene contenido");
            }

            AccesoADatos.Pedido pedidoAGuardar = ConvertirPedidoLogicaADatos(pedido);
            pedido.DescontarIngredientes();
            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                foreach(CantidadAlimento cantidadAlimento in pedido.CantidadAlimentos)
                {
                    if (cantidadAlimento is CantidadProducto cantidadProducto)
                    {
                        ProductoPedido productoPedido = new ProductoPedido();
                        productoPedido.Cantidad = cantidadProducto.Cantidad;
                        productoPedido.Productos = context.Productos.Find(cantidadProducto.Alimento.Id);
                        pedidoAGuardar.ProductoPedido.Add(productoPedido);
                    }
                    else if (cantidadAlimento is CantidadPlatillo cantidadPlatillo)
                    {
                        PlatilloPedido platilloPedido = new PlatilloPedido();
                        platilloPedido.Cantidad = cantidadPlatillo.Cantidad;
                        platilloPedido.Platillo = context.Platillos.Find(cantidadPlatillo.Alimento.Id);
                        pedidoAGuardar.PlatilloPedidos.Add(platilloPedido);
                    }
                }

                pedidoAGuardar.Cuenta = context.Cuentas.Find(pedido.Cuenta.Id);
                context.Pedidos.Add(pedidoAGuardar);
                context.SaveChanges();
            }

        }

        public bool ValidarCuentaParaGuardado(Pedido pedido)
        {
            bool resultado = false;
            if (pedido.CantidadAlimentos.Count > 0)
            {
                resultado = true;
            }
            return resultado;
        }

        public bool ValidarCantidadDeCantidadAlimentos(List<CantidadAlimento> cantidadAlimentos)
        {
            bool discrepanciaEncontrada = false;

            foreach(CantidadAlimento cantidadAlimento in cantidadAlimentos)
            {
                if(cantidadAlimento is CantidadProducto cantidadProducto)
                {
                    if(!cantidadProducto.Alimento.ValidarCantidadAlimento(cantidadAlimento.Cantidad))
                    {
                        discrepanciaEncontrada = true;
                        break;
                    }
                }
                else
                {
                    if(cantidadAlimento is CantidadPlatillo cantidadPlatillo)
                    {
                        if (!cantidadPlatillo.Alimento.ValidarCantidadAlimento(cantidadPlatillo.Cantidad))
                        {
                            discrepanciaEncontrada = true;
                            break;
                        }
                    }
                }
            }

            return !discrepanciaEncontrada;
        }

        private double CalcularPrecioTotal(Pedido pedido)
        {
            double precioTotal = 0;
            foreach(CantidadAlimento cantidadAlimento in pedido.CantidadAlimentos)
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

            return precioTotal;
        }

        public Pedido RecuperarPedidoPorId(int idPedido) 
        {
            AccesoADatos.Pedido pedido;
            using(ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
               pedido = context.Pedidos.Find(idPedido);
            }
            Pedido pedidoLogico = new Pedido();
            if(pedido != null)
            {
                pedidoLogico = ConvertirPedidoDeDatosALogica(pedido);
            }

            return pedidoLogico;
        }

        public AccesoADatos.Pedido ConvertirPedidoLogicaADatos(Pedido pedidoLogica)
        {
            AccesoADatos.Pedido pedidoDatos = new AccesoADatos.Pedido
            {
                Id = pedidoLogica.Id,
                FechaDeCreacion = pedidoLogica.FechaDeCreacion,
                PrecioTotal = pedidoLogica.PrecioTotal,
                Iva = pedidoLogica.Iva,
                Estado = (short)pedidoLogica.Estado,
            };
            return pedidoDatos;
        }

        public Pedido ConvertirPedidoDeDatosALogica(AccesoADatos.Pedido pedidoDatos)
        {
            Pedido pedidoLogica = new Pedido()
            {
                Id = pedidoDatos.Id,
                FechaDeCreacion = pedidoDatos.FechaDeCreacion,
                PrecioTotal = pedidoDatos.PrecioTotal,
                Iva = pedidoDatos.Iva,
                Estado = (EstadoPedido)pedidoDatos.Estado,
            };

            return pedidoLogica;
        }

        public void CambiarEstadoPedido(Pedido pedido)
        {
            using(ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                if (pedido.Estado != EstadoPedido.EnEspera || pedido.Estado != EstadoPedido.Completado || pedido.Estado != EstadoPedido.Realizado || pedido.Estado != EstadoPedido.Entregado) {
                    AccesoADatos.Pedido pedidoDb = context.Pedidos.Find(pedido.Id);
                    if (pedidoDb != null)
                    {
                        pedidoDb.Estado = 6;
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new ArgumentException("Id no encontrada PedidoDAO.DarDeBaja");
                    }
                }
                else
                {
                    throw new ArgumentException("El pedido ya se encuentra en un estado imposible de cancelar");
                }
            }

            pedido.AumentarIngredientes();

        }

        public List<Pedido> CargarPendientes1()
        {
            List<AccesoADatos.Pedido> Pendiente = new List<AccesoADatos.Pedido>();
            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                Pendiente = context.Pedidos.ToList();
            }
            List<Pedido> pedidoL = new List<Pedido>();

            return pedidoL;
        }
    }
}
