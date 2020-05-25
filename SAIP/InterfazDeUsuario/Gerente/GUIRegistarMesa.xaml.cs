﻿using InterfazDeUsuario.UserControls;
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
            String NMesa = TextBoxNumeroMesa.Text;
            MesaRegistar.NumeroDeMesa = int.Parse(NMesa);
            MesaDAO mesaDAO = new MesaDAO();
            mesaDAO.Guardar(MesaRegistar);
            MessageBox.Show("Mesa Registrada con Exito!", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultadoDeMesageBox = MessageBox.Show("Esta a punto de guardar un Empleado nuevo dentro del sistema ¿Esta seguro que desea continuar?", "ADVERTENCIA", MessageBoxButton.YesNo, MessageBoxImage.Error);
            CapturarMesa();
        }
    }
}
