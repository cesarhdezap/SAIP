using AccesoADatos;
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
    public class CuentaDAO
    {
        /// <summary>
        /// Recupera las cuentas con estado <see cref="EstadoCuenta.Abierta"/> con Mesa, Empleado, Pedidos.CantidadPlatillo, Pedidos.CantidadProducto.
        /// </summary>
        /// <param name="empleado"></param>
        /// <returns></returns>
        public List<Clases.Cuenta> RecuperarCuentasAbiertasPorEmpleado(Clases.Empleado empleado)
        {
            List<AccesoADatos.Cuenta> cuentas = new List<AccesoADatos.Cuenta>();

            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                cuentas = context.Cuentas.Where(c => c.Empleado.Id == empleado.Id && c.Estado == (short)EstadoCuenta.Abierta)
                    .Include(c => c.Mesa)
                    .Include(c => c.Empleado)
                    .Include(c=> c.Pedidos)
                    .Include(c => c.Clientes)
                    .ToList();
            }

            return ConvertirListaDeCuentasDatosALogica(cuentas);
        }

        public void ActualizarCuenta(Clases.Cuenta cuenta)
        {
            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                Cuenta cuentaDb = context.Cuentas.Find(cuenta.Id);
                cuentaDb.Estado = (short)cuenta.Estado;
                cuentaDb.PrecioTotal = cuenta.PrecioTotal;
                //Actualizar pedidos
                context.SaveChanges();
            }

        }

        public void CrearCuenta(Clases.Cuenta cuenta)
        {
            var cuentaDb = new AccesoADatos.Cuenta()
            {
                Estado = (short)EstadoCuenta.Abierta
            };

            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                if(cuenta.Id >= 0)
                {
                    cuentaDb.Mesa = context.Mesas.Find(cuenta.Mesa.NumeroDeMesa);
                }
                cuentaDb.Mesa.Estado = (short)EstadoMesa.Ocupada;
                if (cuenta.Mesa != null)
                {
                    cuentaDb.Mesa = context.Mesas.Find(cuenta.Mesa.NumeroDeMesa);
                    cuentaDb.Mesa.Estado = (short)EstadoMesa.Ocupada;
                }
                cuentaDb.Empleado = context.Empleados.Find(cuenta.Empleado.Id);
                context.Cuentas.Add(cuentaDb);
                context.SaveChanges();
            }

        }

        public void CrearCuentaConPedidos(Clases.Cuenta cuenta)
        {
            var cuentaDb = new AccesoADatos.Cuenta()
            {
                Estado = (short)EstadoCuenta.Abierta
            };
            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                cuentaDb.Empleado = context.Empleados.Find(cuenta.Empleado.Id);
                foreach (Clases.Cliente cliente in cuenta.Clientes)
                {
                    if(cliente.Id > 0)
                    {
                        cuentaDb.Clientes.Add(context.Clientes.Find(cliente.Id));
                    }
                    else
                    {
                        cuentaDb.Clientes.Add(new Cliente
                        {
                            Telefono = cliente.Telefono,
                            Nombre = cliente.Nombre,
                            FechaDeCreacion = DateTime.Now,
                            FechaDeModicacion = DateTime.Now,
                            NombreCreador = cliente.NombreDelCreador,
                            Comentarios = cliente.Comentario,
                            Activo = true
                        });
                    }
                }
                foreach (Pedido pedido in cuenta.Pedidos)
                {

                    PedidoDAO pedidoDAO = new PedidoDAO();
                    if (pedidoDAO.ValidarCuentaParaGuardado(pedido) && pedidoDAO.ValidarCantidadDeCantidadAlimentos(pedido.CantidadAlimentos))
                    {
                        pedido.FechaDeCreacion = DateTime.Now;
                        IvaDAO ivaDAO = new IvaDAO();
                        pedido.Iva = ivaDAO.CargarIvaActual().Valor;
                        pedido.CalcularPrecioTotal();
                    }
                    else
                    {
                        throw new ArgumentException("Pedido no tiene contenido o no ");
                    }

                    AccesoADatos.Pedido pedidoAGuardar = pedidoDAO.ConvertirPedidoLogicaADatos(pedido);
                    foreach (CantidadAlimento cantidadAlimento in pedido.CantidadAlimentos)
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
                    cuentaDb.Pedidos.Add(pedidoAGuardar);
                }
                if (cuenta.Mesa != null)
                {
                    cuentaDb.Mesa = context.Mesas.Find(cuenta.Mesa.NumeroDeMesa);
                    cuentaDb.Mesa.Estado = (short)EstadoMesa.Ocupada;
                }
                cuentaDb.Empleado = context.Empleados.Find(cuenta.Empleado.Id);
                context.Cuentas.Add(cuentaDb);
                context.SaveChanges();
            }

        }

        public List<Clases.Cuenta> ConvertirListaDeCuentasDatosALogica(ICollection<Cuenta> cuentas)
        {
            List<Clases.Cuenta> cuentasLogica = new List<Clases.Cuenta>();
            foreach (AccesoADatos.Cuenta cuenta in cuentas)
            {
                cuentasLogica.Add(ConvertirCuentaDatosALogica(cuenta));
            }
            return cuentasLogica;
        }

        public Clases.Cuenta ConvertirCuentaDatosALogica(Cuenta cuenta)
        {
            MesaDAO mesa = new MesaDAO();
            ClienteDAO clienteDAO = new ClienteDAO();
            Clases.Cuenta cuentaLogica = new Clases.Cuenta()
            {
                Id = cuenta.Id,
                Estado = (EstadoCuenta) cuenta.Estado,
                PrecioTotal = cuenta.PrecioTotal,
                Mesa = mesa.ConvertirMesaDatosALogica(cuenta.Mesa),
                Clientes = clienteDAO.ConvertirListaDeDatosALogica(cuenta.Clientes.ToList())

                //Traducir datos de la cuenta
            };

            try
            {
                cuentaLogica.Clientes = clienteDAO.ConvertirListaDeDatosALogica(cuenta.Clientes.ToList());
            }
            catch(ObjectDisposedException)
            {
                cuentaLogica.Clientes = new List<Clases.Cliente>();
            }

            PedidoDAO pedidoDAO = new PedidoDAO();
            foreach(AccesoADatos.Pedido pedido in cuenta.Pedidos)
            {
                cuentaLogica.Pedidos.Add(pedidoDAO.ConvertirPedidoDeDatosALogica(pedido));
            }

            return cuentaLogica;
        }
    }
}
