using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS;
using TCP_Communication;
using ucCustomControl;
using comm;
using DLMS.Comm;

namespace comm
{
  
    public class Modem_Basics
    {
        public string APN_Name;
        public string User_Name;
        public string Password;
        public sbyte PIN_Code;

        public OBIS_data_from_GUI encode_APN_Name()
        {
            OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.;
            obj_OBIS_data_from_GUI.attribute = 2;
            obj_OBIS_data_from_GUI.Type = DataTypes._A09_octet_string;
            obj_OBIS_data_from_GUI.Data_Array = APN_Name;

            return obj_OBIS_data_from_GUI;
        }

        public OBIS_data_from_GUI encode_User_Name()
        {
            OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
            obj_OBIS_data_from_GUI.OBIS_code = Get_Index.ModemBasics_User_Name;
            obj_OBIS_data_from_GUI.attribute = 2;
            obj_OBIS_data_from_GUI.Type = DataTypes._A09_octet_string;
            obj_OBIS_data_from_GUI.Data_Array = User_Name;

            return obj_OBIS_data_from_GUI;
        }

        public OBIS_data_from_GUI encode_Password()
        {
            OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
            obj_OBIS_data_from_GUI.OBIS_code = Get_Index.ModemBasics_Password;
            obj_OBIS_data_from_GUI.attribute = 2;
            obj_OBIS_data_from_GUI.Type = DataTypes._A09_octet_string;
            obj_OBIS_data_from_GUI.Data_Array = Password;

            return obj_OBIS_data_from_GUI;
        }

        public OBIS_data_from_GUI encode_PIN_Code()
        {
            OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
            //obj_OBIS_data_from_GUI.OBIS_code = Get_Index.com;
            obj_OBIS_data_from_GUI.attribute = 2;
            obj_OBIS_data_from_GUI.Type = DataTypes._A09_octet_string;
            obj_OBIS_data_from_GUI.Data_Array = PIN_Code;

            return obj_OBIS_data_from_GUI;
        }

    }
  
}
