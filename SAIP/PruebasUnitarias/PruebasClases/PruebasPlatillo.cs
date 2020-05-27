using System;
using LogicaDeNegocio.Clases;
using LogicaDeNegocio.ObjetosAccesoADatos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PruebasUnitarias.PruebasClases
{
    [TestClass]
    public class PruebasPlatillo
    {
        [DataTestMethod]
        [DataRow(3, 4)]
        public void ProbarValidarCantidadAlimento_IngredienteSinComponentes_RegresaFalse(int cantidadEnInventario, int cantidadMayor)
        {
            PlatilloDAO platilloDao = new PlatilloDAO();
            Platillo platillo = new Platillo
            {
                Nombre = Guid.NewGuid().ToString()
            };

            platilloDao.GuardarPlatillo(platillo);
            bool resultadoValidacion = platillo.ValidarCantidadAlimento(cantidadMayor);
            platilloDao.Depuracion_Eliminar(platillo.Nombre);
            throw new NotImplementedException();
            Assert.IsFalse(resultadoValidacion);
        }
    }
}
