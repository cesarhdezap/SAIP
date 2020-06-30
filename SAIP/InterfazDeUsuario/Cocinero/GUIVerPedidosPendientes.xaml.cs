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
        private List<Pedido> Pedidos { get; set; }
        public Empleado Empleado { get; set; }
        ControladorDeCambioDePantalla Controlador;
        public GUI_VerPedidosPendientes(ControladorDeCambioDePantalla controlador, Empleado empleado)
        {
            Controlador = controlador;
            Empleado = empleado;
            InitializeComponent();
            BarraDeEstado.Controlador = controlador;
            Controlador = controlador;
            BarraDeEstado.ActualizarNombreDeUsuario(empleado.Nombre);
            MostrarPedidos();
        }

        public void MostrarPedidos()
        {
            PedidoDAO pedidoDAO = new PedidoDAO();
            ///Pedidos = pedidoDAO.CargarPendientes();
            DataGridPedidos.ItemsSource = null;
            DataGridPedidos.ItemsSource = Pedidos;
            ActualizarPantalla();
        }

        public void ActualizarPantalla()
        {

            DataGridPedidos.ItemsSource = Pedidos;
        }

        private void BotonVerReceta_Click(object sender, RoutedEventArgs e)
        {
            GUIVerRecetas gUIVerRecetas = new GUIVerRecetas(Controlador, Empleado);
            Controlador.CambiarANuevaPage(gUIVerRecetas);
        }
    }
}
