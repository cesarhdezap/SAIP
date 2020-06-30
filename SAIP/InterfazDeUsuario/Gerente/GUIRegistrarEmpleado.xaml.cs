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
using LogicaDeNegocio.Enumeradores;
using static LogicaDeNegocio.Servicios.ServiciosDeEncriptacion;

namespace InterfazDeUsuario.empleado
{
    /// <summary>
    /// Lógica de interacción para GUIRegistrarEmpleado.xaml
    /// </summary>

    public partial class GUIRegistrarEmpleado : Page
    {
        public Empleado Gerente { get; set; }
        public List<Empleado> Trabajadores { get; set; }
        public List<Empleado> Visible { get; set; }
        public Empleado EmpleadoRegistrar { get; set; } = new Empleado();
        ControladorDeCambioDePantalla Controlador;
        public GUIRegistrarEmpleado(ControladorDeCambioDePantalla controlador, Empleado empleadoCargado)
        {
            InitializeComponent();
            ComboBoxPuesto.ItemsSource = Enum.GetValues(typeof(TipoDeEmpleado));
            ComboBoxPuesto.SelectedIndex = 0;
            Gerente = empleadoCargado;
            BarraDeEstado.Controlador = controlador;
            Controlador = controlador;
            BarraDeEstado.ActualizarNombreDeUsuario(Gerente.Nombre);
            EmpleadoDAO empleadoDAO = new EmpleadoDAO();
            Trabajadores = empleadoDAO.CargarTodos();
            Visible = Trabajadores;

        }


        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarCampos())
            {
                
                MessageBoxResult resultadoDeMesageBox = MessageBox.Show("Esta a punto de guardar un Empleado nuevo dentro del sistema ¿Esta seguro que desea continuar?", "ADVERTENCIA", MessageBoxButton.YesNo, MessageBoxImage.Error);
                if (resultadoDeMesageBox == MessageBoxResult.Yes)
                {
                    CapturarEmpleado();
                    
                }
            }
            else
            {
                 MessageBox.Show("Verifique los campos remarcados en rojo", "Campos invalidos", MessageBoxButton.OK, MessageBoxImage.Error);
                
            }
        }

        public void ActualizarPantalla()
        {
            TextBoxNombre.Clear();
            Usuario.Clear();
            correo.Clear();
            PasswordBoxContraseña.Clear();
            
        }

        private bool ValidarCampos()
        {
            bool resultado = false;
            if (ValidarNombre(TextBoxNombre.Text) &&
                ValidarCadena(Usuario.Text) &&
                ValidarCorreoElectronico(correo.Text) &&
                ValidarContraseña(PasswordBoxContraseña.Password)&&
                ValidarCadena(ComboBoxPuesto.Text))
            {
                resultado = true;
            }
            else
            {
                MostrarEstadoDeValidacionCadena(TextBoxNombre);
                MostrarEstadoDeValidacionCadena(Usuario);
                MostrarEstadoDeValidacionCorreoElectronico(correo);
                MostrarEstadoDeValidacionContraseña(PasswordBoxContraseña);

            }

            return resultado;
        }
            
        public void CapturarEmpleado()
        {
            EmpleadoRegistrar.Nombre = TextBoxNombre.Text;
            EmpleadoRegistrar.NombreDeUsuario = Usuario.Text;
            EmpleadoRegistrar.Contraseña = EncriptarCadena(PasswordBoxContraseña.Password);
            EmpleadoRegistrar.CorreoElectronico = correo.Text;
            EmpleadoRegistrar.TipoDeEmpleado = (TipoDeEmpleado)ComboBoxPuesto.SelectedItem;
            EmpleadoRegistrar.Creador = Gerente.Nombre;
            EmpleadoDAO empleadoDAO = new EmpleadoDAO();
            empleadoDAO.GuardarEmpleado(EmpleadoRegistrar);
            MessageBox.Show("Empleado Registrado con Exito!", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Usuario_TextChanged(object sender, TextChangedEventArgs e)
        {
            MostrarEstadoDeValidacionCadena((TextBox)sender);
        }

        private void correo_TextChanged(object sender, TextChangedEventArgs e)
        {
            MostrarEstadoDeValidacionCorreoElectronico((TextBox)sender);
        }


        private void Nombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            MostrarEstadoDeValidacionCadena((TextBox)sender);

        }

        private void PasswordBoxContraseña_PasswordChanged(object sender, RoutedEventArgs e)
        {
            MostrarEstadoDeValidacionContraseña((PasswordBox)sender);
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultadoDeMessageBox = MessageBox.Show("¿Esta seguro que desea cancelar el Registro? Se perderan los cambios sin guardar", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (resultadoDeMessageBox == MessageBoxResult.Yes)
            {
                Controlador.Regresar();
            }
        }
    }
}
