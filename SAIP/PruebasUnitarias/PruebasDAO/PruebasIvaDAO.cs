using System;
using System.Linq;
using LogicaDeNegocio.Clases;
using LogicaDeNegocio.ObjetosAccesoADatos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PruebasUnitarias.PruebasDAO
{
    [TestClass]
    public class PruebasIvaDAO
    {
        private const int NUMERO_DE_IVAS_EN_BASE = 1;
        private const double VALOR_IVA_ACTUAL_EN_BASE = 0.16;
        private const double VALOR_IVA_A_GUARDAR_CORRECTO = 0.181818;

        [TestMethod]
        public void ProbarCargarTodos_CargaCorrecto_RegresaListaDeIvas()
        {
            IvaDAO ivaDAO = new IvaDAO();
            int numeroDeIvasActual = ivaDAO.CargarTodos().Count();
            Assert.AreEqual(NUMERO_DE_IVAS_EN_BASE, numeroDeIvasActual);
        }

        [TestMethod]
        public void ProbarCargarIvaActual_CargarCorrecto_RegresaIva()
        {
            IvaDAO ivaDAO = new IvaDAO();
            Iva iva = ivaDAO.CargarIvaActual();
            Assert.AreEqual(VALOR_IVA_ACTUAL_EN_BASE, iva.Valor);
        }

        [TestMethod]
        public void ProbarGuardar_IvaActivoCorrecto_RegresaVoid()
        {
            Iva iva = new Iva
            {
                Activo = true,
                Creador = "Test",
                FechaDeCreacion = DateTime.Now,
                FechaDeInicio = DateTime.Now,
                Valor = VALOR_IVA_A_GUARDAR_CORRECTO
            };

            IvaDAO ivaDAO = new IvaDAO();
            ivaDAO.Guardar(iva);

            
            bool resultado = ivaDAO.CargarTodos().Exists(i => i.Valor == VALOR_IVA_A_GUARDAR_CORRECTO && i.Activo == true);
            ivaDAO.Depuracion_Eliminar(VALOR_IVA_A_GUARDAR_CORRECTO);
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void ProbarGuardar_IvaInactivoCorrecto_RegresaVoid()
        {
            Iva iva = new Iva
            {
                Activo = false,
                Creador = "Test",
                FechaDeCreacion = DateTime.Now,
                FechaDeInicio = DateTime.Now,
                Valor = VALOR_IVA_A_GUARDAR_CORRECTO
            };

            IvaDAO ivaDAO = new IvaDAO();
            ivaDAO.Guardar(iva);


            bool resultado = ivaDAO.CargarTodos().Exists(i => i.Valor == VALOR_IVA_A_GUARDAR_CORRECTO && i.Activo == false);
            ivaDAO.Depuracion_Eliminar(VALOR_IVA_A_GUARDAR_CORRECTO);
            Assert.IsTrue(resultado);
        }
    }
}
