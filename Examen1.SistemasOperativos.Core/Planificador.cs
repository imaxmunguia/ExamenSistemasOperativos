using Examen1.SistemasOperativos.Core.Configs;
using Examen1.SistemasOperativos.Core.Models;
using Examen1.SistemasOperativos.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examen1.SistemasOperativos.Core
{
    public class Planificador
    {
        object objLock = new object();
        public List<Procesador> Procesadores;
        public List<Proceso> Procesos;
        public List<Recurso> Recursos;
        public int contador_bloqueo;

        public Planificador()
        {
            this.Procesadores = new List<Procesador>();
            this.Recursos = new List<Recurso>();
            this.Procesos = new List<Proceso>();
        }

        public void agregarProcesadores()
        {
            Procesadores.Clear();
            var cantidadProcesadores = SingletonSimuladorConfigurationFactory.Instance.CantidadProcesadores;
            var cantidadNucleosProcesador = SingletonSimuladorConfigurationFactory.Instance.CantidadNucleosProcesador;

            Procesadores = Enumerable.Range(1, cantidadProcesadores)
                                     .Select(i => new Procesador
                                     {
                                         Id = i,
                                         Nucleos = cantidadNucleosProcesador
                                     })
                                     .ToList();
        }

        public void agregarRecurso()
        {
            this.Recursos.Add(new Recurso(this.Recursos.Count+1));
        }
        public void agregarProceso()
        {
            SingletonSimuladorConfigurationFactory.Instance.CantidadRecursos = 0;
            this.Procesos.Add(new Proceso(this.Procesos.Count+1, this.Recursos));
        }
        public void Simular()
        {
            Procesos.Where(p => p.Estado == 1).ToList().ForEach(s => s.Estado = 2);

            //Ordenamos los procesos que deben entrar al procesador segun el algoritmo de planificacion de procesos
            var procesosListos = Procesos.Where(p => p.TiempoRestante > 0)
                                         .OrderBy(p => SingletonSimuladorConfigurationFactory.Instance.AlgoritmoSimulacion == 0 ? p.Id : p.Turno)
                                         .Take(Procesadores.Count)
                                         .ToList();

            if (SingletonSimuladorConfigurationFactory.Instance.AlgoritmoSimulacion == 1)
                lock (objLock) Recursos.ToList()
                                       .ForEach(recurso => recurso.ProcesosRegistrados.RemoveAll(p => true));

            //Calculamos orden de Round Robin
            Procesos.ToList().ForEach(s => s.Turno = s.Turno == 1 ? Procesos.Count : s.Turno - 1);

            Parallel.ForEach(Procesadores.OrderBy(obj => Guid.NewGuid()), procesador =>
            {
                procesador.Proceso = string.Empty;
               
                Proceso proceso;
                lock (objLock) {
                    proceso = procesosListos.FirstOrDefault();
                    procesosListos.Remove(proceso);
                } 

                if (proceso != null)
                {
                    try
                    {
                        registrarProcesoEnRecursos(proceso);

                        Procesos.First(p => p.Id == proceso.Id).Ciclos++;

                        if (proceso.TiempoRestante > procesador.Nucleos)
                        {
                            Procesos.First(p => p.Id == proceso.Id).TiempoRestante -= procesador.Nucleos;
                            Procesos.First(p => p.Id == proceso.Id).Estado = (int)EstadosProceso.Estado.Ejecutando;
                        }
                        else
                        {
                            Procesos.First(p => p.Id == proceso.Id).TiempoRestante = 0;
                            Procesos.First(p => p.Id == proceso.Id).Estado = (int)EstadosProceso.Estado.Terminado;
                        }

                        if (proceso.Estado == 5)
                            lock (objLock) Recursos.Where(r => proceso.Recursos.Any(pr => pr == r.Id))
                                                   .ToList()
                                                   .ForEach(recurso => recurso.ProcesosRegistrados.RemoveAll(p => p == proceso.Id));

                        procesador.Proceso = imprimirProceso(proceso);
                    }
                    catch (InvalidOperationException oex)
                    {
                        Procesos.First(p => p.Id == proceso.Id).Estado = (int)EstadosProceso.Estado.Esperando;
                        procesador.Proceso = imprimirProceso(proceso);
                    }
                    catch (Exception ex)
                    {

                    }
                }

            });
            System.Threading.Thread.Sleep(SingletonSimuladorConfigurationFactory.Instance.VelocidadSimulacion);
        }
        public void registrarProcesoEnRecursos(Proceso proceso)
        {
            foreach (var recursoId in proceso.Recursos)
            {
                if (!Recursos.First(r => r.Id == recursoId).ProcesosRegistrados.Contains(proceso.Id))
                {
                    lock (objLock)
                    {
                        //Si el recurso esta disponible para el proceso
                        if (Recursos.First(r => r.Id == recursoId).ProcesosRegistrados.Count >= Recursos.First(r => r.Id == recursoId).Apropiacion)
                        {
                            throw new InvalidOperationException("El recurso no esta disponible, el proceso debe esperar");
                            //lock (objLock) recursosNecesitaProceso.ForEach(recurso => recurso.ProcesosRegistrados.RemoveAll(p => p == proceso.Id));
                        }
                        Recursos.First(r => r.Id == recursoId).ProcesosRegistrados.Add(proceso.Id);
                    }
                }
            }
        }

        public void registrarBloqueo(List<Proceso> procesosEjecucion)
        {
            if (SingletonSimuladorConfigurationFactory.Instance.SimularDeadLock && contador_bloqueo != 0)
            {
                contador_bloqueo = 1;
                Recursos.First(r => r.Id == 1 && !r.EsApropiativo).ProcesosRegistrados.Add(1);
                Recursos.First(r => r.Id == 2 && !r.EsApropiativo).ProcesosRegistrados.Add(2);
            }
            else
            {
                contador_bloqueo = 0;
                Recursos.First(r => r.Id == 1 && !r.EsApropiativo).ProcesosRegistrados.RemoveAll(x => x == 1);
                Recursos.First(r => r.Id == 2 && !r.EsApropiativo).ProcesosRegistrados.RemoveAll(x => x == 2);
            }
        }

        private string imprimirProceso(Proceso proceso)
        {
            var estados = new string[] { "N", "L", "E", "B", "T" };
            var porc = (1 - Decimal.Divide(proceso.TiempoRestante, proceso.Tiempo)) * 100;
            return $"P{proceso.Id}: {porc.ToString("0.0")} [{estados[proceso.Estado - 1]}]";
        }
    }
}
