using LogicaDeNegocio.Clases;
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
        public ObservableCollection<Button> ListaDeBotonesDeMesas { get; set; }
        ControladorDeCambioDePantalla Controlador;

        public GUIVerMisMesas(ControladorDeCambioDePantalla controlador, Empleado empleado)
        {
            Controlador = controlador;
            InitializeComponent();
            ItemsControlMesas.ItemsSource = ListaDeBotonesDeMesas = new ObservableCollection<Button>();

        }

        private void ButtonVolverAMenu_Click(object sender, RoutedEventArgs e)
        {
            Controlador.Regresar();
        }
    }
}
 