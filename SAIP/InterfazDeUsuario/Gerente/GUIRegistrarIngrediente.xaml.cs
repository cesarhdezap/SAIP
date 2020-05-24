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
using LogicaDeNegocio.Servicios;

namespace InterfazDeUsuario.Gerente
{
    /// <summary>
    /// Lógica de interacción para GUIRegistrarIngrediente.xaml
    /// </summary>
    public partial class GUIRegistrarIngrediente : Page
    {
        Empleado Empleado;
        readonly ControladorDeCambioDePantalla Controlador;
        public GUIRegistrarIngrediente(ControladorDeCambioDePantalla controlador, Empleado empleado)
        {
            Controlador = controlador;
            Empleado = empleado;
            InitializeComponent();
            BarraDeEstado.Controlador = controlador;
            BarraDeEstado.ActualizarNombreDeUsuario(empleado.NombreDeUsuario);
            GridCompuestos.Visibility = Visibility.Collapsed;
            LabelCodigoBarra.Visibility = Visibility.Collapsed;
            TextBoxCodigoBarras.Visibility = Visibility.Collapsed;
            ComboBoxUnidadMedida.ItemsSource = Enum.GetValues(typeof(UnidadDeMedida));
            ComboBoxUnidadMedida.SelectedIndex = 0;
        }

        private void CheckBoxIngredienteCompuesto_Checked(object sender, RoutedEventArgs e)
        {
            GridCompuestos.Visibility = Visibility.Visible;
            TextBoxCantidad.IsEnabled = false;
            ComboBoxUnidadMedida.IsEnabled = false;
            TextBoxCosto.IsEnabled = false;
        }

        private void CheckBoxIngredienteCompuesto_Unchecked(object sender, RoutedEventArgs e)
        {
            GridCompuestos.Visibility = Visibility.Collapsed;
            TextBoxCantidad.IsEnabled = true;
            ComboBoxUnidadMedida.IsEnabled = true;
            TextBoxCosto.IsEnabled = true;
        }

        private void CheckBoxCodigoBarra_Checked(object sender, RoutedEventArgs e)
        {
            LabelCodigoBarra.Visibility = Visibility.Visible;
            TextBoxCodigoBarras.Visibility = Visibility.Visible;
            LabelCodigoBarra.IsEnabled = true;
            TextBoxCodigoBarras.IsEnabled = true;
        }

        private void CheckBoxCodigoBarra_Unchecked(object sender, RoutedEventArgs e)
        {
            LabelCodigoBarra.Visibility = Visibility.Collapsed;
            TextBoxCodigoBarras.Visibility = Visibility.Collapsed;
        }

        private void TextBoxCodigoBarras_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox padre = sender as TextBox;
            if (!ServiciosDeValidacion.ValidarEntradaDeDatosSoloEntero(e.Text))
            {
                e.Handled = true;
            }
        }

        private void TextBoxCodigoBarras_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string entrada = (string)e.DataObject.GetData(typeof(string));
                if (!ServiciosDeValidacion.ValidarEntero(entrada))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void TextBoxCodigoBarras_TextChanged(object sender, TextChangedEventArgs e)
        {
            UtileriasGráficas.MostrarEstadoDeValidacionTelefono((TextBox)sender);
        }

        private void TextBoxCosto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox padre = sender as TextBox;
            if (!ServiciosDeValidacion.ValidarEntradaDeDatosSoloDecimal(e.Text))
            {
                e.Handled = true;
            }
        }

        private void TextBoxCosto_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string entrada = (string)e.DataObject.GetData(typeof(string));
                if (!ServiciosDeValidacion.ValidarNumeroDecimal(entrada))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void TextBoxCosto_TextChanged(object sender, TextChangedEventArgs e)
        {
            UtileriasGráficas.MostrarEstadoDeValidacionTelefono((TextBox)sender);
        }

        private void TextBoxCantidad_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox padre = sender as TextBox;
            if (!ServiciosDeValidacion.ValidarEntradaDeDatosSoloDecimal(e.Text))
            {
                e.Handled = true;
            }
        }

        private void TextBoxCantidad_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string entrada = (string)e.DataObject.GetData(typeof(string));
                if (!ServiciosDeValidacion.ValidarNumeroDecimal(entrada))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void TextBoxCantidad_TextChanged(object sender, TextChangedEventArgs e)
        {
            UtileriasGráficas.MostrarEstadoDeValidacionTelefono((TextBox)sender);
        }

        private void TextBoxCantidadCompuesto_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string entrada = (string)e.DataObject.GetData(typeof(string));
                if (!ServiciosDeValidacion.ValidarNumeroDecimal(entrada))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void TextBoxCantidadCompuesto_TextChanged(object sender, TextChangedEventArgs e)
        {
            UtileriasGráficas.MostrarEstadoDeValidacionTelefono((TextBox)sender);
        }

        private void TextBoxCantidadCompuesto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox padre = sender as TextBox;
            if (!ServiciosDeValidacion.ValidarEntradaDeDatosSoloDecimal(e.Text))
            {
                e.Handled = true;
            }
        }

        private void ButtonGuardar_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
