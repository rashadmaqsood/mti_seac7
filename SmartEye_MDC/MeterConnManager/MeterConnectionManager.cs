// #define Enable_DEBUG_ECHO
// #define Enable_Error_Logging
// #define Enable_Transactional_Logging

using comm;
using Communicator.MeterConnManager;
using Communicator.MTI_MDC;
using DatabaseManager.Database;
using DLMS;
using DLMS.Comm;
using DLMS.LRUCache;
using LogSystem.Shared.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SharedCode.Comm.DataContainer;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.EventDispatcher;
using SharedCode.Comm.EventDispatcher.Contracts;
using DatabaseConfiguration.DataSet;

namespace Commuincator.MeterConnManager
{
    public class MeterConnectionManager : IDisposable
    {
        #region Data_Members

        public static readonly int MaximumMeterConnections = 15000;
        public static readonly int MinMeterConnections = 40;
        public static readonly TimeSpan MinMeterConnectionLevelDuartion = new TimeSpan(0, 01, 30);

        private ArrayList _MeterLogicController;
        private ushort OwnerId = 0;
        private IEventDispatcher _appEventDispatcher = null;
        private IEventPool _appEventPool = null;

        private ConnectionThreadAllocater _ThreadAllocator;
        private DataTaskAllocater taskRunner;
        private Configs configuration;
        private Configurator _Configurator = null;
        private Debugger _debugger;
        private IDLMSClassFacotry dLMSClassMaker;
        private DLMS.LRUCache.LRUPriorityDLMSCache dLMSInstancesCache;
        // private ConcurrentDictionary<string, SAPTable> _SAPTables;
        private ConnectionManager ConnManager;
        
        /// <summary>
        /// Max Active Meter Connection Threads
        /// </summary>
        private int maxMeterConnection = 1000;
        private int minMeterConnection = 05;
        private TimeSpan minMeterConnDuration = TimeSpan.MinValue;
        private float kaMeterConnections = 25f;
        private TimeSpan _MaxSessionResetDuration = new TimeSpan(0, 0, 60, 0, 0); //60:00 Min default inactivity
        private static TimeSpan objectCacheMinAgeTime = TimeSpan.FromMinutes(2);
        private static TimeSpan objectCahceMaxAgeTime = TimeSpan.FromMinutes(5);
        // Define On Server Max Time Slot
        private TimeSpan defaultTimeSlot = TimeSpan.FromMinutes(5);
        private TimeSpan minMeterConnectionTime = TimeSpan.FromMinutes(2);
        public static readonly TimeSpan MinTimeSlot = TimeSpan.FromMinutes(5);
        private bool enableSlotMonitoring = true;
        private System.Timers.Timer PeriodicSchedular = null;
        private TimeSpan Schedular_Timer = TimeSpan.FromSeconds(60);

        private GetDataReaderBuffer _CreateDataReaderBuffer;

        #endregion

        #region Properties

        public ArrayList MeterLogicController
        {
            get { return _MeterLogicController; }
            set { _MeterLogicController = value; }
        }

        public ConnectionThreadAllocater ThreadAllocator
        {
            get { return _ThreadAllocator; }
            set { _ThreadAllocator = value; }
        }

        public DataTaskAllocater TaskRunner
        {
            get { return taskRunner; }
        }

        public IEventDispatcher ApplicationEventDispatcher
        {
            get { return _appEventDispatcher; }
            private set { _appEventDispatcher = value; }
        }

        public IEventPool ApplicationEventPool
        {
            get { return _appEventPool; }
            private set { _appEventPool = value; }
        }

        public int MaxMeterConnection
        {
            get { return maxMeterConnection; }
            set { maxMeterConnection = value; }
        }

        public int MinMeterConnection
        {
            get { return minMeterConnection; }
            set { minMeterConnection = value; }
        }

        public TimeSpan MinMeterConnectionDuration
        {
            get { return minMeterConnDuration; }
            set { minMeterConnDuration = value; }
        }

