using SharedCode.Comm.Param;
using System;
using System.Collections.Generic;
using System.Linq;
namespace SharedCode.Comm.DataContainer
{
    public class ParametersSet : IDisposable
    {

        #region Data Members
        public Param_Monitoring_Time ParamMonitoringTime;
        public Param_Limits ParamLimits;
        public Param_ActivityCalendar ParamTariffication;
        public Param_MDI_parameters ParamMDI;
        public List<LoadProfileChannelInfo> ParamLoadProfileChannelInfo;
        public Param_CTPT_Ratio ParamCTPTRatio;
        public Param_Decimal_Point ParamDecimalPoint;
        public Param_Energy_Parameter ParamEnergy;
        public Param_Clock_Caliberation ParamClockCalib;
        public Param_ContactorExt ParamContactor;
        public Param_TimeBaseEvents ParamTimeBaseEvent_1;
        public Param_TimeBaseEvents ParamTimeBaseEvent_2;
        public TBE_PowerFail ParamTBPowerFail;
        public Param_MajorAlarmProfile ParamMajorAlarmProfile;
        public Param_IP_Profiles[] ParamIPProfiles;
        public Param_Standard_IP_Profile[] ParamStandardIPProfiles;

        public Param_WakeUp_Profile[] ParamWakeUpProfile;
        public Param_Standard_Number_Profile[] ParamStandardNumberProfile;
        public Param_Number_Profile[] ParamNumberProfile;

        public Param_Keep_Alive_IP ParamKeepAliveIP;
        public Param_ModemLimitsAndTime ParamModemLimitsAndTime;
        public Param_Modem_Initialize ParamModemInitialize;
        public Param_ModemBasics_NEW ParamModemBasicsNEW;
        public Param_Display_PowerDown ParamDisplayPowerDown;
        public Param_Generel_Process ParamGeneralProcess;
        public Param_ModemStatus ParamModemInfo;
        public string Debug_Error;
        public string Debug_Cautions;
        public string Debug_Contactor_Status;
        #endregion //Data Members

        #region Constructor
        public ParametersSet()
        {
            InitAllProperties();
        }
        #endregion //Constructor

        #region Properties

        //public Param_Monitoring_Time ParamMonitoringTime { get; set; }

        //public Param_Limits ParamLimits { get; set; }

        //public Param_ActivityCalendar ParamTariffication { get; set; }

        //public Param_MDI_parameters ParamMDI { get; set; }

        //public List<LoadProfileChannelInfo> ParamLoadProfileChannelInfo { get; set; }

        //public Param_CTPT_Ratio ParamCTPTRatio { get; set; }

        //public Param_Decimal_Point ParamDecimalPoint { get; set; }

        //public Param_Energy_Parameter ParamEnergy { get; set; }

        //public Param_Clock_Caliberation ParamClockCalib { get; set; }

        //public Param_Contactor ParamContactor { get; set; }

        //public Param_TimeBaseEvents ParamTimeBaseEvent_1 { get; set; }

        //public Param_TimeBaseEvents ParamTimeBaseEvent_2 { get; set; }

        //public TBE_PowerFail ParamTBPowerFail { get; set; }

        //public Param_MajorAlarmProfile ParamMajorAlarmProfile { get; set; }

        //public Param_IP_Profiles[] ParamIPProfiles { get; set; }

        //public Param_WakeUp_Profile[] ParamWakeUpProfile { get; set; }

        //public Param_Number_Profile[] ParamNumberProfile { get; set; }

        //public Param_Keep_Alive_IP ParamKeepAliveIP { get; set; }

        //public Param_ModemLimitsAndTime ParamModemLimitsAndTime { get; set; }

        //public Param_Modem_Initialize ParamModemInitialize { get; set; }

        //public Param_ModemBasics_NEW ParamModemBasicsNEW { get; set; }

        //public Param_Display_PowerDown ParamDisplayPowerDown { get; set; }

        //public Param_Generel_Process ParamGeneralProcess { get; set; }

        #endregion //Properties

        #region Methods

        private T[] InitProfiles<T>(T[] objArray, int count)
        {
            var type = typeof(T);
            objArray = new T[count];
            for (int i = 0; i < count; i++)
            {
                objArray[i] = (T)Activator.CreateInstance(type);
            }
            return objArray;
        }

        public void InitAllProperties()
        {
            ParamMonitoringTime = new Param_Monitoring_Time();
            ParamLimits = new Param_Limits();
            ParamTariffication = new Param_ActivityCalendar();
            ParamMDI = new Param_MDI_parameters();
            ParamLoadProfileChannelInfo = new List<LoadProfileChannelInfo>();
            ParamCTPTRatio = new Param_CTPT_Ratio();
            ParamDecimalPoint = new Param_Decimal_Point();
            ParamEnergy = new Param_Energy_Parameter();
            ParamClockCalib = new Param_Clock_Caliberation();
            ParamContactor = new Param_ContactorExt();
            ParamTimeBaseEvent_1 = new Param_TimeBaseEvents();
            ParamTimeBaseEvent_2 = new Param_TimeBaseEvents();
            ParamTBPowerFail = new TBE_PowerFail();
            ParamMajorAlarmProfile = new Param_MajorAlarmProfile();
            ParamKeepAliveIP = new Param_Keep_Alive_IP();
            ParamModemLimitsAndTime = new Param_ModemLimitsAndTime();
            ParamModemInitialize = new Param_Modem_Initialize();
            ParamModemBasicsNEW = new Param_ModemBasics_NEW();
            ParamDisplayPowerDown = new Param_Display_PowerDown();
            ParamGeneralProcess = new Param_Generel_Process();
            ParamModemInfo = new Param_ModemStatus();

            ParamIPProfiles = InitProfiles<Param_IP_Profiles>(ParamIPProfiles, 4);
            ParamStandardIPProfiles = InitProfiles<Param_Standard_IP_Profile>(ParamStandardIPProfiles, 4);
            ParamWakeUpProfile = InitProfiles<Param_WakeUp_Profile>(ParamWakeUpProfile, 4);
            ParamNumberProfile = InitProfiles<Param_Number_Profile>(ParamNumberProfile, 5);
            ParamStandardNumberProfile = InitProfiles<Param_Standard_Number_Profile>(ParamStandardNumberProfile, 5);

            Debug_Error = Debug_Cautions = Debug_Contactor_Status = "00";
        }
        #endregion //Methods

        #region IDisposeable

        public void Dispose()
        {
            ParamMonitoringTime = null;
            ParamLimits = null;
            ParamTariffication = null;
            ParamMDI = null;
            ParamLoadProfileChannelInfo = null;
            ParamCTPTRatio = null;
            ParamDecimalPoint = null;
            ParamEnergy = null;
            ParamClockCalib = null;
            ParamContactor = null;
            ParamTimeBaseEvent_1 = null;
            ParamTimeBaseEvent_2 = null;
            ParamTBPowerFail = null;
            ParamMajorAlarmProfile = null;
            ParamIPProfiles = null;
            ParamStandardIPProfiles = null;
            ParamWakeUpProfile = null;
            ParamNumberProfile = null;
            ParamStandardNumberProfile = null;
            ParamKeepAliveIP = null;
            ParamModemLimitsAndTime = null;
            ParamModemInitialize = null;
            ParamModemBasicsNEW = null;
            ParamDisplayPowerDown = null;
            ParamGeneralProcess = null;
            GC.Collect();
        }
        #endregion
    }
}
