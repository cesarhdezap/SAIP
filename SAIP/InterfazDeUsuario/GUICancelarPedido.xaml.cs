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
        Pedido Pedido;
        public CancelarPedido(ControladorDeCambioDePantalla control, Empleado empleado, Pedido pedido)
        {
            InitializeComponent();
            Controlador = control;
            Empleado = empleado;
            Pedido = pedido;
            barraEstado.Controlador = control;
            barraEstado.ActualizarEmpleado(empleado);
            InitializeComponent();
        }

        private void CancelarPedidoButton_Click(object sender, RoutedEventArgs e)
        {
            PedidoDAO pedidoDAO = new PedidoDAO();

            pedidoDAO.CambiarEstadoPedidoCancelar(Pedido);

        }

        private void RechazarCancelacionButton_Click(object sender, RoutedEventArgs e)
        {
            Controlador.Regresar();
        }
    }
}
