using LogicaDeNegocio.Clases;
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

namespace InterfazDeUsuario.UserControls
{
    /// <summary>
    /// Interaction logic for InformacionDeCuenta.xaml
    /// </summary>
    public partial class InformacionDeCuenta : UserControl
    {
        public InformacionDeCuenta()
        {
            InitializeComponent();

        }

        public void ActualizarCuenta(Cuenta cuenta)
        {
            LabelMesa.Content = cuenta.Mesa.ToString();
            DataGridCuenta.ItemsSource = cuenta.Pedidos;
        }
    }
}
