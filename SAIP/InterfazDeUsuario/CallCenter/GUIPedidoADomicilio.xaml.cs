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
using LogicaDeNegocio.ObjetosAccesoADatos;

namespace InterfazDeUsuario.CallCenter
{
	/// <summary>
	/// Interaction logic for GUIPedidoADomicilio.xaml
	/// </summary>
	public partial class GUIPedidoADomicilio : Window
	{

		public Empleado EmpleadoDeCallCenter { get; set; } = new Empleado();
		public Iva Iva { get; set; } = new Iva();
		public List<Alimento> TodosLostAlimentos { get; set; } = new List<Alimento>();
		public List<Alimento> AlimentosVisibles { get; set; } = new List<Alimento>();
		public GUIPedidoADomicilio(Empleado EmpleadoDeCallCenter)
		{
			InitializeComponent();
			this.EmpleadoDeCallCenter = EmpleadoDeCallCenter;
			NombreDeUsuarioLabel.Content = EmpleadoDeCallCenter.Nombre;
			IvaDAO ivaDAO = new IvaDAO();
			Iva = ivaDAO.CargarIvaActual();
			IvaLabel.Content = "IVA(" + Iva.Valor + "%)";
			AlimentoDAO alimentoDAO = new AlimentoDAO();
			TodosLostAlimentos = alimentoDAO.CargarTodos();
			AlimentosVisibles = TodosLostAlimentos;
			BusquedaDataGrid.ItemsSource = AlimentosVisibles;
			TodosLostAlimentos.ElementAt(0).CalcularCostoDeIngredientes();
			NombreDeClienteTextBox.Content = TodosLostAlimentos.ElementAt(0).CostoDeIngredientes;
			
			DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0,0,1), DispatcherPriority.Normal, delegate
			{
				HoraLabel.Content = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
			}, this.Dispatcher);
			HoraLabel.Content = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
		}

		private void BusquedaTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			string busqueda = BusquedaTextBox.Text;
			if (busqueda != string.Empty)
			{
				AlimentosVisibles = TodosLostAlimentos.TakeWhile(alimento => alimento.Nombre.ToLower().Contains(busqueda.ToLower())).ToList();
			}
			else
			{
				AlimentosVisibles = TodosLostAlimentos;
			}
			ActualizarPantalla();
		}

		private void ActualizarPantalla()
		{
			BusquedaDataGrid.ItemsSource = null;
			BusquedaDataGrid.ItemsSource = AlimentosVisibles;
		}
	}
}
