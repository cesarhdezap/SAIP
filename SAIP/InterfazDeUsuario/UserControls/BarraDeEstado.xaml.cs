using LogicaDeNegocio.Clases;
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
using System.Windows.Threading;

namespace InterfazDeUsuario.UserControls
{
	/// <summary>
	/// Interaction logic for BarraDeEstado.xaml
	/// </summary>
	public partial class BarraDeEstado : UserControl
	{
		public ControladorDeCambioDePantalla Controlador;

		public BarraDeEstado()
		{
			InitializeComponent();
			DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
			{
				HoraLabel.Content = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
			}, Dispatcher);
			HoraLabel.Content = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
		}
		

		public void ActualizarNombreDeUsuario(String nombreDeUsuario)
		{

			Empleado = empleado;
			AsignarNombreDeUsuario();
			MostrarBotones();
		}

		private void AsignarNombreDeUsuario()
		{
			NombreDeUsuarioLabel.Content = Empleado.Nombre;
		}

		private void MostrarBotones()
		{
			List<Button> botones = new List<Button>();
			switch (Empleado.TipoDeEmpleado)
			{
				case LogicaDeNegocio.Enumeradores.TipoDeEmpleado.CallCenter:
					botones.Add((Button)FindResource("ButtonCallCenterNuevoPedidoADomicilio"));
					botones.Add((Button)FindResource("ButtonCallCenterListaDeClientes"));
					botones.Add((Button)FindResource("ButtonCallCenterRegistrarCliente"));
				break;
				case LogicaDeNegocio.Enumeradores.TipoDeEmpleado.Gerente:
					botones.Add((Button)FindResource("ButtonGerenteVerMesas"));
					botones.Add((Button)FindResource("ButtonGerenteListaDePlatillos"));
					botones.Add((Button)FindResource("ButtonGerentePasarInventario"));
					botones.Add((Button)FindResource("ButtonGerenteVerEmpleados"));
					botones.Add((Button)FindResource("ButtonGerenteRegistrarEmpleado"));
					botones.Add((Button)FindResource("ButtonGerenteRegistrarPlatillo"));
					botones.Add((Button)FindResource("ButtonGerenteRegistrarMesa"));
					botones.Add((Button)FindResource("ButtonGerenteEditarImpuesto"));
					botones.Add((Button)FindResource("ButtonGerenteProductos"));
				break;
			}

			foreach(Button boton in botones)
			{
				UniformGridBotones.Children.Add(boton);
			}
		}

		public void OcultarNombreDeUsuarioYBotones()
		{
			NombreDeUsuarioLabel.Visibility = Visibility.Hidden;
			CambiarUsuarioButton.IsEnabled = false;
			RegresarButton.IsEnabled = false;
			CambiarUsuarioButton.Visibility = Visibility.Hidden;
			RegresarButton.Visibility = Visibility.Hidden;
		}

		private void RegresarButton_Click(object sender, RoutedEventArgs e)
		{
			Controlador.Regresar();
		}

		private void CambiarUsuarioButton_Click(object sender, RoutedEventArgs e)
		{
			Controlador.RegresarAInicioDeSesion();
		}

		private void ButtonCallCenterNuevoPedidoADomicilio_Click(object sender, RoutedEventArgs e)
		{
			
			GUIPedidoADomicilio pedidoADomicilio = new GUIPedidoADomicilio(Controlador, Empleado);
			Controlador.CambiarANuevaPage(pedidoADomicilio);
		}

		private void ButtonCallCenterListaDeClientes_Click(object sender, RoutedEventArgs e)
		{
			GUIVisualizarListaDeClientes visualizarListaDeClientes = new GUIVisualizarListaDeClientes(Controlador, Empleado);
			Controlador.CambiarANuevaPage(visualizarListaDeClientes);
		}

		private void ButtonGerenteVerMesas_Click(object sender, RoutedEventArgs e)
		{
			//GUI_VerMesas verMesas = new GUI_VerMesas(Controlador, Empleado);
			//Controlador.CambiarANuevaPage(visualizarListaDeClientes);
			//Falta ponerle controlador a la pantalla
			MessageBox.Show("Falta ponerle controlador a la pantalla");
			throw new NotImplementedException("Falta ponerle controlador a la pantalla");
		}

		private void ButtonGerenteListaDePlatillos_Click(object sender, RoutedEventArgs e)
		{
			//GUIVerPlatillos verPlatillos = new GUIVerPlatillos(Controlador, Empleado);
			//Controlador.CambiarANuevaPage(verPlatillos)
			//Pantalla aun no implementada
			MessageBox.Show("Esta pantalla aun no esta implementada");
			throw new NotImplementedException("Esta pantalla aun no esta implementada");
		}

		private void ButtonGerentePasarInventario_Click(object sender, RoutedEventArgs e)
		{
			GUIPasarInventario pasarInventario = new GUIPasarInventario(Controlador, Empleado);
			Controlador.CambiarANuevaPage(pasarInventario);
		}

		private void ButtonGerenteRegistrarEmpleado_Click(object sender, RoutedEventArgs e)
		{
			GUIRegistrarEmpleado registrarEmpleado = new GUIRegistrarEmpleado(Controlador, Empleado);
			Controlador.CambiarANuevaPage(registrarEmpleado);
		}

		private void ButtonGerenteRegistrarPlatillo_Click(object sender, RoutedEventArgs e)
		{
			GUIRegistrarPlatillo registrarPlatillo = new GUIRegistrarPlatillo(Controlador, Empleado);
			Controlador.CambiarANuevaPage(registrarPlatillo);
		}

		private void ButtonGerenteVerEmpleados_Click(object sender, RoutedEventArgs e)
		{
			GUIVerEmpleados verEmpleados = new GUIVerEmpleados(Controlador, Empleado);
			Controlador.CambiarANuevaPage(verEmpleados);
		}

		private void ButtonGerenteRegistrarMesa_Click(object sender, RoutedEventArgs e)
		{
			//GUIRegistarMesa registrarMesa = new GUIRegistarMesa(Controlador, Empleado);
			//Controlador.CambiarANuevaPage(registrarMesa);
			//Falta ponerle controlador a la pantalla
			MessageBox.Show("Falta ponerle controlador a la pantalla");
			throw new NotImplementedException("Falta ponerle controlador a la pantalla");
		}

		private void ButtonCallCenterRegistrarCliente_Click(object sender, RoutedEventArgs e)
		{
			GUIRegistrarCliente registrarCliente = new GUIRegistrarCliente(Controlador, Empleado);
			Controlador.CambiarANuevaPage(registrarCliente);
		}

		private void ButtonGerenteEditarImpuesto_Click(object sender, RoutedEventArgs e)
		{
			GUIEditarImpuesto editarImpuesto = new GUIEditarImpuesto(Controlador, Empleado);
			Controlador.CambiarANuevaPage(editarImpuesto);
		}

		private void ButtonGerenteProductos_Click(object sender, RoutedEventArgs e)
		{
			GUIListaDeProductos listaDeProductos = new GUIListaDeProductos(Controlador, Empleado);
			Controlador.CambiarANuevaPage(listaDeProductos);
		}
	}
}
