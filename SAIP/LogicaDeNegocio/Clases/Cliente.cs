using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LogicaDeNegocio.Servicios.ServiciosDeValidacion;

namespace LogicaDeNegocio.Clases
{
    public class Cliente
    {
        public int Id { get; set;}
        public string Nombre { get; set;}
        public string Telefono { get; set;}
        public List<string> Direcciones { get; set;}
        public string Comentario { get; set;}
        public string Creador { get; set; }
        public List<Cuenta> Cuenta { get; set;}

        public Cliente()
        {
            Direcciones = new List<string>();
        }

        public bool Validar()
        {
            bool resultado = false;
            if (ValidarNombre(Nombre)
                && ValidarTelefono(Telefono)
                && Direcciones.Count >0
                && ValidarCadenaVacioPermitido(Comentario))
            {
                resultado = true;
            }
            return resultado;
        }
    }
}
