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
    /// Interaction logic for GUIVisualizarListaDeClientes.xaml
    /// </summary>
    public partial class GUIVisualizarListaDeClientes : Page
    {
        Empleado Empleado;
        readonly ControladorDeCambioDePantalla Controlador;
        List<Cliente> Clientes = new List<Cliente>();

        public GUIVisualizarListaDeClientes(ControladorDeCambioDePantalla controlador, Empleado empleado)
        {
            Controlador = controlador;
            Empleado = empleado;
            InitializeComponent();
            BarraDeEstado.Controlador = controlador;
            BarraDeEstado.ActualizarNombreDeUsuario(empleado.NombreDeUsuario);
            MostrarClientes();
        }

        private void MostrarClientes()
        {
            ClienteDAO clienteDAO = new ClienteDAO();
            Clientes = clienteDAO.CargarTodosActivos();
            DataGridClientes.ItemsSource = null;
            DataGridClientes.ItemsSource = Clientes;
        }

        private void ButtonActualizarClientes_Click(object sender, RoutedEventArgs e)
        {
            MostrarClientes();
        }

        private void ButtonDarDeBaja_Click(object sender, RoutedEventArgs e)
        {
            Cliente cliente;
            try
            {
                cliente = (Cliente)DataGridClientes.SelectedItem;
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("Seleccione un cliente válido");
                cliente = null;
            }

            if (cliente != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Seguro que quieres dar de baja Cliente?", "DAR DE BAJA CLIENTE", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    ClienteDAO clienteDAO = new ClienteDAO();
                    bool falloDarDeBaja = false;
                    try
                    {
                        clienteDAO.DarDeBaja(cliente);
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show(ex.Message);
                        falloDarDeBaja = true;
                    }

                    if (!falloDarDeBaja)
                    {
                        MessageBox.Show("Cliente dado de baja!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione un cliente", "Aviso");
            }
            MostrarClientes();
        }

        private void ButtonEditar_Click(object sender, RoutedEventArgs e)
        {
            Cliente cliente;
            try
            {
                cliente = (Cliente)DataGridClientes.SelectedItem;
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("Seleccione un cliente válido");
                cliente = null;
            }

            if(cliente != null)
            {
                GUIEditarCliente page = new GUIEditarCliente(Controlador, Empleado, cliente);
                Controlador.CambiarANuevaPage(page);
            }
        }

        private void DataGridClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Cliente cliente = new Cliente();
            if (DataGridClientes.SelectedItem != null)
            {
                try
                {
                    cliente = (Cliente)DataGridClientes.SelectedItem;
                }
                catch (InvalidCastException)
                {
                    MessageBox.Show("Seleccione un cliente válido");
                }
            }
            
            TextBlockComentarios.Text = cliente.Comentario;
            TextBlockDirecciones.Text = string.Empty;
            foreach (string direccion in cliente.Direcciones)
            {
                TextBlockDirecciones.Text += direccion + Environment.NewLine;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MostrarClientes();
        }
    }
}
