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

namespace InterfazDeUsuario.Gerente
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
        public GUIVerEmpleados(ControladorDeCambioDePantalla controlador, Empleado EmpleadoCargado)
        {
            InitializeComponent();
            Gerente = EmpleadoCargado;
            BarraDeEstado.Controlador = controlador;
            Controlador = controlador;
            BarraDeEstado.ActualizarEmpleado(Gerente);
 
            MostrarEmpleados();

        } 

        public void MostrarEmpleados() {
            EmpleadoDAO empleadoDAO = new EmpleadoDAO();
            Trabajadores = empleadoDAO.CargarTodos();
            ListaE.ItemsSource = Trabajadores;
        }

        public void ActualizarPantalla()
        {
            ListaE.ItemsSource = null;
            ListaE.ItemsSource = Visibles;
        }

       

        private void Busqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            string busqueda = Busqueda.Text;
            if (busqueda != string.Empty)
            {
                Trabajadores = Visibles.TakeWhile(Empleado => Empleado.Nombre.ToLower().Contains(busqueda.ToLower())).ToList();
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

        
        
    }
}
