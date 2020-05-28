using AccesoADatos;
using LogicaDeNegocio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LogicaDeNegocio.ObjetosAccesoADatos
{
    public class IngredienteDAO
    {
        public void GuardarIngrediente(Clases.Ingrediente ingrediente)
        {

            ingrediente.FechaDeCreacion = DateTime.Now;
            ingrediente.FechaDeModificacion = DateTime.Now;
            ingrediente.Activo = true;

            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                AccesoADatos.Ingrediente ingredienteDb = ConvertirDeLogicaADbParaGuardar(ingrediente);

                foreach (Clases.Componente componente in ingrediente.Componentes)
                {
                    RelacionIngrediente componenteDB = new RelacionIngrediente();

                    componenteDB.Cantidad = componente.Cantidad;
                    componenteDB.IngredienteHijo = context.Ingredientes.Find(componente.Ingrediente.Id);
                    componenteDB.IngredientePadre = null;
                    ingredienteDb.RelacionIngredientesHijo.Add(componenteDB);

                }
           
                context.Ingredientes.Add(ingredienteDb);

                if (!ValidarExistenciaDeIngrediente(ingredienteDb))
                {
                    context.SaveChanges();
                }
            }
        }
        public AccesoADatos.Ingrediente ConvertirDeLogicaADbParaGuardar(Clases.Ingrediente ingrediente)
        {
            AccesoADatos.Ingrediente ingredienteConvertido = new AccesoADatos.Ingrediente()
            {
                Id = ingrediente.Id,
                UnidadDeMedida = (short)ingrediente.UnidadDeMedida,
                CantidadEnInventario = ingrediente.CantidadEnInventario,
                Nombre = ingrediente.Nombre,
                FechaDeCreacion = ingrediente.FechaDeCreacion,
                FechaDeModiciacion = ingrediente.FechaDeModificacion,
                Codigo = ingrediente.Codigo,
                CodigoDeBarras = ingrediente.CodigoDeBarras,
                NombreCreador = ingrediente.Creador,
                Activo = ingrediente.Activo,
                Costo = ingrediente.Costo,
                RelacionIngredientesHijo = new List<RelacionIngrediente>()

            };

            return ingredienteConvertido;
        }


        public void DarDeBajaIngrediente(Clases.Ingrediente ingrediente)
        {
            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                Ingrediente ingredienteDb = context.Ingredientes.Find(ingrediente.Id);
                if (ingredienteDb != null)
                {
                    ingredienteDb.Activo = false;
                    context.SaveChanges();
                }
                else
                {
                    throw new ArgumentException("La id del ingrediente no encontrada");
                }
            }
        }

        public Clases.Ingrediente ConvertirDeDatosALogica(AccesoADatos.Ingrediente ingredienteDb)
        {
            Clases.Ingrediente ingredienteConvertido = new Clases.Ingrediente()
            {
                Id = ingredienteDb.Id,
                UnidadDeMedida = (UnidadDeMedida)ingredienteDb.UnidadDeMedida,
                CantidadEnInventario = ingredienteDb.CantidadEnInventario,
                Nombre = ingredienteDb.Nombre,
                FechaDeCreacion = ingredienteDb.FechaDeCreacion,
                FechaDeModificacion = ingredienteDb.FechaDeModiciacion,
                Codigo = ingredienteDb.Codigo,
                CodigoDeBarras = ingredienteDb.CodigoDeBarras,
                Creador = ingredienteDb.NombreCreador,
                Activo = ingredienteDb.Activo,
                Costo = ingredienteDb.Costo
            };

            List<Clases.Componente> componentes = new List<Clases.Componente>();

            foreach (RelacionIngrediente ingredienteIngrediente in ingredienteDb.RelacionIngredientesHijo)
            {
                componentes.Add(new Clases.Componente
                {
                    Cantidad = ingredienteIngrediente.Cantidad,
                    Compuesto = ingredienteConvertido,
                    Ingrediente = ConvertirDeDatosALogica(ingredienteIngrediente.IngredienteHijo)
                });
            }
            ComponenteDAO componenteDAO = new ComponenteDAO();
            ingredienteConvertido.Componentes = componenteDAO.ObtenerComponentesPorIdDeIngredienteCompuesto(ingredienteDb.Id);
            return ingredienteConvertido;
        }

        public AccesoADatos.Ingrediente ConvertirDeLogicaADb(Clases.Ingrediente ingrediente)
        {
            AccesoADatos.Ingrediente ingredienteConvertido;

            if (ingrediente.Id > 0)
            {
                //no es ingrediente nuevo
                using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
                {
                    ingredienteConvertido = context.Ingredientes.Find(ingrediente.Id);
                }
            }
            else
            {
                ingredienteConvertido = new AccesoADatos.Ingrediente()
                {
                    Id = ingrediente.Id,
                    UnidadDeMedida = (short)ingrediente.UnidadDeMedida,
                    CantidadEnInventario = ingrediente.CantidadEnInventario,
                    Nombre = ingrediente.Nombre,
                    FechaDeCreacion = ingrediente.FechaDeCreacion,
                    FechaDeModiciacion = ingrediente.FechaDeModificacion,
                    Codigo = ingrediente.Codigo,
                    CodigoDeBarras = ingrediente.CodigoDeBarras,
                    NombreCreador = ingrediente.Creador,
                    Activo = ingrediente.Activo,
                    Costo = ingrediente.Costo

                };
            }

            ComponenteDAO componenteDAO = new ComponenteDAO();
            if (ingrediente.Componentes.Count > 0)
            {
                ingredienteConvertido.RelacionIngredientesHijo = componenteDAO.ConvertirlistaDeLogicaADatos(ingrediente.Componentes);
            }
            return ingredienteConvertido;
        }


        private List<Clases.Ingrediente> ConvertirListaDeDbAListaDeLogica(List<Ingrediente> IngredientesDeDb)
        {
            List<Clases.Ingrediente> ingredientesResultado = new List<Clases.Ingrediente>();

            foreach (Ingrediente ingrediente in IngredientesDeDb)
            {
                ingredientesResultado.Add(ConvertirDeDatosALogica(ingrediente));
            }

            return ingredientesResultado;
        }

        public List<Ingrediente> ConvertirListaDeIngredientesDeLogicaAListaDeIngredientesDeAccesoADatos(List<Clases.Ingrediente> Ingredientes)
        {
            List<Ingrediente> ingredientesResultado = new List<Ingrediente>();

            foreach (Clases.Ingrediente ingrediente in Ingredientes)
            {
                ingredientesResultado.Add(ConvertirDeLogicaADb(ingrediente));
            }

            return ingredientesResultado;
        }

        public List<Clases.Ingrediente> CargarTodos()
        {
            List<Ingrediente> ingredientesDb = new List<Ingrediente>();
            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                ingredientesDb = context.Ingredientes.Include(i => i.RelacionIngredientesHijo).ToList();
            }

            List<Clases.Ingrediente> ingredientesResultado = new List<Clases.Ingrediente>();
            ingredientesResultado = ConvertirListaDeDbAListaDeLogica(ingredientesDb);
            return ingredientesResultado;
        }

        public List<Clases.Ingrediente> CargarIngredientesActivos()
        {
            List<Ingrediente> ingredientesDb = new List<Ingrediente>();
            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                ingredientesDb = context.Ingredientes.Include(i => i.RelacionIngredientesHijo).Where(ingredienteCargado => ingredienteCargado.Activo == true).ToList();
            }

            List<Clases.Ingrediente> ingredientesResultado = new List<Clases.Ingrediente>();
            ingredientesResultado = ConvertirListaDeDbAListaDeLogica(ingredientesDb);
            return ingredientesResultado;
        }

        public Clases.Ingrediente CargarIngredientePorId(int Id)
        {
            Ingrediente ingredienteDb = new Ingrediente();
            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                ingredienteDb = context.Ingredientes.Include(i => i.RelacionIngredientesHijo).FirstOrDefault(i => i.Id == Id);

            }
            Clases.Ingrediente ingredienteResultado = ConvertirDeDatosALogica(ingredienteDb);

            return ingredienteResultado;
        }

        public void ActualizarIngrediente(Clases.Ingrediente ingrediente)
        {
            Ingrediente ingredienteDb;
            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                ingredienteDb = context.Ingredientes.Find(ingrediente.Id);
                ingredienteDb.FechaDeModiciacion = DateTime.Now;
                ingredienteDb.Nombre = ingrediente.Nombre;
                ingredienteDb.CantidadEnInventario = ingrediente.CantidadEnInventario;
                ingredienteDb.UnidadDeMedida = (short)ingrediente.UnidadDeMedida;
                ingredienteDb.Codigo = ingrediente.Codigo;
                ingredienteDb.CodigoDeBarras = ingrediente.CodigoDeBarras;
                ingredienteDb.Costo = ingrediente.Costo;
                ingredienteDb.Activo = ingrediente.Activo;
                context.SaveChanges();
            }
        }

        public Clases.Ingrediente RecuperarIngredientePorCodigo(string codigo)
        {
            Ingrediente ingredienteDb = new Ingrediente();

            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                ingredienteDb = context.Ingredientes.Include(i => i.RelacionIngredientesHijo).FirstOrDefault(i => i.Codigo == codigo);
            }
            Clases.Ingrediente ingredienteResultado = ConvertirDeDatosALogica(ingredienteDb);

            return ingredienteResultado;
        }

        public bool ValidarCodigoExistente(string codigo)
        {
            bool codigoRepetido = false;

            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                if (context.Ingredientes.ToList().Exists(i => i.Codigo == codigo) || context.Ingredientes.ToList().Exists(i => i.CodigoDeBarras == codigo))

                    codigoRepetido = true;
            }

            return codigoRepetido;
        }
        private bool ValidarExistenciaDeIngrediente(Ingrediente ingredienteDb)
        {
            bool resultado = false;

            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                if (context.Ingredientes.ToList().Exists(i => i.Codigo == ingredienteDb.Codigo) && context.Ingredientes.ToList().Exists(i => i.Codigo == ingredienteDb.CodigoDeBarras))
                {
                    resultado = true;
                }
            }
            return resultado;
        }


        public List<Clases.Ingrediente> ObtenerListaDeIngredientesPorIdDePlatillo(int Id)
        {
            List<RelacionIngrediente> componentes = new List<RelacionIngrediente>();
            List<Clases.Ingrediente> ingredientesResultado = new List<Clases.Ingrediente>();
            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                componentes = context.RelacionIngredientes.ToList().TakeWhile(ingrediente => ingrediente.IngredientePadre.Id == Id).ToList();

                foreach (RelacionIngrediente ingrediente in componentes)
                {
                    ingredientesResultado.Add(CargarIngredientePorId(ingrediente.IngredienteHijo.Id));
                }
            }

            return ingredientesResultado;
        }


    }
}
