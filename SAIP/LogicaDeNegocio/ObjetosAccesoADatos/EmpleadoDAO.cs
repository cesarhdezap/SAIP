using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoADatos;
using LogicaDeNegocio.Clases;


namespace LogicaDeNegocio.ObjetosAccesoADatos
{
	public class EmpleadoDAO
	{
		private AccesoADatos.Empleado ConvertirEmpleadoDeLogicaAEmpleadoDeAccesoADatos(Clases.Empleado EmpleadoLogica)
		{
			AccesoADatos.Empleado empleadoConvertido = new AccesoADatos.Empleado()
			{
				Id = EmpleadoLogica.Id,
				Contraseña = EmpleadoLogica.Contraseña,
				NombreDeUsuario = EmpleadoLogica.NombreDeUsuario,
				Nombre = EmpleadoLogica.Nombre,
				FechaDeCreacion = EmpleadoLogica.FechaDeCreacion,
				FechaDeModicacion = EmpleadoLogica.FechaDeModicacion,
				Creador = EmpleadoLogica.Creador,
				Activo = EmpleadoLogica.Activo
			};

			return empleadoConvertido;
		}

		private Clases.Empleado ConvertireEmpleadoDeAccesoADatosAEmpleadoDeLogica(AccesoADatos.Empleado EmpleadoDb)
		{
			Clases.Empleado empleadoConvertido = new Clases.Empleado()
			{
				Id = EmpleadoDb.Id,
				Contraseña = EmpleadoDb.Contraseña,
				NombreDeUsuario = EmpleadoDb.NombreDeUsuario,
				Nombre = EmpleadoDb.Nombre,
				FechaDeCreacion = EmpleadoDb.FechaDeCreacion,
				FechaDeModicacion = EmpleadoDb.FechaDeModicacion,
				Creador = EmpleadoDb.Creador,
				Activo = EmpleadoDb.Activo,
				TipoDeEmpleado = EmpleadoDb.TipoDeEmpleado
			};

			return empleadoConvertido;
		}

		private List<Clases.Empleado> ConvertirListaDeEmpleadosDeAccesoADatosAListaDeEmpleadosDeLogica(List<AccesoADatos.Empleado> EmpleadosDb)
		{
			List<Clases.Empleado> empleadosResultado = new List<Clases.Empleado>();
			foreach(AccesoADatos.Empleado empleadoDb in EmpleadosDb)
			{
				Clases.Empleado empleadoLogico = ConvertireEmpleadoDeAccesoADatosAEmpleadoDeLogica(empleadoDb);
				empleadosResultado.Add(empleadoLogico);
			}

			return empleadosResultado;
		}

		public Clases.Empleado CargarEmpleadoPorId(int Id)
		{
			AccesoADatos.Empleado empleadoDb;
			using(ModeloDeDatosContainer context = new ModeloDeDatosContainer())
			{
				empleadoDb = context.Empleados.Find(Id);
			}

			Clases.Empleado empleado;
			if (empleadoDb != null)
			{
				empleado = ConvertireEmpleadoDeAccesoADatosAEmpleadoDeLogica(empleadoDb);
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
			empleadosResultado = ConvertirListaDeEmpleadosDeAccesoADatosAListaDeEmpleadosDeLogica(empleadosDb);
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
					usuario = ConvertireEmpleadoDeAccesoADatosAEmpleadoDeLogica(EmpleadoDb);
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
				empleadoLocalizado = context.Empleados.FirstOrDefault(empleado => empleado.NombreDeUsuario == NombreDeUsuario && empleado.Contraseña == Contraseña);
			}
			if (empleadoLocalizado != null)
			{
				resultadoDeExistencia = true;
			}

			return resultadoDeExistencia;
		}
	}
}
