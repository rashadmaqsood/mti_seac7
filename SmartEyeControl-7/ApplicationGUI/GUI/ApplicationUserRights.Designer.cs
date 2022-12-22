namespace AccurateOptocomSoftware.ApplicationGUI.GUI
{
    partial class ApplicationUserRights
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
            this.trvQuantities = new System.Windows.Forms.TreeView();
            this.dtgRights = new System.Windows.Forms.DataGridView();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSaveChanges = new System.Windows.Forms.Button();
            this.lbl_Role = new System.Windows.Forms.Label();
            this.txtIdentifier = new System.Windows.Forms.TextBox();
            this.lbl_Identifier = new System.Windows.Forms.Label();
            this.cmRole = new System.Windows.Forms.ComboBox();
            this.Context_Rights = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.readToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.writeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_header = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.rbElectricity = new System.Windows.Forms.RadioButton();
            this.gbMeterDevice = new System.Windows.Forms.GroupBox();
            this.rbFusion = new System.Windows.Forms.RadioButton();
            this.rbRFP135 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.rbPublic = new System.Windows.Forms.RadioButton();
            this.gbRfp135ServerSap = new System.Windows.Forms.GroupBox();
            this.rbManagment = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButton8 = new System.Windows.Forms.RadioButton();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.flpAssociationSettingRfp = new System.Windows.Forms.FlowLayoutPanel();
            this.flpAssociationSettingFusion = new System.Windows.Forms.FlowLayoutPanel();
            this.flpAssociationSetting = new System.Windows.Forms.FlowLayoutPanel();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.StartMsn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndMsn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRights)).BeginInit();
            this.Context_Rights.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.gbMeterDevice.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbRfp135ServerSap.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.flpAssociationSettingRfp.SuspendLayout();
            this.flpAssociationSettingFusion.SuspendLayout();
            this.flpAssociationSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // trvQuantities
            // 
            this.trvQuantities.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.trvQuantities.Location = new System.Drawing.Point(12, 57);
            this.trvQuantities.Name = "trvQuantities";
            this.trvQuantities.Size = new System.Drawing.Size(241, 494);
            this.trvQuantities.TabIndex = 0;
            this.trvQuantities.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvQuantities_NodeMouseClick);
            // 
            // dtgRights
            // 
            this.dtgRights.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dtgRights.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgRights.Location = new System.Drawing.Point(259, 57);
            this.dtgRights.Name = "dtgRights";
            this.dtgRights.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgRights.Size = new System.Drawing.Size(439, 494);
            this.dtgRights.TabIndex = 1;
            this.dtgRights.CurrentCellDirtyStateChanged += new System.EventHandler(this.dtgRights_CurrentCellDirtyStateChanged);
            this.dtgRights.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dtgRights_RowPrePaint);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Location = new System.Drawing.Point(714, 562);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(119, 35);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "Undo Changes";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveChanges.Location = new System.Drawing.Point(591, 563);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(117, 33);
            this.btnSaveChanges.TabIndex = 3;
            this.btnSaveChanges.Text = "Save Changes";
            this.btnSaveChanges.UseVisualStyleBackColor = true;
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // lbl_Role
            // 
            this.lbl_Role.AutoSize = true;
            this.lbl_Role.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Role.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbl_Role.Location = new System.Drawing.Point(234, 10);
            this.lbl_Role.Name = "lbl_Role";
            this.lbl_Role.Size = new System.Drawing.Size(32, 13);
            this.lbl_Role.TabIndex = 4;
            this.lbl_Role.Text = "Role:";
            // 
            // txtIdentifier
            // 
            this.txtIdentifier.Location = new System.Drawing.Point(63, 7);
            this.txtIdentifier.Name = "txtIdentifier";
            this.txtIdentifier.Size = new System.Drawing.Size(168, 20);
            this.txtIdentifier.TabIndex = 7;
            // 
            // lbl_Identifier
            // 
            this.lbl_Identifier.AutoSize = true;
            this.lbl_Identifier.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Identifier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbl_Identifier.Location = new System.Drawing.Point(10, 10);
            this.lbl_Identifier.Name = "lbl_Identifier";
            this.lbl_Identifier.Size = new System.Drawing.Size(50, 13);
            this.lbl_Identifier.TabIndex = 6;
            this.lbl_Identifier.Text = "Identifier:";
            // 
            // cmRole
            // 
            this.cmRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmRole.FormattingEnabled = true;
            this.cmRole.Location = new System.Drawing.Point(273, 7);
            this.cmRole.Name = "cmRole";
            this.cmRole.Size = new System.Drawing.Size(200, 21);
            this.cmRole.TabIndex = 8;
            // 
            // Context_Rights
            // 
            this.Context_Rights.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Context_Rights.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.readToolStripMenuItem,
            this.writeToolStripMenuItem,
            this.hideToolStripMenuItem});
            this.Context_Rights.Name = "contextMenuStrip1";
            this.Context_Rights.Size = new System.Drawing.Size(119, 70);
            // 
            // readToolStripMenuItem
            // 
            this.readToolStripMenuItem.Name = "readToolStripMenuItem";
            this.readToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.readToolStripMenuItem.Text = "&Read All";
            this.readToolStripMenuItem.Click += new System.EventHandler(this.readToolStripMenuItem_Click);
            // 
            // writeToolStripMenuItem
            // 
            this.writeToolStripMenuItem.Name = "writeToolStripMenuItem";
            this.writeToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.writeToolStripMenuItem.Text = "&Write All";
            this.writeToolStripMenuItem.Click += new System.EventHandler(this.writeToolStripMenuItem_Click);
            // 
            // hideToolStripMenuItem
            // 
            this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            this.hideToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.hideToolStripMenuItem.Text = "&Hide All";
            this.hideToolStripMenuItem.Click += new System.EventHandler(this.hideToolStripMenuItem_Click);
            // 
            // lbl_header
            // 
            this.lbl_header.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_header.AutoSize = true;
            this.lbl_header.BackColor = System.Drawing.Color.Transparent;
            this.lbl_header.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_header.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbl_header.Location = new System.Drawing.Point(275, 33);
            this.lbl_header.Name = "lbl_header";
            this.lbl_header.Size = new System.Drawing.Size(72, 16);
            this.lbl_header.TabIndex = 9;
            this.lbl_header.Text = "lbl_header";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(255, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Note:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(304, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(373, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "1. Available Window List setting has no working in Display Window Parameter";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewCheckBoxColumn2});
            this.dataGridView1.Location = new System.Drawing.Point(259, 297);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(439, 254);
            this.dataGridView1.TabIndex = 12;
            this.dataGridView1.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "QuantityName";
            this.dataGridViewTextBoxColumn1.HeaderText = "Quantity Name";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 25;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "Read";
            this.dataGridViewCheckBoxColumn1.HeaderText = "Read";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            // 
            // dataGridViewCheckBoxColumn2
            // 
            this.dataGridViewCheckBoxColumn2.DataPropertyName = "Write";
            this.dataGridViewCheckBoxColumn2.HeaderText = "Write";
            this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
            // 
            // rbElectricity
            // 
            this.rbElectricity.AutoSize = true;
            this.rbElectricity.BackColor = System.Drawing.Color.Transparent;
            this.rbElectricity.Location = new System.Drawing.Point(6, 38);
            this.rbElectricity.Name = "rbElectricity";
            this.rbElectricity.Size = new System.Drawing.Size(70, 17);
            this.rbElectricity.TabIndex = 13;
            this.rbElectricity.TabStop = true;
            this.rbElectricity.Text = "Electricity";
            this.rbElectricity.UseVisualStyleBackColor = false;
            // 
            // gbMeterDevice
            // 
            this.gbMeterDevice.BackColor = System.Drawing.Color.Transparent;
            this.gbMeterDevice.Controls.Add(this.rbFusion);
            this.gbMeterDevice.Controls.Add(this.rbRFP135);
            this.gbMeterDevice.Location = new System.Drawing.Point(702, 10);
            this.gbMeterDevice.Name = "gbMeterDevice";
            this.gbMeterDevice.Size = new System.Drawing.Size(232, 44);
            this.gbMeterDevice.TabIndex = 14;
            this.gbMeterDevice.TabStop = false;
            this.gbMeterDevice.Text = "Meter Device";
            // 
            // rbFusion
            // 
            this.rbFusion.AutoSize = true;
            this.rbFusion.BackColor = System.Drawing.Color.Transparent;
            this.rbFusion.Location = new System.Drawing.Point(138, 19);
            this.rbFusion.Name = "rbFusion";
            this.rbFusion.Size = new System.Drawing.Size(65, 17);
            this.rbFusion.TabIndex = 16;
            this.rbFusion.TabStop = true;
            this.rbFusion.Text = "FUSION";
            this.rbFusion.UseVisualStyleBackColor = false;
            this.rbFusion.CheckedChanged += new System.EventHandler(this.MeterDevice_CheckedChanged);
            // 
            // rbRFP135
            // 
            this.rbRFP135.AutoSize = true;
            this.rbRFP135.BackColor = System.Drawing.Color.Transparent;
            this.rbRFP135.Location = new System.Drawing.Point(35, 19);
            this.rbRFP135.Name = "rbRFP135";
            this.rbRFP135.Size = new System.Drawing.Size(67, 17);
            this.rbRFP135.TabIndex = 15;
            this.rbRFP135.TabStop = true;
            this.rbRFP135.Text = "RFP-135";
            this.rbRFP135.UseVisualStyleBackColor = false;
            this.rbRFP135.CheckedChanged += new System.EventHandler(this.MeterDevice_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.gbRfp135ServerSap);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(214, 96);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Association Setting RFP-135";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Controls.Add(this.rbPublic);
            this.groupBox2.Location = new System.Drawing.Point(116, 23);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(93, 60);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Client";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.BackColor = System.Drawing.Color.Transparent;
            this.radioButton1.Location = new System.Drawing.Point(6, 16);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(87, 17);
            this.radioButton1.TabIndex = 16;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Management";
            this.radioButton1.UseVisualStyleBackColor = false;
            // 
            // rbPublic
            // 
            this.rbPublic.AutoSize = true;
            this.rbPublic.BackColor = System.Drawing.Color.Transparent;
            this.rbPublic.Location = new System.Drawing.Point(7, 39);
            this.rbPublic.Name = "rbPublic";
            this.rbPublic.Size = new System.Drawing.Size(54, 17);
            this.rbPublic.TabIndex = 14;
            this.rbPublic.TabStop = true;
            this.rbPublic.Text = "Public";
            this.rbPublic.UseVisualStyleBackColor = false;
            // 
            // gbRfp135ServerSap
            // 
            this.gbRfp135ServerSap.BackColor = System.Drawing.Color.Transparent;
            this.gbRfp135ServerSap.Controls.Add(this.rbManagment);
            this.gbRfp135ServerSap.Controls.Add(this.rbElectricity);
            this.gbRfp135ServerSap.Location = new System.Drawing.Point(11, 23);
            this.gbRfp135ServerSap.Name = "gbRfp135ServerSap";
            this.gbRfp135ServerSap.Size = new System.Drawing.Size(93, 60);
            this.gbRfp135ServerSap.TabIndex = 16;
            this.gbRfp135ServerSap.TabStop = false;
            this.gbRfp135ServerSap.Text = "Server";
            // 
            // rbManagment
            // 
            this.rbManagment.AutoSize = true;
            this.rbManagment.BackColor = System.Drawing.Color.Transparent;
            this.rbManagment.Location = new System.Drawing.Point(6, 19);
            this.rbManagment.Name = "rbManagment";
            this.rbManagment.Size = new System.Drawing.Size(87, 17);
            this.rbManagment.TabIndex = 15;
            this.rbManagment.TabStop = true;
            this.rbManagment.Text = "Management";
            this.rbManagment.UseVisualStyleBackColor = false;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.radioButton8);
            this.groupBox3.Controls.Add(this.radioButton7);
            this.groupBox3.Controls.Add(this.radioButton6);
            this.groupBox3.Controls.Add(this.radioButton5);
            this.groupBox3.Controls.Add(this.radioButton4);
            this.groupBox3.Controls.Add(this.radioButton3);
            this.groupBox3.Controls.Add(this.radioButton2);
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(220, 171);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Association Setting Fusion";
            // 
            // radioButton8
            // 
            this.radioButton8.AutoSize = true;
            this.radioButton8.BackColor = System.Drawing.Color.Transparent;
            this.radioButton8.Location = new System.Drawing.Point(10, 148);
            this.radioButton8.Name = "radioButton8";
            this.radioButton8.Size = new System.Drawing.Size(152, 17);
            this.radioButton8.TabIndex = 23;
            this.radioButton8.TabStop = true;
            this.radioButton8.Text = "HLS Management 2 (0x20)";
            this.radioButton8.UseVisualStyleBackColor = false;
            // 
            // radioButton7
            // 
            this.radioButton7.AutoSize = true;
            this.radioButton7.BackColor = System.Drawing.Color.Transparent;
            this.radioButton7.Location = new System.Drawing.Point(10, 126);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(143, 17);
            this.radioButton7.TabIndex = 22;
            this.radioButton7.TabStop = true;
            this.radioButton7.Text = "HLS Management (0x01)";
            this.radioButton7.UseVisualStyleBackColor = false;
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.BackColor = System.Drawing.Color.Transparent;
            this.radioButton6.Location = new System.Drawing.Point(10, 103);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(162, 17);
            this.radioButton6.TabIndex = 21;
            this.radioButton6.TabStop = true;
            this.radioButton6.Text = "HLS ReadOnly + RTC (0x13)";
            this.radioButton6.UseVisualStyleBackColor = false;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.BackColor = System.Drawing.Color.Transparent;
            this.radioButton5.Location = new System.Drawing.Point(10, 81);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(128, 17);
            this.radioButton5.TabIndex = 20;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "HLS ReadOnly (0x12)";
            this.radioButton5.UseVisualStyleBackColor = false;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.BackColor = System.Drawing.Color.Transparent;
            this.radioButton4.Location = new System.Drawing.Point(10, 60);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(85, 17);
            this.radioButton4.TabIndex = 19;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "LLS 2 (0x21)";
            this.radioButton4.UseVisualStyleBackColor = false;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.BackColor = System.Drawing.Color.Transparent;
            this.radioButton3.Location = new System.Drawing.Point(10, 37);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(76, 17);
            this.radioButton3.TabIndex = 18;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "LLS (0x11)";
            this.radioButton3.UseVisualStyleBackColor = false;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.BackColor = System.Drawing.Color.Transparent;
            this.radioButton2.Location = new System.Drawing.Point(10, 17);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(86, 17);
            this.radioButton2.TabIndex = 17;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Public (0x10)";
            this.radioButton2.UseVisualStyleBackColor = false;
            // 
            // flpAssociationSettingRfp
            // 
            this.flpAssociationSettingRfp.BackColor = System.Drawing.Color.Transparent;
            this.flpAssociationSettingRfp.Controls.Add(this.groupBox1);
            this.flpAssociationSettingRfp.Location = new System.Drawing.Point(3, 3);
            this.flpAssociationSettingRfp.Name = "flpAssociationSettingRfp";
            this.flpAssociationSettingRfp.Size = new System.Drawing.Size(226, 100);
            this.flpAssociationSettingRfp.TabIndex = 19;
            this.flpAssociationSettingRfp.Visible = false;
            // 
            // flpAssociationSettingFusion
            // 
            this.flpAssociationSettingFusion.BackColor = System.Drawing.Color.Transparent;
            this.flpAssociationSettingFusion.Controls.Add(this.groupBox3);
            this.flpAssociationSettingFusion.Location = new System.Drawing.Point(3, 109);
            this.flpAssociationSettingFusion.Name = "flpAssociationSettingFusion";
            this.flpAssociationSettingFusion.Size = new System.Drawing.Size(226, 181);
            this.flpAssociationSettingFusion.TabIndex = 20;
            this.flpAssociationSettingFusion.Visible = false;
            // 
            // flpAssociationSetting
            // 
            this.flpAssociationSetting.BackColor = System.Drawing.Color.Transparent;
            this.flpAssociationSetting.Controls.Add(this.flpAssociationSettingRfp);
            this.flpAssociationSetting.Controls.Add(this.flpAssociationSettingFusion);
            this.flpAssociationSetting.Location = new System.Drawing.Point(703, 60);
            this.flpAssociationSetting.Name = "flpAssociationSetting";
            this.flpAssociationSetting.Size = new System.Drawing.Size(231, 296);
            this.flpAssociationSetting.TabIndex = 21;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StartMsn,
            this.EndMsn});
            this.dataGridView2.Location = new System.Drawing.Point(706, 392);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 25;
            this.dataGridView2.Size = new System.Drawing.Size(228, 154);
            this.dataGridView2.TabIndex = 22;
            // 
            // StartMsn
            // 
            this.StartMsn.HeaderText = "Start MSN";
            this.StartMsn.Name = "StartMsn";
            // 
            // EndMsn
            // 
            this.EndMsn.HeaderText = "End MSN";
            this.EndMsn.Name = "EndMsn";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(706, 373);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Allowed MSN Ranges";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 665);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(946, 33);
            this.panel1.TabIndex = 24;
            // 
            // ApplicationUserRights
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(946, 698);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.flpAssociationSetting);
            this.Controls.Add(this.gbMeterDevice);
            this.Controls.Add(this.dtgRights);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lbl_header);
            this.Controls.Add(this.cmRole);
            this.Controls.Add(this.txtIdentifier);
            this.Controls.Add(this.lbl_Identifier);
            this.Controls.Add(this.btnSaveChanges);
            this.Controls.Add(this.lbl_Role);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.trvQuantities);
            this.DoubleBuffered = true;
            this.Name = "ApplicationUserRights";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ApplicationUserRights";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ApplicationUserRights_FormClosed);
            this.Load += new System.EventHandler(this.ApplicationUserRights_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ApplicationUserRights_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.dtgRights)).EndInit();
            this.Context_Rights.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.gbMeterDevice.ResumeLayout(false);
            this.gbMeterDevice.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gbRfp135ServerSap.ResumeLayout(false);
            this.gbRfp135ServerSap.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.flpAssociationSettingRfp.ResumeLayout(false);
            this.flpAssociationSettingFusion.ResumeLayout(false);
            this.flpAssociationSetting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView trvQuantities;
        private System.Windows.Forms.DataGridView dtgRights;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSaveChanges;
        private System.Windows.Forms.Label lbl_Role;
        private System.Windows.Forms.TextBox txtIdentifier;
        private System.Windows.Forms.Label lbl_Identifier;
        private System.Windows.Forms.ComboBox cmRole;
        private System.Windows.Forms.ContextMenuStrip Context_Rights;
        private System.Windows.Forms.ToolStripMenuItem readToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem writeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem;
        private System.Windows.Forms.Label lbl_header;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private System.Windows.Forms.RadioButton rbElectricity;
        private System.Windows.Forms.GroupBox gbMeterDevice;
        private System.Windows.Forms.RadioButton rbFusion;
        private System.Windows.Forms.RadioButton rbRFP135;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbManagment;
        private System.Windows.Forms.RadioButton rbPublic;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.GroupBox gbRfp135ServerSap;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButton8;
        private System.Windows.Forms.RadioButton radioButton7;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.FlowLayoutPanel flpAssociationSettingRfp;
        private System.Windows.Forms.FlowLayoutPanel flpAssociationSettingFusion;
        private System.Windows.Forms.FlowLayoutPanel flpAssociationSetting;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartMsn;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndMsn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
    }
}