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
using System.Windows.Shapes;
using static LogicaDeNegocio.Servicios.ServiciosDeValidacion;
using static InterfazDeUsuario.UtileriasGráficas;

namespace InterfazDeUsuario.Gerente
{
	/// <summary>
	/// Interaction logic for GUIEditarPlatillo.xaml
	/// </summary>
	public partial class GUIEditarPlatillo : Page
	{
		public ControladorDeCambioDePantalla Controlador { get; set; }
		public Empleado Gerente { get; set; }
		public List<Ingrediente> IngredientesCargados { get; set; }
		public List<Ingrediente> IngredientesVisibles { get; set; }
		public List<Proporcion> proporcionesOriginales { get; set; }
		public Platillo Platillo { get; set; } = new Platillo();
		public double Ganancia { get; set; }
		private bool CandadoDeRefrescadoDeCajasDeTexto { get; set; } = true;
		public GUIEditarPlatillo(ControladorDeCambioDePantalla controlador, Empleado EmpleadoCargado, Platillo platillo)
		{
			InitializeComponent();
			Controlador = controlador;
			BarraDeEstado.Controlador = controlador;
			Gerente = EmpleadoCargado;
			Platillo = platillo;
			proporcionesOriginales = new List<Proporcion>();
			foreach(Proporcion proporcion in Platillo.Proporciones)
			{
				proporcionesOriginales.Add(proporcion);
			}
			BarraDeEstado.ActualizarNombreDeUsuario(Gerente.Nombre);
			IngredienteDAO ingredienteDAO = new IngredienteDAO();
			IngredientesCargados = ingredienteDAO.CargarIngredientesActivos();
			IngredientesVisibles = IngredientesCargados;
			BusquedaDataGrid.ItemsSource = IngredientesVisibles;
			AsignarPlatilloAInterfaz(Platillo);
		}

		private void BusquedaTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			string busqueda = BusquedaTextBox.Text;
			if (busqueda != string.Empty)
			{
				IngredientesVisibles = IngredientesCargados.TakeWhile(ingrediente => ingrediente.Nombre.ToLower().Contains(busqueda.ToLower())).ToList();
			}
			else
			{
				IngredientesVisibles = IngredientesCargados;
			}
			ActualizarPantalla();
		}
		
		private void AsignarPlatilloAInterfaz(Platillo platillo)
		{
			NombreTextBox.Text = platillo.Nombre;
			CodigoTextBox.Text = platillo.Codigo;
			NotasTextBox.Text = platillo.Notas;
			DescripcionTextBox.Text = platillo.Descripcion;
			PrecioAlPublicoTextBox.Text = platillo.Precio.ToString();
			ActualizarGanancia();
			ActualizarPantalla();
		}

		private void ActualizarPantalla()
		{
			BusquedaDataGrid.ItemsSource = null;
			BusquedaDataGrid.ItemsSource = IngredientesVisibles;
			IngredientesDataGrid.ItemsSource = null;
			IngredientesDataGrid.ItemsSource = Platillo.Proporciones;
			CostoValorLabel.Content = Platillo.CostoDeIngredientes;
		}

		private void CalcularCostoTotal()
		{
			Platillo.CostoDeIngredientes = 0;
			foreach (Proporcion proporcion in Platillo.Proporciones)
			{
				proporcion.Ingrediente.CalcularCosto();
				Platillo.CostoDeIngredientes += proporcion.CalcularCosto();
			}
		}

		private void ButtonAñadir_Click(object sender, RoutedEventArgs e)
		{
			Ingrediente ingredienteAAñadir = (Ingrediente)BusquedaDataGrid.SelectedItem;
			Platillo.AñadirIngredientePorId(ingredienteAAñadir.Id);
			CalcularCostoTotal();
			ActualizarGanancia();
			ActualizarPantalla();
		}

		private void ButtonEliminar_Click(object sender, RoutedEventArgs e)
		{
			Proporcion ingredienteAEliminar = ((FrameworkElement)sender).DataContext as Proporcion;
			Platillo.EliminarIngredientePorId(ingredienteAEliminar.Ingrediente.Id);
			CalcularCostoTotal();
			ActualizarGanancia();
			ActualizarPantalla();
		}
		private void CantidadDeIngrediente_TextChanged(object sender, RoutedEventArgs e)
		{
			if (CandadoDeRefrescadoDeCajasDeTexto)
			{
				Proporcion proporcion = ((FrameworkElement)sender).DataContext as Proporcion;
				TextBox padre = sender as TextBox;
				if (padre.Text != string.Empty)
				{
					proporcion.Cantidad = double.Parse(padre.Text);
				}
				CalcularCostoTotal();
				ActualizarPantalla();
			}
		}

