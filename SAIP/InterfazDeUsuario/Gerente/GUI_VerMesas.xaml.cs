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
    /// Lógica de interacción para GUI_VerMesas.xaml
    /// </summary>
    public partial class GUI_VerMesas : Window
    {
        public Empleado Gerente { get; set; }
        private List<Cuenta> Cuentas = new List<Cuenta>();
        Empleado Empleado;
        public GUI_VerMesas(Empleado EmpleadoCargado, Empleado empleado)
        {
            InitializeComponent();
            Gerente = EmpleadoCargado;
            Empleado = empleado;
            BarraDeEstado.ActualizarEmpleado(Gerente);
            VerMesas();
        }

        public void VerMesas()
        {
            CuentaDAO cuentaDAO = new CuentaDAO();
            Cuentas = cuentaDAO.RecuperarCuentasAbiertasPorEmpleado(Empleado);
            ListaM.ItemsSource = Cuentas;
        }

        private void registro_Click(object sender, RoutedEventArgs e)
        {
            GUIRegistarMesa registarMesa = new GUIRegistarMesa(Gerente);
            Hide();
            registarMesa.ShowDialog();

        }
    }
}
