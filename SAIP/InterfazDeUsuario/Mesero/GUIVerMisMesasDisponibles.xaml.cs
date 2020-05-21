using LogicaDeNegocio.Clases;
using LogicaDeNegocio.Enumeradores;
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

namespace InterfazDeUsuario.Mesero
{
    /// <summary>
    /// Interaction logic for GUIVerMisMesasDisponibles.xaml
    /// </summary>
    public partial class GUIVerMisMesasDisponibles : Page
    {
        ControladorDeCambioDePantalla Controlador;
        List<Mesa> MesasDisponibles = new List<Mesa>();
        Empleado Empleado;

        public GUIVerMisMesasDisponibles(ControladorDeCambioDePantalla controlador, Empleado empleado)
        {
            Controlador = controlador;
            Empleado = empleado;
            InitializeComponent();
            BarraDeEstado.Controlador = controlador;
            BarraDeEstado.ActualizarNombreDeUsuario(empleado.Nombre);
            MostrarMesasDisponibles();
        }

        private void MostrarMesasDisponibles()
        {
            MesaDAO mesaDAO = new MesaDAO();
            MesasDisponibles = mesaDAO.ObtenerMesasPorEstado(EstadoMesa.Disponible);
            ListBoxMesas.ItemsSource = MesasDisponibles;
        }

        void ButtonMesa_Click(object sender, RoutedEventArgs e)
        {
            Mesa mesa = ((FrameworkElement)sender).DataContext as Mesa;
            MessageBoxResult resultado = MessageBox.Show("¿Abrir nueva cuenta?", "CREAR NUEVA CUENTA", MessageBoxButton.OKCancel);
            if (resultado == MessageBoxResult.OK)
            {
                Cuenta cuenta = new Cuenta
                {
                    Mesa = mesa,
                    Empleado = Empleado
                };
                CuentaDAO cuentaDAO = new CuentaDAO();
                cuentaDAO.CrearCuenta(cuenta);
            }
            MostrarMesasDisponibles();
        }
    }
}
