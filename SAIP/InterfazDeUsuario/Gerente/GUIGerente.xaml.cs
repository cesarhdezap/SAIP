﻿using LogicaDeNegocio.Clases;
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

namespace InterfazDeUsuario.Gerente
{
	/// <summary>
	/// Interaction logic for GUIGerente.xaml
	/// </summary>
	public partial class GUIGerente : Page
	{
		public Empleado Gerente { get; set; }
		ControladorDeCambioDePantalla Controlador;
		public GUIGerente(ControladorDeCambioDePantalla controlador, Empleado empleadoCargado)
		{
			InitializeComponent();
			Gerente = empleadoCargado;
			BarraDeEstado.Controlador = controlador;
			Controlador = controlador;
			BarraDeEstado.ActualizarNombreDeUsuario(Gerente.Nombre);
		}

		private void RegistrarPlatilloButton_Click(object sender, RoutedEventArgs e)
		{
			GUIRegistrarPlatillo registrarPlatillo = new GUIRegistrarPlatillo(Gerente);
			
			registrarPlatillo.ShowDialog();
			
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			PlatilloDAO platilloDAO = new PlatilloDAO();
			GUIEditarPlatillo editarPlatillo = new GUIEditarPlatillo(Gerente, platilloDAO.CargarPlatilloPorId(1));
			
			editarPlatillo.ShowDialog();
			
		}

		private void Button_Click_Lista(object sender, RoutedEventArgs e)
		{
			GUIVerEmpleados verEmpleados = new GUIVerEmpleados(Controlador, Gerente);
			Controlador.CambiarANuevaPage(verEmpleados);
			
			
		}
	}
}
