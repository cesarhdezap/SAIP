using LogicaDeNegocio.Clases;
using LogicaDeNegocio.Enumeradores;
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

namespace InterfazDeUsuario.CallCenter
{
    /// <summary>
    /// Lógica de interacción para GUICambiarEstadoPedidoCallCenter.xaml
    /// </summary>
    public partial class GUICambiarEstadoPedidoCallCenter : Window
    {
        Pedido Pedido;
        public GUICambiarEstadoPedidoCallCenter(Pedido pedido)
        {
            InitializeComponent();
            Pedido = pedido;

            CargarEstados();
        }

        private void CargarEstados()
        {
            ComboBoxEstadoPedido.Items.Add(EstadoPedido.Enviado.ToString());
            ComboBoxEstadoPedido.Items.Add(EstadoPedido.Entregado.ToString());
            ComboBoxEstadoPedido.Items.Add(EstadoPedido.Completado.ToString());
            ComboBoxEstadoPedido.Items.Add(EstadoPedido.Cancelado.ToString());
        }


        private void ButtonAceptar_Click(object sender, RoutedEventArgs e)
        {
            PedidoDAO pedidoDAO = new PedidoDAO();

            string estado = ComboBoxEstadoPedido.SelectedItem.ToString();

            EstadoPedido estadoPedido = (EstadoPedido)Enum.Parse(typeof(EstadoPedido), estado);

            try
            {
                pedidoDAO.CambiarEstadoPedido(Pedido, estadoPedido);
                this.Close();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
