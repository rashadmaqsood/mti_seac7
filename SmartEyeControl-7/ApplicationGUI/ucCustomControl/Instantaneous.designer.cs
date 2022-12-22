using System;
using System.Windows.Forms;
namespace ucCustomControl
{
    partial class Instantaneous
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Instantaneous));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle37 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle38 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle39 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle45 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle46 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle40 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle41 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle42 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle43 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle44 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle47 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle48 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle49 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle50 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle51 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle52 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle53 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle54 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle55 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle56 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle57 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle58 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbl_PbStatus = new System.Windows.Forms.Label();
            this.pb_ins = new System.Windows.Forms.ProgressBar();
            this.bckWorker_Instantanouse = new System.ComponentModel.BackgroundWorker();
            this.bgw_NewINsRead = new System.ComponentModel.BackgroundWorker();
            this.bgw_DisplayWindows = new System.ComponentModel.BackgroundWorker();
            this.tcontrolMain = new System.Windows.Forms.TabControl();
            this.INs = new System.Windows.Forms.TabPage();
            this.gpReadInst = new System.Windows.Forms.GroupBox();
            this.check_I_AddtoDB = new System.Windows.Forms.CheckBox();
            this.check_PowerQuadrent = new System.Windows.Forms.CheckBox();
            this.gpPhase = new System.Windows.Forms.GroupBox();
            this.rdbPhC = new System.Windows.Forms.RadioButton();
            this.rdbAllPhases = new System.Windows.Forms.RadioButton();
            this.rdbPhA = new System.Windows.Forms.RadioButton();
            this.rdbPhB = new System.Windows.Forms.RadioButton();
            this.GpQuantity = new System.Windows.Forms.GroupBox();
            this.check_ReactivePower = new System.Windows.Forms.CheckBox();
            this.check_readMDI_Interval = new System.Windows.Forms.CheckBox();
            this.check_Mdi = new System.Windows.Forms.CheckBox();
            this.check_Apparent = new System.Windows.Forms.CheckBox();
            this.check_misc = new System.Windows.Forms.CheckBox();
            this.check_AllPhy = new System.Windows.Forms.CheckBox();
            this.check_Voltage = new System.Windows.Forms.CheckBox();
            this.check_Powerfactor = new System.Windows.Forms.CheckBox();
            this.check_ActivePower = new System.Windows.Forms.CheckBox();
            this.check_Current = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnIReadInst = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grid_Instanstanouse = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbl_meter_datetime = new System.Windows.Forms.Label();
            this.lbl_meter_serial = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grid_misc = new System.Windows.Forms.DataGridView();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tab_MDI = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tpMDI = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grid_CurrentMDI = new System.Windows.Forms.DataGridView();
            this.QuantityLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.T1_Val = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.T2_Val = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.T3_Val = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.T4_Val = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TL_Val = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chk_All = new System.Windows.Forms.CheckBox();
            this.chkLstMDIs = new System.Windows.Forms.CheckedListBox();
            this.tbDebugMDI = new System.Windows.Forms.TabPage();
            this.gp_MDIs = new System.Windows.Forms.GroupBox();
            this.lbl_MDI_SlideCounter = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.lbl_MDI_Previous_Energy = new System.Windows.Forms.Label();
            this.lbl_MDI_Previous_Power = new System.Windows.Forms.Label();
            this.lbl_MDI_Running_Energy = new System.Windows.Forms.Label();
            this.lbl_MDI_Running_Power = new System.Windows.Forms.Label();
            this.lbl_MDI_TimeLeft = new System.Windows.Forms.Label();
            this.lbl_MDI_SlideCount = new System.Windows.Forms.Label();
            this.lbl_MDI_Counter = new System.Windows.Forms.Label();
            this.lbl_MDI_time = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.grid_MDI = new System.Windows.Forms.DataGridView();
            this.Slide1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Slide2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Slide3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Slide4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Slide5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Slide6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpInstantaneousMDI = new System.Windows.Forms.TabPage();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.btnGetMditoMonitor = new System.Windows.Forms.Button();
            this.label78 = new System.Windows.Forms.Label();
            this.cbxMditoMonitor = new System.Windows.Forms.ComboBox();
            this.btnSetMonitoredValue = new System.Windows.Forms.Button();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.lbl_limit_over_volt = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.lblCurrentAverageValue = new System.Windows.Forms.Label();
            this.lblNumberOfPeriods = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.lblLastAverageValue = new System.Windows.Forms.Label();
            this.label77 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.lblCaptureTime = new System.Windows.Forms.Label();
            this.lblScalerUnit = new System.Windows.Forms.Label();
            this.label76 = new System.Windows.Forms.Label();
            this.label75 = new System.Windows.Forms.Label();
            this.lblStartCaptureTime = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label74 = new System.Windows.Forms.Label();
            this.lblReadMDI = new System.Windows.Forms.Button();
            this.listInstantaneousMDI = new System.Windows.Forms.ListBox();
            this.tab_NewIns = new System.Windows.Forms.TabPage();
            this.pb_newIns = new System.Windows.Forms.ProgressBar();
            this.lbl_MeterDate = new System.Windows.Forms.Label();
            this.btn_INS_Report = new System.Windows.Forms.Button();
            this.lbl_Frequency = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_NewIns = new System.Windows.Forms.Button();
            this.bl = new System.Windows.Forms.Label();
            this.grid_NEwIns = new System.Windows.Forms.DataGridView();
            this.tab_Record = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.all_in_one = new System.Windows.Forms.TabPage();
            this.rb_uuncheck_all = new System.Windows.Forms.RadioButton();
            this.rb_Counter = new System.Windows.Forms.RadioButton();
            this.rb_check_all_log = new System.Windows.Forms.RadioButton();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.lbl_packetSize = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.grid_EventNames = new System.Windows.Forms.DataGridView();
            this.Event = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Log = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.even_counter = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chk_all_monthly = new System.Windows.Forms.CheckBox();
            this.chk_check_all_cummulative = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.check_PABS16 = new System.Windows.Forms.CheckBox();
            this.chk_CUMMDIP = new System.Windows.Forms.CheckBox();
            this.chk_CumMDIQ = new System.Windows.Forms.CheckBox();
            this.check_PABS15 = new System.Windows.Forms.CheckBox();
            this.chk_TL = new System.Windows.Forms.CheckBox();
            this.check_Q16 = new System.Windows.Forms.CheckBox();
            this.check_P16 = new System.Windows.Forms.CheckBox();
            this.check_Q15 = new System.Windows.Forms.CheckBox();
            this.chk_T4 = new System.Windows.Forms.CheckBox();
            this.check_S16 = new System.Windows.Forms.CheckBox();
            this.check_P15 = new System.Windows.Forms.CheckBox();
            this.check_QAbs16 = new System.Windows.Forms.CheckBox();
            this.chk_T3 = new System.Windows.Forms.CheckBox();
            this.check_S15 = new System.Windows.Forms.CheckBox();
            this.check_T16 = new System.Windows.Forms.CheckBox();
            this.chk_T2 = new System.Windows.Forms.CheckBox();
            this.check_QAbs15 = new System.Windows.Forms.CheckBox();
            this.chk_MDIP16 = new System.Windows.Forms.CheckBox();
            this.chk_T1 = new System.Windows.Forms.CheckBox();
            this.check_T15 = new System.Windows.Forms.CheckBox();
            this.chk_MDIQ16 = new System.Windows.Forms.CheckBox();
            this.chk_MDIP15 = new System.Windows.Forms.CheckBox();
            this.chk_MDIQ15 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chk_PT = new System.Windows.Forms.CheckBox();
            this.chk_CT = new System.Windows.Forms.CheckBox();
            this.chk_EventCount = new System.Windows.Forms.CheckBox();
            this.chk_MDI_Reset = new System.Windows.Forms.CheckBox();
            this.chk_S = new System.Windows.Forms.CheckBox();
            this.chk_V = new System.Windows.Forms.CheckBox();
            this.chk_MDITime = new System.Windows.Forms.CheckBox();
            this.chk_Q = new System.Windows.Forms.CheckBox();
            this.chk_AlarmSTS = new System.Windows.Forms.CheckBox();
            this.chk_LPLog = new System.Windows.Forms.CheckBox();
            this.chk_I = new System.Windows.Forms.CheckBox();
            this.chk_MdiPre = new System.Windows.Forms.CheckBox();
            this.check_PF = new System.Windows.Forms.CheckBox();
            this.chk_LPCount = new System.Windows.Forms.CheckBox();
            this.chk_P = new System.Windows.Forms.CheckBox();
            this.chk_EventLog = new System.Windows.Forms.CheckBox();
            this.chk_Tamper_power = new System.Windows.Forms.CheckBox();
            this.chk_frq = new System.Windows.Forms.CheckBox();
            this.btn_GET_INS = new System.Windows.Forms.Button();
            this.btn_SetAll = new System.Windows.Forms.Button();
            this.btn_GET_ALL = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.pnl_TL_Load_Profile = new System.Windows.Forms.Panel();
            this.label26 = new System.Windows.Forms.Label();
            this.grid_tl_load_profile = new System.Windows.Forms.DataGridView();
            this.DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COUNTER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KWH_P = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KWH_N = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KVARH_Q1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KVARH_Q2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KVARH_Q3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KVARH_Q4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KVAH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TAMPER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MDI_KW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MDI_KVAR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CAPTURE_TIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DAY_MDI_KW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DAY_MDI_KVAR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnl_cb_day_record = new System.Windows.Forms.Panel();
            this.label23 = new System.Windows.Forms.Label();
            this.grid_view_cb_day_record = new System.Windows.Forms.DataGridView();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.T1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.T2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.T3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.T4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbl_record_counter = new System.Windows.Forms.Label();
            this.lbl_this_date_time = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.lbl_total_records = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.lbl_record_no = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lbl_last_reset_date_time = new System.Windows.Forms.Label();
            this.btn_next = new System.Windows.Forms.Button();
            this.label25 = new System.Windows.Forms.Label();
            this.btn_previous = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btn_TLLoadProfile = new System.Windows.Forms.Button();
            this.btn_CBDayRecord = new System.Windows.Forms.Button();
            this.btn_MakeError = new System.Windows.Forms.Button();
            this.btn_Set_ReadRawData = new System.Windows.Forms.Button();
            this.txt_EPMNumber = new System.Windows.Forms.TextBox();
            this.txt_RawDataLength = new System.Windows.Forms.TextBox();
            this.txt_RawDataAddress = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.btn_ReadRawData = new System.Windows.Forms.Button();
            this.txt_general = new System.Windows.Forms.RichTextBox();
            this.AllInOnetab = new System.Windows.Forms.TabPage();
            this.lbl_meter_date_time = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.lbl_meterserial = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.txt_alarm_status = new System.Windows.Forms.RichTextBox();
            this.lbl_pt_denominator = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.lbl_pt_nominator = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.lbl_ct_denominator = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.lbl_ct_nominator = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.lbl_mdi_count = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.lbl_mdi_end_date = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.lbl_slide_count = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.lbl_mdi_pre_kvar = new System.Windows.Forms.Label();
            this.lbl_mdi_pre_kw = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.lblmdi_time = new System.Windows.Forms.Label();
            this.lbl_timer = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Label();
            this.tab_Billing = new System.Windows.Forms.TabControl();
            this.Commulative = new System.Windows.Forms.TabPage();
            this.grv_commulative_billing = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.T_L = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Monthly = new System.Windows.Forms.TabPage();
            this.grv_monthly_billing = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grv_event_logs = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Detail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbl_day_profile = new System.Windows.Forms.Label();
            this.lbl_season_profile = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.lbl_channel_4 = new System.Windows.Forms.Label();
            this.lbl_channel_2 = new System.Windows.Forms.Label();
            this.lbl_count = new System.Windows.Forms.Label();
            this.lbl_channel_3 = new System.Windows.Forms.Label();
            this.lbl_channel_1 = new System.Windows.Forms.Label();
            this.lbl_time_id = new System.Windows.Forms.Label();
            this.lbl_FRQ = new System.Windows.Forms.Label();
            this.lbl_TP = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.grv_general_instentanious = new System.Windows.Forms.DataGridView();
            this.InstentaniousTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Phase1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Phase2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Phase3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Avg_Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tab_CrDwData = new System.Windows.Forms.TabPage();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label17 = new System.Windows.Forms.Label();
            this.radio_test = new System.Windows.Forms.RadioButton();
            this.radio_alt = new System.Windows.Forms.RadioButton();
            this.radio_nor = new System.Windows.Forms.RadioButton();
            this.btn_get_Dwd = new System.Windows.Forms.Button();
            this.rtb_1 = new System.Windows.Forms.RichTextBox();
            this.tab_Contactor = new System.Windows.Forms.TabPage();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.chkSkipSchedule = new System.Windows.Forms.CheckBox();
            this.chkWaitOnTariffChange = new System.Windows.Forms.CheckBox();
            this.chkContactorDisabled = new System.Windows.Forms.CheckBox();
            this.chkIsCapCharged = new System.Windows.Forms.CheckBox();
            this.chkOnForRetryBySwitch = new System.Windows.Forms.CheckBox();
            this.chkPUDContactor = new System.Windows.Forms.CheckBox();
            this.chkContactorState = new System.Windows.Forms.CheckBox();
            this.chkContactorEventIndex = new System.Windows.Forms.CheckBox();
            this.chkContactorToOn = new System.Windows.Forms.CheckBox();
            this.chkMakeCOntactorEvent = new System.Windows.Forms.CheckBox();
            this.chkDelayBWContactorState = new System.Windows.Forms.CheckBox();
            this.chkMakePulseContactor = new System.Windows.Forms.CheckBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.lblSkippedScheduleIndex = new System.Windows.Forms.Label();
            this.lblScheduleIndex = new System.Windows.Forms.Label();
            this.label81 = new System.Windows.Forms.Label();
            this.label82 = new System.Windows.Forms.Label();
            this.lblWaitOnTariffChange = new System.Windows.Forms.Label();
            this.lblFailureStateTimer = new System.Windows.Forms.Label();
            this.lblConnectThroughSwitch = new System.Windows.Forms.Label();
            this.lblTimerX = new System.Windows.Forms.Label();
            this.lblTimerX2 = new System.Windows.Forms.Label();
            this.lblTariffIndex = new System.Windows.Forms.Label();
            this.lblRetryCounter = new System.Windows.Forms.Label();
            this.laber100 = new System.Windows.Forms.Label();
            this.label73 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.lblStateShouldBe = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label46 = new System.Windows.Forms.Label();
            this.check_onRemoteCommand = new System.Windows.Forms.CheckBox();
            this.lblTariffIndex_Status = new System.Windows.Forms.Label();
            this.check_onIRDAcommand = new System.Windows.Forms.CheckBox();
            this.lblRetryCount = new System.Windows.Forms.Label();
            this.check_OnTariffChange = new System.Windows.Forms.CheckBox();
            this.label53 = new System.Windows.Forms.Label();
            this.check_Contactor = new System.Windows.Forms.CheckBox();
            this.check_onBySwitch = new System.Windows.Forms.CheckBox();
            this.check_overCurrent = new System.Windows.Forms.CheckBox();
            this.check_offbyRemoteCommand = new System.Windows.Forms.CheckBox();
            this.check_Overload = new System.Windows.Forms.CheckBox();
            this.check_onBySwitchwithRemote = new System.Windows.Forms.CheckBox();
            this.check_offByIRDAcommand = new System.Windows.Forms.CheckBox();
            this.check_MDIExceed = new System.Windows.Forms.CheckBox();
            this.check_underVolt = new System.Windows.Forms.CheckBox();
            this.chkFailureStateDetected_State = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.check_overVolt = new System.Windows.Forms.CheckBox();
            this.chkRecoverFromPowerDown = new System.Windows.Forms.CheckBox();
            this.check_offByRetryExpire = new System.Windows.Forms.CheckBox();
            this.chkDisabled = new System.Windows.Forms.CheckBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.chkOverLoadByPhaseTrig = new System.Windows.Forms.CheckBox();
            this.chkFailureStateDetectedTrig = new System.Windows.Forms.CheckBox();
            this.chkOverLoadByTotalTrig = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chkOverLoadByPhase = new System.Windows.Forms.CheckBox();
            this.chkFailureStateDetected = new System.Windows.Forms.CheckBox();
            this.chkOverLoadByTotal = new System.Windows.Forms.CheckBox();
            this.btn_GetContactorFlags = new System.Windows.Forms.Button();
            this.btn_InsOldReport = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_read_all_instantaneous_values = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.tcontrolMain.SuspendLayout();
            this.INs.SuspendLayout();
            this.gpReadInst.SuspendLayout();
            this.gpPhase.SuspendLayout();
            this.GpQuantity.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_Instanstanouse)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_misc)).BeginInit();
            this.tab_MDI.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tpMDI.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_CurrentMDI)).BeginInit();
            this.panel1.SuspendLayout();
            this.tbDebugMDI.SuspendLayout();
            this.gp_MDIs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_MDI)).BeginInit();
            this.tpInstantaneousMDI.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.tab_NewIns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_NEwIns)).BeginInit();
            this.tab_Record.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.all_in_one.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_EventNames)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.pnl_TL_Load_Profile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_tl_load_profile)).BeginInit();
            this.pnl_cb_day_record.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_view_cb_day_record)).BeginInit();
            this.AllInOnetab.SuspendLayout();
            this.tab_Billing.SuspendLayout();
            this.Commulative.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grv_commulative_billing)).BeginInit();
            this.Monthly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grv_monthly_billing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grv_event_logs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grv_general_instentanious)).BeginInit();
            this.tab_CrDwData.SuspendLayout();
            this.tab_Contactor.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_PbStatus
            // 
            this.lbl_PbStatus.AutoSize = true;
            this.lbl_PbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_PbStatus.Location = new System.Drawing.Point(326, 533);
            this.lbl_PbStatus.Name = "lbl_PbStatus";
            this.lbl_PbStatus.Size = new System.Drawing.Size(146, 16);
            this.lbl_PbStatus.TabIndex = 4;
            this.lbl_PbStatus.Text = "Progress Bar Status";
            this.lbl_PbStatus.Visible = false;
            // 
            // pb_ins
            // 
            this.pb_ins.Location = new System.Drawing.Point(16, 526);
            this.pb_ins.Name = "pb_ins";
            this.pb_ins.Size = new System.Drawing.Size(304, 23);
            this.pb_ins.TabIndex = 3;
            this.pb_ins.Visible = false;
            // 
            // bckWorker_Instantanouse
            // 
            this.bckWorker_Instantanouse.WorkerReportsProgress = true;
            this.bckWorker_Instantanouse.WorkerSupportsCancellation = true;
            this.bckWorker_Instantanouse.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bckWorker_Instantanouse_DoWork);
            this.bckWorker_Instantanouse.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bckWorker_Instantanouse_RunWorkerCompleted);
            // 
            // bgw_NewINsRead
            // 
            this.bgw_NewINsRead.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_NewINsRead_DoWork);
            this.bgw_NewINsRead.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_NewINsRead_RunWorkerCompleted);
            // 
            // bgw_DisplayWindows
            // 
            this.bgw_DisplayWindows.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_DisplayWindows_DoWork);
            this.bgw_DisplayWindows.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_DisplayWindows_RunWorkerCompleted);
            // 
            // tcontrolMain
            // 
            this.tcontrolMain.Controls.Add(this.INs);
            this.tcontrolMain.Controls.Add(this.tab_MDI);
            this.tcontrolMain.Controls.Add(this.tab_NewIns);
            this.tcontrolMain.Controls.Add(this.tab_Record);
            this.tcontrolMain.Controls.Add(this.tab_CrDwData);
            this.tcontrolMain.Controls.Add(this.tab_Contactor);
            this.tcontrolMain.Location = new System.Drawing.Point(16, 41);
            this.tcontrolMain.Name = "tcontrolMain";
            this.tcontrolMain.SelectedIndex = 0;
            this.tcontrolMain.Size = new System.Drawing.Size(1046, 479);
            this.tcontrolMain.TabIndex = 14;
            this.tcontrolMain.SelectedIndexChanged += new System.EventHandler(this.tcontrolMain_SelectedIndexChanged);
            // 
            // INs
            // 
            this.INs.BackColor = System.Drawing.Color.Transparent;
            this.INs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.INs.Controls.Add(this.gpReadInst);
            this.INs.Controls.Add(this.groupBox3);
            this.INs.Controls.Add(this.groupBox2);
            this.INs.Location = new System.Drawing.Point(4, 22);
            this.INs.Name = "INs";
            this.INs.Size = new System.Drawing.Size(1038, 453);
            this.INs.TabIndex = 2;
            this.INs.Text = "Instantaneous";
            this.INs.UseVisualStyleBackColor = true;
            // 
            // gpReadInst
            // 
            this.gpReadInst.Controls.Add(this.check_I_AddtoDB);
            this.gpReadInst.Controls.Add(this.check_PowerQuadrent);
            this.gpReadInst.Controls.Add(this.gpPhase);
            this.gpReadInst.Controls.Add(this.GpQuantity);
            this.gpReadInst.Controls.Add(this.button1);
            this.gpReadInst.Controls.Add(this.btnIReadInst);
            this.gpReadInst.Location = new System.Drawing.Point(6, 7);
            this.gpReadInst.Name = "gpReadInst";
            this.gpReadInst.Size = new System.Drawing.Size(166, 284);
            this.gpReadInst.TabIndex = 1;
            this.gpReadInst.TabStop = false;
            this.gpReadInst.Text = "Read Options";
            // 
            // check_I_AddtoDB
            // 
            this.check_I_AddtoDB.AutoSize = true;
            this.check_I_AddtoDB.Location = new System.Drawing.Point(6, 262);
            this.check_I_AddtoDB.Name = "check_I_AddtoDB";
            this.check_I_AddtoDB.Size = new System.Drawing.Size(107, 17);
            this.check_I_AddtoDB.TabIndex = 4;
            this.check_I_AddtoDB.Text = "Add to DataBase";
            this.check_I_AddtoDB.UseVisualStyleBackColor = true;
            // 
            // check_PowerQuadrent
            // 
            this.check_PowerQuadrent.AutoSize = true;
            this.check_PowerQuadrent.Location = new System.Drawing.Point(266, 250);
            this.check_PowerQuadrent.Name = "check_PowerQuadrent";
            this.check_PowerQuadrent.Size = new System.Drawing.Size(103, 17);
            this.check_PowerQuadrent.TabIndex = 2;
            this.check_PowerQuadrent.Text = "Power Quadrent";
            this.check_PowerQuadrent.UseVisualStyleBackColor = true;
            this.check_PowerQuadrent.Visible = false;
            // 
            // gpPhase
            // 
            this.gpPhase.Controls.Add(this.rdbPhC);
            this.gpPhase.Controls.Add(this.rdbAllPhases);
            this.gpPhase.Controls.Add(this.rdbPhA);
            this.gpPhase.Controls.Add(this.rdbPhB);
            this.gpPhase.Location = new System.Drawing.Point(258, 50);
            this.gpPhase.Name = "gpPhase";
            this.gpPhase.Size = new System.Drawing.Size(111, 107);
            this.gpPhase.TabIndex = 2;
            this.gpPhase.TabStop = false;
            this.gpPhase.Text = "Phase";
            this.gpPhase.Visible = false;
            // 
            // rdbPhC
            // 
            this.rdbPhC.AutoSize = true;
            this.rdbPhC.Location = new System.Drawing.Point(17, 78);
            this.rdbPhC.Name = "rdbPhC";
            this.rdbPhC.Size = new System.Drawing.Size(65, 17);
            this.rdbPhC.TabIndex = 0;
            this.rdbPhC.Text = "Phase C";
            this.rdbPhC.UseVisualStyleBackColor = true;
            // 
            // rdbAllPhases
            // 
            this.rdbAllPhases.AutoSize = true;
            this.rdbAllPhases.Checked = true;
            this.rdbAllPhases.Location = new System.Drawing.Point(17, 15);
            this.rdbAllPhases.Name = "rdbAllPhases";
            this.rdbAllPhases.Size = new System.Drawing.Size(74, 17);
            this.rdbAllPhases.TabIndex = 0;
            this.rdbAllPhases.TabStop = true;
            this.rdbAllPhases.Text = "All Phases";
            this.rdbAllPhases.UseVisualStyleBackColor = true;
            // 
            // rdbPhA
            // 
            this.rdbPhA.AutoSize = true;
            this.rdbPhA.Location = new System.Drawing.Point(17, 36);
            this.rdbPhA.Name = "rdbPhA";
            this.rdbPhA.Size = new System.Drawing.Size(65, 17);
            this.rdbPhA.TabIndex = 0;
            this.rdbPhA.Text = "Phase A";
            this.rdbPhA.UseVisualStyleBackColor = true;
            // 
            // rdbPhB
            // 
            this.rdbPhB.AutoSize = true;
            this.rdbPhB.Location = new System.Drawing.Point(17, 57);
            this.rdbPhB.Name = "rdbPhB";
            this.rdbPhB.Size = new System.Drawing.Size(65, 17);
            this.rdbPhB.TabIndex = 0;
            this.rdbPhB.Text = "Phase B";
            this.rdbPhB.UseVisualStyleBackColor = true;
            // 
            // GpQuantity
            // 
            this.GpQuantity.Controls.Add(this.check_ReactivePower);
            this.GpQuantity.Controls.Add(this.check_readMDI_Interval);
            this.GpQuantity.Controls.Add(this.check_Mdi);
            this.GpQuantity.Controls.Add(this.check_Apparent);
            this.GpQuantity.Controls.Add(this.check_misc);
            this.GpQuantity.Controls.Add(this.check_AllPhy);
            this.GpQuantity.Controls.Add(this.check_Voltage);
            this.GpQuantity.Controls.Add(this.check_Powerfactor);
            this.GpQuantity.Controls.Add(this.check_ActivePower);
            this.GpQuantity.Controls.Add(this.check_Current);
            this.GpQuantity.Location = new System.Drawing.Point(6, 16);
            this.GpQuantity.Name = "GpQuantity";
            this.GpQuantity.Size = new System.Drawing.Size(142, 240);
            this.GpQuantity.TabIndex = 2;
            this.GpQuantity.TabStop = false;
            this.GpQuantity.Text = "Quantity";
            // 
            // check_ReactivePower
            // 
            this.check_ReactivePower.AutoSize = true;
            this.check_ReactivePower.Location = new System.Drawing.Point(7, 124);
            this.check_ReactivePower.Name = "check_ReactivePower";
            this.check_ReactivePower.Size = new System.Drawing.Size(102, 17);
            this.check_ReactivePower.TabIndex = 2;
            this.check_ReactivePower.Text = "Reactive Power";
            this.check_ReactivePower.UseVisualStyleBackColor = true;
            // 
            // check_readMDI_Interval
            // 
            this.check_readMDI_Interval.AutoSize = true;
            this.check_readMDI_Interval.Location = new System.Drawing.Point(6, 210);
            this.check_readMDI_Interval.Name = "check_readMDI_Interval";
            this.check_readMDI_Interval.Size = new System.Drawing.Size(87, 17);
            this.check_readMDI_Interval.TabIndex = 3;
            this.check_readMDI_Interval.Text = " MDI Interval";
            this.check_readMDI_Interval.UseVisualStyleBackColor = true;
            // 
            // check_Mdi
            // 
            this.check_Mdi.AutoSize = true;
            this.check_Mdi.Location = new System.Drawing.Point(7, 187);
            this.check_Mdi.Name = "check_Mdi";
            this.check_Mdi.Size = new System.Drawing.Size(49, 17);
            this.check_Mdi.TabIndex = 3;
            this.check_Mdi.Text = " MDI";
            this.check_Mdi.UseVisualStyleBackColor = true;
            // 
            // check_Apparent
            // 
            this.check_Apparent.AutoSize = true;
            this.check_Apparent.Location = new System.Drawing.Point(7, 145);
            this.check_Apparent.Name = "check_Apparent";
            this.check_Apparent.Size = new System.Drawing.Size(102, 17);
            this.check_Apparent.TabIndex = 3;
            this.check_Apparent.Text = "Apparant Power";
            this.check_Apparent.UseVisualStyleBackColor = true;
            // 
            // check_misc
            // 
            this.check_misc.AutoSize = true;
            this.check_misc.Location = new System.Drawing.Point(7, 166);
            this.check_misc.Name = "check_misc";
            this.check_misc.Size = new System.Drawing.Size(51, 17);
            this.check_misc.TabIndex = 3;
            this.check_misc.Text = " Misc";
            this.check_misc.UseVisualStyleBackColor = true;
            // 
            // check_AllPhy
            // 
            this.check_AllPhy.AutoSize = true;
            this.check_AllPhy.Location = new System.Drawing.Point(7, 19);
            this.check_AllPhy.Name = "check_AllPhy";
            this.check_AllPhy.Size = new System.Drawing.Size(70, 17);
            this.check_AllPhy.TabIndex = 2;
            this.check_AllPhy.Text = "Select All";
            this.check_AllPhy.UseVisualStyleBackColor = true;
            this.check_AllPhy.CheckedChanged += new System.EventHandler(this.check_AllPhy_CheckedChanged);
            // 
            // check_Voltage
            // 
            this.check_Voltage.AutoSize = true;
            this.check_Voltage.Location = new System.Drawing.Point(7, 40);
            this.check_Voltage.Name = "check_Voltage";
            this.check_Voltage.Size = new System.Drawing.Size(62, 17);
            this.check_Voltage.TabIndex = 2;
            this.check_Voltage.Text = "Voltage";
            this.check_Voltage.UseVisualStyleBackColor = true;
            // 
            // check_Powerfactor
            // 
            this.check_Powerfactor.AutoSize = true;
            this.check_Powerfactor.Location = new System.Drawing.Point(7, 82);
            this.check_Powerfactor.Name = "check_Powerfactor";
            this.check_Powerfactor.Size = new System.Drawing.Size(89, 17);
            this.check_Powerfactor.TabIndex = 2;
            this.check_Powerfactor.Text = "Power Factor";
            this.check_Powerfactor.UseVisualStyleBackColor = true;
            // 
            // check_ActivePower
            // 
            this.check_ActivePower.AutoSize = true;
            this.check_ActivePower.Location = new System.Drawing.Point(7, 103);
            this.check_ActivePower.Name = "check_ActivePower";
            this.check_ActivePower.Size = new System.Drawing.Size(89, 17);
            this.check_ActivePower.TabIndex = 2;
            this.check_ActivePower.Text = "Active Power";
            this.check_ActivePower.UseVisualStyleBackColor = true;
            // 
            // check_Current
            // 
            this.check_Current.AutoSize = true;
            this.check_Current.Location = new System.Drawing.Point(7, 61);
            this.check_Current.Name = "check_Current";
            this.check_Current.Size = new System.Drawing.Size(60, 17);
            this.check_Current.TabIndex = 2;
            this.check_Current.Text = "Current";
            this.check_Current.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(255, 195);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "STOP";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // btnIReadInst
            // 
            this.btnIReadInst.Location = new System.Drawing.Point(255, 163);
            this.btnIReadInst.Name = "btnIReadInst";
            this.btnIReadInst.Size = new System.Drawing.Size(114, 23);
            this.btnIReadInst.TabIndex = 1;
            this.btnIReadInst.Text = "Read Values";
            this.btnIReadInst.UseVisualStyleBackColor = true;
            this.btnIReadInst.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.grid_Instanstanouse);
            this.groupBox3.Location = new System.Drawing.Point(175, 23);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(565, 251);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Electrical Qunantities";
            // 
            // grid_Instanstanouse
            // 
            this.grid_Instanstanouse.AllowUserToAddRows = false;
            this.grid_Instanstanouse.AllowUserToDeleteRows = false;
            this.grid_Instanstanouse.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid_Instanstanouse.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grid_Instanstanouse.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grid_Instanstanouse.DefaultCellStyle = dataGridViewCellStyle2;
            this.grid_Instanstanouse.Location = new System.Drawing.Point(6, 18);
            this.grid_Instanstanouse.Name = "grid_Instanstanouse";
            this.grid_Instanstanouse.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid_Instanstanouse.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grid_Instanstanouse.RowHeadersWidth = 200;
            this.grid_Instanstanouse.Size = new System.Drawing.Size(546, 222);
            this.grid_Instanstanouse.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Phase A";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 76;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Phase B";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 76;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Phase C";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 76;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Phase Avg/Total";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 117;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbl_meter_datetime);
            this.groupBox2.Controls.Add(this.lbl_meter_serial);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.grid_misc);
            this.groupBox2.Location = new System.Drawing.Point(746, 23);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(279, 415);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "MISC";
            // 
            // lbl_meter_datetime
            // 
            this.lbl_meter_datetime.AutoSize = true;
            this.lbl_meter_datetime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_meter_datetime.Location = new System.Drawing.Point(127, 36);
            this.lbl_meter_datetime.Name = "lbl_meter_datetime";
            this.lbl_meter_datetime.Size = new System.Drawing.Size(32, 15);
            this.lbl_meter_datetime.TabIndex = 3;
            this.lbl_meter_datetime.Text = "-----";
            // 
            // lbl_meter_serial
            // 
            this.lbl_meter_serial.AutoSize = true;
            this.lbl_meter_serial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_meter_serial.Location = new System.Drawing.Point(127, 16);
            this.lbl_meter_serial.Name = "lbl_meter_serial";
            this.lbl_meter_serial.Size = new System.Drawing.Size(32, 15);
            this.lbl_meter_serial.TabIndex = 2;
            this.lbl_meter_serial.Text = "-----";
            this.lbl_meter_serial.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Meter Date/Time";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Meter Serial No";
            this.label1.Visible = false;
            // 
            // grid_misc
            // 
            this.grid_misc.AllowUserToAddRows = false;
            this.grid_misc.AllowUserToDeleteRows = false;
            this.grid_misc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.grid_misc.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.grid_misc.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid_misc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.grid_misc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Value});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grid_misc.DefaultCellStyle = dataGridViewCellStyle5;
            this.grid_misc.Location = new System.Drawing.Point(9, 52);
            this.grid_misc.Name = "grid_misc";
            this.grid_misc.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid_misc.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.grid_misc.RowHeadersWidth = 200;
            this.grid_misc.Size = new System.Drawing.Size(258, 353);
            this.grid_misc.TabIndex = 1;
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            this.Value.Width = 59;
            // 
            // tab_MDI
            // 
            this.tab_MDI.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tab_MDI.Controls.Add(this.tabControl2);
            this.tab_MDI.Location = new System.Drawing.Point(4, 22);
            this.tab_MDI.Name = "tab_MDI";
            this.tab_MDI.Size = new System.Drawing.Size(1038, 453);
            this.tab_MDI.TabIndex = 3;
            this.tab_MDI.Text = "MDI";
            this.tab_MDI.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tpMDI);
            this.tabControl2.Controls.Add(this.tbDebugMDI);
            this.tabControl2.Controls.Add(this.tpInstantaneousMDI);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(1038, 453);
            this.tabControl2.TabIndex = 10;
            // 
            // tpMDI
            // 
            this.tpMDI.Controls.Add(this.panel2);
            this.tpMDI.Controls.Add(this.panel1);
            this.tpMDI.Location = new System.Drawing.Point(4, 22);
            this.tpMDI.Name = "tpMDI";
            this.tpMDI.Padding = new System.Windows.Forms.Padding(3);
            this.tpMDI.Size = new System.Drawing.Size(1030, 427);
            this.tpMDI.TabIndex = 0;
            this.tpMDI.Text = "MDI";
            this.tpMDI.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.grid_CurrentMDI);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(203, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(824, 421);
            this.panel2.TabIndex = 11;
            // 
            // grid_CurrentMDI
            // 
            this.grid_CurrentMDI.AllowUserToAddRows = false;
            this.grid_CurrentMDI.AllowUserToDeleteRows = false;
            this.grid_CurrentMDI.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.grid_CurrentMDI.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid_CurrentMDI.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.grid_CurrentMDI.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.QuantityLabel,
            this.T1_Val,
            this.T2_Val,
            this.T3_Val,
            this.T4_Val,
            this.TL_Val});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grid_CurrentMDI.DefaultCellStyle = dataGridViewCellStyle9;
            this.grid_CurrentMDI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid_CurrentMDI.Location = new System.Drawing.Point(0, 0);
            this.grid_CurrentMDI.Name = "grid_CurrentMDI";
            this.grid_CurrentMDI.ReadOnly = true;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid_CurrentMDI.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.grid_CurrentMDI.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.grid_CurrentMDI.Size = new System.Drawing.Size(824, 421);
            this.grid_CurrentMDI.TabIndex = 7;
            // 
            // QuantityLabel
            // 
            this.QuantityLabel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.QuantityLabel.DefaultCellStyle = dataGridViewCellStyle8;
            this.QuantityLabel.HeaderText = "Quantity Label";
            this.QuantityLabel.Name = "QuantityLabel";
            this.QuantityLabel.ReadOnly = true;
            // 
            // T1_Val
            // 
            this.T1_Val.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.T1_Val.HeaderText = "T1";
            this.T1_Val.Name = "T1_Val";
            this.T1_Val.ReadOnly = true;
            this.T1_Val.Width = 45;
            // 
            // T2_Val
            // 
            this.T2_Val.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.T2_Val.HeaderText = "T2";
            this.T2_Val.Name = "T2_Val";
            this.T2_Val.ReadOnly = true;
            this.T2_Val.Width = 45;
            // 
            // T3_Val
            // 
            this.T3_Val.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.T3_Val.HeaderText = "T3";
            this.T3_Val.Name = "T3_Val";
            this.T3_Val.ReadOnly = true;
            this.T3_Val.Width = 45;
            // 
            // T4_Val
            // 
            this.T4_Val.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.T4_Val.HeaderText = "T4";
            this.T4_Val.Name = "T4_Val";
            this.T4_Val.ReadOnly = true;
            this.T4_Val.Width = 45;
            // 
            // TL_Val
            // 
            this.TL_Val.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.TL_Val.HeaderText = "TL";
            this.TL_Val.Name = "TL_Val";
            this.TL_Val.ReadOnly = true;
            this.TL_Val.Width = 45;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chk_All);
            this.panel1.Controls.Add(this.chkLstMDIs);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 421);
            this.panel1.TabIndex = 10;
            // 
            // chk_All
            // 
            this.chk_All.AutoSize = true;
            this.chk_All.Location = new System.Drawing.Point(3, 399);
            this.chk_All.Name = "chk_All";
            this.chk_All.Size = new System.Drawing.Size(71, 17);
            this.chk_All.TabIndex = 15;
            this.chk_All.Text = "Check All";
            this.chk_All.UseVisualStyleBackColor = true;
            this.chk_All.CheckedChanged += new System.EventHandler(this.chk_All_CheckedChanged);
            // 
            // chkLstMDIs
            // 
            this.chkLstMDIs.CheckOnClick = true;
            this.chkLstMDIs.FormattingEnabled = true;
            this.chkLstMDIs.Location = new System.Drawing.Point(0, 0);
            this.chkLstMDIs.Name = "chkLstMDIs";
            this.chkLstMDIs.Size = new System.Drawing.Size(200, 394);
            this.chkLstMDIs.TabIndex = 0;
            // 
            // tbDebugMDI
            // 
            this.tbDebugMDI.Controls.Add(this.gp_MDIs);
            this.tbDebugMDI.Location = new System.Drawing.Point(4, 22);
            this.tbDebugMDI.Name = "tbDebugMDI";
            this.tbDebugMDI.Padding = new System.Windows.Forms.Padding(3);
            this.tbDebugMDI.Size = new System.Drawing.Size(1030, 427);
            this.tbDebugMDI.TabIndex = 1;
            this.tbDebugMDI.Text = "Debug MDI";
            this.tbDebugMDI.UseVisualStyleBackColor = true;
            // 
            // gp_MDIs
            // 
            this.gp_MDIs.Controls.Add(this.lbl_MDI_SlideCounter);
            this.gp_MDIs.Controls.Add(this.label62);
            this.gp_MDIs.Controls.Add(this.lbl_MDI_Previous_Energy);
            this.gp_MDIs.Controls.Add(this.lbl_MDI_Previous_Power);
            this.gp_MDIs.Controls.Add(this.lbl_MDI_Running_Energy);
            this.gp_MDIs.Controls.Add(this.lbl_MDI_Running_Power);
            this.gp_MDIs.Controls.Add(this.lbl_MDI_TimeLeft);
            this.gp_MDIs.Controls.Add(this.lbl_MDI_SlideCount);
            this.gp_MDIs.Controls.Add(this.lbl_MDI_Counter);
            this.gp_MDIs.Controls.Add(this.lbl_MDI_time);
            this.gp_MDIs.Controls.Add(this.label54);
            this.gp_MDIs.Controls.Add(this.label50);
            this.gp_MDIs.Controls.Add(this.label48);
            this.gp_MDIs.Controls.Add(this.label52);
            this.gp_MDIs.Controls.Add(this.label58);
            this.gp_MDIs.Controls.Add(this.label56);
            this.gp_MDIs.Controls.Add(this.label49);
            this.gp_MDIs.Controls.Add(this.label44);
            this.gp_MDIs.Controls.Add(this.grid_MDI);
            this.gp_MDIs.Location = new System.Drawing.Point(16, 18);
            this.gp_MDIs.Name = "gp_MDIs";
            this.gp_MDIs.Size = new System.Drawing.Size(528, 209);
            this.gp_MDIs.TabIndex = 4;
            this.gp_MDIs.TabStop = false;
            this.gp_MDIs.Text = "MDI";
            // 
            // lbl_MDI_SlideCounter
            // 
            this.lbl_MDI_SlideCounter.AutoSize = true;
            this.lbl_MDI_SlideCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MDI_SlideCounter.Location = new System.Drawing.Point(330, 93);
            this.lbl_MDI_SlideCounter.Name = "lbl_MDI_SlideCounter";
            this.lbl_MDI_SlideCounter.Size = new System.Drawing.Size(12, 15);
            this.lbl_MDI_SlideCounter.TabIndex = 7;
            this.lbl_MDI_SlideCounter.Text = "-";
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label62.Location = new System.Drawing.Point(217, 93);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(94, 15);
            this.label62.TabIndex = 6;
            this.label62.Text = "Slide Counter";
            // 
            // lbl_MDI_Previous_Energy
            // 
            this.lbl_MDI_Previous_Energy.AutoSize = true;
            this.lbl_MDI_Previous_Energy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MDI_Previous_Energy.Location = new System.Drawing.Point(392, 70);
            this.lbl_MDI_Previous_Energy.Name = "lbl_MDI_Previous_Energy";
            this.lbl_MDI_Previous_Energy.Size = new System.Drawing.Size(12, 15);
            this.lbl_MDI_Previous_Energy.TabIndex = 5;
            this.lbl_MDI_Previous_Energy.Text = "-";
            // 
            // lbl_MDI_Previous_Power
            // 
            this.lbl_MDI_Previous_Power.AutoSize = true;
            this.lbl_MDI_Previous_Power.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MDI_Previous_Power.Location = new System.Drawing.Point(392, 43);
            this.lbl_MDI_Previous_Power.Name = "lbl_MDI_Previous_Power";
            this.lbl_MDI_Previous_Power.Size = new System.Drawing.Size(12, 15);
            this.lbl_MDI_Previous_Power.TabIndex = 5;
            this.lbl_MDI_Previous_Power.Text = "-";
            // 
            // lbl_MDI_Running_Energy
            // 
            this.lbl_MDI_Running_Energy.AutoSize = true;
            this.lbl_MDI_Running_Energy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MDI_Running_Energy.Location = new System.Drawing.Point(330, 70);
            this.lbl_MDI_Running_Energy.Name = "lbl_MDI_Running_Energy";
            this.lbl_MDI_Running_Energy.Size = new System.Drawing.Size(12, 15);
            this.lbl_MDI_Running_Energy.TabIndex = 5;
            this.lbl_MDI_Running_Energy.Text = "-";
            // 
            // lbl_MDI_Running_Power
            // 
            this.lbl_MDI_Running_Power.AutoSize = true;
            this.lbl_MDI_Running_Power.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MDI_Running_Power.Location = new System.Drawing.Point(330, 43);
            this.lbl_MDI_Running_Power.Name = "lbl_MDI_Running_Power";
            this.lbl_MDI_Running_Power.Size = new System.Drawing.Size(12, 15);
            this.lbl_MDI_Running_Power.TabIndex = 5;
            this.lbl_MDI_Running_Power.Text = "-";
            // 
            // lbl_MDI_TimeLeft
            // 
            this.lbl_MDI_TimeLeft.AutoSize = true;
            this.lbl_MDI_TimeLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MDI_TimeLeft.Location = new System.Drawing.Point(103, 70);
            this.lbl_MDI_TimeLeft.Name = "lbl_MDI_TimeLeft";
            this.lbl_MDI_TimeLeft.Size = new System.Drawing.Size(12, 15);
            this.lbl_MDI_TimeLeft.TabIndex = 5;
            this.lbl_MDI_TimeLeft.Text = "-";
            // 
            // lbl_MDI_SlideCount
            // 
            this.lbl_MDI_SlideCount.AutoSize = true;
            this.lbl_MDI_SlideCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MDI_SlideCount.Location = new System.Drawing.Point(103, 93);
            this.lbl_MDI_SlideCount.Name = "lbl_MDI_SlideCount";
            this.lbl_MDI_SlideCount.Size = new System.Drawing.Size(12, 15);
            this.lbl_MDI_SlideCount.TabIndex = 5;
            this.lbl_MDI_SlideCount.Text = "-";
            // 
            // lbl_MDI_Counter
            // 
            this.lbl_MDI_Counter.AutoSize = true;
            this.lbl_MDI_Counter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MDI_Counter.Location = new System.Drawing.Point(103, 43);
            this.lbl_MDI_Counter.Name = "lbl_MDI_Counter";
            this.lbl_MDI_Counter.Size = new System.Drawing.Size(12, 15);
            this.lbl_MDI_Counter.TabIndex = 5;
            this.lbl_MDI_Counter.Text = "-";
            // 
            // lbl_MDI_time
            // 
            this.lbl_MDI_time.AutoSize = true;
            this.lbl_MDI_time.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MDI_time.Location = new System.Drawing.Point(103, 16);
            this.lbl_MDI_time.Name = "lbl_MDI_time";
            this.lbl_MDI_time.Size = new System.Drawing.Size(12, 15);
            this.lbl_MDI_time.TabIndex = 5;
            this.lbl_MDI_time.Text = "-";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.Location = new System.Drawing.Point(217, 43);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(26, 15);
            this.label54.TabIndex = 4;
            this.label54.Text = "kW";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.Location = new System.Drawing.Point(366, 16);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(62, 15);
            this.label50.TabIndex = 4;
            this.label50.Text = "Previous";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.Location = new System.Drawing.Point(299, 16);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(61, 15);
            this.label48.TabIndex = 4;
            this.label48.Text = "Running";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.Location = new System.Drawing.Point(217, 70);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(33, 15);
            this.label52.TabIndex = 4;
            this.label52.Text = "kvar";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label58.Location = new System.Drawing.Point(10, 70);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(67, 15);
            this.label58.TabIndex = 4;
            this.label58.Text = "Time Left";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label56.Location = new System.Drawing.Point(10, 93);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(81, 15);
            this.label56.TabIndex = 4;
            this.label56.Text = "Slide Count";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.Location = new System.Drawing.Point(9, 43);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(95, 15);
            this.label49.TabIndex = 4;
            this.label49.Text = "Time Elapsed";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(10, 16);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(34, 13);
            this.label44.TabIndex = 4;
            this.label44.Text = "Time";
            // 
            // grid_MDI
            // 
            this.grid_MDI.AllowUserToAddRows = false;
            this.grid_MDI.AllowUserToDeleteRows = false;
            this.grid_MDI.Anchor = System.Windows.Forms.AnchorStyles.None;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid_MDI.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.grid_MDI.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Slide1,
            this.Slide2,
            this.Slide3,
            this.Slide4,
            this.Slide5,
            this.Slide6});
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grid_MDI.DefaultCellStyle = dataGridViewCellStyle12;
            this.grid_MDI.Location = new System.Drawing.Point(12, 114);
            this.grid_MDI.Name = "grid_MDI";
            this.grid_MDI.ReadOnly = true;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid_MDI.RowHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.grid_MDI.RowHeadersWidth = 70;
            this.grid_MDI.Size = new System.Drawing.Size(510, 88);
            this.grid_MDI.TabIndex = 3;
            // 
            // Slide1
            // 
            this.Slide1.HeaderText = "Slide1";
            this.Slide1.Name = "Slide1";
            this.Slide1.ReadOnly = true;
            this.Slide1.Width = 67;
            // 
            // Slide2
            // 
            this.Slide2.HeaderText = "Slide2";
            this.Slide2.Name = "Slide2";
            this.Slide2.ReadOnly = true;
            this.Slide2.Width = 67;
            // 
            // Slide3
            // 
            this.Slide3.HeaderText = "Slide3";
            this.Slide3.Name = "Slide3";
            this.Slide3.ReadOnly = true;
            this.Slide3.Width = 67;
            // 
            // Slide4
            // 
            this.Slide4.HeaderText = "Slide4";
            this.Slide4.Name = "Slide4";
            this.Slide4.ReadOnly = true;
            this.Slide4.Width = 67;
            // 
            // Slide5
            // 
            this.Slide5.HeaderText = "Slide5";
            this.Slide5.Name = "Slide5";
            this.Slide5.ReadOnly = true;
            this.Slide5.Width = 67;
            // 
            // Slide6
            // 
            this.Slide6.HeaderText = "Slide6";
            this.Slide6.Name = "Slide6";
            this.Slide6.ReadOnly = true;
            // 
            // tpInstantaneousMDI
            // 
            this.tpInstantaneousMDI.Controls.Add(this.groupBox11);
            this.tpInstantaneousMDI.Controls.Add(this.groupBox10);
            this.tpInstantaneousMDI.Controls.Add(this.lblReadMDI);
            this.tpInstantaneousMDI.Controls.Add(this.listInstantaneousMDI);
            this.tpInstantaneousMDI.Location = new System.Drawing.Point(4, 22);
            this.tpInstantaneousMDI.Name = "tpInstantaneousMDI";
            this.tpInstantaneousMDI.Padding = new System.Windows.Forms.Padding(3);
            this.tpInstantaneousMDI.Size = new System.Drawing.Size(1030, 427);
            this.tpInstantaneousMDI.TabIndex = 2;
            this.tpInstantaneousMDI.Text = "Instantaneous MDI";
            this.tpInstantaneousMDI.UseVisualStyleBackColor = true;
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.btnGetMditoMonitor);
            this.groupBox11.Controls.Add(this.label78);
            this.groupBox11.Controls.Add(this.cbxMditoMonitor);
            this.groupBox11.Controls.Add(this.btnSetMonitoredValue);
            this.groupBox11.Location = new System.Drawing.Point(6, 257);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(495, 51);
            this.groupBox11.TabIndex = 17;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Monitored MDI";
            // 
            // btnGetMditoMonitor
            // 
            this.btnGetMditoMonitor.Location = new System.Drawing.Point(414, 19);
            this.btnGetMditoMonitor.Name = "btnGetMditoMonitor";
            this.btnGetMditoMonitor.Size = new System.Drawing.Size(62, 23);
            this.btnGetMditoMonitor.TabIndex = 4;
            this.btnGetMditoMonitor.Text = "Get";
            this.btnGetMditoMonitor.UseVisualStyleBackColor = true;
            this.btnGetMditoMonitor.Click += new System.EventHandler(this.btnGetMditoMonitor_Click);
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.BackColor = System.Drawing.Color.Transparent;
            this.label78.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label78.ForeColor = System.Drawing.Color.Navy;
            this.label78.Location = new System.Drawing.Point(6, 23);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(128, 15);
            this.label78.TabIndex = 2;
            this.label78.Text = "Select MDI to Monitor";
            // 
            // cbxMditoMonitor
            // 
            this.cbxMditoMonitor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMditoMonitor.FormattingEnabled = true;
            this.cbxMditoMonitor.Location = new System.Drawing.Point(148, 21);
            this.cbxMditoMonitor.Name = "cbxMditoMonitor";
            this.cbxMditoMonitor.Size = new System.Drawing.Size(190, 21);
            this.cbxMditoMonitor.TabIndex = 3;
            // 
            // btnSetMonitoredValue
            // 
            this.btnSetMonitoredValue.Location = new System.Drawing.Point(346, 19);
            this.btnSetMonitoredValue.Name = "btnSetMonitoredValue";
            this.btnSetMonitoredValue.Size = new System.Drawing.Size(62, 23);
            this.btnSetMonitoredValue.TabIndex = 2;
            this.btnSetMonitoredValue.Text = "Set";
            this.btnSetMonitoredValue.UseVisualStyleBackColor = true;
            this.btnSetMonitoredValue.Click += new System.EventHandler(this.btnSetMonitoredValue_Click_1);
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.lbl_limit_over_volt);
            this.groupBox10.Controls.Add(this.label63);
            this.groupBox10.Controls.Add(this.lblCurrentAverageValue);
            this.groupBox10.Controls.Add(this.lblNumberOfPeriods);
            this.groupBox10.Controls.Add(this.label70);
            this.groupBox10.Controls.Add(this.lblPeriod);
            this.groupBox10.Controls.Add(this.lblLastAverageValue);
            this.groupBox10.Controls.Add(this.label77);
            this.groupBox10.Controls.Add(this.label72);
            this.groupBox10.Controls.Add(this.lblCaptureTime);
            this.groupBox10.Controls.Add(this.lblScalerUnit);
            this.groupBox10.Controls.Add(this.label76);
            this.groupBox10.Controls.Add(this.label75);
            this.groupBox10.Controls.Add(this.lblStartCaptureTime);
            this.groupBox10.Controls.Add(this.lblStatus);
            this.groupBox10.Controls.Add(this.label74);
            this.groupBox10.Location = new System.Drawing.Point(154, 6);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(347, 245);
            this.groupBox10.TabIndex = 16;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Detail";
            // 
            // lbl_limit_over_volt
            // 
            this.lbl_limit_over_volt.AutoSize = true;
            this.lbl_limit_over_volt.BackColor = System.Drawing.Color.Transparent;
            this.lbl_limit_over_volt.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lbl_limit_over_volt.ForeColor = System.Drawing.Color.Navy;
            this.lbl_limit_over_volt.Location = new System.Drawing.Point(6, 16);
            this.lbl_limit_over_volt.Name = "lbl_limit_over_volt";
            this.lbl_limit_over_volt.Size = new System.Drawing.Size(131, 15);
            this.lbl_limit_over_volt.TabIndex = 1;
            this.lbl_limit_over_volt.Text = "Current Average Value";
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.BackColor = System.Drawing.Color.Transparent;
            this.label63.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label63.ForeColor = System.Drawing.Color.Navy;
            this.label63.Location = new System.Drawing.Point(27, 42);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(110, 15);
            this.label63.TabIndex = 1;
            this.label63.Text = "Last Average Value";
            // 
            // lblCurrentAverageValue
            // 
            this.lblCurrentAverageValue.AutoSize = true;
            this.lblCurrentAverageValue.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentAverageValue.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblCurrentAverageValue.ForeColor = System.Drawing.Color.Navy;
            this.lblCurrentAverageValue.Location = new System.Drawing.Point(179, 16);
            this.lblCurrentAverageValue.Name = "lblCurrentAverageValue";
            this.lblCurrentAverageValue.Size = new System.Drawing.Size(131, 15);
            this.lblCurrentAverageValue.TabIndex = 1;
            this.lblCurrentAverageValue.Text = "Current Average Value";
            // 
            // lblNumberOfPeriods
            // 
            this.lblNumberOfPeriods.AutoSize = true;
            this.lblNumberOfPeriods.BackColor = System.Drawing.Color.Transparent;
            this.lblNumberOfPeriods.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblNumberOfPeriods.ForeColor = System.Drawing.Color.Navy;
            this.lblNumberOfPeriods.Location = new System.Drawing.Point(179, 214);
            this.lblNumberOfPeriods.Name = "lblNumberOfPeriods";
            this.lblNumberOfPeriods.Size = new System.Drawing.Size(113, 15);
            this.lblNumberOfPeriods.TabIndex = 1;
            this.lblNumberOfPeriods.Text = "Number Of Periods";
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.BackColor = System.Drawing.Color.Transparent;
            this.label70.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label70.ForeColor = System.Drawing.Color.Navy;
            this.label70.Location = new System.Drawing.Point(72, 69);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(65, 15);
            this.label70.TabIndex = 1;
            this.label70.Text = "Scaler Unit";
            // 
            // lblPeriod
            // 
            this.lblPeriod.AutoSize = true;
            this.lblPeriod.BackColor = System.Drawing.Color.Transparent;
            this.lblPeriod.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblPeriod.ForeColor = System.Drawing.Color.Navy;
            this.lblPeriod.Location = new System.Drawing.Point(179, 185);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(43, 15);
            this.lblPeriod.TabIndex = 1;
            this.lblPeriod.Text = "Period";
            // 
            // lblLastAverageValue
            // 
            this.lblLastAverageValue.AutoSize = true;
            this.lblLastAverageValue.BackColor = System.Drawing.Color.Transparent;
            this.lblLastAverageValue.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblLastAverageValue.ForeColor = System.Drawing.Color.Navy;
            this.lblLastAverageValue.Location = new System.Drawing.Point(179, 42);
            this.lblLastAverageValue.Name = "lblLastAverageValue";
            this.lblLastAverageValue.Size = new System.Drawing.Size(110, 15);
            this.lblLastAverageValue.TabIndex = 1;
            this.lblLastAverageValue.Text = "Last Average Value";
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.BackColor = System.Drawing.Color.Transparent;
            this.label77.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label77.ForeColor = System.Drawing.Color.Navy;
            this.label77.Location = new System.Drawing.Point(24, 214);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(113, 15);
            this.label77.TabIndex = 1;
            this.label77.Text = "Number Of Periods";
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.BackColor = System.Drawing.Color.Transparent;
            this.label72.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label72.ForeColor = System.Drawing.Color.Navy;
            this.label72.Location = new System.Drawing.Point(96, 98);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(41, 15);
            this.label72.TabIndex = 1;
            this.label72.Text = "Status";
            // 
            // lblCaptureTime
            // 
            this.lblCaptureTime.AutoSize = true;
            this.lblCaptureTime.BackColor = System.Drawing.Color.Transparent;
            this.lblCaptureTime.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblCaptureTime.ForeColor = System.Drawing.Color.Navy;
            this.lblCaptureTime.Location = new System.Drawing.Point(179, 127);
            this.lblCaptureTime.Name = "lblCaptureTime";
            this.lblCaptureTime.Size = new System.Drawing.Size(81, 15);
            this.lblCaptureTime.TabIndex = 1;
            this.lblCaptureTime.Text = "Capture Time";
            // 
            // lblScalerUnit
            // 
            this.lblScalerUnit.AutoSize = true;
            this.lblScalerUnit.BackColor = System.Drawing.Color.Transparent;
            this.lblScalerUnit.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblScalerUnit.ForeColor = System.Drawing.Color.Navy;
            this.lblScalerUnit.Location = new System.Drawing.Point(179, 69);
            this.lblScalerUnit.Name = "lblScalerUnit";
            this.lblScalerUnit.Size = new System.Drawing.Size(65, 15);
            this.lblScalerUnit.TabIndex = 1;
            this.lblScalerUnit.Text = "Scaler Unit";
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.BackColor = System.Drawing.Color.Transparent;
            this.label76.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label76.ForeColor = System.Drawing.Color.Navy;
            this.label76.Location = new System.Drawing.Point(94, 185);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(43, 15);
            this.label76.TabIndex = 1;
            this.label76.Text = "Period";
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.BackColor = System.Drawing.Color.Transparent;
            this.label75.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label75.ForeColor = System.Drawing.Color.Navy;
            this.label75.Location = new System.Drawing.Point(27, 156);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(110, 15);
            this.label75.TabIndex = 1;
            this.label75.Text = "Start Time Current";
            // 
            // lblStartCaptureTime
            // 
            this.lblStartCaptureTime.AutoSize = true;
            this.lblStartCaptureTime.BackColor = System.Drawing.Color.Transparent;
            this.lblStartCaptureTime.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblStartCaptureTime.ForeColor = System.Drawing.Color.Navy;
            this.lblStartCaptureTime.Location = new System.Drawing.Point(179, 156);
            this.lblStartCaptureTime.Name = "lblStartCaptureTime";
            this.lblStartCaptureTime.Size = new System.Drawing.Size(110, 15);
            this.lblStartCaptureTime.TabIndex = 1;
            this.lblStartCaptureTime.Text = "Start Time Current";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblStatus.ForeColor = System.Drawing.Color.Navy;
            this.lblStatus.Location = new System.Drawing.Point(179, 98);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(19, 15);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "---";
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.BackColor = System.Drawing.Color.Transparent;
            this.label74.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label74.ForeColor = System.Drawing.Color.Navy;
            this.label74.Location = new System.Drawing.Point(56, 127);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(81, 15);
            this.label74.TabIndex = 1;
            this.label74.Text = "Capture Time";
            // 
            // lblReadMDI
            // 
            this.lblReadMDI.Image = ((System.Drawing.Image)(resources.GetObject("lblReadMDI.Image")));
            this.lblReadMDI.Location = new System.Drawing.Point(6, 221);
            this.lblReadMDI.Name = "lblReadMDI";
            this.lblReadMDI.Size = new System.Drawing.Size(145, 30);
            this.lblReadMDI.TabIndex = 15;
            this.lblReadMDI.Tag = "Button";
            this.lblReadMDI.Text = "Read Class Status";
            this.lblReadMDI.Click += new System.EventHandler(this.lblReadMDI_Click);
            // 
            // listInstantaneousMDI
            // 
            this.listInstantaneousMDI.FormattingEnabled = true;
            this.listInstantaneousMDI.Items.AddRange(new object[] {
            "ACTIVE ABSOLUTE",
            "ACTIVE IMPORT",
            "ACTIVE EXPORT",
            "REACTIVE ABSOLUTE",
            "REACTIVE IMPORT",
            "REACTIVE EXPORT"});
            this.listInstantaneousMDI.Location = new System.Drawing.Point(6, 12);
            this.listInstantaneousMDI.Name = "listInstantaneousMDI";
            this.listInstantaneousMDI.Size = new System.Drawing.Size(145, 199);
            this.listInstantaneousMDI.TabIndex = 2;
            this.listInstantaneousMDI.SelectedIndexChanged += new System.EventHandler(this.listInstantaneousMDI_SelectedIndexChanged);
            // 
            // tab_NewIns
            // 
            this.tab_NewIns.Controls.Add(this.pb_newIns);
            this.tab_NewIns.Controls.Add(this.lbl_MeterDate);
            this.tab_NewIns.Controls.Add(this.btn_INS_Report);
            this.tab_NewIns.Controls.Add(this.lbl_Frequency);
            this.tab_NewIns.Controls.Add(this.label5);
            this.tab_NewIns.Controls.Add(this.btn_NewIns);
            this.tab_NewIns.Controls.Add(this.bl);
            this.tab_NewIns.Controls.Add(this.grid_NEwIns);
            this.tab_NewIns.Location = new System.Drawing.Point(4, 22);
            this.tab_NewIns.Name = "tab_NewIns";
            this.tab_NewIns.Size = new System.Drawing.Size(1038, 453);
            this.tab_NewIns.TabIndex = 4;
            this.tab_NewIns.Text = "NEW Instantaneous";
            this.tab_NewIns.UseVisualStyleBackColor = true;
            // 
            // pb_newIns
            // 
            this.pb_newIns.ForeColor = System.Drawing.SystemColors.Desktop;
            this.pb_newIns.Location = new System.Drawing.Point(733, 102);
            this.pb_newIns.Name = "pb_newIns";
            this.pb_newIns.Size = new System.Drawing.Size(140, 23);
            this.pb_newIns.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pb_newIns.TabIndex = 14;
            this.pb_newIns.Visible = false;
            // 
            // lbl_MeterDate
            // 
            this.lbl_MeterDate.AutoSize = true;
            this.lbl_MeterDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MeterDate.Location = new System.Drawing.Point(271, 369);
            this.lbl_MeterDate.Name = "lbl_MeterDate";
            this.lbl_MeterDate.Size = new System.Drawing.Size(39, 20);
            this.lbl_MeterDate.TabIndex = 2;
            this.lbl_MeterDate.Text = "___";
            this.lbl_MeterDate.Visible = false;
            // 
            // btn_INS_Report
            // 
            this.btn_INS_Report.Location = new System.Drawing.Point(733, 62);
            this.btn_INS_Report.Name = "btn_INS_Report";
            this.btn_INS_Report.Size = new System.Drawing.Size(123, 30);
            this.btn_INS_Report.TabIndex = 13;
            this.btn_INS_Report.Tag = "Button";
            this.btn_INS_Report.Text = "Generate Report";
            this.btn_INS_Report.Click += new System.EventHandler(this.btn_INS_Report_Click);
            // 
            // lbl_Frequency
            // 
            this.lbl_Frequency.AutoSize = true;
            this.lbl_Frequency.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Frequency.Location = new System.Drawing.Point(271, 409);
            this.lbl_Frequency.Name = "lbl_Frequency";
            this.lbl_Frequency.Size = new System.Drawing.Size(59, 20);
            this.lbl_Frequency.TabIndex = 2;
            this.lbl_Frequency.Text = "_____";
            this.lbl_Frequency.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(20, 406);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(211, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "Power Supply Frequency:";
            this.label5.Visible = false;
            // 
            // btn_NewIns
            // 
            this.btn_NewIns.Location = new System.Drawing.Point(733, 18);
            this.btn_NewIns.Name = "btn_NewIns";
            this.btn_NewIns.Size = new System.Drawing.Size(123, 30);
            this.btn_NewIns.TabIndex = 13;
            this.btn_NewIns.Tag = "Button";
            this.btn_NewIns.Text = "Read Instantaneous";
            this.btn_NewIns.Click += new System.EventHandler(this.btn_NewIns_Click);
            // 
            // bl
            // 
            this.bl.AutoSize = true;
            this.bl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bl.Location = new System.Drawing.Point(20, 367);
            this.bl.Name = "bl";
            this.bl.Size = new System.Drawing.Size(104, 20);
            this.bl.TabIndex = 2;
            this.bl.Text = "Meter Date:";
            this.bl.Visible = false;
            // 
            // grid_NEwIns
            // 
            this.grid_NEwIns.AllowUserToAddRows = false;
            this.grid_NEwIns.AllowUserToDeleteRows = false;
            this.grid_NEwIns.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid_NEwIns.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grid_NEwIns.DefaultCellStyle = dataGridViewCellStyle15;
            this.grid_NEwIns.Location = new System.Drawing.Point(6, 18);
            this.grid_NEwIns.Name = "grid_NEwIns";
            this.grid_NEwIns.ReadOnly = true;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid_NEwIns.RowHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.grid_NEwIns.RowHeadersWidth = 200;
            this.grid_NEwIns.Size = new System.Drawing.Size(702, 324);
            this.grid_NEwIns.TabIndex = 1;
            // 
            // tab_Record
            // 
            this.tab_Record.Controls.Add(this.tabControl1);
            this.tab_Record.Location = new System.Drawing.Point(4, 22);
            this.tab_Record.Name = "tab_Record";
            this.tab_Record.Size = new System.Drawing.Size(1038, 453);
            this.tab_Record.TabIndex = 5;
            this.tab_Record.Text = "Record";
            this.tab_Record.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.all_in_one);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.AllInOnetab);
            this.tabControl1.Location = new System.Drawing.Point(4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1011, 446);
            this.tabControl1.TabIndex = 1;
            // 
            // all_in_one
            // 
            this.all_in_one.Controls.Add(this.rb_uuncheck_all);
            this.all_in_one.Controls.Add(this.rb_Counter);
            this.all_in_one.Controls.Add(this.rb_check_all_log);
            this.all_in_one.Controls.Add(this.richTextBox1);
            this.all_in_one.Controls.Add(this.lbl_packetSize);
            this.all_in_one.Controls.Add(this.label14);
            this.all_in_one.Controls.Add(this.grid_EventNames);
            this.all_in_one.Controls.Add(this.groupBox4);
            this.all_in_one.Controls.Add(this.groupBox1);
            this.all_in_one.Controls.Add(this.btn_GET_INS);
            this.all_in_one.Controls.Add(this.btn_SetAll);
            this.all_in_one.Controls.Add(this.btn_GET_ALL);
            this.all_in_one.Location = new System.Drawing.Point(4, 22);
            this.all_in_one.Name = "all_in_one";
            this.all_in_one.Padding = new System.Windows.Forms.Padding(3);
            this.all_in_one.Size = new System.Drawing.Size(1003, 420);
            this.all_in_one.TabIndex = 0;
            this.all_in_one.Text = "All In One";
            this.all_in_one.UseVisualStyleBackColor = true;
            // 
            // rb_uuncheck_all
            // 
            this.rb_uuncheck_all.AutoSize = true;
            this.rb_uuncheck_all.Location = new System.Drawing.Point(550, 17);
            this.rb_uuncheck_all.Name = "rb_uuncheck_all";
            this.rb_uuncheck_all.Size = new System.Drawing.Size(84, 17);
            this.rb_uuncheck_all.TabIndex = 8;
            this.rb_uuncheck_all.TabStop = true;
            this.rb_uuncheck_all.Text = "UnCheck All";
            this.rb_uuncheck_all.UseVisualStyleBackColor = true;
            this.rb_uuncheck_all.CheckedChanged += new System.EventHandler(this.rb_uuncheck_all_CheckedChanged);
            // 
            // rb_Counter
            // 
            this.rb_Counter.AutoSize = true;
            this.rb_Counter.Location = new System.Drawing.Point(757, 17);
            this.rb_Counter.Name = "rb_Counter";
            this.rb_Counter.Size = new System.Drawing.Size(110, 17);
            this.rb_Counter.TabIndex = 7;
            this.rb_Counter.TabStop = true;
            this.rb_Counter.Text = "Check All Counter";
            this.rb_Counter.UseVisualStyleBackColor = true;
            this.rb_Counter.CheckedChanged += new System.EventHandler(this.rb_Counter_CheckedChanged);
            // 
            // rb_check_all_log
            // 
            this.rb_check_all_log.AutoSize = true;
            this.rb_check_all_log.Location = new System.Drawing.Point(660, 17);
            this.rb_check_all_log.Name = "rb_check_all_log";
            this.rb_check_all_log.Size = new System.Drawing.Size(91, 17);
            this.rb_check_all_log.TabIndex = 6;
            this.rb_check_all_log.TabStop = true;
            this.rb_check_all_log.Text = "Check All Log";
            this.rb_check_all_log.UseVisualStyleBackColor = true;
            this.rb_check_all_log.CheckedChanged += new System.EventHandler(this.rb_check_all_log_CheckedChanged);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(532, 359);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(200, 55);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            // 
            // lbl_packetSize
            // 
            this.lbl_packetSize.AutoSize = true;
            this.lbl_packetSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_packetSize.Location = new System.Drawing.Point(906, 171);
            this.lbl_packetSize.Name = "lbl_packetSize";
            this.lbl_packetSize.Size = new System.Drawing.Size(36, 25);
            this.lbl_packetSize.TabIndex = 4;
            this.lbl_packetSize.Text = "__";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(873, 135);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(126, 25);
            this.label14.TabIndex = 4;
            this.label14.Text = "Packet Size";
            // 
            // grid_EventNames
            // 
            this.grid_EventNames.AllowUserToAddRows = false;
            this.grid_EventNames.AllowUserToDeleteRows = false;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid_EventNames.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle17;
            this.grid_EventNames.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Event,
            this.Log,
            this.even_counter});
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grid_EventNames.DefaultCellStyle = dataGridViewCellStyle18;
            this.grid_EventNames.Location = new System.Drawing.Point(462, 40);
            this.grid_EventNames.Name = "grid_EventNames";
            this.grid_EventNames.ReadOnly = true;
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid_EventNames.RowHeadersDefaultCellStyle = dataGridViewCellStyle19;
            this.grid_EventNames.RowHeadersVisible = false;
            this.grid_EventNames.RowHeadersWidth = 100;
            this.grid_EventNames.Size = new System.Drawing.Size(405, 318);
            this.grid_EventNames.TabIndex = 3;
            this.grid_EventNames.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_EventNames_CellClick);
            this.grid_EventNames.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_EventNames_CellValueChanged);
            // 
            // Event
            // 
            this.Event.HeaderText = "Event";
            this.Event.Name = "Event";
            this.Event.ReadOnly = true;
            this.Event.Width = 200;
            // 
            // Log
            // 
            this.Log.HeaderText = "Log";
            this.Log.Name = "Log";
            this.Log.ReadOnly = true;
            this.Log.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Log.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Log.Width = 80;
            // 
            // even_counter
            // 
            this.even_counter.HeaderText = "Counter";
            this.even_counter.Name = "even_counter";
            this.even_counter.ReadOnly = true;
            this.even_counter.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.even_counter.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.even_counter.Width = 80;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chk_all_monthly);
            this.groupBox4.Controls.Add(this.chk_check_all_cummulative);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.check_PABS16);
            this.groupBox4.Controls.Add(this.chk_CUMMDIP);
            this.groupBox4.Controls.Add(this.chk_CumMDIQ);
            this.groupBox4.Controls.Add(this.check_PABS15);
            this.groupBox4.Controls.Add(this.chk_TL);
            this.groupBox4.Controls.Add(this.check_Q16);
            this.groupBox4.Controls.Add(this.check_P16);
            this.groupBox4.Controls.Add(this.check_Q15);
            this.groupBox4.Controls.Add(this.chk_T4);
            this.groupBox4.Controls.Add(this.check_S16);
            this.groupBox4.Controls.Add(this.check_P15);
            this.groupBox4.Controls.Add(this.check_QAbs16);
            this.groupBox4.Controls.Add(this.chk_T3);
            this.groupBox4.Controls.Add(this.check_S15);
            this.groupBox4.Controls.Add(this.check_T16);
            this.groupBox4.Controls.Add(this.chk_T2);
            this.groupBox4.Controls.Add(this.check_QAbs15);
            this.groupBox4.Controls.Add(this.chk_MDIP16);
            this.groupBox4.Controls.Add(this.chk_T1);
            this.groupBox4.Controls.Add(this.check_T15);
            this.groupBox4.Controls.Add(this.chk_MDIQ16);
            this.groupBox4.Controls.Add(this.chk_MDIP15);
            this.groupBox4.Controls.Add(this.chk_MDIQ15);
            this.groupBox4.Location = new System.Drawing.Point(7, 154);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(419, 251);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Billing";
            // 
            // chk_all_monthly
            // 
            this.chk_all_monthly.AutoSize = true;
            this.chk_all_monthly.Location = new System.Drawing.Point(229, 205);
            this.chk_all_monthly.Name = "chk_all_monthly";
            this.chk_all_monthly.Size = new System.Drawing.Size(111, 17);
            this.chk_all_monthly.TabIndex = 3;
            this.chk_all_monthly.Text = "Check All Monthly";
            this.chk_all_monthly.UseVisualStyleBackColor = true;
            this.chk_all_monthly.CheckedChanged += new System.EventHandler(this.chk_all_monthly_CheckedChanged);
            // 
            // chk_check_all_cummulative
            // 
            this.chk_check_all_cummulative.AutoSize = true;
            this.chk_check_all_cummulative.Location = new System.Drawing.Point(229, 228);
            this.chk_check_all_cummulative.Name = "chk_check_all_cummulative";
            this.chk_check_all_cummulative.Size = new System.Drawing.Size(134, 17);
            this.chk_check_all_cummulative.TabIndex = 2;
            this.chk_check_all_cummulative.Text = "Check All Cummulative";
            this.chk_check_all_cummulative.UseVisualStyleBackColor = true;
            this.chk_check_all_cummulative.CheckedChanged += new System.EventHandler(this.chk_check_all_cummulative_CheckedChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(-2, 233);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(69, 13);
            this.label13.TabIndex = 1;
            this.label13.Text = "Curr MDI P";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(14, 190);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "P Abs";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(-3, 212);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 13);
            this.label12.TabIndex = 1;
            this.label12.Text = "Curr MDI Q";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(14, 167);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(19, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "P-";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(14, 97);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "S";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(14, 144);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Q Abs";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(14, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "T";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(14, 121);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Q-";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "MDI P";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(126, 13);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(51, 13);
            this.label16.TabIndex = 1;
            this.label16.Text = "Monthly";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(71, 13);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(44, 13);
            this.label15.TabIndex = 1;
            this.label15.Text = "Cumm.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "MDI Q";
            // 
            // check_PABS16
            // 
            this.check_PABS16.AutoSize = true;
            this.check_PABS16.Location = new System.Drawing.Point(142, 190);
            this.check_PABS16.Name = "check_PABS16";
            this.check_PABS16.Size = new System.Drawing.Size(15, 14);
            this.check_PABS16.TabIndex = 0;
            this.check_PABS16.UseVisualStyleBackColor = true;
            this.check_PABS16.CheckedChanged += new System.EventHandler(this.check_PABS16_CheckedChanged);
            // 
            // chk_CUMMDIP
            // 
            this.chk_CUMMDIP.AutoSize = true;
            this.chk_CUMMDIP.Location = new System.Drawing.Point(89, 234);
            this.chk_CUMMDIP.Name = "chk_CUMMDIP";
            this.chk_CUMMDIP.Size = new System.Drawing.Size(15, 14);
            this.chk_CUMMDIP.TabIndex = 0;
            this.chk_CUMMDIP.UseVisualStyleBackColor = true;
            this.chk_CUMMDIP.CheckedChanged += new System.EventHandler(this.chk_CUMMDIP_CheckedChanged);
            // 
            // chk_CumMDIQ
            // 
            this.chk_CumMDIQ.AutoSize = true;
            this.chk_CumMDIQ.Location = new System.Drawing.Point(89, 213);
            this.chk_CumMDIQ.Name = "chk_CumMDIQ";
            this.chk_CumMDIQ.Size = new System.Drawing.Size(15, 14);
            this.chk_CumMDIQ.TabIndex = 0;
            this.chk_CumMDIQ.UseVisualStyleBackColor = true;
            this.chk_CumMDIQ.CheckedChanged += new System.EventHandler(this.chk_CumMDIQ_CheckedChanged);
            // 
            // check_PABS15
            // 
            this.check_PABS15.AutoSize = true;
            this.check_PABS15.Location = new System.Drawing.Point(89, 190);
            this.check_PABS15.Name = "check_PABS15";
            this.check_PABS15.Size = new System.Drawing.Size(15, 14);
            this.check_PABS15.TabIndex = 0;
            this.check_PABS15.UseVisualStyleBackColor = true;
            this.check_PABS15.CheckedChanged += new System.EventHandler(this.check_PABS15_CheckedChanged);
            // 
            // chk_TL
            // 
            this.chk_TL.AutoSize = true;
            this.chk_TL.Location = new System.Drawing.Point(229, 121);
            this.chk_TL.Name = "chk_TL";
            this.chk_TL.Size = new System.Drawing.Size(39, 17);
            this.chk_TL.TabIndex = 0;
            this.chk_TL.Text = "TL";
            this.chk_TL.UseVisualStyleBackColor = true;
            this.chk_TL.CheckedChanged += new System.EventHandler(this.chk_TL_CheckedChanged);
            // 
            // check_Q16
            // 
            this.check_Q16.AutoSize = true;
            this.check_Q16.Location = new System.Drawing.Point(142, 121);
            this.check_Q16.Name = "check_Q16";
            this.check_Q16.Size = new System.Drawing.Size(15, 14);
            this.check_Q16.TabIndex = 0;
            this.check_Q16.UseVisualStyleBackColor = true;
            this.check_Q16.CheckedChanged += new System.EventHandler(this.check_Q16_CheckedChanged);
            // 
            // check_P16
            // 
            this.check_P16.AutoSize = true;
            this.check_P16.Location = new System.Drawing.Point(142, 167);
            this.check_P16.Name = "check_P16";
            this.check_P16.Size = new System.Drawing.Size(15, 14);
            this.check_P16.TabIndex = 0;
            this.check_P16.UseVisualStyleBackColor = true;
            this.check_P16.CheckedChanged += new System.EventHandler(this.check_P16_CheckedChanged);
            // 
            // check_Q15
            // 
            this.check_Q15.AutoSize = true;
            this.check_Q15.Location = new System.Drawing.Point(89, 121);
            this.check_Q15.Name = "check_Q15";
            this.check_Q15.Size = new System.Drawing.Size(15, 14);
            this.check_Q15.TabIndex = 0;
            this.check_Q15.UseVisualStyleBackColor = true;
            this.check_Q15.CheckedChanged += new System.EventHandler(this.check_Q15_CheckedChanged);
            // 
            // chk_T4
            // 
            this.chk_T4.AutoSize = true;
            this.chk_T4.Location = new System.Drawing.Point(229, 98);
            this.chk_T4.Name = "chk_T4";
            this.chk_T4.Size = new System.Drawing.Size(39, 17);
            this.chk_T4.TabIndex = 0;
            this.chk_T4.Text = "T4";
            this.chk_T4.UseVisualStyleBackColor = true;
            this.chk_T4.CheckedChanged += new System.EventHandler(this.chk_T4_CheckedChanged);
            // 
            // check_S16
            // 
            this.check_S16.AutoSize = true;
            this.check_S16.Location = new System.Drawing.Point(142, 98);
            this.check_S16.Name = "check_S16";
            this.check_S16.Size = new System.Drawing.Size(15, 14);
            this.check_S16.TabIndex = 0;
            this.check_S16.UseVisualStyleBackColor = true;
            this.check_S16.CheckedChanged += new System.EventHandler(this.check_S16_CheckedChanged);
            // 
            // check_P15
            // 
            this.check_P15.AutoSize = true;
            this.check_P15.Location = new System.Drawing.Point(89, 167);
            this.check_P15.Name = "check_P15";
            this.check_P15.Size = new System.Drawing.Size(15, 14);
            this.check_P15.TabIndex = 0;
            this.check_P15.UseVisualStyleBackColor = true;
            this.check_P15.CheckedChanged += new System.EventHandler(this.check_P15_CheckedChanged);
            // 
            // check_QAbs16
            // 
            this.check_QAbs16.AutoSize = true;
            this.check_QAbs16.Location = new System.Drawing.Point(142, 144);
            this.check_QAbs16.Name = "check_QAbs16";
            this.check_QAbs16.Size = new System.Drawing.Size(15, 14);
            this.check_QAbs16.TabIndex = 0;
            this.check_QAbs16.UseVisualStyleBackColor = true;
            this.check_QAbs16.CheckedChanged += new System.EventHandler(this.check_QAbs16_CheckedChanged);
            // 
            // chk_T3
            // 
            this.chk_T3.AutoSize = true;
            this.chk_T3.Location = new System.Drawing.Point(229, 75);
            this.chk_T3.Name = "chk_T3";
            this.chk_T3.Size = new System.Drawing.Size(39, 17);
            this.chk_T3.TabIndex = 0;
            this.chk_T3.Text = "T3";
            this.chk_T3.UseVisualStyleBackColor = true;
            this.chk_T3.CheckedChanged += new System.EventHandler(this.chk_T3_CheckedChanged);
            // 
            // check_S15
            // 
            this.check_S15.AutoSize = true;
            this.check_S15.Location = new System.Drawing.Point(89, 98);
            this.check_S15.Name = "check_S15";
            this.check_S15.Size = new System.Drawing.Size(15, 14);
            this.check_S15.TabIndex = 0;
            this.check_S15.UseVisualStyleBackColor = true;
            this.check_S15.CheckedChanged += new System.EventHandler(this.check_S15_CheckedChanged);
            // 
            // check_T16
            // 
            this.check_T16.AutoSize = true;
            this.check_T16.Location = new System.Drawing.Point(142, 75);
            this.check_T16.Name = "check_T16";
            this.check_T16.Size = new System.Drawing.Size(15, 14);
            this.check_T16.TabIndex = 0;
            this.check_T16.UseVisualStyleBackColor = true;
            this.check_T16.CheckedChanged += new System.EventHandler(this.check_T16_CheckedChanged);
            // 
            // chk_T2
            // 
            this.chk_T2.AutoSize = true;
            this.chk_T2.Location = new System.Drawing.Point(229, 52);
            this.chk_T2.Name = "chk_T2";
            this.chk_T2.Size = new System.Drawing.Size(39, 17);
            this.chk_T2.TabIndex = 0;
            this.chk_T2.Text = "T2";
            this.chk_T2.UseVisualStyleBackColor = true;
            this.chk_T2.CheckedChanged += new System.EventHandler(this.chk_T2_CheckedChanged);
            // 
            // check_QAbs15
            // 
            this.check_QAbs15.AutoSize = true;
            this.check_QAbs15.Location = new System.Drawing.Point(89, 144);
            this.check_QAbs15.Name = "check_QAbs15";
            this.check_QAbs15.Size = new System.Drawing.Size(15, 14);
            this.check_QAbs15.TabIndex = 0;
            this.check_QAbs15.UseVisualStyleBackColor = true;
            this.check_QAbs15.CheckedChanged += new System.EventHandler(this.check_QAbs15_CheckedChanged);
            // 
            // chk_MDIP16
            // 
            this.chk_MDIP16.AutoSize = true;
            this.chk_MDIP16.Location = new System.Drawing.Point(142, 52);
            this.chk_MDIP16.Name = "chk_MDIP16";
            this.chk_MDIP16.Size = new System.Drawing.Size(15, 14);
            this.chk_MDIP16.TabIndex = 0;
            this.chk_MDIP16.UseVisualStyleBackColor = true;
            this.chk_MDIP16.CheckedChanged += new System.EventHandler(this.chk_MDIP16_CheckedChanged);
            // 
            // chk_T1
            // 
            this.chk_T1.AutoSize = true;
            this.chk_T1.Checked = true;
            this.chk_T1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_T1.Location = new System.Drawing.Point(229, 29);
            this.chk_T1.Name = "chk_T1";
            this.chk_T1.Size = new System.Drawing.Size(39, 17);
            this.chk_T1.TabIndex = 0;
            this.chk_T1.Text = "T1";
            this.chk_T1.UseVisualStyleBackColor = true;
            this.chk_T1.CheckedChanged += new System.EventHandler(this.chk_T1_CheckedChanged);
            // 
            // check_T15
            // 
            this.check_T15.AutoSize = true;
            this.check_T15.Location = new System.Drawing.Point(89, 75);
            this.check_T15.Name = "check_T15";
            this.check_T15.Size = new System.Drawing.Size(15, 14);
            this.check_T15.TabIndex = 0;
            this.check_T15.UseVisualStyleBackColor = true;
            this.check_T15.CheckedChanged += new System.EventHandler(this.check_T15_CheckedChanged);
            // 
            // chk_MDIQ16
            // 
            this.chk_MDIQ16.AutoSize = true;
            this.chk_MDIQ16.Location = new System.Drawing.Point(142, 29);
            this.chk_MDIQ16.Name = "chk_MDIQ16";
            this.chk_MDIQ16.Size = new System.Drawing.Size(15, 14);
            this.chk_MDIQ16.TabIndex = 0;
            this.chk_MDIQ16.UseVisualStyleBackColor = true;
            this.chk_MDIQ16.CheckedChanged += new System.EventHandler(this.chk_MDIQ16_CheckedChanged);
            // 
            // chk_MDIP15
            // 
            this.chk_MDIP15.AutoSize = true;
            this.chk_MDIP15.Location = new System.Drawing.Point(89, 52);
            this.chk_MDIP15.Name = "chk_MDIP15";
            this.chk_MDIP15.Size = new System.Drawing.Size(15, 14);
            this.chk_MDIP15.TabIndex = 0;
            this.chk_MDIP15.UseVisualStyleBackColor = true;
            this.chk_MDIP15.CheckedChanged += new System.EventHandler(this.chk_MDIP15_CheckedChanged);
            // 
            // chk_MDIQ15
            // 
            this.chk_MDIQ15.AutoSize = true;
            this.chk_MDIQ15.Location = new System.Drawing.Point(89, 29);
            this.chk_MDIQ15.Name = "chk_MDIQ15";
            this.chk_MDIQ15.Size = new System.Drawing.Size(15, 14);
            this.chk_MDIQ15.TabIndex = 0;
            this.chk_MDIQ15.UseVisualStyleBackColor = true;
            this.chk_MDIQ15.CheckedChanged += new System.EventHandler(this.chk_MDIQ15_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chk_PT);
            this.groupBox1.Controls.Add(this.chk_CT);
            this.groupBox1.Controls.Add(this.chk_EventCount);
            this.groupBox1.Controls.Add(this.chk_MDI_Reset);
            this.groupBox1.Controls.Add(this.chk_S);
            this.groupBox1.Controls.Add(this.chk_V);
            this.groupBox1.Controls.Add(this.chk_MDITime);
            this.groupBox1.Controls.Add(this.chk_Q);
            this.groupBox1.Controls.Add(this.chk_AlarmSTS);
            this.groupBox1.Controls.Add(this.chk_LPLog);
            this.groupBox1.Controls.Add(this.chk_I);
            this.groupBox1.Controls.Add(this.chk_MdiPre);
            this.groupBox1.Controls.Add(this.check_PF);
            this.groupBox1.Controls.Add(this.chk_LPCount);
            this.groupBox1.Controls.Add(this.chk_P);
            this.groupBox1.Controls.Add(this.chk_EventLog);
            this.groupBox1.Controls.Add(this.chk_Tamper_power);
            this.groupBox1.Controls.Add(this.chk_frq);
            this.groupBox1.Location = new System.Drawing.Point(7, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(419, 141);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Events";
            // 
            // chk_PT
            // 
            this.chk_PT.AutoSize = true;
            this.chk_PT.Location = new System.Drawing.Point(327, 47);
            this.chk_PT.Name = "chk_PT";
            this.chk_PT.Size = new System.Drawing.Size(40, 17);
            this.chk_PT.TabIndex = 0;
            this.chk_PT.Text = "PT";
            this.chk_PT.UseVisualStyleBackColor = true;
            this.chk_PT.CheckedChanged += new System.EventHandler(this.chk_PT_CheckedChanged);
            // 
            // chk_CT
            // 
            this.chk_CT.AutoSize = true;
            this.chk_CT.Location = new System.Drawing.Point(327, 24);
            this.chk_CT.Name = "chk_CT";
            this.chk_CT.Size = new System.Drawing.Size(40, 17);
            this.chk_CT.TabIndex = 0;
            this.chk_CT.Text = "CT";
            this.chk_CT.UseVisualStyleBackColor = true;
            this.chk_CT.CheckedChanged += new System.EventHandler(this.chk_CT_CheckedChanged);
            // 
            // chk_EventCount
            // 
            this.chk_EventCount.AutoSize = true;
            this.chk_EventCount.Location = new System.Drawing.Point(224, 118);
            this.chk_EventCount.Name = "chk_EventCount";
            this.chk_EventCount.Size = new System.Drawing.Size(85, 17);
            this.chk_EventCount.TabIndex = 0;
            this.chk_EventCount.Text = "Event Count";
            this.chk_EventCount.UseVisualStyleBackColor = true;
            this.chk_EventCount.Visible = false;
            this.chk_EventCount.CheckedChanged += new System.EventHandler(this.chk_EventCount_CheckedChanged);
            // 
            // chk_MDI_Reset
            // 
            this.chk_MDI_Reset.AutoSize = true;
            this.chk_MDI_Reset.Location = new System.Drawing.Point(224, 93);
            this.chk_MDI_Reset.Name = "chk_MDI_Reset";
            this.chk_MDI_Reset.Size = new System.Drawing.Size(85, 17);
            this.chk_MDI_Reset.TabIndex = 0;
            this.chk_MDI_Reset.Text = "MDI RESET";
            this.chk_MDI_Reset.UseVisualStyleBackColor = true;
            this.chk_MDI_Reset.CheckedChanged += new System.EventHandler(this.chk_MDI_Reset_CheckedChanged);
            // 
            // chk_S
            // 
            this.chk_S.AutoSize = true;
            this.chk_S.Location = new System.Drawing.Point(224, 70);
            this.chk_S.Name = "chk_S";
            this.chk_S.Size = new System.Drawing.Size(33, 17);
            this.chk_S.TabIndex = 0;
            this.chk_S.Text = "S";
            this.chk_S.UseVisualStyleBackColor = true;
            this.chk_S.CheckedChanged += new System.EventHandler(this.chk_S_CheckedChanged);
            // 
            // chk_V
            // 
            this.chk_V.AutoSize = true;
            this.chk_V.Location = new System.Drawing.Point(224, 47);
            this.chk_V.Name = "chk_V";
            this.chk_V.Size = new System.Drawing.Size(62, 17);
            this.chk_V.TabIndex = 0;
            this.chk_V.Text = "Voltage";
            this.chk_V.UseVisualStyleBackColor = true;
            this.chk_V.CheckedChanged += new System.EventHandler(this.chk_V_CheckedChanged);
            // 
            // chk_MDITime
            // 
            this.chk_MDITime.AutoSize = true;
            this.chk_MDITime.Location = new System.Drawing.Point(109, 117);
            this.chk_MDITime.Name = "chk_MDITime";
            this.chk_MDITime.Size = new System.Drawing.Size(72, 17);
            this.chk_MDITime.TabIndex = 0;
            this.chk_MDITime.Text = "MDI Time";
            this.chk_MDITime.UseVisualStyleBackColor = true;
            this.chk_MDITime.CheckedChanged += new System.EventHandler(this.chk_MDITime_CheckedChanged);
            // 
            // chk_Q
            // 
            this.chk_Q.AutoSize = true;
            this.chk_Q.Location = new System.Drawing.Point(6, 94);
            this.chk_Q.Name = "chk_Q";
            this.chk_Q.Size = new System.Drawing.Size(34, 17);
            this.chk_Q.TabIndex = 0;
            this.chk_Q.Text = "Q";
            this.chk_Q.UseVisualStyleBackColor = true;
            this.chk_Q.CheckedChanged += new System.EventHandler(this.chk_Q_CheckedChanged);
            // 
            // chk_AlarmSTS
            // 
            this.chk_AlarmSTS.AutoSize = true;
            this.chk_AlarmSTS.Location = new System.Drawing.Point(110, 49);
            this.chk_AlarmSTS.Name = "chk_AlarmSTS";
            this.chk_AlarmSTS.Size = new System.Drawing.Size(87, 17);
            this.chk_AlarmSTS.TabIndex = 0;
            this.chk_AlarmSTS.Text = "ALARM STS";
            this.chk_AlarmSTS.UseVisualStyleBackColor = true;
            this.chk_AlarmSTS.CheckedChanged += new System.EventHandler(this.chk_AlarmSTS_CheckedChanged);
            // 
            // chk_LPLog
            // 
            this.chk_LPLog.AutoSize = true;
            this.chk_LPLog.Location = new System.Drawing.Point(109, 94);
            this.chk_LPLog.Name = "chk_LPLog";
            this.chk_LPLog.Size = new System.Drawing.Size(60, 17);
            this.chk_LPLog.TabIndex = 0;
            this.chk_LPLog.Text = "LP Log";
            this.chk_LPLog.UseVisualStyleBackColor = true;
            this.chk_LPLog.CheckedChanged += new System.EventHandler(this.chk_LPLog_CheckedChanged);
            // 
            // chk_I
            // 
            this.chk_I.AutoSize = true;
            this.chk_I.Location = new System.Drawing.Point(224, 24);
            this.chk_I.Name = "chk_I";
            this.chk_I.Size = new System.Drawing.Size(60, 17);
            this.chk_I.TabIndex = 0;
            this.chk_I.Text = "Current";
            this.chk_I.UseVisualStyleBackColor = true;
            this.chk_I.CheckedChanged += new System.EventHandler(this.chk_I_CheckedChanged);
            // 
            // chk_MdiPre
            // 
            this.chk_MdiPre.AutoSize = true;
            this.chk_MdiPre.Location = new System.Drawing.Point(110, 24);
            this.chk_MdiPre.Name = "chk_MdiPre";
            this.chk_MdiPre.Size = new System.Drawing.Size(71, 17);
            this.chk_MdiPre.TabIndex = 0;
            this.chk_MdiPre.Text = "MDI PRE";
            this.chk_MdiPre.UseVisualStyleBackColor = true;
            this.chk_MdiPre.CheckedChanged += new System.EventHandler(this.chk_MdiPre_CheckedChanged);
            // 
            // check_PF
            // 
            this.check_PF.AutoSize = true;
            this.check_PF.Location = new System.Drawing.Point(6, 71);
            this.check_PF.Name = "check_PF";
            this.check_PF.Size = new System.Drawing.Size(89, 17);
            this.check_PF.TabIndex = 0;
            this.check_PF.Text = "Power Factor";
            this.check_PF.UseVisualStyleBackColor = true;
            this.check_PF.CheckedChanged += new System.EventHandler(this.check_PF_CheckedChanged);
            // 
            // chk_LPCount
            // 
            this.chk_LPCount.AutoSize = true;
            this.chk_LPCount.Location = new System.Drawing.Point(109, 71);
            this.chk_LPCount.Name = "chk_LPCount";
            this.chk_LPCount.Size = new System.Drawing.Size(70, 17);
            this.chk_LPCount.TabIndex = 0;
            this.chk_LPCount.Text = "LP Count";
            this.chk_LPCount.UseVisualStyleBackColor = true;
            this.chk_LPCount.CheckedChanged += new System.EventHandler(this.chk_LPCount_CheckedChanged);
            // 
            // chk_P
            // 
            this.chk_P.AutoSize = true;
            this.chk_P.Location = new System.Drawing.Point(6, 115);
            this.chk_P.Name = "chk_P";
            this.chk_P.Size = new System.Drawing.Size(33, 17);
            this.chk_P.TabIndex = 0;
            this.chk_P.Text = "P";
            this.chk_P.UseVisualStyleBackColor = true;
            this.chk_P.CheckedChanged += new System.EventHandler(this.chk_P_CheckedChanged);
            // 
            // chk_EventLog
            // 
            this.chk_EventLog.AutoSize = true;
            this.chk_EventLog.Location = new System.Drawing.Point(327, 115);
            this.chk_EventLog.Name = "chk_EventLog";
            this.chk_EventLog.Size = new System.Drawing.Size(75, 17);
            this.chk_EventLog.TabIndex = 0;
            this.chk_EventLog.Text = "Event Log";
            this.chk_EventLog.UseVisualStyleBackColor = true;
            this.chk_EventLog.Visible = false;
            this.chk_EventLog.CheckedChanged += new System.EventHandler(this.chk_EventLog_CheckedChanged);
            // 
            // chk_Tamper_power
            // 
            this.chk_Tamper_power.AutoSize = true;
            this.chk_Tamper_power.Location = new System.Drawing.Point(6, 48);
            this.chk_Tamper_power.Name = "chk_Tamper_power";
            this.chk_Tamper_power.Size = new System.Drawing.Size(95, 17);
            this.chk_Tamper_power.TabIndex = 0;
            this.chk_Tamper_power.Text = "Tamper Power";
            this.chk_Tamper_power.UseVisualStyleBackColor = true;
            this.chk_Tamper_power.CheckedChanged += new System.EventHandler(this.chk_T_CheckedChanged);
            // 
            // chk_frq
            // 
            this.chk_frq.AutoSize = true;
            this.chk_frq.Location = new System.Drawing.Point(6, 24);
            this.chk_frq.Name = "chk_frq";
            this.chk_frq.Size = new System.Drawing.Size(76, 17);
            this.chk_frq.TabIndex = 0;
            this.chk_frq.Text = "Frequency";
            this.chk_frq.UseVisualStyleBackColor = true;
            this.chk_frq.CheckedChanged += new System.EventHandler(this.chk_frq_CheckedChanged);
            // 
            // btn_GET_INS
            // 
            this.btn_GET_INS.Location = new System.Drawing.Point(876, 7);
            this.btn_GET_INS.Name = "btn_GET_INS";
            this.btn_GET_INS.Size = new System.Drawing.Size(109, 43);
            this.btn_GET_INS.TabIndex = 0;
            this.btn_GET_INS.Text = "Get Instantaneous All in ONE";
            this.btn_GET_INS.UseVisualStyleBackColor = true;
            this.btn_GET_INS.Click += new System.EventHandler(this.btn_GET_INS_Click);
            // 
            // btn_SetAll
            // 
            this.btn_SetAll.Location = new System.Drawing.Point(876, 56);
            this.btn_SetAll.Name = "btn_SetAll";
            this.btn_SetAll.Size = new System.Drawing.Size(109, 23);
            this.btn_SetAll.TabIndex = 0;
            this.btn_SetAll.Text = "SET  All in ONE";
            this.btn_SetAll.UseVisualStyleBackColor = true;
            this.btn_SetAll.Click += new System.EventHandler(this.btn_SetAll_Click);
            // 
            // btn_GET_ALL
            // 
            this.btn_GET_ALL.Location = new System.Drawing.Point(876, 85);
            this.btn_GET_ALL.Name = "btn_GET_ALL";
            this.btn_GET_ALL.Size = new System.Drawing.Size(109, 29);
            this.btn_GET_ALL.TabIndex = 0;
            this.btn_GET_ALL.Text = "Get All in ONE";
            this.btn_GET_ALL.UseVisualStyleBackColor = true;
            this.btn_GET_ALL.Click += new System.EventHandler(this.btn_GET_ALL_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPage4.BackgroundImage")));
            this.tabPage4.Controls.Add(this.pnl_TL_Load_Profile);
            this.tabPage4.Controls.Add(this.pnl_cb_day_record);
            this.tabPage4.Controls.Add(this.button2);
            this.tabPage4.Controls.Add(this.btn_TLLoadProfile);
            this.tabPage4.Controls.Add(this.btn_CBDayRecord);
            this.tabPage4.Controls.Add(this.btn_MakeError);
            this.tabPage4.Controls.Add(this.btn_Set_ReadRawData);
            this.tabPage4.Controls.Add(this.txt_EPMNumber);
            this.tabPage4.Controls.Add(this.txt_RawDataLength);
            this.tabPage4.Controls.Add(this.txt_RawDataAddress);
            this.tabPage4.Controls.Add(this.label20);
            this.tabPage4.Controls.Add(this.label19);
            this.tabPage4.Controls.Add(this.label18);
            this.tabPage4.Controls.Add(this.btn_ReadRawData);
            this.tabPage4.Controls.Add(this.txt_general);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1003, 420);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Profile";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // pnl_TL_Load_Profile
            // 
            this.pnl_TL_Load_Profile.Controls.Add(this.label26);
            this.pnl_TL_Load_Profile.Controls.Add(this.grid_tl_load_profile);
            this.pnl_TL_Load_Profile.Location = new System.Drawing.Point(232, 7);
            this.pnl_TL_Load_Profile.Name = "pnl_TL_Load_Profile";
            this.pnl_TL_Load_Profile.Size = new System.Drawing.Size(784, 414);
            this.pnl_TL_Load_Profile.TabIndex = 22;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.Navy;
            this.label26.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label26.Location = new System.Drawing.Point(271, 10);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(159, 29);
            this.label26.TabIndex = 15;
            this.label26.Text = "TL Load Profile";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grid_tl_load_profile
            // 
            this.grid_tl_load_profile.AllowUserToAddRows = false;
            this.grid_tl_load_profile.AllowUserToResizeColumns = false;
            this.grid_tl_load_profile.AllowUserToResizeRows = false;
            this.grid_tl_load_profile.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grid_tl_load_profile.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid_tl_load_profile.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle20;
            this.grid_tl_load_profile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid_tl_load_profile.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DATE,
            this.COUNTER,
            this.KWH_P,
            this.KWH_N,
            this.KVARH_Q1,
            this.KVARH_Q2,
            this.KVARH_Q3,
            this.KVARH_Q4,
            this.KVAH,
            this.TAMPER,
            this.MDI_KW,
            this.MDI_KVAR,
            this.PF,
            this.CAPTURE_TIME,
            this.DAY_MDI_KW,
            this.DAY_MDI_KVAR});
            dataGridViewCellStyle37.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle37.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle37.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle37.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle37.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle37.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle37.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grid_tl_load_profile.DefaultCellStyle = dataGridViewCellStyle37;
            this.grid_tl_load_profile.EnableHeadersVisualStyles = false;
            this.grid_tl_load_profile.Location = new System.Drawing.Point(11, 45);
            this.grid_tl_load_profile.Name = "grid_tl_load_profile";
            this.grid_tl_load_profile.ReadOnly = true;
            this.grid_tl_load_profile.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle38.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle38.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle38.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle38.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle38.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle38.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle38.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid_tl_load_profile.RowHeadersDefaultCellStyle = dataGridViewCellStyle38;
            this.grid_tl_load_profile.Size = new System.Drawing.Size(750, 359);
            this.grid_tl_load_profile.TabIndex = 0;
            this.grid_tl_load_profile.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_tl_load_profile_CellContentClick);
            // 
            // DATE
            // 
            this.DATE.DataPropertyName = "DATE";
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DATE.DefaultCellStyle = dataGridViewCellStyle21;
            this.DATE.HeaderText = "DATE";
            this.DATE.Name = "DATE";
            this.DATE.ReadOnly = true;
            this.DATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DATE.Width = 150;
            // 
            // COUNTER
            // 
            this.COUNTER.DataPropertyName = "COUNTER";
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.COUNTER.DefaultCellStyle = dataGridViewCellStyle22;
            this.COUNTER.HeaderText = "COUNTER";
            this.COUNTER.Name = "COUNTER";
            this.COUNTER.ReadOnly = true;
            this.COUNTER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // KWH_P
            // 
            this.KWH_P.DataPropertyName = "KWH_P";
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.KWH_P.DefaultCellStyle = dataGridViewCellStyle23;
            this.KWH_P.HeaderText = "KWH_P";
            this.KWH_P.Name = "KWH_P";
            this.KWH_P.ReadOnly = true;
            this.KWH_P.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // KWH_N
            // 
            this.KWH_N.DataPropertyName = "KWH_N";
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.KWH_N.DefaultCellStyle = dataGridViewCellStyle24;
            this.KWH_N.HeaderText = "KWH_N";
            this.KWH_N.Name = "KWH_N";
            this.KWH_N.ReadOnly = true;
            this.KWH_N.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // KVARH_Q1
            // 
            this.KVARH_Q1.DataPropertyName = "KVARH_Q1";
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.KVARH_Q1.DefaultCellStyle = dataGridViewCellStyle25;
            this.KVARH_Q1.HeaderText = "KVARH_Q1";
            this.KVARH_Q1.Name = "KVARH_Q1";
            this.KVARH_Q1.ReadOnly = true;
            this.KVARH_Q1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // KVARH_Q2
            // 
            this.KVARH_Q2.DataPropertyName = "KVARH_Q2";
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.KVARH_Q2.DefaultCellStyle = dataGridViewCellStyle26;
            this.KVARH_Q2.HeaderText = "KVARH_Q2";
            this.KVARH_Q2.Name = "KVARH_Q2";
            this.KVARH_Q2.ReadOnly = true;
            this.KVARH_Q2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // KVARH_Q3
            // 
            this.KVARH_Q3.DataPropertyName = "KVARH_Q3";
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.KVARH_Q3.DefaultCellStyle = dataGridViewCellStyle27;
            this.KVARH_Q3.HeaderText = "KVARH_Q3";
            this.KVARH_Q3.Name = "KVARH_Q3";
            this.KVARH_Q3.ReadOnly = true;
            this.KVARH_Q3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // KVARH_Q4
            // 
            this.KVARH_Q4.DataPropertyName = "KVARH_Q4";
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.KVARH_Q4.DefaultCellStyle = dataGridViewCellStyle28;
            this.KVARH_Q4.HeaderText = "KVARH_Q4";
            this.KVARH_Q4.Name = "KVARH_Q4";
            this.KVARH_Q4.ReadOnly = true;
            this.KVARH_Q4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // KVAH
            // 
            this.KVAH.DataPropertyName = "KVAH";
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.KVAH.DefaultCellStyle = dataGridViewCellStyle29;
            this.KVAH.HeaderText = "KVAH";
            this.KVAH.Name = "KVAH";
            this.KVAH.ReadOnly = true;
            this.KVAH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TAMPER
            // 
            this.TAMPER.DataPropertyName = "TAMPER";
            dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.TAMPER.DefaultCellStyle = dataGridViewCellStyle30;
            this.TAMPER.HeaderText = "TAMPER";
            this.TAMPER.Name = "TAMPER";
            this.TAMPER.ReadOnly = true;
            this.TAMPER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // MDI_KW
            // 
            this.MDI_KW.DataPropertyName = "MDI_KW";
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.MDI_KW.DefaultCellStyle = dataGridViewCellStyle31;
            this.MDI_KW.HeaderText = "MDI_KW";
            this.MDI_KW.Name = "MDI_KW";
            this.MDI_KW.ReadOnly = true;
            this.MDI_KW.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // MDI_KVAR
            // 
            this.MDI_KVAR.DataPropertyName = "MDI_KVAR";
            dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.MDI_KVAR.DefaultCellStyle = dataGridViewCellStyle32;
            this.MDI_KVAR.HeaderText = "MDI_KVAR";
            this.MDI_KVAR.Name = "MDI_KVAR";
            this.MDI_KVAR.ReadOnly = true;
            this.MDI_KVAR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // PF
            // 
            this.PF.DataPropertyName = "PF";
            dataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.PF.DefaultCellStyle = dataGridViewCellStyle33;
            this.PF.HeaderText = "PF";
            this.PF.Name = "PF";
            this.PF.ReadOnly = true;
            this.PF.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CAPTURE_TIME
            // 
            this.CAPTURE_TIME.DataPropertyName = "CAPTURE_TIME";
            dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CAPTURE_TIME.DefaultCellStyle = dataGridViewCellStyle34;
            this.CAPTURE_TIME.HeaderText = "CAPTURE_TIME";
            this.CAPTURE_TIME.Name = "CAPTURE_TIME";
            this.CAPTURE_TIME.ReadOnly = true;
            this.CAPTURE_TIME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DAY_MDI_KW
            // 
            this.DAY_MDI_KW.DataPropertyName = "DAY_MDI_KW";
            dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DAY_MDI_KW.DefaultCellStyle = dataGridViewCellStyle35;
            this.DAY_MDI_KW.HeaderText = "DAY_MDI_KW";
            this.DAY_MDI_KW.Name = "DAY_MDI_KW";
            this.DAY_MDI_KW.ReadOnly = true;
            this.DAY_MDI_KW.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DAY_MDI_KVAR
            // 
            this.DAY_MDI_KVAR.DataPropertyName = "DAY_MDI_KVAR";
            dataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DAY_MDI_KVAR.DefaultCellStyle = dataGridViewCellStyle36;
            this.DAY_MDI_KVAR.HeaderText = "DAY_MDI_KVAR";
            this.DAY_MDI_KVAR.Name = "DAY_MDI_KVAR";
            this.DAY_MDI_KVAR.ReadOnly = true;
            this.DAY_MDI_KVAR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // pnl_cb_day_record
            // 
            this.pnl_cb_day_record.Controls.Add(this.label23);
            this.pnl_cb_day_record.Controls.Add(this.grid_view_cb_day_record);
            this.pnl_cb_day_record.Controls.Add(this.lbl_record_counter);
            this.pnl_cb_day_record.Controls.Add(this.lbl_this_date_time);
            this.pnl_cb_day_record.Controls.Add(this.label24);
            this.pnl_cb_day_record.Controls.Add(this.label27);
            this.pnl_cb_day_record.Controls.Add(this.lbl_total_records);
            this.pnl_cb_day_record.Controls.Add(this.label22);
            this.pnl_cb_day_record.Controls.Add(this.lbl_record_no);
            this.pnl_cb_day_record.Controls.Add(this.label21);
            this.pnl_cb_day_record.Controls.Add(this.lbl_last_reset_date_time);
            this.pnl_cb_day_record.Controls.Add(this.btn_next);
            this.pnl_cb_day_record.Controls.Add(this.label25);
            this.pnl_cb_day_record.Controls.Add(this.btn_previous);
            this.pnl_cb_day_record.Location = new System.Drawing.Point(233, 7);
            this.pnl_cb_day_record.Name = "pnl_cb_day_record";
            this.pnl_cb_day_record.Size = new System.Drawing.Size(764, 407);
            this.pnl_cb_day_record.TabIndex = 22;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.Navy;
            this.label23.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label23.Location = new System.Drawing.Point(317, 1);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(158, 29);
            this.label23.TabIndex = 15;
            this.label23.Text = "CB Day Record";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grid_view_cb_day_record
            // 
            this.grid_view_cb_day_record.AllowUserToAddRows = false;
            this.grid_view_cb_day_record.AllowUserToDeleteRows = false;
            this.grid_view_cb_day_record.AllowUserToResizeColumns = false;
            this.grid_view_cb_day_record.AllowUserToResizeRows = false;
            dataGridViewCellStyle39.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle39.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle39.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle39.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle39.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle39.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle39.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid_view_cb_day_record.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle39;
            this.grid_view_cb_day_record.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid_view_cb_day_record.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Title,
            this.T1,
            this.T2,
            this.T3,
            this.T4,
            this.TL});
            dataGridViewCellStyle45.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle45.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle45.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle45.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle45.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle45.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle45.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grid_view_cb_day_record.DefaultCellStyle = dataGridViewCellStyle45;
            this.grid_view_cb_day_record.Location = new System.Drawing.Point(14, 76);
            this.grid_view_cb_day_record.Name = "grid_view_cb_day_record";
            this.grid_view_cb_day_record.ReadOnly = true;
            dataGridViewCellStyle46.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle46.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle46.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle46.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle46.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle46.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle46.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid_view_cb_day_record.RowHeadersDefaultCellStyle = dataGridViewCellStyle46;
            this.grid_view_cb_day_record.Size = new System.Drawing.Size(697, 298);
            this.grid_view_cb_day_record.TabIndex = 9;
            // 
            // Title
            // 
            this.Title.DataPropertyName = "title";
            this.Title.HeaderText = "Title";
            this.Title.Name = "Title";
            this.Title.ReadOnly = true;
            this.Title.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Title.Width = 150;
            // 
            // T1
            // 
            this.T1.DataPropertyName = "T1";
            dataGridViewCellStyle40.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.T1.DefaultCellStyle = dataGridViewCellStyle40;
            this.T1.HeaderText = "TL";
            this.T1.Name = "T1";
            this.T1.ReadOnly = true;
            this.T1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // T2
            // 
            this.T2.DataPropertyName = "T2";
            dataGridViewCellStyle41.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.T2.DefaultCellStyle = dataGridViewCellStyle41;
            this.T2.HeaderText = "T1";
            this.T2.Name = "T2";
            this.T2.ReadOnly = true;
            this.T2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // T3
            // 
            this.T3.DataPropertyName = "T3";
            dataGridViewCellStyle42.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.T3.DefaultCellStyle = dataGridViewCellStyle42;
            this.T3.HeaderText = "T2";
            this.T3.Name = "T3";
            this.T3.ReadOnly = true;
            this.T3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // T4
            // 
            this.T4.DataPropertyName = "T4";
            dataGridViewCellStyle43.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.T4.DefaultCellStyle = dataGridViewCellStyle43;
            this.T4.HeaderText = "T3";
            this.T4.Name = "T4";
            this.T4.ReadOnly = true;
            this.T4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TL
            // 
            this.TL.DataPropertyName = "TL";
            dataGridViewCellStyle44.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.TL.DefaultCellStyle = dataGridViewCellStyle44;
            this.TL.HeaderText = "T4";
            this.TL.Name = "TL";
            this.TL.ReadOnly = true;
            this.TL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // lbl_record_counter
            // 
            this.lbl_record_counter.AutoSize = true;
            this.lbl_record_counter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_record_counter.Location = new System.Drawing.Point(697, 60);
            this.lbl_record_counter.Name = "lbl_record_counter";
            this.lbl_record_counter.Size = new System.Drawing.Size(14, 13);
            this.lbl_record_counter.TabIndex = 17;
            this.lbl_record_counter.Text = "0";
            // 
            // lbl_this_date_time
            // 
            this.lbl_this_date_time.AutoSize = true;
            this.lbl_this_date_time.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_this_date_time.Location = new System.Drawing.Point(205, 55);
            this.lbl_this_date_time.Name = "lbl_this_date_time";
            this.lbl_this_date_time.Size = new System.Drawing.Size(14, 13);
            this.lbl_this_date_time.TabIndex = 21;
            this.lbl_this_date_time.Text = "0";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(625, 385);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(16, 13);
            this.label24.TabIndex = 15;
            this.label24.Text = "of";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(65, 55);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(101, 13);
            this.label27.TabIndex = 20;
            this.label27.Text = "This Date Time: ";
            // 
            // lbl_total_records
            // 
            this.lbl_total_records.AutoSize = true;
            this.lbl_total_records.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_total_records.Location = new System.Drawing.Point(647, 385);
            this.lbl_total_records.Name = "lbl_total_records";
            this.lbl_total_records.Size = new System.Drawing.Size(14, 13);
            this.lbl_total_records.TabIndex = 14;
            this.lbl_total_records.Text = "0";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(557, 60);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(104, 13);
            this.label22.TabIndex = 16;
            this.label22.Text = "Record Counter :";
            // 
            // lbl_record_no
            // 
            this.lbl_record_no.AutoSize = true;
            this.lbl_record_no.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_record_no.Location = new System.Drawing.Point(605, 385);
            this.lbl_record_no.Name = "lbl_record_no";
            this.lbl_record_no.Size = new System.Drawing.Size(14, 13);
            this.lbl_record_no.TabIndex = 13;
            this.lbl_record_no.Text = "0";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(557, 385);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(42, 13);
            this.label21.TabIndex = 12;
            this.label21.Text = "Record";
            // 
            // lbl_last_reset_date_time
            // 
            this.lbl_last_reset_date_time.AutoSize = true;
            this.lbl_last_reset_date_time.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_last_reset_date_time.Location = new System.Drawing.Point(205, 32);
            this.lbl_last_reset_date_time.Name = "lbl_last_reset_date_time";
            this.lbl_last_reset_date_time.Size = new System.Drawing.Size(14, 13);
            this.lbl_last_reset_date_time.TabIndex = 19;
            this.lbl_last_reset_date_time.Text = "0";
            // 
            // btn_next
            // 
            this.btn_next.Image = ((System.Drawing.Image)(resources.GetObject("btn_next.Image")));
            this.btn_next.Location = new System.Drawing.Point(393, 380);
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(35, 23);
            this.btn_next.TabIndex = 11;
            this.btn_next.UseVisualStyleBackColor = true;
            this.btn_next.Click += new System.EventHandler(this.btn_next_Click);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(65, 32);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(134, 13);
            this.label25.TabIndex = 18;
            this.label25.Text = "Last Reset Date Time:";
            // 
            // btn_previous
            // 
            this.btn_previous.Image = ((System.Drawing.Image)(resources.GetObject("btn_previous.Image")));
            this.btn_previous.Location = new System.Drawing.Point(352, 380);
            this.btn_previous.Name = "btn_previous";
            this.btn_previous.Size = new System.Drawing.Size(35, 23);
            this.btn_previous.TabIndex = 10;
            this.btn_previous.UseVisualStyleBackColor = true;
            this.btn_previous.Click += new System.EventHandler(this.btn_previous_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(87, 223);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(139, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Clear";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btn_TLLoadProfile
            // 
            this.btn_TLLoadProfile.Location = new System.Drawing.Point(87, 194);
            this.btn_TLLoadProfile.Name = "btn_TLLoadProfile";
            this.btn_TLLoadProfile.Size = new System.Drawing.Size(139, 23);
            this.btn_TLLoadProfile.TabIndex = 7;
            this.btn_TLLoadProfile.Text = "TL Load Profile";
            this.btn_TLLoadProfile.UseVisualStyleBackColor = true;
            this.btn_TLLoadProfile.Click += new System.EventHandler(this.btn_TLLoadProfile_Click_1);
            // 
            // btn_CBDayRecord
            // 
            this.btn_CBDayRecord.Location = new System.Drawing.Point(87, 165);
            this.btn_CBDayRecord.Name = "btn_CBDayRecord";
            this.btn_CBDayRecord.Size = new System.Drawing.Size(139, 23);
            this.btn_CBDayRecord.TabIndex = 6;
            this.btn_CBDayRecord.Text = "CB Day Record";
            this.btn_CBDayRecord.UseVisualStyleBackColor = true;
            this.btn_CBDayRecord.Click += new System.EventHandler(this.btn_CBDayRecord_Click_1);
            // 
            // btn_MakeError
            // 
            this.btn_MakeError.Location = new System.Drawing.Point(87, 136);
            this.btn_MakeError.Name = "btn_MakeError";
            this.btn_MakeError.Size = new System.Drawing.Size(139, 23);
            this.btn_MakeError.TabIndex = 5;
            this.btn_MakeError.Text = "Make Error";
            this.btn_MakeError.UseVisualStyleBackColor = true;
            // 
            // btn_Set_ReadRawData
            // 
            this.btn_Set_ReadRawData.Location = new System.Drawing.Point(87, 252);
            this.btn_Set_ReadRawData.Name = "btn_Set_ReadRawData";
            this.btn_Set_ReadRawData.Size = new System.Drawing.Size(139, 50);
            this.btn_Set_ReadRawData.TabIndex = 4;
            this.btn_Set_ReadRawData.Text = "Read Raw Data Addresses";
            this.btn_Set_ReadRawData.UseVisualStyleBackColor = true;
            // 
            // txt_EPMNumber
            // 
            this.txt_EPMNumber.Location = new System.Drawing.Point(87, 70);
            this.txt_EPMNumber.Name = "txt_EPMNumber";
            this.txt_EPMNumber.Size = new System.Drawing.Size(139, 20);
            this.txt_EPMNumber.TabIndex = 3;
            // 
            // txt_RawDataLength
            // 
            this.txt_RawDataLength.Location = new System.Drawing.Point(87, 36);
            this.txt_RawDataLength.Name = "txt_RawDataLength";
            this.txt_RawDataLength.Size = new System.Drawing.Size(139, 20);
            this.txt_RawDataLength.TabIndex = 3;
            // 
            // txt_RawDataAddress
            // 
            this.txt_RawDataAddress.Location = new System.Drawing.Point(87, 6);
            this.txt_RawDataAddress.Name = "txt_RawDataAddress";
            this.txt_RawDataAddress.Size = new System.Drawing.Size(139, 20);
            this.txt_RawDataAddress.TabIndex = 3;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(9, 70);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(70, 13);
            this.label20.TabIndex = 2;
            this.label20.Text = "EPM Number";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(9, 36);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(40, 13);
            this.label19.TabIndex = 2;
            this.label19.Text = "Length";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(9, 6);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(45, 13);
            this.label18.TabIndex = 2;
            this.label18.Text = "Address";
            // 
            // btn_ReadRawData
            // 
            this.btn_ReadRawData.Location = new System.Drawing.Point(87, 107);
            this.btn_ReadRawData.Name = "btn_ReadRawData";
            this.btn_ReadRawData.Size = new System.Drawing.Size(139, 23);
            this.btn_ReadRawData.TabIndex = 1;
            this.btn_ReadRawData.Text = "Read Raw Data";
            this.btn_ReadRawData.UseVisualStyleBackColor = true;
            // 
            // txt_general
            // 
            this.txt_general.Location = new System.Drawing.Point(6, 333);
            this.txt_general.Name = "txt_general";
            this.txt_general.Size = new System.Drawing.Size(220, 81);
            this.txt_general.TabIndex = 0;
            this.txt_general.Text = "";
            this.txt_general.WordWrap = false;
            // 
            // AllInOnetab
            // 
            this.AllInOnetab.Controls.Add(this.lbl_meter_date_time);
            this.AllInOnetab.Controls.Add(this.label51);
            this.AllInOnetab.Controls.Add(this.lbl_meterserial);
            this.AllInOnetab.Controls.Add(this.label60);
            this.AllInOnetab.Controls.Add(this.txt_alarm_status);
            this.AllInOnetab.Controls.Add(this.lbl_pt_denominator);
            this.AllInOnetab.Controls.Add(this.label59);
            this.AllInOnetab.Controls.Add(this.lbl_pt_nominator);
            this.AllInOnetab.Controls.Add(this.label61);
            this.AllInOnetab.Controls.Add(this.lbl_ct_denominator);
            this.AllInOnetab.Controls.Add(this.label47);
            this.AllInOnetab.Controls.Add(this.lbl_ct_nominator);
            this.AllInOnetab.Controls.Add(this.label45);
            this.AllInOnetab.Controls.Add(this.label42);
            this.AllInOnetab.Controls.Add(this.lbl_mdi_count);
            this.AllInOnetab.Controls.Add(this.label43);
            this.AllInOnetab.Controls.Add(this.lbl_mdi_end_date);
            this.AllInOnetab.Controls.Add(this.label32);
            this.AllInOnetab.Controls.Add(this.lbl_slide_count);
            this.AllInOnetab.Controls.Add(this.label38);
            this.AllInOnetab.Controls.Add(this.lbl_mdi_pre_kvar);
            this.AllInOnetab.Controls.Add(this.lbl_mdi_pre_kw);
            this.AllInOnetab.Controls.Add(this.label40);
            this.AllInOnetab.Controls.Add(this.label41);
            this.AllInOnetab.Controls.Add(this.lblmdi_time);
            this.AllInOnetab.Controls.Add(this.lbl_timer);
            this.AllInOnetab.Controls.Add(this.label39);
            this.AllInOnetab.Controls.Add(this.timer);
            this.AllInOnetab.Controls.Add(this.tab_Billing);
            this.AllInOnetab.Controls.Add(this.grv_event_logs);
            this.AllInOnetab.Controls.Add(this.lbl_day_profile);
            this.AllInOnetab.Controls.Add(this.lbl_season_profile);
            this.AllInOnetab.Controls.Add(this.label55);
            this.AllInOnetab.Controls.Add(this.label57);
            this.AllInOnetab.Controls.Add(this.lbl_channel_4);
            this.AllInOnetab.Controls.Add(this.lbl_channel_2);
            this.AllInOnetab.Controls.Add(this.lbl_count);
            this.AllInOnetab.Controls.Add(this.lbl_channel_3);
            this.AllInOnetab.Controls.Add(this.lbl_channel_1);
            this.AllInOnetab.Controls.Add(this.lbl_time_id);
            this.AllInOnetab.Controls.Add(this.lbl_FRQ);
            this.AllInOnetab.Controls.Add(this.lbl_TP);
            this.AllInOnetab.Controls.Add(this.label37);
            this.AllInOnetab.Controls.Add(this.label35);
            this.AllInOnetab.Controls.Add(this.label36);
            this.AllInOnetab.Controls.Add(this.label33);
            this.AllInOnetab.Controls.Add(this.label34);
            this.AllInOnetab.Controls.Add(this.label30);
            this.AllInOnetab.Controls.Add(this.label31);
            this.AllInOnetab.Controls.Add(this.label29);
            this.AllInOnetab.Controls.Add(this.label28);
            this.AllInOnetab.Controls.Add(this.grv_general_instentanious);
            this.AllInOnetab.Location = new System.Drawing.Point(4, 22);
            this.AllInOnetab.Name = "AllInOnetab";
            this.AllInOnetab.Size = new System.Drawing.Size(1003, 420);
            this.AllInOnetab.TabIndex = 2;
            this.AllInOnetab.Text = "All In One Detail";
            this.AllInOnetab.UseVisualStyleBackColor = true;
            // 
            // lbl_meter_date_time
            // 
            this.lbl_meter_date_time.AutoSize = true;
            this.lbl_meter_date_time.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_meter_date_time.Location = new System.Drawing.Point(819, 10);
            this.lbl_meter_date_time.Name = "lbl_meter_date_time";
            this.lbl_meter_date_time.Size = new System.Drawing.Size(19, 14);
            this.lbl_meter_date_time.TabIndex = 61;
            this.lbl_meter_date_time.Text = "---";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.Location = new System.Drawing.Point(703, 10);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(95, 14);
            this.label51.TabIndex = 60;
            this.label51.Text = "Meter Sate Time:";
            // 
            // lbl_meterserial
            // 
            this.lbl_meterserial.AutoSize = true;
            this.lbl_meterserial.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_meterserial.Location = new System.Drawing.Point(553, 10);
            this.lbl_meterserial.Name = "lbl_meterserial";
            this.lbl_meterserial.Size = new System.Drawing.Size(19, 14);
            this.lbl_meterserial.TabIndex = 59;
            this.lbl_meterserial.Text = "---";
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label60.Location = new System.Drawing.Point(463, 10);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(73, 14);
            this.label60.TabIndex = 58;
            this.label60.Text = "Meter Serial:";
            // 
            // txt_alarm_status
            // 
            this.txt_alarm_status.Location = new System.Drawing.Point(98, 162);
            this.txt_alarm_status.Name = "txt_alarm_status";
            this.txt_alarm_status.ReadOnly = true;
            this.txt_alarm_status.Size = new System.Drawing.Size(254, 28);
            this.txt_alarm_status.TabIndex = 57;
            this.txt_alarm_status.Text = "";
            // 
            // lbl_pt_denominator
            // 
            this.lbl_pt_denominator.AutoSize = true;
            this.lbl_pt_denominator.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pt_denominator.Location = new System.Drawing.Point(382, 144);
            this.lbl_pt_denominator.Name = "lbl_pt_denominator";
            this.lbl_pt_denominator.Size = new System.Drawing.Size(19, 14);
            this.lbl_pt_denominator.TabIndex = 56;
            this.lbl_pt_denominator.Text = "---";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label59.Location = new System.Drawing.Point(365, 144);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(13, 14);
            this.label59.TabIndex = 55;
            this.label59.Text = "/";
            // 
            // lbl_pt_nominator
            // 
            this.lbl_pt_nominator.AutoSize = true;
            this.lbl_pt_nominator.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pt_nominator.Location = new System.Drawing.Point(342, 144);
            this.lbl_pt_nominator.Name = "lbl_pt_nominator";
            this.lbl_pt_nominator.Size = new System.Drawing.Size(19, 14);
            this.lbl_pt_nominator.TabIndex = 54;
            this.lbl_pt_nominator.Text = "---";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label61.Location = new System.Drawing.Point(295, 144);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(24, 14);
            this.label61.TabIndex = 53;
            this.label61.Text = "PT:";
            // 
            // lbl_ct_denominator
            // 
            this.lbl_ct_denominator.AutoSize = true;
            this.lbl_ct_denominator.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ct_denominator.Location = new System.Drawing.Point(154, 144);
            this.lbl_ct_denominator.Name = "lbl_ct_denominator";
            this.lbl_ct_denominator.Size = new System.Drawing.Size(19, 14);
            this.lbl_ct_denominator.TabIndex = 52;
            this.lbl_ct_denominator.Text = "---";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.Location = new System.Drawing.Point(137, 144);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(13, 14);
            this.label47.TabIndex = 51;
            this.label47.Text = "/";
            // 
            // lbl_ct_nominator
            // 
            this.lbl_ct_nominator.AutoSize = true;
            this.lbl_ct_nominator.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ct_nominator.Location = new System.Drawing.Point(114, 144);
            this.lbl_ct_nominator.Name = "lbl_ct_nominator";
            this.lbl_ct_nominator.Size = new System.Drawing.Size(19, 14);
            this.lbl_ct_nominator.TabIndex = 50;
            this.lbl_ct_nominator.Text = "---";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.Location = new System.Drawing.Point(67, 144);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(24, 14);
            this.label45.TabIndex = 49;
            this.label45.Text = "CT:";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(12, 166);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(79, 14);
            this.label42.TabIndex = 47;
            this.label42.Text = "Alarm Status :";
            // 
            // lbl_mdi_count
            // 
            this.lbl_mdi_count.AutoSize = true;
            this.lbl_mdi_count.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_mdi_count.Location = new System.Drawing.Point(333, 120);
            this.lbl_mdi_count.Name = "lbl_mdi_count";
            this.lbl_mdi_count.Size = new System.Drawing.Size(19, 14);
            this.lbl_mdi_count.TabIndex = 46;
            this.lbl_mdi_count.Text = "---";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(222, 120);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(97, 14);
            this.label43.TabIndex = 45;
            this.label43.Text = "MDI Reset Count:";
            // 
            // lbl_mdi_end_date
            // 
            this.lbl_mdi_end_date.AutoSize = true;
            this.lbl_mdi_end_date.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_mdi_end_date.Location = new System.Drawing.Point(95, 120);
            this.lbl_mdi_end_date.Name = "lbl_mdi_end_date";
            this.lbl_mdi_end_date.Size = new System.Drawing.Size(19, 14);
            this.lbl_mdi_end_date.TabIndex = 44;
            this.lbl_mdi_end_date.Text = "---";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(3, 120);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(93, 14);
            this.label32.TabIndex = 40;
            this.label32.Text = "MDI Reset Date :";
            // 
            // lbl_slide_count
            // 
            this.lbl_slide_count.AutoSize = true;
            this.lbl_slide_count.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_slide_count.Location = new System.Drawing.Point(114, 74);
            this.lbl_slide_count.Name = "lbl_slide_count";
            this.lbl_slide_count.Size = new System.Drawing.Size(19, 14);
            this.lbl_slide_count.TabIndex = 39;
            this.lbl_slide_count.Text = "---";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(27, 74);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(69, 14);
            this.label38.TabIndex = 38;
            this.label38.Text = "Slide Count:";
            // 
            // lbl_mdi_pre_kvar
            // 
            this.lbl_mdi_pre_kvar.AutoSize = true;
            this.lbl_mdi_pre_kvar.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_mdi_pre_kvar.Location = new System.Drawing.Point(333, 95);
            this.lbl_mdi_pre_kvar.Name = "lbl_mdi_pre_kvar";
            this.lbl_mdi_pre_kvar.Size = new System.Drawing.Size(19, 14);
            this.lbl_mdi_pre_kvar.TabIndex = 37;
            this.lbl_mdi_pre_kvar.Text = "---";
            // 
            // lbl_mdi_pre_kw
            // 
            this.lbl_mdi_pre_kw.AutoSize = true;
            this.lbl_mdi_pre_kw.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_mdi_pre_kw.Location = new System.Drawing.Point(114, 95);
            this.lbl_mdi_pre_kw.Name = "lbl_mdi_pre_kw";
            this.lbl_mdi_pre_kw.Size = new System.Drawing.Size(19, 14);
            this.lbl_mdi_pre_kw.TabIndex = 36;
            this.lbl_mdi_pre_kw.Text = "---";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(227, 95);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(92, 14);
            this.label40.TabIndex = 35;
            this.label40.Text = "MDI_Pre. (kvar):";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(13, 95);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(83, 14);
            this.label41.TabIndex = 34;
            this.label41.Text = "MDI Pre. (kw):";
            // 
            // lblmdi_time
            // 
            this.lblmdi_time.AutoSize = true;
            this.lblmdi_time.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmdi_time.Location = new System.Drawing.Point(333, 54);
            this.lblmdi_time.Name = "lblmdi_time";
            this.lblmdi_time.Size = new System.Drawing.Size(19, 14);
            this.lblmdi_time.TabIndex = 33;
            this.lblmdi_time.Text = "---";
            // 
            // lbl_timer
            // 
            this.lbl_timer.AutoSize = true;
            this.lbl_timer.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_timer.Location = new System.Drawing.Point(114, 54);
            this.lbl_timer.Name = "lbl_timer";
            this.lbl_timer.Size = new System.Drawing.Size(19, 14);
            this.lbl_timer.TabIndex = 32;
            this.lbl_timer.Text = "---";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(257, 54);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(62, 14);
            this.label39.TabIndex = 31;
            this.label39.Text = "MDI TIme:";
            // 
            // timer
            // 
            this.timer.AutoSize = true;
            this.timer.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timer.Location = new System.Drawing.Point(12, 54);
            this.timer.Name = "timer";
            this.timer.Size = new System.Drawing.Size(84, 14);
            this.timer.TabIndex = 30;
            this.timer.Text = "MDI Time Left:";
            // 
            // tab_Billing
            // 
            this.tab_Billing.Controls.Add(this.Commulative);
            this.tab_Billing.Controls.Add(this.Monthly);
            this.tab_Billing.Location = new System.Drawing.Point(463, 31);
            this.tab_Billing.Name = "tab_Billing";
            this.tab_Billing.SelectedIndex = 0;
            this.tab_Billing.Size = new System.Drawing.Size(531, 204);
            this.tab_Billing.TabIndex = 29;
            // 
            // Commulative
            // 
            this.Commulative.Controls.Add(this.grv_commulative_billing);
            this.Commulative.Location = new System.Drawing.Point(4, 22);
            this.Commulative.Name = "Commulative";
            this.Commulative.Padding = new System.Windows.Forms.Padding(3);
            this.Commulative.Size = new System.Drawing.Size(523, 178);
            this.Commulative.TabIndex = 0;
            this.Commulative.Text = "Commulative";
            this.Commulative.UseVisualStyleBackColor = true;
            // 
            // grv_commulative_billing
            // 
            this.grv_commulative_billing.AllowUserToAddRows = false;
            this.grv_commulative_billing.AllowUserToDeleteRows = false;
            dataGridViewCellStyle47.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle47.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle47.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle47.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle47.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle47.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle47.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grv_commulative_billing.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle47;
            this.grv_commulative_billing.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grv_commulative_billing.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.T_L});
            dataGridViewCellStyle48.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle48.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle48.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle48.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle48.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle48.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle48.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grv_commulative_billing.DefaultCellStyle = dataGridViewCellStyle48;
            this.grv_commulative_billing.Location = new System.Drawing.Point(3, 3);
            this.grv_commulative_billing.Name = "grv_commulative_billing";
            this.grv_commulative_billing.ReadOnly = true;
            dataGridViewCellStyle49.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle49.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle49.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle49.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle49.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle49.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle49.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grv_commulative_billing.RowHeadersDefaultCellStyle = dataGridViewCellStyle49;
            this.grv_commulative_billing.Size = new System.Drawing.Size(514, 169);
            this.grv_commulative_billing.TabIndex = 30;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Title";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn5.Width = 200;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "TL";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn6.Width = 60;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "T1";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn7.Width = 60;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "PhaseC";
            this.dataGridViewTextBoxColumn8.HeaderText = "T2";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn8.Width = 60;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "T3";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // T_L
            // 
            this.T_L.HeaderText = "T4";
            this.T_L.Name = "T_L";
            this.T_L.ReadOnly = true;
            this.T_L.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Monthly
            // 
            this.Monthly.Controls.Add(this.grv_monthly_billing);
            this.Monthly.Location = new System.Drawing.Point(4, 22);
            this.Monthly.Name = "Monthly";
            this.Monthly.Padding = new System.Windows.Forms.Padding(3);
            this.Monthly.Size = new System.Drawing.Size(523, 178);
            this.Monthly.TabIndex = 1;
            this.Monthly.Text = "Monthly";
            this.Monthly.UseVisualStyleBackColor = true;
            // 
            // grv_monthly_billing
            // 
            this.grv_monthly_billing.AllowUserToAddRows = false;
            this.grv_monthly_billing.AllowUserToDeleteRows = false;
            dataGridViewCellStyle50.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle50.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle50.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle50.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle50.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle50.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle50.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grv_monthly_billing.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle50;
            this.grv_monthly_billing.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grv_monthly_billing.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12,
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn14,
            this.dataGridViewTextBoxColumn15});
            dataGridViewCellStyle51.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle51.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle51.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle51.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle51.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle51.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle51.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grv_monthly_billing.DefaultCellStyle = dataGridViewCellStyle51;
            this.grv_monthly_billing.Location = new System.Drawing.Point(4, 5);
            this.grv_monthly_billing.Name = "grv_monthly_billing";
            this.grv_monthly_billing.ReadOnly = true;
            dataGridViewCellStyle52.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle52.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle52.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle52.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle52.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle52.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle52.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grv_monthly_billing.RowHeadersDefaultCellStyle = dataGridViewCellStyle52;
            this.grv_monthly_billing.Size = new System.Drawing.Size(514, 169);
            this.grv_monthly_billing.TabIndex = 31;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "Title";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Width = 200;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "TL";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Width = 60;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "T1";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.Width = 60;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.DataPropertyName = "PhaseC";
            this.dataGridViewTextBoxColumn13.HeaderText = "T2";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            this.dataGridViewTextBoxColumn13.Width = 60;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.HeaderText = "T3";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.HeaderText = "T4";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.ReadOnly = true;
            // 
            // grv_event_logs
            // 
            this.grv_event_logs.AllowUserToAddRows = false;
            this.grv_event_logs.AllowUserToDeleteRows = false;
            dataGridViewCellStyle53.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle53.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle53.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle53.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle53.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle53.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle53.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grv_event_logs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle53;
            this.grv_event_logs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grv_event_logs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.Detail});
            dataGridViewCellStyle54.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle54.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle54.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle54.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle54.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle54.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle54.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grv_event_logs.DefaultCellStyle = dataGridViewCellStyle54;
            this.grv_event_logs.Location = new System.Drawing.Point(463, 241);
            this.grv_event_logs.Name = "grv_event_logs";
            this.grv_event_logs.ReadOnly = true;
            dataGridViewCellStyle55.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle55.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle55.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle55.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle55.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle55.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle55.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grv_event_logs.RowHeadersDefaultCellStyle = dataGridViewCellStyle55;
            this.grv_event_logs.Size = new System.Drawing.Size(531, 176);
            this.grv_event_logs.TabIndex = 28;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Title";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 200;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Event Code";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 60;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Count";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 60;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "PhaseC";
            this.dataGridViewTextBoxColumn4.HeaderText = "Date Time";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn4.Width = 60;
            // 
            // Detail
            // 
            this.Detail.HeaderText = "Detail";
            this.Detail.Name = "Detail";
            this.Detail.ReadOnly = true;
            this.Detail.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // lbl_day_profile
            // 
            this.lbl_day_profile.AutoSize = true;
            this.lbl_day_profile.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_day_profile.Location = new System.Drawing.Point(333, 35);
            this.lbl_day_profile.Name = "lbl_day_profile";
            this.lbl_day_profile.Size = new System.Drawing.Size(19, 14);
            this.lbl_day_profile.TabIndex = 27;
            this.lbl_day_profile.Text = "---";
            this.lbl_day_profile.Click += new System.EventHandler(this.label51_Click);
            // 
            // lbl_season_profile
            // 
            this.lbl_season_profile.AutoSize = true;
            this.lbl_season_profile.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_season_profile.Location = new System.Drawing.Point(333, 16);
            this.lbl_season_profile.Name = "lbl_season_profile";
            this.lbl_season_profile.Size = new System.Drawing.Size(19, 14);
            this.lbl_season_profile.TabIndex = 26;
            this.lbl_season_profile.Text = "---";
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label55.Location = new System.Drawing.Point(208, 35);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(111, 14);
            this.label55.TabIndex = 25;
            this.label55.Text = "Current Day Profile:";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label57.Location = new System.Drawing.Point(192, 16);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(127, 14);
            this.label57.TabIndex = 24;
            this.label57.Text = "Current Season Profile:";
            // 
            // lbl_channel_4
            // 
            this.lbl_channel_4.AutoSize = true;
            this.lbl_channel_4.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_channel_4.Location = new System.Drawing.Point(333, 245);
            this.lbl_channel_4.Name = "lbl_channel_4";
            this.lbl_channel_4.Size = new System.Drawing.Size(19, 14);
            this.lbl_channel_4.TabIndex = 23;
            this.lbl_channel_4.Text = "---";
            // 
            // lbl_channel_2
            // 
            this.lbl_channel_2.AutoSize = true;
            this.lbl_channel_2.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_channel_2.Location = new System.Drawing.Point(333, 225);
            this.lbl_channel_2.Name = "lbl_channel_2";
            this.lbl_channel_2.Size = new System.Drawing.Size(19, 14);
            this.lbl_channel_2.TabIndex = 22;
            this.lbl_channel_2.Text = "---";
            // 
            // lbl_count
            // 
            this.lbl_count.AutoSize = true;
            this.lbl_count.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_count.Location = new System.Drawing.Point(333, 205);
            this.lbl_count.Name = "lbl_count";
            this.lbl_count.Size = new System.Drawing.Size(19, 14);
            this.lbl_count.TabIndex = 20;
            this.lbl_count.Text = "---";
            // 
            // lbl_channel_3
            // 
            this.lbl_channel_3.AutoSize = true;
            this.lbl_channel_3.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_channel_3.Location = new System.Drawing.Point(114, 245);
            this.lbl_channel_3.Name = "lbl_channel_3";
            this.lbl_channel_3.Size = new System.Drawing.Size(19, 14);
            this.lbl_channel_3.TabIndex = 19;
            this.lbl_channel_3.Text = "---";
            // 
            // lbl_channel_1
            // 
            this.lbl_channel_1.AutoSize = true;
            this.lbl_channel_1.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_channel_1.Location = new System.Drawing.Point(114, 225);
            this.lbl_channel_1.Name = "lbl_channel_1";
            this.lbl_channel_1.Size = new System.Drawing.Size(19, 14);
            this.lbl_channel_1.TabIndex = 18;
            this.lbl_channel_1.Text = "---";
            // 
            // lbl_time_id
            // 
            this.lbl_time_id.AutoSize = true;
            this.lbl_time_id.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_time_id.Location = new System.Drawing.Point(114, 205);
            this.lbl_time_id.Name = "lbl_time_id";
            this.lbl_time_id.Size = new System.Drawing.Size(19, 14);
            this.lbl_time_id.TabIndex = 17;
            this.lbl_time_id.Text = "---";
            // 
            // lbl_FRQ
            // 
            this.lbl_FRQ.AutoSize = true;
            this.lbl_FRQ.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_FRQ.Location = new System.Drawing.Point(114, 35);
            this.lbl_FRQ.Name = "lbl_FRQ";
            this.lbl_FRQ.Size = new System.Drawing.Size(19, 14);
            this.lbl_FRQ.TabIndex = 16;
            this.lbl_FRQ.Text = "---";
            // 
            // lbl_TP
            // 
            this.lbl_TP.AutoSize = true;
            this.lbl_TP.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TP.Location = new System.Drawing.Point(114, 16);
            this.lbl_TP.Name = "lbl_TP";
            this.lbl_TP.Size = new System.Drawing.Size(19, 14);
            this.lbl_TP.TabIndex = 15;
            this.lbl_TP.Text = "---";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(264, 205);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(41, 14);
            this.label37.TabIndex = 14;
            this.label37.Text = "Count:";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(238, 245);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(67, 14);
            this.label35.TabIndex = 13;
            this.label35.Text = "Channel#4:";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(29, 245);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(67, 14);
            this.label36.TabIndex = 12;
            this.label36.Text = "Channel#3:";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(238, 225);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(67, 14);
            this.label33.TabIndex = 11;
            this.label33.Text = "Channel#2:";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(29, 225);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(67, 14);
            this.label34.TabIndex = 10;
            this.label34.Text = "Channel#1:";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(45, 205);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(51, 14);
            this.label30.TabIndex = 8;
            this.label30.Text = "Time ID:";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(10, 186);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(72, 14);
            this.label31.TabIndex = 7;
            this.label31.Text = "Load Profile:";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(31, 35);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(65, 14);
            this.label29.TabIndex = 6;
            this.label29.Text = "Frequency:";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(10, 16);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(86, 14);
            this.label28.TabIndex = 5;
            this.label28.Text = "Tamper Power:";
            // 
            // grv_general_instentanious
            // 
            this.grv_general_instentanious.AllowUserToAddRows = false;
            this.grv_general_instentanious.AllowUserToDeleteRows = false;
            dataGridViewCellStyle56.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle56.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle56.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle56.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle56.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle56.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle56.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grv_general_instentanious.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle56;
            this.grv_general_instentanious.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grv_general_instentanious.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.InstentaniousTitle,
            this.Phase1,
            this.Phase2,
            this.Phase3,
            this.Avg_Total});
            dataGridViewCellStyle57.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle57.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle57.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle57.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle57.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle57.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle57.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grv_general_instentanious.DefaultCellStyle = dataGridViewCellStyle57;
            this.grv_general_instentanious.Location = new System.Drawing.Point(14, 267);
            this.grv_general_instentanious.Name = "grv_general_instentanious";
            this.grv_general_instentanious.ReadOnly = true;
            dataGridViewCellStyle58.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle58.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle58.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle58.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle58.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle58.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle58.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grv_general_instentanious.RowHeadersDefaultCellStyle = dataGridViewCellStyle58;
            this.grv_general_instentanious.Size = new System.Drawing.Size(429, 150);
            this.grv_general_instentanious.TabIndex = 0;
            // 
            // InstentaniousTitle
            // 
            this.InstentaniousTitle.HeaderText = "Title";
            this.InstentaniousTitle.Name = "InstentaniousTitle";
            this.InstentaniousTitle.ReadOnly = true;
            this.InstentaniousTitle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.InstentaniousTitle.Width = 200;
            // 
            // Phase1
            // 
            this.Phase1.DataPropertyName = "PhaseA";
            this.Phase1.HeaderText = "Phase1";
            this.Phase1.Name = "Phase1";
            this.Phase1.ReadOnly = true;
            this.Phase1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Phase1.Width = 60;
            // 
            // Phase2
            // 
            this.Phase2.DataPropertyName = "PhaseB";
            this.Phase2.HeaderText = "Phase2";
            this.Phase2.Name = "Phase2";
            this.Phase2.ReadOnly = true;
            this.Phase2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Phase2.Width = 60;
            // 
            // Phase3
            // 
            this.Phase3.DataPropertyName = "PhaseC";
            this.Phase3.HeaderText = "Phase3";
            this.Phase3.Name = "Phase3";
            this.Phase3.ReadOnly = true;
            this.Phase3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Phase3.Width = 60;
            // 
            // Avg_Total
            // 
            this.Avg_Total.DataPropertyName = "PhaseAvg_Total";
            this.Avg_Total.HeaderText = "Avg/Total";
            this.Avg_Total.Name = "Avg_Total";
            this.Avg_Total.ReadOnly = true;
            this.Avg_Total.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tab_CrDwData
            // 
            this.tab_CrDwData.Controls.Add(this.progressBar1);
            this.tab_CrDwData.Controls.Add(this.label17);
            this.tab_CrDwData.Controls.Add(this.radio_test);
            this.tab_CrDwData.Controls.Add(this.radio_alt);
            this.tab_CrDwData.Controls.Add(this.radio_nor);
            this.tab_CrDwData.Controls.Add(this.btn_get_Dwd);
            this.tab_CrDwData.Controls.Add(this.rtb_1);
            this.tab_CrDwData.Location = new System.Drawing.Point(4, 22);
            this.tab_CrDwData.Name = "tab_CrDwData";
            this.tab_CrDwData.Size = new System.Drawing.Size(1038, 453);
            this.tab_CrDwData.TabIndex = 6;
            this.tab_CrDwData.Text = "Current Dw Data";
            this.tab_CrDwData.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(563, 111);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 5;
            this.progressBar1.Visible = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(17, 34);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(270, 25);
            this.label17.TabIndex = 4;
            this.label17.Text = "Display Windows Values";
            // 
            // radio_test
            // 
            this.radio_test.AutoSize = true;
            this.radio_test.Location = new System.Drawing.Point(749, 70);
            this.radio_test.Name = "radio_test";
            this.radio_test.Size = new System.Drawing.Size(76, 17);
            this.radio_test.TabIndex = 3;
            this.radio_test.TabStop = true;
            this.radio_test.Text = "Test Mode";
            this.radio_test.UseVisualStyleBackColor = true;
            // 
            // radio_alt
            // 
            this.radio_alt.AutoSize = true;
            this.radio_alt.Location = new System.Drawing.Point(654, 70);
            this.radio_alt.Name = "radio_alt";
            this.radio_alt.Size = new System.Drawing.Size(67, 17);
            this.radio_alt.TabIndex = 2;
            this.radio_alt.Text = "Alternate";
            this.radio_alt.UseVisualStyleBackColor = true;
            // 
            // radio_nor
            // 
            this.radio_nor.AutoSize = true;
            this.radio_nor.Checked = true;
            this.radio_nor.Location = new System.Drawing.Point(563, 70);
            this.radio_nor.Name = "radio_nor";
            this.radio_nor.Size = new System.Drawing.Size(58, 17);
            this.radio_nor.TabIndex = 2;
            this.radio_nor.TabStop = true;
            this.radio_nor.Text = "Normal";
            this.radio_nor.UseVisualStyleBackColor = true;
            // 
            // btn_get_Dwd
            // 
            this.btn_get_Dwd.Location = new System.Drawing.Point(563, 25);
            this.btn_get_Dwd.Name = "btn_get_Dwd";
            this.btn_get_Dwd.Size = new System.Drawing.Size(75, 23);
            this.btn_get_Dwd.TabIndex = 1;
            this.btn_get_Dwd.Text = "GET";
            this.btn_get_Dwd.UseVisualStyleBackColor = true;
            this.btn_get_Dwd.Click += new System.EventHandler(this.btn_get_Dwd_Click);
            // 
            // rtb_1
            // 
            this.rtb_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_1.Location = new System.Drawing.Point(17, 81);
            this.rtb_1.Name = "rtb_1";
            this.rtb_1.ReadOnly = true;
            this.rtb_1.Size = new System.Drawing.Size(515, 251);
            this.rtb_1.TabIndex = 0;
            this.rtb_1.Text = "";
            // 
            // tab_Contactor
            // 
            this.tab_Contactor.Controls.Add(this.groupBox9);
            this.tab_Contactor.Controls.Add(this.groupBox8);
            this.tab_Contactor.Controls.Add(this.groupBox6);
            this.tab_Contactor.Controls.Add(this.groupBox7);
            this.tab_Contactor.Controls.Add(this.groupBox5);
            this.tab_Contactor.Controls.Add(this.btn_GetContactorFlags);
            this.tab_Contactor.Location = new System.Drawing.Point(4, 22);
            this.tab_Contactor.Name = "tab_Contactor";
            this.tab_Contactor.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Contactor.Size = new System.Drawing.Size(1038, 453);
            this.tab_Contactor.TabIndex = 7;
            this.tab_Contactor.Text = "Contactor";
            this.tab_Contactor.UseVisualStyleBackColor = true;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.chkSkipSchedule);
            this.groupBox9.Controls.Add(this.chkWaitOnTariffChange);
            this.groupBox9.Controls.Add(this.chkContactorDisabled);
            this.groupBox9.Controls.Add(this.chkIsCapCharged);
            this.groupBox9.Controls.Add(this.chkOnForRetryBySwitch);
            this.groupBox9.Controls.Add(this.chkPUDContactor);
            this.groupBox9.Controls.Add(this.chkContactorState);
            this.groupBox9.Controls.Add(this.chkContactorEventIndex);
            this.groupBox9.Controls.Add(this.chkContactorToOn);
            this.groupBox9.Controls.Add(this.chkMakeCOntactorEvent);
            this.groupBox9.Controls.Add(this.chkDelayBWContactorState);
            this.groupBox9.Controls.Add(this.chkMakePulseContactor);
            this.groupBox9.Location = new System.Drawing.Point(9, 49);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(202, 388);
            this.groupBox9.TabIndex = 24;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Comon Flags";
            // 
            // chkSkipSchedule
            // 
            this.chkSkipSchedule.AutoSize = true;
            this.chkSkipSchedule.Enabled = false;
            this.chkSkipSchedule.Location = new System.Drawing.Point(6, 277);
            this.chkSkipSchedule.Name = "chkSkipSchedule";
            this.chkSkipSchedule.Size = new System.Drawing.Size(95, 17);
            this.chkSkipSchedule.TabIndex = 5;
            this.chkSkipSchedule.Text = "Skip Schedule";
            this.chkSkipSchedule.UseVisualStyleBackColor = true;
            // 
            // chkWaitOnTariffChange
            // 
            this.chkWaitOnTariffChange.AutoSize = true;
            this.chkWaitOnTariffChange.Enabled = false;
            this.chkWaitOnTariffChange.Location = new System.Drawing.Point(6, 233);
            this.chkWaitOnTariffChange.Name = "chkWaitOnTariffChange";
            this.chkWaitOnTariffChange.Size = new System.Drawing.Size(132, 17);
            this.chkWaitOnTariffChange.TabIndex = 4;
            this.chkWaitOnTariffChange.Text = "Wait On Tariff Change";
            this.chkWaitOnTariffChange.UseVisualStyleBackColor = true;
            // 
            // chkContactorDisabled
            // 
            this.chkContactorDisabled.AutoSize = true;
            this.chkContactorDisabled.Enabled = false;
            this.chkContactorDisabled.Location = new System.Drawing.Point(6, 187);
            this.chkContactorDisabled.Name = "chkContactorDisabled";
            this.chkContactorDisabled.Size = new System.Drawing.Size(116, 17);
            this.chkContactorDisabled.TabIndex = 4;
            this.chkContactorDisabled.Text = "Contactor Disabled";
            this.chkContactorDisabled.UseVisualStyleBackColor = true;
            this.chkContactorDisabled.CheckedChanged += new System.EventHandler(this.checkBox9_CheckedChanged);
            // 
            // chkIsCapCharged
            // 
            this.chkIsCapCharged.AutoSize = true;
            this.chkIsCapCharged.Enabled = false;
            this.chkIsCapCharged.Location = new System.Drawing.Point(6, 141);
            this.chkIsCapCharged.Name = "chkIsCapCharged";
            this.chkIsCapCharged.Size = new System.Drawing.Size(99, 17);
            this.chkIsCapCharged.TabIndex = 3;
            this.chkIsCapCharged.Text = "Is Cap Charged";
            this.chkIsCapCharged.UseVisualStyleBackColor = true;
            // 
            // chkOnForRetryBySwitch
            // 
            this.chkOnForRetryBySwitch.AutoSize = true;
            this.chkOnForRetryBySwitch.Enabled = false;
            this.chkOnForRetryBySwitch.Location = new System.Drawing.Point(5, 256);
            this.chkOnForRetryBySwitch.Name = "chkOnForRetryBySwitch";
            this.chkOnForRetryBySwitch.Size = new System.Drawing.Size(136, 17);
            this.chkOnForRetryBySwitch.TabIndex = 2;
            this.chkOnForRetryBySwitch.Text = "On For Retry By Switch";
            this.chkOnForRetryBySwitch.UseVisualStyleBackColor = true;
            // 
            // chkPUDContactor
            // 
            this.chkPUDContactor.AutoSize = true;
            this.chkPUDContactor.Enabled = false;
            this.chkPUDContactor.Location = new System.Drawing.Point(6, 95);
            this.chkPUDContactor.Name = "chkPUDContactor";
            this.chkPUDContactor.Size = new System.Drawing.Size(98, 17);
            this.chkPUDContactor.TabIndex = 4;
            this.chkPUDContactor.Text = "PUD Contactor";
            this.chkPUDContactor.UseVisualStyleBackColor = true;
            this.chkPUDContactor.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged_1);
            // 
            // chkContactorState
            // 
            this.chkContactorState.AutoSize = true;
            this.chkContactorState.Enabled = false;
            this.chkContactorState.Location = new System.Drawing.Point(6, 164);
            this.chkContactorState.Name = "chkContactorState";
            this.chkContactorState.Size = new System.Drawing.Size(165, 17);
            this.chkContactorState.TabIndex = 2;
            this.chkContactorState.Text = "Contactor State is Connected";
            this.chkContactorState.UseVisualStyleBackColor = true;
            // 
            // chkContactorEventIndex
            // 
            this.chkContactorEventIndex.AutoSize = true;
            this.chkContactorEventIndex.Enabled = false;
            this.chkContactorEventIndex.Location = new System.Drawing.Point(6, 210);
            this.chkContactorEventIndex.Name = "chkContactorEventIndex";
            this.chkContactorEventIndex.Size = new System.Drawing.Size(126, 17);
            this.chkContactorEventIndex.TabIndex = 0;
            this.chkContactorEventIndex.Text = "ContactorEventIndex";
            this.chkContactorEventIndex.UseVisualStyleBackColor = true;
            // 
            // chkContactorToOn
            // 
            this.chkContactorToOn.AutoSize = true;
            this.chkContactorToOn.Enabled = false;
            this.chkContactorToOn.Location = new System.Drawing.Point(6, 49);
            this.chkContactorToOn.Name = "chkContactorToOn";
            this.chkContactorToOn.Size = new System.Drawing.Size(107, 17);
            this.chkContactorToOn.TabIndex = 3;
            this.chkContactorToOn.Text = "Contactor To ON";
            this.chkContactorToOn.UseVisualStyleBackColor = true;
            // 
            // chkMakeCOntactorEvent
            // 
            this.chkMakeCOntactorEvent.AutoSize = true;
            this.chkMakeCOntactorEvent.Enabled = false;
            this.chkMakeCOntactorEvent.Location = new System.Drawing.Point(6, 118);
            this.chkMakeCOntactorEvent.Name = "chkMakeCOntactorEvent";
            this.chkMakeCOntactorEvent.Size = new System.Drawing.Size(133, 17);
            this.chkMakeCOntactorEvent.TabIndex = 0;
            this.chkMakeCOntactorEvent.Text = "Make Contactor Event";
            this.chkMakeCOntactorEvent.UseVisualStyleBackColor = true;
            // 
            // chkDelayBWContactorState
            // 
            this.chkDelayBWContactorState.AutoSize = true;
            this.chkDelayBWContactorState.Enabled = false;
            this.chkDelayBWContactorState.Location = new System.Drawing.Point(6, 71);
            this.chkDelayBWContactorState.Name = "chkDelayBWContactorState";
            this.chkDelayBWContactorState.Size = new System.Drawing.Size(151, 17);
            this.chkDelayBWContactorState.TabIndex = 2;
            this.chkDelayBWContactorState.Text = "Delay BW Contactor State";
            this.chkDelayBWContactorState.UseVisualStyleBackColor = true;
            // 
            // chkMakePulseContactor
            // 
            this.chkMakePulseContactor.AutoSize = true;
            this.chkMakePulseContactor.Enabled = false;
            this.chkMakePulseContactor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chkMakePulseContactor.Location = new System.Drawing.Point(6, 26);
            this.chkMakePulseContactor.Name = "chkMakePulseContactor";
            this.chkMakePulseContactor.Size = new System.Drawing.Size(129, 17);
            this.chkMakePulseContactor.TabIndex = 0;
            this.chkMakePulseContactor.Text = "Make Pulse Contactor";
            this.chkMakePulseContactor.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.lblSkippedScheduleIndex);
            this.groupBox8.Controls.Add(this.lblScheduleIndex);
            this.groupBox8.Controls.Add(this.label81);
            this.groupBox8.Controls.Add(this.label82);
            this.groupBox8.Controls.Add(this.lblWaitOnTariffChange);
            this.groupBox8.Controls.Add(this.lblFailureStateTimer);
            this.groupBox8.Controls.Add(this.lblConnectThroughSwitch);
            this.groupBox8.Controls.Add(this.lblTimerX);
            this.groupBox8.Controls.Add(this.lblTimerX2);
            this.groupBox8.Controls.Add(this.lblTariffIndex);
            this.groupBox8.Controls.Add(this.lblRetryCounter);
            this.groupBox8.Controls.Add(this.laber100);
            this.groupBox8.Controls.Add(this.label73);
            this.groupBox8.Controls.Add(this.label68);
            this.groupBox8.Controls.Add(this.label71);
            this.groupBox8.Controls.Add(this.label69);
            this.groupBox8.Controls.Add(this.label66);
            this.groupBox8.Controls.Add(this.label65);
            this.groupBox8.Controls.Add(this.lblStateShouldBe);
            this.groupBox8.Controls.Add(this.label64);
            this.groupBox8.Controls.Add(this.label67);
            this.groupBox8.Controls.Add(this.lblState);
            this.groupBox8.Location = new System.Drawing.Point(535, 195);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(497, 242);
            this.groupBox8.TabIndex = 23;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Contactor Controls";
            // 
            // lblSkippedScheduleIndex
            // 
            this.lblSkippedScheduleIndex.AutoSize = true;
            this.lblSkippedScheduleIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSkippedScheduleIndex.Location = new System.Drawing.Point(424, 156);
            this.lblSkippedScheduleIndex.Name = "lblSkippedScheduleIndex";
            this.lblSkippedScheduleIndex.Size = new System.Drawing.Size(37, 13);
            this.lblSkippedScheduleIndex.TabIndex = 21;
            this.lblSkippedScheduleIndex.Text = "[ 00 ]";
            // 
            // lblScheduleIndex
            // 
            this.lblScheduleIndex.AutoSize = true;
            this.lblScheduleIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScheduleIndex.Location = new System.Drawing.Point(425, 124);
            this.lblScheduleIndex.Name = "lblScheduleIndex";
            this.lblScheduleIndex.Size = new System.Drawing.Size(37, 13);
            this.lblScheduleIndex.TabIndex = 22;
            this.lblScheduleIndex.Text = "[ 00 ]";
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label81.Location = new System.Drawing.Point(265, 156);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(153, 13);
            this.label81.TabIndex = 23;
            this.label81.Text = "Skipped Schedule Index :";
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label82.Location = new System.Drawing.Point(315, 124);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(103, 13);
            this.label82.TabIndex = 24;
            this.label82.Text = "Schedule Index :";
            // 
            // lblWaitOnTariffChange
            // 
            this.lblWaitOnTariffChange.AutoSize = true;
            this.lblWaitOnTariffChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWaitOnTariffChange.Location = new System.Drawing.Point(194, 186);
            this.lblWaitOnTariffChange.Name = "lblWaitOnTariffChange";
            this.lblWaitOnTariffChange.Size = new System.Drawing.Size(37, 13);
            this.lblWaitOnTariffChange.TabIndex = 18;
            this.lblWaitOnTariffChange.Text = "[ 00 ]";
            // 
            // lblFailureStateTimer
            // 
            this.lblFailureStateTimer.AutoSize = true;
            this.lblFailureStateTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFailureStateTimer.Location = new System.Drawing.Point(194, 156);
            this.lblFailureStateTimer.Name = "lblFailureStateTimer";
            this.lblFailureStateTimer.Size = new System.Drawing.Size(37, 13);
            this.lblFailureStateTimer.TabIndex = 18;
            this.lblFailureStateTimer.Text = "[ 00 ]";
            // 
            // lblConnectThroughSwitch
            // 
            this.lblConnectThroughSwitch.AutoSize = true;
            this.lblConnectThroughSwitch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnectThroughSwitch.Location = new System.Drawing.Point(194, 124);
            this.lblConnectThroughSwitch.Name = "lblConnectThroughSwitch";
            this.lblConnectThroughSwitch.Size = new System.Drawing.Size(37, 13);
            this.lblConnectThroughSwitch.TabIndex = 18;
            this.lblConnectThroughSwitch.Text = "[ 00 ]";
            // 
            // lblTimerX
            // 
            this.lblTimerX.AutoSize = true;
            this.lblTimerX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimerX.Location = new System.Drawing.Point(424, 93);
            this.lblTimerX.Name = "lblTimerX";
            this.lblTimerX.Size = new System.Drawing.Size(37, 13);
            this.lblTimerX.TabIndex = 18;
            this.lblTimerX.Text = "[ 00 ]";
            // 
            // lblTimerX2
            // 
            this.lblTimerX2.AutoSize = true;
            this.lblTimerX2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimerX2.Location = new System.Drawing.Point(424, 64);
            this.lblTimerX2.Name = "lblTimerX2";
            this.lblTimerX2.Size = new System.Drawing.Size(37, 13);
            this.lblTimerX2.TabIndex = 18;
            this.lblTimerX2.Text = "[ 00 ]";
            // 
            // lblTariffIndex
            // 
            this.lblTariffIndex.AutoSize = true;
            this.lblTariffIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTariffIndex.Location = new System.Drawing.Point(194, 93);
            this.lblTariffIndex.Name = "lblTariffIndex";
            this.lblTariffIndex.Size = new System.Drawing.Size(37, 13);
            this.lblTariffIndex.TabIndex = 18;
            this.lblTariffIndex.Text = "[ 00 ]";
            // 
            // lblRetryCounter
            // 
            this.lblRetryCounter.AutoSize = true;
            this.lblRetryCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRetryCounter.Location = new System.Drawing.Point(194, 64);
            this.lblRetryCounter.Name = "lblRetryCounter";
            this.lblRetryCounter.Size = new System.Drawing.Size(37, 13);
            this.lblRetryCounter.TabIndex = 18;
            this.lblRetryCounter.Text = "[ 00 ]";
            // 
            // laber100
            // 
            this.laber100.AutoSize = true;
            this.laber100.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.laber100.Location = new System.Drawing.Point(33, 186);
            this.laber100.Name = "laber100";
            this.laber100.Size = new System.Drawing.Size(142, 13);
            this.laber100.TabIndex = 18;
            this.laber100.Text = "Wait On Tariff Change :";
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label73.Location = new System.Drawing.Point(53, 156);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(122, 13);
            this.label73.TabIndex = 18;
            this.label73.Text = "Failure State Timer :";
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label68.Location = new System.Drawing.Point(20, 124);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(155, 13);
            this.label68.TabIndex = 18;
            this.label68.Text = "Connect Through Switch :";
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label71.Location = new System.Drawing.Point(364, 93);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(54, 13);
            this.label71.TabIndex = 18;
            this.label71.Text = "TimerX :";
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label69.Location = new System.Drawing.Point(357, 64);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(61, 13);
            this.label69.TabIndex = 18;
            this.label69.Text = "TimerX2 :";
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label66.Location = new System.Drawing.Point(67, 93);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(108, 13);
            this.label66.TabIndex = 18;
            this.label66.Text = "This Tariff Index :";
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label65.Location = new System.Drawing.Point(78, 64);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(97, 13);
            this.label65.TabIndex = 18;
            this.label65.Text = "Retry Counter : ";
            // 
            // lblStateShouldBe
            // 
            this.lblStateShouldBe.AutoSize = true;
            this.lblStateShouldBe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStateShouldBe.Location = new System.Drawing.Point(194, 40);
            this.lblStateShouldBe.Name = "lblStateShouldBe";
            this.lblStateShouldBe.Size = new System.Drawing.Size(52, 15);
            this.lblStateShouldBe.TabIndex = 20;
            this.lblStateShouldBe.Text = "State : ";
            this.lblStateShouldBe.Click += new System.EventHandler(this.label64_Click);
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label64.Location = new System.Drawing.Point(123, 10);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(52, 15);
            this.label64.TabIndex = 20;
            this.label64.Text = "State : ";
            this.label64.Click += new System.EventHandler(this.label64_Click);
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label67.Location = new System.Drawing.Point(53, 40);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(122, 15);
            this.label67.TabIndex = 20;
            this.label67.Text = "State Should Be : ";
            this.label67.Click += new System.EventHandler(this.label64_Click);
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblState.Location = new System.Drawing.Point(194, 10);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(52, 15);
            this.lblState.TabIndex = 20;
            this.lblState.Text = "State : ";
            this.lblState.Click += new System.EventHandler(this.label64_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label46);
            this.groupBox6.Controls.Add(this.check_onRemoteCommand);
            this.groupBox6.Controls.Add(this.lblTariffIndex_Status);
            this.groupBox6.Controls.Add(this.check_onIRDAcommand);
            this.groupBox6.Controls.Add(this.lblRetryCount);
            this.groupBox6.Controls.Add(this.check_OnTariffChange);
            this.groupBox6.Controls.Add(this.label53);
            this.groupBox6.Controls.Add(this.check_Contactor);
            this.groupBox6.Controls.Add(this.check_onBySwitch);
            this.groupBox6.Controls.Add(this.check_overCurrent);
            this.groupBox6.Controls.Add(this.check_offbyRemoteCommand);
            this.groupBox6.Controls.Add(this.check_Overload);
            this.groupBox6.Controls.Add(this.check_onBySwitchwithRemote);
            this.groupBox6.Controls.Add(this.check_offByIRDAcommand);
            this.groupBox6.Controls.Add(this.check_MDIExceed);
            this.groupBox6.Controls.Add(this.check_underVolt);
            this.groupBox6.Controls.Add(this.chkFailureStateDetected_State);
            this.groupBox6.Controls.Add(this.checkBox7);
            this.groupBox6.Controls.Add(this.check_overVolt);
            this.groupBox6.Controls.Add(this.chkRecoverFromPowerDown);
            this.groupBox6.Controls.Add(this.check_offByRetryExpire);
            this.groupBox6.Controls.Add(this.chkDisabled);
            this.groupBox6.Location = new System.Drawing.Point(217, 49);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(312, 388);
            this.groupBox6.TabIndex = 22;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Contactor Control Status";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.Location = new System.Drawing.Point(28, 23);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(82, 13);
            this.label46.TabIndex = 17;
            this.label46.Text = "Retry Count :";
            // 
            // check_onRemoteCommand
            // 
            this.check_onRemoteCommand.AutoSize = true;
            this.check_onRemoteCommand.Enabled = false;
            this.check_onRemoteCommand.Location = new System.Drawing.Point(138, 83);
            this.check_onRemoteCommand.Name = "check_onRemoteCommand";
            this.check_onRemoteCommand.Size = new System.Drawing.Size(138, 17);
            this.check_onRemoteCommand.TabIndex = 14;
            this.check_onRemoteCommand.Text = "ON (Remote Command)";
            this.check_onRemoteCommand.UseVisualStyleBackColor = true;
            // 
            // lblTariffIndex_Status
            // 
            this.lblTariffIndex_Status.AutoSize = true;
            this.lblTariffIndex_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTariffIndex_Status.Location = new System.Drawing.Point(248, 23);
            this.lblTariffIndex_Status.Name = "lblTariffIndex_Status";
            this.lblTariffIndex_Status.Size = new System.Drawing.Size(37, 13);
            this.lblTariffIndex_Status.TabIndex = 18;
            this.lblTariffIndex_Status.Text = "[ 00 ]";
            // 
            // check_onIRDAcommand
            // 
            this.check_onIRDAcommand.AutoSize = true;
            this.check_onIRDAcommand.Enabled = false;
            this.check_onIRDAcommand.Location = new System.Drawing.Point(138, 60);
            this.check_onIRDAcommand.Name = "check_onIRDAcommand";
            this.check_onIRDAcommand.Size = new System.Drawing.Size(130, 17);
            this.check_onIRDAcommand.TabIndex = 14;
            this.check_onIRDAcommand.Text = "ON ( IRDA Command)";
            this.check_onIRDAcommand.UseVisualStyleBackColor = true;
            // 
            // lblRetryCount
            // 
            this.lblRetryCount.AutoSize = true;
            this.lblRetryCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRetryCount.Location = new System.Drawing.Point(114, 23);
            this.lblRetryCount.Name = "lblRetryCount";
            this.lblRetryCount.Size = new System.Drawing.Size(37, 13);
            this.lblRetryCount.TabIndex = 18;
            this.lblRetryCount.Text = "[ 00 ]";
            // 
            // check_OnTariffChange
            // 
            this.check_OnTariffChange.AutoSize = true;
            this.check_OnTariffChange.Enabled = false;
            this.check_OnTariffChange.Location = new System.Drawing.Point(6, 304);
            this.check_OnTariffChange.Name = "check_OnTariffChange";
            this.check_OnTariffChange.Size = new System.Drawing.Size(115, 17);
            this.check_OnTariffChange.TabIndex = 15;
            this.check_OnTariffChange.Text = "ON( Tariff Change)";
            this.check_OnTariffChange.UseVisualStyleBackColor = true;
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.Location = new System.Drawing.Point(164, 23);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(80, 13);
            this.label53.TabIndex = 17;
            this.label53.Text = "Tariff Index :";
            // 
            // check_Contactor
            // 
            this.check_Contactor.AutoSize = true;
            this.check_Contactor.Enabled = false;
            this.check_Contactor.Location = new System.Drawing.Point(6, 51);
            this.check_Contactor.Name = "check_Contactor";
            this.check_Contactor.Size = new System.Drawing.Size(72, 17);
            this.check_Contactor.TabIndex = 0;
            this.check_Contactor.Text = "Contactor";
            this.check_Contactor.UseVisualStyleBackColor = true;
            // 
            // check_onBySwitch
            // 
            this.check_onBySwitch.AutoSize = true;
            this.check_onBySwitch.Enabled = false;
            this.check_onBySwitch.Location = new System.Drawing.Point(6, 281);
            this.check_onBySwitch.Name = "check_onBySwitch";
            this.check_onBySwitch.Size = new System.Drawing.Size(89, 17);
            this.check_onBySwitch.TabIndex = 13;
            this.check_onBySwitch.Text = "On by Switch";
            this.check_onBySwitch.UseVisualStyleBackColor = true;
            // 
            // check_overCurrent
            // 
            this.check_overCurrent.AutoSize = true;
            this.check_overCurrent.Enabled = false;
            this.check_overCurrent.Location = new System.Drawing.Point(6, 97);
            this.check_overCurrent.Name = "check_overCurrent";
            this.check_overCurrent.Size = new System.Drawing.Size(86, 17);
            this.check_overCurrent.TabIndex = 2;
            this.check_overCurrent.Text = "Over Current";
            this.check_overCurrent.UseVisualStyleBackColor = true;
            // 
            // check_offbyRemoteCommand
            // 
            this.check_offbyRemoteCommand.AutoSize = true;
            this.check_offbyRemoteCommand.Enabled = false;
            this.check_offbyRemoteCommand.Location = new System.Drawing.Point(6, 235);
            this.check_offbyRemoteCommand.Name = "check_offbyRemoteCommand";
            this.check_offbyRemoteCommand.Size = new System.Drawing.Size(138, 17);
            this.check_offbyRemoteCommand.TabIndex = 12;
            this.check_offbyRemoteCommand.Text = "Off by remote command";
            this.check_offbyRemoteCommand.UseVisualStyleBackColor = true;
            // 
            // check_Overload
            // 
            this.check_Overload.AutoSize = true;
            this.check_Overload.Enabled = false;
            this.check_Overload.Location = new System.Drawing.Point(6, 74);
            this.check_Overload.Name = "check_Overload";
            this.check_Overload.Size = new System.Drawing.Size(76, 17);
            this.check_Overload.TabIndex = 3;
            this.check_Overload.Text = "Over Load";
            this.check_Overload.UseVisualStyleBackColor = true;
            // 
            // check_onBySwitchwithRemote
            // 
            this.check_onBySwitchwithRemote.AutoSize = true;
            this.check_onBySwitchwithRemote.Enabled = false;
            this.check_onBySwitchwithRemote.Location = new System.Drawing.Point(6, 258);
            this.check_onBySwitchwithRemote.Name = "check_onBySwitchwithRemote";
            this.check_onBySwitchwithRemote.Size = new System.Drawing.Size(154, 17);
            this.check_onBySwitchwithRemote.TabIndex = 11;
            this.check_onBySwitchwithRemote.Text = "On by Switch With Remote";
            this.check_onBySwitchwithRemote.UseVisualStyleBackColor = true;
            // 
            // check_offByIRDAcommand
            // 
            this.check_offByIRDAcommand.AutoSize = true;
            this.check_offByIRDAcommand.Enabled = false;
            this.check_offByIRDAcommand.Location = new System.Drawing.Point(6, 212);
            this.check_offByIRDAcommand.Name = "check_offByIRDAcommand";
            this.check_offByIRDAcommand.Size = new System.Drawing.Size(132, 17);
            this.check_offByIRDAcommand.TabIndex = 10;
            this.check_offByIRDAcommand.Text = "Off by IRDA command";
            this.check_offByIRDAcommand.UseVisualStyleBackColor = true;
            // 
            // check_MDIExceed
            // 
            this.check_MDIExceed.AutoSize = true;
            this.check_MDIExceed.Enabled = false;
            this.check_MDIExceed.Location = new System.Drawing.Point(6, 120);
            this.check_MDIExceed.Name = "check_MDIExceed";
            this.check_MDIExceed.Size = new System.Drawing.Size(85, 17);
            this.check_MDIExceed.TabIndex = 4;
            this.check_MDIExceed.Text = "MDI Exceed";
            this.check_MDIExceed.UseVisualStyleBackColor = true;
            // 
            // check_underVolt
            // 
            this.check_underVolt.AutoSize = true;
            this.check_underVolt.Enabled = false;
            this.check_underVolt.Location = new System.Drawing.Point(6, 166);
            this.check_underVolt.Name = "check_underVolt";
            this.check_underVolt.Size = new System.Drawing.Size(76, 17);
            this.check_underVolt.TabIndex = 5;
            this.check_underVolt.Text = "Under Volt";
            this.check_underVolt.UseVisualStyleBackColor = true;
            // 
            // chkFailureStateDetected_State
            // 
            this.chkFailureStateDetected_State.AutoSize = true;
            this.chkFailureStateDetected_State.Enabled = false;
            this.chkFailureStateDetected_State.Location = new System.Drawing.Point(138, 129);
            this.chkFailureStateDetected_State.Name = "chkFailureStateDetected_State";
            this.chkFailureStateDetected_State.Size = new System.Drawing.Size(126, 17);
            this.chkFailureStateDetected_State.TabIndex = 9;
            this.chkFailureStateDetected_State.Text = "FailureStateDetected";
            this.chkFailureStateDetected_State.UseVisualStyleBackColor = true;
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Enabled = false;
            this.checkBox7.Location = new System.Drawing.Point(138, 175);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(48, 17);
            this.checkBox7.TabIndex = 9;
            this.checkBox7.Text = "RFU";
            this.checkBox7.UseVisualStyleBackColor = true;
            // 
            // check_overVolt
            // 
            this.check_overVolt.AutoSize = true;
            this.check_overVolt.Enabled = false;
            this.check_overVolt.Location = new System.Drawing.Point(6, 143);
            this.check_overVolt.Name = "check_overVolt";
            this.check_overVolt.Size = new System.Drawing.Size(70, 17);
            this.check_overVolt.TabIndex = 6;
            this.check_overVolt.Text = "Over Volt";
            this.check_overVolt.UseVisualStyleBackColor = true;
            // 
            // chkRecoverFromPowerDown
            // 
            this.chkRecoverFromPowerDown.AutoSize = true;
            this.chkRecoverFromPowerDown.Enabled = false;
            this.chkRecoverFromPowerDown.Location = new System.Drawing.Point(138, 106);
            this.chkRecoverFromPowerDown.Name = "chkRecoverFromPowerDown";
            this.chkRecoverFromPowerDown.Size = new System.Drawing.Size(143, 17);
            this.chkRecoverFromPowerDown.TabIndex = 7;
            this.chkRecoverFromPowerDown.Text = "Recovered Power Down";
            this.chkRecoverFromPowerDown.UseVisualStyleBackColor = true;
            // 
            // check_offByRetryExpire
            // 
            this.check_offByRetryExpire.AutoSize = true;
            this.check_offByRetryExpire.Enabled = false;
            this.check_offByRetryExpire.Location = new System.Drawing.Point(6, 189);
            this.check_offByRetryExpire.Name = "check_offByRetryExpire";
            this.check_offByRetryExpire.Size = new System.Drawing.Size(109, 17);
            this.check_offByRetryExpire.TabIndex = 8;
            this.check_offByRetryExpire.Text = "Off by retry Expire";
            this.check_offByRetryExpire.UseVisualStyleBackColor = true;
            // 
            // chkDisabled
            // 
            this.chkDisabled.AutoSize = true;
            this.chkDisabled.Enabled = false;
            this.chkDisabled.Location = new System.Drawing.Point(138, 152);
            this.chkDisabled.Name = "chkDisabled";
            this.chkDisabled.Size = new System.Drawing.Size(67, 17);
            this.chkDisabled.TabIndex = 7;
            this.chkDisabled.Text = "Disabled";
            this.chkDisabled.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.chkOverLoadByPhaseTrig);
            this.groupBox7.Controls.Add(this.chkFailureStateDetectedTrig);
            this.groupBox7.Controls.Add(this.chkOverLoadByTotalTrig);
            this.groupBox7.Enabled = false;
            this.groupBox7.Location = new System.Drawing.Point(723, 49);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(190, 140);
            this.groupBox7.TabIndex = 21;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Contactor Trig Flags";
            // 
            // chkOverLoadByPhaseTrig
            // 
            this.chkOverLoadByPhaseTrig.AutoSize = true;
            this.chkOverLoadByPhaseTrig.Enabled = false;
            this.chkOverLoadByPhaseTrig.Location = new System.Drawing.Point(28, 42);
            this.chkOverLoadByPhaseTrig.Name = "chkOverLoadByPhaseTrig";
            this.chkOverLoadByPhaseTrig.Size = new System.Drawing.Size(124, 17);
            this.chkOverLoadByPhaseTrig.TabIndex = 0;
            this.chkOverLoadByPhaseTrig.Text = "Over Load By Phase";
            this.chkOverLoadByPhaseTrig.UseVisualStyleBackColor = true;
            // 
            // chkFailureStateDetectedTrig
            // 
            this.chkFailureStateDetectedTrig.AutoSize = true;
            this.chkFailureStateDetectedTrig.Enabled = false;
            this.chkFailureStateDetectedTrig.Location = new System.Drawing.Point(28, 88);
            this.chkFailureStateDetectedTrig.Name = "chkFailureStateDetectedTrig";
            this.chkFailureStateDetectedTrig.Size = new System.Drawing.Size(132, 17);
            this.chkFailureStateDetectedTrig.TabIndex = 2;
            this.chkFailureStateDetectedTrig.Text = "Failure State Detected";
            this.chkFailureStateDetectedTrig.UseVisualStyleBackColor = true;
            // 
            // chkOverLoadByTotalTrig
            // 
            this.chkOverLoadByTotalTrig.AutoSize = true;
            this.chkOverLoadByTotalTrig.Enabled = false;
            this.chkOverLoadByTotalTrig.Location = new System.Drawing.Point(28, 65);
            this.chkOverLoadByTotalTrig.Name = "chkOverLoadByTotalTrig";
            this.chkOverLoadByTotalTrig.Size = new System.Drawing.Size(118, 17);
            this.chkOverLoadByTotalTrig.TabIndex = 3;
            this.chkOverLoadByTotalTrig.Text = "Over Load By Total";
            this.chkOverLoadByTotalTrig.UseVisualStyleBackColor = true;
            this.chkOverLoadByTotalTrig.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.chkOverLoadByPhase);
            this.groupBox5.Controls.Add(this.chkFailureStateDetected);
            this.groupBox5.Controls.Add(this.chkOverLoadByTotal);
            this.groupBox5.Enabled = false;
            this.groupBox5.Location = new System.Drawing.Point(535, 49);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(182, 140);
            this.groupBox5.TabIndex = 21;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Contactor Flags";
            // 
            // chkOverLoadByPhase
            // 
            this.chkOverLoadByPhase.AutoSize = true;
            this.chkOverLoadByPhase.Enabled = false;
            this.chkOverLoadByPhase.Location = new System.Drawing.Point(23, 42);
            this.chkOverLoadByPhase.Name = "chkOverLoadByPhase";
            this.chkOverLoadByPhase.Size = new System.Drawing.Size(124, 17);
            this.chkOverLoadByPhase.TabIndex = 0;
            this.chkOverLoadByPhase.Text = "Over Load By Phase";
            this.chkOverLoadByPhase.UseVisualStyleBackColor = true;
            // 
            // chkFailureStateDetected
            // 
            this.chkFailureStateDetected.AutoSize = true;
            this.chkFailureStateDetected.Enabled = false;
            this.chkFailureStateDetected.Location = new System.Drawing.Point(23, 88);
            this.chkFailureStateDetected.Name = "chkFailureStateDetected";
            this.chkFailureStateDetected.Size = new System.Drawing.Size(132, 17);
            this.chkFailureStateDetected.TabIndex = 2;
            this.chkFailureStateDetected.Text = "Failure State Detected";
            this.chkFailureStateDetected.UseVisualStyleBackColor = true;
            // 
            // chkOverLoadByTotal
            // 
            this.chkOverLoadByTotal.AutoSize = true;
            this.chkOverLoadByTotal.Enabled = false;
            this.chkOverLoadByTotal.Location = new System.Drawing.Point(23, 65);
            this.chkOverLoadByTotal.Name = "chkOverLoadByTotal";
            this.chkOverLoadByTotal.Size = new System.Drawing.Size(118, 17);
            this.chkOverLoadByTotal.TabIndex = 3;
            this.chkOverLoadByTotal.Text = "Over Load By Total";
            this.chkOverLoadByTotal.UseVisualStyleBackColor = true;
            this.chkOverLoadByTotal.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // btn_GetContactorFlags
            // 
            this.btn_GetContactorFlags.Location = new System.Drawing.Point(31, 6);
            this.btn_GetContactorFlags.Name = "btn_GetContactorFlags";
            this.btn_GetContactorFlags.Size = new System.Drawing.Size(199, 23);
            this.btn_GetContactorFlags.TabIndex = 18;
            this.btn_GetContactorFlags.Text = "GET Contactor Control Status";
            this.btn_GetContactorFlags.UseVisualStyleBackColor = true;
            this.btn_GetContactorFlags.Click += new System.EventHandler(this.btn_GetContactorFlags_Click);
            // 
            // btn_InsOldReport
            // 
            this.btn_InsOldReport.Location = new System.Drawing.Point(182, 5);
            this.btn_InsOldReport.Name = "btn_InsOldReport";
            this.btn_InsOldReport.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_InsOldReport.Size = new System.Drawing.Size(165, 30);
            this.btn_InsOldReport.TabIndex = 54;
            this.btn_InsOldReport.Values.Text = "Generate Report";
            this.btn_InsOldReport.Visible = false;
            this.btn_InsOldReport.Click += new System.EventHandler(this.btn_InsOldReport_Click);
            // 
            // btn_read_all_instantaneous_values
            // 
            this.btn_read_all_instantaneous_values.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_read_all_instantaneous_values.BackgroundImage")));
            this.btn_read_all_instantaneous_values.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_read_all_instantaneous_values.ForeColor = System.Drawing.Color.Transparent;
            this.btn_read_all_instantaneous_values.Location = new System.Drawing.Point(16, 5);
            this.btn_read_all_instantaneous_values.Name = "btn_read_all_instantaneous_values";
            this.btn_read_all_instantaneous_values.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_read_all_instantaneous_values.Size = new System.Drawing.Size(160, 30);
            this.btn_read_all_instantaneous_values.TabIndex = 53;
            this.btn_read_all_instantaneous_values.Values.Text = "Read Data";
            this.btn_read_all_instantaneous_values.Click += new System.EventHandler(this.btn_read_all_instantaneous_values_Click);
            // 
            // Instantaneous
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.btn_InsOldReport);
            this.Controls.Add(this.btn_read_all_instantaneous_values);
            this.Controls.Add(this.tcontrolMain);
            this.Controls.Add(this.lbl_PbStatus);
            this.Controls.Add(this.pb_ins);
            this.Name = "Instantaneous";
            this.Size = new System.Drawing.Size(1073, 539);
            this.Load += new System.EventHandler(this.Instantaneous_Load);
            this.tcontrolMain.ResumeLayout(false);
            this.INs.ResumeLayout(false);
            this.gpReadInst.ResumeLayout(false);
            this.gpReadInst.PerformLayout();
            this.gpPhase.ResumeLayout(false);
            this.gpPhase.PerformLayout();
            this.GpQuantity.ResumeLayout(false);
            this.GpQuantity.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid_Instanstanouse)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_misc)).EndInit();
            this.tab_MDI.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tpMDI.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid_CurrentMDI)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tbDebugMDI.ResumeLayout(false);
            this.gp_MDIs.ResumeLayout(false);
            this.gp_MDIs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_MDI)).EndInit();
            this.tpInstantaneousMDI.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.tab_NewIns.ResumeLayout(false);
            this.tab_NewIns.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_NEwIns)).EndInit();
            this.tab_Record.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.all_in_one.ResumeLayout(false);
            this.all_in_one.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_EventNames)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.pnl_TL_Load_Profile.ResumeLayout(false);
            this.pnl_TL_Load_Profile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_tl_load_profile)).EndInit();
            this.pnl_cb_day_record.ResumeLayout(false);
            this.pnl_cb_day_record.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_view_cb_day_record)).EndInit();
            this.AllInOnetab.ResumeLayout(false);
            this.AllInOnetab.PerformLayout();
            this.tab_Billing.ResumeLayout(false);
            this.Commulative.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grv_commulative_billing)).EndInit();
            this.Monthly.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grv_monthly_billing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grv_event_logs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grv_general_instentanious)).EndInit();
            this.tab_CrDwData.ResumeLayout(false);
            this.tab_CrDwData.PerformLayout();
            this.tab_Contactor.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // private  Button  btn_read_all_instantaneous_values;
        private System.Windows.Forms.ProgressBar pb_ins;
        //private  CheckBox check_AllPhy;
        private System.ComponentModel.BackgroundWorker bckWorker_Instantanouse;
        private System.Windows.Forms.Label lbl_PbStatus;
        private System.ComponentModel.BackgroundWorker bgw_NewINsRead;
        private System.ComponentModel.BackgroundWorker bgw_DisplayWindows;
        private TabControl tcontrolMain;
        private TabPage INs;
        private GroupBox gpReadInst;
        private CheckBox check_I_AddtoDB;
        private CheckBox check_PowerQuadrent;
        private GroupBox gpPhase;
        private RadioButton rdbPhC;
        private RadioButton rdbAllPhases;
        private RadioButton rdbPhA;
        private RadioButton rdbPhB;
        private GroupBox GpQuantity;
        private CheckBox check_ReactivePower;
        private CheckBox check_readMDI_Interval;
        private CheckBox check_Mdi;
        private CheckBox check_Apparent;
        private CheckBox check_misc;
        private CheckBox check_AllPhy;
        private CheckBox check_Voltage;
        private CheckBox check_Powerfactor;
        private CheckBox check_ActivePower;
        private CheckBox check_Current;
        private Button button1;
        private Button btnIReadInst;
        private GroupBox groupBox3;
        private  DataGridView grid_Instanstanouse;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private GroupBox groupBox2;
        private Label lbl_meter_datetime;
        private Label lbl_meter_serial;
        private Label label2;
        private Label label1;
        private  DataGridView grid_misc;
        private DataGridViewTextBoxColumn Value;
        private TabPage tab_MDI;
        private DataGridView grid_CurrentMDI;
        private GroupBox gp_MDIs;
        private Label lbl_MDI_SlideCounter;
        private Label label62;
        private Label lbl_MDI_Previous_Energy;
        private Label lbl_MDI_Previous_Power;
        private Label lbl_MDI_Running_Energy;
        private Label lbl_MDI_Running_Power;
        private Label lbl_MDI_TimeLeft;
        private Label lbl_MDI_SlideCount;
        private Label lbl_MDI_Counter;
        private Label lbl_MDI_time;
        private Label label54;
        private Label label50;
        private Label label48;
        private Label label52;
        private Label label58;
        private Label label56;
        private Label label49;
        private Label label44;
        private  DataGridView grid_MDI;
        private DataGridViewTextBoxColumn Slide1;
        private DataGridViewTextBoxColumn Slide2;
        private DataGridViewTextBoxColumn Slide3;
        private DataGridViewTextBoxColumn Slide4;
        private DataGridViewTextBoxColumn Slide5;
        private DataGridViewTextBoxColumn Slide6;
        private TabPage tab_NewIns;
        private ProgressBar pb_newIns;
        private Label lbl_MeterDate;
        private  Button btn_INS_Report;
        private Label lbl_Frequency;
        private Label label5;
        private  Button btn_NewIns;
        private Label bl;
        private  DataGridView grid_NEwIns;
        private TabPage tab_Record;
        private TabControl tabControl1;
        private TabPage all_in_one;
        private Label lbl_packetSize;
        private Label label14;
        private GroupBox groupBox4;
        private Label label13;
        private Label label11;
        private Label label12;
        private Label label10;
        private Label label7;
        private Label label9;
        private Label label6;
        private Label label8;
        private Label label4;
        private Label label16;
        private Label label15;
        private Label label3;
        private CheckBox check_PABS16;
        private CheckBox chk_CUMMDIP;
        private CheckBox chk_CumMDIQ;
        private CheckBox check_PABS15;
        private CheckBox chk_TL;
        private CheckBox check_Q16;
        private CheckBox check_P16;
        private CheckBox check_Q15;
        private CheckBox chk_T4;
        private CheckBox check_S16;
        private CheckBox check_P15;
        private CheckBox check_QAbs16;
        private CheckBox chk_T3;
        private CheckBox check_S15;
        private CheckBox check_T16;
        private CheckBox chk_T2;
        private CheckBox check_QAbs15;
        private CheckBox chk_MDIP16;
        private CheckBox chk_T1;
        private CheckBox check_T15;
        private CheckBox chk_MDIQ16;
        private CheckBox chk_MDIP15;
        private CheckBox chk_MDIQ15;
        private GroupBox groupBox1;
        private CheckBox chk_PT;
        private CheckBox chk_CT;
        private CheckBox chk_EventCount;
        private CheckBox chk_MDI_Reset;
        private CheckBox chk_S;
        private CheckBox chk_V;
        private CheckBox chk_MDITime;
        private CheckBox chk_Q;
        private CheckBox chk_AlarmSTS;
        private CheckBox chk_LPLog;
        private CheckBox chk_I;
        private CheckBox chk_MdiPre;
        private CheckBox check_PF;
        private CheckBox chk_LPCount;
        private CheckBox chk_P;
        private CheckBox chk_EventLog;
        private CheckBox chk_Tamper_power;
        private CheckBox chk_frq;
        private Button btn_GET_INS;
        private Button btn_SetAll;
        private Button btn_GET_ALL;
        private TabPage tabPage4;
        private Button button2;
        private Button btn_TLLoadProfile;
        private Button btn_CBDayRecord;
        private Button btn_MakeError;
        private Button btn_Set_ReadRawData;
        private TextBox txt_EPMNumber;
        private TextBox txt_RawDataLength;
        private TextBox txt_RawDataAddress;
        private Label label20;
        private Label label19;
        private Label label18;
        private Button btn_ReadRawData;
        private RichTextBox txt_general;
        private TabPage tab_CrDwData;
        private ProgressBar progressBar1;
        private Label label17;
        private RadioButton radio_test;
        private RadioButton radio_alt;
        private RadioButton radio_nor;
        private Button btn_get_Dwd;
        private RichTextBox rtb_1;
        private DataGridView grid_view_cb_day_record;
        private Button btn_next;
        private Button btn_previous;
        private Label label24;
        private Label lbl_total_records;
        private Label lbl_record_no;
        private Label label21;
        private Label lbl_record_counter;
        private Label label22;
        private Label lbl_this_date_time;
        private Label label27;
        private Label lbl_last_reset_date_time;
        private Label label25;
        private Panel pnl_cb_day_record;
        private Label label23;
        private Panel pnl_TL_Load_Profile;
        private Label label26;
        private DataGridView grid_tl_load_profile;
        private DataGridView grid_EventNames;
        private RichTextBox richTextBox1;
        private RadioButton rb_Counter;
        private RadioButton rb_check_all_log;
        private DataGridViewTextBoxColumn Event;
        private DataGridViewCheckBoxColumn Log;
        private DataGridViewCheckBoxColumn even_counter;
        private RadioButton rb_uuncheck_all;
        private TabPage AllInOnetab;
        private DataGridView grv_general_instentanious;
        private Label label28;
        private Label label35;
        private Label label36;
        private Label label33;
        private Label label34;
        private Label label30;
        private Label label31;
        private Label label29;
        private Label label37;
        private Label lbl_channel_4;
        private Label lbl_channel_2;
        private Label lbl_count;
        private Label lbl_channel_3;
        private Label lbl_channel_1;
        private Label lbl_time_id;
        private Label lbl_FRQ;
        private Label lbl_TP;
        private Label lbl_day_profile;
        private Label lbl_season_profile;
        private Label label55;
        private Label label57;
        private DataGridView grv_event_logs;
        private TabControl tab_Billing;
        private TabPage Commulative;
        private TabPage Monthly;
        private DataGridView grv_commulative_billing;
        private DataGridView grv_monthly_billing;
        private Label lblmdi_time;
        private Label lbl_timer;
        private Label label39;
        private Label timer;
        private Label lbl_mdi_pre_kvar;
        private Label lbl_mdi_pre_kw;
        private Label label40;
        private Label label41;
        private Label lbl_slide_count;
        private Label label38;
        private DataGridViewTextBoxColumn DATE;
        private DataGridViewTextBoxColumn COUNTER;
        private DataGridViewTextBoxColumn KWH_P;
        private DataGridViewTextBoxColumn KWH_N;
        private DataGridViewTextBoxColumn KVARH_Q1;
        private DataGridViewTextBoxColumn KVARH_Q2;
        private DataGridViewTextBoxColumn KVARH_Q3;
        private DataGridViewTextBoxColumn KVARH_Q4;
        private DataGridViewTextBoxColumn KVAH;
        private DataGridViewTextBoxColumn TAMPER;
        private DataGridViewTextBoxColumn MDI_KW;
        private DataGridViewTextBoxColumn MDI_KVAR;
        private DataGridViewTextBoxColumn PF;
        private DataGridViewTextBoxColumn CAPTURE_TIME;
        private DataGridViewTextBoxColumn DAY_MDI_KW;
        private DataGridViewTextBoxColumn DAY_MDI_KVAR;
        private DataGridViewTextBoxColumn Title;
        private DataGridViewTextBoxColumn T1;
        private DataGridViewTextBoxColumn T2;
        private DataGridViewTextBoxColumn T3;
        private DataGridViewTextBoxColumn T4;
        private DataGridViewTextBoxColumn TL;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private DataGridViewTextBoxColumn T_L;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn Detail;
        private DataGridViewTextBoxColumn InstentaniousTitle;
        private DataGridViewTextBoxColumn Phase1;
        private DataGridViewTextBoxColumn Phase2;
        private DataGridViewTextBoxColumn Phase3;
        private DataGridViewTextBoxColumn Avg_Total;
        private Label lbl_mdi_count;
        private Label label43;
        private Label lbl_mdi_end_date;
        private Label label32;
        private Label label42;
        private Label lbl_ct_nominator;
        private Label label45;
        private Label label47;
        private Label lbl_pt_denominator;
        private Label label59;
        private Label lbl_pt_nominator;
        private Label label61;
        private Label lbl_ct_denominator;
        private RichTextBox txt_alarm_status;
        private Label lbl_meter_date_time;
        private Label label51;
        private Label lbl_meterserial;
        private Label label60;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private CheckBox chk_all_monthly;
        private CheckBox chk_check_all_cummulative;
        private TabPage tab_Contactor;
        private CheckBox check_OnTariffChange;
        private CheckBox check_onIRDAcommand;
        private CheckBox check_onBySwitch;
        private CheckBox check_offbyRemoteCommand;
        private CheckBox check_onBySwitchwithRemote;
        private CheckBox check_offByIRDAcommand;
        private CheckBox checkBox7;
        private CheckBox check_offByRetryExpire;
        private CheckBox chkDisabled;
        private CheckBox check_overVolt;
        private CheckBox check_underVolt;
        private CheckBox check_MDIExceed;
        private CheckBox check_Overload;
        private CheckBox check_overCurrent;
        private CheckBox check_Contactor;
        private CheckBox check_onRemoteCommand;
        private Label label53;
        private Label label46;
        private Button btn_GetContactorFlags;
        private Label label64;
        private Label lblState;
        private GroupBox groupBox5;
        private CheckBox chkWaitOnTariffChange;
        private CheckBox chkOverLoadByPhase;
        private CheckBox chkFailureStateDetected;
        private CheckBox chkOverLoadByTotal;
        private GroupBox groupBox6;
        private GroupBox groupBox7;
        private CheckBox chkOverLoadByPhaseTrig;
        private CheckBox chkFailureStateDetectedTrig;
        private CheckBox chkOverLoadByTotalTrig;
        private GroupBox groupBox8;
        private Label lblTariffIndex;
        private Label lblRetryCounter;
        private Label label66;
        private Label label65;
        private Label lblStateShouldBe;
        private Label label67;
        private Label lblConnectThroughSwitch;
        private Label lblTimerX;
        private Label lblTimerX2;
        private Label label68;
        private Label label71;
        private Label label69;
        private Label lblFailureStateTimer;
        private Label label73;
        private Label lblWaitOnTariffChange;
        private Label laber100;
        private Label lblTariffIndex_Status;
        private Label lblRetryCount;
        private GroupBox groupBox9;
        private CheckBox chkRecoverFromPowerDown;
        private CheckBox chkFailureStateDetected_State;
        private CheckBox chkPUDContactor;
        private CheckBox chkContactorToOn;
        private CheckBox chkDelayBWContactorState;
        private CheckBox chkMakePulseContactor;
        private CheckBox chkContactorDisabled;
        private CheckBox chkIsCapCharged;
        private CheckBox chkOnForRetryBySwitch;
        private CheckBox chkContactorState;
        private CheckBox chkContactorEventIndex;
        private CheckBox chkMakeCOntactorEvent;
        private DataGridViewTextBoxColumn QuantityLabel;
        private DataGridViewTextBoxColumn T1_Val;
        private DataGridViewTextBoxColumn T2_Val;
        private DataGridViewTextBoxColumn T3_Val;
        private DataGridViewTextBoxColumn T4_Val;
        private DataGridViewTextBoxColumn TL_Val;
        private TabControl tabControl2;
        private TabPage tpMDI;
        private TabPage tbDebugMDI;
        private Panel panel2;
        private Panel panel1;
        private CheckedListBox chkLstMDIs;
        private TabPage tpInstantaneousMDI;
        private ListBox listInstantaneousMDI;
        public Label lblNumberOfPeriods;
        public Label lblPeriod;
        public Label label77;
        public Label lblCaptureTime;
        public Label label76;
        public Label lblStartCaptureTime;
        public Label label74;
        public Label lblStatus;
        public Label label75;
        public Label lblScalerUnit;
        public Label label72;
        public Label lblLastAverageValue;
        public Label label70;
        public Label lblCurrentAverageValue;
        public Label label63;
        public Label lbl_limit_over_volt;
        private Button lblReadMDI;
        private GroupBox groupBox10;
        private GroupBox groupBox11;
        public Label label78;
        private ComboBox cbxMditoMonitor;
        private Button btnSetMonitoredValue;
        private Button btnGetMditoMonitor;
        private CheckBox chk_All;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_InsOldReport;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_read_all_instantaneous_values;
        private CheckBox chkSkipSchedule;
        private Label lblSkippedScheduleIndex;
        private Label lblScheduleIndex;
        private Label label81;
        private Label label82;
    }
}
