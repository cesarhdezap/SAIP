using InterfazDeUsuario.CallCenter;
using InterfazDeUsuario.Mesero;
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

        public InformacionDeCuenta()
        {
            InitializeComponent();

        }

        public void ActualizarCuenta(Cuenta cuenta)
        {
            LabelMesa.Content = cuenta.Mesa.ToString();
            DataGridPedidos.ItemsSource = cuenta.Pedidos;
            Cuenta = cuenta;
            
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

            Pedido pedido = ObtenerPedidoDeExpander(sender);
            MostrarAlimentosDelPedido(pedido);


            CambiarEstadoDeExpander(sender);
            Mouse.OverrideCursor = null;
        }

        //private DataGrid ObtenerDataGridCantidadPlatillos(object sender)
        //{
        //    Visual elementoVisual = sender as Visual;
        //    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(elementoVisual); i++)
        //    {
        //        VisualTreeHelper.GetChild(elementoVisual, i);

        //    }
        //}

        private Pedido ObtenerPedidoDeExpander(object expander)
        {
            Pedido pedido = new Pedido();
            for (Visual elementoVisual = expander as Visual; elementoVisual != null; elementoVisual = VisualTreeHelper.GetParent(elementoVisual) as Visual)
            {
                if (elementoVisual is DataGridRow fila)
                {
                    pedido = ((FrameworkElement)fila).DataContext as Pedido;
                    break;
                }
            }

            return pedido;
        }

        private void MostrarAlimentosDelPedido(Pedido pedido)
        {
            CantidadPlatilloDAO cantidadPlatilloDAO = new CantidadPlatilloDAO();
            CantidadProductoDAO cantidadProductoDAO = new CantidadProductoDAO();
            //DataGridCantidadPlatillo.ItemSource = cantidadPlatilloDAO.RecuperarCantidadProductoPorIDPedido(pedido.Id);
            //DataGridCantidadProducto.ItemSource = cantidadProductoDAO.RecuperarCantidadProductoPorIDPedido(pedido.Id);
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
    }
}
