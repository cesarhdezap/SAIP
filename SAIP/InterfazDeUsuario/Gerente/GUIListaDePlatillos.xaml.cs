using LogicaDeNegocio.Clases;
using LogicaDeNegocio.ObjetosAccesoADatos;
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

namespace InterfazDeUsuario.Gerente
{
	/// <summary>
	/// Interaction logic for GUIListaDePlatillos.xaml
	/// </summary>
	
	public partial class GUIListaDePlatillos : Page
	{
		private Empleado Empleado;
		private ControladorDeCambioDePantalla Controlador;
		private List<Platillo> PlatillosCargados;
		private List<Platillo> PlatillosVisibles;
		public GUIListaDePlatillos(ControladorDeCambioDePantalla controlador, Empleado empleado)
		{
			InitializeComponent();
			Empleado = empleado;
			BarraDeEstado.Controlador = controlador;
			BarraDeEstado.ActualizarEmpleado(empleado);
			Controlador = controlador;
			PlatilloDAO platilloDAO = new PlatilloDAO();
			PlatillosCargados = platilloDAO.CargarTodos();
			PlatillosVisibles = PlatillosCargados;
			foreach(Platillo platillo in PlatillosCargados)
			{
				platillo.CalcularCostoDeIngredientes();
			}
			ActualizarPantalla();
		}

		private void ButtonEditar_Click(object sender, RoutedEventArgs e)
		{
			Platillo platilloSeleccionado = ((FrameworkElement)sender).DataContext as Platillo;
			GUIEditarPlatillo editarPlatillo = new GUIEditarPlatillo(Controlador, Empleado, platilloSeleccionado);
			Controlador.CambiarANuevaPage(editarPlatillo);
		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			Buscar(((TextBox)sender).Text);
		}

		private void ActualizarPantalla()
		{
			DataGridPlatillos.ItemsSource = null;
			DataGridPlatillos.ItemsSource = PlatillosVisibles;
		}

		private void Buscar(string busqueda)
		{
			if (string.IsNullOrEmpty(busqueda))
			{
				PlatillosVisibles = PlatillosCargados.Where(p => p.Nombre.Contains(busqueda)).ToList();
			}
			else
			{
				PlatillosVisibles = PlatillosCargados;
			}
		}
	}
}
