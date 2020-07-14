using InterfazDeUsuario.UserControls;
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

namespace InterfazDeUsuario.Cocinero
{
    /// <summary>
    /// Lógica de interacción para GUICocinero.xaml
    /// </summary>
    public partial class GUIVerPedidosPendientes : Page
    {
        public Empleado Empleado { get; set; }
        private ControladorDeCambioDePantalla Controlador { get; set; }
        public GUIVerPedidosPendientes(ControladorDeCambioDePantalla controlador, Empleado empleadoCargado)
        {
            InitializeComponent();
            Empleado = empleadoCargado;
            BarraDeEstado.Controlador = controlador;
            Controlador = controlador;
            BarraDeEstado.ActualizarEmpleado(Empleado);
            Controlador = controlador;
            BarraDeEstado.Controlador = controlador;
        }

        private void BotonVerPedidos_Click(object sender, RoutedEventArgs e)
        {
            GUI_VerPedidosPendientes gUI_VerPedidosPendientes = new GUI_VerPedidosPendientes(Controlador, Empleado);
            Controlador.CambiarANuevaPage(gUI_VerPedidosPendientes);
        }
    }
}