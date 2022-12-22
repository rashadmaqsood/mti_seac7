using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Comm;

namespace AccurateOptocomSoftware.comm
{
    public class MonitoringTimeValues
    {
        #region Data Members

        public const string Default_CurrentMeter = "R326";//"ACT34G";
        public static readonly TimeSpan Default_MinValue = TimeSpan.FromSeconds(0.0);
        public static readonly TimeSpan Default_MaxValue = (TimeSpan.FromHours(1.0) - TimeSpan.FromSeconds(1.0));///To 59:59
        
        public static readonly DateTime Default_DateTime = new DateTime(2014, 08, 25, 00, 00, 00, 00, DateTimeKind.Local);///Set Default DateTime 
        public static readonly TimeZoneInfo PST_Zone = TimeZoneInfo.FindSystemTimeZoneById("West Asia Standard Time");
        //TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");

        private string currentMeter;

        ///From 00:00 or 
        ///as defined per MonitoringTime MinValue
        private TimeSpan[] _valuesMin = null;
        ///To 59:59 or 
        ///as defined per MonitoringTime MaxValue
        private TimeSpan[] _valuesMax = null;

        #endregion

        #region Properties

        public string CurrentMeter
        {
            get { return currentMeter; }
            set { currentMeter = value; }
        }

        #region MonitorTime_MIN

        public TimeSpan OverVolt_MIN
        {
            get { return GetMonitorTime_Min(MonitoringTimeItem.OverVolt); }
            set { SetMonitorTime_Min(MonitoringTimeItem.OverVolt, value); }
        }

        public TimeSpan UnderVolt_MIN
        {
            get { return GetMonitorTime_Min(MonitoringTimeItem.UnderVolt); }
            set { SetMonitorTime_Min(MonitoringTimeItem.UnderVolt, value); }
        }

        public TimeSpan ImbalanceVolt_MIN
        {
            get { return GetMonitorTime_Min(MonitoringTimeItem.ImbalanceVolt); }
            set { SetMonitorTime_Min(MonitoringTimeItem.ImbalanceVolt, value); }
        }

        public TimeSpan HighNeutralCurrent_MIN
        {
            get { return GetMonitorTime_Min(MonitoringTimeItem.HighNeutralCurrent); }
            set { SetMonitorTime_Min(MonitoringTimeItem.HighNeutralCurrent, value); }
        }

        public TimeSpan ReverseEnergy_MIN
        {
            get { return GetMonitorTime_Min(MonitoringTimeItem.ReverseEnergy); }
            set { SetMonitorTime_Min(MonitoringTimeItem.ReverseEnergy, value); }
        }

        public TimeSpan TamperEnergy_MIN
        {
            get { return GetMonitorTime_Min(MonitoringTimeItem.TamperEnergy); }
            set { SetMonitorTime_Min(MonitoringTimeItem.TamperEnergy, value); }
        }

        public TimeSpan ReversePolarity_MIN
        {
            get { return GetMonitorTime_Min(MonitoringTimeItem.ReversePolarity); }
            set { SetMonitorTime_Min(MonitoringTimeItem.ReversePolarity, value); }
        }

        public TimeSpan CTFail_MIN
        {
            get { return GetMonitorTime_Min(MonitoringTimeItem.CTFail); }
            set { SetMonitorTime_Min(MonitoringTimeItem.CTFail, value); }
        }

        public TimeSpan PTFail_MIN
        {
            get { return GetMonitorTime_Min(MonitoringTimeItem.PTFail); }
            set { SetMonitorTime_Min(MonitoringTimeItem.PTFail, value); }
        }

        public TimeSpan OverCurrent_MIN
        {
            get { return GetMonitorTime_Min(MonitoringTimeItem.OverCurrent); }
            set { SetMonitorTime_Min(MonitoringTimeItem.OverCurrent, value); }
        }

        public TimeSpan OverLoad_MIN
        {
            get { return GetMonitorTime_Min(MonitoringTimeItem.OverLoad); }
            set { SetMonitorTime_Min(MonitoringTimeItem.OverLoad, value); }
        }

        public TimeSpan PowerFail_MIN
        {
            get { return GetMonitorTime_Min(MonitoringTimeItem.PowerFail); }
            set { SetMonitorTime_Min(MonitoringTimeItem.PowerFail, value); }
        }

        public TimeSpan PhaseFail_MIN
        {
            get { return GetMonitorTime_Min(MonitoringTimeItem.PhaseFail); }
            set { SetMonitorTime_Min(MonitoringTimeItem.PhaseFail, value); }
        }

        public TimeSpan PhaseSequence_MIN
        {
            get { return GetMonitorTime_Min(MonitoringTimeItem.PhaseSequence); }
            set { SetMonitorTime_Min(MonitoringTimeItem.PhaseSequence, value); }
        }

