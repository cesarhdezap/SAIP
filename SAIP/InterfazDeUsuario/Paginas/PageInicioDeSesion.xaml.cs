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
using static LogicaDeNegocio.Servicios.ServiciosDeEncriptacion;
using LogicaDeNegocio.Enumeradores;
using InterfazDeUsuario.CallCenter;
using LogicaDeNegocio.ObjetosAccesoADatos;
using LogicaDeNegocio.Clases;
using static InterfazDeUsuario.UtileriasGráficas;
using InterfazDeUsuario.Gerente;
using InterfazDeUsuario.Mesero;
using LogicaDeNegocio;
using System.Xml;

namespace InterfazDeUsuario.Paginas
{
    /// <summary>
    /// Interaction logic for PageInicioDeSesion.xaml
    /// </summary>
    public partial class PageInicioDeSesion : Page
    {
        ControladorDeCambioDePantalla Controlador;
        public PageInicioDeSesion(ControladorDeCambioDePantalla controlador)
        {
            Controlador = controlador;
            InitializeComponent();
			NombreDeUsuarioTextBox.Focus();
            BarraDeEstado.OcultarNombreDeUsuarioYBotones();
		}

        private void IniciarSesionButton_Click(object sender, RoutedEventArgs e)
        {
			Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
			string nombreDeUsuario = NombreDeUsuarioTextBox.Text.Trim();
			string contraseña = ContraseñaPasswordbox.Password.Trim();

			if (ValidarCadena(nombreDeUsuario) && ValidarContraseña(contraseña))
			{
				contraseña = EncriptarCadena(contraseña);
				EmpleadoDAO empleadoDAO = new EmpleadoDAO();
				bool resultadoDeValidacion;
				try
				{
					resultadoDeValidacion = empleadoDAO.ValidarExistenciaDeNombreDeUsuarioYContraseña(nombreDeUsuario, contraseña);
				}
				catch (InvalidOperationException ex)
				{
					Mouse.OverrideCursor = null;
					MessageBox.Show("No se pudo establecer conexión a la base de datos, consulte a su técnico." + ex.Message, "Error!");
					resultadoDeValidacion = false;
					return;
				}

				if (resultadoDeValidacion)
				{
					Empleado empleadoCargado = empleadoDAO.CargarEmpleadoPorNombreDeUsuario(nombreDeUsuario);
					if (empleadoCargado.TipoDeEmpleado == TipoDeEmpleado.CallCenter)
					{
						GUIPedidoADomicilio pedidoADomicilio = new GUIPedidoADomicilio(Controlador, empleadoCargado);
						Controlador.CambiarANuevaPage(pedidoADomicilio);
					}
					else if (empleadoCargado.TipoDeEmpleado == TipoDeEmpleado.Gerente)
					{
						GUIGerente gerente = new GUIGerente(Controlador, empleadoCargado);
						Controlador.CambiarANuevaPage(gerente);
					}
					else if (empleadoCargado.TipoDeEmpleado == TipoDeEmpleado.Mesero)
					{
						GUIVerMisMesas editarPedido = new GUIVerMisMesas(Controlador, empleadoCargado);
						Controlador.CambiarANuevaPage(editarPedido);
					}
				}
				else
				{
					MessageBox.Show("Contraseña o nombre de usuario invalido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
			Mouse.OverrideCursor = null;
		}

		private void NombreDeUsuarioTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			MostrarEstadoDeValidacionCadena((TextBox)sender);
		}

		private void ContraseñaPasswordbox_PasswordChanged(object sender, RoutedEventArgs e)
		{
			MostrarEstadoDeValidacionContraseña((PasswordBox)sender);
		}

		private void Page_Initialized(object sender, EventArgs e)
		{

		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			EmpleadoDAO empleadoDAO = new EmpleadoDAO();
			List<Empleado> empleados = empleadoDAO.CargarTodos();
			if (!empleados.Any(em => em.TipoDeEmpleado == TipoDeEmpleado.Gerente))
			{
				MessageBox.Show("No existe un gerente en la base de datos, a continuación sera llevado a una pantalla para que pueda registrar tanto un gerente como un técnico nuevo. Si esta no es la primera ves que se corre el SAIP es posible que necesite contactar a su técnico para restaurar un respaldo.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
				GUIRegistroDeGerenteYTécnico registroDeGerenteYTécnico = new GUIRegistroDeGerenteYTécnico(Controlador);
				Controlador.CambiarANuevaPage(registroDeGerenteYTécnico);
			}
		}
	}
}
