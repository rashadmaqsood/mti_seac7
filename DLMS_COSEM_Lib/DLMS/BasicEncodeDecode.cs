// Code Revised & Modified
// Revision # 1.1 Tuesday 09 October 2012
// Revision # 1.2 Tuesday 15 September 2014

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections;
using DLMS.Comm;

namespace DLMS
{
    /// <summary>
    /// This DLMS_Common static class define Set of static helper method used in 
    /// various methods present in DLMS_COSEM Interface Classes(IC),Helper Classes
    /// and other Misc Classes Defined in Code
    /// </summary>
    public static class BasicEncodeDecode
    {
        /// <summary>
        /// Minimum DateTime Value being accepted in Encode/Decode Functions
        /// </summary>
        public static DateTime MinDateTime;

        /// <summary>
        /// Static Default Constructor
        /// </summary>
        static BasicEncodeDecode()
        {
            //January 1, 1753 
            MinDateTime = new DateTime(1753, 1, 1);
        }


        /// <summary>
        /// This helper decoder function decodes most Common Numeric <see cref="DLMS.Comm.DataTypes"/> in DLMS_COSEM Specification.
        /// See also <see cref="Intelligent_Data_Decoder(ref byte[], ref int, int)"/>
        /// </summary>
        public static ValueType Intelligent_Data_Decoder(ref byte[] Source_Array, ref int Source_Index)
        {
            return Intelligent_Data_Decoder(ref Source_Array, ref Source_Index, Source_Array.Length);
        }

        /// <summary>
        /// This helper decoder function decodes most Common Numeric <see cref="DLMS.Comm.DataTypes"/> in DLMS_COSEM Specification.
        /// </summary>
        /// <remarks>
        /// This helper decoder function decodes following Common Numeric <see cref="DLMS.Comm.DataTypes"/> ,
        /// _A14_long_64,_A15_long_64_unsigned,_A05_double_long,_A06_double_long_unsigned,_A10_long,_A11_unsigned,_A0F_integer and
        /// _A16_enum. This function decodes common DataTypes using A-XDR Decoding Scheme(REF),See Glossary and Term Section.
        /// </remarks>
        /// <param name="Source_Array">Source_Array that contains Encoded data</param>
        /// <param name="Source_Index">Start index In Source_Array</param>
        /// <param name="length">Total byte count</param>
        /// <returns><see cref="System.ValueType"/> BaseType for DOTNET Common Numeric DataTypes</returns>
        public static ValueType Intelligent_Data_Decoder(ref byte[] Source_Array, ref int Source_Index, int length)
        {
            DataTypes type_of_data = DataTypes._A00_Null;
            try
            {
                type_of_data = (DataTypes)Source_Array[Source_Index++];
                ValueType to_return;
                switch (type_of_data)
                {
                    case DataTypes._A14_long_64:
                        {
                            long temp =
                            ((long)Source_Array[Source_Index + 7]) + (((long)Source_Array[Source_Index + 6]) << 8) +
                            (((long)Source_Array[Source_Index + 5] << 16)) + (((long)Source_Array[Source_Index + 4]) << 24) +
                            (((long)Source_Array[Source_Index + 3]) << 32) + (((long)Source_Array[Source_Index + 2]) << 40) +
                            (((long)(Source_Array[Source_Index + 1]) << 48) + (((long)Source_Array[Source_Index]) << 56));
                            Source_Index += 8;
                            to_return = temp;
                            break;
                        }
                    case DataTypes._A15_long_64_unsigned:
                        {
                            ulong temp =
                             ((ulong)Source_Array[Source_Index + 7]) + (((ulong)Source_Array[Source_Index + 6]) << 8) +
                            (((ulong)Source_Array[Source_Index + 5] << 16)) + (((ulong)Source_Array[Source_Index + 4]) << 24) +
                            (((ulong)Source_Array[Source_Index + 3]) << 32) + (((ulong)Source_Array[Source_Index + 2]) << 40) +
                            (((ulong)(Source_Array[Source_Index + 1]) << 48) + (((ulong)Source_Array[Source_Index]) << 56));
                            Source_Index += 8;
                            to_return = temp;
                            break;
                        }
                    case DataTypes._A05_double_long:
                        {
                            int temp = (int)((Source_Array[Source_Index + 3]) + (Source_Array[Source_Index + 2] << 8) +
                            (Source_Array[Source_Index + 1] << 16) + (Source_Array[Source_Index] << 24));
                            Source_Index += 4;
                            to_return = temp;
                            break;
                        }
                    case DataTypes._A06_double_long_unsigned:
                        {
                            uint temp = (uint)((Source_Array[Source_Index + 3]) + (Source_Array[Source_Index + 2] << 8) +
                            (Source_Array[Source_Index + 1] << 16) + (Source_Array[Source_Index] << 24));
                            Source_Index += 4;
                            to_return = temp;
                            break;
                        }
                    case DataTypes._A10_long:
                        {
                            short temp = (short)((Source_Array[Source_Index + 1]) + (Source_Array[Source_Index] << 8));
                            Source_Index += 2;
                            to_return = temp;
                            break;
                        }
                    case DataTypes._A12_long_unsigned:
                        {
                            ushort temp = (ushort)((Source_Array[Source_Index + 1]) + (Source_Array[Source_Index] << 8));
                            Source_Index += 2;
                            to_return = temp;
                            break;
                        }
                    case DataTypes._A0D_bcd:
                        {
                            int Length_Array = BasicEncodeDecode.Decode_Length(Source_Array, ref Source_Index);

                            if (Length_Array < 0 || Length_Array > 16)
                                throw new Exception(String.Format("BCD Array Length Invalid, operation not supported (Error Code:{0})",
                               (int)DLMSErrors.Invalid_DataValue));

                            // Make Room For Coming Array
                            byte[] Value_Array = new byte[Length_Array];
                            Buffer.BlockCopy(Source_Array, Source_Index, Value_Array, 0, Value_Array.Length);
                            Source_Index += Length_Array;

                            ulong temp = BasicEncodeDecode.FromBCDToExtUInt64(Value_Array, (uint)0, (uint)Value_Array.Length, true);

                            #region // Range_ValueComparision

                            if (temp >= byte.MinValue && temp <= byte.MaxValue)
                                to_return = Convert.ToByte(temp);
                            else if (temp >= ushort.MinValue && temp <= ushort.MaxValue)
                                to_return = Convert.ToUInt16(temp);
                            else if (temp >= uint.MinValue && temp <= uint.MaxValue)
                                to_return = Convert.ToUInt32(temp);
                            else if (temp >= ulong.MinValue && temp <= ulong.MaxValue)
                                to_return = Convert.ToUInt64(temp);
                            else
                                to_return = temp;

                            #endregion

                            break;
                        }
                    case DataTypes._A11_unsigned:
                    case DataTypes._A0F_integer:
                    case DataTypes._A16_enum:
                    case DataTypes._A03_boolean:
                        to_return = Source_Array[Source_Index];
                        Source_Index++;
                        break;
                        throw new Exception(String.Format("data type DataTypes._A00_Null not included yet_Common.Intelligent_Data Decoder (Error Code:{0})",
                               (int)DLMSErrors.Invalid_TypeNotIncluded));
                    default:
                        throw new Exception(String.Format("data type not included yet_Common.Intelligent_Data Decoder (Error Code:{0})",
                            (int)DLMSErrors.Invalid_TypeNotIncluded));
                }
                return to_return;
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                    throw ex;
                else
                    throw new DLMSDecodingException(String.Format("Error Decoding {0} Type Value (Error Code:{1})",
                        (int)type_of_data, DLMSErrors.Invalid_TypeNotIncluded),
                        "Intelligent_Data_Decoder", ex);
            }
        }

        // ==========================================================================
        // public static void Decode_Array(byte[] Data, ref ushort array_traverser, ref Array decodedObjects)
        // {
        //     byte currentChar = Data[array_traverser++];
        //     if (currentChar != (byte)DataTypes._A01_array)
        //         throw new DLMSDecodingException(String.Format("{0} data type is expected", DataTypes._A01_array),
        //          "Decode_Array_BasicEncodeDecode");
        //     ushort arrlength = BasicEncodeDecode.Decode_Length(Data, ref array_traverser);
        // }
        //==========================================================================
        //==========================================================================
        /// <summary>
        /// This helper decoder function decodes Array Type of most Common Numeric <see cref="DLMS.Comm.DataTypes"/> in DLMS_COSEM Specification. 
        /// See Also <see cref="Intelligent_Data_Decoder(ref byte[], ref int, int)"/>
        /// </summary>
        /// <param name="Source_Array">Source_Array that contains Encoded data</param>
        /// <param name="Source_Index">Start index In Source_Array</param>
        /// <param name="dtType">DataType of Array Element<see cref="DLMS.Comm.DataTypes"/></param>
        /// <param name="length">Total byte count</param>
        /// <returns>Array Of <see cref="System.ValueType"/>, BaseType for DOTNET Common Numeric DataTypes</returns>
        public static ValueType[] Decode_Array(ref byte[] Source_Array, ref int Source_Index, DataTypes dtType, int length)
        {
            try
            {
                List<ValueType> destinatioArray = new List<ValueType>();
                if (Source_Array == null)
                    throw new DLMSDecodingException("Error Decoding Source Array Is Null Type", "Common_Decode_Array");
                byte current_Char = Source_Array[Source_Index++];
                if (current_Char != (byte)DataTypes._A01_array)
                {
                    throw new DLMSDecodingException(String.Format("Error Decoding {0} type is expected (Error Code:{1})",
                        DataTypes._A01_array, (int)DLMSErrors.Invalid_Type_MisMatch),
                        "Common_Decode_Array");
                }

                int Decode_Length = BasicEncodeDecode.Decode_Length(Source_Array, ref Source_Index);
                for (int index = 0; index < Decode_Length; index++)
                {
                    DataTypes dataType = (DataTypes)Source_Array[Source_Index];
                    if (dtType != dataType)
                        throw new DLMSDecodingException(String.Format("Error Decoding {0} type is expected (Error Code:{1})",
                            dtType, (int)DLMSErrors.Invalid_Type_MisMatch), "Common_Decode_Array");

                    ValueType t = Intelligent_Data_Decoder(ref Source_Array, ref Source_Index, Source_Array.Length);
                    destinatioArray.Add(t);
                }
                return destinatioArray.ToArray<ValueType>();
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                    throw ex;
                else
                    throw new DLMSDecodingException(String.Format("Error Decoding {0} Type Array (Error Code:{1})", dtType,
                        (int)DLMSErrors.ErrorDecoding_Type), "Common_Decoder_Array", ex);
            }
        }

        //==========================================================================
        /// <summary>
        /// This decoder function decode simple data types Compact Array<see cref="DLMS.Comm.DataTypes"/> in DLMS_COSEM Specification. 
        /// See Also <see cref="Intelligent_Data_Decoder(ref byte[], ref int, int)"/>
        /// </summary>
        /// <param name="Source_Array">Source_Array that contains Encoded data</param>
        /// <param name="Source_Index">Start index In Source_Array</param>
        /// <param name="dtType">DataType of Array Element <see cref="DLMS.Comm.DataTypes"/></param>
        /// <param name="length">Total byte count</param>
        /// <returns>Array Of <see cref="System.ValueType"/>, BaseType for DOTNET Common Numeric DataTypes</returns>
        public static List<object> Decode_SimpleCompactArray(ref byte[] Source_Array, ref int Source_Index,
                                                ref TypeDescriptor ContentDescription, int length)
        {
            List<object> decoded_data = null;
            List<byte> sourceArray = new List<byte>();
            List<byte> destinationArray = new List<byte>();

            byte[] TmpArray = null;

            try
            {
                #region Initial_Work

                if (Source_Array == null || Source_Array.Length < length)
                    throw new DLMSDecodingException("Error Decoding Source Array Is Null Type", "Parse_CompactArray");

                byte current_Char = Source_Array[Source_Index++];
                if (current_Char != (byte)DataTypes._A13_compact_arry)
                {
                    throw new DLMSDecodingException(String.Format("Error Decoding {0} type is expected (Error Code:{1})",
                        DataTypes._A13_compact_arry, (int)DLMSErrors.Invalid_Type_MisMatch), "Parse_CompactArray");
                }

                #endregion
                #region // Process_TypeDescriptor

                ContentDescription = new TypeDescriptor();
                ContentDescription.Decode_TypeDescripter(Source_Array, ref Source_Index, length);

                List<TypeDescriptor> Content_DescriptorStruct = new List<TypeDescriptor>();
                {
                    var descriptor = ContentDescription.All();
                    foreach (var type in descriptor)
                    {
                        Content_DescriptorStruct.Add(type);
                    }
                }

                #endregion
                #region // Copy Data Contents

                try
                {
                    int dataLength = Decode_Length(Source_Array, ref Source_Index);

                    // Copy Data Contents
#if NETFX_45 || NETFX_451

                    // Console.WriteLine("NET_45 was set");
                    var TArraySeg = new ArraySegment<byte>(Source_Array, Source_Index, dataLength);
#endif
#if NETFX_35 || NETFX_30
                    
                    // Console.WriteLine("NET_40 was set");
                    var TArraySeg = new byte[dataLength];
                    Buffer.BlockCopy(Source_Array, Source_Index, TArraySeg, 0, TArraySeg.Length);
#endif

                    sourceArray.AddRange(TArraySeg);
                }
                catch (Exception ex)
                {
                    throw new DLMSDecodingException("Error Decoding Compact Array,Invalid Array Data Length", "Parse_CompactArray");
                }

                #endregion

                int src_Index = 0;
                int type_Index = 0;
                // Parse Compact Array
                // SimpleDataType Decoding Work For BasicEncoderDecoder Work
                var elment_Count = BasicEncodeDecode.ParserHelper_CompactArray(destinationArray, sourceArray,
                                                                               ref src_Index, Content_DescriptorStruct, ref type_Index);

                // Initialize dtType
                DataTypes dtType = DataTypes._A00_Null, parentType = DataTypes._A00_Null;
                if (Content_DescriptorStruct[0] != null)
                {
                    dtType = Content_DescriptorStruct[0].TypeTAG;
                    if (Content_DescriptorStruct[0].Parent != null)
                    {
                        parentType = ((TypeDescriptor)Content_DescriptorStruct[0].Parent).TypeTAG;
                    }
                }

                TmpArray = destinationArray.ToArray();
                src_Index = 0;
                decoded_data = Decode_SimpleArray(ref TmpArray, ref src_Index, elment_Count, dtType, TmpArray.Length);

                return decoded_data;
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                    throw ex;
                else
                    throw new DLMSDecodingException(String.Format("Error occurred while Decoding DataType {0} (Error Code:{1})",
                         (ContentDescription != null) ? ContentDescription.ToString() : "None",
                         (int)DLMSErrors.ErrorDecoding_Type), "Decode_SimpleCompactArray", ex);
            }
        }

        //==========================================================================
        /// <summary>
        /// This decoder function decode simple data types Array<see cref="DLMS.Comm.DataTypes"/> in DLMS_COSEM Specification. 
        /// See Also <see cref="Intelligent_Data_Decoder(ref byte[], ref int, int)"/>
        /// </summary>
        /// <param name="Source_Array">Source_Array that contains Encoded data</param>
        /// <param name="Source_Index">Start index In Source_Array</param>
        /// <param name="dtType">DataType of Array Element <see cref="DLMS.Comm.DataTypes"/></param>
        /// <param name="length">Total byte count</param>
        /// <returns>Array Of <see cref="System.ValueType"/>, BaseType for DOTNET Common Numeric DataTypes</returns>
        public static List<object> Decode_SimpleArray(ref byte[] Source_Array, ref int Source_Index, int elment_Count, DataTypes dtType, int length)
        {
            List<object> decoded_data = null;

            try
            {
                #region Initial_Work

                if (Source_Array == null || Source_Array.Length < length)
                    throw new DLMSDecodingException("Error Decoding Source Array Is Null Type", "Decode_SimpleArray");

                #endregion

                decoded_data = new List<object>((elment_Count > 0) ? elment_Count : 100);

                // Encode_Data  
                while (elment_Count > 0 &&
                       Source_Index < Source_Array.Length)
                {
                    if (dtType == DataTypes._A01_array || dtType == DataTypes._A02_structure)
                    {
                        throw new DLMSDecodingException(String.Format("Error Decoding _A01_array or _A02_structure data types (Error Code:{0})",
                                                        (int)DLMSErrors.ErrorDecoding_Type), "Decode_SimpleCompactArray");
                    }

                    #region // FIX_Length Numerical DataType

                    else if (dtType == DataTypes._A03_boolean || dtType == DataTypes._A05_double_long || dtType == DataTypes._A06_double_long_unsigned ||
                             dtType == DataTypes._A07_floating_point || dtType == DataTypes._A0F_integer || dtType == DataTypes._A10_long ||
                             dtType == DataTypes._A11_unsigned || dtType == DataTypes._A12_long_unsigned || dtType == DataTypes._A14_long_64 || dtType == DataTypes._A15_long_64_unsigned ||
                             dtType == DataTypes._A16_enum || dtType == DataTypes._A23_Float32 || dtType == DataTypes._A24_Float64 || dtType == DataTypes._A0D_bcd)
                    {
                        var valueType = BasicEncodeDecode.Intelligent_Data_Decoder(ref Source_Array, ref Source_Index);
                        // Todo : Debug Type Decode Work
                        decoded_data.Add(valueType);
                    }

                    #endregion
                    #region // Simple Variable Length DataType

                    else if (dtType == DataTypes._A04_bit_string || dtType == DataTypes._A09_octet_string ||
                             dtType == DataTypes._A0A_visible_string ||
                             dtType == DataTypes._A19_datetime || dtType == DataTypes._A1A_date || dtType == DataTypes._A1B_time)
                    {
                        switch (dtType)
                        {
                            case DataTypes._A09_octet_string:
                                {
                                    var octSTRData = BasicEncodeDecode.Decode_OctectString(Source_Array, ref Source_Index);

                                    decoded_data.Add(octSTRData);
                                    break;
                                }
                            case DataTypes._A0A_visible_string:
                                {
                                    var vsbSTRData = BasicEncodeDecode.Decode_String(Source_Array, ref Source_Index);

                                    decoded_data.Add(vsbSTRData);
                                    break;
                                }
                            case DataTypes._A04_bit_string:
                                {
                                    int bitLength = 0;
                                    var vsbSTRData = BasicEncodeDecode.Decode_BitString(Source_Array, ref Source_Index, ref bitLength);

                                    List<byte> bitSTR = new List<byte>(32);
                                    var lengthData = BitConverter.GetBytes(bitLength);

                                    bitSTR.AddRange(lengthData);
                                    bitSTR.AddRange(vsbSTRData);

                                    decoded_data.Add(bitSTR);
                                    break;
                                }
                            #region date_time

                            case DataTypes._A19_datetime:
                            case DataTypes._A1A_date:
                            case DataTypes._A1B_time:
                                {
                                    byte[] octSTRData = null;
                                    // Skip DateTime Type TAGs
                                    Source_Index++;

                                    StDateTime StdateTime = new StDateTime();

                                    // Decode DateTime
                                    if (dtType == DataTypes._A19_datetime)
                                    {
                                        octSTRData = new byte[12];
                                        Buffer.BlockCopy(Source_Array, Source_Index, octSTRData, 0, octSTRData.Length);
                                        Source_Index += octSTRData.Length;

                                        StdateTime.DecodeDateTime(octSTRData);
                                    }
                                    // Decode Date
                                    else if (dtType == DataTypes._A1A_date)
                                    {
                                        octSTRData = new byte[05];
                                        Buffer.BlockCopy(Source_Array, Source_Index, octSTRData, 0, octSTRData.Length);
                                        Source_Index += octSTRData.Length;

                                        StdateTime.DecodeDate(octSTRData);
                                    }
                                    // Decode Time
                                    else if (dtType == DataTypes._A1B_time)
                                    {
                                        octSTRData = new byte[04];
                                        Buffer.BlockCopy(Source_Array, Source_Index, octSTRData, 0, octSTRData.Length);
                                        Source_Index += octSTRData.Length;

                                        StdateTime.DecodeTime(octSTRData);
                                    }

                                    decoded_data.Add(StdateTime);
                                    break;
                                }

                            #endregion
                            default:
                                throw new DLMSDecodingException(String.Format("Error Decoding DataType,Decoder not implemented {0} (Error Code:{1})",
                                                                dtType, (int)DLMSErrors.ErrorDecoding_Type), "Decode_SimpleArray");
                        }
                    }

                    #endregion

                    // Other DataType not included yet
                    else
                    {
                        throw new DLMSDecodingException(String.Format("Error Decoding DataType,Decoder not implemented {0} (Error Code:{1})",
                        dtType, (int)DLMSErrors.ErrorDecoding_Type), "Decode_SimpleArray");
                    }

                    elment_Count--;
                }

                return decoded_data;
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                    throw ex;
                else
                    throw new DLMSDecodingException(String.Format("Error occurred while Decoding DataType {0} (Error Code:{1})",
                                                    dtType, (int)DLMSErrors.ErrorDecoding_Type), "Decode_SimpleArray", ex);
            }
        }

        //==========================================================================
        /// <summary>
        /// This helper decoder function Parse Compact Array and Convert it to Simple Array Type As<see cref="DLMS.Comm.DataTypes"/> in DLMS_COSEM Specification. 
        /// See Also <see cref="Intelligent_Data_Decoder(ref byte[], ref int, int)"/>
        /// </summary>
        /// <param name="Source_Array">Source_Array that contains Encoded data</param>
        /// <param name="Source_Index">Start index In Source_Array</param>
        /// <param name="dtType">DataType of Array Element<see cref="DLMS.Comm.DataTypes"/></param>
        /// <param name="length">Total byte count</param>
        /// <returns>Array Of <see cref="System.ValueType"/>, BaseType for DOTNET Common Numeric DataTypes</returns>
        public static byte[] Parse_CompactArray(ref byte[] Source_Array, ref int Source_Index,
                                                ref TypeDescriptor ContentDescription, int length)
        {
            List<byte> sourceArray = new List<byte>();
            List<byte> destinationArray = new List<byte>();
            List<byte> tmpArray = new List<byte>();

            int element_Counter = -1;
            byte[] TmpArray = null;

            try
            {
                #region Initial_Work

                if (Source_Array == null || Source_Array.Length < length)
                    throw new DLMSDecodingException("Error Decoding Source Array Is Null Type", "Parse_CompactArray");

                byte current_Char = Source_Array[Source_Index++];
                if (current_Char != (byte)DataTypes._A13_compact_arry)
                {
                    throw new DLMSDecodingException(String.Format("Error Decoding {0} type is expected (Error Code:{1})",
                        DataTypes._A13_compact_arry, (int)DLMSErrors.Invalid_Type_MisMatch), "Parse_CompactArray");
                }

                #endregion
                #region // Decode_TypeDescriptor

                ContentDescription = new TypeDescriptor();
                ContentDescription.Decode_TypeDescripter(Source_Array, ref Source_Index, length);

                #endregion
                #region // Copy Data Contents

                try
                {
                    int dataLength = Decode_Length(Source_Array, ref Source_Index);
                    // Copy Data Contents

#if NETFX_45 || NETFX_451

                    // Console.WriteLine("NET_45 was set");
                    var TArraySeg = new ArraySegment<byte>(Source_Array, Source_Index, dataLength);
#endif
#if NETFX_35 || NETFX_30
                    
                    // Console.WriteLine("NET_40 was set");
                    var TArraySeg = new byte[dataLength];
                    Buffer.BlockCopy(Source_Array, Source_Index, TArraySeg, 0, TArraySeg.Length);
#endif

                    sourceArray.AddRange(TArraySeg);
                }
                catch (Exception ex)
                {
                    throw new DLMSDecodingException("Error Decoding Compact Array,Invalid Array Data Length", "Parse_CompactArray", ex);
                }

                #endregion
                #region // Type Descriptor Code Block

                List<TypeDescriptor> Content_DescriptorStruct = new List<TypeDescriptor>();
                {
                    var descriptor = ContentDescription.All();

                    foreach (var type in descriptor)
                    {
                        Content_DescriptorStruct.Add(type);
                    }
                }

                #endregion

                int src_Index = 0;
                int type_Index = 0;
                // Parse Compact Array
                // To Add Data Type TAGs For BasicEncoderDecoder Work
                element_Counter = BasicEncodeDecode.ParserHelper_CompactArray(destinationArray, sourceArray, ref src_Index, Content_DescriptorStruct, ref type_Index);
                sourceArray = null;
                Content_DescriptorStruct = null;

                #region // Insert Array & Element Count Data

                if (element_Counter <= 0)
                {
                    throw new DLMSDecodingException("Error Decoding Compact Array,Invalid Array Data Length", "Parse_CompactArray");
                }
                BasicEncodeDecode.Encode_Length(ref TmpArray, (ushort)element_Counter);
                destinationArray.Insert(0, (byte)DataTypes._A01_array);
                destinationArray.InsertRange(1, TmpArray);

                #endregion
                return destinationArray.ToArray();
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                    throw ex;
                else
                    throw new DLMSDecodingException(String.Format("Error Decoding Parse_CompactArray DataType {0} (Error Code:{1})",
                        (ContentDescription != null) ? ContentDescription.ToString() : "None",
                        (int)DLMSErrors.ErrorDecoding_Type), "Parse_CompactArray", ex);
            }
        }

