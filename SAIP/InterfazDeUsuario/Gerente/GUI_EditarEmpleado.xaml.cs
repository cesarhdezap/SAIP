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
    /// Lógica de interacción para GUI_EditarEmpleado.xaml
    /// </summary>
    public partial class GUI_EditarEmpleado : Page
    {
        ControladorDeCambioDePantalla Controlador;
        public Empleado Gerente { get; set; }
        public GUI_EditarEmpleado(ControladorDeCambioDePantalla controlador, Empleado EmpleadoCargado)
        {
            InitializeComponent();
            Gerente = EmpleadoCargado;
            BarraDeEstado.Controlador = controlador;
            Controlador = controlador;
            BarraDeEstado.ActualizarNombreDeUsuario(Gerente.Nombre);
        }
    }
}
