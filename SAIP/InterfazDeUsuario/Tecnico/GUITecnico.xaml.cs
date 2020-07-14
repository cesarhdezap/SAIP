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

namespace InterfazDeUsuario.Tecnico
{
    /// <summary>
    /// Interaction logic for GUITecnico.xaml
    /// </summary>
    public partial class GUITecnico : Page
    {
        private ControladorDeCambioDePantalla Controlador;
        private Empleado Empleado;

        public GUITecnico(ControladorDeCambioDePantalla controlador, Empleado empleadoCargado)
        {
            InitializeComponent();
            Controlador = controlador;
            Empleado = empleadoCargado;
            BarraDeEstado.Controlador = controlador;
            BarraDeEstado.ActualizarEmpleado(empleadoCargado);
        }

        private void ButtonGenerarRespaldo_Click(object sender, RoutedEventArgs e)
        {
            GUIGenerarRespaldo page = new GUIGenerarRespaldo(Controlador, Empleado);
            Controlador.CambiarANuevaPage(page);
        }
    }
}
