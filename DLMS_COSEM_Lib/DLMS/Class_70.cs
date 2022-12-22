using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS.Comm;

namespace DLMS
{
    /// <summary>
    /// Disconnect control (class_id: 70, version: 0) Instances of the Disconnect control Class manages an internal or external disconnect unit of the meter
    /// Instances of the Disconnect control Class manage an internal or external disconnect unit of the meter (e.g. electricity breaker, gas valve) in order to connect or disconnect – partly or entirely – the premises of the consumer to / from the supply.
    /// Disconnect and reconnect can be requested:
    /// <list class="bullet">
    /// <listItem>
    /// Remotely, via a communication channel: remote_disconnect, remote_reconnect;
    /// </listItem>
    /// <listItem>
    /// Manually, using e.g. a push button: manual_disconnect, manual_reconnect;
    /// </listItem>
    /// <listItem>
    /// Locally, by a function of the meter, e.g. limiter, prepayment: local_disconnect, local_reconnect.
    /// </listItem>
    /// </list>
    /// </summary>
    public class Class_70 : Base_Class
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Index">OBIS code for specific entity</param>
        /// <param name="Obis_Code">Bytes representation of an OBIS code</param>
        /// <param name="No_of_Associations">maximum no of association</param>
        public Class_70(Get_Index Index, byte[] ObisCode, ushort No_of_Associations)
            : base(70, 2, 0, Index, ObisCode, No_of_Associations)
        { }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="OBISCodeStruct">OBIS code for a specific Object</param>
        public Class_70(StOBISCode OBISCodeStruct) : base(OBISCodeStruct, 2, 0) { }

        private bool output_state;
        /// <summary>
        /// Get or Set the actual physical state of the disconnect unit.
        /// </summary>
        /// <remarks>
        /// Shows the actual physical state of the disconnect unit, i.e. if an electricity breaker or a gas valve is open or closed.
        /// boolean :
        ///     TRUE = Closed,
        ///     FALSE = Open
        /// </remarks> 
        public bool Output_state
        {
            get { return output_state; }
            set { output_state = value; }
        }

        private ControlState control_state;
        /// <summary>
        /// Get or Set the internal state of the disconnect control object
        /// </summary>
        public ControlState Control_state
        {
            get { return control_state; }
            set { control_state = value; }
        }

        private byte control_mode;
        /// <summary>
        /// Get or Set the Control Mode
        /// </summary>
        public byte Control_Mode
        {
            get { return control_mode; }
            set { control_mode = value; }
        }

        public static readonly byte REMOTE_DISCONNECT = 1;
        public static readonly byte REMOTE_RECONNECT = 2;

        /// <summary>
        /// Decode Data of this Class which is received in response of get data Request
        /// </summary>
        /// <param name="Data">Received data from Remote site</param>
        /// <param name="array_traverse">Off-Set</param>
        /// <param name="length">Length to decode</param>
        public override void Decode_Data(ref byte[] Data, ref int array_traverse, int length)
        {
            ///UInt16 array_traverse = 0;
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
                DecoderAttribute_0(ref Data, ref array_traverse, "Decode_Data_Class_70_Object_Array");
                DecoderLogicalName(ref Data, ref array_traverse, ref Obis_code_recieved, "Decode_Data_Class_70_Object_Array");

                #region Attribute 0x02

                if (DecodingAttribute == 0x02 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    /// null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        /// Check Access Rights
                        if (!IsAttribReadable(2))
                            SetAttributeDecodingResult(2, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(2, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_70");
                        }
                    }
                    else
                    {
                        /// Reset Data Variables
                        ValueType Value = null;
                        array_traverse--;
                        /// save value
                        Value = BasicEncodeDecode.Intelligent_Data_Decoder(
                                ref Data, ref array_traverse, Data.Length);
                        this.output_state = Convert.ToBoolean(Value);
                        SetAttributeDecodingResult(2, DecodingResult.Ready);
                    }
                }

                #endregion
                #region Attribute 0x03

                if (DecodingAttribute == 0x03 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    /// null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        /// Check access rights
                        if (!IsAttribReadable(3))
                            SetAttributeDecodingResult(3, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(3, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier (Error Code:{2})",
                                                            OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch),
                                                            "Decode_Data_Class_70");
                        }
                    }
                    else
                    {
                        ///Reset Data Variables
                        ValueType Value = null;
                        array_traverse--;
                        /// save value
                        Value = BasicEncodeDecode.Intelligent_Data_Decoder(
                                ref Data, ref array_traverse, Data.Length);
                        this.control_state = (ControlState)(Convert.ToByte(Value));
                        SetAttributeDecodingResult(3, DecodingResult.Ready);
                    }
                }

                #endregion
                #region Attribute 0x04

                if (DecodingAttribute == 0x04 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    /// null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        /// Check Access Rights
                        if (!IsAttribReadable(4))
                            SetAttributeDecodingResult(4, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(4, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier (Error Code:{2})",
                                                            OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch),
                                                            "Decode_Data_Class_70");
                        }
                    }
                    else
                    {
                        /// Reset Data Variable
                        ValueType Value = null;
                        array_traverse--;
                        /// save value
                        Value = BasicEncodeDecode.Intelligent_Data_Decoder(
                                ref Data, ref array_traverse, Data.Length);
                        this.control_mode = (Convert.ToByte(Value));
                        /// this.control_state = (ControlState)(Convert.ToByte(Value));
                        SetAttributeDecodingResult(4, DecodingResult.Ready);
                    }
                }

                #endregion
                /// make data array ready for upcoming objects
                /// DLMS_Common.Data_Array_resizer(ref Data, array_traverse);
            }
            catch (Exception ex)
            {
                SetAttributeDecodingResult(04, DecodingResult.DecodingError);
                if (ex is DLMSDecodingException)
                {
                    throw ex;
                }
                else
                {
                    throw new DLMSException(String.Format("{0}_{1}  (Error Code:{2})", "Error occurred while decoding data",
                        "Decode_Data_Class_70", (int)DLMSErrors.ErrorDecoding_Type), ex);
                }
            }
        }

        public override byte[] Encode_Parameters()
        {
            EncodedRaw = new List<byte>(0x02);
            try
            {
                #region Execute_Script


                EncodedRaw.AddRange(BasicEncodeDecode.Intelligent_Data_Encoder(DataTypes._A0F_integer, 0));

                #endregion

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
                                                   (int)DLMSErrors.ErrorEncoding_Type), "Encode_Parameters_Class_70");
                }
            }
        }

    }
}
