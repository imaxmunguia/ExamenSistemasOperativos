using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen1.SistemasOperativos.Core.Configs
{
    public class SimuladorConfiguration
    {
        public int CantidadProcesadores { get; set; }
        public int CantidadNucleosProcesador { get; set; }
        public int VelocidadSimulacion { get; set; }
        public int AlgoritmoSimulacion { get; set; }
        public int CantidadProcesos { get; set; }
        public int CantidadMinimaRecursosProceso { get; set; }
        public int CantidadMaximaRecursosProceso { get; set; }
        public int CantidadMinimaTiempoProceso { get; set; }
        public int CantidadMaximaTiempoProceso { get; set; }
        public int CantidadRecursos { get; set; }
        public int CantidadMinimaRecursoApropiacion { get; set; }
        public int CantidadMaximaRecursoApropiacion { get; set; }
        public bool SimularDeadLock { get; set; }
        public override string ToString()
        {
            return $"Configs: No. Procesadores: {CantidadProcesadores}, No. Nucleos: {CantidadNucleosProcesador}, Velocidad: {VelocidadSimulacion}, Algoritmo: {AlgoritmoSimulacion}, No. Procesos: {CantidadProcesos}, No. Recursos: {CantidadRecursos}";
        }
    }
}