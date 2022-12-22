using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS;
using TCP_Communication;
using ucCustomControl;
using comm;
using System.Xml.Serialization;
using DLMS.Comm;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace comm
{
    [Serializable]
    [XmlInclude(typeof(Param_MDI_parameters))]
    public class Param_MDI_parameters : IParam, ICloneable
    {
        #region Data_Members

        [XmlElement("Min_Interval_ManualReset", typeof(byte))]
        public byte Minimum_Time_Interval_Between_Resets_In_case_of_Manual_Reset;
        [XmlElement("Min_Time_Unit", typeof(byte))]
        public byte Min_Time_Unit;
        [XmlElement("MDI_Interval", typeof(uint))]
        public uint MDI_Interval;
        [XmlElement("Slide_Count", typeof(ushort))]
        public ushort Roll_slide_count;
        [XmlElement("Auto_Reset_Date", typeof(StDateTime))]
        public StDateTime Auto_reset_date;

        #region MDI_Flags

        [XmlIgnore()]
        public bool FLAG_Auto_Reset_0;
        [XmlIgnore()]
        public bool FLAG_Manual_Reset_by_button_1;
        [XmlIgnore()]
        public bool FLAG_Manual_Reset_by_remote_command_2;
        [XmlIgnore()]
        public bool Reserved_Flag_3;
        [XmlIgnore()]
        public bool FLAG_Manual_Reset_in_PowerDown_Mode;
        [XmlIgnore()]
        public bool FLAG_Auto_Reset_in_PowerDown_Mode;
        [XmlIgnore()]
        public bool Reserved_Flag_6;
        [XmlIgnore()]
        public bool Reserved_Flag_7;

        #endregion

        #endregion

        #region Property

        [XmlElement("MDI_RAW_Flags", typeof(Byte))]
        public byte Raw_Flags
        {
            get
            {
                ///Encoding Flags Byte
                byte Flags = 0x00;
                Flags += (Reserved_Flag_7) ? (byte)0x80 : (byte)0x00;
                Flags += (Reserved_Flag_6) ? (byte)0x40 : (byte)0x00;
                Flags += (FLAG_Auto_Reset_in_PowerDown_Mode) ? (byte)0x20 : (byte)0x00;
                Flags += (FLAG_Manual_Reset_in_PowerDown_Mode) ? (byte)0x10 : (byte)0x00;
                Flags += (Reserved_Flag_3) ? (byte)0x08 : (byte)0x00;
                Flags += (FLAG_Manual_Reset_by_remote_command_2) ? (byte)0x04 : (byte)0x00;
                Flags += (FLAG_Manual_Reset_by_button_1) ? (byte)0x02 : (byte)0x00;
                Flags += (FLAG_Auto_Reset_0) ? (byte)0x01 : (byte)0x00;
                return Flags;
            }
            set
            {
                ///Decoding Flags Byte
                byte Flags = value;
                Reserved_Flag_7 = ((Flags & 0x80) > 0) ? true : false;
                Reserved_Flag_6 = ((Flags & 0x40) > 0) ? true : false;
                FLAG_Auto_Reset_in_PowerDown_Mode = ((Flags & 0x20) > 0) ? true : false;
                FLAG_Manual_Reset_in_PowerDown_Mode = ((Flags & 0x10) > 0) ? true : false;
                Reserved_Flag_3 = ((Flags & 0x08) > 0) ? true : false;
                FLAG_Manual_Reset_by_remote_command_2 = ((Flags & 0x04) > 0) ? true : false;
                FLAG_Manual_Reset_by_button_1 = ((Flags & 0x02) > 0) ? true : false;
                FLAG_Auto_Reset_0 = ((Flags & 0x01) > 0) ? true : false;
            }
        }

        #endregion

        public Param_MDI_parameters()
        {
            Min_Time_Unit = 0;
        }

        #region Encoders_&_Deocoder

        public Base_Class[] Encode_MDI_Params(GetSAPEntry CommObjectGetter)
        {
            Base_Class[] MDIParms = new Base_Class[5];
            MDIParms[0] = Encode_Minimum_Interval_Manual_Reset(CommObjectGetter);
            MDIParms[1] = Encode_Roll_slide_count(CommObjectGetter);
            MDIParms[2] = Encode_MDI_Interval(CommObjectGetter);
            MDIParms[3] = Encode_Auto_Reset_Date(CommObjectGetter);
            MDIParms[4] = Encode_MDI_FLAGS(CommObjectGetter);
            return MDIParms;
        }

        public Base_Class Encode_Minimum_Interval_Manual_Reset(GetSAPEntry CommObjectGetter)
        {
            Class_1 MDI_Interval_CommObj = (Class_1)CommObjectGetter.Invoke(
                Get_Index.MDIParams_MinTimeIntervalBetweenResetsIncaseofManualReset);
            MDI_Interval_CommObj.EncodingAttribute = 0x02;
            MDI_Interval_CommObj.EncodingType = DataTypes._A06_double_long_unsigned;
            MDI_Interval_CommObj.Value = this.Minimum_Time_Interval_Between_Resets_In_case_of_Manual_Reset;
            return MDI_Interval_CommObj;
        }

        public Base_Class Encode_Minimum_Interval_Manual_Reset_and_unit(GetSAPEntry CommObjectGetter)
        {
            Class_1 MDI_Interval_CommObj = (Class_1)CommObjectGetter.Invoke(
                Get_Index.MDIParams_MinTimeIntervalBetweenResetsIncaseofManualReset);
            MDI_Interval_CommObj.EncodingAttribute = 0x02;
            MDI_Interval_CommObj.EncodingType = DataTypes._A09_octet_string;
            byte[] unit_value = new byte[2];
            unit_value[0] = this.Min_Time_Unit;
            unit_value[1] = this.Minimum_Time_Interval_Between_Resets_In_case_of_Manual_Reset;
            MDI_Interval_CommObj.Value_Array = unit_value;
            return MDI_Interval_CommObj;
        }

        public void Decode_Minimum_Interval_Manual_Reset(Base_Class MDI_Interval_CommObj)
        {
            if (MDI_Interval_CommObj.INDEX == Get_Index.MDIParams_MinTimeIntervalBetweenResetsIncaseofManualReset &&
                MDI_Interval_CommObj.GetAttributeDecodingResult(2) == DecodingResult.Ready)
            //this.Minimum_Time_Interval_Between_Resets_In_case_of_Manual_Reset =
            //Convert.ToByte(((Class_1)MDI_Interval_CommObj).Value);
            {
                byte[] unit_value = new byte[2];
                unit_value = (byte[])(((Class_1)MDI_Interval_CommObj).Value_Array);
                this.Min_Time_Unit = unit_value[0];
                this.Minimum_Time_Interval_Between_Resets_In_case_of_Manual_Reset = unit_value[1];
            }
        }

        public Base_Class Encode_MDI_Interval(GetSAPEntry CommObjectGetter)
        {
            Class_5 MDI_Params_CommObj = (Class_5)CommObjectGetter.Invoke(Get_Index.MDI_Parameters);
            MDI_Params_CommObj.EncodingAttribute = 0x08;
            MDI_Params_CommObj.period = this.MDI_Interval * 60;
            return MDI_Params_CommObj;
        }

        public void Decode_MDI_Interval(Base_Class MDI_Interval_CommObj)
        {
            if (MDI_Interval_CommObj.INDEX == Get_Index.MDI_Parameters &&
                MDI_Interval_CommObj.GetAttributeDecodingResult(0x08) == DecodingResult.Ready)
                this.MDI_Interval = Convert.ToUInt32(((Class_5)MDI_Interval_CommObj).period / 60);
        }

        public Base_Class Encode_Roll_slide_count(GetSAPEntry CommObjectGetter)
        {
            Class_5 MDI_Params_CommObj = (Class_5)CommObjectGetter.Invoke(Get_Index.MDI_Parameters);
            MDI_Params_CommObj.EncodingAttribute = 0x09;
            MDI_Params_CommObj.periodCount = this.Roll_slide_count;
            return MDI_Params_CommObj;
        }

        public void Decode_Roll_slide_count(Base_Class MDI_Interval_CommObj)
        {
            if (MDI_Interval_CommObj.INDEX == Get_Index.MDI_Parameters &&
                MDI_Interval_CommObj.GetAttributeDecodingResult(0x09) == DecodingResult.Ready)
                this.Roll_slide_count = Convert.ToUInt16(((Class_5)MDI_Interval_CommObj).periodCount);
        }

        public Base_Class Encode_Auto_Reset_Date(GetSAPEntry CommObjectGetter)
        {
            Class_22 MDI_Params_CommObj = (Class_22)CommObjectGetter.Invoke(Get_Index.MDIParams_Auto_Reset_Date);
            MDI_Params_CommObj.EncodingAttribute = 0x04;
            if (this.Auto_reset_date != null)   ///Must be zero
            {
                //this.Auto_reset_date.Second = 0x00;
                this.Auto_reset_date.Hundred = 0x00;
            }
            if (MDI_Params_CommObj.executionTimeList == null)
                MDI_Params_CommObj.executionTimeList = new List<StDateTime>();
            else if (MDI_Params_CommObj.executionTimeList.Count > 0)
                MDI_Params_CommObj.executionTimeList.Clear();
            MDI_Params_CommObj.executionTimeList.Add(this.Auto_reset_date);
            return MDI_Params_CommObj;
        }

        public void Decode_Auto_Reset_Date(Base_Class MDI_Interval_CommObj)
        {
            if (MDI_Interval_CommObj.GetAttributeDecodingResult(0x04) == DecodingResult.Ready)
            {
                StDateTime DateTimeStamp = null;
                if (((Class_22)MDI_Interval_CommObj).executionTimeList != null &&
                    ((Class_22)MDI_Interval_CommObj).executionTimeList.Count > 0)
                    DateTimeStamp = ((Class_22)MDI_Interval_CommObj).executionTimeList[0];
                this.Auto_reset_date = DateTimeStamp;
            }
        }

        public Base_Class Encode_MDI_FLAGS(GetSAPEntry CommObjectGetter)
        {
            Class_1 MDI_Flagsl_CommObj = (Class_1)CommObjectGetter.Invoke(Get_Index.MDIParams_Flag);
            MDI_Flagsl_CommObj.EncodingAttribute = 0x02;
            MDI_Flagsl_CommObj.EncodingType = DataTypes._A11_unsigned;
            MDI_Flagsl_CommObj.Value = this.Raw_Flags;
            return MDI_Flagsl_CommObj;
        }

        public void Decode_MDI_FLAGS(Base_Class MDI_Flags_CommObj)
        {
            if (MDI_Flags_CommObj.INDEX == Get_Index.MDIParams_Flag &&
                MDI_Flags_CommObj.GetAttributeDecodingResult(0x02) == DecodingResult.Ready)
                this.Raw_Flags = Convert.ToByte(((Class_1)MDI_Flags_CommObj).Value);
        }

        #endregion

        public object Clone()
        {
            MemoryStream memStream = null;
            Object dp = null;
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (memStream = new MemoryStream(256))
                {
                    formatter.Serialize(memStream, this);
                    memStream.Seek(0, SeekOrigin.Begin);
                    dp = formatter.Deserialize(memStream);
                }
                return dp;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Clone Object", ex);
            }
        }
    }
}
