using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InterfazDeUsuario
{
    public interface ControladorDeCambioDePantalla
    {
        void RegresarAInicioDeSesion();

        void CambiarANuevaPage(Page page);

        void Regresar();
    }
}
