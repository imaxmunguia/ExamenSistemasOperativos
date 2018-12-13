using Examen1.SistemasOperativos.Core.Configs;
using Examen1.SistemasOperativos.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen1.SistemasOperativos.Core
{
    public class SingletonRecursosFactory
    {
        private static volatile List<Recurso> instance = null;
        private static readonly object padlock = new object();
        public SingletonRecursosFactory() { }
        public static List<Recurso> Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                            instance = CreateRecursos();
                    }
                }
                return instance;
            }
        }
        private static List<Recurso> CreateRecursos()
        {
            return Enumerable.Range(1, SingletonSimuladorConfigurationFactory.Instance.CantidadRecursos)
                              .Select(Id => new Recurso(Id))
                              .ToList();
        }
    }
}