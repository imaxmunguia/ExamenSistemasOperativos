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
        public List<Recurso> Memoria;
        public int contador_bloqueo;

        public Planificador()
        {
            this.Procesadores = new List<Procesador>();
            this.Recursos = new List<Recurso>();
            this.Procesos = new List<Proceso>();
            this.Memoria = crearMemoria();
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

        public List<Recurso> crearMemoria()
        {
            var cantidadProcesadores = SingletonSimuladorConfigurationFactory.Instance.CantidadProcesadores;
            var cantidadNucleosProcesador = SingletonSimuladorConfigurationFactory.Instance.CantidadNucleosProcesador;

            return Enumerable.Range(1, 64)
                                     .Select(i => new Recurso(i)
                                     {
                                         Apropiacion = 1,
                                         EsApropiativo = false                                          
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
                                         .OrderBy(p => SingletonSimuladorConfigurationFactory.Instance.AlgoritmoSimulacion == 1 ? p.Prioridad :
                                                       SingletonSimuladorConfigurationFactory.Instance.AlgoritmoSimulacion == 2 ? p.Turno     : p.Id)
                                         .Take(Procesadores.Count)
                                         .ToList();

            if (SingletonSimuladorConfigurationFactory.Instance.AlgoritmoSimulacion == 2)
                lock (objLock) Recursos.ToList().ForEach(recurso => recurso.ProcesosRegistrados.RemoveAll(p => true));

            //Calculamos orden de Round Robin
            Procesos.ToList().ForEach(s => s.Turno = s.Turno == 1 ? Procesos.Count : s.Turno - 1);

            //Simular bloqueos
            registrarBloqueo(procesosListos);

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
                        cargarProcesoEnMemoria(proceso);
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

        public void cargarProcesoEnMemoria(Proceso proceso)
        {
            var marcosOcupaProceso = proceso.TiempoRestante / SingletonSimuladorConfigurationFactory.Instance.TamanioMarcoPagina;

            var marcosPaginaLibres = Memoria.Where(marco => !marco.ProcesosRegistrados.Any()).ToList();

            if (marcosPaginaLibres.Count != 0)
                marcosPaginaLibres.ForEach(marco => 
                {
                    marco.ProcesosRegistrados.Add(proceso.Id);
                    marcosOcupaProceso--;
                });
        }

        public void registrarBloqueo(List<Proceso> procesosEjecucion)
        {
            if (SingletonSimuladorConfigurationFactory.Instance.SimularDeadLock)
            {
                foreach (var p1 in procesosEjecucion)
                {
                    foreach (var p2 in procesosEjecucion)
                    {
                        if (p1.Id != p2.Id)
                        {
                            var rDiferentes = p1.Recursos.Except(p2.Recursos).ToList();
                            if (rDiferentes.Count == 0 && p1.Recursos.Count==2 && p2.Recursos.Count == 2)
                            {
                                var p1R = Recursos.First(r => r.Id == p1.Recursos.FirstOrDefault());
                                var p2R = Recursos.First(r => r.Id == p2.Recursos.FirstOrDefault(rr=>rr!= p1R.Id));

                                Recursos.First(r1 => r1.Id == p1R.Id).ProcesosRegistrados.RemoveAll(r1 => true);
                                Recursos.First(r2 => r2.Id == p2R.Id).ProcesosRegistrados.RemoveAll(r2 => true);

                                Recursos.First(r1 => r1.Id == p1R.Id).ProcesosRegistrados.Add(p1.Id);
                                Recursos.First(r2 => r2.Id == p2R.Id).ProcesosRegistrados.Add(p2.Id);
                            }
                        }
                    }
                }
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
