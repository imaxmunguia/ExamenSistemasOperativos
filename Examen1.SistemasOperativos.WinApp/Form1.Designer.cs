namespace Examen1.SistemasOperativos.WinApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dtProcesadores = new System.Windows.Forms.DataGridView();
            this.cmbProcesadores = new System.Windows.Forms.ComboBox();
            this.cmbNucleos = new System.Windows.Forms.ComboBox();
            this.cmbVelocidadSimulacion = new System.Windows.Forms.ComboBox();
            this.cmbAlgoritmoSimulacion = new System.Windows.Forms.ComboBox();
            this.lblProcesadores = new System.Windows.Forms.Label();
            this.lblNucleos = new System.Windows.Forms.Label();
            this.lblVelocidadSimulacion = new System.Windows.Forms.Label();
            this.lblAlgoritmoSimulacion = new System.Windows.Forms.Label();
            this.btnSimular = new System.Windows.Forms.Button();
            this.dtProcesos = new System.Windows.Forms.DataGridView();
            this.dtRecursos = new System.Windows.Forms.DataGridView();
            this.bgw = new System.ComponentModel.BackgroundWorker();
            this.btnPlanificar = new System.Windows.Forms.Button();
            this.btnBloqueo = new System.Windows.Forms.Button();
            this.btnAgregarRecurso = new System.Windows.Forms.Button();
            this.btnAgregarProceso = new System.Windows.Forms.Button();
            this.dtMemoria = new System.Windows.Forms.DataGridView();
            this.dt = new System.Windows.Forms.DataGridView();
            this.cmbAlgoritmoMemoria = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtProcesadores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtProcesos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtRecursos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtMemoria)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt)).BeginInit();
            this.SuspendLayout();
            // 
            // dtProcesadores
            // 
            this.dtProcesadores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtProcesadores.Location = new System.Drawing.Point(971, 55);
            this.dtProcesadores.Margin = new System.Windows.Forms.Padding(4);
            this.dtProcesadores.Name = "dtProcesadores";
            this.dtProcesadores.RowTemplate.Height = 24;
            this.dtProcesadores.Size = new System.Drawing.Size(845, 762);
            this.dtProcesadores.TabIndex = 0;
            // 
            // cmbProcesadores
            // 
            this.cmbProcesadores.FormattingEnabled = true;
            this.cmbProcesadores.Location = new System.Drawing.Point(1465, 24);
            this.cmbProcesadores.Margin = new System.Windows.Forms.Padding(4);
            this.cmbProcesadores.Name = "cmbProcesadores";
            this.cmbProcesadores.Size = new System.Drawing.Size(50, 24);
            this.cmbProcesadores.TabIndex = 1;
            this.cmbProcesadores.SelectedIndexChanged += new System.EventHandler(this.cmbProcesadores_SelectedIndexChanged);
            // 
            // cmbNucleos
            // 
            this.cmbNucleos.FormattingEnabled = true;
            this.cmbNucleos.Location = new System.Drawing.Point(1574, 24);
            this.cmbNucleos.Margin = new System.Windows.Forms.Padding(4);
            this.cmbNucleos.Name = "cmbNucleos";
            this.cmbNucleos.Size = new System.Drawing.Size(50, 24);
            this.cmbNucleos.TabIndex = 2;
            this.cmbNucleos.SelectedIndexChanged += new System.EventHandler(this.cmbNucleos_SelectedIndexChanged);
            // 
            // cmbVelocidadSimulacion
            // 
            this.cmbVelocidadSimulacion.FormattingEnabled = true;
            this.cmbVelocidadSimulacion.Location = new System.Drawing.Point(1716, 24);
            this.cmbVelocidadSimulacion.Margin = new System.Windows.Forms.Padding(4);
            this.cmbVelocidadSimulacion.Name = "cmbVelocidadSimulacion";
            this.cmbVelocidadSimulacion.Size = new System.Drawing.Size(100, 24);
            this.cmbVelocidadSimulacion.TabIndex = 3;
            this.cmbVelocidadSimulacion.SelectedIndexChanged += new System.EventHandler(this.cmbVelocidadSimulacion_SelectedIndexChanged);
            // 
            // cmbAlgoritmoSimulacion
            // 
            this.cmbAlgoritmoSimulacion.FormattingEnabled = true;
            this.cmbAlgoritmoSimulacion.Location = new System.Drawing.Point(96, 821);
            this.cmbAlgoritmoSimulacion.Margin = new System.Windows.Forms.Padding(4);
            this.cmbAlgoritmoSimulacion.Name = "cmbAlgoritmoSimulacion";
            this.cmbAlgoritmoSimulacion.Size = new System.Drawing.Size(205, 24);
            this.cmbAlgoritmoSimulacion.TabIndex = 4;
            this.cmbAlgoritmoSimulacion.SelectedIndexChanged += new System.EventHandler(this.cmbAlgoritmoSimulacion_SelectedIndexChanged);
            // 
            // lblProcesadores
            // 
            this.lblProcesadores.AutoSize = true;
            this.lblProcesadores.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcesadores.Location = new System.Drawing.Point(1351, 27);
            this.lblProcesadores.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProcesadores.Name = "lblProcesadores";
            this.lblProcesadores.Size = new System.Drawing.Size(106, 16);
            this.lblProcesadores.TabIndex = 7;
            this.lblProcesadores.Text = "Procesadores";
            // 
            // lblNucleos
            // 
            this.lblNucleos.AutoSize = true;
            this.lblNucleos.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNucleos.Location = new System.Drawing.Point(1523, 27);
            this.lblNucleos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNucleos.Name = "lblNucleos";
            this.lblNucleos.Size = new System.Drawing.Size(43, 16);
            this.lblNucleos.TabIndex = 8;
            this.lblNucleos.Text = "Hilos";
            // 
            // lblVelocidadSimulacion
            // 
            this.lblVelocidadSimulacion.AutoSize = true;
            this.lblVelocidadSimulacion.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVelocidadSimulacion.Location = new System.Drawing.Point(1632, 27);
            this.lblVelocidadSimulacion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVelocidadSimulacion.Name = "lblVelocidadSimulacion";
            this.lblVelocidadSimulacion.Size = new System.Drawing.Size(76, 16);
            this.lblVelocidadSimulacion.TabIndex = 9;
            this.lblVelocidadSimulacion.Text = "Velocidad";
            // 
            // lblAlgoritmoSimulacion
            // 
            this.lblAlgoritmoSimulacion.AutoSize = true;
            this.lblAlgoritmoSimulacion.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlgoritmoSimulacion.Location = new System.Drawing.Point(13, 821);
            this.lblAlgoritmoSimulacion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAlgoritmoSimulacion.Name = "lblAlgoritmoSimulacion";
            this.lblAlgoritmoSimulacion.Size = new System.Drawing.Size(75, 16);
            this.lblAlgoritmoSimulacion.TabIndex = 10;
            this.lblAlgoritmoSimulacion.Text = "Algoritmo";
            // 
            // btnSimular
            // 
            this.btnSimular.Location = new System.Drawing.Point(680, 821);
            this.btnSimular.Name = "btnSimular";
            this.btnSimular.Size = new System.Drawing.Size(158, 28);
            this.btnSimular.TabIndex = 23;
            this.btnSimular.Text = "Simular";
            this.btnSimular.UseVisualStyleBackColor = true;
            this.btnSimular.Click += new System.EventHandler(this.btnSimular_Click);
            // 
            // dtProcesos
            // 
            this.dtProcesos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtProcesos.Location = new System.Drawing.Point(12, 457);
            this.dtProcesos.Margin = new System.Windows.Forms.Padding(4);
            this.dtProcesos.Name = "dtProcesos";
            this.dtProcesos.RowTemplate.Height = 24;
            this.dtProcesos.Size = new System.Drawing.Size(503, 356);
            this.dtProcesos.TabIndex = 24;
            // 
            // dtRecursos
            // 
            this.dtRecursos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtRecursos.Location = new System.Drawing.Point(12, 55);
            this.dtRecursos.Margin = new System.Windows.Forms.Padding(4);
            this.dtRecursos.Name = "dtRecursos";
            this.dtRecursos.RowTemplate.Height = 24;
            this.dtRecursos.Size = new System.Drawing.Size(503, 353);
            this.dtRecursos.TabIndex = 25;
            // 
            // bgw
            // 
            this.bgw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_DoWork);
            this.bgw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_RunWorkerCompleted);
            // 
            // btnPlanificar
            // 
            this.btnPlanificar.Location = new System.Drawing.Point(844, 821);
            this.btnPlanificar.Name = "btnPlanificar";
            this.btnPlanificar.Size = new System.Drawing.Size(120, 28);
            this.btnPlanificar.TabIndex = 26;
            this.btnPlanificar.Text = "Reiniciar";
            this.btnPlanificar.UseVisualStyleBackColor = true;
            this.btnPlanificar.Click += new System.EventHandler(this.btnPlanificar_Click);
            // 
            // btnBloqueo
            // 
            this.btnBloqueo.Location = new System.Drawing.Point(522, 821);
            this.btnBloqueo.Name = "btnBloqueo";
            this.btnBloqueo.Size = new System.Drawing.Size(152, 28);
            this.btnBloqueo.TabIndex = 27;
            this.btnBloqueo.Text = "Generar Bloqueo";
            this.btnBloqueo.UseVisualStyleBackColor = true;
            this.btnBloqueo.Click += new System.EventHandler(this.btnBloqueo_Click);
            // 
            // btnAgregarRecurso
            // 
            this.btnAgregarRecurso.Location = new System.Drawing.Point(380, 13);
            this.btnAgregarRecurso.Name = "btnAgregarRecurso";
            this.btnAgregarRecurso.Size = new System.Drawing.Size(135, 35);
            this.btnAgregarRecurso.TabIndex = 28;
            this.btnAgregarRecurso.Text = "Agregar Recurso";
            this.btnAgregarRecurso.UseVisualStyleBackColor = true;
            this.btnAgregarRecurso.Click += new System.EventHandler(this.btnAgregarRecurso_Click);
            // 
            // btnAgregarProceso
            // 
            this.btnAgregarProceso.Location = new System.Drawing.Point(381, 415);
            this.btnAgregarProceso.Name = "btnAgregarProceso";
            this.btnAgregarProceso.Size = new System.Drawing.Size(135, 35);
            this.btnAgregarProceso.TabIndex = 29;
            this.btnAgregarProceso.Text = "Agregar Proceso";
            this.btnAgregarProceso.UseVisualStyleBackColor = true;
            this.btnAgregarProceso.Click += new System.EventHandler(this.btnAgregarProceso_Click);
            // 
            // dtMemoria
            // 
            this.dtMemoria.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtMemoria.Location = new System.Drawing.Point(972, 824);
            this.dtMemoria.Name = "dtMemoria";
            this.dtMemoria.Size = new System.Drawing.Size(845, 25);
            this.dtMemoria.TabIndex = 30;
            // 
            // dt
            // 
            this.dt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dt.Location = new System.Drawing.Point(522, 55);
            this.dt.Name = "dt";
            this.dt.Size = new System.Drawing.Size(442, 760);
            this.dt.TabIndex = 31;
            // 
            // cmbAlgoritmoMemoria
            // 
            this.cmbAlgoritmoMemoria.FormattingEnabled = true;
            this.cmbAlgoritmoMemoria.Location = new System.Drawing.Point(309, 821);
            this.cmbAlgoritmoMemoria.Margin = new System.Windows.Forms.Padding(4);
            this.cmbAlgoritmoMemoria.Name = "cmbAlgoritmoMemoria";
            this.cmbAlgoritmoMemoria.Size = new System.Drawing.Size(204, 24);
            this.cmbAlgoritmoMemoria.TabIndex = 32;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1829, 861);
            this.Controls.Add(this.cmbAlgoritmoMemoria);
            this.Controls.Add(this.dt);
            this.Controls.Add(this.dtMemoria);
            this.Controls.Add(this.btnAgregarProceso);
            this.Controls.Add(this.btnAgregarRecurso);
            this.Controls.Add(this.btnBloqueo);
            this.Controls.Add(this.btnPlanificar);
            this.Controls.Add(this.dtRecursos);
            this.Controls.Add(this.dtProcesos);
            this.Controls.Add(this.btnSimular);
            this.Controls.Add(this.lblAlgoritmoSimulacion);
            this.Controls.Add(this.lblVelocidadSimulacion);
            this.Controls.Add(this.lblNucleos);
            this.Controls.Add(this.lblProcesadores);
            this.Controls.Add(this.cmbAlgoritmoSimulacion);
            this.Controls.Add(this.cmbVelocidadSimulacion);
            this.Controls.Add(this.cmbNucleos);
            this.Controls.Add(this.cmbProcesadores);
            this.Controls.Add(this.dtProcesadores);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Planificador";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtProcesadores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtProcesos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtRecursos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtMemoria)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtProcesadores;
        private System.Windows.Forms.ComboBox cmbProcesadores;
        private System.Windows.Forms.ComboBox cmbNucleos;
        private System.Windows.Forms.ComboBox cmbVelocidadSimulacion;
        private System.Windows.Forms.ComboBox cmbAlgoritmoSimulacion;
        private System.Windows.Forms.Label lblProcesadores;
        private System.Windows.Forms.Label lblNucleos;
        private System.Windows.Forms.Label lblVelocidadSimulacion;
        private System.Windows.Forms.Label lblAlgoritmoSimulacion;
        private System.Windows.Forms.Button btnSimular;
        private System.Windows.Forms.DataGridView dtProcesos;
        private System.Windows.Forms.DataGridView dtRecursos;
        private System.ComponentModel.BackgroundWorker bgw;
        private System.Windows.Forms.Button btnPlanificar;
        private System.Windows.Forms.Button btnBloqueo;
        private System.Windows.Forms.Button btnAgregarRecurso;
        private System.Windows.Forms.Button btnAgregarProceso;
        private System.Windows.Forms.DataGridView dtMemoria;
        private System.Windows.Forms.DataGridView dt;
        private System.Windows.Forms.ComboBox cmbAlgoritmoMemoria;
    }
}

