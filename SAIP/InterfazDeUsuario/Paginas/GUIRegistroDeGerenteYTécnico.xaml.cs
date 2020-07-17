using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static LogicaDeNegocio.Servicios.ServiciosDeValidacion;
using static InterfazDeUsuario.UtileriasGráficas;
using static LogicaDeNegocio.Servicios.ServiciosDeEncriptacion;
using LogicaDeNegocio.ObjetosAccesoADatos;
using LogicaDeNegocio.Clases;

namespace InterfazDeUsuario.Paginas
{
	/// <summary>
	/// Interaction logic for GUIRegistroDeGerenteYTécnico.xaml
	/// </summary>
	public partial class GUIRegistroDeGerenteYTécnico : Page
	{
		private ControladorDeCambioDePantalla Controlador { get; set; }
		private Empleado Gerente { get; set; } = new Empleado();
		private Empleado Tecnico { get; set; } = new Empleado();
		public GUIRegistroDeGerenteYTécnico(ControladorDeCambioDePantalla controlador)
		{
			InitializeComponent();
			Controlador = controlador;
		}

		private void Nombre_TextChanged(object sender, TextChangedEventArgs e)
		{
			MostrarEstadoDeValidacionCadena((TextBox)sender);
		}

		private void Usuario_TextChanged(object sender, TextChangedEventArgs e)
		{
			MostrarEstadoDeValidacionCadena((TextBox)sender);
		}

		private void correo_TextChanged(object sender, TextChangedEventArgs e)
		{
			MostrarEstadoDeValidacionCorreoElectronico((TextBox)sender);
		}

		private void PasswordboxContraseña_PasswordChanged(object sender, RoutedEventArgs e)
		{
			MostrarEstadoDeValidacionContraseña((PasswordBox)sender);
		}

		private void Agregar_Click(object sender, RoutedEventArgs e)
		{
			if (ValidarCampos())
			{
				EmpleadoDAO empleadoDAO = new EmpleadoDAO();
				Gerente.Nombre = TextBoxNombreGerente.Text;
				Gerente.NombreDeUsuario = TextboxNombreDeUsuarioGerente.Text;
				Gerente.CorreoElectronico = TextboxCorreoGerente.Text;
				Gerente.Contraseña = EncriptarCadena(PasswordboxContraseñaGerente.Password);
				Gerente.Creador = Gerente.Nombre;
				Gerente.TipoDeEmpleado = LogicaDeNegocio.Enumeradores.TipoDeEmpleado.Gerente;
				try
				{
					empleadoDAO.GuardarEmpleado(Gerente);
				}
				catch(Exception ex)
				{
					MessageBox.Show("Hubo un problema registrando el gerente.", "Error");
					return;
				}
				Tecnico.Nombre = TextBoxNombreTecnico.Text;
				Tecnico.NombreDeUsuario = TextboxNombreDeUsuarioTecnico.Text;
				Tecnico.CorreoElectronico = TextboxCorreoTecnico.Text;
				Tecnico.Contraseña = EncriptarCadena(PasswordboxContraseñaTecnico.Password);
				Tecnico.Creador = Gerente.Nombre;
				Tecnico.TipoDeEmpleado = LogicaDeNegocio.Enumeradores.TipoDeEmpleado.Tecnico;
				//Tecnico.TipoDeEmpleado = LogicaDeNegocio.Enumeradores.TipoDeEmpleado.Tecnico;
				try
				{
					empleadoDAO.GuardarEmpleado(Tecnico);
				}
				catch (Exception ex)
				{
					MessageBox.Show("Hubo un problema registrando el técnico, sin embargo el gerente se registro correctamente", "Error");
					return;
				}
				MessageBox.Show("Gerente y técnico registrados!", "¡Exito!");
				Controlador.RegresarAInicioDeSesion();
			}
		}

		private bool ValidarCampos()
		{
			bool resultado = false;
			if (ValidarCadena(TextBoxNombreGerente.Text) &&
				ValidarCadena(TextboxNombreDeUsuarioGerente.Text) &&
				ValidarCorreoElectronico(TextboxCorreoGerente.Text) &&
				ValidarContraseña(PasswordboxContraseñaGerente.Password)&&
				ValidarCadena(TextBoxNombreTecnico.Text) &&
				ValidarCadena(TextboxNombreDeUsuarioTecnico.Text) &&
				ValidarCorreoElectronico(TextboxCorreoTecnico.Text) &&
				ValidarContraseña(PasswordboxContraseñaTecnico.Password))
			{
				resultado = true;
			}
			else
			{
				MostrarEstadoDeValidacionCadena(TextBoxNombreGerente);
				MostrarEstadoDeValidacionCadena(TextboxNombreDeUsuarioGerente);
				MostrarEstadoDeValidacionCorreoElectronico(TextboxCorreoGerente);
				MostrarEstadoDeValidacionContraseña(PasswordboxContraseñaGerente);
				MostrarEstadoDeValidacionCadena(TextBoxNombreTecnico);
				MostrarEstadoDeValidacionCadena(TextboxNombreDeUsuarioTecnico);
				MostrarEstadoDeValidacionCorreoElectronico(TextboxCorreoTecnico);
				MostrarEstadoDeValidacionContraseña(PasswordboxContraseñaTecnico);
			}

			return resultado;
		}
	}
}
