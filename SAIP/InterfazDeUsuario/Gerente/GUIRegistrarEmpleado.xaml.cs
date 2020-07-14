using InterfazDeUsuario.UserControls;
using LogicaDeNegocio.Clases;
using LogicaDeNegocio.Enumeradores;
using LogicaDeNegocio.ObjetosAccesoADatos;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using static InterfazDeUsuario.UtileriasGráficas;
using static LogicaDeNegocio.Servicios.ServiciosDeValidacion;

namespace InterfazDeUsuario.Gerente
{
    /// <summary>
    /// Lógica de interacción para GUIRegistrarEmpleado.xaml
    /// </summary>

    public partial class GUIRegistrarEmpleado : Page
    {
        public Empleado Gerente { get; set; }
        public List<Empleado> Trabajadores { get; set; }
        public List<Empleado> Visible { get; set; }
        public Empleado Empleado { get; set; } = new Empleado();
        ControladorDeCambioDePantalla Controlador;
        public GUIRegistrarEmpleado(ControladorDeCambioDePantalla controlador, Empleado empleadoCargado)
        {
            InitializeComponent();
            Gerente = empleadoCargado;
            BarraDeEstado.Controlador = controlador;
            Controlador = controlador;
            BarraDeEstado.ActualizarEmpleado(Gerente);
            EmpleadoDAO empleadoDAO = new EmpleadoDAO();
            Trabajadores = empleadoDAO.CargarTodos();
            Visible = Trabajadores;
        }


        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            CapturarEmpleado();
            ValidarCampos();
            ActualizarPantalla();
        }

        public void ActualizarPantalla()
        {
            TextBoxNombre.Clear();
            Usuario.Clear();
            correo.Clear();
            password.Clear();
            puesto.Clear();
        }

        private bool ValidarCampos()
        {
            bool resultado = false;
            if (ValidarCadena(TextBoxNombre.Text) &&
                ValidarCadena(Usuario.Text) &&
                ValidarCadena(correo.Text) &&
                ValidarCadena(password.Text) &&
                ValidarCadena(puesto.Text))
            {
                resultado = true;
            }
            else
            {
                MostrarEstadoDeValidacionCadena(TextBoxNombre);
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

        public void CapturarEmpleado()
        {
            Empleado.Nombre = TextBoxNombre.Text;
            Empleado.NombreDeUsuario = Usuario.Text;
            Empleado.Contraseña = password.Text;
            Empleado.CorreoElectronico = correo.Text;
            Empleado.TipoDeEmpleado = TipoDeEmpleado.Mesero;
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
