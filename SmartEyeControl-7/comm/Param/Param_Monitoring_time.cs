using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Xml.Serialization;
using AccurateOptocomSoftware.comm;
using DLMS;
using DLMS.Comm;
namespace comm
{
    [Serializable]
    [XmlInclude(typeof(Param_Monitoring_time))]
    public class Param_Monitoring_time : ISerializable, ICloneable, IParam
    {
        #region Data_Members

        private TimeSpan[] _values = null;

        private byte _MonitoringTime_FLAG;
        private bool _PowerupDelayMonitorVolt_FLAG0;
        private bool _PowerupDelayMonitorCurrent_FLAG1;
        private bool _PowerupDelayMonitorLoad_FLAG2;
        private bool _DisablePulseOnPowerUp_FLAG3;
        private bool _DisableEnergyOnPowerUp_FLAG4;

        #endregion

        #region Properties

        [XmlIgnore()]
        internal TimeSpan[] Values
        {
            get { return _values; }
            set { _values = value; }
        }

        [XmlIgnore()]
        public TimeSpan OverVolt
        {
            get { return GetMonitorTime(MonitoringTimeItem.OverVolt); }
            set { SetMonitorTime(MonitoringTimeItem.OverVolt, value); }
        }

        [XmlIgnore()]
        public TimeSpan UnderVolt
        {
            get { return GetMonitorTime(MonitoringTimeItem.UnderVolt); }
            set { SetMonitorTime(MonitoringTimeItem.UnderVolt, value); }
        }

        [XmlIgnore()]
        public TimeSpan ImbalanceVolt
        {
            get { return GetMonitorTime(MonitoringTimeItem.ImbalanceVolt); }
            set { SetMonitorTime(MonitoringTimeItem.ImbalanceVolt, value); }
        }

        [XmlIgnore()]
        public TimeSpan HighNeutralCurrent
        {
            get { return GetMonitorTime(MonitoringTimeItem.HighNeutralCurrent); }
            set { SetMonitorTime(MonitoringTimeItem.HighNeutralCurrent, value); }
        }

        [XmlIgnore()]
        public TimeSpan ReverseEnergy
        {
            get { return GetMonitorTime(MonitoringTimeItem.ReverseEnergy); }
            set { SetMonitorTime(MonitoringTimeItem.ReverseEnergy, value); }
        }

        [XmlIgnore()]
        public TimeSpan TamperEnergy
        {
            get { return GetMonitorTime(MonitoringTimeItem.TamperEnergy); }
            set { SetMonitorTime(MonitoringTimeItem.TamperEnergy, value); }
        }

        [XmlIgnore()]
        public TimeSpan ReversePolarity
        {
            get { return GetMonitorTime(MonitoringTimeItem.ReversePolarity); }
            set { SetMonitorTime(MonitoringTimeItem.ReversePolarity, value); }
        }

        [XmlIgnore()]
        public TimeSpan CTFail
        {
            get { return GetMonitorTime(MonitoringTimeItem.CTFail); }
            set { SetMonitorTime(MonitoringTimeItem.CTFail, value); }
        }

        [XmlIgnore()]
        public TimeSpan PTFail
        {
            get { return GetMonitorTime(MonitoringTimeItem.PTFail); }
            set { SetMonitorTime(MonitoringTimeItem.PTFail, value); }
        }

        [XmlIgnore()]
        public TimeSpan OverCurrent
        {
            get { return GetMonitorTime(MonitoringTimeItem.OverCurrent); }
            set { SetMonitorTime(MonitoringTimeItem.OverCurrent, value); }
        }

        [XmlIgnore()]
        public TimeSpan OverLoad
        {
            get { return GetMonitorTime(MonitoringTimeItem.OverLoad); }
            set { SetMonitorTime(MonitoringTimeItem.OverLoad, value); }
        }

        [XmlIgnore()]
        public TimeSpan PowerFail
        {
            get { return GetMonitorTime(MonitoringTimeItem.PowerFail); }
            set { SetMonitorTime(MonitoringTimeItem.PowerFail, value); }
        }

        [XmlIgnore()]
        public TimeSpan PhaseFail
        {
            get { return GetMonitorTime(MonitoringTimeItem.PhaseFail); }
            set { SetMonitorTime(MonitoringTimeItem.PhaseFail, value); }
        }

        [XmlIgnore()]
        public TimeSpan PhaseSequence
        {
            get { return GetMonitorTime(MonitoringTimeItem.PhaseSequence); }
            set { SetMonitorTime(MonitoringTimeItem.PhaseSequence, value); }
        }

        [XmlIgnore()]
        public TimeSpan PowerUPDelay
        {
            get { return GetMonitorTime(MonitoringTimeItem.PowerUPDelay); }
            set { SetMonitorTime(MonitoringTimeItem.PowerUPDelay, value); }
        }

        [XmlIgnore()]
        public TimeSpan PowerUpDelayEnergyRecording
        {
            get { return GetMonitorTime(MonitoringTimeItem.PowerUpDelayEnergyRecording); }
            set { SetMonitorTime(MonitoringTimeItem.PowerUpDelayEnergyRecording, value); }
        }

