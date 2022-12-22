using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DLMS;
using DLMS.Comm;
using SmartEyeControl_7.ApplicationGUI.ucCustomControl;

namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    public partial class ucSPDayProfileDateTime : UserControl
    {
        //private Param_MDI_parameters _Param_MDI_parameters_object;
        internal static readonly StDateTime Start_Time;
        internal static readonly StDateTime Start_Date;
        private StDateTime _Profile_StDateTime = null;

        internal dtpCustomExtensions dtcCustomFormat = dtpCustomExtensions.dtpDayShortName;//default Initialize
        internal string StartStDate_View = Weekly_View;
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
        public StDateTime Profile_StDateTime
        {
            get { return _Profile_StDateTime; }
            set { _Profile_StDateTime = value; }
        }

        public bool IsValidated
        {
            get
            {
                if (!dtc_StDate.IsValidated)
                    return false;
                return true;
            }
        }

        public ucSPDayProfileDateTime()
        {
            InitializeComponent();
            try
            {
                if (dtc_StDate != null)
                {
                    dtc_StDate.ContentControl.ValueCustom = Start_Date;
                    _Profile_StDateTime = Save_StDateTime();
                    showToGUI_StDateTime(_Profile_StDateTime);
                }
                if (dateTimeChooser != null)
                {
                    dateTimeChooser.DateTimeStruct = _Profile_StDateTime;
                    dateTimeChooser.showDatetime();
                }
            }
            catch
            {
            }
        }

        static ucSPDayProfileDateTime()
        {
            Start_Time = new StDateTime();
            Start_Time.Kind = StDateTime.DateTimeType.Time;
            StDateTime.StDateTimeHelper.Save_WildCards(new List<StDateTimeWildCards>() 
            {
                StDateTimeWildCards.NullYear,StDateTimeWildCards.NullMonth,
                StDateTimeWildCards.NullDay,StDateTimeWildCards.NullDayOfWeek,StDateTimeWildCards.NullGMT
            }, Start_Time);
            Start_Date = new StDateTime();
            Start_Date.Kind = StDateTime.DateTimeType.Date;
            StDateTime.StDateTimeHelper.Save_WildCards(new List<StDateTimeWildCards>() { StDateTimeWildCards.NullGMT }, Start_Date);
        }

        #region Local_Event_Handlers

        private StDateTime Save_StDateTime()
        {
            StDateTime localResetDateTime = null;
            if (dtc_StDate != null)
            {
                localResetDateTime = new StDateTime(dtc_StDate.ContentControl.ValueCustom);
                localResetDateTime.Kind = StDateTime.DateTimeType.DateTime;
                //Initialize StDateTime Time
                localResetDateTime.Hour = Start_Time.Hour;
                localResetDateTime.Minute = Start_Time.Minute;
                localResetDateTime.Second = Start_Time.Second;
                localResetDateTime.Hundred = Start_Time.Hundred;
                localResetDateTime.UTCOffset = Start_Time.UTCOffset;
            }
            return localResetDateTime;
        }

        private void ucSPDayProfileDateTime_Load(object sender, EventArgs e)
        {
            dtc_StDate.ContentControl.ValueCustomChanged += dtc_StDate_ValueChanged;

            if (dateTimeChooser != null)
            {
                dateTimeChooser.ValueCustomChanged += new EventHandler(dateTimeChooser_ValueCustomChanged);
            }

            #region Set_ErrorMessage

            if (dtc_StDate.ErrorMessage != null)
            {
                dtc_StDate.ErrorMessage = errorProvider;
                dtc_StDate.ErrorMessage.SetIconAlignment(dtc_StDate, ErrorIconAlignment.MiddleLeft);
                dtc_StDate.ErrorMessage.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            }
            if (errorProvider != null)
            {
                errorProvider.SetIconAlignment(this, ErrorIconAlignment.MiddleLeft);
                ///errorProvider.SetIconAlignment(dtc_MDI_ResetDate, ErrorIconAlignment.MiddleLeft);
                errorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            }

            #endregion
        }

        private void ucSPDayProfileDateTime_Leave(object sender, EventArgs e)
        {
            String ErrorMessage = String.Format("Error Validating MDIAutoResetDateTime,invalid DateTime Obj");
            try
            {
                ///bool isValiated = App_Validation.Validate_Param_MDI_Parameters(_Param_MDI_parameters_object, ref ErrorMessage);
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
                    StDateTimeLocal = _Profile_StDateTime;
                if (StDateTimeLocal == null)
                    throw new ArgumentNullException("StDateTimeLocal");
                String StDateTime_View = Advance_View;
                //Advance_View[Not AlreadyTaken]
                if (true || !String.Equals(StartStDate_View, Advance_View))
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
                    //try
                    //{
                    //    if (String.IsNullOrEmpty(StDateTimeLocalStr) &&
                    //        StDateTime.StDateTimeHelper.IsRepeatDailyFormat(StDateTimeLocal))
                    //    {
                    //        StDateTimeLocalStr = StDateTimeLocal.ToString(format_Daily_View);
                    //        MDIReset_View = Daily_View;
                    //    }
                    //}
                    //catch
                    //{
                    //    StDateTimeLocalStr = null;
                    //}
                }
                //Advance_View[Least Not Shown By Either]
                if (!String.Equals(StartStDate_View, StDateTime_View))
                {
                    if (String.Equals(StDateTime_View, Advance_View))
                        rdb_Advance_View.Checked = true;
                    else if (String.Equals(StDateTime_View, OnlyOnce_View))
                        rdb_OnlyOnce.Checked = true;
                    //else if (String.Equals(MDIReset_View, Daily_View))
                    //    rdb_Daily.Checked = true;
                    else if (String.Equals(StDateTime_View, Weekly_View))
                        rdb_Week.Checked = true;
                    else if (String.Equals(StDateTime_View, Monthly_View))
                        rdb_Month.Checked = true;
                    else if (String.Equals(StDateTime_View, Yearly_View))
                        rdb_Year.Checked = true;
                    //ApplyVisual_ControlMode(MDIReset_View);
                }
                //Save_Date Param_MDI Auto_Reset_date_&_Time
                StDateTimeLocal.Kind = StDateTime.DateTimeType.Date;
                dtc_StDate.ContentControl.ValueCustom = StDateTimeLocal;
                //Save_Time Param_MDI
                StDateTimeLocal.Kind = StDateTime.DateTimeType.DateTime;
                //Show StDateTime in DateTimeChooser
                dateTimeChooser.DateTimeStruct = StDateTimeLocal;
                dateTimeChooser.showDatetime();
                //Save Mdi_autoResetDateTime
                if (_Profile_StDateTime != StDateTimeLocal)
                    _Profile_StDateTime = new StDateTime(StDateTimeLocal);
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error show Param_MDI_parameters_object", ex.Message,
                    5000, Notification.Sounds.Hand);
            }
        }

        private void rdb_AutoResetDateView_CheckedChanged(object sender, EventArgs e)
        {
            string formatString = String.Empty;
            RadioButton rdb_Button = (RadioButton)sender;
            if (rdb_Year.Checked)
                formatString = Yearly_View;
            else if (rdb_Month.Checked)
                formatString = Monthly_View;
            else if (rdb_Week.Checked)
                formatString = Weekly_View;
            //else if (rdb_Daily.Checked)
            //    formatString = Daily_View;
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
                StartStDate_View = formatString;
                //Default View Setting For dtc_MDI_ResetDate
                dtc_StDate.Visible = dtc_StDate.Enabled = true;
                lbl_Format_ResetDate.Visible = lbl_Format_ResetDate.Enabled = true;
                dtc_StDate.BringToFront();
                lbl_Format_ResetDate.BringToFront();
                dtc_StDate.ShowWildCardWinButton = false;


                //Default View Setting For dateTimeChooser
                if (dateTimeChooser != null)
                {
                    dateTimeChooser.Enabled = dateTimeChooser.Visible = false;
                    dateTimeChooser.SendToBack();
                }

                StDateTime local = dtc_StDate.ContentControl.ValueCustom;
            default_Case: ;
                //Apply MDIResetDate Visual Format Accordingly
                if (String.Equals(Yearly_View, StartStDate_View))
                {
                    dtcCustomFormat = format_Yearly_View;
                    lbl_Format_ResetDate.Text = lbl_Yearly_View;

                    dtc_StDate.ShowButtons = true;
                    //Apply Format
                    dtc_StDate.ContentControl.Format = DateTimePickerFormat.Custom;
                    dtc_StDate.FormatEx = dtcCustomFormat;
                }
                else if (String.Equals(Monthly_View, StartStDate_View))
                {
                    dtcCustomFormat = format_Monthly_View;
                    lbl_Format_ResetDate.Text = lbl_Monthly_View;

                    dtc_StDate.ShowButtons = true;
                    //Apply Format
                    dtc_StDate.ContentControl.Format = DateTimePickerFormat.Custom;
                    dtc_StDate.FormatEx = dtcCustomFormat;
                }
                else if (String.Equals(Weekly_View, StartStDate_View))
                {
                    dtcCustomFormat = format_Weekly_View;
                    lbl_Format_ResetDate.Text = lbl_Weekly_View;

                    dtc_StDate.ShowButtons = true;
                    //Apply Format
                    dtc_StDate.ContentControl.Format = DateTimePickerFormat.Custom;
                    dtc_StDate.FormatEx = dtcCustomFormat;
                }
                else if (String.Equals(Daily_View, StartStDate_View))
                {
                    dtcCustomFormat = format_Daily_View;
                    lbl_Format_ResetDate.Text = lbl_Daily_View;

                    dtc_StDate.ShowButtons = true;
                    //Apply Format
                    dtc_StDate.Visible = dtc_StDate.Enabled = false;
                    lbl_Format_ResetDate.Visible = lbl_Format_ResetDate.Enabled = false;
                    ///StDateTime Should be
                    var t = new StDateTime() { Kind = StDateTime.DateTimeType.Date };
                    local = t;
                }
                else if (String.Equals(OnlyOnce_View, StartStDate_View))
                {
                    dtcCustomFormat = format_OnlyOnce_View;
                    lbl_Format_ResetDate.Text = lbl_OnlyOnce_View;

                    dtc_StDate.ShowButtons = true;
                    //Apply Format
                    dtc_StDate.ContentControl.Format = DateTimePickerFormat.Custom;
                    dtc_StDate.FormatEx = dtcCustomFormat;
                }
                else if (String.Equals(Advance_View, StartStDate_View))
                {
                    dtcCustomFormat = format_Advance_View;
                    lbl_Format_ResetDate.Text = lbl_Advance_View;

                    dtc_StDate.ShowWildCardWinButton = true;
                    dtc_StDate.ShowButtons = true;
                    //Apply Format
                    dtc_StDate.ContentControl.Format = DateTimePickerFormat.Custom;
                    dtc_StDate.FormatEx = dtcCustomFormat;


                    //Default View Setting For dtc_MDI_ResetDate
                    dtc_StDate.Visible = dtc_StDate.Enabled = false;
                    lbl_Format_ResetDate.Visible = lbl_Format_ResetDate.Enabled = false;
                    dtc_StDate.SendToBack();
                    lbl_Format_ResetDate.SendToBack();

                    //Default View Setting For dateTimeChooser
                    if (dateTimeChooser != null)
                    {
                        dateTimeChooser.Enabled = dateTimeChooser.Visible = true;
                        dateTimeChooser.BringToFront();

                        dateTimeChooser.DateTimeStruct = _Profile_StDateTime;
                        dateTimeChooser.showDatetime();
                    }
                }
                else
                {
                    StartStDate_View = Monthly_View;
                    goto default_Case;
                }
                dtc_StDate.ContentControl.ValueCustom = local;

            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error ApplyVisual_Control Mode", ex.Message,
                    5000, Notification.Sounds.Hand);
            }
        }

        /// <summary>
        /// Format dtc_Date_ValueChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void dtc_StDate_ValueChanged(object sender, EventArgs e)
        {
            String customFormatStr = String.Empty;
            String customStrFormatMsg = String.Empty;
            StDateTime localDate = null;

            //String dt_ValStr = null;
            //String dt_ValStrPrint = null;
            try
            {
                //DateTime dt_Val = dtc_MDI_ResetDate.ContentControl.Value;
                localDate = Save_StDateTime();
                lbl_RawDate.Text = localDate.ToString();
                //TODO:Verification Code For MDI_AutoResetDate
                _Profile_StDateTime = localDate;
            }
            catch (Exception ex)
            {
                lbl_RawDate.Text = "Error";
                Notification notifier = new Notification("Error Print dtc_dateTime value in StDateTime", ex.Message,
                   5000, Notification.Sounds.Hand);
            }
        }

        internal void dateTimeChooser_ValueCustomChanged(object arg1, EventArgs arg2)
        {
            String customFormatStr = String.Empty;
            String customStrFormatMsg = String.Empty;
            StDateTime localDate = null;

            //String dt_ValStr = null;
            //String dt_ValStrPrint = null;
            try
            {
                if (!String.Equals(Advance_View, StartStDate_View))
                    return;
                //DateTime dt_Val = dtc_MDI_ResetDate.ContentControl.Value;
                localDate = dateTimeChooser.DateTimeStruct;
                ///Initialize StDateTime TIME
                localDate.Hour = Start_Time.Hour;
                localDate.Minute = Start_Time.Minute;
                localDate.Second = Start_Time.Second;
                localDate.Hundred = Start_Time.Hundred;
                localDate.UTCOffset = Start_Time.UTCOffset;

                lbl_RawDate.Text = localDate.ToString();
                //TODO:Verification Code For MDI_AutoResetDate
                _Profile_StDateTime = localDate;
            }
            catch (Exception ex)
            {
                lbl_RawDate.Text = "Error";
                Notification notifier = new Notification("Error Print dtc_dateTime value in StDateTime", ex.Message,
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
