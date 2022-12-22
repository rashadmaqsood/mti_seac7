using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS.Comm;

namespace DLMS
{
    /// <summary>
    /// Security Setup  (class_id: 64, version: 1) Manage SecurityPolicy & Security Suite Settings.
    /// Within COSEM Security Context referencing, For Current AA displays ClientSystemTitle and ServerSystemTitle
    /// Enable to transfer all types of Secret Keys With Server for HLS Mechanism. Enable to Activate\Apply Security Policy
    /// for the provided ServerSytemTitle and ClientSystemTitle.
    /// </summary>
    public class Class_64 : Base_Class
    {
        public static readonly byte Security_Activate = 1;
        public static readonly byte Global_Key_Transfer = 2;

        #region DataMembers

        public Security_Policy SecurityPolicy;
        public Security_Suite SecuritySuite;
        public stAppTitle ClientSystemTitle;
        public stAppTitle ServerSystemTitle;

        public List<Key> Key_Transfer_Arg;

        #endregion

        #region Constructors

        public Class_64(Get_Index Index, byte[] ObisCode, ushort No_of_Associations) : base(64, 05, 02, Index, ObisCode, No_of_Associations) { }

        public Class_64(StOBISCode OBISCodeStruct) : base(OBISCodeStruct, 05, 02) { }

        #endregion

        #region Decoders_Encoders

        public override void Decode_Data(ref byte[] Data, ref int array_traverse, int length)
        {
            byte[] Obis_code_recieved = null;
            byte current_char;

            try
            {
                if (DecodingAttribute == 0x00)
                {
                    for (int attributeNo = 1; attributeNo < base.Attribs_No; attributeNo++)
                        base.SetAttributeDecodingResult(attributeNo, DecodingResult.DataNotPresent);
                }
                else if (AccessResults != null && DecodingAttribute <= AccessResults.Length)
                {
                    SetAttributeDecodingResult(DecodingAttribute, DecodingResult.DataNotPresent);
                }

                byte AttribCount = DecoderAttribute_0(ref Data, ref array_traverse, "Decode_Class64_SecuritySetup");
                DecoderLogicalName(ref Data, ref array_traverse, ref Obis_code_recieved, "Decode_Class64_SecuritySetup");

                // Version_1 Check
                if (DecodingAttribute == 0x00 && (AttribCount != 05 || AttribCount != base.Attribs_No))
                {
                    // Generate Error and Return
                    SetAttributeDecodingResult(1, DecodingResult.DecodingError);
                    throw new DLMSDecodingException(String.Format("{0}_{1} Wrong Number of Attributes Received (Error Code:{2})"
                            , OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_DecodingAttribute), "Decode_Class64_SecuritySetup");
                }

                #region Attribute 0x02 SecurityPolicy

                ///------------------------------------------------------
                if (DecodingAttribute == 0x02 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check access rights
                        if (!base.IsAttribReadable(0x02))
                            SetAttributeDecodingResult(0x02, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(02, DecodingResult.DecodingError);

                            throw new DLMSDecodingException(String.Format("{0}_{1} Unable to decode SecurityPolicy,Invalid identifier of value (Error Code:{2})"
                            , OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Class64_SecuritySetup");
                        }
                    }
                    else
                    {
                        current_char = Data[array_traverse++];
                        SecurityPolicy = (Security_Policy)current_char;
                        SetAttributeDecodingResult(0x02, DecodingResult.Ready);
                    }
                }

                #endregion
                #region Attribute 0x03 SecuritySuite

                ///------------------------------------------------------
                if (DecodingAttribute == 0x03 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check Access Rights
                        if (!base.IsAttribReadable(0x03))
                            SetAttributeDecodingResult(0x03, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(0x03, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} Unable to decode SecuritySuite,Invalid identifier of value (Error Code:{2})"
                            , OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Class64_SecuritySetup");
                        }
                    }
                    else
                    {
                        current_char = Data[array_traverse++];
                        SecuritySuite = (Security_Suite)current_char;
                        SetAttributeDecodingResult(0x03, DecodingResult.Ready);
                    }
                }

                #endregion
                #region Attribute 0x04 ClientSystemTitle

                ///------------------------------------------------------
                if (DecodingAttribute == 0x04 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check Access Rights
                        if (!base.IsAttribReadable(0x04))
                            SetAttributeDecodingResult(0x04, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(0x04, DecodingResult.DecodingError);

                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier of 09 (octet string)_decoding ClientSystemTitle (Error Code:{2})", OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Class64_SecuritySetup");
                        }
                    }
                    else
                    {
                        try
                        {
                            array_traverse--;
                            var Calling_System_title = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse);

                            ClientSystemTitle = new stAppTitle()
                            {
                                AP_Title = Calling_System_title,
                                UserId = 0
                            };

                            SetAttributeDecodingResult(0x04, DecodingResult.Ready);
                        }
                        catch (Exception ex)
                        {
                            SetAttributeDecodingResult(0x04, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} Error decoding (octet string) Client SystemTitle (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Class64_SecuritySetup", ex);
                        }
                    }
                }

