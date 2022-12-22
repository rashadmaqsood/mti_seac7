using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DLMS;
using datetime;
using SmartEyeControl_7.ApplicationGUI.ucCustomControl;
using ucDateTimeChooser;
using DLMS.Comm;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.Param;
using SharedCode.Common;

namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    public partial class ucTimeWindowParam : UserControl
    {
        public const string DisableStr = "Disable";
        public const string DateTimeStr = "DateTime";
        public const string TimeIntervalStr = "Time Interval";
        public const string TimeIntervalSinkStr = "Time Interval[Sink]";
        public const string TimeIntervalFixedStr = "Time Interval[Fixed]";

        public const string Access_Error_Format = "Control Mode {0} \r\nInsufficient Privilege To View Details";

        internal static readonly StDateTime Interval_Val;
        internal static readonly StDateTime DateTime_Val;

        private Param_TimeBaseEvents _TBE;
        private int _TBE_Mode = Tb_Interval;
        //****************************************************************************
        public const int Tb_Disable = 0;
        public const int Tb_DateTime = 1;
        public const int Tb_Interval = 2;
        public const int Tb_IntervalTimeSink = 3;
        public const int Tb_Fixed = 4;
        //*****************************************************************************
        
        #region Properties

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Param_TimeBaseEvents Param_TimeBaseEvent
        {
            get { return _TBE; }
            set { _TBE = value; }
        }

        public int TBE_Mode
        {
            get { return _TBE_Mode; }
            private set { _TBE_Mode = value; }
        }

        public string TimeWindowTitle
        {
            get
            {
                return gpTimeWindow.Text;
            }
            set
            {
                gpTimeWindow.Text = value;
            }
        }

        public bool IsValidated
        {
            get
            {
                String ErrorMessage = errorProvider.GetError(this);
                if (!String.IsNullOrEmpty(ErrorMessage) ||
                    !String.IsNullOrWhiteSpace(ErrorMessage) || !DTC_TBE.IsValidated)
                    return false;
                else
                    return true;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<AccessRights> AccessRights 
        { 
            get; 
            set; 
        }

        #endregion

        static ucTimeWindowParam()
        {
            Interval_Val = new StDateTime();
            Interval_Val.Kind = StDateTime.DateTimeType.Time;
            DLMS.Comm.StDateTime.StDateTimeHelper.Save_WildCards(new List<StDateTimeWildCards>() {StDateTimeWildCards.NullYear,StDateTimeWildCards.NullMonth,
                StDateTimeWildCards.NullDay,StDateTimeWildCards.NullDayOfWeek,StDateTimeWildCards.NullGMT }, Interval_Val);

            DateTime_Val = new StDateTime();
            DateTime_Val.Kind = StDateTime.DateTimeType.DateTime;
            DLMS.Comm.StDateTime.StDateTimeHelper.Save_WildCards(new List<StDateTimeWildCards>() { StDateTimeWildCards.NullGMT }, DateTime_Val);
        }

        public ucTimeWindowParam()
        {
            InitializeComponent();
            #region //Initialize Access Rights here

            this.AccessRights = ApplicationRight.GetDefaultAccessRightByEnum(typeof(MeterScheduling));
            foreach (var item in AccessRights)
            {
                item.Read = item.Write = true;
            }

            #endregion
            if (_TBE == null)
                _TBE = new Param_TimeBaseEvents();
            if (DTC_TBE != null)
            {
                DTC_TBE.ContentControl.ValueCustom = DateTime_Val;
                DTC_TBE.Visible = true;
                DTC_TBE.Enabled = true;
            }
            combo_control_TBE.SelectedItem = TimeIntervalStr;
            ucCustomDateTime.StDateTime_Value = DateTime_Val;

            ApplyVisual_ControlMode(TimeIntervalStr);
        }

        #region Event_Handlers

        private void combo_control_TBE1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isWrite = false;
            //bool isRead = false;

            //_TBE.Control_Enum = (byte)((combo_control_TBE_.SelectedIndex != -1) ? combo_control_TBE_.SelectedIndex : 0);
            //_TBE.Control_Enum = (byte)((combo_control_TBE.SelectedIndex != -1) ? combo_control_TBE.SelectedIndex : 0);
            string Control_Enum_Str = (combo_control_TBE.SelectedIndex != -1) ? (string)combo_control_TBE.SelectedItem : null;

            ShowDTC_TBE(false);
            if (!String.IsNullOrEmpty(Control_Enum_Str))
            {
                if (Control_Enum_Str.Equals(DisableStr, StringComparison.OrdinalIgnoreCase))
                {
                    isWrite = IsControlWTEnable(MeterScheduling.Disable);
                    //isRead = IsControlRDEnable(MeterScheduling.DateTime);
                    if (isWrite)
                        _TBE.Control_Enum = Tb_Disable;

                    ApplyVisual_ControlMode(DisableStr);
                }
                else if (Control_Enum_Str.Equals(DateTimeStr, StringComparison.OrdinalIgnoreCase))
                {
                    isWrite = IsControlWTEnable(MeterScheduling.DateTime);
                    //isRead = IsControlRDEnable(MeterScheduling.DateTime);
                    if (isWrite)
                        _TBE.Control_Enum = Tb_DateTime;

                    ApplyVisual_ControlMode(DateTimeStr);
                }
                else if (Control_Enum_Str.Equals(TimeIntervalStr, StringComparison.OrdinalIgnoreCase))
                {
                    isWrite = IsControlWTEnable(MeterScheduling.TimeInterval);
                    //isRead = IsControlRDEnable(MeterScheduling.TimeInterval);
                    if (isWrite)
                        _TBE.Control_Enum = Tb_Interval;

                    ApplyVisual_ControlMode(TimeIntervalStr);
                }
                else if (Control_Enum_Str.Equals(TimeIntervalSinkStr, StringComparison.OrdinalIgnoreCase))
                {
                    isWrite = IsControlWTEnable(MeterScheduling.TimeInterval_Sink);
                    //isRead = IsControlRDEnable(MeterScheduling.TimeInterval_Sink);
                    if (isWrite)
                        _TBE.Control_Enum = Tb_IntervalTimeSink;

                    ApplyVisual_ControlMode(TimeIntervalSinkStr);
                }
                else if (Control_Enum_Str.Equals(TimeIntervalFixedStr, StringComparison.OrdinalIgnoreCase))
                {
                    isWrite = IsControlWTEnable(MeterScheduling.TimeInterval_Fixed);
                    //isRead = IsControlRDEnable(MeterScheduling.TimeInterval_Fixed);
                    if (isWrite)
                        _TBE.Control_Enum = Tb_Fixed;

                    ApplyVisual_ControlMode(TimeIntervalFixedStr);
                }
                else
                {
                    ApplyVisual_ControlMode(DisableStr);
                }
            }
            else
            {
                ApplyVisual_ControlMode(DisableStr);
            }
            Application.DoEvents();
        }

        private void DTC_TBE1_Load(object sender, EventArgs e)
        {
            Application.DoEvents();
            if (ucCustomDateTime != null)
            {
                ucCustomDateTime.dtc_Date.ContentControl.ValueChanged += new EventHandler(ContentControl_ValueChanged);
                ucCustomDateTime.dtc_Date.ContentControl.ValueChanged += new EventHandler(ContentControl_ValueChanged);
                ucCustomDateTime.dateTimeChooser.ValueCustomChanged += new EventHandler(dateTimeChooser_ValueCustomChanged);
            }
        }

        #endregion

        internal void SaveTimeWindowParam()
        {
            ushort tbe_min = 0;
            ushort tbe_sec = 0;

            _TBE.Control_Enum = (byte)((combo_control_TBE.SelectedIndex != -1) ? combo_control_TBE.SelectedIndex : 0);
            if (_TBE.Control_Enum.Equals(Tb_DateTime)) //dateTime selected
            {
                //Set Default Max Value
                _TBE.Interval = 64800;
                StDateTime dtDateTime = null;
                
                dtDateTime = new StDateTime(DTC_TBE.ContentControl.ValueCustom);

                //Store dtDateTime from ucCustomControl
                ///dtDateTime = new StDateTime(ucCustomDateTime.StDateTime_Value);
                dtDateTime.Kind = StDateTime.DateTimeType.DateTime;
                
                ///Set Repeat Daily Options On Date
                dtDateTime.Year = StDateTime.NullYear;
                dtDateTime.Month = StDateTime.Null;
                dtDateTime.DayOfMonth = StDateTime.Null;
                dtDateTime.DayOfWeek = StDateTime.Null;

                dtDateTime.UTCOffset = StDateTime.NullUTCOffset;
                dtDateTime.ClockStatus = StDateTime.Null;

                //Validate dtDateTime
                if(true) //(dtDateTime.IsDateTimeValid && dtDateTime.IsValid)
                {
                    _TBE.DateTime = dtDateTime;
                    
                    /// Show DateTime
                    Console.Out.WriteLine(String.Format("Debug:ucTimeWindowParam CustomDateTime {0}", dtDateTime));
                }
                else
                {
                    throw new Exception("DateTime entered is INVALID. Year, Month and DayofMonth can not be Wild card entries");
                }
            }
            else if (_TBE.Control_Enum.Equals(Tb_Interval))
            {
                //Limit of 18h required
                DateTime DateTimeVal = DTC_TBE.ContentControl.Value;
                int value = (int)(DateTimeVal.Hour * 60 * 60 + DateTimeVal.Minute * 60 + DateTimeVal.Second);
                if (value <= 64800) //18h x60x60
                    _TBE.Interval = Convert.ToUInt16(value);
                else
                {
                    _TBE.Interval = 64800;//set max value
                    throw new Exception("Interval value should be less than or equal to 18 hours");
                }
            }
            else if (_TBE.Control_Enum.Equals(Tb_IntervalTimeSink) || _TBE.Control_Enum.Equals(Tb_Fixed))//fixed
            {
                DateTime DateTimeVal = DTC_TBE.ContentControl.Value;
                tbe_min = (ushort)(DateTimeVal.Minute << 8);
                tbe_sec = (ushort)(DateTimeVal.Second);
                _TBE.Interval = (ushort)(tbe_min + tbe_sec);
            }
            else if (_TBE.Control_Enum.Equals(Tb_Disable)) //disabled
            {
                _TBE.Interval = 64800;
                _TBE.DateTime = DateTime_Val;
            }
        }

        void dateTimeChooser_ValueCustomChanged(object sender, EventArgs e)
        {
            try
            {
                SaveTimeWindowParam();

            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error Save DateTime for Param_TimeBaseEvents", ex.Message,
                   5000, Notification.Sounds.Hand);
            }
        }

        void ContentControl_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                SaveTimeWindowParam();

            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error Save DateTime for Param_TimeBaseEvents", ex.Message,
                   5000, Notification.Sounds.Hand);
            }
        }

        private bool isDateTimeValid(SharedCode.Comm.HelperClasses.DateTimeChooser Dt)
        {
            //if (Dt.Year == StDateTime.NullYear && Dt.Month == StDateTime.Null && Dt.Date == StDateTime.Null && Dt.DayOfWeek == StDateTime.Null)
            //{
            //    return false;
            //}
            if (Dt.Year != StDateTime.NullYear && Dt.Month != StDateTime.Null
                && Dt.Month != StDateTime.DaylightSavingBegin && Dt.Month != StDateTime.DaylightSavingEnd &&
                Dt.Date != StDateTime.LastDayOfMonth && Dt.Date != StDateTime.SecondLastDayOfMonth && Dt.Date != StDateTime.Null)
            {
                DateTime actualDate = new DateTime(Dt.Year, Dt.Month, Dt.Date);

                if (Dt.DayOfWeek == StDateTime.Null)
                {
                    return true;
                }
                else
                {
                    switch (Dt.DayOfWeek)
                    {
                        case 1:
                            if (actualDate.DayOfWeek == DayOfWeek.Monday)
                                return true;
                            else return false;
                        case 2:
                            if (actualDate.DayOfWeek == DayOfWeek.Tuesday)
                                return true;
                            else return false;
                        case 3:
                            if (actualDate.DayOfWeek == DayOfWeek.Wednesday)
                                return true;
                            else return false;
                        case 4:
                            if (actualDate.DayOfWeek == DayOfWeek.Thursday)
                                return true;
                            else return false;
                        case 5:
                            if (actualDate.DayOfWeek == DayOfWeek.Friday)
                                return true;
                            else return false;
                        case 6:
                            if (actualDate.DayOfWeek == DayOfWeek.Saturday)
                                return true;
                            else return false;
                        case 7:
                            if (actualDate.DayOfWeek == DayOfWeek.Sunday)
                                return true;
                            else return false;
                    }
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        public void ShowtoGUI_TBE()
        {
            try
            {
                ushort tbe_min = 0;
                ushort tbe_sec = 0;
                DateTime temp = DateTime.Now.Date;
                StDateTime localDateTime = new StDateTime();
                //combo_control_TBE.SelectedIndex = _TBE.Control_Enum;
                #region Update_combo_Control
                combo_control_TBE.SelectedIndex = -1;
                switch (_TBE.Control_Enum)
                {
                    case Tb_Disable:
                        combo_control_TBE.SelectedItem = DisableStr;
                        break;
                    case Tb_DateTime:
                        combo_control_TBE.SelectedItem = DateTimeStr;
                        break;
                    case Tb_Interval:
                        combo_control_TBE.SelectedItem = TimeIntervalStr;
                        break;
                    case Tb_IntervalTimeSink:
                        combo_control_TBE.SelectedItem = TimeIntervalSinkStr;
                        break;
                    case Tb_Fixed:
                        combo_control_TBE.SelectedItem = TimeIntervalFixedStr;
                        break;
                    default:
                        combo_control_TBE.SelectedIndex = -1;
                        break;
                }
                #endregion
                if (_TBE.Control_Enum.Equals(Tb_Disable))
                {
                    ApplyVisual_ControlMode(DisableStr);
                }
                else if (_TBE.Control_Enum.Equals(Tb_DateTime))
                {
                    ApplyVisual_ControlMode(DateTimeStr);

                    _TBE.DateTime.Kind = StDateTime.DateTimeType.DateTime;
                    DTC_TBE.ContentControl.ValueCustom = _TBE.DateTime;
                    //DTC_TBE.ContentControl.Value = _TBE.DateTime.GetDateTime();

                    //ucCustomDateTime.StDateTime_Value = _TBE.DateTime;
                    //ucCustomDateTime.showToGUI_StDateTime(_TBE.DateTime);
                }
                else if (_TBE.Control_Enum.Equals(Tb_Interval))
                {
                    temp = temp.AddSeconds(_TBE.Interval);

                    ApplyVisual_ControlMode(TimeIntervalStr);
                    localDateTime.Kind = StDateTime.DateTimeType.Time;
                    StDateTime.StDateTimeHelper.Save_WildCards(new List<StDateTimeWildCards>() { StDateTimeWildCards.NullGMT }, localDateTime);
                    localDateTime.Kind = StDateTime.DateTimeType.Time;
                    StDateTime.StDateTimeHelper.SetTime(temp.TimeOfDay, localDateTime);
                    DTC_TBE.ContentControl.Value = temp;
                    DTC_TBE.ContentControl.ValueCustom = localDateTime;
                }
                else
                {
                    tbe_sec = (ushort)(_TBE.Interval & 0x00FF);
                    tbe_min = (ushort)((_TBE.Interval & 0xFF00) >> 08);
                    temp = temp.AddSeconds(tbe_sec + tbe_min * 60);

                    if (_TBE.Control_Enum.Equals(Tb_IntervalTimeSink))
                    {
                        ApplyVisual_ControlMode(TimeIntervalSinkStr);
                        localDateTime.Kind = StDateTime.DateTimeType.Time;
                        StDateTime.StDateTimeHelper.Save_WildCards(new List<StDateTimeWildCards>() { StDateTimeWildCards.NullGMT,
                                                                                                     StDateTimeWildCards.NullHour }, localDateTime);
                        localDateTime.Kind = StDateTime.DateTimeType.Time;
                        StDateTime.StDateTimeHelper.SetTime(temp.TimeOfDay, localDateTime);
                        DTC_TBE.ContentControl.Value = temp;
                        DTC_TBE.ContentControl.ValueCustom = localDateTime;
                    }
                    else if (_TBE.Control_Enum.Equals(Tb_Fixed))
                    {
                        ApplyVisual_ControlMode(TimeIntervalFixedStr);
                        localDateTime.Kind = StDateTime.DateTimeType.Time;
                        StDateTime.StDateTimeHelper.Save_WildCards(new List<StDateTimeWildCards>() { StDateTimeWildCards.NullGMT, 
                                                                                                     StDateTimeWildCards.NullHour}, localDateTime);
                        localDateTime.Kind = StDateTime.DateTimeType.Time;
                        StDateTime.StDateTimeHelper.SetTime(temp.TimeOfDay, localDateTime);
                        DTC_TBE.ContentControl.Value = temp;
                        DTC_TBE.ContentControl.ValueCustom = localDateTime;
                    }
                    else
                    {
                        ApplyVisual_ControlMode(DisableStr);
                    }
                }
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error Display Time Window Parameter", ex.Message, 2500, Notification.Sounds.Exclamation);
            }
        }

        private void ShowDTC_TBE(bool enable)
        {
            lbl_Format.Visible = enable;
            lbl_heading.Visible = enable;
        }

        private void ucTimeWindowParam_Load(object sender, EventArgs e)
        {

        }

        private void ucTimeWindowParam_Leave(object sender, EventArgs e)
        {
            try
            {
                SaveTimeWindowParam();
                bool isValidated = false;
                String ErrorMessage = String.Empty;
                #region Validate_Param_TimeBase_Event

                isValidated = App_Validation.Validate_Param_TimeBase_Event(_TBE, ref ErrorMessage);
                if (!isValidated)
                    App_Validation.Apply_ValidationResult(isValidated, ErrorMessage, this, ref errorProvider);
                else
                    App_Validation.Apply_ValidationResult(isValidated, String.Empty, this, ref errorProvider);

                #endregion
            }
            catch (Exception ex)
            {
                String ErrorMessage = String.Format("DateTime Entered INVALID,{0}", ex.Message);
                Notification notifier = new Notification("DateTime entered is INVALID", ex.Message, 1500);
                App_Validation.Apply_ValidationResult(false, ErrorMessage, this, ref errorProvider);
            }
        }

        private void ApplyVisual_ControlMode(String SelectionStr)
        {
            bool isWrite = false;
            bool isRead = false;
            try
            {
                this.SuspendLayout();
                this.fLP_Main.SuspendLayout();

                lbl_Format.Enabled = lbl_Format.Visible = true;
                lbl_heading.Enabled = lbl_heading.Visible = true;
                DTC_TBE.Enabled = DTC_TBE.Visible = true;
                fLP_DTC_TBE.Visible = true;
                lbl_Access_Error.Visible = false;
                
                DTC_TBE.ShowButtons = true;
                DTC_TBE.Size = new Size(100, 22);

                DTC_TBE.ShowButtons = true;
                DTC_TBE.ShowUpDownButton = false;
                DTC_TBE.ShowWildCardWinButton = false;
                //Reset Min_Max DateTime here
                DTC_TBE.ContentControl.MinDate = ucCustomDateTimePicker.MinDateAllowed;
                DTC_TBE.ContentControl.MaxDate = ucCustomDateTimePicker.MaxDateAllowed;
                //default Setting for ucCustomDateTime
                
                //ucCustomDateTime.Enabled = ucCustomDateTime.Visible = false;
                
                //Control Mode Disable
                if (DisableStr.Equals(SelectionStr, StringComparison.OrdinalIgnoreCase))
                {
                    lbl_Format.Enabled = lbl_Format.Visible = false;
                    lbl_heading.Enabled = lbl_heading.Visible = false;
                    DTC_TBE.Enabled = DTC_TBE.Visible = false;
                }
                //Control Mode DateTime
                else if (DateTimeStr.Equals(SelectionStr, StringComparison.OrdinalIgnoreCase))
                {
                    lbl_heading.Text = "Repeat Daily";
                    lbl_Format.Text = "(Hour:Minute:Second)";
                    DTC_TBE.FormatEx = dtpCustomExtensions.dtpLongTime;
                    DTC_TBE.ShowUpDownButton = true;

                    DTC_TBE.Size = new Size(120, 22);
                    DTC_TBE.ContentControl.ValueCustom =  Interval_Val;
                    #region //Apply Access Rights here

                    isWrite = IsControlWTEnable(MeterScheduling.DateTime);
                    isRead = IsControlRDEnable(MeterScheduling.DateTime);

                    if (!isWrite && isRead)
                    {
                        ucCustomDateTime.Enabled = false;
                        DTC_TBE.Enabled = false;
                    }
                    else if (!isRead)
                    {
                        ucCustomDateTime.Enabled = ucCustomDateTime.Visible = false;
                        lbl_heading.Enabled = lbl_heading.Visible = false;

                        lbl_Access_Error.Text = String.Format(Access_Error_Format, DateTimeStr);
                        lbl_Access_Error.Visible = true;

                        DTC_TBE.Enabled = DTC_TBE.Visible = false;
                        fLP_DTC_TBE.Visible = false;
                    }


                    #endregion

                    //lbl_heading.Text = "Start DateTime";
                    //lbl_Format.Text = "(DayOfWeek, DayOfMonth Month Year HH:MM:SS)";
                    
                    //DTC_TBE.FormatEx = dtpCustomExtensions.dtpLongDateTimeWildCard;
                    //DTC_TBE.ShowWildCardWinButton = true;

                    //DTC_TBE.Size = new Size(270, 22);
                    ////if (_TBE.DateTime.IsValid)
                    //DTC_TBE.ContentControl.ValueCustom = _TBE.DateTime;
                    ////else
                    //// DTC_TBE.ContentControl.ValueCustom = DateTime_Val;
                    //DTC_TBE.Enabled = DTC_TBE.Visible = false;
                    //lbl_Format.Enabled = lbl_Format.Visible = false;
                    //enable ucCustomDateTime Control
                    ////ucCustomDateTime.Enabled = ucCustomDateTime.Visible = true;

                    #region //Apply Access Rights here

                    ///Commented Code Section For DataTime Control Mode
                    ///
                    //isWrite = IsControlWTEnable(MeterScheduling.DateTime);
                    //isRead = IsControlRDEnable(MeterScheduling.DateTime);

                    //if (!isWrite && isRead)
                    //{
                    //    ucCustomDateTime.Enabled = false;
                    //}
                    //else if (!isRead)
                    //{
                    //    ucCustomDateTime.Enabled = ucCustomDateTime.Visible = false;
                    //    lbl_heading.Enabled = lbl_heading.Visible = false;

                    //    lbl_Access_Error.Text = String.Format(Access_Error_Format, DateTimeStr);
                    //    lbl_Access_Error.Visible = true;
                    //}

                    isWrite = IsControlWTEnable(MeterScheduling.DateTime);
                    isRead = IsControlRDEnable(MeterScheduling.DateTime);

                    if (!isWrite && isRead)
                    {
                        ucCustomDateTime.Enabled = false;
                        DTC_TBE.Enabled = false;
                    }
                    else if (!isRead)
                    {
                        ucCustomDateTime.Enabled = ucCustomDateTime.Visible = false;
                        lbl_heading.Enabled = lbl_heading.Visible = false;

                        lbl_Access_Error.Text = String.Format(Access_Error_Format, DateTimeStr);
                        lbl_Access_Error.Visible = true;

                        DTC_TBE.Enabled = DTC_TBE.Visible = false;
                        fLP_DTC_TBE.Visible = false;
                    }


                    #endregion
                }
                //Control Mode Time Interval
                else if (TimeIntervalStr.Equals(SelectionStr, StringComparison.OrdinalIgnoreCase))
                {
                    lbl_heading.Text = "Time Interval";
                    lbl_Format.Text = "(Hour:Minute:Second)";
                    DTC_TBE.FormatEx = dtpCustomExtensions.dtpLongTime;
                    DTC_TBE.ShowUpDownButton = true;

                    ///Max DateTimeCompute
                    DateTime minVal = DateTime.Now.Date;
                    DateTime maxVal = minVal.AddSeconds(18 * 60 * 60);
                    DTC_TBE.ContentControl.MinDate = minVal;
                    DTC_TBE.ContentControl.MaxDate = maxVal;
                    DTC_TBE.Size = new Size(120, 22);
                    DTC_TBE.ContentControl.ValueCustom = Interval_Val;
                    #region //Apply Access Rights here

                    isWrite = IsControlWTEnable(MeterScheduling.TimeInterval);
                    isRead = IsControlRDEnable(MeterScheduling.TimeInterval);

                    if (!isWrite && isRead)
                    {
                        DTC_TBE.Enabled = false;
                    }
                    else if (!isRead)
                    {
                        DTC_TBE.Enabled = DTC_TBE.Visible = false;
                        fLP_DTC_TBE.Visible = false;
                        lbl_heading.Enabled = lbl_heading.Visible = false;

                        lbl_Access_Error.Text = String.Format(Access_Error_Format, TimeIntervalStr);
                        lbl_Access_Error.Visible = true;
                    }

                    #endregion
                }
                //Control Mode Time Interval Sink
                else if (TimeIntervalSinkStr.Equals(SelectionStr, StringComparison.OrdinalIgnoreCase))
                {
                    lbl_heading.Text = "Time Interval";
                    lbl_Format.Text = "(Minute:Second)";
                    DTC_TBE.FormatEx = dtpCustomExtensions.dtpShortIntervalTimeSink;
                    DTC_TBE.ShowUpDownButton = true;

                    DTC_TBE.Size = new Size(120, 22);
                    DTC_TBE.ContentControl.ValueCustom = Interval_Val;
                    #region //Apply Access Rights here

                    isWrite = IsControlWTEnable(MeterScheduling.TimeInterval_Sink);
                    isRead = IsControlRDEnable(MeterScheduling.TimeInterval_Sink);

                    if (!isWrite && isRead)
                    {
                        DTC_TBE.Enabled = false;
                    }
                    else if (!isRead)
                    {
                        DTC_TBE.Enabled = DTC_TBE.Visible = false;
                        fLP_DTC_TBE.Visible = false;
                        lbl_heading.Enabled = lbl_heading.Visible = false;

                        lbl_Access_Error.Text = String.Format(Access_Error_Format, TimeIntervalSinkStr);
                        lbl_Access_Error.Visible = true;
                    }

                    #endregion
                }
                //Control Mode Time Interval Fixed
                else if (TimeIntervalFixedStr.Equals(SelectionStr, StringComparison.OrdinalIgnoreCase))
                {
                    lbl_heading.Text = "Time Interval";
                    lbl_Format.Text = "(Minute:Second)";
                    DTC_TBE.FormatEx = dtpCustomExtensions.dtpShortIntervalFixed;
                    DTC_TBE.ShowUpDownButton = true;

                    DTC_TBE.Size = new Size(120, 22);
                    DTC_TBE.ContentControl.ValueCustom = Interval_Val;
                    #region //Apply Access Rights here

                    isWrite = IsControlWTEnable(MeterScheduling.TimeInterval_Fixed);
                    isRead = IsControlRDEnable(MeterScheduling.TimeInterval_Fixed);

                    if (!isWrite && isRead)
                    {
                        DTC_TBE.Enabled = false;
                    }
                    else if (!isRead)
                    {
                        DTC_TBE.Enabled = DTC_TBE.Visible = false;
                        fLP_DTC_TBE.Visible = false;
                        lbl_heading.Enabled = lbl_heading.Visible = false;

                        lbl_Access_Error.Text = String.Format(Access_Error_Format, TimeIntervalFixedStr);
                        lbl_Access_Error.Visible = true;
                    }

                    #endregion
                }
                //Invalid Control Mode Selection
                else
                {
                    lbl_Format.Enabled = lbl_Format.Visible = false;
                    lbl_heading.Enabled = lbl_heading.Visible = false;
                    DTC_TBE.Enabled = DTC_TBE.Visible = false;

                    lbl_Access_Error.Text = String.Format(Access_Error_Format, DisableStr);
                    lbl_Access_Error.Visible = true;
                }
            }
            catch
            {
                Notification notifier = new Notification("ApplyVisual ControlMode Error", "Unable to apply appropriate Control Mode", 2500);
            }
            finally
            {
                this.fLP_Main.ResumeLayout();
                this.ResumeLayout();
            }
        }

        #region AccessControlMethods

        public bool ApplyAccessRights(List<AccessRights> Rights)
        {
            try
            {
                AccessRights = Rights;
                this.SuspendLayout();
                this.fLP_Main.SuspendLayout();
                #region Disable Time base Event

                //4.8.16
                //if (!String.IsNullOrEmpty(TimeWindowTitle))
                //{
                //    if (TimeWindowTitle.EndsWith("1", StringComparison.OrdinalIgnoreCase))
                //    {
                //        if (!IsControlRDEnable(MeterScheduling.TimeBaseEvent1))
                //        {
                //            this.Visible = false;
                //            return false;
                //        }
                //        else
                //            this.Visible = true;
                //    }
                //    else if (TimeWindowTitle.EndsWith("2", StringComparison.OrdinalIgnoreCase))
                //    {
                //        if (!IsControlRDEnable(MeterScheduling.TimeBaseEvent2))
                //        {
                //            this.Visible = false;
                //            return false;
                //        }
                //        else
                //            this.Visible = true;
                //    }
                //}

                #endregion

                if (Rights.Find(x => x.Read == true || x.Write == true) != null)
                {
                    #region Initialize Combo_control_TBE

                    combo_control_TBE.Items.Clear();
                    if (IsControlRDEnable(MeterScheduling.Disable) || IsControlWTEnable(MeterScheduling.Disable))
                        combo_control_TBE.Items.Add(DisableStr);
                    if (IsControlRDEnable(MeterScheduling.DateTime) || IsControlWTEnable(MeterScheduling.DateTime))
                        combo_control_TBE.Items.Add(DateTimeStr);
                    if (IsControlRDEnable(MeterScheduling.TimeInterval) || IsControlWTEnable(MeterScheduling.TimeInterval))
                        combo_control_TBE.Items.Add(TimeIntervalStr);
                    if (IsControlRDEnable(MeterScheduling.TimeInterval_Sink) || IsControlWTEnable(MeterScheduling.TimeInterval_Sink))
                        combo_control_TBE.Items.Add(TimeIntervalSinkStr);
                    if (IsControlRDEnable(MeterScheduling.TimeInterval_Fixed) || IsControlWTEnable(MeterScheduling.TimeInterval_Fixed))
                        combo_control_TBE.Items.Add(TimeIntervalFixedStr);

                    #endregion
                    ShowtoGUI_TBE();
                    return true;
                }
                return false;
            }
            finally
            {
                this.fLP_Main.ResumeLayout();
                this.ResumeLayout();
            }
        }

        #endregion

        #region Support_Fuction

        private bool IsControlWTEnable(MeterScheduling type)
        {
            bool isEnable = false;
            try
            {
                AccessRights right = AccessRights.Find((x) => (MeterScheduling)Enum.Parse(x.QuantityType, x.QuantityName) == type);
                if (right != null)
                    isEnable = right.Write;
            }
            catch { }
            return isEnable;
        }

        public bool IsControlRDEnable(MeterScheduling type)
        {
            bool isEnable = false;
            try
            {
                AccessRights right = AccessRights.Find((x) => (MeterScheduling)Enum.Parse(x.QuantityType, x.QuantityName) == type);
                if (right != null)
                    isEnable = right.Read;
            }
            catch { }
            return isEnable;
        }

        #endregion

        //Flickering Reduction
        protected override CreateParams CreateParams
        {
            get
            {
                var parms = base.CreateParams;
                parms.Style &= ~0x02000000;  // Turn off WS_CLIPCHILDREN
                return parms;
            }
        }
    }
}
