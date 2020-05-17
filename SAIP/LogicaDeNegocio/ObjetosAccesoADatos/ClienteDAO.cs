using AccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.ObjetosAccesoADatos
{
    public class ClienteDAO
    {
        private AccesoADatos.Cliente ConvertirPedidoLogicaADatos(Clases.Cliente cliente)
        {
            AccesoADatos.Cliente clienteDatos = new AccesoADatos.Cliente
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Telefono = cliente.Telefono,
                Comentarios = cliente.Comentario
            };
            return clienteDatos;
        }

        public Clases.Cliente ConvertirClienteDatosALogica(AccesoADatos.Cliente clienteDatos)
        {
            CuentaDAO cuentaDAO = new CuentaDAO();
            Clases.Cliente clienteLogica = new Clases.Cliente()
            {
                Id = clienteDatos.Id,
                Nombre = clienteDatos.Nombre,
                Telefono = clienteDatos.Telefono,
                Comentario = clienteDatos.Comentarios,
                Direcciones = ConvertirListaDeDirecciones(clienteDatos.Direcciones),
                Cuenta = cuentaDAO.ConvertirListaDeCuentasDatosALogica(clienteDatos.Cuenta)
            };

            return clienteLogica;
        }

        private List<string> ConvertirListaDeDirecciones(ICollection<Direcciones> direcciones)
        {
            List<string> resultado = new List<string>();
            foreach(Direcciones direccion in direcciones)
            {
                resultado.Add(direccion.Direccion);
            }

            return resultado;
        }

        public List<Clases.Cliente> ConvertirListaDeClientesDatosALogica(List<AccesoADatos.Cliente> clientes)
        {
            List<Clases.Cliente> resultado = new List<Clases.Cliente>();
            foreach (Cliente cliente in clientes)
            {
                resultado.Add(ConvertirClienteDatosALogica(cliente));
            }

            return resultado;
        }
    }
}
