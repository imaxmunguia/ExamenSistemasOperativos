using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen1.SistemasOperativos.Core.Utils
{
    public static class EstadosProceso
    {
        public enum Estado : int
        {
            Nuevo = 1,
            Listo = 2,
            Ejecutando = 3,
            Esperando = 4,
            Terminado = 5
        }
    }
}
