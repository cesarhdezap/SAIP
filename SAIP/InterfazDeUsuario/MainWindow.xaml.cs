using InterfazDeUsuario.Mesero;
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

namespace InterfazDeUsuario
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ControladorDeCambioDePantalla
    {
        private Stack<Page> Pantallas = new Stack<Page>();

        public MainWindow()
        {
            InitializeComponent();
            RegresarAInicioDeSesion();
        }

        public void CambiarANuevaPage(Page page)
        {
            Pantallas.Push(page);
            Content = page;
        }

        public void Regresar()
        {
            if (Pantallas.Count > 0)
            {
                Pantallas.Pop();
                Content = Pantallas.Peek();
            }
        }

        public void RegresarAInicioDeSesion()
        {
            PageInicioDeSesion inicioDeSesion = new PageInicioDeSesion(this);
            Pantallas.Clear();
            Content = inicioDeSesion;
        }
    }
}
