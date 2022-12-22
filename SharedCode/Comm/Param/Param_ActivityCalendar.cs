using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using DLMS.Comm;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SharedCode.Comm.Param
{
    [Serializable]
    [XmlInclude(typeof(Param_SeasonProfile))]
    public class Param_ActivityCalendar : ISerializable, ICloneable,IParam
    {
        private readonly Param_DayProfile paramDayProfile;
        private readonly Param_WeeKProfile paramWeekProfile;
        private readonly Param_SeasonProfile paramSeasonProfile;
        private readonly Param_SpecialDay paramSpecialDay;
        private StDateTime calendarstartDate;
        private String calendarName;
        private String calendarNamePassive;
        private bool executeActivateCalendar = true;

        /// <summary>
        /// Initialize the Param_Activity_Calendar object and register all events & handlers
        /// </summary>
        public Param_ActivityCalendar()
        {
            paramDayProfile = new Param_DayProfile();
            paramWeekProfile = new Param_WeeKProfile();
            paramSeasonProfile = new Param_SeasonProfile();
            paramSpecialDay = new Param_SpecialDay();
            CalendarstartDate = new StDateTime();
            ///Apply Approx after 1 hour
            CalendarName = "MTI_Calendar";
            CalendarNamePassive = "MTI_Calendar";
            ///Register Event Handlers
            ParamDayProfile.VerifyProfileAssigned += ParamWeekProfile.Param_WEEK_PROFILE_VerifyDayProfileAssigned;
            ParamDayProfile.VerifyProfileAssigned += ParamSpecialDay.Param_Special_Day_VerifyProfileAssigned;
            ParamWeekProfile.VerifyProfileAssigned += ParamSeasonProfile.Param_Season_Profile_VerifyProfileAssigned;
            ExecuteActivateCalendarAction = true;
        }

        #region Properties

        public Param_DayProfile ParamDayProfile
        {
            get { return paramDayProfile; }
        }

        public Param_WeeKProfile ParamWeekProfile
        {
            get { return paramWeekProfile; }
        }

        public Param_SeasonProfile ParamSeasonProfile
        {
            get { return paramSeasonProfile; }
        }

        public Param_SpecialDay ParamSpecialDay
        {
            get { return paramSpecialDay; }
        }

        public StDateTime CalendarstartDate
        {
            get { return calendarstartDate; }
            set { calendarstartDate = value; }
        }

        public String CalendarName
        {
            get { return calendarName; }
            set { calendarName = value; }
        }

        public String CalendarNamePassive
        {
            get { return calendarNamePassive; }
            set { calendarNamePassive = value; }
        }

        public bool ExecuteActivateCalendarAction
        {
            get { return executeActivateCalendar; }
            set { executeActivateCalendar = value; }
        }

        public bool IsConsistent
        {
            get
            {
                #region ///Either assigned Week Profile Exists WeekProfile Table
                
                foreach (var seasonProfile in ParamSeasonProfile)
                {
                    bool weekProfileExists = false;
                    foreach (var weekProfile in ParamWeekProfile)
                    {
                        if (weekProfile == seasonProfile.Week_Profile)
                        {
                            weekProfileExists = true;
                            break;
                        }
                    }
                    if (!weekProfileExists)
                        return false;
                }

                #endregion
                #region ///Either assigned SpecialProfileTable DayProfile Exists DayProfileTable
                foreach (var specialDayProfile in this.ParamSpecialDay)
                {
                    bool dayProfileExists = false;
                    foreach (var dayProfile in this.ParamDayProfile)
                    {
                        if (specialDayProfile.DayProfile == dayProfile)
                        {
                            dayProfileExists = true;
                            break;
                        }
                    }
                    if (!dayProfileExists)
                        return false;
                }
                #endregion
                #region ///Either assigned WeekProfile Day Profile Exists in DayProfile Table
                foreach (var weekProfile in ParamWeekProfile)
                {
                    if (ParamDayProfile.First((x) => x == weekProfile.Day_Profile_MON) == null)     ///Monday Day Profile Never exists
                        return false;
                    if (ParamDayProfile.First((x) => x == weekProfile.Day_Profile_TUE) == null)     ///Tuesday Day Profile Never exists
                        return false;
                    if (ParamDayProfile.First((x) => x == weekProfile.Day_Profile_WED) == null)     ///Wednesday Day Profile Never exists
                        return false;
                    if (ParamDayProfile.First((x) => x == weekProfile.Day_Profile_THRU) == null)     ///Thursday Day Profile Never exists
                        return false;
                    if (ParamDayProfile.First((x) => x == weekProfile.Day_Profile_FRI) == null)     ///Friday Day Profile Never exists
                        return false;
                    if (ParamDayProfile.First((x) => x == weekProfile.Day_Profile_SAT) == null)     ///Saturday Day Profile Never exists
                        return false;
                    if (ParamDayProfile.First((x) => x == weekProfile.Day_Profile_SUN) == null)     ///Sunday Day Profile Never exists
                        return false;
                }
                #endregion
                ///Consistency Checks
                return ParamDayProfile.IsConsistent &&
                       ParamWeekProfile.IsConsistent &&
                       ParamSeasonProfile.IsConsistent &&
                       ParamSpecialDay.IsConsistent;
            }
        }

        #endregion

        #region ISerializable Members

        protected Param_ActivityCalendar(SerializationInfo info, StreamingContext context)
        {
            ///Getting CalendarName Type String
            this.CalendarName = info.GetString("CalendarName");
            try
            {
                ///Getting CalendarName Type String
                this.CalendarNamePassive = info.GetString("CalendarNamePassive");
            }
            catch
            {
                this.CalendarNamePassive = this.CalendarName;
            }
            ///Getting CalendarStartDateTime Type StDateTime
            this.CalendarstartDate = (StDateTime)info.GetValue("CalendarStartDateTime", typeof(StDateTime));
            ///Getting Parameters_DayProfile Type Param_DayProfile
            this.paramDayProfile = (Param_DayProfile)info.GetValue("Parameters_DayProfile", typeof(Param_DayProfile));
            ///Getting Parameters_WeekProfile Type Param_WeekProfile
            this.paramWeekProfile = (Param_WeeKProfile)info.GetValue("Parameters_WeekProfile", typeof(Param_WeeKProfile));
            ///Adding Parameters_SeasonProfile Type Param_WeekProfile
            this.paramSeasonProfile = (Param_SeasonProfile)info.GetValue("Parameters_SeasonProfile", typeof(Param_SeasonProfile));
            ///Adding Parameters_SpecialDayProfile Type Param_SpecialDay
            this.paramSpecialDay = (Param_SpecialDay)info.GetValue("Parameters_SpecialDayProfile", typeof(Param_SpecialDay));
            ///Attach Event Handlers
            ParamDayProfile.VerifyProfileAssigned += ParamWeekProfile.Param_WEEK_PROFILE_VerifyDayProfileAssigned;
            ParamDayProfile.VerifyProfileAssigned += ParamSpecialDay.Param_Special_Day_VerifyProfileAssigned;
            ParamWeekProfile.VerifyProfileAssigned += ParamSeasonProfile.Param_Season_Profile_VerifyProfileAssigned;
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ///Adding CalendarName Type String
            info.AddValue("CalendarName", this.CalendarName);
            ///Adding CalendarName Type String
            info.AddValue("CalendarNamePassive", this.CalendarNamePassive);
            ///Adding CalendarStartDateTime Type StDateTime
            info.AddValue("CalendarStartDateTime", this.CalendarstartDate);
            ///Adding Parameters_DayProfile Type Param_DayProfile
            info.AddValue("Parameters_DayProfile", this.ParamDayProfile);
            ///Adding Parameters_WeekProfile Type Param_WeekProfile
            info.AddValue("Parameters_WeekProfile", this.ParamWeekProfile);
            ///Adding Parameters_SeasonProfile Type Param_WeekProfile
            info.AddValue("Parameters_SeasonProfile", this.ParamSeasonProfile);
            ///Adding Parameters_SpecialDayProfile Type Param_SpecialDay
            info.AddValue("Parameters_SpecialDayProfile", this.ParamSpecialDay);
        }

        #endregion

        public object Clone()
        {
            MemoryStream memStream = null;
            Object dp = null;
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (memStream = new MemoryStream(256))
                {
                    formatter.Serialize(memStream, this);
                    memStream.Seek(0, SeekOrigin.Begin);
                    dp = formatter.Deserialize(memStream);
                }
                return dp;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while Clone Object", ex);
            }
        }
    }
}
