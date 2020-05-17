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
    /// Lógica de interacción para GUIEditarPedido.xaml
    /// </summary>
    public partial class GUIEditarPedido : Page
    {
        private List<Pedido> ListaDePedidos = new List<Pedido>();
        ControladorDeCambioDePantalla Controlador;
        Empleado Empleado;
        Pedido pedido;
        public GUIEditarPedido(ControladorDeCambioDePantalla control, Empleado empleado, Pedido pedido)
        {
            Controlador = control;
            Empleado = empleado;
            InitializeComponent();
            barraEstado.ActualizarNombreDeUsuario(empleado.NombreDeUsuario);
            MostrarPedido(pedido.Id);
        }

        public void MostrarPedido(int idPedido)
        {
            PedidoDAO pedidoDAO = new PedidoDAO();
            ListaDePedidos.Add(pedidoDAO.RecuperarPedidoPorId(idPedido));
        }

    }
}
