﻿using LogicaDeNegocio;
using LogicaDeNegocio.Clases;
using LogicaDeNegocio.ObjetosAccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace InterfazDeUsuario.CallCenter
{
    /// <summary>
    /// Lógica de interacción para GUIPrincipalCallCenter.xaml
    /// </summary>
    public partial class GUIPrincipalCallCenter : Page
    {
        Empleado EmpleadoCallCenter;
        ControladorDeCambioDePantalla Controlador;
        List<Pedido> Pedidos = new List<Pedido>();
        private bool candado;
        private Timer Timer;

        public GUIPrincipalCallCenter(ControladorDeCambioDePantalla controlador, Empleado empleadoDeCallCenter)
        {
            InitializeComponent();
            EmpleadoCallCenter = empleadoDeCallCenter;
            Controlador = controlador;
            BarraDeEstado.Controlador = controlador;
            BarraDeEstado.ActualizarEmpleado(empleadoDeCallCenter);
            MostrarPedidos();
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(3);
            candado = true;
            Timer = new System.Threading.Timer((e) =>
            {
                
                Dispatcher.Invoke(() =>
                {
                    if (candado)
                    {
                        candado = false;
                        MostrarPedidos();
                        candado = true;
                    }
                });

            }, null, startTimeSpan, periodTimeSpan);
        }

        private void MostrarPedidos()
        {
            PedidoDAO pedidoDAO = new PedidoDAO();
            Pedidos = pedidoDAO.CargarRecientes();
            ActualizarPantalla();
        }

        private void ActualizarPantalla()
        {
            DataGridPedidos.ItemsSource = null;
            DataGridPedidos.ItemsSource = Pedidos;
        }

        private void ButtonCambiarEstado_Click(object sender, RoutedEventArgs e)
        {
            Pedido pedido;
            try
            {
                pedido = (Pedido)DataGridPedidos.SelectedItem;
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("Seleccione un pedido válido");
                pedido = null;
            }

            if(pedido != null)
            {
                GUICambiarEstadoPedidoCallCenter cambiarEstadoPedidoCallCenter = new GUICambiarEstadoPedidoCallCenter(pedido);

                cambiarEstadoPedidoCallCenter.ShowDialog();

                MostrarPedidos();
            }
            else
            {
                MessageBox.Show("Porfavor seleccione un pedido", "Aviso");
            }
        }

        private void ButtonEditar_Click(object sender, RoutedEventArgs e)
        {
            Pedido pedido;
            try
            {
                pedido = (Pedido)DataGridPedidos.SelectedItem;
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("Seleccione un pedido válido");
                pedido = null;
            }

            if (pedido == null)
            {
                throw new NotImplementedException();
            }
            else
            {
                MessageBox.Show("Porfavor seleccione un pedido", "Aviso");
            }
        }

        private void ButtonRegistrarPedido_Click(object sender, RoutedEventArgs e)
        {
            GUIPedidoADomicilio pedidoADomicilio = new GUIPedidoADomicilio(Controlador, EmpleadoCallCenter);
            Controlador.CambiarANuevaPage(pedidoADomicilio);
        }
    }
}
