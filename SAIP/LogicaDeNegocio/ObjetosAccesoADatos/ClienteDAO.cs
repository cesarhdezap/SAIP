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

        private Clases.Cliente ConvertirPedidoDatosALogica(AccesoADatos.Cliente clienteDatos)
        {
            Clases.Cliente clienteLogica = new Clases.Cliente()
            {
                Id = clienteDatos.Id,
                Nombre = clienteDatos.Nombre,
                Telefono = clienteDatos.Telefono,
                Comentario = clienteDatos.Comentarios,
            };

            return clienteLogica;
        }
    }
}
