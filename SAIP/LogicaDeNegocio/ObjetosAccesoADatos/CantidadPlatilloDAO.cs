using AccesoADatos;
using LogicaDeNegocio.Clases.ClasesAsociativas;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.ObjetosAccesoADatos
{
    public class CantidadPlatilloDAO
    {
        public List<CantidadPlatillo> RecuperarCantidadPlatilloPorIDPedido(int idPedido)
        {
            List<AccesoADatos.PlatilloPedido> platilloPedido = new List<AccesoADatos.PlatilloPedido>();

            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                platilloPedido = context.PlatilloPedido.Where(p => p.Pedido.Id == idPedido)
                    .Include(p => p.Alimento)
                    .ToList();
            }
            return ConvertirListaDatosALogica(platilloPedido);
        }

        public List<CantidadPlatillo> ConvertirListaDatosALogica(List<AccesoADatos.PlatilloPedido> platilloPedidos)
        {
            List<CantidadPlatillo> cantidadPlatillos = new List<CantidadPlatillo>();

            foreach(PlatilloPedido platilloPedido  in platilloPedidos)
            {
                PlatilloDAO platilloDAO = new PlatilloDAO();
                CantidadPlatillo cantidadPlatillo = new CantidadPlatillo
                {
                    Cantidad = platilloPedido.Cantidad,
                    Alimento = platilloDAO.ConvertirPlatilloDeAccesoADatosAPlatilloDeLogica(platilloPedido.Alimento)
                };
                cantidadPlatillos.Add(cantidadPlatillo);
            }
            
            return cantidadPlatillos;
        }
    }
}
