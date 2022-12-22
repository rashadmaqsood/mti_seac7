using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS.Comm;

namespace DLMS
{
    /// <summary>
    /// Demand register (class_id: 5, version: 0) allows modeling a demand value, with its associated scaler, unit, status and time information.
    /// A Demand register object measures and computes a current_average_value periodically, and it stores a last_average_value.
    /// The time interval T over which the demand is measured or computed is defined by specifying number_of_periods and period.
    /// Class_5 object is identified by the Index Property <see cref="StOBISCode"/>(logical_name)
    ///</summary>
    public class Class_5 : Base_Class
    {
        #region Data_Members

        /// <summary>
        /// Current Average Value COSEM Interface Class 5 Attribute 0x2;Specify Demand Register Current Average Numerical Value
        /// </summary>
        public ValueType CValue;
        /// <summary>
        /// Current Average Value Array COSEM Interface Class 5 Attribute 0x2;Specify Demand Register Current Average Binary Value
        /// </summary>
        public byte[] CValue_Array;

        /// <summary>
        /// Last Average Value COSEM Interface Class 5 Attribute 0x03;Specify Demand Register Last Average Numerical Value
        /// </summary>
        public ValueType LValue;
        /// <summary>
        /// Last Average Value Array COSEM Interface Class 5 Attribute 0x03;Demand Register Last Average binary Value
        /// </summary>
        public byte[] LValue_Array;

        /// <summary>
        /// <see cref="DLMS.Comm.Unit_Scaler"/> COSEM Interface Class 5 Attribute 0x4;Unit is an Enumeration which is the exponent (to the base of 10) of the multiplication factor
        /// </summary>
        public units Unit;
        /// <summary>
        /// <see cref="DLMS.Comm.Unit_Scaler"/> COSEM Interface Class 5 Attribute 0x04;Specify Demand Register Scaler
        /// </summary>
        public sbyte scaler;

        /// <summary>
        /// Last Average Value CaptureTime COSEM Interface Class 5 Attribute 0x06;Specify Demands Register Last Average Value Capture time
        /// </summary>
        public StDateTime capture_time;
        /// <summary>
        /// Last Average Value CaptureTime COSEM Interface Class 5 Attribute 0x07;Specify Demands Register Current Average Value Capture time
        /// </summary>
        public StDateTime start_time_Current;

