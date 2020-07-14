using LogicaDeNegocio.Clases;
using LogicaDeNegocio.ObjetosAccesoADatos;
using LogicaDeNegocio.Servicios;
using System;
using System.Collections.Generic;
using System.IO;
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
	/// Interaction logic for GUIEditarProducto.xaml
	/// </summary>
	public partial class GUIEditarProducto : Page
	{
		private string DireccionDeArchivo { get; set; }
		private ControladorDeCambioDePantalla Controlador { get; set; }
		private Empleado Gerente { get; set; }
		private Producto Producto { get; set; } = new Producto();
		private double Ganancia { get; set; }
		private bool ImagenFueModificada { get; set; }

		public GUIEditarProducto(ControladorDeCambioDePantalla controlador, Empleado gerente, Producto producto)
		{
			InitializeComponent();
			Controlador = controlador;
			Gerente = gerente;
			Producto = producto;
			TextBoxNombre.Text = producto.Nombre;
			TextBoxCodigo.Text = producto.Codigo;
			TextBoxCodigoDeBarras.Text = producto.CodigoDeBarras;
			TextBoxCantidadEnInventario.Text = producto.CantidadEnInventario.ToString();
			TextBoxCosto.Text = producto.Costo.ToString();
			TextBoxPrecioAlPublico.Text = producto.Precio.ToString();
			ImageImagenDeProducto.Source = CargarImagen(producto.Imagen);
		}

		private void TextBoxNombre_TextChanged(object sender, TextChangedEventArgs e)
		{
			MostrarEstadoDeValidacionCadena(sender as TextBox);
		}

		private void TextBoxCodigoDeBarras_TextChanged(object sender, TextChangedEventArgs e)
		{
			MostrarEstadoDeValidacionCadena(sender as TextBox);
		}

		private void TextBoxCodigo_TextChanged(object sender, TextChangedEventArgs e)
		{
			MostrarEstadoDeValidacionCadena(sender as TextBox);
		}

		private void TextBoxDescripción_TextChanged(object sender, TextChangedEventArgs e)
		{
			MostrarEstadoDeValidacionCadena(sender as TextBox);
		}

		private void TextBoxNotas_TextChanged(object sender, TextChangedEventArgs e)
		{
			MostrarEstadoDeValidacionCadenaVacioPermitido(sender as TextBox);
		}

		private void TextBoxPrecioAlPublico_TextChanged(object sender, TextChangedEventArgs e)
		{
			string precioAlPublico = (sender as TextBox).Text;
			if (ValidarNumeroDecimal(precioAlPublico) && ValidarCadena(precioAlPublico))
			{
				Producto.Precio = double.Parse(precioAlPublico);
				ActualizarGanancia();
			}
			MostrarEstadoDeValidacionNumero((TextBox)sender);
		}

		private void ActualizarGanancia()
		{
			Ganancia = Producto.Precio - Producto.Costo;
			if (Ganancia > 0)
			{
				LabelGanancia.Foreground = Brushes.Green;
			}
			else
			{
				LabelGanancia.Foreground = Brushes.Red;
			}
			LabelGanancia.Content = Ganancia;
		}

		private void TextBoxCosto_TextChanged(object sender, TextChangedEventArgs e)
		{
			string costo = (sender as TextBox).Text;
			if (ValidarNumeroDecimal(costo) && ValidarCadena(costo))
			{
				Producto.Costo = double.Parse(costo);
				ActualizarGanancia();
			}
			MostrarEstadoDeValidacionNumero((TextBox)sender);
		}

		private void TextBoxCantidadEnInventario_TextChanged(object sender, TextChangedEventArgs e)
		{
			string costo = (sender as TextBox).Text;
			if (ValidarEntero(costo) && ValidarCadena(costo))
			{
				Producto.CantidadEnInventario = int.Parse(costo);
				ActualizarGanancia();
			}
			MostrarEstadoDeValidacionNumero((TextBox)sender);
		}

		private void ButtonSeleccionarArchivo_Click(object sender, RoutedEventArgs e)
		{
			DireccionDeArchivo = MostrarVentanaDeSeleccionDeArchivos();

			if (!string.IsNullOrEmpty(DireccionDeArchivo))
			{
				LabelDireccionDeArchivo.Content = DireccionDeArchivo;
				ImageImagenDeProducto.Source = new BitmapImage(new Uri(DireccionDeArchivo));
				ImageImagenDeProducto.Visibility = Visibility.Visible;
				ImagenFueModificada = true;
			}
			else
			{
				LabelDireccionDeArchivo.Content = "Ningun archivo seleccionado";
				ImageImagenDeProducto.Visibility = Visibility.Hidden;
				ImagenFueModificada = false;
			}
		}

		private void ButtonCancelarRegistro_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult resultadoDeMessageBox = MessageBox.Show("¿Esta seguro que desea cancelar la edición? Se perderan los cambios sin guardar", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
			if (resultadoDeMessageBox == MessageBoxResult.Yes)
			{
				Controlador.Regresar();
			}
		}

		private void ButtonRegistrarProducto_Click(object sender, RoutedEventArgs e)
		{
			if (ValidarCampos())
			{
				if (ValidarGanancia())
				{
					ActualizarProducto();
				}
				else
				{
					MessageBoxResult resultadoDeMesageBox = MessageBox.Show("Esta a punto de guardar un producto con GANANCIA NEGATIVA por lo que se venderia este platillo con PERDIDA. ¿Esta seguro que desea continuar?", "ADVERTENCIA", MessageBoxButton.YesNo, MessageBoxImage.Error);
					if (resultadoDeMesageBox == MessageBoxResult.Yes)
					{
						ActualizarProducto();
					}
				}
			}
			else
			{
				MessageBox.Show("Verifique los campos remarcados en rojo", "Campos invalidos", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void ActualizarProducto()
		{
			if (ImagenFueModificada)
			{
				Producto.Imagen = ServiciosDeIO.CargarBytesDeArchivo(DireccionDeArchivo);
			}
			Producto.Nombre = TextBoxNombre.Text;
			Producto.Codigo = TextBoxCodigo.Text;
			Producto.CodigoDeBarras = TextBoxCodigoDeBarras.Text;
			Producto.Creador = Gerente.Nombre;

			ProductoDAO productoDAO = new ProductoDAO();
			try
			{
				productoDAO.ActualizarProducto(Producto);
			}
			catch (Exception e)
			{
				MessageBox.Show("Hubo un problema conectandose a la base de datos. Contacte a su administrador.", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
				return;
			}
			MessageBox.Show("¡El producto fue actualizado exitosamente!", "¡Exito!", MessageBoxButton.OK, MessageBoxImage.Information);
			Controlador.Regresar();
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

		private bool ValidarCampos()
		{
			bool resultado = false;
			if (ValidarCadena(TextBoxNombre.Text) &&
				ValidarCadena(TextBoxCodigo.Text) &&
				ValidarCadena(TextBoxCodigoDeBarras.Text) &&
				(!string.IsNullOrEmpty(DireccionDeArchivo)|| !ImagenFueModificada))
			{
				resultado = true;
			}
			else
			{
				MostrarEstadoDeValidacionCadena(TextBoxNombre);
				MostrarEstadoDeValidacionCadena(TextBoxCodigo);
				MostrarEstadoDeValidacionCadena(TextBoxCodigoDeBarras);
			}

			return resultado;
		}

		private void TextBoxPrecioAlPublico_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			TextBox padre = sender as TextBox;
			if (!ValidarEntradaDeDatosSoloDecimal(e.Text))
			{
				e.Handled = true;
			}
		}

		private void TextBoxCosto_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			TextBox padre = sender as TextBox;
			if (!ValidarEntradaDeDatosSoloDecimal(e.Text))
			{
				e.Handled = true;
			}
		}

		private static BitmapImage CargarImagen(byte[] imagen)
		{
			if (imagen == null || imagen.Length == 0) return null;
			var bitmapImage = new BitmapImage();
			using (var stream = new MemoryStream(imagen))
			{
				stream.Position = 0;
				bitmapImage.BeginInit();
				bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
				bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
				bitmapImage.UriSource = null;
				bitmapImage.StreamSource = stream;
				bitmapImage.EndInit();
			}
			bitmapImage.Freeze();
			return bitmapImage;
		}
	}
}
