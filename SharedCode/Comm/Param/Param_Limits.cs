using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLMS;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SharedCode.Comm.DataContainer;
using DLMS.Comm;
using SharedCode.Comm.HelperClasses;

namespace SharedCode.Comm.Param
{
    [Serializable]
    [XmlInclude(typeof(Param_Limits))]
    public class Param_Limits : ICloneable, IParam
    {
        #region DataMembers

        private double[] _values = null;

        #endregion

        #region Properties

        public double Contactor_Fail_Pwr_Limit { get; set; }

        [XmlIgnore()]
        internal double[] Values
        {
            get { return _values; }
            set { _values = value; }
        }

        public bool WriteOverVolt { get; set; }
        public double OverVolt
        {
            get { return GetLimit(ThreshouldItem.OverVolt); }
            set { SetLimit(ThreshouldItem.OverVolt, value); }
        }

        public bool WriteUnderVolt { get; set; }
        public double UnderVolt
        {
            get { return GetLimit(ThreshouldItem.UnderVolt); }
            set { SetLimit(ThreshouldItem.UnderVolt, value); }
        }

        public bool WriteImbalanceVolt { get; set; }
        public double ImbalanceVolt
        {
            get { return GetLimit(ThreshouldItem.ImbalanceVolt); }
            set { SetLimit(ThreshouldItem.ImbalanceVolt, value); }
        }

        public bool WriteHighNeutralCurrent { get; set; }
        public double HighNeutralCurrent
        {
            get { return GetLimit(ThreshouldItem.HighNeutralCurrent); }
            set { SetLimit(ThreshouldItem.HighNeutralCurrent, value); }
        }

        public bool WriteOverCurrent_Phase { get; set; }
        public double OverCurrent_Phase
        {
            get { return GetLimit(ThreshouldItem.OverCurrent_Phase); }
            set { SetLimit(ThreshouldItem.OverCurrent_Phase, value); }
        }

        public bool WriteMeterOnLoad { get; set; }
        public double MeterOnLoad
        {
            get { return GetLimit(ThreshouldItem.Meter_ON_Load); }
            set { SetLimit(ThreshouldItem.Meter_ON_Load, value); }
        }
        public double OverPower
        {
            get { return GetLimit(ThreshouldItem.OverPower); }
            set { SetLimit(ThreshouldItem.OverPower, value); }
        }

        public bool WritePowerFactor_Change { get; set; }
        public double PowerFactor_Change
        {
            get { return GetLimit(ThreshouldItem.PowerFactor_Change); }
            set { SetLimit(ThreshouldItem.PowerFactor_Change, value); }
        }

        public bool WriteCrestFactorLow { get; set; }
        public double CrestFactorLow
        {
            get { return GetLimit(ThreshouldItem.CrestFactorLow); }
            set { SetLimit(ThreshouldItem.CrestFactorLow, value); }
        }

        //public bool WriteCrestFactorHigh { get; set; }
        public double CrestFactorHigh
        {
            get { return GetLimit(ThreshouldItem.CrestFactorHigh); }
            set { SetLimit(ThreshouldItem.CrestFactorHigh, value); }
        }

        public bool WriteReverseEnergy { get; set; }
        public double ReverseEnergy
        {
            get { return GetLimit(ThreshouldItem.ReverseEnergy); }
            set { SetLimit(ThreshouldItem.ReverseEnergy, value); }
        }

        public bool WriteTamperEnergy { get; set; }
        public double TamperEnergy
        {
            get { return GetLimit(ThreshouldItem.TamperEnergy); }
            set { SetLimit(ThreshouldItem.TamperEnergy, value); }
        }

        public bool WriteCTFail_AMP { get; set; }
        public double CTFail_AMP
        {
            get { return GetLimit(ThreshouldItem.CTFail); }
            set { SetLimit(ThreshouldItem.CTFail, value); }
        }

        public bool WritePTFail_AMP { get; set; }
        public double PTFail_AMP
        {
            get { return GetLimit(ThreshouldItem.PTFail_Amp); }
            set { SetLimit(ThreshouldItem.PTFail_Amp, value); }
        }

        public bool WritePTFail_Volt { get; set; }
        public double PTFail_Volt
        {
            get { return GetLimit(ThreshouldItem.PTFail_Volt); }
            set { SetLimit(ThreshouldItem.PTFail_Volt, value); }
        }

        #region OverCurrentByPhase

        public bool WriteOverCurrentByPhase_T1 { get; set; }
        public double OverCurrentByPhase_T1
        {
            get { return GetLimit(ThreshouldItem.OverCurrentByPhase_T1); }
            set { SetLimit(ThreshouldItem.OverCurrentByPhase_T1, value); }
        }

        public bool WriteOverCurrentByPhase_T2 { get; set; }
        public double OverCurrentByPhase_T2
        {
            get { return GetLimit(ThreshouldItem.OverCurrentByPhase_T2); }
            set { SetLimit(ThreshouldItem.OverCurrentByPhase_T2, value); }
        }

        public bool WriteOverCurrentByPhase_T3 { get; set; }
        public double OverCurrentByPhase_T3
        {
            get { return GetLimit(ThreshouldItem.OverCurrentByPhase_T3); }
            set { SetLimit(ThreshouldItem.OverCurrentByPhase_T3, value); }
        }

        public bool WriteOverCurrentByPhase_T4 { get; set; }
        public double OverCurrentByPhase_T4
        {
            get { return GetLimit(ThreshouldItem.OverCurrentByPhase_T4); }
            set { SetLimit(ThreshouldItem.OverCurrentByPhase_T4, value); }
        }

        #endregion

        #region OverLoadByPhase

        public bool WriteOverLoadByPhase_T1 { get; set; }
        public double OverLoadByPhase_T1
        {
            get { return GetLimit(ThreshouldItem.OverLoadByPhase_T1); }
            set { SetLimit(ThreshouldItem.OverLoadByPhase_T1, value); }
        }

        public bool WriteOverLoadByPhase_T2 { get; set; }
        public double OverLoadByPhase_T2
        {
            get { return GetLimit(ThreshouldItem.OverLoadByPhase_T2); }
            set { SetLimit(ThreshouldItem.OverLoadByPhase_T2, value); }
        }

        public bool WriteOverLoadByPhase_T3 { get; set; }
        public double OverLoadByPhase_T3
        {
            get { return GetLimit(ThreshouldItem.OverLoadByPhase_T3); }
            set { SetLimit(ThreshouldItem.OverLoadByPhase_T3, value); }
        }

        public bool WriteOverLoadByPhase_T4 { get; set; }
        public double OverLoadByPhase_T4
        {
            get { return GetLimit(ThreshouldItem.OverLoadByPhase_T4); }
            set { SetLimit(ThreshouldItem.OverLoadByPhase_T4, value); }
        }

        #endregion

        #region OverLoadTotal

        public bool WriteOverLoadTotal_T1 { get; set; }
        public double OverLoadTotal_T1
        {
            get { return GetLimit(ThreshouldItem.OverLoadTotal_T1); }
            set { SetLimit(ThreshouldItem.OverLoadTotal_T1, value); }
        }

        public bool WriteOverLoadTotal_T2 { get; set; }
        public double OverLoadTotal_T2
        {
            get { return GetLimit(ThreshouldItem.OverLoadTotal_T2); }
            set { SetLimit(ThreshouldItem.OverLoadTotal_T2, value); }
        }

        public bool WriteOverLoadTotal_T3 { get; set; }
        public double OverLoadTotal_T3
        {
            get { return GetLimit(ThreshouldItem.OverLoadTotal_T3); }
            set { SetLimit(ThreshouldItem.OverLoadTotal_T3, value); }
        }

        public bool WriteOverLoadTotal_T4 { get; set; }
        public double OverLoadTotal_T4
        {
            get { return GetLimit(ThreshouldItem.OverLoadTotal_T4); }
            set { SetLimit(ThreshouldItem.OverLoadTotal_T4, value); }
        }

        #endregion

        #region DemandOverLoadTotal

