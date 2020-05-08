using InterfazDeUsuario.Paginas;
using InterfazDeUsuario.UserControls;
using LogicaDeNegocio.Clases;
using LogicaDeNegocio.ObjetosAccesoADatos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace InterfazDeUsuario.Mesero
{
    /// <summary>
    /// Interaction logic for GUIVerMisMesas.xaml
    /// </summary>
    public partial class GUIVerMisMesas : Page
    {
        private ObservableCollection<Button> ListaDeBotonesDeMesas { get; set; }
        private List<Mesa> Mesas = new List<Mesa>();
        ControladorDeCambioDePantalla Controlador;
        

        public GUIVerMisMesas(ControladorDeCambioDePantalla controlador, Empleado empleado)
        {
            Controlador = controlador;
            InitializeComponent();
            BarraDeEstado.Controlador = controlador;
            ItemsControlMesas.ItemsSource = ListaDeBotonesDeMesas = new ObservableCollection<Button>();
            
        }

        private void MostrarMesas()
        {
            MesaDAO mesaDAO = new MesaDAO();
            Mesas = mesaDAO.RecuperarTodos();
            foreach(Mesa mesa in Mesas)
            {
                Button button = new Button();
                button.Content = "";
                //ListaDeBotonesDeMesas.Add();
            }
            
        }

        private void ButtonVolverAMenu_Click(object sender, RoutedEventArgs e)
        {
            Controlador.Regresar();
        }
    }
}
 