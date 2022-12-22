using System;

namespace SharedCode.Comm.HelperClasses
{
    public class LimitValues
    {
        #region Data Members

        public const string Default_CurrentMeter = "R326";//"ACT34G";
        private string currentMeter;

        private double[] _valuesMin = null;
        private double[] _valuesMax = null;

        private double contactorFailure_min = 0; //Double.MinValue;
        private double contactorFailure_max = 100; //Double.MaxValue;

        #endregion

        #region Properties

        public string CurrentMeter
        {
            get { return currentMeter; }
            set { currentMeter = value; }
        }

        #region Limits_MIN

        public double ContactorFailure_min
        {
            get { return contactorFailure_min; }
            set { contactorFailure_min = value; }
        }

        public double OverVolt_MIN
        {
            get { return GetLimitMin(ThreshouldItem.OverVolt); }
            set { SetLimitMin(ThreshouldItem.OverVolt, value); }
        }

        public double UnderVolt_MIN
        {
            get { return GetLimitMin(ThreshouldItem.UnderVolt); }
            set { SetLimitMin(ThreshouldItem.UnderVolt, value); }
        }

        public double ImbalanceVolt_MIN
        {
            get { return GetLimitMin(ThreshouldItem.ImbalanceVolt); }
            set { SetLimitMin(ThreshouldItem.ImbalanceVolt, value); }
        }

        public double HighNeutralCurrent_MIN
        {
            get { return GetLimitMin(ThreshouldItem.HighNeutralCurrent); }
            set { SetLimitMin(ThreshouldItem.HighNeutralCurrent, value); }
        }

        public double ReverseEnergy_MIN
        {
            get { return GetLimitMin(ThreshouldItem.ReverseEnergy); }
            set { SetLimitMin(ThreshouldItem.ReverseEnergy, value); }
        }

        public double TamperEnergy_MIN
        {
            get { return GetLimitMin(ThreshouldItem.TamperEnergy); }
            set { SetLimitMin(ThreshouldItem.TamperEnergy, value); }
        }

        public double CTFail_MIN
        {
            get { return GetLimitMin(ThreshouldItem.CTFail); }
            set { SetLimitMin(ThreshouldItem.CTFail, value); }
        }

        public double PTFail_AMP_MIN
        {
            get { return GetLimitMin(ThreshouldItem.PTFail_Amp); }
            set { SetLimitMin(ThreshouldItem.PTFail_Amp, value); }
        }

        public double PTFail_Volt_MIN
        {
            get { return GetLimitMin(ThreshouldItem.PTFail_Volt); }
            set { SetLimitMin(ThreshouldItem.PTFail_Volt, value); }
        }

        public double OverCurrentByPhase_T1_MIN
        {
            get { return GetLimitMin(ThreshouldItem.OverCurrentByPhase_T1); }
            set { SetLimitMin(ThreshouldItem.OverCurrentByPhase_T1, value); }
        }

        public double OverCurrentByPhase_T2_MIN
        {
            get { return GetLimitMin(ThreshouldItem.OverCurrentByPhase_T2); }
            set { SetLimitMin(ThreshouldItem.OverCurrentByPhase_T2, value); }
        }

        public double OverCurrentByPhase_T3_MIN
        {
            get { return GetLimitMin(ThreshouldItem.OverCurrentByPhase_T3); }
            set { SetLimitMin(ThreshouldItem.OverCurrentByPhase_T3, value); }
        }

        public double OverCurrentByPhase_T4_MIN
        {
            get { return GetLimitMin(ThreshouldItem.OverCurrentByPhase_T4); }
            set { SetLimitMin(ThreshouldItem.OverCurrentByPhase_T4, value); }
        }

        public double OverLoadByPhase_T1_MIN
        {
            get { return GetLimitMin(ThreshouldItem.OverLoadByPhase_T1); }
            set { SetLimitMin(ThreshouldItem.OverLoadByPhase_T1, value); }
        }

        public double OverLoadByPhase_T2_MIN
        {
            get { return GetLimitMin(ThreshouldItem.OverLoadByPhase_T2); }
            set { SetLimitMin(ThreshouldItem.OverLoadByPhase_T2, value); }
        }

        public double OverLoadByPhase_T3_MIN
        {
            get { return GetLimitMin(ThreshouldItem.OverLoadByPhase_T3); }
            set { SetLimitMin(ThreshouldItem.OverLoadByPhase_T3, value); }
        }

        public double OverLoadByPhase_T4_MIN
        {
            get { return GetLimitMin(ThreshouldItem.OverLoadByPhase_T4); }
            set { SetLimitMin(ThreshouldItem.OverLoadByPhase_T4, value); }
        }

