using AccesoADatos;
using LogicaDeNegocio.Clases;
using LogicaDeNegocio.Clases.ClasesAsociativas;
using LogicaDeNegocio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.ObjetosAccesoADatos
{
    public class PedidoDAO
    {
        public void Guardar(Clases.Pedido pedido)
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

        public bool ValidarCuentaParaGuardado(Clases.Pedido pedido)
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

        public List<Clases.Pedido> CargarTodosRealizados()
        {
            List<AccesoADatos.Pedido> pedidos = new List<AccesoADatos.Pedido>();

            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                pedidos = context.Pedidos.Where(p => p.Estado == 1 || p.Estado == 2 || p.Estado == 3 || p.Estado == 7)
                    .ToList();
            }

            return ConvertirListaDeDatosALogicaConCuenta(pedidos);
        }

        private double CalcularPrecioTotal(Clases.Pedido pedido)
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

        public Clases.Pedido RecuperarPedidoPorId(int idPedido) 
        {
            AccesoADatos.Pedido pedido;
            using(ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
               pedido = context.Pedidos.Find(idPedido);
            }
            Clases.Pedido pedidoLogico = new Clases.Pedido();
            if(pedido != null)
            {
                pedidoLogico = ConvertirPedidoDeDatosALogica(pedido);
            }

            return pedidoLogico;
        }

        public AccesoADatos.Pedido ConvertirPedidoLogicaADatos(Clases.Pedido pedidoLogica)
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

        public Clases.Pedido ConvertirPedidoDeDatosALogica(AccesoADatos.Pedido pedidoDatos)
        {
            Clases.Pedido pedidoLogica = new Clases.Pedido()
            {
                Id = pedidoDatos.Id,
                FechaDeCreacion = pedidoDatos.FechaDeCreacion,
                PrecioTotal = pedidoDatos.PrecioTotal,
                Iva = pedidoDatos.Iva,
                Estado = (EstadoPedido)pedidoDatos.Estado
            };

            return pedidoLogica;
        }

        public Clases.Pedido ConvertirPedidoDeDatosALogicaConCuenta(AccesoADatos.Pedido pedidoDatos)
        {
            CuentaDAO cuentaDAO = new CuentaDAO();

            Clases.Pedido pedidoLogica = new Clases.Pedido()
            {
                Id = pedidoDatos.Id,
                FechaDeCreacion = pedidoDatos.FechaDeCreacion,
                PrecioTotal = pedidoDatos.PrecioTotal,
                Iva = pedidoDatos.Iva,
                Estado = (EstadoPedido)pedidoDatos.Estado,
                Cuenta = cuentaDAO.ConvertirCuentaSinMesaDatosALogica(pedidoDatos.Cuenta)
            };

            return pedidoLogica;
        }
        public List<Clases.Pedido> ConvertirListaDeDatosALogicaConCuenta(List<AccesoADatos.Pedido> pedidos)
        {
            List<Clases.Pedido> resultado = new List<Clases.Pedido>();
            foreach (AccesoADatos.Pedido pedido in pedidos)
            {
                resultado.Add(ConvertirPedidoDeDatosALogicaConCuenta(pedido));
            }

            return resultado;
        }

        public List<Clases.Pedido> ConvertirListaDeDatosALogica(List<AccesoADatos.Pedido> pedidos)
        {
            List<Clases.Pedido> resultado = new List<Clases.Pedido>();
            foreach (AccesoADatos.Pedido pedido in pedidos)
            {
                resultado.Add(ConvertirPedidoDeDatosALogica(pedido));
            }

            return resultado;
        }

        public void CambiarEstadoPedido(Clases.Pedido pedido, EstadoPedido estado)
        {
            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                if (pedido.Estado != EstadoPedido.Cancelado)
                {
                    AccesoADatos.Pedido pedidoDb = context.Pedidos.Find(pedido.Id);
                    if (pedidoDb != null)
                    {
                        pedidoDb.Estado = (short)estado;
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new ArgumentException("Id no encontrada PedidoDAO.DarDeBaja");
                    }
                }
                else
                {
                    throw new ArgumentException("El pedido ya se encuentra en un estado imposible de cambiar");
                }
            }

            pedido.AumentarIngredientes();

        }

        public void CambiarEstadoPedidoCancelar(Clases.Pedido pedido)
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
    }
}
