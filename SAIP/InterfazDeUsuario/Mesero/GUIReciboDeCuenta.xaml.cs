using LogicaDeNegocio.Clases;
using LogicaDeNegocio.Clases.ClasesAsociativas;
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

namespace InterfazDeUsuario.Mesero
{
	/// <summary>
	/// Interaction logic for GUIReciboDeCuenta.xaml
	/// </summary>
	public partial class GUIReciboDeCuenta : Window
	{
		private Cuenta Cuenta { get; set; }
		private Iva Iva { get; set; }
		private List<CantidadAlimento> TodosLosAlimentos { get; set; }
		public GUIReciboDeCuenta(Cuenta cuenta)
		{

			InitializeComponent();
			IvaDAO ivaDAO = new IvaDAO();
			Iva = ivaDAO.CargarIvaActual();
			Cuenta = cuenta;
			cuenta.CalcularPrecioTotal();
			LabelCuenta.Content = "Cuenta: " + cuenta.Id + " " + cuenta.Estado.ToString();
			LabelIva.Content = Iva.Valor * cuenta.PrecioTotal;
			LabelPrecioTotal.Content =  (cuenta.PrecioTotal * Iva.Valor) + cuenta.PrecioTotal;
			DataGridPedidos.ItemsSource = cuenta.Pedidos;
			DataGridPedidos.ItemsSource = CargarAlimentosDePedidos();
		}


		private List<CantidadAlimento> CargarAlimentosDePedidos()
		{
			List<CantidadAlimento> alimentosCargados = new List<CantidadAlimento>();
			foreach (Pedido pedido in Cuenta.Pedidos)
			{
				CantidadPlatilloDAO cantidadPlatilloDAO = new CantidadPlatilloDAO();
				CantidadProductoDAO cantidadProductoDAO = new CantidadProductoDAO();
				alimentosCargados = alimentosCargados.Concat(cantidadPlatilloDAO.RecuperarPorIDPedido(pedido.Id)).ToList();
				alimentosCargados = alimentosCargados.Concat(cantidadProductoDAO.RecuperarPorIDPedido(pedido.Id)).ToList();
			}
			return alimentosCargados;
		}
	}
}
