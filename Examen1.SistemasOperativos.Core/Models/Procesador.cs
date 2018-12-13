using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen1.SistemasOperativos.Core.Models
{
    public class Procesador
    {
        public int Id { get; set; }
        public int Nucleos { get; set; }
        public string Proceso { get; set; }
        public override string ToString()
        {
            return $"Id: {Id}, Nucleos: {Nucleos}, Proceso: {Proceso}";
        }

    }
}
