using LogicaDeNegocio;
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
            barraEstado.Controlador = control;
            barraEstado.AsignarUsuarioActual(empleado);
            MostrarPedido(pedido.Id);
        }

        public void MostrarPedido(int idPedido)
        {
            //List<Alimento> orden = new List<Alimento>();
            //List<int> cantidad = new List<int>();
            //PedidoDAO pedidoDAO = new PedidoDAO();
            //pedido = pedidoDAO.RecuperarPedidoPorId(idPedido);

            //foreach (CantidadPlatillo platillo in pedido.CantidadPlatillos)
            //{
            //    orden.Add(platillo.Platillo);
            //    cantidad.Add(platillo.Cantidad);
            //}
            //foreach (CantidadProducto producto in pedido.CantidadProductos)
            //{
            //    orden.Add(producto.Producto);
            //    cantidad.Add(producto.Cantidad);
            //}

        }

        public void MostrarCliente(int idCuenta)
        {
            ClienteDAO clienteDAO = new ClienteDAO();
            Cliente cliente = clienteDAO.RecuperarClientePorIdCuenta(idCuenta);
            TextBlockNombreCliente.Text = cliente.Nombre;
            TextBlockTelefonoCliente.Text = cliente.Telefono;
            TextBlockComentarioCliente.Text = cliente.Comentario;

        }

        private void ButtonEliminarAlimento_Click (object sender, RoutedEventArgs e)
        {

        }

        public void MostrarMenuDeAlimentos()
        {

        }
    }
}
