using InterfazDeUsuario.Mesero;
using InterfazDeUsuario.Paginas;
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
            if (!Pantallas.Any(x => x.GetType() == page.GetType()))
            {
                Pantallas.Push(page);
                Content = page;
                page.Width = double.NaN;
                page.Height = double.NaN;
            }
            else
            {
                Page pageExistente = Pantallas.FirstOrDefault(x => x.GetType() == page.GetType());
                Remover(pageExistente);
                Pantallas.Push(pageExistente);
                Content = pageExistente;
            }
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            SizeToContent = SizeToContent.WidthAndHeight;
        }

        private void Remover(Page page)
        {
            if (Pantallas.Peek() == page)
            {
                Pantallas.Pop();
            }
            else
            {
                Page almacenamiento = Pantallas.Peek();
                Pantallas.Pop();
                Remover(page);
                Pantallas.Push(almacenamiento);
            }
        }

        public void Regresar()
        {
            if (Pantallas.Any())
            {
                Pantallas.Pop();
            }
            
            if (Pantallas.Any())
            {
                Content = Pantallas.Peek();
            }
            else
            {
                RegresarAInicioDeSesion();
            }
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            SizeToContent = SizeToContent.WidthAndHeight;
        }

        public void RegresarAInicioDeSesion()
        {
            PageInicioDeSesion inicioDeSesion = new PageInicioDeSesion(this);
            Pantallas.Clear();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            SizeToContent = SizeToContent.WidthAndHeight;
            Content = inicioDeSesion;
            inicioDeSesion.Width = double.NaN;
            inicioDeSesion.Height = double.NaN; 
        }
    }
}
