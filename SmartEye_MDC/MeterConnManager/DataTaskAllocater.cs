#define Enable_Abstract_Log
#define Enable_DEBUG_ECHO
// #define Enable_DEBUG_RunMode
#define Enable_Error_Logging
// #define Enable_Transactional_Logging
#define Enable_LoadTester_Mode

using comm;
using Communicator.Properties;
using DatabaseManager.Database;
using DLMS;
using DLMS.Comm;
using SharedCode.Comm.DataContainer;
using SharedCode.Comm.EventDispatcher.Contracts;
using SharedCode.Comm.HelperClasses;
using SharedCode.Common;
using SharedCode.TCP_Communication;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Communicator.MeterConnManager
{
    public interface IDataTaskAllocater
    {
        // Task ConnectionRunnableAsync(object Controller);
        void ConnectionRunnable(object Controller);
    }

    public class DataTaskAllocater : IDataTaskAllocater
    {
        public static readonly TimeSpan MaxThreadIdleSuspendTime = TimeSpan.FromSeconds(600);
        public static readonly float MAX_Failure_Count = 75.0f;

        // Major Alarm Handler Processing
        private LinkedList<IEvent> _cachedEvents;
        private Thread ASyncThread = null;
        private bool isThreadRunning = false;

        private DatabaseController _DBController;

        private Commuincator.MeterConnManager.MeterConnectionManager owner;
        private BitArray PermissionWriteParams;
        static int HeartBeatWaitTime = Settings.Default.HeartBeatWaitTime;

        public Commuincator.MeterConnManager.MeterConnectionManager Owner
        {
            get { return owner; }
            internal set { owner = value; }
        }

        public DataTaskAllocater(Commuincator.MeterConnManager.MeterConnectionManager Instance)
        {
            if (Instance == null)
                throw new NullReferenceException("Unable to initialize DataTaskAllocater,Null Initializer Passed");

            owner = Instance;

            _cachedEvents = new LinkedList<IEvent>();
            _DBController = new DatabaseController();

            try
            {
                PermissionWriteParams = Commons.GetSubscripotionArray(Settings.Default.PermissionParamsWrite);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Run Handler for particular meter
        /// </summary>
        /// <param name="Controller"></param>
        //  public async Task ConnectionRunnableAsync(object Controller)
        //  {
        //      return;
        //  }

        /// <summary>
        /// Run Handler for particular meter
        /// </summary>
        /// <param name="Controller"></param>
        public void ConnectionRunnable(object Controller)
        {
            #region Members

            ApplicationController Contr = null;
            DateTime T1 = DateTime.Now;
            IOConnection IOConn = null;
            MeterSerialNumber MeterSerialNumberObject = null;
            CustomException Cus_Exc = new CustomException();
            bool EnableGeneralLog = Settings.Default.EnableGeneralLog;
            bool MakeKeepAliveTransaction = false;
            MeterInformation Gateway_Device_Schedule = null;
            GatewayScanResult _gatewayScanResult = null;

            #endregion

            try
            {
                #region Configurations

                try
                {
                    // Code To Proceed
                    Contr = (ApplicationController)Controller;
                    Contr.ReInitApplicationController();
                    Contr.Applicationprocess_Controller.IsCompatibilityMode = true;
                    // Register Activity Logger Here
                    if (Owner != null &&
                        Owner.Activity_DataLogger != null)
                        Contr.ActivityLogger = Owner.Activity_DataLogger;

                    // Contr.LoadProfile_Controller.Configurator = Contr.Configurator;
                    // Contr.Event_Controller.Configurator = Contr.Configurator;
                    Contr.InstantaneousController.Configurator = Contr.Configurator;
                    Contr.Billing_Controller.Configurator = Contr.Configurator;
                    Contr.ConnectToMeter = Contr.ConnectionController.CurrentConnection;
                    Contr.PermissionToWriteParams = PermissionWriteParams;

                    // Contr.StatisticsObj = new Statistics();
                    // Contr.DB_Controller = new DatabaseController();
                    // Contr.Param_OBJ.DBController = Contr.DB_Controller;
                    // Contr.Events_OBJ.DBController = Contr.DB_Controller;

                    IOConn = Contr.ConnectionController.CurrentConnection;
                    Contr.GetAccessRightsDelegate = new GetSAPTable(Contr.GetAccessRights);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error While loading Configurations", ex);
                }

                #endregion

                #region Debugging & Logging
                #region Insert Incoming Connection To Log

#if Enable_Abstract_Log

                Contr.StatisticsObj.DP_Logger.InsertLog("IIP", IOConn.IOStream.ToString(), IOConn.ConnectionTime);
#endif

#if !Enable_Abstract_Log
               Contr.StatisticsObj.DP_Logger.InsertLog("Incoming Connection",IOConn.IOStream.ToString(), IOConn.ConnectionTime.AddSeconds(1));
			   // StatisticsObj.DP_Logger.InsertLog(string.Format("{0,21} ..... {1,10}: {2}", IOConn.ConnectionTime.AddSeconds(1), "Incoming Connection", IOConn.IOStream.ToString()),false);
#endif
                #endregion

                // Contr.StatisticsObj.InsertLog("Incoming Connection : " + IOConn.IOStream.ToString());

#if Enable_Transactional_Logging

                Contr.StatisticsObj.InsertLog_Temp("Incoming Connection : " + IOConn.IOStream.ToString());

#endif
                #endregion
                #region Cancel Request

                if (Contr.ThreadCancelToken != null &&
                    Contr.ThreadCancelToken.IsCancellationRequested)
                {
                    Contr.ThreadCancelToken.Token.ThrowIfCancellationRequested();
                }

                #endregion

                try
                {
                    MeterSerialNumber MSN_Old = IOConn.MeterSerialNumberObj;
                    Interlocked.Increment(ref Owner.ThreadAllocator.active_Count);

                    if (Settings.Default.OnRequestDelay > 0)
                        Contr.Applicationprocess_Controller.Write_Delay = TimeSpan.FromMilliseconds(Settings.Default.OnRequestDelay);
                    else
                        Contr.Applicationprocess_Controller.Write_Delay = TimeSpan.Zero;

                    // Delay For 3G Compatibility
                    #region OnConnectionDelay

                    if (Settings.Default.OnConnectionDelay > 0)
                        Commons.Delay(Settings.Default.OnConnectionDelay);

                    #endregion
                    #region WaitForHeartBeat

                    if (HeartBeatWaitTime > 0)
                    {
                        LogMessage(Contr, IOConn, "HB", "W");
                        Contr.ConnectToMeter.TCPWrapperStream.CommunicationMode = CommunicationMode.IdleAliveMode;
                        SpinWait.SpinUntil(() => { return Contr.ConnectToMeter.IsHeartBeatReceive; }, (int)HeartBeatWaitTime * 1000);

                        if (Contr.ConnectToMeter.IsHeartBeatReceive)
                        {
                            LogMessage(Contr, IOConn, "HB", "S");
                            //Contr.LogMessage(String.Format(" {0,10}\t{1,-8}{2,-2}", IOConn.MSN, "HB", "S"));
                            MeterSerialNumberObject = IOConn.MeterSerialNumberObj;
                        }
                        else
                        {
                            LogMessage(Contr, IOConn, "HB", "F");
                            //Contr.LogMessage(String.Format(" {0,10}\t{1,-8}{2,-2}", IOConn.MSN, "HB", "F"));
                        }
                    }

                    #endregion

                    // Register DLMS Data Logger
                    if (IOConn != null &&
                        Contr.Applicationprocess_Controller.Logger != null)
                    {
                        // Initialize DLMS Logger
                        DLMSLogger logger = Contr.Applicationprocess_Controller.Logger;
                        if (IOConn.MeterSerialNumberObj == null)
                            logger.Identifier = string.Empty;
                        else
                            logger.Identifier = IOConn.MeterSerialNumberObj.ToString();
                    }

                    if (IOConn.MeterSerialNumberObj == null ||
                       !IOConn.MeterSerialNumberObj.IsMSN_Valid)
                    {
                        #region public login

                        Contr.ConnectToMeter.TCPWrapperStream.CommunicationMode = CommunicationMode.ActiveIOSessionMode;
                        Contr.LoginMeterConnection(HLS_Mechanism.LowestSec, LLCProtocolType.TCP_Wrapper);

                        #endregion
                        #region MSN reading

                        #region Debugging & Logging
#if Enable_DEBUG_ECHO
                        #region Getting MSN
#if Enable_Abstract_Log

                        //var msg = String.Format("{0,-8}{1,-2}", "GM", "R");
                        //if (EnableGeneralLog)
                        LogMessage(Contr, IOConn, "GM", "R", EnableGeneralLog);
                        //Contr.LogMessage(String.Format(" {0,10}\t{1}", IOConn.MSN, msg));
                        // Contr.LogMessage(string.Format("{0,-16}{1}", "", msg));

#endif
#if !Enable_Abstract_Log
                                var msg = String.Format(" {0,10}\tGetting MSN", "");
                                if (EnableGeneralLog)
                                    Contr.LogMessage(msg);
                                IOConn.MeterLiveLog = "Getting MSN";
						        Contr.StatisticsObj.InsertLog("Getting MSN");
#endif
                        #endregion
#endif
                        #endregion
                        MeterSerialNumberObject = Contr.ConnectionController.GetMeterSerialNumber();
                        IOConn.MeterSerialNumberObj = MeterSerialNumberObject;
                        // Register DLMS Data Logger
                        if (Contr.Applicationprocess_Controller.Logger != null)
                        {
                            // Initialize DLMS Logger
                            DLMSLogger logger = Contr.Applicationprocess_Controller.Logger;
                            if (MeterSerialNumberObject == null)
                                logger.Identifier = string.Empty;
                            else
                                logger.Identifier = MeterSerialNumberObject.ToString();
                        }
                        // Register DLMS Data Logger
                        if (Contr.Applicationprocess_Controller.Logger != null)
                        {
                            // Initialize DLMS Logger
                            DLMSLogger logger = Contr.Applicationprocess_Controller.Logger;
                            if (MeterSerialNumberObject == null)
                                logger.Identifier = string.Empty;
                            else
                                logger.Identifier = MeterSerialNumberObject.ToString();
                        }

                        #region Debug & Logging
#if Enable_DEBUG_ECHO

                        #region MSN Read Successfully
#if Enable_Abstract_Log
                        LogMessage(Contr, IOConn, "GM", "S", EnableGeneralLog);
                        //msg = String.Format("{0,-8}{1,-2}", "GM", "S");
                        //if (EnableGeneralLog)
                        //    Contr.LogMessage(String.Format(" {0,10}\t{1}", MeterSerialNumberObject, msg));
                        //// Contr.LogMessage(string.Format("{0,-16}{1}", "", msg));
                        //Contr.StatisticsObj.InsertLog(msg);
                        //IOConn.MeterLiveLog = msg;

#endif
#if !Enable_Abstract_Log

                                msg = String.Format(" {0,10}\tMSN Read Successfully", MeterSerialNumberObject);
                                if (EnableGeneralLog)
                                    Contr.LogMessage(msg);
						        Contr.StatisticsObj.InsertLog("MSN Read Successfully");
                                    IOConn.MeterLiveLog ="MSN Read Successfully";
                              
#endif
                        #endregion
#endif
#if Enable_Transactional_Logging
                                if (EnableGeneralLog)
                                    Contr.StatisticsObj.InsertLog_Temp("MSN Reading Success");
#endif
                        #endregion

                        #endregion

                        if (Contr.ConnectionController.IsConnected)
                        {
                            #region Disconnect

                            try
                            {
                                #region Debugging & Logging
#if Enable_DEBUG_ECHO

                                #region Releasing Public Association
#if Enable_Abstract_Log
                                LogMessage(Contr, IOConn, "PBLO", "R", EnableGeneralLog);
                                //msg = String.Format("{0,-8}{1,-2}", "PBLO", "R");
                                //if (EnableGeneralLog)
                                //    Contr.LogMessage(String.Format(" {0,10}\t{1}", MeterSerialNumberObject, msg));
                                //// Contr.LogMessage(string.Format("{0,-16}{1}", "", msg));
                                //Contr.StatisticsObj.InsertLog(msg);
                                //IOConn.MeterLiveLog = msg;

#endif
#if !Enable_Abstract_Log
                                msg = String.Format(" {0,10}\tReleasing Public Association", Contr.MeterInfo.MSN);
                                if (EnableGeneralLog)
                                    Contr.LogMessage(msg);
                                Contr.StatisticsObj.InsertLog(msg);
                                IOConn.MeterLiveLog = msg;  
#endif
                                #endregion
#endif
                                #endregion
                                Contr.ConnectionController.Disconnect();
                                #region Debugging & Logging
#if Enable_DEBUG_ECHO

                                #region Public Association released Successfully
#if Enable_Abstract_Log
                                LogMessage(Contr, IOConn, "PBLO", "S", EnableGeneralLog);
                                //msg = String.Format("{0,-8}{1,-2}", "PBLO", "S");
                                //if (EnableGeneralLog)
                                //    Contr.LogMessage(String.Format(" {0,10}\t{1}", MeterSerialNumberObject, msg));
                                ////Contr.LogMessage(string.Format("{0,-16}{1}","",msg));
                                //Contr.StatisticsObj.InsertLog(msg);
                                //IOConn.MeterLiveLog = msg;
#endif

#if !Enable_Abstract_Log
                                var msg = String.Format(" {0,10}\tPublic Association released Successfully", Contr.MeterInfo.MSN);
                                if (EnableGeneralLog)
                                    Contr.LogMessage(msg);
                                Contr.StatisticsObj.InsertLog(msg);
                                IOConn.MeterLiveLog = msg; 
#endif
                                #endregion



#endif
#if Enable_Transactional_Logging
                                if (EnableGeneralLog)
                                    Contr.StatisticsObj.InsertLog_Temp("Public Association released Successfully");
#endif
                                #endregion
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(String.Format("Error while releasing public association"), ex);
                            }

                            #endregion
                        }
                    }

                    IOConn.MeterSerialNumberObj = MeterSerialNumberObject;

                    if ((MSN_Old == null ||
                         MSN_Old != MeterSerialNumberObject) &&
                        !Owner.ThreadAllocator.IsMeterConnectionAllocated(MeterSerialNumberObject))
                    {
                        // Update MSN (KEY,Value)
                        Owner.ThreadAllocator.TryUpdate_MSN_MeterConnection(Contr, MeterSerialNumberObject, MSN_Old);
                    }

                    #region // Read Meter Settings

                    try
                    {
                        IOConn.MeterSerialNumberObj = MeterSerialNumberObject;
                        Contr.MeterInfo = Contr.DB_Controller.GetMeterSettings(MeterSerialNumberObject.ToString());

                        IOConn.CurrentDeviceType = Contr.MeterInfo.DeviceTypeVal;
                    }
                    catch (Exception ex)
                    {
                        LogMessage(Contr, IOConn, "Meter Settings loading from Database", "F", EnableGeneralLog);
                        //if (EnableGeneralLog)
                        //    Contr.LogMessage(String.Format("Meter Settings loading from Database Failed! Reason:" +
                        //                     ex.Message, MeterSerialNumberObject));
                        throw ex;
#if Enable_Transactional_Logging

                            Contr.StatisticsObj.InsertLog_Temp(String.Format("Meter Settings loading from Database Failed", MeterSerialNumberObject));

#endif
                        //  Contr.LogMessage(String.Format(" {0,10}\t Error", ex.Message));
                    }

                    #endregion

                    #region Intruder_Check

                    if (Contr.MeterInfo.MeterType_OBJ.Equals(MeterType.Intruder))
                    {

                        #region Intruder Disconnect
                        try
                        {
                            Contr.DB_Controller.InsertIntruders(MeterSerialNumberObject.ToString(), IOConn.IOStream.ToString(), DateTime.Now);
                            Contr.ConnectionController.Disconnect();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(String.Format("Error while releasing public association"), ex);
                        }
                        #endregion

                        #region Debug & Logging
#if Enable_DEBUG_ECHO
                        if (EnableGeneralLog)
                            LogMessage(Contr, IOConn, "Intr", "DC");
#endif
#if Enable_Transactional_Logging
                                Contr.StatisticsObj.InsertLog_Temp(String.Format("Intruder {0} Disconnected", MeterSerialNumberObject));
#endif
                        #endregion

                        return;
                    }

                    #endregion
                    #region Check Status

                    if (!Contr.MeterInfo.Status)
                    {
                        Contr.LogMessage("Deactivated Meter {0} Disconnected", 0);
                        return;
                    }

                    #endregion
                }

                #region Try & Catch

                catch (Exception ex)
                {

                    Contr.StatisticsObj.InsertLog("~*Exception*~", true);
                    if (ex.TargetSite != null)
                        Contr.StatisticsObj.InsertLog("Method Name: " + ex.TargetSite.Name + " ~ ");
                    Contr.ExtractEachExceptionAndLog(ex, 2, Contr.StatisticsObj);
                    Contr.StatisticsObj.InsertLog("~*End-Exception*~");

                    throw ex;
                }
                finally
                {
                    Interlocked.Decrement(ref Owner.ThreadAllocator.active_Count);
                }

                #endregion

                #region Statistics 1

                if (IOConn.ConnectionInfo != null)
                {
                    try
                    {
                        Contr.StatisticsObj.MeterSerialNumber = IOConn.MSN;
                        Contr.StatisticsObj.ConnectionType = Contr.ConnectToMeter.CurrentConnection;
                        Contr.StatisticsObj.Max_Allocated_Count = Owner.ThreadAllocator.Allocated_Count;
                        Contr.StatisticsObj.Max_Active_Session_Count = Owner.ThreadAllocator.Active_Connection_Count;
                        Contr.StatisticsObj.DP_Logger.IsSaveToDb = Convert.ToBoolean(Settings.Default.SaveLogToDBFlag);
                        Contr.StatisticsObj.StartSessionDateTime = T1;
                    }
                    catch
                    { }
                }

                #endregion
                #region Application Logic

                if (IOConn.MeterSerialNumberObj != null)
                {
                    #region Meter Device Application Logic

                    if (IOConn.CurrentDeviceType == DeviceType.Not_Assigned ||
                        IOConn.CurrentDeviceType == DeviceType.MeterDevice ||
                        IOConn.CurrentDeviceType == DeviceType.eGenious)
                    {
                        int tmpBufferSize = 1024;
                        MeterSerialNumberObject = IOConn.MeterSerialNumberObj;

                        #region Initialize Meter Setting

                        IOConn.MeterSerialNumberObj = MeterSerialNumberObject;
                        InitializeMeterSetting(Contr, IOConn, EnableGeneralLog, false);

                        #endregion

                        // Reset Application Controller
                        Contr.ResetApplicationController();
                        if (IOConn.ConnectionInfo.ConnectionType == PhysicalConnectionType.NonKeepAlive)
                        {
                            #region Application logic for Non Keep Alive

                            try
                            {
                                if (!Contr.MeterInfo.IsDataRequestEmpty || !Contr.MeterInfo.IsParamEmpty)
                                {
                                    Interlocked.Increment(ref Owner.ThreadAllocator.active_Count);
                                    // Update Read\Write BufferLength Here 
                                    // For Each Meter BufferLength As Per Meter or MeterModel Based Settings
                                    IOConn.InitBuffer(tmpBufferSize, tmpBufferSize, Owner.CreateDataReaderBuffer);
                                    Contr.Applicationprocess_Controller.MaxLocalBuffer = tmpBufferSize;

                                    Cus_Exc = Contr.RunApplicationLogic();
                                    Interlocked.Decrement(ref Owner.ThreadAllocator.active_Count);
                                    if (Contr.MeterInfo != null && !Contr.MeterInfo.IsDataRequestEmpty)
                                        Interlocked.Increment(ref Owner.ThreadAllocator.MDC_Status_Obj.NKA_Expected_Transactions);
                                    // Execute Application Logic Here
                                    if (Cus_Exc.isTrue && Cus_Exc.Ex == null)
                                    {
                                        Interlocked.Increment(ref Owner.ThreadAllocator.MDC_Status_Obj.NKA_Successful_Transactions);
                                        Contr.LogMessage("*~*~* Request processed successfully for Non Keep Alive Meter *~*~*", "RNKA", "S", 0);

                                        Contr.StatisticsObj.IsSuccessful = true;
                                    }
                                    else if (!Cus_Exc.isTrue || Cus_Exc.Ex != null)
                                    {
                                        Contr.LogMessage("@-@-@ Request process failed for Non Keep Alive Meter @-@-@", "RNKA", "F", 0);
                                        if (Cus_Exc.Ex != null)
                                            throw Cus_Exc.Ex;
                                    }
                                }
                            }
                            catch (Exception exc)
                            {
                                throw exc;
                            }
                            finally
                            {
                                try
                                {
                                    #region Duration

                                    Contr.StatisticsObj.Duration = DateTime.Now.Subtract(T1);
                                    Contr.LogMessage(String.Format("Meter Connection Deallocated, Duration = {0}",
                                                     Contr.StatisticsObj.Duration), "CDD", Contr.StatisticsObj.Duration.ToString(), 0);

                                    if (Contr.ConnectToMeter != null &&
                                        Contr.ConnectToMeter.TCPWrapperStream != null)
                                    {
                                        LogMessage(Contr, IOConn, "BREC", Contr.ConnectToMeter.TCPWrapperStream.TotalBytesReceived.ToString());
                                        LogMessage(Contr, IOConn, "BSENT", Contr.ConnectToMeter.TCPWrapperStream.TotalBytesSent.ToString());

                                        //Contr.LogMessage(String.Format("Total Bytes Received: {0}",
                                        //  Contr.ConnectToMeter.TCPWrapperStream.TotalBytesReceived));
                                        //Contr.LogMessage(String.Format("Total Bytes Sent: {0}",
                                        //  Contr.ConnectToMeter.TCPWrapperStream.TotalBytesSent));
                                    }

                                    #endregion
                                    #region Statistics 2

                                    try
                                    {
                                        Contr.StatisticsObj.InsertLog(Contr.ConnectToMeter.TCPWrapperStream.TotalBytesSent,
                                            Contr.ConnectToMeter.TCPWrapperStream.TotalBytesReceived);
                                        Contr.StatisticsObj.DBController = Contr.DB_Controller;

                                        if (Contr.MeterInfo != null && Contr.MeterInfo.Save_ST && Settings.Default.SaveStatistics)
                                        {
                                            Contr.StatisticsObj.SaveStatictics();  // Done Statistics
                                        }

                                        #region Log and Error Log

                                        if (Contr.MeterInfo.EnableSaveLog)
                                        {
                                            if (Contr.StatisticsObj.DP_Logger.ErrorListCount > 0 && Settings.Default.SaveErrorToDB)
                                                Contr.StatisticsObj.SaveErrors();   // Done Statistics
                                            if (Contr.StatisticsObj.DP_Logger.LogListCount > 0)
                                                Contr.StatisticsObj.SaveLogging(Contr.StatisticsObj.Duration);   // Done Statistics
                                        }

                                        #endregion
                                        Contr.StatisticsObj.clearStatistics();
                                    }
                                    catch
                                    { }

                                    #endregion
                                }
                                catch
                                { }

                                // Reset Total Utilized Bandwidth
                                Contr.ConnectToMeter.TCPWrapperStream.TotalBytesReceived = 0;
                                Contr.ConnectToMeter.TCPWrapperStream.TotalBytesSent = 0;
                            }

                            #endregion
                        }
                        else if (IOConn.ConnectionInfo.ConnectionType == PhysicalConnectionType.KeepAlive)
                        {
                            #region Application logic for Keep Alive

                            DateTime LastCommunicationStart = Contr.MeterInfo.Kas_DueTime;
                            try
                            {
                                IOConn.DeInitBufferKeepAlive();
                                // Threshold of 20 seconds otherwise process as normal request
                                if (Contr.MeterInfo.Kas_DueTime > DateTime.Now.AddSeconds(20))
                                {
                                    TimeSpan TS = Contr.MeterInfo.Kas_DueTime.Subtract(DateTime.Now);
                                    Thread.Sleep(TS);
                                }

                                do
                                {
                                    if (MakeKeepAliveTransaction)
                                    {
                                        #region Statistics 1
                                        if (Contr.ConnectToMeter.ConnectionInfo != null)
                                        {
                                            Contr.StatisticsObj.MeterSerialNumber = Contr.ConnectToMeter.MSN;
                                            Contr.StatisticsObj.Max_Allocated_Count = Owner.ThreadAllocator.Allocated_Count;
                                            Contr.StatisticsObj.Max_Active_Session_Count = Owner.ThreadAllocator.Active_Connection_Count;
                                        }
                                        #endregion
                                        Security_Data Prev_Sec_Data = null;
                                        Security_Data _Sec_Data = null;
                                        Prev_Sec_Data = Contr.Applicationprocess_Controller.Security_Data;
                                        // Load Latest Security Data
                                        Contr.MeterInfo = Contr.DB_Controller.GetMeterSettings(IOConn.MSN);
                                        _Sec_Data = Contr.MeterInfo.ObjSecurityData;

                                        #region // Update Meter CurrentConnectionType

                                        if (Contr.MeterInfo.MeterType_OBJ != MeterType.KeepAlive)
                                        {
                                            IOConn.CurrentConnection = (Contr.MeterInfo.MeterType_OBJ == MeterType.KeepAlive) ?
                                            PhysicalConnectionType.KeepAlive : PhysicalConnectionType.NonKeepAlive;
                                            if (IOConn.CurrentConnection == PhysicalConnectionType.NonKeepAlive)
                                            {
                                                //Because Keep-Alive meter Type Change here
                                                Interlocked.Decrement(ref Owner.ThreadAllocator.KA_Alloc_Count);
                                            }
                                        }

                                        #endregion
                                        #region // Update Security Data

                                        if (Prev_Sec_Data != null)
                                        {
                                            if (Contr.MeterInfo.ObjSecurityData == null)
                                                Contr.MeterInfo.ObjSecurityData = Prev_Sec_Data;
                                            else
                                            {
                                                // Validate Security Data
                                                if (_Sec_Data.AuthenticationKey == null ||
                                                    _Sec_Data.AuthenticationKey.Value == null ||
                                                    _Sec_Data.AuthenticationKey.Value.Count <= 0)
                                                    _Sec_Data.AuthenticationKey = Prev_Sec_Data.AuthenticationKey;

                                                if (_Sec_Data.EncryptionKey == null ||
                                                    _Sec_Data.EncryptionKey.Value == null ||
                                                    _Sec_Data.EncryptionKey.Value.Count <= 0)
                                                    _Sec_Data.EncryptionKey = Prev_Sec_Data.EncryptionKey;

                                                if (_Sec_Data.SystemTitle == null ||
                                                    _Sec_Data.SystemTitle.Count <= 0)
                                                    _Sec_Data.SystemTitle = Prev_Sec_Data.SystemTitle;

                                                if (_Sec_Data.ServerSystemTitle == null ||
                                                    _Sec_Data.ServerSystemTitle.Count <= 0)
                                                    _Sec_Data.ServerSystemTitle = Prev_Sec_Data.ServerSystemTitle;

                                            }
                                        }

                                        Contr.Applicationprocess_Controller.Security_Data = Contr.MeterInfo.ObjSecurityData;

                                        #endregion


                                        Contr.MeterInfo.Kas_NextCallTime = Contr.MeterInfo.GetNextCallTimeForFixedInterval(Contr.MeterInfo.Kas_DueTime,
                                                                                 Contr.MeterInfo.Kas_Interval.TotalMinutes);
                                        T1 = DateTime.Now;
                                    }

                                    if (Contr.MeterInfo != null)
                                    {
                                        bool updateKasTime = false;

                                        try
                                        {
                                            if (!Contr.MeterInfo.IsDataRequestEmpty ||
                                                !Contr.MeterInfo.IsParamEmpty)
                                            {
                                                Interlocked.Increment(ref Owner.ThreadAllocator.ConditionHitCount);
                                                Interlocked.Increment(ref Owner.ThreadAllocator.active_Count);
                                                Interlocked.Increment(ref Owner.ThreadAllocator.KA_active_Count);

                                                if (Contr.MeterInfo.WakeUp_Request_ID != 0)
                                                    Contr.DB_Controller.UpdateWakeUpProcess(true, 1, Contr.MeterInfo.WakeUp_Request_ID);

                                                // Initial IOConnection Internal Buffers
                                                IOConn.InitBuffer(tmpBufferSize, tmpBufferSize, Owner.CreateDataReaderBuffer);
                                                Contr.Applicationprocess_Controller.MaxLocalBuffer = tmpBufferSize;

                                                // IOConn.InitBuffer(Owner.ConnectionManager.MaxReadBuffer, Owner.ConnectionManager.MaxWriteBuffer, Owner.CreateDataReaderBuffer);
                                                // Application Logic called from here
                                                Cus_Exc = Contr.RunApplicationLogic();
                                                // DeInit IOConnection Internal Buffers For KeepAliver Meter
                                                IOConn.DeInitBufferKeepAlive();
                                                Interlocked.Decrement(ref Owner.ThreadAllocator.active_Count);
                                                Interlocked.Decrement(ref Owner.ThreadAllocator.KA_active_Count);
                                                Interlocked.Increment(ref Owner.ThreadAllocator.MDC_Status_Obj.KA_Expected_Transactions);
                                            }
                                            else updateKasTime = true;

                                            if (Cus_Exc.isTrue && Cus_Exc.Ex == null)
                                            {
                                                Contr.LogMessage("*~*~* Request processed Successfully for Keep Alive Meter *~*~*", "RKA", "S", 0);
                                                Contr.MeterInfo.ProcessStatus = Process_Status.RequestProcessedSuccessfully;
                                                Contr.StatisticsObj.IsSuccessful = true;
                                                Interlocked.Increment(ref Owner.ThreadAllocator.MDC_Status_Obj.KA_Successful_Transactions);
                                            }
                                            else if (!Cus_Exc.isTrue || Cus_Exc.Ex != null)
                                            {
                                                Contr.LogMessage("@-@-@ Request Process failed for Keep Alive Meter @-@-@", "RKA", "F", 0);
                                                Contr.MeterInfo.ProcessStatus = Process_Status.RequestProccessedFailed;
                                                if (Cus_Exc.Ex != null)
                                                    throw Cus_Exc.Ex;
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
                                                #region Duration

                                                Contr.StatisticsObj.Duration = DateTime.Now.Subtract(T1);

                                                if (Contr.ConnectToMeter != null &&
                                                    Contr.ConnectToMeter.TCPWrapperStream != null)
                                                {
                                                    LogMessage(Contr, IOConn, "BREC", Contr.ConnectToMeter.TCPWrapperStream.TotalBytesReceived.ToString());
                                                    LogMessage(Contr, IOConn, "BSENT", Contr.ConnectToMeter.TCPWrapperStream.TotalBytesSent.ToString());

                                                    //                                                    Contr.LogMessage(String.Format("Total Bytes Received: {0}",
                                                    //                                                      Contr.ConnectToMeter.TCPWrapperStream.TotalBytesReceived));
                                                    //                                                Contr.LogMessage(String.Format("Total Bytes Sent: {0}",
                                                    //                                                  Contr.ConnectToMeter.TCPWrapperStream.TotalBytesSent));
                                                }

#if Enable_Transactional_Logging
                                            Contr.StatisticsObj.InsertLog("Duration:" + Contr.StatisticsObj.Duration.ToString());
#endif

                                                #endregion
                                                #region Statistics 2

                                                try
                                                {
                                                    Contr.StatisticsObj.InsertLog(Contr.ConnectToMeter.TCPWrapperStream.TotalBytesSent,
                                                                                  Contr.ConnectToMeter.TCPWrapperStream.TotalBytesReceived);
                                                    Contr.StatisticsObj.DBController = Contr.DB_Controller;
                                                    Contr.StatisticsObj.StartSessionDateTime = Contr.SessionDateTime;
                                                    if (Contr.MeterInfo != null &&
                                                        Contr.MeterInfo.Save_ST &&
                                                        Settings.Default.SaveStatistics)
                                                    {
                                                        Contr.StatisticsObj.SaveStatictics(); // Not Done - update Security Data
                                                    }

                                                    #region Log and Error Log

                                                    if (Contr.MeterInfo.EnableSaveLog)
                                                    {
                                                        if (Contr.StatisticsObj.DP_Logger.ErrorListCount > 0 &&
                                                            Settings.Default.SaveErrorToDB)
                                                            Contr.StatisticsObj.SaveErrors();
                                                        if (Contr.StatisticsObj.DP_Logger.LogListCount > 0)
                                                        {
                                                            // TODO:Save Keep-Alive Log On Request Process Success only
                                                            if (IOConn.CurrentConnection == PhysicalConnectionType.NonKeepAlive)
                                                                Contr.StatisticsObj.SaveLogging(Contr.StatisticsObj.Duration);
                                                            else
                                                            {
                                                                // if (Cus_Exc.isTrue)
                                                                Contr.StatisticsObj.SaveLogging(Contr.StatisticsObj.Duration);
                                                            }
                                                        }
                                                    }

                                                    #endregion
                                                    Contr.StatisticsObj.clearStatistics();
                                                }
                                                catch
                                                { }

                                                #endregion

                                                // Update into database and memory
                                                if (Contr.MeterInfo.ProcessStatus == Process_Status.RequestProcessedSuccessfully)
                                                {
                                                    // Contr.MeterInfo.Kas_DueTime = Contr.MeterInfo.Kas_NextCallTime;
                                                    Contr.MeterInfo.Kas_NextCallTime = Contr.MeterInfo.GetNextCallTimeForFixedInterval(Contr.MeterInfo.Kas_NextCallTime,
                                                                                                                                       Contr.MeterInfo.Kas_Interval.TotalMinutes);
                                                }

                                                // Reset Total Utilized Bandwidth
                                                Contr.ConnectToMeter.TCPWrapperStream.TotalBytesReceived = 0;
                                                Contr.ConnectToMeter.TCPWrapperStream.TotalBytesSent = 0;
                                            }
                                            catch
                                            { }
                                        }

                                        if (!Owner.ConnectionManager.TCPIPConnController_OBJ.TCPServer.IsShutDownInitiated &&
                                            !IOConn.CancelationRequest && IOConn.IsConnected)
                                        {
                                            LastCommunicationStart = Contr.MeterInfo.Kas_DueTime;
                                            TimeSpan CalculatedDelay = LastCommunicationStart.Subtract(DateTime.Now);

                                            if (CalculatedDelay > TimeSpan.FromSeconds(0))
                                                Thread.Sleep(CalculatedDelay);

                                            MakeKeepAliveTransaction = true &&
                                            (IOConn.CurrentConnection == PhysicalConnectionType.KeepAlive) &&
                                            !Owner.ConnectionManager.TCPIPConnController_OBJ.TCPServer.IsShutDownInitiated &&
                                            !IOConn.CancelationRequest && Contr.MeterInfo.Status;
                                        }
                                        else
                                            MakeKeepAliveTransaction = false;

                                        if (updateKasTime)
                                        {
                                            Contr.MeterInfo.Kas_DueTime = Contr.MeterInfo.Kas_NextCallTime;
                                            Contr.MIUF.KAS_DueTime = true;
                                            Contr.DB_Controller.UpdateMeterSettings(Contr.MeterInfo, Contr.MIUF);
                                            Contr.MIUF = new MeterInfoUpdateFlags();
                                        }
                                    }
                                } while (MakeKeepAliveTransaction);

                            }
                            catch (Exception ex)
                            {
                                throw new Exception("Error while Processing keep alive meter : " + Contr.MeterInfo.MSN, ex);
                            }

                            #endregion
                        }

                        if (Contr.MeterInfo.IsDataRequestEmpty &&
                            Contr.MeterInfo.IsParamEmpty)
                        {
                            if (!string.IsNullOrEmpty(Contr.MeterInfo.MSN) &&
                                !string.IsNullOrWhiteSpace(Contr.MeterInfo.MSN))
                            {
                                Contr.LogMessage("Request was Empty MSN: " + Contr.MeterInfo.MSN, "RQST", "EMPT");
                            }
                            else
                            {
                                Contr.LogMessage("Error Meter Info is NULL", "MI", "EMPT");
                            }

                            IOConn.Disconnect();
                        }
                    }

                    #endregion
                    #region // GateWay Device Application Logic

                    else if (IOConn.CurrentDeviceType == DeviceType.GateWay)
                    {
                        //int _totalFailureCount = 0;
                        //int _totoalSuccessCount = 0;
                        string ParentId = Contr.MeterInfo.MeterID.ToString();

                        DateTime LastCommunicationStart = DateTime.MinValue;
                        IOConn.GatewaySerialNumber = IOConn.MeterSerialNumberObj;
                        Gateway_Device_Schedule = null;
                        _gatewayScanResult = new GatewayScanResult();
                        MeterInfoBaseScheduler _scheduler = new SimpleMeterInfoScheduler();
                        //_scheduler.UpdateMeterInformation = new Func<MeterSerialNumber, MeterInformation>((msn) => Contr.DB_Controller.GetMeterSettings(msn.ToString(), true, Gateway_Device_Schedule.MeterID));

                        // Threshold of 20 seconds otherwise process as normal request
                        //if (Contr.MeterInfo.Kas_DueTime > DateTime.Now.AddSeconds(20))
                        //{
                        //    TimeSpan TS = Contr.MeterInfo.Kas_DueTime.Subtract(DateTime.Now);
                        //    Thread.Sleep(TS);
                        //}
                        RepeatLoop_GSN:
                        Contr.ResetApplicationController();
                        // Reset DB_Controller
                        if (Contr.DB_Controller.DBConnect != null)
                        {
                            Contr.DB_Controller.DBConnect.DisposeConnection();
                            Contr.DB_Controller.DBConnect = null;
                            Contr.DB_Controller.DBConnect = new DBConnect();
                        }

                        MeterSerialNumber CurrentMeterSerialNumber = null;

                        Gateway_Device_Schedule = Contr.DB_Controller.GetMeterSettings(IOConn.GatewaySerialNumber.ToString());
                        if (Gateway_Device_Schedule == null)
                            throw new ArgumentNullException("Meter Info Settings is null For GateWay Device", "MeterInfoSetting");

                        if (Gateway_Device_Schedule.Kas_DueTime > DateTime.Now.AddSeconds(20))
                        {
                            Thread.Sleep(5000);
                            Gateway_Device_Schedule.Kas_NextCallTime = DateTime.MinValue;
                            Gateway_Device_Schedule.Scheduler_Type = Scheduler_Type.OnDemandReading;
                        }
                        else if (Gateway_Device_Schedule.Kas_Interval.TotalMinutes > 0) // Execute Gateway Schedule
                        {
                            Gateway_Device_Schedule.Kas_NextCallTime = Contr.MeterInfo.GetNextCallTimeForFixedInterval(Gateway_Device_Schedule.Kas_DueTime,
                                                                                                                        Gateway_Device_Schedule.Kas_Interval.TotalMinutes);
                        }
                        //Reset Processing Status of Gateway Sub Meters
                        Contr.DB_Controller.UpdateGatewayMetersProcessingStatus(Gateway_Device_Schedule.MeterID);
                        //_totalFailureCount = 0;
                        //_totoalSuccessCount = 0;
                        _gatewayScanResult.Reset();

                        var metersList = Contr.DB_Controller.GetMSNListByGatewaySerialNumber(Gateway_Device_Schedule.MeterID.ToString());

                        if (Gateway_Device_Schedule != null)// &&
                        //Gateway_Device_Schedule.Scheduler_Type != Scheduler_Type.Not_Assigned &&
                        //Gateway_Device_Schedule.Scheduler_Type >= Scheduler_Type.BatchProcessor &&
                        //Gateway_Device_Schedule.Scheduler_Type <= Scheduler_Type.OnDemandReading)
                        {
                            if (_scheduler == null || _scheduler.GetType() != typeof(MeterInfoScheduler))
                                _scheduler = new MeterInfoScheduler() { Processing_Mode = Gateway_Device_Schedule.Scheduler_Type };
                            // Reset Scheduler 
                            else
                            {
                                // Reset Processing Mode here
                                if (_scheduler is MeterInfoScheduler)
                                    (_scheduler as MeterInfoScheduler).Processing_Mode = Gateway_Device_Schedule.Scheduler_Type;
                            }
                        }
                        if (_scheduler.MeterList != null)
                            _scheduler.MeterList.Clear();
                        if (_scheduler.ScheduledMeterList != null)
                            _scheduler.ScheduledMeterList.Clear();

                        // Other MeterInfoScheduler Types
                        //                        else ;

                        // new MeterInfoScheduler() { Processing_Mode = Scheduler_Type.BatchProcessor };
                        //if (Gateway_Device_Schedule.Scheduler_Type == Scheduler_Type.OnDemandReading)
                        //{
                        //    _scheduler.UpdateMeterInformation = new Func<MeterSerialNumber, MeterInformation>((msn) => Contr.DB_Controller.GetMeterSettings(msn.ToString(), true, Gateway_Device_Schedule.MeterID));
                        //}
                        //else
                        _scheduler.UpdateMeterInformation = new Func<MeterSerialNumber, MeterInformation>((msn) => Contr.DB_Controller.GetMeterSettings(msn.ToString()));

                        // Initialize Meter Connected With Gateway Device
                        // _scheduler.MeterList.Add(IOConn.GatewaySerialNumber);
                        if (metersList != null && metersList.Count > 0)
                        {
                            foreach (var srNumberSTR in metersList)
                            {
                                uint srNum = 0;
                                uint.TryParse(srNumberSTR, out srNum);
                                if (srNum > 0)
                                {
                                    CurrentMeterSerialNumber = new MeterSerialNumber();
                                    CurrentMeterSerialNumber.MSN = srNum;
                                    if (!_scheduler.MeterList.Contains(CurrentMeterSerialNumber))
                                        _scheduler.MeterList.Add(CurrentMeterSerialNumber);
                                }
                            }
                        }
                        _scheduler.ExecuteScheduler();
                        _gatewayScanResult.ScheduleType = (int)Gateway_Device_Schedule.Scheduler_Type;
                        _gatewayScanResult.TotalGatewayMeters = metersList.Count;
                        _gatewayScanResult.TotalReadableMeters = _scheduler.ScheduledMeterList.Count;
                        _gatewayScanResult.ScanStartTime = DateTime.Now;
                        _gatewayScanResult.GatewayNo = Convert.ToUInt32(Gateway_Device_Schedule.MSN);

                        //if (Gateway_Device_Schedule.Scheduler_Type != Scheduler_Type.OnDemandReading)
                        {
                            _scheduler.UpdateMeterInformation = new Func<MeterSerialNumber, MeterInformation>((msn) => Contr.DB_Controller.GetMeterSettings(msn.ToString(), true, Gateway_Device_Schedule.MeterID));
                        }
                        // Execute Each MeterSerailNumber 
                        // One After Another
                        //foreach (var MSN in _scheduler)
                        for (int i = 0; i < _scheduler.ScheduledMeterList.Count; i++)
                        {
                            var MSN = _scheduler.ScheduledMeterList[i];
                            CurrentMeterSerialNumber = MSN;
                            if (CurrentMeterSerialNumber.IsProcessed)
                                continue;
                            // Get MeterInformation
                            Contr.MeterInfo = _scheduler.GetMeterInformation(CurrentMeterSerialNumber, true);

                            if (Contr.MeterInfo == null)
                            {
                                Contr.LogMessage("@-@-@ Request Process failed for Meter @-@-@", "RKA", "F", 0);
                                continue;
                            }

                            if (Contr.MeterInfo.MSN != MSN.ToString())
                            {
                                CurrentMeterSerialNumber = _scheduler.ScheduledMeterList.Find(x => x.MSN.ToString() == Contr.MeterInfo.MSN);
                                i--;
                            }


                            Contr.MeterInfo.logoutMeter = true;

                            int tmpBufferSize = 1024;
                            #region Initialize Meter Setting

                            // Update MSN
                            IOConn.MeterSerialNumberObj = CurrentMeterSerialNumber;
                            // IOConn.GatewaySerialNumber = MeterSerialNumberObject;
                            IOConn.ConnectionTime = DateTime.Now;
                            InitializeMeterSetting(Contr, IOConn, EnableGeneralLog, false);
                            if (Contr.StatisticsObj != null)
                                Contr.StatisticsObj.MeterSerialNumber = CurrentMeterSerialNumber.ToString();

                            #endregion

                            // Reset Application Controller
                            Contr.ResetApplicationController();

                            #region Application Logic For Keep Alive

                            LastCommunicationStart = Contr.MeterInfo.Kas_DueTime;
                            try
                            {
                                IOConn.DeInitBufferKeepAlive();

                                if (MakeKeepAliveTransaction)
                                {
                                    #region Statistics 1

                                    if (Contr.ConnectToMeter.ConnectionInfo != null)
                                    {
                                        Contr.StatisticsObj.MeterSerialNumber = Contr.ConnectToMeter.MSN;
                                        Contr.StatisticsObj.Max_Allocated_Count = Owner.ThreadAllocator.Allocated_Count;
                                        Contr.StatisticsObj.Max_Active_Session_Count = Owner.ThreadAllocator.Active_Connection_Count;
                                    }

                                    #endregion
                                    Security_Data Prev_Sec_Data = null;
                                    Security_Data _Sec_Data = null;
                                    Prev_Sec_Data = Contr.Applicationprocess_Controller.Security_Data;
                                    // Load Latest Security Data
                                    // Contr.MeterInfo = Contr.DB_Controller.GetMeterSettings(IOConn.MSN);
                                    _Sec_Data = Contr.MeterInfo.ObjSecurityData;

                                    #region // Update Meter CurrentConnectionType

                                    if (Contr.MeterInfo.MeterType_OBJ != MeterType.KeepAlive)
                                    {
                                        IOConn.CurrentConnection = (Contr.MeterInfo.MeterType_OBJ == MeterType.KeepAlive) ?
                                        PhysicalConnectionType.KeepAlive : PhysicalConnectionType.NonKeepAlive;
                                        if (IOConn.CurrentConnection == PhysicalConnectionType.NonKeepAlive)
                                        {
                                            //Because Keep-Alive meter Type Change here
                                            Interlocked.Decrement(ref Owner.ThreadAllocator.KA_Alloc_Count);
                                        }
                                    }

                                    #endregion
                                    #region // Update Security Data

                                    if (Prev_Sec_Data != null)
                                    {
                                        if (Contr.MeterInfo.ObjSecurityData == null)
                                            Contr.MeterInfo.ObjSecurityData = Prev_Sec_Data;
                                        else
                                        {
                                            // Validate Security Data
                                            if (_Sec_Data.AuthenticationKey == null ||
                                                _Sec_Data.AuthenticationKey.Value == null ||
                                                _Sec_Data.AuthenticationKey.Value.Count <= 0)
                                                _Sec_Data.AuthenticationKey = Prev_Sec_Data.AuthenticationKey;

                                            if (_Sec_Data.EncryptionKey == null ||
                                                _Sec_Data.EncryptionKey.Value == null ||
                                                _Sec_Data.EncryptionKey.Value.Count <= 0)
                                                _Sec_Data.EncryptionKey = Prev_Sec_Data.EncryptionKey;

                                            if (_Sec_Data.SystemTitle == null ||
                                                _Sec_Data.SystemTitle.Count <= 0)
                                                _Sec_Data.SystemTitle = Prev_Sec_Data.SystemTitle;

                                            if (_Sec_Data.ServerSystemTitle == null ||
                                                _Sec_Data.ServerSystemTitle.Count <= 0)
                                                _Sec_Data.ServerSystemTitle = Prev_Sec_Data.ServerSystemTitle;

                                        }
                                    }

                                    Contr.Applicationprocess_Controller.Security_Data = Contr.MeterInfo.ObjSecurityData;

                                    #endregion

                                    if (Gateway_Device_Schedule.MeterID != Contr.MeterInfo.MeterID)
                                        Contr.MeterInfo.Kas_NextCallTime = Contr.MeterInfo.GetNextCallTimeForFixedInterval(Contr.MeterInfo.Kas_DueTime,
                                                                                                              Contr.MeterInfo.Kas_Interval.TotalMinutes);
                                    else
                                        Contr.MeterInfo.Kas_NextCallTime = DateTime.MinValue;


                                    T1 = DateTime.Now;
                                }
                                Cus_Exc = null;

                                if (Contr.MeterInfo != null)
                                {
                                    bool updateKasTime = false;

                                    try
                                    {
                                        if (!Contr.MeterInfo.IsDataRequestEmpty ||
                                            !Contr.MeterInfo.IsParamEmpty)
                                        {
                                            Interlocked.Increment(ref Owner.ThreadAllocator.ConditionHitCount);
                                            Interlocked.Increment(ref Owner.ThreadAllocator.active_Count);
                                            Interlocked.Increment(ref Owner.ThreadAllocator.KA_active_Count);

                                            if (Contr.MeterInfo.WakeUp_Request_ID != 0)
                                                Contr.DB_Controller.UpdateWakeUpProcess(true, 1, Contr.MeterInfo.WakeUp_Request_ID);

                                            // Initial IOConnection Internal Buffers
                                            IOConn.InitBuffer(tmpBufferSize, tmpBufferSize, Owner.CreateDataReaderBuffer);
                                            Contr.Applicationprocess_Controller.MaxLocalBuffer = tmpBufferSize;

                                            // FIX Delay To FIX HDLC Connect Error
                                            // Commons.Delay(Settings.Default.OnConnectionDelay);

                                            Contr.MeterInfo.SubMeterProcessedByGateway = CurrentMeterSerialNumber.IsProcessed = true;
                                            _gatewayScanResult.ReqTime = DateTime.Now;
                                            // Application Logic called from here
                                            Cus_Exc = Contr.RunApplicationLogic();
                                            // Meter In Login State
                                            if (Contr.Applicationprocess_Controller.IsConnected)
                                            {
                                                Contr.MeterInfo.logoutMeter = true;
                                                Contr.LogoutMeterConnection();
                                            }
                                            // DeInit IOConnection Internal Buffers For KeepAliver Meter
                                            IOConn.DeInitBufferKeepAlive();
                                            Interlocked.Decrement(ref Owner.ThreadAllocator.active_Count);
                                            Interlocked.Decrement(ref Owner.ThreadAllocator.KA_active_Count);
                                            Interlocked.Increment(ref Owner.ThreadAllocator.MDC_Status_Obj.KA_Expected_Transactions);
                                        }
                                        else updateKasTime = true;

                                        if (Gateway_Device_Schedule.MeterID != Contr.MeterInfo.MeterID)
                                            Contr.MIUF.IsTotal_RetriesWrite = true;
                                        Contr.MeterInfo.total_tries = (Contr.MeterInfo.total_tries + 1) % uint.MaxValue;
                                        if (Cus_Exc.isTrue && Cus_Exc.Ex == null)
                                        {
                                            Contr.MeterInfo.total_success = (Contr.MeterInfo.total_success + 1) % uint.MaxValue;
                                            _gatewayScanResult.Success++;
                                            _gatewayScanResult.ResponseTime = DateTime.Now;

                                            Contr.LogMessage("*~*~* Request processed Successfully for Meter *~*~*", "RKA", "S", 0);
                                            Contr.MeterInfo.ProcessStatus = Process_Status.RequestProcessedSuccessfully;
                                            Contr.StatisticsObj.IsSuccessful = true;
                                            Interlocked.Increment(ref Owner.ThreadAllocator.MDC_Status_Obj.KA_Successful_Transactions);
                                            //Interlocked.Increment(ref _totoalSuccessCount);
                                        }
                                        else if (!Cus_Exc.isTrue || Cus_Exc.Ex != null)
                                        {
                                            Contr.LogMessage("@-@-@ Request Process failed for Meter @-@-@", "RKA", "F", 0);
                                            Contr.MeterInfo.ProcessStatus = Process_Status.RequestProccessedFailed;

                                            //Interlocked.Increment(ref _totalFailureCount);
                                            _gatewayScanResult.Failure++;
                                            //float _totoal_Failure_Per = (_totalFailureCount /
                                            //                             (float)((metersList != null && metersList.Count > 0) ? metersList.Count : 1)) * 100f;

                                            if (!Commons.IsTCP_Connected(Cus_Exc.Ex))// || _totoal_Failure_Per >= MAX_Failure_Count)
                                            {
                                                // Raise_Error
                                                if (Cus_Exc.Ex != null)
                                                    throw Cus_Exc.Ex;
                                            }
                                            else
                                                ; // Skip Current Meter Error
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
                                            #region Duration

                                            Contr.StatisticsObj.Duration = DateTime.Now.Subtract(T1);

                                            if (Contr.ConnectToMeter != null &&
                                                Contr.ConnectToMeter.TCPWrapperStream != null)
                                            {
                                                LogMessage(Contr, IOConn, "BREC", Contr.ConnectToMeter.TCPWrapperStream.TotalBytesReceived.ToString());
                                                LogMessage(Contr, IOConn, "BSENT", Contr.ConnectToMeter.TCPWrapperStream.TotalBytesSent.ToString());

                                                //Contr.LogMessage(String.Format("Total Bytes Received: {0}",
                                                //    Contr.ConnectToMeter.TCPWrapperStream.TotalBytesReceived));
                                                //Contr.LogMessage(String.Format("Total Bytes Sent: {0}",
                                                //    Contr.ConnectToMeter.TCPWrapperStream.TotalBytesSent));
                                            }

#if Enable_Transactional_Logging
                                            Contr.StatisticsObj.InsertLog("Duration:" + Contr.StatisticsObj.Duration.ToString());
#endif

                                            #endregion
                                            #region Statistics 2

                                            try
                                            {
                                                Contr.StatisticsObj.InsertLog(Contr.ConnectToMeter.TCPWrapperStream.TotalBytesSent,
                                                                              Contr.ConnectToMeter.TCPWrapperStream.TotalBytesReceived);
                                                Contr.StatisticsObj.DBController = Contr.DB_Controller;
                                                Contr.StatisticsObj.StartSessionDateTime = Contr.SessionDateTime;

                                                if (Contr.MeterInfo != null &&
                                                    Contr.MeterInfo.Save_ST &&
                                                    Settings.Default.SaveStatistics)
                                                {
                                                    Contr.StatisticsObj.SaveStatictics();
                                                }

                                                #region Log and Error Log

                                                if (Contr.MeterInfo.EnableSaveLog)
                                                {
                                                    if (Contr.StatisticsObj.DP_Logger.ErrorListCount > 0 &&
                                                        Settings.Default.SaveErrorToDB)
                                                        Contr.StatisticsObj.SaveErrors();
                                                    if (Contr.StatisticsObj.DP_Logger.LogListCount > 0)
                                                    {
                                                        // TODO:Save Keep-Alive Log On Request Process Success only
                                                        if (IOConn.CurrentConnection == PhysicalConnectionType.NonKeepAlive)
                                                            Contr.StatisticsObj.SaveLogging(Contr.StatisticsObj.Duration);
                                                        else
                                                        {
                                                            // if (Cus_Exc.isTrue)
                                                            Contr.StatisticsObj.SaveLogging(Contr.StatisticsObj.Duration);
                                                        }
                                                    }
                                                }

                                                #endregion
                                                Contr.StatisticsObj.clearStatistics();
                                            }
                                            catch
                                            { }

                                            #endregion

                                            // Update into database and memory
                                            if (Contr.MeterInfo.ProcessStatus == Process_Status.RequestProcessedSuccessfully)
                                            {
                                                // Contr.MeterInfo.Kas_DueTime = Contr.MeterInfo.Kas_NextCallTime;
                                                Contr.MeterInfo.Kas_NextCallTime = Contr.MeterInfo.GetNextCallTimeForFixedInterval(Contr.MeterInfo.Kas_NextCallTime,
                                                                                                                                   Contr.MeterInfo.Kas_Interval.TotalMinutes);
                                            }

                                            // Reset Total Utilized Bandwidth
                                            Contr.ConnectToMeter.TCPWrapperStream.TotalBytesReceived = 0;
                                            Contr.ConnectToMeter.TCPWrapperStream.TotalBytesSent = 0;
                                        }
                                        catch
                                        { }
                                    }

                                    if (!Owner.ConnectionManager.TCPIPConnController_OBJ.TCPServer.IsShutDownInitiated &&
                                        !IOConn.CancelationRequest && IOConn.IsConnected)
                                    {
                                        // LastCommunicationStart = Contr.MeterInfo.Kas_DueTime;
                                        // TimeSpan CalculatedDelay = LastCommunicationStart.Subtract(DateTime.Now);
                                        // if (CalculatedDelay > TimeSpan.FromSeconds(0))
                                        //     Thread.Sleep(CalculatedDelay);

                                        MakeKeepAliveTransaction = true &&
                                        // (IOConn.CurrentConnection == PhysicalConnectionType.KeepAlive) &&
                                        !Owner.ConnectionManager.TCPIPConnController_OBJ.TCPServer.IsShutDownInitiated &&
                                        !IOConn.CancelationRequest;
                                    }
                                    else
                                        MakeKeepAliveTransaction = false;

                                    if (updateKasTime || Contr.MIUF.IsTotal_RetriesWrite)
                                    {
                                        Contr.MeterInfo.Kas_DueTime = Contr.MeterInfo.Kas_NextCallTime;
                                        if (updateKasTime && Contr.MeterInfo.Kas_DueTime != DateTime.MinValue)
                                            Contr.MIUF.KAS_DueTime = updateKasTime;
                                        else
                                            Contr.MIUF.KAS_DueTime = false;

                                        Contr.DB_Controller.UpdateMeterSettings(Contr.MeterInfo, Contr.MIUF);
                                        Contr.MIUF = new MeterInfoUpdateFlags();
                                    }
                                }
                                _gatewayScanResult.ScanTerminationReason = ScanTerminationReason.Completed;
                                // Exit Meter Process Loop
                                if (!MakeKeepAliveTransaction)
                                {
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {
                                _gatewayScanResult.ScanTerminationReason = ScanTerminationReason.Network_Error;
                                throw new Exception("Error while Processing meter : " + Contr.MeterInfo.MSN, ex);
                            }

                            #endregion
                        }

                        //uint ReadMetersCount = (uint)_scheduler.ScheduledMeterList.FindAll(x => x.IsProcessed).Count;

                        if (!Owner.ConnectionManager.TCPIPConnController_OBJ.TCPServer.IsShutDownInitiated &&
                            !IOConn.CancelationRequest && IOConn.IsConnected)
                        {
                            //float _totoal_Failure_Per = (_totalFailureCount /
                            //                             (float)((metersList != null && metersList.Count > 0) ? metersList.Count : 1)) * 100f;

                            MakeKeepAliveTransaction = true &&
                            (Gateway_Device_Schedule != null && Gateway_Device_Schedule.MeterType_OBJ == MeterType.KeepAlive) &&
                            !Owner.ConnectionManager.TCPIPConnController_OBJ.TCPServer.IsShutDownInitiated &&
                            !IOConn.CancelationRequest;

                        }
                        else
                        {
                            MakeKeepAliveTransaction = false;
                            _gatewayScanResult.ScanTerminationReason = ScanTerminationReason.Manual_Communication_Reset;
                        }
                        SaveGatewayScanResults(Contr, Gateway_Device_Schedule, _gatewayScanResult);
                        // Master Meter Configure As 
                        // Keep Alive ConnectionType
                        if (MakeKeepAliveTransaction)
                        {
                            //if (Gateway_Device_Schedule != null)
                            //{
                            //    LastCommunicationStart = Gateway_Device_Schedule.Kas_DueTime;
                            //    TimeSpan CalculatedDelay = LastCommunicationStart.Subtract(DateTime.Now);

                            //    // Sleep GateWay Device For 
                            //    // Next Transaction
                            //    if (CalculatedDelay > TimeSpan.FromSeconds(0))
                            //        Thread.Sleep(CalculatedDelay);
                            //}

                            goto RepeatLoop_GSN;
                        }
                    }

                    #endregion
                }

                #endregion
            }

            #region Catch block

            catch (OperationCanceledException ex)
            {
                String error = String.Format("Error Occurred While Processing meter:The operation was canceled {0},\r\n{1}", IOConn, ex.Message);
                // Contr.LogMessage(error);
                Contr.StatisticsObj.StartSessionDateTime = T1;
                Contr.StatisticsObj.Duration = DateTime.Now.Subtract(T1);
                Contr.StatisticsObj.IsSuccessful = false;
                #region Debugging & Logging
#if Enable_DEBUG_ECHO
                // Common.WriteLine(error);
#endif
#if Enable_Error_Logging
                Contr.StatisticsObj.InsertError(error);
#endif
                #endregion
                #region // Disconnection TCP Connection If Error related to IOException

                try
                {
                    SaveGatewayScanResults(Contr, Gateway_Device_Schedule, _gatewayScanResult);
                    if (IOConn != null && IOConn.IsConnected)
                    {
                        Owner.ConnectedMeterList.Remove(IOConn);
                        IOConn.Disconnect();
                    }
                }
                catch (Exception) { }

                #endregion
            }
            catch (Exception ex)
            {
                #region // Disconnection TCP Connection If Error related to IOException

                try
                {
                    if (IOConn != null && IOConn.IsConnected)
                    {
                        Exception _ex = ex;
                        while (_ex != null)
                        {
                            if (_ex is System.IO.IOException)
                            {
                                /// *** Commented ConnStatus
                                /// IOConn.InsertLogMessage(String.Format("Catch block Connection ConnectionRunnable_Logic_ConnectionThreadAllocater {0}", _ex.Message));
                                IOConn.Disconnect();
                                Owner.ConnectedMeterList.Remove(IOConn);
                                break;
                            }
                            _ex = _ex.InnerException;
                        }
                    }
                }
                catch (Exception) { }

                #endregion

                try
                {
                    // Contr.LogMessage(ex);
                    string Info = null;
                    Exception _ex = ex;
                    Info = String.Format("Connection Info:{0} __ {1}", IOConn, IOConn.IOStream);
                    Contr.StatisticsObj.StartSessionDateTime = T1;
                    Contr.StatisticsObj.Duration = DateTime.Now.Subtract(T1);
                    Contr.StatisticsObj.IsSuccessful = false;
                    #region Debugging & Logging
#if Enable_DEBUG_ECHO
                    // Common.WriteLine(ex.Message);
#endif
#if Enable_Error_Logging
                    Contr.StatisticsObj.InsertError(ex, Contr.SessionDateTime, 15);
#endif
                    #endregion
                    SaveGatewayScanResults(Contr, Gateway_Device_Schedule, _gatewayScanResult);
                }
                catch
                { }
            }

            #endregion
            #region finally block

            finally
            {
                #region Statistics 2
                try
                {
                    Contr.StatisticsObj.InsertLog(Contr.ConnectToMeter.TCPWrapperStream.TotalBytesSent,
                                        Contr.ConnectToMeter.TCPWrapperStream.TotalBytesReceived);
                    Contr.StatisticsObj.DBController = Contr.DB_Controller;
                    Contr.StatisticsObj.StartSessionDateTime = Contr.SessionDateTime;
                    if (Contr.MeterInfo != null &&
                        Contr.MeterInfo.Save_ST && Settings.Default.SaveStatistics)
                    {

                        Contr.StatisticsObj.SaveStatictics(); // Not Done - Already i think

                    }

                    #region Log and Error Log

                    if (Contr.StatisticsObj.DP_Logger.ErrorListCount > 0 && Settings.Default.SaveErrorToDB)
                        Contr.StatisticsObj.SaveErrors();

                    if (Contr.StatisticsObj.DP_Logger.LogListCount > 0)
                    {
                        Contr.StatisticsObj.DP_Logger.IsSaveToDb = Convert.ToBoolean(Settings.Default.SaveLogToDBFlag);
                        Contr.StatisticsObj.MeterSerialNumber = IOConn.MSN;
                        Contr.StatisticsObj.SaveLogging(Contr.StatisticsObj.Duration);
                    }

                    #endregion

                    Contr.StatisticsObj.clearStatistics();
                    // Reset Total Utilized Bandwidth
                    Contr.ConnectToMeter.TCPWrapperStream.TotalBytesReceived = 0;
                    Contr.ConnectToMeter.TCPWrapperStream.TotalBytesSent = 0;
                }
                catch
                { }

                #endregion

                Contr.StatisticsObj = null;
                #region Deallocation Process Starts

                try
                {
                    if (IOConn != null && Contr != null)
                    {
                        // Fix_Deallocation_Meter_Error
                        try
                        {
                            if (Contr.Applicationprocess_Controller.IsConnected)
                            {
                                Contr.LogoutMeterConnection(); // LOGOUT
                            }
                            if (Contr.Applicationprocess_Controller.IsConnected)
                            {
                                Contr.Applicationprocess_Controller.ApplicationProcess.Is_Association_Developed = false;

                                IOConn.Disconnect();
                                Owner.ConnectedMeterList.Remove(IOConn);
                            }
                        }
                        catch { }

                        Owner.ThreadAllocator.DeAllocateMeterConnection(IOConn, ref Contr);
                        if (IOConn != null && IOConn.IsConnected)
                        {
                            IOConn.Disconnect();
                            Owner.ConnectedMeterList.Remove(IOConn);

                        }
                    }
                    else if (Contr != null)
                    {
                        if (Contr.Applicationprocess_Controller.IsConnected)
                        {
                            Contr.Applicationprocess_Controller.ApplicationProcess.Is_Association_Developed = false;
                        }

                        // Initial DLMS Logger
                        DLMSLogger logger = Contr.Applicationprocess_Controller.ApplicationProcess.Logger;
                        if (logger != null)
                            logger.Identifier = "";

                    }
                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine("Error De-Allocate Meter Connection");
                    #region Debugging &  Logging
#if Enable_DEBUG_ECHO

                    // Common.WriteLine(String.Format("finally->catch 2"));

#endif
                    #endregion
                }

                #endregion
            }

            #endregion
        }

        private void LogMessage(ApplicationController Contr, IOConnection IOConn, string Message, string Status, bool ViewLog = true)
        {
            string msg = String.Format("{0,-8}{1,-2}", Message, Status);
            if (ViewLog)
                Contr.LogMessage(String.Format(" {0,10}\t{1}", IOConn.MSN, msg));//));
            Contr.StatisticsObj.InsertLog(msg);
            IOConn.MeterLiveLog = msg;
        }

        private void SaveGatewayScanResults(ApplicationController Contr, MeterInformation Gateway_Device_Schedule, GatewayScanResult _gatewayScanResult)
        {

            var MIUF = new MeterInfoUpdateFlags();
            if (Gateway_Device_Schedule != null)
            {
                if (_gatewayScanResult.ScanedMeters > 0 && _gatewayScanResult.ScanTerminationReason != ScanTerminationReason.Unknown)
                {
                    Contr.LogMessage(String.Format("CSR: {0} %",
                                                Math.Round(_gatewayScanResult.Success / (float)_gatewayScanResult.ScanedMeters * 100f)));
                    Gateway_Device_Schedule.total_success = (Gateway_Device_Schedule.total_success + (uint)_gatewayScanResult.Success) % uint.MaxValue;
                    Gateway_Device_Schedule.total_tries = (Gateway_Device_Schedule.total_tries + (uint)_gatewayScanResult.ScanedMeters) % uint.MaxValue;
                    _gatewayScanResult.SessionTime = _gatewayScanResult.ScanEndTime = DateTime.Now;
                    Contr.DB_Controller.insert_GateWayScanResults(_gatewayScanResult);
                }

                if (Gateway_Device_Schedule.Kas_NextCallTime != DateTime.MinValue)
                {
                    Gateway_Device_Schedule.Kas_DueTime = Gateway_Device_Schedule.Kas_NextCallTime;
                    MIUF.KAS_DueTime = true;
                }
                MIUF.IsTotal_RetriesWrite = true;
                Contr.DB_Controller.UpdateMeterSettings(Gateway_Device_Schedule, MIUF);
            }

        }

        private void InitializeMeterSetting(ApplicationController Contr, IOConnection IOConn, bool EnableGeneralLog = true, bool isUpdateMeterSettings = true)
        {
            MeterSerialNumber MeterSerialNumberObject = IOConn.MeterSerialNumberObj;

            #region // Read Meter Settings

            try
            {
                if (isUpdateMeterSettings)
                    Contr.MeterInfo = Contr.DB_Controller.GetMeterSettings(MeterSerialNumberObject.ToString());
            }
            catch (Exception ex)
            {
                if (EnableGeneralLog)
                    Contr.LogMessage(String.Format("Meter Settings loading from Database Failed! Reason:" +
                                      ex.Message, MeterSerialNumberObject));
                throw ex;
#if Enable_Transactional_Logging

                 Contr.StatisticsObj.InsertLog_Temp(String.Format("Meter Settings loading from Database Failed", MeterSerialNumberObject));

#endif
                //  Contr.LogMessage(String.Format(" {0,10}\t Error", ex.Message));
            }

            if (Contr.MeterInfo.MeterType_OBJ != MeterType.Intruder)
            {
                if (!Commons.ValidateNetworkAddress(IOConn.TCPWrapperStream.ToString(),
                    Contr.MeterInfo.NetworkAdress, Contr.MeterInfo.SubNetMask))
                    Contr.MeterInfo.MeterType_OBJ = MeterType.Intruder;
            }

            // DDS110            
            Contr.Applicationprocess_Controller.IsCompatibilityMode = Contr.MeterInfo.DDS110_Compatible;

            #region Load Meter Configures

            try
            {
                long? LP_GP_ID = 0;
                int Association_Id = Contr.MeterInfo.Association_Id;

                // FIX Device Association ID
                // MAX ASSOC ID = 100
                if (Association_Id <= 0 || Association_Id > 100)
                {
                    // Management R326
                    Association_Id = 08;
                    // throw new ArgumentException("Association Id Not Valid", "Association_Id");
                }

                Contr.Configurator.GetMeterConnectionInfo(IOConn, MeterSerialNumberObject, Association_Id);

                // Set Current Server & Client SAP addresses
                SAP_Object Meter_Device = new SAP_Object(IOConn.ConnectionInfo.MeterInfo.Device_Association.MeterSap);
                SAP_Object Client = new SAP_Object(IOConn.ConnectionInfo.MeterInfo.Device_Association.ClientSap);

                if (Meter_Device == null || Client == null)
                    throw new InvalidOperationException("Meter_Device or Client Login Invalid");

                // Initial ConnectionInfo
                Contr.LoadProfile_Controller.CurrentConnectionInfo = IOConn.ConnectionInfo;
                Contr.Event_Controller.CurrentConnectionInfo = IOConn.ConnectionInfo;
                Contr.InstantaneousController.ConnectionInfo = IOConn.ConnectionInfo;
                Contr.Billing_Controller.CurrentConnectionInfo = IOConn.ConnectionInfo;
                //********************************************************************
                #region Configuration Test Code Region

                // var OBIS_DetailsROW = Contr.Configurator.ConfigurationHelper.LoadedConfigurations.OBIS_Details.NewOBIS_DetailsRow();

                // OBIS_DetailsROW.id = 1462;
                // OBIS_DetailsROW.Obis_Code = 2251799830462719;
                // OBIS_DetailsROW.Default_OBIS_Code = 2251799830462719;
                // OBIS_DetailsROW.Device_Id = 5;

                // Contr.Configurator.ConfigurationHelper.LoadedConfigurations.OBIS_Details.AddOBIS_DetailsRow(OBIS_DetailsROW);

                // OBISDetailsDAO ObisDetails = new OBISDetailsDAO();
                // ObisDetails.AcceptChangesObisDetails(Contr.Configurator.ConfigurationHelper.LoadedConfigurations);

                // SAP TABLE
                // var SAPTABLE = Contr.Configurator.GetAccessRights(IOConn.ConnectionInfo);
                // var LPChannels = Contr.Configurator.GetMeterLoadProfileChannels(IOConn.ConnectionInfo);

                #region Read Load Profile Channel Info

                // var LP_Counters = new Profile_Counter();
                // LP_Counters.Current_Counter = int.MaxValue;
                // LP_Counters.Previous_Counter = (uint)Contr.MeterInfo.Counter_Obj.LoadProfile_Count;
                // LP_Counters.Max_Size = (uint)Limits.Max_LoadProfile_Count_Limit;
                // LP_Counters.GroupId = Contr.MeterInfo.Counter_Obj.LoadProfile_GroupID;

                // List<LoadProfileChannelInfo> _loadProfileChannelsInfo = Contr.LoadProfile_Controller.GetChannelsInfoList(LP_Counters);
                // Contr.Configurator.GetMeterGroupByLoadProfileChannels(IOConn.ConnectionInfo, _loadProfileChannelsInfo, out LP_GP_ID);

                #endregion

                // var eventInfo_Lst = Contr.Configurator.GetMeterEventInfo(IOConn.ConnectionInfo);
                // var eventLogInfo_Lst = Contr.Configurator.GetMeterEventLogInfo(IOConn.ConnectionInfo);
                // var billItemFrmt_Lst = Contr.Configurator.GetBillingItemsFormat(IOConn.ConnectionInfo);

                #region Capture Objects

                #region CumulativeBilling

                var ProfileOBISCode = new StOBISCode() { };
                ProfileOBISCode = Get_Index.CumulativeBilling;
                List<CaptureObject> captureObjectsList = null;

                // Initialize Capture Object List
                // try
                // {
                //     captureObjectsList = Contr.Configurator.GetProfileCaptureObjectList(IOConn.ConnectionInfo, ProfileOBISCode);
                // }
                // catch (Exception ex)
                // {
                //     Contr.LogMessage(String.Format("Error Load Cumulative Billing Capture Objects" + ex.Message, MeterSerialNumberObject));
                // }

                // if (captureObjectsList == null || captureObjectsList.Count <= 0)
                // {
                //     captureObjectsList = new List<CaptureObject>();
                //     captureObjectsList.Add(new CaptureObject() { StOBISCode = (Get_Index)2251799830462719, AttributeIndex = 2, DataIndex = 0 });
                //     captureObjectsList.Add(new CaptureObject() { StOBISCode = (Get_Index)845524693942527, AttributeIndex = 0, DataIndex = 0 });
                //     captureObjectsList.Add(new CaptureObject() { StOBISCode = (Get_Index)845524693942783, AttributeIndex = 0, DataIndex = 0 });
                //
                //     captureObjectsList.Add(new CaptureObject() { StOBISCode = (Get_Index)845524693943039, AttributeIndex = 0, DataIndex = 0 });
                //     captureObjectsList.Add(new CaptureObject() { StOBISCode = (Get_Index)845524693943295, AttributeIndex = 0, DataIndex = 0 });
                //     captureObjectsList.Add(new CaptureObject() { StOBISCode = (Get_Index)845524693943551, AttributeIndex = 0, DataIndex = 0 });
                //
                //     captureObjectsList.Add(new CaptureObject() { StOBISCode = (Get_Index)845526024847615, AttributeIndex = 0, DataIndex = 0 });
                //     captureObjectsList.Add(new CaptureObject() { StOBISCode = (Get_Index)845526024847871, AttributeIndex = 0, DataIndex = 0 });
                //     captureObjectsList.Add(new CaptureObject() { StOBISCode = (Get_Index)845526024848127, AttributeIndex = 0, DataIndex = 0 });
                //
                //     captureObjectsList.Add(new CaptureObject() { StOBISCode = (Get_Index)845526024848383, AttributeIndex = 0, DataIndex = 0 });
                //     captureObjectsList.Add(new CaptureObject() { StOBISCode = (Get_Index)845526024848639, AttributeIndex = 0, DataIndex = 0 });
                //     captureObjectsList.Add(new CaptureObject() { StOBISCode = (Get_Index)1126999670259967, AttributeIndex = 0, DataIndex = 0 });
                //
                //     captureObjectsList.Add(new CaptureObject() { StOBISCode = (Get_Index)1126999670260223, AttributeIndex = 0, DataIndex = 0 });
                //     captureObjectsList.Add(new CaptureObject() { StOBISCode = (Get_Index)1126999670260479, AttributeIndex = 0, DataIndex = 0 });
                //     captureObjectsList.Add(new CaptureObject() { StOBISCode = (Get_Index)1126999670260735, AttributeIndex = 0, DataIndex = 0 });
                //
                //     captureObjectsList.Add(new CaptureObject() { StOBISCode = (Get_Index)1126999670260991, AttributeIndex = 0, DataIndex = 0 });
                //
                //     Contr.Configurator.SaveProfileCaptureObjectList(IOConn.ConnectionInfo, captureObjectsList, ProfileOBISCode, null);
                // }

                #endregion

                // Event Controller
                ProfileOBISCode = Get_Index._Event_Log_All;
                captureObjectsList = null;
                // captureObjectsList = Contr.Configurator.GetProfileCaptureObjectList(IOConn.ConnectionInfo, ProfileOBISCode);

                #endregion

                #endregion
            }
            catch (Exception ex)
            {
                Contr.LogMessage(String.Format("Load Meter Configures Failed! Reason:" + ex.Message, MeterSerialNumberObject));
            }

            #endregion

            #region Keep Alive Meter

            if (Contr.MeterInfo.MeterType_OBJ == MeterType.KeepAlive)
            {
                // if (!Commons.ValidateNetworkAddress(IOConn.TCPWrapperStream.ToString(), Contr.MeterInfo.NetworkAdress, Contr.MeterInfo.SubNetMask))
                //     Contr.MeterInfo.MeterType_OBJ = MeterType.Intruder;

                Contr.MeterInfo.Kas_NextCallTime = Contr.MeterInfo.
                    GetNextCallTimeForFixedInterval(Contr.MeterInfo.Kas_DueTime, Contr.MeterInfo.Kas_Interval.TotalMinutes);

                IOConn.CurrentConnection = PhysicalConnectionType.KeepAlive;
                Interlocked.Increment(ref Owner.ThreadAllocator.KA_Alloc_Count);
            }

            #endregion
            #region Non Keep Alive Meter

            else if (Contr.MeterInfo.MeterType_OBJ == MeterType.NonKeepAlive)
            {
                //if (!Commons.ValidateNetworkAddress(IOConn.TCPWrapperStream.ToString(), Contr.MeterInfo.NetworkAdress, Contr.MeterInfo.SubNetMask))
                //    Contr.MeterInfo.MeterType_OBJ = MeterType.Intruder;

                IOConn.CurrentConnection = PhysicalConnectionType.NonKeepAlive;
            }

            #endregion

            #region CheckStatus

            if (Contr.MeterInfo.Status)
            {
                #region Insert Connection Time

                // if (Contr.MeterInfo.SaveLifeTime)
                //  Contr.DB_Controller.InsertMeterConnectionTime(MeterSerialNumberObject.ToString(), IOConn.IOStream.ToString(), IOConn.ConnectionTime);

                Contr.DB_Controller.UpdateMeterConnectionTimeForLiveData(MeterSerialNumberObject.ToString(), IOConn.ConnectionTime);
                if (Contr.MeterInfo.WakeUp_Request_ID != 0)
                    Contr.DB_Controller.UpdateWakeUpProcess(true, 1, Contr.MeterInfo.WakeUp_Request_ID);

                #endregion
                #region Debugging & Logging
#if Enable_DEBUG_RunMode
                            Contr.LogMessage("Reading Public Login application data access rights");
                            List<OBISCodeRights> AccessRights = await Contr.ConnectionController.ReadMeterAccessRightsAsync(IOConn);
                            Contr.LogMessage("Reading completes,Saving application data access rights");
                            Owner.Configurator.SaveOBISCodeRights(IOConn.ConnectionInfo, AccessRights);
#endif
                #endregion
            }

            #endregion

            #endregion
        }

        public void MajorAlarmNotification_Handler(MajorAlarmNotification mjrAlrmNotify)
        {
            try
            {
                // Add Event To Be Invoke Later
                lock (_cachedEvents)
                {
                    _cachedEvents.AddLast(mjrAlrmNotify);
                }

                bool isRunner = false;
                GetThreadState(out isRunner);
                if (!isRunner)
                {
                    lock (this)
                    {
                        GetThreadState(out isRunner);
                        if (isRunner)
                            return;
                        StartAsyncHelperThread();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log Error Message
            }
        }

        private void AsyncMajorAlarmNotification_Helper()
        {

            Func<bool> IsEventCached = new Func<bool>(() => _cachedEvents != null &&
                                                            _cachedEvents.Count > 0);

            #region ParalLoopAction

            Action<List<MajorAlarmNotification>, ParallelLoopState> ParalLoopAction
                    = new Action<List<MajorAlarmNotification>, ParallelLoopState>((@evntEvList, loopState) =>
                {
                    MeterData.events_data_individualDataTable all_MjrAlarms = null;

                    try
                    {
                        MajorAlarmNotification @evntLocal = null;

                        all_MjrAlarms = new MeterData.events_data_individualDataTable();

                        // local variable to store
                        List<object> _dataToSave = null;
                        MeterSerialNumber _msn = null;
                        //string CustomerRef = "00000000000000";
                        long? customer_Id = null;
                        DateTime session_DateTime = DateTime.MinValue;

                        foreach (IEvent @alrm in @evntEvList)
                        {
                            @evntLocal = (MajorAlarmNotification)@alrm;

                            try
                            {
                                _dataToSave = @evntLocal.DataToSave;

                                if (_dataToSave == null ||
                                    _dataToSave.Count <= 1)
                                {
                                    // Log Debug Message
                                    continue;
                                }

                                _msn = (MeterSerialNumber)_dataToSave[0];
                                if (_dataToSave.Count > 1)
                                    session_DateTime = (DateTime)_dataToSave[1];
                                //if (_dataToSave.Count > 2)
                                //    CustomerRef = (string)_dataToSave[2];
                                if (_dataToSave.Count > 2)
                                    customer_Id = Convert.ToInt64(_dataToSave[2]);

                                if (_msn == null || !_msn.IsMSN_Valid)
                                {
                                    // Log Debug Message
                                    continue;
                                    // throw new Exception("Meter Serial Number not Valid");
                                }

                                var DT_ROW = all_MjrAlarms.Newevents_data_individualRow();

                                // session_DateTime, @evntLocal.OccurrenceTimeStamp, @evntLocal.ReceptionTimeStamp,
                                // _msn.ToString(), @evntLocal.EventCode, 0, string.Empty, true, CustomerRef, customer_Id

                                // Initialize Data Rows
                                // Validate Data Before Insert
                                if (customer_Id.HasValue)
                                    DT_ROW.customer_id = customer_Id.Value;
                                else
                                    DT_ROW.Setcustomer_idNull();

                                if (session_DateTime != null)
                                    DT_ROW.session_datetime = session_DateTime;
                                else
                                    DT_ROW.session_datetime = DateTime.MinValue;

                                if (@evntLocal.OccurrenceTimeStamp != null)
                                    DT_ROW.arrival_time = @evntLocal.OccurrenceTimeStamp;
                                else
                                    DT_ROW.arrival_time = DateTime.MinValue;

                                DT_ROW.msn = _msn.ToString();
                                //DT_ROW.reference_no = CustomerRef;

                                if (@evntLocal.ReceptionTimeStamp != null)
                                {
                                    DT_ROW.date = @evntLocal.ReceptionTimeStamp;
                                    DT_ROW.time = @evntLocal.ReceptionTimeStamp.TimeOfDay;
                                }
                                else
                                {
                                    DT_ROW.date = DateTime.MinValue;
                                    DT_ROW.time = TimeSpan.MinValue;
                                }

                                DT_ROW.event_code = @evntLocal.EventCode.ToString();
                                DT_ROW.counter = 0;

                                DT_ROW.description = string.Empty;
                                DT_ROW.is_individual = 1;

                                all_MjrAlarms.Addevents_data_individualRow(DT_ROW);
                            }
                            catch
                            {
                                // Log Message
                            }
                            finally
                            {
                                if (@evntLocal != null &&
                                    Owner.ApplicationEventPool != null)
                                {
                                    @alrm.Init();
                                    // Add Event To Application Event
                                    // To Reuse Event
                                    Owner.ApplicationEventPool.TryAdd<SharedCode.Comm.EventDispatcher.Contracts.IEvent>(@alrm);
                                }
                            }
                        }

                        // Save Major Alarms Data
                        _DBController.saveMajorAlarmEventData_Individual(all_MjrAlarms);
                    }
                    catch
                    {
                        loopState.Break();
                    }
                    finally
                    {
                        if (all_MjrAlarms != null)
                            all_MjrAlarms.Dispose();
                        all_MjrAlarms = null;
                    }
                });

            #endregion

            IEvent @event = null;

            try
            {
                if (_DBController == null)
                    throw new ArgumentNullException("Database Controller");
                if (!_DBController.DBConnect.OpenConnection())
                    throw new Exception("Unable to open Database Connection Properly");

                // Continue ASync Event Handler
                while (true)
                {
                    List<List<MajorAlarmNotification>> majorAlarmsToProc = new List<List<MajorAlarmNotification>>();
                    List<MajorAlarmNotification> localLst = null;

                    while (_cachedEvents != null &&
                           _cachedEvents.Count > 0 &&
                           majorAlarmsToProc.Count <= 05)
                    {
                        localLst = new List<MajorAlarmNotification>();

                        while (_cachedEvents != null &&
                               _cachedEvents.Count > 0 &&
                               localLst.Count <= 10)
                        {
                            lock (_cachedEvents)
                            {
                                var lnkNode = _cachedEvents.First;
                                _cachedEvents.RemoveFirst();
                                if (lnkNode != null &&
                                    lnkNode.Value != null)
                                {
                                    @event = lnkNode.Value;
                                }
                            }

                            if (@event == null ||
                                @event.GetType() != typeof(MajorAlarmNotification))
                                continue;
                            else
                                localLst.Add(@event as MajorAlarmNotification);
                        }

                        majorAlarmsToProc.Add(localLst);
                        localLst = null;
                    }

                    // Invoke Event In Parallel
                    Parallel.ForEach<List<MajorAlarmNotification>>(majorAlarmsToProc, ParalLoopAction);
                    majorAlarmsToProc.Clear();

                    if (IsEventCached.Invoke())
                        continue;
                    Commons.DelayUntil(IsEventCached, MaxThreadIdleSuspendTime.Ticks);
                    // break On MaxThreadIdleTime Elapsed
                    if (!IsEventCached.Invoke())
                        break;
                }
            }
            catch (Exception ex)
            {
                // Log Error Message
                System.Diagnostics.Debug.WriteLine("Error AsyncMajorAlarmNotification_Helper: " + ex.Message);
            }
            finally
            {
                SetThreadState(false);
            }
        }

        #region Support_Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isRunner"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void StartAsyncHelperThread()
        {
            try
            {
                SetThreadState(true);
                if (ASyncThread != null)
                    ASyncThread.Abort();
            }
            catch (Exception ex)
            {
                // Debug.WriteLine("Error:" + ex.Message);
            }

            try
            {
                ASyncThread = new System.Threading.Thread(AsyncMajorAlarmNotification_Helper) { Priority = ThreadPriority.AboveNormal };
                ASyncThread.Start();
                SetThreadState(true);
            }
            catch
            {
                SetThreadState(false);
            }
        }

        /// <summary>
        /// Set Current Helper Thread Running State
        /// </summary>
        /// <param name="isRunner"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void SetThreadState(bool isRunner)
        {
            isThreadRunning = isRunner;
        }

        /// <summary>
        /// Get Current Helper Thread Running State
        /// </summary>
        /// <param name="isRunner"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void GetThreadState(out bool isRunner)
        {
            isRunner = isThreadRunning;
        }

        #endregion

    }
}
