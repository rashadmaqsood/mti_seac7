using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS.Comm;

namespace DLMS
{
    /// <summary>
    /// Data Class (class_id: 1, version: 0) This allows modeling various data, such as configuration data and parameters.
    /// the object is identified though its logical name 
    /// <see cref="StOBISCode"/>
    /// </summary>
    public class Class_1 : Base_Class
    {
        #region DataMembers

        private ValueType value;
        private Array value_Array;
        private Object value_Obj;
        private int bitLength;



        #endregion

        #region Properties

        /// <summary>
        /// Value COSEM Interface Class 1 Attribute 0x02;Value Property To hold any ValueType data (value as choice)<see cref="DLMS.Comm.DataTypes"/> 
        /// </summary>
        public ValueType Value
        {
            get { return this.value; }
            set
            {
                this.value = value;
                EncodingAttribute = 0x02;
            }
        }
        /// <summary>
        /// Value COSEM Interface Class 1 Attribute 0x02;Value_Array Property To hold any type of array (value as choice)<see cref="DLMS.Comm.DataTypes"/> 
        /// </summary>
        public Array Value_Array
        {
            get { return value_Array; }
            set
            {
                value_Array = value;
                if (value_Array != null && value_Array is ICustomStructure[])
                {
                    EncodingAttribute = 0x02;
                    EncodingType = DataTypes._A01_array;
                    EncodingSubType = DataTypes._A02_structure;
                }
            }
        }
        /// <summary>
        /// Value COSEM Interface Class 1 Attribute 0x02;Value_Obj Property To hold any type of Object (value as choice)<see cref="DLMS.Comm.DataTypes"/> 
        /// </summary>
        public Object Value_Obj
        {
            get { return value_Obj; }
            set
            {
                value_Obj = value;
                if (value_Obj != null && value_Obj is ICustomStructure)
                {
                    EncodingAttribute = 0x02;
                    EncodingType = DataTypes._A02_structure;
                }
            }
        }

