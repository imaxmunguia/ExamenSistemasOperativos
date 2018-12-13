using Examen1.SistemasOperativos.Core.Configs;
using Examen1.SistemasOperativos.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen1.SistemasOperativos.Core.Models
{
    public class Proceso
    {
        public Proceso(int Id, List<Recurso> recursos)
        {
            this.Id = Id;
            Recursos = recursos.OrderBy(obj => Guid.NewGuid())
                               .Take(GeneradorAleatorio.GenerarEntero(SingletonSimuladorConfigurationFactory.Instance.CantidadMinimaRecursosProceso,
                                                                      SingletonSimuladorConfigurationFactory.Instance.CantidadMaximaRecursosProceso))
                               .Select(recurso => recurso.Id)
                               .ToList();
            Tiempo = GeneradorAleatorio.GenerarEntero(SingletonSimuladorConfigurationFactory.Instance.CantidadMinimaTiempoProceso,
                                                           SingletonSimuladorConfigurationFactory.Instance.CantidadMaximaTiempoProceso);
            TiempoRestante = this.Tiempo;
            Estado = (int)EstadosProceso.Estado.Nuevo;
            Turno = Id;
        }
        public int Id { get; set; }
        public int Estado { get; set; }
        public int Tiempo { get; set; }
        public int TiempoRestante { get; set; }
        public int Ciclos { get; set; }
        public int Turno { get; set; }
        public List<int> Recursos { get; set; }

        public override string ToString()
        {
            var porc = (1 - Decimal.Divide(TiempoRestante, Tiempo)) * 100;
            var estados = new string[] { "Nuevo", "Listo", "Ejecucion", "Bloqueo", "Terminado" };
            return $"ProcesoId: {Id}, Estado: {estados[Estado - 1]}, Tiempo: {Tiempo}, TiempoRestante: {TiempoRestante}, Procesado: {porc.ToString("0.00")}%, Ciclos: {Ciclos}, Recursos: [{string.Join(",", Recursos)}]";
        }
    }
}