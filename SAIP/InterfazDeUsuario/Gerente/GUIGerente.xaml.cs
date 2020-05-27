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
using System.Windows.Shapes;

namespace InterfazDeUsuario.Gerente
{
	/// <summary>
	/// Interaction logic for GUIGerente.xaml
	/// </summary>
	public partial class GUIGerente : Page
	{
		public Empleado Gerente { get; set; }
		private ControladorDeCambioDePantalla Controlador { get; set; }
    
		public GUIGerente(ControladorDeCambioDePantalla controlador, Empleado empleadoCargado)
		{
			InitializeComponent();
			Gerente = empleadoCargado;
			BarraDeEstado.Controlador = controlador;
			Controlador = controlador;
			BarraDeEstado.AsignarUsuarioActual(Gerente);
			Controlador = controlador;
			BarraDeEstado.Controlador = controlador;
		}

		private void RegistrarPlatilloButton_Click(object sender, RoutedEventArgs e)
		{
			GUIRegistrarPlatillo registrarPlatillo = new GUIRegistrarPlatillo(Controlador, Gerente);
			Controlador.CambiarANuevaPage(registrarPlatillo);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			PlatilloDAO platilloDAO = new PlatilloDAO();
			GUIEditarPlatillo editarPlatillo = new GUIEditarPlatillo(Controlador, Gerente, platilloDAO.CargarPlatilloPorId(1));
			Controlador.CambiarANuevaPage(editarPlatillo);
		}

		private void PasarInventarioButton_Click(object sender, RoutedEventArgs e)
		{
			GUIPasarInventario pasarInventario = new GUIPasarInventario(Controlador, Gerente);
			Controlador.CambiarANuevaPage(pasarInventario);
		}

		private void ButtonRegistrarIngrediente_Click(object sender, RoutedEventArgs e)
		{
			GUIRegistrarIngrediente registrarIngrediente = new GUIRegistrarIngrediente(Controlador, Gerente);
			Controlador.CambiarANuevaPage(registrarIngrediente);
		}

		private void Button_Click_Lista(object sender, RoutedEventArgs e)
		{
			GUIVerEmpleados verEmpleados = new GUIVerEmpleados(Controlador, Gerente);
			Controlador.CambiarANuevaPage(verEmpleados);
		}
	}
}
