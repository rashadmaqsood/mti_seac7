using System;
using System.Xml.Serialization;
using DLMS;
using DLMS.Comm;
using System.Runtime.Serialization;

namespace SharedCode.Comm.Param
{
    [XmlInclude(typeof(Param_Clock_Caliberation))]
    [Serializable]
    public class Param_Clock_Caliberation:IParam, ICloneable
    {
        private readonly static DateTime DefaultDate = new DateTime(2004, 1, 1);
        [XmlElement("Begin_Date", typeof(DateTime))]
        public DateTime Begin_Date;
        [XmlElement("End_Date", typeof(DateTime))]
        public DateTime End_Date;
        [XmlElement("Set_Time", typeof(DateTime))]
        public DateTime Set_Time;
        public bool Enable_Day_Light_Saving_FLAG;
        public sbyte DayLight_Saving_Deviation;

        public ushort Clock_Caliberation_PPM;
        public bool Enable_Caliberation_FLAG;
        public bool PPM_Add_FLAG;

        public Param_Clock_Caliberation()
        {
            this.Set_Time = DateTime.Now;
        }

        //public OBIS_data_from_GUI[] encode_ALL_attrib()
        //{

        //    OBIS_data_from_GUI[] structToReturn = new OBIS_data_from_GUI[2];
        //    int i = 0;
        //    structToReturn[i++] = encode_Set_Time();
        //    Array.Resize(ref structToReturn, i);
        //    return structToReturn;
        //}

        #region Meter Date Time Value

        public struct OBIS_data_from_GUI
        {
            public Get_Index OBIS_code;
            public byte attribute;
            public DataTypes Type;
            public object Data_Array;
            public DataTypes Sub_type;
            public double Data;
            public Data_Access_Result Data_Result;
        }
        public OBIS_data_from_GUI encode_Set_Time()
        {
            OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
            obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Meter_Clock;
            obj_OBIS_data_from_GUI.attribute = 2;
            obj_OBIS_data_from_GUI.Type = DataTypes._A09_octet_string;
            obj_OBIS_data_from_GUI.Data_Array = Set_Time;             // Time from the GUI
            return obj_OBIS_data_from_GUI;
        }