        /// <summary>
        /// COSEM Interface Class 5 Attribute 0x08;Specify the MDI Period Value
        /// </summary>
        public uint period;
        /// <summary>
        ///COSEM Interface Class 5 Attribute 0x09;Specify the MDI Sliding Window Count.Value must be greater than zero.
        /// </summary>
        public ushort periodCount;
        
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Index">OBIS code for specific entity</param>
        /// <param name="Obis_Code">Bytes representation of an OBIS code</param>
        /// <param name="No_of_Associations">maximum no of association</param>
        public Class_5(Get_Index Index, byte[] Obis_Code, UInt16 No_of_Associations)
            : base(5, 9, 2, Index, Obis_Code, No_of_Associations)
        {
            this.periodCount = 1;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Obis_Code">Bytes representation of an OBIS code</param>
        /// <param name="Attribute_recieved">no of attribute associated with current object</param>
        public Class_5(byte[] Obis_Code, byte Attribute_recieved)
            : base(5, 9, 2, Obis_Code)
        {
            DecodingAttribute = Attribute_recieved;
            this.periodCount = 1;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="OBISCodeStruct">OBIS code for a specific Object</param>
        public Class_5(StOBISCode OBISCodeStruct)
            : base(OBISCodeStruct, 9, 2)
        {
            this.periodCount = 1;
        }
        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="obj">Class_5 Object</param>
        public Class_5(Class_5 obj)
            : base(obj)
        {

        }

        #endregion

        #region Decoder/Encoder

        /// <summary>
        /// Decode Data of this Class which is received in response of get data Request
        /// </summary>
        /// <param name="Data">Received data from Remote site</param>
        /// <param name="array_traverse">Off-Set</param>
        /// <param name="length">Length to decode</param>
        public override void Decode_Data(ref byte[] Data, ref int array_traverse, int length)
        {
            byte[] Obis_code_recieved = null;
            byte current_char;
            //------------------------------------------------------
            try
            {
                //SET All Attribute Access Status Results
                if (DecodingAttribute == 0x00)
                {
                    for (int index = 0; index < AccessResults.Length; index++)
                        AccessResults[index] = DecodingResult.DataNotPresent;
                }
                else if (AccessResults != null && DecodingAttribute <= AccessResults.Length)
                {
                    SetAttributeDecodingResult(DecodingAttribute, DecodingResult.DataNotPresent);
                }
                DecoderAttribute_0(ref Data, ref array_traverse, "Decode_Data_Demand_Register_Class_5");
                DecoderLogicalName(ref Data, ref array_traverse, ref Obis_code_recieved, "Decode_Data_Demand_Register_Class_5");
                #region Attribute 0x02 Current_AVG_Value
                
                if (DecodingAttribute == 0x02 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        if (!IsAttribReadable(02))
                            SetAttributeDecodingResult(02, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(02, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} Unable to decode Current Average Value,Invalid identifier of value (Error Code:{2})"
                            , OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_5");
                        }
                    }
                    else
                    {
                        DataTypes Recieved_Obj_Type = (DataTypes)current_char;
                        switch (Recieved_Obj_Type)
                        {
                            case DataTypes._A14_long_64:
                            case DataTypes._A15_long_64_unsigned:
                            case DataTypes._A05_double_long:
                            case DataTypes._A06_double_long_unsigned:
                            case DataTypes._A10_long:
                            case DataTypes._A12_long_unsigned:
                            case DataTypes._A11_unsigned:
                            case DataTypes._A0D_bcd:
                            case DataTypes._A0F_integer:
                            case DataTypes._A16_enum:
                                {
                                    array_traverse--;
                                    // save value
                                    CValue = BasicEncodeDecode.Intelligent_Data_Decoder(
                                            ref Data, ref array_traverse, Data.Length);
                                    break;
                                }
                            case DataTypes._A09_octet_string:
                            case DataTypes._A0A_visible_string:
                                {
                                    array_traverse--;
                                    CValue_Array = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse, Data.Length);
                                    break;
                                }
                            default:
                                throw new DLMSDecodingException(String.Format("{0}_{1} unable to decode Current Average Value,{2} data type not included yet (Error Code:{3})",
                                OBISIndex, OBISIndex.OBISIndex, Recieved_Obj_Type, (int)DLMSErrors.Invalid_TypeNotIncluded), "Decode_Data_Class_5");

                        }
                        SetAttributeDecodingResult(2, DecodingResult.Ready);
                    }
                }

                #endregion
                #region Attribute 0x03 Last_AVG_Value
                
                if (DecodingAttribute == 0x03 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        if (!IsAttribReadable(0x03))
                            SetAttributeDecodingResult(0x03, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(0x03, DecodingResult.DecodingError);                            
                            throw new DLMSDecodingException(String.Format("{0}_{1} unable to decode Last Average Value,invalid identifier (Error Code:{2})", OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_5");
                        }
                    }
                    else
                    {
                        DataTypes Recieved_Obj_Type = (DataTypes)current_char;
                        switch (Recieved_Obj_Type)
                        {
                            case DataTypes._A14_long_64:
                            case DataTypes._A15_long_64_unsigned:
                            case DataTypes._A05_double_long:
                            case DataTypes._A06_double_long_unsigned:
                            case DataTypes._A10_long:
                            case DataTypes._A12_long_unsigned:
                            case DataTypes._A11_unsigned:
                            case DataTypes._A0D_bcd:
                            case DataTypes._A0F_integer:
                            case DataTypes._A16_enum:
                                {
                                    array_traverse--;
                                    // save value
                                    LValue = BasicEncodeDecode.Intelligent_Data_Decoder(
                                            ref Data, ref array_traverse, length);
                                    break;
                                }
                            case DataTypes._A09_octet_string:
                            case DataTypes._A0A_visible_string:
                                {
                                    array_traverse--;
                                    LValue_Array = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse, Data.Length);
                                    break;
                                }
                            default:
                                throw new DLMSDecodingException(String.Format("{0}_{1} unable to decode Last Average Value,{2} data type not included yet (Error Code:{3})",
                               OBISIndex, OBISIndex.OBISIndex, Recieved_Obj_Type, (int)DLMSErrors.Invalid_TypeNotIncluded), "Decode_Data_Class_5");

                        }
                        SetAttributeDecodingResult(3, DecodingResult.Ready);
                    }
                }

                #endregion
                #region Attribute 0x04
                
                if (DecodingAttribute == 0x04 || DecodingAttribute == 0x00)
                {
                    try
                    {
                        current_char = Data[array_traverse++];
                        if (current_char != (byte)DataTypes._A02_structure || 
                            Data[array_traverse++] != 0x02)
                        {
                            /// Generate Error
                            SetAttributeDecodingResult(0x04, DecodingResult.DecodingError);

                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier of scaler_unit structure (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_5_Object");
                        }
                        ValueType vl = BasicEncodeDecode.Intelligent_Data_Decoder(ref Data,
                        ref array_traverse, Data.Length);
                        byte _scaler = Convert.ToByte(vl);
                        unchecked
                        {
                            this.scaler = (sbyte)_scaler;
                        }
                        Unit = (units)Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data,
                        ref array_traverse, Data.Length));
                        SetAttributeDecodingResult(0x04, DecodingResult.Ready);
                    }
                    catch (Exception ex)
                    {
                        if (!IsAttribReadable(0x04))
                        {
                            SetAttributeDecodingResult(0x04, DecodingResult.NoAccess);
                        }
                        else
                        {
                            SetAttributeDecodingResult(0x04, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier of Scaler_Unit structure (Error Code:{2})",
                                 OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_5_Object", ex);
                        }
                    }
                }

                #endregion
                #region Attribute 0x05 Status

                if (DecodingAttribute == 0x05 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        if (!IsAttribReadable(0x05))
                            SetAttributeDecodingResult(0x05, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(0x05, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} unable to decode status,invalid identifier (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_5_Object");
                        }
                    }
                    else
                    {
                        SetAttributeDecodingResult(0x05, DecodingResult.DecodingError);
                        throw new DLMSEncodingException(String.Format("Unable to decode MDI Status,decoder Not Implemented Yet (Error Code:{0})",
                            (int)DLMSErrors.Invalid_DecoderNotIncluded), "Decode_Data_Demand_Register_Class_5");
                    }
                }

                #endregion
                #region Attribute 0x06 CaptureTime

                if (DecodingAttribute == 0x06 || DecodingAttribute == 0x00)
                {
                    try
                    {
                        this.capture_time = new StDateTime();
                        this.capture_time.DecodeRawBytes(Data, ref array_traverse);
                        SetAttributeDecodingResult(0x06, DecodingResult.Ready);
                    }
                    catch (Exception ex)
                    {
                        // Check Access Rights
                        if (!IsAttribReadable(0x06))
                            SetAttributeDecodingResult(0x06, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(0x06, DecodingResult.DecodingError);

                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier of 09 (octet string)_decoding Capture_Time_Stamp (Error Code:{2})", OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_5_Object", ex);
                        }
                    }
                }

                #endregion
                #region Attribute 0x07 Start_Current_Time

                if (DecodingAttribute == 0x07 || DecodingAttribute == 0x00)
                {
                    try
                    {
                        this.start_time_Current = new StDateTime();
                        capture_time.DecodeRawBytes(Data, ref array_traverse);
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
                            
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier of 09 (octet string)_decoding Start_Time_Stamp (Error Code:{2})", OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch),
                                "Decode_Data_Class_5_Object", ex);
                        }
                    }
                }

