using System;
using System.Collections.Generic;
using DLMS;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using DLMS.Comm;

namespace SharedCode.Comm.Param
{
    [Serializable]
    [XmlInclude(typeof(DayProfile))]
    public class Param_SpecialDay : IEnumerable<SpecialDay>, ISerializable,ICloneable
    {
        public List<SpecialDay> specialDay_Table;

        public Param_SpecialDay()
        {
            ///Max Special Day
            specialDay_Table = new List<SpecialDay>((int)SpecialDay.MAX_Special_Days);
        }

        public bool IsConsistent
        {
            get
            {
                foreach (var item in specialDay_Table)
                {
                    if (!item.IsConsistent)
                        return false;
                }
                return true;
                ///return !IsSpecialDayProfileOverlapping(specialDay_Table);
            }
        }

        private SpecialDay CreateSpecialDay(uint SID, StDateTime startDate, DayProfile DayProfile)
        {
            try
            {
                if (specialDay_Table.Exists((x) => x.SpecialDayID == SID) ||
                    specialDay_Table.Exists((x) => x.StartDate.CompareTo(startDate) == 0)) ///DayProfile Already Exists With Same ID or Date
                    throw new Exception(String.Format("Special day profile already defined/duplicate {0}", SID));
                SpecialDay spDay = new SpecialDay(SID, startDate, DayProfile);
                spDay.StartDate_Changed += new Action<SpecialDay>(SpecialDayProfile_StartDate_Changed);
                specialDay_Table.Add(spDay);
                specialDay_Table.Sort((x, y) => x.SpecialDayID.CompareTo(y.SpecialDayID)); ///Sort Special Days List
                return spDay;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Create Special Day Profile
        /// </summary>
        /// <param quantity_name="StartDateTime"></param>
        /// <param quantity_name="DP"></param>
        public SpecialDay CreateSpecialDay(StDateTime StartDateTime, DayProfile DP)
        {
            try
            {
                //specialDay_Table.Sort((x, y) => x.SpecialDayID.CompareTo(y.SpecialDayID));  ///Sort Special Days List based On Sp ID's
                SpecialDay lastSPDay = GetLastSpecialDay();
                uint lastSpecialDayId = 0;
                if (lastSPDay != null)
                    lastSpecialDayId = lastSPDay.SpecialDayID;
                if (lastSpecialDayId >= SpecialDay.MAX_Special_Days)
                    throw new Exception("Max Special Day Profiles defined,");
                return CreateSpecialDay(++lastSpecialDayId, StartDateTime, DP);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SpecialDay CreateSpecialDay()
        {
            return CreateSpecialDay(null, null);
        }

        //private void DeleteSpecialDay(uint SID)
        //{
        //    try
        //    {
        //        specialDay_Table.Sort((x, y) => x.SpecialDayID.CompareTo(y.SpecialDayID));  ///Sort Special Days List based On Sp ID's
        //        SpecialDay spToDel = specialDay_Table.Find((x) => x.SpecialDayID == SID);
        //        if (!specialDay_Table.Remove(spToDel))
        //            throw new Exception(String.Format("Unable to delete special day profile,ID {0}", SID));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void DeleteSpecialDay(uint SID)
        {
            try
            {
                specialDay_Table.Sort((x, y) => x.SpecialDayID.CompareTo(y.SpecialDayID));  ///Sort Special Days List based On Sp ID's
                SpecialDay spToDel = specialDay_Table.Find((x) => x.SpecialDayID == SID);
                if (!specialDay_Table.Remove(spToDel))
                    throw new Exception(String.Format("Unable to delete special day profile,ID {0}", SID));
                for (int i = (int)(SID - 1); i <= specialDay_Table.Count - 1; i++)
                {
                    specialDay_Table[i].SpecialDayID = (ushort)(specialDay_Table[i].SpecialDayID - 1);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteSpecialDay()
        {
            if (specialDay_Table.Count > 0)
            {
                specialDay_Table.Sort((x, y) => x.SpecialDayID.CompareTo(y.SpecialDayID));  ///Sort Special Days List based On Sp ID's
                SpecialDay spLast = specialDay_Table[specialDay_Table.Count - 1];
                DeleteSpecialDay(spLast.SpecialDayID);
            }
            else
                throw new Exception("No Special Days exists in Special Day Profile Table");
        }

        /// <summary>
        /// Function To Check Either DayProfile already Assigned
        /// </summary>
        /// <param quantity_name="obj"></param>
        public void Param_Special_Day_VerifyProfileAssigned(DayProfile objDP)
        {
            try
            {
                foreach (var item in specialDay_Table)
                {
                    if (item.DayProfile != null && item.DayProfile.Equals(objDP))
                        throw new Exception(String.Format("Day Profile {0} is assigned in Special Day ID {1}",
                            objDP.Day_Profile_ID, item.SpecialDayID));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SpecialDay GetSpecialDay(uint SpecialDayId)
        {
            try
            {
                SpecialDay SP = specialDay_Table.Find((x) => x.SpecialDayID == SpecialDayId);
                return SP;
            }
            catch (Exception ex)
            {
                ///
                return null;
            }
        }

        public SpecialDay GetLastSpecialDay()
        {
            if (specialDay_Table.Count > 0)
                return specialDay_Table[specialDay_Table.Count - 1];
            else
                return null;
        }

        #region Encoders/Decoders

        public Base_Class Encode_SpecialDay_Profile(GetSAPEntry CommObjectGetter)
        {
            try
            {
                DLMS.Class_11 SpecialDaysTable = (DLMS.Class_11)CommObjectGetter.Invoke(Get_Index.Special_Days_Table);
                SpecialDaysTable.EncodingAttribute = 2;               ///DayProfileListActive
                List<StSpecialDayProfile> SpecialDayProfiles = new List<StSpecialDayProfile>();
                foreach (var item in this.specialDay_Table)
                {
                    StSpecialDayProfile _SDP = new StSpecialDayProfile((ushort)item.SpecialDayID, item.StartDate, item.DayProfile.Day_Profile_ID);
                    SpecialDayProfiles.Add(_SDP);
                }
                SpecialDaysTable.SpecialDayProfiles = SpecialDayProfiles;
                return SpecialDaysTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Decode_SpecialDay_Profile(Base_Class arg, Param_DayProfile Param_DayProfile)
        {
            try
            {
                DLMS.Class_11 SpecialDayTable = (DLMS.Class_11)arg;
                ///Verify data Receiced/OBIS/ETC
                if (SpecialDayTable.GetAttributeDecodingResult(2) == DecodingResult.Ready)   ///Attribute Ready
                {
                    List<StSpecialDayProfile> specialDayProfiles = SpecialDayTable.SpecialDayProfiles;
                    specialDay_Table.Clear();
                    foreach (var item in specialDayProfiles)
                    {
                        SpecialDay _SDP = CreateSpecialDay(item.Index, item.Date, Param_DayProfile.GetDayProfile(item.DayProfileId));
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region SpecialDay Profile Overlapping Start Date

        private void SpecialDayProfile_StartDate_Changed(SpecialDay obj)
        {
            try
            {
                SpecialDay OverlappingSpecialDayProfile = null;
                if (IsSpecialDayProfileOverlapping(specialDay_Table, out OverlappingSpecialDayProfile))        ///Check SpecialDayProfile StartDate OverLapping
                {
                    if (OverlappingSpecialDayProfile == obj)
                        throw new Exception(String.Format("{0} 's Special Day Profile Overlapping", obj.SpecialDayID));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsSpecialDayProfileOverlapping(List<SpecialDay> SpecialDayProfiles, out SpecialDay OverlappingSpecialDayProfile)
        {
            try
            {
                ///Sort SeasonProfile Based On Start Time Values
                specialDay_Table.Sort((x, y) => ((x == null) ? -1 : x.StartDate.CompareTo(y.StartDate)));
                for (int index = 0; index < specialDay_Table.Count - 1; index++)
                {
                    ///If two timeSlices equals
                    if (specialDay_Table[index].StartDate.CompareTo(specialDay_Table[index + 1].StartDate) == 0 ||
                        specialDay_Table[index] == null ||
                        specialDay_Table[index + 1] == null)
                    {
                        OverlappingSpecialDayProfile = specialDay_Table[index];
                        return true;
                    }

                }
                OverlappingSpecialDayProfile = null;
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                specialDay_Table.Sort((x, y) => (x.SpecialDayID.CompareTo(y.SpecialDayID)));       ///Sort List Based On Special Day Id
            }

        }

        private bool IsSpecialDayProfileOverlapping(List<SpecialDay> SpecialDayProfiles)
        {
            SpecialDay OverlappingSpecialDayProfile = null;
            return IsSpecialDayProfileOverlapping(specialDay_Table, out OverlappingSpecialDayProfile);
        }

        #endregion

        #region IEnumerable<SpecialDay> Members

        public IEnumerator<SpecialDay> GetEnumerator()
        {
            return specialDay_Table.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return specialDay_Table.GetEnumerator();
        }

        #endregion

        #region ISerializable Members

        protected Param_SpecialDay(SerializationInfo info, StreamingContext context)
        {
            /////Getting SpecialDayList Type SpecialDay[]
            //SpecialDay [] SpecialDayList = (SpecialDay[]) info.GetValue("SpecialDayList", typeof(SpecialDay[]));
            this.specialDay_Table = new List<SpecialDay>();

            /////Getting SpecialDayProfiles Count Type Int
            int SPDayCount = info.GetInt32("SpecialDayProfiles");
            for (int index = 0; index < SPDayCount; index++)
            {
                /////Getting SpecialDayProfile Type SpecialDayProfile
                String keyRet = String.Format("SpecialDayProfile_{0:X2}", index);
                SpecialDay SP = (SpecialDay)info.GetValue(keyRet, typeof(SpecialDay));
                specialDay_Table.Add(SP);
            }
            foreach (var item in specialDay_Table)
            {
                if (item != null)
                    item.StartDate_Changed += new Action<SpecialDay>(SpecialDayProfile_StartDate_Changed);
            }
            specialDay_Table.Sort((x, y) => x.SpecialDayID.CompareTo(y.SpecialDayID));
        }


        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            /////Adding SpecialDayList Type SpecialDay[]
            //info.AddValue("SpecialDayList", this.specialDay_Table.ToArray());

            /////Adding SpecialDayProfiles Count Type Int
            info.AddValue("SpecialDayProfiles", this.specialDay_Table.Count);
            for (int index = 0; index < specialDay_Table.Count; index++)
            {
                /////Adding DayProfile Type DayProfile
                String keyRet = String.Format("SpecialDayProfile_{0:X2}", index);
                info.AddValue(keyRet, specialDay_Table[index]);
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
                throw new Exception("Error occured while Clone Param_SpecialDayProfile", ex);
            }
        }
    }

    [Serializable]
    [XmlInclude(typeof(DayProfile))]
    [XmlInclude(typeof(StDateTime))]
    public class SpecialDay : ISerializable,ICloneable
    {
        public const uint MAX_Special_Days = 100;
        private uint sdayID;
        private StDateTime startDate;
        private DayProfile dayProfile;
        public event Action<SpecialDay> StartDate_Changed = delegate { };

        public SpecialDay(uint SID, StDateTime startDateTime, DayProfile dayProfile)
        {
            if (SID > 0 && SID <= MAX_Special_Days)
            {
                sdayID = SID;
            }
            else
                throw new Exception(String.Format("Invalid Specical Day ID,{0}", SID));
            StartDate = startDateTime;
            DayProfile = dayProfile;
        }

        public SpecialDay(uint SID)
            : this(SID, new StDateTime(), null)
        { }

        #region Property_SpecialDay

        public uint SpecialDayID
        {
            get { return sdayID; }
            set { sdayID = value; }
        }

        public StDateTime StartDate
        {
            get { return startDate; }
            set
            {
                /// Invoke StartDate Change
                startDate = value;
                StartDate_Changed.Invoke(this);
            }
        }

        public DayProfile DayProfile
        {
            get { return dayProfile; }
            set { dayProfile = value; }
        }

        public bool IsConsistent
        {
            get
            {
                if (SpecialDayID > 0 && SpecialDayID <= MAX_Special_Days &&
                   StartDate != null && DayProfile != null && DayProfile.IsConsistent)
                    return true;
                else
                    return false;
            }
        }

        public override string ToString()
        {
            return String.Format("SP_{0:D3}", SpecialDayID);
        }

        #endregion

        #region ISerializable Members

        protected SpecialDay(SerializationInfo info, StreamingContext context)
        {
            ///Getting SpecialDayId Type uint
            this.sdayID = info.GetUInt32("SpecialDayId");
            ///Getting StartDate Type DLMS.StDateTime
            this.StartDate = (StDateTime)info.GetValue("StartDate", typeof(StDateTime));
            ///Getting DayProfile Type DayProfile
            this.DayProfile = (DayProfile)info.GetValue("DayProfile", typeof(DayProfile));
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ///Adding SpecialDayId Type uint
            info.AddValue("SpecialDayId", this.SpecialDayID);
            ///Adding StartDate Type DLMS.StDateTime
            info.AddValue("StartDate", this.StartDate);
            ///Adding DayProfile Type DayProfile
            info.AddValue("DayProfile", this.DayProfile);
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
                throw new Exception("Error occured while Clone SpecialDayProfile", ex);
            }
        }
    }
}
