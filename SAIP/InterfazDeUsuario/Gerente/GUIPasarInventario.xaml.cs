using LogicaDeNegocio.Clases;
using LogicaDeNegocio.Clases.ClasesCompuestas;
using LogicaDeNegocio.ObjetosAccesoADatos;
using LogicaDeNegocio.Enumeradores;
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

namespace InterfazDeUsuario.Gerente
{
	/// <summary>
	/// Interaction logic for GUIPasarInventario.xaml
	/// </summary>
	public partial class GUIPasarInventario : Page

	{
		public ControladorDeCambioDePantalla Controlador { get; set; }
		public List<Ingrediente> IngredientesCargados { get; set; }
		public List<Producto> ProductosCargados { get; set; }
		public List<ObjetoDeInventario> ObjetosDeInventarioCargados { get; set; }
		public List<ObjetoDeInventario> ObjetosDeInventarioVisibles { get; set; }
		public List<ObjetoDeInventario> ObjetosDeInventarioAñadidos { get; set; } = new List<ObjetoDeInventario>();
		public Empleado Gerente { get; set; }
		private bool CandadoDeRefrescadoDeCajasDeTexto { get;  set; }

		public GUIPasarInventario(ControladorDeCambioDePantalla controlador, Empleado empleadoCargado)
		{
			InitializeComponent();
			Gerente = empleadoCargado;
			Controlador = controlador;
			BarraDeEstado.Controlador = controlador;
			IngredienteDAO ingredienteDAO = new IngredienteDAO();
			ProductoDAO productoDAO = new ProductoDAO();
			ProductosCargados = productoDAO.CargarProductosActivos();
			IngredientesCargados = ingredienteDAO.CargarIngredientesActivos();
			ObjetosDeInventarioCargados = CombinarIngredientesYProductos(IngredientesCargados, ProductosCargados);
			ObjetosDeInventarioVisibles = ObjetosDeInventarioCargados;
			BusquedaDataGrid.ItemsSource = ObjetosDeInventarioVisibles;
		}

		private void BusquedaTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			string busqueda = BusquedaTextBox.Text;
			if (busqueda != string.Empty)
			{
				ObjetosDeInventarioVisibles = ObjetosDeInventarioCargados.FindAll(x => x.Nombre.ToLower().Contains(busqueda.ToLower()));
			}
			else
			{
				ObjetosDeInventarioVisibles = ObjetosDeInventarioCargados;
			}
			ActualizarPantalla();
		}

		private void ActualizarPantalla()
		{
			BusquedaDataGrid.ItemsSource = null;
			BusquedaDataGrid.ItemsSource = ObjetosDeInventarioVisibles;
			IngredientesOProductosDataGrid.ItemsSource = null; 
			IngredientesOProductosDataGrid.ItemsSource = ObjetosDeInventarioAñadidos;
		}

