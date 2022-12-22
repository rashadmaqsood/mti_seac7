using System;
using System.Net;
using System.Xml.Serialization;
using DLMS;
using DLMS.Comm;


namespace SharedCode.Comm.Param
{
    [Serializable]
    [XmlInclude(typeof(Param_IPV4))]
    public class Param_IPV4:IParam
    {
        #region Data_Members

        [XmlElement("DirectLinkReference", Type = typeof(byte[]))]
        public byte[] DL_reference;
        [XmlElement("IP", Type = typeof(uint))]
        public uint IP;
        [XmlElement("SubnetMask", Type = typeof(uint))]
        public uint Subnet_Mask;
        [XmlElement("GatewayIP", Type = typeof(uint))]
        public uint Gateway_IP;
        [XmlElement("DHCPFlag", Type = typeof(bool))]
        public bool DHCP_Flag;
        [XmlElement("PrimaryDNS", Type = typeof(uint))]
        public uint Primary_DNS;
        [XmlElement("SecondaryDNS", Type = typeof(uint))]
        public uint Secondary_DNS; 
        
        #endregion

        #region Encoder/Decoder

        public OBIS_data_from_GUI[] encode_ALL()
        {

            OBIS_data_from_GUI[] structToReturn = new OBIS_data_from_GUI[7];

            structToReturn[0] = encode_DL_reference();
            structToReturn[1] = encode_IP();
            structToReturn[2] = encode_Subnet_Mask();
            structToReturn[3] = encode_Gateway_IP();
            structToReturn[4] = encode_DHCP_Flag();
            structToReturn[5] = encode_Primary_DNS();
            structToReturn[6] = encode_Secondary_DNS();

            return structToReturn;
        }

        public OBIS_data_from_GUI encode_DL_reference()
        {
            OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
            obj_OBIS_data_from_GUI.OBIS_code = Get_Index.IPv4;
            obj_OBIS_data_from_GUI.attribute = 2;
            obj_OBIS_data_from_GUI.Type = DataTypes._A09_octet_string;
            obj_OBIS_data_from_GUI.Data_Array = DL_reference;

            return obj_OBIS_data_from_GUI;
        }

        public OBIS_data_from_GUI encode_IP()
        {
            OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
            obj_OBIS_data_from_GUI.OBIS_code = Get_Index.IPv4;
            obj_OBIS_data_from_GUI.attribute = 3;
            obj_OBIS_data_from_GUI.Type = DataTypes._A06_double_long_unsigned;
            obj_OBIS_data_from_GUI.Data = IP;

            return obj_OBIS_data_from_GUI;
        }

        public OBIS_data_from_GUI encode_Subnet_Mask()
        {
            OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
            obj_OBIS_data_from_GUI.OBIS_code = Get_Index.IPv4;
            obj_OBIS_data_from_GUI.attribute = 6;
            obj_OBIS_data_from_GUI.Type = DataTypes._A06_double_long_unsigned;
            obj_OBIS_data_from_GUI.Data = Subnet_Mask;

            return obj_OBIS_data_from_GUI;
        }

        public OBIS_data_from_GUI encode_Gateway_IP()
        {
            OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
            obj_OBIS_data_from_GUI.OBIS_code = Get_Index.IPv4;
            obj_OBIS_data_from_GUI.attribute = 7;
            obj_OBIS_data_from_GUI.Type = DataTypes._A06_double_long_unsigned;
            obj_OBIS_data_from_GUI.Data = Gateway_IP;

            return obj_OBIS_data_from_GUI;
        }

        public OBIS_data_from_GUI encode_DHCP_Flag()
        {
            OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
            obj_OBIS_data_from_GUI.OBIS_code = Get_Index.IPv4;
            obj_OBIS_data_from_GUI.attribute = 8;
            obj_OBIS_data_from_GUI.Type = DataTypes._A03_boolean;
            obj_OBIS_data_from_GUI.Data_Array = DHCP_Flag;
            return obj_OBIS_data_from_GUI;
        }

        public OBIS_data_from_GUI encode_Primary_DNS()
        {
            OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
            obj_OBIS_data_from_GUI.OBIS_code = Get_Index.IPv4;
            obj_OBIS_data_from_GUI.attribute = 9;
            obj_OBIS_data_from_GUI.Type = DataTypes._A06_double_long_unsigned;
            obj_OBIS_data_from_GUI.Data = Primary_DNS;

            return obj_OBIS_data_from_GUI;
        }

        public OBIS_data_from_GUI encode_Secondary_DNS()
        {
            OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
            obj_OBIS_data_from_GUI.OBIS_code = Get_Index.IPv4;
            obj_OBIS_data_from_GUI.attribute = 10;
            obj_OBIS_data_from_GUI.Type = DataTypes._A06_double_long_unsigned;
            obj_OBIS_data_from_GUI.Data = Secondary_DNS;
            return obj_OBIS_data_from_GUI;
        }

        public ulong Decode_Any(Base_Class arg, byte Class_ID, byte Attribute)
        {
            try
            {
                ulong temp = 0;
                if (Class_ID == 42)
                {
                    IPAddress _ip;

                    Class_42 temp_obj = (Class_42)arg;
                    switch (Attribute)
                    {
                        case 3:
                            _ip = temp_obj.IP_Address;
                            temp = (ulong)DLMS_Common.IPAddressToLong(_ip);
                            break;
                        case 6:
                            _ip = temp_obj.Subnet_Mask;
                            temp = (ulong)DLMS_Common.IPAddressToLong(_ip);
                            break;
                        case 7:
                            _ip = temp_obj.Gateway_IP;
                            temp = (ulong)DLMS_Common.IPAddressToLong(_ip);
                            break;
                        case 9:
                            _ip = temp_obj.Primary_DNS_IP;
                            temp = (ulong)DLMS_Common.IPAddressToLong(_ip);
                            break;
                        case 10:
                            _ip = temp_obj.Secondary_DNS_IP;
                            temp = (ulong)DLMS_Common.IPAddressToLong(_ip);
                            break;
                        default:
                            temp = 0;
                            break;
                    }
                }
                return temp;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public byte[] Decode_byte_array(Base_Class arg, byte Class_ID, byte attribute)
        {
            try
            {
                if (Class_ID == 42 && attribute == 2)
                {
                    Class_42 temp_obj = (Class_42)arg;
                    byte[] dtArray = temp_obj.DataLink_Reference;
                    return dtArray;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Decode_bool(Base_Class arg, byte Class_ID, byte attribute)
        {
            try
            {
                if (Class_ID == 42 && attribute == 8)
                {
                    Class_42 temp_obj = (Class_42)arg;
                    bool dtArray = temp_obj.flg_Use_DHCP;
                    return dtArray;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        } 

        #endregion

    }
}
