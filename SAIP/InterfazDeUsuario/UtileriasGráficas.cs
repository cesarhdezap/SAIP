using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using static LogicaDeNegocio.Servicios.ServiciosDeValidacion;

namespace InterfazDeUsuario
{
	public static class UtileriasGráficas
	{
		private static void MostrarToolTip(Control controlGrafico, string mensaje)
		{
			if (controlGrafico.ToolTip == null)
			{
				controlGrafico.ToolTip = new ToolTip()
				{
					Content = mensaje,
					Placement = System.Windows.Controls.Primitives.PlacementMode.Right,
				};
			}

			((ToolTip)controlGrafico.ToolTip).IsEnabled = true;
			ToolTipService.SetPlacementTarget((ToolTip)controlGrafico.ToolTip, controlGrafico);
			((ToolTip)controlGrafico.ToolTip).IsOpen = true;
		}

		private static void OcultarToolTip(Control controlGrafico)
		{
			if (controlGrafico.ToolTip != null)
			{
				((ToolTip)controlGrafico.ToolTip).IsOpen = false;
				((ToolTip)controlGrafico.ToolTip).IsEnabled = false;
				controlGrafico.ToolTip = null;
			}
		}

		public static void MostrarEstadoDeValidacionCadena(TextBox textBoxCadena)
		{
			if (ValidarCadena(textBoxCadena.Text))
			{
				textBoxCadena.BorderBrush = Brushes.Green;
				OcultarToolTip(textBoxCadena);
			}
			else
			{
				textBoxCadena.BorderBrush = Brushes.Red;
				MostrarToolTip(textBoxCadena, "Debe tener un largo de 0 a 255 caractéres");
			}
		}

		public static void MostrarEstadoDeValidacionContraseña(PasswordBox textBoxContraseña)
		{
			if (ValidarContraseña(textBoxContraseña.Password))
			{
				textBoxContraseña.BorderBrush = Brushes.Green;
				OcultarToolTip(textBoxContraseña);
			}
			else
			{
				textBoxContraseña.BorderBrush = Brushes.Red;
				MostrarToolTip(textBoxContraseña, "Debe tener un largo de 6 a 255 caractéres");
			}
		}

		public static void MostrarEstadoDeValidacionNumero(TextBox textBoxNumero)
		{
			if (ValidarNumeroDecimal(textBoxNumero.Text))
			{
				textBoxNumero.BorderBrush = Brushes.Green;
				OcultarToolTip(textBoxNumero);
			}
			else
			{
				textBoxNumero.BorderBrush = Brushes.Red;
				MostrarToolTip(textBoxNumero, "Solo se permiten numeros enteros o decimales");
			}
		}

		public static void MostrarEstadoDeValidacionTelefono(TextBox textBoxTelefono)
		{
			if (ValidarTelefono(textBoxTelefono.Text))
			{
				textBoxTelefono.BorderBrush = Brushes.Green;
				OcultarToolTip(textBoxTelefono);
			}
			else
			{
				textBoxTelefono.BorderBrush = Brushes.Red;
				MostrarToolTip(textBoxTelefono, "El numero de telefono ingresado es invalido");
			}
		}

		public static void MostrarEstadoDeValidacionCorreoElectronico(TextBox textBoxCorreoElectronico)
		{
			if (ValidarCorreoElectronico(textBoxCorreoElectronico.Text))
			{
				textBoxCorreoElectronico.BorderBrush = Brushes.Green;
				OcultarToolTip(textBoxCorreoElectronico);
			}
			else
			{
				textBoxCorreoElectronico.BorderBrush = Brushes.Red;
				MostrarToolTip(textBoxCorreoElectronico, "El correo ingresado es invalido");
			}
		}

		public static void MostrarEstadoDeValidacionNombre(TextBox textBoxNombre)
		{
			if (ValidarNombre(textBoxNombre.Text))
			{
				textBoxNombre.BorderBrush = Brushes.Green;
				OcultarToolTip(textBoxNombre);
			}
			else
			{
				textBoxNombre.BorderBrush = Brushes.Red;
				MostrarToolTip(textBoxNombre, "El nombre ingresado es invalido");
			}
		}

		public static string MostrarVentanaDeSeleccionDeArchivos()
		{

			string direccionDeArchivoSeleccionado = string.Empty;

			SaveFileDialog ventanaDeSeleccionDeArchivo = new SaveFileDialog
			{
				Filter = "DocumentosPDF (*.PDF)|*.PDF",
				InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			};

			if (ventanaDeSeleccionDeArchivo.ShowDialog() == true)
			{
				direccionDeArchivoSeleccionado = ventanaDeSeleccionDeArchivo.FileName;
			}

			return direccionDeArchivoSeleccionado;
		}
	}
}
