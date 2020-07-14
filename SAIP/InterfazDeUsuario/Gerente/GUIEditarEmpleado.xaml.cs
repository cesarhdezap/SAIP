using LogicaDeNegocio.Clases;
using LogicaDeNegocio.Enumeradores;
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
using LogicaDeNegocio.ObjetosAccesoADatos;
using InterfazDeUsuario.UserControls;

namespace InterfazDeUsuario.empleado
{
    /// <summary>
    /// Lógica de interacción para GUI_EditarEmpleado.xaml
    /// </summary>
    public partial class GUI_EditarEmpleado : Page
    {
        ControladorDeCambioDePantalla Controlador;
        public Empleado Gerente { get; set; }
        public Empleado empleadoaEditar { get; set; } = new Empleado();
        public GUI_EditarEmpleado(ControladorDeCambioDePantalla controlador, Empleado EmpleadoCargado, Empleado empleadoAEditar)
        {
            InitializeComponent();
            ComboBoxPuesto.ItemsSource = Enum.GetValues(typeof(TipoDeEmpleado));
            ComboBoxPuesto.SelectedIndex = 0;
            Gerente = EmpleadoCargado;
            BarraDeEstado.Controlador = controlador;
            Controlador = controlador;
            BarraDeEstado.ActualizarEmpleado(Gerente);
        }

        private void ButtonCambiar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarCampos())
            {

                MessageBoxResult resultadoDeMesageBox = MessageBox.Show("Esta a punto de Editar la Informacion de un Empleado dentro del sistema ¿Esta seguro que desea continuar?", "ADVERTENCIA", MessageBoxButton.YesNo, MessageBoxImage.Error);
                if (resultadoDeMesageBox == MessageBoxResult.Yes)
                {
                    EditarEmpleado();

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
            TextBoxUsuario.Clear();
            TextBoxCorreo.Clear();
            PasswordBoxContraseña.Clear();

        }

        private bool ValidarCampos()
        {
            bool resultado = false;
            if (ValidarNombre(TextBoxNombre.Text) &&
                ValidarCadena(TextBoxUsuario.Text) &&
                ValidarCorreoElectronico(TextBoxCorreo.Text) &&
                ValidarContraseña(PasswordBoxContraseña.Password) &&
                ValidarCadena(ComboBoxPuesto.Text))
            {
                resultado = true;
            }
            else
            {
                MostrarEstadoDeValidacionCadena(TextBoxNombre);
                MostrarEstadoDeValidacionCadena(TextBoxUsuario);
                MostrarEstadoDeValidacionCorreoElectronico(TextBoxCorreo);
                MostrarEstadoDeValidacionContraseña(PasswordBoxContraseña);

            }

            return resultado;
        }
        public void EditarEmpleado()
        {
            empleadoaEditar.Nombre = TextBoxNombre.Text;
            empleadoaEditar.NombreDeUsuario = TextBoxUsuario.Text;
            empleadoaEditar.Contraseña = PasswordBoxContraseña.Password;
            empleadoaEditar.CorreoElectronico = TextBoxCorreo.Text;
            empleadoaEditar.TipoDeEmpleado = (TipoDeEmpleado)ComboBoxPuesto.SelectedItem;
            empleadoaEditar.Creador = Gerente.Nombre;
            EmpleadoDAO empleadoDAO = new EmpleadoDAO();
            empleadoDAO.EditarEmpleado(empleadoaEditar);
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

        private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultadoDeMessageBox = MessageBox.Show("¿Esta seguro que desea cancelar la edicion? Se perderan los cambios sin guardar", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (resultadoDeMessageBox == MessageBoxResult.Yes)
            {
                Controlador.Regresar();
            }
        }
    }
}
