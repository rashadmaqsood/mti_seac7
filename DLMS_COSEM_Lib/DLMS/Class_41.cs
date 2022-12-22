using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS.Comm;

namespace DLMS
{
    /// <summary>
    /// TCP-UDP setup (class_id: 41, version: 0) allows modeling the setup of the TCP or UDP sub-layer of the COSEM TCP or UDP based transport layer of a TCP-UDP/IP based communication profile.
    /// In TCP-UDP/IP based communication profiles, all AAs (Application Accossiation) between a physical device hosting one or more COSEM client application processes and a physical device hosting one or more COSEM server APs rely on a single TCP or UDP connection.
    /// The TCP or UDP entity is wrapped in the COSEM TCP-UDP based transport layer.
    /// Within a physical device, each AP (Application Process) – client AP or server logical device – is bound to a Wrapper Port (WPort).
    /// The binding is done with the help of the SAP Assignment object.
    /// On the other hand, a COSEM TCP or UDP based transport layer may be capable to support more than one TCP or UDP connections, between a physical device and several peer physical devices hosting COSEM APs (Application Process).
    /// </summary>
    public class Class_41 : Base_Class
    {
        #region DataMembers

        /// <summary>
        /// Holds the TCP-UDP port number on which the physical device is listening for the DLMS/COSEM application.
        /// </summary>
        public UInt16 TCP_UDP_Port;
        /// <summary>
        /// References an IP setup object by its logical name.
        /// </summary>
        /// <remarks>
        /// The referenced object contains information about the IP Address settings of the IP layer supporting the TCP-UDP layer.
        /// </remarks>
        public byte[] IP_Reference;
        /// <summary>
        /// It is Used by TCP indicate the maximum receive segment size to its partner
        /// </summary>
        /// <remarks>
        /// With the help of the Maximum Segment Size (MSS) option, a TCP can indicate the maximum receive segment size to its partner. Note, that:
        /// <list type="Bullet">
        /// - this option must only be sent in the initial connection request (i.e. in segments with the SYN control bit sent);
        /// - if this option is not present, conventionally MSS is considered as its default value, 576;
        /// - MSS is not negotiable; its value is indicated by this attribute.
        /// </list>
        /// </remarks>
        public UInt16 Max_Segment_Size;
        /// <summary>
        /// The maximum number of simultaneous connections the COSEM TCP-UDP based transport layer is able to support.
        /// </summary>
        public byte Simultaneous_Conn_No;
        /// <summary>
        /// Defines the TCP Connection Timeout
        /// </summary>
        /// <remarks>
        /// Defines the time, expressed in seconds over which, if no frame is received from the COSEM client, the inactive TCP connection shall be aborted.
        /// When this value is set to 0, this means that the inactivity_time_out is not operational.
        /// In other words, a TCP connection, once established, in normal conditions – no power failure, etc. – will never be aborted by the COSEM server.
        /// Note, that all actions related to the management of the inactivity time-out function – measuring the inactivity time, aborting the TCP connection if the time-out is over, etc. – are managed inside the TCP-UDP layer implementation.
        /// </remarks>
        public UInt16 Inactivity_Time_Out_Secs;

