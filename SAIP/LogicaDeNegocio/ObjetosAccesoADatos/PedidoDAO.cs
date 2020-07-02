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
            if (pedido.CantidadAlimentos.Count > 0 && ValidarCantidadDeCantidadAlimentos(pedido.CantidadAlimentos))
            {
                pedido.FechaDeCreacion = DateTime.Now;
                IvaDAO ivaDAO = new IvaDAO();
                pedido.Iva = ivaDAO.CargarIvaActual().Valor;
                pedido.PrecioTotal = CalcularPrecioTotal(pedido);
            }
            else
            {
                throw new ArgumentException("Pedido inválido");
            }

            AccesoADatos.Pedido pedidoAGuardar = ConvertirPedidoLogicaADatos(pedido);
            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                context.Pedidos.Add(pedidoAGuardar);
                context.SaveChanges();
            }

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

        public List<Pedido> CargarPendientes1()
        {
            List<AccesoADatos.Pedido> Pendiente = new List<AccesoADatos.Pedido>();
            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                Pendiente = context.Pedidos.ToList();
            }
            List<Pedido> pedidoL = new List<Pedido>();
            ///pedidoL = ConvertirPedidoDeDatosALogica(Pendiente);
            return pedidoL;
        }

        public Pedido CargarPendientes(int estadoPedido)
        {
            AccesoADatos.Pedido pedidoP = new AccesoADatos.Pedido();
            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                pedidoP = context.Pedidos.Find(estadoPedido);
            }

            Pedido pedidoLogico = new Pedido();
            /*if(pedidoP != null)
            {
                pedidoLogico = ConvertirPedidoDeDatosALogica(pedidoP);
            }*/
            pedidoLogico = ConvertirPedidoDeDatosALogica(pedidoP);
            return pedidoLogico;
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

        private AccesoADatos.Pedido ConvertirPedidoLogicaADatos(Pedido pedidoLogica)
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

        
    }
}
