﻿using InterfazDeUsuario.UserControls;
using LogicaDeNegocio;
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

namespace InterfazDeUsuario.Cocinero
{
    /// <summary>
    /// Lógica de interacción para GUI_VerPedidosPendientes.xaml
    /// </summary>
    public partial class GUI_VerPedidosPendientes : Page
    {
        private List<Pedido> PedidosRegistrados { get; set; }
        private List<Pedido> PedidosEnProceso { get; set; }
        public Empleado Empleado { get; set; }
        ControladorDeCambioDePantalla Controlador;
        public GUI_VerPedidosPendientes(ControladorDeCambioDePantalla controlador, Empleado empleado)
        {
            Controlador = controlador;
            Empleado = empleado;
            InitializeComponent();
            BarraDeEstado.Controlador = controlador;
            Controlador = controlador;
            BarraDeEstado.ActualizarEmpleado(empleado);
            MostrarPedidosRegistrados();
            MostrarPedidosEnProceso();
        }

        public void MostrarPedidosRegistrados()
        {

            PedidoDAO pedidoDAO = new PedidoDAO();
            PedidosRegistrados = pedidoDAO.CargarRecientes();
            DataGridPedidosRegistrados.ItemsSource = null;
            DataGridPedidosRegistrados.ItemsSource = PedidosRegistrados;
            ActualizarPantalla();
        }

        public void MostrarPedidosEnProceso()
        {
            PedidoDAO pedidoDAO = new PedidoDAO();
            PedidosEnProceso = pedidoDAO.CargarEnProceso();
            DataGridPedidosEnProceso.ItemsSource = null;
            DataGridPedidosRegistrados.ItemsSource = PedidosEnProceso;
            ActualizarPantalla();
        }

        public void ActualizarPantalla()
        {
            DataGridPedidosEnProceso.ItemsSource = null;
            DataGridPedidosEnProceso.ItemsSource = PedidosEnProceso;
            DataGridPedidosRegistrados.ItemsSource = null;
            DataGridPedidosRegistrados.ItemsSource = PedidosRegistrados;
        }

        private void BotonVerReceta_Click(object sender, RoutedEventArgs e)
        {
            Pedido pedido = ((FrameworkElement)sender).DataContext as Pedido;
            GUIVerRecetas gUIVerRecetas = new GUIVerRecetas(Controlador, Empleado);
                Controlador.CambiarANuevaPage(gUIVerRecetas);
            
        }

        private void BotonRealizado_Click(object sender, RoutedEventArgs e, PedidoDAO pedido)
        {
           
                MessageBox.Show("No se a seleccionado un Pedido para Completar", "Seleccionar Pedido", MessageBoxButton.OK, MessageBoxImage.Error);
            
        }

        private void ButtonEnProceso_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}