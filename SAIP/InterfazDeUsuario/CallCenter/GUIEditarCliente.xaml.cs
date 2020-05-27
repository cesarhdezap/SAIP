using LogicaDeNegocio.Clases;
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
using static LogicaDeNegocio.Servicios.ServiciosDeValidacion;
using static InterfazDeUsuario.UtileriasGráficas;
using LogicaDeNegocio.ObjetosAccesoADatos;

namespace InterfazDeUsuario.CallCenter
{
    /// <summary>
    /// Interaction logic for GUIEditarCliente.xaml
    /// </summary>
    public partial class GUIEditarCliente : Page
    {
        List<string> Direcciones = new List<string>();
        ControladorDeCambioDePantalla Controlador;
        Empleado Empleado;
        Cliente Cliente;
        string DireccionEnBuffer = string.Empty;

        public GUIEditarCliente(ControladorDeCambioDePantalla controlador, Empleado empleado, Cliente cliente)
        {
            Cliente = cliente;
            Controlador = controlador;
            Empleado = empleado;
            InitializeComponent();
            BarraDeEstado.Controlador = Controlador;
            BarraDeEstado.AsignarUsuarioActual(Empleado);
            MostrarCliente();
        }

        private void MostrarCliente()
        {
            TextBoxNombre.Text = Cliente.Nombre;
            TextBoxComentarios.Text = Cliente.Comentario;
            TextBoxTelefono.Text = Cliente.Telefono;
            Direcciones = Cliente.Direcciones;
            ListBoxDirecciones.ItemsSource = null;
            ListBoxDirecciones.ItemsSource = Direcciones;
        }

        private void ButtonActualizar_Click(object sender, RoutedEventArgs e)
        {
            Cliente cliente = new Cliente
            {
                Id = Cliente.Id,
                Nombre = TextBoxNombre.Text,
                Telefono = TextBoxTelefono.Text,
                Comentario = TextBoxComentarios.Text,
                Direcciones = Direcciones,
                Creador = Empleado.Nombre
            };

            if (cliente.Validar())
            {
                ClienteDAO clienteDAO = new ClienteDAO();
                clienteDAO.Actualizar(cliente);
                MessageBox.Show("Cliente actualizado correctamente!", "EXITO");
                Controlador.Regresar();
            }
        }

        private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea salir del registro?", "Advertencia", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Controlador.Regresar();
            }
        }

        private void ButtonEliminarDireccion_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxDirecciones.SelectedItem != null)
            {
                string direccion = ListBoxDirecciones.SelectedItem.ToString();
                Direcciones.Remove(direccion);
                TextBoxDireccion.Text = DireccionEnBuffer;
                DireccionEnBuffer = string.Empty;

                ListBoxDirecciones.ItemsSource = null;
                ListBoxDirecciones.ItemsSource = Direcciones;
            }
            else
            {
                MessageBox.Show("Seleccione una dirección de la lista", "AVISO");
            }
        }

        private void ButtonAñadirDireccion_Click(object sender, RoutedEventArgs e)
        {
            if(ListBoxDirecciones.SelectedItem == null)
            {
                string direccion = TextBoxDireccion.Text;
                if (ValidarCadena(direccion))
                {
                    Direcciones.Add(direccion);
                }
            }
            else
            {
                string direccion = TextBoxDireccion.Text;
                if (ValidarCadena(direccion))
                {
                    int indice = Direcciones.IndexOf((string)ListBoxDirecciones.SelectedItem);
                    Direcciones[indice] = direccion;
                }
            }

            ListBoxDirecciones.SelectedItem = null;
            ListBoxDirecciones.ItemsSource = null;
            ListBoxDirecciones.ItemsSource = Direcciones;
        }

        private void TextBoxTelefono_TextChanged(object sender, TextChangedEventArgs e)
        {
            MostrarEstadoDeValidacionTelefono(TextBoxTelefono);
        }

        private void TextBoxNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            MostrarEstadoDeValidacionNombre(TextBoxNombre);
        }

        private void TextBoxComentarios_TextChanged(object sender, TextChangedEventArgs e)
        {
            MostrarEstadoDeValidacionCadenaVacioPermitido(TextBoxComentarios);
        }

        private void ListBoxDirecciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ListBoxDirecciones.SelectedItem == null)
            {
                TextBoxDireccion.Text = DireccionEnBuffer;
                DireccionEnBuffer = string.Empty;
            }
            else
            {
                DireccionEnBuffer = TextBoxDireccion.Text;
                TextBoxDireccion.Text = (string)ListBoxDirecciones.SelectedItem;
            }
        }
    }
}
