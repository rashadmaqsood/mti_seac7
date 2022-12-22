using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS;
using TCP_Communication;
using ucCustomControl;
using comm;
using System.Xml;
using System.Xml.Serialization;
using DLMS.Comm;

namespace comm
{
    [Serializable]
    [XmlInclude(typeof(Param_Modem_Initialize))]
    public class Param_Modem_Initialize : IParam, ICloneable
    {
        #region Data_Members

        public string Username;
        public string Password;
        public string APN;
        [XmlElement("PINCode", Type = typeof(ushort))]
        public ushort PIN_code;

        #endregion

        #region Encoder/Decoders

        public Base_Class Encode_Username(GetSAPEntry GetSAPEntry)
        {
            Base_Class ModemBasics = GetSAPEntry.Invoke(Get_Index.ModemBasics_User_Name);
            ModemBasics.EncodingAttribute = 0x02;
            ((Class_1)ModemBasics).Value_Array = ASCIIEncoding.ASCII.GetBytes(this.Username);
            return ModemBasics;
        }

        public Base_Class Encode_Password(GetSAPEntry GetSAPEntry)
        {
            Base_Class ModemBasics = GetSAPEntry.Invoke(Get_Index.ModemBasics_Password);
            ModemBasics.EncodingAttribute = 0x02;
            ((Class_1)ModemBasics).Value_Array = ASCIIEncoding.ASCII.GetBytes(this.Password);
            return ModemBasics;
        }

        public Base_Class Encode_APN(GetSAPEntry GetSAPEntry)
        {
            Base_Class ModemBasics = GetSAPEntry.Invoke(Get_Index.GPRS_Modem_Configuration);
            ModemBasics.EncodingAttribute = 0x02;
            ((Class_45)ModemBasics).APN = this.APN;
            return ModemBasics;
        }

        public Base_Class Encode_PIN(GetSAPEntry GetSAPEntry)
        {
            Base_Class ModemBasics = GetSAPEntry.Invoke(Get_Index.GPRS_Modem_Configuration);
            ModemBasics.EncodingAttribute = 0x03;
            ((Class_45)ModemBasics).Pin_Code = this.PIN_code;
            return ModemBasics;
        }

        public void Decode_Username(Base_Class ModemBasics)
        {
            this.Username = BasicEncodeDecode.Decode_String(ModemBasics);
        }

        public void Decode_Password(Base_Class ModemBasics)
        {
            this.Password = BasicEncodeDecode.Decode_String(ModemBasics);
        }

        public void Decode_ModemGPRSConfigs(Base_Class ModemBasics)
        {
            Class_45 ModemBasics_Obj = (Class_45)ModemBasics;
            if (ModemBasics_Obj.GetAttributeDecodingResult(2) == DecodingResult.Ready)       ///APN
            {
                this.APN = ModemBasics_Obj.APN;
            }
            if (ModemBasics_Obj.GetAttributeDecodingResult(3) == DecodingResult.Ready)       ///APN
            {
                this.PIN_code = ModemBasics_Obj.Pin_Code;
            }
        }

        public OBIS_data_from_GUI encode_PIN_code()
        {
            OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
            //     obj_OBIS_data_from_GUI.OBIS_code = Get_Index.PT_Ratio_Denominator;
            obj_OBIS_data_from_GUI.attribute = 2;
            obj_OBIS_data_from_GUI.Type = DataTypes._A09_octet_string;
            obj_OBIS_data_from_GUI.Data_Array = PIN_code;

            return obj_OBIS_data_from_GUI;
        }

        public double Decode_Any(Base_Class arg, byte Class_ID)
        {
            try
            {
                if (Class_ID == 1)
                {
                    Class_1 temp_obj = (Class_1)arg;
                    double temp = Convert.ToDouble(temp_obj.Value);
                    return temp;
                }
                if (Class_ID == 3)
                {
                    Class_3 temp_obj = (Class_3)arg;
                    double temp = Convert.ToDouble(temp_obj.Value);
                    return temp;
                }
                if (Class_ID == 4)
                {
                    Class_4 temp_obj = (Class_4)arg;
                    double temp = Convert.ToDouble(temp_obj.Value);
                    return temp;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string Decode_string(Base_Class arg, byte Class_ID)
        {
            try
            {
                if (Class_ID == 1)
                {
                    Class_1 temp_obj = (Class_1)arg;
                    if (temp_obj.GetAttributeDecodingResult(2) == DecodingResult.Ready)
                    {
                        byte[] dtArray = (byte[])temp_obj.Value_Array;
                        string temp = "---";
                        if (temp_obj.Value_Array.Length != 0)
                            temp = new String(ASCIIEncoding.ASCII.GetChars(dtArray));
                        return temp;
                    }
                }
                if (Class_ID == 3)
                {
                    Class_3 temp_obj = (Class_3)arg;
                    if (temp_obj.GetAttributeDecodingResult(2) == DecodingResult.Ready)
                    {
                        byte[] dtArray = (byte[])temp_obj.Value_Array;
                        string temp = "---";
                        if (temp_obj.Value_Array.Length != 0)
                            temp = new String(ASCIIEncoding.ASCII.GetChars(dtArray));
                        return temp;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
