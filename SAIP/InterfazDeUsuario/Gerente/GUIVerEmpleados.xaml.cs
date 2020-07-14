using InterfazDeUsuario.UserControls;
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

namespace InterfazDeUsuario.empleado
{
    /// <summary>
    /// Lógica de interacción para GUIVerEmpleados.xaml
    /// </summary>
    public partial class GUIVerEmpleados : Page
    {
        private List<Empleado> Trabajadores { get; set; }
        private List<Empleado> Visibles { get; set; }
        public Empleado Gerente { get; set; }
        public Empleado empleadoAEditar { get; private set; }

        ControladorDeCambioDePantalla Controlador;
        public GUIVerEmpleados(ControladorDeCambioDePantalla controlador, Empleado EmpleadoCargado, Empleado empleadoADesactivar)
        {
            InitializeComponent();
            Gerente = EmpleadoCargado;
            BarraDeEstado.Controlador = controlador;
            Controlador = controlador;
            BarraDeEstado.ActualizarEmpleado(Gerente);
            MostrarEmpleados();

        }

        public void MostrarEmpleados()
        {
            EmpleadoDAO empleadoDAO = new EmpleadoDAO();
            Trabajadores = empleadoDAO.CargarTodos();
            ListaE.ItemsSource = null;
            ListaE.ItemsSource = Trabajadores;
            ActualizarLista();
        }

        public void ActualizarPantalla()
        {

            ListaE.ItemsSource = Visibles;
        }

        public void ActualizarLista()
        {
            ListaE.ItemsSource = Trabajadores;
        }


        private void Busqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            string busqueda = Busqueda.Text;
            if (busqueda != string.Empty)
            {
                Trabajadores = Visibles.TakeWhile(empleado => empleado.Nombre.ToLower().Contains(busqueda.ToLower())).ToList();
            }
            else
            {
                Trabajadores = Visibles;
            }
            ActualizarPantalla();
        }


        public void ButtonEditar_Click(object sender, RoutedEventArgs e)
        {
            Empleado empleado = ((FrameworkElement)sender).DataContext as Empleado;
            GUI_EditarEmpleado editar = new GUI_EditarEmpleado(Controlador, empleado, empleadoAEditar);
            Controlador.CambiarANuevaPage(editar);

        }

        

        private void ButtonEliminar_Click(object sender, RoutedEventArgs e)
        {
            EmpleadoDAO empleadoDAO = new EmpleadoDAO();
            Empleado empleadoADesactivar = ((FrameworkElement)sender).DataContext as Empleado;
            if (empleadoADesactivar != null)
            {
                GUIVerEmpleados desactivarempleado = new GUIVerEmpleados(Controlador, Gerente, empleadoADesactivar);
                empleadoDAO.DesactivarEmpleado(empleadoADesactivar);
            }
            else
            {
                MessageBox.Show("No se a seleccionado un Empleado para su Desactivación", "Seleccionar Empleado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

