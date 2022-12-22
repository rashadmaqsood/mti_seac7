using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Xml.Serialization;
using DLMS;
using DLMS.Comm;

namespace comm
{
    #region Param_WeekProfile
    
    [Serializable]
    [XmlInclude(typeof(DayProfile))]
    public class Param_WeeKProfile : IEnumerable<WeekProfile>,ICloneable ,ISerializable
    {
        public static readonly String WeekProfileNamePre = "WeekProfile_";
        public List<WeekProfile> weekProfile_Table;
        public event Action<WeekProfile> VerifyProfileAssigned = delegate { };

        public Param_WeeKProfile()
        {
            weekProfile_Table = new List<WeekProfile>(WeekProfile.MAX_Week_Profile_Count);
        }

        public bool IsConsistent
        {
            get
            {
                foreach (var item in weekProfile_Table)
                {
                    return item.IsConsistent;
                }
                if (weekProfile_Table.Count > 0)
                    return true;
                else
                    return false;
            }
        }

        private WeekProfile Create_Week_Profile(String WeekProfileName)
        {
            try
            {
                ///Check Correctness of Week Profile Table
                if (weekProfile_Table.Count >= WeekProfile.MAX_Week_Profile_Count)
                    throw new Exception(String.Format("{0} Maximum no of profiles are already defined", WeekProfile.MAX_Week_Profile_Count));
                if (weekProfile_Table.Exists((x) => x.Week_Profile_Name.Equals(WeekProfileName)))    ///Check Either Already Week Profile Defined
                    throw new Exception(String.Format("{0} Week Profile already exists", WeekProfileName));
                
                WeekProfile newProfile = new WeekProfile(WeekProfileName);
                weekProfile_Table.Add(newProfile);
                ///Sort Week Profile By Name
                weekProfile_Table.Sort((x, y) => x.Week_Profile_Name.CompareTo(y.Week_Profile_Name));
                return newProfile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public WeekProfile Create_Week_Profile()
        {
            int wkId = (weekProfile_Table.Count == 0) ? 1 : weekProfile_Table.Count + 1;
            String WeekProfileName = "" + Convert.ToChar((byte)wkId);
            return Create_Week_Profile(WeekProfileName);
        }

        private void Delete_Week_Profile(String WeekProfileName)
        {
            try
            {
                WeekProfile WkProfile = weekProfile_Table.Find((x) => x.Week_Profile_Name.Equals(WeekProfileName));
                if (WkProfile == null)
                    throw new Exception(String.Format("Week Profile {0} never exists", WeekProfileName));
                VerifyProfileAssigned.Invoke(WkProfile);        ///Verify Week Profile not already Assign
                weekProfile_Table.Remove(WkProfile);
                ///Sort Week Profile By Name
                weekProfile_Table.Sort((x, y) => x.Week_Profile_Name.CompareTo(y.Week_Profile_Name));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete_Week_Profile()
        {
            WeekProfile WK_Profile = Get_Last_Week_Profile();
            Delete_Week_Profile(WK_Profile.Week_Profile_Name);
        }

        public WeekProfile Get_Last_Week_Profile()
        {
            if (weekProfile_Table.Count > 0)
            {
                ///weekProfile_Table.Sort((x, y) => x.Week_Profile_Name.CompareTo(y.Week_Profile_Name));
                return weekProfile_Table[weekProfile_Table.Count - 1];
            }
            else
                return null;

        }

        public WeekProfile Get_Week_Profile(String Week_Profile_Name)
        {
            foreach (var item in weekProfile_Table)
            {
                if (item.Week_Profile_Name.Equals(Week_Profile_Name))
                    return item;
            }
            return null;
        }

        public WeekProfile Get_Week_Profile(ushort Week_Profile_ID)
        {
            String WeekProfileName = "" + Convert.ToChar(Week_Profile_ID);
            return Get_Week_Profile(WeekProfileName);
        }

        public DLMS.Base_Class Encode_Week_Profile(GetSAPEntry CommObjectGetter)
        {
            try
            {
                DLMS.Class_20 ActivityCalendar = (DLMS.Class_20)CommObjectGetter.Invoke(Get_Index.Activity_Calendar);
                ActivityCalendar.EncodingAttribute = 8;               ///DayProfileListActive
                List<StWeekProfile> weekProfileTable = new List<StWeekProfile>();
                foreach (var item in this.weekProfile_Table)
                {
                    StWeekProfile wk = new StWeekProfile(item.Week_Profile_Name, item.Day_Profile_MON.Day_Profile_ID, item.Day_Profile_TUE.Day_Profile_ID,
                        item.Day_Profile_WED.Day_Profile_ID, item.Day_Profile_THRU.Day_Profile_ID, item.Day_Profile_FRI.Day_Profile_ID,
                        item.Day_Profile_SAT.Day_Profile_ID, item.Day_Profile_SUN.Day_Profile_ID);
                    weekProfileTable.Add(wk);
                }
                ActivityCalendar.WeekProfilePassive = weekProfileTable;
                return ActivityCalendar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Decode_Week_Profile(Base_Class arg, Param_DayProfile ParamDayProfiles)
        {
            DLMS.Class_20 ActivityCalendar = (DLMS.Class_20)arg;
            ///Verify data Receiced/OBIS/ETC
            if (ActivityCalendar.GetAttributeDecodingResult(4) == DecodingResult.Ready)   ///Attribute Ready
            {
                List<StWeekProfile> weekProfileTable = ActivityCalendar.WeekProfileActive;
                weekProfile_Table.Clear();
                foreach (var item in weekProfileTable)
                {
                    WeekProfile wk = Create_Week_Profile(item.ProfileName);
                    DayProfile dp = ParamDayProfiles.GetDayProfile(item.DayProfileIdMon);
                    wk.Day_Profile_MON = dp;
                    dp = ParamDayProfiles.GetDayProfile(item.DayProfileIdTue);
                    wk.Day_Profile_TUE = dp;
                    dp = ParamDayProfiles.GetDayProfile(item.DayProfileIdWed);
                    wk.Day_Profile_WED = dp;
                    dp = ParamDayProfiles.GetDayProfile(item.DayProfileIdThu);
                    wk.Day_Profile_THRU = dp;
                    dp = ParamDayProfiles.GetDayProfile(item.DayProfileIdFri);
                    wk.Day_Profile_FRI = dp;
                    dp = ParamDayProfiles.GetDayProfile(item.DayProfileIdSat);
                    wk.Day_Profile_SAT = dp;
                    dp = ParamDayProfiles.GetDayProfile(item.DayProfileIdSun);
                    wk.Day_Profile_SUN = dp;
                }
            }
        }

        public void Param_WEEK_PROFILE_VerifyDayProfileAssigned(DayProfile obj)
        {
            foreach (var item in weekProfile_Table)
            {
                if (obj.Equals(item.Day_Profile_MON) ||
                    obj.Equals(item.Day_Profile_TUE) ||
                    obj.Equals(item.Day_Profile_WED) ||
                    obj.Equals(item.Day_Profile_THRU) ||
                    obj.Equals(item.Day_Profile_FRI) ||
                    obj.Equals(item.Day_Profile_SAT) ||
                    obj.Equals(item.Day_Profile_SUN)
                    )

                    throw new Exception(String.Format("Day Profile is assigned in {0}", item.ToString()));
            }
        }

        #region IEnumerable<Week_Profile> Members

        public IEnumerator<WeekProfile> GetEnumerator()
        {
            return weekProfile_Table.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return weekProfile_Table.GetEnumerator();
        }

        #endregion

        #region ISerializable Members

        protected Param_WeeKProfile(SerializationInfo info, StreamingContext context)
        {
            ///Getting WeekProfiles Count <Type int>
            int WeekProfileCount =  info.GetInt32("WeekProfiles");
            this.weekProfile_Table = new List<WeekProfile>();
            for (int index = 0; index < WeekProfileCount; index++)
            {
                /////Adding DayProfile Type DayProfile
                String keyRet = String.Format("WeekProfile_{0:X2}", index);
                WeekProfile wk = (WeekProfile)info.GetValue(keyRet, typeof(WeekProfile));
                this.weekProfile_Table.Add(wk);
            }
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ///Adding WeekProfiles Count <Type int>
            info.AddValue("WeekProfiles", this.weekProfile_Table.Count);
            for (int index = 0; index < weekProfile_Table.Count; index++)
            {
                ///Adding DayProfile Type DayProfile
                String keyRet = String.Format("WeekProfile_{0:X2}", index);
                info.AddValue(keyRet, weekProfile_Table[index]);
            }
        }

        #endregion

        public object Clone()
        {
            MemoryStream memStream = null;
            Object dp = null;
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (memStream = new MemoryStream(1024))
                {
                    formatter.Serialize(memStream, this);
                    memStream.Seek(0, SeekOrigin.Begin);
                    dp = formatter.Deserialize(memStream);
                }
                return dp;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while Clone Param_WeekProfile", ex);
            }
        }
    } 
    
    #endregion

    #region WeekProfile
    
    [Serializable]
    [XmlInclude(typeof(DayProfile))]
    public class WeekProfile : ISerializable,ICloneable
    {
        #region DataMembers

        public static readonly ushort MAX_Week_Profile_Count = 0x04;
        private  string week_profile_name;
        private DayProfile day_Profile_MON;
        private DayProfile day_Profile_TUE;
        private DayProfile day_Profile_WED;
        private DayProfile day_Profile_THRU;
        private DayProfile day_Profile_FRI;
        private DayProfile day_Profile_SAT;
        private DayProfile day_Profile_SUN;

        #endregion

        #region Constructur

        public WeekProfile(String profileName)
        {
            if (String.IsNullOrEmpty(profileName))
                throw new Exception(String.Format("Invalid Week Profile quantity_name {0}", profileName));
            week_profile_name = profileName;
        }

        #endregion

        #region Properties

        public string Profile_Name_Str
        {
            get
            {
                try
                {
                    String WeekName = "" + week_profile_name;
                    if (Convert.ToString("" + (char)1).
                            Equals(this.week_profile_name, StringComparison.OrdinalIgnoreCase))
                    {
                        WeekName = Param_WeeKProfile.WeekProfileNamePre + 1;
                    }
                    else if (Convert.ToString("" + (char)2).
                            Equals(this.week_profile_name, StringComparison.OrdinalIgnoreCase))
                    {
                        WeekName = Param_WeeKProfile.WeekProfileNamePre + 2;
                    }
                    else if (Convert.ToString("" + (char)3).
                        Equals(this.week_profile_name, StringComparison.OrdinalIgnoreCase))
                    {
                        WeekName = Param_WeeKProfile.WeekProfileNamePre + 3;
                    }
                    else if (Convert.ToString("" + (char)4).
                        Equals(this.week_profile_name, StringComparison.OrdinalIgnoreCase))
                    {
                        WeekName = Param_WeeKProfile.WeekProfileNamePre + 4;
                    }
                    else
                        WeekName = week_profile_name;
                    ///Console.Out.WriteLine(Char.GetNumericValue(Profile_Name[0]));
                    return WeekName;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            set
            {
                if (Convert.ToString(Param_SeasonProfile.SeasonProfileNamePre + 1).
                    Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    this.week_profile_name = Convert.ToString("" + (char)1);
                }
                else if (Convert.ToString(Param_SeasonProfile.SeasonProfileNamePre + 2).
                    Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    this.week_profile_name = Convert.ToString("" + (char)2);
                }
                else if (Convert.ToString(Param_SeasonProfile.SeasonProfileNamePre + 3).
                    Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    this.week_profile_name = Convert.ToString("" + (char)3);
                }
                else if (Convert.ToString(Param_SeasonProfile.SeasonProfileNamePre + 4).
                    Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    this.week_profile_name = Convert.ToString("" + (char)4);
                }
                else
                    this.week_profile_name = value;
                ///Console.Out.WriteLine(Char.GetNumericValue(profile_Name[0]));
            }
        }
        
        public bool IsConsistent
        {
            get     ///Check all day profiles assigned & profiles are also consistent
            {
                if (Day_Profile_MON != null && Day_Profile_MON.IsConsistent &&
                   Day_Profile_TUE != null && Day_Profile_TUE.IsConsistent &&
                   Day_Profile_WED != null && Day_Profile_WED.IsConsistent &&
                   Day_Profile_THRU != null && Day_Profile_THRU.IsConsistent &&
                   Day_Profile_FRI != null && Day_Profile_FRI.IsConsistent &&
                   Day_Profile_SAT != null && Day_Profile_SAT.IsConsistent &&
                   Day_Profile_SUN != null && Day_Profile_SUN.IsConsistent
                    )
                    return true;
                else
                    return false;

            }
        }

        public string Week_Profile_Name
        {
            get { return week_profile_name; }
        }

        public DayProfile Day_Profile_MON
        {
            get { return day_Profile_MON; }
            set { day_Profile_MON = value; }
        }

        public DayProfile Day_Profile_TUE
        {
            get { return day_Profile_TUE; }
            set { day_Profile_TUE = value; }
        }

        public DayProfile Day_Profile_WED
        {
            get { return day_Profile_WED; }
            set { day_Profile_WED = value; }
        }

        public DayProfile Day_Profile_THRU
        {
            get { return day_Profile_THRU; }
            set { day_Profile_THRU = value; }
        }

        public DayProfile Day_Profile_FRI
        {
            get { return day_Profile_FRI; }
            set { day_Profile_FRI = value; }
        }

        public DayProfile Day_Profile_SAT
        {
            get { return day_Profile_SAT; }
            set { day_Profile_SAT = value; }
        }

        public DayProfile Day_Profile_SUN
        {
            get { return day_Profile_SUN; }
            set { day_Profile_SUN = value; }
        }

        #endregion

        #region Member_Methods
        public override string ToString()
        {
            try
            {
                String wkName = null;
                ///Custom Modification For WeekProfileName 1,2,3,4
                if (("" + (char)1).Equals(Week_Profile_Name))
                {
                    wkName = Param_WeeKProfile.WeekProfileNamePre + 1;
                }
                else if (("" + (char)2).Equals(Week_Profile_Name))
                {
                    wkName = Param_WeeKProfile.WeekProfileNamePre + 2;
                }
                else if (("" + (char)3).Equals(Week_Profile_Name))
                {
                    wkName = Param_WeeKProfile.WeekProfileNamePre + 3;
                }
                else if (("" + (char)4).Equals(Week_Profile_Name))
                {
                    wkName = Param_WeeKProfile.WeekProfileNamePre + 4;
                }
                else
                    wkName = Week_Profile_Name;
                //if(Week_Profile_Name.Equals()
                return wkName;
            }
            catch (Exception ex)
            {
                return "Error Decoding WeekProfileId";
            }
        }
        #endregion

        #region ISerializable Members

        protected WeekProfile(SerializationInfo info, StreamingContext context)
        {
            ///Getting WeekProfileName Type String
            this.week_profile_name = info.GetString("WeekProfileName");
            ///Getting DayProfileMonday Type DayProfile
            this.day_Profile_MON = (DayProfile)info.GetValue("DayProfileMonday", typeof(DayProfile));
            ///Getting DayProfileTuesday Type DayProfile
            this.day_Profile_TUE = (DayProfile)info.GetValue("DayProfileTuesday", typeof(DayProfile));
            ///Getting DayProfileWednesday Type DayProfile
            this.day_Profile_WED = (DayProfile)info.GetValue("DayProfileWednesday", typeof(DayProfile));
            ///Getting DayProfileThursday Type DayProfile
            this.day_Profile_THRU = (DayProfile)info.GetValue("DayProfileThursday", typeof(DayProfile));
            ///Getting DayProfileFriday Type DayProfile
            this.day_Profile_FRI = (DayProfile)info.GetValue("DayProfileFriday", typeof(DayProfile));
            ///Getting DayProfileSaturday Type DayProfile
            this.day_Profile_SAT = (DayProfile)info.GetValue("DayProfileSaturday", typeof(DayProfile));
            ///Getting DayProfileSunday Type DayProfile
            this.day_Profile_SUN = (DayProfile)info.GetValue("DayProfileSunday", typeof(DayProfile));
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ///Adding WeekProfileName Type String
            info.AddValue("WeekProfileName", this.Week_Profile_Name);
            ///Adding DayProfileMonday Type DayProfile
            info.AddValue("DayProfileMonday", this.Day_Profile_MON);
            ///Adding DayProfileTuesday Type DayProfile
            info.AddValue("DayProfileTuesday", this.Day_Profile_TUE);
            ///Adding DayProfileWednesday Type DayProfile
            info.AddValue("DayProfileWednesday", this.Day_Profile_WED);
            ///Adding DayProfileThursday Type DayProfile
            info.AddValue("DayProfileThursday", this.Day_Profile_THRU);
            ///Adding DayProfileFriday Type DayProfile
            info.AddValue("DayProfileFriday", this.Day_Profile_FRI);
            ///Adding DayProfileSaturday Type DayProfile
            info.AddValue("DayProfileSaturday", this.Day_Profile_SAT);
            ///Adding DayProfileSunday Type DayProfile
            info.AddValue("DayProfileSunday", this.Day_Profile_SUN);
        }

        #endregion

        public object Clone()
        {
            MemoryStream memStream = null;
            Object dp = null;
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (memStream = new MemoryStream(1024))
                {
                    formatter.Serialize(memStream, this);
                    memStream.Seek(0, SeekOrigin.Begin);
                    dp = formatter.Deserialize(memStream);
                }
                return dp;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while Clone WeekProfile", ex);
            }
        }
    } 
    
    #endregion
}