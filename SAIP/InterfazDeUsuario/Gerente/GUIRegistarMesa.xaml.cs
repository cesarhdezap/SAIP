using InterfazDeUsuario.UserControls;
using LogicaDeNegocio.Clases;
using LogicaDeNegocio.ObjetosAccesoADatos;
using System;
using System.Windows;
using System.Windows.Controls;

namespace InterfazDeUsuario.Gerente
{
    /// <summary>
    /// Lógica de interacción para GUIRegistarMesa.xaml
    /// </summary>
    public partial class GUIRegistarMesa : Page
    {
        public Empleado Gerente { get; set; }
        public Mesa MesaRegistar { get; set; } 
        ControladorDeCambioDePantalla Controlador;
        public GUIRegistarMesa(ControladorDeCambioDePantalla controlador, Empleado EmpleadoCargado)
        {
            InitializeComponent();
            Gerente = EmpleadoCargado;
            BarraDeEstado.Controlador = controlador;
            Controlador = controlador;
            BarraDeEstado.ActualizarNombreDeUsuario(Gerente.Nombre);
        }

        public void CapturarMesa()
        {
            MesaRegistar = new Mesa();
            String NMesa = TextBoxNumeroMesa.Text;
            MesaRegistar.NumeroDeMesa = int.Parse(NMesa);
            MesaDAO mesaDAO = new MesaDAO();
            mesaDAO.GuardarMesas(MesaRegistar);
            MessageBox.Show("Mesa Registrada con Exito!", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultadoDeMesageBox = MessageBox.Show("Esta a punto de guardar una Mesa nueva dentro del sistema ¿Esta seguro que desea continuar?", "ADVERTENCIA", MessageBoxButton.YesNo, MessageBoxImage.Error);
            CapturarMesa();
        }
    }
}
