using System;
using LogicaDeNegocio.Clases;
using LogicaDeNegocio.Enumeradores;
using LogicaDeNegocio.ObjetosAccesoADatos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PruebasUnitarias.PruebasClases
{
    [TestClass]
    public class PruebasMesa
    {
        private readonly int ID_MESA_ESTADO_DISPONIBLE = 4;

        [TestMethod]
        public void ProbarCambiarEstado_DisponibleAOcupado()
        {
            Mesa mesa = new Mesa();
            mesa.NumeroDeMesa = ID_MESA_ESTADO_DISPONIBLE;
            mesa.CambiarEstado(EstadoMesa.Ocupada);

            MesaDAO mesaDAO = new MesaDAO();
            Mesa mesaRecuperada = mesaDAO.ObtenerMesaPorID(ID_MESA_ESTADO_DISPONIBLE);

            Assert.AreEqual(EstadoMesa.Ocupada, mesaRecuperada.Estado);
        }
    }
}
