using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Examen1.SistemasOperativos.Core.Configs
{
    public class SingletonSimuladorConfigurationFactory
    {
        private static volatile SimuladorConfiguration instance = null;
        private static readonly object padlock = new object();
        public SingletonSimuladorConfigurationFactory(){ }
        public static SimuladorConfiguration Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                            instance = new SimuladorConfiguration();
                    }
                }
                return instance;
            }
        }
    }
}