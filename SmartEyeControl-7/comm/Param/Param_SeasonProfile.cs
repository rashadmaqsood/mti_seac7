using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartEyeControl_7.Controllers;
using DLMS;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DLMS.Comm;

namespace comm
{
    [Serializable]
    [XmlInclude(typeof(Param_SeasonProfile))]
    [XmlInclude(typeof(SeasonProfile))]
    public class Param_SeasonProfile : IEnumerable<SeasonProfile>, ISerializable, ICloneable
    {
        public List<SeasonProfile> seasonProfile_Table;
        public static readonly String SeasonProfileNamePre = "SeasonProfile_";

        public Param_SeasonProfile()  ///Constructor to initialize valid start date
        {
            ///Define Season Profile MAX
            seasonProfile_Table = new List<SeasonProfile>(SeasonProfile.MAX_Season_Profiles);
        }

        public bool IsConsistent
        {
            get
            {
                foreach (var item in seasonProfile_Table)
                {
                    if (!item.IsConsistent)
                        return false;
                }
                if (seasonProfile_Table.Count > 0 &&
                    !IsSeasonProfilesOverlapping(seasonProfile_Table))
                    return true;
                else
                    return false;
            }
        }

        public SeasonProfile Create_Season_Profile(string SeasonProfileName)
        {
            try
            {
                if (seasonProfile_Table.Count < SeasonProfile.MAX_Season_Profiles)
                {
                    if (seasonProfile_Table.Exists((x) => x.Profile_Name_Str.Equals(SeasonProfileName)))
                        throw new Exception(String.Format("Season Profile {0},already defined", SeasonProfileName));
                    SeasonProfile newSP = new SeasonProfile(SeasonProfileName);
                    newSP.StartDate_Changed += new Action<SeasonProfile>(SeasonProfile_StartDate_Changed);
                    seasonProfile_Table.Add(newSP);

                    seasonProfile_Table.Sort((x, y) => x.Start_Date.CompareTo(y.Start_Date));
                    return newSP;
                }
                else
                    throw new Exception(String.Format("Unable to create Season Profile,maximum {0} profiles already created", SeasonProfile.MAX_Season_Profiles));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SeasonProfile Create_Season_Profile()
        {
            try
            {
                int seasonId = seasonProfile_Table.Count + 1;
                return Create_Season_Profile("" + Convert.ToChar(seasonId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete_Season_Profile(String SeasonProfileName)
        {
            try
            {
                SeasonProfile SPProfile = seasonProfile_Table.Find((x) => x.Profile_Name.Equals(SeasonProfileName));
                if (SPProfile != null)
                    seasonProfile_Table.Remove(SPProfile);
                else
                    throw new Exception(String.Format("{0} Season Profile never exists", SeasonProfileName));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete_Season_Profile()
        {
            try
            {
                SeasonProfile spE = Get_Last_Season_Profile();
                if (spE != null)
                    Delete_Season_Profile(spE.Profile_Name);
                else
                    throw new Exception("No Season Profiles exists");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SeasonProfile Get_Season_Profile(String SeasonProfileName)
        {
            try
            {
                foreach (var item in seasonProfile_Table)
                {
                    if (item.Profile_Name.Equals(SeasonProfileName))
                        return item;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SeasonProfile Get_Season_Profile(ushort SeasonProfileID)
        {
            return Get_Season_Profile("" + Convert.ToChar(SeasonProfileID));
        }

        public SeasonProfile Get_Last_Season_Profile()
        {
            try
            {
                if (seasonProfile_Table.Count > 0)
                    return seasonProfile_Table[seasonProfile_Table.Count - 1];
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #region Season Profile Overlapping

        private void SeasonProfile_StartDate_Changed(SeasonProfile obj)
        {
            try
            {
                SeasonProfile OverlappingSeasonProfile = null;
                if (IsSeasonProfilesOverlapping(seasonProfile_Table, out OverlappingSeasonProfile))        ///Check TimeSlice OverLapping
                {
                    if (OverlappingSeasonProfile == obj)
                        throw new Exception(String.Format("{0} 's Season Profile Overlapping", obj.ToString()));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool IsSeasonProfilesOverlapping(List<SeasonProfile> SeasonProfiles, out SeasonProfile OverlappingSeasonProfile)
        {
            OverlappingSeasonProfile = null;
            try
            {
                //Sort SeasonProfile Based On Start Time Values //ahmed
                //SeasonProfiles.Sort((x, y) => (((x == null) ? -1 : (y == null) ? 1 : x.Start_Date.CompareTo(y.Start_Date))));
                OverlappingSeasonProfile = null;
                return false;
                for (int index = 0; index < SeasonProfiles.Count - 1; index++)
                {
                    // If two timeSlices equals
                    if (SeasonProfiles[index] == null ||
                        SeasonProfiles[index + 1] == null ||
                        SeasonProfiles[index].Start_Date.CompareTo(SeasonProfiles[index + 1].Start_Date) >= 0)
                    {
                        OverlappingSeasonProfile = SeasonProfiles[index];
                        return true;
                    }
                    else
                    {
                        ///(SeasonProfile_1 < SesasonProfile_2) ==>True 
                        //if (SeasonProfiles[index].Profile_Name.CompareTo(SeasonProfiles[index + 1].Profile_Name) == -1)
                        continue;
                        //else
                        //{
                        //    OverlappingSeasonProfile = SeasonProfiles[index];
                        //    return true;
                        //}
                    }
                }
                OverlappingSeasonProfile = null;
                return false;
            }
            catch
            {
                OverlappingSeasonProfile = null;
                return false;
                ///throw ex;
            }
            finally
            {
                //SeasonProfiles.Sort((x, y) => x.Profile_Name.CompareTo(y.Profile_Name));
                //Sort List Based On Season ProfileName
            }
        }

        //private bool IsSeasonProfilesOverlapping(List<SeasonProfile> SeasonProfilesarg, out SeasonProfile OverlappingSeasonProfile)
        //{
        //    try
        //    {
        //        List<SeasonProfile> SeasonProfiles = new List<SeasonProfile>(SeasonProfilesarg);
        //        OverlappingSeasonProfile = null;
        //        return false;
        //        ///Sort SeasonProfile Based On Start Time Values //ahmed
        //        SeasonProfiles.Sort((x, y) => (((x == null) ? -1 : (y == null) ? 1 : x.Start_Date.CompareTo(y.Start_Date))));
        //        for (int index = 0; index < SeasonProfiles.Count - 1; index++)
        //        {

        //            // If two timeSlices equals
        //            if (SeasonProfiles[index].Start_Date.CompareTo(SeasonProfiles[index + 1].Start_Date) == 0 ||
        //                SeasonProfiles[index] == null ||
        //                SeasonProfiles[index + 1] == null)
        //            {
        //                OverlappingSeasonProfile = SeasonProfiles[index];
        //                return true;
        //            }
        //            else
        //            {
        //                ///(SeasonProfile_1 < SesasonProfile_2) ==>True 
        //                if (SeasonProfiles[index].Profile_Name.CompareTo(SeasonProfiles[index + 1].Profile_Name) == -1)
        //                    continue;
        //                else
        //                {
        //                    OverlappingSeasonProfile = SeasonProfiles[index];
        //                    return true;
        //                }
        //            }
        //        }
        //        OverlappingSeasonProfile = null;
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        OverlappingSeasonProfile = null;
        //        return false;
        //        //    throw ex;
        //    }
        //    finally
        //    {
        //        //seasonProfile_Table.Sort((x, y) => x.Profile_Name.CompareTo(y.Profile_Name));       ///Sort List Based On Season ProfileName
        //    }

        //}

        private bool IsSeasonProfilesOverlapping(List<SeasonProfile> SeasonProfiles)
        {
            SeasonProfile OverlappingSeasonProfile = null;
            return IsSeasonProfilesOverlapping(SeasonProfiles, out OverlappingSeasonProfile);
        }

        #endregion

        #region Encoder/Decoder

        public DLMS.Base_Class Encode_Season_Profile(GetSAPEntry CommObjectGetter)
        {
            try
            {
                DLMS.Class_20 ActivityCalendar = (DLMS.Class_20)CommObjectGetter.Invoke(Get_Index.Activity_Calendar);
                ActivityCalendar.EncodingAttribute = 7;               ///DayProfileListActive
                List<StSeasonProfile> seasonProfileTable = new List<StSeasonProfile>();
                foreach (var item in this.seasonProfile_Table)
                {
                    StSeasonProfile _Sp = new StSeasonProfile(item.Profile_Name, item.Start_Date, item.Week_Profile.Week_Profile_Name);
                    seasonProfileTable.Add(_Sp);
                }
                ActivityCalendar.SeasonProfilePassive = seasonProfileTable;
                return ActivityCalendar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Decode_Season_Profile(Base_Class arg, Param_WeeKProfile ParamWeekProfiles)
        {
            DLMS.Class_20 ActivityCalendar = (DLMS.Class_20)arg;
            ///Verify data Receiced/OBIS/ETC
            if (ActivityCalendar.GetAttributeDecodingResult(3) == DecodingResult.Ready)   ///Attribute Ready
            {
                List<StSeasonProfile> seasonProfileTable = ActivityCalendar.SeasonProfileActive;
                seasonProfile_Table.Clear();
                foreach (var item in seasonProfileTable)
                {
                    SeasonProfile _Sp = Create_Season_Profile(item.ProfileName);
                    _Sp.Start_Date = item.StartDate;
                    _Sp.Week_Profile = ParamWeekProfiles.Get_Week_Profile(item.WeekProfileName);
                }
            }
        }

        #endregion

        public void Param_Season_Profile_VerifyProfileAssigned(WeekProfile obj)
        {
            try
            {
                foreach (var item in seasonProfile_Table)
                {
                    if (item.Week_Profile.Equals(obj))   ///Check If Week Profile Assigne to a Season Profile
                        throw new Exception(String.Format("Week Profile {0} is assigned to Season Profile {1}", obj.ToString(), item.ToString()));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region IEnumerable<Season_Profile> Members

        public IEnumerator<SeasonProfile> GetEnumerator()
        {
            return seasonProfile_Table.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return seasonProfile_Table.GetEnumerator();
        }

        #endregion

        #region IEnumerable<SeasonProfile> Members

        IEnumerator<SeasonProfile> IEnumerable<SeasonProfile>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ISerializable Members

        protected Param_SeasonProfile(SerializationInfo info, StreamingContext context)
        {
            /////Getting SeasonProfileList Type SeasonProfile[]
            //SeasonProfile[] seasonProfileList =(SeasonProfile[])info.GetValue("SeasonProfileList", typeof(SeasonProfile[]));
            ////Object obj = info.GetValue("SeasonProfileList", typeof(SeasonProfile));
            //this.seasonProfile_Table = new List<SeasonProfile>(seasonProfileList);
            /////Attach SeasonStartDate_Changed event handler
            ///Getting SeasonProfiles Count <Type int>
            seasonProfile_Table = new List<SeasonProfile>();
            int profileCount = info.GetInt32("SeasonProfiles");
            for (int index = 0; index < profileCount; index++)
            {
                /////Adding SeasonProfile Type SeasonProfile
                String keyRet = String.Format("SeasonProfile_{0:X2}", index);
                SeasonProfile sp = (SeasonProfile)info.GetValue(keyRet, typeof(SeasonProfile));
                seasonProfile_Table.Add(sp);
            }

            foreach (var item in seasonProfile_Table)
            {
                if (item != null)
                    item.StartDate_Changed += new Action<SeasonProfile>(SeasonProfile_StartDate_Changed);
            }
            ///Sort Season Name
            //seasonProfile_Table.Sort((x, y) => x.Profile_Name.CompareTo(y.Profile_Name));
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            /////Adding SeasonProfileList Type SeasonProfile[]
            //info.AddValue("SeasonProfileList", this.seasonProfile_Table.ToArray());

            ///Adding SeasonProfiles Count <Type int>
            info.AddValue("SeasonProfiles", this.seasonProfile_Table.Count);
            for (int index = 0; index < seasonProfile_Table.Count; index++)
            {
                /////Adding SeasonProfile Type SeasonProfile
                String keyRet = String.Format("SeasonProfile_{0:X2}", index);
                info.AddValue(keyRet, seasonProfile_Table[index]);
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
                throw new Exception("Error occured while Clone Param_SeasonProfile", ex);
            }
        }
    }

    [Serializable]
    [XmlInclude(typeof(SeasonProfile))]
    [XmlInclude(typeof(WeekProfile))]
    [XmlInclude(typeof(StDateTime))]
    public class SeasonProfile : ISerializable, ICloneable
    {
        #region Data Members

        private int index;
        public static readonly byte MAX_Season_Profiles = 0x04;
        private String profile_Name;
        private StDateTime start_Date;
        private WeekProfile week_Profile;
        public event Action<SeasonProfile> StartDate_Changed = delegate { };

        #endregion

        #region Constructur

        public SeasonProfile(String profileName, StDateTime StartDateTime, WeekProfile wk_Profile)
        {
            if (!String.IsNullOrEmpty(profileName))
            {
                ///Enter Season_Profile Name 
                this.Profile_Name = profileName;
                this.Profile_Name_Str = profileName;
            }
            else
                throw new Exception("");
            Start_Date = StartDateTime;
            Week_Profile = wk_Profile;
        }

        public SeasonProfile(String profileName)
            : this(profileName, new StDateTime(), null)
        {

        }

        #endregion

        #region Season_Profile_Properties

        public string Profile_Name
        {
            get
            {
                return profile_Name;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    profile_Name = value;
                else
                    throw new Exception("Error setting Season Profile Name");
            }
        }
        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        public string Profile_Name_Str
        {
            get
            {
                try
                {
                    String SeasonName = "";
                    if (Convert.ToString("" + (char)1).
                            Equals(this.profile_Name, StringComparison.OrdinalIgnoreCase))
                    {
                        SeasonName = Param_SeasonProfile.SeasonProfileNamePre + 1;
                    }
                    else if (Convert.ToString("" + (char)2).
                            Equals(this.profile_Name, StringComparison.OrdinalIgnoreCase))
                    {
                        SeasonName = Param_SeasonProfile.SeasonProfileNamePre + 2;
                    }
                    else if (Convert.ToString("" + (char)3).
                        Equals(this.profile_Name, StringComparison.OrdinalIgnoreCase))
                    {
                        SeasonName = Param_SeasonProfile.SeasonProfileNamePre + 3;
                    }
                    else if (Convert.ToString("" + (char)4).
                        Equals(this.profile_Name, StringComparison.OrdinalIgnoreCase))
                    {
                        SeasonName = Param_SeasonProfile.SeasonProfileNamePre + 4;
                    }
                    else
                        SeasonName = profile_Name;
                    ///Console.Out.WriteLine(Char.GetNumericValue(Profile_Name[0]));
                    return SeasonName;
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
                    this.profile_Name = Convert.ToString("" + (char)1);
                }
                else if (Convert.ToString(Param_SeasonProfile.SeasonProfileNamePre + 2).
                    Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    this.profile_Name = Convert.ToString("" + (char)2);
                }
                else if (Convert.ToString(Param_SeasonProfile.SeasonProfileNamePre + 3).
                    Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    this.profile_Name = Convert.ToString("" + (char)3);
                }
                else if (Convert.ToString(Param_SeasonProfile.SeasonProfileNamePre + 4).
                    Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    this.profile_Name = Convert.ToString("" + (char)4);
                }
                else
                    this.profile_Name = value;
                ///Console.Out.WriteLine(Char.GetNumericValue(profile_Name[0]));
            }
        }

        public StDateTime Start_Date
        {
            get { return start_Date; }
            set
            {
                start_Date = value;
                StartDate_Changed.Invoke(this);

            }
        }

        public WeekProfile Week_Profile
        {
            get { return week_Profile; }
            set { week_Profile = value; }
        }

        public bool IsConsistent
        {
            get
            {
                if (!String.IsNullOrEmpty(Profile_Name) &&
                   Start_Date != null &&
                   Week_Profile != null && Week_Profile.IsConsistent)
                    return true;
                else
                    return false;
            }
        }

        #endregion

        public override string ToString()
        {

            //String SeasonName = "";
            //SeasonName = Profile_Name_Str;
            //return SeasonName;
            ///return this.index.ToString();

            //v10.0.21 by Azeem Inayat
            //string weekName = this.week_Profile.Profile_Name_Str.Replace("WeekProfile", "W.P");
            //string SeasonName = this.Profile_Name_Str.Replace("SeasonProfile", "S.P");
            
            //return
            //        this.index + "_[" +
            //        this.Start_Date.DayOfMonth.ToString("00") + "/" +
            //        this.Start_Date.Month.ToString("00") + "] [" +
            //        SeasonName + "] [" +
            //        weekName + "]";

            return this.index.ToString();
        }

        #region ISerializable Members

        protected SeasonProfile(SerializationInfo info, StreamingContext context)
        {
            ///Getting SeasonProfileName Type String
            this.profile_Name = info.GetString("SeasonProfileName");
            ///Getting SeasonStartDate Type DayProfile
            this.start_Date = (StDateTime)info.GetValue("SeasonStartDate", typeof(StDateTime));
            ///Adding WeekProfile Type WeekProfile
            this.Week_Profile = (WeekProfile)info.GetValue("WeekProfile", typeof(WeekProfile));

            this.Index = (int)info.GetValue("Index", typeof(int));
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ///Adding SeasonProfileName Type String
            info.AddValue("SeasonProfileName", this.Profile_Name);
            ///Adding SeasonStartDate Type DayProfile
            info.AddValue("SeasonStartDate", this.Start_Date);
            ///Adding WeekProfile Type WeekProfile
            info.AddValue("WeekProfile", this.Week_Profile);

            info.AddValue("Index", this.Index);
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
                throw new Exception("Error occurred while Clone SeasonProfile", ex);
            }
        }
    }

    //[Serializable]
    //[XmlInclude(typeof(Param_SeasonProfile))]
    //[XmlInclude(typeof(SeasonProfile))]
    //public class Param_SeasonProfile : IEnumerable<SeasonProfile>, ISerializable, ICloneable
    //{
    //    public List<SeasonProfile> seasonProfile_Table;
    //    public static readonly String SeasonProfileNamePre = "SeasonProfile_";

    //    public Param_SeasonProfile()  ///Constructor to initialize valid start date
    //    {
    //        ///Define Season Profile MAX
    //        seasonProfile_Table = new List<SeasonProfile>(SeasonProfile.MAX_Season_Profiles);
    //    }

    //    public bool IsConsistent
    //    {
    //        get
    //        {
    //            foreach (var item in seasonProfile_Table)
    //            {
    //                if (!item.IsConsistent)
    //                    return false;
    //            }
    //            if (seasonProfile_Table.Count > 0 &&
    //                !IsSeasonProfilesOverlapping(seasonProfile_Table))
    //                return true;
    //            else
    //                return false;
    //        }
    //    }

    //    public SeasonProfile Create_Season_Profile(string SeasonProfileName)
    //    {
    //        try
    //        {
    //            if (seasonProfile_Table.Count < SeasonProfile.MAX_Season_Profiles)
    //            {
    //                if (seasonProfile_Table.Exists((x) => x.Profile_Name_Str.Equals(SeasonProfileName)))
    //                    throw new Exception(String.Format("Season Profile {0},already defined", SeasonProfileName));
    //                SeasonProfile newSP = new SeasonProfile(SeasonProfileName);
    //                newSP.StartDate_Changed += new Action<SeasonProfile>(SeasonProfile_StartDate_Changed);
    //                seasonProfile_Table.Add(newSP);

    //                seasonProfile_Table.Sort((x, y) => x.Start_Date.CompareTo(y.Start_Date));
    //                return newSP;
    //            }
    //            else
    //                throw new Exception(String.Format("Unable to create Season Profile,maximum {0} profiles already created", SeasonProfile.MAX_Season_Profiles));
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    public SeasonProfile Create_Season_Profile()
    //    {
    //        try
    //        {
    //            int seasonId = seasonProfile_Table.Count + 1;
    //            return Create_Season_Profile("" + Convert.ToChar(seasonId));
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    public void Delete_Season_Profile(String SeasonProfileName)
    //    {
    //        try
    //        {
    //            SeasonProfile SPProfile = seasonProfile_Table.Find((x) => x.Profile_Name.Equals(SeasonProfileName));
    //            if (SPProfile != null)
    //                seasonProfile_Table.Remove(SPProfile);
    //            else
    //                throw new Exception(String.Format("{0} Season Profile never exists", SeasonProfileName));
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    public void Delete_Season_Profile()
    //    {
    //        try
    //        {
    //            SeasonProfile spE = Get_Last_Season_Profile();
    //            if (spE != null)
    //                Delete_Season_Profile(spE.Profile_Name);
    //            else
    //                throw new Exception("No Season Profiles exists");
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    public SeasonProfile Get_Season_Profile(String SeasonProfileName)
    //    {
    //        try
    //        {
    //            foreach (var item in seasonProfile_Table)
    //            {
    //                if (item.Profile_Name.Equals(SeasonProfileName))
    //                    return item;
    //            }
    //            return null;
    //        }
    //        catch (Exception ex)
    //        {
    //            return null;
    //        }
    //    }

    //    public SeasonProfile Get_Season_Profile(ushort SeasonProfileID)
    //    {
    //        return Get_Season_Profile("" + Convert.ToChar(SeasonProfileID));
    //    }

    //    public SeasonProfile Get_Last_Season_Profile()
    //    {
    //        try
    //        {
    //            if (seasonProfile_Table.Count > 0)
    //                return seasonProfile_Table[seasonProfile_Table.Count - 1];
    //            else
    //                return null;
    //        }
    //        catch (Exception ex)
    //        {
    //            return null;
    //        }
    //    }

    //    #region Season Profile Overlapping

    //    private void SeasonProfile_StartDate_Changed(SeasonProfile obj)
    //    {
    //        try
    //        {
    //            SeasonProfile OverlappingSeasonProfile = null;
    //            if (IsSeasonProfilesOverlapping(seasonProfile_Table, out OverlappingSeasonProfile))        ///Check TimeSlice OverLapping
    //            {
    //                if (OverlappingSeasonProfile == obj)
    //                    throw new Exception(String.Format("{0} 's Season Profile Overlapping", obj.ToString()));
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    public static bool IsSeasonProfilesOverlapping(List<SeasonProfile> SeasonProfiles, out SeasonProfile OverlappingSeasonProfile)
    //    {
    //        OverlappingSeasonProfile = null;
    //        try
    //        {
    //            //Sort SeasonProfile Based On Start Time Values //ahmed
    //            //SeasonProfiles.Sort((x, y) => (((x == null) ? -1 : (y == null) ? 1 : x.Start_Date.CompareTo(y.Start_Date))));
    //            OverlappingSeasonProfile = null;
    //            return false;
    //            for (int index = 0; index < SeasonProfiles.Count - 1; index++)
    //            {
    //                // If two timeSlices equals
    //                if (SeasonProfiles[index] == null ||
    //                    SeasonProfiles[index + 1] == null ||
    //                    SeasonProfiles[index].Start_Date.CompareTo(SeasonProfiles[index + 1].Start_Date) >= 0)
    //                {
    //                    OverlappingSeasonProfile = SeasonProfiles[index];
    //                    return true;
    //                }
    //                else
    //                {
    //                    ///(SeasonProfile_1 < SesasonProfile_2) ==>True 
    //                    //if (SeasonProfiles[index].Profile_Name.CompareTo(SeasonProfiles[index + 1].Profile_Name) == -1)
    //                    continue;
    //                    //else
    //                    //{
    //                    //    OverlappingSeasonProfile = SeasonProfiles[index];
    //                    //    return true;
    //                    //}
    //                }
    //            }
    //            OverlappingSeasonProfile = null;
    //            return false;
    //        }
    //        catch
    //        {
    //            OverlappingSeasonProfile = null;
    //            return false;
    //            ///throw ex;
    //        }
    //        finally
    //        {
    //            //SeasonProfiles.Sort((x, y) => x.Profile_Name.CompareTo(y.Profile_Name));
    //            //Sort List Based On Season ProfileName
    //        }
    //    }

    //    //private bool IsSeasonProfilesOverlapping(List<SeasonProfile> SeasonProfilesarg, out SeasonProfile OverlappingSeasonProfile)
    //    //{
    //    //    try
    //    //    {
    //    //        List<SeasonProfile> SeasonProfiles = new List<SeasonProfile>(SeasonProfilesarg);
    //    //        OverlappingSeasonProfile = null;
    //    //        return false;
    //    //        ///Sort SeasonProfile Based On Start Time Values //ahmed
    //    //        SeasonProfiles.Sort((x, y) => (((x == null) ? -1 : (y == null) ? 1 : x.Start_Date.CompareTo(y.Start_Date))));
    //    //        for (int index = 0; index < SeasonProfiles.Count - 1; index++)
    //    //        {

    //    //            // If two timeSlices equals
    //    //            if (SeasonProfiles[index].Start_Date.CompareTo(SeasonProfiles[index + 1].Start_Date) == 0 ||
    //    //                SeasonProfiles[index] == null ||
    //    //                SeasonProfiles[index + 1] == null)
    //    //            {
    //    //                OverlappingSeasonProfile = SeasonProfiles[index];
    //    //                return true;
    //    //            }
    //    //            else
    //    //            {
    //    //                ///(SeasonProfile_1 < SesasonProfile_2) ==>True 
    //    //                if (SeasonProfiles[index].Profile_Name.CompareTo(SeasonProfiles[index + 1].Profile_Name) == -1)
    //    //                    continue;
    //    //                else
    //    //                {
    //    //                    OverlappingSeasonProfile = SeasonProfiles[index];
    //    //                    return true;
    //    //                }
    //    //            }
    //    //        }
    //    //        OverlappingSeasonProfile = null;
    //    //        return false;
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        OverlappingSeasonProfile = null;
    //    //        return false;
    //    //        //    throw ex;
    //    //    }
    //    //    finally
    //    //    {
    //    //        //seasonProfile_Table.Sort((x, y) => x.Profile_Name.CompareTo(y.Profile_Name));       ///Sort List Based On Season ProfileName
    //    //    }

    //    //}

    //    private bool IsSeasonProfilesOverlapping(List<SeasonProfile> SeasonProfiles)
    //    {
    //        SeasonProfile OverlappingSeasonProfile = null;
    //        return IsSeasonProfilesOverlapping(SeasonProfiles, out OverlappingSeasonProfile);
    //    }

    //    #endregion

    //    #region Encoder/Decoder

    //    public DLMS.Base_Class Encode_Season_Profile(GetSAPEntry CommObjectGetter)
    //    {
    //        try
    //        {
    //            DLMS.Class_20 ActivityCalendar = (DLMS.Class_20)CommObjectGetter.Invoke(Get_Index.Activity_Calendar);
    //            ActivityCalendar.EncodingAttribute = 7;               ///DayProfileListActive
    //            List<StSeasonProfile> seasonProfileTable = new List<StSeasonProfile>();
    //            foreach (var item in this.seasonProfile_Table)
    //            {
    //                StSeasonProfile _Sp = new StSeasonProfile(item.Profile_Name, item.Start_Date, item.Week_Profile.Week_Profile_Name);
    //                seasonProfileTable.Add(_Sp);
    //            }
    //            ActivityCalendar.SeasonProfilePassive = seasonProfileTable;
    //            return ActivityCalendar;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    public void Decode_Season_Profile(Base_Class arg, Param_WeeKProfile ParamWeekProfiles)
    //    {
    //        DLMS.Class_20 ActivityCalendar = (DLMS.Class_20)arg;
    //        ///Verify data Receiced/OBIS/ETC
    //        if (ActivityCalendar.GetAttributeDecodingResult(3) == DecodingResult.Ready)   ///Attribute Ready
    //        {
    //            List<StSeasonProfile> seasonProfileTable = ActivityCalendar.SeasonProfileActive;
    //            seasonProfile_Table.Clear();
    //            foreach (var item in seasonProfileTable)
    //            {
    //                SeasonProfile _Sp = Create_Season_Profile(item.ProfileName);
    //                _Sp.Start_Date = item.StartDate;
    //                _Sp.Week_Profile = ParamWeekProfiles.Get_Week_Profile(item.WeekProfileName);
    //            }
    //        }
    //    }

    //    #endregion

    //    public void Param_Season_Profile_VerifyProfileAssigned(WeekProfile obj)
    //    {
    //        try
    //        {
    //            foreach (var item in seasonProfile_Table)
    //            {
    //                if (item.Week_Profile.Equals(obj))   ///Check If Week Profile Assigne to a Season Profile
    //                    throw new Exception(String.Format("Week Profile {0} is assigned to Season Profile {1}", obj.ToString(), item.ToString()));
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    #region IEnumerable<Season_Profile> Members

    //    public IEnumerator<SeasonProfile> GetEnumerator()
    //    {
    //        return seasonProfile_Table.GetEnumerator();
    //    }

    //    #endregion

    //    #region IEnumerable Members

    //    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    //    {
    //        return seasonProfile_Table.GetEnumerator();
    //    }

    //    #endregion

    //    #region IEnumerable<SeasonProfile> Members

    //    IEnumerator<SeasonProfile> IEnumerable<SeasonProfile>.GetEnumerator()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    #endregion

    //    #region ISerializable Members

    //    protected Param_SeasonProfile(SerializationInfo info, StreamingContext context)
    //    {
    //        /////Getting SeasonProfileList Type SeasonProfile[]
    //        //SeasonProfile[] seasonProfileList =(SeasonProfile[])info.GetValue("SeasonProfileList", typeof(SeasonProfile[]));
    //        ////Object obj = info.GetValue("SeasonProfileList", typeof(SeasonProfile));
    //        //this.seasonProfile_Table = new List<SeasonProfile>(seasonProfileList);
    //        /////Attach SeasonStartDate_Changed event handler
    //        ///Getting SeasonProfiles Count <Type int>
    //        seasonProfile_Table = new List<SeasonProfile>();
    //        int profileCount = info.GetInt32("SeasonProfiles");
    //        for (int index = 0; index < profileCount; index++)
    //        {
    //            /////Adding SeasonProfile Type SeasonProfile
    //            String keyRet = String.Format("SeasonProfile_{0:X2}", index);
    //            SeasonProfile sp = (SeasonProfile)info.GetValue(keyRet, typeof(SeasonProfile));
    //            seasonProfile_Table.Add(sp);
    //        }

    //        foreach (var item in seasonProfile_Table)
    //        {
    //            if (item != null)
    //                item.StartDate_Changed += new Action<SeasonProfile>(SeasonProfile_StartDate_Changed);
    //        }
    //        ///Sort Season Name
    //        //seasonProfile_Table.Sort((x, y) => x.Profile_Name.CompareTo(y.Profile_Name));
    //    }

    //    [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
    //    public void GetObjectData(SerializationInfo info, StreamingContext context)
    //    {
    //        /////Adding SeasonProfileList Type SeasonProfile[]
    //        //info.AddValue("SeasonProfileList", this.seasonProfile_Table.ToArray());

    //        ///Adding SeasonProfiles Count <Type int>
    //        info.AddValue("SeasonProfiles", this.seasonProfile_Table.Count);
    //        for (int index = 0; index < seasonProfile_Table.Count; index++)
    //        {
    //            /////Adding SeasonProfile Type SeasonProfile
    //            String keyRet = String.Format("SeasonProfile_{0:X2}", index);
    //            info.AddValue(keyRet, seasonProfile_Table[index]);
    //        }
    //    }
    //    #endregion

    //    public object Clone()
    //    {
    //        MemoryStream memStream = null;
    //        Object dp = null;
    //        try
    //        {
    //            BinaryFormatter formatter = new BinaryFormatter();
    //            using (memStream = new MemoryStream(1024))
    //            {
    //                formatter.Serialize(memStream, this);
    //                memStream.Seek(0, SeekOrigin.Begin);
    //                dp = formatter.Deserialize(memStream);
    //            }
    //            return dp;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception("Error occured while Clone Param_SeasonProfile", ex);
    //        }
    //    }
    //}

    //[Serializable]
    //[XmlInclude(typeof(SeasonProfile))]
    //[XmlInclude(typeof(WeekProfile))]
    //[XmlInclude(typeof(StDateTime))]
    //public class SeasonProfile : ISerializable, ICloneable
    //{
    //    #region Data Members

    //    private int index;
    //    public static readonly byte MAX_Season_Profiles = 0x04;
    //    private String profile_Name;
    //    private StDateTime start_Date;
    //    private WeekProfile week_Profile;
    //    public event Action<SeasonProfile> StartDate_Changed = delegate { };

    //    #endregion

    //    #region Constructur

    //    public SeasonProfile(String profileName, StDateTime StartDateTime, WeekProfile wk_Profile)
    //    {
    //        if (!String.IsNullOrEmpty(profileName))
    //        {
    //            ///Enter Season_Profile Name 
    //            this.Profile_Name = profileName;
    //            this.Profile_Name_Str = profileName;
    //        }
    //        else
    //            throw new Exception("");
    //        Start_Date = StartDateTime;
    //        Week_Profile = wk_Profile;
    //    }

    //    public SeasonProfile(String profileName)
    //        : this(profileName, new StDateTime(), null)
    //    {

    //    }

    //    #endregion

    //    #region Season_Profile_Properties

    //    public string Profile_Name
    //    {
    //        get
    //        {
    //            return profile_Name;
    //        }
    //        set
    //        {
    //            if (!String.IsNullOrEmpty(value))
    //                profile_Name = value;
    //            else
    //                throw new Exception("Error setting Season Profile Name");
    //        }
    //    }
    //    public int Index
    //    {
    //        get { return index; }
    //        set { index = value; }
    //    }

    //    public string Profile_Name_Str
    //    {
    //        get
    //        {
    //            try
    //            {
    //                String SeasonName = "";
    //                if (Convert.ToString("" + (char)1).
    //                        Equals(this.profile_Name, StringComparison.OrdinalIgnoreCase))
    //                {
    //                    SeasonName = Param_SeasonProfile.SeasonProfileNamePre + 1;
    //                }
    //                else if (Convert.ToString("" + (char)2).
    //                        Equals(this.profile_Name, StringComparison.OrdinalIgnoreCase))
    //                {
    //                    SeasonName = Param_SeasonProfile.SeasonProfileNamePre + 2;
    //                }
    //                else if (Convert.ToString("" + (char)3).
    //                    Equals(this.profile_Name, StringComparison.OrdinalIgnoreCase))
    //                {
    //                    SeasonName = Param_SeasonProfile.SeasonProfileNamePre + 3;
    //                }
    //                else if (Convert.ToString("" + (char)4).
    //                    Equals(this.profile_Name, StringComparison.OrdinalIgnoreCase))
    //                {
    //                    SeasonName = Param_SeasonProfile.SeasonProfileNamePre + 4;
    //                }
    //                else
    //                    SeasonName = profile_Name;
    //                ///Console.Out.WriteLine(Char.GetNumericValue(Profile_Name[0]));
    //                return SeasonName;
    //            }
    //            catch (Exception ex)
    //            {
    //                return null;
    //            }
    //        }
    //        set
    //        {
    //            if (Convert.ToString(Param_SeasonProfile.SeasonProfileNamePre + 1).
    //                Equals(value, StringComparison.OrdinalIgnoreCase))
    //            {
    //                this.profile_Name = Convert.ToString("" + (char)1);
    //            }
    //            else if (Convert.ToString(Param_SeasonProfile.SeasonProfileNamePre + 2).
    //                Equals(value, StringComparison.OrdinalIgnoreCase))
    //            {
    //                this.profile_Name = Convert.ToString("" + (char)2);
    //            }
    //            else if (Convert.ToString(Param_SeasonProfile.SeasonProfileNamePre + 3).
    //                Equals(value, StringComparison.OrdinalIgnoreCase))
    //            {
    //                this.profile_Name = Convert.ToString("" + (char)3);
    //            }
    //            else if (Convert.ToString(Param_SeasonProfile.SeasonProfileNamePre + 4).
    //                Equals(value, StringComparison.OrdinalIgnoreCase))
    //            {
    //                this.profile_Name = Convert.ToString("" + (char)4);
    //            }
    //            else
    //                this.profile_Name = value;
    //            ///Console.Out.WriteLine(Char.GetNumericValue(profile_Name[0]));
    //        }
    //    }

    //    public StDateTime Start_Date
    //    {
    //        get { return start_Date; }
    //        set
    //        {
    //            start_Date = value;
    //            StartDate_Changed.Invoke(this);

    //        }
    //    }

    //    public WeekProfile Week_Profile
    //    {
    //        get { return week_Profile; }
    //        set { week_Profile = value; }
    //    }

    //    public bool IsConsistent
    //    {
    //        get
    //        {
    //            if (!String.IsNullOrEmpty(Profile_Name) &&
    //               Start_Date != null &&
    //               Week_Profile != null && Week_Profile.IsConsistent)
    //                return true;
    //            else
    //                return false;
    //        }
    //    }

    //    #endregion

    //    public override string ToString()
    //    {
    //        String SeasonName = "";
    //        SeasonName = Profile_Name_Str;
    //        return SeasonName;
    //        //return this.Start_Date.DayOfMonth.ToString()+"/"+this.Start_Date.Month.ToString();
    //        ///return this.index.ToString();
    //    }

    //    #region ISerializable Members

    //    protected SeasonProfile(SerializationInfo info, StreamingContext context)
    //    {
    //        ///Getting SeasonProfileName Type String
    //        this.profile_Name = info.GetString("SeasonProfileName");
    //        ///Getting SeasonStartDate Type DayProfile
    //        this.start_Date = (StDateTime)info.GetValue("SeasonStartDate", typeof(StDateTime));
    //        ///Adding WeekProfile Type WeekProfile
    //        this.Week_Profile = (WeekProfile)info.GetValue("WeekProfile", typeof(WeekProfile));

    //        this.Index = (int)info.GetValue("Index", typeof(int));
    //    }

    //    [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
    //    public void GetObjectData(SerializationInfo info, StreamingContext context)
    //    {
    //        ///Adding SeasonProfileName Type String
    //        info.AddValue("SeasonProfileName", this.Profile_Name);
    //        ///Adding SeasonStartDate Type DayProfile
    //        info.AddValue("SeasonStartDate", this.Start_Date);
    //        ///Adding WeekProfile Type WeekProfile
    //        info.AddValue("WeekProfile", this.Week_Profile);

    //        info.AddValue("Index", this.Index);
    //    }

    //    #endregion

    //    public object Clone()
    //    {
    //        MemoryStream memStream = null;
    //        Object dp = null;
    //        try
    //        {
    //            BinaryFormatter formatter = new BinaryFormatter();
    //            using (memStream = new MemoryStream(1024))
    //            {
    //                formatter.Serialize(memStream, this);
    //                memStream.Seek(0, SeekOrigin.Begin);
    //                dp = formatter.Deserialize(memStream);
    //            }
    //            return dp;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception("Error occurred while Clone SeasonProfile", ex);
    //        }
    //    }
    //}
}