        /// <summary>
        /// This helper method Parse data from Source Array then append it in destinationArray with relevant DataType TAGs 
        /// </summary>
        /// <param name="destinationArray">Resultant Encoded data with relevant DataType TAGs</param>
        /// <param name="sourceArray">Compact Array Data Contents received from metering device</param>
        /// <param name="src_Index">Index to Source Array</param>
        /// <param name="ContentDescription">Content Description is Type Descriptor of Compact Array</param>
        /// <param name="typeIndex">Index to Type Descriptor Data Structure</param>
        /// <returns></returns>
        internal static int ParserHelper_CompactArray(List<byte> destinationArray, List<byte> sourceArray,
                                                      ref int src_Index, List<TypeDescriptor> ContentDescription, ref int typeIndex)
        {
            int element_Counter = -1;
            TypeDescriptor currentTypeTAG = null;
            DataTypes dtType = DataTypes._A00_Null;
            DataTypes parentType = DataTypes._A00_Null;

            byte[] TmpArray = null;

            // Parse Compact Array
            // To Add Type TAGs For Normal Decoders To Work Proper
            try
            {
                // iterator
                while (src_Index < sourceArray.Count &&
                       sourceArray.Count > 0)
                {
                    // Reset TypeIndex To Zero
                    if (ContentDescription.Count > 0)
                        typeIndex = typeIndex % ContentDescription.Count;

                    currentTypeTAG = ContentDescription[typeIndex++];
                    if (currentTypeTAG == null)
                        throw new ArgumentNullException("ContentDescription");

                    // Initialize dtType
                    dtType = parentType = DataTypes._A00_Null;
                    if (currentTypeTAG != null)
                    {
                        dtType = currentTypeTAG.TypeTAG;
                        if (currentTypeTAG.Parent != null)
                        {
                            parentType = ((TypeDescriptor)currentTypeTAG.Parent).TypeTAG;
                        }
                    }

                    if (currentTypeTAG != null &&
                                currentTypeTAG.Parent == null)
                    {
                        // Reset Element Counter
                        if (element_Counter <= -1)
                            element_Counter = 0;
                        // Increment Counter
                        element_Counter++;
                    }

                    #region // Complex_DataType

                    if (dtType == DataTypes._A01_array || dtType == DataTypes._A02_structure)
                    {
                        #region _A01_array

                        if (dtType == DataTypes._A01_array)
                        {
                            #region Structural_Validation

                            if (currentTypeTAG != null &&
                                currentTypeTAG.TypeTAG == DataTypes._A01_array)
                            {
                                if (currentTypeTAG.Elements == null || currentTypeTAG.NumberOfElements < 0 ||
                                    currentTypeTAG.NumberOfElements != currentTypeTAG.Elements.Count)
                                    throw new DLMSDecodingException(String.Format("Error Invalid Element Count for Array Type ParserHelper_CompactArray (Error Code:{0})",
                                                                    (int)DLMSErrors.ErrorDecoding_Type), "ParserHelper_CompactArray");
                            }

                            #endregion

                            // Add DataType TAG
                            destinationArray.Add((byte)dtType);
                            BasicEncodeDecode.Encode_Length(ref TmpArray, currentTypeTAG.NumberOfElements);
                            destinationArray.AddRange(TmpArray);
                        }

                        #endregion
                        #region _A02_structure

                        else if (dtType == DataTypes._A02_structure)
                        {
                            // Structural_Validation
                            if (currentTypeTAG != null &&
                                currentTypeTAG.TypeTAG == DataTypes._A02_structure)
                            {
                                if (currentTypeTAG.Elements == null || currentTypeTAG.NumberOfElements < 0 ||
                                    currentTypeTAG.NumberOfElements != currentTypeTAG.Elements.Count)
                                    throw new DLMSDecodingException(String.Format("Error Invalid Element Count for Structure ParserHelper_CompactArray (Error Code:{0})",
                                                                    (int)DLMSErrors.ErrorDecoding_Type), "ParserHelper_CompactArray");
                            }

                            // Add DataType TAG
                            destinationArray.Add((byte)dtType);
                            destinationArray.Add((byte)currentTypeTAG.NumberOfElements);
                        }

                        #endregion
                    }

                    #endregion
                    #region // Simple DataType Decoding

                    else
                    {
                        #region // FIX_Length DataType

                        if (dtType == DataTypes._A03_boolean || dtType == DataTypes._A05_double_long || dtType == DataTypes._A06_double_long_unsigned ||
                            dtType == DataTypes._A07_floating_point || dtType == DataTypes._A0F_integer || dtType == DataTypes._A10_long ||
                            dtType == DataTypes._A11_unsigned || dtType == DataTypes._A12_long_unsigned || dtType == DataTypes._A14_long_64 ||
                            dtType == DataTypes._A15_long_64_unsigned || dtType == DataTypes._A16_enum
                            || dtType == DataTypes._A19_datetime || dtType == DataTypes._A1A_date || dtType == DataTypes._A1B_time)
                        {
                            // Add DataType TAG
                            destinationArray.Add((byte)dtType);

                            switch (dtType)
                            {
                                case DataTypes._A24_Float64:
                                case DataTypes._A15_long_64_unsigned:
                                case DataTypes._A14_long_64:
                                    {
                                        // 8 byte long type
                                        destinationArray.AddRange(sourceArray.GetRange(src_Index, 8));
                                        src_Index += 8;

                                        break;
                                    }
                                case DataTypes._A23_Float32:
                                case DataTypes._A06_double_long_unsigned:
                                case DataTypes._A05_double_long:
                                    {
                                        // 4 byte long type
                                        destinationArray.AddRange(sourceArray.GetRange(src_Index, 4));
                                        src_Index += 4;

                                        break;
                                    }
                                case DataTypes._A12_long_unsigned:
                                case DataTypes._A10_long:
                                    {
                                        // 2 byte long type
                                        destinationArray.AddRange(sourceArray.GetRange(src_Index, 2));
                                        src_Index += 2;

                                        break;
                                    }
                                case DataTypes._A11_unsigned:
                                case DataTypes._A0F_integer:
                                case DataTypes._A16_enum:
                                case DataTypes._A03_boolean:
                                    {
                                        // single byte long
                                        destinationArray.Add(sourceArray[src_Index]);
                                        src_Index += 1;

                                        break;
                                    }
                                #region date_time
                                case DataTypes._A19_datetime:
                                    {
                                        // 12 byte long date-time
                                        destinationArray.AddRange(sourceArray.GetRange(src_Index, 12));
                                        src_Index += 12;

                                        break;
                                    }
                                case DataTypes._A1A_date:
                                    {
                                        // 05 byte long date
                                        destinationArray.AddRange(sourceArray.GetRange(src_Index, 05));
                                        src_Index += 05;

                                        break;
                                    }
                                case DataTypes._A1B_time:
                                    {
                                        // 05 byte long time
                                        destinationArray.AddRange(sourceArray.GetRange(src_Index, 12));
                                        src_Index += 12;

                                        break;
                                    }
                                #endregion
                                default:
                                    throw new Exception(String.Format("data type not included yet ParserHelper_CompactArray Decoder (Error Code:{0})",
                                        (int)DLMSErrors.Invalid_TypeNotIncluded));

                            }

                        }

                        #endregion
                        #region // Simple Variable Length DataType

                        else if (dtType == DataTypes._A04_bit_string || dtType == DataTypes._A09_octet_string ||
                                 dtType == DataTypes._A0A_visible_string || dtType == DataTypes._A0D_bcd)
                        {
                            try
                            {
                                // Decode Data Length
                                int srcIndexer_len = 0;
                                int Dlength = BasicEncodeDecode.Decode_Length(sourceArray.GetRange(src_Index, 4).ToArray(), ref srcIndexer_len);
                                if (srcIndexer_len > 0)
                                    src_Index += srcIndexer_len;

                                #region // Special Case Handler null Value

                                if (Dlength <= 0)
                                {
                                    // Special null Data_Handler
                                    if (parentType == DataTypes._A02_structure)
                                    {
                                        // null Data Item
                                        destinationArray.Add(00);
                                    }
                                    else
                                    {
                                        destinationArray.Add((byte)dtType);
                                        destinationArray.Add(00);
                                    }
                                }

                                #endregion

                                else if (Dlength >= 0)
                                {
                                    BasicEncodeDecode.Encode_Length(ref TmpArray, Dlength);

                                    // Add Variable_Data,TypeTAG and DataLength 
                                    destinationArray.Add((byte)dtType);
                                    destinationArray.AddRange(TmpArray);

                                    // Append Variable_Data
                                    destinationArray.AddRange(sourceArray.GetRange(src_Index, Dlength));
                                    src_Index += Dlength;
                                }

                            }
                            catch (Exception ex)
                            {
                                throw new DLMSDecodingException(String.Format("Error decoding variable data length type {0}", dtType),
                                                                "ParserHelper_CompactArray", ex);
                            }

                            // else-if end
                        }

                        #endregion
                    }

                    #endregion

                    // End While
                }

                // return destinationArray.ToArray();
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                    throw ex;
                else
                    throw new DLMSDecodingException(String.Format("Error Exec ParserHelper_CompactArray (Error Code:{0})",
                        (int)DLMSErrors.ErrorDecoding_Type), "ParserHelper_CompactArray", ex);
            }
            return element_Counter;
        }

        //==========================================================================
        /// <summary>
        /// This helper function decodes an encoded length using A-XDR Decoding(REF) Scheme
        /// </summary>
        /// <param name="Packet">Source_Array that contains Encoded data</param>
        /// <param name="starting_index">Start_index In Source_Array</param>
        /// <param name="length">Total byte count</param>
        /// <returns>The decoded length</returns>
        public static int Decode_Length(byte[] Packet, ref int starting_index, int length)
        {

            //to find length
            //UInt16 length = Common.Decode_Length(Recieved_Packet, ref decode_counter);
            byte i = 1;
            UInt16 usLen = 0;

            if ((Packet[starting_index] & 0x80) == 0x80)
            {
                i = (byte)(Packet[starting_index++] & 0x7F);
            }
            else
            {
                usLen = Packet[starting_index++];
                return usLen;
            }

            for (; i > 0; i--)
            {
                usLen <<= 8;
                usLen |= (UInt16)Packet[starting_index++];
            }
            return usLen;
        }

        //==========================================================================
        /// <summary>
        /// This helper function decodes an encoded length using A-XDR Decoding(REF) Scheme.
        /// See also <see cref="Decode_Length(byte[],ref int, int)"/>
        /// </summary>
        public static int Decode_Length(byte[] Packet, ref int starting_index)
        {

            //to find length
            //UInt16 length = Common.Decode_Length(Recieved_Packet, ref decode_counter);

            byte i = 1;
            int usLen = 0;

            if ((Packet[starting_index] & 0x80) == 0x80)
            {
                i = (byte)(Packet[starting_index++] & 0x7F);
            }
            else
            {
                usLen = Packet[starting_index++];
                return usLen;
            }
            for (; i != 0; i--)
            {
                usLen <<= 8;
                usLen |= Packet[starting_index++];
            }
            return usLen;
        }

        //==========================================================================
        /// <summary>
        /// This helper function decodes an OctectString Type _A09_octet_string <see cref="DLMS.Comm.DataTypes"/> using A-XDR Decoding(REF) Scheme
        /// </summary>
        /// <param name="Source_Array">Source_Array that contains Encoded data</param>
        /// <param name="array_traverse">Start index In Source_Array</param>
        /// <param name="length">Total byte count</param>
        /// <returns>byte[] data decoded</returns>
        public static byte[] Decode_OctectString(byte[] Source_Array, ref int array_traverse, int length)
        {
            try
            {
                DataTypes dtType = (DataTypes)Source_Array[array_traverse++];
                if (dtType == DataTypes._A09_octet_string || dtType == DataTypes._A0A_visible_string)
                {
                    //Store length
                    int Length_Array = BasicEncodeDecode.Decode_Length(Source_Array, ref array_traverse);

                    // Make room for coming array
                    byte[] Value_Array = new byte[Length_Array];

                    Buffer.BlockCopy(Source_Array, array_traverse, Value_Array, 0, Value_Array.Length);
                    array_traverse += Length_Array;

                    // save coming array
                    // array_traverse--; // for the function to follow it has to be done (chamchuss)
                    // byte[] _val_Array = null;
                    // DLMS_Common.Save_Data_Subfunction(Source_Array, ref array_traverse,
                    //     ref _val_Array);
                    // return _val_Array;

                    return Value_Array;
                }
                else if (dtType == DataTypes._A00_Null)
                    return null;
                else
                    throw new DLMSDecodingException(String.Format("Error occurred while decoding the {0} type (Error Code:{1})",
                         dtType, (int)DLMSErrors.ErrorDecoding_Type), "Decode_OctectString_BasicEncodeDecode");
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                    throw ex;
                else
                    throw new DLMSDecodingException(String.Format("Error Decoding Octet/Visible String (Error Code:{0})",
                        (int)DLMSErrors.ErrorDecoding_Type), "Decode_OctectString_BasicEncodeDecode", ex);
            }
        }


        //==========================================================================
        /// <summary>
        /// This helper function decodes an UTF-8 String Type <see cref="DLMS.Comm.DataTypes"/> using A-XDR Decoding(REF) Scheme
        /// </summary>
        /// <param name="Source_Array">Source_Array that contains Encoded data</param>
        /// <param name="array_traverse">Start index In Source_Array</param>
        /// <param name="length">Total byte count</param>
        /// <returns>byte[] data decoded</returns>
        public static string Decode_UTF8_String(byte[] Source_Array, ref int array_traverse, int length)
        {
            string ret_DataType = string.Empty;
            try
            {
                DataTypes dtType = (DataTypes)Source_Array[array_traverse++];
                if (dtType == DataTypes._A0C_utf8_string)
                {
                    // Store length
                    int Length_Array = BasicEncodeDecode.Decode_Length(Source_Array, ref array_traverse);

                    // Make room for coming array
                    byte[] Value_Array = new byte[Length_Array * 2];

                    Buffer.BlockCopy(Source_Array, array_traverse, Value_Array, 0, Value_Array.Length);
                    array_traverse += Length_Array;

                    // Perform UTF8 Encoding/Decoding
                    Encoding enc8 = Encoding.UTF8;
                    ret_DataType = enc8.GetString(Value_Array);

                    return ret_DataType;
                }
                else if (dtType == DataTypes._A00_Null)
                    return null;
                else
                    throw new DLMSDecodingException(String.Format("Error occurred while decoding _A0C_utf8_string {0} type (Error Code:{1})",
                         dtType, (int)DLMSErrors.ErrorDecoding_Type), "Decode_UTF8_String_BasicEncodeDecode");
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                    throw ex;
                else
                    throw new DLMSDecodingException(String.Format("Error Decoding UTF8_String (Error Code:{0})",
                        (int)DLMSErrors.ErrorDecoding_Type), "Decode_UTF8_String_BasicEncodeDecode", ex);
            }
        }


        //==========================================================================
        /// <summary>
        /// This helper function decodes Type Descriptor for Compact Array DataType Decoding <see cref="DLMS.Comm.DataTypes"/> using A-XDR Decoding(REF) Scheme
        /// </summary>
        /// <param name="typeDescriptor">type</param>
        /// <param name = "Source_Array">Source_Array that contains Encoded data</param>
        /// <param name = "array_traverse">Start index In Source_Array</param>
        /// <param name = "length">Total byte count</param>
        /// <returns> byte[] data decoded</returns>
        public static void Decode_TypeDescripter(this TypeDescriptor typeDescriptor, byte[] Source_Array, ref int array_traverse, int length)
        {
            try
            {
                DataTypes dtType = (DataTypes)Source_Array[array_traverse++];

                // Special Decoding_Rule
                if (dtType == DataTypes._A01_array || dtType == DataTypes._A02_structure)
                {
                    if (dtType == DataTypes._A01_array)
                    {
                        typeDescriptor.TypeTAG = dtType;
                        typeDescriptor.NumberOfElements = 0;

                        // Number Of Element Of Array
                        ushort temp = (ushort)((Source_Array[array_traverse + 1]) + (Source_Array[array_traverse] << 8));
                        array_traverse += 2;

                        typeDescriptor.NumberOfElements = temp;

                        TypeDescriptor ArrayElementType = new TypeDescriptor(typeDescriptor, DataTypes._A00_Null);
                        typeDescriptor.AddSubTypeDescripter(ArrayElementType);

                        ArrayElementType.Decode_TypeDescripter(Source_Array, ref array_traverse, length);
                    }
                    else if (dtType == DataTypes._A02_structure)
                    {
                        typeDescriptor.TypeTAG = dtType;
                        typeDescriptor.NumberOfElements = 0;

                        // Number Of Element Of Array
                        typeDescriptor.NumberOfElements = Source_Array[array_traverse++];

                        // Decode Seq of Structural Elements
                        for (int NoOfElement = 1; typeDescriptor.NumberOfElements > 0 && NoOfElement <= typeDescriptor.NumberOfElements; NoOfElement++)
                        {
                            TypeDescriptor ArrayElementType = new TypeDescriptor(typeDescriptor, DataTypes._A00_Null);

                            typeDescriptor.AddSubTypeDescripter(ArrayElementType, (sbyte)NoOfElement);
                            ArrayElementType.Decode_TypeDescripter(Source_Array, ref array_traverse, length);
                        }
                    }
                }
                // Simple DataType Decoding Rule
                else
                {
                    typeDescriptor.TypeTAG = dtType;
                    typeDescriptor.NumberOfElements = 0;
                }
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                    throw ex;
                else
                    throw new DLMSDecodingException(String.Format("Error Decoding TypeDescripter (Error Code:{0})",
                        (int)DLMSErrors.ErrorDecoding_Type), "Decode_TypeDescripter", ex);
            }
        }

        //==========================================================================
        //==========================================================================
        /// <summary>
        /// This helper function decodes an OctectString Type _A09_octet_string <see cref="DLMS.Comm.DataTypes"/> using A-XDR Decoding(REF) Scheme
        /// See also <see cref="Decode_OctectString(byte[], ref int, int)"/>
        /// </summary>
        public static byte[] Decode_OctectString(byte[] Source_Array, ref int array_traverse)
        {
            return Decode_OctectString(Source_Array, ref array_traverse, Source_Array.Length);
        }

        ////==========================================================================
        /// <summary>
        /// This helper function decodes String Type _A0A_visible_string <see cref="DLMS.Comm.DataTypes"/> using A-XDR Decoding(REF) Scheme
        /// </summary>
        /// <param name="Source_Array">Source_Array that contains Encoded data</param>
        /// <param name="array_traverse">Start index In Source_Array</param>
        /// <param name="length">Total byte count</param>
        /// <returns>Decoded String</returns>
        public static String Decode_String(byte[] Source_Array, ref int array_traverse, int length)
        {
            try
            {
                byte[] rawBytes = Decode_OctectString(Source_Array, ref array_traverse, Source_Array.Length);
                String _decodeString = DLMS_Common.ArrayToPrintableString(rawBytes); /// new String(ASCIIEncoding.ASCII.GetChars(rawBytes));
                return _decodeString;
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                    throw ex;
                else
                    throw new DLMSDecodingException(String.Format("Error Decoding Octet/Visible String (Error Code:{0})",
                        (int)DLMSErrors.ErrorDecoding_Type), "Decode_String_BasicEncodeDecode", ex);

            }
        }

        /// <summary>
        /// This helper function decodes String Type _A0A_visible_string <see cref="DLMS.Comm.DataTypes"/> using A-XDR Decoding(REF) Scheme
        /// See Also <see cref="Decode_String(byte[], ref int, int)"/>
        /// </summary>
        /// <param name="Source_Array">Source_Array that contains Encoded data</param>
        /// <param name="array_traverse">Start index In Source_Array</param>
        /// <returns>Decoded String</returns>
        public static String Decode_String(byte[] Source_Array, ref int array_traverse)
        {
            try
            {
                return Decode_String(Source_Array, ref array_traverse, Source_Array.Length);
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                    throw;
                else
                    throw new DLMSDecodingException(String.Format("Error Decoding Octet/Visible String (Error Code:{0})",
                        (int)DLMSErrors.ErrorDecoding_Type),
                        "Decode_String_BasicEncodeDecode", ex);
            }
        }

        ////==========================================================================
        /// <summary>
        /// This helper function decodes String Type _A0A_visible_string <see cref="DLMS.Comm.DataTypes"/> using A-XDR Decoding(REF) Scheme
        /// See Also <see cref="Decode_String(byte[], ref int, int)"/>
        /// </summary>
        /// <param name="arg">The <see cref="Base_Class"/> IC_1,IC_3 (REF) Interface Classes Instance;
        /// that contain data read for particular attribute</param>
        /// <returns>Decoded String</returns>
        public static string Decode_String(Base_Class arg)
        {
            try
            {
                if (arg.GetType() == typeof(Class_1))
                {
                    Class_1 temp_obj = (Class_1)arg;
                    if (temp_obj.GetAttributeDecodingResult(2) == DecodingResult.Ready)
                    {
                        byte[] dtArray = (byte[])temp_obj.Value_Array;
                        string temp = new String(ASCIIEncoding.ASCII.GetChars(dtArray));
                        return temp;
                    }
                }
                if (arg.GetType() == typeof(Class_3))
                {
                    Class_3 temp_obj = (Class_3)arg;
                    if (temp_obj.GetAttributeDecodingResult(2) == DecodingResult.Ready)
                    {
                        byte[] dtArray = (byte[])temp_obj.Value_Array;
                        string temp = new String(ASCIIEncoding.ASCII.GetChars(dtArray));
                        return temp;
                    }
                }
                return null;
            }
            catch
            {
                throw;
            }
        }

        ////==========================================================================
        /// <summary>
        /// This helper function Decodes Bit Length using the A-XDR Decoding Encoding Scheme (REF)
        /// for type _A04_bit_string <see cref="DLMS.Comm.DataTypes"/>
        /// </summary>
        /// <remarks>
        /// The Bit Length for type _A04_bit_string Type <see cref="DLMS.Comm.DataTypes"/>;
        /// Internal/Encoded Strucute (DataType;0x4,bitlegth,BitString + Trailing Bits)
        /// </remarks>
        /// <param name="Source_Array">Source_Array that contains Encoded data</param>
        /// <param name="array_traverse">Start index In Source_Array</param>
        /// <param name="bitLength">Total Bit Count</param>
        /// <returns>The decoded String</returns>
        public static byte[] Decode_BitString(byte[] Source_Array, ref int array_traverse, ref int bitLength)
        {
            try
            {
                int DataByteCount = 0;
                DataTypes dtType = (DataTypes)Source_Array[array_traverse++];
                if (dtType != DataTypes._A04_bit_string)
                    throw new DLMSDecodingException(String.Format("Error occurred while decoding the {0} type (Error Code:{0})", dtType,
                         (int)DLMSErrors.ErrorDecoding_Type), "Decode_BitString_BasicEncodeDecode");
                //Decode Data Bit Length
                bitLength = BasicEncodeDecode.Decode_Length(Source_Array, ref array_traverse);
                DataByteCount = Convert.ToInt32(Math.Ceiling((bitLength / 8.0)));
                byte[] TArray = new byte[DataByteCount];
                //Copy Data Raw Data
                Array.Copy(Source_Array, array_traverse, TArray, 0, DataByteCount);
                array_traverse += (ushort)DataByteCount;
                //Verify BitLength according the array size
                if ((bitLength == 0 && (TArray == null || TArray.Length == 0)) ||
                     bitLength <= TArray.Length * 8)
                {
                    if (bitLength > 0)
                    {
                        BitArray TBits = new BitArray(bitLength, false);
                        int bitIndex = 0;
                        int index = 0;
                        int temp = 0;
                        //Copy Each bit From Data Array
                        while (bitIndex < bitLength || index < TArray.Length)
                        {
                            if (bitIndex % 8 == 0)   //Copy New Byte
                            {
                                temp = TArray[index++];
                            }
                            if ((temp & 0x80) != 0)
                                TBits[bitIndex] = true;
                            temp = temp << 1;       //Right Shift One Bit
                            bitIndex++;
                        }
                        //Reverse Bits Stored in the Bit Array
                        //DLMS_Common.Reverse(TBits);
                        TBits.CopyTo(TArray, 0);
                    }
                }
                else
                    throw new DLMSEncodingException(String.Format("Invalid bitLength received (Error Code:{0})", (int)DLMSErrors.Invalid_DataLength), "Encode_BitString");
                return TArray;
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                    throw ex;
                else
                    throw new DLMSEncodingException(String.Format("Error Decode_BitString (Error Code:{0})",
                        (int)DLMSErrors.ErrorDecoding_Type), "Common_Decode_BitString", ex);
            }
        }

        ////==========================================================================
        /// <summary>
        /// This helper function Decodes Struct DateTime(REF) 
        /// </summary>
        /// <remarks>
        ///  This helper function Decodes Struct DateTime(REF),encoded as 12 byte long Octect String(REF).
        ///  This function could only parse Struct DateTime In Exact DateTime element Values(No DateTime WildCared Support(REF)).
        ///  See Also <see cref="Decode_OctectString(byte[], ref int, int)"/>
        /// </remarks>
        /// <param name="DateTime_Octet_String">Encode Byte Array</param>
        /// <returns>DateTime</returns>
        public static DateTime Decode_DateTime(byte[] DateTime_Octet_String)
        {
            try
            {
                DateTime Decoded_val;
                if (DateTime_Octet_String.Length != 12 || DateTime_Octet_String == null)
                {
                    throw new DLMSDecodingException("Error occurred while decoding Date & Time,Invalid Data argument",
                        "Decode_DateTime_BasicEncodeDecode");
                }
                UInt16 Year = (UInt16)(((UInt16)(DateTime_Octet_String[0]) << 8) + DateTime_Octet_String[1]);
                byte Month = DateTime_Octet_String[2];
                byte Day_of_Month = DateTime_Octet_String[3];
                byte Day_of_Week = DateTime_Octet_String[4];
                byte Hours = DateTime_Octet_String[5];
                byte Minutes = DateTime_Octet_String[6];
                byte Secs = DateTime_Octet_String[7];
                byte m_Secs_x10 = (DateTime_Octet_String[8] == 0xFF) ? (byte)DateTimeWildCardMask.HourNotSpecified : DateTime_Octet_String[8];
                UInt16 Deviation = (UInt16)(((UInt16)(DateTime_Octet_String[9]) << 8) + DateTime_Octet_String[10]);
                byte clk_Status = DateTime_Octet_String[11];
                //TODO:Reconsider Code To Include Values Not Being Used UTC Deviation,Hundreds Seconds_x10
                string datetime_str = String.Format("{0:D4}/{1:D2}/{2:D2} {3:D2}:{4:D2}:{5:D2}", Year, Day_of_Month, Month, Hours, Minutes, Secs);
                Decoded_val = DateTime.ParseExact(datetime_str, "yyyy/dd/MM HH:mm:ss", null);
                return Decoded_val;
            }
            catch //(Exception exc)
            {
                //To Exec Decoders/Encoders
                //throw new DLMSDecodingException("Error occurred while decoding,Date & Time", "Decode_DateTime_BasicEncodeDecode",exc);
                //****Comment To WOrk Out Code
                return DateTime.MinValue;
            }
        }

        //==========================================================================
        /// <summary>
        /// This helper function Decodes Struct Date(REF) 
        /// </summary>
        /// <remarks>
        ///  This helper function Decodes Struct DateTime(REF),encoded as 05 byte long Octect String(REF).
        ///  This function could only parse Struct Date In Exact Date element Values(No Date WildCared Support(REF)).
        ///  See Also <see cref="Decode_OctectString(byte[], ref int, int)"/>
        /// </remarks>
        /// <param name="Date_Octet_String">Encoded Byte Array</param>
        /// <returns>DateTime</returns>
        public static DateTime Decode_Date(byte[] Date_Octet_String)
        {
            try
            {
                DateTime Decoded_val;
                if (Date_Octet_String.Length != 5 || Date_Octet_String == null)
                {
                    throw new DLMSDecodingException("Error occurred while decoding Date,Invalid Data argument",
                        "Decode_DateTime_BasicEncodeDecode");
                }
                UInt16 Year = (UInt16)(((UInt16)(Date_Octet_String[0]) << 8) + Date_Octet_String[1]);
                byte Month = Date_Octet_String[2];
                byte Day_of_Month = Date_Octet_String[3];
                byte Day_of_Week = Date_Octet_String[4];
                //Try Convert Into Valid Date Object
                //TODO:Recheck All Special Values/Masks
                if (Year == 0xFFFF)  //Year Wild Card
                {
                    Year = (ushort)DateTimeWildCardMask.YearNotSpecified;
                }
                string date_str = String.Format("{0:D4}/{1:D2}/{2:D2}", Year, Day_of_Month, Day_of_Week);
                Decoded_val = DateTime.ParseExact(date_str, "yyyy/dd/MM", null);
                return Decoded_val;
            }
            catch //(Exception exec)
            {
                //throw new DLMSDecodingException("Error occurred while decoding,Date & Time", "Decode_Date_BasicEncodeDecode",exec);
                //****Comment To WOrk Out Code
                return DateTime.MinValue;
            }
        }