        public double OverLoadTotal_T1_MIN
        {
            get { return GetLimitMin(ThreshouldItem.OverLoadTotal_T1); }
            set { SetLimitMin(ThreshouldItem.OverLoadTotal_T1, value); }
        }

        public double OverLoadTotal_T2_MIN
        {
            get { return GetLimitMin(ThreshouldItem.OverLoadTotal_T2); }
            set { SetLimitMin(ThreshouldItem.OverLoadTotal_T2, value); }
        }

        public double OverLoadTotal_T3_MIN
        {
            get { return GetLimitMin(ThreshouldItem.OverLoadTotal_T3); }
            set { SetLimitMin(ThreshouldItem.OverLoadTotal_T3, value); }
        }

        public double OverLoadTotal_T4_MIN
        {
            get { return GetLimitMin(ThreshouldItem.OverLoadTotal_T4); }
            set { SetLimitMin(ThreshouldItem.OverLoadTotal_T4, value); }
        }

        public double MDIExceed_T1_MIN
        {
            get { return GetLimitMin(ThreshouldItem.MDIExceed_T1); }
            set { SetLimitMin(ThreshouldItem.MDIExceed_T1, value); }
        }

        public double MDIExceed_T2_MIN
        {
            get { return GetLimitMin(ThreshouldItem.MDIExceed_T2); }
            set { SetLimitMin(ThreshouldItem.MDIExceed_T2, value); }
        }

        public double MDIExceed_T3_MIN
        {
            get { return GetLimitMin(ThreshouldItem.MDIExceed_T3); }
            set { SetLimitMin(ThreshouldItem.MDIExceed_T3, value); }
        }

        public double MDIExceed_T4_MIN
        {
            get { return GetLimitMin(ThreshouldItem.MDIExceed_T4); }
            set { SetLimitMin(ThreshouldItem.MDIExceed_T4, value); }
        }

        //Meter_ON_Load,
        public double MeterOnLoad_MIN
        {
            get { return GetLimitMin(ThreshouldItem.Meter_ON_Load); }
            set { SetLimitMin(ThreshouldItem.Meter_ON_Load, value); }
        }

        //OverCurrent_Phase,
        public double OverCurrent_Phase_MIN
        {
            get { return GetLimitMin(ThreshouldItem.OverCurrent_Phase); }
            set { SetLimitMin(ThreshouldItem.OverCurrent_Phase, value); }
        }

        //PowerFactor_Change,
        public double PowerFactor_Change_MIN
        {
            get { return GetLimitMin(ThreshouldItem.PowerFactor_Change); }
            set { SetLimitMin(ThreshouldItem.PowerFactor_Change, value); }
        }

        //CrestFactorLow,
        public double CrestFactorLow_MIN
        {
            get { return GetLimitMin(ThreshouldItem.CrestFactorLow); }
            set { SetLimitMin(ThreshouldItem.CrestFactorLow, value); }
        }

        //CrestFactorHigh
        public double CrestFactorHigh_MIN
        {
            get { return GetLimitMin(ThreshouldItem.CrestFactorHigh); }
            set { SetLimitMin(ThreshouldItem.CrestFactorHigh, value); }
        }

        public double OverLoad_1p_MIN
        {
            get { return GetLimitMin(ThreshouldItem.OverPower); }
            set { SetLimitMin(ThreshouldItem.OverPower, value); }
        }

        public double GeneratorStart_MIN
        {
            get { return GetLimitMin(ThreshouldItem.GeneratorStart); }
            set { SetLimitMin(ThreshouldItem.GeneratorStart, value); }
        }

        #endregion

        #region Limits_MAX

        public double ContactorFailure_max
        {
            get { return contactorFailure_max; }
            set { contactorFailure_max = value; }
        }

        public double OverVolt_MAX
        {
            get { return GetLimitMax(ThreshouldItem.OverVolt); }
            set { SetLimitMax(ThreshouldItem.OverVolt, value); }
        }

        public double UnderVolt_MAX
        {
            get { return GetLimitMax(ThreshouldItem.UnderVolt); }
            set { SetLimitMax(ThreshouldItem.UnderVolt, value); }
        }

        public double ImbalanceVolt_MAX
        {
            get { return GetLimitMax(ThreshouldItem.ImbalanceVolt); }
            set { SetLimitMax(ThreshouldItem.ImbalanceVolt, value); }
        }

        public double HighNeutralCurrent_MAX
        {
            get { return GetLimitMax(ThreshouldItem.HighNeutralCurrent); }
            set { SetLimitMax(ThreshouldItem.HighNeutralCurrent, value); }
        }

