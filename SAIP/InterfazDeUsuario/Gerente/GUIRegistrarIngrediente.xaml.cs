using LogicaDeNegocio.Clases;
using LogicaDeNegocio.Enumeradores;
using LogicaDeNegocio.ObjetosAccesoADatos;
using LogicaDeNegocio.Servicios;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace InterfazDeUsuario.Gerente
{
    /// <summary>
    /// Lógica de interacción para GUIRegistrarIngrediente.xaml
    /// </summary>
    public partial class GUIRegistrarIngrediente : Page
    {
        Empleado Empleado;
        readonly ControladorDeCambioDePantalla Controlador;
        List<Componente> Componentes = new List<Componente>();
        Ingrediente ingredienteNuevo = new Ingrediente();

        public GUIRegistrarIngrediente(ControladorDeCambioDePantalla controlador, Empleado empleado)
        {
            Controlador = controlador;
            Empleado = empleado;
            InitializeComponent();
            BarraDeEstado.Controlador = controlador;
            BarraDeEstado.ActualizarNombreDeUsuario(empleado.NombreDeUsuario);
            GridCompuestos.Visibility = Visibility.Collapsed;
            ComboBoxUnidadMedida.ItemsSource = Enum.GetValues(typeof(UnidadDeMedida));
            ComboBoxUnidadMedida.SelectedIndex = 0;
        }

        private void CheckBoxIngredienteCompuesto_Checked(object sender, RoutedEventArgs e)
        {
            GridCompuestos.Visibility = Visibility.Visible;
            TextBoxCantidad.IsEnabled = false;
            TextBoxCosto.IsEnabled = false;
        }

        private void CheckBoxIngredienteCompuesto_Unchecked(object sender, RoutedEventArgs e)
        {
            GridCompuestos.Visibility = Visibility.Collapsed;
            TextBoxCantidad.IsEnabled = true;
            TextBoxCosto.IsEnabled = true;
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
            UtileriasGráficas.MostrarEstadoDeValidacionNumero((TextBox)sender);
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
            UtileriasGráficas.MostrarEstadoDeValidacionNumero((TextBox)sender);
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
            UtileriasGráficas.MostrarEstadoDeValidacionNumero((TextBox)sender);
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
            UtileriasGráficas.MostrarEstadoDeValidacionNumero((TextBox)sender);
        }

        private void TextBoxCantidadCompuesto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox padre = sender as TextBox;
            if (!ServiciosDeValidacion.ValidarEntradaDeDatosSoloDecimal(e.Text))
            {
                e.Handled = true;
            }
        }

        private void TextBoxCodigoCompuesto_TextChanged(object sender, TextChangedEventArgs e)
        {
            UtileriasGráficas.MostrarEstadoDeValidacionCadena((TextBox)sender);

        }

        private void TextBoxCodigo_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string entrada = (string)e.DataObject.GetData(typeof(string));
                if (!ServiciosDeValidacion.ValidarCadena(entrada))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void TextBoxCodigoCompuesto_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string entrada = (string)e.DataObject.GetData(typeof(string));
                if (!ServiciosDeValidacion.ValidarCadena(entrada))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void TextBoxCodigo_TextChanged(object sender, TextChangedEventArgs e)
        {
            UtileriasGráficas.MostrarEstadoDeValidacionCadena((TextBox)sender);
        }

        private void TextBoxCodigo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox padre = sender as TextBox;
            if (!ServiciosDeValidacion.ValidarCadena(e.Text))
            {
                e.Handled = true;
            }
        }

        private void TextBoxCodigoCompuesto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox padre = sender as TextBox;
            if (!ServiciosDeValidacion.ValidarCadena(e.Text))
            {
                e.Handled = true;
            }
        }

        private void TextBoxNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            UtileriasGráficas.MostrarEstadoDeValidacionCadena((TextBox)sender);
        }

        private void TextBoxNombre_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string entrada = (string)e.DataObject.GetData(typeof(string));
                if (!ServiciosDeValidacion.ValidarCadena(entrada))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void TextBoxNombre_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox padre = sender as TextBox;
            if (!ServiciosDeValidacion.ValidarCadena(e.Text))
            {
                e.Handled = true;
            }
        }

        private void ButtonGuardar_Click(object sender, RoutedEventArgs e)
        {
            ingredienteNuevo.Codigo = TextBoxCodigo.Text;
            ingredienteNuevo.Nombre = TextBoxNombre.Text;
            try
            {
                ingredienteNuevo.Costo = double.Parse(TextBoxCosto.Text);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Alerta");
            }
            try
            {
                ingredienteNuevo.CantidadEnInventario = double.Parse(TextBoxCantidad.Text);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Alerta");
            }
            ingredienteNuevo.UnidadDeMedida = (UnidadDeMedida)ComboBoxUnidadMedida.SelectedItem;
            ingredienteNuevo.CodigoDeBarras = TextBoxCodigoBarras.Text;
            ingredienteNuevo.Creador = Empleado.Nombre;

            if (CheckBoxIngredienteCompuesto.IsChecked == true && Componentes.Count > 0)
            {
                ingredienteNuevo.Componentes = Componentes;
            }
            else
            {
                MessageBox.Show("Este ingrediente no tiene componentes.", "Alerta");
            }

            IngredienteDAO ingredienteDAO = new IngredienteDAO();

            bool resultadoValidacion = false;

            try
            {
                if (ingredienteNuevo.ValidarParaGuardar())
                {
                    resultadoValidacion = true;
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Alerta");
                UtileriasGráficas.MostrarEstadoDeValidacionCadena(TextBoxCodigo);
                UtileriasGráficas.MostrarEstadoDeValidacionCadena(TextBoxCodigoBarras);
                UtileriasGráficas.MostrarEstadoDeValidacionCadena(TextBoxNombre);
                UtileriasGráficas.MostrarEstadoDeValidacionNumero(TextBoxCosto);
                UtileriasGráficas.MostrarEstadoDeValidacionNumero(TextBoxCantidad);

            }

            if (resultadoValidacion)
            {
                try
                {
                    ingredienteDAO.GuardarIngrediente(ingredienteNuevo);
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "Alerta");
                }

            }
        }

        private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
        {
            Controlador.Regresar();
        }

        private void ButtonAgregarComponente_Click(object sender, RoutedEventArgs e)
        {
            Ingrediente ingredienteHijo = new Ingrediente();

            ingredienteHijo.Codigo = TextBoxCodigoCompuesto.Text;

            try
            {
                ingredienteHijo.CantidadEnInventario = double.Parse(TextBoxCantidadCompuesto.Text);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Alerta");
            }

            IngredienteDAO ingredienteDAO = new IngredienteDAO();

            if (ServiciosDeValidacion.ValidarNumeroDecimal(ingredienteHijo.CantidadEnInventario.ToString()) && ServiciosDeValidacion.ValidarCadena(ingredienteHijo.Codigo))
            {
                if (ingredienteDAO.ValidarCodigoExistente(ingredienteHijo.Codigo))
                {
                    Componente componente = new Componente();
                    componente.Cantidad = ingredienteHijo.CantidadEnInventario;

                    ingredienteHijo = ingredienteDAO.RecuperarIngredientePorCodigo(ingredienteHijo.Codigo);

                    componente.Compuesto = ingredienteNuevo;
                    componente.Ingrediente = ingredienteHijo;

                    Componentes.Add(componente);
                    ingredienteNuevo.Componentes = Componentes;
                    TextBoxCosto.Text = ingredienteNuevo.CalcularCosto().ToString();
                }
                else
                {
                    MessageBox.Show("El codigo no esta asoiado a ningun ingrediente registrado \nPorfavor ingrese un código válido ", "Alerta");
                }
            }
            else
            {
                UtileriasGráficas.MostrarEstadoDeValidacionCadena(TextBoxCodigoCompuesto);
                UtileriasGráficas.MostrarEstadoDeValidacionNumero(TextBoxCantidadCompuesto);
            }

            DataGridComponentes.ItemsSource = null;
            DataGridComponentes.ItemsSource = Componentes;
        }

    }
}
