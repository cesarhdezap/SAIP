using InterfazDeUsuario.UserControls;
using LogicaDeNegocio;
using LogicaDeNegocio.Clases;
using LogicaDeNegocio.ObjetosAccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        
        private List<Pedido> PedidosEnProceso { get; set; }
        public Empleado Empleado { get; set; }
        ControladorDeCambioDePantalla Controlador;
        private bool Candado { get; set; }
        private Timer Timer { get; set; }
        public GUI_VerPedidosPendientes(ControladorDeCambioDePantalla controlador, Empleado empleado)
        {
            Controlador = controlador;
            Empleado = empleado;
            InitializeComponent();
            BarraDeEstado.Controlador = controlador;
            Controlador = controlador;
            BarraDeEstado.ActualizarEmpleado(empleado);
            MostrarPedidosEnProceso();
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(3);
            Candado = true;
            Timer = new Timer((e) =>
            {

                Dispatcher.Invoke(() =>
                {
                    if (Candado)
                    {
                        Candado = false;
                        MostrarPedidosEnProceso();
                        Candado = true;
                    }
                });

            }, null, startTimeSpan, periodTimeSpan);
        }

        

        public void MostrarPedidosEnProceso()
        {
            PedidoDAO pedidoDAO = new PedidoDAO();
            try
            {
                PedidosEnProceso = pedidoDAO.CargarRecientes();
            }catch(Exception e)
            {
                MessageBox.Show("Hubo un problema al conectarse con la base de datos", "Error");
            }
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
            MostrarPedidosEnProceso();
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
            MostrarPedidosEnProceso();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Pedido PedidoACargar = ((FrameworkElement)sender).DataContext as Pedido;
            if (PedidoACargar != null)
            {
                GUIVerRecetas verRecetas = new GUIVerRecetas(Controlador, Empleado, PedidoACargar);
                Controlador.CambiarANuevaPage(verRecetas);
            }
            else
            {
                MessageBox.Show("No se a seleccionado un Pedido", "Seleccionar Pedido", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}