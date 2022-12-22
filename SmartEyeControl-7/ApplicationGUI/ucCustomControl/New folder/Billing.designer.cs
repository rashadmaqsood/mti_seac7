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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PnlBilling));
            this.lblHeading = new System.Windows.Forms.Label();
            this.pb_billing = new System.Windows.Forms.ProgressBar();
            this.check_Billing_AddToDB = new System.Windows.Forms.CheckBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.gpMonthlyBillingFilter = new System.Windows.Forms.GroupBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.check_readBilling = new System.Windows.Forms.CheckBox();
            this.combo_billingSelective = new System.Windows.Forms.ComboBox();
            this.lbl_billingCount = new System.Windows.Forms.Label();
            this.lbl_month = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_Page = new System.Windows.Forms.Label();
            this.lbl_TotalPages = new System.Windows.Forms.Label();
            this.lbl_CurrentPage = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.grid_billing = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.T1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.T2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.T3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.T4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bckWorker_Billing = new System.ComponentModel.BackgroundWorker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_BillingReport = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_comm_verification_rpt = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_firstRecord = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_PreviousMonth = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_lastRecord = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_NextMonth = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_newBillingbutton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_clearBillingGrid = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.gpMonthlyBillingFilter.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_billing)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.Black;
            this.lblHeading.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblHeading.Location = new System.Drawing.Point(365, 4);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(213, 33);
            this.lblHeading.TabIndex = 9;
            this.lblHeading.Text = "     Billing Heading";
            // 
            // pb_billing
            // 
            this.pb_billing.Location = new System.Drawing.Point(126, 572);
            this.pb_billing.MarqueeAnimationSpeed = 35;
            this.pb_billing.Name = "pb_billing";
            this.pb_billing.Size = new System.Drawing.Size(590, 28);
            this.pb_billing.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pb_billing.TabIndex = 50;
            this.pb_billing.Visible = false;
            // 
            // check_Billing_AddToDB
            // 
            this.check_Billing_AddToDB.AutoSize = true;
            this.check_Billing_AddToDB.Checked = true;
            this.check_Billing_AddToDB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_Billing_AddToDB.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_Billing_AddToDB.Location = new System.Drawing.Point(764, 383);
            this.check_Billing_AddToDB.Name = "check_Billing_AddToDB";
            this.check_Billing_AddToDB.Size = new System.Drawing.Size(116, 19);
            this.check_Billing_AddToDB.TabIndex = 12;
            this.check_Billing_AddToDB.Text = "Add to Database";
            this.check_Billing_AddToDB.UseVisualStyleBackColor = true;
            this.check_Billing_AddToDB.Visible = false;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.Black;
            this.label22.Location = new System.Drawing.Point(26, 7);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(41, 18);
            this.label22.TabIndex = 2;
            this.label22.Text = "Date:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.Black;
            this.label24.Location = new System.Drawing.Point(532, 13);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(125, 18);
            this.label24.TabIndex = 2;
            this.label24.Text = "MDI Reset Counter";
            // 
            // gpMonthlyBillingFilter
            // 
            this.gpMonthlyBillingFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gpMonthlyBillingFilter.Controls.Add(this.label26);
            this.gpMonthlyBillingFilter.Controls.Add(this.label25);
            this.gpMonthlyBillingFilter.Controls.Add(this.check_readBilling);
            this.gpMonthlyBillingFilter.Controls.Add(this.combo_billingSelective);
            this.gpMonthlyBillingFilter.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpMonthlyBillingFilter.ForeColor = System.Drawing.Color.Maroon;
            this.gpMonthlyBillingFilter.Location = new System.Drawing.Point(755, 283);
            this.gpMonthlyBillingFilter.Name = "gpMonthlyBillingFilter";
            this.gpMonthlyBillingFilter.Size = new System.Drawing.Size(200, 94);
            this.gpMonthlyBillingFilter.TabIndex = 10;
            this.gpMonthlyBillingFilter.TabStop = false;
            this.gpMonthlyBillingFilter.Text = "Monthly Billing Filter";
            this.gpMonthlyBillingFilter.Visible = false;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.ForeColor = System.Drawing.Color.Black;
            this.label26.Location = new System.Drawing.Point(125, 56);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(57, 15);
            this.label26.TabIndex = 2;
            this.label26.Text = "month(s)";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.ForeColor = System.Drawing.Color.Black;
            this.label25.Location = new System.Drawing.Point(6, 56);
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
            this.combo_billingSelective.Location = new System.Drawing.Point(65, 53);
            this.combo_billingSelective.Name = "combo_billingSelective";
            this.combo_billingSelective.Size = new System.Drawing.Size(54, 23);
            this.combo_billingSelective.TabIndex = 0;
            this.combo_billingSelective.SelectedIndexChanged += new System.EventHandler(this.combo_billingSelective_SelectedIndexChanged);
            // 
            // lbl_billingCount
            // 
            this.lbl_billingCount.AutoSize = true;
            this.lbl_billingCount.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_billingCount.ForeColor = System.Drawing.Color.Navy;
            this.lbl_billingCount.Location = new System.Drawing.Point(672, 13);
            this.lbl_billingCount.Name = "lbl_billingCount";
            this.lbl_billingCount.Size = new System.Drawing.Size(18, 18);
            this.lbl_billingCount.TabIndex = 2;
            this.lbl_billingCount.Text = "--";
            // 
            // lbl_month
            // 
            this.lbl_month.AutoSize = true;
            this.lbl_month.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_month.ForeColor = System.Drawing.Color.Black;
            this.lbl_month.Location = new System.Drawing.Point(73, 8);
            this.lbl_month.Name = "lbl_month";
            this.lbl_month.Size = new System.Drawing.Size(36, 18);
            this.lbl_month.TabIndex = 2;
            this.lbl_month.Text = "____";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_Page);
            this.panel1.Controls.Add(this.lbl_TotalPages);
            this.panel1.Controls.Add(this.lbl_CurrentPage);
            this.panel1.Controls.Add(this.label23);
            this.panel1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(544, 372);
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
            this.grid_billing.Location = new System.Drawing.Point(10, 34);
            this.grid_billing.Name = "grid_billing";
            this.grid_billing.ReadOnly = true;
            this.grid_billing.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Control;
            this.grid_billing.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.grid_billing.Size = new System.Drawing.Size(680, 332);
            this.grid_billing.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // T1
            // 
            this.T1.HeaderText = "T1";
            this.T1.Name = "T1";
            this.T1.ReadOnly = true;
            // 
            // T2
            // 
            this.T2.HeaderText = "T2";
            this.T2.Name = "T2";
            this.T2.ReadOnly = true;
            // 
            // T3
            // 
            this.T3.HeaderText = "T3";
            this.T3.Name = "T3";
            this.T3.ReadOnly = true;
            // 
            // T4
            // 
            this.T4.HeaderText = "T4";
            this.T4.Name = "T4";
            this.T4.ReadOnly = true;
            // 
            // TL
            // 
            this.TL.HeaderText = "TL";
            this.TL.Name = "TL";
            this.TL.ReadOnly = true;
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
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.lbl_month);
            this.groupBox1.Controls.Add(this.btn_firstRecord);
            this.groupBox1.Controls.Add(this.btn_PreviousMonth);
            this.groupBox1.Controls.Add(this.grid_billing);
            this.groupBox1.Controls.Add(this.btn_lastRecord);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.lbl_billingCount);
            this.groupBox1.Controls.Add(this.btn_NextMonth);
            this.groupBox1.Location = new System.Drawing.Point(19, 142);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(736, 424);
            this.groupBox1.TabIndex = 51;
            this.groupBox1.TabStop = false;
            // 
            // btn_BillingReport
            // 
            this.btn_BillingReport.Location = new System.Drawing.Point(138, 42);
            this.btn_BillingReport.Name = "btn_BillingReport";
            this.btn_BillingReport.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_BillingReport.Size = new System.Drawing.Size(125, 30);
            this.btn_BillingReport.TabIndex = 52;
            this.btn_BillingReport.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_BillingReport.Values.Image")));
            this.btn_BillingReport.Values.Text = "Generate Report";
            this.btn_BillingReport.Visible = false;
            this.btn_BillingReport.Click += new System.EventHandler(this.btn_BillingReport_Click);
            // 
            // btn_comm_verification_rpt
            // 
            this.btn_comm_verification_rpt.Location = new System.Drawing.Point(400, 42);
            this.btn_comm_verification_rpt.Name = "btn_comm_verification_rpt";
            this.btn_comm_verification_rpt.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_comm_verification_rpt.Size = new System.Drawing.Size(146, 30);
            this.btn_comm_verification_rpt.TabIndex = 53;
            this.btn_comm_verification_rpt.Values.Text = "Comm. Verification Report";
            this.btn_comm_verification_rpt.Visible = false;
            this.btn_comm_verification_rpt.Click += new System.EventHandler(this.btn_comm_verification_rpt_Click);
            // 
            // btn_firstRecord
            // 
            this.btn_firstRecord.AutoSize = true;
            this.btn_firstRecord.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_firstRecord.BackgroundImage")));
            this.btn_firstRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_firstRecord.ForeColor = System.Drawing.Color.OldLace;
            this.btn_firstRecord.Location = new System.Drawing.Point(159, 380);
            this.btn_firstRecord.Name = "btn_firstRecord";
            this.btn_firstRecord.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_firstRecord.Size = new System.Drawing.Size(50, 25);
            this.btn_firstRecord.TabIndex = 7;
            this.btn_firstRecord.Values.Text = "<<";
            this.btn_firstRecord.Visible = false;
            this.btn_firstRecord.Click += new System.EventHandler(this.btn_firstRecord_Click);
            // 
            // btn_PreviousMonth
            // 
            this.btn_PreviousMonth.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_PreviousMonth.BackgroundImage")));
            this.btn_PreviousMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PreviousMonth.ForeColor = System.Drawing.Color.OldLace;
            this.btn_PreviousMonth.Location = new System.Drawing.Point(253, 379);
            this.btn_PreviousMonth.Name = "btn_PreviousMonth";
            this.btn_PreviousMonth.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_PreviousMonth.Size = new System.Drawing.Size(37, 25);
            this.btn_PreviousMonth.TabIndex = 7;
            this.btn_PreviousMonth.Values.Text = "<";
            this.btn_PreviousMonth.Visible = false;
            this.btn_PreviousMonth.Click += new System.EventHandler(this.btn_PreviousMonth_Click);
            // 
            // btn_lastRecord
            // 
            this.btn_lastRecord.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_lastRecord.BackgroundImage")));
            this.btn_lastRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_lastRecord.ForeColor = System.Drawing.Color.OldLace;
            this.btn_lastRecord.Location = new System.Drawing.Point(378, 379);
            this.btn_lastRecord.Name = "btn_lastRecord";
            this.btn_lastRecord.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_lastRecord.Size = new System.Drawing.Size(49, 25);
            this.btn_lastRecord.TabIndex = 6;
            this.btn_lastRecord.Values.Text = ">>";
            this.btn_lastRecord.Visible = false;
            this.btn_lastRecord.Click += new System.EventHandler(this.btn_lastRecord_Click);
            // 
            // btn_NextMonth
            // 
            this.btn_NextMonth.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_NextMonth.BackgroundImage")));
            this.btn_NextMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_NextMonth.ForeColor = System.Drawing.Color.OldLace;
            this.btn_NextMonth.Location = new System.Drawing.Point(296, 379);
            this.btn_NextMonth.Name = "btn_NextMonth";
            this.btn_NextMonth.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_NextMonth.Size = new System.Drawing.Size(37, 25);
            this.btn_NextMonth.TabIndex = 6;
            this.btn_NextMonth.Values.Text = ">";
            this.btn_NextMonth.Visible = false;
            this.btn_NextMonth.Click += new System.EventHandler(this.btn_NextMonth_Click);
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
            this.btn_newBillingbutton.Values.Text = "Get Billing Data";
            this.btn_newBillingbutton.Click += new System.EventHandler(this.btn_newBillingbutton_Click);
            // 
            // btn_clearBillingGrid
            // 
            this.btn_clearBillingGrid.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_clearBillingGrid.BackgroundImage")));
            this.btn_clearBillingGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_clearBillingGrid.ForeColor = System.Drawing.Color.OldLace;
            this.btn_clearBillingGrid.Location = new System.Drawing.Point(269, 42);
            this.btn_clearBillingGrid.Name = "btn_clearBillingGrid";
            this.btn_clearBillingGrid.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_clearBillingGrid.Size = new System.Drawing.Size(125, 30);
            this.btn_clearBillingGrid.TabIndex = 6;
            this.btn_clearBillingGrid.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_clearBillingGrid.Values.Image")));
            this.btn_clearBillingGrid.Values.Text = "Clear Data";
            this.btn_clearBillingGrid.Visible = false;
            this.btn_clearBillingGrid.Click += new System.EventHandler(this.btn_clearBillingGrid_Click);
            // 
            // PnlBilling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_comm_verification_rpt);
            this.Controls.Add(this.btn_BillingReport);
            this.Controls.Add(this.pb_billing);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.btn_newBillingbutton);
            this.Controls.Add(this.check_Billing_AddToDB);
            this.Controls.Add(this.btn_clearBillingGrid);
            this.Controls.Add(this.gpMonthlyBillingFilter);
            this.DoubleBuffered = true;
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
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // private System.Windows.Forms.Button btn_newBillingbutton;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_newBillingbutton;
        private System.Windows.Forms.Label lbl_month;
        private System.Windows.Forms.Label label22;
      //  private System.Windows.Forms.Button btn_PreviousMonth;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_PreviousMonth;
       // private System.Windows.Forms.Button btn_NextMonth;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_NextMonth;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_Page;
        private System.Windows.Forms.Label lbl_TotalPages;
        private System.Windows.Forms.Label lbl_CurrentPage;
        private System.Windows.Forms.Label label23;
      //  private System.Windows.Forms.Button btn_firstRecord;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_firstRecord;
     //   private System.Windows.Forms.Button btn_lastRecord;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_lastRecord;
        private System.Windows.Forms.Label lblHeading;
       // private System.Windows.Forms.Button btn_clearBillingGrid;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_clearBillingGrid;

        private System.Windows.Forms.Label label24;
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
        public ProgressBar pb_billing;
        //private Button btn_BillingReport;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_BillingReport;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_comm_verification_rpt;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn T1;
        private DataGridViewTextBoxColumn T2;
        private DataGridViewTextBoxColumn T3;
        private DataGridViewTextBoxColumn T4;
        private DataGridViewTextBoxColumn TL;
            
       
    }
}
