using System;
using System.Collections.Generic;
using LogicaDeNegocio.Clases;
using LogicaDeNegocio.ObjetosAccesoADatos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PruebasUnitarias.PruebasDAO
{
    [TestClass]
    public class PruebasIngredienteDAO
    {
        [TestMethod]
        public void GuardarIngrediente_Exitoso() {
            IngredienteDAO ingredienteDAO = new IngredienteDAO();
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

            ingredienteDAO.GuardarIngrediente(ingrediente);
            bool resultado = ingredienteDAO.CargarTodos().Exists(i => i.Id == ingrediente.Id);
            Assert.IsTrue(resultado);

        }
        public void GuardarIngrediente_Fallido()
        {
            IngredienteDAO ingredienteDAO = new IngredienteDAO();
            Ingrediente ingrediente = new Ingrediente()
            {
                Id = 34,
                UnidadDeMedida = LogicaDeNegocio.Enumeradores.UnidadDeMedida.Litro,
                Nombre = " ",
                CantidadEnInventario = 250,
                CodigoDeBarras = "",
                Costo = 34.99,
                FechaDeCreacion = DateTime.Now,
                FechaDeModificacion = DateTime.Now,
                Creador = "gerente",
                Codigo = ".",
                Activo = true,
                Componentes = new List<Componente>()
            };

            ingredienteDAO.GuardarIngrediente(ingrediente);
            bool resultado = ingredienteDAO.CargarTodos().Exists(i => i.Id == ingrediente.Id);
            Assert.IsFalse(resultado);
        }
    }
}
