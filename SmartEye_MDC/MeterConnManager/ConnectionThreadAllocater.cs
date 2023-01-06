// #define Enable_DEBUG_ECHO
// #define Enable_DEBUG_RunMode
// #define Enable_Error_Logging
// #define Enable_Transactional_Logging

using comm;
using Commuincator.MeterConnManager;
using Communicator.comm;
using Communicator.MTI_MDC;
using Communicator.Properties;
using DatabaseManager.Database;
using DLMS;
using SharedCode.Comm.DataContainer;
using SharedCode.Comm.HelperClasses;
using SharedCode.Common;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace Communicator.MeterConnManager
{
    public class ConnectionThreadAllocater : IDisposable
    {
        #region Data_Members
        private static int CummulativeCouter = 0;
        private MeterConnectionManager Owner;
        private static readonly TimeSpan MinConnnectionAllocateTime = new TimeSpan(00, 00, 30);
        private ConcurrentDictionary<MeterSerialNumber, ApplicationController> ConnThreadsAllocated;
        private ConcurrentDictionary<DeAllocate_Key, ApplicationController> premtiveThreadDeallocated;

        private Thread connAllocaterThreads;
        private Thread connDeAllocaterThread;
        private int maxConnProcessCount = 512;

        public static readonly TimeSpan AllocationTimeOut = TimeSpan.FromSeconds(30);
        public static readonly TimeSpan DeallocationTimeOut = TimeSpan.FromSeconds(70);
        public static readonly ParallelOptions ConnectionRunnableOpt = new ParallelOptions() { MaxDegreeOfParallelism = 25 };

        private Thread KA_Sch_Thread;
        // private bool stratKeepAlive_Scheduler = false;
        internal ConcurrentDictionary<string, ScheduledRequest> connToAllocate;
        internal ConcurrentDictionary<string, ScheduledRequest> connProcessed;

        internal int CNA_Counter;
        internal MDC_Status _MDC_Status_Obj;
        internal long KA_Alloc_Count;
        internal long Alloc_Count;
        internal long active_Count;
        internal long KA_active_Count;
        internal int ConditionHitCount;

        #endregion

        #region Constructors

        public ConnectionThreadAllocater(MeterConnectionManager OwnerInstance)
        {
            Owner = OwnerInstance;

        }

        #endregion

        #region Properties

        public MDC_Status MDC_Status_Obj
        {
            get { return _MDC_Status_Obj; }
            set { _MDC_Status_Obj = value; }
        }

        public long KA_Allocated_Count
        {
            get
            {
                return Interlocked.Read(ref KA_Alloc_Count);
            }
        }

        public long Allocated_Count
        {
            get
            {
                return Interlocked.Read(ref Alloc_Count);
            }
        }

        public long NKA_Allocated_Count
        {
            get
            {
                return (Allocated_Count - KA_Allocated_Count);
            }
        }

        public long Active_Connection_Count
        {
            get
            {
                return Interlocked.Read(ref active_Count);
            }
        }

        public long KA_Active_Connection_Count
        {
            get
            {
                return Interlocked.Read(ref KA_active_Count);
            }
        }

        public long NKA_Active_Connection_Count
        {
            get
            {
                return Active_Connection_Count - KA_Active_Connection_Count;
            }
        }

        public int MaxConnectionProcessCount
        {
            get { return maxConnProcessCount; }
            set { maxConnProcessCount = value; }
        }

        public bool IsConnectionAllocated
        {
            get
            {
                try
                {
                    //if (Owner.MeterLogicController.Count < Owner.MaxMeterConnection)
                    //{
                    //    return true;
                    //}
                    //else if (ConnectionThreadsAllocated.Count < Owner.MaxMeterConnection)
                    //{
                    //    return true;
                    //}
                    //else
                    //{
                    //    //foreach (ApplicationController controller in ConnectionThreadsAllocated)
                    //    //{
                    //    //    if (!controller.IsAllocated)
                    //    //        return true;
                    //    //    else
                    //    //        continue;
                    //    //}
                    //}
                    //return false;

                    return Allocated_Count < Owner.MaxMeterConnection;
                }
                catch
                {
                    return false;
                }
            }
        }

        internal ConcurrentDictionary<MeterSerialNumber, ApplicationController> ConnectionThreadsAllocated
        {
            get { return ConnThreadsAllocated; }
            set { ConnThreadsAllocated = value; }
        }

        internal ConcurrentDictionary<DeAllocate_Key, ApplicationController> PremtiveThreadDeallocated
        {
            get { return premtiveThreadDeallocated; }
        }

        public bool IsConnectionAllocaterRunning
        {
            get
            {
                return Commons.IsThreadRunning(connAllocaterThreads);
            }
        }

        public bool IsConnectionDeAllocaterRunning
        {
            get
            {
                return Commons.IsThreadRunning(connDeAllocaterThread);
            }
        }

        public bool IsKA_SchedularRunning
        {
            get
            {
                return Commons.IsThreadRunning(KA_Sch_Thread);
            }
        }

        public bool IsThreadAllocaterRunning
        {
            get
            {
                if (IsConnectionAllocaterRunning &&
                    IsConnectionDeAllocaterRunning)
                    return true;
                else
                    return false;
            }
        }

        #endregion

        #region Member_Methods

        /// <summary>
        /// Runs Connection Allocator thread to be executed
        /// </summary>
        [HostProtectionAttribute(Synchronization = true, ExternalThreading = true)]
        public void ConnectionAllocaterRunable()
        {
            ArrayList connProcess = ArrayList.Synchronized(new ArrayList());
            try
            {
                Func<bool> IsAllocateConnection = new Func<bool>(() => Owner.ConnectedMeterList != null &&
                                                                       Allocated_Count < Owner.MaxMeterConnection &&
                                                                       Allocated_Count < Owner.ConnectedMeterList.Count);

                // int index = 0;
                while (true)
                {
                    // TimeSpan T = DateTime.Now.TimeOfDay;
                    bool IsConnectionProcessed = false;
                    try
                    {
                        #region //Still Some IOConnections Exists To be Processed

                        if (Owner.ConnectedMeterList != null &&
                            Owner.ConnectedMeterList.Count > 0)
                        {
                            try
                            {
                                int maxCountMain = Owner.ConnectedMeterList.Count;
                                long IterationCount = maxCountMain - (Allocated_Count);

                                #region Process Internal Counters

                                if (IterationCount > maxCountMain)
                                    IterationCount = maxCountMain;

                                if ((IterationCount > 0 && IterationCount < 1024) ||
                                    (IterationCount < 0 && Allocated_Count < Owner.MaxMeterConnection))
                                    IterationCount = maxCountMain;

                                #endregion

                                // Extract IOConnections From Owner.ConnectedMeterList
                                var Iterator = from Conn in Owner.ConnectedMeterList //.AsParallel()
                                               where Conn.Value != null &&
                                                Conn.Value.CurrentConnection != PhysicalConnectionType.KeepAlive &&
                                                !Conn.Value.IsAssigned &&
                                                (Conn.Value.MeterSerialNumberObj == null ||
                                                !IsMeterConnectionAllocated(Conn.Value.MeterSerialNumberObj))
                                               orderby Conn.Value.ConnectionTime ascending
                                               select Conn.Value;

                                #region Debugging & Logging
#if Enable_DEBUG_ECHO
                                if (this.Owner != null &&
                                    this.Owner.Activity_DataLogger != null)
                                    Commons.WriteError(Owner.Activity_DataLogger, "Starting Adding Connection To connProcess");
                                else
                                    Commons.WriteError(MDC.Default_DataLogger, "Starting Adding Connection To connProcess");
#endif
                                #endregion

                                // Parallel.ForEach<IOConnection>(Iterator, (Conn, LoopState) =>
                                foreach (var Conn in Iterator)
                                {
                                    try
                                    {
                                        int countT = connProcess.Count;
                                        if (countT > IterationCount || countT >= 1024 || countT > (Owner.MaxMeterConnection / 3))
                                            break; //LoopState.Break();
                                        else if (Conn != null && !connProcess.Contains(Conn))
                                        {
                                            IsConnectionProcessed = true;
                                            connProcess.Add(Conn);
                                        }
                                    }
                                    catch
                                    {
                                        throw;
                                    }
                                } //);

                                #region Sort Meter Incoming Connections On Time

                                //if (IsConnectionProcessed)
                                //{
                                //    connProcess.Sort(new IConnection_ConnectionTimeSortHelper());
                                //}

                                #endregion
                                #region Debugging & Logging
#if Enable_DEBUG_ECHO

                                if (this.Owner != null &&
                                    this.Owner.Activity_DataLogger != null)
                                    Commons.WriteError(Owner.Activity_DataLogger, "Added Connection To connProcess" + connProcess.Count);
                                else
                                    Commons.WriteError(MDC.Default_DataLogger, "Added Connection To connProcess" + connProcess.Count);
#endif
                                #endregion
                            }
                            catch
                            {
                                IsConnectionProcessed = false;
                            }
                        }

                        #endregion
                        #region Dispose Off Old ApplicationController

                        try
                        {
                            if (!IsConnectionProcessed)
                            {
                                if (Allocated_Count > Owner.MinMeterConnection)
                                {
                                    Owner.MinMeterConnectionDuration = TimeSpan.MinValue;
                                }
                                else if (Allocated_Count <= Owner.MinMeterConnection &&
                                    Owner.MinMeterConnectionDuration == TimeSpan.MinValue)
                                {
                                    Owner.MinMeterConnectionDuration = DateTime.Now.TimeOfDay;
                                }
                                if (Owner.MeterLogicController.Count > Owner.MinMeterConnection &&
                                    Allocated_Count <= Owner.MinMeterConnection &&
                                    Owner.IsMinMeterConnDurationValid)
                                {
                                    ApplicationController Contr = GetApplicationControllerForDeallocation();
                                    if (Contr != null)
                                        Contr.Dispose();
                                }
                            }
                        }
                        catch (Exception)
                        { }

                        #endregion
                        #region // New Connections Waiting to be assigned

                        if (Allocated_Count < Owner.MaxMeterConnection)
                        {
                            #region New Keep Alive Connection Waiting to be assigned

                            //if (Owner.KeepAliveScheduler.ConnToAllocate != null &&
                            //    Owner.KeepAliveScheduler.ConnToAllocate.Count > 0)
                            //{
                            //    Parallel.ForEach<string>(Owner.KeepAliveScheduler.ConnToAllocate.Keys, ConnectionThreadAllocater.ConnectionRunnableOpt,
                            //        (dt, ParallelLoopState) =>
                            //        {
                            //            try
                            //            {
                            //                if (KA_Allocated_Count >= Owner.MaxKeepAliveMeterConnections)
                            //                    ParallelLoopState.Break();
                            //                if (dt != null)
                            //                {
                            //                    ScheduledRequest KP = Owner.KeepAliveScheduler.ConnToAllocate[dt];
                            //                    if (KP.Request_Status == Request_Status.ScheduleRead)
                            //                    {
                            //                        ProcessKeepAliveReq(KP);
                            //                    }
                            //                    Owner.KeepAliveScheduler.ConnToAllocate.TryRemove(dt, out KP);
                            //                    if (KP.Request_Status == Request_Status.Assigned ||
                            //                        KP.Request_Status == Request_Status.ConnectionNotAvailable)
                            //                        Owner.KeepAliveScheduler.ConnProcessed.TryAdd(dt, KP);
                            //                }
                            //            }
                            //            catch (Exception)
                            //            {
                            //                ParallelLoopState.Break();
                            //            }
                            //        });
                            //}

                            #endregion
                            #region //New Non Keep Alive Connections Waiting to be assigned

                            var Iterator = from IOConnection x in connProcess
                                           where true
                                           orderby x.ConnectionTime ascending
                                           select x;

                            var ILConnIterator = Partitioner.Create<IOConnection>(Iterator.AsEnumerable<IOConnection>());

                            //if (connProcess.Count > 0)
                            //    Commons.WriteError("Starting Allocating Connections." + connProcess.Count);
                            //int _UpperIndex = connProcess.Count;

                            /// Parallel.ForEach<IOConnection>(ILConnIterator, ConnectionThreadAllocater.ConnectionRunnableOpt, (Conn, LoopState) =>
                            foreach (var Conn_var in Iterator)
                            {
                                IOConnection Conn = Conn_var;
                                try
                                {
                                    // Conditional Breaking
                                    if ((Allocated_Count - KA_Allocated_Count) > Owner.MaxNonKeepAliveMeterConnections)
                                        //LoopState.Break();
                                        break;
                                    // Conn = (IOConnection)connProcess[_Index];
                                    if (Conn == null)
                                    {
                                        break;
                                        // LoopState.Break();
                                    }

                                    #region Get_MeterInfo

                                    try
                                    {
                                        MeterSerialNumber msn = null;
                                        // Remove Duplicate Physical IOConnection If Exists
                                        if (Conn != null && Conn.ConnectionInfo != null &&
                                            Conn.MeterSerialNumberObj != null &&
                                            Conn.MeterSerialNumberObj.IsMSN_Valid)
                                        {
                                            msn = Conn.MeterSerialNumberObj;
                                            // Remove Duplicate Connections disconnected From ConnectedMeterList
                                            Owner.ConnectionManager.RemoveDuplicateConnections(Owner.ConnectedMeterList, msn, true);
                                            IOConnection _Conn = Owner.ConnectionManager.GetConnectedIOConnection(Conn.MeterSerialNumberObj);
                                            // By Pass Check
                                            if (_Conn != null)
                                            {
                                                Conn = _Conn;
                                            }
                                        }

                                        // if (Conn.ConnectionInfo == null ||
                                        //     (Conn.MeterSerialNumberObj != null &&
                                        //     Conn.MeterSerialNumberObj.IsMSN_Valid &&
                                        //     !Conn.ConnectionInfo.IsInitialized))
                                        //     Conn.ConnectionInfo = ConnectionInitUtil.GetMeterConnectionInfo(Conn.MeterSerialNumberObj, Owner.Configuration);
                                    }
                                    catch (Exception)
                                    { }

                                    #endregion

                                    if (Conn != null && Conn.IsConnected)
                                    {
                                        if (IsConnectionAllocated)
                                        {
                                            ApplicationController COntr = null;
                                            if (Conn.MeterSerialNumberObj == null)
                                            {
                                                try
                                                {
                                                    // Assign Random MeterSerialNumber
                                                    MeterSerialNumber MSN = null;
                                                    do
                                                    {
                                                        MSN = ConnectionInitUtil.GetRandomSerialNumber();
                                                        Conn.MeterSerialNumberObj = MSN;
                                                        if (!IsMeterConnectionAllocated(MSN))
                                                        {
                                                            TryAllocateMeterConnection(Conn, out COntr);
                                                            break;
                                                        }
                                                        else
                                                            continue;

                                                    } while (true);
                                                }
                                                catch
                                                { }
                                            }
                                            else
                                            {
                                                TryAllocateMeterConnection(Conn, out COntr);
                                            }
                                            if (COntr == null)
                                            {
                                                Owner.ConnectedMeterList.Remove(Conn);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Owner.ConnectedMeterList.Remove(Conn);
                                    }
                                }
                                catch (Exception)
                                {
                                    break;
                                    //LoopState.Break();
                                }
                            }//);

                            #endregion
                        }

                        #endregion
                        // if (connProcess.Count > 0)
                        //     Commons.WriteError("Allocating Connections complete." + connProcess.Count);
                        connProcess.Clear();

                        #region // Wait Delay

                        // if (DateTime.Now.TimeOfDay.Subtract(T) < MinConnnectionAllocateTime)
                        //     Thread.Sleep(TimeSpan.FromSeconds(0.050));

                        Commons.DelayUntil(IsAllocateConnection, Convert.ToInt64(MinConnnectionAllocateTime.TotalMilliseconds));

                        #endregion

                    }
                    catch (Exception ex)
                    {
                        #region Debugging & Logging
#if Enable_DEBUG_ECHO

                        if (this.Owner != null &&
                                    this.Owner.Activity_DataLogger != null)
                            Commons.WriteError(Owner.Activity_DataLogger, "Error Handling Exceptions " + ex);
                        else
                            Commons.WriteError(MDC.Default_DataLogger, "Error Handling Exceptions " + ex);
#endif
                        #endregion

                        if (Owner != null &&
                            Owner.Activity_DataLogger != null)
                        {
                            Owner.Activity_DataLogger.LogMessage("Error Handling Exceptions ", ex, 10);
                        }

                        throw ex;
                    }
                }
            }
            catch (ThreadAbortException ex)
            {
                #region Debugging & Logging
#if Enable_DEBUG_ECHO
                Commons.WriteError(MDC.Default_DataLogger, "ConnectionAllocaterRunable function is being terminated_ThreadAbortException," + ex.Message);
#endif
                #endregion
                string path = LocalCommon.GetApplicationConfigsDirectory() + @"Exceptions\ConnectionAllocaterRunnableError.txt";
                LocalCommon.SaveException(ex, path);

                if (Owner != null &&
                    Owner.Activity_DataLogger != null)
                {
                    Owner.Activity_DataLogger.LogMessage("ConnectionAllocaterRunable function is being terminated_ThreadAbortException,", ex, 10);
                }

                return;
            }
            catch (Exception ex)
            {
                try
                {
                    #region Debugging & Logging
#if Enable_DEBUG_ECHO

                    if (this.Owner != null &&
                                    this.Owner.Activity_DataLogger != null)
                        Commons.WriteError(Owner.Activity_DataLogger, "Error Occurred While processing Connections_Exception" + ex.Message);
                    else
                        Commons.WriteError(MDC.Default_DataLogger, "Error Occurred While processing Connections_Exception" + ex.Message);
#endif
                    #endregion
                    string path = LocalCommon.GetApplicationConfigsDirectory() + @"Exceptions\ConnectionAllocaterRunnableError.txt";
                    LocalCommon.SaveException(ex, path);

                    if (Owner != null &&
                        Owner.Activity_DataLogger != null)
                    {
                        Owner.Activity_DataLogger.LogMessage("Error Occurred While processing Connections_Exception", ex, 10);
                    }

                    Init_ConnectionThread();
                }
                catch
                { }
                // throw ex;

            }
        }

        public void ConnectionDeAllocatorRunable()
        {
            try
            {
                while (true)
                {
                    try
                    {
                        #region  // Reset & Save MDC Status Object

                        try
                        {
                            // if (!MDC_Status_Obj.mdc_status_sync)
                            // {
                            //MDC_Status_Obj.sync_mdcStatus();
                            //     MDC_Status_Obj.mdc_status_sync = true;
                            //  }

                            if (MDC_Status_Obj.IsSessionPeriodElapsed)
                            {
                                MDC_Status LocalStatus = null;
                                long Connection_Count = Owner.ConnectedMeterList.Count;
                                long KA_Connection_Count = Owner.GetCountMeterConnections_KA();
                                MDC_Status_Obj.NKA_Connection_Count = (Connection_Count - KA_Connection_Count);
                                MDC_Status_Obj.KA_Connection_Count = KA_Connection_Count;
                                MDC_Status_Obj.SessionStop = DateTime.Now;
                                lock (MDC_Status_Obj)
                                {
                                    LocalStatus = new MDC_Status(MDC_Status_Obj);
                                    MDC_Status_Obj.Reset();
                                }
                                //Update Duration
                                if (MDC_Status_Obj.Duration == TimeSpan.MinValue || MDC_Status_Obj.Duration == TimeSpan.MaxValue)
                                {
                                    MDC_Status_Obj.Duration = MDC_Status.DefaultDurationPeriod;
                                }
                                using (DatabaseController Local_DBController = new DatabaseController())
                                {
                                    if (Settings.Default.SaveMDCStatus)
                                    {
                                        Local_DBController.insert_mdc_status(LocalStatus);
                                    }
                                    MDC_Status_Obj.Duration = Settings.Default.StatisticsUpdateSessionTime;
                                }
                            }
                        }
                        catch
                        { }

                        #endregion
                        #region DeAllocate Threads

                        ArrayList ConToDis = GetConnectionsDeallocate();
                        foreach (IOConnection conToDis in ConToDis)
                        {
                            if (conToDis != null)
                            {
                                //Commented ConnStatus
                                //conToDis.InsertLogMessage("DeAllocateMeterConnection_Called From_ConnectionDeAllocatorRunable_ConnectionThreadAllocater");
                                ApplicationController contrl = GetControllerConnectionAllocated(conToDis.MeterSerialNumberObj);
                                TryDeAllocateMeterConnection(conToDis, contrl);
                            }
                        }

                        #endregion
                        if (ConToDis.Count <= 0)
                            Thread.Sleep(TimeSpan.FromSeconds(0.50));
                    }
                    catch (ThreadAbortException ex)
                    {
                        throw ex;
                    }
                    catch (Exception ex)
                    {
                        #region Debugging & Logging
#if Enable_DEBUG_ECHO

                        if (this.Owner != null &&
                                    this.Owner.Activity_DataLogger != null)
                            Commons.WriteError(Owner.Activity_DataLogger, "Error Handling Exceptions " + ex);
                        else
                            Commons.WriteError(MDC.Default_DataLogger, "Error Handling Exceptions " + ex);
#endif
                        #endregion
                    }
                }
            }
            catch (ThreadAbortException ex)
            {
#if Enable_DEBUG_ECHO

                if (this.Owner != null &&
                                    this.Owner.Activity_DataLogger != null)
                    Commons.WriteError(Owner.Activity_DataLogger, "ConnectionDeAllocaterRunable function is being terminated," + ex.Message);
                else
                Commons.WriteError(MDC.Default_DataLogger, "ConnectionDeAllocaterRunable function is being terminated," + ex.Message);

#endif

                // string path = Commons.GetApplicationConfigsDirectory() + @"\Schedule_Errors.txt";
                // Commons.SaveException(ex, path);
                return;
            }
            catch (Exception ex)
            {

                try
                {

#if Enable_DEBUG_ECHO
                    
                    if (this.Owner != null &&
                        this.Owner.Activity_DataLogger != null)
                        Commons.WriteError(Owner.Activity_DataLogger, "Error Occurred While processing Connections " + ex.Message);
                    else
                        Commons.WriteError(MDC.Default_DataLogger, "Error Occurred While processing Connections " + ex.Message);

#endif

                    // string path = Commons.GetApplicationConfigsDirectory() + @"\Schedule_Errors.txt";
                    // Commons.SaveException(ex, path);
                    Init_ConnectionThread();
                }
                catch (Exception)
                { }
                // throw ex;
            }
        }

        public bool IsMeterConnectionAllocated(MeterSerialNumber IOConn)
        {
            bool isExists = false;
            try
            {
                if (IOConn != null)
                {
                    ApplicationController Contr = null;
                    ConnectionThreadsAllocated.TryGetValue(IOConn, out Contr);
                    if (Contr != null && Contr.IsAllocated)
                        isExists = true;
                }
            }
            catch (Exception)
            { }
            return isExists;
        }

        public ApplicationController GetControllerConnectionAllocated(MeterSerialNumber IOConn)
        {
            ApplicationController contr = null;
            try
            {
                if (IOConn != null)
                {
                    ConnectionThreadsAllocated.TryGetValue(IOConn, out contr);
                }
            }
            catch (Exception ex)
            { }
            return contr;
        }

        //public void CreateMeterConnection(IOConnection IOConn)
        //{
        //    try
        //    {
        //        if (IOConn.ConnectionInfo != null &&
        //            !IsMeterConnectionAllocated(IOConn.MeterSerialNumberObj))
        //        {
        //            if (!IOConn.IsChannelConnected)
        //                throw new Exception(String.Format("{0} physical connection disconnected", IOConn));
        //            ApplicationController appContr = GetApplicationControllerForAllocation();
        //            if (appContr == null)
        //            {
        //                appContr = Owner.InitApplicationController();
        //                Owner.MeterLogicController.Add(appContr);
        //            }
        //            AllocateMeterConnection(IOConn, appContr);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Unable to allocate meter connection", ex);
        //    }
        //}

        //public void DisconnectMeterConnection(MeterSerialNumber IOConn)
        //{
        //    try
        //    {
        //        if (IOConn != null && IsMeterConnectionAllocated(IOConn))
        //        {
        //            ApplicationController Contr = GetControllerConnectionAllocated(IOConn);
        //            Contr.ThreadCancelToken.Cancel(true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(String.Format("Unable to disconnect meter connection {0}", IOConn), ex);
        //    }
        //}

        //public void DisconnectAll()
        //{
        //    try
        //    {
        //        foreach (var conn in ConnectionThreadsAllocated.Keys.ToList<MeterSerialNumber>())
        //        {
        //            if (conn != null)
        //            {
        //                MeterSerialNumber mtrconn = conn;
        //                DisconnectMeterConnection(mtrconn);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        #endregion

        #region Support_Method

        public void Init_ConnectionThread()
        {
            // Initialize Work Here
            ConnectionThreadsAllocated = new ConcurrentDictionary<MeterSerialNumber, ApplicationController>(1024, (int)Owner.MaxMeterConnection);
            premtiveThreadDeallocated = new ConcurrentDictionary<DeAllocate_Key, ApplicationController>(100, (int)Owner.MaxMeterConnection, new DeAllocate_KeyComparer());
            _MDC_Status_Obj = new MDC_Status(Settings.Default.StatisticsUpdateSessionTime);

            #region // Initialize Connection Allocate Threads Count

            try
            {
                if (connAllocaterThreads != null)
                    connAllocaterThreads.Abort();
            }
            catch (Exception)
            { }
            connAllocaterThreads = new System.Threading.Thread(ConnectionAllocaterRunable);
            connAllocaterThreads.Priority = ThreadPriority.Highest;
            connAllocaterThreads.Start();

            #endregion
            #region Initialize Connection Deallocator Thread

            try
            {
                if (connDeAllocaterThread != null)
                {
                    connDeAllocaterThread.Abort();
                }
            }
            catch (Exception)
            { }

            connDeAllocaterThread = new System.Threading.Thread(ConnectionDeAllocatorRunable);
            connDeAllocaterThread.Start();
            #endregion
            #region Initialize Keep Alive SchedularThread

            // if (Owner.KeepAliveScheduler != null)
            //     Owner.KeepAliveScheduler.MDC_Status_Obj = _MDC_Status_Obj;

            #endregion
        }

        #region KeepAliveSchedular

        public void Start_KeepAliveSchedular()
        {
            try
            {
                if (KA_Sch_Thread != null && KA_Sch_Thread.IsAlive)
                {
                    try
                    {
                        KA_Sch_Thread.Abort();
                        KA_Sch_Thread = null;
                    }
                    catch (Exception)
                    { }
                }
                KA_Sch_Thread = new Thread(KeepAlivePooling);
                KA_Sch_Thread.Start();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occurred while starting Non Keep Alive Scheduler,{0}", ex.Message), ex);
            }
        }

        public void KeepAlivePooling()
        {
            try
            {
                TimeSpan TPollPeriod = Settings.Default.SchedulerPoolingTime;
                func_KeepAlive(TPollPeriod, false);
            }
            catch (Exception)
            {
                return;
            }
        }

        public void func_KeepAlive(TimeSpan PollingTime, bool isPollingNow)
        {

        }

        public bool ProcessKeepAliveReq(ScheduledRequest KeepAliveReq)
        {
            return false;
        }

        #endregion

        private ApplicationController GetApplicationControllerForAllocation()
        {
            try
            {
                ApplicationController Contr = null;
                ///Check if already ConnectionThread deallocated in Owner.MeterLogicController
                ///Parallel.For(0, Owner.MeterLogicController.Count, (index, LoopState) =>
                for (int index = 0; index < Owner.MeterLogicController.Count; index++)
                {
                    ApplicationController controller = (ApplicationController)Owner.MeterLogicController[index];
                    if (controller != null && !controller.IsAllocated)
                    {
                        ///Synchronization Block
                        if (Monitor.TryEnter(controller, Commons.ReadLOCKLow_TimeOut))
                        {
                            try
                            {
                                if (!controller.IsAllocated &&
                                controller.ConnectToMeter == null)
                                {
                                    Contr = controller;
                                    Contr.IsAllocated = true;
                                    Contr.ReInitApplicationController();
                                    // Initialize Work here
                                    Owner.PartialInitApplicationController(Contr);
                                    break;
                                    // LoopState.Break();
                                    // LoopState.Stop();
                                }
                            }
                            catch
                            {
                            }
                            finally
                            {
                                Monitor.Exit(controller);
                            }
                        }
                    }
                }//)

                // Check if already ConnectionThread deallocated
                // foreach (ApplicationController controller in ConnectionThreadsAllocated)
                // {
                //     if (!controller.IsAllocated)
                //     {
                //         Contr = controller;
                //         break;
                //     }
                // }
                // if (Contr != null)
                // {
                //     DeAllocateMeterConnection(Contr.ConnectionController.CurrentConnection);
                //     return Contr;
                // }

                // Check if already ConnectionThread deallocated in Owner.MeterLogicController
                if (Contr != null)
                {
                    // if (Contr.ConnectionController.CurrentConnection != null)
                    //     DeAllocateMeterConnection(Contr.ConnectionController.CurrentConnection);
                    return Contr;
                }
                // Check if new ConnectionThread could be allocated from Owner.MeterLogicController
                if (Monitor.TryEnter(Owner.MeterLogicController, Commons.ReadLOCKLow_TimeOut))
                {
                    try
                    {
                        // Initialize New Application Controller Here
                        if (Owner.MeterLogicController.Count < Owner.MaxMeterConnection)
                        {
                            Contr = Owner.InitApplicationController();
                            Owner.MeterLogicController.Add(Contr);
                        }
                    }
                    finally
                    {
                        Monitor.Exit(Owner.MeterLogicController);
                    }
                }
                return Contr;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to allocate new Meter Connection Thread", ex);
            }
        }

        private ApplicationController GetApplicationControllerForDeallocation()
        {
            try
            {
                ApplicationController Contr = null;
                ApplicationController controller = null;
                for (int index = Owner.MeterLogicController.Count - 1; index > 0; index--)
                {
                    controller = (ApplicationController)Owner.MeterLogicController[index];
                    if (controller != null && !controller.IsAllocated &&
                        (controller.ConnectToMeter == null || !controller.ConnectToMeter.IsAssigned))
                    {
                        Contr = controller;
                        break;
                    }
                }
                if (Contr != null)
                {
                    // if (Contr.ConnectionController.CurrentConnection != null)
                    //     DeAllocateMeterConnection(Contr.ConnectionController.CurrentConnection);
                    if (Monitor.TryEnter(Contr, Commons.WriteLOCKLow_TimeOut))
                    {
                        try
                        {
                            Owner.MeterLogicController.Remove(Contr);
                        }
                        finally
                        {
                            Monitor.Exit(Contr);
                        }
                    }
                    else
                        throw new SynchronizationLockException("Unable to take lock on GetApplicationControllerForDeallocation_ConnectionThreadAllocater");
                    return Contr;
                }
                return Contr;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to dispose meter connection thread ", ex);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool TryAllocateMeterConnection(IOConnection Conn, out ApplicationController Contr)
        {
            try
            {
                Contr = AllocateMeterConnection(Conn);
                return Contr != null ||
                    (Conn.ConnectionInfo != null && Conn.MeterSerialNumberObj != null &&
                    IsMeterConnectionAllocated(Conn.MeterSerialNumberObj));
            }
            catch (Exception)
            {
                Contr = null;
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public ApplicationController AllocateMeterConnection(IOConnection Conn)
        {
            ApplicationController Contr = null;
            try
            {
                Contr = GetApplicationControllerForAllocation();
                // Obtain WriteLock On App_Contr
                if (Contr != null)
                {
                    try
                    {
                        AllocateMeterConnection(Conn, Contr);
                    }
                    finally
                    {

                    }
                }
                else if (Contr == null)
                    throw new Exception(String.Format("Null AppController,Unable to assign meter connection {0}", Conn));
                return Contr;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to allocate meter connection {0}", Conn), ex);
            }
            finally
            {
                try
                {
                    if (Contr != null && Conn.MeterSerialNumberObj != null &&
                        !IsMeterConnectionAllocated(Conn.MeterSerialNumberObj))
                    {
                        TryCancelRunningMeterConnection(Contr);
                    }
                }
                catch { }
            }
        }

        [HostProtectionAttribute(Synchronization = true, ExternalThreading = true)]
        private void AllocateMeterConnection(IOConnection Conn, ApplicationController Contr)
        {
            try
            {
                if (Monitor.TryEnter(Conn, AllocationTimeOut))
                {
                    try
                    {
                        if (Monitor.TryEnter(Contr, AllocationTimeOut))
                        {
                            try
                            {
                                if (!Conn.IsChannelConnected || Conn.IsAssigned ||
                                    (Conn.ConnectionInfo != null && IsMeterConnectionAllocated(Conn.MeterSerialNumberObj)))
                                    throw new Exception(String.Format("Error,Unable to re-assign meter connection {0}", Conn));
                                // Meter Connection Could be allocated
                                if (Contr == null)
                                    throw new Exception(String.Format("Unable to assign meter connection {0}", Conn));
                                if (!Conn.IsAssigned)
                                {
                                    Conn.IsAssigned = true;
                                    Conn.ResetStream();
                                }
                                // Initial IOConnection Internal Buffers
                                Conn.InitBuffer(Owner.ConnectionManager.MaxReadBuffer,
                                    Owner.ConnectionManager.MaxWriteBuffer, Owner.CreateDataReaderBuffer);

                                Contr.IsAllocated = true;
                                Contr.ConnectToMeter = Conn;
                                if (Contr.ConnectionController != null)
                                    Contr.ConnectionController.CurrentConnection = Conn;

                                CancellationTokenSource TK = new CancellationTokenSource();
                                Contr.IsAllocated = ConnectionThreadsAllocated.TryAdd(Conn.MeterSerialNumberObj, Contr);
                                if (!Contr.IsAllocated)
                                {
                                    Conn.IsAssigned = false;
                                    Contr.IsAllocated = false;
                                    Contr.ConnectToMeter = null;
                                    Contr.ConnectionController.CurrentConnection = null;
                                    throw new Exception(String.Format("Unable to assign meter connection {0}", Conn));
                                }
                                Contr.ThreadCancelToken = TK;
                                // Initial IOConnection
                                Conn.IOTrafficLogger = Contr.Applicationprocess_Controller.ApplicationProcess.ApplicationLayer.DLMSLogger;

                                // Start New Meter Handler Thread
                                // System.Threading.Tasks.Task _RunningThread = System.Threading.Tasks.
                                // Task.Factory.StartNew<Task>(new Func<object, Task>(Owner.TaskRunner.ConnectionRunnable), Contr, TK.Token);
                                // Start New Meter Handler Thread
                                System.Threading.Tasks.Task _RunningThread = System.Threading.Tasks.Task.Factory.StartNew(Owner.TaskRunner.ConnectionRunnable, Contr, TK.Token, TaskCreationOptions.LongRunning,TaskScheduler.Default);
                                Interlocked.Increment(ref CummulativeCouter);
                                Contr.ExecRunnerThread = _RunningThread;
                                Contr.CummId = CummulativeCouter;
                                // Increment Allocated Meter Count
                                if (Conn.CurrentConnection == PhysicalConnectionType.KeepAlive)
                                {
                                    Interlocked.Increment(ref KA_Alloc_Count);
                                }
                                Interlocked.Increment(ref Alloc_Count);
                            }
                            catch (Exception ex)
                            {
                                Conn.IsAssigned = false;
                                Contr.IsAllocated = false;
                                throw ex;
                            }
                            finally
                            {
                                if (Conn != null && !IsMeterConnectionAllocated(Conn.MeterSerialNumberObj))
                                    Conn.IsAssigned = false;
                                Monitor.Exit(Contr);
                            }
                        } //Internal Contr Lock Block End
                        else throw new SynchronizationLockException(String.Format("Unable to take Write Lock App_Contr {0}_AllocateMeterConnection", Conn));
                    }
                    finally
                    {
                        Monitor.Exit(Conn);
                    }
                } // External Conn Lock Block End
                else throw new SynchronizationLockException(String.Format("Unable to take Write Lock Conn {0}_AllocateMeterConnection", Conn));
            }
            catch (NullReferenceException ex)
            {
                string error_Message = String.Format("Null Reference,Unable to allocate meter connection {0} {1} {2}", Conn, Conn.IOStream, DateTime.Now);

                Exception _ex = new Exception(error_Message, ex);
#if Enable_DEBUG_ECHO
                Commons.WriteError(Contr.ActivityLogger, "AllocateMeterConnection error occurred while allocating," + ex.Message);
#endif
                string path = LocalCommon.GetApplicationConfigsDirectory() + @"Exceptions\AllocationError_Errors.txt";
                LocalCommon.SaveException(_ex, path);

                if (Owner != null &&
                    Owner.Activity_DataLogger != null)
                {
                    Owner.Activity_DataLogger.LogMessage(error_Message, ex, 10);
                }

                throw _ex;
            }
            catch (Exception ex)
            {
                string error_Message = String.Format("Unable to allocate meter connection {0} {1} {2}", Conn, Conn.IOStream, DateTime.Now);

                Exception _ex = new Exception(error_Message, ex);
#if Enable_DEBUG_ECHO
                Commons.WriteError(Contr.ActivityLogger, "AllocateMeterConnection error occurred while allocating," + ex.Message);
#endif
                string path = LocalCommon.GetApplicationConfigsDirectory() + @"Exceptions\AllocationError_Errors.txt";
                LocalCommon.SaveException(_ex, path);

                if (Owner != null &&
                    Owner.Activity_DataLogger != null)
                {
                    Owner.Activity_DataLogger.LogMessage(error_Message, ex, 10);
                }

                throw _ex;
            }
            finally
            {
                try
                {
                    #region Debugging & Logging
#if Enable_DEBUG_ECHO

                    Commons.WriteAlert(Contr.ActivityLogger, String.Format("finally-AllocateMeterConnection {0}", Conn));

#endif
#if Enable_Transactional_Logging

                    string path = Commons.GetApplicationConfigsDirectory() + @"AllocationLogs\ThreadAlloc.txt";
                    StringBuilder strBuilder = new StringBuilder("finally Allocation");
                    strBuilder.AppendFormat("IOConn:{0} AllocID:{1} TimeStamp:{2} \r\n", Conn.IOStream, Contr.Applicationprocess_Controller.OwnerThreadId, DateTime.Now.TimeOfDay);
                    Commons.SaveApplicationLogMessage(strBuilder, path);

#endif
                    #endregion
                }
                catch
                { }
            }
        }

        #region DeAllocater_Member

        public bool TryDeAllocateMeterConnection(IOConnection Conn, ApplicationController Contr)
        {
            try
            {
                DeAllocateMeterConnection(Conn, ref Contr);
                return Contr != null;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HostProtectionAttribute(Synchronization = true, ExternalThreading = true)]
        public void DeAllocateMeterConnection(IOConnection Conn, ref ApplicationController Contr)
        {
            System.Threading.Tasks.Task RunningThread = null;
            ApplicationController AppContr_T = null;
            try
            {
                // Connection Synchronization Lock
                if (Monitor.TryEnter(Conn, DeallocationTimeOut))
                {
                    try
                    {
                        // ApplicationController Synchronization Lock
                        if (Monitor.TryEnter(Contr, DeallocationTimeOut))
                        {
                            try
                            {
                                // Remove Entry By MSN
                                #region Remove From ConnectionThreadsAllocated

                                if (Conn.ConnectionInfo != null && Conn.MeterSerialNumberObj != null &&
                                    ConnectionThreadsAllocated.ContainsKey(Conn.MeterSerialNumberObj))
                                {
                                    ConnectionThreadsAllocated.TryGetValue(Conn.MeterSerialNumberObj, out AppContr_T);
                                    if (AppContr_T == Contr)
                                        ConnectionThreadsAllocated.TryRemove(Conn.MeterSerialNumberObj, out AppContr_T);
                                    else
                                        AppContr_T = null;
                                    //Try To Remove From PremtiveThreadDeallocation Structure
                                    ApplicationController Contr_Prem = null;
                                    DeAllocate_Key T_KEY = new DeAllocate_Key(Conn);
                                    PremtiveThreadDeallocated.TryGetValue(T_KEY, out Contr_Prem);
                                    if (Contr_Prem == Contr)
                                        PremtiveThreadDeallocated.TryRemove(T_KEY, out Contr_Prem);
                                }
                                //Remove Entry By ApplicationController_Value
                                if (Contr != null && AppContr_T == null)
                                {
                                    bool IsDeAllocated = false;
                                    List<MeterSerialNumber> SrNumKeyList = ConnectionThreadsAllocated.Keys.ToList<MeterSerialNumber>();
                                    foreach (var SrNumKey in SrNumKeyList)
                                    {
                                        ApplicationController contr_T = null;
                                        ConnectionThreadsAllocated.TryGetValue(SrNumKey, out contr_T);

                                        if (contr_T == Contr &&
                                            Contr.ConnectToMeter == Conn &&
                                            Contr.IsAllocated && Conn.IsAssigned)
                                        {
                                            ApplicationController Contr_T = null;
                                            ConnectionThreadsAllocated.TryRemove(SrNumKey, out Contr_T);
                                            ///DeAllocate From PremtiveThreadDeallocated
                                            DeAllocate_Key T_KEY = new DeAllocate_Key(Conn);
                                            PremtiveThreadDeallocated.TryGetValue(T_KEY, out Contr_T);
                                            if (Contr_T != null && Contr_T == Contr)
                                                PremtiveThreadDeallocated.TryRemove(T_KEY, out Contr_T);
                                            IsDeAllocated = true;
                                        }
                                    }
                                    if (!IsDeAllocated)
                                        throw new Exception(String.Format("Error occurred while DeAllocating Meter Connection_NotFound {0}",
                                            Conn));
                                }

                                #endregion
                                try
                                {
                                    #region Reset ApplicationController

                                    try
                                    {
                                        RunningThread = Contr.ExecRunnerThread;
                                        Contr.DeInitApplicationController();
                                    }
                                    catch
                                    {
                                    }

                                    #endregion
                                    #region Reset IOConnection

                                    try
                                    {
                                        if (Conn != null && Conn.IsConnected)
                                        {
                                            if (Conn.ConnectionInfo.ConnectionType == PhysicalConnectionType.NonKeepAlive)
                                            {
                                                Conn.Disconnect();
                                                Conn.Dispose();
                                                Owner.ConnectedMeterList.Remove(Conn);
                                            }
                                            else
                                            {
                                                //De-Initialize IO_Buffers For Keep Alive Meters
                                                Conn.DeInitBufferKeepAlive();
                                            }
                                        }
                                        else
                                        {
                                            Conn.Dispose();
                                            Owner.ConnectedMeterList.Remove(Conn);
                                        }
                                    }
                                    catch
                                    {
                                    }

                                    #endregion
                                    #region //Dispose Off Running Thread Task
                                    try
                                    {
                                        //Dispose Off Running Thread Task
                                        if (RunningThread == null ||
                                                RunningThread.Status == TaskStatus.WaitingForActivation ||
                                                RunningThread.Status == TaskStatus.Created ||
                                                RunningThread.Status == TaskStatus.Faulted)
                                            if (Contr.ThreadCancelToken != null)
                                            {
                                                Contr.ThreadCancelToken.Cancel(true);
                                            }
                                        if (RunningThread != null)
                                        {
                                            Exception ex = RunningThread.Exception;
                                            if (RunningThread.Status == System.Threading.Tasks.TaskStatus.Faulted ||
                                                RunningThread.Status == System.Threading.Tasks.TaskStatus.Canceled ||
                                                RunningThread.Status == System.Threading.Tasks.TaskStatus.RanToCompletion)
                                            {
                                                RunningThread.Dispose();
                                            }
                                            //if(ex != null)
                                            //    Common.WriteError("Error In Connection Handler Thread " + ex);
                                        }
                                    }
                                    catch
                                    { }
                                    #endregion
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                                finally
                                {
                                    try
                                    {
                                        if (Contr != null)
                                        {
                                            Interlocked.Decrement(ref Alloc_Count);
                                            // Program.saveConnectionLog(Conn);
                                            // Release Resource In ApplicationController
                                            Contr.IsAllocated = false;
                                            if (Conn.CurrentConnection == PhysicalConnectionType.KeepAlive)
                                            {
                                                Conn.IsAssigned = false;
                                                Interlocked.Decrement(ref KA_Alloc_Count);
                                            }
                                            // Owner.MeterLogicController.Insert(0, Contr);
                                        }
                                    }
                                    catch (Exception)
                                    { }
                                }
                            }
                            finally
                            {
                                Monitor.Exit(Contr);
                            }
                        }//Internal Contr Lock Block End
                        else throw new SynchronizationLockException(String.Format("Unable to take Write Lock App_Contr {0}_DeAllocateMeterConnection",
                            Conn));
                    }
                    finally
                    {
                        Monitor.Exit(Conn);
                    }

                }//External Conn Lock Block End
                else throw new SynchronizationLockException(String.Format("Unable to take Write Lock Conn {0}_DeAllocateMeterConnection", Conn));
            }
            catch (NullReferenceException ex)
            {
                string error_Message = String.Format("Error occurred while deallocate meter connection {0} {1} {2}", Conn, DateTime.Now);

                Exception _ex = new Exception(error_Message, ex);
#if Enable_DEBUG_ECHO
                Commons.WriteError(Contr.ActivityLogger, "DeAllocateMeterConnection error occurred while De-allocating," + ex.Message);
#endif
                string path = LocalCommon.GetApplicationConfigsDirectory() + @"Exceptions\DeallocationError_Errors.txt";
                LocalCommon.SaveException(_ex, path);

                if (Owner != null &&
                    Owner.Activity_DataLogger != null)
                {
                    Owner.Activity_DataLogger.LogMessage(error_Message, ex, 10);
                }

                throw _ex;
            }
            catch (Exception ex)
            {
                string error_Message = String.Format("Error occurred while deallocate meter connection {0} {1} {2}", Conn, DateTime.Now);
                Exception _ex = new Exception(error_Message, ex);

#if Enable_DEBUG_ECHO

                Commons.WriteError(Contr.ActivityLogger, "DeAllocateMeterConnection error occurred while De-allocating," + ex.Message);

#endif
                string path = LocalCommon.GetApplicationConfigsDirectory() + @"Exceptions\DeallocationError_Errors.txt";
                LocalCommon.SaveException(_ex, path);

                if (Owner != null &&
                    Owner.Activity_DataLogger != null)
                {
                    Owner.Activity_DataLogger.LogMessage(error_Message, ex, 10);
                }

                throw _ex;
            }
            finally
            {
#if Enable_DEBUG_ECHO
                Commons.WriteAlert(Contr.ActivityLogger, String.Format("finally-De-allocateMeterConnection {0}", Conn));
#endif
                try
                {
                    #region Debugging & Logging
#if Enable_Transactional_Logging

                    string path = Commons.GetApplicationConfigsDirectory() + @"AllocationLogs\ThreadDeAlloc.txt";
                    StringBuilder strBuilder = new StringBuilder("finally De-alloc");
                    strBuilder.AppendFormat("IOConn:{1} AllocID:{2} TimeStamp:{0} \r\n",
                        Conn.IOStream,
                        (Contr != null) ? Contr.Applicationprocess_Controller.OwnerThreadId : -1
                        , DateTime.Now.TimeOfDay);
                    Commons.SaveApplicationLogMessage(strBuilder, path);

#endif
                    #endregion
                }
                catch
                { }

            }
        }

        #endregion

        public ArrayList GetConnectionsDeallocate()
        {
            ArrayList ListDis = ArrayList.Synchronized(new ArrayList());
            try
            {
                List<KeyValuePair<MeterSerialNumber, ApplicationController>> ContrsToRemove =
                    new List<KeyValuePair<MeterSerialNumber, ApplicationController>>();
                List<MeterSerialNumber> SrNumKeyList = ConnectionThreadsAllocated.Keys.ToList<MeterSerialNumber>();

                foreach (var contr_Key in SrNumKeyList)
                {
                    try
                    {
                        ApplicationController contr = null;
                        ConnectionThreadsAllocated.TryGetValue(contr_Key, out contr);
                        try
                        {
                            // Meter Connection De-Allocation Criteria
                            if (contr.ConnectToMeter != null && contr.IsAllocated && contr.ConnectToMeter.IsAssigned &&
                                (contr.ConnectToMeter.LastIOActivityDuration >= Owner.ConnectionManager.TCPInactivityDuration &&
                                 (DateTime.Now.Subtract(contr.SessionDateTime) >= Owner.MaxSessionResetDuration ||
                                  !contr.ConnectToMeter.IsChannelConnected))
                                    && !ListDis.Contains(contr.ConnectToMeter))
                                if (contr != null)
                                    try
                                    {
                                        ListDis.Add(contr.ConnectToMeter);
                                    }
                                    finally
                                    {
                                        // Monitor.Exit(contr);
                                    }
                        }
                        finally
                        {

                        }
                    }
                    catch
                    { }
                }
            }
            catch
            {
                ListDis.Clear();
            }
            return ListDis;
        }

        public bool TryUpdate_MSN_MeterConnection(ApplicationController Contr, MeterSerialNumber MSN, MeterSerialNumber MSN_OldVal = null)
        {
            bool isUpdated = false;
            ApplicationController AppContrT = null;
            try
            {
                if (MSN != null && MSN != MSN_OldVal &&
                    (ConnectionThreadsAllocated.TryGetValue(MSN, out AppContrT) ||
                ConnectionThreadsAllocated.TryGetValue(MSN_OldVal, out AppContrT)))
                {
                    if (AppContrT != null && AppContrT != Contr)
                        ConnectionThreadsAllocated.TryUpdate(MSN, Contr, AppContrT);
                    else if (MSN_OldVal != MSN && AppContrT != null)
                    {
                        ConnectionThreadsAllocated.TryRemove(MSN_OldVal, out AppContrT);
                        ConnectionThreadsAllocated.TryAdd(MSN, Contr);
                    }
                    else
                        AppContrT = null;
                }
                if (MSN != null && Contr != null && AppContrT == null)
                {
                    foreach (var key_ValPair in ConnectionThreadsAllocated)
                    {
                        if (key_ValPair.Value == Contr)
                        {
                            isUpdated = ConnectionThreadsAllocated.TryRemove(key_ValPair.Key, out Contr);
                            if (isUpdated)
                                isUpdated = ConnectionThreadsAllocated.TryAdd(MSN, Contr);
                            break;
                        }
                    }
                }
                return isUpdated;
            }
            catch (Exception)
            {
                Contr = null;
                return false;
            }
        }

        #region TryCancelAllARunningMeterConnections

        public void TryCancelAllRunningMeterConnections()
        {
            try
            {
                ///Try To Call Cancel Already Running Threads
                if (ConnectionThreadsAllocated != null && ConnectionThreadsAllocated.Count > 0)
                    foreach (var connAllocated in ConnectionThreadsAllocated.Values)
                    {
                        try
                        {
                            if (connAllocated != null && connAllocated.ThreadCancelToken != null)
                                connAllocated.ThreadCancelToken.Cancel();
                        }
                        finally
                        { }
                    }
            }
            catch (Exception) { }
        }

        public bool TryCancelRunningMeterConnections(ArrayList ConController)
        {
            try
            {
                foreach (ApplicationController AppController in ConController)
                {
                    bool IsCancel = false;
                    if (AppController != null)
                    {
                        IsCancel = TryCancelRunningMeterConnection(AppController);
                    }
                    if (!IsCancel)
                        return IsCancel;
                }
                return true;
            }
            catch (Exception)
            { }
            return false;
        }

        public bool TryCancelRunningMeterConnection(ApplicationController ConController)
        {
            try
            {

                if (ConController.ConnectToMeter != null &&
                            ConController.IsAllocated &&
                            ConController.ConnectToMeter.IsAssigned && ConController.ThreadCancelToken != null)
                {
                    DeAllocate_Key Key = new DeAllocate_Key(ConController.ConnectToMeter);
                    ApplicationController Contr = null;
                    PremtiveThreadDeallocated.TryGetValue(Key, out Contr);
                    //Check Application Controller For IOConnection Already Cancel
                    if (ConController == Contr &&
                        Contr != null &&
                        Contr.ConnectToMeter == ConController.ConnectToMeter)
                        return true;
                    Contr = PremtiveThreadDeallocated.AddOrUpdate(Key, ConController, (x, y) =>
                    {
                        if (ConController != y && y != null
                            && y.ConnectToMeter != ConController.ConnectToMeter)
                            return y;
                        else
                            return ConController;
                    });
                    Contr.ThreadCancelToken.Cancel(true);
                    return true;
                }
                else
                    return false;

            }
            catch (Exception) { }
            return false;
        }

        #endregion

        //public long _GetCountMeterConnectionAllocated()
        //{
        //    long _t = -1;
        //    try
        //    {
        //        _t = ConnectionThreadsAllocated.Count<KeyValuePair<MeterSerialNumber, ApplicationController>>((x) =>
        //        {
        //            return (x.Key != null && x.Value != null && x.Value.IsAllocated && x.Value.ConnectionController.CurrentConnection.IsAssigned);
        //        });
        //    }
        //    catch (Exception ex) { }
        //    return _t;
        //}

        //public long _GetCountMeterConnections()
        //{
        //    long _t = -1;
        //    try
        //    {
        //        _t = ConnectionThreadsAllocated.Count<KeyValuePair<MeterSerialNumber, ApplicationController>>((x) =>
        //        {
        //            return (x.Key != null && x.Value != null && x.Value.IsAllocated && x.Value.ConnectionController.CurrentConnection.IsAssigned && x.Value.Applicationprocess_Controller.IsConnected);
        //        });
        //    }
        //    catch (Exception ex) { }
        //    return _t;
        //}

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                #region Dispose Off ConnectionRunnerThreads

                try
                {
                    if (connAllocaterThreads != null)
                    {
                        connAllocaterThreads.Abort();
                    }
                }
                catch (Exception)
                { }

                #endregion
                #region Initial Connection Deallocator Thread

                try
                {
                    if (connDeAllocaterThread != null)
                    {
                        connDeAllocaterThread.Abort();
                    }
                }
                catch (Exception)
                { }

                #endregion
                #region ConnectionThreadsAllocated

                if (ConnectionThreadsAllocated != null)
                {
                    ConnectionThreadsAllocated.Clear();
                    ConnectionThreadsAllocated = null;
                }

                #endregion
                #region Dispose Off KA_SchedularThread

                try
                {
                    if (KA_Sch_Thread != null)
                        KA_Sch_Thread.Abort();
                    KA_Sch_Thread = null;
                }
                catch (Exception)
                { }

                #endregion
                #region Dispose Related Data Members

                if (ConnectionThreadsAllocated != null)
                {
                    ConnectionThreadsAllocated.Clear();
                    ConnectionThreadsAllocated = null;
                }
                if (connToAllocate != null)
                {
                    connToAllocate.Clear();
                    connToAllocate = null;
                }
                _MDC_Status_Obj = null;

                #endregion
            }
            catch
            { }
        }

        #endregion

        ~ConnectionThreadAllocater()
        {
            try
            {
                Dispose();
            }
            catch (Exception) { }
        }
    }
}
