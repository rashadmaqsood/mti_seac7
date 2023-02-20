#define Enable_DEBUG_ECHO
#define Enable_Read_Param_Log
#define Enable_Error_Logging
//#define Enable_DEBUG_RunMode
// #define Enable_Transactional_Logging
// #define Enable_LoadTester_Mode
#define Enable_Abstract_Log

using comm;
using comm.DataContainer;
using Communicator.MeterConfiguration;
using Communicator.MTI_MDC;
using Communicator.Properties;
using DatabaseConfiguration.DataBase;
using DatabaseConfiguration.DataSet;
using DatabaseManager.Database;
using DLMS;
using DLMS.Comm;
using LogSystem.Shared.Common;
using LogSystem.Shared.Common.Enums;
using Serenity.Crypto;
using SharedCode.Comm.DataContainer;
using SharedCode.Comm.EventDispatcher.Contracts;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.Param;
using SharedCode.Common;
using SharedCode.Controllers;
using SharedCode.eGeniousDisplayUnit;
using SharedCode.Others;
using SharedCode.TCP_Communication;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Communicator.MeterConnManager
{
    public delegate SAPTable GetSAPTable(IOConnection connInfo);

    public class ApplicationController : IDisposable, INotifyPropertyChanged
    {
        #region Data_Members

        private int _signalStrength = 0;
        public static readonly uint DefaultExceptionLevel = 0;
        public static readonly uint MaxExceptionLevel = 0;
        public static readonly int MaxRequestReset = 10;

        public static readonly TimeSpan DefaultMaxSessionDuration = new TimeSpan(0, 30, 0);
        public static readonly TimeSpan DefaultMinSessionDuration = new TimeSpan(0, 05, 0);
        public static readonly TimeSpan DefaultKeepAliveSleepTime = new TimeSpan(0, 2, 15);

        private ParameterController _Param_Controller;
        protected ApplicationProcess_Controller _AP_Controller;
        private ConnectionController _ConnectionController;
        private BillingController _Billing_Controller;
        private LoadProfileController _LoadProfile_Controller;
        private InstantaneousController _InstantaneousController;
        private EventController _EventController;
        private Configs config;
        private Configurator _Configurator;
        private Task execRunnerThread;
        private CancellationTokenSource _threadCancelToken;
        private GetSAPTable GetAccessRightsDlg;
        private Param_MajorAlarmProfile param_MajorAlarmProfile_obj = null;

        private IOConnection connectToMeter;
        private bool isIOBusy;
        private bool isAllocated = false;
        private DatabaseController _DBController;
        private Statistics _Statistics_Obj;
        protected DateTime _session_DateTime;
        private MeterInformation _MeterInfo_OBJ;
        public MeterInfoUpdateFlags MIUF;
        private const string DateFormat = "yyyy-MM-dd HH:mm:ss";
        private List<GridStatusItem> _GridInputStatus;


        //cumulativeBilling_SinglePhase data_SP;

        private IEventDispatcher _appEventDispatcher = null;
        private IEventPool _appEventPool = null;

        private int timeSync_SecondsThreshold = Settings.Default.timeSync_SecondThreshold;
        private int timeSync_MinuteThreshold = Settings.Default.timeSync_MinuteThreshold;
        private int timeSync_TransmissionOffset_Seconds = Settings.Default.timeSync_transmissionOffset_Seconds;
        private int timeSync_TransmissionOffset_Minutes = Settings.Default.timeSync_transmissionOffset_Minutes;
        private bool EnableInvaldiClockSync = Settings.Default.IsEnableInvalidTimeSync;
        private bool TimeSyncOnBatteryDead = Settings.Default.TimeSyncOnBatteryDead;

        public Parameterization Param_OBJ = null;
        public Communicator.MeterConfiguration.Events Events_OBJ = null;
        public static readonly TimeSpan NKA_DefaultResetSessionDuration = Settings.Default.DefaultResetSessionDurationForNonKeepAliveMeters;
        public static readonly TimeSpan KA_DefaultResetSessionDuration = Settings.Default.DefaultResetSessionDurationForKeepAliveMeters;
        public AssociationState AssociationState_Obj = AssociationState.Logout;
        public List<byte> ServerSystemTitle = null;

        const int MaxLPIterations = 3;
        public const int MaxRetryReadFailure = 02;
        private byte _lastIOFailureCount = 0;


        string debug_error = string.Empty;
        string debug_caution = string.Empty;
        string debug_contactor = string.Empty;

        #endregion

        #region Properties

        public Statistics StatisticsObj
        {
            get { return _Statistics_Obj; }
            set { _Statistics_Obj = value; }
        }

        public ILogWriter ActivityLogger
        {
            get
            {
                if (_AP_Controller != null)
                    return _AP_Controller.ActivityLogger;
                else
                    return null;
            }
            set
            {
                if (_AP_Controller != null)
                    _AP_Controller.ActivityLogger = value;
            }
        }

        public Param_MajorAlarmProfile ParamMajorAlarmProfileObj
        {
            get { return param_MajorAlarmProfile_obj; }
            set { param_MajorAlarmProfile_obj = value; }
        }

        public MeterInformation MeterInfo
        {
            get { return _MeterInfo_OBJ; }
            set { _MeterInfo_OBJ = value; }
        }

        internal DateTime SessionDateTime
        {
            get { return _session_DateTime; }
            set { _session_DateTime = value; }
        }

        public TimeSpan SessionDuration
        {
            get
            {
                try
                {
                    if (SessionDateTime == DateTime.MinValue)
                        return TimeSpan.MinValue;
                    else
                        return DateTime.Now.Subtract(SessionDateTime);
                }
                catch (Exception)
                {
                    return TimeSpan.MinValue;
                }
            }
        }

        public DatabaseController DB_Controller
        {
            get { return _DBController; }
            set { _DBController = value; }
        }

        public CancellationTokenSource ThreadCancelToken
        {
            get { return _threadCancelToken; }
            set { _threadCancelToken = value; }
        }

        public Task ExecRunnerThread
        {
            get { return execRunnerThread; }
            set { execRunnerThread = value; }
        }

        public int CummId
        {
            get;
            set;
        }

        public bool IsIOBusy
        {
            get
            {
                bool t = false;
                lock (this)
                {
                    t = isIOBusy;
                }
                return t;
            }
            set
            {
                lock (this)
                {
                    isIOBusy = value;
                }
                NotifyPropertyChanged("IsIOBusy");
            }
        }

        public IOConnection ConnectToMeter
        {
            get { return connectToMeter; }
            set
            {
                connectToMeter = value;
                NotifyPropertyChanged("ConnectToMeter");
            }
        }

        public bool IsAllocated
        {
            get
            {
                lock (this)
                {
                    return isAllocated;
                }
            }
            set
            {
                lock (this)
                {
                    isAllocated = value;
                }
            }
        }

        public ParameterController Param_Controller
        {
            get { return _Param_Controller; }
        }

        public BillingController Billing_Controller
        {
            get { return _Billing_Controller; }
            set { _Billing_Controller = value; }
        }

        public LoadProfileController LoadProfile_Controller
        {
            get { return _LoadProfile_Controller; }
            set { _LoadProfile_Controller = value; }
        }

        public ApplicationProcess_Controller Applicationprocess_Controller
        {
            get { return _AP_Controller; }

        }

        public ConnectionController ConnectionController
        {
            get { return _ConnectionController; }
            set { _ConnectionController = value; }
        }

        public EventController Event_Controller
        {
            get { return _EventController; }
            set { _EventController = value; }
        }

        internal InstantaneousController InstantaneousController
        {
            get { return _InstantaneousController; }
            set { _InstantaneousController = value; }
        }

        public IEventDispatcher ApplicationEventDispatcher
        {
            get { return _appEventDispatcher; }
            set { _appEventDispatcher = value; }
        }

        public IEventPool ApplicationEventPool
        {
            get { return _appEventPool; }
            set { _appEventPool = value; }
        }

        public GetSAPTable GetAccessRightsDelegate
        {
            get { return GetAccessRightsDlg; }
            set { GetAccessRightsDlg = value; }
        }

        public InitHDLCParam HDLCInitParameters
        {
            get
            {
                InitHDLCParam _hdlcParams = null;
                InitParamsHelper param_Helper = new InitParamsHelper();
                try
                {

                    _hdlcParams = param_Helper.LoadHDLCParams();
                }
                catch (Exception)
                {
                    _hdlcParams = param_Helper.GetDefaultHDLCParams();
                }

                return _hdlcParams;
            }
        }

        public Configs Configurations
        {
            get
            {
                try
                {
                    // String DefaultURLPath = String.Format(@"{0}\Configs.dat", 
                    // SmartDebugUtility.Common.Commons.GetApplicationConfigsDirectory());
                    // LoadConfigurations(DefaultURLPath,config);
                    // LoadConfiguration(config);
                    return config;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            set
            {
                config = value;
            }
        }

        public Configurator Configurator
        {
            get { return _Configurator; }
            set { _Configurator = value; }
        }

        public MDCEventsClass MDCAlarm { get; set; }

        public BitArray PermissionToWriteParams { get; set; }

        public byte LastIOFailureCount
        {
            get { return _lastIOFailureCount; }
            set { _lastIOFailureCount = value; }
        }

        public void LoadConfiguration(Configs configDataSet)
        {
            try
            {
                string dsn = string.Format("Dsn={0}", DatabaseManager.Properties.Settings.Default.MDC_DSN);
                MDC_DBAccessLayer DBDAO = new MDC_DBAccessLayer(dsn);
                DBDAO.Load_All_Configurations(ref configDataSet);

                // Configuration Based On MeterInfo Id
                Configs.ConfigurationRow DefaultConfig = null;
                if (config.Configuration != null &&
                    config.Configuration.Count > 0)
                {
                    DefaultConfig = config.Configuration[0];
                }

                config.Configuration.CurrentConfiguration = DefaultConfig;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadConfiguration()
        {
            try
            {
                LoadConfiguration(config);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveConfigurations(Configs ToSave)
        {
            try
            {
                string dsn = string.Format("Dsn={0}", DatabaseManager.Properties.Settings.Default.MDC_DSN);
                MDC_DBAccessLayer DBDAO = new MDC_DBAccessLayer(dsn);
                DBDAO.Load_All_Configurations(ref ToSave);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Constructor

        public ApplicationController()
        {
            config = new Configs();
            this.ConnectToMeter = null;
            _AP_Controller = new ApplicationProcess_Controller();
            _Param_Controller = new ParameterController();
            _ConnectionController = new ConnectionController();
            _Billing_Controller = new BillingController();
            _LoadProfile_Controller = new LoadProfileController();
            _EventController = ObjectFactory.GetEventControllerObject();// new EventController();
            _InstantaneousController = new InstantaneousController();
            _GridInputStatus = new List<GridStatusItem>();

            Param_Controller.AP_Controller = Applicationprocess_Controller;
            Billing_Controller.AP_Controller = Applicationprocess_Controller;
            LoadProfile_Controller.AP_Controller = Applicationprocess_Controller;
            ConnectionController.AP_Controller = Applicationprocess_Controller;
            _InstantaneousController.AP_Controller = Applicationprocess_Controller;
            _EventController.AP_Controller = Applicationprocess_Controller;
            _EventController.ParentContainer = this;

            _EventController.MajorAlarmEventDispatcher = ApplicationEventDispatcher;
            _EventController.MajorAlarmEventPool = ApplicationEventPool;
            var EventHandler = new Event_Handler_EventNotify(_EventController.EventNotification_Recieved);
            _AP_Controller.EventNotify += EventHandler;

            Param_OBJ = new Parameterization();
            Param_OBJ.Param_Controller = Param_Controller;
            Param_OBJ.LoadProfile_Controller = LoadProfile_Controller;
            // Param_OBJ.Init_LoadProfilesChannelsQuantities();
            Events_OBJ = new Communicator.MeterConfiguration.Events();
            Events_OBJ.Param_Controller = Param_Controller;

            MIUF = new MeterInfoUpdateFlags();
            // added by furqan 28-11-2014
            MDCAlarm = new MDCEventsClass();
            ServerSystemTitle = new List<byte>();

            _lastIOFailureCount = 0;
        }

        #endregion

        #region Member functions

        public CustomException RunApplicationLogic()
        {
            #region Temp Members
            var CusExc_ProcessRequest = new CustomException();
            CancellationTokenSource SessionResetCancelTK = null;
            bool IsProcessComplete = false;
            int Reset_Counts = 1;
            TimeSpan sessionResetDuration = DefaultMaxSessionDuration;
            string MSN = MeterInfo.MSN;
            //CompleteRequest = new ScheduledRequest();
            #endregion
            try
            {
                #region Initial work

                IOConnection IOConn = ConnectionController.CurrentConnection;
                SessionDateTime = DateTime.Now;
                #region // If Task Canceled

                if (ThreadCancelToken != null && _threadCancelToken.IsCancellationRequested)
                {
                    ThreadCancelToken.Token.ThrowIfCancellationRequested();
                }

                #endregion

                if (IOConn == null || IOConn.ConnectionInfo == null
                    || IOConn.MeterSerialNumberObj == null) // || !IOConn.MeterSerialNumberObj.IsMSN_Valid)
                {
                    throw new Exception("Error Occurred while processing,IO Connection Object is not initialized Properly");
                }
                // Start logging for msn
                StatisticsObj.InitLogging(MeterInfo.MSN, IOConn);
                #region Insert Incoming Connection To Log
#if Enable_Abstract_Log

                StatisticsObj.DP_Logger.InsertLog("IIP_" + execRunnerThread.Id, IOConn.IOStream.ToString(), IOConn.ConnectionTime.AddSeconds(1));
                StatisticsObj.DP_Logger.InsertLog("MSN", ConnectToMeter.MeterSerialNumberObj.ToString(), IOConn.ConnectionTime.AddSeconds(1));
                StatisticsObj.DP_Logger.InsertLog("CummId", CummId.ToString(), IOConn.ConnectionTime.AddSeconds(1));
#endif

#if !Enable_Abstract_Log
                 StatisticsObj.DP_Logger.InsertLog("Incoming Connection",IOConn.IOStream.ToString(), IOConn.ConnectionTime.AddSeconds(1));
				 // StatisticsObj.DP_Logger.InsertLog(string.Format("{0,21} ..... {1,10}: {2}", IOConn.ConnectionTime.AddSeconds(1), "Incoming Connection", IOConn.IOStream.ToString()),false);
#endif
                #endregion

                DeviceAssociation Association_Details = null;

                if (connectToMeter.ConnectionInfo.IsInitialized)
                    Association_Details = connectToMeter.ConnectionInfo.MeterInfo.Device_Association;

                if (Association_Details == null || (Association_Details.Id <= 0 || Association_Details.Id > 150))
                    throw new ArgumentNullException("Association Detail Id");

                // Re-Initialize
                #endregion

                do
                {
                    // No need to change the session, 
                    // only needed on first iteration with reset session
                    if (Reset_Counts > 1)
                        SessionDateTime = DateTime.Now;

                    LLCProtocolType Proto_Type = LLCProtocolType.TCP_Wrapper;
                    if (MeterInfo != null &&
                        MeterInfo.LLC_Protocol_Type != LLCProtocolType.Not_Assigned)
                    {
                        Proto_Type = MeterInfo.LLC_Protocol_Type;
                    }

                    bool isAssociated = false;

                    if (Association_Details.AuthenticationType <= HLS_Mechanism.LowSec)
                    {
                        #region Private Login

                        try
                        {
                            isAssociated = LoginMeterConnection(Association_Details.AuthenticationType, Proto_Type);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(string.Format("Error in Private Login (Error Code:{0})", (int)MDCErrors.App_Private_Login), ex);
                        }

                        #endregion
                    }
                    else if (Association_Details.AuthenticationType >= HLS_Mechanism.HLS_Manufac)
                    {
                        #region Initialize_Security_Data

                        ApplicationProcess_Controller AP_Controller = null;
                        AP_Controller = Applicationprocess_Controller;

                        byte[] Client_AP_Title = DLMS_Common.PrintableStringToByteArray(Settings.Default.ApplicationTitle);
                        AP_Controller.Client_ApplicationTitle = Client_AP_Title;
                        MeterInfo.ObjSecurityData.SystemTitle = new List<byte>(Client_AP_Title);

                        // Authentication Process
                        if (AP_Controller.Crypto == null ||
                            AP_Controller.Crypto.GetType() != typeof(AESGCM))
                        {
                            AP_Controller.Crypto = new AESGCM();

                            ((AESGCM)AP_Controller.Crypto).KeySize = 128;
                            ((AESGCM)AP_Controller.Crypto).GMacBitSize = 96;
                        }

                        // Update Security Data
                        AP_Controller.Security_Data = this.Applicationprocess_Controller.Security_Data = MeterInfo.ObjSecurityData;

                        #endregion
                        #region Authentication_HLS

                        try
                        {
                            // Update Security Data
                            // AuthenticateMeterConnection_HLS(Association_Details);
                            isAssociated = LoginMeterConnection(Association_Details.AuthenticationType, Proto_Type);
                        }
                        catch
                        {
                            throw;
                        }

                        #endregion
                    }

                    #region Association Failure Response

                    if (!isAssociated)
                    {
                        string[] PrivateLogging_FailureResponse = new string[] { "Private Login Failure", "PVLI" };
                        string[] PublicLogging_FailureResponse = new string[] { "Public Login Failure", "PBLI" };
                        string[] HLS_FailureResponse = new string[] { "HLS Login Failure", "HLSI" };

                        string error_message = string.Empty;

                        if (Association_Details.AuthenticationType == HLS_Mechanism.LowestSec)
                        {
                            LogMessage(PublicLogging_FailureResponse[0], PublicLogging_FailureResponse[1], "F", 1);
                            error_message = PublicLogging_FailureResponse[0];
                        }
                        else if (Association_Details.AuthenticationType == HLS_Mechanism.LowSec)
                        {
                            LogMessage(PrivateLogging_FailureResponse[0], PrivateLogging_FailureResponse[1], "F", 1);
                            error_message = PrivateLogging_FailureResponse[0];
                        }
                        else if (Association_Details.AuthenticationType >= HLS_Mechanism.HLS_Manufac)
                        {
                            LogMessage(HLS_FailureResponse[0], HLS_FailureResponse[1], "F", 1);
                            error_message = HLS_FailureResponse[0];
                        }

                        throw new Exception(error_message);
                    }

                    #endregion
                    #region Validate_Gateway Linked Meter Serial Number

                    if (IOConn.CurrentDeviceType == DeviceType.GateWay &&
                        MeterInformation.Validate_MSN)
                    {
                        LogMessage("Validating MSN", "VGM", "R", 1);
                        var MeterSerialNumberObject = ConnectionController.GetMeterSerialNumber();
                        // MSN Not Validated
                        if (MeterSerialNumberObject == null || !MeterSerialNumberObject.IsMSN_Valid ||
                            IOConn.MeterSerialNumberObj.MSN != MeterSerialNumberObject.MSN)
                        {
                            LogMessage("Validation MSN", "VGM", "F", 1);
                            throw new ArgumentException(string.Format("The MSN {0} not Validated for Linked Metering Device", MeterSerialNumberObject),
                                                        "MSN_Validation_Fails");
                        }
                        else
                            LogMessage("Validation MSN", "VGM", "S", 1);
                    }

                    #endregion

                    // Test Delay
                    // Check HDLC Inactivity Logic
                    // Commons.Delay(TimeSpan.FromSeconds(120));

                    #region Configuration Test Code Region
                    #region Read Load Profile Channel Info

                    // try
                    // {
                    //     long? LP_GP_ID = -1;
                    //     var LP_Counters = new Profile_Counter();
                    //     LP_Counters.Current_Counter = int.MaxValue;
                    //    LP_Counters.Previous_Counter = (uint)MeterInfo.LP_Counters.DB_Counter;
                    //     LP_Counters.Max_Size = (uint)Limits.Max_LoadProfile_Count_Limit;
                    //     LP_Counters.GroupId = MeterInfo.Counter_Obj.LoadProfile_GroupID;
                    //  
                    //     List<LoadProfileChannelInfo> _loadProfileChannelsInfo = LoadProfile_Controller.GetChannelsInfoList(LP_Counters);
                    //     Configurator.GetMeterGroupByLoadProfileChannels(IOConn.ConnectionInfo, _loadProfileChannelsInfo, out LP_GP_ID);
                    // }
                    // catch (Exception ex)
                    // {
                    //     Commons.WriteLine(String.Format("Error Read Load Profile Channels Info " + ex.Message, IOConn.MSN));
                    // }

                    #endregion
                    #endregion

                    #region Watcher Check

                    string Model = ConnectToMeter.MeterSerialNumberObj.GetMeterModel();
                    if (string.Equals(Model, "W275@", StringComparison.OrdinalIgnoreCase))
                    {
                        IsProcessComplete = true;
                        goto LOGOUT;
                    }

                    #endregion
                    if (ConnectionController.IsConnected)
                    {
                        #region Initial Work
                        // Initial Session Reset CancelTK 
                        SessionResetCancelTK = new CancellationTokenSource();

                        SAPTable sapTb = GetAccessRightsDelegate.Invoke(IOConn);
                        Applicationprocess_Controller.ApplicationProcessSAPTable = sapTb;

                        #endregion

                        #region Limit Features
                        MeterInfo.Read_AR = false;
                        MeterInfo.Read_EV = true;
                        MeterInfo.Read_CB =  READ_METHOD.Disabled;
                        MeterInfo.Read_PQ = false;
                        MeterInfo.Read_LP = READ_METHOD.ByDateTime;
                        MeterInfo.Read_LP2 = READ_METHOD.ByDateTime;
                        MeterInfo.Read_LP3 = READ_METHOD.ByDateTime;
                        MeterInfo.Read_MB = READ_METHOD.ByCounter;
                        MeterInfo.ReadPlan.Clear();
                        MeterInfo.ReadPlan.Add(Schedules.Events);
                        MeterInfo.ReadPlan.Add(Schedules.CumulativeBilling);
                        MeterInfo.ReadPlan.Add(Schedules.PowerQuantities);
                        MeterInfo.ReadPlan.Add(Schedules.LoadProfile);
                        MeterInfo.ReadPlan.Add(Schedules.DailyLoadProfile);
                        MeterInfo.ReadPlan.Add(Schedules.LoadProfile2);
                        //MeterInfo.ReadPlan.Add(Schedules.MonthlyBilling);
                        MeterInfo.Schedule_CB.SchType = ScheduleType.EveryTime;
                        MeterInfo.Schedule_EV.SchType = ScheduleType.EveryTime;
                        MeterInfo.Schedule_LP.SchType = ScheduleType.EveryTime;
                        MeterInfo.Schedule_LP2.SchType = ScheduleType.EveryTime;
                        MeterInfo.Schedule_LP3.SchType = ScheduleType.EveryTime;
                        MeterInfo.Schedule_MB.SchType = ScheduleType.EveryTime;
                        MeterInfo.EnableLiveUpdate = false;
                        #endregion

                        #region // If Task Canceled

                        if (ThreadCancelToken != null && _threadCancelToken.IsCancellationRequested)
                        {
                            ThreadCancelToken.Token.ThrowIfCancellationRequested();
                        }

                        #endregion
                        #region Alarm Register

                        try
                        {
                            if (MeterInfo.Read_AR)
                            {
                                #region Reading Alarm Register
#if Enable_Abstract_Log
                                LogMessage("Reading Alarm Register", "AR", "R", 0);
#endif

#if !Enable_Abstract_Log
						        LogMessage("Reading Alarm Register",1);
#endif
                                #endregion
                                ParamMajorAlarmProfileObj = ReadAlarmRegister();
                                #region Alarm Register Read completed
#if Enable_Abstract_Log
                                LogMessage("Alarm Register Read completed", "AR", "S", 1);
#endif
#if !Enable_Abstract_Log
						        LogMessage("Alarm Register read completed",1);
#endif
                                #endregion
                                #region Send Alarms response
                                if (MeterInfo.SendAlarmsResponse && ParamMajorAlarmProfileObj != null)
                                {
                                    using (Param_MajorAlarmProfile AlarmProfile = (Param_MajorAlarmProfile)ParamMajorAlarmProfileObj.Clone())
                                    {
                                        bool IsAnyStatusToReset = false;
                                        foreach (var energymizerStatus in DLMS_Common.GetValues<EnergyMizerAlarmStatus>())
                                        {
                                            if (energymizerStatus == EnergyMizerAlarmStatus.Normal)
                                                continue;
                                            IsAnyStatusToReset = false;
                                            string ResponseRows = _DBController.GetAlarmUserResponse(MeterInfo.MSN, AlarmProfile, (byte)energymizerStatus);
                                            foreach (MajorAlarm alarm in AlarmProfile.AlarmItems)
                                            {
                                                if (alarm != null && alarm.Info != null && alarm.IsResetUserStatus == energymizerStatus)
                                                    IsAnyStatusToReset = true;
                                            }
                                            if (IsAnyStatusToReset)
                                            {
                                                LogMessage("Sending User Response", "ARR:" + energymizerStatus, "R");
                                                Data_Access_Result status = _Param_Controller.SET_MajorAlarmProfile_UserStatus(AlarmProfile, energymizerStatus);
                                                if (status == Data_Access_Result.Success)
                                                {
                                                    LogMessage("Sending User Response", "ARR:" + (byte)energymizerStatus, "S");
                                                    DB_Controller.SaveUserAlarmResponseLogandDelete(ResponseRows, SessionDateTime);
                                                }
                                                else
                                                {
                                                    LogMessage("Sending User Response", "ARR:" + (byte)energymizerStatus, "F:" + status);
                                                }
                                            }
                                        }

                                    }
                                }
                                #endregion
                                // Save to DB
                                // if (MeterInfo.Save_AR)
                                // {
                                //     bool save_Flag = DB_Controller.SaveAlarmStatus(MSN, MeterInfo.Reference_no, SessionDateTime, ParamMajorAlarmProfileObj.MA_Status_Array);
                                //     if (save_Flag)
                                //     {
                                //         LogMessage("Alarm Status saved to DB", 2);
                                //     }
                                //     else
                                //         LogMessage("Error while saving Alarm Status to DB", 2);
                                // }
                            }
                        }
                        catch (Exception ex)
                        {
                            if (!(ex is NullReferenceException))
                                throw ex;
                        }

                        #endregion

                        #region // If Task Canceled
                        if (ThreadCancelToken != null && _threadCancelToken.IsCancellationRequested)
                        {
                            ThreadCancelToken.Token.ThrowIfCancellationRequested();
                        }
                        #endregion
                        #region Events Check

                        try
                        {
                            if (ParamMajorAlarmProfileObj != null)
                            {
                                //  List<MajorAlarm> TriggeredList = Param_MajorAlarmProfile_obj.AlarmItems.FindAll(x => x.Info.EventId != MeterEvent.TimeBaseEvent_1 && x.Info.EventId != MeterEvent.TimeBaseEvent_2 && x.IsTriggered);
                                List<MajorAlarm> TriggeredList = ParamMajorAlarmProfileObj.AlarmItems.FindAll(x => x.IsTriggered);
                                //  int r=TriggeredList.RemoveAll(x => x.Info.EventCode.Equals(211) || x.Info.Equals(212)); //Remove TBEs

                                if (TriggeredList != null && TriggeredList.Count > 0)
                                {
                                    if (MeterInfo.Update_Alarm_Register_Live)
                                    {
                                        LogMessage("Updating Alarms Live", "ARL", "R");
                                        bool status = DB_Controller.UpdateAlarmStatusLive(SessionDateTime, param_MajorAlarmProfile_obj.MA_Status_Array, MeterInfo);
                                        LogMessage("Updating Alarms Live" + (status ? "Success" : "Failure"), "ARL", (status ? "S" : "F"));
                                    }
                                    MeterInfo.IsAnyAlarmTriggered = true;
                                    if (MeterInfo.Save_Events_On_Major_Alarm)
                                    {
                                        List<EventInfo> EventsInfo = _Configurator.GetMeterEventInfo(ConnectionController.CurrentConnection.ConnectionInfo);
                                        foreach (MajorAlarm alarm in TriggeredList)
                                        {
                                            //_EventId handles Combine event //Azeem
                                            alarm.Info = EventsInfo.Find(x => x.EventCode != 0 && x._EventId == alarm.Info._EventId);
                                            if (alarm.Info == null) TriggeredList.Remove(alarm);
                                        }

                                        bool isEmDevice = MeterInfo.DeviceTypeVal == DeviceType.eGenious;
                                        DB_Controller.Insert_Singlephase_Events_Log(TriggeredList, MeterInfo, SessionDateTime, isEmDevice);
                                    }
                                }
                            }

                            if (MeterInfo != null)
                            {
                                // Check for Events if triggered any other than timebase events
                                if (MeterInfo.ReadEventsOnMajorAlarms && MeterInfo.IsAnyAlarmTriggered)
                                // && triggered alarm is defined in the word in `individual_event_string_alarm`
                                {
                                    MeterInfo.ReadEventsForcibly = true;
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        #endregion

                        #region // If Task Canceled
                        if (ThreadCancelToken != null && _threadCancelToken.IsCancellationRequested)
                        {
                            ThreadCancelToken.Token.ThrowIfCancellationRequested();
                        }
                        #endregion
                        #region ResetAlarms

                        try
                        {
                            if (MeterInfo.Read_AR)
                            {
                                ResetAlarmRegister();
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        #endregion

                        #region // If Task Canceled

                        //                        if (ThreadCancelToken != null && _threadCancelToken.IsCancellationRequested)
                        //                        {
                        //                            ThreadCancelToken.Token.ThrowIfCancellationRequested();
                        //                        }

                        #endregion
                        #region Read Error String & caution

                        //                        try
                        //                        {
                        //                            if (MeterInfo.ReadDebugError)
                        //                            {
                        //                                #region Reading Debug Error String
                        //#if Enable_Abstract_Log
                        //                                LogMessage("Reading Debug Error String", "DES", "R");
                        //#endif

                        //#if !Enable_Abstract_Log
                        //                        LogMessage("Reading Debug Error String",1);
                        //#endif
                        //                                #endregion
                        //                                debug_error = ReadErrorString();
                        //                                #region Reading Debug Caution String
                        //#if Enable_Abstract_Log
                        //                                LogMessage("Reading Debug Caution String", "DCS", "R");
                        //#endif

                        //#if !Enable_Abstract_Log
                        //                        LogMessage("Reading Debug Caution String",1);
                        //#endif
                        //                                #endregion
                        //                                debug_caution = ReadCautionString();
                        //                                #region Reading Debug Contactor Status
                        //#if Enable_Abstract_Log
                        //                                LogMessage("Reading Debug Contactor Status", "DCNS", "R");
                        //#endif
                        //#if !Enable_Abstract_Log
                        //                        LogMessage("Reading Debug Contactor Status",1);
                        //#endif
                        //                                #endregion
                        //                                debug_contactor = ReadDebugContactorStatus();
                        //                            }
                        //                        }
                        //                        catch (Exception e)
                        //                        {
                        //                            LogMessage(e, 1);
                        //                        }

                        #endregion
                    }

                    #region Compute Reset Duration

                    if (MeterInfo != null)
                    {
                        sessionResetDuration = MeterInfo.DefaultResetSessionDuration;
                        if (sessionResetDuration < DefaultMinSessionDuration)
                            sessionResetDuration = DefaultMinSessionDuration;
                        else if (sessionResetDuration > DefaultMaxSessionDuration)
                            sessionResetDuration = DefaultMaxSessionDuration;

                        // Todo:Modification
                        // if (sessionResetDuration < TimeSpan.FromMinutes(2))//Approximately request process in 2 minutes
                        //     sessionResetDuration = (ConnectToMeter.CurrentConnection == PhysicalConnectionType.KeepAlive) ? KA_DefaultResetSessionDuration : NKA_DefaultResetSessionDuration;
                    }

                    #endregion
                    #region Process Request

                    if (SessionResetCancelTK != null)
                        SessionResetCancelTK.CancelAfter(sessionResetDuration);
                    else
                    {
                        SessionResetCancelTK = new CancellationTokenSource();
                        SessionResetCancelTK.CancelAfter(sessionResetDuration);
                    }
                    // Reset Session After Duration
                    CancellationTokenSource CancelTK = CancellationTokenSource.CreateLinkedTokenSource(ThreadCancelToken.Token, SessionResetCancelTK.Token);
                    #region Session Reset Duration
#if Enable_Abstract_Log
                    StatisticsObj.InsertLog("SRD", sessionResetDuration.ToString());
#endif

#if !Enable_Abstract_Log
						StatisticsObj.InsertLog(String.Format("Session Reset Duration : {0}", sessionResetDuration));
#endif
                    #endregion
                    MeterInfo.ModelID = 1;
                    if (IOConn.ConnectionInfo.MeterInfo.Device.IsSinglePhase) // 1 means R283
                    {
                        CusExc_ProcessRequest = ProcessRequest_SinglePhase(CancelTK);//also used for update schedule
                    }
                    else
                    {
                        //-- Alarm will reset either on successful or unsuccessful transaction

                        // on successful transaction this alarms will reset
                        CusExc_ProcessRequest = ProcessRequest(CancelTK);//also used for update schedule
                        // if (CusExc_ProcessRequest != null && CusExc_ProcessRequest.Ex == null)
                        // {
                        if (!MeterInfo.read_individual_events_sch && !MeterInfo.ReadEventsOnMajorAlarms)
                        {
                            // NO EVENTS TO READ. ALARM REGISTER STILL NOT CLEAR. CLEAR ALARM REGISTER NOW
                            //  Alarm Resetting change:5656
                            // if (MeterInfo.Read_AR)
                            //     ResetAlarmRegister();
                        }
                        // }
                    }
                    if (CancelTK != null)
                        CancelTK.Dispose();

                    // If Error Occurred Or Time Expire
                    if (CusExc_ProcessRequest != null && (!CusExc_ProcessRequest.isTrue || CusExc_ProcessRequest.Ex != null))
                    {
                        // Exit If External Thread Canceled
                        if (ThreadCancelToken.IsCancellationRequested)
                        {
                            throw CusExc_ProcessRequest.Ex;
                        }
                        else if (SessionResetCancelTK.IsCancellationRequested)
                        {
                            try
                            {
                                if (SessionResetCancelTK != null)
                                    SessionResetCancelTK.Dispose();
                                ConnectionController.CurrentConnection.ResetStream();
                                LogMessage("Resetting Session", 0);
                            }
                            catch (Exception ex)
                            {
                                LogMessage(String.Format("Error while Resetting Request Session\tDetail:{0}", ex.Message), 4);
                                break;
                            }
                        }
                        else
                        {
                            if (CusExc_ProcessRequest.Ex != null)
                                throw CusExc_ProcessRequest.Ex;
                            else
                            {
                                #region Logout/Disconnect

                                try
                                {
                                    LogoutMeterConnection();
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }

                                #endregion
                                break;
                            }
                        }
                    }
                    else
                    {
                        #region Logout/Disconnect

                        try
                        {
                            LogoutMeterConnection();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        #endregion
                        break;
                    }

                #endregion

                LOGOUT:
                    #region Logout/Disconnect

                    try
                    {
                        LogoutMeterConnection();
                    }
                    catch
                    {
                        break; // We are not pointing error if complete communication is successful and only issue in log out
                    }

                    #endregion
                    Thread.Sleep(1000);
                    Reset_Counts++;
                }
                while (Reset_Counts < MaxRequestReset &&
                       !IsProcessComplete);

                #region Logout/Disconnect If Request Was Empty

                try
                {
                    if (ConnectionController.IsConnected &&
                        MeterInfo.MustLogoutMeter)
                    {
                        LogoutMeterConnection();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                #endregion
            }
            catch (Exception ex)
            {
                try
                {

                    LogMessage(String.Format("Error in processing request: {0}", ex.Message), 4);
                    StatisticsObj.InsertError(ex, _session_DateTime, 15);
                    // error save in finally

                }
                catch { }
                CusExc_ProcessRequest.Ex = ex;
                CusExc_ProcessRequest.isTrue = false;
            }
            finally
            {
                try
                {
                    if (MeterInfo.Read_AR && MeterInfo.Save_AR)
                    {
                        // var hex = BitConverter.ToString(Commons.BitArrayToByteArray(MDCAlarm.MDCCombineEvents)).Replace("-", "");
                        var MA_Array = (param_MajorAlarmProfile_obj != null) ? param_MajorAlarmProfile_obj.MA_Status_Array : new BitArray(60);
                        var Success = DB_Controller.SaveAlarmStatus(MeterInfo.MSN, SessionDateTime, MA_Array, MDCAlarm.MDCCombineEvents, MeterInfo);
                        if (Success)
                            #region Alarm Status saved to DB
#if Enable_Abstract_Log
                            LogMessage("Alarm Status saved to DB", "ASD", "S", 2);
#endif

#if !Enable_Abstract_Log
						LogMessage("Alarm Status saved to DB", 2);
#endif
                        #endregion
                        else
                            #region Error while saving Alarm Status to DB
#if Enable_Abstract_Log
                            LogMessage("Error while saving Alarm Status to DB", "ASD", "F", 2);
#endif

#if !Enable_Abstract_Log
						LogMessage("Error while saving Alarm Status to DB", 2);
#endif
                        #endregion
                    }
                    MDCAlarm = new MDCEventsClass();
                    param_MajorAlarmProfile_obj = null;
                    // save all MDC Exceptions
                    // StatisticsObj.SaveErrors();
                    #region Errors debug

                    //                      if (MeterInfo.ReadDebugError)
                    //                      {
                    //                          #region Saving Debug Error, Cautions and Contactor Status
                    //  #if Enable_Abstract_Log
                    //                          LogMessage("Saving Debug Error, Cautions and Contactor Status", "DECCD", "R");
                    //  #endif
                    //  #if !Enable_Abstract_Log
                    //                          LogMessage("Saving Debug Error, Cautions and Contactor Status");
                    //  #endif
                    //                          #endregion
                    //                          var rslt = DB_Controller.Save_DebugErrorCautions(MeterInfo.MSN, MeterInfo.Reference_no, SessionDateTime, debug_error, debug_caution, debug_contactor);
                    //                          if (rslt)
                    //                              #region Saving Debug Error, Cautions and Contactor Status Successful
                    //  #if Enable_Abstract_Log
                    //                              LogMessage("Saving Debug Error, Cautions and Contactor Status Successful", "DECCD", "S");
                    //  #endif

                    //  #if !Enable_Abstract_Log
                    //                              LogMessage("Saving Debug Error, Cautions and Contactor Status Successful");
                    //  #endif
                    //                              #endregion
                    //                          else
                    //                              #region Error Saving Debug Error, Cautions and Contactor Status
                    //  #if Enable_Abstract_Log
                    //                              LogMessage("Error Saving Debug Error, Cautions and Contactor Status", "DECCD", "F");
                    //  #endif

                    //  #if !Enable_Abstract_Log
                    //                          LogMessage("Error Saving Debug Error, Cautions and Contactor Status");
                    //  #endif
                    //                              #endregion
                    //                          debug_caution = debug_contactor = debug_error = string.Empty;
                    //                      } 

                    #endregion
                }
                catch (Exception)
                {
                }
            }

            return CusExc_ProcessRequest;
        }

        public CustomException ProcessRequest(CancellationTokenSource CancelTokenSource = null)
        {
            var custExc = new CustomException();
            bool IsProcessed = true;
            bool IsMDIResetOccured = false;
            bool signal_strength_read = false;

            int meterEvetnsCount = _EventController.EventLogInfoList.FindAll(x => x.EventCode > 0).Count;

            try
            {
                if (MeterInfo != null && MeterInfo.MSN != null)
                {
                    #region /// If Task Canceled

                    if (CancelTokenSource != null &&
                        _threadCancelToken.IsCancellationRequested)
                    {
                        CancelTokenSource.Token.ThrowIfCancellationRequested();
                    }

                    #endregion
                    #region Save PQ Check

                    if (MeterInfo.IsAnyAlarmTriggered)
                    {
                        try
                        {
                            MeterInfo.EventsToSavePQ = Commons.HexStringToBinary(MeterInfo.EventsHexString, meterEvetnsCount);
                            MeterInfo.SavePQForcibly = CheckAnyEventToSavePQ();
                        }
                        catch // On exception Instantaneous Data Will not be saved on selected major alarms
                        {
                        }
                    }

                    #endregion
                    #region MDI Reset Check

                    if (ParamMajorAlarmProfileObj != null)
                    {
                        MajorAlarm MA = ParamMajorAlarmProfileObj.AlarmItems.Find(x => x.Info._EventId == 24); // MDI RESET
                        if (MA != null && MA.IsTriggered)
                        {
                            IsMDIResetOccured = true;
                        }
                    }

                    #endregion
                    foreach (var item in MeterInfo.ReadPlan)
                    {
                        switch (item)
                        {
                            case Schedules.ReadContactorStatus:
                                {
                                    #region // If Task Cancelled
                                    if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                                    {
                                        CancelTokenSource.Token.ThrowIfCancellationRequested();
                                    }
                                    #endregion
                                    IsProcessed = ReadContactorStatus(IsProcessed);
                                }
                                break;
                            case Schedules.RemoteGrid:
                                {
                                    #region // If Task Cancelled
                                    if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                                    {
                                        CancelTokenSource.Token.ThrowIfCancellationRequested();
                                    }
                                    #endregion
                                    #region RemoteGridStatus Reading

                                    if (MeterInfo.Schedule_RG.IsSuperImmediate ||
                                       (MeterInfo.Read_RG && MeterInfo.Schedule_RG.SchType != ScheduleType.Disabled))
                                    {
                                        try
                                        {
                                            List<GridStatusItem> objStatus = null;
                                            bool lastTimeUpdate = false;
                                            #region Reading Remote Grid Status
#if Enable_Abstract_Log
                                            LogMessage("Reading Remote Grid Status", "RGS", "R", 0);
#endif
#if !Enable_Abstract_Log
						        LogMessage("Reading Remote Grid Status");
#endif
                                            #endregion
                                            // _AP_Controller.ARLRQ();
                                            ReadRemoteGridStatus(ref objStatus);

                                            #region Reading Remote Grid Status completed
#if Enable_Abstract_Log
                                            LogMessage("Reading Remote Grid Status completed", "RGS", "S", 1);
#endif
#if !Enable_Abstract_Log
						        LogMessage("Reading Remote Grid Status completed",1);
#endif
                                            #endregion

                                            if (objStatus != null)
                                            {
                                                bool StatusChanged = GridStatusItem.IsStatusChanged(_GridInputStatus, objStatus);
                                                _GridInputStatus = objStatus;
                                                if (StatusChanged)
                                                    MeterInfo.ReadEventsForcibly = true;

                                                #region Saving

                                                DB_Controller.DBConnect.OpenConnection();

                                                bool saveFlag = DB_Controller.SaveRGCMStatus(SessionDateTime, objStatus, MeterInfo, StatusChanged);
                                                if (saveFlag)
                                                {
                                                    // MeterInfo.Schedule_CB.IsSuperImmediate = false;

                                                    #region Saving Remote Grid Status completed
#if Enable_Abstract_Log
                                                    LogMessage("Saving Remote Grid Status completed", "RGD", "S", 2);
#endif
#if !Enable_Abstract_Log
						LogMessage("Saving Remote Grid Status completed",2);
#endif
                                                    #endregion
                                                }
                                                else
                                                {
                                                    #region Error Occurred while Saving Cumulative billing Data
#if Enable_Abstract_Log
                                                    LogMessage("Error Occurred while Saving Remote Grid Status", "RGD", "F", 1);
#endif

#if !Enable_Abstract_Log
					LogMessage("Error Occurred while Saving Remote Grid Status", 2);
#endif
                                                    #endregion
                                                }

                                                #endregion
                                                lastTimeUpdate = saveFlag;
                                                MeterInfo.PreUpdateSchedule(MeterInfo.Schedule_RG, SessionDateTime);
                                                if (MeterInfo.Schedule_RG.SchType == ScheduleType.Disabled)
                                                    MIUF.Schedule_RG = true;
                                                MIUF.last_RG_time = lastTimeUpdate;
                                                if (MeterInfo.Schedule_RG.IsSuperImmediate)
                                                    MIUF.SuperImmediate_RG = true;
                                                if (MeterInfo.Schedule_RG.SchType == ScheduleType.IntervalFixed || MeterInfo.Schedule_RG.SchType == ScheduleType.IntervalRandom)
                                                    MIUF.base_time_RG = true;
                                                MeterInfo.Schedule_RG.IsSuperImmediate = false;
                                                IsProcessed = true;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            IsProcessed = false;
                                            LogMessage(ex, 4, "Remote Grid Status");
                                            // if (!(ex is NullReferenceException))
                                            //     throw ex;
                                            if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                                            StatisticsObj.InsertError(ex, _session_DateTime, 15);
                                        }
                                        finally
                                        {
                                            if (IsProcessed)
                                                ResetMaxIOFailure();
                                            DB_Controller.DBConnect.CloseConnection();
                                        }
                                    }

                                    #endregion
                                }
                                break;
                            case Schedules.CumulativeBilling:
                                {
                                    #region // If Task Canceled
                                    if (CancelTokenSource != null &&
                                        _threadCancelToken.IsCancellationRequested)
                                    {
                                        CancelTokenSource.Token.ThrowIfCancellationRequested();
                                    }
                                    #endregion
                                    #region Cumulative Billing Data

                                    if (MeterInfo.Schedule_CB.IsSuperImmediate || (!MeterInfo.PrioritizeWakeup && MeterInfo.Schedule_CB.SchType != ScheduleType.Disabled && MeterInfo.Read_CB != READ_METHOD.Disabled && MeterInfo.Save_CB))
                                    {
                                        if (MeterInfo.Schedule_CB.IsSuperImmediate || MeterInfo.IsScheduleReadyToBeProcess(MeterInfo.Schedule_CB))
                                        {
                                            IsProcessed = ReadAndSaveCummBilling(false);
                                        }
                                    }
                                    //}
                                    #endregion
                                }
                                break;
                            case Schedules.SignalStrength:
                                {
                                    #region // If Task Canceled
                                    if (CancelTokenSource != null)
                                    {
                                        CancelTokenSource.Token.ThrowIfCancellationRequested();
                                    }
                                    #endregion
                                    #region Signal Strength
#if !Enable_LoadTester_Mode
                                    if (MeterInfo.Schedule_SS.IsSuperImmediate || (!MeterInfo.PrioritizeWakeup && MeterInfo.Schedule_SS.SchType != ScheduleType.Disabled && MeterInfo.Read_SS))
                                    {
                                        if (MeterInfo.Schedule_SS.IsSuperImmediate || MeterInfo.IsScheduleReadyToBeProcess(MeterInfo.Schedule_SS))
                                        {
                                            try
                                            {
                                                #region Reading Signal Strength
#if Enable_Abstract_Log
                                                LogMessage("Reading Signal Strength", "SS", "R", 1);
#endif
#if !Enable_Abstract_Log
						LogMessage("Reading Signal Strength",1);
#endif
                                                #endregion;
                                                var insObj = new Instantaneous_Class();
                                                _signalStrength = Convert.ToInt32(InstantaneousController.GETDouble_Any(insObj, Get_Index.RSSI_SignalStrength, 2));//by attribute 0 to 2
                                                signal_strength_read = true;
                                                #region Signal Strength read successfully
#if Enable_Abstract_Log
                                                LogMessage("Signal Strength read successfully", "SS", "S", 1);
#endif
#if !Enable_Abstract_Log
						LogMessage("Signal Strength read successfully",1);
#endif
                                                #endregion
                                                MeterInfo.PreUpdateSchedule(MeterInfo.Schedule_SS, SessionDateTime);
                                                if (MeterInfo.Schedule_SS.SchType == ScheduleType.Disabled)
                                                    MIUF.Schedule_SS = true;
                                                MIUF.last_SS_time = true;
                                                if (MeterInfo.Schedule_SS.IsSuperImmediate)
                                                    MIUF.SuperImmediate_SS = true;
                                                if (MeterInfo.Schedule_SS.SchType == ScheduleType.IntervalFixed || MeterInfo.Schedule_SS.SchType == ScheduleType.IntervalRandom)
                                                    MIUF.base_time_SS = true;
                                                MeterInfo.Schedule_SS.IsSuperImmediate = false;
                                                bool Res;
                                                try
                                                {
                                                    Res = DB_Controller.UpdateSignalStrength_Live(_signalStrength, MeterInfo.MSN, SessionDateTime);
                                                    //  DB_Controller.InsertSignalStrength_Live(_signalStrength, MeterInfo.MSN);
                                                }
                                                catch (Exception)
                                                {
                                                    try
                                                    {
                                                        #region Signal Strength inserted successfully
#if Enable_Abstract_Log

                                                        if (DB_Controller.InsertSignalStrength_Live(_signalStrength, MeterInfo.MSN, SessionDateTime))
                                                            LogMessage("Signal Strength inserted successfully", "SSD", "S", 2);
                                                        else
                                                            LogMessage("Signal Strength update and insertion is unsuccessful", "SSD", "F", 2);
#endif
#if !Enable_Abstract_Log
						LogMessage(
                                            DB_Controller.InsertSignalStrength_Live(_signalStrength, MeterInfo.MSN)
                                                ? "Signal Strength inserted successfully"
                                                : "Signal Strength update and insertion is unsuccessful", 2);
#endif
                                                        #endregion
                                                    }
                                                    catch
                                                    { }
                                                    goto SS_Exit;
                                                }
                                                if (Res)
                                                    #region Signal Strength updated successfully
#if Enable_Abstract_Log
                                                    LogMessage("Signal Strength updated successfully", "SSD", "S", 1);
#endif
#if !Enable_Abstract_Log
						            LogMessage("Signal Strength updated successfully", 2);
#endif
                                                #endregion
                                                else if (!MeterInfo.IsLiveUpdated)
                                                {
                                                    #region Signal Strength inserted successfully
#if Enable_Abstract_Log

                                                    if (DB_Controller.InsertSignalStrength_Live(_signalStrength, MeterInfo.MSN, SessionDateTime))
                                                        LogMessage("Signal Strength inserted successfully", "SSD", "N", 2);
                                                    else
                                                        LogMessage("Signal Strength update and insertion is unsuccessful", "SSD", "F", 2);
#endif
#if !Enable_Abstract_Log
						LogMessage(
                                            DB_Controller.InsertSignalStrength_Live(_signalStrength, MeterInfo.MSN)
                                                ? "Signal Strength inserted successfully"
                                                : "Signal Strength update and insertion is unsuccessful", 2);
#endif
                                                    #endregion
                                                }

                                            SS_Exit:
                                                ;//Do Nothing
                                            }
                                            catch (Exception ex)
                                            {
                                                ex = new Exception(string.Format("{0} (Error Code:{1})", ex.Message, (int)MDCErrors.App_Signal_Strength_Read), ex.InnerException);
                                                LogMessage(ex, 4, "Signal Strength");
                                                // LogMessage("Error while reading Signal Strength: " + ex.Message, 4);
                                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                                                StatisticsObj.InsertError(ex, _session_DateTime, 15);
                                            }
                                            finally
                                            {
                                                if (signal_strength_read)
                                                    ResetMaxIOFailure();
                                            }
                                        }
                                    }
#endif
                                    #endregion
                                }
                                break;
                            case Schedules.PowerQuantities:
                                {
                                    #region // If Task Canceled
                                    if (CancelTokenSource != null)
                                    {
                                        CancelTokenSource.Token.ThrowIfCancellationRequested();
                                    }
                                    #endregion
                                    #region Instantaneous_Data

                                    if (MeterInfo.SavePQForcibly || MeterInfo.Schedule_PQ.IsSuperImmediate || (!MeterInfo.PrioritizeWakeup && MeterInfo.Schedule_PQ.SchType != ScheduleType.Disabled && MeterInfo.Read_PQ))
                                    {
                                        if (MeterInfo.SavePQForcibly || MeterInfo.Schedule_PQ.IsSuperImmediate || MeterInfo.IsScheduleReadyToBeProcess(MeterInfo.Schedule_PQ))
                                        {
                                            InstantaneousData insData = null;
                                            Instantaneous_Class data = null;
                                            try
                                            {
                                                int retryCount = 0;
                                                bool lastTimeUpdate = false;
                                                #region Reading Instantaneous Data
#if Enable_Abstract_Log
                                                LogMessage("Reading Instantaneous Data", "ID", "R", 1);
#endif

#if !Enable_Abstract_Log
						LogMessage("Reading Instantaneous Data", 0);
#endif
                                                #endregion

                                                if (MeterInfo.DDS110_Compatible)
                                                {
                                                    data = InstantaneousController.ReadInstantaneousDataByOBISList(); //ReadInstantaneousDataDDS();
                                                    data.MSN = MeterInfo.MSN;
                                                    //data.reference_no = MeterInfo.Reference_no;
                                                }
                                                else
                                                    insData = InstantaneousController.ReadInstantaneousData(false);

                                                #region Reading Instantaneous Data completed
#if Enable_Abstract_Log
                                                LogMessage("Reading Instantaneous Data completed", "ID", "S", 1);
#endif

#if !Enable_Abstract_Log
						LogMessage("Reading Instantaneous Data completed", 1);
#endif
                                                #endregion
                                                if (insData != null || data != null)
                                                {
                                                    if (!MeterInfo.DDS110_Compatible)
                                                        data = InstantaneousController.saveToClass(insData, MeterInfo.MSN);

                                                    try
                                                    {
                                                        DB_Controller.DBConnect.OpenConnection();
                                                        #region Update Live
#if !Enable_LoadTester_Mode
                                                        if (MeterInfo.EnableLiveUpdate)
                                                        {
                                                            try
                                                            {
                                                                if (MeterInfo.DDS110_Compatible) //By Azeem
                                                                {
                                                                    if (DB_Controller.Insert_Update_Instantaneous_Live_byObisList(data, SessionDateTime, ConnectToMeter.ConnectionTime, MeterInfo))//Updating data for live monitoring 
                                                                    {
                                                                        #region Instantaneous live data updated successfully
#if Enable_Abstract_Log
                                                                        LogMessage("Instantaneous live data updated successfully", "IDLD", "S", 1);
#endif
#if !Enable_Abstract_Log
						LogMessage("Instantaneous live data updated successfully", 2);
#endif
                                                                        #endregion
                                                                        MeterInfo.IsLiveUpdated = true;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (DB_Controller.UpdateInstantaneous_Live(data, SessionDateTime, ConnectToMeter.ConnectionTime, MeterInfo))//Updating data for live monitoring 
                                                                    {
                                                                        #region Instantaneous live data updated successfully
#if Enable_Abstract_Log
                                                                        LogMessage("Instantaneous live data updated successfully", "IDLD", "S", 1);
#endif
#if !Enable_Abstract_Log
						LogMessage("Instantaneous live data updated successfully", 2);
#endif
                                                                        #endregion
                                                                        MeterInfo.IsLiveUpdated = true;
                                                                    }
                                                                    else
                                                                    {
                                                                        #region Unable to update Instantaneous live data Server Inserting as a new Record
#if Enable_Abstract_Log
                                                                        LogMessage("Unable to update Instantaneous live data Server Inserting as a new Record", "IDLD", "N", 1);
#endif
#if !Enable_Abstract_Log
						LogMessage("Unable to update Instantaneous live data Server Inserting as a new Record", 2);
#endif
                                                                        #endregion
                                                                        if (DB_Controller.InsertInstantaneous_Live(data, SessionDateTime, ConnectToMeter.ConnectionTime, MeterInfo))
                                                                            MeterInfo.IsLiveUpdated = true;
                                                                    }
                                                                }
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                LogMessage(String.Format("Unable to update Instantaneous live data\r\nDetails:{0}", ex.Message), 4);
                                                            }
                                                        }
#endif
                                                        #endregion
                                                        #region Save
                                                        if (MeterInfo.SavePQForcibly || (MeterInfo.IsScheduleReadyToBeProcess(MeterInfo.SaveSchedule_PQ) && MeterInfo.SaveSchedule_PQ.SchType != ScheduleType.Disabled && MeterInfo.Save_PQ))
                                                        {
                                                            var saveFlag = false;
                                                            if (MeterInfo.DDS110_Compatible)
                                                                saveFlag = DB_Controller.InsertInstantaneous_byObisList(data, SessionDateTime, MeterInfo);
                                                            else
                                                                saveFlag = DB_Controller.SaveInstantaneous(data, SessionDateTime, _signalStrength, signal_strength_read, MeterInfo);

                                                            lastTimeUpdate = saveFlag;
                                                            if (saveFlag)
                                                            {

                                                                MeterInfo.PreUpdateSchedule(MeterInfo.SaveSchedule_PQ, SessionDateTime);
                                                                if (MeterInfo.SaveSchedule_PQ.SchType == ScheduleType.Disabled)
                                                                    MIUF.SaveSchedule_PQ = true;
                                                                MIUF.last_Save_PQ_time = true;
                                                                if (MeterInfo.SaveSchedule_PQ.IsSuperImmediate)
                                                                    MIUF.SuperImmediate_Save_PQ = true;
                                                                if (MeterInfo.SaveSchedule_PQ.SchType == ScheduleType.IntervalFixed || MeterInfo.SaveSchedule_PQ.SchType == ScheduleType.IntervalRandom)
                                                                    MIUF.base_time_Save_PQ = true;
                                                                MeterInfo.SaveSchedule_PQ.IsSuperImmediate = false;
                                                                #region Saving Instantaneous Data completed

                                                                LogMessage("Saving Instantaneous Data completed", "IDD", "S", 1);

                                                                #endregion
                                                            }
                                                            else
                                                            {
                                                                #region

                                                                LogMessage(string.Format("Error while saving Instantaneous Data (Error Code:{0})", (int)MDCErrors.App_Instantaneous_Data_Save), "IDD", "F", 1);

                                                                #endregion
                                                            }
                                                        }
                                                        #endregion
                                                    }
                                                    finally
                                                    {
                                                        DB_Controller.DBConnect.CloseConnection();
                                                    }

                                                    MeterInfo.PreUpdateSchedule(MeterInfo.Schedule_PQ, SessionDateTime);
                                                    if (MeterInfo.Schedule_PQ.SchType == ScheduleType.Disabled)
                                                        MIUF.Schedule_PQ = true;
                                                    MIUF.last_PQ_time = lastTimeUpdate;
                                                    if (MeterInfo.Schedule_PQ.IsSuperImmediate)
                                                        MIUF.SuperImmediate_PQ = true;
                                                    if (MeterInfo.Schedule_PQ.SchType == ScheduleType.IntervalFixed || MeterInfo.Schedule_PQ.SchType == ScheduleType.IntervalRandom)
                                                        MIUF.base_time_PQ = true;

                                                    MeterInfo.Schedule_PQ.IsSuperImmediate = false;
                                                    IsProcessed = true;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                IsProcessed = false;
                                                LogMessage(ex, 4, "Instantaneous Data");
                                                // if (!(ex is NullReferenceException))
                                                //    throw;
                                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                                                StatisticsObj.InsertError(ex, _session_DateTime, 15);
                                            }
                                            finally
                                            {
                                                if (IsProcessed)
                                                    ResetMaxIOFailure();
                                            }
                                        }
                                    }

                                    #endregion
                                }
                                break;
                            case Schedules.Events:
                                {
                                    #region // If Task Cancelled

                                    if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                                    {
                                        CancelTokenSource.Token.ThrowIfCancellationRequested();
                                    }

                                    #endregion
                                    #region Events

                                    #region Read Combine Event Logs

                                    IsProcessed = ProcessEventsLogRequest(CancelTokenSource, meterEvetnsCount);

                                    #endregion

                                    #region READ SEPARATE EVENTS

                                    List<EventData> _obj_EventData = null;
                                    try
                                    {

                                        List<EventInfo> eventsToRead = new List<EventInfo>();
                                        List<EventInfo> eventsIndividualRead = new List<EventInfo>();
                                        EventLogInfo _obj = null;
                                        MeterInfo.individual_events_string_sch = (string.IsNullOrEmpty(MeterInfo.individual_events_string_sch)) ? "0" : MeterInfo.individual_events_string_sch;
                                        MeterInfo.individual_events_string_alarm = (string.IsNullOrEmpty(MeterInfo.individual_events_string_alarm)) ? "0" : MeterInfo.individual_events_string_alarm;
                                        // EventIDtoCode eventIDtoCode_obj = new EventIDtoCode();
                                        if ((MeterInfo.read_individual_events_sch && MeterInfo.ReadEventsForcibly))
                                        {
                                            //long longValue1 = Convert.ToInt64(MeterInfo.individual_events_string_alarm, 16);
                                            //long longValue2 = Convert.ToInt64(MeterInfo.individual_events_string_sch, 16);
                                            //long finalVal = longValue1 | longValue2; //bitwise OR to get ALL 1's
                                            ////string binRepresentation = Convert.ToString(finalVal, 2);
                                            ////while (binRepresentation.Length < 60)
                                            ////{
                                            ////    //binRepresentation = String.Concat("0", binRepresentation);
                                            ////    binRepresentation = String.Concat(binRepresentation, "0");
                                            ////    MeterInfo.individual_events_array = binRepresentation.ToCharArray();
                                            ////}
                                            //MeterInfo.individual_events_array = LongToBinary(finalVal, meterEvetnsCount);
                                            char[] binStringEventsOnAlarm = Commons.HexStringToBinary(MeterInfo.individual_events_string_alarm, meterEvetnsCount);
                                            char[] binStringEventsSch = Commons.HexStringToBinary(MeterInfo.individual_events_string_sch, meterEvetnsCount);
                                            MeterInfo.individual_events_array = new char[meterEvetnsCount];
                                            for (int i = 0; i < meterEvetnsCount; i++)
                                            {
                                                MeterInfo.individual_events_array[i] = (Convert.ToByte(binStringEventsOnAlarm[i]) | Convert.ToByte(binStringEventsSch[i])).ToString().ToCharArray()[0];
                                            }
                                        }
                                        if (MeterInfo.read_individual_events_sch)
                                        {
                                            //WHen reading events on schedule, we do not need to check if that alarm was triggered. This check
                                            //is included when we read events in case of major alarms
                                            if ((!MeterInfo.PrioritizeWakeup && MeterInfo.individual_events_string_sch != string.Empty &&
                                                MeterInfo.IsScheduleReadyToBeProcess(MeterInfo.Schedule_EV)) ||
                                                MeterInfo.Schedule_EV.IsSuperImmediate)
                                            {
                                                MeterInfo.individual_events_array = Commons.HexStringToBinary(MeterInfo.individual_events_string_sch, meterEvetnsCount);
                                                for (int a = 1; a <= MeterInfo.individual_events_array.Length; a++)
                                                {
                                                    if (MeterInfo.individual_events_array[a - 1].Equals('1'))
                                                    {
                                                        //TODO:Modification
                                                        //int CurrentEventCode = eventIDtoCode_obj.getEventCode((int)a);
                                                        //if (CurrentEventCode != -1)
                                                        _obj = _EventController.EventLogInfoList.Find(x => x._EventId == a); //|| x.EventCode == CurrentEventCode);
                                                        //No Event Data in case of TBE1 and TBE2
                                                        if (_obj != null && ((_obj._EventId != 38 && _obj._EventId != 39) ||
                                                                            (_obj.EventCode != 211 && _obj.EventCode != 212)))
                                                            eventsToRead.Add(_obj);
                                                    }
                                                }
                                                // else goto exitEvents;
                                            }
                                        }
                                        if ((MeterInfo.ReadEventsForcibly && MeterInfo.individual_events_string_alarm != string.Empty) || MeterInfo.Schedule_EV.IsSuperImmediate)
                                        {
                                            MeterInfo.individual_events_array = Commons.HexStringToBinary(MeterInfo.individual_events_string_alarm, meterEvetnsCount);
                                            for (int a = 1; a <= MeterInfo.individual_events_array.Length; a++)
                                            {
                                                if (MeterInfo.individual_events_array[a - 1].Equals('1') && ParamMajorAlarmProfileObj != null && ParamMajorAlarmProfileObj.AlarmItems[a - 1].IsTriggered)
                                                {
                                                    //TODO:Modification
                                                    //int CurrentEventCode = eventIDtoCode_obj.getEventCode((int)a);
                                                    //if (CurrentEventCode != -1)
                                                    _obj = _EventController.EventLogInfoList.Find(x => x._EventId == a); //|| x.EventCode == CurrentEventCode);
                                                    //No Event Data in case of TBE1 and TBE2
                                                    if (_obj != null && ((_obj._EventId != 38 && _obj._EventId != 39) || (_obj.EventCode != 211 && _obj.EventCode != 212)))
                                                        eventsToRead.Add(_obj);
                                                }
                                            }
                                        }

                                        if ((MeterInfo.ReadEventsForcibly || MeterInfo.read_individual_events_sch) && eventsToRead.Count > 0)
                                            eventsToRead = eventsToRead.Distinct().ToList();

                                        #region //Clone EventInfo & EventLogInfo Obj

                                        IList<EventInfo> event_Info_T = eventsToRead.ToList();
                                        eventsToRead.Clear();
                                        foreach (var ev_Info in event_Info_T)
                                        {
                                            EventInfo t_Obj = null;
                                            if (ev_Info is EventLogInfo)
                                                t_Obj = (EventInfo)((EventLogInfo)ev_Info).Clone();
                                            else if (ev_Info is EventInfo)
                                                t_Obj = (EventInfo)((EventInfo)ev_Info).Clone();
                                            eventsToRead.Add(t_Obj);
                                        }

                                        #endregion

                                        Exception Internal_Exception = null;
                                        bool isSuccessful = false;
                                        _obj_EventData = null;

                                        if (eventsToRead.Count > 0)
                                        {
                                            #region Debugging & Logging
#if Enable_DEBUG_ECHO
                                            #region Reading Individual Events Data
#if Enable_Abstract_Log
                                            LogMessage("Reading Individual Events Data", "IE", "R", 1);
#endif
#if !Enable_Abstract_Log
						                    LogMessage("Reading Individual Events Data", 0);
#endif
                                            #endregion
#endif
                                            #endregion

                                            _obj_EventData = new List<EventData>();
                                            //_obj_EventData = _EventController.ReadEventLogData(eventsToRead);
                                            isSuccessful = _EventController.TryReadEventLogData(eventsToRead, ref _obj_EventData, (ex) => Internal_Exception = ex, CancelTokenSource);
                                            if (!isSuccessful && Internal_Exception != null)
                                                throw Internal_Exception;
                                            //Update Individual Events Read Successfully
                                            foreach (var ev_Data in _obj_EventData)
                                            {
                                                if (ev_Data != null && ev_Data.EventInfo != null)
                                                    eventsIndividualRead.Add(ev_Data.EventInfo);
                                            }
                                            MeterInfo.eventsForLiveUpdate_individual = Commons.HexStringToBinary(MeterInfo.eventsForLiveUpdate_individual_string, meterEvetnsCount);
                                            DateTime latest_Event = DateTime.MinValue;
                                            EventData data = null;
                                            EventItem data_Item = null;
                                            DateTime currentMax = DateTime.MinValue;
                                            int latestEventCode = 0;

                                            for (int i = 0; i < MeterInfo.eventsForLiveUpdate_individual.Length; i++)
                                            {
                                                if (MeterInfo.eventsForLiveUpdate_individual[i] == '1')
                                                {
                                                    //int code = eventIDtoCode_obj.getEventCode((int)i + 1);
                                                    data = _obj_EventData.Find(x => x.EventInfo != null &&
                                                                              (x.EventInfo._EventId == i + 1));
                                                    // || x.EventInfo.EventCode == code));

                                                    if (data != null && data.EventRecords.Count > 0)
                                                    {
                                                        currentMax = data.EventRecords.Max(x => x.EventDateTimeStamp);
                                                        data_Item = data.EventRecords.Find(x => x.EventDateTimeStamp == currentMax);

                                                        if (currentMax > latest_Event)
                                                        {
                                                            latest_Event = currentMax;
                                                            if (data_Item != null && data_Item.EventInfo != null)
                                                                latestEventCode = data_Item.EventInfo.EventCode;
                                                        }
                                                    }
                                                }
                                            }
                                            if (latest_Event != DateTime.MinValue)
                                            {
                                                if (DB_Controller.UpdateLastEvent_Live_individual(MeterInfo.MSN, latestEventCode, latest_Event))
                                                {
                                                    #region  LogMessage("Individual Events->Event Code " + latestEventCode + " updated to Instantaneous Live ")
#if Enable_Abstract_Log
                                                    LogMessage("Individual Events->Event Code " + latestEventCode + " updated to Instantaneous Live ", "IEDL", latestEventCode.ToString(), 1);
#endif
#if !Enable_Abstract_Log
						                            LogMessage("Individual Events->Event Code " + latestEventCode + " updated to Instantaneous Live ");
#endif
                                                    #endregion
                                                }
                                                else
                                                {
                                                    #region  LogMessage("Error occurred while updating Event Code " + latestEventCode + " updated to Instantaneous Live ")
#if Enable_Abstract_Log
                                                    LogMessage("Error occurred while updating Event Code " + latestEventCode + " updated to Instantaneous Live", "IEDL", "F", 1);
#endif

#if !Enable_Abstract_Log
						                            LogMessage("Error occurred while updating Event Code " + latestEventCode + " updated to Instantaneous Live ");
#endif
                                                    #endregion
                                                }
                                            }
                                            #region Debugging & Logging
#if Enable_DEBUG_ECHO

                                            #region LogMessage("Reading Individual Events Data Complete", 0)
#if Enable_Abstract_Log
                                            if (isSuccessful)
                                                LogMessage("Reading Individual Events Data Complete", "IE", "S", 1);
#endif

#if !Enable_Abstract_Log
						LogMessage("Reading Individual Events Data Complete", 0);
#endif
                                            #endregion
#endif
#if Enable_Transactional_Logging
                           Statistics_Obj.InsertLog(String.Format("Reading Events Data Complete for MSN {0}", MSN));
#endif
                                            #endregion
                                        }
                                        else
                                        {
                                            #region Debugging & Logging

#if Enable_Transactional_Logging

                           //Program.Out.WriteLine(String.Format("No New Event triggered for MSN {0}", MSN));
                           Statistics_Obj.InsertLog(String.Format("No New Event triggered for MSN {0}", MSN));

#endif
                                            #endregion
                                        }
                                        // exitEvents:
                                        if (_obj_EventData != null)
                                        {

                                            #region  LogMessage("Saving Individual Events data to DB", 0);
#if Enable_Abstract_Log
                                            LogMessage("Saving Individual Events data to DB", "IED", "R", 1);
#endif

#if !Enable_Abstract_Log
						 LogMessage("Saving Individual Events data to DB", 0);
#endif
                                            #endregion
                                            CustomException CEX = DB_Controller.saveEventsData_IndividualithReplace(_obj_EventData, MeterInfo.MSN, SessionDateTime, MeterInfo);
                                            if (CEX != null && CEX.Ex != null)
                                            {

                                                #region LogMessage(string.Format("Error Saving Individual Events data (Error Code:{0})", (int)MDCErrors.App_Individual_Events_Data_Save), 0)
#if Enable_Abstract_Log
                                                LogMessage(string.Format("Error Saving Individual Events data (Error Code:{0})", (int)MDCErrors.App_Individual_Events_Data_Save), "IED", "F", 1);
#endif

#if !Enable_Abstract_Log
						LogMessage(string.Format("Error Saving Individual Events data (Error Code:{0})", (int)MDCErrors.App_Individual_Events_Data_Save), 0);
#endif
                                                #endregion
                                                throw CEX.Ex;
                                            }
                                            else if (!isSuccessful && Internal_Exception != null)
                                            {

                                                #region   LogMessage(string.Format("Error Occurred reading Individual Events data (Error Code:{0})", (int)MDCErrors.App_Individual_Events_Data_Read), 0);
#if Enable_Abstract_Log
                                                LogMessage(string.Format("Error Occurred reading Individual Events data (Error Code:{0})", (int)MDCErrors.App_Individual_Events_Data_Read), "IE", "F", 1);
#endif

#if !Enable_Abstract_Log
						  LogMessage(string.Format("Error Occurred reading Individual Events data (Error Code:{0})", (int)MDCErrors.App_Individual_Events_Data_Read), 0);
#endif
                                                #endregion
                                                //Reset Alarm Register
                                                if (MeterInfo.Read_AR)
                                                {
                                                    ResetAlarmRegister(eventsIndividualRead);
                                                }
                                                IsProcessed = false;
                                                throw Internal_Exception;
                                            }
                                            else
                                            {
                                                //Reset Alarm Register // Alarm Resetting change:5656
                                                //if (MeterInfo.Read_AR)
                                                //{
                                                //    ResetAlarmRegister();
                                                //}
                                                MeterInfo.ReadEventsForcibly = false;
                                                MeterInfo.PreUpdateSchedule(MeterInfo.Schedule_EV, SessionDateTime);
                                                MIUF.Schedule_EV = true;
                                                MIUF.last_EV_time = true;
                                                if (MeterInfo.Schedule_EV.IsSuperImmediate)
                                                    MIUF.SuperImmediate_EV = true;
                                                if (MeterInfo.Schedule_EV.SchType == ScheduleType.IntervalFixed || MeterInfo.Schedule_EV.SchType == ScheduleType.IntervalRandom)
                                                    MIUF.base_time_EV = true;

                                                MeterInfo.Schedule_EV.IsSuperImmediate = false;
                                                IsProcessed = true;
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        IsProcessed = false;
                                        LogMessage(ex, 4, "Events");
                                        // throw ex;
                                        if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                                        StatisticsObj.InsertError(ex, _session_DateTime, 15);
                                    }
                                    finally
                                    {
                                        if (IsProcessed)
                                            ResetMaxIOFailure();
                                    }


                                    #endregion
                                    #endregion
                                }
                                break;
                            case Schedules.MonthlyBilling:
                                {
                                    #region // If Task Canceled
                                    if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                                    {
                                        CancelTokenSource.Token.ThrowIfCancellationRequested();
                                    }
                                    #endregion
                                    #region Monthly Billing
#if !Enable_LoadTester_Mode
                                    if (MeterInfo.Schedule_MB.IsSuperImmediate || (!MeterInfo.PrioritizeWakeup && (IsMDIResetOccured ||
                                        MeterInfo.Schedule_MB.SchType != ScheduleType.Disabled) && MeterInfo.MB_Counters.DB_Counter >= 0 &&
                                        MeterInfo.Read_MB != READ_METHOD.Disabled && MeterInfo.Save_MB))
                                    {
                                        if (MeterInfo.Schedule_MB.IsSuperImmediate ||
                                            MeterInfo.IsScheduleReadyToBeProcess(MeterInfo.Schedule_MB))
                                        {
                                            List<BillingData> monthlyBillingData = null;
                                            Monthly_Billing_data monthlyData = null;
                                            var lastTimeUpdate = false;
                                            //uint meterCounter = 0;
                                            bool IsMBUpToDate = false;
                                            bool IsMBRead = true;
                                            try
                                            {
                                                if (MeterInfo.Read_MB == READ_METHOD.ByCounter && MeterInfo.MB_Counters.DB_Counter >= 0)
                                                {
                                                    monthlyBillingData = new List<BillingData>();
                                                    MeterInfo.MB_Counters.Meter_Counter = Billing_Controller.Get_BillingCounter_Internal();
                                                    #region LogMessage(String.Format("Monthly Billing Counter Received from Meter: {0}", meterCounter), 3)
#if Enable_Abstract_Log
                                                    //LogMessage(String.Format("Monthly Billing Counter Received from Meter: {0}", meterCounter), true, 1);
#endif

#if !Enable_Abstract_Log
						LogMessage(String.Format("Monthly Billing Counter Received from Meter: {0}", meterCounter), 3);
#endif
                                                    #endregion
                                                    if (MeterInfo.MB_Counters.Meter_Counter > 0) //Billing available
                                                    {
                                                        #region Billing available
                                                        if (MeterInfo.MB_Counters.Difference == 0)
                                                        {
                                                            #region LogMessage("Monthly Billing Data is up-to-date");
#if Enable_Abstract_Log
                                                            LogMessage("Monthly Billing Data is up-to-date", "MB", string.Format("U, {0}", MeterInfo.MB_Counters.DB_Counter), 1);
#endif

#if !Enable_Abstract_Log
						LogMessage("Monthly Billing Data is up-to-date");
#endif
                                                            #endregion
                                                            IsMBUpToDate = true;
                                                        }
                                                        else if (MeterInfo.MB_Counters.Difference > 0)
                                                        {
                                                            #region CalculateMonthlyBillingCounter
                                                            //uint differenceInCounter = Convert.ToUInt32(meterCounter - MeterInfo.Counter_Obj.MonthlyBilling_Count);
                                                            int counterToread;

                                                            // Billing_Controller.MonthlyBillingFilter = Convert.ToByte(counterToread);
                                                            #endregion
                                                            #region LogMessage("Reading Monthly Billing Data")
#if Enable_Abstract_Log
                                                            var mCount = MeterInfo.MB_Counters.Meter_Counter;
                                                            var Dbcount = MeterInfo.MB_Counters.DB_Counter;
                                                            var reading = MeterInfo.MB_Counters.Difference;
                                                            LogMessage("Reading Monthly Billing Data", "MB",
                                                                       string.Format("R {0}, {1}, {2}, {3}, {4} - {5}", mCount, Dbcount, (mCount - Dbcount), reading, Dbcount, Dbcount + reading, 1));
#endif

#if !Enable_Abstract_Log
						                                    LogMessage("Reading Monthly Billing Data");
#endif
                                                            #endregion
                                                            if (MeterInfo.BillingMethodId == (byte)BillingMethods.CustomMethod)
                                                            {
                                                                monthlyData = Billing_Controller.GetBillingDataCustomMethod(MeterInfo.MB_Counters.DB_Counter, MeterInfo.MB_Counters.Meter_Counter, (byte)MeterInfo.MB_Counters.Max_Size);
                                                            }
                                                            else if (MeterInfo.BillingMethodId == (byte)BillingMethods.GetByQuantity)
                                                            {
                                                                monthlyData = Billing_Controller.GetBillingDataDetail(MeterInfo.MB_Counters.DB_Counter, MeterInfo.MB_Counters.Meter_Counter, Convert.ToByte(MeterInfo.MB_Counters.Max_Size));
                                                            }
                                                            else if (MeterInfo.BillingMethodId == (byte)BillingMethods.OneGetMethod)
                                                            {
                                                                byte MaxCounterToread = (byte)(MeterInfo.MB_Counters.Max_Size - 1);
                                                                if (MeterInfo.MB_Counters.Difference > MaxCounterToread) counterToread = MaxCounterToread;
                                                                counterToread = MeterInfo.MB_Counters.Difference + 100;
                                                                monthlyBillingData = Billing_Controller.GetBillingData(MeterInfo.MB_Counters, MeterInfo.Read_MB);
                                                            }
                                                            #region LogMessage("Reading Monthly Billing Data Complete", 1);
#if Enable_Abstract_Log
                                                            LogMessage("Reading Monthly Billing Data Complete", "MB", "S", 1);
#endif

#if !Enable_Abstract_Log
						LogMessage("Reading Monthly Billing Data Complete", 1);
#endif
                                                            #endregion
                                                        }

                                                        #endregion
                                                    }
                                                    else
                                                    {
                                                        #region LogMessage("Billing not available (MDI Reset Count = 0)", 3);
#if Enable_Abstract_Log
                                                        LogMessage("Billing not Available (MDI Reset Count = 0)", "MB", "EMPT", 1);
#endif

#if !Enable_Abstract_Log
						LogMessage("Billing not available (MDI Reset Count = 0)", 3);
#endif
                                                        #endregion
                                                    }
                                                }
                                                else if (MeterInfo.Read_MB == READ_METHOD.ByDateTime)
                                                {
                                                    LogMessage("Reading Monthly Billing Data", "MB",
           string.Format("R {0} - {1}", MeterInfo.MB_Counters.LastReadTime.ToString(Commons.DateTimeFormat), DateTime.Now.ToString(Commons.DateTimeFormat)));

                                                    monthlyBillingData = Billing_Controller.GetBillingData(MeterInfo.MB_Counters, MeterInfo.Read_MB);
                                                    LogMessage("Reading Monthly Billing Data Complete", "MB", "S", 1);

                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                IsProcessed = false;
                                                LogMessage(ex, 4, "Monthly Billing");
                                                // if (!(ex is NullReferenceException))
                                                //     throw;
                                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                                                StatisticsObj.InsertError(ex, _session_DateTime, 15);
                                                IsMBRead = false;
                                            }
                                            finally
                                            {
                                                try
                                                {
                                                    if (MeterInfo != null && MeterInfo.MB_Counters != null &&
                                                        (MeterInfo.MB_Counters.Meter_Counter > 0 || MeterInfo.Read_MB == READ_METHOD.ByDateTime))
                                                        ResetMaxIOFailure();

                                                    if (MeterInfo.Save_MB && !IsMBUpToDate && IsMBRead && ((MeterInfo.DDS110_Compatible && monthlyData != null) || monthlyBillingData != null))
                                                    {
                                                        //save to class
                                                        if (MeterInfo.BillingMethodId == (byte)BillingMethods.OneGetMethod)
                                                        {
                                                            monthlyData = Billing_Controller.SaveToClass(monthlyBillingData, MeterInfo.MSN);
                                                        }
                                                        else
                                                        {
                                                            foreach (m_data billingData in monthlyData.monthly_billing_data)
                                                            {
                                                                billingData.billData_obj.DBColumns.Append("`active_energy_t1`, `active_energy_t2`, `active_energy_t3`, `active_energy_t4`, `active_energy_tl`, `reactive_energy_t1`, `reactive_energy_t2`, `reactive_energy_t3`, `reactive_energy_t4`, `reactive_energy_tl`, `active_mdi_t1`, `active_mdi_t2`, `active_mdi_t3`, `active_mdi_t4`, `active_mdi_tl`,");
                                                                billingData.billData_obj.DBValues.Append(string.Format("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}'"
                                                                        , billingData.billData_obj.activeEnergy_T1
                                                                        , billingData.billData_obj.activeEnergy_T2
                                                                        , billingData.billData_obj.activeEnergy_T3
                                                                        , billingData.billData_obj.activeEnergy_T4
                                                                        , billingData.billData_obj.activeEnergy_TL
                                                                        , billingData.billData_obj.reactiveEnergy_T1
                                                                        , billingData.billData_obj.reactiveEnergy_T2
                                                                        , billingData.billData_obj.reactiveEnergy_T3
                                                                        , billingData.billData_obj.reactiveEnergy_T4
                                                                        , billingData.billData_obj.reactiveEnergy_TL
                                                                        , billingData.billData_obj.activeMDI_T1
                                                                        , billingData.billData_obj.activeMDI_T2
                                                                        , billingData.billData_obj.activeMDI_T3
                                                                        , billingData.billData_obj.activeMDI_T4
                                                                        , billingData.billData_obj.activeMDI_TL));
                                                            }
                                                            //
                                                        }
                                                        /*
                                                        DB_Controller.ReadMeterStatusInfo(monthlyData, MeterInfo.Customer_ID);
                                                        */
                                                        // there will come the logic of reading meter info
                                                        var CEx = DB_Controller.SaveMonthlyBillingDataWithReplaceEx(monthlyData,
                                                            MeterInfo.MB_Counters.Meter_Counter, SessionDateTime, MeterInfo, MIUF);
                                                        if (CEx != null && CEx.isTrue && CEx.Ex == null && monthlyData.monthly_billing_data.Count > 0)
                                                        {
                                                            if (MeterInfo.Read_MB == READ_METHOD.ByCounter)
                                                            {

                                                                //bool flag = DB_Controller.update_MonthlyBilling_Counter(MeterInfo.MSN, meterCounter);
                                                                MeterInfo.MonthlyBillingCounterToUpdate = (int)MeterInfo.MB_Counters.Meter_Counter;
                                                                MIUF.UpdateMBCounter = true;
                                                                #region LogMessage(String.Format("Saving Monthly billing Data completed and updated to count {0}", meterCounter), 2);
#if Enable_Abstract_Log
                                                                LogMessage(String.Format("Saving Monthly billing Data completed and updated to count {0}", MeterInfo.MB_Counters.Meter_Counter), "MBD", string.Format("S, {0}", MeterInfo.MB_Counters.Meter_Counter.ToString()), 1);
#endif
#if !Enable_Abstract_Log
						LogMessage(String.Format("Saving Monthly billing Data completed and updated to count {0}", meterCounter), 2);
#endif
                                                            }
                                                            else
                                                            {
                                                                LogMessage("Saving Monthly billing Data completed", "MBD", "S", 1);
                                                            }
                                                            #endregion
                                                            lastTimeUpdate = true;
                                                        }
                                                        else
                                                        {
                                                            if (MeterInfo.Read_MB == READ_METHOD.ByCounter && CEx.SomeMessage.Contains(string.Format("Error:{0}", (int)MDCErrors.DB_Duplicate_Entery)))
                                                            {
                                                                #region monthly billing Counter difference check w.r.t database count
                                                                DB_Controller.DBConnect.OpenConnection();
                                                                var warning = String.Format("Invalid Monthly billing Counter Received DBCounter:{0}, MeterCounter:{1}, Server is disabling the Monthly billing", MeterInfo.MB_Counters.DB_Counter, MeterInfo.MB_Counters.Meter_Counter);
                                                                #region LogMessage(warning);
#if Enable_Abstract_Log
                                                                LogMessage(warning, "MB", string.Format("D, {0},{1}", MeterInfo.MB_Counters.Meter_Counter, MeterInfo.MB_Counters.DB_Counter), 1);
#endif
#if !Enable_Abstract_Log
						LogMessage(warning);
#endif
                                                                #endregion
                                                                DB_Controller.InsertWarning(MeterInfo.MSN, SessionDateTime, ConnectToMeter.ConnectionTime, warning);

                                                                MIUF.IsDisableMB = true;

                                                                MDCAlarm.MDCCombineEvents[(ushort)MDCEvents.mb_counter_mismatch] = true;
                                                                MDCAlarm.IsMDCEventOuccer = true;
                                                                DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.mb_counter_mismatch), MeterInfo, SessionDateTime);
                                                                #endregion
                                                            }
                                                            else
                                                            {
                                                                string Error = "Saving Monthly Billing failed";
                                                                if (CEx != null && CEx.Ex != null)
                                                                    Error += " Error: " + CEx.Ex.Message;
                                                                #region  LogMessage(string.Format("{0} (Error Code:{1})", Error, (int)MDCErrors.App_Monthly_Billing_Save), 4);
#if Enable_Abstract_Log
                                                                LogMessage(string.Format("{0} (Error Code:{1})", Error, (int)MDCErrors.App_Monthly_Billing_Save), "MBD", "F", 1);
#endif

#if !Enable_Abstract_Log
						 LogMessage(string.Format("{0} (Error Code:{1})", Error, (int)MDCErrors.App_Monthly_Billing_Save), 4);
#endif
                                                                #endregion
                                                            }
                                                        }

                                                    }
                                                    if (IsMBUpToDate || monthlyBillingData == null)
                                                    {
                                                        MeterInfo.PreUpdateSchedule(MeterInfo.Schedule_MB, SessionDateTime);
                                                        if (MeterInfo.Read_MB == READ_METHOD.ByDateTime && monthlyBillingData != null && monthlyBillingData.Count > 0)
                                                        {
                                                            MeterInfo.Schedule_MB.LastReadTime = monthlyBillingData.Last().TimeStamp;
                                                        }
                                                        MIUF.Schedule_MB = true;
                                                        MIUF.last_MB_time = lastTimeUpdate | IsMBUpToDate;
                                                        if (MeterInfo.Schedule_MB.SchType == ScheduleType.IntervalFixed || MeterInfo.Schedule_MB.SchType == ScheduleType.IntervalRandom)
                                                            MIUF.base_time_MB = true;
                                                        if (MeterInfo.Schedule_MB.IsSuperImmediate)
                                                            MIUF.SuperImmediate_MB = true;

                                                        MeterInfo.Schedule_MB.IsSuperImmediate = false;
                                                    }
                                                    IsProcessed = true;
                                                }
                                                catch (Exception ex)
                                                {
                                                    IsProcessed = false;
                                                    LogMessage("Error:" + ex.Message, "MB", ex.Message, 1);
                                                }
                                                finally
                                                {


                                                }
                                            }
                                        }
                                    }
#endif
                                    #endregion
                                }
                                break;
                            case Schedules.LoadProfile:
                                {
                                    #region /// If Task Canceled
                                    if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                                    {
                                        CancelTokenSource.Token.ThrowIfCancellationRequested();
                                    }
                                    #endregion
                                    IsProcessed = ReadLoadProfile(ref MeterInfo.Schedule_LP, MeterInfo.LP_Counters, LoadProfileScheme.Load_Profile, ref MeterInfo.Read_LP, MeterInfo.Save_LP, CancelTokenSource, IsProcessed);
                                }
                                break;
                            case Schedules.LoadProfile2:
                                {
                                    #region /// If Task Canceled
                                    if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                                    {
                                        CancelTokenSource.Token.ThrowIfCancellationRequested();
                                    }
                                    #endregion
                                    IsProcessed = ReadLoadProfile(ref MeterInfo.Schedule_LP2, MeterInfo.LP2_Counters, LoadProfileScheme.Load_Profile_Channel_2, ref MeterInfo.Read_LP2, MeterInfo.Save_LP, CancelTokenSource, IsProcessed);
                                }
                                break;
                            case Schedules.DailyLoadProfile:
                                {
                                    #region /// If Task Canceled
                                    if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                                    {
                                        CancelTokenSource.Token.ThrowIfCancellationRequested();
                                    }
                                    #endregion
                                    IsProcessed = ReadLoadProfile(ref MeterInfo.Schedule_LP3, MeterInfo.LP3_Counters, LoadProfileScheme.Daily_Load_Profile, ref MeterInfo.Read_LP3, MeterInfo.Save_LP3, CancelTokenSource, IsProcessed);
                                }
                                break;
                            case Schedules.PerameterizationWrite:
                                {
                                    #region ///If Task Cancelled
                                    if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                                    {
                                        CancelTokenSource.Token.ThrowIfCancellationRequested();
                                    }
                                    #endregion
                                    #region Parameterization
#if !Enable_LoadTester_Mode


                                    if (!MeterInfo.IsParamEmpty)// || MeterInfo.isPrepaid)
                                    {
                                        try
                                        {
                                            if (TryParameterize(CancelTokenSource))
                                            {
                                                LogMessage("Parameterization is Successful", "PMR", "S");
                                                IsProcessed = true;
                                            }
                                            else if (!MeterInfo.IsParamEmpty) // || MeterInfo.prepaid_request_exist)
                                                LogMessage("Parameterization is Unsuccessful", "PMR", "F");
                                        }
                                        catch (Exception x)
                                        {
                                            throw x;
                                        }
                                    }
#endif
                                    #endregion
                                }
                                break;
                            case Schedules.ParamterizationRead:
                                {
                                    #region ///If Task Cancelled
                                    if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                                    {
                                        CancelTokenSource.Token.ThrowIfCancellationRequested();
                                    }
                                    #endregion
                                    #region Read Parameterization
#if !Enable_LoadTester_Mode

                                    if (MeterInfo.ReadParams && !MeterInfo.PrioritizeWakeup)// || MeterInfo.isPrepaid)
                                    {
                                        try
                                        {
                                            LogMessage("Parameterization is Successful", "RPMR", "R");
                                            if (TryReadParameters(CancelTokenSource))
                                            {
                                                LogMessage("Parameterization is Successful", "RPMR", "S");
                                                IsProcessed = true;
                                            }
                                            else if (!MeterInfo.IsParamEmpty) // || MeterInfo.prepaid_request_exist)
                                                LogMessage("Parameterization is Unsuccessful", "RPMR", "F");
                                        }
                                        catch (Exception x)
                                        {
                                            throw x;
                                        }
                                    }
#endif
                                    #endregion
                                }
                                break;
                            default:
                                break;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                IsProcessed = false;
                custExc.Ex = ex;
            }
            finally
            {
                try
                {
                    if (MeterInfo.MeterType_OBJ == MeterType.KeepAlive)
                    {
                        // Assign Next Call time and update the meter settings
                        MeterInfo.Kas_DueTime = MeterInfo.Kas_NextCallTime;
                        if (MeterInfo.Kas_DueTime != DateTime.MinValue)
                            MIUF.KAS_DueTime = true;
                        else
                            MIUF.KAS_DueTime = false;

                        // MeterInfo.logoutMeter = true;
                        // }
                    }
                    if (MeterInfo.WakeUp_Request_ID > 0)
                    {
                        if (((MIUF.IsPasswordTemporary || MIUF.IsDefaultPassWordActive) ||
                            (!MeterInfo.Schedule_MB.IsSuperImmediate && !MeterInfo.Schedule_EV.IsSuperImmediate &&
                            !MeterInfo.Schedule_CB.IsSuperImmediate && !MeterInfo.Schedule_LP.IsSuperImmediate && !MeterInfo.Schedule_PQ.IsSuperImmediate &&
                            !MeterInfo.Schedule_CS.IsSuperImmediate && !MeterInfo.Apply_new_contactor_state)))
                            DB_Controller.UpdateWakeUpProcess(false, 1, MeterInfo.WakeUp_Request_ID);
                        else
                            DB_Controller.UpdateWakeUpProcess(false, 0, MeterInfo.WakeUp_Request_ID);
                    }

                    DB_Controller.UpdateMeterSettings(MeterInfo, MIUF);
                    MIUF = new MeterInfoUpdateFlags();
                }
                catch (Exception)
                {
                }
            }

            custExc.isTrue = IsProcessed;
            return custExc;
        }

        protected virtual bool ProcessEventsLogRequest(CancellationTokenSource CancelTokenSource, int meterEvetnsCount)
        {
            bool IsProcessed = false;
            if (!MeterInfo.PrioritizeWakeup && MeterInfo.read_individual_events_sch || MeterInfo.Schedule_EV.IsSuperImmediate || (MeterInfo.ReadEventsForcibly && MeterInfo.Read_EV && MeterInfo.Save_EV))
            {
                if (MeterInfo.read_logbook && (MeterInfo.ReadEventsForcibly || MeterInfo.Schedule_EV.IsSuperImmediate ||
                    MeterInfo.IsScheduleReadyToBeProcess(MeterInfo.Schedule_EV)))
                {
                    bool lastTimeUpdate = false;
                    var EventsData = new EventData();
                    //bool IsEventsUpToDate = false;
                    bool IsEventsReadSuccessfully = false;
                    //long MeterCount = 0;
                    long DBCount = MeterInfo.EV_Counters.DB_Counter;
                    try
                    {
                        if (MeterInfo.EV_Counters.DB_Counter >= 0)
                        {
                            DateTime latest_event_logbook = DateTime.MinValue;

                            #region Read Current Counter from Meter

                            uint currentCounter = 0;
                            // var Validator = 0;
                            // uint temp=0;
                            try
                            {
                                currentCounter = Event_Controller.Get_EventCounter_Internal();
                                MeterInfo.EV_Counters.Meter_Counter = currentCounter;

                                #region Events Counter Received from Meter and DBCount
#if Enable_Abstract_Log
                                // LogMessage(String.Format("Events Counter Received from Meter: {0} and DB: {1}", currentCounter, MeterInfo.Counter_Obj.Events_Count), true, 1);
                                // LogMessage(String.Format("Events Counter Received from DB: {0}"), "EVCOUNTDB", MeterInfo.Counter_Obj.Events_Count.ToString(), 1);
#endif

#if !Enable_Abstract_Log
						LogMessage(String.Format("Events Counter Received from Meter: {0} and DBCount: {1}", currentCounter,
                                            MeterInfo.Counter_Obj.Events_Count), 3);
#endif
                                #endregion
                                #region Counter Recheck Algorithm
                                //while (validator++ <3)
                                //{
                                //    currentCounter = Event_Controller.Get_EventCounter_Internal();
                                //    MeterCount = currentCounter;
                                //    temp = Event_Controller.Get_EventCounter_Internal();
                                //    if (currentCounter == temp)
                                //    {
                                //        MeterCount = currentCounter;
                                //        break;
                                //    }
                                //    else if (currentCounter + 3 >= temp)
                                //    {
                                //        currentCounter = temp;
                                //    }
                                //}
                                //if (MeterCount == 0)
                                //{
                                //    MIUF.IsDisableEv = true;
                                //    var err = ", Other " + temp;
                                //    var warning = String.Format("InConsitent Event Counter Received DBCounter:{0}, MeterCounter:{1}{2}, Server is disabling the Events", MeterInfo.Counter_Obj.Events_Count, MeterCount,err);
                                //    LogMessage(warning);
                                //    DB_Controller.InsertWarning(MeterInfo.MSN, MeterInfo.Reference_no, SessionDateTime, ConnectToMeter.ConnectionTime, warning);

                                //    throw new Exception(warning);
                                //}
                                //else
                                //{
                                //    LogMessage(String.Format("Events Counter Received from Meter: {0} and DBCount: {1}", MeterCount,
                                //        MeterInfo.Counter_Obj.Events_Count), 3);
                                //} 
                                #endregion
                            }
                            catch
                            {
                                throw;
                            }

                            #endregion
                            #region Compare Counter and Disable Events

                            bool isEVCountValid = MeterInfo.EV_Counters.IsCounterValid;

                            if (MeterInfo.EV_Counters.IsLowCounter)
                            {
                                MDCAlarm.MDCCombineEvents[(ushort)MDCEvents.low_ev_counter] = true;
                                MDCAlarm.IsMDCEventOuccer = true;

                                var warning = string.Format("Low Event Counter Received MeterCounter:{0}, DBCounter:{1}. Server is Disabling Events", MeterInfo.EV_Counters.Meter_Counter, MeterInfo.EV_Counters.DB_Counter);
                                DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.low_ev_counter), MeterInfo, SessionDateTime);
                            }
                            else if (MeterInfo.EV_Counters.IsHighCounter)
                            {
                                MDCAlarm.MDCCombineEvents[(ushort)MDCEvents.high_ev_counter] = true;
                                MDCAlarm.IsMDCEventOuccer = true;

                                var warning = string.Format("High Event Counter Received MeterCounter:{0}, DBCounter:{1} Maximum Limit Exceeded. Server is Disabling Events", MeterInfo.EV_Counters.Meter_Counter, MeterInfo.EV_Counters.DB_Counter);
                                DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.high_ev_counter), MeterInfo, SessionDateTime);
                            }

                            if (!isEVCountValid)
                            {
                                //Retry
                                MeterInfo.EV_Counters.Meter_Counter = Event_Controller.Get_EventCounter_Internal(); ;

                                #region Events Counter Received on Retry\tMeterCount
#if Enable_Abstract_Log
                                LogMessage(String.Format("Events Counter Received on Retry\tMeterCount: {0}", currentCounter), "ED", string.Format("RR, {0}", currentCounter.ToString()), 3);
#endif

#if !Enable_Abstract_Log
						LogMessage(String.Format("Events Counter Received on Retry\tMeterCount: {0}", currentCounter), 3);
#endif
                                #endregion
                                bool isEVCountValidRetry = MeterInfo.EV_Counters.IsCounterValid;
                                if (!isEVCountValidRetry)
                                {
                                    //Disable EV and Insert Warning
                                    try
                                    {
                                        var msg = String.Format("Invalid Events Counter Received DBCounter:{0}, MeterCounter:{1}, Server is disabling the Events", MeterInfo.EV_Counters.DB_Counter, MeterInfo.EV_Counters.Meter_Counter);
                                        DB_Controller.DBConnect.OpenConnection();
                                        DB_Controller.InsertWarning(MeterInfo.MSN, SessionDateTime, ConnectToMeter.ConnectionTime, msg);
                                        // const QuantityName x = QuantityName.Read_logbook;
                                        // DB_Controller.DisableQuantity(MeterInfo.MSN, Enum.GetName(typeof(QuantityName), x));
                                        #region Disabale Code
#if Enable_Abstract_Log
                                        LogMessage(msg, "ED", String.Format("D, {0}, {1}, {2}", MeterInfo.EV_Counters.Meter_Counter, MeterInfo.EV_Counters.DB_Counter, MeterInfo.EV_Counters.MaxDifferenceCheck), 1);
#endif

#if !Enable_Abstract_Log
						
#endif
                                        #endregion
                                        MIUF.IsDisableEv = true;
                                    }
                                    catch { }
                                    finally
                                    {
                                        DB_Controller.DBConnect.CloseConnection();
                                        MeterInfo.Read_EV = false;
                                    }
                                    goto Exit;
                                }
                            }

                            #endregion
                            #region Making Entry

                            var eventCounter = MeterInfo.EV_Counters;
                            //eventCounter.Previous_Counter = (uint)MeterInfo.Counter_Obj.Events_Count;
                            //eventCounter.Current_Counter = currentCounter;
                            //eventCounter.Max_Size = Limits.Max_Events_Count_Limit;

                            //long old_counter = MeterInfo.Counter_Obj.Events_Count;
                            var e_info = new EventInfo();
                            e_info.EventCode = 0;
                            e_info = Event_Controller.EventLogInfoList.Find((x) => x.EventCode == 0);

                            int difference = eventCounter.Difference;

                            if (eventCounter.IsUptoDate)
                            {
                                //MeterInfo.EvCounterToUpdate = currentCounter;
                                LogMessage("Events Data is up-to-date", "ED", string.Format("U, {0}", eventCounter.DB_Counter), 1);
                                //IsEventsUpToDate = true;
                            }

                            #endregion
                            #region Read

                            if (eventCounter.IsReadable)
                            {
                                Exception innerException = null;
                                try
                                {

                                    #region Reading Events Data from
#if Enable_Abstract_Log
                                    var mCount = eventCounter.Meter_Counter;
                                    var dBCount = eventCounter.DB_Counter;
                                    var reading = (eventCounter.Difference > 100) ? 100 : eventCounter.Difference;
                                    LogMessage(String.Format("Reading Events Data from {0} to {1} {2}", eventCounter.DB_Counter, eventCounter.Meter_Counter, (MeterInfo.ReadEventsForcibly) ? "due to some Major Alarm occurred" : ""), (MeterInfo.ReadEventsForcibly) ? "EDMA" : "ED",
                                        string.Format("R {0}, {1}, {2}, {3}, {4} - {5}", mCount, dBCount, (mCount - dBCount), reading, dBCount, dBCount + reading), 1);
#endif

#if !Enable_Abstract_Log
						LogMessage(String.Format("Reading Events Data from {0} to {1} {2}", eventCounter.Previous_Counter, eventCounter.Current_Counter, (MeterInfo.ReadEventsForcibly) ? "due to some Major Alarm occurred" : ""), 0);
#endif
                                    #endregion
                                    IsEventsReadSuccessfully = Event_Controller.TryReadEventLogDataInChunks(eventCounter, e_info, EventsData,
                                                    (ex) => innerException = ex, 25, CancelTokenSource);

                                    if (IsEventsReadSuccessfully)
                                    {
                                        #region Events Data read complete
#if Enable_Abstract_Log
                                        LogMessage("Events Data read complete", (MeterInfo.ReadEventsForcibly) ? "EDMA" : "ED", "S", 3);
#endif

#if !Enable_Abstract_Log
						 LogMessage("Events Data read complete",3)
#endif
                                        #endregion
                                        //Update Event For Live-Update

                                        //EventIDtoCode idTOcode = new EventIDtoCode();
                                        DateTime latest = DateTime.MinValue;
                                        int id = 0;
                                        bool retry = true;
                                        EventItem data = null;

                                        EventData copy_data = EventsData.Clone();

                                        #region Contactor Status Update
                                        var lastState = (from ev in copy_data.EventRecords
                                                         where ev.EventInfo.EventCode == 213 || ev.EventInfo.EventCode == 214
                                                         orderby ev.EventDateTimeStamp descending
                                                         select ev).FirstOrDefault();

                                        if (lastState != null)
                                        {
                                            var status = -1;
                                            if (lastState.EventInfo.EventCode == 213)
                                                status = 1;
                                            else
                                                status = 0;
                                            MeterInfo.Current_contactor_status = status;
                                            MeterInfo.Schedule_CO.LastReadTime = DateTime.Now;
                                            MIUF.IsContactorStatusUpdate = true;
                                        }
                                        #endregion

                                        while (retry && copy_data.EventRecords.Count > 0)
                                        {
                                            id = 0;
                                            latest = copy_data.EventRecords.Max(x => x.EventDateTimeStamp);
                                            data = copy_data.EventRecords.Find(x => x.EventDateTimeStamp == latest);

                                            //id = idTOcode.getEventID(data.EventInfo.EventCode);
                                            if (data != null && data.EventInfo != null)
                                                id = (int)(data.EventInfo.EventId ?? 0);

                                            MeterInfo.eventsForLiveUpdate_logbook = Commons.HexStringToBinary(MeterInfo.eventsForLiveUpdate_logbook_string, meterEvetnsCount);
                                            if (id > 0 && MeterInfo.eventsForLiveUpdate_logbook[id - 1] == '1')
                                            {
                                                retry = false;
                                            }
                                            else
                                            {
                                                copy_data.EventRecords.Remove(data);
                                            }
                                        }
                                        if (!retry)
                                        {
                                            if (DB_Controller.UpdateLastEvent_Live_logbook(MeterInfo.MSN, data.EventInfo.EventCode, data.EventDateTimeStamp))
                                            {
                                                #region Logbook->Event Code " + data.EventInfo.EventCode + " updated to Instantaneous Live
#if Enable_Abstract_Log
                                                LogMessage("Logbook->Event Code " + data.EventInfo.EventCode + " updated to Instantaneous Live ", "LEDL", data.EventInfo.EventCode.ToString(), 1);
#endif

#if !Enable_Abstract_Log
						LogMessage("Logbook->Event Code " + data.EventInfo.EventCode + " updated to Instantaneous Live ");
#endif
                                                #endregion
                                            }
                                            else
                                            {
                                                #region Error occurred while updating Event Code
#if Enable_Abstract_Log
                                                LogMessage("Error occurred while updating Event Code " + data.EventInfo.EventCode + " updated to Instantaneous Live ", "LEDL", "F", 1);
#endif
#if !Enable_Abstract_Log
						LogMessage("Error occurred while updating Event Code " + data.EventInfo.EventCode + " updated to Instantaneous Live ");
#endif
                                                #endregion
                                            }
                                        }
                                        else
                                        {
                                            #region No events to read from the logbook
#if Enable_Abstract_Log
                                            LogMessage("No events to read from the logbook", "LEDL", "EMPT", 1);
#endif

#if !Enable_Abstract_Log
						 LogMessage("No events to read from the logbook");
#endif
                                            #endregion
                                        }
                                    }
                                    if (!IsEventsReadSuccessfully && innerException != null)
                                        throw innerException;
                                }
                                catch (Exception ex)
                                {
                                    LogMessage("Error while reading Events Data: " + ex.Message, 4);
                                    throw;
                                }
                                if (innerException != null)
                                {
                                    throw innerException;
                                }
                            }

                            #endregion
                        }
                    }
                    catch (Exception ex)
                    {
                        IsProcessed = false;
                        LogMessage(ex, 4, "Combine Events Log");
                        //if (!(ex is NullReferenceException))
                        //    throw;
                        if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                        StatisticsObj.InsertError(ex, _session_DateTime, 15);
                    }
                    finally
                    {
                        // MeterInfo.ReadEventsForcibly = false;
                        #region Saving To Database
                        try
                        {
                            if (IsEventsReadSuccessfully)
                                ResetMaxIOFailure();
                            if (!MeterInfo.EV_Counters.IsUptoDate && IsEventsReadSuccessfully)
                            {
                                if (MeterInfo.Save_EV && EventsData != null && EventsData.MaxEventCounter > 0)
                                {
                                    uint tempMAXEventCount = EventsData.MaxEventCounter;
                                    CustomException CEX = DB_Controller.saveEventsDataWithReplace(EventsData, MeterInfo.MSN, SessionDateTime, MeterInfo, MIUF);

                                    if (CEX != null && CEX.isTrue && CEX.Ex == null)
                                    {
                                        MeterInfo.EV_Counters.DB_Counter = tempMAXEventCount;

                                        if (CEX.SomeNumber > MeterInfo.EV_Counters.Meter_Counter || CEX.SomeNumber < DBCount || !CEX.SomeMessage.Equals("Nothing", StringComparison.OrdinalIgnoreCase))
                                        {
                                            #region Saving Events Data failed
#if Enable_Abstract_Log
                                            LogMessage(String.Format("Saving Events Data failed, {0} (Error Code:{1})", CEX.SomeMessage, (int)MDCErrors.App_Events_Data_Save), "EDD", "F", 1);
#endif

#if !Enable_Abstract_Log
						 LogMessage(String.Format("Saving Events Data failed, {0} (Error Code:{1})", CEX.SomeMessage, (int)MDCErrors.App_Events_Data_Save));
#endif
                                            #endregion
                                        }
                                        else
                                        {
                                            #region Events Data saved successfully
#if Enable_Abstract_Log
                                            LogMessage(String.Format("Events Data saved successfully and updated to count {0}, Internal Message: " + CEX.SomeMessage, CEX.SomeNumber), "EDD", string.Format("S, {0}", CEX.SomeNumber.ToString()), 1);
#endif

#if !Enable_Abstract_Log
						 LogMessage(String.Format("Events Data saved successfully and updated to count {0}, Internal Message: " + CEX.SomeMessage, CEX.SomeNumber), 2);
#endif
                                            #endregion
                                            lastTimeUpdate = true;
                                        }
                                    }
                                    else
                                    {
                                        if (CEX.SomeMessage.Contains(string.Format("Error:{0}", (int)MDCErrors.DB_Duplicate_Entery)))
                                        {
                                            #region Events Counter difference check w.r.t database count

                                            DB_Controller.DBConnect.OpenConnection();
                                            var warning = String.Format("Invalid Event Counter Received DBCounter:{0}, MeterCounter:{1}, Server is disabling the Events", MeterInfo.EV_Counters.DB_Counter, MeterInfo.EV_Counters.Meter_Counter);
                                            #region Server is disabling the Events
#if Enable_Abstract_Log
                                            LogMessage(warning, "ED", string.Format("D, {0},{1},{2}", MeterInfo.EV_Counters.Meter_Counter, MeterInfo.EV_Counters.DB_Counter, MeterInfo.EV_Counters.MinDifferenceCheck), 1);
#endif
#if !Enable_Abstract_Log
						LogMessage(warning);
#endif
                                            #endregion
                                            DB_Controller.InsertWarning(MeterInfo.MSN, SessionDateTime, ConnectToMeter.ConnectionTime, warning);
                                            MIUF.IsDisableEv = true;
                                            MDCAlarm.MDCCombineEvents[(ushort)MDCEvents.ev_counter_mismatch] = true;
                                            MDCAlarm.IsMDCEventOuccer = true;
                                            DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.ev_counter_mismatch), MeterInfo, SessionDateTime);
                                            #endregion
                                        }
                                        else
                                        {
                                            var Error = "Saving Events Data failed";
                                            if (CEX != null && CEX.Ex != null)
                                                Error += " Error: " + CEX.Ex.Message;
                                            #region Saving Events Data failed
#if Enable_Abstract_Log
                                            LogMessage(Error, "EDD", "F", 4);
#endif
#if !Enable_Abstract_Log
						LogMessage(Error, 4);
#endif
                                            #endregion
                                        }
                                    }
                                }
                            }
                            if ((MeterInfo.EV_Counters.IsUptoDate || IsEventsReadSuccessfully) && MeterInfo.Save_EV)//!MeterInfo.read_individual_events_sch)change by furqan
                            {
                                MeterInfo.PreUpdateSchedule(MeterInfo.Schedule_EV, SessionDateTime);
                                MIUF.Schedule_EV = true;
                                MIUF.last_EV_time = lastTimeUpdate | MeterInfo.EV_Counters.IsUptoDate;
                                if (MeterInfo.Schedule_EV.IsSuperImmediate)
                                    MIUF.SuperImmediate_EV = true;
                                if (MeterInfo.Schedule_EV.SchType == ScheduleType.IntervalFixed ||
                                    MeterInfo.Schedule_EV.SchType == ScheduleType.IntervalRandom)
                                    MIUF.base_time_EV = true;
                                MeterInfo.Schedule_EV.IsSuperImmediate = false;
                            }
                            IsProcessed = true;
                        }
                        catch (Exception ex)
                        {
                            LogMessage(ex.InnerException, 4);
                        }
                        #endregion
                    }
                Exit:
                    #region Events Disabled
#if Enable_Abstract_Log
                    // if (!MeterInfo.Read_EV)
                    // LogMessage("Events Disabled", "SVRDSEVIVCOUNT", "DSEV", 1);
                    ;
#endif

#if !Enable_Abstract_Log
                            if (!MeterInfo.Read_EV)
						LogMessage("Events Disabled", 3);
#endif
                    #endregion
                }
            }

            return IsProcessed;
        }

        private bool ReadAndSaveCummBilling(bool IsSinglePhase)
        {
            bool IsProcessed = false;
            try
            {
                BillingData billData = null;
                Cumulative_billing_data data = null;
                bool lastTimeUpdate = false;
                #region Reading Cumulative Billing Data
#if Enable_Abstract_Log
                LogMessage("Reading Cumulative Billing Data", "CB", "R", 0);
#endif
#if !Enable_Abstract_Log
						        LogMessage("Reading Cumulative Billing Data");
#endif
                #endregion

                // Only One Get packet billing required
                if (MeterInfo.DDS110_Compatible)
                {
                    data = Billing_Controller.ReadCummulativeBillingDataByOBISList();
                }
                else if (IsSinglePhase)
                {
                    billData = Billing_Controller.GetCumulativeBillingData(true);
                }
                else //if (MeterInfo.DetailedBillingID == 0)
                {
                    billData = Billing_Controller.GetCumulativeBillingData(MeterInfo.Read_CB);
                }
                // Detailed billing by getting tariffs detail from meter
                //else
                //{
                //    GetTariff tariffDetails = DB_Controller.getTariffDetails(MeterInfo.DetailedBillingID);
                //    if (tariffDetails != null)
                //        billData = Billing_Controller.GetCumulativeBillingData_KESC(tariffDetails);
                //    else
                //    {
                //        throw new Exception("No Tariff details found");
                //    }
                //}

                #region Reading Cumulative billing Data completed
#if Enable_Abstract_Log
                LogMessage("Reading Cumulative billing Data completed", "CB", "S", 1);
#endif
#if !Enable_Abstract_Log
						        LogMessage("Reading Cumulative billing Data completed",1);
#endif
                #endregion
                DB_Controller.DBConnect.OpenConnection();

                if (!MeterInfo.DDS110_Compatible && billData != null)
                    data = Billing_Controller.saveToClass(billData, MeterInfo.MSN);

                if (data != null && !String.IsNullOrEmpty(data.DBColumns.ToString()) && !String.IsNullOrEmpty(data.DBValues.ToString()))
                {
                    if (MeterInfo.Save_CB)
                    {
                        #region Saving
                        /*string log = $"MeterInfo MSN : {MeterInfo.MSN}, MeterSerialObjectMSN: {connectToMeter.MeterSerialNumberObj.ToString()}" +
                            $"columns: {data.DBColumns}, Values: {data.DBValues}";
                        LogMessage("",log,"I",2);*/
                        if (MeterInfo.MSN != connectToMeter.MeterSerialNumberObj.ToString())
                        {
                            MeterInfo = DB_Controller.GetMeterSettings(connectToMeter.MeterSerialNumberObj.ToString());
                        }
                        bool saveFlag = DB_Controller.saveCumulativeBillingDataEx(data, SessionDateTime, MeterInfo);

                        //if (MeterInfo.DDS110_Compatible && data != null)
                        //{
                        //    LogMessage("Saving Cumulative billing Data", "CBD_CB", "R", 2);
                        //    saveFlag = DB_Controller.saveCumulativeBillingDataEx(data, SessionDateTime, MeterInfo);
                        //    LogMessage("Saving Cumulative billing Data", "CBD_CB", "S", 1);
                        //}
                        //else if (MeterInfo.DetailedBillingID == 0)
                        //{

                        //    //saveFlag = DB_Controller.saveCumulativeBillingData(data, SessionDateTime, MeterInfo);
                        //}
                        //else
                        //{
                        //    saveFlag = DB_Controller.saveCumulativeBillingData_KESC(billData, SessionDateTime, MeterInfo.MSN, MeterInfo);
                        //}
                        lastTimeUpdate = saveFlag;

                        if (saveFlag)
                        {
                            // MeterInfo.Schedule_CB.IsSuperImmediate = false;

                            #region Saving Cumulative billing Data completed
#if Enable_Abstract_Log
                            LogMessage("Saving Cumulative billing Data completed", "CBD", "S", 2);
#endif
#if !Enable_Abstract_Log
						    LogMessage("Saving Cumulative billing Data completed",2);
#endif
                            #endregion
                            if (MeterInfo.isPrepaid && DB_Controller.saveCumulativeBillingForPrepaid(data, SessionDateTime, MeterInfo))
                            {
                                #region Cumulative Billing Data saved for Prepaid
#if Enable_Abstract_Log
                                LogMessage("Cumulative Billing Data saved for Prepaid", "CBPD", "S", 1);
#endif

#if !Enable_Abstract_Log
						LogMessage("Cumulative Billing Data saved for Prepaid",1);
#endif
                                #endregion
                            }
                        }
                        else
                        {
                            #region Error Occurred while Saving Cumulative billing Data
#if Enable_Abstract_Log
                            LogMessage("Error Occurred while Saving Cumulative billing Data", "CBD", "F", 1);
#endif

#if !Enable_Abstract_Log
						LogMessage("Error Occurred while Saving Cumulative billing Data", 2);
#endif
                            #endregion
                        }

                        #endregion

#if Enable_Abstract_Log
                        LogMessage("Saving Cumulative billing Data completed", "CBD", "S", 2);
#endif
                        if (MeterInfo.EnableLiveUpdate)
                        {
                            //LogMessage("Saving Instantaneous Data Live(Cumm)", "CBLD_ID", "R", 2);
                            //Saves Activ e and Reactive Energies in Instantaneosu Data Live 
                            //DB_Controller.UpdateCumulativeEnergy_Live(MeterInfo.MSN, data.activeEnergy_TL, data.reactiveEnergy_TL, _session_DateTime);
                            //LogMessage("Cumulative Billing Data Live (Inst) saved", "CBLD_ID", "S", 1);

                            //Saves Cumm BIlling Data in CUmulative Data Live
                            LogMessage("Saving Cumulative billing Data Live(Cumm)", "CBLD", "R", 2);
                            DB_Controller.UpdateCumulativeEnergy_Live(data, _session_DateTime, MeterInfo);
                            LogMessage("Cumulative Billing Data Live(Cumm) saved", "CBLD", "S", 1);
                            //End
                        }
                    }
                    MeterInfo.PreUpdateSchedule(MeterInfo.Schedule_CB, SessionDateTime);
                    if (MeterInfo.Schedule_CB.SchType == ScheduleType.Disabled)
                        MIUF.Schedule_CB = true;
                    MIUF.last_CB_time = lastTimeUpdate;
                    if (MeterInfo.Schedule_CB.IsSuperImmediate)
                        MIUF.SuperImmediate_CB = true;
                    if (MeterInfo.Schedule_CB.SchType == ScheduleType.IntervalFixed ||
                        MeterInfo.Schedule_CB.SchType == ScheduleType.IntervalRandom)
                        MIUF.base_time_CB = true;
                    MeterInfo.Schedule_CB.IsSuperImmediate = false;
                    IsProcessed = true;
                }
            }
            catch (Exception ex)
            {
                IsProcessed = false;
                LogMessage(ex, 4, "Cumulative Billing Data");
                // if (!(ex is NullReferenceException))
                //     throw ex;
                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                StatisticsObj.InsertError(ex, _session_DateTime, 15);
            }
            finally
            {
                if (IsProcessed)
                    ResetMaxIOFailure();
                DB_Controller.DBConnect.CloseConnection();
            }

            return IsProcessed;
        }

        private bool ReadContactorStatus(bool IsProcessed)
        {
            #region ContactorStatus reading

            if (MeterInfo.Schedule_CO.IsSuperImmediate || (MeterInfo.Read_CO && MeterInfo.Schedule_CO.SchType != ScheduleType.Disabled && MeterInfo.IsScheduleReadyToBeProcess(MeterInfo.Schedule_CO)))
            {
                try
                {
                    #region Reading Contactor Status
#if Enable_Abstract_Log
                    LogMessage("Reading Contactor Status", "COS", "R", 0);
#endif
#if !Enable_Abstract_Log
						        LogMessage("Reading Contactor Status");
#endif
                    #endregion
                    //_AP_Controller.ARLRQ();
                    bool currentStatus = _Param_Controller.GET_Relay_Status();
                    if (Convert.ToBoolean(MeterInfo.Current_contactor_status) != currentStatus)
                    {
                        MeterInfo.Current_contactor_status = currentStatus ? 1 : 0;
                        MIUF.IsContactorStatusUpdate = true;
                        MeterInfo.Schedule_CO.LastReadTime = DateTime.Now;
                    }

                    #region Reading Contactor Status completed
#if Enable_Abstract_Log
                    LogMessage("Reading Contactor Status completed", "COS", "S: " + (currentStatus ? "ON" : "OFF"), 1);
#endif
#if !Enable_Abstract_Log
						        LogMessage("Reading Contactor Status completed",1);
#endif
                    #endregion
                    MeterInfo.PreUpdateSchedule(MeterInfo.Schedule_CO, SessionDateTime);
                    if (MeterInfo.Schedule_CO.SchType == ScheduleType.Disabled)
                        MIUF.Schedule_CO = true;
                    if (MeterInfo.Schedule_CO.IsSuperImmediate)
                        MIUF.SuperImmediate_CO = true;
                    if (MeterInfo.Schedule_CO.SchType == ScheduleType.IntervalFixed || MeterInfo.Schedule_CO.SchType == ScheduleType.IntervalRandom)
                        MIUF.base_time_CO = true;
                    MeterInfo.Schedule_CO.IsSuperImmediate = false;
                    IsProcessed = true;

                }
                catch (Exception ex)
                {
                    LogMessage(ex, 4, "Contactor Status");

                    // if (!(ex is NullReferenceException))
                    //     throw ex;

                    if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                    StatisticsObj.InsertError(ex, _session_DateTime, 15);
                }
                finally
                {
                    if (IsProcessed)
                        ResetMaxIOFailure();
                }
            }

            #endregion
            return IsProcessed;
        }

        private bool ReadLoadProfile(ref Schedule LP_Schedule, Profile_Counter LP_Counters, LoadProfileScheme LP_Scheme, ref READ_METHOD Read_LP, bool Save_LP, CancellationTokenSource CancelTokenSource, bool IsProcessed)
        {
            #region LoadProfileData

            ///Load Profile

            if (LP_Schedule.IsSuperImmediate || (!MeterInfo.PrioritizeWakeup && LP_Schedule.SchType != ScheduleType.Disabled && LP_Counters.DB_Counter >= 0 && Read_LP != READ_METHOD.Disabled && Save_LP))
            {
                if (LP_Schedule.IsSuperImmediate || MeterInfo.IsScheduleReadyToBeProcess(LP_Schedule))
                {
                    #region Declaration and Initialization
                    LoadProfileData loadData = new LoadProfileData();
                    var lastTimeUpdate = false;
                    uint CountLastUpdated;
                    bool IsLPDataRead = false;
                    uint LPIterationCounter = 1;
                    bool IsLPUpdated = false;
                    LoadProfile_Controller.LoadProfileInformation = new GenericProfileInfo();
                    LoadProfile_Controller.UpdateChannelInfo = true;
                    GenericProfileInfo loadProfileInfo = new GenericProfileInfo();

                    #endregion
                    try
                    {
#if Enable_Abstract_Log
                        LogMessage(String.Format("Load Profile Scheme: {0}", (byte)LP_Scheme), "LPS", string.Format("{0}", (byte)LP_Scheme), 1);
#endif

                        if (LP_Counters.DB_Counter >= 0 && LP_Counters.Max_Size > 0)
                        {
                            if (Read_LP == READ_METHOD.ByCounter)
                            {
                                #region Read By Counter
                                #region Initial work
                                /// Reset Load Profile Generic Info For New Session
                                loadProfileInfo = LoadProfile_Controller.Get_LoadProfileInternal_Counter(LP_Scheme);
                                LP_Counters.Meter_Counter = loadProfileInfo.EntriesInUse;
                                #region LogMessage(String.Format("Load Profile Counter Received from Meter: {0}", loadProfileInfo.EntriesInUse), 3);
#if Enable_Abstract_Log
                                // LogMessage(String.Format("Load Profile Counter Received from Meter: {0}", loadProfileInfo.EntriesInUse), false, 1);
                                // StatisticsObj.InsertLog("LPCOUNTDB",LP_Counters.DB_Counter.ToString());
#endif

#if !Enable_Abstract_Log
						LogMessage(String.Format("Load Profile Counter Received from Meter: {0}", loadProfileInfo.EntriesInUse), 3);
#endif
                                #region Compare Counter and Disable Load Profile

                                var isLpCountValid = LP_Counters.IsCounterValid;
                                //MDC Alarms added 
                                if (LP_Counters.IsLowCounter)
                                {
                                    MDCAlarm.MDCCombineEvents[(ushort)MDCEvents.low_lp_counter] = true;
                                    MDCAlarm.IsMDCEventOuccer = true;

                                    var warning = string.Format("Low LP Counter Received MeterCounter:{0}, DBCounter:{1}. Server is Disabling Load Profile {2}", loadProfileInfo.EntriesInUse, LP_Counters.DB_Counter, (byte)LP_Scheme);
                                    DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.low_lp_counter), MeterInfo, SessionDateTime);
                                }
                                else if (LP_Counters.IsHighCounter)
                                {
                                    MDCAlarm.MDCCombineEvents[(ushort)MDCEvents.high_lp_counter] = true;
                                    MDCAlarm.IsMDCEventOuccer = true;

                                    var warning = string.Format("High LP Counter Received MeterCounter:{0}, DBCounter:{1} Maximum limit Exceeded. Server is Disabling Load Profile {2}", loadProfileInfo.EntriesInUse, LP_Counters.DB_Counter, (byte)LP_Scheme);
                                    DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.high_lp_counter), MeterInfo, SessionDateTime);
                                }

                                if (!isLpCountValid)
                                {
                                    //Retry
                                    loadProfileInfo = LoadProfile_Controller.Get_LoadProfileInternal_Counter(LP_Scheme);
                                    LP_Counters.Meter_Counter = loadProfileInfo.EntriesInUse;
                                    #region LogMessage(String.Format("Load Profile Counter Received on Retry\tMeterCount: {0}", loadProfileInfo.EntriesInUse), 3);
#if Enable_Abstract_Log
                                    LogMessage(String.Format("Load Profile Counter Received on Retry\tMeterCount: {0}", loadProfileInfo.EntriesInUse), "LP", string.Format("RR, {0}", loadProfileInfo.EntriesInUse.ToString()), 1);
#endif

#if !Enable_Abstract_Log
						LogMessage(String.Format("Load Profile Counter Received on Retry\tMeterCount: {0}", loadProfileInfo.EntriesInUse), 3);
#endif
                                    #endregion
                                    bool isLpCountValidRetry = LP_Counters.IsCounterValid;
                                    if (!isLpCountValidRetry)
                                    {
                                        //Disable LP and Insert Warning
                                        try
                                        {
                                            DB_Controller.DBConnect.OpenConnection();
                                            var msg = String.Format("Invalid Load Profile Counter Received DBCounter:{0}, MeterCounter:{1}, Server is disabling the Load Profile {2}", LP_Counters.DB_Counter, loadProfileInfo.EntriesInUse, LP_Scheme);
                                            #region  LogMessage(msg, 4);
#if Enable_Abstract_Log
                                            LogMessage(msg, "LP", string.Format("D, {0}, {1}, {2}", loadProfileInfo.EntriesInUse, LP_Counters.DB_Counter, LP_Counters.MaxDifferenceCheck), 4);
#endif

#if !Enable_Abstract_Log
						 LogMessage(msg, 4);
#endif
                                            #endregion
                                            DB_Controller.InsertWarning(MeterInfo.MSN, SessionDateTime, ConnectToMeter.ConnectionTime, msg);
                                            //const QuantityName x = QuantityName.Read_LP;
                                            //DB_Controller.DisableQuantity(MeterInfo.MSN, Enum.GetName(typeof(QuantityName), x));
                                            #region Disable LP
                                            LP_Schedule.IsDisable = true;
                                            //if (LP_Scheme == LoadProfileScheme.Scheme_1)

                                            //    MIUF.IsDisableLP = true;
                                            //else if (LP_Scheme == LoadProfileScheme.Scheme_2)
                                            //    MIUF.IsDisableLP2 = true;
                                            #endregion
                                        }
                                        catch { }
                                        finally
                                        {
                                            DB_Controller.DBConnect.CloseConnection();
                                            Read_LP = READ_METHOD.Disabled;
                                        }
                                        goto Exit;
                                    }
                                }

                                #endregion
                                #endregion

                                //long dbCounter =LP_Counters.DB_Counter;
                                //int difference = (int)loadProfileInfo.EntriesInUse - (int)dbCounter;
                                #endregion

                                #region If Counter Less Than Min Diff
                                if (LP_Counters.IsEqual)
                                {
                                    IsLPUpdated = true;
                                    LogMessage("Load Profile Data Is UP-To-Date", "LP", string.Format("U, {0}", LP_Counters.DB_Counter), 1);
                                    LP_Counters.CounterToUpdate = loadProfileInfo.EntriesInUse;
                                }
                                if (LP_Counters.IsLessThanMinDifference)
                                {
                                    if (!LP_Counters.IsEqual)
                                    {
                                        #region LogMessage(String.Format("Load Profile difference is less than the Min Limit"), 3);
#if Enable_Abstract_Log

                                        LogMessage("Load Profile difference is less than the Min Limit", "LP", string.Format("D, {0}, {1}, {2}", loadProfileInfo.EntriesInUse, LP_Counters.DB_Counter, LP_Counters.MinDifferenceCheck), 1);
#endif

#if !Enable_Abstract_Log
						LogMessage(String.Format("Load Profile difference is less than the Min Limit"), 3);
#endif
                                        #endregion
                                        #region Disable LP
                                        LP_Schedule.IsDisable = true;
                                        //if (LP_Scheme == LoadProfileScheme.Scheme_1)
                                        //    MIUF.IsDisableLP = true;
                                        //else if (LP_Scheme == LoadProfileScheme.Scheme_2)
                                        //    MIUF.IsDisableLP2 = true;
                                        #endregion
                                    }

                                    MeterInfo.PreUpdateSchedule(LP_Schedule, SessionDateTime);
                                    goto Exit;
                                }
                                #endregion

                                #endregion
                            }


                            if (Read_LP == READ_METHOD.ByDateTime || (!LP_Counters.IsEqual && LP_Counters.Difference > 0))
                            {
                                #region Read Load Profile data

                                #region Get Load Profile Channels Info

                                List<LoadProfileChannelInfo> _loadProfileChannelsInfo = LoadProfile_Controller.GetChannelsInfoList(LP_Counters, LP_Scheme);

                                #endregion

                                bool IsLPReadAble = true;
                                // Iterate and save LP to prevent software from Stack Overflow

                                Exception InnerException = null;
                                bool IsSuccess = false;
                                Load_Profile toSaveloadData = null;
                                CustomException custException = null;


                                //while ((LP_Counters.DB_Counter < LP_Counters.Meter_Counter) && IsLPReadAble && (LPIterationCounter++ <= MaxLPIterations))
                                {
                                    try
                                    {
                                        if (loadData.ChannelsInstances != null)
                                            loadData.ChannelsInstances.Clear();//Reset channel instances already read

                                        //For Safe Hand Break the
                                        if (Read_LP == READ_METHOD.ByCounter)
                                        {
                                            if (LP_Counters.IsEqual)
                                            {
                                                IsLPReadAble = false;
                                                return true;
                                            }

                                            #region LogMessage(String.Format("Reading Load Profile Data From {0} to {1}",LP_Counters.DB_Counter, MeterCount));

#if Enable_Abstract_Log

                                            var mt = LP_Counters.Meter_Counter;
                                            var db = LP_Counters.DB_Counter;
                                            var reading = (((mt - db) > LP_Counters.ChunkSize) ? LP_Counters.ChunkSize : (mt - db));
                                            if (MeterInfo.EnableEchoLog)
                                                LogMessage(String.Format("Reading Load Profile Data From {0} to {1}", db, mt), "LP",
                                                    string.Format("R {0}, {1}, {2}, {3}, {4} - {5}", mt, db, (mt - db), reading, db, db + reading), 1);
#endif

#if !Enable_Abstract_Log
						 LogMessage(String.Format("Reading Load Profile Data From {0} to {1}",LP_Counters.DB_Counter, MeterCount));
#endif
                                            #endregion

                                            loadData.ChannelGroupId = 0;
                                            IsSuccess = LoadProfile_Controller.TryGet_LoadProfileDataInChunks(LP_Scheme, LP_Counters, loadData, _loadProfileChannelsInfo, (x) => InnerException = x, LP_Counters.ChunkSize, 2, CancelTokenSource);

                                            #region Compare Counter again before saving and updating in DB
                                            uint tempMaxLoadProfileCount = loadData.MaxCounter;
                                            if (loadData.CounterAvailable && (tempMaxLoadProfileCount - (uint)LP_Counters.DB_Counter > LP_Counters.MaxDifferenceCheck ||
                                                                                    (uint)LP_Counters.DB_Counter > tempMaxLoadProfileCount ||
                                                                                    tempMaxLoadProfileCount > LP_Counters.Meter_Counter))
                                            {
                                                if (tempMaxLoadProfileCount - 1 == LP_Counters.Meter_Counter)
                                                {
                                                    #region Counter Shift during Read handling

                                                    #region LogMessage("Meter Load Profile buffer updated during read server is reading clipped data");
#if Enable_Abstract_Log
                                                    LogMessage("Meter Load Profile buffer updated during read server is reading clipped data", "LP", "RR, 1", 1);
#endif

#if !Enable_Abstract_Log
						LogMessage("Meter Load Profile buffer updated during read server is reading clipped data");
#endif
                                                    #endregion
                                                    LoadProfile_Controller.LoadProfileInformation = new GenericProfileInfo();
                                                    LoadProfile_Controller.UpdateChannelInfo = true;

                                                    //Reset channel instances already read
                                                    LP_Counters.Meter_Counter = (uint)LP_Counters.DB_Counter + 1;
                                                    //LP_Counters.DB_Counter = (uint)LP_Counters.DB_Counter;
                                                    // dbCounter = (uint)LP_Counters.DB_Counter;
                                                    //MeterCount = tempMaxLoadProfileCount;
                                                    var LData = new LoadProfileData();
                                                    if (LData.ChannelsInstances != null)
                                                        LData.ChannelsInstances.Clear();


                                                    IsSuccess = LoadProfile_Controller.TryGet_LoadProfileDataInChunks(LP_Scheme, LP_Counters, LData, _loadProfileChannelsInfo, (x) => InnerException = x, 1, 2, CancelTokenSource);
                                                    if (LData.ChannelsInstances.Count > 0)
                                                    {
                                                        loadData.ChannelsInstances.Add(LData.ChannelsInstances[0]);
                                                        loadData.ChannelsInstances = new List<LoadProfileItem>(loadData.ChannelsInstances.OrderBy(x => x.Counter));
                                                    }
                                                    #endregion
                                                }
                                                else
                                                {
                                                    #region Invalid Counter Check For LP data Read By Meter

                                                    if (InnerException != null)
                                                        LogMessage(InnerException);
                                                    var warning = String.Format("Invalid LP Counter Received in Data --> DBCounter:{0} and NewCounter:{1} Server is Disabling Load Profile", LP_Counters.DB_Counter, tempMaxLoadProfileCount);
                                                    #region LogMessage(warning, 3);
#if Enable_Abstract_Log
                                                    LogMessage(warning, "LP", string.Format("F, {0}, {1}", tempMaxLoadProfileCount, LP_Counters.DB_Counter), 1);
#endif

#if !Enable_Abstract_Log
						LogMessage(warning, 3);
#endif
                                                    #endregion
                                                    IsLPReadAble = false;//Terminate the iteration
                                                    // DB_Controller.InsertWarning(MeterInfo.MSN, MeterInfo.Reference_no, SessionDateTime, ConnectToMeter.ConnectionTime, warning);
                                                    // MIUF.IsDisableLP = true;
                                                    #region Update Load Profile COunter Flag
                                                    LP_Counters.UpdateCounter = false;
                                                    //if (LP_Scheme == LoadProfileScheme.Scheme_1)
                                                    //    MIUF.UpdateLoadProfileCounter = false;
                                                    //else if (LP_Scheme == LoadProfileScheme.Scheme_2)
                                                    //    MIUF.UpdateLoadProfile2Counter = false;

                                                    #endregion
                                                    MDCAlarm.MDCCombineEvents[(ushort)MDCEvents.lp_counter_mismatch] = true;
                                                    MDCAlarm.IsMDCEventOuccer = true;
                                                    DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.lp_counter_mismatch), MeterInfo, SessionDateTime);
                                                    //if (LP_Schedule.IsSuperImmediate) MIUF.SuperImmediate_LP = true;
                                                    //LP_Schedule.IsSuperImmediate = false;
                                                    IsProcessed = true;
                                                    return IsProcessed;
                                                    #endregion
                                                }
                                            }
                                            #endregion

                                        }
                                        else if (Read_LP == READ_METHOD.ByDateTime)
                                        {
                                            loadData.ChannelGroupId = 0;
                                            if (LP_Counters.ReadInstant && LP_Counters.LastReadTime < LP_Counters.InstantReadTime)
                                            {
                                                LP_Counters.LastReadTime = LP_Counters.InstantReadTime;
                                            }
                                            else if (LP_Counters.LastReadTime < LP_Counters.MaxEntriesTime)
                                            {
                                                LP_Counters.LastReadTime = LP_Counters.MaxEntriesTime;
                                            }
                                            if (MeterInfo.EnableEchoLog)
                                                LogMessage(String.Format("Reading Load Profile Data From {0}", LP_Counters.LastReadTime.ToString(Commons.DateTimeFormat)), "LP",
                                                    string.Format("R {0}", LP_Counters.LastReadTime.ToString(Commons.DateTimeFormat)), 1);
                                            IsSuccess = LoadProfile_Controller.TryGet_LoadProfileDataByDateTime(LP_Scheme, LP_Counters, loadData, _loadProfileChannelsInfo, (x) => InnerException = x, CancelTokenSource);

                                        }
                                        // var abc = loadData.ChannelGroupId;
                                        if (loadData.ChannelsInstances != null && loadData.ChannelsInstances.Count > 0)
                                            IsLPDataRead = true;

                                        #region Successful/ Unsuccessful

                                        if (IsSuccess)
                                        {
                                            #region LogMessage("Reading Load Profile Data Complete", 1);
#if Enable_Abstract_Log
                                            LogMessage("Reading Load Profile Data Complete", "LP", "S", 1);
#endif

#if !Enable_Abstract_Log
						LogMessage("Reading Load Profile Data Complete", 1);
#endif
                                            #endregion


                                        }
                                        else
                                        {
                                            #region  LogMessage(string.Format("Error while reading complete Load Profile Data (Error Code:{0})", (int)MDCErrors.App_Load_Profile_Read), 1);
#if Enable_Abstract_Log
                                            LogMessage(string.Format("Error while reading complete Load Profile Data (Error Code:{0})", (int)MDCErrors.App_Load_Profile_Read), "LP", "F", 1);
#endif

#if !Enable_Abstract_Log
						 LogMessage(string.Format("Error while reading complete Load Profile Data (Error Code:{0})", (int)MDCErrors.App_Load_Profile_Read), 1);
#endif
                                            #endregion
                                            if (InnerException != null)
                                                throw InnerException;

                                            throw new Exception("Error while reading complete Load Profile Data");
                                        }
                                        #endregion
                                    }
                                    catch (Exception)
                                    {
                                        throw;
                                    }
                                    finally
                                    {
                                        try
                                        {
                                            #region Save & Update

                                            if (Save_LP && loadData != null && IsLPDataRead && IsSuccess)
                                            {
                                                //Save to Class

                                                toSaveloadData = LoadProfile_Controller.saveToClass(loadData, MeterInfo.MSN);

                                                //Save To Database
                                                //if (LP_Scheme == LoadProfileScheme.PQ_Load_Profile)
                                                //    custException = DB_Controller.savePQLoadProfileWithReplace(Read_LP, LP_Counters, toSaveloadData, SessionDateTime, MeterInfo);
                                                //else
                                                custException = DB_Controller.saveLoadProfileWithReplace(Read_LP, LP_Counters, toSaveloadData, SessionDateTime, MeterInfo, LP_Scheme);
                                                if (custException != null && custException.isTrue && custException.Ex == null)
                                                {
                                                    uint tempMaxLoadProfileCount = loadData.MaxCounter;
                                                    //LP_Counters.DB_Counter = tempMaxLoadProfileCount;
                                                    CountLastUpdated = (uint)custException.SomeNumber;
                                                    if (!loadData.CounterAvailable)
                                                        tempMaxLoadProfileCount = (uint)CountLastUpdated;
                                                    if (Read_LP == READ_METHOD.ByCounter && (CountLastUpdated > tempMaxLoadProfileCount || CountLastUpdated < LP_Counters.DB_Counter))
                                                    {
                                                        #region LogMessage("Error While Updating LP Counter:" + custException.SomeMessage);
#if Enable_Abstract_Log
                                                        LogMessage("Error While Updating LP Counter:" + custException.SomeMessage, "LPD", string.Format("F, {0}", custException.SomeMessage), 1);
#endif
#if !Enable_Abstract_Log
						LogMessage("Error While Updating LP Counter:" + custException.SomeMessage);
#endif
                                                        #endregion
                                                        #region Update Load Profile COunter Flag
                                                        LP_Counters.UpdateCounter = false;
                                                        //if (LP_Scheme == LoadProfileScheme.Scheme_1)
                                                        //    MIUF.UpdateLoadProfileCounter = false;
                                                        //else if (LP_Scheme == LoadProfileScheme.Scheme_2)
                                                        //    MIUF.UpdateLoadProfile2Counter = false;

                                                        #endregion
                                                    }
                                                    else if (!custException.SomeMessage.Equals("Nothing", StringComparison.OrdinalIgnoreCase))
                                                    {

                                                    }
                                                    else
                                                    {
                                                        #region Update to live table
                                                        if (LP_Scheme == LoadProfileScheme.Load_Profile && MeterInfo.EnableLiveUpdate && loadData.ChannelsInstances[loadData.ChannelsInstances.Count - 1].LoadProfileInstance.Count >= 4)
                                                        {
                                                            //Update LoadProfile_Live
                                                            var LPLiveData = new LoadProfile_Live();

                                                            LPLiveData.Channel_1 = loadData.ChannelsInstances[loadData.ChannelsInstances.Count - 1].LoadProfileInstance[0];
                                                            LPLiveData.Channel_2 = loadData.ChannelsInstances[loadData.ChannelsInstances.Count - 1].LoadProfileInstance[1];
                                                            LPLiveData.Channel_3 = loadData.ChannelsInstances[loadData.ChannelsInstances.Count - 1].LoadProfileInstance[2];
                                                            LPLiveData.Channel_4 = loadData.ChannelsInstances[loadData.ChannelsInstances.Count - 1].LoadProfileInstance[3];
                                                            var captureDate = loadData.ChannelsInstances[loadData.ChannelsInstances.Count - 1].DateTimeStamp;

                                                            try
                                                            {
                                                                // throw new Exception("Check Inserting LOG");
                                                                if (DB_Controller.UpdateLoadProfile_Live(MeterInfo.MSN, LPLiveData, captureDate))
                                                                    #region LogMessage("Load Profile Data Live Updated successfully", 2);
#if Enable_Abstract_Log
                                                                    LogMessage("Load Profile Data Live Updated successfully", "LPDL", "S", 1);
#endif

#if !Enable_Abstract_Log
						LogMessage("Load Profile Data Live Updated successfully", 2);
#endif
                                                                #endregion
                                                                else
                                                                {
                                                                    #region LogMessage("Load Profile Data Live Update is unsuccessful", 2);
#if Enable_Abstract_Log
                                                                    LogMessage("Load Profile Data Live Update is unsuccessful", "LPDL", "F", 1);
#endif
#if !Enable_Abstract_Log
						LogMessage("Load Profile Data Live Update is unsuccessful", 2);
#endif
                                                                    #endregion
                                                                    if (DB_Controller.InsertLoadProfile_Live(MeterInfo.MSN, LPLiveData, SessionDateTime, captureDate))
                                                                        #region LogMessage("Load Profile Data Live inserted successfully", 2);
#if Enable_Abstract_Log
                                                                        LogMessage("Load Profile Data Live inserted successfully", "LPDL", "N", 1);
#endif
#if !Enable_Abstract_Log
						LogMessage("Load Profile Data Live inserted successfully", 2);
#endif
                                                                    #endregion
                                                                }
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                LogMessage(String.Format("Unable to update LP live data\r\nDetails:{0}", ex.Message), 4);
                                                            }
                                                        }
                                                        #endregion
                                                        #region LogMessage(String.Format("Load Profile Data saved successfully and updated to count {0}, Internal Message: " + custException.SomeMessage, CountLastUpdated), 2);
#if Enable_Abstract_Log
                                                        if (Read_LP == READ_METHOD.ByCounter)
                                                        {
                                                            LogMessage(String.Format("Load Profile Data saved successfully and updated to count {0}, Internal Message: " + custException.SomeMessage, CountLastUpdated), "LPD",
                                                                string.Format("S, {0}", CountLastUpdated), 1);
                                                        }
                                                        else if (Read_LP == READ_METHOD.ByDateTime)
                                                        {
                                                            LogMessage(String.Format("Load Profile Data saved successfully and updated to Time {0}, Internal Message: " + custException.SomeMessage, LP_Counters.LastReadTime.ToString(Commons.DateTimeFormat)), "LPD",
    string.Format("S, {0}", LP_Counters.LastReadTime.ToString(Commons.DateTimeFormat)), 1);

                                                        }

#endif

#if !Enable_Abstract_Log
						LogMessage(String.Format("Load Profile Data saved successfully and updated to count {0}, Internal Message: " + custException.SomeMessage, CountLastUpdated), 2);
#endif
                                                        #endregion
                                                        if (LP_Counters.ReadInstant)
                                                        {
                                                            LP_Counters.ReadInstant = false;
                                                            LP_Counters.UpdateInstantFlag = true;
                                                            LP_Schedule.UpdateLastReadTime = false;
                                                        }
                                                        else lastTimeUpdate = true;
                                                    }
                                                    LP_Counters.CounterToUpdate = LP_Counters.DB_Counter = CountLastUpdated;
                                                }
                                                else
                                                {
                                                    if (custException.SomeMessage.Contains(string.Format("Error:{0}", (int)MDCErrors.DB_Duplicate_Entery)))
                                                    {
                                                        #region Lp Counter difference check w.r.t database count
                                                        var lastLoadProfileCounter = custException.SomeNumber;
                                                        DB_Controller.DBConnect.OpenConnection();
                                                        var warning = String.Format("Invalid Load Profile Counter Received DBCounter:{0}, MeterCounter:{1}, Server is disabling the Load Profile", LP_Counters.DB_Counter, loadProfileInfo.EntriesInUse);
                                                        #region LogMessage(warning);
#if Enable_Abstract_Log
                                                        LogMessage(warning, "LPD", string.Format("D ,{0}, {1}, {2}", loadProfileInfo.EntriesInUse, LP_Counters.DB_Counter, LP_Counters.MaxDifferenceCheck), 1);
#endif

#if !Enable_Abstract_Log
						LogMessage(warning);
#endif
                                                        #endregion
                                                        DB_Controller.InsertWarning(MeterInfo.MSN, SessionDateTime, ConnectToMeter.ConnectionTime, warning);
                                                        //const QuantityName x = QuantityName.Read_LP;
                                                        //DB_Controller.DisableQuantity(MeterInfo.MSN, Enum.GetName(typeof(QuantityName), x));
                                                        #region Disable LP
                                                        LP_Schedule.IsDisable = true;
                                                        //if (LP_Scheme == LoadProfileScheme.Scheme_1)
                                                        //    MIUF.IsDisableLP = true;
                                                        //else if (LP_Scheme == LoadProfileScheme.Scheme_2)
                                                        //    MIUF.IsDisableLP2 = true;
                                                        #endregion
                                                        #region Update Load Profile COunter Flag
                                                        LP_Counters.UpdateCounter = false;
                                                        //if (LP_Scheme == LoadProfileScheme.Scheme_1)
                                                        //    MIUF.UpdateLoadProfileCounter = false;
                                                        //else if (LP_Scheme == LoadProfileScheme.Scheme_2)
                                                        //    MIUF.UpdateLoadProfile2Counter = false;

                                                        #endregion
                                                        MDCAlarm.MDCCombineEvents[(ushort)MDCEvents.lp_counter_mismatch] = true;
                                                        MDCAlarm.IsMDCEventOuccer = true;
                                                        DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.lp_counter_mismatch), MeterInfo, SessionDateTime);

                                                        IsLPReadAble = false;

                                                        //if (LP_Schedule.IsSuperImmediate) MIUF.SuperImmediate_LP = true;
                                                        //LP_Schedule.IsSuperImmediate = false;
                                                        #endregion
                                                    }
                                                    else
                                                    {
                                                        string Error = "Saving Load Profile Data failed";
                                                        if (custException != null && custException.Ex != null)
                                                            Error += " Error: " + custException.Ex.Message;
                                                        #region Update Load Profile COunter Flag
                                                        LP_Counters.UpdateCounter = false;
                                                        //if (LP_Scheme == LoadProfileScheme.Scheme_1)
                                                        //    MIUF.UpdateLoadProfileCounter = false;
                                                        //else if (LP_Scheme == LoadProfileScheme.Scheme_2)
                                                        //    MIUF.UpdateLoadProfile2Counter = false;

                                                        #endregion
                                                        #region LogMessage(string.Format("{0}(Error Code:{1})", Error, (int)MDCErrors.App_Load_Profile_Read), 4);
#if Enable_Abstract_Log
                                                        LogMessage(string.Format("{0}(Error Code:{1})", Error, (int)MDCErrors.App_Load_Profile_Read), "LPD", "F", 1);
#endif
#if !Enable_Abstract_Log
						LogMessage(string.Format("{0}(Error Code:{1})", Error, (int)MDCErrors.App_Load_Profile_Read), 4);
#endif
                                                        #endregion
                                                    }
                                                }

                                            }


                                        #endregion

                                        ExitFinally:
                                            IsProcessed = true;
                                        }
                                        catch (Exception e)
                                        {
                                            LogMessage("Error:" + e.Message, 4);
                                        }
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.HelpLink != null && ex.HelpLink.Contains("LP76"))
                        {

                            #region   LogMessage(String.Format("Invalid Load Profile Group ID:{0}, No Group Found with this ID, Server is disabling the Load Profile (Error Code:{1})", MeterInfo.Counter_Obj.LoadProfile_GroupID, (int)MDCErrors.App_Load_Profile_Channels_GroupID), 4);
#if Enable_Abstract_Log
                            LogMessage(String.Format("Invalid Load Profile Group ID:{0}, No Group Found with this ID, Server is disabling the Load Profile (Error Code:{1})", LP_Counters.GroupId, (int)MDCErrors.App_Load_Profile_Channels_GroupID), "LP", string.Format("D, GRPID:{0}", LP_Counters.GroupId), 1);
#endif

#if !Enable_Abstract_Log
						  LogMessage(String.Format("Invalid Load Profile Group ID:{0}, No Group Found with this ID, Server is disabling the Load Profile (Error Code:{1})", MeterInfo.Counter_Obj.LoadProfile_GroupID, (int)MDCErrors.App_Load_Profile_Channels_GroupID), 4);
#endif
                            #endregion
                            DB_Controller.DBConnect.OpenConnection();
                            DB_Controller.InsertWarning(MeterInfo.MSN, SessionDateTime, ConnectToMeter.ConnectionTime, String.Format("Invalid Load Profile Group ID:{0}, No Group Found with this ID, Server is disabling the Load Profile", LP_Counters.GroupId));
                            //const QuantityName x = QuantityName.Read_LP;
                            //DB_Controller.DisableQuantity(MeterInfo.MSN, Enum.GetName(typeof(QuantityName), x));
                            #region Disable LP
                            LP_Schedule.IsDisable = true;
                            //if (LP_Scheme == LoadProfileScheme.Scheme_1)
                            //    MIUF.IsDisableLP = true;
                            //else if (LP_Scheme == LoadProfileScheme.Scheme_2)
                            //    MIUF.IsDisableLP2 = true;
                            #endregion
                            //if (LP_Schedule.IsSuperImmediate) MIUF.SuperImmediate_LP = true;
                            //LP_Schedule.IsSuperImmediate = false;
                        }
                        LogMessage(ex, 4, "Load Profile Data");
                        //if (!(ex is NullReferenceException))
                        //    throw;
                        //else
                        //LogMessage("Load Profile Error:" + ex.Message);
                        if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                        StatisticsObj.InsertError(ex, _session_DateTime, 15);
                    }
                    #region Finally Block

                    finally
                    {
                        if (IsLPDataRead || IsLPUpdated)
                        {
                            ResetMaxIOFailure();
                            MeterInfo.PreUpdateSchedule(LP_Schedule, SessionDateTime);
                            if (Read_LP == READ_METHOD.ByCounter)
                                LP_Counters.LastReadTime = LP_Schedule.LastReadTime;

                            #region Last LP Time Update
                            LP_Schedule.UpdateScheduleType = true;
                            LP_Schedule.UpdateLastReadTime = lastTimeUpdate | IsLPUpdated;

                            //if (LP_Scheme == LoadProfileScheme.Scheme_1)
                            //{

                            //    MIUF.Schedule_LP = true;
                            //    MIUF.last_LP_time = 
                            //}
                            //else if (LP_Scheme == LoadProfileScheme.Scheme_2)
                            //{
                            //    MIUF.Schedule_LP2 = true;
                            //    MIUF.last_LP2_time = lastTimeUpdate | IsLPUpdated;

                            //}
                            //if (Read_LP == READ_METHOD.ByDateTime)
                            //    LP_Schedule.LastReadTime = loadData.MaxDateTime;

                            #endregion
                            #region Update Super Immediate Flag
                            if (LP_Schedule.IsSuperImmediate)
                            {
                                LP_Schedule.UpdateIsSuperImmediate = true;
                                //if (LP_Scheme == LoadProfileScheme.Scheme_1)
                                //    MIUF.SuperImmediate_LP = true;
                                //else if (LP_Scheme == LoadProfileScheme.Scheme_2)
                                //    MIUF.SuperImmediate_LP2 = true;
                            }
                            #endregion
                            #region Update Base Time Flag

                            if (LP_Schedule.SchType == ScheduleType.IntervalFixed ||
                                LP_Schedule.SchType == ScheduleType.IntervalRandom)
                            {
                                LP_Schedule.UpdateBaseTime = true;

                                //if (LP_Scheme == LoadProfileScheme.Scheme_1)
                                //    MIUF.base_time_LP = true;
                                //else if (LP_Scheme == LoadProfileScheme.Scheme_2)
                                //    MIUF.base_time_LP2 = true;
                            }

                            #endregion

                            LP_Schedule.IsSuperImmediate = false;
                        }
                        else if (LP_Schedule.IsDisable && LP_Schedule.IsSuperImmediate)
                        {
                            LP_Schedule.UpdateIsSuperImmediate = true;
                            LP_Schedule.IsSuperImmediate = false;
                        }

                        // Reset Load Profile Generic Info For New Session
                        LoadProfile_Controller.LoadProfileInformation = null;
                    }

                #endregion

                Exit:
                    ;
                }

            }
            #endregion
            return IsProcessed;
        }

        public CustomException ProcessRequest_SinglePhase(CancellationTokenSource CancelTokenSource = null)
        {
            CustomException custExc = new CustomException();
            bool IsProcessed = false;
            bool IsMDIResetOccured = false;
            bool signal_strength_read_SP = false;
            int meterEvetnsCount = _EventController.EventLogInfoList.Count;
            try
            {
                if (MeterInfo != null && MeterInfo.MSN != null)
                {
                    #region // If Task Canceled
                    if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                    {
                        CancelTokenSource.Token.ThrowIfCancellationRequested();
                    }
                    #endregion
                    #region MDI Reset Check

                    if (ParamMajorAlarmProfileObj != null)
                    {
                        MajorAlarm MA = ParamMajorAlarmProfileObj.AlarmItems.Find(x => x.Info._EventId == 24); //MDI RESET
                        if (MA != null && MA.IsTriggered)
                        {
                            IsMDIResetOccured = true;
                        }
                    }

                    #endregion
                    #region Verify Single Phase Request(Disable EV and LP)
                    MeterInfo.Schedule_EV.SchType = ScheduleType.Disabled;
                    MeterInfo.Schedule_LP.SchType = ScheduleType.Disabled;
                    #endregion

                    foreach (var item in MeterInfo.ReadPlan)
                    {
                        switch (item)
                        {
                            case Schedules.ReadContactorStatus:
                                {
                                    #region ///If Task Cancelled
                                    if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                                    {
                                        CancelTokenSource.Token.ThrowIfCancellationRequested();
                                    }
                                    #endregion
                                    IsProcessed = ReadContactorStatus(IsProcessed);
                                }
                                break;
                            case Schedules.CumulativeBilling:
                                {
                                    #region // If Task Canceled

                                    if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                                    {
                                        CancelTokenSource.Token.ThrowIfCancellationRequested();
                                    }

                                    #endregion
                                    #region Cumulative Billing Data

                                    if (MeterInfo.Schedule_CB.IsSuperImmediate ||
                                        (MeterInfo.Schedule_CB.SchType != ScheduleType.Disabled && MeterInfo.Read_CB != READ_METHOD.Disabled))
                                    {
                                        if (MeterInfo.Schedule_CB.IsSuperImmediate || MeterInfo.IsScheduleReadyToBeProcess(MeterInfo.Schedule_CB))
                                        {
                                            ReadAndSaveCummBilling(true);
                                            //                                            BillingData billData = null;
                                            //                                            try
                                            //                                            {
                                            //                                                LogMessage("Reading Cumulative Billing Data", "CB", "R");
                                            //                                                if (MeterInfo.DDS110_Compatible) data = Billing_Controller.ReadCummulativeBillingDataByOBISList(); //Billing_Controller.ReadCummulativeBillingsDataDDS_SinglePhase();
                                            //                                                else billData = Billing_Controller.GetCumulativeBillingData(true);
                                            //                                                LogMessage("Reading Cumulative Billing Data Completed :-)", "CB", "S");

                                            //                                                if (billData != null)
                                            //                                                {
                                            //                                                    if (MeterInfo.Save_CB)
                                            //                                                    {
                                            //                                                        DB_Controller.DBConnect.OpenConnection();
                                            //                                                        if (!MeterInfo.DDS110_Compatible)
                                            //                                                            data = Billing_Controller.saveToClass(billData, MeterInfo.MSN);

                                            //                                                        bool save_Flag = DB_Controller.saveCumulativeBillingDataEx(data, SessionDateTime, MeterInfo);
                                            //                                                        if (save_Flag)
                                            //                                                        {
                                            //                                                            LogMessage("Saving Cumulative billing Data completed", "CBD", "S");
                                            //                                                            if (MeterInfo.isPrepaid && DB_Controller.saveCumulativeBillingForPrepaid_SinglePhase(data, SessionDateTime, MeterInfo))
                                            //                                                            {
                                            //                                                                LogMessage("Cumulative Billing Data saved for Prepaid", "CBPD", "S");
                                            //                                                            }
                                            //                                                        }
                                            //                                                        else
                                            //                                                        {
                                            //                                                            LogMessage(string.Format("Error Occurred while reading Cumulative billing Data (Error Code:{0})", (int)MDCErrors.App_Cum_Billing_Save), "CB", "F");
                                            //                                                        }

                                            //#if Enable_Abstract_Log
                                            //                                                        LogMessage("Saving Cumulative billing Data completed", "CBD", "S", 2);
                                            //#endif

                                            //                                                        if (MeterInfo.EnableLiveUpdate)
                                            //                                                        {

                                            //                                                            //Saves Cumm BIlling Data in CUmulative Data Live
                                            //                                                            LogMessage("Saving Cumulative billing Data Live(Cumm)", "CBLD", "R", 2);
                                            //                                                            DB_Controller.UpdateCumulativeEnergy_Live(data, _session_DateTime, MeterInfo);
                                            //                                                            LogMessage("Cumulative Billing Data Live(Cumm) saved", "CBLD", "S", 1);
                                            //                                                            //End
                                            //                                                        }


                                            //                                                    }

                                            //                                                    MeterInfo.PreUpdateSchedule(MeterInfo.Schedule_CB, SessionDateTime);
                                            //                                                    MIUF.Schedule_CB = true;
                                            //                                                    MIUF.last_CB_time = true;
                                            //                                                    if (MeterInfo.Schedule_CB.IsSuperImmediate)
                                            //                                                        MIUF.SuperImmediate_CB = true;
                                            //                                                    if (MeterInfo.Schedule_CB.SchType == ScheduleType.IntervalFixed || MeterInfo.Schedule_CB.SchType == ScheduleType.IntervalRandom)
                                            //                                                        MIUF.base_time_CB = true;
                                            //                                                    MeterInfo.Schedule_CB.IsSuperImmediate = false;
                                            //                                                    IsProcessed = true;
                                            //                                                }
                                            //                                            }
                                            //                                            catch (Exception ex)
                                            //                                            {
                                            //                                                LogMessage(ex, 4, "Cumulative Billing Data");
                                            //                                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                                            //                                                StatisticsObj.InsertError(ex, _session_DateTime, 15);
                                            //                                            }
                                            //                                            finally
                                            //                                            {
                                            //                                                if (IsProcessed)
                                            //                                                    ResetMaxIOFailure();
                                            //                                            }
                                        }
                                    }

                                    #endregion
                                }
                                break;
                            case Schedules.SignalStrength:
                                {
                                    #region /// If Task Cancelled
                                    if (CancelTokenSource != null)
                                    {
                                        CancelTokenSource.Token.ThrowIfCancellationRequested();
                                    }
                                    #endregion
                                    #region Signal Strength
#if !Enable_LoadTester_Mode
                                    if (MeterInfo.Schedule_SS.IsSuperImmediate || (MeterInfo.Schedule_SS.SchType != ScheduleType.Disabled && MeterInfo.Read_SS))
                                    {
                                        if (MeterInfo.Schedule_SS.IsSuperImmediate || MeterInfo.IsScheduleReadyToBeProcess(MeterInfo.Schedule_SS))
                                        {
                                            try
                                            {
                                                LogMessage("Reading Signal Strength", "SS", "R", 0);
                                                var insObj = new Instantaneous_Class();
                                                _signalStrength = Convert.ToInt32(InstantaneousController.GET_Any(insObj, Get_Index.RSSI_SignalStrength, 0));
                                                signal_strength_read_SP = true;
                                                LogMessage("Signal Strength read successfully", "SS", "S", 1);
                                                MeterInfo.PreUpdateSchedule(MeterInfo.Schedule_SS, SessionDateTime);
                                                MIUF.Schedule_SS = true;
                                                MIUF.last_SS_time = true;
                                                if (MeterInfo.Schedule_SS.IsSuperImmediate)
                                                    MIUF.SuperImmediate_SS = true;
                                                if (MeterInfo.Schedule_SS.SchType == ScheduleType.IntervalFixed || MeterInfo.Schedule_SS.SchType == ScheduleType.IntervalRandom)
                                                    MIUF.base_time_SS = true;
                                                MeterInfo.Schedule_SS.IsSuperImmediate = false;
                                                bool Res;
                                                try
                                                {
                                                    Res = DB_Controller.UpdateSignalStrength_Live(_signalStrength, MeterInfo.MSN, SessionDateTime);
                                                    // Res = DB_Controller.InsertSignalStrength_Live(_signalStrength, MeterInfo.MSN);
                                                }
                                                catch (Exception)
                                                {
                                                    try
                                                    {
                                                        // var rslt =DB_Controller.InsertSignalStrength_Live(_signalStrength, MeterInfo.MSN);
                                                        // LogMessage(
                                                        //         rslt ? "Signal Strength inserted successfully"
                                                        //         : "Signal Strength update and insertion is unsuccessful", 2);

                                                        var rslt = DB_Controller.InsertSignalStrength_Live(_signalStrength, MeterInfo.MSN, SessionDateTime);
                                                        LogMessage("", "SSLD", rslt ? "N" : "F", 2);
                                                    }
                                                    catch
                                                    { }
                                                    goto SS_Exit;
                                                }
                                                if (Res)
                                                    LogMessage("Signal Strength updated successfully", "SSLD", "S", 2);
                                                else
                                                {
                                                    var rslt = DB_Controller.InsertSignalStrength_Live(_signalStrength, MeterInfo.MSN, SessionDateTime);
                                                    LogMessage("", "SSLD", rslt ? "N" : "F", 2);
                                                }

                                            SS_Exit:;// Do Nothing
                                            }
                                            catch (Exception ex)
                                            {
                                                ex = new Exception(string.Format("{0} (Error Code:{1})", ex.Message, (int)MDCErrors.App_Signal_Strength_Read), ex.InnerException);
                                                LogMessage(ex, 4, "Signal Strength");
                                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                                                StatisticsObj.InsertError(ex, _session_DateTime, 15);
                                            }
                                            finally
                                            {

                                                if (signal_strength_read_SP)
                                                    ResetMaxIOFailure();
                                            }
                                        }
                                    }
#endif
                                    #endregion
                                }
                                break;
                            case Schedules.PowerQuantities:
                                {

                                    #region /// If Task Canceled

                                    if (CancelTokenSource != null)
                                    {
                                        CancelTokenSource.Token.ThrowIfCancellationRequested();
                                    }

                                    #endregion
                                    #region Instantaneous Data

                                    if (MeterInfo.Schedule_PQ.IsSuperImmediate || (MeterInfo.Schedule_PQ.SchType != ScheduleType.Disabled && MeterInfo.Read_PQ))
                                    {
                                        if (MeterInfo.Schedule_PQ.IsSuperImmediate || MeterInfo.IsScheduleReadyToBeProcess(MeterInfo.Schedule_PQ))
                                        {
                                            InstantaneousData insData = null;
                                            Instantaneous_Class data = null;
                                            try
                                            {
                                                LogMessage("Reading Instantaneous Data", "ID", "R");
                                                if (MeterInfo.DDS110_Compatible) data = InstantaneousController.ReadInstantaneousDataDDS_SinglePhase();
                                                insData = InstantaneousController.ReadInstantaneousData_SinglePhase();
                                                LogMessage("Reading Instantaneous Data completed", "ID", "S");
                                                if (insData != null)
                                                {
                                                    if (MeterInfo.Save_PQ)
                                                    {
                                                        DB_Controller.DBConnect.OpenConnection();
                                                        if (!MeterInfo.DDS110_Compatible)
                                                            data = InstantaneousController.saveToClass(insData, MeterInfo.MSN);

                                                        bool save_Flag = DB_Controller.SaveInstantaneous(data, SessionDateTime, _signalStrength, signal_strength_read_SP, MeterInfo);

                                                    }
                                                    #region Update Live
#if !Enable_LoadTester_Mode
                                                    if (MeterInfo.EnableLiveUpdate)
                                                    {
                                                        #region Live Update
                                                        //if (ConnectToMeter.CurrentConnection == PhysicalConnectionType.KeepAlive)//For Keep Alive
                                                        //{
                                                        try
                                                        {
                                                            if (DB_Controller.UpdateInstantaneous_Live_SinglePhase(MeterInfo.MeterID, data, SessionDateTime, ConnectToMeter.ConnectionTime, (int)MeterInfo.MeterType_OBJ))
                                                                MeterInfo.IsLiveUpdated = true;
                                                            else if (!MeterInfo.IsLiveUpdated)
                                                            {
                                                                LogMessage(String.Format("Unable to update PQ live data Server Inserted as a new Row"), "IDL", "N", 1);
                                                                if (DB_Controller.InsertInstantaneous_Live_SinglePhase(MeterInfo.MeterID, data, SessionDateTime, ConnectToMeter.ConnectionTime, (int)MeterInfo.MeterType_OBJ))
                                                                    MeterInfo.IsLiveUpdated = true;
                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            LogMessage(String.Format("Unable to update PQ live data\r\nDetails:{0}", ex.Message));
                                                        }
                                                        //}

                                                        #endregion
                                                        LogMessage("Saving Instantaneous Data completed", "IDD", "S");
                                                    }
                                                    else
                                                    {
                                                        LogMessage(string.Format("Error while saving Instantaneous Data (Error Code:{0})", (int)MDCErrors.App_Instantaneous_Data_Save), "ID", "F");
                                                    }
#endif
                                                    #endregion
                                                    MeterInfo.PreUpdateSchedule(MeterInfo.Schedule_PQ, SessionDateTime);
                                                    MIUF.Schedule_PQ = true;
                                                    MIUF.last_PQ_time = true;
                                                    if (MeterInfo.Schedule_PQ.IsSuperImmediate)
                                                        MIUF.SuperImmediate_PQ = true;
                                                    if (MeterInfo.Schedule_PQ.SchType == ScheduleType.IntervalFixed || MeterInfo.Schedule_PQ.SchType == ScheduleType.IntervalRandom)
                                                        MIUF.base_time_PQ = true;
                                                    MeterInfo.Schedule_PQ.IsSuperImmediate = false;
                                                    IsProcessed = true;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                ex = new Exception(string.Format("{0} (Error Code:{1})", ex.Message, (int)MDCErrors.App_Instantaneous_Data_Read), ex.InnerException);
                                                LogMessage(ex, 4, "Instantaneous Data");
                                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                                                StatisticsObj.InsertError(ex, _session_DateTime, 15);
                                            }
                                            finally
                                            {

                                                if (IsProcessed)
                                                    ResetMaxIOFailure();
                                            }
                                        }
                                    }

                                    #endregion
                                }
                                break;
                            case Schedules.Events:
                                {
                                    #region ///If Task Canceled
                                    if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                                    {
                                        CancelTokenSource.Token.ThrowIfCancellationRequested();
                                    }

                                    #endregion
                                    #region Events

#if !Enable_LoadTester_Mode

                                    #region Read Combine Event Logs

                                    if (MeterInfo.read_individual_events_sch || MeterInfo.Schedule_EV.IsSuperImmediate || (MeterInfo.ReadEventsForcibly && MeterInfo.Read_EV && MeterInfo.Save_EV))
                                    {
                                        if (MeterInfo.read_logbook && (MeterInfo.ReadEventsForcibly || MeterInfo.Schedule_EV.IsSuperImmediate ||
                                            MeterInfo.IsScheduleReadyToBeProcess(MeterInfo.Schedule_EV)))
                                        {
                                            var EventsData = new EventData();
                                            //bool IsEventsUpToDate = false;
                                            bool IsEventsReadSuccessfully = false;
                                            //long MeterCount = 0;
                                            long DBCount = MeterInfo.EV_Counters.DB_Counter;
                                            try
                                            {
                                                if (MeterInfo.EV_Counters.DB_Counter >= 0)
                                                {
                                                    DateTime latest_event_logbook = DateTime.MinValue;

                                                    #region Read Current Counter from Meter

                                                    //uint currentCounter;
                                                    try
                                                    {
                                                        MeterInfo.EV_Counters.Meter_Counter = Event_Controller.Get_EventCounter_Internal();
                                                        //MeterCount = currentCounter;
                                                        LogMessage(String.Format("Events Counter Received from Meter: {0} and DBCount: {1}", MeterInfo.EV_Counters.Meter_Counter,
                                                            MeterInfo.EV_Counters.DB_Counter), 3);
                                                    }
                                                    catch
                                                    {
                                                        throw;
                                                    }

                                                    #endregion
                                                    #region Compare Counter and Disable Events

                                                    bool isEVCountValid = MeterInfo.EV_Counters.IsCounterValid;
                                                    if (!isEVCountValid)
                                                    {
                                                        //Retry
                                                        MeterInfo.EV_Counters.Meter_Counter = Event_Controller.Get_EventCounter_Internal();
                                                        //MeterCount = currentCounter;
                                                        //LogMessage(String.Format("Events Counter Received on Retry\tMeterCount: {0}", currentCounter), 3);

                                                        bool isEVCountValidRetry = MeterInfo.EV_Counters.IsCounterValid;
                                                        //!(MeterInfo.Counter_Obj.Events_Count > currentCounter
                                                        //                             || (currentCounter - (uint)MeterInfo.Counter_Obj.Events_Count) > MeterInfo.Counter_Obj.MaxEventsDiffCheck);
                                                        if (!isEVCountValidRetry)
                                                        {
                                                            //Disable EV and Insert Warning
                                                            try
                                                            {
                                                                DB_Controller.DBConnect.OpenConnection();
                                                                DB_Controller.InsertWarning(MeterInfo.MSN, SessionDateTime, ConnectToMeter.ConnectionTime, String.Format("Invalid Events Counter Received DBCounter:{0}, MeterCounter:{1}, Server is disabling the Events", MeterInfo.EV_Counters.DB_Counter, MeterInfo.EV_Counters.Meter_Counter));
                                                                //const QuantityName x = QuantityName.Read_logbook;
                                                                //DB_Controller.DisableQuantity(MeterInfo.MSN, Enum.GetName(typeof(QuantityName), x));
                                                                LogMessage(String.Format("Invalid Events Counter Received DBCounter:{0}, MeterCounter:{1}, Server is disabling the Events", MeterInfo.EV_Counters.DB_Counter, MeterInfo.EV_Counters.Meter_Counter),
                                                                    "ED", string.Format("D, {0},{1},{2}", MeterInfo.EV_Counters.Meter_Counter, MeterInfo.EV_Counters.DB_Counter, MeterInfo.EV_Counters.MinDifferenceCheck), 1);
                                                                MIUF.IsDisableEv = true;
                                                            }
                                                            catch { }
                                                            finally
                                                            {
                                                                DB_Controller.DBConnect.CloseConnection();
                                                                MeterInfo.Read_EV = false;
                                                            }
                                                            goto Exit;
                                                        }
                                                    }

                                                    #endregion
                                                    #region Making Entry

                                                    var eventCounter = new Profile_Counter();
                                                    //eventCounter.Previous_Counter = (uint)MeterInfo.Counter_Obj.Events_Count;
                                                    //eventCounter.Current_Counter = currentCounter;
                                                    //eventCounter.Max_Size = Limits.Max_Events_Count_Limit;

                                                    //long old_counter = MeterInfo.Counter_Obj.Events_Count;
                                                    var e_info = new EventInfo();
                                                    e_info.EventCode = 0;
                                                    e_info = Event_Controller.EventLogInfoList.Find((x) => x.EventCode == 0);

                                                    //long difference = (long)currentCounter - old_counter;

                                                    if (MeterInfo.EV_Counters.IsUptoDate)
                                                    {
                                                        LogMessage("Events Data is up-to-date", 3);
                                                        //IsEventsUpToDate = true;
                                                    }

                                                    #endregion
                                                    #region Read

                                                    if (MeterInfo.EV_Counters.IsReadable)
                                                    {
                                                        Exception innerException = null;
                                                        try
                                                        {
                                                            LogMessage(String.Format("Reading Events Data from {0} to {1} {2}", eventCounter.DB_Counter, eventCounter.Meter_Counter, (MeterInfo.ReadEventsForcibly) ? "due to some Major Alarm occurred" : ""), 0);

                                                            IsEventsReadSuccessfully = Event_Controller.TryReadEventLogDataInChunks(eventCounter, e_info, EventsData,
                                                                            (ex) => innerException = ex, 25, CancelTokenSource);

                                                            if (IsEventsReadSuccessfully)
                                                            {
                                                                LogMessage("Events Data read complete", 1);

                                                                //Update Event For Live-Update

                                                                //EventIDtoCode idTOcode = new EventIDtoCode();
                                                                DateTime latest = DateTime.MinValue;
                                                                int id = 0;
                                                                bool retry = true;
                                                                EventItem data = null;

                                                                EventData copy_data = EventsData.Clone();

                                                                while (retry && copy_data.EventRecords.Count > 0)
                                                                {
                                                                    latest = copy_data.EventRecords.Max(x => x.EventDateTimeStamp);
                                                                    data = copy_data.EventRecords.Find(x => x.EventDateTimeStamp == latest);

                                                                    //id = idTOcode.getEventID(data.EventInfo.EventCode);
                                                                    if (data != null)
                                                                        id = data.EventInfo._EventId;

                                                                    MeterInfo.eventsForLiveUpdate_logbook = Commons.HexStringToBinary(MeterInfo.eventsForLiveUpdate_logbook_string, meterEvetnsCount);
                                                                    if (MeterInfo.eventsForLiveUpdate_logbook[id - 1] == '1')
                                                                    {
                                                                        retry = false;
                                                                    }
                                                                    else
                                                                    {
                                                                        copy_data.EventRecords.Remove(data);
                                                                    }
                                                                }
                                                                if (!retry)
                                                                {
                                                                    if (DB_Controller.UpdateLastEvent_Live_logbook(MeterInfo.MSN, data.EventInfo.EventCode, data.EventDateTimeStamp))
                                                                    {
                                                                        LogMessage("Logbook->Event Code " + data.EventInfo.EventCode + " updated to Instantaneous Live ");
                                                                    }
                                                                    else
                                                                    {
                                                                        LogMessage("Error occurred while updating Event Code " + data.EventInfo.EventCode + " updated to Instantaneous Live ");
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    LogMessage("No events to read from the logbook");
                                                                }
                                                            }
                                                            if (!IsEventsReadSuccessfully && innerException != null)
                                                                throw innerException;
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            LogMessage("Error while reading Events Data: " + ex, 4);
                                                            throw;
                                                        }
                                                        if (innerException != null)
                                                        {
                                                            throw innerException;
                                                        }
                                                    }

                                                    #endregion
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                LogMessage(ex, 4, "Read Combine Event Logs");
                                                // if (!(ex is NullReferenceException))
                                                //    throw;
                                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                                                StatisticsObj.InsertError(ex, _session_DateTime, 15);
                                            }
                                            finally
                                            {
                                                // MeterInfo.ReadEventsForcibly = false;
                                                #region Saving To Database

                                                try
                                                {
                                                    if (IsEventsReadSuccessfully)
                                                        ResetMaxIOFailure();

                                                    if (!MeterInfo.EV_Counters.IsUptoDate && IsEventsReadSuccessfully)
                                                    {
                                                        if (MeterInfo.Save_EV && EventsData != null && EventsData.MaxEventCounter > 0)
                                                        {
                                                            uint tempMAXEventCount = EventsData.MaxEventCounter;
                                                            CustomException CEX = DB_Controller.saveEventsDataWithReplace(EventsData, MeterInfo.MSN, SessionDateTime, MeterInfo, MIUF);

                                                            if (CEX != null && CEX.isTrue && CEX.Ex == null)
                                                            {
                                                                MeterInfo.EV_Counters.DB_Counter = tempMAXEventCount;

                                                                if (CEX.SomeNumber > MeterInfo.EV_Counters.Meter_Counter || CEX.SomeNumber < DBCount || !CEX.SomeMessage.Equals("Nothing", StringComparison.OrdinalIgnoreCase))
                                                                {
                                                                    LogMessage(String.Format("Saving Events Data failed, {0}", CEX.SomeMessage));
                                                                }
                                                                else
                                                                    LogMessage(String.Format("Events Data saved successfully and updated to count {0}, Internal Message: " + CEX.SomeMessage, CEX.SomeNumber), 2);
                                                            }
                                                            else
                                                            {
                                                                var Error = "Saving Events Data failed";
                                                                if (CEX != null && CEX.Ex != null)
                                                                    Error += " Error: " + CEX.Ex.Message;
                                                                LogMessage(Error, 4);
                                                            }
                                                        }
                                                    }
                                                    if ((MeterInfo.EV_Counters.IsUptoDate || IsEventsReadSuccessfully) && MeterInfo.read_logbook)//!MeterInfo.read_individual_events_sch) changed by furqan
                                                    {
                                                        MeterInfo.PreUpdateSchedule(MeterInfo.Schedule_EV, SessionDateTime);
                                                        MIUF.Schedule_EV = true;
                                                        MIUF.last_EV_time = true;
                                                        if (MeterInfo.Schedule_EV.IsSuperImmediate)
                                                            MIUF.SuperImmediate_EV = true;
                                                        if (MeterInfo.Schedule_EV.SchType == ScheduleType.IntervalFixed ||
                                                            MeterInfo.Schedule_EV.SchType == ScheduleType.IntervalRandom)
                                                            MIUF.base_time_EV = true;

                                                        MeterInfo.Schedule_EV.IsSuperImmediate = false;
                                                    }

                                                    IsProcessed = true;
                                                }
                                                catch (Exception ex)
                                                {
                                                    LogMessage(ex.Message, 4);
                                                }

                                                #endregion
                                            }
                                        Exit:
                                            if (!MeterInfo.Read_EV)
                                                LogMessage("Events Disabled", 3);
                                        }
                                    }

                                    #endregion

                                    #region READ SEPARATE EVENTS

                                    List<EventData> _obj_EventData = null;
                                    try
                                    {
                                        List<EventInfo> eventsToRead = new List<EventInfo>();
                                        List<EventInfo> eventsIndividualRead = new List<EventInfo>();
                                        EventLogInfo _obj = null;
                                        MeterInfo.individual_events_string_sch = (string.IsNullOrEmpty(MeterInfo.individual_events_string_sch)) ? "0" : MeterInfo.individual_events_string_sch;
                                        MeterInfo.individual_events_string_alarm = (string.IsNullOrEmpty(MeterInfo.individual_events_string_alarm)) ? "0" : MeterInfo.individual_events_string_alarm;
                                        // EventIDtoCode eventIDtoCode_obj = new EventIDtoCode();
                                        if ((MeterInfo.read_individual_events_sch && MeterInfo.ReadEventsForcibly))
                                        {
                                            char[] binStringEventsOnAlarm = Commons.HexStringToBinary(MeterInfo.individual_events_string_alarm, meterEvetnsCount);
                                            char[] binStringEventsSch = Commons.HexStringToBinary(MeterInfo.individual_events_string_sch, meterEvetnsCount);
                                            MeterInfo.individual_events_array = new char[meterEvetnsCount];
                                            for (int i = 0; i < meterEvetnsCount; i++)
                                            {
                                                MeterInfo.individual_events_array[i] = (Convert.ToByte(binStringEventsOnAlarm[i]) | Convert.ToByte(binStringEventsSch[i])).ToString().ToCharArray()[0];
                                            }

                                        }
                                        if (MeterInfo.read_individual_events_sch)
                                        {
                                            // WHen reading events on schedule, we do not need to check if that alarm was triggered. This check
                                            // is included when we read events in case of major alarms

                                            if ((MeterInfo.individual_events_string_sch != string.Empty &&
                                                MeterInfo.IsScheduleReadyToBeProcess(MeterInfo.Schedule_EV)) ||
                                                MeterInfo.Schedule_EV.IsSuperImmediate)
                                            {
                                                MeterInfo.individual_events_array = Commons.HexStringToBinary(MeterInfo.individual_events_string_sch, meterEvetnsCount);
                                                for (int a = 1; a <= MeterInfo.individual_events_array.Length; a++)
                                                {
                                                    if (MeterInfo.individual_events_array[a - 1].Equals('1'))
                                                    {
                                                        // TODO:Modification
                                                        // int CurrentEventCode = eventIDtoCode_obj.getEventCode((int)a);
                                                        // if (CurrentEventCode != -1)
                                                        _obj = _EventController.EventLogInfoList.Find(x => x._EventId == a); //|| x.EventCode == CurrentEventCode);
                                                        // No Event Data in case of TBE1 and TBE2
                                                        if (_obj != null && ((_obj._EventId != 38 && _obj._EventId != 39) ||
                                                                            (_obj.EventCode != 211 && _obj.EventCode != 212)))
                                                            eventsToRead.Add(_obj);
                                                    }
                                                }
                                                // else goto exitEvents;
                                            }
                                        }

                                        if ((MeterInfo.ReadEventsForcibly && MeterInfo.individual_events_string_alarm != string.Empty) || MeterInfo.Schedule_EV.IsSuperImmediate)
                                        {
                                            MeterInfo.individual_events_array = Commons.HexStringToBinary(MeterInfo.individual_events_string_alarm, meterEvetnsCount);
                                            for (int a = 1; a <= MeterInfo.individual_events_array.Length; a++)
                                            {
                                                if (MeterInfo.individual_events_array[a - 1].Equals('1') && ParamMajorAlarmProfileObj != null && ParamMajorAlarmProfileObj.AlarmItems[a - 1].IsTriggered)
                                                {
                                                    // TODO:Modification
                                                    // int CurrentEventCode = eventIDtoCode_obj.getEventCode((int)a);
                                                    // if (CurrentEventCode != -1)
                                                    _obj = _EventController.EventLogInfoList.Find(x => x._EventId == a);
                                                    // || x.EventCode == CurrentEventCode);
                                                    // No Event Data in case of TBE1 and TBE2
                                                    if (_obj != null && ((_obj._EventId != 38 && _obj._EventId != 39) ||
                                                                        (_obj.EventCode != 211 && _obj.EventCode != 212)))
                                                        eventsToRead.Add(_obj);
                                                }
                                            }
                                        }

                                        if ((MeterInfo.ReadEventsForcibly || MeterInfo.read_individual_events_sch) && eventsToRead.Count > 0)
                                            eventsToRead = eventsToRead.Distinct().ToList();

                                        #region //Clone EventInfo & EventLogInfo Obj
                                        IList<EventInfo> event_Info_T = eventsToRead.ToList();
                                        eventsToRead.Clear();
                                        foreach (var ev_Info in event_Info_T)
                                        {
                                            EventInfo t_Obj = null;
                                            if (ev_Info is EventLogInfo)
                                                t_Obj = (EventInfo)((EventLogInfo)ev_Info).Clone();
                                            else if (ev_Info is EventInfo)
                                                t_Obj = (EventInfo)((EventInfo)ev_Info).Clone();
                                            eventsToRead.Add(t_Obj);
                                        }
                                        #endregion

                                        Exception Internal_Exception = null;
                                        bool isSuccessful = false;
                                        _obj_EventData = new List<EventData>();

                                        if (eventsToRead.Count > 0)
                                        {
                                            #region Debugging & Logging
#if Enable_DEBUG_ECHO
                                            LogMessage("Reading Individual Events Data", 0);
#endif
                                            #endregion

                                            //_obj_EventData = _EventController.ReadEventLogData(eventsToRead);
                                            isSuccessful = _EventController.TryReadEventLogData(eventsToRead, ref _obj_EventData, (ex) => Internal_Exception = ex, CancelTokenSource);
                                            // Update Individual Events Read Successfully
                                            foreach (var ev_Data in _obj_EventData)
                                            {
                                                if (ev_Data != null && ev_Data.EventInfo != null)
                                                    eventsIndividualRead.Add(ev_Data.EventInfo);
                                            }
                                            MeterInfo.eventsForLiveUpdate_individual = Commons.HexStringToBinary(MeterInfo.eventsForLiveUpdate_individual_string, meterEvetnsCount);
                                            DateTime latest_Event = DateTime.MinValue;
                                            EventData data = null;
                                            EventItem data_Item = null;
                                            DateTime currentMax = DateTime.MinValue;
                                            int latestEventCode = 0;

                                            for (int i = 0; i < MeterInfo.eventsForLiveUpdate_individual.Length; i++)
                                            {
                                                if (MeterInfo.eventsForLiveUpdate_individual[i] == '1')
                                                {
                                                    //int code = eventIDtoCode_obj.getEventCode((int)i + 1);
                                                    data = _obj_EventData.Find(x => x.EventInfo != null &&
                                                                              (x.EventInfo._EventId == i + 1));
                                                    // || x.EventInfo.EventCode == code));

                                                    if (data != null && data.EventRecords.Count > 0)
                                                    {
                                                        currentMax = data.EventRecords.Max(x => x.EventDateTimeStamp);
                                                        data_Item = data.EventRecords.Find(x => x.EventDateTimeStamp == currentMax);

                                                        if (currentMax > latest_Event)
                                                        {
                                                            latest_Event = currentMax;
                                                            if (data_Item != null && data_Item.EventInfo != null)
                                                                latestEventCode = data_Item.EventInfo.EventCode;
                                                        }
                                                    }
                                                }
                                            }
                                            if (latest_Event != DateTime.MinValue)
                                            {
                                                if (DB_Controller.UpdateLastEvent_Live_individual(MeterInfo.MSN, latestEventCode, latest_Event))
                                                {
                                                    LogMessage("Individual Events->Event Code " + latestEventCode + " updated to Instantaneous Live ");
                                                }
                                                else
                                                {
                                                    LogMessage("Error occurred while updating Event Code " + latestEventCode + " updated to Instantaneous Live ");
                                                }
                                            }
                                            #region Debugging & Logging

#if Enable_DEBUG_ECHO
                                            LogMessage("Reading Individual Events Data Complete", 0);
#endif
#if Enable_Transactional_Logging
                                            Statistics_Obj.InsertLog(String.Format("Reading Events Data Complete for MSN {0}", MSN));
#endif

                                            #endregion
                                        }
                                        else
                                        {
                                            #region Debugging & Logging
#if Enable_Transactional_Logging
                           //Program.Out.WriteLine(String.Format("No New Event triggered for MSN {0}", MSN));
                           Statistics_Obj.InsertLog(String.Format("No New Event triggered for MSN {0}", MSN));
#endif
                                            #endregion
                                        }
                                        // exitEvents:
                                        if (_obj_EventData != null)
                                        {
                                            CustomException CEX = DB_Controller.saveEventsData_IndividualithReplace(_obj_EventData, MeterInfo.MSN, SessionDateTime, MeterInfo);
                                            if (CEX != null && CEX.Ex != null)
                                            {
                                                LogMessage("Error Saving Individual Events data", 0);
                                                throw CEX.Ex;
                                            }
                                            else if (!isSuccessful && Internal_Exception != null)
                                            {
                                                LogMessage("Error Occurred reading Individual Events data", 0);

                                                //Reset Alarm Register
                                                if (MeterInfo.Read_AR)
                                                {
                                                    ResetAlarmRegister(eventsIndividualRead);
                                                }
                                                IsProcessed = false;
                                                throw Internal_Exception;
                                            }
                                            else
                                            {
                                                //Reset Alarm Register Alarm Resetting change:5656
                                                //if (MeterInfo.Read_AR)
                                                //{
                                                //    ResetAlarmRegister();
                                                //}
                                                MeterInfo.ReadEventsForcibly = false;
                                                MeterInfo.PreUpdateSchedule(MeterInfo.Schedule_EV, SessionDateTime);
                                                MIUF.Schedule_EV = true;
                                                MIUF.last_EV_time = true;
                                                if (MeterInfo.Schedule_EV.IsSuperImmediate)
                                                    MIUF.SuperImmediate_EV = true;
                                                if (MeterInfo.Schedule_EV.SchType == ScheduleType.IntervalFixed || MeterInfo.Schedule_EV.SchType == ScheduleType.IntervalRandom)
                                                    MIUF.base_time_EV = true;

                                                MeterInfo.Schedule_EV.IsSuperImmediate = false;
                                                IsProcessed = true;
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        LogMessage(ex, 4, "Events");
                                        if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                                        StatisticsObj.InsertError(ex, _session_DateTime, 15);
                                    }
                                    finally
                                    {
                                        if (IsProcessed)
                                            ResetMaxIOFailure();
                                    }

                                    #endregion
#endif
                                    #endregion
                                }
                                break;
                            case Schedules.MonthlyBilling:
                                {
                                    #region ///If Task Cancelled
                                    if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                                    {
                                        CancelTokenSource.Token.ThrowIfCancellationRequested();
                                    }
                                    #endregion
                                    #region Monthly Billing
#if !Enable_LoadTester_Mode
                                    if (MeterInfo.Schedule_MB.IsSuperImmediate || ((IsMDIResetOccured || MeterInfo.Schedule_MB.SchType != ScheduleType.Disabled) && MeterInfo.MB_Counters.DB_Counter >= 0 && MeterInfo.Read_MB != READ_METHOD.Disabled))
                                    {
                                        if (MeterInfo.Schedule_MB.IsSuperImmediate || MeterInfo.IsScheduleReadyToBeProcess(MeterInfo.Schedule_MB))
                                        {
                                            List<BillingData> MonthlyBillingData = null;
                                            //uint meterCounter = 0;
                                            bool IsMBUpToDate = false;
                                            try
                                            {
                                                if (MeterInfo.MB_Counters.DB_Counter >= 0)
                                                {
                                                    MonthlyBillingData = new List<BillingData>();
                                                    MeterInfo.MB_Counters.Meter_Counter = Billing_Controller.Get_BillingCounter_Internal();

                                                    if (MeterInfo.MB_Counters.Meter_Counter > 0) //Billing available
                                                    {
                                                        #region Billing available
                                                        if (MeterInfo.MB_Counters.Difference == 0)
                                                        {
                                                            LogMessage("Monthly Billing Data is up-to-date", "MB", string.Format("U, {0}", MeterInfo.MB_Counters.DB_Counter));
                                                            IsMBUpToDate = true;
                                                        }
                                                        else if (MeterInfo.MB_Counters.Difference > 0)
                                                        {
                                                            #region CalculateMonthlyBillingCounter

                                                            // uint differenceInCounter = Convert.ToUInt32(meterCounter - MeterInfo.MB_Counters.DB_Counter);
                                                            int counterToread;
                                                            if (MeterInfo.MB_Counters.Difference > 23) counterToread = 23;
                                                            counterToread = MeterInfo.MB_Counters.Difference + 100;
                                                            Billing_Controller.MonthlyBillingFilter = Convert.ToByte(counterToread);

                                                            #endregion
                                                            LogMessage("Reading Monthly Billing Data", "MB", "R");
                                                            MonthlyBillingData = Billing_Controller.GetBillingData();
                                                            LogMessage("Reading Monthly Billing Data Complete", "MB", "S");
                                                        }
                                                        #endregion
                                                    }
                                                    else
                                                    {
                                                        LogMessage("Billing not available (MDI Reset Count = 0)", "MB", "EMPT");
                                                    }
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                LogMessage(ex, 4, "Monthly Billing");
                                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                                                StatisticsObj.InsertError(ex, _session_DateTime, 15);
                                            }
                                            finally
                                            {
                                                try
                                                {
                                                    if (MeterInfo != null && MeterInfo.MB_Counters != null &&
                                                        MeterInfo.MB_Counters.Meter_Counter > 0)
                                                        ResetMaxIOFailure();

                                                    if (MeterInfo.Save_MB && MonthlyBillingData != null && !IsMBUpToDate && MonthlyBillingData.Count > 0)
                                                    {
                                                        //save to class
                                                        //Monthly_Billing_data_SinglePhase monthlyData = Billing_Controller.saveToClass_SinglePhase(MonthlyBillingData, MeterInfo.MSN);
                                                        //bool save_Flag = DB_Controller.saveMonthlyBillingData_SinglePhase(monthlyData, SessionDateTime, MeterInfo);
                                                        Monthly_Billing_data monthlyData = null;
                                                        if (MeterInfo.BillingMethodId == (byte)BillingMethods.OneGetMethod)
                                                        {
                                                            monthlyData = Billing_Controller.SaveToClass(MonthlyBillingData, MeterInfo.MSN);
                                                        }
                                                        else
                                                        {
                                                            LogMessage(String.Format("Saving Monthly billing Data completed and updated to count {0}", MeterInfo.MB_Counters.Meter_Counter), "MBID", string.Format("INVALID, {0}", MeterInfo.BillingMethodId.ToString()), 1);
                                                        }
                                                        //bool save_Flag = DB_Controller.saveMonthlyBillingData_SinglePhase(monthlyData, SessionDateTime, MeterInfo);
                                                        var CEx = DB_Controller.SaveMonthlyBillingDataWithReplaceEx(monthlyData,
                                                            MeterInfo.MB_Counters.Meter_Counter, SessionDateTime, MeterInfo, MIUF);
                                                        if (CEx != null && CEx.isTrue && CEx.Ex == null && monthlyData.monthly_billing_data.Count > 0)
                                                        {
                                                            //bool flag = DB_Controller.update_MonthlyBilling_Counter(MeterInfo.MSN, meterCounter);
                                                            MeterInfo.MonthlyBillingCounterToUpdate = (int)MeterInfo.MB_Counters.Meter_Counter;
                                                            MIUF.UpdateMBCounter = true;
                                                            #region LogMessage(String.Format("Saving Monthly billing Data completed and updated to count {0}", meterCounter), 2);
#if Enable_Abstract_Log
                                                            LogMessage(String.Format("Saving Monthly billing Data completed and updated to count {0}", MeterInfo.MB_Counters.Meter_Counter), "MBD", string.Format("S, {0}", MeterInfo.MB_Counters.Meter_Counter.ToString()), 1);
#endif
#if !Enable_Abstract_Log
						LogMessage(String.Format("Saving Monthly billing Data completed and updated to count {0}", meterCounter), 2);
#endif
                                                            #endregion
                                                            //lastTimeUpdate = true;
                                                        }
                                                        else
                                                        {
                                                            if (CEx.SomeMessage.Contains(string.Format("Error:{0}", (int)MDCErrors.DB_Duplicate_Entery)))
                                                            {
                                                                #region monthly billing Counter difference check w.r.t database count
                                                                DB_Controller.DBConnect.OpenConnection();
                                                                var warning = String.Format("Invalid Monthly billing Counter Received DBCounter:{0}, MeterCounter:{1}, Server is disabling the Monthly billing", MeterInfo.MB_Counters.DB_Counter, MeterInfo.MB_Counters.Meter_Counter);
                                                                #region LogMessage(warning);
#if Enable_Abstract_Log
                                                                LogMessage(warning, "MB", string.Format("D, {0},{1}", MeterInfo.MB_Counters.Meter_Counter, MeterInfo.MB_Counters.DB_Counter), 1);
#endif
#if !Enable_Abstract_Log
						LogMessage(warning);
#endif
                                                                #endregion
                                                                DB_Controller.InsertWarning(MeterInfo.MSN, SessionDateTime, ConnectToMeter.ConnectionTime, warning);

                                                                MIUF.IsDisableMB = true;

                                                                MDCAlarm.MDCCombineEvents[(ushort)MDCEvents.mb_counter_mismatch] = true;
                                                                MDCAlarm.IsMDCEventOuccer = true;
                                                                DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.mb_counter_mismatch), MeterInfo, SessionDateTime);
                                                                #endregion
                                                            }
                                                            else
                                                            {
                                                                string Error = "Saving Monthly Billing failed";
                                                                if (CEx != null && CEx.Ex != null)
                                                                    Error += " Error: " + CEx.Ex.Message;
                                                                #region  LogMessage(string.Format("{0} (Error Code:{1})", Error, (int)MDCErrors.App_Monthly_Billing_Save), 4);
#if Enable_Abstract_Log
                                                                LogMessage(string.Format("{0} (Error Code:{1})", Error, (int)MDCErrors.App_Monthly_Billing_Save), "MBD", "F", 1);
#endif

#if !Enable_Abstract_Log
						 LogMessage(string.Format("{0} (Error Code:{1})", Error, (int)MDCErrors.App_Monthly_Billing_Save), 4);
#endif
                                                                #endregion
                                                            }
                                                        }
                                                    }
                                                    if (IsMBUpToDate || MonthlyBillingData == null)
                                                    {
                                                        MeterInfo.PreUpdateSchedule(MeterInfo.Schedule_MB, SessionDateTime);

                                                        MIUF.Schedule_MB = true;
                                                        MIUF.last_MB_time = true;
                                                        if (MeterInfo.Schedule_MB.SchType == ScheduleType.IntervalFixed || MeterInfo.Schedule_MB.SchType == ScheduleType.IntervalRandom)
                                                            MIUF.base_time_MB = true;
                                                        if (MeterInfo.Schedule_MB.IsSuperImmediate)
                                                            MIUF.SuperImmediate_MB = true;

                                                        MeterInfo.Schedule_MB.IsSuperImmediate = false;
                                                    }
                                                    IsProcessed = true;
                                                }
                                                catch (Exception ex)
                                                {
                                                    LogMessage(ex, DefaultExceptionLevel, "Monthly Billing Data");
                                                }
                                            }
                                        }
                                    }
#endif
                                    #endregion
                                }
                                break;
                            case Schedules.LoadProfile:
                                break;
                            case Schedules.PerameterizationWrite:
                                {
                                    #region ///If Task Cancelled
                                    if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                                    {
                                        CancelTokenSource.Token.ThrowIfCancellationRequested();
                                    }
                                    #endregion
                                    #region Parameterization
                                    if (!MeterInfo.IsParamEmpty || MeterInfo.isPrepaid)
                                    {
                                        if (TryParameterize(CancelTokenSource))
                                        {
                                            LogMessage("Parameterization is Successful", "PMR", "S");
                                            IsProcessed = true;
                                        }
                                        else
                                            if (!MeterInfo.IsParamEmpty || MeterInfo.prepaid_request_exist)
                                            LogMessage("Parameterization is Unsuccessful", "PMR", "F");
                                    }
                                    #endregion
                                }
                                break;
                            case Schedules.ParamterizationRead:
                                {
                                    #region ///If Task Canceled
                                    if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                                    {
                                        CancelTokenSource.Token.ThrowIfCancellationRequested();
                                    }
                                    #endregion
                                    #region Read Parameterization
#if !Enable_LoadTester_Mode

                                    if (MeterInfo.ReadParams)// || MeterInfo.isPrepaid)
                                    {
                                        try
                                        {
                                            if (TryReadParameters(CancelTokenSource))
                                            {
                                                LogMessage("Parameterization is Successful", "RPMR", "S");
                                                IsProcessed = true;
                                            }
                                            else if (!MeterInfo.IsParamEmpty) // || MeterInfo.prepaid_request_exist)
                                                LogMessage("Parameterization is Unsuccessful", "RPMR", "F");
                                        }
                                        catch (Exception x)
                                        {
                                            throw x;
                                        }
                                    }
#endif
                                    #endregion
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage(ex, 4);
                IsProcessed = false;
                custExc.Ex = ex;
            }
            finally
            {
                if (MeterInfo.MeterType_OBJ == MeterType.KeepAlive)
                {
                    // Assign next call time and update the meter settings
                    MeterInfo.Kas_DueTime = MeterInfo.Kas_NextCallTime;
                    MIUF.KAS_DueTime = true;

                    // //TODO:Modification Force To Logout Meter
                    // DateTime Next_KAS_Due_Time = MeterInfo.GetNextCallTimeForFixedInterval(MeterInfo.Kas_DueTime,
                    //                 MeterInfo.Kas_Interval.TotalMinutes);
                    // //Next Due Time is later than DefaultKeepAliveSleepTime
                    // if ((DateTime.Now + DefaultKeepAliveSleepTime) < Next_KAS_Due_Time)
                    // {
                    //     MeterInfo.logoutMeter = true;
                    // }
                }
                if (MeterInfo.WakeUp_Request_ID > 0)
                {
                    if (((MIUF.IsPasswordTemporary || MIUF.IsDefaultPassWordActive) || (!MeterInfo.Schedule_MB.IsSuperImmediate && !MeterInfo.Schedule_EV.IsSuperImmediate && !MeterInfo.Schedule_CB.IsSuperImmediate && !MeterInfo.Schedule_LP.IsSuperImmediate && !MeterInfo.Schedule_PQ.IsSuperImmediate && !MeterInfo.Schedule_CS.IsSuperImmediate && !MeterInfo.Apply_new_contactor_state)))
                        DB_Controller.UpdateWakeUpProcess(false, 1, MeterInfo.WakeUp_Request_ID);
                    else
                        DB_Controller.UpdateWakeUpProcess(false, 0, MeterInfo.WakeUp_Request_ID);
                }
                DB_Controller.UpdateMeterSettings(MeterInfo, MIUF);
                MIUF = new MeterInfoUpdateFlags();
            }
            custExc.isTrue = IsProcessed;
            return custExc;
        }

        public void ReadRemoteGridStatus(ref List<GridStatusItem> temp_Param_RemoteGridStatus)
        {
            try
            {
                Param_Controller.GET_RemoteGridInputsStatus(ref temp_Param_RemoteGridStatus);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error while reading Remote Grid Status(Error Code:{0})", (int)MDCErrors.App_RemoteGridStatus), ex);
            }
        }
        public Param_MajorAlarmProfile ReadAlarmRegister()
        {
            try
            {
                Param_MajorAlarmProfile temp_Param_MajorAlarmProfile = new Param_MajorAlarmProfile();
                Param_Controller.GET_MajorAlarmProfile_AlarmStatus(ref temp_Param_MajorAlarmProfile);
                return temp_Param_MajorAlarmProfile;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error while reading Alarm Status Register(Error Code:{0})" + Environment.NewLine + "Detail: {1}", (int)MDCErrors.App_AlarmRegister_Read, ex.InnerException.Message), ex);
            }
        }

        public void ResetAlarmRegister()
        {
            try
            {
                bool isAnyAlarmTriggered = false;
                foreach (var item in ParamMajorAlarmProfileObj.AlarmItems)
                {
                    if (item != null && item.IsTriggered)
                    {
                        item.IsReset = true;
                        isAnyAlarmTriggered = true;
                    }
                }

                if (isAnyAlarmTriggered)
                {

                    #region Resetting Alarm Status
#if Enable_Abstract_Log
                    LogMessage("Resetting Alarm Status", "CAS", "R", 1);
#endif

#if !Enable_Abstract_Log
						LogMessage("Resetting Alarm Status",1);
#endif
                    #endregion
                    Data_Access_Result t = Param_Controller.SET_MajorAlarmProfile_Status(ParamMajorAlarmProfileObj);
                    #region Alarm Status Reset Complete with Status
#if Enable_Abstract_Log
                    LogMessage(String.Format("Alarm Status Reset Complete with Status= {0}", t.ToString()), "CAS", (t == Data_Access_Result.Success) ? "S" : "F", 1);
#endif

#if !Enable_Abstract_Log
						LogMessage(String.Format("Alarm Status Reset Complete with Status= {0}", t.ToString()),1);
#endif
                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error Resetting Alarm Register(Error Code:{0})", (int)MDCErrors.App_AlarmRegister_Reset), ex);
            }
        }

        public void ResetAlarmRegister(List<EventInfo> AlarmsToReset)
        {
            try
            {
                bool isAnyAlarmTriggered = false;
                foreach (var item in ParamMajorAlarmProfileObj.AlarmItems)
                {
                    if (item != null && item.IsTriggered)
                    {
                        bool isExist = AlarmsToReset.Exists((E_Info) => E_Info != null && item.Info != null &&
                                                                        E_Info._EventId == item.Info._EventId); //Event_Info.Id Condition
                        if (isExist)
                        {
                            item.IsReset = true;
                            isAnyAlarmTriggered = true;
                        }
                    }
                }
                if (isAnyAlarmTriggered)
                {
                    LogMessage("Resetting Alarm Status");
                    Data_Access_Result t = Param_Controller.SET_MajorAlarmProfile_Status(ParamMajorAlarmProfileObj);
                    LogMessage(String.Format("Only Selected Alarm Status Reset Complete with Status= {0}", t.ToString()));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error Resetting Alarm Register(Error Code:{0})", (int)MDCErrors.App_AlarmRegister_Reset), ex);
            }
        }

        public bool TryParameterize(CancellationTokenSource CancelTokenSource = null)
        {
            try
            {
                //MeterInfo.logoutMeter = false;
                if (PermissionToWriteParams == null && PermissionToWriteParams.Count < 32)
                    PermissionToWriteParams = Commons.GetSubscripotionArray(Settings.Default.PermissionParamsWrite);

                #region // If Task Canceled

                if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                {
                    CancelTokenSource.Token.ThrowIfCancellationRequested();
                }

                #endregion
                #region Contactor status

                if (MeterInfo.IsContactorSupported)
                {
                    string contactorAck = string.Empty;
                    try
                    {
                        if (MeterInfo.Apply_new_contactor_state)
                        {
                            #region Make Contactor Command

                            ContactorControlData contactReq;
                            // if return false that means there is no request pending disable process loop
                            var RequestPending = DB_Controller.getContactorRequest(MeterInfo, out contactReq, MeterInfo.Contactor_lock == 1);
                            if (!RequestPending)
                            {
                                MeterInfo.Apply_new_contactor_state = false;
                                MIUF.NoContactorRequestPending = true;
                            }
                            if (contactReq != null)
                            {
                                var command = (contactReq.Command == 1) ? true : false;
                                var shouldAquireContactorLock = contactReq.CommandType == ContactorCommandType.OnDemand && !command;

                                #region Commented Code_Section

                                // if (Settings.Default.ByPassContactorStateChecks)
                                // {
                                //     if (command && Settings.Default.GrantContactorOnPermission)
                                //     {
                                //         Param_Controller.RelayConnectRequest();
                                //         ContactorStateChanged(command, ref contactorAck);
                                //     }
                                //     else if(!command && Settings.Default.GrantContactorOFFPermission)
                                //     {
                                //         Param_Controller.RelayDisConnectRequest();
                                //         ContactorStateChanged(command, ref contactorAck);
                                //     }
                                // }
                                // else  

                                #endregion

                                if (MeterInfo.Contactor_lock == 1 &&
                                    contactReq.CommandType == ContactorCommandType.OnDemand && !command)
                                {
                                    if (ApplyContactorRequest(command))
                                    {
                                        MIUF.IsContactorStatusUpdate = true;
                                        //DB_Controller.UpdateContactorLiveData(MeterInfo.MSN, contactReq.ContactorID, DateTime.Now, command);
                                        DB_Controller.UpdateContactorLognData(MeterInfo.MSN, contactReq);
                                        DB_Controller.UpdateContactorLogLive(MeterInfo.MSN, contactReq);
                                    }
                                }
                                else if (MeterInfo.Contactor_lock == 0 && ApplyContactorRequest(command))
                                {
                                    MIUF.AquireContactorLock = shouldAquireContactorLock;
                                    MIUF.IsContactorStatusUpdate = true;
                                    //DB_Controller.UpdateContactorLiveData(MeterInfo.MSN, contactReq.ContactorID, DateTime.Now, command);
                                    DB_Controller.UpdateContactorLognData(MeterInfo.MSN, contactReq);
                                    DB_Controller.UpdateContactorLogLive(MeterInfo.MSN, contactReq);
                                }
                                else
                                {
                                    DB_Controller.UpdateContactorLognData(MeterInfo.MSN, contactReq);
                                }

                            }
                            else
                                goto Exit;

                            #endregion
                        }

                    }
                    catch (Exception ex)
                    {
                        LogMessage(ex, 4, "Contactor status");
                        if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                        StatisticsObj.InsertError(ex, _session_DateTime, 15);
                        MIUF.NoContactorRequestPending = false;
                    }
                    finally
                    {
                        if (MIUF.IsContactorStatusUpdate)
                            ResetMaxIOFailure();
                    }
                Exit: MeterInfo.IsMeterParameterized = true;
                }

                #endregion

                #region /// If Task Cancelled
                if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                {
                    CancelTokenSource.Token.ThrowIfCancellationRequested();
                }
                #endregion
                #region Contactor Params

                if (MeterInfo.Write_contactor_param && PermissionToWriteParams[(int)WriteParams.SetContactorParam])
                {
                    bool Success = false;
                    try
                    {
                        Param_ContactorExt obj_param = new Param_ContactorExt();
                        Param_Monitoring_Time obj_MT = new Param_Monitoring_Time();
                        Param_Limits obj_limits = new Param_Limits();
                        if (DB_Controller.GetContactorParamFromDB(MeterInfo.Contactor_param_id, ref obj_param, ref obj_limits, ref obj_MT))
                        {
                            if (obj_param.WriteContactorParam)
                            {
                                LogMessage("Setting Contactor Parameters", "CON", "PW");
                                if (Param_Controller.SET_Contactor_Params(obj_param))
                                {
                                    LogMessage("Setting Contactor Parameters Successful", "CON", "PS");
                                    Success = true;
                                }
                                else
                                {
                                    LogMessage("Error Setting Contactor Parameters", "CON", "PF");
                                    // MIUF.IsContactorParamsWrite = false;
                                    Success = false;
                                }
                            }
                            if (obj_MT.WriteOverLoad)
                            {
                                LogMessage("Setting Overload Monitoring Time", "OMT", "PW");
                                if (Param_Controller.SET_MonitoringTimeAll(obj_MT))
                                {
                                    LogMessage("Setting Overload Monitoring Time Successful", "OMT", "PS");
                                    Success = true;
                                }
                                else
                                {
                                    LogMessage("Error setting Overload Monitoring Time", "OMT", "PF");
                                    Success = false;
                                }
                            }
                            if (obj_limits.WriteOverLoadTotal_T1 || obj_limits.WriteOverLoadTotal_T2 || obj_limits.WriteOverLoadTotal_T3 || obj_limits.WriteOverLoadTotal_T4)
                            {
                                LogMessage("Setting Overload limits", "OL", "PW");
                                if (Param_Controller.SET_Limits_All(obj_limits))
                                {
                                    LogMessage("Setting Overload limits Successful", "OL", "PS");
                                    Success = true;
                                }
                                else
                                {
                                    LogMessage("Error setting Overload limits", "OL", "PF");
                                    Success = false;
                                }
                            }
                            //MIUF.IsContactorParamsWrite = true;
                            //MDC alarms
                            if (Success)
                            {
                                MDCAlarm.MDCCombineEvents[((ushort)MDCEvents.contactor_param)] = true;
                                MDCAlarm.IsMDCEventOuccer = true;

                                var warning = string.Format("Parameterization Occur. MDC writes Contactor Parameters.");
                                DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.contactor_param), MeterInfo, SessionDateTime);
                            }

                        }
                        else
                        {
                            LogMessage("Parameters not found with current Contactor Parameter id", "COND", "PF");

                        }
                    }
                    catch (Exception ex)
                    {
                        Success = false;
                        LogMessage(ex, 4, "Contactor Parameters");
                        // throw;
                        if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                        StatisticsObj.InsertError(ex, _session_DateTime, 15);
                    }
                    finally
                    {
                        if (Success)
                        {
                            ResetMaxIOFailure();
                            MeterInfo.IsMeterParameterized = true;
                            MIUF.IsContactorParamsWrite = true;
                        }
                    }
                }

                #endregion

                #region ///If Task Cancelled
                if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                {
                    CancelTokenSource.Token.ThrowIfCancellationRequested();
                }
                #endregion
                #region Modem Limits and Time

                if (!MeterInfo.PrioritizeWakeup && MeterInfo.Write_modem_limits_time && PermissionToWriteParams[(int)WriteParams.SetModemLimitsAndTime])
                {
                    var success = true;
                    try
                    {
                        Param_ModemLimitsAndTime obj_Param = new Param_ModemLimitsAndTime();
                        if (DB_Controller.GetModemParamFromDB(MeterInfo.Modem_limits_time_param_id, ref obj_Param))
                        {
                            LogMessage("Setting modem limits and time", "MLT", "PW");
                            if (Param_Controller.SET_Modem_Limit_Time(obj_Param))
                            {
                                LogMessage("Setting modem limits and time Successful", "MLT", "PS");
                                //MIUF.IsModemLimitsTimeWrite = true;

                                //MDC Alarms
                                MDCAlarm.MDCCombineEvents[((ushort)MDCEvents.param_m_limit_time)] = true;
                                MDCAlarm.IsMDCEventOuccer = true;

                                var warning = string.Format("Parameterization Occur. MDC writes Modem Limits and Time Parameters.");
                                DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.param_m_limit_time), MeterInfo, SessionDateTime);

                            }
                            else
                            {
                                LogMessage("Error setting modem limits and time", "MLT", "PF");
                                //MIUF.IsModemLimitsTimeWrite = false;
                                success = false;
                            }
                        }
                        else
                        {
                            LogMessage("Parameters not found with current Modem Parameter id", "MLTD", "F");

                        }

                    }
                    catch (Exception ex)
                    {
                        success = false;
                        LogMessage(ex, 4, "Modem Parameters");
                        // throw;
                        if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                        StatisticsObj.InsertError(ex, _session_DateTime, 15);
                    }
                    finally
                    {
                        if (success)
                        {
                            ResetMaxIOFailure();
                            MIUF.IsModemLimitsTimeWrite = true;
                            MeterInfo.IsMeterParameterized = true;
                        }
                    }
                }

                #endregion

                #region ///If Task Cancelled
                if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                {
                    CancelTokenSource.Token.ThrowIfCancellationRequested();
                }
                #endregion
                #region TIME SYNC

#if !Enable_LoadTester_Mode
                if ((MeterInfo.Schedule_CS.IsSuperImmediate || (!MeterInfo.PrioritizeWakeup && MeterInfo.Read_CS && MeterInfo.Schedule_CS.SchType != ScheduleType.Disabled)) && PermissionToWriteParams[(int)WriteParams.TimeSync])
                {
                    if (MeterInfo.Schedule_CS.IsSuperImmediate || MeterInfo.IsScheduleReadyToBeProcess(MeterInfo.Schedule_CS))
                    {
                        try
                        {
                            LogMessage("Time Synchronization in Process", "CS", "PW,MID:" + MeterInfo.ClockSyncronizationMethod);
                            #region Declarations
                            //Limits for settings meter clock
                            int timeSync_SecondsThreshold = Settings.Default.timeSync_SecondThreshold;
                            int timeSync_MinuteThreshold = Settings.Default.timeSync_MinuteThreshold;
                            int timeSync_TransmissionOffset_Seconds = Settings.Default.timeSync_transmissionOffset_Seconds;
                            int timeSync_TransmissionOffset_Minutes = Settings.Default.timeSync_transmissionOffset_Minutes;
                            var tranmissionOffset = new TimeSpan(0, timeSync_TransmissionOffset_Minutes, timeSync_TransmissionOffset_Seconds);


                            bool timeSynced = false;
                            bool shouldSynchronise = true;

                            #endregion

                            if (MeterInfo.ClockSyncronizationMethod == Byte.MaxValue ||
                                MeterInfo.ClockSyncronizationMethod == (byte)Clock_Synchronization_Method.SHIFT_TIME ||
                                MeterInfo.ClockSyncronizationMethod == (byte)Clock_Synchronization_Method.ADJUST_TO_PRESET_TIME
                                )
                            {
                                #region Initial Logic

                                var meterDatetime = new Param_Clock_Caliberation();
                                var mdiParam = new Param_MDI_parameters();
                                meterDatetime.Set_Time = DateTime.Now;

                                try
                                {
                                    Param_Controller.GET_MDI_Auto_Reset_Date(ref mdiParam);
                                    byte currentDay = Convert.ToByte(DateTime.Now.Day);
                                    byte currentMonth = Convert.ToByte(DateTime.Now.Month);

                                    if ((mdiParam.Auto_reset_date.DayOfMonth.Equals(0xfd) || mdiParam.Auto_reset_date.DayOfMonth.Equals(0xfe)) && currentMonth.Equals(2) && (currentDay.Equals(27) || currentDay.Equals(28) || currentDay.Equals(29)))
                                    {
                                        shouldSynchronise = false;
                                    }
                                    if (mdiParam.Auto_reset_date.DayOfMonth.Equals(currentDay))
                                    {
                                        shouldSynchronise = false;
                                    }
                                    else if (mdiParam.Auto_reset_date.DayOfMonth == 0xfd)
                                    {
                                        if (currentDay == 29 || currentDay == 30)
                                            shouldSynchronise = false;
                                    }
                                    else if (mdiParam.Auto_reset_date.DayOfMonth == 0xfe)
                                    {
                                        if (currentDay == 30 || currentDay == 31)
                                            shouldSynchronise = false;
                                    }
                                    else
                                    {
                                        shouldSynchronise = true;
                                    }
                                }
                                catch { shouldSynchronise = true; }




                                Param_Controller.GET_MeterClock_Date_Time(ref meterDatetime);
                                var timereceived = meterDatetime.Set_Time;
                                meterDatetime.Set_Time += tranmissionOffset;


                                TimeSpan timeDifference = meterDatetime.Set_Time - DateTime.Now;
                                int diff_seconds = timeDifference.Seconds;
                                int diff_minutes = timeDifference.Minutes;
                                int diff_hours = timeDifference.Hours;
                                int diff_days = timeDifference.Days;

                                if (diff_seconds < 0) diff_seconds *= -1;
                                if (diff_minutes < 0) diff_minutes *= -1;
                                if (diff_hours < 0) diff_hours *= -1;
                                if (diff_days < 0) diff_days *= -1;
                                #endregion
                                #region Try Set Clock

                                if (shouldSynchronise && (diff_seconds > timeSync_SecondsThreshold || diff_minutes > timeSync_MinuteThreshold || diff_hours > 0 || diff_days > 0))
                                {
                                    var difference_cs = timeDifference.TotalSeconds;
                                    //if (difference_cs < 0) difference_cs *= -1;

                                    meterDatetime.Set_Time = DateTime.Now + tranmissionOffset;


                                    var batteryDead = (param_MajorAlarmProfile_obj == null) ? false : (param_MajorAlarmProfile_obj.MA_Status_Array[21] & param_MajorAlarmProfile_obj.MA_Status_Array[33]);
                                    var shouldSyncOnBatteryDead = batteryDead & TimeSyncOnBatteryDead;

                                    if ((Math.Abs(difference_cs) <= MeterInfo.Max_cs_difference) || (EnableInvaldiClockSync && (!batteryDead || shouldSyncOnBatteryDead)))
                                    {
                                        #region Write New Time
                                        //need to synchronize time
                                        if (MeterInfo.ClockSyncronizationMethod == Byte.MaxValue)
                                        {
                                            Data_Access_Result result = Param_Controller.SET_MeterClock_Date_Time(meterDatetime);
                                            timeSynced = result == Data_Access_Result.Success;
                                        }
                                        else if (MeterInfo.ClockSyncronizationMethod == (byte)Clock_Synchronization_Method.SHIFT_TIME)
                                        {
                                            Action_Result result = Param_Controller.ShiftTime((short)(-1 * difference_cs));
                                            timeSynced = result == Action_Result.Success;
                                        }
                                        else if (MeterInfo.ClockSyncronizationMethod == (byte)Clock_Synchronization_Method.ADJUST_TO_PRESET_TIME)
                                        {
                                            //multiplication with 3 is due to 3 time communication exchange before activating time
                                            DateTime PresetTime = DateTime.Now.AddSeconds(tranmissionOffset.TotalSeconds * 3);
                                            DateTime ValidityStartInterval = timereceived.AddDays(-1);
                                            DateTime ValidityEndInterval = timereceived.AddDays(1);
                                            Action_Result result = Param_Controller.PresetAdjustingTime(PresetTime, ValidityStartInterval, ValidityEndInterval);
                                            if (result == Action_Result.Success)
                                            {
                                                result = Param_Controller.AdjustTimeToPresetTime();
                                                timeSynced = result == Action_Result.Success;
                                            }
                                        }
                                        if (timeSynced && MeterInfo.EnableLiveUpdate)
                                            UpdateCsLiveDB(meterDatetime.Set_Time, MeterInfo.MSN);
                                        #endregion
                                    }
                                    if (Math.Abs(difference_cs) <= MeterInfo.Max_cs_difference && timeSynced)
                                    {
                                        MDCAlarm.MDCCombineEvents[((ushort)MDCEvents.cs_valid_sync)] = true;
                                        MDCAlarm.IsMDCEventOuccer = true;
                                        var warning = string.Format("Time Synchronized, Meter Time Received: {0}, Updated Time: {1}", timereceived.ToString("yyyy-MM-dd HH:mm:ss"), meterDatetime.Set_Time.ToString("yyyy-MM-dd HH:mm:ss"));
                                        LogMessage(warning, "CS", string.Format("PS, {0} - {1}", timereceived.ToString("yyyy-MM-dd HH:mm:ss"), meterDatetime.Set_Time.ToString("yyyy-MM-dd HH:mm:ss")));
                                        DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.cs_valid_sync), MeterInfo, SessionDateTime);
                                    }
                                    else if (batteryDead)
                                    {
                                        #region battery Dead Clock Synchronization

                                        MDCAlarm.MDCCombineEvents[((ushort)MDCEvents.rtc_failed_battery)] = true;
                                        MDCAlarm.IsMDCEventOuccer = true;
                                        if (shouldSyncOnBatteryDead && timeSynced)
                                        {
                                            MDCAlarm.MDCCombineEvents[((ushort)MDCEvents.cs_invalid_sync)] = true;
                                            MDCAlarm.IsMDCEventOuccer = true;
                                            var warning = string.Format("Incorrect Time, Meter Time Received: {0} Server is Synchronizing Clock", timereceived.ToString("yyyy-MM-dd HH:mm:ss"));
                                            DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.rtc_failed_battery), MeterInfo, SessionDateTime);
                                            LogMessage(warning, "CS", string.Format("PS, {0} - {1}", timereceived.ToString("yyyy-MM-dd HH:mm:ss"), meterDatetime.Set_Time.ToString("yyyy-MM-dd HH:mm:ss")));
                                            //LogMessage(string.Format("Time Synchronized Current Meter Time: {0}", meterDatetime.Set_Time.ToString("yyyy-MM-dd HH:mm:ss")), 1);
                                        }
                                        else
                                        {
                                            var warning = string.Format("Clock Synchronization is disabled by server due to Battery failure");
                                            DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.rtc_failed_battery), MeterInfo, SessionDateTime);
                                            LogMessage(warning, "CS", "D, " + meterDatetime.Set_Time.ToString("yyyy-MM-dd HH:mm:ss"));
                                            MIUF.IsDisableCS = true;
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        if (EnableInvaldiClockSync && timeSynced)
                                        {
                                            MDCAlarm.MDCCombineEvents[((ushort)MDCEvents.cs_invalid_sync)] = true;
                                            MDCAlarm.IsMDCEventOuccer = true;
                                            var warning = string.Format("Incorrect Time, Meter Time Received: {0} Server is Synchronizing Clock", timereceived.ToString("yyyy-MM-dd HH:mm:ss"));
                                            DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.cs_invalid_sync), MeterInfo, SessionDateTime);
                                            LogMessage(warning, "CS", string.Format("PS, {0} - {1}", timereceived.ToString("yyyy-MM-dd HH:mm:ss"), meterDatetime.Set_Time.ToString("yyyy-MM-dd HH:mm:ss")));
                                            //LogMessage(string.Format("Time Synchronized Current Meter Time: {0}", meterDatetime.Set_Time.ToString("yyyy-MM-dd HH:mm:ss")), 1);
                                        }
                                        else
                                        {
                                            var warning = string.Format("Incorrect Time, Meter Time Received: {0} Server is Disabling Clock", timereceived.ToString("yyyy-MM-dd HH:mm:ss"));
                                            DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.cs_invalid_sync), MeterInfo, SessionDateTime);
                                            LogMessage(warning, "CS", "D, " + timereceived.ToString("yyyy-MM-dd HH:mm:ss") + " - " + meterDatetime.Set_Time.ToString("yyyy-MM-dd HH:mm:ss"));
                                            MIUF.IsDisableCS = true;
                                        }

                                    }
                                    //DB_Controller.InsertWarning(MeterInfo.MSN, MeterInfo.Reference_no, SessionDateTime, ConnectToMeter.ConnectionTime, warning);

                                    //if (!EnableInvaldiClockSync) MIUF.IsDisableCS = true;// disabling Clock Sync
                                    if (MeterInfo.Schedule_CS.IsSuperImmediate) MIUF.SuperImmediate_CS = true;
                                    MeterInfo.Schedule_CS.IsSuperImmediate = false;
                                }
                                else
                                {
                                    LogMessage("Meter Time already Synchronized", "CS", "PU", 1);
                                    MeterInfo.IsMeterParameterized = true;
                                    timeSynced = true;
                                    //no need to synchronize time
                                }

                                #endregion
                            }
                            else
                            {
                                Clock_Synchronization_Method CSMethod = (Clock_Synchronization_Method)MeterInfo.ClockSyncronizationMethod;
                                Action_Result result = Action_Result.Temporary_failure;
                                switch (CSMethod)
                                {
                                    case Clock_Synchronization_Method.ADJUST_TO_QUARTER:
                                        result = Param_Controller.AdjustTimeToQuarter();
                                        break;
                                    case Clock_Synchronization_Method.ADJUST_TO_MINUTE:
                                        result = Param_Controller.AdjustTimeToMinute();
                                        break;
                                    case Clock_Synchronization_Method.ADJUST_TO_MEASURING_PERIOD:
                                        result = Param_Controller.AdjustTimeToMeasuringPeriod();
                                        break;
                                }
                                timeSynced = result == Action_Result.Success;
                                if (timeSynced)
                                    LogMessage(string.Empty, "CS", "PS");
                            }

                            if (timeSynced)
                            {
                                #region Update Info
                                MeterInfo.PreUpdateSchedule(MeterInfo.Schedule_CS, SessionDateTime);
                                MIUF.Schedule_CS = true;
                                MIUF.last_CS_time = true;
                                if (MeterInfo.Schedule_CS.IsSuperImmediate)
                                    MIUF.SuperImmediate_CS = true;
                                if (MeterInfo.Schedule_CS.SchType == ScheduleType.IntervalFixed || MeterInfo.Schedule_CS.SchType == ScheduleType.IntervalRandom)
                                    MIUF.base_time_CS = true;
                                MeterInfo.Schedule_CS.IsSuperImmediate = false;
                                #endregion
                                //Change Parameterization Status
                                MeterInfo.IsMeterParameterized = true;
                                ResetMaxIOFailure();
                            }
                            else
                            {
                                LogMessage("Time Synchronization Failed", "CS", "PF", 1);
                            }
                        }
                        catch (Exception ex)
                        {
                            LogMessage(ex, 4, "Time Synchronization");
                            // throw;
                            if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            StatisticsObj.InsertError(ex, _session_DateTime, 15);
                        }
                    }
                    else
                    {
                        MeterInfo.IsMeterParameterized = true;
                    }
                }
#endif

                #endregion

                #region ///If Task Cancelled
                if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                {
                    CancelTokenSource.Token.ThrowIfCancellationRequested();
                }
                #endregion
                #region SET_KeepAlive

#if !Enable_LoadTester_Mode

                if (!MeterInfo.PrioritizeWakeup && MeterInfo.SetKeepAlive > 0 && PermissionToWriteParams[(int)WriteParams.SetKeepAlive])
                {
                    try
                    {
                        LogMessage("Setting Keep Alive Flag", "KA", "PW");
                        Param_OBJ.SET_KeepAlive(MeterInfo.SetKeepAlive);
                        LogMessage(String.Format("Keep Alive Flag Set, KeepAlive Flag: {0}", Param_OBJ.Param_Keep_Alive_IP_object.Enabled), "KA", "PS, " + Param_OBJ.Param_Keep_Alive_IP_object.Enabled, 1);
                        MeterInfo.SetKeepAlive = 0;
                        MIUF.SetKeepAlive = true;
                        //Change Parameterization Status
                        MeterInfo.IsMeterParameterized = true;
                        MeterInfo.logoutMeter = true;

                        //MDC Alarms
                        MDCAlarm.MDCCombineEvents[((ushort)MDCEvents.type_change)] = true;
                        MDCAlarm.IsMDCEventOuccer = true;
                        var warning = string.Format("Parameterization Occur. MDC writes Meter type (KeepAlive or Non-KeepAlive) Parameters.");
                        DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.type_change), MeterInfo, SessionDateTime);

                    }
                    catch (Exception ex)
                    {
                        // throw ex;
                        MeterInfo.SetKeepAlive = 0;
                        MIUF.SetKeepAlive = false;
                        LogMessage(ex, 4, "Keep Alive Flag");
                        if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                        StatisticsObj.InsertError(ex, _session_DateTime, 15);
                    }
                    finally
                    {
                        if (MIUF.SetKeepAlive)
                            ResetMaxIOFailure();
                    }
                }

#endif

                #endregion

                #region ///If Task Cancelled
                if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                {
                    CancelTokenSource.Token.ThrowIfCancellationRequested();
                }
                #endregion
                #region SET_IPProfiles

#if !Enable_LoadTester_Mode
                if (!MeterInfo.PrioritizeWakeup && MeterInfo.SetIPProfile > 0 && PermissionToWriteParams[(int)WriteParams.SetIPProfile])
                {
                    try
                    {
                        LogMessage("Setting IP Profiles", "IP", "PW");
                        if (Param_OBJ.SET_IPProfiles(MeterInfo.SetIPProfile, MeterInfo.StandardParameter))
                        {
                            //LogMessage(String.Format("IP Profiles Set Successfully, IP: {0}, Port: {1}", DLMS.DLMS_Common.LongToIPAddressString(Param_OBJ.Param_IP_Profiles_object[0].IP), Param_OBJ.Param_IP_Profiles_object[0].Wrapper_Over_TCP_port), 1);
                            LogMessage("", "IP", string.Format("PS, {0}:{1}", DLMS.DLMS_Common.LongToIPAddressString(Param_OBJ.Param_IP_Profiles_object[0].IP), Param_OBJ.Param_IP_Profiles_object[0].Wrapper_Over_TCP_port));
                            MeterInfo.SetIPProfile = 0;
                            MIUF.SetIpProfile = true;
                            //Change Parameterization Status
                            MeterInfo.IsMeterParameterized = true;
                            MeterInfo.logoutMeter = true;
                        }
                        else
                        {
                            LogMessage("Setting IP Profiles", "IP", "F,NF");
                        }
                    }
                    catch (Exception ex)
                    {
                        //throw ex;
                        MIUF.SetIpProfile = false;
                        LogMessage(ex, 4, "IP Profiles Parameters");
                        if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                        StatisticsObj.InsertError(ex, _session_DateTime, 15);
                    }
                    finally
                    {
                        if (MIUF.SetIpProfile)
                            ResetMaxIOFailure();
                    }
                }
#endif

                #endregion

                #region ///If Task Cancelled
                if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                {
                    CancelTokenSource.Token.ThrowIfCancellationRequested();
                }
                #endregion
                #region Modem Initialization
#if !Enable_LoadTester_Mode
                if (PermissionToWriteParams[(int)WriteParams.SetModemInit])
                {
                    if (!MeterInfo.PrioritizeWakeup && MeterInfo.SetModemInitializeBasics > 0 && MeterInfo.SetModemInitializeExtended > 0)
                    {
                        var success = true;
                        try
                        {
                            LogMessage("Complete Modem Initializing in process", "MI", "PW");
                            Param_OBJ.SET_ModemInitializeComplete(MeterInfo.SetModemInitializeBasics);
                            LogMessage("Complete Modem Initializing successful", "MI", "PS", 1);
                            //Change Parameterization Status
                            MeterInfo.logoutMeter = true;
                        }
                        catch (Exception ex)
                        {
                            success = false;
                            //throw ex;
                            LogMessage(ex, 4, "Complete Modem Initializing");
                            if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            StatisticsObj.InsertError(ex, _session_DateTime, 15);
                        }
                        finally
                        {
                            if (success)
                            {
                                ResetMaxIOFailure();
                                MeterInfo.SetModemInitializeBasics = 0;
                                MeterInfo.SetModemInitializeExtended = 0;
                                MIUF.SetModemInitializeBasics = true;
                                MIUF.SetModemInitializeExtended = true;
                                MeterInfo.IsMeterParameterized = true;
                            }
                        }
                    }
                    else
                    {
                        #region SET_ModemInitializeBasic
                        if (MeterInfo.SetModemInitializeBasics > 0)
                        {
                            try
                            {
                                LogMessage("Initializing Modem Basic", "MIB", "PW");
                                Param_OBJ.SET_ModemInitializeBasic(MeterInfo.SetModemInitializeBasics);
                                LogMessage("Initializing Modem Basic successful", "MIB", "PS", 1);
                                ResetMaxIOFailure();

                                //Change Parameterization Status
                                MeterInfo.IsMeterParameterized = true;
                                MeterInfo.logoutMeter = true;


                            }
                            catch (Exception ex)
                            {
                                //throw ex;
                                LogMessage(ex, 4, "Initializing Modem Basic");
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                                StatisticsObj.InsertError(ex, _session_DateTime, 15);
                            }
                            finally
                            {
                                MeterInfo.SetModemInitializeBasics = 0;
                                MIUF.SetModemInitializeBasics = true;
                            }
                        }
                        #endregion

                        #region SET_ModemInitializeExtended


                        if (MeterInfo.SetModemInitializeExtended > 0)
                        {
                            try
                            {
                                LogMessage("Initializing Modem Extended", "MIE", "PW");
                                Param_OBJ.SET_ModemInitializeExtended(MeterInfo.SetModemInitializeExtended);
                                LogMessage("Initializing Modem Extended successful", "MIE", "PS", 1);
                                ResetMaxIOFailure();

                                //Change Parameterization Status
                                MeterInfo.IsMeterParameterized = true;
                                MeterInfo.logoutMeter = true;

                            }
                            catch (Exception ex)
                            {
                                //throw ex;
                                LogMessage(ex, 4, "Initializing Modem Extended");
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                                StatisticsObj.InsertError(ex, _session_DateTime, 15);
                            }
                            finally
                            {
                                MeterInfo.SetModemInitializeExtended = 0;
                                MIUF.SetModemInitializeExtended = true;
                            }
                        }

                        #endregion
                    }
                }
#endif
                #endregion

                #region ///If Task Cancelled
                if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                {
                    CancelTokenSource.Token.ThrowIfCancellationRequested();
                }
                #endregion
                #region Timebase Events
#if !Enable_LoadTester_Mode
                if (PermissionToWriteParams[(int)WriteParams.SetTimeBaseEvents])
                {
                    #region Timebase Event 1
                    if (!MeterInfo.PrioritizeWakeup &&
                        MeterInfo.TBE1WriteRequestID > 0)
                    {
                        var success = true;
                        try
                        {
                            LogMessage("Setting Timebase Event 1", "TBE1", "PW");
                            Events_OBJ.SET_TimeBaseEvent_1(MeterInfo.TBE1WriteRequestID);
                            LogMessage("Timebase Event 1 Set successfully", "TBE1", "PS", 1);
                            //Change Parameterization Status
                            MeterInfo.IsMeterParameterized = true;
                            MeterInfo.logoutMeter = true;

                            //MDC Alarms
                            MDCAlarm.MDCCombineEvents[((ushort)MDCEvents.tbe1_change)] = true;
                            MDCAlarm.IsMDCEventOuccer = true;

                            var warning = string.Format("Parameterization Occur. MDC writes Time Based Event 1 Parameters.");
                            DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.tbe1_change), MeterInfo, SessionDateTime);
                        }
                        catch (Exception ex)
                        {
                            success = false;
                            //throw ex;
                            LogMessage(ex, 4, "Time based Event 1");
                            if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            StatisticsObj.InsertError(ex, _session_DateTime, 15);
                        }
                        finally
                        {
                            if (success)
                            {
                                ResetMaxIOFailure();
                                MeterInfo.TBE1WriteRequestID = 0;
                                MIUF.TBE1WriteRequestID = true;
                            }
                        }
                    }
                    #endregion
                    #region Timebase Event 2
                    if (!MeterInfo.PrioritizeWakeup && MeterInfo.TBE2WriteRequestID > 0)
                    {
                        var success = true;
                        try
                        {
                            LogMessage("Setting Time based Event 2", "TBE2", "PW");
                            Events_OBJ.SET_TimeBaseEvent_2(MeterInfo.TBE2WriteRequestID);
                            LogMessage("Time based Event 2 Set successfully", "TBE2", "PS", 1);

                            //Change Parameterization Status
                            MeterInfo.IsMeterParameterized = true;
                            MeterInfo.logoutMeter = true;

                            //MDC Alarms
                            MDCAlarm.MDCCombineEvents[((ushort)MDCEvents.tbe2_change)] = true;
                            MDCAlarm.IsMDCEventOuccer = true;

                            var warning = string.Format("Parameterization Occur. MDC writes Time Based Event 2 Parameters.");
                            DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.tbe2_change), MeterInfo, SessionDateTime);
                        }
                        catch (Exception ex)
                        {
                            success = false;
                            //throw ex;
                            LogMessage(ex, 4, "Time based Event 2");
                            if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            StatisticsObj.InsertError(ex, _session_DateTime, 15);
                        }
                        finally
                        {
                            if (success)
                            {
                                ResetMaxIOFailure();
                                MeterInfo.TBE2WriteRequestID = 0;
                                MIUF.TBE2WriteRequestID = true;
                            }
                        }
                    }
                    #endregion
                }
#endif
                #endregion

                #region ///If Task Cancelled
                if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                {
                    CancelTokenSource.Token.ThrowIfCancellationRequested();
                }
                #endregion
                #region Disable TBEx On Power Fail Flags

                if (!MeterInfo.PrioritizeWakeup && MeterInfo.TBEPowerFailParamOptions > 0 && PermissionToWriteParams[(int)WriteParams.SetDisableTBEsFlag])
                {
                    var success = true;
                    try
                    {

                        bool IsTBE1Valid = false;
                        bool IsTBE2Valid = false;
                        var obj_TBEDisable = new TBE_PowerFail();

                        #region Initail Logic
                        byte[] bytes = new byte[1];
                        bytes[0] = MeterInfo.TBEPowerFailParamOptions;
                        var bits = new BitArray(bytes);

                        if (bits[0] == true && bits[2] == false)
                        {
                            obj_TBEDisable.disableEventAtPowerFail_TBE1 = 0;
                            IsTBE1Valid = true;
                        }
                        else if (bits[0] == false && bits[2] == true)
                        {
                            obj_TBEDisable.disableEventAtPowerFail_TBE1 = 1;
                            IsTBE1Valid = true;
                        }
                        else if (bits[0] == false && bits[2] == true)
                        {
                            IsTBE1Valid = false;
                        }

                        if (bits[1] == true && bits[3] == false)
                        {
                            obj_TBEDisable.disableEventAtPowerFail_TBE2 = 0;
                            IsTBE2Valid = true;
                        }
                        else if (bits[1] == false && bits[3] == true)
                        {
                            obj_TBEDisable.disableEventAtPowerFail_TBE2 = 1;
                            IsTBE2Valid = true;
                        }
                        else if (bits[1] == false && bits[3] == false)
                        {
                            IsTBE2Valid = false;
                        }


                        #endregion

                        if (IsTBE1Valid && IsTBE2Valid)
                        {
                            LogMessage("Setting Disable TBE on PowerFail Flag", "DTPF", "PW", 1);
                            if (Events_OBJ.SET_Disable_TBE_on_PowerFail(obj_TBEDisable))
                            {
                                LogMessage("Setting Disable TBE on PowerFail Successful", "DTPF", "PS", 1);
                                MeterInfo.IsMeterParameterized = true;
                            }
                            else
                            {
                                LogMessage("Error Setting Disable TBE on PowerFail Flag", "DTPF", "PF", 1);
                                success = false;
                            }
                        }
                        else
                        {
                            LogMessage("Invalid Parameters of Disable TBE on PowerFail Flag", "DTPFD", "F", 1);
                            MeterInfo.IsMeterParameterized = true;
                        }

                    }
                    catch (Exception ex)
                    {
                        success = false;
                        LogMessage(ex, 4, "TBEx On Power Fail Flags");
                        if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                        StatisticsObj.InsertError(ex, _session_DateTime, 15);
                    }
                    finally
                    {
                        if (success)
                        {
                            ResetMaxIOFailure();
                            MIUF.ISDisableTBEOnPowerFailWrite = true;
                        }
                    }



                }
                #endregion

                #region ///If Task Cancelled
                if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                {
                    CancelTokenSource.Token.ThrowIfCancellationRequested();
                }
                #endregion
                #region Set Major Alarms

                if (!MeterInfo.PrioritizeWakeup && MeterInfo.MajorAlarmGroupID > 0 && ParamMajorAlarmProfileObj != null && PermissionToWriteParams[(int)WriteParams.SetMajorAlarm])
                {
                    var sucess = true;
                    try
                    {

                        LogMessage("Setting Major Alarms", "MA", "PW");
                        char[] MajorAlarms = Commons.HexStringToBinary(MeterInfo.MajorAlarmsString, _EventController.EventLogInfoList.FindAll(x => x.EventCode != 0).Count);

                        //Param_MajorAlarmProfile Param_MajorAlarmProfile_Set = new Param_MajorAlarmProfile();
                        //Param_OBJ.ReadMajorAlarm(MeterInfo.MajorAlarmGroupID);
                        //Param_MajorAlarmProfile_Set._BitLength = MajorAlarms.Length;

                        for (int i = 0; i < ParamMajorAlarmProfileObj.AlarmItems.Count && i < MajorAlarms.Length; i++)
                        {
                            try
                            {
                                MajorAlarm Alarm = param_MajorAlarmProfile_obj.AlarmItems[i];
                                if (Alarm != null)
                                {
                                    Alarm.IsMajorAlarm = MajorAlarms[i].Equals('1') ? true : false;
                                    //Param_MajorAlarmProfile_Set.AlarmItems.Add(Alarm);
                                }
                            }
                            catch (Exception ex)
                            {
                                sucess = false;
                                LogMessage(ex, DefaultExceptionLevel);
                            }
                        }
                        Data_Access_Result temp = Param_Controller.SET_MajorAlarmProfile_Filter(param_MajorAlarmProfile_obj);
                        Data_Access_Result temp_1 = Param_Controller.SET_MajorAlarmProfile_Status(param_MajorAlarmProfile_obj);

                        if (temp == Data_Access_Result.Success && temp_1 == Data_Access_Result.Success)
                        {
                            //Change Parameterization Status
                            MeterInfo.IsMeterParameterized = true;
                            LogMessage("Setting Major Alarms Successful", "MA", "PS", 1);
                        }
                        else
                        {
                            LogMessage("Setting Major Alarms is Unsuccessful", "MA", "PF", 1);
                            sucess = false;
                        }


                    }
                    catch (Exception ex)
                    {
                        //throw new Exception("Error While Setting Major Alarms " + ex.Message);
                        LogMessage(ex, 4, "Set Major Alarms");
                        if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                        StatisticsObj.InsertError(ex, _session_DateTime, 15);
                    }
                    finally
                    {
                        if (sucess)
                        {
                            ResetMaxIOFailure();
                            MeterInfo.MajorAlarmGroupID = 0;
                            MIUF.MajorAlarmGroupID = true;
                        }
                    }
                }
                #endregion

                #region ///If Task Cancelled
                if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                {
                    CancelTokenSource.Token.ThrowIfCancellationRequested();
                }
                #endregion
                #region Unset Major Alarms

                if (MeterInfo.Unset_MajorAlarms && PermissionToWriteParams[(int)WriteParams.UnsetMajorAlarm])
                {
                    var success = true;
                    try
                    {
                        LogMessage("Unsetting Major Alarms", "UMA", "PW");
                        foreach (MajorAlarm item in ParamMajorAlarmProfileObj.AlarmItems)
                        {
                            item.IsMajorAlarm = false;
                        }

                        if (Param_Controller.SET_MajorAlarmProfile_Filter(ParamMajorAlarmProfileObj) == Data_Access_Result.Success)
                        {
                            //DB_Controller.UpdateUnsetMA(MSN);

                            //Change Parameterization Status
                            MeterInfo.IsMeterParameterized = true;
                            LogMessage("Unsetting Major Alarms Successful", "UMA", "PS", 1);
                        }
                        else
                        {
                            LogMessage("Unsetting Major Alarms is Unsuccessful", "UMA", "PF", 1);
                            success = false;
                        }

                    }
                    catch (Exception ex)
                    {
                        success = false;
                        //throw new Exception("Error While Unsetting Major Alarms " + ex.Message);
                        LogMessage(ex, 4, "Set Major Alarms");
                        if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                        StatisticsObj.InsertError(ex, _session_DateTime, 15);
                    }
                    finally
                    {
                        if (success)
                        {
                            ResetMaxIOFailure();
                            MeterInfo.Unset_MajorAlarms = false;
                            MIUF.UnsetMajorAlarms = true;
                        }
                    }
                }


                #endregion

                #region ///If Task Cancelled
                if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                {
                    CancelTokenSource.Token.ThrowIfCancellationRequested();
                }
                #endregion
                #region SET_Load Profile Interval

#if !Enable_LoadTester_Mode
                if (!MeterInfo.PrioritizeWakeup && PermissionToWriteParams[(int)WriteParams.SetLoadProfileInterval])
                {
                    if (MeterInfo.LPParamRequest.ChangeIntervalRequestLP1)
                        WriteLoadProfileInterval(MeterInfo.LP_Counters.Period, LoadProfileScheme.Load_Profile, ref MeterInfo.LPParamRequest.ChangeIntervalRequestLP1, ref MIUF.LP1_IntervalWriteRequest);
                    if (MeterInfo.LPParamRequest.ChangeIntervalRequestLP2)
                        WriteLoadProfileInterval(MeterInfo.LP2_Counters.Period, LoadProfileScheme.Load_Profile_Channel_2, ref MeterInfo.LPParamRequest.ChangeIntervalRequestLP2, ref MIUF.LP2_IntervalWriteRequest);
                    if (MeterInfo.LPParamRequest.ChangeIntervalRequestLP3)
                        WriteLoadProfileInterval(MeterInfo.LP3_Counters.Period, LoadProfileScheme.Daily_Load_Profile, ref MeterInfo.LPParamRequest.ChangeIntervalRequestLP3, ref MIUF.LP3_IntervalWriteRequest);
                }
#endif

                #endregion

                #region ///If Task Cancelled
                if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                {
                    CancelTokenSource.Token.ThrowIfCancellationRequested();
                }
                #endregion
                #region SET_Load Profile Channels

#if !Enable_LoadTester_Mode
                if (!MeterInfo.PrioritizeWakeup && PermissionToWriteParams[(int)WriteParams.SetLoadProfileChannel])
                {
                    if (MeterInfo.LPParamRequest.ChannelRequestLP1)
                        WriteLoadProfileChannels(MeterInfo.LP_Counters.GroupId, LoadProfileScheme.Load_Profile, ref MeterInfo.LPParamRequest.ChannelRequestLP1, ref MIUF.LP1_ChannelsWriteRequest);
                    if (MeterInfo.LPParamRequest.ChannelRequestLP2)
                        WriteLoadProfileChannels(MeterInfo.LP2_Counters.GroupId, LoadProfileScheme.Load_Profile_Channel_2, ref MeterInfo.LPParamRequest.ChannelRequestLP2, ref MIUF.LP2_ChannelsWriteRequest);
                }
#endif

                #endregion

                #region ///If Task Cancelled
                if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                {
                    CancelTokenSource.Token.ThrowIfCancellationRequested();
                }
                #endregion
                #region SET Display Windows Normal

#if !Enable_LoadTester_Mode
                if (!MeterInfo.PrioritizeWakeup && MeterInfo.DW_NormalID > 0 && PermissionToWriteParams[(int)WriteParams.SetDisplayWindows])
                {
                    var success = true;
                    try
                    {
                        LogMessage("Setting Display Windows for Normal Mode", "DWN", "PW");
                        if (Param_OBJ.SET_DisplayWindows_Nor(MeterInfo.DW_NormalID, TimeSpan.FromSeconds(MeterInfo.DW_ScrollTime), MeterInfo.DW_Normal_format))
                        {
                            LogMessage("Display Windows for Normal Mode Set successfully", "DWN", "PS", 1);
                            //Change Parameterization Status
                            MeterInfo.IsMeterParameterized = true;
                        }
                        else
                        {
                            success = false;
                            LogMessage("Display Windows for Normal Mode Setting unsuccessful", "DWN", "PF", 1);
                        }
                    }
                    catch (Exception ex)
                    {
                        //throw ex;
                        success = false;
                        LogMessage(ex, 4, "Display Windows Normal");
                        if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                        StatisticsObj.InsertError(ex, _session_DateTime, 15);
                    }
                    finally
                    {
                        if (success)
                        {
                            ResetMaxIOFailure();
                            MeterInfo.DW_NormalID = 0;
                            MIUF.DW_NormalID = true;
                        }
                    }
                }
#endif

                #endregion

                #region ///If Task Cancelled
                if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                {
                    CancelTokenSource.Token.ThrowIfCancellationRequested();
                }
                #endregion
                #region SET Display Windows Alternate

#if !Enable_LoadTester_Mode
                if (!MeterInfo.PrioritizeWakeup && MeterInfo.DW_AlternateID > 0 && PermissionToWriteParams[(int)WriteParams.SetDisplayWindows])
                {
                    var success = true;
                    try
                    {
                        LogMessage("Setting Display Windows for Alternate Mode", "DWA", "PW");
                        if (Param_OBJ.SET_DisplayWindows_Alt(MeterInfo.DW_AlternateID, TimeSpan.FromSeconds(MeterInfo.DW_ScrollTime), MeterInfo.DW_Alternate_format))
                        {
                            LogMessage("Display Windows for Alternate Mode Set successfully", "DWA", "PS", 1);
                            //Change Parameterization Status
                            MeterInfo.IsMeterParameterized = true;
                        }
                        else
                        {
                            LogMessage("Display Windows for Alternate Mode Setting unsuccessful", "DWA", "PF", 1);
                            success = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        //throw ex;
                        success = false;
                        LogMessage(ex, 4, "Display Windows Alternate");
                        if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                        StatisticsObj.InsertError(ex, _session_DateTime, 15);
                    }
                    finally
                    {
                        if (success)
                        {
                            ResetMaxIOFailure();
                            MeterInfo.DW_AlternateID = 0;
                            MIUF.DW_AlternateID = true;
                        }
                    }
                }
#endif

                #endregion

                #region ///If Task Cancelled
                if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                {
                    CancelTokenSource.Token.ThrowIfCancellationRequested();
                }
                #endregion
                #region Password

                //if (MeterInfo.Write_password_flag && MeterInfo.New_password_activation_time < DateTime.Now && MeterInfo.New_password_activation_time > MeterInfo.Last_Password_update_time )
                //{
                if (MeterInfo.Write_password_flag && PermissionToWriteParams[(int)WriteParams.SetPassword])
                {
                    if (DateTime.Now > MeterInfo.New_password_activation_time && MeterInfo.New_meter_password == MeterInfo.Password)
                    {
                        try
                        {
                            //if (MeterInfo.New_password_activation_time > MeterInfo.Last_Password_update_time && MeterInfo.Password != MeterInfo.Default_Password)
                            if (MeterInfo.Password != MeterInfo.Default_Password)
                            {
                                LogMessage("Setting Meter Association Password", "AP", "PW");
                                if (Param_OBJ.SET_Current_Association_Password(MeterInfo.Default_Password))
                                {
                                    LogMessage("Setting Meter Association Password Successful", "AP", "PS");
                                    //DB_Controller.Update_Password(MeterInfo.MSN,MeterInfo.New_meter_password,MeterInfo.Password);
                                    MeterInfo.IsMeterParameterized = true;

                                    //MDC Alarms
                                    MDCAlarm.MDCCombineEvents[((ushort)MDCEvents.pwd_change)] = true;
                                    MDCAlarm.IsMDCEventOuccer = true;

                                    var warning = string.Format("Parameterization Occur. MDC writes Meter Password Parameters.");
                                    DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.pwd_change), MeterInfo, SessionDateTime);

                                    //MIUF.IsPasswordUpdated = true;
                                    MIUF.IsDefaultPassWordActive = true;
                                    DB_Controller.Insert_Password_Log(MeterInfo.MSN, MeterInfo.Default_Password, MeterInfo.Password);
                                }
                                else
                                {
                                    LogMessage("Association Password Setting unsuccessful", "AP", "PF");
                                }
                            }
                            else
                            {
                                LogMessage("Password does not change password is already same", "AP", "U");
                                MIUF.IsPasswordInvalid = true;
                                MeterInfo.IsMeterParameterized = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            //throw ex;
                            LogMessage(ex, 4, "Password");
                            if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            StatisticsObj.InsertError(ex, _session_DateTime, 15);
                        }
                        finally
                        {
                            if (MIUF.IsDefaultPassWordActive)
                                ResetMaxIOFailure();
                        }
                    }
                    else if (MeterInfo.New_password_activation_time > DateTime.Now && MeterInfo.Password != MeterInfo.New_meter_password)
                    {
                        try
                        {
                            LogMessage("Setting Meter Association Password", "AP", "PW");
                            if (Param_OBJ.SET_Current_Association_Password(MeterInfo.New_meter_password))
                            {
                                LogMessage("Setting Meter Association Password Successful", "AP", "PS");
                                //DB_Controller.Update_Password(MeterInfo.MSN,MeterInfo.New_meter_password,MeterInfo.Password);
                                MeterInfo.IsMeterParameterized = true;

                                //MDC Alarms
                                MDCAlarm.MDCCombineEvents[((ushort)MDCEvents.pwd_change)] = true;
                                MDCAlarm.IsMDCEventOuccer = true;
                                var warning = string.Format("Parameterization Occur. MDC writes Meter Password Parameters.");
                                DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.pwd_change), MeterInfo, SessionDateTime);

                                //MIUF.IsPasswordUpdated = true;
                                MIUF.IsPasswordTemporary = true;
                                DB_Controller.Insert_Password_Log(MeterInfo.MSN, MeterInfo.New_meter_password, MeterInfo.Password);
                            }
                            else
                            {
                                LogMessage("Association Password Setting unsuccessful", "AP", "PF");
                            }
                        }
                        catch (Exception ex)
                        {
                            //throw ex;
                            LogMessage(ex, 4, "Password");
                            if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            StatisticsObj.InsertError(ex, _session_DateTime, 15);
                        }
                        finally
                        {
                            if (MIUF.IsPasswordTemporary)
                                ResetMaxIOFailure();
                        }
                    }
                    else if (DateTime.Now > MeterInfo.New_password_activation_time)
                    {
                        LogMessage("Password Does Not Change. Meter did not respond in valid Time", "AP", "PF");
                        MIUF.IsPasswordInvalid = true;

                    }
                }


                #endregion

                #region ///If Task Cancelled
                if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                {
                    CancelTokenSource.Token.ThrowIfCancellationRequested();
                }
                #endregion
                #region MDI Reset Date

                if (!MeterInfo.PrioritizeWakeup && MeterInfo.Write_mdi_reset_date_time && PermissionToWriteParams[(int)WriteParams.SetMDIResetDate])
                {
                    var success = true;
                    try
                    {
                        StDateTime date = new StDateTime();
                        if (MeterInfo.Mdi_reset_date_time > 0 && MeterInfo.Mdi_reset_date_time <= 28)
                        {
                            date.DayOfMonth = MeterInfo.Mdi_reset_date_time;// day of month
                            date.Hour = 00;
                            date.Minute = 00;
                            date.Second = 00;
                            LogMessage("Setting MDI Auto Reset Date", "MARD", "PW", 1);
                            if (Param_OBJ.SET_MDI_AUTO_REST_DATE_TIME(date))
                            {
                                LogMessage("Setting MDI Auto Reset Date Successful", "MARD", "PS", 1);
                                MeterInfo.IsMeterParameterized = true;

                                //MDC Alarms
                                MDCAlarm.MDCCombineEvents[((ushort)MDCEvents.mdi_date_change)] = true;
                                MDCAlarm.IsMDCEventOuccer = true;

                                var warning = string.Format("Parameterization Occur. MDC writes MDI Auto Reset Date and Time Parameters.");
                                DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.mdi_date_change), MeterInfo, SessionDateTime);
                            }
                            else
                            {
                                LogMessage("Error setting MDI Auto Reset Date", "MARD", "PF", 1);
                                success = false;

                            }
                        }
                        else
                        {
                            LogMessage("Invalid Date received from DB: " + MeterInfo.Mdi_reset_date_time, "MARDD", "F", 1);

                        }
                    }
                    catch (Exception ex)
                    {
                        success = false;
                        LogMessage(ex, 4, "MDI Reset Date");
                        if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                        StatisticsObj.InsertError(ex, _session_DateTime, 15);
                    }
                    finally
                    {
                        if (success)
                        {
                            ResetMaxIOFailure();
                            MIUF.IsMDIDateReset = true;
                        }
                    }

                }

                #endregion

                #region ///If Task Cancelled
                if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                {
                    CancelTokenSource.Token.ThrowIfCancellationRequested();
                }
                #endregion
                #region Customer Reference no

                if (!MeterInfo.PrioritizeWakeup && MeterInfo.Write_reference_no && PermissionToWriteParams[(int)WriteParams.SetCustomerReferenceNo])
                {
                    var success = true;
                    try
                    {
                        LogMessage("Setting Customer Reference Number", "CR", "PW");
                        if (Param_Controller.SET_Customer_Reference_no(MeterInfo.Reference_no))
                        {
                            LogMessage("Setting Customer Reference Number Successful", "CR", "PS");
                            MeterInfo.IsMeterParameterized = true;
                        }
                        else
                        {
                            success = false;
                            LogMessage("Error Setting Customer Reference Number", "CR", "PS");
                        }
                    }
                    catch (Exception ex)
                    {
                        // throw;
                        success = false;
                        LogMessage(ex, 4, "Customer Reference no");
                        if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                        StatisticsObj.InsertError(ex, _session_DateTime, 15);
                    }
                    finally
                    {
                        if (success)
                        {
                            ResetMaxIOFailure();
                            MIUF.IsCReferenceWrite = true;
                        }
                    }
                }

                #endregion

                #region // If Task Canceled
                if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                {
                    CancelTokenSource.Token.ThrowIfCancellationRequested();
                }
                #endregion
                #region Params WakeUp Profile
                if (!MeterInfo.PrioritizeWakeup && MeterInfo.WakeUp_Profile_Id > 0
                    && PermissionToWriteParams[(int)WriteParams.SetWakeupProfiles])
                {
                    var success = true;
                    try
                    {
                        Param_WakeUp_Profile[] obj_Wakeup;
                        if (DB_Controller.InitWakeUpProfilesParams(MeterInfo.WakeUp_Profile_Id, out obj_Wakeup))
                        {
                            LogMessage("Setting WakeUp Profile", "WP", "PW");
                            if (Param_Controller.SET_WakeUp_Profile(obj_Wakeup))
                            {
                                LogMessage("Setting WakeUp Profile Successful", "WP", "PS");
                                MeterInfo.IsMeterParameterized = true;
                            }
                            else
                            {
                                success = false;
                                LogMessage("Error Setting WakeUp Profile", "WP", "PF");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //throw;
                        success = false;
                        LogMessage(ex, 4, "Parameters WakeUp Profile");
                        if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                        StatisticsObj.InsertError(ex, _session_DateTime, 15);
                    }
                    finally
                    {
                        if (success)
                        {
                            ResetMaxIOFailure();
                            MIUF.IsWakeUpProfileWrite = true;
                        }
                    }

                }

                #endregion

                #region ///If Task Cancelled
                if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                {
                    CancelTokenSource.Token.ThrowIfCancellationRequested();
                }
                #endregion
                #region Params Number Profile

                if (!MeterInfo.PrioritizeWakeup && MeterInfo.Write_Number_Profile > 0 && PermissionToWriteParams[(int)WriteParams.SetNumberProfile])
                {
                    var success = true;
                    try
                    {
                        Param_Standard_Number_Profile[] obj_num;
                        if (DB_Controller.InitNumberProfilesParams(MeterInfo.StandardParameter, MeterInfo.Write_Number_Profile, out obj_num))
                        {
                            Param_Number_Profile[] localNumberProfileObj = null;
                            if (!MeterInfo.StandardParameter)
                            {
                                localNumberProfileObj = new Param_Number_Profile[obj_num.Length];
                                for (int i = 0; i < localNumberProfileObj.Length; i++)
                                    localNumberProfileObj[i] = (Param_Number_Profile)obj_num[i];

                            }

                            LogMessage("Setting Number Profile", "NP", "PW");
                            if ((!MeterInfo.StandardParameter && Param_Controller.SET_NumberProfiles(localNumberProfileObj)) ||
                                (MeterInfo.StandardParameter && Param_Controller.SET_Number_Profiles_AllowedCallers(obj_num) == Data_Access_Result.Success))
                            {
                                LogMessage("Setting Number Profile Successful", "NP", "PS");
                                MeterInfo.IsMeterParameterized = true;
                            }
                            else
                            {
                                LogMessage("Error Setting Number Profile", "NP", "PF");
                                success = false;

                            }
                        }
                        else
                        {
                            LogMessage("Invalid Number Profile Parameters", "NPD", "F");
                            MIUF.IsNumberProFileWrite = true;
                        }
                    }
                    catch (Exception ex)
                    {

                        success = false;
                        LogMessage(ex, 4, "Parameters Number Profile");
                        if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                        StatisticsObj.InsertError(ex, _session_DateTime, 15);
                        //throw;
                    }
                    finally
                    {
                        if (success)
                        {
                            ResetMaxIOFailure();
                            MIUF.IsNumberProFileWrite = true;
                        }
                    }
                }

                #endregion

                #region ///If Task Cancelled
                if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                {
                    CancelTokenSource.Token.ThrowIfCancellationRequested();
                }
                #endregion
                #region Display_Power_Down

                if (!MeterInfo.PrioritizeWakeup && MeterInfo.Write_Display_PowerDown_param > 0 && PermissionToWriteParams[(int)WriteParams.SetDisplayPowerDown])
                {
                    var success = true;
                    try
                    {
                        Param_Display_PowerDown obj_parms;
                        var rslt = DB_Controller.InitDisplayPowerDownParams(MeterInfo.Write_Display_PowerDown_param, out obj_parms);
                        if (rslt)
                        {
                            LogMessage("Setting Display Power Down", "DPD", "PW");
                            if (Param_Controller.SET_Display_Power_Down(obj_parms))
                            {
                                LogMessage("Setting Display Power Down Successful", "DPD", "PS");
                                MeterInfo.IsMeterParameterized = true;
                            }
                            else
                            {
                                LogMessage("Error Setting Display Power Down", "DPD", "PF");
                                success = false;
                            }
                        }
                        else { LogMessage("Error Loading Display Power Down Parameters", "DPDD", "F"); }
                    }
                    catch (Exception ex)
                    {
                        success = false;
                        LogMessage(ex, 4, "Display Power Down");
                        if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                        StatisticsObj.InsertError(ex, _session_DateTime, 15);
                        //throw;
                    }
                    finally
                    {
                        if (success)
                        {
                            ResetMaxIOFailure();
                            MIUF.IsDisplayPowerDownWrite = true;
                        }
                    }
                }

                #endregion

                #region ///If Task Cancelled
                if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                {
                    CancelTokenSource.Token.ThrowIfCancellationRequested();
                }
                #endregion
                #region Limits

                if (!MeterInfo.PrioritizeWakeup && MeterInfo.Limits_Param_Id > 0 && PermissionToWriteParams[(int)WriteParams.SetLimits])
                {
                    var success = true;
                    try
                    {
                        LogMessage("Setting Limits", "LM", "PW");
                        if (Param_OBJ.SET_Limits(MeterInfo.Limits_Param_Id))
                        {
                            LogMessage("Setting Limits Successful", "LM", "PS");
                            MeterInfo.IsMeterParameterized = true;

                            MDCAlarm.MDCCombineEvents[((ushort)MDCEvents.param_limit)] = true;
                            MDCAlarm.IsMDCEventOuccer = true;

                            var warning = string.Format("Meter Limits Changed.");
                            DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.param_limit), MeterInfo, SessionDateTime);
                        }
                        else
                        {
                            LogMessage("Error Setting Limits", "LM", "PF");
                            success = false;
                        }

                    }
                    catch (Exception ex)
                    {
                        success = false;
                        LogMessage(ex, 04, "Limits");
                        if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                        StatisticsObj.InsertError(ex, _session_DateTime, 15);
                        //throw;
                    }
                    finally
                    {
                        if (success)
                        {
                            ResetMaxIOFailure();
                            MIUF.IsLimitsWrite = true;
                        }
                    }
                }

                #endregion

                #region ///If Task Cancelled
                if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                {
                    CancelTokenSource.Token.ThrowIfCancellationRequested();
                }
                #endregion
                #region Monitoring Time

                if (!MeterInfo.PrioritizeWakeup && MeterInfo.MonitoringTime_Param_Id > 0 && PermissionToWriteParams[(int)WriteParams.SetMonitoringTime])
                {
                    var success = true;
                    try
                    {

                        LogMessage("Setting Monitoring Time", "MT", "PW");
                        if (Param_OBJ.SET_MonitoringTime(MeterInfo.MonitoringTime_Param_Id))
                        {
                            LogMessage("Setting Monitoring Time Successful", "MT", "PS");
                            MeterInfo.IsMeterParameterized = true;

                            MDCAlarm.MDCCombineEvents[((ushort)MDCEvents.Param_Monitoring_Time)] = true;
                            MDCAlarm.IsMDCEventOuccer = true;

                            var warning = string.Format("Monitoring Times Changed.");
                            DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.Param_Monitoring_Time), MeterInfo, SessionDateTime);
                        }
                        else
                        {
                            success = false;
                            LogMessage("Error Setting Monitoring Time", "MT", "PF");
                        }

                    }
                    catch (Exception ex)
                    {
                        success = false;
                        LogMessage(ex, 4, "Monitoring Time");
                        if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                        StatisticsObj.InsertError(ex, _session_DateTime, 15);
                        //throw;
                    }
                    finally
                    {
                        if (success)
                        {
                            ResetMaxIOFailure();
                            MIUF.IsMonitoringTimeWrite = true;
                        }
                    }
                }

                #endregion

                #region ///If Task Cancelled
                if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                {
                    CancelTokenSource.Token.ThrowIfCancellationRequested();
                }
                #endregion
                #region CT PT Params

                if (!MeterInfo.PrioritizeWakeup && MeterInfo.CP_PT_Param_Id > 0 && PermissionToWriteParams[(int)WriteParams.SetCTPT])
                {
                    var success = true;
                    try
                    {

                        LogMessage("Setting CT PT Parameters", "CTPT", "PW");
                        if (Param_OBJ.SET_CTPT_Ratio(MeterInfo.CP_PT_Param_Id))
                        {
                            LogMessage("Setting CT PT Parameters Successful", "CTPT", "PS");
                            MeterInfo.IsMeterParameterized = true;

                            MDCAlarm.MDCCombineEvents[((ushort)MDCEvents.param_ct_pt)] = true;
                            MDCAlarm.IsMDCEventOuccer = true;

                            var warning = string.Format("CT PT Values Changed.");
                            DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.param_ct_pt), MeterInfo, SessionDateTime);

                        }
                        else
                        {
                            LogMessage("Error Setting CT PT Parameters", "CTPT", "PF");
                            success = false;
                        }

                    }
                    catch (Exception ex)
                    {
                        success = false;
                        LogMessage(ex, 4, "CT PT Parameters");
                        if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                        StatisticsObj.InsertError(ex, _session_DateTime, 15);
                        // throw;
                    }
                    finally
                    {
                        if (success)
                        {
                            ResetMaxIOFailure();
                            MIUF.IsCTPTWrite = true;
                        }
                    }
                }

                #endregion

                #region ///If Task Cancelled
                if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                {
                    CancelTokenSource.Token.ThrowIfCancellationRequested();
                }
                #endregion
                #region Decimal Points

                if (!MeterInfo.PrioritizeWakeup && MeterInfo.DecimalPoints_Param_Id > 0 && PermissionToWriteParams[(int)WriteParams.SetDecimalPoints])
                {
                    var success = true;
                    try
                    {

                        LogMessage("Setting Decimal Points", "DP", "PW");
                        if (Param_OBJ.SET_DecimalPoints(MeterInfo.DecimalPoints_Param_Id))
                        {
                            LogMessage("Setting Decimal Points Successful", "DP", "PS");
                            MeterInfo.IsMeterParameterized = true;

                            MDCAlarm.MDCCombineEvents[((ushort)MDCEvents.Param_Decimal_Point)] = true;
                            MDCAlarm.IsMDCEventOuccer = true;

                            var warning = string.Format("Decimal Points Changed.");
                            DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.Param_Decimal_Point), MeterInfo, SessionDateTime);

                        }
                        else
                        {
                            success = false;
                            LogMessage("Error Setting Decimal Points", "DP", "PF");
                        }

                    }
                    catch (Exception ex)
                    {
                        success = false;
                        LogMessage(ex, 4, "Decimal Points");
                        if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                        StatisticsObj.InsertError(ex, _session_DateTime, 15);
                        //throw;
                    }
                    finally
                    {
                        if (success)
                        {
                            ResetMaxIOFailure();
                            MIUF.IsDecimalPointsWrite = true;
                        }
                    }
                }

                #endregion

                #region ///If Task Cancelled
                if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                {
                    CancelTokenSource.Token.ThrowIfCancellationRequested();
                }
                #endregion
                #region Energy Params

                if (!MeterInfo.PrioritizeWakeup && MeterInfo.Energy_Param_Id > 0 && PermissionToWriteParams[(int)WriteParams.SetEnergyParam])
                {
                    var success = true;
                    try
                    {

                        LogMessage("Setting Energy Parameters", "EP", "PW");
                        if (Param_OBJ.SET_EnergyParams(MeterInfo.Energy_Param_Id))
                        {
                            LogMessage("Setting Energy Parameters Successful", "EP", "PS");
                            MeterInfo.IsMeterParameterized = true;

                            MDCAlarm.MDCCombineEvents[((ushort)MDCEvents.param_energy)] = true;
                            MDCAlarm.IsMDCEventOuccer = true;

                            var warning = string.Format("Energy Parameters Changed.");
                            DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.param_energy), MeterInfo, SessionDateTime);
                        }
                        else
                        {
                            success = false;
                            LogMessage("Error Setting Energy Parameters", "EP", "PF");
                        }

                    }
                    catch (Exception ex)
                    {
                        success = false;
                        LogMessage(ex, 4, "Energy Parameters");
                        if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                        StatisticsObj.InsertError(ex, _session_DateTime, 15);
                        //throw;
                    }
                    finally
                    {
                        if (success)
                        {
                            ResetMaxIOFailure();
                            MIUF.IsEnergyParamsWrite = true;
                        }
                    }
                }

                #endregion


                #region ///If Task Cancelled
                if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                {
                    CancelTokenSource.Token.ThrowIfCancellationRequested();
                }
                #endregion
                #region SET Load Scheduling
                if (!MeterInfo.PrioritizeWakeup && MeterInfo.Load_Shedding_Schedule_Id > 0 && MeterInfo.Write_Load_Shedding_Schedule == true && PermissionToWriteParams[(int)WriteParams.SetLoadSchedule])
                {
                    var sucess = true;
                    try
                    {
                        LogMessage("Setting Load Shedding Schedule", "LS", "PW");

                        if (Param_OBJ.SET_SchedulerTable(MeterInfo.Load_Shedding_Schedule_Id))
                        {
                            LogMessage("Setting Load Shedding Schedule Successful", "LS", "PS", 1);

                            // Now Schedule Added in List
                            //Param_OBJ.ParamLoadScheduling;
                        }
                        else
                        {
                            sucess = false;
                            LogMessage("Setting Load Shedding Schedule is Unsuccessful", "LS", "PF", 1);
                            // Schedule List Not Added
                        }

                        //Param_Controller.GET_SchedulerTable //SET_SchedulerTable()

                    }
                    catch (Exception ex)
                    {
                        sucess = false;
                        //throw new Exception("Error While Setting Major Alarms " + ex.Message);
                        LogMessage(ex, 4, "Set Load Shedding Schedule");
                        if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                        StatisticsObj.InsertError(ex, _session_DateTime, 15);
                    }
                    finally
                    {
                        if (sucess)
                        {
                            ResetMaxIOFailure();
                            MIUF.IsLoadSheddingScheduleWrite = true;
                        }
                    }
                }
                #endregion

                #region Consumption Data Now/Weekly/Monthly
                if (MeterInfo.DeviceTypeVal == DeviceType.eGenious)
                {
                    #region ///If Task Cancelled
                    if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                    {
                        CancelTokenSource.Token.ThrowIfCancellationRequested();
                    }
                    #endregion
                    #region SET Consumption Data - Now
                    if (MeterInfo.Write_Consumption_Data_Now == true && PermissionToWriteParams[(int)WriteParams.SetConsumptionDataNow])
                    {
                        var sucess = true;
                        try
                        {
                            LogMessage("Setting Consumption Data Now", "CDN", "PW");

                            ConsumptionDataNow obj_Consumption_Data_Now = Param_OBJ.SET_ConsumptionDataNow(MeterInfo.ParentID);
                            LogMessage("Setting Consumption Data Now Successful", "CDN", "PS", 1);

                            DB_Controller.SaveConsumptionDataSentNow_Log(obj_Consumption_Data_Now, SessionDateTime, MeterInfo.Customer_ID, MeterInfo.MSN);
                            LogMessage("Save ConsumptionDataSentNow_Log Successful", "CDNL", "DS", 1);

                            DB_Controller.InsertInstantaneous_Live_ConsumptionData(obj_Consumption_Data_Now, SessionDateTime, MeterInfo.MeterID, MeterInfo.MSN);
                            LogMessage("Save Instantaneous_data_live_Consumption Successful", "CDNI", "DS", 1);
                        }
                        catch (Exception ex)
                        {
                            sucess = false;
                            LogMessage(ex, 4, "Set Consumption Data Now");
                            if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            StatisticsObj.InsertError(ex, _session_DateTime, 15);
                        }
                        finally
                        {
                            if (sucess)
                            {
                                ResetMaxIOFailure();
                                MIUF.IsConsumptionDataNowWrite = true;
                            }
                        }
                    }
                    #endregion

                    #region ///If Task Cancelled
                    if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                    {
                        CancelTokenSource.Token.ThrowIfCancellationRequested();
                    }
                    #endregion

                    #region ///If Task Cancelled
                    if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                    {
                        CancelTokenSource.Token.ThrowIfCancellationRequested();
                    }
                    #endregion
                    #region SET Consumption Data - Weekly
                    if (!MeterInfo.PrioritizeWakeup && MeterInfo.Write_Consumption_Data_Weekly == true && PermissionToWriteParams[(int)WriteParams.SetConsumptionDataWeekly])
                    {
                        var sucess = true;
                        try
                        {
                            LogMessage("Setting Consumption Data Weekly", "CDW", "PW");

                            ConsumptionDataWeekly obj_Consumption_Data_Weekly = Param_OBJ.SET_ConsumptionDataWeekly(MeterInfo.ParentID);
                            LogMessage("Setting Consumption Data Weekly Successful", "CDW", "PS", 1);

                            DB_Controller.SaveConsumptionDataWeekly_Log(obj_Consumption_Data_Weekly, SessionDateTime, MeterInfo.Customer_ID, MeterInfo.MSN);
                            LogMessage("Save ConsumptionDataWeekly_Log Successful", "CDWL", "DS", 1);
                        }
                        catch (Exception ex)
                        {
                            sucess = false;
                            LogMessage(ex, 4, "Set Consumption Data Weekly");
                            if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            StatisticsObj.InsertError(ex, _session_DateTime, 15);
                        }
                        finally
                        {
                            if (sucess)
                            {
                                ResetMaxIOFailure();
                                MIUF.IsConsumptionDataWeeklyWrite = true;
                            }
                        }
                    }
                    #endregion

                    #region ///If Task Cancelled
                    if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                    {
                        CancelTokenSource.Token.ThrowIfCancellationRequested();
                    }
                    #endregion
                    #region SET Consumption Data - Monthly
                    if (!MeterInfo.PrioritizeWakeup && MeterInfo.Write_Consumption_Data_Monthly == true && PermissionToWriteParams[(int)WriteParams.SetConsumptionDataMonthly])
                    {
                        var sucess = true;
                        try
                        {
                            LogMessage("Setting Consumption Data Monthly", "CDM", "PW");

                            ConsumptionDataMonthly obj_Consumption_Data_Monthly = Param_OBJ.SET_ConsumptionDataMonthly(MeterInfo.ParentID, MeterInfo.MSN);

                            try
                            {
                                foreach (var item in obj_Consumption_Data_Monthly.consumptionDataMonthlyArr)
                                {
                                    LogMessage("CDM", $"DT:{item.DateTime}  ", $"kwh:{item.Energy}, Rs:{item.Price}", 1);
                                }
                            }
                            catch (Exception)
                            {
                            }

                            LogMessage("Setting Consumption Data Monthly Successful", "CDM", "PS", 1);
                        }
                        catch (Exception ex)
                        {
                            sucess = false;
                            LogMessage(ex, 4, "Set Consumption Data Monthly");
                            if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            StatisticsObj.InsertError(ex, _session_DateTime, 15);
                        }
                        finally
                        {
                            if (sucess)
                            {
                                ResetMaxIOFailure();
                                MIUF.IsConsumptionDataWeeklyWrite = true;
                            }
                        }
                    }
                    #endregion

                }

                #endregion //  Consumption Data Now/Weekly/Monthly


                return (MeterInfo.IsMeterParameterized);
            }
            catch (Exception ex)
            {
                LogMessage(ex.InnerException, 4);
                return false;
            }
        }

        private void WriteLoadProfileChannels(uint LoadProfileGroupId, LoadProfileScheme lpScheme, ref bool ChannelRequestLP, ref bool LP_ChannelsWriteRequest)
        {
            var success = true;
            try
            {
                LogMessage(string.Format("Setting Load Profile {0} Channels", (byte)lpScheme), string.Format("CLP {0}", (byte)lpScheme), "PW");
                if (LoadProfileGroupId > 0 && Param_OBJ.SET_LoadProfileChannels(LoadProfileGroupId, lpScheme))
                {
                    LogMessage("Load Profile Channels Set successfully", "CLP", "PS", 1);
                    //Update Load Profile Counter and Load Profile Group to Zero
                    if (DB_Controller.UpdateLoadProfileDefaultSettings(MeterInfo.MSN, lpScheme))
                    {
                        LogMessage("Load Profile counter reset successful", "CLPD", "RESET", 1);
                    }
                    else
                    {
                        LogMessage("Post Load Profile channels write settings failed", "CLPD", "PF", 1);
                    }
                    //Change Parameterization Status
                    MeterInfo.IsMeterParameterized = true;
                }
                else
                {
                    LogMessage("Load Profile Channels Setting unsuccessful", "CLP", "PF", 1);
                    success = false;
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                success = false;
                LogMessage(ex, 4, "Load Profile Channels");
                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                StatisticsObj.InsertError(ex, _session_DateTime, 15);
            }
            finally
            {
                if (success)
                {
                    ResetMaxIOFailure();
                    ChannelRequestLP = false;
                    LP_ChannelsWriteRequest = true;
                }
            }
        }

        private void WriteLoadProfileInterval(TimeSpan LPPeriod, LoadProfileScheme lpScheme, ref bool ChangeIntervalReq, ref bool UpdateIntervalFlag)
        {
            var success = true;
            try
            {
                LogMessage(string.Format("Setting Load Profile {0} Interval", (byte)lpScheme), string.Format("LPI {0}", (byte)lpScheme), "PW");
                if (Param_OBJ.SET_LoadProfileInterval(LPPeriod, lpScheme))
                {
                    LogMessage("Load Profile Interval Set successfully", "LPI", "PS", 1);
                    //Change Parameterization Status
                    MeterInfo.IsMeterParameterized = true;
                }
                else
                {
                    LogMessage("Load Profile Interval Setting unsuccessful", "LPI", "PF", 1);
                    success = false;
                }

            }
            catch (Exception ex)
            {
                //throw ex;
                success = false;
                LogMessage(ex, 4, "Load profile Interval");
                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                StatisticsObj.InsertError(ex, _session_DateTime, 15);
            }
            finally
            {
                if (success)
                {
                    ResetMaxIOFailure();
                    ChangeIntervalReq = false;
                    UpdateIntervalFlag = true;
                }
            }
        }

        public bool TryReadParameters(CancellationTokenSource CancelTokenSource = null)
        {
            if (MeterInfo.ParamsToRead == null || MeterInfo.ParamsToRead.Count == 0)
                return false;
            var rsltLocal = true;
            var rslt = true;
            var prmsRead = new ParametersSet();

            for (int i = 0; i < MeterInfo.ParamsToRead.Count; i++)
            {
                #region Reading Paramter

                switch (MeterInfo.ParamsToRead[i])
                {
                    case ParamList.Modem_Status_Information:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Modem Status Information
#if Enable_Read_Param_Log
                                LogMessage("Parameters read", "MSI", "PR", 4);
#endif

                                #endregion
                                _Param_Controller.GetModemStatus(ref prmsRead.ParamModemInfo);
                                #region  Modem Status IOnformation
#if Enable_Read_Param_Log
                                LogMessage("Message", "MSI", "PS", 4);
#endif

                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.LM_OverVolt:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region LMOV
#if Enable_Read_Param_Log
                                LogMessage("Parameters read", "LOV", "PR", 4);
#endif

                                #endregion
                                _Param_Controller.GET_Limit_Over_Voltage(ref prmsRead.ParamLimits);
                                #region  LMOV
#if Enable_Read_Param_Log
                                LogMessage("Message", "LOV", "PS", 4);
#endif

                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.LM_UnderVolt:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region LMUV
#if Enable_Read_Param_Log
                                LogMessage("Message", "LUV", "PR", 4);
#endif

#if !Enable_Read_Param_Log
                                LogMessage("Message",1);
#endif
                                #endregion
                                _Param_Controller.GET_Limit_Under_Voltage(ref prmsRead.ParamLimits);
                                #region LMUV
#if Enable_Read_Param_Log
                                LogMessage("Message", "LUV", "PS", 4);
#endif

#if !Enable_Read_Param_Log
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.LM_ImbalanceVolt:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "LIBV", "PR", 4);
#endif
                                #endregion
                                _Param_Controller.GET_Limit_Imbalance_Voltage(ref prmsRead.ParamLimits);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "LIBV", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.LM_HighNeutralCurrent:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "LHNC", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_Limit_High_Neutral_Current(ref prmsRead.ParamLimits);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "LHNC", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.LM_ReverseEnergy:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "LRE", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_Limit_Reverse_Energy(ref prmsRead.ParamLimits);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "LRE", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.LM_TamperEnergy:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "LTE", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_Limit_Tamper_Energy(ref prmsRead.ParamLimits);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "LTE", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.LM_CTFailLimit:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "LCTF", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_Limit_CT_Fail_AMP(ref prmsRead.ParamLimits);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "LCTF", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.LM_PTFailLimit:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "LPTF", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_Limit_PT_Fail_AMP(ref prmsRead.ParamLimits);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "LPTF", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.LM_PTFailLimit_Volt:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "LPTFV", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_Limit_PT_Fail_Volt(ref prmsRead.ParamLimits);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "LPTFV", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.LM_OverCurrentByPhase:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "LOCP", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_Limit_Over_Current_by_Phase_T1(ref prmsRead.ParamLimits);
                                _Param_Controller.GET_Limit_Over_Current_by_Phase_T2(ref prmsRead.ParamLimits);
                                _Param_Controller.GET_Limit_Over_Current_by_Phase_T3(ref prmsRead.ParamLimits);
                                _Param_Controller.GET_Limit_Over_Current_by_Phase_T4(ref prmsRead.ParamLimits);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "LOCP", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.LM_MDIExceed:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "LMDIE", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_Limit_MDIExceedT1(ref prmsRead.ParamLimits);
                                _Param_Controller.GET_Limit_MDIExceedT2(ref prmsRead.ParamLimits);
                                _Param_Controller.GET_Limit_MDIExceedT3(ref prmsRead.ParamLimits);
                                _Param_Controller.GET_Limit_MDIExceedT4(ref prmsRead.ParamLimits);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "LMDIE", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                rsltLocal = false;
                                LogMessage(ex, DefaultExceptionLevel);
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.LM_Over_Load_Phase:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "LOLP", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_Limit_Over_Load_by_Phase_T1(ref prmsRead.ParamLimits);
                                _Param_Controller.GET_Limit_Over_Load_by_Phase_T2(ref prmsRead.ParamLimits);
                                _Param_Controller.GET_Limit_Over_Load_by_Phase_T3(ref prmsRead.ParamLimits);
                                _Param_Controller.GET_Limit_Over_Load_by_Phase_T4(ref prmsRead.ParamLimits);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "LOLP", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                rsltLocal = false;
                                LogMessage(ex, DefaultExceptionLevel);
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.LM_Over_Load_Total:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "LOLT", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_Limit_Over_Load_Total_T1(ref prmsRead.ParamLimits);
                                _Param_Controller.GET_Limit_Over_Load_Total_T2(ref prmsRead.ParamLimits);
                                _Param_Controller.GET_Limit_Over_Load_Total_T3(ref prmsRead.ParamLimits);
                                _Param_Controller.GET_Limit_Over_Load_Total_T4(ref prmsRead.ParamLimits);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "LOLT", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                rsltLocal = false;
                                LogMessage(ex, DefaultExceptionLevel);
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.MT_PowerFail:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MPWF", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_MT_Power_Fail(ref prmsRead.ParamMonitoringTime);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MPWF", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.MT_PhaseFail:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MPHF", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_MT_Phase_Fail(ref prmsRead.ParamMonitoringTime);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MPHF", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.MT_OverVolt:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MOV", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_MT_Over_Volt(ref prmsRead.ParamMonitoringTime);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MOV", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.MT_OverLoad:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MOL", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_MT_Over_Load(ref prmsRead.ParamMonitoringTime);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MOL", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.MT_HighNeutralCurrent:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MHNC", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_MT_High_Neutral_Current(ref prmsRead.ParamMonitoringTime);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MHNC", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.MT_ImbalanceVoltage:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MIBV", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_MT_Imbalance_Volt(ref prmsRead.ParamMonitoringTime);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MIBV", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.MT_OverCurrent:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MOC", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_MT_Over_Current(ref prmsRead.ParamMonitoringTime);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MOC", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.MT_ReversePolarity:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MRP", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_MT_Reverse_Polarity(ref prmsRead.ParamMonitoringTime);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MRP", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.MT_HallSensor:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MHS", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_MT_HALL_Sensor(ref prmsRead.ParamMonitoringTime);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MHS", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.MT_UnderVoltage:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MUV", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_MT_Under_Volt(ref prmsRead.ParamMonitoringTime);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MUV", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.MT_ReverseEnergy:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MRE", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_MT_Reverse_Energy(ref prmsRead.ParamMonitoringTime);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MRE", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.MT_TamperEnergy:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MTE", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_MT_Tamper_Energy(ref prmsRead.ParamMonitoringTime);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MTE", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.MT_CTFail:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MCTF", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_MT_CT_Fail(ref prmsRead.ParamMonitoringTime);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MCTF", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.MT_PTFail:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MPTF", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_MT_PT_Fail(ref prmsRead.ParamMonitoringTime);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MPTF", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                rsltLocal = false;
                                LogMessage(ex, DefaultExceptionLevel);
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.MT_PUDtoMonitor:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MPUD", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_MT_Power_Up_Delay_To_Monitor(ref prmsRead.ParamMonitoringTime);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MPUD", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.MT_PUDforEnergyRecording:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MPUDER", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_MT_Power_Up_Delay_For_Energy_Recording(ref prmsRead.ParamMonitoringTime);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MPUDER", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                rsltLocal = false;
                                LogMessage(ex, DefaultExceptionLevel);
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.MT_PhaseSequence:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MPS", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_MT_Phase_Sequence(ref prmsRead.ParamMonitoringTime);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MPS", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.ActivityCalaneder:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.WeekProfile:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.SeasonProfile:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.MDIParameters:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MDIP", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_MDI_Parameters(ref prmsRead.ParamMDI);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MDIP", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.MDIAutoResetDate:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MDIARD", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_MDI_Auto_Reset_Date(ref prmsRead.ParamMDI);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MDIARD", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.MDIIntervalTime:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MDIIT", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_MDI_Interval_Count(ref prmsRead.ParamMDI);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MDIIT", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.MDISlideCount:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MDISC", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_MDI_Slide_Count(ref prmsRead.ParamMDI);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MDISC", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.LoadProfile:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.CTPT_Ratio:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "CTPT", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_CTPT_Param(ref prmsRead.ParamCTPTRatio);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "CTPT", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.DecimalPoints:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "DP", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_Decimal_Point(ref prmsRead.ParamDecimalPoint);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "DP", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.EnergyParam:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "EP", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_EnergyParams(ref prmsRead.ParamEnergy);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "EP", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.DisplayPowerDown:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "DPD", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_Display_PowerDown(ref prmsRead.ParamDisplayPowerDown);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "DPD", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.EnableSVSFlag:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "ESVS", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_General_Process(prmsRead.ParamGeneralProcess);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "ESVS", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.IPProfile:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "IP", "PR", 4);
#endif
                                #endregion

                                if (MeterInfo.StandardParameter)
                                {
                                    _Param_Controller.GET_Standard_IP_Profiles(ref prmsRead.ParamStandardIPProfiles);
                                }
                                else
                                {

                                    _Param_Controller.GET_IP_Profiles(ref prmsRead.ParamIPProfiles);
                                }

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "IP", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.NumberProfile:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "NP", "PR", 4);
#endif
                                #endregion
                                if (MeterInfo.StandardParameter)
                                {
                                    _Param_Controller.GET_Number_Profiles_AllowedCallers(ref prmsRead.ParamStandardNumberProfile);
                                }
                                else _Param_Controller.GET_Number_Profiles(ref prmsRead.ParamNumberProfile);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "NP", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.KeepAlive:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "KA", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_Keep_Alive_IP(ref prmsRead.ParamKeepAliveIP);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "KA", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.ModemLimitsAndTime:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MLT", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_ModemLimitsAndTime(ref prmsRead.ParamModemLimitsAndTime);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MLT", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.ModemInitialize:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MI", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_ModemBasics(ref prmsRead.ParamModemInitialize);
                                _Param_Controller.GET_ModemBasicsNew(ref prmsRead.ParamModemBasicsNEW);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "MI", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.TimeBaseEvent1:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "TBE1", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_TimeBaseEvents(ref prmsRead.ParamTimeBaseEvent_1, Get_Index._Time_Based_Event_1);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "TBE1", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.TimeBaseEvent2:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "TBE2", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_TimeBaseEvents(ref prmsRead.ParamTimeBaseEvent_2, Get_Index._Time_Based_Event_2);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "TBE2", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.DisableOnPowerFailTBEs:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "DTBEPF", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_TBE_PowerFAil(ref prmsRead.ParamTBPowerFail);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "DTBEPF", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.Contactor:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "CON", "PR", 4);
#endif
                                #endregion

                                _Param_Controller.GET_ContactorParams(prmsRead.ParamContactor);

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "CON", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.Debug_Error:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "DES", "PR", 4);
#endif
                                #endregion

                                prmsRead.Debug_Error = ReadErrorString();

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "DES", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.Debug_Caution:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "DCS", "PR", 4);
#endif
                                #endregion

                                prmsRead.Debug_Cautions = ReadCautionString();

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "DCS", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    case ParamList.Debug_Contactor_Status:
                        {
                            #region ///If Task Cancelled
                            if (CancelTokenSource != null && _threadCancelToken.IsCancellationRequested)
                            {
                                CancelTokenSource.Token.ThrowIfCancellationRequested();
                            }
                            #endregion
                            #region Read
                            try
                            {

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "DCNS", "PR", 4);
#endif
                                #endregion

                                prmsRead.Debug_Contactor_Status = ReadDebugContactorStatus();

                                #region Read paramters Log
#if Enable_Read_Param_Log
                                LogMessage("Read Parameters", "DCNS", "PS", 4);
#endif
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                LogMessage(ex, DefaultExceptionLevel);
                                rsltLocal = false;
                                if (!Commons.IsTCP_Connected(ex) || !this._AP_Controller.IsConnected || IsMaxIOFailureOccure()) throw ex;
                            }
                            #endregion
                        }
                        break;
                    default:
                        break;
                }

                #endregion

                if (rsltLocal)
                    ResetMaxIOFailure();

                rslt = rslt & rsltLocal;
                rsltLocal = true;
            }
            var data = new MeterReliablityParams();
            data.InitParameterSet(prmsRead, MeterInfo.StandardParameter);

            LogMessage("Read Parameters Save", "RPMRD", "R", 4);
            if (DB_Controller.SaveReadParams(MeterInfo.MSN, SessionDateTime, data) && rsltLocal)
            {
                LogMessage("Read Parameters Save", "RPMRD", "S", 4);
                return true;
            }
            LogMessage("Read Parameters Save", "RPMRD", "F", 4);
            return false;
        }

        public bool Process_Security_MDCAlarms(Exception ExInfo)
        {
            bool isAlarmProcess = false;
            try
            {
                /// Process MDC Error Codes Here
                string ErrorCodes = Commons.GET_ErrorCodes(ExInfo);

                if (ErrorCodes.Contains(String.Format("(Error Code:{0})", (int)DLMSErrors.Invalid_AuthenticationTAG)))
                {
                    MDCAlarm.MDCCombineEvents[((ushort)MDCEvents.Invalid_AuthenticationTAG)] = true;
                    MDCAlarm.IsMDCEventOuccer = true;
                    isAlarmProcess = true;

                    var warning = string.Format("Message Authentication TAG Mismatch Error");

                    DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.Invalid_AuthenticationTAG), MeterInfo, SessionDateTime);
                }
                else if (ErrorCodes.Contains(String.Format("(Error Code:{0})", (int)DLMSErrors.Invalid_SecurityData_AK)) ||
                         ErrorCodes.Contains(String.Format("(Error Code:{0})", (int)DLMSErrors.Invalid_SecurityData_EK)))
                {
                    MDCAlarm.MDCCombineEvents[((ushort)MDCEvents.Invalid_SecurityData)] = true;
                    MDCAlarm.IsMDCEventOuccer = true;
                    isAlarmProcess = true;

                    var warning = string.Format("Invalid Encryption/Authentication Key Error MDC trying Update Meter Security Data");

                    DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.Invalid_SecurityData), MeterInfo, SessionDateTime);
                }
                else if (ErrorCodes.Contains(String.Format("(Error Code:{0})", (int)DLMSErrors.Invalid_SecurityData_SystemTitle)) ||
                         ErrorCodes.Contains(String.Format("(Error Code:{0})", (int)DLMSErrors.Invalid_SecurityHeader)))
                {
                    MDCAlarm.MDCCombineEvents[((ushort)MDCEvents.Invalid_SecurityData)] = true;
                    MDCAlarm.IsMDCEventOuccer = true;
                    isAlarmProcess = true;

                    var warning = string.Format("Invalid Encryption/Authentication Key Error MDC trying Update Meter Security Data");

                    DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.Invalid_SecurityData), MeterInfo, SessionDateTime);
                }
                else if (ErrorCodes.Contains(String.Format("(Error Code:{0})", (int)DLMSErrors.Invalid_KeyLengthSuport)) ||
                         ErrorCodes.Contains(String.Format("(Error Code:{0})", (int)DLMSErrors.Invalid_KeyWrap)) ||
                         ErrorCodes.Contains(String.Format("(Error Code:{0})", (int)DLMSErrors.Invalid_KeyUnWrap)))
                {
                    MDCAlarm.MDCCombineEvents[((ushort)MDCEvents.Invalid_SecurityData)] = true;
                    MDCAlarm.IsMDCEventOuccer = true;
                    isAlarmProcess = true;

                    var warning = string.Format("Invalid Encryption/Authentication Key Error MDC trying Update Meter Security Data");

                    DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.Invalid_SecurityData), MeterInfo, SessionDateTime);
                }

                return isAlarmProcess;
            }
            catch
            {
                return false;
            }
        }

        public bool Process_Security_MDCAlarms(string ExInfo)
        {
            bool isAlarmProcess = false;
            try
            {
                /// Process MDC Error Codes Here

                string ErrorCodes = Commons.GET_ErrorCodes(ExInfo);

                if (ErrorCodes.Contains(String.Format("(Error Code:{0})", (int)DLMSErrors.Invalid_AuthenticationTAG)))
                {
                    MDCAlarm.MDCCombineEvents[((ushort)MDCEvents.Invalid_AuthenticationTAG)] = true;
                    MDCAlarm.IsMDCEventOuccer = true;
                    isAlarmProcess = true;

                    var warning = string.Format("Message Authentication TAG Mismatch Error");

                    DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.Invalid_AuthenticationTAG), MeterInfo, SessionDateTime);
                }
                else if (ErrorCodes.Contains(String.Format("(Error Code:{0})", (int)DLMSErrors.Invalid_SecurityData_AK)) ||
                         ErrorCodes.Contains(String.Format("(Error Code:{0})", (int)DLMSErrors.Invalid_SecurityData_EK)))
                {
                    MDCAlarm.MDCCombineEvents[((ushort)MDCEvents.Invalid_SecurityData)] = true;
                    MDCAlarm.IsMDCEventOuccer = true;
                    isAlarmProcess = true;

                    var warning = string.Format("Invalid Encryption/Authentication Key Error MDC trying Update Meter Security Data");

                    DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.Invalid_SecurityData), MeterInfo, SessionDateTime);
                }
                else if (ErrorCodes.Contains(String.Format("(Error Code:{0})", (int)DLMSErrors.Invalid_SecurityData_SystemTitle)) ||
                         ErrorCodes.Contains(String.Format("(Error Code:{0})", (int)DLMSErrors.Invalid_SecurityHeader)))
                {
                    MDCAlarm.MDCCombineEvents[((ushort)MDCEvents.Invalid_SecurityData)] = true;
                    MDCAlarm.IsMDCEventOuccer = true;
                    isAlarmProcess = true;

                    var warning = string.Format("Invalid Encryption/Authentication Key Error MDC trying Update Meter Security Data");

                    DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.Invalid_SecurityData), MeterInfo, SessionDateTime);
                }
                else if (ErrorCodes.Contains(String.Format("(Error Code:{0})", (int)DLMSErrors.Invalid_KeyLengthSuport)) ||
                         ErrorCodes.Contains(String.Format("(Error Code:{0})", (int)DLMSErrors.Invalid_KeyWrap)) ||
                         ErrorCodes.Contains(String.Format("(Error Code:{0})", (int)DLMSErrors.Invalid_KeyUnWrap)))
                {
                    MDCAlarm.MDCCombineEvents[((ushort)MDCEvents.Invalid_SecurityData)] = true;
                    MDCAlarm.IsMDCEventOuccer = true;
                    isAlarmProcess = true;

                    var warning = string.Format("Invalid Encryption/Authentication Key Error MDC trying Update Meter Security Data");

                    DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.Invalid_SecurityData), MeterInfo, SessionDateTime);
                }

                return isAlarmProcess;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Support Functions

        public void PopulateOBISCodesMap(List<KeyValuePair<StOBISCode, StOBISCode>> OBISCodes)
        {
            try
            {
                // PopulateSAPTable(OBISCodes, Application_Process.CurrentMeterSAP, Application_Process.CurrentClientSAP);

                if (Applicationprocess_Controller.ApplicationProcessSAPTable == null)
                    Applicationprocess_Controller.ApplicationProcessSAPTable = new SAPTable();

                this.Applicationprocess_Controller.ApplicationProcessSAPTable.OBISLabelLookup.Clear();
                this.Applicationprocess_Controller.ApplicationProcessSAPTable.AddRangeOBISCode(OBISCodes);
            }
            catch (Exception ex)
            {
                if (ex is DLMSException)
                    throw ex;
                else
                    throw new DLMSException("Unable to populate ApplicationProcess SAP table", ex);
            }
        }

        public void PopulateSAPTable(List<OBISCodeRights> OBISCodes)
        {
            try
            {
                // PopulateSAPTable(OBISCodes, Application_Process.CurrentMeterSAP, Application_Process.CurrentClientSAP);

                if (Applicationprocess_Controller.ApplicationProcessSAPTable == null)
                    Applicationprocess_Controller.ApplicationProcessSAPTable = new SAPTable();

                this.Applicationprocess_Controller.ApplicationProcessSAPTable.SapTable.Clear();
                this.Applicationprocess_Controller.ApplicationProcessSAPTable.AddRangeOBISRights(OBISCodes);
            }
            catch (Exception ex)
            {
                if (ex is DLMSException)
                    throw ex;
                else
                    throw new DLMSException("Unable to populate ApplicationProcess SAP table", ex);
            }
        }

        public bool ApplyContactorRequest(bool State)
        {
            try
            {
                string contactorAck = string.Empty;

                //MeterInfo.prepaid_request_exist = true;
                contactorAck = DateTime.Now.ToString(DateFormat) + " Meter Connected";
                DB_Controller.updateContactorStatus(MeterInfo.MSN, contactorAck);
                //contactorAck = "Prepaid Server reading Contactor Request from database";
                contactorAck += "\r\n\r\n" + DateTime.Now.ToString(DateFormat) + " Metering Server Receiving Command";
                DB_Controller.updateContactorStatus(MeterInfo.MSN, contactorAck);
                contactorAck += "\r\n\r\n" + DateTime.Now.ToString(DateFormat) + " Metering Server Command Received for meter:  " + ((State) ? "ON" : "OFF");
                DB_Controller.updateContactorStatus(MeterInfo.MSN, contactorAck);
                contactorAck += "\r\n\r\n" + DateTime.Now.ToString(DateFormat) + " Metering Server reading current meter status";
                DB_Controller.updateContactorStatus(MeterInfo.MSN, contactorAck);
                var currentRelayStatus = !State;
                string onoff = string.Empty;
                if (!Settings.Default.ByPassContactorStateChecks)
                {
                    currentRelayStatus = Param_Controller.GET_Relay_Status();
                    onoff = (currentRelayStatus) ? "ON" : "OFF";
                    LogMessage(String.Format("Current Contactor Status =  {0}", currentRelayStatus), "CONS", "P,ST " + onoff, 4);
                    contactorAck += "\r\n\r\n" + DateTime.Now.ToString(DateFormat) + " Meter status: " + onoff;
                    DB_Controller.updateContactorStatus(MeterInfo.MSN, contactorAck);
                }



                if (State && !currentRelayStatus) //&& MeterInfo.Current_contactor_status == 0) //if (contactReq.Equals("Meter ON") && !currentRelayStatus)
                {
                    if (Settings.Default.GrantContactorOnPermission)
                    {
                        //TURN ON CONTACTOR
                        contactorAck += "\r\n\r\n" + DateTime.Now.ToString(DateFormat) + " Metering Server sending Command to Turn on meter";
                        DB_Controller.updateContactorStatus(MeterInfo.MSN, contactorAck);


                        Param_Controller.RelayConnectRequest();

                        contactorAck += "\r\n\r\n" + DateTime.Now.ToString(DateFormat) + " Metering Server Waiting for Meter acknowledgement";
                        DB_Controller.updateContactorStatus(MeterInfo.MSN, contactorAck);
                        if (Settings.Default.ContactorStatusReadDelay > 0)
                        {
                            Thread.Sleep(Settings.Default.ContactorStatusReadDelay * 1000);
                            currentRelayStatus = Param_Controller.GET_Relay_Status();
                        }
                        else currentRelayStatus = true;
                        onoff = (currentRelayStatus) ? "ON" : "OFF";

                        if (currentRelayStatus)
                        {
                            ContactorStateChanged(currentRelayStatus, ref contactorAck);
                            //MeterInfo.Schedule_CO.LastReadTime = DateTime.Now;
                            //contactorAck += "\r\n\r\n" + DateTime.Now.ToString(DateFormat) + " Meter ON";
                            //DB_Controller.updateContactorStatus(MeterInfo.MSN,  contactorAck);
                            ////contactorAck += "1";
                            ////DB_Controller.updateContactorStatus(MeterInfo.MSN, MeterInfo.Reference_no, contactorAck);
                            //DB_Controller.updateFINALContactorStatus(MeterInfo.MSN,  contactorAck, "1");
                            //LogMessage("Contactor turned ON", "CONS", "PS, ON");
                            //MeterInfo.Current_contactor_status = (currentRelayStatus) ? 1 : 0;

                            //MDCAlarm.MDCCombineEvents[((ushort)MDCEvents.contactor_status_on)] = true;
                            //MDCAlarm.IsMDCEventOuccer = true;

                            //var warning = string.Format("MDC Turned Contactor ON.");
                            //DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.contactor_status_on), MeterInfo, SessionDateTime);
                            return true;
                        }
                        else
                        {
                            contactorAck += "\r\n\r\n" + DateTime.Now.ToString(DateFormat) + " Meter " + onoff + " ";
                            DB_Controller.updateContactorStatus(MeterInfo.MSN, contactorAck);
                            //contactorAck += "1";
                            //DB_Controller.updateContactorStatus(MeterInfo.MSN, MeterInfo.Reference_no, contactorAck);
                            DB_Controller.updateFINALContactorStatus(MeterInfo.MSN, contactorAck, "-1");
                            LogMessage("Contactor ON Request Failed!", "CONS", "PF, ON");
                            MeterInfo.Current_contactor_status = (currentRelayStatus) ? 1 : 0;
                            return false;
                        }
                    }
                    else
                    {
                        LogMessage("Contactor ON Access Denied!", "COAD", "PF, ON");
                        return false;
                    }
                }
                else if (!State && currentRelayStatus)//&& MeterInfo.Current_contactor_status == 1)//else if (contactReq.Equals("Meter OFF") && currentRelayStatus)
                {
                    if (Settings.Default.GrantContactorOFFPermission)
                    {
                        //TURN OFF CONTACTOR
                        contactorAck += "\r\n\r\n" + DateTime.Now.ToString(DateFormat) + " Metering Server sending Command to Turn off meter";

                        DB_Controller.updateContactorStatus(MeterInfo.MSN, contactorAck);


                        Param_Controller.RelayDisConnectRequest();

                        contactorAck += "\r\n\r\n" + DateTime.Now.ToString(DateFormat) + " Metering Server Waiting for Meter acknowledgement";

                        DB_Controller.updateContactorStatus(MeterInfo.MSN, contactorAck);
                        if (Settings.Default.ContactorStatusReadDelay > 0)
                        {
                            Thread.Sleep(Settings.Default.ContactorStatusReadDelay * 1000);
                            currentRelayStatus = Param_Controller.GET_Relay_Status();
                        }
                        else currentRelayStatus = false;
                        onoff = (currentRelayStatus) ? "ON" : "OFF";

                        if (!currentRelayStatus)
                        {
                            ContactorStateChanged(currentRelayStatus, ref contactorAck);
                            return true;
                        }
                        else
                        {
                            LogMessage("Contactor Turned OFF Request Failed!", "CONS", "PF, OFF");
                            contactorAck += "\r\n\r\n" + DateTime.Now.ToString(DateFormat) + " Meter " + onoff + " ";
                            DB_Controller.updateContactorStatus(MeterInfo.MSN, contactorAck);
                            DB_Controller.updateFINALContactorStatus(MeterInfo.MSN, contactorAck, "-1");
                            MeterInfo.Current_contactor_status = (currentRelayStatus) ? 1 : 0;

                            return false;
                        }
                    }
                    else
                    {
                        LogMessage("Contactor OFF Access Denied!", "COAD", "PF, OFF");
                        return false;
                    }
                }
                else
                {
                    string temp = (currentRelayStatus) ? "ON" : "Off";
                    //Delete entry from contactor_control_data table
                    LogMessage("Meter Contactor state is Already " + temp, "CONS", "PU, " + temp);
                    //contactorAck += "1";
                    //DB_Controller.updateContactorStatus(MeterInfo.MSN, MeterInfo.Reference_no, contactorAck);
                    string i = (currentRelayStatus) ? "1" : "0";
                    DB_Controller.updateFINALContactorStatus(MeterInfo.MSN, contactorAck, i);

                    MeterInfo.Current_contactor_status = (currentRelayStatus) ? 1 : 0;

                    //Modification 20-11-2014 version 3.0.0.143
                    //DB_Controller.deleteContactorRequestEntry(MeterInfo.MSN);
                    return true;

                }


            }
            catch (Exception)
            {

                throw;
            }

        }
        private void ContactorStateChanged(bool currentRelayStatus, ref string contactorAck)
        {

            MeterInfo.Schedule_CO.LastReadTime = DateTime.Now;
            MeterInfo.Current_contactor_status = (currentRelayStatus) ? 1 : 0;
            MDCEvents mdcEventTriggered = (currentRelayStatus) ? MDCEvents.contactor_status_on : MDCEvents.contactor_status_off;
            string relayStatus = (currentRelayStatus) ? "ON" : "OFF";
            LogMessage(string.Format("Contactor Turned {0}", relayStatus), "CONS", string.Format("PS, {0}", relayStatus));
            contactorAck += "\r\n\r\n" + DateTime.Now.ToString(DateFormat) + " Meter " + relayStatus;
            DB_Controller.updateContactorStatus(MeterInfo.MSN, contactorAck);
            //contactorAck += "1";
            //DB_Controller.updateContactorStatus(MeterInfo.MSN, MeterInfo.Reference_no, contactorAck);
            DB_Controller.updateFINALContactorStatus(MeterInfo.MSN, contactorAck, MeterInfo.Current_contactor_status.ToString());

            MDCAlarm.MDCCombineEvents[((ushort)mdcEventTriggered)] = true;
            MDCAlarm.IsMDCEventOuccer = true;

            var warning = string.Format("MDC Turned Contactor {0}.", relayStatus);
            DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)mdcEventTriggered), MeterInfo, SessionDateTime);
        }

        public void LogoutMeterConnection()
        {
            try
            {
                if (ConnectToMeter.CurrentConnection == PhysicalConnectionType.KeepAlive &&
                    MeterInfo != null)
                {
                    if (MeterInfo.logoutMeter)
                    {
                        try
                        {
                            #region Logging Out Meter
#if Enable_Abstract_Log
                            LogMessage("Logging Out Meter", "PVLO", "R");
#endif
#if !Enable_Abstract_Log
						LogMessage("Logging Out Meter");
#endif
                            #endregion
                            ConnectionController.Disconnect();
                            #region Logging Out Successful
#if Enable_Abstract_Log
                            LogMessage("Logging Out Successful", "PVLO", "S", 1);
#endif
#if !Enable_Abstract_Log
						LogMessage("Logging Out Successful", 1);
#endif
                            #endregion
                        }
                        finally
                        {
                            AssociationState_Obj = AssociationState.Logout;
                        }
                    }
                    else
                        if (AssociationState_Obj == AssociationState.Login &&
                            MeterInfo.Kas_DueTime.Subtract(DateTime.Now) > Settings.Default.AssociationTimeout)
                    {
                        try
                        {
                            #region Logging Out Meter
#if Enable_Abstract_Log
                            LogMessage("Logging Out Meter", "PVLO", "R");
#endif

#if !Enable_Abstract_Log
						LogMessage("Logging Out Meter");
#endif
                            #endregion
                            ConnectionController.Disconnect();
                            #region Logging Out Successful
#if Enable_Abstract_Log
                            LogMessage("Logging Out Successful", "PVLO", "S", 1);
#endif

#if !Enable_Abstract_Log
						    LogMessage("Logging Out Successful", 1);
#endif
                            #endregion
                        }
                        finally
                        {
                            AssociationState_Obj = AssociationState.Logout;
                        }
                    }
                    // MeterInfo.MustLogoutMeter = false;
                }
                else
                {
                    try
                    {
                        #region Logging Out Meter
#if Enable_Abstract_Log
                        LogMessage("Logging Out Meter", "PVLO", "R", 1);
#endif
#if !Enable_Abstract_Log
						 LogMessage("Logging Out Meter",1);
#endif
                        #endregion
                        ConnectionController.Disconnect();
                        #region Logging Out Successful
#if Enable_Abstract_Log
                        LogMessage("Logging Out Successful", "PVLO", "S", 1);
#endif

#if !Enable_Abstract_Log
						LogMessage("Logging Out Successful", 1);
#endif
                        #endregion
                    }
                    finally
                    {
                        AssociationState_Obj = AssociationState.Logout;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    if (ConnectToMeter != null)
                        if ((ConnectToMeter.CurrentConnection == PhysicalConnectionType.NonKeepAlive) ||
                            (ConnectToMeter.CurrentConnection == PhysicalConnectionType.KeepAlive &&
                             AssociationState_Obj == AssociationState.Logout))
                        {
                            // Disconnect Overlay Stream
                            if (ConnectToMeter.IsOverlayStreamEnable)
                                ConnectToMeter.Disconnect();
                            ConnectToMeter.OverlayStream = null;

                            // Disable Overlay Stream Mode
                            if (ConnectToMeter.IOStream is TCPStream)
                                (ConnectToMeter.IOStream as TCPStream).OverlayMode = false;
                        }
                }
                // Don't Raise Error
                catch
                {
                }
            }
        }

        public bool LoginMeterConnection(HLS_Mechanism authenticationType = HLS_Mechanism.LowSec, LLCProtocolType ProtocolType = LLCProtocolType.Not_Assigned)
        {
            string[] PrivateLogging_Request = new string[] { "Trying Private Login", "PVLI" };
            string[] PrivateLogging_Response = new string[] { "Private Login Successful", "PVLI" };

            string[] PubilcLogging_Request = new string[] { "Trying Public Login", "PBLI" };
            string[] PublicLogging_Response = new string[] { "Public Login Successful", "PBLI" };

            string[] HLS_Request = new string[] { "Trying HLS Login", "HLS" };
            string[] HLS_Response = new string[] { "HLS Login Successful", "HLSI" };

            DeviceAssociation Association_Details = null;

            try
            {
                // For KA Meter
                // Only Authenticate When AssociationState.Logout
                if (ConnectToMeter.CurrentConnection == PhysicalConnectionType.KeepAlive &&
                    AssociationState_Obj == AssociationState.Login)
                {
                    return true;
                }

                if (connectToMeter.ConnectionInfo.IsInitialized)
                {
                    Association_Details = connectToMeter.ConnectionInfo.MeterInfo.Device_Association;
                }

                ConnectionController.ProtocolType = ProtocolType;

                #region Trying Private Login
#if Enable_Abstract_Log

                if (authenticationType == HLS_Mechanism.LowSec)
                    LogMessage(PrivateLogging_Request[0], PrivateLogging_Request[1], "R");
                else if (authenticationType == HLS_Mechanism.LowestSec)
                    LogMessage(PubilcLogging_Request[0], PubilcLogging_Request[1], "R");
                else if (authenticationType >= HLS_Mechanism.HLS_Manufac)
                    LogMessage(HLS_Request[0], HLS_Request[1], "R");

#endif
#if !Enable_Abstract_Log
						LogMessage("Trying Private Login",1);
#endif
                #endregion

                // HDLC Protocol Connect
                if (ProtocolType == LLCProtocolType.Direct_HDLC ||
                    ProtocolType == LLCProtocolType.PG_BOTH)
                {
                    bool hdlcConnected = false;
                    // HDLC Initial Parameter
                    ConnectionController.InitHDLC_Params = HDLCInitParameters;
                    // Update HDLC Device Address
                    if (MeterInfo != null && MeterInfo.HDLC_Address > 0)
                    {
                        ConnectionController.InitHDLC_Params.DeviceAddress = MeterInfo.HDLC_Address;// 0x0011;
                    }


                    try
                    {
                        // Enable Overlay Stream Mode
                        if (ConnectToMeter.IOStream is TCPStream)
                            (ConnectToMeter.IOStream as TCPStream).OverlayMode = true;

                        ushort meterSAP = ConnectionController.ManagementDevice.SAP_Address;
                        ushort clientSAP = ConnectionController.Public.SAP_Address;

                        if (Association_Details != null && Association_Details.DeviceId > 0)
                        {
                            meterSAP = Association_Details.MeterSap;
                            clientSAP = Association_Details.ClientSap;
                        }


                        hdlcConnected = ConnectionController.ConnectDirectHDLC(ConnectToMeter.IOStreamLocal,
                                                                               meterSAP, clientSAP,
                                                                               ConnectionController.InitHDLC_Params);
                        if (hdlcConnected)
                            ConnectionController.HDLCConnection.HDLCDisconnected += new Action(ConnectToMeter.HdlcProtocol_HDLCDisconnected);
                    }
                    finally
                    {
                        // Initial Overlay HDLC Stream
                        if (hdlcConnected)
                        {
                            // Set Overlay Stream
                            ConnectToMeter.OverlayStream = ConnectionController.HDLCConnection.BaseStream;

                            LogMessage("Direct HDLC Connected", "HDLC", "S", 1);
                        }
                        else
                        {
                            ConnectToMeter.OverlayStream = null;
                            if (ConnectionController.HDLCConnection != null)
                                ConnectionController.HDLCConnection.ResetHDLC();

                            // Disable Overlay Stream Mode
                            if (ConnectToMeter.IOStream is TCPStream)
                                (ConnectToMeter.IOStream as TCPStream).OverlayMode = false;

                            LogMessage("Direct HDLC Connection Failure", "HDLC", "F", 1);
                        }
                    }

                    // Raise HDLC Error
                    if (!hdlcConnected)
                    {
                        return false;
                    }
                }
                else
                {
                    ConnectToMeter.OverlayStream = null;
                    // Enable Overlay Stream Mode
                    if (ConnectToMeter.IOStream is TCPStream)
                        (ConnectToMeter.IOStream as TCPStream).OverlayMode = false;
                }


                if (authenticationType == HLS_Mechanism.LowestSec)
                    ConnectionController.PublicLogin(ConnectToMeter);
                else if (authenticationType == HLS_Mechanism.LowSec)
                {
                    if (Association_Details == null || (Association_Details.Id <= 0 ||
                                                           Association_Details.Id > 150))
                        throw new ArgumentNullException("Invalid Association Detail Id");

                    ConnectionController.PrivateLogin(ConnectToMeter, MeterInfo.Password);
                }
                else if (authenticationType >= HLS_Mechanism.HLS_Manufac)
                {
                    if (Association_Details == null || (Association_Details.Id <= 0 ||
                                                        Association_Details.Id > 150))
                        throw new ArgumentNullException("Invalid Association Detail Id");

                    // Update Security Data
                    AuthenticateMeterConnection_HLS(Association_Details);
                }

                #region Private Login Successful
#if Enable_Abstract_Log

                if (authenticationType == HLS_Mechanism.LowSec)
                    LogMessage(PrivateLogging_Response[0], PrivateLogging_Response[1], "S", 1);
                else if (authenticationType == HLS_Mechanism.LowestSec)
                    LogMessage(PublicLogging_Response[0], PublicLogging_Response[1], "S", 1);

#endif

#if !Enable_Abstract_Log
						LogMessage("Private Login Successful", 1);
#endif
                #endregion
                AssociationState_Obj = AssociationState.Login;

                #region Access Rights if Debug Mode enabled

                SAPTable sapTb = null;

#if Enable_DEBUG_RunMode

                try
                {
                    sapTb = GetAccessRightsDelegate.Invoke(ConnectToMeter);
                    Applicationprocess_Controller.ApplicationProcessSAPTable = sapTb;
                }
                catch { }

                if ((sapTb != null && sapTb.SapTable.Count > 0) || authenticationType == HLS_Mechanism.LowestSec)
                    return true;

                LogMessage("Reading current application data access rights", 0);
                List<OBISCodeRights> AccessRights = ConnectionController.ReadMeterAccessRights(ConnectToMeter);
                LogMessage("Reading completes,Saving application data access rights", 0);
                Configurator.SaveOBISCodeRights(ConnectToMeter.ConnectionInfo, AccessRights);

#endif
                #endregion

                return (_AP_Controller != null && _AP_Controller.IsConnected);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Local_Remote_Error:A1,Failure Type:0D"))
                {
                    MDCAlarm.MDCCombineEvents[((ushort)MDCEvents.pwd_error)] = true;
                    MDCAlarm.IsMDCEventOuccer = true;

                    var warning = string.Format("Password Error Received MDC trying Login Meter With Wrong Password");
                    DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.pwd_error), MeterInfo, SessionDateTime);
                }

                throw ex;
            }
            finally
            {
                #region HDLC Resource Clearance

                try
                {
                    if (ProtocolType == LLCProtocolType.Direct_HDLC ||
                                ProtocolType == LLCProtocolType.PG_BOTH)
                    {
                        // Association Unsuccessful
                        if (this.Applicationprocess_Controller != null &&
                            !this.Applicationprocess_Controller.IsConnected)
                        {
                            // Disconnect Direct HDLC
                            if (ConnectionController.HDLCConnection.Connected)
                            {
                                ConnectionController.DisConnectHdlc();
                            }

                            // Release HDLC Resource
                            ConnectToMeter.OverlayStream = null;
                            if (ConnectionController.HDLCConnection != null)
                                ConnectionController.HDLCConnection.ResetHDLC();

                            // Disable Overlay Stream Mode
                            if (ConnectToMeter.IOStream is TCPStream)
                                (ConnectToMeter.IOStream as TCPStream).OverlayMode = false;
                            ConnectToMeter.OverlayStream = null;
                        }
                    }
                }
                catch
                {
                    // No Error
                }

                #endregion
            }
        }

        public bool AuthenticateMeterConnection_HLS(DeviceAssociation AssociationDetail = null)
        {
            try
            {
                if (ConnectToMeter.CurrentConnection == PhysicalConnectionType.KeepAlive)
                    if (AssociationState_Obj == AssociationState.Login)
                    {
                        if (ServerSystemTitle != null && ServerSystemTitle.Count != 0)
                            this.Applicationprocess_Controller.Security_Data.ServerSystemTitle = ServerSystemTitle;
                        return true;
                    }

                LogMessage("GMAC Authentication", "GMAC", "R", 0);

                // Update Connection Controller
                ConnectionController.CurrentConnection = connectToMeter;
                Applicationprocess_Controller.GetCommunicationObject = connectToMeter;
                ConnectionController.HLS_Authentication(this.Applicationprocess_Controller.Security_Data, AssociationDetail);
                LogMessage("GMAC Authentication Successful", "GMAC", "S", 1);
                ServerSystemTitle = this.Applicationprocess_Controller.Security_Data.ServerSystemTitle;
                AssociationState_Obj = AssociationState.Login;

                #region Access Rights if Debug Mode enabled

                SAPTable sapTb = null;

#if Enable_DEBUG_RunMode

                try
                {
                    sapTb = GetAccessRightsDelegate.Invoke(ConnectToMeter);
                    Applicationprocess_Controller.ApplicationProcessSAPTable = sapTb;
                }
                catch { }

                if (sapTb != null && sapTb.SapTable.Count > 0)
                    return true;

                LogMessage("Reading current application data access rights", 0);

                List<OBISCodeRights> AccessRights = ConnectionController.ReadMeterAccessRights(ConnectToMeter);
                // var rslt = ConnectionController.ReadMeterAccessRightsAsync(ConnectToMeter);
                // rslt.Wait();
                // List<OBISCodeRights> AccessRights = rslt.Result;

                LogMessage("Reading completes,Saving application data access rights", 0);
                Configurator.SaveOBISCodeRights(ConnectToMeter.ConnectionInfo, AccessRights);

#endif

                #endregion
                return true;
            }
            catch (Exception ex)
            {
                LogMessage("GMAC Authentication Unsuccessful", "GMAC", "F", 1);

                Process_Security_MDCAlarms(ex);

                if (ex.Message.Contains(String.Format("(Error Code:{0})", (int)DLMSErrors.Password_Error)))
                {
                    MDCAlarm.MDCCombineEvents[((ushort)MDCEvents.pwd_error)] = true;
                    MDCAlarm.IsMDCEventOuccer = true;

                    var warning = string.Format("Password Error Received MDC trying Login Meter With Wrong Password");
                    DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.pwd_error), MeterInfo, SessionDateTime);
                }
                else if (ex.Message.Contains(String.Format("(Error Code:{0})", (int)DLMSErrors.Invalid_AuthenticationTAG)))
                {
                    MDCAlarm.MDCCombineEvents[((ushort)MDCEvents.pwd_error)] = true;
                    MDCAlarm.IsMDCEventOuccer = true;

                    var warning = string.Format("Message Authentication TAG Mismatch Error");
                    DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.Invalid_AuthenticationTAG), MeterInfo, SessionDateTime);
                }

                throw ex;
            }
        }

        public SAPTable GetAccessRights(IOConnection ConnectionInfo)
        {
            try
            {
                SAPTable tb = null;
                tb = Configurator.GetAccessRights(ConnectionInfo.ConnectionInfo);
                return tb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //char[] HexStringToBinary(string hexString, int bitsCount)
        //{
        //    long longValue = HexToLong(hexString);
        //    return LongToBinary(longValue, bitsCount);
        //    //long longValue = Convert.ToInt64(hexString, 16);
        //    //string binRepresentation = Convert.ToString(longValue, 2);
        //    //byte stringLength = 48;
        //    //if (binRepresentation.Length > 48) stringLength = 60;
        //    //while (binRepresentation.Length < stringLength)
        //    //{
        //    //    binRepresentation = String.Concat("0", binRepresentation);
        //    //    //binRepresentation = String.Concat(binRepresentation, "0");
        //    //}
        //    //return binRepresentation.ToCharArray();
        //}

        //long HexToLong(string hexString)
        //{
        //    return Convert.ToInt64(hexString, 16); ;
        //}
        //char[] LongToBinary(long longValue, int bitsCount)
        //{
        //    string binRepresentation = Convert.ToString(longValue, 2);

        //    if (binRepresentation.Length > bitsCount)
        //        binRepresentation.Substring(binRepresentation.Length - bitsCount, bitsCount);

        //    while (binRepresentation.Length < bitsCount)
        //    {
        //        binRepresentation = String.Concat("0", binRepresentation);
        //        //binRepresentation = String.Concat(binRepresentation, "0");
        //    }
        //    return binRepresentation.ToCharArray();
        //}

        public void ResetMaxIOFailure()
        {
            LastIOFailureCount = 0;
        }

        public bool IsMaxIOFailureOccure(bool isFailure = true)
        {
            try
            {
                if (isFailure)
                    this.LastIOFailureCount += 1;
                if (LastIOFailureCount > MaxRetryReadFailure)
                    return true;
                else
                    return false;
            }
            finally
            {
                // IO Operation Is Success
                if (!isFailure)
                    this.LastIOFailureCount = 0;
            }
        }

        public void ReInitApplicationController()
        {
            try
            {
                this.ActivityLogger = null;
                _AP_Controller.Init_ApplicationProcess_Controller();
                // Re-Instantiate Objects
                if (_Param_Controller == null)
                    _Param_Controller = new ParameterController();
                if (_ConnectionController == null)
                    _ConnectionController = new ConnectionController();
                if (_Billing_Controller == null)
                    _Billing_Controller = new BillingController();
                if (_LoadProfile_Controller == null)
                    _LoadProfile_Controller = new LoadProfileController();
                if (_EventController == null)
                    _EventController = ObjectFactory.GetEventControllerObject(); // new EventController();
                if (_InstantaneousController == null)
                    _InstantaneousController = new InstantaneousController();

                // Assign Application Process Controller
                Param_Controller.AP_Controller = Applicationprocess_Controller;
                Billing_Controller.AP_Controller = Applicationprocess_Controller;
                LoadProfile_Controller.AP_Controller = Applicationprocess_Controller;
                ConnectionController.AP_Controller = Applicationprocess_Controller;
                _InstantaneousController.AP_Controller = Applicationprocess_Controller;
                _EventController.AP_Controller = Applicationprocess_Controller;
                _EventController.ParentContainer = this;

                // Initialize ApplicationEvent Dispatcher
                _EventController.MajorAlarmEventDispatcher = ApplicationEventDispatcher;
                _EventController.MajorAlarmEventPool = ApplicationEventPool;
                var EventHandler = new Event_Handler_EventNotify(_EventController.EventNotification_Recieved);
                _AP_Controller.EventNotify += EventHandler;

                _lastIOFailureCount = 0;

                //  Initialize ConnectionInfo
                if (ConnectToMeter != null &&
                    ConnectToMeter.ConnectionInfo != null)
                {
                    LoadProfile_Controller.CurrentConnectionInfo = ConnectToMeter.ConnectionInfo;
                    Event_Controller.CurrentConnectionInfo = ConnectToMeter.ConnectionInfo;
                }
                else
                {
                    LoadProfile_Controller.CurrentConnectionInfo = null;
                    Event_Controller.CurrentConnectionInfo = null;
                }

                // Initialize Configurations
                LoadProfile_Controller.Configurator = Configurator;
                Event_Controller.Configurator = Configurator;
                if (Param_OBJ == null)
                    Param_OBJ = new Parameterization();

                // Assign and Initialize Load Profile Controller for Parameterizing Load Profile Channels
                Param_OBJ.Param_Controller = Param_Controller;
                Param_OBJ.LoadProfile_Controller = LoadProfile_Controller;

                // Param_OBJ.Init_LoadProfilesChannelsQuantities();
                if (Events_OBJ == null)
                    Events_OBJ = new Communicator.MeterConfiguration.Events();
                Events_OBJ.Param_Controller = Param_Controller;

                if (MIUF == null)
                    MIUF = new MeterInfoUpdateFlags();
                if (StatisticsObj == null)
                    StatisticsObj = new Statistics();
                if (DB_Controller == null)
                {
                    DB_Controller = new DatabaseController();
                    DB_Controller.NewDbException += new DbExceptionOccur(DB_Controller_NewDbException);
                }

                Param_OBJ.DBController = DB_Controller;
                Events_OBJ.DBController = DB_Controller;

                if (MeterInfo == null)
                    MeterInfo = new MeterInformation() { LogLevel = 5 };
            }
            catch
            { }
        }

        public void DeInitApplicationController()
        {
            try
            {
                this.ActivityLogger = null;
                _AP_Controller.DeInit_ApplicationProcess_Controller();
                execRunnerThread = null;
                GetAccessRightsDlg = null;
                connectToMeter = null;
                _MeterInfo_OBJ = null;
                param_MajorAlarmProfile_obj = null;

                _Param_Controller = null;
                _ConnectionController = null;
                _Billing_Controller = null;
                _LoadProfile_Controller = null;
                _InstantaneousController = null;
                _EventController = null;

                param_MajorAlarmProfile_obj = null;

                if (_DBController != null)
                {
                    _DBController.NewDbException -= DB_Controller_NewDbException;
                    _DBController.Dispose();
                    _DBController = null;
                }
                Param_OBJ = null;
                Events_OBJ = null;

                MeterInfo = null;
            }
            catch
            { }
        }

        public void ResetApplicationController()
        {
            try
            {
                _signalStrength = 0;
                AssociationState_Obj = AssociationState.Logout;
            }
            catch
            { }
        }

        #region Debugger & Logging

        public void LogMessage(string Message, uint Level = 0)
        {
            try
            {
                LogMessage(Message, true, Level);
                if (StatisticsObj != null)
                    StatisticsObj.InsertLog("", Message);
            }
            catch
            { }
        }

        public void LogMessage(string Message)
        {
            string msn = string.Empty;

            if (MeterInfo != null && !string.IsNullOrEmpty(MeterInfo.MSN))
                msn = MeterInfo.MSN;

            try
            {
                if (ActivityLogger != null)
                    ActivityLogger.WriteInformation(String.Format("{0}", Message),
                        LogDestinations.Console | LogDestinations.TextFile | LogDestinations.UDP);
            }
            catch (Exception ex)
            {
                if (ActivityLogger != null && ActivityLogger.WriteToEventLog)
                    ActivityLogger.WriteError(String.Format(" {0,10}\t{1}", msn, ex.Message), LogDestinations.EventLog);
            }
        }

        public void LogMessage(string Message, bool IsSaveLog, uint Level = 0)
        {
            string msn = string.Empty;

            if (MeterInfo != null && !string.IsNullOrEmpty(MeterInfo.MSN))
                msn = MeterInfo.MSN;

            try
            {
                if (MeterInfo.LogLevel >= Level)
                {
                    if (MeterInfo.EnableEchoLog && ActivityLogger != null)
                        ActivityLogger.WriteInformation(String.Format(" {0,10}\t{1}", msn, Message),
                            LogDestinations.Console | LogDestinations.TextFile | LogDestinations.UDP);
                }
                // notify live log to IConnection added by furqan 19/12/2014
                if (ConnectToMeter != null) ConnectToMeter.MeterLiveLog = Message;
            }
            catch (Exception ex)
            {
                if (ActivityLogger != null && ActivityLogger.WriteToEventLog)
                    ActivityLogger.WriteError(String.Format(" {0,10}\t{1}", msn, ex.Message), LogDestinations.EventLog);
            }
        }

        public void LogMessage(string Message, string _logCode, string value, uint Level = 0)
        {
            string msn = string.Empty;

            if (MeterInfo != null && !string.IsNullOrEmpty(MeterInfo.MSN))
                msn = MeterInfo.MSN;

            try
            {
                if (MeterInfo.LogLevel >= Level)
                {
                    if (MeterInfo.EnableEchoLog && ActivityLogger != null)
                        ActivityLogger.WriteInformation(String.Format(" {0,10}\t{1,-8}{2,-2}", msn, _logCode, value), LogDestinations.Console | LogDestinations.TextFile | LogDestinations.UDP);
                    if (StatisticsObj != null)
                        StatisticsObj.InsertLog(_logCode, value);
                    // notify live log to IConnection added by furqan 19/12/2014
                    if (ConnectToMeter != null) ConnectToMeter.MeterLiveLog = string.Format("{0,-8}{1,-2}", _logCode, value);
                }
            }
            catch (Exception ex)
            {
                if (ActivityLogger != null && ActivityLogger.WriteToEventLog)
                    ActivityLogger.WriteError(String.Format(" {0,10}\t{1}", msn, ex.Message), LogDestinations.EventLog);
            }
        }

        public void LogMessage(Exception Ex)
        {
            string msn = string.Empty;

            if (MeterInfo != null && !string.IsNullOrEmpty(MeterInfo.MSN))
                msn = MeterInfo.MSN;

            try
            {
                if (ActivityLogger != null)
                    ActivityLogger.LogMessage(String.Format(" {0,10}\t{1}", msn, Ex.Message), Ex, 1, string.Empty);

            }
            catch (Exception ex)
            {
                // Commons.WriteLine(String.Format(" {0,10}\t{1}", MeterInfo.MSN, ex.Message));
                if (ActivityLogger != null && ActivityLogger.WriteToEventLog)
                    ActivityLogger.WriteError(String.Format(" {0,10}\t{1}", msn, ex.Message), LogDestinations.EventLog);
            }
        }

        public void LogMessage(Exception Ex, uint Level, string methodName = "")
        {
            string msn = string.Empty;

            if (MeterInfo != null && !string.IsNullOrEmpty(MeterInfo.MSN))
                msn = MeterInfo.MSN;

            try
            {
                if (MeterInfo.LogLevel >= Level && Ex != null)
                {
                    var msg = "Originator: " + methodName;

                    if (MeterInfo.EnableEchoLog && ActivityLogger != null)
                        ActivityLogger.LogMessage(String.Format(" {0,10}\t{1}", msn, Ex.Message), Ex, Level, methodName);

                    if (ConnectToMeter != null) ConnectToMeter.MeterLiveLog = msg;

                    StatisticsObj.InsertLog("~*Exception*~");
                    StatisticsObj.InsertLog(msg, true);
                    if (Ex.TargetSite != null)
                        StatisticsObj.InsertLog("Method Name: " + Ex.TargetSite.Name + " ~ ");
                    ExtractEachExceptionAndLog(Ex, 10, StatisticsObj);

                    StatisticsObj.InsertLog("~*End-Exception*~");

                    MDCAlarm.MDCCombineEvents[((ushort)MDCEvents.exception_occur)] = true;
                    MDCAlarm.IsMDCEventOuccer = true;

                    var warning = string.Format("MDC Error:" + Ex.Message);
                    DB_Controller.Insert_Mdc_Events_Log(warning, ((ushort)MDCEvents.exception_occur), MeterInfo, SessionDateTime);
                }
            }
            catch (Exception ex)
            {
                // Commons.WriteLine(String.Format(" {0,10}\t{1}", MeterInfo.MSN, ex.Message));
                if (ActivityLogger != null && ActivityLogger.WriteToEventLog)
                    ActivityLogger.WriteError(String.Format(" {0,10}\t{1}", msn, ex.Message), LogDestinations.EventLog);
            }
        }

        public void ExtractEachExceptionAndLog(Exception ex, int level, Statistics logger)
        {
            try
            {
                logger.InsertLog(ex.Message);
                for (int i = 1; i < level; i++)
                {
                    if (ex.InnerException != null)
                        ex = ex.InnerException;
                    else
                        break;

                    logger.InsertLog(ex.Message);
                }
            }
            catch (Exception)
            {
            }
        }

        #endregion

        void DB_Controller_NewDbException(Exception ex)
        {
            try
            {
                LogMessage(ex, 4, "DatabaseController");
            }
            catch (Exception)
            {
            }
        }

        public bool CheckAnyEventToSavePQ()
        {
            try
            {
                for (int i = 0; i < ParamMajorAlarmProfileObj.AlarmItems.Count && i < MeterInfo.EventsToSavePQ.Length; i++)
                {
                    if (ParamMajorAlarmProfileObj.AlarmItems[i].IsTriggered && MeterInfo.EventsToSavePQ[i].Equals(1))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            { return false; }
        }

        private string ReadErrorString()
        {
            try
            {
                var buff = InstantaneousController.GETArray_Any(Get_Index.Read_errors, 2);
                StringBuilder err = new StringBuilder();
                for (int i = 0; i < buff.Length; i++)
                {
                    err.AppendFormat("E{0},", Convert.ToInt16(buff[i]));
                }
                return err.ToString().Trim(',');
            }
            catch (Exception)
            {

                throw;
            }
        }

        private string ReadCautionString()
        {
            try
            {
                var buff = InstantaneousController.GETArray_Any(Get_Index.Read_Cautions, 2);
                StringBuilder err = new StringBuilder();
                for (int i = 0; i < buff.Length; i++)
                {
                    err.AppendFormat("C{0},", Convert.ToInt16(buff[i]));
                }
                return err.ToString().Trim(',');
            }
            catch (Exception)
            {

                throw;
            }
        }

        private string ReadDebugContactorStatus()
        {
            try
            {
                var buff = InstantaneousController.GETArray_Any(Get_Index.I_Contactor_Flag, 2);
                return BitConverter.ToString(buff).Replace('-', ' ');
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void UpdateCsLiveDB(DateTime time, string msn)
        {
            try
            {

                if (_DBController.UpdateCS_Live(msn, time))
                {
                    LogMessage("Clock Synchronization time has been updated To Live", "CSLD", "PS");
                }
                else
                {
                    LogMessage("Clock Synchronization time has been failed to update Live", "CSLD", "PF");
                }
            }
            catch (Exception)
            {
                if (_DBController.InsertCS_Live(msn, time))
                {
                    LogMessage("Clock Synchronization time inserted as a new record To Live", "CSLD", "N");
                }
                else
                {
                    LogMessage("Clock Synchronization time insertion as a new record in live has been failed", "CSLD", "PF");
                }
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(info));
            }
        }

        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            // Remove All Initialized Variables
            try
            {
                if (_AP_Controller != null)
                {
                    _AP_Controller.ApplicationProcess.Dispose();
                    _AP_Controller = null;
                }

                #region Nullify All Objects

                _Param_Controller = null;
                _ConnectionController = null;
                Billing_Controller = null;
                LoadProfile_Controller = null;
                _InstantaneousController = null;
                _EventController = null;
                config = null;
                execRunnerThread = null;
                _threadCancelToken = null;
                GetAccessRightsDlg = null;
                connectToMeter = null;
                DB_Controller = null;
                Param_OBJ = null;

                if (StatisticsObj != null)
                {
                    StatisticsObj.Dispose();
                    StatisticsObj = null;
                }

                #endregion
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        ~ApplicationController()
        {
            try
            {
                Dispose();
            }
            catch (Exception)
            { }
        }
    }
}