		private void CantidadDeIngrediente_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			TextBox padre = sender as TextBox;
			if (!ValidarEntradaDeDatosSoloDecimal(e.Text))
			{
				e.Handled = true;
			}
		}

		private void TextBox_Loaded(object sender, RoutedEventArgs e)
		{
			Proporcion proporcion = ((FrameworkElement)sender).DataContext as Proporcion;
			TextBox padre = sender as TextBox;
			CandadoDeRefrescadoDeCajasDeTexto = false;
			padre.Text = proporcion.Cantidad.ToString();
			CandadoDeRefrescadoDeCajasDeTexto = true;
		}

		private void PrecioAlPublicoTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			string precioAlPublico = PrecioAlPublicoTextBox.Text;
			if (ValidarNumeroDecimal(precioAlPublico) && ValidarCadena(precioAlPublico))
			{
				Platillo.Precio = double.Parse(precioAlPublico);
				ActualizarGanancia();
			}
			MostrarEstadoDeValidacionNumero((TextBox)sender);
		}
		private void EditarPlatillo()
		{
			ObtenerProporcionesARemover();
			Platillo.Nombre = NombreTextBox.Text;
			Platillo.Codigo = CodigoTextBox.Text;
			Platillo.Descripcion = DescripcionTextBox.Text;
			Platillo.Notas = NotasTextBox.Text;
			Platillo.Codigo = CodigoTextBox.Text;
			foreach(Proporcion proporcion in Platillo.Proporciones)
			{
				proporcion.Alimento = Platillo;
			}
			PlatilloDAO platilloDAO = new PlatilloDAO();
			platilloDAO.EditarPlatillo(Platillo);
			MessageBox.Show("¡El platillo fue editado exitosamente!", "¡Exito!", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		private void ObtenerProporcionesARemover()
		{
			foreach (Proporcion proporcion in Platillo.Proporciones)
			{
				proporcionesOriginales.Remove(proporcion);
			}
		}

		private void GuardarButton_Click(object sender, RoutedEventArgs e)
		{
			if (ValidarCampos())
			{
				if (Platillo.Validar())
				{
					if (ValidarGanancia())
					{
						EditarPlatillo();
					}
					else
					{
						MessageBoxResult resultadoDeMesageBox = MessageBox.Show("Esta a punto de guardar un platillo con GANANCIA NEGATIVA por lo que se venderia este platillo con PERDIDA. ¿Esta seguro que desea continuar?", "ADVERTENCIA", MessageBoxButton.YesNo, MessageBoxImage.Error);
						if (resultadoDeMesageBox == MessageBoxResult.Yes)
						{
							EditarPlatillo();
						}
					}
				}
				else
				{
					MessageBox.Show("El platillo debe tener por lo menos un ingrediente para ser registrado", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
			else
			{
				MessageBox.Show("Verifique los campos remarcados en rojo", "Campos invalidos", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void ActualizarGanancia()
		{
			Ganancia = Platillo.Precio - Platillo.CostoDeIngredientes;
			if (Ganancia > 0)
			{
				GananciaLabel.Foreground = Brushes.Green;
			}
			else
			{
				GananciaLabel.Foreground = Brushes.Red;
			}
			GananciaLabel.Content = Ganancia;
		}

		private bool ValidarCampos()
		{
			bool resultado = false;
			if (ValidarCadena(NombreTextBox.Text) &&
				ValidarCadena(CodigoTextBox.Text) &&
				ValidarCadena(DescripcionTextBox.Text) &&
				ValidarCadena(NotasTextBox.Text))
			{
				resultado = true;
			}
			else
			{
				MostrarEstadoDeValidacionCadena(CodigoTextBox);
				MostrarEstadoDeValidacionCadena(NombreTextBox);
				MostrarEstadoDeValidacionCadena(DescripcionTextBox);
				MostrarEstadoDeValidacionCadena(NotasTextBox);

			}

			return resultado;
		}

		private bool ValidarGanancia()
		{
			bool resultado = false;
			if (Ganancia > 0)
			{
				resultado = true;
			}
			return resultado;
		}

		private void NombreTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			MostrarEstadoDeValidacionCadena((TextBox)sender);
		}

		private void CodigoTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			MostrarEstadoDeValidacionCadena((TextBox)sender);
		}

		private void DescripcionTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			MostrarEstadoDeValidacionCadena((TextBox)sender);
		}

		private void NotasTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			MostrarEstadoDeValidacionCadena((TextBox)sender);
		}

		private void CancelarButton_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult resultadoDeMessageBox = MessageBox.Show("¿Esta seguro que desea cancelar el registro? Se perderan los cambios sin guardar", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
			if (resultadoDeMessageBox == MessageBoxResult.Yes)
			{
				Controlador.Regresar();
			}
		}

		private void BusquedaTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{

		}
	}
}
