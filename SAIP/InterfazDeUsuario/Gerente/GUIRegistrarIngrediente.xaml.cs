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
            Ingrediente ingredienteNuevo = new Ingrediente
            {
                Codigo = TextBoxCodigo.Text,
                Nombre = TextBoxNombre.Text,
                Costo = double.Parse(TextBoxCosto.Text),
                CantidadEnInventario = double.Parse(TextBoxCantidad.Text),
                UnidadDeMedida = (UnidadDeMedida)ComboBoxUnidadMedida.SelectedItem,
                CodigoDeBarras = TextBoxCodigoBarras.Text,
                Creador = Empleado.Nombre
            };

            if (CheckBoxIngredienteCompuesto.IsChecked == true && Componentes.Count > 0)
            {
                ingredienteNuevo.Componentes = Componentes;
            }
            else
            {
                MessageBox.Show("El ingrediente compuesto no tiene ningun componente, porfavor agregar componentes.", "Alerta");
            }

            IngredienteDAO ingredienteDAO = new IngredienteDAO();

            bool resultadoValidacion = false;

            if (ingredienteNuevo.ValidarParaGuardar())
            {
                resultadoValidacion = true;
            }
            else
            {
                UtileriasGráficas.MostrarEstadoDeValidacionCadena(TextBoxCodigo);
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
                catch (InvalidOperationException ex)
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
            Ingrediente ingredienteHijo = new Ingrediente()
            {
                Codigo = TextBoxCodigoCompuesto.Text,
                CantidadEnInventario = double.Parse(TextBoxCantidadCompuesto.Text)
            };

            Ingrediente ingredientePadre = new Ingrediente()
            {
                Codigo = TextBoxCodigo.Text
            };

            IngredienteDAO ingredienteDAO = new IngredienteDAO();

            if (ServiciosDeValidacion.ValidarNumeroDecimal(ingredienteHijo.CantidadEnInventario.ToString()) && ServiciosDeValidacion.ValidarCadena(ingredienteHijo.Codigo))
            {
                if (ingredienteDAO.ValidarCodigoExistente(ingredienteHijo.Codigo))
                {
                    Componente componente = new Componente();
                    componente.Cantidad = ingredienteHijo.CantidadEnInventario;

                    ingredienteHijo = ingredienteDAO.RecuperarIngredientePorCodigo(ingredienteHijo.Codigo);

                    componente.Compuesto = ingredientePadre;
                    componente.Ingrediente = ingredienteHijo;

                    Componentes.Add(componente);
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
