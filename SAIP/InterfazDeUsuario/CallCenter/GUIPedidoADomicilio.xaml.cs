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
using LogicaDeNegocio;
using LogicaDeNegocio.Clases;
using LogicaDeNegocio.Clases.ClasesAsociativas;
using LogicaDeNegocio.Clases.ClasesCompuestas;
using static LogicaDeNegocio.Servicios.ServiciosDeValidacion;
using static InterfazDeUsuario.UtileriasGráficas;
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
		public Pedido Pedido { get; set; } = new Pedido();
		public List<Platillo> PlatillosCargados { get; set; } 
		public List<Producto> ProductosCargados { get; set; }
		public List<Alimento> AlimentosCargados { get; set; } = new List<Alimento>();
		public List<Alimento> AlimentosVisibles { get; set; }
		private bool CandadoDeRefrescadoDeCajasDeTexto { get; set; } = true;
		public GUIPedidoADomicilio(ControladorDeCambioDePantalla controlador, Empleado empleadoDeCallCenter)
		{
			InitializeComponent();
			this.EmpleadoDeCallCenter = empleadoDeCallCenter;
			IvaDAO ivaDAO = new IvaDAO(); 
			PlatilloDAO platilloDAO = new PlatilloDAO();
			ProductoDAO productoDAO = new ProductoDAO();
			Iva = ivaDAO.CargarIvaActual();
			IvaLabel.Content = "IVA(" + Iva.Valor * 10 + "%)";
			Controlador = controlador;
			BarraDeEstado.Controlador = controlador;
			ProductosCargados = productoDAO.CargarProductosActivos();
			PlatillosCargados = platilloDAO.CargarTodos();
			AlimentosCargados = AlimentosCargados.Concat(PlatillosCargados).ToList();
			AlimentosCargados = AlimentosCargados.Concat(ProductosCargados).ToList();
			AlimentosVisibles = AlimentosCargados;
			ActualizarPantalla();
		}

		private void BusquedaTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			string busqueda = BusquedaTextBox.Text;
			if (busqueda != string.Empty)
			{
				AlimentosVisibles = AlimentosCargados.FindAll(alimento => alimento.Nombre.ToLower().Contains(busqueda.ToLower())).ToList();
			}
			else
			{
				AlimentosVisibles = AlimentosCargados;
			}
			ActualizarPantalla();
		}

		private void ActualizarPantalla()
		{
			BusquedaDataGrid.ItemsSource = null;
			BusquedaDataGrid.ItemsSource = AlimentosVisibles;
			AlimentosDataGrid.ItemsSource = null;
			AlimentosDataGrid.ItemsSource = Pedido.CantidadAlimentos;
			double totalSinIva = 0;
			double iva = 0;
			foreach(CantidadAlimento cantidad in Pedido.CantidadAlimentos)
			{
				if(cantidad is CantidadPlatillo cantidadPlatillo)
				{
					totalSinIva += cantidadPlatillo.Cantidad * cantidadPlatillo.Alimento.Precio;
				} 
				else if (cantidad is CantidadProducto cantidadProducto)
				{
					totalSinIva += cantidadProducto.Cantidad * cantidadProducto.Alimento.Precio;
				}
			}
			iva = Math.Round(totalSinIva * Iva.Valor, 2);
			IvaValorLabel.Content = iva;
			TotalValorLabel.Content = totalSinIva + iva;
		}

		private void LimpiarPantalla()
		{
			IvaDAO ivaDAO = new IvaDAO();
			PlatilloDAO platilloDAO = new PlatilloDAO();
			ProductoDAO productoDAO = new ProductoDAO();
			Iva = ivaDAO.CargarIvaActual();
			IvaLabel.Content = "IVA(" + Iva.Valor * 10 + "%)";
			ProductosCargados = productoDAO.CargarProductosActivos();
			PlatillosCargados = platilloDAO.CargarTodos();
			AlimentosCargados = new List<Alimento>();
			AlimentosCargados = AlimentosCargados.Concat(PlatillosCargados).ToList();
			AlimentosCargados = AlimentosCargados.Concat(ProductosCargados).ToList();
			AlimentosVisibles = AlimentosCargados;
			Pedido = new Pedido();
			ActualizarPantalla();
			BusquedaTextBox.Text = string.Empty;
			NombreDeClienteTextBox.Text = string.Empty;
			NumeroTelefonicoTextBox.Text = string.Empty;
			DireccionClienteTextBlock.Text = string.Empty;
			ComentariosClienteTextBlock.Text = string.Empty;
			ComentariosOrdenTextBlock.Text = string.Empty;
		}

		private void ButtonAñadir_Click(object sender, RoutedEventArgs e)
		{
			Alimento alimentoAAñadir = (Alimento)BusquedaDataGrid.SelectedItem;
			Pedido.AñadirAlimento(alimentoAAñadir);
			ActualizarPantalla();
		}

		private void ButtonEliminar_Click(object sender, RoutedEventArgs e)
		{
			CantidadAlimento alimentoAEliminar = ((FrameworkElement)sender).DataContext as CantidadAlimento;
			if(alimentoAEliminar is CantidadPlatillo platillo)
			{
				Pedido.EliminarAlimento(platillo.Alimento);
			}
			else if(alimentoAEliminar is CantidadProducto producto)
			{
				Pedido.EliminarAlimento(producto.Alimento);
			}

			ActualizarPantalla();
		}

		private void CantidadDeIngrediente_TextChanged(object sender, RoutedEventArgs e)
		{
			if (CandadoDeRefrescadoDeCajasDeTexto)
			{
				CantidadAlimento cantidad = ((FrameworkElement)sender).DataContext as CantidadAlimento;
				TextBox padre = sender as TextBox;
				if (padre.Text != string.Empty)
				{
					cantidad.Cantidad = int.Parse(padre.Text);
				}
				ActualizarPantalla();
			}
		}

		private void TextBox_Loaded(object sender, RoutedEventArgs e)
		{
			CantidadAlimento cantidad = ((FrameworkElement)sender).DataContext as CantidadAlimento;
			TextBox padre = sender as TextBox;
			CandadoDeRefrescadoDeCajasDeTexto = false;
			padre.Text = cantidad.Cantidad.ToString();
			CandadoDeRefrescadoDeCajasDeTexto = true;
		}

		private void CantidadDeIngrediente_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			TextBox padre = sender as TextBox;
			if (!ValidarEntradaDeDatosSoloEntero(e.Text))
			{
				e.Handled = true;
			}
		}

		private void NumeroTelefonicoTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			MostrarEstadoDeValidacionTelefono((TextBox)sender);
		}

		private void NombreDeClienteTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			MostrarEstadoDeValidacionNombre((TextBox)sender);
		}

		private void DireccionClienteTextBlock_TextChanged(object sender, TextChangedEventArgs e)
		{
			MostrarEstadoDeValidacionCadena((TextBox)sender);
		}

		private void ComentariosClienteTextBlock_TextChanged(object sender, TextChangedEventArgs e)
		{
			MostrarEstadoDeValidacionCadenaVacioPermitido((TextBox)sender);
		}

		private void ComentariosOrdenTextBlock_TextChanged(object sender, TextChangedEventArgs e)
		{
			MostrarEstadoDeValidacionCadenaVacioPermitido((TextBox)sender);
		}

		private void CancelarButton_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult resultado = MessageBox.Show("¿Esta seguro que desea cancelar? Los datos no se puede recuperar", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
			if(resultado == MessageBoxResult.Yes)
			{
				LimpiarPantalla();
			}
		}

		private void FinalizarButton_Click(object sender, RoutedEventArgs e)
		{
			PedidoDAO pedidoDAO = new PedidoDAO();
			//pedidoDAO.Guardar(Platillo);
		}
	}
}
