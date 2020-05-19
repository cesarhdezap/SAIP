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

namespace InterfazDeUsuario.Mesero
{
    /// <summary>
    /// Interaction logic for GUIRegistrarPedidoLocal.xaml
    /// </summary>
    public partial class GUIRegistrarPedidoLocal : Page
    {
        ControladorDeCambioDePantalla ControladorDeCambioDePantalla;
        public GUIRegistrarPedidoLocal(ControladorDeCambioDePantalla controlador, Empleado empleado, Cuenta cuenta)
        {
            ControladorDeCambioDePantalla = controlador;
            InitializeComponent();
            BarraDeEstado.Controlador = controlador;
            BarraDeEstado.ActualizarNombreDeUsuario(empleado.NombreDeUsuario);
            MostrarAlimentos();
        }

        private void MostrarAlimentos()
        {
            //Cargar alimentos y productos y mostrarlos en la listbox
        }

        private void ButtonRealizar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
