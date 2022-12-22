using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS.Comm;

namespace DLMS
{
    /// <summary>
    /// Register Class (class_id: 3, version: 0) allows modeling a process or a status value with its associated scaler and unit.
    /// Register objects know the nature of the process or status value. It is identified by the attribute Index <see cref="StOBISCode"/>(logical_name)
    /// </summary>
    public class Class_3 : Base_Class
    {
        /// <summary>
        /// Value COSEM Interface Class 3 Attribute 0x02;Value Property To hold any ValueType data(value as choice)<see cref="DLMS.Comm.DataTypes"/>  
        /// </summary>
        public ValueType Value;
        /// <summary>
        /// Value COSEM Interface Class 3 Attribute 0x02;ValueArray Property To hold any byte Array data(value as choice)<see cref="DLMS.Comm.DataTypes"/>  
        /// </summary>
        public byte[] Value_Array;

        /// <summary>
        /// Value COSEM Interface Class 3 Attribute 0x02;Value_Obj Property To hold any type of Object (value as choice)<see cref="DLMS.Comm.DataTypes"/> 
        /// </summary>
        public Object Value_Obj;
        

        /// <summary>
        /// <see cref="DLMS.Comm.Unit_Scaler"/> COSEM Interface Class 3 Attribute 0x03;Unit is an Enumeration which is the exponent (to the base of 10) of the multiplication factor.
        /// </summary>
        public units Unit;
        /// <summary>
        /// <see cref="DLMS.Comm.Unit_Scaler"/> COSEM Interface Class 3 Attribute 0x03;Scaler value that would later composite with Unit
        /// </summary>
        public sbyte scaler;

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Index">OBIS code for specific entity</param>
        /// <param name="Obis_Code">Bytes representation of an OBIS code</param>
        /// <param name="No_of_Associations">maximum no of association</param>
        public Class_3(Get_Index Index, byte[] Obis_Code, UInt16 No_of_Associations)
            : base(3, 3, 1, Index, Obis_Code, No_of_Associations)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Obis_Code">Bytes representation of an OBIS code</param>
        /// <param name="Attribute_recieved">no of attribute associated with current object</param>
        public Class_3(byte[] Obis_Code, byte Attribute_recieved)
            : base(3, 3, 1, Obis_Code)
        {
            DecodingAttribute = Attribute_recieved;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="OBISCode">StOBISCode for a specific Object</param>
        public Class_3(StOBISCode OBISCode) : base(OBISCode, 3, 1) { }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="obj">Class_3 Object</param>
        public Class_3(Class_3 obj)
            : base(obj)
        {
            Value = obj.Value;
            Value_Array = obj.Value_Array;
            scaler = obj.scaler;
            Unit = obj.Unit;
        }

        #endregion

        #region Decoder / Encoder

        /// <summary>
        /// Decode Data of this Class which is received in response of get data Request
        /// </summary>
        /// <param name="Data">Received data from Remote site</param>
        /// <param name="array_traverse">Off-Set</param>
        /// <param name="length">Length to decode</param>
        public override void Decode_Data(ref byte[] Data, ref int array_traverse, int length)
        {
            //---------------------------------------------
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
            try
            {
                DecoderAttribute_0(ref Data, ref array_traverse, "Decode_Data_Class_3_Object");
                DecoderLogicalName(ref Data, ref array_traverse, ref Obis_code_recieved, "Decode_Data_Class_3_Object");
                //------------------------------------------------------
                #region Attribute 0x02

                if (DecodingAttribute == 0x02 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // Null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check Access Rights
                        if (!IsAttribReadable(0x02))
                            SetAttributeDecodingResult(0x02, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(0x02, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0} invalid identifier of value (Error Code:{1})",
                               this.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch),
                               "Decode_Data_Class_3");
                        }
                    }
                    else
                    {
                        DataTypes Recieved_Obj_Type = (DataTypes)current_char;
                        DecodingType = Recieved_Obj_Type;

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
                            case DataTypes._A03_boolean:
                                {
                                    array_traverse--;
                                    // save value
                                    this.Value = BasicEncodeDecode.Intelligent_Data_Decoder(
                                            ref Data, ref array_traverse, Data.Length);
                                    break;
                                }
                            case DataTypes._A09_octet_string:
                            case DataTypes._A0A_visible_string:
                                {
                                    array_traverse--;
                                    Value_Array = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse, Data.Length);
                                    break;
                                }
                            #region date_time

                            case DataTypes._A19_datetime:
                            case DataTypes._A1A_date:
                            case DataTypes._A1B_time:
                                {
                                    DecodingType = Recieved_Obj_Type;
                                    byte[] octSTRData = null;

                                    StDateTime StdateTime = new StDateTime();

                                    // Decode DateTime
                                    if (Recieved_Obj_Type == DataTypes._A19_datetime)
                                    {
                                        octSTRData = new byte[12];
                                        Buffer.BlockCopy(Data, array_traverse, octSTRData, 0, octSTRData.Length);
                                        array_traverse += octSTRData.Length;

                                        StdateTime.DecodeDateTime(octSTRData);
                                    }
                                    // Decode Date
                                    else if (Recieved_Obj_Type == DataTypes._A1A_date)
                                    {
                                        octSTRData = new byte[05];
                                        Buffer.BlockCopy(Data, array_traverse, octSTRData, 0, octSTRData.Length);
                                        array_traverse += octSTRData.Length;

                                        StdateTime.DecodeDate(octSTRData);
                                    }
                                    // Decode Time
                                    else if (Recieved_Obj_Type == DataTypes._A1B_time)
                                    {
                                        octSTRData = new byte[04];
                                        Buffer.BlockCopy(Data, array_traverse, octSTRData, 0, octSTRData.Length);
                                        array_traverse += octSTRData.Length;

                                        StdateTime.DecodeTime(octSTRData);
                                    }

                                    Value_Obj = StdateTime;
                                    break;
                                }

                            #endregion
                            default:
                                throw new DLMSDecodingException(String.Format("{0} data type {1} not included yet (Error Code:{2})",
                                    this.OBISIndex, Recieved_Obj_Type, (int)DLMSErrors.Invalid_TypeNotIncluded),
                                    "Decode_Data_Class_3");

                        }
                        SetAttributeDecodingResult(2, DecodingResult.Ready);
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x03