        public TimeSpan PowerUPDelay_MIN
        {
            get { return GetMonitorTime_Min(MonitoringTimeItem.PowerUPDelay); }
            set { SetMonitorTime_Min(MonitoringTimeItem.PowerUPDelay, value); }
        }

        public TimeSpan PowerUpDelayEnergyRecording_MIN
        {
            get { return GetMonitorTime_Min(MonitoringTimeItem.PowerUpDelayEnergyRecording); }
            set { SetMonitorTime_Min(MonitoringTimeItem.PowerUpDelayEnergyRecording, value); }
        }

        public TimeSpan PowerUpDelayEarth_MIN
        {
            get { return GetMonitorTime_Min(MonitoringTimeItem.PowerUpDelayEarth); }
            set { SetMonitorTime_Min(MonitoringTimeItem.PowerUpDelayEarth, value); }
        }

        public TimeSpan Earth_MIN
        {
            get { return GetMonitorTime_Min(MonitoringTimeItem.Earth); }
            set { SetMonitorTime_Min(MonitoringTimeItem.Earth, value); }
        }

        public TimeSpan HALLSensor_MIN
        {
            get { return GetMonitorTime_Min(MonitoringTimeItem.HALLSensor); }
            set { SetMonitorTime_Min(MonitoringTimeItem.HALLSensor, value); }
        }

        #endregion

        #region MonitorTime_MAX

        public TimeSpan OverVolt_MAX
        {
            get { return GetMonitorTime_Max(MonitoringTimeItem.OverVolt); }
            set { SetMonitorTime_Max(MonitoringTimeItem.OverVolt, value); }
        }

        public TimeSpan UnderVolt_MAX
        {
            get { return GetMonitorTime_Max(MonitoringTimeItem.UnderVolt); }
            set { SetMonitorTime_Max(MonitoringTimeItem.UnderVolt, value); }
        }

        public TimeSpan ImbalanceVolt_MAX
        {
            get { return GetMonitorTime_Max(MonitoringTimeItem.ImbalanceVolt); }
            set { SetMonitorTime_Max(MonitoringTimeItem.ImbalanceVolt, value); }
        }

        public TimeSpan HighNeutralCurrent_MAX
        {
            get { return GetMonitorTime_Max(MonitoringTimeItem.HighNeutralCurrent); }
            set { SetMonitorTime_Max(MonitoringTimeItem.HighNeutralCurrent, value); }
        }

        public TimeSpan ReverseEnergy_MAX
        {
            get { return GetMonitorTime_Max(MonitoringTimeItem.ReverseEnergy); }
            set { SetMonitorTime_Max(MonitoringTimeItem.ReverseEnergy, value); }
        }

        public TimeSpan TamperEnergy_MAX
        {
            get { return GetMonitorTime_Max(MonitoringTimeItem.TamperEnergy); }
            set { SetMonitorTime_Max(MonitoringTimeItem.TamperEnergy, value); }
        }

        public TimeSpan ReversePolarity_MAX
        {
            get { return GetMonitorTime_Max(MonitoringTimeItem.ReversePolarity); }
            set { SetMonitorTime_Max(MonitoringTimeItem.ReversePolarity, value); }
        }

        public TimeSpan CTFail_MAX
        {
            get { return GetMonitorTime_Max(MonitoringTimeItem.CTFail); }
            set { SetMonitorTime_Max(MonitoringTimeItem.CTFail, value); }
        }

        public TimeSpan PTFail_MAX
        {
            get { return GetMonitorTime_Max(MonitoringTimeItem.PTFail); }
            set { SetMonitorTime_Max(MonitoringTimeItem.PTFail, value); }
        }

        public TimeSpan OverCurrent_MAX
        {
            get { return GetMonitorTime_Max(MonitoringTimeItem.OverCurrent); }
            set { SetMonitorTime_Max(MonitoringTimeItem.OverCurrent, value); }
        }

        public TimeSpan OverLoad_MAX
        {
            get { return GetMonitorTime_Max(MonitoringTimeItem.OverLoad); }
            set { SetMonitorTime_Max(MonitoringTimeItem.OverLoad, value); }
        }

        public TimeSpan PowerFail_MAX
        {
            get { return GetMonitorTime_Max(MonitoringTimeItem.PowerFail); }
            set { SetMonitorTime_Max(MonitoringTimeItem.PowerFail, value); }
        }

        public TimeSpan PhaseFail_MAX
        {
            get { return GetMonitorTime_Max(MonitoringTimeItem.PhaseFail); }
            set { SetMonitorTime_Max(MonitoringTimeItem.PhaseFail, value); }
        }

