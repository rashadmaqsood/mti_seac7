using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace DLMS.Comm
{
    #region StDateTime

    /// <summary>
    /// In COSEM, date and time values are represented as attributes with the data type octet-string.
    /// The formatting of each element is defined precisely. this structure provide a wide functionality for date and time processing.
    /// </summary>
    [XmlInclude(typeof(StDateTime))]
    [Serializable]
    public partial class StDateTime : IComparable<StDateTime>, ICloneable, ISerializable
    {
        #region Data_Members
        
        /// <summary>
        /// Allowed Year Value Null/Not Specified Value
        /// </summary>
        public static readonly ushort NullYear = 0xFFFF;
        /// <summary>
        /// Allowed Null/Not Specified Value
        /// </summary>
        public static readonly byte Null = 0xFF;
        /// <summary>
        /// Allowed UTC offset Null/NotSpecified Values
        /// </summary>
        public static readonly short NullUTCOffset = short.MinValue;
        /// <summary>
        /// WildCard of Month Field,Daylight Saving Begin
        /// </summary>
        public static readonly byte DaylightSavingBegin = 0xFE;
        /// <summary>
        /// WildCard of Month Field,Daylight Saving End
        /// </summary>
        public static readonly byte DaylightSavingEnd = 0xFD;
        /// <summary>
        /// Wild Card Allowed For DayOfMonth Field,Specify Last Day Of Month
        /// </summary>
        public static readonly byte LastDayOfMonth = 0xFE;
        /// <summary>
        /// Wild Card Allowed For DayOfMonth Field,Specify Second Last Day Of Month
        /// </summary>
        public static readonly byte SecondLastDayOfMonth = 0xFD;
        /// <summary>
        /// Get the Default Date & Time
        /// </summary>

        public static readonly DateTime DefaultDateTime = DateTime.MinValue;

        private ushort year;
        private byte month;
        private byte dayOfMonth;
        private byte dayOfWeek;
        // Time
        private byte hour;
        private byte minute;
        private byte second;
        private byte hundredth;

        private short uTCOffset;
        private byte clockStatus;
        // Supporting Variables
        private DateTimeType kind;
        private String ErrorMessage = "";
        /// <summary>
        /// GET/SET the Error Message occurs during Parsing process
        /// </summary>
        public String ErrorMessageStr
        {
            get { return ErrorMessage; }
            set { ErrorMessage = value; }
        }

        #endregion

        #region Properties
        /// <summary>
        /// GET/SET valid year
        /// </summary>
        public ushort Year
        {
            get { return year; }
            set
            { //ahmed added value>=0
                //Year Validity Checks
                if ((value > 0 && value < ushort.MaxValue) || value == NullYear)
                    year = value;
                else
                    throw new Exception(String.Format("Invalid Year Value {0:X2}", value));
            }
        }
        /// <summary>
        ///  GET/SET valid month
        /// </summary>
        public byte Month
        {
            get { return month; }
            set
            {
                //Month Validity Checks
                if ((value >= 0 && value <= 12) ||
                    value == DaylightSavingBegin ||
                    value == DaylightSavingEnd ||
                    value == Null
                    )
                    month = value;
                else

                    throw new Exception(String.Format("Invalid Month Value {0:X2}", value));
            }
        }
        /// <summary>
        ///  GET/SET valid Total days in current selected month
        /// </summary>
        public byte DayOfMonth
        {
            get { return dayOfMonth; }
            set
            {
                //Day Of Month Validity Checks
                if ((value >= 0 && value <= 31) ||
                    value == LastDayOfMonth ||
                    value == SecondLastDayOfMonth ||
                    value == Null ||
                    (value >= 0xE0 && value <= 0xFC))
                    dayOfMonth = value;
                else
                    throw new Exception(String.Format("Invalid DayOfMonth field value {0:X2}", value));
            }
        }
        /// <summary>
        ///  GET/SET valid days in week
        /// </summary>
        public byte DayOfWeek
        {
            get { return dayOfWeek; }
            set
            {
                //DayOfWeek Validity Check
                if ((value >= 0 && value <= 7) ||
                    value == Null)
                    dayOfWeek = value;
                else
                    throw new Exception(String.Format("Invalid DayOfWeek field value {0:X2}", value));
            }
        }
        /// <summary>
        ///  GET/SET valid Hour
        /// </summary>
        public byte Hour
        {
            get { return hour; }
            set
            {
                //Hour Field Validity Check
                if ((value >= 0 && value <= 23) ||
                    value == Null
                    )
                    hour = value;
                else
                    throw new Exception(String.Format("Invalid Hour field value {0:X2}", value));
            }
        }
        /// <summary>
        ///  GET/SET valid minute
        /// </summary>
        public byte Minute
        {
            get { return minute; }
            set
            {
                //Minute Field Validity Check
                if ((value >= 0 && value <= 59) ||
                    value == Null
                    )
                    minute = value;
                else
                    throw new Exception(String.Format("Invalid Minute field value {0:X2}", value));
            }
        }
        /// <summary>
        ///  GET/SET valid seconds
        /// </summary>
        public byte Second
        {
            get { return second; }
            set
            {
                //Second Field Validity Check
                if ((value >= 0 && value <= 59) ||
                    value == Null
                    )
                    second = value;
                else
                    throw new Exception(String.Format("Invalid Second field value {0:X2}", value));
            }
        }
        /// <summary>
        ///  GET/SET valid Hundred
        /// </summary>
        public byte Hundred
        {
            get { return hundredth; }
            set
            {
                //Hundred Second Field Validity Check
                if ((value >= 0 && value <= 99) ||
                    value == Null
                    )
                    hundredth = value;
                else
                    throw new Exception(String.Format("Invalid Hundred-Second field value {0:X2}", value));

            }
        }
        /// <summary>
        ///  GET/SET valid GMT off-set
        /// </summary>
        public short UTCOffset
        {
            get { return uTCOffset; }
            set
            {
                //GMT_UTC OffSET Field Validity Check
                if ((value >= -720 && value <= 720) ||
                    value == NullUTCOffset
                    )
                    uTCOffset = value;
                else
                    throw new Exception(String.Format("Invalid GMT/UTC-OffSet field value {0:X2}", value));
            }
        }
        /// <summary>
        ///  GET/SET valid ClockStatus
        /// </summary>
        public byte ClockStatus
        {
            get { return clockStatus; }
            set
            {
                clockStatus = value;
            }
        }
        /// <summary>
        /// GET/SET valid <see cref="DateTimeType"/>
        /// </summary>
        public DateTimeType Kind
        {
            get { return kind; }
            set { kind = value; }
        }

        /// <summary>
        /// Checks either stDateTime object is Convertible into DateTime Object without rasing error
        /// </summary>
        public bool IsDateTimeConvertible
        {
            get
            {
                if (Kind != DateTimeType.DateTime)
                    return false;
                if (this.Year == NullYear || !(this.Month >= 1 && this.Month <= 12) ||
                    !(this.DayOfMonth >= 1 && this.DayOfMonth <= 31) ||
                    Hour == Null || Minute == Null || Second == Null)
                    return false;
                try
                {
                    //*** Disable Till Date & Time Corrected
                    DateTime dt = new DateTime(Year, Month, this.DayOfMonth, Hour, Minute, Second);
                    // Disable Check Till DateTime Works
                    //if (dt.DayOfWeek == ((this.DayOfWeek == 7) ? System.DayOfWeek.Sunday : (DayOfWeek)this.DayOfWeek))
                    //    return true;
                    //else
                    //    return false;
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Checks either stDateTime object is Convertible into DateTime(Date) Object without rasing error
        /// </summary>
        public bool IsDateConvertible
        {
            get
            {
                if (!(Kind == DateTimeType.DateTime || Kind == DateTimeType.Date))
                    return false;
                if (this.Year == NullYear || !(this.Month >= 1 && this.Month <= 12) ||
                    !(this.DayOfMonth >= 1 && this.DayOfMonth <= 31))
                    return false;
                try
                {
                    DateTime dt = new DateTime(Year, Month, this.DayOfMonth);
                    //*** Code Modified To Be Worked For Tester
                    //if (dt.DayOfWeek == ((this.DayOfWeek == 7) ? System.DayOfWeek.Sunday : (DayOfWeek)this.DayOfWeek))
                    //    return true;
                    //else
                    //    return false;
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Checks either Date Part Has Any Wild Card
        /// </summary>
        public bool IsDateExplicit
        {
            get
            {
                bool isDateExplicit = false;
                try
                {
                    if (!(Kind == DateTimeType.DateTime || Kind == DateTimeType.Date))
                        isDateExplicit = false;
                    if (this.Year == NullYear || !(this.Month >= 1 && this.Month <= 12) ||
                        !(this.DayOfMonth >= 1 && this.DayOfMonth <= 31) || !(dayOfWeek >= 1 && dayOfWeek <= 7))
                        isDateExplicit = false;
                    else
                        isDateExplicit = true;
                }
                catch (Exception) { }
                return isDateExplicit;
            }
        }
        /// <summary>
        /// Is Time is User defined by a user
        /// </summary>
        public bool IsTimeExplicit
        {
            get
            {
                bool isTimeExplicit = false;
                try
                {
                    if (!(Kind == DateTimeType.DateTime || Kind == DateTimeType.Time))
                        isTimeExplicit = false;
                    if (Hour == Null || Minute == Null || Second == Null)
                        isTimeExplicit = false;
                    else
                        isTimeExplicit = true;
                }
                catch (Exception)
                {

                }
                return isTimeExplicit;
            }
        }
        /// <summary>
        /// Is date and time is defined by a user
        /// </summary>
        public bool IsDateTimeExplicit
        {
            get
            {
                return IsDateExplicit && IsTimeExplicit;
            }
        }

        /// <summary>
        /// Checks either stDateTime object is Convertible into TimeSpan Object without rasing error
        /// </summary>
        public bool IsTimeConvertible
        {
            get
            {
                if (!(Kind == DateTimeType.DateTime || Kind == DateTimeType.Time))
                    return false;
                if (Hour == Null || Minute == Null || Second == Null)
                    return false;
                else
                    return true;
            }
        }
        /// <summary>
        /// Determine a valid date
        /// </summary>
        public bool IsDateValid
        {
            get
            {
                try
                {
                    //Validation Rule ++--__--++ If Date & Time Is Explicitly Specified
                    //then values should be valid convertible DateTime
                    if (IsDateTimeExplicit)
                    {
                        if (IsDateTimeConvertible)
                            return true;
                        else
                        {
                            ErrorMessage = "Non Wild Card Date has invalid values";
                            return false;
                        }
                    }
                    else if (IsDateExplicit)     ///Date Explicitly Specified
                    {
                        if (IsDateConvertible)
                            return true;
                        else
                        {
                            ErrorMessage = "Non Wild Card Date has invalid values";
                            return false;
                        }
                    }
                    //Validation Rule ++--__--++ If Date Time Is Wild Carded Date & Time
                    //then both DayOfMonth & DayOfWeek Not Nullable    (Not All Fields are Nullable)
                    if (Kind == DateTimeType.Date && DayOfMonth == Null && DayOfWeek == Null)
                    {
                        ErrorMessageStr = "DayOfMonth and DayOfWeek Nullable Wild Cards are not valid";
                        return false;
                    }
                    else
                        return true;
                }
                catch (Exception)
                { }
                return false;
            }
        }
        /// <summary>
        /// Determine a valid time
        /// </summary>
        public bool IsTimeValid
        {
            get
            {
                try
                {
                    if (Kind == DateTimeType.Time)
                    {
                        if (Hour == Null &&
                           Minute == Null &&
                           Second == Null &&
                           Hundred == Null)
                            return false;
                        else
                            return true;
                    }
                    else
                        return true;
                }
                catch (Exception) { }
                return false;
            }
        }
        /// <summary>
        /// Determine a valid Deviation
        /// </summary>
        public bool IsDeviationValid
        {
            get
            {
                try
                {
                    if (UTCOffset == NullUTCOffset ||
                        (UTCOffset < 720 && UTCOffset > -720))
                        return true;
                    else
                        return false;
                }
                catch (Exception) { }
                return false;
            }
        }
        /// <summary>
        /// Determine a valid date and time
        /// </summary>
        public bool IsDateTimeValid
        {
            get
            {
                return (IsDateValid && IsTimeValid);
            }
        }
        /// <summary>
        /// Determine collectively all values are valid used in this structure to form a date and time
        /// </summary>
        public bool IsValid
        {
            get
            {
                return (IsDateValid && IsTimeValid && IsDeviationValid);
            }
        }
        #endregion

        /// <summary>
        /// Initialize the structure with default values
        /// </summary>
        public StDateTime()
        {
            Year = NullYear;         //Nullable or Not Initialized
            Month = Null;
            DayOfMonth = Null;
            DayOfWeek = Null;

            Hour = Null;
            Minute = Null;
            Second = Null;
            Hundred = Null;
            UTCOffset = NullUTCOffset;
            kind = DateTimeType.DateTime;
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dtObj"><see cref="StDateTime"/></param>
        public StDateTime(StDateTime dtObj)
            : this()
        {
            Year = dtObj.Year;
            Month = dtObj.Month;
            DayOfMonth = dtObj.DayOfMonth;
            DayOfWeek = dtObj.DayOfWeek;

            Hour = dtObj.Hour;
            Minute = dtObj.Minute;
            Second = dtObj.Second;
            Hundred = dtObj.Hundred;

            UTCOffset = dtObj.UTCOffset;
            ClockStatus = dtObj.ClockStatus;

            kind = dtObj.Kind;
        }

        #region Member Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Kind"></param>
        /// <returns></returns>
        public byte[] EncodeRawBytes(DateTimeType Kind)
        {
            try
            {
                byte[] RawData = null;

                if (Kind == DateTimeType.DateTime)
                    RawData = EncodeDateTime();
                else if (Kind == DateTimeType.Date)
                    RawData = EncodeDate();
                else
                    RawData = EncodeTime();

                RawData = BasicEncodeDecode.Encode_OctetString((byte[])RawData.Clone(), DataTypes._A09_octet_string);
                return RawData;
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                    throw ex;
                else
                    throw new DLMSEncodingException(String.Format("Error occurred while Encoding DateTime (Error Code:{0})",
                                                           (int)DLMSErrors.ErrorEncoding_Type), "EncodeRawBytes", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] EncodeRawBytes()
        {
            return EncodeRawBytes(Kind);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="Indexer"></param>
        public void DecodeRawBytes(byte[] Data, ref int Indexer)
        {
            try
            {
                byte[] OctectString = BasicEncodeDecode.Decode_OctectString(Data, ref Indexer, Data.Length);
                if (OctectString == null || (OctectString.Length != 12
                                    && OctectString.Length != 4 &&
                                    OctectString.Length != 5))
                    throw new DLMSDecodingException("Error occurred while decoding Date & Time,Invalid data", "DecodeRawBytes_StDateTime");
                else if (OctectString.Length == 12)
                    DecodeDateTime(OctectString);
                else if (OctectString.Length == 5)
                    DecodeDate(OctectString);
                else
                    DecodeTime(OctectString);

            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                    throw ex;
                else
                    throw new DLMSDecodingException(String.Format("Error occurred while decoding,DateTime (Error Code:{0})",
                                                    (int)DLMSErrors.ErrorDecoding_Type), "DecodeRawBytes_StDateTime", ex);
            }

        }

        public void DecodeDateTime(byte[] DateTime_Octet_String)
        {
            try
            {
                if (DateTime_Octet_String.Length != 12 || DateTime_Octet_String == null)
                {
                    throw new DLMSDecodingException("Error occurred while decoding Date Time,Invalid Data", "Decode_DateTime_StDateTime");
                }
                ushort Year = (UInt16)(((UInt16)(DateTime_Octet_String[0]) << 8) + DateTime_Octet_String[1]);
                byte Month = DateTime_Octet_String[2];
                byte Day_of_Month = DateTime_Octet_String[3];
                byte Day_of_Week = DateTime_Octet_String[4];
                byte Hours = DateTime_Octet_String[5];
                byte Minutes = DateTime_Octet_String[6];
                byte Secs = DateTime_Octet_String[7];
                byte m_Secs_x10 = DateTime_Octet_String[8];
                short Deviation = NullUTCOffset;
                unchecked
                {
                    Deviation = (short)((DateTime_Octet_String[9] << 8) | (int)DateTime_Octet_String[10]);
                }
                byte clk_Status = DateTime_Octet_String[11];
                //Assign Value To Properties
                this.year = Year;
                this.month = Month;
                this.dayOfMonth = Day_of_Month;
                this.dayOfWeek = Day_of_Week;
                this.hour = Hours;
                this.minute = Minutes;
                this.second = Secs;
                this.hundredth = m_Secs_x10;
                this.uTCOffset = Deviation;
                this.clockStatus = clk_Status;
                kind = DateTimeType.DateTime;
            }
            catch (Exception exc)
            {
                if (exc is DLMSDecodingException)
                    throw exc;
                else
                    throw new DLMSDecodingException(String.Format("Error occurred while decoding, Date&Time (Error Code:{0})",
                                                    (int)DLMSErrors.ErrorDecoding_Type), "Decode_DateTime_StDateTime", exc);

            }
        }

        public void DecodeDate(byte[] Date_Octet_String)
        {
            try
            {
                DateTime Decoded_val;
                if (Date_Octet_String.Length != 5 || Date_Octet_String == null)
                {
                    throw new DLMSDecodingException("Error occurred while decoding Date,Invalid Data", "DecodeDate_StDateTime");
                }
                UInt16 Year = (UInt16)(((UInt16)(Date_Octet_String[0]) << 8) + Date_Octet_String[1]);
                byte Month = Date_Octet_String[2];
                byte Day_of_Month = Date_Octet_String[3];
                byte Day_of_Week = Date_Octet_String[4];
                //Assign Value To Properties
                this.year = Year;
                this.month = Month;
                this.dayOfMonth = Day_of_Month;
                this.dayOfWeek = Day_of_Week;

                //this.Hour = Null;
                //this.Minute = Null;
                //this.Second = Null;
                //this.Hundred = Null;
                //this.UTCOffset = NullUTCOffset;
                //this.ClockStatus = Null;

                kind = DateTimeType.Date;
            }
            catch (Exception exc)
            {
                if (exc is DLMSDecodingException)
                    throw exc;
                else
                    throw new DLMSDecodingException(String.Format("Error occurred while decoding,Date (Error Code:{0})",
                                                        (int)DLMSErrors.ErrorDecoding_Type), "Decode_DateTime_StDateTime", exc);
            }
        }
        
        public void DecodeTime(byte[] Time_Octet_String)
        {
            try
            {
                DateTime Decoded_val;
                
                if (Time_Octet_String.Length != 4 || Time_Octet_String == null)
                {
                    throw new DLMSDecodingException("Error occurred while decoding Time,Invalid Data", "DecodeTime_StDateTime");
                }
                byte Hours = Time_Octet_String[0];
                byte Minutes = Time_Octet_String[1];
                byte Secs = Time_Octet_String[2];
                byte m_Secs_x10 = Time_Octet_String[3];

                // Assign Value To Properties
                // this.Year = NullYear;
                // this.Month = Null;
                // this.DayOfMonth = Null;
                // this.DayOfWeek = Null;

                this.hour = Hours;
                this.minute = Minutes;
                this.second = Secs;
                this.hundredth = m_Secs_x10;

                // this.UTCOffset = NullUTCOffset;
                // this.ClockStatus = Null;

                kind = DateTimeType.Time;
            }
            catch (Exception exc)
            {
                if (exc is DLMSDecodingException)
                    throw exc;
                else
                    throw new DLMSDecodingException(String.Format("Error occurred while decoding,Date&Time (Error Code:{0})",
                                                            (int)DLMSErrors.ErrorDecoding_Type), "Decode_DateTime_StDateTime", exc);
            }
        }

        public byte[] EncodeDateTime()
        {
            try
            {
                byte[] DateTime_Octet_String = new byte[12];
                unchecked
                {
                    DateTime_Octet_String[0] = (byte)((Year >> 8) & 0xFF);
                    DateTime_Octet_String[1] = (byte)(Year & 0xFF);
                    DateTime_Octet_String[2] = Month;
                    DateTime_Octet_String[3] = DayOfMonth;
                    DateTime_Octet_String[4] = DayOfWeek;
                    DateTime_Octet_String[5] = Hour;
                    DateTime_Octet_String[6] = Minute;
                    DateTime_Octet_String[7] = Second;
                    DateTime_Octet_String[8] = Hundred;

                    DateTime_Octet_String[9] = (byte)((UTCOffset >> 8) & 0xFF);
                    DateTime_Octet_String[10] = (byte)(UTCOffset & 0xFF);
                    DateTime_Octet_String[11] = ClockStatus;
                }
                return DateTime_Octet_String;
            }
            catch (Exception exc)
            {
                if (exc is DLMSEncodingException)
                    throw exc;
                else
                    throw new DLMSEncodingException(String.Format("Error occurred while encoding DateTime,Invalid Data(Error Code:{0})",
                                                            (int)DLMSErrors.ErrorEncoding_Type), "EncodeDateTime_StDateTime", exc);
            }
        }
        public byte[] EncodeDate()
        {
            try
            {
                byte[] DateTime_Octet_String = new byte[5];
                unchecked
                {
                    DateTime_Octet_String[0] = (byte)((Year >> 8) & 0xFF);
                    DateTime_Octet_String[1] = (byte)(Year & 0xFF);
                    DateTime_Octet_String[2] = Month;
                    DateTime_Octet_String[3] = DayOfMonth;
                    DateTime_Octet_String[4] = DayOfWeek;
                }
                return DateTime_Octet_String;
            }
            catch (Exception exc)
            {
                if (exc is DLMSEncodingException)
                    throw exc;
                else
                    throw new DLMSEncodingException(String.Format("Error occurred while encoding Date,Invalid Data(Error Code:{0})",
                                                            (int)DLMSErrors.ErrorEncoding_Type), "EncodeDate_StDateTime", exc);
            }
        }
        public byte[] EncodeTime()
        {
            try
            {
                byte[] DateTime_Octet_String = new byte[4];
                unchecked
                {
                    DateTime_Octet_String[0] = Hour;
                    DateTime_Octet_String[1] = Minute;
                    DateTime_Octet_String[2] = Second;
                    DateTime_Octet_String[3] = Hundred;
                }
                return DateTime_Octet_String;
            }
            catch (Exception exc)
            {
                if (exc is DLMSEncodingException)
                    throw exc;
                else
                    throw new DLMSEncodingException(String.Format("Error occurred while encoding Time,Invalid Data(Error Code:{0})",
                                                                    (int)DLMSErrors.ErrorEncoding_Type), "EncodeTime_StDateTime", exc);
            }
        }

        /// <summary>
        /// Convert Explicit Specified Complete stDateTime Object into DateTime Stamp Object
        /// </summary>
        /// <returns></returns>
        public DateTime GetDateTime()
        {
            try
            {
                DateTime dt;
                if (IsDateTimeConvertible)    //Could be converted 
                    dt = new DateTime(this.Year, this.Month, this.DayOfMonth, this.Hour, this.Minute, this.Second, (this.Hundred == Null) ? 0 : this.Hundred * 10);
                else
                {
                    //throw new Exception("Unable to convert stDateTime object into valid DateTime Stamp");
                    dt = DefaultDateTime;
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Set date time defined by user
        /// </summary>
        /// <param name="objDateTime"></param>
        public void SetDateTime(DateTime objDateTime)
        {
            try
            {
                kind = DateTimeType.DateTime;
                this.Year = (ushort)objDateTime.Year;
                this.Month = (byte)objDateTime.Month;
                this.DayOfMonth = (byte)objDateTime.Day;
                this.DayOfWeek = (byte)((objDateTime.DayOfWeek != System.DayOfWeek.Sunday) ? (byte)objDateTime.DayOfWeek : 7);

                this.Hour = (byte)objDateTime.Hour;
                this.Minute = (byte)objDateTime.Minute;
                this.Second = (byte)objDateTime.Second;
                this.Hundred = (byte)(objDateTime.Millisecond / 10);

                this.UTCOffset = NullUTCOffset;
                this.ClockStatus = Null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Time
        /// </summary>
        /// <returns></returns>
        public TimeSpan GetTime()
        {
            try
            {
                TimeSpan dt;
                if (IsTimeConvertible)    //Could be converted 
                    dt = new TimeSpan(0, this.Hour, this.Minute, this.Second, (this.Hundred == Null) ? 0 : this.Hundred * 10);
                else
                {
                    //throw new Exception("Unable to convert stDateTime object into valid TimeSpan");
                    dt = DefaultDateTime.TimeOfDay;
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Set Time
        /// </summary>
        /// <param name="objTime"><see cref="TimeSpan"/></param>
        public void SetTime(TimeSpan objTime)
        {
            try
            {
                try
                {
                    kind = DateTimeType.Time;
                    this.Year = NullYear;
                    this.Month = Null;
                    this.DayOfMonth = Null;
                    this.DayOfWeek = Null;

                    this.Hour = (byte)objTime.Hours;
                    this.Minute = (byte)objTime.Minutes;
                    this.Second = (byte)objTime.Seconds;
                    this.Hundred = (byte)(objTime.Milliseconds / 10);

                    this.UTCOffset = NullUTCOffset;
                    this.ClockStatus = Null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Set user defined date
        /// </summary>
        /// <param name="objDate"></param>
        public void SetDate(DateTime objDate)
        {
            try
            {

                SetDateTime(objDate);

                kind = DateTimeType.Date;
                this.Hour = Null;
                this.Minute = Null;
                this.Second = Null;
                this.Hundred = Null;

                this.UTCOffset = NullUTCOffset;
                this.ClockStatus = Null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get Date of current object
        /// </summary>
        /// <returns></returns>
        public DateTime GetDate()
        {
            try
            {
                DateTime dt = new DateTime();
                if (IsDateConvertible)          //Could be converted safly
                    dt = new DateTime(this.Year, this.Month, this.DayOfMonth);
                else
                {
                    //throw new Exception("Unable to convert stDateTime object into valid DateTime(Date)");
                    dt = DefaultDateTime;
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        
        /// <summary>
        /// Parsing Type 
        /// </summary>
        public enum DateTimeType : byte
        {
            DateTime = 0x01,
            Date,
            Time,
            ShortDateTime
        }

        /// <summary>
        /// Convert the current object into a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            String dateTime = null;
            
            if (Kind == DateTimeType.DateTime)
                dateTime = String.Format("{0}/{1}/{2}/{3} {4}:{5}:{6}:{7} GMT+ 0X{8:X2} CLKS 0X{9:X2}", (Year == NullYear) ? Year.ToString("X4") : Year.ToString("D4"),
                                                                  (Month >= 1 && Month <= 12) ? Month.ToString("D1") : Month.ToString("X2"),
                                                                  (DayOfMonth >= 1 && DayOfMonth <= 31) ? DayOfMonth.ToString("D2") : DayOfMonth.ToString("X2"),
                                                                  (DayOfWeek >= 1 && DayOfWeek <= 7) ? ((DayOfWeek == 7) ? System.DayOfWeek.Sunday.ToString() :
                                                                  ((DayOfWeek)DayOfWeek).ToString()) : DayOfWeek.ToString("X2"),
                                                                  (Hour != Null) ? Hour.ToString("D2") : Hour.ToString("X2"),
                                                                  (Minute != Null) ? Minute.ToString("D2") : Minute.ToString("X2"),
                                                                  (Second != Null) ? Second.ToString("D2") : Second.ToString("X2"),
                                                                  (Hundred != Null) ? Hundred.ToString("D2") : Hundred.ToString("X2"),
                                                                   UTCOffset, ClockStatus);
            else if (Kind == DateTimeType.Date)
                dateTime = String.Format("{0}/{1}/{2}/{3}", (Year == NullYear) ? Year.ToString("X4") : Year.ToString("D4"),
                                                             (Month >= 1 && Month <= 12) ? Month.ToString("D1") : Month.ToString("X2"),
                                                             (DayOfMonth >= 1 && DayOfMonth <= 31) ? DayOfMonth.ToString("D2") : DayOfMonth.ToString("X2"),
                                                             (DayOfWeek >= 1 && DayOfWeek <= 7) ? ((DayOfWeek == 7) ? System.DayOfWeek.Sunday.ToString() :
                                                                  ((DayOfWeek)DayOfWeek).ToString()) : DayOfWeek.ToString("X2"));
            else if (Kind == DateTimeType.Time)
                dateTime = String.Format("{0}:{1}:{2}:{3}", (Hour != Null) ? Hour.ToString("D2") : Hour.ToString("X2"),
                                                            (Minute != Null) ? Minute.ToString("D2") : Minute.ToString("X2"),
                                                            (Second != Null) ? Second.ToString("D2") : Second.ToString("X2"),
                                                            (Hundred != Null) ? Hundred.ToString("D2") : Hundred.ToString("X2"));
            else
                dateTime = String.Format("{0}/{1}/{2}/{3} {4}:{5}:{6}:{7}", (Year == NullYear) ? Year.ToString("X4") : Year.ToString("D4"),
                                                                      (Month >= 1 && Month <= 12) ? Month.ToString("D1") : Month.ToString("X2"),
                                                                      (DayOfMonth >= 1 && DayOfMonth <= 31) ? DayOfMonth.ToString("D2") : DayOfMonth.ToString("X2"),
                                                                      (DayOfWeek >= 1 && DayOfWeek <= 7) ? ((DayOfWeek == 7) ? System.DayOfWeek.Sunday.ToString() :
                                                                  ((DayOfWeek)DayOfWeek).ToString()) : DayOfWeek.ToString("X2"),
                                                                      (Hour != Null) ? Hour.ToString("D2") : Hour.ToString("X2"),
                                                                      (Minute != Null) ? Minute.ToString("D2") : Minute.ToString("X2"),
                                                                      (Second != Null) ? Second.ToString("D2") : Second.ToString("X2"),
                                                                      (Hundred != Null) ? Hundred.ToString("D2") : Hundred.ToString("X2"));
            return dateTime;
        }

        public string ToString(dtpCustomExtensions mvarFormatEx)
        {
            return StDateTimeHelper.ToStringStDateTime(this, mvarFormatEx);
        }

        #region IComparable<StDateTime> Members

        public int CompareTo(StDateTime other)
        {
            try
            {
                bool comparable = true;
                if (other == null)
                    return -1;
                //Don't Compare Two DateTime Objects that never belongs with Same TimeZone
                if (!(this.UTCOffset == NullUTCOffset ||
                    other.UTCOffset == NullUTCOffset) && this.UTCOffset != other.UTCOffset)
                    throw new Exception("Objects are not comparable");
                #region ///Compare Year Part
                int yResult = 0;
                int t_year = this.Year;
                int o_year = other.Year;
                if (!((t_year == NullYear) || (o_year == NullYear)))
                {
                    yResult = t_year.CompareTo(o_year);
                    if (yResult != 0)
                        return yResult;
                }
                #region ///test either comparable
                else if (comparable && (t_year == NullYear && o_year == NullYear))
                    comparable = true;
                else
                    comparable = false;
                #endregion
                #endregion
                #region  ///Compare Month Part
                int mResult = 0;
                int t_month = this.Month;
                int o_month = other.Month;
                if (((o_month >= 1) && (o_month <= 12)) &&
                    ((t_month >= 1) && (t_month <= 12)))
                {
                    mResult = t_month.CompareTo(o_month);      //Compare Explicitly Specified Months
                    if (mResult != 0)
                        return mResult;
                }
                #region ///test either comparable
                else if (comparable && ((t_month == Null && o_month == Null) ||
                    (t_month == DaylightSavingBegin && o_month == DaylightSavingBegin) ||
                    (t_month == DaylightSavingEnd && o_month == DaylightSavingEnd)))
                    comparable = true;
                else
                    comparable = false;
                #endregion
                #endregion
                #region  ///Compare DayOfMonth Part
                int dResult = 0;
                int t_day = this.DayOfMonth;
                int o_day = other.DayOfMonth;
                if (((t_day >= 1) && (t_day <= 31) && (o_day >= 1) && (o_day <= 31)))
                {
                    dResult = t_day.CompareTo(o_day);      //Compare Explicitly Specified Days
                    if (dResult != 0)
                        return dResult;
                }
                #region ///test either comparable
                else if (comparable && ((t_day == StDateTime.LastDayOfMonth && o_day == StDateTime.LastDayOfMonth) ||
                                   (t_day == StDateTime.SecondLastDayOfMonth && o_day == StDateTime.SecondLastDayOfMonth) ||
                                   (t_day == StDateTime.Null && o_day == StDateTime.Null)))
                    comparable = true;
                else
                    comparable = false;
                #endregion
                #endregion
                #region  ///Compare DayOfWeek
                int dayOfWeekResult = 0;
                int t_dayOfWeek = this.DayOfWeek;
                int o_dayOfWeek = other.DayOfWeek;
                if ((t_dayOfWeek >= 1 && t_dayOfWeek <= 7) &&
                    (o_dayOfWeek >= 1 && o_dayOfWeek <= 7))
                {
                    dayOfWeekResult = t_dayOfWeek.CompareTo(o_dayOfWeek);
                }
                #region ///test either comparable
                else if (comparable && (t_dayOfWeek == Null && o_dayOfWeek == Null))
                    comparable = true;
                else
                    comparable = false;
                #endregion
                if (dayOfWeekResult != 0)
                    return dayOfWeekResult;
                #endregion
                #region  ///Compare Hour Part
                int hResult = 0;
                int t_hour = this.Hour;
                int o_hour = other.Hour;
                //Revise Case Ignore Daylight Saving BEGIN//END Month Part
                if (!(t_hour == Null || o_hour == Null))
                    hResult = t_hour.CompareTo(o_hour);
                //Compare Explicitly Specified Days
                #region test case comparable
                else if (comparable &&
                            t_hour == Null &&
                            o_hour == Null)
                    comparable = true;
                else
                    comparable = false;
                #endregion
                if (hResult != 0)
                    return hResult;
                #endregion
                #region  ///Compare Minute Part
                int minResult = 0;
                int t_min = this.Minute;
                int o_min = other.Minute;
                //Compare Explicitly Specified Minutes
                if (!(t_min == Null || o_min == Null))
                    minResult = t_min.CompareTo(o_min);
                #region test case comparable
                else if (comparable &&
                   t_min == Null &&
                   o_min == Null)
                    comparable = true;
                else
                    comparable = false;
                #endregion
                if (minResult != 0)
                    return minResult;
                #endregion
                #region  ///Compare Second Part
                int secResult = 0;
                int t_sec = this.Second;
                int o_sec = other.Second;
                if (!(t_sec == Null || o_sec == Null))
                    secResult = t_sec.CompareTo(o_sec);      //Compare Explicitly Specified Minutes
                #region test case comparable
                else if (comparable &&
                   t_sec == Null &&
                   o_sec == Null)
                    comparable = true;
                else
                    comparable = false;
                #endregion
                if (secResult != 0)
                    return secResult;
                #endregion
                #region  ///Compare Hundredth_Second Part
                int hsecResult = 0;
                int t_Hsec = this.Hundred;
                int o_Hsec = other.Hundred;
                if (!(t_Hsec == Null || o_Hsec == Null))
                    hsecResult = t_Hsec.CompareTo(o_Hsec);      //Compare Explicitly Specified Minutes
                #region test case comparable
                else if (comparable &&
                   t_Hsec == Null &&
                   o_Hsec == Null)
                    comparable = true;
                else
                    comparable = false;
                #endregion
                if (hsecResult != 0)
                    return hsecResult;
                #endregion
                if (!comparable)
                    return -1;
                else
                    return hsecResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            StDateTime clonne = new StDateTime(this);
            return clonne;
        }

        #endregion

        #region ISerializable Members

        protected StDateTime(SerializationInfo info, StreamingContext context)
        {
            //Getting StDateTimeKind Type DateTimeKind
            this.kind = (DateTimeType)info.GetByte("DateTimeKind");
            if (kind == DateTimeType.Date || kind == DateTimeType.DateTime)
            {
                //Getting Year Type Short
                this.Year = info.GetUInt16("Year");
                //Getting Month Type Byte
                this.Month = info.GetByte("Month");
                //Getting DayOfMonth Type Byte
                this.DayOfMonth = info.GetByte("DayOfMonth");
                //Getting DayOfMonth Type Byte
                this.DayOfWeek = info.GetByte("DayOfWeek");

            }
            if (kind == DateTimeType.Time || kind == DateTimeType.DateTime)
            {
                //Getting Hour Type Byte
                this.Hour = info.GetByte("Hour");
                //Getting Minute Type Byte
                this.Minute = info.GetByte("Minute");
                //Getting Second Type Byte
                this.Second = info.GetByte("Second");
                //Getting Hundredth_Second Type Byte
                this.Hundred = info.GetByte("Hundredth_Second");
            }
            if (Kind == DateTimeType.DateTime)
            {
                //Getting UTCOffset Type short
                this.UTCOffset = info.GetInt16("UTCOffset");
                //Getting ClockStatus Type Byte
                this.ClockStatus = info.GetByte("ClockStatus");
            }
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            //Adding StDateTimeKind Type DateTimeKind
            info.AddValue("DateTimeKind", (byte)this.Kind);
            if (kind == DateTimeType.Date || kind == DateTimeType.DateTime)
            {
                //Adding Year Type UShort
                info.AddValue("Year", this.Year);
                //Adding Month Type Byte
                info.AddValue("Month", this.Month);
                //Adding DayOfMonth Type Byte
                info.AddValue("DayOfMonth", this.DayOfMonth);
                //Adding DayOfMonth Type Byte
                info.AddValue("DayOfWeek", this.DayOfWeek);
            }
            if (kind == DateTimeType.Time || kind == DateTimeType.DateTime)
            {
                //Adding Hour Type Byte
                info.AddValue("Hour", this.Hour);
                //Adding Minute Type Byte
                info.AddValue("Minute", this.Minute);
                //Adding Second Type Byte
                info.AddValue("Second", this.Second);
                //Adding Hundredth_Second Type Byte
                info.AddValue("Hundredth_Second", this.Hundred);
            }
            if (Kind == DateTimeType.DateTime)
            {
                //Adding UTCOffset Type short
                info.AddValue("UTCOffset", this.UTCOffset);
                //Adding ClockStatus Type Byte
                info.AddValue("ClockStatus", this.ClockStatus);
            }

        }


        #endregion
    }

    #endregion
}
