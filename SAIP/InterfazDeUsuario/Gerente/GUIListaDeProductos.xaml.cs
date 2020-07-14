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
	/// Interaction logic for GUIListaDeProductos.xaml
	/// </summary>
	public partial class GUIListaDeProductos : Page
	{
		private Empleado Empleado;
		private ControladorDeCambioDePantalla Controlador;
		private List<Producto> productosCargados;
		private List<Producto> productosVisibles;

		public GUIListaDeProductos(ControladorDeCambioDePantalla controlador, Empleado empleado)
		{
			InitializeComponent();
			Empleado = empleado;
			BarraDeEstado.Controlador = controlador;
			BarraDeEstado.ActualizarEmpleado(empleado);
			Controlador = controlador;
			ProductoDAO productoDAO = new ProductoDAO();
			productosCargados = productoDAO.CargarTodos();
			productosVisibles = productosCargados;
			ActualizarPantalla();
		}

		private void ButtonEditar_Click(object sender, RoutedEventArgs e)
		{
			Producto productoSeleccionado = ((FrameworkElement)sender).DataContext as Producto;
			GUIEditarProducto editarProducto = new GUIEditarProducto(Controlador, Empleado, productoSeleccionado);
			Controlador.CambiarANuevaPage(editarProducto);
		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			Buscar(((TextBox) sender).Text);
		}

		private void ActualizarPantalla()
		{
			DataGridProductos.ItemsSource = null;
			DataGridProductos.ItemsSource = productosVisibles;
		}

		private void Buscar(string busqueda)
		{
			if (string.IsNullOrEmpty(busqueda))
			{
				productosVisibles = productosCargados.Where(p => p.Nombre.Contains(busqueda)).ToList();
			}
			else
			{
				productosVisibles = productosCargados;
			}
		}
	}
}
