using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLMS.Comm
{
    // <summary>
    
    // </summary>
    public class StDayProfileAction
    {
        #region Data_Members

        private StDateTime startTime;
        private byte[] script_logicalName;
        private ushort scriptSelector;

        #endregion

        #region Constructor

        public StDayProfileAction()
        {
            startTime = new StDateTime();
            script_logicalName = new byte[6];
            scriptSelector = 1;
        }

        public StDayProfileAction(StDateTime StartTime, byte[] Script_logicalName, ushort ScriptSelector)
        {
            this.StartTime = StartTime;
            this.Script_logicalName = Script_logicalName;
            this.ScriptSelector = ScriptSelector;
        }

        #endregion

        #region Properties

        public StDateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public byte[] Script_logicalName
        {
            get { return script_logicalName; }
            set
            {
                script_logicalName = value;
            }
        }

        public ushort ScriptSelector
        {
            get { return scriptSelector; }
            set { scriptSelector = value; }
        }
        #endregion
    }

    /// <summary>
    /// An attribute of the interface class "Activity calendar". It defines an ordered list of actions and
    /// the corresponding activation times for each day type.
    /// </summary>
    public class StDayProfile
    {
        private byte dayId;
        private List<StDayProfileAction> daySchedule;

        public StDayProfile()
        {
            dayId = 0x00;
            daySchedule = new List<StDayProfileAction>(0x06);
        }

        public StDayProfile(byte DayId, List<StDayProfileAction> DaySchedule)
        {
            this.DayId = DayId;
            this.DaySchedule = DaySchedule;
        }

        public List<StDayProfileAction> DaySchedule
        {
            get { return daySchedule; }
            set { daySchedule = value; }
        }

        public byte DayId
        {
            get { return dayId; }
            set { dayId = value; }
        }
    }
    
    /// <summary>
    /// An attribute of the interface class "Activity calendar". It defines the name of the day profiles to be used for every day of the week in a particular season.
    /// </summary>
    public class StWeekProfile
    {
        private string profileName;
        private byte dayProfileIdMon;
        private byte dayProfileIdTue;
        private byte dayProfileIdWed;
        private byte dayProfileIdThu;
        private byte dayProfileIdFri;
        private byte dayProfileIdSat;
        private byte dayProfileIdSun;

        public StWeekProfile()
        {
            profileName = "";
        }

        public StWeekProfile(String WeekProfileName,
            byte DPIdMon,
            byte DPIdTue,
            byte DPIdWed,
            byte DPIdThu,
            byte DPIdFri,
            byte DPIdSat,
            byte DPIdSun)
        {
            this.ProfileName = WeekProfileName;
            this.DayProfileIdMon = DPIdMon;
            this.DayProfileIdTue = DPIdTue;
            this.DayProfileIdWed = DPIdWed;
            this.DayProfileIdThu = DPIdThu;
            this.DayProfileIdFri = DPIdFri;
            this.DayProfileIdSat = DPIdSat;
            this.DayProfileIdSun = DPIdSun;
        }

        public string ProfileName
        {
            get { return profileName; }
            set { profileName = value; }
        }

        public byte DayProfileIdMon
        {
            get { return dayProfileIdMon; }
            set { dayProfileIdMon = value; }
        }

        public byte DayProfileIdTue
        {
            get { return dayProfileIdTue; }
            set { dayProfileIdTue = value; }
        }

        public byte DayProfileIdWed
        {
            get { return dayProfileIdWed; }
            set { dayProfileIdWed = value; }
        }

        public byte DayProfileIdThu
        {
            get { return dayProfileIdThu; }
            set { dayProfileIdThu = value; }
        }

        public byte DayProfileIdFri
        {
            get { return dayProfileIdFri; }
            set { dayProfileIdFri = value; }
        }

        public byte DayProfileIdSat
        {
            get { return dayProfileIdSat; }
            set { dayProfileIdSat = value; }
        }

        public byte DayProfileIdSun
        {
            get { return dayProfileIdSun; }
            set { dayProfileIdSun = value; }
        }

    }
    
    /// <summary>
    /// An attribute of the interface class "Activity calendar".
    /// It contains a list defining the starting date of seasons.
    /// This list is sorted according to season_start. Each season activates a specific week_profile.
    /// </summary>
    public class StSeasonProfile
    {
        private String profileName;
        private StDateTime startDate;
        private String weekProfileName;

        public StSeasonProfile()
        {
            ProfileName = "";
            StartDate = new StDateTime();
            WeekProfileName = "";
        }

        public StSeasonProfile(String SeasonProfileName, StDateTime StartDate, String WeekProfileName)
        {
            this.ProfileName = SeasonProfileName;
            this.StartDate = StartDate;
            this.WeekProfileName = WeekProfileName;
        }
        /// <summary>
        /// Get or Set the Profile name to identify the season
        /// </summary>
        public String ProfileName
        {
            get { return profileName; }
            set { profileName = value; }
        }
        /// <summary>
        /// Get or Set the start Date of the season (on which date teriff for this season to be activate)
        /// </summary>
        public StDateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        /// <summary>
        /// Get or Set the Week Profile Name for the Season 
        /// </summary>
        public String WeekProfileName
        {
            get { return weekProfileName; }
            set { weekProfileName = value; }
        }
    }

    /// <summary>
    /// A COSEM interface class allowing defining dates, on which a special switching behaviour will override normal switching behaviour.
    /// “Special days table” interface objects work in conjunction with the interface objects “Schedule” and “Activity calendar”.
    /// </summary>
    public class StSpecialDayProfile
    {
        #region Data_Member
        
        private ushort index;
        private StDateTime date;
        private byte dayProfileId;
        
        #endregion

        #region Properties
        public ushort Index
        {
            get { return index; }
            set { index = value; }
        }


        public byte DayProfileId
        {
            get { return dayProfileId; }
            set { dayProfileId = value; }
        }

        public StDateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        #endregion

        #region Constructor
        public StSpecialDayProfile()
        {
            Date = new StDateTime();
        }

        public StSpecialDayProfile(ushort SpecialDayIndex, StDateTime SpecialDayDate, byte DayProfileId)
        {
            this.Index = SpecialDayIndex;
            this.DayProfileId = DayProfileId;
            this.Date = SpecialDayDate;
        }

        #endregion
    }

    public enum DateTimeWildCardMask : ushort
    {
        // DateTime Data Members Valid Values Flags
        YearNotSpecified = 0x01,
        MonthInValid = 0x02,
        DayOfMonthInValid = 0x04,
        DayOfWeekInValid = 0x08,
        HourInValid = 0x010,
        MinuteInValid = 0x20,
        SecondInValid = 0x30,
        HundredSecInvalid = 0x40,
        // Individual DateTime Masks
        HourNotSpecified = 0x2F,
        MinNotSpecified = 0x2E
    }

}
