namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    partial class ucGeneralProcess
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
            this.gp_GeneralProcess = new System.Windows.Forms.GroupBox();
            this.chkSvSControl = new System.Windows.Forms.CheckBox();
            this.gp_GeneralProcess.SuspendLayout();
            this.SuspendLayout();
            // 
            // gp_GeneralProcess
            // 
            this.gp_GeneralProcess.Controls.Add(this.chkSvSControl);
            this.gp_GeneralProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gp_GeneralProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gp_GeneralProcess.ForeColor = System.Drawing.Color.DarkRed;
            this.gp_GeneralProcess.Location = new System.Drawing.Point(0, 0);
            this.gp_GeneralProcess.Name = "gp_GeneralProcess";
            this.gp_GeneralProcess.Size = new System.Drawing.Size(216, 55);
            this.gp_GeneralProcess.TabIndex = 0;
            this.gp_GeneralProcess.TabStop = false;
            this.gp_GeneralProcess.Text = " Gerenal Process Parameters";
            // 
            // chkSvSControl
            // 
            this.chkSvSControl.AutoSize = true;
            this.chkSvSControl.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.chkSvSControl.ForeColor = System.Drawing.Color.Navy;
            this.chkSvSControl.Location = new System.Drawing.Point(48, 21);
            this.chkSvSControl.Name = "chkSvSControl";
            this.chkSvSControl.Size = new System.Drawing.Size(129, 19);
            this.chkSvSControl.TabIndex = 0;
            this.chkSvSControl.Text = "Enable SVS Control";
            this.chkSvSControl.UseVisualStyleBackColor = true;
            this.chkSvSControl.CheckedChanged += new System.EventHandler(this.chkSvSControl_CheckedChanged);
            // 
            // ucGeneralProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.gp_GeneralProcess);
            this.Name = "ucGeneralProcess";
            this.Size = new System.Drawing.Size(216, 55);
            this.gp_GeneralProcess.ResumeLayout(false);
            this.gp_GeneralProcess.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gp_GeneralProcess;
        private System.Windows.Forms.CheckBox chkSvSControl;
    }
}
