using System;
using System.Xml.Serialization;
using DLMS;
using DLMS.Comm;

namespace comm
{
    [Serializable]
    [XmlInclude(typeof(Param_TCP_UDP))]
    public class Param_TCP_UDP:IParam,ICloneable
    {
        #region Data_Members

        [XmlElement("IPPort", typeof(ushort))]
        public ushort IP_Port;
        [XmlElement("MaxSegmentationSize", typeof(ushort))]
        public ushort Max_Segmentation_Size;
        [XmlElement("InactivityTimeOut", typeof(ushort))]
        public ushort Inactivity_Time_Out;
        [XmlElement("MaxSimulaneousConnections", typeof(ushort))]
        public ushort Max_no_of_simulaneous_connections;
        [XmlElement("IPReference", typeof(ushort))]
        public ushort IP_reference; 

        #endregion
        
        #region Encoder/Decoder

        public OBIS_data_from_GUI[] encode_ALL()
        {

            OBIS_data_from_GUI[] structToReturn = new OBIS_data_from_GUI[5];

            structToReturn[0] = encode_IP_Port();
            structToReturn[1] = encode_Max_Segmentation_Size();
            structToReturn[2] = encode_Inactivity_Time_Out();
            structToReturn[3] = encode_Max_no_of_simulaneous_connections();
            structToReturn[4] = encode_IP_reference();

            return structToReturn;
        }

        public OBIS_data_from_GUI encode_IP_Port()
        {
            OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
            obj_OBIS_data_from_GUI.OBIS_code = Get_Index.TCP_UDP_Setup;
            obj_OBIS_data_from_GUI.attribute = 2;
            obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
            obj_OBIS_data_from_GUI.Data = IP_Port;

            return obj_OBIS_data_from_GUI;
        }

        public OBIS_data_from_GUI encode_Max_Segmentation_Size()
        {
            OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
            obj_OBIS_data_from_GUI.OBIS_code = Get_Index.TCP_UDP_Setup;
            obj_OBIS_data_from_GUI.attribute = 4;
            obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
            obj_OBIS_data_from_GUI.Data = Max_Segmentation_Size;

            return obj_OBIS_data_from_GUI;
        }

        public OBIS_data_from_GUI encode_Inactivity_Time_Out()
        {
            OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
            obj_OBIS_data_from_GUI.OBIS_code = Get_Index.TCP_UDP_Setup;
            obj_OBIS_data_from_GUI.attribute = 6;
            obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
            obj_OBIS_data_from_GUI.Data = Inactivity_Time_Out;

            return obj_OBIS_data_from_GUI;
        }

        public OBIS_data_from_GUI encode_Max_no_of_simulaneous_connections()
        {
            OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
            obj_OBIS_data_from_GUI.OBIS_code = Get_Index.TCP_UDP_Setup;
            obj_OBIS_data_from_GUI.attribute = 5;
            obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
            obj_OBIS_data_from_GUI.Data = Max_no_of_simulaneous_connections;

            return obj_OBIS_data_from_GUI;
        }

        public OBIS_data_from_GUI encode_IP_reference()
        {
            OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
            obj_OBIS_data_from_GUI.OBIS_code = Get_Index.TCP_UDP_Setup;
            obj_OBIS_data_from_GUI.attribute = 3;
            obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
            obj_OBIS_data_from_GUI.Data = IP_reference;

            return obj_OBIS_data_from_GUI;
        } 
        
        #endregion

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