                #endregion
                #region Attribute 0x08 Period

                if (DecodingAttribute == 0x08 || DecodingAttribute == 0x00)
                {
                    try
                    {
                        // Decode Period << DataTypes._A06_double_long_unsigned>>
                        this.period = Convert.ToUInt32(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, Data.Length));
                        SetAttributeDecodingResult(0x08, DecodingResult.Ready);
                    }
                    catch (Exception ex)
                    {
                        // Check access rights
                        if (!IsAttribReadable(0x08))
                            SetAttributeDecodingResult(0x08, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(0x08, DecodingResult.DecodingError);
                            
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier decoding Capture Period (Error Code:{2})", OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch),
                               "Decode_Data_Class_5_Object", ex);
                        }
                    }
                }

                #endregion
                #region Attribute 0x09 PeriodCount
                
                if (DecodingAttribute == 0x09 || DecodingAttribute == 0x00)
                {
                    try
                    {
                        // Decode Period < DataTypes._A06_long_unsigned>
                        this.periodCount = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, Data.Length));
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
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier decoding Period Count (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch),
                               "Decode_Data_Class_5_Object", ex);
                        }
                    }
                }

                #endregion
                // DLMS_Common.Data_Array_resizer(ref Data, array_traverse);
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                {
                    throw ex;
                }
                else
                    throw new DLMSDecodingException(String.Format("Error occurred while decoding data (Error Code:{0})",
                        OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.ErrorDecoding_Type), "Decode_Data_Class_5_Object", ex);
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
                #region Attribute 0x02 Current_AVG_Value

                if (EncodingAttribute == 0x02 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x02);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode Current_AVG_Value,No Access (Error Code:{0})",
                             (int)DLMSErrors.Insufficient_Priviledge), "Encode_Data_Demand_Register_Class_5");
                    }
                    // Encode Here Data
                    else if (EncodingAttribute == 0x02)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode Current_AVG_Value,Encoder Not Implemented Yet (Error Code:{2})",
                            EncodingType, (int)DLMSErrors.Invalid_EncoderNotIncluded), "Encode_Data_Demand_Register_Class_5");
                    }
                }

                #endregion
                //------------------------------------------------------ 
                #region Attribute 0x03 Last_AVG_Value
                
                if (EncodingAttribute == 0x03 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x03);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode Last_AVG_Value,No Access (Error Code:{2})",
                            EncodingType, (int)DLMSErrors.Insufficient_Priviledge), "Encode_Data_Demand_Register_Class_5");
                    }
                    //Encode Here Data
                    else if (EncodingAttribute == 0x03)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode Last_AVG_Value,Encoder Not Implemented Yet (Error Code:{2})",
                            EncodingType, (int)DLMSErrors.Invalid_EncoderNotIncluded), "Encode_Data_Demand_Register_Class_5");
                    }
                }

                #endregion
                //------------------------------------------------------ 
                #region Attribute 0x04 Scaler_Unit

                if (EncodingAttribute == 0x03 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x04);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode Scaler_Unit,No Access (Error Code:{2})",
                            EncodingType, (int)DLMSErrors.Insufficient_Priviledge), "Encode_Data_Demand_Register_Class_5");
                    }
                    // Encode Here Data
                    else if (EncodingAttribute == 0x04)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode Scaler_Unit,Encoder Not Implemented Yet (Error Code:{2})",
                            EncodingType, (int)DLMSErrors.Invalid_EncoderNotIncluded), "Encode_Data_Demand_Register_Class_5");
                    }
                }

                #endregion
                //------------------------------------------------------ 
                #region Attribute 0x05 Status

                if (EncodingAttribute == 0x05 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x05);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode Status,No Access (Error Code:{2})",
                            EncodingType, (int)DLMSErrors.Insufficient_Priviledge), "Encode_Data_Demand_Register_Class_5");
                    }
                    // Encode Here Data
                    else if (EncodingAttribute == 0x05)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode Status,Encoder Not Implemented Yet (Error Code:{2})",
                            EncodingType, (int)DLMSErrors.Invalid_EncoderNotIncluded), "Encode_Data_Demand_Register_Class_5");
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x06 CaptureTime

                if (EncodingAttribute == 0x06 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x06);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode CaptureTime,No Access (Error Code:{2})",
                            EncodingType, (int)DLMSErrors.Insufficient_Priviledge), "Encode_Data_Demand_Register_Class_5");
                    }
                    //Encode Here Data
                    else if (EncodingAttribute == 0x06)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode CaptureTime,Encoder Not Implemented Yet (Error Code:{2})",
                            EncodingType, (int)DLMSErrors.Invalid_EncoderNotIncluded), "Encode_Data_Demand_Register_Class_5");
                    }
                }

                #endregion
                //------------------------------------------------------ 
                #region Attribute 0x07 Start_Current_Time

                if (EncodingAttribute == 0x07 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x07);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode Start_Current_Time,No Access (Error Code:{2})",
                            EncodingType, (int)DLMSErrors.Insufficient_Priviledge), "Encode_Data_Demand_Register_Class_5");
                    }
                    // Encode Here Data
                    else if (EncodingAttribute == 0x07)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode Start_Current_Time,Encoder Not Implemented Yet (Error Code:{2})",
                            EncodingType, (int)DLMSErrors.Invalid_EncoderNotIncluded), "Encode_Data_Demand_Register_Class_5");
                    }
                }

                #endregion
                //------------------------------------------------------ 
                #region Attribute 0x08 Period

                if (EncodingAttribute == 0x08 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x08);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode Period,No Access (Error Code:{2})",
                            EncodingType, (int)DLMSErrors.Insufficient_Priviledge), "Encode_Data_Demand_Register_Class_5");
                    }
                    //Encode Here Data
                    else if (EncodingAttribute == 0x08)
                    {
                        //Encoder Period << DataTypes._A06_double_long_unsigned>>
                        EncodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A06_double_long_unsigned, this.period));
                    }
                }

                #endregion
                //------------------------------------------------------ 
                #region Attribute 0x09 PeriodCount

                if (EncodingAttribute == 0x09 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x09);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode PeriodCount,No Access (Error Code:{2})",
                            EncodingType, (int)DLMSErrors.Insufficient_Priviledge), "Encode_Data_Demand_Register_Class_5");

                    }
                    //Encode Here Data
                    else if (EncodingAttribute == 0x09)
                    {
                        //Encoder Period Count<< DataTypes._A06_long_unsigned>>
                        EncodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, this.periodCount));
                    }
                }

                #endregion
                //------------------------------------------------------ 
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
                    throw new DLMSEncodingException(String.Format("Error occurred while encoding data (Error Code:{0})",
                                               OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.ErrorEncoding_Type), "Encode_Data_Class_5_Object", ex);
            }
        }
        #endregion

        #region Member Methods
        
        public override object Clone()
        {
            Class_5 cloned = new Class_5(this);
            return cloned;
        }

        /// <summary>
        /// Returns the String representation of the Class_4 object
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            try
            {
                string baseStr = base.ToString();
                
                string time_str = "NIL";
                if (capture_time != null && GetAttributeDecodingResult(06) == DecodingResult.Ready)
                    time_str = capture_time.ToString();
                StringBuilder strVal = new StringBuilder();
                strVal.AppendFormat(",Request Current Avg Val:{0:f3}:{1}", CValue, GetAttributeDecodingResult(2));
                strVal.AppendFormat(",Request Last Avg Val:{0:f3}:{1}", LValue, GetAttributeDecodingResult(3));
                strVal.AppendFormat(",Scaler_Unit:{0}_{1}:{2}", this.scaler, this.Unit, GetAttributeDecodingResult(4));
                ///Missing Attributes 4 Status
                strVal.AppendFormat(",Capture_time:{0}:{1}", time_str, GetAttributeDecodingResult(6));

                return baseStr + strVal.ToString();
            }
            catch (Exception ex)
            {
                return "Error Str" + ex.Message;
            }
        }

        #endregion
    }
}
