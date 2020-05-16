using LogicaDeNegocio.Enumeradores;
using LogicaDeNegocio.ObjetosAccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.Clases
{
    public class Mesa
    {
        public EstadoMesa Estado;
        public int NumeroDeMesa;

        public void CambiarEstado(EstadoMesa estado)
        {
            if (NumeroDeMesa >= 0)
            {
                MesaDAO mesaDAO = new MesaDAO();
                mesaDAO.CambiarEstadoPorID(NumeroDeMesa, estado);
            }

        }

        public override string ToString()
        {

            return NumeroDeMesa.ToString() + " Estado: " + Estado.ToString();
        }
    }
    
}
