using LogicaDeNegocio.Clases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for GUIRegistrarCliente.xaml
    /// </summary>
    public partial class GUIRegistrarCliente : Page
    {
        List<string> Direcciones = new List<string>();
        ControladorDeCambioDePantalla Controlador;
        Empleado Empleado;

        public GUIRegistrarCliente(ControladorDeCambioDePantalla controlador, Empleado empleado)
        {
            Controlador = controlador;
            Empleado = empleado;
            InitializeComponent();
            BarraDeEstado.Controlador = Controlador;
            BarraDeEstado.ActualizarEmpleado(empleado);
        }

        private void ButtonAñadirDireccion_Click(object sender, RoutedEventArgs e)
        {
            string direccion = TextBoxDireccion.Text;
            if (ValidarCadena(direccion))
            {
                Direcciones.Add(direccion);
                ListBoxDirecciones.ItemsSource = null;
                ListBoxDirecciones.ItemsSource = Direcciones;
            }
            MostrarEstadoDeValidacionCadena(TextBoxDireccion);
            TextBoxDireccion.Clear();
        }

        private void ButtonEliminarDireccion_Click(object sender, RoutedEventArgs e)
        {
            if(ListBoxDirecciones.SelectedItem != null)
            {
                string direccion = ListBoxDirecciones.SelectedItem.ToString();
                Direcciones.Remove(direccion);
                ListBoxDirecciones.ItemsSource = null;
                ListBoxDirecciones.ItemsSource = Direcciones;
            }
            else
            {
                MessageBox.Show("Seleccione una dirección de la lista", "AVISO");
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

        private void ButtonRegistrar_Click(object sender, RoutedEventArgs e)
        {
            Cliente cliente = new Cliente
            {
                Nombre = TextBoxNombre.Text,
                Telefono = TextBoxTelefono.Text,
                Comentario = TextBoxComentarios.Text,
                Direcciones = Direcciones,
                Creador = Empleado.Nombre
            };

            if (cliente.Validar())
            {
                ClienteDAO clienteDAO = new ClienteDAO();
                clienteDAO.Guardar(cliente);
                MessageBox.Show("Cliente registrado correctamente!", "EXITO");
                Controlador.Regresar();
            }
        }

        private void TextBoxNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            MostrarEstadoDeValidacionNombre(TextBoxNombre);
        }

        private void TextBoxTelefono_TextChanged(object sender, TextChangedEventArgs e)
        {
            MostrarEstadoDeValidacionTelefono(TextBoxTelefono);
        }

        private void TextBoxComentarios_TextChanged(object sender, TextChangedEventArgs e)
        {
            MostrarEstadoDeValidacionCadenaVacioPermitido(TextBoxComentarios);
        }
    }
}
