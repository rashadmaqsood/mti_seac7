using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS.Comm;

namespace DLMS
{
    /// <summary>
    /// GPRS modem setup (class_id: 45, version: 0) allows setting up GPRS modems.
    /// This Class allows setting up GPRS modems, by handling all data necessary data for modem management.
    /// </summary>
    public class Class_45 : Base_Class
    {
        #region DataMembers
        
        /// <summary>
        /// Defines the access point name of the network.
        /// </summary>
        public string APN;
        /// <summary>
        /// Holds the personal identification number.
        /// </summary>
        public UInt16 Pin_Code;

        #endregion

        #region Constructors
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Index">OBIS code for specific entity</param>
        /// <param name="Obis_Code">Bytes representation of an OBIS code</param>
        /// <param name="No_of_Associations">maximum no of association</param>
        public Class_45(Get_Index Index, byte[] Obis_Code, UInt16 No_of_Associations)
            : base(45, 4, 0, Index, Obis_Code, No_of_Associations)
        {
            /// make room
            APN = "";
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Obis_Code">Bytes representation of an OBIS code</param>
        /// <param name="Attribute_recieved">no of attribute associated with current object</param>
        public Class_45(byte[] Obis_Code, byte Attribute_recieved)
            : base(45, 4, 0, Obis_Code)
        {
            DecodingAttribute = Attribute_recieved;
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="OBISCodeStruct">OBIS code for a specific Object</param>
        public Class_45(StOBISCode OBISCodeStruct)
            : base(OBISCodeStruct, 4, 0)
        {
            /// make room
            APN = "";
        }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="obj">Class_45 Object</param>             
        public Class_45(Class_45 obj)
            : base(obj)
        {
            if (obj.APN != null)
                this.APN = obj.APN;
            Pin_Code = obj.Pin_Code;
        }

        #endregion

        #region Decoders/Encoders
        
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
            ///------------------------------------------------------
            if (DecodingAttribute == 0x00)
            {
                for (int index = 0; index < AccessResults.Length; index++)
                    AccessResults[index] = DecodingResult.DataNotPresent;
            }
            else if (AccessResults != null && DecodingAttribute <= AccessResults.Length)
            {
                SetAttributeDecodingResult(DecodingAttribute, DecodingResult.DataNotPresent);
            }

            try
            {
                DecoderAttribute_0(ref Data, ref array_traverse, "Decode_Data_Class_45_GPRS_Modem_SETUP");
                DecoderLogicalName(ref Data, ref array_traverse, ref Obis_code_recieved, "Decode_Data_Class_45_GPRS_Modem_SETUP");
                ///------------------------------------------------------
                #region Attribute 0x02
                
                if (DecodingAttribute == 0x02 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    
                    /// null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        /// Check Access Rights
                        if (!IsAttribReadable(02))
                            SetAttributeDecodingResult(02, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(02, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_45_GPRS_Modem_SETUP");
                        }
                    }
                    else
                    {
                        try
                        {
                            array_traverse--;
                            APN = BasicEncodeDecode.Decode_String(Data, ref array_traverse, length);
                            SetAttributeDecodingResult(2, DecodingResult.Ready);
                        }
                        catch (Exception ex)
                        {
                            SetAttributeDecodingResult(02, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} This element is not 09 (octet string) (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_45_GPRS_Modem_SETUP", ex);

                        }

                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x03
                
                if (DecodingAttribute == 0x03 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check Access Rights
                        if (!IsAttribReadable(03))
                            SetAttributeDecodingResult(03, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(03, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_45_GPRS_Modem_SETUP");
                        }
                    }
                    else
                    {
                        try
                        {
                            array_traverse--;
                            Pin_Code = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, length));
                            SetAttributeDecodingResult(03, DecodingResult.Ready);
                        }
                        catch (Exception ex)
                        {
                            SetAttributeDecodingResult(03, DecodingResult.DecodingError);

                            throw new DLMSDecodingException(String.Format("{0}_{1} This element is not (long_unsigned)_Error Decoding Pin_Code,invalid identifier (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, 
                                (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_45_GPRS_Modem_SETUP", ex);
                        }
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x04
                
                if (DecodingAttribute == 0x04 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    /// null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        /// Check Access Rights
                        if (!IsAttribReadable(04))
                            SetAttributeDecodingResult(04, DecodingResult.NoAccess);
                        else
                        {

                            SetAttributeDecodingResult(04, DecodingResult.Ready);
                            //throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier (Error Code:{2})",
                              //  OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_45_GPRS_Modem_SETUP");
                        }
                    }
                    else
                    {
                        SetAttributeDecodingResult(0x04, DecodingResult.DecodingError);
                        throw new DLMSDecodingException(String.Format("{0}_{1} Error Decoding Attribute 04,decoder not implemented (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_DecoderNotIncluded), "Decode_Data_Class_45_GPRS_Modem_SETUP");
                    }
                }

                #endregion

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
                            OBISIndex, OBISIndex.OBISIndex, "Decode_Data_Class_45_GPRS_Modem_SETUP", (int)DLMSErrors.ErrorDecoding_Type), ex);
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
                #region Attribute 0x02 APN
                
                if (EncodingAttribute == 0x02 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x02);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode APN String,No Access (Error Code:{0})",
                            (int)DLMSErrors.Insufficient_Priviledge), "Encode_Data_Class_45_GPRS_Modem_SETUP");
                    }
                    else /// Encode Here Data APN String
                    {
                        EncodedRaw.AddRange(BasicEncodeDecode.Encode_String(this.APN, DataTypes._A09_octet_string));
                    }

                }

                #endregion
                //------------------------------------------------------ 
                #region Attribute 0x03 PINCODE
                
                if (EncodingAttribute == 0x03 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x03);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode PIN,No Access (Error Code:{0})",
                                                       (int)DLMSErrors.Insufficient_Priviledge), "Encode_Data_Class_45_GPRS_Modem_SETUP");
                    }
                    else /// Encode Here Data PIN CODE <DataType long_unsigned>
                    {
                        EncodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A12_long_unsigned, this.Pin_Code));
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x04 QOS
                
