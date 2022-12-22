using ComponentFactory.Krypton.Toolkit;
using System.Drawing;
namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    partial class ucActivityCalendar
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucActivityCalendar));
            this.Profiles = new System.Windows.Forms.TabControl();
            this.Calendar_Page = new System.Windows.Forms.TabPage();
            this.lbl_ErrorTOD = new System.Windows.Forms.Label();
            this.gp_Activate = new System.Windows.Forms.GroupBox();
            this.rdb_Disable = new System.Windows.Forms.RadioButton();
            this.rdb_Enable = new System.Windows.Forms.RadioButton();
            this.lbl_calendarName = new System.Windows.Forms.Label();
            this.txt_CalendarName = new System.Windows.Forms.TextBox();
            this.btn_WriteToDatabaseTariffication = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.gpActivationMode = new System.Windows.Forms.GroupBox();
            this.rdbActivateOnDate = new System.Windows.Forms.RadioButton();
            this.rdbInvokeAction = new System.Windows.Forms.RadioButton();
            this.dtc_CalendarActivationDate = new System.Windows.Forms.DateTimePicker();
            this.btn_GetActivityCalendar = new System.Windows.Forms.Button();
            this.btn_setActivityCalendar = new System.Windows.Forms.Button();
            this.Day_Profile = new System.Windows.Forms.TabPage();
            this.btnGetDayProfile = new System.Windows.Forms.Button();
            this.btnSetDayProfile = new System.Windows.Forms.Button();
            this.lbl_ErrorDayProfile = new System.Windows.Forms.Label();
            this.gpDayProfileSettings = new System.Windows.Forms.GroupBox();
            this.label76 = new System.Windows.Forms.Label();
            this.panel_numberofslots = new System.Windows.Forms.Panel();
            this.combo_tariff_NumberofSlots = new System.Windows.Forms.ComboBox();
            this.label71 = new System.Windows.Forms.Label();
            this.label77 = new System.Windows.Forms.Label();
            this.label73 = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.btn_Save_DayProfile = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.label79 = new System.Windows.Forms.Label();
            this.panel_dayslot6 = new System.Windows.Forms.Panel();
            this.txt_DayProfile_s6 = new System.Windows.Forms.DateTimePicker();
            this.panel15 = new System.Windows.Forms.Panel();
            this.radio_DayProfile_s6t2 = new System.Windows.Forms.RadioButton();
            this.radio_DayProfile_s6t1 = new System.Windows.Forms.RadioButton();
            this.radio_DayProfile_s6t3 = new System.Windows.Forms.RadioButton();
            this.radio_DayProfile_s6t4 = new System.Windows.Forms.RadioButton();
            this.lbl_Dp_Slot_s6 = new System.Windows.Forms.Label();
            this.panel_dayslot1 = new System.Windows.Forms.Panel();
            this.txt_DayProfile_s1 = new System.Windows.Forms.DateTimePicker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radio_DayProfile_s1t2 = new System.Windows.Forms.RadioButton();
            this.radio_DayProfile_s1t1 = new System.Windows.Forms.RadioButton();
            this.radio_DayProfile_s1t3 = new System.Windows.Forms.RadioButton();
            this.radio_DayProfile_s1t4 = new System.Windows.Forms.RadioButton();
            this.lbl_Dp_Slot_s1 = new System.Windows.Forms.Label();
            this.btn_AddDayProfile = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.panel_dayslot5 = new System.Windows.Forms.Panel();
            this.txt_DayProfile_s5 = new System.Windows.Forms.DateTimePicker();
            this.panel7 = new System.Windows.Forms.Panel();
            this.radio_DayProfile_s5t2 = new System.Windows.Forms.RadioButton();
            this.radio_DayProfile_s5t1 = new System.Windows.Forms.RadioButton();
            this.radio_DayProfile_s5t3 = new System.Windows.Forms.RadioButton();
            this.radio_DayProfile_s5t4 = new System.Windows.Forms.RadioButton();
            this.lbl_Dp_Slot_s5 = new System.Windows.Forms.Label();
            this.panel_dayslot2 = new System.Windows.Forms.Panel();
            this.txt_DayProfile_s2 = new System.Windows.Forms.DateTimePicker();
            this.panel3 = new System.Windows.Forms.Panel();
            this.radio_DayProfile_s2t2 = new System.Windows.Forms.RadioButton();
            this.radio_DayProfile_s2t1 = new System.Windows.Forms.RadioButton();
            this.radio_DayProfile_s2t3 = new System.Windows.Forms.RadioButton();
            this.radio_DayProfile_s2t4 = new System.Windows.Forms.RadioButton();
            this.lbl_Dp_Slot_s2 = new System.Windows.Forms.Label();
            this.panel_dayslot4 = new System.Windows.Forms.Panel();
            this.txt_DayProfile_s4 = new System.Windows.Forms.DateTimePicker();
            this.panel6 = new System.Windows.Forms.Panel();
            this.radio_DayProfile_s4t2 = new System.Windows.Forms.RadioButton();
            this.radio_DayProfile_s4t1 = new System.Windows.Forms.RadioButton();
            this.radio_DayProfile_s4t3 = new System.Windows.Forms.RadioButton();
            this.radio_DayProfile_s4t4 = new System.Windows.Forms.RadioButton();
            this.lbl_Dp_Slot_s4 = new System.Windows.Forms.Label();
            this.panel_dayslot3 = new System.Windows.Forms.Panel();
            this.txt_DayProfile_s3 = new System.Windows.Forms.DateTimePicker();
            this.panel5 = new System.Windows.Forms.Panel();
            this.radio_DayProfile_s3t2 = new System.Windows.Forms.RadioButton();
            this.radio_DayProfile_s3t1 = new System.Windows.Forms.RadioButton();
            this.radio_DayProfile_s3t3 = new System.Windows.Forms.RadioButton();
            this.radio_DayProfile_s3t4 = new System.Windows.Forms.RadioButton();
            this.lbl_Dp_Slot_s3 = new System.Windows.Forms.Label();
            this.btn_delete_DayProfile = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_newDayProfile = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.list_DayProfile = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.Week_Profile = new System.Windows.Forms.TabPage();
            this.btnGetWeekProfile = new System.Windows.Forms.Button();
            this.btnSetWeekProfile = new System.Windows.Forms.Button();
            this.lbl_ErrorWeekProfile = new System.Windows.Forms.Label();
            this.btn_Delete_WeekProfile = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.groupBox_WeekProfile = new System.Windows.Forms.GroupBox();
            this.combo_day_fri = new System.Windows.Forms.ComboBox();
            this.btn_Save_WeekProfile = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.combo_day_Mon = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.combo_day_wed = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.combo_day_tue = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.combo_day_thu = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.combo_day_sat = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.combo_day_sun = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.lbl_day = new System.Windows.Forms.Label();
            this.btn_weekProfile_ADD = new System.Windows.Forms.Button();
            this.btn_newWeekProfile = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.list_WeekProfile = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this._SeasonProfile = new System.Windows.Forms.TabPage();
            this.btnGetSeasonProfile = new System.Windows.Forms.Button();
            this.btnSetSeasonProfile = new System.Windows.Forms.Button();
            this.lbl_ErrorSeasonProfiles = new System.Windows.Forms.Label();
            this.list_SeasonProfile = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.groupBox_SeasonProfile = new System.Windows.Forms.GroupBox();
            this.lbl_Format = new System.Windows.Forms.Label();
            this.dtc_SeasonProfile_ = new ucDateTimeChooser.ucCustomDateTimePicker();
            this.combo_SeasonName = new System.Windows.Forms.ComboBox();
            this.label157 = new System.Windows.Forms.Label();
            this.label158 = new System.Windows.Forms.Label();
            this.label86 = new System.Windows.Forms.Label();
            this.combo_season_weekProfile1 = new System.Windows.Forms.ComboBox();
            this.btn_addSeasonProfile = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_Save_SeasonProfile = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_DeleteSeasonProfile = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.Special_Days = new System.Windows.Forms.TabPage();
            this.btnGetSpecialDays = new System.Windows.Forms.Button();
            this.btnSetSpecialDays = new System.Windows.Forms.Button();
            this.lbl_ErrorSpecialDays = new System.Windows.Forms.Label();
            this.specialDays = new System.Windows.Forms.GroupBox();
            this.ucSPDayProfileDateTime = new AccurateOptocomSoftware.ApplicationGUI.ucCustomControl.ucSPDayProfileDateTime();
            this.lbl_SpDay_StartDate = new System.Windows.Forms.Label();
            this.lbl_SpDay_DayProfileID = new System.Windows.Forms.Label();
            this.btn_SpecialDays_add = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.combo_SpecialDay_ProfileID = new System.Windows.Forms.ComboBox();
            this.btn_Save_SpecialDay = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.list_SpecialDays = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.btn_Delete_SpecialDay = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.Profiles.SuspendLayout();
            this.Calendar_Page.SuspendLayout();
            this.gp_Activate.SuspendLayout();
            this.gpActivationMode.SuspendLayout();
            this.Day_Profile.SuspendLayout();
            this.gpDayProfileSettings.SuspendLayout();
            this.panel_numberofslots.SuspendLayout();
            this.panel_dayslot6.SuspendLayout();
            this.panel15.SuspendLayout();
            this.panel_dayslot1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel_dayslot5.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel_dayslot2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel_dayslot4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel_dayslot3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.Week_Profile.SuspendLayout();
            this.groupBox_WeekProfile.SuspendLayout();
            this._SeasonProfile.SuspendLayout();
            this.groupBox_SeasonProfile.SuspendLayout();
            this.Special_Days.SuspendLayout();
            this.specialDays.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // Profiles
            // 
            this.Profiles.Controls.Add(this.Calendar_Page);
            this.Profiles.Controls.Add(this.Day_Profile);
            this.Profiles.Controls.Add(this.Week_Profile);
            this.Profiles.Controls.Add(this._SeasonProfile);
            this.Profiles.Controls.Add(this.Special_Days);
            this.Profiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Profiles.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.Profiles.Location = new System.Drawing.Point(0, 0);
            this.Profiles.Name = "Profiles";
            this.Profiles.Padding = new System.Drawing.Point(0, 0);
            this.Profiles.SelectedIndex = 0;
            this.Profiles.Size = new System.Drawing.Size(638, 418);
            this.Profiles.TabIndex = 35;
            this.Profiles.SelectedIndexChanged += new System.EventHandler(this.Profiles_SelectedIndexChanged);
            this.Profiles.Selected += new System.Windows.Forms.TabControlEventHandler(this.Profiles_Selected);
            // 
            // Calendar_Page
            // 
            this.Calendar_Page.BackColor = System.Drawing.Color.Transparent;
            this.Calendar_Page.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Calendar_Page.Controls.Add(this.lbl_ErrorTOD);
            this.Calendar_Page.Controls.Add(this.gp_Activate);
            this.Calendar_Page.Controls.Add(this.lbl_calendarName);
            this.Calendar_Page.Controls.Add(this.txt_CalendarName);
            this.Calendar_Page.Controls.Add(this.btn_WriteToDatabaseTariffication);
            this.Calendar_Page.Controls.Add(this.gpActivationMode);
            this.Calendar_Page.Controls.Add(this.btn_GetActivityCalendar);
            this.Calendar_Page.Controls.Add(this.btn_setActivityCalendar);
            this.Calendar_Page.ForeColor = System.Drawing.Color.Black;
            this.Calendar_Page.Location = new System.Drawing.Point(4, 24);
            this.Calendar_Page.Name = "Calendar_Page";
            this.Calendar_Page.Size = new System.Drawing.Size(630, 390);
            this.Calendar_Page.TabIndex = 4;
            this.Calendar_Page.Text = "Calendar";
            this.Calendar_Page.Enter += new System.EventHandler(this.CalendarPage_Enter);
            this.Calendar_Page.Leave += new System.EventHandler(this.Calendar_Page_Leave);
            // 
            // lbl_ErrorTOD
            // 
            this.lbl_ErrorTOD.AutoSize = true;
            this.lbl_ErrorTOD.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbl_ErrorTOD.ForeColor = System.Drawing.Color.DarkRed;
            this.lbl_ErrorTOD.Location = new System.Drawing.Point(69, 239);
            this.lbl_ErrorTOD.Name = "lbl_ErrorTOD";
            this.lbl_ErrorTOD.Size = new System.Drawing.Size(268, 15);
            this.lbl_ErrorTOD.TabIndex = 41;
            this.lbl_ErrorTOD.Text = "Error Validation,Invalid Param Tariff Of Day(TOD)";
            this.lbl_ErrorTOD.Visible = false;
            // 
            // gp_Activate
            // 
            this.gp_Activate.Controls.Add(this.rdb_Disable);
            this.gp_Activate.Controls.Add(this.rdb_Enable);
            this.gp_Activate.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.gp_Activate.ForeColor = System.Drawing.Color.Navy;
            this.gp_Activate.Location = new System.Drawing.Point(475, 69);
            this.gp_Activate.Name = "gp_Activate";
            this.gp_Activate.Size = new System.Drawing.Size(145, 45);
            this.gp_Activate.TabIndex = 32;
            this.gp_Activate.TabStop = false;
            this.gp_Activate.Text = "Prog_Enable_Disable";
            this.gp_Activate.Visible = false;
            // 
            // rdb_Disable
            // 
            this.rdb_Disable.AutoSize = true;
            this.rdb_Disable.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.rdb_Disable.ForeColor = System.Drawing.Color.Black;
            this.rdb_Disable.Location = new System.Drawing.Point(75, 18);
            this.rdb_Disable.Name = "rdb_Disable";
            this.rdb_Disable.Size = new System.Drawing.Size(64, 19);
            this.rdb_Disable.TabIndex = 1;
            this.rdb_Disable.Text = "Disable";
            this.rdb_Disable.UseVisualStyleBackColor = true;
            // 
            // rdb_Enable
            // 
            this.rdb_Enable.AutoSize = true;
            this.rdb_Enable.Checked = true;
            this.rdb_Enable.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.rdb_Enable.ForeColor = System.Drawing.Color.Black;
            this.rdb_Enable.Location = new System.Drawing.Point(8, 18);
            this.rdb_Enable.Name = "rdb_Enable";
            this.rdb_Enable.Size = new System.Drawing.Size(61, 19);
            this.rdb_Enable.TabIndex = 0;
            this.rdb_Enable.TabStop = true;
            this.rdb_Enable.Text = "Enable";
            this.rdb_Enable.UseVisualStyleBackColor = true;
            this.rdb_Enable.CheckedChanged += new System.EventHandler(this.rdb_Enable_CheckedChanged);
            // 
            // lbl_calendarName
            // 
            this.lbl_calendarName.AutoSize = true;
            this.lbl_calendarName.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbl_calendarName.ForeColor = System.Drawing.Color.Black;
            this.lbl_calendarName.Location = new System.Drawing.Point(69, 56);
            this.lbl_calendarName.Name = "lbl_calendarName";
            this.lbl_calendarName.Size = new System.Drawing.Size(91, 15);
            this.lbl_calendarName.TabIndex = 40;
            this.lbl_calendarName.Text = "Calendar Name";
            // 
            // txt_CalendarName
            // 
            this.txt_CalendarName.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.txt_CalendarName.Location = new System.Drawing.Point(185, 53);
            this.txt_CalendarName.Name = "txt_CalendarName";
            this.txt_CalendarName.Size = new System.Drawing.Size(163, 23);
            this.txt_CalendarName.TabIndex = 0;
            this.txt_CalendarName.Text = "MTI CALENDAR ";
            this.txt_CalendarName.TextChanged += new System.EventHandler(this.txt_CalendarName_TextChanged);
            // 
            // btn_WriteToDatabaseTariffication
            // 
            this.btn_WriteToDatabaseTariffication.Location = new System.Drawing.Point(416, 8);
            this.btn_WriteToDatabaseTariffication.Name = "btn_WriteToDatabaseTariffication";
            this.btn_WriteToDatabaseTariffication.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_WriteToDatabaseTariffication.Size = new System.Drawing.Size(204, 30);
            this.btn_WriteToDatabaseTariffication.TabIndex = 38;
            this.btn_WriteToDatabaseTariffication.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_WriteToDatabaseTariffication.Values.Image")));
            this.btn_WriteToDatabaseTariffication.Values.Text = "Add Tariffication to Database";
            this.btn_WriteToDatabaseTariffication.Click += new System.EventHandler(this.btn_WriteToDatabaseTariffication_Click);
            // 
            // gpActivationMode
            // 
            this.gpActivationMode.Controls.Add(this.rdbActivateOnDate);
            this.gpActivationMode.Controls.Add(this.rdbInvokeAction);
            this.gpActivationMode.Controls.Add(this.dtc_CalendarActivationDate);
            this.gpActivationMode.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.gpActivationMode.ForeColor = System.Drawing.Color.Maroon;
            this.gpActivationMode.Location = new System.Drawing.Point(72, 108);
            this.gpActivationMode.Name = "gpActivationMode";
            this.gpActivationMode.Size = new System.Drawing.Size(294, 128);
            this.gpActivationMode.TabIndex = 37;
            this.gpActivationMode.TabStop = false;
            this.gpActivationMode.Text = "Calendar Activation Mode";
            // 
            // rdbActivateOnDate
            // 
            this.rdbActivateOnDate.AutoSize = true;
            this.rdbActivateOnDate.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.rdbActivateOnDate.ForeColor = System.Drawing.Color.Black;
            this.rdbActivateOnDate.Location = new System.Drawing.Point(16, 60);
            this.rdbActivateOnDate.Name = "rdbActivateOnDate";
            this.rdbActivateOnDate.Size = new System.Drawing.Size(136, 19);
            this.rdbActivateOnDate.TabIndex = 2;
            this.rdbActivateOnDate.Text = "Specified Date/Time";
            this.rdbActivateOnDate.UseVisualStyleBackColor = true;
            // 
            // rdbInvokeAction
            // 
            this.rdbInvokeAction.AutoSize = true;
            this.rdbInvokeAction.Checked = true;
            this.rdbInvokeAction.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.rdbInvokeAction.ForeColor = System.Drawing.Color.Black;
            this.rdbInvokeAction.Location = new System.Drawing.Point(16, 24);
            this.rdbInvokeAction.Name = "rdbInvokeAction";
            this.rdbInvokeAction.Size = new System.Drawing.Size(51, 19);
            this.rdbInvokeAction.TabIndex = 1;
            this.rdbInvokeAction.TabStop = true;
            this.rdbInvokeAction.Text = "Now";
            this.rdbInvokeAction.UseVisualStyleBackColor = true;
            this.rdbInvokeAction.CheckedChanged += new System.EventHandler(this.rdbInvokeAction_CheckedChanged);
            // 
            // dtc_CalendarActivationDate
            // 
            this.dtc_CalendarActivationDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt";
            this.dtc_CalendarActivationDate.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtc_CalendarActivationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtc_CalendarActivationDate.Location = new System.Drawing.Point(67, 89);
            this.dtc_CalendarActivationDate.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dtc_CalendarActivationDate.Name = "dtc_CalendarActivationDate";
            this.dtc_CalendarActivationDate.Size = new System.Drawing.Size(197, 23);
            this.dtc_CalendarActivationDate.TabIndex = 3;
            this.dtc_CalendarActivationDate.ValueChanged += new System.EventHandler(this.dtc_CalendarActivationDate_ValueChanged);
            // 
            // btn_GetActivityCalendar
            // 
            this.btn_GetActivityCalendar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_GetActivityCalendar.Image = ((System.Drawing.Image)(resources.GetObject("btn_GetActivityCalendar.Image")));
            this.btn_GetActivityCalendar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_GetActivityCalendar.Location = new System.Drawing.Point(229, 267);
            this.btn_GetActivityCalendar.Name = "btn_GetActivityCalendar";
            this.btn_GetActivityCalendar.Size = new System.Drawing.Size(150, 30);
            this.btn_GetActivityCalendar.TabIndex = 5;
            this.btn_GetActivityCalendar.Text = "Get Activity Calendar";
            this.btn_GetActivityCalendar.UseVisualStyleBackColor = true;
            // 
            // btn_setActivityCalendar
            // 
            this.btn_setActivityCalendar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_setActivityCalendar.Image = ((System.Drawing.Image)(resources.GetObject("btn_setActivityCalendar.Image")));
            this.btn_setActivityCalendar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_setActivityCalendar.Location = new System.Drawing.Point(72, 267);
            this.btn_setActivityCalendar.Name = "btn_setActivityCalendar";
            this.btn_setActivityCalendar.Size = new System.Drawing.Size(150, 30);
            this.btn_setActivityCalendar.TabIndex = 4;
            this.btn_setActivityCalendar.Text = "Set Activity Calendar";
            this.btn_setActivityCalendar.UseVisualStyleBackColor = true;
            // 
            // Day_Profile
            // 
            this.Day_Profile.BackColor = System.Drawing.SystemColors.Control;
            this.Day_Profile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Day_Profile.Controls.Add(this.btnGetDayProfile);
            this.Day_Profile.Controls.Add(this.btnSetDayProfile);
            this.Day_Profile.Controls.Add(this.lbl_ErrorDayProfile);
            this.Day_Profile.Controls.Add(this.gpDayProfileSettings);
            this.Day_Profile.Controls.Add(this.btn_delete_DayProfile);
            this.Day_Profile.Controls.Add(this.btn_newDayProfile);
            this.Day_Profile.Controls.Add(this.list_DayProfile);
            this.Day_Profile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Day_Profile.Location = new System.Drawing.Point(4, 24);
            this.Day_Profile.Name = "Day_Profile";
            this.Day_Profile.Padding = new System.Windows.Forms.Padding(3);
            this.Day_Profile.Size = new System.Drawing.Size(192, 72);
            this.Day_Profile.TabIndex = 0;
            this.Day_Profile.Text = "Day Profile";
            this.Day_Profile.Enter += new System.EventHandler(this.Day_Profile_Enter);
            this.Day_Profile.Leave += new System.EventHandler(this.Day_Profile_Leave);
            this.Day_Profile.MouseEnter += new System.EventHandler(this.Day_Profile_MouseEnter);
            // 
            // btnGetDayProfile
            // 
            this.btnGetDayProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetDayProfile.Image = ((System.Drawing.Image)(resources.GetObject("btnGetDayProfile.Image")));
            this.btnGetDayProfile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGetDayProfile.Location = new System.Drawing.Point(474, 354);
            this.btnGetDayProfile.Name = "btnGetDayProfile";
            this.btnGetDayProfile.Size = new System.Drawing.Size(150, 30);
            this.btnGetDayProfile.TabIndex = 60;
            this.btnGetDayProfile.Text = "Get Day Profile";
            this.btnGetDayProfile.UseVisualStyleBackColor = true;
            // 
            // btnSetDayProfile
            // 
            this.btnSetDayProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetDayProfile.Image = ((System.Drawing.Image)(resources.GetObject("btnSetDayProfile.Image")));
            this.btnSetDayProfile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSetDayProfile.Location = new System.Drawing.Point(317, 354);
            this.btnSetDayProfile.Name = "btnSetDayProfile";
            this.btnSetDayProfile.Size = new System.Drawing.Size(150, 30);
            this.btnSetDayProfile.TabIndex = 59;
            this.btnSetDayProfile.Text = "Set Day Profile";
            this.btnSetDayProfile.UseVisualStyleBackColor = true;
            // 
            // lbl_ErrorDayProfile
            // 
            this.lbl_ErrorDayProfile.AutoSize = true;
            this.lbl_ErrorDayProfile.BackColor = System.Drawing.Color.Transparent;
            this.lbl_ErrorDayProfile.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbl_ErrorDayProfile.ForeColor = System.Drawing.Color.DarkRed;
            this.lbl_ErrorDayProfile.Location = new System.Drawing.Point(144, 317);
            this.lbl_ErrorDayProfile.Name = "lbl_ErrorDayProfile";
            this.lbl_ErrorDayProfile.Size = new System.Drawing.Size(232, 15);
            this.lbl_ErrorDayProfile.TabIndex = 58;
            this.lbl_ErrorDayProfile.Text = "Error Validation,Invalid Param DayProfiles";
            this.lbl_ErrorDayProfile.Visible = false;
            // 
            // gpDayProfileSettings
            // 
            this.gpDayProfileSettings.BackColor = System.Drawing.Color.Transparent;
            this.gpDayProfileSettings.Controls.Add(this.label76);
            this.gpDayProfileSettings.Controls.Add(this.panel_numberofslots);
            this.gpDayProfileSettings.Controls.Add(this.label77);
            this.gpDayProfileSettings.Controls.Add(this.label73);
            this.gpDayProfileSettings.Controls.Add(this.label78);
            this.gpDayProfileSettings.Controls.Add(this.btn_Save_DayProfile);
            this.gpDayProfileSettings.Controls.Add(this.label79);
            this.gpDayProfileSettings.Controls.Add(this.panel_dayslot6);
            this.gpDayProfileSettings.Controls.Add(this.panel_dayslot1);
            this.gpDayProfileSettings.Controls.Add(this.btn_AddDayProfile);
            this.gpDayProfileSettings.Controls.Add(this.panel_dayslot5);
            this.gpDayProfileSettings.Controls.Add(this.panel_dayslot2);
            this.gpDayProfileSettings.Controls.Add(this.panel_dayslot4);
            this.gpDayProfileSettings.Controls.Add(this.panel_dayslot3);
            this.gpDayProfileSettings.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpDayProfileSettings.ForeColor = System.Drawing.Color.Maroon;
            this.gpDayProfileSettings.Location = new System.Drawing.Point(140, 12);
            this.gpDayProfileSettings.Name = "gpDayProfileSettings";
            this.gpDayProfileSettings.Size = new System.Drawing.Size(300, 300);
            this.gpDayProfileSettings.TabIndex = 57;
            this.gpDayProfileSettings.TabStop = false;
            this.gpDayProfileSettings.Text = "Day Profile Settings";
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label76.ForeColor = System.Drawing.Color.Navy;
            this.label76.Location = new System.Drawing.Point(240, 49);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(20, 15);
            this.label76.TabIndex = 28;
            this.label76.Text = "T4";
            // 
            // panel_numberofslots
            // 
            this.panel_numberofslots.Controls.Add(this.combo_tariff_NumberofSlots);
            this.panel_numberofslots.Controls.Add(this.label71);
            this.panel_numberofslots.Location = new System.Drawing.Point(15, 14);
            this.panel_numberofslots.Name = "panel_numberofslots";
            this.panel_numberofslots.Size = new System.Drawing.Size(271, 32);
            this.panel_numberofslots.TabIndex = 50;
            this.panel_numberofslots.Visible = false;
            // 
            // combo_tariff_NumberofSlots
            // 
            this.combo_tariff_NumberofSlots.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_tariff_NumberofSlots.FormattingEnabled = true;
            this.combo_tariff_NumberofSlots.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.combo_tariff_NumberofSlots.Location = new System.Drawing.Point(126, 5);
            this.combo_tariff_NumberofSlots.Name = "combo_tariff_NumberofSlots";
            this.combo_tariff_NumberofSlots.Size = new System.Drawing.Size(50, 23);
            this.combo_tariff_NumberofSlots.TabIndex = 8;
            this.combo_tariff_NumberofSlots.SelectedIndexChanged += new System.EventHandler(this.combo_tariff_NumberofSlots_SelectedIndexChanged);
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label71.ForeColor = System.Drawing.Color.Navy;
            this.label71.Location = new System.Drawing.Point(7, 9);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(96, 15);
            this.label71.TabIndex = 21;
            this.label71.Text = "Number of Slots";
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label77.ForeColor = System.Drawing.Color.Navy;
            this.label77.Location = new System.Drawing.Point(224, 49);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(20, 15);
            this.label77.TabIndex = 24;
            this.label77.Text = "T3";
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label73.ForeColor = System.Drawing.Color.Navy;
            this.label73.Location = new System.Drawing.Point(84, 49);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(64, 15);
            this.label73.TabIndex = 31;
            this.label73.Text = "Start Time";
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label78.ForeColor = System.Drawing.Color.Navy;
            this.label78.Location = new System.Drawing.Point(208, 49);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(20, 15);
            this.label78.TabIndex = 25;
            this.label78.Text = "T2";
            // 
            // btn_Save_DayProfile
            // 
            this.btn_Save_DayProfile.Location = new System.Drawing.Point(152, 268);
            this.btn_Save_DayProfile.Name = "btn_Save_DayProfile";
            this.btn_Save_DayProfile.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_Save_DayProfile.Size = new System.Drawing.Size(134, 24);
            this.btn_Save_DayProfile.TabIndex = 16;
            this.btn_Save_DayProfile.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_Save_DayProfile.Values.Image")));
            this.btn_Save_DayProfile.Values.Text = "Save Day Profile";
            this.btn_Save_DayProfile.Click += new System.EventHandler(this.btn_Save_DayProfile_Click);
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label79.ForeColor = System.Drawing.Color.Navy;
            this.label79.Location = new System.Drawing.Point(189, 49);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(20, 15);
            this.label79.TabIndex = 26;
            this.label79.Text = "T1";
            // 
            // panel_dayslot6
            // 
            this.panel_dayslot6.Controls.Add(this.txt_DayProfile_s6);
            this.panel_dayslot6.Controls.Add(this.panel15);
            this.panel_dayslot6.Controls.Add(this.lbl_Dp_Slot_s6);
            this.panel_dayslot6.ForeColor = System.Drawing.Color.Navy;
            this.panel_dayslot6.Location = new System.Drawing.Point(15, 212);
            this.panel_dayslot6.Name = "panel_dayslot6";
            this.panel_dayslot6.Size = new System.Drawing.Size(271, 30);
            this.panel_dayslot6.TabIndex = 54;
            this.panel_dayslot6.Visible = false;
            // 
            // txt_DayProfile_s6
            // 
            this.txt_DayProfile_s6.CustomFormat = "HH:mm";
            this.txt_DayProfile_s6.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.txt_DayProfile_s6.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txt_DayProfile_s6.Location = new System.Drawing.Point(72, 3);
            this.txt_DayProfile_s6.Name = "txt_DayProfile_s6";
            this.txt_DayProfile_s6.ShowUpDown = true;
            this.txt_DayProfile_s6.Size = new System.Drawing.Size(57, 23);
            this.txt_DayProfile_s6.TabIndex = 14;
            this.txt_DayProfile_s6.Value = new System.DateTime(1753, 1, 1, 12, 1, 0, 0);
            this.txt_DayProfile_s6.ValueChanged += new System.EventHandler(this.txt_DayProfile_s_ValueChanged);
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.radio_DayProfile_s6t2);
            this.panel15.Controls.Add(this.radio_DayProfile_s6t1);
            this.panel15.Controls.Add(this.radio_DayProfile_s6t3);
            this.panel15.Controls.Add(this.radio_DayProfile_s6t4);
            this.panel15.Location = new System.Drawing.Point(172, 2);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(87, 24);
            this.panel15.TabIndex = 34;
            // 
            // radio_DayProfile_s6t2
            // 
            this.radio_DayProfile_s6t2.AutoSize = true;
            this.radio_DayProfile_s6t2.Location = new System.Drawing.Point(24, 4);
            this.radio_DayProfile_s6t2.Name = "radio_DayProfile_s6t2";
            this.radio_DayProfile_s6t2.Size = new System.Drawing.Size(14, 13);
            this.radio_DayProfile_s6t2.TabIndex = 0;
            this.radio_DayProfile_s6t2.UseVisualStyleBackColor = true;
            this.radio_DayProfile_s6t2.CheckedChanged += new System.EventHandler(this.radio_DayProfile_s1t1_CheckedChanged);
            // 
            // radio_DayProfile_s6t1
            // 
            this.radio_DayProfile_s6t1.AutoSize = true;
            this.radio_DayProfile_s6t1.Checked = true;
            this.radio_DayProfile_s6t1.Location = new System.Drawing.Point(8, 4);
            this.radio_DayProfile_s6t1.Name = "radio_DayProfile_s6t1";
            this.radio_DayProfile_s6t1.Size = new System.Drawing.Size(14, 13);
            this.radio_DayProfile_s6t1.TabIndex = 0;
            this.radio_DayProfile_s6t1.TabStop = true;
            this.radio_DayProfile_s6t1.UseVisualStyleBackColor = true;
            this.radio_DayProfile_s6t1.CheckedChanged += new System.EventHandler(this.radio_DayProfile_s1t1_CheckedChanged);
            // 
            // radio_DayProfile_s6t3
            // 
            this.radio_DayProfile_s6t3.AutoSize = true;
            this.radio_DayProfile_s6t3.Location = new System.Drawing.Point(40, 4);
            this.radio_DayProfile_s6t3.Name = "radio_DayProfile_s6t3";
            this.radio_DayProfile_s6t3.Size = new System.Drawing.Size(14, 13);
            this.radio_DayProfile_s6t3.TabIndex = 0;
            this.radio_DayProfile_s6t3.UseVisualStyleBackColor = true;
            this.radio_DayProfile_s6t3.CheckedChanged += new System.EventHandler(this.radio_DayProfile_s1t1_CheckedChanged);
            // 
            // radio_DayProfile_s6t4
            // 
            this.radio_DayProfile_s6t4.AutoSize = true;
            this.radio_DayProfile_s6t4.Location = new System.Drawing.Point(56, 4);
            this.radio_DayProfile_s6t4.Name = "radio_DayProfile_s6t4";
            this.radio_DayProfile_s6t4.Size = new System.Drawing.Size(14, 13);
            this.radio_DayProfile_s6t4.TabIndex = 0;
            this.radio_DayProfile_s6t4.UseVisualStyleBackColor = true;
            // 
            // lbl_Dp_Slot_s6
            // 
            this.lbl_Dp_Slot_s6.AutoSize = true;
            this.lbl_Dp_Slot_s6.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Dp_Slot_s6.ForeColor = System.Drawing.Color.Navy;
            this.lbl_Dp_Slot_s6.Location = new System.Drawing.Point(7, 7);
            this.lbl_Dp_Slot_s6.Name = "lbl_Dp_Slot_s6";
            this.lbl_Dp_Slot_s6.Size = new System.Drawing.Size(44, 15);
            this.lbl_Dp_Slot_s6.TabIndex = 33;
            this.lbl_Dp_Slot_s6.Text = "SLOT:6";
            // 
            // panel_dayslot1
            // 
            this.panel_dayslot1.Controls.Add(this.txt_DayProfile_s1);
            this.panel_dayslot1.Controls.Add(this.panel2);
            this.panel_dayslot1.Controls.Add(this.lbl_Dp_Slot_s1);
            this.panel_dayslot1.ForeColor = System.Drawing.Color.Navy;
            this.panel_dayslot1.Location = new System.Drawing.Point(15, 64);
            this.panel_dayslot1.Name = "panel_dayslot1";
            this.panel_dayslot1.Size = new System.Drawing.Size(271, 30);
            this.panel_dayslot1.TabIndex = 51;
            this.panel_dayslot1.Visible = false;
            // 
            // txt_DayProfile_s1
            // 
            this.txt_DayProfile_s1.CustomFormat = "HH:mm";
            this.txt_DayProfile_s1.Enabled = false;
            this.txt_DayProfile_s1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.txt_DayProfile_s1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txt_DayProfile_s1.Location = new System.Drawing.Point(72, 3);
            this.txt_DayProfile_s1.Name = "txt_DayProfile_s1";
            this.txt_DayProfile_s1.ShowUpDown = true;
            this.txt_DayProfile_s1.Size = new System.Drawing.Size(57, 23);
            this.txt_DayProfile_s1.TabIndex = 9;
            this.txt_DayProfile_s1.Value = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.txt_DayProfile_s1.ValueChanged += new System.EventHandler(this.txt_DayProfile_s_ValueChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radio_DayProfile_s1t2);
            this.panel2.Controls.Add(this.radio_DayProfile_s1t1);
            this.panel2.Controls.Add(this.radio_DayProfile_s1t3);
            this.panel2.Controls.Add(this.radio_DayProfile_s1t4);
            this.panel2.Location = new System.Drawing.Point(172, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(87, 24);
            this.panel2.TabIndex = 29;
            // 
            // radio_DayProfile_s1t2
            // 
            this.radio_DayProfile_s1t2.AutoSize = true;
            this.radio_DayProfile_s1t2.Location = new System.Drawing.Point(24, 4);
            this.radio_DayProfile_s1t2.Name = "radio_DayProfile_s1t2";
            this.radio_DayProfile_s1t2.Size = new System.Drawing.Size(14, 13);
            this.radio_DayProfile_s1t2.TabIndex = 0;
            this.radio_DayProfile_s1t2.UseVisualStyleBackColor = true;
            this.radio_DayProfile_s1t2.CheckedChanged += new System.EventHandler(this.radio_DayProfile_s1t1_CheckedChanged);
            // 
            // radio_DayProfile_s1t1
            // 
            this.radio_DayProfile_s1t1.AutoSize = true;
            this.radio_DayProfile_s1t1.Checked = true;
            this.radio_DayProfile_s1t1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radio_DayProfile_s1t1.Location = new System.Drawing.Point(8, 4);
            this.radio_DayProfile_s1t1.Name = "radio_DayProfile_s1t1";
            this.radio_DayProfile_s1t1.Size = new System.Drawing.Size(14, 13);
            this.radio_DayProfile_s1t1.TabIndex = 0;
            this.radio_DayProfile_s1t1.TabStop = true;
            this.radio_DayProfile_s1t1.UseVisualStyleBackColor = true;
            this.radio_DayProfile_s1t1.CheckedChanged += new System.EventHandler(this.radio_DayProfile_s1t1_CheckedChanged);
            // 
            // radio_DayProfile_s1t3
            // 
            this.radio_DayProfile_s1t3.AutoSize = true;
            this.radio_DayProfile_s1t3.Location = new System.Drawing.Point(40, 4);
            this.radio_DayProfile_s1t3.Name = "radio_DayProfile_s1t3";
            this.radio_DayProfile_s1t3.Size = new System.Drawing.Size(14, 13);
            this.radio_DayProfile_s1t3.TabIndex = 0;
            this.radio_DayProfile_s1t3.UseVisualStyleBackColor = true;
            this.radio_DayProfile_s1t3.CheckedChanged += new System.EventHandler(this.radio_DayProfile_s1t1_CheckedChanged);
            // 
            // radio_DayProfile_s1t4
            // 
            this.radio_DayProfile_s1t4.AutoSize = true;
            this.radio_DayProfile_s1t4.Location = new System.Drawing.Point(56, 4);
            this.radio_DayProfile_s1t4.Name = "radio_DayProfile_s1t4";
            this.radio_DayProfile_s1t4.Size = new System.Drawing.Size(14, 13);
            this.radio_DayProfile_s1t4.TabIndex = 0;
            this.radio_DayProfile_s1t4.UseVisualStyleBackColor = true;
            // 
            // lbl_Dp_Slot_s1
            // 
            this.lbl_Dp_Slot_s1.AutoSize = true;
            this.lbl_Dp_Slot_s1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Dp_Slot_s1.ForeColor = System.Drawing.Color.Navy;
            this.lbl_Dp_Slot_s1.Location = new System.Drawing.Point(7, 7);
            this.lbl_Dp_Slot_s1.Name = "lbl_Dp_Slot_s1";
            this.lbl_Dp_Slot_s1.Size = new System.Drawing.Size(44, 15);
            this.lbl_Dp_Slot_s1.TabIndex = 27;
            this.lbl_Dp_Slot_s1.Text = "SLOT:1";
            // 
            // btn_AddDayProfile
            // 
            this.btn_AddDayProfile.Enabled = false;
            this.btn_AddDayProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddDayProfile.Location = new System.Drawing.Point(15, 268);
            this.btn_AddDayProfile.Name = "btn_AddDayProfile";
            this.btn_AddDayProfile.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_AddDayProfile.Size = new System.Drawing.Size(129, 24);
            this.btn_AddDayProfile.TabIndex = 15;
            this.btn_AddDayProfile.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_AddDayProfile.Values.Image")));
            this.btn_AddDayProfile.Values.Text = "Add Day Profile";
            this.btn_AddDayProfile.Visible = false;
            this.btn_AddDayProfile.Click += new System.EventHandler(this.btn_AddDayProfile_Click);
            // 
            // panel_dayslot5
            // 
            this.panel_dayslot5.Controls.Add(this.txt_DayProfile_s5);
            this.panel_dayslot5.Controls.Add(this.panel7);
            this.panel_dayslot5.Controls.Add(this.lbl_Dp_Slot_s5);
            this.panel_dayslot5.ForeColor = System.Drawing.Color.Navy;
            this.panel_dayslot5.Location = new System.Drawing.Point(15, 182);
            this.panel_dayslot5.Name = "panel_dayslot5";
            this.panel_dayslot5.Size = new System.Drawing.Size(271, 30);
            this.panel_dayslot5.TabIndex = 55;
            this.panel_dayslot5.Visible = false;
            // 
            // txt_DayProfile_s5
            // 
            this.txt_DayProfile_s5.CustomFormat = "HH:mm";
            this.txt_DayProfile_s5.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.txt_DayProfile_s5.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txt_DayProfile_s5.Location = new System.Drawing.Point(72, 3);
            this.txt_DayProfile_s5.Name = "txt_DayProfile_s5";
            this.txt_DayProfile_s5.ShowUpDown = true;
            this.txt_DayProfile_s5.Size = new System.Drawing.Size(57, 23);
            this.txt_DayProfile_s5.TabIndex = 13;
            this.txt_DayProfile_s5.Value = new System.DateTime(1753, 1, 1, 12, 1, 0, 0);
            this.txt_DayProfile_s5.ValueChanged += new System.EventHandler(this.txt_DayProfile_s_ValueChanged);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.radio_DayProfile_s5t2);
            this.panel7.Controls.Add(this.radio_DayProfile_s5t1);
            this.panel7.Controls.Add(this.radio_DayProfile_s5t3);
            this.panel7.Controls.Add(this.radio_DayProfile_s5t4);
            this.panel7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel7.Location = new System.Drawing.Point(172, 2);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(87, 24);
            this.panel7.TabIndex = 34;
            // 
            // radio_DayProfile_s5t2
            // 
            this.radio_DayProfile_s5t2.AutoSize = true;
            this.radio_DayProfile_s5t2.Location = new System.Drawing.Point(24, 4);
            this.radio_DayProfile_s5t2.Name = "radio_DayProfile_s5t2";
            this.radio_DayProfile_s5t2.Size = new System.Drawing.Size(14, 13);
            this.radio_DayProfile_s5t2.TabIndex = 0;
            this.radio_DayProfile_s5t2.UseVisualStyleBackColor = true;
            this.radio_DayProfile_s5t2.CheckedChanged += new System.EventHandler(this.radio_DayProfile_s1t1_CheckedChanged);
            // 
            // radio_DayProfile_s5t1
            // 
            this.radio_DayProfile_s5t1.AutoSize = true;
            this.radio_DayProfile_s5t1.Checked = true;
            this.radio_DayProfile_s5t1.Location = new System.Drawing.Point(8, 4);
            this.radio_DayProfile_s5t1.Name = "radio_DayProfile_s5t1";
            this.radio_DayProfile_s5t1.Size = new System.Drawing.Size(14, 13);
            this.radio_DayProfile_s5t1.TabIndex = 0;
            this.radio_DayProfile_s5t1.TabStop = true;
            this.radio_DayProfile_s5t1.UseVisualStyleBackColor = true;
            this.radio_DayProfile_s5t1.CheckedChanged += new System.EventHandler(this.radio_DayProfile_s1t1_CheckedChanged);
            // 
            // radio_DayProfile_s5t3
            // 
            this.radio_DayProfile_s5t3.AutoSize = true;
            this.radio_DayProfile_s5t3.Location = new System.Drawing.Point(40, 4);
            this.radio_DayProfile_s5t3.Name = "radio_DayProfile_s5t3";
            this.radio_DayProfile_s5t3.Size = new System.Drawing.Size(14, 13);
            this.radio_DayProfile_s5t3.TabIndex = 0;
            this.radio_DayProfile_s5t3.UseVisualStyleBackColor = true;
            this.radio_DayProfile_s5t3.CheckedChanged += new System.EventHandler(this.radio_DayProfile_s1t1_CheckedChanged);
            // 
            // radio_DayProfile_s5t4
            // 
            this.radio_DayProfile_s5t4.AutoSize = true;
            this.radio_DayProfile_s5t4.Location = new System.Drawing.Point(56, 4);
            this.radio_DayProfile_s5t4.Name = "radio_DayProfile_s5t4";
            this.radio_DayProfile_s5t4.Size = new System.Drawing.Size(14, 13);
            this.radio_DayProfile_s5t4.TabIndex = 0;
            this.radio_DayProfile_s5t4.UseVisualStyleBackColor = true;
            // 
            // lbl_Dp_Slot_s5
            // 
            this.lbl_Dp_Slot_s5.AutoSize = true;
            this.lbl_Dp_Slot_s5.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Dp_Slot_s5.ForeColor = System.Drawing.Color.Navy;
            this.lbl_Dp_Slot_s5.Location = new System.Drawing.Point(7, 7);
            this.lbl_Dp_Slot_s5.Name = "lbl_Dp_Slot_s5";
            this.lbl_Dp_Slot_s5.Size = new System.Drawing.Size(44, 15);
            this.lbl_Dp_Slot_s5.TabIndex = 33;
            this.lbl_Dp_Slot_s5.Text = "SLOT:5";
            // 
            // panel_dayslot2
            // 
            this.panel_dayslot2.Controls.Add(this.txt_DayProfile_s2);
            this.panel_dayslot2.Controls.Add(this.panel3);
            this.panel_dayslot2.Controls.Add(this.lbl_Dp_Slot_s2);
            this.panel_dayslot2.ForeColor = System.Drawing.Color.Navy;
            this.panel_dayslot2.Location = new System.Drawing.Point(15, 92);
            this.panel_dayslot2.Name = "panel_dayslot2";
            this.panel_dayslot2.Size = new System.Drawing.Size(271, 30);
            this.panel_dayslot2.TabIndex = 52;
            this.panel_dayslot2.Visible = false;
            // 
            // txt_DayProfile_s2
            // 
            this.txt_DayProfile_s2.CustomFormat = "HH:mm";
            this.txt_DayProfile_s2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.txt_DayProfile_s2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txt_DayProfile_s2.Location = new System.Drawing.Point(72, 3);
            this.txt_DayProfile_s2.Name = "txt_DayProfile_s2";
            this.txt_DayProfile_s2.ShowUpDown = true;
            this.txt_DayProfile_s2.Size = new System.Drawing.Size(57, 23);
            this.txt_DayProfile_s2.TabIndex = 10;
            this.txt_DayProfile_s2.Value = new System.DateTime(1753, 1, 1, 12, 1, 0, 0);
            this.txt_DayProfile_s2.ValueChanged += new System.EventHandler(this.txt_DayProfile_s_ValueChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.radio_DayProfile_s2t2);
            this.panel3.Controls.Add(this.radio_DayProfile_s2t1);
            this.panel3.Controls.Add(this.radio_DayProfile_s2t3);
            this.panel3.Controls.Add(this.radio_DayProfile_s2t4);
            this.panel3.Location = new System.Drawing.Point(172, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(87, 24);
            this.panel3.TabIndex = 34;
            // 
            // radio_DayProfile_s2t2
            // 
            this.radio_DayProfile_s2t2.AutoSize = true;
            this.radio_DayProfile_s2t2.Location = new System.Drawing.Point(24, 4);
            this.radio_DayProfile_s2t2.Name = "radio_DayProfile_s2t2";
            this.radio_DayProfile_s2t2.Size = new System.Drawing.Size(14, 13);
            this.radio_DayProfile_s2t2.TabIndex = 0;
            this.radio_DayProfile_s2t2.UseVisualStyleBackColor = true;
            this.radio_DayProfile_s2t2.CheckedChanged += new System.EventHandler(this.radio_DayProfile_s1t1_CheckedChanged);
            // 
            // radio_DayProfile_s2t1
            // 
            this.radio_DayProfile_s2t1.AutoSize = true;
            this.radio_DayProfile_s2t1.Checked = true;
            this.radio_DayProfile_s2t1.Location = new System.Drawing.Point(8, 4);
            this.radio_DayProfile_s2t1.Name = "radio_DayProfile_s2t1";
            this.radio_DayProfile_s2t1.Size = new System.Drawing.Size(14, 13);
            this.radio_DayProfile_s2t1.TabIndex = 0;
            this.radio_DayProfile_s2t1.TabStop = true;
            this.radio_DayProfile_s2t1.UseVisualStyleBackColor = true;
            this.radio_DayProfile_s2t1.CheckedChanged += new System.EventHandler(this.radio_DayProfile_s1t1_CheckedChanged);
            // 
            // radio_DayProfile_s2t3
            // 
            this.radio_DayProfile_s2t3.AutoSize = true;
            this.radio_DayProfile_s2t3.Location = new System.Drawing.Point(40, 4);
            this.radio_DayProfile_s2t3.Name = "radio_DayProfile_s2t3";
            this.radio_DayProfile_s2t3.Size = new System.Drawing.Size(14, 13);
            this.radio_DayProfile_s2t3.TabIndex = 0;
            this.radio_DayProfile_s2t3.UseVisualStyleBackColor = true;
            this.radio_DayProfile_s2t3.CheckedChanged += new System.EventHandler(this.radio_DayProfile_s1t1_CheckedChanged);
            // 
            // radio_DayProfile_s2t4
            // 
            this.radio_DayProfile_s2t4.AutoSize = true;
            this.radio_DayProfile_s2t4.Location = new System.Drawing.Point(56, 4);
            this.radio_DayProfile_s2t4.Name = "radio_DayProfile_s2t4";
            this.radio_DayProfile_s2t4.Size = new System.Drawing.Size(14, 13);
            this.radio_DayProfile_s2t4.TabIndex = 0;
            this.radio_DayProfile_s2t4.UseVisualStyleBackColor = true;
            // 
            // lbl_Dp_Slot_s2
            // 
            this.lbl_Dp_Slot_s2.AutoSize = true;
            this.lbl_Dp_Slot_s2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Dp_Slot_s2.ForeColor = System.Drawing.Color.Navy;
            this.lbl_Dp_Slot_s2.Location = new System.Drawing.Point(7, 7);
            this.lbl_Dp_Slot_s2.Name = "lbl_Dp_Slot_s2";
            this.lbl_Dp_Slot_s2.Size = new System.Drawing.Size(44, 15);
            this.lbl_Dp_Slot_s2.TabIndex = 33;
            this.lbl_Dp_Slot_s2.Text = "SLOT:2";
            // 
            // panel_dayslot4
            // 
            this.panel_dayslot4.Controls.Add(this.txt_DayProfile_s4);
            this.panel_dayslot4.Controls.Add(this.panel6);
            this.panel_dayslot4.Controls.Add(this.lbl_Dp_Slot_s4);
            this.panel_dayslot4.ForeColor = System.Drawing.Color.Navy;
            this.panel_dayslot4.Location = new System.Drawing.Point(15, 152);
            this.panel_dayslot4.Name = "panel_dayslot4";
            this.panel_dayslot4.Size = new System.Drawing.Size(271, 30);
            this.panel_dayslot4.TabIndex = 56;
            this.panel_dayslot4.Visible = false;
            // 
            // txt_DayProfile_s4
            // 
            this.txt_DayProfile_s4.CustomFormat = "HH:mm";
            this.txt_DayProfile_s4.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.txt_DayProfile_s4.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txt_DayProfile_s4.Location = new System.Drawing.Point(72, 3);
            this.txt_DayProfile_s4.Name = "txt_DayProfile_s4";
            this.txt_DayProfile_s4.ShowUpDown = true;
            this.txt_DayProfile_s4.Size = new System.Drawing.Size(57, 23);
            this.txt_DayProfile_s4.TabIndex = 12;
            this.txt_DayProfile_s4.Value = new System.DateTime(1753, 1, 1, 12, 1, 0, 0);
            this.txt_DayProfile_s4.ValueChanged += new System.EventHandler(this.txt_DayProfile_s_ValueChanged);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.radio_DayProfile_s4t2);
            this.panel6.Controls.Add(this.radio_DayProfile_s4t1);
            this.panel6.Controls.Add(this.radio_DayProfile_s4t3);
            this.panel6.Controls.Add(this.radio_DayProfile_s4t4);
            this.panel6.Location = new System.Drawing.Point(172, 2);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(87, 24);
            this.panel6.TabIndex = 34;
            // 
            // radio_DayProfile_s4t2
            // 
            this.radio_DayProfile_s4t2.AutoSize = true;
            this.radio_DayProfile_s4t2.Location = new System.Drawing.Point(24, 4);
            this.radio_DayProfile_s4t2.Name = "radio_DayProfile_s4t2";
            this.radio_DayProfile_s4t2.Size = new System.Drawing.Size(14, 13);
            this.radio_DayProfile_s4t2.TabIndex = 0;
            this.radio_DayProfile_s4t2.UseVisualStyleBackColor = true;
            this.radio_DayProfile_s4t2.CheckedChanged += new System.EventHandler(this.radio_DayProfile_s1t1_CheckedChanged);
            // 
            // radio_DayProfile_s4t1
            // 
            this.radio_DayProfile_s4t1.AutoSize = true;
            this.radio_DayProfile_s4t1.Checked = true;
            this.radio_DayProfile_s4t1.Location = new System.Drawing.Point(8, 4);
            this.radio_DayProfile_s4t1.Name = "radio_DayProfile_s4t1";
            this.radio_DayProfile_s4t1.Size = new System.Drawing.Size(14, 13);
            this.radio_DayProfile_s4t1.TabIndex = 0;
            this.radio_DayProfile_s4t1.TabStop = true;
            this.radio_DayProfile_s4t1.UseVisualStyleBackColor = true;
            this.radio_DayProfile_s4t1.CheckedChanged += new System.EventHandler(this.radio_DayProfile_s1t1_CheckedChanged);
            // 
            // radio_DayProfile_s4t3
            // 
            this.radio_DayProfile_s4t3.AutoSize = true;
            this.radio_DayProfile_s4t3.Location = new System.Drawing.Point(40, 4);
            this.radio_DayProfile_s4t3.Name = "radio_DayProfile_s4t3";
            this.radio_DayProfile_s4t3.Size = new System.Drawing.Size(14, 13);
            this.radio_DayProfile_s4t3.TabIndex = 0;
            this.radio_DayProfile_s4t3.UseVisualStyleBackColor = true;
            this.radio_DayProfile_s4t3.CheckedChanged += new System.EventHandler(this.radio_DayProfile_s1t1_CheckedChanged);
            // 
            // radio_DayProfile_s4t4
            // 
            this.radio_DayProfile_s4t4.AutoSize = true;
            this.radio_DayProfile_s4t4.Location = new System.Drawing.Point(56, 4);
            this.radio_DayProfile_s4t4.Name = "radio_DayProfile_s4t4";
            this.radio_DayProfile_s4t4.Size = new System.Drawing.Size(14, 13);
            this.radio_DayProfile_s4t4.TabIndex = 0;
            this.radio_DayProfile_s4t4.UseVisualStyleBackColor = true;
            // 
            // lbl_Dp_Slot_s4
            // 
            this.lbl_Dp_Slot_s4.AutoSize = true;
            this.lbl_Dp_Slot_s4.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Dp_Slot_s4.ForeColor = System.Drawing.Color.Navy;
            this.lbl_Dp_Slot_s4.Location = new System.Drawing.Point(7, 7);
            this.lbl_Dp_Slot_s4.Name = "lbl_Dp_Slot_s4";
            this.lbl_Dp_Slot_s4.Size = new System.Drawing.Size(44, 15);
            this.lbl_Dp_Slot_s4.TabIndex = 33;
            this.lbl_Dp_Slot_s4.Text = "SLOT:4";
            // 
            // panel_dayslot3
            // 
            this.panel_dayslot3.Controls.Add(this.txt_DayProfile_s3);
            this.panel_dayslot3.Controls.Add(this.panel5);
            this.panel_dayslot3.Controls.Add(this.lbl_Dp_Slot_s3);
            this.panel_dayslot3.ForeColor = System.Drawing.Color.Navy;
            this.panel_dayslot3.Location = new System.Drawing.Point(15, 122);
            this.panel_dayslot3.Name = "panel_dayslot3";
            this.panel_dayslot3.Size = new System.Drawing.Size(271, 30);
            this.panel_dayslot3.TabIndex = 53;
            this.panel_dayslot3.Visible = false;
            // 
            // txt_DayProfile_s3
            // 
            this.txt_DayProfile_s3.CustomFormat = "HH:mm";
            this.txt_DayProfile_s3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.txt_DayProfile_s3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txt_DayProfile_s3.Location = new System.Drawing.Point(72, 3);
            this.txt_DayProfile_s3.Name = "txt_DayProfile_s3";
            this.txt_DayProfile_s3.ShowUpDown = true;
            this.txt_DayProfile_s3.Size = new System.Drawing.Size(57, 23);
            this.txt_DayProfile_s3.TabIndex = 11;
            this.txt_DayProfile_s3.Value = new System.DateTime(1753, 1, 1, 12, 1, 0, 0);
            this.txt_DayProfile_s3.ValueChanged += new System.EventHandler(this.txt_DayProfile_s_ValueChanged);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.radio_DayProfile_s3t2);
            this.panel5.Controls.Add(this.radio_DayProfile_s3t1);
            this.panel5.Controls.Add(this.radio_DayProfile_s3t3);
            this.panel5.Controls.Add(this.radio_DayProfile_s3t4);
            this.panel5.Location = new System.Drawing.Point(172, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(87, 24);
            this.panel5.TabIndex = 34;
            // 
            // radio_DayProfile_s3t2
            // 
            this.radio_DayProfile_s3t2.AutoSize = true;
            this.radio_DayProfile_s3t2.Location = new System.Drawing.Point(24, 4);
            this.radio_DayProfile_s3t2.Name = "radio_DayProfile_s3t2";
            this.radio_DayProfile_s3t2.Size = new System.Drawing.Size(14, 13);
            this.radio_DayProfile_s3t2.TabIndex = 0;
            this.radio_DayProfile_s3t2.UseVisualStyleBackColor = true;
            this.radio_DayProfile_s3t2.CheckedChanged += new System.EventHandler(this.radio_DayProfile_s1t1_CheckedChanged);
            // 
            // radio_DayProfile_s3t1
            // 
            this.radio_DayProfile_s3t1.AutoSize = true;
            this.radio_DayProfile_s3t1.Checked = true;
            this.radio_DayProfile_s3t1.Location = new System.Drawing.Point(8, 4);
            this.radio_DayProfile_s3t1.Name = "radio_DayProfile_s3t1";
            this.radio_DayProfile_s3t1.Size = new System.Drawing.Size(14, 13);
            this.radio_DayProfile_s3t1.TabIndex = 0;
            this.radio_DayProfile_s3t1.TabStop = true;
            this.radio_DayProfile_s3t1.UseVisualStyleBackColor = true;
            this.radio_DayProfile_s3t1.CheckedChanged += new System.EventHandler(this.radio_DayProfile_s1t1_CheckedChanged);
            // 
            // radio_DayProfile_s3t3
            // 
            this.radio_DayProfile_s3t3.AutoSize = true;
            this.radio_DayProfile_s3t3.Location = new System.Drawing.Point(40, 4);
            this.radio_DayProfile_s3t3.Name = "radio_DayProfile_s3t3";
            this.radio_DayProfile_s3t3.Size = new System.Drawing.Size(14, 13);
            this.radio_DayProfile_s3t3.TabIndex = 0;
            this.radio_DayProfile_s3t3.UseVisualStyleBackColor = true;
            this.radio_DayProfile_s3t3.CheckedChanged += new System.EventHandler(this.radio_DayProfile_s1t1_CheckedChanged);
            // 
            // radio_DayProfile_s3t4
            // 
            this.radio_DayProfile_s3t4.AutoSize = true;
            this.radio_DayProfile_s3t4.Location = new System.Drawing.Point(56, 4);
            this.radio_DayProfile_s3t4.Name = "radio_DayProfile_s3t4";
            this.radio_DayProfile_s3t4.Size = new System.Drawing.Size(14, 13);
            this.radio_DayProfile_s3t4.TabIndex = 0;
            this.radio_DayProfile_s3t4.UseVisualStyleBackColor = true;
            // 
            // lbl_Dp_Slot_s3
            // 
            this.lbl_Dp_Slot_s3.AutoSize = true;
            this.lbl_Dp_Slot_s3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Dp_Slot_s3.ForeColor = System.Drawing.Color.Navy;
            this.lbl_Dp_Slot_s3.Location = new System.Drawing.Point(7, 7);
            this.lbl_Dp_Slot_s3.Name = "lbl_Dp_Slot_s3";
            this.lbl_Dp_Slot_s3.Size = new System.Drawing.Size(44, 15);
            this.lbl_Dp_Slot_s3.TabIndex = 33;
            this.lbl_Dp_Slot_s3.Text = "SLOT:3";
            // 
            // btn_delete_DayProfile
            // 
            this.btn_delete_DayProfile.Location = new System.Drawing.Point(8, 316);
            this.btn_delete_DayProfile.Name = "btn_delete_DayProfile";
            this.btn_delete_DayProfile.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_delete_DayProfile.Size = new System.Drawing.Size(126, 25);
            this.btn_delete_DayProfile.TabIndex = 7;
            this.btn_delete_DayProfile.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_delete_DayProfile.Values.Image")));
            this.btn_delete_DayProfile.Values.Text = "Delete DayProfile";
            this.btn_delete_DayProfile.Click += new System.EventHandler(this.btn_delete_DayProfile_Click);
            // 
            // btn_newDayProfile
            // 
            this.btn_newDayProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_newDayProfile.Location = new System.Drawing.Point(8, 21);
            this.btn_newDayProfile.Name = "btn_newDayProfile";
            this.btn_newDayProfile.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_newDayProfile.Size = new System.Drawing.Size(126, 25);
            this.btn_newDayProfile.TabIndex = 6;
            this.btn_newDayProfile.Values.Text = "New DayProfile";
            this.btn_newDayProfile.Click += new System.EventHandler(this.btn_newDayProfile_Click);
            // 
            // list_DayProfile
            // 
            this.list_DayProfile.FormattingEnabled = true;
            this.list_DayProfile.Location = new System.Drawing.Point(8, 51);
            this.list_DayProfile.Name = "list_DayProfile";
            this.list_DayProfile.Size = new System.Drawing.Size(126, 261);
            this.list_DayProfile.StateCommon.Item.Content.LongText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.list_DayProfile.StateCommon.Item.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.list_DayProfile.TabIndex = 39;
            this.list_DayProfile.SelectedIndexChanged += new System.EventHandler(this.list_DayProfile_SelectedIndexChanged);
            this.list_DayProfile.Click += new System.EventHandler(this.list_DayProfile_Click);
            // 
            // Week_Profile
            // 
            this.Week_Profile.BackColor = System.Drawing.Color.Transparent;
            this.Week_Profile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Week_Profile.Controls.Add(this.btnGetWeekProfile);
            this.Week_Profile.Controls.Add(this.btnSetWeekProfile);
            this.Week_Profile.Controls.Add(this.lbl_ErrorWeekProfile);
            this.Week_Profile.Controls.Add(this.btn_Delete_WeekProfile);
            this.Week_Profile.Controls.Add(this.groupBox_WeekProfile);
            this.Week_Profile.Controls.Add(this.btn_weekProfile_ADD);
            this.Week_Profile.Controls.Add(this.btn_newWeekProfile);
            this.Week_Profile.Controls.Add(this.list_WeekProfile);
            this.Week_Profile.Location = new System.Drawing.Point(4, 24);
            this.Week_Profile.Name = "Week_Profile";
            this.Week_Profile.Padding = new System.Windows.Forms.Padding(3);
            this.Week_Profile.Size = new System.Drawing.Size(192, 72);
            this.Week_Profile.TabIndex = 1;
            this.Week_Profile.Text = "Week Profile";
            this.Week_Profile.Enter += new System.EventHandler(this.Week_Profile_Enter);
            this.Week_Profile.Leave += new System.EventHandler(this.Week_Profile_Leave);
            this.Week_Profile.MouseEnter += new System.EventHandler(this.Week_Profile_MouseEnter);
            // 
            // btnGetWeekProfile
            // 
            this.btnGetWeekProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetWeekProfile.Image = ((System.Drawing.Image)(resources.GetObject("btnGetWeekProfile.Image")));
            this.btnGetWeekProfile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGetWeekProfile.Location = new System.Drawing.Point(384, 351);
            this.btnGetWeekProfile.Name = "btnGetWeekProfile";
            this.btnGetWeekProfile.Size = new System.Drawing.Size(150, 30);
            this.btnGetWeekProfile.TabIndex = 62;
            this.btnGetWeekProfile.Text = "Get Week Profile";
            this.btnGetWeekProfile.UseVisualStyleBackColor = true;
            // 
            // btnSetWeekProfile
            // 
            this.btnSetWeekProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetWeekProfile.Image = ((System.Drawing.Image)(resources.GetObject("btnSetWeekProfile.Image")));
            this.btnSetWeekProfile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSetWeekProfile.Location = new System.Drawing.Point(227, 351);
            this.btnSetWeekProfile.Name = "btnSetWeekProfile";
            this.btnSetWeekProfile.Size = new System.Drawing.Size(150, 30);
            this.btnSetWeekProfile.TabIndex = 61;
            this.btnSetWeekProfile.Text = "Set Week Profile";
            this.btnSetWeekProfile.UseVisualStyleBackColor = true;
            // 
            // lbl_ErrorWeekProfile
            // 
            this.lbl_ErrorWeekProfile.AutoSize = true;
            this.lbl_ErrorWeekProfile.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbl_ErrorWeekProfile.ForeColor = System.Drawing.Color.DarkRed;
            this.lbl_ErrorWeekProfile.Location = new System.Drawing.Point(157, 328);
            this.lbl_ErrorWeekProfile.Name = "lbl_ErrorWeekProfile";
            this.lbl_ErrorWeekProfile.Size = new System.Drawing.Size(244, 15);
            this.lbl_ErrorWeekProfile.TabIndex = 59;
            this.lbl_ErrorWeekProfile.Text = "Error Validation,Invalid Param WeekProfiles";
            this.lbl_ErrorWeekProfile.Visible = false;
            // 
            // btn_Delete_WeekProfile
            // 
            this.btn_Delete_WeekProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Delete_WeekProfile.Location = new System.Drawing.Point(18, 325);
            this.btn_Delete_WeekProfile.Name = "btn_Delete_WeekProfile";
            this.btn_Delete_WeekProfile.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_Delete_WeekProfile.Size = new System.Drawing.Size(114, 25);
            this.btn_Delete_WeekProfile.TabIndex = 18;
            this.btn_Delete_WeekProfile.Values.Text = "Delete WeekProfile";
            this.btn_Delete_WeekProfile.Click += new System.EventHandler(this.btn_Delete_WeekProfile_Click);
            // 
            // groupBox_WeekProfile
            // 
            this.groupBox_WeekProfile.Controls.Add(this.combo_day_fri);
            this.groupBox_WeekProfile.Controls.Add(this.btn_Save_WeekProfile);
            this.groupBox_WeekProfile.Controls.Add(this.combo_day_Mon);
            this.groupBox_WeekProfile.Controls.Add(this.label10);
            this.groupBox_WeekProfile.Controls.Add(this.combo_day_wed);
            this.groupBox_WeekProfile.Controls.Add(this.label14);
            this.groupBox_WeekProfile.Controls.Add(this.combo_day_tue);
            this.groupBox_WeekProfile.Controls.Add(this.label16);
            this.groupBox_WeekProfile.Controls.Add(this.combo_day_thu);
            this.groupBox_WeekProfile.Controls.Add(this.label17);
            this.groupBox_WeekProfile.Controls.Add(this.combo_day_sat);
            this.groupBox_WeekProfile.Controls.Add(this.label18);
            this.groupBox_WeekProfile.Controls.Add(this.combo_day_sun);
            this.groupBox_WeekProfile.Controls.Add(this.label19);
            this.groupBox_WeekProfile.Controls.Add(this.lbl_day);
            this.groupBox_WeekProfile.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_WeekProfile.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox_WeekProfile.Location = new System.Drawing.Point(138, 20);
            this.groupBox_WeekProfile.Name = "groupBox_WeekProfile";
            this.groupBox_WeekProfile.Size = new System.Drawing.Size(279, 300);
            this.groupBox_WeekProfile.TabIndex = 25;
            this.groupBox_WeekProfile.TabStop = false;
            this.groupBox_WeekProfile.Text = "Week Profile Settings";
            this.groupBox_WeekProfile.Visible = false;
            // 
            // combo_day_fri
            // 
            this.combo_day_fri.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_day_fri.FormattingEnabled = true;
            this.combo_day_fri.Location = new System.Drawing.Point(73, 133);
            this.combo_day_fri.Name = "combo_day_fri";
            this.combo_day_fri.Size = new System.Drawing.Size(72, 23);
            this.combo_day_fri.TabIndex = 23;
            // 
            // btn_Save_WeekProfile
            // 
            this.btn_Save_WeekProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save_WeekProfile.Location = new System.Drawing.Point(15, 260);
            this.btn_Save_WeekProfile.Name = "btn_Save_WeekProfile";
            this.btn_Save_WeekProfile.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_Save_WeekProfile.Size = new System.Drawing.Size(75, 24);
            this.btn_Save_WeekProfile.TabIndex = 26;
            this.btn_Save_WeekProfile.Values.Text = "Save";
            this.btn_Save_WeekProfile.Click += new System.EventHandler(this.btn_Save_WeekProfile_Click);
            // 
            // combo_day_Mon
            // 
            this.combo_day_Mon.DisplayMember = "Select Day PRofile";
            this.combo_day_Mon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_day_Mon.FormattingEnabled = true;
            this.combo_day_Mon.Location = new System.Drawing.Point(73, 25);
            this.combo_day_Mon.Name = "combo_day_Mon";
            this.combo_day_Mon.Size = new System.Drawing.Size(72, 23);
            this.combo_day_Mon.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(15, 55);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(27, 15);
            this.label10.TabIndex = 20;
            this.label10.Text = "TUE";
            // 
            // combo_day_wed
            // 
            this.combo_day_wed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_day_wed.FormattingEnabled = true;
            this.combo_day_wed.Location = new System.Drawing.Point(73, 79);
            this.combo_day_wed.Name = "combo_day_wed";
            this.combo_day_wed.Size = new System.Drawing.Size(72, 23);
            this.combo_day_wed.TabIndex = 21;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(15, 163);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(26, 15);
            this.label14.TabIndex = 18;
            this.label14.Text = "SAT";
            // 
            // combo_day_tue
            // 
            this.combo_day_tue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_day_tue.FormattingEnabled = true;
            this.combo_day_tue.Location = new System.Drawing.Point(73, 52);
            this.combo_day_tue.Name = "combo_day_tue";
            this.combo_day_tue.Size = new System.Drawing.Size(72, 23);
            this.combo_day_tue.TabIndex = 20;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(15, 190);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(30, 15);
            this.label16.TabIndex = 17;
            this.label16.Text = "SUN";
            // 
            // combo_day_thu
            // 
            this.combo_day_thu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_day_thu.FormattingEnabled = true;
            this.combo_day_thu.Location = new System.Drawing.Point(73, 106);
            this.combo_day_thu.Name = "combo_day_thu";
            this.combo_day_thu.Size = new System.Drawing.Size(72, 23);
            this.combo_day_thu.TabIndex = 22;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(15, 136);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(23, 15);
            this.label17.TabIndex = 16;
            this.label17.Text = "FRI";
            // 
            // combo_day_sat
            // 
            this.combo_day_sat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_day_sat.FormattingEnabled = true;
            this.combo_day_sat.Location = new System.Drawing.Point(73, 160);
            this.combo_day_sat.Name = "combo_day_sat";
            this.combo_day_sat.Size = new System.Drawing.Size(72, 23);
            this.combo_day_sat.TabIndex = 24;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Black;
            this.label18.Location = new System.Drawing.Point(15, 109);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(29, 15);
            this.label18.TabIndex = 15;
            this.label18.Text = "THU";
            // 
            // combo_day_sun
            // 
            this.combo_day_sun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_day_sun.FormattingEnabled = true;
            this.combo_day_sun.Location = new System.Drawing.Point(73, 187);
            this.combo_day_sun.Name = "combo_day_sun";
            this.combo_day_sun.Size = new System.Drawing.Size(72, 23);
            this.combo_day_sun.TabIndex = 25;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(15, 82);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(33, 15);
            this.label19.TabIndex = 14;
            this.label19.Text = "WED";
            // 
            // lbl_day
            // 
            this.lbl_day.AutoSize = true;
            this.lbl_day.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_day.ForeColor = System.Drawing.Color.Black;
            this.lbl_day.Location = new System.Drawing.Point(15, 28);
            this.lbl_day.Name = "lbl_day";
            this.lbl_day.Size = new System.Drawing.Size(36, 15);
            this.lbl_day.TabIndex = 13;
            this.lbl_day.Text = "MON";
            // 
            // btn_weekProfile_ADD
            // 
            this.btn_weekProfile_ADD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_weekProfile_ADD.Location = new System.Drawing.Point(553, 355);
            this.btn_weekProfile_ADD.Name = "btn_weekProfile_ADD";
            this.btn_weekProfile_ADD.Size = new System.Drawing.Size(75, 23);
            this.btn_weekProfile_ADD.TabIndex = 27;
            this.btn_weekProfile_ADD.Text = "ADD";
            this.btn_weekProfile_ADD.UseVisualStyleBackColor = true;
            this.btn_weekProfile_ADD.Visible = false;
            this.btn_weekProfile_ADD.Click += new System.EventHandler(this.btn_weekProfile_ADD_Click);
            // 
            // btn_newWeekProfile
            // 
            this.btn_newWeekProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_newWeekProfile.Location = new System.Drawing.Point(18, 27);
            this.btn_newWeekProfile.Name = "btn_newWeekProfile";
            this.btn_newWeekProfile.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_newWeekProfile.Size = new System.Drawing.Size(114, 25);
            this.btn_newWeekProfile.TabIndex = 17;
            this.btn_newWeekProfile.Values.Text = "New WeekProfile";
            this.btn_newWeekProfile.Click += new System.EventHandler(this.btn_newWeekProfile_Click);
            // 
            // list_WeekProfile
            // 
            this.list_WeekProfile.FormattingEnabled = true;
            this.list_WeekProfile.Location = new System.Drawing.Point(18, 59);
            this.list_WeekProfile.Name = "list_WeekProfile";
            this.list_WeekProfile.Size = new System.Drawing.Size(114, 261);
            this.list_WeekProfile.StateCommon.Item.Content.LongText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.list_WeekProfile.StateCommon.Item.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.list_WeekProfile.TabIndex = 15;
            this.list_WeekProfile.SelectedIndexChanged += new System.EventHandler(this.list_WeekProfile_SelectedIndexChanged);
            this.list_WeekProfile.Click += new System.EventHandler(this.list_WeekProfile_Click);
            // 
            // _SeasonProfile
            // 
            this._SeasonProfile.BackColor = System.Drawing.Color.Transparent;
            this._SeasonProfile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._SeasonProfile.Controls.Add(this.btnGetSeasonProfile);
            this._SeasonProfile.Controls.Add(this.btnSetSeasonProfile);
            this._SeasonProfile.Controls.Add(this.lbl_ErrorSeasonProfiles);
            this._SeasonProfile.Controls.Add(this.list_SeasonProfile);
            this._SeasonProfile.Controls.Add(this.groupBox_SeasonProfile);
            this._SeasonProfile.Controls.Add(this.btn_DeleteSeasonProfile);
            this._SeasonProfile.Location = new System.Drawing.Point(4, 24);
            this._SeasonProfile.Name = "_SeasonProfile";
            this._SeasonProfile.Padding = new System.Windows.Forms.Padding(3);
            this._SeasonProfile.Size = new System.Drawing.Size(630, 390);
            this._SeasonProfile.TabIndex = 2;
            this._SeasonProfile.Text = "Season Profile";
            this._SeasonProfile.Enter += new System.EventHandler(this.Season_Profiles_Enter);
            this._SeasonProfile.Leave += new System.EventHandler(this.Season_Profiles_Leave);
            // 
            // btnGetSeasonProfile
            // 
            this.btnGetSeasonProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetSeasonProfile.Image = ((System.Drawing.Image)(resources.GetObject("btnGetSeasonProfile.Image")));
            this.btnGetSeasonProfile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGetSeasonProfile.Location = new System.Drawing.Point(464, 354);
            this.btnGetSeasonProfile.Name = "btnGetSeasonProfile";
            this.btnGetSeasonProfile.Size = new System.Drawing.Size(150, 30);
            this.btnGetSeasonProfile.TabIndex = 64;
            this.btnGetSeasonProfile.Text = "Get Season Profile";
            this.btnGetSeasonProfile.UseVisualStyleBackColor = true;
            // 
            // btnSetSeasonProfile
            // 
            this.btnSetSeasonProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetSeasonProfile.Image = ((System.Drawing.Image)(resources.GetObject("btnSetSeasonProfile.Image")));
            this.btnSetSeasonProfile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSetSeasonProfile.Location = new System.Drawing.Point(307, 354);
            this.btnSetSeasonProfile.Name = "btnSetSeasonProfile";
            this.btnSetSeasonProfile.Size = new System.Drawing.Size(150, 30);
            this.btnSetSeasonProfile.TabIndex = 63;
            this.btnSetSeasonProfile.Text = "Set Season Profile";
            this.btnSetSeasonProfile.UseVisualStyleBackColor = true;
            // 
            // lbl_ErrorSeasonProfiles
            // 
            this.lbl_ErrorSeasonProfiles.AutoSize = true;
            this.lbl_ErrorSeasonProfiles.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbl_ErrorSeasonProfiles.ForeColor = System.Drawing.Color.DarkRed;
            this.lbl_ErrorSeasonProfiles.Location = new System.Drawing.Point(145, 287);
            this.lbl_ErrorSeasonProfiles.Name = "lbl_ErrorSeasonProfiles";
            this.lbl_ErrorSeasonProfiles.Size = new System.Drawing.Size(250, 15);
            this.lbl_ErrorSeasonProfiles.TabIndex = 60;
            this.lbl_ErrorSeasonProfiles.Text = "Error Validation,Invalid Param SeasonProfiles";
            this.lbl_ErrorSeasonProfiles.Visible = false;
            // 
            // list_SeasonProfile
            // 
            this.list_SeasonProfile.FormattingEnabled = true;
            this.list_SeasonProfile.Location = new System.Drawing.Point(22, 37);
            this.list_SeasonProfile.Name = "list_SeasonProfile";
            this.list_SeasonProfile.Size = new System.Drawing.Size(114, 244);
            this.list_SeasonProfile.StateCommon.Item.Content.LongText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.list_SeasonProfile.StateCommon.Item.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.list_SeasonProfile.TabIndex = 25;
            this.list_SeasonProfile.SelectedIndexChanged += new System.EventHandler(this.list_SeasonProfile_SelectedIndexChanged);
            // 
            // groupBox_SeasonProfile
            // 
            this.groupBox_SeasonProfile.Controls.Add(this.lbl_Format);
            this.groupBox_SeasonProfile.Controls.Add(this.dtc_SeasonProfile_);
            this.groupBox_SeasonProfile.Controls.Add(this.combo_SeasonName);
            this.groupBox_SeasonProfile.Controls.Add(this.label157);
            this.groupBox_SeasonProfile.Controls.Add(this.label158);
            this.groupBox_SeasonProfile.Controls.Add(this.label86);
            this.groupBox_SeasonProfile.Controls.Add(this.combo_season_weekProfile1);
            this.groupBox_SeasonProfile.Controls.Add(this.btn_addSeasonProfile);
            this.groupBox_SeasonProfile.Controls.Add(this.btn_Save_SeasonProfile);
            this.groupBox_SeasonProfile.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBox_SeasonProfile.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox_SeasonProfile.Location = new System.Drawing.Point(142, 33);
            this.groupBox_SeasonProfile.Name = "groupBox_SeasonProfile";
            this.groupBox_SeasonProfile.Size = new System.Drawing.Size(409, 248);
            this.groupBox_SeasonProfile.TabIndex = 31;
            this.groupBox_SeasonProfile.TabStop = false;
            this.groupBox_SeasonProfile.Text = "Season Profile Settings";
            // 
            // lbl_Format
            // 
            this.lbl_Format.AutoSize = true;
            this.lbl_Format.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbl_Format.ForeColor = System.Drawing.Color.Black;
            this.lbl_Format.Location = new System.Drawing.Point(108, 83);
            this.lbl_Format.Name = "lbl_Format";
            this.lbl_Format.Size = new System.Drawing.Size(128, 15);
            this.lbl_Format.TabIndex = 36;
            this.lbl_Format.Text = "(DayOfMonth, Month)";
            // 
            // dtc_SeasonProfile_
            // 
            this.dtc_SeasonProfile_.BackColor = System.Drawing.SystemColors.Window;
            this.dtc_SeasonProfile_.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtc_SeasonProfile_.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtc_SeasonProfile_.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dtc_SeasonProfile_.FormatEx = DLMS.dtpCustomExtensions.dtpShortDate;
            this.dtc_SeasonProfile_.Location = new System.Drawing.Point(111, 101);
            this.dtc_SeasonProfile_.Name = "dtc_SeasonProfile_";
            this.dtc_SeasonProfile_.ShowButtons = true;
            this.dtc_SeasonProfile_.ShowUpDownButton = false;
            this.dtc_SeasonProfile_.ShowWildCardWinButton = false;
            this.dtc_SeasonProfile_.Size = new System.Drawing.Size(144, 22);
            this.dtc_SeasonProfile_.TabIndex = 35;
            // 
            // combo_SeasonName
            // 
            this.combo_SeasonName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_SeasonName.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.combo_SeasonName.FormattingEnabled = true;
            this.combo_SeasonName.Items.AddRange(new object[] {
            "SeasonProfile_1",
            "SeasonProfile_2",
            "SeasonProfile_3",
            "SeasonProfile_4"});
            this.combo_SeasonName.Location = new System.Drawing.Point(111, 16);
            this.combo_SeasonName.Name = "combo_SeasonName";
            this.combo_SeasonName.Size = new System.Drawing.Size(144, 23);
            this.combo_SeasonName.TabIndex = 30;
            // 
            // label157
            // 
            this.label157.AutoSize = true;
            this.label157.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.label157.ForeColor = System.Drawing.Color.Black;
            this.label157.Location = new System.Drawing.Point(6, 103);
            this.label157.Name = "label157";
            this.label157.Size = new System.Drawing.Size(63, 15);
            this.label157.TabIndex = 27;
            this.label157.Text = "Start Date";
            // 
            // label158
            // 
            this.label158.AutoSize = true;
            this.label158.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.label158.ForeColor = System.Drawing.Color.Black;
            this.label158.Location = new System.Drawing.Point(5, 24);
            this.label158.Name = "label158";
            this.label158.Size = new System.Drawing.Size(79, 15);
            this.label158.TabIndex = 27;
            this.label158.Text = "Profile Name";
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.label86.ForeColor = System.Drawing.Color.Black;
            this.label86.Location = new System.Drawing.Point(5, 59);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(92, 15);
            this.label86.TabIndex = 27;
            this.label86.Text = "Week Profile ID";
            // 
            // combo_season_weekProfile1
            // 
            this.combo_season_weekProfile1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_season_weekProfile1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.combo_season_weekProfile1.FormattingEnabled = true;
            this.combo_season_weekProfile1.Location = new System.Drawing.Point(111, 56);
            this.combo_season_weekProfile1.Name = "combo_season_weekProfile1";
            this.combo_season_weekProfile1.Size = new System.Drawing.Size(144, 23);
            this.combo_season_weekProfile1.TabIndex = 26;
            // 
            // btn_addSeasonProfile
            // 
            this.btn_addSeasonProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_addSeasonProfile.Location = new System.Drawing.Point(6, 204);
            this.btn_addSeasonProfile.Name = "btn_addSeasonProfile";
            this.btn_addSeasonProfile.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_addSeasonProfile.Size = new System.Drawing.Size(75, 24);
            this.btn_addSeasonProfile.TabIndex = 32;
            this.btn_addSeasonProfile.Values.Text = "Add";
            this.btn_addSeasonProfile.Click += new System.EventHandler(this.btn_addSeasonProfile_Click);
            // 
            // btn_Save_SeasonProfile
            // 
            this.btn_Save_SeasonProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save_SeasonProfile.Location = new System.Drawing.Point(87, 204);
            this.btn_Save_SeasonProfile.Name = "btn_Save_SeasonProfile";
            this.btn_Save_SeasonProfile.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_Save_SeasonProfile.Size = new System.Drawing.Size(75, 24);
            this.btn_Save_SeasonProfile.TabIndex = 33;
            this.btn_Save_SeasonProfile.Values.Text = "Save";
            this.btn_Save_SeasonProfile.Click += new System.EventHandler(this.btn_Save_SeasonProfile_Click);
            // 
            // btn_DeleteSeasonProfile
            // 
            this.btn_DeleteSeasonProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DeleteSeasonProfile.Location = new System.Drawing.Point(22, 287);
            this.btn_DeleteSeasonProfile.Name = "btn_DeleteSeasonProfile";
            this.btn_DeleteSeasonProfile.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_DeleteSeasonProfile.Size = new System.Drawing.Size(115, 24);
            this.btn_DeleteSeasonProfile.TabIndex = 34;
            this.btn_DeleteSeasonProfile.Values.Text = "Delete SeasonProfile";
            this.btn_DeleteSeasonProfile.Click += new System.EventHandler(this.btn_DeleteSeasonProfile_Click);
            // 
            // Special_Days
            // 
            this.Special_Days.BackColor = System.Drawing.Color.Transparent;
            this.Special_Days.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Special_Days.Controls.Add(this.btnGetSpecialDays);
            this.Special_Days.Controls.Add(this.btnSetSpecialDays);
            this.Special_Days.Controls.Add(this.lbl_ErrorSpecialDays);
            this.Special_Days.Controls.Add(this.specialDays);
            this.Special_Days.Controls.Add(this.list_SpecialDays);
            this.Special_Days.Controls.Add(this.btn_Delete_SpecialDay);
            this.Special_Days.Location = new System.Drawing.Point(4, 24);
            this.Special_Days.Name = "Special_Days";
            this.Special_Days.Size = new System.Drawing.Size(192, 72);
            this.Special_Days.TabIndex = 3;
            this.Special_Days.Text = "Special Days";
            this.Special_Days.Enter += new System.EventHandler(this.SpecialDay_Profile_Enter);
            this.Special_Days.Leave += new System.EventHandler(this.SpecialDay_Profile_Leave);
            this.Special_Days.MouseEnter += new System.EventHandler(this.Special_Days_MouseEnter);
            // 
            // btnGetSpecialDays
            // 
            this.btnGetSpecialDays.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetSpecialDays.Image = ((System.Drawing.Image)(resources.GetObject("btnGetSpecialDays.Image")));
            this.btnGetSpecialDays.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGetSpecialDays.Location = new System.Drawing.Point(467, 347);
            this.btnGetSpecialDays.Name = "btnGetSpecialDays";
            this.btnGetSpecialDays.Size = new System.Drawing.Size(150, 30);
            this.btnGetSpecialDays.TabIndex = 66;
            this.btnGetSpecialDays.Text = "Get Special Days";
            this.btnGetSpecialDays.UseVisualStyleBackColor = true;
            // 
            // btnSetSpecialDays
            // 
            this.btnSetSpecialDays.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetSpecialDays.Image = ((System.Drawing.Image)(resources.GetObject("btnSetSpecialDays.Image")));
            this.btnSetSpecialDays.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSetSpecialDays.Location = new System.Drawing.Point(310, 347);
            this.btnSetSpecialDays.Name = "btnSetSpecialDays";
            this.btnSetSpecialDays.Size = new System.Drawing.Size(150, 30);
            this.btnSetSpecialDays.TabIndex = 65;
            this.btnSetSpecialDays.Text = "Set Special Days";
            this.btnSetSpecialDays.UseVisualStyleBackColor = true;
            // 
            // lbl_ErrorSpecialDays
            // 
            this.lbl_ErrorSpecialDays.AutoSize = true;
            this.lbl_ErrorSpecialDays.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbl_ErrorSpecialDays.ForeColor = System.Drawing.Color.DarkRed;
            this.lbl_ErrorSpecialDays.Location = new System.Drawing.Point(121, 299);
            this.lbl_ErrorSpecialDays.Name = "lbl_ErrorSpecialDays";
            this.lbl_ErrorSpecialDays.Size = new System.Drawing.Size(272, 15);
            this.lbl_ErrorSpecialDays.TabIndex = 61;
            this.lbl_ErrorSpecialDays.Text = "Error Validation,Invalid Param SpecialDay Profiles";
            this.lbl_ErrorSpecialDays.Visible = false;
            // 
            // specialDays
            // 
            this.specialDays.Controls.Add(this.ucSPDayProfileDateTime);
            this.specialDays.Controls.Add(this.lbl_SpDay_StartDate);
            this.specialDays.Controls.Add(this.lbl_SpDay_DayProfileID);
            this.specialDays.Controls.Add(this.btn_SpecialDays_add);
            this.specialDays.Controls.Add(this.combo_SpecialDay_ProfileID);
            this.specialDays.Controls.Add(this.btn_Save_SpecialDay);
            this.specialDays.ForeColor = System.Drawing.Color.Maroon;
            this.specialDays.Location = new System.Drawing.Point(116, 30);
            this.specialDays.Name = "specialDays";
            this.specialDays.Size = new System.Drawing.Size(456, 259);
            this.specialDays.TabIndex = 53;
            this.specialDays.TabStop = false;
            this.specialDays.Text = "Settings";
            // 
            // ucSPDayProfileDateTime
            // 
            this.ucSPDayProfileDateTime.BackColor = System.Drawing.Color.Transparent;
            this.ucSPDayProfileDateTime.ForeColor = System.Drawing.Color.Black;
            this.ucSPDayProfileDateTime.Location = new System.Drawing.Point(8, 68);
            this.ucSPDayProfileDateTime.Name = "ucSPDayProfileDateTime";
            this.ucSPDayProfileDateTime.Size = new System.Drawing.Size(444, 91);
            this.ucSPDayProfileDateTime.TabIndex = 58;
            // 
            // lbl_SpDay_StartDate
            // 
            this.lbl_SpDay_StartDate.AutoSize = true;
            this.lbl_SpDay_StartDate.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbl_SpDay_StartDate.ForeColor = System.Drawing.Color.Black;
            this.lbl_SpDay_StartDate.Location = new System.Drawing.Point(5, 50);
            this.lbl_SpDay_StartDate.Name = "lbl_SpDay_StartDate";
            this.lbl_SpDay_StartDate.Size = new System.Drawing.Size(63, 15);
            this.lbl_SpDay_StartDate.TabIndex = 57;
            this.lbl_SpDay_StartDate.Text = "Start Date";
            // 
            // lbl_SpDay_DayProfileID
            // 
            this.lbl_SpDay_DayProfileID.AutoSize = true;
            this.lbl_SpDay_DayProfileID.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbl_SpDay_DayProfileID.ForeColor = System.Drawing.Color.Black;
            this.lbl_SpDay_DayProfileID.Location = new System.Drawing.Point(5, 23);
            this.lbl_SpDay_DayProfileID.Name = "lbl_SpDay_DayProfileID";
            this.lbl_SpDay_DayProfileID.Size = new System.Drawing.Size(80, 15);
            this.lbl_SpDay_DayProfileID.TabIndex = 52;
            this.lbl_SpDay_DayProfileID.Text = "Day Profile ID";
            // 
            // btn_SpecialDays_add
            // 
            this.btn_SpecialDays_add.Location = new System.Drawing.Point(8, 229);
            this.btn_SpecialDays_add.Name = "btn_SpecialDays_add";
            this.btn_SpecialDays_add.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_SpecialDays_add.Size = new System.Drawing.Size(75, 24);
            this.btn_SpecialDays_add.TabIndex = 36;
            this.btn_SpecialDays_add.Values.Text = "ADD";
            this.btn_SpecialDays_add.Click += new System.EventHandler(this.btn_SpecialDays_add_Click_1);
            // 
            // combo_SpecialDay_ProfileID
            // 
            this.combo_SpecialDay_ProfileID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_SpecialDay_ProfileID.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.combo_SpecialDay_ProfileID.FormattingEnabled = true;
            this.combo_SpecialDay_ProfileID.Location = new System.Drawing.Point(125, 20);
            this.combo_SpecialDay_ProfileID.Name = "combo_SpecialDay_ProfileID";
            this.combo_SpecialDay_ProfileID.Size = new System.Drawing.Size(93, 23);
            this.combo_SpecialDay_ProfileID.TabIndex = 35;
            // 
            // btn_Save_SpecialDay
            // 
            this.btn_Save_SpecialDay.Location = new System.Drawing.Point(89, 229);
            this.btn_Save_SpecialDay.Name = "btn_Save_SpecialDay";
            this.btn_Save_SpecialDay.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_Save_SpecialDay.Size = new System.Drawing.Size(75, 24);
            this.btn_Save_SpecialDay.TabIndex = 38;
            this.btn_Save_SpecialDay.Values.Text = "Save";
            this.btn_Save_SpecialDay.Click += new System.EventHandler(this.btn_Save_SpecialDay_Click);
            // 
            // list_SpecialDays
            // 
            this.list_SpecialDays.FormatString = "M";
            this.list_SpecialDays.FormattingEnabled = true;
            this.list_SpecialDays.Location = new System.Drawing.Point(11, 34);
            this.list_SpecialDays.Name = "list_SpecialDays";
            this.list_SpecialDays.Size = new System.Drawing.Size(100, 259);
            this.list_SpecialDays.StateCommon.Item.Content.LongText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.list_SpecialDays.StateCommon.Item.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.list_SpecialDays.TabIndex = 6;
            this.list_SpecialDays.SelectedIndexChanged += new System.EventHandler(this.list_SpecialDays_SelectedIndexChanged);
            // 
            // btn_Delete_SpecialDay
            // 
            this.btn_Delete_SpecialDay.Location = new System.Drawing.Point(11, 299);
            this.btn_Delete_SpecialDay.Name = "btn_Delete_SpecialDay";
            this.btn_Delete_SpecialDay.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btn_Delete_SpecialDay.Size = new System.Drawing.Size(105, 24);
            this.btn_Delete_SpecialDay.TabIndex = 39;
            this.btn_Delete_SpecialDay.Values.Text = "Delete SpecialDay";
            this.btn_Delete_SpecialDay.Click += new System.EventHandler(this.btn_Delete_SpecialDay_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // ucActivityCalendar
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.Profiles);
            this.DoubleBuffered = true;
            this.Name = "ucActivityCalendar";
            this.Size = new System.Drawing.Size(638, 418);
            this.Load += new System.EventHandler(this.ucActivityCalendar_Load);
            this.Leave += new System.EventHandler(this.ucActivityCalendar_Leave);
            this.Profiles.ResumeLayout(false);
            this.Calendar_Page.ResumeLayout(false);
            this.Calendar_Page.PerformLayout();
            this.gp_Activate.ResumeLayout(false);
            this.gp_Activate.PerformLayout();
            this.gpActivationMode.ResumeLayout(false);
            this.gpActivationMode.PerformLayout();
            this.Day_Profile.ResumeLayout(false);
            this.Day_Profile.PerformLayout();
            this.gpDayProfileSettings.ResumeLayout(false);
            this.gpDayProfileSettings.PerformLayout();
            this.panel_numberofslots.ResumeLayout(false);
            this.panel_numberofslots.PerformLayout();
            this.panel_dayslot6.ResumeLayout(false);
            this.panel_dayslot6.PerformLayout();
            this.panel15.ResumeLayout(false);
            this.panel15.PerformLayout();
            this.panel_dayslot1.ResumeLayout(false);
            this.panel_dayslot1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel_dayslot5.ResumeLayout(false);
            this.panel_dayslot5.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel_dayslot2.ResumeLayout(false);
            this.panel_dayslot2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel_dayslot4.ResumeLayout(false);
            this.panel_dayslot4.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel_dayslot3.ResumeLayout(false);
            this.panel_dayslot3.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.Week_Profile.ResumeLayout(false);
            this.Week_Profile.PerformLayout();
            this.groupBox_WeekProfile.ResumeLayout(false);
            this.groupBox_WeekProfile.PerformLayout();
            this._SeasonProfile.ResumeLayout(false);
            this._SeasonProfile.PerformLayout();
            this.groupBox_SeasonProfile.ResumeLayout(false);
            this.groupBox_SeasonProfile.PerformLayout();
            this.Special_Days.ResumeLayout(false);
            this.Special_Days.PerformLayout();
            this.specialDays.ResumeLayout(false);
            this.specialDays.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl Profiles;
        private System.Windows.Forms.TabPage Calendar_Page;
        private System.Windows.Forms.TextBox txt_CalendarName;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_WriteToDatabaseTariffication;
        private System.Windows.Forms.GroupBox gpActivationMode;
        private System.Windows.Forms.RadioButton rdbActivateOnDate;
        private System.Windows.Forms.RadioButton rdbInvokeAction;
        private System.Windows.Forms.DateTimePicker dtc_CalendarActivationDate;
        private System.Windows.Forms.TabPage Day_Profile;
        private System.Windows.Forms.GroupBox gpDayProfileSettings;
        private System.Windows.Forms.Panel panel_numberofslots;
        private System.Windows.Forms.ComboBox combo_tariff_NumberofSlots;
        private System.Windows.Forms.Label label71;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_Save_DayProfile;
        private System.Windows.Forms.Panel panel_dayslot6;
        private System.Windows.Forms.DateTimePicker txt_DayProfile_s6;
        public System.Windows.Forms.Panel panel15;
        public System.Windows.Forms.RadioButton radio_DayProfile_s6t2;
        public System.Windows.Forms.RadioButton radio_DayProfile_s6t1;
        public System.Windows.Forms.RadioButton radio_DayProfile_s6t3;
        public System.Windows.Forms.RadioButton radio_DayProfile_s6t4;
        public System.Windows.Forms.Label lbl_Dp_Slot_s6;
        private System.Windows.Forms.Panel panel_dayslot1;
        public System.Windows.Forms.Label label73;
        private System.Windows.Forms.DateTimePicker txt_DayProfile_s1;
        public System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.RadioButton radio_DayProfile_s1t2;
        public System.Windows.Forms.RadioButton radio_DayProfile_s1t1;
        public System.Windows.Forms.RadioButton radio_DayProfile_s1t3;
        public System.Windows.Forms.RadioButton radio_DayProfile_s1t4;
        public System.Windows.Forms.Label label76;
        public System.Windows.Forms.Label label77;
        public System.Windows.Forms.Label label78;
        public System.Windows.Forms.Label label79;
        public System.Windows.Forms.Label lbl_Dp_Slot_s1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_AddDayProfile;
        private System.Windows.Forms.Panel panel_dayslot5;
        private System.Windows.Forms.DateTimePicker txt_DayProfile_s5;
        public System.Windows.Forms.Panel panel7;
        public System.Windows.Forms.RadioButton radio_DayProfile_s5t2;
        public System.Windows.Forms.RadioButton radio_DayProfile_s5t1;
        public System.Windows.Forms.RadioButton radio_DayProfile_s5t3;
        public System.Windows.Forms.RadioButton radio_DayProfile_s5t4;
        public System.Windows.Forms.Label lbl_Dp_Slot_s5;
        private System.Windows.Forms.Panel panel_dayslot2;
        private System.Windows.Forms.DateTimePicker txt_DayProfile_s2;
        public System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.RadioButton radio_DayProfile_s2t2;
        public System.Windows.Forms.RadioButton radio_DayProfile_s2t1;
        public System.Windows.Forms.RadioButton radio_DayProfile_s2t3;
        public System.Windows.Forms.RadioButton radio_DayProfile_s2t4;
        public System.Windows.Forms.Label lbl_Dp_Slot_s2;
        private System.Windows.Forms.Panel panel_dayslot4;
        private System.Windows.Forms.DateTimePicker txt_DayProfile_s4;
        public System.Windows.Forms.Panel panel6;
        public System.Windows.Forms.RadioButton radio_DayProfile_s4t2;
        public System.Windows.Forms.RadioButton radio_DayProfile_s4t1;
        public System.Windows.Forms.RadioButton radio_DayProfile_s4t3;
        public System.Windows.Forms.RadioButton radio_DayProfile_s4t4;
        public System.Windows.Forms.Label lbl_Dp_Slot_s4;
        private System.Windows.Forms.Panel panel_dayslot3;
        private System.Windows.Forms.DateTimePicker txt_DayProfile_s3;
        public System.Windows.Forms.Panel panel5;
        public System.Windows.Forms.RadioButton radio_DayProfile_s3t2;
        public System.Windows.Forms.RadioButton radio_DayProfile_s3t1;
        public System.Windows.Forms.RadioButton radio_DayProfile_s3t3;
        public System.Windows.Forms.RadioButton radio_DayProfile_s3t4;
        public System.Windows.Forms.Label lbl_Dp_Slot_s3;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_delete_DayProfile;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_newDayProfile;
        private ComponentFactory.Krypton.Toolkit.KryptonListBox list_DayProfile;
        private System.Windows.Forms.TabPage Week_Profile;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_Delete_WeekProfile;
        private System.Windows.Forms.GroupBox groupBox_WeekProfile;
        private System.Windows.Forms.ComboBox combo_day_fri;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_Save_WeekProfile;
        private System.Windows.Forms.ComboBox combo_day_Mon;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox combo_day_wed;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox combo_day_tue;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox combo_day_thu;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox combo_day_sat;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox combo_day_sun;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lbl_day;
        private System.Windows.Forms.Button btn_weekProfile_ADD;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_newWeekProfile;
        private ComponentFactory.Krypton.Toolkit.KryptonListBox list_WeekProfile;
        private System.Windows.Forms.TabPage _SeasonProfile;
        private ComponentFactory.Krypton.Toolkit.KryptonListBox list_SeasonProfile;
        private System.Windows.Forms.GroupBox groupBox_SeasonProfile;
        private System.Windows.Forms.ComboBox combo_SeasonName;
        private System.Windows.Forms.Label label157;
        private System.Windows.Forms.Label label158;
        private System.Windows.Forms.Label label86;
        private System.Windows.Forms.ComboBox combo_season_weekProfile1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_DeleteSeasonProfile;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_addSeasonProfile;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_Save_SeasonProfile;
        private System.Windows.Forms.TabPage Special_Days;
        private System.Windows.Forms.GroupBox specialDays;
        private System.Windows.Forms.Label lbl_SpDay_DayProfileID;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_SpecialDays_add;
        private System.Windows.Forms.ComboBox combo_SpecialDay_ProfileID;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_Save_SpecialDay;
        private ComponentFactory.Krypton.Toolkit.KryptonListBox list_SpecialDays;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_Delete_SpecialDay;
        private System.Windows.Forms.GroupBox gp_Activate;
        private System.Windows.Forms.RadioButton rdb_Disable;
        private System.Windows.Forms.RadioButton rdb_Enable;
        private System.Windows.Forms.Label lbl_calendarName;
        internal System.Windows.Forms.Button btn_GetActivityCalendar;
        internal System.Windows.Forms.Button btn_setActivityCalendar;
        private ucDateTimeChooser.ucCustomDateTimePicker dtc_SeasonProfile_;
        private System.Windows.Forms.Label lbl_Format;
        private System.Windows.Forms.Label lbl_SpDay_StartDate;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label lbl_ErrorTOD;
        private System.Windows.Forms.Label lbl_ErrorDayProfile;
        private System.Windows.Forms.Label lbl_ErrorWeekProfile;
        private System.Windows.Forms.Label lbl_ErrorSeasonProfiles;
        private System.Windows.Forms.Label lbl_ErrorSpecialDays;
        private ucSPDayProfileDateTime ucSPDayProfileDateTime;
        internal System.Windows.Forms.Button btnGetDayProfile;
        internal System.Windows.Forms.Button btnSetDayProfile;
        internal System.Windows.Forms.Button btnGetWeekProfile;
        internal System.Windows.Forms.Button btnSetWeekProfile;
        internal System.Windows.Forms.Button btnGetSeasonProfile;
        internal System.Windows.Forms.Button btnSetSeasonProfile;
        internal System.Windows.Forms.Button btnGetSpecialDays;
        internal System.Windows.Forms.Button btnSetSpecialDays;
    }
}
