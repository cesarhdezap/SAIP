using LogicaDeNegocio;
using LogicaDeNegocio.Clases;
using LogicaDeNegocio.Clases.ClasesAsociativas;
using LogicaDeNegocio.Enumeradores;
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

namespace InterfazDeUsuario.Mesero
{
    /// <summary>
    /// Interaction logic for GUIRegistrarPedidoLocal.xaml
    /// </summary>
    public partial class GUIRegistrarPedidoLocal : Page
    {
        Empleado Empleado;
        Cuenta Cuenta;
        List<CantidadAlimento> AlimentosDelPedido = new List<CantidadAlimento>();
        List<Alimento> AlimentosCargados = new List<Alimento>();
        readonly ControladorDeCambioDePantalla Controlador;
        const int TIEMPO_DE_ESPERA_CANTIDAD_INSUFICIENTE = 3000;
        const int TIEMPO_DE_ESPERA_NOTIFICACION_PEDIDO_REALIZADO = 2000;
        const int CANTIDAD_ALIMENTOS_POR_CLIC = 1;

        public GUIRegistrarPedidoLocal(ControladorDeCambioDePantalla controlador, Empleado empleado, Cuenta cuenta)
        {
            Controlador = controlador;
            Empleado = empleado;
            Cuenta = cuenta;
            InitializeComponent();
            BarraDeEstado.Controlador = controlador;
            BarraDeEstado.ActualizarNombreDeUsuario(empleado.NombreDeUsuario);
            MostrarAlimentos();

        }

        private void MostrarAlimentos()
        {
            PlatilloDAO platilloDAO = new PlatilloDAO();
            ProductoDAO productoDAO = new ProductoDAO();
            List<Alimento> alimentos = new List<Alimento>();
            alimentos = alimentos.Concat(platilloDAO.CargarTodos()).ToList();
            alimentos = alimentos.Concat(productoDAO.CargarProductosActivos()).ToList();
            AlimentosCargados = alimentos;
            ListBoxAlimentos.ItemsSource = alimentos;
        }

        private void ButtonBorrarAlimento_Click(object sender, RoutedEventArgs e)
        {
            CantidadAlimento alimento = ((FrameworkElement)sender).DataContext as CantidadAlimento;
            AlimentosDelPedido.Remove(alimento);
            DataGridAlimentosEnPedido.ItemsSource = null;
            DataGridAlimentosEnPedido.ItemsSource = AlimentosDelPedido;
        }

        private void ButtonAñadirAlimento_Click(object sender, RoutedEventArgs e)
        {
            Alimento alimento = ((FrameworkElement)sender).DataContext as Alimento;
            bool cantidadInsuficiente = false;

            CantidadAlimento cantidadAlimento = BuscarCantidadAlimento(alimento);
            if (cantidadAlimento == null)
            {
                if (alimento.ValidarCantidadAlimento(CANTIDAD_ALIMENTOS_POR_CLIC))
                {
                    if (alimento is Producto producto)
                    {
                        CantidadProducto cantidadProducto = new CantidadProducto();
                        cantidadProducto.Alimento = producto;
                        cantidadProducto.Cantidad = CANTIDAD_ALIMENTOS_POR_CLIC;
                        AlimentosDelPedido.Add(cantidadProducto);
                    }
                    else if (alimento is Platillo platillo)
                    {
                        CantidadPlatillo cantidadPlatillo = new CantidadPlatillo();
                        cantidadPlatillo.Alimento = platillo;
                        cantidadPlatillo.Cantidad = CANTIDAD_ALIMENTOS_POR_CLIC;
                        AlimentosDelPedido.Add(cantidadPlatillo);
                    }
                }
                else
                {
                    cantidadInsuficiente = true;
                }
            }
            else
            {
                if (alimento.ValidarCantidadAlimento(cantidadAlimento.Cantidad + CANTIDAD_ALIMENTOS_POR_CLIC))
                {
                    cantidadAlimento.Cantidad++;
                }
                else
                {
                    cantidadInsuficiente = true;
                }
            }

            if (cantidadInsuficiente)
            {
                MostrarMensajeCantidadInsuficiente(alimento.Nombre);
            }

            DataGridAlimentosEnPedido.ItemsSource = null;
            DataGridAlimentosEnPedido.ItemsSource = AlimentosDelPedido;
        }

        private void MostrarMensajeCantidadInsuficiente(string elemento)
        {
            StackPanelRealizarPedido.Visibility = Visibility.Collapsed;
            StackPanelConfirmacion.Visibility = Visibility.Visible;
            ButtonNotificacionConfirmar.Visibility = Visibility.Collapsed;
            ButtonNotificacionCancelar.Visibility = Visibility.Collapsed;
            LabelNotificacion.Foreground = Brushes.Red;
            LabelNotificacion.Content = elemento + " no tiene existencias.";

            Task.Delay(TIEMPO_DE_ESPERA_CANTIDAD_INSUFICIENTE).ContinueWith(_ =>
            {
                Dispatcher.Invoke(() =>
                {
                    StackPanelConfirmacion.Visibility = Visibility.Collapsed;
                    StackPanelRealizarPedido.Visibility = Visibility.Visible;
                    ButtonNotificacionConfirmar.Visibility = Visibility.Visible;
                    ButtonNotificacionCancelar.Visibility = Visibility.Visible;
                    LabelNotificacion.Foreground = Brushes.Black;
                });
            });
        }