        public TimeSpan PhaseSequence_MAX
        {
            get { return GetMonitorTime_Max(MonitoringTimeItem.PhaseSequence); }
            set { SetMonitorTime_Max(MonitoringTimeItem.PhaseSequence, value); }
        }

        public TimeSpan PowerUPDelay_MAX
        {
            get { return GetMonitorTime_Max(MonitoringTimeItem.PowerUPDelay); }
            set { SetMonitorTime_Max(MonitoringTimeItem.PowerUPDelay, value); }
        }

        public TimeSpan PowerUpDelayEnergyRecording_MAX
        {
            get { return GetMonitorTime_Max(MonitoringTimeItem.PowerUpDelayEnergyRecording); }
            set { SetMonitorTime_Max(MonitoringTimeItem.PowerUpDelayEnergyRecording, value); }
        }

        public TimeSpan PowerUpDelayEarth_MAX
        {
            get { return GetMonitorTime_Max(MonitoringTimeItem.PowerUpDelayEarth); }
            set { SetMonitorTime_Max(MonitoringTimeItem.PowerUpDelayEarth, value); }
        }

        public TimeSpan Earth_MAX
        {
            get { return GetMonitorTime_Max(MonitoringTimeItem.Earth); }
            set { SetMonitorTime_Max(MonitoringTimeItem.Earth, value); }
        }

        public TimeSpan HALLSensor_MAX
        {
            get { return GetMonitorTime_Max(MonitoringTimeItem.HALLSensor); }
            set { SetMonitorTime_Max(MonitoringTimeItem.HALLSensor, value); }
        }

        #endregion

        #endregion

        #region Getter/Setter Functions

        internal TimeSpan GetMonitorTime_Min(MonitoringTimeItem Item)
        {
            TimeSpan value = Default_MinValue;
            try
            {
                value = _valuesMin[(int)Item];
                if (!(value >= Default_MinValue && value <= Default_MaxValue))
                    throw new ArgumentNullException(Item.ToString(),
                            String.Format("{0}_MIN MonitoringTime Value not init properly", Item));
                return value;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error to get {0}_Min MonitoringTime,{1}", Item, ex.Message), ex);
            }
        }

        internal TimeSpan GetMonitorTime_Max(MonitoringTimeItem Item)
        {
            TimeSpan value = Default_MinValue;
            try
            {
                value = _valuesMax[(int)Item];
                if (!(value >= Default_MinValue && value <= Default_MaxValue))
                    throw new ArgumentNullException(Item.ToString(),
                            String.Format("{0}_Max MonitoringTime Value not init properly", Item));
                return value;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error to get {0}_Max MonitoringTime,{1}", Item, ex.Message), ex);
            }
        }

        internal void SetMonitorTime_Min(MonitoringTimeItem Item, TimeSpan value)
        {
            try
            {
                _valuesMin[(int)Item] = value;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error to set {0}_Min MonitoringTime,{1}", Item, ex.Message), ex);
            }
        }

        internal void SetMonitorTime_Max(MonitoringTimeItem Item, TimeSpan value)
        {
            try
            {
                _valuesMax[(int)Item] = value;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error to set {0}_Max MonitoringTime,{1}", Item, ex.Message), ex);
            }
        }

        #endregion

        public MonitoringTimeValues()
            : this(Default_CurrentMeter)
        {
        }

        public MonitoringTimeValues(string CurrentmeterName)
        {
            int total_Count = Enum.GetValues(typeof(ThreshouldItem)).Length;
            ///Init _valuesMin,_valuesMax
            _valuesMax = new TimeSpan[total_Count];
            _valuesMin = new TimeSpan[total_Count];
            for (int index = 0; index < total_Count; index++)
            {
                _valuesMin[index] = Default_MinValue;
                _valuesMax[index] = Default_MaxValue;
            }
            ///Init Limits Value Min/Max
            InitMonitoringTimeObj(this, CurrentmeterName);
        }

        public static void InitMonitoringTimeObj(MonitoringTimeValues MonitorTimeObj, String CurrentmeterName)
        {
            MonitorTimeObj.CurrentMeter = CurrentmeterName;
            switch (CurrentmeterName)
            {
                case "R283":
                case "ACT34G":
                case "R326":
                case "T421":
                case "R411":
                case "R421":
                    MonitorTimeObj.PowerUPDelay_MIN = Default_MinValue;
                    MonitorTimeObj.PowerUPDelay_MAX = TimeSpan.FromSeconds(10.0);

                    MonitorTimeObj.PowerUpDelayEnergyRecording_MIN = Default_MinValue;
                    MonitorTimeObj.PowerUpDelayEnergyRecording_MAX = TimeSpan.FromSeconds(10.0);
                    break;
            }
        }
    }
}
