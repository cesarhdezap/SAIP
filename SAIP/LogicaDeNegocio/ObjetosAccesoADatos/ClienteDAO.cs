﻿using AccesoADatos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.ObjetosAccesoADatos
{
    public class ClienteDAO
    {
        public void Guardar(Clases.Cliente cliente)
        {
            Cliente clienteDb = ConvertirClienteLogicaADatos(cliente);
            clienteDb.Activo = true;
            clienteDb.FechaDeCreacion = DateTime.Now;
            clienteDb.FechaDeModicacion = DateTime.Now;
            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                context.Clientes.Add(clienteDb);
                context.SaveChanges();
            }
        }

        public void Actualizar(Clases.Cliente cliente)
        {
            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                Cliente clienteDb = context.Clientes.Find(cliente.Id);
                clienteDb.FechaDeModicacion = DateTime.Now;
                clienteDb.Nombre = cliente.Nombre;
                clienteDb.Telefono = cliente.Telefono;
                clienteDb.Comentarios = cliente.Comentario;

                List<Direcciones> direcciones = new List<Direcciones>();
                foreach (string direccion in cliente.Direcciones)
                {
                    Direcciones direccionTabla = new Direcciones
                    {
                        Direccion = direccion
                    };
                    direcciones.Add(direccionTabla);
                }
                context.Direcciones.RemoveRange(clienteDb.Direcciones);
                clienteDb.Direcciones.Clear();
                clienteDb.Direcciones = direcciones;
                context.SaveChanges();
            }
        }

        public void DarDeBaja(Clases.Cliente cliente)
        {
            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                Cliente clienteDb = context.Clientes.Find(cliente.Id);
                if (clienteDb != null)
                {
                    clienteDb.Activo = false;
                    context.SaveChanges();
                }
                else
                {
                    throw new ArgumentException("Id no encontrada ClienteDAO.DarDeBaja");
                }
            }
        }
        public List<Clases.Cliente> CargarTodosActivos()
        {
            List<Cliente> clientes = new List<Cliente>();
            using(ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                clientes = context.Clientes.Where(c => c.Activo == true)
                    .Include(c => c.Direcciones)
                    .Include(c => c.Cuenta)
                    .ToList();
            }

            return ConvertirListaDeDatosALogica(clientes);
        }
        
        private AccesoADatos.Cliente ConvertirClienteLogicaADatos(Clases.Cliente cliente)
        {
            AccesoADatos.Cliente clienteDatos = new AccesoADatos.Cliente
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Telefono = cliente.Telefono,
                Comentarios = cliente.Comentario,
                NombreCreador = cliente.NombreDelCreador
            };

            foreach(string direccion in cliente.Direcciones)
            {
                Direcciones direccionTabla = new Direcciones();
                direccionTabla.Direccion = direccion;
                clienteDatos.Direcciones.Add(direccionTabla);
            }

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
            };
            try
            {
                clienteLogica.Direcciones = ConvertirListaDeDirecciones(clienteDatos.Direcciones);
            }
            catch (Exception)
            {
            }

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

        public List<Clases.Cliente> ConvertirListaDeDatosALogica(List<AccesoADatos.Cliente> clientes)
        {
            List<Clases.Cliente> resultado = new List<Clases.Cliente>();
            foreach (Cliente cliente in clientes)
            {
                resultado.Add(ConvertirClienteDatosALogica(cliente));
            }

            return resultado;
        }
        public Clases.Cliente CargarClientePorNumeroTelefonico(string numeroTelefonico)
        {
            Cliente cliente = new Cliente();

            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                cliente = context.Clientes.Include(c => c.Direcciones).Include(c => c.Cuenta).FirstOrDefault(c => c.Telefono == numeroTelefonico);

            }
            return ConvertirClienteDatosALogica(cliente);

        }

        public bool ValidarExistenciaDeEmpleadoPorNumeroTelefonico(string numeroTelefonico)
        {
            bool resultado = false;
            Cliente cliente = new Cliente();

            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                cliente = context.Clientes.FirstOrDefault(c => c.Telefono == numeroTelefonico);

                if (cliente != null)
                {
                    resultado = true;
                }
            }

            return resultado;
        }


        public Clases.Cliente RecuperarClientePorIdCuenta(int idCuenta)
        {
            Cliente cliente = new Cliente();
            using(ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                cliente = context.Clientes.FirstOrDefault(c => c.Cuenta.ToList().Exists(cuenta => cuenta.Id == idCuenta));
            }

            return ConvertirClienteDatosALogica(cliente);
        }
    }
}
