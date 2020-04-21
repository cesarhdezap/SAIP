using InterfazDeUsuario.Mesero;
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
using LogicaDeNegocio.Clases;
using LogicaDeNegocio.ObjetosAccesoADatos;
using LogicaDeNegocio.Servicios;
using InterfazDeUsuario.Gerente;
using LogicaDeNegocio.Enumeradores;
using InterfazDeUsuario.CallCenter;
using static LogicaDeNegocio.Servicios.ServiciosDeValidacion;
using static LogicaDeNegocio.Servicios.ServiciosDeEncriptacion;
using static InterfazDeUsuario.UtileriasGráficas;

namespace InterfazDeUsuario
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class GUIInicioDeSesion : Window
	{
		public GUIInicioDeSesion()
		{
			InitializeComponent();
			BarraDeEstado.OcultarNombreDeUsuarioYBotones();
		}

		private void IniciarSesionButton_Click(object sender, RoutedEventArgs e)
		{ 
			string nombreDeUsuario = NombreDeUsuarioTextBox.Text;
			string contraseña = ContraseñaPasswordbox.Password;
			if (ValidarCadena(nombreDeUsuario) && ValidarContraseña(contraseña))
			{
				contraseña = EncriptarCadena(contraseña);
				EmpleadoDAO empleadoDAO = new EmpleadoDAO();
				bool resultadoDeValidacion = empleadoDAO.ValidarExistenciaDeNombreDeUsuarioYContraseña(nombreDeUsuario, contraseña);
				if (resultadoDeValidacion)
				{
					Empleado empleadoCargado = empleadoDAO.CargarEmpleadoPorNombreDeUsuario(nombreDeUsuario);
					if (empleadoCargado.TipoDeEmpleado == TiposDeEmpleados.CallCenter.ToString())
					{
						GUIPedidoADomicilio pedidoADomicilio = new GUIPedidoADomicilio(empleadoCargado);
						Hide();
						pedidoADomicilio.ShowDialog();
						Show();
					}
					else if (empleadoCargado.TipoDeEmpleado == TiposDeEmpleados.Gerente.ToString())
					{
						GUIGerente gerente = new GUIGerente(empleadoCargado);
						Hide();
						gerente.ShowDialog();
						Show();
					}
					else if (empleadoCargado.TipoDeEmpleado == TiposDeEmpleados.Mesero.ToString())
					{

					}
					else if (empleadoCargado.TipoDeEmpleado == TiposDeEmpleados.Mesero.ToString())
					{
						GUIVerMisMesas verMisMesas = new GUIVerMisMesas(empleadoCargado);
						verMisMesas.ShowDialog();
					}
				}
				else
				{
					MessageBox.Show("Contraseña o nombre de usuario invalido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}

		private void NombreDeUsuarioTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			MostrarEstadoDeValidacionCadena((TextBox)sender);
		}

		private void ContraseñaPasswordbox_PasswordChanged(object sender, RoutedEventArgs e)
		{
			MostrarEstadoDeValidacionContraseña((PasswordBox)sender);
		}
	}
}
