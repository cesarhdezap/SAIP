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
using LogicaDeNegocio.Enumeradores;
using InterfazDeUsuario.CallCenter;

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
		}

		private void IniciarSesionButton_Click(object sender, RoutedEventArgs e)
		{
			String nombreDeUsuario = NombreDeUsuarioTextBox.Text;
			String contraseña = ServiciosDeEncriptacion.EncriptarCadena(ContraseñaTextBox.Text);
			EmpleadoDAO empleadoDAO = new EmpleadoDAO();
			bool resultadoDeValidacion = empleadoDAO.ValidarExistenciaDeNombreDeUsuarioYContraseña(nombreDeUsuario, contraseña);
			if (resultadoDeValidacion)
			{
				Empleado empleadoCargado = empleadoDAO.CargarEmpleadoPorNombreDeUsuario(nombreDeUsuario);
				
				if (empleadoCargado.Activo)
				{
					if (empleadoCargado.TipoDeEmpleado == TiposDeEmpleados.CallCenter.ToString())
					{
						GUIPedidoADomicilio pedidoADomicilio = new GUIPedidoADomicilio(empleadoCargado);
						pedidoADomicilio.ShowDialog();
					}
				}
			}
			else
			{
				NombreDeUsuarioTextBox.Text = "Nope :(";
			}
		}
	}
}
