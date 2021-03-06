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
        public List<CantidadPlatillo> RecuperarPorIDPedido(int idPedido)
        {
            List<AccesoADatos.PlatilloPedido> platilloPedido = new List<AccesoADatos.PlatilloPedido>();

            using (ModeloDeDatosContainer context = new ModeloDeDatosContainer())
            {
                platilloPedido = context.PlatilloPedido.Where(p => p.Pedido.Id == idPedido)
                    .Include(p => p.Platillo)
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
                    Alimento = platilloDAO.ConvertirDatosALogica(platilloPedido.Platillo) 
                };
                cantidadPlatillos.Add(cantidadPlatillo);
            }
            
            return cantidadPlatillos;
        }
    }
}
