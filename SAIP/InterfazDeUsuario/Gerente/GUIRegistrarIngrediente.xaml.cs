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

namespace InterfazDeUsuario.Gerente
{
    /// <summary>
    /// Lógica de interacción para GUIRegistrarIngrediente.xaml
    /// </summary>
    public partial class GUIRegistrarIngrediente : Page
    {
        Empleado Empleado;
        readonly ControladorDeCambioDePantalla Controlador;
        public GUIRegistrarIngrediente(ControladorDeCambioDePantalla controlador, Empleado empleado)
        {
            Controlador = controlador;
            Empleado = empleado;
            InitializeComponent();
            BarraDeEstado.Controlador = controlador;
            BarraDeEstado.ActualizarNombreDeUsuario(empleado.NombreDeUsuario);
            GridCompuestos.Visibility = Visibility.Collapsed;
        }

        private void CheckBoxIngredienteCompuesto_Checked(object sender, RoutedEventArgs e)
        {
            GridCompuestos.Visibility = Visibility.Visible;
        }

        private void CheckBoxIngredienteCompuesto_Unchecked(object sender, RoutedEventArgs e)
        {
            GridCompuestos.Visibility = Visibility.Collapsed;
        }
    }
}
