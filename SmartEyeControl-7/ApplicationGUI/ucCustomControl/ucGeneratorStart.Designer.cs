namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    partial class ucGeneratorStart
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnl_StartGenerator = new System.Windows.Forms.Panel();
            this.grpTariffOnStartGenerator = new System.Windows.Forms.GroupBox();
            this.lblTarrifOnGeneratorStart = new System.Windows.Forms.Label();
            this.cmbTarrifOnGeneratorStart = new System.Windows.Forms.ComboBox();
            this.txt_MonitoringTime_GeneratorStart = new System.Windows.Forms.DateTimePicker();
            this.lbl_MonitoringTime_StartGenerator = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnl_StartGenerator.SuspendLayout();
            this.grpTariffOnStartGenerator.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_StartGenerator
            // 
            this.pnl_StartGenerator.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnl_StartGenerator.Controls.Add(this.grpTariffOnStartGenerator);
            this.pnl_StartGenerator.Controls.Add(this.txt_MonitoringTime_GeneratorStart);
            this.pnl_StartGenerator.Controls.Add(this.lbl_MonitoringTime_StartGenerator);
            this.pnl_StartGenerator.Location = new System.Drawing.Point(8, 8);
            this.pnl_StartGenerator.Name = "pnl_StartGenerator";
            this.pnl_StartGenerator.Size = new System.Drawing.Size(409, 351);
            this.pnl_StartGenerator.TabIndex = 79;
            // 
            // grpTariffOnStartGenerator
            // 
            this.grpTariffOnStartGenerator.Controls.Add(this.lblTarrifOnGeneratorStart);
            this.grpTariffOnStartGenerator.Controls.Add(this.cmbTarrifOnGeneratorStart);
            this.grpTariffOnStartGenerator.ForeColor = System.Drawing.Color.Maroon;
            this.grpTariffOnStartGenerator.Location = new System.Drawing.Point(6, 51);
            this.grpTariffOnStartGenerator.Name = "grpTariffOnStartGenerator";
            this.grpTariffOnStartGenerator.Size = new System.Drawing.Size(238, 49);
            this.grpTariffOnStartGenerator.TabIndex = 34;
            this.grpTariffOnStartGenerator.TabStop = false;
            this.grpTariffOnStartGenerator.Text = "Tariff On Generator Start";
            // 
            // lblTarrifOnGeneratorStart
            // 
            this.lblTarrifOnGeneratorStart.AutoSize = true;
            this.lblTarrifOnGeneratorStart.ForeColor = System.Drawing.Color.Navy;
            this.lblTarrifOnGeneratorStart.Location = new System.Drawing.Point(10, 26);
            this.lblTarrifOnGeneratorStart.Name = "lblTarrifOnGeneratorStart";
            this.lblTarrifOnGeneratorStart.Size = new System.Drawing.Size(31, 13);
            this.lblTarrifOnGeneratorStart.TabIndex = 44;
            this.lblTarrifOnGeneratorStart.Text = "Tarrif";
            // 
            // cmbTarrifOnGeneratorStart
            // 
            this.cmbTarrifOnGeneratorStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTarrifOnGeneratorStart.FormattingEnabled = true;
            this.cmbTarrifOnGeneratorStart.Location = new System.Drawing.Point(161, 18);
            this.cmbTarrifOnGeneratorStart.Name = "cmbTarrifOnGeneratorStart";
            this.cmbTarrifOnGeneratorStart.Size = new System.Drawing.Size(71, 21);
            this.cmbTarrifOnGeneratorStart.TabIndex = 43;
            this.cmbTarrifOnGeneratorStart.SelectedIndexChanged += new System.EventHandler(this.cmbTarrifOnGeneratorStart_SelectedIndexChanged);
            // 
            // txt_MonitoringTime_GeneratorStart
            // 
            this.txt_MonitoringTime_GeneratorStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_MonitoringTime_GeneratorStart.CustomFormat = "mm:ss";
            this.txt_MonitoringTime_GeneratorStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txt_MonitoringTime_GeneratorStart.Location = new System.Drawing.Point(191, 19);
            this.txt_MonitoringTime_GeneratorStart.MinDate = new System.DateTime(2018, 10, 19, 0, 0, 0, 0);
            this.txt_MonitoringTime_GeneratorStart.Name = "txt_MonitoringTime_GeneratorStart";
            this.txt_MonitoringTime_GeneratorStart.ShowUpDown = true;
            this.txt_MonitoringTime_GeneratorStart.Size = new System.Drawing.Size(53, 20);
            this.txt_MonitoringTime_GeneratorStart.TabIndex = 14;
            this.txt_MonitoringTime_GeneratorStart.Value = new System.DateTime(2018, 10, 19, 0, 0, 0, 0);
            this.txt_MonitoringTime_GeneratorStart.Leave += new System.EventHandler(this.txt_MonitoringTime_GeneratorStart_Leave);
            // 
            // lbl_MonitoringTime_StartGenerator
            // 
            this.lbl_MonitoringTime_StartGenerator.AutoSize = true;
            this.lbl_MonitoringTime_StartGenerator.BackColor = System.Drawing.Color.Transparent;
            this.lbl_MonitoringTime_StartGenerator.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lbl_MonitoringTime_StartGenerator.ForeColor = System.Drawing.Color.Black;
            this.lbl_MonitoringTime_StartGenerator.Location = new System.Drawing.Point(3, 25);
            this.lbl_MonitoringTime_StartGenerator.Name = "lbl_MonitoringTime_StartGenerator";
            this.lbl_MonitoringTime_StartGenerator.Size = new System.Drawing.Size(169, 14);
            this.lbl_MonitoringTime_StartGenerator.TabIndex = 33;
            this.lbl_MonitoringTime_StartGenerator.Text = "Generator Start Monitoring Time";
            // 
            // ucGeneratorStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_StartGenerator);
            this.Name = "ucGeneratorStart";
            this.Size = new System.Drawing.Size(434, 378);
            this.pnl_StartGenerator.ResumeLayout(false);
            this.pnl_StartGenerator.PerformLayout();
            this.grpTariffOnStartGenerator.ResumeLayout(false);
            this.grpTariffOnStartGenerator.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_StartGenerator;
        public System.Windows.Forms.Label lbl_MonitoringTime_StartGenerator;
        public System.Windows.Forms.GroupBox grpTariffOnStartGenerator;
        private System.Windows.Forms.Label lblTarrifOnGeneratorStart;
        private System.Windows.Forms.ComboBox cmbTarrifOnGeneratorStart;
        private System.Windows.Forms.DateTimePicker txt_MonitoringTime_GeneratorStart;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