        //==========================================================================
        /// <summary>
        /// This helper function Decodes Struct Time(REF) 
        /// </summary>
        /// <remarks>
        ///  This helper function Decodes Struct Time(REF),encoded as 04 byte long Octect String(REF).
        ///  This function could only parse Struct Time In Exact Time element Values(No Time WildCared Support(REF)).
        ///  See Also <see cref="Decode_OctectString(byte[], ref int, int)"/>
        /// </remarks>
        /// <param name="Time_Octet_String">Encoded Byte Array</param>
        /// <returns>DateTime</returns>
        public static DateTime Decode_Time(byte[] Time_Octet_String)
        {
            try
            {
                DateTime Decoded_val;
                if (Time_Octet_String.Length != 4 || Time_Octet_String == null)
                {
                    throw new DLMSDecodingException("Error occurred while decodingTime,Invalid Data argument",
                        "Decode_Time_BasicEncodeDecode");
                }
                byte Hours = Time_Octet_String[0];
                byte Minutes = Time_Octet_String[1];
                byte Secs = Time_Octet_String[2];
                byte m_Secs_x10 = Time_Octet_String[3];
                //Plz Recheck The Special Time Values Masks etc
                //if (Hours == 0xFF)  ///Hours Not Specified
                //{
                //    Hours = 0;  ///Tempo Value
                //}
                string time_str = String.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2}", Hours, Minutes, Secs, m_Secs_x10);
                Decoded_val = DateTime.ParseExact(time_str, "HH:mm:ss:tt", null);
                //if (Hours == 0xFF)  ///Hours Not Specified
                //{
                //   ///SET Masks In DateTime Object
                //    Decoded_val =  Decoded_val.AddYears((int)DateTimeWildCardMask.HourInValid);
                //    Decoded_val =  Decoded_val.AddHours((int)DateTimeWildCardMask.HourNotSpecified);
                //}
                return Decoded_val;
            }
            catch //(Exception exc)
            {
                //throw new DLMSDecodingException("Error occurred while decoding,Time", "Decode_Time_BasicEncodeDecode", exc);
                //****Comment To WOrk Out Code
                return DateTime.MinValue;
            }
        }

        //==========================================================================

        /// <summary>
        /// This helper function Encodes Struct DateTime(REF) 
        /// </summary>
        /// <remarks>
        ///  This helper function Encodes Struct DateTime(REF),Struct is encoded as 12 byte long Octect String(REF).
        ///  This function could only encode Struct DateTime In Exact DateTime element Values(No DateTime WildCared Support(REF)).
        ///  See Also <see cref="Decode_OctectString(byte[], ref int, int)"/>
        /// </remarks>
        /// <param name="To_Convert">DateTime parameter</param>
        /// <param name="Encoded_String">The encoded String for Struct DateTime(REF)</param>
        public static void Encode_DateTime(DateTime To_Convert, ref byte[] Encoded_String)
        {
            try
            {
                Encoded_String = null;
                UInt16 Year = (UInt16)(To_Convert.Year);
                byte Month = (byte)(To_Convert.Month);
                byte Day_of_Month = (byte)(To_Convert.Day);
                byte Day_of_Week = (byte)(To_Convert.DayOfWeek);
                byte Hours = (byte)(To_Convert.Hour);
                byte Minutes = (byte)(To_Convert.Minute);
                byte Secs = (byte)(To_Convert.Second);
                byte m_Secs_x10 = (byte)(To_Convert.Millisecond / 10);
                UInt16 Deviation = 0x00;
                byte clk_Status = 0x00;
                //****Comment Recheck Code Works Properly
                //Check & Process Wild Card
                Encoded_String = DLMS_Common.Append_to_End(Month, Day_of_Month);
                Encoded_String = DLMS_Common.Append_to_Start(Encoded_String, Year);
                Encoded_String = DLMS_Common.Append_to_End(Encoded_String, Day_of_Week);
                Encoded_String = DLMS_Common.Append_to_End(Encoded_String, Hours);
                Encoded_String = DLMS_Common.Append_to_End(Encoded_String, Minutes);
                Encoded_String = DLMS_Common.Append_to_End(Encoded_String, Secs);
                Encoded_String = DLMS_Common.Append_to_End(Encoded_String, m_Secs_x10);
                Encoded_String = DLMS_Common.Append_to_End(Encoded_String, Deviation);
                Encoded_String = DLMS_Common.Append_to_End(Encoded_String, clk_Status);
            }
            catch (Exception ex)
            {
                throw new DLMSEncodingException("Error occurred while encoding,Date & Time", "Encode_DateTime_BasicEncodeDecode", ex);
            }
        }

        //==========================================================================

        /// <summary>
        /// This helper function Encodes Struct Date(REF) 
        /// </summary>
        /// <remarks>
        ///  This helper function Encodes Struct Date(REF),Struct is encoded as 05 byte long Octect String(REF).
        ///  This function could only encode Struct Date In Exact Date element Values(No Date WildCared Support(REF)).
        ///  See Also <see cref="Decode_OctectString(byte[], ref int, int)"/>
        /// </remarks>
        /// <param name="To_Convert">DateTime parameter</param>
        /// <param name="Encoded_String">The encoded String for Struct DateTime(REF)</param>
        public static void Encode_Date(DateTime To_Convert, ref byte[] Encoded_String)
        {
            try
            {
                Encoded_String = null;
                UInt16 Year = (UInt16)(To_Convert.Year);
                byte Month = (byte)(To_Convert.Month);
                byte Day_of_Month = (byte)(To_Convert.Day);
                byte Day_of_Week = (byte)(To_Convert.DayOfWeek);

                if (To_Convert <= MinDateTime)       ///Check Special DateTime Objects
                {
                    //Check & Process Wild Card
                    if (Year == (ushort)DateTimeWildCardMask.YearNotSpecified)
                        Year = 0xFFFF;
                }
                //****Comment Recheck Code Works Properly
                Encoded_String = DLMS_Common.Append_to_End(Month, Day_of_Month);
                Encoded_String = DLMS_Common.Append_to_Start(Encoded_String, Year);
                Encoded_String = DLMS_Common.Append_to_End(Encoded_String, Day_of_Week);

            }
            catch (Exception ex)
            {
                throw new DLMSEncodingException("Error occurred while encoding,Date", "Encode_Date_BasicEncodeDecode", ex);
            }
        }

        //==========================================================================
        /// <summary>
        /// This helper function Encodes Struct Time(REF) 
        /// </summary>
        /// <remarks>
        ///  This helper function Encodes Struct Time(REF),Struct is encoded as 04 byte long Octect String(REF).
        ///  This function could only encode Struct Time In Exact Time element Values(No Time WildCared Support(REF)).
        ///  See Also <see cref="Decode_OctectString(byte[], ref int, int)"/>
        /// </remarks>
        /// <param name="To_Convert">DateTime parameter</param>
        /// <param name="Encoded_String">The encoded String for Struct DateTime(REF)</param>
        public static void Encode_Time(DateTime To_Convert, ref byte[] Encoded_String)
        {
            try
            {
                Encoded_String = null;
                UInt16 Year = (UInt16)(To_Convert.Year);

                byte Hours = (byte)(To_Convert.Hour);
                byte Minutes = (byte)(To_Convert.Minute);
                byte Secs = (byte)(To_Convert.Second);
                byte m_Secs_x10 = (byte)(To_Convert.Millisecond / 10);

                if (To_Convert <= MinDateTime)       //Check Special DateTime Objects
                {
                    //Check & Process Wild Card
                    if (Year == (ushort)DateTimeWildCardMask.HourInValid && Hours == (byte)DateTimeWildCardMask.HourNotSpecified)
                        Hours = 0xFF;
                }
                //****Comment Recheck Code Works Properly
                Encoded_String = DLMS_Common.Append_to_End(Encoded_String, Hours);
                Encoded_String = DLMS_Common.Append_to_End(Encoded_String, Minutes);
                Encoded_String = DLMS_Common.Append_to_End(Encoded_String, Secs);
                Encoded_String = DLMS_Common.Append_to_End(Encoded_String, m_Secs_x10);
            }
            catch (Exception ex)
            {
                throw new DLMSEncodingException("Error occurred while encoding,Time", "Encode_Time_BasicEncodeDecode", ex);
            }
        }

        //==========================================================================
        /// <summary>
        /// This helper Function Convert from IPv4 Address to UInt32 Number to 
        /// <see cref="System.Net.IPAddress"/>
        /// </summary>
        /// <param name="IP_Raw">UInt32 Number</param>
        /// <returns><see cref="System.Net.IPAddress"/></returns>
        public static IPAddress Decode_IP(UInt32 IP_Raw)
        {
            string str_IP = ((IP_Raw & 0xFF000000) >> 24).ToString() + "." +
                ((IP_Raw & 0x00FF0000) >> 16).ToString() + "." +
                ((IP_Raw & 0x0000FF00) >> 8).ToString() + "." +
                ((IP_Raw & 0x000000FF)).ToString();
            IPAddress IP_to_return = IPAddress.Parse(str_IP);
            return IP_to_return;
        }

        //==========================================================================

        /// <summary>
        /// This helper decoder function skip out most Common Numeric <see cref="DLMS.Comm.DataTypes"/> in DLMS_COSEM Specification.
        /// </summary>
        /// <remarks>
        /// This helper decoder function Skip out following Common Numeric <see cref="DLMS.Comm.DataTypes"/>
        /// _A14_long_64,_A15_long_64_unsigned,_A05_double_long,_A06_double_long_unsigned,_A10_long,_A11_unsigned,_A0F_integer and
        /// _A16_enum. This function decodes common DataTypes using A-XDR Decoding Scheme,See Glossary And Term Section
        /// </remarks>
        /// <param name="Source_Array">Source_Array that contains Encoded data</param>
        /// <param name="Source_Index">Start index In Source_Array</param>
        public static void Skip_Elements(byte[] Source_Array, ref int Source_Index)
        {
            DataTypes type_of_data = (DataTypes)Source_Array[Source_Index++];

            switch (type_of_data)
            {
                case DataTypes._A00_Null:
                    break;

                case DataTypes._A03_boolean:
                    Source_Index++;
                    break;

                case DataTypes._A04_bit_string:
                    byte temp_len = Source_Array[Source_Index++];
                    Source_Index += temp_len;
                    break;

                case DataTypes._A05_double_long:
                    Source_Index += 4;
                    break;

                case DataTypes._A06_double_long_unsigned:
                    Source_Index += 4;
                    break;

                case DataTypes._A07_floating_point:
                    Source_Index += 4;
                    break;

                case DataTypes._A09_octet_string:
                    byte temp_length = Source_Array[Source_Index++];
                    Source_Index += temp_length;
                    break;

                case DataTypes._A0A_visible_string:
                    byte temp_len1 = Source_Array[Source_Index++];
                    Source_Index += temp_len1;
                    break;

                case DataTypes._A0D_bcd:
                    Source_Index++;
                    break;

                case DataTypes._A0F_integer:
                    Source_Index++;
                    break;

                case DataTypes._A11_unsigned:
                    Source_Index++;
                    break;

                case DataTypes._A12_long_unsigned:
                    Source_Index += 2;
                    break;

                case DataTypes._A14_long_64:
                    Source_Index += 8;
                    break;

                case DataTypes._A15_long_64_unsigned:
                    Source_Index += 8;
                    break;

                case DataTypes._A16_enum:
                    Source_Index++;
                    break;

                case DataTypes._A23_Float32:
                    Source_Index += 4;
                    break;

                case DataTypes._A24_Float64:
                    Source_Index += 8;
                    break;

                default:
                    throw new DLMSException(String.Format("data type not included yet_Common.In_Data Decoder (Error Code:{0})",
                           (int)DLMSErrors.Invalid_TypeNotIncluded));
            }
        }

        //==========================================================================
        /// <summary>
        /// This helper encoder function encodes most Common Numeric <see cref="DLMS.Comm.DataTypes"/> in DLMS_COSEM Specification.
        /// </summary>
        /// <remarks>
        /// This helper encoder function Encodes following Common Numeric <see cref="DLMS.Comm.DataTypes"/> ,
        /// _A14_long_64,_A15_long_64_unsigned,_A05_double_long,_A06_double_long_unsigned,_A10_long,_A11_unsigned,_A0F_integer and
        /// _A16_enum. This function encodes common DataTypes using A-XDR Encoding Scheme(REF),See Glossary And Term Section
        /// </remarks>
        /// <param name="type_of_data"><see cref="DLMS.Comm.DataTypes"/> Parameter Encoding Type</param>
        /// <param name="_value"><see cref="System.ValueType"/>Parameter Value</param>
        /// <returns>Encoded Value Array</returns>
        public static byte[] Intelligent_Data_Encoder(DataTypes type_of_data, ValueType _value)
        {
            try
            {
                List<byte> destinationArray = new List<byte>();
                destinationArray.Add((byte)type_of_data);
                switch (type_of_data)
                {
                    #region DataTypes._A00_Null

                    case DataTypes._A00_Null:
                        {
                            return new byte[1] { (byte)DataTypes._A00_Null };
                            break;
                        }

                    #endregion
                    #region DataTypes._A0F_integer

                    case DataTypes._A0F_integer:
                        {
                            long value = Convert.ToInt64(_value);
                            if (value > sbyte.MaxValue || value < sbyte.MinValue)
                            {
                                throw new DLMSEncodingException(String.Format("Invalid {0} Value (Error Code:{1})", type_of_data,
                                                                            (int)DLMSErrors.Invalid_DataValue),
                                    "Intelligent_Data_Encoder");
                            }
                            else
                            {
                                destinationArray.Add((byte)value);
                            }
                            break;
                        }

                    #endregion
                    #region DataTypes._A11_unsigned

                    case DataTypes._A11_unsigned:
                        {
                            ulong value = Convert.ToUInt64(_value);
                            if (value > byte.MaxValue || value < byte.MinValue)
                            {

                                throw new DLMSEncodingException(String.Format("Invalid {0} Value (Error Code:{1})", type_of_data,
                                    (int)DLMSErrors.Invalid_DataValue), "Intelligent_Data_Encoder");
                            }
                            else
                            {
                                destinationArray.Add((byte)value);
                            }
                            break;
                        }

                    #endregion
                    #region DataTypes._A16_enum:

                    case DataTypes._A16_enum:
                        {
                            ulong value = Convert.ToUInt64(_value);
                            if (value > byte.MaxValue || value < byte.MinValue)
                            {
                                throw new DLMSEncodingException(String.Format("Invalid {0} Value (Error Code:{1})", type_of_data,
                                    (int)DLMSErrors.Invalid_DataValue), "Intelligent_Data_Encoder");
                            }
                            else
                            {
                                destinationArray.Add((byte)value);
                            }
                            break;
                        }

                    #endregion
                    #region DataTypes._A03_boolean:

                    case DataTypes._A03_boolean:
                        {
                            long value = Convert.ToInt64(_value);
                            byte t = (value == 0) ? (byte)0 : (byte)1;
                            destinationArray.Add(t);
                            break;
                        }

                    #endregion
                    #region DataTypes._A05_double_long:

                    case DataTypes._A05_double_long:
                        {
                            long value = Convert.ToInt64(_value);
                            if (value > int.MaxValue || value < int.MinValue)
                            {
                                throw new DLMSEncodingException(String.Format("Invalid {0} Value (Error Code:{1})", type_of_data,
                                    (int)DLMSErrors.Invalid_DataValue), "Intelligent_Data_Encoder");
                            }
                            else
                            {
                                destinationArray.Add((byte)(value >> 24));
                                destinationArray.Add((byte)(value >> 16));
                                destinationArray.Add((byte)(value >> 8));
                                destinationArray.Add((byte)(value & 0xFF));
                            }
                            break;
                        }

                    #endregion
                    #region DataTypes._A06_double_long_unsigned:

                    case DataTypes._A06_double_long_unsigned:
                        {
                            ulong value = Convert.ToUInt64(_value);
                            if (value > uint.MaxValue || value < uint.MinValue)
                            {
                                throw new DLMSEncodingException(String.Format("Invalid {0} Value (Error Code:{1})", type_of_data,
                                    (int)DLMSErrors.Invalid_DataValue), "Intelligent_Data_Encoder");
                            }
                            else
                            {
                                destinationArray.Add((byte)(value >> 24));
                                destinationArray.Add((byte)(value >> 16));
                                destinationArray.Add((byte)(value >> 8));
                                destinationArray.Add((byte)(value & 0xFF));
                            }
                            break;
                        }

                    #endregion
                    #region DataTypes._A10_long

                    case DataTypes._A10_long:
                        {
                            long value = Convert.ToInt64(_value);
                            if (value < short.MinValue || value > short.MaxValue)
                                throw new DLMSEncodingException(String.Format("Invalid {0} Value (Error Code:{1})", type_of_data,
                                    (int)DLMSErrors.Invalid_DataValue), "Intelligent_Data_Encoder");
                            else
                            {
                                destinationArray.Add((byte)(value >> 8));
                                destinationArray.Add((byte)(value & 0xFF));
                            }
                            break;
                        }

                    #endregion
                    #region DataTypes._A12_long_unsigned

                    case DataTypes._A12_long_unsigned:
                        {
                            ulong value = Convert.ToUInt64(_value);
                            if (value < ushort.MinValue || value > ushort.MaxValue)
                                throw new DLMSEncodingException(String.Format("Invalid {0} Value (Error Code:{1})", type_of_data,
                                    (int)DLMSErrors.Invalid_DataValue), "Intelligent_Data_Encoder");
                            else
                            {
                                destinationArray.Add((byte)(value >> 8));
                                destinationArray.Add((byte)(value & 0xFF));
                            }
                            break;
                        }

                    #endregion
                    #region DataTypes._A14_long_64

                    case DataTypes._A14_long_64:
                        {
                            long value = Convert.ToInt64(_value);
                            if (value < long.MinValue || value > long.MaxValue)
                            {
                                throw new DLMSEncodingException(String.Format("Invalid {0} Value (Error Code:{1})", type_of_data,
                                    (int)DLMSErrors.Invalid_DataValue), "Intelligent_Data_Encoder");
                            }
                            else
                            {
                                destinationArray.Add((byte)(value >> (7 * 8)));
                                destinationArray.Add((byte)(value >> (6 * 8)));
                                destinationArray.Add((byte)(value >> (5 * 8)));
                                destinationArray.Add((byte)(value >> (4 * 8)));
                                destinationArray.Add((byte)(value >> (3 * 8)));
                                destinationArray.Add((byte)(value >> (2 * 8)));
                                destinationArray.Add((byte)(value >> (1 * 8)));
                                destinationArray.Add((byte)(value & 0xFF));
                            }
                            break;
                        }

                    #endregion
                    #region DataTypes._A15_long_64_unsigned

                    case DataTypes._A15_long_64_unsigned:
                        {
                            ulong value = Convert.ToUInt64(_value);
                            if (value < ulong.MinValue || value > ulong.MaxValue)
                            {
                                throw new DLMSEncodingException(String.Format("Invalid {0} Value (Error Code:{1})", type_of_data,
                                    (int)DLMSErrors.Invalid_DataValue), "Intelligent_Data_Encoder");
                            }
                            else
                            {
                                destinationArray.Add((byte)(value >> (7 * 8)));
                                destinationArray.Add((byte)(value >> (6 * 8)));
                                destinationArray.Add((byte)(value >> (5 * 8)));
                                destinationArray.Add((byte)(value >> (4 * 8)));
                                destinationArray.Add((byte)(value >> (3 * 8)));
                                destinationArray.Add((byte)(value >> (2 * 8)));
                                destinationArray.Add((byte)(value >> (1 * 8)));
                                destinationArray.Add((byte)(value & 0xFF));
                            }
                            break;
                        }

                    #endregion
                    #region Default
                    default:
                        throw new DLMSEncodingException(String.Format("{0} Data Type not implemented yet,1Sorry", type_of_data), "Intelligent_Data_Encoder");

                        #endregion
                }
                ///Type TAG + Value Bytes
                if (destinationArray.Count > 1)
                    return destinationArray.ToArray<byte>();
                else
                    return null;
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                    throw ex;
                else
                    throw new DLMSEncodingException(String.Format("Error Encoding {0} Type Value (Error Code:{1})", type_of_data),
                        "Intelligent_Data_Encoder", ex);
            }
        }

        //==========================================================================
        /// <summary>
        /// This helper encoder function encodes BCD DataType Numeric 
        /// <see cref="DLMS.Comm.DataTypes"/> in DLMS_COSEM Specification
        /// </summary>
        /// <param name="_value"><see cref="System.ValueType"/>Parameter Value</param>
        /// <param name="DataLength">BCD Encoded Data Length</param>
        /// <returns>Encoded Value Array</returns>
        public static byte[] BCD_Data_Encoder(ValueType _value, int DataLength = 0)
        {
            try
            {
                List<byte> destinationArray = new List<byte>();
                destinationArray.Add((byte)DataTypes._A0D_bcd);

                ulong value = Convert.ToUInt64(_value);

                byte[] NumberElements = BasicEncodeDecode.FindNumberElements(value);
                byte[] Encode_BCD = BasicEncodeDecode.IntToBCD(NumberElements, true);
                byte[] Encoded_Length = null;
                ushort Length_Array = (ushort)Encode_BCD.Length;

                // Adjust BCD Encoded Data_Length
                if (DataLength != 0 && DataLength > 0)
                {
                    if (DataLength < Encode_BCD.Length)
                        throw new ArgumentException("Error,DataLength Parameter Value Truncate Encoded Data", "DataLength");

                    byte[] tmpArray = new byte[DataLength];

                    // Copy Data From Encode_BCD To tmpArray
                    // Loop Initialization
                    int indexSource = Encode_BCD.Length - 1;
                    int index = tmpArray.Length - 1;

                    for (; index >= 0 && indexSource >= 0; index--, indexSource--)
                    {
                        tmpArray[index] = Encode_BCD[indexSource];
                    }

                    Encode_BCD = tmpArray;
                    // Update Length_Array
                    Length_Array = (ushort)Encode_BCD.Length;
                }

                if (Length_Array < 0 || Length_Array > 16)
                    throw new Exception(String.Format("BCD Array Length Invalid, operation not supported (Error Code:{0})",
                                        (int)DLMSErrors.Invalid_DataValue));

                BasicEncodeDecode.Encode_Length(ref Encoded_Length, (ushort)Encode_BCD.Length);

                // Add BCD Array Length
                destinationArray.AddRange(Encoded_Length);
                // Add Encode BCD Data
                destinationArray.AddRange(Encode_BCD);

                // Type_TAG + Value_Byte
                if (destinationArray.Count > 1)
                    return destinationArray.ToArray<byte>();
                else
                    return null;
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                    throw ex;
                else
                    throw new DLMSEncodingException(String.Format("Error Encoding {0} Type Value (Error Code:{1})", DataTypes._A0D_bcd),
                        "BCD_Data_Encoder", ex);
            }
        }

        //==========================================================================

        /// <summary>
        /// This helper function encodes UTF8-String Type _A0C_utf8_string(REF)
        /// </summary>
        /// <remarks>
        /// This helper function encodes String Type _A0C_utf8_string <see cref="DLMS.Comm.DataTypes"/> using A-XDR Encoding Scheme(REF)
        /// </remarks>
        /// <param name="Value_Array">Array that contains Encoded data</param>
        /// <param name="EncodingType">_A0C_utf8_string or _A0A_visible_string <see cref="DLMS.Comm.DataTypes"/></param>
        /// <returns>Encode Octet String</returns>
        public static byte[] Encode_UTF8String(char[] Value_Array)
        {
            Encoding enc8 = Encoding.UTF8;
            try
            {
                // Encode Null Octet/Visible String
                if (Value_Array == null)
                    return new byte[] { (byte)DataTypes._A00_Null };

                int data_length = -1;
                byte[] encodedlength = null;
                byte[] _valArray = null;

                data_length = (ushort)Value_Array.Length;
                BasicEncodeDecode.Encode_Length(ref encodedlength, (ushort)Value_Array.Length);

                _valArray = enc8.GetBytes(Value_Array);

                int index = 0;
                List<byte> _encodedRaw = new List<byte>(_valArray.Length + 10);
                foreach (object x in Value_Array)
                {
                    _valArray[index++] = Convert.ToByte(x);
                }

                _encodedRaw.Add((byte)DataTypes._A0C_utf8_string);
                _encodedRaw.AddRange(encodedlength);
                _encodedRaw.AddRange(_valArray);
                return _encodedRaw.ToArray();
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                    throw ex;
                else
                    throw new DLMSEncodingException(String.Format("Error Encoding UTF8 Visible String (Error Code:{0})", DataTypes._A0C_utf8_string,
                        (int)DLMSErrors.ErrorEncoding_Type),
                        "Encode_UTF8String", ex);
            }
        }

        //==========================================================================

        /// <summary>
        /// This helper function encodes String Type _A09_octet_string,_A0A_visible_string(REF)
        /// </summary>
        /// <remarks>
        /// This helper function encodes String Type _A09_octet_string,_A0A_visible_string <see cref="DLMS.Comm.DataTypes"/> using A-XDR Encoding Scheme(REF)
        /// </remarks>
        /// <param name="Value_Array">Array that contains Encoded data</param>
        /// <param name="EncodingType">_A09_octet_string or _A0A_visible_string <see cref="DLMS.Comm.DataTypes"/></param>
        /// <returns>Encode Octet String</returns>
        public static byte[] Encode_OctetString(Array Value_Array, DataTypes EncodingType)
        {
            try
            {
                if (EncodingType == DataTypes._A09_octet_string || EncodingType == DataTypes._A0A_visible_string)
                {
                    //Encode Null Octect/Visible String
                    if (Value_Array == null)
                        return new byte[] { (byte)DataTypes._A00_Null };
                    byte[] encodedlength = null;
                    byte[] _valArray = new byte[Value_Array.Length];
                    int index = 0;
                    List<byte> _encodedRaw = new List<byte>();
                    foreach (object x in Value_Array)
                    {
                        _valArray[index++] = Convert.ToByte(x);
                    }
                    BasicEncodeDecode.Encode_Length(ref encodedlength, (ushort)Value_Array.Length);
                    _encodedRaw.Add((byte)EncodingType);
                    _encodedRaw.AddRange(encodedlength);
                    _encodedRaw.AddRange(_valArray);
                    return _encodedRaw.ToArray();
                }
                else
                {
                    throw new DLMSEncodingException(String.Format("{0} is not supported (Error Code:{1})",
                        EncodingType, (int)DLMSErrors.Invalid_Type_MisMatch), "Encode_OctetString");
                }
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                    throw ex;
                else
                    throw new DLMSEncodingException(String.Format("Error Encoding Octet/Visible String (Error Code:{0})", EncodingType,
                        (int)DLMSErrors.ErrorEncoding_Type),
                        "Common_Encoder_OctectString", ex);
            }
        }

