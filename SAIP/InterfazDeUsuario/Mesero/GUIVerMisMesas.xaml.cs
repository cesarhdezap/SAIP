using InterfazDeUsuario.Paginas;
using InterfazDeUsuario.UserControls;
using LogicaDeNegocio;
using LogicaDeNegocio.Clases;
using LogicaDeNegocio.Enumeradores;
using LogicaDeNegocio.ObjetosAccesoADatos;
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
using System.Windows.Shapes;


namespace InterfazDeUsuario.Mesero
{
    /// <summary>
    /// Interaction logic for GUIVerMisMesas.xaml
    /// </summary>
    public partial class GUIVerMisMesas : Page
    {
        private List<Cuenta> CuentasDelEmpleado = new List<Cuenta>();
        ControladorDeCambioDePantalla Controlador;
        Empleado Empleado;
        

        public GUIVerMisMesas(ControladorDeCambioDePantalla controlador, Empleado empleado)
        {
            Controlador = controlador;
            Empleado = empleado;
            InitializeComponent();
            UserControlInformacionDeCuenta.Controlador = controlador;
            UserControlInformacionDeCuenta.Empleado = empleado;
            BarraDeEstado.Controlador = controlador;
            BarraDeEstado.AsignarUsuarioActual(empleado);
            MostrarMisMesas();
        }

        public void MostrarMisMesas()
        {
            CuentaDAO cuentaDAO = new CuentaDAO();
            CuentasDelEmpleado = cuentaDAO.RecuperarCuentasAbiertasPorEmpleado(Empleado);
            ListBoxMesas.ItemsSource = CuentasDelEmpleado;
        }

        void ButtonMesa_Click(object sender, RoutedEventArgs e)
        {
            Cuenta cuenta = ((FrameworkElement)sender).DataContext as Cuenta;
            StackPanelCuenta.Visibility = Visibility.Visible;
            UserControlInformacionDeCuenta.ActualizarCuenta(cuenta);
        }

        private void VerMesasDiponiblesButton_Click(object sender, RoutedEventArgs e)
        {
            GUIVerMisMesasDisponibles page = new GUIVerMisMesasDisponibles(Controlador, Empleado);
            Controlador.CambiarANuevaPage(page);
        }

        private void ButtonOcultarCuenta_Click(object sender, RoutedEventArgs e)
        {
            StackPanelCuenta.Visibility = Visibility.Collapsed;
        }

        private void ButtonActualizar_Click(object sender, RoutedEventArgs e)
        {
            MostrarMisMesas();
            StackPanelCuenta.Visibility = Visibility.Collapsed;
            
        }
    }
}
 