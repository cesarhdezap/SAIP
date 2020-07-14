using InterfazDeUsuario.UserControls;
using LogicaDeNegocio.Clases;
using LogicaDeNegocio.ObjetosAccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace InterfazDeUsuario.Mesero
{
    /// <summary>
    /// Interaction logic for GUIVerMisMesas.xaml
    /// </summary>
    public partial class GUIVerMisMesas : Page
    {
        private List<Cuenta> CuentasDelEmpleado = new List<Cuenta>();
        private Cuenta CuentaSeleccionada;
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
            BarraDeEstado.ActualizarEmpleado(empleado);

            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(1);

            var timer = new System.Threading.Timer((e) =>
            {
                Dispatcher.Invoke(() => 
                {
                    MostrarMisMesas();
                    ActualizarListBoxMesas();
                });
                
            }, null, startTimeSpan, periodTimeSpan);
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
            if (CuentaSeleccionada != null)
            {
                CuentasDelEmpleado.Add(CuentaSeleccionada);
            }
            CuentaSeleccionada = cuenta;
            CuentasDelEmpleado.Remove(cuenta);

            ActualizarListBoxMesas();
            StackPanelCuenta.Visibility = Visibility.Visible;
            UserControlInformacionDeCuenta.ActualizarCuenta(cuenta);
        }

        private void ActualizarListBoxMesas()
        {
            ListBoxMesas.SelectedItem = null;
            ListBoxMesas.ItemsSource = null;
            ListBoxMesas.ItemsSource = CuentasDelEmpleado;
        }

        private void VerMesasDiponiblesButton_Click(object sender, RoutedEventArgs e)
        {
            GUIVerMisMesasDisponibles page = new GUIVerMisMesasDisponibles(Controlador, Empleado);
            Controlador.CambiarANuevaPage(page);
        }

        private void ButtonOcultarCuenta_Click(object sender, RoutedEventArgs e)
        {
            StackPanelCuenta.Visibility = Visibility.Collapsed;
            CuentasDelEmpleado.Add(CuentaSeleccionada);
            CuentaSeleccionada = null;
            ActualizarListBoxMesas();
        }

        private void ButtonActualizar_Click(object sender, RoutedEventArgs e)
        {
            MostrarMisMesas();
            StackPanelCuenta.Visibility = Visibility.Collapsed;
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MostrarMisMesas();
            if(CuentaSeleccionada != null)
            {
                Cuenta cuentaActualizada = CuentasDelEmpleado.FirstOrDefault(c => c.Id == CuentaSeleccionada.Id);
                CuentaSeleccionada = cuentaActualizada;
                CuentasDelEmpleado.Remove(CuentaSeleccionada);

                UserControlInformacionDeCuenta.ActualizarCuenta(CuentaSeleccionada);
                ActualizarListBoxMesas();
            }
        }
    }
}
 