using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicaDeNegocio.ObjetosAccesoADatos;

namespace PruebasUnitarias.PruebasDAO
{
	/// <summary>
	/// Summary description for PruebasIngredienteDAO
	/// </summary>
	[TestClass]
	public class PruebasIngredienteDAO
	{
		public PruebasIngredienteDAO()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		#region Additional test attributes
		//
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test 
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

		[TestMethod]
		public void ProbarCargarIngredientesActivos_BaseDeDatosActiva_RegresaListaDeIngredientes()
		{
			IngredienteDAO ingredienteDAO = new IngredienteDAO();
			List<LogicaDeNegocio.Clases.Ingrediente> ingredientes = ingredienteDAO.CargarIngredientesActivos();
			Assert.AreEqual(2, ingredientes.Count);
		}

		[TestMethod]
		public void ProbarCargarIngredientesPorId_IdValida_RegresaIngrediente()
		{
			IngredienteDAO ingredienteDAO = new IngredienteDAO();
			LogicaDeNegocio.Clases.Ingrediente ingrediente = ingredienteDAO.CargarIngredientePorId(1);
			Assert.AreEqual("Tomate", ingrediente.Nombre);
		}
		[TestMethod]
		public void ProbarCargarIngredientesTodos_BaseDeDatosActiva_RegresaListaDeIngredientes()
		{
			IngredienteDAO ingredienteDAO = new IngredienteDAO();
			List<LogicaDeNegocio.Clases.Ingrediente> ingredientes = ingredienteDAO.CargarTodos();
			Assert.AreEqual(3, ingredientes.Count);
		}
		[TestMethod]
		public void ProbarConvertirDeLogicaADatos_IdValida_RegresaIngrediente()
		{
			IngredienteDAO ingredienteDAO = new IngredienteDAO();
			LogicaDeNegocio.Clases.Ingrediente ingrediente = new LogicaDeNegocio.Clases.Ingrediente()
			{
				Id = 1
			};
			LogicaDeNegocio.Clases.Ingrediente ingredienteCargado = ingredienteDAO.CargarIngredientePorId(ingrediente.Id);
			Assert.AreEqual("Tomate", ingredienteCargado.Nombre);
		}

		[TestMethod]
		public void ProbarRecuperarIngredientePorCodigo_CodigoValido_RegresaIngrediente()
		{
			IngredienteDAO ingredienteDAO = new IngredienteDAO();
			LogicaDeNegocio.Clases.Ingrediente ingrediente = new LogicaDeNegocio.Clases.Ingrediente()
			{
				Codigo = "tmt"
			};
			LogicaDeNegocio.Clases.Ingrediente ingredienteCargado = ingredienteDAO.RecuperarIngredientePorCodigo(ingrediente.Codigo);
			Assert.AreEqual("Tomate", ingredienteCargado.Nombre);
		}
    
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