                #endregion
                #region Attribute 0x05 ServerSystemTitle

                ///------------------------------------------------------
                if (DecodingAttribute == 0x05 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check Access Rights
                        if (!base.IsAttribReadable(0x05))
                            SetAttributeDecodingResult(0x05, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(0x05, DecodingResult.DecodingError);

                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier of 09 (octet string)_decoding Server SystemTitle (Error Code:{2})", OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Class64_SecuritySetup");
                        }
                    }
                    else
                    {
                        try
                        {
                            array_traverse--;
                            var Server_System_title = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse);

                            ServerSystemTitle = new stAppTitle()
                            {
                                AP_Title = Server_System_title,
                                UserId = 0
                            };

                            SetAttributeDecodingResult(0x05, DecodingResult.Ready);
                        }
                        catch (Exception ex)
                        {
                            SetAttributeDecodingResult(0x05, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} Error decoding (octet string) Server SystemTitle (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Class64_SecuritySetup", ex);
                        }
                    }
                }

                #endregion

                // Version 1 Compatibility Check
                if (DecodingAttribute == 0x00 &&
                    AttribCount <= 05)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                {
                    throw ex;
                }
                else
                {
                    throw new DLMSDecodingException(String.Format("{0}_{1} Error occurred while decoding (Error Code:{2})",
                                 OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.ErrorDecoding_Type), "Decode_Class64_SecuritySetup", ex);
                }
            }
        } // End Method

        public override byte[] Encode_Data()
        {
            try
            {
                EncodedRaw = new List<byte>(0x0A);
                ///------------------------------------------------------
                EncoderAttribute_0();
                EncoderLogicalName();
                ///------------------------------------------------------
                #region Attribute 0x02 SecurityPolicy

                if (EncodingAttribute == 0x02 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x02);
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Error Encoding SecurityPolicy,No Access (Error Code:{0})",
                            (int)DLMSErrors.Insufficient_Priviledge), "Encode_Class64_SecuritySetup");
                    }
                    // Encode Here Data
                    else if (EncodingAttribute == 0x02)
                    {
                        throw new DLMSEncodingException(String.Format("Error Encoding SecurityPolicy,Encoder not implemented yet (Error Code:{0})",
                            (int)DLMSErrors.Invalid_EncoderNotIncluded), "Encode_Class64_SecuritySetup");
                    }
                }

                #endregion
                #region Attribute 0x03 SecuritySuite

                if (EncodingAttribute == 0x03 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x03);
                    
                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Error Encoding SecuritySuite,No Access (Error Code:{0})",
                            (int)DLMSErrors.Insufficient_Priviledge), "Encode_Class64_SecuritySetup");
                    }
                    // Encode Here Data
                    else if (EncodingAttribute == 0x03)
                    {
                        throw new DLMSEncodingException(String.Format("Error Encoding SecuritySuite,Encoder not implemented yet (Error Code:{0})",
                            (int)DLMSErrors.Invalid_EncoderNotIncluded), "Encode_Class64_SecuritySetup");
                    }
                }

                #endregion
                #region Attribute 0x04 ClientSystemTitle

                if (EncodingAttribute == 0x04 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x04);

                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Error Encoding ClientSystemTitle,No Access (Error Code:{0})",
                            (int)DLMSErrors.Insufficient_Priviledge), "Encode_Class64_SecuritySetup");
                    }
                    else  // Encode Here Data
                    {
                        var Calling_System_title = BasicEncodeDecode.Encode_OctetString(ClientSystemTitle.AP_Title, DataTypes._A09_octet_string);
                        EncodedRaw.AddRange(Calling_System_title);
                    }
                }

                #endregion
                #region Attribute 0x05 ServerSystemTitle

                if (EncodingAttribute == 0x05 || EncodingAttribute == 0x00)
                {
                    bool IsWriteable = IsAttribWritable(0x05);

                    if (EncodingAttribute == 0x00 && !IsWriteable)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else if (!IsWriteable)
                    {
                        throw new DLMSEncodingException(String.Format("Error Encoding Server System Title,No Access (Error Code:{0})",
                            (int)DLMSErrors.Insufficient_Priviledge), "Encode_Class64_SecuritySetup");
                    }
                    else if (EncodingAttribute == 0x05)
                    {
                        throw new DLMSEncodingException(String.Format("Error Encoding ServerSystemTitle,Encoder not implemented yet (Error Code:{0})",
                            (int)DLMSErrors.Invalid_EncoderNotIncluded), "Encode_Class64_SecuritySetup");
                    }
                }

                #endregion

                return EncodedRaw.ToArray();

            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                {
                    throw ex;
                }
                else
                    throw new DLMSEncodingException(String.Format("{0}_{1} Error occurred while encoding data (Error Code:{2})",
                                               OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.ErrorEncoding_Type), "Encode_Class64_SecuritySetup", ex);
            }
        }

        public override int Decode_Parameters(ref byte[] Data, ref int array_traverse, int length)
        {

            try
            {
                #region Secret

                if (base.MethodInvokeId == Security_Activate ||
                    base.MethodInvokeId == Global_Key_Transfer)
                {
                    // Do_Nothing
                }
                return 1;

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
                    throw new DLMSDecodingException(String.Format("Error occurred while decoding (Error Code:{0})",
                                                   (int)DLMSErrors.ErrorDecoding_Type), "Decode_Parameters_Class64_SecuritySetup");
                }
            }
        }

        public override byte[] Encode_Parameters()
        {
            EncodedRaw = new List<byte>(0x0A);
            try
            {
                #region Security_Activate

                if (base.MethodInvokeId == Security_Activate)
                {
                    EncodedRaw.Add((byte)DataTypes._A16_enum);
                    EncodedRaw.Add((byte)SecurityPolicy);
                }

                #endregion
                #region Key_Transfer

                else if (base.MethodInvokeId == Global_Key_Transfer)
                {

                    if (Key_Transfer_Arg == null ||
                        Key_Transfer_Arg.Count <= 0)

                    // Key_Transfer_Arg.Value == null ||           
                    // Key_Transfer_Arg.Value.Count <= 16)
                    {
                        EncodedRaw.Add((byte)DataTypes._A00_Null);
                    }
                    else   // Encode Here Data
                    {
                        int Key_Count = 0;

                        Key_Count = Key_Transfer_Arg.Count;
                        EncodedRaw.Add((byte)DataTypes._A01_array);
                        // Assume Max Key Data Length <= 128
                        EncodedRaw.Add((byte)Key_Count);
                        // Encode KEY
                        foreach (var key in Key_Transfer_Arg)
                        {
                            // Valid key Object
                            if (key == null || key.Value == null ||
                                key.Value.Count < 16)
                            {
                                throw new DLMSEncodingException(String.Format("Unable to encode Key_Transfer_Data,invalid data", EncodingType),
                                                                "Encode_Class64_SecuritySetup");
                            }

                            EncodedRaw.Add((byte)DataTypes._A02_structure);
                            EncodedRaw.Add((byte)0x02);

                            EncodedRaw.Add((byte)DataTypes._A16_enum);
                            EncodedRaw.Add((byte)key.KeyType);

                            var Wrapped_KEY = BasicEncodeDecode.Encode_OctetString(key.Value.ToArray(), DataTypes._A09_octet_string);
                            EncodedRaw.AddRange(Wrapped_KEY);
                        }
                    }
                }

                #endregion
                else
                    EncodedRaw.Add((byte)DataTypes._A00_Null);

                return EncodedRaw.ToArray();
            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                {
                    throw ex;
                }
                else
                {
                    throw new DLMSEncodingException(String.Format("Error occurred while Encode_Parameters (Error Code:{0})",
                                                   (int)DLMSErrors.ErrorEncoding_Type), "Encode_Class64_SecuritySetup");
                }
            }
        }

        #endregion

        #region Member_Methods

        public override string ToString()
        {
            return base.ToString();
        }

        #endregion
    }
}
