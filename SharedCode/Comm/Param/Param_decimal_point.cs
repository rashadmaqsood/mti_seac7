using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DLMS.Comm;
using DLMS;

namespace SharedCode.Comm.Param
{
    [Serializable]
    [XmlInclude(typeof(Param_Decimal_Point))]
    public class Param_Decimal_Point :  ICloneable,IParam
    {
        [XmlElement("BillingEnergy", Type = typeof(byte))]
        public byte Billing_Energy;
        [XmlElement("BillingMDI", Type = typeof(byte))]
        public byte Billing_MDI;
        [XmlElement("InstataneousVoltage", Type = typeof(byte))]
        public byte Instataneous_Voltage;
        [XmlElement("InstataneousCurrent", Type = typeof(byte))]
        public byte Instataneous_Current;
        [XmlElement("InstataneousPower", Type = typeof(byte))]
        public byte Instataneous_Power;
        [XmlElement("InstataneousMDI", Type = typeof(byte))]
        public byte Instataneous_MDI;

        #region Encode/Decode Functions

        public OBIS_data_from_GUI encode_DecimalPoint()
        {
            OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
            obj_OBIS_data_from_GUI.OBIS_code = Get_Index._DecimalPoint;
            obj_OBIS_data_from_GUI.attribute = 2;
            obj_OBIS_data_from_GUI.Type = DataTypes._A09_octet_string;
            byte[] bytearray = new byte[6];
            bytearray[0] = Billing_Energy;
            bytearray[1] = Billing_MDI;
            bytearray[2] = Instataneous_Voltage;
            bytearray[3] = Instataneous_Current;
            bytearray[4] = Instataneous_Power;
            bytearray[5] = Instataneous_MDI;
            obj_OBIS_data_from_GUI.Data_Array = bytearray;

            return obj_OBIS_data_from_GUI;
        }

        public Base_Class Encode_DecimalPoint(GetSAPEntry CommObjectGetter)
        {
            Class_1 DecimalPoint_obj = (Class_1)CommObjectGetter.Invoke(Get_Index._DecimalPoint);
            DecimalPoint_obj.EncodingAttribute = 2;
            DecimalPoint_obj.EncodingType = DataTypes._A09_octet_string;

            byte[] bytearray = new byte[6];
            bytearray[0] = Billing_Energy;
            bytearray[1] = Billing_MDI;
            bytearray[2] = Instataneous_Voltage;
            bytearray[3] = Instataneous_Current;
            bytearray[4] = Instataneous_Power;
            bytearray[5] = Instataneous_MDI;
            DecimalPoint_obj.Value_Array = bytearray;
            return DecimalPoint_obj;

        }

        public void Decode_DecimalPoint(Base_Class arg)
        {
            try
            {
                Class_1 DecimalPoint = (Class_1)arg;
                ///Verify data Receiced/OBIS/ETC
                if (DecimalPoint.GetAttributeDecodingResult(2) == DecodingResult.Ready)
                {
                    byte[] bytearray = new byte[6];
                    bytearray = (byte[])DecimalPoint.Value_Array;
                    Billing_Energy = bytearray[0];
                    Billing_MDI = bytearray[1];
                    Instataneous_Voltage = bytearray[2];
                    Instataneous_Current = bytearray[3];
                    Instataneous_Power = bytearray[4];
                    Instataneous_MDI = bytearray[5];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
   
        #endregion      

        public object Clone()
        {
            return MemberwiseClone();
        }
    } 
}
