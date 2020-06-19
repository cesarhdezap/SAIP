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
using LogicaDeNegocio.Clases;
using LogicaDeNegocio.ObjetosAccesoADatos;

namespace InterfazDeUsuario.Gerente
{
    /// <summary>
    /// Lógica de interacción para GUI_DarBajaProductoOPlatillo.xaml
    /// </summary>
    public partial class GUI_DarBajaProductoOPlatillo : Page
    {
        public Empleado Gerente;
        public Producto Producto;
        public Platillo Platillo;
        public ControladorDeCambioDePantalla Controlador { get; set; }
        public GUI_DarBajaProductoOPlatillo(ControladorDeCambioDePantalla controlador, Empleado empleadoCargado, Producto producto, Platillo platillo)
        {
            InitializeComponent();
            Gerente = empleadoCargado;
            BarraDeEstado.Controlador = controlador;
            Controlador = controlador;
            BarraDeEstado.ActualizarEmpleado(Gerente);
            Controlador = controlador;
            BarraDeEstado.Controlador = controlador;
            Producto = producto;
            Platillo = platillo;
        }


    }
}