        public double ReverseEnergy_MAX
        {
            get { return GetLimitMax(ThreshouldItem.ReverseEnergy); }
            set { SetLimitMax(ThreshouldItem.ReverseEnergy, value); }
        }

        public double TamperEnergy_MAX
        {
            get { return GetLimitMax(ThreshouldItem.TamperEnergy); }
            set { SetLimitMax(ThreshouldItem.TamperEnergy, value); }
        }

        public double CTFail_MAX
        {
            get { return GetLimitMax(ThreshouldItem.CTFail); }
            set { SetLimitMax(ThreshouldItem.CTFail, value); }
        }

        public double PTFail_AMP_MAX
        {
            get { return GetLimitMax(ThreshouldItem.PTFail_Amp); }
            set { SetLimitMax(ThreshouldItem.PTFail_Amp, value); }
        }

        public double PTFail_Volt_MAX
        {
            get { return GetLimitMax(ThreshouldItem.PTFail_Volt); }
            set { SetLimitMax(ThreshouldItem.PTFail_Volt, value); }
        }

        public double OverCurrentByPhase_T1_MAX
        {
            get { return GetLimitMax(ThreshouldItem.OverCurrentByPhase_T1); }
            set { SetLimitMax(ThreshouldItem.OverCurrentByPhase_T1, value); }
        }

        public double OverCurrentByPhase_T2_MAX
        {
            get { return GetLimitMax(ThreshouldItem.OverCurrentByPhase_T2); }
            set { SetLimitMax(ThreshouldItem.OverCurrentByPhase_T2, value); }
        }

        public double OverCurrentByPhase_T3_MAX
        {
            get { return GetLimitMax(ThreshouldItem.OverCurrentByPhase_T3); }
            set { SetLimitMax(ThreshouldItem.OverCurrentByPhase_T3, value); }
        }

        public double OverCurrentByPhase_T4_MAX
        {
            get { return GetLimitMax(ThreshouldItem.OverCurrentByPhase_T4); }
            set { SetLimitMax(ThreshouldItem.OverCurrentByPhase_T4, value); }
        }

        public double OverLoadByPhase_T1_MAX
        {
            get { return GetLimitMax(ThreshouldItem.OverLoadByPhase_T1); }
            set { SetLimitMax(ThreshouldItem.OverLoadByPhase_T1, value); }
        }

        public double OverLoadByPhase_T2_MAX
        {
            get { return GetLimitMax(ThreshouldItem.OverLoadByPhase_T2); }
            set { SetLimitMax(ThreshouldItem.OverLoadByPhase_T2, value); }
        }

        public double OverLoadByPhase_T3_MAX
        {
            get { return GetLimitMax(ThreshouldItem.OverLoadByPhase_T3); }
            set { SetLimitMax(ThreshouldItem.OverLoadByPhase_T3, value); }
        }

        public double OverLoadByPhase_T4_MAX
        {
            get { return GetLimitMax(ThreshouldItem.OverLoadByPhase_T4); }
            set { SetLimitMax(ThreshouldItem.OverLoadByPhase_T4, value); }
        }

        public double OverLoadTotal_T1_MAX
        {
            get { return GetLimitMax(ThreshouldItem.OverLoadTotal_T1); }
            set { SetLimitMax(ThreshouldItem.OverLoadTotal_T1, value); }
        }

        public double OverLoadTotal_T2_MAX
        {
            get { return GetLimitMax(ThreshouldItem.OverLoadTotal_T2); }
            set { SetLimitMax(ThreshouldItem.OverLoadTotal_T2, value); }
        }

        public double OverLoadTotal_T3_MAX
        {
            get { return GetLimitMax(ThreshouldItem.OverLoadTotal_T3); }
            set { SetLimitMax(ThreshouldItem.OverLoadTotal_T3, value); }
        }

        public double OverLoadTotal_T4_MAX
        {
            get { return GetLimitMax(ThreshouldItem.OverLoadTotal_T4); }
            set { SetLimitMax(ThreshouldItem.OverLoadTotal_T4, value); }
        }

        public double MDIExceed_T1_MAX
        {
            get { return GetLimitMax(ThreshouldItem.MDIExceed_T1); }
            set { SetLimitMax(ThreshouldItem.MDIExceed_T1, value); }
        }

        public double MDIExceed_T2_MAX
        {
            get { return GetLimitMax(ThreshouldItem.MDIExceed_T2); }
            set { SetLimitMax(ThreshouldItem.MDIExceed_T2, value); }
        }

        public double MDIExceed_T3_MAX
        {
            get { return GetLimitMax(ThreshouldItem.MDIExceed_T3); }
            set { SetLimitMax(ThreshouldItem.MDIExceed_T3, value); }
        }

