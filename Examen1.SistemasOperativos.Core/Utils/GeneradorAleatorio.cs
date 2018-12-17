using Examen1.SistemasOperativos.Core.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen1.SistemasOperativos.Core.Utils
{
    public static class GeneradorAleatorio
    {
        public static int GenerarEntero(int valorMinimo, int valorMaximo)
        {
            var guid = Guid.NewGuid();
            var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
            var seed = int.Parse(justNumbers.Substring(0, 4));
            return new Random(seed).Next(valorMinimo, valorMaximo+1);
        }
        public static bool GenerarValorLogico()
        {
            return GenerarEntero(0, 1).Equals(1);
        }

        public static int GenerarEnteroMultiplo2(int valorMinimo, int valorMaximo)
        {
            var guid = Guid.NewGuid();
            var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
            var seed = int.Parse(justNumbers.Substring(0, 4));
            var value = new Random(seed).Next(valorMinimo, valorMaximo + 1);
            return value * SingletonSimuladorConfigurationFactory.Instance.TamanioMarcoPagina;
        }
    }
}