        //==========================================================================
        /// <summary>
        /// This helper function encodes BitString(REF) Type
        /// </summary>
        /// <remarks>
        /// This helper function encodes BitString(REF) Type _A04_bit_string <see cref="DLMS.Comm.DataTypes"/> using A-XDR Encoding Scheme(REF)
        /// The Encoded BitString format is (_A04_bit_string,bitlegth,BitString + Trailing Bits)
        /// </remarks>
        /// <param name="Value_Array">Array that contains Encoded data</param>
        /// <param name="bitLength">Total bit Count<see cref="DLMS.Comm.DataTypes"/></param>
        /// <returns>Encode Bit String</returns>
        public static byte[] Encode_BitString(byte[] Value_Array, int bitLength)
        {
            try
            {
                int DataByteCount = Convert.ToInt32(Math.Ceiling((bitLength / 8.0)));
                List<byte> _encodedRaw = new List<byte>(DataByteCount + 3);
                //Verify BitLength according the array size
                if ((bitLength == 0 && (Value_Array == null || Value_Array.Length == 0)) ||
                     bitLength <= Value_Array.Length * 8)
                {
                    //<_A04_bit_string><bitLength><bits><trailing Bits>
                    _encodedRaw.Add((byte)DataTypes._A04_bit_string);
                    byte[] tLength = null;
                    Encode_Length(ref tLength, (ushort)bitLength);
                    _encodedRaw.AddRange(tLength);
                    if (bitLength > 0)
                    {
                        BitArray TBits = new BitArray(bitLength, false);
                        int bitIndex = 0;
                        int index = 0;
                        int temp = 0;
                        //Copy Each bit From Data Array
                        while (bitIndex < bitLength || index < Value_Array.Length)
                        {
                            if (bitIndex % 8 == 0)   //Copy New Byte
                            {
                                temp = Value_Array[index++];
                            }
                            if ((temp & 0x80) != 0)
                                TBits[bitIndex] = true;
                            temp = temp << 1;       //Right Shift One Bit
                            bitIndex++;
                        }
                        /// Reverse Bits Stored in the Bit Array
                        /// DLMS_Common.Reverse(TBits);
                        byte[] T = new byte[DataByteCount];
                        TBits.CopyTo(T, 0);
                        _encodedRaw.AddRange(T);
                    }
                }
                else
                    throw new DLMSEncodingException(String.Format("Invalid Argument bitLength (Error Code:{0})",
                        (int)DLMSErrors.Invalid_DataLength),
                        "Encode_BitString");
                return _encodedRaw.ToArray();
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                    throw ex;
                else
                    throw new DLMSEncodingException(String.Format("Error Encode_BitString (Error Code:{0})", (int)DLMSErrors.ErrorEncoding_Type),
                        "Common_Encode_BitString", ex);
            }
        }

        //==========================================================================
        /// <summary>
        /// This helper function encodes String Type _A0A_visible_string(REF)
        /// </summary>
        /// <remarks>
        /// This helper function encodes String Type _A0A_visible_string <see cref="DLMS.Comm.DataTypes"/> using A-XDR Encoding Scheme(REF)
        /// </remarks>
        /// <param name="StrValue">The String parameter to Encode</param>
        /// <param name="EncodingType">_A09_octet_string or _A0A_visible_string <see cref="DLMS.Comm.DataTypes"/></param>
        /// <returns>Encoded Octet String</returns>
        public static byte[] Encode_String(String StrValue, DataTypes EncodingType)
        {
            try
            {
                byte[] rawBytes = ASCIIEncoding.ASCII.GetBytes(StrValue);
                return Encode_OctetString(rawBytes, EncodingType);
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                    throw ex;
                else
                    throw new DLMSEncodingException(String.Format("Error Encoding Printable String (Error Code:{0})",
                        (int)DLMSErrors.ErrorEncoding_Type),
                           "Common_Encoder_String", ex);
            }
        }

        //==========================================================================
        /// <summary>
        /// This helper function Encode Type Descriptor for Compact Array DataType Encoding <see cref="DLMS.Comm.DataTypes"/> using A-XDR Decoding(REF) Scheme
        /// </summary>
        /// <param name="typeDescriptor">type</param>
        /// <param name = "Source_Array">Source_Array that contains Encoded data</param>
        /// <param name = "array_traverse">Start index In Source_Array</param>
        /// <param name = "length">Total byte count</param>
        /// <returns> byte[] data decoded</returns>
        public static byte[] Encode_TypeDescripter(this TypeDescriptor typeDescriptor)
        {
            List<byte> Encoder_Raw = new List<byte>();

            string testVar = "Type Descriptor";

            try
            {
                DataTypes dtType = typeDescriptor.TypeTAG;

                // Complex DataType Encoding
                if (dtType == DataTypes._A01_array || dtType == DataTypes._A02_structure)
                {
                    #region DataTypes._A01_array

                    if (dtType == DataTypes._A01_array)
                    {
                        // Validity Check For typeDescriptor
                        if (typeDescriptor.NumberOfElements <= 0 ||
                            typeDescriptor.Elements == null ||
                            typeDescriptor.Elements.Count <= 0)
                        {
                            throw new DLMSEncodingException(String.Format("Error Encoding _A01_array TypeDescripter,Invalid Data (Error Code:{0})",
                                                    (int)DLMSErrors.ErrorEncoding_Type), "Encode_TypeDescripter");
                        }

                        Encoder_Raw.Add((byte)dtType);

                        // Add Array Length As Unsigned16
                        Encoder_Raw.Add((byte)((typeDescriptor.NumberOfElements >> 8) & 0xFF));
                        Encoder_Raw.Add((byte)(typeDescriptor.NumberOfElements & 0xFF));

                        TypeDescriptor ArrayElementType = typeDescriptor.Elements[0];
                        var encode_Data = ArrayElementType.Encode_TypeDescripter();

                        Encoder_Raw.AddRange(encode_Data);
                    }

                    #endregion
                    #region DataTypes._A02_structure

                    else if (dtType == DataTypes._A02_structure)
                    {
                        // Validity Check For typeDescriptor
                        if (typeDescriptor.NumberOfElements <= 0 ||
                            typeDescriptor.Elements == null ||
                            typeDescriptor.Elements.Count <= 0 ||
                            typeDescriptor.Elements.Count < typeDescriptor.NumberOfElements)
                        {
                            throw new DLMSEncodingException(String.Format("Error Encoding _A02_structure TypeDescripter,Invalid Data (Error Code:{0})",
                                                    (int)DLMSErrors.ErrorEncoding_Type), "Encode_TypeDescripter");
                        }

                        Encoder_Raw.Add((byte)dtType);
                        // Add Array Length As Int
                        Encoder_Raw.Add((byte)(typeDescriptor.NumberOfElements & 0xFF));

                        int indexer = 0;
                        // Encode Sequence Of Structural Elements
                        for (int NoOfElement = 1; (typeDescriptor.NumberOfElements > 0 &&
                                                   indexer < typeDescriptor.Elements.Count); NoOfElement++)
                        {
                            TypeDescriptor ArrayElementType = typeDescriptor.Elements[indexer++];

                            var encode_Data = ArrayElementType.Encode_TypeDescripter();
                            Encoder_Raw.AddRange(encode_Data);
                        }
                    }

                    #endregion
                }
                // Simple DataType Encoding Rule
                else
                {
                    Encoder_Raw.Add((byte)dtType);
                }

                return Encoder_Raw.ToArray<byte>();
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                    throw ex;
                else
                    throw new DLMSEncodingException(String.Format("Error Encoding TypeDescripter (Error Code:{0})",
                                                    (int)DLMSErrors.ErrorEncoding_Type), "Encode_TypeDescripter", ex);
            }
        }

        //==========================================================================

        /// <summary>
        /// This helper encoder function encodes Array of Common Numeric <see cref="DLMS.Comm.DataTypes"/> in DLMS_COSEM Specification.
        /// </summary>
        /// <remarks>
        /// This helper encoder function Encodes following Array of Common Numeric <see cref="DLMS.Comm.DataTypes"/> ,
        /// _A14_long_64,_A15_long_64_unsigned,_A05_double_long,_A06_double_long_unsigned,_A10_long,_A11_unsigned,_A0F_integer and
        /// _A16_enum. This function encodes common DataTypes using A-XDR Encoding Scheme(REF),See Glossary And Term Section
        /// </remarks>
        /// <param name="sourceArray">Array parameter</param>
        /// <param name="dtType">Parameter Encoding Type,<see cref="DLMS.Comm.DataTypes"/></param>
        /// <returns>Encoded Value Array</returns>
        public static byte[] Encode_Array(Array sourceArray, DataTypes dtType)
        {
            try
            {
                List<byte> destinatioArray = new List<byte>(50);
                if (sourceArray == null)
                    throw new DLMSEncodingException("Source Array Is Null Type", "Common_Encode_Array");
                byte[] t = null;
                destinatioArray.Add((byte)DataTypes._A01_array);
                Encode_Length(ref t, (ushort)sourceArray.Length);
                destinatioArray.AddRange(t);
                switch (dtType)
                {
                    case DataTypes._A00_Null:
                    case DataTypes._A03_boolean:
                    case DataTypes._A16_enum:
                    case DataTypes._A0F_integer:
                    case DataTypes._A11_unsigned:
                    case DataTypes._A05_double_long:
                    case DataTypes._A06_double_long_unsigned:
                    case DataTypes._A10_long:
                    case DataTypes._A12_long_unsigned:
                    case DataTypes._A14_long_64:
                    case DataTypes._A15_long_64_unsigned:
                        for (int index = 0; index < sourceArray.Length; index++)
                        {
                            t = Intelligent_Data_Encoder(dtType, (ValueType)sourceArray.GetValue(index));
                            destinatioArray.AddRange(t);
                        }
                        break;
                    default:
                        throw new DLMSEncodingException(String.Format("{0} is not supported,(Error Code:{1})",
                           dtType, (int)DLMSErrors.Invalid_TypeNotIncluded), "Common_Encoder_Array");
                }
                return destinatioArray.ToArray<byte>();
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                    throw ex;
                else
                    throw new DLMSEncodingException(String.Format("Error Encoding {0} Type Array (Error Code:{0})",
                        (int)DLMSErrors.ErrorEncoding_Type), "Common_Encoder_Array", ex);
            }
        }

        //==========================================================================

        /// <summary>
        /// This helper encoder function encodes Array of ICustomStructure Custom_Struct <see cref="DLMS.Comm.ICustomStructure"/> Objects
        /// </summary>
        /// <param name="ValueArray">Array of Element type ICustomStructure</param>
        /// <returns>Encoded Array</returns>
        public static byte[] Encode_Array(ICustomStructure[] ValueArray)
        {
            try
            {
                List<byte> destinatioArray = new List<byte>(50);
                if (ValueArray == null)
                    throw new DLMSEncodingException("Source Array Is Null Type", "Common_Encode_Array");
                byte[] t = null;
                destinatioArray.Add((byte)DataTypes._A01_array);
                Encode_Length(ref t, (ushort)ValueArray.Length);
                destinatioArray.AddRange(t);
                foreach (var item in ValueArray)
                {
                    if (item == null)
                        throw new DLMSEncodingException("ICustomStructure object reference Is Null", "Common_Encode_Array");
                    t = item.Encode_Data();
                    destinatioArray.AddRange(t);
                }
                return destinatioArray.ToArray<byte>();
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                    throw ex;
                else
                    throw new DLMSEncodingException(String.Format("Error Encoding ICustomStructure Array(Error Code:{0})",
                        (int)DLMSErrors.ErrorEncoding_Type), "Common_Encoder_Array", ex);
            }
        }

        //==========================================================================
        /// <summary>
        /// This helper function encodes length using A-XDR Encoding(REF) Scheme.
        /// See also <see cref="Decode_Length(byte[],ref int, int)"/>
        /// </summary>
        /// <param name="Length_Array">Array to append length</param>
        /// <param name="Length_to_encode">Length parameter to Encode</param>
        static public void Encode_Length(ref byte[] Length_Array, ushort Length_to_encode)
        {
            if (Length_to_encode < 128)
            {
                Length_Array = new byte[1];
                Length_Array[0] = (byte)(Length_to_encode);
                return;
            }
            else if (Length_to_encode < 256)
            {
                Length_Array = new byte[2];
                Length_Array[0] = 0x81;
                Length_Array[1] = (byte)(Length_to_encode);
                return;
            }
            else
            {
                Length_Array = new byte[3];
                Length_Array[0] = 0x82;
                Length_Array[1] = (byte)((UInt16)(Length_to_encode) >> 8);
                Length_Array[2] = (byte)(Length_to_encode);
            }
            return;
        }


        /// <summary>
        /// This helper function encodes length using A-XDR Encoding(REF) Scheme.
        /// See also <see cref="Decode_Length(byte[],ref int, int)"/>
        /// </summary>
        /// <param name="Length_Array">Array to append length</param>
        /// <param name="Length_to_encode">Length parameter to Encode</param>
        static public void Encode_Length(ref byte[] Length_Array, int Length_to_encode)
        {
            if (Length_to_encode < 128)
            {
                Length_Array = new byte[1];
                Length_Array[0] = (byte)(Length_to_encode);
                return;
            }
            else if (Length_to_encode <= byte.MaxValue)
            {
                Length_Array = new byte[2];
                Length_Array[0] = 0x81;
                Length_Array[1] = (byte)(Length_to_encode);
                return;
            }
            else if (Length_to_encode <= ushort.MaxValue)
            {
                Length_Array = new byte[3];
                Length_Array[0] = 0x82;
                Length_Array[1] = (byte)((UInt16)(Length_to_encode) >> 8);
                Length_Array[2] = (byte)(Length_to_encode);
            }
            else
            {
                Length_Array = new byte[5];
                Length_Array[0] = 0x84;
                Length_Array[1] = (byte)(Length_to_encode >> 24);
                Length_Array[2] = (byte)(Length_to_encode >> 16);
                Length_Array[3] = (byte)(Length_to_encode >> 8);
                Length_Array[4] = (byte)(Length_to_encode);
            }
            return;
        }
        //==========================================================================
        /// <summary>
        /// Appends Encoded Length at the start of Array passed to it
        /// </summary>
        /// <param name="Source_Array">The array whose length is to be appended at the start</param>
        /// <returns>Returns final array with its length appended at the start</returns>
        static public byte[] Append_Length_to_Start(byte[] Source_Array)
        {
            UInt16 Length_of_Array = (UInt16)Source_Array.Length;
            byte[] Encoded_Length = new byte[1];

            Encode_Length(ref Encoded_Length, Length_of_Array);

            return DLMS_Common.Append_to_Start(Source_Array, Encoded_Length);
        }

        //==========================================================================
        /// <summary>
        /// This helper method Convert IPAddress of type <see cref="System.Net.IPAddress"/> to number IPv number
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static long IPAddressToLong(System.Net.IPAddress address)
        {
            byte[] byteIP = address.GetAddressBytes();

            long ip = (long)byteIP[0] << 24;
            ip += (long)byteIP[1] << 16;
            ip += (long)byteIP[2] << 8;
            ip += (long)byteIP[3];
            return ip;
        }

        //==========================================================================
        /// <summary>
        /// This helper method ToString parameter to IPv string representation
        /// </summary>
        /// <param name="IPAddress">IPv4 parameter</param>
        /// <returns>String</returns>
        public static string LongToIPAddressString(long IPAddress)
        {
            byte[] ip = new byte[4];
            ip[0] = (byte)(IPAddress / 256 / 256 / 256);
            ip[1] = (byte)((IPAddress - ip[0]) / 256);
            ip[2] = (byte)((IPAddress - ip[1]) / 256);
            ip[3] = (byte)((IPAddress - ip[2]) / 256);
            string IP = String.Format("{0:000}", ip[0]);

            IP += String.Format("{0:000}", ip[1]);
            IP += String.Format("{0:000}", ip[2]);
            IP += String.Format("{0:000}", ip[3]);
            return IP;
        }

        //==========================================================================
        /// <summary>
        /// This helper method encode <see cref="DLMS.Comm.Unit_Scaler"/> Struct(REF)
        /// </summary>
        /// <remarks>
        /// This helper method encode <see cref="DLMS.Comm.Unit_Scaler"/> Struct(REF),
        /// these Unit_Scaler struct is applicable in IC_3 Register(REF) <see cref="DLMS.Class_3"/>
        /// and IC_4 Extended Register <see cref="DLMS.Class_4"/> data types.
        /// </remarks>
        /// <param name="scalar">Parameter Scaler</param>
        /// <param name="unit">Parameter Unit</param>
        /// <returns>Encoded Array</returns>
        public static byte[] encode_class_3_attribute_3(int scalar, units unit)
        {
            byte[] byte_array = new byte[5];
            byte_array[0] = 0x02;               // Number of elements of the structure
            byte_array[1] = 0x0F;               // tag of signed
            byte_array[2] = (byte)scalar;       // passed scaLAR
            byte_array[3] = 0x16;               // tag of enum
            byte_array[4] = (byte)unit;         // passed enum
            return byte_array;
        }

        /// <summary>
        /// This helper method adjust Val Parameter according to scaler parameter.
        /// </summary>
        /// <remarks>
        /// This helper method apply arithmetic mulitplication/division on parameter val as
        /// 10;s Pow to scaler Parameter. RetVal = Val * 10^scaler
        /// </remarks>
        /// <param name="Val">Val parameter to adjust</param>
        /// <param name="scaler">scaler parameter to apply</param>
        /// <returns>Adjusted Value</returns>
        public static ValueType ValueUnitScalerAdjustment(ValueType Val, sbyte scaler)
        {
            try
            {
                ValueType Value = null;
                if (Val == null)
                    return null;
                // multiply with scaler
                long multiplier = Convert.ToInt64(Math.Pow(10, (byte)Math.Abs(scaler)));
                //Algorithm Revised
                if (scaler != 0)                        //Compute Value With Scaler
                {
                    if (Val is float || Val is Double)
                    {
                        double temporary_value = Convert.ToDouble(Val);
                        if (scaler > 0)
                        {
                            temporary_value = temporary_value * multiplier;
                            //for (byte i = 0; i < multiplier; i++) temporary_value = temporary_value * 10;
                        }
                        else if (scaler < 0)
                        {
                            //for (byte i = 0; i < multiplier; i++) temporary_value /= 10;
                            temporary_value = temporary_value / multiplier;
                        }
                        Value = temporary_value;
                    }
                    else
                    {
                        long temporary_value = Convert.ToInt64(Val);
                        if (scaler > 0)
                        {
                            Value = temporary_value * multiplier;
                            //for (byte i = 0; i < multiplier; i++) temporary_value = temporary_value * 10;
                        }
                        else if (scaler < 0)
                        {
                            //for (byte i = 0; i < multiplier; i++) temporary_value /= 10;
                            Value = temporary_value / (double)multiplier;
                        }
                    }
                }
                else
                    Value = Val;
                return Value;
            }
            catch (Exception ex)
            {
                throw new DLMSException("Error adjusting value according to Scaler_Unit Structure_BasicEncodeDecode_UnitScalerAdjustment", ex);
            }
        }


        public static UInt64 FromBCDToExtUInt64(byte[] bcds, uint startOf, uint nBytes, bool isPackedBCD = true)
        {
            UInt64 result = 0;
            uint i = 0;

            byte signDigit = 0;
            byte highNible = 0;
            byte lowNible = 0;

            // isPackedBCD 
            // Either Two BCD Digits Stored on Single Byte or Two
            uint maxLimit = (!isPackedBCD) ? nBytes * 2 : nBytes;

            for (i = 0; i < maxLimit;)
            {
                result *= 100;

                unchecked
                {
                    if (isPackedBCD)
                        highNible = (byte)(bcds[startOf + i] >> 4);
                    else
                        highNible = bcds[startOf + i + 1];

                    lowNible = (byte)(bcds[startOf + i] & 0x0F);
                }

                // High Nibble Processing
                if (highNible >= 0 && highNible < 0x0A)
                    result += (UInt64)(10 * highNible);
                else
                    signDigit = highNible;

                // Low Nibble Processing
                if (lowNible >= 0 && lowNible < 0x0A)
                    result += lowNible;
                else
                    signDigit = lowNible;

                if (!isPackedBCD)
                    i += 2;
                else
                    i++;

            }

            //    8 4 2 1    Sign Notes
            // A  1 0 1 0 	+ 	 
            // B  1 0 1 1 	- 	 
            // C  1 1 0 0 	+ 	Preferred
            // D  1 1 0 1 	- 	Preferred
            // E  1 1 1 0 	+ 	 
            // F  1 1 1 1 	+ 	Unsigned

            // Sign BCD Digit Check
            // if ((signDigit == 0x0B || signDigit == 0x0D) && signDigit > 0)
            //    result *= -1;

            return (result);
        }

        public static byte[] IntToBCD(byte[] input, bool isPackedBCD = true)
        {
            byte[] outArr = null;

            if (isPackedBCD)
                outArr = new byte[Convert.ToInt32(Math.Ceiling((double)input.Length / 02))];
            else
                outArr = new byte[Convert.ToInt32(Math.Ceiling((double)input.Length))];

            // Handle the case of an odd number in which
            // a zero should be added at the beginning
            if (input.Length % 2 != 0)
            {
                // Use a temp array to expand the old one, you can use lists or 
                // another data-structure if you wish to
                byte[] newInput = new byte[input.Length + 1];
                Array.Copy(input, 0, newInput, 1, input.Length);
                newInput[0] = 0;
                input = newInput;
                // Dispose the temp array
                newInput = null;
            }

            for (int i = 0; i < outArr.Length; i++)
            {
                if (isPackedBCD)
                {
                    outArr[i] = (byte)(input[(i * 2)] << 4);
                    outArr[i] |= (byte)(input[(i * 2) + 1]);
                }
                else
                {
                    outArr[i] = (byte)(input[i]);
                }
            }

            return outArr;
        }

        public static int FindNumberLength(ulong number)
        {
            return Convert.ToInt32(Math.Floor(Math.Log(number, 10)) + 1);
        }

        public static ulong FindNumberDivisor(ulong number)
        {
            return Convert.ToUInt64(Math.Pow(10, FindNumberLength(number) - 1));
        }

        public static byte[] FindNumberElements(ulong number)
        {
            byte[] elements = new byte[FindNumberLength(number)];
            ulong divisor = FindNumberDivisor(number);

            for (int i = 0; i < elements.Length; i++)
            {
                elements[i] = Convert.ToByte(number / (ulong)divisor);
                number %= (ulong)divisor;
                divisor /= 10;
            }

            return elements;
        }

    }
}



//// Code Revised & Modified
//// Revision # 1.1 Tuesday 09 October 2012
//// Revision # 1.2 Tuesday 15 September 2014

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Net;
//using System.Collections;
//using DLMS.Comm;

//namespace DLMS
//{
//    /// <summary>
//    /// This DLMS_Common static class define Set of static helper method used in 
//    /// various methods present in DLMS_COSEM Interface Classes(IC),Helper Classes
//    /// and other Misc Classes Defined in Code
//    /// </summary>
//    public static class BasicEncodeDecode
//    {
//        /// <summary>
//        /// Minimum DateTime Value being accepted in Encode/Decode Functions
//        /// </summary>
//        public static DateTime MinDateTime;

//        /// <summary>
//        /// Static Default Constructor
//        /// </summary>
//        static BasicEncodeDecode()
//        {
//            //January 1, 1753 
//            MinDateTime = new DateTime(1753, 1, 1);
//        }


//        /// <summary>
//        /// This helper decoder function decodes most Common Numeric <see cref="DLMS.Comm.DataTypes"/> in DLMS_COSEM Specification.
//        /// See also <see cref="Intelligent_Data_Decoder(ref byte[], ref int, int)"/>
//        /// </summary>
//        public static ValueType Intelligent_Data_Decoder(ref byte[] Source_Array, ref int Source_Index)
//        {
//            return Intelligent_Data_Decoder(ref Source_Array, ref Source_Index, Source_Array.Length);
//        }

//        /// <summary>
//        /// This helper decoder function decodes most Common Numeric <see cref="DLMS.Comm.DataTypes"/> in DLMS_COSEM Specification.
//        /// </summary>
//        /// <remarks>
//        /// This helper decoder function decodes following Common Numeric <see cref="DLMS.Comm.DataTypes"/> ,
//        /// _A14_long_64,_A15_long_64_unsigned,_A05_double_long,_A06_double_long_unsigned,_A10_long,_A11_unsigned,_A0F_integer and
//        /// _A16_enum. This function decodes common DataTypes using A-XDR Decoding Scheme(REF),See Glossary and Term Section.
//        /// </remarks>
//        /// <param name="Source_Array">Source_Array that contains Encoded data</param>
//        /// <param name="Source_Index">Start index In Source_Array</param>
//        /// <param name="length">Total byte count</param>
//        /// <returns><see cref="System.ValueType"/> BaseType for DOTNET Common Numeric DataTypes</returns>
//        public static ValueType Intelligent_Data_Decoder(ref byte[] Source_Array, ref int Source_Index, int length)
//        {
//            DataTypes type_of_data = DataTypes._A00_Null;
//            try
//            {
//                type_of_data = (DataTypes)Source_Array[Source_Index++];
//                ValueType to_return;
//                switch (type_of_data)
//                {
//                    case DataTypes._A14_long_64:
//                        {
//                            long temp =
//                            ((long)Source_Array[Source_Index + 7]) + (((long)Source_Array[Source_Index + 6]) << 8) +
//                            (((long)Source_Array[Source_Index + 5] << 16)) + (((long)Source_Array[Source_Index + 4]) << 24) +
//                            (((long)Source_Array[Source_Index + 3]) << 32) + (((long)Source_Array[Source_Index + 2]) << 40) +
//                            (((long)(Source_Array[Source_Index + 1]) << 48) + (((long)Source_Array[Source_Index]) << 56));
//                            Source_Index += 8;
//                            to_return = temp;
//                            break;
//                        }
//                    case DataTypes._A15_long_64_unsigned:
//                        {
//                            ulong temp =
//                             ((ulong)Source_Array[Source_Index + 7]) + (((ulong)Source_Array[Source_Index + 6]) << 8) +
//                            (((ulong)Source_Array[Source_Index + 5] << 16)) + (((ulong)Source_Array[Source_Index + 4]) << 24) +
//                            (((ulong)Source_Array[Source_Index + 3]) << 32) + (((ulong)Source_Array[Source_Index + 2]) << 40) +
//                            (((ulong)(Source_Array[Source_Index + 1]) << 48) + (((ulong)Source_Array[Source_Index]) << 56));
//                            Source_Index += 8;
//                            to_return = temp;
//                            break;
//                        }
//                    case DataTypes._A05_double_long:
//                        {
//                            int temp = (int)((Source_Array[Source_Index + 3]) + (Source_Array[Source_Index + 2] << 8) +
//                            (Source_Array[Source_Index + 1] << 16) + (Source_Array[Source_Index] << 24));
//                            Source_Index += 4;
//                            to_return = temp;
//                            break;
//                        }
//                    case DataTypes._A06_double_long_unsigned:
//                        {
//                            uint temp = (uint)((Source_Array[Source_Index + 3]) + (Source_Array[Source_Index + 2] << 8) +
//                            (Source_Array[Source_Index + 1] << 16) + (Source_Array[Source_Index] << 24));
//                            Source_Index += 4;
//                            to_return = temp;
//                            break;
//                        }
//                    case DataTypes._A10_long:
//                        {
//                            short temp = (short)((Source_Array[Source_Index + 1]) + (Source_Array[Source_Index] << 8));
//                            Source_Index += 2;
//                            to_return = temp;
//                            break;
//                        }
//                    case DataTypes._A12_long_unsigned:
//                        {
//                            ushort temp = (ushort)((Source_Array[Source_Index + 1]) + (Source_Array[Source_Index] << 8));
//                            Source_Index += 2;
//                            to_return = temp;
//                            break;
//                        }
//                    case DataTypes._A0D_bcd:
//                        {
//                            int Length_Array = BasicEncodeDecode.Decode_Length(Source_Array, ref Source_Index);

