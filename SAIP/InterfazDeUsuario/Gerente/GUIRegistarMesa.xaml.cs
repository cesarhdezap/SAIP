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
    /// Lógica de interacción para GUIRegistarMesa.xaml
    /// </summary>
    public partial class GUIRegistarMesa : Page
    {
        public Empleado Gerente { get; set; }
        ControladorDeCambioDePantalla Controlador;
        public GUIRegistarMesa(ControladorDeCambioDePantalla controlador, Empleado EmpleadoCargado)
        {
            InitializeComponent();
            Gerente = EmpleadoCargado;
            BarraDeEstado.Controlador = controlador;
            Controlador = controlador;
            BarraDeEstado.ActualizarNombreDeUsuario(Gerente.Nombre);
        }

        
    }
}