        [XmlIgnore()]
        public TimeSpan PowerUpDelayEarth
        {
            get { return GetMonitorTime(MonitoringTimeItem.PowerUpDelayEarth); }
            set { SetMonitorTime(MonitoringTimeItem.PowerUpDelayEarth, value); }
        }

        [XmlIgnore()]
        public TimeSpan Earth
        {
            get { return GetMonitorTime(MonitoringTimeItem.Earth); }
            set { SetMonitorTime(MonitoringTimeItem.Earth, value); }
        }

        [XmlIgnore()]
        public TimeSpan HALLSensor
        {
            get { return GetMonitorTime(MonitoringTimeItem.HALLSensor); }
            set { SetMonitorTime(MonitoringTimeItem.HALLSensor, value); }
        }

        [XmlIgnore()]
        public byte MonitoringTime_FLAG
        {
            get { return _MonitoringTime_FLAG; }
            set { _MonitoringTime_FLAG = value; }
        }

        [XmlIgnore()]
        public bool IsPowerupDelayMonitorVolt_FLAG0
        {
            get { return _PowerupDelayMonitorVolt_FLAG0; }
            set { _PowerupDelayMonitorVolt_FLAG0 = value; }
        }

        [XmlIgnore()]
        public bool IsPowerupDelayMonitorCurrent_FLAG1
        {
            get { return _PowerupDelayMonitorCurrent_FLAG1; }
            set { _PowerupDelayMonitorCurrent_FLAG1 = value; }
        }

        [XmlIgnore()]
        public bool IsPowerupDelayMonitorLoad_FLAG2
        {
            get { return _PowerupDelayMonitorLoad_FLAG2; }
            set { _PowerupDelayMonitorLoad_FLAG2 = value; }
        }

        [XmlIgnore()]
        public bool IsDisablePulseOnPowerUp_FLAG3
        {
            get { return _DisablePulseOnPowerUp_FLAG3; }
            set { _DisablePulseOnPowerUp_FLAG3 = value; }
        }

        [XmlIgnore()]
        public bool IsDisableEnergyOnPowerUp_FLAG4
        {
            get { return _DisableEnergyOnPowerUp_FLAG4; }
            set { _DisableEnergyOnPowerUp_FLAG4 = value; }
        }

        #endregion

        #region Getter/Setter Functions

        internal TimeSpan GetMonitorTime(MonitoringTimeItem Item)
        {
            TimeSpan value = MonitoringTimeValues.Default_MinValue;
            try
            {
                value = _values[(int)Item];
                if (!(value >= MonitoringTimeValues.Default_MinValue && value <= MonitoringTimeValues.Default_MaxValue))
                    throw new ArgumentNullException(Item.ToString(),
                            String.Format("{0} MonitoringTime Value not init properly", Item));
                return value;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error to get {0} MonitoringTime,{1}", Item, ex.Message), ex);
            }
        }

        internal void SetMonitorTime(MonitoringTimeItem Item, TimeSpan value)
        {
            try
            {
                _values[(int)Item] = value;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error to set {0} MonitoringTime,{1}", Item, ex.Message), ex);
            }
        }

        #endregion

        public Param_Monitoring_time()
        {
            int total_Count = Enum.GetValues(typeof(MonitoringTimeItem)).Length;
            ///Init_values
            _values = new TimeSpan[total_Count];
            for (int index = 0; index < total_Count; index++)
            {
                _values[index] = MonitoringTimeValues.Default_MinValue;
            }
        }

        public OBIS_data_from_GUI[] encode_ALL()
        {
            OBIS_data_from_GUI[] structToReturn = new OBIS_data_from_GUI[100];
            int i = 0;
            //structToReturn[i++] = encode_Over_Volt_attrib2();
            //structToReturn[i++] = encode_Over_Volt_attrib3();
            //structToReturn[i++] = encode_Under_Volt_attrib2();
            //structToReturn[i++] = encode_Under_Volt_attrib3();
            Array.Resize(ref structToReturn, i);
            return structToReturn;
        }

        public DateTime UpdateMT_object(double seconds)
        {
            DateTime temp = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            temp = temp.AddSeconds(seconds);
            return temp;
        }

        public DateTime UpdateMT_object(int seconds)
        {
            DateTime temp = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            temp = temp.AddSeconds(seconds);
            return temp;
        }

        public double SET_MT(DateTime arg)
        {
            double temp = 0;
            // temp = arg.Hour * 60 * 60 + arg.Minute * 60 + arg.Second; //AHMED 19092013
            temp = arg.Minute * 60 + arg.Second;
            return temp;
        }

        /// <summary>
        ///UpdateMT_object
        /// </summary>
        /// <returns></returns>
        public static TimeSpan UpdateMTObject(double seconds)
        {
            TimeSpan temp = MonitoringTimeValues.Default_MinValue +
                            TimeSpan.FromSeconds(seconds);
            return temp;
        }

        public static TimeSpan UpdateMObject(int seconds)
        {
            TimeSpan temp = MonitoringTimeValues.Default_MinValue + TimeSpan.FromSeconds(seconds);
            return temp;
        }

