namespace Communicator
{
    partial class MDCForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDCForm));
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.lbl_datetime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timer_RefreshAll = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearOutputScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundWorker_Statistics = new System.ComponentModel.BackgroundWorker();
            this.timer_LicenseValidation = new System.Windows.Forms.Timer(this.components);
            this.btn_Start_Server = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblKeepAliveCount = new System.Windows.Forms.Label();
            this.lblNonKeepAliveCount = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl_ConnectedCount = new System.Windows.Forms.Label();
            this.lbl_AllocatedCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblkam_succ_trans = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblkam_exp_trans = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblnkam_succ_trans = new System.Windows.Forms.Label();
            this.lblnkam_exp_trans = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.cb_Echo = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cb_GeneralLog = new System.Windows.Forms.CheckBox();
            this.linkConnections = new System.Windows.Forms.LinkLabel();
            this.timer_StartMDC = new System.Windows.Forms.Timer(this.components);
            this.btnShutDown = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // lbl_datetime
            // 
            this.lbl_datetime.AutoSize = true;
            this.lbl_datetime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_datetime.Location = new System.Drawing.Point(87, 29);
            this.lbl_datetime.Name = "lbl_datetime";
            this.lbl_datetime.Size = new System.Drawing.Size(30, 13);
            this.lbl_datetime.TabIndex = 5;
            this.lbl_datetime.Text = "Time";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Server UP Time:";
            // 
            // timer_RefreshAll
            // 
            this.timer_RefreshAll.Interval = 1000;
            this.timer_RefreshAll.Tick += new System.EventHandler(this.timer_AllocatedCount_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(309, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearOutputScreenToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // clearOutputScreenToolStripMenuItem
            // 
            this.clearOutputScreenToolStripMenuItem.Name = "clearOutputScreenToolStripMenuItem";
            this.clearOutputScreenToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.clearOutputScreenToolStripMenuItem.Text = "Clear Output Screen";
            this.clearOutputScreenToolStripMenuItem.Click += new System.EventHandler(this.clearOutputScreenToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // backgroundWorker_Statistics
            // 
            this.backgroundWorker_Statistics.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_Statistics_DoWork);
            this.backgroundWorker_Statistics.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_Statistics_RunWorkerCompleted);
            // 
            // timer_LicenseValidation
            // 
            this.timer_LicenseValidation.Enabled = true;
            this.timer_LicenseValidation.Interval = 86400000;
            this.timer_LicenseValidation.Tick += new System.EventHandler(this.timer_LiscenseValidation_Tick);
            // 
            // btn_Start_Server
            // 
            this.btn_Start_Server.Location = new System.Drawing.Point(9, 52);
            this.btn_Start_Server.Name = "btn_Start_Server";
            this.btn_Start_Server.Size = new System.Drawing.Size(75, 23);
            this.btn_Start_Server.TabIndex = 0;
            this.btn_Start_Server.Text = "Start";
            this.btn_Start_Server.UseVisualStyleBackColor = true;
            this.btn_Start_Server.Click += new System.EventHandler(this.btn_Start_Server_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblKeepAliveCount);
            this.groupBox1.Controls.Add(this.lblNonKeepAliveCount);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lbl_ConnectedCount);
            this.groupBox1.Controls.Add(this.lbl_AllocatedCount);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(9, 95);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(295, 55);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // lblKeepAliveCount
            // 
            this.lblKeepAliveCount.AutoSize = true;
            this.lblKeepAliveCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKeepAliveCount.ForeColor = System.Drawing.Color.Black;
            this.lblKeepAliveCount.Location = new System.Drawing.Point(250, 12);
            this.lblKeepAliveCount.Name = "lblKeepAliveCount";
            this.lblKeepAliveCount.Size = new System.Drawing.Size(19, 13);
            this.lblKeepAliveCount.TabIndex = 3;
            this.lblKeepAliveCount.Text = "__";
            // 
            // lblNonKeepAliveCount
            // 
            this.lblNonKeepAliveCount.AutoSize = true;
            this.lblNonKeepAliveCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNonKeepAliveCount.ForeColor = System.Drawing.Color.Black;
            this.lblNonKeepAliveCount.Location = new System.Drawing.Point(250, 32);
            this.lblNonKeepAliveCount.Name = "lblNonKeepAliveCount";
            this.lblNonKeepAliveCount.Size = new System.Drawing.Size(19, 13);
            this.lblNonKeepAliveCount.TabIndex = 4;
            this.lblNonKeepAliveCount.Text = "__";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Green;
            this.label6.Location = new System.Drawing.Point(157, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 15);
            this.label6.TabIndex = 1;
            this.label6.Text = "KeepAlive";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Green;
            this.label7.Location = new System.Drawing.Point(157, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 15);
            this.label7.TabIndex = 2;
            this.label7.Text = "NonKeepAlive";
            // 
            // lbl_ConnectedCount
            // 
            this.lbl_ConnectedCount.AutoSize = true;
            this.lbl_ConnectedCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ConnectedCount.ForeColor = System.Drawing.Color.Black;
            this.lbl_ConnectedCount.Location = new System.Drawing.Point(111, 11);
            this.lbl_ConnectedCount.Name = "lbl_ConnectedCount";
            this.lbl_ConnectedCount.Size = new System.Drawing.Size(19, 13);
            this.lbl_ConnectedCount.TabIndex = 0;
            this.lbl_ConnectedCount.Text = "__";
            // 
            // lbl_AllocatedCount
            // 
            this.lbl_AllocatedCount.AutoSize = true;
            this.lbl_AllocatedCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AllocatedCount.ForeColor = System.Drawing.Color.Black;
            this.lbl_AllocatedCount.Location = new System.Drawing.Point(111, 31);
            this.lbl_AllocatedCount.Name = "lbl_AllocatedCount";
            this.lbl_AllocatedCount.Size = new System.Drawing.Size(19, 13);
            this.lbl_AllocatedCount.TabIndex = 0;
            this.lbl_AllocatedCount.Text = "__";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Green;
            this.label4.Location = new System.Drawing.Point(6, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "Connected Count";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Green;
            this.label2.Location = new System.Drawing.Point(8, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Allocated Count";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblkam_succ_trans);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.lblkam_exp_trans);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox4.Location = new System.Drawing.Point(9, 154);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(138, 69);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Keep Alive Meters";
            // 
            // lblkam_succ_trans
            // 
            this.lblkam_succ_trans.AutoSize = true;
            this.lblkam_succ_trans.ForeColor = System.Drawing.Color.Black;
            this.lblkam_succ_trans.Location = new System.Drawing.Point(95, 46);
            this.lblkam_succ_trans.Name = "lblkam_succ_trans";
            this.lblkam_succ_trans.Size = new System.Drawing.Size(13, 13);
            this.lblkam_succ_trans.TabIndex = 8;
            this.lblkam_succ_trans.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(3, 32);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(68, 13);
            this.label13.TabIndex = 2;
            this.label13.Text = "Transactions";
            // 
            // lblkam_exp_trans
            // 
            this.lblkam_exp_trans.AutoSize = true;
            this.lblkam_exp_trans.ForeColor = System.Drawing.Color.Black;
            this.lblkam_exp_trans.Location = new System.Drawing.Point(95, 33);
            this.lblkam_exp_trans.Name = "lblkam_exp_trans";
            this.lblkam_exp_trans.Size = new System.Drawing.Size(13, 13);
            this.lblkam_exp_trans.TabIndex = 7;
            this.lblkam_exp_trans.Text = "0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(4, 45);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(57, 13);
            this.label15.TabIndex = 3;
            this.label15.Text = "Processed";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblnkam_succ_trans);
            this.groupBox3.Controls.Add(this.lblnkam_exp_trans);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox3.Location = new System.Drawing.Point(153, 154);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(151, 69);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Non Keep Alive Meters";
            // 
            // lblnkam_succ_trans
            // 
            this.lblnkam_succ_trans.AutoSize = true;
            this.lblnkam_succ_trans.ForeColor = System.Drawing.Color.Black;
            this.lblnkam_succ_trans.Location = new System.Drawing.Point(106, 44);
            this.lblnkam_succ_trans.Name = "lblnkam_succ_trans";
            this.lblnkam_succ_trans.Size = new System.Drawing.Size(13, 13);
            this.lblnkam_succ_trans.TabIndex = 11;
            this.lblnkam_succ_trans.Text = "0";
            // 
            // lblnkam_exp_trans
            // 
            this.lblnkam_exp_trans.AutoSize = true;
            this.lblnkam_exp_trans.ForeColor = System.Drawing.Color.Black;
            this.lblnkam_exp_trans.Location = new System.Drawing.Point(106, 31);
            this.lblnkam_exp_trans.Name = "lblnkam_exp_trans";
            this.lblnkam_exp_trans.Size = new System.Drawing.Size(13, 13);
            this.lblnkam_exp_trans.TabIndex = 10;
            this.lblnkam_exp_trans.Text = "0";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(14, 45);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(57, 13);
            this.label16.TabIndex = 5;
            this.label16.Text = "Processed";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(14, 31);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(68, 13);
            this.label17.TabIndex = 4;
            this.label17.Text = "Transactions";
            // 
            // cb_Echo
            // 
            this.cb_Echo.AutoSize = true;
            this.cb_Echo.Location = new System.Drawing.Point(19, 11);
            this.cb_Echo.Name = "cb_Echo";
            this.cb_Echo.Size = new System.Drawing.Size(108, 17);
            this.cb_Echo.TabIndex = 1;
            this.cb_Echo.Text = "Show/Hide Echo";
            this.cb_Echo.UseVisualStyleBackColor = true;
            this.cb_Echo.CheckedChanged += new System.EventHandler(this.cb_Echo_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cb_GeneralLog);
            this.groupBox2.Controls.Add(this.cb_Echo);
            this.groupBox2.Location = new System.Drawing.Point(90, 45);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(214, 32);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            // 
            // cb_GeneralLog
            // 
            this.cb_GeneralLog.AutoSize = true;
            this.cb_GeneralLog.Location = new System.Drawing.Point(126, 11);
            this.cb_GeneralLog.Name = "cb_GeneralLog";
            this.cb_GeneralLog.Size = new System.Drawing.Size(84, 17);
            this.cb_GeneralLog.TabIndex = 1;
            this.cb_GeneralLog.Text = "General Log";
            this.cb_GeneralLog.UseVisualStyleBackColor = true;
            this.cb_GeneralLog.CheckedChanged += new System.EventHandler(this.cb_GeneralLog_CheckedChanged);
            // 
            // linkConnections
            // 
            this.linkConnections.AutoSize = true;
            this.linkConnections.Location = new System.Drawing.Point(229, 81);
            this.linkConnections.Name = "linkConnections";
            this.linkConnections.Size = new System.Drawing.Size(69, 13);
            this.linkConnections.TabIndex = 3;
            this.linkConnections.TabStop = true;
            this.linkConnections.Text = " Connections";
            this.linkConnections.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkConnections_LinkClicked);
            this.linkConnections.Click += new System.EventHandler(this.linkConnections_Click);
            // 
            // timer_StartMDC
            // 
            this.timer_StartMDC.Enabled = true;
            this.timer_StartMDC.Tick += new System.EventHandler(this.timer_StartMDC_Tick);
            // 
            // btnShutDown
            // 
            this.btnShutDown.Location = new System.Drawing.Point(9, 75);
            this.btnShutDown.Name = "btnShutDown";
            this.btnShutDown.Size = new System.Drawing.Size(75, 23);
            this.btnShutDown.TabIndex = 18;
            this.btnShutDown.Text = "ShutDown";
            this.btnShutDown.UseVisualStyleBackColor = true;
            this.btnShutDown.Click += new System.EventHandler(this.btnShutDown_Click);
            // 
            // MDCForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 226);
            this.Controls.Add(this.btnShutDown);
            this.Controls.Add(this.linkConnections);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_datetime);
            this.Controls.Add(this.btn_Start_Server);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MDCForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MDC Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label lbl_datetime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer_RefreshAll;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearOutputScreenToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker backgroundWorker_Statistics;
        private System.Windows.Forms.Timer timer_LicenseValidation;
        private System.Windows.Forms.Button btn_Start_Server;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_ConnectedCount;
        private System.Windows.Forms.Label lbl_AllocatedCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblkam_succ_trans;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblkam_exp_trans;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblnkam_succ_trans;
        private System.Windows.Forms.Label lblnkam_exp_trans;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox cb_Echo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.LinkLabel linkConnections;
        private System.Windows.Forms.CheckBox cb_GeneralLog;
        private System.Windows.Forms.Timer timer_StartMDC;
        private System.Windows.Forms.Label lblKeepAliveCount;
        private System.Windows.Forms.Label lblNonKeepAliveCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnShutDown;
    }
}

