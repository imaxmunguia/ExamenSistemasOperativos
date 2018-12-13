using Examen1.SistemasOperativos.Core;
using Examen1.SistemasOperativos.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen1.SistemasOperativos.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            Planificador planificador = new Planificador();

            planificador.Simular();

            Console.ReadKey();


        }
    }
}
