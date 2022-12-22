namespace SmartEyeControl_7.Reporting.WapdaFormat
{
    partial class ParamReportSelection
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
            this.rdbTestModeReport = new System.Windows.Forms.RadioButton();
            this.rdbAlternateModeReport = new System.Windows.Forms.RadioButton();
            this.rdbNormalModeReport = new System.Windows.Forms.RadioButton();
            this.rdbProgrammingReport = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btnViewReport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rdbTestModeReport
            // 
            this.rdbTestModeReport.AutoSize = true;
            this.rdbTestModeReport.BackColor = System.Drawing.Color.Transparent;
            this.rdbTestModeReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbTestModeReport.Location = new System.Drawing.Point(28, 138);
            this.rdbTestModeReport.Name = "rdbTestModeReport";
            this.rdbTestModeReport.Size = new System.Drawing.Size(123, 19);
            this.rdbTestModeReport.TabIndex = 22;
            this.rdbTestModeReport.Text = "Test Mode Report";
            this.rdbTestModeReport.UseVisualStyleBackColor = false;
            // 
            // rdbAlternateModeReport
            // 
            this.rdbAlternateModeReport.AutoSize = true;
            this.rdbAlternateModeReport.BackColor = System.Drawing.Color.Transparent;
            this.rdbAlternateModeReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbAlternateModeReport.Location = new System.Drawing.Point(28, 115);
            this.rdbAlternateModeReport.Name = "rdbAlternateModeReport";
            this.rdbAlternateModeReport.Size = new System.Drawing.Size(148, 19);
            this.rdbAlternateModeReport.TabIndex = 21;
            this.rdbAlternateModeReport.Text = "Alternate Mode Report";
            this.rdbAlternateModeReport.UseVisualStyleBackColor = false;
            // 
            // rdbNormalModeReport
            // 
            this.rdbNormalModeReport.AutoSize = true;
            this.rdbNormalModeReport.BackColor = System.Drawing.Color.Transparent;
            this.rdbNormalModeReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbNormalModeReport.Location = new System.Drawing.Point(28, 92);
            this.rdbNormalModeReport.Name = "rdbNormalModeReport";
            this.rdbNormalModeReport.Size = new System.Drawing.Size(141, 19);
            this.rdbNormalModeReport.TabIndex = 20;
            this.rdbNormalModeReport.Text = "Normal Mode Report";
            this.rdbNormalModeReport.UseVisualStyleBackColor = false;
            // 
            // rdbProgrammingReport
            // 
            this.rdbProgrammingReport.AutoSize = true;
            this.rdbProgrammingReport.BackColor = System.Drawing.Color.Transparent;
            this.rdbProgrammingReport.Checked = true;
            this.rdbProgrammingReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbProgrammingReport.Location = new System.Drawing.Point(28, 69);
            this.rdbProgrammingReport.Name = "rdbProgrammingReport";
            this.rdbProgrammingReport.Size = new System.Drawing.Size(141, 19);
            this.rdbProgrammingReport.TabIndex = 19;
            this.rdbProgrammingReport.TabStop = true;
            this.rdbProgrammingReport.Text = "Programming Report";
            this.rdbProgrammingReport.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 16);
            this.label1.TabIndex = 23;
            this.label1.Text = "Please Select Report Type";
            // 
            // btnViewReport
            // 
            this.btnViewReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewReport.Location = new System.Drawing.Point(82, 174);
            this.btnViewReport.Name = "btnViewReport";
            this.btnViewReport.Size = new System.Drawing.Size(96, 42);
            this.btnViewReport.TabIndex = 24;
            this.btnViewReport.Text = "View Report";
            this.btnViewReport.UseVisualStyleBackColor = true;
            this.btnViewReport.Click += new System.EventHandler(this.btnViewReport_Click);
            // 
            // ParamReportSelection
            // 
            this.AcceptButton = this.btnViewReport;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 237);
            this.Controls.Add(this.btnViewReport);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rdbTestModeReport);
            this.Controls.Add(this.rdbAlternateModeReport);
            this.Controls.Add(this.rdbNormalModeReport);
            this.Controls.Add(this.rdbProgrammingReport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(276, 275);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(276, 275);
            this.Name = "ParamReportSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Parameter Report Selection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdbTestModeReport;
        private System.Windows.Forms.RadioButton rdbAlternateModeReport;
        private System.Windows.Forms.RadioButton rdbNormalModeReport;
        private System.Windows.Forms.RadioButton rdbProgrammingReport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnViewReport;
    }
}