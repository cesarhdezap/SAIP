using System;
using System.Collections.Generic;
using LogicaDeNegocio;
using LogicaDeNegocio.Clases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PruebasUnitarias.PruebasClases
{
    [TestClass]
    public class PruebasIngrediente
    {
        [TestMethod]
        public void ProbarValidarParaGuardar_Exitoso()
        {
             Ingrediente ingrediente = new Ingrediente()
            {
                Id = 34,
                UnidadDeMedida = LogicaDeNegocio.Enumeradores.UnidadDeMedida.Litro,
                Nombre = "Aceite",
                CantidadEnInventario = 250,
                CodigoDeBarras = "7827348",
                Costo = 34.99,
                FechaDeCreacion = DateTime.Now,
                FechaDeModificacion = DateTime.Now,
                Creador = "gerente",
                Codigo = "ACEITE",
                Activo = true,
                Componentes = new List<Componente>()
            };

            bool resultado = ingrediente.ValidarParaGuardar();

            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void ProbarValidarParaGuardar_EsFalso()
        {
            Ingrediente ingrediente = new Ingrediente()
            {
                Id = 34,
                UnidadDeMedida = LogicaDeNegocio.Enumeradores.UnidadDeMedida.Litro,
                Nombre = "      ",
                CantidadEnInventario = 0,
                CodigoDeBarras = "7827348",
                Costo = 34.99,
                FechaDeCreacion = DateTime.Now,
                FechaDeModificacion = DateTime.Now,
                Creador = "gerente",
                Codigo = "      ",
                Activo = true,
                Componentes = new List<Componente>()
            };

            bool resultado = ingrediente.ValidarParaGuardar();

            Assert.IsFalse(resultado);
        }
    }
}
