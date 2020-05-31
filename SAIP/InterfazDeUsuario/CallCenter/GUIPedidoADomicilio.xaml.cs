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
		public Cliente Cliente { get; set; } = new Cliente();
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
			try
			{
				Iva = ivaDAO.CargarIvaActual();
			} 
			catch (InvalidOperationException e)
			{
				MessageBox.Show(e.Message + "Porfavor contacte a su administrador", "Error! ", MessageBoxButton.OK);
				controlador.Regresar();
			}

			IvaLabel.Content = "IVA(" + Iva.Valor * 10 + "%)";
			Controlador = controlador;
			BarraDeEstado.Controlador = controlador;
			try
			{
				ProductosCargados = productoDAO.CargarProductosActivos();
				PlatillosCargados = platilloDAO.CargarTodos();
			}
			catch (InvalidOperationException e)
			{
				MessageBox.Show(e.Message + "Porfavor contacte a su administrador", "Error! ", MessageBoxButton.OK);
				controlador.Regresar();
			}
			AlimentosCargados = AlimentosCargados.Concat(PlatillosCargados).ToList();
			AlimentosCargados = AlimentosCargados.Concat(ProductosCargados).ToList();
			AlimentosVisibles = AlimentosCargados;
			ActualizarPantalla();
			BarraDeEstado.ActualizarEmpleado(empleadoDeCallCenter);
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
			int cantidadNueva = 1;
			foreach(CantidadAlimento cantidadAlimento in Pedido.CantidadAlimentos)
			{
				if (cantidadAlimento is CantidadPlatillo cantidadPlatillo)
				{
					if(alimentoAAñadir is Platillo platillo)
					{
						if(alimentoAAñadir.Id == platillo.Id)
						{
							cantidadNueva = cantidadPlatillo.Cantidad + 1;
							break;
						}
					}
				}
				else if (cantidadAlimento is CantidadProducto cantidadProducto)
				{
					if (alimentoAAñadir is Producto producto)
					{
						if (alimentoAAñadir.Id == producto.Id)
						{
							cantidadNueva = cantidadProducto.Cantidad + 1;
							break;
						}
					}
				}
			}

			if (alimentoAAñadir.ValidarCantidadAlimento(cantidadNueva))
			{
				Pedido.AñadirAlimento(alimentoAAñadir);
				LabelErrorDeCantidad.Visibility = Visibility.Hidden;
				ActualizarPantalla();
			}
			else
			{
				LabelErrorDeCantidad.Visibility = Visibility.Visible;
				LabelErrorDeCantidad.Content = "No hay suficientes ingredientes para añadir un " + alimentoAAñadir.Nombre;	
			}

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
					int cantidadAAñadir = int.Parse(padre.Text);
					if (cantidad is CantidadPlatillo cantidadPlatillo)
					{
						if (cantidadPlatillo.Alimento.ValidarCantidadAlimento(cantidadAAñadir))
						{
							cantidad.Cantidad = cantidadAAñadir;
							LabelErrorDeCantidad.Visibility = Visibility.Hidden;
						}
						else
						{
							LabelErrorDeCantidad.Visibility = Visibility.Visible;
							LabelErrorDeCantidad.Content = "No hay suficientes existencias para añadir " + cantidadAAñadir + " de " + cantidadPlatillo.Alimento.Nombre;
						}
					}
					else if(cantidad is CantidadProducto cantidadProducto)
					{
						if (cantidadProducto.Alimento.ValidarCantidadAlimento(cantidadAAñadir))
						{
							cantidad.Cantidad = cantidadAAñadir;
							LabelErrorDeCantidad.Visibility = Visibility.Hidden;
						}
						else
						{
							LabelErrorDeCantidad.Visibility = Visibility.Visible;
							LabelErrorDeCantidad.Content = "No hay suficientes existencias para añadir " + cantidadAAñadir + " de " + cantidadProducto.Alimento.Nombre;
						}
					}
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
			string numeroTelefonico = ((TextBox)sender).Text;
			if (ValidarTelefono(numeroTelefonico))
			{
				ClienteDAO clienteDAO = new ClienteDAO();
				if (clienteDAO.ValidarExistenciaDeEmpleadoPorNumeroTelefonico(numeroTelefonico))
				{
					Cliente = clienteDAO.CargarClientePorNumeroTelefonico(numeroTelefonico);
				}
				if (Cliente != null)
				{
					AsignarClienteAPantalla();
					NumeroTelefonicoTextBox.IsEnabled = false;
					ButtonLimpiarCliente.IsEnabled = true;
				}
			}
		}

		private void AsignarClienteAPantalla()
		{
			NombreDeClienteTextBox.Text = Cliente.Nombre;
			if(Cliente.Direcciones.FirstOrDefault() != null)
			{
				DireccionClienteTextBlock.Text = Cliente.Direcciones.FirstOrDefault();
			}

			ComentariosClienteTextBlock.Text = Cliente.Comentario;
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
			ButtonLimpiarCliente.IsEnabled = false;
			NumeroTelefonicoTextBox.IsEnabled = true;
		}

		private void FinalizarButton_Click(object sender, RoutedEventArgs e)
		{
			if (ValidarCadenaVacioPermitido(ComentariosClienteTextBlock.Text) &&
				ValidarTelefono(NumeroTelefonicoTextBox.Text) &&
				ValidarNombre(NombreDeClienteTextBox.Text) &&
				ValidarCadena(DireccionClienteTextBlock.Text) &&
				ValidarCadenaVacioPermitido(ComentariosOrdenTextBlock.Text)
				&& Pedido.CantidadAlimentos.Count > 0)
			{
				Pedido.CalcularPrecioTotal();
				Pedido.Comentario = ComentariosOrdenTextBlock.Text;
				Pedido.Creador = EmpleadoDeCallCenter.Nombre;
				Pedido.Iva = Iva.Valor;
				Pedido.FechaDeCreacion = DateTime.Now;
				Cliente.Direcciones.Add(DireccionClienteTextBlock.Text);
				if(Cliente.Id <= 0)
				{
					Cliente.Telefono = NumeroTelefonicoTextBox.Text;
					Cliente.Comentario = ComentariosClienteTextBlock.Text;
					Cliente.Nombre = NombreDeClienteTextBox.Text;
					Cliente.Direcciones.Add(DireccionClienteTextBlock.Text);
					Cliente.NombreDelCreador = EmpleadoDeCallCenter.Nombre;
				}
				Cuenta cuenta = new Cuenta()
				{
					Direccion = DireccionClienteTextBlock.Text,
					Clientes = new List<Cliente>()
				{
					Cliente
				},
					Estado = LogicaDeNegocio.Enumeradores.EstadoCuenta.Abierta,
					Empleado = EmpleadoDeCallCenter,
					Pedidos = new List<Pedido>()
				{
					Pedido
				}
				};
				Pedido.Cuenta = cuenta;
				cuenta.PrecioTotal = Pedido.PrecioTotal;
				cuenta.CalcularPrecioTotal();
				CuentaDAO cuentaDAO = new CuentaDAO();
				cuentaDAO.CrearCuentaConPedidos(cuenta);
				Pedido.DescontarIngredientes();
				MessageBox.Show("Pedido realizado con exito", "¡Exito!");
				LimpiarPantalla();
				LimpiarCliente();
			}
			else
			{
				MessageBox.Show("Error, verifique los campos remarcados en rojo y que haya añadido por lo menos un producto al pedido", "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
				MostrarEstadoDeValidacionCadenaVacioPermitido(ComentariosClienteTextBlock);
				MostrarEstadoDeValidacionTelefono(NumeroTelefonicoTextBox);
				MostrarEstadoDeValidacionNombre(NombreDeClienteTextBox);
				MostrarEstadoDeValidacionCadena(DireccionClienteTextBlock);
				MostrarEstadoDeValidacionCadenaVacioPermitido(ComentariosOrdenTextBlock);
			}
		}

		private void ButtonLimpiarCliente_Click(object sender, RoutedEventArgs e)
		{
			LimpiarCliente();
		}

		private void LimpiarCliente()
		{
			ButtonLimpiarCliente.IsEnabled = false;
			NumeroTelefonicoTextBox.IsEnabled = true;
			Cliente = new Cliente();
			NombreDeClienteTextBox.Text = string.Empty;
			NumeroTelefonicoTextBox.Text = string.Empty;
			ComentariosClienteTextBlock.Text = string.Empty;
			DireccionClienteTextBlock.Text = string.Empty;

		}
	}
}
