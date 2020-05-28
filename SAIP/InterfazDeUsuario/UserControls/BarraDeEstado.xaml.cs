﻿using LogicaDeNegocio.Clases;
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
using System.Windows.Threading;

namespace InterfazDeUsuario.UserControls
{
	/// <summary>
	/// Interaction logic for BarraDeEstado.xaml
	/// </summary>
	public partial class BarraDeEstado : UserControl
	{
		public ControladorDeCambioDePantalla Controlador;

		public BarraDeEstado()
		{
			InitializeComponent();
			DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
			{
				HoraLabel.Content = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
			}, Dispatcher);
			HoraLabel.Content = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
		}
		

		public void ActualizarNombreDeUsuario(String nombreDeUsuario)
		{
			NombreDeUsuarioLabel.Content = nombreDeUsuario;
		}

		public void OcultarNombreDeUsuarioYBotones()
		{
			NombreDeUsuarioLabel.Visibility = Visibility.Hidden;
			CambiarUsuarioButton.IsEnabled = false;
			RegresarButton.IsEnabled = false;
			CambiarUsuarioButton.Visibility = Visibility.Hidden;
			RegresarButton.Visibility = Visibility.Hidden;
		}

		private void RegresarButton_Click(object sender, RoutedEventArgs e)
		{
			Controlador.Regresar();
		}

		private void CambiarUsuarioButton_Click(object sender, RoutedEventArgs e)
		{
			Controlador.RegresarAInicioDeSesion();
		}
	}
}
