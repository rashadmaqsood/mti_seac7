using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using DLMS.Comm;

namespace DLMS
{
    /// <summary>
    /// IPv4 setup (class_id: 42, version: 0) allows modeling the setup of the IPv4 layer
    /// This Class allows modeling the setup of the IPv4 layer, handling all information related to the IP Address settings associated to a given device and to a lower layer connection on which these settings are used.
    /// There shall be an instance of this Class in a device for each different network interface implemented.
    /// For example, if a device has two interfaces (using the TCP/IP profile on both of them), there shall be two instances of the IPv4 setup Class in that device: one for each of these interfaces.   
    /// </summary>
    public class Class_42 : Base_Class
    {
        #region Data_Members
        
        /// <summary>
        /// References a Data link layer (e.g. Ethernet or PPP)
        /// </summary>
        /// <remarks>
        /// References a Data link layer setup object by its logical name.
        /// The referenced object contains information about the specific settings of the data link layer supporting the IP layer.
        /// </remarks>
        public byte[] DataLink_Reference;
        
        /// <summary>
        /// Carries the value of the IP address (IPv4) of this physical device
        /// </summary>
        /// <remarks>
        /// Carries the value of the IP address of this physical device on the network to which the device is connected via the referenced lower layer interface.
        /// It can be either (static) or (dynamic). In the latter case, dynamic IP address assignment (for example DHCP) is used.
        /// If no IP address is assigned, the value is 0.
        /// </remarks>
        public IPAddress IP_Address;
        
        /// <summary>
        /// Contains the subnet mask.
        /// </summary>
        /// <remarks>
        /// When sub-networking is used in a network segment, each device concerned must behave conforming to the sub-networking rules. 
        /// In order to do that, the device, besides of its IP address, needs also to know, how the IP address is structured within this sub-networked segment. 
        /// The subnet_mask attribute carries this information.
        /// With IPv4, the subnet_mask is a 32 bits word, expressed exactly in the same format as an IP Address (for example 255.255.255.0), but has another meaning: the ‘0’ bits of the subnet_mask indicate the portion of the IP Address which is still used as Device_ID on a sub-networked IP Network
        /// </remarks>
        public IPAddress Subnet_Mask;
        
        /// <summary>
        /// Contains the IP Address of the gateway device.
        /// </summary>
        /// <remarks>
        /// In most IP implementations, there is code in the module that handles outgoing datagrams to decide if a datagram can be sent directly to the destination on the local network or if it must be sent to a gateway. 
        /// In order to be able to send non-local datagrams to the gateway, the device must know the IP address of the gateway device assigned to the given network segment.
        /// If no IP address is assigned, the value is 0.
        /// </remarks>
        public IPAddress Gateway_IP;
        
        /// <summary>
        /// Determine either to use DHCP (Dynamic Host Configuration Protocol) or not
        /// </summary>
        /// <remarks>
        /// When this flag is set to TRUE, the device uses DHCP (Dynamic Host Configuration Protocol) to dynamically determine the IP_address, subnet_mask and gateway_IP_address parameters.
        /// On the other hand, when this flag is set to FALSE, the IP_address, subnet_mask and gateway_IP_address parameters must be locally set.
        /// </remarks>
        public bool flg_Use_DHCP;
        
        /// <summary>
        /// The IP Address of the primary Domain Name Server (DNS).
        /// </summary>
        public IPAddress Primary_DNS_IP;
        
