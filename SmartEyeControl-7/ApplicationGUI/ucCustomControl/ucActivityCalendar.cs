using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartEyeControl_7.ApplicationGUI.ucCustomControl;
using DLMS.Comm;
using SharedCode.Comm.DataContainer;
using SharedCode.Controllers;
using SharedCode.Comm.HelperClasses;
using SharedCode.Common;
using SEAC.Common;
using SharedCode.Comm.Param;
using SmartEyeControl_7.DB;
using System.Text;

namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    public partial class ucActivityCalendar : UserControl
    {
        #region Properties

        private Param_ActivityCalendar _Calendar = null;
        internal Param_SeasonProfile Param_SeasonProfileObj = null;
        internal SeasonProfile Sp = null;
        private List<TimeSlot> CurrentTimeSlots = null;

        ParameterController _paramController;

        DBConnect myDB = new DBConnect();

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<AccessRights> AccessRights { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Param_ActivityCalendar Calendar
        {
            get { return _Calendar; }
            set { _Calendar = value; }
        }

        public bool IsProgramDateTimeNow
        {
            get
            {
                return this.rdbInvokeAction.Checked;
            }
        }

        public bool IsValidated
        {
            get
            {
                if (errorProvider != null)
                {
                    ///Validate Error txt_CalendarName
                    if (!App_Validation.IsControlValidated(txt_CalendarName, errorProvider))
                        return false;
                    if (!App_Validation.IsControlValidated(lbl_ErrorTOD, errorProvider))
                        return false;
                    if (!App_Validation.IsControlValidated(lbl_ErrorDayProfile, errorProvider))
                        return false;
                    if (!App_Validation.IsControlValidated(lbl_ErrorSeasonProfiles, errorProvider))
                        return false;
                    if (!App_Validation.IsControlValidated(lbl_ErrorWeekProfile, errorProvider))
                        return false;
                    if (!App_Validation.IsControlValidated(lbl_ErrorSpecialDays, errorProvider))
                        return false;
                }
                if (!dtc_SeasonProfile_.IsValidated)
                    return false;
                if (ucSPDayProfileDateTime == null || !ucSPDayProfileDateTime.IsValidated)
                    return false;
                return true;
            }
        }

        #region Enable/Disable Controls Logic

        private readonly Control[] CalendarPage_WT_Controls = null;
        private readonly Control[] Day_Profile_WT_Controls = null;
        private readonly Control[] Week_Profile_WT_Controls = null;
        private readonly Control[] Season_Profile_WT_Controls = null;
        private readonly Control[] SpecialDay_Profile_WT_Controls = null;

        #endregion

        #endregion

        #region Constructor

        public ucActivityCalendar()
        {
            InitializeComponent();

            _paramController = new ParameterController();

            AccessRights = ApplicationRight.GetDefaultAccessRightByEnum(typeof(ActivityCalender));
            foreach (var right in AccessRights)
            {
                right.Read = true;
                right.Write = true;
            }

            this.SetStyle(
                    ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.UserPaint |
                    ControlStyles.DoubleBuffer,
                    true);

            CalendarPage_WT_Controls = new Control[] { rdbInvokeAction, rdbActivateOnDate, txt_CalendarName, dtc_CalendarActivationDate };
            Day_Profile_WT_Controls = new Control[] { btn_newDayProfile, btn_delete_DayProfile, btn_AddDayProfile, btn_Save_DayProfile, combo_tariff_NumberofSlots };
            Week_Profile_WT_Controls = new Control[] { btn_newWeekProfile, btn_Delete_WeekProfile, btn_Save_WeekProfile };
            Season_Profile_WT_Controls = new Control[] { btn_DeleteSeasonProfile, btn_addSeasonProfile, btn_Save_SeasonProfile, dtc_SeasonProfile_ };
            SpecialDay_Profile_WT_Controls = new Control[] { btn_Delete_SpecialDay, btn_SpecialDays_add, btn_Save_SpecialDay, ucSPDayProfileDateTime };
        }

        public ucActivityCalendar(Param_ActivityCalendar Calendar)
            : this()
        {
            this._Calendar = Calendar;
        }

        public ucActivityCalendar(Param_ActivityCalendar calender, List<AccessRights> rights)
            : this(calender)
        {
            AccessRights = rights;
            ApplyAccessRights(AccessRights);
        }
        public ucActivityCalendar(List<AccessRights> rights)
            : this()
        {
            AccessRights = rights;
            ApplyAccessRights(AccessRights);
        }

        #endregion

        private void ucActivityCalendar_Load(object sender, EventArgs e)
        {
            if (_Calendar == null)
                _Calendar = new Param_ActivityCalendar();
            if (rdbInvokeAction.Checked)
            {
                dtc_CalendarActivationDate.Enabled = false;
            }
            else
            {
                dtc_CalendarActivationDate.Enabled = true;
            }
            #region Set ErrorMessage

            if (dtc_SeasonProfile_.ErrorMessage != null)
            {
                dtc_SeasonProfile_.ErrorMessage.SetIconAlignment(dtc_SeasonProfile_, ErrorIconAlignment.MiddleLeft);
                dtc_SeasonProfile_.ErrorMessage.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            }
            //if (dtc_SpecialDays_.ErrorMessage != null)
            //{
            //    dtc_SpecialDays_.ErrorMessage.SetIconAlignment(dtc_SpecialDays_, ErrorIconAlignment.BottomRight);
            //    dtc_SpecialDays_.ErrorMessage.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            //}
            if (errorProvider != null)
            {
                lbl_ErrorTOD.Visible = lbl_ErrorDayProfile.Visible = lbl_ErrorSeasonProfiles.Visible =
                    lbl_ErrorWeekProfile.Visible = lbl_ErrorSpecialDays.Visible = false;
                errorProvider.SetIconAlignment(txt_CalendarName, ErrorIconAlignment.MiddleRight);
                errorProvider.SetIconAlignment(lbl_ErrorTOD, ErrorIconAlignment.MiddleRight);
                errorProvider.SetIconAlignment(lbl_ErrorDayProfile, ErrorIconAlignment.MiddleRight);
                errorProvider.SetIconAlignment(lbl_ErrorSeasonProfiles, ErrorIconAlignment.MiddleRight);
                errorProvider.SetIconAlignment(lbl_ErrorWeekProfile, ErrorIconAlignment.MiddleRight);
                errorProvider.SetIconAlignment(lbl_ErrorSpecialDays, ErrorIconAlignment.MiddleRight);
                errorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            }

            #endregion
        }

        #region Validation_Error_Handler

        public void Reset_Validation_ErrorNotification()
        {
            lbl_ErrorTOD.Visible = lbl_ErrorDayProfile.Visible = lbl_ErrorSeasonProfiles.Visible =
                                    lbl_ErrorWeekProfile.Visible = lbl_ErrorSpecialDays.Visible = false;

            App_Validation.Apply_ValidationResult(IsValidated, String.Empty, lbl_ErrorTOD, errorProvider);
            App_Validation.Apply_ValidationResult(IsValidated, String.Empty, lbl_ErrorDayProfile, errorProvider);
            App_Validation.Apply_ValidationResult(IsValidated, String.Empty, lbl_ErrorSeasonProfiles, errorProvider);
            App_Validation.Apply_ValidationResult(IsValidated, String.Empty, lbl_ErrorWeekProfile, errorProvider);
            App_Validation.Apply_ValidationResult(IsValidated, String.Empty, lbl_ErrorSpecialDays, errorProvider);
        }

        private void CalendarPage_Enter(object sender, EventArgs e)
        {
            bool IsValidated = false;
            String ErrorMessage = String.Empty;
            try
            {
                if (!String.IsNullOrEmpty(Calendar.CalendarName))
                {
                    DateTime Dummy = new DateTime(1900, 1, 1);
                    #region Commented_CodeSection

                    //byte[] Active_calendar_name = null;
                    //byte[] Passive_calendar_name = null;

                    //if (!String.IsNullOrEmpty(Calendar.CalendarName))
                    //{
                    //    Active_calendar_name = Encoding.ASCII.GetBytes(Calendar.CalendarName);
                    //    txt_CalendarName.Text = DLMS_Common.ArrayToHexString(Active_calendar_name);
                    //}
                    //else
                    //    txt_CalendarName.Text = "NIL";

                    //if (!String.IsNullOrEmpty(Calendar.CalendarName))
                    //{
                    //    Active_calendar_name = Encoding.ASCII.GetBytes(Calendar.CalendarName);
                    //    txt_CalendarName.Text = DLMS_Common.ArrayToHexString(Active_calendar_name);
                    //}
                    //else
                    //    txt_CalendarName.Text = "NIL";

                    #endregion
                    txt_CalendarName.Text = Calendar.CalendarName;
                    if (Calendar.CalendarstartDate.IsDateTimeConvertible)
                    {
                        DateTime StDateTime = Calendar.CalendarstartDate.GetDateTime();
                        dtc_CalendarActivationDate.Value = StDateTime;
                    }
                    else
                        dtc_CalendarActivationDate.Value = DateTime.Now;

                    #region Commented_CodeSection

                    //dtc_CalendarActivationDate.Value.Year = Calendar.CalendarstartDate.Year;
                    //dtc_CalendarActivationDate.Value.Month = Calendar.CalendarstartDate.Month;
                    //dtc_CalendarActivationDate.Value.Date = Calendar.CalendarstartDate.DayOfMonth;
                    //dtc_CalendarActivationDate.Value.Hours = Calendar.CalendarstartDate.Hour;
                    //dtc_CalendarActivationDate.Value.Minutes = Calendar.CalendarstartDate.Minute;
                    //dtc_CalendarActivationDate.Value.Seconds = Calendar.CalendarstartDate.Second; 

                    #endregion
                }
                ucActivityCalendar_Leave(sender, e);
            }
            catch (Exception ex)
            {
                Notification Notifier = new Notification("Error Occurred Validating Param TOD", ex.Message);
            }
        }

        private void Calendar_Page_Leave(object sender, EventArgs e)
        {
            ucActivityCalendar_Leave(sender, e);
        }

        private void ucActivityCalendar_Leave(object sender, EventArgs e)
        {
            bool IsValidated = false;
            String ErrorMessage = String.Empty;
            try
            {
                IsValidated = App_Validation.Validate_Param_TariffOfDay(Calendar, ref ErrorMessage);
                if (IsValidated)
                {
                    Reset_Validation_ErrorNotification();
                }
                else
                {
                    App_Validation.Apply_ValidationResult(IsValidated, ErrorMessage, lbl_ErrorTOD, errorProvider);
                    lbl_ErrorTOD.Visible = true;
                    ///Notification Notifier = new Notification("Validation Error Param_TariffOfDay(TOD)", ErrorMessage, 10000, Notification.Sounds.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Notification Notifier = new Notification("Error Occurred Validating Param TOD", ex.Message);
            }
        }

        private void Validate_Day_Profile(Param_DayProfile ParamDayProfile)
        {
            bool IsValidated = false;
            String ErrorMessage = String.Empty;
            try
            {
                IsValidated = App_Validation.Validate_DayProfile(ParamDayProfile, ref ErrorMessage);
                if (IsValidated)
                {
                    ///Reset Validation Error
                    App_Validation.Apply_ValidationResult(IsValidated, ErrorMessage, lbl_ErrorDayProfile, errorProvider);
                    lbl_ErrorDayProfile.Visible = false;
                }
                else
                {
                    App_Validation.Apply_ValidationResult(IsValidated, ErrorMessage, lbl_ErrorDayProfile, errorProvider);
                    lbl_ErrorDayProfile.Visible = true;
                    ///Notification Notifier = new Notification("Validation Error Param_DayProfiles(TOD)", ErrorMessage, 10000, Notification.Sounds.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Notification Notifier = new Notification("Error Occured Validating Param DayProfile", ex.Message);
            }
        }

        private void Validate_Day_Profile(DayProfile DayProfile)
        {
            bool IsValidated = false;
            String ErrorMessage = String.Empty;
            try
            {
                IsValidated = App_Validation.Validate_DayProfile(DayProfile, ref ErrorMessage);
                if (IsValidated)
                {
                    ///Reset Validation Error
                    App_Validation.Apply_ValidationResult(IsValidated, ErrorMessage, lbl_ErrorDayProfile, errorProvider);
                    lbl_ErrorDayProfile.Visible = false;
                }
                else
                {
                    App_Validation.Apply_ValidationResult(IsValidated, ErrorMessage, lbl_ErrorDayProfile, errorProvider);
                    lbl_ErrorDayProfile.Visible = true;
                    Notification Notifier = new Notification("Validation Error Param_DayProfiles(TOD)", ErrorMessage, 10000, Notification.Sounds.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Notification Notifier = new Notification("Error Occurred Validating Param DayProfile", ex.Message);
            }
        }

        private void Day_Profile_Enter(object sender, EventArgs e)
        {
            try
            {
                Validate_Day_Profile(Calendar.ParamDayProfile);
            }
            catch (Exception ex)
            {
                Notification Notifier = new Notification("Error Occured Validating Param DayProfiles", ex.Message);
            }
        }

        private void Day_Profile_Leave(object sender, EventArgs e)
        {
            try
            {
                Validate_Day_Profile(Calendar.ParamDayProfile);
            }
            catch (Exception ex)
            {
                Notification Notifier = new Notification("Error Occured Validating Param DayProfiles", ex.Message);
            }
        }

        private void Validate_Week_Profile(Param_WeeKProfile ParamWeekProfile)
        {
            bool IsValidated = false;
            String ErrorMessage = String.Empty;
            try
            {
                IsValidated = App_Validation.Validate_WeekProfile(ParamWeekProfile, ref ErrorMessage);
                if (IsValidated)
                {
                    ///Reset Validation Error
                    App_Validation.Apply_ValidationResult(IsValidated, ErrorMessage, lbl_ErrorWeekProfile, errorProvider);
                    lbl_ErrorWeekProfile.Visible = false;
                }
                else
                {
                    App_Validation.Apply_ValidationResult(IsValidated, ErrorMessage, lbl_ErrorWeekProfile, errorProvider);
                    lbl_ErrorWeekProfile.Visible = true;
                    ///Notification Notifier = new Notification("Validation Error Param WeekProfile", ErrorMessage, 10000, Notification.Sounds.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Notification Notifier = new Notification("Error Occured Validating Param WeekProfile", ex.Message);
            }
        }

        private void Validate_Week_Profile(WeekProfile ParamWeekProfile)
        {
            bool IsValidated = false;
            String ErrorMessage = String.Empty;
            try
            {
                IsValidated = App_Validation.Validate_WeekProfile(ParamWeekProfile, ref ErrorMessage);
                if (IsValidated)
                {
                    ///Reset Validation Error
                    App_Validation.Apply_ValidationResult(IsValidated, ErrorMessage, lbl_ErrorWeekProfile, errorProvider);
                    lbl_ErrorWeekProfile.Visible = false;
                }
                else
                {
                    App_Validation.Apply_ValidationResult(IsValidated, ErrorMessage, lbl_ErrorWeekProfile, errorProvider);
                    lbl_ErrorWeekProfile.Visible = true;
                    Notification Notifier = new Notification("Validation Error Param WeekProfile", ErrorMessage, 10000, Notification.Sounds.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Notification Notifier = new Notification("Error Occurred Validating Param WeekProfile", ex.Message);
            }
        }

        private void Week_Profile_Enter(object sender, EventArgs e)
        {
            try
            {
                Validate_Week_Profile(Calendar.ParamWeekProfile);
            }
            catch (Exception ex)
            {
                Notification Notifier = new Notification("Error Occured Validating Param WeekProfile", ex.Message);
            }
        }

        private void Week_Profile_Leave(object sender, EventArgs e)
        {
            try
            {
                Validate_Week_Profile(Calendar.ParamWeekProfile);
            }
            catch (Exception ex)
            {
                Notification Notifier = new Notification("Error Occured Validating Param WeekProfile", ex.Message);
            }
        }

        private void Validate_Season_Profiles(Param_SeasonProfile ParamSeasonProfile)
        {
            bool IsValidated = false;
            String ErrorMessage = String.Empty;
            try
            {
                IsValidated = App_Validation.Validate_SeasonProfile(ParamSeasonProfile, ref ErrorMessage);
                if (IsValidated)
                {
                    ///Reset Validation Error
                    App_Validation.Apply_ValidationResult(IsValidated, ErrorMessage, lbl_ErrorSeasonProfiles, errorProvider);
                    lbl_ErrorSeasonProfiles.Visible = false;
                }
                else
                {
                    App_Validation.Apply_ValidationResult(IsValidated, ErrorMessage, lbl_ErrorSeasonProfiles, errorProvider);
                    ///Notification Notifier = new Notification("Validation Error Param SeasonProfiles", ErrorMessage, 10000, Notification.Sounds.Exclamation);
                    lbl_ErrorSeasonProfiles.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Notification Notifier = new Notification("Validation Error Param SeasonProfiles", ex.Message);
            }
        }

        private void Validate_Season_Profiles(SeasonProfile SeasonProfile)
        {
            bool IsValidated = false;
            String ErrorMessage = String.Empty;
            try
            {
                IsValidated = App_Validation.Validate_SeasonProfile(SeasonProfile, ref ErrorMessage);
                if (IsValidated)
                {
                    ///Reset Validation Error
                    App_Validation.Apply_ValidationResult(IsValidated, ErrorMessage, lbl_ErrorSeasonProfiles, errorProvider);
                    lbl_ErrorSeasonProfiles.Visible = false;
                }
                else
                {
                    App_Validation.Apply_ValidationResult(IsValidated, ErrorMessage, lbl_ErrorSeasonProfiles, errorProvider);
                    Notification Notifier = new Notification("Validation Error Param SeasonProfiles", ErrorMessage, 10000, Notification.Sounds.Exclamation);
                    lbl_ErrorSeasonProfiles.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Notification Notifier = new Notification("Validation Error Param SeasonProfiles", ex.Message);
            }
        }

        private void Season_Profiles_Enter(object sender, EventArgs e)
        {
            try
            {
                Validate_Season_Profiles(Calendar.ParamSeasonProfile);
            }
            catch (Exception ex)
            {
                Notification Notifier = new Notification("Validation Error Param SeasonProfiles", ex.Message);
            }
        }

        private void Season_Profiles_Leave(object sender, EventArgs e)
        {
            try
            {
                ////ToDo:Fix Sort Season-Profile DateTime
                //Calendar.ParamSeasonProfile.seasonProfile_Table.Sort((X, y) => X.Start_Date.CompareTo(y.Start_Date));
                //int index = 1;
                //foreach (var SpDP in Calendar.ParamSeasonProfile.seasonProfile_Table)
                //{
                //    SpDP.Index = index++;
                //}
                //RefreshGUI_SeasonProfile(Sp);
                Validate_Season_Profiles(Calendar.ParamSeasonProfile);
            }
            catch (Exception ex)
            {
                Notification Notifier = new Notification("Validation Error Param SeasonProfiles", ex.Message);
            }
        }

        private void Validate_SpecialDayProfile(Param_SpecialDay ParamSpecialDay)
        {
            bool IsValidated = false;
            String ErrorMessage = String.Empty;
            try
            {
                IsValidated = App_Validation.Validate_SpecialDayProfile(ParamSpecialDay, ref ErrorMessage);
                if (IsValidated)
                {
                    ///Reset Validation Error
                    App_Validation.Apply_ValidationResult(IsValidated, ErrorMessage, lbl_ErrorSpecialDays, errorProvider);
                    lbl_ErrorSpecialDays.Visible = false;
                }
                else
                {
                    App_Validation.Apply_ValidationResult(IsValidated, ErrorMessage, lbl_ErrorSpecialDays, errorProvider);
                    ///Notification Notifier = new Notification("Validation Error Param Specialday Profiles", ErrorMessage, 10000, Notification.Sounds.Exclamation);
                    lbl_ErrorSpecialDays.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Notification Notifier = new Notification("Validation Error Param Specialday Profiles", ex.Message);
            }
        }

        private void Validate_SpecialDayProfile(SpecialDay SpecialDay)
        {
            bool IsValidated = false;
            String ErrorMessage = String.Empty;
            try
            {
                IsValidated = App_Validation.Validate_SpecialDayProfile(SpecialDay, ref ErrorMessage);
                if (IsValidated)
                {
                    ///Reset Validation Error
                    App_Validation.Apply_ValidationResult(IsValidated, ErrorMessage, lbl_ErrorSpecialDays, errorProvider);
                    lbl_ErrorSpecialDays.Visible = false;
                }
                else
                {
                    App_Validation.Apply_ValidationResult(IsValidated, ErrorMessage, lbl_ErrorSpecialDays, errorProvider);
                    ///Notification Notifier = new Notification("Validation Error Param Specialday Profiles", ErrorMessage, 10000, Notification.Sounds.Exclamation);
                    lbl_ErrorSpecialDays.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Notification Notifier = new Notification("Validation Error Specialday Profile", ex.Message);
            }
        }

        private void SpecialDay_Profile_Enter(object sender, EventArgs e)
        {
            try
            {
                Validate_SpecialDayProfile(Calendar.ParamSpecialDay);
                combo_SpecialDay_ProfileID.Items.Clear();
                for (int h = 0; h < list_DayProfile.Items.Count; h++)
                {
                    combo_SpecialDay_ProfileID.Items.Add(list_DayProfile.Items[h]);
                }
            }
            catch (Exception ex)
            {
                Notification Notifier = new Notification("Validation Error Param Specialday Profile", ex.Message);
            }
        }

        private void SpecialDay_Profile_Leave(object sender, EventArgs e)
        {
            try
            {
                Validate_SpecialDayProfile(Calendar.ParamSpecialDay);
            }
            catch (Exception ex)
            {
                Notification Notifier = new Notification("Validation Error Param Specialday Profile", ex.Message);
            }
        }

        #endregion

        #region Local_Event_Handlers

        private void Profiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Profiles.SelectedIndex)
            {
                case 0:
                    if (Calendar.CalendarstartDate.IsDateTimeConvertible)
                    {
                        dtc_CalendarActivationDate.Value = Calendar.CalendarstartDate.GetDateTime();
                    }
                    else if (Calendar.CalendarstartDate.IsDateConvertible)
                    {
                        dtc_CalendarActivationDate.Value = Calendar.CalendarstartDate.GetDate();
                    }
                    else
                    {
                        dtc_CalendarActivationDate.Value = dtc_CalendarActivationDate.MinDate;
                        dtc_CalendarActivationDate.Enabled = false;
                    }
                    break;
                case 1:
                    if (list_DayProfile.Items.Count > 0)
                    {
                        list_DayProfile.SelectedIndex = 0;
                        display_DayProfile();
                    }
                    break;
                case 2:
                    if (list_WeekProfile.Items.Count > 0)
                    {
                        list_WeekProfile.SelectedIndex = 0;
                        display_WeekProfile();
                    }
                    break;
                case 3:
                    combo_season_weekProfile1.Items.Clear();
                    for (int g = 0; g < list_WeekProfile.Items.Count; g++)
                    {
                        combo_season_weekProfile1.Items.Add(list_WeekProfile.Items[g]);
                    }
                    if (list_SeasonProfile.Items.Count > 0)
                    {
                        list_SeasonProfile.SelectedIndex = 0;
                        //dtc_SeasonProfile.showMonth = true;
                        //dtc_SeasonProfile.showDate = true;
                        //combo_season_weekProfile1.Items.Clear();
                        //for (int g = 0; g < list_WeekProfile.Items.Count; g++)
                        //{
                        //    combo_season_weekProfile1.Items.Add(list_WeekProfile.Items[g]);
                        //}
                        display_SeasonProfile();
                    }
                    break;
                case 4:
                    if (list_SpecialDays.Items.Count > 0)
                    {
                        list_SpecialDays.SelectedIndex = 0;
                        //dtc_SpecialDays.showYear = true;
                        //dtc_SpecialDays.showMonth = true;
                        //dtc_SpecialDays.showDate = true;
                        //dtc_SpecialDays.showDayOfWeek = true;
                        if (ucSPDayProfileDateTime != null)
                            ucSPDayProfileDateTime.rdb_Year.Checked = true;
                        Display_Special_Day_Info();
                    }
                    break;
            }
        }

        private void Profiles_Selected(object sender, TabControlEventArgs e)
        {
            #region NewCOde
            combo_day_fri.Items.Clear();
            combo_day_Mon.Items.Clear();
            combo_day_sat.Items.Clear();
            combo_day_sun.Items.Clear();
            combo_day_thu.Items.Clear();
            combo_day_tue.Items.Clear();
            combo_day_wed.Items.Clear();

            for (int h = 0; h < list_DayProfile.Items.Count; h++)
            {
                combo_day_Mon.Items.Add(list_DayProfile.Items[h]);
                combo_day_tue.Items.Add(list_DayProfile.Items[h]);
                combo_day_wed.Items.Add(list_DayProfile.Items[h]);
                combo_day_thu.Items.Add(list_DayProfile.Items[h]);
                combo_day_fri.Items.Add(list_DayProfile.Items[h]);
                combo_day_sat.Items.Add(list_DayProfile.Items[h]);
                combo_day_sun.Items.Add(list_DayProfile.Items[h]);

                if (list_WeekProfile.SelectedIndex == -1)
                {
                    btn_weekProfile_ADD.Enabled = true;
                }
                else
                {
                    btn_weekProfile_ADD.Enabled = false;
                }
            }
            #endregion
            EventArgs ev = new EventArgs();
            if (list_DayProfile.Items.Count > 0)
            {
                list_DayProfile.SelectedIndex = 0;
                list_DayProfile_SelectedIndexChanged(this, ev);
                list_DayProfile_Click(this, ev);
            }

            if (list_WeekProfile.Items.Count > 0)
            {

                //list_WeekProfile_SelectedIndexChanged(this, ev);
                list_WeekProfile_Click(this, ev);
            }

            if (list_SeasonProfile.Items.Count > 0)
            {
                list_SeasonProfile.SelectedIndex = 0;
                list_SeasonProfile_SelectedIndexChanged(this, ev);
                list_SeasonProfile_Click(this, ev);
            }

            if (list_SpecialDays.Items.Count > 0)
            {
                list_SpecialDays.SelectedIndex = 0;
                list_SpecialDays_SelectedIndexChanged(this, ev);

            }

        }

        private void dtc1_Load(object sender, EventArgs e)
        {
            //dtc_SeasonProfile.showMonth = true;
            //dtc_SeasonProfile.showDate = true;
            dtc_SeasonProfile_.Visible = true;
            SetControlEnable(true, dtc_SeasonProfile_);
        }

        //private void dtc2_Load(object sender, EventArgs e)
        //{
        //    //dtc_SpecialDays.showYear = true;
        //    //dtc_SpecialDays.showMonth = true;
        //    //dtc_SpecialDays.showDate = true;
        //    //dtc_SpecialDays.showDayOfWeek = true;
        //    //dtc_SpecialDays_.Enabled = true;
        //    //SetControlEnable(true, dtc_SpecialDays_);
        //    //dtc_SpecialDays_.Visible = true;
        //}

        #region Calendar_Page_Handlers

        private void txt_CalendarName_TextChanged(object sender, EventArgs e)
        {
            ///Validate txt_Calendar Name
            String Calendar_Name = String.Empty;
            String ErrorMessage = String.Empty;
            bool IsValidated = false;
            try
            {
                Calendar_Name = txt_CalendarName.Text;
                IsValidated = App_Validation.Validate_Param_TOD_CalendarName(Calendar_Name, ref ErrorMessage);
                if (IsValidated)
                {
                    Calendar.CalendarNamePassive = Calendar_Name;
                    Calendar.CalendarName = Calendar_Name;
                }
                App_Validation.Apply_ValidationResult(IsValidated, ErrorMessage, txt_CalendarName, errorProvider);
            }
            catch (Exception ex)
            {
                if (ErrorMessage == String.Empty)
                    ErrorMessage = String.Format("Error occurred while Exec txt_CalendarName_TextChanged {0}", ex.Message);
            }
        }

        private void rdbInvokeAction_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbInvokeAction.Checked)
            {
                dtc_CalendarActivationDate.Enabled = false;
            }
            else
            {
                //dtc_CalendarActivationDate.Enabled = true;
                SetControlEnable(true, dtc_CalendarActivationDate);
            }
        }

        private void rdb_Enable_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ///To Enable Season Profile
                if (rdb_Enable.Checked)
                {
                    SetControlEnable(true, dtc_SeasonProfile_);
                    //dtc_SeasonProfile_.Enabled = true;
                    combo_season_weekProfile1.Enabled = true;
                }
                else
                {

                    dtc_SeasonProfile_.Enabled = false;
                    combo_season_weekProfile1.Enabled = false;
                }
            }
            catch
            {
            }
        }

        private void dtc_CalendarActivationDate_ValueChanged(object sender, EventArgs e)
        {
            if (Calendar.CalendarstartDate == null)
                Calendar.CalendarstartDate = new StDateTime();
            Calendar.CalendarstartDate.SetDateTime(dtc_CalendarActivationDate.Value);
        }

        private void btn_WriteToDatabaseTariffication_Click(object sender, EventArgs e)
        {
            //saveTerrificationToDB();
            //dbController.saveActivityCalendar(Application_Controller.ConnectionManager.ConnectionInfo.MSN, Calendar);
        }

        public void saveTerrificationToDB()
        {
            if (_paramController.CurrentConnectionInfo != null)
            {
                string MSNtoPass = _paramController.CurrentConnectionInfo.MSN;
                if (!myDB.save_TerrificationToDatabase(Calendar, MSNtoPass))
                {
                    ///MessageBox.Show("Error saving to Database");
                    Notification Notifier = new Notification("Error ", "Error Saving to Database");
                }
                else
                {
                    ///MessageBox.Show("Tariffication Added to Database!!!!");
                    Notification Notifier = new Notification("Process Completed", "Tariffication Added to Database", 3000);
                }
            }
            else
            {
                Notification Notifier = new Notification("Error ", "Make Association first");
            }
        }

        #endregion

        #region Day_Profile_Handlers

        private void combo_tariff_NumberofSlots_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel_dayslot1.Visible = false;
            panel_dayslot2.Visible = false;
            panel_dayslot3.Visible = false;
            panel_dayslot4.Visible = false;
            panel_dayslot5.Visible = false;
            panel_dayslot6.Visible = false;
            ///groupBox11.BackColor = Color.LightGray;
            int noOfDayslots = Convert.ToInt16(combo_tariff_NumberofSlots.SelectedIndex + 1);
            if (noOfDayslots < 1)
            {
                MessageBox.Show("Invalid value for number of slots. Value must be between 1 and 6");
                return;
            }
            else
            {
                if (noOfDayslots > 0)
                {
                    panel_dayslot1.Visible = true;
                }
                if (noOfDayslots > 1)
                {
                    panel_dayslot2.Visible = true;
                }
                if (noOfDayslots > 2)
                {
                    panel_dayslot3.Visible = true;
                }
                if (noOfDayslots > 3)
                {
                    panel_dayslot4.Visible = true;
                }
                if (noOfDayslots > 4)
                {
                    panel_dayslot5.Visible = true;
                }
                if (noOfDayslots > 5)
                {
                    panel_dayslot6.Visible = true;
                }
            }
            Save_Validate_DayProfileSchedules();
        }

        private void txt_DayProfile_s_ValueChanged(object sender, EventArgs e)
        {
            Save_Validate_DayProfileSchedules();
        }

        private void radio_DayProfile_s1t1_CheckedChanged(object sender, EventArgs e)
        {
            Save_Validate_DayProfileSchedules();
        }

        private void btn_AddDayProfile_MouseHover(object sender, EventArgs e)
        {
            EventArgs ev = new EventArgs();
            combo_tariff_NumberofSlots_SelectedIndexChanged(this, ev);
        }

        private void list_DayProfile_Click(object sender, EventArgs e)
        {
            btn_AddDayProfile.Enabled = false;
            combo_tariff_NumberofSlots.Visible = true;
            panel_numberofslots.Visible = true;
            //btn_Save_DayProfile.Enabled = true;
            SetControlEnable(true, btn_Save_DayProfile);
            //  groupBox11.Text = "Day Profile " + (list_DayProfile.SelectedIndex + 1).ToString();
            //display_DayProfile();
            Application.DoEvents();
        }

        private void saveDayProfile()
        {
            String ErrorMessageVal = null;
            Param_DayProfile ParamDayProfile = null;
            DayProfile day_profile = null;
            DayProfile day_profile_Cloned = null;
            int currentSlotsCount = 0;
            byte index;
            try
            {
                if (list_DayProfile.SelectedIndex == -1)
                {
                    // index = 1;
                    //   MessageBox.Show("Select any Day Profile to save!");
                    Notification n = new Notification("Error", "Select any Day Profile to save!");
                    return;
                }
                else
                {
                    index = (byte)(list_DayProfile.SelectedIndex + 1);
                }
                ParamDayProfile = Calendar.ParamDayProfile;
                day_profile = ParamDayProfile.GetDayProfile(index);
                day_profile_Cloned = (DayProfile)day_profile.Clone();
                ///finding current number of slots in day profile
                currentSlotsCount = day_profile.Count<TimeSlot>();

                TimeSpan[] newslots_array = new TimeSpan[6];
                newslots_array[0] = txt_DayProfile_s1.Value.TimeOfDay;///txt_DayProfile_s1.Value.Subtract(DateTimePicker.MinimumDateTime);  
                newslots_array[1] = txt_DayProfile_s2.Value.TimeOfDay;///-DateTimePicker.MinimumDateTime;
                newslots_array[2] = txt_DayProfile_s3.Value.TimeOfDay;///-DateTimePicker.MinimumDateTime;
                newslots_array[3] = txt_DayProfile_s4.Value.TimeOfDay;///-DateTimePicker.MinimumDateTime;
                newslots_array[4] = txt_DayProfile_s5.Value.TimeOfDay;///-DateTimePicker.MinimumDateTime;
                newslots_array[5] = txt_DayProfile_s6.Value.TimeOfDay;///-DateTimePicker.MinimumDateTime;

                int newSlotsCount = combo_tariff_NumberofSlots.SelectedIndex + 1;
                int traverseNewSlots = currentSlotsCount;
                ///this checks how many slots to be added 
                ///where to get the starting time and tariff from
                TimeSlot th = null;
                if (newSlotsCount > currentSlotsCount)
                {

                    for (int i = 0; i < currentSlotsCount; i++)
                    {
                        th = day_profile.GetDaySchedule(i + 1);///.StartTime = newslots_array[i];
                        ///Copy Saved Details
                        th.StartTime = CurrentTimeSlots[i].StartTime;
                        th.ScriptSelector = CurrentTimeSlots[i].ScriptSelector;
                    }
                    for (int h = 0; h < (newSlotsCount - currentSlotsCount); h++)
                    {
                        //Add new slots
                        th = day_profile.CreateDayProfileSchedule();///.StartTime = newslots_array[traverseNewSlots];
                        ///Copy Saved Details
                        th.StartTime = CurrentTimeSlots[traverseNewSlots].StartTime;
                        th.ScriptSelector = CurrentTimeSlots[traverseNewSlots].ScriptSelector;
                        traverseNewSlots++;
                    }
                }
                else if (newSlotsCount < currentSlotsCount)
                {
                    //delete Timeslots
                    for (int h = 0; h < (currentSlotsCount - newSlotsCount); h++)
                    {
                        day_profile.DeleteDayProfileSchedule(); //it will automatically delete slots starting from last slot
                    }
                }
                ///At this point all addition or deletion of slots is completed.
                /// Assign new number of slots to current number of slots
                currentSlotsCount = newSlotsCount;
                #region Copy Start_Times & Tariff Selector

                for (int slide_CountId = 1; slide_CountId <= currentSlotsCount; slide_CountId++)
                {
                    th = day_profile.GetDaySchedule(slide_CountId);
                    ///Copy Saved Details
                    th.StartTime = CurrentTimeSlots[slide_CountId - 1].StartTime;
                    th.ScriptSelector = CurrentTimeSlots[slide_CountId - 1].ScriptSelector;
                }

                //day_profile.GetDaySchedule(1).StartTime = newslots_array[0];
                //if (currentSlotsCount > 1)
                //{
                //    day_profile.GetDaySchedule(2).StartTime = newslots_array[1];
                //}
                //if (currentSlotsCount > 2)
                //{
                //    day_profile.GetDaySchedule(3).StartTime = newslots_array[2];
                //}
                //if (currentSlotsCount > 3)
                //{
                //    day_profile.GetDaySchedule(4).StartTime = newslots_array[3];
                //}
                //if (currentSlotsCount > 4)
                //{
                //    day_profile.GetDaySchedule(5).StartTime = newslots_array[4];
                //}
                //if (currentSlotsCount > 5)
                //{
                //    day_profile.GetDaySchedule(6).StartTime = newslots_array[5];
                //}

                #endregion
                ///assignTariffs(day_profile.dayProfile_Schedule, currentSlotsCount);
            }
            catch (Exception ex)
            {

                String ErrorMessage = String.Format("Error occured Saving DayProfile Schedule {0}", day_profile);
                String ErrorMessageDetails = ex.Message;
                Notification notifier = null;
                if (day_profile != null && ParamDayProfile != null)
                {
                    #region ///Restore DayProfile Orignal Before Modifications

                    try
                    {
                        if (!App_Validation.Validate_DayProfile(day_profile, ref ErrorMessageVal) &&
                            App_Validation.Validate_DayProfile(day_profile_Cloned, ref ErrorMessageVal))
                        {
                            while (day_profile.Count<TimeSlot>() > 0)
                            {
                                day_profile.DeleteDayProfileSchedule();
                            }
                            ///Copy Clonned Object DayProfile Schedule details
                            foreach (var dp_Sch in day_profile_Cloned)
                            {
                                TimeSlot dayProfSch = day_profile.CreateDayProfileSchedule();
                                dayProfSch.StartTime = dp_Sch.StartTime;
                                dayProfSch.ScriptSelector = dp_Sch.ScriptSelector;
                            }
                            ///Display DayProfileSchedule Prev
                            list_DayProfile_SelectedIndexChanged(list_DayProfile, new EventArgs());
                        }
                    }
                    catch
                    {
                        ErrorMessageDetails = String.Format("Error Restore DayProfile {0},{1}", day_profile, ErrorMessageDetails);
                    }
                    finally
                    {
                        notifier = new Notification(ErrorMessage, ErrorMessageDetails, Notification.Sounds.Asterisk);
                    }

                    #endregion
                }
            }
            finally
            {
                Validate_Day_Profile(day_profile);
            }
        }

        private void Save_Validate_DayProfileSchedules()
        {
            bool isValidated = false;
            String ErrorMessage = null;
            try
            {
                if (CurrentTimeSlots == null)  CurrentTimeSlots = new List<TimeSlot>(); //Added by Azeem

                Save_DayProfileSchedules(CurrentTimeSlots);
                int SlideCountMax = combo_tariff_NumberofSlots.SelectedIndex + 1;
                //List<TimeSlot> tempSlots = new List<TimeSlot>();
                //for(int slideCount = 1; slideCount <= SlideCountMax;slideCount++)
                //    tempSlots.Add(CurrentTimeSlots[slideCount-1]);
                if (SlideCountMax <= 0)
                    return;
                TimeSlot OverLappSlotId = null;
                isValidated = !App_Validation.IsTimeSlicesOverlapping(CurrentTimeSlots, out OverLappSlotId, (ushort)SlideCountMax);
                Label[] TimePickers = new Label[] {lbl_Dp_Slot_s1, 
                                                   lbl_Dp_Slot_s2,
                                                   lbl_Dp_Slot_s3,
                                                   lbl_Dp_Slot_s4,
                                                   lbl_Dp_Slot_s5,
                                                   lbl_Dp_Slot_s6};
                #region Apply_Validation_Results

                if (!isValidated)
                {
                    ErrorMessage = String.Format("DayProfile Schedule TimeSlice Overlapping {0}", OverLappSlotId);

                    String picker_Name = String.Format("_s{0}", OverLappSlotId.TimeSlotId);

                    foreach (var dt_Picker in TimePickers)
                    {
                        String txt_Name = dt_Picker.Name;
                        txt_Name = txt_Name.Substring(txt_Name.IndexOf("_s"), 3);
                        if (picker_Name.CompareTo(txt_Name) <= 0)
                        {
                            App_Validation.Apply_ValidationResult(isValidated, ErrorMessage, dt_Picker, errorProvider);
                        }
                        else
                        {
                            App_Validation.Apply_ValidationResult(true, String.Empty, dt_Picker, errorProvider);
                            dt_Picker.ForeColor = dt_Picker.Parent.ForeColor;
                        }
                    }
                }
                else
                    foreach (var dt_Picker in TimePickers)
                    {
                        App_Validation.Apply_ValidationResult(true, String.Empty, dt_Picker, errorProvider);
                        dt_Picker.ForeColor = dt_Picker.Parent.ForeColor;
                    }

                #endregion
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error occured Validate/Save DayProfile Schedule", ex.Message);
            }
        }

        private void Save_DayProfileSchedules(List<TimeSlot> dayProfileSchedules)
        {
            String ErrorMessage = String.Empty;
            String ErrorMessageDetails = String.Empty;
            DayProfile day_profile = null;
            int index = -1;
            try
            {
                ///Select Current DayProfile
                if (list_DayProfile.SelectedIndex != -1)
                {
                    index = (byte)(list_DayProfile.SelectedIndex + 1);
                    day_profile = Calendar.ParamDayProfile.GetDayProfile((byte)index);
                }
                TimeSpan[] newslots_array = new TimeSpan[6];
                newslots_array[0] = txt_DayProfile_s1.Value.TimeOfDay;///txt_DayProfile_s1.Value.Subtract(DateTimePicker.MinimumDateTime);  
                newslots_array[1] = txt_DayProfile_s2.Value.TimeOfDay;///-DateTimePicker.MinimumDateTime;
                newslots_array[2] = txt_DayProfile_s3.Value.TimeOfDay;///-DateTimePicker.MinimumDateTime;
                newslots_array[3] = txt_DayProfile_s4.Value.TimeOfDay;///-DateTimePicker.MinimumDateTime;
                newslots_array[4] = txt_DayProfile_s5.Value.TimeOfDay;///-DateTimePicker.MinimumDateTime;
                newslots_array[5] = txt_DayProfile_s6.Value.TimeOfDay;///-DateTimePicker.MinimumDateTime;
                TimeSlot thSlot = null;
                ///Init dayProfileSchedules
                for (ushort dpSchId = 1; dpSchId <= TimeSlot.MAX_TimeSlot; dpSchId++)
                {
                    thSlot = null; //By Azeem
                        try
                        {
                            thSlot = dayProfileSchedules.Find((x) => x != null && x.TimeSlotId == dpSchId);
                        }
                        catch (Exception) { /* //by Azeem */ }
                        if (thSlot == null)
                        {
                            thSlot = new TimeSlot(dpSchId);
                            dayProfileSchedules.Add(thSlot);
                        }
                        thSlot.StartTime = newslots_array[dpSchId - 1]; ///Assign Start Time 
                }
                dayProfileSchedules.Sort(Common_Comparable.CompareTimeSlotById);
                assignTariffs(dayProfileSchedules, TimeSlot.MAX_TimeSlot);
            }
            catch (Exception ex)
            {
                ErrorMessageDetails = String.Format("Error Store Current TimeSchedules {0},{1}", day_profile, ex.Message);
                throw new Exception(ErrorMessageDetails, ex);
            }
        }

        private void assignTariffs(List<TimeSlot> DayProfileSchedule, int numberOfSlots)
        {
            TimeSlot SelectedSlot = null;
            #region Slot1 tariff

            SelectedSlot = DayProfileSchedule[0];
            if (radio_DayProfile_s1t1.Checked)
            {
                SelectedSlot.ScriptSelector = Tarrif_ScriptSelector.T1;
            }
            else if (radio_DayProfile_s1t2.Checked)
            {
                SelectedSlot.ScriptSelector = Tarrif_ScriptSelector.T2;
            }
            else if (radio_DayProfile_s1t3.Checked)
            {
                SelectedSlot.ScriptSelector = Tarrif_ScriptSelector.T3;
            }
            else if (radio_DayProfile_s1t4.Checked)
            {
                SelectedSlot.ScriptSelector = Tarrif_ScriptSelector.T4;
            }

            #endregion
            #region Slot2 Tariff

            if (numberOfSlots > 1)
            {
                SelectedSlot = DayProfileSchedule[1];
                if (radio_DayProfile_s2t1.Checked)
                {
                    SelectedSlot.ScriptSelector = Tarrif_ScriptSelector.T1;
                }
                else if (radio_DayProfile_s2t2.Checked)
                {
                    SelectedSlot.ScriptSelector = Tarrif_ScriptSelector.T2;
                }
                else if (radio_DayProfile_s2t3.Checked)
                {
                    SelectedSlot.ScriptSelector = Tarrif_ScriptSelector.T3;
                }
                else if (radio_DayProfile_s2t4.Checked)
                {
                    SelectedSlot.ScriptSelector = Tarrif_ScriptSelector.T4;
                }

            }
            #endregion
            #region Slot3 Tariff

            if (numberOfSlots > 2)
            {
                SelectedSlot = DayProfileSchedule[2];
                if (radio_DayProfile_s3t1.Checked)
                {
                    SelectedSlot.ScriptSelector = Tarrif_ScriptSelector.T1;
                }
                else if (radio_DayProfile_s3t2.Checked)
                {
                    SelectedSlot.ScriptSelector = Tarrif_ScriptSelector.T2;
                }
                else if (radio_DayProfile_s3t3.Checked)
                {
                    SelectedSlot.ScriptSelector = Tarrif_ScriptSelector.T3;
                }
                else if (radio_DayProfile_s3t4.Checked)
                {
                    SelectedSlot.ScriptSelector = Tarrif_ScriptSelector.T4;
                }
            }
            #endregion
            #region Slot4 Tariff

            if (numberOfSlots > 3)
            {
                SelectedSlot = DayProfileSchedule[3];
                if (radio_DayProfile_s4t1.Checked)
                {
                    SelectedSlot.ScriptSelector = Tarrif_ScriptSelector.T1;
                }
                else if (radio_DayProfile_s4t2.Checked)
                {
                    SelectedSlot.ScriptSelector = Tarrif_ScriptSelector.T2;
                }
                else if (radio_DayProfile_s4t3.Checked)
                {
                    SelectedSlot.ScriptSelector = Tarrif_ScriptSelector.T3;
                }
                else if (radio_DayProfile_s4t4.Checked)
                {
                    SelectedSlot.ScriptSelector = Tarrif_ScriptSelector.T4;
                }
            }

            #endregion
            #region Slot5 Tariff

            if (numberOfSlots > 4)
            {
                SelectedSlot = DayProfileSchedule[4];
                if (radio_DayProfile_s5t1.Checked)
                {
                    SelectedSlot.ScriptSelector = Tarrif_ScriptSelector.T1;
                }
                else if (radio_DayProfile_s5t2.Checked)
                {
                    SelectedSlot.ScriptSelector = Tarrif_ScriptSelector.T2;
                }
                else if (radio_DayProfile_s5t3.Checked)
                {
                    SelectedSlot.ScriptSelector = Tarrif_ScriptSelector.T3;
                }
                else if (radio_DayProfile_s5t4.Checked)
                {
                    SelectedSlot.ScriptSelector = Tarrif_ScriptSelector.T4;
                }
            }
            #endregion
            #region Slot6 Tariff

            if (numberOfSlots > 5)
            {
                SelectedSlot = DayProfileSchedule[5];
                if (radio_DayProfile_s6t1.Checked)
                {
                    SelectedSlot.ScriptSelector = Tarrif_ScriptSelector.T1;
                }
                else if (radio_DayProfile_s6t2.Checked)
                {
                    SelectedSlot.ScriptSelector = Tarrif_ScriptSelector.T2;
                }
                else if (radio_DayProfile_s6t3.Checked)
                {
                    SelectedSlot.ScriptSelector = Tarrif_ScriptSelector.T3;
                }
                else if (radio_DayProfile_s6t4.Checked)
                {
                    SelectedSlot.ScriptSelector = Tarrif_ScriptSelector.T4;
                }
            }

            #endregion
        }

        #endregion

        #region Week_Profile_Handlers

        private void list_WeekProfile_Click(object sender, EventArgs e)
        {
            if (list_WeekProfile.Items.Count > 0)
            {
                SetControlEnable(true, btn_Save_WeekProfile);
                //btn_Save_WeekProfile.Enabled = true;
                //btn_weekProfile_ADD.Enabled = false;
            }
            else
            {
                btn_Save_WeekProfile.Enabled = false;
                //btn_weekProfile_ADD.Enabled = true;
                SetControlEnable(true, btn_weekProfile_ADD);
            }
        }

        private void list_SeasonProfile_Click(object sender, EventArgs e)
        {
            if (list_SeasonProfile.Items.Count > 0)
            {
                //btn_Save_SeasonProfile.Enabled = true;
                SetControlEnable(true, btn_Save_SeasonProfile);
            }
            else
            {
                btn_Save_SeasonProfile.Enabled = false;
            }
        }

        #endregion

        #region Adding_Profiles

        private void btn_AddDayProfile_Click(object sender, EventArgs e)
        {
            Param_DayProfile ParamDayProfile = null;
            DayProfile dp = null;
            try
            {
                ParamDayProfile = Calendar.ParamDayProfile;
                if (ParamDayProfile.DayProfileCount == -1)
                {
                    return;
                }
                if (ParamDayProfile.DayProfileCount == 16)
                {
                    Notification n = new Notification("Error", "You have already defined \r\n maximum number of Day Profiles");
                    btn_AddDayProfile.Enabled = false;
                    SetControlEnable(true, btn_newDayProfile);
                    return;
                }
                if (combo_tariff_NumberofSlots.SelectedIndex == -1)
                {
                    //SetControlEnable(true, btn_newDayProfile);
                    //btn_AddDayProfile.Enabled = true;
                    SetControlEnable(true, btn_AddDayProfile);
                    return;
                }
                ushort numberOfSlots = Convert.ToUInt16(combo_tariff_NumberofSlots.SelectedIndex + 1);
                dp = ParamDayProfile.CreateDayProfile(numberOfSlots); //a single day Profile created

                ///this tells which profile to access
                ///dp = ParamDayProfile.GetLastDayProfile();
                ///making space for maximum slots
                //TimeSpan[] slots_array = new TimeSpan[6];
                //slots_array[0] = txt_DayProfile_s1.Value.TimeOfDay;/// txt_DayProfile_s1.Value.Subtract(DateTimePicker.MinimumDateTime);  
                /////converting datetime to timespan
                //slots_array[1] = txt_DayProfile_s2.Value.TimeOfDay;/// -DateTimePicker.MinimumDateTime;
                //slots_array[2] = txt_DayProfile_s3.Value.TimeOfDay;/// -DateTimePicker.MinimumDateTime;
                //slots_array[3] = txt_DayProfile_s4.Value.TimeOfDay;/// -DateTimePicker.MinimumDateTime;
                //slots_array[4] = txt_DayProfile_s5.Value.TimeOfDay;/// -DateTimePicker.MinimumDateTime;
                //slots_array[5] = txt_DayProfile_s6.Value.TimeOfDay;/// -DateTimePicker.MinimumDateTime;
                TimeSlot th;
                ///creating and assigning values only that are required
                for (int i = 1; i <= numberOfSlots; i++)
                {
                    th = dp.GetDaySchedule((ushort)i);///.StartTime = CurrentTimeSlots[i - 1]; ///writing start times
                    th.StartTime = CurrentTimeSlots[i - 1].StartTime;                                 //
                    th.ScriptSelector = CurrentTimeSlots[i - 1].ScriptSelector;
                }
                ///Writing Tariffs
                assignTariffs(dp.dayProfile_Schedule, numberOfSlots);
                ///at this point, a day profile has been alotted all its slots
                ///show it to listbox
                if (dp != null && dp.IsConsistent)
                {
                    list_DayProfile.Items.Add(dp);
                    SetControlEnable(true, btn_newDayProfile);
                    btn_AddDayProfile.Enabled = false;
                    combo_tariff_NumberofSlots.SelectedItem = "";
                    btn_AddDayProfile.Enabled = false;
                }
                if (!dp.IsConsistent)
                {
                    ///MessageBox.Show("Time slots not consistent");
                    Notification n = new Notification("Error", "Time slots not consistent");
                    ParamDayProfile.DeleteDayProfile();
                }
                else if (btn_newDayProfile.Text != "New Day Profile")
                {
                    btn_newDayProfile.Text = "New Day Profile";
                    //btn_delete_DayProfile.Enabled = true;
                    //btn_Save_DayProfile.Enabled = true;
                    SetControlEnable(true, new Control[] { btn_delete_DayProfile, btn_Save_DayProfile });
                    list_DayProfile.Enabled = true;
                    btn_AddDayProfile.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                if (ParamDayProfile != null)
                {
                    if (dp != null && !dp.IsConsistent)
                        ParamDayProfile.DeleteDayProfile();
                }
                //MessageBox.Show("Error Adding slots: " + ex.Message);
                Notification n = new Notification("Error Adding slots", ex.Message);
            }
            finally
            {

                Validate_Day_Profile(Calendar.ParamDayProfile);
            }
        }

        private void btn_newDayProfile_Click(object sender, EventArgs e)
        {
            if (btn_newDayProfile.Text == "New DayProfile") //Space between Day & Profile removed by Azeem Inayat
            {
                btn_newDayProfile.Text = "Cancel";
                btn_Save_DayProfile.Enabled = false;
                btn_delete_DayProfile.Enabled = false;
                list_DayProfile.Enabled = false;

                panel_numberofslots.Visible = true;
                panel_dayslot1.Visible = false;
                panel_dayslot2.Visible = false;
                panel_dayslot3.Visible = false;
                panel_dayslot4.Visible = false;
                panel_dayslot5.Visible = false;
                panel_dayslot6.Visible = false;
                //btn_AddDayProfile.Enabled = true;
                SetControlEnable(true, btn_AddDayProfile);
                combo_tariff_NumberofSlots.SelectedItem = "";

                list_DayProfile.SelectedIndex = -1;
                if (list_DayProfile.Items.Count < 16)
                {
                    gpDayProfileSettings.Text = "Day Profile " + (list_DayProfile.Items.Count + 1).ToString();
                }
                gpDayProfileSettings.BackColor = Color.Transparent;
                Application.DoEvents();
            }
            else if (btn_newDayProfile.Text == "Cancel")
            {
                btn_newDayProfile.Text = "New DayProfile";
                //btn_Save_DayProfile.Enabled = true;
                //btn_delete_DayProfile.Enabled = true;

                SetControlEnable(true, btn_Save_DayProfile);
                SetControlEnable(true, btn_delete_DayProfile);

                btn_AddDayProfile.Enabled = false;
                list_DayProfile.Enabled = true;
                panel_dayslot1.Visible = false;
                panel_dayslot2.Visible = false;
                panel_dayslot3.Visible = false;
                panel_dayslot4.Visible = false;
                panel_dayslot5.Visible = false;
                panel_dayslot6.Visible = false;
            }
            /// SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS
            if (!(combo_tariff_NumberofSlots.SelectedIndex >= 0))
            {
                combo_tariff_NumberofSlots.SelectedIndex = 0;
            }
            combo_tariff_NumberofSlots_SelectedIndexChanged(this, new EventArgs());
            btn_AddDayProfile_Click(this, new EventArgs());
            /// SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS
        }

        private void btn_newWeekProfile_Click(object sender, EventArgs e)
        {
            if (list_WeekProfile.Items.Count > 0)
            {
                list_WeekProfile.SelectedIndex = -1;
            }
            groupBox_WeekProfile.Visible = true;
            btn_Save_WeekProfile.Enabled = false;
            combo_day_Mon.SelectedIndex = 0;
            combo_day_tue.SelectedIndex = 0;
            combo_day_wed.SelectedIndex = 0;
            combo_day_thu.SelectedIndex = 0;
            combo_day_fri.SelectedIndex = 0;
            combo_day_sat.SelectedIndex = 0;
            combo_day_sun.SelectedIndex = 0;

            btn_weekProfile_ADD.Enabled = true;

            btn_weekProfile_ADD_Click(btn_weekProfile_ADD, new EventArgs());

            list_WeekProfile.SelectedItem = "";
            groupBox_WeekProfile.BackColor = Color.LightGray;
            groupBox_WeekProfile.Text = "Week Profile " + (list_WeekProfile.Items.Count + 1).ToString();

            list_WeekProfile.SelectedIndex = list_WeekProfile.Items.Count - 1;
        }

        private void btn_weekProfile_ADD_Click(object sender, EventArgs e)
        {
            Param_WeeKProfile Param_WeekProfile = Calendar.ParamWeekProfile;
            WeekProfile WeekProfile = null;
            try
            {
                WeekProfile = Param_WeekProfile.Create_Week_Profile();
                if (combo_day_Mon.SelectedItem.ToString() != "" && combo_day_tue.SelectedItem.ToString() != "" &&
                        combo_day_wed.SelectedItem.ToString() != "" && combo_day_thu.SelectedItem.ToString() != "" &&
                        combo_day_fri.SelectedItem.ToString() != "" && combo_day_sat.SelectedItem.ToString() != "" &&
                        combo_day_sun.SelectedItem.ToString() != "")
                {
                    WeekProfile.Day_Profile_MON = (DayProfile)(combo_day_Mon.SelectedItem);
                    WeekProfile.Day_Profile_TUE = (DayProfile)combo_day_tue.SelectedItem;
                    WeekProfile.Day_Profile_WED = (DayProfile)combo_day_wed.SelectedItem;
                    WeekProfile.Day_Profile_THRU = (DayProfile)combo_day_thu.SelectedItem;
                    WeekProfile.Day_Profile_FRI = (DayProfile)combo_day_fri.SelectedItem;
                    WeekProfile.Day_Profile_SAT = (DayProfile)combo_day_sat.SelectedItem;
                    WeekProfile.Day_Profile_SUN = (DayProfile)combo_day_sun.SelectedItem;
                }
                else
                {
                    //   MessageBox.Show("Enter Day Profile IDs for all days");
                    Notification n = new Notification("Error", "Enter Day Profile IDs for all days");
                    btn_weekProfile_ADD.Enabled = false;
                    btn_Save_WeekProfile.Enabled = false;
                    return;
                }
                list_WeekProfile.Items.Add(WeekProfile);
                groupBox_WeekProfile.BackColor = Color.Transparent;
                btn_weekProfile_ADD.Enabled = false;
                btn_Save_WeekProfile.Enabled = false;
            }
            catch (Exception ex)
            {
                if (WeekProfile != null)
                {
                    Param_WeekProfile.Delete_Week_Profile();
                    btn_weekProfile_ADD.Enabled = false;
                    btn_Save_WeekProfile.Enabled = false;
                }
                // MessageBox.Show("Error4: " + ex.Message);
                Notification n = new Notification("Error", ex.Message);
            }
            finally
            {
                if (Week_Profile != null)
                    Validate_Week_Profile(WeekProfile);
            }
        }

        private void btn_SpecialDays_add_Click_1(object sender, EventArgs e)
        {
            Param_SpecialDay ParamSPDay = null;
            SpecialDay SPDay = null;
            StDateTime obj_datetime = null;

            try
            {
                ParamSPDay = Calendar.ParamSpecialDay;
                if (combo_SpecialDay_ProfileID.SelectedIndex != -1)
                {
                    DayProfile dp = (DayProfile)combo_SpecialDay_ProfileID.SelectedItem;

                    //obj_datetime = new StDateTime(dtc_SpecialDays_.ContentControl.ValueCustom);
                    if (ucSPDayProfileDateTime != null)
                    {
                        obj_datetime = new StDateTime(ucSPDayProfileDateTime.Profile_StDateTime);
                    }
                    obj_datetime.Kind = StDateTime.DateTimeType.DateTime;

                    //obj_datetime.Year = Convert.ToUInt16(dtc_SpecialDays.Year);
                    //obj_datetime.Month = (byte)(dtc_SpecialDays.Month);
                    //obj_datetime.DayOfMonth = (byte)(dtc_SpecialDays.Date);
                    //obj_datetime.DayOfWeek = (byte)dtc_SpecialDays.DayOfWeek;

                    if (!obj_datetime.IsDateValid)
                        throw new Exception(String.Format("Invalid date format {0}", obj_datetime.ErrorMessageStr));
                    SPDay = ParamSPDay.CreateSpecialDay(obj_datetime, dp);
                    list_SpecialDays.Items.Add(SPDay);
                    list_SpecialDays.SelectedIndex = list_SpecialDays.Items.Count - 1;

                    if (list_SpecialDays.Items.Count > 0)
                    {
                        //btn_Delete_SpecialDay.Enabled = true;
                        SetControlEnable(true, btn_Delete_SpecialDay);
                    }
                }
                else
                {
                    //   MessageBox.Show("Please Assign a DayProfile before saving Special Day");
                    Notification n = new Notification("Error", "Please Assign a DayProfile before \r\n saving Special Day");
                    return;
                }
            }
            catch (Exception ex)
            {
                if (SPDay != null && ParamSPDay != null)
                    ParamSPDay.DeleteSpecialDay();
                //  MessageBox.Show("Error3:" + ex.Message);
                Notification n = new Notification("Error", ex.Message);
            }
            finally
            {
                if (SPDay != null)
                    Validate_SpecialDayProfile(SPDay);
            }
        }

        private void btn_addSeasonProfile_Click(object sender, EventArgs e)
        {
            try
            {
                StDateTime strDateTime = new StDateTime(dtc_SeasonProfile_.ContentControl.ValueCustom);
                ///strDateTime.Month = (byte)dtc_SeasonProfile_.ContentControl.ValueCustom.Month;
                ///strDateTime.DayOfMonth = (byte)dtc_SeasonProfile_.ContentControl.ValueCustom.DayOfMonth;
                strDateTime.Year = StDateTime.NullYear;

                if (!Is_seasonProfileDate_Already_Exist())
                {
                    string SeasonProfile_Name = combo_SeasonName.Text;
                    Param_SeasonProfileObj.Create_Season_Profile(SeasonProfile_Name);

                    Sp = GetSeasonProfile(SeasonProfile_Name);
                    if (Sp != null)
                    {
                        Sp.Start_Date = strDateTime;
                        Sp.Week_Profile = (WeekProfile)(combo_season_weekProfile1.SelectedItem);
                        Sp.Index = Param_SeasonProfileObj.seasonProfile_Table.Count;
                    }
                    if (!Param_SeasonProfileObj.IsConsistent)
                    {
                        //  MessageBox.Show("Season Profiles not consistent");
                        Notification n = new Notification("Error", "Some error exists in Season Profiles");
                    }
                    Param_SeasonProfileObj.seasonProfile_Table.Sort((x, y) => x.Start_Date.CompareTo(y.Start_Date));
                    int index = 1;
                    foreach (var spD in Param_SeasonProfileObj.seasonProfile_Table)
                    {
                        if (spD != null)
                            spD.Index = index++;
                    }
                    RefreshGUI_SeasonProfile(Sp);
                }
                else
                {
                    Notification n = new Notification("Error", "More than one season profiles can't start in same month.");
                }
            }
            catch (Exception ex)
            {
                Notification n = new Notification("Error", ex.Message);
            }
            finally
            {
                if (Sp != null)
                    Validate_Season_Profiles(Sp);
            }

            //try
            //{
            //    StDateTime strDateTime = new StDateTime(dtc_SeasonProfile_.ContentControl.ValueCustom);
            //    //StDateTime strDateTime = new StDateTime();
            //    //strDateTime.Month = (byte)dtc_SeasonProfile_.ContentControl.ValueCustom.Month;
            //    //strDateTime.DayOfMonth = (byte)dtc_SeasonProfile_.ContentControl.ValueCustom.DayOfMonth;
            //    //strDateTime.Kind = StDateTime.DateTimeType.Date; 
            //    strDateTime.Year = StDateTime.NullYear;
                
            //    if (!Is_seasonProfileDate_Already_Exist())
            //    {
            //        string SeasonProfile_Name = combo_SeasonName.Text;
            //        Param_SeasonProfileObj.Create_Season_Profile(SeasonProfile_Name);

            //        Sp = GetSeasonProfile(SeasonProfile_Name);
            //        if (Sp != null)
            //        {
            //            Sp.Start_Date = strDateTime;
            //            Sp.Week_Profile = (WeekProfile)(combo_season_weekProfile1.SelectedItem);
            //            Sp.Index = Param_SeasonProfileObj.seasonProfile_Table.Count;
            //        }
            //        if (!Param_SeasonProfileObj.IsConsistent)
            //        {
            //            //  MessageBox.Show("Season Profiles not consistent");
            //            Notification n = new Notification("Error", "Some error exists in Season Profiles");
            //        }
            //        Param_SeasonProfileObj.seasonProfile_Table.Sort((x, y) => x.Start_Date.CompareTo(y.Start_Date));
            //        int index = 1;
            //        foreach (var spD in Param_SeasonProfileObj.seasonProfile_Table)
            //        {
            //            if (spD != null)
            //                spD.Index = index++;
            //        }
            //        RefreshGUI_SeasonProfile(Sp);
            //    }
            //    else
            //    {
            //        Notification n = new Notification("Error", "More than one season profiles can't start in same month.");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Notification n = new Notification("Error", ex.Message);
            //}
            //finally
            //{
            //    if (Sp != null)
            //        Validate_Season_Profiles(Sp);
            //}
        }

        #endregion

        #region Deleting Profiles

        private void btn_delete_DayProfile_Click(object sender, EventArgs e)
        {
            try
            {
                if (list_DayProfile.Items.Count > 0)
                {
                    Param_DayProfile ParamDayProfile = Calendar.ParamDayProfile;
                    ParamDayProfile.DeleteDayProfile();
                }
                else
                {
                    panel_numberofslots.Visible = false;
                    panel_dayslot1.Visible = false;
                    panel_dayslot2.Visible = false;
                    panel_dayslot3.Visible = false;
                    panel_dayslot4.Visible = false;
                    panel_dayslot5.Visible = false;
                    panel_dayslot6.Visible = false;
                    btn_AddDayProfile.Enabled = false;
                    SetControlEnable(true, btn_newDayProfile);
                    //btn_newDayProfile.Enabled = true;
                    // MessageBox.Show("There are no more Day Profiles to be deleted!");
                    Notification n = new Notification("Error", "There are no more Day Profiles to be deleted!");
                    return;
                }
                //if everything is OK
                list_DayProfile.Items.RemoveAt(list_DayProfile.Items.Count - 1);
                if (list_DayProfile.Items.Count > 0)
                {
                    gpDayProfileSettings.Text = "Day Profile " + (list_DayProfile.Items.Count).ToString();
                }
                else
                {
                    gpDayProfileSettings.Text = "Day Profile Settings ";
                }
            }
            catch (Exception ex)
            {
                //  MessageBox.Show("Error6: " + ex.Message);
                Notification n = new Notification("Error", ex.Message);
            }
            finally
            {
                Validate_Day_Profile(Calendar.ParamDayProfile);
            }
        }

        private void btn_Delete_WeekProfile_Click(object sender, EventArgs e)
        {
            try
            {
                Param_WeeKProfile ParamWeekProfile = Calendar.ParamWeekProfile;
                if (list_WeekProfile.Items.Count > 0)
                {
                    ParamWeekProfile.Delete_Week_Profile();
                }
                else
                {
                    //MessageBox.Show("There are no more Week Profiles to be deleted!");
                    Notification n = new Notification("Error", "There are no more Week Profiles \r\n to be deleted!");
                    return;
                }
                ///if everything is OK, remove item from the list 
                list_WeekProfile.Items.RemoveAt(list_WeekProfile.Items.Count - 1);
                groupBox_WeekProfile.Text = "Week Profile " + (list_WeekProfile.Items.Count + 1).ToString();
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Error7: " + ex.Message);
                Notification n = new Notification("Error", ex.Message);
                return;
            }
            finally
            {
                Validate_Week_Profile(Calendar.ParamWeekProfile);
            }
        }

        private void btn_Delete_SpecialDay_Click(object sender, EventArgs e)
        {
            Param_SpecialDay ParamSpecialDay = Calendar.ParamSpecialDay;
            try
            {
                if (list_SpecialDays.Items.Count > 0)
                {
                    ParamSpecialDay.DeleteSpecialDay((uint)(list_SpecialDays.SelectedIndex + 1));
                }
                else
                {
                    //MessageBox.Show("There are no more Special Day Profiles to be deleted!");
                    Notification n = new Notification("Error", "There are no more Special Day Profiles \r\n  to be deleted!");
                    return;

                }
                //if everything is OK, remove item from the list 
                list_SpecialDays.Items.RemoveAt(list_SpecialDays.SelectedIndex);

                if (list_SpecialDays.Items.Count == 0)
                {
                    btn_Delete_SpecialDay.Enabled = false;
                }

                //repopulate Special Day List
                list_SpecialDays.Items.Clear();

                foreach (var item in ParamSpecialDay)
                {
                    list_SpecialDays.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error9:" + ex.Message);
                Notification n = new Notification("Error", ex.Message);
            }
            finally
            {
                Special_Days_Leave(sender, e);
            }
        }

        private void btn_DeleteSeasonProfile_Click(object sender, EventArgs e)
        {
            try
            {
                string SeasonProfile_Name = combo_SeasonName.Text;
                Sp = GetSeasonProfile(SeasonProfile_Name);
                ///Delete Season Profile to disable profile
                if (Sp != null)
                {
                    Param_SeasonProfileObj = Calendar.ParamSeasonProfile;
                    Param_SeasonProfileObj.Delete_Season_Profile(Sp.Profile_Name);

                    Param_SeasonProfileObj.seasonProfile_Table.Sort((x, y) => x.Start_Date.CompareTo(y.Start_Date));
                    //Re-arrange SeasonProfile Index
                    int index = 1;
                    foreach (var spD in Param_SeasonProfileObj.seasonProfile_Table)
                    {
                        if (spD != null)
                            spD.Index = index++;
                    }
                    //list_SeasonProfile.Items.Remove(Sp);
                }
                RefreshGUI_SeasonProfile(Sp);
            }
            catch (Exception ex)
            {
                Notification n = new Notification("Error", ex.Message);
            }
            finally
            {
                Validate_Season_Profiles(Calendar.ParamSeasonProfile);
            }
            //try
            //{
            //    string SeasonProfile_Name = combo_SeasonName.Text;
            //    Sp = GetSeasonProfile(SeasonProfile_Name);
            //    ///Delete Season Profile to disable profile
            //    if (Sp != null)
            //    {
            //        Param_SeasonProfileObj = Calendar.ParamSeasonProfile;
            //        Param_SeasonProfileObj.Delete_Season_Profile(Sp.Profile_Name);

            //        Param_SeasonProfileObj.seasonProfile_Table.Sort((x, y) => x.Start_Date.CompareTo(y.Start_Date));
            //        //Re-arrange SeasonProfile Index
            //        int index = 1;
            //        foreach (var spD in Param_SeasonProfileObj.seasonProfile_Table)
            //        {
            //            if (spD != null)
            //                spD.Index = index++;
            //        }
            //        //list_SeasonProfile.Items.Remove(Sp);
            //    }
            //    RefreshGUI_SeasonProfile(Sp);
            //}
            //catch (Exception ex)
            //{
            //    Notification n = new Notification("Error", ex.Message);
            //}
            //finally
            //{
            //    Validate_Season_Profiles(Calendar.ParamSeasonProfile);
            //}
        }

        #endregion

        #region Saving_Profile

        private void btn_Save_SpecialDay_Click(object sender, EventArgs e)
        {
            SpecialDay SPDay = null;
            try
            {
                uint index;
                if (list_SpecialDays.SelectedIndex == -1)
                {
                    //index = 1;
                    // MessageBox.Show("Select any Special Day to save!");
                    Notification n = new Notification("Error", "Select any Special Day to save!");
                    return;
                }
                index = (uint)(list_SpecialDays.SelectedIndex + 1);
                Param_SpecialDay ParamSPDay = Calendar.ParamSpecialDay;
                SPDay = ParamSPDay.GetSpecialDay(index);

                ///set SpecialDay Start_Date
                StDateTime stDate = null;
                //stDate = new StDateTime(dtc_SpecialDays_.ContentControl.ValueCustom);
                stDate.Kind = StDateTime.DateTimeType.DateTime;

                if (ucSPDayProfileDateTime != null)
                {
                    stDate = new StDateTime(ucSPDayProfileDateTime.Profile_StDateTime);
                    stDate.Kind = StDateTime.DateTimeType.DateTime;
                }

                //stDate.Year = (ushort)dtc_SpecialDays.Year;
                //stDate.Month = (byte)dtc_SpecialDays.Month;
                //stDate.DayOfMonth = (byte)dtc_SpecialDays.Date;
                //stDate.DayOfWeek = (byte)dtc_SpecialDays.DayOfWeek;

                if (!stDate.IsDateValid)
                    throw new Exception(String.Format("Invalid date format {0}", stDate.ErrorMessageStr));
                SPDay.StartDate = stDate;
                SPDay.DayProfile = (DayProfile)(combo_SpecialDay_ProfileID.SelectedItem);
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Error10: " + ex.Message);
                Notification n = new Notification("Error", ex.Message);
            }
            finally
            {
                Validate_SpecialDayProfile(SPDay);
            }
        }

        private void btn_Save_SeasonProfile_Click(object sender, EventArgs e)
        {
            try
            {
                ///Try To Get Season Profile Name
                string SeasonProfile_Name = combo_SeasonName.Text;
                Param_SeasonProfile Param_SeasonProfileObj = Calendar.ParamSeasonProfile;
                if (String.IsNullOrEmpty(SeasonProfile_Name))
                {
                    Notification n = new Notification("Error", "Select any Season Profile to save!");
                    return;
                }
                Sp = GetSeasonProfile(SeasonProfile_Name);
                ////Save All Season Profile Details
                StDateTime strDateTime = new StDateTime(dtc_SeasonProfile_.ContentControl.ValueCustom);
                strDateTime.Kind = StDateTime.DateTimeType.Date;
                //strDateTime.Month = (byte)dtc_SeasonProfile.Month;
                //strDateTime.DayOfMonth = (byte)dtc_SeasonProfile.Date;
                strDateTime.Year = StDateTime.NullYear;
                if (Sp != null)
                {
                    Sp.Start_Date = strDateTime;
                    Sp.Week_Profile = (WeekProfile)(combo_season_weekProfile1.SelectedItem);
                }
                Param_SeasonProfileObj.seasonProfile_Table.Sort((x, y) => x.Start_Date.CompareTo(y.Start_Date));
                //Re-arrange SeasonProfile Index
                int index = 1;
                foreach (var spD in Param_SeasonProfileObj.seasonProfile_Table)
                {
                    if (spD != null)
                        spD.Index = index++;
                }
                if (!Param_SeasonProfileObj.IsConsistent)
                {
                    //  MessageBox.Show("Season Profiles not consistent");
                    Notification n = new Notification("Error", "Some error exists in Season Profiles");
                }
                RefreshGUI_SeasonProfile(Sp);
                ///Notification notifier = new Notification("Saved Successfully", "Season Profiles successfully updated");
            }
            catch (Exception ex)
            {
                Notification n = new Notification("Error", "Error occurred while saving Season Profile_" + ex.Message);
            }
            finally
            {
                Validate_Season_Profiles(Sp);
            }

            //try
            //{
            //    ///Try To Get Season Profile Name
            //    string SeasonProfile_Name = combo_SeasonName.Text;
            //    Param_SeasonProfile Param_SeasonProfileObj = Calendar.ParamSeasonProfile;
            //    if (String.IsNullOrEmpty(SeasonProfile_Name))
            //    {
            //        Notification n = new Notification("Error", "Select any Season Profile to save!");
            //        return;
            //    }
            //    Sp = GetSeasonProfile(SeasonProfile_Name);
            //    ////Save All Season Profile Details
            //    ///StDateTime strDateTime = new StDateTime(dtc_SeasonProfile_.ContentControl.ValueCustom);
            //    StDateTime strDateTime = new StDateTime();
            //    strDateTime.Kind = StDateTime.DateTimeType.Date;
            //    strDateTime.Month = (byte)dtc_SeasonProfile_.ContentControl.ValueCustom.Month;
            //    strDateTime.DayOfMonth = (byte)dtc_SeasonProfile_.ContentControl.ValueCustom.DayOfMonth;
            //    strDateTime.Year = StDateTime.NullYear;

            //    if (Sp != null)
            //    {
            //        Sp.Start_Date = strDateTime;
            //        Sp.Week_Profile = (WeekProfile)(combo_season_weekProfile1.SelectedItem);
            //    }
            //    Param_SeasonProfileObj.seasonProfile_Table.Sort((x, y) => x.Start_Date.CompareTo(y.Start_Date));
            //    //Re-arrange SeasonProfile Index
            //    int index = 1;
            //    foreach (var spD in Param_SeasonProfileObj.seasonProfile_Table)
            //    {
            //        if (spD != null)
            //            spD.Index = index++;
            //    }
            //    if (!Param_SeasonProfileObj.IsConsistent)
            //    {
            //        //  MessageBox.Show("Season Profiles not consistent");
            //        Notification n = new Notification("Error", "Some error exists in Season Profiles");
            //    }
            //    RefreshGUI_SeasonProfile(Sp);
            //    ///Notification notifier = new Notification("Saved Successfully", "Season Profiles successfully updated");
            //}
            //catch (Exception ex)
            //{
            //    Notification n = new Notification("Error", "Error occurred while saving Season Profile_" + ex.Message);
            //}
            //finally
            //{
            //    Validate_Season_Profiles(Sp);
            //}
        }

        private void btn_Save_WeekProfile_Click(object sender, EventArgs e)
        {
            ushort index;
            if (list_WeekProfile.SelectedIndex == -1)
            {
                //index = 1;
                // MessageBox.Show("Select any Week Profile to save!");
                Notification n = new Notification("Error", "Select any Week Profile to save!");
                return;
            }
            index = (ushort)(list_WeekProfile.SelectedIndex + 1);
            Param_WeeKProfile ParamWeekProfile = Calendar.ParamWeekProfile;
            WeekProfile Week_Profile = ParamWeekProfile.Get_Week_Profile(index);

            try
            {
                Week_Profile.Day_Profile_FRI = (DayProfile)combo_day_fri.SelectedItem;
                Week_Profile.Day_Profile_SAT = (DayProfile)combo_day_sat.SelectedItem;
                Week_Profile.Day_Profile_SUN = (DayProfile)combo_day_sun.SelectedItem;
                Week_Profile.Day_Profile_MON = (DayProfile)combo_day_Mon.SelectedItem;
                Week_Profile.Day_Profile_TUE = (DayProfile)combo_day_tue.SelectedItem;
                Week_Profile.Day_Profile_WED = (DayProfile)combo_day_wed.SelectedItem;
                Week_Profile.Day_Profile_THRU = (DayProfile)combo_day_thu.SelectedItem;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error1: Saving WeekProfile" + ex.Message);
                Notification n = new Notification("Error", ex.Message);
            }
            finally
            {
                Validate_Week_Profile(Week_Profile);
            }
        }

        private void btn_Save_DayProfile_Click(object sender, EventArgs e)
        {
            try
            {
                saveDayProfile();
            }
            catch (Exception ex)
            {
                ///MessageBox.Show("Error1: Saving WeekProfile" + ex.Message);
                Notification n = new Notification("Error", ex.Message);
            }
        }

        #endregion

        #region Lists_Selected_Index_Change

        private void list_SpecialDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            Display_Special_Day_Info();
        }

        private void list_SeasonProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                display_SeasonProfile();

                /////btn_Save_SeasonProfile.Enabled = true;
                //SeasonProfile sp = (SeasonProfile)list_SeasonProfile.SelectedItem;
                //if (sp != null)
                //    display_SeasonProfile(sp);
                /////btn_Delete_SeasonProfile.Enabled = true;
            }
            catch (Exception)
            {
                Notification nt = new Notification("Error", "Error displaying season profile");
            }
        }

        private void list_WeekProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            display_WeekProfile();
        }

        private void list_DayProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int x = list_DayProfile.SelectedIndex;
                display_DayProfile();

                Param_DayProfile ParamDayProfile = null;
                DayProfile day_profile = null;
                ParamDayProfile = Calendar.ParamDayProfile;
                day_profile = ParamDayProfile.GetDayProfile((byte)(list_DayProfile.SelectedIndex + 1));
                TimeSpan[] temp_timeArray = null;
                int numberOfSlots = combo_tariff_NumberofSlots.SelectedIndex + 1;
                if (list_DayProfile.SelectedIndex != -1)
                {
                    if (day_profile != null && day_profile.IsConsistent)
                    {
                        ///save settings to temp variables
                        temp_timeArray = new TimeSpan[numberOfSlots];
                        for (int i = 0; i < numberOfSlots; i++)
                        {
                            ushort j = (ushort)(i + 1);
                            temp_timeArray[i] = day_profile.GetDaySchedule(j).StartTime;
                        }
                        gpDayProfileSettings.Text = "Day Profile " + (list_DayProfile.SelectedIndex + 1).ToString();
                    }
                    ///************************************************************
                    else
                    {
                        temp_timeArray = new TimeSpan[6];
                        for (int i = 0; i < 6; i++)
                        {
                            ushort j = (ushort)(i + 1);
                            temp_timeArray[i] = new TimeSpan(0, 0, 0);
                        }
                        gpDayProfileSettings.Text = "Day Profile " + (list_DayProfile.SelectedIndex + 1).ToString();
                    }
                    ///************************************************************
                }
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error", String.Format("list_DayProfile_SelectedIndexChanged \n Details{0}", ex.Message));
            }
        }

        #endregion

        #region Profile Leave Events

        private void Special_Days_Leave(object sender, EventArgs e)
        {
            combo_SpecialDay_ProfileID.Items.Clear();
        }

        #endregion

        #region Profile Enter Events

        private void Special_Days_Enter(object sender, EventArgs e)
        {
            for (int h = 0; h < list_DayProfile.Items.Count; h++)
            {
                combo_SpecialDay_ProfileID.Items.Add(list_DayProfile.Items[h]);
            }
            //if (combo_SpecialDay_ProfileID.Items.Count > 0)
            //    combo_SpecialDay_ProfileID.SelectedIndex = 0;
        }

        private void Day_Profile_MouseEnter(object sender, EventArgs e)
        {
            if (list_DayProfile.SelectedIndex == -1)
            {
                btn_Save_DayProfile.Enabled = false;
                btn_delete_DayProfile.Enabled = false;
            }
            else
            {
                //btn_Save_DayProfile.Enabled = true;
                //btn_delete_DayProfile.Enabled = true;
                //btn_Save_DayProfile.Enabled = true;

                SetControlEnable(true, btn_Save_DayProfile);
                SetControlEnable(true, btn_delete_DayProfile);
                SetControlEnable(true, btn_Save_DayProfile);

            }
            combo_tariff_NumberofSlots.SelectedItem = "";
        }

        private void Week_Profile_MouseEnter(object sender, EventArgs e)
        {

            if (list_WeekProfile.Items.Count < 1)
            {
                btn_Save_WeekProfile.Enabled = false;
                btn_Delete_WeekProfile.Enabled = false;

            }
            else
            {
                //btn_Delete_WeekProfile.Enabled = true;
                SetControlEnable(true, btn_Delete_WeekProfile);
            }

        }

        private void Special_Days_MouseEnter(object sender, EventArgs e)
        {
            if (list_SpecialDays.Items.Count < 1)
            {
                btn_Save_SpecialDay.Enabled = false;
                btn_Delete_SpecialDay.Enabled = false;
            }
            else
            {
                //btn_Save_SpecialDay.Enabled = true;
                //btn_Delete_SpecialDay.Enabled = true;

                SetControlEnable(true, btn_Save_SpecialDay);
                SetControlEnable(true, btn_Delete_SpecialDay);
            }
        }

        #endregion

        #endregion

        #region Show_TO_GUI_METHODS

        public void showTariffication(bool specific = false)
        {
            try
            {
                DisplayNameAndDate();

                #region SHOW day profiles

                DisplayDayProfile();

                #endregion


                

                Application.DoEvents();

                #region SHOW Week profiles

                DisplayWeekProfile();

                #endregion
                Application.DoEvents();

                #region SHOW Season profiles

                RefreshGUI_SeasonProfile(null);

                #endregion
                Application.DoEvents();
                #region SHOW Special Days

                DisplaySpecialDays();
                
                #endregion
                Application.DoEvents();
                Select_DefaultPage();
                //Validate New Loaded Calendar_Page

                Season_Profiles_Enter(_SeasonProfile, new EventArgs());
                
                if (!Calendar.IsConsistent && !specific)
                {
                    if (!Calendar.ParamSeasonProfile.IsConsistent)
                        throw new Exception("Unable to Populate Activity Calendar Properly,Season Profile Table Inconsistent");
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Notification Notifier = new Notification("Error", String.Format("Error displaying Tarrification\r\n{0}", ex.Message), 6000);
            }

        }

        public void DisplaySpecialDays()
        {
            try
            {
                list_SpecialDays.Items.Clear(); Application.DoEvents();
                foreach (var specialDays in Calendar.ParamSpecialDay)
                {
                    list_SpecialDays.Items.Add(specialDays);
                }
                if (list_SpecialDays.Items.Count > 0)
                {
                    //list_SpecialDays.SelectedIndex = 0;
                }

                Special_Days_Enter(Special_Days, new EventArgs());

                if (!Calendar.IsConsistent)
                {
                    if (!Calendar.ParamSpecialDay.IsConsistent)
                        throw new Exception("Unable to Populate Activity Calendar Properly,Special Day Profile Table Inconsistent");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DisplayWeekProfile()
        {
            try
            {
                list_WeekProfile.Items.Clear(); Application.DoEvents();
                foreach (var weekProfile in Calendar.ParamWeekProfile)
                {
                    list_WeekProfile.Items.Add(weekProfile);
                }
                if (list_WeekProfile.Items.Count > 0)
                {
                    //list_WeekProfile.SelectedIndex = 0;
                }

                Week_Profile_Enter(Week_Profile, new EventArgs());

                if (!Calendar.IsConsistent)
                {
                    if (!Calendar.ParamWeekProfile.IsConsistent)
                        throw new Exception("Unable to Populate Activity Calendar Properly,Week Profile Table Inconsistent");

                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DisplayNameAndDate()
        {
            try
            {
                dtc1_Load(this, new EventArgs());
                //dtc2_Load(this, new EventArgs());
                ///////////////////////////////////////
                SetControlEnable(true, btn_newDayProfile);
                SetControlEnable(true, dtc_SeasonProfile_);
                //btn_newDayProfile.Enabled = true;
                //this.dtc_SeasonProfile_.Visible = true;
                groupBox_WeekProfile.Visible = true;
                if (combo_day_tue.Items.Count > 0) combo_day_Mon.SelectedIndex = 0;
                if (combo_day_wed.Items.Count > 0) combo_day_tue.SelectedIndex = 0;
                if (combo_day_thu.Items.Count > 0) combo_day_wed.SelectedIndex = 0;
                if (combo_day_fri.Items.Count > 0) combo_day_thu.SelectedIndex = 0;
                if (combo_day_sat.Items.Count > 0) combo_day_fri.SelectedIndex = 0;
                if (combo_day_sun.Items.Count > 0) combo_day_sat.SelectedIndex = 0;
                if (combo_day_Mon.Items.Count > 0) combo_day_sun.SelectedIndex = 0;

                //btn_weekProfile_ADD.Enabled = true;
                SetControlEnable(true, btn_weekProfile_ADD);
                ///////////////////////////////////////

                if (Calendar.CalendarstartDate.IsDateTimeConvertible)
                {
                    dtc_CalendarActivationDate.Value = Calendar.CalendarstartDate.GetDateTime();
                }
                else if (Calendar.CalendarstartDate.IsDateConvertible)
                {
                    dtc_CalendarActivationDate.Value = Calendar.CalendarstartDate.GetDate();
                }
                else
                {
                    dtc_CalendarActivationDate.Value = dtc_CalendarActivationDate.MinDate;
                    //dtc_CalendarActivationDate.Enabled = false;
                    SetControlEnable(true, dtc_CalendarActivationDate);
                }

                txt_CalendarName.Text = Calendar.CalendarName;
                Param_SeasonProfileObj = Calendar.ParamSeasonProfile;
                for (int i = 0; i < Param_SeasonProfileObj.seasonProfile_Table.Count; i++)
                {
                    Param_SeasonProfileObj.seasonProfile_Table[i].Index = i + 1;
                }
                if (Calendar.CalendarstartDate.GetDateTime() > DateTime.Now)
                {
                    rdbActivateOnDate.Checked = true;
                }
                else
                {
                    rdbInvokeAction.Checked = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DisplayDayProfile()
        {
            try
            {
                list_DayProfile.Items.Clear(); Application.DoEvents();                

                foreach (var dayProfile in Calendar.ParamDayProfile)
                {
                    list_DayProfile.Items.Add(dayProfile);
                }

                if (list_DayProfile.Items.Count > 0)
                {
                    //  list_DayProfile.SelectedIndex = 0;
                    //display_DayProfile();
                }
                //Application.DoEvents();
                //if (list_DayProfile.Items.Count > 0)
                //{
                //    list_DayProfile.SelectedIndex = 0;
                //}

                Day_Profile_Enter(Day_Profile, new EventArgs());

                if (!Calendar.IsConsistent)
                {
                    if (!Calendar.ParamDayProfile.IsConsistent)
                        throw new Exception("Unable to Populate Activity Calendar Properly,Day Profile Table Inconsistent");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // check for the duplication of season profile date
        private bool Is_seasonProfileDate_Already_Exist()
        {
            Param_SeasonProfile Param_Season = Calendar.ParamSeasonProfile;
            bool status = false;
            ///Find Out Season Profile of specified Selected Name
            foreach (SeasonProfile SP in Param_Season)
            {
                if (SP.Start_Date.Month == dtc_SeasonProfile_.ContentControl.ValueCustom.Month)
                {
                    status = true;
                    break;
                }
            }
            return status;
        }

        private SeasonProfile GetSeasonProfile(string SeasonProfileName)
        {
            Param_SeasonProfile Param_Season = Calendar.ParamSeasonProfile;
            SeasonProfile sp = null;
            ///Find Out Season Profile of specified Selected Name
            foreach (SeasonProfile SP in Param_Season)
            {
                if (SP.Profile_Name_Str.Equals(SeasonProfileName))
                {
                    sp = SP;
                    break;
                }
            }
            return sp;
        }

        public void display_WeekProfile()
        {
            try
            {
                //btn_Save_WeekProfile.Enabled = true;
                SetControlEnable(true, btn_Save_WeekProfile);
                ushort i = (ushort)(list_WeekProfile.SelectedIndex + 1);
                Param_WeeKProfile Param_WeekProfile = Calendar.ParamWeekProfile;
                WeekProfile WeekProfile = Param_WeekProfile.Get_Week_Profile(i);

                combo_day_Mon.SelectedItem = WeekProfile.Day_Profile_MON;
                combo_day_tue.SelectedItem = WeekProfile.Day_Profile_TUE;
                combo_day_wed.SelectedItem = WeekProfile.Day_Profile_WED;
                combo_day_thu.SelectedItem = WeekProfile.Day_Profile_THRU;
                combo_day_fri.SelectedItem = WeekProfile.Day_Profile_FRI;
                combo_day_sat.SelectedItem = WeekProfile.Day_Profile_SAT;
                combo_day_sun.SelectedItem = WeekProfile.Day_Profile_SUN;
                groupBox_WeekProfile.Text = "Week Profile " + (list_WeekProfile.SelectedIndex + 1).ToString();
                Application.DoEvents();
            }
            catch (Exception Ex)
            {
                ///MessageBox.Show("Some error in Week Profile"+Ex.Message);
            }
        }

        public void display_DayProfile()
        {
            combo_tariff_NumberofSlots.Visible = true;
            Param_DayProfile ParamDayProfile = Calendar.ParamDayProfile;
            DayProfile dp;
            panel_dayslot2.Visible = false;
            panel_dayslot3.Visible = false;
            panel_dayslot4.Visible = false;
            panel_dayslot5.Visible = false;
            panel_dayslot6.Visible = false;
            if (list_DayProfile.SelectedIndex == -1 || list_DayProfile.Items.Count == 0)
            {
                return;
            }
            dp = ParamDayProfile.GetDayProfile((byte)(list_DayProfile.SelectedIndex + 1));
            if (dp == null) return;
            #region ///Copy TimeSlots From Current DayProfile

            if (CurrentTimeSlots == null)
                CurrentTimeSlots = new List<TimeSlot>();
            else
                CurrentTimeSlots.Clear();
            foreach (var timeSlot in dp)
            {
                CurrentTimeSlots.Add((TimeSlot)timeSlot.Clone());
            }

            #endregion
            int TimeSliceCount = 0;
            foreach (var item in dp)
            {
                TimeSliceCount++;
            }
            Application.DoEvents();
            combo_tariff_NumberofSlots.SelectedIndex = TimeSliceCount - 1;
            if (TimeSliceCount > 0)
            {
                panel_dayslot1.Visible = true;
                txt_DayProfile_s1.Value = DateTimePicker.MinimumDateTime.Add(dp.GetDaySchedule(1).StartTime);
                #region Tariffs
                if (dp.GetDaySchedule(1).ScriptSelector == Tarrif_ScriptSelector.T1)
                {
                    radio_DayProfile_s1t1.Checked = true;
                }
                else
                {
                    if (dp.GetDaySchedule(1).ScriptSelector == Tarrif_ScriptSelector.T2)
                    {
                        radio_DayProfile_s1t2.Checked = true;
                    }
                    else
                    {
                        if (dp.GetDaySchedule(1).ScriptSelector == Tarrif_ScriptSelector.T3)
                        {
                            radio_DayProfile_s1t3.Checked = true;
                        }
                        else
                        {
                            radio_DayProfile_s1t4.Checked = true;
                        }
                    }
                }
                #endregion
            }
            if (TimeSliceCount > 1)
            {
                panel_dayslot2.Visible = true;
                txt_DayProfile_s2.Value = DateTimePicker.MinimumDateTime.Add(dp.GetDaySchedule(2).StartTime);
                #region Tariffs
                if (dp.GetDaySchedule(2).ScriptSelector == Tarrif_ScriptSelector.T1)
                {
                    radio_DayProfile_s2t1.Checked = true;
                }
                else
                {
                    if (dp.GetDaySchedule(2).ScriptSelector == Tarrif_ScriptSelector.T2)
                    {
                        radio_DayProfile_s2t2.Checked = true;
                    }
                    else
                    {
                        if (dp.GetDaySchedule(2).ScriptSelector == Tarrif_ScriptSelector.T3)
                        {
                            radio_DayProfile_s2t3.Checked = true;
                        }
                        else
                        {
                            radio_DayProfile_s2t4.Checked = true;
                        }
                    }
                }
                #endregion
            }
            if (TimeSliceCount > 2)
            {
                panel_dayslot3.Visible = true;
                txt_DayProfile_s3.Value = DateTimePicker.MinimumDateTime.Add(dp.GetDaySchedule(3).StartTime);
                #region Tariffs
                if (dp.GetDaySchedule(3).ScriptSelector == Tarrif_ScriptSelector.T1)
                {
                    radio_DayProfile_s3t1.Checked = true;
                }
                else
                {
                    if (dp.GetDaySchedule(3).ScriptSelector == Tarrif_ScriptSelector.T2)
                    {
                        radio_DayProfile_s3t2.Checked = true;
                    }
                    else
                    {
                        if (dp.GetDaySchedule(3).ScriptSelector == Tarrif_ScriptSelector.T3)
                        {
                            radio_DayProfile_s3t3.Checked = true;
                        }
                        else
                        {
                            radio_DayProfile_s3t4.Checked = true;
                        }
                    }
                }
                #endregion
            }
            if (TimeSliceCount > 3)
            {
                panel_dayslot4.Visible = true;
                txt_DayProfile_s4.Value = DateTimePicker.MinimumDateTime.Add(dp.GetDaySchedule(4).StartTime);
                #region Tariffs
                if (dp.GetDaySchedule(4).ScriptSelector == Tarrif_ScriptSelector.T1)
                {
                    radio_DayProfile_s4t1.Checked = true;
                }
                else
                {
                    if (dp.GetDaySchedule(4).ScriptSelector == Tarrif_ScriptSelector.T2)
                    {
                        radio_DayProfile_s4t2.Checked = true;
                    }
                    else
                    {
                        if (dp.GetDaySchedule(4).ScriptSelector == Tarrif_ScriptSelector.T3)
                        {
                            radio_DayProfile_s4t3.Checked = true;
                        }
                        else
                        {
                            radio_DayProfile_s4t4.Checked = true;
                        }
                    }
                }
                #endregion
            }
            if (TimeSliceCount > 4)
            {
                panel_dayslot5.Visible = true;
                txt_DayProfile_s5.Value = DateTimePicker.MinimumDateTime.Add(dp.GetDaySchedule(5).StartTime);
                #region Tariffs
                if (dp.GetDaySchedule(5).ScriptSelector == Tarrif_ScriptSelector.T1)
                {
                    radio_DayProfile_s5t1.Checked = true;
                }
                else
                {
                    if (dp.GetDaySchedule(5).ScriptSelector == Tarrif_ScriptSelector.T2)
                    {
                        radio_DayProfile_s5t2.Checked = true;
                    }
                    else
                    {
                        if (dp.GetDaySchedule(5).ScriptSelector == Tarrif_ScriptSelector.T3)
                        {
                            radio_DayProfile_s5t3.Checked = true;
                        }
                        else
                        {
                            radio_DayProfile_s5t4.Checked = true;
                        }
                    }
                }
                #endregion
            }
            if (TimeSliceCount > 5)
            {
                panel_dayslot6.Visible = true;
                txt_DayProfile_s6.Value = DateTimePicker.MinimumDateTime.Add(dp.GetDaySchedule(6).StartTime);
                #region Tariffs
                if (dp.GetDaySchedule(6).ScriptSelector == Tarrif_ScriptSelector.T1)
                {
                    radio_DayProfile_s6t1.Checked = true;
                }
                else
                {
                    if (dp.GetDaySchedule(6).ScriptSelector == Tarrif_ScriptSelector.T2)
                    {
                        radio_DayProfile_s6t2.Checked = true;
                    }
                    else
                    {
                        if (dp.GetDaySchedule(6).ScriptSelector == Tarrif_ScriptSelector.T3)
                        {
                            radio_DayProfile_s6t3.Checked = true;
                        }
                        else
                        {
                            radio_DayProfile_s6t4.Checked = true;
                        }
                    }
                }
                #endregion
            }

            Application.DoEvents();
        }

        public void display_SeasonProfile()
        {
            try
            {
                Param_SeasonProfileObj = Calendar.ParamSeasonProfile;
                if (Param_SeasonProfileObj != null)
                {
                    SeasonProfile sp = (SeasonProfile)list_SeasonProfile.SelectedItem;
                    //int index = Convert.ToInt32(list_SeasonProfile.SelectedIndex);
                    //Sp = Param_SeasonProfileObj.seasonProfile_Table.Find(x => x.Index.Equals(index));
                    if (sp != null)
                    {
                        ///int id = Sp.Index;
                        ///StDateTime obj_datetime = new StDateTime();
                        ///byte[] my = Encoding.ASCII.GetBytes(Sp.Profile_Name);

                        ///combo_SeasonName.SelectedIndex = (int)(my[0] - 1);
                        ///obj_datetime = Sp.Start_Date;

                        /////dtc_SeasonProfile.Month = obj_datetime.Month;
                        /////dtc_SeasonProfile.Date = obj_datetime.DayOfMonth;
                        /////dtc_SeasonProfile.showDatetime(); 
                        /////this method of custom control updates GUI

                        /////Show to GUI CustomDateTimePicker
                        //obj_datetime.Kind = StDateTime.DateTimeType.Date;
                        //dtc_SeasonProfile_.ContentControl.ValueCustom = obj_datetime;

                        //combo_season_weekProfile1.SelectedItem = Sp.Week_Profile;
                        ///groupBox_SeasonProfile.Text = "Season Profile" + (list_SeasonProfile.SelectedIndex + 1).ToString();

                        display_SeasonProfile(sp);
                        Application.DoEvents();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void display_SeasonProfile(SeasonProfile obj_SeasonProfile)
        {
            try
            {
                Param_SeasonProfile Param_Season = Calendar.ParamSeasonProfile;
                if (combo_SeasonName.SelectedItem == null ||
                   !combo_SeasonName.SelectedItem.Equals(obj_SeasonProfile.Profile_Name_Str))
                    combo_SeasonName.SelectedItem = obj_SeasonProfile.Profile_Name_Str;

                StDateTime obj_datetime = obj_SeasonProfile.Start_Date;
                ///dtc_SeasonProfile.Month = obj_datetime.Month;
                ///dtc_SeasonProfile.Date = obj_datetime.DayOfMonth;
                ///dtc_SeasonProfile.showDatetime(); //this method of custom control updates GUI

                obj_datetime.Kind = StDateTime.DateTimeType.Date;
                dtc_SeasonProfile_.ContentControl.ValueCustom = obj_datetime;
                combo_season_weekProfile1.SelectedItem = obj_SeasonProfile.Week_Profile;
                SeasonProfile obj_SP = Param_Season.Get_Season_Profile(obj_SeasonProfile.Profile_Name);

                if (obj_SP != null)
                    rdb_Enable.Checked = true;
                else
                    rdb_Enable.Checked = false;

                ///groupBox_SeasonProfile.Text = "Season Profile" + (list_SeasonProfile.SelectedIndex + 1).ToString();
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                Notification nty = new Notification("Error", "Error display season profile");
            }

        }

        public void RefreshGUI_SeasonProfile(SeasonProfile obj_SeasonProfile)
        {
            try
            {
                list_SeasonProfile.Items.Clear();

                foreach (SeasonProfile SP in Calendar.ParamSeasonProfile)
                {
                    list_SeasonProfile.Items.Add(SP);
                    if (obj_SeasonProfile == null)
                        obj_SeasonProfile = SP;
                }
                //list_SeasonProfile.Sorted = true;
                SeasonProfile XSP = Calendar.ParamSeasonProfile.Get_Season_Profile(obj_SeasonProfile.Profile_Name);

                if (XSP != null)
                {
                    list_SeasonProfile.SelectedItem = obj_SeasonProfile;
                    display_SeasonProfile(obj_SeasonProfile);
                }
                else if (list_SeasonProfile.SelectedItem != null)
                {
                    display_SeasonProfile((SeasonProfile)list_SeasonProfile.SelectedItem);
                }
                else if (list_SeasonProfile.Items.Count > 0)
                {
                    list_SeasonProfile.SelectedIndex = 0;
                    display_SeasonProfile((SeasonProfile)list_SeasonProfile.SelectedItem);
                }
                else
                    throw new Exception("There should be at-least single season profile defined");

            }
            catch (Exception ex)
            {
                Notification nty = new Notification("Error", "Error display season profile\r\n" + ex.Message);
            }
        }

        public void Display_Special_Day_Info()
        {

            uint i = (uint)(list_SpecialDays.SelectedIndex + 1);
            Param_SpecialDay ParamSPDay = Calendar.ParamSpecialDay;
            SpecialDay SPDay = ParamSPDay.GetSpecialDay(i);
            if (SPDay != null)
            {
                StDateTime obj_datetime = SPDay.StartDate;

                //dtc_SpecialDays.Year = obj_datetime.Year;
                //dtc_SpecialDays.Month = obj_datetime.Month;
                //dtc_SpecialDays.Date = obj_datetime.DayOfMonth;
                //dtc_SpecialDays.DayOfWeek = obj_datetime.DayOfWeek;
                //dtc_SpecialDays.showDatetime();

                obj_datetime.Kind = StDateTime.DateTimeType.Date;
                //dtc_SpecialDays_.ContentControl.ValueCustom = obj_datetime;
                //ucCustomDateTime Control
                if (ucSPDayProfileDateTime != null)
                    ucSPDayProfileDateTime.showToGUI_StDateTime(obj_datetime);

                combo_SpecialDay_ProfileID.SelectedItem = SPDay.DayProfile;
            }
            Application.DoEvents();
        }

        public void Select_DefaultPage()
        {
            ///Select Default Calendar Page
            this.Profiles.SelectedTab = this.Calendar_Page;
        }

        #endregion

        #region AccessControlMethods

        public bool ApplyAccessRights(List<AccessRights> Rights)
        {
            try
            {
                AccessRights = Rights;

                this.SuspendLayout();
                Profiles.SuspendLayout();

                if (Rights.Find(x => x.Read == true || x.Write == true) != null)
                {
                    Profiles.TabPages.Clear();
                    foreach (var item in Rights)
                    {
                        _HelperAccessRights((ActivityCalender)Enum.Parse(item.QuantityType, item.QuantityName), item.Read, item.Write);
                    }
                    return true;
                }
                return false;
            }
            finally
            {
                this.ResumeLayout();
                Profiles.ResumeLayout();
            }
        }

        private void _HelperAccessRights(ActivityCalender qty, bool read, bool write)
        {
            switch (qty)
            {
                #region ActivityCalender.ActivateCalander
                case ActivityCalender.ActivateCalander:
                    {
                        if (!write)
                        {
                            //rdbInvokeAction.Enabled = false;
                            //rdbActivateOnDate.Enabled = false;
                            //dtc_CalendarActivationDate.Enabled = false;

                            SetControlEnable(false, new Control[] { rdbInvokeAction, rdbActivateOnDate, dtc_CalendarActivationDate, txt_CalendarName });

                        }
                        else
                        {
                            //rdbInvokeAction.Enabled = true;
                            //rdbActivateOnDate.Enabled = true;
                            //dtc_CalendarActivationDate.Enabled = true;

                            SetControlEnable(true, new Control[] { rdbInvokeAction, rdbActivateOnDate, dtc_CalendarActivationDate, txt_CalendarName });
                        }
                        if (!read) Profiles.TabPages.Remove(Calendar_Page);
                        else
                        {
                            if (!Profiles.TabPages.Contains(Calendar_Page))
                            {
                                Profiles.TabPages.Add(Calendar_Page);
                            }
                        }
                        break;
                    }
                #endregion
                #region ActivityCalender.DayProfile
                case ActivityCalender.DayProfile:
                    {
                        if (!write)
                        {
                            //btn_newDayProfile.Enabled = btn_delete_DayProfile.Enabled =
                            //btn_AddDayProfile.Enabled = btn_Save_DayProfile.Enabled =
                            //combo_tariff_NumberofSlots.Enabled = false;

                            //btn_newDayProfile.Visible = btn_delete_DayProfile.Visible =
                            //btn_AddDayProfile.Visible = btn_Save_DayProfile.Visible = false;

                            SetControlEnable(false, new Control[] { btn_newDayProfile, btn_delete_DayProfile, btn_AddDayProfile, btn_Save_DayProfile, combo_tariff_NumberofSlots });
                        }
                        else
                        {
                            //btn_newDayProfile.Enabled = btn_delete_DayProfile.Enabled =
                            //btn_AddDayProfile.Enabled = btn_Save_DayProfile.Enabled =
                            //combo_tariff_NumberofSlots.Enabled = true;

                            //btn_newDayProfile.Visible = btn_delete_DayProfile.Visible =
                            //btn_AddDayProfile.Visible = btn_Save_DayProfile.Visible = true;

                            SetControlEnable(true, new Control[] { btn_newDayProfile, btn_delete_DayProfile, btn_AddDayProfile, btn_Save_DayProfile, combo_tariff_NumberofSlots });

                        }
                        if (!read) Profiles.TabPages.Remove(Day_Profile);
                        else
                        {
                            if (!Profiles.TabPages.Contains(Day_Profile))
                            {
                                Profiles.TabPages.Add(Day_Profile);
                            }
                        }
                        break;
                    }
                #endregion
                #region ActivityCalender.WeekProfile
                case ActivityCalender.WeekProfile:
                    {
                        if (!write)
                        {
                            //btn_newWeekProfile.Enabled = btn_Delete_WeekProfile.Enabled =
                            //btn_Save_WeekProfile.Enabled = false;

                            //btn_newWeekProfile.Visible = btn_Delete_WeekProfile.Visible =
                            //btn_Save_WeekProfile.Visible = false;

                            SetControlEnable(false, new Control[] { btn_newWeekProfile, btn_Delete_WeekProfile, btn_Save_WeekProfile });
                        }
                        else
                        {
                            //btn_newWeekProfile.Enabled = btn_Delete_WeekProfile.Enabled =
                            //btn_Save_WeekProfile.Enabled = true;

                            //btn_newWeekProfile.Visible = btn_Delete_WeekProfile.Visible =
                            //btn_Save_WeekProfile.Visible = false;
                            SetControlEnable(true, new Control[] { btn_newWeekProfile, btn_Delete_WeekProfile, btn_Save_WeekProfile });
                        }
                        if (!read) Profiles.TabPages.Remove(Week_Profile);
                        else
                        {
                            if (!Profiles.TabPages.Contains(Week_Profile))
                            {
                                Profiles.TabPages.Add(Week_Profile);
                            }
                        }
                        break;
                    }
                #endregion
                #region ActivityCalender.SeasonProfile
                case ActivityCalender.SeasonProfile:
                    {
                        if (!write)
                        {
                            //btn_DeleteSeasonProfile.Enabled = btn_addSeasonProfile.Enabled =
                            //btn_Save_SeasonProfile.Enabled = false;

                            //dtc_SeasonProfile_.Enabled = false;

                            //btn_DeleteSeasonProfile.Visible = btn_addSeasonProfile.Visible =
                            //btn_Save_SeasonProfile.Visible = false;

                            SetControlEnable(false, new Control[] { btn_DeleteSeasonProfile, btn_addSeasonProfile, btn_Save_SeasonProfile, dtc_SeasonProfile_ });
                        }
                        else
                        {
                            //btn_DeleteSeasonProfile.Enabled = btn_addSeasonProfile.Enabled =
                            //btn_Save_SeasonProfile.Enabled = true;

                            //dtc_SeasonProfile_.Enabled = true;

                            //btn_DeleteSeasonProfile.Visible = btn_addSeasonProfile.Visible =
                            //btn_Save_SeasonProfile.Visible = true;

                            SetControlEnable(true, new Control[] { btn_DeleteSeasonProfile, btn_addSeasonProfile, btn_Save_SeasonProfile, dtc_SeasonProfile_ });
                        }
                        if (!read) Profiles.TabPages.Remove(_SeasonProfile);
                        else
                        {
                            if (!Profiles.TabPages.Contains(_SeasonProfile))
                            {
                                Profiles.TabPages.Add(_SeasonProfile);
                            }
                        }
                        break;
                    }
                #endregion
                #region ActivityCalender.SpecialDays
                case ActivityCalender.SpecialDays:
                    {
                        if (!write)
                        {
                            //btn_Delete_SpecialDay.Enabled = btn_SpecialDays_add.Enabled =
                            //btn_Save_SpecialDay.Enabled = false;

                            //ucSPDayProfileDateTime.Enabled = false;

                            //btn_Delete_SpecialDay.Visible = btn_SpecialDays_add.Visible =
                            //btn_Save_SpecialDay.Visible = false;

                            SetControlEnable(false, new Control[] { btn_Delete_SpecialDay, btn_SpecialDays_add, btn_Save_SpecialDay, ucSPDayProfileDateTime });
                        }
                        else
                        {
                            //btn_Delete_SpecialDay.Enabled = btn_SpecialDays_add.Enabled =
                            //btn_Save_SpecialDay.Enabled = true;

                            //ucSPDayProfileDateTime.Enabled = true;

                            //btn_Delete_SpecialDay.Visible = btn_SpecialDays_add.Visible =
                            //btn_Save_SpecialDay.Visible = true;

                            SetControlEnable(true, new Control[] { btn_Delete_SpecialDay, btn_SpecialDays_add, btn_Save_SpecialDay, ucSPDayProfileDateTime });
                        }
                        if (!read) Profiles.TabPages.Remove(Special_Days);
                        else
                        {
                            if (!Profiles.TabPages.Contains(Special_Days))
                            {
                                Profiles.TabPages.Add(Special_Days);
                            }
                        }
                        break;
                    }
                #endregion
                default:
                    break;
            }
        }

        #endregion

        #region Support_Fuction

        private void SetControlEnable(bool isEnable, Control[] Controls)
        {
            bool isCalendar = false;
            bool isDayProfileCalendar = false;
            bool isWeekProfileCalendar = false;
            bool isSeasonProfileCalendar = false;
            bool isSpecialDayProfileCalendar = false;

            if (!isEnable)
            {
                foreach (var control in Controls)
                {
                    control.Enabled = false;
                }
            }
            else
            {
                isCalendar = IsControlWTEnable(ActivityCalender.ActivateCalander);
                isDayProfileCalendar = IsControlWTEnable(ActivityCalender.DayProfile);
                isWeekProfileCalendar = IsControlWTEnable(ActivityCalender.WeekProfile);
                isSeasonProfileCalendar = IsControlWTEnable(ActivityCalender.SeasonProfile);
                isSpecialDayProfileCalendar = IsControlWTEnable(ActivityCalender.SpecialDays);

                foreach (var control in Controls)
                {
                    //Calendar Page Control
                    if (CalendarPage_WT_Controls.Contains<Control>(control))
                    {
                        if (isCalendar)
                            control.Enabled = true;
                    }
                    //DayProfiles Page Control
                    else if (Day_Profile_WT_Controls.Contains<Control>(control))
                    {
                        if (isDayProfileCalendar)
                            control.Enabled = true;
                    }
                    //WeekProfile Page Control
                    else if (Week_Profile_WT_Controls.Contains<Control>(control))
                    {
                        if (isWeekProfileCalendar)
                            control.Enabled = true;
                    }
                    //SeasonProfile Page Control
                    else if (Season_Profile_WT_Controls.Contains<Control>(control))
                    {
                        if (isSeasonProfileCalendar)
                            control.Enabled = true;
                    }
                    //SpecialDay Page Control
                    else if (SpecialDay_Profile_WT_Controls.Contains<Control>(control))
                    {
                        if (isSpecialDayProfileCalendar)
                            control.Enabled = true;
                    }
                }
            }
        }

        private void SetControlEnable(bool isEnable, Control Control)
        {
            if (!isEnable)
                Control.Enabled = isEnable;
            else
            {
                //Calendar Page Control
                if (CalendarPage_WT_Controls.Contains<Control>(Control))
                {
                    if (IsControlWTEnable(ActivityCalender.ActivateCalander))
                        Control.Enabled = true;
                }
                //DayProfiles Page Control
                else if (Day_Profile_WT_Controls.Contains<Control>(Control))
                {
                    if (IsControlWTEnable(ActivityCalender.DayProfile))
                        Control.Enabled = true;
                }
                //WeekProfile Page Control
                else if (Week_Profile_WT_Controls.Contains<Control>(Control))
                {
                    if (IsControlWTEnable(ActivityCalender.WeekProfile))
                        Control.Enabled = true;
                }
                //SeasonProfile Page Control
                else if (Season_Profile_WT_Controls.Contains<Control>(Control))
                {
                    if (IsControlWTEnable(ActivityCalender.SeasonProfile))
                        Control.Enabled = true;
                }
                //SpecialDay Page Control
                else if (SpecialDay_Profile_WT_Controls.Contains<Control>(Control))
                {
                    if (IsControlWTEnable(ActivityCalender.SpecialDays))
                        Control.Enabled = true;
                }
            }
        }

        private bool IsControlWTEnable(ActivityCalender type)
        {
            bool isEnable = false;
            try
            {
                isEnable = ApplicationRight.IsControlWTEnable(typeof(ActivityCalender), type.ToString(), AccessRights);
            }
            catch { }
            return isEnable;
        }

        private bool IsControlRDEnable(ActivityCalender type)
        {
            bool isEnable = false;
            try
            {
                isEnable = ApplicationRight.IsControlRDEnable(typeof(ActivityCalender), type.ToString(), AccessRights);
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
