using LogicaDeNegocio.Clases;
using LogicaDeNegocio.Clases.ClasesCompuestas;
using LogicaDeNegocio.ObjetosAccesoADatos;
using LogicaDeNegocio.Servicios;
using System;
using System.Collections.Generic;
using System.IO;
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
	/// Interaction logic for GUIReporteDeInventario.xaml
	/// </summary>
	public partial class GUIReporteDeInventario : Page
	{
		public Empleado Gerente { get; set; }
		public ControladorDeCambioDePantalla Controlador { get; set; }
		public List<Discrepancia> Discrepancias { get; set; }
		public List<ObjetoDeInventario> objetosDeInventario { get; set; }
		public GUIReporteDeInventario(ControladorDeCambioDePantalla Controlador, Empleado EmpleadoCargado, List<Discrepancia> Discrepancias, List<ObjetoDeInventario> objetosDeInventario)
		{
			InitializeComponent();
			this.Controlador = Controlador;
			this.objetosDeInventario = objetosDeInventario;
			Gerente = EmpleadoCargado;
			this.Discrepancias = Discrepancias; 
			BarraDeEstado.Controlador = Controlador;
			DiscrepanciasDataGrid.ItemsSource = Discrepancias;
			InventarioDataGrid.ItemsSource = objetosDeInventario;
		}

		private void GuardarCambiosButton_Click(object sender, RoutedEventArgs e)
		{
			IngredienteDAO ingredienteDAO = new IngredienteDAO();
			ProductoDAO productoDAO = new ProductoDAO();
			foreach(Discrepancia discrepancia in Discrepancias)
			{
				if(discrepancia.TipoDeProducto == TipoDeProducto.Ingrediente)
				{
					Ingrediente ingrediente = ingredienteDAO.CargarIngredientePorId(discrepancia.Id);
					ingrediente.CantidadEnInventario = discrepancia.CantidadRegistrada;
					ingredienteDAO.ActualizarIngrediente(ingrediente);
				}
				else if(discrepancia.TipoDeProducto == TipoDeProducto.Producto)
				{
					Producto producto = productoDAO.CargarProductoPorID(discrepancia.Id);
					producto.CantidadEnInventario = (int)discrepancia.CantidadRegistrada;
					productoDAO.ActualizarProducto(producto);
				}
			}

			MessageBox.Show("¡Exito!");
			
		}

		private void GenerarReporteDeInventarioButton_Click(object sender, RoutedEventArgs e)
		{
			string direccionDeGuardado = UtileriasGráficas.MostrarVentanaDeSeleccionDeArchivos();
			if (!string.IsNullOrWhiteSpace(direccionDeGuardado))
			{
				try
				{


					ServiciosDeCreacionDePDF.GenerarReporteDeInventario(Discrepancias, objetosDeInventario, direccionDeGuardado);
					MessageBox.Show("¡El archivo fue creado con exito!", "¡Exito!");
					System.Diagnostics.Process.Start(direccionDeGuardado);
				}
				catch(IOException)
				{
					MessageBox.Show("No se pudo realizar el guardado porque el archivo indicado se encuentra abierto. Cierre el archivo e intentelo de nuevo", "Alerta");
				}
			}
		}
	}
}
