using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DLMS;
using DLMS.Comm;
using SmartEyeControl_7.ApplicationGUI.ucCustomControl;

namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    public partial class ucCustomDateTime : UserControl
    {
        internal static readonly StDateTime default_CurrentTime;
        internal static readonly StDateTime default_CurrenttDate;
        private StDateTime _StDateTime_Value = null;

        internal dtpCustomExtensions dtcCustomFormat = dtpCustomExtensions.dtpDayShortName;//default Initialize
        internal string Date_View = Weekly_View;
        //Fixed StDateTime View 
        public const String Yearly_View = "Repeat_Yearly";    //dtpMonthNameAndDay DayOfMonth,Month
        public const String Monthly_View = "Repeat_Monthly";  //dtpDayOfMonth DayOfMonth
        public const String Weekly_View = "Repeat_Weekly";    //dtpDayShortName DayOfWeek
        public const String Daily_View = "Repeat_Daily";      //dtpCustom Hide MDIResetDate
        public const String OnlyOnce_View = "Repeat_OnlyOnce";//dtpLong With_No_WildCard_Support
        public const String Advance_View = "AdvanceUser_View";//dtpLongDateWildCard With_WildCard_Support
        //Fixed Label For View
        public const String lbl_Yearly_View = "(Month DayOfMonth)";
        public const String lbl_Monthly_View = "(DayOfMonth)";
        public const String lbl_Weekly_View = "(DayOfWeek)";
        public const String lbl_Daily_View = "(NIL)";         //No Date To Choose 
        public const String lbl_OnlyOnce_View = "(DayOfWeek,DayOfMonth Month Year)";
        public const String lbl_Advance_View = "(DayOfWeek,DayOfMonth Month Year)";
        //Fixed CustomDateTimeChooser Formats
        public const dtpCustomExtensions format_Yearly_View = dtpCustomExtensions.dtpMonthNameAndDay;
        public const dtpCustomExtensions format_Monthly_View = dtpCustomExtensions.dtpDayOfMonth;
        public const dtpCustomExtensions format_Weekly_View = dtpCustomExtensions.dtpDayFullName;
        public const dtpCustomExtensions format_Daily_View = dtpCustomExtensions.dtpLongTime24Hour;
        public const dtpCustomExtensions format_OnlyOnce_View = dtpCustomExtensions.dtpLong;
        public const dtpCustomExtensions format_Advance_View = dtpCustomExtensions.dtpLongDateWildCard;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public StDateTime StDateTime_Value
        {
            get { return _StDateTime_Value; }
            set { _StDateTime_Value = value; }
        }

        public bool IsValidated
        {
            get
            {
                if (!dtc_Date.IsValidated)
                    return false;
                if (!dtc_Time.IsValidated)
                    return false;
                return true;
            }
        }

        public ucCustomDateTime()
        {
            InitializeComponent();
            try
            {
                if (dtc_Date != null)
                {
                    dtc_Date.ContentControl.ValueCustom = default_CurrenttDate;
                    dtc_Time.ContentControl.ValueCustom = default_CurrentTime;
                    _StDateTime_Value = Save_StDateTimeValue();

                    showToGUI_StDateTime(_StDateTime_Value);

                    dtc_Date.ContentControl.ValueCustomChanged += dtc_DateTime_ValueChanged;
                    dtc_Time.ContentControl.ValueCustomChanged += dtc_DateTime_ValueChanged;

                }
                if (dateTimeChooser != null)
                {
                    dateTimeChooser.DateTimeStruct = _StDateTime_Value;
                    dateTimeChooser.showDatetime();

                    dateTimeChooser.ValueCustomChanged += new EventHandler(dateTimeChooser_ValueCustomChanged);
                }
            }
            catch
            {
            }
        }

        static ucCustomDateTime()
        {
            try
            {
                default_CurrentTime = new StDateTime();
                default_CurrentTime.Kind = StDateTime.DateTimeType.Time;
                StDateTime.StDateTimeHelper.Save_WildCards(new List<StDateTimeWildCards>() 
            {
                StDateTimeWildCards.NullYear,StDateTimeWildCards.NullMonth,
                StDateTimeWildCards.NullDay,StDateTimeWildCards.NullDayOfWeek,StDateTimeWildCards.NullGMT
            }, default_CurrentTime);
                default_CurrenttDate = new StDateTime();
                default_CurrenttDate.Kind = StDateTime.DateTimeType.Date;
                StDateTime.StDateTimeHelper.Save_WildCards(new List<StDateTimeWildCards>() { StDateTimeWildCards.NullGMT }, default_CurrenttDate);
            }
            catch (Exception)
            {
                
                
            }
        }

        #region Local_Event_Handlers

        private StDateTime Save_StDateTimeValue()
        {
            StDateTime localDateTime = null;
            if (dtc_Date != null && dtc_Time != null)
            {
                localDateTime = new StDateTime(dtc_Date.ContentControl.ValueCustom);
                localDateTime.Kind = StDateTime.DateTimeType.DateTime;
                ///Initialize StDateTimeValue
                localDateTime.Hour = dtc_Time.ContentControl.ValueCustom.Hour;
                localDateTime.Minute = dtc_Time.ContentControl.ValueCustom.Minute;
                localDateTime.Second = dtc_Time.ContentControl.ValueCustom.Second;
                localDateTime.Hundred = dtc_Time.ContentControl.ValueCustom.Hundred;
                localDateTime.UTCOffset = dtc_Time.ContentControl.ValueCustom.UTCOffset;
            }
            return localDateTime;
        }

        private void ucCustomDateTime_Load(object sender, EventArgs e)
        {

            try
            {
                #region Set_ErrorMessage

                if (dtc_Date.ErrorMessage != null)
                {
                    dtc_Date.ErrorMessage = errorProvider;
                    dtc_Date.ErrorMessage.SetIconAlignment(dtc_Date, ErrorIconAlignment.MiddleLeft);
                    dtc_Date.ErrorMessage.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
                }
                if (dtc_Time.ErrorMessage != null)
                {
                    dtc_Time.ErrorMessage = errorProvider;
                    dtc_Time.ErrorMessage.SetIconAlignment(dtc_Time, ErrorIconAlignment.MiddleRight);
                    dtc_Time.ErrorMessage.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
                }
                if (errorProvider != null)
                {
                    errorProvider.SetIconAlignment(this, ErrorIconAlignment.MiddleLeft);
                    //errorProvider.SetIconAlignment(dtc_MDI_ResetDate, ErrorIconAlignment.MiddleLeft);
                    errorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
                }
            }
            catch (Exception)
            {
                
                
            }

            #endregion

            //Added by Azeem
            if (!rdb_Month.Checked) rdb_Month.Checked = true;
        }

        private void ucCustomDateTime_Leave(object sender, EventArgs e)
        {
            String ErrorMessage = String.Format("Error Validating StDateTime_Value,invalid DateTime Obj");
            try
            {
                if (!IsValidated)
                    errorProvider.SetError(this, ErrorMessage);
                else
                    errorProvider.SetError(this, String.Empty);
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error Save Param_MDI_parameters_object", ex.Message,
                    5000, Notification.Sounds.Hand);
                errorProvider.SetError(this, ErrorMessage);
            }
        }

        #endregion

        public void showToGUI_StDateTime(StDateTime StDateTimeLocal = null)
        {
            try
            {
                String StDateTimeLocalStr = String.Empty;
                if (StDateTimeLocal == null)
                    StDateTimeLocal = _StDateTime_Value;
                if (StDateTimeLocal == null)
                    throw new ArgumentNullException("StDateTimeLocal");
                String StDateTime_View = Advance_View;
                //Advance_View[Not AlreadyTaken]
                if (!String.Equals(Date_View, Advance_View))
                {
                    //OnlyOnce_View
                    try
                    {
                        StDateTimeLocalStr = StDateTimeLocal.ToString(format_OnlyOnce_View);
                        StDateTime_View = OnlyOnce_View;
                    }
                    catch
                    {
                        StDateTimeLocalStr = null;
                    }
                    //Yearly_View
                    try
                    {
                        if (String.IsNullOrEmpty(StDateTimeLocalStr))
                        {
                            StDateTimeLocalStr = StDateTimeLocal.ToString(format_Yearly_View);
                            StDateTime_View = Yearly_View;
                        }
                    }
                    catch
                    {
                        StDateTimeLocalStr = null;
                    }
                    //Monthly_View
                    try
                    {
                        if (String.IsNullOrEmpty(StDateTimeLocalStr))
                        {
                            StDateTimeLocalStr = StDateTimeLocal.ToString(format_Monthly_View);
                            StDateTime_View = Monthly_View;
                        }
                    }
                    catch
                    {
                        StDateTimeLocalStr = null;
                    }
                    //Weekly_View
                    try
                    {
                        if (String.IsNullOrEmpty(StDateTimeLocalStr))
                        {
                            StDateTimeLocalStr = StDateTimeLocal.ToString(format_Weekly_View);
                            StDateTime_View = Weekly_View;
                        }
                    }
                    catch
                    {
                        StDateTimeLocalStr = null;
                    }
                    //Daily_View
                    try
                    {
                        if (String.IsNullOrEmpty(StDateTimeLocalStr) &&
                            StDateTime.StDateTimeHelper.IsRepeatDailyFormat(StDateTimeLocal))
                        {
                            StDateTimeLocalStr = StDateTimeLocal.ToString(format_Daily_View);
                            StDateTime_View = Daily_View;
                        }
                    }
                    catch
                    {
                        StDateTimeLocalStr = null;
                    }
                }
                //Advance_View[Least Not Shown By Either]
                if (!String.Equals(Date_View, StDateTime_View))
                {
                    if (String.Equals(StDateTime_View, Advance_View))
                        rdb_Advance_View.Checked = true;
                    else if (String.Equals(StDateTime_View, OnlyOnce_View))
                        rdb_OnlyOnce.Checked = true;
                    else if (String.Equals(StDateTime_View, Daily_View))
                        rdb_Daily.Checked = true;
                    else if (String.Equals(StDateTime_View, Weekly_View))
                        rdb_Week.Checked = true;
                    else if (String.Equals(StDateTime_View, Monthly_View))
                        rdb_Month.Checked = true;
                    else if (String.Equals(StDateTime_View, Yearly_View))
                        rdb_Year.Checked = true;
                    //ApplyVisual_ControlMode(StDateTime_View);
                }
                //Show StDateTime
                StDateTimeLocal.Kind = StDateTime.DateTimeType.Date;
                dtc_Date.ContentControl.ValueCustom = StDateTimeLocal;
                //Show Time
                StDateTimeLocal.Kind = StDateTime.DateTimeType.Time;
                dtc_Time.ContentControl.ValueCustom = StDateTimeLocal;
                StDateTimeLocal.Kind = StDateTime.DateTimeType.DateTime;
                //Show StDateTime DateTimeChooser
                dateTimeChooser.DateTimeStruct = StDateTimeLocal;
                dateTimeChooser.showDatetime();
                //Save DateTime
                if (_StDateTime_Value != StDateTimeLocal)
                    _StDateTime_Value = new StDateTime(StDateTimeLocal);
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error show CustomDateTime", ex.Message,
                    5000, Notification.Sounds.Hand);
            }
        }

        private void rdb_DateView_CheckedChanged(object sender, EventArgs e)
        {
            string formatString = String.Empty;
            RadioButton rdb_Button = (RadioButton)sender;
            if (rdb_Year.Checked)
                formatString = Yearly_View;
            else if (rdb_Month.Checked)
                formatString = Monthly_View;
            else if (rdb_Week.Checked)
                formatString = Weekly_View;
            else if (rdb_Daily.Checked)
                formatString = Daily_View;
            else if (rdb_OnlyOnce.Checked)
                formatString = OnlyOnce_View;
            else if (rdb_Advance_View.Checked)
                formatString = Advance_View;
            ///Default View
            ApplyVisual_ControlMode(formatString);
        }

        private void ApplyVisual_ControlMode(String formatString)
        {
            try
            {
                Date_View = formatString;
                //Default View Setting For dtc_Date
                dtc_Date.Visible = dtc_Date.Enabled = true;
                lbl_Format_ResetDate.Visible = lbl_Format_ResetDate.Enabled = true;
                dtc_Date.BringToFront();
                lbl_Format_ResetDate.BringToFront();
                dtc_Date.ShowWildCardWinButton = false;

                //Default View Setting For dtc_Time
                /////////////Commented by Azeem //Hide Time selection for Accurate
                //dtc_Time.Visible = dtc_Time.Enabled = true; 
                //lbl_Format_ResetTime.Visible = lbl_Format_ResetTime.Enabled = true;
                //dtc_Time.BringToFront();
                //lbl_Format_ResetTime.BringToFront();
                ///////////

                //Default View Setting For dateTimeChooser
                if (dateTimeChooser != null)
                {
                    dateTimeChooser.Enabled = dateTimeChooser.Visible = false;
                    dateTimeChooser.SendToBack();
                }
                StDateTime local = dtc_Date.ContentControl.ValueCustom;
            default_Case: ;
                //Apply DateTime Visual Format Accordingly
                if (String.Equals(Yearly_View, Date_View))
                {
                    dtcCustomFormat = format_Yearly_View;
                    lbl_Format_ResetDate.Text = lbl_Yearly_View;

                    dtc_Date.ShowButtons = true;
                    //Apply Format
                    dtc_Date.ContentControl.Format = DateTimePickerFormat.Custom;
                    dtc_Date.FormatEx = dtcCustomFormat;
                }
                else if (String.Equals(Monthly_View, Date_View))
                {
                    dtcCustomFormat = format_Monthly_View;
                    lbl_Format_ResetDate.Text = lbl_Monthly_View;

                    dtc_Date.ShowButtons = true;
                    //Apply Format
                    dtc_Date.ContentControl.Format = DateTimePickerFormat.Custom;
                    dtc_Date.FormatEx = dtcCustomFormat;
                }
                else if (String.Equals(Weekly_View, Date_View))
                {
                    dtcCustomFormat = format_Weekly_View;
                    lbl_Format_ResetDate.Text = lbl_Weekly_View;

                    dtc_Date.ShowButtons = true;
                    //Apply Format
                    dtc_Date.ContentControl.Format = DateTimePickerFormat.Custom;
                    dtc_Date.FormatEx = dtcCustomFormat;
                }
                else if (String.Equals(Daily_View, Date_View))
                {
                    dtcCustomFormat = format_Daily_View;
                    lbl_Format_ResetDate.Text = lbl_Daily_View;

                    dtc_Date.ShowButtons = true;
                    //Apply Format
                    dtc_Date.Visible = dtc_Date.Enabled = false;
                    lbl_Format_ResetDate.Visible = lbl_Format_ResetDate.Enabled = false;
                    ///StDateTime
                    var t = new StDateTime() { Kind = StDateTime.DateTimeType.Date };
                    local = t;
                }
                else if (String.Equals(OnlyOnce_View, Date_View))
                {
                    dtcCustomFormat = format_OnlyOnce_View;
                    lbl_Format_ResetDate.Text = lbl_OnlyOnce_View;

                    dtc_Date.ShowButtons = true;
                    //Apply Format
                    dtc_Date.ContentControl.Format = DateTimePickerFormat.Custom;
                    dtc_Date.FormatEx = dtcCustomFormat;
                }
                else if (String.Equals(Advance_View, Date_View))
                {
                    dtcCustomFormat = format_Advance_View;
                    lbl_Format_ResetDate.Text = lbl_Advance_View;

                    dtc_Date.ShowWildCardWinButton = true;
                    dtc_Date.ShowButtons = true;
                    //Apply Format
                    dtc_Date.ContentControl.Format = DateTimePickerFormat.Custom;
                    dtc_Date.FormatEx = dtcCustomFormat;

                    //Default View Setting For dtc_Date
                    dtc_Date.Visible = dtc_Date.Enabled = false;
                    lbl_Format_ResetDate.Visible = lbl_Format_ResetDate.Enabled = false;
                    dtc_Date.SendToBack();
                    lbl_Format_ResetDate.SendToBack();
                    //Default View Setting For dtc_Time
                    //dtc_Time.Visible = dtc_Time.Enabled = false;
                    //lbl_Format_ResetTime.Visible = lbl_Format_ResetTime.Enabled = false;
                    //dtc_Time.SendToBack();
                    //lbl_Format_ResetTime.SendToBack();
                    //Default View Setting For dateTimeChooser
                    if (dateTimeChooser != null)
                    {
                        dateTimeChooser.Enabled = dateTimeChooser.Visible = true;
                        dateTimeChooser.BringToFront();

                        dateTimeChooser.DateTimeStruct = _StDateTime_Value;
                        dateTimeChooser.showDatetime();
                    }
                }
                else
                {
                    Date_View = Monthly_View;
                    goto default_Case;
                }
                dtc_Date.ContentControl.ValueCustom = local;
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error ApplyVisual_Control Mode", ex.Message,
                    5000, Notification.Sounds.Hand);
            }
        }

        /// <summary>
        /// Format dtc_DateTime_ValueChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void dtc_DateTime_ValueChanged(object sender, EventArgs e)
        {
            StDateTime localDate = null;
            try
            {
                localDate = Save_StDateTimeValue();
                lbl_RawDate.Text = localDate.ToString();
                //TODO:Verification Code For MDI_AutoResetDate
                StDateTime_Value = localDate;
            }
            catch (Exception ex)
            {
                lbl_RawDate.Text = "Error";
                Notification notifier = new Notification("Error Print dtc_dateTime value", ex.Message,
                   5000, Notification.Sounds.Hand);
            }
        }

        /// <summary>
        /// Format dateTimeChooser_ValueCustomChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void dateTimeChooser_ValueCustomChanged(object arg1, EventArgs arg2)
        {
            StDateTime localDate = null;
            try
            {
                if (!String.Equals(Advance_View, Date_View))
                    return;
                localDate = dateTimeChooser.DateTimeStruct;
                lbl_RawDate.Text = localDate.ToString();
                //TODO:Verification Code For localDateTime
                StDateTime_Value = localDate;
            }
            catch (Exception ex)
            {
                lbl_RawDate.Text = "Error";
                Notification notifier = new Notification("Error Print dtc_dateTime value", ex.Message,
                   5000, Notification.Sounds.Hand);
            }
        }

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
