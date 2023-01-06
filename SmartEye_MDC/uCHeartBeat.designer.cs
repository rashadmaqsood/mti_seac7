namespace Communicator
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
            this.timer_refreshList = new System.Windows.Forms.Timer(this.components);
            this.BckWorkerThread = new System.ComponentModel.BackgroundWorker();
            this.lbl_1 = new System.Windows.Forms.Label();
            this.lbl_RefreshTimeLeft = new System.Windows.Forms.Label();
            this.lbl_2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_totalConnections = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_First = new System.Windows.Forms.Button();
            this.btn_Previous = new System.Windows.Forms.Button();
            this.btn_Next = new System.Windows.Forms.Button();
            this.btn_Last = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grid_MeterHb)).BeginInit();
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
            this.grid_MeterHb.Location = new System.Drawing.Point(21, 50);
            this.grid_MeterHb.MultiSelect = false;
            this.grid_MeterHb.Name = "grid_MeterHb";
            this.grid_MeterHb.ReadOnly = true;
            this.grid_MeterHb.RowHeadersWidth = 60;
            this.grid_MeterHb.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grid_MeterHb.Size = new System.Drawing.Size(783, 513);
            this.grid_MeterHb.TabIndex = 0;
            this.grid_MeterHb.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_MeterHb_CellContentClick);
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
            // timer_refreshList
            // 
            this.timer_refreshList.Interval = 15000;
            this.timer_refreshList.Tick += new System.EventHandler(this.timer_refreshList_Tick);
            // 
            // lbl_1
            // 
            this.lbl_1.AutoSize = true;
            this.lbl_1.Location = new System.Drawing.Point(23, 23);
            this.lbl_1.Name = "lbl_1";
            this.lbl_1.Size = new System.Drawing.Size(72, 13);
            this.lbl_1.TabIndex = 3;
            this.lbl_1.Text = "Refreshing in ";
            // 
            // lbl_RefreshTimeLeft
            // 
            this.lbl_RefreshTimeLeft.AutoSize = true;
            this.lbl_RefreshTimeLeft.Location = new System.Drawing.Point(95, 23);
            this.lbl_RefreshTimeLeft.Name = "lbl_RefreshTimeLeft";
            this.lbl_RefreshTimeLeft.Size = new System.Drawing.Size(13, 13);
            this.lbl_RefreshTimeLeft.TabIndex = 3;
            this.lbl_RefreshTimeLeft.Text = "_";
            // 
            // lbl_2
            // 
            this.lbl_2.AutoSize = true;
            this.lbl_2.Location = new System.Drawing.Point(116, 23);
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
            this.button1.Location = new System.Drawing.Point(459, 10);
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
            this.label3.Location = new System.Drawing.Point(617, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Total Connections";
            // 
            // lbl_totalConnections
            // 
            this.lbl_totalConnections.AutoSize = true;
            this.lbl_totalConnections.Location = new System.Drawing.Point(716, 23);
            this.lbl_totalConnections.Name = "lbl_totalConnections";
            this.lbl_totalConnections.Size = new System.Drawing.Size(93, 13);
            this.lbl_totalConnections.TabIndex = 5;
            this.lbl_totalConnections.Text = "Total Connections";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "10",
            "15",
            "30",
            "60",
            "120",
            "180",
            "300"});
            this.comboBox1.Location = new System.Drawing.Point(345, 20);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(58, 21);
            this.comboBox1.TabIndex = 6;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(253, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Change Interval";
            // 
            // btn_First
            // 
            this.btn_First.Location = new System.Drawing.Point(290, 569);
            this.btn_First.Name = "btn_First";
            this.btn_First.Size = new System.Drawing.Size(33, 23);
            this.btn_First.TabIndex = 7;
            this.btn_First.Text = "|<";
            this.btn_First.UseVisualStyleBackColor = true;
            this.btn_First.Click += new System.EventHandler(this.btn_First_Click);
            // 
            // btn_Previous
            // 
            this.btn_Previous.Location = new System.Drawing.Point(345, 569);
            this.btn_Previous.Name = "btn_Previous";
            this.btn_Previous.Size = new System.Drawing.Size(33, 23);
            this.btn_Previous.TabIndex = 7;
            this.btn_Previous.Text = "<";
            this.btn_Previous.UseVisualStyleBackColor = true;
            this.btn_Previous.Click += new System.EventHandler(this.btn_Previous_Click);
            // 
            // btn_Next
            // 
            this.btn_Next.Location = new System.Drawing.Point(400, 569);
            this.btn_Next.Name = "btn_Next";
            this.btn_Next.Size = new System.Drawing.Size(33, 23);
            this.btn_Next.TabIndex = 7;
            this.btn_Next.Text = ">";
            this.btn_Next.UseVisualStyleBackColor = true;
            this.btn_Next.Click += new System.EventHandler(this.btn_Next_Click);
            // 
            // btn_Last
            // 
            this.btn_Last.Location = new System.Drawing.Point(455, 569);
            this.btn_Last.Name = "btn_Last";
            this.btn_Last.Size = new System.Drawing.Size(33, 23);
            this.btn_Last.TabIndex = 7;
            this.btn_Last.Text = ">|";
            this.btn_Last.UseVisualStyleBackColor = true;
            this.btn_Last.Click += new System.EventHandler(this.btn_Last_Click);
            // 
            // uCHeartBeat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 604);
            this.Controls.Add(this.btn_Last);
            this.Controls.Add(this.btn_Next);
            this.Controls.Add(this.btn_Previous);
            this.Controls.Add(this.btn_First);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.lbl_totalConnections);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.grid_MeterHb);
            this.Controls.Add(this.lbl_RefreshTimeLeft);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_2);
            this.Controls.Add(this.lbl_1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "uCHeartBeat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "uCHeartBeat";
            this.Load += new System.EventHandler(this.uCHeartBeat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid_MeterHb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grid_MeterHb;
        private System.Windows.Forms.Timer timer_refreshList;
        private System.ComponentModel.BackgroundWorker BckWorkerThread;
        private System.Windows.Forms.Label lbl_1;
        private System.Windows.Forms.Label lbl_RefreshTimeLeft;
        private System.Windows.Forms.Label lbl_2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn MSN;
        private System.Windows.Forms.DataGridViewTextBoxColumn IP;
        private System.Windows.Forms.DataGridViewTextBoxColumn PORT;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConnectionTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastHeartBeat;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_totalConnections;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_First;
        private System.Windows.Forms.Button btn_Previous;
        private System.Windows.Forms.Button btn_Next;
        private System.Windows.Forms.Button btn_Last;
    }
}