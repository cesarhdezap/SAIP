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
        ControladorDeCambioDePantalla Controlador;
        public GUIVerEmpleados(ControladorDeCambioDePantalla controlador, Empleado EmpleadoCargado, Empleado empleadoADesactivar)
        {
            InitializeComponent();
            Gerente = EmpleadoCargado;
            BarraDeEstado.Controlador = controlador;
            Controlador = controlador;

            BarraDeEstado.ActualizarNombreDeUsuario(Gerente.Nombre);

            MostrarEmpleados();

        } 

        public void MostrarEmpleados() {
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

        private void Registro_Click(object sender, RoutedEventArgs e)
        {
            GUIRegistrarEmpleado registrarEmpleado = new GUIRegistrarEmpleado(Controlador, Gerente);
            Controlador.CambiarANuevaPage(registrarEmpleado);
        }

        public void Editar_Click(object sender, RoutedEventArgs e)
        {
            Empleado empleadoACargar = (Empleado)ListaE.SelectedItem;
            if (empleadoACargar!= null)
            {
                GUI_EditarEmpleado editarEmpleado1 = new GUI_EditarEmpleado(Controlador, Gerente, empleadoACargar);
                Controlador.CambiarANuevaPage(editarEmpleado1);
            }
            else
            {
                MessageBox.Show("No se a seleccionado un Empleado para su Edicion", "Seleccionar Empleado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void Eliminar_Click(object sender, RoutedEventArgs e, EmpleadoDAO empleado)

        {
            Empleado empleadoADesactivar = (Empleado)ListaE.SelectedItem;
            if (empleadoADesactivar != null)
            {
                GUIVerEmpleados desactivarempleado = new GUIVerEmpleados(Controlador, Gerente, empleadoADesactivar);
                empleado.DesactivarEmpleado(empleadoADesactivar);
            }

        }


    }
}