        public bool WriteDemandOverLoadTotal_T1 { get; set; }
        public double DemandOverLoadTotal_T1
        {
            get { return GetLimit(ThreshouldItem.MDIExceed_T1); }
            set { SetLimit(ThreshouldItem.MDIExceed_T1, value); }
        }

        public bool WriteDemandOverLoadTotal_T2 { get; set; }
        public double DemandOverLoadTotal_T2
        {
            get { return GetLimit(ThreshouldItem.MDIExceed_T2); }
            set { SetLimit(ThreshouldItem.MDIExceed_T2, value); }
        }

        public bool WriteDemandOverLoadTotal_T3 { get; set; }
        public double DemandOverLoadTotal_T3
        {
            get { return GetLimit(ThreshouldItem.MDIExceed_T3); }
            set { SetLimit(ThreshouldItem.MDIExceed_T3, value); }
        }

        public bool WriteDemandOverLoadTotal_T4 { get; set; }
        public double DemandOverLoadTotal_T4
        {
            get { return GetLimit(ThreshouldItem.MDIExceed_T4); }
            set { SetLimit(ThreshouldItem.MDIExceed_T4, value); }
        } 

        #endregion

        #endregion

        #region Getter/Setter Functions

        internal double GetLimit(ThreshouldItem Item)
        {
            double value = double.NaN;
            try
            {
                value = _values[(int)Item];
                if (!(value >= double.MinValue &&
                      value <= double.MaxValue))
                    throw new ArgumentNullException(Item.ToString(),
                        String.Format("{0}_Limit Value not init properly", Item));
                return value;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error to get {0} Limit,{1}", Item, ex.Message), ex);
            }
        }

        public void SetLimit(ThreshouldItem Item, double value)
        {
            try
            {
                _values[(int)Item] = value;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error to set {0} Limit,{1}", Item, ex.Message), ex);
            }
        }

        #endregion

        public Param_Limits()
        {
            int total_Count = Enum.GetValues(typeof(ThreshouldItem)).Length;
            ///Init _values
            _values = new double[total_Count];
            for (int index = 0; index < total_Count; index++)
            {
                _values[index] = 0.0;
            }
        }

        #region Encoders/Decoders

        public Base_Class Encode_Limits_Over_Voltage_Limit_attrib2(GetSAPEntry CommObjectGetter)
        {
            Class_3 Over_Voltage_Limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Over_Voltage_Limit);
            Over_Voltage_Limit.EncodingAttribute = 2;
            Over_Voltage_Limit.EncodingType = DataTypes._A12_long_unsigned;
            Over_Voltage_Limit.Value = (OverVolt * 100);
            return Over_Voltage_Limit;
        }

        public void Decode_Limits_Over_Voltage_Limit_attrib2(Base_Class arg)
        {
            try
            {
                Class_3 Over_Voltage_Limit = (Class_3)arg;
                ///Verify data Receiced/OBIS/ETC
                OverVolt = Convert.ToDouble(Over_Voltage_Limit.Value);
            }
            catch
            {
                throw;
            }
        }

