namespace SmartEyeControl_7.ApplicationGUI.ucCustomControl
{
    partial class uCHeartBeat
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
            if (obj_ApplicationController != null)
            {
                obj_ApplicationController.PropertyChanged -= Application_Controller_PropertyChanged;
            }
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
            this.grid_MeterHb = new System.Windows.Forms.DataGridView();
            this.MSN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PORT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConnectionTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastHeartBeat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IpConContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Connect = new System.Windows.Forms.ToolStripMenuItem();
            this.Disconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.GetConnectionInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.GetMeterInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.addToFavourites = new System.Windows.Forms.ToolStripMenuItem();
            this.timer_refreshList = new System.Windows.Forms.Timer(this.components);
            this.BckWorkerThread = new System.ComponentModel.BackgroundWorker();
            this.combo_HB_RefreshInterval = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_1 = new System.Windows.Forms.Label();
            this.lbl_RefreshTimeLeft = new System.Windows.Forms.Label();
            this.lbl_2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer_updateButton = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_totalConnections = new System.Windows.Forms.Label();
            this.btn_wakeup = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grid_MeterHb)).BeginInit();
            this.IpConContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // grid_MeterHb
            // 
            this.grid_MeterHb.AllowUserToAddRows = false;
            this.grid_MeterHb.AllowUserToDeleteRows = false;
            this.grid_MeterHb.AllowUserToResizeColumns = false;
            this.grid_MeterHb.AllowUserToResizeRows = false;
            this.grid_MeterHb.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grid_MeterHb.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MSN,
            this.IP,
            this.PORT,
            this.ConnectionTime,
            this.LastHeartBeat,
            this.Status});
            this.grid_MeterHb.ContextMenuStrip = this.IpConContextMenu;
            this.grid_MeterHb.Location = new System.Drawing.Point(21, 50);
            this.grid_MeterHb.MultiSelect = false;
            this.grid_MeterHb.Name = "grid_MeterHb";
            this.grid_MeterHb.ReadOnly = true;
            this.grid_MeterHb.RowHeadersWidth = 60;
            this.grid_MeterHb.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grid_MeterHb.Size = new System.Drawing.Size(783, 337);
            this.grid_MeterHb.TabIndex = 0;
            this.grid_MeterHb.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_MeterHb_CellClick);
            // 
            // MSN
            // 
            this.MSN.HeaderText = "Meter Serial Number";
            this.MSN.Name = "MSN";
            this.MSN.ReadOnly = true;
            this.MSN.Width = 150;
            // 
            // IP
            // 
            this.IP.HeaderText = "IP";
            this.IP.Name = "IP";
            this.IP.ReadOnly = true;
            // 
            // PORT
            // 
            this.PORT.HeaderText = "PORT";
            this.PORT.Name = "PORT";
            this.PORT.ReadOnly = true;
            this.PORT.Width = 60;
            // 
            // ConnectionTime
            // 
            this.ConnectionTime.HeaderText = "Connection Time";
            this.ConnectionTime.Name = "ConnectionTime";
            this.ConnectionTime.ReadOnly = true;
            this.ConnectionTime.Width = 150;
            // 
            // LastHeartBeat
            // 
            this.LastHeartBeat.HeaderText = "Last HeartBeat Time";
            this.LastHeartBeat.Name = "LastHeartBeat";
            this.LastHeartBeat.ReadOnly = true;
            this.LastHeartBeat.Width = 150;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // IpConContextMenu
            // 
            this.IpConContextMenu.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.IpConContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Connect,
            this.Disconnect,
            this.GetConnectionInfo,
            this.GetMeterInfo,
            this.addToFavourites});
            this.IpConContextMenu.Name = "IpConContextMenu";
            this.IpConContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.IpConContextMenu.Size = new System.Drawing.Size(198, 114);
            // 
            // Connect
            // 
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(197, 22);
            this.Connect.Text = "Connect";
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // Disconnect
            // 
            this.Disconnect.AutoToolTip = true;
            this.Disconnect.Name = "Disconnect";
            this.Disconnect.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.Disconnect.Size = new System.Drawing.Size(197, 22);
            this.Disconnect.Text = "Disconnect";
            this.Disconnect.ToolTipText = "Disconnect Physical Connection";
            this.Disconnect.Click += new System.EventHandler(this.DropConnMenuItem_Click);
            // 
            // GetConnectionInfo
            // 
            this.GetConnectionInfo.AutoToolTip = true;
            this.GetConnectionInfo.Name = "GetConnectionInfo";
            this.GetConnectionInfo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.GetConnectionInfo.Size = new System.Drawing.Size(197, 22);
            this.GetConnectionInfo.Text = "Connection Info";
            this.GetConnectionInfo.ToolTipText = "Display connected meter infomation in details";
            this.GetConnectionInfo.Click += new System.EventHandler(this.GetMeterInfoMenuItem_Click);
            // 
            // GetMeterInfo
            // 
            this.GetMeterInfo.Name = "GetMeterInfo";
            this.GetMeterInfo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.GetMeterInfo.Size = new System.Drawing.Size(197, 22);
            this.GetMeterInfo.Text = "Read Meter Info";
            this.GetMeterInfo.Click += new System.EventHandler(this.readMeterInfoMenuItem_Click);
            // 
            // addToFavourites
            // 
            this.addToFavourites.Name = "addToFavourites";
            this.addToFavourites.Size = new System.Drawing.Size(197, 22);
            this.addToFavourites.Text = "Add to Favourites";
            this.addToFavourites.Click += new System.EventHandler(this.addToFavourites_Click);
            // 
            // timer_refreshList
            // 
            this.timer_refreshList.Interval = 5000;
            this.timer_refreshList.Tick += new System.EventHandler(this.timer_refreshList_Tick);
            // 
            // BckWorkerThread
            // 
            this.BckWorkerThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BckWorker_ReadMeterInfo_DoEventHandler);
            this.BckWorkerThread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BckWorker_ReadMeterInfo_WorkCompleted);
            // 
            // combo_HB_RefreshInterval
            // 
            this.combo_HB_RefreshInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_HB_RefreshInterval.FormattingEnabled = true;
            this.combo_HB_RefreshInterval.Items.AddRange(new object[] {
            "5",
            "10",
            "20",
            "30",
            "45",
            "60",
            "90",
            "120",
            "150",
            "180"});
            this.combo_HB_RefreshInterval.Location = new System.Drawing.Point(259, 16);
            this.combo_HB_RefreshInterval.Name = "combo_HB_RefreshInterval";
            this.combo_HB_RefreshInterval.Size = new System.Drawing.Size(55, 21);
            this.combo_HB_RefreshInterval.TabIndex = 1;
            this.combo_HB_RefreshInterval.SelectedIndexChanged += new System.EventHandler(this.combo_HB_RefreshInterval_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(171, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Refresh Interval";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(320, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "seconds";
            // 
            // lbl_1
            // 
            this.lbl_1.AutoSize = true;
            this.lbl_1.BackColor = System.Drawing.Color.Transparent;
            this.lbl_1.Location = new System.Drawing.Point(6, 19);
            this.lbl_1.Name = "lbl_1";
            this.lbl_1.Size = new System.Drawing.Size(72, 13);
            this.lbl_1.TabIndex = 3;
            this.lbl_1.Text = "Refreshing in ";
            // 
            // lbl_RefreshTimeLeft
            // 
            this.lbl_RefreshTimeLeft.AutoSize = true;
            this.lbl_RefreshTimeLeft.BackColor = System.Drawing.Color.Transparent;
            this.lbl_RefreshTimeLeft.Location = new System.Drawing.Point(74, 20);
            this.lbl_RefreshTimeLeft.Name = "lbl_RefreshTimeLeft";
            this.lbl_RefreshTimeLeft.Size = new System.Drawing.Size(13, 13);
            this.lbl_RefreshTimeLeft.TabIndex = 3;
            this.lbl_RefreshTimeLeft.Text = "_";
            // 
            // lbl_2
            // 
            this.lbl_2.AutoSize = true;
            this.lbl_2.BackColor = System.Drawing.Color.Transparent;
            this.lbl_2.Location = new System.Drawing.Point(103, 20);
            this.lbl_2.Name = "lbl_2";
            this.lbl_2.Size = new System.Drawing.Size(47, 13);
            this.lbl_2.TabIndex = 3;
            this.lbl_2.Text = "seconds";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(389, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 31);
            this.button1.TabIndex = 4;
            this.button1.Text = "Update List";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(641, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Total Connections";
            // 
            // lbl_totalConnections
            // 
            this.lbl_totalConnections.AutoSize = true;
            this.lbl_totalConnections.BackColor = System.Drawing.Color.Transparent;
            this.lbl_totalConnections.Location = new System.Drawing.Point(740, 14);
            this.lbl_totalConnections.Name = "lbl_totalConnections";
            this.lbl_totalConnections.Size = new System.Drawing.Size(93, 13);
            this.lbl_totalConnections.TabIndex = 5;
            this.lbl_totalConnections.Text = "Total Connections";
            // 
            // btn_wakeup
            // 
            this.btn_wakeup.BackColor = System.Drawing.Color.Transparent;
            this.btn_wakeup.Location = new System.Drawing.Point(497, 10);
            this.btn_wakeup.Name = "btn_wakeup";
            this.btn_wakeup.Size = new System.Drawing.Size(102, 31);
            this.btn_wakeup.TabIndex = 6;
            this.btn_wakeup.Text = "Send Wakeup";
            this.btn_wakeup.UseVisualStyleBackColor = false;
            this.btn_wakeup.Click += new System.EventHandler(this.btn_wakeup_Click);
            // 
            // uCHeartBeat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(845, 396);
            this.Controls.Add(this.btn_wakeup);
            this.Controls.Add(this.lbl_totalConnections);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.grid_MeterHb);
            this.Controls.Add(this.lbl_RefreshTimeLeft);
            this.Controls.Add(this.lbl_2);
            this.Controls.Add(this.lbl_1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.combo_HB_RefreshInterval);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "uCHeartBeat";
            this.Text = "uCHeartBeat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.uCHeartBeat_FormClosing);
            this.Load += new System.EventHandler(this.uCHeartBeat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid_MeterHb)).EndInit();
            this.IpConContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grid_MeterHb;
        private System.Windows.Forms.Timer timer_refreshList;
        private System.Windows.Forms.ContextMenuStrip IpConContextMenu;
        private System.Windows.Forms.ToolStripMenuItem GetConnectionInfo;
        private System.Windows.Forms.ToolStripMenuItem GetMeterInfo;
        private System.Windows.Forms.ToolStripMenuItem Disconnect;
        private System.Windows.Forms.ToolStripMenuItem addToFavourites;
        private System.ComponentModel.BackgroundWorker BckWorkerThread;
        private System.Windows.Forms.ComboBox combo_HB_RefreshInterval;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_1;
        private System.Windows.Forms.Label lbl_RefreshTimeLeft;
        private System.Windows.Forms.Label lbl_2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer_updateButton;
        private System.Windows.Forms.ToolStripMenuItem Connect;
        private System.Windows.Forms.DataGridViewTextBoxColumn MSN;
        private System.Windows.Forms.DataGridViewTextBoxColumn IP;
        private System.Windows.Forms.DataGridViewTextBoxColumn PORT;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConnectionTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastHeartBeat;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_totalConnections;
        private System.Windows.Forms.Button btn_wakeup;
    }
}