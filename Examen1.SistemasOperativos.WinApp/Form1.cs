using Examen1.SistemasOperativos.Core;
using Examen1.SistemasOperativos.Core.Configs;
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
        }
        private void cargarConfiguracionesSimulador()
        {
            //Configuracion de Recursos
            SingletonSimuladorConfigurationFactory.Instance.CantidadMinimaRecursoApropiacion = 1;
            SingletonSimuladorConfigurationFactory.Instance.CantidadMaximaRecursoApropiacion = 4;

            //Configuracion de Procesos
            SingletonSimuladorConfigurationFactory.Instance.CantidadMinimaRecursosProceso = 2;
            SingletonSimuladorConfigurationFactory.Instance.CantidadMaximaRecursosProceso = 2;
            SingletonSimuladorConfigurationFactory.Instance.CantidadMinimaTiempoProceso = 100;
            SingletonSimuladorConfigurationFactory.Instance.CantidadMaximaTiempoProceso = 400;
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
                new Item("SJF",0),
                new Item("Prioridad",1),
                new Item("Round Robin",2)
            };
            cmbAlgoritmoSimulacion.SelectedIndex = 0;
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
            var procesos = planificador.Procesos.OrderBy(p => p.Id).Select(s => new { ProcesoId = "P" + s.Id, Estado = estados[s.Estado - 1], Tiempo = s.Tiempo, Recursos = $"[{string.Join(",", s.Recursos)}]" }).ToList();
            dtProcesos.DataSource = procesos;
        }
        private void actualizarRecursos()
        {
            dtRecursos.Columns.Clear();
            var recursos = planificador.Recursos.OrderBy(r => r.Id).Select(r => new { RecursoId = "R" + r.Id, Apropiacion = r.Apropiacion, ProcesosRegistrados = $"[{string.Join(",", r.ProcesosRegistrados)}]" }).ToList();
            dtRecursos.DataSource = recursos;
        }
        private void limpiarDtProcesadores()
        {
            dtProcesadores.Columns.Clear();
            planificador.Procesadores.ForEach(x => dtProcesadores.Columns.Add($"Procesador{x.Id}", $"Procesador{x.Id}"));
        }
        private void escribirLog()
        {
            log.Info("Recursos");
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
    }
}
