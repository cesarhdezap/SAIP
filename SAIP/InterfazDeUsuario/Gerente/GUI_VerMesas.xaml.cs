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

namespace InterfazDeUsuario.empleado
{
    /// <summary>
    /// Lógica de interacción para GUI_VerMesas.xaml
    /// </summary>
    public partial class GUI_VerMesas : Page
    {
        public Empleado Gerente { get; set; }
        private List<Mesa> Mesas = new List<Mesa>();
        
        ControladorDeCambioDePantalla Controlador;
        public GUI_VerMesas(ControladorDeCambioDePantalla controlador ,Empleado EmpleadoCargado)
        {
            InitializeComponent();
            Gerente = EmpleadoCargado;

            Empleado = empleado;
            BarraDeEstado.ActualizarEmpleado(Gerente);

            VerMesas();
        }

        public void VerMesas()
        {
            MesaDAO mesaDAO = new MesaDAO();
            Mesas = mesaDAO.RecuperarTodos();
            ListaM.ItemsSource = Mesas;
        }

        private void Registro_Click(object sender, RoutedEventArgs e)
        {
            GUIRegistarMesa registarMesa = new GUIRegistarMesa(Controlador, Gerente);
            Controlador.CambiarANuevaPage(registarMesa);

        }
    }
}
