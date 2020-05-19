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

namespace InterfazDeUsuario.Gerente
{
    /// <summary>
    /// Lógica de interacción para GUIRegistrarEmpleado.xaml
    /// </summary>
    public partial class GUIRegistrarEmpleado : Window
    {
        public Empleado Gerente { get; set; }
        public GUIRegistrarEmpleado(Empleado EmpleadoCargado)
        {
            InitializeComponent();
            Gerente = EmpleadoCargado;
            BarraDeEstado.ActualizarNombreDeUsuario(Gerente.Nombre);
        }
    }
}
