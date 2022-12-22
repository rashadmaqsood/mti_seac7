using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS.Comm;

namespace DLMS
{
    /// <summary>
    /// Activity_Calendar(REF) (class_id: 20, version: 0) allows modeling the handling of various tariff structures in the meter.
    /// provides a list of scheduled actions, following the classical way of calendar based schedules by defining seasons, weeks…
    /// An Activity_Calendar(REF) object may co-exist with the more general Schedule(REF) object and it can even overlap with it.
    /// If actions in a Schedule(REF) object are scheduled for the same activation time as in an Activity calendar object, 
    /// the actions triggered by the Schedule(REF) object are executed first.
    /// After a power failure, only the last_action missed from the object Activity_calendar(REF) is executed (delayed).
    /// This is to ensure proper tariffication after power up.
    /// If a Schedule(REF) object is present, then the missed last_action of the Activity_Calendar must be executed at the correct time within the sequence of actions requested by the Schedule(REF) object.
    /// The Activity_Calendar object defines the activation of certain scripts, which can perform different activities inside the logical device. 
    /// its object is identified by the attribute Index<see cref="StOBISCode"/>
    /// If an instance of the Class Special_Days_Table(REF) is available, relevant entries there take precedence over the Activity_Calendar object driven selection of a day profile.
    /// The day profile referenced in the Special_Days_Table activates the day_schedule of the day_profile_table in the Activity_Calendar object by referencing through the day_id.
    /// </summary>
    public class Class_20 : Base_Class
    {
        #region DataMembers_Activity_Calendar

        public static readonly byte ACTIVATE_CALENDAR = 0x01;
        // Active Calendar
        private String calendarNameActive;
        private List<StSeasonProfile> seasonProfileActive;
        private List<StWeekProfile> weekProfileActive;
        private List<StDayProfile> dayProfileActive;
        // Passive Calendar
        private String calendarNamePassive;
        private List<StSeasonProfile> seasonProfilePassive;
        private List<StWeekProfile> weekProfilePassive;
        private List<StDayProfile> dayProfilePassive;
        // Activate_Passive_Calendar
        private StDateTime activatePassiveCalendarDateTime;

        #endregion

        #region Properties

        /// <summary>
        /// Get or Set the identifier, which is descriptive to the set of scripts activated by the object.
        /// </summary>
        /// <returns>String</returns>
        public String CalendarNameActive
        {
            get { return calendarNameActive; }
            set { calendarNameActive = value; }
        }
        /// <summary>
        /// Contains a list of seasons defined by their starting date and a specific week profile to be executed. The list is sorted according to season start.
        /// </summary>
        public List<StSeasonProfile> SeasonProfileActive
        {
            get { return seasonProfileActive; }
            set { seasonProfileActive = value; }
        }
        /// <summary>
        /// Contains a list of <see target="StWeekProfile"/> (week profiles).
        /// </summary>
        /// <remarks>
        /// Week profiling used in seasons for teriffication. For each week profile, the day profile for every day of a week is identified.
        /// </remarks>

        public List<StWeekProfile> WeekProfileActive
        {
            get { return weekProfileActive; }
            set { weekProfileActive = value; }
        }
        /// <summary>
        ///  Contains an list of day profiles, identified by their day id, <see cref="StDayProfile"/>
        /// </summary>
        ///  <remarks>
        ///  For each day profile, a list of scheduled actions is defined by a script to be executed and the corresponding activation time (start time).
        ///  The list is sorted according to start_time. 
        ///  </remarks>

        public List<StDayProfile> DayProfileActive
        {
            get { return dayProfileActive; }
            set { dayProfileActive = value; }
        }

        public String CalendarNamePassive
        {
            get { return calendarNamePassive; }
            set { calendarNamePassive = value; }
        }

        public List<StSeasonProfile> SeasonProfilePassive
        {
            get { return seasonProfilePassive; }
            set { seasonProfilePassive = value; }
        }

        public List<StWeekProfile> WeekProfilePassive
        {
            get { return weekProfilePassive; }
            set { weekProfilePassive = value; }
        }

        public List<StDayProfile> DayProfilePassive
        {
            get { return dayProfilePassive; }
            set { dayProfilePassive = value; }
        }
        /// <summary>
        /// Defines the time of active passive calander 
        /// </summary>
        /// <remarks>A definition with "not specified" notation in all fields of the attribute will deactivate this automatism. 
        /// Partial "not specified" notation in just some fields of date and time are not allowed.</remarks>

