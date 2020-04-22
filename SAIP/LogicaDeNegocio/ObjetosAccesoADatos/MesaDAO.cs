using AccesoADatos;
using LogicaDeNegocio.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.ObjetosAccesoADatos
{
    public class MesaDAO
    {
        public void Guardar(Clases.Mesa mesa)
        {

            AccesoADatos.Mesa mesaAGuardar = ConvertirMesaLogicaADatos(mesa);
            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                context.Mesas.Add(mesaAGuardar);
                context.SaveChanges();
            }
        }

        public Clases.Mesa ObtenerMesaPorID(int idMesa)
        {
            AccesoADatos.Mesa mesa;
            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                 mesa = context.Mesas.Find(idMesa);
            }
            Clases.Mesa mesaLogica = new Clases.Mesa();
            if (mesa != null)
            {
                mesaLogica = ConvertirMesaDatosALogica(mesa);
            }

            return mesaLogica;
        }

        public void CambiarEstadoPorID(int idMesa, EstadoMesa nuevoEstado)
        {
            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                AccesoADatos.Mesa mesa = context.Mesas.Find(idMesa);
                if (mesa != null)
                {
                    mesa.Estado = (short) nuevoEstado;
                }
                context.SaveChanges();
            }
        }

        public List<Clases.Mesa> RecuperarTodos()
        {
            List<AccesoADatos.Mesa> mesas;
            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                mesas = context.Mesas.ToList();
            }
            
            return ConvertirListaDeMesasDatosALogica(mesas);
        }

        private AccesoADatos.Mesa ConvertirMesaLogicaADatos(Clases.Mesa mesa)
        {
            AccesoADatos.Mesa mesaDatos = new AccesoADatos.Mesa
            {
                Id = mesa.NumeroDeMesa,
                Estado = (short)mesa.Estado
            };

            return mesaDatos;
        }

        private Clases.Mesa ConvertirMesaDatosALogica(AccesoADatos.Mesa mesa)
        {
            Clases.Mesa mesaLogica = new Clases.Mesa()
            {
                Estado = (EstadoMesa)mesa.Estado,
                NumeroDeMesa = mesa.Id
            };
            return mesaLogica;
        }

        private List<Clases.Mesa> ConvertirListaDeMesasDatosALogica(List<AccesoADatos.Mesa> mesas)
        {
            List<Clases.Mesa> mesasLogica = new List<Clases.Mesa>();
            foreach(AccesoADatos.Mesa mesa in mesas)
            {
                mesasLogica.Add(ConvertirMesaDatosALogica(mesa));
            }
            return mesasLogica;
        }
    }
}
