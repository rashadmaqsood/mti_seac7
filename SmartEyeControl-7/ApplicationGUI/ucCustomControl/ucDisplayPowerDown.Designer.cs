namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    partial class ucDisplayPowerDown
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
            this.gp_PWD_Flags = new System.Windows.Forms.GroupBox();
            this.fLP_Flags = new System.Windows.Forms.FlowLayoutPanel();
            this.chkAlwaysOn = new System.Windows.Forms.CheckBox();
            this.chkImmediatelyOff = new System.Windows.Forms.CheckBox();
            this.chkDisplayOnByButton = new System.Windows.Forms.CheckBox();
            this.chkOnTimeModeScrollCycle = new System.Windows.Forms.CheckBox();
            this.chkDisplayRepeat = new System.Windows.Forms.CheckBox();
            this.gp_PWDParam = new System.Windows.Forms.GroupBox();
            this.fLPanel_Main = new System.Windows.Forms.FlowLayoutPanel();
            this.gp_OffDelay = new System.Windows.Forms.GroupBox();
            this.nudOffDelay = new System.Windows.Forms.NumericUpDown();
            this.gp_OnTime = new System.Windows.Forms.GroupBox();
            this.rdbOnMin = new System.Windows.Forms.RadioButton();
            this.rdbOnSec = new System.Windows.Forms.RadioButton();
            this.nudOnTime = new System.Windows.Forms.NumericUpDown();
            this.gp_OffTime = new System.Windows.Forms.GroupBox();
            this.rdbOffMin = new System.Windows.Forms.RadioButton();
            this.rdbOffSec = new System.Windows.Forms.RadioButton();
            this.nudOffTime = new System.Windows.Forms.NumericUpDown();
            this.gp_PWD_Flags.SuspendLayout();
            this.fLP_Flags.SuspendLayout();
            this.gp_PWDParam.SuspendLayout();
            this.fLPanel_Main.SuspendLayout();
            this.gp_OffDelay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffDelay)).BeginInit();
            this.gp_OnTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOnTime)).BeginInit();
            this.gp_OffTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffTime)).BeginInit();
            this.SuspendLayout();
            // 
            // gp_PWD_Flags
            // 
            this.gp_PWD_Flags.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gp_PWD_Flags.Controls.Add(this.fLP_Flags);
            this.gp_PWD_Flags.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.gp_PWD_Flags.ForeColor = System.Drawing.Color.Navy;
            this.gp_PWD_Flags.Location = new System.Drawing.Point(3, 162);
            this.gp_PWD_Flags.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.gp_PWD_Flags.Name = "gp_PWD_Flags";
            this.gp_PWD_Flags.Size = new System.Drawing.Size(324, 120);
            this.gp_PWD_Flags.TabIndex = 0;
            this.gp_PWD_Flags.TabStop = false;
            this.gp_PWD_Flags.Text = "Power Down Display Flags";
            // 
            // fLP_Flags
            // 
            this.fLP_Flags.AutoSize = true;
            this.fLP_Flags.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fLP_Flags.Controls.Add(this.chkAlwaysOn);
            this.fLP_Flags.Controls.Add(this.chkImmediatelyOff);
            this.fLP_Flags.Controls.Add(this.chkDisplayOnByButton);
            this.fLP_Flags.Controls.Add(this.chkOnTimeModeScrollCycle);
            this.fLP_Flags.Controls.Add(this.chkDisplayRepeat);
            this.fLP_Flags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fLP_Flags.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLP_Flags.Location = new System.Drawing.Point(3, 19);
            this.fLP_Flags.Name = "fLP_Flags";
            this.fLP_Flags.Size = new System.Drawing.Size(318, 98);
            this.fLP_Flags.TabIndex = 2;
            // 
            // chkAlwaysOn
            // 
            this.chkAlwaysOn.AutoSize = true;
            this.chkAlwaysOn.Location = new System.Drawing.Point(3, 0);
            this.chkAlwaysOn.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.chkAlwaysOn.Name = "chkAlwaysOn";
            this.chkAlwaysOn.Size = new System.Drawing.Size(83, 19);
            this.chkAlwaysOn.TabIndex = 0;
            this.chkAlwaysOn.Text = "Always On";
            this.chkAlwaysOn.UseVisualStyleBackColor = true;
            this.chkAlwaysOn.CheckedChanged += new System.EventHandler(this.chkOnTimeModeScrollCycle_CheckedChanged);
            // 
            // chkImmediatelyOff
            // 
            this.chkImmediatelyOff.AutoSize = true;
            this.chkImmediatelyOff.Location = new System.Drawing.Point(3, 19);
            this.chkImmediatelyOff.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.chkImmediatelyOff.Name = "chkImmediatelyOff";
            this.chkImmediatelyOff.Size = new System.Drawing.Size(115, 19);
            this.chkImmediatelyOff.TabIndex = 1;
            this.chkImmediatelyOff.Text = "Immediately Off";
            this.chkImmediatelyOff.UseVisualStyleBackColor = true;
            this.chkImmediatelyOff.CheckedChanged += new System.EventHandler(this.chkOnTimeModeScrollCycle_CheckedChanged);
            // 
            // chkDisplayOnByButton
            // 
            this.chkDisplayOnByButton.AutoSize = true;
            this.chkDisplayOnByButton.Location = new System.Drawing.Point(3, 38);
            this.chkDisplayOnByButton.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.chkDisplayOnByButton.Name = "chkDisplayOnByButton";
            this.chkDisplayOnByButton.Size = new System.Drawing.Size(139, 19);
            this.chkDisplayOnByButton.TabIndex = 2;
            this.chkDisplayOnByButton.Text = "Display On By Button";
            this.chkDisplayOnByButton.UseVisualStyleBackColor = true;
            this.chkDisplayOnByButton.CheckedChanged += new System.EventHandler(this.chkOnTimeModeScrollCycle_CheckedChanged);
            // 
            // chkOnTimeModeScrollCycle
            // 
            this.chkOnTimeModeScrollCycle.AutoSize = true;
            this.chkOnTimeModeScrollCycle.Location = new System.Drawing.Point(3, 57);
            this.chkOnTimeModeScrollCycle.Margin = new System.Windows.Forms.Padding(3, 0, 145, 0);
            this.chkOnTimeModeScrollCycle.Name = "chkOnTimeModeScrollCycle";
            this.chkOnTimeModeScrollCycle.Size = new System.Drawing.Size(170, 19);
            this.chkOnTimeModeScrollCycle.TabIndex = 3;
            this.chkOnTimeModeScrollCycle.Text = "On Time Mode Scroll Cycle";
            this.chkOnTimeModeScrollCycle.UseVisualStyleBackColor = true;
            this.chkOnTimeModeScrollCycle.CheckedChanged += new System.EventHandler(this.chkOnTimeModeScrollCycle_CheckedChanged);
            // 
            // chkDisplayRepeat
            // 
            this.chkDisplayRepeat.AutoSize = true;
            this.chkDisplayRepeat.Location = new System.Drawing.Point(3, 76);
            this.chkDisplayRepeat.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.chkDisplayRepeat.Name = "chkDisplayRepeat";
            this.chkDisplayRepeat.Size = new System.Drawing.Size(106, 19);
            this.chkDisplayRepeat.TabIndex = 4;
            this.chkDisplayRepeat.Text = "Display Repeat";
            this.chkDisplayRepeat.UseVisualStyleBackColor = true;
            this.chkDisplayRepeat.CheckedChanged += new System.EventHandler(this.chkOnTimeModeScrollCycle_CheckedChanged);
            // 
            // gp_PWDParam
            // 
            this.gp_PWDParam.AutoSize = true;
            this.gp_PWDParam.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gp_PWDParam.BackColor = System.Drawing.Color.Transparent;
            this.gp_PWDParam.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gp_PWDParam.Controls.Add(this.fLPanel_Main);
            this.gp_PWDParam.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gp_PWDParam.ForeColor = System.Drawing.Color.DarkRed;
            this.gp_PWDParam.Location = new System.Drawing.Point(0, 0);
            this.gp_PWDParam.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.gp_PWDParam.MaximumSize = new System.Drawing.Size(335, 400);
            this.gp_PWDParam.MinimumSize = new System.Drawing.Size(335, 305);
            this.gp_PWDParam.Name = "gp_PWDParam";
            this.gp_PWDParam.Size = new System.Drawing.Size(335, 305);
            this.gp_PWDParam.TabIndex = 1;
            this.gp_PWDParam.TabStop = false;
            this.gp_PWDParam.Text = "Display Power Down Parameters";
            // 
            // fLPanel_Main
            // 
            this.fLPanel_Main.Controls.Add(this.gp_OffDelay);
            this.fLPanel_Main.Controls.Add(this.gp_OnTime);
            this.fLPanel_Main.Controls.Add(this.gp_OffTime);
            this.fLPanel_Main.Controls.Add(this.gp_PWD_Flags);
            this.fLPanel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fLPanel_Main.Location = new System.Drawing.Point(3, 16);
            this.fLPanel_Main.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.fLPanel_Main.Name = "fLPanel_Main";
            this.fLPanel_Main.Size = new System.Drawing.Size(329, 286);
            this.fLPanel_Main.TabIndex = 0;
            // 
            // gp_OffDelay
            // 
            this.gp_OffDelay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.gp_OffDelay.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gp_OffDelay.Controls.Add(this.nudOffDelay);
            this.gp_OffDelay.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.gp_OffDelay.ForeColor = System.Drawing.Color.Navy;
            this.gp_OffDelay.Location = new System.Drawing.Point(3, 0);
            this.gp_OffDelay.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.gp_OffDelay.Name = "gp_OffDelay";
            this.gp_OffDelay.Size = new System.Drawing.Size(323, 54);
            this.gp_OffDelay.TabIndex = 2;
            this.gp_OffDelay.TabStop = false;
            this.gp_OffDelay.Text = "Off Delay";
            // 
            // nudOffDelay
            // 
            this.nudOffDelay.Location = new System.Drawing.Point(9, 19);
            this.nudOffDelay.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudOffDelay.Name = "nudOffDelay";
            this.nudOffDelay.Size = new System.Drawing.Size(120, 23);
            this.nudOffDelay.TabIndex = 3;
            this.nudOffDelay.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudOffDelay.ValueChanged += new System.EventHandler(this.nudOffDelay_ValueChanged);
            // 
            // gp_OnTime
            // 
            this.gp_OnTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.gp_OnTime.Controls.Add(this.rdbOnMin);
            this.gp_OnTime.Controls.Add(this.rdbOnSec);
            this.gp_OnTime.Controls.Add(this.nudOnTime);
            this.gp_OnTime.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.gp_OnTime.ForeColor = System.Drawing.Color.Navy;
            this.gp_OnTime.Location = new System.Drawing.Point(3, 54);
            this.gp_OnTime.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.gp_OnTime.Name = "gp_OnTime";
            this.gp_OnTime.Size = new System.Drawing.Size(323, 54);
            this.gp_OnTime.TabIndex = 2;
            this.gp_OnTime.TabStop = false;
            this.gp_OnTime.Text = "On Time";
            // 
            // rdbOnMin
            // 
            this.rdbOnMin.AutoSize = true;
            this.rdbOnMin.Location = new System.Drawing.Point(185, 19);
            this.rdbOnMin.Name = "rdbOnMin";
            this.rdbOnMin.Size = new System.Drawing.Size(46, 19);
            this.rdbOnMin.TabIndex = 5;
            this.rdbOnMin.Text = "Min";
            this.rdbOnMin.UseVisualStyleBackColor = true;
            this.rdbOnMin.Click += new System.EventHandler(this.ucDisplayPowerDown_Click);
            // 
            // rdbOnSec
            // 
            this.rdbOnSec.AutoSize = true;
            this.rdbOnSec.Checked = true;
            this.rdbOnSec.Location = new System.Drawing.Point(135, 19);
            this.rdbOnSec.Name = "rdbOnSec";
            this.rdbOnSec.Size = new System.Drawing.Size(43, 19);
            this.rdbOnSec.TabIndex = 3;
            this.rdbOnSec.TabStop = true;
            this.rdbOnSec.Text = "Sec";
            this.rdbOnSec.UseVisualStyleBackColor = true;
            this.rdbOnSec.Click += new System.EventHandler(this.ucDisplayPowerDown_Click);
            // 
            // nudOnTime
            // 
            this.nudOnTime.Location = new System.Drawing.Point(9, 19);
            this.nudOnTime.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudOnTime.Name = "nudOnTime";
            this.nudOnTime.Size = new System.Drawing.Size(120, 23);
            this.nudOnTime.TabIndex = 4;
            this.nudOnTime.Value = new decimal(new int[] {
            45,
            0,
            0,
            0});
            this.nudOnTime.ValueChanged += new System.EventHandler(this.nudOffDelay_ValueChanged);
            // 
            // gp_OffTime
            // 
            this.gp_OffTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.gp_OffTime.Controls.Add(this.rdbOffMin);
            this.gp_OffTime.Controls.Add(this.rdbOffSec);
            this.gp_OffTime.Controls.Add(this.nudOffTime);
            this.gp_OffTime.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.gp_OffTime.ForeColor = System.Drawing.Color.Navy;
            this.gp_OffTime.Location = new System.Drawing.Point(3, 108);
            this.gp_OffTime.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.gp_OffTime.Name = "gp_OffTime";
            this.gp_OffTime.Size = new System.Drawing.Size(323, 54);
            this.gp_OffTime.TabIndex = 1;
            this.gp_OffTime.TabStop = false;
            this.gp_OffTime.Text = "Off Time";
            // 
            // rdbOffMin
            // 
            this.rdbOffMin.AutoSize = true;
            this.rdbOffMin.Location = new System.Drawing.Point(185, 19);
            this.rdbOffMin.Name = "rdbOffMin";
            this.rdbOffMin.Size = new System.Drawing.Size(46, 19);
            this.rdbOffMin.TabIndex = 7;
            this.rdbOffMin.Text = "Min";
            this.rdbOffMin.UseVisualStyleBackColor = true;
            this.rdbOffMin.Click += new System.EventHandler(this.rdbOffMin_Click);
            // 
            // rdbOffSec
            // 
            this.rdbOffSec.AutoSize = true;
            this.rdbOffSec.Checked = true;
            this.rdbOffSec.Location = new System.Drawing.Point(135, 19);
            this.rdbOffSec.Name = "rdbOffSec";
            this.rdbOffSec.Size = new System.Drawing.Size(43, 19);
            this.rdbOffSec.TabIndex = 6;
            this.rdbOffSec.TabStop = true;
            this.rdbOffSec.Text = "Sec";
            this.rdbOffSec.UseVisualStyleBackColor = true;
            this.rdbOffSec.Click += new System.EventHandler(this.rdbOffMin_Click);
            // 
            // nudOffTime
            // 
            this.nudOffTime.Location = new System.Drawing.Point(9, 19);
            this.nudOffTime.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudOffTime.Name = "nudOffTime";
            this.nudOffTime.Size = new System.Drawing.Size(120, 23);
            this.nudOffTime.TabIndex = 5;
            this.nudOffTime.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.nudOffTime.ValueChanged += new System.EventHandler(this.nudOffDelay_ValueChanged);
            // 
            // ucDisplayPowerDown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.gp_PWDParam);
            this.Name = "ucDisplayPowerDown";
            this.Size = new System.Drawing.Size(338, 305);
            this.Load += new System.EventHandler(this.ucDisplayPowerDown_Load);
            this.Click += new System.EventHandler(this.ucDisplayPowerDown_Click);
            this.gp_PWD_Flags.ResumeLayout(false);
            this.gp_PWD_Flags.PerformLayout();
            this.fLP_Flags.ResumeLayout(false);
            this.fLP_Flags.PerformLayout();
            this.gp_PWDParam.ResumeLayout(false);
            this.fLPanel_Main.ResumeLayout(false);
            this.gp_OffDelay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudOffDelay)).EndInit();
            this.gp_OnTime.ResumeLayout(false);
            this.gp_OnTime.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOnTime)).EndInit();
            this.gp_OffTime.ResumeLayout(false);
            this.gp_OffTime.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOffTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gp_PWD_Flags;
        private System.Windows.Forms.CheckBox chkDisplayRepeat;
        private System.Windows.Forms.CheckBox chkOnTimeModeScrollCycle;
        private System.Windows.Forms.CheckBox chkDisplayOnByButton;
        private System.Windows.Forms.CheckBox chkImmediatelyOff;
        private System.Windows.Forms.CheckBox chkAlwaysOn;
        private System.Windows.Forms.GroupBox gp_PWDParam;
        private System.Windows.Forms.GroupBox gp_OffDelay;
        private System.Windows.Forms.NumericUpDown nudOffDelay;
        private System.Windows.Forms.GroupBox gp_OnTime;
        private System.Windows.Forms.RadioButton rdbOnMin;
        private System.Windows.Forms.RadioButton rdbOnSec;
        private System.Windows.Forms.NumericUpDown nudOnTime;
        private System.Windows.Forms.GroupBox gp_OffTime;
        private System.Windows.Forms.RadioButton rdbOffMin;
        private System.Windows.Forms.RadioButton rdbOffSec;
        private System.Windows.Forms.NumericUpDown nudOffTime;
        private System.Windows.Forms.FlowLayoutPanel fLP_Flags;
        private System.Windows.Forms.FlowLayoutPanel fLPanel_Main;
    }
}
