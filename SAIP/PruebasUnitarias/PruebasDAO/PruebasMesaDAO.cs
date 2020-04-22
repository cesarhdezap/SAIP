using System;
using System.Collections.Generic;
using LogicaDeNegocio.Clases;
using LogicaDeNegocio.ObjetosAccesoADatos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PruebasUnitarias.PruebasDAO
{
    [TestClass]
    public class PruebasMesaDAO
    {
        private readonly int NUMERO_DE_MESAS_EN_DB = 4;

        [TestMethod]
        public void ProbarRecuperarTodos()
        {
            MesaDAO mesaDAO = new MesaDAO();
            List<Mesa> mesas = mesaDAO.RecuperarTodos();
            Assert.AreEqual(NUMERO_DE_MESAS_EN_DB, mesas.Count);
        }
    }
}
