using InterfazDeUsuario.UserControls;
using LogicaDeNegocio.Clases;
using LogicaDeNegocio.Clases.ClasesAsociativas;
using LogicaDeNegocio.ObjetosAccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
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
    /// Lógica de interacción para GUIVerRecetas.xaml
    /// </summary>
    public partial class GUIVerRecetas : Page
    {
        public Empleado Empleado { get; set; }
        ControladorDeCambioDePantalla Controlador;
        private List<Pedido> ListaAlimentos {get; set;}
        private Pedido Pedido { get; set; }
        public GUIVerRecetas(ControladorDeCambioDePantalla controlador, Empleado empleado, Pedido pedido)
        {
            Controlador = controlador;
            Empleado = empleado;
            InitializeComponent();
            BarraDeEstado.Controlador = controlador;
            Controlador = controlador;
            BarraDeEstado.ActualizarEmpleado(empleado);
            Pedido = pedido;
            CargarAlimentosDePedidos();
            DataGridAlimentos.ItemsSource = Pedido.CantidadAlimentos;
        }


        private void CargarAlimentosDePedidos()
        {

            CantidadPlatilloDAO cantidadPlatilloDAO = new CantidadPlatilloDAO();
            CantidadProductoDAO cantidadProductoDAO = new CantidadProductoDAO();
            Pedido.CantidadAlimentos = new List<CantidadAlimento>();
            Pedido.CantidadAlimentos = Pedido.CantidadAlimentos.Concat(cantidadPlatilloDAO.RecuperarPorIDPedido(Pedido.Id)).ToList();
            Pedido.CantidadAlimentos = Pedido.CantidadAlimentos.Concat(cantidadProductoDAO.RecuperarPorIDPedido(Pedido.Id)).ToList();
            
        }
    }
}