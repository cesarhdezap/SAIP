using InterfazDeUsuario.UserControls;
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
using static LogicaDeNegocio.Servicios.ServiciosDeValidacion;
using static InterfazDeUsuario.UtileriasGráficas;

namespace InterfazDeUsuario.Gerente
{
    /// <summary>
    /// Lógica de interacción para GUIRegistrarEmpleado.xaml
    /// </summary>
    public partial class GUIRegistrarEmpleado : Window
    {
        public Empleado Gerente { get; set; }
        public List<Empleado> Trabajadores { get; set; }
        public List<Empleado> Visible { get; set; }
        public Empleado empleado { get; set; } = new Empleado();
        private bool CandadoDeRefrescadoDeCajasDeTexto = true;
        public GUIRegistrarEmpleado(Empleado EmpleadoCargado)
        {
            InitializeComponent();
            Gerente = EmpleadoCargado;
            BarraDeEstado.ActualizarNombreDeUsuario(Gerente.Nombre);
            EmpleadoDAO empleadoDAO = new EmpleadoDAO();
            Trabajadores = empleadoDAO.CargarTodos();
            Visible = Trabajadores;
            ActualizarLista();

        }

        public void ActualizarLista()
        {

        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
           /// Empleado empleadoa = (Empleado);
            
        }


        private bool ValidarCampos()
        {
            bool resultado = false;
            if (ValidarCadena(Nombre.Text) &&
                ValidarCadena(Usuario.Text) &&
                ValidarCadena(correo.Text) &&
                ValidarCadena(password.Text)&&
                ValidarCadena(puesto.Text))
            {
                resultado = true;
            }
            else
            {
                MostrarEstadoDeValidacionCadena(Nombre);
                MostrarEstadoDeValidacionCadena(Usuario);
                MostrarEstadoDeValidacionCadena(correo);
                MostrarEstadoDeValidacionCadena(password);
                MostrarEstadoDeValidacionCadena(puesto);

            }

            return resultado;
        }
            private void Nombre_TextChanged(object sender, TextChangedEventArgs e)
            {
            MostrarEstadoDeValidacionCadena((TextBox)sender);

            }
        public void GuardarEmpleado()
        {
            empleado.Nombre = Nombre.Text;
            empleado.NombreDeUsuario = Usuario.Text;
            empleado.Contraseña = password.Text;
            empleado.CorreoElectronico = correo.Text;
           /// empleado.TipoDeEmpleado = puesto.Text. ;
        }

        private void Usuario_TextChanged(object sender, TextChangedEventArgs e)
        {
            MostrarEstadoDeValidacionCadena((TextBox)sender);
        }

        private void correo_TextChanged(object sender, TextChangedEventArgs e)
        {
            MostrarEstadoDeValidacionCadena((TextBox)sender);
        }

        private void puesto_TextChanged(object sender, TextChangedEventArgs e)
        {
            MostrarEstadoDeValidacionCadena((TextBox)sender);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            MostrarEstadoDeValidacionCadena((TextBox)sender);
        }

        
    }
}
