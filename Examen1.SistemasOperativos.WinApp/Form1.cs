using Examen1.SistemasOperativos.Core;
using Examen1.SistemasOperativos.Core.Configs;
using Examen1.SistemasOperativos.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Examen1.SistemasOperativos.WinApp
{
    public partial class Form1 : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Planificador planificador;
        private int ciclo = 0;
        public Form1()
        {
            InitializeComponent();            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            planificador = new Planificador();
            inicializarControlesFormulario();
            cargarConfiguracionesSimulador();
            actualizarMemoria();
        }
        private void cargarConfiguracionesSimulador()
        {
            //Configuracion de Recursos
            SingletonSimuladorConfigurationFactory.Instance.CantidadMinimaRecursoApropiacion = 2;
            SingletonSimuladorConfigurationFactory.Instance.CantidadMaximaRecursoApropiacion = 2;

            //Configuracion de Procesos
            SingletonSimuladorConfigurationFactory.Instance.CantidadMinimaRecursosProceso = 1;
            SingletonSimuladorConfigurationFactory.Instance.CantidadMaximaRecursosProceso = 2;
            SingletonSimuladorConfigurationFactory.Instance.CantidadMinimaTiempoProceso = 1;
            SingletonSimuladorConfigurationFactory.Instance.CantidadMaximaTiempoProceso = 2;
            SingletonSimuladorConfigurationFactory.Instance.TamanioMarcoPagina = 16;
        }
        private void inicializarControlesFormulario()
        {
            //Cantidad de procesadores
            cmbProcesadores.DataSource = new List<int> { 2, 4, 8 };
            cmbProcesadores.SelectedIndex = 2;

            //Cantidad de nucleos por procesador
            cmbNucleos.DataSource = new List<int> { 1, 2, 3 };
            cmbNucleos.SelectedIndex = 2;

            //Velocidad de simulacion
            cmbVelocidadSimulacion.DataSource = new List<Item> {
                new Item("1x",4000),
                new Item("2x",2000),
                new Item("4x",1000),
                new Item("Ultra Speed",250)
            };
            cmbVelocidadSimulacion.SelectedIndex = 1;

            //Algoritmo para planificar procesos
            cmbAlgoritmoSimulacion.DataSource = new List<Item> {
                new Item("FiFo",0),
                new Item("Prioridad",1),
                new Item("Round Robin",2),
                new Item("SJF",3)
            };
            cmbAlgoritmoSimulacion.SelectedIndex = 0;

            cmbAlgoritmoMemoria.DataSource = new List<Item> {
                new Item("FiFo",0)
            };
            cmbAlgoritmoMemoria.SelectedIndex = 0;
        }

        private void btnSimular_Click(object sender, EventArgs e)
        {
            ciclo = 0;
            limpiarDtProcesadores();
            btnPlanificar.Enabled = false;
            btnSimular.Enabled = false;

            log.Info("Inicio de simulación ");
            log.Info(SingletonSimuladorConfigurationFactory.Instance);
            escribirLog();

            simularAsync();
        }
        private void simularAsync()
        {
            if (!bgw.IsBusy)
                bgw.RunWorkerAsync();
        }
        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            planificador.Simular();
        }
        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var fila = planificador.Procesadores.Select(s => s.Proceso).ToArray();
            dtProcesadores.Rows.Add(fila);

            log.Info($"Ciclo de procesador: {++ciclo}");
            escribirLog();

            actualizarProcesos();
            actualizarRecursos();
            actualizarMemoria();


            //Seguir el ciclo
            if (planificador.Procesos.Any(p => p.Estado != 5))
            {
                simularAsync();
            }
            if (!bgw.IsBusy)
            {
                btnPlanificar.Enabled = true;
                log.Info("Fin de simulación ");
                MessageBox.Show("La simulación  ha finalizado");
            }
        }

        private void btnPlanificar_Click(object sender, EventArgs e)
        {
            planificador = new Planificador();

            actualizarProcesos();
            actualizarRecursos();

            limpiarDtProcesadores();
            btnSimular.Enabled = true;

            planificador.agregarProcesadores();
            limpiarDtProcesadores();
        }
        private void actualizarProcesos()
        {
            var estados = new string[] { "Nuevo", "Listo", "Ejecucion", "Bloqueo", "Terminado" };
            dtProcesos.Columns.Clear();
            var procesos = planificador.Procesos
                                       .OrderBy(p => SingletonSimuladorConfigurationFactory.Instance.AlgoritmoSimulacion == 1 ? p.Prioridad :
                                                     SingletonSimuladorConfigurationFactory.Instance.AlgoritmoSimulacion == 2 ? p.Turno      : p.Id)
                                       .Select(s => new
                                       {
                                            ProcesoId = "P" + s.Id,
                                            Estado = estados[s.Estado - 1],
                                            Orden = SingletonSimuladorConfigurationFactory.Instance.AlgoritmoSimulacion == 1 ? s.Prioridad :
                                                    SingletonSimuladorConfigurationFactory.Instance.AlgoritmoSimulacion == 2 ? s.Turno : s.Id,
                                            Recursos = $"[{string.Join(",", s.Recursos)}]",
                                            s.Tiempo,
                                            Restante = s.TiempoRestante,
                                            Porc = imprimirProceso(s) 
                                       }).ToList();
            dtProcesos.DataSource = procesos;
        }
        private void actualizarRecursos()
        {
            dtRecursos.Columns.Clear();
            var recursos = planificador.Recursos.OrderBy(r => r.Id).Select(r => new { RecursoId = "R" + r.Id, r.EsApropiativo,r.Apropiacion, ProcesosRegistrados = $"[{string.Join(",", r.ProcesosRegistrados)}]" }).ToList();
            dtRecursos.DataSource = recursos;
        }
        private void limpiarDtProcesadores()
        {
            dtProcesadores.Columns.Clear();
            planificador.Procesadores.ForEach(x => dtProcesadores.Columns.Add($"Procesador{x.Id}", $"Procesador{x.Id}"));
        }
        private void actualizarMemoria()
        {
            dtMemoria.Columns.Clear();
            Enumerable.Range(1, 8).ToList().ForEach(x => dtMemoria.Columns.Add($"M{x}", string.Empty));

            var memoria = planificador.Memoria.OrderBy(m => m.Id).Select(m => $"M{m.Id}:{string.Join(",", m.ProcesosRegistrados)}").ToList();
            dtMemoria.Rows.Add(memoria.Skip(0).Take(8).ToArray());
            dtMemoria.Rows.Add(memoria.Skip(8).Take(8).ToArray());
            dtMemoria.Rows.Add(memoria.Skip(16).Take(8).ToArray());
            dtMemoria.Rows.Add(memoria.Skip(32).Take(8).ToArray());
        }
        private void escribirLog()
        {
            log.Info("Recursos ");
            planificador.Recursos.ForEach(r => log.Info(r.ToString()));
            log.Info("Procesos");
            planificador.Procesos.ForEach(p => log.Info(p.ToString()));
        }
        private void btnBloqueo_Click(object sender, EventArgs e)
        {
            SingletonSimuladorConfigurationFactory.Instance.SimularDeadLock = !SingletonSimuladorConfigurationFactory.Instance.SimularDeadLock;
            btnBloqueo.Text = SingletonSimuladorConfigurationFactory.Instance.SimularDeadLock ? "Desbloquear" : "Generar Bloqueo";
        }
        private void btnAgregarRecurso_Click(object sender, EventArgs e)
        {
            planificador.agregarRecurso();
            actualizarRecursos();
        }
        private void btnAgregarProceso_Click(object sender, EventArgs e)
        {
            planificador.agregarProceso();
            actualizarProcesos();
        }
        private void cmbProcesadores_SelectedIndexChanged(object sender, EventArgs e)
        {
            SingletonSimuladorConfigurationFactory.Instance.CantidadProcesadores = (int)cmbProcesadores.SelectedValue;
            planificador.agregarProcesadores();
            limpiarDtProcesadores();
        }
        private void cmbNucleos_SelectedIndexChanged(object sender, EventArgs e)
        {
            SingletonSimuladorConfigurationFactory.Instance.CantidadNucleosProcesador = (int)cmbNucleos.SelectedValue;
            planificador.agregarProcesadores();
            limpiarDtProcesadores();
        }
        private void cmbVelocidadSimulacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            SingletonSimuladorConfigurationFactory.Instance.VelocidadSimulacion = ((Item)cmbVelocidadSimulacion.SelectedValue).Value;
        }
        private void cmbAlgoritmoSimulacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            SingletonSimuladorConfigurationFactory.Instance.AlgoritmoSimulacion = ((Item)cmbAlgoritmoSimulacion.SelectedValue).Value;
        }
        private string imprimirProceso(Proceso proceso)
        {
            var porc = (1 - Decimal.Divide(proceso.TiempoRestante, proceso.Tiempo)) * 100;
            return $"{porc.ToString("0.0")}%";
        }
    }
}
