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
    /// Lógica de interacción para GUI_VerListaIngredientes.xaml
    /// </summary>
    public partial class GUI_VerListaIngredientes : Page
    {
        private Empleado Empleado;
        private ControladorDeCambioDePantalla Controlador;
        private List<Ingrediente> IngredientesCargados;
        private List<Ingrediente> IngredientesVisibles;
        public GUI_VerListaIngredientes(ControladorDeCambioDePantalla controlador, Empleado empleado)
        {
            InitializeComponent();
            Empleado = empleado;
            BarraDeEstado.Controlador = controlador;
            BarraDeEstado.ActualizarEmpleado(empleado);
            Controlador = controlador;
            IngredienteDAO ingredienteDAO = new IngredienteDAO();
            IngredientesCargados = ingredienteDAO.CargarTodos();
            IngredientesVisibles = IngredientesCargados;
            ActualizarPantalla();
        }

        private void ButtonEditar_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Buscar(((TextBox)sender).Text);
        }

        private void ActualizarPantalla()
        {
            DataGridIngredientes.ItemsSource = null;
            DataGridIngredientes.ItemsSource = IngredientesVisibles;
        }

        private void Buscar(string busqueda)
        {
            if (string.IsNullOrEmpty(busqueda))
            {
                IngredientesVisibles = IngredientesCargados.Where(p => p.Nombre.Contains(busqueda)).ToList();
            }
            else
            {
                IngredientesVisibles = IngredientesCargados;
            }
        }
    }
}
