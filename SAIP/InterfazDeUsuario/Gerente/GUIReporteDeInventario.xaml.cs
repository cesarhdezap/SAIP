using LogicaDeNegocio.Clases;
using LogicaDeNegocio.Clases.ClasesCompuestas;
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
	/// Interaction logic for GUIReporteDeInventario.xaml
	/// </summary>
	public partial class GUIReporteDeInventario : Page
	{
		public Empleado Gerente { get; set; }
		public ControladorDeCambioDePantalla Controlador { get; set; }
		public List<Discrepancia> Discrepancias { get; set; }
		public GUIReporteDeInventario(ControladorDeCambioDePantalla Controlador, Empleado EmpleadoCargado, List<Discrepancia> Discrepancias)
		{
			InitializeComponent();
			this.Controlador = Controlador;
			Gerente = EmpleadoCargado;
			this.Discrepancias = Discrepancias; 
			BarraDeEstado.Controlador = Controlador;
			DiscrepanciasDataGrid.ItemsSource = Discrepancias;
		}

		private void GuardarCambiosButton_Click(object sender, RoutedEventArgs e)
		{
			IngredienteDAO ingredienteDAO = new IngredienteDAO();
			ProductoDAO productoDAO = new ProductoDAO();
		}
	}
}
