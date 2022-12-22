using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS.Comm;

namespace DLMS
{
    /// <summary>
    /// Clock (class_id: 8, version: 0) models the device clock, managing all information related to date and time
    /// including deviations of the local time to a generalized time reference (Coordinated Universal Time, UTC), due to time zones and daylight saving time schemes.
    /// The date information includes the elements year, month, day of month and day of week. 
    /// The time information includes the elements hour, minutes, seconds, hundredths of seconds, and the deviation of the local time from UTC.
    /// The Interface Class also offers various methods to adjust the clock.The daylight saving time function modifies the deviation of local time to UTC depending on the attributes.
    /// </summary>
    public class Class_8 : Base_Class
    {
        #region DataMembers

        /// <summary>
        /// Date_Time_Value COSEM Interface Class 8 Attribute 0x02;meter’s local date and time
        /// </summary>
        /// <remarks>
        /// When this value is set, only specified fields of the date_time are changed.
        /// For example, for setting the date without changing the time, all time-relevant octets of the date_time shall be set to not specified
        /// </remarks>
        public StDateTime Date_Time_Value;
        /// <summary>
        /// Time_Zone COSEM Interface Class 8 Attribute 0x03;The deviation of local, normal time to UTC in minutes.
        /// </summary>
        public Int16 Time_Zone;
        /// <summary>
        /// Status COSEM Interface Class 8 Attribute 0x04;The status is equal to the status read in time.
        /// </summary>
        public byte Status;
        /// <summary>
        /// COSEM Interface Class 8 Attribute 0x05;Defines the local switch date and time when the local time has to be deviated from the normal time.
        /// </summary>
        public StDateTime Daylight_Savings_Begin;
        /// <summary>
        /// COSEM Interface Class 8 Attribute 0x06;Defines the local switch date and time when the local time has to be deviated from the normal time.
        /// </summary>
        public StDateTime Daylight_Savings_End;
        /// <summary>
        /// COSEM Interface Class 8 Attribute 0x07;Contains the number of minutes(byte) by which the deviation in generalized time must be corrected at daylight savings begin.
        /// </summary>
        public sbyte Daylight_Savings_Deviation;
        /// <summary>
        /// COSEM Interface Class 8 Attribute 0x08;True,False determine whether to enable or disable Daylight Saving 
        /// </summary>
        public bool flg_Daylight_Savings_Enabled;
        /// <summary>
        /// <see cref="Clock_Base"/>COSEM Interface Class 8 Attribute 0x09;Defines where the basic timing information comes from 
        /// </summary>
        public Clock_Base Server_Clk_Base;
        /// <summary>
        /// <see cref="StDateTime"/>COSEM Interface Class 8 Method 5 Parameter;Defines where the Preset Time Validity Interval Start Time 
        /// </summary>
        public StDateTime ValidityIntervalStart;
        /// <summary>
        /// <see cref="StDateTime"/>COSEM Interface Class 8 Method 5 Parameter;Defines where the Preset Time Validity Interval End Time
        /// </summary>
        public StDateTime ValidityIntervalEnd;
        /// <summary>
        ///COSEM Interface Class 8 Method 6 Parameter;Defines Seconds to shift time
        /// </summary>
        public short SecondsToShift; // in Seconds

        #endregion

        #region MethodIds

