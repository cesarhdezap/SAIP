using InterfazDeUsuario.UserControls;
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

namespace InterfazDeUsuario.Cocinero
{
    /// <summary>
    /// Lógica de interacción para GUI_VerPedidosPendientes.xaml
    /// </summary>
    public partial class GUI_VerPedidosPendientes : Page
    {
        private List<Pedido> PedidosRegistrados { get; set; }
        private List<Pedido> PedidosEnProceso { get; set; }
        public Empleado Empleado { get; set; }
        ControladorDeCambioDePantalla Controlador;
        public GUI_VerPedidosPendientes(ControladorDeCambioDePantalla controlador, Empleado empleado)
        {
            Controlador = controlador;
            Empleado = empleado;
            InitializeComponent();
            BarraDeEstado.Controlador = controlador;
            Controlador = controlador;
            BarraDeEstado.ActualizarEmpleado(empleado);
            MostrarPedidosEnProceso();
        }

        

        public void MostrarPedidosEnProceso()
        {
            PedidoDAO pedidoDAO = new PedidoDAO();
            PedidosEnProceso = pedidoDAO.CargarAlimentos();
            DataGridPedidosEnProceso.ItemsSource = null;
            ActualizarPantalla();
        }

        public void ActualizarPantalla()
        {
            DataGridPedidosEnProceso.ItemsSource = null;
            DataGridPedidosEnProceso.ItemsSource = PedidosEnProceso;
        }


        private void ButtonEnProceso_Click(object sender, RoutedEventArgs e)
        {
            Pedido enproceso = ((FrameworkElement)sender).DataContext as Pedido;
            if (enproceso != null)
            {
                PedidoDAO pedidoDAO = new PedidoDAO();
                pedidoDAO.PedidoenEspera(enproceso);
                ActualizarPantalla();
            }
            else 
            {
                MessageBox.Show("No se a seleccionado un Pedido para Completar", "Seleccionar Pedido", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Pedido realizado = ((FrameworkElement)sender).DataContext as Pedido;
            if (realizado != null)
            {
                PedidoDAO pedidoDAO = new PedidoDAO();
                pedidoDAO.PedidoRealizado(realizado);
                ActualizarPantalla();
            }
            else
            {
                MessageBox.Show("No se a seleccionado un Pedido para Completar", "Seleccionar Pedido", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Pedido PedidoACargar = ((FrameworkElement)sender).DataContext as Pedido;
            if (PedidoACargar != null)
            {
                GUIVerRecetas verRecetas = new GUIVerRecetas(Controlador, Empleado);
                Controlador.CambiarANuevaPage(verRecetas);
            }
            else
            {
                MessageBox.Show("No se a seleccionado un Pedido", "Seleccionar Pedido", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}