        public double MDIExceed_T4_MAX
        {
            get { return GetLimitMax(ThreshouldItem.MDIExceed_T4); }
            set { SetLimitMax(ThreshouldItem.MDIExceed_T4, value); }
        }

        //Meter_ON_Load,
        public double MeterOnLoad_MAX
        {
            get { return GetLimitMax(ThreshouldItem.Meter_ON_Load); }
            set { SetLimitMax(ThreshouldItem.Meter_ON_Load, value); }
        }

        //OverCurrent_Phase,
        public double OverCurrent_Phase_MAX
        {
            get { return GetLimitMax(ThreshouldItem.OverCurrent_Phase); }
            set { SetLimitMax(ThreshouldItem.OverCurrent_Phase, value); }
        }

        //PowerFactor_Change,
        public double PowerFactor_Change_MAX
        {
            get { return GetLimitMax(ThreshouldItem.PowerFactor_Change); }
            set { SetLimitMax(ThreshouldItem.PowerFactor_Change, value); }
        }

        //CrestFactorLow,
        public double CrestFactorLow_MAX
        {
            get { return GetLimitMax(ThreshouldItem.CrestFactorLow); }
            set { SetLimitMax(ThreshouldItem.CrestFactorLow, value); }
        }

        //CrestFactorHigh
        public double CrestFactorHigh_MAX
        {
            get { return GetLimitMax(ThreshouldItem.CrestFactorHigh); }
            set { SetLimitMax(ThreshouldItem.CrestFactorHigh, value); }
        }
        public double OverLoad_1p_MAX
        {
            get { return GetLimitMax(ThreshouldItem.OverPower); }
            set { SetLimitMax(ThreshouldItem.OverPower, value); }
        }

        public double GeneratorStart_MAX
        {
            get { return GetLimitMax(ThreshouldItem.GeneratorStart); }
            set { SetLimitMax(ThreshouldItem.GeneratorStart, value); }
        }

        #endregion

        #endregion

        #region Getter/Setter Functions

        public double GetLimitMin(ThreshouldItem Item)
        {
            double value = double.NaN;
            try
            {
                value = _valuesMin[(int)Item];
                if (value == double.NaN ||
               !(value >= double.MinValue && value <= double.MaxValue))
                    throw new ArgumentNullException(Item.ToString(),
                            String.Format("{0}_MIN Limit Value not init properly", Item));
                return value;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error to get {0}_Min Limit,{1}", Item, ex.Message), ex);
            }
        }

        public double GetLimitMax(ThreshouldItem Item)
        {
            double value = double.NaN;
            try
            {
                value = _valuesMax[(int)Item];
                if (value == double.NaN ||
                  !(value >= double.MinValue && value <= double.MaxValue))
                    throw new ArgumentNullException(Item.ToString(),
                            String.Format("{0}_MAX Limit Value not init properly", Item));
                return value;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error to get {0}_Max Limit,{1}", Item, ex.Message), ex);
            }
        }

        internal void SetLimitMin(ThreshouldItem Item, double value)
        {
            try
            {
                _valuesMin[(int)Item] = value;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error to set {0}_Min Limit,{1}", Item, ex.Message), ex);
            }
        }

        internal void SetLimitMax(ThreshouldItem Item, double value)
        {
            try
            {
                _valuesMax[(int)Item] = value;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error to set {0}_Max Limit,{1}", Item, ex.Message), ex);
            }
        }

        #endregion

        public LimitValues()
            : this(Default_CurrentMeter)
        {
        }

        public LimitValues(string CurrentmeterName)
        {
            int total_Count = Enum.GetValues(typeof(ThreshouldItem)).Length;
            ///Init _valuesMin,_valuesMax
            _valuesMax = new double[total_Count];
            _valuesMin = new double[total_Count];
            for (int index = 0; index < total_Count; index++)
            {
                _valuesMin[index] = _valuesMax[index] = double.NaN;
            }
            
            ///Init Limits Value Min/Max
            InitLimitsObj(this, CurrentmeterName);

        }

