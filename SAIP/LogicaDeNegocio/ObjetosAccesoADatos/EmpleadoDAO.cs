﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoADatos;
using LogicaDeNegocio.Clases;
using LogicaDeNegocio.Enumeradores;

namespace LogicaDeNegocio.ObjetosAccesoADatos
{
	public class EmpleadoDAO
	{
		private AccesoADatos.Empleado ConvertirDeLogicaADatos(Clases.Empleado empleadoLogica)
		{
			AccesoADatos.Empleado empleadoConvertido = new AccesoADatos.Empleado()
			{
				Id = empleadoLogica.Id,
				Contraseña = empleadoLogica.Contraseña,
				NombreDeUsuario = empleadoLogica.NombreDeUsuario,
				Nombre = empleadoLogica.Nombre,
				FechaDeCreacion = empleadoLogica.FechaDeCreacion,
				FechaDeModicacion = empleadoLogica.FechaDeModicacion,
				NombreCreador = empleadoLogica.Creador,
				Activo = empleadoLogica.Activo,
				TipoDeEmpleado = (short)empleadoLogica.TipoDeEmpleado,
				CorreoElectronico = empleadoLogica.CorreoElectronico
			};

			return empleadoConvertido;
		}

		private Clases.Empleado ConvertirDeDatosALogica(AccesoADatos.Empleado empleadoDb)
		{
			Clases.Empleado empleadoConvertido = new Clases.Empleado()
			{
				Id = empleadoDb.Id,
				Contraseña = empleadoDb.Contraseña,
				NombreDeUsuario = empleadoDb.NombreDeUsuario,
				Nombre = empleadoDb.Nombre,
				FechaDeCreacion = empleadoDb.FechaDeCreacion,
				FechaDeModicacion = empleadoDb.FechaDeModicacion,
				Creador = empleadoDb.NombreCreador,
				Activo = empleadoDb.Activo,
				TipoDeEmpleado = (TipoDeEmpleado)empleadoDb.TipoDeEmpleado,
				CorreoElectronico = empleadoDb.CorreoElectronico
			};

			return empleadoConvertido;
		}

		private List<Clases.Empleado> ConvertirListaDeDatosALogica(List<AccesoADatos.Empleado> empleadosDb)
		{
			List<Clases.Empleado> empleadosResultado = new List<Clases.Empleado>();
			foreach (AccesoADatos.Empleado empleadoDb in empleadosDb)
			{
				Clases.Empleado empleadoLogico = ConvertirDeDatosALogica(empleadoDb);
				empleadosResultado.Add(empleadoLogico);
			}

			return empleadosResultado;
		}

		public Clases.Empleado CargarEmpleadoPorId(int Id)
		{
			AccesoADatos.Empleado empleadoDb;
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				empleadoDb = context.Empleados.Find(Id);
			}

			Clases.Empleado empleado;
			if (empleadoDb != null)
			{
				empleado = ConvertirDeDatosALogica(empleadoDb);
			}
			else
			{
				throw new InvalidOperationException("La id del empleado no existe");
			}
			return empleado;
		}

		public bool ValidarExistenciaDeNombreDeUsuario(string NombreDeUsuario)
		{
			List<AccesoADatos.Empleado> empleadosContext;
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				empleadosContext = context.Empleados.ToList();
			}
			bool resultadoDeExistencia = empleadosContext.Exists(usuario => usuario.NombreDeUsuario == NombreDeUsuario);

			return resultadoDeExistencia;
		}

		public List<Clases.Empleado> CargarTodos()
		{
			List<AccesoADatos.Empleado> empleadosDb = new List<AccesoADatos.Empleado>();
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				empleadosDb = context.Empleados.ToList();
			}

			List<Clases.Empleado> empleadosResultado = new List<Clases.Empleado>();
			empleadosResultado = ConvertirListaDeDatosALogica(empleadosDb);
			return empleadosResultado;

		}

		public Clases.Empleado CargarEmpleadoPorNombreDeUsuario(string NombreDeUsuario)
		{
			Clases.Empleado usuario = new Clases.Empleado();
			if (ValidarExistenciaDeNombreDeUsuario(NombreDeUsuario))
			{
				AccesoADatos.Empleado EmpleadoDb;

				using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
				{
					EmpleadoDb = context.Empleados.FirstOrDefault(usuarioBusqueda => usuarioBusqueda.NombreDeUsuario == NombreDeUsuario && usuarioBusqueda.Activo == true);
				}

				if (EmpleadoDb != null)
				{
					usuario = ConvertirDeDatosALogica(EmpleadoDb);
				}
			}
			else
			{
				throw new InvalidOperationException("El nombre de usuario no existe");
			}
			return usuario;
		}


		public bool ValidarExistenciaDeNombreDeUsuarioYContraseña(string NombreDeUsuario, string Contraseña)
		{
			bool resultadoDeExistencia = false;
			AccesoADatos.Empleado empleadoLocalizado;
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				//System.IO.FileNotFoundException Unable to resolve assembly NodeloDeDatos.csdl

				try
				{
					empleadoLocalizado = context.Empleados.FirstOrDefault(empleado => empleado.NombreDeUsuario == NombreDeUsuario && empleado.Contraseña == Contraseña);
				}
				catch (System.Data.Entity.Core.EntityException e)
				{
					throw new InvalidOperationException(e.Message);
				}
			}
			if (empleadoLocalizado != null)
			{
				resultadoDeExistencia = true;
			}

			return resultadoDeExistencia;
		}
		public void DesactivarEmpleado(Clases.Empleado empleado)
		{
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				AccesoADatos.Empleado empleadodesactivado = context.Empleados.Find(empleado.Id);
				if(empleadodesactivado != null)
				{
					empleadodesactivado.Activo = false;
					context.SaveChanges();
				}
				else
				{
					throw new ArgumentException("La id del Empleado no encontrada");
				}

			}

		}

		public void GuardarEmpleado(Clases.Empleado empleado)
		{
			empleado.Activo = true;
			empleado.FechaDeCreacion = DateTime.Now;
			empleado.FechaDeModicacion = DateTime.Now;
			AccesoADatos.Empleado empleadoguardado = ConvertirDeLogicaADatos(empleado);
			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				context.Empleados.Add(empleadoguardado);
				context.SaveChanges();
			}
		}

		public void EditarEmpleado(Clases.Empleado empleado)
		{
			empleado.FechaDeModicacion = DateTime.Now;
			empleado.Activo = true;

			using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				AccesoADatos.Empleado empleadoDb = ConvertirDeLogicaADatos(empleado);
				if (empleadoDb != null)
				{
					empleadoDb = context.Empleados.Find(empleado.Id);
					empleadoDb.Nombre = empleado.Nombre;
					empleadoDb.NombreDeUsuario = empleado.NombreDeUsuario;
					empleadoDb.Contraseña = empleado.Contraseña;
					empleadoDb.CorreoElectronico = empleado.CorreoElectronico;
					empleadoDb.TipoDeEmpleado = (short)empleado.TipoDeEmpleado;

					context.SaveChanges();
				}
				else
				{

				}

			}
		}
	}
}
