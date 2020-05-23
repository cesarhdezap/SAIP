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
using System.Windows.Shapes;
using System.Windows.Threading;
using LogicaDeNegocio.Clases;
using LogicaDeNegocio.Clases.ClasesCompuestas;
using LogicaDeNegocio.ObjetosAccesoADatos;

namespace InterfazDeUsuario.CallCenter
{
	/// <summary>
	/// Interaction logic for GUIPedidoADomicilio.xaml
	/// </summary>
	public partial class GUIPedidoADomicilio : Page
	{
		public ControladorDeCambioDePantalla Controlador { get; set; }
		public Empleado EmpleadoDeCallCenter { get; set; } = new Empleado();
		public Iva Iva { get; set; } = new Iva();
		public List<Platillo> PlatillosCargados { get; set; } = new List<Platillo>();
		public List<Producto> ProductosCargados { get; set; }
		public GUIPedidoADomicilio(ControladorDeCambioDePantalla controlador, Empleado EmpleadoDeCallCenter)
		{
			InitializeComponent();
			this.EmpleadoDeCallCenter = EmpleadoDeCallCenter;
			IvaDAO ivaDAO = new IvaDAO(); 
			PlatilloDAO platilloDAO = new PlatilloDAO();
			ProductoDAO productoDAO = new ProductoDAO();
			Iva = ivaDAO.CargarIvaActual();
			IvaLabel.Content = "IVA(" + Iva.Valor + "%)";
			Controlador = controlador;
			BarraDeEstado.Controlador = controlador;
			ProductosCargados = productoDAO.CargarProductosActivos();
			PlatillosCargados = platilloDAO.CargarTodos();
		}

		private void BusquedaTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			string busqueda = BusquedaTextBox.Text;
			if (busqueda != string.Empty)
			{
				AlimentosVisibles = PlatillosCargados.TakeWhile(alimento => alimento.Nombre.ToLower().Contains(busqueda.ToLower())).ToList();
			}
			else
			{
				AlimentosVisibles = PlatillosCargados;
			}
			ActualizarPantalla();
		}

		private void ActualizarPantalla()
		{
			BusquedaDataGrid.ItemsSource = null;
			BusquedaDataGrid.ItemsSource = AlimentosVisibles;
		}



		private void ButtonAñadir_Click(object sender, RoutedEventArgs e)
		{

		}

		private void ButtonEliminar_Click(object sender, RoutedEventArgs e)
		{

		}

		private void CantidadDeIngrediente_TextChanged(object sender, RoutedEventArgs e)
		{

		}

		private void TextBox_Loaded(object sender, RoutedEventArgs e)
		{

		}

		private void CantidadDeIngrediente_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{

		}
	}
}
