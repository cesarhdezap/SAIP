using LogicaDeNegocio.Clases;
using LogicaDeNegocio.Servicios;
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
using static LogicaDeNegocio.Servicios.ServiciosDeRespaldos;
using static InterfazDeUsuario.UtileriasGráficas;
using LogicaDeNegocio.Interfaces;
using System.IO;

namespace InterfazDeUsuario.Tecnico
{
    /// <summary>
    /// Interaction logic for GUIGenerarRespaldo.xaml
    /// </summary>
    public partial class GUIGenerarRespaldo : Page, IActualizarBarraDeProgreso
    {
        private ControladorDeCambioDePantalla Controlador;
        private Empleado Empleado;
        private string Ruta;
        bool Generado = false;

        public GUIGenerarRespaldo(ControladorDeCambioDePantalla controlador, Empleado empleado)
        {
            InitializeComponent();
            Controlador = controlador;
            Empleado = empleado;
            BarraDeEstado.Controlador = controlador;
            BarraDeEstado.ActualizarEmpleado(empleado);
            Ruta = new ServiciosDeRespaldos(this).ObtenerRutaPorDefectoCompleta();
            LabelRuta.Content = Ruta;
            CargarRespaldosPorDefecto();
        }

        public void ActualizarBarraDeProgreso(int cantidad)
        {
            ProgressBarEstado.Value += cantidad;
            if(ProgressBarEstado.Value >= ProgressBarEstado.Maximum && !Generado)
            {
                RespaldoGenerado();
                Generado = true;
            }
        }

        private void CargarRespaldosPorDefecto()
        {
            string ruta = new ServiciosDeRespaldos(this).ObtenerRutaPorDefectoCompleta();
            var files = Directory.EnumerateFiles(ruta, "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".bak")).ToList();
            files.Count();
        }

        private void RespaldoGenerado()
        {
            MessageBox.Show("Se ha generado el respaldo correctamente!");
            Controlador.Regresar();
        }

        private void ButtonEmpezar_Click(object sender, RoutedEventArgs e)
        {
            
            ButtonEmpezar.IsEnabled = false;

            var servicioDeRespaldo = new ServiciosDeRespaldos(this);
            servicioDeRespaldo.GenerarRespaldo(Empleado.Nombre, Ruta);
        }

        private void ButtonRuta_Click(object sender, RoutedEventArgs e)
        {
            Ruta = UtileriasGráficas.MostrarVentanaDeSeleccionDeArchivosParaCarpetas();
            LabelRuta.Content = Ruta;
        }
    }
}
