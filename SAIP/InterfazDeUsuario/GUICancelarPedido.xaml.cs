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

namespace InterfazDeUsuario
{
    /// <summary>
    /// Lógica de interacción para CancelarPedido.xaml
    /// </summary>
    public partial class CancelarPedido : Page
    {
        ControladorDeCambioDePantalla Controlador;
        Empleado Empleado;
        Pedido pedido;
        public CancelarPedido(ControladorDeCambioDePantalla control, Empleado empleado, Pedido pedido)
        {
            Controlador = control;
            Empleado = empleado;
            InitializeComponent();
            barraEstado.Controlador = control;
            barraEstado.ActualizarEmpleado(empleado);
            InitializeComponent();
        }

        private void CancelarPedidoButton_Click(object sender, RoutedEventArgs e)
        {
            PedidoDAO pedidoDAO = new PedidoDAO();

            pedidoDAO.CambiarEstadoPedido(pedido);

        }

        private void RechazarCancelacionButton_Click(object sender, RoutedEventArgs e)
        {
            Controlador.Regresar();
        }
    }
}
