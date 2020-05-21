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
    public partial class GUIRegistarMesa : Window
    {
        public Empleado Gerente { get; set; }
        public GUIRegistarMesa(Empleado EmpleadoCargado)
        {
            InitializeComponent();
            Gerente = EmpleadoCargado;
            
            BarraDeEstado.ActualizarNombreDeUsuario(Gerente.Nombre);
        }

        
    }
}
