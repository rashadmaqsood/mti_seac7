
using DLMS;
using DLMS.Comm;
using GUI;
using OptocomSoftware.Reporting;
using SEAC.Common;
using SharedCode.Comm.DataContainer;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.Param;
using SharedCode.Common;
using SharedCode.Controllers;
using SharedCode.Others;
using SmartEyeControl_7.ApplicationGUI.GUI;
using SmartEyeControl_7.ApplicationGUI.ucCustomControl;
using SmartEyeControl_7.comm;
using SmartEyeControl_7.DB;
using SmartEyeControl_7.Reporting;
using SmartEyeControl_7.Reporting.WapdaFormat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace ucCustomControl
{
    public partial class pnlParameterization : UserControl
    {
        #region Data_Members
        bool HidePrintReportButtons;
        const int KeySize = 16;
        private int interval = 0;
        private int TimeToAdd = 0;
        private int currentRow = 0;
        private int Debug_Counter_Read_Log = 0;

        private TimeSpan JumpTime = new TimeSpan(0, 10, 0);
        private CustomSetGet CustGetDialog = null;
        private CustomSetGet setter = null;
        private ProgressDialog dlg;
        private bool GetCompleted = false;
        private bool SabSet = true;
        private string KharabParams = "";
        private DBConnect myDB = new DBConnect();
        Param_Customer_Code obj_CustomerCode = null;

        ds_Param Dataset_Param = new ds_Param();
        dataset_Calendar dataset_calendar = new dataset_Calendar();
        private Instantaneous_Class Instantaneous_Class_obj = new Instantaneous_Class();
        public const string Single_Phase_Model = "R283";

        /// This region declares all the paramerterization classes before hand for usage
        ///**************************************************************************** for SETTING PARAMETERS
        private DLMS_Application_Process Application_Process;
        private ConnectionManager connectionManager;
        private ConnectionController ConnController;
        private ApplicationProcess_Controller AP_Controller;
        private ApplicationController application_Controller;
        private ParameterController Param_Controller;
        private BillingController Instantanous_Controller;
        private LoadProfileController LoadProfile_Controller;
        private ProgressDialog progressDialog = new ProgressDialog();
        ///****************************************************************************
        private ParamConfigurationSet loadedConfigurationSet = null;
        private ParamConfigurationSet _paramConfigurationSet = null;

        DateTimeRequestType RTC_RequestType = DateTimeRequestType.SetTime;
        string errorMessage = string.Empty;
        Clock_Synchronization_Method Clock_Synch_Method = Clock_Synchronization_Method.NO_SYNCHRONIZATION;
        Action_Result MethodInvokeResult;
        Data_Access_Result dataAccessResult;
        private Param_PresetTime Param_PresetTime_object = new Param_PresetTime();
        private Param_Standard_IP_Profile[] Param_Standard_IP_Profiles_object = new Param_Standard_IP_Profile[4];
        private Param_Standard_Number_Profile[] Param_Standard_Number_Profile_object = new Param_Standard_Number_Profile[4];
        private AutoConnectMode Auto_Connect_Mode = AutoConnectMode.PermanentConnectionAlways;

        internal ParamConfigurationSet ParamConfigurationSetObj
        {
            get
            {
                if (application_Controller == null ||
                   application_Controller.ParameterConfigurationSet == null)
                    _paramConfigurationSet = null;
                else if (_paramConfigurationSet == null ||
                        _paramConfigurationSet != application_Controller.ParameterConfigurationSet)
                    _paramConfigurationSet = application_Controller.ParameterConfigurationSet;
                return _paramConfigurationSet;
                //return new ParamConfigurationSet();
            }
        }

        #region Instantiate_Param_Classes

        //private Param_Clock_Caliberation Param_meterClock = new Param_Clock_Caliberation();
        //private Param_Clock_Caliberation Param_clock_caliberation_object = new Param_Clock_Caliberation();

        //private Param_Contactor Param_Contactor_object = new Param_Contactor();
        //private Param_CTPT_Ratio Param_CTPT_ratio_object = new Param_CTPT_Ratio();
        //private Param_Customer_Code Param_customer_code_object = new Param_Customer_Code();
        //private Param_Decimal_Point Param_decimal_point_object = new Param_Decimal_Point();
        //private Param_Display_windows[] Param_Display_windows_object = new Param_Display_windows[2];
        //private Param_Energy_Parameter Param_energy_parameters_object = new Param_Energy_Parameter();

        ///private Param_IP_Profiles[] Param_IP_Profiles_object = null;
        private Param_IP_ProfilesHelper Param_IP_ProfilesHelperObj = null;
        private Param_Number_ProfileHelper Param_Number_ProfileHelperObj = null;
        ///private Param_Number_Profile[] Param_Number_Profile_object = null;
        private Param_WakeUp_ProfileHelper Param_WakeUp_ProfileHelperObj = null;
        ///private Param_WakeUp_Profile[] Param_Wakeup_Profile_object = null;

        internal Param_IPV4 Param_IPV4_object = new Param_IPV4();

        //private Param_Communication_Profile Param_Communication_Profile_object = new Param_Communication_Profile();
        //private Param_ModemLimitsAndTime Param_ModemLimitsAndTime_Object = new Param_ModemLimitsAndTime();
        //private Param_Modem_Initialize Param_Modem_Initialize_Object = new Param_Modem_Initialize();
        //private Param_ModemBasics_NEW Param_ModemBasics_NEW_object = new Param_ModemBasics_NEW();
        //private Param_IPV4 Param_IPV4_object = new Param_IPV4();
        //private Param_Keep_Alive_IP Param_Keep_Alive_IP_object = new Param_Keep_Alive_IP();
        //private Param_TCP_UDP Param_TCP_UDP_object = new Param_TCP_UDP();

        ///private Param_Limits Param_Limits_object = new Param_Limits();
        ///private Param_Limit_Demand_OverLoad Param_Limit_Demand_OverLoad_T1 = new Param_Limit_Demand_OverLoad();
        ///private Param_Limit_Demand_OverLoad Param_Limit_Demand_OverLoad_T2 = new Param_Limit_Demand_OverLoad();
        ///private Param_Limit_Demand_OverLoad Param_Limit_Demand_OverLoad_T3 = new Param_Limit_Demand_OverLoad();
        ///private Param_Limit_Demand_OverLoad Param_Limit_Demand_OverLoad_T4 = new Param_Limit_Demand_OverLoad();

        private Param_ErrorDetail Param_Error_Details = null;

        private List<LoadProfileChannelInfo> AllSelectableChannels = null;

        private DataBaseController dbController = new DataBaseController();

        ///private Param_MDI_parameters Param_MDI_parameters_object = new Param_MDI_parameters();
        ///private Param_Monitoring_Time Param_Monitoring_time_object = new Param_Monitoring_Time();
        ///private Param_password param_password_object = new Param_password();
        ////private Param_ActivityCalendar Calendar = new Param_ActivityCalendar();

        private LimitValues _Limits;

        ///private Param_TimeBaseEvents TBE1 = new Param_TimeBaseEvents();
        ///private Param_TimeBaseEvents TBE2 = new Param_TimeBaseEvents();
        ///internal TBE_PowerFail obj_TBE_PowerFail = new TBE_PowerFail();
        public TBE tbe_obj = new TBE();

        internal ds_events Dataset_Events = new ds_events();
        internal BackgroundWorker bgw_getReverseEnergyMT;
        internal BackgroundWorker bgw_setReverseEnergyMT;
        internal BackgroundWorker bgw_getEarthMT;
        internal BackgroundWorker bgw_setEarthMT;
        internal BackgroundWorker bgw_setPowerFailMT;
        internal BackgroundWorker bgw_SP_setOverLoadTotal;
        internal BackgroundWorker bgw_SP_getOverLoadTotal;
        internal bool _Modem_Warnings_disable = true;

        #endregion

        private List<TabPage> tbcMeterParam_TABPages = null;
        private List<TabPage> tab_ModemParameters_TABPages = null;
        private List<TabPage> Tab_Main_TABPages = null;


        #endregion

        #region Internal_Parameter_Properties

        #region Param_TimeBaseEvent

        internal Param_TimeBaseEvents TBE1
        {
            get
            {
                return ParamConfigurationSetObj.ParamTimeBaseEvent_1;
            }
            set
            {
                ParamConfigurationSetObj.ParamTimeBaseEvent_1 = value;
            }
        }
        internal Param_TimeBaseEvents TBE2
        {
            get
            {
                return ParamConfigurationSetObj.ParamTimeBaseEvent_2;
            }
            set
            {
                ParamConfigurationSetObj.ParamTimeBaseEvent_2 = value;
            }
        }
        internal TBE_PowerFail obj_TBE_PowerFail
        {
            get
            {
                return ParamConfigurationSetObj.ParamTBPowerFail;
            }
            set
            {
                ParamConfigurationSetObj.ParamTBPowerFail = value;
            }
        }

        #endregion

        internal Param_ActivityCalendar Calendar
        {
            get
            {
                return ParamConfigurationSetObj.ParamTariffication;
            }
            set
            {
                ParamConfigurationSetObj.ParamTariffication = value;
            }
        }

        internal Param_Limits Param_Limits_Object
        {
            get
            {
                return (ParamConfigurationSetObj == null) ? null : ParamConfigurationSetObj.ParamLimits;
            }
            set
            {
                ParamConfigurationSetObj.ParamLimits = value;
            }
        }

        //internal Param_Limit_Demand_OverLoad Param_Limit_Demand_OverLoad_T1 = new Param_Limit_Demand_OverLoad();
        //internal Param_Limit_Demand_OverLoad Param_Limit_Demand_OverLoad_T2 = new Param_Limit_Demand_OverLoad();
        //internal Param_Limit_Demand_OverLoad Param_Limit_Demand_OverLoad_T3 = new Param_Limit_Demand_OverLoad();
        //internal Param_Limit_Demand_OverLoad Param_Limit_Demand_OverLoad_T4 = new Param_Limit_Demand_OverLoad();

        internal Param_Monitoring_Time Param_Monitoring_Time_Object
        {
            get
            {
                return ParamConfigurationSetObj.ParamMonitoringTime;
            }
            set
            {
                ParamConfigurationSetObj.ParamMonitoringTime = value;
            }
        }

        internal Param_MDI_parameters Param_MDI_Parameters_Object
        {
            get
            {
                return ParamConfigurationSetObj.ParamMDI;
            }
            set
            {
                ParamConfigurationSetObj.ParamMDI = value;
            }
        }

        internal Param_Clock_Caliberation Param_MeterClock
        {
            get
            {
                return ParamConfigurationSetObj.ParamClockCalib;
            }
            set
            {
                ParamConfigurationSetObj.ParamClockCalib = value;
            }
        }

        internal Param_Clock_Caliberation Param_Clock_Caliberation_Object
        {
            get
            {
                return ParamConfigurationSetObj.ParamClockCalib;
            }
            set
            {
                ParamConfigurationSetObj.ParamClockCalib = value;
            }
        }

        internal Param_ContactorExt Param_Contactor_Object
        {
            get
            {
                if (ParamConfigurationSetObj.ParamContactor != null &&
                   typeof(Param_ContactorExt) == ParamConfigurationSetObj.ParamContactor.GetType())
                    return (Param_ContactorExt)ParamConfigurationSetObj.ParamContactor;
                else
                    return new Param_ContactorExt(ParamConfigurationSetObj.ParamContactor);
            }
            set
            {
                Param_ContactorExt _val = null;
                if (value != null)//&& typeof(Param_ContactorExt) != value.GetType())
                    _val = new Param_ContactorExt(value);
                ParamConfigurationSetObj.ParamContactor = _val;
            }
        }

        internal Param_CTPT_Ratio Param_CTPT_Ratio_Object
        {
            get
            {
                return ParamConfigurationSetObj.ParamCTPTRatio;
            }
            set
            {
                ParamConfigurationSetObj.ParamCTPTRatio = value;
            }
        }

        internal Param_Customer_Code Param_Customer_Code_Object
        {
            get
            {
                return ParamConfigurationSetObj.ParamCustomerReferenceCode;
            }
            set
            {
                ParamConfigurationSetObj.ParamCustomerReferenceCode = value;
            }
        }

        internal Param_Password Param_Password_Object
        {
            get
            {
                return ParamConfigurationSetObj.ParamPassword;
            }
            set
            {
                ParamConfigurationSetObj.ParamPassword = value;
            }
        }

        internal Param_Decimal_Point Param_Decimal_Point_Object
        {
            get
            {
                return ParamConfigurationSetObj.ParamDecimalPoint;
            }
            set
            {
                ParamConfigurationSetObj.ParamDecimalPoint = value;
            }
        }
        internal Param_StatusWordMap Param_Status_Word_Map_Object
        {
            get
            {
                return ParamConfigurationSetObj.ParamStatusWordMap;
            }
            set
            {
                ParamConfigurationSetObj.ParamStatusWordMap = value;
            }
        }

        internal Param_Energy_Parameter Param_Energy_Parameters_Object
        {
            get
            {
                return ParamConfigurationSetObj.ParamEnergy;
            }
            set
            {
                ParamConfigurationSetObj.ParamEnergy = value;
            }
        }

        internal DisplayWindows Param_DisplayWindowsNormal
        {
            get
            {
                return ParamConfigurationSetObj.ParamDisplayWindowsNormal;
            }
            set
            {
                ParamConfigurationSetObj.ParamDisplayWindowsNormal = value;
                if (ucDisplayWindows1 != null)
                {
                    ucDisplayWindows1.Obj_displayWindows_normal = value;
                    ucDisplayWindows1.Customercode = Param_Customer_Code_Object.Customer_Code_String;
                    ucDisplayWindows1.ActiveSeason = Instantaneous_Class_obj.Active_Season.ToString();
                }
            }
        }

        internal DisplayWindows Param_DisplayWindowsAlternate
        {
            get
            {
                return ParamConfigurationSetObj.ParamDisplayWindowsAlternate;
            }
            set
            {
                ParamConfigurationSetObj.ParamDisplayWindowsAlternate = value;
                if (ucDisplayWindows1 != null)
                {
                    ucDisplayWindows1.Obj_displayWindows_alternate = value;
                    ucDisplayWindows1.Customercode = Param_Customer_Code_Object.Customer_Code_String;
                    ucDisplayWindows1.ActiveSeason = Instantaneous_Class_obj.Active_Season.ToString();
                }
            }
        }

        internal DisplayWindows Param_DisplayWindowsTest
        {
            get
            {
                return ParamConfigurationSetObj.ParamDisplayWindowsTestMode;
            }
            set
            {
                ParamConfigurationSetObj.ParamDisplayWindowsTestMode = value;
                if (ucDisplayWindows1 != null)
                {
                    ucDisplayWindows1.Obj_displayWindows_test = value;
                    ucDisplayWindows1.Customercode = Param_Customer_Code_Object.Customer_Code_String;
                    ucDisplayWindows1.ActiveSeason = Instantaneous_Class_obj.Active_Season.ToString();
                }
            }
        }

        internal List<LoadProfileChannelInfo> LoadProfileChannelsInfo
        {
            get
            {
                return ParamConfigurationSetObj.ParamLoadProfileChannelInfo;
            }
            set
            {
                ParamConfigurationSetObj.ParamLoadProfileChannelInfo = value;
            }
        }
        internal List<LoadProfileChannelInfo> LoadProfileChannelsInfo_2
        {
            get
            {
                return ParamConfigurationSetObj.ParamLoadProfileChannelInfo_2;
            }
            set
            {
                ParamConfigurationSetObj.ParamLoadProfileChannelInfo_2 = value;
            }
        }

        internal TimeSpan LoadProfilePeriod
        {
            get
            {
                TimeSpan LP_Period = TimeSpan.FromMinutes(15);
                if (ucLoadProfile != null)
                {
                    LP_Period = ucLoadProfile.LoadProfilePeriod;
                }
                return LP_Period;
            }
            set
            {
                if (ucLoadProfile != null)
                {
                    ucLoadProfile.LoadProfilePeriod = value;
                }
                try
                {
                    var _LoadProfileChannelInfo = LoadProfileChannelsInfo;
                    foreach (var lpChInfo in _LoadProfileChannelInfo)
                    {
                        if (lpChInfo != null)
                            lpChInfo.CapturePeriod = value;
                    }
                }
                catch { }
            }
        }
        internal TimeSpan LoadProfilePeriod_2
        {
            get
            {
                TimeSpan LP_Period = TimeSpan.FromMinutes(15);
                if (ucLoadProfile != null)
                {
                    LP_Period = ucLoadProfile.LoadProfilePeriod_2;
                }
                return LP_Period;
            }
            set
            {
                if (ucLoadProfile != null)
                {
                    ucLoadProfile.LoadProfilePeriod_2 = value;
                }
                try
                {
                    var _LoadProfileChannelInfo = LoadProfileChannelsInfo_2;
                    foreach (var lpChInfo in _LoadProfileChannelInfo)
                    {
                        if (lpChInfo != null)
                            lpChInfo.CapturePeriod = value;
                    }
                }
                catch { }
            }
        }
        internal TimeSpan PQLoadProfilePeriod
        {
            get
            {
                TimeSpan LP_Period = TimeSpan.FromMinutes(15);
                if (ucLoadProfile != null)
                {
                    LP_Period = ucLoadProfile.PQLoadProfilePeriod;
                }
                return LP_Period;
            }
            set
            {
                if (ucLoadProfile != null)
                {
                    ucLoadProfile.PQLoadProfilePeriod = value;
                }
            }
        }

        internal Param_IP_Profiles[] Param_IP_Profiles_object
        {
            get
            {
                return ParamConfigurationSetObj.ParamIPProfiles;
            }
            set
            {
                ParamConfigurationSetObj.ParamIPProfiles = value;
            }
        }

        internal Param_Number_Profile[] Param_Number_Profile_object
        {
            get
            {
                return ParamConfigurationSetObj.ParamNumberProfile;
            }
            set
            {
                ParamConfigurationSetObj.ParamNumberProfile = value;
            }
        }

        internal Param_WakeUp_Profile[] Param_Wakeup_Profile_object
        {
            get
            {
                return ParamConfigurationSetObj.ParamWakeUpProfile;
            }
            set
            {
                ParamConfigurationSetObj.ParamWakeUpProfile = value;
            }
        }

        internal Param_Communication_Profile Param_Communication_Profile_object
        {
            get
            {
                return ParamConfigurationSetObj.ParamCommunicationProfile;
            }
            set
            {
                ParamConfigurationSetObj.ParamCommunicationProfile = value;
            }
        }

        internal Param_ModemLimitsAndTime Param_ModemLimitsAndTime_Object
        {
            get
            {
                return ParamConfigurationSetObj.ParamModemLimitsAndTime;
            }
            set
            {
                ParamConfigurationSetObj.ParamModemLimitsAndTime = value;
            }
        }

        internal Param_Modem_Initialize Param_Modem_Initialize_Object
        {
            get
            {
                return ParamConfigurationSetObj.ParamModemInitialize;
            }
            set
            {
                ParamConfigurationSetObj.ParamModemInitialize = value;
            }
        }

        internal Param_ModemBasics_NEW Param_ModemBasics_NEW_object
        {
            get
            {
                return ParamConfigurationSetObj.ParamModemBasicsNEW;
            }
            set
            {
                ParamConfigurationSetObj.ParamModemBasicsNEW = value;
            }
        }

        internal Param_Keep_Alive_IP Param_Keep_Alive_IP_object
        {
            get
            {
                return ParamConfigurationSetObj.ParamKeepAliveIP;
            }
            set
            {
                ParamConfigurationSetObj.ParamKeepAliveIP = value;
            }
        }

        internal Param_TCP_UDP Param_TCP_UDP_object
        {
            get
            {
                return ParamConfigurationSetObj.ParamTCPUDP;
            }
            set
            {
                ParamConfigurationSetObj.ParamTCPUDP = value;
            }
        }

        internal Param_Display_PowerDown Param_Display_PowerDown
        {
            get
            {
                return ParamConfigurationSetObj.ParamDisplayPowerDown;
            }
            set
            {
                ParamConfigurationSetObj.ParamDisplayPowerDown = value;
            }
        }

        internal Param_Generel_Process Param_GeneralProcess
        {
            get
            {
                return ParamConfigurationSetObj.ParamGeneralProcess;
            }
            set
            {
                ParamConfigurationSetObj.ParamGeneralProcess = value;
            }
        }

        #endregion

        internal LimitValues Limits
        {
            get { return _Limits; }
            set
            {
                if (value != null && _Limits != value)
                {
                    LocalCommon.AppValidationInfo._LimitValues = value;
                }
                _Limits = value;
            }
        }

        public ApplicationController Application_Controller
        {
            get
            {
                //if (application_Controller == null)
                //    throw new Exception("Application not Initialized properly");
                return application_Controller;
            }
            set
            {
                if (value != application_Controller)
                {
                    application_Controller = value;
                    Application_Controller.PropertyChanged += new PropertyChangedEventHandler(Application_Controller_PropertyChanged);
                    Limits = new LimitValues(Application_Controller.CurrentMeterName);
                }
            }
        }

        private void Application_Controller_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                ///Okay IsIOBusy Status
                if ("IsIOBusy".Equals(e.PropertyName) && sender is ApplicationController)
                {
                    UpdateReadWriteStatus(Application_Controller.IsIOBusy);
                }
                else
                {
                    ///Update Only On NEw Meter Connected
                    ///limits = new LimitsValues(Application_Controller.CurrentMeterName);
                    InitializeUcLimitsParameters();
                    InitializeUcSinglePhaseParameters();
                }
            }
            catch
            { }
        }

        public bool Modem_Warnings_disable
        {
            get { return _Modem_Warnings_disable; }
            set
            {
                _Modem_Warnings_disable = value;
                if (ucIPProfiles != null)
                    ucIPProfiles.Modem_Warnings_disable = _Modem_Warnings_disable;
                if (ucWakeupProfile != null)
                    ucWakeupProfile.Modem_Warnings_disable = _Modem_Warnings_disable;
                if (ucNumberProfile != null)
                    ucNumberProfile.Modem_Warnings_disable = _Modem_Warnings_disable;
                if (ucCommProfile != null)
                    ucCommProfile.Modem_Warnings_disable = _Modem_Warnings_disable;
                if (ucKeepAlive != null)
                    ucKeepAlive.Modem_Warnings_disable = _Modem_Warnings_disable;
                if (ucModemLimitsAndTime != null)
                    ucModemLimitsAndTime.Modem_Warnings_disable = _Modem_Warnings_disable;
                if (ucModemInitialize != null)
                    ucModemInitialize.Modem_Warnings_disable = _Modem_Warnings_disable;
            }
        }

        private List<StatusWord> StatusWordItems1
        {
            get
            {
                if (ucStatusWordMap1 != null)
                    return ucStatusWordMap1.StatusWordItems1;
                else
                    return new List<StatusWord>();
            }
            set
            {
                if (ucStatusWordMap1 != null)
                {
                    //ucStatusWord1.StatusWordItems.Clear();
                    ucStatusWordMap1.StatusWordItems1 = value;
                }
            }
        }

        private List<StatusWord> StatusWordItems2
        {
            get
            {
                if (ucStatusWordMap1 != null)
                    return ucStatusWordMap1.StatusWordItems2;
                else
                    return new List<StatusWord>();
            }
            set
            {
                if (ucStatusWordMap1 != null)
                {
                    //ucStatusWord1.StatusWordItems.Clear();
                    ucStatusWordMap1.StatusWordItems2 = value;
                }
            }
        }

        #region Constructor

        public pnlParameterization()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw |
                            ControlStyles.OptimizedDoubleBuffer |
                            ControlStyles.UserPaint |
                            ControlStyles.AllPaintingInWmPaint |
                            ControlStyles.SupportsTransparentBackColor, true);

            //this.SetStyle(ControlStyles.ResizeRedraw, true);
            //ParamConfigurationSetObj = new AccurateOptocomSoftware.comm.Param.ParamConfigurationSet();
            tbcMeterParam_TABPages = new List<TabPage>();
            //Init tbcMeterParam_TABPages
            foreach (TabPage item in tbcMeterParams.TabPages)
            {
                tbcMeterParam_TABPages.Add(item);
            }
            Tab_Main_TABPages = new List<TabPage>();
            //Init Tab_Main
            foreach (TabPage item in Tab_Main.TabPages)
            {
                Tab_Main_TABPages.Add(item);
            }
            tab_ModemParameters_TABPages = new List<TabPage>();
            //Init tab_ModemParameters
            foreach (TabPage item in tab_ModemParameters.TabPages)
            {
                tab_ModemParameters_TABPages.Add(item);
            }

            this.ucActivityCalendar.Load += ucActivityCalendar_Load;
            this.ucContactor.Load += ucContactor_Load;

            if (cmbTarrifOnGeneratorStart.Items.Count > 0)
            {
                cmbTarrifOnGeneratorStart.SelectedIndex = 0;
            }

        }

        public void RefreshParameterizationConfig(ConnectionInfo connInfo = null)
        {
            try
            {
                Init_LoadProfiles(connInfo);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating parameterization configs", ex);
            }
        }

        #endregion

        public void myTempFunction(string MeterModel)
        {
            Limits = new LimitValues(MeterModel);
            String fileUrl = Environment.CurrentDirectory + "\\DLMS_saved_files\\";
            DirectoryInfo dictoryInfo = new DirectoryInfo(fileUrl);
            if (dictoryInfo.Exists)
                Load_and_show_Limits_Only(fileUrl);
        }

        #region Configuration_Caliberation_Loadall_Save_Click

        private void btn_Caliberation_Save_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog Folder = new FolderBrowserDialog();
                Folder.RootFolder = Environment.SpecialFolder.Desktop;
                Folder.ShowDialog();
                string path = Folder.SelectedPath;
                ///Save Configurations 
                if (!String.IsNullOrEmpty(path) || !String.IsNullOrWhiteSpace(path))
                {
                    ///Save_All_Params(path);
                    if (ucEnergyMizer1 != null)
                        loadedConfigurationSet.ParamEnergyMizer = ucEnergyMizer1.GetParams();
                    XMLParamsProcessor.Export_AllParams(path, application_Controller.CurrentMeterName, loadedConfigurationSet);

                    String ConfURL = path + "\\ParameterExport\\Parameters.conf";
                    XMLParamsProcessor.Export_AllParams(ConfURL, loadedConfigurationSet);
                    MessageBox.Show("Parameters Save Successfully");

                }
                else
                    MessageBox.Show("Profile Saving Unsuccessful, Enter valid Path");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.Parent, String.Format("Error Saving Parameterization Configuration fils,{0}", ex.Message),
                    "Error Saving Configurations", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_caliberation_loadall_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog Folder = new FolderBrowserDialog();
                Folder.RootFolder = Environment.SpecialFolder.Desktop;
                Folder.ShowDialog();
                string path = Folder.SelectedPath;
                if (!String.IsNullOrEmpty(path) || !String.IsNullOrWhiteSpace(path))
                    Load_and_show_all(path + "\\");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.Parent, String.Format("Error Loading Parameterization Configuration files,{0}", ex.Message),
                    "Error Loading Configurations", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion


        #region Saving_Functions_For_All_Objects

        public void Save_All_Params(String Dir)
        {
            //try
            //{
            //    Object loadedObj = null;
            //    Object[] loadedObjs = null;

            //    //FolderBrowserDialog Folder = new FolderBrowserDialog();
            //    //Folder.RootFolder = Environment.SpecialFolder.Desktop;
            //    //Folder.ShowDialog();
            //    ///string Dir = Folder.SelectedPath;

            //    ///Create directory if not Exists
            //    if (!new DirectoryInfo(Dir).Exists)
            //        Directory.CreateDirectory(Dir);

            //    if (!String.IsNullOrEmpty(Dir) || !String.IsNullOrWhiteSpace(Dir))
            //    {
            //        ///Append Notes
            //        string notes = "\r\n" + Dir + "   Created ON :   " + DateTime.Now.ToString();
            //        File.AppendAllText(Dir + "\\.SEC7", notes);

            //        ///String dir;
            //        XMLParamsProcessor.save_CTPTShowError(Dir, Param_CTPT_Ratio_Object);
            //        XMLParamsProcessor.save_DecimalPointShowError(Dir, Param_Decimal_Point_Object);
            //        {
            //            ///Save Display Windows
            //            DisplayWindows[] paramloadedObjs = new DisplayWindows[03];
            //            paramloadedObjs[XMLParamsProcessor.Param_DisplayWindowsNormal] = Param_DisplayWindowsNormal;
            //            paramloadedObjs[XMLParamsProcessor.Param_DisplayWindowsAlternate] = Param_DisplayWindowsAlternate;
            //            paramloadedObjs[XMLParamsProcessor.Param_DisplayWindowstest] = Param_DisplayWindowsTest;
            //            XMLParamsProcessor.Save_DisplayWindowsShowError(Dir, (DisplayWindows[])paramloadedObjs);
            //        }

            //        XMLParamsProcessor.Save_CustomerReferenceShowError(Dir, Param_Customer_Code_Object);
            //        XMLParamsProcessor.Save_ClockShowError(Dir, Param_Clock_Caliberation_Object);
            //        XMLParamsProcessor.Save_EnergyParamsShowError(Dir, Param_Energy_Parameters_Object);
            //        XMLParamsProcessor.Save_MDIParamsShowError(Dir, Param_MDI_Parameters_Object);
            //        XMLParamsProcessor.Save_ContactorShowError(Dir, Param_Contactor_Object);
            //        ///Save Load Profile Channels Info
            //        if (LoadProfileChannelsInfo != null && LoadProfileChannelsInfo.Count > 0)
            //        {
            //            LoadProfileChannelInfo[] paramloadedObjs = new LoadProfileChannelInfo[LoadProfileChannelsInfo.Count];
            //            int index = 0;
            //            foreach (var channelInfo in LoadProfileChannelsInfo)
            //            {
            //                paramloadedObjs[index++] = channelInfo;
            //            }
            //            XMLParamsProcessor.Save_LoadProfileShowError(Dir, paramloadedObjs);
            //        }
            //        XMLParamsProcessor.Save_MonitoringTimeShowError(Dir, Param_Monitoring_Time_Object);
            //        XMLParamsProcessor.Save_LimitsShowError(Dir, application_Controller.CurrentMeterName, Param_Limits_Object);
            //        {
            //            ///Save Params Limits_Param_Limit_Demand_OverLoad
            //            Param_Limit_Demand_OverLoad[] paramloadedObjs = new Param_Limit_Demand_OverLoad[04];
            //            paramloadedObjs[0] = Param_Limit_Demand_OverLoad_T1;
            //            paramloadedObjs[1] = Param_Limit_Demand_OverLoad_T2;
            //            paramloadedObjs[2] = Param_Limit_Demand_OverLoad_T3;
            //            paramloadedObjs[3] = Param_Limit_Demand_OverLoad_T4;
            //            XMLParamsProcessor.SaveAll_Limits_Param_Limit_Demand_OverLoadShowError(Dir, application_Controller.CurrentMeterName,
            //                                  (Param_Limit_Demand_OverLoad[])paramloadedObjs);
            //        }
            //        ///Save Modem Parameters
            //        ///Save Param_IP_Profiles
            //        XMLParamsProcessor.Save_IP_profileShowError(Dir, Param_IP_Profiles_object);
            //        XMLParamsProcessor.Save_NumberProfileShowError(Dir, Param_Number_Profile_object);
            //        XMLParamsProcessor.Save_CommProfile(Dir, Param_Communication_Profile_object);
            //        XMLParamsProcessor.Save_Modem_InitializeShowError(Dir, Param_Modem_Initialize_Object);
            //        XMLParamsProcessor.Save_Modem_InitializeShowError(Dir, Param_ModemBasics_NEW_object);
            //        XMLParamsProcessor.Save_WakeUpProfileShowError(Dir, Param_Wakeup_Profile_object);
            //        XMLParamsProcessor.Save_ModemLimitsAndTimeShowError(Dir, Param_ModemLimitsAndTime_Object);
            //        XMLParamsProcessor.Save_IPV4ShowError(Dir, Param_IPV4_object);
            //        XMLParamsProcessor.Save_KeepAliveShowError(Dir, Param_Keep_Alive_IP_object);
            //        ///Save Param_Activity_Calendar
            //        XMLParamsProcessor.Save_ActivityCalendarShowError(Dir, Calendar);
            //        #region ///here sync from ParamTimeBaseEvent Object

            //        if (tbe_obj == null)
            //            tbe_obj = new TBE();
            //        ///Sync ParamTimeBaseEvent1
            //        tbe_obj.ControlEnum_Tbe1 = TBE1.Control_Enum;
            //        tbe_obj.Tbe1_interval = TBE1.Interval;
            //        tbe_obj.Tbe1_datetime = TBE1.DateTime;
            //        ///Sync ParamTimeBaseEvent2
            //        tbe_obj.ControlEnum_Tbe2 = TBE2.Control_Enum;
            //        tbe_obj.Tbe2_interval = TBE2.Interval;
            //        tbe_obj.Tbe2_datetime = TBE2.DateTime;

            //        #endregion
            //        XMLParamsProcessor.Save_TOFILE_TBEsShowError(Dir, tbe_obj);
            //    }
            //    else
            //        throw new Exception("Profile Saving Unsuccessful,Enter valid Path");
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(String.Format("Error Saving Parameterization Configuration files,{0}", ex.Message), ex);
            //}
        }

        public void Export_All_Params(String Dir)
        {
            //try
            //{
            //    //FolderBrowserDialog Folder = new FolderBrowserDialog();
            //    //Folder.RootFolder = Environment.SpecialFolder.Desktop;
            //    //Folder.ShowDialog();
            //    ///string Dir = Folder.SelectedPath;

            //    ///Create directory if not Exists
            //    //if (!new DirectoryInfo(Dir).Exists)
            //    //    Directory.CreateDirectory(Dir);

            //    //if (!String.IsNullOrEmpty(Dir) || !String.IsNullOrWhiteSpace(Dir))
            //    //{
            //    Export_Parameters(Dir + "\\ParameterExport\\Parameters.conf", null, null);
            //    ///Save All Parameters To Single XML Source File
            //    ///XMLParamsProcessor.Save_AllParameters(Dir + "\\ParameterExport\\Parameters.conf", ParamList);
            //    //}
            //    //else
            //    //    throw new Exception("Profile Saving Unsuccessful,Enter valid Path");
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(String.Format("Error Exporting Parameterization Configuration file,{0}", ex.Message), ex);
            //}
        }

        public void Export_Parameters(string fileURL, List<Params> SelectedParameters = null, List<ParamsCategory> SelectedParamsCategory = null)
        {
            //try
            //{
            //    List<IParam> ParamList = new List<IParam>();
            //    #region Handle_Null_ArgumentsException
            //    if ((SelectedParameters == null || SelectedParameters.Count <= 0) &&
            //                (SelectedParamsCategory == null || SelectedParamsCategory.Count <= 0))
            //    {
            //        SelectedParamsCategory = new List<ParamsCategory>();
            //        ///Add Category For All Parameters
            //        SelectedParamsCategory.Add(ParamsCategory.ParamMeter);
            //        SelectedParamsCategory.Add(ParamsCategory.ParamMajorAlarm);
            //        SelectedParamsCategory.Add(ParamsCategory.ParamModem);
            //    }
            //    #endregion
            //    if (SelectedParameters == null)
            //        SelectedParameters = new List<Params>();
            //    #region ///Process ParamsCategory
            //    foreach (var category in SelectedParamsCategory)
            //    {
            //        Params[] paramList = ConfigFileHelper.GetParamsByCategory(category);
            //        foreach (var selParam in paramList)
            //        {
            //            if (!SelectedParameters.Contains(selParam))
            //                SelectedParameters.Add(selParam);
            //        }
            //    }
            //    SelectedParameters.Sort();
            //    #endregion
            //    #region Process_Meter_Parameters

            //    #region ///ParamMonitoringTime
            //    if (SelectedParameters.Contains(Params.ParamMonitoringTime))
            //        ParamList.Add(Param_Monitoring_Time_Object);
            //    #endregion
            //    #region ///ParamLimitsObject
            //    if (SelectedParameters.Contains(Params.ParamLimits))
            //    {
            //        //ParamList.Add(Param_Limits_Object);
            //        //ParamList.Add(Param_Limit_Demand_OverLoad_T1);
            //        //ParamList.Add(Param_Limit_Demand_OverLoad_T2);
            //        //ParamList.Add(Param_Limit_Demand_OverLoad_T3);
            //        //ParamList.Add(Param_Limit_Demand_OverLoad_T4);
            //    }
            //    #endregion
            //    #region ///ParamTariffication
            //    if (SelectedParameters.Contains(Params.ParamTariffication))
            //        ParamList.Add(Calendar);
            //    #endregion
            //    #region Param_MDI_parameters_object
            //    if (SelectedParameters.Contains(Params.ParamMDI))
            //        ParamList.Add(Param_MDI_Parameters_Object);
            //    #endregion
            //    #region ParamLoadProfilePeriod
            //    //if (SelectedParameters.Contains(Params.ParamLoadProfilePeriod))
            //    //{
            //    //    foreach (var lpChInfo in LoadProfileChannelsInfo)
            //    //    {
            //    //        lpChInfo.CapturePeriod = LoadProfilePeriod;
            //    //    }
            //    //}
            //    #endregion
            //    #region ParamLoadProfileChannelInfo
            //    if (SelectedParameters.Contains(Params.ParamLoadProfileChannelInfo))
            //    {
            //        ParamList.AddRange(LoadProfileChannelsInfo);
            //    }
            //    #endregion
            //    #region ParamDisplayWindows

            //    if (SelectedParameters.Contains(Params.ParamDisplayWindowsNormal))
            //    {
            //        Param_DisplayWindowsTest.WindowsMode = DispalyWindowsModes.Normal;
            //        ParamList.Add(Param_DisplayWindowsNormal);
            //    }
            //    if (SelectedParameters.Contains(Params.ParamDisplayWindowsAlternate))
            //    {
            //        Param_DisplayWindowsTest.WindowsMode = DispalyWindowsModes.Alternate;
            //        ParamList.Add(Param_DisplayWindowsAlternate);
            //    }
            //    if (SelectedParameters.Contains(Params.ParamDisplayWindowsTestMode))
            //    {
            //        Param_DisplayWindowsTest.WindowsMode = DispalyWindowsModes.Test;
            //        ParamList.Add(Param_DisplayWindowsTest);
            //    }

            //    #endregion
            //    #region ParamCTPTRatio
            //    if (SelectedParameters.Contains(Params.ParamCTPTRatio))
            //    {
            //        ParamList.Add(Param_CTPT_Ratio_Object);
            //    }
            //    #endregion
            //    #region ParamDecimalPoint
            //    if (SelectedParameters.Contains(Params.ParamDecimalPoint))
            //    {
            //        ParamList.Add(Param_Decimal_Point_Object);
            //    }
            //    #endregion
            //    #region ParamCustomerReferenceCode
            //    if (SelectedParameters.Contains(Params.ParamCustomerReferenceCode))
            //    {
            //        ParamList.Add(Param_Customer_Code_Object);
            //    }
            //    #endregion
            //    #region ParamPassword
            //    if (SelectedParameters.Contains(Params.ParamPassword))
            //    {
            //        ParamList.Add(Param_Password_Object);
            //    }
            //    #endregion
            //    #region ParamEnergy
            //    if (SelectedParameters.Contains(Params.ParamEnergy))
            //    {
            //        ParamList.Add(Param_Energy_Parameters_Object);
            //    }
            //    #endregion
            //    #region ParamClockCalib
            //    if (SelectedParameters.Contains(Params.ParamClockCalib))
            //    {
            //        ParamList.Add(Param_Clock_Caliberation_Object);
            //    }
            //    #endregion
            //    #region ParamClock
            //    if (SelectedParameters.Contains(Params.ParamClock))
            //    {
            //        ParamList.Add(Param_MeterClock);
            //    }
            //    #endregion
            //    #region ParamContactor
            //    if (SelectedParameters.Contains(Params.ParamContactor))
            //    {
            //        ParamList.Add(Param_Contactor_Object);
            //    }
            //    #endregion
            //    #region ParamTimeBaseEvent

            //    if (SelectedParameters.Contains(Params.ParamTimeBaseEvent))
            //    {
            //        ParamList.Add(TBE1);
            //        ParamList.Add(TBE2);
            //    }
            //    if (SelectedParameters.Contains(Params.ParamTBPowerFail))
            //    {
            //        ParamList.Add(obj_TBE_PowerFail);
            //    }

            //    #endregion

            //    #endregion
            //    ///Param_MajorAlarm Parameterization
            //    #region ParamMajorAlarmProfile
            //    if (SelectedParameters.Contains(Params.ParamMajorAlarmProfile))
            //    {
            //        ///ParamList.Add(tbe_obj);
            //    }
            //    #endregion
            //    #region ParamEventsCaution
            //    if (SelectedParameters.Contains(Params.ParamEventsCaution))
            //    {
            //        //ParamList.Add(tbe_obj);
            //    }
            //    #endregion
            //    #region Process_ModemParameters
            //    #region ///ParamIPProfiles

            //    if (SelectedParameters.Contains(Params.ParamIPProfiles))
            //        ParamList.AddRange(Param_IP_Profiles_object);
            //    #endregion
            //    #region ///ParamWakeUpProfile

            //    if (SelectedParameters.Contains(Params.ParamWakeUpProfile))
            //        ParamList.AddRange(Param_Wakeup_Profile_object);
            //    #endregion
            //    #region ///ParamNumberProfile

            //    if (SelectedParameters.Contains(Params.ParamNumberProfile))
            //        ParamList.AddRange(Param_Number_Profile_object);
            //    #endregion
            //    #region ///ParamCommunicationProfile

            //    if (SelectedParameters.Contains(Params.ParamCommunicationProfile))
            //        ParamList.Add(Param_Communication_Profile_object);
            //    #endregion
            //    #region ///ParamKeepAliveIP

            //    if (SelectedParameters.Contains(Params.ParamKeepAliveIP))
            //        ParamList.Add(Param_Keep_Alive_IP_object);
            //    #endregion
            //    #region ///ParamModemLimitsAndTime

            //    if (SelectedParameters.Contains(Params.ParamModemLimitsAndTime))
            //        ParamList.Add(Param_ModemLimitsAndTime_Object);
            //    #endregion
            //    #region ///ParamModemInitialize

            //    if (SelectedParameters.Contains(Params.ParamModemInitialize))
            //        ParamList.Add(Param_Modem_Initialize_Object);
            //    #endregion
            //    #region ///ParamModemBasicsNEW

            //    if (SelectedParameters.Contains(Params.ParamModemBasicsNEW))
            //        ParamList.Add(Param_ModemBasics_NEW_object);
            //    #endregion
            //    #region ///ParamTCPUDP

            //    if (SelectedParameters.Contains(Params.ParamTCPUDP))
            //        ParamList.Add(Param_TCP_UDP_object);
            //    #endregion
            //    #endregion
            //    #region MISC

            //    ParamList.Add(Param_IPV4_object);
            //    ParamList.Add(Param_Error_Details);

            //    #endregion
            //    ///Remove Nullable ParamList
            //    for (int index = 0; index < ParamList.Count; index++)
            //    {
            //        if (ParamList[index] == null)
            //        {
            //            ParamList.Remove(ParamList[index]);
            //            index--;
            //        }
            //    }
            //    ///Save All Parameters To Single XML Source File
            //    ///XMLParamsProcessor.Save_AllParameters(Dir + "\\ParameterExport\\Parameters.conf", ParamList);
            //    XMLParamsProcessor.Save_AllParameters(fileURL, ParamList);
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(String.Format("Error occured while exec Export_Parameters {0}", ex.Message), ex);
            //}
        }

        public void Import_All_Params(string Dir)
        {
            //try
            //{
            //    Import_All_Params(Dir + "\\ParameterExport\\Parameters.conf", null, null);
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(String.Format("Error occured while Importing Parameterization Configuration file,{0}", ex.Message), ex);
            //}
        }

        public void Import_All_Params(string fileURL, List<Params> SelectedParameters = null, List<ParamsCategory> SelectedParamsCategory = null)
        {
            //List<IParam> ParamList = null;
            //try
            //{
            //    ///FolderBrowserDialog Folder = new FolderBrowserDialog();
            //    ///Folder.RootFolder = Environment.SpecialFolder.Desktop;
            //    ///Folder.ShowDialog();
            //    ///string Dir = Folder.SelectedPath;
            //    ///Create directory if not Exists
            //    //if (!new DirectoryInfo(Dir).Exists)
            //    // 
            //    //if (!String.IsNullOrEmpty(Dir) || !String.IsNullOrWhiteSpace(Dir))
            //    //{
            //    ///Load All Parameters From Single XML Source File
            //    ///ParamList = XMLParamsProcessor.Load_AllParameters(Dir + "\\ParameterExport\\Parameters.conf");

            //    HeaderInfo HInfo = null;
            //    #region Handle_Null_ArgumentsException
            //    if ((SelectedParameters == null || SelectedParameters.Count <= 0) &&
            //                (SelectedParamsCategory == null || SelectedParamsCategory.Count <= 0))
            //        SelectedParamsCategory = new List<ParamsCategory>();
            //    #endregion
            //    if (SelectedParameters == null)
            //        SelectedParameters = new List<Params>();
            //    #region ///Process ParamsCategory
            //    foreach (var category in SelectedParamsCategory)
            //    {
            //        Params[] paramList = ConfigFileHelper.GetParamsByCategory(category);
            //        foreach (var selParam in paramList)
            //        {
            //            if (!SelectedParameters.Contains(selParam))
            //                SelectedParameters.Add(selParam);
            //        }
            //    }
            //    SelectedParameters.Sort();
            //    #endregion
            //    ParamList = XMLParamsProcessor.Load_AllParameters(fileURL);
            //    List<IParam> SubParamList = null;
            //    if (ParamList.Count > 0)
            //        HInfo = (HeaderInfo)ParamList[0];
            //    #region Process_Meter_Parameters

            //    #region ///ParamMonitoringTime

            //    if (HInfo.ParamList.Contains(Params.ParamMonitoringTime))
            //    {
            //        try
            //        {
            //            SubParamList = ConfigFileHelper.SelectByParam(ParamList, Params.ParamMonitoringTime);
            //            ///Verify Param_Monitoring_Time here
            //            Param_Monitoring_Time_Object = (Param_Monitoring_Time)SubParamList[0];
            //        }
            //        catch
            //        {
            //            throw new Exception("Unable to Import ParamMonitoringTime");
            //        }
            //    }

            //    #endregion
            //    #region ///ParamLimits

            //    if (HInfo.ParamList.Contains(Params.ParamLimits))
            //    {
            //        try
            //        {
            //            SubParamList = ConfigFileHelper.SelectByParam(ParamList, Params.ParamLimits);
            //            ///Verify Param_Limits here
            //            Param_Limits_Object = (Param_Limits)SubParamList[0];

            //            //Param_Limit_Demand_OverLoad_T1 = (Param_Limit_Demand_OverLoad)SubParamList[1];
            //            //Param_Limit_Demand_OverLoad_T2 = (Param_Limit_Demand_OverLoad)SubParamList[2];
            //            //Param_Limit_Demand_OverLoad_T3 = (Param_Limit_Demand_OverLoad)SubParamList[3];
            //            //Param_Limit_Demand_OverLoad_T4 = (Param_Limit_Demand_OverLoad)SubParamList[4];
            //        }
            //        catch
            //        {
            //            throw new Exception("Unable to Import Param_Limits");
            //        }
            //    }

            //    #endregion
            //    #region ///ParamTariffication

            //    if (HInfo.ParamList.Contains(Params.ParamTariffication))
            //    {
            //        try
            //        {
            //            SubParamList = ConfigFileHelper.SelectByParam(ParamList, Params.ParamTariffication);
            //            ///Verify Param_ActivityCalendar here
            //            Calendar = (Param_ActivityCalendar)SubParamList[0];
            //        }
            //        catch
            //        {
            //            throw new Exception("Unable to Import Param_ActivityCalendar");
            //        }
            //    }

            //    #endregion
            //    #region Param_MDI_parameters_object

            //    if (HInfo.ParamList.Contains(Params.ParamMDI))
            //    {
            //        try
            //        {
            //            SubParamList = ConfigFileHelper.SelectByParam(ParamList, Params.ParamMDI);
            //            ///Verify Param_MDI_parameters_object here
            //            Param_MDI_Parameters_Object = (Param_MDI_parameters)SubParamList[0];
            //        }
            //        catch
            //        {
            //            throw new Exception("Unable to Import Param_MDI_parameters_object");
            //        }
            //    }

            //    #endregion
            //    #region ParamLoadProfileChannelInfo

            //    if (HInfo.ParamList.Contains(Params.ParamLoadProfileChannelInfo))
            //    {
            //        try
            //        {
            //            SubParamList = ConfigFileHelper.SelectByParam(ParamList, Params.ParamLoadProfileChannelInfo);
            //            if (SubParamList.Count > 0 && SubParamList[0].GetType() == typeof(LoadProfileChannelInfo))
            //            {
            //                LoadProfileChannelsInfo = new List<LoadProfileChannelInfo>();
            //                ///Verify LoadProfileChannelsInfo here
            //                foreach (LoadProfileChannelInfo chInfo in SubParamList)
            //                {
            //                    LoadProfileChannelsInfo.Add(chInfo);
            //                }
            //            }
            //            else
            //                throw new Exception("Invalid ParamLoadProfileChannelInfo Structure");
            //        }
            //        catch
            //        {
            //            throw new Exception("Unable to Import LoadProfileChannelsInfo");
            //        }
            //    }

            //    #endregion
            //    #region ParamLoadProfilePeriod

            //    ///if (SelectedParameters.Contains(Params.ParamLoadProfilePeriod))
            //    {
            //        if (LoadProfileChannelsInfo.Count > 0)
            //        {
            //            LoadProfilePeriod = LoadProfileChannelsInfo[0].CapturePeriod;
            //        }
            //    }

            //    #endregion
            //    #region ParamDisplayWindows

            //    if (HInfo.ParamList.Contains(Params.ParamDisplayWindowsNormal))
            //    {
            //        try
            //        {
            //            SubParamList = ConfigFileHelper.SelectByParam(ParamList, Params.ParamDisplayWindowsNormal);
            //            ///Verify Param_DisplayWindowsNormal here
            //            foreach (DisplayWindows dispWin in SubParamList)
            //            {
            //                if (dispWin != null && dispWin.WindowsMode == DispalyWindowsModes.Normal)
            //                {
            //                    Param_DisplayWindowsNormal = dispWin;
            //                    break;
            //                }
            //            }
            //        }
            //        catch
            //        {
            //            throw new Exception("Unable to Import Param_DisplayWindowsNormal");
            //        }
            //    }
            //    if (HInfo.ParamList.Contains(Params.ParamDisplayWindowsAlternate))
            //    {
            //        try
            //        {
            //            SubParamList = ConfigFileHelper.SelectByParam(ParamList, Params.ParamDisplayWindowsAlternate);
            //            ///Verify Param_DisplayWindowsAlternate here
            //            foreach (DisplayWindows dispWin in SubParamList)
            //            {
            //                if (dispWin != null && dispWin.WindowsMode == DispalyWindowsModes.Alternate)
            //                {
            //                    Param_DisplayWindowsAlternate = dispWin;
            //                    break;
            //                }
            //            }
            //        }
            //        catch
            //        {
            //            throw new Exception("Unable to Import Param_DisplayWindowsAlternate");
            //        }
            //    }
            //    if (HInfo.ParamList.Contains(Params.ParamDisplayWindowsTestMode))
            //    {
            //        try
            //        {
            //            SubParamList = ConfigFileHelper.SelectByParam(ParamList, Params.ParamDisplayWindowsTestMode);
            //            ///Verify Param_DisplayWindowstest here
            //            foreach (DisplayWindows dispWin in SubParamList)
            //            {
            //                if (dispWin != null && dispWin.WindowsMode == DispalyWindowsModes.Test)
            //                {
            //                    Param_DisplayWindowsTest = dispWin;
            //                    break;
            //                }
            //            }
            //        }
            //        catch
            //        {
            //            throw new Exception("Unable to Import Param_DisplayWindowstest");
            //        }
            //    }

            //    #endregion
            //    #region ParamCTPTRatio

            //    if (HInfo.ParamList.Contains(Params.ParamCTPTRatio))
            //    {
            //        try
            //        {
            //            SubParamList = ConfigFileHelper.SelectByParam(ParamList, Params.ParamCTPTRatio);
            //            ///Verify Param_CTPT_ratio_object here
            //            Param_CTPT_Ratio_Object = (Param_CTPT_Ratio)SubParamList[0];
            //        }
            //        catch
            //        {
            //            throw new Exception("Unable to Import Param_CTPT_ratio_object");
            //        }
            //    }

            //    #endregion
            //    #region ParamDecimalPoint

            //    if (HInfo.ParamList.Contains(Params.ParamDecimalPoint))
            //    {
            //        try
            //        {
            //            SubParamList = ConfigFileHelper.SelectByParam(ParamList, Params.ParamDecimalPoint);
            //            ///Verify Param_decimal_point_object here
            //            Param_Decimal_Point_Object = (Param_Decimal_Point)SubParamList[0];
            //        }
            //        catch
            //        {
            //            throw new Exception("Unable to Import Param_Decimal_Point");
            //        }
            //    }

            //    #endregion
            //    #region Param_password_object

            //    if (HInfo.ParamList.Contains(Params.ParamPassword))
            //    {
            //        try
            //        {
            //            SubParamList = ConfigFileHelper.SelectByParam(ParamList, Params.ParamPassword);
            //            ///Verify param_password_object here
            //            Param_Password_Object = (Param_password)SubParamList[0];
            //        }
            //        catch
            //        {
            //            throw new Exception("Unable to Import Param_password_object");
            //        }
            //    }

            //    #endregion
            //    #region ParamEnergy

            //    if (HInfo.ParamList.Contains(Params.ParamEnergy))
            //    {
            //        try
            //        {
            //            SubParamList = ConfigFileHelper.SelectByParam(ParamList, Params.ParamEnergy);
            //            ///Verify Param_energy_parameters_object here
            //            Param_Energy_Parameters_Object = (Param_Energy_Parameter)SubParamList[0];
            //        }
            //        catch
            //        {
            //            throw new Exception("Unable to Import Param_energy_parameters_object");
            //        }
            //    }

            //    #endregion
            //    #region ParamClockCalib

            //    if (HInfo.ParamList.Contains(Params.ParamClockCalib))
            //    {
            //        try
            //        {
            //            SubParamList = ConfigFileHelper.SelectByParam(ParamList, Params.ParamClockCalib);
            //            ///Verify Param_clock_caliberation_object here
            //            Param_Clock_Caliberation_Object = (Param_Clock_Caliberation)SubParamList[0];
            //        }
            //        catch
            //        {
            //            throw new Exception("Unable to Import Param_clock_caliberation_object");
            //        }
            //    }

            //    #endregion
            //    #region ParamClock

            //    if (HInfo.ParamList.Contains(Params.ParamClock))
            //    {
            //        try
            //        {
            //            SubParamList = ConfigFileHelper.SelectByParam(ParamList, Params.ParamClock);
            //            ///Verify ParamClock here
            //            Param_Clock_Caliberation_Object = (Param_Clock_Caliberation)SubParamList[0];
            //        }
            //        catch
            //        {
            //            throw new Exception("Unable to Import Param_Clock_Caliberation");
            //        }
            //    }

            //    #endregion
            //    #region ParamContactor

            //    if (HInfo.ParamList.Contains(Params.ParamContactor))
            //    {
            //        try
            //        {
            //            SubParamList = ConfigFileHelper.SelectByParam(ParamList, Params.ParamContactor);
            //            ///Verify Param_Contactor here
            //            Param_Contactor_Object = (Param_Contactor)SubParamList[0];
            //        }
            //        catch
            //        {
            //            throw new Exception("Unable to Import Param_Contactor");
            //        }
            //    }

            //    #endregion
            //    #region Param_TimeBaseEvents

            //    if (HInfo.ParamList.Contains(Params.ParamTimeBaseEvent))
            //    {
            //        try
            //        {
            //            SubParamList = ConfigFileHelper.SelectByParam(ParamList, Params.ParamTimeBaseEvent);
            //            ///Verify ParamTimeBaseEvent here
            //            TBE1 = (Param_TimeBaseEvents)SubParamList[0];
            //            TBE2 = (Param_TimeBaseEvents)SubParamList[1];
            //        }
            //        catch
            //        {
            //            throw new Exception("Unable to Import ParamTimeBaseEvent");
            //        }
            //    }
            //    if (HInfo.ParamList.Contains(Params.ParamTBPowerFail))
            //    {
            //        try
            //        {
            //            SubParamList = ConfigFileHelper.SelectByParam(ParamList, Params.ParamTBPowerFail);
            //            ///Verify ParamTBPowerFail here
            //            obj_TBE_PowerFail = (TBE_PowerFail)SubParamList[0];
            //        }
            //        catch
            //        {
            //            throw new Exception("Unable to Import obj_TBE_PowerFail");
            //        }
            //    }

            //    #endregion
            //    ///Param_MajorAlarm Parameterization
            //    #region ParamMajorAlarmProfile

            //    if (HInfo.ParamList.Contains(Params.ParamMajorAlarmProfile))
            //    {
            //        try
            //        {
            //            SubParamList = ConfigFileHelper.SelectByParam(ParamList, Params.ParamMajorAlarmProfile);
            //            ///Verify ParamTimeBaseEvent here
            //            ///tbe_obj = (TBE)SubParamList[0];
            //        }
            //        catch
            //        {
            //            throw new Exception("Unable to Import ParamMajorAlarmProfile");
            //        }
            //    }

            //    #endregion
            //    #region ParamEventsCaution

            //    if (HInfo.ParamList.Contains(Params.ParamEventsCaution))
            //    {
            //        try
            //        {
            //            SubParamList = ConfigFileHelper.SelectByParam(ParamList, Params.ParamEventsCaution);
            //            ///Verify ParamTimeBaseEvent here
            //            ///tbe_obj = (TBE)SubParamList[0];
            //        }
            //        catch
            //        {
            //            throw new Exception("Unable to Import ParamEventsCaution");
            //        }
            //    }

            //    #endregion
            //    #endregion
            //    #region Process_ModemParameters

            //    if (ConfigFileHelper.IsCompleteModemParamDefined(HInfo))
            //    {
            //        ///here init ModemParameters
            //        InitializeUcModemParameters();
            //    }

            //    #region ///ParamIPProfiles

            //    if (HInfo.ParamList.Contains(Params.ParamIPProfiles))
            //    {
            //        try
            //        {
            //            SubParamList = ConfigFileHelper.SelectByParam(ParamList, Params.ParamIPProfiles);
            //            ///Verify Param_IP_Profiles here
            //            if (SubParamList != null && SubParamList.Count > 0 &&
            //                SubParamList[0].GetType() == typeof(Param_IP_Profiles))
            //            {
            //                ///Param_IP_Profiles_object = new Param_IP_Profiles[SubParamList.Count];
            //                int count = 0;
            //                foreach (var prmIPProfile in SubParamList)
            //                {
            //                    Param_IP_Profiles_object[count++] = (Param_IP_Profiles)prmIPProfile;
            //                }
            //            }
            //            else
            //                throw new Exception("Unable to Import Param_IP_Profiles_object");
            //        }
            //        catch
            //        {
            //            throw new Exception("Unable to Import Param_IP_Profiles_object");
            //        }
            //    }

            //    #endregion
            //    #region ///ParamWakeUpProfile

            //    if (HInfo.ParamList.Contains(Params.ParamWakeUpProfile))
            //    {
            //        try
            //        {
            //            SubParamList = ConfigFileHelper.SelectByParam(ParamList, Params.ParamWakeUpProfile);
            //            ///Verify ParamWakeUpProfile here
            //            if (SubParamList != null && SubParamList.Count > 0 &&
            //                SubParamList[0].GetType() == typeof(Param_WakeUp_Profile))
            //            {
            //                ///Param_Wakeup_Profile_object = new Param_WakeUp_Profile[SubParamList.Count];
            //                int count = 0;
            //                foreach (var prmWkProfile in SubParamList)
            //                {
            //                    Param_Wakeup_Profile_object[count++] = (Param_WakeUp_Profile)prmWkProfile;
            //                }
            //            }
            //            else
            //                throw new Exception("Unable to Import Param_Wakeup_Profile_object");
            //        }
            //        catch
            //        {
            //            throw new Exception("Unable to Import Param_Wakeup_Profile_object");
            //        }
            //    }

            //    #endregion
            //    #region ///ParamNumberProfile

            //    if (HInfo.ParamList.Contains(Params.ParamNumberProfile))
            //    {
            //        try
            //        {
            //            SubParamList = ConfigFileHelper.SelectByParam(ParamList, Params.ParamNumberProfile);
            //            ///Verify Param_Number_Profile here
            //            if (SubParamList != null && SubParamList.Count > 0 &&
            //                SubParamList[0].GetType() == typeof(Param_Number_Profile))
            //            {
            //                ///Param_Number_Profile_object = new Param_Number_Profile[SubParamList.Count];
            //                int count = 0;
            //                foreach (var prmNumProfile in SubParamList)
            //                {
            //                    Param_Number_Profile_object[count++] = (Param_Number_Profile)prmNumProfile;
            //                }
            //            }
            //            else
            //                throw new Exception("Unable to Import Param_Number_Profile_object");
            //        }
            //        catch
            //        {
            //            throw new Exception("Unable to Import Param_Number_Profile_object");
            //        }
            //    }

            //    #endregion
            //    #region ///Param_Communication_Profile_object

            //    if (HInfo.ParamList.Contains(Params.ParamCommunicationProfile))
            //    {
            //        try
            //        {
            //            SubParamList = ConfigFileHelper.SelectByParam(ParamList, Params.ParamCommunicationProfile);
            //            ///Verify Param_Communication_Profile here
            //            Param_Communication_Profile_object = (Param_Communication_Profile)SubParamList[0];
            //        }
            //        catch
            //        {
            //            throw new Exception("Unable to Import Param_Communication_Profile_object");
            //        }
            //    }

            //    #endregion
            //    #region ///ParamKeepAliveIP

            //    if (HInfo.ParamList.Contains(Params.ParamKeepAliveIP))
            //    {
            //        try
            //        {
            //            SubParamList = ConfigFileHelper.SelectByParam(ParamList, Params.ParamKeepAliveIP);
            //            ///Verify Param_Keep_Alive_IP here
            //            Param_Keep_Alive_IP_object = (Param_Keep_Alive_IP)SubParamList[0];
            //        }
            //        catch
            //        {
            //            throw new Exception("Unable to Import Param_Keep_Alive_IP");
            //        }
            //    }

            //    #endregion
            //    #region ///Param_ModemLimitsAndTime_Object

            //    if (HInfo.ParamList.Contains(Params.ParamModemLimitsAndTime))
            //    {
            //        try
            //        {
            //            SubParamList = ConfigFileHelper.SelectByParam(ParamList, Params.ParamModemLimitsAndTime);
            //            ///Verify Param_ModemLimitsAndTime_Object here
            //            Param_ModemLimitsAndTime_Object = (Param_ModemLimitsAndTime)SubParamList[0];
            //        }
            //        catch
            //        {
            //            throw new Exception("Unable to Import Param_ModemLimitsAndTime");
            //        }
            //    }

            //    #endregion
            //    #region ///ParamModemInitialize

            //    if (HInfo.ParamList.Contains(Params.ParamModemInitialize))
            //    {
            //        try
            //        {
            //            SubParamList = ConfigFileHelper.SelectByParam(ParamList, Params.ParamModemInitialize);
            //            ///Verify ParamModemInitialize here
            //            Param_Modem_Initialize_Object = (Param_Modem_Initialize)SubParamList[0];
            //        }
            //        catch
            //        {
            //            throw new Exception("Unable to Import Param_Modem_Initialize_Object");
            //        }
            //    }

            //    #endregion
            //    #region ///ParamModemBasicsNEW

            //    if (HInfo.ParamList.Contains(Params.ParamModemBasicsNEW))
            //    {
            //        try
            //        {
            //            SubParamList = ConfigFileHelper.SelectByParam(ParamList, Params.ParamModemBasicsNEW);
            //            ///Verify Param_ModemBasics_NEW_object here
            //            Param_ModemBasics_NEW_object = (Param_ModemBasics_NEW)SubParamList[0];
            //        }
            //        catch
            //        {
            //            throw new Exception("Unable to Import Param_ModemBasics_NEW");
            //        }
            //    }

            //    #endregion
            //    #region ///ParamTCPUDP

            //    if (HInfo.ParamList.Contains(Params.ParamTCPUDP))
            //    {
            //        try
            //        {
            //            SubParamList = ConfigFileHelper.SelectByParam(ParamList, Params.ParamTCPUDP);
            //            ///Verify Param_TCP_UDP_object here
            //            Param_TCP_UDP_object = (Param_TCP_UDP)SubParamList[0];
            //        }
            //        catch
            //        {
            //            throw new Exception("Unable to Import Param_TCP_UDP_object");
            //        }
            //    }

            //    #endregion

            //    #endregion
            //    #region MISC
            //    ///Process Param_IPV4_Object
            //    IParam tObject = null;
            //    tObject = ParamList.Find((x) => x.GetType() == typeof(Param_IPV4));
            //    if (tObject != null)
            //        Param_IPV4_object = (Param_IPV4)tObject;
            //    ///Process Param_ErrorDetail
            //    tObject = ParamList.Find((x) => x.GetType() == typeof(Param_ErrorDetail));
            //    if (tObject != null)
            //        Param_Error_Details = (Param_ErrorDetail)tObject;

            //    #endregion
            //    #region Commented_Code_Section

            //    ///Inist ParamList With
            //    ///ParamList = new List<IParam>()
            //    ///{
            //    ///   Param_CTPT_ratio_object,Param_decimal_point_object,
            //    ///   Param_customer_code_object,param_password_object,
            //    ///   Param_clock_caliberation_object,Param_energy_parameters_object,
            //    ///   Param_MDI_parameters_object,///Param_Contactor_object,
            //    ///   Param_Monitoring_time_object,Param_Limits_object,
            //    ///   Param_Limit_Demand_OverLoad_T1,Param_Limit_Demand_OverLoad_T2,
            //    ///   Param_Limit_Demand_OverLoad_T3,Param_Limit_Demand_OverLoad_T4,
            //    ///   Param_Communication_Profile_object,Param_Modem_Initialize_Object,
            //    ///   Param_ModemBasics_NEW_object,Param_ModemLimitsAndTime_Object,
            //    ///   Param_IPV4_object,Param_Keep_Alive_IP_object,tbe_obj,
            //    ///   Param_Error_Details,Param_TCP_UDP_object,obj_TBE_PowerFail
            //    ///};

            //    ///Add LoadProfileChannelInfo
            //    //ParamList.AddRange(LoadProfileChannelsInfo);
            //    /////Add Modem Parameters
            //    //ParamList.AddRange(Param_IP_Profiles_object);
            //    //ParamList.AddRange(Param_Number_Profile_object);
            //    //ParamList.AddRange(Param_Wakeup_Profile_object);
            //    //ParamList.AddRange(Param_Wakeup_Profile_object);
            //    /////Add Display Windows
            //    //ParamList.Add(Param_DisplayWindowsNormal);
            //    //ParamList.Add(Param_DisplayWindowsAlternate);
            //    //ParamList.Add(Param_DisplayWindowstest);
            //    /////Add Activity Calendar
            //    //ParamList.Add(Calendar);

            //    /////Remove Nullable ParamList
            //    //for (int index = 0; index < ParamList.Count; index++)
            //    //{
            //    //    if (ParamList[index] == null)
            //    //    {
            //    //        ParamList.Remove(ParamList[index]);
            //    //        index--;
            //    //    }
            //    //} 

            //    #endregion
            //    //}
            //    //else
            //    //    throw new Exception("Profile Loading Unsuccessful,Enter valid Path");
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(String.Format("Error Exporting Parameterization Configuration file,{0}", ex.Message), ex);
            //}
        }

        #endregion

        #region Load_All_Objects

        public void Load_and_show_Limits_Only(string Directory)
        {
            try
            {
                Modem_Warnings_disable = true;

                object[] loadedObjs = null;
                object loadedObj = null;

                loadedObj = XMLParamsProcessor.load_LimitsShowError(Directory, application_Controller.CurrentMeterName);
                if (loadedObj != null)
                    Param_Limits_Object = (Param_Limits)loadedObj;
                ///loadedObjs = XMLParamsProcessor.loadAll_Limits_Param_Limit_Demand_OverLoadShowError(Directory, application_Controller.CurrentMeterName);
                if (loadedObjs != null)
                {
                    try
                    {
                        //Param_Limit_Demand_OverLoad_T1 = ((Param_Limit_Demand_OverLoad[])loadedObjs)[0];
                        //Param_Limit_Demand_OverLoad_T2 = ((Param_Limit_Demand_OverLoad[])loadedObjs)[1];
                        //Param_Limit_Demand_OverLoad_T3 = ((Param_Limit_Demand_OverLoad[])loadedObjs)[2];
                        //Param_Limit_Demand_OverLoad_T4 = ((Param_Limit_Demand_OverLoad[])loadedObjs)[3];
                    }
                    catch (Exception) { }
                }
                InitializeUcLimitsParameters();
                ucLimits.showToGUI_Limits();
                ///Update Param Contactor
                ucContactor.Param_Contactor_object = (Param_ContactorExt)Param_Contactor_Object;
                if (ucContactor != null)
                {
                    ucContactor.Param_Monitoring_time_object = Param_Monitoring_Time_Object;
                    ucContactor.Param_Limits_object = Param_Limits_Object;
                    ucContactor.Parameterization_Limits = Limits;

                    ucContactor.showToGUI_MonitoringTime();
                    ucContactor.showToGUI_Limits();
                }
                if (ucParamSinglePhase != null)
                {
                    InitializeUcSinglePhaseParameters();
                    ucParamSinglePhase.showToGUI_Limits();
                }

                Modem_Warnings_disable = false;
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error occurred while loading and displaying Limit Parameters", ex.Message, 5000);
            }
        }

        #endregion

        public void showToGUI_ALL()
        {
            if (ucStatusWordMap1 != null)
            {
                ucStatusWordMap1.showToGUI_StatusWord(StatusWordMapType.StatusWordMap_1);
                ucStatusWordMap1.showToGUI_StatusWord(StatusWordMapType.StatusWordMap_2);
            }

            if (ucPasswords != null)
            {
                ucPasswords.Param_password_object = Param_Password_Object;
                ucPasswords.showToGUI();
            }
            if (ucCustomerReference != null)
            {
                ucCustomerReference.Param_customer_code_object = Param_Customer_Code_Object;
                ucCustomerReference.showToGUI_CustomerReference();
            }

            //load_MajorAlarm();
            if (ucCTPTRatio != null)
            {
                ucCTPTRatio.Param_CTPT_ratio_object = Param_CTPT_Ratio_Object;
                ucCTPTRatio.showToGUI_CTPTS(false);
            }
            if (ucEnergyParam != null)
            {
                ucEnergyParam.Param_energy_parameters_object = Param_Energy_Parameters_Object;
                ucEnergyParam.showToGUI_EnergyParam();
            }
            if (ucDecimalPoint != null)
            {
                ucDecimalPoint.Param_decimal_point_object = Param_Decimal_Point_Object;
                ucDecimalPoint.showToGUI_DecimalPoint();
            }

            if (ucClockCalib != null)
            {
                ucClockCalib.Param_clock_caliberation_object = Param_Clock_Caliberation_Object;
                ucClockCalib.showToGUI_Clock();
            }
            if (ucDateTime != null)
            {
                ucDateTime.Param_clock_caliberation_object = Param_Clock_Caliberation_Object;
                ucDateTime.showToGUI_Clock();
            }

            if (ucDisplayWindows1 != null)
            {
                ucDisplayWindows1.Obj_displayWindows_normal = Param_DisplayWindowsNormal;
                ucDisplayWindows1.Obj_displayWindows_alternate = Param_DisplayWindowsAlternate;
                ucDisplayWindows1.Obj_displayWindows_test = Param_DisplayWindowsTest;
                ucDisplayWindows1.showToGUI_DisplayWindows();
            }
            if (ucDisplayPowerDown1 != null)
            {
                ucDisplayPowerDown1.Obj_Param_Display_PowerDown = Param_Display_PowerDown;
                ucDisplayPowerDown1.Show_To_GUI();
            }
            if (ucGeneralProcess != null)
            {
                ucGeneralProcess.Obj_General_Process = Param_GeneralProcess;
                ucGeneralProcess.Show_To_GUI();
            }
            //Assign Param_Monitoring_time_object
            if (ucMonitoringTime != null && Param_Monitoring_Time_Object != null)
            {
                ucMonitoringTime.Param_Monitoring_time_object = Param_Monitoring_Time_Object;
                ucMonitoringTime.showToGUI_MonitoringTime();
            }
            if (ucLimits != null)
            {
                InitializeUcLimitsParameters();
                ucLimits.showToGUI_Limits();
            }
            ///Update Param Contactor
            if (ucContactor != null)
            {
                ucContactor.Param_Contactor_object = (Param_ContactorExt)Param_Contactor_Object;
                ucContactor.Param_Monitoring_time_object = Param_Monitoring_Time_Object;
                ucContactor.Param_Limits_object = Param_Limits_Object;
                ucContactor.Parameterization_Limits = Limits;


                ucContactor.showToGUI_Contactor();
                ucContactor.showToGUI_MonitoringTime();
                ucContactor.showToGUI_Limits();
            }

            if (ucParamSinglePhase != null)
            {
                InitializeUcSinglePhaseParameters();
                ucParamSinglePhase.showToGUI_MonitoringTime();
                ucParamSinglePhase.showToGUI_MDIParms();
                ucParamSinglePhase.showToGUI_Limits();
            }
            if (ucLoadProfile != null)
            {
                ucLoadProfile.LoadProfileChannelsInfo = LoadProfileChannelsInfo;
                ucLoadProfile.LoadProfileChannelsInfo_2 = LoadProfileChannelsInfo_2;
                ucLoadProfile.showToGUI_LoadProfile();
            }
            //ucLoadProfile.LoadProfilePeriod = LoadProfilePeriod;
            //ucLoadProfile.LoadProfilePeriod_2 = LoadProfilePeriod_2;
            //ucLoadProfile.PQLoadProfilePeriod = PQLoadProfilePeriod;
            ////Assign LoadProfileChannelInfo
            //if (_loadProfileChannelsInfo != null && _loadProfileChannelsInfo.Count > 0)
            //{
            //    ucLoadProfile.LoadProfileChannelsInfo = _loadProfileChannelsInfo;
            //    ucLoadProfile.LoadProfilePeriod = _loadProfileChannelsInfo[0].CapturePeriod;
            //    ///Show LoadProfile to GUI
            //    ucLoadProfile.ShowLoadProfile(_loadProfileChannelsInfo, _loadProfileChannelsInfo[0].CapturePeriod);
            //    ucLoadProfile.show
            //}

            ///Assign Param_MDI_parameters_object
            if (ucMDIParams != null && Param_MDI_Parameters_Object != null)
            {
                ucMDIParams.Param_MDI_parameters_object = Param_MDI_Parameters_Object;
                ucMDIParams.showToGUI_MDIParms();
            }
            if (ucActivityCalendar != null)
            {
                ///Show Tarrification
                ucActivityCalendar.Calendar = Calendar;
                ucActivityCalendar.showTariffication();
            }

            if (ucScheduleTableEntry1 != null)
            {
                ucScheduleTableEntry1.ShowToGUI(loadedConfigurationSet.ParamLoadShedding);
            }

            if (ucGeneratorStart1 != null)
            {

                ucGeneratorStart1.ShowToGUI(loadedConfigurationSet.ParamGeneratorStart);
            }

            //TODO: LoadParamsFromFile  -> check GUI Existance
            //??? 
            if (ucEnergyMizer1 != null)
            {
                ucEnergyMizer1.Application_Controller = this.application_Controller;
                ucEnergyMizer1.ShowToGUI_EnergyMizerParams(loadedConfigurationSet.ParamEnergyMizer);
            }

            InitializeUcModemParameters();
            ///Show ALL MODEM
            showToGUI_ALL_Modem();
            showToGUI_ALL_StandardModem();
            InitializeUcTimeWindowParameters();
            ShowtoGUI_TimeBaseEvents();
        }

        public void Load_and_show_all(string Directory)
        {
            try
            {
                Modem_Warnings_disable = true;
                ///Load_All_Params(Directory);
                ///Load_All_Params(Directory);

                string meter_Model = Single_Phase_Model;
                if (application_Controller != null && !String.IsNullOrEmpty(application_Controller.CurrentMeterName))
                    meter_Model = application_Controller.CurrentMeterName;

                loadedConfigurationSet = XMLParamsProcessor.Import_AllParams(Directory, meter_Model, loadedConfigurationSet);
                application_Controller.ParameterConfigurationSet = loadedConfigurationSet;
                ///String ConfURL = Directory + "\\ParameterExport\\Parameters.conf";
                ///ConfigFileList =  XMLParamsProcessor.Import_All_Params(ConfURL, ConfigFileList);
                showToGUI_ALL();
                Modem_Warnings_disable = false;
                ///showToGUI_MajorAlarm();
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error occurred while parameters loading and displaying Parameters", ex.Message, 5000);
            }
        }

        public void saveToDatabase_ALL()
        {
            try
            {
                string msn = Application_Controller.ConnectionManager.ConnectionInfo.MSN;
                ///Save Parameters for selected Categories
                if (CustGetDialog.check_ActivityCalender.Checked == true)
                {
                    dbController.saveActivityCalendar(Calendar);
                }
                if (CustGetDialog.check_Clock.Checked == true)
                {
                    dbController.saveClock(Param_Clock_Caliberation_Object);
                }
                if (CustGetDialog.check_Contactor.Checked)
                {
                    dbController.saveContactor(Param_Contactor_Object);
                }
                if (CustGetDialog.check_CTPT.Checked)
                {
                    dbController.saveCTPT(Param_CTPT_Ratio_Object);
                }
                if (CustGetDialog.check_customerReference.Checked)
                {
                    dbController.saveCustomerRef(Param_Customer_Code_Object, Param_Customer_Code_Object.Customer_Name,
                        Param_Customer_Code_Object.Customer_Address);
                }
                if (CustGetDialog.check_DataProfilewithEvents.Checked == true)
                {
                    ///Save DataProfileWithEvents
                }
                if (CustGetDialog.check_DecimalPoint.Checked)
                {
                    dbController.saveDecimalPoint(Param_Decimal_Point_Object);
                }
                if (CustGetDialog.check_DisplayWindows_Nor.Checked == true)
                {
                    dbController.saveDisplayWindow_Normal(ucDisplayWindows1.Obj_displayWindows_normal);
                }
                if (CustGetDialog.check_DisplayWindows_Alt.Checked == true)
                {
                    dbController.saveDisplayWindow_Alternate(ucDisplayWindows1.Obj_displayWindows_alternate);
                }
                if (CustGetDialog.check_DisplayWindows_test.Checked == true)
                {

                }
                if (CustGetDialog.check_DisplayPowerDown.Checked == true)
                {
                    dbController.saveDisplayPowerDown(Param_Display_PowerDown);
                }
                if (CustGetDialog.check_EnergyParams.Checked)
                {
                    ///Save Energy Params
                }
                //if (CustGetDialog.check_EventCaution.Checked) GET_EventCautons();
                if (CustGetDialog.check_IPV4.Checked)
                {
                }
                if (CustGetDialog.check_Limits.Checked)
                {
                    dbController.saveLimits(Param_Limits_Object);
                }
                if (CustGetDialog.chk_LoadProfile.Checked || CustGetDialog.chk_LoadProfile_Interval.Checked)
                {
                    dbController.saveLoadProfileChannels(LoadProfileChannelsInfo);
                }
                if (CustGetDialog.check_MajorAlarmprofile.Checked == true)
                {
                }
                if (CustGetDialog.check_MDI_params.Checked)
                {
                    dbController.saveMDIparams(Param_MDI_Parameters_Object);
                }
                if (CustGetDialog.check_MonitoringTime.Checked)
                {
                    dbController.saveMonitoringTime(Param_Monitoring_Time_Object);
                }
                if (CustGetDialog.check_Password_Elec.Checked)
                {
                }
                if (CustGetDialog.check_TCPUDP.Checked)
                {
                }
                if (CustGetDialog.check_Time.Checked)
                {
                }
                if (CustGetDialog.check_IP_Profile.Checked)
                {
                    int id = dbController.saveModem(Application_Controller.ConnectionManager.ConnectionInfo.MSN);
                    dbController.saveIPProfile(id, Param_IP_Profiles_object);
                }
                if (CustGetDialog.chbWakeupProfile.Checked)
                {
                    int id = dbController.saveModem(Application_Controller.ConnectionManager.ConnectionInfo.MSN);
                    dbController.saveWakeupProfile(id, Param_Wakeup_Profile_object);

                }
                if (CustGetDialog.chbNumberProfile.Checked)
                {
                    int id = dbController.saveModem(Application_Controller.ConnectionManager.ConnectionInfo.MSN);
                    dbController.saveNumberProfile(id, Param_Number_Profile_object);
                }
                if (CustGetDialog.chbCommunicationProfile.Checked)
                {
                    int id = dbController.saveModem(Application_Controller.ConnectionManager.ConnectionInfo.MSN);
                    dbController.saveCommProfile(id, Param_Communication_Profile_object);
                }
                if (CustGetDialog.chbKeepAlive.Checked)
                {
                    int id = dbController.saveModem(Application_Controller.ConnectionManager.ConnectionInfo.MSN);
                    dbController.saveKeepAlive(id, Param_Keep_Alive_IP_object);
                }
                if (CustGetDialog.chbModemLimitsAndTime.Checked)
                {
                    int id = dbController.saveModem(Application_Controller.ConnectionManager.ConnectionInfo.MSN);

                    dbController.saveModemLimitsAndTime(id, Param_ModemLimitsAndTime_Object);

                }
                if (CustGetDialog.chbModemInitialize.Checked)
                {
                    int id = dbController.saveModem(Application_Controller.ConnectionManager.ConnectionInfo.MSN);
                    dbController.saveModemInitialze(id, Param_Modem_Initialize_Object, Param_ModemBasics_NEW_object);
                }
                if (CustGetDialog.check_TBEs.Checked)
                {
                    #region ///here sync from ParamTimeBaseEvent Object

                    if (tbe_obj == null)
                        tbe_obj = new TBE();
                    ///Sync ParamTimeBaseEvent1
                    tbe_obj.ControlEnum_Tbe1 = TBE1.Control_Enum;
                    tbe_obj.Tbe1_interval = TBE1.Interval;
                    tbe_obj.Tbe1_datetime = TBE1.DateTime;
                    ///Sync ParamTimeBaseEvent2
                    tbe_obj.ControlEnum_Tbe2 = TBE2.Control_Enum;
                    tbe_obj.Tbe2_interval = TBE2.Interval;
                    tbe_obj.Tbe2_datetime = TBE2.DateTime;

                    #endregion
                    dbController.saveTBEs(tbe_obj, obj_TBE_PowerFail);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void showToGUI_ALL_Modem()
        {
            Modem_Warnings_disable = true;

            #region Assign_Variables

            /////Initialize ucIPProfiles
            //ucIPProfiles.Param_IP_ProfilesHelper = Param_IP_ProfilesHelperObj;
            //ucIPProfiles.Param_IP_Profiles_object = Param_IP_Profiles_object;
            //ucIPProfiles.Param_IPV4_object = Param_IPV4_object;
            //ucIPProfiles.Param_WakeUp_ProfileHelperObj = Param_WakeUp_ProfileHelperObj;
            /////Initialize ucWakeupProfiles
            //ucWakeupProfile.Param_IP_ProfilesHelper = Param_IP_ProfilesHelperObj;
            //ucWakeupProfile.Param_WakeUp_ProfileHelperObj = Param_WakeUp_ProfileHelperObj;
            //ucWakeupProfile.Param_Wakeup_Profile_object =
            //    Param_WakeUp_ProfileHelperObj.Param_WakeUp_Profile_object;
            //ucWakeupProfile.Param_Communication_Profile_object = Param_Communication_Profile_object;
            //ucWakeupProfile.Param_Number_ProfileHelperObj = Param_Number_ProfileHelperObj;
            //ucWakeupProfile.Param_Keep_Alive_IP_object = Param_Keep_Alive_IP_object;
            /////Initialize ucNumberProfiles
            //ucNumberProfile.Param_Number_Profile_object =
            //    Param_Number_ProfileHelperObj.Param_Number_Profiles_object;
            //ucNumberProfile.Param_Number_ProfileHelperObj = Param_Number_ProfileHelperObj;
            //ucNumberProfile.Param_Communication_Profile_object = Param_Communication_Profile_object;
            //ucNumberProfile.Param_WakeUp_ProfileHelperObj = Param_WakeUp_ProfileHelperObj;
            /////Initialize ucCommunicationProfiles
            //ucCommProfile.Param_Communication_Profile_object = Param_Communication_Profile_object;
            //ucCommProfile.Param_Number_ProfileHelperObj = Param_Number_ProfileHelperObj;
            //ucCommProfile.Param_WakeUp_ProfileHelperObj = Param_WakeUp_ProfileHelperObj;
            /////Initialize ucKeepAliveProfiles
            //ucKeepAlive.Param_Keep_Alive_IP_object = Param_Keep_Alive_IP_object;
            //ucKeepAlive.Param_WakeUp_ProfileHelperObj = Param_WakeUp_ProfileHelperObj;
            /////Initialize ucModemLimitsAndTime
            //ucModemLimitsAndTime.Param_ModemLimitsAndTime_Object = Param_ModemLimitsAndTime_Object;
            /////Initialize ucModemInitialize
            //ucModemInitialize.Param_Modem_Initialize_Object = Param_Modem_Initialize_Object;
            //ucModemInitialize.Param_ModemBasics_NEW_object = Param_ModemBasics_NEW_object;

            #endregion
            if (ucIPProfiles != null)
            {
                ucIPProfiles.showToGUI_IPProfile();
                ucIPProfiles.showToGUI_IPV4();
            }
            if (ucWakeupProfile != null)
            {
                ucWakeupProfile.showToGUI_WakeUpProfile();
            }
            if (ucCommProfile != null)
            {
                ucCommProfile.showToGUI_CommunicationProfile();
            }
            if (ucKeepAlive != null)
            {
                ucKeepAlive.showToGUI_KeepAlive();
            }
            if (ucModemLimitsAndTime != null)
            {
                ucModemLimitsAndTime.showToGUI_ModemLimitsAndTime();
            }
            if (ucModemInitialize != null)
            {
                ucModemInitialize.showToGUI_ModemInitialize();
            }

            if (ucNumberProfile != null) //Azeem //Fix Number profile display after Get
                ucNumberProfile.showToGUI_NumberProfile();

            Application.DoEvents();
            Modem_Warnings_disable = false;
        }

        public void SHOWModem(bool ISModem)
        {
            if (ISModem)
            {
                Tab_Main.SelectedIndex = (Tab_Main.TabCount > 0) ? 1 : 0;
            }
            else
            {
                Tab_Main.SelectedIndex = (Tab_Main.TabCount > 0) ? 0 : -1;
            }
        }

        ///===============================================================================================
        ///===============================================================================================
        ///===============================================================================================

        #region GET_Params_BackgroundWorker_Handlers

        private void btn_GETAsync_Parameters_Click(Object sender, EventArgs e)
        {
            try
            {
                ///Not Connected Properly
                if (!(Application_Process.Is_Association_Developed))
                {
                    Notification n = new Notification("Disconnected", "Create Association to Meter");
                    return;
                }

                //By Azeem
                if (Application_Controller.CurrentUser != null)
                {
                    CustGetDialog = new CustomSetGet("Get Parameters", application_Controller.isSinglePhase, Application_Controller.CurrentUser.CurrentAccessRights, 1);
                }
                else
                {
                    CustGetDialog = new CustomSetGet("Get Parameters.", application_Controller.isSinglePhase);
                    CustGetDialog.IsReadMode = 1;
                }
                //if (Application_Controller.CurrentUser != null)
                //    CustGetDialog.AccessRights = Application_Controller.CurrentUser.CurrentAccessRights;


                //CustGetDialog.AccessRights = AccessRights_R326;
                ///Disable Check Password
                CustGetDialog.check_Password_Elec.Enabled = false;
                if (CustGetDialog.ShowDialog(this.Parent) == DialogResult.OK && CustGetDialog.IsAnyChecked)
                {
                    this.Parameterization_BckWorkerThread.DoWork += new DoWorkEventHandler(BckWorker_GETParams_DoEventHandler);
                    this.Parameterization_BckWorkerThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BckWorker_GETParam_WorkCompleted);
                    this.Parameterization_BckWorkerThread.ProgressChanged += new ProgressChangedEventHandler(BckWorker_GETParam_ProgressChanged);
                    dlg = new ProgressDialog();
                    Param_Controller.ParameterGetStatus += dlg.ConnController_ProcessStatusHandler;

                    //dlg.btnCancel.Click += new EventHandler(btn_CancelGETAsync_Parameters_Click);
                    //dlg.btnCancel.Enabled = true;

                    dlg.EnableProgressBar = true;
                    dlg.Text = "Reading Parameters";
                    dlg.DialogTitle = "Reading Parameters";

                    if (!Parameterization_BckWorkerThread.IsBusy)
                    {
                        Application_Controller.IsIOBusy = true;
                        dlg.okButton.Visible = false;
                        Parameterization_BckWorkerThread.RunWorkerAsync();
                        ///Disable Meter Passwords
                        dlg.ShowDialog(this.Parent);
                        Application.DoEvents();
                        dlg.ConnController_ProcessStatusHandler("Parameterization Read Process is started");
                    }
                }
                else
                    return;
            }
            catch (Exception ex)
            {
                String ErrorMsg = string.Format("Error Reading Parameters {0},Details{1}", ex.Message, (ex.InnerException == null) ? null : ex.InnerException.Message);
                MessageBox.Show(ErrorMsg, "Error Reading Parameters", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }

        private void btn_CancelGETAsync_Parameters_Click(Object sender, EventArgs e)
        {
            try
            {
                Parameterization_BckWorkerThread.CancelAsync();
            }
            catch (Exception ex)
            {
                String ErrorMsg = string.Format("Error Reading Parameters {0},Details{1}", ex.Message, (ex.InnerException == null) ? null : ex.InnerException.Message);
                MessageBox.Show(ErrorMsg, "Error Reading Parameters", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }

        private void BckWorker_GETParams_DoEventHandler(Object sender, DoWorkEventArgs arg)
        {
            try
            {
                KharabParams = "";
                SabSet = true;
                Param_Controller.ParametersGETStatus.ResetCommandStatus();
                dlg.ConnController_ProcessStatusHandler("Parameter Read Process is started");

                dbController.createSession(Application_Controller.ConnectionManager.ConnectionInfo.MSN,
                                           Application_Controller.Applicationprocess_Controller.UserId.ToString());
                if (CustGetDialog.check_ActivityCalender.Checked)//  && CustGetDialog.check_ActivityCalender.Visible)
                {
                    try
                    {
                        GET_ActivityCalendar();
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "Activity Calendar \r\n";
                    }
                }
                if (CustGetDialog.check_Clock.Checked)//    && CustGetDialog.check_Clock.Visible)
                {
                    try
                    {
                        GET_Clock();
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "Clock \r\n";
                    }
                }
                if (CustGetDialog.check_Contactor.Checked)//   && CustGetDialog.check_Contactor.Visible)
                {
                    try
                    {
                        GET_Contactor();
                    }
                    catch (Exception ex)
                    {
                        SabSet = false;
                        KharabParams += $"Contactor \r\n{ex.Message}";
                    }
                }
                if (CustGetDialog.check_CTPT.Checked)//   && CustGetDialog.check_CTPT.Visible)
                {
                    try
                    {
                        GET_CTPT();
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "CTPT \r\n";
                    }
                }
                if (CustGetDialog.check_customerReference.Checked)//   && CustGetDialog.check_customerReference.Visible)
                {
                    try
                    {
                        GET_CustomerReference();
                        GET_CustomerName();
                        GET_CustomerAddress();
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "Customer Reference \r\n";
                    }
                }
                if (CustGetDialog.check_DataProfilewithEvents.Checked && CustGetDialog.check_DataProfilewithEvents.Visible)
                {
                    try
                    {
                        GET_DataProfilesWithEvents();
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "DataPrfile with Events \r\n";
                    }
                }
                if (CustGetDialog.check_DecimalPoint.Checked)//   && CustGetDialog.check_DecimalPoint.Visible)
                {
                    try
                    {
                        GET_DecimalPoint();
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "Decimal Point \r\n";
                    }
                }

                if (CustGetDialog.chbStatusWordMap1.Checked)//   && CustGetDialog.chbStatusWordMap1.Visible)
                {
                    try
                    {
                        GET_StatusWordMap(StatusWordMapType.StatusWordMap_1);
                    }
                    catch (Exception)
                    {

                        SabSet = false;
                        KharabParams += "Status Word Map 1\r\n";
                    }
                }
                if (CustGetDialog.chbStatusWordMap2.Checked)//   && CustGetDialog.chbStatusWordMap2.Visible)
                {
                    try
                    {
                        GET_StatusWordMap(StatusWordMapType.StatusWordMap_2);
                    }
                    catch (Exception)
                    {

                        SabSet = false;
                        KharabParams += "Status Word Map 2\r\n";
                    }
                }
                //if (CustGetDialog.check_DisplayWindows_Nor.Checked == true) GET_DisplayWindows();
                if (CustGetDialog.check_DisplayWindows_Nor.Checked)//   && CustGetDialog.check_DisplayWindows_Nor.Visible)
                {
                    try
                    {
                        GET_DisplayWindows_Nor();
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "Display Windows Normal \r\n";
                    }
                }
                if (CustGetDialog.check_DisplayWindows_Alt.Checked)//   && CustGetDialog.check_DisplayWindows_Alt.Visible)
                {
                    try
                    {
                        GET_DisplayWindows_Alt();
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "Display Window Alternate \r\n";
                    }
                }

                if (CustGetDialog.check_DisplayWindows_test.Checked)//   && CustGetDialog.check_DisplayWindows_test.Visible)
                {
                    try
                    {
                        GET_DisplayWindows_Test();
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "Display WIndow Test mode \r\n";
                    }
                }
                if (CustGetDialog.check_DisplayPowerDown.Checked)//   && CustGetDialog.check_DisplayPowerDown.Visible)
                {
                    try
                    {
                        GET_DisplayWindowsPowerDown();
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "Display Power Down Mode \r\n";
                    }
                }
                if (CustGetDialog.check_EnergyParams.Checked)//   && CustGetDialog.check_EnergyParams.Visible)
                {
                    try
                    {
                        GET_EnergyParam();
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "Energy Params \r\n";
                    }
                }
                //if (CustGetDialog.check_EventCaution.Checked) GET_EventCautons();
                if (CustGetDialog.check_IPV4.Checked && CustGetDialog.check_IPV4.Visible)
                {
                    try
                    {
                        GET_IPV4();
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "IPV4 \r\n";
                    }
                }
                if (CustGetDialog.check_Limits.Checked)//    && CustGetDialog.check_Limits.Visible)
                {
                    try
                    {
                        GET_Limits();
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "Limits \r\n";
                    }
                }
                if ((CustGetDialog.chk_LoadProfile.Checked) ||//   && CustGetDialog.chk_LoadProfile.Visible) || 
                    (CustGetDialog.chk_LoadProfile_Interval.Checked && CustGetDialog.chk_LoadProfile_Interval.Visible))
                {
                    try
                    {
                        GET_LoadProfileChannels(LoadProfileScheme.Load_Profile);
                    }
                    catch (Exception)
                    {

                        SabSet = false;
                        KharabParams += "Load Profile Channels \r\n";
                    }
                }

                if ((CustGetDialog.chk_LoadProfile_2.Checked) || //   && CustGetDialog.chk_LoadProfile_2.Visible) || 
                   (CustGetDialog.chk_LoadProfile_2_Interval.Checked)) //   && CustGetDialog.chk_LoadProfile_2_Interval.Visible))
                {
                    try
                    {
                        GET_LoadProfileChannels(LoadProfileScheme.Load_Profile_Channel_2);
                    }
                    catch (Exception)
                    {

                        SabSet = false;
                        KharabParams += "Load Profile Channels \r\n";
                    }
                }

                if (CustGetDialog.chk_PQ_LoadProfileInterval.Checked)//   && CustGetDialog.chk_PQ_LoadProfileInterval.Visible)
                {
                    try
                    {
                        GET_LoadProfileInterval(LoadProfileScheme.Daily_Load_Profile);
                    }
                    catch (Exception)
                    {

                        SabSet = false;
                        KharabParams += "Load Profile Interval \r\n";
                    }
                }
                if (CustGetDialog.check_MajorAlarmprofile.Checked && CustGetDialog.check_MajorAlarmprofile.Visible)
                {
                    try
                    {
                        GET_MajorAlarmProfile();
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "Major Alarm Profile \r\n";
                    }
                }
                if (CustGetDialog.check_MDI_params.Checked)//  && CustGetDialog.check_MDI_params.Visible)
                {
                    try
                    {
                        GET_MDIParams();
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "MDI Params \r\n";
                    }
                }
                if (CustGetDialog.check_MonitoringTime.Checked)//  && CustGetDialog.check_MonitoringTime.Visible)
                {
                    try
                    {
                        GET_MonitoringTime();
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "Monitoring Time \r\n";
                    }
                }
                if (CustGetDialog.chk_GPP.Checked)//  && CustGetDialog.chk_GPP.Visible)
                {
                    try
                    {
                        GET_GeneralProcess();
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "General Process \r\n";
                    }
                }
                //if (CustGetDialog.check_Password_Elec.Checked)
                //{
                //    try
                //    {
                //        GET_Passwords();
                //    }
                //    catch (Exception)
                //    {
                //        SabSet = false;
                //        KharabParams += "Passwords \r\n";
                //    }
                //}
                if (CustGetDialog.check_TCPUDP.Checked && CustGetDialog.check_TCPUDP.Visible)
                {
                    try
                    {
                        GET_TCPUDP();
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "TCP UDP \r\n";
                    }
                }
                if (CustGetDialog.check_Time.Checked)//   && CustGetDialog.check_Time.Visible)
                {
                    try
                    {
                        GET_Clock_Only();
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "Time \r\n";
                    }
                }
                if (CustGetDialog.check_IP_Profile.Checked)//   && CustGetDialog.check_IP_Profile.Visible)
                {
                    try
                    {

                        GET_IPProfile();

                    }
                    catch (Exception)
                    {

                        SabSet = false;
                        KharabParams += "Modem Parameters IP Profile \r\n";
                    }
                }
                if (CustGetDialog.chbWakeupProfile.Checked)//  && CustGetDialog.chbWakeupProfile.Visible)
                {
                    try
                    {
                        GET_WakeupProfile();

                    }
                    catch (Exception)
                    {

                        SabSet = false;
                        KharabParams += "Modem Parameters WakeUp Profile\r\n";
                    }
                }

                if (CustGetDialog.chbNumberProfile.Checked)//  && CustGetDialog.chbNumberProfile.Visible)
                {
                    try
                    {

                        GET_NumberProfile();

                    }
                    catch (Exception)
                    {

                        SabSet = false;
                        KharabParams += "Modem Parameters Number Profile\r\n";
                    }
                }
                if (CustGetDialog.chbKeepAlive.Checked)//   && CustGetDialog.chbKeepAlive.Visible)
                {
                    try
                    {
                        GET_KeepAlive();

                    }
                    catch (Exception)
                    {

                        SabSet = false;
                        KharabParams += "Modem Parameters Keep Alive\r\n";
                    }
                }

                if (CustGetDialog.chbModemLimitsAndTime.Checked)//  && CustGetDialog.chbModemLimitsAndTime.Visible)
                {
                    try
                    {

                        GET_ModemLimitsAndTime();

                    }
                    catch (Exception)
                    {

                        SabSet = false;
                        KharabParams += "Modem Parameters Limits and Time\r\n";
                    }
                }
                if (CustGetDialog.chbModemInitialize.Checked)//  && CustGetDialog.chbModemInitialize.Visible)
                {
                    try
                    {

                        GET_ModemInitialize();

                    }
                    catch (Exception)
                    {

                        SabSet = false;
                        KharabParams += "Modem Parameters Modem Initialize\r\n";
                    }
                }
                if (CustGetDialog.chbCommunicationProfile.Checked)//  && CustGetDialog.chbCommunicationProfile.Visible)
                {
                    try
                    {
                        GET_CommunicationProfile();

                    }
                    catch (Exception)
                    {

                        SabSet = false;
                        KharabParams += "Modem Parameters Communication Profile\r\n";
                    }
                }
                if (CustGetDialog.check_StandardModem_IP_Profile.Checked)//  && CustGetDialog.check_StandardModem_IP_Profile.Visible)
                {
                    try
                    {

                        GET_StandardIPProfile();

                    }
                    catch (Exception)
                    {

                        SabSet = false;
                        KharabParams += "Modem Parameters Standard IP Profile \r\n";
                    }
                }
                if (CustGetDialog.check_StandardModem_Number_Profile.Checked)//  && CustGetDialog.check_StandardModem_Number_Profile.Visible)
                {
                    try
                    {

                        GET_StandardNumberProfile();

                    }
                    catch (Exception)
                    {

                        SabSet = false;
                        KharabParams += "Modem Parameters Standard Number Profile\r\n";
                    }
                }
                if (CustGetDialog.check_StandardModem_KeepAlive.Checked)//  && CustGetDialog.check_StandardModem_KeepAlive.Visible)
                {
                    try
                    {
                        GET_KeepAlive_Standard();

                    }
                    catch (Exception)
                    {

                        SabSet = false;
                        KharabParams += "Modem Parameters Keep Alive\r\n";
                    }
                }
                if (CustGetDialog.check_TBEs.Checked)//   && CustGetDialog.check_TBEs.Visible)
                {
                    try
                    {
                        //Checks added in v4.8.16
                        bool AnyVisible = false;

                        if (ucTimeWindowParam1.Visible)
                        {
                            AnyVisible = true;
                            GET_Time_Based_Event_1();
                        }
                        if (ucTimeWindowParam2.Visible)
                        {
                            AnyVisible = true;
                            GET_Time_Based_Event_2();
                        }

                        if (AnyVisible)
                        {
                            var _obj_TBE_PowerFail = obj_TBE_PowerFail;
                            Application_Controller.Param_Controller.GET_TBE_PowerFAil(ref _obj_TBE_PowerFail);
                            obj_TBE_PowerFail = _obj_TBE_PowerFail;
                        }
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "Time Based Events \r\n";
                    }
                }
                if (CustGetDialog.check_loadShedding.Checked)//  && CustGetDialog.check_loadShedding.Visible)
                {
                    try
                    {
                        GET_SchedulerTable();
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "Scheduler Table \r\n";
                    }
                }
                if (CustGetDialog.check_GeneratorStart.Checked)//  && CustGetDialog.check_GeneratorStart.Visible)
                {
                    try
                    {
                        GET_GeneratorStart();
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "Generator Start \r\n";
                    }
                }
                //get customer code
                try
                {
                    //Application_Controller.Param_Controller.GET_Customer_Reference(ref obj_CustomerCode); //Added by Azeem
                    //Application_Controller.Param_Controller.GET_Customer_Name(ref obj_CustomerCode); //Added by Azeem
                    //Application_Controller.Param_Controller.GET_Customer_Address(ref obj_CustomerCode); //Added by Azeem
                }
                catch (Exception)
                {
                }

                GetCompleted = true;
                if (!this.HidePrintReportButtons)
                {
                    try
                    {
                        //Verify Either Active Season Object Exists in meter
                        Base_Class obj = Application_Controller.Applicationprocess_Controller.GetSAPEntry(Get_Index.Active_Season);
                        if (obj.IsAttribReadable(0x02))
                        {
                            Application_Controller.InstantaneousController.Get_Active_Season(Instantaneous_Class_obj);
                            if (ucDisplayWindows1 != null)
                                ucDisplayWindows1.ActiveSeason = Instantaneous_Class_obj.Active_Season.ToString();
                        }
                        else
                        {
                            //Set Default Active Season 0
                            if (ucDisplayWindows1 != null)
                                ucDisplayWindows1.ActiveSeason = "0";
                        }
                    }
                    catch
                    {
                        //Set Default Active Season 0
                        ucDisplayWindows1.ActiveSeason = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                GetCompleted = false;
            }

        }

        private void BckWorker_GETParam_WorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    throw e.Error;
                }
                else if (e.Cancelled)
                {
                    Notification Notifier = new Notification("Process Aborted", "Parameterization Read Process Is Cancel by User");
                    //MessageBox.Show("Parameterization Read Process Is Cancel by User", "Process Aborted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    try
                    {
                        //saveToDatabase_ALL(); //No Use of Saving data so skipped for time being
                    }
                    catch (Exception ex)
                    {
                        Notification notifier = new Notification("Error Saving Meter Configurations", "Error occured while save meter configurations,\r\n" + ex.Message, 5000);
                        ///***modification
                        Console.Out.WriteLine("Error Saving Configuration " + ex.ToString());
                        MessageBox.Show(ex.Message); //v4.8.35 temp for debugging
                    }
                    ///Show Parameters GET Process
                    showToGUI_ALL();
                    Application.DoEvents();

                    String ParamStatistics = null;
                    ParamStatistics = Param_Controller.ParametersGETStatus.BuildParameterReadStatistic();
                    String ParameterReadSummary = Param_Controller.ParametersGETStatus.BuildParamterReadLog(DecodingResult.Ready, false);
                    ///String ParamSummary = Param_Controller.ParametersSETStatus.BuildParamterizationLog(Data_Access_Result.Success, false);
                    ///String MessageContent = String.Format("{0}", ParamSummary);
                    String MessageContent;
                    if (SabSet)
                        MessageContent = String.Format("{0}\r\n\r\nSummary\r\n\r\n{1}",
                            "Parameter Read Process Completed",
                            ParameterReadSummary);
                    else
                        MessageContent = String.Format("{0}\r\n\r\nSummary\r\n\r\n{1}",
                            "Parameter Read Process Completed",
                            "The following parameters were not read successfully \r\n " + KharabParams);

                    Thread.Sleep(250);

                    int copy_text_index = ParameterReadSummary.IndexOf('\r', 0);
                    string msgtoShow = "";
                    if (copy_text_index != -1)
                    {
                        //msgtoShow = ParamSummary.Substring(0, copy_text_index);
                        msgtoShow = "";
                    }
                    else
                    {
                        msgtoShow = ParameterReadSummary;
                    }
                    Notification Notifier;
                    if (GetCompleted && SabSet)
                        Notifier = new Notification("Process Completed", msgtoShow);
                    else
                        Notifier = new Notification("Error", "Error reading parameterization");
                    dlg.ConnController_ProcessStatusHandler(MessageContent);
                }
            }
            catch (Exception ex)
            {
                String _txt = String.Format("Error occurred while reading Parameters,Details:\r\n{0} {1}",
                    ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : "");
                //MessageBox.Show(_txt, "Error reading Parameters", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressDialog.UpdateDialogStatusHandler(_txt);
                Notification Notifier = new Notification("Error", "Error occurred while reading Parameters");
            }
            finally
            {
                this.Parameterization_BckWorkerThread.DoWork -= new DoWorkEventHandler(BckWorker_GETParams_DoEventHandler);
                this.Parameterization_BckWorkerThread.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(BckWorker_GETParam_WorkCompleted);
                this.Parameterization_BckWorkerThread.ProgressChanged -= new ProgressChangedEventHandler(BckWorker_GETParam_ProgressChanged);
                Param_Controller.ParameterGetStatus -= dlg.ConnController_ProcessStatusHandler;
                //dlg.btnCancel.Click -= new EventHandler(btn_CancelGETAsync_Parameters_Click);
                Application_Controller.IsIOBusy = false;
                dlg.EnableProgressBar = false;
                dlg.okButton.Visible = true;
                //dlg.btnCancel.Enabled = false;
            }
        }

        private void BckWorker_GETParam_ProgressChanged(object sender, ProgressChangedEventArgs e) { }

        #endregion

        #region Get_Parameters

        public void GET_Time_Based_Event_1()
        {
            Param_Controller.ParametersGETStatus.BuildStatusCollection("Time Based Event 1");
            var _TBE1 = TBE1;
            Param_Controller.GET_TimeBaseEvents(ref _TBE1, Get_Index._Time_Based_Event_1);
            TBE1 = _TBE1;
        }

        public void GET_Time_Based_Event_2()
        {
            Param_Controller.ParametersGETStatus.BuildStatusCollection("Time Based Event 2");
            var _TBE2 = TBE2;
            Param_Controller.GET_TimeBaseEvents(ref _TBE2, Get_Index._Time_Based_Event_2);
            TBE2 = _TBE2;
        }

        public void GET_ActivityCalendar()
        {
            try
            {
                Param_Controller.ParametersGETStatus.BuildStatusCollection("Tariffication");
                Param_ActivityCalendar Calendar_Receive = new Param_ActivityCalendar();
                Param_Controller.GET_ActivityCalendar_Complete(ref Calendar_Receive);
                Calendar = Calendar_Receive;

                Application.DoEvents();

                ///Profiles.SelectedIndex = 0;

                //if (ucActivityCalendar.IsProgramDateTimeNow)
                //{
                //StDateTime date = new StDateTime();
                //date.SetDateTime(DateTime.Now);
                //Calendar.CalendarstartDate = date;
                //}
                ///***modified
                ///dbController.saveActivityCalendar(Calendar);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error Getting Activity Calendar" + Ex.Message); //v4.8.30
                return;
            }
        }

        public void GET_CTPT()
        {
            Param_Controller.ParametersGETStatus.BuildStatusCollection("CT PT");

            var _Param_CTPT_Ratio_Object = Param_CTPT_Ratio_Object;
            Param_CTPT_Ratio_Object.CTratio_Numerator = (ushort)Param_Controller.GET_Any(ref _Param_CTPT_Ratio_Object, Get_Index.CT_Ratio_Numerator, 2, 1);
            Param_CTPT_Ratio_Object.CTratio_Denominator = (ushort)Param_Controller.GET_Any(ref _Param_CTPT_Ratio_Object, Get_Index.CT_Ratio_Denominator, 2, 1);
            Param_CTPT_Ratio_Object.PTratio_Numerator = (ushort)Param_Controller.GET_Any(ref _Param_CTPT_Ratio_Object, Get_Index.PT_Ratio_Numerator, 2, 1);
            Param_CTPT_Ratio_Object.PTratio_Denominator = (ushort)Param_Controller.GET_Any(ref _Param_CTPT_Ratio_Object, Get_Index.PT_Ratio_Denominator, 2, 1);
            Param_CTPT_Ratio_Object = _Param_CTPT_Ratio_Object;

            ///dbController.saveCTPT(Param_CTPT_ratio_object);
        }

        public void GET_Clock()
        {
            Param_Controller.ParametersGETStatus.BuildStatusCollection("Meter Time Caliberation");
            var _Param_Clock_Caliberation_Object = Param_Clock_Caliberation_Object;
            ///Param_Controller.GET_Meter_Clock(ref _Param_Clock_Caliberation_Object);
            ///Get Meter Clock Only
            Param_Controller.GET_Clock_Calib_PPM(ref _Param_Clock_Caliberation_Object);
            Param_Controller.GET_Clock_Calib_Flags(ref _Param_Clock_Caliberation_Object);
            ///Param_Controller.GET_MeterClock_DaylightSaving(ref _Param_Clock_Caliberation_Object);
            ///Param_Controller.GET_MeterClock_DLS_BEGIN_Date_Time(ref _Param_Clock_Caliberation_Object);
            ///Param_Controller.GET_MeterClock_DLS_Deviation(ref _Param_Clock_Caliberation_Object);
            ///Param_Controller.GET_MeterClock_DLS_Enable(ref _Param_Clock_Caliberation_Object);
            ///Param_Controller.GET_MeterClock_DLS_END_Date_Time(ref _Param_Clock_Caliberation_Object);
            ///showToGUI_Clock();

            ///***modified
            ///dbController.saveClock(_Param_Clock_Caliberation_Object);
            Param_Clock_Caliberation_Object = _Param_Clock_Caliberation_Object;
        }

        public void GET_EventDetails()
        {
            Param_Controller.ParametersGETStatus.BuildStatusCollection("Event Details");
            Param_Controller.Get_EventDetails(ref Param_Error_Details);
        }

        public void GET_Clock_Only()
        {
            Param_Controller.ParametersGETStatus.BuildStatusCollection("Meter Real Time Clock");
            var _Param_Clock_Caliberation_Object = Param_Clock_Caliberation_Object;
            ///Param_Controller.GET_Meter_Clock(ref _Param_Clock_Caliberation_Object);
            ///Get Meter Clock Only
            Param_Controller.GET_MeterClock_Date_Time(ref _Param_Clock_Caliberation_Object);
            ///Param_Controller.GET_MeterClock_Date_Time(ref _Param_Clock_Caliberation_Object);
            //Param_Controller.GET_MeterClock_DaylightSaving(ref _Param_Clock_Caliberation_Object);
            //Param_Controller.GET_MeterClock_DLS_BEGIN_Date_Time(ref _Param_Clock_Caliberation_Object);
            //Param_Controller.GET_MeterClock_DLS_Deviation(ref _Param_Clock_Caliberation_Object);
            //Param_Controller.GET_MeterClock_DLS_Enable(ref _Param_Clock_Caliberation_Object);
            //Param_Controller.GET_MeterClock_DLS_END_Date_Time(ref _Param_Clock_Caliberation_Object);
            ///showToGUI_Clock();
            ///
            ///***modified
            ///dbController.saveClock(Param_clock_caliberation_object);
            Param_Clock_Caliberation_Object = _Param_Clock_Caliberation_Object;
        }

        public void GET_Contactor()
        {
            Param_Controller.ParametersGETStatus.BuildStatusCollection("Contactors");
            Param_Contactor _Param_Contactor_Object = Param_Contactor_Object;
            Param_Controller.GET_ContactorParams(ref _Param_Contactor_Object);
            ///***modified
            ///dbController.saveContactor(Param_Contactor_object);
            Param_Contactor_Object = (Param_ContactorExt)_Param_Contactor_Object;
        }

        public void GET_CommunicationProfile()
        {
            Param_Controller.ParametersGETStatus.BuildStatusCollection("Communication Parameters");
            var _Param_Communication_Profile_object = Param_Communication_Profile_object;
            Param_Controller.GET_Communication_Profiles(ref _Param_Communication_Profile_object);
            Param_Communication_Profile_object = _Param_Communication_Profile_object;
        }

        public void GET_CustomerReference()
        {
            Param_Controller.ParametersGETStatus.BuildStatusCollection("Customer Reference Number");
            var _Param_Customer_Code_Object = Param_Customer_Code_Object;
            Param_Customer_Code_Object.Customer_Code_String = (string)Param_Controller.GET_Any(ref _Param_Customer_Code_Object,
                Get_Index.Customer_Reference_No, 2, 1);
            Param_Customer_Code_Object = _Param_Customer_Code_Object;
            ///dbController.saveCustomerRef(Param_customer_code_object, txt_CustomerName.Text, txt_CustomerAddress.Text);
        }

        public void GET_CustomerName()
        {
            Param_Controller.ParametersGETStatus.BuildStatusCollection("Customer Name");
            var _Param_Customer_Code_Object = Param_Customer_Code_Object;
            Param_Customer_Code_Object.Customer_Name = (string)Param_Controller.GET_Any(ref _Param_Customer_Code_Object,
                Get_Index.Customer_Name, 2, 1);
            Param_Customer_Code_Object = _Param_Customer_Code_Object;
            ///dbController.saveCustomerRef(Param_customer_code_object, txt_CustomerName.Text, txt_CustomerAddress.Text);
        }

        public void GET_CustomerAddress()
        {
            Param_Controller.ParametersGETStatus.BuildStatusCollection("Customer Address");
            var _Param_Customer_Code_Object = Param_Customer_Code_Object;
            Param_Customer_Code_Object.Customer_Address = (string)Param_Controller.GET_Any(ref _Param_Customer_Code_Object,
                Get_Index.Customer_Address, 2, 1);
            Param_Customer_Code_Object = _Param_Customer_Code_Object;
            ///dbController.saveCustomerRef(Param_customer_code_object, txt_CustomerName.Text, txt_CustomerAddress.Text);
        }

        #region Status Word Map

        void GET_StatusWordMap(StatusWordMapType type)
        {
            Param_Controller.ParametersGETStatus.BuildStatusCollection("Status Word Map");
            var Param_Status_Word = new Param_StatusWordMap();
            Param_Controller.GET_Status_Word_Map(ref Param_Status_Word, type);
            Param_Status_Word_Map_Object = Param_Status_Word;
            if (type == StatusWordMapType.StatusWordMap_1)
            {
                StatusWordItems1 = Param_Status_Word_Map_Object.StatusWordList;
            }
            else if (type == StatusWordMapType.StatusWordMap_2)
            {
                StatusWordItems2 = Param_Status_Word_Map_Object.StatusWordList;
            }
        }

        void SET_StatusWordMap(StatusWordMapType type)
        {
            Param_Controller.ParametersSETStatus.BuildStatusCollection("Status Word");
            if (type == StatusWordMapType.StatusWordMap_1)
            {
                Param_Status_Word_Map_Object.StatusWordList = StatusWordItems1;
            }
            else if (type == StatusWordMapType.StatusWordMap_2)
            {
                Param_Status_Word_Map_Object.StatusWordList = StatusWordItems2;
            }
            Param_Controller.SET_Status_Word_Map(Param_Status_Word_Map_Object, type);
        }

        #endregion // Status Word Map
        public void GET_DecimalPoint()
        {
            Param_Controller.ParametersGETStatus.BuildStatusCollection("Decimal Point");
            var _Param_Decimal_Point_Object = Param_Decimal_Point_Object;
            Param_Controller.GET_Decimal_Point(ref _Param_Decimal_Point_Object);
            ///***modified
            ///dbController.saveDecimalPoint(Param_decimal_point_object);
            Param_Decimal_Point_Object = _Param_Decimal_Point_Object;
        }

        public void GET_DataProfilesWithEvents()
        {
            Param_Controller.ParametersGETStatus.BuildStatusCollection("Data Profile With Events");
        }

        public void GET_DisplayWindows()
        {
            Param_Controller.ParametersGETStatus.BuildStatusCollection("Display Windows");
            DisplayWindows Param_DisplayNormal = null, Param_DisplayAlternate = null;
            Param_Controller.Get_DisplayWindow_Normal(ref Param_DisplayNormal);
            Param_Controller.Get_DisplayWindow_Alternate(ref Param_DisplayAlternate);

            Param_DisplayWindowsNormal = Param_DisplayNormal;
            Param_DisplayWindowsAlternate = Param_DisplayAlternate;

            ///showToGUI_DisplayWindow();

        }

        public void GET_DisplayWindows_Nor()
        {

            Param_Controller.ParametersGETStatus.BuildStatusCollection("Display Windows - Normal");
            DisplayWindows Param_DisplayNormal = null;
            Param_Controller.Get_DisplayWindow_Normal(ref Param_DisplayNormal);
            Param_DisplayWindowsNormal = Param_DisplayNormal;
            //dbController.saveDisplayWindow_Normal(Param_DisplayWindowsNormal);
            // showToGUI_DisplayWindow();
        }

        public void GET_DisplayWindows_Alt()
        {

            Param_Controller.ParametersGETStatus.BuildStatusCollection("Display Windows - Alternate");
            DisplayWindows Param_DisplayAlternate = null;
            Param_Controller.Get_DisplayWindow_Alternate(ref Param_DisplayAlternate);

            Param_DisplayWindowsAlternate = Param_DisplayAlternate;
            ///dbController.saveDisplayWindow_Alternate(Param_DisplayAlternate);
            // showToGUI_DisplayWindow();
        }

        public void GET_DisplayWindows_Test()
        {

            Param_Controller.ParametersGETStatus.BuildStatusCollection("Display Windows - TestMode");
            DisplayWindows Param_DisplayTest = null;
            Param_Controller.Get_DisplayWindow_Test(ref Param_DisplayTest);

            Param_DisplayWindowsTest = Param_DisplayTest;

            // showToGUI_DisplayWindow();
        }

        public void GET_DisplayWindowsPowerDown()
        {
            Param_Controller.ParametersGETStatus.BuildStatusCollection("Display Power Down");
            Param_Controller.GET_Display_PowerDown(Param_Display_PowerDown);
            // showToGUI_DisplayWindow();
        }

        public void GET_EnergyParam()
        {
            Param_Controller.ParametersGETStatus.BuildStatusCollection("Energy Parameters");
            var _Param_Energy_Parameters_Object = Param_Energy_Parameters_Object;
            Param_Controller.GET_EnergyParams(ref _Param_Energy_Parameters_Object);
            Param_Energy_Parameters_Object = _Param_Energy_Parameters_Object;
        }

        public void GET_GeneralProcess()
        {
            Param_Controller.ParametersGETStatus.BuildStatusCollection("General Process");
            Param_Controller.GET_General_Process(Param_GeneralProcess);
        }

        public void GET_IPV4()
        {
            #region IPV4

            Param_Controller.ParametersGETStatus.BuildStatusCollection("IPv4");
            Class_42 _IPV4 = Param_Controller.GET_class_42(ref Param_IPV4_object, Get_Index.IPv4, 0, 42);
            Param_IPV4_object.DHCP_Flag = _IPV4.flg_Use_DHCP;
            Param_IPV4_object.DL_reference = _IPV4.DataLink_Reference;
            Param_IPV4_object.Gateway_IP = (DLMS_Common.IPAddressToLong(_IPV4.Gateway_IP));
            Param_IPV4_object.Primary_DNS = (DLMS_Common.IPAddressToLong(_IPV4.Primary_DNS_IP));
            Param_IPV4_object.Secondary_DNS = (DLMS_Common.IPAddressToLong(_IPV4.Secondary_DNS_IP));
            Param_IPV4_object.Subnet_Mask = (DLMS_Common.IPAddressToLong(_IPV4.Subnet_Mask));
            Param_IPV4_object.IP = (DLMS_Common.IPAddressToLong(_IPV4.IP_Address));

            #endregion
        }

        public void GET_IPProfile()
        {
            Param_Controller.ParametersGETStatus.BuildStatusCollection("IP Profiles");
            var _Param_IP_Profiles_object = Param_IP_Profiles_object;
            Param_Controller.GET_IP_Profiles(ref _Param_IP_Profiles_object);
            Param_IP_Profiles_object = _Param_IP_Profiles_object;
        }

        void GET_StandardIPProfile()
        {
            Param_Controller.ParametersGETStatus.BuildStatusCollection("Standard IP Profiles");
            Param_Controller.GET_Standard_IP_Profiles(ref Param_Standard_IP_Profiles_object);
        }

        public void GET_KeepAlive()
        {
            Param_Controller.ParametersGETStatus.BuildStatusCollection("Keep Alive IP");
            var _Param_Keep_Alive_IP_object = Param_Keep_Alive_IP_object;
            Param_Controller.GET_Keep_Alive_IP(ref _Param_Keep_Alive_IP_object);
            Param_Keep_Alive_IP_object = _Param_Keep_Alive_IP_object;
        }

        void GET_KeepAlive_Standard()
        {
            Param_Controller.ParametersGETStatus.BuildStatusCollection("Keep Alive Standard");
            Param_Controller.GET_AutoConnect_Mode(ref Auto_Connect_Mode);
        }

        public void GET_Limits()
        {
            #region Get_LIMITS

            Param_Controller.ParametersGETStatus.BuildStatusCollection("Limits");

            var _Param_Limits_Object = Param_Limits_Object;
            ///*********************************************
            ///*********************************************
            if (CanGetLimit(SharedCode.Comm.HelperClasses.Limits.OverVolt))
                Param_Controller.GET_Limit_Over_Voltage(ref _Param_Limits_Object);

            if (CanGetLimit(SharedCode.Comm.HelperClasses.Limits.UnderVolt))
                Param_Controller.GET_Limit_Under_Voltage(ref _Param_Limits_Object);

            if (CanGetLimit(SharedCode.Comm.HelperClasses.Limits.ImbalanceVolt))
                Param_Controller.GET_Limit_Imbalance_Voltage(ref _Param_Limits_Object);

            if (CanGetLimit(SharedCode.Comm.HelperClasses.Limits.CTFailLimitAmp))
                Param_Controller.GET_Limit_CT_Fail_AMP(ref _Param_Limits_Object);

            if (CanGetLimit(SharedCode.Comm.HelperClasses.Limits.HighNeturalCurrent))
                Param_Controller.GET_Limit_High_Neutral_Current(ref _Param_Limits_Object);

            //Duplicated so Commented
            //if (CanGetLimit(SharedCode.Comm.HelperClasses.Limits.ImbalanceVolt))
            //    Param_Controller.GET_Limit_Imbalance_Voltage(ref _Param_Limits_Object);

            if (CanGetLimit(SharedCode.Comm.HelperClasses.Limits.OverCurrentByPhaseT1))
                Param_Controller.GET_Limit_Over_Current_by_Phase_T1(ref _Param_Limits_Object);
            if (CanGetLimit(SharedCode.Comm.HelperClasses.Limits.OverCurrentByPhaseT2))
                Param_Controller.GET_Limit_Over_Current_by_Phase_T2(ref _Param_Limits_Object);
            if (CanGetLimit(SharedCode.Comm.HelperClasses.Limits.OverCurrentByPhaseT3))
                Param_Controller.GET_Limit_Over_Current_by_Phase_T3(ref _Param_Limits_Object);
            if (CanGetLimit(SharedCode.Comm.HelperClasses.Limits.OverCurrentByPhaseT4))
                Param_Controller.GET_Limit_Over_Current_by_Phase_T4(ref _Param_Limits_Object);

            if (CanGetLimit(SharedCode.Comm.HelperClasses.Limits.OverLoadByPhaseT1))
                Param_Controller.GET_Limit_Over_Load_by_Phase_T1(ref _Param_Limits_Object);
            if (CanGetLimit(SharedCode.Comm.HelperClasses.Limits.OverLoadByPhaseT2))
                Param_Controller.GET_Limit_Over_Load_by_Phase_T2(ref _Param_Limits_Object);
            if (CanGetLimit(SharedCode.Comm.HelperClasses.Limits.OverLoadByPhaseT3))
                Param_Controller.GET_Limit_Over_Load_by_Phase_T3(ref _Param_Limits_Object);
            if (CanGetLimit(SharedCode.Comm.HelperClasses.Limits.OverLoadByPhaseT4))
                Param_Controller.GET_Limit_Over_Load_by_Phase_T4(ref _Param_Limits_Object);

            if (CanGetLimit(SharedCode.Comm.HelperClasses.Limits.OverLoadTotalT1))
                Param_Controller.GET_Limit_Over_Load_Total_T1(ref _Param_Limits_Object);
            if (CanGetLimit(SharedCode.Comm.HelperClasses.Limits.OverLoadTotalT2))
                Param_Controller.GET_Limit_Over_Load_Total_T2(ref _Param_Limits_Object);
            if (CanGetLimit(SharedCode.Comm.HelperClasses.Limits.OverLoadTotalT3))
                Param_Controller.GET_Limit_Over_Load_Total_T3(ref _Param_Limits_Object);
            if (CanGetLimit(SharedCode.Comm.HelperClasses.Limits.OverLoadTotalT4))
                Param_Controller.GET_Limit_Over_Load_Total_T4(ref _Param_Limits_Object);

            if (CanGetLimit(SharedCode.Comm.HelperClasses.Limits.PTFailLimitAmp))
                Param_Controller.GET_Limit_PT_Fail_AMP(ref _Param_Limits_Object);
            if (CanGetLimit(SharedCode.Comm.HelperClasses.Limits.PTFailLimitV))
                Param_Controller.GET_Limit_PT_Fail_Volt(ref _Param_Limits_Object);

            if (CanGetLimit(SharedCode.Comm.HelperClasses.Limits.ReverseEnergykWh))
                Param_Controller.GET_Limit_Reverse_Energy(ref _Param_Limits_Object);

            if (CanGetLimit(SharedCode.Comm.HelperClasses.Limits.TemperEnergykWh))
                Param_Controller.GET_Limit_Tamper_Energy(ref _Param_Limits_Object);

            if (CanGetLimit(SharedCode.Comm.HelperClasses.Limits.Contactor_Failure_Pwr)) //5.3.12
                Param_Controller.GET_Limit_Contactor_Power_Failure(ref _Param_Limits_Object);

            if (CanGetLimit(SharedCode.Comm.HelperClasses.Limits.OverCurrent_Phase))
                Param_Controller.GET_Limit_Over_Current_Phase(ref _Param_Limits_Object);
            if (CanGetLimit(SharedCode.Comm.HelperClasses.Limits.Meter_ON_Load))
                Param_Controller.GET_Limit_Meter_On_Load(ref _Param_Limits_Object);
            if (CanGetLimit(SharedCode.Comm.HelperClasses.Limits.PowerFactor_Change))
                Param_Controller.GET_Limit_Power_Factor_Change(ref _Param_Limits_Object);
            if (CanGetLimit(SharedCode.Comm.HelperClasses.Limits.CrestFactorLow))
                Param_Controller.GET_Limit_Crest_Factor_Low(ref _Param_Limits_Object);
            if (CanGetLimit(SharedCode.Comm.HelperClasses.Limits.CrestFactorHigh))
                Param_Controller.GET_Limit_Crest_Factor_High(ref _Param_Limits_Object);
            if (CanGetLimit(SharedCode.Comm.HelperClasses.Limits.OverPower))
                Param_Controller.GET_Limit_OverPower(ref _Param_Limits_Object);



            Param_Limit_Demand_OverLoad Param_Limit_Demand_OverLoad_T1 = new Param_Limit_Demand_OverLoad();
            Param_Limit_Demand_OverLoad Param_Limit_Demand_OverLoad_T2 = new Param_Limit_Demand_OverLoad();
            Param_Limit_Demand_OverLoad Param_Limit_Demand_OverLoad_T3 = new Param_Limit_Demand_OverLoad();
            Param_Limit_Demand_OverLoad Param_Limit_Demand_OverLoad_T4 = new Param_Limit_Demand_OverLoad();

            try
            {
                if (CanGetLimit(SharedCode.Comm.HelperClasses.Limits.MDIExceedT1))
                    Param_Controller.GET_Limit_Demand_OverLoad_T1(ref Param_Limit_Demand_OverLoad_T1);
                if (CanGetLimit(SharedCode.Comm.HelperClasses.Limits.MDIExceedT2))
                    Param_Controller.GET_Limit_Demand_OverLoad_T2(ref Param_Limit_Demand_OverLoad_T2);
                if (CanGetLimit(SharedCode.Comm.HelperClasses.Limits.MDIExceedT3))
                    Param_Controller.GET_Limit_Demand_OverLoad_T3(ref Param_Limit_Demand_OverLoad_T3);
                if (CanGetLimit(SharedCode.Comm.HelperClasses.Limits.MDIExceedT4))
                    Param_Controller.GET_Limit_Demand_OverLoad_T4(ref Param_Limit_Demand_OverLoad_T4);
            }
            finally
            {
                _Param_Limits_Object.DemandOverLoadTotal_T1 = Param_Limit_Demand_OverLoad_T1.Threshold;
                _Param_Limits_Object.DemandOverLoadTotal_T2 = Param_Limit_Demand_OverLoad_T2.Threshold;
                _Param_Limits_Object.DemandOverLoadTotal_T3 = Param_Limit_Demand_OverLoad_T3.Threshold;
                _Param_Limits_Object.DemandOverLoadTotal_T4 = Param_Limit_Demand_OverLoad_T4.Threshold;
            }

            #endregion
        }

        public void GET_MajorAlarmProfile()
        {
            Param_Controller.ParametersGETStatus.BuildStatusCollection("Major Alarms Profiles");
        }

        public void GET_ModemLimitsAndTime()
        {
            Param_Controller.ParametersGETStatus.BuildStatusCollection("Modem Limits and Times");
            var _Param_ModemLimitsAndTime_Object = Param_ModemLimitsAndTime_Object;
            Param_Controller.GET_ModemLimitsAndTime(ref _Param_ModemLimitsAndTime_Object);
            Param_ModemLimitsAndTime_Object = _Param_ModemLimitsAndTime_Object;
        }

        public void GET_MonitoringTime()
        {
            #region MONITORING TIMES

            var _Param_Monitoring_Time_Object = Param_Monitoring_Time_Object;
            Param_Controller.ParametersGETStatus.BuildStatusCollection("Monitoring Times");

            if (false)
            {
                Param_Controller.GET_MT_CT_Fail(ref _Param_Monitoring_Time_Object);
                //Update_GUI();
                Param_Controller.GET_MT_High_Neutral_Current(ref _Param_Monitoring_Time_Object);
                //Update_GUI();
                Param_Controller.GET_MT_Imbalance_Volt(ref _Param_Monitoring_Time_Object);
                //Update_GUI();
                Param_Controller.GET_MT_Over_Current(ref _Param_Monitoring_Time_Object);
                //Update_GUI();
                Param_Controller.GET_MT_Over_Load(ref _Param_Monitoring_Time_Object);
                //Update_GUI();
                Param_Controller.GET_MT_Over_Volt(ref _Param_Monitoring_Time_Object);
                //Update_GUI();
                Param_Controller.GET_MT_Phase_Fail(ref _Param_Monitoring_Time_Object);
                //Update_GUI();
                Param_Controller.GET_MT_Phase_Sequence(ref _Param_Monitoring_Time_Object);
                //Update_GUI();
                Param_Controller.GET_MT_Power_Fail(ref _Param_Monitoring_Time_Object);
                //Update_GUI();
                Param_Controller.GET_MT_Power_Up_Delay_For_Energy_Recording(ref _Param_Monitoring_Time_Object);
                //Update_GUI();
                Param_Controller.GET_MT_Power_Up_Delay_To_Monitor(ref _Param_Monitoring_Time_Object);
                //Update_GUI();
                Param_Controller.GET_MT_PT_Fail(ref _Param_Monitoring_Time_Object);
                //Update_GUI();
                Param_Controller.GET_MT_Reverse_Energy(ref _Param_Monitoring_Time_Object);
                //Update_GUI();
                Param_Controller.GET_MT_Reverse_Polarity(ref _Param_Monitoring_Time_Object);
                //Update_GUI();
                Param_Controller.GET_MT_Tamper_Energy(ref _Param_Monitoring_Time_Object);
                //Update_GUI();
                Param_Controller.GET_MT_Under_Volt(ref _Param_Monitoring_Time_Object);
                //Update_GUI();
            }
            else
            {
                if (CanGetMonitoringTime(MonitoringTime.CTFail))
                    Param_Controller.GET_MT_CT_Fail(ref _Param_Monitoring_Time_Object);
                if (CanGetMonitoringTime(MonitoringTime.HighNeturalCurrent))
                    Param_Controller.GET_MT_High_Neutral_Current(ref _Param_Monitoring_Time_Object);
                if (CanGetMonitoringTime(MonitoringTime.ImbalanceVolt))
                    Param_Controller.GET_MT_Imbalance_Volt(ref _Param_Monitoring_Time_Object);
                if (CanGetMonitoringTime(MonitoringTime.OverCurrent))
                    Param_Controller.GET_MT_Over_Current(ref _Param_Monitoring_Time_Object);
                if (CanGetMonitoringTime(MonitoringTime.OverLoad))
                    Param_Controller.GET_MT_Over_Load(ref _Param_Monitoring_Time_Object);
                if (CanGetMonitoringTime(MonitoringTime.OverVolt))
                    Param_Controller.GET_MT_Over_Volt(ref _Param_Monitoring_Time_Object);
                if (CanGetMonitoringTime(MonitoringTime.PhaseFail))
                    Param_Controller.GET_MT_Phase_Fail(ref _Param_Monitoring_Time_Object);
                if (CanGetMonitoringTime(MonitoringTime.PhaseSequence))
                    Param_Controller.GET_MT_Phase_Sequence(ref _Param_Monitoring_Time_Object);
                if (CanGetMonitoringTime(MonitoringTime.PowerFail))
                    Param_Controller.GET_MT_Power_Fail(ref _Param_Monitoring_Time_Object);
                if (CanGetMonitoringTime(MonitoringTime.PUDForEnergyRecording))
                    Param_Controller.GET_MT_Power_Up_Delay_For_Energy_Recording(ref _Param_Monitoring_Time_Object);
                if (CanGetMonitoringTime(MonitoringTime.PUDToMoniter))
                    Param_Controller.GET_MT_Power_Up_Delay_To_Monitor(ref _Param_Monitoring_Time_Object);
                if (CanGetMonitoringTime(MonitoringTime.PTFail))
                    Param_Controller.GET_MT_PT_Fail(ref _Param_Monitoring_Time_Object);
                if (CanGetMonitoringTime(MonitoringTime.ReverseEnergy))
                    Param_Controller.GET_MT_Reverse_Energy(ref _Param_Monitoring_Time_Object);
                if (CanGetMonitoringTime(MonitoringTime.ReversePolarity))
                    Param_Controller.GET_MT_Reverse_Polarity(ref _Param_Monitoring_Time_Object);
                if (CanGetMonitoringTime(MonitoringTime.TemperEnergy))
                    Param_Controller.GET_MT_Tamper_Energy(ref _Param_Monitoring_Time_Object);
                if (CanGetMonitoringTime(MonitoringTime.UnderVolt))
                    Param_Controller.GET_MT_Under_Volt(ref _Param_Monitoring_Time_Object);
                if (CanGetMonitoringTime(MonitoringTime.ContactorFailure))
                    Param_Controller.GET_MT_Contactor_Failure(ref _Param_Monitoring_Time_Object);
            }

            #endregion
            ///dbController.saveMonitoringTime(Param_Monitoring_time_object);
        }

        public void GET_ModemInitialize()
        {
            try
            {
                var _Param_Modem_Initialize_Object = Param_Modem_Initialize_Object;
                var _Param_ModemBasics_NEW_object = Param_ModemBasics_NEW_object;

                Param_Controller.ParametersGETStatus.BuildStatusCollection("Modem Initialization");
                var result = Param_Controller.GET_ModemBasics(ref _Param_Modem_Initialize_Object);
                if (result != DecodingResult.Ready)
                {
                }
                Param_Controller.ParametersGETStatus.BuildStatusCollection("Modem Initialization New");
                Param_Controller.GET_ModemBasicsNew(ref _Param_ModemBasics_NEW_object);
            }
            catch
            {
                throw;
            }
        }

        public void GET_NumberProfile()
        {
            Param_Controller.ParametersGETStatus.BuildStatusCollection("Number Profiles");

            var _Param_Number_Profile_object = Param_Number_Profile_object;

            Param_Controller.GET_Number_Profiles(ref _Param_Number_Profile_object);
            Param_Number_Profile_object = _Param_Number_Profile_object;
        }

        void GET_StandardNumberProfile()
        {
            Param_Controller.ParametersGETStatus.BuildStatusCollection("Standard Number Profiles");
            Param_Controller.GET_Number_Profiles_AllowedCallers(ref Param_Standard_Number_Profile_object);
        }

        public void GET_Passwords()
        {
            #region Password
            Param_Controller.ParametersGETStatus.BuildStatusCollection("Passwords");
            var _Param_Password_Object = Param_Password_Object;

            Param_Password_Object.Management_Device = (string)Param_Controller.GET_Any(ref _Param_Password_Object, Get_Index.Current_Association, 7, 15);
            /// param_password_object.Electrical_Device = (string)Param_Controller.GET_Any(ref param_password_object, Get_Index.Current_Association2, 7, 15);
            Param_Password_Object = _Param_Password_Object;

            #endregion
        }

        public void GET_TCPUDP()
        {
            #region TCP/UDP
            Param_Controller.ParametersGETStatus.BuildStatusCollection("TCP UDP");
            var _Param_TCP_UDP_object = Param_TCP_UDP_object;

            Class_41 TCP_UDP = Param_Controller.GET_class_41(ref _Param_TCP_UDP_object, Get_Index.TCP_UDP_Setup, 0, 41);
            Param_TCP_UDP_object.Inactivity_Time_Out = TCP_UDP.Inactivity_Time_Out_Secs;
            Param_TCP_UDP_object.IP_Port = TCP_UDP.TCP_UDP_Port;
            //Param_TCP_UDP_object.IP_reference=  TCP_UDP.IP_Reference;
            Param_TCP_UDP_object.Max_no_of_simulaneous_connections = TCP_UDP.Simultaneous_Conn_No;
            Param_TCP_UDP_object.Max_Segmentation_Size = TCP_UDP.Max_Segment_Size;
            //Update_GUI();
            //showToGUI_TCPUDP();
            #endregion
        }

        public void GET_MDIParams()
        {
            Param_Controller.ParametersGETStatus.BuildStatusCollection("MDI");
            var _Param_MDI_Parameters_Object = Param_MDI_Parameters_Object;
            Param_Controller.GET_MDI_Parameters(ref _Param_MDI_Parameters_Object);
            ///***modified
            ///dbController.saveMDIparams(Param_MDI_parameters_object);
            Param_MDI_Parameters_Object = _Param_MDI_Parameters_Object;
        }

        public void GET_WakeupProfile()
        {
            Param_Controller.ParametersGETStatus.BuildStatusCollection("WakeUp Profiles");
            var _Param_Wakeup_Profile_object = Param_Wakeup_Profile_object;
            Param_Controller.GET_WakeUp_Profile(ref _Param_Wakeup_Profile_object);
            Param_Wakeup_Profile_object = _Param_Wakeup_Profile_object;
        }

        public void GET_IPProfiles()
        {
            Param_Controller.ParametersGETStatus.BuildStatusCollection("IP Profiles");
            var _Param_IP_Profiles_object = Param_IP_Profiles_object;
            Param_Controller.GET_IP_Profiles(ref _Param_IP_Profiles_object);
            Param_IP_Profiles_object = _Param_IP_Profiles_object;
        }

        public void GET_NumberProfiles()
        {
            Param_Controller.ParametersGETStatus.BuildStatusCollection("Number Profiles");
            var _Param_Number_Profile_object = Param_Number_Profile_object;
            Param_Controller.GET_Number_Profiles(ref _Param_Number_Profile_object);
            _Param_Number_Profile_object = Param_Number_Profile_object;
        }

        public void GET_KeepAliveIP()
        {
            //Param_Controller.GET_Keep_Alive_IP(
            Param_Controller.ParametersGETStatus.BuildStatusCollection("Keep Alive IP");
        }

        void GET_LoadProfileChannels(LoadProfileScheme LP_Scheme)
        {
            try
            {
                // Load Profile Channels Reading
                List<LoadProfileChannelInfo> LPChannels;
                TimeSpan LPPeriod;
                Param_Controller.ParametersGETStatus.BuildStatusCollection(string.Format("Load Profile Channels_ {0}", (byte)LP_Scheme));
                LPChannels = LoadProfile_Controller.Get_LoadProfileChannels(LP_Scheme);

                if (LPChannels.Count > 0)
                {
                    LPPeriod = LPChannels[0].CapturePeriod;
                }
                else
                    LPPeriod = new TimeSpan();     // Default Time Period

                if (LP_Scheme == LoadProfileScheme.Load_Profile)
                {
                    LoadProfileChannelsInfo = LPChannels;
                    LoadProfilePeriod = LPPeriod;
                }
                else if (LP_Scheme == LoadProfileScheme.Load_Profile_Channel_2)
                {
                    LoadProfileChannelsInfo_2 = LPChannels;
                    LoadProfilePeriod_2 = LPPeriod;
                }

                #region Add Load Profile GET Success Status
                Status LPGetStatus = new Status();
                ///Load Profile Period
                LPGetStatus.OBIS_Code = LoadProfileController.GetLoadProfileIndex(LP_Scheme);
                LPGetStatus.AttributeLabel = string.Format("Capture Period {0}", (byte)LP_Scheme);
                LPGetStatus.AttributeNo = 0x04;
                LPGetStatus.GETCommStatus = DecodingResult.Ready;
                Param_Controller.ParametersGETStatus.Current.AddCommandStatus(LPGetStatus);
                ///Load Profile Period
                LPGetStatus = new Status();
                LPGetStatus.OBIS_Code = LoadProfileController.GetLoadProfileIndex(LP_Scheme);
                LPGetStatus.AttributeLabel = string.Format("Capture Buffer {0}", (byte)LP_Scheme);
                LPGetStatus.AttributeNo = 0x03;
                LPGetStatus.GETCommStatus = DecodingResult.Ready;
                Param_Controller.ParametersGETStatus.Current.AddCommandStatus(LPGetStatus);
                #endregion
            }
            catch (Exception ex)
            {
                #region Add Load Profile Get Status
                Status LPGetStatus = new Status();
                LPGetStatus.OBIS_Code = LoadProfileController.GetLoadProfileIndex(LP_Scheme);
                LPGetStatus.AttributeNo = 0x04;
                LPGetStatus.AttributeLabel = string.Format("Capture Period {0}", (byte)LP_Scheme);
                LPGetStatus.GETCommStatus = DecodingResult.DataNotPresent;
                Param_Controller.ParametersGETStatus.Current.AddCommandStatus(LPGetStatus);
                ///Load Profile Period
                LPGetStatus = new Status();
                LPGetStatus.OBIS_Code = LoadProfileController.GetLoadProfileIndex(LP_Scheme);
                LPGetStatus.AttributeLabel = string.Format("Capture Buffer {0}", (byte)LP_Scheme);
                LPGetStatus.AttributeNo = 0x03;
                LPGetStatus.GETCommStatus = DecodingResult.DataNotPresent;
                Param_Controller.ParametersGETStatus.Current.AddCommandStatus(LPGetStatus);
                #endregion
            }
        }

        void GET_LoadProfileInterval(LoadProfileScheme LP_Scheme)
        {
            try
            {
                // Load Profile Channels Reading
                Param_Controller.ParametersGETStatus.BuildStatusCollection(string.Format("Load Profile Interval_ {0}", (byte)LP_Scheme));
                TimeSpan LPPeriod = LoadProfile_Controller.Get_LoadProfileInterval(LP_Scheme);

                if (LP_Scheme == LoadProfileScheme.Load_Profile)
                    LoadProfilePeriod = LPPeriod;
                else if (LP_Scheme == LoadProfileScheme.Load_Profile_Channel_2)
                    LoadProfilePeriod_2 = LPPeriod;
                else if (LP_Scheme == LoadProfileScheme.Daily_Load_Profile)
                    PQLoadProfilePeriod = LPPeriod;


                #region Add Load Profile GET Success Status
                Status LPGetStatus = new Status();
                ///Load Profile Period
                LPGetStatus.OBIS_Code = LoadProfileController.GetLoadProfileIndex(LP_Scheme);
                LPGetStatus.AttributeLabel = string.Format("Capture Period {0}", (byte)LP_Scheme);
                LPGetStatus.AttributeNo = 0x04;
                LPGetStatus.GETCommStatus = DecodingResult.Ready;
                Param_Controller.ParametersGETStatus.Current.AddCommandStatus(LPGetStatus);

                #endregion
            }
            catch (Exception ex)
            {
                #region Add Load Profile Get Status
                Status LPGetStatus = new Status();
                LPGetStatus.OBIS_Code = LoadProfileController.GetLoadProfileIndex(LP_Scheme);
                LPGetStatus.AttributeNo = 0x04;
                LPGetStatus.AttributeLabel = string.Format("Capture Period {0}", (byte)LP_Scheme);
                LPGetStatus.GETCommStatus = DecodingResult.DataNotPresent;
                Param_Controller.ParametersGETStatus.Current.AddCommandStatus(LPGetStatus);

                #endregion
            }
        }
        #endregion

        #region SET_Params_BackgroundWorker Handlers

        private void btn_SETAsync_Parameters_Click(Object sender, EventArgs e)
        {
            try
            {
                ///Not Connected Properly
                if (!(Application_Process.Is_Association_Developed))
                {
                    Notification n = new Notification("Disconnected", "Create Association to Meter");
                    return;
                }

                if (Application_Controller.CurrentUser != null)
                {
                    setter = new CustomSetGet("Set Parameters", application_Controller.isSinglePhase, Application_Controller.CurrentUser.CurrentAccessRights, 2);
                }
                else
                {
                    setter = new CustomSetGet("Set Parameters.", application_Controller.isSinglePhase);
                    setter.IsReadMode = 2;
                }
                //setter = new CustomSetGet("Set Parameters", application_Controller.isSinglePhase);
                //if (Application_Controller.CurrentUser != null)
                //    setter.AccessRights = Application_Controller.CurrentUser.CurrentAccessRights;


                //setter.AccessRights = AccessRights_R326;
                if (setter.ShowDialog(this.Parent) == DialogResult.OK && setter.IsAnyChecked)
                {
                    this.Parameterization_BckWorkerThread.DoWork += new DoWorkEventHandler(BckWorker_SETParams_DoEventHandler);
                    this.Parameterization_BckWorkerThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BckWorker_SETParam_WorkCompleted);
                    this.Parameterization_BckWorkerThread.ProgressChanged += new ProgressChangedEventHandler(BckWorker_SETParam_ProgressChanged);
                    dlg = new ProgressDialog();
                    Param_Controller.ParameterSetStatus += dlg.ConnController_ProcessStatusHandler;
                    //dlg.btnCancel.Click += new EventHandler(btn_CancelSETAsync_Parameters_Click);
                    //dlg.btnCancel.Enabled = true;
                    dlg.EnableProgressBar = true;
                    dlg.Text = "Parameterizing";
                    dlg.DialogTitle = "Setting Parameters";
                    Application_Controller.IsIOBusy = true;
                    dlg.okButton.Visible = false;
                    Parameterization_BckWorkerThread.RunWorkerAsync();
                    dlg.ShowDialog(this.Parent);
                    Application.DoEvents();
                }
                else
                    return;
            }
            catch (Exception ex)
            {
                String ErrorMsg = string.Format("Error Setting Parameters {0},Details{1}", ex.Message, (ex.InnerException == null) ? null : ex.InnerException.Message);
                ///MessageBox.Show(ErrorMsg, "Error Setting Parameters", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Notification Notifier = new Notification("Error", ErrorMsg);
            }
            finally
            {

            }
        }

        private void btn_CancelSETAsync_Parameters_Click(Object sender, EventArgs e)
        {
            try
            {
                Parameterization_BckWorkerThread.CancelAsync();
            }
            catch (Exception ex)
            {
                String ErrorMsg = string.Format("Error Setting Parameters {0},Details{1}", ex.Message, (ex.InnerException == null) ? null : ex.InnerException.Message);
                ///MessageBox.Show(ErrorMsg, "Error Setting Parameters", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Notification Notifier = new Notification("Error ", ErrorMsg);

            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void BckWorker_SETParams_DoEventHandler(Object sender, DoWorkEventArgs arg)
        {
            try
            {
                Param_Controller.ParametersSETStatus.ResetCommandStatus();
                if (true)
                {
                    if (setter.check_ActivityCalender.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_ActivityCalendar();
                    }
                    if (setter.check_Clock.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_Clock_Misc();
                    }
                    if (setter.check_IP_Profile.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;

                        SET_IPProfiles();
                    }
                    if (setter.chbWakeupProfile.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_WakeupProfile();
                    }
                    if (setter.chbNumberProfile.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;

                        SET_NumberProfiles();
                    }
                    if (setter.chbKeepAlive.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;

                        SET_KeepAlive();
                    }
                    if (setter.check_StandardModem_KeepAlive.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        Auto_Connect_Mode = ucStandardModem.chkKeepALiveStandard.Checked ? AutoConnectMode.PermanentConnectionAlways : AutoConnectMode.ManualInvokeConnect;
                        SET_KeepAlive_Standard();
                    }
                    if (setter.chbModemLimitsAndTime.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;

                        SET_ModemLimitsAndTime();
                    }
                    if (setter.chbModemInitialize.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_ModemInitialize();
                    }
                    if (setter.chbCommunicationProfile.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        //UpdateNetworkModes();
                        SET_Communicationprofile();
                    }
                    if (setter.check_StandardModem_IP_Profile.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_StandardIPProfiles();
                    }
                    if (setter.check_StandardModem_Number_Profile.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_NumberProfile_Standard();
                    }

                    if (setter.check_Contactor.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_Contactor();
                    }
                    if (setter.check_CTPT.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_CTPT();
                    }
                    if (setter.check_customerReference.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_CustomerReference();
                        //SET_CustomerName();
                        //SET_CustomerAddress();
                    }
                    if (setter.check_DataProfilewithEvents.Checked && setter.check_DataProfilewithEvents.Visible)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_DataProfilesWithEvents();
                    }
                    if (setter.check_DecimalPoint.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_DecimalPoint();
                    }
                    if (setter.chbStatusWordMap1.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_StatusWordMap(StatusWordMapType.StatusWordMap_1);
                    }
                    if (setter.chbStatusWordMap2.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_StatusWordMap(StatusWordMapType.StatusWordMap_2);
                    }
                    if (setter.check_DisplayWindows_Alt.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_DisplayWindows_Alt();
                    }
                    if (setter.check_DisplayWindows_Nor.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_DisplayWindows_Nor();
                    }
                    if (setter.check_DisplayWindows_test.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_DisplayWindows_Test();
                    }
                    if (setter.check_DisplayPowerDown.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_DisplayPowerDown();
                    }
                    if (setter.check_EnergyParams.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_EnergyParam();
                    }
                    if (setter.check_IPV4.Checked && setter.check_IPV4.Visible)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_IPV4();
                    }

                    if (setter.check_Limits.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_Limits();
                    }
                    if (setter.chk_LoadProfile_Interval.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_LoadProfileChannels_Interval(LoadProfileScheme.Load_Profile);
                    }

                    if (setter.chk_LoadProfile_2_Interval.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_LoadProfileChannels_Interval(LoadProfileScheme.Load_Profile_Channel_2);
                    }

                    if (setter.chk_LoadProfile.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_LoadProfileChannels(LoadProfileScheme.Load_Profile);
                    }

                    if (setter.chk_LoadProfile_2.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_LoadProfileChannels(LoadProfileScheme.Load_Profile_Channel_2);
                    }

                    if (setter.chk_PQ_LoadProfileInterval.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_LoadProfileChannels_Interval(LoadProfileScheme.Daily_Load_Profile);
                    }
                    if (setter.check_MajorAlarmprofile.Checked && setter.check_MajorAlarmprofile.Visible)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_MajorAlarmProfile();
                    }
                    if (setter.check_MDI_params.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_MDIParams();
                    }
                    if (setter.check_MonitoringTime.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_MonitoringTime();
                    }
                    if (setter.chk_GPP.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_GeneralProcess();
                    }
                    if (setter.check_TCPUDP.Checked && setter.check_TCPUDP.Visible)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_TCPUDP();
                    }
                    //if (setter.check_Passwords.Checked)
                    //{
                    //    if (Parameterization_BckWorkerThread.CancellationPending)
                    //        arg.Cancel = true;
                    //    Set_Password_Managerial();
                    //}
                    if (setter.check_Password_Elec.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        Set_Password_Electrical();
                    }
                    if (setter.check_Time.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_Clock();
                    }
                    if (setter.check_TBEs.Checked)
                    {

                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        if (ucTimeWindowParam1.Visible && ucTimeWindowParam1.Enabled) //Check added in v4.8.16
                            SET_Time_Based_Event_1();
                        if (ucTimeWindowParam2.Visible && ucTimeWindowParam2.Enabled) //Check added in v4.8.16
                            SET_Time_Based_Event_2();

                        //Check added in v4.8.16
                        if ((check_TBE1_PowerFail.Visible && check_TBE1_PowerFail.Enabled) || (check_TBE2_PowerFail.Visible && check_TBE2_PowerFail.Enabled))
                        {
                            Data_Access_Result d = Application_Controller.Param_Controller.SET_TBE_PowerFAil(obj_TBE_PowerFail);
                        }
                    }
                    //setter.check_loadShedding.Visible && 
                    if (setter.check_loadShedding.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;

                        SET_SchedulerTable();
                    }
                    if (setter.check_GeneratorStart.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;

                        SET_GeneratorStart();
                    }
                    //v4.8.32
                    if (setter.gbSecurityKeys.Visible)
                    {
                        if (setter.check_EncryptionKey.Visible && setter.check_EncryptionKey.Checked)
                        {
                            if (!string.IsNullOrEmpty(txtEncryptionKey.Text) && txtEncryptionKey.Text.Length == KeySize * 2)
                            {
                                byte[] keyToWrite = DLMS_Common.String_to_Hex_array(txtEncryptionKey.Text);

                                if (SET_SecurityKey(keyToWrite, KEY_ID.GLOBAL_Unicast_EncryptionKey) == Action_Result.Success)
                                {
                                    AP_Controller.Security_Data.EncryptionKey.Value = new List<byte>(keyToWrite);
                                }
                            }

                        }
                        if (setter.check_EncryptionKey.Visible && setter.check_WriteAuthenticationKey.Checked)
                        {
                            if (!string.IsNullOrEmpty(txtAuthenticationKey.Text) && txtAuthenticationKey.Text.Length == KeySize * 2)
                            {
                                byte[] keyToWrite = DLMS_Common.String_to_Hex_array(txtAuthenticationKey.Text);
                                if (SET_SecurityKey(keyToWrite, KEY_ID.AuthenticationKey) == Action_Result.Success)
                                {
                                    AP_Controller.Security_Data.AuthenticationKey.Value = new List<byte>(keyToWrite);
                                }
                            }
                        }
                        if (setter.check_EncryptionKey.Visible && setter.check_SecurityPolicy.Checked)
                        {
                            SET_SecurityPolicy((Security_Policy)cmbSecurityControl.SelectedIndex);
                        }
                    }
                    if (!HidePrintReportButtons)
                    {
                        //get customer code
                        try
                        {
                            Application_Controller.Param_Controller.GET_Customer_Reference(ref obj_CustomerCode); //Added by Azeem
                            Application_Controller.Param_Controller.GET_Customer_Name(ref obj_CustomerCode); //Added by Azeem
                            Application_Controller.Param_Controller.GET_Customer_Address(ref obj_CustomerCode); //Added by Azeem
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                //else return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BckWorker_SETParam_WorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                    throw e.Error;
                else if (e.Cancelled)
                {
                    /// MessageBox.Show("Parameterization Process Is Cancel by User", "Process Aborted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Notification Notifier = new Notification("Process Aborted", "Parameterization Process Is Cancel by User");
                }
                else
                {
                    String ParamStatistics = null;
                    ParamStatistics = Param_Controller.ParametersSETStatus.BuildParameterizationStatistic();
                    String ParamSummary = Param_Controller.ParametersSETStatus.BuildParamterizationLog(Data_Access_Result.Success, false);
                    int copy_text_index = ParamSummary.IndexOf('\r', 0);
                    string msgtoShow = "";
                    if (copy_text_index != -1)
                    {
                        //msgtoShow = ParamSummary.Substring(0, copy_text_index);
                        msgtoShow = "Parameterization InComplete";
                    }
                    else
                    {
                        msgtoShow = ParamSummary;
                    }
                    String MessageContent = String.Format("{0}", ParamSummary);
                    //MessageBox.Show(MessageContent, "Parameterization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Thread.Sleep(250);
                    Notification Notifier = new Notification("Process Completed", msgtoShow);
                    dlg.ConnController_ProcessStatusHandler(MessageContent);
                }
                /////Application Connected disable Controls,Update Controls On Connection Establishment
                ////UpdateConnectStatus((bool)e.Result);

            }
            catch (Exception ex)
            {
                String _txt = String.Format("Error occurred while setting Parameters,Details:\r\n{0} {1}",
                    ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : "");
                /// MessageBox.Show(_txt, "Error Setting Parameters", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Notification Notifier = new Notification("Error ", _txt);
                //progressDialog.UpdateDialogStatusHandler(_txt);
            }
            finally
            {
                //progressDialog.UserInputEnable = true;
                this.Parameterization_BckWorkerThread.DoWork -= new DoWorkEventHandler(BckWorker_SETParams_DoEventHandler);
                this.Parameterization_BckWorkerThread.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(BckWorker_SETParam_WorkCompleted);
                this.Parameterization_BckWorkerThread.ProgressChanged -= new ProgressChangedEventHandler(BckWorker_SETParam_ProgressChanged);
                Param_Controller.ParameterSetStatus -= dlg.ConnController_ProcessStatusHandler;
                //dlg.btnCancel.Click -= new EventHandler(btn_CancelSETAsync_Parameters_Click);
                dlg.EnableProgressBar = false;
                dlg.okButton.Visible = true;
                //dlg.btnCancel.Enabled = false;
                Application_Controller.IsIOBusy = false;
            }
        }

        private void BckWorker_SETParam_ProgressChanged(object sender, ProgressChangedEventArgs e)
        { }

        #endregion

        //#region NETWORK Modes
        //private void UpdateNetworkModes()
        //{
        //    Param_Communication_Profile_object.Network_Mode.AutoSelection = ucCommProfile.rbAutoNetworkMode.Checked;
        //    Param_Communication_Profile_object.Network_Mode.Priority1 = (byte)cbxNetworkModePriority1.SelectedIndex;
        //    Param_Communication_Profile_object.Network_Mode.Priority2 = (byte)cbxNetworkModePriority2.SelectedIndex;
        //    Param_Communication_Profile_object.Network_Mode.Priority3 = (byte)cbxNetworkModePriority3.SelectedIndex;
        //}
        //#endregion

        #region Set_Param_Methods

        public void SET_Time_Based_Event_1()
        {
            try
            {


                Param_Controller.ParametersSETStatus.BuildStatusCollection("Time Based Event 1");
                if (TBE1.Control_Enum == Param_TimeBaseEvents.Tb_DateTime) //dateTime selected
                {
                    if (!TBE1.DateTime.IsValid)
                    {
                        throw new Exception("Datetime entered is INVALID. Year, Month and DayofMonth can not be Wild card entries");
                    }
                }
                else if (TBE1.Control_Enum == Param_TimeBaseEvents.Tb_Interval)
                {
                    //Limit of 18h required
                    if (TBE1.Interval > 64800) //18h x60x60
                        throw new Exception("Interval value should be less than or equal to 18 hours");
                }
                Data_Access_Result temp = Param_Controller.SET_TimeBaseEvents(TBE1, Get_Index._Time_Based_Event_1);
                #region Add_TimeBaseEvent_1_SET_Status

                Status TBESetStatus = new Status();
                TBESetStatus.OBIS_Code = Get_Index._Time_Based_Event_1;
                TBESetStatus.AttributeLabel = "Time Based Event 1";
                TBESetStatus.AttributeNo = 0x02;
                TBESetStatus.SETCommStatus = temp;
                Param_Controller.ParametersSETStatus.Current.AddCommandStatus(TBESetStatus);

                #endregion
            }
            catch (Exception ex)
            {
                Status TBESetStatus = new Status();
                TBESetStatus.OBIS_Code = Get_Index._Time_Based_Event_1;
                TBESetStatus.AttributeLabel = "Time Based Event 1";
                TBESetStatus.AttributeNo = 0x02;
                TBESetStatus.SETCommStatus = Data_Access_Result.Other_Reason;
                Param_Controller.ParametersSETStatus.Current.AddCommandStatus(TBESetStatus);
            }
        }

        public void SET_Time_Based_Event_2()
        {
            try
            {
                Param_Controller.ParametersSETStatus.BuildStatusCollection("Time Based Event 2");

                if (TBE2.Control_Enum == Param_TimeBaseEvents.Tb_DateTime) //dateTime selected
                {
                    if (!TBE2.DateTime.IsValid)
                    {
                        throw new Exception("Datetime entered is INVALID. Year, Month and DayofMonth can not be Wild card entries");
                    }

                }
                else if (TBE2.Control_Enum == Param_TimeBaseEvents.Tb_Interval)
                {
                    //Limit of 18h required
                    if (TBE2.Interval > 64800) //18h x60x60
                        throw new Exception("Interval value should be less than or equal to 18 hours");
                }
                Data_Access_Result temp = Param_Controller.SET_TimeBaseEvents(TBE2, Get_Index._Time_Based_Event_2);
                #region Add_TimeBaseEvent_2_SET_Status

                Status TBESetStatus = new Status();
                TBESetStatus.OBIS_Code = Get_Index._Time_Based_Event_2;
                TBESetStatus.AttributeLabel = "Time Based Event 2";
                TBESetStatus.AttributeNo = 0x02;
                TBESetStatus.SETCommStatus = temp;
                Param_Controller.ParametersSETStatus.Current.AddCommandStatus(TBESetStatus);

                #endregion
            }
            catch (Exception ex)
            {
                Status TBESetStatus = new Status();
                TBESetStatus.OBIS_Code = Get_Index._Time_Based_Event_2;
                TBESetStatus.AttributeLabel = "Time Based Event 2";
                TBESetStatus.AttributeNo = 0x02;
                TBESetStatus.SETCommStatus = Data_Access_Result.Other_Reason;
                Param_Controller.ParametersSETStatus.Current.AddCommandStatus(TBESetStatus);
            }
        }

        //private bool isDateTimeValid(DateTimeChooser Dt)
        //{
        //    //if (Dt.Year == StDateTime.NullYear && Dt.Month == StDateTime.Null && Dt.Date == StDateTime.Null && Dt.DayOfWeek == StDateTime.Null)
        //    //{
        //    //    return false;
        //    //}
        //    if (Dt.Year != StDateTime.NullYear && Dt.Month != StDateTime.Null && Dt.Month != StDateTime.DaylightSavingBegin
        //        && Dt.Month != StDateTime.DaylightSavingEnd && Dt.Date != StDateTime.LastDayOfMonth
        //        && Dt.Date != StDateTime.SecondLastDayOfMonth && Dt.Date != StDateTime.Null)
        //    {
        //        DateTime actualDate = new DateTime(Dt.Year, Dt.Month, Dt.Date);

        //        if (Dt.DayOfWeek == StDateTime.Null)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            switch (Dt.DayOfWeek)
        //            {
        //                case 1:
        //                    if (actualDate.DayOfWeek == DayOfWeek.Monday)
        //                        return true;
        //                    else return false;
        //                case 2:
        //                    if (actualDate.DayOfWeek == DayOfWeek.Tuesday)
        //                        return true;
        //                    else return false;
        //                case 3:
        //                    if (actualDate.DayOfWeek == DayOfWeek.Wednesday)
        //                        return true;
        //                    else return false;
        //                case 4:
        //                    if (actualDate.DayOfWeek == DayOfWeek.Thursday)
        //                        return true;
        //                    else return false;
        //                case 5:
        //                    if (actualDate.DayOfWeek == DayOfWeek.Friday)
        //                        return true;
        //                    else return false;
        //                case 6:
        //                    if (actualDate.DayOfWeek == DayOfWeek.Saturday)
        //                        return true;
        //                    else return false;
        //                case 7:
        //                    if (actualDate.DayOfWeek == DayOfWeek.Sunday)
        //                        return true;
        //                    else return false;
        //            }
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}

        void SET_ActivityCalendar()
        {
            ///Mark Invoke Passive Tariffication Calendar 
            ///***modified
            //if (rdbInvokeAction.Checked)
            //{
            //    Calendar.ExecuteActivateCalendarAction = true;
            //}
            //else
            //{
            //    Calendar.ExecuteActivateCalendarAction = false;
            //    Calendar.CalendarstartDate.SetDateTime(dtc_CalendarActivationDate.Value);
            //}

            Param_Controller.ParametersSETStatus.BuildStatusCollection("Tariffication");
            Param_Controller.SET_ActivityCalendar(Calendar);
        }

        void SET_CTPT()
        {
            Param_Controller.ParametersSETStatus.BuildStatusCollection("CT PT Ratio");
            Param_Controller.SET_CTPT_Param(Param_CTPT_Ratio_Object);
        }

        void SET_Clock_Misc()
        {
            Param_Controller.ParametersSETStatus.BuildStatusCollection("Meter Real Time Clock");
            Param_Controller.SET_Meter_Clock_MISC(Param_Clock_Caliberation_Object);
        }

        void SET_Clock()
        {
            Param_Controller.ParametersSETStatus.BuildStatusCollection("Meter Real Time Clock");
            Param_Controller.SET_Meter_Clock(Param_Clock_Caliberation_Object);
        }

        void SET_Contactor()
        {
            #region Commented_Section

            //Param_Contactor_object.Local_Control_FLAG_3 = check_Contactor_LocalControl.Checked;
            //Param_Contactor_object.Over_MDI_T1_FLAG_4 = check_Contactor_MDIOverLoad_T1.Checked;
            //Param_Contactor_object.Over_MDI_T2_FLAG_5 = check_Contactor_MDIOverLoad_T2.Checked;
            //Param_Contactor_object.Over_MDI_T3_FLAG_6 = check_Contactor_MDIOverLoad_T3.Checked;
            //Param_Contactor_object.Over_MDI_T4_FLAG_7 = check_Contactor_MDIOverLoad_T4.Checked;
            //Param_Contactor_object.Over_Current_By_Phase_T1_FLAG_0 = check_Contactor_OverCurrentByPhase_T1.Checked;
            //Param_Contactor_object.Over_Current_By_Phase_T2_FLAG_1 = check_Contactor_OverCurrentByPhase_T2.Checked;
            //Param_Contactor_object.Over_Current_By_Phase_T3_FLAG_2 = check_Contactor_OverCurrentByPhase_T3.Checked;
            //Param_Contactor_object.Over_Current_By_Phase_T4_FLAG_3 = check_Contactor_OverCurrentByPhase_T4.Checked;
            //Param_Contactor_object.Over_Load_By_Phase_T1_FLAG_4 = check_Contactor_OverLoadByPhase_T1.Checked;
            //Param_Contactor_object.Over_Load_By_Phase_T2_FLAG_5 = check_Contactor_OverLoadByPhase_T2.Checked;
            //Param_Contactor_object.Over_Load_By_Phase_T3_FLAG_6 = check_Contactor_OverLoadByPhase_T3.Checked;
            //Param_Contactor_object.Over_Load_By_Phase_T4_FLAG_7 = check_Contactor_OverLoadByPhase_T4.Checked;
            //Param_Contactor_object.Over_Load_T1_FLAG_0 = check_Contactor_OverLoadTotal_T1.Checked;
            //Param_Contactor_object.Over_Load_T2_FLAG_1 = check_Contactor_OverLoadTotal_T2.Checked;
            //Param_Contactor_object.Over_Load_T3_FLAG_2 = check_Contactor_OverLoadTotal_T3.Checked;
            //Param_Contactor_object.Over_Load_T4_FLAG_3 = check_Contactor_OverLoadTotal_T4.Checked;
            ////Param_Contactor_object.Local_Control_FLAG_3 = check_Contactor_LocalControl.Checked;
            //Param_Contactor_object.Over_Volt_FLAG_0 = check_Contactor_OverVolt.Checked;
            //Param_Contactor_object.Under_Volt_FLAG_1 = check_Contactor_UnderVolt.Checked;
            ///Param_Contactor_object.Remotely_Control_FLAG_2 = check_Contactor_RemoteControl.Checked;

            #endregion

            Param_Controller.ParametersSETStatus.BuildStatusCollection("Contactors");

            Param_Controller.SET_ContactorParams(Param_Contactor_Object);
            Param_Controller.SET_Limit_Over_Load_Total_T1(Param_Limits_Object);

            if (!Application_Controller.isSinglePhase)
            {
                Param_Controller.SET_Limit_Over_Load_Total_T2(Param_Limits_Object);
                Param_Controller.SET_Limit_Over_Load_Total_T3(Param_Limits_Object);
                Param_Controller.SET_Limit_Over_Load_Total_T4(Param_Limits_Object);
                Param_Controller.SET_MT_Over_Load(Param_Monitoring_Time_Object);
            }
        }

        void SET_CustomerReference()
        {
            Param_Controller.ParametersSETStatus.BuildStatusCollection("Customer Reference Number");
            Param_Controller.SET_Customer_Reference(Param_Customer_Code_Object);

            Param_Controller.ParametersSETStatus.BuildStatusCollection("Customer Name");
            Param_Controller.SET_Customer_Name(Param_Customer_Code_Object);

            Param_Controller.ParametersSETStatus.BuildStatusCollection("Customer Address");
            Param_Controller.SET_Customer_Address(Param_Customer_Code_Object);
        }
        void SET_CustomerName()
        {
            Param_Controller.ParametersSETStatus.BuildStatusCollection("Customer Name");
            Param_Controller.SET_Customer_Name(Param_Customer_Code_Object);
        }
        void SET_CustomerAddress()
        {
            Param_Controller.ParametersSETStatus.BuildStatusCollection("Customer Address");
            Param_Controller.SET_Customer_Address(Param_Customer_Code_Object);
        }

        void SET_DisplayWindows()
        {
            Notification N;
            try
            {
                if (Param_DisplayWindowsNormal == null || !Param_DisplayWindowsNormal.IsValid)
                {
                    N = new Notification("Error Programming Display Windows", "Display Windows NORMAL are not programmed");
                    return;
                }
                if (Param_DisplayWindowsAlternate == null || !Param_DisplayWindowsAlternate.IsValid)
                {
                    N = new Notification("Error Programming Display Windows", "Display Windows ALTERNATE are not programmed");
                    return;
                }
                Param_Controller.ParametersSETStatus.BuildStatusCollection("Display Windows");

                Param_Controller.Set_DisplayWindow_Normal(Param_DisplayWindowsNormal);
                Param_Controller.Set_DisplayWindow_Alternate(Param_DisplayWindowsAlternate);
            }
            catch (Exception ex)
            {
                Status TBESetStatus = new Status();
                TBESetStatus.OBIS_Code = Get_Index._Time_Based_Event_1;
                TBESetStatus.AttributeLabel = "Display Windows";
                TBESetStatus.AttributeNo = 0x02;
                TBESetStatus.SETCommStatus = Data_Access_Result.Other_Reason;
                Param_Controller.ParametersSETStatus.Current.AddCommandStatus(TBESetStatus);
                N = new Notification("Error Programming Display Windows", ex.Message, 1500);
            }
        }

        void SET_DisplayWindows_Nor()
        {
            //v10.0.19  implemented      //v10.0.20  Rights check added (bug Fixed) 
            bool FirstlyGet = (application_Controller.CurrentUser.CurrentAccessRights.DisplayWindowsRights.Find(x => x.QuantityName == "DisplayWindowsNormal" && x.Write) == null);
            TimeSpan ScrollTime_ToBeSet = new TimeSpan(0, 0, 15);
            if (FirstlyGet)
            {
                //((application_Controller.CurrentUser.CurrentAccessRights.DisplayWindowsRights.Find(x => x.QuantityName == "ScrollTime" && x.Write) == null)
                ScrollTime_ToBeSet = Param_DisplayWindowsNormal.ScrollTime;

                GET_DisplayWindows_Nor();
            }
            //


            try
            {
                if (Param_DisplayWindowsNormal == null || !Param_DisplayWindowsNormal.IsValid)
                {
                    Notification n = new Notification("Error", "Display Windows NORMAL are not programmed");
                    return;
                }
                ///if (Param_DisplayWindowsAlternate == null || !Param_DisplayWindowsAlternate.IsValid)
                ///{
                ///    MessageBox.Show("Display Windows ALTERNATE are not programmed");
                ///    return;
                ///}

                if (FirstlyGet)
                {
                    Param_DisplayWindowsNormal.ScrollTime = ScrollTime_ToBeSet; //v10.0.19
                }

                Param_Controller.ParametersSETStatus.BuildStatusCollection("Display Windows Normal");
                Param_Controller.Set_DisplayWindow_Normal(Param_DisplayWindowsNormal);
                //Param_Controller.Set_DisplayWindow_Alternate(Param_DisplayWindowsAlternate);
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("{0},{1}", (ex != null) ? ex.Message : "", (ex != null && ex.InnerException != null) ? ex.InnerException.Message : ""));

            }


        }

        void SET_DisplayWindows_Alt()
        {
            try
            {
                //if (Param_DisplayWindowsNormal == null || !Param_DisplayWindowsNormal.IsValid)
                //{
                //    MessageBox.Show("Display Windows NORMAL are not programmed");
                //    return;
                //}
                if (Param_DisplayWindowsAlternate == null || !Param_DisplayWindowsAlternate.IsValid)
                {
                    Notification n = new Notification("Error", "Display Windows Alternate are not programmed");
                    return;
                }
                Param_Controller.ParametersSETStatus.BuildStatusCollection("Display Windows Normal");
                //Param_Controller.Set_DisplayWindow_Normal(Param_DisplayWindowsNormal);
                Param_Controller.Set_DisplayWindow_Alternate(Param_DisplayWindowsAlternate);
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("{0},{1}", (ex != null) ? ex.Message : "", (ex != null && ex.InnerException != null) ? ex.InnerException.Message : ""));

            }
        }

        void SET_DisplayWindows_Test()
        {
            try
            {
                //if (Param_DisplayWindowsNormal == null || !Param_DisplayWindowsNormal.IsValid)
                //{
                //    MessageBox.Show("Display Windows NORMAL are not programmed");
                //    return;
                //}
                //if (Param_DisplayWindowstest == null || !Param_DisplayWindowstest.IsValid)
                //{
                //    Notification n = new Notification("Error", "Display Windows TestMode are not programmed");
                //    return;
                //}
                Param_Controller.ParametersSETStatus.BuildStatusCollection("Display Windows TestMode");
                //Param_Controller.Set_DisplayWindow_Normal(Param_DisplayWindowsNormal);
                Param_Controller.Set_DisplayWindow_test(Param_DisplayWindowsTest);
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("{0},{1}", (ex != null) ? ex.Message : "", (ex != null && ex.InnerException != null) ? ex.InnerException.Message : ""));
            }
        }

        void SET_DisplayPowerDown()
        {
            try
            {
                Param_Controller.ParametersSETStatus.BuildStatusCollection("Display Power Down");
                //Param_Controller.Set_DisplayWindow_Normal(Param_DisplayWindowsNormal);
                Param_Controller.SET_Display_PowerDown(Param_Display_PowerDown);
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("{0},{1}", (ex != null) ? ex.Message : "", (ex != null && ex.InnerException != null) ? ex.InnerException.Message : ""));
            }
        }

        void SET_DecimalPoint()
        {
            Param_Controller.ParametersSETStatus.BuildStatusCollection("Decimal Point");
            Param_Controller.SET_Decimal_Point(Param_Decimal_Point_Object);
        }

        void SET_DataProfilesWithEvents()
        {
            Param_Controller.ParametersSETStatus.BuildStatusCollection("Data Profile With Events");
        }

        void SET_EnergyParam()
        {
            Param_Controller.ParametersSETStatus.BuildStatusCollection("Energy Quadrants");
            Param_Controller.SET_EnergyParams(Param_Energy_Parameters_Object);
        }

        void SET_IPV4()
        {
            Param_Controller.ParametersSETStatus.BuildStatusCollection("IPv4");
        }

        void SET_IPProfile()
        {
            Param_Controller.ParametersSETStatus.BuildStatusCollection("IP Profiles");
        }

        void SET_KeepAlive()
        {
            Param_Controller.ParametersSETStatus.BuildStatusCollection("Keep Alive");
            Param_Controller.SET_Keep_Alive_IP(Param_Keep_Alive_IP_object);
        }

        void SET_KeepAlive_Standard()
        {
            Param_Controller.ParametersSETStatus.BuildStatusCollection("Keep Alive Standard");
            Param_Controller.SET_AutoConnect_Mode(Auto_Connect_Mode);
        }



        #region ///CanSet Rights Check
        /// <summary>
        /// Check in Current User Rights list that can be Set specific Limit? return true if Yes otherwise false
        /// Implemented by M. Azeem Inayat
        /// </summary>
        private bool CanSetLimit(Limits limitType)
        {
            foreach (var item in Application_Controller.CurrentUser.CurrentAccessRights.LimitsRights)
            {
                if ((Limits)Enum.Parse(item.QuantityType, item.QuantityName) == limitType)
                {
                    if (item.Write) return true;
                    else return false;
                }
            }
            return true;
        }
        private bool CanSetMonitoringTime(MonitoringTime mntrTimeType)
        {
            foreach (var item in Application_Controller.CurrentUser.CurrentAccessRights.MonitoringTimeRights)
            {
                if ((MonitoringTime)Enum.Parse(item.QuantityType, item.QuantityName) == mntrTimeType)
                {
                    if (item.Write) return true;
                    else return false;
                }
            }
            return true;
        }
        #endregion

        #region ///CanGet Rights Check
        /// <summary>
        /// Check in Current User Rights list that can be Get specific Limit? return true if Yes otherwise false
        /// Implemented by M. Azeem Inayat
        /// </summary>
        private bool CanGetLimit(Limits limitType)
        {
            foreach (var item in Application_Controller.CurrentUser.CurrentAccessRights.LimitsRights) //5.3.12 Uncommented
            {
                if ((Limits)Enum.Parse(item.QuantityType, item.QuantityName) == limitType)
                {
                    if (item.Read) return true;
                    else return false;
                }
            }
            return true;
        }
        private bool CanGetMonitoringTime(MonitoringTime mntrTimeType)
        {
            foreach (var item in Application_Controller.CurrentUser.CurrentAccessRights.MonitoringTimeRights) //5.3.12 Uncommented
            {
                if ((MonitoringTime)Enum.Parse(item.QuantityType, item.QuantityName) == mntrTimeType)
                {
                    if (item.Read) return true;
                    else return false;
                }
            }
            return true;
        }

        #endregion

        void SET_Limits()
        {
            Param_Controller.ParametersSETStatus.BuildStatusCollection("Limits");
            #region Limits

            //If Current user have rights to set then will be set in meter
            //Implemented by M. Azeem Inayat
            if (CanSetLimit(SharedCode.Comm.HelperClasses.Limits.CTFailLimitAmp))
                Param_Controller.SET_Limit_CT_Fail_AMP(Param_Limits_Object);
            if (CanSetLimit(SharedCode.Comm.HelperClasses.Limits.HighNeturalCurrent))
                Param_Controller.SET_Limit_High_Neutral_Current(Param_Limits_Object);
            if (CanSetLimit(SharedCode.Comm.HelperClasses.Limits.ImbalanceVolt))
                Param_Controller.SET_Limit_Imbalance_Voltage(Param_Limits_Object);
            if (CanSetLimit(SharedCode.Comm.HelperClasses.Limits.OverCurrentByPhaseT1))
                Param_Controller.SET_Limit_Over_Current_by_Phase_T1(Param_Limits_Object);
            if (CanSetLimit(SharedCode.Comm.HelperClasses.Limits.OverCurrentByPhaseT2))
                Param_Controller.SET_Limit_Over_Current_by_Phase_T2(Param_Limits_Object);
            if (CanSetLimit(SharedCode.Comm.HelperClasses.Limits.OverCurrentByPhaseT3))
                Param_Controller.SET_Limit_Over_Current_by_Phase_T3(Param_Limits_Object);
            if (CanSetLimit(SharedCode.Comm.HelperClasses.Limits.OverCurrentByPhaseT4))
                Param_Controller.SET_Limit_Over_Current_by_Phase_T4(Param_Limits_Object);
            if (CanSetLimit(SharedCode.Comm.HelperClasses.Limits.OverLoadByPhaseT1))
                Param_Controller.SET_Limit_Over_Load_by_Phase_T1(Param_Limits_Object);
            if (CanSetLimit(SharedCode.Comm.HelperClasses.Limits.OverLoadByPhaseT2))
                Param_Controller.SET_Limit_Over_Load_by_Phase_T2(Param_Limits_Object);
            if (CanSetLimit(SharedCode.Comm.HelperClasses.Limits.OverLoadByPhaseT3))
                Param_Controller.SET_Limit_Over_Load_by_Phase_T3(Param_Limits_Object);
            if (CanSetLimit(SharedCode.Comm.HelperClasses.Limits.OverLoadByPhaseT4))
                Param_Controller.SET_Limit_Over_Load_by_Phase_T4(Param_Limits_Object);
            if (CanSetLimit(SharedCode.Comm.HelperClasses.Limits.OverLoadTotalT1))
                Param_Controller.SET_Limit_Over_Load_Total_T1(Param_Limits_Object);
            if (CanSetLimit(SharedCode.Comm.HelperClasses.Limits.OverLoadTotalT2))
                Param_Controller.SET_Limit_Over_Load_Total_T2(Param_Limits_Object);
            if (CanSetLimit(SharedCode.Comm.HelperClasses.Limits.OverLoadTotalT3))
                Param_Controller.SET_Limit_Over_Load_Total_T3(Param_Limits_Object);
            if (CanSetLimit(SharedCode.Comm.HelperClasses.Limits.OverLoadTotalT4))
                Param_Controller.SET_Limit_Over_Load_Total_T4(Param_Limits_Object);
            if (CanSetLimit(SharedCode.Comm.HelperClasses.Limits.OverVolt))
                Param_Controller.SET_Limit_Over_Voltage(Param_Limits_Object);
            if (CanSetLimit(SharedCode.Comm.HelperClasses.Limits.PTFailLimitAmp))
                Param_Controller.SET_Limit_PT_Fail_AMP_or_OverLoad_1P(Param_Limits_Object);
            if (CanSetLimit(SharedCode.Comm.HelperClasses.Limits.PTFailLimitV))
                Param_Controller.SET_Limit_PT_Fail_Volt(Param_Limits_Object);
            if (CanSetLimit(SharedCode.Comm.HelperClasses.Limits.ReverseEnergykWh))
                Param_Controller.SET_Limit_Reverse_Energy_Limit(Param_Limits_Object);
            if (CanSetLimit(SharedCode.Comm.HelperClasses.Limits.TemperEnergykWh))
                Param_Controller.SET_Limit_Tamper_Energy(Param_Limits_Object);
            if (CanSetLimit(SharedCode.Comm.HelperClasses.Limits.UnderVolt))
                Param_Controller.SET_Limit_Under_Voltage(Param_Limits_Object);

            /* for Single Phase .... // Azeem */
            if (CanSetLimit(SharedCode.Comm.HelperClasses.Limits.OverCurrent_Phase))
                Param_Controller.SET_Limit_Over_Current_Phase(Param_Limits_Object);
            if (CanSetLimit(SharedCode.Comm.HelperClasses.Limits.Meter_ON_Load))
                Param_Controller.SET_Limit_Meter_On_Load(Param_Limits_Object);
            if (CanSetLimit(SharedCode.Comm.HelperClasses.Limits.PowerFactor_Change))
                Param_Controller.SET_Limit_Power_Factor_Change(Param_Limits_Object);
            if (CanSetLimit(SharedCode.Comm.HelperClasses.Limits.CrestFactorLow))
                Param_Controller.SET_Limit_Crest_Factor_Low(Param_Limits_Object);
            if (CanSetLimit(SharedCode.Comm.HelperClasses.Limits.CrestFactorHigh))
                Param_Controller.SET_Limit_Crest_Factor_High(Param_Limits_Object);
            if (CanSetLimit(SharedCode.Comm.HelperClasses.Limits.OverPower))
                Param_Controller.SET_Limit_OverPower(Param_Limits_Object);


            #region ///Init Param_Limit_Demand_Overload

            Param_Limit_Demand_OverLoad Param_Limit_Demand_OverLoad_T1 = new Param_Limit_Demand_OverLoad()
            {
                Threshold = Param_Limits_Object.DemandOverLoadTotal_T1
            };

            Param_Limit_Demand_OverLoad Param_Limit_Demand_OverLoad_T2 = new Param_Limit_Demand_OverLoad()
            {
                Threshold = Param_Limits_Object.DemandOverLoadTotal_T2
            };

            Param_Limit_Demand_OverLoad Param_Limit_Demand_OverLoad_T3 = new Param_Limit_Demand_OverLoad()
            {
                Threshold = Param_Limits_Object.DemandOverLoadTotal_T3
            };

            Param_Limit_Demand_OverLoad Param_Limit_Demand_OverLoad_T4 = new Param_Limit_Demand_OverLoad()
            {
                Threshold = Param_Limits_Object.DemandOverLoadTotal_T4
            };

            #endregion

            if (CanSetLimit(SharedCode.Comm.HelperClasses.Limits.MDIExceedT1))
                Param_Controller.SET_Limit_Demand_OverLoad_T1(Param_Limit_Demand_OverLoad_T1);
            if (CanSetLimit(SharedCode.Comm.HelperClasses.Limits.MDIExceedT2))
                Param_Controller.SET_Limit_Demand_OverLoad_T2(Param_Limit_Demand_OverLoad_T2);
            if (CanSetLimit(SharedCode.Comm.HelperClasses.Limits.MDIExceedT3))
                Param_Controller.SET_Limit_Demand_OverLoad_T3(Param_Limit_Demand_OverLoad_T3);
            if (CanSetLimit(SharedCode.Comm.HelperClasses.Limits.MDIExceedT4))
                Param_Controller.SET_Limit_Demand_OverLoad_T4(Param_Limit_Demand_OverLoad_T4);

            if (CanSetLimit(SharedCode.Comm.HelperClasses.Limits.Contactor_Failure_Pwr))
                Param_Controller.TrySET_ContactorFailure_Limit(Param_Limits_Object);

            #endregion
        }

        void SET_LoadProfileChannels(LoadProfileScheme LP_Scheme)
        {
            try
            {
                List<LoadProfileChannelInfo> LPChannels = null;
                if (LP_Scheme == LoadProfileScheme.Load_Profile) LPChannels = ucLoadProfile.LoadProfileChannelsInfo;
                else if (LP_Scheme == LoadProfileScheme.Load_Profile_Channel_2) LPChannels = ucLoadProfile.LoadProfileChannelsInfo_2;
                Param_Controller.ParametersSETStatus.BuildStatusCollection(string.Format("Load Profile_{0} Channel", (byte)LP_Scheme));
                Data_Access_Result result = this.LoadProfile_Controller.Set_LoadProfileChannels(LPChannels, LP_Scheme);

                #region Add Load Profile Channels SET Status

                Status LPSetStatus = new Status();
                LPSetStatus = new Status();
                LPSetStatus.OBIS_Code = LoadProfileController.GetLoadProfileIndex(LP_Scheme);// Get_Index.Load_Profile;
                LPSetStatus.AttributeLabel = "Capture Buffer";
                LPSetStatus.AttributeNo = 0x03;
                LPSetStatus.SETCommStatus = result;
                Param_Controller.ParametersSETStatus.Current.AddCommandStatus(LPSetStatus);

                #endregion
            }
            catch (Exception ex)
            {
                #region Add Load Profile Channels SET Status
                Status LPSetStatus = new Status();
                LPSetStatus.OBIS_Code = LoadProfileController.GetLoadProfileIndex(LP_Scheme);
                LPSetStatus.AttributeLabel = "Capture Buffer";
                LPSetStatus.AttributeNo = 0x03;
                LPSetStatus.SETCommStatus = Data_Access_Result.Other_Reason;
                Param_Controller.ParametersSETStatus.Current.AddCommandStatus(LPSetStatus);
                #endregion
            }
        }

        void SET_LoadProfileChannels_Interval(LoadProfileScheme LP_Scheme)
        {
            try
            {
                TimeSpan LPPeriod = new TimeSpan();
                if (LP_Scheme == LoadProfileScheme.Load_Profile)
                    LPPeriod = LoadProfilePeriod;
                else if (LP_Scheme == LoadProfileScheme.Load_Profile_Channel_2)
                    LPPeriod = LoadProfilePeriod_2;
                else if (LP_Scheme == LoadProfileScheme.Daily_Load_Profile)
                    LPPeriod = PQLoadProfilePeriod;

                Param_Controller.ParametersSETStatus.BuildStatusCollection(string.Format("Load Profile_{0} Interval", (byte)LP_Scheme));
                Data_Access_Result result = this.LoadProfile_Controller.Set_LoadProfileInterval(LPPeriod, LP_Scheme);

                #region Add Load Profile Channels SET Status

                Status LPSetStatus = new Status();
                LPSetStatus.OBIS_Code = LoadProfileController.GetLoadProfileIndex(LP_Scheme);
                LPSetStatus.AttributeLabel = "Capture Period";
                LPSetStatus.AttributeNo = 0x04;
                LPSetStatus.SETCommStatus = result;
                Param_Controller.ParametersSETStatus.Current.AddCommandStatus(LPSetStatus);

                #endregion
            }
            catch (Exception ex)
            {
                #region Add Load Profile Channels SET Status
                Status LPSetStatus = new Status();
                LPSetStatus.OBIS_Code = LoadProfileController.GetLoadProfileIndex(LP_Scheme);
                LPSetStatus.AttributeLabel = "Capture Period";
                LPSetStatus.AttributeNo = 0x04;
                LPSetStatus.SETCommStatus = Data_Access_Result.Other_Reason;
                Param_Controller.ParametersSETStatus.Current.AddCommandStatus(LPSetStatus);
                #endregion
            }
        }

        void SET_MajorAlarmProfile()
        {
            Param_Controller.ParametersSETStatus.BuildStatusCollection("Major Alarm Profiles");
        }

        void SET_ModemLimitsAndTime()
        {
            Param_Controller.ParametersSETStatus.BuildStatusCollection("Modem Limits and Time");
            Param_Controller.SET_ModemLimitsAndTime(Param_ModemLimitsAndTime_Object);
        }

        void SET_MonitoringTime()
        {
            Param_Controller.ParametersSETStatus.BuildStatusCollection("Monitoring Times");
            #region Monitoring Times

            if (false)
            {
                Param_Controller.SET_MT_CT_Fail(Param_Monitoring_Time_Object);
                Param_Controller.SET_MT_High_Neutral_Current(Param_Monitoring_Time_Object);
                Param_Controller.SET_MT_Imbalance_Volt(Param_Monitoring_Time_Object);
                Param_Controller.SET_MT_Over_Current(Param_Monitoring_Time_Object);
                Param_Controller.SET_MT_Over_Load(Param_Monitoring_Time_Object);
                Param_Controller.SET_MT_Over_Volt(Param_Monitoring_Time_Object);
                Param_Controller.SET_MT_Phase_Fail(Param_Monitoring_Time_Object);
                Param_Controller.SET_MT_Phase_Sequence(Param_Monitoring_Time_Object);
                Param_Controller.SET_MT_Power_Fail(Param_Monitoring_Time_Object);
                Param_Controller.SET_MT_Power_Up_Delay_For_Energy_Recording(Param_Monitoring_Time_Object);
                Param_Controller.SET_MT_Power_Up_Delay_To_Monitor(Param_Monitoring_Time_Object);
                Param_Controller.SET_MT_PT_Fail(Param_Monitoring_Time_Object);
                Param_Controller.SET_MT_Reverse_Energy(Param_Monitoring_Time_Object);
                Param_Controller.SET_MT_Reverse_Polarity(Param_Monitoring_Time_Object);
                Param_Controller.SET_MT_Tamper_Energy(Param_Monitoring_Time_Object);
                Param_Controller.SET_MT_Under_Volt(Param_Monitoring_Time_Object);
            }
            else
            {
                if (CanSetMonitoringTime(MonitoringTime.CTFail))
                    Param_Controller.SET_MT_CT_Fail(Param_Monitoring_Time_Object);
                if (CanSetMonitoringTime(MonitoringTime.HighNeturalCurrent))
                    Param_Controller.SET_MT_High_Neutral_Current(Param_Monitoring_Time_Object);
                if (CanSetMonitoringTime(MonitoringTime.ImbalanceVolt))
                    Param_Controller.SET_MT_Imbalance_Volt(Param_Monitoring_Time_Object);
                if (CanSetMonitoringTime(MonitoringTime.OverCurrent))
                    Param_Controller.SET_MT_Over_Current(Param_Monitoring_Time_Object);
                if (CanSetMonitoringTime(MonitoringTime.OverLoad))
                    Param_Controller.SET_MT_Over_Load(Param_Monitoring_Time_Object);
                if (CanSetMonitoringTime(MonitoringTime.OverVolt))
                    Param_Controller.SET_MT_Over_Volt(Param_Monitoring_Time_Object);
                if (CanSetMonitoringTime(MonitoringTime.PhaseFail))
                    Param_Controller.SET_MT_Phase_Fail(Param_Monitoring_Time_Object);
                if (CanSetMonitoringTime(MonitoringTime.PhaseSequence))
                    Param_Controller.SET_MT_Phase_Sequence(Param_Monitoring_Time_Object);
                if (CanSetMonitoringTime(MonitoringTime.PowerFail))
                    Param_Controller.SET_MT_Power_Fail(Param_Monitoring_Time_Object);
                if (CanSetMonitoringTime(MonitoringTime.PUDForEnergyRecording))
                    Param_Controller.SET_MT_Power_Up_Delay_For_Energy_Recording(Param_Monitoring_Time_Object);
                if (CanSetMonitoringTime(MonitoringTime.PUDToMoniter))
                    Param_Controller.SET_MT_Power_Up_Delay_To_Monitor(Param_Monitoring_Time_Object);
                if (CanSetMonitoringTime(MonitoringTime.PTFail))
                    Param_Controller.SET_MT_PT_Fail(Param_Monitoring_Time_Object);
                if (CanSetMonitoringTime(MonitoringTime.ReverseEnergy))
                    Param_Controller.SET_MT_Reverse_Energy(Param_Monitoring_Time_Object);
                if (CanSetMonitoringTime(MonitoringTime.ReversePolarity))
                    Param_Controller.SET_MT_Reverse_Polarity(Param_Monitoring_Time_Object);
                if (CanSetMonitoringTime(MonitoringTime.TemperEnergy))
                    Param_Controller.SET_MT_Tamper_Energy(Param_Monitoring_Time_Object);
                if (CanSetMonitoringTime(MonitoringTime.UnderVolt))
                    Param_Controller.SET_MT_Under_Volt(Param_Monitoring_Time_Object);
                if (CanSetMonitoringTime(MonitoringTime.ContactorFailure))
                    Param_Controller.SET_MT_Contactor_Failure(Param_Monitoring_Time_Object);
            }

            #endregion            //Array.Resize(ref Data_Access_Results, total_objects);
        }

        void SET_ModemInitialize()
        {
            Param_Controller.ParametersSETStatus.BuildStatusCollection("Modem Initialization APN and Username");
            Param_Controller.SET_ModemBasics(Param_Modem_Initialize_Object);
            Param_Controller.ParametersSETStatus.BuildStatusCollection("Modem Initialization Complete");
            Param_Controller.SET_ModemBasicsNew(Param_ModemBasics_NEW_object);
        }

        void SET_Passwords()
        {
            Param_Controller.ParametersSETStatus.BuildStatusCollection("Passwords");

            SharedCode.Comm.HelperClasses.Customized_Encoder.Encode_Any(Application_Controller.Param_Controller.GetSAPEntry,
                            15, Get_Index.Current_Association1, 7, DataTypes._A09_octet_string, Param_Password_Object.Management_Device);
            SharedCode.Comm.HelperClasses.Customized_Encoder.Encode_Any(Application_Controller.Param_Controller.GetSAPEntry,
                            15, Get_Index.Current_Association2, 7, DataTypes._A09_octet_string, Param_Password_Object.Electrical_Device);
        }

        //void Set_Password_Managerial()
        //{
        //    try
        //    {
        //        Param_Controller.ParametersSETStatus.BuildStatusCollection("Management Logical Device Password");
        //        Param_Controller.Set_Password_ManagementDevice(param_password_object.Management_Device);

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        void Set_Password_Electrical()
        {
            try
            {
                Param_Controller.ParametersSETStatus.BuildStatusCollection("Electrical Logical Device Password");
                Param_Controller.Set_CurrentAssociationPassword(Param_Password_Object.Management_Device);

            }
            catch (Exception ex)
            {

            }
        }

        void SET_TCPUDP()
        {
            Param_Controller.ParametersSETStatus.BuildStatusCollection("TCP UDP");
        }

        void SET_MDIParams()
        {
            Param_Controller.ParametersSETStatus.BuildStatusCollection("Maximum Demand Interval");
            Param_Controller.SET_MDI_Parameters(Param_MDI_Parameters_Object);
        }

        void SET_Communicationprofile()
        {
            Param_Controller.ParametersSETStatus.BuildStatusCollection("Coummunication Profile");
            Param_Controller.SET_Communication_Profiles(Param_Communication_Profile_object);

        }

        void SET_WakeupProfile()
        {
            Param_Controller.ParametersSETStatus.BuildStatusCollection("WakeUp Profile");
            Param_Wakeup_Profile_object = ucWakeupProfile.Param_Wakeup_Profile_object;
            Param_Controller.SET_WakeUp_Profile(Param_Wakeup_Profile_object);
        }

        void SET_IPProfiles()
        {
            Param_Controller.ParametersSETStatus.BuildStatusCollection("IP Profiles");
            Param_Controller.SET_IP_Profiles(Param_IP_Profiles_object);
        }
        void SET_StandardIPProfiles()
        {
            Param_Controller.ParametersSETStatus.BuildStatusCollection("Standard IP Profiles");
            Param_Controller.SET_Standard_IP_Profiles(Param_Standard_IP_Profiles_object);
        }

        void SET_NumberProfiles()
        {
            Param_Controller.ParametersSETStatus.BuildStatusCollection("Number Profiles");
            Param_Number_Profile_object = ucNumberProfile.Param_Number_Profile_object;
            Param_Controller.SET_Number_Profiles(Param_Number_Profile_object);
        }
        void SET_NumberProfile_Standard()
        {
            Param_Controller.ParametersSETStatus.BuildStatusCollection("Number Profiles Standard");
            Data_Access_Result result = Data_Access_Result.Other_Reason;
            int profilesCount = ucStandardModem.cmbStandardNumberProfile.SelectedIndex + 1;
            if (SaveNumberProfile(profilesCount))
            {
                Param_Standard_Number_Profile[] Profile_object = (Param_Standard_Number_Profile[])Param_Standard_Number_Profile_object.Clone();
                Array.Resize(ref Profile_object, profilesCount);
                result = Param_Controller.SET_Number_Profiles_AllowedCallers(Profile_object);
            }
            ////#region Add  SET Status
            //Status SetStatus = new Status();
            //SetStatus.OBIS_Code = Get_Index.AutoAnswer;
            //SetStatus.AttributeLabel = "Standard Number Profile";
            //SetStatus.AttributeNo = 0x07;
            //SetStatus.SETCommStatus = result;
            //Param_Controller.ParametersSETStatus.Current.AddCommandStatus(SetStatus);
            //#endregion
        }

        void SET_KeepAliveIP(ref DLMS.Data_Access_Result[] Data_Access_Results, ref int total_objects)
        {
            Param_Controller.ParametersSETStatus.BuildStatusCollection("Keep Alive IP");
        }

        void SET_GeneralProcess()
        {
            try
            {
                Param_Controller.ParametersSETStatus.BuildStatusCollection("General Process");
                //Param_Controller.Set_DisplayWindow_Normal(Param_DisplayWindowsNormal);
                Param_Controller.SET_General_Process(Param_GeneralProcess);
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("{0},{1}", (ex != null) ? ex.Message : "", (ex != null && ex.InnerException != null) ? ex.InnerException.Message : ""));
            }
        }

        Action_Result SET_SecurityKey(byte[] keyValue, KEY_ID keyType)
        {

            Param_Controller.ParametersSETStatus.BuildStatusCollection("Security Key:" + keyType.ToString());

            return Param_Controller.Set_SecurityKey(keyValue, keyType);
        }

        void SET_SecurityPolicy(Security_Policy securityPolicy)
        {

            Param_Controller.ParametersSETStatus.BuildStatusCollection("Security Policy");

            Action_Result result = Param_Controller.Set_SecurityPolicy(securityPolicy);
        }
        #endregion

        #region Load Shedding
        void SET_SchedulerTable()
        {
            try
            {
                Param_Controller.ParametersSETStatus.BuildStatusCollection("Scheduler Table");
                StOBISCode obis = Get_Index.SCHEDULE_TABLE;
                Data_Access_Result result = Param_Controller.SET_SchedulerTable(obis, ucScheduleTableEntry1.ParamLoadScheduling.ListOfEntries);

                Status TBESetStatus = new Status();
                TBESetStatus.OBIS_Code = obis.OBISIndex;
                TBESetStatus.AttributeLabel = "Schedular Table";
                TBESetStatus.AttributeNo = 0x02;
                TBESetStatus.SETCommStatus = result;
                Param_Controller.ParametersSETStatus.Current.AddCommandStatus(TBESetStatus);
            }
            catch (Exception ex)
            {
                Status TBESetStatus = new Status();
                TBESetStatus.OBIS_Code = Get_Index.SCHEDULE_TABLE;
                TBESetStatus.AttributeLabel = "Schedular Table";
                TBESetStatus.AttributeNo = 0x02;
                TBESetStatus.SETCommStatus = Data_Access_Result.Other_Reason;
                Param_Controller.ParametersSETStatus.Current.AddCommandStatus(TBESetStatus);
            }


        }
        void GET_SchedulerTable()
        {
            Param_Controller.ParametersGETStatus.BuildStatusCollection("Scheduler Table");
            StOBISCode obis = Get_Index.SCHEDULE_TABLE;

            List<ScheduleEntry> list = new List<ScheduleEntry>();
            Param_Controller.GET_SchedulerTable(obis, ref list);

            loadedConfigurationSet.ParamLoadShedding.ListOfEntries = list;

        }

        #endregion

        #region Generator Start
        void SET_GeneratorStart()
        {
            //*** Setting Generator Start Monitoring Time
            try
            {
                Param_Controller.ParametersSETStatus.BuildStatusCollection("Generator Start");

                Data_Access_Result temp = Param_Controller.SET_Generator_Start_Monitoring_Time(ucGeneratorStart1.ParamGeneratorStart);

                Status TBESetStatus = new Status();
                TBESetStatus.OBIS_Code = Get_Index.MonitoringTime_GENERATOR_START;
                TBESetStatus.AttributeLabel = "Generator Start Monitoring Time";
                TBESetStatus.AttributeNo = 0x02;
                TBESetStatus.SETCommStatus = temp;
                Param_Controller.ParametersSETStatus.Current.AddCommandStatus(TBESetStatus);
            }
            catch (Exception ex)
            {
                Status TBESetStatus = new Status();
                TBESetStatus.OBIS_Code = Get_Index.MonitoringTime_GENERATOR_START;
                TBESetStatus.AttributeLabel = "Generator Start Monitoring Time";
                TBESetStatus.AttributeNo = 0x02;
                TBESetStatus.SETCommStatus = Data_Access_Result.Other_Reason;
                Param_Controller.ParametersSETStatus.Current.AddCommandStatus(TBESetStatus);
            }

            //*** Setting Tariff on Generator Start
            try
            {
                Data_Access_Result temp = Param_Controller.SET_Generator_Start_Tariff(ucGeneratorStart1.ParamGeneratorStart);

                Status TBESetStatus = new Status();
                TBESetStatus.OBIS_Code = Get_Index.TARIFF_ON_GENERATOR;
                TBESetStatus.AttributeLabel = "Tariff on Generator Start";
                TBESetStatus.AttributeNo = 0x02;
                TBESetStatus.SETCommStatus = temp;
                Param_Controller.ParametersSETStatus.Current.AddCommandStatus(TBESetStatus);
            }
            catch (Exception ex)
            {
                Status TBESetStatus = new Status();
                TBESetStatus.OBIS_Code = Get_Index.TARIFF_ON_GENERATOR;
                TBESetStatus.AttributeLabel = "Tariff on Generator Start";
                TBESetStatus.AttributeNo = 0x02;
                TBESetStatus.SETCommStatus = Data_Access_Result.Other_Reason;
                Param_Controller.ParametersSETStatus.Current.AddCommandStatus(TBESetStatus);
            }

        }
        void GET_GeneratorStart()
        {
            Param_Controller.ParametersGETStatus.BuildStatusCollection("Generator Start");

            Param_Generator_Start obj = new Param_Generator_Start();
            Param_Controller.GET_Generator_Start(ref obj);
            loadedConfigurationSet.ParamGeneratorStart = obj;
        }

        #endregion

        #region RF_CHANNELS

        #endregion


        private void Update_GUI()
        {
            showToGUI_ALL();
            Application.DoEvents();
        }

        public void pnlParameterization_Load(object sender, EventArgs e)
        {
            try
            {
                Limits = new LimitValues(Application_Controller.CurrentMeterName);
                InitializeUcModemParameters();
                #region //Interface_Init_Work

                //=========================================================================================================
                //=========================================================================================================
                if (Application_Controller != null)
                {
                    obj_CustomerCode = new Param_Customer_Code(); //Added by Azeem
                    Param_Controller = Application_Controller.Param_Controller;
                    Application_Process = Application_Controller.Applicationprocess_Controller.ApplicationProcess;
                    connectionManager = Application_Controller.ConnectionManager;
                    AP_Controller = Application_Controller.Applicationprocess_Controller;
                    LoadProfile_Controller = Application_Controller.LoadProfile_Controller;
                    ConnController = Application_Controller.ConnectionController;
                    Instantanous_Controller = Application_Controller.Billing_Controller;
                    if (ucDisplayWindows1 != null)
                        ucDisplayWindows1.Obj_ApplicationController = Application_Controller;
                    if (ucStatusWordMap1 != null)
                        ucStatusWordMap1.Obj_ApplicationController = Application_Controller;



                }
                Param_MDI_Parameters_Object.Auto_reset_date = new StDateTime();

                //=========================================================================================================
                dlg = new ProgressDialog();

                #region Datagrid Sorting Disable

                foreach (DataGridViewColumn column in Grid_Debug.Columns)
                { column.SortMode = DataGridViewColumnSortMode.NotSortable; }

                #endregion
                #endregion
                try
                {
                    //Init ucLoadProfile
                    this.AllSelectableChannels = LoadProfile_Controller.Get_SelectableLoadProfileChannels();
                    ucLoadProfile.Init_LoadProfiles(AllSelectableChannels);
                    loadedConfigurationSet = new ParamConfigurationSet();

                    Modem_Warnings_disable = true;
                    DirectoryInfo dictoryInfo = new DirectoryInfo(Environment.CurrentDirectory + "\\DLMS_saved_files\\");
                    if (dictoryInfo.Exists)
                        Load_and_show_all(Environment.CurrentDirectory + "\\DLMS_saved_files\\");
                    Modem_Warnings_disable = false;
                    //Set Startup Values
                    combo_ClockInterval.SelectedIndex = (combo_ClockInterval.Items.Count > 0) ? 0 : -1;
                    combo_ClkTimeToAdd.SelectedIndex = (combo_ClkTimeToAdd.Items.Count > 0) ? 0 : -1;
                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine(ex);
                }
                try
                {
                    //Init ucDisplayWindows
                    if (Param_Controller != null)
                    {
                        Param_Controller.DisplayWindowsHelper_Obj = new DisplayWindowsHelper();
                        Param_Controller.DisplayWindowsHelper_Obj.LoadSelectableDisplayWindows(Application_Controller.Configurations);
                        //Param_Controller.DisplayWindowsHelper = new DisplayWindowsHelper();
                        //Param_Controller.DisplayWindowsHelper.LoadSelectableDisplayWindows(Application_Controller.Configurations);
                    }
                    InitializeUcDisplayWindowsParameters();
                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine("Error Initialize ucDisplay Windows" + ex);
                }
                bgw_getReverseEnergyMT = new BackgroundWorker();
                bgw_getReverseEnergyMT.DoWork += new DoWorkEventHandler(bgw_getReverseEnergy_DoWork);
                bgw_getReverseEnergyMT.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_getReverseEnergy_RunWorkerCompleted);

                bgw_setReverseEnergyMT = new BackgroundWorker();
                bgw_setReverseEnergyMT.DoWork += new DoWorkEventHandler(bgw_setReverseEnergy_DoWork);
                bgw_setReverseEnergyMT.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_setReverseEnergy_RunWorkerCompleted);

                bgw_getEarthMT = new BackgroundWorker();
                bgw_getEarthMT.DoWork += new DoWorkEventHandler(bgw_getEarth_DoWork);
                bgw_getEarthMT.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_getEarth_RunWorkerCompleted);

                bgw_setEarthMT = new BackgroundWorker();
                bgw_setEarthMT.DoWork += new DoWorkEventHandler(bgw_setEarth_DoWork);
                bgw_setEarthMT.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_setEarth_RunWorkerCompleted);

                bgw_setPowerFailMT = new BackgroundWorker();
                bgw_setPowerFailMT.DoWork += new DoWorkEventHandler(bgw_setPowerFailMT_DoWork);
                bgw_setPowerFailMT.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_setPowerFailMT_RunWorkerCompleted);

                bgw_SP_getOverLoadTotal = new BackgroundWorker();
                bgw_SP_getOverLoadTotal.DoWork += new DoWorkEventHandler(bgw_SP_getOverLoadTotal_DoWork);
                bgw_SP_getOverLoadTotal.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_SP_getOverLoadTotal_RunWorkerCompleted);

                bgw_SP_setOverLoadTotal = new BackgroundWorker();
                bgw_SP_setOverLoadTotal.DoWork += new DoWorkEventHandler(bgw_SP_setOverLoadTotal_DoWork);
                bgw_SP_setOverLoadTotal.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_SP_setOverLoadTotal_RunWorkerCompleted);
                Application.DoEvents();
            }
            catch
            {
            }
            finally
            {
            }
            tbcMeterParams.BackColor = Color.Transparent;
            Tab_Main.BackColor = Color.Transparent;

            //Tab_Main.Appearance = TabAppearance.Buttons;
            //tbcMeterParams.Appearance = TabAppearance.FlatButtons;
            //tbcMeterParams.BackColor = Color.Transparent;
            //Tab_Main.BackColor = Color.Transparent;
        }

        #region Functions_for_initialzing

        void Param_IP_Profiles_initailze(int Instances)
        {
            int count;
            for (count = 0; count < Instances; count++)
            {
                Param_IP_Profiles_object[count] = new Param_IP_Profiles();
            }
        }

        void Param_Number_Profile_object_initialze(int Instances)
        {
            int count;
            for (count = 0; count < Instances; count++)
            {
                Param_Number_Profile_object[count] = new Param_Number_Profile();
                Param_Number_Profile_object[count].Number = new byte[16] { 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15 };
                Param_Number_Profile_object[count].Datacall_Number = new byte[16] { 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15 };

            }
            if (Param_Number_Profile_object.Length > 4)
            {
                Param_Number_Profile_object[4].Number = new byte[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                Param_Number_Profile_object[4].Datacall_Number = new byte[16] { 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15 };
            }
        }

        void Param_Wakeup_Profile_object_initialze(int Instances)
        {
            int count;
            for (count = 0; count < Instances; count++)
            {
                Param_Wakeup_Profile_object[count] = new Param_WakeUp_Profile();
            }
        }

        //void Param_Display_windows_object_initialze(int Instances)
        //{
        //    //int count;
        //    //for (count = 0; count < Instances; count++)
        //    //{
        //    //    Param_Display_windows_object[count] = new Param_Display_windows();
        //    //}
        //}

        public void InitializeUcModemParameters()
        {
            #region Initialize_Variables

            Param_IP_Profiles[] _param_IP_Profiles_Obj = null;
            Param_WakeUp_Profile[] _param_WakeUp_Profiles_Obj = null;
            Param_Number_Profile[] _param_Number_Profiles_Obj = null;

            _param_IP_Profiles_Obj = Param_IP_Profiles_object;
            _param_WakeUp_Profiles_Obj = Param_Wakeup_Profile_object;
            _param_Number_Profiles_Obj = Param_Number_Profile_object;

            ///Param_IP_Profiles_object 
            if (_param_IP_Profiles_Obj == null ||
                _param_IP_Profiles_Obj.Length != Param_IP_ProfilesHelper.Max_IP_Profile)
            {
                _param_IP_Profiles_Obj = Param_IP_ProfilesHelper.Param_IP_Profiles_initailze(Param_IP_ProfilesHelper.Max_IP_Profile);
                Param_IP_Profiles_object = _param_IP_Profiles_Obj;
            }
            if (Param_IP_ProfilesHelperObj == null ||
                Param_IP_ProfilesHelperObj.Param_IP_Profiles_object != _param_IP_Profiles_Obj)
                Param_IP_ProfilesHelperObj = new Param_IP_ProfilesHelper(_param_IP_Profiles_Obj);

            ///Param_Wakeup_Profile_object
            if (_param_WakeUp_Profiles_Obj == null ||
                _param_WakeUp_Profiles_Obj.Length != Param_WakeUp_ProfileHelper.Max_WakeUp_Profile)
            {
                _param_WakeUp_Profiles_Obj = Param_WakeUp_ProfileHelper.Param_Wakeup_Profile_object_initialze(Param_WakeUp_ProfileHelper.Max_WakeUp_Profile);
                Param_Wakeup_Profile_object = _param_WakeUp_Profiles_Obj;
            }
            if (Param_WakeUp_ProfileHelperObj == null ||
               Param_WakeUp_ProfileHelperObj.Param_WakeUp_Profile_object != _param_WakeUp_Profiles_Obj)
                Param_WakeUp_ProfileHelperObj = new Param_WakeUp_ProfileHelper(_param_WakeUp_Profiles_Obj);

            ///Param_Number_Profile_object
            if (_param_Number_Profiles_Obj == null ||
                !(_param_Number_Profiles_Obj.Length == Param_Number_ProfileHelper.Max_Number_Profile ||
                _param_Number_Profiles_Obj.Length == Param_Number_ProfileHelper.Max_Number_Profile + 1))
            {
                _param_Number_Profiles_Obj = Param_Number_ProfileHelper.
                    Param_Number_Profile_object_initialze(Param_Number_ProfileHelper.Max_Number_Profile + 1);
                Param_Number_Profile_object = _param_Number_Profiles_Obj;
            }
            if (Param_Number_ProfileHelperObj == null ||
                Param_Number_ProfileHelperObj.Param_Number_Profiles_object != _param_Number_Profiles_Obj)
                Param_Number_ProfileHelperObj = new Param_Number_ProfileHelper(_param_Number_Profiles_Obj);

            #endregion

            ///Initialize ucIPProfiles
            if (ucIPProfiles != null)
            {
                ucIPProfiles.Param_IP_ProfilesHelper = Param_IP_ProfilesHelperObj;
                ucIPProfiles.Param_IP_Profiles_object = _param_IP_Profiles_Obj;
                ucIPProfiles.Param_IPV4_object = Param_IPV4_object;
                ucIPProfiles.Param_WakeUp_ProfileHelperObj = Param_WakeUp_ProfileHelperObj;
            }

            ///Initialize ucWakeupProfiles
            if (ucWakeupProfile != null)
            {
                ucWakeupProfile.Param_IP_ProfilesHelper = Param_IP_ProfilesHelperObj;
                ucWakeupProfile.Param_Wakeup_Profile_object = _param_WakeUp_Profiles_Obj;
                ucWakeupProfile.Param_WakeUp_ProfileHelperObj = Param_WakeUp_ProfileHelperObj;

                ucWakeupProfile.Param_Communication_Profile_object = Param_Communication_Profile_object;
                ucWakeupProfile.Param_Number_ProfileHelperObj = Param_Number_ProfileHelperObj;
                ucWakeupProfile.Param_Keep_Alive_IP_object = Param_Keep_Alive_IP_object;
            }

            ///Initialize ucNumberProfiles
            if (ucNumberProfile != null)
            {
                ucNumberProfile.Param_Number_Profile_object = _param_Number_Profiles_Obj;
                ucNumberProfile.Param_Number_ProfileHelperObj = Param_Number_ProfileHelperObj;
                ucNumberProfile.Param_Communication_Profile_object = Param_Communication_Profile_object;
                ucNumberProfile.Param_WakeUp_ProfileHelperObj = Param_WakeUp_ProfileHelperObj;
            }

            ///Initialize ucCommunicationProfiles
            if (ucCommProfile != null)
            {
                ucCommProfile.Param_Communication_Profile_object = Param_Communication_Profile_object;
                ucCommProfile.Param_Number_ProfileHelperObj = Param_Number_ProfileHelperObj;
                ucCommProfile.Param_WakeUp_ProfileHelperObj = Param_WakeUp_ProfileHelperObj;
            }

            ///Initialize ucKeepAliveProfiles
            if (ucKeepAlive != null)
            {
                ucKeepAlive.Param_Keep_Alive_IP_object = Param_Keep_Alive_IP_object;
                ucKeepAlive.Param_WakeUp_ProfileHelperObj = Param_WakeUp_ProfileHelperObj;
            }
            ///Initialize ucModemLimitsAndTime
            if (ucModemLimitsAndTime != null)
            {
                ucModemLimitsAndTime.Param_ModemLimitsAndTime_Object = Param_ModemLimitsAndTime_Object;
            }
            ///Initialize ucModemInitialize
            if (ucModemInitialize != null)
            {
                ucModemInitialize.Param_Modem_Initialize_Object = Param_Modem_Initialize_Object;
                ucModemInitialize.Param_ModemBasics_NEW_object = Param_ModemBasics_NEW_object;
            }


        }

        public void InitializeUcLimitsParameters()
        {
            ///Assign ucLimits variable
            if (ucLimits != null)
            {
                ucLimits.Param_Limits_object = Param_Limits_Object;
                #region Commented_Code_Section
                //ucLimits.Param_Limit_Demand_OverLoad_T1 = Param_Limit_Demand_OverLoad_T1;
                //ucLimits.Param_Limit_Demand_OverLoad_T2 = Param_Limit_Demand_OverLoad_T2;
                //ucLimits.Param_Limit_Demand_OverLoad_T3 = Param_Limit_Demand_OverLoad_T3;
                //ucLimits.Param_Limit_Demand_OverLoad_T4 = Param_Limit_Demand_OverLoad_T4; 
                #endregion
                if (Application_Controller != null &&
                    !String.IsNullOrWhiteSpace(Application_Controller.CurrentMeterName))
                    if (Limits == null || !Limits.CurrentMeter.Equals(Application_Controller.CurrentMeterName))
                        Limits = new LimitValues(Application_Controller.CurrentMeterName);
                ucLimits.Parameterization_Limits = Limits;
            }
        }

        public void InitializeUcSinglePhaseParameters()
        {
            LimitValues limitValues = null;
            ///Assign ucLimits variable
            if (ucParamSinglePhase != null)
            {
                ucParamSinglePhase.Param_Limits_object = Param_Limits_Object;
                limitValues = ucParamSinglePhase.Limits;

                if (Application_Controller != null &&
                   !String.IsNullOrWhiteSpace(Application_Controller.CurrentMeterName))
                    if (limitValues == null ||
                        !limitValues.CurrentMeter.Equals(Single_Phase_Model))
                        limitValues = new LimitValues(Single_Phase_Model);

                ucParamSinglePhase.Limits = limitValues;
                ucParamSinglePhase.Param_Monitoring_time_object = Param_Monitoring_Time_Object;
                ucParamSinglePhase.Param_MDI_parameters_object = Param_MDI_Parameters_Object;
                ucParamSinglePhase.Param_ErrorDetails = Param_Error_Details;
            }
        }

        public void InitializeUcDisplayWindowsParameters()
        {
            if (application_Controller != null)
            {
                if (ucDisplayWindows1 != null)
                    ucDisplayWindows1.Obj_ApplicationController = Application_Controller;

            }
            if (ucDisplayWindows1 != null)
                ucDisplayWindows1.Param_Controller = Param_Controller;

            #region ///Init Loal Variables

            if (Param_DisplayWindowsNormal == null)
                Param_DisplayWindowsNormal = new DisplayWindows();
            if (Param_DisplayWindowsAlternate == null)
                Param_DisplayWindowsAlternate = new DisplayWindows();
            if (Param_DisplayWindowsTest == null)
                Param_DisplayWindowsTest = new DisplayWindows();

            Param_DisplayWindowsNormal.WindowsMode = DispalyWindowsModes.Normal;
            Param_DisplayWindowsAlternate.WindowsMode = DispalyWindowsModes.Alternate;
            Param_DisplayWindowsTest.WindowsMode = DispalyWindowsModes.Test; //v4.8.23

            ///Assign Windows Numbers
            for (int index = 0; index < Param_DisplayWindowsNormal.Windows.Count; index++)
            {
                Param_DisplayWindowsNormal.Windows[index].WindowNumberToDisplay = (ushort)(index + 1);
            }

            for (int index = 0; index < Param_DisplayWindowsAlternate.Windows.Count; index++)
            {
                Param_DisplayWindowsAlternate.Windows[index].WindowNumberToDisplay = (ushort)(index + 1);
            }

            Param_DisplayWindowsNormal.ScrollTime = new TimeSpan(0, 0, 05);
            Param_DisplayWindowsAlternate.ScrollTime = new TimeSpan(0, 0, 05);

            #endregion
        }

        public void InitializeUcTimeWindowParameters()
        {
            if (ucTimeWindowParam1 != null)
            {
                ucTimeWindowParam1.Param_TimeBaseEvent = TBE1;
            }
            if (ucTimeWindowParam2 != null)
            {
                ucTimeWindowParam2.Param_TimeBaseEvent = TBE2;
            }
        }

        private void Init_LoadProfiles(ConnectionInfo connInfo = null)
        {
            try
            {
                this.AllSelectableChannels = LoadProfile_Controller.Get_SelectableLoadProfileChannels(connInfo);
                ucLoadProfile.Init_LoadProfiles(AllSelectableChannels);
                this.LoadProfileChannelsInfo = new List<LoadProfileChannelInfo>();
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region ucActivity_Calendar_EventHandlers

        private void btn_setActivityCalendar_Click(object sender, EventArgs e)
        {
            try
            {
                Application_Controller.IsIOBusy = true;
                //set Activity Calendar function
                Data_Access_Result res = this.Param_Controller.SET_ActivityCalendar(Calendar);
                if (res == Data_Access_Result.Success)
                {
                    Notification notifier = new Notification("Process Completed", "Tariffication Parameters written successfuly");
                    //MessageBox.Show("Tariffication Parameters written successfuly", "Tarification Successful");

                }
                else
                {
                    Notification notifier = new Notification("Error ", String.Format("Tariffication Parameters not written completly,\r\nError:{0}", res));
                    //MessageBox.Show(String.Format("Tariffication Parameters not written completly,Error:{0}", res), "Error Setting Tariff");

                }
            }
            catch (Exception ex)
            {

                ///MessageBox.Show("Error,Details\r\n" + ex.Message, "Error Setting Activity Calendar");
                Notification notifier = new Notification("Error ", String.Format("Tariffication Parameters not written \r\n completly,\r\nError:{0}", ex.Message));
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btn_GetActivityCalendar_Click(object sender, EventArgs e)
        {
            try
            {
                Application_Controller.IsIOBusy = true;
                GET_ActivityCalendar();
                ///modified
                ucActivityCalendar.Calendar = Calendar;
                ucActivityCalendar.showTariffication();
                ///showTariffication();
                ///MessageBox.Show("Activity Calendar Read Complete");
                Notification notifier = new Notification("Success", "Tariffication Parameters read successfuly");
                ///Profiles_SelectedIndexChanged(this, new EventArgs());
            }
            catch (Exception ex)
            {
                ///MessageBox.Show("Error,Details\r\n" + ex.Message, "Error Getting Activity Calendar");
                Notification notifier = new Notification("Error", String.Format("Error Reading Tariffication Parameters,Details{0}", ex.Message));
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btn_setActivityCalendar_Name_Click(object sender, EventArgs e)
        {
            try
            {
                Application_Controller.IsIOBusy = true;
                //set Activity Calendar function
                Data_Access_Result res = this.Param_Controller.SET_ActivityCalendarName(Calendar);
                if (res == Data_Access_Result.Success)
                {
                    Notification notifier = new Notification("Process Completed", "Tariffication Name Parameters written successfuly");
                    //MessageBox.Show("Tariffication Parameters written successfuly", "Tarification Successful");

                }
                else
                {
                    Notification notifier = new Notification("Error ", String.Format("Tariffication Name Parameters not written completly,\r\nError:{0}", res));
                    //MessageBox.Show(String.Format("Tariffication Parameters not written completly,Error:{0}", res), "Error Setting Tariff");

                }
            }
            catch (Exception ex)
            {

                ///MessageBox.Show("Error,Details\r\n" + ex.Message, "Error Setting Activity Calendar");
                Notification notifier = new Notification("Error ", String.Format("Tariffication Name Parameters not written \r\n completly,\r\nError:{0}", ex.Message));
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btn_GetActivityCalendar_Name_Click(object sender, EventArgs e)
        {
            try
            {
                Application_Controller.IsIOBusy = true;
                Param_ActivityCalendar Calendar_Receive = new Param_ActivityCalendar();
                this.Param_Controller.GET_ActivityCalendar_Name(ref Calendar_Receive);
                this.Param_Controller.GET_ActivityCalendar_NamePassive(ref Calendar_Receive);
                this.Param_Controller.GET_ActivityCalendar_ActivationDateTime(ref Calendar_Receive);
                Calendar = Calendar_Receive;
                ///modified
                ucActivityCalendar.Calendar = Calendar;
                ucActivityCalendar.DisplayNameAndDate();
                ///showTariffication();
                ///MessageBox.Show("Activity Calendar Read Complete");
                Notification notifier = new Notification("Success", "Tariffication Name,Date Parameters read successfuly");
                ///Profiles_SelectedIndexChanged(this, new EventArgs());
            }
            catch (Exception ex)
            {
                ///MessageBox.Show("Error,Details\r\n" + ex.Message, "Error Getting Activity Calendar");
                Notification notifier = new Notification("Error", String.Format("Error Reading Tariffication Name,Date Parameters,Details{0}", ex.Message));
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btn_setActivityCalendar_DayProfile_Click(object sender, EventArgs e)
        {
            try
            {
                Application_Controller.IsIOBusy = true;
                //set Activity Calendar function
                Data_Access_Result res = this.Param_Controller.SET_Param_DayProfile(Calendar.ParamDayProfile);
                if (res == Data_Access_Result.Success)
                {
                    Notification notifier = new Notification("Process Completed", "Activity Calendar Day Profile written successfuly");
                    //MessageBox.Show("Tariffication Parameters written successfuly", "Tarification Successful");

                }
                else
                {
                    Notification notifier = new Notification("Error ", String.Format("Activity Calendar Day Profile not written completly,\r\nError:{0}", res));
                    //MessageBox.Show(String.Format("Tariffication Parameters not written completly,Error:{0}", res), "Error Setting Tariff");

                }
            }
            catch (Exception ex)
            {

                ///MessageBox.Show("Error,Details\r\n" + ex.Message, "Error Setting Activity Calendar");
                Notification notifier = new Notification("Error ", String.Format("Activity Calendar Day Profile Parameters not written \r\n completly,\r\nError:{0}", ex.Message));
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btn_GetActivityCalendar_DayProfile_Click(object sender, EventArgs e)
        {
            try
            {
                Application_Controller.IsIOBusy = true;
                Param_ActivityCalendar Calendar_Receive = new Param_ActivityCalendar();
                this.Param_Controller.GET_ActivityCalendar_DayProfile(ref Calendar_Receive);
                Calendar = Calendar_Receive;
                ///modified
                ucActivityCalendar.Calendar = Calendar;
                ucActivityCalendar.DisplayDayProfile();
                ///showTariffication();
                ///MessageBox.Show("Activity Calendar Read Complete");
                Notification notifier = new Notification("Success", "Activity Calendar Day Profile Parameters read successfuly");
                ///Profiles_SelectedIndexChanged(this, new EventArgs());
            }
            catch (Exception ex)
            {
                ///MessageBox.Show("Error,Details\r\n" + ex.Message, "Error Getting Activity Calendar");
                Notification notifier = new Notification("Error", String.Format("Error Reading Activity Calendar Day Profile Parameters,Details{0}", ex.Message));
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btn_setActivityCalendar_SeasonProfile_Click(object sender, EventArgs e)
        {
            try
            {
                Application_Controller.IsIOBusy = true;
                //set Activity Calendar function
                Data_Access_Result res = this.Param_Controller.SET_Param_SeasonProfile(Calendar.ParamSeasonProfile);
                if (res == Data_Access_Result.Success)
                {
                    Notification notifier = new Notification("Process Completed", "Activity Calendar Season Profile written successfuly");
                    //MessageBox.Show("Tariffication Parameters written successfuly", "Tarification Successful");

                }
                else
                {
                    Notification notifier = new Notification("Error ", String.Format("Activity Calendar Season Profile not written completly,\r\nError:{0}", res));
                    //MessageBox.Show(String.Format("Tariffication Parameters not written completly,Error:{0}", res), "Error Setting Tariff");

                }
            }
            catch (Exception ex)
            {

                ///MessageBox.Show("Error,Details\r\n" + ex.Message, "Error Setting Activity Calendar");
                Notification notifier = new Notification("Error ", String.Format("Activity Calendar Season Profile Parameters not written \r\n completly,\r\nError:{0}", ex.Message));
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btn_GetActivityCalendar_SeasonProfile_Click(object sender, EventArgs e)
        {
            try
            {
                Application_Controller.IsIOBusy = true;
                Param_ActivityCalendar Calendar_Receive = new Param_ActivityCalendar();
                this.Param_Controller.GET_ActivityCalendar_SeasonProfile(ref Calendar_Receive);
                Calendar = Calendar_Receive;
                ///modified
                ucActivityCalendar.Calendar = Calendar;
                ucActivityCalendar.RefreshGUI_SeasonProfile(null);
                ///showTariffication();
                ///MessageBox.Show("Activity Calendar Read Complete");
                Notification notifier = new Notification("Success", "Activity Calendar Season Profile Parameters read successfuly");
                ///Profiles_SelectedIndexChanged(this, new EventArgs());
            }
            catch (Exception ex)
            {
                ///MessageBox.Show("Error,Details\r\n" + ex.Message, "Error Getting Activity Calendar");
                Notification notifier = new Notification("Error", String.Format("Error Reading Activity Calendar Season Profile Parameters,Details{0}", ex.Message));
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btn_setActivityCalendar_WeekProfile_Click(object sender, EventArgs e)
        {
            try
            {
                Application_Controller.IsIOBusy = true;
                //set Activity Calendar function
                Data_Access_Result res = this.Param_Controller.SET_Param_WeekProfile(Calendar.ParamWeekProfile);
                if (res == Data_Access_Result.Success)
                {
                    Notification notifier = new Notification("Process Completed", "Activity Calendar Week Profile written successfuly");
                    //MessageBox.Show("Tariffication Parameters written successfuly", "Tarification Successful");

                }
                else
                {
                    Notification notifier = new Notification("Error ", String.Format("Activity Calendar Week Profile not written completly,\r\nError:{0}", res));
                    //MessageBox.Show(String.Format("Tariffication Parameters not written completly,Error:{0}", res), "Error Setting Tariff");

                }
            }
            catch (Exception ex)
            {

                ///MessageBox.Show("Error,Details\r\n" + ex.Message, "Error Setting Activity Calendar");
                Notification notifier = new Notification("Error ", String.Format("Activity Calendar Week Profile Parameters not written \r\n completly,\r\nError:{0}", ex.Message));
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btn_GetActivityCalendar_WeekProfile_Click(object sender, EventArgs e)
        {
            try
            {
                Application_Controller.IsIOBusy = true;
                Param_ActivityCalendar Calendar_Receive = new Param_ActivityCalendar();
                this.Param_Controller.GET_ActivityCalendar_DayProfile(ref Calendar_Receive);
                this.Param_Controller.GET_ActivityCalendar_WeekProfile(ref Calendar_Receive);
                Calendar = Calendar_Receive;
                ///modified
                ucActivityCalendar.Calendar = Calendar;
                ucActivityCalendar.DisplayWeekProfile();
                ///showTariffication();
                ///MessageBox.Show("Activity Calendar Read Complete");
                Notification notifier = new Notification("Success", "Activity Calendar Week Profile Parameters read successfuly");
                ///Profiles_SelectedIndexChanged(this, new EventArgs());
            }
            catch (Exception ex)
            {
                ///MessageBox.Show("Error,Details\r\n" + ex.Message, "Error Getting Activity Calendar");
                Notification notifier = new Notification("Error", String.Format("Error Reading Activity Calendar Week Profile Parameters\nDetails{0}", ex.Message));
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btn_setActivityCalendar_SpecialDay_Click(object sender, EventArgs e)
        {
            try
            {
                Application_Controller.IsIOBusy = true;
                //set Activity Calendar function
                Data_Access_Result res = this.Param_Controller.SET_Param_SpecialDayProfile(Calendar.ParamSpecialDay);
                if (res == Data_Access_Result.Success)
                {
                    Notification notifier = new Notification("Process Completed", "Activity Calendar SpecialDay Profile written successfuly");
                    //MessageBox.Show("Tariffication Parameters written successfuly", "Tarification Successful");

                }
                else
                {
                    Notification notifier = new Notification("Error ", String.Format("Activity Calendar SpecialDay Profile not written completly,\r\nError:{0}", res));
                    //MessageBox.Show(String.Format("Tariffication Parameters not written completly,Error:{0}", res), "Error Setting Tariff");

                }
            }
            catch (Exception ex)
            {

                ///MessageBox.Show("Error,Details\r\n" + ex.Message, "Error Setting Activity Calendar");
                Notification notifier = new Notification("Error ", String.Format("Activity Calendar SpecialDay Profile Parameters not written \r\n completly,\r\nError:{0}", ex.Message));
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btn_GetActivityCalendar_SpecialDay_Click(object sender, EventArgs e)
        {
            try
            {
                Application_Controller.IsIOBusy = true;
                Param_ActivityCalendar Calendar_Receive = new Param_ActivityCalendar();
                this.Param_Controller.GET_ActivityCalendar_SpecialDay(ref Calendar_Receive);
                Calendar = Calendar_Receive;
                ///modified
                ucActivityCalendar.Calendar = Calendar;
                ucActivityCalendar.DisplaySpecialDays();
                ///showTariffication();
                ///MessageBox.Show("Activity Calendar Read Complete");
                Notification notifier = new Notification("Success", "Activity Calendar SpecialDay Profile Parameters read successfuly");
                ///Profiles_SelectedIndexChanged(this, new EventArgs());
            }
            catch (Exception ex)
            {
                ///MessageBox.Show("Error,Details\r\n" + ex.Message, "Error Getting Activity Calendar");
                Notification notifier = new Notification("Error", String.Format("Error Reading Activity Calendar SpecialDay Profile Parameters,Details{0}", ex.Message));
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btn_WriteToDatabaseTariffication_Click(object sender, EventArgs e)
        {
            ///dbController.saveActivityCalendar(Application_Controller.ConnectionManager.ConnectionInfo.MSN, Calendar);
        }

        #endregion

        #region Time_Based_Scheduling

        private void ShowtoGUI_TimeBaseEvents()
        {
            if (obj_TBE_PowerFail != null)
            {
                check_TBE1_PowerFail.Checked = Convert.ToBoolean(obj_TBE_PowerFail.disableEventAtPowerFail_TBE1);
                check_TBE2_PowerFail.Checked = Convert.ToBoolean(obj_TBE_PowerFail.disableEventAtPowerFail_TBE2);
            }
            ///dbController.saveTBEs(tbe_obj, obj_TBE_PowerFail);
            if (ucTimeWindowParam1 != null)
                ucTimeWindowParam1.ShowtoGUI_TBE();
            if (ucTimeWindowParam2 != null)
                ucTimeWindowParam2.ShowtoGUI_TBE();
        }

        private void check_TBE1_PowerFail_CheckedChanged(object sender, EventArgs e)
        {
            obj_TBE_PowerFail.disableEventAtPowerFail_TBE1 = Convert.ToByte(check_TBE1_PowerFail.Checked);
        }

        private void check_TBE2_PowerFail_CheckedChanged(object sender, EventArgs e)
        {
            obj_TBE_PowerFail.disableEventAtPowerFail_TBE2 = Convert.ToByte(check_TBE2_PowerFail.Checked);
        }

        #endregion

        #region Event Handlers

        private void check_AddTime_CheckedChanged(object sender, EventArgs e)
        {
            ///***modified
            //Application.DoEvents();
            //if (Application_Process.Is_Association_Developed &&
            //     Application_Process.CurrentClientSAP._SAP_Address == 1 &&
            //     Application_Process.CurrentMeterSAP._SAP_Address == 1 && check_AddTime.Checked)
            //{
            //    if (combo_ClockInterval.SelectedIndex == -1) return;

            //    interval = Convert.ToInt16(combo_ClockInterval.SelectedItem);
            //    TimeToAdd = Convert.ToInt16(combo_ClkTimeToAdd.SelectedItem);
            //    this.JumpTime = new TimeSpan(0, TimeToAdd, 0);
            //    Param_Controller.GET_Meter_Clock(ref meterClock);
            //    timer_ClockFWD.Enabled = false;
            //    timer_ClockFWD.Interval = (interval > 0 && interval <= 60) ? 1000 * interval : 10000;
            //    timer_ClockFWD.Enabled = true;
            //    Application.DoEvents();
            //}
            //else if (!check_AddTime.Checked)
            //{ 
            //    timer_ClockFWD.Enabled = false;
            //}
        }

        private void btnStartTimer_Click(object sender, EventArgs e)
        {
            Notification N;
            if (application_Controller.ConnectionController.IsConnected)
            {
                try
                {
                    progressBar1.Visible = true;
                    if (check_AddTime.Checked)
                    {
                        gb_Debug_Read_LOG.Enabled = false;
                        if (Application_Process.Is_Association_Developed &&
                          check_AddTime.Checked)
                        {
                            if (combo_ClockInterval.SelectedIndex == -1) return;

                            interval = Convert.ToInt16(combo_ClockInterval.SelectedItem);
                            TimeToAdd = Convert.ToInt16(combo_ClkTimeToAdd.SelectedItem);
                            this.JumpTime = new TimeSpan(0, TimeToAdd, 0);

                            var _Param_MeterClock = Param_MeterClock;
                            Param_Controller.GET_MeterClock(ref _Param_MeterClock);

                            timer_ClockFWD.Enabled = false;
                            timer_ClockFWD.Interval = (interval > 0 && interval <= 60) ? 1000 * interval : 10000;
                            timer_ClockFWD.Enabled = true;
                        }
                        else if (!check_AddTime.Checked)
                        {
                            timer_ClockFWD.Enabled = false;
                        }
                    }
                }
                finally
                {
                    if (timer_ClockFWD.Enabled)
                        Application_Controller.IsIOBusy = true;
                    else
                        Application_Controller.IsIOBusy = false;
                }
            }
            else N = new Notification("Not Connected", "Please Connect to meter");

        }

        private void timer_ClockFWD_Tick(object sender, EventArgs e)
        {
            try
            {
                timer_ClockFWD.Stop();
                BW_Testing.RunWorkerAsync();
            }
            catch
            { }
        }

        private void Update_TestGrid()
        {
            Thread.Sleep(1000);
            Grid_Debug.Rows.Add();

            Instantaneous_Class Instantaneous_Class_obj = new Instantaneous_Class();
            //Verify Either Active Season Object Exists in meter
            Base_Class obj = Application_Controller.Applicationprocess_Controller.GetSAPEntry(Get_Index.Active_Season);
            if (obj.IsAttribReadable(0x02))
            {
                Application_Controller.InstantaneousController.Get_Active_Season(Instantaneous_Class_obj);
            }

            Grid_Debug.Rows[currentRow].HeaderCell.Value = (currentRow + 1).ToString();
            Grid_Debug[0, currentRow].Value = Param_Clock_Caliberation_Object.Set_Time.Date.ToString("dd/MM/yyyy");
            Grid_Debug[1, currentRow].Value = (Param_Clock_Caliberation_Object.Set_Time.TimeOfDay.Hours) + ":"
                                            + (Param_Clock_Caliberation_Object.Set_Time.TimeOfDay.Minutes)
                                            + ":" + (Param_Clock_Caliberation_Object.Set_Time.TimeOfDay.Seconds);

            Grid_Debug[2, currentRow].Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            Grid_Debug[3, currentRow].Value = Instantaneous_Class_obj.Active_Season;
            Grid_Debug[4, currentRow].Value = Instantaneous_Class_obj.Active_Tariff;
            Grid_Debug[5, currentRow].Value = Instantaneous_Class_obj.Active_Day_Profile;
            currentRow++;
            Grid_Debug.FirstDisplayedScrollingRowIndex = Grid_Debug.Rows.Count - 1;
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            try
            {
                if (timer_Debug_Read_Log.Enabled)
                {
                    timer_Debug_Read_Log.Enabled = false;
                    btn_Debug.Text = "START READ LOG";
                    txt_Debug_Log_rows_Count.Enabled = true;
                    btnStartTimer.Enabled = true;
                    Application_Controller.IsIOBusy = false;
                }
                else
                {
                    btn_Debug.Text = "STOP READ LOG";

                    txt_Debug_Log_rows_Count.Enabled = false;
                    if (Debug_Counter_Read_Log >= Convert.ToInt32(txt_Debug_Log_rows_Count.Text))
                    {
                        Debug_Counter_Read_Log = 0;
                    }
                    btnStartTimer.Enabled = false;
                    timer_Debug_Read_Log.Enabled = true;
                    Application_Controller.IsIOBusy = false;
                }
                /*
                if (Application_Process.Is_Association_Developed &&
                             Application_Process.CurrentClientSAP._SAP_Address == 1 &&
                             Application_Process.CurrentMeterSAP._SAP_Address == 1) //if true; Write time to meter
                {
                    Param_Controller.GET_Meter_Clock(ref Param_clock_caliberation_object);
                    showToGUI_Clock();
                    Application.DoEvents();
                    Update_TestGrid();
                }
                else MessageBox.Show("Connect to Management via Management");
                */
            }
            finally
            {
                if (timer_Debug_Read_Log.Enabled)
                    Application_Controller.IsIOBusy = false;
                else
                    Application_Controller.IsIOBusy = true;
            }
        }

        private void hsb_Debug_RefreshRate_Scroll(object sender, ScrollEventArgs e)
        {
            bool bEnable = timer_Debug_Read_Log.Enabled;
            timer_Debug_Read_Log.Enabled = false;
            timer_Debug_Read_Log.Interval = hsb_Debug_RefreshRate.Value;
            timer_Debug_Read_Log.Enabled = bEnable;
        }

        private void timer_Debug_Read_Log_Tick(object sender, EventArgs e)
        {
            try
            {
                timer_Debug_Read_Log.Stop();
                if (Application_Process.Is_Association_Developed) //if true; Write time to meter
                {
                    var _Param_Clock_Caliberation_Object = Param_Clock_Caliberation_Object;
                    Param_Controller.GET_MeterClock(ref _Param_Clock_Caliberation_Object);
                    ///***modified
                    ///showToGUI_Clock();
                    Application.DoEvents();
                    Update_TestGrid();
                    if (++Debug_Counter_Read_Log >= Convert.ToInt32(txt_Debug_Log_rows_Count.Text))
                    {
                        timer_Debug_Read_Log.Enabled = false;
                        btn_Debug.Text = "START READ LOG";
                        txt_Debug_Log_rows_Count.Enabled = true;
                        Debug_Counter_Read_Log = 0;
                        btnStartTimer.Enabled = true;
                        ///Finish Read LOG
                        Application_Controller.IsIOBusy = false;
                    }
                    else
                    {
                        timer_Debug_Read_Log.Enabled = true;
                        timer_Debug_Read_Log.Start();
                    }
                }
                else
                {
                    timer_Debug_Read_Log.Enabled = false;
                    Application_Controller.IsIOBusy = false;
                    /// MessageBox.Show("Connect to Management via Management");
                    Notification notifier = new Notification("Error", "Connect to Meter");
                }
            }
            catch (Exception ex)
            {
                timer_Debug_Read_Log.Enabled = false;
                Application_Controller.IsIOBusy = false;
                /// MessageBox.Show("Connect to Management via Management");
                Notification notifier = new Notification("Error", String.Format("Error reading Read LOG {0}", ex.Message));

            }
            finally
            {
                if (timer_Debug_Read_Log.Enabled)
                {
                    Application_Controller.IsIOBusy = true;

                }
                else
                {
                    Application_Controller.IsIOBusy = false;
                }
            }
        }

        private void txt_Debug_Log_rows_Count_TextChanged(object sender, EventArgs e)
        {
            int x;
            try
            {
                x = Convert.ToInt32(txt_Debug_Log_rows_Count.Text);
            }
            catch (Exception ex)
            {
                txt_Debug_Log_rows_Count.Text = "1";
            }
            Debug_Counter_Read_Log = 0;
        }

        private void btn_ClearGrid_Click(object sender, EventArgs e)
        {
            clearGrid(Grid_Debug);
        }

        public void clearGridRow(DataGridView grid1)
        {
            grid1.Visible = false;
            grid1.DataSource = null;

            int totalRows = grid1.Rows.Count;
            if (grid1.Rows.Count > 0)
            {
                for (int i = totalRows - 1; i >= 0; i--)
                {
                    grid1.Rows.RemoveAt(i);
                }
            }
        }

        public void clearGridColumn(DataGridView grid1)
        {
            grid1.Visible = false;
            grid1.DataSource = null;

            int totalCols = grid1.Columns.Count;
            if (grid1.Columns.Count > 0)
            {
                for (int i = totalCols - 1; i >= 0; i--)
                {
                    grid1.Columns.RemoveAt(i);
                }
            }
        }

        public void clearGrid(DataGridView grid)
        {
            //clearGridColumn(grid);
            clearGridRow(grid);

        }

        private void btn_StopTariffTest_Click(object sender, EventArgs e)
        {
            try
            {
                timer_ClockFWD.Enabled = false;
                gb_Debug_Read_LOG.Enabled = true;
                progressBar1.Visible = false;
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void BW_Testing_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (Application_Process.Is_Association_Developed &&
                  check_AddTime.Checked) //if true; Write time to meter
                {
                    var _Param_MeterClock = Param_MeterClock;
                    Application_Controller.IsIOBusy = true;
                    Param_Controller.GET_MeterClock(ref _Param_MeterClock);
                    int seconds = Param_MeterClock.Set_Time.Second;
                    DateTime temp = Param_MeterClock.Set_Time.AddSeconds(60 - seconds - 3);
                    Param_MeterClock.Set_Time = Param_MeterClock.Set_Time.Add(JumpTime);

                    if (check_ClkAddMonth.Checked && Param_MeterClock.Set_Time.Date != temp.Date)
                    {
                        Param_MeterClock.Set_Time = Param_MeterClock.Set_Time.AddMonths(1);
                    }

                    Param_Controller.SET_Meter_Clock(Param_MeterClock); //Write newTime to Meter

                    Param_Clock_Caliberation_Object = Param_MeterClock;
                    // Update_TestGrid();

                }
                else
                {
                    this.timer_ClockFWD.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                this.timer_ClockFWD.Enabled = false;
                ///MessageBox.Show(this.Parent, "Unable to update RTC" + ex.Message);
                Notification Notifier = new Notification("Error Update RTC", ex.Message);
                ///throw;
            }
        }

        private void BW_Testing_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                Update_TestGrid();

                timer_ClockFWD.Start();
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        #endregion
        public void saveTerrificationToDB()
        {
            string MSNtoPass = Param_Controller.CurrentConnectionInfo.MSN;
            if (!myDB.save_TerrificationToDatabase(Calendar, MSNtoPass))
            {
                ///MessageBox.Show("Error saving to Database");
                Notification Notifier = new Notification("Error ", "Error Saving to Database");
            }
            else
            {
                ///MessageBox.Show("Tariffication Added to Database!!!!");
                //Notification Notifier = new Notification("Process Completed", "Tariffication Added to Database", 3000); //v4.8.30
            }
        }

        public void UpdateReadWriteStatus(bool IsReadWriteBusy)
        {
            #region Functions_Body

            try
            {
                ///Disable IO Read Write btns 
                if (IsReadWriteBusy)
                {
                    btn_Caliberation_Save.Enabled = false;
                    btn_caliberation_loadall.Enabled = false;
                    btn_GET_paramameters.Enabled = false;
                    btn_SET_paramameters.Enabled = false;
                    btn_Debug.Enabled = false;

                    btnStartTimer.Enabled = false;
                    btn_StopTariffTest.Enabled = false;

                    ///ucDateTime Clock
                    if (ucDateTime != null)
                    {
                        ucDateTime.btn_GetTime.Enabled = false;
                        ucDateTime.btn_SETtime.Enabled = false;
                    }
                    ///UcParameterSinglePhase
                    if (ucParamSinglePhase != null)
                    {
                        ucParamSinglePhase.SP_btn_Get_MTPowerFail.Enabled = false;
                        ucParamSinglePhase.SP_btn_Get_MTReverseEnergy.Enabled = false;
                        ucParamSinglePhase.SP_btn_Get_MTEarth.Enabled = false;
                        ucParamSinglePhase.SP_btn_Set_MTPowerFail.Enabled = false;
                        ucParamSinglePhase.SP_btn_Set_MTReverseEnergy.Enabled = false;
                        ucParamSinglePhase.SP_btn_Set_MTEarth.Enabled = false;
                        ucParamSinglePhase.btn_SP_GetOverLoadTotal.Enabled = false;
                        ucParamSinglePhase.btn_SP_SetOverLoadTotal.Enabled = false;
                        ucParamSinglePhase.SP_btn_Set_MDI_AutoResetDate.Enabled = false;
                        ucParamSinglePhase.SP_btn_Get_MDI_AutoResetDate.Enabled = false;
                        ucParamSinglePhase.SP_btn_getEventString.Enabled = false;
                    }
                }
                ///Enable Read Write btns
                else
                {
                    btn_Caliberation_Save.Enabled = true;
                    btn_caliberation_loadall.Enabled = true;
                    btn_GET_paramameters.Enabled = true;
                    btn_SET_paramameters.Enabled = true;
                    btn_Debug.Enabled = true;

                    btnStartTimer.Enabled = true;
                    btn_StopTariffTest.Enabled = true;
                    ///modified
                    //btn_GetTime.Enabled = true;
                    //btn_SETtime.Enabled = true;

                    ///ucDateTime Clock
                    if (ucDateTime != null)
                    {
                        ucDateTime.btn_GetTime.Enabled = true;
                        ucDateTime.btn_SETtime.Enabled = true;
                    }
                    ///UcParameterSinglePhase
                    if (ucParamSinglePhase != null)
                    {
                        ucParamSinglePhase.SP_btn_Get_MTPowerFail.Enabled = true;
                        ucParamSinglePhase.SP_btn_Get_MTReverseEnergy.Enabled = true;
                        ucParamSinglePhase.SP_btn_Get_MTEarth.Enabled = true;
                        ucParamSinglePhase.SP_btn_Set_MTPowerFail.Enabled = true;
                        ucParamSinglePhase.SP_btn_Set_MTReverseEnergy.Enabled = true;
                        ucParamSinglePhase.SP_btn_Set_MTEarth.Enabled = true;
                        ucParamSinglePhase.btn_SP_GetOverLoadTotal.Enabled = true;
                        ucParamSinglePhase.btn_SP_SetOverLoadTotal.Enabled = true;
                        ucParamSinglePhase.SP_btn_Set_MDI_AutoResetDate.Enabled = true;
                        ucParamSinglePhase.SP_btn_Get_MDI_AutoResetDate.Enabled = true;
                        ucParamSinglePhase.SP_btn_getEventString.Enabled = true;
                    }
                }
            }
            catch (Exception)
            {
                btn_Caliberation_Save.Enabled = true;
                btn_caliberation_loadall.Enabled = true;
                btn_GET_paramameters.Enabled = true;
                btn_SET_paramameters.Enabled = true;
                btn_Debug.Enabled = true;

                btnStartTimer.Enabled = true;
                btn_StopTariffTest.Enabled = true;
                ///modified
                //btn_GetTime.Enabled = true;
                //btn_SETtime.Enabled = true;
            }

            #endregion
        }

        internal void Reset_State()
        {
            try
            {
                String fileUrl = Environment.CurrentDirectory + "\\DLMS_saved_files\\";
                DirectoryInfo dictoryInfo = new DirectoryInfo(fileUrl);
                if (dictoryInfo.Exists)
                    Load_and_show_all(fileUrl);
            }
            catch { }
        }

        #region Background_Worker_Single_Phase

        private void bgw_MDIAUto_DoWork(object sender, DoWorkEventArgs e)
        {
            var _Param_MDI_Parameters_Object = Param_MDI_Parameters_Object;
            Param_Controller.GET_MDI_Parameters_SP(ref _Param_MDI_Parameters_Object);
            #region OLDCODE

            //if (Param_MDI_parameters_object.Auto_reset_date.DayOfMonth == 0xfe)
            //{
            //    SP_combo_MDI_AutoResetDate.SelectedItem = "Last Day";
            //}
            //else if (Param_MDI_parameters_object.Auto_reset_date.DayOfMonth == 0xfd)
            //{
            //    SP_combo_MDI_AutoResetDate.SelectedItem = "Second Last Day";
            //}
            //else
            //{
            //    if (Param_MDI_parameters_object.Auto_reset_date.DayOfMonth - 1 > -1)
            //    {
            //        SP_combo_MDI_AutoResetDate.SelectedIndex = Param_MDI_parameters_object.Auto_reset_date.DayOfMonth - 1;
            //    }
            //
            //} 

            #endregion
        }

        private void bgw_MDIAUto_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //dtc_mdi_SinglePhase.Year = Param_MDI_parameters_object.Auto_reset_date.Year;
            //dtc_mdi_SinglePhase.Month = Param_MDI_parameters_object.Auto_reset_date.Month;
            //dtc_mdi_SinglePhase.Date = Param_MDI_parameters_object.Auto_reset_date.DayOfMonth;
            //dtc_mdi_SinglePhase.DayOfWeek = Param_MDI_parameters_object.Auto_reset_date.DayOfWeek;
            //dtc_mdi_SinglePhase.Hours = Param_MDI_parameters_object.Auto_reset_date.Hour;
            //dtc_mdi_SinglePhase.Minutes = Param_MDI_parameters_object.Auto_reset_date.Minute;
            //dtc_mdi_SinglePhase.Seconds = Param_MDI_parameters_object.Auto_reset_date.Second;
            //dtc_mdi_SinglePhase.showDatetime();

            ///***modified
            Application_Controller.IsIOBusy = false;
            ucParamSinglePhase.progressBar2.Visible = false;
            ucParamSinglePhase.showToGUI_MDIParms();
            Application.DoEvents();
        }

        private void bhw_GetMT_DoWork(object sender, DoWorkEventArgs e)
        {
            var _Param_Monitoring_Time_Object = Param_Monitoring_Time_Object;
            Param_Controller.GET_MT_Power_Fail(ref _Param_Monitoring_Time_Object);
        }

        private void bhw_GetMT_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ///***modified
            ///SP_DT_MT_PowerFail.Text = Convert.ToString(Param_Monitoring_time_object.Power_Fail);
            //SP_btn_Get_MTPowerFail.Enabled = true;

            Application_Controller.IsIOBusy = false;
            ucParamSinglePhase.progressBar2.Visible = false;
            ucParamSinglePhase.showToGUI_MonitoringTime();
        }

        private void bgw_SetMDIAuto_DoWork(object sender, DoWorkEventArgs e)
        {
            Param_Controller.SET_MDI_Parameters_SP(Param_MDI_Parameters_Object);
        }

        private void bgw_SetMDIAuto_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Application_Controller.IsIOBusy = false;
            ucParamSinglePhase.progressBar2.Visible = false;
        }

        //private void bgw_time_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    showToGUI_ClockOnly();
        //    txt_Clock_set_time_debug.Value = Param_clock_caliberation_object.Set_Time;
        //    Application_Controller.IsIOBusy = false;
        //}
        //private void bgw_SetTime_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    Application_Controller.IsIOBusy = true;
        //    Param_clock_caliberation_object.Set_Time = txt_Clock_set_time_debug.Value;
        //    Param_Controller.SET_MeterClock_Date_Time(Param_clock_caliberation_object);

        //} 

        void bgw_SP_setOverLoadTotal_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Application_Controller.IsIOBusy = false;
            ucParamSinglePhase.progressBar2.Visible = false;
        }

        void bgw_SP_setOverLoadTotal_DoWork(object sender, DoWorkEventArgs e)
        {
            //***modified
            //Param_Limits_object.Over_Load_Total_T1 = Convert.ToUInt16(txt_SP_overLoadTotal.Text);
            Param_Controller.SET_Limit_Over_Load_Total_T1(Param_Limits_Object);
        }

        void bgw_SP_getOverLoadTotal_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Application_Controller.IsIOBusy = false;
            ucParamSinglePhase.progressBar2.Visible = false;
            ucParamSinglePhase.showToGUI_Limits();
        }

        void bgw_SP_getOverLoadTotal_DoWork(object sender, DoWorkEventArgs e)
        {
            var _Param_Limits_Object = Param_Limits_Object;
            Param_Controller.GET_Limit_Over_Load_Total_T1(ref _Param_Limits_Object);
            ///double val = Param_Controller.GET_Any(Get_Index.Limits_Over_Load_Total_T1, 2, 3) / 1000;
            ///txt_SP_overLoadTotal.Text = val.ToString();
        }

        void bgw_setPowerFailMT_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Application_Controller.IsIOBusy = false;
            ucParamSinglePhase.progressBar2.Visible = false;
            ucParamSinglePhase.SP_btn_Set_MTPowerFail.Enabled = true;
        }

        void bgw_setPowerFailMT_DoWork(object sender, DoWorkEventArgs e)
        {
            Param_Controller.SET_MT_Power_Fail(Param_Monitoring_Time_Object);
        }

        void bgw_setEarth_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Application_Controller.IsIOBusy = false;
            ucParamSinglePhase.progressBar2.Visible = false;
        }

        void bgw_getEarth_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ///***modified
            ///progressBar2.Visible = false;
            ///SP_btn_Get_MTEarth.Enabled = true;
            Application_Controller.IsIOBusy = false;
            ucParamSinglePhase.progressBar2.Visible = false;
            ucParamSinglePhase.showToGUI_MonitoringTime();
        }

        void bgw_setReverseEnergy_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ///***modified
            ///progressBar2.Visible = false;
            ///SP_btn_Set_MTReverseEnergy.Enabled = true;
            Application_Controller.IsIOBusy = false;
            ucParamSinglePhase.progressBar2.Visible = false;
        }

        void bgw_getReverseEnergy_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ///***modified
            ///progressBar2.Visible = false;
            ///SP_btn_Get_MTReverseEnergy.Enabled = true;
            Application_Controller.IsIOBusy = false;
            ucParamSinglePhase.progressBar2.Visible = false;
            ucParamSinglePhase.showToGUI_MonitoringTime();
        }

        void bgw_setEarth_DoWork(object sender, DoWorkEventArgs e)
        {
            Param_Controller.SET_MT_earth(Param_Monitoring_Time_Object);
        }

        void bgw_getEarth_DoWork(object sender, DoWorkEventArgs e)
        {
            var _Param_Monitoring_Time_Object = Param_Monitoring_Time_Object;
            Param_Controller.GET_MT_earth(ref _Param_Monitoring_Time_Object);
            ///SP_DT_MT_Earth.Value = Param_Monitoring_time_object.earth;
        }

        void bgw_setReverseEnergy_DoWork(object sender, DoWorkEventArgs e)
        {
            Param_Controller.SET_MT_Reverse_Energy(Param_Monitoring_Time_Object);
        }

        void bgw_getReverseEnergy_DoWork(object sender, DoWorkEventArgs e)
        {
            var _Param_Monitoring_Time_Object = Param_Monitoring_Time_Object;
            Param_Controller.GET_MT_Reverse_Energy(ref _Param_Monitoring_Time_Object);
            ///SP_DT_MT_ReverseEnergy.Value = Param_Monitoring_time_object.Reverse_Energy;
        }

        #endregion

        #region Param_Single_Phase_Envet_Handlers

        private void SP_btn_Get_MTPowerFail_Click(object sender, EventArgs e)
        {
            Application_Controller.IsIOBusy = true;
            ucParamSinglePhase.progressBar2.Visible = true;
            if (sender != null && sender is ComponentFactory.Krypton.Toolkit.KryptonButton)
                ((ComponentFactory.Krypton.Toolkit.KryptonButton)sender).Enabled = false;
            bhw_GetMT.RunWorkerAsync();
        }

        private void SP_btn_Get_MDI_AutoResetDate_Click(object sender, EventArgs e)
        {
            Application_Controller.IsIOBusy = true;
            ucParamSinglePhase.progressBar2.Visible = true;
            if (sender != null && sender is ComponentFactory.Krypton.Toolkit.KryptonButton)
                ((ComponentFactory.Krypton.Toolkit.KryptonButton)sender).Enabled = false;
            bgw_GetMDIAUto.RunWorkerAsync();
        }

        private void SP_btn_Set_MDI_AutoResetDate_Click(object sender, EventArgs e)
        {
            Application_Controller.IsIOBusy = true;
            ucParamSinglePhase.progressBar2.Visible = true;
            if (sender != null && sender is ComponentFactory.Krypton.Toolkit.KryptonButton)
                ((ComponentFactory.Krypton.Toolkit.KryptonButton)sender).Enabled = false;
            bgw_SetMDIAuto.RunWorkerAsync();
        }

        private void SP_btn_Set_MTPowerFail_Click(object sender, EventArgs e)
        {
            Application_Controller.IsIOBusy = true;
            ucParamSinglePhase.progressBar2.Visible = true;
            if (sender != null && sender is ComponentFactory.Krypton.Toolkit.KryptonButton)
                ((ComponentFactory.Krypton.Toolkit.KryptonButton)sender).Enabled = false;
            bgw_setPowerFailMT.RunWorkerAsync();

        }

        private void SP_btn_Get_MTReverseEnergy_Click(object sender, EventArgs e)
        {
            Application_Controller.IsIOBusy = true;
            ucParamSinglePhase.progressBar2.Visible = true;
            if (sender != null && sender is ComponentFactory.Krypton.Toolkit.KryptonButton)
                ((ComponentFactory.Krypton.Toolkit.KryptonButton)sender).Enabled = false;
            bgw_getReverseEnergyMT.RunWorkerAsync();
        }

        private void SP_btn_Set_MTReverseEnergy_Click(object sender, EventArgs e)
        {
            Application_Controller.IsIOBusy = true;
            ucParamSinglePhase.progressBar2.Visible = true;
            if (sender != null && sender is ComponentFactory.Krypton.Toolkit.KryptonButton)
                ((ComponentFactory.Krypton.Toolkit.KryptonButton)sender).Enabled = false;
            bgw_setReverseEnergyMT.RunWorkerAsync();
        }

        private void SP_btn_Get_MTEarth_Click(object sender, EventArgs e)
        {
            Application_Controller.IsIOBusy = true;
            ucParamSinglePhase.progressBar2.Visible = true;
            if (sender != null && sender is ComponentFactory.Krypton.Toolkit.KryptonButton)
                ((ComponentFactory.Krypton.Toolkit.KryptonButton)sender).Enabled = false;
            bgw_getEarthMT.RunWorkerAsync();
        }

        private void SP_btn_Set_MTEarth_Click(object sender, EventArgs e)
        {
            Application_Controller.IsIOBusy = true;
            ucParamSinglePhase.progressBar2.Visible = true;
            if (sender != null && sender is ComponentFactory.Krypton.Toolkit.KryptonButton)
                ((ComponentFactory.Krypton.Toolkit.KryptonButton)sender).Enabled = false;
            bgw_setEarthMT.RunWorkerAsync();
        }

        private void btn_SP_SetOverLoadTotal_Click(object sender, EventArgs e)
        {
            Application_Controller.IsIOBusy = true;
            ucParamSinglePhase.progressBar2.Visible = true;
            if (sender != null && sender is ComponentFactory.Krypton.Toolkit.KryptonButton)
                ((ComponentFactory.Krypton.Toolkit.KryptonButton)sender).Enabled = false;
            bgw_SP_setOverLoadTotal.RunWorkerAsync();
        }

        private void btn_SP_GetOverLoadTotal_Click(object sender, EventArgs e)
        {
            Application_Controller.IsIOBusy = true;
            ucParamSinglePhase.progressBar2.Visible = true;
            if (sender != null && sender is ComponentFactory.Krypton.Toolkit.KryptonButton)
                ((ComponentFactory.Krypton.Toolkit.KryptonButton)sender).Enabled = false;
            bgw_SP_getOverLoadTotal.RunWorkerAsync();
        }

        private void btn_getEventString_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Application_Controller.Applicationprocess_Controller.IsConnected)
                {
                    Notification notifier = new Notification("Application Disconnected", "Please create application assocation first");
                    return;
                }
                Param_Controller.Get_EventDetails(ref Param_Error_Details);
                if (ucParamSinglePhase != null)
                    ucParamSinglePhase.ShowToGUI_ErrorFlages();
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error Reading Event Details", ex.Message, 1500);
            }
        }

        #endregion

        #region Background_Workder_ClockOnly_Handlers

        private void bgw_time_DoWork(object sender, DoWorkEventArgs e)
        {
            Application_Controller.IsIOBusy = true;
            GET_Clock();
            GET_Clock_Only();
        }

        private void bgw_time_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ucDateTime.showToGUI_Clock();
            ucDateTime.txt_Clock_set_time_debug.Value = Param_Clock_Caliberation_Object.Set_Time;
            Application_Controller.IsIOBusy = false;
        }

        private void bgw_SetTime_DoWork(object sender, DoWorkEventArgs e)
        {
            Application_Controller.IsIOBusy = true;
            if (RTC_RequestType == DateTimeRequestType.SetTime)
            {

                Param_Clock_Caliberation_Object.Set_Time = ucDateTime.txt_Clock_set_time_debug.Value;
                dataAccessResult = Param_Controller.SET_MeterClock_Date_Time(Param_Clock_Caliberation_Object);

            }
            else if (RTC_RequestType == DateTimeRequestType.AdjustToMethods)
            {
                MethodInvokeResult = Action_Result.other_reason;
                this.InvokeMeterClockMethods();
            }
            else if (RTC_RequestType == DateTimeRequestType.ClockSynchMethod)
            {
                dataAccessResult = Data_Access_Result.Rejected;
                this.SetClockSynchMethod();
            }
            else if (RTC_RequestType == DateTimeRequestType.ClockTimeShiftLimit)
            {
                dataAccessResult = Data_Access_Result.Rejected;
                this.SetClockTimeShift();
            }
            else if (RTC_RequestType == DateTimeRequestType.GetClockSynchMethod)
            {
                this.GetClockSyncMethod();
            }
            else if (RTC_RequestType == DateTimeRequestType.GetClockTimeShiftLimit)
            {
                this.GetClockTimeShift();
            }
            else if (RTC_RequestType == DateTimeRequestType.GetPresetAdjustingTime)
            {
                this.GetPresetAdjustingTime();
            }

        }

        private void bgw_SetTime_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (RTC_RequestType == DateTimeRequestType.SetTime)
            {
                Notification notifier = new Notification("Set Time", (dataAccessResult == Data_Access_Result.Success) ? "Success" : "Error:" + dataAccessResult.ToString());
            }
            else if (RTC_RequestType == DateTimeRequestType.AdjustToMethods)
            {
                Notification notifier = new Notification("Date Time Methods", (MethodInvokeResult == Action_Result.Success) ? "Success" : "Error:" + errorMessage);
            }
            else if (RTC_RequestType == DateTimeRequestType.ClockSynchMethod)
            {
                Notification notifier = new Notification("Change Clock Sync Method", (dataAccessResult == Data_Access_Result.Success) ? "Success" : "Error:" + errorMessage);
            }

            else if (RTC_RequestType == DateTimeRequestType.ClockTimeShiftLimit)
            {
                Notification notifier = new Notification("Clock Time Shift Limit", (dataAccessResult == Data_Access_Result.Success) ? "Success" : "Error:" + errorMessage);
            }

            else if (RTC_RequestType == DateTimeRequestType.GetClockSynchMethod)
            {
                Notification notifier = new Notification("Clock Sync Method Get", (errorMessage == string.Empty) ? "Success" : "Error:" + errorMessage);
            }

            else if (RTC_RequestType == DateTimeRequestType.GetClockTimeShiftLimit)
            {
                Notification notifier = new Notification("Time Shift Range Get", (errorMessage == string.Empty) ? "Success" : "Error:" + errorMessage);
            }
            else if (RTC_RequestType == DateTimeRequestType.GetPresetAdjustingTime)
            {
                Notification notifier = new Notification("Preset Adjusting Time Get", (errorMessage == string.Empty) ? "Success" : "Error:" + errorMessage);
            }

            ucDateTime.btnTimeMethodsInvoke.Enabled = true;
            ucDateTime.btnClockSynchronizationMethodSet.Enabled = true;
            ucDateTime.btnClockLimitSet.Enabled = true;
            ucDateTime.btnClockSyncMethodGet.Enabled = true;
            ucDateTime.btnGetShiftRange.Enabled = true;
            ucDateTime.btnGetPresetAdjustTime.Enabled = true;
            Application_Controller.IsIOBusy = false;
            Application.DoEvents();
        }

        #endregion

        #region btn_ClockOnly_Handlers

        private void btn_GetTime_Click(object sender, EventArgs e)
        {
            try
            {
                bgw_Gettime.RunWorkerAsync();
            }
            catch (Exception)
            { }
            finally
            { Application_Controller.IsIOBusy = false; }
        }

        private void btn_SETtime_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                {
                    Notification notifier = new Notification("Association Error", "Create Application Association");
                }
                else
                {
                    RTC_RequestType = DateTimeRequestType.SetTime;
                    bgw_SetTime.RunWorkerAsync();
                }
            }
            catch (Exception)
            {

            }
            finally
            {

            }
        }

        #endregion

        #region Clock Sync Methods

        private void SetClockSynchMethod()
        {
            try
            {
                errorMessage = string.Empty;
                Application_Controller.IsIOBusy = true;
                dataAccessResult = Param_Controller.SET_any_class1((byte)Clock_Synch_Method, Get_Index.Clock_Synchronization_Method, DataTypes._A16_enum, 2);

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void GetClockSyncMethod()
        {
            try
            {
                errorMessage = string.Empty;
                Application_Controller.IsIOBusy = true;
                int method = (int)Param_Controller.GETDouble_Any(Get_Index.Clock_Synchronization_Method, 2);
                ucDateTime.cmbClockSynchronizationMethod.SelectedIndex = (ucDateTime.cmbClockSynchronizationMethod.Items.Count > 0) ? method : -1;

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void GetClockTimeShift()
        {
            try
            {
                errorMessage = string.Empty;
                Application_Controller.IsIOBusy = true;
                int seconds = (int)Param_Controller.GETDouble_Any(Get_Index.Clock_Time_Shift_Limit, 2);
                ucDateTime.txtClockShiftLimit.Text = seconds.ToString();

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }
        private void GetPresetAdjustingTime()
        {
            try
            {
                errorMessage = string.Empty;
                Application_Controller.IsIOBusy = true;
                Param_Controller.GET_PrestTime(ref Param_PresetTime_object);

                ucDateTime.dtpPresetTime.Value = Param_PresetTime_object.PresetTime.GetDateTime();
                ucDateTime.dtpValidityStart.Value = Param_PresetTime_object.ValidityStartInterval.GetDateTime();
                ucDateTime.dtpValidityEnd.Value = Param_PresetTime_object.ValidityEndInterval.GetDateTime();

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void TxtClockShiftLimit_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int val = Convert.ToInt32(ucDateTime.txtClockShiftLimit.Text);
                if (val >= 0 && val <= 900)
                    ucDateTime.txtClockShiftLimit.ForeColor = Color.Black;
                else
                    ucDateTime.txtClockShiftLimit.ForeColor = Color.Red;

            }
            catch (Exception)
            {
                ucDateTime.txtClockShiftLimit.ForeColor = Color.Red;
            }
        }

        private void cmbTimeMethods_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ucDateTime.cmbTimeMethods.SelectedIndex)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    {
                        ucDateTime.dtpPresetTime.Enabled = false;
                        ucDateTime.dtpValidityEnd.Enabled = false;
                        ucDateTime.dtpValidityStart.Enabled = false;
                        ucDateTime.txtSecondsToShift.Enabled = false;
                        break;
                    }
                case 4:
                    {
                        ucDateTime.dtpPresetTime.Enabled = true;
                        ucDateTime.dtpValidityEnd.Enabled = true;
                        ucDateTime.dtpValidityStart.Enabled = true;
                        ucDateTime.txtSecondsToShift.Enabled = false;
                        break;
                    }
                case 5:
                    {
                        ucDateTime.dtpPresetTime.Enabled = false;
                        ucDateTime.dtpValidityEnd.Enabled = false;
                        ucDateTime.dtpValidityStart.Enabled = false;
                        ucDateTime.txtSecondsToShift.Enabled = true;
                        break;
                    }

            }
        }

        private void btnTimeMethodsInvoke_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                {
                    Notification notifier = new Notification("Association Error", "Create Application Association");
                }
                else
                {
                    RTC_RequestType = DateTimeRequestType.AdjustToMethods;
                    ucDateTime.btnTimeMethodsInvoke.Enabled = false;
                    bgw_SetTime.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("DateTime Invoke Methods", ex.Message);
            }

        }

        private void InvokeMeterClockMethods()
        {
            int index = ucDateTime.cmbTimeMethods.SelectedIndex;
            errorMessage = string.Empty;
            try
            {
                switch (index)
                {
                    case 0:
                        {
                            MethodInvokeResult = Param_Controller.AdjustTimeToQuarter();
                            break;
                        }
                    case 1:
                        {
                            MethodInvokeResult = Param_Controller.AdjustTimeToMeasuringPeriod();
                            break;
                        }
                    case 2:
                        {
                            MethodInvokeResult = Param_Controller.AdjustTimeToMinute();
                            break;
                        }
                    case 3:
                        {
                            MethodInvokeResult = Param_Controller.AdjustTimeToPresetTime();
                            break;
                        }
                    case 4:
                        {
                            DateTime presetTime = ucDateTime.dtpPresetTime.Value;
                            DateTime validityStart = ucDateTime.dtpValidityStart.Value;
                            DateTime validityEnd = ucDateTime.dtpValidityEnd.Value;
                            MethodInvokeResult = Param_Controller.PresetAdjustingTime(presetTime, validityStart, validityEnd);
                            break;
                        }
                    case 5:
                        {
                            if (ucDateTime.txtSecondsToShift.ForeColor == Color.Red)
                                throw new Exception("Invalid Clock Shift Limit");
                            short shiftTime = Convert.ToInt16(ucDateTime.txtSecondsToShift.Text);
                            MethodInvokeResult = Param_Controller.ShiftTime(shiftTime);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void SetClockTimeShift()
        {
            try
            {
                errorMessage = string.Empty;
                if (ucDateTime.txtClockShiftLimit.ForeColor == Color.Red)
                    throw new Exception("Invalid Clock Shift Range value");
                Application_Controller.IsIOBusy = true;
                short seconds = Convert.ToInt16(ucDateTime.txtClockShiftLimit.Text);
                dataAccessResult = Param_Controller.SET_any_class1(seconds, Get_Index.Clock_Time_Shift_Limit, DataTypes._A12_long_unsigned, 2);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnClockSynchronizationMethodSet_Click(object sender, EventArgs e)
        {
            if (!(Application_Process.Is_Association_Developed))
            {
                Notification notifier = new Notification("Association Error", "Create Application Association");
            }
            else
            {
                int index = ucDateTime.cmbClockSynchronizationMethod.SelectedIndex;
                Clock_Synch_Method = (Clock_Synchronization_Method)index;

                RTC_RequestType = DateTimeRequestType.ClockSynchMethod;
                ucDateTime.btnClockSynchronizationMethodSet.Enabled = false;

                if (!bgw_SetTime.IsBusy)
                    bgw_SetTime.RunWorkerAsync();
            }

        }



        // if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '-' || e.KeyChar == '\b') //The  character represents a backspace
        //{
        //    e.Handled = false; //Do not reject the input
        //}
        //else
        //{
        //    e.Handled = true; //Reject the input
        //}


        private void btnClockLimitSet_Click(object sender, EventArgs e)
        {
            if (!(Application_Process.Is_Association_Developed))
            {
                Notification notifier = new Notification("Association Error", "Create Application Association");
            }
            else
            {
                RTC_RequestType = DateTimeRequestType.ClockTimeShiftLimit;
                ucDateTime.btnClockLimitSet.Enabled = false;
                bgw_SetTime.RunWorkerAsync();
            }

        }

        private void txtSecondsToShift_Leave(object sender, EventArgs e)
        {
            LocalCommon.TextBox_validation(short.MinValue, short.MaxValue, (TextBox)sender);
        }

        private void txtClockShiftLimit_Leave(object sender, EventArgs e)
        {
            LocalCommon.TextBox_validation(0, short.MaxValue, (TextBox)sender);
        }

        private void btnClockSyncMethodGet_Click(object sender, EventArgs e)
        {
            if (!(Application_Process.Is_Association_Developed))
            {
                Notification notifier = new Notification("Association Error", "Create Application Association");
            }
            else
            {
                RTC_RequestType = DateTimeRequestType.GetClockSynchMethod;
                ucDateTime.btnClockSyncMethodGet.Enabled = false;
                bgw_SetTime.RunWorkerAsync();

            }
        }

        private void btnGetShiftRange_Click(object sender, EventArgs e)
        {
            if (!(Application_Process.Is_Association_Developed))
            {
                Notification notifier = new Notification("Association Error", "Create Application Association");
            }
            else
            {
                RTC_RequestType = DateTimeRequestType.GetClockTimeShiftLimit;
                ucDateTime.btnGetShiftRange.Enabled = false;
                bgw_SetTime.RunWorkerAsync();

            }
        }

        private void btnGetPresetAdjustTime_Click(object sender, EventArgs e)
        {
            if (!(Application_Process.Is_Association_Developed))
            {
                Notification notifier = new Notification("Association Error", "Create Application Association");
            }
            else
            {
                RTC_RequestType = DateTimeRequestType.GetPresetAdjustingTime;
                ucDateTime.btnGetPresetAdjustTime.Enabled = false;
                bgw_SetTime.RunWorkerAsync();
            }

        }

        #endregion

        #region Handler_ContactorParams_Get_SET

        private void btn_GetContactorParams_Click(object sender, EventArgs e)
        {
            if (!(Application_Process.Is_Association_Developed))
            {
                Notification n = new Notification("Disconnected", "Create Association to Meter");
                return;
            }
            Application_Controller.IsIOBusy = true;
            if (ucContactor != null)
                ucContactor.pb_Contactor.Visible = true;
            bgw_contactor_getParams.RunWorkerAsync();
            ///modified
            //pb_Contactor.Visible = true;
            //btn_GetContactorParams.Enabled = false;
            //btn_SetContactorParameters.Enabled = false;
        }

        private void btn_SetContactorParameters_Click(object sender, EventArgs e)
        {
            if (!(Application_Process.Is_Association_Developed))
            {
                Notification n = new Notification("Disconnected", "Create Association to Meter");
                return;
            }
            Application_Controller.IsIOBusy = true;
            if (ucContactor != null)
                ucContactor.pb_Contactor.Visible = true;
            bgw_contactor_setParams.RunWorkerAsync();

        }

        private void btn_ConnectContactor_Click(object sender, EventArgs e)
        {
            if (!(Application_Process.Is_Association_Developed))
            {
                Notification n = new Notification("Disconnected", "Create Association to Meter");
                return;
            }
            Application_Controller.IsIOBusy = true;
            bgw_Contactor.RunWorkerAsync();

            ///***modified
            //btn_DisconnectContactor.Enabled = false;
            //btnReadStatus.Enabled = false;
            //btn_ConnectContactor.Enabled = false;

        }

        private void btn_connectThroughSwitch_Click(object sender, EventArgs e)
        {
            if (!(Application_Process.Is_Association_Developed))
            {
                Notification n = new Notification("Disconnected", "Create Association to Meter");
                return;
            }
            Application_Controller.IsIOBusy = true;
            // Instantanous_Controller.GET_Any_string(Get_Index.Contactor_connect_through_Switch, 2, 1);
            Class_1 baseClass = (Class_1)Param_Controller.GetSAPEntry(Get_Index.Contactor_connect_through_Switch);
            baseClass.EncodingType = DataTypes._A16_enum;
            baseClass.EncodingAttribute = 2;
            baseClass.Value = 1;
            Param_Controller.SET_Param(baseClass);
            Application_Controller.IsIOBusy = false;
        }

        private void btn_DisconnectContactor_Click(object sender, EventArgs e)
        {
            if (!(Application_Process.Is_Association_Developed))
            {
                Notification n = new Notification("Disconnected", "Create Association to Meter");
                return;
            }
            Application_Controller.IsIOBusy = true;
            bgw_contactor_disconnect.RunWorkerAsync();
            ///***modified
            //btn_DisconnectContactor.Enabled = false;
            //btnReadStatus.Enabled = false;
            //btn_ConnectContactor.Enabled = false;
        }

        private void btnReadStatus_Click(object sender, EventArgs e)
        {
            if (!(Application_Process.Is_Association_Developed))
            {
                Notification n = new Notification("Disconnected", "Create Association to Meter");
                return;
            }
            Application_Controller.IsIOBusy = true;
            bgw_contactor_status.RunWorkerAsync();
            ///***modified
            //btn_DisconnectContactor.Enabled = false;
            //btnReadStatus.Enabled = false;
            //btn_ConnectContactor.Enabled = false;
        }

        #endregion

        #region Contactor_Background_Worker

        private void bgw_contactor_setParams_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                Application_Controller.IsIOBusy = false;
                if (ucContactor != null)
                {
                    ucContactor.pb_Contactor.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error Programming Contactor Parameters", ex.Message, 5000);
            }
        }

        private void bgw_contactor_setParams_DoWork(object sender, DoWorkEventArgs e)
        {
            #region Commented_Section
            ///***modified
            //Param_Contactor_object.Over_MDI_T1_FLAG_4 = check_Contactor_MDIOverLoad_T1.Checked;
            //Param_Contactor_object.Over_MDI_T2_FLAG_5 = check_Contactor_MDIOverLoad_T2.Checked;
            //Param_Contactor_object.Over_MDI_T3_FLAG_6 = check_Contactor_MDIOverLoad_T3.Checked;
            //Param_Contactor_object.Over_MDI_T4_FLAG_7 = check_Contactor_MDIOverLoad_T4.Checked;
            //Param_Contactor_object.Over_Current_By_Phase_T1_FLAG_0 = check_Contactor_OverCurrentByPhase_T1.Checked;
            //Param_Contactor_object.Over_Current_By_Phase_T2_FLAG_1 = check_Contactor_OverCurrentByPhase_T2.Checked;
            //Param_Contactor_object.Over_Current_By_Phase_T3_FLAG_2 = check_Contactor_OverCurrentByPhase_T3.Checked;
            //Param_Contactor_object.Over_Current_By_Phase_T4_FLAG_3 = check_Contactor_OverCurrentByPhase_T4.Checked;
            //Param_Contactor_object.Over_Load_By_Phase_T1_FLAG_4 = check_Contactor_OverLoadByPhase_T1.Checked;
            //Param_Contactor_object.Over_Load_By_Phase_T2_FLAG_5 = check_Contactor_OverLoadByPhase_T2.Checked;
            //Param_Contactor_object.Over_Load_By_Phase_T3_FLAG_6 = check_Contactor_OverLoadByPhase_T3.Checked;
            //Param_Contactor_object.Over_Load_By_Phase_T4_FLAG_7 = check_Contactor_OverLoadByPhase_T4.Checked;
            //Param_Contactor_object.Over_Load_T1_FLAG_0 = check_Contactor_OverLoadTotal_T1.Checked;

            //if (!Application_Controller.isSinglePhase)
            //{
            //    Param_Contactor_object.Over_Load_T2_FLAG_1 = check_Contactor_OverLoadTotal_T2.Checked;
            //    Param_Contactor_object.Over_Load_T3_FLAG_2 = check_Contactor_OverLoadTotal_T3.Checked;
            //    Param_Contactor_object.Over_Load_T4_FLAG_3 = check_Contactor_OverLoadTotal_T4.Checked;
            //}

            //Param_Contactor_object.Over_Volt_FLAG_0 = check_Contactor_OverVolt.Checked;
            //Param_Contactor_object.Under_Volt_FLAG_1 = check_Contactor_UnderVolt.Checked;
            //Param_Contactor_object.off_by_optically = check_Contactor_offOptically.Checked;
            //Param_Contactor_object.on_by_optically = check_Contactor_onOptically.Checked;

            //if (radio_contactor_auto.Checked)
            //{
            //    Param_Contactor_object.reconnect_automatic_or_switch = true;
            //}
            //else
            //{
            //    Param_Contactor_object.reconnect_automatic_or_switch = false;

            //}

            ////Param_Contactor_object.reconnect_automatic_or_switch = check_Contactor_ReconnectAutoOrSwitch.Checked;

            //Param_Contactor_object.Reconnect_Automatically_on_Retries_Expire = check_contactor_reconnectAuto.Checked;
            //Param_Contactor_object.Reconnect_By_Switch_on_Retries_Expire = check_contactor_reconnectSwitch.Checked;

            //Param_Contactor_object.reconnect_by_tariff_change = check_Contactor_ReconnectonTariffChange.Checked;

            //Param_Limits_object.Over_Load_Total_T1 = Convert.ToDouble(txt_OverLoadTotal_T1.Text) * 1000;
            //Param_Limits_object.Over_Load_Total_T2 = Convert.ToDouble(txt_OverLoadTotal_T2.Text) * 1000;
            //Param_Limits_object.Over_Load_Total_T3 = Convert.ToDouble(txt_OverLoadTotal_T3.Text) * 1000;
            //Param_Limits_object.Over_Load_Total_T4 = Convert.ToDouble(txt_OverLoadTotal_T4.Text) * 1000; 
            #endregion

            ///Set Contactor Parameters
            Param_Controller.SET_ContactorParams(Param_Contactor_Object);

            Param_Controller.SET_Limit_Over_Load_Total_T1(Param_Limits_Object);

            if (!Application_Controller.isSinglePhase)
            {
                Param_Controller.SET_Limit_Over_Load_Total_T2(Param_Limits_Object);
                Param_Controller.SET_Limit_Over_Load_Total_T3(Param_Limits_Object);
                Param_Controller.SET_Limit_Over_Load_Total_T4(Param_Limits_Object);
                Param_Controller.SET_MT_Over_Load(Param_Monitoring_Time_Object);
            }

        }

        private void bgw_contactor_getParams_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                Application_Controller.IsIOBusy = false;
                if (ucContactor != null)
                {
                    ucContactor.showToGUI_Contactor();
                    ucContactor.showToGUI_Limits();
                    ucContactor.showToGUI_MonitoringTime();
                    ucContactor.pb_Contactor.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error Reading Contactor Parameters", ex.Message, 5000);
            }
        }

        private void bgw_contactor_getParams_DoWork(object sender, DoWorkEventArgs e)
        {
            Param_Contactor _Param_Contactor_Object = Param_Contactor_Object;
            var _Param_Limits_Object = Param_Limits_Object;
            var _Param_Monitoring_Time_Object = Param_Monitoring_Time_Object;

            Param_Controller.GET_ContactorParams(ref _Param_Contactor_Object);
            Param_Controller.GET_Limit_Over_Load_Total_T1(ref _Param_Limits_Object);
            if (!Application_Controller.isSinglePhase)
            {
                Param_Controller.GET_Limit_Over_Load_Total_T2(ref _Param_Limits_Object);
                Param_Controller.GET_Limit_Over_Load_Total_T3(ref _Param_Limits_Object);
                Param_Controller.GET_Limit_Over_Load_Total_T4(ref _Param_Limits_Object);
                Param_Controller.GET_MT_Over_Load(ref _Param_Monitoring_Time_Object);
            }
        }

        private void bgw_Contactor_DoWork(object sender, DoWorkEventArgs e)
        {
            Param_Contactor_Object.contactor_read_Status = null;
            Action_Result r = Param_Controller.RelayConnectRequest();
            if (r != Action_Result.Success)
                throw new Exception(String.Format("Unable exec contactor Connect Command,coz {0}", r));
        }

        private void bgw_Contactor_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Application_Controller.IsIOBusy = false;
            if (ucContactor != null)
                ucContactor.update_Contactor_Status();
            ///***modified
            //btn_DisconnectContactor.Enabled = true;
            //btnReadStatus.Enabled = true;
            //btn_ConnectContactor.Enabled = true;
        }

        private void bgw_contactor_disconnect_DoWork(object sender, DoWorkEventArgs e)
        {
            Param_Contactor_Object.contactor_read_Status = null;
            Action_Result r = Param_Controller.RelayDisConnectRequest();
            if (r != Action_Result.Success)
                throw new Exception(String.Format("Unable exec contactor Disconnect Command,coz {0}", r));
        }

        private void bgw_contactor_disconnect_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Application_Controller.IsIOBusy = false;
            if (ucContactor != null)
                ucContactor.update_Contactor_Status();
            ///modified
            //btn_DisconnectContactor.Enabled = true;
            //btnReadStatus.Enabled = true;
            //btn_ConnectContactor.Enabled = true;

        }

        private void bgw_contactor_status_DoWork(object sender, DoWorkEventArgs e)
        {
            Application_Controller.IsIOBusy = true;
            bool status = Param_Controller.GET_Relay_Status();
            ///modified
            ///check_Status.Checked = status;
            Param_Contactor_Object.contactor_read_Status = status;
        }

        private void bgw_contactor_status_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Application_Controller.IsIOBusy = false;
            if (ucContactor != null)
                ucContactor.update_Contactor_Status();
            ///modified
            ///btn_DisconnectContactor.Enabled = true;
            ///btnReadStatus.Enabled = true;
            ///btn_ConnectContactor.Enabled = true;
        }

        #endregion

        bool IsWapdaFormat = true;
        //Param_for_WapdaFormat_rpt param_obj;

        private string get_TariffTimings(List<TimeSlot> timeSlot)
        {
            string Timing = "";
            string TariffTimeFormat = @"hh";
            for (int i = 0; i < timeSlot.Count; i++)
            {
                Timing += timeSlot[i].StartTime.ToString(TariffTimeFormat) + "-";
                if (i + 1 < timeSlot.Count)
                {
                    Timing += timeSlot[i + 1].StartTime.ToString(TariffTimeFormat) + " (" + timeSlot[i].ScriptSelector + "), ";
                }
                else if (i + 1 == timeSlot.Count)
                {
                    Timing += timeSlot[0].StartTime.ToString(TariffTimeFormat) + " (" + timeSlot[i].ScriptSelector + ")";
                }
            }
            return Timing;
        }

        private int get_TariffCount(List<TimeSlot> timeSlot)
        {
            bool t1 = false, t2 = false, t3 = false, t4 = false;
            int tariffCount = 0;
            foreach (TimeSlot ts in timeSlot)
            {
                if (ts.ScriptSelector == Tarrif_ScriptSelector.T1 && !t1)
                {
                    tariffCount++;
                    t1 = true;
                }
                else if (ts.ScriptSelector == Tarrif_ScriptSelector.T2 && !t2)
                {
                    tariffCount++;
                    t2 = true;
                }
                else if (ts.ScriptSelector == Tarrif_ScriptSelector.T3 && !t3)
                {
                    tariffCount++;
                    t3 = true;
                }
                else if (ts.ScriptSelector == Tarrif_ScriptSelector.T4 && !t4)
                {
                    tariffCount++;
                    t4 = true;
                }
            }

            return tariffCount;
        }

        private string get_TariffTimings_(List<TimeSlot> timeSlot)
        {
            string Timing = "";
            string TariffTimeFormat = @"hh";
            for (int i = 0; i < timeSlot.Count; i++)
            {
                if (timeSlot.Count == 1)
                {
                    string time = (timeSlot[i].StartTime.ToString(TariffTimeFormat) + "-");
                    Timing += ((i == 0) ? time : ", " + time);

                    if (i + 1 < timeSlot.Count)
                    {
                        Timing += timeSlot[i + 1].StartTime.ToString(TariffTimeFormat) + " (" + timeSlot[i].ScriptSelector + ")";
                    }
                    else if (i + 1 == timeSlot.Count)
                    {
                        Timing += timeSlot[0].StartTime.ToString(TariffTimeFormat) + " (" + timeSlot[i].ScriptSelector + ")";
                    }
                }
                else if (i + 1 == timeSlot.Count && timeSlot[0].ScriptSelector == timeSlot[i].ScriptSelector)
                {
                    string newStr = timeSlot[i].StartTime.ToString(TariffTimeFormat) + "-";
                    Timing = Timing.Replace("00-", newStr);
                }
                else
                {
                    string time = (timeSlot[i].StartTime.ToString(TariffTimeFormat) + "-");
                    Timing += ((i == 0) ? time : ", " + time);

                    if (i + 1 < timeSlot.Count)
                    {
                        Timing += timeSlot[i + 1].StartTime.ToString(TariffTimeFormat) + " (" + timeSlot[i].ScriptSelector + ")";
                    }
                    else if (i + 1 == timeSlot.Count)
                    {
                        Timing += timeSlot[0].StartTime.ToString(TariffTimeFormat) + " (" + timeSlot[i].ScriptSelector + ")";
                    }
                }
            }
            return Timing;
        }
        private void DisplayParamRpt_W(MeterConfig meter_type_info)
        {
            string Meter_Parameters = "Meter Parameters";
            string Tariffs_Parameters = "Tariffs Parameters";
            string Season_Parameters = "Season Parameters";
            string Programmable = "Programmable";
            string parameter = "parameter",
                value1 = "value1",
                value2 = "value2",
                value3 = "value3",
                ParameterGroup = "ParameterGroup";
            int dataset_row_counter = 1;

            if (Dataset_Param.Meter_Parameters.Rows.Count > 0)
                Dataset_Param.Meter_Parameters.Rows.Clear();

            if (meter_type_info.MeterType.Contains("PT"))
            {
                Dataset_Param.Meter_Parameters.Rows.Add();
                Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][parameter] = "P.T Ratio";
                Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value1] = Programmable;
                Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value2] = Param_CTPT_Ratio_Object.PTratio_Numerator + ":" + Param_CTPT_Ratio_Object.PTratio_Denominator;
                Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value3] = "";
                Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][ParameterGroup] = Meter_Parameters;
                dataset_row_counter++;
            }

            if (meter_type_info.MeterType.Contains("CT") || meter_type_info.MeterType.Contains("PT"))
            {
                Dataset_Param.Meter_Parameters.Rows.Add();
                Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][parameter] = "C.T Ratio";
                Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value1] = Programmable;
                Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value2] = Param_CTPT_Ratio_Object.CTratio_Numerator + ":" + Param_CTPT_Ratio_Object.CTratio_Denominator;
                Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value3] = "";
                Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][ParameterGroup] = Meter_Parameters;
                dataset_row_counter++;
            }

            Dataset_Param.Meter_Parameters.Rows.Add();
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][parameter] = "Over Voltage";
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value1] = Programmable;
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value2] = Param_Limits_Object.OverVolt.ToString() + "V";
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value3] = Param_Monitoring_Time_Object.UnderVolt.Minutes.ToString().PadLeft(2, '0') + ":" + Param_Monitoring_Time_Object.UnderVolt.Seconds.ToString().PadLeft(2, '0') + " mm:ss";
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][ParameterGroup] = Meter_Parameters;
            dataset_row_counter++;

            Dataset_Param.Meter_Parameters.Rows.Add();
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][parameter] = "Under Voltage";
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value1] = Programmable;
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value2] = Param_Limits_Object.UnderVolt.ToString() + "V";  //v4.8.29
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value3] = Param_Monitoring_Time_Object.UnderVolt.Minutes.ToString().PadLeft(2, '0') + ":" + Param_Monitoring_Time_Object.UnderVolt.Seconds.ToString().PadLeft(2, '0') + " mm:ss";
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][ParameterGroup] = Meter_Parameters;
            dataset_row_counter++;

            Dataset_Param.Meter_Parameters.Rows.Add();
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][parameter] = "Power Limit, kW";
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value1] = Programmable;
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value2] = Param_Limits_Object.OverLoadTotal_T1.ToString() + " kW";
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value3] = Param_Monitoring_Time_Object.OverLoad.Minutes.ToString().PadLeft(2, '0') + ":" + Param_Monitoring_Time_Object.OverLoad.Seconds.ToString().PadLeft(2, '0') + " mm:ss";
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][ParameterGroup] = Meter_Parameters;
            dataset_row_counter++;

            Dataset_Param.Meter_Parameters.Rows.Add();
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][parameter] = "Date of Reset";
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value1] = Programmable;
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value2] = Param_MDI_Parameters_Object.Auto_reset_date.DayOfMonth.ToString();
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value3] = "";
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][ParameterGroup] = Meter_Parameters;
            dataset_row_counter++;

            Dataset_Param.Meter_Parameters.Rows.Add();
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][parameter] = "Display Scrolling Time";
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value1] = Programmable + " in second";
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value2] = Param_DisplayWindowsNormal.ScrollTime.TotalSeconds.ToString() + " sec";
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value3] = "";
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][ParameterGroup] = Meter_Parameters;
            dataset_row_counter++;

            Dataset_Param.Meter_Parameters.Rows.Add();
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][parameter] = "Reset Method";
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value1] = "Automatic/Manual";//Programmable;
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value2] = (Param_MDI_Parameters_Object.FLAG_Auto_Reset_0) ? "Automatic" : "Manual";
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value3] = "";
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][ParameterGroup] = Meter_Parameters;
            dataset_row_counter++;

            Dataset_Param.Meter_Parameters.Rows.Add();
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][parameter] = "Integration Period\n(Demand Interval)";
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value1] = Programmable;
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value2] = Param_MDI_Parameters_Object.MDI_Interval.ToString() + " Min";
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value3] = "";
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][ParameterGroup] = Meter_Parameters;
            dataset_row_counter++;

            //////////////////////////// Tariffs Parameters
            Param_ActivityCalendar CalendarTemp = Calendar;
            CalendarTemp.ParamSeasonProfile.seasonProfile_Table.Sort((x, y) => x.Profile_Name_Str.CompareTo(y.Profile_Name_Str));
            Dataset_Param.Meter_Parameters.Rows.Add();
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][parameter] = "Number of Seasons";
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value1] = CalendarTemp.ParamSeasonProfile.seasonProfile_Table.Count.ToString().PadLeft(2, '0');
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value2] = "";
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value3] = "";
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][ParameterGroup] = Tariffs_Parameters;
            dataset_row_counter++;

            Dataset_Param.Meter_Parameters.Rows.Add();
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][parameter] = "Tariff Number";
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value1] = get_TariffCount(CalendarTemp.ParamSeasonProfile.seasonProfile_Table[0].Week_Profile.Day_Profile_WED.dayProfile_Schedule).ToString().PadLeft(2, '0'); //v4.8.30
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value2] = "";
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value3] = "";
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][ParameterGroup] = Tariffs_Parameters;
            dataset_row_counter++;
            //////////////////////////// Season Parameters
            int SeasonNum = 0;
            Dataset_Param.Meter_Parameters.Rows.Add();
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][parameter] =
                CalendarTemp.ParamSeasonProfile.seasonProfile_Table[SeasonNum].Start_Date.DayOfMonth.ToString().PadLeft(2, '0') + " / " +
                CalendarTemp.ParamSeasonProfile.seasonProfile_Table[SeasonNum].Start_Date.Month.ToString().PadLeft(2, '0');
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value1] = "Day Time";
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value2] =
                get_TariffTimings_(CalendarTemp.ParamSeasonProfile.seasonProfile_Table[SeasonNum].Week_Profile.Day_Profile_MON.dayProfile_Schedule);

            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value3] = "";
            Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][ParameterGroup] = Season_Parameters;
            dataset_row_counter++; SeasonNum++;

            if (CalendarTemp.ParamSeasonProfile.seasonProfile_Table.Count > 1)
            {
                Dataset_Param.Meter_Parameters.Rows.Add();
                Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][parameter] =
                    CalendarTemp.ParamSeasonProfile.seasonProfile_Table[SeasonNum].Start_Date.DayOfMonth.ToString().PadLeft(2, '0') + " / " +
                    CalendarTemp.ParamSeasonProfile.seasonProfile_Table[SeasonNum].Start_Date.Month.ToString().PadLeft(2, '0');
                Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value1] = "Day Time";
                Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value2] =
                       get_TariffTimings_(CalendarTemp.ParamSeasonProfile.seasonProfile_Table[SeasonNum].Week_Profile.Day_Profile_WED.dayProfile_Schedule);
                Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value3] = "";
                Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][ParameterGroup] = Season_Parameters;
                dataset_row_counter++; SeasonNum++;

            }
            if (CalendarTemp.ParamSeasonProfile.seasonProfile_Table.Count > 2)
            {
                Dataset_Param.Meter_Parameters.Rows.Add();
                Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][parameter] =
                    CalendarTemp.ParamSeasonProfile.seasonProfile_Table[SeasonNum].Start_Date.DayOfMonth.ToString().PadLeft(2, '0') + " / " +
                    CalendarTemp.ParamSeasonProfile.seasonProfile_Table[SeasonNum].Start_Date.Month.ToString().PadLeft(2, '0');
                Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value1] = "Day Time";
                Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value2] =
                    get_TariffTimings_(CalendarTemp.ParamSeasonProfile.seasonProfile_Table[SeasonNum].Week_Profile.Day_Profile_WED.dayProfile_Schedule);
                Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value3] = "";
                Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][ParameterGroup] = Season_Parameters;
                dataset_row_counter++; SeasonNum++;
            }

            if (CalendarTemp.ParamSeasonProfile.seasonProfile_Table.Count > 3)
            {
                Dataset_Param.Meter_Parameters.Rows.Add();
                Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][parameter] =
                    CalendarTemp.ParamSeasonProfile.seasonProfile_Table[SeasonNum].Start_Date.DayOfMonth.ToString().PadLeft(2, '0') + " / " +
                    CalendarTemp.ParamSeasonProfile.seasonProfile_Table[SeasonNum].Start_Date.Month.ToString().PadLeft(2, '0');
                Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value1] = "Day Time";
                Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value2] =
                    get_TariffTimings_(CalendarTemp.ParamSeasonProfile.seasonProfile_Table[SeasonNum].Week_Profile.Day_Profile_WED.dayProfile_Schedule);
                Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][value3] = "";
                Dataset_Param.Meter_Parameters.Rows[dataset_row_counter - 1][ParameterGroup] = Season_Parameters;
                dataset_row_counter++;
            }

            if (Param_Controller.CurrentConnectionInfo != null)
            {
                ReportViewer rpt = new ReportViewer(Dataset_Param.Meter_Parameters, Param_Controller.CurrentConnectionInfo.MSN,
                Param_Controller.CurrentConnectionInfo.MeterInfo.MeterModel, obj_CustomerCode.Customer_Code_String, obj_CustomerCode.Customer_Name, obj_CustomerCode.Customer_Address,
                Application_Controller.Applicationprocess_Controller.UserId, Instantaneous_Class_obj.Active_Season.ToString(), meter_type_info);
                rpt.Show();
            }
            else
                MessageBox.Show("Please Read some parameters first");
        }
        private void Display_DisplayWindows_Rpt(DisplayWindows obj, int RptType, MeterConfig meter_type_info)
        {
            string tableName = "DisplayWindows";
            Dataset_Param.Tables[tableName].Rows.Clear();

            int dataset_row_counter = 1;  //1;

            for (int i = 0; i < obj.Windows.Count; i++)
            {
                Dataset_Param.Tables[tableName].Rows.Add();
                Dataset_Param.Tables[tableName].Rows[i][0] = obj.Windows[i].WindowNumberToDisplay;
                Dataset_Param.Tables[tableName].Rows[i][1] = obj.Windows[i].Window_Name;

                dataset_row_counter++;
            }

            ////Display Window Scroll Time added by M.Azeem Inayat
            //if (obj.WindowsMode == DispalyWindowsModes.Normal && Dataset_Param.Tables[tableName].Rows.Count > 0)
            //{
            //    Dataset_Param.Tables[tableName].Rows.Add();
            //    Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][0] = "Display Scroll Time";
            //    Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][1] = obj.ScrollTime.TotalSeconds + " (seconds)";
            //}

            if (dataset_row_counter != 1)
            {
                for (int i = 0; i < Dataset_Param.Tables[tableName].Rows.Count; i++)
                    Dataset_Param.Tables[tableName].Rows[i][0] =
                        Dataset_Param.Tables[tableName].Rows[i][0].ToString().Replace('_', ' ');
            }
            ReportViewer rpt = new ReportViewer(Dataset_Param.DisplayWindows, Param_Controller.CurrentConnectionInfo.MSN,
                                    Param_Controller.CurrentConnectionInfo.MeterInfo.MeterModel, obj_CustomerCode.Customer_Code_String,
                                    Application_Controller.Applicationprocess_Controller.UserId, Instantaneous_Class_obj.Active_Season.ToString(),
                                    RptType, meter_type_info, application_Controller.CurrentUser.CurrentAccessRights);
            rpt.Show();
        }
        private void btn_GenerateReport_Click(object sender, EventArgs e)
        {
            MeterConfig meter_type_info = application_Controller.ConnectionController.SelectedMeter;

            if (rdbAdvance.Checked && rdbAdvance.Visible)
            {
                CustGetDialog.IsReadMode = 0;
                CustGetDialog.setEnableStatus();
                if (CustGetDialog.ShowDialog(this.Parent) == DialogResult.OK && CustGetDialog.IsAnyChecked)
                //if (CustGetDialog.ShowDialog(this.Parent) == DialogResult.OK)
                {
                    //CustGetDialog.ShowDialog();
                    #region FILL_DATASET_TABLES & DISPLAY_REPORT

                    Dataset_Param.Clear();
                    #region ACTIVITY_CALENDER

                    if (CustGetDialog.check_ActivityCalender.Checked)
                        to_fill_Calender(Calendar, "Calender");
                    else
                    {
                        Dataset_Param.Tables["Calender"].Clear();
                        dataset_calendar.Clear();
                    }

                    #endregion
                    #region CT PT RATIOS

                    if (CustGetDialog.check_CTPT.Checked)
                        to_fill_ABC(Param_CTPT_Ratio_Object, "CT & PT Ratio");
                    else
                        Dataset_Param.Tables["CT & PT Ratio"].Clear();

                    #endregion
                    #region CLOCK CALIBRATION & TIME

                    if (CustGetDialog.check_Clock.Checked || CustGetDialog.check_Time.Checked)
                        to_fill_ClockCalibration(Param_Clock_Caliberation_Object, "Clock Calibration");
                    else
                        Dataset_Param.Tables["Clock Calibration"].Clear();

                    #endregion
                    #region CONTACTOR

                    if (CustGetDialog.check_Contactor.Checked)
                        to_fill_Contactor(Param_Contactor_Object, "Contactor");
                    else
                        Dataset_Param.Tables["Contactor"].Clear();

                    #endregion
                    #region CUSTOMER CODE

                    if (CustGetDialog.check_customerReference.Checked)
                        to_fill_CustomerReference(Param_Customer_Code_Object, "Customer Code");
                    else
                        Dataset_Param.Tables["Customer Code"].Clear();

                    #endregion
                    #region DECIMAL POINTS

                    if (CustGetDialog.check_DecimalPoint.Checked)
                        to_fill_DecimalPoints(Param_Decimal_Point_Object, "Decimal Points");
                    else
                        Dataset_Param.Tables["Decimal Points"].Clear();

                    #endregion
                    #region DISPLAY WINDOWS

                    if (CustGetDialog.check_DisplayWindows_Alt.Checked)
                        to_fill_DisplayWindows(ucDisplayWindows1.Obj_displayWindows_alternate, "Display Windows Alternate");
                    else
                        Dataset_Param.Tables["Display Windows Alternate"].Clear();

                    if (CustGetDialog.check_DisplayWindows_Nor.Checked)
                        to_fill_DisplayWindows(ucDisplayWindows1.Obj_displayWindows_normal, "Display Windows Normal");
                    else
                        Dataset_Param.Tables["Display Windows Normal"].Clear();

                    if (CustGetDialog.check_DisplayWindows_test.Checked)
                        to_fill_DisplayWindows(ucDisplayWindows1.Obj_displayWindows_test, "Display Windows TestMode");
                    else
                        Dataset_Param.Tables["Display Windows TestMode"].Clear();

                    #endregion
                    #region ENERGY PARAMETERS

                    if (CustGetDialog.check_EnergyParams.Checked)
                        to_fill_EnergyParameters(Param_Energy_Parameters_Object, "Energy Parameters");
                    else
                        Dataset_Param.Tables["Energy Parameters"].Clear();

                    #endregion
                    #region LIMITS

                    if (CustGetDialog.check_Limits.Checked)
                        to_fill_Limits(Param_Limits_Object, "Limits"); //by Azeem //Param_Limits_Object, "Limits");
                    else
                        Dataset_Param.Tables["Limits"].Clear();

                    #endregion
                    #region LOAD PROFILE

                    if (CustGetDialog.chk_LoadProfile_Interval.Checked || CustGetDialog.chk_LoadProfile.Checked)
                    {

                        Param_Load_Profile Param_load_profile_object = new Param_Load_Profile();
                        to_fill_ABC(ucLoadProfile.LoadProfileChannelsInfo, "Load Profile"); // Param_load_profile_object, "Load Profile");
                    }
                    else
                        Dataset_Param.Tables["Load Profile"].Clear();

                    #endregion
                    #region MDI PARAMETERS

                    if (CustGetDialog.check_MDI_params.Checked)
                        to_fill_MDIParameters(Param_MDI_Parameters_Object, "MDI Parameters");
                    else
                        Dataset_Param.Tables["MDI Parameters"].Clear();

                    #endregion
                    #region MONITORNG TIMES

                    if (CustGetDialog.check_MonitoringTime.Checked)
                        to_fill_MonitoringTimes(/*Param_Monitoring_Time_Object*/ ucMonitoringTime.Param_Monitoring_time_object, "Monitoring Times");
                    else
                        Dataset_Param.Tables["Monitoring Times"].Clear();

                    #endregion
                    #region PASSWORD

                    if (CustGetDialog.check_Password_Elec.Checked)
                        to_fill_ABC(Param_Password_Object, "Password");
                    else
                        Dataset_Param.Tables["Password"].Clear();

                    #endregion
                    #region TCP UDP

                    if (CustGetDialog.check_TCPUDP.Checked)
                        to_fill_ABC(Param_TCP_UDP_object, "TCP UDP");
                    else
                        Dataset_Param.Tables["TCP UDP"].Clear();

                    #endregion
                    #region MODEM PARAMETERS

                    if (CustGetDialog.check_IP_Profile.Checked)
                        to_fill_IPProfile(Param_IP_Profiles_object[0], Param_IP_Profiles_object[1],
                                          Param_IP_Profiles_object[2], Param_IP_Profiles_object[3], "IP Profile");
                    else
                        Dataset_Param.Tables["IP Profile"].Clear();

                    if (CustGetDialog.chbWakeupProfile.Checked)
                        to_fill_WakeupProfile(Param_Wakeup_Profile_object[0], Param_Wakeup_Profile_object[1],
                                              Param_Wakeup_Profile_object[2], Param_Wakeup_Profile_object[3],
                                              "Wakeup Profile");
                    else
                        Dataset_Param.Tables["Wakeup Profile"].Clear();

                    if (CustGetDialog.chbNumberProfile.Checked)
                        to_fill_NumberProfile(Param_Number_Profile_object[0], Param_Number_Profile_object[1],
                                              Param_Number_Profile_object[2], Param_Number_Profile_object[3],
                                              Param_Number_Profile_object[4], "Number Profile");
                    else
                        Dataset_Param.Tables["Number Profile"].Clear();

                    if (CustGetDialog.chbCommunicationProfile.Checked)
                        to_fill_CommunicationProfile(Param_Communication_Profile_object,
                                                     Param_Communication_Profile_object.NumberProfileID,
                                                     "Communication Profile");
                    else
                        Dataset_Param.Tables["Communication Profile"].Clear();

                    if (CustGetDialog.chbKeepAlive.Checked)
                        to_fill_KeepAlive(Param_Keep_Alive_IP_object, "Keep Alive");
                    else
                        Dataset_Param.Tables["Keep Alive"].Clear();

                    if (CustGetDialog.chbModemLimitsAndTime.Checked)
                        to_fill_ModemLimitsTimes(Param_ModemLimitsAndTime_Object, "Modem Limits & Times");
                    else
                        Dataset_Param.Tables["Modem Limits & Times"].Clear();

                    if (CustGetDialog.chbModemInitialize.Checked)
                        to_fill_ModemInitialize(Param_Modem_Initialize_Object, Param_ModemBasics_NEW_object,
                                                "Modem Initialize");
                    else
                        Dataset_Param.Tables["Modem Initialize"].Clear();

                    if (CustGetDialog.check_TBEs.Checked)
                        getTimeBasesEventsReportData();
                    else
                        Dataset_Events.DataTable_Time_Based_Events.Clear();

                    #endregion


                    ReportViewer rpt = new ReportViewer(Dataset_Param, Dataset_Events, dataset_calendar, Param_Controller.CurrentConnectionInfo.MSN,
                        Param_Controller.CurrentConnectionInfo.MeterInfo.MeterModel, "Hello World.",
                        obj_CustomerCode.Customer_Code_String, Application_Controller.Applicationprocess_Controller.UserId, Instantaneous_Class_obj.Active_Season.ToString()
                        , meter_type_info);
                    rpt.Show();
                    #endregion
                }
            }
            else //(rdbNormal.Checked)
            {
                ParamReportSelection RptSelection = new ParamReportSelection(application_Controller.CurrentUser.CurrentAccessRights.DisplayWindowsRights);
                if (RptSelection.ShowDialog() == DialogResult.OK)
                {
                    if (RptSelection.bParamRptType == 1) //
                    {
                        DisplayParamRpt_W(meter_type_info);
                    }
                    else if (RptSelection.bParamRptType == 2)
                    {
                        Display_DisplayWindows_Rpt(ucDisplayWindows1.Obj_displayWindows_normal, 2, meter_type_info);
                    }
                    else if (RptSelection.bParamRptType == 3)
                    {
                        Display_DisplayWindows_Rpt(ucDisplayWindows1.Obj_displayWindows_alternate, 3, meter_type_info);
                    }
                    else if (RptSelection.bParamRptType == 4)
                    {
                        Display_DisplayWindows_Rpt(ucDisplayWindows1.Obj_displayWindows_test, 4, meter_type_info);
                    }
                    else
                    {
                    }
                }
            }
        }

        #region Parameterization_Report

        private void to_fill_ClockCalibration(object obj, string tableName)
        {
            int dataset_row_counter = 1;
            Type type = obj.GetType();
            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo field in fields)
            {
                Dataset_Param.Tables[tableName].Rows.Add();
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][0] = field.Name;
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][1] = field.GetValue(obj).ToString();

                dataset_row_counter++;
            }
            if (dataset_row_counter != 1)
            {
                for (int i = 0; i < Dataset_Param.Tables[tableName].Rows.Count; i++)
                    Dataset_Param.Tables[tableName].Rows[i][0] =
                        Dataset_Param.Tables[tableName].Rows[i][0].ToString().Replace('_', ' ');

                Dataset_Param.Tables[tableName].Rows[0][1] =
                    Dataset_Param.Tables[tableName].Rows[0][1].ToString().Split(' ')[0];

                Dataset_Param.Tables[tableName].Rows[1][1] =
                    Dataset_Param.Tables[tableName].Rows[1][1].ToString().Split(' ')[0];

                Dataset_Param.Tables[tableName].Rows[2].Delete();
                Dataset_Param.Tables[tableName].Rows[3].Delete();
                Dataset_Param.Tables[tableName].Rows[3].Delete();
            }
        }
        private void to_fill_Contactor(object obj, string tableName)
        {
            int dataset_row_counter = 1;
            Type type = obj.GetType();
            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo field in fields)
            {
                try
                {
                    Dataset_Param.Tables[tableName].Rows.Add();
                    Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][0] = field.Name;
                    Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][1] = (field.GetValue(obj) == null ? string.Empty : field.GetValue(obj).ToString());

                    dataset_row_counter++;
                }
                catch (Exception)
                {
                }
            }
            if (dataset_row_counter != 1)
            {

                for (int i = 0; i < Dataset_Param.Tables[tableName].Rows.Count; i++)
                {
                    try
                    {
                        Dataset_Param.Tables[tableName].Rows[i][0] =
                    Dataset_Param.Tables[tableName].Rows[i][0].ToString().Replace('_', ' ');
                    }
                    catch (Exception)
                    { }
                }
                try
                {
                    Dataset_Param.Tables[tableName].Rows[0][1] = Dataset_Param.Tables[tableName].Rows[0][1] + " mSec";
                    Dataset_Param.Tables[tableName].Rows[1][1] = Dataset_Param.Tables[tableName].Rows[1][1] + " mSec";
                    Dataset_Param.Tables[tableName].Rows[2][1] = Dataset_Param.Tables[tableName].Rows[2][1] + " Sec";
                    Dataset_Param.Tables[tableName].Rows[3][1] = Dataset_Param.Tables[tableName].Rows[3][1] + " Sec";
                    Dataset_Param.Tables[tableName].Rows[4][1] = Dataset_Param.Tables[tableName].Rows[4][1] + " Sec";

                    Dataset_Param.Tables[tableName].Rows[27].Delete();
                    Dataset_Param.Tables[tableName].Rows[27].Delete();
                    Dataset_Param.Tables[tableName].Rows[27].Delete();
                    Dataset_Param.Tables[tableName].Rows[27].Delete();
                }
                catch (Exception)
                {
                }
            }
        }
        private void to_fill_CustomerReference(object obj, string tableName)
        {
            int dataset_row_counter = 1;
            Type type = obj.GetType();
            PropertyInfo[] Properties = type.GetProperties();
            foreach (PropertyInfo property in Properties)
            {
                Dataset_Param.Tables[tableName].Rows.Add();
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][0] = property.Name;
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][1] = (property.GetValue(obj, null) == null ? string.Empty : property.GetValue(obj, null).ToString());

                dataset_row_counter++;
            }
            if (dataset_row_counter != 1)
            {
                for (int i = 0; i < Dataset_Param.Tables[tableName].Rows.Count; i++)
                    Dataset_Param.Tables[tableName].Rows[i][0] =
                        Dataset_Param.Tables[tableName].Rows[i][0].ToString().Replace('_', ' ');

                Dataset_Param.Tables[tableName].Rows[1].Delete();
            }
        }
        private void to_fill_DecimalPoints(object obj, string tableName)
        {
            int dataset_row_counter = 1;
            Type type = obj.GetType();
            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo field in fields)
            {
                Dataset_Param.Tables[tableName].Rows.Add();
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][0] = field.Name;
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][1] = field.GetValue(obj).ToString();

                dataset_row_counter++;
            }
            if (dataset_row_counter != 1)
            {
                for (int i = 0; i < Dataset_Param.Tables[tableName].Rows.Count; i++)
                {
                    Dataset_Param.Tables[tableName].Rows[i][0] =
                        Dataset_Param.Tables[tableName].Rows[i][0].ToString().Replace('_', ' ');

                    Dataset_Param.Tables[tableName].Rows[i][1] =
                        LocalCommon.DecimalPoint_toGUI(
                            Convert.ToByte(Dataset_Param.Tables[tableName].Rows[i][1].ToString()));
                }
            }
        }
        private void to_fill_EnergyParameters(object obj, string tableName)
        {
            int dataset_row_counter = 1;
            Type type = obj.GetType();
            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo field in fields)
            {
                Dataset_Param.Tables[tableName].Rows.Add();
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][0] = field.Name;
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][1] = field.GetValue(obj).ToString();

                dataset_row_counter++;
            }
            if (dataset_row_counter != 1)
                Dataset_Param.Tables[tableName].Rows[4].Delete();
        }
        private void to_fill_Limits(object obj, string tableName)
        {
            int dataset_row_counter = 1;
            Type type = obj.GetType();
            var fields = type.GetProperties();
            foreach (var field in fields)
            {
                Dataset_Param.Tables[tableName].Rows.Add();
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][0] = field.Name;
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][1] = field.GetValue(obj, null).ToString();

                dataset_row_counter++;
            }
            if (dataset_row_counter != 1)
            {
                Dataset_Param.Tables[tableName].Rows[0][1] += " V";
                Dataset_Param.Tables[tableName].Rows[1][1] += " V";
                Dataset_Param.Tables[tableName].Rows[2][1] += " V";
                Dataset_Param.Tables[tableName].Rows[8][1] += " V";
                Dataset_Param.Tables[tableName].Rows[3][1] += " Amp";
                Dataset_Param.Tables[tableName].Rows[6][1] += " Amp";
                Dataset_Param.Tables[tableName].Rows[7][1] += " Amp";
                Dataset_Param.Tables[tableName].Rows[9][1] += " Amp";
                Dataset_Param.Tables[tableName].Rows[10][1] += " Amp";
                Dataset_Param.Tables[tableName].Rows[11][1] += " Amp";
                Dataset_Param.Tables[tableName].Rows[12][1] += " Amp";
                Dataset_Param.Tables[tableName].Rows[4][1] += " kW";
                Dataset_Param.Tables[tableName].Rows[5][1] += " kW";
                for (int i = 13; i <= 24; i++)
                    Dataset_Param.Tables[tableName].Rows[i][1] += " kW";

                for (int i = 0; i < Dataset_Param.Tables[tableName].Rows.Count; i++)
                {
                    Dataset_Param.Tables[tableName].Rows[i][0] =
                        Dataset_Param.Tables[tableName].Rows[i][0].ToString().Replace('_', ' ');

                    if (Dataset_Param.Tables[tableName].Rows[i][1].ToString().Contains("kW"))
                        Dataset_Param.Tables[tableName].Rows[i][1] = (Convert.ToDouble(
                            Dataset_Param.Tables[tableName].Rows[i][1].ToString().Split(' ')[0]) / 1000).
                                                                         ToString() + " kW";
                }

                //Delete Unnecessary Rows
                for (int i = 0; i < Dataset_Param.Tables[tableName].Rows.Count - 2;)
                    Dataset_Param.Tables[tableName].Rows[2].Delete();


                //Dataset_Param.Tables[tableName].Rows[29][0] = "MDI Exceed T1";
                //Dataset_Param.Tables[tableName].Rows[30][0] = "MDI Exceed T2";
                //Dataset_Param.Tables[tableName].Rows[31][0] = "MDI Exceed T3";
                //Dataset_Param.Tables[tableName].Rows[32][0] = "MDI Exceed T4";

                ///***Commented_CodeSection
                //Dataset_Param.Tables[tableName].Rows[29][1] = ((double)(Param_Limit_Demand_OverLoad_T1.Threshold / 1000)).ToString() + " kW";
                //Dataset_Param.Tables[tableName].Rows[30][1] = ((double)(Param_Limit_Demand_OverLoad_T2.Threshold / 1000)).ToString() + " kW";
                //Dataset_Param.Tables[tableName].Rows[31][1] = ((double)(Param_Limit_Demand_OverLoad_T3.Threshold / 1000)).ToString() + " kW";
                //Dataset_Param.Tables[tableName].Rows[32][1] = ((double)(Param_Limit_Demand_OverLoad_T4.Threshold / 1000)).ToString() + " kW";

            }
        }
        private void to_fill_MDIParameters(object obj, string tableName)
        {
            int dataset_row_counter = 1;
            Type type = obj.GetType();
            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo field in fields)
            {
                Dataset_Param.Tables[tableName].Rows.Add();
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][0] = field.Name;
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][1] = field.GetValue(obj).ToString();

                dataset_row_counter++;
            }
            if (dataset_row_counter != 1)
            {
                for (int i = 0; i < Dataset_Param.Tables[tableName].Rows.Count; i++)
                    Dataset_Param.Tables[tableName].Rows[i][0] =
                        Dataset_Param.Tables[tableName].Rows[i][0].ToString().Replace('_', ' ');

                if (Dataset_Param.Tables[tableName].Rows[1][1].ToString() == "0")
                    Dataset_Param.Tables[tableName].Rows[0][1] += " Sec";
                else if (Dataset_Param.Tables[tableName].Rows[1][1].ToString() == "1")
                    Dataset_Param.Tables[tableName].Rows[0][1] += " Min";
                else if (Dataset_Param.Tables[tableName].Rows[1][1].ToString() == "2")
                    Dataset_Param.Tables[tableName].Rows[0][1] += " Hrs";
                else if (Dataset_Param.Tables[tableName].Rows[1][1].ToString() == "3")
                    Dataset_Param.Tables[tableName].Rows[0][1] += " Day";

                Dataset_Param.Tables[tableName].Rows[2][1] += " Min";
                Dataset_Param.Tables[tableName].Rows[4][1] =
                    Dataset_Param.Tables[tableName].Rows[4][1].ToString().Split('/')[2];

                if (Dataset_Param.Tables[tableName].Rows[4][1].ToString() == "FE")
                    Dataset_Param.Tables[tableName].Rows[4][1] = "Last Day";
                else if (Dataset_Param.Tables[tableName].Rows[4][1].ToString() == "FD")
                    Dataset_Param.Tables[tableName].Rows[4][1] = "Second Last Day";


                Dataset_Param.Tables[tableName].Rows[0].Delete();
                Dataset_Param.Tables[tableName].Rows[0].Delete();
                Dataset_Param.Tables[tableName].Rows[1].Delete();
                Dataset_Param.Tables[tableName].Rows[2].Delete();
                Dataset_Param.Tables[tableName].Rows[2].Delete();
                Dataset_Param.Tables[tableName].Rows[2].Delete();
                Dataset_Param.Tables[tableName].Rows[2].Delete();
                Dataset_Param.Tables[tableName].Rows[2].Delete();
                Dataset_Param.Tables[tableName].Rows[2].Delete();
                Dataset_Param.Tables[tableName].Rows[2].Delete();
                Dataset_Param.Tables[tableName].Rows[2].Delete();
                //Commented for Test
                //Dataset_Param.Tables[tableName].Rows[1].Delete();
                //Dataset_Param.Tables[tableName].Rows[7].Delete();
                //Dataset_Param.Tables[tableName].Rows[8].Delete();
                //Dataset_Param.Tables[tableName].Rows[8].Delete();
                //Dataset_Param.Tables[tableName].Rows[8].Delete();


            }
        }
        //private void to_fill_MDIParameters(object obj, string tableName)
        //{
        //    int dataset_row_counter = 1;
        //    Type type = obj.GetType();
        //    FieldInfo[] fields = type.GetFields();
        //    foreach (FieldInfo field in fields)
        //    {

        //        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //        bool AnyReadOrWrite = false;
        //        bool isAnyFlagchecked = false;
        //        try
        //        {
        //            if ((Rights.Find(x => x.Read == true || x.Write == true) != null) && field.Name==)
        //            {

        //                foreach (var item in Rights)
        //                {
        //                    /////////////////////////////
        //                    bool read = item.Read, write = item.Write;
        //                    switch ((MDIParameters)Enum.Parse(item.QuantityType, item.QuantityName))
        //                    {
        //                        case MDIParameters.MinimumTimeIntervalBetweenManualReset:
        //                            txt_MDIParams_minTime.Enabled = write;
        //                            radio_MDI_d.Enabled = write;
        //                            radio_MDI_h.Enabled = write;
        //                            radio_MDI_min.Enabled = write;
        //                            radio_MDI_s.Enabled = write;
        //                            pnl_Manual_Reset.Visible = read;
        //                            break;

        //                        case MDIParameters.ManualResetByButtonflag:
        //                            check_MDIParams_ManualResetByButton.Enabled = write;
        //                            check_MDIParams_ManualResetByButton.Visible = read;
        //                            break;
        //                        case MDIParameters.ManualResetByRemoteCommandflag:
        //                            check_MDIParams_ManualResetByRemote.Enabled = write;
        //                            check_MDIParams_ManualResetByRemote.Visible = read;
        //                            break;
        //                        case MDIParameters.ManualResetinPowerDownModeflag:
        //                            check_MDIParams_Manual_Reset_powerDown_Mode.Enabled = write;
        //                            check_MDIParams_Manual_Reset_powerDown_Mode.Visible = read;
        //                            break;
        //                        case MDIParameters.DisableAutoResetinPowerDownModeflag:
        //                            check_MDIParams_Auto_Reset_powerDown_Mode.Enabled = write;
        //                            check_MDIParams_Auto_Reset_powerDown_Mode.Visible = read;
        //                            break;
        //                        case MDIParameters.AutoResetEnableflag:
        //                            check_MDIParams_Autoreset.Enabled = write;
        //                            check_MDIParams_Autoreset.Visible = read;
        //                            break;

        //                        case MDIParameters.MDIAutoResetDataTime:
        //                            pnl_AutoResetEnable.Enabled = write;
        //                            pnl_AutoResetEnable.Visible = read;

        //                            ucMDIAutoResetDateParam.Enabled = write;
        //                            ucMDIAutoResetDateParam.Visible = read;
        //                            break;

        //                        case MDIParameters.MDInterval:
        //                            combo_MDIParams_MDIInterval.Enabled = write;
        //                            lbl_MDIInterval.Visible = lblMDIInterval.Visible = combo_MDIParams_MDIInterval.Visible = read;
        //                            break;

        //                        case MDIParameters.MDISlidesCount:
        //                            tb_MDIParams_SlidesCount.Enabled = write;
        //                            pnl_SlideCount_Container.Visible = tb_MDIParams_SlidesCount.Visible = read;
        //                            break;

        //                        default:
        //                            break;
        //                    }

        //                    ///////////////////////////
        //                    _HelperAccessRights((MDIParameters)Enum.Parse(item.QuantityType, item.QuantityName), item.Read, item.Write);
        //                    if (!AnyReadOrWrite)
        //                        AnyReadOrWrite = (item.Read || item.Write);
        //                    if (!isAnyFlagchecked && !String.IsNullOrEmpty(item.QuantityName) && item.QuantityName.EndsWith("flag", StringComparison.OrdinalIgnoreCase))
        //                    {
        //                        isAnyFlagchecked = (item.Read || item.Write);
        //                    }
        //                }
        //                if (!AnyReadOrWrite)
        //                    return AnyReadOrWrite;
        //                #region Make Manual_Reset GP_Invisible here

        //                var Right_IntervalManualReset = Rights.Find((x) => x.QuantityType == typeof(MDIParameters) &&
        //                                    String.Equals(x.QuantityName, MDIParameters.MinimumTimeIntervalBetweenManualReset.ToString()));
        //                if (Right_IntervalManualReset != null &&
        //                   !(Right_IntervalManualReset.Read || Right_IntervalManualReset.Write) && !isAnyFlagchecked)
        //                {
        //                    this.gpMDIManualReset.Visible = false;
        //                    this.gpMDIAutoReset.Margin = this.gpMDIManualReset.Margin;
        //                }
        //                else
        //                {
        //                    this.gpMDIManualReset.Visible = true;
        //                }

        //                #endregion
        //                #region Make Auto Reset GP_Invisible here

        //                bool Auto_reset = false;
        //                foreach (var item in Rights)
        //                {
        //                    if (item == Right_IntervalManualReset || item.QuantityName.EndsWith("flag", StringComparison.OrdinalIgnoreCase))
        //                        continue;
        //                    else
        //                    {
        //                        if (!Auto_reset)
        //                            Auto_reset = (item.Read || item.Write);
        //                    }
        //                }
        //                if (!Auto_reset)
        //                    gpMDIAutoReset.Visible = false;
        //                else
        //                    gpMDIAutoReset.Visible = true;

        //                #endregion
        //                return AnyReadOrWrite;
        //            }
        //            return false;
        //        }
        //        finally
        //        {
        //            this.ResumeLayout();
        //        }



        //        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //        Dataset_Param.Tables[tableName].Rows.Add();
        //        Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][0] = field.Name;
        //        Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][1] = field.GetValue(obj).ToString();

        //        dataset_row_counter++;
        //    }
        //    if (dataset_row_counter != 1)
        //    {
        //        for (int i = 0; i < Dataset_Param.Tables[tableName].Rows.Count; i++)
        //            Dataset_Param.Tables[tableName].Rows[i][0] =
        //                Dataset_Param.Tables[tableName].Rows[i][0].ToString().Replace('_', ' ');

        //        if (Dataset_Param.Tables[tableName].Rows[1][1].ToString() == "0")
        //            Dataset_Param.Tables[tableName].Rows[0][1] += " Sec";
        //        else if (Dataset_Param.Tables[tableName].Rows[1][1].ToString() == "1")
        //            Dataset_Param.Tables[tableName].Rows[0][1] += " Min";
        //        else if (Dataset_Param.Tables[tableName].Rows[1][1].ToString() == "2")
        //            Dataset_Param.Tables[tableName].Rows[0][1] += " Hrs";
        //        else if (Dataset_Param.Tables[tableName].Rows[1][1].ToString() == "3")
        //            Dataset_Param.Tables[tableName].Rows[0][1] += " Day";

        //        Dataset_Param.Tables[tableName].Rows[2][1] += " Min";
        //        Dataset_Param.Tables[tableName].Rows[4][1] =
        //            Dataset_Param.Tables[tableName].Rows[4][1].ToString().Split('/')[2];

        //        if (Dataset_Param.Tables[tableName].Rows[4][1].ToString() == "FE")
        //            Dataset_Param.Tables[tableName].Rows[4][1] = "Last Day";
        //        else if (Dataset_Param.Tables[tableName].Rows[4][1].ToString() == "FD")
        //            Dataset_Param.Tables[tableName].Rows[4][1] = "Second Last Day";

        //        //Commented for Test
        //        //Dataset_Param.Tables[tableName].Rows[1].Delete();
        //        //Dataset_Param.Tables[tableName].Rows[7].Delete();
        //        //Dataset_Param.Tables[tableName].Rows[8].Delete();
        //        //Dataset_Param.Tables[tableName].Rows[8].Delete();
        //        //Dataset_Param.Tables[tableName].Rows[8].Delete();


        //    }
        //}
        private void to_fill_MonitoringTimes(object obj, string tableName)
        {
            int dataset_row_counter = 1;
            Type type = obj.GetType();
            FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (FieldInfo field in fields)
            {
                Dataset_Param.Tables[tableName].Rows.Add();
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][0] = field.Name;
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][1] = field.GetValue(obj).ToString();

                dataset_row_counter++;
            }
            if (dataset_row_counter != 1)
            {
                for (int i = 0; i < Dataset_Param.Tables[tableName].Rows.Count; i++)
                {
                    //MessageBox.Show(Dataset_Param.Tables[tableName].Rows[i][1].ToString());

                    if (Dataset_Param.Tables[tableName].Rows[i][1].ToString().Contains(":"))
                        ;
                    else
                    {
                        Dataset_Param.Tables[tableName].Rows[i].Delete();
                        i--;
                    }
                }

                for (int i = 0; i < Dataset_Param.Tables[tableName].Rows.Count; i++)
                {
                    Dataset_Param.Tables[tableName].Rows[i][0] =
                        Dataset_Param.Tables[tableName].Rows[i][0].ToString().Replace('_', ' ');

                    //MessageBox.Show(Dataset_Param.Tables[tableName].Rows[i][1].ToString());

                    Dataset_Param.Tables[tableName].Rows[i][1] =
                        Dataset_Param.Tables[tableName].Rows[i][1].ToString().Split(' ')[1];

                    Dataset_Param.Tables[tableName].Rows[i][1] =
                        Dataset_Param.Tables[tableName].Rows[i][1].ToString().Split(':')[0] + " Min  " +
                        Dataset_Param.Tables[tableName].Rows[i][1].ToString().Split(':')[1] + " Sec";
                }
            }
        }
        private void to_fill_IPProfile(object obj1, object obj2, object obj3, object obj4, string tableName)
        {
            int dataset_row_counter1 = 1;
            Type type1 = obj1.GetType();
            FieldInfo[] fields1 = type1.GetFields();
            foreach (FieldInfo field1 in fields1)
            {
                Dataset_Param.Tables[tableName].Rows.Add();
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter1 - 1][0] = field1.Name;
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter1 - 1][1] = field1.GetValue(obj1).ToString();
                dataset_row_counter1++;
            }
            Dataset_Param.Tables[tableName].Rows[1][1] =
                DLMS_Common.LongToIPAddressString(Convert.ToUInt32(Dataset_Param.Tables[tableName].Rows[1][1]));

            int dataset_row_counter2 = 1;
            Type type2 = obj2.GetType();
            FieldInfo[] fields2 = type2.GetFields();
            foreach (FieldInfo field2 in fields2)
            {
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter2 - 1][2] = field2.GetValue(obj2).ToString();
                if (dataset_row_counter2 == 1 && Dataset_Param.Tables[tableName].Rows[0][2].ToString() == "0")
                {
                    Dataset_Param.Tables[tableName].Rows[dataset_row_counter2 - 1][2] = "";
                    goto ex;
                }
                dataset_row_counter2++;
            }
            Dataset_Param.Tables[tableName].Rows[1][2] =
                DLMS_Common.LongToIPAddressString(Convert.ToUInt32(Dataset_Param.Tables[tableName].Rows[1][2]));

            int dataset_row_counter3 = 1;
            Type type3 = obj3.GetType();
            FieldInfo[] fields3 = type3.GetFields();
            foreach (FieldInfo field3 in fields3)
            {
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter3 - 1][3] = field3.GetValue(obj3).ToString();
                if (dataset_row_counter3 == 1 && Dataset_Param.Tables[tableName].Rows[0][3].ToString() == "0")
                {
                    Dataset_Param.Tables[tableName].Rows[dataset_row_counter3 - 1][3] = "";
                    goto ex;
                }
                dataset_row_counter3++;
            }
            Dataset_Param.Tables[tableName].Rows[1][3] =
                DLMS_Common.LongToIPAddressString(Convert.ToUInt32(Dataset_Param.Tables[tableName].Rows[1][3]));

            int dataset_row_counter4 = 1;
            Type type4 = obj4.GetType();
            FieldInfo[] fields4 = type4.GetFields();
            foreach (FieldInfo field4 in fields4)
            {
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter4 - 1][4] = field4.GetValue(obj4).ToString();
                if (dataset_row_counter4 == 1 && Dataset_Param.Tables[tableName].Rows[0][4].ToString() == "0")
                {
                    Dataset_Param.Tables[tableName].Rows[dataset_row_counter4 - 1][4] = "";
                    goto ex;
                }
                dataset_row_counter4++;
            }
            Dataset_Param.Tables[tableName].Rows[1][4] =
                DLMS_Common.LongToIPAddressString(Convert.ToUInt32(Dataset_Param.Tables[tableName].Rows[1][4]));

            ex:
            if (dataset_row_counter1 != 1)
            {
                for (int i = 0; i < Dataset_Param.Tables[tableName].Rows.Count; i++)
                    Dataset_Param.Tables[tableName].Rows[i][0] =
                        Dataset_Param.Tables[tableName].Rows[i][0].ToString().Replace('_', ' ');
            }

            Dataset_Param.Tables[tableName].Rows[0].Delete();
            for (int i = 0; i < 8; i++)
                Dataset_Param.Tables[tableName].Rows[5].Delete();
        }
        private void to_fill_WakeupProfile(object obj1, object obj2, object obj3, object obj4, string tableName)
        {
            int dataset_row_counter1 = 1;
            Type type1 = obj1.GetType();
            FieldInfo[] fields1 = type1.GetFields();
            foreach (FieldInfo field1 in fields1)
            {
                Dataset_Param.Tables[tableName].Rows.Add();
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter1 - 1][0] = field1.Name;
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter1 - 1][1] = field1.GetValue(obj1).ToString();
                dataset_row_counter1++;
            }

            int dataset_row_counter2 = 1;
            Type type2 = obj2.GetType();
            FieldInfo[] fields2 = type2.GetFields();
            foreach (FieldInfo field2 in fields2)
            {
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter2 - 1][2] = field2.GetValue(obj2).ToString();
                if (dataset_row_counter2 == 1 && Dataset_Param.Tables[tableName].Rows[0][2].ToString() == "0")
                {
                    Dataset_Param.Tables[tableName].Rows[dataset_row_counter2 - 1][2] = "";
                    goto ex;
                }
                dataset_row_counter2++;
            }

            int dataset_row_counter3 = 1;
            Type type3 = obj3.GetType();
            FieldInfo[] fields3 = type3.GetFields();
            foreach (FieldInfo field3 in fields3)
            {
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter3 - 1][3] = field3.GetValue(obj3).ToString();
                if (dataset_row_counter3 == 1 && Dataset_Param.Tables[tableName].Rows[0][3].ToString() == "0")
                {
                    Dataset_Param.Tables[tableName].Rows[dataset_row_counter3 - 1][3] = "";
                    goto ex;
                }
                dataset_row_counter3++;
            }

            int dataset_row_counter4 = 1;
            Type type4 = obj4.GetType();
            FieldInfo[] fields4 = type4.GetFields();
            foreach (FieldInfo field4 in fields4)
            {
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter4 - 1][4] = field4.GetValue(obj4).ToString();
                if (dataset_row_counter4 == 1 && Dataset_Param.Tables[tableName].Rows[0][4].ToString() == "0")
                {
                    Dataset_Param.Tables[tableName].Rows[dataset_row_counter4 - 1][4] = "";
                    goto ex;
                }
                dataset_row_counter4++;
            }

            ex:
            if (dataset_row_counter1 != 1)
            {
                for (int i = 0; i < Dataset_Param.Tables[tableName].Rows.Count; i++)
                    Dataset_Param.Tables[tableName].Rows[i][0] =
                        Dataset_Param.Tables[tableName].Rows[i][0].ToString().Replace('_', ' ');
            }

            for (int i = 1; i < 5; i++)
                for (int j = 1; j < 5; j++)
                    if (Dataset_Param.Tables[tableName].Rows[i][j].ToString() == "0")
                        Dataset_Param.Tables[tableName].Rows[i][j] = "None";

            Dataset_Param.Tables[tableName].Rows[0].Delete();
            for (int i = 0; i < 16; i++)
                Dataset_Param.Tables[tableName].Rows[4].Delete();

        }
        private void to_fill_NumberProfile(object obj1, object obj2, object obj3, object obj4, object obj5, string tableName)
        {
            int dataset_row_counter1 = 1;
            Type type1 = obj1.GetType();
            FieldInfo[] fields1 = type1.GetFields();
            foreach (FieldInfo field1 in fields1)
            {
                Dataset_Param.Tables[tableName].Rows.Add();
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter1 - 1][0] = field1.Name;
                if (dataset_row_counter1 == 2 || dataset_row_counter1 == 3)
                    Dataset_Param.Tables[tableName].Rows[dataset_row_counter1 - 1][1] =
                        LocalCommon.ConvertToValidString(field1.GetValue(obj1) as byte[]);
                else
                    Dataset_Param.Tables[tableName].Rows[dataset_row_counter1 - 1][1] = field1.GetValue(obj1).ToString();
                dataset_row_counter1++;
            }

            int dataset_row_counter2 = 1;
            Type type2 = obj2.GetType();
            FieldInfo[] fields2 = type2.GetFields();
            foreach (FieldInfo field2 in fields2)
            {
                if (dataset_row_counter2 == 2 || dataset_row_counter2 == 3)
                    Dataset_Param.Tables[tableName].Rows[dataset_row_counter2 - 1][2] =
                        LocalCommon.ConvertToValidString(field2.GetValue(obj2) as byte[]);
                else
                    Dataset_Param.Tables[tableName].Rows[dataset_row_counter2 - 1][2] = field2.GetValue(obj2).ToString();
                if (dataset_row_counter2 == 1 && Dataset_Param.Tables[tableName].Rows[0][2].ToString() == "0")
                {
                    Dataset_Param.Tables[tableName].Rows[dataset_row_counter2 - 1][2] = "";
                    goto ex;
                }
                dataset_row_counter2++;
            }

            int dataset_row_counter3 = 1;
            Type type3 = obj3.GetType();
            FieldInfo[] fields3 = type3.GetFields();
            foreach (FieldInfo field3 in fields3)
            {
                if (dataset_row_counter3 == 2 || dataset_row_counter3 == 3)
                    Dataset_Param.Tables[tableName].Rows[dataset_row_counter3 - 1][3] =
                        LocalCommon.ConvertToValidString(field3.GetValue(obj3) as byte[]);
                else
                    Dataset_Param.Tables[tableName].Rows[dataset_row_counter3 - 1][3] = field3.GetValue(obj3).ToString();
                if (dataset_row_counter3 == 1 && Dataset_Param.Tables[tableName].Rows[0][3].ToString() == "0")
                {
                    Dataset_Param.Tables[tableName].Rows[dataset_row_counter3 - 1][3] = "";
                    goto ex;
                }
                dataset_row_counter3++;
            }

            int dataset_row_counter4 = 1;
            Type type4 = obj4.GetType();
            FieldInfo[] fields4 = type4.GetFields();
            foreach (FieldInfo field4 in fields4)
            {
                if (dataset_row_counter4 == 2 || dataset_row_counter4 == 3)
                    Dataset_Param.Tables[tableName].Rows[dataset_row_counter4 - 1][4] =
                        LocalCommon.ConvertToValidString(field4.GetValue(obj4) as byte[]);
                else
                    Dataset_Param.Tables[tableName].Rows[dataset_row_counter4 - 1][4] = field4.GetValue(obj4).ToString();
                if (dataset_row_counter4 == 1 && Dataset_Param.Tables[tableName].Rows[0][4].ToString() == "0")
                {
                    Dataset_Param.Tables[tableName].Rows[dataset_row_counter4 - 1][4] = "";
                    goto ex;
                }
                dataset_row_counter4++;
            }

            ex:
            int dataset_row_counter5 = 1;
            Type type5 = obj5.GetType();
            FieldInfo[] fields5 = type5.GetFields();
            foreach (FieldInfo field5 in fields5)
            {
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter5 - 1][5] = field5.GetValue(obj5).ToString();
                dataset_row_counter5++;
            }


            if (dataset_row_counter1 != 1)
            {
                for (int i = 0; i < Dataset_Param.Tables[tableName].Rows.Count; i++)
                    Dataset_Param.Tables[tableName].Rows[i][0] =
                        Dataset_Param.Tables[tableName].Rows[i][0].ToString().Replace('_', ' ');
            }

            Dataset_Param.Tables[tableName].Rows[0].Delete();
            Dataset_Param.Tables[tableName].Rows[3].Delete();
            Dataset_Param.Tables[tableName].Rows[8].Delete();
            Dataset_Param.Tables[tableName].Rows[10].Delete();
            Dataset_Param.Tables[tableName].Rows[0][5] = "";
            Dataset_Param.Tables[tableName].Rows[1][5] = "";
        }
        private void to_fill_KeepAlive(object obj, string tableName)
        {
            int dataset_row_counter = 1;
            Type type = obj.GetType();
            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo field in fields)
            {
                Dataset_Param.Tables[tableName].Rows.Add();
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][0] = field.Name;
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][1] = field.GetValue(obj).ToString();

                dataset_row_counter++;
            }
            if (dataset_row_counter != 1)
            {
                for (int i = 0; i < Dataset_Param.Tables[tableName].Rows.Count; i++)
                    Dataset_Param.Tables[tableName].Rows[i][0] =
                        Dataset_Param.Tables[tableName].Rows[i][0].ToString().Replace('_', ' ');
            }

            if (Dataset_Param.Tables[tableName].Rows[0][1].ToString() == "True" || Convert.ToInt32(Dataset_Param.Tables[tableName].Rows[1][1].ToString()) > 0)
                Dataset_Param.Tables[tableName].Rows[0][1] = "True";
            else
            {
                Dataset_Param.Tables[tableName].Rows[0][1] = "False";
                Dataset_Param.Tables[tableName].Rows[1].Delete();
                Dataset_Param.Tables[tableName].Rows[1].Delete();
            }



        }
        private void to_fill_ModemLimitsTimes(object obj, string tableName)
        {
            int dataset_row_counter = 1;
            Type type = obj.GetType();
            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo field in fields)
            {
                Dataset_Param.Tables[tableName].Rows.Add();
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][0] = field.Name;
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][1] = field.GetValue(obj).ToString();

                dataset_row_counter++;
            }
            if (dataset_row_counter != 1)
            {
                for (int i = 0; i < Dataset_Param.Tables[tableName].Rows.Count; i++)
                    Dataset_Param.Tables[tableName].Rows[i][0] =
                        Dataset_Param.Tables[tableName].Rows[i][0].ToString().Replace('_', ' ');
            }
            for (int i = 0; i < Dataset_Param.Tables[tableName].Rows.Count; i++)
                if (Dataset_Param.Tables[tableName].Rows[i][0].ToString().Substring(0, 1) == "T")
                    Dataset_Param.Tables[tableName].Rows[i][1] += " Sec";
        }
        private void to_fill_ModemInitialize(object obj1, object obj2, string tableName)
        {
            int dataset_row_counter = 1;
            Type type = obj1.GetType();
            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo field in fields)
            {
                Dataset_Param.Tables[tableName].Rows.Add();
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][0] = field.Name;
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][1] = field.GetValue(obj1).ToString();

                dataset_row_counter++;
            }

            type = obj2.GetType();
            fields = type.GetFields();
            foreach (FieldInfo field in fields)
            {
                Dataset_Param.Tables[tableName].Rows.Add();
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][0] = field.Name;
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][1] = field.GetValue(obj2).ToString();

                dataset_row_counter++;
            }

            if (dataset_row_counter != 1)
            {
                for (int i = 0; i < Dataset_Param.Tables[tableName].Rows.Count; i++)
                    Dataset_Param.Tables[tableName].Rows[i][0] =
                        Dataset_Param.Tables[tableName].Rows[i][0].ToString().Replace('_', ' ');
            }
            Dataset_Param.Tables[tableName].Rows[0].Delete();
            Dataset_Param.Tables[tableName].Rows[0].Delete();

            for (int i = 5; i <= 7; i++)
                if (Dataset_Param.Tables[tableName].Rows[i][1].ToString() == "0")
                    Dataset_Param.Tables[tableName].Rows[i][1] = "False";
                else
                    Dataset_Param.Tables[tableName].Rows[i][1] = "True";
        }
        private void to_fill_CommunicationProfile(object obj, byte[] num, string tableName)
        {
            int dataset_row_counter = 1;
            Type type = obj.GetType();
            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo field in fields)
            {
                Dataset_Param.Tables[tableName].Rows.Add();
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][0] = field.Name;
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][1] = field.GetValue(obj).ToString();

                dataset_row_counter++;
            }
            if (dataset_row_counter != 1)
            {
                for (int i = 0; i < Dataset_Param.Tables[tableName].Rows.Count; i++)
                    Dataset_Param.Tables[tableName].Rows[i][0] =
                        Dataset_Param.Tables[tableName].Rows[i][0].ToString().Replace('_', ' ');
            }
            Dataset_Param.Tables[tableName].Rows[2].Delete();
            Dataset_Param.Tables[tableName].Rows[2].Delete();
            Dataset_Param.Tables[tableName].Rows[2].Delete();

            if (Dataset_Param.Tables[tableName].Rows[0][1].ToString() == "0")
            {
                Dataset_Param.Tables[tableName].Rows[0][1] = "SMS";
                Dataset_Param.Tables[tableName].Rows[1].Delete();
                for (int i = 0; i < 4; i++)
                {
                    Dataset_Param.Tables[tableName].Rows.Add();
                    Dataset_Param.Tables[tableName].Rows[i + 1][0] = "Number Profile ID " +
                                                                                       (i + 1).ToString();
                    if (num[i] != 0)
                        Dataset_Param.Tables[tableName].Rows[i + 1][1] = num[i];
                    else
                        Dataset_Param.Tables[tableName].Rows[i + 1][1] = "None";
                }
            }
            else if (Dataset_Param.Tables[tableName].Rows[0][1].ToString() == "1")
            {
                Dataset_Param.Tables[tableName].Rows[0][1] = "TCP Link";
            }
            else if (Dataset_Param.Tables[tableName].Rows[0][1].ToString() == "2")
            {
                Dataset_Param.Tables[tableName].Rows[0][1] = "No Event Notification";
                Dataset_Param.Tables[tableName].Rows[1].Delete();
            }

            //Dataset_Param.Tables[tableName].Rows[3][1] = (Dataset_Param.Tables[tableName].Rows[3][1].ToString() ==
            //                                              "False")
            //                                                 ? "HDLC"
            //                                                 : "TCP";
            //Dataset_Param.Tables[tableName].Rows[4][1] = (Dataset_Param.Tables[tableName].Rows[4][1].ToString() ==
            //                                              "False")
            //                                                 ? "TCP"
            //                                                 : "UDP";
        }
        private void to_fill_DisplayWindows(DisplayWindows obj, string tableName)
        {
            int dataset_row_counter = 1;  //1;

            for (int i = 0; i < obj.Windows.Count; i++)
            {
                Dataset_Param.Tables[tableName].Rows.Add();
                Dataset_Param.Tables[tableName].Rows[i][0] = obj.Windows[i].WindowNumberToDisplay;
                Dataset_Param.Tables[tableName].Rows[i][1] = obj.Windows[i].Window_Name;

                dataset_row_counter++;
            }

            //Display Window Scroll Time added by M.Azeem Inayat
            if (obj.WindowsMode == DispalyWindowsModes.Normal && Dataset_Param.Tables[tableName].Rows.Count > 0)
            {
                Dataset_Param.Tables[tableName].Rows.Add();
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][0] = "Display Scroll Time";
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][1] = obj.ScrollTime.TotalSeconds + " (seconds)";
            }

            if (dataset_row_counter != 1)
            {
                for (int i = 0; i < Dataset_Param.Tables[tableName].Rows.Count; i++)
                    Dataset_Param.Tables[tableName].Rows[i][0] =
                        Dataset_Param.Tables[tableName].Rows[i][0].ToString().Replace('_', ' ');
            }
        }
        private void to_fill_Calender(object obj, string tableName)
        {
            Dataset_Param.Tables[tableName].Rows.Add();
            Dataset_Param.Tables[tableName].Rows[0][0] = "Calendar Name";
            Dataset_Param.Tables[tableName].Rows[0][1] = Calendar.CalendarName;

            Dataset_Param.Tables[tableName].Rows.Add();
            Dataset_Param.Tables[tableName].Rows[1][0] = "Start Date";
            ///***modified
            if (true)///rdbInvokeAction.Checked)
                Dataset_Param.Tables[tableName].Rows[1][1] = "Immediately";
            else
            {
                Dataset_Param.Tables[tableName].Rows[1][1] = Calendar.CalendarstartDate.GetDate();
                if (Calendar.CalendarstartDate.IsDateTimeConvertible)
                {
                    Dataset_Param.Tables[tableName].Rows[1][1] = Calendar.CalendarstartDate.GetDateTime();
                }
                else if (Calendar.CalendarstartDate.IsDateConvertible)
                {
                    Dataset_Param.Tables[tableName].Rows[1][1] = Calendar.CalendarstartDate.GetDate();
                }
                else
                {
                    Dataset_Param.Tables[tableName].Rows[1][1] = null;
                }
            }
            dataset_calendar.table_season.Clear();
            dataset_calendar.table_week.Clear();
            dataset_calendar.table_specialDays.Clear();
            dataset_calendar.table_day.Clear();
            //season profile
            foreach (var item in Calendar.ParamSeasonProfile.seasonProfile_Table)
            {
                dataset_calendar.table_season.Rows.Add();
                dataset_calendar.table_season[dataset_calendar.table_season.Rows.Count - 1].season_profileID = item.Index.ToString();
                dataset_calendar.table_season[dataset_calendar.table_season.Rows.Count - 1].profileName = item.Profile_Name_Str;
                dataset_calendar.table_season[dataset_calendar.table_season.Rows.Count - 1].startDate = String.Format("Month= {0}, Date={1}", item.Start_Date.Month,
                    item.Start_Date.DayOfMonth);
                dataset_calendar.table_season[dataset_calendar.table_season.Rows.Count - 1].weekprofileID = item.Week_Profile.ToString();

            }
            // week profile
            foreach (var item in Calendar.ParamWeekProfile.weekProfile_Table)
            {
                dataset_calendar.table_week.Rows.Add();
                dataset_calendar.table_week[dataset_calendar.table_week.Rows.Count - 1].week_profileID = item.Profile_Name_Str;
                dataset_calendar.table_week[dataset_calendar.table_week.Rows.Count - 1].monday = item.Day_Profile_MON.ToString();
                dataset_calendar.table_week[dataset_calendar.table_week.Rows.Count - 1].tuesday = item.Day_Profile_TUE.ToString();
                dataset_calendar.table_week[dataset_calendar.table_week.Rows.Count - 1].wednesday = item.Day_Profile_WED.ToString();
                dataset_calendar.table_week[dataset_calendar.table_week.Rows.Count - 1].thursday = item.Day_Profile_THRU.ToString();
                dataset_calendar.table_week[dataset_calendar.table_week.Rows.Count - 1].friday = item.Day_Profile_FRI.ToString();
                dataset_calendar.table_week[dataset_calendar.table_week.Rows.Count - 1].saturday = item.Day_Profile_SAT.ToString();
                dataset_calendar.table_week[dataset_calendar.table_week.Rows.Count - 1].sunday = item.Day_Profile_SUN.ToString();

            }
            ///day profile
            foreach (var item in Calendar.ParamDayProfile.dayProfile_Table)
            {
                foreach (var schedule in item.dayProfile_Schedule)
                {
                    dataset_calendar.table_day.Rows.Add();
                    dataset_calendar.table_day[dataset_calendar.table_day.Rows.Count - 1].slotID = schedule.TimeSlotId.ToString();
                    dataset_calendar.table_day[dataset_calendar.table_day.Rows.Count - 1].startTime = schedule.StartTime.ToString();
                    dataset_calendar.table_day[dataset_calendar.table_day.Rows.Count - 1].tariff = schedule.ScriptSelector.ToString();
                    dataset_calendar.table_day[dataset_calendar.table_day.Rows.Count - 1].profileID = item.Day_Profile_ID;

                }
            }
        }
        private void to_fill_Time_Based_Events()
        {
        }

        public void getTimeBasesEventsReportData()
        {
            check_TBE("Time Based Event_1", "T1", TBE1);
            check_TBE("Time Based Event_2", "T2", TBE2);
            Dataset_Events.DataTable_Time_Based_Events.Rows.Add();
            Dataset_Events.DataTable_Time_Based_Events[Dataset_Events.DataTable_Time_Based_Events.Rows.Count - 1].group = "Disable on Power Fail Flags";
            Dataset_Events.DataTable_Time_Based_Events[Dataset_Events.DataTable_Time_Based_Events.Rows.Count - 1].name = "Disable on power fail TBE_1 ";
            Dataset_Events.DataTable_Time_Based_Events[Dataset_Events.DataTable_Time_Based_Events.Rows.Count - 1].value = Convert.ToBoolean(obj_TBE_PowerFail.disableEventAtPowerFail_TBE1).ToString();

            Dataset_Events.DataTable_Time_Based_Events.Rows.Add();
            Dataset_Events.DataTable_Time_Based_Events[Dataset_Events.DataTable_Time_Based_Events.Rows.Count - 1].group = "Disable on Power Fail Flags";
            Dataset_Events.DataTable_Time_Based_Events[Dataset_Events.DataTable_Time_Based_Events.Rows.Count - 1].name = "Disable on power fail TBE_2 ";
            Dataset_Events.DataTable_Time_Based_Events[Dataset_Events.DataTable_Time_Based_Events.Rows.Count - 1].value = Convert.ToBoolean(obj_TBE_PowerFail.disableEventAtPowerFail_TBE2).ToString();

        }

        private void check_TBE(string group, string e, Param_TimeBaseEvents TBE)
        {
            DateTime temp = new DateTime(2010, 1, 1, 0, 0, 0);
            temp = temp.AddSeconds(TBE.Interval);
            if (TBE.Control_Enum.Equals(Param_TimeBaseEvents.Tb_Disable))
            {
                add_TBEs_to_Dataset(group, e, "Disable", "-", "-", "-");
            }
            else if (TBE.Control_Enum.Equals(Param_TimeBaseEvents.Tb_DateTime))
            {
                add_TBEs_to_Dataset(group, e, "Date Time", "-", getTime(TBE), getDate(TBE));
            }
            else if (TBE.Control_Enum.Equals(Param_TimeBaseEvents.Tb_Interval))
            {
                add_TBEs_to_Dataset(group, e, "Interval", TBE.Interval.ToString(), "-", "-");
            }
            else if (TBE.Control_Enum.Equals(Param_TimeBaseEvents.Tb_IntervalTimeSink))
            {
                string min = (TBE.Interval / 256).ToString();
                string sec = (TBE.Interval & 255).ToString();
                add_TBEs_to_Dataset(group, e, "Interval Time Sink", getFormatedValue(min) + " : " + getFormatedValue(sec), "-", "-");

            }
            else if (TBE.Control_Enum.Equals(Param_TimeBaseEvents.Tb_Fixed))
            {
                string min = (TBE.Interval / 256).ToString();
                string sec = (TBE.Interval & 255).ToString();
                add_TBEs_to_Dataset(group, e, "Fixed", getFormatedValue(min) + " : " + getFormatedValue(sec), "-", "-");

            }
            else
            {
            }
            Application.DoEvents();
        }

        public string getDate(Param_TimeBaseEvents TBE)
        {
            string year = "";
            string month = "";
            string day = "";
            try
            {
                if (TBE.DateTime.Year == StDateTime.NullYear)
                {
                    year = "every year";
                }
                else
                {
                    year = TBE.DateTime.Year.ToString();
                }

                if (TBE.DateTime.Month == StDateTime.Null)
                {
                    month = "every month";
                }
                else if (TBE.DateTime.Month == StDateTime.DaylightSavingBegin)
                {
                    month = "DLSB"; // Day light saving begin
                }
                else if (TBE.DateTime.Month == StDateTime.DaylightSavingEnd)
                {
                    month = "DLSE"; // Day light saving end
                }
                else
                {
                    month = TBE.DateTime.Month.ToString();
                }

                if (TBE.DateTime.DayOfMonth == StDateTime.Null)
                {
                    day = "every day";
                }
                else if (TBE.DateTime.DayOfMonth == StDateTime.LastDayOfMonth)
                {
                    day = "Last Day";
                }
                else if (TBE.DateTime.DayOfMonth == StDateTime.SecondLastDayOfMonth)
                {
                    day = "2nd Last Day";
                }
                else
                {
                    day = TBE.DateTime.DayOfMonth.ToString();
                }
                return year + "/" + month + "/" + day;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private string getTime(Param_TimeBaseEvents TBE)
        {
            string hour = "";
            string min = "";
            string sec = "";
            try
            {
                if (TBE.DateTime.Hour == 0xff)
                    hour = "every hour";
                else
                    hour = TBE.DateTime.Hour.ToString();
                if (TBE.DateTime.Minute == 0xff)
                    min = "every minute";
                else
                    min = TBE.DateTime.Minute.ToString();
                if (TBE.DateTime.Second == 0xff)
                    sec = "every second";
                else
                    sec = TBE.DateTime.Second.ToString();
                return hour + " : " + min + " : " + sec;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private string getFormatedValue(string val)
        {
            if (Convert.ToInt32(val) <= 9)
            {
                return "0" + val;
            }
            else
            {
                return val;
            }
        }

        private void add_TBEs_to_Dataset(string group, string e, string status, string interval, string time, string date)
        {

            Dataset_Events.DataTable_Time_Based_Events.Rows.Add();
            Dataset_Events.DataTable_Time_Based_Events[Dataset_Events.DataTable_Time_Based_Events.Rows.Count - 1].group = group;
            Dataset_Events.DataTable_Time_Based_Events[Dataset_Events.DataTable_Time_Based_Events.Rows.Count - 1].name = "Time Window " + e;
            Dataset_Events.DataTable_Time_Based_Events[Dataset_Events.DataTable_Time_Based_Events.Rows.Count - 1].value = status;

            Dataset_Events.DataTable_Time_Based_Events.Rows.Add();
            Dataset_Events.DataTable_Time_Based_Events[Dataset_Events.DataTable_Time_Based_Events.Count - 1].group = group;
            Dataset_Events.DataTable_Time_Based_Events[Dataset_Events.DataTable_Time_Based_Events.Count - 1].name = "Interval " + e;
            Dataset_Events.DataTable_Time_Based_Events[Dataset_Events.DataTable_Time_Based_Events.Count - 1].value = interval;//TBE1.Interval;

            Dataset_Events.DataTable_Time_Based_Events.Rows.Add();
            Dataset_Events.DataTable_Time_Based_Events[Dataset_Events.DataTable_Time_Based_Events.Rows.Count - 1].group = group;
            Dataset_Events.DataTable_Time_Based_Events[Dataset_Events.DataTable_Time_Based_Events.Rows.Count - 1].name = "Date of Activation " + e;
            Dataset_Events.DataTable_Time_Based_Events[Dataset_Events.DataTable_Time_Based_Events.Rows.Count - 1].value = date;//T

            Dataset_Events.DataTable_Time_Based_Events.Rows.Add();
            Dataset_Events.DataTable_Time_Based_Events[Dataset_Events.DataTable_Time_Based_Events.Rows.Count - 1].group = group;
            Dataset_Events.DataTable_Time_Based_Events[Dataset_Events.DataTable_Time_Based_Events.Rows.Count - 1].name = "Time of Activation " + e;
            Dataset_Events.DataTable_Time_Based_Events[Dataset_Events.DataTable_Time_Based_Events.Rows.Count - 1].value = time;//T
        }

        private void to_fill_ABC(object obj, string tableName)
        {
            int dataset_row_counter = 1;
            Type type = obj.GetType();
            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo field in fields)
            {
                Dataset_Param.Tables[tableName].Rows.Add();
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][0] = field.Name;
                Dataset_Param.Tables[tableName].Rows[dataset_row_counter - 1][1] = field.GetValue(obj).ToString();

                dataset_row_counter++;
            }
            if (dataset_row_counter != 1)
            {
                for (int i = 0; i < Dataset_Param.Tables[tableName].Rows.Count; i++)
                    Dataset_Param.Tables[tableName].Rows[i][0] =
                        Dataset_Param.Tables[tableName].Rows[i][0].ToString().Replace('_', ' ');
            }
        }

        #endregion

        #region Controls_Load_EventHandlers

        private void ucActivityCalendar_Load(object sender, EventArgs e)
        {
            ///Assign Calendar variable
            if (ucActivityCalendar != null && Calendar != null)
                ucActivityCalendar.Calendar = Calendar;
            ucActivityCalendar.btn_GetActivityCalendar.Click += new EventHandler(btn_GetActivityCalendar_Name_Click);
            ucActivityCalendar.btn_setActivityCalendar.Click += new EventHandler(btn_setActivityCalendar_Name_Click);
            ucActivityCalendar.btnSetDayProfile.Click += new EventHandler(btn_setActivityCalendar_DayProfile_Click);
            ucActivityCalendar.btnGetDayProfile.Click += new EventHandler(btn_GetActivityCalendar_DayProfile_Click);
            ucActivityCalendar.btnSetSeasonProfile.Click += new EventHandler(btn_setActivityCalendar_SeasonProfile_Click);
            ucActivityCalendar.btnGetSeasonProfile.Click += new EventHandler(btn_GetActivityCalendar_SeasonProfile_Click);
            ucActivityCalendar.btnSetWeekProfile.Click += new EventHandler(btn_setActivityCalendar_WeekProfile_Click);
            ucActivityCalendar.btnGetWeekProfile.Click += new EventHandler(btn_GetActivityCalendar_WeekProfile_Click);
            ucActivityCalendar.btnSetSpecialDays.Click += new EventHandler(btn_setActivityCalendar_SpecialDay_Click);
            ucActivityCalendar.btnGetSpecialDays.Click += new EventHandler(btn_GetActivityCalendar_SpecialDay_Click);
        }

        private void ucLimits_Load(object sender, EventArgs e)
        {
            InitializeUcLimitsParameters();
        }

        private void ucMonitoringTime_Load(object sender, EventArgs e)
        {
            ///Assign Param_Monitoring_time_object
            if (ucMonitoringTime != null && Param_Monitoring_Time_Object != null)
                ucMonitoringTime.Param_Monitoring_time_object = Param_Monitoring_Time_Object;

        }

        private void ucMDIParams_Load(object sender, EventArgs e)
        {
            ///Assign Param_MDI_parameters_object
            if (ucMDIParams != null && Param_MDI_Parameters_Object != null)
                ucMDIParams.Param_MDI_parameters_object = Param_MDI_Parameters_Object;

        }

        private void ucLoadProfile_Load(object sender, EventArgs e)
        {
            try
            {
                var _loadProfileChannelsInfo = LoadProfileChannelsInfo;
                if (ucLoadProfile != null && _loadProfileChannelsInfo != null)
                {
                    ucLoadProfile.LoadProfileChannelsInfo = _loadProfileChannelsInfo;
                }
                _loadProfileChannelsInfo = LoadProfileChannelsInfo_2;
                if (ucLoadProfile != null && _loadProfileChannelsInfo != null)
                {
                    ucLoadProfile.LoadProfileChannelsInfo_2 = _loadProfileChannelsInfo;
                }
                if (ucLoadProfile != null && LoadProfile_Controller != null)
                {
                    this.AllSelectableChannels = LoadProfile_Controller.Get_SelectableLoadProfileChannels();
                    ucLoadProfile.Init_LoadProfiles(AllSelectableChannels);
                }

            }
            catch
            {
                throw;
            }
        }

        private void ucLoadProfile_Leave(object sender, System.EventArgs e)
        {
            try
            {
                if (ucLoadProfile != null && ucLoadProfile.LoadProfileChannelsInfo != null)
                {
                    LoadProfileChannelsInfo = ucLoadProfile.LoadProfileChannelsInfo;
                    LoadProfileChannelsInfo_2 = ucLoadProfile.LoadProfileChannelsInfo_2;
                }
            }
            catch
            {
            }
        }

        private void ucDisplayWindows_Load(object sender, EventArgs e)
        {
            InitializeUcDisplayWindowsParameters();

        }

        private void ucContactor_Load(object sender, EventArgs e)
        {
            ucContactor.btn_ConnectContactor.Click += new EventHandler(btn_ConnectContactor_Click);
            ucContactor.btn_connectThroughSwitch.Click += new EventHandler(btn_connectThroughSwitch_Click);
            ucContactor.btn_DisconnectContactor.Click += new EventHandler(btn_DisconnectContactor_Click);
            ucContactor.btn_SetContactorParameters.Click += new EventHandler(btn_SetContactorParameters_Click);
            ucContactor.btnReadStatus.Click += new EventHandler(btnReadStatus_Click);
            ucContactor.btn_GetContactorParams.Click += new EventHandler(btn_GetContactorParams_Click);
            ///Assign ucContactor Params
            ucContactor.Param_Contactor_object = (Param_ContactorExt)Param_Contactor_Object;
            Param_Contactor_Object.contactor_read_Status = null;
            ucContactor.Param_Monitoring_time_object = Param_Monitoring_Time_Object;
            ucContactor.Param_Limits_object = Param_Limits_Object;
            ucContactor.Parameterization_Limits = Limits;

        }

        private void ucDateTime_Load(object sender, EventArgs e)
        {
            ucDateTime.Param_clock_caliberation_object = Param_Clock_Caliberation_Object;
            ucDateTime.btn_GetTime.Click += new EventHandler(btn_GetTime_Click);
            ucDateTime.btn_SETtime.Click += new EventHandler(btn_SETtime_Click);
            ucDateTime.btnClockLimitSet.Click += btnClockLimitSet_Click;
            ucDateTime.btnTimeMethodsInvoke.Click += btnTimeMethodsInvoke_Click;
            ucDateTime.btnGetPresetAdjustTime.Click += btnGetPresetAdjustTime_Click;
            ucDateTime.btnClockSynchronizationMethodSet.Click += btnClockSynchronizationMethodSet_Click;
            ucDateTime.btnClockSyncMethodGet.Click += btnClockSyncMethodGet_Click;
            ucDateTime.btnGetShiftRange.Click += btnGetShiftRange_Click;
            ucDateTime.cmbTimeMethods.SelectedIndexChanged += cmbTimeMethods_SelectedIndexChanged;
            ucDateTime.txtClockShiftLimit.TextChanged += TxtClockShiftLimit_TextChanged;
        }

        private void ucParamSinglePhase_Load(object sender, EventArgs e)
        {
            InitializeUcSinglePhaseParameters();
            ///Init Event_Handlers
            ///Single Phase Monitoring Times Event Handlers
            ucParamSinglePhase.SP_btn_Get_MTPowerFail.Click += new EventHandler(SP_btn_Get_MTPowerFail_Click);
            ucParamSinglePhase.SP_btn_Get_MTReverseEnergy.Click += new EventHandler(SP_btn_Get_MTReverseEnergy_Click);
            ucParamSinglePhase.SP_btn_Get_MTEarth.Click += new EventHandler(SP_btn_Get_MTEarth_Click);
            ///Monitoring Times SET Event Handlers
            ucParamSinglePhase.SP_btn_Set_MTPowerFail.Click += new EventHandler(SP_btn_Set_MTPowerFail_Click);
            ucParamSinglePhase.SP_btn_Set_MTReverseEnergy.Click += new EventHandler(SP_btn_Set_MTReverseEnergy_Click);
            ucParamSinglePhase.SP_btn_Set_MTEarth.Click += new EventHandler(SP_btn_Set_MTEarth_Click);
            ///Single Phase Limits Event Handlers
            ucParamSinglePhase.btn_SP_GetOverLoadTotal.Click += new EventHandler(btn_SP_GetOverLoadTotal_Click);
            ucParamSinglePhase.btn_SP_SetOverLoadTotal.Click += new EventHandler(btn_SP_SetOverLoadTotal_Click);
            ///SinglePhaseMDI Parameters
            ucParamSinglePhase.SP_btn_Set_MDI_AutoResetDate.Click += new EventHandler(SP_btn_Set_MDI_AutoResetDate_Click);
            ucParamSinglePhase.SP_btn_Get_MDI_AutoResetDate.Click += new EventHandler(SP_btn_Get_MDI_AutoResetDate_Click);
            ///SinglePhaseEvent Error Details
            ucParamSinglePhase.SP_btn_getEventString.Click += new EventHandler(btn_getEventString_Click);
        }

        private void ucTimeWindowParam1_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeUcTimeWindowParameters();
            }
            catch { }
        }

        private void ucIPProfiles_Leave(object sender, EventArgs e)
        {
            try
            {
                if (ucIPProfiles != null && ucIPProfiles.Param_IP_Profiles_object != null)
                {
                    Param_IP_Profiles_object = ucIPProfiles.Param_IP_Profiles_object;
                }
            }
            catch { }
        }

        private void ucWakeupProfile_Leave(object sender, EventArgs e)
        {
            try
            {
                if (ucWakeupProfile != null && ucWakeupProfile.Param_Wakeup_Profile_object != null)
                {
                    Param_Wakeup_Profile_object = ucWakeupProfile.Param_Wakeup_Profile_object;
                }
            }
            catch { }
        }

        private void ucNumberProfile_Leave(object sender, System.EventArgs e)
        {
            try
            {
                if (ucNumberProfile != null && ucNumberProfile.Param_Number_Profile_object != null)
                {
                    Param_Number_Profile_object = ucNumberProfile.Param_Number_Profile_object;
                }
            }
            catch { }
        }

        #endregion

        public void ApplyAccessRights(ApplicationRight Rights, UserTypeID userTypeId, bool isSinglePhase)
        {
            try
            {
                this.SuspendLayout();

                #region Reset TAB Controls
                //tbcMeterParam_TABPages = null;
                //tab_ModemParameters_TABPages = null;
                //Tab_Main_TABPages = null;
                if (Tab_Main_TABPages != null && Tab_Main_TABPages.Count > 0)
                {
                    Tab_Main.TabPages.Clear();
                    Tab_Main.TabPages.AddRange(Tab_Main_TABPages.ToArray());
                }
                if (tbcMeterParam_TABPages != null && tbcMeterParam_TABPages.Count > 0)
                {
                    tbcMeterParams.TabPages.Clear();
                    tbcMeterParams.TabPages.AddRange(tbcMeterParam_TABPages.ToArray());
                }
                if (tab_ModemParameters_TABPages != null && tab_ModemParameters_TABPages.Count > 0)
                {
                    tab_ModemParameters.TabPages.Clear();
                    tab_ModemParameters.TabPages.AddRange(tab_ModemParameters_TABPages.ToArray());
                }
                #endregion

                #region Set Button Rights
                bool isMeterRight = Rights.MeterRights.Find(x => x.Write == true) != null;
                bool isLPRight = Rights.LoadProfileParams.Find(x => x.Write == true) != null;
                bool isModemRight = Rights.ModemRights.Find(x => x.Write == true) != null;
                bool isStatusWordRight = Rights.StatusWordParamRights.Find(x => x.Write == true) != null;
                bool isStandardModemRight = Rights.StandardModemRights.Find(x => x.Write == true) != null;

                if (!isMeterRight && !isLPRight && !isModemRight && !isStatusWordRight && !isStandardModemRight)
                {
                    if (this.fLP_ParamButtons.Controls.Contains(btn_SET_paramameters))
                        this.fLP_ParamButtons.Controls.Remove(btn_SET_paramameters);
                }
                else
                {
                    if (!this.fLP_ParamButtons.Controls.Contains(btn_SET_paramameters))
                        this.fLP_ParamButtons.Controls.Add(btn_SET_paramameters);
                }
                #endregion

                #region Get Button Rights
                isMeterRight = Rights.MeterRights.Find(x => x.Read == true) != null;
                isLPRight = Rights.LoadProfileParams.Find(x => x.Read == true) != null;
                isModemRight = Rights.ModemRights.Find(x => x.Read == true) != null;
                isStatusWordRight = Rights.StatusWordParamRights.Find(x => x.Read == true) != null;
                isStandardModemRight = Rights.StandardModemRights.Find(x => x.Read == true) != null;

                if (!isMeterRight && !isLPRight && !isModemRight && !isStatusWordRight && !isStandardModemRight)
                {
                    if (this.fLP_ParamButtons.Controls.Contains(btn_GET_paramameters))
                        this.fLP_ParamButtons.Controls.Remove(btn_GET_paramameters);
                }
                else
                {
                    if (!this.fLP_ParamButtons.Controls.Contains(btn_GET_paramameters))
                        this.fLP_ParamButtons.Controls.Add(btn_GET_paramameters);
                }
                #endregion

                #region Meter Parameters
                //Contactor Parameters____________________________________________________________________
                bool isAny = (Rights.MeterRights.Find(x => x.QuantityName == SharedCode.Comm.HelperClasses.MeterRights.Contactor.ToString() && x.Write == false && x.Read == false) == null);
                var IsAnyTrue = ucContactor.ApplyAccessRights(Rights.ContactorParamRights);
                if (!IsAnyTrue || !isAny) tbcMeterParams.TabPages.Remove(tbContactor);
                //Display Windows_________________________________________________________________________
                isAny = (Rights.MeterRights.Find(x => x.QuantityName == SharedCode.Comm.HelperClasses.MeterRights.DisplayWindows.ToString() && x.Write == false && x.Read == false) == null);
                IsAnyTrue = ucDisplayWindows1.ApplyAccessRights(Rights.DisplayWindowsRights, userTypeId);
                if (!IsAnyTrue || !isAny) tbcMeterParams.TabPages.Remove(tbDisplayWindows);
                //MDI Parameters__________________________________________________________________________
                isAny = (Rights.MeterRights.Find(x => x.QuantityName == SharedCode.Comm.HelperClasses.MeterRights.MDI.ToString() && x.Write == false && x.Read == false) == null);
                IsAnyTrue = ucMDIParams.ApplyAccessRights(Rights.MDIParametersRights);
                if (!IsAnyTrue || !isAny) tbcMeterParams.TabPages.Remove(tbMDI);
                //Monitoring Time Parameter_______________________________________________________________
                isAny = (Rights.MeterRights.Find(x => x.QuantityName == SharedCode.Comm.HelperClasses.MeterRights.MonitoringTime.ToString() && x.Write == false && x.Read == false) == null);
                IsAnyTrue = ucMonitoringTime.ApplyAccessRights(Rights.MonitoringTimeRights);
                if (!IsAnyTrue || !isAny) tbcMeterParams.TabPages.Remove(tbMonitoring);
                //Limits__________________________________________________________________________________
                isAny = (Rights.MeterRights.Find(x => x.QuantityName == SharedCode.Comm.HelperClasses.MeterRights.Limits.ToString() && x.Write == false && x.Read == false) == null);
                IsAnyTrue = ucLimits.ApplyAccessRights(Rights.LimitsRights, isSinglePhase); //for 1P
                if (!IsAnyTrue || !isAny) tbcMeterParams.TabPages.Remove(tbLimits);
                //Load Profile Parameters_________________________________________________________________
                isAny = (Rights.MeterRights.Find(x => x.QuantityName == SharedCode.Comm.HelperClasses.MeterRights.LoadProfile.ToString() && x.Write == false && x.Read == false) == null);
                IsAnyTrue = ucLoadProfile.ApplyAccessRights(Rights.LoadProfileParams);
                if (!IsAnyTrue || !isAny) tbcMeterParams.TabPages.Remove(tbLoad_profile);
                //Activity_Calendar_______________________________________________________________________
                isAny = (Rights.MeterRights.Find(x => x.QuantityName == SharedCode.Comm.HelperClasses.MeterRights.ActivityCalender.ToString() && x.Write == false && x.Read == false) == null);
                IsAnyTrue = ucActivityCalendar.ApplyAccessRights(Rights.ActivityCalenderRights);
                if (!IsAnyTrue || !isAny) tbcMeterParams.TabPages.Remove(tbTarrification);
                //Misc_Parameters_________________________________________________________________________
                isAny = (Rights.MeterRights.Find(x => x.QuantityName == SharedCode.Comm.HelperClasses.MeterRights.Misc.ToString() && x.Write == false && x.Read == false) == null);
                IsAnyTrue = HandleMiscOnAccessRights(Rights.MiscRights);
                if (!IsAnyTrue || !isAny) tbcMeterParams.TabPages.Remove(tbCaliberation);
                //Single_Phase_Meter Parameters___________________________________________________________
                isAny = (Rights.MeterRights.Find(x => x.QuantityName == SharedCode.Comm.HelperClasses.MeterRights.SinglePhase.ToString() && x.Write == false && x.Read == false) == null);
                IsAnyTrue = ucParamSinglePhase.ApplyAccessRights(Rights.SinglePhaseRights);
                if (!IsAnyTrue || !isAny) tbcMeterParams.TabPages.Remove(tbPnlSinglePhase);
                //Testing TAB Parameters___________________________________________________________________
                isAny = (Rights.MeterRights.Find(x => x.QuantityName == SharedCode.Comm.HelperClasses.MeterRights.Testing.ToString() && x.Write == false && x.Read == false) == null);
                IsAnyTrue = HandleTestingTabAccessRights(Rights.TestingRights);
                if (!IsAnyTrue || !isAny) tbcMeterParams.TabPages.Remove(tbTesting);
                //DateTime_Parameter_______________________________________________________________________
                isAny = (Rights.MeterRights.Find(x => x.QuantityName == SharedCode.Comm.HelperClasses.MeterRights.Clock.ToString() && x.Write == false && x.Read == false) == null);
                IsAnyTrue = ucDateTime.ApplyAccessRights(Rights.ClockRights);
                if (!IsAnyTrue || !isAny) tbcMeterParams.TabPages.Remove(tbRTC);
                //Display_Window Power_Down_Mode___________________________________________________________
                isAny = (Rights.MeterRights.Find(x => x.QuantityName == SharedCode.Comm.HelperClasses.MeterRights.DisplayPowerDown.ToString() && x.Write == false && x.Read == false) == null);
                IsAnyTrue = ucDisplayPowerDown1.ApplyAccessRights(Rights.DisplayPowerDown);
                if (!IsAnyTrue || !isAny) tbcMeterParams.TabPages.Remove(tbDisplayPowerDown);

                // TODO: Fahad 03   ** Access Writes ** (Remove TAB) Condition verify with thanda dimagh
                //Schedule Entries___________________________________________________________
                isAny = (Rights.MeterRights.Find(x => x.QuantityName == SharedCode.Comm.HelperClasses.MeterRights.Schedule_Entry.ToString() && x.Write == false && x.Read == false) == null);
                //IsAnyTrue = ucDisplayPowerDown1.ApplyAccessRights(Rights.DisplayPowerDown);
                if (!isAny)
                    tbcMeterParams.TabPages.Remove(tbSchedule);
                else if (!tbcMeterParams.TabPages.Contains(tbSchedule))
                    tbcMeterParams.TabPages.Add(tbSchedule);

                //Generator_Start___________________________________________________________
                isAny = (Rights.MeterRights.Find(x => x.QuantityName == SharedCode.Comm.HelperClasses.MeterRights.GeneratorStart.ToString() && x.Write == false && x.Read == false) == null);
                if (!isAny)
                    tbcMeterParams.TabPages.Remove(tbGeneratorStart);
                else if (!tbcMeterParams.TabPages.Contains(tbGeneratorStart))
                    tbcMeterParams.TabPages.Add(tbGeneratorStart);
                //~~

                if (tbcMeterParams.TabPages.Count <= 0)
                    this.Tab_Main.TabPages.Remove(Meter);
                #endregion

                //Meter Schedule___________________________________________________________________________
                #region Meter Schedule

                //IsAnyTrue = ucTimeWindowParam1.ApplyAccessRights(Rights.MeterSchedulingRights);
                //if (!IsAnyTrue)
                //    check_TBE1_PowerFail.Enabled = check_TBE1_PowerFail.Visible = false;
                //bool _IsAnyTrue = ucTimeWindowParam2.ApplyAccessRights(Rights.MeterSchedulingRights);
                //if (!_IsAnyTrue)
                //    check_TBE2_PowerFail.Enabled = check_TBE2_PowerFail.Visible = false;
                //if (!IsAnyTrue && !_IsAnyTrue) Tab_Main.TabPages.Remove(TimeBasedEvents);

                IsAnyTrue = ucTimeWindowParam1.ApplyAccessRights(Rights.MeterSchedulingRights);
                if (!IsAnyTrue)
                    check_TBE1_PowerFail.Enabled = check_TBE1_PowerFail.Visible = false;

                if (!ucTimeWindowParam1.IsControlRDEnable(MeterScheduling.TimeBaseEvent1))
                {
                    ucTimeWindowParam1.Visible = false;
                }
                else
                    ucTimeWindowParam1.Visible = true;

                bool _IsAnyTrue = ucTimeWindowParam2.ApplyAccessRights(Rights.MeterSchedulingRights);
                if (!_IsAnyTrue)
                    check_TBE2_PowerFail.Enabled = check_TBE2_PowerFail.Visible = false;

                if (!ucTimeWindowParam2.IsControlRDEnable(MeterScheduling.TimeBaseEvent2))
                {
                    ucTimeWindowParam2.Visible = false;
                }
                else
                    ucTimeWindowParam2.Visible = true;

                if (!IsAnyTrue && !_IsAnyTrue) Tab_Main.TabPages.Remove(tpTimeBasedEvents);

                //TB1_PowerFail_Enable
                var AccessRight = Rights.MeterSchedulingRights.Find((x) => (x.QuantityName.Equals(MeterScheduling.PowerFailTBE1.ToString())));
                if (AccessRight != null)
                {
                    check_TBE1_PowerFail.Enabled = AccessRight.Write;
                    check_TBE1_PowerFail.Visible = AccessRight.Read || AccessRight.Write;
                }
                //TB2_PowerFail_Enable
                AccessRight = Rights.MeterSchedulingRights.Find((x) => (x.QuantityName.Equals(MeterScheduling.PowerFailTBE2.ToString())));
                if (AccessRight != null)
                {
                    check_TBE2_PowerFail.Enabled = AccessRight.Write;
                    check_TBE2_PowerFail.Visible = AccessRight.Read || AccessRight.Write;
                }

                isAny = (Rights.MeterRights.Find(x => x.QuantityName == SharedCode.Comm.HelperClasses.MeterRights.MeterScheduling.ToString() && x.Write == false && x.Read == false) == null);

                if (!isAny && Tab_Main.TabPages.Contains(tpTimeBasedEvents))
                    Tab_Main.TabPages.Remove(tpTimeBasedEvents);

                #endregion

                #region Modem Handling Combine Control

                bool isIpTabe = (Rights.ModemRights.Find(x => x.QuantityName == SharedCode.Comm.HelperClasses.Modem.IPProfile.ToString() && x.Write == false && x.Read == false) == null);
                bool isCommunicationProfileTabe = (Rights.ModemRights.Find(x => x.QuantityName == SharedCode.Comm.HelperClasses.Modem.CommunicationProfile.ToString() && x.Write == false && x.Read == false) == null);
                bool isWakeUpProfileTabe = (Rights.ModemRights.Find(x => x.QuantityName == SharedCode.Comm.HelperClasses.Modem.WakeUpProfile.ToString() && x.Write == false && x.Read == false) == null);
                bool isKeepAliveTabe = (Rights.ModemRights.Find(x => x.QuantityName == SharedCode.Comm.HelperClasses.Modem.KeepAlive.ToString() && x.Write == false && x.Read == false) == null);
                bool isNumberProfileTabe = (Rights.ModemRights.Find(x => x.QuantityName == SharedCode.Comm.HelperClasses.Modem.NumberProfile.ToString() && x.Write == false && x.Read == false) == null);
                bool isModemLimitsAndTimeTabe = (Rights.ModemRights.Find(x => x.QuantityName == SharedCode.Comm.HelperClasses.Modem.ModemLimitsAndTime.ToString() && x.Write == false && x.Read == false) == null);
                bool isModemInitiallizationTabe = (Rights.ModemRights.Find(x => x.QuantityName == SharedCode.Comm.HelperClasses.Modem.ModemInitiallization.ToString() && x.Write == false && x.Read == false) == null);

                //Modem IPProfile Parameters________________________________________________________________
                bool IsIPProfile = ucIPProfiles.ApplyAccessRights(Rights.IPProfiles);
                if (!IsIPProfile || !isIpTabe) tab_ModemParameters.TabPages.Remove(IP_Profiles);
                //Modem NumberProfile Parameters________________________________________________________________
                bool IsNumberProfile = ucNumberProfile.ApplyAccessRights(Rights.NumberProfiles);
                if (!IsNumberProfile || !isNumberProfileTabe) tab_ModemParameters.TabPages.Remove(Number_Profile);
                //Modem CommunicationProfile Parameters________________________________________________________________
                bool IsCommunicationProfile = ucCommProfile.ApplyAccessRights(Rights.CommunicationProfiles);
                if (!IsCommunicationProfile || !isCommunicationProfileTabe) tab_ModemParameters.TabPages.Remove(communication_profile);
                //Modem WakeupProfiles Parameters________________________________________________________________
                bool IsWakeupProfile = ucWakeupProfile.ApplyAccessRights(Rights.WakeupProfiles);
                if (!IsWakeupProfile || !isWakeUpProfileTabe) tab_ModemParameters.TabPages.Remove(WakeUp_profiles);
                //Modem KeepAlive Parameters________________________________________________________________
                bool IsKeepAlive = ucKeepAlive.ApplyAccessRights(Rights.KeepAlive);
                if (!IsKeepAlive || !isKeepAliveTabe) tab_ModemParameters.TabPages.Remove(Keep_Alive);
                //Modem Limits Parameters________________________________________________________________
                bool IsModemLimit = ucModemLimitsAndTime.ApplyAccessRights(Rights.ModemLimits);
                if (!IsModemLimit || !isModemLimitsAndTimeTabe) tab_ModemParameters.TabPages.Remove(modem_limits);
                //Modem Initialize Parameters________________________________________________________________
                bool IsModemInitiailze = ucModemInitialize.ApplyAccessRights(Rights.ModemInitialize);
                if (!IsModemInitiailze || !isModemInitiallizationTabe) tab_ModemParameters.TabPages.Remove(Modem_Initialize);
                //Modem HDLC Setup Parameters________________________________________________________________
                bool IsHDLCSetup = ucHDLCSetup.ApplyAccessRights(Rights.HDLCSetupRights);
                //Check if nothing is functional in this tab then it should be remove
                if (!IsHDLCSetup && ((ucHDLCSetup.btnGetParameter.Visible == false || ucHDLCSetup.btnSetDeviceAddress.Visible == false
                                 || ucHDLCSetup.btnSetInactivityTimeOut.Visible == false)))
                    tab_ModemParameters.TabPages.Remove(tpHDLCSetup);
                //Remove Modem Parameter TAB
                if (tab_ModemParameters.TabPages.Count <= 0)
                    Tab_Main.TabPages.Remove(Modem);

                #endregion

                #region Standard Modem Handling Combine Control

                //Standard Modem Parameters________________________________________________________________
                bool IsStandardModem = ucStandardModem.ApplyAccessRights(Rights.StandardModemRights);
                isAny = (Rights.StandardModemRights.Find(x => x.QuantityName == SharedCode.Comm.HelperClasses.StandardModem.StandardIPProfile.ToString() && x.Write == false && x.Read == false) == null) &&
                        (Rights.StandardModemRights.Find(x => x.QuantityName == SharedCode.Comm.HelperClasses.StandardModem.StandardKeepAlive.ToString() && x.Write == false && x.Read == false) == null) &&
                        (Rights.StandardModemRights.Find(x => x.QuantityName == SharedCode.Comm.HelperClasses.StandardModem.StandardNumberProfile.ToString() && x.Write == false && x.Read == false) == null);

                //Remove Standard Modem Parameter TAB
                if (!IsStandardModem || !isAny)
                    Tab_Main.TabPages.Remove(tpStandardModem);

                #endregion

                //v4.8.22
                #region Normal/Advance Print Report
                var genRights = Application_Controller.CurrentUser.CurrentAccessRights.GeneralRights;


                if (genRights == null)
                    throw new Exception("Unable to apply gen Rights");

                var AccessRight_gen = genRights.Find((x) => x.QuantityName.Contains(GeneralRights.Parameter.ToString()));

                if (AccessRight_gen != null && (AccessRight_gen.Write))
                {
                    rdbNormal.Visible = rdbAdvance.Visible = true;
                    rdbNormal.Checked = true;   //v4.8.31
                }
                else
                {
                    rdbNormal.Visible = rdbAdvance.Visible = false;
                    rdbNormal.Checked = true;
                }
                #endregion

                //v4.8.41
                #region Status Word Window Parameters

                IsAnyTrue = ucStatusWordMap1.ApplyAccessRights(Rights.StatusWordMapRights);
                isAny = (Rights.StatusWordParamRights.Find(x => x.QuantityName == SharedCode.Comm.HelperClasses.StatusWordParam.StausWord1.ToString() && x.Write == false && x.Read == false) == null) &&
                        (Rights.StatusWordParamRights.Find(x => x.QuantityName == SharedCode.Comm.HelperClasses.StatusWordParam.StatusWord2.ToString() && x.Write == false && x.Read == false) == null);
                if (!IsAnyTrue || !isAny)
                    Tab_Main.TabPages.Remove(tpStatusWord);


                #endregion

                // v 5.0.0.3
                #region Parameters Reports
                this.btn_GenerateReport.Visible = false;
                foreach (var item in Rights.GeneralRights)
                {
                    _HelperAccessRights((GeneralRights)Enum.Parse(item.QuantityType, item.QuantityName), item.Read, item.Write);
                }
                #endregion

                //v5.3.12
                #region Energy Mizer
                IsAnyTrue = ucEnergyMizer1.ApplyAccessRights(Rights.EnergyMizerRights);
                if (!IsAnyTrue)
                    Tab_Main.TabPages.Remove(tpEnergyMizer);
                #endregion
            }
            finally
            {
                this.ResumeLayout();
            }
        }

        private void _HelperAccessRights(GeneralRights qty, bool read, bool write)
        {
            switch (qty)
            {
                case GeneralRights.IgnoreReports:
                    if (read == true)
                    {
                        this.HidePrintReportButtons = true;
                        this.btn_GenerateReport.Visible = false;
                    }
                    else
                    {
                        this.btn_GenerateReport.Visible = true;
                        this.HidePrintReportButtons = false;
                    }
                    break;
                default:
                    break;
            }
        }


        #region Miscellaneous Handling Combine Control

        private bool HandleMiscOnAccessRights(List<AccessRights> misc)
        {
            bool IsAnyReadWrite = false;
            try
            {
                if (misc.Find(x => x.Write == true || x.Read == true) == null)
                {
                    return false;
                }
                else
                {
                    //Apply Customer Reference
                    IsAnyReadWrite = ucCustomerReference.ApplyAccessRights(misc);
                    ucCustomerReference.Visible = ucCustomerReference.Enabled = IsAnyReadWrite;
                    //Apply CTPTPatio
                    IsAnyReadWrite = ucCTPTRatio.ApplyAccessRights(misc);
                    ucCTPTRatio.Visible = ucCTPTRatio.Enabled = IsAnyReadWrite;
                    //Apply AssociationPassword
                    IsAnyReadWrite = ucPasswords.ApplyAccessRights(misc);
                    ucPasswords.Visible = ucPasswords.Enabled = IsAnyReadWrite;
                    //Apply GernalProcessParameter
                    IsAnyReadWrite = ucGeneralProcess.ApplyAccessRights(misc);
                    ucGeneralProcess.Visible = ucGeneralProcess.Enabled = IsAnyReadWrite;
                    //Apply EnergyParam
                    IsAnyReadWrite = ucEnergyParam.ApplyAccessRights(misc);
                    ucEnergyParam.Visible = ucEnergyParam.Enabled = IsAnyReadWrite;
                    //Apply DecimalPoint
                    IsAnyReadWrite = ucDecimalPoint.ApplyAccessRights(misc);
                    ucDecimalPoint.Visible = ucDecimalPoint.Enabled = IsAnyReadWrite;
                    //Apply Clock
                    IsAnyReadWrite = ucClockCalib.ApplyAccessRights(misc);
                    ucClockCalib.Visible = ucClockCalib.Enabled = IsAnyReadWrite;

                    //SecurityControl
                    ApplyAccessRights_SecurityControl(misc);

                    IsAnyReadWrite = false;
                    foreach (var item in misc)
                    {
                        if (item.QuantityType == typeof(Misc) && !IsAnyReadWrite)
                            IsAnyReadWrite = (item.Read || item.Write);
                    }
                    return IsAnyReadWrite;
                }
            }
            finally
            {

            }
        }

        #endregion

        #region Security Data Access Rights

        public bool ApplyAccessRights_SecurityControl(List<AccessRights> Rights)
        {
            bool isControlVisible = false;
            bool isSuccess = false;
            try
            {
                this.SuspendLayout();
                if (Rights.Find(x => x.Read == true || x.Write == true) != null)
                {
                    foreach (var item in Rights)
                    {
                        _HelperAccessRights((Misc)Enum.Parse(item.QuantityType, item.QuantityName), item.Read, item.Write, ref isControlVisible);

                        //if (!isControlVisible && (item.Read || item.Write))
                        //    isControlVisible = true;
                    }
                    isSuccess = true;
                }

                gpSecurityData.Visible = isControlVisible;
            }
            finally
            {
                this.ResumeLayout();
            }
            return isSuccess;
        }

        private void _HelperAccessRights(Misc qty, bool read, bool write, ref bool IsGbSecurityVisble)
        {
            //No Security
            //AUTHENTICATION
            //ENCRYPTION
            //AUTH_ENCR
            switch (qty)
            {
                case Misc.SecData_AuthenticationKey:
                    if (!IsGbSecurityVisble) IsGbSecurityVisble = write;
                    lblAuthenticationKey.Visible =
                    txtAuthenticationKey.Visible =
                    btnGenerateAuthenticationKey.Visible = (write);
                    //txtAuthenticationKey.Enabled = write;
                    break;
                case Misc.SecData_EncrptiontionKey:
                    if (!IsGbSecurityVisble) IsGbSecurityVisble = write;
                    lblEncryptionKey.Visible =
                    txtEncryptionKey.Visible =
                    btnGenetrateEncryptionKey.Visible = (write);
                    //txtEncryptionKey.Enabled = write;
                    break;
                case Misc.SecData_Control_Authentication:
                    if (!IsGbSecurityVisble) IsGbSecurityVisble = (read || write);
                    if (read || write)
                        cmbSecurityControl.Items.Add(SecurityControl.AuthenticationOnly.ToString());
                    break;
                case Misc.SecData_Control_Encrptiontion:
                    if (!IsGbSecurityVisble) IsGbSecurityVisble = (read || write);
                    if (read || write)
                        cmbSecurityControl.Items.Add(SecurityControl.EncryptionOnly.ToString());
                    break;
                case Misc.SecData_Control_Auth_Encrp:
                    if (!IsGbSecurityVisble) IsGbSecurityVisble = (read || write);
                    if (read || write)
                        cmbSecurityControl.Items.Add(SecurityControl.AuthenticationAndEncryption.ToString());
                    break;
                case Misc.SecData_Control_NoSecurity:
                    if (!IsGbSecurityVisble) IsGbSecurityVisble = (read || write);
                    if (read || write)
                        cmbSecurityControl.Items.Add(SecurityControl.None.ToString());
                    break;
                case Misc.TariffOnStartGenerator:
                    grpTariffOnStartGenerator.Visible = lblTarrifOnGeneratorStart.Visible = cmbTarrifOnGeneratorStart.Visible = read || write;
                    cmbTarrifOnGeneratorStart.Enabled = write;
                    break;
                default:
                    break;
            }
        }

        #endregion


        #region Testing Tab Handling Combine Control

        private bool HandleTestingTabAccessRights(List<AccessRights> testPanel)
        {
            bool IsAnyReadWrite = false;
            try
            {
                tbTesting.SuspendLayout();
                fLP_Main_Testing.SuspendLayout();

                if (testPanel.Find(x => x.Write == true || x.Read == true) == null)
                {
                    return false;
                }
                else
                {
                    //Apply Testing Panel Access Rights
                    bool IsReadWrite = ApplicationRight.IsControlRDEnable(typeof(Testing), Testing.TestingPanel.ToString(), testPanel);
                    IsAnyReadWrite = IsReadWrite = IsReadWrite || ApplicationRight.IsControlWTEnable(typeof(Testing), Testing.TestingPanel.ToString(), testPanel);
                    gb_Debug_Read_LOG.Enabled = IsReadWrite;
                    FLP_ReadLog.Visible = IsReadWrite;
                    //Apply RTC Update Rights
                    IsReadWrite = ApplicationRight.IsControlWTEnable(typeof(Testing), Testing.RTCUpdate.ToString(), testPanel);
                    if (!IsAnyReadWrite)
                        IsAnyReadWrite = IsReadWrite;
                    gpClockUp.Enabled = IsReadWrite;
                    gpClockUp.Visible = IsReadWrite;

                    return IsAnyReadWrite;
                }
            }
            finally
            {
                tbTesting.ResumeLayout();
                fLP_Main_Testing.ResumeLayout();
            }
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                            "Save Param vsbl:" + btn_Caliberation_Save.Visible +
                            "\nSave Param enb:" + btn_Caliberation_Save.Enabled +

                            "\nLoad Param vsbl:" + btn_caliberation_loadall.Visible +
                            "\nLoad Param enb:" + btn_caliberation_loadall.Enabled +

                            "\nGet vsbl:" + btn_GET_paramameters.Visible +
                            "\nGet enb:" + btn_GET_paramameters.Enabled +

                            "\nSet vsbl:" + btn_SET_paramameters.Visible +
                            "\nSet enb:" + btn_SET_paramameters.Enabled +

                            "\nRpt vsbl:" + btn_GenerateReport.Visible +
                            "\nRpt enb:" + btn_GenerateReport.Enabled

);
        }

        #region Param Read Write Test
        bool is_Set = false;
        bool is_Get = false;
        int WriteTestCount = 0,
            ReadTestCount = 0,
            WriteFailCount = 0,
            ReadFailCount = 0;

        private void btnStartParamTest_Click(object sender, EventArgs e)
        {
            //Param Read Write Test Implemented in v4.8.21 by M.Azeem Inayat
            bool get_Called = false;
            try
            {
                ///Not Connected Properly
                if (!(Application_Process.Is_Association_Developed))
                {
                    Notification n = new Notification("Disconnected", "Create Association to Meter");
                    return;
                }

                if (cbIsWriteParam.Checked)
                {
                    if (Application_Controller.CurrentUser != null)
                    {
                        setter = new CustomSetGet("Set Parameters", application_Controller.isSinglePhase, Application_Controller.CurrentUser.CurrentAccessRights, 2);
                    }
                    else
                    {
                        setter = new CustomSetGet("Set Parameters", application_Controller.isSinglePhase);
                        setter.IsReadMode = 2;
                    }

                    if (setter.ShowDialog(this.Parent) == DialogResult.OK && setter.IsAnyChecked)
                    {
                        is_Set = true;
                    }
                    else
                    {
                        is_Set = false;
                    }
                }

                if (cbIsReadParam.Checked)
                {
                    //By Azeem
                    if (Application_Controller.CurrentUser != null)
                    {
                        CustGetDialog = new CustomSetGet("Get Parameters", application_Controller.isSinglePhase, Application_Controller.CurrentUser.CurrentAccessRights, 1);
                    }
                    else
                    {
                        CustGetDialog = new CustomSetGet("Get Parameters.", application_Controller.isSinglePhase);
                        CustGetDialog.IsReadMode = 1;
                    }

                    CustGetDialog.check_Password_Elec.Enabled = false;
                    if (CustGetDialog.ShowDialog(this.Parent) == DialogResult.OK && CustGetDialog.IsAnyChecked)
                    {
                        is_Get = true;
                    }
                    else
                    {
                        is_Get = false;
                    }
                }

                if (is_Set && cbWriteThenRead.Checked)
                {
                    this.Parameterization_BckWorkerThread.DoWork += new DoWorkEventHandler(BckWorker_SETParams_T_DoEventHandler);
                    this.Parameterization_BckWorkerThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BckWorker_SETParam_T_WorkCompleted);
                    this.Parameterization_BckWorkerThread.ProgressChanged += new ProgressChangedEventHandler(BckWorker_SETParam_T_ProgressChanged);
                    //dlg = new ProgressDialog();
                    //dlg.IsAutoHideNow = true;

                    //Param_Controller.ParameterSetStatus += dlg.ConnController_ProcessStatusHandler;
                    //dlg.EnableProgressBar = true;
                    //dlg.Text = "Parameterizing";
                    //dlg.DialogTitle = "Setting Parameters";
                    Application_Controller.IsIOBusy = true;
                    Parameterization_BckWorkerThread.RunWorkerAsync();
                    //dlg.ShowDialog(this.Parent);
                    Application.DoEvents();
                }
                else
                {
                    get_Called = true;
                    this.Parameterization_BckWorkerThread.DoWork += new DoWorkEventHandler(BckWorker_GETParams_T_DoEventHandler);
                    this.Parameterization_BckWorkerThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BckWorker_GETParam_T_WorkCompleted);
                    this.Parameterization_BckWorkerThread.ProgressChanged += new ProgressChangedEventHandler(BckWorker_GETParam_T_ProgressChanged);

                    //dlg = new ProgressDialog();
                    //dlg.IsAutoHideNow = true;

                    //Param_Controller.ParameterGetStatus += dlg.ConnController_ProcessStatusHandler;
                    //dlg.EnableProgressBar = true;
                    //dlg.Text = "Reading Parameters";
                    //dlg.DialogTitle = "Reading Parameters";

                    if (!Parameterization_BckWorkerThread.IsBusy)
                    {
                        Application_Controller.IsIOBusy = true;
                        Parameterization_BckWorkerThread.RunWorkerAsync();
                        ///Disable Meter Passwords
                        //dlg.ShowDialog(this.Parent);
                        Application.DoEvents();
                        //dlg.ConnController_ProcessStatusHandler("Parameterization Read Process is started");
                    }
                }

                if (!is_Set && !get_Called)
                {
                    this.Parameterization_BckWorkerThread.DoWork += new DoWorkEventHandler(BckWorker_GETParams_T_DoEventHandler);
                    this.Parameterization_BckWorkerThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BckWorker_GETParam_T_WorkCompleted);
                    this.Parameterization_BckWorkerThread.ProgressChanged += new ProgressChangedEventHandler(BckWorker_GETParam_T_ProgressChanged);

                    //dlg = new ProgressDialog();
                    //dlg.IsAutoHideNow = true;

                    //Param_Controller.ParameterGetStatus += dlg.ConnController_ProcessStatusHandler;
                    //dlg.EnableProgressBar = true;
                    //dlg.Text = "Reading Parameters";
                    //dlg.DialogTitle = "Reading Parameters";

                    if (!Parameterization_BckWorkerThread.IsBusy)
                    {
                        Application_Controller.IsIOBusy = true;
                        Parameterization_BckWorkerThread.RunWorkerAsync();
                        ///Disable Meter Passwords
                        //dlg.ShowDialog(this.Parent);
                        Application.DoEvents();
                        //dlg.ConnController_ProcessStatusHandler("Parameterization Read Process is started");
                    }
                }
            }
            catch (Exception ex)
            {
                String ErrorMsg = string.Format("Error in SET/GET {0},Details{1}", ex.Message, (ex.InnerException == null) ? null : ex.InnerException.Message);
                MessageBox.Show(ErrorMsg, "Error in SET/GET ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }

        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            //StopTest = true;
            is_Get = is_Set = false;
            Application_Controller.IsIOBusy = false;
        }

        private void BckWorker_GETParams_T_DoEventHandler(Object sender, DoWorkEventArgs arg)
        {
            Application.DoEvents();
            try
            {
                Notification Notifier;
                //dlg.Visible = true;
                KharabParams = "";
                SabSet = true;
                Param_Controller.ParametersGETStatus.ResetCommandStatus();

                tbReadTestCount.Text = (++ReadTestCount).ToString();

                //dlg.ConnController_ProcessStatusHandler("Parameter Read Process is started");

                dbController.createSession(Application_Controller.ConnectionManager.ConnectionInfo.MSN,
                                           Application_Controller.Applicationprocess_Controller.UserId.ToString());
                if (CustGetDialog.check_ActivityCalender.Checked)// && CustGetDialog.check_ActivityCalender.Visible)
                {
                    try
                    {
                        GET_ActivityCalendar();
                        Notifier = new Notification("GET_OK", "ActivityCalendar");
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "Activity Calendar \r\n";
                        Notifier = new Notification("GET_Fail", "ActivityCalendar");
                        tbReadFailCount.Text = (++ReadFailCount).ToString();
                    }
                }
                if (CustGetDialog.check_Clock.Checked)// && CustGetDialog.check_Clock.Visible)
                {
                    try
                    {
                        GET_Clock();
                        Notifier = new Notification("GET_OK", "Clock");
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "Clock \r\n";
                        Notifier = new Notification("GET_Fail", "Clock");
                        tbReadFailCount.Text = (++ReadFailCount).ToString();
                    }
                }
                if (CustGetDialog.check_Contactor.Checked)// && CustGetDialog.check_Contactor.Visible)
                {
                    try
                    {
                        GET_Contactor();
                        Notifier = new Notification("GET_OK", "Contactor");
                    }
                    catch (Exception)
                    {
                        KharabParams += "Contactor \r\n";
                        Notifier = new Notification("GET_Fail", "Contactor");
                        tbReadFailCount.Text = (++ReadFailCount).ToString();
                    }
                }
                Application.DoEvents();

                if (CustGetDialog.check_CTPT.Checked)//&& CustGetDialog.check_CTPT.Visible)
                {
                    try
                    {
                        GET_CTPT();
                        Notifier = new Notification("GET_OK", "CTPT");
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "CTPT \r\n";
                        Notifier = new Notification("GET_Fail", "CTPT");
                        tbReadFailCount.Text = (++ReadFailCount).ToString();
                    }
                }
                if (CustGetDialog.check_customerReference.Checked)//&& CustGetDialog.check_customerReference.Visible)
                {
                    try
                    {
                        GET_CustomerReference();
                        GET_CustomerName();
                        GET_CustomerAddress();
                        Notifier = new Notification("GET_OK", "GET_CustomerReference");
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "Customer Reference \r\n";
                        Notifier = new Notification("GET_Fail", "GET_CustomerReference");
                        tbReadFailCount.Text = (++ReadFailCount).ToString();
                    }
                }
                if (CustGetDialog.check_DataProfilewithEvents.Checked && CustGetDialog.check_DataProfilewithEvents.Visible)
                {
                    try
                    {
                        GET_DataProfilesWithEvents();
                        Notifier = new Notification("GET_OK", "GET_DataProfilesWithEvents");
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "DataPrfile with Events \r\n";
                        Notifier = new Notification("GET_Fail", "GET_DataProfilesWithEvents");
                        tbReadFailCount.Text = (++ReadFailCount).ToString();
                    }
                }
                Application.DoEvents();

                if (CustGetDialog.check_DecimalPoint.Checked)//&& CustGetDialog.check_DecimalPoint.Visible)
                {
                    try
                    {
                        GET_DecimalPoint();
                        Notifier = new Notification("GET_OK", "GET_DecimalPoint");
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "Decimal Point \r\n";
                        Notifier = new Notification("GET_Fail", "GET_DecimalPoint");
                        tbReadFailCount.Text = (++ReadFailCount).ToString();
                    }
                }
                //if (CustGetDialog.check_DisplayWindows_Nor.Checked == true) GET_DisplayWindows();
                if (CustGetDialog.check_DisplayWindows_Nor.Checked)//&& CustGetDialog.check_DisplayWindows_Nor.Visible)
                {
                    try
                    {
                        GET_DisplayWindows_Nor();
                        Notifier = new Notification("GET_OK", "GET_DisplayWindows_Nor");
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "Display Windows Normal \r\n";
                        Notifier = new Notification("GET_Fail", "GET_DisplayWindows_Nor");
                        tbReadFailCount.Text = (++ReadFailCount).ToString();
                    }
                }
                if (CustGetDialog.check_DisplayWindows_Alt.Checked)//&& CustGetDialog.check_DisplayWindows_Alt.Visible)
                {
                    try
                    {
                        GET_DisplayWindows_Alt();
                        Notifier = new Notification("GET_OK", "GET_DisplayWindows_Alt");
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "Display Window Alternate \r\n";
                        Notifier = new Notification("GET_Fail", "GET_DisplayWindows_Alt");
                        tbReadFailCount.Text = (++ReadFailCount).ToString();
                    }
                }

                if (CustGetDialog.check_DisplayWindows_test.Checked)// && CustGetDialog.check_DisplayWindows_test.Visible)
                {
                    try
                    {
                        GET_DisplayWindows_Test();
                        Notifier = new Notification("GET_OK", "GET_DisplayWindows_Test");
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "Display WIndow Test mode \r\n";
                        Notifier = new Notification("GET_Fail", "GET_DisplayWindows_Test");
                        tbReadFailCount.Text = (++ReadFailCount).ToString();
                    }
                }
                Application.DoEvents();

                if (CustGetDialog.check_DisplayPowerDown.Checked)//&& CustGetDialog.check_DisplayPowerDown.Visible)
                {
                    try
                    {
                        GET_DisplayWindowsPowerDown();
                        Notifier = new Notification("GET_OK", "GET_DisplayWindowsPowerDown");
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "Display Power Down Mode \r\n";
                        Notifier = new Notification("GET_Fail", "GET_DisplayWindowsPowerDown");
                        tbReadFailCount.Text = (++ReadFailCount).ToString();
                    }
                }
                if (CustGetDialog.check_EnergyParams.Checked)//&& CustGetDialog.check_EnergyParams.Visible)
                {
                    try
                    {
                        GET_EnergyParam();
                        Notifier = new Notification("GET_OK", "GET_EnergyParam");
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "Energy Params \r\n";
                        Notifier = new Notification("GET_Fail", "GET_EnergyParam");
                        tbReadFailCount.Text = (++ReadFailCount).ToString();
                    }
                }
                //if (CustGetDialog.check_EventCaution.Checked) GET_EventCautons();
                if (CustGetDialog.check_IPV4.Checked && CustGetDialog.check_IPV4.Visible)
                {
                    try
                    {
                        GET_IPV4();
                        Notifier = new Notification("GET_OK", "GET_IPV4");
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "IPV4 \r\n";
                        Notifier = new Notification("GET_Fail", "GET_IPV4");
                        tbReadFailCount.Text = (++ReadFailCount).ToString();
                    }
                }
                Application.DoEvents();

                if (CustGetDialog.check_Limits.Checked)// && CustGetDialog.check_Limits.Visible)
                {
                    try
                    {
                        GET_Limits();
                        Notifier = new Notification("GET_OK", "GET_Limits");
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "Limits \r\n";
                        Notifier = new Notification("GET_Fail", "GET_Limits");
                        tbReadFailCount.Text = (++ReadFailCount).ToString();
                    }
                }
                if (CustGetDialog.chk_LoadProfile.Checked || CustGetDialog.chk_LoadProfile_Interval.Checked)
                {
                    try
                    {
                        GET_LoadProfileChannels(LoadProfileScheme.Load_Profile);
                    }
                    catch (Exception)
                    {

                        SabSet = false;
                        KharabParams += "Load Profile Channels \r\n";
                    }
                }

                if (CustGetDialog.chk_LoadProfile_2.Checked || CustGetDialog.chk_LoadProfile_2_Interval.Checked)
                {
                    try
                    {
                        GET_LoadProfileChannels(LoadProfileScheme.Load_Profile_Channel_2);
                    }
                    catch (Exception)
                    {

                        SabSet = false;
                        KharabParams += "Load Profile Channels \r\n";
                    }
                }

                if (CustGetDialog.chk_PQ_LoadProfileInterval.Checked)
                {
                    try
                    {
                        GET_LoadProfileInterval(LoadProfileScheme.Daily_Load_Profile);
                    }
                    catch (Exception)
                    {

                        SabSet = false;
                        KharabParams += "Load Profile Interval \r\n";
                    }
                }
                if (CustGetDialog.check_MajorAlarmprofile.Checked && CustGetDialog.check_MajorAlarmprofile.Visible)
                {
                    try
                    {
                        GET_MajorAlarmProfile();
                        Notifier = new Notification("GET_OK", "GET_MajorAlarmProfile");
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "Major Alarm Profile \r\n";
                        Notifier = new Notification("GET_Fail", "GET_MajorAlarmProfile");
                        tbReadFailCount.Text = (++ReadFailCount).ToString();
                    }
                }
                if (CustGetDialog.check_MDI_params.Checked)//&& CustGetDialog.check_MDI_params.Visible)
                {
                    try
                    {
                        GET_MDIParams();
                        Notifier = new Notification("GET_OK", "GET_MDIParams");
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "MDI Params \r\n";
                        Notifier = new Notification("GET_Fail", "GET_MDIParams");
                        tbReadFailCount.Text = (++ReadFailCount).ToString();
                    }
                }
                Application.DoEvents();

                if (CustGetDialog.check_MonitoringTime.Checked)//&& CustGetDialog.check_MonitoringTime.Visible)
                {
                    try
                    {
                        GET_MonitoringTime();
                        Notifier = new Notification("GET_OK", "GET_MonitoringTime");
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "Monitoring Time \r\n";
                        Notifier = new Notification("GET_Fail", "GET_MonitoringTime");
                        tbReadFailCount.Text = (++ReadFailCount).ToString();
                    }
                }
                if (CustGetDialog.chk_GPP.Checked)// && CustGetDialog.chk_GPP.Visible)
                {
                    try
                    {
                        GET_GeneralProcess();
                        Notifier = new Notification("GET_OK", "GET_GeneralProcess");
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "General Process \r\n";
                        Notifier = new Notification("GET_Fail", "GET_GeneralProcess");
                        tbReadFailCount.Text = (++ReadFailCount).ToString();
                    }
                }

                if (CustGetDialog.check_TCPUDP.Checked)// && CustGetDialog.check_TCPUDP.Visible)
                {
                    try
                    {
                        GET_TCPUDP();
                        Notifier = new Notification("GET_OK", "GET_TCPUDP");
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "TCP UDP \r\n";
                        Notifier = new Notification("GET_Fail", "GET_TCPUDP");
                        tbReadFailCount.Text = (++ReadFailCount).ToString();
                    }
                }
                Application.DoEvents();

                if (CustGetDialog.check_Time.Checked)//&& CustGetDialog.check_Time.Visible)
                {
                    try
                    {
                        GET_Clock_Only();
                        Notifier = new Notification("GET_OK", "GET_Clock_Only");
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "Time \r\n";
                        Notifier = new Notification("GET_Fail", "GET_Clock_Only");
                        tbReadFailCount.Text = (++ReadFailCount).ToString();
                    }
                }
                if (CustGetDialog.check_IP_Profile.Checked)
                {
                    try
                    {

                        GET_IPProfile();

                    }
                    catch (Exception)
                    {


                        SabSet = false;
                        Notifier = new Notification("GET_Fail", "IPProfile");
                        tbReadFailCount.Text = (++ReadFailCount).ToString();
                        KharabParams += "Modem Parameters IP Profile\r\n";
                    }
                }
                if (CustGetDialog.chbWakeupProfile.Checked)
                {
                    try
                    {
                        GET_WakeupProfile();

                    }
                    catch (Exception)
                    {

                        SabSet = false;
                        Notifier = new Notification("GET_Fail", "WakeupProfile");
                        tbReadFailCount.Text = (++ReadFailCount).ToString();
                        KharabParams += "Modem Parameters Wakeup Profile\r\n";
                    }
                }

                if (CustGetDialog.chbNumberProfile.Checked)
                {
                    try
                    {

                        GET_NumberProfile();

                    }
                    catch (Exception)
                    {

                        SabSet = false;
                        Notifier = new Notification("GET_Fail", "NumberProfile");
                        tbReadFailCount.Text = (++ReadFailCount).ToString();
                        KharabParams += "Modem Parameters Number Profile\r\n";
                    }
                }
                if (CustGetDialog.chbKeepAlive.Checked)
                {
                    try
                    {
                        GET_KeepAlive();

                    }
                    catch (Exception)
                    {

                        SabSet = false;
                        Notifier = new Notification("GET_Fail", "KeepAlive");
                        tbReadFailCount.Text = (++ReadFailCount).ToString();
                        KharabParams += "Modem Parameters Keep Alive\r\n";
                    }
                }

                if (CustGetDialog.check_StandardModem_KeepAlive.Checked)
                {
                    try
                    {
                        GET_KeepAlive_Standard();

                    }
                    catch (Exception)
                    {

                        SabSet = false;
                        Notifier = new Notification("GET_Fail", "KeepAlive_Standard");
                        tbReadFailCount.Text = (++ReadFailCount).ToString();
                        KharabParams += "Modem Parameters Keep Alive Standard\r\n";
                    }
                }

                if (CustGetDialog.chbModemLimitsAndTime.Checked)
                {
                    try
                    {

                        GET_ModemLimitsAndTime();

                    }
                    catch (Exception)
                    {

                        SabSet = false;
                        Notifier = new Notification("GET_Fail", "ModemLimitsAndTime");
                        tbReadFailCount.Text = (++ReadFailCount).ToString();
                        KharabParams += "Modem Parameters Limits And Time\r\n";
                    }
                }
                if (CustGetDialog.chbModemInitialize.Checked)
                {
                    try
                    {

                        GET_ModemInitialize();

                    }
                    catch (Exception)
                    {

                        SabSet = false;
                        Notifier = new Notification("GET_Fail", "ModemInitialize");
                        tbReadFailCount.Text = (++ReadFailCount).ToString();
                        KharabParams += "Modem Parameters Modem Initialize\r\n";
                    }
                }
                if (CustGetDialog.chbCommunicationProfile.Checked)
                {
                    try
                    {
                        GET_CommunicationProfile();

                    }
                    catch (Exception)
                    {

                        SabSet = false;
                        Notifier = new Notification("GET_Fail", "CommunicationProfile");
                        tbReadFailCount.Text = (++ReadFailCount).ToString();
                        KharabParams += "Modem Parameters Communication Profile\r\n";
                    }
                }
                if (CustGetDialog.check_StandardModem_IP_Profile.Checked)
                {
                    try
                    {

                        GET_StandardIPProfile();

                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "Standard IP Profile \r\n";
                        Notifier = new Notification("GET_Fail", "StandardIPProfile");
                        tbReadFailCount.Text = (++ReadFailCount).ToString();
                    }
                }
                if (CustGetDialog.check_StandardModem_Number_Profile.Checked)
                {
                    try
                    {

                        GET_StandardNumberProfile();

                    }
                    catch (Exception)
                    {

                        SabSet = false;
                        KharabParams += "Standard Number Profile \r\n";
                        Notifier = new Notification("GET_Fail", "StandardNumberProfile");
                        tbReadFailCount.Text = (++ReadFailCount).ToString();
                    }
                }
                if (CustGetDialog.check_TBEs.Checked)//&& CustGetDialog.check_TBEs.Visible)
                {
                    try
                    {
                        //Checks added in v4.8.16
                        bool AnyVisible = false;

                        if (ucTimeWindowParam1.Visible)
                        {
                            AnyVisible = true;
                            GET_Time_Based_Event_1();
                        }
                        if (ucTimeWindowParam2.Visible)
                        {
                            AnyVisible = true;
                            GET_Time_Based_Event_2();
                        }

                        if (AnyVisible)
                        {
                            var _obj_TBE_PowerFail = obj_TBE_PowerFail;
                            Application_Controller.Param_Controller.GET_TBE_PowerFAil(ref _obj_TBE_PowerFail);
                            obj_TBE_PowerFail = _obj_TBE_PowerFail;
                        }
                        Notifier = new Notification("GET_OK", "TBEs");
                    }
                    catch (Exception)
                    {
                        SabSet = false;
                        KharabParams += "Time Based Events \r\n";
                        Notifier = new Notification("GET_Fail", "TBEs");
                        tbReadFailCount.Text = (++ReadFailCount).ToString();
                    }
                }
                Application.DoEvents();

                GetCompleted = true;
            }
            catch (Exception ex)
            {
                GetCompleted = false;
            }

        }

        private void BckWorker_GETParam_T_WorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    throw e.Error;
                }
                else if (e.Cancelled)
                {
                    Notification Notifier = new Notification("Process Aborted", "Parameterization Read Process Is Cancel by User");
                    //MessageBox.Show("Parameterization Read Process Is Cancel by User", "Process Aborted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //try
                    //{
                    //    saveToDatabase_ALL();
                    //}
                    //catch (Exception ex)
                    //{
                    //    Notification notifier = new Notification("Error Saving Meter Configurations", "Error occured while save meter configurations,\r\n" + ex.Message, 5000);
                    //    ///***modification
                    //    Console.Out.WriteLine("Error Saving Configuration " + ex.ToString());
                    //}
                    /////Show Parameters GET Process
                    showToGUI_ALL();
                    Application.DoEvents();

                    String ParamStatistics = null;
                    ParamStatistics = Param_Controller.ParametersGETStatus.BuildParameterReadStatistic();
                    String ParameterReadSummary = Param_Controller.ParametersGETStatus.BuildParamterReadLog(DecodingResult.Ready, false);
                    ///String ParamSummary = Param_Controller.ParametersSETStatus.BuildParamterizationLog(Data_Access_Result.Success, false);
                    ///String MessageContent = String.Format("{0}", ParamSummary);
                    String MessageContent;
                    if (SabSet)
                        MessageContent = String.Format("{0}\r\n\r\nSummary\r\n\r\n{1}",
                            "Parameter Read Process Completed",
                            ParameterReadSummary);
                    else
                        MessageContent = String.Format("{0}\r\n\r\nSummary\r\n\r\n{1}",
                            "Parameter Read Process Completed",
                            "The following parameters were not read successfully \r\n " + KharabParams);

                    Thread.Sleep(250);

                    int copy_text_index = ParameterReadSummary.IndexOf('\r', 0);
                    string msgtoShow = "";
                    if (copy_text_index != -1)
                    {
                        //msgtoShow = ParamSummary.Substring(0, copy_text_index);
                        msgtoShow = "";
                    }
                    else
                    {
                        msgtoShow = ParameterReadSummary;
                    }
                    Notification Notifier;
                    if (GetCompleted && SabSet)
                        Notifier = new Notification("Process Completed", msgtoShow);
                    else
                        Notifier = new Notification("Error", "Error reading parameterization");
                    //dlg.ConnController_ProcessStatusHandler(MessageContent);
                    //dlg.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                String _txt = String.Format("Error occurred while reading Parameters,Details:\r\n{0} {1}",
                    ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : "");
                //MessageBox.Show(_txt, "Error reading Parameters", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressDialog.UpdateDialogStatusHandler(_txt);
                Notification Notifier = new Notification("Error", "Error occurred while reading Parameters");
            }
            finally
            {
                this.Parameterization_BckWorkerThread.DoWork -= new DoWorkEventHandler(BckWorker_GETParams_T_DoEventHandler);
                this.Parameterization_BckWorkerThread.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(BckWorker_GETParam_T_WorkCompleted);
                this.Parameterization_BckWorkerThread.ProgressChanged -= new ProgressChangedEventHandler(BckWorker_GETParam_T_ProgressChanged);
                //Param_Controller.ParameterGetStatus -= dlg.ConnController_ProcessStatusHandler;
                Application_Controller.IsIOBusy = false;
                //dlg.EnableProgressBar = false;
                //dlg.IsAutoHideNow = true;
                Application.DoEvents();
                //Thread.Sleep(100);

                if (is_Set)
                {
                    this.Parameterization_BckWorkerThread.DoWork += new DoWorkEventHandler(BckWorker_SETParams_T_DoEventHandler);
                    this.Parameterization_BckWorkerThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BckWorker_SETParam_T_WorkCompleted);
                    this.Parameterization_BckWorkerThread.ProgressChanged += new ProgressChangedEventHandler(BckWorker_SETParam_T_ProgressChanged);
                    //dlg = new ProgressDialog();

                    //Param_Controller.ParameterSetStatus += dlg.ConnController_ProcessStatusHandler;
                    //dlg.EnableProgressBar = true;
                    //dlg.Text = "Parameterizing";
                    //dlg.DialogTitle = "Setting Parameters";
                    Application_Controller.IsIOBusy = true;
                    Parameterization_BckWorkerThread.RunWorkerAsync();
                    //dlg.ShowDialog(this.Parent);
                    Application.DoEvents();
                }
                else if (is_Get)
                {
                    this.Parameterization_BckWorkerThread.DoWork += new DoWorkEventHandler(BckWorker_GETParams_T_DoEventHandler);
                    this.Parameterization_BckWorkerThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BckWorker_GETParam_T_WorkCompleted);
                    this.Parameterization_BckWorkerThread.ProgressChanged += new ProgressChangedEventHandler(BckWorker_GETParam_T_ProgressChanged);

                    //dlg = new ProgressDialog();

                    //Param_Controller.ParameterGetStatus += dlg.ConnController_ProcessStatusHandler;
                    //dlg.EnableProgressBar = true;
                    //dlg.Text = "Reading Parameters";
                    //dlg.DialogTitle = "Reading Parameters";

                    if (!Parameterization_BckWorkerThread.IsBusy)
                    {
                        Application_Controller.IsIOBusy = true;
                        Parameterization_BckWorkerThread.RunWorkerAsync();
                        ///Disable Meter Passwords
                        //dlg.ShowDialog(this.Parent);
                        Application.DoEvents();
                        //dlg.ConnController_ProcessStatusHandler("Parameterization Read Process is started");
                    }
                }

            }
        }

        private void BckWorker_GETParam_T_ProgressChanged(object sender, ProgressChangedEventArgs e) { }

        private void BckWorker_SETParams_T_DoEventHandler(Object sender, DoWorkEventArgs arg)
        {
            try
            {
                Notification Notifier;
                //dlg.Visible = true;
                Param_Controller.ParametersSETStatus.ResetCommandStatus();
                tbWriteTestCount.Text = (++WriteTestCount).ToString();
                if (true)
                {
                    if (setter.check_ActivityCalender.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_ActivityCalendar();
                        Notifier = new Notification("SET_OK", "SET_ActivityCalendar");

                    }
                    if (setter.check_Clock.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_Clock_Misc();
                        Notifier = new Notification("SET_OK", "SET_Clock_Misc");
                    }
                    Application.DoEvents();

                    if (setter.check_IP_Profile.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;

                        SET_IPProfiles();
                        Notifier = new Notification("SET_OK", "SET_IPProfiles");
                    }
                    if (setter.chbWakeupProfile.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_WakeupProfile();
                        Notifier = new Notification("SET_OK", "SET_WakeupProfile");
                    }
                    if (setter.chbNumberProfile.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;

                        SET_NumberProfiles();
                        Notifier = new Notification("SET_OK", "SET_NumberProfiles");
                    }
                    if (setter.chbKeepAlive.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;

                        SET_KeepAlive();
                        Notifier = new Notification("SET_OK", "SET_KeepAlive");
                    }
                    if (setter.check_StandardModem_KeepAlive.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        Auto_Connect_Mode = ucStandardModem.chkKeepALiveStandard.Checked ? AutoConnectMode.PermanentConnectionAlways : AutoConnectMode.ManualInvokeConnect;
                        SET_KeepAlive_Standard();
                        Notifier = new Notification("SET_OK", "SET_KeepAlive_Standard");
                    }
                    if (setter.chbModemLimitsAndTime.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;

                        SET_ModemLimitsAndTime();
                        Notifier = new Notification("SET_OK", "SET_ModemLimitsAndTime");
                    }
                    if (setter.chbModemInitialize.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_ModemInitialize();
                        Notifier = new Notification("SET_OK", "SET_ModemInitialize");
                    }
                    if (setter.chbCommunicationProfile.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        //UpdateNetworkModes();
                        SET_Communicationprofile();
                        Notifier = new Notification("SET_OK", "SET_Communicationprofile");
                    }

                    if (setter.check_Contactor.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_Contactor();
                        Notifier = new Notification("SET_OK", "SET_Contactor");
                    }
                    if (setter.check_CTPT.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_CTPT();
                        Notifier = new Notification("SET_OK", "SET_CTPT");
                    }
                    if (setter.check_customerReference.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_CustomerReference();

                        Notifier = new Notification("SET_OK", "SET_CustomerReference");
                    }
                    if (setter.check_DataProfilewithEvents.Checked && setter.check_DataProfilewithEvents.Visible)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_DataProfilesWithEvents();
                        Notifier = new Notification("SET_OK", "SET_DataProfilesWithEvents");
                    }
                    if (setter.check_DecimalPoint.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_DecimalPoint();
                        Notifier = new Notification("SET_OK", "SET_DecimalPoint");
                    }
                    Application.DoEvents();
                    if (setter.check_DisplayWindows_Alt.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_DisplayWindows_Alt();
                        Notifier = new Notification("SET_OK", "SET_DisplayWindows_Alt");
                    }
                    if (setter.check_DisplayWindows_Nor.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_DisplayWindows_Nor();
                        Notifier = new Notification("SET_OK", "SET_DisplayWindows_Nor");
                    }
                    if (setter.check_DisplayWindows_test.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_DisplayWindows_Test();
                        Notifier = new Notification("SET_OK", "SET_DisplayWindows_Test");
                    }
                    if (setter.check_DisplayPowerDown.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_DisplayPowerDown();
                        Notifier = new Notification("SET_OK", "SET_DisplayPowerDown");
                    }
                    Application.DoEvents();
                    if (setter.check_EnergyParams.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_EnergyParam();
                        Notifier = new Notification("SET_OK", "SET_EnergyParam");
                    }
                    if (setter.check_IPV4.Checked && setter.check_IPV4.Visible)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_IPV4();
                        Notifier = new Notification("SET_OK", "SET_IPV4");
                    }

                    if (setter.check_Limits.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_Limits();
                        Notifier = new Notification("SET_OK", "SET_Limits");
                    }
                    if (setter.chk_LoadProfile_Interval.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_LoadProfileChannels_Interval(LoadProfileScheme.Load_Profile);
                    }

                    if (setter.chk_LoadProfile_2_Interval.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_LoadProfileChannels_Interval(LoadProfileScheme.Load_Profile_Channel_2);
                    }

                    if (setter.chk_LoadProfile.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_LoadProfileChannels(LoadProfileScheme.Load_Profile);
                    }

                    if (setter.chk_LoadProfile_2.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_LoadProfileChannels(LoadProfileScheme.Load_Profile_Channel_2);
                    }

                    if (setter.chk_PQ_LoadProfileInterval.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_LoadProfileChannels_Interval(LoadProfileScheme.Daily_Load_Profile);
                    }
                    Application.DoEvents();
                    if (setter.check_MajorAlarmprofile.Checked && setter.check_MajorAlarmprofile.Visible)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_MajorAlarmProfile();
                        Notifier = new Notification("SET_OK", "SET_MajorAlarmProfile");
                    }
                    if (setter.check_MDI_params.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_MDIParams();
                        Notifier = new Notification("SET_OK", "SET_MDIParams");
                    }
                    if (setter.check_MonitoringTime.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_MonitoringTime();
                        Notifier = new Notification("SET_OK", "SET_MonitoringTime");
                    }
                    if (setter.chk_GPP.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_GeneralProcess();
                        Notifier = new Notification("SET_OK", "SET_GeneralProcess");
                    }
                    Application.DoEvents();
                    if (setter.check_TCPUDP.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_TCPUDP();
                        Notifier = new Notification("SET_OK", "SET_TCPUDP");
                    }
                    //if (setter.check_Passwords.Checked)
                    //{
                    //    if (Parameterization_BckWorkerThread.CancellationPending)
                    //        arg.Cancel = true;
                    //    Set_Password_Managerial();
                    //}
                    if (setter.check_Password_Elec.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        Set_Password_Electrical();
                        Notifier = new Notification("SET_OK", "Set_Password_Electrical");
                    }
                    if (setter.check_Time.Checked)
                    {
                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        SET_Clock();
                        Notifier = new Notification("SET_OK", "SET_Clock");
                    }
                    Application.DoEvents();
                    if (setter.check_TBEs.Checked)
                    {

                        if (Parameterization_BckWorkerThread.CancellationPending)
                            arg.Cancel = true;
                        if (ucTimeWindowParam1.Visible && ucTimeWindowParam1.Enabled) //Check added in v4.8.16
                        {
                            SET_Time_Based_Event_1();
                            Notifier = new Notification("SET_OK", "SET_Time_Based_Event_1");
                        }
                        if (ucTimeWindowParam2.Visible && ucTimeWindowParam2.Enabled) //Check added in v4.8.16
                        {
                            SET_Time_Based_Event_2();
                            Notifier = new Notification("SET_OK", "SET_Time_Based_Event_2");
                        }
                        //Check added in v4.8.16
                        if ((check_TBE1_PowerFail.Visible && check_TBE1_PowerFail.Enabled) || (check_TBE2_PowerFail.Visible && check_TBE2_PowerFail.Enabled))
                        {
                            Data_Access_Result d = Application_Controller.Param_Controller.SET_TBE_PowerFAil(obj_TBE_PowerFail);
                            Notifier = new Notification("SET_OK", "SET_TBE_PowerFAil");
                        }
                    }
                }
                //else return;
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                tbWriteFailCount.Text = (++WriteFailCount).ToString();
                throw ex;
            }
        }

        private void BckWorker_SETParam_T_WorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                    throw e.Error;
                else if (e.Cancelled)
                {
                    Notification Notifier = new Notification("Process Aborted", "Parameterization Process Is Cancel by User");
                }
                else
                {
                    String ParamStatistics = null;
                    ParamStatistics = Param_Controller.ParametersSETStatus.BuildParameterizationStatistic();
                    String ParamSummary = Param_Controller.ParametersSETStatus.BuildParamterizationLog(Data_Access_Result.Success, false);
                    int copy_text_index = ParamSummary.IndexOf('\r', 0);
                    string msgtoShow = "";
                    if (copy_text_index != -1)
                    {
                        msgtoShow = "Parameterization InComplete";
                    }
                    else
                    {
                        msgtoShow = ParamSummary;
                    }
                    String MessageContent = String.Format("{0}", ParamSummary);
                    Thread.Sleep(250);
                    Notification Notifier = new Notification("Process Completed", msgtoShow);
                    //dlg.ConnController_ProcessStatusHandler(MessageContent);
                    //dlg.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                String _txt = String.Format("Error occurred while setting Parameters,Details:\r\n{0} {1}",
                    ex.Message, (ex.InnerException != null) ? ex.InnerException.Message : "");
                Notification Notifier = new Notification("Error ", _txt);
            }
            finally
            {
                this.Parameterization_BckWorkerThread.DoWork -= new DoWorkEventHandler(BckWorker_SETParams_T_DoEventHandler);
                this.Parameterization_BckWorkerThread.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(BckWorker_SETParam_T_WorkCompleted);
                this.Parameterization_BckWorkerThread.ProgressChanged -= new ProgressChangedEventHandler(BckWorker_SETParam_T_ProgressChanged);
                //Param_Controller.ParameterSetStatus -= dlg.ConnController_ProcessStatusHandler;
                //dlg.EnableProgressBar = false;
                Application_Controller.IsIOBusy = false;
                //dlg.IsAutoHideNow = true;
                //Thread.Sleep(250);
                Application.DoEvents();
                if (is_Get)
                {
                    this.Parameterization_BckWorkerThread.DoWork += new DoWorkEventHandler(BckWorker_GETParams_T_DoEventHandler);
                    this.Parameterization_BckWorkerThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BckWorker_GETParam_T_WorkCompleted);
                    this.Parameterization_BckWorkerThread.ProgressChanged += new ProgressChangedEventHandler(BckWorker_GETParam_T_ProgressChanged);

                    //dlg = new ProgressDialog();

                    //Param_Controller.ParameterGetStatus += dlg.ConnController_ProcessStatusHandler;
                    //dlg.EnableProgressBar = true;
                    //dlg.Text = "Reading Parameters";
                    //dlg.DialogTitle = "Reading Parameters";

                    if (!Parameterization_BckWorkerThread.IsBusy)
                    {
                        Application_Controller.IsIOBusy = true;
                        Parameterization_BckWorkerThread.RunWorkerAsync();
                        ///Disable Meter Passwords
                        //dlg.ShowDialog(this.Parent);
                        Application.DoEvents();
                        //dlg.ConnController_ProcessStatusHandler("Parameterization Read Process is started");
                    }
                }
                else if (is_Set)
                {
                    this.Parameterization_BckWorkerThread.DoWork += new DoWorkEventHandler(BckWorker_SETParams_T_DoEventHandler);
                    this.Parameterization_BckWorkerThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BckWorker_SETParam_T_WorkCompleted);
                    this.Parameterization_BckWorkerThread.ProgressChanged += new ProgressChangedEventHandler(BckWorker_SETParam_T_ProgressChanged);
                    //dlg = new ProgressDialog();

                    //Param_Controller.ParameterSetStatus += dlg.ConnController_ProcessStatusHandler;
                    //dlg.EnableProgressBar = true;
                    //dlg.Text = "Parameterizing";
                    //dlg.DialogTitle = "Setting Parameters";
                    Application_Controller.IsIOBusy = true;
                    Parameterization_BckWorkerThread.RunWorkerAsync();
                    //dlg.ShowDialog(this.Parent);
                    Application.DoEvents();
                }
            }
        }

        private void BckWorker_SETParam_T_ProgressChanged(object sender, ProgressChangedEventArgs e)
        { }

        #endregion Param Read Write Test

        #region Check Fields in UserRights to show in Reports
        //public bool ApplyAccessRights(List<AccessRights> Rights)
        //{
        //    bool AnyReadOrWrite = false;
        //    bool isAnyFlagchecked = false;
        //    try
        //    {
        //        this.SuspendLayout();

        //        if (Rights.Find(x => x.Read == true || x.Write == true) != null)
        //        {

        //            foreach (var item in Rights)
        //            {
        //                /////////////////////////////
        //                bool read = item.Read, write = item.Write;
        //                switch ((MDIParameters)Enum.Parse(item.QuantityType, item.QuantityName))
        //                {
        //                    case MDIParameters.MinimumTimeIntervalBetweenManualReset:
        //                        txt_MDIParams_minTime.Enabled = write;
        //                        radio_MDI_d.Enabled = write;
        //                        radio_MDI_h.Enabled = write;
        //                        radio_MDI_min.Enabled = write;
        //                        radio_MDI_s.Enabled = write;
        //                        pnl_Manual_Reset.Visible = read;
        //                        break;

        //                    case MDIParameters.ManualResetByButtonflag:
        //                        check_MDIParams_ManualResetByButton.Enabled = write;
        //                        check_MDIParams_ManualResetByButton.Visible = read;
        //                        break;
        //                    case MDIParameters.ManualResetByRemoteCommandflag:
        //                        check_MDIParams_ManualResetByRemote.Enabled = write;
        //                        check_MDIParams_ManualResetByRemote.Visible = read;
        //                        break;
        //                    case MDIParameters.ManualResetinPowerDownModeflag:
        //                        check_MDIParams_Manual_Reset_powerDown_Mode.Enabled = write;
        //                        check_MDIParams_Manual_Reset_powerDown_Mode.Visible = read;
        //                        break;
        //                    case MDIParameters.DisableAutoResetinPowerDownModeflag:
        //                        check_MDIParams_Auto_Reset_powerDown_Mode.Enabled = write;
        //                        check_MDIParams_Auto_Reset_powerDown_Mode.Visible = read;
        //                        break;
        //                    case MDIParameters.AutoResetEnableflag:
        //                        check_MDIParams_Autoreset.Enabled = write;
        //                        check_MDIParams_Autoreset.Visible = read;
        //                        break;

        //                    case MDIParameters.MDIAutoResetDataTime:
        //                        pnl_AutoResetEnable.Enabled = write;
        //                        pnl_AutoResetEnable.Visible = read;

        //                        ucMDIAutoResetDateParam.Enabled = write;
        //                        ucMDIAutoResetDateParam.Visible = read;
        //                        break;

        //                    case MDIParameters.MDInterval:
        //                        combo_MDIParams_MDIInterval.Enabled = write;
        //                        lbl_MDIInterval.Visible = lblMDIInterval.Visible = combo_MDIParams_MDIInterval.Visible = read;
        //                        break;

        //                    case MDIParameters.MDISlidesCount:
        //                        tb_MDIParams_SlidesCount.Enabled = write;
        //                        pnl_SlideCount_Container.Visible = tb_MDIParams_SlidesCount.Visible = read;
        //                        break;

        //                    default:
        //                        break;
        //                }

        //                ///////////////////////////
        //                _HelperAccessRights((MDIParameters)Enum.Parse(item.QuantityType, item.QuantityName), item.Read, item.Write);
        //                if (!AnyReadOrWrite)
        //                    AnyReadOrWrite = (item.Read || item.Write);
        //                if (!isAnyFlagchecked && !String.IsNullOrEmpty(item.QuantityName) && item.QuantityName.EndsWith("flag", StringComparison.OrdinalIgnoreCase))
        //                {
        //                    isAnyFlagchecked = (item.Read || item.Write);
        //                }
        //            }
        //            if (!AnyReadOrWrite)
        //                return AnyReadOrWrite;
        //            #region Make Manual_Reset GP_Invisible here

        //            var Right_IntervalManualReset = Rights.Find((x) => x.QuantityType == typeof(MDIParameters) &&
        //                                String.Equals(x.QuantityName, MDIParameters.MinimumTimeIntervalBetweenManualReset.ToString()));
        //            if (Right_IntervalManualReset != null &&
        //               !(Right_IntervalManualReset.Read || Right_IntervalManualReset.Write) && !isAnyFlagchecked)
        //            {
        //                this.gpMDIManualReset.Visible = false;
        //                this.gpMDIAutoReset.Margin = this.gpMDIManualReset.Margin;
        //            }
        //            else
        //            {
        //                this.gpMDIManualReset.Visible = true;
        //            }

        //            #endregion
        //            #region Make Auto Reset GP_Invisible here

        //            bool Auto_reset = false;
        //            foreach (var item in Rights)
        //            {
        //                if (item == Right_IntervalManualReset || item.QuantityName.EndsWith("flag", StringComparison.OrdinalIgnoreCase))
        //                    continue;
        //                else
        //                {
        //                    if (!Auto_reset)
        //                        Auto_reset = (item.Read || item.Write);
        //                }
        //            }
        //            if (!Auto_reset)
        //                gpMDIAutoReset.Visible = false;
        //            else
        //                gpMDIAutoReset.Visible = true;

        //            #endregion
        //            return AnyReadOrWrite;
        //        }
        //        return false;
        //    }
        //    finally
        //    {
        //        this.ResumeLayout();
        //    }
        //}

        //private void _HelperAccessRights(MDIParameters qty, bool read, bool write)
        //{
        //    switch (qty)
        //    {
        //        case MDIParameters.MinimumTimeIntervalBetweenManualReset:

        //            txt_MDIParams_minTime.Enabled = write;

        //            radio_MDI_d.Enabled = write;
        //            radio_MDI_h.Enabled = write;
        //            radio_MDI_min.Enabled = write;
        //            radio_MDI_s.Enabled = write;
        //            pnl_Manual_Reset.Visible = read;
        //            break;

        //        case MDIParameters.ManualResetByButtonflag:
        //            check_MDIParams_ManualResetByButton.Enabled = write;
        //            check_MDIParams_ManualResetByButton.Visible = read;
        //            break;
        //        case MDIParameters.ManualResetByRemoteCommandflag:
        //            check_MDIParams_ManualResetByRemote.Enabled = write;
        //            check_MDIParams_ManualResetByRemote.Visible = read;
        //            break;
        //        case MDIParameters.ManualResetinPowerDownModeflag:
        //            check_MDIParams_Manual_Reset_powerDown_Mode.Enabled = write;
        //            check_MDIParams_Manual_Reset_powerDown_Mode.Visible = read;
        //            break;
        //        case MDIParameters.DisableAutoResetinPowerDownModeflag:
        //            check_MDIParams_Auto_Reset_powerDown_Mode.Enabled = write;
        //            check_MDIParams_Auto_Reset_powerDown_Mode.Visible = read;
        //            break;
        //        case MDIParameters.AutoResetEnableflag:
        //            check_MDIParams_Autoreset.Enabled = write;
        //            check_MDIParams_Autoreset.Visible = read;
        //            break;

        //        case MDIParameters.MDIAutoResetDataTime:
        //            pnl_AutoResetEnable.Enabled = write;
        //            pnl_AutoResetEnable.Visible = read;

        //            ucMDIAutoResetDateParam.Enabled = write;
        //            ucMDIAutoResetDateParam.Visible = read;
        //            break;

        //        case MDIParameters.MDInterval:
        //            combo_MDIParams_MDIInterval.Enabled = write;
        //            lbl_MDIInterval.Visible = lblMDIInterval.Visible = combo_MDIParams_MDIInterval.Visible = read;
        //            break;

        //        case MDIParameters.MDISlidesCount:
        //            tb_MDIParams_SlidesCount.Enabled = write;
        //            pnl_SlideCount_Container.Visible = tb_MDIParams_SlidesCount.Visible = read;
        //            break;

        //        default:
        //            break;
        //    }
        //}
        #endregion


        //Flickering Reduction
        protected override CreateParams CreateParams
        {
            get
            {
                var parms = base.CreateParams;
                parms.Style &= ~0x02000000;  // Turn off WS_CLIPCHILDREN
                return parms;
            }
        }
        private void btnGenerateAuthenticationKey_Click(object sender, EventArgs e)
        {
            txtAuthenticationKey.Text = DLMS_Common.ArrayToHexString(LocalCommon.GetRandomOctectString(KeySize)).Replace(" ", string.Empty);
        }

        private void btnGenetrateEncryptionKey_Click(object sender, EventArgs e)
        {
            txtEncryptionKey.Text = DLMS_Common.ArrayToHexString(LocalCommon.GetRandomOctectString(KeySize)).Replace(" ", string.Empty);
        }
        void showToGUI_ALL_StandardModem()
        {

            showToGUI_StandardIPProfile();
            showToGUI_StandardNumberProfile();
            ucStandardModem.chkKeepALiveStandard.Checked = Auto_Connect_Mode == AutoConnectMode.PermanentConnectionAlways;
            //showToGUI_StandardKeepAlive();

        }

        private void ucStandardModem_Load(object sender, EventArgs e)
        {
            ucStandardModem.txtPrimaryTcpPort.TextChanged += txt_StandardIPProfile_WrapperOverTCP_Leave;
            ucStandardModem.txtSecodaryTcpPort.TextChanged += txt_StandardIPProfile_WrapperOverTCP_Leave;
            ucStandardModem.txt3rdTcpPortIpProfile.TextChanged += txt_StandardIPProfile_WrapperOverTCP_Leave;
            ucStandardModem.txt4thTcpPort.TextChanged += txt_StandardIPProfile_WrapperOverTCP_Leave;

            ucStandardModem.txtPrimaryIp.TextChanged += txt_StandardIPProfile_IP_Leave;
            ucStandardModem.txtSecondaryIp.TextChanged += txt_StandardIPProfile_IP_Leave;
            ucStandardModem.txt3rdIp.TextChanged += txt_StandardIPProfile_IP_Leave;
            ucStandardModem.txt4thIpProfile.TextChanged += txt_StandardIPProfile_IP_Leave;

            ucStandardModem.cmb_StandardIPProfileList.SelectedIndexChanged += cmb_StandardIPProfileList_SelectedIndexChanged;
            ucStandardModem.cmbStandardNumberProfile.SelectedIndexChanged += cmbStandardNumberProfile_SelectedIndexChanged;

            ucStandardModem.cmb_StandardIPProfileList.SelectedIndex = (ucStandardModem.cmb_StandardIPProfileList.Items.Count > 0) ? 0 : -1;
            ucStandardModem.cmbStandardNumberProfile.SelectedIndex = (ucStandardModem.cmbStandardNumberProfile.Items.Count > 0) ? 0 : -1;
        }

        private void txt_StandardIPProfile_IP_Leave(object sender, EventArgs e)
        {
            MaskedTextBox[] IPProfiles = { ucStandardModem.txtPrimaryIp, ucStandardModem.txtSecondaryIp, ucStandardModem.txt3rdIp, ucStandardModem.txt4thIpProfile };

            for (int i = 0; i < IPProfiles.Length; i++)
            {
                try
                {
                    Param_Standard_IP_Profiles_object[i].IP = DLMS.DLMS_Common.IPAddressToLong(IPAddress.Parse(IPProfiles[i].Text));
                    IPProfiles[i].ForeColor = Color.Black;
                }
                catch
                {
                    IPProfiles[i].ForeColor = Color.Red;
                }
            }
        }

        void showToGUI_StandardIPProfile()
        {
            Param_Standard_IP_Profiles_object = loadedConfigurationSet.ParamStandardIPProfiles;
            int ID = Param_Standard_IP_Profiles_object.Length;
            ID = ID - 1;    // indexing starts with 0
            ucStandardModem.cmb_StandardIPProfileList.SelectedIndex = (ucStandardModem.cmb_StandardIPProfileList.Items.Count > 0) ? ID : -1;
            if (ID >= 4)
            {
                ucStandardModem.txt4thIpProfile.Text = DLMS.DLMS_Common.LongToIPAddressString(Param_Standard_IP_Profiles_object[3].IP);
                ucStandardModem.txt4thTcpPort.Text = Convert.ToString(Param_Standard_IP_Profiles_object[3].Wrapper_Over_TCP_port);
            }
            if (ID >= 3)
            {
                ucStandardModem.txt3rdIp.Text = DLMS.DLMS_Common.LongToIPAddressString(Param_Standard_IP_Profiles_object[2].IP);
                ucStandardModem.txt3rdTcpPortIpProfile.Text = Convert.ToString(Param_Standard_IP_Profiles_object[2].Wrapper_Over_TCP_port);
            }
            if (ID >= 2)
            {
                ucStandardModem.txtSecondaryIp.Text = DLMS.DLMS_Common.LongToIPAddressString(Param_Standard_IP_Profiles_object[1].IP);
                ucStandardModem.txtSecodaryTcpPort.Text = Convert.ToString(Param_Standard_IP_Profiles_object[1].Wrapper_Over_TCP_port);
            }
            if (ID >= 1)
            {
                ucStandardModem.txtPrimaryIp.Text = DLMS.DLMS_Common.LongToIPAddressString(Param_Standard_IP_Profiles_object[0].IP);
                ucStandardModem.txtPrimaryTcpPort.Text = Convert.ToString(Param_Standard_IP_Profiles_object[0].Wrapper_Over_TCP_port);
            }
        }

        void showToGUI_StandardNumberProfile()
        {
            try
            {
                Param_Standard_Number_Profile_object = loadedConfigurationSet.ParamStandardNumberProfile;
                ucStandardModem.cmbStandardNumberProfile.SelectedIndex = (ucStandardModem.cmbStandardNumberProfile.Items.Count > 0) ? (Param_Standard_Number_Profile_object.Length - 1) : -1;
                for (int i = 0; i < Param_Standard_Number_Profile_object.Length; i++)
                {
                    int ID = i;
                    if (ID == 0)
                        ucStandardModem.txtStandardModemDataCallNo.Text = LocalCommon.ConvertToValidString(Param_Standard_Number_Profile_object[ID].Number);
                    else if (ID == 1)
                        ucStandardModem.txtMdf.Text = LocalCommon.ConvertToValidString(Param_Standard_Number_Profile_object[ID].Number);
                    else if (ID == 2)
                        ucStandardModem.txtNumberProfile3.Text = LocalCommon.ConvertToValidString(Param_Standard_Number_Profile_object[ID].Number);
                    else if (ID == 3)
                        ucStandardModem.txtNumberProfile4.Text = LocalCommon.ConvertToValidString(Param_Standard_Number_Profile_object[ID].Number);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Enter Number Profile Params again if required");
            }
            //NOTE: defined as byte; but CHeckBOX on GUI....some flag conversion expected!!
        }
        private void txt_StandardIPProfile_WrapperOverTCP_Leave(object sender, EventArgs e)
        {
            TextBox[] IPProfiles = { ucStandardModem.txtPrimaryTcpPort, ucStandardModem.txtSecodaryTcpPort, ucStandardModem.txt3rdTcpPortIpProfile, ucStandardModem.txt4thTcpPort };

            for (int i = 0; i < IPProfiles.Length; i++)
            {
                try
                {
                    LocalCommon.TextBox_validation(0, 65536, IPProfiles[i]);
                    Param_Standard_IP_Profiles_object[i].Wrapper_Over_TCP_port = Convert.ToUInt16(IPProfiles[i].Text);
                    IPProfiles[i].ForeColor = Color.Black;
                }
                catch
                {
                    IPProfiles[i].ForeColor = Color.Red;
                }
            }
        }

        private bool SaveNumberProfile(int profilesCount)
        {
            TextBox[] NUmberProfiles = { ucStandardModem.txtStandardModemDataCallNo, ucStandardModem.txtMdf, ucStandardModem.txtNumberProfile3, ucStandardModem.txtNumberProfile4 };
            if (Param_Standard_Number_Profile_object == null || Param_Standard_Number_Profile_object.Length < profilesCount)
                Param_Standard_Number_Profile_object = new Param_Standard_Number_Profile[profilesCount];
            try
            {
                for (int i = 0; i < profilesCount; i++)
                {
                    if (NUmberProfiles[i].Text.Length < 16)
                    {
                        Param_Standard_Number_Profile_object[i].Number = Commons.ConvertFromValidNumberString(NUmberProfiles[i].Text, true);
                        NUmberProfiles[i].ForeColor = Color.Black;
                    }
                    else
                    {
                        NUmberProfiles[i].ForeColor = Color.Red;
                        return false;
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void cmb_StandardIPProfileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ucStandardModem.cmb_StandardIPProfileList.SelectedIndex == 0)
            {
                ucStandardModem.groupBox18.Visible = true;
                ucStandardModem.groupBox48.Visible = false;
                ucStandardModem.groupBox49.Visible = false;
                ucStandardModem.groupBox50.Visible = false;
            }
            else if (ucStandardModem.cmb_StandardIPProfileList.SelectedIndex == 1)
            {
                ucStandardModem.groupBox18.Visible = true;
                ucStandardModem.groupBox48.Visible = true;
                ucStandardModem.groupBox49.Visible = false;
                ucStandardModem.groupBox50.Visible = false;
            }
            else if (ucStandardModem.cmb_StandardIPProfileList.SelectedIndex == 2)
            {
                ucStandardModem.groupBox18.Visible = true;
                ucStandardModem.groupBox48.Visible = true;
                ucStandardModem.groupBox49.Visible = true;
                ucStandardModem.groupBox50.Visible = false;
            }
            else if (ucStandardModem.cmb_StandardIPProfileList.SelectedIndex == 3)
            {
                ucStandardModem.groupBox18.Visible = true;
                ucStandardModem.groupBox48.Visible = true;
                ucStandardModem.groupBox49.Visible = true;
                ucStandardModem.groupBox50.Visible = true;
            }
        }

        private void cmbStandardNumberProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ucStandardModem.cmbStandardNumberProfile.SelectedIndex == 0)
            {
                ucStandardModem.groupBox42.Visible = true;
                ucStandardModem.groupBox40.Visible = false;
                ucStandardModem.groupBox41.Visible = false;
                ucStandardModem.groupBox43.Visible = false;
            }
            else if (ucStandardModem.cmbStandardNumberProfile.SelectedIndex == 1)
            {
                ucStandardModem.groupBox42.Visible = true;
                ucStandardModem.groupBox40.Visible = true;
                ucStandardModem.groupBox41.Visible = false;
                ucStandardModem.groupBox43.Visible = false;
            }
            else if (ucStandardModem.cmbStandardNumberProfile.SelectedIndex == 2)
            {
                ucStandardModem.groupBox42.Visible = true;
                ucStandardModem.groupBox40.Visible = true;
                ucStandardModem.groupBox41.Visible = true;
                ucStandardModem.groupBox43.Visible = false;
            }
            else if (ucStandardModem.cmbStandardNumberProfile.SelectedIndex == 3)
            {
                ucStandardModem.groupBox42.Visible = true;
                ucStandardModem.groupBox40.Visible = true;
                ucStandardModem.groupBox41.Visible = true;
                ucStandardModem.groupBox43.Visible = true;
            }

        }

        private void chbWifiSettings_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chbWifiSettings.Checked)
            {
                this.ucNumberProfile.gpNumberProfile.Enabled = false;
                this.ucCommProfile.check_CommProfile_SMS.Enabled = false;
                this.ucCommProfile.check_CommProfile_TCP.Checked = true;
                this.ucModemInitialize.txt_ModemInitialize_APNString.Enabled = false;
                this.ucModemLimitsAndTime.lbl_ModemLimitsAndTime_Time_Between_Retries_SMS_.Enabled = false;
                this.ucModemLimitsAndTime.txt_ModemLimitsAndTime_TimeBetweenRetriesSMS.Enabled = false;
                this.ucModemLimitsAndTime.lbl_ModemLimitsAndTime_Time_Between_Retries_UDP.Enabled = false;
                this.ucModemLimitsAndTime.txt_ModemLimitsAndTime_TimeBetweenRetriesUDP.Enabled = false;
                this.ucModemLimitsAndTime.lbl_ModemLimitsAndTime_Time_Between_Retries_Data_Call.Enabled = false;
                this.ucModemLimitsAndTime.txt_ModemLimitsAndTime_TimeBetweenRetriesDataCall.Enabled = false;
                this.ucModemLimitsAndTime.label148.Enabled = false;
                this.ucModemLimitsAndTime.label150.Enabled = false;
                this.ucModemLimitsAndTime.label152.Enabled = false;
                this.ucNumberProfile.gpNumberProfile.Enabled = false;
                this.ucModemInitialize.lbl_ModemInitialize_APNString.Enabled = false;
                this.ucModemInitialize.label22.Enabled = false;
                this.ucModemInitialize.lbl_ModemInitialize_PinCode.Enabled = false;

                this.ucModemInitialize.lbl_ModemInitialize_UserName.Text = "Access Point Name";
                this.ucModemInitialize.lbl_ModemInitialize_Password.Text = "Access Point Password";
            }
            else
            {
                this.ucNumberProfile.gpNumberProfile.Enabled = true;
                this.ucCommProfile.check_CommProfile_SMS.Enabled = true;
                this.ucCommProfile.check_CommProfile_SMS.Checked = true;
                this.ucModemInitialize.txt_ModemInitialize_APNString.Enabled = true;
                this.ucModemInitialize.txt_ModemLimitsAndTime_Wakepassword.Enabled = true;
                this.ucModemInitialize.txt_ModemInitialize_PinCode.Enabled = true;
                this.ucModemLimitsAndTime.lbl_ModemLimitsAndTime_Retry_SMS.Enabled = true;
                this.ucModemLimitsAndTime.txt_ModemLimitsAndTime_RetrySMS.Enabled = true;
                this.ucModemLimitsAndTime.lbl_ModemLimitsAndTime_Retry_UDP.Enabled = true;
                this.ucModemLimitsAndTime.txt_ModemLimitsAndTime_RetryUDP.Enabled = true;
                this.ucModemLimitsAndTime.lbl_ModemLimitsAndTime_Retry.Enabled = true;
                this.ucModemLimitsAndTime.txt_ModemLimitsAndTime_Retry.Enabled = true;
                this.ucModemLimitsAndTime.lbl_ModemLimitsAndTime_Time_Between_Retries_SMS_.Enabled = true;
                this.ucModemLimitsAndTime.txt_ModemLimitsAndTime_TimeBetweenRetriesSMS.Enabled = true;
                this.ucModemLimitsAndTime.lbl_ModemLimitsAndTime_Time_Between_Retries_UDP.Enabled = true;
                this.ucModemLimitsAndTime.txt_ModemLimitsAndTime_TimeBetweenRetriesUDP.Enabled = true;
                this.ucModemLimitsAndTime.lbl_ModemLimitsAndTime_Time_Between_Retries_Data_Call.Enabled = true;
                this.ucModemLimitsAndTime.txt_ModemLimitsAndTime_TimeBetweenRetriesDataCall.Enabled = true;
                this.ucModemLimitsAndTime.label148.Enabled = true;
                this.ucModemLimitsAndTime.label150.Enabled = true;
                this.ucModemLimitsAndTime.label152.Enabled = true;
                this.ucNumberProfile.Enabled = true;
                this.ucModemInitialize.lbl_ModemInitialize_APNString.Enabled = true;
                this.ucModemInitialize.label22.Enabled = true;
                this.ucModemInitialize.lbl_ModemInitialize_PinCode.Enabled = true;

                this.ucModemInitialize.lbl_ModemInitialize_UserName.Text = "User Name";
                this.ucModemInitialize.lbl_ModemInitialize_Password.Text = "Password";
            }
        }

        private void ucHDLCSetup_Load(object sender, EventArgs e)
        {
            this.ucHDLCSetup.btnGetParameter.Click += btnGetParameter_Click;
            this.ucHDLCSetup.btnSetDeviceAddress.Click += btnSetDeviceAddress_Click;
            this.ucHDLCSetup.btnSetInactivityTimeOut.Click += btnSetInactivityTimeOut_Click;
        }

        private void ucContactor_Load_1(object sender, EventArgs e)
        {

        }

        private void ucEnergyMizer1_Load(object sender, EventArgs e)
        {

        }

        private void btnSetDeviceAddress_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Application Association");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Data_Access_Result result = Param_Controller.SetHDLCAddress(Convert.ToUInt16(ucHDLCSetup.txtSetHdlcDeviceAddress.Text ?? "0"));
                    Notification notifier = new Notification("Set HDLC Address", result.ToString());
                }
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Set HDLC Address", ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnSetInactivityTimeOut_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Application Association");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Data_Access_Result result = Param_Controller.SetInactivityTime(Convert.ToUInt16(ucHDLCSetup.txtSetInactivityTimeOut.Text ?? "0"));
                    Notification notifier = new Notification("Set Inactivity Time", result.ToString());
                }
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Set Inactivity Time", ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }
        private void btnGetParameter_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Application Association");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Class_23 HDLC_Setup = Param_Controller.GetHDLCSetup();

                    ucHDLCSetup.lblCommSpeed.Text = Enum.GetName(typeof(CommSpeed), HDLC_Setup.Comm_Speed);
                    ucHDLCSetup.lblWinSizeTransmit.Text = HDLC_Setup.WindowSizeTransmit.ToString();
                    ucHDLCSetup.lblWinSizeReceive.Text = HDLC_Setup.WindowSizeReceive.ToString();
                    ucHDLCSetup.lblMaxInfoFieldLengthTransmit.Text = HDLC_Setup.MaxInfoFieldLengthTransmit.ToString();
                    ucHDLCSetup.lblMaxInfoFieldLengthReceive.Text = HDLC_Setup.MaxInfoFieldLengthReceive.ToString();
                    ucHDLCSetup.lblInterOctetTimeOut.Text = HDLC_Setup.InterOctetTimeOut.ToString();
                    ucHDLCSetup.txtSetInactivityTimeOut.Text = HDLC_Setup.InActivityTimeOut.ToString();
                    ucHDLCSetup.txtSetHdlcDeviceAddress.Text = HDLC_Setup.DeviceAddress.ToString();
                    Notification notifier = new Notification("Notification", "Get HDLC Setup Success");
                }
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Notification", "Get HDLC Setup Error" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }
    }
}