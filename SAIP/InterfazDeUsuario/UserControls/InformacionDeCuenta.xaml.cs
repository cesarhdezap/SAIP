using InterfazDeUsuario.CallCenter;
using InterfazDeUsuario.Mesero;
using LogicaDeNegocio;
using LogicaDeNegocio.Clases;
using LogicaDeNegocio.Clases.ClasesAsociativas;
using LogicaDeNegocio.Enumeradores;
using LogicaDeNegocio.Interfaces;
using LogicaDeNegocio.ObjetosAccesoADatos;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static InterfazDeUsuario.UtileriasGráficas;

namespace InterfazDeUsuario.UserControls
{
    /// <summary>
    /// Interaction logic for InformacionDeCuenta.xaml
    /// </summary>
    public partial class InformacionDeCuenta : UserControl
    {
        Cuenta Cuenta;
        public Empleado Empleado;
        public ControladorDeCambioDePantalla Controlador;
        public IControladorInformacionDeCuenta ControladorInformacionDeCuenta;

        public InformacionDeCuenta()
        {
            InitializeComponent();
        }

        public void ActualizarCuenta(Cuenta cuenta)
        {
            Cuenta = cuenta;
            CargarAlimentosDePedidos();
            LabelCuenta.Content = "Cuenta: " + cuenta.Id + " " + cuenta.Estado.ToString();
            LabelMesa.Content = "Mesa: " + cuenta.Mesa.ToString();
            cuenta.CalcularPrecioTotal();

            LabelPrecioTotal.Content = "Precio total: " + cuenta.PrecioTotal;
            DataGridPedidos.ItemsSource = cuenta.Pedidos;
        }

        private void ButtonNuevoPedido_Click(object sender, RoutedEventArgs e)
        {
            GUIRegistrarPedidoLocal page = new GUIRegistrarPedidoLocal(Controlador, Empleado, Cuenta);
            Controlador.CambiarANuevaPage(page);
        }

        private void ButtonEditarPedido_Click(object sender, RoutedEventArgs e)
        {
            Pedido pedido = ((FrameworkElement)sender).DataContext as Pedido;
            GUIEditarPedido page = new GUIEditarPedido(Controlador, Empleado, pedido);
            Controlador.CambiarANuevaPage(page);

        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            CambiarEstadoDeExpander(sender);
            Mouse.OverrideCursor = null;
        }

        private void CargarAlimentosDePedidos()
        {
            foreach (Pedido pedido in Cuenta.Pedidos)
            {
                CantidadPlatilloDAO cantidadPlatilloDAO = new CantidadPlatilloDAO();
                CantidadProductoDAO cantidadProductoDAO = new CantidadProductoDAO();
                pedido.CantidadAlimentos = new List<CantidadAlimento>();
                pedido.CantidadAlimentos = pedido.CantidadAlimentos.Concat(cantidadPlatilloDAO.RecuperarPorIDPedido(pedido.Id)).ToList();
                pedido.CantidadAlimentos = pedido.CantidadAlimentos.Concat(cantidadProductoDAO.RecuperarPorIDPedido(pedido.Id)).ToList();
            }
        }

        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            CambiarEstadoDeExpander(sender);
        }

        public void CambiarEstadoDeExpander(object expander)
        {
            for (Visual elementoVisual = expander as Visual; elementoVisual != null; elementoVisual = VisualTreeHelper.GetParent(elementoVisual) as Visual)
            {
                if (elementoVisual is DataGridRow fila)
                {
                    if (fila.DetailsVisibility == Visibility.Visible)
                    {
                        fila.DetailsVisibility = Visibility.Collapsed;
                    }
                    else
                    {
                        fila.DetailsVisibility = Visibility.Visible;
                    }

                    break;
                }
            }
        }


        private void ButtonTerminarCuenta_Click(object sender, RoutedEventArgs e)
        {
            var resultado = MessageBox.Show("¿Desea cobrar esta cuenta?", "AVISO", MessageBoxButton.YesNo);
            if(resultado == MessageBoxResult.Yes)
            {
                CuentaDAO cuentaDAO = new CuentaDAO();
                Cuenta.Estado = EstadoCuenta.Terminada;
                Cuenta.CalcularPrecioTotal();
                cuentaDAO.ActualizarCuenta(Cuenta);

                MesaDAO mesaDAO = new MesaDAO();
                mesaDAO.CambiarEstadoPorID(Cuenta.Mesa.NumeroDeMesa, EstadoMesa.Disponible);
                ControladorInformacionDeCuenta.OcultarCuenta();
            }

            GUIReciboDeCuenta reciboDeCuenta = new GUIReciboDeCuenta(Cuenta);
            reciboDeCuenta.ShowDialog();
        }
    }
}
