using System;
using System.Collections.Generic;
using SmartEyeControl_7.Controllers;
using DLMS;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using AccurateOptocomSoftware.Common;
using DLMS.Comm;

namespace comm
{
    [Serializable]
    [XmlInclude(typeof(Param_DayProfile))]
    public class Param_DayProfile : IEnumerable<DayProfile>,ICloneable ,ISerializable
    {
        public List<DayProfile> dayProfile_Table;
        public event Action<DayProfile> VerifyProfileAssigned = delegate { };

        public int DayProfileCount
        {
            get
            {
                if (dayProfile_Table != null && dayProfile_Table.Count >= 0)
                    return dayProfile_Table.Count;
                else
                    return -1;
            }
        }

        /// <summary>
        /// Constructors
        /// </summary>
        /// <param quantity_name="ID"></param>
        public Param_DayProfile()
        {
            dayProfile_Table = new List<DayProfile>(DayProfile.MAX_Day_Profile);    ///Create Max Day Profiles
        }

        public DayProfile GetDayProfile(byte DayProfileID)
        {
            try
            {
                DayProfile dp = dayProfile_Table.Find((x) => x.Day_Profile_ID == DayProfileID);
                return dp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DayProfile GetLastDayProfile()
        {
            try
            {
                ///Sort Day Table With DayProfile ID's
                //dayProfile_Table.Sort((x, y) => x.Day_Profile_ID.CompareTo(y.Day_Profile_ID));
                DayProfile lastDayProfile = null;
                if (dayProfile_Table.Count > 0)
                {
                    lastDayProfile = dayProfile_Table[dayProfile_Table.Count - 1];
                }
                return lastDayProfile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DayProfile CreateDayProfile(byte dayProfileID, ushort dayProfile_ScheduleCount)
        {
            try
            {
                if (dayProfile_Table.Count < DayProfile.MAX_Day_Profile)
                {
                    if (dayProfile_Table.Exists((x) => x.Day_Profile_ID == dayProfileID))
                        throw new Exception(String.Format("Day Profile with day Id {0},already defined", dayProfileID));
                    DayProfile newDP = new DayProfile(dayProfileID, dayProfile_ScheduleCount);
                    dayProfile_Table.Add(newDP);
                    ///Sort Day Table With DayProfile ID's
                    dayProfile_Table.Sort((x, y) => x.Day_Profile_ID.CompareTo(y.Day_Profile_ID));
                    return newDP;
                }
                else
                    throw new Exception(String.Format("Unable to create day profile,maximum {0} profiles already created", DayProfile.MAX_Day_Profile));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DayProfile CreateDayProfile(ushort dayProfile_ScheduleCount)
        {
            try
            {
                DayProfile lastDayProfile = GetLastDayProfile();
                byte lastDayProfileID = 0;
                if (lastDayProfile != null)
                    lastDayProfileID = lastDayProfile.Day_Profile_ID;
                return CreateDayProfile(++lastDayProfileID, dayProfile_ScheduleCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DayProfile CreateDayProfile()
        {
            return CreateDayProfile(1);
        }

        public bool IsConsistent
        {
            get
            {
                try
                {
                    foreach (var item in dayProfile_Table)
                    {
                        if (!item.IsConsistent)
                            return false;
                    }
                    if (dayProfile_Table.Count > 0)  ///There should be atleast One DayProfile In Table
                        return true;
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        private void DeleteDayProfile(byte dayProfileID)
        {
            try
            {
                if (dayProfile_Table.Count > 0)
                {
                    DayProfile dayProfile = dayProfile_Table.Find((x) => x.Day_Profile_ID == dayProfileID);
                    if (dayProfile == null)
                        throw new Exception(String.Format("Day Profile {0} never exists", dayProfileID));
                    VerifyProfileAssigned.Invoke(dayProfile);
                    dayProfile_Table.Remove(dayProfile);
                    dayProfile_Table.Sort((x, y) => x.Day_Profile_ID.CompareTo(y.Day_Profile_ID));
                }
                else
                    throw new Exception("No more day profile exist in DayProfile Table");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteDayProfile()
        {
            try
            {
                DayProfile lastDp = GetLastDayProfile();
                if (lastDp == null)
                    throw new Exception("No more day profile defined in DayProfile Table");
                DeleteDayProfile(lastDp.Day_Profile_ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #region Encode/Decode Param DayProfileTable

        public DLMS.Base_Class Encode_Param_DayProfile(GetSAPEntry CommObjectGetter)
        {
            try
            {
                DLMS.Class_20 ActivityCalendar = (DLMS.Class_20)CommObjectGetter.Invoke(Get_Index.Activity_Calendar);
                ActivityCalendar.EncodingAttribute = 9;               ///DayProfileListActive
                byte[] ScriptOBIS_Code = new byte[] { 0, 0, 10, 00, 100, 255 };
                List<StDayProfile> DayProfileTable = new List<StDayProfile>();
                foreach (var item in this.dayProfile_Table)
                {
                    List<StDayProfileAction> timeSlotList = new List<StDayProfileAction>();
                    foreach (var timeSlot in item)
                    {
                        StDateTime strTime = new StDateTime();
                        strTime.SetTime(timeSlot.StartTime);
                        StDayProfileAction dpAction = new StDayProfileAction(strTime, ScriptOBIS_Code, (ushort)timeSlot.ScriptSelector);
                        timeSlotList.Add(dpAction);
                    }
                    StDayProfile dp = new StDayProfile(item.Day_Profile_ID, timeSlotList);
                    DayProfileTable.Add(dp);
                }
                ActivityCalendar.DayProfilePassive = DayProfileTable;
                return ActivityCalendar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Decode_Param_DayProfile(DLMS.Base_Class arg)
        {
            try
            {
                DLMS.Class_20 ActivityCalendar = (DLMS.Class_20)arg;
                ///Verify data Receiced/OBIS/ETC
                if (ActivityCalendar.GetAttributeDecodingResult(5) == DecodingResult.Ready)   ///Attribute Ready
                {
                    List<StDayProfile> dayProfileTable = ActivityCalendar.DayProfileActive;
                    dayProfile_Table.Clear();
                    foreach (var item in dayProfileTable)
                    {
                        DayProfile dp = CreateDayProfile((byte)item.DayId, (ushort)item.DaySchedule.Count);
                        ushort timeScheduleId = 1;
                        foreach (var dayProfileTimeSlot in item.DaySchedule)
                        {
                            TimeSlot slt = dp.GetDaySchedule(timeScheduleId++);
                            slt.ScriptSelector = (Tarrif_ScriptSelector)dayProfileTimeSlot.ScriptSelector;
                            if (!dayProfileTimeSlot.StartTime.IsTimeConvertible)
                            {
                                dayProfileTimeSlot.StartTime.Hundred = 0;    ///Ignore HSEC & SEC
                                dayProfileTimeSlot.StartTime.Second = 0;
                            }
                            slt.StartTime = dayProfileTimeSlot.StartTime.GetTime();
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region IEnumerable<DayProfile> Members

        public IEnumerator<DayProfile> GetEnumerator()
        {
            return dayProfile_Table.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return dayProfile_Table.GetEnumerator();
        }

        #endregion

        #region ISerializable Members

        protected Param_DayProfile(SerializationInfo info, StreamingContext context)
        {
            //Getting DayProfileList <Type List<DayProfile> >
            //DayProfile[] _dayProfile_Table = (DayProfile[])info.GetValue("DayProfileList", typeof(DayProfile[]));
            this.dayProfile_Table = new List<DayProfile>();
            //Getting DayProfiles Count Type Int
            int dayProfileCount = info.GetInt32("DayProfiles");
            for (int index = 0; index < dayProfileCount; index++)
            {
                //Adding DayProfile Type DayProfile
                String keyRet = String.Format("DayProfile_{0:X2}", index);
                DayProfile dp = (DayProfile)info.GetValue(keyRet, typeof(DayProfile));
                dayProfile_Table.Add(dp);
            }
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            //Adding DayProfileList <Type List<DayProfile> >
            //info.AddValue("DayProfileList", this.dayProfile_Table.ToArray()); 
            //Adding DayProfiles Count Type Int
            info.AddValue("DayProfiles", this.dayProfile_Table.Count);
            for (int index = 0; index < dayProfile_Table.Count; index++)
            {
                //Adding DayProfile Type DayProfile
                String keyRet = String.Format("DayProfile_{0:X2}", index);
                info.AddValue(keyRet, dayProfile_Table[index]);
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
                throw new Exception("Error occured while Clone Param_DayProfile", ex);
            }
        }
    }

    [Serializable]
    [XmlInclude(typeof(DayProfile))]
    public class DayProfile : IEnumerable<TimeSlot>, IComparable<DayProfile>, ICloneable, ISerializable
    {
        #region DataMembers

        public static readonly byte MAX_Day_Profile = 16;
        public static readonly String DayProfileIdStr = "DayProfileId";
        public static readonly String DayProfileTimeSlots = "DayProfileTimeSlots";

        private readonly byte day_Profile_ID;
        public List<TimeSlot> dayProfile_Schedule;

        #endregion

        #region Properties

        public byte Day_Profile_ID
        {
            get { return day_Profile_ID; }
        }

        /// <summary>
        /// Verify that Eithter the Day Profile Data is in Consisten
        /// </summary>
        public bool IsConsistent
        {
            get         ///Check Either Day_Profile Verify Business Domain Rules
            {
                if (IsDayIdCorrect(Day_Profile_ID) &&
                    dayProfile_Schedule.Count > 0 &&
                    dayProfile_Schedule.Count <= TimeSlot.MAX_TimeSlot &&
                    !IsTimeSlicesOverlapping(dayProfile_Schedule) &&
                    dayProfile_Schedule[0].StartTime.Equals(new TimeSpan(0))   ///First Slot Should be Zero Hour
                    )
                    return true;
                else
                    return false;
            }
        }

        #endregion

        #region Constructur

        public DayProfile()
            : this(1, 1)
        { }

        public DayProfile(byte day_Profile_Id) : this(day_Profile_Id, 1) { }

        public DayProfile(byte day_Profile_Id, ushort dayProfileSchduleCount)
        {
            if (IsDayIdCorrect(day_Profile_Id))
                this.day_Profile_ID = day_Profile_Id;
            else
                throw new Exception("Invalid day profile Id");
            dayProfile_Schedule = new List<TimeSlot>(TimeSlot.MAX_TimeSlot);
            CreateDaySchedule(dayProfileSchduleCount);
        }

        #endregion

        #region Member Methods

        public TimeSlot CreateDayProfileSchedule()
        {
            try
            {
                CreateDaySchedule(1);
                return dayProfile_Schedule[dayProfile_Schedule.Count - 1];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void DeleteDayProfileSchedule()
        {
            try
            {
                if (dayProfile_Schedule.Count > 0)
                    dayProfile_Schedule.RemoveAt(dayProfile_Schedule.Count - 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region TimeSlices Overlapping

        private void DayProfile_StartTime_Changed(TimeSlot obj)
        {
            try
            {
                TimeSlot OverlappingSlot = null;
                bool newAdd = false;
                if (!dayProfile_Schedule.Exists((x) => x.Equals(obj)))    ///Object already in list
                {
                    dayProfile_Schedule.Add(obj);
                    newAdd = true;
                }
                if (IsTimeSlicesOverlapping(dayProfile_Schedule, out OverlappingSlot))        ///Check TimeSlice OverLapping
                {
                    if (newAdd)
                        dayProfile_Schedule.Remove(obj);
                    if (OverlappingSlot == obj)
                        throw new Exception(String.Format("{0} 's Time Slices Overlapping", obj.TimeSlotId));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsTimeSlicesOverlapping(List<TimeSlot> timeSlices, out TimeSlot OverlappingTimeSlot, ushort Slide_Count = TimeSlot.MAX_TimeSlot)
        {
            try
            {
                timeSlices.Sort(Common_Comparable.CompareTimeSlotById);   ///Sort List Based On TimeSliceId
                for (int index = 0; (index < timeSlices.Count - 1) &&
                                    (index < Slide_Count - 1); index++)
                {
                    ///If TWO timeSlices equals
                    if (timeSlices[index] == null || timeSlices[index + 1] == null ||
                        timeSlices[index].StartTime >= timeSlices[index + 1].StartTime)
                    {
                        OverlappingTimeSlot = timeSlices[index + 1];
                        return true;
                    }
                    else if (timeSlices[index].TimeSlotId + 1 != timeSlices[index + 1].TimeSlotId)
                    {
                        OverlappingTimeSlot = timeSlices[index + 1];
                        return true;
                    }
                }
                OverlappingTimeSlot = null;
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsTimeSlicesOverlapping(List<TimeSlot> timeSlices)
        {
            TimeSlot overlappingSlot = null;
            return IsTimeSlicesOverlapping(timeSlices, out overlappingSlot);
        }

        #endregion

        private bool IsDayIdCorrect(byte dayProfileid)
        {
            if (dayProfileid >= 1 && dayProfileid <= MAX_Day_Profile)
                return true;
            else
                return false;
        }

        private void CreateDaySchedule(ushort dayProfileSchduleCount)
        {
            if (dayProfileSchduleCount >= 1 && dayProfileSchduleCount +
                dayProfile_Schedule.Count <= TimeSlot.MAX_TimeSlot)
            {
                dayProfile_Schedule.Sort((x, y) => x.TimeSlotId.CompareTo(y.TimeSlotId));       ///Sort List Based On TimeSliceId
                ushort lastTimeSliceId = 0;
                if (dayProfile_Schedule.Count >= 1)                                             ///Find Max TimeSlice ID
                {
                    lastTimeSliceId = dayProfile_Schedule[dayProfile_Schedule.Count - 1].TimeSlotId;
                }
                for (int count = 0; count < dayProfileSchduleCount; count++)
                {
                    TimeSlot TimeSlice = new TimeSlot(++lastTimeSliceId);
                    dayProfile_Schedule.Add(TimeSlice);
                    TimeSlice.StartTime_Changed += new Action<TimeSlot>(DayProfile_StartTime_Changed);
                }
            }
            else
                throw new Exception("Invalid day profile schedule count");
        }

        public TimeSlot GetDaySchedule(int dayScheduleId)
        {
            try
            {
                if (dayScheduleId > 0 && dayScheduleId <= dayProfile_Schedule.Count)
                    return dayProfile_Schedule[dayScheduleId - 1];
                else
                    return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        #region IEnumerable<TimeSlot> Members

        public IEnumerator<TimeSlot> GetEnumerator()
        {
            return dayProfile_Schedule.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return dayProfile_Schedule.GetEnumerator();
        }

        #endregion

        public override string ToString()
        {
            return "DP_" + this.Day_Profile_ID;
        }

        #region IComparable<DayProfile>_Members

        public int CompareTo(DayProfile other)
        {
            if (other == null)
                return -1;
            return this.Day_Profile_ID.CompareTo(other.Day_Profile_ID);
        }

        #endregion

        #region ISerializable Members

        protected DayProfile(SerializationInfo info, StreamingContext context)
        {
            //Getting DayProfileId Type UShort
            this.day_Profile_ID = (byte)info.GetValue("DayProfileId", typeof(byte));
            //Getting DayProfileTimeSlots Count Type int
            int TimeSliceCount = info.GetInt32("DayProfileTimeSlots");
            dayProfile_Schedule = new List<TimeSlot>();
            //Getting TimeSlice Object Type TimeSlot
            for (int index = 0; index < TimeSliceCount; index++)
            {
                String keyRet = String.Format("{0}_TimeSlotId_{1:X2}", this.ToString(), index);
                TimeSlot slice = (TimeSlot)info.GetValue(keyRet, typeof(TimeSlot));
                dayProfile_Schedule.Add(slice);
            }
            //Adding DayProfileTimeSlots Type Array []TimeSlot
            //this.dayProfile_Schedule = new List<TimeSlot>( (TimeSlot[])info.GetValue("DayProfileTimeSlots", typeof(TimeSlot[])));
            //Attach event StartTimeChangedEvent Handler
            foreach (var item in dayProfile_Schedule)
            {
                if (item != null)
                    item.StartTime_Changed += new Action<TimeSlot>(DayProfile_StartTime_Changed);
            }
            dayProfile_Schedule.Sort((x, y) => x.TimeSlotId.CompareTo(y.TimeSlotId));
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            //Adding DayProfileId Type byte
            info.AddValue("DayProfileId", this.Day_Profile_ID);
            //Adding DayProfileTimeSlots Count Type int
            info.AddValue("DayProfileTimeSlots", this.dayProfile_Schedule.Count);
            for (int index = 0; index < dayProfile_Schedule.Count; index++)
            {
                String keyRet = String.Format("{0}_TimeSlotId_{1:X2}", this.ToString(), index);
                info.AddValue(keyRet, dayProfile_Schedule[index]);
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
                throw new Exception("Error occured while Clone DayProfile", ex);
            }
        }
    }
}
