using LogicaDeNegocio;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InterfazDeUsuario.CallCenter
{
    /// <summary>
    /// Lógica de interacción para GUIPrincipalCallCenter.xaml
    /// </summary>
    public partial class GUIPrincipalCallCenter : Page
    {
        Empleado EmpleadoCallCenter;
        ControladorDeCambioDePantalla Controlador;
        List<Pedido> Pedidos = new List<Pedido>();
        public GUIPrincipalCallCenter(ControladorDeCambioDePantalla controlador, Empleado empleadoDeCallCenter)
        {
            InitializeComponent();
            EmpleadoCallCenter = empleadoDeCallCenter;
            Controlador = controlador;
            BarraDeEstado.Controlador = controlador;
            BarraDeEstado.ActualizarEmpleado(empleadoDeCallCenter);
            MostrarPedidos();

        }

        private void MostrarPedidos()
        {
            PedidoDAO pedidoDAO = new PedidoDAO();
            Pedidos = pedidoDAO.CargarTodosRealizados();
            DataGridPedidos.ItemsSource = null;
            DataGridPedidos.ItemsSource = Pedidos;
        }

        private void DataGridPedidos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ButtonCambiarEstado_Click(object sender, RoutedEventArgs e)
        {
            Pedido pedido;
            try
            {
                pedido = (Pedido)DataGridPedidos.SelectedItem;
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("Seleccione un pedido válido");
                pedido = null;
            }

            if(pedido == null)
            {
                throw new NotImplementedException();
            }
            else
            {
                MessageBox.Show("Porfavor seleccione un pedido", "Aviso");
            }
        }

        private void ButtonEditar_Click(object sender, RoutedEventArgs e)
        {
            Pedido pedido;
            try
            {
                pedido = (Pedido)DataGridPedidos.SelectedItem;
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("Seleccione un pedido válido");
                pedido = null;
            }

            if (pedido == null)
            {
                throw new NotImplementedException();
            }
            else
            {
                MessageBox.Show("Porfavor seleccione un pedido", "Aviso");
            }
        }
    }
}