        /// <summary>
        ///SET_MT
        /// </summary>
        /// <returns></returns>
        public static double SET_MT(TimeSpan arg)
        {
            double temp = 0;
            // temp = arg.Hour * 60 * 60 + arg.Minute * 60 + arg.Second; //AHMED 19092013
            temp = arg.TotalSeconds;
            return temp;
        }

        #region Encoders/Decoders

        public static Base_Class EncodeMonitoringTimeParam(GetSAPEntry commObjectGetter, StOBISCode OBISCode, TimeSpan monitoringTimeParam)
        {
            Class_3 paramMTCommObj = (Class_3)commObjectGetter.Invoke(OBISCode);
            paramMTCommObj.EncodingAttribute = 2;
            paramMTCommObj.EncodingType = DataTypes._A06_double_long_unsigned;
            Unit_Scaler uni_Scaler = new Unit_Scaler() { Unit = units._B07_second, Scaler = 0 };
            paramMTCommObj.Unit = units._B07_second;
            paramMTCommObj.scaler = 0;
            paramMTCommObj.Value = SET_MT(monitoringTimeParam);
            return paramMTCommObj;
        }

        //public OBIS_data_from_GUI encode_Power_Fail_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.MonitoringTime_Power_Fail;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A06_double_long_unsigned;
        //    int time_in_secs = 0;
        //    time_in_secs = PowerFail.Hour * 60 * 60 + PowerFail.Minute * 60 + PowerFail.Second;
        //    obj_OBIS_data_from_GUI.Data = time_in_secs;

        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Power_Fail_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.MonitoringTime_Power_Fail;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B07_second);
        //    return obj_OBIS_data_from_GUI;
        //}

        //public OBIS_data_from_GUI encode_Phase_Fail_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.MonitoringTime_Phase_Fail;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A06_double_long_unsigned;
        //    int time_in_secs = 0;
        //    time_in_secs = PhaseFail.Hour * 60 * 60 + PhaseFail.Minute * 60 + PhaseFail.Second;
        //    obj_OBIS_data_from_GUI.Data = time_in_secs;

        //    return obj_OBIS_data_from_GUI;
        //}