//                            if (Length_Array < 0 || Length_Array > 16)
//                                throw new Exception(String.Format("BCD Array Length Invalid, operation not supported (Error Code:{0})",
//                               (int)DLMSErrors.Invalid_DataValue));

//                            // Make Room For Coming Array
//                            byte[] Value_Array = new byte[Length_Array];
//                            Buffer.BlockCopy(Source_Array, Source_Index, Value_Array, 0, Value_Array.Length);
//                            Source_Index += Length_Array;

//                            ulong temp = BasicEncodeDecode.FromBCDToExtUInt64(Value_Array, (uint)0, (uint)Value_Array.Length, true);

//                            #region // Range_ValueComparision

//                            if (temp >= byte.MinValue && temp <= byte.MaxValue)
//                                to_return = Convert.ToByte(temp);
//                            else if (temp >= ushort.MinValue && temp <= ushort.MaxValue)
//                                to_return = Convert.ToUInt16(temp);
//                            else if (temp >= uint.MinValue && temp <= uint.MaxValue)
//                                to_return = Convert.ToUInt32(temp);
//                            else if (temp >= ulong.MinValue && temp <= ulong.MaxValue)
//                                to_return = Convert.ToUInt64(temp);
//                            else
//                                to_return = temp;

//                            #endregion

//                            break;
//                        }
//                    case DataTypes._A11_unsigned:
//                    case DataTypes._A0F_integer:
//                    case DataTypes._A16_enum:
//                    case DataTypes._A03_boolean:
//                        to_return = Source_Array[Source_Index];
//                        Source_Index++;
//                        break;
//                        throw new Exception(String.Format("data type DataTypes._A00_Null not included yet_Common.Intelligent_Data Decoder (Error Code:{0})",
//                               (int)DLMSErrors.Invalid_TypeNotIncluded));
//                    default:
//                        throw new Exception(String.Format("data type not included yet_Common.Intelligent_Data Decoder (Error Code:{0})",
//                            (int)DLMSErrors.Invalid_TypeNotIncluded));
//                }
//                return to_return;
//            }
//            catch (Exception ex)
//            {
//                if (ex is DLMSDecodingException)
//                    throw ex;
//                else
//                    throw new DLMSDecodingException(String.Format("Error Decoding {0} Type Value (Error Code:{1})",
//                        (int)type_of_data, DLMSErrors.Invalid_TypeNotIncluded),
//                        "Intelligent_Data_Decoder", ex);
//            }
//        }

//        // ==========================================================================
//        // public static void Decode_Array(byte[] Data, ref ushort array_traverser, ref Array decodedObjects)
//        // {
//        //     byte currentChar = Data[array_traverser++];
//        //     if (currentChar != (byte)DataTypes._A01_array)
//        //         throw new DLMSDecodingException(String.Format("{0} data type is expected", DataTypes._A01_array),
//        //          "Decode_Array_BasicEncodeDecode");
//        //     ushort arrlength = BasicEncodeDecode.Decode_Length(Data, ref array_traverser);
//        // }
//        //==========================================================================
//        //==========================================================================
//        /// <summary>
//        /// This helper decoder function decodes Array Type of most Common Numeric <see cref="DLMS.Comm.DataTypes"/> in DLMS_COSEM Specification. 
//        /// See Also <see cref="Intelligent_Data_Decoder(ref byte[], ref int, int)"/>
//        /// </summary>
//        /// <param name="Source_Array">Source_Array that contains Encoded data</param>
//        /// <param name="Source_Index">Start index In Source_Array</param>
//        /// <param name="dtType">DataType of Array Element<see cref="DLMS.Comm.DataTypes"/></param>
//        /// <param name="length">Total byte count</param>
//        /// <returns>Array Of <see cref="System.ValueType"/>, BaseType for DOTNET Common Numeric DataTypes</returns>
//        public static ValueType[] Decode_Array(ref byte[] Source_Array, ref int Source_Index, DataTypes dtType, int length)
//        {
//            try
//            {
//                List<ValueType> destinatioArray = new List<ValueType>();
//                if (Source_Array == null)
//                    throw new DLMSDecodingException("Error Decoding Source Array Is Null Type", "Common_Decode_Array");
//                byte current_Char = Source_Array[Source_Index++];
//                if (current_Char != (byte)DataTypes._A01_array)
//                {
//                    throw new DLMSDecodingException(String.Format("Error Decoding {0} type is expected (Error Code:{1})",
//                        DataTypes._A01_array, (int)DLMSErrors.Invalid_Type_MisMatch),
//                        "Common_Decode_Array");
//                }

//                int Decode_Length = BasicEncodeDecode.Decode_Length(Source_Array, ref Source_Index);
//                for (int index = 0; index < Decode_Length; index++)
//                {
//                    DataTypes dataType = (DataTypes)Source_Array[Source_Index];
//                    if (dtType != dataType)
//                        throw new DLMSDecodingException(String.Format("Error Decoding {0} type is expected (Error Code:{1})",
//                            dtType, (int)DLMSErrors.Invalid_Type_MisMatch), "Common_Decode_Array");

//                    ValueType t = Intelligent_Data_Decoder(ref Source_Array, ref  Source_Index, Source_Array.Length);
//                    destinatioArray.Add(t);
//                }
//                return destinatioArray.ToArray<ValueType>();
//            }
//            catch (Exception ex)
//            {
//                if (ex is DLMSDecodingException)
//                    throw ex;
//                else
//                    throw new DLMSDecodingException(String.Format("Error Decoding {0} Type Array (Error Code:{1})", dtType,
//                        (int)DLMSErrors.ErrorDecoding_Type), "Common_Decoder_Array", ex);
//            }
//        }

//        //==========================================================================
//        /// <summary>
//        /// This decoder function decode simple data types Compact Array<see cref="DLMS.Comm.DataTypes"/> in DLMS_COSEM Specification. 
//        /// See Also <see cref="Intelligent_Data_Decoder(ref byte[], ref int, int)"/>
//        /// </summary>
//        /// <param name="Source_Array">Source_Array that contains Encoded data</param>
//        /// <param name="Source_Index">Start index In Source_Array</param>
//        /// <param name="dtType">DataType of Array Element <see cref="DLMS.Comm.DataTypes"/></param>
//        /// <param name="length">Total byte count</param>
//        /// <returns>Array Of <see cref="System.ValueType"/>, BaseType for DOTNET Common Numeric DataTypes</returns>
//        public static List<object> Decode_SimpleCompactArray(ref byte[] Source_Array, ref int Source_Index,
//                                                ref TypeDescriptor ContentDescription, int length)
//        {
//            List<object> decoded_data = null;
//            List<byte> sourceArray = new List<byte>();
//            List<byte> destinationArray = new List<byte>();

//            byte[] TmpArray = null;

//            try
//            {
//                #region Initial_Work

//                if (Source_Array == null || Source_Array.Length < length)
//                    throw new DLMSDecodingException("Error Decoding Source Array Is Null Type", "Parse_CompactArray");

//                byte current_Char = Source_Array[Source_Index++];
//                if (current_Char != (byte)DataTypes._A13_compact_arry)
//                {
//                    throw new DLMSDecodingException(String.Format("Error Decoding {0} type is expected (Error Code:{1})",
//                        DataTypes._A13_compact_arry, (int)DLMSErrors.Invalid_Type_MisMatch), "Parse_CompactArray");
//                }

//                #endregion
//                #region // Process_TypeDescriptor

//                ContentDescription = new TypeDescriptor();
//                ContentDescription.Decode_TypeDescripter(Source_Array, ref Source_Index, length);

//                List<TypeDescriptor> Content_DescriptorStruct = new List<TypeDescriptor>();
//                {
//                    var descriptor = ContentDescription.All();
//                    foreach (var type in descriptor)
//                    {
//                        Content_DescriptorStruct.Add(type);
//                    }
//                }

//                #endregion
//                #region // Copy Data Contents

//                try
//                {
//                    int dataLength = Decode_Length(Source_Array, ref Source_Index);

//                    // Copy Data Contents
//#if NETFX_45 || NETFX_451

//                    // Console.WriteLine("NET_45 was set");
//                    var TArraySeg = new ArraySegment<byte>(Source_Array, Source_Index, dataLength);
//#endif
//#if NETFX_35 || NETFX_30

//                    // Console.WriteLine("NET_40 was set");
//                    var TArraySeg = new byte[dataLength];
//                    Buffer.BlockCopy(Source_Array, Source_Index, TArraySeg, 0, TArraySeg.Length);
//#endif

//                    sourceArray.AddRange(TArraySeg);
//                }
//                catch (Exception ex)
//                {
//                    throw new DLMSDecodingException("Error Decoding Compact Array,Invalid Array Data Length", "Parse_CompactArray");
//                }

//                #endregion

//                int src_Index = 0;
//                int type_Index = 0;
//                // Parse Compact Array
//                // SimpleDataType Decoding Work For BasicEncoderDecoder Work
//                var elment_Count = BasicEncodeDecode.ParserHelper_CompactArray(destinationArray, sourceArray,
//                                                                               ref src_Index, Content_DescriptorStruct, ref type_Index);

//                // Initialize dtType
//                DataTypes dtType = DataTypes._A00_Null, parentType = DataTypes._A00_Null;
//                if (Content_DescriptorStruct[0] != null)
//                {
//                    dtType = Content_DescriptorStruct[0].TypeTAG;
//                    if (Content_DescriptorStruct[0].Parent != null)
//                    {
//                        parentType = ((TypeDescriptor)Content_DescriptorStruct[0].Parent).TypeTAG;
//                    }
//                }

//                TmpArray = destinationArray.ToArray();
//                src_Index = 0;
//                decoded_data = Decode_SimpleArray(ref TmpArray, ref src_Index, elment_Count, dtType, TmpArray.Length);

//                return decoded_data;
//            }
//            catch (Exception ex)
//            {
//                if (ex is DLMSDecodingException)
//                    throw ex;
//                else
//                    throw new DLMSDecodingException(String.Format("Error occurred while Decoding DataType {0} (Error Code:{1})",
//                         (ContentDescription != null) ? ContentDescription.ToString() : "None",
//                         (int)DLMSErrors.ErrorDecoding_Type), "Decode_SimpleCompactArray", ex);
//            }
//        }

//        //==========================================================================
//        /// <summary>
//        /// This decoder function decode simple data types Array<see cref="DLMS.Comm.DataTypes"/> in DLMS_COSEM Specification. 
//        /// See Also <see cref="Intelligent_Data_Decoder(ref byte[], ref int, int)"/>
//        /// </summary>
//        /// <param name="Source_Array">Source_Array that contains Encoded data</param>
//        /// <param name="Source_Index">Start index In Source_Array</param>
//        /// <param name="dtType">DataType of Array Element <see cref="DLMS.Comm.DataTypes"/></param>
//        /// <param name="length">Total byte count</param>
//        /// <returns>Array Of <see cref="System.ValueType"/>, BaseType for DOTNET Common Numeric DataTypes</returns>
//        public static List<object> Decode_SimpleArray(ref byte[] Source_Array, ref int Source_Index, int elment_Count, DataTypes dtType, int length)
//        {
//            List<object> decoded_data = null;

//            try
//            {
//                #region Initial_Work

//                if (Source_Array == null || Source_Array.Length < length)
//                    throw new DLMSDecodingException("Error Decoding Source Array Is Null Type", "Decode_SimpleArray");

//                #endregion

//                decoded_data = new List<object>((elment_Count > 0) ? elment_Count : 100);

//                // Encode_Data  
//                while (elment_Count > 0 &&
//                       Source_Index < Source_Array.Length)
//                {
//                    if (dtType == DataTypes._A01_array || dtType == DataTypes._A02_structure)
//                    {
//                        throw new DLMSDecodingException(String.Format("Error Decoding _A01_array or _A02_structure data types (Error Code:{0})",
//                                                        (int)DLMSErrors.ErrorDecoding_Type), "Decode_SimpleCompactArray");
//                    }

//                    #region // FIX_Length Numerical DataType

//                    else if (dtType == DataTypes._A03_boolean || dtType == DataTypes._A05_double_long || dtType == DataTypes._A06_double_long_unsigned ||
//                             dtType == DataTypes._A07_floating_point || dtType == DataTypes._A0F_integer || dtType == DataTypes._A10_long ||
//                             dtType == DataTypes._A11_unsigned || dtType == DataTypes._A12_long_unsigned || dtType == DataTypes._A14_long_64 || dtType == DataTypes._A15_long_64_unsigned ||
//                             dtType == DataTypes._A16_enum || dtType == DataTypes._A23_Float32 || dtType == DataTypes._A24_Float64 || dtType == DataTypes._A0D_bcd)
//                    {
//                        var valueType = BasicEncodeDecode.Intelligent_Data_Decoder(ref Source_Array, ref Source_Index);
//                        // Todo : Debug Type Decode Work
//                        decoded_data.Add(valueType);
//                    }

//                    #endregion
//                    #region // Simple Variable Length DataType

//                    else if (dtType == DataTypes._A04_bit_string || dtType == DataTypes._A09_octet_string ||
//                             dtType == DataTypes._A0A_visible_string ||
//                             dtType == DataTypes._A19_datetime || dtType == DataTypes._A1A_date || dtType == DataTypes._A1B_time)
//                    {
//                        switch (dtType)
//                        {
//                            case DataTypes._A09_octet_string:
//                                {
//                                    var octSTRData = BasicEncodeDecode.Decode_OctectString(Source_Array, ref Source_Index);

//                                    decoded_data.Add(octSTRData);
//                                    break;
//                                }
//                            case DataTypes._A0A_visible_string:
//                                {
//                                    var vsbSTRData = BasicEncodeDecode.Decode_String(Source_Array, ref Source_Index);

//                                    decoded_data.Add(vsbSTRData);
//                                    break;
//                                }
//                            case DataTypes._A04_bit_string:
//                                {
//                                    int bitLength = 0;
//                                    var vsbSTRData = BasicEncodeDecode.Decode_BitString(Source_Array, ref Source_Index, ref bitLength);

//                                    List<byte> bitSTR = new List<byte>(32);
//                                    var lengthData = BitConverter.GetBytes(bitLength);

//                                    bitSTR.AddRange(lengthData);
//                                    bitSTR.AddRange(vsbSTRData);

//                                    decoded_data.Add(bitSTR);
//                                    break;
//                                }
//                            #region date_time

//                            case DataTypes._A19_datetime:
//                            case DataTypes._A1A_date:
//                            case DataTypes._A1B_time:
//                                {
//                                    byte[] octSTRData = null;
//                                    // Skip DateTime Type TAGs
//                                    Source_Index++;

//                                    StDateTime StdateTime = new StDateTime();

//                                    // Decode DateTime
//                                    if (dtType == DataTypes._A19_datetime)
//                                    {
//                                        octSTRData = new byte[12];
//                                        Buffer.BlockCopy(Source_Array, Source_Index, octSTRData, 0, octSTRData.Length);
//                                        Source_Index += octSTRData.Length;

//                                        StdateTime.DecodeDateTime(octSTRData);
//                                    }
//                                    // Decode Date
//                                    else if (dtType == DataTypes._A1A_date)
//                                    {
//                                        octSTRData = new byte[05];
//                                        Buffer.BlockCopy(Source_Array, Source_Index, octSTRData, 0, octSTRData.Length);
//                                        Source_Index += octSTRData.Length;

//                                        StdateTime.DecodeDate(octSTRData);
//                                    }
//                                    // Decode Time
//                                    else if (dtType == DataTypes._A1B_time)
//                                    {
//                                        octSTRData = new byte[04];
//                                        Buffer.BlockCopy(Source_Array, Source_Index, octSTRData, 0, octSTRData.Length);
//                                        Source_Index += octSTRData.Length;

//                                        StdateTime.DecodeTime(octSTRData);
//                                    }

//                                    decoded_data.Add(StdateTime);
//                                    break;
//                                }

//                            #endregion
//                            default:
//                                throw new DLMSDecodingException(String.Format("Error Decoding DataType,Decoder not implemented {0} (Error Code:{1})",
//                                                                dtType, (int)DLMSErrors.ErrorDecoding_Type), "Decode_SimpleArray");
//                        }
//                    }

//                    #endregion

//                    // Other DataType not included yet
//                    else
//                    {
//                        throw new DLMSDecodingException(String.Format("Error Decoding DataType,Decoder not implemented {0} (Error Code:{1})",
//                        dtType, (int)DLMSErrors.ErrorDecoding_Type), "Decode_SimpleArray");
//                    }

//                    elment_Count--;
//                }

//                return decoded_data;
//            }
//            catch (Exception ex)
//            {
//                if (ex is DLMSDecodingException)
//                    throw ex;
//                else
//                    throw new DLMSDecodingException(String.Format("Error occurred while Decoding DataType {0} (Error Code:{1})",
//                                                    dtType, (int)DLMSErrors.ErrorDecoding_Type), "Decode_SimpleArray", ex);
//            }
//        }

//        //==========================================================================
//        /// <summary>
//        /// This helper decoder function Parse Compact Array and Convert it to Simple Array Type As<see cref="DLMS.Comm.DataTypes"/> in DLMS_COSEM Specification. 
//        /// See Also <see cref="Intelligent_Data_Decoder(ref byte[], ref int, int)"/>
//        /// </summary>
//        /// <param name="Source_Array">Source_Array that contains Encoded data</param>
//        /// <param name="Source_Index">Start index In Source_Array</param>
//        /// <param name="dtType">DataType of Array Element<see cref="DLMS.Comm.DataTypes"/></param>
//        /// <param name="length">Total byte count</param>
//        /// <returns>Array Of <see cref="System.ValueType"/>, BaseType for DOTNET Common Numeric DataTypes</returns>
//        public static byte[] Parse_CompactArray(ref byte[] Source_Array, ref int Source_Index,
//                                                ref TypeDescriptor ContentDescription, int length)
//        {
//            List<byte> sourceArray = new List<byte>();
//            List<byte> destinationArray = new List<byte>();
//            List<byte> tmpArray = new List<byte>();

//            int element_Counter = -1;
//            byte[] TmpArray = null;

//            try
//            {
//                #region Initial_Work

//                if (Source_Array == null || Source_Array.Length < length)
//                    throw new DLMSDecodingException("Error Decoding Source Array Is Null Type", "Parse_CompactArray");

//                byte current_Char = Source_Array[Source_Index++];
//                if (current_Char != (byte)DataTypes._A13_compact_arry)
//                {
//                    throw new DLMSDecodingException(String.Format("Error Decoding {0} type is expected (Error Code:{1})",
//                        DataTypes._A13_compact_arry, (int)DLMSErrors.Invalid_Type_MisMatch), "Parse_CompactArray");
//                }

//                #endregion
//                #region // Decode_TypeDescriptor

//                ContentDescription = new TypeDescriptor();
//                ContentDescription.Decode_TypeDescripter(Source_Array, ref Source_Index, length);

//                #endregion
//                #region // Copy Data Contents

//                try
//                {
//                    int dataLength = Decode_Length(Source_Array, ref Source_Index);                    
//                    // Copy Data Contents

//#if NETFX_45 || NETFX_451

//                    // Console.WriteLine("NET_45 was set");
//                    var TArraySeg = new ArraySegment<byte>(Source_Array, Source_Index, dataLength);
//#endif
//#if NETFX_35 || NETFX_30

//                    // Console.WriteLine("NET_40 was set");
//                    var TArraySeg = new byte[dataLength];
//                    Buffer.BlockCopy(Source_Array, Source_Index, TArraySeg, 0, TArraySeg.Length);
//#endif

//                    sourceArray.AddRange(TArraySeg);
//                }
//                catch (Exception ex)
//                {
//                    throw new DLMSDecodingException("Error Decoding Compact Array,Invalid Array Data Length", "Parse_CompactArray", ex);
//                }

//                #endregion
//                #region // Type Descriptor Code Block

//                List<TypeDescriptor> Content_DescriptorStruct = new List<TypeDescriptor>();
//                {
//                    var descriptor = ContentDescription.All();

//                    foreach (var type in descriptor)
//                    {
//                        Content_DescriptorStruct.Add(type);
//                    }
//                }

//                #endregion

//                int src_Index = 0;
//                int type_Index = 0;
//                // Parse Compact Array
//                // To Add Data Type TAGs For BasicEncoderDecoder Work
//                element_Counter = BasicEncodeDecode.ParserHelper_CompactArray(destinationArray, sourceArray, ref src_Index, Content_DescriptorStruct, ref type_Index);
//                sourceArray = null;
//                Content_DescriptorStruct = null;

//                #region // Insert Array & Element Count Data

//                if (element_Counter <= 0)
//                {
//                    throw new DLMSDecodingException("Error Decoding Compact Array,Invalid Array Data Length", "Parse_CompactArray");
//                }
//                BasicEncodeDecode.Encode_Length(ref TmpArray, (ushort)element_Counter);
//                destinationArray.Insert(0, (byte)DataTypes._A01_array);
//                destinationArray.InsertRange(1, TmpArray);

//                #endregion
//                return destinationArray.ToArray();
//            }
//            catch (Exception ex)
//            {
//                if (ex is DLMSDecodingException)
//                    throw ex;
//                else
//                    throw new DLMSDecodingException(String.Format("Error Decoding Parse_CompactArray DataType {0} (Error Code:{1})",
//                        (ContentDescription != null) ? ContentDescription.ToString() : "None",
//                        (int)DLMSErrors.ErrorDecoding_Type), "Parse_CompactArray", ex);
//            }
//        }

//        /// <summary>
//        /// This helper method Parse data from Source Array then append it in destinationArray with relevant DataType TAGs 
//        /// </summary>
//        /// <param name="destinationArray">Resultant Encoded data with relevant DataType TAGs</param>
//        /// <param name="sourceArray">Compact Array Data Contents received from metering device</param>
//        /// <param name="src_Index">Index to Source Array</param>
//        /// <param name="ContentDescription">Content Description is Type Descriptor of Compact Array</param>
//        /// <param name="typeIndex">Index to Type Descriptor Data Structure</param>
//        /// <returns></returns>
//        internal static int ParserHelper_CompactArray(List<byte> destinationArray, List<byte> sourceArray,
//                                                      ref int src_Index, List<TypeDescriptor> ContentDescription, ref int typeIndex)
//        {
//            int element_Counter = -1;
//            TypeDescriptor currentTypeTAG = null;
//            DataTypes dtType = DataTypes._A00_Null;
//            DataTypes parentType = DataTypes._A00_Null;

//            byte[] TmpArray = null;

//            // Parse Compact Array
//            // To Add Type TAGs For Normal Decoders To Work Proper
//            try
//            {
//                // iterator
//                while (src_Index < sourceArray.Count &&
//                       sourceArray.Count > 0)
//                {
//                    // Reset TypeIndex To Zero
//                    if (ContentDescription.Count > 0)
//                        typeIndex = typeIndex % ContentDescription.Count;

//                    currentTypeTAG = ContentDescription[typeIndex++];
//                    if (currentTypeTAG == null)
//                        throw new ArgumentNullException("ContentDescription");

//                    // Initialize dtType
//                    dtType = parentType = DataTypes._A00_Null;
//                    if (currentTypeTAG != null)
//                    {
//                        dtType = currentTypeTAG.TypeTAG;
//                        if (currentTypeTAG.Parent != null)
//                        {
//                            parentType = ((TypeDescriptor)currentTypeTAG.Parent).TypeTAG;
//                        }
//                    }

//                    if (currentTypeTAG != null &&
//                                currentTypeTAG.Parent == null)
//                    {
//                        // Reset Element Counter
//                        if (element_Counter <= -1)
//                            element_Counter = 0;
//                        // Increment Counter
//                        element_Counter++;
//                    }

//                    #region // Complex_DataType

//                    if (dtType == DataTypes._A01_array || dtType == DataTypes._A02_structure)
//                    {
//                        #region _A01_array

//                        if (dtType == DataTypes._A01_array)
//                        {
//                            #region Structural_Validation

//                            if (currentTypeTAG != null &&
//                                currentTypeTAG.TypeTAG == DataTypes._A01_array)
//                            {
//                                if (currentTypeTAG.Elements == null || currentTypeTAG.NumberOfElements < 0 ||
//                                    currentTypeTAG.NumberOfElements != currentTypeTAG.Elements.Count)
//                                    throw new DLMSDecodingException(String.Format("Error Invalid Element Count for Array Type ParserHelper_CompactArray (Error Code:{0})",
//                                                                    (int)DLMSErrors.ErrorDecoding_Type), "ParserHelper_CompactArray");
//                            }

//                            #endregion

//                            // Add DataType TAG
//                            destinationArray.Add((byte)dtType);
//                            BasicEncodeDecode.Encode_Length(ref TmpArray, currentTypeTAG.NumberOfElements);
//                            destinationArray.AddRange(TmpArray);
//                        }

//                        #endregion
//                        #region _A02_structure

//                        else if (dtType == DataTypes._A02_structure)
//                        {
//                            // Structural_Validation
//                            if (currentTypeTAG != null &&
//                                currentTypeTAG.TypeTAG == DataTypes._A02_structure)
//                            {
//                                if (currentTypeTAG.Elements == null || currentTypeTAG.NumberOfElements < 0 ||
//                                    currentTypeTAG.NumberOfElements != currentTypeTAG.Elements.Count)
//                                    throw new DLMSDecodingException(String.Format("Error Invalid Element Count for Structure ParserHelper_CompactArray (Error Code:{0})",
//                                                                    (int)DLMSErrors.ErrorDecoding_Type), "ParserHelper_CompactArray");
//                            }