        public static void InitLimitsObj(LimitValues limitsObj, String CurrentmeterName)
        {
            limitsObj.CurrentMeter = CurrentmeterName;

            limitsObj.GeneratorStart_MIN = 0;
            limitsObj.GeneratorStart_MAX = 1000;

            #region Common ... Added for Single Phase
            //Over Current Neutral //HNC
            limitsObj.HighNeutralCurrent_MIN = 0.01;
            limitsObj.HighNeutralCurrent_MAX = 40;

            //Over Current Phase
            limitsObj.OverCurrent_Phase_MIN = 0.01;
            limitsObj.OverCurrent_Phase_MAX = 40;

            //Meter On Load
            limitsObj.MeterOnLoad_MIN = 0.03;
            limitsObj.MeterOnLoad_MAX = 1.0;

            //Power Factor change
            limitsObj.PowerFactor_Change_MIN = 0.01;
            limitsObj.PowerFactor_Change_MAX = 1.00;

            //Crest Factor Low
            limitsObj.CrestFactorLow_MIN = 1.20;
            limitsObj.CrestFactorLow_MAX = 1.41;

            //Crest Factor High
            limitsObj.CrestFactorHigh_MIN = 1.42;
            limitsObj.CrestFactorHigh_MAX = 1.60;

            //Unbalance LN Current //CT Fail Amp
            limitsObj.CTFail_MIN = 0.01;
            limitsObj.CTFail_MAX = 40.0;

            //Current Without Volts //PT Fail Volt
            limitsObj.PTFail_Volt_MIN = 0.10;
            limitsObj.PTFail_Volt_MAX = 40.0;

            limitsObj.OverLoad_1p_MIN = 0.10;
            limitsObj.OverLoad_1p_MAX = 10.0;
            #endregion

            switch (CurrentmeterName)
            {
                case "R283":
                    #region Over_Load_Total

                    limitsObj.OverLoadTotal_T1_MIN = 0.05;
                    limitsObj.OverLoadTotal_T1_MAX = 70;

                    limitsObj.OverLoadTotal_T2_MIN = 0.05;
                    limitsObj.OverLoadTotal_T2_MAX = 70;

                    limitsObj.OverLoadTotal_T3_MIN = 0.05;
                    limitsObj.OverLoadTotal_T3_MAX = 70;

                    limitsObj.OverLoadTotal_T4_MIN = 0.05;
                    limitsObj.OverLoadTotal_T4_MAX = 70;

                    #endregion
                    break;
                case "ACT34G":

                    #region CTFail

                    limitsObj.CTFail_MIN = 0.1;
                    limitsObj.CTFail_MAX = 100;

                    #endregion

                    #region High Neutral Current

                    limitsObj.HighNeutralCurrent_MIN = 0.1;
                    limitsObj.HighNeutralCurrent_MAX = 300;

                    #endregion

                    #region Imbalance Volt

                    limitsObj.ImbalanceVolt_MIN = 30;
                    limitsObj.ImbalanceVolt_MAX = 100;

                    #endregion

                    #region MDI_Exceed

                    limitsObj.MDIExceed_T1_MIN = 0.06;
                    limitsObj.MDIExceed_T1_MAX = 70;

                    limitsObj.MDIExceed_T2_MIN = 0.06;
                    limitsObj.MDIExceed_T2_MAX = 70;

                    limitsObj.MDIExceed_T3_MIN = 0.06;
                    limitsObj.MDIExceed_T3_MAX = 70;

                    limitsObj.MDIExceed_T4_MIN = 0.06;
                    limitsObj.MDIExceed_T4_MAX = 70;

                    #endregion

                    #region Over Volt

                    limitsObj.OverVolt_MIN = 230;
                    limitsObj.OverVolt_MAX = 300;

                    #endregion

                    #region Over Current By Phase

                    limitsObj.OverCurrentByPhase_T1_MIN = 1;
                    limitsObj.OverCurrentByPhase_T1_MAX = 100;

                    limitsObj.OverCurrentByPhase_T2_MIN = 1;
                    limitsObj.OverCurrentByPhase_T2_MAX = 100;

                    limitsObj.OverCurrentByPhase_T3_MIN = 1;
                    limitsObj.OverCurrentByPhase_T3_MAX = 100;

                    limitsObj.OverCurrentByPhase_T4_MIN = 1;
                    limitsObj.OverCurrentByPhase_T4_MAX = 100;

                    #endregion

                    #region Over Load by Phase

                    limitsObj.OverLoadByPhase_T1_MIN = 0.05;
                    limitsObj.OverLoadByPhase_T1_MAX = 23;

                    limitsObj.OverLoadByPhase_T2_MIN = 0.05;
                    limitsObj.OverLoadByPhase_T2_MAX = 23;

                    limitsObj.OverLoadByPhase_T3_MIN = 0.05;
                    limitsObj.OverLoadByPhase_T3_MAX = 23;

                    limitsObj.OverLoadByPhase_T4_MIN = 0.05;
                    limitsObj.OverLoadByPhase_T4_MAX = 23;

                    #endregion

                    #region Over Load Total

                    limitsObj.OverLoadTotal_T1_MIN = 0.05;
                    limitsObj.OverLoadTotal_T1_MAX = 70;

                    limitsObj.OverLoadTotal_T2_MIN = 0.05;
                    limitsObj.OverLoadTotal_T2_MAX = 70;

                    limitsObj.OverLoadTotal_T3_MIN = 0.05;
                    limitsObj.OverLoadTotal_T3_MAX = 70;

                    limitsObj.OverLoadTotal_T4_MIN = 0.05;
                    limitsObj.OverLoadTotal_T4_MAX = 70;

                    #endregion

                    #region PTFail_Amp

                    limitsObj.PTFail_AMP_MIN = 0.1;
                    limitsObj.PTFail_AMP_MAX = 100;

                    #endregion

                    #region PTFail_Volt

                    limitsObj.PTFail_Volt_MIN = 10;
                    limitsObj.PTFail_Volt_MAX = 200;

                    #endregion

                    #region Reverse Energy

                    limitsObj.ReverseEnergy_MIN = 0.1;
                    limitsObj.ReverseEnergy_MAX = 3.0;

                    #endregion

                    #region Tamper Energy

                    limitsObj.TamperEnergy_MIN = 0.1;
                    limitsObj.TamperEnergy_MAX = 50.0;

                    #endregion

                    #region Under Volt

                    limitsObj.UnderVolt_MIN = 150;
                    limitsObj.UnderVolt_MAX = 180;


                    #endregion

                    break;
                case "R326":

                    #region CTFail

                    limitsObj.CTFail_MIN = 0.1;
                    limitsObj.CTFail_MAX = 100;

                    #endregion

                    #region High Neutral Current

                    limitsObj.HighNeutralCurrent_MIN = 0.1;
                    limitsObj.HighNeutralCurrent_MAX = 300;

                    #endregion

                    #region Imbalance Volt

                    limitsObj.ImbalanceVolt_MIN = 30;
                    limitsObj.ImbalanceVolt_MAX = 100;

                    #endregion

                    #region MDI_Exceed

                    limitsObj.MDIExceed_T1_MIN = 0.06;
                    limitsObj.MDIExceed_T1_MAX = 70;

                    limitsObj.MDIExceed_T2_MIN = 0.06;
                    limitsObj.MDIExceed_T2_MAX = 70;

                    limitsObj.MDIExceed_T3_MIN = 0.06;
                    limitsObj.MDIExceed_T3_MAX = 70;

                    limitsObj.MDIExceed_T4_MIN = 0.06;
                    limitsObj.MDIExceed_T4_MAX = 70;

                    #endregion

                    #region Over Volt

                    limitsObj.OverVolt_MIN = 230;
                    limitsObj.OverVolt_MAX = 300;

                    #endregion

                    #region Over Current By Phase

                    limitsObj.OverCurrentByPhase_T1_MIN = 1;
                    limitsObj.OverCurrentByPhase_T1_MAX = 100;

                    limitsObj.OverCurrentByPhase_T2_MIN = 1;
                    limitsObj.OverCurrentByPhase_T2_MAX = 100;

                    limitsObj.OverCurrentByPhase_T3_MIN = 1;
                    limitsObj.OverCurrentByPhase_T3_MAX = 100;

                    limitsObj.OverCurrentByPhase_T4_MIN = 1;
                    limitsObj.OverCurrentByPhase_T4_MAX = 100;

                    #endregion

                    #region Over Load by Phase

                    limitsObj.OverLoadByPhase_T1_MIN = 0.05;
                    limitsObj.OverLoadByPhase_T1_MAX = 23;

                    limitsObj.OverLoadByPhase_T2_MIN = 0.05;
                    limitsObj.OverLoadByPhase_T2_MAX = 23;

                    limitsObj.OverLoadByPhase_T3_MIN = 0.05;
                    limitsObj.OverLoadByPhase_T3_MAX = 23;

                    limitsObj.OverLoadByPhase_T4_MIN = 0.05;
                    limitsObj.OverLoadByPhase_T4_MAX = 23;

                    #endregion

                    #region Over Load Total

                    limitsObj.OverLoadTotal_T1_MIN = 0.05;
                    limitsObj.OverLoadTotal_T1_MAX = 70;

                    limitsObj.OverLoadTotal_T2_MIN = 0.05;
                    limitsObj.OverLoadTotal_T2_MAX = 70;

                    limitsObj.OverLoadTotal_T3_MIN = 0.05;
                    limitsObj.OverLoadTotal_T3_MAX = 70;

                    limitsObj.OverLoadTotal_T4_MIN = 0.05;
                    limitsObj.OverLoadTotal_T4_MAX = 70;

                    #endregion

                    #region PTFail_Amp

                    limitsObj.PTFail_AMP_MIN = 0.1;
                    limitsObj.PTFail_AMP_MAX = 100;

                    #endregion

                    #region PTFail_Volt

                    limitsObj.PTFail_Volt_MIN = 10;
                    limitsObj.PTFail_Volt_MAX = 200;

                    #endregion

                    #region Reverse Energy

                    limitsObj.ReverseEnergy_MIN = 0.1;
                    limitsObj.ReverseEnergy_MAX = 3.0;

                    #endregion

                    #region Tamper Energy

                    limitsObj.TamperEnergy_MIN = 0.1;
                    limitsObj.TamperEnergy_MAX = 50.0;

                    #endregion

                    #region Under Volt

                    limitsObj.UnderVolt_MIN = 150;
                    limitsObj.UnderVolt_MAX = 180;


                    #endregion

                    break;

                //case "T421":
                case "R411":

                    #region CTFail

                    limitsObj.CTFail_MIN = 0.1;
                    limitsObj.CTFail_MAX = 10;

                    #endregion

                    #region High Neutral Current

                    limitsObj.HighNeutralCurrent_MIN = 0.1;
                    limitsObj.HighNeutralCurrent_MAX = 15;

                    #endregion

                    #region Imbalance Volt

                    limitsObj.ImbalanceVolt_MIN = 5;
                    limitsObj.ImbalanceVolt_MAX = 60;

                    #endregion

                    #region MDI Exceed

                    limitsObj.MDIExceed_T1_MIN = 0.005;
                    limitsObj.MDIExceed_T1_MAX = 10;

                    limitsObj.MDIExceed_T2_MIN = 0.005;
                    limitsObj.MDIExceed_T2_MAX = 10;

                    limitsObj.MDIExceed_T3_MIN = 0.005;
                    limitsObj.MDIExceed_T3_MAX = 10;

                    limitsObj.MDIExceed_T4_MIN = 0.005;
                    limitsObj.MDIExceed_T4_MAX = 10;

                    #endregion

                    #region Over Volt

                    limitsObj.OverVolt_MIN = 63;
                    limitsObj.OverVolt_MAX = 75;

                    #endregion

                    #region Over Current By Phase

                    limitsObj.OverCurrentByPhase_T1_MIN = 0.1;
                    limitsObj.OverCurrentByPhase_T1_MAX = 10;

                    limitsObj.OverCurrentByPhase_T2_MIN = 0.1;
                    limitsObj.OverCurrentByPhase_T2_MAX = 10;

                    limitsObj.OverCurrentByPhase_T3_MIN = 0.1;
                    limitsObj.OverCurrentByPhase_T3_MAX = 10;

                    limitsObj.OverCurrentByPhase_T4_MIN = 0.1;
                    limitsObj.OverCurrentByPhase_T4_MAX = 10;

                    #endregion

                    #region Over Load by Phase

                    limitsObj.OverLoadByPhase_T1_MIN = 0.005;
                    limitsObj.OverLoadByPhase_T1_MAX = 3.0;

                    limitsObj.OverLoadByPhase_T2_MIN = 0.005;
                    limitsObj.OverLoadByPhase_T2_MAX = 3.0;

                    limitsObj.OverLoadByPhase_T3_MIN = 0.005;
                    limitsObj.OverLoadByPhase_T3_MAX = 3.0;

                    limitsObj.OverLoadByPhase_T4_MIN = 0.005;
                    limitsObj.OverLoadByPhase_T4_MAX = 3.0;

                    #endregion

                    #region Over Load Total

                    limitsObj.OverLoadTotal_T1_MIN = 0.005;
                    limitsObj.OverLoadTotal_T1_MAX = 7.0;

                    limitsObj.OverLoadTotal_T2_MIN = 0.005;
                    limitsObj.OverLoadTotal_T2_MAX = 7.0;

                    limitsObj.OverLoadTotal_T3_MIN = 0.005;
                    limitsObj.OverLoadTotal_T3_MAX = 7.0;

                    limitsObj.OverLoadTotal_T4_MIN = 0.005;
                    limitsObj.OverLoadTotal_T4_MAX = 7.0;

                    #endregion

                    #region PTFail_Amp

                    limitsObj.PTFail_AMP_MIN = 0.1;
                    limitsObj.PTFail_AMP_MAX = 10;

                    #endregion

                    #region PTFail_Volt

                    limitsObj.PTFail_Volt_MIN = 10;
                    limitsObj.PTFail_Volt_MAX = 50;

                    #endregion

                    #region Reverse Energy

                    limitsObj.ReverseEnergy_MIN = 0.1;
                    limitsObj.ReverseEnergy_MAX = 7.0;

                    #endregion

                    #region Tamper Energy

                    limitsObj.TamperEnergy_MIN = 0.1;
                    limitsObj.TamperEnergy_MAX = 7.0;

                    #endregion

                    #region Under Volt

                    limitsObj.UnderVolt_MIN = 50;
                    limitsObj.UnderVolt_MAX = 60;

                    #endregion

                    break;

                case "T421": //v4.8.41
                case "R421":

                    #region CTFail

                    limitsObj.CTFail_MIN = 0.1;
                    limitsObj.CTFail_MAX = 10.0;

                    #endregion

                    #region High Neutral Current

                    limitsObj.HighNeutralCurrent_MIN = 0.1;
                    limitsObj.HighNeutralCurrent_MAX = 30.0;

                    #endregion

                    #region Imbalance Volt

                    limitsObj.ImbalanceVolt_MIN = 30;
                    limitsObj.ImbalanceVolt_MAX = 100;

                    #endregion

                    #region MDI Exceed

                    limitsObj.MDIExceed_T1_MIN = 0.005;
                    limitsObj.MDIExceed_T1_MAX = 10;

                    limitsObj.MDIExceed_T2_MIN = 0.005;
                    limitsObj.MDIExceed_T2_MAX = 10;

                    limitsObj.MDIExceed_T3_MIN = 0.005;
                    limitsObj.MDIExceed_T3_MAX = 10;

                    limitsObj.MDIExceed_T4_MIN = 0.005;
                    limitsObj.MDIExceed_T4_MAX = 10;

                    #endregion

                    #region Over Volt

                    limitsObj.OverVolt_MIN = 230;
                    limitsObj.OverVolt_MAX = 300;

                    #endregion

                    #region Over Current By Phase

                    limitsObj.OverCurrentByPhase_T1_MIN = 0.1;
                    limitsObj.OverCurrentByPhase_T1_MAX = 10;

                    limitsObj.OverCurrentByPhase_T2_MIN = 0.1;
                    limitsObj.OverCurrentByPhase_T2_MAX = 10;

                    limitsObj.OverCurrentByPhase_T3_MIN = 0.1;
                    limitsObj.OverCurrentByPhase_T3_MAX = 10;

                    limitsObj.OverCurrentByPhase_T4_MIN = 0.1;
                    limitsObj.OverCurrentByPhase_T4_MAX = 10;

                    #endregion

                    #region Over Load by Phase

                    limitsObj.OverLoadByPhase_T1_MIN = 0.005;
                    limitsObj.OverLoadByPhase_T1_MAX = 3.0;

                    limitsObj.OverLoadByPhase_T2_MIN = 0.005;
                    limitsObj.OverLoadByPhase_T2_MAX = 3.0;

                    limitsObj.OverLoadByPhase_T3_MIN = 0.005;
                    limitsObj.OverLoadByPhase_T3_MAX = 3.0;

                    limitsObj.OverLoadByPhase_T4_MIN = 0.005;
                    limitsObj.OverLoadByPhase_T4_MAX = 3.0;

                    #endregion

                    #region Over Load Total

                    limitsObj.OverLoadTotal_T1_MIN = 0.005;
                    limitsObj.OverLoadTotal_T1_MAX = 7;

                    limitsObj.OverLoadTotal_T2_MIN = 0.005;
                    limitsObj.OverLoadTotal_T2_MAX = 7;

                    limitsObj.OverLoadTotal_T3_MIN = 0.005;
                    limitsObj.OverLoadTotal_T3_MAX = 7;

                    limitsObj.OverLoadTotal_T4_MIN = 0.005;
                    limitsObj.OverLoadTotal_T4_MAX = 7;

                    #endregion

                    #region PTFail_Amp

                    limitsObj.PTFail_AMP_MIN = 0.1;
                    limitsObj.PTFail_AMP_MAX = 10.0;

                    #endregion

                    #region PTFail_Volt

                    limitsObj.PTFail_Volt_MIN = 10;
                    limitsObj.PTFail_Volt_MAX = 200;

                    #endregion

                    #region Reverse Energy

                    limitsObj.ReverseEnergy_MIN = 0.1;
                    limitsObj.ReverseEnergy_MAX = 7.0;

                    #endregion

                    #region Tamper Energy

                    limitsObj.TamperEnergy_MIN = 0.1;
                    limitsObj.TamperEnergy_MAX = 7.0;

                    #endregion

                    #region Under Volt

                    limitsObj.UnderVolt_MIN = 150;
                    limitsObj.UnderVolt_MAX = 180;


                    #endregion

                    break;
            }
        }
    }
}



