using AccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.ObjetosAccesoADatos
{
    public class MesaDAO
    {
        public void Guardar(Mesa mesa)
        {

            AccesoADatos.Mesa mesaAGuardar = ConvertirMesaLogicaADatos(mesa);
            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                context.Mesas.Add(mesaAGuardar);
                context.SaveChanges();
            }
        }

        public List<Mesa> RecuperarTodos()
        {
            List<Mesa> mesas;
            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                mesas = context.Mesas.ToList();
            }
            
            return mesas;
        }

        private AccesoADatos.Mesa ConvertirMesaLogicaADatos(Mesa mesa)
        {
            throw new NotImplementedException();
        }

        private Clases.Mesa ConvertirMesaDatosALogica(AccesoADatos.Mesa mesa)
        {
            throw new NotImplementedException();
        }

        private List<Clases.Mesa> ConvertirListaDeMesasDatosALogica(List<AccesoADatos.Mesa> mesas)
        {
            throw new NotImplementedException();
        }
    }
}
