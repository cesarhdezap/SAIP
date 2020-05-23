using AccesoADatos;
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
                cuentaDb.Mesa = context.Mesas.Find(cuenta.Mesa.NumeroDeMesa);
                cuentaDb.Mesa.Estado = (short)EstadoMesa.Ocupada;
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
                Mesa = mesa.ConvertirMesaDatosALogica(cuenta.Mesa),               Cliente = clienteDAO.ConvertirListaDeDatosALogica(cuenta.Clientes.ToList()) 
                
                //Traducir datos de la cuenta
            };

            try
            {
                cuentaLogica.Clientes = clienteDAO.ConvertirListaDeClientesDatosALogica(cuenta.Clientes.ToList());
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