        public StDateTime ActivatePassiveCalendarTime
        {
            get { return activatePassiveCalendarDateTime; }
            set { activatePassiveCalendarDateTime = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Index">OBIS code for specific entity</param>
        /// <param name="Obis_Code">Bytes representation of an OBIS code</param>
        /// <param name="No_of_Associations">maximum no of association</param>
        public Class_20(Get_Index Index, byte[] Obis_Code, UInt16 No_of_Associations)
            : base(20, 10, 1, Index, Obis_Code, No_of_Associations)
        {
            calendarNameActive = "";
            dayProfileActive = new List<StDayProfile>();
            weekProfileActive = new List<StWeekProfile>();
            dayProfileActive = new List<StDayProfile>();

            calendarNamePassive = "";
            dayProfilePassive = new List<StDayProfile>();
            weekProfilePassive = new List<StWeekProfile>();
            dayProfilePassive = new List<StDayProfile>();

            activatePassiveCalendarDateTime = new StDateTime();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Obis_Code">Bytes representation of an OBIS code</param>
        /// <param name="Attribute_recieved">no of attribute associated with current object</param>
        public Class_20(byte[] Obis_Code, byte Attribute_recieved)
            : base(20, 10, 1, Obis_Code)
        {
            DecodingAttribute = Attribute_recieved;

            calendarNameActive = "";
            dayProfileActive = new List<StDayProfile>();
            weekProfileActive = new List<StWeekProfile>();
            dayProfileActive = new List<StDayProfile>();

            calendarNamePassive = "";
            dayProfilePassive = new List<StDayProfile>();
            weekProfilePassive = new List<StWeekProfile>();
            dayProfilePassive = new List<StDayProfile>();

            activatePassiveCalendarDateTime = new StDateTime();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="OBISCodeStruct">OBIS code for a specific Object</param>
        public Class_20(StOBISCode OBISCodeStruct)
            : base(OBISCodeStruct, 10, 1)
        {
            calendarNameActive = "";
            dayProfileActive = new List<StDayProfile>();
            weekProfileActive = new List<StWeekProfile>();
            dayProfileActive = new List<StDayProfile>();

            calendarNamePassive = "";
            dayProfilePassive = new List<StDayProfile>();
            weekProfilePassive = new List<StWeekProfile>();
            dayProfilePassive = new List<StDayProfile>();

            activatePassiveCalendarDateTime = new StDateTime();
        }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="obj">Class_20 Object</param>
        public Class_20(Class_20 obj)
            : base(obj)
        {
            calendarNameActive = obj.calendarNameActive;
            ///Active Calendar
            if (obj.seasonProfileActive != null && obj.seasonProfileActive.Count > 0)
                seasonProfileActive = new List<StSeasonProfile>(obj.seasonProfileActive);
            else
                seasonProfileActive = new List<StSeasonProfile>();
            if (obj.weekProfileActive != null && obj.weekProfileActive.Count > 0)
                weekProfileActive = new List<StWeekProfile>(obj.weekProfileActive);
            else
                weekProfileActive = new List<StWeekProfile>();
            if (obj.dayProfileActive != null && obj.dayProfileActive.Count > 0)
                dayProfileActive = new List<StDayProfile>(obj.dayProfileActive);
            ///Passive Calendar
            calendarNamePassive = obj.calendarNamePassive;
            if (obj.seasonProfilePassive != null && obj.seasonProfilePassive.Count > 0)
                seasonProfilePassive = new List<StSeasonProfile>(obj.seasonProfilePassive);
            else
                seasonProfilePassive = new List<StSeasonProfile>();
            if (obj.weekProfilePassive != null && obj.weekProfilePassive.Count > 0)
                weekProfilePassive = new List<StWeekProfile>(obj.weekProfilePassive);
            else
                weekProfilePassive = new List<StWeekProfile>();
            if (obj.dayProfilePassive != null && obj.dayProfilePassive.Count > 0)
                dayProfilePassive = new List<StDayProfile>(obj.dayProfilePassive);

            if (obj.activatePassiveCalendarDateTime != null)
                activatePassiveCalendarDateTime = (StDateTime)obj.activatePassiveCalendarDateTime.Clone();
        }

        #endregion

        #region Decoder/Encoder
        /// <summary>
        /// Decode Data of this Class which is received in response of get data Request
        /// </summary>
        /// <param name="Data">Recived data from Remote site</param>
        /// <param name="array_traverse">Off-Set</param>
        /// <param name="length">Length to decode</param>
        public override void Decode_Data(ref byte[] Data, ref int array_traverse, int length)
        {
            byte[] Obis_code_recieved = null;
            byte current_char;
            //------------------------------------------------------
            try
            {
                ///SET All Attribute Access Status Results
                if (DecodingAttribute == 0x00)
                {
                    for (int index = 0; index < AccessResults.Length; index++)
                        AccessResults[index] = DecodingResult.DataNotPresent;
                }
                else if (AccessResults != null && DecodingAttribute <= AccessResults.Length)
                {
                    SetAttributeDecodingResult(DecodingAttribute, DecodingResult.DataNotPresent);
                }
                DecoderAttribute_0(ref Data, ref array_traverse, "Decode_Data_Class_20_ActivityCalendar");
                DecoderLogicalName(ref Data, ref array_traverse, ref Obis_code_recieved, "Decode_Data_Class_20_ActivityCalendar");
                #region Attribute 0x02 ActivityCalendarNameActive
                if (DecodingAttribute == 0x02 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    /// null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        if (!IsAttribReadable(2))
                            SetAttributeDecodingResult(2, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(2, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier(Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_20_ActivityCalendar");
                        }

                    }
                    else
                    {
                        array_traverse--;
                        CalendarNameActive = BasicEncodeDecode.Decode_String(Data, ref array_traverse, Data.Length);
                    }
                    SetAttributeDecodingResult(2, DecodingResult.Ready);
                }
                #endregion
                #region Attribute 0x03 SeasonProfileActive
                if (DecodingAttribute == 0x03 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        if (!IsAttribReadable(3))
                            SetAttributeDecodingResult(3, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(3, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier(Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_20_ActivityCalendar");
                        }
                    }
                    else
                    {
                        ///Decoding SeasonProfileActiveTable
                        array_traverse--;
                        SeasonProfileActive = DecodeSeasonProfile(Data, ref array_traverse, Data.Length);
                    }
                    SetAttributeDecodingResult(3, DecodingResult.Ready);
                }
                #endregion
                #region Attribute 0x04 WeekProfileActive
                if (DecodingAttribute == 0x04 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        if (!IsAttribReadable(4))
                            SetAttributeDecodingResult(4, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(4, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier(Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_20_ActivityCalendar");
                        }
                    }
                    else
                    {
                        ///Decoding WeekProfileActiveTable
                        array_traverse--;
                        WeekProfileActive = DecodeWeekProfile(Data, ref array_traverse, Data.Length);
                    }
                    SetAttributeDecodingResult(4, DecodingResult.Ready);
                }

                #endregion
                #region Attribute 0x05 DayProfileActive
                if (DecodingAttribute == 0x05 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        if (!IsAttribReadable(5))
                            SetAttributeDecodingResult(5, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(5, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier(Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_20_ActivityCalendar");
                        }
                    }
                    else
                    {
                        ///Decoding WeekProfileActiveTable
                        array_traverse--;
                        DayProfileActive = DecodeDayProfile(Data, ref array_traverse, Data.Length);
                    }
                    SetAttributeDecodingResult(5, DecodingResult.Ready);
                }

                #endregion
                #region Attribute 0x06 ActivityCalendarNamePassive
                if (DecodingAttribute == 0x06 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        if (!IsAttribReadable(6))
                            SetAttributeDecodingResult(6, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(6, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier(Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_20_ActivityCalendar");
                        }
                    }
                    else
                    {
                        //tArray = BasicEncodeDecode.Decode_OctectString(DateTime, array_traverse);
                        //char[] calendarName = ASCIIEncoding.ASCII.GetChars(tArray);
                        array_traverse--;
                        CalendarNamePassive = BasicEncodeDecode.Decode_String(Data, ref array_traverse, Data.Length);
                    }
                    SetAttributeDecodingResult(6, DecodingResult.Ready);
                }

                #endregion
                #region Attribute 0x07 SeasonProfilePassive
                if (DecodingAttribute == 0x07 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        if (!IsAttribReadable(7))
                            SetAttributeDecodingResult(7, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(7, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier(Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_20_ActivityCalendar");
                        }
                    }
                    else
                    {
                        ///Decoding SeasonProfilePassiveTable
                        array_traverse--;
                        SeasonProfilePassive = DecodeSeasonProfile(Data, ref array_traverse, Data.Length);
                    }
                    SetAttributeDecodingResult(7, DecodingResult.Ready);
                }

                #endregion
                #region Attribute 0x08 WeekProfilePassive
                if (DecodingAttribute == 0x08 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        if (!IsAttribReadable(0x08))
                            SetAttributeDecodingResult(0x08, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(0x08, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier(Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_20_ActivityCalendar");
                        }
                    }
                    else
                    {
                        ///Decoding WeekProfileActiveTable
                        array_traverse--;
                        WeekProfilePassive = DecodeWeekProfile(Data, ref array_traverse, Data.Length);
                    }
                    SetAttributeDecodingResult(0x08, DecodingResult.Ready);
                }

                #endregion
                #region Attribute 0x09 DayProfilePassive
                if (DecodingAttribute == 0x09 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        if (!IsAttribReadable(09))
                            SetAttributeDecodingResult(0x09, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(0x09, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier(Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_20_ActivityCalendar");
                        }
                    }
                    else
                    {
                        ///Decoding DayProfilePassiveTable
                        array_traverse--;
                        DayProfilePassive = DecodeDayProfile(Data, ref array_traverse, Data.Length);
                    }
                    SetAttributeDecodingResult(0x09, DecodingResult.Ready);
                }

                #endregion
                #region Attribute 0x0A activatePassiveCalendarDateTime
                if (DecodingAttribute == 0x0A || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        if (!IsAttribReadable(0x0A))
                            SetAttributeDecodingResult(0x0A, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(0x0A, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier(Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_20_ActivityCalendar");
                        }

                    }
                    else
                    {
                        //tArray = BasicEncodeDecode.Decode_OctectString(Data,ref array_traverse);
                        //BasicEncodeDecode.Decode_DateTime(tArray);
                        array_traverse--;
                        this.ActivatePassiveCalendarTime = new StDateTime();
                        this.ActivatePassiveCalendarTime.DecodeRawBytes(Data, ref array_traverse);
                    }
                    SetAttributeDecodingResult(0x0A, DecodingResult.Ready);
                }

                #endregion
                // make data array ready for upcoming objects
                ///DLMS_Common.Data_Array_resizer(ref Data, array_traverse);
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                {
                    throw ex;
                }
                else
                    throw new DLMSDecodingException("Error occurred while decoding data", "Decode_Data_Class_20_ActivityCalendar", ex);
            }
        }

        private List<StSeasonProfile> DecodeSeasonProfile(byte[] Data, ref int array_traverse, int length)
        {
            try
            {
                byte[] to_Compare_Array = null;
                byte current_char = 0;
                //Array Of SeasonProfiles Structure
                current_char = Data[array_traverse++];
                if (current_char != (byte)DataTypes._A01_array)
                    throw new DLMSDecodingException(String.Format("Invalid data Type,array data type is expected,invalid identifier (Error Code:{0})"
                            , (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_20_ActivityCalendar");
                int SeasonProfileCount = BasicEncodeDecode.Decode_Length(Data, ref array_traverse);
                int SpProfileCount = 1;
                byte[] ReadValues = null;
                List<StSeasonProfile> seasonProfilesList = new List<StSeasonProfile>(SeasonProfileCount);
                while (SpProfileCount <= SeasonProfileCount)
                {
                    // <DataType Structure> Three Elements
                    to_Compare_Array = new byte[] { (byte)DataTypes._A02_structure, 0x03 };
                    ReadValues = new byte[2];
                    Array.Copy(Data, array_traverse, ReadValues, 0, ReadValues.Length);
                    array_traverse += 2;
                    if (!to_Compare_Array.SequenceEqual<byte>(ReadValues))
                        throw new DLMSDecodingException("Invalid Season Profile structure", "Decode_Data_Class_20_ActivityCalendar");
                    ///Decoding Season Profile Name
                    String SeasonProfileName = BasicEncodeDecode.Decode_String(Data, ref array_traverse, Data.Length);
                    ///Decoding Season Profile Start Date
                    //ReadValues = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse);
                    //DateTime SeasonProfileDate = BasicEncodeDecode.Decode_DateTime(ReadValues);
                    StDateTime SeasonProfileDate = new StDateTime();
                    SeasonProfileDate.DecodeRawBytes(Data, ref array_traverse);
                    ///Decoding Week Profile Name
                    String WeekProfileName = BasicEncodeDecode.Decode_String(Data, ref array_traverse, Data.Length);

                    StSeasonProfile SeasonProfile = new StSeasonProfile(SeasonProfileName, SeasonProfileDate, WeekProfileName);
                    seasonProfilesList.Add(SeasonProfile);
                    SpProfileCount++;

                }
                if (SeasonProfileCount != seasonProfilesList.Count)
                    throw new DLMSDecodingException(String.Format("Invalid number of Season Profile entries received {0}",
                                                                               SpProfileCount), "Decode_Data_Class_11_SpecialDayTable");
                return seasonProfilesList;
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                {
                    throw ex;
                }
                else
                    throw new DLMSException(String.Format("{0}_{1}_{2}_{3} (Error Code:{4})", "Error occurred while decoding SeasonProfileTable ",
                            OBISIndex, OBISIndex.OBISIndex, "Decode_Data_Class_20_ActivityCalendar", (int)DLMSErrors.ErrorDecoding_Type), ex);
            }
        }

        private List<StWeekProfile> DecodeWeekProfile(byte[] Data, ref int array_traverse, int length)
        {
            try
            {
                byte[] to_Compare_Array = null;
                byte current_char = 0;
                //Array Of WeekProfile Structure
                current_char = Data[array_traverse++];
                if (current_char != (byte)DataTypes._A01_array)
                    throw new DLMSDecodingException(String.Format("Invalid data Type,array data type is expected,invalid identifier (Error Code:{0})"
                             , (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_20_ActivityCalendar");
                int WeekProfileCountRecv = BasicEncodeDecode.Decode_Length(Data, ref array_traverse);
                int weekProfileCount = 1;
                byte[] ReadValues = null;
                List<StWeekProfile> weekProfilesList = new List<StWeekProfile>(WeekProfileCountRecv);
                while (weekProfileCount <= WeekProfileCountRecv)
                {
                    // <DataType Structure> Three Elements
                    to_Compare_Array = new byte[] { (byte)DataTypes._A02_structure, 0x08 };
                    ReadValues = new byte[2];
                    Array.Copy(Data, array_traverse, ReadValues, 0, ReadValues.Length);
                    array_traverse += 2;
                    if (!to_Compare_Array.SequenceEqual<byte>(ReadValues))
                        throw new DLMSDecodingException(String.Format("Invalid Season Profile structure,invalid identifier (Error Code:{0})",
                                                       (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_20_ActivityCalendar");
                    ///Decoding Week Profile Name
                    String WeekProfileName = BasicEncodeDecode.Decode_String(Data, ref array_traverse, length);
                    ///Decoding Monday DayProfile ID
                    byte monDPId = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, length));
                    ///Decoding Tuesday DayProfile ID
                    byte tueDPId = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, length));
                    ///Decoding Wednesday DayProfile ID
                    byte wedDPId = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, length));
                    ///Decoding Thursday DayProfile ID
                    byte thurDPId = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, length));
                    ///Decoding Friday DayProfile ID
                    byte friDPId = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, length));
                    ///Decoding Saturday DayProfile ID
                    byte satDPId = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, length));
                    ///Decoding Sunday DayProfile ID
                    byte sunDPId = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, length));

                    StWeekProfile WkProfile = new StWeekProfile(WeekProfileName, monDPId, tueDPId, wedDPId, thurDPId, friDPId, satDPId, sunDPId);

                    weekProfilesList.Add(WkProfile);
                    weekProfileCount++;

                }
                if (WeekProfileCountRecv != weekProfilesList.Count)
                    throw new DLMSDecodingException(String.Format("Invalid number of Week Profile entities received {0}",
                                                                               weekProfileCount), "Decode_Data_Class_11_SpecialDayTable");
                return weekProfilesList;
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                {
                    throw ex;
                }
                else
                    throw new DLMSDecodingException(String.Format("Error occurred while decoding WeekProfileTable (Error Code:{0})"
                            , (int)DLMSErrors.ErrorDecoding_Type), "Decode_Data_Class_20_ActivityCalendar", ex);
            }
        }

        private List<StDayProfile> DecodeDayProfile(byte[] Data, ref int array_traverse, int length)
        {
            try
            {
                byte[] to_Compare_Array = null;
                byte current_char = 0;
                //Array Of DayProfile Structure
                current_char = Data[array_traverse++];
                if (current_char != (byte)DataTypes._A01_array)
                    throw new DLMSDecodingException(String.Format("Invalid data Type,array data type is expected,invalid identifier (Error Code:{0})"
                         , (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_20_ActivityCalendar");
                int DayProfileCountRecv = BasicEncodeDecode.Decode_Length(Data, ref array_traverse);
                int DayProfileCount = 1;
                byte[] ReadValues = null;
                List<StDayProfile> dayProfileList = new List<StDayProfile>();

                while (DayProfileCount <= DayProfileCountRecv)
                {
                    // <DataType Structure> 2 Elements
                    to_Compare_Array = new byte[] { (byte)DataTypes._A02_structure, 0x02 };
                    ReadValues = new byte[2];
                    Array.Copy(Data, array_traverse, ReadValues, 0, ReadValues.Length);
                    array_traverse += 2;
                    if (!to_Compare_Array.SequenceEqual<byte>(ReadValues))
                        throw new DLMSDecodingException(String.Format("Invalid Day Profile Structure,Invalid identifier (Error Code:{0})",
                            (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_20_ActivityCalendar");
                    ///Decoding Day Profile ID
                    byte dayProfileId = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, length));
                    ///Decoding DayProfile Schedule Table
                    current_char = Data[array_traverse++];
                    if (current_char != (byte)DataTypes._A01_array)
                        throw new DLMSDecodingException(String.Format("Invalid data Type,array data type is expected,invalid identifier (Error Code:{0})"
                            , (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_20_ActivityCalendar");
                    int day_profile_actionCountRecv = BasicEncodeDecode.Decode_Length(Data, ref array_traverse);
                    int dayProfileScheduleCount = 1;
                    List<StDayProfileAction> DayProfileActionsList = new List<StDayProfileAction>(day_profile_actionCountRecv);
                    while (dayProfileScheduleCount <= day_profile_actionCountRecv)
                    {
                        // <DataType Structure> 3 Elements
                        to_Compare_Array = new byte[] { (byte)DataTypes._A02_structure, 0x03 };
                        ReadValues = new byte[2];
                        Array.Copy(Data, array_traverse, ReadValues, 0, ReadValues.Length);
                        array_traverse += 2;
                        if (!to_Compare_Array.SequenceEqual<byte>(ReadValues))
                            throw new DLMSDecodingException(String.Format("Invalid Day Profile Action/TimeSlots structure,invalid identifier (Error Code:{0})"
                                , (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_20_ActivityCalendar");
                        // Decoding DayProfileSchedule StartTime
                        // ReadValues = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse);
                        StDateTime StartTime = new StDateTime();
                        StartTime.DecodeRawBytes(Data, ref  array_traverse);
                        // Decoding script_logical_name OBIS CODE
                        byte[] script_logical_name = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse, Data.Length);
                        // Decoding Script Selector long_unsigned
                        ushort scriptSelector = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, Data.Length));

                        StDayProfileAction dayProfileScheduleEntity = new StDayProfileAction(StartTime, script_logical_name, scriptSelector);
                        DayProfileActionsList.Add(dayProfileScheduleEntity);
                        dayProfileScheduleCount++;
                    }

                    if (day_profile_actionCountRecv != DayProfileActionsList.Count)
                        throw new DLMSDecodingException(String.Format("Invalid number of DayProfileScheduleActionEntries received {0}",
                                                                       day_profile_actionCountRecv),
                                                                       "Decode_Data_Class_11_SpecialDayTable");

                    StDayProfile dayProfileEntry = new StDayProfile(dayProfileId, DayProfileActionsList);
                    dayProfileList.Add(dayProfileEntry);
                    DayProfileCount++;
                }

                if (DayProfileCountRecv != dayProfileList.Count)
                    throw new DLMSDecodingException(String.Format("Invalid number of Day Profile entries received {0}",
                                                    DayProfileCountRecv), "Decode_Data_Class_11_SpecialDayTable");
                return dayProfileList;
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                {
                    throw ex;
                }
                else
                    throw new DLMSDecodingException(String.Format("Error occurred while decoding DayProfileTable (Error Code:{0})"
                        , (int)DLMSErrors.ErrorDecoding_Type), "Decode_Data_Class_20_ActivityCalendar", ex);
            }
        }

        /// <summary>
        /// Set the Request Encoder
        /// </summary>
        /// <returns>byte[]</returns>
        public override byte[] Encode_Data()
        {
            try
            {
                EncodedRaw = new List<byte>(0x0A);
                byte[] t_Array = null;
                //------------------------------------------------------
                EncoderAttribute_0();
                EncoderLogicalName();
                //------------------------------------------------------
                #region Attribute 0x02 ActivityCalendarNameActive
                if (EncodingAttribute == 0x02 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x02);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode,No Access (Error Code:{0})",
                            (int)DLMSErrors.Insufficient_Priviledge), "Encode_Data_Class_20_ActivityCalendar");
                    }
                    ///Encode Here Data
                    else if (EncodingAttribute == 0x02)
                    {
                        EncodedRaw.AddRange(BasicEncodeDecode.Encode_String(this.CalendarNameActive, DataTypes._A09_octet_string));
                    }
                }
                #endregion
                //------------------------------------------------------ 
                #region Attribute 0x03 ActivityCalendar SeasonProfileActive

                if (EncodingAttribute == 0x03 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x03);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode,No Access (Error Code:{0})",
                            (int)DLMSErrors.Insufficient_Priviledge), "Encode_Data_Class_20_ActivityCalendar");
                    }

                    // Encode Here Data
                    else if (EncodingAttribute == 0x03)
                    {
                        t_Array = EncodeSeasonProfile(this.SeasonProfileActive);
                        EncodedRaw.AddRange(t_Array);
                    }
                }
                #endregion
                //------------------------------------------------------ 
                #region Attribute 0x04 ActivityCalendar WeekProfileActive
                if (EncodingAttribute == 0x04 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x04);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode,No Access (Error Code:{0})",
                            (int)DLMSErrors.Insufficient_Priviledge), "Encode_Data_Class_20_ActivityCalendar");
                    }
                    ///Encode Here Data
                    else if (EncodingAttribute == 0x04)
                    {
                        t_Array = EncodeWeekProfile(WeekProfileActive);
                        EncodedRaw.AddRange(t_Array);
                    }
                }
                #endregion
                //------------------------------------------------------
                #region Attribute 0x05 ActivityCalendar DayProfileActive
                if (EncodingAttribute == 0x05 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x05);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode,No Access (Error Code:{0})",
                            (int)DLMSErrors.Insufficient_Priviledge), "Encode_Data_Class_20_ActivityCalendar");
                    }
                    ///Encode Here Data
                    else if (EncodingAttribute == 0x05)
                    {
                        t_Array = EncodeDayProfie(this.DayProfileActive);
                        EncodedRaw.AddRange(t_Array);
                    }
                }
                #endregion
                //------------------------------------------------------
                #region Attribute 0x06 ActivityCalendarNamePassive
                if (EncodingAttribute == 0x06 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x06);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode,No Access (Error Code:{0})",
                            (int)DLMSErrors.Insufficient_Priviledge), "Encode_Data_Class_20_ActivityCalendar");
                    }
                    // Encode Here Data
                    else if (EncodingAttribute == 0x06)
                    {
                        EncodedRaw.AddRange(BasicEncodeDecode.Encode_String(this.CalendarNamePassive, DataTypes._A09_octet_string));
                    }
                }
                #endregion
                //------------------------------------------------------ 
                #region Attribute 0x07 ActivityCalendar SeasonProfilePassive
                if (EncodingAttribute == 0x07 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x07);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode,No Access (Error Code:{0})",
                            (int)DLMSErrors.Insufficient_Priviledge), "Encode_Data_Class_20_ActivityCalendar");
                    }
                    ///Encode Here Data
                    else if (EncodingAttribute == 0x07)
                    {
                        t_Array = EncodeSeasonProfile(this.SeasonProfilePassive);
                        EncodedRaw.AddRange(t_Array);
                    }
                }
                #endregion
                //------------------------------------------------------
                #region Attribute 0x08 ActivityCalendar WeekProfilePassive
                if (EncodingAttribute == 0x08 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x07);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode,No Access (Error Code:{0})",
                            (int)DLMSErrors.Insufficient_Priviledge), "Encode_Data_Class_20_ActivityCalendar");
                    }
                    ///Encode Here Data
                    else if (EncodingAttribute == 0x08)
                    {
                        t_Array = EncodeWeekProfile(this.WeekProfilePassive);
                        EncodedRaw.AddRange(t_Array);
                    }
                }
                #endregion
                //------------------------------------------------------
                #region Attribute 0x09 ActivityCalendar DayProfilePassive
                if (EncodingAttribute == 0x09 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x09);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode,No Access (Error Code:{0})",
                            (int)DLMSErrors.Insufficient_Priviledge), "Encode_Data_Class_20_ActivityCalendar");
                    }
                    ///Encode Here Data
                    else if (EncodingAttribute == 0x09)
                    {
                        t_Array = EncodeDayProfie(this.DayProfilePassive);
                        EncodedRaw.AddRange(t_Array);
                    }
                }
                #endregion
                //------------------------------------------------------
                #region Attribute 0x0A ActivityCalendar_Activate_Passive_Calendar_DateTime

                if (EncodingAttribute == 0x0A || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x0A);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode,No Access (Error Code:{0})",
                            (int)DLMSErrors.Insufficient_Priviledge), "Encode_Data_Class_20_ActivityCalendar");
                    }
                    // Encode Here Data
                    else if (EncodingAttribute == 0x0A)
                    {
                        // <DataType _A09_octet_string> Activate_Passive_Calendar_DateTime
                        // BasicEncodeDecode.Encode_DateTime(this.ActivatePassiveCalendarTime, ref t_Array);
                        // t_Array = BasicEncodeDecode.Encode_OctetString((byte[])t_Array.Clone(), DataTypes._A09_octet_string);
                        t_Array = ActivatePassiveCalendarTime.EncodeRawBytes(StDateTime.DateTimeType.DateTime);
                        EncodedRaw.AddRange(t_Array);
                    }
                }

                #endregion
                //------------------------------------------------------
                return EncodedRaw.ToArray<byte>();

            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                {
                    throw ex;
                }
                else
                    throw new DLMSException(String.Format("{0}_{1}  (Error Code:{2})", "Error occurred while encoding data",
                                            "Encode_Data_Class_20_ActivityCalendar",
                                            (int)DLMSErrors.ErrorEncoding_Type), ex);
            }
            finally
            {
                EncodedRaw = null;
            }
        }

        private byte[] EncodeSeasonProfile(List<StSeasonProfile> SeasonProfiles)
        {
            try
            {
                List<byte> EncodedRaw = new List<byte>(0x0A);
                byte[] t_Array = null;
                int SeasonProfileCount = SeasonProfiles.Count;
                int SpProfileCount = 1;
                BasicEncodeDecode.Encode_Length(ref t_Array, (ushort)SeasonProfileCount);
                ///<DataType Array> ArrayLength 
                EncodedRaw.Add((byte)DataTypes._A01_array);
                EncodedRaw.AddRange(t_Array);
                while (SpProfileCount <= SeasonProfileCount)
                {
                    StSeasonProfile tSeasonProfile = SeasonProfiles[SpProfileCount - 1];
                    // <DataType Structure> 0x03 Structure Elements
                    t_Array = new byte[] { (byte)DataTypes._A02_structure, 0x03 };
                    EncodedRaw.AddRange(t_Array);
                    // <DataType _A09_octet_string> SeasonProfileName
                    t_Array = BasicEncodeDecode.Encode_String(tSeasonProfile.ProfileName, DataTypes._A09_octet_string);
                    EncodedRaw.AddRange(t_Array);
                    // <DataType _A09_octet_string> SeasonProfileStartDate
                    //BasicEncodeDecode.Encode_DateTime(tSeasonProfile.StartDate, ref t_Array);
                    //t_Array = BasicEncodeDecode.Encode_OctetString((byte[])t_Array.Clone(), DataTypes._A09_octet_string);
                    t_Array = tSeasonProfile.StartDate.EncodeRawBytes(StDateTime.DateTimeType.DateTime);
                    EncodedRaw.AddRange(t_Array);
                    // <DataType _A09_octet_string> WeekProfileName
                    t_Array = BasicEncodeDecode.Encode_String(tSeasonProfile.WeekProfileName, DataTypes._A09_octet_string);
                    EncodedRaw.AddRange(t_Array);

                    SpProfileCount++;
                }
                return EncodedRaw.ToArray();
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                {
                    throw ex;
                }
                else
                    throw new DLMSException(String.Format("{0}_{1}  (Error Code:{2})", "Error occurred while Encoding Season Profiles",
                                "Encode_Data_Class_20_ActivityCalendar",
                                (int)DLMSErrors.ErrorEncoding_Type), ex);
            }
        }

        private byte[] EncodeWeekProfile(List<StWeekProfile> WeekProfiles)
        {
            try
            {
                List<byte> EncodedRaw = new List<byte>(0x0A);
                byte[] t_Array = null;
                int WeekProfileCount = WeekProfiles.Count;
                int WkProfileCount = 1;
                BasicEncodeDecode.Encode_Length(ref t_Array, (ushort)WeekProfileCount);
                ///<DataType Array> ArrayLength 
                EncodedRaw.Add((byte)DataTypes._A01_array);
                EncodedRaw.AddRange(t_Array);
                while (WkProfileCount <= WeekProfileCount)
                {
                    StWeekProfile tWeekProfile = WeekProfiles[WkProfileCount - 1];
                    // <DataType Structure> 0x08 Structure Elements
                    t_Array = new byte[] { (byte)DataTypes._A02_structure, 0x08 };
                    EncodedRaw.AddRange(t_Array);
                    // <DataType _A09_octet_string> WeekProfileName
                    t_Array = BasicEncodeDecode.Encode_String(tWeekProfile.ProfileName, DataTypes._A09_octet_string);
                    EncodedRaw.AddRange(t_Array);
                    // <DataType _A11_unsigned> MondayDayProfileId
                    t_Array = BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, tWeekProfile.DayProfileIdMon);
                    EncodedRaw.AddRange(t_Array);
                    // <DataType _A11_unsigned> TuesdayDayProfileId
                    t_Array = BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, tWeekProfile.DayProfileIdTue);
                    EncodedRaw.AddRange(t_Array);
                    // <DataType _A11_unsigned> WednesdayDayProfileId
                    t_Array = BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, tWeekProfile.DayProfileIdWed);
                    EncodedRaw.AddRange(t_Array);
                    // <DataType _A11_unsigned> ThursdayDayProfileId
                    t_Array = BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, tWeekProfile.DayProfileIdThu);
                    EncodedRaw.AddRange(t_Array);
                    // <DataType _A11_unsigned> FridayDayProfileId
                    t_Array = BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, tWeekProfile.DayProfileIdFri);
                    EncodedRaw.AddRange(t_Array);
                    // <DataType _A11_unsigned> SaturdayDayProfileId
                    t_Array = BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, tWeekProfile.DayProfileIdSat);
                    EncodedRaw.AddRange(t_Array);
                    // <DataType _A11_unsigned> SundayDayProfileId
                    t_Array = BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, tWeekProfile.DayProfileIdSun);
                    EncodedRaw.AddRange(t_Array);
                    WkProfileCount++;
                }
                return EncodedRaw.ToArray();
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                {
                    throw ex;
                }
                else
                    throw new DLMSException(String.Format("{0}_{1}  (Error Code:{2})", "Error occurred while Encoding Week Profiles",
                                "Encode_Data_Class_20_ActivityCalendar",
                                (int)DLMSErrors.ErrorEncoding_Type), ex);
            }
        }

        private byte[] EncodeDayProfie(List<StDayProfile> DayProfiles)
        {
            try
            {
                List<byte> EncodedRaw = new List<byte>(0x0A);
                byte[] t_Array = null;
                int DayProfileListCount = DayProfiles.Count;
                BasicEncodeDecode.Encode_Length(ref t_Array, (ushort)DayProfileListCount);
                ///Encode Day Profile
                ///<DataType Array> ArrayLength 
                EncodedRaw.Add((byte)DataTypes._A01_array);
                EncodedRaw.AddRange(t_Array);

                int dayProfileCount = 1;
                while (dayProfileCount <= DayProfileListCount)
                {
                    StDayProfile tDayProfile = DayProfiles[dayProfileCount - 1];
                    ///Encode DayProfile Structure
                    // <DataType Structure> 0x02 Structure Elements
                    t_Array = new byte[] { (byte)DataTypes._A02_structure, 0x02 };
                    EncodedRaw.AddRange(t_Array);
                    ///Encode DayProfileId
                    // <DataType _A11_unsigned> 0x02 Structure Elements
                    t_Array = BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, tDayProfile.DayId);
                    EncodedRaw.AddRange(t_Array);
                    ///Encode DayProfile Action Schedules
                    // <DataType _A09_Array> ArrayLength
                    List<StDayProfileAction> DayProfileActionsList = tDayProfile.DaySchedule;
                    int day_profile_actionListCount = DayProfileActionsList.Count;
                    BasicEncodeDecode.Encode_Length(ref t_Array, (ushort)day_profile_actionListCount);
                    EncodedRaw.Add((byte)DataTypes._A01_array);
                    EncodedRaw.AddRange(t_Array);
                    ///Encode DayProfile Action Entries
                    int dayProfileScheduleCount = 1;
                    while (dayProfileScheduleCount <= day_profile_actionListCount)
                    {
                        StDayProfileAction dayProfileScheduleEntity = DayProfileActionsList[dayProfileScheduleCount - 1];
                        // <DataType Structure> 3 Elements
                        t_Array = new byte[] { (byte)DataTypes._A02_structure, 0x03 };
                        EncodedRaw.AddRange(t_Array);
                        ///Encoding DayProfileSchedule StartTime
                        // <DataType _A09_octet_string> Length,Data
                        //BasicEncodeDecode.Encode_Time(DateTime.MinValue.Add(dayProfileScheduleEntity.StartTime) , ref t_Array);
                        //t_Array = BasicEncodeDecode.Encode_OctetString((byte[])t_Array.Clone(),DataTypes._A09_octet_string);
                        t_Array = dayProfileScheduleEntity.StartTime.EncodeRawBytes(StDateTime.DateTimeType.Time);
                        EncodedRaw.AddRange(t_Array);
                        ///Encoding script_logical_name OBISCode
                        // <DataType _A09_octet_string> Length,Data
                        t_Array = BasicEncodeDecode.Encode_OctetString(dayProfileScheduleEntity.Script_logicalName, DataTypes._A09_octet_string);
                        EncodedRaw.AddRange(t_Array);
                        ///Encoding script_selector long_unsigned value
                        // <DataType _A12_long_unsigned> Value
                        t_Array = BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, dayProfileScheduleEntity.ScriptSelector);
                        EncodedRaw.AddRange(t_Array);
                        dayProfileScheduleCount++;
                    }
                    dayProfileCount++;
                }
                return EncodedRaw.ToArray();
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                {
                    throw ex;
                }
                else
                    throw new DLMSException(String.Format("{0}_{1}  (Error Code:{2})", "Error occurred while Encoding Day Profiles",
                                    "Encode_Data_Class_20_ActivityCalendar",
                                    (int)DLMSErrors.ErrorEncoding_Type), ex);
            }
        }

        #endregion

        #region Member Methods

        public override object Clone()
        {
            Class_20 cloned = new Class_20(this);

            if (cloned.seasonProfileActive != null && cloned.seasonProfileActive.Count > 0)
                cloned.seasonProfileActive.Clear();
            if (cloned.weekProfileActive != null && cloned.weekProfileActive.Count > 0)
                cloned.weekProfileActive.Clear();
            if (cloned.dayProfileActive != null && cloned.dayProfileActive.Count > 0)
                cloned.dayProfileActive.Clear();
            // Passive Calendar
            if (cloned.seasonProfilePassive != null && cloned.seasonProfilePassive.Count > 0)
                cloned.seasonProfilePassive.Clear();
            if (cloned.weekProfilePassive != null && cloned.weekProfilePassive.Count > 0)
                cloned.weekProfilePassive.Clear();
            if (cloned.dayProfilePassive != null && cloned.dayProfilePassive.Count > 0)
                cloned.dayProfilePassive.Clear();

            return cloned;
        }

        #endregion
    }
}
