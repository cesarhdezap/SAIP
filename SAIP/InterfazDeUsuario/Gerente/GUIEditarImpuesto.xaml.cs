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
using static InterfazDeUsuario.UtileriasGráficas;
using static LogicaDeNegocio.Servicios.ServiciosDeValidacion;

namespace InterfazDeUsuario.Gerente
{
    /// <summary>
    /// Interaction logic for GUIEditarImpuesto.xaml
    /// </summary>
    public partial class GUIEditarImpuesto : Page
    {
        private List<Iva> Ivas = new List<Iva>();
        private Iva IvaActual = new Iva();
        private Empleado Empleado;
        private ControladorDeCambioDePantalla Controlador;

        public GUIEditarImpuesto(ControladorDeCambioDePantalla controlador, Empleado empleado)
        {
            InitializeComponent();
            MostrarIvas();
            Empleado = empleado;
            BarraDeEstado.Controlador = controlador;
            BarraDeEstado.ActualizarEmpleado(empleado);
            Controlador = controlador;
        }

        private void MostrarIvas()
        {
            IvaDAO ivaDAO = new IvaDAO();
            Ivas = ivaDAO.CargarTodos();
            IvaActual = Ivas.FirstOrDefault(i => i.Activo == true);
            if(IvaActual != null)
            {
                LabelValorIvaActual.Content = "Valor: " + IvaActual.Valor;
                LabelFechaCreacionIvaActual.Content = "Fecha creación: " + IvaActual.FechaDeCreacion.ToString();
                LabelFechaDeInicioIvaActual.Content = "Fecha de inicio: " + IvaActual.FechaDeInicio.ToString();
                LabelCreadorIvaActual.Content = "Creador: " + IvaActual.Creador;
            }
            Ivas.Remove(IvaActual);

            DataGridIvasAnteriores.ItemsSource = null;
            DataGridIvasAnteriores.ItemsSource = Ivas;
        }

        private void ButtonActualizarIva_Click(object sender, RoutedEventArgs e)
        {
            Iva ivaAGuardar = new Iva()
            {
                Creador = Empleado.Nombre,
                FechaDeCreacion = DateTime.Now
            };
            bool validacion = false;

            if (double.TryParse(TextBoxValor.Text, out double valor) && DatePickerFechaDeInicio.SelectedDate >= DateTime.Now.Date)
            {
                ivaAGuardar.Valor = valor;
                ivaAGuardar.FechaDeInicio = DatePickerFechaDeInicio.SelectedDate.GetValueOrDefault();
                if (valor <= 0 && valor > 100)
                {
                    validacion = false;
                }
                validacion = true;
            }

            if (validacion && ValidarNumeroDecimal(TextBoxValor.Text))
            {
                if (ivaAGuardar.Valor <= 0 && ivaAGuardar.Valor > 100)
                {
                    MessageBox.Show("Debe insertar un valor mayor a 0 y menor o igual a 100.");   
                }
                else
                {
                    ivaAGuardar.Valor = ivaAGuardar.Valor / 100;
                    IvaDAO ivaDAO = new IvaDAO();
                    ivaAGuardar.Activo = false;
                    if (DatePickerFechaDeInicio.SelectedDate.GetValueOrDefault().Date == DateTime.Now.Date)
                    {
                        ivaAGuardar.Activo = true;
                    }

                    ivaDAO.Guardar(ivaAGuardar);
                    MessageBox.Show("Iva registrado correctamente!", "NOTIFICACION", MessageBoxButton.OK);
                    Controlador.Regresar();
                }
            }
            else
            {
                MessageBox.Show("Ingrese un numero como valor");
            }
        }

        private void TextBoxValor_TextChanged(object sender, TextChangedEventArgs e)
        {
            MostrarEstadoDeValidacionNumero(TextBoxValor);
        }

        private void DatePickerFechaDeInicio_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(DatePickerFechaDeInicio.SelectedDate >= DateTime.Now.Date))
            {
                MessageBox.Show("Ingrese una fecha posterior o igual a la de hoy");
                DatePickerFechaDeInicio.SelectedDate = null;
            }
        }
    }
}