        private bool flg_MSS_too_small = false;
        private bool flg_Sim_Conn_No_too_small = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Index">OBIS code for specific entity</param>
        /// <param name="Obis_Code">Bytes representation of an OBIS code</param>
        /// <param name="No_of_Associations">maximum no of association</param>
        public Class_41(Get_Index Index, byte[] Obis_Code, UInt16 No_of_Associations)
            : base(41, 6, 0, Index, Obis_Code, No_of_Associations)
        {
            // Make Room
            IP_Reference = new byte[6];
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Obis_Code">Bytes representation of an OBIS code</param>
        /// <param name="Attribute_recieved">no of attribute associated with current object</param>
        public Class_41(byte[] Obis_Code, byte Attribute_recieved)
            : base(41, 6, 0, Obis_Code)
        {
            DecodingAttribute = Attribute_recieved;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="OBISCodeStruct">OBIS code for a specific Object</param>
        public Class_41(StOBISCode OBISCodeStruct)
            : base(OBISCodeStruct, 6, 0)
        {
            // Make Room
            IP_Reference = new byte[6];
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="obj">Class_41 Object</param>                
        public Class_41(Class_41 obj)
            : base(obj)
        {
            TCP_UDP_Port = obj.TCP_UDP_Port;
            if (obj.IP_Reference != null)
                IP_Reference = (byte[])obj.IP_Reference.Clone();
            Max_Segment_Size = obj.Max_Segment_Size;
            Simultaneous_Conn_No = obj.Simultaneous_Conn_No;
            Inactivity_Time_Out_Secs = obj.Inactivity_Time_Out_Secs;
            flg_MSS_too_small = obj.flg_MSS_too_small;
            flg_Sim_Conn_No_too_small = obj.flg_Sim_Conn_No_too_small;
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
                DecoderAttribute_0(ref Data, ref array_traverse, "Decode_Data_Class_41_TCP_UDP_Object");
                DecoderLogicalName(ref Data, ref array_traverse, ref Obis_code_recieved, "Decode_Data_Class_41_TCP_UDP_Object");
                //------------------------------------------------------
                #region Attribute 0x02

                if (DecodingAttribute == 0x02 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check Access Right
                        if (!IsAttribReadable(0x02))
                            SetAttributeDecodingResult(0x02, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(0x02, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier of TCP UDP Port (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_41_TCP_UDP_Object");
                        }
                    }
                    else
                    {
                        try
                        {
                            array_traverse--;
                            TCP_UDP_Port = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, Data.Length));
                            SetAttributeDecodingResult(0x02, DecodingResult.Ready);
                        }
                        catch (Exception ex)
                        {
                            SetAttributeDecodingResult(0x02, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} This element is not (long_unsigned),invalid identifier of value (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_41_TCP_UDP_Object", ex);

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
                        // Check Access Right
                        if (!IsAttribReadable(03))
                            SetAttributeDecodingResult(0x03, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(0x03, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier of IP_Reference (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_41_TCP_UDP_Object");
                        }
                    }
                    else
                    {
                        try
                        {
                            array_traverse--;
                            IP_Reference = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse, Data.Length);
                            SetAttributeDecodingResult(0x03, DecodingResult.Ready);
                        }
                        catch (Exception ex)
                        {
                            // Generate Error and Return
                            SetAttributeDecodingResult(0x03, DecodingResult.DecodingError);
                            
                            throw new DLMSDecodingException(String.Format("{0}_{1} This element is not 09 (octet string),invalid identifier of value (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_41_TCP_UDP_Object", ex);
                        }
                    }
                }
                #endregion
                //------------------------------------------------------
                #region Attribute 0x04
                if (DecodingAttribute == 0x04 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check access right
                        if (!IsAttribReadable(04))
                            SetAttributeDecodingResult(04, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(04, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier of Max Segment Size (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_41_TCP_UDP_Object");
                        }
                    }
                    else
                    {
                        try
                        {
                            array_traverse--;
                            Max_Segment_Size = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, Data.Length));
                            SetAttributeDecodingResult(04, DecodingResult.Ready);
                        }
                        catch (Exception ex)
                        {
                            SetAttributeDecodingResult(04, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} This element is not 12 (long_unsigned),invalid identifier of value (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_41_TCP_UDP_Object", ex);
                        }
                        // MSS is smaller than minimum value
                        if (Max_Segment_Size < 40)
                        {
                            // used at the end of this function
                            flg_MSS_too_small = true;
                        }

                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x05
                if (DecodingAttribute == 0x05 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check Access Right
                        if (!IsAttribReadable(05))
                            SetAttributeDecodingResult(05, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(05, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} Invalid identifier of Simultaneous_Conn_No (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_41_TCP_UDP_Object");
                        }
                    }
                    else
                    {
                        try
                        {
                            array_traverse--;
                            Simultaneous_Conn_No = Convert.ToByte(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, Data.Length));
                            SetAttributeDecodingResult(05, DecodingResult.Ready);
                        }
                        catch (Exception ex)
                        {
                            SetAttributeDecodingResult(05, DecodingResult.DecodingError);
                            
                            throw new DLMSDecodingException(String.Format("{0}_{1} This element is not 11 (unsigned),invalid identifier of value (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_41_TCP_UDP_Object", ex);
                        }
                        // MSS is smaller than minimum value
                        if (Simultaneous_Conn_No == 0)
                        {
                            flg_Sim_Conn_No_too_small = true;
                        }
                    }
                }
                #endregion
                //------------------------------------------------------
                #region Attribute 0x06
                
                if (DecodingAttribute == 0x06 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        ///Check access right
                        if (!IsAttribReadable(06))
                            SetAttributeDecodingResult(06, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(06, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} Invalid identifier of Simultaneous_Conn_No (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_41_TCP_UDP_Object");
                        }
                    }
                    else
                    {
                        try
                        {
                            array_traverse--;
                            Inactivity_Time_Out_Secs = Convert.ToUInt16(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, Data.Length));
                            SetAttributeDecodingResult(06, DecodingResult.Ready);
                        }
                        catch (Exception ex)
                        {
                            SetAttributeDecodingResult(06, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} This element is not 11 (unsigned),invalid identifier of value (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_41_TCP_UDP_Object", ex);
                        }
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
                        OBISIndex, OBISIndex.OBISIndex, "Decode_Data_Class_41_TCP_UDP_Object", (int)DLMSErrors.ErrorDecoding_Type), ex);
                }
            }
        }

        #endregion

        #region Member Methods

        public override object Clone()
        {
            Class_41 cloned = new Class_41(this);
            return cloned;
        }

        /// <summary>
        /// Returns the String representation of the Class_41 object
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            String baseStr = base.ToString();
            StringBuilder strVal = new StringBuilder();

            strVal.AppendFormat(",TCP_UDP Port:{0}:{1}", TCP_UDP_Port, GetAttributeDecodingResult(2));
            strVal.AppendFormat(",OBIS Code IP Reference:{0}:{1}", DLMS_Common.ArrayToHexString(IP_Reference), GetAttributeDecodingResult(3));
            strVal.AppendFormat(",Max Segment Size{0}:{1}", Max_Segment_Size, GetAttributeDecodingResult(4));
            if (GetAttributeDecodingResult(4) == DecodingResult.Ready && flg_MSS_too_small)
                strVal.AppendFormat(" Error MMS too small");
            strVal.AppendFormat(",Max Simultaneous Conn:{0}:{1}", Simultaneous_Conn_No, GetAttributeDecodingResult(5));
            if (GetAttributeDecodingResult(4) == DecodingResult.Ready && flg_Sim_Conn_No_too_small)
                strVal.AppendFormat(" Error Simultaneous Conn Zero");
            strVal.AppendFormat(",Inactivity Timeout:{0} sec:{1}", Inactivity_Time_Out_Secs, GetAttributeDecodingResult(6));

            return baseStr + strVal;
        }
        #endregion


    }
}
