// #define Enable_DEBUG_ECHO
#define Enable_Error_Logging
// #define Enable_Transactional_Logging
#define Enable_Log_Message
#define Enable_Log_Error
#define Enable_IO_Logging

using comm;
using Comm;
using Commuincator.MeterConnManager;
using Communicator.Properties;
using DatabaseConfiguration.DataBase;
using DatabaseConfiguration.DataSet;
using DatabaseManager.Database;
using DLMS;
using LogSystem.Shared;
using LogSystem.Shared.Common;
using Serenity.Util;
using ServerToolkit.BufferManagement;
using SharedCode.Comm.DataContainer;
using SharedCode.Comm.HelperClasses;
using SharedCode.Common;
using SharedCode.Controllers;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace Communicator.MTI_MDC
{
    public class MDC : IDisposable
    {
        #region DataMembers

        private DateTime startTime;
        private DateTime stopTime;
        private ConnectionManager _ConnManager = null;
        private TCPConController _TCPConController_Obj = null;
        private MeterConnectionManager _meterConnManager;
        private Configs configs = null;
        private Thread NKA_Sch_Thread;
        private bool IsServerInit = false;
        private Configurator _Configurator;// = new Configurator();
        // Total 20 MB Pre-Reserved Buffer Pool Size
        public static readonly long BufferPoolSlabSize = 1024 * 1024;  // 1_MB Pre-Reserved Slab Size
        public static readonly int BufferPoolSlabCount = 20;           // 20 Slab Count
        private BufferPool pool = null;
        private GetDataReaderBuffer DataBuffer = null;
        const string tcpWindowsParamspathInRegistry = @"SYSTEM\CurrentControlSet\services\tcpip\Parameters\";
        static string ConnStr = string.Empty;
        #endregion

        #region Properties

        public ConnectionManager ConnectionManager
        {
            get { return _ConnManager; }
            set { _ConnManager = value; }
        }


        public TCPConController TCPConController
        {
            get { return _TCPConController_Obj; }
            set { _TCPConController_Obj = value; }
        }

        public MeterConnectionManager MeterConnectionManager
        {
            get { return _meterConnManager; }
            set { _meterConnManager = value; }
        }

        public Configs Configuration
        {
            get { return configs; }
            set { configs = value; }
        }

        public Configurator Configurator
        {
            get { return _Configurator; }
            set { _Configurator = value; }
        }

        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        public DateTime StopTime
        {
            get { return stopTime; }
            set { stopTime = value; }
        }

        public bool IsServerInitialized
        {
            get { return IsServerInit; }
        }

        public bool Is_NKA_SchedularRunning
        {
            get
            {
                return Commons.IsThreadRunning(NKA_Sch_Thread);
            }
        }

        public bool IsTCP_ListenerRunning
        {
            get
            {
                if (TCPConController != null &&
                    TCPConController.IsServerListening)
                    return true;
                else
                    return false;
            }
        }

        public bool IsServerRunning
        {
            get
            {
                if (IsServerInit && IsTCP_ListenerRunning &&
                    MeterConnectionManager != null &&
                    MeterConnectionManager.ThreadAllocator != null &&
                    MeterConnectionManager.ThreadAllocator.IsThreadAllocaterRunning)
                    return true;
                else
                    return false;
            }
        }

        public List<MeterSerialNumber> LogMeterSerialNumbersFilter
        {
            get
            {
                try
                {
                    List<MeterSerialNumber> _MSNFilter = null;

                    bool isValueProcessed = false;
                    // Default LogsMeterIdsFilter is String.Empty
                    string _LogsMeterIdsFilter = string.Empty;
                    var val = Settings.Default.LogMeterSerialNumbersFilter;

                    try
                    {
                        _MSNFilter = Commons.ConvertSTRToMSNList(val);
                        // IsvalueProcessed
                        isValueProcessed = _MSNFilter != null &&
                                           _MSNFilter.Count > 0;
                    }
                    catch
                    {
                        isValueProcessed = false;
                    }

                    // Validate Value Read from Configurations
                    if (string.IsNullOrEmpty(val) || !isValueProcessed)
                    {
                        _MSNFilter = null;
                        // throw new Exception("Error occurred while Loading/Save Logger Port Address Directory Configuration");
                    }

                    return _MSNFilter;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading MSN Filter Configuration", ex);
                }
            }
            set
            {
                try
                {
                    string MSN_STR = string.Empty;
                    if (value != null && value.Count > 0)
                        MSN_STR = Commons.ConvertMSNListToSTR(value);

                    // Validate
                    if (value == null || value.Count <= 0 ||
                        string.IsNullOrEmpty(MSN_STR))
                        return;

                    Settings.Default["LogMeterSerialNumbersFilter"] = MSN_STR;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while saving MSN Filter Configuration", ex);
                }
            }
        }



        public static ILogWriter Default_DataLogger
        {
            get
            {
                return LogWriter.LogWriter;
            }
        }

        public ILogWriter Activity_DataLogger
        {
            get;
            set;
        }


        #endregion

        #region Constructor

        public MDC()
        {
            try
            {
                LogWriter Activity_DataLoggerLocal = null;
                Activity_DataLogger = Activity_DataLoggerLocal = new LogWriter();

                Init_ActivityLogger(Activity_DataLoggerLocal);
            }
            catch (Exception ex)
            {
                LocalCommon.SaveApplicationException(new Exception("Main MDC"), 1);
                LocalCommon.LogMDCExceptionIntoFile(ex);
            }
        }

        /// <summary>
        /// MDC Static Constructor
        /// </summary>
        static MDC()
        {
            try
            {

                // Initialize Default MDC Logger
                var Default_DataLogger = LogWriter.LogWriter;

                // Configure Data Logger
                if (Default_DataLogger != null)
                {
                    // Default Data Logger
                    Default_DataLogger.ApplicationName = "Default_DataLogger";
                    Default_DataLogger.EnableLogs = true;
                    Default_DataLogger.EnableLogsBuffer = false;

                    Default_DataLogger.WriteToConsole = false;
                    Default_DataLogger.WriteToEventLog = true;
                    Default_DataLogger.WriteToTextLog = true;
                }
            }
            catch (Exception ex)
            {
                LocalCommon.SaveApplicationException(new Exception("Main_MDC"), 1);
#if Enable_Error_Logging
                LocalCommon.LogMDCExceptionIntoFile(ex);
#endif
            }
        }

        #endregion

        public void Start_TCPListener()
        {
            try
            {
                if (TCPConController.IsServerListening)
                    throw new Exception(String.Format("Error TCP Listener is already listening {0}", TCPConController.TCPServer.LocalSocket));
                // Reload TCP Server Parameters
                IPAddress ServerIP = TCPConController.TCPParams.ServerIP;
                int localPort = TCPConController.TCPParams.ServerPort;

                TCPConController.TCPServer.LocalSocket = new IPEndPoint(ServerIP, localPort);
                TCPConController.RestartServer();
                StartTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                LocalCommon.SaveApplicationException(new Exception("Main Start_TCPListener "), 1);
                throw new Exception("TCP Listener not Started " + ex.Message, ex);
            }
        }

        public void Stop_MeterConnections()
        {
            try
            {
                // Cancel Already Running Meter Connection
                if (MeterConnectionManager != null)
                {
                    MeterConnectionManager.ThreadAllocator.TryCancelAllRunningMeterConnections();
                }
                // Disconnect All Meter Connections
                ConnectionManager.Disconnect();
            }
            catch { }
        }

        public void Start()
        {
            try
            {


                if (TCPConController != null && TCPConController.IsServerListening)
                {
                    TCPConController.DisConnectServer();
                    TCPConController.Dispose();
                }

                // Initialize Data Activity Logger
                LogWriter Activity_DataLoggerLocal = null;
                if (Activity_DataLogger == null)
                {
                    Activity_DataLogger = Activity_DataLoggerLocal = new LogWriter();
                    Init_ActivityLogger(Activity_DataLoggerLocal);
                }

                Init_MDC();
                Start_TCPListener();
                StartTime = DateTime.Now;
                // Start_NonKeepAliveSchedular();
            }
            catch (Exception ex)
            {
                LocalCommon.SaveApplicationException(new Exception("Main MDC Start"), 1);
                throw new Exception("Error occurred while starting MDC_Application Server," + ex.Message, ex);
            }
        }

        public void Stop()
        {
            try
            {
                Dispose();
                GC.Collect();
                StopTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while stopping MDC_Application Server," + ex.Message, ex);
            }
        }

        public void Reset()
        {
            try
            {
                if (IsServerRunning)
                    Stop();
                Start();
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while resetting MDC_Application Server," + ex.Message, ex);
            }
        }

        // public void Stop_DataReaderThreadPools()
        // {
        //     try
        //     {
        //         if (ConnectionManager != null)
        //         {
        //             if (ConnectionManager.IOConnectionsList != null)
        //                 ConnectionManager.IOConnectionsList.Disconnect();
        //             ConnectionManager.StopAvailableDataPolling();
        //         }
        //     }
        //     catch (Exception ex) { throw ex; }
        // }

        #region Support_Function

        public IBuffer GetDataBuffer(int MaxBufferSize = Commons.IOBufferLength)
        {
            try
            {
                lock (pool)
                {
                    return pool.GetBuffer(MaxBufferSize);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while taking Data Reader Buffer ", ex);
            }
        }

        public void Init_MDC()
        {
            try
            {
                #region // Object Initial Work
                #region Initial Pre_Allocated BufferPool

                pool = new BufferPool(BufferPoolSlabSize, BufferPoolSlabCount, BufferPoolSlabCount / 2);
                DataBuffer = new GetDataReaderBuffer(GetDataBuffer);
                //     () =>
                // {
                //     try
                //     {
                //         return pool.GetBuffer(Commons.IOBufferLength);
                //     }
                //     catch (Exception ex)
                //     {
                //         throw new Exception("Error occurred while initialize Data Reader Buffer ", ex);
                //     }
                // };

                #endregion
                ConnectionManager = new ConnectionManager();
                TCPConController = new TCPConController();
                Configuration = new Configs();
                Configurator = new Configurator();
                this._Configurator.ConfigurationHelper = new ConfigsHelper(Configuration);

                // Load Application Initialization Settings
                Init_Application_Config();
                string dsn = string.Format("Dsn={0}", DatabaseManager.Properties.Settings.Default.MDC_DSN);
                //ConnStr = Crypto.Decrypt(DatabaseManager.Properties.Settings.Default.MDC_DSN, Commons.Key_ConStr);
                //this._Configurator.ConfigurationHelper.DAL = new MDC_DBAccessLayer(dsn);
                ConnStr = dsn;
                this._Configurator.ConfigurationHelper.DAL = new ConfigDBController(ConnStr, DatabaseConfiguration.DataBaseTypes.MDC_DATABASE_With_ODBC);
                Commons.GetNetMaskValues();
                // Load Meter Configurations
                TryLoadConfiguration(Configuration);
                // Configuration.LoadConfigurations();
                MeterConnectionManager = new MeterConnectionManager();
                MeterConnectionManager.Init_MeterConnectionManager();
                MeterConnectionManager.ConnectionManager = ConnectionManager;
                // Meter Connection Manager Init
                // These settings form after verification at onfiguration_Settings
                MeterConnectionManager.MaxMeterConnection = Settings.Default.MaxMeterConn;
                MeterConnectionManager.MinMeterConnection = Settings.Default.MinMeterConn;
                MeterConnectionManager.MaxKAMeterConnections = Settings.Default.MaxKeepAlive;
                MeterConnectionManager.Configuration = Configuration;
                MeterConnectionManager.Configurator = Configurator;
                MeterConnectionManager.ObjectCacheMinAgeTime = Settings.Default.MinCacheAge;
                MeterConnectionManager.ObjectCahceMaxAgeTime = Settings.Default.MaxCahceAge;
                MeterConnectionManager.ThreadAllocator.MDC_Status_Obj.Duration = Settings.Default.StatisticsUpdateSessionTime;
                MeterConnectionManager.Activity_DataLogger = this.Activity_DataLogger;

                // Initial Create Data Buffer Delegate
                ConnectionManager.CreateDataReaderBuffer = DataBuffer;
                MeterConnectionManager.CreateDataReaderBuffer = DataBuffer;

                ConnectionManager.TCPIPConnController_OBJ = TCPConController;
                TCPConController.TCPClientConnected += new Action<Socket>(ConnectionManager.TCP_CONController_TCPClientConnected);
                if (TCPConController.TCPServer != null)
                {
                    TCPConController.TCPServer.MAX_PHYSICAL_CONNECTIONS = Settings.Default.MaxPhyConn;
                    TCPConController.TCPServer.ConnectionList = ConnectionManager.IOConnectionsList;
                }

                // Initial Connection Manager
                ConnectionManager.DataReadTimeOut = Settings.Default.TCPTimeOut;
                // Loading TCP TimeOut/Inactivity Settings
                ConnectionManager.Enable_TCPInactivity = Settings.Default.isTCPTimeOut;
                ConnectionManager.TCPInactivityDuration = Settings.Default.TCP_Inactivity_TimeOut;
                // ThreadPool Object Initial Work
                // Start_DataReaderThreadPools();

                try
                {
                    ThreadPool.SetMaxThreads(Settings.Default.WorkerThreadPoolSize, Settings.Default.IOThreadPoolSize);
                    ThreadPool.SetMinThreads(Settings.Default.MinWorkerThreadPoolSize, Settings.Default.MinIOThreadPoolSize);

#if Enable_DEBUG_ECHO

                // int workerTH,IOTH;
                // System.Threading.ThreadPool.GetMinThreads(out workerTH,out IOTH);
                // Console.Out.WriteLine("Thread Pool Size  MIN Worker {0} IOCompletion{1}", workerTH, IOTH);
                // System.Threading.ThreadPool.GetMaxThreads(out workerTH, out IOTH);
                // Console.Out.WriteLine("Thread Pool Size  MAX Worker {0} IOCompletion{1}", workerTH, IOTH);
                // System.Threading.ThreadPool.GetAvailableThreads(out workerTH, out IOTH);
                // Console.Out.WriteLine("Thread Pool Size  Available Worker {0} IOCompletion{1}", workerTH, IOTH);

#endif
                }
                catch (Exception ex)
                {
                    LocalCommon.SaveApplicationException(new Exception("Main Init_MDC "), 1);
#if Enable_Error_Logging
                    LocalCommon.LogMDCExceptionIntoFile(ex);
                    // MessageBox.Show("Error in [Init_MDC]:" + ex.Message + Environment.NewLine + "Detail:" + ex.StackTrace);
#endif
                }

                // CheckAccessRights

                // Comment see later sajid

                //if (Settings.Default.CheckAccessRights != null)
                ApplicationProcess_Controller.CheckAccessRights = Settings.Default.CheckAccessRights;

                #endregion

                // MeterConnectionManager.Debugger.IOLog += new Action<string, byte[], DataStatus, DateTime>(Commons.Debugger_IOLog);
                // MeterConnectionManager.Debugger.Logger += new LogMessage(Commons.Debugger_Logger);

                // Register Activity Logger
                MeterConnectionManager.Debugger.RegisterActivityLogger = this.Activity_DataLogger;

                // Default DLMSDebuggerLogger Configuration
                MeterConnectionManager.Debugger.EnableProcessInfoLog = false;
                MeterConnectionManager.Debugger.EnableErrorLog = false;
                MeterConnectionManager.Debugger.EnableIOLog = false;

                #region Enable Logs & Debugging

#if Enable_Log_Message
                MeterConnectionManager.Debugger.EnableProcessInfoLog = Settings.Default.EnableProcessInfoLog;
#endif
#if Enable_IO_Logging
                MeterConnectionManager.Debugger.EnableIOLog = Settings.Default.EnableIOLog;
#endif
#if Enable_Log_Error
                MeterConnectionManager.Debugger.EnableErrorLog = Settings.Default.EnableErrorLog;
#endif

                #region MSN Based Filter For DLMS Logger

                // Remove MSN Based Filter
                if (LogMeterSerialNumbersFilter == null ||
                   LogMeterSerialNumbersFilter.Count <= 0)
                {
                    if (MeterConnectionManager.Debugger.EnableErrorInfoLog != null)
                        MeterConnectionManager.Debugger.EnableErrorInfoLog.Clear();
                    if (MeterConnectionManager.Debugger.EnableProcessingInfoLog != null)
                        MeterConnectionManager.Debugger.EnableProcessingInfoLog.Clear();
                    if (MeterConnectionManager.Debugger.EnableIOFlowLogs != null)
                        MeterConnectionManager.Debugger.EnableIOFlowLogs.Clear();
                }
                else
                {
                    string MSN_STR = String.Empty;
                    List<MeterSerialNumber> msnFilter = LogMeterSerialNumbersFilter;
                    bool EnableMsnFilter = Settings.Default.EnableMSNFilter;

                    // Clear All Previous MSN Filter Settings
                    if (MeterConnectionManager.Debugger.EnableErrorInfoLog != null)
                        MeterConnectionManager.Debugger.EnableErrorInfoLog.Clear();
                    if (MeterConnectionManager.Debugger.EnableProcessingInfoLog != null)
                        MeterConnectionManager.Debugger.EnableProcessingInfoLog.Clear();
                    if (MeterConnectionManager.Debugger.EnableIOFlowLogs != null)
                        MeterConnectionManager.Debugger.EnableIOFlowLogs.Clear();

                    // New MSN Based Filter
                    foreach (var msn in msnFilter)
                    {
                        MSN_STR = String.Empty;
                        MSN_STR = msn.ToString();

                        // SET MSN Filter Settings
                        if (MeterConnectionManager.Debugger.EnableErrorInfoLog != null)
                            MeterConnectionManager.Debugger.EnableErrorInfoLog.TryAdd(MSN_STR, EnableMsnFilter);

                        if (MeterConnectionManager.Debugger.EnableProcessingInfoLog != null)
                            MeterConnectionManager.Debugger.EnableProcessingInfoLog.TryAdd(MSN_STR, EnableMsnFilter);

                        if (MeterConnectionManager.Debugger.EnableIOFlowLogs != null)
                            MeterConnectionManager.Debugger.EnableIOFlowLogs.TryAdd(MSN_STR, EnableMsnFilter);
                    }
                }

                #endregion

                #endregion

                MeterConnectionManager.Debugger.RegisterActivityLogger = Activity_DataLogger;

                string Server_Instance = "Default";
                if (!string.IsNullOrEmpty(Communicator.Properties.Settings.Default.Instance))
                    Server_Instance = Communicator.Properties.Settings.Default.Instance;

                Activity_DataLogger.ApplicationName = string.Format("MDC_{0}", Server_Instance);

                // Configure Activity Logger
                Activity_DataLogger.EnableLogs = Settings.Default.EnableLogs;
                Activity_DataLogger.EnableLogsBuffer = Settings.Default.EnableLogsBuffer;

                Activity_DataLogger.WriteToConsole = Settings.Default.EnableWriteToConsole;
                Activity_DataLogger.WriteToTextLog = Settings.Default.EnableWriteToTextLog;
                Activity_DataLogger.WriteToEventLog = Settings.Default.EnableWriteToEventLog;
                Activity_DataLogger.WriteToUDPLog = Settings.Default.EnableWriteToUDPLog;

                // Text Logging Setting
                Activity_DataLogger.LogsDirectory = Settings.Default.LogsDirectory;
                Activity_DataLogger.LogsFileSize = Settings.Default.LogsFileSize;
                Activity_DataLogger.LogsFileCount = Settings.Default.LogsFileCount;

                // Activity_DataLoggerLocal.LogsFile = string.Empty;
                IPAddress LoggerBroadcastIPAddress = IPAddress.Broadcast;
                // UDP Broad Cast Settings
                if (IPAddress.TryParse(Settings.Default.LoggerBroadcastIPAddress, out LoggerBroadcastIPAddress))
                    Activity_DataLogger.BroadcastIPAddress = LoggerBroadcastIPAddress;
                Activity_DataLogger.Port = Settings.Default.LoggerPort;

                MeterInformation.Validate_MSN = Settings.Default.Validate_MSN;

                IsServerInit = true;
            }
            catch (Exception ex)
            {
                LocalCommon.SaveApplicationException(new Exception("Main_Init_MDC "), 1);
                LocalCommon.LogMDCExceptionIntoFile(ex);
            }
        }

        public void Init_ActivityLogger(LogWriter Activity_DataLoggerLocal)
        {
            try
            {
                // Application
                MDC.Init_Application_Config();

                string Server_Instance = "Default";
                if (!string.IsNullOrEmpty(Communicator.Properties.Settings.Default.Instance))
                    Server_Instance = Communicator.Properties.Settings.Default.Instance;

                Activity_DataLoggerLocal.ApplicationName = string.Format("MDC_{0}", Server_Instance);

                // Configure Activity Logger 
                Activity_DataLoggerLocal.EnableLogs = Settings.Default.EnableLogs;
                Activity_DataLoggerLocal.EnableLogsBuffer = Settings.Default.EnableLogsBuffer;

                Activity_DataLoggerLocal.WriteToConsole = Settings.Default.EnableWriteToConsole;
                Activity_DataLoggerLocal.WriteToTextLog = Settings.Default.EnableWriteToTextLog;
                Activity_DataLoggerLocal.WriteToEventLog = Settings.Default.EnableWriteToEventLog;

                Activity_DataLoggerLocal.WriteToUDPLog = Settings.Default.EnableWriteToUDPLog;
                Activity_DataLoggerLocal.DisplayMessageBoxes = false;

                // Text Logging Setting
                // Set Default Application Logs Directory
                Activity_DataLoggerLocal.LogsDirectory = Settings.Default.LogsDirectory;
                Activity_DataLoggerLocal.LogsFileSize = Settings.Default.LogsFileSize;
                Activity_DataLoggerLocal.LogsFileCount = Settings.Default.LogsFileCount;

                // Activity_DataLoggerLocal.LogsFile = string.Empty;
                IPAddress LoggerBroadcastIPAddress = IPAddress.Broadcast;
                // UDP Broad Cast Settings
                if (IPAddress.TryParse(Settings.Default.LoggerBroadcastIPAddress, out LoggerBroadcastIPAddress))
                    Activity_DataLoggerLocal.BroadcastIPAddress = LoggerBroadcastIPAddress;
                Activity_DataLoggerLocal.Port = Settings.Default.LoggerPort;

                Activity_DataLoggerLocal.AttachGlobalExceptionHandler();
            }
            catch(Exception ex)
            {
#if Enable_Error_Logging
                LocalCommon.LogMDCExceptionIntoFile(ex);
                //MessageBox.Show("Error in [Init_ActivityLogger]:" + ex.Message+Environment.NewLine + "Detail:"+ex.StackTrace);
#endif
            }
        }

        public static void Init_Application_Config()
        {
            try
            {
                Configuration_Settings Local = new Configuration_Settings();
                Local.Local_Config = Configuration_Settings.Load_ExternalConfig();

                // initial Windows Side TCP Settings
                // Init_Registry_TCP_Params(Local);
                // Load Application Configurations To DatabaseManager Settings Class
                #region Load_Settings

                DatabaseManager.Properties.Settings.Default["MDC_DSN"] = Local.MDC_DSN;
                DatabaseManager.Properties.Settings.Default["MaxPoolSize"] = Local.ConnectionPoolSize;
                DatabaseManager.Properties.Settings.Default["ConnectionLifeTime"] = Local.ConnectionResetTime;
                DatabaseManager.Properties.Settings.Default["EnableWeeklyLoadProfile"] = Local.IsEnableWeeklyLoadProfile;
                DatabaseManager.Properties.Settings.Default["EnableWeeklyInstentanousData"] = Local.IsEnableWeeklyInstantanous;
                DatabaseManager.Properties.Settings.Default.Save();

                #endregion

                // Load Application Configurations To Comm.Properties.Settings Class
                #region Load_Settings

                Properties.Settings.Default["ApplicationConfigsDirectory"] = Local.Applicatoin_Config_Directory;
                Properties.Settings.Default.Save();

                #endregion

                // Load Application Configurations To Comm.Properties.Settings Class
                #region Load_Settings

                Communicator.Properties.Settings.Default["ApplicationConfigsDirectory"] = Local.Applicatoin_Config_Directory;
                Communicator.Properties.Settings.Default["ServerIP"] = Local.Server_IP;
                Communicator.Properties.Settings.Default["Port"] = Local.Server_Port;
                Communicator.Properties.Settings.Default["Instance"] = Local.ServerInstanceName;
                Communicator.Properties.Settings.Default["MaxPhyConn"] = Local.MaxTCPIPConnection;
                Communicator.Properties.Settings.Default["MaxMeterConn"] = Local.MaxConcurrentMeterConnection;
                Communicator.Properties.Settings.Default["MinMeterConn"] = Local.MinConcurrentMeterConnection;
                Communicator.Properties.Settings.Default["MaxKeepAlive"] = Local.MaxConcurrentKeepAliveMeterConnection;
                Communicator.Properties.Settings.Default["WorkerThreadPoolSize"] = Local.MaxWorkerThreadPoolSize;
                Communicator.Properties.Settings.Default["IOThreadPoolSize"] = Local.MaxIOThreadPoolSize;
                Communicator.Properties.Settings.Default["TCPTimeOut"] = Local.TCPTimeOut;
                Communicator.Properties.Settings.Default["TCP_Inactivity_TimeOut"] = Local.TCPInactivityTimeOut;
                Communicator.Properties.Settings.Default["isTCPTimeOut"] = Local.IsEnableTCPInactivityTimeOut;
                Communicator.Properties.Settings.Default["SchedulerPoolingTime"] = Local.KeepAliveSchedulerPollingTime;
                Communicator.Properties.Settings.Default["startKAScheduler"] = Local.IsEnableKeepAliveScheduler;
                Communicator.Properties.Settings.Default["SaveLogToDBFlag"] = Local.SaveLogToDB;
                Communicator.Properties.Settings.Default["MaxCahceAge"] = Local.ApplicationDataCacheMaxAge;
                Communicator.Properties.Settings.Default["MinCacheAge"] = Local.ApplicationDataCacheMinAge;
                Communicator.Properties.Settings.Default["IsEnableInvalidTimeSync"] = Local.IsEnableInvalidTimeSync;
                Communicator.Properties.Settings.Default["SaveMDCSessions"] = Local.SaveMDCSession;
                Communicator.Properties.Settings.Default["SaveMDCStatus"] = Local.SaveMDCStatus;
                Communicator.Properties.Settings.Default["SaveStatistics"] = Local.EnableSaveStatistics;
                Communicator.Properties.Settings.Default["SaveErrorToDB"] = Local.IsEnableSaveErrorLogToDB;
                Communicator.Properties.Settings.Default["GrantContactorOnPermission"] = Local.PermissionToONContactor;
                Communicator.Properties.Settings.Default["GrantContactorOFFPermission"] = Local.PermissionToOFFContactor;
                Communicator.Properties.Settings.Default["CheckAccessRights"] = Local.CheckAccessRights;
                Communicator.Properties.Settings.Default["TimeSyncOnBatteryDead"] = Local.PermissionToTimeSyncOnBatteryDead;
                Communicator.Properties.Settings.Default["PermissionParamsWrite"] = Local.WriteParamString;

                // Debugger & Logger Settings
                Communicator.Properties.Settings.Default["EnableProcessInfoLog"] = Local.EnableProcessInfoLog;
                Communicator.Properties.Settings.Default["EnableIOLog"] = Local.EnableIOLog;
                Communicator.Properties.Settings.Default["EnableErrorLog"] = Local.EnableErrorLog;

                // Default LogsMeterIdsFilter is String.Empty
                string _LogsMeterIdsFilter = Configuration_Settings.LogsMeterIdsFilter_VALUE;
                var val = Local.Local_Config.AppSettings.Settings[Configuration_Settings.LogsMeterIdsFilter_KEY].Value;

                if (!string.IsNullOrEmpty(val))
                    Communicator.Properties.Settings.Default["LogMeterSerialNumbersFilter"] = val;
                else
                    Communicator.Properties.Settings.Default["LogMeterSerialNumbersFilter"] = _LogsMeterIdsFilter;
                Communicator.Properties.Settings.Default["EnableMSNFilter"] = Local.EnableMSNFilter;

                Communicator.Properties.Settings.Default["EnableLogs"] = Local.EnableLogs;
                Communicator.Properties.Settings.Default["EnableLogsBuffer"] = Local.EnableLogsBuffer;
                Communicator.Properties.Settings.Default["EnableWriteToConsole"] = Local.EnableWriteToConsole;
                Communicator.Properties.Settings.Default["EnableWriteToTextLog"] = Local.EnableWriteToTextLog;
                Communicator.Properties.Settings.Default["EnableWriteToEventLog"] = Local.EnableWriteToEventLog;
                Communicator.Properties.Settings.Default["EnableWriteToUDPLog"] = Local.EnableWriteToUDPLog;

                Communicator.Properties.Settings.Default["LogsDirectory"] = Local.LogsDirectory;
                Communicator.Properties.Settings.Default["LogsFileSize"] = Local.LogsFileSize;
                Communicator.Properties.Settings.Default["LogsFileCount"] = Local.LogsFileCount;

                Communicator.Properties.Settings.Default["LoggerBroadcastIPAddress"] = Local.LoggerBroadcastIPAddress.ToString();
                Communicator.Properties.Settings.Default["LoggerPort"] = Local.LoggerPort;

                // HDLC Configuration Parameter Update
                Communicator.Properties.Settings.Default["HDLCAddressLength"] = Local.HDLCAddressLength;
                Communicator.Properties.Settings.Default["MaxInfoBufferTransmit"] = Local.MaxInfoBufferTransmit;
                Communicator.Properties.Settings.Default["MaxInfoBufferReceive"] = Local.MaxInfoBufferReceive;
                Communicator.Properties.Settings.Default["WinSizeTransmit"] = Local.WindowSizeTransmit;
                Communicator.Properties.Settings.Default["WinSizeReceive"] = Local.WindowSizeReceive;
                Communicator.Properties.Settings.Default["DeviceAddress"] = Local.DeviceAddress;
                Communicator.Properties.Settings.Default["ResponseTimeOut"] = Local.RequestResponseTimeOut.ToString();
                Communicator.Properties.Settings.Default["InActivityTimeOut"] = Local.InActivityTimeOut.ToString();
                Communicator.Properties.Settings.Default["IsKAEnable"] = Local.IsKeepAliveEnable;
                Communicator.Properties.Settings.Default["IsEnableRetry"] = Local.IsEnableRetrySend;
                Communicator.Properties.Settings.Default["IsSkipLoginParam"] = Local.IsSkipLoginParameter;

                Communicator.Properties.Settings.Default.Save();

                // Shared Code Configuration Updates

                SharedCode.Properties.Settings.Default["ApplicationConfigsDirectory"] = Local.Applicatoin_Config_Directory;
                SharedCode.Properties.Settings.Default["ServerIP"] = Local.Server_IP;
                SharedCode.Properties.Settings.Default["Port"] = Local.Server_Port;
                SharedCode.Properties.Settings.Default["TCPTimeOut"] = Local.TCPTimeOut;
                SharedCode.Properties.Settings.Default["isTCPTimeOut"] = Local.IsEnableTCPInactivityTimeOut;

                SharedCode.Properties.Settings.Default["CheckAccessRights"] = Local.CheckAccessRights;

                SharedCode.Properties.Settings.Default["HDLCAddressLength"] = Local.HDLCAddressLength;
                SharedCode.Properties.Settings.Default["MaxInfoBufferTransmit"] = Local.MaxInfoBufferTransmit;
                SharedCode.Properties.Settings.Default["MaxInfoBufferReceive"] = Local.MaxInfoBufferReceive;
                SharedCode.Properties.Settings.Default["WinSizeTransmit"] = Local.WindowSizeTransmit;
                SharedCode.Properties.Settings.Default["WinSizeReceive"] = Local.WindowSizeReceive;
                SharedCode.Properties.Settings.Default["DeviceAddress"] = Local.DeviceAddress;
                SharedCode.Properties.Settings.Default["ResponseTimeOut"] = Local.RequestResponseTimeOut;
                SharedCode.Properties.Settings.Default["InActivityTimeOut"] = Local.InActivityTimeOut;
                SharedCode.Properties.Settings.Default["IsKAEnable"] = Local.IsKeepAliveEnable;
                SharedCode.Properties.Settings.Default["IsEnableRetry"] = Local.IsEnableRetrySend;
                SharedCode.Properties.Settings.Default["IsSkipLoginParam"] = Local.IsSkipLoginParameter;

                SharedCode.Properties.Settings.Default.Save();

                #endregion
                Configuration_Settings.Save_Configuration(Local.Local_Config);
            }
            catch (Exception ex)
            {
#if Enable_Error_Logging
                LocalCommon.LogMDCExceptionIntoFile(ex);
                //MessageBox.Show("Error in [Init_Application_Config]:" + ex.Message + Environment.NewLine + "Detail:" + ex.StackTrace);
#endif
            }
        }

        public bool TryLoadConfiguration(Configs configDataSet)
        {
            bool isSuccess = false;
            try
            {
                //string dsn = string.Format("Dsn={0}", DatabaseManager.Properties.Settings.Default.MDC_DSN);
                //MDC_DBAccessLayer DBDAO = new MDC_DBAccessLayer(dsn);

                ConfigDBController DBDAO = new ConfigDBController(ConnStr, DatabaseConfiguration.DataBaseTypes.MDC_DATABASE_With_ODBC);
                DBDAO.Load_All_Configurations(configDataSet);

                // Select Configuration
                Configs.ConfigurationRow DefaultConfig = null;
                if (configDataSet.Configuration != null &&
                    configDataSet.Configuration.Count > 0)
                {
                    DefaultConfig = configDataSet.Configuration[0];
                }

                configDataSet.Configuration.CurrentConfiguration = DefaultConfig;

                isSuccess = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error loading device Configuration " + ex.Message));
                // throw ex;
                isSuccess = false;
#if Enable_Error_Logging
                LocalCommon.LogMDCExceptionIntoFile(ex);
                //MessageBox.Show("Error in [TryLoadConfiguration]:" + ex.Message + Environment.NewLine + "Detail:" + ex.StackTrace);
#endif
            }
            return isSuccess;
        }

        // public void Init_Registry_TCP_Params(Configuration_Settings configs)
        // {
        // try
        // {
        //     //========================================================================================================================
        //     var k1 = Registry.LocalMachine.CreateSubKey(tcpWindowsParamspathInRegistry, RegistryKeyPermissionCheck.ReadWriteSubTree);
        //     //========================================================================================================================
        //     var tcpTime = k1.GetValue("TcpTimedWaitDelay", null);
        //     if(tcpTime!=null && Convert.ToInt32(tcpTime) != configs.TcpTimedWaitDelay) k1.SetValue("TcpTimedWaitDelay", configs.TcpTimedWaitDelay);
        //     //========================================================================================================================
        //     var tcpNumConnection = k1.GetValue("TcpNumConnections", null);
        //     if (tcpTime != null && Convert.ToInt32(tcpNumConnection) != configs.TcpNumConnections) k1.SetValue("TcpNumConnections", configs.TcpNumConnections);
        //     //========================================================================================================================
        //     var tcpMaxUserPort = k1.GetValue("MaxUserPort", null);
        //     if (tcpTime != null && Convert.ToInt32(tcpMaxUserPort) != configs.MaxUserPort) k1.SetValue("MaxUserPort", configs.MaxUserPort);
        //     //========================================================================================================================
        //     var tcpMaxHashTable = k1.GetValue("MaxHashTableSize", null);
        //     if (tcpTime != null && Convert.ToInt32(tcpMaxUserPort) != configs.MaxHashTableSize) k1.SetValue("MaxHashTableSize", configs.MaxHashTableSize);
        //     //========================================================================================================================
        //     var tcpMaxFreeTcbs = k1.GetValue("MaxFreeTcbs", null);
        //     if (tcpTime != null && Convert.ToInt32(tcpMaxUserPort) != configs.MaxFreeTcbs) k1.SetValue("MaxFreeTcbs", configs.MaxFreeTcbs);
        //     //========================================================================================================================
        // }
        // catch (Exception)
        // {
        // }
        //  }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                this.Activity_DataLogger = null;

                if (TCPConController != null)
                {
                    TCPConController.DisConnectServer();
                    TCPConController.Dispose();
                    TCPConController = null;
                }
                if (MeterConnectionManager != null)
                {
                    MeterConnectionManager.Dispose();
                    MeterConnectionManager = null;
                }
                if (Configuration != null)
                {
                    Configuration.Dispose();
                    Configuration = null;
                }
                if (ConnectionManager != null)
                {
                    ConnectionManager.Dispose();
                    ConnectionManager = null;
                }

                IsServerInit = false;
                OdbcConnection.ReleaseObjectPool();
            }
            catch { }
        }

        #endregion
    }
}