        /// <summary>
        /// The IP Address of the secondary Domain Name Server (DNS).
        /// </summary>
        public IPAddress Secondary_DNS_IP;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Index">OBIS code for specific entity</param>
        /// <param name="Obis_Code">Bytes representation of an OBIS code</param>
        /// <param name="No_of_Associations">maximum no of association</param>
        public Class_42(Get_Index Index, byte[] Obis_Code, UInt16 No_of_Associations)
            : base(42, 10, 3, Index, Obis_Code, No_of_Associations)
        {
            /// make room
            DataLink_Reference = new byte[6];
            IP_Address = IPAddress.Any;
            Subnet_Mask = IPAddress.Broadcast;
            Gateway_IP = IPAddress.Any;
            Primary_DNS_IP = IPAddress.Any;
            Secondary_DNS_IP = IPAddress.Any;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Obis_Code">Bytes representation of an OBIS code</param>
        /// <param name="Attribute_recieved">no of attribute associated with current object</param>
        public Class_42(byte[] Obis_Code, byte Attribute_rcieved)
            : base(42, 10, 3, Obis_Code)
        {
            DecodingAttribute = Attribute_rcieved;
            /// make room
            DataLink_Reference = new byte[6];
            IP_Address = IPAddress.Any;
            Subnet_Mask = IPAddress.Broadcast;
            Gateway_IP = IPAddress.Any;
            Primary_DNS_IP = IPAddress.Any;
            Secondary_DNS_IP = IPAddress.Any;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="OBISCodeStruct">OBIS code for a specific Object</param>
        public Class_42(StOBISCode OBISCodeStruct)
            : base(OBISCodeStruct, 10, 3)
        {
            // make room
            DataLink_Reference = new byte[6];
            /// make room
            DataLink_Reference = new byte[6];
            IP_Address = IPAddress.Any;
            Subnet_Mask = IPAddress.Broadcast;
            Gateway_IP = IPAddress.Any;
            Primary_DNS_IP = IPAddress.Any;
            Secondary_DNS_IP = IPAddress.Any;
        }

        /// <summary>
        /// Copy cunstructor
        /// </summary>
        /// <param name="obj">Class_42 Object</param>                
        public Class_42(Class_42 obj)
            : base(obj)
        {
            if (obj.DataLink_Reference != null)
                DataLink_Reference = (byte[])obj.DataLink_Reference.Clone();
            if (obj.IP_Address != null)
                IP_Address = new IPAddress(obj.IP_Address.GetAddressBytes());
            if (obj.Subnet_Mask != null)
                Subnet_Mask = new IPAddress(obj.Subnet_Mask.GetAddressBytes());
            if (obj.Gateway_IP != null)
                Gateway_IP = new IPAddress(obj.Gateway_IP.GetAddressBytes());
            flg_Use_DHCP = obj.flg_Use_DHCP;
            if (obj.Gateway_IP != null)
                Gateway_IP = new IPAddress(obj.Gateway_IP.GetAddressBytes());
            if (obj.Primary_DNS_IP != null)
                Primary_DNS_IP = new IPAddress(obj.Primary_DNS_IP.GetAddressBytes());
            if (obj.Secondary_DNS_IP != null)
                Secondary_DNS_IP = new IPAddress(obj.Secondary_DNS_IP.GetAddressBytes());
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
            UInt32 ip_Address = 0;
            UInt32 Subnet = 0;
            UInt32 Gateway = 0;
            UInt32 P_DNS = 0;
            UInt32 Sec_DNS = 0;
            bool flg_use_DHCP = false;
            
            try
            {
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
                DecoderAttribute_0(ref Data, ref array_traverse, "Decode_Data_Class_42_IPv4_Setup");
                DecoderLogicalName(ref Data, ref array_traverse, ref Obis_code_recieved, "Decode_Data_Class_42_IPv4_Setup");
                //------------------------------------------------------
                #region Attribute 0x02
                
                if (DecodingAttribute == 0x02 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check Access Rights
                        if (!IsAttribReadable(02))
                            SetAttributeDecodingResult(02, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(02, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier of value (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_42_IPv4_Setup");
                        }
                    }
                    else
                    {
                        try
                        {
                            array_traverse--; // for the function to follow it has to be done (chamchuss)
                            DataLink_Reference = BasicEncodeDecode.Decode_OctectString(Data, ref array_traverse, Data.Length);
                            SetAttributeDecodingResult(02, DecodingResult.Ready);
                        }
                        catch (Exception ex)
                        {
                            SetAttributeDecodingResult(02, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} This element is not (octet string),invalid identifier (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_42_IPv4_Setup");
                        }
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute0x03
                
                if (DecodingAttribute == 0x03 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        //Check access rights
                        if (!IsAttribReadable(03))
                            SetAttributeDecodingResult(03, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(03, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier of value (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_42_IPv4_Setup");
                        }
                    }
                    else
                    {
                        try
                        {
                            array_traverse--;
                            ip_Address = Convert.ToUInt32(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, Data.Length));
                            IP_Address = BasicEncodeDecode.Decode_IP(ip_Address);
                            SetAttributeDecodingResult(03, DecodingResult.Ready);
                        }
                        catch (Exception ex)
                        {
                            SetAttributeDecodingResult(03, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1}This element is not (double_long_unsigned),invalid identifier (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_42_IPv4_Setup");
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
                        //Check access rights
                        if (!IsAttribReadable(04))
                            SetAttributeDecodingResult(04, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(04, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier of value (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_42_IPv4_Setup");
                        }
                    }
                    else
                    {
                        ///if (current_char != (byte)DataTypes._A01_array)
                        ///{
                        //    Generate Error and return

                        //    AccessResults[3] = DecodingResult.DecodingError;

                        ///}

                        throw new DLMSDecodingException(String.Format("{0}_{1} decoder not implemented yet (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_DecoderNotIncluded), "Decode_Data_Class_42_IPv4_Setup");

                        ///further decoding work required
                        //array_traverse++;
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x05
                
                if (DecodingAttribute == 0x05 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];// null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check Access Rights
                        if (!IsAttribReadable(05))
                            SetAttributeDecodingResult(05, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(05, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier of value (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_42_IPv4_Setup");
                        }
                    }
                    else
                    {
                        //if (current_char != (byte)DataTypes._A01_array)
                        //{
                        //    // Generate Error and return
                        //    AccessResults[4] = DecodingResult.DecodingError;
                        //    throw new DLMSDecodingException("This element is not 01 (array)", "Decode_Data_Class_42_IPv4_Setup");
                        //}
                        ///AccessResults[4] = DecodingResult.Ready;
                        array_traverse++;
                        throw new DLMSDecodingException(String.Format("{0}_{1} decoder not implemented yet (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_DecoderNotIncluded), "Decode_Data_Class_42_IPv4_Setup");
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
                        // Check Access Rights
                        if (!IsAttribReadable(06))
                            SetAttributeDecodingResult(06, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(06, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier of value (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_42_IPv4_Setup");
                        }
                    }
                    else
                    {
                        try
                        {
                            array_traverse--;
                            Subnet = Convert.ToUInt32(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, Data.Length));
                            Subnet_Mask = BasicEncodeDecode.Decode_IP(Subnet);
                            SetAttributeDecodingResult(06, DecodingResult.Ready);
                        }
                        catch (Exception ex)
                        {
                            SetAttributeDecodingResult(06, DecodingResult.DecodingError);
                            
                            throw new DLMSDecodingException(String.Format("{0}_{1} This element is not (double_long_unsigned),invalid identifier (Error Code:{2})",
                                 OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_42_IPv4_Setup",ex);
                        }
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x07
                
                if (DecodingAttribute == 0x07 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check Access Rights
                        if (!IsAttribReadable(07))
                            SetAttributeDecodingResult(07, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(07, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier of value (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_42_IPv4_Setup");
                        }
                    }
                    else
                    {
                        try
                        {
                            array_traverse--;
                            Gateway = Convert.ToUInt32(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, Data.Length));
                            Gateway_IP = BasicEncodeDecode.Decode_IP(Subnet);

                            SetAttributeDecodingResult(07, DecodingResult.Ready);
                        }
                        catch (Exception ex)
                        {
                            SetAttributeDecodingResult(07, DecodingResult.DecodingError);

                            throw new DLMSDecodingException(String.Format("{0}_{1} This element is not (double_long_unsigned)_decoding Default GateWay,invalid identifier (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_42_IPv4_Setup",ex);
                        }
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x08
                
                if (DecodingAttribute == 0x08 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check Access Rights
                        if (!IsAttribReadable(08))
                            SetAttributeDecodingResult(08, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(08, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier of value (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_42_IPv4_Setup");
                        }
                    }
                    else
                    {
                        try
                        {
                            array_traverse--;
                            flg_use_DHCP = Convert.ToBoolean(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, Data.Length));
                            SetAttributeDecodingResult(08, DecodingResult.Ready);
                        }
                        catch (Exception ex)
                        {
                            SetAttributeDecodingResult(08, DecodingResult.DecodingError);

                            throw new DLMSDecodingException(String.Format("{0}_{1} This element is not (double_long_unsigned)_decoding Flg_Use_DHCP,invalid identifier (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_42_IPv4_Setup",ex);
                        }
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x09
                
                if (DecodingAttribute == 0x09 || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check Access Rights
                        if (!IsAttribReadable(09))
                            SetAttributeDecodingResult(09, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(09, DecodingResult.DecodingError);
                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier of value (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_42_IPv4_Setup");
                        }
                    }
                    else
                    {
                        try
                        {
                            array_traverse--;
                            P_DNS = Convert.ToUInt32(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, Data.Length));
                            Primary_DNS_IP = BasicEncodeDecode.Decode_IP(Subnet);
                            SetAttributeDecodingResult(09, DecodingResult.Ready);
                        }
                        catch (Exception ex)
                        {
                            SetAttributeDecodingResult(09, DecodingResult.DecodingError);
                            
                            throw new DLMSDecodingException(String.Format("{0}_{1} This element is not (double_long_unsigned)_decoding Primary DNS,invalid identifier (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_42_IPv4_Setup", ex);
                        }
                    }
                }

                #endregion
                //------------------------------------------------------
                #region Attribute 0x0A
                
                if (DecodingAttribute == 0x0A || DecodingAttribute == 0x00)
                {
                    current_char = Data[array_traverse++];
                    // null Data
                    if (current_char == (byte)DataTypes._A00_Null)
                    {
                        // Check Access Rights
                        if (!IsAttribReadable(0x0A))
                            SetAttributeDecodingResult(0x0A, DecodingResult.NoAccess);
                        else
                        {
                            SetAttributeDecodingResult(0x0A, DecodingResult.DecodingError);

                            throw new DLMSDecodingException(String.Format("{0}_{1} invalid identifier of value (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_42_IPv4_Setup");
                        }
                    }
                    else
                    {
                        try
                        {
                            array_traverse--;
                            Sec_DNS = Convert.ToUInt32(BasicEncodeDecode.Intelligent_Data_Decoder(ref Data, ref array_traverse, Data.Length));
                            Secondary_DNS_IP = BasicEncodeDecode.Decode_IP(Subnet);
                            SetAttributeDecodingResult(0x0A, DecodingResult.Ready);
                        }
                        catch (Exception ex)
                        {
                            SetAttributeDecodingResult(0x0A, DecodingResult.DecodingError);

                            throw new DLMSDecodingException(String.Format("{0}_{1} This element is not (double_long_unsigned)_decoding Secondary DNS,invalid identifier (Error Code:{2})",
                                OBISIndex, OBISIndex.OBISIndex, (int)DLMSErrors.Invalid_Type_MisMatch), "Decode_Data_Class_42_IPv4_Setup", ex);
                        }
                    }
                }

                #endregion

            }
            catch (Exception ex)
            {
                if (ex is DLMSDecodingException)
                    throw ex;
                else
                    throw new DLMSException(String.Format("{0}_{1} (Error Code:{2})", "Error occurred while encoding data",
                        "Decode_Data_Class_42_IPv4_Setup", (int)DLMSErrors.ErrorEncoding_Type), ex);
            }
        }

        #endregion

        #region Member Methods

        public override object Clone()
        {
            Class_42 cloned = new Class_42(this);
            return cloned;
        }

        /// <summary>
        /// Returns the String representation of the Class_21 object
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            try
            {
                String baseStr = base.ToString();
                StringBuilder strVal = new StringBuilder();

                strVal.AppendFormat(",OBIS DataLink Reference:{0}:{1}", DLMS_Common.ArrayToHexString(DataLink_Reference), GetAttributeDecodingResult(0x02));
                strVal.AppendFormat(",IP:{0}:{1}", IP_Address.ToString(), GetAttributeDecodingResult(0x03));
                strVal.AppendFormat(",MultiCast IP:{0}:{1}", "", GetAttributeDecodingResult(0x04));
                strVal.AppendFormat(",IP Options:{0}:{1}", "", GetAttributeDecodingResult(0x05));
                strVal.AppendFormat(",Subnet Mask:{0}:{1}", Subnet_Mask.ToString(), GetAttributeDecodingResult(0x06));
                strVal.AppendFormat(",GateWay IP:{0}:{1}", Gateway_IP.ToString(), GetAttributeDecodingResult(0x07));
                strVal.AppendFormat(",DHCP:{0}:{1}", flg_Use_DHCP, GetAttributeDecodingResult(0x08));
                strVal.AppendFormat(",Primary DNS IP:{0}:{1}", Primary_DNS_IP, GetAttributeDecodingResult(0x09));
                strVal.AppendFormat(",Secondary DNS IP:{0}:{1}", Secondary_DNS_IP, GetAttributeDecodingResult(0x0A));

                return baseStr + strVal;
            }
            catch (Exception)
            {
                return "Error Occurred In Append Format";
            }
        }

        #endregion

    }
}