        /// <summary>
        /// BitLength Property is used to specify the encoded bit length for (Value as BitString)<see cref="DLMS.Comm.DataTypes"/> 
        /// </summary>
        public int BitLength
        {
            get { return bitLength; }
            set { bitLength = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Index">OBIS code for specific entity</param>
        /// <param name="Obis_Code">Bytes representation of an OBIS code</param>
        /// <param name="No_of_Associations">maximum no of association</param>
        public Class_1(Get_Index Index, byte[] Obis_Code,
            UInt16 No_of_Associations)
            : base(1, 2, 0, Index, Obis_Code, No_of_Associations)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="OBISCode">OBIS for desire DLMS/COSEM class instance</param>
        public Class_1(StOBISCode OBISCode)
            : base(OBISCode, 2, 0)
        {

        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Obis_Code">Bytes Representation of an OBIS code</param>
        /// <param name="Attribute_recieved">no of attribute associated with current object</param>
        public Class_1(byte[] Obis_Code, byte Attribute_recieved)
            : base(1, 2, 0, Obis_Code)
        {
            DecodingAttribute = Attribute_recieved;
        }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="obj">Class_1 object</param>
        public Class_1(Class_1 obj)
            : base(obj)
        {
            Value = obj.Value;
            Value_Array = obj.Value_Array;
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
            // UInt16 array_traverse = 0;
            byte[] Obis_code_recieved = null;
            byte current_char = 0;
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
                DecoderAttribute_0(ref Data, ref array_traverse, "Decode_Data_Class_1_Object_Array");
                DecoderLogicalName(ref Data, ref array_traverse, ref Obis_code_recieved, "Decode_Data_Class_1_Object_Array");
                #region Attribute 0x02

                if (DecodingAttribute == 0x02 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check access rights
                        if (!IsAttribReadable(2))
                            SetAttributeDecodingResult(2, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(2, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier of value (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_1");
                        }
                    }
                    else
                    {
                        // Reset Data Variables
                        Value = null;

                        // Save in array
                        DataTypes Recieved_Obj_Type = (DataTypes)current_char;
                        DecodingType = Recieved_Obj_Type;


                        switch (Recieved_Obj_Type)
                        {
                            #region ValueType

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
                                    Value_Obj = null;
                                    Value_Array = null;
                                    break;
                                }

                            #endregion
                            #region ICustomStructure

                            case DataTypes._A02_structure:
                                {
                                    array_traverse--;
                                    // save value
                                    if (this.Value_Obj == null)
                                        throw new DLMSDecodingException(String.Format("{0}_{1} Unable to decode custom structure,Null Reference", OBISIndex, OBISIndex.OBISIndex),
                                            "Decode_Data_Class_1");
                                    else
                                        ((ICustomStructure)this.Value_Obj).Decode_Data(Data, ref array_traverse, Data.Length);
                                    Value_Array = null;
                                    break;
                                }
                            #endregion
                            #region Octect_String

                            case DataTypes._A09_octet_string:
                            case DataTypes._A0A_visible_string:
                                {
                                    array_traverse--;
                                    Value_Array = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse, Data.Length);
                                    Value_Obj = null;
                                    break;
                                }
                            case DataTypes._A0C_utf8_string:
                                {
                                    array_traverse--;
                                    string retValue = BasicEncodeDecode.Decode_UTF8_String(Data, ref array_traverse, Data.Length);
                                    Value_Array = retValue.ToCharArray();
                                    Value_Obj = null;
                                    break;
                                }

                            #endregion
                            #region BitString

                            case DataTypes._A04_bit_string:
                                {
                                    array_traverse--;
                                    Value_Array = BasicEncodeDecode.Decode_BitString(Data, ref array_traverse, ref bitLength);
                                    Value_Obj = null;
                                    break;
                                }

                            #endregion
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
                            #region Array_Decoder

                            case DataTypes._A01_array:
                                {
                                    int arrlength = BasicEncodeDecode.Decode_Length(Data, ref array_traverse);
                                    DataTypes arrayType = (DataTypes)Data[array_traverse++];
                                    // Update Data Types
                                    DecodingType = Recieved_Obj_Type;
                                    DecodingSubType = arrayType;
                                    array_traverse--;

                                    #region Array Of ICustomStructure

                                    if (arrayType == DataTypes._A02_structure)  // Custom Structure's expected here
                                    {
                                        if (Value_Array == null)
                                            throw new DLMSDecodingException(String.Format("{0}_{1} ICustomStructure type objects are expected to decode  (Error Code:{2})",
                                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch),
                                                "Decode_Data_Class_1");
                                        ICustomStructure CopyObj = null;
                                        foreach (var item in Value_Array)
                                        {
                                            try
                                            {
                                                if (item != null)
                                                {
                                                    CopyObj = (ICustomStructure)item;
                                                    break;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                throw new DLMSDecodingException(String.Format("{0}_{1} ICustomStructure type objects are expected to decode (Error Code:{2})",
                                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch),
                                                "Decode_Data_Class_1");
                                            }
                                        }
                                        if (CopyObj == null)
                                            throw new DLMSDecodingException(String.Format("{0}_{1} Unable to decode custom structure,Null Reference (Error Code:{2})",
                                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_ObjectReference),
                                                "Decode_Data_Class_1");
                                        if (Value_Array.Length != arrlength)       //Read_Size Array for coming data
                                        {
                                            Array TArray = Array.CreateInstance(typeof(ICustomStructure), arrlength);
                                            //Copy Cloned Object In new Array
                                            for (int index = 0; index < TArray.Length; index++)
                                            {
                                                ((Object[])TArray)[index] = CopyObj.Clone();
                                            }
                                            Value_Array = TArray;
                                        }
                                        else
                                        {
                                            // Copy Cloned Object In new Array
                                            for (int index = 0; index < Value_Array.Length; index++)
                                            {
                                                ((Object[])Value_Array)[index] = CopyObj.Clone();
                                            }
                                        }
                                        // Decode ICustomStructure Array Objects
                                        for (int index = 0; index < arrlength; index++)
                                        {
                                            ((ICustomStructure[])Value_Array)[index].Decode_Data(Data, ref array_traverse, Data.Length);
                                        }
                                    }

                                    #endregion
                                    #region Array Of Simple DataTypes

                                    else if (// FIX_Length DataType 
                                            (arrayType == DataTypes._A03_boolean || arrayType == DataTypes._A05_double_long || arrayType == DataTypes._A06_double_long_unsigned ||
                                             arrayType == DataTypes._A07_floating_point || arrayType == DataTypes._A0F_integer || arrayType == DataTypes._A10_long ||
                                             arrayType == DataTypes._A11_unsigned || arrayType == DataTypes._A12_long_unsigned || arrayType == DataTypes._A14_long_64 || arrayType == DataTypes._A15_long_64_unsigned ||
                                             arrayType == DataTypes._A16_enum || arrayType == DataTypes._A23_Float32 || arrayType == DataTypes._A24_Float64 || arrayType == DataTypes._A0D_bcd) ||
                                        // Simple Variable Length DataType
                                            (arrayType == DataTypes._A04_bit_string || arrayType == DataTypes._A09_octet_string ||
                                             arrayType == DataTypes._A0A_visible_string ||
                                             arrayType == DataTypes._A19_datetime || arrayType == DataTypes._A1A_date || arrayType == DataTypes._A1B_time))
                                    {
                                        var decoded_objs = BasicEncodeDecode.Decode_SimpleArray(ref Data, ref array_traverse, arrlength, arrayType, length);
                                        // Store Decoded Objects in Object Reference
                                        if (decoded_objs != null)
                                            Value_Array = decoded_objs.ToArray();
                                        else
                                            // No Data Object Decoded
                                            Value_Array = new object[0];
                                    }

                                    #endregion
                                    else
                                    {
                                        throw new DLMSDecodingException(String.Format("{0}_{1} Array of type {2},decoder not implemented yet (Error Code:{3})", OBISIndex, OBISIndex.OBISIndex,
                                                                        arrayType, (int)DLMSErrors.Invalid_DecoderNotIncluded), "Decode_Data_Class_1");
                                    }
                                    Value_Obj = null;
                                    break;
                                }

                            #endregion
                            #region Compact_Array Decoder For Simple DataTypes

                            case DataTypes._A13_compact_arry:
                                {
                                    TypeDescriptor ContentDescription = null;
                                    DecodingType = DataTypes._A13_compact_arry;
                                    DecodingSubType = DataTypes._A00_Null;

                                    array_traverse--;
                                    var decoded_objs = BasicEncodeDecode.Decode_SimpleCompactArray(ref Data, ref array_traverse, ref ContentDescription, length);

                                    // Store Decoded Objects in Object Reference
                                    if (decoded_objs != null)
                                    {
                                        Value_Array = decoded_objs.ToArray();

                                    }
                                    else
                                        // No Data Object Decoded
                                        Value_Array = new object[0];

                                    if (ContentDescription != null)
                                    {
                                        DecodingSubType = ContentDescription.TypeTAG;
                                        if (ContentDescription.Elements != null &&
                                           ContentDescription.Elements.Count > 0)
                                        {
                                            DecodingSubType = ContentDescription.Elements[0].TypeTAG;
                                        }
                                    }

                                    break;
                                }

                            #endregion
                            default:
                                throw new DLMSDecodingException(String.Format("{0}_{1} {2} data type not implemented yet (Error Code:{3})",
                                    OBISIndex, OBISIndex.OBISIndex, Recieved_Obj_Type, (int)DLMSErrors.Invalid_TypeNotIncluded), "Decode_Data_Class_1");
                        }
                        SetAttributeDecodingResult(2, DecodingResult.Ready);
                    }
                }

                #endregion
                // make data array ready for upcoming objects
                // DLMS_Common.Data_Array_resizer(ref Data, array_traverse);
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
                        OBISIndex, OBISIndex.OBISIndex, "Decode_Data_Class_1_Object_Number", (int)DLMSErrors.ErrorDecoding_Type), ex);
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
                        throw new DLMSEncodingException(String.Format("Unable to encode,No Access (Error Code:{0})",
                            (int)DLMSErrors.Insufficient_Priviledge), "EncodeData_Class_1");
                    }
                    else  // Encode Here Data
                    {
                        switch (EncodingType)
                        {
                            #region Value_Type_DataTypes

                            case DataTypes._A03_boolean:            // Integral Math Types
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
                            case DataTypes._A0D_bcd:
                                {
                                    byte[] encodedData =
                                        BasicEncodeDecode.BCD_Data_Encoder(Value, BitLength);
                                    EncodedRaw.AddRange(encodedData);
                                    break;
                                }

                            #endregion
                            #region ICustomStructure DataType
                            case DataTypes._A02_structure:          //Encode ICustomStructure Object
                                {
                                    if (this.Value_Obj != null)
                                        EncodedRaw.AddRange(((ICustomStructure)Value_Obj).Encode_Data());
                                    else
                                        throw new DLMSEncodingException(String.Format("Unable to encode custom structure,null reference (Error Code:{0})",
                                            (int)DLMSErrors.Invalid_ObjectReference), "Encode_Data_Class_1");
                                    break;
                                }
                            #endregion
                            #region Octect_Visible Strings

                            case DataTypes._A09_octet_string:
                            case DataTypes._A0A_visible_string:
                                {
                                    byte[] _valArray = BasicEncodeDecode.Encode_OctetString(Value_Array, EncodingType);
                                    EncodedRaw.AddRange(_valArray);
                                    break;
                                }
                            case DataTypes._A0C_utf8_string:
                                {
                                    byte[] _valArray = BasicEncodeDecode.Encode_UTF8String((char[])Value_Array);
                                    EncodedRaw.AddRange(_valArray);
                                    break;
                                }

                            #endregion
                            #region BitString DataType
                            case DataTypes._A04_bit_string:
                                {
                                    byte[] _valArray = BasicEncodeDecode.Encode_BitString((byte[])Value_Array, BitLength);
                                    EncodedRaw.AddRange(_valArray);
                                    break;
                                }
                            #endregion
                            #region Array DataType
                            case DataTypes._A01_array:
                                {
                                    if (this.Value_Array == null)
                                        throw new DLMSEncodingException(String.Format("Unable to encode  values array,null reference (Error Code:{0})",
                                            (int)DLMSErrors.Invalid_ObjectReference), "Encode_Data_Class_1");
                                    //Encode Array Of ICustomStructurte Objects 
                                    if (this.Value_Array is ICustomStructure[])
                                    {
                                        byte[] dt = BasicEncodeDecode.Encode_Array((ICustomStructure[])Value_Array);
                                        EncodedRaw.AddRange(dt);
                                    }
                                    else
                                    {
                                        byte[] dt = BasicEncodeDecode.Encode_Array(Value_Array, EncodingSubType);
                                        EncodedRaw.AddRange(dt);
                                    }
                                    break;
                                }
                            #endregion
                            default:
                                throw new DLMSEncodingException(String.Format("Unable to encode,{0} type not Supported  (Error Code:{1})",
                                    EncodingType, (int)DLMSErrors.Invalid_TypeNotIncluded), "EncodeData_Class_1");

                        }
                    }
                }
                //------------------------------------------------------ 

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
                    throw new DLMSException(String.Format("{0}_{1}  (Error Code:{2})", "Error occurred while encoding data", "Encode_Data_Class_1",
                        (int)DLMSErrors.ErrorEncoding_Type), ex);
            }
        }

        #endregion

        #region Member Methods

        public override object Clone()
        {
            Class_1 cloned = new Class_1(this);
            return cloned;
        }

        /// <summary>
        /// Returns the String representation of the Class_1 object
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            String baseStr = base.ToString();
            StringBuilder strVal = new StringBuilder();
            strVal.AppendFormat(",Integral Value Requested:{0:f4}:{1}", Value, GetAttributeDecodingResult(2));
            if (Value_Array != null &&
                value_Array is byte[])
                strVal.AppendFormat(",Array Value Requested:{0}:{1}", DLMS_Common.ArrayToHexString((byte[])Value_Array), GetAttributeDecodingResult(2));
            return baseStr + strVal;
        }

        #endregion
    }
}