        /// <summary>
        /// Status COSEM Interface Class 8 Method 0x01;Adjusts Meter Clock to the nearesrt Quarter.
        /// </summary>
        public static readonly byte ADJUST_TO_QUARTER = 1;
        /// <summary>
        /// Status COSEM Interface Class 8 Method 0x02;Adjusts Meter Clock to the nearesrt Measuring Period.
        /// </summary>
        public static readonly byte ADJUST_TO_MEASURING_PERIOD = 2;
        /// <summary>
        /// Status COSEM Interface Class 8 Method 0x03;Adjusts Meter Clock to the nearesrt Minute.
        /// </summary>
        public static readonly byte ADJUST_TO_MINUTE = 3;
        /// <summary>
        /// Status COSEM Interface Class 8 Method 0x04;Adjusts Meter Clock to Preset Time.
        /// </summary>
        public static readonly byte ADJUST_TO_PRESET_TIME = 4;
        /// <summary>
        /// Status COSEM Interface Class 8 Method 0x05;Sets Prest Time.
        /// </summary>
        public static readonly byte PRESET_ADJUSTING_TIME = 5;
        /// <summary>
        /// Status COSEM Interface Class 8 Method 0x06;Shifts Time to the specified Seconds.
        /// </summary>
        public static readonly byte SHIFT_TIME = 6;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Index">OBIS code for specific entity</param>
        /// <param name="Obis_Code">Bytes representation of an OBIS code</param>
        /// <param name="No_of_Associations">maximum no of association</param>
        public Class_8(Get_Index Index, byte[] Obis_Code, UInt16 No_of_Associations)
            : base(8, 9, 6, Index, Obis_Code, No_of_Associations)
        {

            // Create array
            //Daylight_Savings_Begin = new byte[12];
            //Daylight_Savings_End = new byte[12];
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Obis_Code">Bytes representation of an OBIS code</param>
        /// <param name="Attribute_recieved">no of attribute associated with current object</param>
        public Class_8(byte[] Obis_Code, byte Attribute_recieved)
            : base(8, 9, 6, Obis_Code)
        {
            DecodingAttribute = Attribute_recieved;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="OBISCodeStruct">OBIS code for a specific Object</param>
        public Class_8(StOBISCode OBISCodeStruct) : base(OBISCodeStruct, 9, 6) { }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="obj">Class_8 Object</param>
        public Class_8(Class_8 obj)
            : base(obj)
        {
            if (obj.Date_Time_Value != null)
                Date_Time_Value = (StDateTime)obj.Date_Time_Value.Clone();
            Time_Zone = obj.Time_Zone;
            Status = obj.Status;
            if (obj.Daylight_Savings_Begin != null)
                Date_Time_Value = (StDateTime)obj.Daylight_Savings_Begin.Clone();
            if (obj.Daylight_Savings_End != null)
                Date_Time_Value = (StDateTime)obj.Daylight_Savings_End.Clone();
            Daylight_Savings_Deviation = obj.Daylight_Savings_Deviation;
            flg_Daylight_Savings_Enabled = obj.flg_Daylight_Savings_Enabled;
            Server_Clk_Base = obj.Server_Clk_Base;
        }

        #endregion

        #region Decoders / Encoders

        /// <summary>
        /// Decode Data of this Class which is received in response of get data Request
        /// </summary>
        /// <param name="Data">Received data from Remote site</param>
        /// <param name="array_traverse">Off-Set</param>
        /// <param name="length">Length to decode</param>
        public override void Decode_Data(ref byte[] Data, ref int array_traverse, int length)
        {
            try
            {
                byte[] Obis_code_recieved = null;
                byte current_char;
                if (DecodingAttribute == 0x00)
                {
                    for (int index = 0; index < AccessResults.Length; index++)
                        AccessResults[index] = DecodingResult.DataNotPresent;
                }
                else if (AccessResults != null && DecodingAttribute <= AccessResults.Length)
                {
                    SetAttributeDecodingResult(DecodingAttribute, DecodingResult.DataNotPresent);
                }
                //------------------------------------------------------
                DecoderAttribute_0(ref Data, ref array_traverse, "Decode_Data_Class_8_Meter_clock");
                DecoderLogicalName(ref Data, ref array_traverse, ref Obis_code_recieved, "Decode_Data_Class_8_Meter_clock");
                //------------------------------------------------------
                #region Attribute 0x02

                if (DecodingAttribute == 0x02 || DecodingAttribute == 0x00)
                {
                    try
                    {
                        Date_Time_Value = new StDateTime();
                        Date_Time_Value.DecodeRawBytes(Data, ref array_traverse);
                        SetAttributeDecodingResult(0x02, DecodingResult.Ready);
                    }
                    catch (Exception ex)
                    {
                        //Check access rights
                        if (!IsAttribReadable(0x02))
                            SetAttributeDecodingResult(0x02, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(0x02, DecodingResult.DecodingError);

                            throw new DLMSDecodingException(String.Format("{0}_{1} element is not (octet-string) decoding Date_Time_Stamp,invalid identifier (Error Code:{2}) ", OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch),
                                "Decode_Data_Class_8_Meter_clock", ex);
                        }
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x03

                if (DecodingAttribute == 0x03 || DecodingAttribute == 0x00)
                {
                    try
                    {
                        Time_Zone = Convert.ToInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, Data.Length));
                        SetAttributeDecodingResult(03, DecodingResult.Ready);
                    }
                    catch (Exception ex)
                    {
                        //Check access rights
                        if (!IsAttribReadable(03))
                            SetAttributeDecodingResult(03, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(03, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} element is not (long)_decoding Time Zone (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch),
                                "Method: Decode_Data_Class_8_Meter_clock", ex);
                        }
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x04

                if (DecodingAttribute == 0x04 || DecodingAttribute == 0x00)
                {
                    try
                    {
                        Status = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, Data.Length));
                        SetAttributeDecodingResult(0x04, DecodingResult.Ready);
                    }
                    catch (Exception ex)
                    {
                        // Check Access Rights
                        if (!IsAttribReadable(0x04))
                            SetAttributeDecodingResult(0x04, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(0x04, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} element is not (unsigned)_decoding Status (Error Code:{2})"
                                , OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch),
                                "Method: Decode_Data_Class_8_Meter_clock", ex);
                        }
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x05

                if (DecodingAttribute == 0x05 || DecodingAttribute == 0x00)
                {
                    try
                    {
                        Daylight_Savings_Begin = new StDateTime();
                        Daylight_Savings_Begin.DecodeRawBytes(Data, ref array_traverse);
                        SetAttributeDecodingResult(0x05, DecodingResult.Ready);
                    }
                    catch (Exception ex)
                    {
                        //Check access rights
                        if (!IsAttribReadable(0x05))
                            SetAttributeDecodingResult(0x05, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(0x05, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} element is not (octet string)_decoding Daylight_Saving_Begin (Error Code:{2})", OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch),
                                "Decode_Data_Class_8_Meter_clock", ex);
                        }
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x06

                if (DecodingAttribute == 0x06 || DecodingAttribute == 0x00)
                {
                    try
                    {
                        Daylight_Savings_End = new StDateTime();
                        Daylight_Savings_End.DecodeRawBytes(Data, ref array_traverse);
                        SetAttributeDecodingResult(0x06, DecodingResult.Ready);
                    }
                    catch (Exception ex)
                    {
                        //Check access rights
                        if (!IsAttribReadable(0x06))
                            SetAttributeDecodingResult(0x06, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(0x06, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} element is not (octet string)_decoding Daylight_Saving_End (Error Code:{2})", OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch),
                                "Decode_Data_Class_8_Meter_clock", ex);
                        }
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x07

                if (DecodingAttribute == 0x07 || DecodingAttribute == 0x00)
                {
                    try
                    {
                        Daylight_Savings_Deviation = Convert.ToSByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data,
                            ref array_traverse, Data.Length));
                        SetAttributeDecodingResult(0x07, DecodingResult.Ready);
                    }
                    catch (Exception ex)
                    {
                        //Check access rights
                        if (!IsAttribReadable(0x07))
                            SetAttributeDecodingResult(0x07, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(0x07, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} element is not (integer)_decoding Daylight_Savings_Deviation (Error Code:{2})", OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch),
                                "Method: Decode_Data_Class_8_Meter_clock", ex);
                        }
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x08

                if (DecodingAttribute == 0x08 || DecodingAttribute == 0x00)
                {
                    try
                    {
                        current_char = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data,
                            ref array_traverse, Data.Length));
                        flg_Daylight_Savings_Enabled = (current_char == 0) ? false : true;
                        SetAttributeDecodingResult(0x08, DecodingResult.Ready);
                    }
                    catch (Exception ex)
                    {
                        // Check Access Rights
                        if (!IsAttribReadable(0x08))
                            SetAttributeDecodingResult(0x08, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(0x08, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} element is not (boolean)_decoding flg_Daylight_Savings_Enabled (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch),
                                "Method: Decode_Data_Class_8_Meter_clock", ex);
                        }
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x09

                if (DecodingAttribute == 0x09 || DecodingAttribute == 0x00)
                {
                    try
                    {
                        current_char = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data,
                            ref array_traverse, Data.Length));
                        Server_Clk_Base = (Clock_Base)current_char;
                        SetAttributeDecodingResult(0x09, DecodingResult.Ready);
                    }
                    catch (Exception ex)
                    {
                        // Check access rights
                        if (!IsAttribReadable(0x09))
                            SetAttributeDecodingResult(0x09, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(0x09, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} element is not (enum)_decoding Server_Clk_Base (Error Code:{2})", OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch),
                                "Method: Decode_Data_Class_8_Meter_clock", ex);
                        }
                    }
                }

