using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocio.Enumeradores
{
    public enum EstadoPedido
    {
        Registrado = 1,
        EnEspera = 2,
        Realizado = 3,
        Entregado = 4,
        Completado = 5,
        Cancelado = 6,
        Enviado = 7
    }
}