        public Base_Class Encode_Limits_Under_Voltage_Limit_attrib2(GetSAPEntry CommObjectGetter)
        {
            Class_3 Under_Voltage_Limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Under_Voltage_Limit);
            Under_Voltage_Limit.EncodingAttribute = 2;
            Under_Voltage_Limit.EncodingType = DataTypes._A12_long_unsigned;
            Under_Voltage_Limit.Value = UnderVolt * 100;
            return Under_Voltage_Limit;
        }
        public void Decode_Limits_Under_Voltage_Limit_attrib2(Base_Class arg)
        {
            try
            {
                Class_3 Under_Voltage_Limit = (Class_3)arg;
                ///Verify data Receiced/OBIS/ETC
                UnderVolt = Convert.ToDouble(Under_Voltage_Limit.Value);
            }
            catch
            {
                throw;
            }
        }

        public Base_Class Encode_Limits_Imbalance_Volt_attrib2(GetSAPEntry CommObjectGetter)
        {
            Class_3 Under_Voltage_Limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Limits_Imbalance_Volt);
            Under_Voltage_Limit.EncodingAttribute = 2;
            Under_Voltage_Limit.EncodingType = DataTypes._A12_long_unsigned;
            Under_Voltage_Limit.Value = ImbalanceVolt * 100;
            return Under_Voltage_Limit;
        }
        public void Decode_Limits_Imbalance_Volt_attrib2(Base_Class arg)
        {
            try
            {
                Class_3 Imbalance_Volt_Limit = (Class_3)arg;
                ///Verify data Receiced/OBIS/ETC
                ImbalanceVolt = Convert.ToDouble(Imbalance_Volt_Limit.Value);
            }
            catch
            {
                throw;
            }
        }

        public Base_Class Encode_Limits_High_Neutral_Current_attrib2(GetSAPEntry CommObjectGetter)
        {
            Class_3 High_Neutral_Current_Limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Limits_High_Neutral_Current);
            High_Neutral_Current_Limit.EncodingAttribute = 2;
            High_Neutral_Current_Limit.EncodingType = DataTypes._A12_long_unsigned;
            High_Neutral_Current_Limit.Value = HighNeutralCurrent * 100;
            return High_Neutral_Current_Limit;
        }

        public void Decode_Limits_High_Neutral_Current_attrib2(Base_Class arg)
        {
            try
            {
                Class_3 High_Neutral_Current_Limit = (Class_3)arg;
                ///Verify data Receiced/OBIS/ETC
                HighNeutralCurrent = Convert.ToDouble(High_Neutral_Current_Limit.Value);
            }
            catch
            {
                throw;
            }
        }

        public Base_Class Encode_Limits_Reverse_Energy_attrib2(GetSAPEntry CommObjectGetter)
        {
            Class_3 Reverse_Energy_Limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Limits_Reverse_Energy_);
            Reverse_Energy_Limit.EncodingAttribute = 2;
            Reverse_Energy_Limit.EncodingType = DataTypes._A06_double_long_unsigned;
            Reverse_Energy_Limit.Value =  ReverseEnergy * 1000; ///Fixed 1000 multiplier 
            return Reverse_Energy_Limit;
        }
        public void Decode_Limits_Reverse_Energy_attrib2(Base_Class arg)
        {
            try
            {
                Class_3 Reverse_Energy_Limit = (Class_3)arg;
                ///Verify data Receiced/OBIS/ETC
                ReverseEnergy = Convert.ToDouble(Reverse_Energy_Limit.Value) / 1000;
            }
            catch
            {
                throw;
            }
        }

        public Base_Class Encode_Limits_Tamper_Energy_attrib2(GetSAPEntry CommObjectGetter)
        {
            Class_3 Tamper_Energy_Limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Limits_Tamper_Energy);
            Tamper_Energy_Limit.EncodingAttribute = 2;
            Tamper_Energy_Limit.EncodingType = DataTypes._A06_double_long_unsigned;
            Tamper_Energy_Limit.Value = TamperEnergy * 1000; ///Fixed 1000 multiplier 
            return Tamper_Energy_Limit;
        }
        public void Decode_Limits_Tamper_Energy_attrib2(Base_Class arg)
        {
            try
            {
                Class_3 Tamper_Energy_Limit = (Class_3)arg;
                ///Verify data Receiced/OBIS/ETC
                TamperEnergy = Convert.ToDouble(Tamper_Energy_Limit.Value) / 1000;///Fixed 1000 Divider 
            }
            catch
            {
                throw;
            }
        }

        public Base_Class Encode_Limits_CT_Fail__Amp_Limit_attrib2(GetSAPEntry CommObjectGetter)
        {
            Class_3 CT_Fail__Amp_Limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Limits_CT_Fail__Amp_Limit);
            CT_Fail__Amp_Limit.EncodingAttribute = 2;
            CT_Fail__Amp_Limit.EncodingType = DataTypes._A12_long_unsigned;
            CT_Fail__Amp_Limit.Value = this.CTFail_AMP * 100;
            return CT_Fail__Amp_Limit;
        }
        public void Decode_Limits_CT_Fail__Amp_Limit_attrib2(Base_Class arg)
        {
            try
            {
                Class_3 CT_Fail__Amp_Limit = (Class_3)arg;
                ///Verify data Receiced/OBIS/ETC
                CTFail_AMP = Convert.ToDouble(CT_Fail__Amp_Limit.Value);
            }
            catch
            {
                throw;
            }
        }

        public Base_Class Encode_Contactor_Failure_attrib2(GetSAPEntry CommObjectGetter)
        {
            Class_3 Obj_Limit = (Class_3)CommObjectGetter.Invoke(Get_Index.CONTACTOR_FAILURE_POWER_LIMIT);
            Obj_Limit.EncodingAttribute = 2;
            Obj_Limit.EncodingType = DataTypes._A06_double_long_unsigned;
            Obj_Limit.Value = Contactor_Fail_Pwr_Limit * 1000;
            return Obj_Limit;
        }

        public void Decode_Limits_ContactorFailure_attrib2(Base_Class arg)
        {
            try
            {
                Class_3 ContactorFailure_Limit = (Class_3)arg;
                // Verify data Receiced/OBIS/ETC
                Contactor_Fail_Pwr_Limit = Convert.ToDouble(ContactorFailure_Limit.Value) / 1000;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public Base_Class Encode_Limits_PT_Fail_Amp_Limit_attrib2(GetSAPEntry CommObjectGetter)
        {
            Class_3 PT_Fail_Amp_Limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Limits_PT_Fail_Amp_Limit);
            PT_Fail_Amp_Limit.EncodingAttribute = 2;
            PT_Fail_Amp_Limit.EncodingType = DataTypes._A12_long_unsigned;
            PT_Fail_Amp_Limit.Value = this.PTFail_AMP * 100;
            return PT_Fail_Amp_Limit;
        }
        public void Decode_Limits_PT_Fail_Amp_Limit_attrib2(Base_Class arg)
        {
            try
            {
                Class_3 PT_Fail_Amp_Limit_ = (Class_3)arg;
                ///Verify data Receiced/OBIS/ETC
                PTFail_AMP = Convert.ToDouble(PT_Fail_Amp_Limit_.Value);
            }
            catch
            {
                throw;
            }
        }

        public Base_Class Encode_Limits_PT_Fail_Volt_Limit_attrib2(GetSAPEntry CommObjectGetter)
        {
            Class_3 PT_Fail_Volt_Limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Limits_PT_Fail_Volt_Limit);
            PT_Fail_Volt_Limit.EncodingAttribute = 2;
            PT_Fail_Volt_Limit.EncodingType = DataTypes._A12_long_unsigned;
            PT_Fail_Volt_Limit.Value = this.PTFail_Volt * 100;
            return PT_Fail_Volt_Limit;
        }
        public void Decode_Limits_PT_Fail_Volt_Limit_attrib2(Base_Class arg)
        {
            try
            {
                Class_3 PT_Fail_Volt_Limit_ = (Class_3)arg;
                ///Verify data Receiced/OBIS/ETC
                PTFail_Volt = Convert.ToDouble(PT_Fail_Volt_Limit_.Value);
            }
            catch
            {
                throw;
            }
        }

        #region Over_Current_By_Phase

        public Base_Class Encode_Limits_Over_Current_By_Phase_T1_attrib2(GetSAPEntry CommObjectGetter)
        {
            Class_3 Over_Current_By_Phase_T1_Limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Limits_Over_Current_By_Phase_T1);
            Over_Current_By_Phase_T1_Limit.EncodingAttribute = 2;
            Over_Current_By_Phase_T1_Limit.EncodingType = DataTypes._A12_long_unsigned;
            Over_Current_By_Phase_T1_Limit.Value = this.OverCurrentByPhase_T1 * 100;
            return Over_Current_By_Phase_T1_Limit;
        }
        public void Decode_Limits_Over_Current_By_Phase_T1_attrib2(Base_Class arg)
        {
            try
            {
                Class_3 Over_Current_By_Phase_T1_Limit = (Class_3)arg;
                ///Verify data Receiced/OBIS/ETC
                OverCurrentByPhase_T1 = Convert.ToDouble(Over_Current_By_Phase_T1_Limit.Value);
            }
            catch
            {
                throw;
            }
        }


        public Base_Class Encode_Limits_Over_Current_By_Phase_T2_attrib2(GetSAPEntry CommObjectGetter)
        {
            Class_3 Over_Current_By_Phase_T2_Limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Limits_Over_Current_By_Phase_T2);
            Over_Current_By_Phase_T2_Limit.EncodingAttribute = 2;
            Over_Current_By_Phase_T2_Limit.EncodingType = DataTypes._A12_long_unsigned;
            Over_Current_By_Phase_T2_Limit.Value = this.OverCurrentByPhase_T2 * 100;
            return Over_Current_By_Phase_T2_Limit;
        }
        public void Decode_Limits_Over_Current_By_Phase_T2_attrib2(Base_Class arg)
        {
            try
            {
                Class_3 Over_Current_By_Phase_T2_Limit = (Class_3)arg;
                ///Verify data Receiced/OBIS/ETC
                OverCurrentByPhase_T2 = Convert.ToDouble(Over_Current_By_Phase_T2_Limit.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public Base_Class Encode_Limits_Over_Current_By_Phase_T3_attrib2(GetSAPEntry CommObjectGetter)
        {
            Class_3 Over_Current_By_Phase_T3_Limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Limits_Over_Current_By_Phase_T3);
            Over_Current_By_Phase_T3_Limit.EncodingAttribute = 2;
            Over_Current_By_Phase_T3_Limit.EncodingType = DataTypes._A12_long_unsigned;
            Over_Current_By_Phase_T3_Limit.Value = OverCurrentByPhase_T3 * 100;
            return Over_Current_By_Phase_T3_Limit;
        }
        public void Decode_Limits_Over_Current_By_Phase_T3_attrib2(Base_Class arg)
        {
            try
            {
                Class_3 Over_Current_By_Phase_T3_Limit = (Class_3)arg;
                ///Verify data Receiced/OBIS/ETC
                OverCurrentByPhase_T3 = Convert.ToDouble(Over_Current_By_Phase_T3_Limit.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Base_Class Encode_Limits_Over_Current_By_Phase_T4_attrib2(GetSAPEntry CommObjectGetter)
        {
            Class_3 Over_Current_By_Phase_T4_Limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Limits_Over_Current_By_Phase_T4);
            Over_Current_By_Phase_T4_Limit.EncodingAttribute = 2;
            Over_Current_By_Phase_T4_Limit.EncodingType = DataTypes._A12_long_unsigned;
            Over_Current_By_Phase_T4_Limit.Value = OverCurrentByPhase_T4 * 100;
            return Over_Current_By_Phase_T4_Limit;
        }
        public void Decode_Limits_Over_Current_By_Phase_T4_attrib2(Base_Class arg)
        {
            try
            {
                Class_3 Over_Current_By_Phase_T4_Limit = (Class_3)arg;
                ///Verify data Receiced/OBIS/ETC
                OverCurrentByPhase_T4 = Convert.ToDouble(Over_Current_By_Phase_T4_Limit.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        } 
        
        #endregion

        #region Over_Load_By_Phase1_Encoder/Decoder

        public Base_Class Encode_Limits_Over_Load_By_Phase1_T1_attrib2(GetSAPEntry CommObjectGetter)
        {
            Class_3 Over_Load_By_Phase1_T1_Limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Limits_Over_Load_By_Phase1_T1);
            Over_Load_By_Phase1_T1_Limit.EncodingAttribute = 2;
            Over_Load_By_Phase1_T1_Limit.EncodingType = DataTypes._A06_double_long_unsigned;
            Over_Load_By_Phase1_T1_Limit.Value = OverLoadByPhase_T1 * 1000; ///Fixed 1000 multiplier
            return Over_Load_By_Phase1_T1_Limit;
        }
        public void Decode_Limits_Over_Load_By_Phase1_T1_attrib2(Base_Class arg)
        {
            try
            {
                Class_3 Over_Load_By_Phase1_T1_Limit_ = (Class_3)arg;
                ///Verify data Receiced/OBIS/ETC
                OverLoadByPhase_T1 = Convert.ToDouble(Over_Load_By_Phase1_T1_Limit_.Value) / 1000;///Fixed 1000 Divider
            }
            catch
            {
                throw;
            }
        }

        public Base_Class Encode_Limits_Over_Load_By_Phase1_T2_attrib2(GetSAPEntry CommObjectGetter)
        {
            Class_3 Over_Load_By_Phase1_T2_Limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Limits_Over_Load_By_Phase1_T2);
            Over_Load_By_Phase1_T2_Limit.EncodingAttribute = 2;
            Over_Load_By_Phase1_T2_Limit.EncodingType = DataTypes._A06_double_long_unsigned;
            Over_Load_By_Phase1_T2_Limit.Value = OverLoadByPhase_T2 * 1000; ///Fixed 1000 multiplier
            return Over_Load_By_Phase1_T2_Limit;
        }
        public void Decode_Limits_Over_Load_By_Phase1_T2_attrib2(Base_Class arg)
        {
            try
            {
                Class_3 Over_Load_By_Phase1_T2_Limit_ = (Class_3)arg;
                ///Verify data Receiced/OBIS/ETC
                OverLoadByPhase_T2 = Convert.ToDouble(Over_Load_By_Phase1_T2_Limit_.Value) / 1000;///Fixed 1000 Divider
            }
            catch
            {
                throw;
            }
        }

        public Base_Class Encode_Limits_Over_Load_By_Phase1_T3_attrib2(GetSAPEntry CommObjectGetter)
        {
            Class_3 Over_Load_By_Phase1_T3_Limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Limits_Over_Load_By_Phase1_T3);
            Over_Load_By_Phase1_T3_Limit.EncodingAttribute = 2;
            Over_Load_By_Phase1_T3_Limit.EncodingType = DataTypes._A06_double_long_unsigned;
            Over_Load_By_Phase1_T3_Limit.Value = OverLoadByPhase_T3 * 1000; ///Fixed 1000 multiplier
            return Over_Load_By_Phase1_T3_Limit;
        }
        public void Decode_Limits_Over_Load_By_Phase1_T3_attrib2(Base_Class arg)
        {
            try
            {
                Class_3 Over_Load_By_Phase1_T3_Limit_ = (Class_3)arg;
                ///Verify data Receiced/OBIS/ETC
                OverLoadByPhase_T3 = Convert.ToDouble(Over_Load_By_Phase1_T3_Limit_.Value) / 1000;///Fixed 1000 Divider
            }
            catch
            {
                throw;
            }
        }

        public Base_Class Encode_Limits_Over_Load_By_Phase1_T4_attrib2(GetSAPEntry CommObjectGetter)
        {
            Class_3 Over_Load_By_Phase1_T4_Limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Limits_Over_Load_By_Phase1_T4);
            Over_Load_By_Phase1_T4_Limit.EncodingAttribute = 2;
            Over_Load_By_Phase1_T4_Limit.EncodingType = DataTypes._A06_double_long_unsigned;
            Over_Load_By_Phase1_T4_Limit.Value = OverLoadByPhase_T4 * 1000; ///Fixed 1000 multiplier
            return Over_Load_By_Phase1_T4_Limit;
        }
        public void Decode_Limits_Over_Load_By_Phase1_T4_attrib2(Base_Class arg)
        {
            try
            {
                Class_3 Over_Load_By_Phase1_T4_Limit_ = (Class_3)arg;
                ///Verify data Receiced/OBIS/ETC
                OverLoadByPhase_T4 = Convert.ToDouble(Over_Load_By_Phase1_T4_Limit_.Value) / 1000;///Fixed 1000 Divider
            }
            catch
            {
                throw;
            }
        }
        
        #endregion

        //public Base_Class Encode_Limits_Over_Load_By_Phase2_T1_attrib2(GetSAPEntry CommObjectGetter)
        //{
        //    Class_3 Over_Load_By_Phase2_T1_Limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Limits_Over_Load_By_Phase2_T1);
        //    Over_Load_By_Phase2_T1_Limit.EncodingAttribute = 2;
        //    Over_Load_By_Phase2_T1_Limit.EncodingType = DataTypes._A06_double_long_unsigned;
        //    Over_Load_By_Phase2_T1_Limit.Value = Over_Load_By_Phase2_T1;
        //    return Over_Load_By_Phase2_T1_Limit;
        //}
        //public void Decode_Limits_Over_Load_By_Phase2_T1_attrib2(Base_Class arg)
        //{
        //    try
        //    {
        //        Class_3 Over_Load_By_Phase2_T1_Limit_ = (Class_3)arg;
        //        ///Verify data Receiced/OBIS/ETC
        //        Over_Load_By_Phase2_T1 = Convert.ToDouble(Over_Load_By_Phase2_T1_Limit_.Value);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        //public Base_Class Encode_Limits_Over_Load_By_Phase2_T2_attrib2(GetSAPEntry CommObjectGetter)
        //{
        //    Class_3 Over_Load_By_Phase2_T2_Limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Limits_Over_Load_By_Phase2_T2);
        //    Over_Load_By_Phase2_T2_Limit.EncodingAttribute = 2;
        //    Over_Load_By_Phase2_T2_Limit.EncodingType = DataTypes._A06_double_long_unsigned;
        //    Over_Load_By_Phase2_T2_Limit.Value = Over_Load_By_Phase2_T2;
        //    return Over_Load_By_Phase2_T2_Limit;
        //}
        //public void Decode_Limits_Over_Load_By_Phase2_T2_attrib2(Base_Class arg)
        //{
        //    try
        //    {
        //        Class_3 Over_Load_By_Phase2_T2_Limit_ = (Class_3)arg;
        //        ///Verify data Receiced/OBIS/ETC
        //        Over_Load_By_Phase2_T2 = Convert.ToDouble(Over_Load_By_Phase2_T2_Limit_.Value);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        //public Base_Class Encode_Limits_Over_Load_By_Phase2_T3_attrib2(GetSAPEntry CommObjectGetter)
        //{
        //    Class_3 Over_Load_By_Phase2_T3_Limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Limits_Over_Load_By_Phase2_T3);
        //    Over_Load_By_Phase2_T3_Limit.EncodingAttribute = 2;
        //    Over_Load_By_Phase2_T3_Limit.EncodingType = DataTypes._A06_double_long_unsigned;
        //    Over_Load_By_Phase2_T3_Limit.Value = Over_Load_By_Phase2_T3;
        //    return Over_Load_By_Phase2_T3_Limit;
        //}
        //public void Decode_Limits_Over_Load_By_Phase2_T3_attrib2(Base_Class arg)
        //{
        //    try
        //    {
        //        Class_3 Over_Load_By_Phase2_T3_Limit_ = (Class_3)arg;
        //        ///Verify data Receiced/OBIS/ETC
        //        Over_Load_By_Phase2_T3 = Convert.ToDouble(Over_Load_By_Phase2_T3_Limit_.Value);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}



        //public Base_Class Encode_Limits_Over_Load_By_Phase3_T1_attrib2(GetSAPEntry CommObjectGetter)
        //{
        //    Class_3 Over_Load_By_Phase3_T1_Limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Limits_Over_Load_By_Phase3_T1);
        //    Over_Load_By_Phase3_T1_Limit.EncodingAttribute = 2;
        //    Over_Load_By_Phase3_T1_Limit.EncodingType = DataTypes._A06_double_long_unsigned;
        //    Over_Load_By_Phase3_T1_Limit.Value = Over_Load_By_Phase3_T1;
        //    return Over_Load_By_Phase3_T1_Limit;
        //}
        //public void Decode_Limits_Over_Load_By_Phase3_T1_attrib2(Base_Class arg)
        //{
        //    try
        //    {
        //        Class_3 Over_Load_By_Phase3_T1_Limit_ = (Class_3)arg;
        //        ///Verify data Receiced/OBIS/ETC
        //        Over_Load_By_Phase3_T1 = Convert.ToDouble(Over_Load_By_Phase3_T1_Limit_.Value);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        //public Base_Class Encode_Limits_Over_Load_By_Phase3_T2_attrib2(GetSAPEntry CommObjectGetter)
        //{
        //    Class_3 Over_Load_By_Phase3_T2_Limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Limits_Over_Load_By_Phase3_T2);
        //    Over_Load_By_Phase3_T2_Limit.EncodingAttribute = 2;
        //    Over_Load_By_Phase3_T2_Limit.EncodingType = DataTypes._A06_double_long_unsigned;
        //    Over_Load_By_Phase3_T2_Limit.Value = Over_Load_By_Phase3_T2;
        //    return Over_Load_By_Phase3_T2_Limit;
        //}
        //public void Decode_Limits_Over_Load_By_Phase3_T2_attrib2(Base_Class arg)
        //{
        //    try
        //    {
        //        Class_3 Over_Load_By_Phase3_T2_Limit_ = (Class_3)arg;
        //        ///Verify data Receiced/OBIS/ETC
        //        Over_Load_By_Phase3_T2 = Convert.ToDouble(Over_Load_By_Phase3_T2_Limit_.Value);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        //public Base_Class Encode_Limits_Over_Load_By_Phase3_T3_attrib2(GetSAPEntry CommObjectGetter)
        //{
        //    Class_3 Over_Load_By_Phase3_T3_Limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Limits_Over_Load_By_Phase3_T3);
        //    Over_Load_By_Phase3_T3_Limit.EncodingAttribute = 2;
        //    Over_Load_By_Phase3_T3_Limit.EncodingType = DataTypes._A06_double_long_unsigned;
        //    Over_Load_By_Phase3_T3_Limit.Value = Over_Load_By_Phase3_T3;
        //    return Over_Load_By_Phase3_T3_Limit;
        //}
        //public void Decode_Limits_Over_Load_By_Phase3_T3_attrib2(Base_Class arg)
        //{
        //    try
        //    {
        //        Class_3 Over_Load_By_Phase3_T3_Limit_ = (Class_3)arg;
        //        ///Verify data Receiced/OBIS/ETC
        //        Over_Load_By_Phase3_T3 = Convert.ToDouble(Over_Load_By_Phase3_T3_Limit_.Value);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}


        //public Base_Class Encode_Limits_Over_Load_By_Phase2_T4_attrib2(GetSAPEntry CommObjectGetter)
        //{
        //    Class_3 Over_Load_By_Phase2_T4_Limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Limits_Over_Load_By_Phase2_T4);
        //    Over_Load_By_Phase2_T4_Limit.EncodingAttribute = 2;
        //    Over_Load_By_Phase2_T4_Limit.EncodingType = DataTypes._A06_double_long_unsigned;
        //    Over_Load_By_Phase2_T4_Limit.Value = Over_Load_By_Phase2_T4;
        //    return Over_Load_By_Phase2_T4_Limit;
        //}
        //public void Decode_Limits_Over_Load_By_Phase2_T4_attrib2(Base_Class arg)
        //{
        //    try
        //    {
        //        Class_3 Over_Load_By_Phase2_T4_Limit_ = (Class_3)arg;
        //        ///Verify data Receiced/OBIS/ETC
        //        Over_Load_By_Phase2_T4 = Convert.ToDouble(Over_Load_By_Phase2_T4_Limit_.Value);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}


        //public Base_Class Encode_Limits_Over_Load_By_Phase3_T4_attrib2(GetSAPEntry CommObjectGetter)
        //{
        //    Class_3 Over_Load_By_Phase3_T4_Limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Limits_Over_Load_By_Phase3_T4);
        //    Over_Load_By_Phase3_T4_Limit.EncodingAttribute = 2;
        //    Over_Load_By_Phase3_T4_Limit.EncodingType = DataTypes._A06_double_long_unsigned;
        //    Over_Load_By_Phase3_T4_Limit.Value = Over_Load_By_Phase3_T4;
        //    return Over_Load_By_Phase3_T4_Limit;
        //}

        //public void Decode_Limits_Over_Load_By_Phase3_T4_attrib2(Base_Class arg)
        //{
        //    try
        //    {
        //        Class_3 Over_Load_By_Phase3_T4_Limit_ = (Class_3)arg;
        //        ///Verify data Receiced/OBIS/ETC
        //        Over_Load_By_Phase3_T4 = Convert.ToDouble(Over_Load_By_Phase3_T4_Limit_.Value);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        #region Over_Load_Total

        public Base_Class Encode_Limits_Over_Load_Total_T1_attrib2(GetSAPEntry CommObjectGetter)
        {
            Class_3 Over_Load_Total_T1_Limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Limits_Over_Load_Total_T1);
            Over_Load_Total_T1_Limit.EncodingAttribute = 2;
            Over_Load_Total_T1_Limit.EncodingType = DataTypes._A06_double_long_unsigned;
            Over_Load_Total_T1_Limit.Value = OverLoadTotal_T1 * 1000; ///Fixed 1000 multiplier
            return Over_Load_Total_T1_Limit;
        }
        public void Decode_Limits_Over_Load_Total_T1_attrib2(Base_Class arg)
        {
            try
            {
                Class_3 Over_Load_Total_T1_Limit = (Class_3)arg;
                ///Verify data Receiced/OBIS/ETC
                OverLoadTotal_T1 = Convert.ToDouble(Over_Load_Total_T1_Limit.Value) / 1000;///Fixed 1000 Divider
            }
            catch
            {
                throw;
            }
        }

        public Base_Class Encode_Limits_Over_Load_Total_T2_attrib2(GetSAPEntry CommObjectGetter)
        {
            Class_3 Over_Load_Total_T2_Limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Limits_Over_Load_Total_T2);
            Over_Load_Total_T2_Limit.EncodingAttribute = 2;
            Over_Load_Total_T2_Limit.EncodingType = DataTypes._A06_double_long_unsigned;
            Over_Load_Total_T2_Limit.Value = OverLoadTotal_T2 * 1000; ///Fixed 1000 multiplier
            return Over_Load_Total_T2_Limit;
        }
        public void Decode_Limits_Over_Load_Total_T2_attrib2(Base_Class arg)
        {
            try
            {
                Class_3 Over_Load_Total_T2_Limit = (Class_3)arg;
                ///Verify data Receiced/OBIS/ETC
                OverLoadTotal_T2 = Convert.ToDouble(Over_Load_Total_T2_Limit.Value) / 1000;///Fixed 1000 Divider
            }
            catch 
            {
                throw;
            }
        }

        public Base_Class Encode_Limits_Over_Load_Total_T3_attrib2(GetSAPEntry CommObjectGetter)
        {
            Class_3 Over_Load_Total_T3_Limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Limits_Over_Load_Total_T3);
            Over_Load_Total_T3_Limit.EncodingAttribute = 2;
            Over_Load_Total_T3_Limit.EncodingType = DataTypes._A06_double_long_unsigned;
            Over_Load_Total_T3_Limit.Value = OverLoadTotal_T3 * 1000; ///Fixed 1000 multiplier
            return Over_Load_Total_T3_Limit;
        }
        public void Decode_Limits_Over_Load_Total_T3_attrib2(Base_Class arg)
        {
            try
            {
                Class_3 Over_Load_Total_T3_Limit = (Class_3)arg;
                ///Verify data Receiced/OBIS/ETC
                OverLoadTotal_T3 = Convert.ToDouble(Over_Load_Total_T3_Limit.Value) / 1000;///Fixed 1000 Divider
            }
            catch
            {
                throw;
            }
        }

        public Base_Class Encode_Limits_Over_Load_Total_T4_attrib2(GetSAPEntry CommObjectGetter)
        {
            Class_3 Over_Load_Total_T4_Limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Limits_Over_Load_Total_T4);
            Over_Load_Total_T4_Limit.EncodingAttribute = 2;
            Over_Load_Total_T4_Limit.EncodingType = DataTypes._A06_double_long_unsigned;
            Over_Load_Total_T4_Limit.Value = OverLoadTotal_T4 * 1000; ///Fixed 1000 multiplier
            return Over_Load_Total_T4_Limit;
        }
        //***************************************************************
        public void Decode_Limits_Over_Load_Total_T4_attrib2(Base_Class arg)
        {
            try
            {
                Class_3 Over_Load_Total_T4_Limit = (Class_3)arg;
                ///Verify data Receiced/OBIS/ETC
                OverLoadTotal_T4 = Convert.ToDouble(Over_Load_Total_T4_Limit.Value) / 1000;///Fixed 1000 Divider
            }
            catch
            {
                throw;
            }
        } 
        
        #endregion

        #region MDI_Exceed
        
        public void Decode_Limits_MDI_Exceed_T1_attrib2(Base_Class arg)
        {
            try
            {
                Class_21 MDI_Exceed_T1_Limit = (Class_21)arg;
                ///Verify data Receiced/OBIS/ETC
                DemandOverLoadTotal_T1 = Math.Round(Convert.ToDouble(MDI_Exceed_T1_Limit.Thresholds[0]) / 1000, 3);
            }
            catch
            {
                throw;
            }
        }

        public void Decode_Limits_MDI_Exceed_T2_attrib2(Base_Class arg)
        {
            try
            {
                Class_21 MDI_Exceed_T2_Limit = (Class_21)arg;
                ///Verify data Receiced/OBIS/ETC
                DemandOverLoadTotal_T2 = Math.Round(Convert.ToDouble(MDI_Exceed_T2_Limit.Thresholds[0]) / 1000, 03);
            }
            catch
            {
                throw;
            }
        }

        public void Decode_Limits_MDI_Exceed_T3_attrib2(Base_Class arg)
        {
            try
            {
                Class_21 MDI_Exceed_T3_Limit = (Class_21)arg;
                ///Verify data Receiced/OBIS/ETC
                DemandOverLoadTotal_T3 = Math.Round(Convert.ToDouble(MDI_Exceed_T3_Limit.Thresholds[0]) / 1000, 03);
            }
            catch
            {
                throw;
            }
        }

        public void Decode_Limits_MDI_Exceed_T4_attrib2(Base_Class arg)
        {
            try
            {
                Class_21 MDI_Exceed_T4_Limit = (Class_21)arg;
                ///Verify data Receiced/OBIS/ETC
                DemandOverLoadTotal_T4 = Math.Round(Convert.ToDouble(MDI_Exceed_T4_Limit.Thresholds[0]) / 1000,03);
            }
            catch
            {
                throw;
            }
        }

        #endregion


        /* for Single Phase // Azeem */

        public Base_Class Encode_OverPower_Limit(GetSAPEntry CommObjectGetter)
        {
            Class_3 limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Over_Power_Limit);
            limit.EncodingAttribute = 2;
            limit.EncodingType = DataTypes._A06_double_long_unsigned;
            limit.Value = OverPower * 1000;
            return limit;
        }

        public void Decode_OverPower_Limit(Base_Class arg)
        {
            try
            {
                Class_3 limit = (Class_3)arg;
                ///Verify data Receiced/OBIS/ETC
                OverPower = Convert.ToDouble(limit.Value) / 1000;
            }
            catch
            {
                throw;
            }
        }
        public Base_Class Encode_Limits_Over_Current_Phase_attrib2(GetSAPEntry CommObjectGetter)
        {
            Class_3 Over_Current_Limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Over_Current_Limit);
            Over_Current_Limit.EncodingAttribute = 2;
            Over_Current_Limit.EncodingType = DataTypes._A12_long_unsigned;
            Over_Current_Limit.Value = OverCurrent_Phase * 100;
            return Over_Current_Limit;
        }

        public void Decode_Limits_Over_Current_Phase_attrib2(Base_Class arg)
        {
            try
            {
                Class_3 Over_Current_Limit = (Class_3)arg;
                ///Verify data Receiced/OBIS/ETC
                OverCurrent_Phase = Convert.ToDouble(Over_Current_Limit.Value);
            }
            catch
            {
                throw;
            }
        }

        public Base_Class Encode_MeterOnLoad_Limit(GetSAPEntry CommObjectGetter)
        {
            Class_3 limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Limits_Meter_ON_LOAD);
            limit.EncodingAttribute = 2;
            limit.EncodingType = DataTypes._A06_double_long_unsigned;
            limit.Value = MeterOnLoad * 1000;
            return limit;
        }

        public void Decode_MeterOnLoad_Limit(Base_Class arg)
        {
            try
            {
                Class_3 limit = (Class_3)arg;
                ///Verify data Receiced/OBIS/ETC
                MeterOnLoad = Convert.ToDouble(limit.Value)/1000;
            }
            catch
            {
                throw;
            }
        }

        public Base_Class Encode_PowerFactor_Change_Limit(GetSAPEntry CommObjectGetter)
        {
            Class_3 limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Limits_POWER_FACTOR_CHANGE);
            limit.EncodingAttribute = 2;
            limit.EncodingType = DataTypes._A12_long_unsigned;
            limit.Value = PowerFactor_Change * 100;
            return limit;
        }

        public void Decode_PowerFactor_Change_Limit(Base_Class arg)
        {
            try
            {
                Class_3 limit = (Class_3)arg;
                ///Verify data Receiced/OBIS/ETC
                PowerFactor_Change = Convert.ToDouble(limit.Value);
            }
            catch
            {
                throw;
            }
        }

        public Base_Class Encode_Crest_Factor_Low_Limit(GetSAPEntry CommObjectGetter)
        {
            Class_3 limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Limits_Crest_Factor_Low);
            limit.EncodingAttribute = 2;
            limit.EncodingType = DataTypes._A12_long_unsigned;
            limit.Value = CrestFactorLow * 100;
            return limit;
        }

        public void Decode_Crest_Factor_Low_Limit(Base_Class arg)
        {
            try
            {
                Class_3 limit = (Class_3)arg;
                ///Verify data Receiced/OBIS/ETC
                CrestFactorLow = Convert.ToDouble(limit.Value);
            }
            catch
            {
                throw;
            }
        }

        public Base_Class Encode_Crest_Factor_High_Limit(GetSAPEntry CommObjectGetter)
        {
            Class_3 limit = (Class_3)CommObjectGetter.Invoke(Get_Index.Limits_Crest_Factor_High);
            limit.EncodingAttribute = 2;
            limit.EncodingType = DataTypes._A12_long_unsigned;
            limit.Value = CrestFactorHigh * 100;
            return limit;
        }

        public void Decode_Crest_Factor_High_Limit(Base_Class arg)
        {
            try
            {
                Class_3 limit = (Class_3)arg;
                ///Verify data Receiced/OBIS/ETC
                CrestFactorHigh = Convert.ToDouble(limit.Value);
            }
            catch
            {
                throw;
            }
        }



        //***************************************************************
        //***************************************************************
        public Base_Class Encode_any_class1(GetSAPEntry CommObjectGetter, Get_Index Get_any_arg, byte encoding_attrib, double value, DataTypes arg_DataTypes)
        {
            Class_1 class_1_obj = (Class_1)CommObjectGetter.Invoke(Get_any_arg);
            class_1_obj.EncodingAttribute = encoding_attrib;
            class_1_obj.EncodingType = arg_DataTypes;
            class_1_obj.Value = value;
            return class_1_obj;
        }

        #endregion

        #region Commented_CodeSection

        //public OBIS_data_from_GUI[] encode_ALL_attrib()
        //{

        //    OBIS_data_from_GUI[] structToReturn = new OBIS_data_from_GUI[100];
        //    int i = 0;
        //    structToReturn[i++] = encode_Over_Voltage_Limit_attrib2();
        //    structToReturn[i++] = encode_Over_Voltage_Limit_attrib3();
        //    structToReturn[i++] = encode_Under_Voltage_Limit_attrib2();
        //    structToReturn[i++] = encode_Under_Voltage_Limit_attrib3();
        //    //structToReturn[i++] = encode_Limits_Over_Current_By_Phase_T1_attrib2();
        //    //structToReturn[i++] = encode_Limits_Over_Current_By_Phase_T1_attrib3();


        //    //structToReturn[i++] = encode_Limits_Imbalance_Volt_attrib2();
        //    //structToReturn[i++] = encode_Limits_High_Neutral_Current_attrib2();
        //    //structToReturn[i++] = encode_Limits_Reverse_Energy_attrib2();
        //    //structToReturn[i++] = encode_Limits_Tamper_Energy_attrib2();
        //    //structToReturn[i++] = encode_Limits_CT_Fail__Amp_Limit_attrib2();
        //    //structToReturn[i++] = encode_Limits_PT_Fail_Amp_Limit_attrib2();
        //    //structToReturn[i++] = encode_Limits_PT_Fail_Volt_Limit_attrib2();
        //    //structToReturn[i++] = encode_Limits_Over_Current_By_Phase_T1_attrib2();
        //    //structToReturn[i++] = encode_Limits_Over_Current_By_Phase_T2_attrib2();
        //    //structToReturn[i++] = encode_Limits_Over_Current_By_Phase_T3_attrib2();
        //    //structToReturn[i++] = encode_Limits_Over_Current_By_Phase_T4_attrib2();
        //    //structToReturn[i++] = encode_Limits_Over_Load_By_Phase1_T1_attrib2();
        //    //structToReturn[i++] = encode_Limits_Over_Load_By_Phase1_T2_attrib2();
        //    //structToReturn[i++] = encode_Limits_Over_Load_By_Phase1_T3_attrib2();
        //    //structToReturn[i++] = encode_Limits_Over_Load_By_Phase1_T4_attrib2();
        //    //structToReturn[i++] = encode_Limits_Over_Load_By_Phase2_T1_attrib2();
        //    //structToReturn[i++] = encode_Limits_Over_Load_By_Phase2_T2_attrib2();
        //    //structToReturn[i++] = encode_Limits_Over_Load_By_Phase2_T3_attrib2();
        //    //structToReturn[i++] = encode_Limits_Over_Load_By_Phase2_T4_attrib2();
        //    //structToReturn[i++] = encode_Limits_Over_Load_By_Phase3_T1_attrib2();
        //    //structToReturn[i++] = encode_Limits_Over_Load_By_Phase3_T2_attrib2();
        //    //structToReturn[i++] = encode_Limits_Over_Load_By_Phase3_T3_attrib2();
        //    //structToReturn[i++] = encode_Limits_Over_Load_By_Phase3_T4_attrib2();
        //    //structToReturn[i++] = encode_Limits_Over_Load_Total_T1_attrib2();
        //    //structToReturn[i++] = encode_Limits_Over_Load_Total_T2_attrib2();
        //    //structToReturn[i++] = encode_Limits_Over_Load_Total_T3_attrib2();
        //    //structToReturn[i++] = encode_Limits_Over_Load_Total_T4_attrib2();
        //    //structToReturn[i++] = encode_Limits_Demand_Over_Load_T1_attrib2();
        //    //structToReturn[i++] = encode_Limits_Demand_Over_Load_T2_attrib2();
        //    //structToReturn[i++] = encode_Limits_Demand_Over_Load_T3_attrib2();
        //    //structToReturn[i++] = encode_Limits_Demand_Over_Load_T4_attrib2();

        //    Array.Resize(ref structToReturn, i);
        //    return structToReturn;
        //}

        //public OBIS_data_from_GUI encode_Over_Voltage_Limit_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Over_Voltage_Limit;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
        //    obj_OBIS_data_from_GUI.Data = Over_Volt;

        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Over_Voltage_Limit_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Over_Voltage_Limit;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B23_voltage);
        //    return obj_OBIS_data_from_GUI;
        //}




        //public OBIS_data_from_GUI encode_Under_Voltage_Limit_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Under_Voltage_Limit;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
        //    obj_OBIS_data_from_GUI.Data = Under_Volt;

        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Under_Voltage_Limit_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Under_Voltage_Limit;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B23_voltage);
        //    return obj_OBIS_data_from_GUI;
        //}

        //public OBIS_data_from_GUI encode_Limits_Imbalance_Volt_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Imbalance_Volt;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
        //    obj_OBIS_data_from_GUI.Data = Imbalance_Volt;

        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Limits_Imbalance_Volt_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Imbalance_Volt;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B23_voltage);
        //    return obj_OBIS_data_from_GUI;
        //}

        //public OBIS_data_from_GUI encode_Limits_High_Neutral_Current_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_High_Neutral_Current;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
        //    obj_OBIS_data_from_GUI.Data = High_Neutral_Current;

        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Limits_High_Neutral_Current_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_High_Neutral_Current;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B21_current);
        //    return obj_OBIS_data_from_GUI;
        //}

        //public OBIS_data_from_GUI encode_Limits_Reverse_Energy_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Reverse_Energy_;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
        //    obj_OBIS_data_from_GUI.Data = Reverse_Energy;

        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Limits_Reverse_Energy_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Reverse_Energy_;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B1E_active_energy_watt_hour);
        //    return obj_OBIS_data_from_GUI;
        //}

        //public OBIS_data_from_GUI encode_Limits_Tamper_Energy_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Tamper_Energy;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
        //    obj_OBIS_data_from_GUI.Data = Tamper_Energy;

        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Limits_Tamper_Energy_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Tamper_Energy;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B1E_active_energy_watt_hour);
        //    return obj_OBIS_data_from_GUI;
        //}

        //public OBIS_data_from_GUI encode_Limits_CT_Fail__Amp_Limit_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_CT_Fail__Amp_Limit;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
        //    obj_OBIS_data_from_GUI.Data = CT_Fail_Amp_Limit;

        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Limits_CT_Fail__Amp_Limit_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_CT_Fail__Amp_Limit;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B21_current);
        //    return obj_OBIS_data_from_GUI;
        //}

        //public OBIS_data_from_GUI encode_Limits_PT_Fail_Amp_Limit_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_PT_Fail_Amp_Limit;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
        //    obj_OBIS_data_from_GUI.Data = PT_Fail_Amp_Limit;

        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Limits_PT_Fail_Amp_Limit_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_PT_Fail_Amp_Limit;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B21_current);
        //    return obj_OBIS_data_from_GUI;
        //}

        //public OBIS_data_from_GUI encode_Limits_PT_Fail_Volt_Limit_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_PT_Fail_Volt_Limit;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
        //    obj_OBIS_data_from_GUI.Data = PT_Fail_Volt_Limit;

        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Limits_PT_Fail_Volt_Limit_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_PT_Fail_Volt_Limit;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B23_voltage);
        //    return obj_OBIS_data_from_GUI;
        //}


        //public OBIS_data_from_GUI encode_Limits_Over_Current_By_Phase_T1_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Current_By_Phase_T1;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
        //    obj_OBIS_data_from_GUI.Data = Over_Current_By_Phase_T1;

        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Limits_Over_Current_By_Phase_T1_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Current_By_Phase_T1;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B21_current);
        //    return obj_OBIS_data_from_GUI;
        //}


        //public OBIS_data_from_GUI encode_Limits_Over_Current_By_Phase_T2_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Current_By_Phase_T2;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
        //    obj_OBIS_data_from_GUI.Data = Over_Current_By_Phase_T2;

        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Limits_Over_Current_By_Phase_T2_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Current_By_Phase_T2;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B21_current);
        //    return obj_OBIS_data_from_GUI;
        //}


        //public OBIS_data_from_GUI encode_Limits_Over_Current_By_Phase_T3_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Current_By_Phase_T3;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
        //    obj_OBIS_data_from_GUI.Data = Over_Current_By_Phase_T3;

        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Limits_Over_Current_By_Phase_T3_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Current_By_Phase_T3;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B21_current);
        //    return obj_OBIS_data_from_GUI;
        //}


        //public OBIS_data_from_GUI encode_Limits_Over_Current_By_Phase_T4_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Current_By_Phase_T4;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
        //    obj_OBIS_data_from_GUI.Data = Over_Current_By_Phase_T4;

        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Limits_Over_Current_By_Phase_T4_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Current_By_Phase_T4;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B21_current);
        //    return obj_OBIS_data_from_GUI;
        //}


        //public OBIS_data_from_GUI encode_Limits_Over_Load_By_Phase1_T1_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Load_By_Phase1_T1;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
        //    obj_OBIS_data_from_GUI.Data = Over_Load_By_Phase1_T1;

        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Limits_Over_Load_By_Phase1_T1_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Load_By_Phase1_T1;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B1B_active_power_WATT);
        //    return obj_OBIS_data_from_GUI;
        //}


        //public OBIS_data_from_GUI encode_Limits_Over_Load_By_Phase1_T2_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Load_By_Phase1_T2;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
        //    obj_OBIS_data_from_GUI.Data = Over_Load_By_Phase1_T2;

        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Limits_Over_Load_By_Phase1_T2_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Load_By_Phase2_T1;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B1B_active_power_WATT);
        //    return obj_OBIS_data_from_GUI;
        //}


        //public OBIS_data_from_GUI encode_Limits_Over_Load_By_Phase1_T3_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Load_By_Phase1_T3;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
        //    obj_OBIS_data_from_GUI.Data = Over_Load_By_Phase1_T3;

        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Limits_Over_Load_By_Phase1_T3_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Load_By_Phase3_T1;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B1B_active_power_WATT);
        //    return obj_OBIS_data_from_GUI;
        //}

        //public OBIS_data_from_GUI encode_Limits_Over_Load_By_Phase2_T1_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Load_By_Phase2_T1;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
        //    obj_OBIS_data_from_GUI.Data = Over_Load_By_Phase1_T1;

        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Limits_Over_Load_By_Phase2_T1_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Load_By_Phase2_T1;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B1B_active_power_WATT);
        //    return obj_OBIS_data_from_GUI;
        //}

        //public OBIS_data_from_GUI encode_Limits_Over_Load_By_Phase2_T2_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Load_By_Phase2_T2;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
        //    obj_OBIS_data_from_GUI.Data = Over_Load_By_Phase1_T2;

        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Limits_Over_Load_By_Phase2_T2_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Load_By_Phase2_T2;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B1B_active_power_WATT);
        //    return obj_OBIS_data_from_GUI;
        //}

        //public OBIS_data_from_GUI encode_Limits_Over_Load_By_Phase2_T3_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Load_By_Phase2_T3;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
        //    obj_OBIS_data_from_GUI.Data = Over_Load_By_Phase1_T3;

        //    return obj_OBIS_data_from_GUI;
        //}

        //public OBIS_data_from_GUI encode_Limits_Over_Load_By_Phase2_T3_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Load_By_Phase2_T3;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B1B_active_power_WATT);
        //    return obj_OBIS_data_from_GUI;
        //}


        //public OBIS_data_from_GUI encode_Limits_Over_Load_By_Phase3_T1_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Load_By_Phase3_T1;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
        //    obj_OBIS_data_from_GUI.Data = Over_Load_By_Phase1_T1;

        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Limits_Over_Load_By_Phase3_T1_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Load_By_Phase3_T1;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B1B_active_power_WATT);
        //    return obj_OBIS_data_from_GUI;
        //}


        //public OBIS_data_from_GUI encode_Limits_Over_Load_By_Phase3_T2_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Load_By_Phase3_T2;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
        //    obj_OBIS_data_from_GUI.Data = Over_Load_By_Phase1_T2;

        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Limits_Over_Load_By_Phase3_T2_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Load_By_Phase3_T2;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B1B_active_power_WATT);
        //    return obj_OBIS_data_from_GUI;
        //}


        //public OBIS_data_from_GUI encode_Limits_Over_Load_By_Phase3_T3_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Load_By_Phase3_T3;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
        //    obj_OBIS_data_from_GUI.Data = Over_Load_By_Phase1_T3;

        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Limits_Over_Load_By_Phase3_T3_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Load_By_Phase3_T3;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B1B_active_power_WATT);
        //    return obj_OBIS_data_from_GUI;
        //}


        //public OBIS_data_from_GUI encode_Limits_Over_Load_By_Phase1_T4_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Load_By_Phase1_T4;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
        //    obj_OBIS_data_from_GUI.Data = Over_Load_By_Phase1_T4;

        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Limits_Over_Load_By_Phase1_T4_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Load_By_Phase1_T4;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B1B_active_power_WATT);
        //    return obj_OBIS_data_from_GUI;
        //}


        //public OBIS_data_from_GUI encode_Limits_Over_Load_By_Phase2_T4_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Load_By_Phase2_T4;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
        //    obj_OBIS_data_from_GUI.Data = Over_Load_By_Phase1_T4;

        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Limits_Over_Load_By_Phase2_T4_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Load_By_Phase2_T4;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B1B_active_power_WATT);
        //    return obj_OBIS_data_from_GUI;
        //}


        //public OBIS_data_from_GUI encode_Limits_Over_Load_By_Phase3_T4_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Load_By_Phase3_T4;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
        //    obj_OBIS_data_from_GUI.Data = Over_Load_By_Phase1_T4;

        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Limits_Over_Load_By_Phase3_T4_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Load_By_Phase3_T4;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B1B_active_power_WATT);
        //    return obj_OBIS_data_from_GUI;
        //}


        //public OBIS_data_from_GUI encode_Limits_Over_Load_Total_T1_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Load_Total_T1;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
        //    obj_OBIS_data_from_GUI.Data = Over_Load_Total_T1;

        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Limits_Over_Load_Total_T1_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Load_Total_T1;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B1B_active_power_WATT);
        //    return obj_OBIS_data_from_GUI;
        //}


        //public OBIS_data_from_GUI encode_Limits_Over_Load_Total_T2_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Load_Total_T2;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
        //    obj_OBIS_data_from_GUI.Data = Over_Load_Total_T2;

        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Limits_Over_Load_Total_T2_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Load_Total_T2;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B1B_active_power_WATT);
        //    return obj_OBIS_data_from_GUI;
        //}

        //public OBIS_data_from_GUI encode_Limits_Over_Load_Total_T3_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Load_Total_T3;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
        //    obj_OBIS_data_from_GUI.Data = Over_Load_Total_T3;

        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Limits_Over_Load_Total_T3_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Load_Total_T3;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B1B_active_power_WATT);
        //    return obj_OBIS_data_from_GUI;
        //}


        //public OBIS_data_from_GUI encode_Limits_Over_Load_Total_T4_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Load_Total_T4;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
        //    obj_OBIS_data_from_GUI.Data = Over_Load_Total_T4;

        //    return obj_OBIS_data_from_GUI;
        //}
        //public OBIS_data_from_GUI encode_Limits_Over_Load_Total_T4_attrib3()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Over_Load_Total_T4;
        //    obj_OBIS_data_from_GUI.attribute = 3;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A02_structure;
        //    obj_OBIS_data_from_GUI.Data_Array = DLMS.BasicEncodeDecode.encode_class_3_attribute_3(0, units._B1B_active_power_WATT);
        //    return obj_OBIS_data_from_GUI;
        //}


        //public OBIS_data_from_GUI encode_Limits_Demand_Over_Load_T1_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Demand_Over_Load_T1;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
        //    obj_OBIS_data_from_GUI.Data = Demand_Over_Load_Total_T1;

        //    return obj_OBIS_data_from_GUI;
        //}

        //public OBIS_data_from_GUI encode_Limits_Demand_Over_Load_T2_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Demand_Over_Load_T2;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
        //    obj_OBIS_data_from_GUI.Data = Demand_Over_Load_Total_T2;

        //    return obj_OBIS_data_from_GUI;
        //}

        //public OBIS_data_from_GUI encode_Limits_Demand_Over_Load_T3_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Demand_Over_Load_T3;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
        //    obj_OBIS_data_from_GUI.Data = Demand_Over_Load_Total_T3;

        //    return obj_OBIS_data_from_GUI;
        //}

        //public OBIS_data_from_GUI encode_Limits_Demand_Over_Load_T4_attrib2()
        //{
        //    OBIS_data_from_GUI obj_OBIS_data_from_GUI = new OBIS_data_from_GUI();
        //    obj_OBIS_data_from_GUI.OBIS_code = Get_Index.Limits_Demand_Over_Load_T4;
        //    obj_OBIS_data_from_GUI.attribute = 2;
        //    obj_OBIS_data_from_GUI.Type = DataTypes._A12_long_unsigned;
        //    obj_OBIS_data_from_GUI.Data = Demand_Over_Load_Total_T4;

        //    return obj_OBIS_data_from_GUI;
        //}

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