                #endregion
                // Data Resize
                // DLMS_Common.Data_Array_resizer(ref Data, array_traverse);
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                    throw ex;
                else
                    throw new DLMSDecodingException(String.Format("{0}_{1} Error occurred while decoding (Error Code:{2})", OBISIndex,
                            OBISIndex.OBISIndex, (int)DLMSErrors.ErrorDecoding_Type), "Decode_Data_Class_8_Meter_Clock", ex);
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
                //------------------------------------------------------
                EncoderAttribute_0();
                EncoderLogicalName();
                //------------------------------------------------------
                #region Attribute 0x02  Date & Time
                if (EncodingAttribute == 0x02 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x02);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode DateTime,No Access (Error Code:{0})",
                            EncodingType, (int)DLMSErrors.Insufficient_Priviledge), "EncodeData_Class_8");
                    }
                    else  // Encode Here Data
                    {
                        byte[] data_array = Date_Time_Value.EncodeRawBytes(StDateTime.DateTimeType.DateTime);
                        // BasicEncodeDecode.Encode_DateTime(Date_Time_Value, ref data_array);
                        // EncodedRaw.Add((byte)DataTypes._A09_octet_string);
                        // EncodedRaw.Add((byte)data_array.Length);
                        EncodedRaw.AddRange(data_array);
                    }
                }
                //------------------------------------------------------
                #endregion
                #region Attribute 0x03  Time Zone

                if (EncodingAttribute == 0x03 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x03);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode Time Zone,No Access (Error Code:{0})",
                            EncodingType, (int)DLMSErrors.Insufficient_Priviledge), "EncodeData_Class_8");
                    }
                    else                                //Encode Here Data
                    {
                        byte[] data_array = null;
                        data_array = BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A10_long, Time_Zone);
                        EncodedRaw.AddRange(data_array);
                    }
                }

                //------------------------------------------------------
                #endregion
                #region Attribute 0x04  Status

                if (EncodingAttribute == 0x04 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x04);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode Status,No Access (Error Code:{0})",
                            EncodingType, (int)DLMSErrors.Insufficient_Priviledge), "EncodeData_Class_8");
                    }
                    else                                //Encode Here Data
                    {
                        byte[] data_array = null;
                        data_array = BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A11_unsigned, Status);
                        EncodedRaw.AddRange(data_array);
                    }
                }

                //------------------------------------------------------
                #endregion
                #region Attribute 0x05  Daylight Saving Begin

                if (EncodingAttribute == 0x05 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x05);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode Daylight Saving Begin,No Access (Error Code:{0})",
                            EncodingType, (int)DLMSErrors.Insufficient_Priviledge), "EncodeData_Class_8");
                    }
                    else   // Encode Here Data
                    {
                        //BasicEncodeDecode.Encode_DateTime(Daylight_Savings_Begin, ref data_array);
                        byte[] data_array = Daylight_Savings_Begin.EncodeRawBytes(StDateTime.DateTimeType.DateTime);
                        // EncodedRaw.Add((byte)DataTypes._A09_octet_string);
                        // EncodedRaw.Add((byte)data_array.Length);
                        EncodedRaw.AddRange(data_array);
                    }
                }

                //------------------------------------------------------
                #endregion
                #region Attribute 0x06  Daylight Saving End

                if (EncodingAttribute == 0x06 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x06);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode Daylight Saving End,No Access (Error Code:{0})",
                            EncodingType, (int)DLMSErrors.Insufficient_Priviledge), "EncodeData_Class_8");
                    }
                    else            // Encode Here Data
                    {
                        // byte[] data_array = null;
                        // BasicEncodeDecode.Encode_DateTime(Daylight_Savings_End, ref data_array);
                        // EncodedRaw.Add((byte)DataTypes._A09_octet_string);
                        // EncodedRaw.Add((byte)data_array.Length);

                        byte[] data_array = Daylight_Savings_End.EncodeRawBytes(StDateTime.DateTimeType.DateTime);
                        EncodedRaw.AddRange(data_array);
                    }
                }

                //------------------------------------------------------
                #endregion
                #region Attribute 0x07  Daylight Saving Deviation

                if (EncodingAttribute == 0x08 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x07);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode Daylight Saving Deviation,No Access (Error Code:{0})",
                            EncodingType, (int)DLMSErrors.Insufficient_Priviledge), "EncodeData_Class_8");
                    }
                    else   // Encode Here Data
                    {
                        byte[] data_array = null;
                        data_array = BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A0F_integer, this.Daylight_Savings_Deviation);
                        EncodedRaw.AddRange(data_array);
                    }
                }

                //------------------------------------------------------
                #endregion
                #region Attribute 0x08  Flg_DaylightSavingEnable

                if (EncodingAttribute == 0x08 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x08);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode FlagDaylightSavingEnable,No Access (Error Code:{0})",
                            EncodingType, (int)DLMSErrors.Insufficient_Priviledge), "EncodeData_Class_8");
                    }
                    else                                // Encode Here Data
                    {
                        byte[] data_array = null;
                        data_array = BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A03_boolean, this.flg_Daylight_Savings_Enabled);
                        EncodedRaw.AddRange(data_array);
                    }
                }

                //------------------------------------------------------
                #endregion
                #region Attribute 0x09  Device Clock Base

                if (EncodingAttribute == 0x09 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x09);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode ServerClockCrystalBase,No Access (Error Code:{0})",
                            EncodingType, (int)DLMSErrors.Insufficient_Priviledge), "EncodeData_Class_8");
                    }
                    else                                // Encode Here Data
                    {
                        byte[] data_array = null;
                        data_array = BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A03_boolean, this.Server_Clk_Base);
                        EncodedRaw.AddRange(data_array);
                    }
                }

                ///------------------------------------------------------
                #endregion
                byte[] dtBuf = EncodedRaw.ToArray<byte>();
                EncodedRaw = null;
                return dtBuf;
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                {
                    throw ex;
                }
                else
                    throw new DLMSException(String.Format("{0}_{1} (Error Code:{2})", "Error occurred while encoding data",
                        "Encode_Data_Class_8", (int)DLMSErrors.ErrorEncoding_Type), ex);
            }
        }
        /// <summary>
        /// Method Parameter Encoder
        /// </summary>
        /// <returns>byte[]</returns>
        public override byte[] Encode_Parameters()
        {
            EncodedRaw = new List<byte>(0x0A);
            try
            {

                #region Integer Data 0

                if (
                    base.MethodInvokeId == ADJUST_TO_QUARTER ||
                    base.MethodInvokeId == ADJUST_TO_MEASURING_PERIOD ||
                    base.MethodInvokeId == ADJUST_TO_MINUTE ||
                    base.MethodInvokeId == ADJUST_TO_PRESET_TIME
                    )
                {
                    EncodedRaw.Add((byte)DataTypes._A0F_integer);
                    EncodedRaw.Add(0);
                }

                #endregion

                #region Preset Adjusting Time

                else if (base.MethodInvokeId == PRESET_ADJUSTING_TIME)
                {
                    EncodedRaw.Add((byte)DataTypes._A02_structure);
                    EncodedRaw.Add(3);


                    byte[] EncodedDateTime = null;
                    EncodedDateTime = Date_Time_Value.EncodeRawBytes(StDateTime.DateTimeType.DateTime);
                    EncodedRaw.AddRange(EncodedDateTime);

                    EncodedDateTime = ValidityIntervalStart.EncodeRawBytes(StDateTime.DateTimeType.DateTime);
                    EncodedRaw.AddRange(EncodedDateTime);

                    EncodedDateTime = ValidityIntervalEnd.EncodeRawBytes(StDateTime.DateTimeType.DateTime);
                    EncodedRaw.AddRange(EncodedDateTime);

                }

                #endregion

                #region Shift Time
                else if (base.MethodInvokeId == SHIFT_TIME)
                {
                    byte[] data_array = null;
                    data_array = BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A10_long, SecondsToShift);
                    EncodedRaw.AddRange(data_array);
                }
                #endregion
                else
                    EncodedRaw.Add((byte)DataTypes._A00_Null);

                return EncodedRaw.ToArray();
            }
            catch (Exception ex)
            {
                if (ex is DLMSEncodingException)
                {
                    throw ex;
                }
                else
                {
                    throw new DLMSEncodingException(String.Format("Error occurred while Encode_Parameters (Error Code:{0})",
                                                   (int)DLMSErrors.ErrorEncoding_Type), "Encode_Parameters_Class8");
                }
            }
        }

        #endregion

        #region Member Methods

        public override object Clone()
        {
            Class_8 cloned = new Class_8(this);
            return cloned;
        }

        /// <summary>
        /// Returns the String Representation of the Class_8 (clock) object
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            String baseStr = base.ToString();
            StringBuilder strVal = new StringBuilder();
            String time = "", daylightSavBegin = "", daylightSavEnd = "";         // Date Time Values

            // Compute The DateTime String If Data Available
            if (this.Date_Time_Value != null && GetAttributeDecodingResult(2) == DecodingResult.Ready)
                time = Date_Time_Value.ToString();
            if (this.Daylight_Savings_Begin != null && GetAttributeDecodingResult(5) == DecodingResult.Ready)
                daylightSavBegin = Daylight_Savings_Begin.ToString();
            if (this.Daylight_Savings_End != null && GetAttributeDecodingResult(6) == DecodingResult.Ready)
                daylightSavEnd = Daylight_Savings_End.ToString();

            strVal.AppendFormat(",Time:{0}:{1}", time, GetAttributeDecodingResult(2));
            strVal.AppendFormat(",Time Zone:{0}:{1}", this.Time_Zone, GetAttributeDecodingResult(3));
            strVal.AppendFormat(",Status:{0}:{1}", this.Status, GetAttributeDecodingResult(4));
            strVal.AppendFormat(",DayLight Saving Start:{0}:{1}", daylightSavBegin, GetAttributeDecodingResult(5));
            strVal.AppendFormat(",DayLight Saving End:{0}:{1}", daylightSavEnd, GetAttributeDecodingResult(6));
            strVal.AppendFormat(",DayLight Saving Deviation:{0}:{1}", this.Daylight_Savings_Deviation, GetAttributeDecodingResult(7));
            strVal.AppendFormat(",DayLight Saving Enabled:{0}:{1}", this.flg_Daylight_Savings_Enabled, GetAttributeDecodingResult(8));
            strVal.AppendFormat(",Server Clock Base:{0}:{1}", this.Server_Clk_Base, GetAttributeDecodingResult(9));

            return baseStr + strVal;
        }

        #endregion

    }
}
