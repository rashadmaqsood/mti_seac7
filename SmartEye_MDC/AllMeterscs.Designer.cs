namespace Communicator
{
    partial class AllMeterscs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AllMeterscs));
            this.btnUpdate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dtg = new System.Windows.Forms.DataGridView();
            this.clmMsn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmIp1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPort1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmCommunicationTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmHB1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmStatus1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmLog1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.chkSingleSearch = new System.Windows.Forms.CheckBox();
            this.cmbSearchOn = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.ComboBox();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotalConnection = new System.Windows.Forms.Label();
            this.cmbAutoRefresh = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblRefreshTimeLeft = new System.Windows.Forms.Label();
            this.flpFilter = new System.Windows.Forms.FlowLayoutPanel();
            this.chkMultiSelect = new System.Windows.Forms.CheckBox();
            this.btnAddToFilter = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(223, 10);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Refresh";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Filter:";
            // 
            // dtg
            // 
            this.dtg.AllowUserToAddRows = false;
            this.dtg.AllowUserToDeleteRows = false;
            this.dtg.AllowUserToResizeRows = false;
            this.dtg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmMsn1,
            this.clmType,
            this.clmIp1,
            this.clmPort1,
            this.clmCommunicationTime,
            this.clmHB1,
            this.clmStatus1,
            this.clmLog1});
            this.dtg.Location = new System.Drawing.Point(-1, 89);
            this.dtg.Name = "dtg";
            this.dtg.Size = new System.Drawing.Size(1052, 375);
            this.dtg.TabIndex = 4;
            this.dtg.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtg_CellContentClick);
            this.dtg.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.dtg_CellContextMenuStripNeeded);
            // 
            // clmMsn1
            // 
            this.clmMsn1.DataPropertyName = "msn";
            this.clmMsn1.HeaderText = "Msn";
            this.clmMsn1.Name = "clmMsn1";
            this.clmMsn1.Width = 70;
            // 
            // clmType
            // 
            this.clmType.DataPropertyName = "type";
            this.clmType.HeaderText = "Type";
            this.clmType.Name = "clmType";
            this.clmType.Width = 80;
            // 
            // clmIp1
            // 
            this.clmIp1.DataPropertyName = "ip";
            this.clmIp1.HeaderText = "IP";
            this.clmIp1.Name = "clmIp1";
            this.clmIp1.Width = 80;
            // 
            // clmPort1
            // 
            this.clmPort1.DataPropertyName = "port";
            this.clmPort1.HeaderText = "Port";
            this.clmPort1.Name = "clmPort1";
            this.clmPort1.Width = 38;
            // 
            // clmCommunicationTime
            // 
            this.clmCommunicationTime.DataPropertyName = "connectionTime";
            this.clmCommunicationTime.HeaderText = "Communication Time";
            this.clmCommunicationTime.Name = "clmCommunicationTime";
            this.clmCommunicationTime.Width = 130;
            // 
            // clmHB1
            // 
            this.clmHB1.DataPropertyName = "last_HB_time";
            this.clmHB1.HeaderText = "Last Heart Beat Time";
            this.clmHB1.Name = "clmHB1";
            this.clmHB1.Width = 130;
            // 
            // clmStatus1
            // 
            this.clmStatus1.DataPropertyName = "status";
            this.clmStatus1.HeaderText = "Status";
            this.clmStatus1.Name = "clmStatus1";
            this.clmStatus1.Width = 400;
            // 
            // clmLog1
            // 
            this.clmLog1.HeaderText = "Log";
            this.clmLog1.Name = "clmLog1";
            this.clmLog1.Text = "<<View Log>>";
            this.clmLog1.UseColumnTextForButtonValue = true;
            this.clmLog1.Width = 80;
            // 
            // chkSingleSearch
            // 
            this.chkSingleSearch.AutoSize = true;
            this.chkSingleSearch.Location = new System.Drawing.Point(72, 65);
            this.chkSingleSearch.Name = "chkSingleSearch";
            this.chkSingleSearch.Size = new System.Drawing.Size(92, 17);
            this.chkSingleSearch.TabIndex = 6;
            this.chkSingleSearch.Text = "Single Search";
            this.chkSingleSearch.UseVisualStyleBackColor = true;
            this.chkSingleSearch.CheckedChanged += new System.EventHandler(this.chkSingleSearch_CheckedChanged);
            // 
            // cmbSearchOn
            // 
            this.cmbSearchOn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchOn.FormattingEnabled = true;
            this.cmbSearchOn.Items.AddRange(new object[] {
            "Meter Serial Number",
            "Meter Type"});
            this.cmbSearchOn.Location = new System.Drawing.Point(72, 12);
            this.cmbSearchOn.Name = "cmbSearchOn";
            this.cmbSearchOn.Size = new System.Drawing.Size(145, 21);
            this.cmbSearchOn.TabIndex = 7;
            this.cmbSearchOn.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Search on";
            // 
            // txtFilter
            // 
            this.txtFilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtFilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtFilter.FormattingEnabled = true;
            this.txtFilter.Items.AddRange(new object[] {
            "Meter Serial Number,",
            "Meter Type"});
            this.txtFilter.Location = new System.Drawing.Point(72, 39);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(145, 21);
            this.txtFilter.TabIndex = 9;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // btnClearAll
            // 
            this.btnClearAll.Location = new System.Drawing.Point(303, 10);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(75, 23);
            this.btnClearAll.TabIndex = 10;
            this.btnClearAll.Text = "Clear All";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(848, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "Total Connection:";
            // 
            // lblTotalConnection
            // 
            this.lblTotalConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalConnection.AutoSize = true;
            this.lblTotalConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalConnection.Location = new System.Drawing.Point(983, 65);
            this.lblTotalConnection.Name = "lblTotalConnection";
            this.lblTotalConnection.Size = new System.Drawing.Size(23, 16);
            this.lblTotalConnection.TabIndex = 12;
            this.lblTotalConnection.Text = "---";
            // 
            // cmbAutoRefresh
            // 
            this.cmbAutoRefresh.FormattingEnabled = true;
            this.cmbAutoRefresh.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "20",
            "30",
            "60",
            "120",
            "200",
            "300"});
            this.cmbAutoRefresh.Location = new System.Drawing.Point(964, 5);
            this.cmbAutoRefresh.Name = "cmbAutoRefresh";
            this.cmbAutoRefresh.Size = new System.Drawing.Size(46, 21);
            this.cmbAutoRefresh.TabIndex = 13;
            this.cmbAutoRefresh.SelectedIndexChanged += new System.EventHandler(this.cmbAutoRefresh_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(848, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Auto Refresh Interval:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1016, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Sec";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(848, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Refreshing In:";
            // 
            // lblRefreshTimeLeft
            // 
            this.lblRefreshTimeLeft.AutoSize = true;
            this.lblRefreshTimeLeft.Location = new System.Drawing.Point(927, 37);
            this.lblRefreshTimeLeft.Name = "lblRefreshTimeLeft";
            this.lblRefreshTimeLeft.Size = new System.Drawing.Size(13, 13);
            this.lblRefreshTimeLeft.TabIndex = 17;
            this.lblRefreshTimeLeft.Text = "--";
            // 
            // flpFilter
            // 
            this.flpFilter.AutoScroll = true;
            this.flpFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpFilter.Location = new System.Drawing.Point(384, 10);
            this.flpFilter.Name = "flpFilter";
            this.flpFilter.Size = new System.Drawing.Size(458, 69);
            this.flpFilter.TabIndex = 18;
            this.flpFilter.Visible = false;
            // 
            // chkMultiSelect
            // 
            this.chkMultiSelect.AutoSize = true;
            this.chkMultiSelect.Location = new System.Drawing.Point(170, 66);
            this.chkMultiSelect.Name = "chkMultiSelect";
            this.chkMultiSelect.Size = new System.Drawing.Size(136, 17);
            this.chkMultiSelect.TabIndex = 19;
            this.chkMultiSelect.Text = "Multi Select Filter Mode";
            this.chkMultiSelect.UseVisualStyleBackColor = true;
            this.chkMultiSelect.CheckedChanged += new System.EventHandler(this.chkMultiSelect_CheckedChanged);
            // 
            // btnAddToFilter
            // 
            this.btnAddToFilter.Location = new System.Drawing.Point(223, 39);
            this.btnAddToFilter.Name = "btnAddToFilter";
            this.btnAddToFilter.Size = new System.Drawing.Size(75, 23);
            this.btnAddToFilter.TabIndex = 20;
            this.btnAddToFilter.Text = "Add To Filter";
            this.btnAddToFilter.UseVisualStyleBackColor = true;
            this.btnAddToFilter.Click += new System.EventHandler(this.btnAddToFilter_Click);
            // 
            // AllMeterscs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1051, 464);
            this.Controls.Add(this.btnAddToFilter);
            this.Controls.Add(this.chkMultiSelect);
            this.Controls.Add(this.flpFilter);
            this.Controls.Add(this.lblRefreshTimeLeft);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbAutoRefresh);
            this.Controls.Add(this.lblTotalConnection);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnClearAll);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbSearchOn);
            this.Controls.Add(this.chkSingleSearch);
            this.Controls.Add(this.dtg);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnUpdate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AllMeterscs";
            this.Text = "Connected Meter List";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AllMeterscs_FormClosing);
            this.Load += new System.EventHandler(this.AllMeterscs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dtg;
        private System.Windows.Forms.CheckBox chkSingleSearch;
        private System.Windows.Forms.ComboBox cmbSearchOn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox txtFilter;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotalConnection;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmMsn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmType;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmIp1;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPort1;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCommunicationTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmHB1;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmStatus1;
        private System.Windows.Forms.DataGridViewButtonColumn clmLog1;
        private System.Windows.Forms.ComboBox cmbAutoRefresh;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblRefreshTimeLeft;
        private System.Windows.Forms.FlowLayoutPanel flpFilter;
        private System.Windows.Forms.CheckBox chkMultiSelect;
        private System.Windows.Forms.Button btnAddToFilter;
    }
}