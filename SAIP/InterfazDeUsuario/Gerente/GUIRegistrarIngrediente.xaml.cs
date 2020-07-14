using LogicaDeNegocio.Clases;
using LogicaDeNegocio.Enumeradores;
using LogicaDeNegocio.ObjetosAccesoADatos;
using LogicaDeNegocio.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
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
        List<Ingrediente> Ingredientes = new List<Ingrediente>();


        public GUIRegistrarIngrediente(ControladorDeCambioDePantalla controlador, Empleado empleado)
        {
            Controlador = controlador;
            Empleado = empleado;
            InitializeComponent();
            IngredienteDAO ingredienteDAO = new IngredienteDAO();
            try
            {
                Ingredientes = ingredienteDAO.CargarIngredientesActivos();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message + "Porfavor contacte a su administrador", "Error! ", MessageBoxButton.OK);
                controlador.Regresar();

            }

            DataGridIngredientes.ItemsSource = Ingredientes;

            BarraDeEstado.Controlador = controlador;
            BarraDeEstado.ActualizarEmpleado(empleado);
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
            IngredienteDAO ingredienteDAO = new IngredienteDAO();

            if (ValidarEntradas())
            {
                AsignarValoresAIngrediente();

                if (!ValidarIngredienteExistente(ingredienteNuevo))
                {
                    try
                    {
                        ingredienteDAO.GuardarIngrediente(ingredienteNuevo);
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace.ToString(), "Alerta");
                    }

                }
                else
                {
                    MessageBox.Show("Este ingrediente ya se encuentra registrado, si desea aumentar su cantidad porfavor dirigase al apartado Editar Ingrediente");
                }

            }
            else
            {
                MostrarEstadoDeValidacion();
            }
        }

        private bool ValidarIngredienteExistente(Ingrediente ingrediente)
        {
            bool resultado = false;

            List<Ingrediente> ingredientes = new List<Ingrediente>();

            IngredienteDAO ingredienteDAO = new IngredienteDAO();

            ingredientes = ingredienteDAO.CargarTodos();

            if (ingredientes.Contains(ingrediente))
            {
                resultado = true;
            }

            return resultado;
        }

        private bool ValidarEntradas()
        {
            bool resultado = false;
            if (ServiciosDeValidacion.ValidarCadena(TextBoxCodigo.Text)
                && ServiciosDeValidacion.ValidarCadena(TextBoxNombre.Text)
                && ServiciosDeValidacion.ValidarCadena(TextBoxCodigoBarras.Text)
                && ServiciosDeValidacion.ValidarNumeroDecimal(TextBoxCosto.Text)
                && ServiciosDeValidacion.ValidarNumeroDecimal(TextBoxCantidad.Text))
            {
                if (CheckBoxIngredienteCompuesto.IsChecked == true)
                {
                    if (Componentes.Count <= 0)
                    {
                        MessageBox.Show("Este ingrediente no tiene componentes.", "Alerta");
                    }
                    else
                    {
                        resultado = true;
                    }
                }
                else
                {
                    resultado = true;
                }
            }

            return resultado;
        }

        private void AsignarValoresAIngrediente()
        {
            ingredienteNuevo.Codigo = TextBoxCodigo.Text;
            ingredienteNuevo.Nombre = TextBoxNombre.Text;
            ingredienteNuevo.UnidadDeMedida = (UnidadDeMedida)ComboBoxUnidadMedida.SelectedItem;
            ingredienteNuevo.CodigoDeBarras = TextBoxCodigoBarras.Text;
            ingredienteNuevo.Creador = Empleado.Nombre;
            ingredienteNuevo.Costo = double.Parse(TextBoxCosto.Text);
            ingredienteNuevo.CantidadEnInventario = double.Parse(TextBoxCantidad.Text);

            if (CheckBoxIngredienteCompuesto.IsChecked == true)
            {
                ingredienteNuevo.Componentes = Componentes;
            }

        }

        private void MostrarEstadoDeValidacion()
        {
            UtileriasGráficas.MostrarEstadoDeValidacionCadena(TextBoxCodigo);
            UtileriasGráficas.MostrarEstadoDeValidacionCadena(TextBoxCodigoBarras);
            UtileriasGráficas.MostrarEstadoDeValidacionCadena(TextBoxNombre);
            UtileriasGráficas.MostrarEstadoDeValidacionNumero(TextBoxCosto);
            UtileriasGráficas.MostrarEstadoDeValidacionNumero(TextBoxCantidad);
        }

        private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
        {
            Controlador.Regresar();
        }

        private void ActualizarPantalla()
        {
            DataGridComponentes.ItemsSource = null;
            DataGridComponentes.ItemsSource = Componentes;
        }

        private void ActualizarPrecio()
        {
            TextBoxCosto.Text = ingredienteNuevo.CalcularCosto().ToString();
        }

        private void ButtonAñadir_Click(object sender, RoutedEventArgs e)
        {
            Ingrediente ingredienteAAñadir = ((FrameworkElement)sender).DataContext as Ingrediente;
            if (!Componentes.Any(c => c.Ingrediente.Id == ingredienteAAñadir.Id))
            {
                Componente componente = new Componente();
                componente.Compuesto = ingredienteNuevo;
                componente.Ingrediente = ingredienteAAñadir;
                componente.Cantidad = 1;
                Componentes.Add(componente);

            }
            else
            {
                Componentes.FirstOrDefault(c => c.Ingrediente.Id == ingredienteAAñadir.Id).Cantidad += 1;

            }

            ActualizarPrecio();
            ActualizarPantalla();
        }

        private void ButtonEliminar_Click(object sender, RoutedEventArgs e)
        {
            Componente ingredienteAEliminar = ((FrameworkElement)sender).DataContext as Componente;

            Componentes.Remove(ingredienteAEliminar);
            ActualizarPrecio();
            ActualizarPantalla();
        }

        private void ButtonDisminuir_Click(object sender, RoutedEventArgs e)
        {
            Componente ingredienteADisminuir = ((FrameworkElement)sender).DataContext as Componente;

            if (ingredienteADisminuir.Cantidad -1 > 0 )
            {
                ingredienteADisminuir.Cantidad--;
            }
            else
            {
                Componentes.Remove(ingredienteADisminuir);
            }
            ActualizarPrecio();
            ActualizarPantalla();
        }

        private void ButtonAumentar_Click(object sender, RoutedEventArgs e)
        {
            Componente ingredienteADisminuir = ((FrameworkElement)sender).DataContext as Componente;

            if (ingredienteADisminuir.Cantidad > 0)
            {
                ingredienteADisminuir.Cantidad++;
            }

            ActualizarPrecio();
            ActualizarPantalla();
        }
    }
}

