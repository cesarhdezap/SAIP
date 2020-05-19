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
    public partial class GUIVerEmpleados : Window
    {
        private List<Empleado> Trabajadores = new List<Empleado>();
        private List<Empleado> Visibles { get; set; }

        
        public Empleado Gerente { get; set; }
        

        public GUIVerEmpleados(Empleado EmpleadoCargado)
        {
            
            InitializeComponent();
            Gerente = EmpleadoCargado;
            BarraDeEstado.ActualizarNombreDeUsuario(Gerente.Nombre);
            MostrarEmpleados();

        }

        public void MostrarEmpleados() {
            EmpleadoDAO empleadoDAO = new EmpleadoDAO();
            Trabajadores = empleadoDAO.CargarTodos();
            ListBoxEmpleados.ItemsSource = Trabajadores;
        }

        public void ActualizarPantalla()
        {
            ListBoxEmpleados.ItemsSource = null;
            ListBoxEmpleados.ItemsSource = Visibles;
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
    }
}