        public bool IsMinMeterConnDurationValid
        {
            get
            {
                try
                {
                    if (MinMeterConnectionDuration != TimeSpan.MinValue)
                    {
                        if (DateTime.Now.TimeOfDay.Subtract(MinMeterConnectionDuration) > MinMeterConnectionLevelDuartion)
                            return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public float MaxKAMeterConnections
        {
            get { return kaMeterConnections; }
            set
            {
                //Valid Value Range 0.0f -- 100.0f
                kaMeterConnections = value;
            }
        }

        public int MaxKeepAliveMeterConnections
        {
            get
            {
                return Convert.ToInt32(MaxMeterConnection * (MaxKAMeterConnections * .01f));
            }
        }

        public int MaxNonKeepAliveMeterConnections
        {
            get
            {
                return (MaxMeterConnection - MaxKeepAliveMeterConnections);
            }
        }

        public Debugger Debugger
        {
            get { return _debugger; }
            set { _debugger = value; }
        }

        public IDLMSClassFacotry DLMSClassMaker
        {
            get { return dLMSClassMaker; }
            set { dLMSClassMaker = value; }
        }

        public DLMS.LRUCache.LRUPriorityDLMSCache DLMSInstancesCache
        {
            get { return dLMSInstancesCache; }
            set { dLMSInstancesCache = value; }
        }

        //public ConcurrentDictionary<string, SAPTable> SAPTables
        //{
        //    get { return _SAPTables; }
        //    set { _SAPTables = value; }
        //}

        public ConnectionsList ConnectedMeterList
        {
            get
            {
                if (ConnectionManager == null)
                    return null;
                else
                    return ConnectionManager.IOConnectionsList;
            }
        }

        public ConnectionManager ConnectionManager
        {
            get { return ConnManager; }
            set { ConnManager = value; }
        }

        public static TimeSpan ObjectCacheMinAgeTime
        {
            get { return MeterConnectionManager.objectCacheMinAgeTime; }
            set { MeterConnectionManager.objectCacheMinAgeTime = value; }
        }

        public static TimeSpan ObjectCahceMaxAgeTime
        {
            get { return MeterConnectionManager.objectCahceMaxAgeTime; }
            set { MeterConnectionManager.objectCahceMaxAgeTime = value; }
        }

        public Configs Configuration
        {
            get { return configuration; }
            set { configuration = value; }
        }

        public Configurator Configurator
        {
            get { return _Configurator; }
            set { _Configurator = value; }
        }

        /// <summary>
        /// Min Meter Time Connection Should be Connected From Server
        /// </summary>
        public TimeSpan MinMeterConnectionTime
        {
            get { return minMeterConnectionTime; }
            set { minMeterConnectionTime = value; }
        }

        public bool EnableTimeSlotMonitoring
        {
            get { return enableSlotMonitoring; }
            set { enableSlotMonitoring = value; }
        }

        public TimeSpan DefaultTimeSlot
        {
            get { return defaultTimeSlot; }
            set
            {
                if (value < MinTimeSlot)
                    defaultTimeSlot = MinTimeSlot;
                else
                    defaultTimeSlot = value;
            }
        }

        public TimeSpan MaxSessionResetDuration
        {
            get { return _MaxSessionResetDuration; }
            set { _MaxSessionResetDuration = value; }
        }

        public GetDataReaderBuffer CreateDataReaderBuffer
        {
            get { return _CreateDataReaderBuffer; }
            set { _CreateDataReaderBuffer = value; }
        }

        public ILogWriter Activity_DataLogger
        {
            get;
            set;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default MeterConnectionManager Constructor
        /// </summary>
        public MeterConnectionManager()
        {
            try
            {
                //Init MeterConnection Manager
                Debugger = new Debugger();
            }
            catch (Exception ex)
            {
                //throw ex
                throw ex;
            }
        }

        #endregion

        #region Member_Methods

        /// <summary>
        /// Determine either meter device with particular MSN,currently allocated/connected 
        /// </summary>
        /// <param name="SerialNumber"></param>
        /// <returns></returns>
        public bool IsMeterConnectionAllocated(MeterSerialNumber SerialNumber)
        {
            try
            {
                bool isAllocated = false;
                IOConnection Conn = ConnectionManager.GetConnectedIOConnection(SerialNumber);
                if (Conn != null)
                {
                    isAllocated = IsMeterConnectionAllocated(Conn);
                }
                return isAllocated;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool IsMeterConnectionAllocated(IOConnection IOConn)
        {
            try
            {
                return ThreadAllocator.IsMeterConnectionAllocated(IOConn.MeterSerialNumberObj);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Determine either meter device with particular MSN,currently login
        /// </summary>
        /// <param name="SerialNumber"></param>
        /// <returns></returns>
        public bool IsMeterLogin(MeterSerialNumber SerialNumber)
        {
            bool isLogin = false;
            try
            {
                if (SerialNumber != null)
                {
                    ApplicationController Contr = ThreadAllocator.GetControllerConnectionAllocated(SerialNumber);
                    if (Contr != null && Contr.Applicationprocess_Controller != null &&
                       Contr.Applicationprocess_Controller.IsConnected)
                        isLogin = true;
                }
            }
            catch { }
            return isLogin;
        }

        public bool IsMeterLogin(IOConnection IOConn)
        {
            bool isLogin = false;
            try
            {
                if (IOConn != null)
                {
                    ApplicationController Contr = ThreadAllocator.GetControllerConnectionAllocated(IOConn.MeterSerialNumberObj);
                    if (Contr != null && Contr.Applicationprocess_Controller != null &&
                       Contr.Applicationprocess_Controller.GetCommunicationObject == IOConn &&
                       Contr.Applicationprocess_Controller.IsConnected)
                        isLogin = true;
                }
            }
            catch { }
            return isLogin;
        }

        //public long GetCountMeterConnectionAllocated()
        //{
        //    long _t = -1;
        //    try
        //    {
        //        _t = ThreadAllocator.ConnectionThreadsAllocated.Count<KeyValuePair<MeterSerialNumber, ApplicationController>>((x) =>
        //        {
        //            return (x.Key != null && x.Value != null && x.Value.IsAllocated &&
        //                x.Key == x.Value.ConnectionController.CurrentConnection.MeterSerialNumberObj);
        //        });
        //    }
        //    catch (Exception) { }
        //    return _t;
        //}

        //public long GetCountMeterConnections()
        //{
        //    long _t = -1;
        //    try
        //    {
        //        _t = ThreadAllocator.ConnectionThreadsAllocated.Count<KeyValuePair<MeterSerialNumber, ApplicationController>>((x) =>
        //        {
        //            return (x.Key != null && x.Value != null && x.Value.IsAllocated &&
        //                x.Key == x.Value.ConnectionController.CurrentConnection.MeterSerialNumberObj &&
        //                x.Value.Applicationprocess_Controller.IsConnected);
        //        });
        //    }
        //    catch (Exception) { }
        //    return _t;
        //}

        public long GetCountMeterConnections_KA()
        {
            long _t = -1;
            try
            {
                _t = ConnectedMeterList.LongCount<KeyValuePair<MeterSerialNumber, IOConnection>>((x) =>
                {
                    return (x.Value != null && x.Value.CurrentConnection == PhysicalConnectionType.KeepAlive);
                });
            }
            catch { }
            return _t;
        }

        public long GetCountMeterConnections_NKA()
        {
            long _t = -1;
            try
            {

                _t = ConnectedMeterList.LongCount<KeyValuePair<MeterSerialNumber, IOConnection>>((x) =>
                {
                    return (x.Value != null && x.Value.CurrentConnection == PhysicalConnectionType.NonKeepAlive);
                });
            }
            catch (Exception) { }
            return _t;
        }

        #endregion

        #region Support_Method

        public void Init_MeterConnectionManager()
        {
            try
            {
                // Init MeterConnection Manager
                MeterLogicController = ArrayList.Synchronized(new ArrayList((int)MaxMeterConnection));
                ThreadAllocator = new ConnectionThreadAllocater(this);
                ThreadAllocator.Init_ConnectionThread();
                taskRunner = new DataTaskAllocater(this);
                if (Debugger != null)
                {
                    Debugger.GetDLMSLoggersDlg = new Action<ArrayList>(GetDLMSLoggers);
                    Debugger.Reset_Debugger();
                }
                DLMSClassMaker = new DLMSClassFactory();
                ((DLMSClassFactory)dLMSClassMaker).GetSAPEntryDelegate = new GetSAPEntryKeyIndex(GetSAPEntryKeyIndexer);
                ((DLMSClassFactory)dLMSClassMaker).GetSAPAccessRightsDelegate = new GetSAPRights((x) => { return null; }); // nullable rights
                DLMSInstancesCache = new LRUPriorityDLMSCache(ObjectCacheMinAgeTime, ObjectCahceMaxAgeTime,
                    new DLMSCalculatePriority(this.DefaultPriorityComputer), new GetSAPEntryKeyIndex(DLMSInstanceMaker));
                // SAPTables = new ConcurrentDictionary<string, SAPTable>();

                // Init Application Event Dispatcher
                _appEventDispatcher = new AsyncEventDispatcher();
                _appEventPool = new EventPool();
                // Register Major Alarm Event Notification Handler
                _appEventDispatcher.AddListener<MajorAlarmNotification>(new EventHandlerDelegate<MajorAlarmNotification>(taskRunner.MajorAlarmNotification_Handler));


                MajorAlarmNotification mjrAlarmNotify = null;
                MSN_Notification msnNotify = null;

                // Pool MajorAlarmNotification
                // Pool MSNNotification
                for (int counter = 1; counter <= 50; counter++)
                {
                    mjrAlarmNotify = new MajorAlarmNotification();
                    //  msnNotify = new MSN_Notification();

                    _appEventPool.TryAdd<MajorAlarmNotification>(mjrAlarmNotify);
                    // _appEventPool.TryAdd<MSN_Notification>(msnNotify);
                }
            }
            catch(Exception ex)
            {
                LocalCommon.SaveApplicationException(new Exception("Main Init_MeterConnectionManager "), 1);
                LocalCommon.SaveApplicationException(ex, 1);
            }
        }

        internal ApplicationController InitApplicationController()
        {
            try
            {
                ApplicationController NEw_AppProcess = MDC_ObjectFactory.GetApplicationControllerObject();//  new ApplicationController();
                NEw_AppProcess.Applicationprocess_Controller.OwnerThreadId = OwnerId++;
                // Init Work Here
                NEw_AppProcess.Applicationprocess_Controller.GetSAPEntryDlg = new GetSAPEntryKeyIndex(GetSAPEntryKeyIndexer);
                NEw_AppProcess.Configurations = Configuration;
                NEw_AppProcess.Configurator = Configurator;
                NEw_AppProcess.ApplicationEventDispatcher = ApplicationEventDispatcher;
                NEw_AppProcess.ApplicationEventPool = ApplicationEventPool;
                if (Debugger != null)
                    Debugger.Register = NEw_AppProcess.Applicationprocess_Controller.ApplicationProcess.Logger;
                return NEw_AppProcess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void PartialInitApplicationController(ApplicationController NEw_AppProcess)
        {
            try
            {
                ///Init Work Here
                NEw_AppProcess.Applicationprocess_Controller.GetSAPEntryDlg = new GetSAPEntryKeyIndex(GetSAPEntryKeyIndexer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DLMSCachePriority DefaultPriorityComputer(KeyIndexer OBISIndex)
        {
            try
            {
                if (OBISIndex.ObisCode.ClassId == 7 || OBISIndex.ObisCode.ClassId == 17)
                {
                    return DLMSCachePriority.HighPriority;
                }
                else
                    return DLMSCachePriority.LowPriority;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Base_Class DLMSInstanceMaker(KeyIndexer OBIS_CODE)
        {
            try
            {
                StOBISCode OBISCode = OBIS_CODE.ObisCode;
                Base_Class obj = DLMSClassMaker.DLMS_FactoryMethod(OBISCode);
                obj.OwnerId = OBIS_CODE.OwnerId;
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Base_Class GetSAPEntryKeyIndexer(KeyIndexer ObjectKey)
        {
            try
            {
                ///***Modification By Pass DLMS Object Cache
                Base_Class obj = DLMSInstancesCache.GetBaseObject(ObjectKey);
                return obj;
                //StOBISCode OBISCode = ObjectKey.ObisCode;
                //Base_Class obj = DLMSClassMaker.DLMS_FactoryMethod(OBISCode);
                //obj.OwnerId = ObjectKey.OwnerId;
                //return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetDLMSLoggers(ArrayList LoggerList)
        {
            try
            {
                if (LoggerList == null)
                    return;
                LoggerList.Clear();
                var TList =
                    from ApplicationController entry in MeterLogicController.ToArray(typeof(ApplicationController))
                    where entry != null && entry.Applicationprocess_Controller != null &&
                          entry.Applicationprocess_Controller != null &&
                          entry.Applicationprocess_Controller.ApplicationProcess != null &&
                          entry.Applicationprocess_Controller.ApplicationProcess.Logger != null &&
                          entry.Applicationprocess_Controller.ApplicationProcess.Logger.TaskList.Count > 0
                    select entry.Applicationprocess_Controller.ApplicationProcess.Logger;

                foreach (DLMSLogger item in TList)
                {
                    if (item != null)
                    {
                        LoggerList.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                this.Activity_DataLogger = null;
                
                // Dispose Off Member Methods
                if (ThreadAllocator != null)
                {
                    ThreadAllocator.Dispose();
                    ThreadAllocator = null;
                }
                if (Debugger != null)
                {
                    Debugger.Dispose();
                    Debugger = null;
                }
                if (dLMSInstancesCache != null)
                {
                    dLMSInstancesCache.Dispose();
                    dLMSInstancesCache = null;
                }
                if (_appEventDispatcher != null)
                {
                    _appEventDispatcher.Dispose();
                    _appEventDispatcher = null;
                }

                if (_appEventPool != null)
                {
                    _appEventPool = null;
                }

                if (Configuration != null)
                {
                    configuration.Dispose();
                    configuration = null;
                }
                
                // _SAPTables = null;
                
                ConnectionManager = null;
                if (MeterLogicController != null)
                {
                    foreach (ApplicationController contr in MeterLogicController)
                    {
                        if (contr != null)
                            contr.Dispose();
                    }
                    MeterLogicController.Clear();
                    MeterLogicController = null;
                }
                dLMSClassMaker = null;
            }
            catch
            { }
        }

        #endregion

        ~MeterConnectionManager()
        {
            try
            {
                Dispose();
            }
            catch (Exception) { }
        }

        /// <summary>
        /// ConnectionThreadAllocator is inner class of MeterConnectionManager to access feilds and mthods of
        /// of Container class to perform its tasks
        /// </summary>
        public partial class ConnectionThreadAllocator
        { }
    }
}
