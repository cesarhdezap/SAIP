using LogicaDeNegocio.Clases;
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
	public partial class GUIGerente : Window
	{
		public Empleado Gerente { get; set; }
		public GUIGerente(Empleado empleadoCargado)
		{
			InitializeComponent();
			Gerente = empleadoCargado;
			BarraDeEstado.ActualizarNombreDeUsuario(Gerente.Nombre);
		}

		private void RegistrarPlatilloButton_Click(object sender, RoutedEventArgs e)
		{
			GUIRegistrarPlatillo registrarPlatillo = new GUIRegistrarPlatillo(Gerente);
			Hide();
			registrarPlatillo.ShowDialog();
			Show();
		}
	}
}