//                            // Add DataType TAG
//                            destinationArray.Add((byte)dtType);
//                            destinationArray.Add((byte)currentTypeTAG.NumberOfElements);
//                        }

//                        #endregion
//                    }

//                    #endregion
//                    #region // Simple DataType Decoding

//                    else
//                    {
//                        #region // FIX_Length DataType

//                        if (dtType == DataTypes._A03_boolean || dtType == DataTypes._A05_double_long || dtType == DataTypes._A06_double_long_unsigned ||
//                            dtType == DataTypes._A07_floating_point || dtType == DataTypes._A0F_integer || dtType == DataTypes._A10_long ||
//                            dtType == DataTypes._A11_unsigned || dtType == DataTypes._A12_long_unsigned || dtType == DataTypes._A14_long_64 ||
//                            dtType == DataTypes._A15_long_64_unsigned || dtType == DataTypes._A16_enum
//                            || dtType == DataTypes._A19_datetime || dtType == DataTypes._A1A_date || dtType == DataTypes._A1B_time)
//                        {
//                            // Add DataType TAG
//                            destinationArray.Add((byte)dtType);

//                            switch (dtType)
//                            {
//                                case DataTypes._A24_Float64:
//                                case DataTypes._A15_long_64_unsigned:
//                                case DataTypes._A14_long_64:
//                                    {
//                                        // 8 byte long type
//                                        destinationArray.AddRange(sourceArray.GetRange(src_Index, 8));
//                                        src_Index += 8;

//                                        break;
//                                    }
//                                case DataTypes._A23_Float32:
//                                case DataTypes._A06_double_long_unsigned:
//                                case DataTypes._A05_double_long:
//                                    {
//                                        // 4 byte long type
//                                        destinationArray.AddRange(sourceArray.GetRange(src_Index, 4));
//                                        src_Index += 4;

//                                        break;
//                                    }
//                                case DataTypes._A12_long_unsigned:
//                                case DataTypes._A10_long:
//                                    {
//                                        // 2 byte long type
//                                        destinationArray.AddRange(sourceArray.GetRange(src_Index, 2));
//                                        src_Index += 2;

//                                        break;
//                                    }
//                                case DataTypes._A11_unsigned:
//                                case DataTypes._A0F_integer:
//                                case DataTypes._A16_enum:
//                                case DataTypes._A03_boolean:
//                                    {
//                                        // single byte long
//                                        destinationArray.Add(sourceArray[src_Index]);
//                                        src_Index += 1;

//                                        break;
//                                    }
//                                #region date_time
//                                case DataTypes._A19_datetime:
//                                    {
//                                        // 12 byte long date-time
//                                        destinationArray.AddRange(sourceArray.GetRange(src_Index, 12));
//                                        src_Index += 12;

//                                        break;
//                                    }
//                                case DataTypes._A1A_date:
//                                    {
//                                        // 05 byte long date
//                                        destinationArray.AddRange(sourceArray.GetRange(src_Index, 05));
//                                        src_Index += 05;

//                                        break;
//                                    }
//                                case DataTypes._A1B_time:
//                                    {
//                                        // 05 byte long time
//                                        destinationArray.AddRange(sourceArray.GetRange(src_Index, 12));
//                                        src_Index += 12;

//                                        break;
//                                    }
//                                #endregion
//                                default:
//                                    throw new Exception(String.Format("data type not included yet ParserHelper_CompactArray Decoder (Error Code:{0})",
//                                        (int)DLMSErrors.Invalid_TypeNotIncluded));

//                            }

//                        }

//                        #endregion
//                        #region // Simple Variable Length DataType

//                        else if (dtType == DataTypes._A04_bit_string || dtType == DataTypes._A09_octet_string ||
//                                 dtType == DataTypes._A0A_visible_string || dtType == DataTypes._A0D_bcd)
//                        {
//                            try
//                            {
//                                // Decode Data Length
//                                int srcIndexer_len = 0;
//                                int Dlength = BasicEncodeDecode.Decode_Length(sourceArray.GetRange(src_Index, 4).ToArray(), ref srcIndexer_len);
//                                if (srcIndexer_len > 0)
//                                    src_Index += srcIndexer_len;

//                                #region // Special Case Handler null Value

//                                if (Dlength <= 0)
//                                {
//                                    // Special null Data_Handler
//                                    if (parentType == DataTypes._A02_structure)
//                                    {
//                                        // null Data Item
//                                        destinationArray.Add(00);
//                                    }
//                                    else
//                                    {
//                                        destinationArray.Add((byte)dtType);
//                                        destinationArray.Add(00);
//                                    }
//                                }

//                                #endregion

//                                else if (Dlength >= 0)
//                                {
//                                    BasicEncodeDecode.Encode_Length(ref TmpArray, Dlength);

//                                    // Add Variable_Data,TypeTAG and DataLength 
//                                    destinationArray.Add((byte)dtType);
//                                    destinationArray.AddRange(TmpArray);

//                                    // Append Variable_Data
//                                    destinationArray.AddRange(sourceArray.GetRange(src_Index, Dlength));
//                                    src_Index += Dlength;
//                                }

//                            }
//                            catch (Exception ex)
//                            {
//                                throw new DLMSDecodingException(String.Format("Error decoding variable data length type {0}", dtType),
//                                                                "ParserHelper_CompactArray", ex);
//                            }

//                            // else-if end
//                        }

//                        #endregion
//                    }

//                    #endregion

//                    // End While
//                }

//                // return destinationArray.ToArray();
//            }
//            catch (Exception ex)
//            {
//                if (ex is DLMSDecodingException)
//                    throw ex;
//                else
//                    throw new DLMSDecodingException(String.Format("Error Exec ParserHelper_CompactArray (Error Code:{0})",
//                        (int)DLMSErrors.ErrorDecoding_Type), "ParserHelper_CompactArray", ex);
//            }
//            return element_Counter;
//        }

//        //==========================================================================
//        /// <summary>
//        /// This helper function decodes an encoded length using A-XDR Decoding(REF) Scheme
//        /// </summary>
//        /// <param name="Packet">Source_Array that contains Encoded data</param>
//        /// <param name="starting_index">Start_index In Source_Array</param>
//        /// <param name="length">Total byte count</param>
//        /// <returns>The decoded length</returns>
//        public static int Decode_Length(byte[] Packet, ref int starting_index, int length)
//        {

//            //to find length
//            //UInt16 length = Common.Decode_Length(Recieved_Packet, ref decode_counter);
//            byte i = 1;
//            UInt16 usLen = 0;

//            if ((Packet[starting_index] & 0x80) == 0x80)
//            {
//                i = (byte)(Packet[starting_index++] & 0x7F);
//            }
//            else
//            {
//                usLen = Packet[starting_index++];
//                return usLen;
//            }

//            for (; i > 0; i--)
//            {
//                usLen <<= 8;
//                usLen |= (UInt16)Packet[starting_index++];
//            }
//            return usLen;
//        }

//        //==========================================================================
//        /// <summary>
//        /// This helper function decodes an encoded length using A-XDR Decoding(REF) Scheme.
//        /// See also <see cref="Decode_Length(byte[],ref int, int)"/>
//        /// </summary>
//        public static int Decode_Length(byte[] Packet, ref int starting_index)
//        {

//            //to find length
//            //UInt16 length = Common.Decode_Length(Recieved_Packet, ref decode_counter);

//            byte i = 1;
//            int usLen = 0;

//            if ((Packet[starting_index] & 0x80) == 0x80)
//            {
//                i = (byte)(Packet[starting_index++] & 0x7F);
//            }
//            else
//            {
//                usLen = Packet[starting_index++];
//                return usLen;
//            }
//            for (; i != 0; i--)
//            {
//                usLen <<= 8;
//                usLen |= Packet[starting_index++];
//            }
//            return usLen;
//        }

//        //==========================================================================
//        /// <summary>
//        /// This helper function decodes an OctectString Type _A09_octet_string <see cref="DLMS.Comm.DataTypes"/> using A-XDR Decoding(REF) Scheme
//        /// </summary>
//        /// <param name="Source_Array">Source_Array that contains Encoded data</param>
//        /// <param name="array_traverse">Start index In Source_Array</param>
//        /// <param name="length">Total byte count</param>
//        /// <returns>byte[] data decoded</returns>
//        public static byte[] Decode_OctectString(byte[] Source_Array, ref int array_traverse, int length)
//        {
//            try
//            {
//                DataTypes dtType = (DataTypes)Source_Array[array_traverse++];
//                if (dtType == DataTypes._A09_octet_string || dtType == DataTypes._A0A_visible_string)
//                {
//                    //Store length
//                    int Length_Array = BasicEncodeDecode.Decode_Length(Source_Array, ref array_traverse);

//                    // Make room for coming array
//                    byte[] Value_Array = new byte[Length_Array];

//                    Buffer.BlockCopy(Source_Array, array_traverse, Value_Array, 0, Value_Array.Length);
//                    array_traverse += Length_Array;

//                    // save coming array
//                    // array_traverse--; // for the function to follow it has to be done (chamchuss)
//                    // byte[] _val_Array = null;
//                    // DLMS_Common.Save_Data_Subfunction(Source_Array, ref array_traverse,
//                    //     ref _val_Array);
//                    // return _val_Array;

//                    return Value_Array;
//                }
//                else if (dtType == DataTypes._A00_Null)
//                    return null;
//                else
//                    throw new DLMSDecodingException(String.Format("Error occurred while decoding the {0} type (Error Code:{1})",
//                         dtType, (int)DLMSErrors.ErrorDecoding_Type), "Decode_OctectString_BasicEncodeDecode");
//            }
//            catch (Exception ex)
//            {
//                if (ex is DLMSDecodingException)
//                    throw ex;
//                else
//                    throw new DLMSDecodingException(String.Format("Error Decoding Octet/Visible String (Error Code:{0})",
//                        (int)DLMSErrors.ErrorDecoding_Type), "Decode_OctectString_BasicEncodeDecode", ex);
//            }
//        }


//        //==========================================================================
//        /// <summary>
//        /// This helper function decodes an UTF-8 String Type <see cref="DLMS.Comm.DataTypes"/> using A-XDR Decoding(REF) Scheme
//        /// </summary>
//        /// <param name="Source_Array">Source_Array that contains Encoded data</param>
//        /// <param name="array_traverse">Start index In Source_Array</param>
//        /// <param name="length">Total byte count</param>
//        /// <returns>byte[] data decoded</returns>
//        public static string Decode_UTF8_String(byte[] Source_Array, ref int array_traverse, int length)
//        {
//            string ret_DataType = string.Empty;
//            try
//            {
//                DataTypes dtType = (DataTypes)Source_Array[array_traverse++];
//                if (dtType == DataTypes._A0C_utf8_string)
//                {
//                    // Store length
//                    int Length_Array = BasicEncodeDecode.Decode_Length(Source_Array, ref array_traverse);

//                    // Make room for coming array
//                    byte[] Value_Array = new byte[Length_Array * 2];

//                    Buffer.BlockCopy(Source_Array, array_traverse, Value_Array, 0, Value_Array.Length);
//                    array_traverse += Length_Array;

//                    // Perform UTF8 Encoding/Decoding
//                    Encoding enc8 = Encoding.UTF8;
//                    ret_DataType = enc8.GetString(Value_Array);

//                    return ret_DataType;
//                }
//                else if (dtType == DataTypes._A00_Null)
//                    return null;
//                else
//                    throw new DLMSDecodingException(String.Format("Error occurred while decoding _A0C_utf8_string {0} type (Error Code:{1})",
//                         dtType, (int)DLMSErrors.ErrorDecoding_Type), "Decode_UTF8_String_BasicEncodeDecode");
//            }
//            catch (Exception ex)
//            {
//                if (ex is DLMSDecodingException)
//                    throw ex;
//                else
//                    throw new DLMSDecodingException(String.Format("Error Decoding UTF8_String (Error Code:{0})",
//                        (int)DLMSErrors.ErrorDecoding_Type), "Decode_UTF8_String_BasicEncodeDecode", ex);
//            }
//        }


//        //==========================================================================
//        /// <summary>
//        /// This helper function decodes Type Descriptor for Compact Array DataType Decoding <see cref="DLMS.Comm.DataTypes"/> using A-XDR Decoding(REF) Scheme
//        /// </summary>
//        /// <param name="typeDescriptor">type</param>
//        /// <param name = "Source_Array">Source_Array that contains Encoded data</param>
//        /// <param name = "array_traverse">Start index In Source_Array</param>
//        /// <param name = "length">Total byte count</param>
//        /// <returns> byte[] data decoded</returns>
//        public static void Decode_TypeDescripter(this TypeDescriptor typeDescriptor, byte[] Source_Array, ref int array_traverse, int length)
//        {
//            try
//            {
//                DataTypes dtType = (DataTypes)Source_Array[array_traverse++];

//                // Special Decoding_Rule
//                if (dtType == DataTypes._A01_array || dtType == DataTypes._A02_structure)
//                {
//                    if (dtType == DataTypes._A01_array)
//                    {
//                        typeDescriptor.TypeTAG = dtType;
//                        typeDescriptor.NumberOfElements = 0;

//                        // Number Of Element Of Array
//                        ushort temp = (ushort)((Source_Array[array_traverse + 1]) + (Source_Array[array_traverse] << 8));
//                        array_traverse += 2;

//                        typeDescriptor.NumberOfElements = temp;

//                        TypeDescriptor ArrayElementType = new TypeDescriptor(typeDescriptor, DataTypes._A00_Null);
//                        typeDescriptor.AddSubTypeDescripter(ArrayElementType);

//                        ArrayElementType.Decode_TypeDescripter(Source_Array, ref array_traverse, length);
//                    }
//                    else if (dtType == DataTypes._A02_structure)
//                    {
//                        typeDescriptor.TypeTAG = dtType;
//                        typeDescriptor.NumberOfElements = 0;

//                        // Number Of Element Of Array
//                        typeDescriptor.NumberOfElements = Source_Array[array_traverse++];

//                        // Decode Seq of Structural Elements
//                        for (int NoOfElement = 1; typeDescriptor.NumberOfElements > 0 && NoOfElement <= typeDescriptor.NumberOfElements; NoOfElement++)
//                        {
//                            TypeDescriptor ArrayElementType = new TypeDescriptor(typeDescriptor, DataTypes._A00_Null);

//                            typeDescriptor.AddSubTypeDescripter(ArrayElementType, (sbyte)NoOfElement);
//                            ArrayElementType.Decode_TypeDescripter(Source_Array, ref array_traverse, length);
//                        }
//                    }
//                }
//                // Simple DataType Decoding Rule
//                else
//                {
//                    typeDescriptor.TypeTAG = dtType;
//                    typeDescriptor.NumberOfElements = 0;
//                }
//            }
//            catch (Exception ex)
//            {
//                if (ex is DLMSDecodingException)
//                    throw ex;
//                else
//                    throw new DLMSDecodingException(String.Format("Error Decoding TypeDescripter (Error Code:{0})",
//                        (int)DLMSErrors.ErrorDecoding_Type), "Decode_TypeDescripter", ex);
//            }
//        }

//        //==========================================================================
//        //==========================================================================
//        /// <summary>
//        /// This helper function decodes an OctectString Type _A09_octet_string <see cref="DLMS.Comm.DataTypes"/> using A-XDR Decoding(REF) Scheme
//        /// See also <see cref="Decode_OctectString(byte[], ref int, int)"/>
//        /// </summary>
//        public static byte[] Decode_OctectString(byte[] Source_Array, ref int array_traverse)
//        {
//            return Decode_OctectString(Source_Array, ref array_traverse, Source_Array.Length);
//        }

//        ////==========================================================================
//        /// <summary>
//        /// This helper function decodes String Type _A0A_visible_string <see cref="DLMS.Comm.DataTypes"/> using A-XDR Decoding(REF) Scheme
//        /// </summary>
//        /// <param name="Source_Array">Source_Array that contains Encoded data</param>
//        /// <param name="array_traverse">Start index In Source_Array</param>
//        /// <param name="length">Total byte count</param>
//        /// <returns>Decoded String</returns>
//        public static String Decode_String(byte[] Source_Array, ref int array_traverse, int length)
//        {
//            try
//            {
//                byte[] rawBytes = Decode_OctectString(Source_Array, ref array_traverse, Source_Array.Length);
//                String _decodeString = DLMS_Common.ArrayToPrintableString(rawBytes); /// new String(ASCIIEncoding.ASCII.GetChars(rawBytes));
//                return _decodeString;
//            }
//            catch (Exception ex)
//            {
//                if (ex is DLMSDecodingException)
//                    throw ex;
//                else
//                    throw new DLMSDecodingException(String.Format("Error Decoding Octet/Visible String (Error Code:{0})",
//                        (int)DLMSErrors.ErrorDecoding_Type), "Decode_String_BasicEncodeDecode", ex);

//            }
//        }

//        /// <summary>
//        /// This helper function decodes String Type _A0A_visible_string <see cref="DLMS.Comm.DataTypes"/> using A-XDR Decoding(REF) Scheme
//        /// See Also <see cref="Decode_String(byte[], ref int, int)"/>
//        /// </summary>
//        /// <param name="Source_Array">Source_Array that contains Encoded data</param>
//        /// <param name="array_traverse">Start index In Source_Array</param>
//        /// <returns>Decoded String</returns>
//        public static String Decode_String(byte[] Source_Array, ref int array_traverse)
//        {
//            try
//            {
//                return Decode_String(Source_Array, ref array_traverse, Source_Array.Length);
//            }
//            catch (Exception ex)
//            {
//                if (ex is DLMSDecodingException)
//                    throw;
//                else
//                    throw new DLMSDecodingException(String.Format("Error Decoding Octet/Visible String (Error Code:{0})",
//                        (int)DLMSErrors.ErrorDecoding_Type),
//                        "Decode_String_BasicEncodeDecode", ex);
//            }
//        }

//        ////==========================================================================
//        /// <summary>
//        /// This helper function decodes String Type _A0A_visible_string <see cref="DLMS.Comm.DataTypes"/> using A-XDR Decoding(REF) Scheme
//        /// See Also <see cref="Decode_String(byte[], ref int, int)"/>
//        /// </summary>
//        /// <param name="arg">The <see cref="Base_Class"/> IC_1,IC_3 (REF) Interface Classes Instance;
//        /// that contain data read for particular attribute</param>
//        /// <returns>Decoded String</returns>
//        public static string Decode_String(Base_Class arg)
//        {
//            try
//            {
//                if (arg.GetType() == typeof(Class_1))
//                {
//                    Class_1 temp_obj = (Class_1)arg;
//                    if (temp_obj.GetAttributeDecodingResult(2) == DecodingResult.Ready)
//                    {
//                        byte[] dtArray = (byte[])temp_obj.Value_Array;
//                        string temp = new String(ASCIIEncoding.ASCII.GetChars(dtArray));
//                        return temp;
//                    }
//                }
//                if (arg.GetType() == typeof(Class_3))
//                {
//                    Class_3 temp_obj = (Class_3)arg;
//                    if (temp_obj.GetAttributeDecodingResult(2) == DecodingResult.Ready)
//                    {
//                        byte[] dtArray = (byte[])temp_obj.Value_Array;
//                        string temp = new String(ASCIIEncoding.ASCII.GetChars(dtArray));
//                        return temp;
//                    }
//                }
//                return null;
//            }
//            catch
//            {
//                throw;
//            }
//        }

//        ////==========================================================================
//        /// <summary>
//        /// This helper function Decodes Bit Length using the A-XDR Decoding Encoding Scheme (REF)
//        /// for type _A04_bit_string <see cref="DLMS.Comm.DataTypes"/>
//        /// </summary>
//        /// <remarks>
//        /// The Bit Length for type _A04_bit_string Type <see cref="DLMS.Comm.DataTypes"/>;
//        /// Internal/Encoded Strucute (DataType;0x4,bitlegth,BitString + Trailing Bits)
//        /// </remarks>
//        /// <param name="Source_Array">Source_Array that contains Encoded data</param>
//        /// <param name="array_traverse">Start index In Source_Array</param>
//        /// <param name="bitLength">Total Bit Count</param>
//        /// <returns>The decoded String</returns>
//        public static byte[] Decode_BitString(byte[] Source_Array, ref int array_traverse, ref int bitLength)
//        {
//            try
//            {
//                int DataByteCount = 0;
//                DataTypes dtType = (DataTypes)Source_Array[array_traverse++];
//                if (dtType != DataTypes._A04_bit_string)
//                    throw new DLMSDecodingException(String.Format("Error occurred while decoding the {0} type (Error Code:{0})", dtType,
//                         (int)DLMSErrors.ErrorDecoding_Type), "Decode_BitString_BasicEncodeDecode");
//                //Decode Data Bit Length
//                bitLength = BasicEncodeDecode.Decode_Length(Source_Array, ref array_traverse);
//                DataByteCount = Convert.ToInt32(Math.Ceiling((bitLength / 8.0)));
//                byte[] TArray = new byte[DataByteCount];
//                //Copy Data Raw Data
//                Array.Copy(Source_Array, array_traverse, TArray, 0, DataByteCount);
//                array_traverse += (ushort)DataByteCount;
//                //Verify BitLength according the array size
//                if ((bitLength == 0 && (TArray == null || TArray.Length == 0)) ||
//                     bitLength <= TArray.Length * 8)
//                {
//                    if (bitLength > 0)
//                    {
//                        BitArray TBits = new BitArray(bitLength, false);
//                        int bitIndex = 0;
//                        int index = 0;
//                        int temp = 0;
//                        //Copy Each bit From Data Array
//                        while (bitIndex < bitLength || index < TArray.Length)
//                        {
//                            if (bitIndex % 8 == 0)   //Copy New Byte
//                            {
//                                temp = TArray[index++];
//                            }
//                            if ((temp & 0x80) != 0)
//                                TBits[bitIndex] = true;
//                            temp = temp << 1;       //Right Shift One Bit
//                            bitIndex++;
//                        }
//                        //Reverse Bits Stored in the Bit Array
//                        //DLMS_Common.Reverse(TBits);
//                        TBits.CopyTo(TArray, 0);
//                    }
//                }
//                else
//                    throw new DLMSEncodingException(String.Format("Invalid bitLength received (Error Code:{0})", (int)DLMSErrors.Invalid_DataLength), "Encode_BitString");
//                return TArray;
//            }
//            catch (Exception ex)
//            {
//                if (ex is DLMSDecodingException)
//                    throw ex;
//                else
//                    throw new DLMSEncodingException(String.Format("Error Decode_BitString (Error Code:{0})",
//                        (int)DLMSErrors.ErrorDecoding_Type), "Common_Decode_BitString", ex);
//            }
//        }

//        ////==========================================================================
//        /// <summary>
//        /// This helper function Decodes Struct DateTime(REF) 
//        /// </summary>
//        /// <remarks>
//        ///  This helper function Decodes Struct DateTime(REF),encoded as 12 byte long Octect String(REF).
//        ///  This function could only parse Struct DateTime In Exact DateTime element Values(No DateTime WildCared Support(REF)).
//        ///  See Also <see cref="Decode_OctectString(byte[], ref int, int)"/>
//        /// </remarks>
//        /// <param name="DateTime_Octet_String">Encode Byte Array</param>
//        /// <returns>DateTime</returns>
//        public static DateTime Decode_DateTime(byte[] DateTime_Octet_String)
//        {
//            try
//            {
//                DateTime Decoded_val;
//                if (DateTime_Octet_String.Length != 12 || DateTime_Octet_String == null)
//                {
//                    throw new DLMSDecodingException("Error occurred while decoding Date & Time,Invalid Data argument",
//                        "Decode_DateTime_BasicEncodeDecode");
//                }
//                UInt16 Year = (UInt16)(((UInt16)(DateTime_Octet_String[0]) << 8) + DateTime_Octet_String[1]);
//                byte Month = DateTime_Octet_String[2];
//                byte Day_of_Month = DateTime_Octet_String[3];
//                byte Day_of_Week = DateTime_Octet_String[4];
//                byte Hours = DateTime_Octet_String[5];
//                byte Minutes = DateTime_Octet_String[6];
//                byte Secs = DateTime_Octet_String[7];
//                byte m_Secs_x10 = (DateTime_Octet_String[8] == 0xFF) ? (byte)DateTimeWildCardMask.HourNotSpecified : DateTime_Octet_String[8];
//                UInt16 Deviation = (UInt16)(((UInt16)(DateTime_Octet_String[9]) << 8) + DateTime_Octet_String[10]);
//                byte clk_Status = DateTime_Octet_String[11];
//                //TODO:Reconsider Code To Include Values Not Being Used UTC Deviation,Hundreds Seconds_x10
//                string datetime_str = String.Format("{0:D4}/{1:D2}/{2:D2} {3:D2}:{4:D2}:{5:D2}", Year, Day_of_Month, Month, Hours, Minutes, Secs);
//                Decoded_val = DateTime.ParseExact(datetime_str, "yyyy/dd/MM HH:mm:ss", null);
//                return Decoded_val;
//            }
//            catch //(Exception exc)
//            {
//                //To Exec Decoders/Encoders
//                //throw new DLMSDecodingException("Error occurred while decoding,Date & Time", "Decode_DateTime_BasicEncodeDecode",exc);
//                //****Comment To WOrk Out Code
//                return DateTime.MinValue;
//            }
//        }

//        //==========================================================================
//        /// <summary>
//        /// This helper function Decodes Struct Date(REF) 
//        /// </summary>
//        /// <remarks>
//        ///  This helper function Decodes Struct DateTime(REF),encoded as 05 byte long Octect String(REF).
//        ///  This function could only parse Struct Date In Exact Date element Values(No Date WildCared Support(REF)).
//        ///  See Also <see cref="Decode_OctectString(byte[], ref int, int)"/>
//        /// </remarks>
//        /// <param name="Date_Octet_String">Encoded Byte Array</param>
//        /// <returns>DateTime</returns>
//        public static DateTime Decode_Date(byte[] Date_Octet_String)
//        {
//            try
//            {
//                DateTime Decoded_val;
//                if (Date_Octet_String.Length != 5 || Date_Octet_String == null)
//                {
//                    throw new DLMSDecodingException("Error occurred while decoding Date,Invalid Data argument",
//                        "Decode_DateTime_BasicEncodeDecode");
//                }
//                UInt16 Year = (UInt16)(((UInt16)(Date_Octet_String[0]) << 8) + Date_Octet_String[1]);
//                byte Month = Date_Octet_String[2];
//                byte Day_of_Month = Date_Octet_String[3];
//                byte Day_of_Week = Date_Octet_String[4];
//                //Try Convert Into Valid Date Object
//                //TODO:Recheck All Special Values/Masks
//                if (Year == 0xFFFF)  //Year Wild Card
//                {
//                    Year = (ushort)DateTimeWildCardMask.YearNotSpecified;
//                }
//                string date_str = String.Format("{0:D4}/{1:D2}/{2:D2}", Year, Day_of_Month, Day_of_Week);
//                Decoded_val = DateTime.ParseExact(date_str, "yyyy/dd/MM", null);
//                return Decoded_val;
//            }
//            catch //(Exception exec)
//            {
//                //throw new DLMSDecodingException("Error occurred while decoding,Date & Time", "Decode_Date_BasicEncodeDecode",exec);
//                //****Comment To WOrk Out Code
//                return DateTime.MinValue;
//            }
//        }

