using System.Windows.Forms;
namespace SmartEyeControl_7.ApplicationGUI.GUI
{
    partial class PnlBilling
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
            if (Application_Controller != null)
            {
                Application_Controller.PropertyChanged -= Application_Controller_PropertyChanged;
            }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PnlBilling));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblHeading = new System.Windows.Forms.Label();
            this.check_Billing_AddToDB = new System.Windows.Forms.CheckBox();
            this.btn_firstRecord = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.btn_PreviousMonth = new System.Windows.Forms.Button();
            this.lbl_MDI_Reset_Counter = new System.Windows.Forms.Label();
            this.btn_lastRecord = new System.Windows.Forms.Button();
            this.btn_NextMonth = new System.Windows.Forms.Button();
            this.gpMonthlyBillingFilter = new System.Windows.Forms.GroupBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.check_readBilling = new System.Windows.Forms.CheckBox();
            this.combo_billingSelective = new System.Windows.Forms.ComboBox();
            this.lbl_billingCount = new System.Windows.Forms.Label();
            this.lbl_month = new System.Windows.Forms.Label();
            this.btn_clearBillingGrid = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_Page = new System.Windows.Forms.Label();
            this.lbl_TotalPages = new System.Windows.Forms.Label();
            this.lbl_CurrentPage = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.btn_newBillingbutton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.grid_billing = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.T1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.T2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.T3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.T4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bckWorker_Billing = new System.ComponentModel.BackgroundWorker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Label_lblDayResetCount = new System.Windows.Forms.Label();
            this.lbl_day_count = new System.Windows.Forms.Label();
            this.Label_lblDayResetDate = new System.Windows.Forms.Label();
            this.lbl_day_reset_date = new System.Windows.Forms.Label();
            this.btn_BillingReport = new System.Windows.Forms.Button();
            this.btn_comm_verification_rpt = new System.Windows.Forms.Button();
            this.tbBillingTypes = new System.Windows.Forms.TabControl();
            this.tpCumulative = new System.Windows.Forms.TabPage();
            this.tpMonthly = new System.Windows.Forms.TabPage();
            this.tpDaily = new System.Windows.Forms.TabPage();
            this.gpBillingType = new System.Windows.Forms.GroupBox();
            this.rdbDailyMonthlyBilling = new System.Windows.Forms.RadioButton();
            this.rdbMonthlyBilling = new System.Windows.Forms.RadioButton();
            this.rdbCumulativeBilling = new System.Windows.Forms.RadioButton();
            this.chkDetail = new System.Windows.Forms.CheckBox();
            this.gpMonthlyBillingFilter.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_billing)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tbBillingTypes.SuspendLayout();
            this.gpBillingType.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.BackColor = System.Drawing.Color.Transparent;
            this.lblHeading.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.Black;
            this.lblHeading.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblHeading.Location = new System.Drawing.Point(365, 4);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(115, 33);
            this.lblHeading.TabIndex = 9;
            this.lblHeading.Text = "     Billing";
            // 
            // check_Billing_AddToDB
            // 
            this.check_Billing_AddToDB.AutoSize = true;
            this.check_Billing_AddToDB.BackColor = System.Drawing.Color.Transparent;
            this.check_Billing_AddToDB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.check_Billing_AddToDB.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_Billing_AddToDB.Location = new System.Drawing.Point(925, 22);
            this.check_Billing_AddToDB.Name = "check_Billing_AddToDB";
            this.check_Billing_AddToDB.Size = new System.Drawing.Size(116, 19);
            this.check_Billing_AddToDB.TabIndex = 12;
            this.check_Billing_AddToDB.Text = "Add to Database";
            this.check_Billing_AddToDB.UseVisualStyleBackColor = false;
            this.check_Billing_AddToDB.Visible = false;
            // 
            // btn_firstRecord
            // 
            this.btn_firstRecord.AutoSize = true;
            this.btn_firstRecord.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_firstRecord.BackgroundImage")));
            this.btn_firstRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_firstRecord.ForeColor = System.Drawing.Color.OldLace;
            this.btn_firstRecord.Location = new System.Drawing.Point(239, 5);
            this.btn_firstRecord.Name = "btn_firstRecord";
            this.btn_firstRecord.Size = new System.Drawing.Size(50, 34);
            this.btn_firstRecord.TabIndex = 7;
            this.btn_firstRecord.Text = "<<";
            this.btn_firstRecord.Visible = false;
            this.btn_firstRecord.Click += new System.EventHandler(this.btn_firstRecord_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.Black;
            this.label22.Location = new System.Drawing.Point(266, 6);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(108, 18);
            this.label22.TabIndex = 2;
            this.label22.Text = "MDI Reset Date:";
            // 
            // btn_PreviousMonth
            // 
            this.btn_PreviousMonth.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_PreviousMonth.BackgroundImage")));
            this.btn_PreviousMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PreviousMonth.ForeColor = System.Drawing.Color.OldLace;
            this.btn_PreviousMonth.Location = new System.Drawing.Point(306, 5);
            this.btn_PreviousMonth.Name = "btn_PreviousMonth";
            this.btn_PreviousMonth.Size = new System.Drawing.Size(37, 34);
            this.btn_PreviousMonth.TabIndex = 7;
            this.btn_PreviousMonth.Text = "<";
            this.btn_PreviousMonth.Visible = false;
            this.btn_PreviousMonth.Click += new System.EventHandler(this.btn_PreviousMonth_Click);
            // 
            // lbl_MDI_Reset_Counter
            // 
            this.lbl_MDI_Reset_Counter.AutoSize = true;
            this.lbl_MDI_Reset_Counter.BackColor = System.Drawing.Color.Transparent;
            this.lbl_MDI_Reset_Counter.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MDI_Reset_Counter.ForeColor = System.Drawing.Color.Black;
            this.lbl_MDI_Reset_Counter.Location = new System.Drawing.Point(789, 6);
            this.lbl_MDI_Reset_Counter.Name = "lbl_MDI_Reset_Counter";
            this.lbl_MDI_Reset_Counter.Size = new System.Drawing.Size(129, 18);
            this.lbl_MDI_Reset_Counter.TabIndex = 2;
            this.lbl_MDI_Reset_Counter.Text = "MDI Reset Counter:";
            // 
            // btn_lastRecord
            // 
            this.btn_lastRecord.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_lastRecord.BackgroundImage")));
            this.btn_lastRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_lastRecord.ForeColor = System.Drawing.Color.OldLace;
            this.btn_lastRecord.Location = new System.Drawing.Point(402, 5);
            this.btn_lastRecord.Name = "btn_lastRecord";
            this.btn_lastRecord.Size = new System.Drawing.Size(49, 34);
            this.btn_lastRecord.TabIndex = 6;
            this.btn_lastRecord.Text = ">>";
            this.btn_lastRecord.Visible = false;
            this.btn_lastRecord.Click += new System.EventHandler(this.btn_lastRecord_Click);
            // 
            // btn_NextMonth
            // 
            this.btn_NextMonth.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_NextMonth.BackgroundImage")));
            this.btn_NextMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_NextMonth.ForeColor = System.Drawing.Color.OldLace;
            this.btn_NextMonth.Location = new System.Drawing.Point(349, 5);
            this.btn_NextMonth.Name = "btn_NextMonth";
            this.btn_NextMonth.Size = new System.Drawing.Size(37, 34);
            this.btn_NextMonth.TabIndex = 6;
            this.btn_NextMonth.Text = ">";
            this.btn_NextMonth.Visible = false;
            this.btn_NextMonth.Click += new System.EventHandler(this.btn_NextMonth_Click);
            // 
            // gpMonthlyBillingFilter
            // 
            this.gpMonthlyBillingFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gpMonthlyBillingFilter.BackColor = System.Drawing.Color.Transparent;
            this.gpMonthlyBillingFilter.Controls.Add(this.label26);
            this.gpMonthlyBillingFilter.Controls.Add(this.label25);
            this.gpMonthlyBillingFilter.Controls.Add(this.check_readBilling);
            this.gpMonthlyBillingFilter.Controls.Add(this.combo_billingSelective);
            this.gpMonthlyBillingFilter.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpMonthlyBillingFilter.ForeColor = System.Drawing.Color.Maroon;
            this.gpMonthlyBillingFilter.Location = new System.Drawing.Point(693, 4);
            this.gpMonthlyBillingFilter.Name = "gpMonthlyBillingFilter";
            this.gpMonthlyBillingFilter.Size = new System.Drawing.Size(200, 76);
            this.gpMonthlyBillingFilter.TabIndex = 10;
            this.gpMonthlyBillingFilter.TabStop = false;
            this.gpMonthlyBillingFilter.Text = "Monthly Billing Filter";
            this.gpMonthlyBillingFilter.Visible = false;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.ForeColor = System.Drawing.Color.Black;
            this.label26.Location = new System.Drawing.Point(125, 48);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(57, 15);
            this.label26.TabIndex = 2;
            this.label26.Text = "month(s)";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.ForeColor = System.Drawing.Color.Black;
            this.label25.Location = new System.Drawing.Point(6, 48);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(59, 15);
            this.label25.TabIndex = 2;
            this.label25.Text = "Read Last";
            // 
            // check_readBilling
            // 
            this.check_readBilling.AutoSize = true;
            this.check_readBilling.ForeColor = System.Drawing.Color.Black;
            this.check_readBilling.Location = new System.Drawing.Point(6, 19);
            this.check_readBilling.Name = "check_readBilling";
            this.check_readBilling.Size = new System.Drawing.Size(86, 19);
            this.check_readBilling.TabIndex = 1;
            this.check_readBilling.Text = "All Records";
            this.check_readBilling.UseVisualStyleBackColor = true;
            this.check_readBilling.CheckedChanged += new System.EventHandler(this.check_readBilling_CheckedChanged);
            // 
            // combo_billingSelective
            // 
            this.combo_billingSelective.FormattingEnabled = true;
            this.combo_billingSelective.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24"});
            this.combo_billingSelective.Location = new System.Drawing.Point(65, 45);
            this.combo_billingSelective.Name = "combo_billingSelective";
            this.combo_billingSelective.Size = new System.Drawing.Size(54, 23);
            this.combo_billingSelective.TabIndex = 0;
            this.combo_billingSelective.SelectedIndexChanged += new System.EventHandler(this.combo_billingSelective_SelectedIndexChanged);
            // 
            // lbl_billingCount
            // 
            this.lbl_billingCount.AutoSize = true;
            this.lbl_billingCount.BackColor = System.Drawing.Color.Transparent;
            this.lbl_billingCount.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_billingCount.ForeColor = System.Drawing.Color.Black;
            this.lbl_billingCount.Location = new System.Drawing.Point(924, 6);
            this.lbl_billingCount.Name = "lbl_billingCount";
            this.lbl_billingCount.Size = new System.Drawing.Size(36, 18);
            this.lbl_billingCount.TabIndex = 2;
            this.lbl_billingCount.Text = "____";
            // 
            // lbl_month
            // 
            this.lbl_month.AutoSize = true;
            this.lbl_month.BackColor = System.Drawing.Color.Transparent;
            this.lbl_month.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_month.ForeColor = System.Drawing.Color.Black;
            this.lbl_month.Location = new System.Drawing.Point(380, 6);
            this.lbl_month.Name = "lbl_month";
            this.lbl_month.Size = new System.Drawing.Size(36, 18);
            this.lbl_month.TabIndex = 2;
            this.lbl_month.Text = "____";
            // 
            // btn_clearBillingGrid
            // 
            this.btn_clearBillingGrid.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_clearBillingGrid.BackgroundImage")));
            this.btn_clearBillingGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_clearBillingGrid.ForeColor = System.Drawing.Color.OldLace;
            this.btn_clearBillingGrid.Location = new System.Drawing.Point(138, 42);
            this.btn_clearBillingGrid.Name = "btn_clearBillingGrid";
            this.btn_clearBillingGrid.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_clearBillingGrid.Size = new System.Drawing.Size(125, 30);
            this.btn_clearBillingGrid.TabIndex = 6;
            this.btn_clearBillingGrid.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_clearBillingGrid.Values.Image")));
            this.btn_clearBillingGrid.Values.Text = "Clear Data";
            this.btn_clearBillingGrid.Click += new System.EventHandler(this.btn_clearBillingGrid_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.lbl_Page);
            this.panel1.Controls.Add(this.lbl_TotalPages);
            this.panel1.Controls.Add(this.lbl_CurrentPage);
            this.panel1.Controls.Add(this.label23);
            this.panel1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(508, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(86, 24);
            this.panel1.TabIndex = 5;
            this.panel1.Visible = false;
            // 
            // lbl_Page
            // 
            this.lbl_Page.AutoSize = true;
            this.lbl_Page.Location = new System.Drawing.Point(3, 7);
            this.lbl_Page.Name = "lbl_Page";
            this.lbl_Page.Size = new System.Drawing.Size(33, 15);
            this.lbl_Page.TabIndex = 3;
            this.lbl_Page.Text = "Page";
            // 
            // lbl_TotalPages
            // 
            this.lbl_TotalPages.AutoSize = true;
            this.lbl_TotalPages.Location = new System.Drawing.Point(59, 7);
            this.lbl_TotalPages.Name = "lbl_TotalPages";
            this.lbl_TotalPages.Size = new System.Drawing.Size(13, 15);
            this.lbl_TotalPages.TabIndex = 3;
            this.lbl_TotalPages.Text = "_";
            // 
            // lbl_CurrentPage
            // 
            this.lbl_CurrentPage.AutoSize = true;
            this.lbl_CurrentPage.Location = new System.Drawing.Point(34, 7);
            this.lbl_CurrentPage.Name = "lbl_CurrentPage";
            this.lbl_CurrentPage.Size = new System.Drawing.Size(13, 15);
            this.lbl_CurrentPage.TabIndex = 3;
            this.lbl_CurrentPage.Text = "_";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(50, 7);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(13, 16);
            this.label23.TabIndex = 3;
            this.label23.Text = "/";
            // 
            // btn_newBillingbutton
            // 
            this.btn_newBillingbutton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_newBillingbutton.BackgroundImage")));
            this.btn_newBillingbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_newBillingbutton.ForeColor = System.Drawing.Color.Transparent;
            this.btn_newBillingbutton.Location = new System.Drawing.Point(7, 42);
            this.btn_newBillingbutton.Name = "btn_newBillingbutton";
            this.btn_newBillingbutton.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_newBillingbutton.Size = new System.Drawing.Size(125, 30);
            this.btn_newBillingbutton.TabIndex = 1;
            this.btn_newBillingbutton.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_newBillingbutton.Values.Image")));
            this.btn_newBillingbutton.Values.Text = "Read Data";
            this.btn_newBillingbutton.Click += new System.EventHandler(this.btn_newBillingbutton_Click);
            // 
            // grid_billing
            // 
            this.grid_billing.AllowUserToAddRows = false;
            this.grid_billing.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grid_billing.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grid_billing.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid_billing.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.grid_billing.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.grid_billing.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.T1,
            this.T2,
            this.T3,
            this.T4,
            this.TL});
            this.grid_billing.Cursor = System.Windows.Forms.Cursors.Hand;
            this.grid_billing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid_billing.Location = new System.Drawing.Point(3, 50);
            this.grid_billing.Name = "grid_billing";
            this.grid_billing.ReadOnly = true;
            this.grid_billing.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Control;
            this.grid_billing.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.grid_billing.Size = new System.Drawing.Size(998, 411);
            this.grid_billing.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column1.FillWeight = 27.58591F;
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 23;
            // 
            // T1
            // 
            this.T1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.T1.FillWeight = 90.10152F;
            this.T1.HeaderText = "T1";
            this.T1.Name = "T1";
            this.T1.ReadOnly = true;
            this.T1.Width = 49;
            // 
            // T2
            // 
            this.T2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.T2.FillWeight = 74.29176F;
            this.T2.HeaderText = "T2";
            this.T2.Name = "T2";
            this.T2.ReadOnly = true;
            this.T2.Width = 49;
            // 
            // T3
            // 
            this.T3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.T3.FillWeight = 62.43748F;
            this.T3.HeaderText = "T3";
            this.T3.Name = "T3";
            this.T3.ReadOnly = true;
            this.T3.Width = 49;
            // 
            // T4
            // 
            this.T4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.T4.FillWeight = 53.59191F;
            this.T4.HeaderText = "T4";
            this.T4.Name = "T4";
            this.T4.ReadOnly = true;
            this.T4.Width = 49;
            // 
            // TL
            // 
            this.TL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TL.FillWeight = 46.99141F;
            this.TL.HeaderText = "TL";
            this.TL.Name = "TL";
            this.TL.ReadOnly = true;
            this.TL.Width = 49;
            // 
            // bckWorker_Billing
            // 
            this.bckWorker_Billing.WorkerSupportsCancellation = true;
            this.bckWorker_Billing.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bckWorker_Billing_DoWork);
            this.bckWorker_Billing.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bckWorker_Billing_RunWorkerCompleted);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.grid_billing);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Location = new System.Drawing.Point(19, 102);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1004, 507);
            this.groupBox1.TabIndex = 51;
            this.groupBox1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btn_firstRecord);
            this.panel3.Controls.Add(this.btn_NextMonth);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.btn_PreviousMonth);
            this.panel3.Controls.Add(this.btn_lastRecord);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(3, 461);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(998, 43);
            this.panel3.TabIndex = 17;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.Label_lblDayResetCount);
            this.panel2.Controls.Add(this.lbl_billingCount);
            this.panel2.Controls.Add(this.lbl_day_count);
            this.panel2.Controls.Add(this.lbl_month);
            this.panel2.Controls.Add(this.Label_lblDayResetDate);
            this.panel2.Controls.Add(this.lbl_MDI_Reset_Counter);
            this.panel2.Controls.Add(this.lbl_day_reset_date);
            this.panel2.Controls.Add(this.label22);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 16);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(998, 34);
            this.panel2.TabIndex = 16;
            // 
            // Label_lblDayResetCount
            // 
            this.Label_lblDayResetCount.AutoSize = true;
            this.Label_lblDayResetCount.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_lblDayResetCount.ForeColor = System.Drawing.Color.Black;
            this.Label_lblDayResetCount.Location = new System.Drawing.Point(573, 6);
            this.Label_lblDayResetCount.Name = "Label_lblDayResetCount";
            this.Label_lblDayResetCount.Size = new System.Drawing.Size(126, 18);
            this.Label_lblDayResetCount.TabIndex = 14;
            this.Label_lblDayResetCount.Text = "Day Reset Counter:";
            // 
            // lbl_day_count
            // 
            this.lbl_day_count.AutoSize = true;
            this.lbl_day_count.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_day_count.ForeColor = System.Drawing.Color.Black;
            this.lbl_day_count.Location = new System.Drawing.Point(706, 6);
            this.lbl_day_count.Name = "lbl_day_count";
            this.lbl_day_count.Size = new System.Drawing.Size(36, 18);
            this.lbl_day_count.TabIndex = 15;
            this.lbl_day_count.Text = "____";
            // 
            // Label_lblDayResetDate
            // 
            this.Label_lblDayResetDate.AutoSize = true;
            this.Label_lblDayResetDate.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_lblDayResetDate.ForeColor = System.Drawing.Color.Black;
            this.Label_lblDayResetDate.Location = new System.Drawing.Point(6, 6);
            this.Label_lblDayResetDate.Name = "Label_lblDayResetDate";
            this.Label_lblDayResetDate.Size = new System.Drawing.Size(105, 18);
            this.Label_lblDayResetDate.TabIndex = 12;
            this.Label_lblDayResetDate.Text = "Day Reset Date:";
            // 
            // lbl_day_reset_date
            // 
            this.lbl_day_reset_date.AutoSize = true;
            this.lbl_day_reset_date.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_day_reset_date.ForeColor = System.Drawing.Color.Black;
            this.lbl_day_reset_date.Location = new System.Drawing.Point(120, 6);
            this.lbl_day_reset_date.Name = "lbl_day_reset_date";
            this.lbl_day_reset_date.Size = new System.Drawing.Size(36, 18);
            this.lbl_day_reset_date.TabIndex = 13;
            this.lbl_day_reset_date.Text = "____";
            // 
            // btn_BillingReport
            // 
            this.btn_BillingReport.Image = ((System.Drawing.Image)(resources.GetObject("btn_BillingReport.Image")));
            this.btn_BillingReport.Location = new System.Drawing.Point(270, 42);
            this.btn_BillingReport.Name = "btn_BillingReport";
            this.btn_BillingReport.Size = new System.Drawing.Size(125, 30);
            this.btn_BillingReport.TabIndex = 52;
            this.btn_BillingReport.Text = "Generate Report";
            this.btn_BillingReport.Visible = false;
            this.btn_BillingReport.Click += new System.EventHandler(this.btn_BillingReport_Click);
            // 
            // btn_comm_verification_rpt
            // 
            this.btn_comm_verification_rpt.Location = new System.Drawing.Point(400, 42);
            this.btn_comm_verification_rpt.Name = "btn_comm_verification_rpt";
            this.btn_comm_verification_rpt.Size = new System.Drawing.Size(146, 30);
            this.btn_comm_verification_rpt.TabIndex = 53;
            this.btn_comm_verification_rpt.Text = "Comm. Verification Report";
            this.btn_comm_verification_rpt.Visible = false;
            this.btn_comm_verification_rpt.Click += new System.EventHandler(this.btn_comm_verification_rpt_Click);
            // 
            // tbBillingTypes
            // 
            this.tbBillingTypes.Controls.Add(this.tpCumulative);
            this.tbBillingTypes.Controls.Add(this.tpMonthly);
            this.tbBillingTypes.Controls.Add(this.tpDaily);
            this.tbBillingTypes.Location = new System.Drawing.Point(19, 85);
            this.tbBillingTypes.Name = "tbBillingTypes";
            this.tbBillingTypes.SelectedIndex = 0;
            this.tbBillingTypes.Size = new System.Drawing.Size(219, 23);
            this.tbBillingTypes.TabIndex = 54;
            this.tbBillingTypes.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tpCumulative
            // 
            this.tpCumulative.Location = new System.Drawing.Point(4, 22);
            this.tpCumulative.Name = "tpCumulative";
            this.tpCumulative.Padding = new System.Windows.Forms.Padding(3);
            this.tpCumulative.Size = new System.Drawing.Size(211, 0);
            this.tpCumulative.TabIndex = 0;
            this.tpCumulative.Text = "Cumulative";
            this.tpCumulative.UseVisualStyleBackColor = true;
            // 
            // tpMonthly
            // 
            this.tpMonthly.Location = new System.Drawing.Point(4, 22);
            this.tpMonthly.Name = "tpMonthly";
            this.tpMonthly.Padding = new System.Windows.Forms.Padding(3);
            this.tpMonthly.Size = new System.Drawing.Size(211, 0);
            this.tpMonthly.TabIndex = 1;
            this.tpMonthly.Text = "Monthly";
            this.tpMonthly.UseVisualStyleBackColor = true;
            // 
            // tpDaily
            // 
            this.tpDaily.Location = new System.Drawing.Point(4, 22);
            this.tpDaily.Name = "tpDaily";
            this.tpDaily.Padding = new System.Windows.Forms.Padding(3);
            this.tpDaily.Size = new System.Drawing.Size(211, 0);
            this.tpDaily.TabIndex = 2;
            this.tpDaily.Text = "Daily Energy Profile";
            this.tpDaily.UseVisualStyleBackColor = true;
            // 
            // gpBillingType
            // 
            this.gpBillingType.Controls.Add(this.rdbDailyMonthlyBilling);
            this.gpBillingType.Controls.Add(this.rdbMonthlyBilling);
            this.gpBillingType.Controls.Add(this.rdbCumulativeBilling);
            this.gpBillingType.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpBillingType.ForeColor = System.Drawing.Color.Maroon;
            this.gpBillingType.Location = new System.Drawing.Point(1029, 110);
            this.gpBillingType.Name = "gpBillingType";
            this.gpBillingType.Size = new System.Drawing.Size(200, 100);
            this.gpBillingType.TabIndex = 12;
            this.gpBillingType.TabStop = false;
            this.gpBillingType.Text = "Billing Type Selection";
            this.gpBillingType.Visible = false;
            // 
            // rdbDailyMonthlyBilling
            // 
            this.rdbDailyMonthlyBilling.AutoSize = true;
            this.rdbDailyMonthlyBilling.ForeColor = System.Drawing.Color.Navy;
            this.rdbDailyMonthlyBilling.Location = new System.Drawing.Point(11, 71);
            this.rdbDailyMonthlyBilling.Name = "rdbDailyMonthlyBilling";
            this.rdbDailyMonthlyBilling.Size = new System.Drawing.Size(130, 19);
            this.rdbDailyMonthlyBilling.TabIndex = 2;
            this.rdbDailyMonthlyBilling.TabStop = true;
            this.rdbDailyMonthlyBilling.Text = "Daily Energy Profile";
            this.rdbDailyMonthlyBilling.UseVisualStyleBackColor = true;
            // 
            // rdbMonthlyBilling
            // 
            this.rdbMonthlyBilling.AutoSize = true;
            this.rdbMonthlyBilling.ForeColor = System.Drawing.Color.Navy;
            this.rdbMonthlyBilling.Location = new System.Drawing.Point(10, 47);
            this.rdbMonthlyBilling.Name = "rdbMonthlyBilling";
            this.rdbMonthlyBilling.Size = new System.Drawing.Size(106, 19);
            this.rdbMonthlyBilling.TabIndex = 1;
            this.rdbMonthlyBilling.TabStop = true;
            this.rdbMonthlyBilling.Text = "Monthly Billing";
            this.rdbMonthlyBilling.UseVisualStyleBackColor = true;
            // 
            // rdbCumulativeBilling
            // 
            this.rdbCumulativeBilling.AutoSize = true;
            this.rdbCumulativeBilling.Checked = true;
            this.rdbCumulativeBilling.ForeColor = System.Drawing.Color.Navy;
            this.rdbCumulativeBilling.Location = new System.Drawing.Point(10, 20);
            this.rdbCumulativeBilling.Name = "rdbCumulativeBilling";
            this.rdbCumulativeBilling.Size = new System.Drawing.Size(122, 19);
            this.rdbCumulativeBilling.TabIndex = 0;
            this.rdbCumulativeBilling.TabStop = true;
            this.rdbCumulativeBilling.Text = "Cumulative Billing";
            this.rdbCumulativeBilling.UseVisualStyleBackColor = true;
            // 
            // chkDetail
            // 
            this.chkDetail.AutoSize = true;
            this.chkDetail.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDetail.ForeColor = System.Drawing.Color.Black;
            this.chkDetail.Location = new System.Drawing.Point(925, 46);
            this.chkDetail.Name = "chkDetail";
            this.chkDetail.Size = new System.Drawing.Size(126, 19);
            this.chkDetail.TabIndex = 12;
            this.chkDetail.Text = "Detail Information";
            this.chkDetail.UseVisualStyleBackColor = true;
            // 
            // PnlBilling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.chkDetail);
            this.Controls.Add(this.gpBillingType);
            this.Controls.Add(this.tbBillingTypes);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_comm_verification_rpt);
            this.Controls.Add(this.btn_BillingReport);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.btn_newBillingbutton);
            this.Controls.Add(this.check_Billing_AddToDB);
            this.Controls.Add(this.btn_clearBillingGrid);
            this.Controls.Add(this.gpMonthlyBillingFilter);
            this.Location = new System.Drawing.Point(0, 72);
            this.Name = "PnlBilling";
            this.Size = new System.Drawing.Size(1350, 650);
            this.Load += new System.EventHandler(this.pnlBilling_Load);
            this.gpMonthlyBillingFilter.ResumeLayout(false);
            this.gpMonthlyBillingFilter.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_billing)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tbBillingTypes.ResumeLayout(false);
            this.gpBillingType.ResumeLayout(false);
            this.gpBillingType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // private System.Windows.Forms.Button btn_newBillingbutton;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_newBillingbutton;
        private System.Windows.Forms.Label lbl_month;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button btn_PreviousMonth;
        //private ComponentFactory.Krypton.Toolkit.KryptonButton btn_PreviousMonth;
        private System.Windows.Forms.Button btn_NextMonth;
        //private ComponentFactory.Krypton.Toolkit.KryptonButton btn_NextMonth;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_Page;
        private System.Windows.Forms.Label lbl_TotalPages;
        private System.Windows.Forms.Label lbl_CurrentPage;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button btn_firstRecord;
        //private ComponentFactory.Krypton.Toolkit.KryptonButton btn_firstRecord;
        private System.Windows.Forms.Button btn_lastRecord;
        //private ComponentFactory.Krypton.Toolkit.KryptonButton btn_lastRecord;
        private System.Windows.Forms.Label lblHeading;
       // private System.Windows.Forms.Button btn_clearBillingGrid;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_clearBillingGrid;

        private System.Windows.Forms.Label lbl_MDI_Reset_Counter;
        private System.Windows.Forms.Label lbl_billingCount;
        private System.Windows.Forms.GroupBox gpMonthlyBillingFilter;
        private System.Windows.Forms.CheckBox check_readBilling;
        private System.Windows.Forms.ComboBox combo_billingSelective;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.ComponentModel.BackgroundWorker bckWorker_Billing;
        private System.Windows.Forms.CheckBox check_Billing_AddToDB;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid_billing;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_BillingReport;
        //private ComponentFactory.Krypton.Toolkit.KryptonButton btn_BillingReport;
        private System.Windows.Forms.Button btn_comm_verification_rpt;
        private TabControl tbBillingTypes;
        private TabPage tpCumulative;
        private TabPage tpMonthly;
        private TabPage tpDaily;
        private GroupBox gpBillingType;
        private RadioButton rdbDailyMonthlyBilling;
        private RadioButton rdbMonthlyBilling;
        private RadioButton rdbCumulativeBilling;
        private CheckBox chkDetail;
        private Label Label_lblDayResetCount;
        private Label lbl_day_count;
        private Label Label_lblDayResetDate;
        private Label lbl_day_reset_date;
        private Panel panel3;
        private Panel panel2;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn T1;
        private DataGridViewTextBoxColumn T2;
        private DataGridViewTextBoxColumn T3;
        private DataGridViewTextBoxColumn T4;
        private DataGridViewTextBoxColumn TL;
    }
}