                if (DecodingAttribute == 0x03 || DecodingAttribute == 0x00)
                {
                    try
                    {
                        current_char = Data[array_traverse++];
                        // null Data
                        if (current_char == (byte)DataTypes._A00_Null)
                        {
                            // Check Access Rights
                            if (!IsAttribReadable(0x03))
                                SetAttributeDecodingResult(0x03, DecodingResult.NoAccess);
                            else
                            {
                                SetAttributeDecodingResult(0x03, DecodingResult.DecodingError);
                                throw new DLMSDecodingException(String.Format("{0} invalid identifier of value (Error Code:{1})",
                                   this.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch),
                                   "Decode_Data_Class_3");
                            }
                        }
                        else
                        {
                            if (current_char != (byte)DataTypes._A02_structure || Data[array_traverse++] != 0x02)
                            {
                                // Generate Error
                                SetAttributeDecodingResult(03, DecodingResult.DecodingError);
                                throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier of scaler_unit structure (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_3_Object");
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
                            SetAttributeDecodingResult(0x03, DecodingResult.Ready);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (!IsAttribReadable(0x03))
                            SetAttributeDecodingResult(03, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(03, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier 0f scaler in scaler_unit", OBISIndex, OBISIndex.OBISIndex),
                                "Decode_Data_Class_3_Object", ex);
                        }
                    }
                    //Perform Value Adjustment According Scaler_Unit Structure
                    Value = Convert.ToDouble(BasicEncodeDecode.ValueUnitScalerAdjustment(Value, scaler));

                }

                #endregion
                // make data array ready for upcoming objects
                //DLMS_Common.Data_Array_resizer(ref Data, array_traverse);
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                {
                    throw ex;
                }
                else
                {
                    throw new DLMSException(String.Format("{0}_{1}_{2}_{3} (Error Code:{4})", "Error occurred while decoding data ", 
                        OBISIndex, OBISIndex.OBISIndex,
                        "Decode_Data_Class_3_Object", (int)DLMSErrors.ErrorDecoding_Type), ex);
                }
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
                #region Attribute 0x02

                if (EncodingAttribute == 0x02 || EncodingAttribute == 0x00)
                {

                    bool IsWriteable = IsAttribWritable(0x02);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode {0},No Access (Error Code:{1}) "
                            , EncodingType, (int)DLMSErrors.Insufficient_Priviledge), "EncodeData_Class_3");

                    }
                    else                                        //Encode Here Data
                    {
                        switch (EncodingType)
                        {
                            case DataTypes._A03_boolean:        //Integral Math Types
                            case DataTypes._A16_enum:
                            case DataTypes._A11_unsigned:
                            case DataTypes._A0F_integer:
                            case DataTypes._A10_long:
                            case DataTypes._A12_long_unsigned:
                            case DataTypes._A05_double_long:
                            case DataTypes._A06_double_long_unsigned:
                            case DataTypes._A14_long_64:
                            case DataTypes._A15_long_64_unsigned:
                                {
                                    byte[] encodedData =
                                        BasicEncodeDecode.Intelligent_Data_Encoder(EncodingType, Value);
                                    EncodedRaw.AddRange(encodedData);
                                    break;
                                }
                            case DataTypes._A09_octet_string:
                            case DataTypes._A0A_visible_string:
                                {
                                    byte[] Encodedt = BasicEncodeDecode.Encode_OctetString(Value_Array, EncodingType);
                                    EncodedRaw.AddRange(Encodedt);
                                    break;
                                }
                            case DataTypes._A01_array:
                                {
                                    byte[] dt = BasicEncodeDecode.Encode_Array(Value_Array, EncodingSubType);
                                    EncodedRaw.AddRange(dt);
                                    break;
                                }
                            default:
                                throw new DLMSDecodingException(String.Format("{0}_{1} {2} data type not implemented yet (Error Code:{3})",
                                   OBISIndex, OBISIndex.OBISIndex, EncodingType, (int)DLMSErrors.Invalid_TypeNotIncluded), "EncodeData_Class_3");
                        }
                    }
                }