		private void CodigoDeBarrasTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			TextBox padre = sender as TextBox;
			foreach (ObjetoDeInventario ingredienteOProducto in ObjetosDeInventarioCargados)
			{
				if(ingredienteOProducto.CodigoDeBarras.ToLower() == padre.Text.ToLower())
				{
					AñadirIngredienteOProducto(ingredienteOProducto);
					padre.Text = string.Empty;
				}
			}
		}

		public void AñadirIngredienteOProducto(ObjetoDeInventario ingredienteOProducto)
		{
			if (!ObjetosDeInventarioAñadidos.Contains(ingredienteOProducto))
			{
				ingredienteOProducto.Cantidad = 1;
				ObjetosDeInventarioAñadidos.Add(ingredienteOProducto);
			}
			else
			{
				ObjetosDeInventarioAñadidos.FirstOrDefault(x => x.Id == ingredienteOProducto.Id && x.TipoDeProducto == ingredienteOProducto.TipoDeProducto).Cantidad++;
			}
			ActualizarPantalla();
		}

		public void EliminarIngredienteOProducto(ObjetoDeInventario ingredienteOProducto)
		{
			if (ObjetosDeInventarioAñadidos.Any(x => x.Id == ingredienteOProducto.Id && x.TipoDeProducto == ingredienteOProducto.TipoDeProducto))
			{
				ObjetosDeInventarioAñadidos.Remove(ingredienteOProducto);
			}
			ActualizarPantalla();
		}

		private void ButtonAñadir_Click(object sender, RoutedEventArgs e)
		{
			ObjetoDeInventario ingredienteOProductoAAñadir = (ObjetoDeInventario)BusquedaDataGrid.SelectedItem;
			AñadirIngredienteOProducto(ingredienteOProductoAAñadir);
		}

		private void CodigoDeBarrasTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			TextBox padre = sender as TextBox;
			if (!ValidarCadena(e.Text))
			{
				e.Handled = true;
			}
		}

		private List<ObjetoDeInventario> CombinarIngredientesYProductos(List<Ingrediente> ingredientes, List<Producto> productos)
		{
			List<ObjetoDeInventario> ingredientesYProductos = new List<ObjetoDeInventario>();

			foreach(Ingrediente ingrediente in ingredientes)
			{
				ObjetoDeInventario ingredienteAAñadir = new ObjetoDeInventario()
				{
					Id = ingrediente.Id,
					Nombre = ingrediente.Nombre,
					Codigo = ingrediente.Codigo,
					CodigoDeBarras = ingrediente.CodigoDeBarras,
					Cantidad = 0,
					Costo = ingrediente.Costo,
					UnidadDeMedida = ingrediente.UnidadDeMedida,
					TipoDeProducto = TipoDeProducto.Ingrediente
				};

				ingredientesYProductos.Add(ingredienteAAñadir);
			}

			foreach(Producto producto in productos)
			{
				ObjetoDeInventario ingredienteAAñadir = new ObjetoDeInventario()
				{
					Id = producto.Id,
					Nombre = producto.Nombre,
					CodigoDeBarras = producto.CodigoDeBarras,
					Cantidad = 0,
					Codigo = producto.Codigo,
					Costo = producto.Costo,
					UnidadDeMedida = UnidadDeMedida.Unidad,
					TipoDeProducto = TipoDeProducto.Producto
				};

				ingredientesYProductos.Add(ingredienteAAñadir);
			}

			return ingredientesYProductos;
		}

		private void ButtonEliminar_Click(object sender, RoutedEventArgs e)
		{
			ObjetoDeInventario ingredienteOProductoAAñadir = (ObjetoDeInventario)IngredientesOProductosDataGrid.SelectedItem;
			EliminarIngredienteOProducto(ingredienteOProductoAAñadir);
		}

		private void TextBox_Loaded(object sender, RoutedEventArgs e)
		{
			ObjetoDeInventario ingredienteOProducto = ((FrameworkElement)sender).DataContext as ObjetoDeInventario;
			TextBox padre = sender as TextBox;
			CandadoDeRefrescadoDeCajasDeTexto = false;
			padre.Text = ingredienteOProducto.Cantidad.ToString();
			CandadoDeRefrescadoDeCajasDeTexto = true;
		}

		private void CantidadDeIngrediente_TextChanged(object sender, RoutedEventArgs e)
		{
			if (CandadoDeRefrescadoDeCajasDeTexto)
			{
				ObjetoDeInventario proporcion = ((FrameworkElement)sender).DataContext as ObjetoDeInventario;
				TextBox padre = sender as TextBox;
				if (padre.Text != string.Empty)
				{
					proporcion.Cantidad = double.Parse(padre.Text);
				}
				ActualizarPantalla();
			}
		}

		private void CantidadDeIngrediente_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			TextBox padre = sender as TextBox;
			if (!ValidarEntradaDeDatosSoloEntero(e.Text))
			{
				e.Handled = true;
			}
		}

		private List<Discrepancia> CalcularDiscrepancias(List<ObjetoDeInventario> ingredientesOProductos)
		{
			List<Discrepancia> discrepancias = new List<Discrepancia>();
			foreach(ObjetoDeInventario ingredienteOProducto in ingredientesOProductos)
			{
				if (ingredienteOProducto.TipoDeProducto == TipoDeProducto.Ingrediente)
				{
					Ingrediente ingrediente = IngredientesCargados.FirstOrDefault(x => x.Id == ingredienteOProducto.Id);
					if (ingrediente.CantidadEnInventario != ingredienteOProducto.Cantidad)
					{
						discrepancias.Add(new Discrepancia
						{
							Id = ingrediente.Id,
							Nombre = ingrediente.Nombre,
							Codigo = ingrediente.Codigo,
							CantidadEsperada = ingrediente.CantidadEnInventario,
							CantidadRegistrada = ingredienteOProducto.Cantidad,
							Costo = ingrediente.Costo,
							UnidadDeMedida = ingrediente.UnidadDeMedida,
							TipoDeProducto = TipoDeProducto.Ingrediente
						});
					}
				}
				else
				{
					Producto producto = ProductosCargados.FirstOrDefault(x => x.Id == ingredienteOProducto.Id);
					if (producto.CantidadEnInventario != ingredienteOProducto.Cantidad)
					{
						discrepancias.Add(new Discrepancia
						{
							Id = producto.Id,
							Nombre = producto.Nombre,
							Codigo = producto.CodigoDeBarras,
							CantidadEsperada = producto.CantidadEnInventario,
							CantidadRegistrada = ingredienteOProducto.Cantidad,
							Costo = producto.Costo,
							UnidadDeMedida = UnidadDeMedida.Unidad,
							TipoDeProducto = TipoDeProducto.Producto
						});
					}
				}
			}
			return discrepancias;
		}

		private void ContinuarButton_Click(object sender, RoutedEventArgs e)
		{
			if (ObjetosDeInventarioAñadidos.Count >= 0)
			{ 
				GUIReporteDeInventario reporteDeInventario = new GUIReporteDeInventario(Controlador, Gerente, CalcularDiscrepancias(ObjetosDeInventarioAñadidos), ObjetosDeInventarioCargados);
				Controlador.CambiarANuevaPage(reporteDeInventario);
			}
			else
			{
				MessageBox.Show("Debe ingresar almenos un ingrediente o producto para pasar al inventario", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
	}
}