//        //==========================================================================
//        /// <summary>
//        /// This helper function Decodes Struct Time(REF) 
//        /// </summary>
//        /// <remarks>
//        ///  This helper function Decodes Struct Time(REF),encoded as 04 byte long Octect String(REF).
//        ///  This function could only parse Struct Time In Exact Time element Values(No Time WildCared Support(REF)).
//        ///  See Also <see cref="Decode_OctectString(byte[], ref int, int)"/>
//        /// </remarks>
//        /// <param name="Time_Octet_String">Encoded Byte Array</param>
//        /// <returns>DateTime</returns>
//        public static DateTime Decode_Time(byte[] Time_Octet_String)
//        {
//            try
//            {
//                DateTime Decoded_val;
//                if (Time_Octet_String.Length != 4 || Time_Octet_String == null)
//                {
//                    throw new DLMSDecodingException("Error occurred while decodingTime,Invalid Data argument",
//                        "Decode_Time_BasicEncodeDecode");
//                }
//                byte Hours = Time_Octet_String[0];
//                byte Minutes = Time_Octet_String[1];
//                byte Secs = Time_Octet_String[2];
//                byte m_Secs_x10 = Time_Octet_String[3];
//                //Plz Recheck The Special Time Values Masks etc
//                //if (Hours == 0xFF)  ///Hours Not Specified
//                //{
//                //    Hours = 0;  ///Tempo Value
//                //}
//                string time_str = String.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2}", Hours, Minutes, Secs, m_Secs_x10);
//                Decoded_val = DateTime.ParseExact(time_str, "HH:mm:ss:tt", null);
//                //if (Hours == 0xFF)  ///Hours Not Specified
//                //{
//                //   ///SET Masks In DateTime Object
//                //    Decoded_val =  Decoded_val.AddYears((int)DateTimeWildCardMask.HourInValid);
//                //    Decoded_val =  Decoded_val.AddHours((int)DateTimeWildCardMask.HourNotSpecified);
//                //}
//                return Decoded_val;
//            }
//            catch //(Exception exc)
//            {
//                //throw new DLMSDecodingException("Error occurred while decoding,Time", "Decode_Time_BasicEncodeDecode", exc);
//                //****Comment To WOrk Out Code
//                return DateTime.MinValue;
//            }
//        }

//        //==========================================================================

//        /// <summary>
//        /// This helper function Encodes Struct DateTime(REF) 
//        /// </summary>
//        /// <remarks>
//        ///  This helper function Encodes Struct DateTime(REF),Struct is encoded as 12 byte long Octect String(REF).
//        ///  This function could only encode Struct DateTime In Exact DateTime element Values(No DateTime WildCared Support(REF)).
//        ///  See Also <see cref="Decode_OctectString(byte[], ref int, int)"/>
//        /// </remarks>
//        /// <param name="To_Convert">DateTime parameter</param>
//        /// <param name="Encoded_String">The encoded String for Struct DateTime(REF)</param>
//        public static void Encode_DateTime(DateTime To_Convert, ref byte[] Encoded_String)
//        {
//            try
//            {
//                Encoded_String = null;
//                UInt16 Year = (UInt16)(To_Convert.Year);
//                byte Month = (byte)(To_Convert.Month);
//                byte Day_of_Month = (byte)(To_Convert.Day);
//                byte Day_of_Week = (byte)(To_Convert.DayOfWeek);
//                byte Hours = (byte)(To_Convert.Hour);
//                byte Minutes = (byte)(To_Convert.Minute);
//                byte Secs = (byte)(To_Convert.Second);
//                byte m_Secs_x10 = (byte)(To_Convert.Millisecond / 10);
//                UInt16 Deviation = 0x00;
//                byte clk_Status = 0x00;
//                //****Comment Recheck Code Works Properly
//                //Check & Process Wild Card
//                Encoded_String = DLMS_Common.Append_to_End(Month, Day_of_Month);
//                Encoded_String = DLMS_Common.Append_to_Start(Encoded_String, Year);
//                Encoded_String = DLMS_Common.Append_to_End(Encoded_String, Day_of_Week);
//                Encoded_String = DLMS_Common.Append_to_End(Encoded_String, Hours);
//                Encoded_String = DLMS_Common.Append_to_End(Encoded_String, Minutes);
//                Encoded_String = DLMS_Common.Append_to_End(Encoded_String, Secs);
//                Encoded_String = DLMS_Common.Append_to_End(Encoded_String, m_Secs_x10);
//                Encoded_String = DLMS_Common.Append_to_End(Encoded_String, Deviation);
//                Encoded_String = DLMS_Common.Append_to_End(Encoded_String, clk_Status);
//            }
//            catch (Exception ex)
//            {
//                throw new DLMSEncodingException("Error occurred while encoding,Date & Time", "Encode_DateTime_BasicEncodeDecode", ex);
//            }
//        }

//        //==========================================================================

//        /// <summary>
//        /// This helper function Encodes Struct Date(REF) 
//        /// </summary>
//        /// <remarks>
//        ///  This helper function Encodes Struct Date(REF),Struct is encoded as 05 byte long Octect String(REF).
//        ///  This function could only encode Struct Date In Exact Date element Values(No Date WildCared Support(REF)).
//        ///  See Also <see cref="Decode_OctectString(byte[], ref int, int)"/>
//        /// </remarks>
//        /// <param name="To_Convert">DateTime parameter</param>
//        /// <param name="Encoded_String">The encoded String for Struct DateTime(REF)</param>
//        public static void Encode_Date(DateTime To_Convert, ref byte[] Encoded_String)
//        {
//            try
//            {
//                Encoded_String = null;
//                UInt16 Year = (UInt16)(To_Convert.Year);
//                byte Month = (byte)(To_Convert.Month);
//                byte Day_of_Month = (byte)(To_Convert.Day);
//                byte Day_of_Week = (byte)(To_Convert.DayOfWeek);

//                if (To_Convert <= MinDateTime)       ///Check Special DateTime Objects
//                {
//                    //Check & Process Wild Card
//                    if (Year == (ushort)DateTimeWildCardMask.YearNotSpecified)
//                        Year = 0xFFFF;
//                }
//                //****Comment Recheck Code Works Properly
//                Encoded_String = DLMS_Common.Append_to_End(Month, Day_of_Month);
//                Encoded_String = DLMS_Common.Append_to_Start(Encoded_String, Year);
//                Encoded_String = DLMS_Common.Append_to_End(Encoded_String, Day_of_Week);

//            }
//            catch (Exception ex)
//            {
//                throw new DLMSEncodingException("Error occurred while encoding,Date", "Encode_Date_BasicEncodeDecode", ex);
//            }
//        }

//        //==========================================================================
//        /// <summary>
//        /// This helper function Encodes Struct Time(REF) 
//        /// </summary>
//        /// <remarks>
//        ///  This helper function Encodes Struct Time(REF),Struct is encoded as 04 byte long Octect String(REF).
//        ///  This function could only encode Struct Time In Exact Time element Values(No Time WildCared Support(REF)).
//        ///  See Also <see cref="Decode_OctectString(byte[], ref int, int)"/>
//        /// </remarks>
//        /// <param name="To_Convert">DateTime parameter</param>
//        /// <param name="Encoded_String">The encoded String for Struct DateTime(REF)</param>
//        public static void Encode_Time(DateTime To_Convert, ref byte[] Encoded_String)
//        {
//            try
//            {
//                Encoded_String = null;
//                UInt16 Year = (UInt16)(To_Convert.Year);

//                byte Hours = (byte)(To_Convert.Hour);
//                byte Minutes = (byte)(To_Convert.Minute);
//                byte Secs = (byte)(To_Convert.Second);
//                byte m_Secs_x10 = (byte)(To_Convert.Millisecond / 10);

//                if (To_Convert <= MinDateTime)       //Check Special DateTime Objects
//                {
//                    //Check & Process Wild Card
//                    if (Year == (ushort)DateTimeWildCardMask.HourInValid && Hours == (byte)DateTimeWildCardMask.HourNotSpecified)
//                        Hours = 0xFF;
//                }
//                //****Comment Recheck Code Works Properly
//                Encoded_String = DLMS_Common.Append_to_End(Encoded_String, Hours);
//                Encoded_String = DLMS_Common.Append_to_End(Encoded_String, Minutes);
//                Encoded_String = DLMS_Common.Append_to_End(Encoded_String, Secs);
//                Encoded_String = DLMS_Common.Append_to_End(Encoded_String, m_Secs_x10);
//            }
//            catch (Exception ex)
//            {
//                throw new DLMSEncodingException("Error occurred while encoding,Time", "Encode_Time_BasicEncodeDecode", ex);
//            }
//        }

//        //==========================================================================
//        /// <summary>
//        /// This helper Function Convert from IPv4 Address to UInt32 Number to 
//        /// <see cref="System.Net.IPAddress"/>
//        /// </summary>
//        /// <param name="IP_Raw">UInt32 Number</param>
//        /// <returns><see cref="System.Net.IPAddress"/></returns>
//        public static IPAddress Decode_IP(UInt32 IP_Raw)
//        {
//            string str_IP = ((IP_Raw & 0xFF000000) >> 24).ToString() + "." +
//                ((IP_Raw & 0x00FF0000) >> 16).ToString() + "." +
//                ((IP_Raw & 0x0000FF00) >> 8).ToString() + "." +
//                ((IP_Raw & 0x000000FF)).ToString();
//            IPAddress IP_to_return = IPAddress.Parse(str_IP);
//            return IP_to_return;
//        }

//        //==========================================================================

//        /// <summary>
//        /// This helper decoder function skip out most Common Numeric <see cref="DLMS.Comm.DataTypes"/> in DLMS_COSEM Specification.
//        /// </summary>
//        /// <remarks>
//        /// This helper decoder function Skip out following Common Numeric <see cref="DLMS.Comm.DataTypes"/>
//        /// _A14_long_64,_A15_long_64_unsigned,_A05_double_long,_A06_double_long_unsigned,_A10_long,_A11_unsigned,_A0F_integer and
//        /// _A16_enum. This function decodes common DataTypes using A-XDR Decoding Scheme,See Glossary And Term Section
//        /// </remarks>
//        /// <param name="Source_Array">Source_Array that contains Encoded data</param>
//        /// <param name="Source_Index">Start index In Source_Array</param>
//        public static void Skip_Elements(byte[] Source_Array, ref int Source_Index)
//        {
//            DataTypes type_of_data = (DataTypes)Source_Array[Source_Index++];

//            switch (type_of_data)
//            {
//                case DataTypes._A00_Null:
//                    break;

//                case DataTypes._A03_boolean:
//                    Source_Index++;
//                    break;

//                case DataTypes._A04_bit_string:
//                    byte temp_len = Source_Array[Source_Index++];
//                    Source_Index += temp_len;
//                    break;

//                case DataTypes._A05_double_long:
//                    Source_Index += 4;
//                    break;

//                case DataTypes._A06_double_long_unsigned:
//                    Source_Index += 4;
//                    break;

//                case DataTypes._A07_floating_point:
//                    Source_Index += 4;
//                    break;

//                case DataTypes._A09_octet_string:
//                    byte temp_length = Source_Array[Source_Index++];
//                    Source_Index += temp_length;
//                    break;

//                case DataTypes._A0A_visible_string:
//                    byte temp_len1 = Source_Array[Source_Index++];
//                    Source_Index += temp_len1;
//                    break;

//                case DataTypes._A0D_bcd:
//                    Source_Index++;
//                    break;

//                case DataTypes._A0F_integer:
//                    Source_Index++;
//                    break;

//                case DataTypes._A11_unsigned:
//                    Source_Index++;
//                    break;

//                case DataTypes._A12_long_unsigned:
//                    Source_Index += 2;
//                    break;

//                case DataTypes._A14_long_64:
//                    Source_Index += 8;
//                    break;

//                case DataTypes._A15_long_64_unsigned:
//                    Source_Index += 8;
//                    break;

//                case DataTypes._A16_enum:
//                    Source_Index++;
//                    break;

//                case DataTypes._A23_Float32:
//                    Source_Index += 4;
//                    break;

//                case DataTypes._A24_Float64:
//                    Source_Index += 8;
//                    break;

//                default:
//                    throw new DLMSException(String.Format("data type not included yet_Common.In_Data Decoder (Error Code:{0})",
//                           (int)DLMSErrors.Invalid_TypeNotIncluded));
//            }
//        }

//        //==========================================================================
//        /// <summary>
//        /// This helper encoder function encodes most Common Numeric <see cref="DLMS.Comm.DataTypes"/> in DLMS_COSEM Specification.
//        /// </summary>
//        /// <remarks>
//        /// This helper encoder function Encodes following Common Numeric <see cref="DLMS.Comm.DataTypes"/> ,
//        /// _A14_long_64,_A15_long_64_unsigned,_A05_double_long,_A06_double_long_unsigned,_A10_long,_A11_unsigned,_A0F_integer and
//        /// _A16_enum. This function encodes common DataTypes using A-XDR Encoding Scheme(REF),See Glossary And Term Section
//        /// </remarks>
//        /// <param name="type_of_data"><see cref="DLMS.Comm.DataTypes"/> Parameter Encoding Type</param>
//        /// <param name="_value"><see cref="System.ValueType"/>Parameter Value</param>
//        /// <returns>Encoded Value Array</returns>
//        public static byte[] Intelligent_Data_Encoder(DataTypes type_of_data, ValueType _value)
//        {
//            try
//            {
//                List<byte> destinationArray = new List<byte>();
//                destinationArray.Add((byte)type_of_data);
//                switch (type_of_data)
//                {
//                    #region DataTypes._A00_Null

//                    case DataTypes._A00_Null:
//                        {
//                            return new byte[1] { (byte)DataTypes._A00_Null };
//                            break;
//                        }

//                    #endregion
//                    #region DataTypes._A0F_integer

//                    case DataTypes._A0F_integer:
//                        {
//                            long value = Convert.ToInt64(_value);
//                            if (value > sbyte.MaxValue || value < sbyte.MinValue)
//                            {
//                                throw new DLMSEncodingException(String.Format("Invalid {0} Value (Error Code:{1})", type_of_data,
//                                                                            (int)DLMSErrors.Invalid_DataValue),
//                                    "Intelligent_Data_Encoder");
//                            }
//                            else
//                            {
//                                destinationArray.Add((byte)value);
//                            }
//                            break;
//                        }

//                    #endregion
//                    #region DataTypes._A11_unsigned

//                    case DataTypes._A11_unsigned:
//                        {
//                            ulong value = Convert.ToUInt64(_value);
//                            if (value > byte.MaxValue || value < byte.MinValue)
//                            {

//                                throw new DLMSEncodingException(String.Format("Invalid {0} Value (Error Code:{1})", type_of_data,
//                                    (int)DLMSErrors.Invalid_DataValue), "Intelligent_Data_Encoder");
//                            }
//                            else
//                            {
//                                destinationArray.Add((byte)value);
//                            }
//                            break;
//                        }

//                    #endregion
//                    #region DataTypes._A16_enum:

//                    case DataTypes._A16_enum:
//                        {
//                            ulong value = Convert.ToUInt64(_value);
//                            if (value > byte.MaxValue || value < byte.MinValue)
//                            {
//                                throw new DLMSEncodingException(String.Format("Invalid {0} Value (Error Code:{1})", type_of_data,
//                                    (int)DLMSErrors.Invalid_DataValue), "Intelligent_Data_Encoder");
//                            }
//                            else
//                            {
//                                destinationArray.Add((byte)value);
//                            }
//                            break;
//                        }

//                    #endregion
//                    #region DataTypes._A03_boolean:

//                    case DataTypes._A03_boolean:
//                        {
//                            long value = Convert.ToInt64(_value);
//                            byte t = (value == 0) ? (byte)0 : (byte)1;
//                            destinationArray.Add(t);
//                            break;
//                        }

//                    #endregion
//                    #region DataTypes._A05_double_long:

//                    case DataTypes._A05_double_long:
//                        {
//                            long value = Convert.ToInt64(_value);
//                            if (value > int.MaxValue || value < int.MinValue)
//                            {
//                                throw new DLMSEncodingException(String.Format("Invalid {0} Value (Error Code:{1})", type_of_data,
//                                    (int)DLMSErrors.Invalid_DataValue), "Intelligent_Data_Encoder");
//                            }
//                            else
//                            {
//                                destinationArray.Add((byte)(value >> 24));
//                                destinationArray.Add((byte)(value >> 16));
//                                destinationArray.Add((byte)(value >> 8));
//                                destinationArray.Add((byte)(value & 0xFF));
//                            }
//                            break;
//                        }

//                    #endregion
//                    #region DataTypes._A06_double_long_unsigned:

//                    case DataTypes._A06_double_long_unsigned:
//                        {
//                            ulong value = Convert.ToUInt64(_value);
//                            if (value > uint.MaxValue || value < uint.MinValue)
//                            {
//                                throw new DLMSEncodingException(String.Format("Invalid {0} Value (Error Code:{1})", type_of_data,
//                                    (int)DLMSErrors.Invalid_DataValue), "Intelligent_Data_Encoder");
//                            }
//                            else
//                            {
//                                destinationArray.Add((byte)(value >> 24));
//                                destinationArray.Add((byte)(value >> 16));
//                                destinationArray.Add((byte)(value >> 8));
//                                destinationArray.Add((byte)(value & 0xFF));
//                            }
//                            break;
//                        }

//                    #endregion
//                    #region DataTypes._A10_long

//                    case DataTypes._A10_long:
//                        {
//                            long value = Convert.ToInt64(_value);
//                            if (value < short.MinValue || value > short.MaxValue)
//                                throw new DLMSEncodingException(String.Format("Invalid {0} Value (Error Code:{1})", type_of_data,
//                                    (int)DLMSErrors.Invalid_DataValue), "Intelligent_Data_Encoder");
//                            else
//                            {
//                                destinationArray.Add((byte)(value >> 8));
//                                destinationArray.Add((byte)(value & 0xFF));
//                            }
//                            break;
//                        }

//                    #endregion
//                    #region DataTypes._A12_long_unsigned

//                    case DataTypes._A12_long_unsigned:
//                        {
//                            ulong value = Convert.ToUInt64(_value);
//                            if (value < ushort.MinValue || value > ushort.MaxValue)
//                                throw new DLMSEncodingException(String.Format("Invalid {0} Value (Error Code:{1})", type_of_data,
//                                    (int)DLMSErrors.Invalid_DataValue), "Intelligent_Data_Encoder");
//                            else
//                            {
//                                destinationArray.Add((byte)(value >> 8));
//                                destinationArray.Add((byte)(value & 0xFF));
//                            }
//                            break;
//                        }

//                    #endregion
//                    #region DataTypes._A14_long_64

//                    case DataTypes._A14_long_64:
//                        {
//                            long value = Convert.ToInt64(_value);
//                            if (value < long.MinValue || value > long.MaxValue)
//                            {
//                                throw new DLMSEncodingException(String.Format("Invalid {0} Value (Error Code:{1})", type_of_data,
//                                    (int)DLMSErrors.Invalid_DataValue), "Intelligent_Data_Encoder");
//                            }
//                            else
//                            {
//                                destinationArray.Add((byte)(value >> (7 * 8)));
//                                destinationArray.Add((byte)(value >> (6 * 8)));
//                                destinationArray.Add((byte)(value >> (5 * 8)));
//                                destinationArray.Add((byte)(value >> (4 * 8)));
//                                destinationArray.Add((byte)(value >> (3 * 8)));
//                                destinationArray.Add((byte)(value >> (2 * 8)));
//                                destinationArray.Add((byte)(value >> (1 * 8)));
//                                destinationArray.Add((byte)(value & 0xFF));
//                            }
//                            break;
//                        }

//                    #endregion
//                    #region DataTypes._A15_long_64_unsigned

//                    case DataTypes._A15_long_64_unsigned:
//                        {
//                            ulong value = Convert.ToUInt64(_value);
//                            if (value < ulong.MinValue || value > ulong.MaxValue)
//                            {
//                                throw new DLMSEncodingException(String.Format("Invalid {0} Value (Error Code:{1})", type_of_data,
//                                    (int)DLMSErrors.Invalid_DataValue), "Intelligent_Data_Encoder");
//                            }
//                            else
//                            {
//                                destinationArray.Add((byte)(value >> (7 * 8)));
//                                destinationArray.Add((byte)(value >> (6 * 8)));
//                                destinationArray.Add((byte)(value >> (5 * 8)));
//                                destinationArray.Add((byte)(value >> (4 * 8)));
//                                destinationArray.Add((byte)(value >> (3 * 8)));
//                                destinationArray.Add((byte)(value >> (2 * 8)));
//                                destinationArray.Add((byte)(value >> (1 * 8)));
//                                destinationArray.Add((byte)(value & 0xFF));
//                            }
//                            break;
//                        }

//                    #endregion
//                    #region Default
//                    default:
//                        throw new DLMSEncodingException(String.Format("{0} Data Type not implemented yet,1Sorry", type_of_data), "Intelligent_Data_Encoder");

//                    #endregion
//                }
//                ///Type TAG + Value Bytes
//                if (destinationArray.Count > 1)
//                    return destinationArray.ToArray<byte>();
//                else
//                    return null;
//            }
//            catch (Exception ex)
//            {
//                if (ex is DLMSDecodingException)
//                    throw ex;
//                else
//                    throw new DLMSEncodingException(String.Format("Error Encoding {0} Type Value (Error Code:{1})", type_of_data),
//                        "Intelligent_Data_Encoder", ex);
//            }
//        }

//        //==========================================================================
//        /// <summary>
//        /// This helper encoder function encodes BCD DataType Numeric 
//        /// <see cref="DLMS.Comm.DataTypes"/> in DLMS_COSEM Specification
//        /// </summary>
//        /// <param name="_value"><see cref="System.ValueType"/>Parameter Value</param>
//        /// <param name="DataLength">BCD Encoded Data Length</param>
//        /// <returns>Encoded Value Array</returns>
//        public static byte[] BCD_Data_Encoder(ValueType _value, int DataLength = 0)
//        {
//            try
//            {
//                List<byte> destinationArray = new List<byte>();
//                destinationArray.Add((byte)DataTypes._A0D_bcd);

//                ulong value = Convert.ToUInt64(_value);

//                byte[] NumberElements = BasicEncodeDecode.FindNumberElements(value);
//                byte[] Encode_BCD = BasicEncodeDecode.IntToBCD(NumberElements, true);
//                byte[] Encoded_Length = null;
//                ushort Length_Array = (ushort)Encode_BCD.Length;

//                // Adjust BCD Encoded Data_Length
//                if (DataLength != 0 && DataLength > 0)
//                {
//                    if (DataLength < Encode_BCD.Length)
//                        throw new ArgumentException("Error,DataLength Parameter Value Truncate Encoded Data", "DataLength");

//                    byte[] tmpArray = new byte[DataLength];

//                    // Copy Data From Encode_BCD To tmpArray
//                    // Loop Initialization
//                    int indexSource = Encode_BCD.Length - 1;
//                    int index = tmpArray.Length - 1;

//                    for (; index >= 0 && indexSource >= 0; index--, indexSource--)
//                    {
//                        tmpArray[index] = Encode_BCD[indexSource];
//                    }

//                    Encode_BCD = tmpArray;
//                    // Update Length_Array
//                    Length_Array = (ushort)Encode_BCD.Length;
//                }

//                if (Length_Array < 0 || Length_Array > 16)
//                    throw new Exception(String.Format("BCD Array Length Invalid, operation not supported (Error Code:{0})",
//                                        (int)DLMSErrors.Invalid_DataValue));

//                BasicEncodeDecode.Encode_Length(ref Encoded_Length, (ushort)Encode_BCD.Length);

//                // Add BCD Array Length
//                destinationArray.AddRange(Encoded_Length);
//                // Add Encode BCD Data
//                destinationArray.AddRange(Encode_BCD);

//                // Type_TAG + Value_Byte
//                if (destinationArray.Count > 1)
//                    return destinationArray.ToArray<byte>();
//                else
//                    return null;
//            }
//            catch (Exception ex)
//            {
//                if (ex is DLMSDecodingException)
//                    throw ex;
//                else
//                    throw new DLMSEncodingException(String.Format("Error Encoding {0} Type Value (Error Code:{1})", DataTypes._A0D_bcd),
//                        "BCD_Data_Encoder", ex);
//            }
//        }

//        //==========================================================================

//        /// <summary>
//        /// This helper function encodes UTF8-String Type _A0C_utf8_string(REF)
//        /// </summary>
//        /// <remarks>
//        /// This helper function encodes String Type _A0C_utf8_string <see cref="DLMS.Comm.DataTypes"/> using A-XDR Encoding Scheme(REF)
//        /// </remarks>
//        /// <param name="Value_Array">Array that contains Encoded data</param>
//        /// <param name="EncodingType">_A0C_utf8_string or _A0A_visible_string <see cref="DLMS.Comm.DataTypes"/></param>
//        /// <returns>Encode Octet String</returns>
//        public static byte[] Encode_UTF8String(char[] Value_Array)
//        {
//            Encoding enc8 = Encoding.UTF8;
//            try
//            {
//                // Encode Null Octet/Visible String
//                if (Value_Array == null)
//                    return new byte[] { (byte)DataTypes._A00_Null };

//                int data_length = -1;
//                byte[] encodedlength = null;
//                byte[] _valArray = null;

//                data_length = (ushort)Value_Array.Length;
//                BasicEncodeDecode.Encode_Length(ref encodedlength, (ushort)Value_Array.Length);

//                _valArray = enc8.GetBytes(Value_Array);

//                int index = 0;
//                List<byte> _encodedRaw = new List<byte>(_valArray.Length + 10);
//                foreach (object x in Value_Array)
//                {
//                    _valArray[index++] = Convert.ToByte(x);
//                }

//                _encodedRaw.Add((byte)DataTypes._A0C_utf8_string);
//                _encodedRaw.AddRange(encodedlength);
//                _encodedRaw.AddRange(_valArray);
//                return _encodedRaw.ToArray();
//            }
//            catch (Exception ex)
//            {
//                if (ex is DLMSDecodingException)
//                    throw ex;
//                else
//                    throw new DLMSEncodingException(String.Format("Error Encoding UTF8 Visible String (Error Code:{0})", DataTypes._A0C_utf8_string,
//                        (int)DLMSErrors.ErrorEncoding_Type),
//                        "Encode_UTF8String", ex);
//            }
//        }