                #endregion
                #region Attribute 0x3 Unit Scaler

                if (EncodingAttribute == 0x03 || EncodingAttribute == 0x00)
                {

                    bool IsWriteable = IsAttribWritable(0x03);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode Unit Scaler,No Access (Error Code:{0})",
                            (int)DLMSErrors.Insufficient_Priviledge), "EncodeData_Class_3");
                    }
                    else
                    {
                        byte[] _valArray = new byte[5];
                        EncodedRaw.Add((byte)2);            // tag of structure
                        EncodedRaw.AddRange(Value_Array);   // rest of the things are written n the lower layers

                    }
                }

                #endregion
                return EncodedRaw.ToArray<byte>();
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                {
                    throw ex;
                }
                else
                    throw new DLMSException(String.Format("{0}_{1} (Error Code:{2})", "Error occurred while encoding data", "Encode_Data_Class_3",
                            (int)DLMSErrors.ErrorEncoding_Type), ex);
            }
            finally
            {
                EncodedRaw = null;
            }
        }

        #endregion

        #region Member Methods

        public override object Clone()
        {
            Class_3 cloned = new Class_3(this);
            return cloned;
        }

        /// <summary>
        /// Returns the String representation of the Class_3 object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            String BaseStr = base.ToString();
            StringBuilder strVal = new StringBuilder();
            strVal.AppendFormat(",Value Requested:{0:f3}:{1}", Value, GetAttributeDecodingResult(2));
            strVal.AppendFormat(",Unit:{0}:{1}", Unit, GetAttributeDecodingResult(3));
            return BaseStr + strVal.ToString();
        }

        #endregion
    }
}
