using comm;
using SharedCode.Comm.DataContainer;
using SharedCode.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Comm
{
    public class Configuration_Settings
    {
        #region Properties_Default_Keys

        #region DataBaseManager_KEYS

        public static readonly string AssembllyConfigURL = @"Application_Configs\Application_Common_Config.config";
        public static readonly string DSN_KEY = "MDC_DSN";
        public static readonly string PoolSize_KEY = "MaxPoolSize";
        public static readonly string ConnectionLifeTime_KEY = "ConnectionLifeTime";
        public static readonly string ConfigDirectory_KEY = "ConfigurationDirectory";

        #endregion

        #region Communicator_KEYS

        public static readonly string ServerIP_KEY = "ServerIP";
        public static readonly string ServerPort_KEY = "ServerPort";
        public static readonly string ServerInstanceName_KEY = "InstanceName";
        public static readonly string MaxTCPIPConnection_KEY = "MaxTCPIPConnectionCount";
        public static readonly string MaxConcurrentMeterConnection_KEY = "MaxConcurrentMeterConnection";
        public static readonly string MinConcurrentMeterConnection_KEY = "MinConcurrentMeterConnection";
        public static readonly string MaxConcurrentKeepAliveMeterConnection_KEY = "MaxConcurrentKeepAliveMeterConnection";
        public static readonly string MaxWorkerThreadPoolSize_KEY = "MaxWorkerThreadPoolSize";
        public static readonly string MaxIOThreadPoolSize_KEY = "MaxIOThreadPoolSize";
        public static readonly string TCPTimeOut_KEY = "TCPTimeOut";
        public static readonly string TCPInactivityTimeOut_KEY = "TCPInactivityTimeOut";
        public static readonly string IsEnableTCPInactivityTimeOut_KEY = "IsEnableTCPInactivityTimeOut";
        public static readonly string KeepAliveSchedulerPollingTime_KEY = "KeepAliveSchedulerPollingTime";
        public static readonly string IsEnableKeepAliveScheduler_KEY = "IsEnableKeepAliveScheduler";
        public static readonly string IsEnableSaveErrorLogToDB_KEY = "IsEnableSaveErrorLogToDB";
        public static readonly string IsEnableInvalidTimeSync_KEY = "ClockSyncOnInvalidTime";
        public static readonly string IsEnableWeeklyLoadProfile_KEY = "EnableWeeklyLoadProfile";
        public static readonly string IsEnableWeeklyInstantaneous_KEY = "EnableWeeklyInstantaneous";

        public static readonly string SaveMDCSession_KEY = "SaveMDCSessions";
        public static readonly string SaveMDCStatus_KEY = "SaveMDCStatus";
        public static readonly string SaveStatistics_KEY = "SaveStatistics";
        public static readonly string SaveLogToDB_KEY = "SaveLogToDB";

        // Debugger & Logger Features
        public static readonly string EnableProcessInfoLog_KEY = "EnableProcessInfoLog";
        public static readonly string EnableIOLog_KEY = "EnableIOLog";
        public static readonly string EnableErrorLog_KEY = "EnableErrorLog";
        public static readonly string LogsMeterIdsFilter_KEY = "LogsMeterIdsFilter";
        public static readonly string EnableMSNFilter_KEY = "EnableMSNFilter";

        public static readonly string EnableLogs_KEY = "EnableLogs";
        public static readonly string EnableLogsBuffer_KEY = "EnableLogsBuffer";
        public static readonly string EnableWriteToConsole_KEY = "EnableWriteToConsole";
        public static readonly string EnableWriteToTextLog_KEY = "EnableWriteToTextLog";
        public static readonly string EnableWriteToEventLog_KEY = "EnableWriteToEventLog";
        public static readonly string EnableWriteToUDPLog_KEY = "EnableWriteToUDPLog";
        public static readonly string LogsDirectory_KEY = "LogsDirectory";

        public static readonly string LogsFileSize_KEY = "LogsFileSize";
        public static readonly string LogsFileCount_KEY = "LogsFileCount";
        public static readonly string LoggerBroadcastIPAddress_KEY = "LoggerBroadcastIPAddress";
        public static readonly string LoggerPort_KEY = "LoggerPort";

        #region Permissions

        public static readonly string PermissionToWriteParam_KEY = "WriteParamConfigurationString";
        public static readonly string PermissionToOnContactor_KEY = "PermissionToOnContactor";
        public static readonly string PermissionToOffContactor_KEY = "PermissionToOffContactor";
        public static readonly string CheckAccessRights_KEY = "CheckAccessRights";
        public static readonly string PermissionToSyncTimeOnBatteryDead_KEY = "TimeSyncOnBatteryDead";

        #endregion

        #endregion

        #region DLMS_COSEM_LIB_KEYS
        public static readonly string ApplicationDataCacheMinAge_KEY = "ApplicationDataCacheMinAge";
        public static readonly string ApplicationDataCacheMaxAge_KEY = "ApplicationDataCacheMaxAge";
        #endregion

        #region TCPWindowsParamsKeys

        // public static readonly string TcpParamWriteKey = "TcpTimedWaitDelay";
        // public static readonly string TcpTimedWaitDelayKey = "TcpTimedWaitDelay";
        // public static readonly string TcpNumConnectionsKey = "TcpNumConnections";
        // public static readonly string MaxHashTableSizeKey = "MaxHashTableSize";
        // public static readonly string MaxUserPortKey = "MaxUserPort";
        // public static readonly string MaxFreeTcbsKey = "MaxFreeTcbs";

        #endregion

        #region HDLC_LIB_KEYS

        public static readonly string HDLCAddressLength_KEY = "HDLCAddressLength";
        public static readonly string MaxInformationBufferTransmit_KEY = "MaxInfoBufferTransmit";
        public static readonly string MaxInformationBufferReceive_KEY = "MaxInfoBufferReceive";
        public static readonly string WinSizeTransmit_KEY = "WinSizeTransmit";
        public static readonly string WinSizeReceive_KEY = "WinSizeReceive";
        public static readonly string DeviceAddress_KEY = "DeviceAddress";
        public static readonly string RequestResponseTimeOut_KEY = "ResponseTimeOut";
        public static readonly string InActivityTimeOut_KEY = "InActivityTimeOut";
        public static readonly string IsKeepAliveEnable_KEY = "IsKeepAliveEnable";
        public static readonly string IsEnableRetrySend_KEY = "IsEnableRetrySend";
        public static readonly string IsSkipLoginParameter_KEY = "IsSkipLoginParameter";

        #endregion

        #endregion

        #region Properties_Default_Values

        #region DataBaseManager_DEFAULT

        public static readonly string SectionName = "appSettings";

        /// <summary>
        /// Default Values
        /// </summary>
        public static readonly string DSN_VALUE = "SmartEyeMDC_DSN";
        public static readonly int PoolSize_VALUE = 1200;
        public static readonly int ConnectionLifeTime_VALUE = 300;
        public static readonly string ConfigDirectory_VALUE = @"\Application_Configs";

        #endregion

        #region Communicator_DEFAULT

        public static readonly string ServerIP_VALUE = "0.0.0.0";
        public static readonly int ServerPort_VALUE = 4059;
        public static readonly string ServerInstanceName_VALUE = "SmartEyeMDC";
        public static readonly int MaxTCPIPConnection_VALUE = 2000;
        public static readonly int MaxConcurrentMeterConnection_VALUE = 400;
        public static readonly int MinConcurrentMeterConnection_VALUE = 400;
        public static readonly float MaxConcurrentKeepAliveMeterConnection_VALUE = 10f;
        public static readonly int MaxWorkerThreadPoolSize_VALUE = 600;
        public static readonly int MaxIOThreadPoolSize_VALUE = 4000;
        public static readonly TimeSpan TCPTimeOut_VALUE = TimeSpan.FromSeconds(30f);
        public static readonly TimeSpan TCPInactivityTimeOut_VALUE = TimeSpan.FromMinutes(3.0f);
        public static readonly bool IsEnableTCPInactivityTimeOut_VALUE = true;
        public static readonly TimeSpan KeepAliveSchedulerPollingTime_VALUE = TimeSpan.FromSeconds(60f);
        public static readonly bool IsEnableKeepAliveScheduler_VALUE = true;
        public static readonly bool IsEnableSaveErrorLogToDB_VALUE = true;
        public static readonly bool IsEnableInvalidTimeSync_VALUE = true;
        public static readonly bool IsEnableWeeklyLoadProfile_VALUE = true;
        public static readonly bool IsEnableWeeklyInstantaneous_VALUE = true;

        public static readonly bool SaveMDCSession_VALUE = false;
        public static readonly bool SaveMDCStatus_VALUE = false;
        public static readonly bool SaveStatistics_VALUE = false;
        public static readonly bool SaveLogToDB_VALUE = true;

        public static readonly bool PermissionToOnContactor_VALUE = false;
        public static readonly bool PermissionToOffContactor_VALUE = false;

        public static readonly bool CheckAccessRights_VALUE = false;
        public static readonly bool PermissionToTimeSyncOnBatteryDead_VALUE = false;
        public static readonly string PermissionToWriteParam_VALUE = "FFFFFFFF";

        public static readonly bool EnableProcessInfoLog_VALUE = false;
        public static readonly bool EnableIOLog_VALUE = false;
        public static readonly bool EnableErrorLog_VALUE = true;
        public static readonly string LogsMeterIdsFilter_VALUE = string.Empty;
        public static readonly bool EnableMSNFilter_VALUE = true;

        public static readonly bool EnableLogs_VALUE = true;
        public static readonly bool EnableLogsBuffer_VALUE = true;
        public static readonly bool EnableWriteToConsole_VALUE = false;
        public static readonly bool EnableWriteToTextLog_VALUE = true;
        public static readonly bool EnableWriteToEventLog_VALUE = false;
        public static readonly bool EnableWriteToUDPLog_VALUE = false;

        public static readonly string LogsDirectory_VALUE = "";
        public static readonly float LogsFileSize_VALUE = -1;
        public static readonly int LogsFileCount_VALUE = -1;
        public static readonly string LoggerBroadcastIPAddress_VALUE = "255.255.255.255";
        public static readonly int LoggerPort_VALUE = 8009;

        #endregion

        #region DLMS_COSEM_LIB_DEFAULTS

        public static readonly TimeSpan ApplicationDataCacheMinAge_VALUE = TimeSpan.FromMinutes(5.0f);
        public static readonly TimeSpan ApplicationDataCacheMaxAge_VALUE = TimeSpan.FromMinutes(15.0f);

        #endregion

        #region HDLC_LIB_DEFAULTS

        public static readonly byte HDLCAddressLength_VALUE = 4;
        public static readonly ushort MaxInformationBufferTransmit_VALUE = 128;
        public static readonly ushort MaxInformationBufferReceive_VALUE = 128;
        public static readonly ushort WinSizeTransmit_VALUE = 1;
        public static readonly ushort WinSizeReceive_VALUE = 1;
        public static readonly ushort DeviceAddress_VALUE = 17;
        public static readonly TimeSpan RequestResponseTimeOut_VALUE = TimeSpan.FromSeconds(10.0d);
        public static readonly TimeSpan InActivityTimeOut_VALUE = TimeSpan.FromSeconds(20.0d);
        public static readonly bool IsKeepAliveEnable_VALUE = true;
        public static readonly bool IsEnableRetrySend_VALUE = true;
        public static readonly bool IsSkipLoginParameter_VALUE = true;

        #endregion

        #region TCPWindowsParams_Default

        // public static readonly string TcpTimedWaitDelayVal = "120";
        // public static readonly string TcpNumConnectionsVal = "16000000";
        // public static readonly string MaxHashTableSizeVal = "65530";
        // public static readonly string MaxUserPortVal = "1024";
        // public static readonly string MaxFreeTcbsVal = "1000";

        #endregion

        #endregion

        private Configuration local_Config = null;

        #region Member_Properties
        public Configuration Local_Config
        {
            get { return local_Config; }
            set { local_Config = value; }
        }
        #endregion

        #region Member_Functions

        public static Configuration Load_ExternalConfig()
        {
            try
            {
                ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
                fileMap.ExeConfigFilename = AssembllyConfigURL;
                // relative path names possible

                // Open another configuration file
                Configuration Config =
                   ConfigurationManager.OpenMappedExeConfiguration(fileMap,
                   ConfigurationUserLevel.None);

                // ExeConfigurationFileMap ConfilMap = new ExeConfigurationFileMap(AssembllyConfigURL);
                // Configuration Config = ConfigurationManager.OpenMappedMachineConfiguration(ConfilMap,ConfigurationUserLevel.None);
                // ConfigurationSection Sec = Config.GetSection("appSettings");

                // SET DEFAULT KEY VALUES
                #region Database_Manager_LIB Configurations

                if (Config.AppSettings.Settings[DSN_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(DSN_KEY, DSN_VALUE));
                if (Config.AppSettings.Settings[PoolSize_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(PoolSize_KEY, PoolSize_VALUE.ToString()));
                if (Config.AppSettings.Settings[ConnectionLifeTime_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(ConnectionLifeTime_KEY, ConnectionLifeTime_VALUE.ToString()));

                if (Config.AppSettings.Settings[ConfigDirectory_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(ConfigDirectory_KEY, ConfigDirectory_VALUE));

                if (Config.AppSettings.Settings[IsEnableWeeklyLoadProfile_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(IsEnableWeeklyLoadProfile_KEY,
                        IsEnableWeeklyLoadProfile_VALUE.ToString()));
                if (Config.AppSettings.Settings[IsEnableWeeklyInstantaneous_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(IsEnableWeeklyInstantaneous_KEY,
                        IsEnableWeeklyInstantaneous_VALUE.ToString()));

                #endregion

                #region Communicator Configuration

                if (Config.AppSettings.Settings[ServerIP_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(ServerIP_KEY,
                        ServerIP_VALUE));
                if (Config.AppSettings.Settings[ServerPort_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(ServerPort_KEY,
                        ServerPort_VALUE.ToString()));
                if (Config.AppSettings.Settings[ServerInstanceName_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(ServerInstanceName_KEY,
                        ServerInstanceName_VALUE.ToString()));
                if (Config.AppSettings.Settings[MaxTCPIPConnection_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(MaxTCPIPConnection_KEY,
                        MaxTCPIPConnection_VALUE.ToString()));
                if (Config.AppSettings.Settings[MaxConcurrentMeterConnection_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(MaxConcurrentMeterConnection_KEY,
                        MaxConcurrentMeterConnection_VALUE.ToString()));
                if (Config.AppSettings.Settings[MinConcurrentMeterConnection_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(MinConcurrentMeterConnection_KEY,
                        MinConcurrentMeterConnection_VALUE.ToString()));
                if (Config.AppSettings.Settings[MaxConcurrentKeepAliveMeterConnection_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(MaxConcurrentKeepAliveMeterConnection_KEY,
                        MaxConcurrentKeepAliveMeterConnection_VALUE.ToString()));
                if (Config.AppSettings.Settings[MaxWorkerThreadPoolSize_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(MaxWorkerThreadPoolSize_KEY,
                        MaxWorkerThreadPoolSize_VALUE.ToString()));
                if (Config.AppSettings.Settings[MaxIOThreadPoolSize_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(MaxIOThreadPoolSize_KEY,
                        MaxIOThreadPoolSize_VALUE.ToString()));
                if (Config.AppSettings.Settings[TCPTimeOut_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(TCPTimeOut_KEY, TCPTimeOut_VALUE.ToString()));
                if (Config.AppSettings.Settings[TCPInactivityTimeOut_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(TCPInactivityTimeOut_KEY,
                        TCPInactivityTimeOut_VALUE.ToString()));
                if (Config.AppSettings.Settings[IsEnableTCPInactivityTimeOut_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(IsEnableTCPInactivityTimeOut_KEY,
                        IsEnableTCPInactivityTimeOut_VALUE.ToString()));
                if (Config.AppSettings.Settings[KeepAliveSchedulerPollingTime_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(KeepAliveSchedulerPollingTime_KEY,
                        KeepAliveSchedulerPollingTime_VALUE.ToString()));
                if (Config.AppSettings.Settings[IsEnableKeepAliveScheduler_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(IsEnableKeepAliveScheduler_KEY,
                        IsEnableKeepAliveScheduler_VALUE.ToString()));
                if (Config.AppSettings.Settings[IsEnableSaveErrorLogToDB_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(IsEnableSaveErrorLogToDB_KEY,
                        IsEnableSaveErrorLogToDB_VALUE.ToString()));
                if (Config.AppSettings.Settings[IsEnableInvalidTimeSync_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(IsEnableInvalidTimeSync_KEY,
                        IsEnableInvalidTimeSync_VALUE.ToString()));
                //------------------------------------------Enable Log/Statistics/Status of MDC Settings
                if (Config.AppSettings.Settings[SaveMDCSession_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(SaveMDCSession_KEY,
                        SaveMDCSession_VALUE.ToString()));
                if (Config.AppSettings.Settings[SaveMDCStatus_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(SaveMDCStatus_KEY,
                        SaveMDCStatus_VALUE.ToString()));
                if (Config.AppSettings.Settings[SaveStatistics_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(SaveStatistics_KEY,
                        SaveStatistics_VALUE.ToString()));
                if (Config.AppSettings.Settings[SaveLogToDB_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(SaveLogToDB_KEY,
                        SaveLogToDB_VALUE.ToString()));
                if (Config.AppSettings.Settings[PermissionToOnContactor_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(PermissionToOnContactor_KEY,
                        PermissionToOnContactor_VALUE.ToString()));
                if (Config.AppSettings.Settings[PermissionToOffContactor_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(PermissionToOffContactor_KEY,
                        PermissionToOffContactor_VALUE.ToString()));
                if (Config.AppSettings.Settings[PermissionToSyncTimeOnBatteryDead_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(PermissionToSyncTimeOnBatteryDead_KEY,
                        PermissionToTimeSyncOnBatteryDead_VALUE.ToString()));
                if (Config.AppSettings.Settings[PermissionToWriteParam_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(PermissionToWriteParam_KEY,
                        PermissionToWriteParam_VALUE.ToString()));
                //------------------------------------------Debugger & Logger Option
                if (Config.AppSettings.Settings[EnableProcessInfoLog_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(EnableProcessInfoLog_KEY, EnableProcessInfoLog_VALUE.ToString()));

                if (Config.AppSettings.Settings[EnableIOLog_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(EnableIOLog_KEY, EnableIOLog_VALUE.ToString()));

                if (Config.AppSettings.Settings[EnableErrorLog_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(EnableErrorLog_KEY, EnableErrorLog_VALUE.ToString()));

                if (Config.AppSettings.Settings[LogsMeterIdsFilter_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(LogsMeterIdsFilter_KEY, LogsMeterIdsFilter_VALUE));

                if (Config.AppSettings.Settings[EnableMSNFilter_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(EnableMSNFilter_KEY, EnableMSNFilter_VALUE.ToString()));

                if (Config.AppSettings.Settings[EnableLogs_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(EnableLogs_KEY, EnableLogs_VALUE.ToString()));

                if (Config.AppSettings.Settings[EnableLogsBuffer_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(EnableLogsBuffer_KEY, EnableLogsBuffer_VALUE.ToString()));

                if (Config.AppSettings.Settings[EnableWriteToConsole_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(EnableWriteToConsole_KEY, EnableWriteToConsole_VALUE.ToString()));

                if (Config.AppSettings.Settings[EnableWriteToTextLog_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(EnableWriteToTextLog_KEY, EnableWriteToTextLog_VALUE.ToString()));

                if (Config.AppSettings.Settings[EnableWriteToEventLog_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(EnableWriteToEventLog_KEY, EnableWriteToEventLog_VALUE.ToString()));

                if (Config.AppSettings.Settings[EnableWriteToUDPLog_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(EnableWriteToUDPLog_KEY, EnableWriteToUDPLog_VALUE.ToString()));


                if (Config.AppSettings.Settings[LogsDirectory_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(LogsDirectory_KEY, LogsDirectory_VALUE.ToString()));

                if (Config.AppSettings.Settings[LogsFileSize_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(LogsFileSize_KEY, LogsFileSize_VALUE.ToString()));

                if (Config.AppSettings.Settings[LogsFileCount_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(LogsFileCount_KEY, LogsFileCount_VALUE.ToString()));


                if (Config.AppSettings.Settings[LoggerBroadcastIPAddress_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(LoggerBroadcastIPAddress_KEY, LoggerBroadcastIPAddress_VALUE.ToString()));

                if (Config.AppSettings.Settings[LoggerPort_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(LoggerPort_KEY, LoggerPort_VALUE.ToString()));


                #endregion

                #region DLMS_COSEM Configuration

                if (Config.AppSettings.Settings[ApplicationDataCacheMinAge_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(ApplicationDataCacheMinAge_KEY,
                        ApplicationDataCacheMinAge_VALUE.ToString()));
                if (Config.AppSettings.Settings[ApplicationDataCacheMaxAge_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(ApplicationDataCacheMaxAge_KEY,
                        ApplicationDataCacheMaxAge_VALUE.ToString()));

                #endregion

                #region HDLC Configuration

                if (Config.AppSettings.Settings[HDLCAddressLength_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(HDLCAddressLength_KEY,
                                                    HDLCAddressLength_VALUE.ToString()));
                if (Config.AppSettings.Settings[MaxInformationBufferTransmit_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(MaxInformationBufferTransmit_KEY,
                                                    MaxInformationBufferTransmit_VALUE.ToString()));
                if (Config.AppSettings.Settings[MaxInformationBufferReceive_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(MaxInformationBufferReceive_KEY,
                                                    MaxInformationBufferReceive_VALUE.ToString()));
                if (Config.AppSettings.Settings[WinSizeTransmit_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(WinSizeTransmit_KEY,
                                                    WinSizeTransmit_VALUE.ToString()));
                if (Config.AppSettings.Settings[WinSizeReceive_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(WinSizeReceive_KEY,
                                                    WinSizeReceive_VALUE.ToString()));
                if (Config.AppSettings.Settings[DeviceAddress_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(DeviceAddress_KEY,
                                                    DeviceAddress_VALUE.ToString()));
                if (Config.AppSettings.Settings[InActivityTimeOut_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(InActivityTimeOut_KEY,
                                                    InActivityTimeOut_VALUE.ToString()));
                if (Config.AppSettings.Settings[IsKeepAliveEnable_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(IsKeepAliveEnable_KEY,
                                                    IsKeepAliveEnable_VALUE.ToString()));
                if (Config.AppSettings.Settings[IsEnableRetrySend_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(IsEnableRetrySend_KEY,
                                                    IsEnableRetrySend_VALUE.ToString()));
                if (Config.AppSettings.Settings[IsSkipLoginParameter_KEY] == null)
                    Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(IsSkipLoginParameter_KEY,
                                                    IsSkipLoginParameter_VALUE.ToString()));

                #endregion

                return Config;
            }
            catch
            {
                try
                {
                    ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
                    fileMap.ExeConfigFilename = AssembllyConfigURL;
                    // relative path names possible
                    // Open Another Configuration file
                    Configuration Config =
                       ConfigurationManager.OpenMappedExeConfiguration(fileMap,
                       ConfigurationUserLevel.None);
                    Config.GetSection("appSettings");
                    #region Database_Manager_LIB Configurations

                    if (Config.AppSettings.Settings[DSN_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(DSN_KEY, DSN_VALUE));
                    if (Config.AppSettings.Settings[PoolSize_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(PoolSize_KEY, PoolSize_VALUE.ToString()));
                    if (Config.AppSettings.Settings[ConnectionLifeTime_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(ConnectionLifeTime_KEY, ConnectionLifeTime_VALUE.ToString()));

                    if (Config.AppSettings.Settings[ConfigDirectory_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(ConfigDirectory_KEY, ConfigDirectory_VALUE));

                    #endregion
                    #region Communicator Configuration

                    if (Config.AppSettings.Settings[ServerIP_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(ServerIP_KEY,
                            ServerIP_VALUE));
                    if (Config.AppSettings.Settings[ServerPort_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(ServerPort_KEY,
                            ServerPort_VALUE.ToString()));
                    if (Config.AppSettings.Settings[ServerInstanceName_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(ServerInstanceName_KEY,
                            ServerInstanceName_VALUE.ToString()));
                    if (Config.AppSettings.Settings[MaxTCPIPConnection_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(MaxTCPIPConnection_KEY,
                            MaxTCPIPConnection_VALUE.ToString()));
                    ///ConfigurationManager.RefreshSection("appSettings"); 
                    if (Config.AppSettings.Settings[MaxConcurrentMeterConnection_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(MaxConcurrentMeterConnection_KEY,
                            MaxConcurrentMeterConnection_VALUE.ToString()));
                    if (Config.AppSettings.Settings[MinConcurrentMeterConnection_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(MinConcurrentMeterConnection_KEY,
                            MinConcurrentMeterConnection_VALUE.ToString()));
                    if (Config.AppSettings.Settings[TCPTimeOut_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(TCPTimeOut_KEY, TCPTimeOut_VALUE.ToString()));
                    if (Config.AppSettings.Settings[TCPInactivityTimeOut_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(TCPInactivityTimeOut_KEY,
                            TCPInactivityTimeOut_VALUE.ToString()));
                    if (Config.AppSettings.Settings[IsEnableTCPInactivityTimeOut_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(IsEnableTCPInactivityTimeOut_KEY,
                            IsEnableTCPInactivityTimeOut_VALUE.ToString()));
                    if (Config.AppSettings.Settings[KeepAliveSchedulerPollingTime_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(KeepAliveSchedulerPollingTime_KEY,
                            KeepAliveSchedulerPollingTime_VALUE.ToString()));
                    if (Config.AppSettings.Settings[IsEnableKeepAliveScheduler_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(IsEnableKeepAliveScheduler_KEY,
                            IsEnableKeepAliveScheduler_VALUE.ToString()));
                    if (Config.AppSettings.Settings[IsEnableSaveErrorLogToDB_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(IsEnableSaveErrorLogToDB_KEY,
                            IsEnableSaveErrorLogToDB_VALUE.ToString()));
                    //-------------------------------------------------------------------------------------
                    // Enable Log/Statistics/Status of MDC Settings

                    if (Config.AppSettings.Settings[SaveMDCSession_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(SaveMDCSession_KEY,
                            SaveMDCSession_VALUE.ToString()));
                    if (Config.AppSettings.Settings[SaveMDCStatus_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(SaveMDCStatus_KEY,
                            SaveMDCStatus_VALUE.ToString()));
                    if (Config.AppSettings.Settings[SaveStatistics_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(SaveStatistics_KEY,
                            SaveStatistics_VALUE.ToString()));
                    if (Config.AppSettings.Settings[SaveLogToDB_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(SaveLogToDB_KEY,
                            SaveLogToDB_VALUE.ToString()));
                    if (Config.AppSettings.Settings[PermissionToOnContactor_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(PermissionToOnContactor_KEY,
                            PermissionToOnContactor_VALUE.ToString()));
                    if (Config.AppSettings.Settings[PermissionToOffContactor_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(PermissionToOffContactor_KEY,
                            PermissionToOffContactor_VALUE.ToString()));

                    //------------------------------------------Debugger & Logger Option
                    if (Config.AppSettings.Settings[EnableProcessInfoLog_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(EnableProcessInfoLog_KEY, EnableProcessInfoLog_VALUE.ToString()));

                    if (Config.AppSettings.Settings[EnableIOLog_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(EnableIOLog_KEY, EnableIOLog_VALUE.ToString()));

                    if (Config.AppSettings.Settings[EnableErrorLog_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(EnableErrorLog_KEY, EnableErrorLog_VALUE.ToString()));

                    if (Config.AppSettings.Settings[EnableLogs_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(EnableLogs_KEY, EnableLogs_VALUE.ToString()));

                    if (Config.AppSettings.Settings[EnableLogsBuffer_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(EnableLogsBuffer_KEY, EnableLogsBuffer_VALUE.ToString()));

                    if (Config.AppSettings.Settings[EnableWriteToConsole_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(EnableWriteToConsole_KEY, EnableWriteToConsole_VALUE.ToString()));

                    if (Config.AppSettings.Settings[EnableWriteToTextLog_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(EnableWriteToTextLog_KEY, EnableWriteToTextLog_VALUE.ToString()));

                    if (Config.AppSettings.Settings[EnableWriteToEventLog_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(EnableWriteToEventLog_KEY, EnableWriteToEventLog_VALUE.ToString()));

                    if (Config.AppSettings.Settings[EnableWriteToUDPLog_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(EnableWriteToUDPLog_KEY, EnableWriteToUDPLog_VALUE.ToString()));

                    if (Config.AppSettings.Settings[LogsDirectory_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(LogsDirectory_KEY, LogsDirectory_VALUE.ToString()));

                    if (Config.AppSettings.Settings[LogsFileSize_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(LogsFileSize_KEY, LogsFileSize_VALUE.ToString()));

                    if (Config.AppSettings.Settings[LogsFileCount_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(LogsFileCount_KEY, LogsFileCount_VALUE.ToString()));


                    if (Config.AppSettings.Settings[LoggerBroadcastIPAddress_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(LoggerBroadcastIPAddress_KEY, LoggerBroadcastIPAddress_VALUE.ToString()));

                    if (Config.AppSettings.Settings[LoggerPort_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(LoggerPort_KEY, LoggerPort_VALUE.ToString()));

                    #endregion
                    #region DLMS_COSEM Configuration

                    if (Config.AppSettings.Settings[ApplicationDataCacheMinAge_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(ApplicationDataCacheMinAge_KEY,
                                                        ApplicationDataCacheMinAge_VALUE.ToString()));
                    if (Config.AppSettings.Settings[ApplicationDataCacheMaxAge_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(ApplicationDataCacheMaxAge_KEY,
                                                        ApplicationDataCacheMaxAge_VALUE.ToString()));

                    #endregion
                    #region HDLC Configuration

                    if (Config.AppSettings.Settings[HDLCAddressLength_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(HDLCAddressLength_KEY,
                                                        HDLCAddressLength_VALUE.ToString()));
                    if (Config.AppSettings.Settings[MaxInformationBufferTransmit_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(MaxInformationBufferTransmit_KEY,
                                                        MaxInformationBufferTransmit_VALUE.ToString()));
                    if (Config.AppSettings.Settings[MaxInformationBufferReceive_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(MaxInformationBufferReceive_KEY,
                                                        MaxInformationBufferReceive_VALUE.ToString()));
                    if (Config.AppSettings.Settings[WinSizeTransmit_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(WinSizeTransmit_KEY,
                                                        WinSizeTransmit_VALUE.ToString()));
                    if (Config.AppSettings.Settings[WinSizeReceive_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(WinSizeReceive_KEY,
                                                        WinSizeReceive_VALUE.ToString()));
                    if (Config.AppSettings.Settings[DeviceAddress_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(DeviceAddress_KEY,
                                                        DeviceAddress_VALUE.ToString()));
                    if (Config.AppSettings.Settings[InActivityTimeOut_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(InActivityTimeOut_KEY,
                                                        InActivityTimeOut_VALUE.ToString()));
                    if (Config.AppSettings.Settings[IsKeepAliveEnable_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(IsKeepAliveEnable_KEY,
                                                        IsKeepAliveEnable_VALUE.ToString()));
                    if (Config.AppSettings.Settings[IsEnableRetrySend_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(IsEnableRetrySend_KEY,
                                                        IsEnableRetrySend_VALUE.ToString()));
                    if (Config.AppSettings.Settings[IsSkipLoginParameter_KEY] == null)
                        Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(IsSkipLoginParameter_KEY,
                                                        IsSkipLoginParameter_VALUE.ToString()));

                    #endregion

                    return Config;
                }
                catch (Exception)
                {
                }
            }
            return null;
        }

        public static void Save_Configuration(Configuration local_Config)
        {
            try
            {
                local_Config.Save(ConfigurationSaveMode.Modified);
                // Force a reload of the changed section,  
                // if needed. This makes the new values available  
                // for reading.
                ConfigurationManager.RefreshSection(SectionName);

                // Get the AppSettings section.
                AppSettingsSection appSettingSection =
                  (AppSettingsSection)local_Config.GetSection(SectionName);

            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while saving application configuration", ex);
            }
        }

        #endregion

        #region Database_Project_Configurations

        public string MDC_DSN
        {
            get
            {
                try
                {
                    var val = local_Config.AppSettings.Settings[DSN_KEY].Value;
                    if (val == null || String.IsNullOrEmpty(val))
                    {
                        ///throw new Exception("Error occurred while loading MDC DSN Configuration");
                        val = DSN_VALUE;
                    }
                    return Convert.ToString(val);
                }
                catch
                {
                    throw new Exception("Error occurred while loading MDC DSN Configuration");
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(DSN_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(DSN_KEY, value));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while saving MDC DSN Configuration", ex);
                }
            }
        }

        public int ConnectionPoolSize
        {
            get
            {
                try
                {
                    var val = local_Config.AppSettings.Settings[PoolSize_KEY].Value;
                    ///Init Default Size
                    int size = PoolSize_VALUE;
                    if (val == null || !int.TryParse(val, out size) || size < 0 || size > 5000)
                    {
                        size = PoolSize_VALUE;
                        ///throw new Exception("Error occurred while loading Thread Pool Size Configuration");
                    }
                    return size;
                }
                catch
                {
                    throw new Exception("Error occurred while loading Thread Pool Size Configuration");
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(PoolSize_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(PoolSize_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while saving Thread Pool Size Configuration", ex);
                }
            }
        }

        public int ConnectionResetTime
        {
            get
            {
                try
                {
                    var val = local_Config.AppSettings.Settings[ConnectionLifeTime_KEY].Value;
                    ///Init Default Size
                    int size = ConnectionLifeTime_VALUE;
                    if (val == null || !int.TryParse(val, out size) || size < 0 || size > 1000)
                    {
                        size = ConnectionLifeTime_VALUE;
                        ///throw new Exception("Error occurred while loading ConnectionReset Time Configuration");
                    }
                    return size;
                }
                catch
                {
                    throw new Exception("Error occurred while loading ConnectionReset Time Configuration");
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(ConnectionLifeTime_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(ConnectionLifeTime_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while saving Thread Pool Size Configuration", ex);
                }
            }
        }

        public bool IsEnableWeeklyLoadProfile
        {
            get
            {
                try
                {
                    bool IsEnableWeeklyLoadProfile = IsEnableWeeklyLoadProfile_VALUE;
                    var val = local_Config.AppSettings.Settings[IsEnableWeeklyLoadProfile_KEY].Value;
                    ///Validate Value Read from Configurations
                    if (val == null || !bool.TryParse(val, out IsEnableWeeklyLoadProfile))
                    {
                        IsEnableWeeklyLoadProfile = IsEnableWeeklyLoadProfile_VALUE;
                        ///throw new Exception("Error occurred while loading IsEnable Save Error Logs to Database Configuration");
                    } return IsEnableWeeklyLoadProfile;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading IsEnable Weekly Load Profile Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(IsEnableWeeklyLoadProfile_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(IsEnableWeeklyLoadProfile_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving IsEnable Weekly Load Profile Configuration", ex);
                }
            }
        }

        public bool IsEnableWeeklyInstantanous
        {
            get
            {
                try
                {
                    bool IsEnableWeeklyInstantanous = IsEnableWeeklyInstantaneous_VALUE;
                    var val = local_Config.AppSettings.Settings[IsEnableWeeklyInstantaneous_KEY].Value;
                    ///Validate Value Read from Configurations
                    if (val == null || !bool.TryParse(val, out IsEnableWeeklyInstantanous))
                    {
                        IsEnableWeeklyInstantanous = IsEnableWeeklyInstantaneous_VALUE;
                        ///throw new Exception("Error occurred while loading IsEnable Save Error Logs to Database Configuration");
                    } return IsEnableWeeklyInstantanous;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading IsEnable Weekly Instantaneous Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(IsEnableWeeklyInstantaneous_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(IsEnableWeeklyInstantaneous_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving IsEnable Weekly Instantaneous Configuration", ex);
                }
            }
        }

        #endregion

        #region Common_Project_Configuration
        public string Applicatoin_Config_Directory
        {
            get
            {
                try
                {
                    var val = local_Config.AppSettings.Settings[ConfigDirectory_KEY].Value;
                    if (val == null || String.IsNullOrEmpty(val))
                    {
                        val = ConfigDirectory_VALUE;
                        ///throw new Exception("Error occurred while loading Application Configuration Directory");
                    }
                    return Convert.ToString(val);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading Application Configuration Directory", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(ConfigDirectory_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(ConfigDirectory_KEY, value));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while saving Application Configuration Directory", ex);
                }
            }
        }
        #endregion

        #region Communicator_Project_Configuration

        public string Server_IP
        {
            get
            {
                try
                {
                    var val = local_Config.AppSettings.Settings[ServerIP_KEY].Value;
                    ///Validate Server IP String
                    if (val == null || String.IsNullOrEmpty(val))
                    {
                        ///throw new Exception("Error occurred while loading Server IP Configuration");
                        val = ServerIP_VALUE;
                    }
                    return Convert.ToString(val);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading Server IP Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(ServerIP_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(ServerIP_KEY, value));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while saving Server IP Configuration", ex);
                }
            }
        }

        public int Server_Port
        {
            get
            {
                try
                {
                    int port = ServerPort_VALUE;
                    var val = local_Config.AppSettings.Settings[ServerPort_KEY].Value;
                    ///Validate Server Port Value
                    if (val == null || !int.TryParse(val, out port) || port < 0 || port > 65536)
                    {
                        ///throw new Exception("Error occurred while loading Server Port Configuration");
                        port = ServerPort_VALUE;
                    }
                    return port;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading Server IP Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(ServerPort_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(ServerPort_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while saving Server IP Configuration", ex);
                }
            }
        }

        public string ServerInstanceName
        {
            get
            {
                try
                {
                    var val = local_Config.AppSettings.Settings[ServerInstanceName_KEY].Value;
                    if (val == null || String.IsNullOrEmpty(val))
                    {
                        val = ServerInstanceName_VALUE;
                        ///throw new Exception("Error occurred while loading Server Instance Name  Configuration");
                    }
                    return Convert.ToString(val);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading Server Instance Name  Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(ServerInstanceName_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(ServerInstanceName_KEY, value));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while saving Server IP Configuration", ex);
                }
            }
        }

        public int MaxTCPIPConnection
        {
            get
            {
                try
                {
                    int Conn_Count = MaxTCPIPConnection_VALUE;
                    var val = local_Config.AppSettings.Settings[MaxTCPIPConnection_KEY].Value;
                    ///Validate Value Read from Configurations
                    if (val == null || !int.TryParse(val, out Conn_Count) || Conn_Count <= 0 || Conn_Count > 25000)
                    {
                        Conn_Count = MaxTCPIPConnection_VALUE;
                        ///throw new Exception("Error occurred while loading maximum TCPIP Connection Count Configuration");
                    }
                    return Conn_Count;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading Server IP Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(ServerPort_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(ServerPort_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while saving Server IP Configuration", ex);
                }
            }
        }

        public int MaxConcurrentMeterConnection
        {
            get
            {
                try
                {
                    int Conn_Count = MaxConcurrentMeterConnection_VALUE;
                    var val = local_Config.AppSettings.Settings[MaxConcurrentMeterConnection_KEY].Value;
                    ///Validate Value Read from Configurations
                    //  if (val == null || !int.TryParse(val, out Conn_Count) || Conn_Count <= 0 || Conn_Count > 2500)
                    if (val == null || !int.TryParse(val, out Conn_Count) || Conn_Count <= 0 || Conn_Count > 12000)
                    {
                        Conn_Count = MaxConcurrentMeterConnection_VALUE;
                        ///throw new Exception("Error occurred while loading maximum concurrent meter Connection Count Configuration");
                    }
                    return Conn_Count;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading maximum concurrent meter Connection Count Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(MaxConcurrentMeterConnection_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(MaxConcurrentMeterConnection_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading maximum concurrent meter Connection Count Configuration", ex);
                }
            }
        }

        public int MinConcurrentMeterConnection
        {
            get
            {
                try
                {
                    int Conn_Count = MinConcurrentMeterConnection_VALUE;
                    var val = local_Config.AppSettings.Settings[MinConcurrentMeterConnection_KEY].Value;
                    ///Validate Value Read from Configurations
                    //  if (val == null || !int.TryParse(val, out Conn_Count) || Conn_Count < 50 || Conn_Count > 2500)
                    if (val == null || !int.TryParse(val, out Conn_Count) || Conn_Count < 50 || Conn_Count > 4000)
                    {
                        Conn_Count = MinConcurrentMeterConnection_VALUE;
                        ///throw new Exception("Error occurred while loading minimum concurrent meter Connection Count Configuration");
                    }
                    return Conn_Count;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading minimum concurrent meter Connection Count Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(MinConcurrentMeterConnection_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(MinConcurrentMeterConnection_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading minimum concurrent meter Connection Count Configuration", ex);
                }
            }
        }

        public float MaxConcurrentKeepAliveMeterConnection
        {
            get
            {
                try
                {
                    float Conn_Count = MaxConcurrentKeepAliveMeterConnection_VALUE;
                    var val = local_Config.AppSettings.Settings[MaxConcurrentKeepAliveMeterConnection_KEY].Value;
                    ///Validate Value Read from Configurations
                    if (val == null || !float.TryParse(val, out Conn_Count) || Conn_Count < 0.0f || Conn_Count > 100.0f)
                    {
                        Conn_Count = MaxConcurrentKeepAliveMeterConnection_VALUE;
                        ///throw new Exception("Error occurred while loading maximum concurrent keep alive meter Connection Configuration");
                    }
                    return Conn_Count;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading maximum concurrent keep alive meter Connection Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(MaxConcurrentKeepAliveMeterConnection_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(MaxConcurrentKeepAliveMeterConnection_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading maximum concurrent Keep Alive meter Connection Count", ex);
                }
            }
        }

        public int MaxWorkerThreadPoolSize
        {
            get
            {
                try
                {
                    int Conn_Count = MaxWorkerThreadPoolSize_VALUE;
                    var val = local_Config.AppSettings.Settings[MaxWorkerThreadPoolSize_KEY].Value;
                    ///Validate Value Read from Configurations
                    if (val == null || !int.TryParse(val, out Conn_Count) || Conn_Count < 400 || Conn_Count > 12000)
                    {
                        Conn_Count = MaxWorkerThreadPoolSize_VALUE;
                        ///throw new Exception("Error occurred while loading maximum worker Thread Pool Size Configuration");
                    }
                    return Conn_Count;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading maximum worker Thread Pool Size Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(MaxWorkerThreadPoolSize_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(MaxWorkerThreadPoolSize_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while saving maximum worker Thread Pool Size Configuration", ex);
                }
            }
        }

        public int MaxIOThreadPoolSize
        {
            get
            {
                try
                {
                    int Conn_Count = MaxIOThreadPoolSize_VALUE;
                    var val = local_Config.AppSettings.Settings[MaxIOThreadPoolSize_KEY].Value;
                    ///Validate Value Read from Configurations
                    if (val == null || !int.TryParse(val, out Conn_Count) || Conn_Count < 400 || Conn_Count > 12000)
                    {
                        Conn_Count = MaxIOThreadPoolSize_VALUE;
                        ///throw new Exception("Error occurred while loading maximum IO Thread Pool Size Configuration");
                    }
                    return Conn_Count;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading maximum IO Thread Pool Size Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(MaxIOThreadPoolSize_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(MaxIOThreadPoolSize_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading maximum IO Thread Pool Size Configuration", ex);
                }
            }
        }

        public TimeSpan TCPTimeOut
        {
            get
            {
                try
                {
                    TimeSpan TCPTimeOut = TCPTimeOut_VALUE;
                    var val = local_Config.AppSettings.Settings[TCPTimeOut_KEY].Value;
                    ///Validate Value Read from Configurations
                    if (val == null || !TimeSpan.TryParse(val, out TCPTimeOut) || TCPTimeOut < TimeSpan.FromSeconds(20) || TCPTimeOut >= TimeSpan.FromMinutes(5))
                    {
                        TCPTimeOut = TCPTimeOut_VALUE;
                        ///throw new Exception("Error occurred while loading TCPTimeOut TCPIP Connection Configuration");
                    }
                    return TCPTimeOut;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading TCPTimeOut TCPIP Connection Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(TCPTimeOut_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(TCPTimeOut_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while saving TCPTimeOut TCPIP Connection Configuration", ex);
                }
            }
        }

        public TimeSpan TCPInactivityTimeOut
        {
            get
            {
                try
                {
                    TimeSpan TCPInactivityTimeOut = TCPInactivityTimeOut_VALUE;
                    var val = local_Config.AppSettings.Settings[TCPInactivityTimeOut_KEY].Value;
                    ///Validate Value Read from Configurations
                    if (val == null || !TimeSpan.TryParse(val, out TCPInactivityTimeOut) || TCPInactivityTimeOut < TimeSpan.FromMinutes(2.5)
                        || TCPInactivityTimeOut >= TimeSpan.FromMinutes(30))
                    {
                        TCPInactivityTimeOut = TCPInactivityTimeOut_VALUE;
                        ///throw new Exception("Error occurred while loading TCPInactivity TimeOut TCPIP Connection Configuration");
                    }
                    return TCPInactivityTimeOut;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading TCPInactivity TimeOut TCPIP Connection Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(TCPInactivityTimeOut_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(TCPInactivityTimeOut_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while saving TCPInactivity TimeOut TCPIP Connection Configuration", ex);
                }
            }
        }

        public bool IsEnableTCPInactivityTimeOut
        {
            get
            {
                try
                {
                    bool IsEnableTCPInactivityTimeOut = IsEnableTCPInactivityTimeOut_VALUE;
                    var val = local_Config.AppSettings.Settings[IsEnableTCPInactivityTimeOut_KEY].Value;
                    ///Validate Value Read from Configurations
                    if (val == null || !bool.TryParse(val, out IsEnableTCPInactivityTimeOut))
                    {
                        IsEnableTCPInactivityTimeOut = IsEnableTCPInactivityTimeOut_VALUE;
                        ///throw new Exception("Error occurred while loading IsEnableTCPInactivity TimeOut TCPIP Connection Configuration");
                    }
                    return IsEnableTCPInactivityTimeOut;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading IsEnableTCPInactivity TimeOut TCPIP Connection Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(IsEnableTCPInactivityTimeOut_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(IsEnableTCPInactivityTimeOut_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading IsEnableTCPInactivity TimeOut TCPIP Connection Configuration", ex);
                }
            }
        }

        public TimeSpan KeepAliveSchedulerPollingTime
        {
            get
            {
                try
                {
                    TimeSpan KeepAliveSchedulerPollingTime = KeepAliveSchedulerPollingTime_VALUE;
                    var val = local_Config.AppSettings.Settings[KeepAliveSchedulerPollingTime_KEY].Value;
                    ///Validate Value Read from Configurations
                    if (val == null || !TimeSpan.TryParse(val, out KeepAliveSchedulerPollingTime) || KeepAliveSchedulerPollingTime < TimeSpan.FromSeconds(30)
                        || KeepAliveSchedulerPollingTime >= TimeSpan.FromMinutes(15))
                    {
                        KeepAliveSchedulerPollingTime = KeepAliveSchedulerPollingTime_VALUE;
                        throw new Exception("Error occurred while loading KeepAlive Scheduler PollingTime Connection Configuration");
                    }
                    return KeepAliveSchedulerPollingTime;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading KeepAlive Scheduler PollingTime Connection Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(KeepAliveSchedulerPollingTime_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(KeepAliveSchedulerPollingTime_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while saving TCPInactivity TimeOut TCPIP Connection Configuration", ex);
                }
            }
        }

        public bool IsEnableKeepAliveScheduler
        {
            get
            {
                try
                {
                    bool IsEnableKeepAliveScheduler = IsEnableKeepAliveScheduler_VALUE;
                    var val = local_Config.AppSettings.Settings[IsEnableKeepAliveScheduler_KEY].Value;
                    ///Validate Value Read from Configurations
                    if (val == null || !bool.TryParse(val, out IsEnableKeepAliveScheduler))
                    {
                        IsEnableKeepAliveScheduler = IsEnableKeepAliveScheduler_VALUE;
                        ///throw new Exception("Error occurred while loading IsEnable KeepAlive Scheduler Connection Configuration");
                    }
                    return IsEnableKeepAliveScheduler;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading IsEnable KeepAlive Scheduler Connection Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(IsEnableKeepAliveScheduler_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(IsEnableKeepAliveScheduler_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading IsEnableTCPInactivity TimeOut TCPIP Connection Configuration", ex);
                }
            }
        }

        public bool IsEnableSaveErrorLogToDB
        {
            get
            {
                try
                {
                    bool IsEnableSaveErrorLogToDB = IsEnableSaveErrorLogToDB_VALUE;
                    var val = local_Config.AppSettings.Settings[IsEnableSaveErrorLogToDB_KEY].Value;
                    ///Validate Value Read from Configurations
                    if (val == null || !bool.TryParse(val, out IsEnableSaveErrorLogToDB))
                    {
                        IsEnableSaveErrorLogToDB = IsEnableSaveErrorLogToDB_VALUE;
                        ///throw new Exception("Error occurred while loading IsEnable Save Error Logs to Database Configuration");
                    } return IsEnableSaveErrorLogToDB;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading IsEnable Save Error Logs to Database Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(IsEnableSaveErrorLogToDB_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(IsEnableSaveErrorLogToDB_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while saving IsEnable Save Error Logs to Database Configuration", ex);
                }
            }
        }

        public bool IsEnableInvalidTimeSync
        {
            get
            {
                try
                {
                    bool IsEnableInvalidClockSync = IsEnableInvalidTimeSync_VALUE;
                    var val = local_Config.AppSettings.Settings[IsEnableInvalidTimeSync_KEY].Value;
                    ///Validate Value Read from Configurations
                    if (val == null || !bool.TryParse(val, out IsEnableInvalidClockSync))
                    {
                        IsEnableInvalidClockSync = IsEnableSaveErrorLogToDB_VALUE;
                        ///throw new Exception("Error occurred while loading IsEnable Save Error Logs to Database Configuration");
                    } return IsEnableInvalidClockSync;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading IsEnable Clock Sync Invalid Update Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(IsEnableInvalidTimeSync_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(IsEnableInvalidTimeSync_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving IsEnable Clock Sync Invalid Update Configuration", ex);
                }
            }
        }

        public bool SaveMDCStatus
        {
            get
            {
                try
                {
                    bool IsSaveMDCStatus = SaveMDCStatus_VALUE;
                    var val = local_Config.AppSettings.Settings[SaveMDCStatus_KEY].Value;
                    ///Validate Value Read from Configurations
                    if (val == null || !bool.TryParse(val, out IsSaveMDCStatus))
                    {
                        IsSaveMDCStatus = SaveMDCStatus_VALUE;
                        ///throw new Exception("Error occurred while loading IsEnable Save Error Logs to Database Configuration");
                    } return IsSaveMDCStatus;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading Save MDC Status Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(SaveMDCStatus_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(SaveMDCStatus_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving MDC Status Configuration", ex);
                }
            }
        }

        public bool SaveMDCSession
        {
            get
            {
                try
                {
                    bool IsSaveMDCSession = SaveMDCSession_VALUE;
                    var val = local_Config.AppSettings.Settings[SaveMDCSession_KEY].Value;
                    ///Validate Value Read from Configurations
                    if (val == null || !bool.TryParse(val, out IsSaveMDCSession))
                    {
                        IsSaveMDCSession = SaveMDCSession_VALUE;
                        ///throw new Exception("Error occurred while loading IsEnable Save Error Logs to Database Configuration");
                    } return IsSaveMDCSession;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading Save MDC Session Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(SaveMDCSession_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(SaveMDCSession_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving MDC Session Configuration", ex);
                }
            }
        }

        public bool EnableSaveStatistics
        {
            get
            {
                try
                {
                    bool IsSaveMDCStatistics = SaveStatistics_VALUE;
                    var val = local_Config.AppSettings.Settings[SaveStatistics_KEY].Value;
                    ///Validate Value Read from Configurations
                    if (val == null || !bool.TryParse(val, out IsSaveMDCStatistics))
                    {
                        IsSaveMDCStatistics = SaveStatistics_VALUE;
                        ///throw new Exception("Error occurred while loading IsEnable Save Error Logs to Database Configuration");
                    } return IsSaveMDCStatistics;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading Save MDC Statistics Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(SaveStatistics_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(SaveStatistics_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving MDC Statistics Configuration", ex);
                }
            }
        }

        public bool SaveLogToDB
        {
            get
            {
                try
                {
                    bool IsSaveLogTODB = SaveLogToDB_VALUE;
                    var val = local_Config.AppSettings.Settings[SaveLogToDB_KEY].Value;
                    ///Validate Value Read from Configurations
                    if (val == null || !bool.TryParse(val, out IsSaveLogTODB))
                    {
                        IsSaveLogTODB = SaveLogToDB_VALUE;
                        ///throw new Exception("Error occurred while loading IsEnable Save Error Logs to Database Configuration");
                    } return IsSaveLogTODB;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading Save MDC Log Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(SaveLogToDB_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(SaveLogToDB_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving MDC Log Configuration", ex);
                }
            }
        }

        public bool PermissionToONContactor
        {
            get
            {
                try
                {
                    bool GrantContatcorOnPermission = PermissionToOnContactor_VALUE;
                    var val = local_Config.AppSettings.Settings[PermissionToOnContactor_KEY].Value;
                    ///Validate Value Read from Configurations
                    if (val == null || !bool.TryParse(val, out GrantContatcorOnPermission))
                    {
                        GrantContatcorOnPermission = PermissionToOnContactor_VALUE;
                        ///throw new Exception("Error occurred while loading IsEnable Save Error Logs to Database Configuration");
                    } return GrantContatcorOnPermission;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading Save Contactor On Permission Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(PermissionToOnContactor_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(PermissionToOnContactor_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving Contactor On Permission Configuration", ex);
                }
            }
        }

        public bool PermissionToOFFContactor
        {
            get
            {
                try
                {
                    bool GrantContatcorOFFPermission = PermissionToOffContactor_VALUE;
                    var val = local_Config.AppSettings.Settings[PermissionToOffContactor_KEY].Value;
                    ///Validate Value Read from Configurations
                    if (val == null || !bool.TryParse(val, out GrantContatcorOFFPermission))
                    {
                        GrantContatcorOFFPermission = PermissionToOffContactor_VALUE;
                        ///throw new Exception("Error occurred while loading IsEnable Save Error Logs to Database Configuration");
                    } return GrantContatcorOFFPermission;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading Save Contactor OFF Permission Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(PermissionToOffContactor_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(PermissionToOffContactor_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving Contactor OFF Permission Configuration", ex);
                }
            }
        }

        public bool PermissionToTimeSyncOnBatteryDead
        {
            get
            {
                try
                {
                    bool PermissionToTimeSyncOnBattryDead = PermissionToTimeSyncOnBatteryDead_VALUE;
                    var val = local_Config.AppSettings.Settings[PermissionToSyncTimeOnBatteryDead_KEY].Value;
                    ///Validate Value Read from Configurations
                    if (val == null || !bool.TryParse(val, out PermissionToTimeSyncOnBattryDead))
                    {
                        PermissionToTimeSyncOnBattryDead = PermissionToTimeSyncOnBatteryDead_VALUE;
                        ///throw new Exception("Error occurred while loading IsEnable Save Error Logs to Database Configuration");
                    } return PermissionToTimeSyncOnBattryDead;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading Time Sync On Battery Dead Permission Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(PermissionToSyncTimeOnBatteryDead_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(PermissionToSyncTimeOnBatteryDead_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving Time Sync On Battery Dead Permission Configuration", ex);
                }
            }
        }

        public bool CheckAccessRights
        {
            get
            {
                try
                {
                    bool CheckAccessRights = CheckAccessRights_VALUE;
                    var val = local_Config.AppSettings.Settings[CheckAccessRights_KEY].Value;
                    // Validate Value Read from Configurations
                    if (val == null || !bool.TryParse(val, out CheckAccessRights))
                    {
                        CheckAccessRights = CheckAccessRights_VALUE;
                        // throw new Exception("Error occurred while loading CheckAccessRights Save Error Logs to Database Configuration");
                    } return CheckAccessRights;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading Check Access Rights Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(CheckAccessRights_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(CheckAccessRights_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving Check Access Rights Configuration", ex);
                }
            }
        }

        public bool EnableProcessInfoLog
        {
            get
            {
                try
                {
                    bool _enableProcessInfoLog = EnableProcessInfoLog_VALUE;
                    var val = local_Config.AppSettings.Settings[EnableProcessInfoLog_KEY].Value;
                    // Validate Value Read from Configurations
                    if (val == null || !bool.TryParse(val, out _enableProcessInfoLog))
                    {
                        _enableProcessInfoLog = EnableProcessInfoLog_VALUE;
                        // throw new Exception("Error occurred while loading EnableProcessInfoLog Save Error Logs to Database Configuration");
                    }
                    return _enableProcessInfoLog;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading Enable Process Info Log Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(EnableProcessInfoLog_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(EnableProcessInfoLog_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving Enable Process Info Log Configuration", ex);
                }
            }
        }

        public bool EnableIOLog
        {
            get
            {
                try
                {
                    bool _enableIOLog = EnableIOLog_VALUE;
                    var val = local_Config.AppSettings.Settings[EnableIOLog_KEY].Value;
                    // Validate Value Read from Configurations
                    if (val == null || !bool.TryParse(val, out _enableIOLog))
                    {
                        _enableIOLog = EnableIOLog_VALUE;
                        // throw new Exception("Error occurred while loading/Save EnableIOLog Logs to Database Configuration");
                    }
                    return _enableIOLog;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading Enable Process Info Log Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(EnableIOLog_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(EnableIOLog_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving Enable IO Log Configuration", ex);
                }
            }
        }

        public bool EnableErrorLog
        {
            get
            {
                try
                {
                    bool _enableErrorLog = EnableErrorLog_VALUE;
                    var val = local_Config.AppSettings.Settings[EnableErrorLog_KEY].Value;
                    // Validate Value Read from Configurations
                    if (val == null || !bool.TryParse(val, out _enableErrorLog))
                    {
                        _enableErrorLog = EnableErrorLog_VALUE;
                        // throw new Exception("Error occurred while loading/Save Enable ErrorLog to Database Configuration");
                    }
                    return _enableErrorLog;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading Enable Error Log Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(EnableErrorLog_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(EnableErrorLog_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving Enable Error Log Configuration", ex);
                }
            }
        }

        public bool EnableMSNFilter
        {
            get
            {
                try
                {
                    bool _enableMSNFilter = EnableMSNFilter_VALUE;
                    var val = local_Config.AppSettings.Settings[EnableMSNFilter_KEY].Value;
                    // Validate Value Read from Configurations
                    if (val == null || !bool.TryParse(val, out _enableMSNFilter))
                    {
                        _enableMSNFilter = EnableMSNFilter_VALUE;
                        // throw new Exception("Error occurred while loading/Save Enable ErrorLog to Database Configuration");
                    }
                    return _enableMSNFilter;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading Enable MSN Filter Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(EnableMSNFilter_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(EnableMSNFilter_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving Enable MSN Filter Configuration", ex);
                }
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
                    string _LogsMeterIdsFilter = LogsMeterIdsFilter_VALUE;
                    var val = local_Config.AppSettings.Settings[LogsMeterIdsFilter_KEY].Value;

                    try
                    {
                        _MSNFilter = Commons.ConvertSTRToMSNList(val);

                        // IsvalueProcessed
                        isValueProcessed = _MSNFilter != null && _MSNFilter.Count > 0;
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
                    string MSN_STR = Commons.ConvertMSNListToSTR(value);

                    // Validate 
                    if (value == null || value.Count <= 0 ||
                        string.IsNullOrEmpty(MSN_STR))
                        return;

                    local_Config.AppSettings.Settings.Remove(LogsMeterIdsFilter_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(LogsMeterIdsFilter_KEY, MSN_STR));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while saving MSN Filter Configuration", ex);
                }
            }
        }


        public bool EnableLogs
        {
            get
            {
                try
                {
                    bool _enableLogs = EnableLogs_VALUE;
                    var val = local_Config.AppSettings.Settings[EnableLogs_KEY].Value;
                    // Validate Value Read from Configurations
                    if (val == null || !bool.TryParse(val, out _enableLogs))
                    {
                        _enableLogs = EnableLogs_VALUE;
                        // throw new Exception("Error occurred while loading/Save Enable Logs to Database Configuration");
                    }
                    return _enableLogs;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading Enable Error Log Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(EnableLogs_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(EnableLogs_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving Enable Error Log Configuration", ex);
                }
            }
        }

        public bool EnableLogsBuffer
        {
            get
            {
                try
                {
                    bool _enableLogsBuffer = EnableLogsBuffer_VALUE;
                    var val = local_Config.AppSettings.Settings[EnableLogsBuffer_KEY].Value;
                    // Validate Value Read from Configurations
                    if (val == null || !bool.TryParse(val, out _enableLogsBuffer))
                    {
                        _enableLogsBuffer = EnableLogsBuffer_VALUE;
                        // throw new Exception("Error occurred while loading/Save Enable Logs to Database Configuration");
                    }
                    return _enableLogsBuffer;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading Enable Error Log Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(EnableLogsBuffer_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(EnableLogsBuffer_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving Enable Logs Buffer Configuration", ex);
                }
            }
        }

        public bool EnableWriteToConsole
        {
            get
            {
                try
                {
                    bool _enableWriteToConsole = EnableWriteToConsole_VALUE;
                    var val = local_Config.AppSettings.Settings[EnableWriteToConsole_KEY].Value;
                    // Validate Value Read from Configurations
                    if (val == null || !bool.TryParse(val, out _enableWriteToConsole))
                    {
                        _enableWriteToConsole = EnableWriteToConsole_VALUE;
                        // throw new Exception("Error occurred while loading/Save Enable Write To Console Configuration");
                    }
                    return _enableWriteToConsole;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading Enable Write To Console Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(EnableWriteToConsole_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(EnableWriteToConsole_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving Enable Write To Console Configuration", ex);
                }
            }
        }

        public bool EnableWriteToTextLog
        {
            get
            {
                try
                {
                    bool _enableWriteToTextLog = EnableWriteToTextLog_VALUE;
                    var val = local_Config.AppSettings.Settings[EnableWriteToTextLog_KEY].Value;
                    // Validate Value Read from Configurations
                    if (val == null || !bool.TryParse(val, out _enableWriteToTextLog))
                    {
                        _enableWriteToTextLog = EnableWriteToTextLog_VALUE;
                        // throw new Exception("Error occurred while loading/Save Enable Write To TextLog Database Configuration");
                    }
                    return _enableWriteToTextLog;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading Enable Write To TextLog Log Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(EnableWriteToTextLog_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(EnableWriteToTextLog_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving Enable Write To TextLog Configuration", ex);
                }
            }
        }

        public bool EnableWriteToEventLog
        {
            get
            {
                try
                {
                    bool _enableWriteToEventLog = EnableWriteToEventLog_VALUE;
                    var val = local_Config.AppSettings.Settings[EnableWriteToEventLog_KEY].Value;
                    // Validate Value Read from Configurations
                    if (val == null || !bool.TryParse(val, out _enableWriteToEventLog))
                    {
                        _enableWriteToEventLog = EnableWriteToEventLog_VALUE;
                        // throw new Exception("Error occurred while loading/Save Enable Write To Event Log Database Configuration");
                    }
                    return _enableWriteToEventLog;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading Enable Write To Event Log Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(EnableWriteToEventLog_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(EnableWriteToEventLog_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving Enable Write To Event Log Configuration", ex);
                }
            }
        }


        public bool EnableWriteToUDPLog
        {
            get
            {
                try
                {
                    bool _enableWriteToUDPLog = EnableWriteToUDPLog_VALUE;
                    var val = local_Config.AppSettings.Settings[EnableWriteToUDPLog_KEY].Value;
                    // Validate Value Read from Configurations
                    if (val == null || !bool.TryParse(val, out _enableWriteToUDPLog))
                    {
                        _enableWriteToUDPLog = EnableWriteToUDPLog_VALUE;
                        // throw new Exception("Error occurred while loading/Save Enable Write To UDP Log Database Configuration");
                    }
                    return _enableWriteToUDPLog;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading Enable Write To UDP Log Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(EnableWriteToUDPLog_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(EnableWriteToUDPLog_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving Enable Write To UDP Log Configuration", ex);
                }
            }
        }

        public string LogsDirectory
        {
            get
            {
                try
                {
                    string _logsDirectoryInfo = string.Empty;
                    var val = local_Config.AppSettings.Settings[LogsDirectory_KEY].Value;
                    bool isPathCorrect = false;

                    // Validate Director Path
                    try
                    {
                        var dirInfo = new DirectoryInfo(val);

                        if (dirInfo.Parent == null || string.IsNullOrEmpty(dirInfo.Parent.ToString())) // || 
                        //string.CompareOrdinal(dirInfo.Parent.FullName,dirInfo.Root.FullName) == 0)
                        {
                            val = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\" + dirInfo.Name;
                        }

                        string path = dirInfo.FullName;
                        isPathCorrect = true;
                    }
                    catch
                    {
                        isPathCorrect = false;
                    }

                    // Validate Value Read from Configurations
                    if (string.IsNullOrEmpty(val) || !isPathCorrect)
                    {
                        _logsDirectoryInfo = System.IO.Path.GetDirectoryName(Application.ExecutablePath);

                        // _logsDirectoryInfo = Directory.GetCurrentDirectory();
                        // throw new Exception("Error occurred while loading/Save Enable Write To Event Log Database Configuration");
                    }
                    else
                        _logsDirectoryInfo = val;

                    return _logsDirectoryInfo;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading Logs Directory Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(LogsDirectory_KEY);

                    bool isPathCorrect = false;
                    // Validate Directory Path
                    try
                    {
                        var dirInfo = new DirectoryInfo(value);
                        isPathCorrect = true;
                    }
                    catch
                    {
                        isPathCorrect = false;
                    }

                    // Update Only Validate Path
                    if (isPathCorrect)
                        local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(LogsDirectory_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving Logs Directory Configuration", ex);
                }
            }
        }

        public float LogsFileSize
        {
            get
            {
                try
                {
                    float _logsFileSize = -1;
                    var val = local_Config.AppSettings.Settings[LogsFileSize_KEY].Value;
                    // Validate Value Read from Configurations
                    if (val == null || !float.TryParse(val, out _logsFileSize))
                    {
                        _logsFileSize = -1.0f;
                        // throw new Exception("Error occurred while loading/save Logs File Size Configuration");
                    }
                    return _logsFileSize;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading Logs File Size Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(LogsFileSize_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(LogsFileSize_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving Logs File Size Configuration", ex);
                }
            }
        }

        public int LogsFileCount
        {
            get
            {
                try
                {
                    int _logsFileCount = -1;
                    var val = local_Config.AppSettings.Settings[LogsFileCount_KEY].Value;
                    // Validate Value Read from Configurations
                    if (val == null || !int.TryParse(val, out _logsFileCount))
                    {
                        _logsFileCount = -1;
                        // throw new Exception("Error occurred while Loading/Save Logs File Count Directory Configuration");
                    }
                    return _logsFileCount;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading Logs File Count Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(LogsFileCount_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(LogsFileCount_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving Logs File Count Configuration", ex);
                }
            }
        }

        public IPAddress LoggerBroadcastIPAddress
        {
            get
            {
                try
                {
                    // Default BroadCast 255.255.255.255
                    IPAddress _loggerBroadcastIPAddress = IPAddress.Broadcast;
                    var val = local_Config.AppSettings.Settings[LoggerBroadcastIPAddress_KEY].Value;

                    // Validate Value Read from Configurations
                    if (val == null || !IPAddress.TryParse(val, out _loggerBroadcastIPAddress))
                    {
                        _loggerBroadcastIPAddress = IPAddress.Broadcast;
                        // throw new Exception("Error occurred while Loading/Save Logger BroadCast IP Address Directory Configuration");
                    }
                    return _loggerBroadcastIPAddress;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading BroadCast IP Address Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(LoggerBroadcastIPAddress_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(LoggerBroadcastIPAddress_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving BroadCast IP Address in Configuration", ex);
                }
            }
        }

        public int LoggerPort
        {
            get
            {
                try
                {
                    // Default BroadCast Port 8009
                    int _LoggerPort = LoggerPort_VALUE;
                    var val = local_Config.AppSettings.Settings[LoggerPort_KEY].Value;

                    // Validate Value Read from Configurations
                    if (val == null || !int.TryParse(val, out _LoggerPort))
                    {
                        _LoggerPort = LoggerPort_VALUE;
                        // throw new Exception("Error occurred while Loading/Save Logger Port Address Directory Configuration");
                    }
                    return _LoggerPort;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading BroadCast Port Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(LoggerPort_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(LoggerPort_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while saving BroadCast Port Configuration", ex);
                }
            }
        }

        public string WriteParamString
        {
            get
            {
                try
                {
                    var val = local_Config.AppSettings.Settings[PermissionToWriteParam_KEY].Value;
                    // Validate Value Read from Configure
                    if (val == null || string.IsNullOrEmpty(val))
                    {
                        val = PermissionToWriteParam_VALUE;
                        // throw new Exception("Error occurred while loading IsEnable Save Error Logs to Database Configuration");
                    }
                    return val;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading Write Parameters Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(PermissionToWriteParam_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(PermissionToWriteParam_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving Write Parameters Configuration", ex);
                }
            }
        }



        #endregion

        #region DLMS_COSEM_LIB_Project_Configurations

        public TimeSpan ApplicationDataCacheMinAge
        {
            get
            {
                try
                {
                    TimeSpan ApplicationDataCacheMinAge = ApplicationDataCacheMinAge_VALUE;
                    var val = local_Config.AppSettings.Settings[ApplicationDataCacheMinAge_KEY].Value;
                    ///Validate Value Read from Configurations
                    if (val == null || !TimeSpan.TryParse(val, out ApplicationDataCacheMinAge) || ApplicationDataCacheMinAge < TimeSpan.FromMinutes(2.0)
                        || ApplicationDataCacheMinAge >= TimeSpan.FromMinutes(60))
                    {
                        ApplicationDataCacheMinAge = ApplicationDataCacheMinAge_VALUE;
                        ///throw new Exception("Error occurred while loading Application DataCache MinAge Configuration");
                    }
                    return ApplicationDataCacheMinAge;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading Application DataCache MinAge Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(ApplicationDataCacheMinAge_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(ApplicationDataCacheMinAge_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while saving Application DataCache MinAge Configuration", ex);
                }
            }
        }

        public TimeSpan ApplicationDataCacheMaxAge
        {
            get
            {
                try
                {
                    TimeSpan ApplicationDataCacheMaxAge = ApplicationDataCacheMaxAge_VALUE;
                    var val = local_Config.AppSettings.Settings[ApplicationDataCacheMaxAge_KEY].Value;
                    ///Validate Value Read from Configurations
                    if (val == null || !TimeSpan.TryParse(val, out ApplicationDataCacheMaxAge) || ApplicationDataCacheMinAge < TimeSpan.FromMinutes(2.0)
                        || ApplicationDataCacheMinAge >= TimeSpan.FromMinutes(60))
                    {
                        ApplicationDataCacheMaxAge = ApplicationDataCacheMaxAge_VALUE;
                        ///throw new Exception("Error occurred while loading Application DataCache MaxAge Configuration");
                    }
                    return ApplicationDataCacheMaxAge;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading Application DataCache MaxAge Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(ApplicationDataCacheMaxAge_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(ApplicationDataCacheMaxAge_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while saving Application DataCache MaxAge Configuration", ex);
                }
            }
        }

        #endregion

        #region HDLC_LIB_Project_Configurations

        public ushort HDLCAddressLength
        {
            get
            {
                try
                {
                    var val = local_Config.AppSettings.Settings[HDLCAddressLength_KEY].Value;
                    // Initial Default Size
                    ushort size = HDLCAddressLength_VALUE;
                    if (val == null || !ushort.TryParse(val, out size) || size < 0 || size > ushort.MaxValue)
                    {
                        size = HDLCAddressLength_VALUE;
                        // throw new Exception("Error occurred while loading HDLC Address Length Configuration");
                    }

                    return size;
                }
                catch
                {
                    throw new Exception("Error occurred while loading HDLC Address Configuration");
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(HDLCAddressLength_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(HDLCAddressLength_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while saving HDLC Address Configuration", ex);
                }
            }
        }

        public ushort MaxInfoBufferTransmit
        {
            get
            {
                try
                {
                    var val = local_Config.AppSettings.Settings[MaxInformationBufferTransmit_KEY].Value;
                    // Initial Default Size
                    ushort size = MaxInformationBufferTransmit_VALUE;
                    if (val == null || !ushort.TryParse(val, out size) || size < 0 || size > ushort.MaxValue)
                    {
                        size = MaxInformationBufferTransmit_VALUE;
                        // throw new Exception("Error occurred while loading HDLC Address Length Configuration");
                    }

                    return size;
                }
                catch
                {
                    throw new Exception("Error occurred while loading Max Info Buffer Transmit Configuration");
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(MaxInformationBufferTransmit_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(MaxInformationBufferTransmit_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while saving Max Info Buffer Transmit Configuration", ex);
                }
            }
        }

        public ushort MaxInfoBufferReceive
        {
            get
            {
                try
                {
                    var val = local_Config.AppSettings.Settings[MaxInformationBufferReceive_KEY].Value;
                    // Initial Default Size
                    ushort size = MaxInformationBufferReceive_VALUE;
                    if (val == null || !ushort.TryParse(val, out size) || size < 0 || size > ushort.MaxValue)
                    {
                        size = MaxInformationBufferReceive_VALUE;
                        // throw new Exception("Error occurred while loading HDLC Address Length Configuration");
                    }

                    return size;
                }
                catch
                {
                    throw new Exception("Error occurred while Loading Max Info Buffer Receive Configuration");
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(MaxInformationBufferReceive_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(MaxInformationBufferReceive_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving Max Info Buffer Receive Configuration", ex);
                }
            }
        }

        public ushort WindowSizeTransmit
        {
            get
            {
                try
                {
                    var val = local_Config.AppSettings.Settings[WinSizeTransmit_KEY].Value;
                    // Initial Default Size
                    ushort size = WinSizeTransmit_VALUE;
                    if (val == null || !ushort.TryParse(val, out size) || size < 0 || size > 7)
                    {
                        size = WinSizeTransmit_VALUE;
                        // throw new Exception("Error occurred while loading HDLC Address Length Configuration");
                    }

                    return size;
                }
                catch
                {
                    throw new Exception("Error occurred while Loading Window Size Transmit Configuration");
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(WinSizeTransmit_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(WinSizeTransmit_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving Window Size Transmit Configuration", ex);
                }
            }
        }

        public ushort WindowSizeReceive
        {
            get
            {
                try
                {
                    var val = local_Config.AppSettings.Settings[WinSizeReceive_KEY].Value;
                    // Initial Default Size
                    ushort size = WinSizeReceive_VALUE;
                    if (val == null || !ushort.TryParse(val, out size) || size < 0 || size > 7)
                    {
                        size = WinSizeReceive_VALUE;
                        // throw new Exception("Error occurred while loading HDLC Address Length Configuration");
                    }

                    return size;
                }
                catch
                {
                    throw new Exception("Error occurred while Loading Window Size Receive Configuration");
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(WinSizeReceive_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(WinSizeReceive_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving Window Size Receive Configuration", ex);
                }
            }
        }

        public ushort DeviceAddress
        {
            get
            {
                try
                {
                    var val = local_Config.AppSettings.Settings[DeviceAddress_KEY].Value;
                    // Initial Default Size
                    ushort size = DeviceAddress_VALUE;
                    if (val == null || !ushort.TryParse(val, out size) || size < 0 || size > ushort.MaxValue)
                    {
                        size = DeviceAddress_VALUE;
                        // throw new Exception("Error occurred while loading HDLC Address Length Configuration");
                    }

                    return size;
                }
                catch
                {
                    throw new Exception("Error occurred while Loading Device Address Configuration");
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(DeviceAddress_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(DeviceAddress_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving Device Address Configuration", ex);
                }
            }
        }

        public TimeSpan RequestResponseTimeOut
        {
            get
            {
                try
                {
                    TimeSpan TimeOutVal = RequestResponseTimeOut_VALUE;
                    var val = local_Config.AppSettings.Settings[RequestResponseTimeOut_KEY].Value;
                    // Validate Value Read from Configurations
                    if (val == null || !TimeSpan.TryParse(val, out TimeOutVal) || TimeOutVal < TimeSpan.FromSeconds(2.0)
                        || TimeOutVal >= TimeSpan.FromMinutes(15))
                    {
                        TimeOutVal = RequestResponseTimeOut_VALUE;
                        // throw new Exception("Error occurred while loading Application DataCache MaxAge Configuration");
                    }
                    return TimeOutVal;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading Application Request Response TimeOut Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(RequestResponseTimeOut_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(RequestResponseTimeOut_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while saving Request Response TimeOut Configuration", ex);
                }
            }
        }

        public TimeSpan InActivityTimeOut
        {
            get
            {
                try
                {
                    TimeSpan TimeOutVal = InActivityTimeOut_VALUE;
                    var val = local_Config.AppSettings.Settings[InActivityTimeOut_KEY].Value;
                    // Validate Value Read from Configurations
                    if (val == null || !TimeSpan.TryParse(val, out TimeOutVal) || TimeOutVal < TimeSpan.FromSeconds(2.0)
                        || TimeOutVal >= TimeSpan.FromMinutes(30))
                    {
                        TimeOutVal = InActivityTimeOut_VALUE;
                        // throw new Exception("Error occurred while loading Application DataCache MaxAge Configuration");
                    }
                    return TimeOutVal;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading Application HDLC InActivity TimeOut Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(InActivityTimeOut_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(InActivityTimeOut_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while saving HDLC InActivity TimeOut Configuration", ex);
                }
            }
        }

        public bool IsKeepAliveEnable
        {
            get
            {
                try
                {
                    var val = local_Config.AppSettings.Settings[IsKeepAliveEnable_KEY].Value;
                    bool temps;

                    if (val == null || !bool.TryParse(val, out temps))
                    {
                        temps = Convert.ToBoolean(IsKeepAliveEnable_VALUE);
                    }
                    return temps;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading IsKeepAliveEnable Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(IsKeepAliveEnable_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(IsKeepAliveEnable_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving IsKeepAliveEnable Configuration", ex);
                }
            }
        }

        public bool IsEnableRetrySend
        {
            get
            {
                try
                {
                    var val = local_Config.AppSettings.Settings[IsEnableRetrySend_KEY].Value;
                    bool temps;

                    if (val == null || !bool.TryParse(val, out temps))
                    {
                        temps = Convert.ToBoolean(IsEnableRetrySend_VALUE);
                    }
                    return temps;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading IsEnableRetrySend Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(IsEnableRetrySend_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(IsEnableRetrySend_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving IsEnableRetrySend Configuration", ex);
                }
            }
        }

        public bool IsSkipLoginParameter
        {
            get
            {
                try
                {
                    var val = local_Config.AppSettings.Settings[IsSkipLoginParameter_KEY].Value;
                    bool temps;

                    if (val == null || !bool.TryParse(val, out temps))
                    {
                        temps = Convert.ToBoolean(IsSkipLoginParameter_VALUE);
                    }
                    return temps;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while loading Is SkipLogin Parameter Configuration", ex);
                }
            }
            set
            {
                try
                {
                    local_Config.AppSettings.Settings.Remove(IsSkipLoginParameter_KEY);
                    local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(IsSkipLoginParameter_KEY, value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while Saving Is SkipLogin Parameter Configuration", ex);
                }
            }
        }

        #endregion

        //#region TCPWindowsSideConfigs

        //public int TcpTimedWaitDelay
        //{
        //    get {
        //        try
        //        {
        //            var val = local_Config.AppSettings.Settings[TcpTimedWaitDelayKey].Value;
        //            int temps;
        //            if (val == null || !int.TryParse(val, out temps) || temps < 30 || temps > 300)
        //            {
        //                temps = Convert.ToInt32(TcpTimedWaitDelayVal);
        //            }
        //            return temps;
        //        }
        //        catch (Exception ex)
        //        {

        //            throw new Exception("Error occurred while loading TcpTimedWaitDelay Configuration", ex);
        //        }
        //    }
        //    set
        //    {
        //        try
        //        {
        //            local_Config.AppSettings.Settings.Remove(TcpTimedWaitDelayKey);
        //            local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(TcpTimedWaitDelayKey, value.ToString()));
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error occurred while loading TcpTimedWaitDelay Configuration", ex);
        //        }
        //    }
        //}

        //public int TcpNumConnections
        //{
        //    get
        //    {
        //        try
        //        {
        //            var val = local_Config.AppSettings.Settings[TcpNumConnectionsKey].Value;
        //            int temps;
        //            if (val == null || !int.TryParse(val, out temps) || temps < 1000 || temps > 16777214)
        //            {
        //                temps = Convert.ToInt32(TcpNumConnectionsVal);
        //            }
        //            return temps;
        //        }
        //        catch (Exception ex)
        //        {

        //            throw new Exception("Error occurred while loading TcpNumConnections Configuration", ex);
        //        }
        //    }
        //    set
        //    {
        //        try
        //        {
        //            local_Config.AppSettings.Settings.Remove(TcpNumConnectionsKey);
        //            local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(TcpNumConnectionsKey, value.ToString()));
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error occurred while loading TcpNumConnections Configuration", ex);
        //        }
        //    }
        //}

        //public int MaxUserPort
        //{
        //    get
        //    {
        //        try
        //        {
        //            var val = local_Config.AppSettings.Settings[MaxUserPortKey].Value;
        //            int temps;
        //            if (val == null || !int.TryParse(val, out temps) || temps < 5000 || temps > 65534)
        //            {
        //                temps = Convert.ToInt32(MaxUserPortVal);
        //            }
        //            return temps;
        //        }
        //        catch (Exception ex)
        //        {

        //            throw new Exception("Error occurred while loading MaxUserPort Configuration", ex);
        //        }
        //    }
        //    set
        //    {
        //        try
        //        {
        //            local_Config.AppSettings.Settings.Remove(MaxUserPortKey);
        //            local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(MaxUserPortKey, value.ToString()));
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error occurred while loading MaxUserPort Configuration", ex);
        //        }
        //    }
        //}

        //public int MaxHashTableSize
        //{
        //    get
        //    {
        //        try
        //        {
        //            var val = local_Config.AppSettings.Settings[MaxHashTableSizeKey].Value;
        //            int temps;
        //            if (val == null || !int.TryParse(val, out temps) || temps < 65 || temps > 65536)
        //            {
        //                temps = Convert.ToInt32(MaxHashTableSizeVal);
        //            }
        //            return temps;
        //        }
        //        catch (Exception ex)
        //        {

        //            throw new Exception("Error occurred while loading MaxHashTableSize Configuration", ex);
        //        }
        //    }
        //    set
        //    {
        //        try
        //        {
        //            local_Config.AppSettings.Settings.Remove(MaxHashTableSizeKey);
        //            local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(MaxHashTableSizeKey, value.ToString()));
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error occurred while loading MaxHashTableSize Configuration", ex);
        //        }
        //    }
        //}

        //public int MaxFreeTcbs
        //{
        //    get
        //    {
        //        try
        //        {
        //            var val = local_Config.AppSettings.Settings[MaxFreeTcbsKey].Value;
        //            int temps;
        //            if (val == null || !int.TryParse(val, out temps) || temps < 500 || temps > 60000)
        //            {
        //                temps = Convert.ToInt32(MaxFreeTcbsVal);
        //            }
        //            return temps;
        //        }
        //        catch (Exception ex)
        //        {

        //            throw new Exception("Error occurred while loading MaxFreeTcbs Configuration", ex);
        //        }
        //    }
        //    set
        //    {
        //        try
        //        {
        //            local_Config.AppSettings.Settings.Remove(MaxFreeTcbsKey);
        //            local_Config.AppSettings.Settings.Add(new KeyValueConfigurationElement(MaxFreeTcbsKey, value.ToString()));
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error occurred while loading MaxFreeTcbs Configuration", ex);
        //        }
        //    }
        //}

        //#endregion
    }
}
