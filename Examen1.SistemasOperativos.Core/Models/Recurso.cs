using Examen1.SistemasOperativos.Core.Configs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen1.SistemasOperativos.Core.Models
{
    public class Recurso
    {
        public Recurso(int Id)
        {
            this.Id = Id;
            EsApropiativo = Utils.GeneradorAleatorio.GenerarValorLogico();
            Apropiacion = EsApropiativo ? Utils.GeneradorAleatorio.GenerarEntero(SingletonSimuladorConfigurationFactory.Instance.CantidadMinimaRecursoApropiacion,
                                                                                 SingletonSimuladorConfigurationFactory.Instance.CantidadMaximaRecursoApropiacion) : 1;
            ProcesosRegistrados = new List<int>();
        }
        public int Id { get; set; }
        public bool EsApropiativo { get; set; }
        public int Apropiacion { get; set; }
        public List<int> ProcesosRegistrados { get; set; }

        public override string ToString()
        {
            return $"RecursoId: {Id}, EsApropiativo: {EsApropiativo}, Apropiacion: {Apropiacion}, Procesos: [{string.Join(",", ProcesosRegistrados)}]";
        }
    }
}