        //public OBIS_data_from_GUI encode_Phase_Fail_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.MonitoringTime_Phase_Fail;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B07_second);
        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Over_Volt_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.MonitoringTime_Over_Volt_;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A06_double_long_unsigned;
        //    int time_in_secs = 0;
        //    time_in_secs = OverVolt.Hour * 60 * 60 + OverVolt.Minute * 60 + OverVolt.Second;
        //    obj_OBIS_data_from_GUI.Data = time_in_secs;

        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Over_Volt_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.MonitoringTime_Over_Volt_;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B07_second);
        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Under_Volt_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.MonitoringTime_Under_Volt;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A06_double_long_unsigned;
        //    int time_in_secs = 0;
        //    time_in_secs = UnderVolt.Hour * 60 * 60 + UnderVolt.Minute * 60 + UnderVolt.Second;
        //    obj_OBIS_data_from_GUI.Data = time_in_secs;

        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Under_Volt_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.MonitoringTime_Under_Volt;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B07_second);
        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Imbalance_Volt_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.MonitoringTime_Imbalance_Volt;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A06_double_long_unsigned;
        //    int time_in_secs = 0;
        //    time_in_secs = ImbalanceVolt.Hour * 60 * 60 + ImbalanceVolt.Minute * 60 + ImbalanceVolt.Second;
        //    obj_OBIS_data_from_GUI.Data = time_in_secs;

        //    return obj_OBIS_data_from_GUI;
        //}

        //public OBIS_data_from_GUI encode_Imbalance_Volt_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.MonitoringTime_Imbalance_Volt;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B07_second);
        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Over_Current_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.MonitoringTime_Over_Current;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A06_double_long_unsigned;
        //    int time_in_secs = 0;
        //    time_in_secs = OverCurrent.Hour * 60 * 60 + OverCurrent.Minute * 60 + OverCurrent.Second;
        //    obj_OBIS_data_from_GUI.Data = time_in_secs;

        //    return obj_OBIS_data_from_GUI;
        //}

        //public OBIS_data_from_GUI encode_Over_Current_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.MonitoringTime_Over_Current;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B07_second);
        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Over_Load_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.MonitoringTime_Over_Load;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A06_double_long_unsigned;
        //    int time_in_secs = 0;
        //    time_in_secs = OverLoad.Hour * 60 * 60 + OverLoad.Minute * 60 + OverLoad.Second;
        //    obj_OBIS_data_from_GUI.Data = time_in_secs;

        //    return obj_OBIS_data_from_GUI;
        //}

        //public OBIS_data_from_GUI encode_Over_Load_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.MonitoringTime_Over_Load;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B07_second);
        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_High_Neutral_Current_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.MonitoringTime_High_Neutral_Current;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A06_double_long_unsigned;
        //    int time_in_secs = 0;
        //    time_in_secs = HighNeutralCurrent.Hour * 60 * 60 + HighNeutralCurrent.Minute * 60 + HighNeutralCurrent.Second;
        //    obj_OBIS_data_from_GUI.Data = time_in_secs;

        //    return obj_OBIS_data_from_GUI;
        //}

        //public OBIS_data_from_GUI encode_High_Neutral_Current_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.MonitoringTime_High_Neutral_Current;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B07_second);
        //    return obj_OBIS_data_from_GUI;
        //}

        public Base_Class Encode_Power_Fail_attrib2(GetSAPEntry CommObjectGetter)
        {
            Class_3 Power_Fail_MT = (Class_3)EncodeMonitoringTimeParam(CommObjectGetter, Get_Index.MonitoringTime_Power_Fail, PowerFail);
            return Power_Fail_MT;
        }

        public void Decode_Power_Fail(Base_Class arg)
        {
            try
            {
                Class_3 Power_Fail_MT = (Class_3)arg;
                PowerFail = UpdateMTObject(Convert.ToDouble(Power_Fail_MT.Value));
            }
            catch
            {
                throw;
            }
        }

        public Base_Class Encode_Phase_Fail_attrib2(GetSAPEntry CommObjectGetter)
        {
            Class_3 Phase_Fail_MT = (Class_3)EncodeMonitoringTimeParam(CommObjectGetter, Get_Index.MonitoringTime_Phase_Fail, PhaseFail);
            return Phase_Fail_MT;
        }

        public void Decode_Phase_Fail(Base_Class arg)
        {
            try
            {
                Class_3 Phase_Fail_MT = (Class_3)arg;
                PhaseFail = UpdateMTObject(Convert.ToDouble(Phase_Fail_MT.Value));
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public Base_Class Encode_Over_Volt_attrib2(GetSAPEntry CommObjectGetter)
        {
            Class_3 Over_volt_MT = (Class_3)EncodeMonitoringTimeParam(CommObjectGetter, Get_Index.MonitoringTime_Over_Volt_, OverVolt);
            return Over_volt_MT;

            //Class_3 Over_volt_MT = (Class_3)CommObjectGetter.Invoke(Get_Index.MonitoringTime_Over_Volt_);
            //Over_volt_MT.EncodingAttribute = 2;
            //Over_volt_MT.EncodingType = DataTypes._A06_double_long_unsigned;
            //Over_volt_MT.Value = SET_MT(OverVolt);
            //Unit_Scaler uni_Scaler = new Unit_Scaler() { Unit = units._B07_second, Scaler = 0 };
            //Over_volt_MT.Unit = units._B07_second;
            //Over_volt_MT.scaler = 0;
            //return Over_volt_MT;
        }

        public void Decode_Over_Volt(Base_Class arg)
        {
            try
            {
                Class_3 Over_volt_MT = (Class_3)arg;
                OverVolt = UpdateMTObject(Convert.ToDouble(Over_volt_MT.Value));
            }
            catch
            {
                throw;
            }
        }

        public Base_Class Encode_Under_Volt_attrib2(GetSAPEntry CommObjectGetter)
        {
            Class_3 Under_Volt_MT = (Class_3)EncodeMonitoringTimeParam(CommObjectGetter, Get_Index.MonitoringTime_Under_Volt, UnderVolt);
            return Under_Volt_MT;
        }

        public void Decode_Under_Volt(Base_Class arg)
        {
            try
            {
                Class_3 Under_Volt_MT = (Class_3)arg;
                UnderVolt = UpdateMTObject(Convert.ToDouble(Under_Volt_MT.Value));
            }
            catch
            {
                throw;
            }
        }


        public Base_Class Encode_Imbalance_Volt(GetSAPEntry CommObjectGetter)
        {
            Class_3 Imbalance_Volt_MT = (Class_3)EncodeMonitoringTimeParam(CommObjectGetter, Get_Index.MonitoringTime_Imbalance_Volt, ImbalanceVolt);
            return Imbalance_Volt_MT;
        }

        public void Decode_Imbalance_Volt(Base_Class arg)
        {
            try
            {
                Class_3 Imbalance_Volt_MT = (Class_3)arg;
                ImbalanceVolt = UpdateMTObject(Convert.ToDouble(Imbalance_Volt_MT.Value));
            }
            catch
            {
                throw;
            }
        }

        public Base_Class Encode_Over_Current(GetSAPEntry CommObjectGetter)
        {
            Class_3 Over_Current_MT = (Class_3)EncodeMonitoringTimeParam(CommObjectGetter, Get_Index.MonitoringTime_Over_Current, OverCurrent);
            return Over_Current_MT;

        }

        public void Decode_Over_Current(Base_Class arg)
        {
            try
            {
                Class_3 Over_Current_MT = (Class_3)arg;
                OverCurrent = UpdateMTObject(Convert.ToDouble(Over_Current_MT.Value));
            }
            catch
            {
                throw;
            }
        }

        public Base_Class Encode_Over_Load(GetSAPEntry CommObjectGetter)
        {
            Class_3 Power_Fail_MT = (Class_3)EncodeMonitoringTimeParam(CommObjectGetter, Get_Index.MonitoringTime_Over_Load, OverLoad);
            return Power_Fail_MT;
        }

        public void Decode_Over_Load(Base_Class arg)
        {
            try
            {
                Class_3 Over_Load_MT = (Class_3)arg;
                OverLoad = UpdateMTObject(Convert.ToDouble(Over_Load_MT.Value));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Base_Class Encode_High_Neutral_Current(GetSAPEntry CommObjectGetter)
        {
            Class_3 High_Neutral_Current_MT = (Class_3)EncodeMonitoringTimeParam(CommObjectGetter, Get_Index.MonitoringTime_High_Neutral_Current, HighNeutralCurrent);
            return High_Neutral_Current_MT;

        }

        public void Decode_High_Neutral_Current(Base_Class arg)
        {
            try
            {
                Class_3 High_Neutral_Current_MT = (Class_3)arg;
                HighNeutralCurrent = UpdateMTObject(Convert.ToDouble(High_Neutral_Current_MT.Value));
            }
            catch
            {
                throw;
            }
        }

        //public OBIS_data_from_GUI encode_Reverse_Energy_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.MonitoringTime_Reverse_Energy;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A06_double_long_unsigned;
        //    int time_in_secs = 0;
        //    time_in_secs = ReverseEnergy.Hour * 60 * 60 + ReverseEnergy.Minute * 60 + ReverseEnergy.Second;
        //    obj_OBIS_data_from_GUI.Data = time_in_secs;
        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Reverse_Energy_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.MonitoringTime_Reverse_Energy;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B07_second);
        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Reverse_Polarity_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.MonitoringTime_Reverse_Polarity;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A06_double_long_unsigned;
        //    int time_in_secs = 0;
        //    time_in_secs = ReversePolarity.Hour * 60 * 60 + ReversePolarity.Minute * 60 + ReversePolarity.Second;
        //    obj_OBIS_data_from_GUI.Data = time_in_secs;

        //    return obj_OBIS_data_from_GUI;
        //}

        //public OBIS_data_from_GUI encode_Reverse_Polarity_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.MonitoringTime_Reverse_Polarity;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B07_second);
        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Phase_Sequence_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.MonitoringTime_Phase_Sequence;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A06_double_long_unsigned;
        //    int time_in_secs = 0;
        //    time_in_secs = PhaseSequence.Hour * 60 * 60 + PhaseSequence.Minute * 60 + PhaseSequence.Second;
        //    obj_OBIS_data_from_GUI.Data = time_in_secs;

        //    return obj_OBIS_data_from_GUI;
        //}

        //public OBIS_data_from_GUI encode_Phase_Sequence_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.MonitoringTime_Phase_Sequence;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B07_second);
        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Tamper_Energy_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.MonitoringTime_Tamper_Energy;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A06_double_long_unsigned;
        //    int time_in_secs = 0;
        //    time_in_secs = TamperEnergy.Hour * 60 * 60 + TamperEnergy.Minute * 60 + TamperEnergy.Second;
        //    obj_OBIS_data_from_GUI.Data = time_in_secs;

        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Tamper_Energy_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.MonitoringTime_Tamper_Energy;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B07_second);
        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_CT_Fail_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.MonitoringTime_CT_Fail;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A06_double_long_unsigned;
        //    int time_in_secs = 0;
        //    time_in_secs = CTFail.Hour * 60 * 60 + CTFail.Minute * 60 + CTFail.Second;
        //    obj_OBIS_data_from_GUI.Data = time_in_secs;

        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_CT_Fail_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.MonitoringTime_CT_Fail;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B07_second);
        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_PT_Fail_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.MonitoringTime_PT_Fail;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A06_double_long_unsigned;
        //    int time_in_secs = 0;
        //    time_in_secs = PTFail.Hour * 60 * 60 + PTFail.Minute * 60 + PTFail.Second;
        //    obj_OBIS_data_from_GUI.Data = time_in_secs;

        //    return obj_OBIS_data_from_GUI;
        //}

        //public OBIS_data_from_GUI encode_PT_Fail_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.MonitoringTime_PT_Fail;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B07_second);
        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Power_Up_Delay_To_Monitor_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.MonitoringTime_Power_Up_Delay_To_Monitor;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A06_double_long_unsigned;
        //    int time_in_secs = 0;
        //    time_in_secs = PowerUpDelay.Hour * 60 * 60 + PowerUpDelay.Minute * 60 + PowerUpDelay.Second;
        //    obj_OBIS_data_from_GUI.Data = time_in_secs;

        //    return obj_OBIS_data_from_GUI;
        //}

        //public OBIS_data_from_GUI encode_Power_Up_Delay_To_Monitor_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.MonitoringTime_Power_Up_Delay_To_Monitor;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B07_second);
        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Power_Up_Delay_For_Energy_Recording_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.MonitoringTime_Power_Up_Delay_For_Energy_Recording;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A06_double_long_unsigned;
        //    int time_in_secs = 0;
        //    time_in_secs = PowerUpDelayEnergyRecording.Hour * 60 * 60 + PowerUpDelayEnergyRecording.Minute * 60 + PowerUpDelayEnergyRecording.Second;
        //    obj_OBIS_data_from_GUI.Data = time_in_secs;

        //    return obj_OBIS_data_from_GUI;
        //}

        //public OBIS_data_from_GUI encode_Power_Up_Delay_For_Energy_Recording_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.MonitoringTime_Power_Up_Delay_For_Energy_Recording;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B07_second);
        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_FLAG_MONITORING_TIME()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.MonitoringTime_Flag;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A09_octet_string;
        //    byte flag_byte = (byte)(Convert.ToInt32(IsDisableEnergyOnPowerUp_FLAG4) * 16 +
        //        Convert.ToInt32(IsDisablePulseOnPowerUp_FLAG3) * 8 + Convert.ToInt32(IsPowerupDelayMonitorLoad_FLAG2) * 4 +
        //        Convert.ToInt32(IsPowerupDelayMonitorCurrent_FLAG1) * 2 + Convert.ToInt32(IsPowerupDelayMonitorVolt_FLAG0));
        //    obj_OBIS_data_from_GUI.Data = flag_byte;
        //    return obj_OBIS_data_from_GUI;
        //}

        public Base_Class Encode_Reverse_Energy(GetSAPEntry CommObjectGetter)
        {
            Class_3 Reverse_Energy_MT = (Class_3)EncodeMonitoringTimeParam(CommObjectGetter, Get_Index.MonitoringTime_Reverse_Energy, ReverseEnergy);
            return Reverse_Energy_MT;
        }

        public Base_Class Encode_earth(GetSAPEntry CommObjectGetter)
        {
            Class_3 earth_MT = (Class_3)EncodeMonitoringTimeParam(CommObjectGetter, Get_Index.MonitoringTime_Earth, PowerUpDelayEarth);
            return earth_MT;
        }

        public void Decode_Reverse_Energy(Base_Class arg)
        {
            try
            {
                Class_3 Reverse_Energy_MT = (Class_3)arg;
                ReverseEnergy = UpdateMTObject(Convert.ToDouble(Reverse_Energy_MT.Value));
            }
            catch
            {
                throw;
            }
        }

        public void Decode_earth(Base_Class arg)
        {
            try
            {
                Class_3 earth_MT = (Class_3)arg;
                PowerUpDelayEarth = UpdateMTObject(Convert.ToDouble(earth_MT.Value));
            }
            catch
            {
                throw;
            }
        }

        public Base_Class Encode_Reverse_Polarity(GetSAPEntry CommObjectGetter)
        {
            Class_3 Reverse_Polarity_MT = (Class_3)EncodeMonitoringTimeParam(CommObjectGetter, Get_Index.MonitoringTime_Reverse_Polarity, ReversePolarity);
            return Reverse_Polarity_MT;
        }

        public void Decode_Reverse_Polarity(Base_Class arg)
        {
            try
            {
                Class_3 Reverse_Polarity_MT = (Class_3)arg;
                ReversePolarity = UpdateMTObject(Convert.ToDouble(Reverse_Polarity_MT.Value));
            }
            catch
            {
                throw;
            }
        }

        public Base_Class Encode_Phase_Sequence(GetSAPEntry CommObjectGetter)
        {
            Class_3 Phase_Sequence_MT = (Class_3)EncodeMonitoringTimeParam(CommObjectGetter, Get_Index.MonitoringTime_Phase_Sequence, PhaseSequence);
            return Phase_Sequence_MT;
        }

        public void Decode_Phase_Sequence(Base_Class arg)
        {
            try
            {
                Class_3 Phase_Sequence_MT = (Class_3)arg;
                PhaseSequence = UpdateMTObject(Convert.ToDouble(Phase_Sequence_MT.Value));
            }
            catch
            {
                throw;
            }
        }

        public Base_Class Encode_Tamper_Energy(GetSAPEntry CommObjectGetter)
        {
            Class_3 Tamper_Energy_MT = (Class_3)EncodeMonitoringTimeParam(CommObjectGetter, Get_Index.MonitoringTime_Tamper_Energy, TamperEnergy);
            return Tamper_Energy_MT;
        }

        public void Decode_Tamper_Energy(Base_Class arg)
        {
            try
            {
                Class_3 Tamper_Energy_MT = (Class_3)arg;
                TamperEnergy = UpdateMTObject(Convert.ToDouble(Tamper_Energy_MT.Value));
            }
            catch
            {
                throw;
            }
        }

        public Base_Class Encode_CT_Fail(GetSAPEntry CommObjectGetter)
        {
            Class_3 MonitoringTime_CT_Fail_MT = (Class_3)EncodeMonitoringTimeParam(CommObjectGetter, Get_Index.MonitoringTime_CT_Fail, CTFail);
            return MonitoringTime_CT_Fail_MT;
        }

        public void Decode_CT_Fail(Base_Class arg)
        {
            try
            {
                Class_3 MonitoringTime_CT_Fail_MT = (Class_3)arg;
                CTFail = UpdateMTObject(Convert.ToDouble(MonitoringTime_CT_Fail_MT.Value));
            }
            catch
            {
                throw;
            }
        }

        public Base_Class Encode_PT_Fail(GetSAPEntry CommObjectGetter)
        {
            Class_3 MonitoringTime_PT_Fail_MT = (Class_3)EncodeMonitoringTimeParam(CommObjectGetter, Get_Index.MonitoringTime_PT_Fail, PTFail);
            return MonitoringTime_PT_Fail_MT;
        }

        public void Decode_PT_Fail(Base_Class arg)
        {
            try
            {
                Class_3 MonitoringTime_PT_Fail_MT = (Class_3)arg;
                PTFail = UpdateMTObject(Convert.ToDouble(MonitoringTime_PT_Fail_MT.Value));
            }
            catch
            {
                throw;
            }
        }

        public Base_Class Encode_Power_Up_Delay_To_Monitor(GetSAPEntry CommObjectGetter)
        {
            Class_3 MonitoringTime_Power_Up_Delay_To_Monitor_MT = (Class_3)EncodeMonitoringTimeParam(CommObjectGetter,
                Get_Index.MonitoringTime_Power_Up_Delay_To_Monitor, PowerUPDelay);
            return MonitoringTime_Power_Up_Delay_To_Monitor_MT;
        }

        public void Decode_Power_Up_Delay_To_Monitor(Base_Class arg)
        {
            try
            {
                Class_3 MonitoringTime_Power_Up_Delay_To_Monitor_MT = (Class_3)arg;
                PowerUPDelay = UpdateMTObject(Convert.ToDouble(MonitoringTime_Power_Up_Delay_To_Monitor_MT.Value));
            }
            catch
            {
                throw;
            }
        }

        public Base_Class Encode_Power_Up_Delay_For_Energy_Recording(GetSAPEntry CommObjectGetter)
        {
            Class_3 PowerUpDelay_EnergyRecording_MT =
                (Class_3)EncodeMonitoringTimeParam(CommObjectGetter, Get_Index.MonitoringTime_Power_Up_Delay_For_Energy_Recording, PowerUpDelayEnergyRecording);
            return PowerUpDelay_EnergyRecording_MT;
        }

        public void Decode_Power_Up_Delay_For_Energy_Recording(Base_Class arg)
        {
            try
            {
                Class_3 PowerUpDelay_EnergyRecording_MT = (Class_3)arg;
                PowerUpDelayEnergyRecording = UpdateMTObject(Convert.ToDouble(PowerUpDelay_EnergyRecording_MT.Value));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Base_Class Encode_HALL_Sensor(GetSAPEntry CommObjectGetter)
        {
            Class_3 MonitoringTime_HALL_Sensorl_MT =
                (Class_3)EncodeMonitoringTimeParam(CommObjectGetter, Get_Index.MonitoringTime_HALL_Sensor, HALLSensor);
            return MonitoringTime_HALL_Sensorl_MT;
        }

        public void Decode_HALL_Sensor(Base_Class arg)
        {
            try
            {
                Class_3 MonitoringTime_HALL_Sensor_MT = (Class_3)arg;
                HALLSensor = UpdateMTObject(Convert.ToDouble(MonitoringTime_HALL_Sensor_MT.Value));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Base_Class Encode_MONITORING_TIME_FLAG(GetSAPEntry CommObjectGetter)
        {
            byte flag_byte = (byte)(Convert.ToInt32(IsDisableEnergyOnPowerUp_FLAG4) * 16 +
                Convert.ToInt32(IsDisablePulseOnPowerUp_FLAG3) * 8 + Convert.ToInt32(IsPowerupDelayMonitorLoad_FLAG2) * 4 +
                Convert.ToInt32(IsPowerupDelayMonitorCurrent_FLAG1) * 2 + Convert.ToInt32(IsPowerupDelayMonitorVolt_FLAG0));

            Class_3 Flag_MT = (Class_3)CommObjectGetter.Invoke(Get_Index.MonitoringTime_Flag);
            Flag_MT.EncodingAttribute = 2;
            Flag_MT.EncodingType = DataTypes._A09_octet_string;
            Flag_MT.Value_Array = new byte[] { flag_byte };

            return Flag_MT;

        }

        public void Decode_MONITORING_TIME_FLAG(Base_Class arg)
        {
            try
            {
                Class_3 PowerUpDelay_EnergyRecording_MT = (Class_3)arg;
                byte[] dtArray = (byte[])PowerUpDelay_EnergyRecording_MT.Value_Array;
                if (dtArray != null && dtArray.Length > 0)
                {
                    byte flag_byte = dtArray[0];
                    ///Extract Flags from flag_byte
                    Update_MonitoringTimeFlags(flag_byte);
                }
            }
            catch
            {
                throw;
            }
        }

        private void Update_MonitoringTimeFlags(byte flag_byte)
        {
            IsPowerupDelayMonitorVolt_FLAG0 = Convert.ToBoolean(0x01 & flag_byte);
            IsPowerupDelayMonitorCurrent_FLAG1 = Convert.ToBoolean(0x02 & flag_byte);
            IsPowerupDelayMonitorLoad_FLAG2 = Convert.ToBoolean(0x04 & flag_byte);
            IsDisablePulseOnPowerUp_FLAG3 = Convert.ToBoolean(0x08 & flag_byte);
        }

        #endregion

        #region ISerializable Members

        protected Param_Monitoring_time(SerializationInfo info, StreamingContext context)
        {
            var val_Array = Enum.GetValues(typeof(MonitoringTimeItem));
            int total_Count_Enum = val_Array.Length;
            ///Init _values
            _values = new TimeSpan[total_Count_Enum];
            for (int index = 0; index < total_Count_Enum; index++)
            {
                _values[index] = MonitoringTimeValues.Default_MinValue;
            }
            ///Getting MonitoringTimes Count
            int total_Count = (int)info.GetInt32("ParamMonitoringTime_Count");

            String Key = String.Empty;
            String Val = String.Empty;
            DateTime date_Now = MonitoringTimeValues.Default_DateTime;
            

            foreach (MonitoringTimeItem item in val_Array)
            {
                Key = String.Format("{0}_MonitoringTime", item);
                date_Now = MonitoringTimeValues.Default_DateTime;
                ///Getting  CalendarStartDateTime Type StDateTime
                try
                {
                    Val = info.GetString(Key);
                    DateTime.TryParseExact(Val, "G", SmartEyeControl_7.Properties.Resources.Culture,
                           DateTimeStyles.None, out date_Now);

                    //date_Now = info.GetDateTime(Key);
                    //date_Now = TimeZoneInfo.ConvertTimeFromUtc(convertedDate, MonitoringTimeValues.PST_Zone);
                    
                    date_Now = DateTime.SpecifyKind(
                        date_Now, DateTimeKind.Local);

                    
                }
                catch { }
                _values[(int)item] = date_Now.TimeOfDay;
            }
            ///Getting MonitoringTime_Flag Type byte
            this.MonitoringTime_FLAG = (byte)info.GetValue("MonitoringTime_Flag", typeof(byte));
            ///Getting PowerupDelayMonitorVolt_FLAG0 Type bool
            this.IsPowerupDelayMonitorVolt_FLAG0 = info.GetBoolean("PowerupDelayMonitorVolt_FLAG0");
            ///Getting PowerupDelayMonitorCurrent_FLAG1 Type bool
            IsPowerupDelayMonitorCurrent_FLAG1 = info.GetBoolean("PowerupDelayMonitorCurrent_FLAG1");
            ///Getting PowerupDelayMonitorLoad_FLAG2 Type bool
            this.IsPowerupDelayMonitorLoad_FLAG2 = info.GetBoolean("PowerupDelayMonitorLoad_FLAG2");
            ///Getting DisablePulseOnPowerUp_FLAG3 Type bool
            this.IsDisablePulseOnPowerUp_FLAG3 = info.GetBoolean("DisablePulseOnPowerUp_FLAG3");
            ///Adding DisableEnergyOnPowerUp_FLAG4 Type bool
            this.IsDisableEnergyOnPowerUp_FLAG4 = info.GetBoolean("DisableEnergyOnPowerUp_FLAG4");
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            var val_Array = Enum.GetValues(typeof(MonitoringTimeItem));
            int total_Count_Enum = val_Array.Length;
            ///Adding MonitoringTimes Type int32
            info.AddValue("ParamMonitoringTime_Count", total_Count_Enum);
            String Key = String.Empty;
            DateTime date_Now = MonitoringTimeValues.Default_DateTime;
            if (_values == null)
            {
                _values = new TimeSpan[total_Count_Enum];
                for (int index = 0; index < total_Count_Enum; index++)
                {
                    _values[index] = MonitoringTimeValues.Default_MinValue;
                }
            }
            foreach (MonitoringTimeItem item in val_Array)
            {
                Key = String.Format("{0}_MonitoringTime", item);
                
                //Adding MonitoringTime Values Type DateTime
                TimeSpan MT_Val = _values[(int)item];
                var _dtVal = date_Now.Add(MT_Val);
                _dtVal = DateTime.SpecifyKind(_dtVal, DateTimeKind.Local);
                //_dtVal = TimeZoneInfo.ConvertTimeToUtc(_dtVal, MonitoringTimeValues.PST_Zone);
                info.AddValue(Key, _dtVal.ToString("G"));
            }
            ///Adding MonitoringTime_Flag Type byte
            info.AddValue("MonitoringTime_Flag", this.MonitoringTime_FLAG);
            ///Adding PowerupDelayMonitorVolt_FLAG0 Type bool
            info.AddValue("PowerupDelayMonitorVolt_FLAG0", this.IsPowerupDelayMonitorVolt_FLAG0);
            ///Adding PowerupDelayMonitorCurrent_FLAG1 Type bool
            info.AddValue("PowerupDelayMonitorCurrent_FLAG1", this.IsPowerupDelayMonitorCurrent_FLAG1);
            ///Adding PowerupDelayMonitorLoad_FLAG2 Type bool
            info.AddValue("PowerupDelayMonitorLoad_FLAG2", this.IsPowerupDelayMonitorLoad_FLAG2);
            ///Adding DisablePulseOnPowerUp_FLAG3 Type bool
            info.AddValue("DisablePulseOnPowerUp_FLAG3", this.IsDisablePulseOnPowerUp_FLAG3);
            ///Adding DisableEnergyOnPowerUp_FLAG4 Type bool
            info.AddValue("DisableEnergyOnPowerUp_FLAG4", this.IsDisableEnergyOnPowerUp_FLAG4);
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
                throw new Exception("Error occured while Clone Param_MonitoringTime", ex);
            }
        }
    }
}