        public Base_Class Encode_Date_Time(GetSAPEntry CommObjectGetter)
        {
            try
            {
                Class_8 Clock_Caliberation = (Class_8)CommObjectGetter.Invoke(Get_Index.Meter_Clock);
                Clock_Caliberation.EncodingAttribute = 2;
                Clock_Caliberation.Date_Time_Value = new StDateTime();
                Clock_Caliberation.Date_Time_Value.SetDateTime(Set_Time);
                return Clock_Caliberation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Decode_Date_Time(Base_Class arg)
        {
            try
            {
                if (arg.GetType() == typeof(Class_8))
                {
                    Class_8 Meter_Clock = (Class_8)arg;
                    ///Verify data Receiced/OBIS/ETC
                    if (Meter_Clock.GetAttributeDecodingResult(2) == DecodingResult.Ready)  ///Update Only If Valid
                    {
                        if (Meter_Clock.Date_Time_Value.IsDateTimeConvertible)
                        {
                            Set_Time = Meter_Clock.Date_Time_Value.GetDateTime();
                        }
                        else
                        {
                            if (!Meter_Clock.Date_Time_Value.IsTimeConvertible)
                                Meter_Clock.Date_Time_Value.Hundred = 0x00;
                            Set_Time = Meter_Clock.Date_Time_Value.GetDateTime();

                        }

                    }
                }
                else if(arg.GetType() == typeof(Class_3))
                {
                    Class_3 Meter_Clock = (Class_3)arg;
                    if (Meter_Clock.GetAttributeDecodingResult(2) == DecodingResult.Ready)  ///Update Only If Valid
                    {
                        Set_Time = BasicEncodeDecode.Decode_DateTime(Meter_Clock.Value_Array);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Daylight Saving BEGIN Date & Time
        public Base_Class Encode_Daylight_Saving_BEGIN_Date_Time(GetSAPEntry CommObjectGetter)
        {
            try
            {
                Class_8 Clock_Caliberation = (Class_8)CommObjectGetter.Invoke(Get_Index.Meter_Clock);
                Clock_Caliberation.EncodingAttribute = 5;
                Clock_Caliberation.Daylight_Savings_Begin = new StDateTime();
                Clock_Caliberation.Daylight_Savings_Begin.SetDateTime(Begin_Date);
                Clock_Caliberation.Daylight_Savings_Begin.Year = StDateTime.NullYear;
                return Clock_Caliberation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Decode_Daylight_Saving_BEGIN_Date_Time(Base_Class arg)
        {
            try
            {
                Class_8 Meter_Clock = (Class_8)arg;
                ///Verify data Receiced/OBIS/ETC
                if (Meter_Clock.GetAttributeDecodingResult(5) == DecodingResult.Ready)  ///Update Only If Valid
                {
                    if (Meter_Clock.Daylight_Savings_Begin.IsDateConvertible)
                        Begin_Date = Meter_Clock.Daylight_Savings_Begin.GetDate();
                    else
                    {
                        if (Meter_Clock.Daylight_Savings_Begin.Year == StDateTime.NullYear)
                            Meter_Clock.Daylight_Savings_Begin.Year = (ushort)DefaultDate.Year;
                        Begin_Date = Meter_Clock.Daylight_Savings_Begin.GetDate();
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Daylight Saving END Date & Time
        public Base_Class Encode_Daylight_Saving_END_Date_Time(GetSAPEntry CommObjectGetter)
        {
            try
            {
                Class_8 Clock_Caliberation = (Class_8)CommObjectGetter.Invoke(Get_Index.Meter_Clock);
                Clock_Caliberation.EncodingAttribute = 6;
                Clock_Caliberation.Daylight_Savings_End = new StDateTime();
                Clock_Caliberation.Daylight_Savings_End.SetDate(End_Date);
                Clock_Caliberation.Daylight_Savings_End.Year = StDateTime.NullYear;
                return Clock_Caliberation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Decode_Daylight_Saving_END_Date_Time(Base_Class arg)
        {
            try
            {
                Class_8 Meter_Clock = (Class_8)arg;
                ///Verify data Receiced/OBIS/ETC
                if (Meter_Clock.GetAttributeDecodingResult(6) == DecodingResult.Ready)  ///Update Only If Valid
                {
                    if (Meter_Clock.Daylight_Savings_End.IsDateConvertible)
                        Begin_Date = Meter_Clock.Daylight_Savings_End.GetDate();
                    else
                    {
                        if (Meter_Clock.Daylight_Savings_End.Year == StDateTime.NullYear)
                            Meter_Clock.Daylight_Savings_End.Year = (ushort)DefaultDate.Year;
                        Begin_Date = Meter_Clock.Daylight_Savings_Begin.GetDate();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Daylight Saving Enable Flag
        public Base_Class Encode_Daylight_Saving_Enable(GetSAPEntry CommObjectGetter)
        {
            try
            {
                Class_8 Clock_Caliberation = (Class_8)CommObjectGetter.Invoke(Get_Index.Meter_Clock);
                Clock_Caliberation.EncodingAttribute = 8;
                Clock_Caliberation.flg_Daylight_Savings_Enabled = Enable_Day_Light_Saving_FLAG;
                return Clock_Caliberation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Decode_Daylight_Saving_Enable(Base_Class arg)
        {
            try
            {
                Class_8 Meter_Clock = (Class_8)arg;
                ///Verify data Receiced/OBIS/ETC
                if (Meter_Clock.GetAttributeDecodingResult(7) == DecodingResult.Ready)          ///UPdate Only If Value Received Correctly
                    Enable_Day_Light_Saving_FLAG = Meter_Clock.flg_Daylight_Savings_Enabled;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Daylight Saving Deviation

        public Base_Class Encode_Daylight_Saving_Deviation(GetSAPEntry CommObjectGetter)
        {
            try
            {
                Class_8 Clock_Caliberation = (Class_8)CommObjectGetter.Invoke(Get_Index.Meter_Clock);
                Clock_Caliberation.EncodingAttribute = 7;
                Clock_Caliberation.Daylight_Savings_Deviation = DayLight_Saving_Deviation;
                return Clock_Caliberation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Decode_Daylight_Saving_Deviation(Base_Class arg)
        {
            try
            {
                Class_8 Meter_Clock = (Class_8)arg;
                ///Verify data Receiced/OBIS/ETC
                if (Meter_Clock.GetAttributeDecodingResult(7) == DecodingResult.Ready)          ///UPdate Only If Value Received Correctly
                    DayLight_Saving_Deviation = Meter_Clock.Daylight_Savings_Deviation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Clock Calib Flags

        public Base_Class Encode_Clock_Calib_Flags(GetSAPEntry CommObjectGetter)
        {
            try
            {
                Class_1 Clock_Calib = (Class_1)CommObjectGetter.Invoke(Get_Index.Clock_Caliberation_Flags);
                Clock_Calib.EncodingAttribute = 2;
                Clock_Calib.EncodingType = DataTypes._A11_unsigned;
                byte TFlags = 0;
                if (Enable_Caliberation_FLAG)
                    TFlags |= 1;
                if (PPM_Add_FLAG)
                    TFlags |= 2;
                Clock_Calib.Value = TFlags;
                ///Clock_Caliberation.Daylight_Savings_Deviation = DayLight_Saving_Deviation;
                return Clock_Calib;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Decode_Clock_Calib_Flags(Base_Class arg)
        {
            try
            {
                Class_1 Clock_Calib = (Class_1)arg;
                ///Verify data Receiced/OBIS/ETC
                if (Clock_Calib.GetAttributeDecodingResult(2) == DecodingResult.Ready)  ///Updated Values Received Correctly
                {
                    byte TFlags = Convert.ToByte(Clock_Calib.Value);
                    if ((TFlags & 0x01) != 0)
                        Enable_Caliberation_FLAG = true;
                    else
                        Enable_Caliberation_FLAG = false;
                    if ((TFlags & 0x02) != 0)
                        PPM_Add_FLAG = true;
                    else
                        PPM_Add_FLAG = false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Clock Calib PPM

        public Base_Class Encode_Clock_Calib_PPM(GetSAPEntry CommObjectGetter)
        {
            try
            {
                Class_1 Clock_Calib = (Class_1)CommObjectGetter.Invoke(Get_Index.Clock_Caliberation);
                Clock_Calib.EncodingAttribute = 2;
                Clock_Calib.EncodingType = DataTypes._A12_long_unsigned;
                Clock_Calib.Value = Clock_Caliberation_PPM;
                ///Clock_Caliberation.Daylight_Savings_Deviation = DayLight_Saving_Deviation;
                return Clock_Calib;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Decode_Clock_Calib_PPM(Base_Class arg)
        {
            try
            {
                Class_1 Clock_Calib = (Class_1)arg;
                ///Verify data Receiced/OBIS/ETC
                if (Clock_Calib.GetAttributeDecodingResult(2) == DecodingResult.Ready)  ///Update Values Received Correctly
                    Clock_Caliberation_PPM = Convert.ToUInt16(Clock_Calib.Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