        private void NotificarPedidoRealizado()
        {
            LabelNotificacion.Content = "Pedido realizado";
            LabelNotificacion.Foreground = Brushes.LightGreen;
            ButtonNotificacionConfirmar.IsEnabled = false;
            ButtonNotificacionCancelar.IsEnabled = false;
            Task.Delay(TIEMPO_DE_ESPERA_NOTIFICACION_PEDIDO_REALIZADO).ContinueWith(_ =>
            {
                Dispatcher.Invoke(() => { Controlador.Regresar(); });
            });
        }

        private void ButtonRealizarPedido_Click(object sender, RoutedEventArgs e)
        {
            ListBoxAlimentos.IsEnabled = false;
            LabelNotificacion.Content = "¿Desea Crear el pedido?";
            ButtonNotificacionConfirmar.Visibility = Visibility.Visible;
            ButtonNotificacionCancelar.Visibility = Visibility.Visible;
            StackPanelRealizarPedido.Visibility = Visibility.Collapsed;
            StackPanelConfirmacion.Visibility = Visibility.Visible;
        }

        private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
        {
            Controlador.Regresar();
        }

        private void ButtonNotificacionConfirmar_Click(object sender, RoutedEventArgs e)
        {
            Pedido pedido = new Pedido
            {
                Comentarios = new List<string>(),
                Creador = Empleado.Nombre,
                Empleado = Empleado,
                Cuenta = Cuenta,
                Estado = EstadoPedido.EnEspera,
                CantidadAlimentos = AlimentosDelPedido,
            };
            
            PedidoDAO pedidoDAO = new PedidoDAO();
            pedidoDAO.Guardar(pedido);

            NotificarPedidoRealizado();
        }

        private void ButtonNotificacionCancelar_Click(object sender, RoutedEventArgs e)
        {
            ListBoxAlimentos.IsEnabled = true;
            StackPanelRealizarPedido.Visibility = Visibility.Visible;
            StackPanelConfirmacion.Visibility = Visibility.Collapsed;
        }

        private void ButtonDisminuir_Click(object sender, RoutedEventArgs e)
        {
            CantidadAlimento cantidadAlimento = ((FrameworkElement)sender).DataContext as CantidadAlimento;
            if(cantidadAlimento.Cantidad - 1 > 0)
            {
                cantidadAlimento.Cantidad--;
            }
            else
            {
                AlimentosDelPedido.Remove(cantidadAlimento);
            }

            DataGridAlimentosEnPedido.ItemsSource = null;
            DataGridAlimentosEnPedido.ItemsSource = AlimentosDelPedido;
        }

        private void ButtonAñadir_Click(object sender, RoutedEventArgs e)
        {
            CantidadAlimento cantidadAlimento = ((FrameworkElement)sender).DataContext as CantidadAlimento;
            if (cantidadAlimento is CantidadPlatillo cantidadPlatillo)
            {
                bool existenAlimentos = cantidadPlatillo.Alimento.ValidarCantidadAlimento(cantidadPlatillo.Cantidad + CANTIDAD_ALIMENTOS_POR_CLIC);
                if (existenAlimentos)
                {
                    cantidadAlimento.Cantidad++;
                }
                else
                {
                    MostrarMensajeCantidadInsuficiente(cantidadPlatillo.Alimento.Nombre);
                }
            }
            else if (cantidadAlimento is CantidadProducto cantidadProducto)
            {
                bool existenAlimentos = cantidadProducto.Alimento.ValidarCantidadAlimento(cantidadProducto.Cantidad + CANTIDAD_ALIMENTOS_POR_CLIC);
                if (existenAlimentos)
                {
                    cantidadAlimento.Cantidad++;
                }
                else
                {
                    MostrarMensajeCantidadInsuficiente(cantidadProducto.Alimento.Nombre);
                }
            }
            DataGridAlimentosEnPedido.ItemsSource = null;
            DataGridAlimentosEnPedido.ItemsSource = AlimentosDelPedido;
        }

        private CantidadAlimento BuscarCantidadAlimento(Alimento alimento)
        {
            CantidadAlimento resultadoBusqueda = null;
            foreach (CantidadAlimento cantidadAlimento in AlimentosDelPedido)
            {
                if (cantidadAlimento is CantidadProducto cantidadProducto)
                {
                    if (cantidadProducto.Alimento.Codigo == alimento.Codigo)
                    {
                        resultadoBusqueda = cantidadProducto;
                    }
                }
                else if (cantidadAlimento is CantidadPlatillo cantidadPlatillo)
                {
                    if (cantidadPlatillo.Alimento.Codigo == alimento.Codigo)
                    {
                        resultadoBusqueda = cantidadPlatillo;
                    }
                }
            }
            return resultadoBusqueda;
        }

        private void TextBoxBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            string busqueda = TextBoxBusqueda.Text;
            if (busqueda != string.Empty)
            {
                List<Alimento> alimentos = AlimentosCargados.Where(a => ValidarCantidadAlimentoConBusqueda(a, busqueda)).ToList();
                ListBoxAlimentos.ItemsSource = null;
                ListBoxAlimentos.ItemsSource = alimentos;
            }
            else
            {
                ListBoxAlimentos.ItemsSource = null;
                ListBoxAlimentos.ItemsSource = AlimentosCargados;
            }
        }

        private bool ValidarCantidadAlimentoConBusqueda(Alimento alimento, string busqueda)
        {
            string nombre = string.Empty;
            string codigo = string.Empty;

            if(alimento is Producto producto)
            {
                nombre = producto.Nombre;
                codigo = producto.Codigo;
            }
            else if(alimento is Platillo platillo)
            {
                nombre = platillo.Nombre;
                codigo = platillo.Codigo;
            }

            return nombre.ToLower().Contains(busqueda.ToLower()) || codigo.ToLower().Contains(busqueda.ToLower());
        }
    }
}