//        //==========================================================================

//        /// <summary>
//        /// This helper function encodes String Type _A09_octet_string,_A0A_visible_string(REF)
//        /// </summary>
//        /// <remarks>
//        /// This helper function encodes String Type _A09_octet_string,_A0A_visible_string <see cref="DLMS.Comm.DataTypes"/> using A-XDR Encoding Scheme(REF)
//        /// </remarks>
//        /// <param name="Value_Array">Array that contains Encoded data</param>
//        /// <param name="EncodingType">_A09_octet_string or _A0A_visible_string <see cref="DLMS.Comm.DataTypes"/></param>
//        /// <returns>Encode Octet String</returns>
//        public static byte[] Encode_OctetString(Array Value_Array, DataTypes EncodingType)
//        {
//            try
//            {
//                if (EncodingType == DataTypes._A09_octet_string || EncodingType == DataTypes._A0A_visible_string)
//                {
//                    //Encode Null Octect/Visible String
//                    if (Value_Array == null)
//                        return new byte[] { (byte)DataTypes._A00_Null };
//                    byte[] encodedlength = null;
//                    byte[] _valArray = new byte[Value_Array.Length];
//                    int index = 0;
//                    List<byte> _encodedRaw = new List<byte>();
//                    foreach (object x in Value_Array)
//                    {
//                        _valArray[index++] = Convert.ToByte(x);
//                    }
//                    BasicEncodeDecode.Encode_Length(ref encodedlength, (ushort)Value_Array.Length);
//                    _encodedRaw.Add((byte)EncodingType);
//                    _encodedRaw.AddRange(encodedlength);
//                    _encodedRaw.AddRange(_valArray);
//                    return _encodedRaw.ToArray();
//                }
//                else
//                {
//                    throw new DLMSEncodingException(String.Format("{0} is not supported (Error Code:{1})",
//                        EncodingType, (int)DLMSErrors.Invalid_Type_MisMatch), "Encode_OctetString");
//                }
//            }
//            catch (Exception ex)
//            {
//                if (ex is DLMSDecodingException)
//                    throw ex;
//                else
//                    throw new DLMSEncodingException(String.Format("Error Encoding Octet/Visible String (Error Code:{0})", EncodingType,
//                        (int)DLMSErrors.ErrorEncoding_Type),
//                        "Common_Encoder_OctectString", ex);
//            }
//        }

//        //==========================================================================
//        /// <summary>
//        /// This helper function encodes BitString(REF) Type
//        /// </summary>
//        /// <remarks>
//        /// This helper function encodes BitString(REF) Type _A04_bit_string <see cref="DLMS.Comm.DataTypes"/> using A-XDR Encoding Scheme(REF)
//        /// The Encoded BitString format is (_A04_bit_string,bitlegth,BitString + Trailing Bits)
//        /// </remarks>
//        /// <param name="Value_Array">Array that contains Encoded data</param>
//        /// <param name="bitLength">Total bit Count<see cref="DLMS.Comm.DataTypes"/></param>
//        /// <returns>Encode Bit String</returns>
//        public static byte[] Encode_BitString(byte[] Value_Array, int bitLength)
//        {
//            try
//            {
//                int DataByteCount = Convert.ToInt32(Math.Ceiling((bitLength / 8.0)));
//                List<byte> _encodedRaw = new List<byte>(DataByteCount + 3);
//                //Verify BitLength according the array size
//                if ((bitLength == 0 && (Value_Array == null || Value_Array.Length == 0)) ||
//                     bitLength <= Value_Array.Length * 8)
//                {
//                    //<_A04_bit_string><bitLength><bits><trailing Bits>
//                    _encodedRaw.Add((byte)DataTypes._A04_bit_string);

//                    byte[] tLength = null;
//                    Encode_Length(ref tLength, (ushort)bitLength);
//                    _encodedRaw.AddRange(tLength);
//                    if (bitLength > 0)
//                    {
//                        BitArray TBits = new BitArray(Value_Array.Length * 8, false);
//                        int bitIndex = 0;
//                        int index = 0;
//                        int temp = 0;
//                        //Copy Each bit From Data Array
//                        //while (bitIndex < bitLength || index < Value_Array.Length)
//                        while (bitIndex < TBits.Length || index < Value_Array.Length)
//                        {
//                            if (bitIndex % 8 == 0)   //Copy New Byte
//                            {
//                                temp = Value_Array[index++];
//                            }
//                            if ((temp & 0x80) != 0)
//                                TBits[bitIndex] = true;
//                            temp = temp << 1;       //Right Shift One Bit
//                            bitIndex++;
//                        }
//                        /// Reverse Bits Stored in the Bit Array
//                        /// DLMS_Common.Reverse(TBits);
//                        byte[] T = new byte[DataByteCount];
//                        TBits.CopyTo(T, 0);
//                        _encodedRaw.AddRange(T);
//                    }
//                }
//                else
//                    throw new DLMSEncodingException(String.Format("Invalid Argument bitLength (Error Code:{0})",
//                        (int)DLMSErrors.Invalid_DataLength),
//                        "Encode_BitString");
//                return _encodedRaw.ToArray();
//            }
//            catch (Exception ex)
//            {
//                if (ex is DLMSDecodingException)
//                    throw ex;
//                else
//                    throw new DLMSEncodingException(String.Format("Error Encode_BitString (Error Code:{0})", (int)DLMSErrors.ErrorEncoding_Type),
//                        "Common_Encode_BitString", ex);
//            }
//        }

//        //==========================================================================
//        /// <summary>
//        /// This helper function encodes String Type _A0A_visible_string(REF)
//        /// </summary>
//        /// <remarks>
//        /// This helper function encodes String Type _A0A_visible_string <see cref="DLMS.Comm.DataTypes"/> using A-XDR Encoding Scheme(REF)
//        /// </remarks>
//        /// <param name="StrValue">The String parameter to Encode</param>
//        /// <param name="EncodingType">_A09_octet_string or _A0A_visible_string <see cref="DLMS.Comm.DataTypes"/></param>
//        /// <returns>Encoded Octet String</returns>
//        public static byte[] Encode_String(String StrValue, DataTypes EncodingType)
//        {
//            try
//            {
//                byte[] rawBytes = ASCIIEncoding.ASCII.GetBytes(StrValue);
//                return Encode_OctetString(rawBytes, EncodingType);
//            }
//            catch (Exception ex)
//            {
//                if (ex is DLMSDecodingException)
//                    throw ex;
//                else
//                    throw new DLMSEncodingException(String.Format("Error Encoding Printable String (Error Code:{0})",
//                        (int)DLMSErrors.ErrorEncoding_Type),
//                           "Common_Encoder_String", ex);
//            }
//        }

//        //==========================================================================
//        /// <summary>
//        /// This helper function Encode Type Descriptor for Compact Array DataType Encoding <see cref="DLMS.Comm.DataTypes"/> using A-XDR Decoding(REF) Scheme
//        /// </summary>
//        /// <param name="typeDescriptor">type</param>
//        /// <param name = "Source_Array">Source_Array that contains Encoded data</param>
//        /// <param name = "array_traverse">Start index In Source_Array</param>
//        /// <param name = "length">Total byte count</param>
//        /// <returns> byte[] data decoded</returns>
//        public static byte[] Encode_TypeDescripter(this TypeDescriptor typeDescriptor)
//        {
//            List<byte> Encoder_Raw = new List<byte>();

//            string testVar = "Type Descriptor";

//            try
//            {
//                DataTypes dtType = typeDescriptor.TypeTAG;

//                // Complex DataType Encoding
//                if (dtType == DataTypes._A01_array || dtType == DataTypes._A02_structure)
//                {
//                    #region DataTypes._A01_array

//                    if (dtType == DataTypes._A01_array)
//                    {
//                        // Validity Check For typeDescriptor
//                        if (typeDescriptor.NumberOfElements <= 0 ||
//                            typeDescriptor.Elements == null ||
//                            typeDescriptor.Elements.Count <= 0)
//                        {
//                            throw new DLMSEncodingException(String.Format("Error Encoding _A01_array TypeDescripter,Invalid Data (Error Code:{0})",
//                                                    (int)DLMSErrors.ErrorEncoding_Type), "Encode_TypeDescripter");
//                        }

//                        Encoder_Raw.Add((byte)dtType);

//                        // Add Array Length As Unsigned16
//                        Encoder_Raw.Add((byte)((typeDescriptor.NumberOfElements >> 8) & 0xFF));
//                        Encoder_Raw.Add((byte)(typeDescriptor.NumberOfElements & 0xFF));

//                        TypeDescriptor ArrayElementType = typeDescriptor.Elements[0];
//                        var encode_Data = ArrayElementType.Encode_TypeDescripter();

//                        Encoder_Raw.AddRange(encode_Data);
//                    }

//                    #endregion
//                    #region DataTypes._A02_structure

//                    else if (dtType == DataTypes._A02_structure)
//                    {
//                        // Validity Check For typeDescriptor
//                        if (typeDescriptor.NumberOfElements <= 0 ||
//                            typeDescriptor.Elements == null ||
//                            typeDescriptor.Elements.Count <= 0 ||
//                            typeDescriptor.Elements.Count < typeDescriptor.NumberOfElements)
//                        {
//                            throw new DLMSEncodingException(String.Format("Error Encoding _A02_structure TypeDescripter,Invalid Data (Error Code:{0})",
//                                                    (int)DLMSErrors.ErrorEncoding_Type), "Encode_TypeDescripter");
//                        }

//                        Encoder_Raw.Add((byte)dtType);
//                        // Add Array Length As Int
//                        Encoder_Raw.Add((byte)(typeDescriptor.NumberOfElements & 0xFF));

//                        int indexer = 0;
//                        // Encode Sequence Of Structural Elements
//                        for (int NoOfElement = 1; (typeDescriptor.NumberOfElements > 0 &&
//                                                   indexer < typeDescriptor.Elements.Count); NoOfElement++)
//                        {
//                            TypeDescriptor ArrayElementType = typeDescriptor.Elements[indexer++];

//                            var encode_Data = ArrayElementType.Encode_TypeDescripter();
//                            Encoder_Raw.AddRange(encode_Data);
//                        }
//                    }

//                    #endregion
//                }
//                // Simple DataType Encoding Rule
//                else
//                {
//                    Encoder_Raw.Add((byte)dtType);
//                }

//                return Encoder_Raw.ToArray<byte>();
//            }
//            catch (Exception ex)
//            {
//                if (ex is DLMSDecodingException)
//                    throw ex;
//                else
//                    throw new DLMSEncodingException(String.Format("Error Encoding TypeDescripter (Error Code:{0})",
//                                                    (int)DLMSErrors.ErrorEncoding_Type), "Encode_TypeDescripter", ex);
//            }
//        }

//        //==========================================================================

//        /// <summary>
//        /// This helper encoder function encodes Array of Common Numeric <see cref="DLMS.Comm.DataTypes"/> in DLMS_COSEM Specification.
//        /// </summary>
//        /// <remarks>
//        /// This helper encoder function Encodes following Array of Common Numeric <see cref="DLMS.Comm.DataTypes"/> ,
//        /// _A14_long_64,_A15_long_64_unsigned,_A05_double_long,_A06_double_long_unsigned,_A10_long,_A11_unsigned,_A0F_integer and
//        /// _A16_enum. This function encodes common DataTypes using A-XDR Encoding Scheme(REF),See Glossary And Term Section
//        /// </remarks>
//        /// <param name="sourceArray">Array parameter</param>
//        /// <param name="dtType">Parameter Encoding Type,<see cref="DLMS.Comm.DataTypes"/></param>
//        /// <returns>Encoded Value Array</returns>
//        public static byte[] Encode_Array(Array sourceArray, DataTypes dtType)
//        {
//            try
//            {
//                List<byte> destinatioArray = new List<byte>(50);
//                if (sourceArray == null)
//                    throw new DLMSEncodingException("Source Array Is Null Type", "Common_Encode_Array");
//                byte[] t = null;
//                destinatioArray.Add((byte)DataTypes._A01_array);
//                Encode_Length(ref t, (ushort)sourceArray.Length);
//                destinatioArray.AddRange(t);
//                switch (dtType)
//                {
//                    case DataTypes._A00_Null:
//                    case DataTypes._A03_boolean:
//                    case DataTypes._A16_enum:
//                    case DataTypes._A0F_integer:
//                    case DataTypes._A11_unsigned:
//                    case DataTypes._A05_double_long:
//                    case DataTypes._A06_double_long_unsigned:
//                    case DataTypes._A10_long:
//                    case DataTypes._A12_long_unsigned:
//                    case DataTypes._A14_long_64:
//                    case DataTypes._A15_long_64_unsigned:
//                        for (int index = 0; index < sourceArray.Length; index++)
//                        {
//                            t = Intelligent_Data_Encoder(dtType, (ValueType)sourceArray.GetValue(index));
//                            destinatioArray.AddRange(t);
//                        }
//                        break;
//                    default:
//                        throw new DLMSEncodingException(String.Format("{0} is not supported,(Error Code:{1})",
//                           dtType, (int)DLMSErrors.Invalid_TypeNotIncluded), "Common_Encoder_Array");
//                }
//                return destinatioArray.ToArray<byte>();
//            }
//            catch (Exception ex)
//            {
//                if (ex is DLMSDecodingException)
//                    throw ex;
//                else
//                    throw new DLMSEncodingException(String.Format("Error Encoding {0} Type Array (Error Code:{0})",
//                        (int)DLMSErrors.ErrorEncoding_Type), "Common_Encoder_Array", ex);
//            }
//        }

//        //==========================================================================

//        /// <summary>
//        /// This helper encoder function encodes Array of ICustomStructure Custom_Struct <see cref="DLMS.Comm.ICustomStructure"/> Objects
//        /// </summary>
//        /// <param name="ValueArray">Array of Element type ICustomStructure</param>
//        /// <returns>Encoded Array</returns>
//        public static byte[] Encode_Array(ICustomStructure[] ValueArray)
//        {
//            try
//            {
//                List<byte> destinatioArray = new List<byte>(50);
//                if (ValueArray == null)
//                    throw new DLMSEncodingException("Source Array Is Null Type", "Common_Encode_Array");
//                byte[] t = null;
//                destinatioArray.Add((byte)DataTypes._A01_array);
//                Encode_Length(ref t, (ushort)ValueArray.Length);
//                destinatioArray.AddRange(t);
//                foreach (var item in ValueArray)
//                {
//                    if (item == null)
//                        throw new DLMSEncodingException("ICustomStructure object reference Is Null", "Common_Encode_Array");
//                    t = item.Encode_Data();
//                    destinatioArray.AddRange(t);
//                }
//                return destinatioArray.ToArray<byte>();
//            }
//            catch (Exception ex)
//            {
//                if (ex is DLMSDecodingException)
//                    throw ex;
//                else
//                    throw new DLMSEncodingException(String.Format("Error Encoding ICustomStructure Array(Error Code:{0})",
//                        (int)DLMSErrors.ErrorEncoding_Type), "Common_Encoder_Array", ex);
//            }
//        }

//        //==========================================================================
//        /// <summary>
//        /// This helper function encodes length using A-XDR Encoding(REF) Scheme.
//        /// See also <see cref="Decode_Length(byte[],ref int, int)"/>
//        /// </summary>
//        /// <param name="Length_Array">Array to append length</param>
//        /// <param name="Length_to_encode">Length parameter to Encode</param>
//        static public void Encode_Length(ref byte[] Length_Array, ushort Length_to_encode)
//        {
//            if (Length_to_encode < 128)
//            {
//                Length_Array = new byte[1];
//                Length_Array[0] = (byte)(Length_to_encode);
//                return;
//            }
//            else if (Length_to_encode < 256)
//            {
//                Length_Array = new byte[2];
//                Length_Array[0] = 0x81;
//                Length_Array[1] = (byte)(Length_to_encode);
//                return;
//            }
//            else
//            {
//                Length_Array = new byte[3];
//                Length_Array[0] = 0x82;
//                Length_Array[1] = (byte)((UInt16)(Length_to_encode) >> 8);
//                Length_Array[2] = (byte)(Length_to_encode);
//            }
//            return;
//        }


//        /// <summary>
//        /// This helper function encodes length using A-XDR Encoding(REF) Scheme.
//        /// See also <see cref="Decode_Length(byte[],ref int, int)"/>
//        /// </summary>
//        /// <param name="Length_Array">Array to append length</param>
//        /// <param name="Length_to_encode">Length parameter to Encode</param>
//        static public void Encode_Length(ref byte[] Length_Array, int Length_to_encode)
//        {
//            if (Length_to_encode < 128)
//            {
//                Length_Array = new byte[1];
//                Length_Array[0] = (byte)(Length_to_encode);
//                return;
//            }
//            else if (Length_to_encode <= byte.MaxValue)
//            {
//                Length_Array = new byte[2];
//                Length_Array[0] = 0x81;
//                Length_Array[1] = (byte)(Length_to_encode);
//                return;
//            }
//            else if (Length_to_encode <= ushort.MaxValue)
//            {
//                Length_Array = new byte[3];
//                Length_Array[0] = 0x82;
//                Length_Array[1] = (byte)((UInt16)(Length_to_encode) >> 8);
//                Length_Array[2] = (byte)(Length_to_encode);
//            }
//            else
//            {
//                Length_Array = new byte[5];
//                Length_Array[0] = 0x84;
//                Length_Array[1] = (byte)(Length_to_encode >> 24);
//                Length_Array[2] = (byte)(Length_to_encode >> 16);
//                Length_Array[3] = (byte)(Length_to_encode >> 8);
//                Length_Array[4] = (byte)(Length_to_encode);
//            }
//            return;
//        }
//        //==========================================================================
//        /// <summary>
//        /// Appends Encoded Length at the start of Array passed to it
//        /// </summary>
//        /// <param name="Source_Array">The array whose length is to be appended at the start</param>
//        /// <returns>Returns final array with its length appended at the start</returns>
//        static public byte[] Append_Length_to_Start(byte[] Source_Array)
//        {
//            UInt16 Length_of_Array = (UInt16)Source_Array.Length;
//            byte[] Encoded_Length = new byte[1];

//            Encode_Length(ref Encoded_Length, Length_of_Array);

//            return DLMS_Common.Append_to_Start(Source_Array, Encoded_Length);
//        }

//        //==========================================================================
//        /// <summary>
//        /// This helper method Convert IPAddress of type <see cref="System.Net.IPAddress"/> to number IPv number
//        /// </summary>
//        /// <param name="address"></param>
//        /// <returns></returns>
//        public static long IPAddressToLong(System.Net.IPAddress address)
//        {
//            byte[] byteIP = address.GetAddressBytes();

//            long ip = (long)byteIP[0] << 24;
//            ip += (long)byteIP[1] << 16;
//            ip += (long)byteIP[2] << 8;
//            ip += (long)byteIP[3];
//            return ip;
//        }

//        //==========================================================================
//        /// <summary>
//        /// This helper method ToString parameter to IPv string representation
//        /// </summary>
//        /// <param name="IPAddress">IPv4 parameter</param>
//        /// <returns>String</returns>
//        public static string LongToIPAddressString(long IPAddress)
//        {
//            byte[] ip = new byte[4];
//            ip[0] = (byte)(IPAddress / 256 / 256 / 256);
//            ip[1] = (byte)((IPAddress - ip[0]) / 256);
//            ip[2] = (byte)((IPAddress - ip[1]) / 256);
//            ip[3] = (byte)((IPAddress - ip[2]) / 256);
//            string IP = String.Format("{0:000}", ip[0]);

//            IP += String.Format("{0:000}", ip[1]);
//            IP += String.Format("{0:000}", ip[2]);
//            IP += String.Format("{0:000}", ip[3]);
//            return IP;
//        }

//        //==========================================================================
//        /// <summary>
//        /// This helper method encode <see cref="DLMS.Comm.Unit_Scaler"/> Struct(REF)
//        /// </summary>
//        /// <remarks>
//        /// This helper method encode <see cref="DLMS.Comm.Unit_Scaler"/> Struct(REF),
//        /// these Unit_Scaler struct is applicable in IC_3 Register(REF) <see cref="DLMS.Class_3"/>
//        /// and IC_4 Extended Register <see cref="DLMS.Class_4"/> data types.
//        /// </remarks>
//        /// <param name="scalar">Parameter Scaler</param>
//        /// <param name="unit">Parameter Unit</param>
//        /// <returns>Encoded Array</returns>
//        public static byte[] encode_class_3_attribute_3(int scalar, units unit)
//        {
//            byte[] byte_array = new byte[5];
//            byte_array[0] = 0x02;               // Number of elements of the structure
//            byte_array[1] = 0x0F;               // tag of signed
//            byte_array[2] = (byte)scalar;       // passed scaLAR
//            byte_array[3] = 0x16;               // tag of enum
//            byte_array[4] = (byte)unit;         // passed enum
//            return byte_array;
//        }

//        /// <summary>
//        /// This helper method adjust Val Parameter according to scaler parameter.
//        /// </summary>
//        /// <remarks>
//        /// This helper method apply arithmetic mulitplication/division on parameter val as
//        /// 10;s Pow to scaler Parameter. RetVal = Val * 10^scaler
//        /// </remarks>
//        /// <param name="Val">Val parameter to adjust</param>
//        /// <param name="scaler">scaler parameter to apply</param>
//        /// <returns>Adjusted Value</returns>
//        public static ValueType ValueUnitScalerAdjustment(ValueType Val, sbyte scaler)
//        {
//            try
//            {
//                ValueType Value = null;
//                if (Val == null)
//                    return null;
//                // multiply with scaler
//                long multiplier = Convert.ToInt64(Math.Pow(10, (byte)Math.Abs(scaler)));
//                //Algorithm Revised
//                if (scaler != 0)                        //Compute Value With Scaler
//                {
//                    if (Val is float || Val is Double)
//                    {
//                        double temporary_value = Convert.ToDouble(Val);
//                        if (scaler > 0)
//                        {
//                            temporary_value = temporary_value * multiplier;
//                            //for (byte i = 0; i < multiplier; i++) temporary_value = temporary_value * 10;
//                        }
//                        else if (scaler < 0)
//                        {
//                            //for (byte i = 0; i < multiplier; i++) temporary_value /= 10;
//                            temporary_value = temporary_value / multiplier;
//                        }
//                        Value = temporary_value;
//                    }
//                    else
//                    {
//                        long temporary_value = Convert.ToInt64(Val);
//                        if (scaler > 0)
//                        {
//                            Value = temporary_value * multiplier;
//                            //for (byte i = 0; i < multiplier; i++) temporary_value = temporary_value * 10;
//                        }
//                        else if (scaler < 0)
//                        {
//                            //for (byte i = 0; i < multiplier; i++) temporary_value /= 10;
//                            Value = temporary_value / (double)multiplier;
//                        }
//                    }
//                }
//                else
//                    Value = Val;
//                return Value;
//            }
//            catch (Exception ex)
//            {
//                throw new DLMSException("Error adjusting value according to Scaler_Unit Structure_BasicEncodeDecode_UnitScalerAdjustment", ex);
//            }
//        }


//        public static UInt64 FromBCDToExtUInt64(byte[] bcds, uint startOf, uint nBytes, bool isPackedBCD = true)
//        {
//            UInt64 result = 0;
//            uint i = 0;

//            byte signDigit = 0;
//            byte highNible = 0;
//            byte lowNible = 0;

//            // isPackedBCD 
//            // Either Two BCD Digits Stored on Single Byte or Two
//            uint maxLimit = (!isPackedBCD) ? nBytes * 2 : nBytes;

//            for (i = 0; i < maxLimit; )
//            {
//                result *= 100;

//                unchecked
//                {
//                    if (isPackedBCD)
//                        highNible = (byte)(bcds[startOf + i] >> 4);
//                    else
//                        highNible = bcds[startOf + i + 1];

//                    lowNible = (byte)(bcds[startOf + i] & 0x0F);
//                }

//                // High Nibble Processing
//                if (highNible >= 0 && highNible < 0x0A)
//                    result += (UInt64)(10 * highNible);
//                else
//                    signDigit = highNible;

//                // Low Nibble Processing
//                if (lowNible >= 0 && lowNible < 0x0A)
//                    result += lowNible;
//                else
//                    signDigit = lowNible;

//                if (!isPackedBCD)
//                    i += 2;
//                else
//                    i++;

//            }

//            //    8 4 2 1    Sign Notes
//            // A  1 0 1 0 	+ 	 
//            // B  1 0 1 1 	- 	 
//            // C  1 1 0 0 	+ 	Preferred
//            // D  1 1 0 1 	- 	Preferred
//            // E  1 1 1 0 	+ 	 
//            // F  1 1 1 1 	+ 	Unsigned

//            // Sign BCD Digit Check
//            // if ((signDigit == 0x0B || signDigit == 0x0D) && signDigit > 0)
//            //    result *= -1;

//            return (result);
//        }

//        public static byte[] IntToBCD(byte[] input, bool isPackedBCD = true)
//        {
//            byte[] outArr = null;

//            if (isPackedBCD)
//                outArr = new byte[Convert.ToInt32(Math.Ceiling((double)input.Length / 02))];
//            else
//                outArr = new byte[Convert.ToInt32(Math.Ceiling((double)input.Length))];

//            // Handle the case of an odd number in which
//            // a zero should be added at the beginning
//            if (input.Length % 2 != 0)
//            {
//                // Use a temp array to expand the old one, you can use lists or 
//                // another data-structure if you wish to
//                byte[] newInput = new byte[input.Length + 1];
//                Array.Copy(input, 0, newInput, 1, input.Length);
//                newInput[0] = 0;
//                input = newInput;
//                // Dispose the temp array
//                newInput = null;
//            }

//            for (int i = 0; i < outArr.Length; i++)
//            {
//                if (isPackedBCD)
//                {
//                    outArr[i] = (byte)(input[(i * 2)] << 4);
//                    outArr[i] |= (byte)(input[(i * 2) + 1]);
//                }
//                else
//                {
//                    outArr[i] = (byte)(input[i]);
//                }
//            }

//            return outArr;
//        }

//        public static int FindNumberLength(ulong number)
//        {
//            return Convert.ToInt32(Math.Floor(Math.Log(number, 10)) + 1);
//        }

//        public static ulong FindNumberDivisor(ulong number)
//        {
//            return Convert.ToUInt64(Math.Pow(10, FindNumberLength(number) - 1));
//        }

//        public static byte[] FindNumberElements(ulong number)
//        {
//            byte[] elements = new byte[FindNumberLength(number)];
//            ulong divisor = FindNumberDivisor(number);

//            for (int i = 0; i < elements.Length; i++)
//            {
//                elements[i] = Convert.ToByte(number / (ulong)divisor);
//                number %= (ulong)divisor;
//                divisor /= 10;
//            }

//            return elements;
//        }

//    }
//}