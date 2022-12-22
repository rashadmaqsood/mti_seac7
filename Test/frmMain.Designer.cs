namespace RelayTest
{
    partial class frmMain
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
            this.cmbMeterType = new System.Windows.Forms.ComboBox();
            this.lblMeterType = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSelectPort = new System.Windows.Forms.Label();
            this.cmbPorts = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.gbTimerSettings = new System.Windows.Forms.GroupBox();
            this.dtpOffTime = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.btnTimerStart = new System.Windows.Forms.Button();
            this.dtpOnTime = new System.Windows.Forms.DateTimePicker();
            this.lblInterval = new System.Windows.Forms.Label();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.lblLog = new System.Windows.Forms.Label();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.lblTotalItrations = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblSuccess = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblFail = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbTimerSettings.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbMeterType
            // 
            this.cmbMeterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMeterType.FormattingEnabled = true;
            this.cmbMeterType.Items.AddRange(new object[] {
            "Fusion",
            "Non-Fusion"});
            this.cmbMeterType.Location = new System.Drawing.Point(100, 38);
            this.cmbMeterType.Name = "cmbMeterType";
            this.cmbMeterType.Size = new System.Drawing.Size(124, 21);
            this.cmbMeterType.TabIndex = 0;
            this.cmbMeterType.SelectedIndexChanged += new System.EventHandler(this.cmbMeterType_SelectedIndexChanged);
            // 
            // lblMeterType
            // 
            this.lblMeterType.AutoSize = true;
            this.lblMeterType.Location = new System.Drawing.Point(97, 22);
            this.lblMeterType.Name = "lblMeterType";
            this.lblMeterType.Size = new System.Drawing.Size(94, 13);
            this.lblMeterType.TabIndex = 1;
            this.lblMeterType.Text = "Select Meter Type";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(235, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Meter RelayTester";
            // 
            // lblSelectPort
            // 
            this.lblSelectPort.AutoSize = true;
            this.lblSelectPort.Location = new System.Drawing.Point(6, 22);
            this.lblSelectPort.Name = "lblSelectPort";
            this.lblSelectPort.Size = new System.Drawing.Size(86, 13);
            this.lblSelectPort.TabIndex = 7;
            this.lblSelectPort.Text = "Select COM Port";
            // 
            // cmbPorts
            // 
            this.cmbPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPorts.FormattingEnabled = true;
            this.cmbPorts.Items.AddRange(new object[] {
            "Fusion",
            "Non-Fusion"});
            this.cmbPorts.Location = new System.Drawing.Point(6, 38);
            this.cmbPorts.Name = "cmbPorts";
            this.cmbPorts.Size = new System.Drawing.Size(85, 21);
            this.cmbPorts.TabIndex = 6;
            this.cmbPorts.SelectedIndexChanged += new System.EventHandler(this.cmbPorts_SelectedIndexChanged);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(228, 36);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(98, 23);
            this.btnConnect.TabIndex = 9;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // gbTimerSettings
            // 
            this.gbTimerSettings.Controls.Add(this.dtpOffTime);
            this.gbTimerSettings.Controls.Add(this.label2);
            this.gbTimerSettings.Controls.Add(this.btnTimerStart);
            this.gbTimerSettings.Controls.Add(this.dtpOnTime);
            this.gbTimerSettings.Controls.Add(this.lblInterval);
            this.gbTimerSettings.Location = new System.Drawing.Point(348, 48);
            this.gbTimerSettings.Name = "gbTimerSettings";
            this.gbTimerSettings.Size = new System.Drawing.Size(337, 67);
            this.gbTimerSettings.TabIndex = 10;
            this.gbTimerSettings.TabStop = false;
            this.gbTimerSettings.Text = "Contactor ON/OFF Settings";
            // 
            // dtpOffTime
            // 
            this.dtpOffTime.CustomFormat = "mm:ss";
            this.dtpOffTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOffTime.Location = new System.Drawing.Point(215, 26);
            this.dtpOffTime.Name = "dtpOffTime";
            this.dtpOffTime.ShowUpDown = true;
            this.dtpOffTime.Size = new System.Drawing.Size(57, 20);
            this.dtpOffTime.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(156, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "OFF Time";
            // 
            // btnTimerStart
            // 
            this.btnTimerStart.Location = new System.Drawing.Point(275, 23);
            this.btnTimerStart.Name = "btnTimerStart";
            this.btnTimerStart.Size = new System.Drawing.Size(56, 23);
            this.btnTimerStart.TabIndex = 12;
            this.btnTimerStart.Text = "Start";
            this.btnTimerStart.UseVisualStyleBackColor = true;
            this.btnTimerStart.Click += new System.EventHandler(this.btnTimerStart_Click);
            // 
            // dtpOnTime
            // 
            this.dtpOnTime.CustomFormat = "mm:ss";
            this.dtpOnTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOnTime.Location = new System.Drawing.Point(94, 26);
            this.dtpOnTime.Name = "dtpOnTime";
            this.dtpOnTime.ShowUpDown = true;
            this.dtpOnTime.Size = new System.Drawing.Size(57, 20);
            this.dtpOnTime.TabIndex = 14;
            this.dtpOnTime.Value = new System.DateTime(2018, 1, 1, 9, 29, 39, 0);
            // 
            // lblInterval
            // 
            this.lblInterval.AutoSize = true;
            this.lblInterval.Location = new System.Drawing.Point(30, 30);
            this.lblInterval.Name = "lblInterval";
            this.lblInterval.Size = new System.Drawing.Size(47, 13);
            this.lblInterval.TabIndex = 12;
            this.lblInterval.Text = "On Time";
            // 
            // rtbLog
            // 
            this.rtbLog.Location = new System.Drawing.Point(11, 134);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.Size = new System.Drawing.Size(668, 224);
            this.rtbLog.TabIndex = 11;
            this.rtbLog.Text = "";
            this.rtbLog.TextChanged += new System.EventHandler(this.rtbLog_TextChanged);
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.Location = new System.Drawing.Point(14, 118);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(25, 13);
            this.lblLog.TabIndex = 12;
            this.lblLog.Text = "Log";
            // 
            // btnClearLog
            // 
            this.btnClearLog.Location = new System.Drawing.Point(621, 362);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(56, 23);
            this.btnClearLog.TabIndex = 16;
            this.btnClearLog.Text = "Clear";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // lblTotalItrations
            // 
            this.lblTotalItrations.AutoSize = true;
            this.lblTotalItrations.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalItrations.Location = new System.Drawing.Point(522, 118);
            this.lblTotalItrations.Name = "lblTotalItrations";
            this.lblTotalItrations.Size = new System.Drawing.Size(14, 13);
            this.lblTotalItrations.TabIndex = 18;
            this.lblTotalItrations.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(429, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Total Iterations";
            // 
            // lblSuccess
            // 
            this.lblSuccess.AutoSize = true;
            this.lblSuccess.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSuccess.Location = new System.Drawing.Point(597, 118);
            this.lblSuccess.Name = "lblSuccess";
            this.lblSuccess.Size = new System.Drawing.Size(14, 13);
            this.lblSuccess.TabIndex = 20;
            this.lblSuccess.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(545, 118);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Success";
            // 
            // lblFail
            // 
            this.lblFail.AutoSize = true;
            this.lblFail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFail.Location = new System.Drawing.Point(664, 118);
            this.lblFail.Name = "lblFail";
            this.lblFail.Size = new System.Drawing.Size(14, 13);
            this.lblFail.TabIndex = 22;
            this.lblFail.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(630, 118);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "Fail";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbPorts);
            this.groupBox1.Controls.Add(this.cmbMeterType);
            this.groupBox1.Controls.Add(this.lblMeterType);
            this.groupBox1.Controls.Add(this.lblSelectPort);
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Location = new System.Drawing.Point(11, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(331, 67);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection Settings";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 388);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblFail);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblSuccess);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblTotalItrations);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnClearLog);
            this.Controls.Add(this.lblLog);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.gbTimerSettings);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RelayTest Application V 1.0.0.2";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.gbTimerSettings.ResumeLayout(false);
            this.gbTimerSettings.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbMeterType;
        private System.Windows.Forms.Label lblMeterType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSelectPort;
        private System.Windows.Forms.ComboBox cmbPorts;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.GroupBox gbTimerSettings;
        private System.Windows.Forms.Label lblInterval;
        private System.Windows.Forms.Button btnTimerStart;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpOnTime;
        private System.Windows.Forms.DateTimePicker dtpOffTime;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.Label lblTotalItrations;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblSuccess;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblFail;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