                if (EncodingAttribute == 0x04 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x04);
                    
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Unable to encode QOS, No Access (Error Code:{0})",
                                                       (int)DLMSErrors.Insufficient_Priviledge), "Encode_Data_Class_45_GPRS_Modem_SETUP");
                    }
                    else /// Encode Here Data PIN CODE <DataType long_unsigned>
                    {
                        throw new DLMSEncodingException(String.Format("{0}_{1} Unable to encode QOS, Encoder Not Implemented Yet (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_EncoderNotIncluded), "Encode_Data_Class_45_GPRS_Modem_SETUP");
                    }
                }

                #endregion
                ///------------------------------------------------------
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
                    throw new DLMSException(String.Format("{0}_{1}_{2}_{3} (Error Code:{4})", "Error occurred while encoding data ",
                            OBISIndex, OBISIndex.OBISIndex, "Encode_Data_Class_45_GPRS_Modem_SETUP", (int)DLMSErrors.ErrorEncoding_Type), ex);
            }
        }

        #endregion

        #region Member Methods

        public override object Clone()
        {
            Class_45 cloned = new Class_45(this);
            return cloned;
        }

        /// <summary>
        /// Returns the String Representation of the Class_21 object
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            String baseStr = base.ToString();
            StringBuilder strVal = new StringBuilder();
            
            strVal.AppendFormat(",APN:{0}:{1}", APN, GetAttributeDecodingResult(2));
            strVal.AppendFormat(",PIN Code:{0}:{1}", Pin_Code, GetAttributeDecodingResult(3));
            strVal.AppendFormat(",QOS:{0}:{1}", "", GetAttributeDecodingResult(4));

            return baseStr + strVal;
        }

        #endregion
    }
}
