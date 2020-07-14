using InterfazDeUsuario.empleado;
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
		public Empleado empleadoADesactivar { get; private set; }

		public GUIGerente(ControladorDeCambioDePantalla controlador, Empleado empleadoCargado)
		{
			InitializeComponent();
			Gerente = empleadoCargado;
			BarraDeEstado.Controlador = controlador;
			Controlador = controlador;
			BarraDeEstado.ActualizarEmpleado(Gerente);
			Controlador = controlador;
			BarraDeEstado.Controlador = controlador;
		}

		private void Button_Click_Lista(object sender, RoutedEventArgs e)
		{
			GUIVerEmpleados verEmpleados = new GUIVerEmpleados(Controlador, Gerente, empleadoADesactivar);
			Controlador.CambiarANuevaPage(verEmpleados);
		}
	}
}
