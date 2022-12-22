///#define Enable_DEBUG_ECHO
#define Enable_DEBUG_ECHO

using SharedCode.Common;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SharedCode.Comm.HelperClasses
{
    public class AsyncTimeOuts : IDisposable
    {
        #region Data_Members

        public static readonly int InvokeQueue = 0;
        public static readonly int WaitList = 1;
        public static readonly int Q1Wait = 2;
        public static readonly int Q2Wait = 3;

        private static readonly int WaitListCapacity = 2000;
        private static readonly string CurrentInstanceKey = "CurrentAppTimeoutInstance";
        public static readonly object StaticReaderLockingObject = null;

        public static readonly int MinQueueLevel = 02;
        public static readonly int MaxQueueLevel = 10;

        public static readonly TimeSpan MinTimeDuration = TimeSpan.FromSeconds(10);
        public static readonly TimeSpan MaxTimeDuration = TimeSpan.FromSeconds(30);
        public static readonly TimeSpan DefaultTimeDuration = TimeSpan.FromSeconds(10);
        public static readonly int DefaultMultiQueueLevel = 06;

        internal static AsyncTimeOuts currentInstance = null;
        private int _QueueLevels = 06;

        private static readonly TimeSpan _PollingTimeLow = TimeSpan.FromMilliseconds(500);
        private TimeSpan _PollingTime = DefaultTimeDuration;

        ///Delta 5 Seconds To Invoke
        private ConcurrentQueue<BaseWaitToken> _LastInvokeQueue;
        private ConcurrentQueue<BaseWaitToken> _EntryLevelQueue;
        private ArrayList TimeWaitList = null;

        private System.Timers.Timer _LastInvokeQueueTimer = null;
        private System.Timers.Timer _EntryLevelQueueTimer = null;
        private System.Timers.Timer WaitQueuePollTimer = null;
        ///List of Waits Queues (Q1,Q2,Q3,Q4....Qn)(Wt10,Wt20,Wt30,Wt40....Wtn*10)
        private List<ConcurrentQueue<BaseWaitToken>> WaitQueue = null;

        #endregion

        #region Properties

        public static AsyncTimeOuts CurrentInstance
        {
            get
            {
                try
                {
                    if (currentInstance == null || !(currentInstance is AsyncTimeOuts))
                    {
                        ///Try obtain Current Application Instance 
                        lock (StaticReaderLockingObject)
                        {
                            if (currentInstance == null || !(currentInstance is AsyncTimeOuts))
                            {
                                currentInstance = new AsyncTimeOuts();
                            }

                        }
                    }
                    return (AsyncTimeOuts)currentInstance;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while intialize current applicatoin AsyncTimeOuts", ex);
                }
            }
        }

        public AsyncTimeOuts CurrentInstanceLocal
        {
            get
            {
                try
                {
                    if (currentInstance == null)
                        currentInstance = AsyncTimeOuts.CurrentInstance;
                    return currentInstance;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        internal ConcurrentQueue<BaseWaitToken> LastInvokeQueue
        {
            get { return _LastInvokeQueue; }
        }

        internal ConcurrentQueue<BaseWaitToken> EntryLevelQueue
        {
            get { return _EntryLevelQueue; }
        }

        public TimeSpan PollingTime
        {
            get { return _PollingTime; }
            set
            {
                if (value >= MinTimeDuration && value <= MaxTimeDuration)
                    _PollingTime = value;
                else
                    throw new Exception("Unable to initialze Polling Time Period");
            }
        }

        public static TimeSpan PollingTimeLow
        {
            get { return AsyncTimeOuts._PollingTimeLow; }
        }


        public int QueueLevels
        {
            get { return _QueueLevels; }
            set
            {
                if (value >= MinQueueLevel && value <= MaxQueueLevel)
                    _QueueLevels = value;
                else
                    throw new Exception("Unable to initialze QueueLevels");
                _QueueLevels = value;
            }
        }

        #endregion

        #region Constructer

        static AsyncTimeOuts()
        {
            ///Init Static Reader Locking Object
            StaticReaderLockingObject = new object();
        }

        public AsyncTimeOuts(int QueueLevels, TimeSpan TimeDuration)
        {
            try
            {
                PollingTime = TimeDuration;
                this.QueueLevels = QueueLevels;
                Init_Structs();
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while initializ AsyncTimeouts", ex);
            }
        }

        /// <summary>
        /// AsyncTimeOuts Default Constructer
        /// </summary>
        public AsyncTimeOuts() : this(DefaultMultiQueueLevel, DefaultTimeDuration) { }

        #endregion

        #region Member_Functions

        /// <summary>
        /// RegisterWaitHandler Registers BaseWaitToken Handler to occure
        /// TimeOut After Specified Period Time
        /// </summary>
        /// <param name="WaitToken">WaitToken to represent waits</param>
        public static void RegisterWaitHandler(BaseWaitToken WaitToken)
        {
            try
            {
                if (WaitToken == null)
                    throw new ArgumentNullException("WaitToken RegisterWaitHandler_ApplicationTimeOuts");
                CurrentInstance.RegisterWaitHandle(WaitToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// UnRegisterWaitHandler dispose BaseWaitToken Handler release AppResources
        /// </summary>
        /// <param name="WaitToken"></param>
        public static void UnRegisterWaitHandler(BaseWaitToken WaitToken)
        {
            try
            {
                if (WaitToken != null)
                    WaitToken.Dispose();
            }
            catch
            { }
        }

        public void RegisterWaitHandle(BaseWaitToken WaitToken)
        {
            int count = 0;
            try
            {
                if (WaitToken == null || WaitToken.IsCancel)
                    throw new ArgumentNullException("WaitToken RegisterWaitHandler_ApplicationTimeOuts");
                int currentWaitLevel = NextNLevelQueue(WaitToken);
                if (currentWaitLevel == InvokeQueue || currentWaitLevel == WaitList)
                {
                    TimeWaitList.Add(WaitToken);
                    count = TimeWaitList.Count;
                }
                else
                {
                    EntryLevelQueue.Enqueue(WaitToken);
                    count = EntryLevelQueue.Count;
                }
                //Commons.WriteLine(String.Format("Wait Registered Successfuly {0} {1}", currentWaitLevel, count));
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occurred while RegisterWaitHandle_AsyncTimeOuts"), ex);
            }
        }

        public void UnRegisterWaitHandle(BaseWaitToken WaitToken)
        {
            try
            {
                if (WaitToken == null)
                    throw new ArgumentNullException("WaitToken RegisterWaitHandler_ApplicationTimeOuts");
                WaitToken.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occurred while UnRegisterWaitHandle_AsyncTimeOuts"), ex);
            }
        }

        #endregion

        #region Support_Level_Functions

        /// <summary>
        /// Returns Next N Queue Level Jump(Also Perform N_Queue Level Jump On Wait Starving)
        /// </summary>
        /// <param name="WtTokenHandler"></param>
        /// <param name="TriggeredTime"></param>
        /// <param name="WaitQueueTime"></param>
        /// <returns>N_Queue Number</returns>
        public static int NextNLevelQueue(BaseWaitToken WtTokenHandler)
        {
            int currentLevel = 0;
            TimeSpan DurationToTrigger = TimeSpan.MinValue;
            try
            {
                DurationToTrigger = WtTokenHandler.TriggerDuration;
                if (DurationToTrigger <= MinTimeDuration)
                    currentLevel = 0;
                else if (DurationToTrigger > MinTimeDuration && DurationToTrigger <= (MinTimeDuration + MinTimeDuration))
                {
                    currentLevel = 1;
                }
                else
                {
                    currentLevel = Convert.ToInt32(DurationToTrigger.TotalSeconds / MinTimeDuration.TotalSeconds) + 1;
                }
            }
            catch { }
            return currentLevel;
        }

        internal void Process_InvokeQueue()
        {
            try
            {
                ///Select All WaitToken That Needs to trigger Or Expired
                var Iterator = from BaseWaitToken x in TimeWaitList
                               where x != null && (x.IsCancel || x.TriggerDuration < MinTimeDuration)
                               orderby x.TriggerTime
                               select x;
                ArrayList tmpRemoved = new ArrayList();
                #region Process_TimeWaitList Items
                foreach (var BaseWaitToken in Iterator)
                //Parallel.ForEach<BaseWaitToken>(PartionDyn, (BaseWaitToken, LoopStat) =>
                {
                    try
                    {
                        if (BaseWaitToken == null)
                            return;
                        tmpRemoved.Add(BaseWaitToken);
                        if (BaseWaitToken.IsCancel)
                            BaseWaitToken.Dispose();
                        ///TriggerDuration Less Than MinTimeDuration    
                        else if (BaseWaitToken.TriggerDuration <= MinTimeDuration)
                        {
                            LastInvokeQueue.Enqueue(BaseWaitToken);
#if Enable_DEBUG_ECHO
                            Console.Out.WriteLine(String.Format("TimeToTrigger:{0} Enque_Process_InvokeQueue", BaseWaitToken.TriggerTime));
#endif
                        }
                        else
                        {
#if Enable_DEBUG_ECHO
                            Console.Out.WriteLine(String.Format("TimeToTrigger:{0} ELSE_No_Case_Process_InvokeQueue", BaseWaitToken.TriggerTime));
#endif
                        }
                    }
                    catch { }
                }//);
                foreach (var wtToken in tmpRemoved)
                {
                    TimeWaitList.Remove(wtToken);
                }
                tmpRemoved.Clear();
                #endregion
                #region Process_InvokeQueue Items
                Parallel.For(0, LastInvokeQueue.Count, (int Index_BaseWtToken, ParallelLoopState LoopStat) =>
                {
                    try
                    {
                        BaseWaitToken BaseWtToken = null;
                        ///Dequeue Items Successfuly
                        if (!LastInvokeQueue.TryDequeue(out BaseWtToken))
                        {
                            ///LoopStat.Break();
                            return;
                        }
                        ///BaseWtToken != null
                        if (BaseWtToken != null)
                        {
                            if (BaseWtToken.TriggerDuration <= MinTimeDuration)
                                Invoke_WaitHandlerResponse(BaseWtToken);
                            else if (BaseWtToken.IsCancel)
                                BaseWtToken.Dispose();
                        }
                    }
                    catch { }
                });
                #endregion
            }
            catch (Exception ex) { throw new Exception("Error occured while Process_InvokeQueue_ApplicationTimeOuts", ex) { }; }
        }

        internal void Process_WaitQueue()
        {
            try
            {
                ///Process Wait Queue's in reverse order of Waits (Q1,Q2,Q3,Q4....Qn)(Wt10,Wt20,Wt30,Wt40....Wtn*10)
                for (int index = WaitQueue.Count - 1; index >= 0; index--)
                {
                    try
                    {
                        ConcurrentQueue<BaseWaitToken> CurrentWaitQueue = WaitQueue[index];
                        ConcurrentQueue<BaseWaitToken> NextWaitQueue = ((index + 1) >= WaitQueue.Count) ? null : WaitQueue[index + 1];
                        ProcessWaitQueue(NextWaitQueue, CurrentWaitQueue);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            catch (Exception ex) { throw new Exception("Error occurred while Process_WaitQueue_ApplicationTimeOuts", ex) { }; }
        }

        internal void Process_EntryLevelWaitQueue()
        {
            try
            {
                #region Process_EntryLevelWaitQueue Items
                ///Shift BaseWaitToken Items From EntryLevelQueue to Q1
                if (EntryLevelQueue!=null && EntryLevelQueue.Count > 0)
                    ProcessWaitQueue(WaitQueue[0], EntryLevelQueue);
                #endregion
            }
            catch (Exception ex) { throw new Exception("Error occurred while Process_EntryLevelWaitQueue_ApplicationTimeOuts", ex) { }; }
        }

        /// <summary>
        /// ProcessWaitQueue
        /// </summary>
        /// <param name="NextWaitQueue"></param>
        /// <param name="CurrentWaitQueue"></param>
        internal void ProcessWaitQueue(ConcurrentQueue<BaseWaitToken> NextWaitQueue, ConcurrentQueue<BaseWaitToken> CurrentWaitQueue)
        {
            try
            {
                if (CurrentWaitQueue == null)
                    throw new ArgumentException("Current WaitQueue is nullable");
                ///Synchronization Lock
                if (Monitor.TryEnter(CurrentWaitQueue, Commons.ReadLOCKLow_TimeOut))
                {
                    try
                    {
                        #region Process_InvokeQueue Items
                        Parallel.For(0, CurrentWaitQueue.Count, (int Index_BaseWtToken, ParallelLoopState LoopStat) =>
                        {
                            try
                            {
                                BaseWaitToken BaseWtToken = null;
                                ///Dequeue Items Successfuly
                                if (!CurrentWaitQueue.TryDequeue(out BaseWtToken))
                                {
                                    ///LoopStat.Break();
                                    return;
                                }
                                try
                                {
                                    if (BaseWtToken != null)
                                    {
                                        ///Process Single BaseWaitQueueToken
                                        ProcessWaitQueueToken(NextWaitQueue, BaseWtToken);
                                    }
                                }
                                catch
                                {
                                }
                            }
                            catch { }
                        });
                        #endregion
                    }
                    finally
                    {
                        Monitor.Exit(CurrentWaitQueue);
                    }
                }
            }
            catch
            { }
        }

        /// <summary>
        /// ProcessWaitQueueToken functions either place BaseWaitQueue Token in NextWaitQueue
        /// or in LastInvokeQueue,TimeWaitList
        /// </summary>
        /// <param name="NextWaitQueue"></param>
        /// <param name="WtToken"></param>
        internal void ProcessWaitQueueToken(ConcurrentQueue<BaseWaitToken> NextWaitQueue, BaseWaitToken BaseWtToken)
        {
            try
            {
                if (!BaseWtToken.IsCancel)
                {
                    ///Process BaseWaitToken Either to Place In NextWaitQueue or
                    ///In LastInvokeQueue to register for TimeWaitHandler 
                    int currentLevel = NextNLevelQueue(BaseWtToken);
                    //if (currentLevel == AsyncTimeOuts.Q1Wait)
                    //    LastInvokeQueue.Enqueue(BaseWtToken);
                    ///else 
                    if (currentLevel == AsyncTimeOuts.InvokeQueue || currentLevel == AsyncTimeOuts.WaitList || NextWaitQueue == null)
                        TimeWaitList.Add(BaseWtToken);
                    else
                        NextWaitQueue.Enqueue(BaseWtToken);
                }
                else
                {
                    BaseWtToken.Dispose();
#if Enable_DEBUG_ECHO
                    Console.Out.WriteLine(String.Format("Base Wait Token Disposed Off {0}", BaseWtToken));
#endif
                }
            }
            catch
            { }
        }

        /// <summary>
        /// Invoke_WaitHandlerResponse  
        /// </summary>
        /// <param name="WtTokenHandler"></param>
        internal void Invoke_WaitHandlerResponse(BaseWaitToken WtTokenHandler)
        {
            TimeSpan TimeToTrigger = TimeSpan.MinValue;
            try
            {
                if (WtTokenHandler.TriggerDuration <= MinTimeDuration || WtTokenHandler.IsInvokeRequire)
                {

                    try
                    {
                        if (WtTokenHandler.IsInvokeRequire)
                            TimeToTrigger = WtTokenHandler.TriggerTime.Subtract(DateTime.Now.TimeOfDay);
                        if (TimeToTrigger.TotalMilliseconds < 0)
                            TimeToTrigger = TimeSpan.FromSeconds(0);
                    }
                    catch
                    {
                        TimeToTrigger = TimeSpan.FromSeconds(0);
                    }

                    ///Perform Process Invoke WaitOrTimer Register
                    WtTokenHandler.Wt_Handler =
                        ThreadPool.RegisterWaitForSingleObject(WtTokenHandler.WaitHandler.Value,
                        WtTokenHandler.WaitOrTimerCallbackLocal, WtTokenHandler.State,
                        TimeToTrigger, true);
                }
                else if (WtTokenHandler.IsCancel)
                    WtTokenHandler.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
#if Enable_DEBUG_ECHO
                Console.Out.WriteLine(String.Format("TimeToTrigger:{0} Invoke_WaitHandlerResponse", TimeToTrigger));
#endif
            }
        }

        internal void Init_Structs()
        {
            try
            {
                ///Init Last TimeWaitQueue
                TimeWaitList = ArrayList.Synchronized(new ArrayList(WaitListCapacity));
                ///Init WaitQueues Structure Here
                WaitQueue = new List<ConcurrentQueue<BaseWaitToken>>(QueueLevels);
                for (int count = 1; count <= QueueLevels; count++)
                    WaitQueue.Add(new ConcurrentQueue<BaseWaitToken>());
                ///Init _LastInvokeQueue
                _LastInvokeQueue = new ConcurrentQueue<BaseWaitToken>();
                ///Init _EntryLevelQueue
                _EntryLevelQueue = new ConcurrentQueue<BaseWaitToken>();
                #region ///Init_LastInvokeQueueTimer
                _LastInvokeQueueTimer = new System.Timers.Timer();
                _LastInvokeQueueTimer.AutoReset = true;
                _LastInvokeQueueTimer.Interval = PollingTimeLow.TotalMilliseconds;
                _LastInvokeQueueTimer.Enabled = true;
                _LastInvokeQueueTimer.Elapsed += new System.Timers.ElapsedEventHandler(_LastInvokeQueueTimer_Elapsed);
                #endregion
                #region ///Init_WaitQueuePollTimer
                WaitQueuePollTimer = new System.Timers.Timer();
                WaitQueuePollTimer.AutoReset = true;
                WaitQueuePollTimer.Interval = PollingTime.TotalMilliseconds;
                WaitQueuePollTimer.Enabled = true;
                WaitQueuePollTimer.Elapsed += new System.Timers.ElapsedEventHandler(WaitQueuePollTimer_Elapsed);
                #endregion
                #region ///Init_EntryLevelQueueTimer
                _EntryLevelQueueTimer = new System.Timers.Timer();
                _EntryLevelQueueTimer.AutoReset = true;
                _EntryLevelQueueTimer.Interval = PollingTimeLow.TotalMilliseconds;
                _EntryLevelQueueTimer.Enabled = true;
                _EntryLevelQueueTimer.Elapsed += new System.Timers.ElapsedEventHandler(_EntryLevelQueueTimer_Elapsed);
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while initializ AsyncTimeouts", ex);
            }
        }

        internal void DeInit_Structs()
        {
            try
            {
                #region Deinit_TimeWaitQueue
                try
                {
                    if (TimeWaitList != null)
                    {
                        foreach (BaseWaitToken wtToken in TimeWaitList)
                        {
                            if (wtToken != null)
                                wtToken.Dispose();
                        }
                        TimeWaitList.Clear();
                        TimeWaitList = null;
                    }
                }
                catch
                { }
                #endregion
                #region #region De-init_TimeWaitQueue
                try
                {
                    ///Deinit WaitQueues Structure Here
                    if (WaitQueue != null)
                    {
                        foreach (var wtQueue in WaitQueue)
                        {
                            foreach (var wtToken in wtQueue)
                            {
                                if (wtToken != null)
                                    wtToken.Dispose();
                            }
                        }
                        WaitQueue = null;
                    }
                }
                catch
                { }
                #endregion
                #region ///DeInit _LastInvokeQueue

                _LastInvokeQueue = new ConcurrentQueue<BaseWaitToken>();
                try
                {
                    if (_LastInvokeQueue != null)
                    {
                        foreach (BaseWaitToken wtToken in _LastInvokeQueue)
                        {
                            if (wtToken != null)
                                wtToken.Dispose();
                        }
                        _LastInvokeQueue = null;
                    }
                }
                catch
                { }
                #endregion
                #region ///De-Init _EntryLevelQueue
                try
                {
                    if (_EntryLevelQueue != null)
                    {
                        foreach (BaseWaitToken wtToken in _EntryLevelQueue)
                        {
                            if (wtToken != null)
                                wtToken.Dispose();
                        }
                        _EntryLevelQueue = null;
                    }
                }
                catch
                { }
                #endregion
            }
            catch { }
        }

        #endregion

        #region Event_Handlers

        internal void WaitQueuePollTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                #region ///DisableInterval WaitQueuePollTimer
                WaitQueuePollTimer.Enabled = false;
                WaitQueuePollTimer.Interval = PollingTime.TotalMilliseconds;
                #endregion
                //Process Process_WaitQueue Here
                Process_WaitQueue();
            }
            catch { }
            finally
            {
                ///Re_Enable Short Interval LastInvokeQueueTimer
                if (WaitQueuePollTimer != null)
                    WaitQueuePollTimer.Enabled = true;
            }
        }

        internal void _LastInvokeQueueTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                ///Disable Short Interval LastInvokeQueueTimer
                _LastInvokeQueueTimer.Enabled = false;
                //Process _LastInvokeQueueTimer Here
                Process_InvokeQueue();
            }
            catch { }
            finally
            {
                ///Re_Enable Short Interval LastInvokeQueueTimer
                if (_LastInvokeQueueTimer != null)
                    _LastInvokeQueueTimer.Enabled = true;
            }
        }

        internal void _EntryLevelQueueTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                ///Disable Short Interval LastInvokeQueueTimer
                _EntryLevelQueueTimer.Enabled = false;
                ///Process EnteryLevelQueueTimer_Elapsed Here
                Process_EntryLevelWaitQueue();
            }
            catch { }
            finally
            {
                ///Re_Enable Short Interval LastInvokeQueueTimer
                if (_EntryLevelQueueTimer != null)
                    _EntryLevelQueueTimer.Enabled = true;
            }
        }

        #endregion

        public void Dispose()
        {
            ///Dispose All Objects
            try
            {
                DeInit_Structs();
            }
            catch { }
        }
    }

    #region BaseWaitToken

    public class BaseWaitToken : IDisposable
    {
        public static TimeSpan MaxTimeOutValue = TimeSpan.FromMinutes(120);
        public static TimeSpan MinTimeOutValue = TimeSpan.FromSeconds(05);

        #region Data_Members

        private readonly TimeSpan _WaitDuration = TimeSpan.MinValue;
        private readonly TimeSpan _triggerTime = TimeSpan.MinValue;
        private Lazy<WaitHandle> _waitHandler = null;
        private WaitOrTimerCallback _callBack = null;
        private object state = null;
        internal RegisteredWaitHandle Wt_Handler = null;

        #endregion

        #region Property

        public TimeSpan TriggerTime
        {
            get { return _triggerTime; }
        }

        public TimeSpan TriggerDuration
        {
            get
            {
                try
                {
                    return TriggerTime.Subtract(DateTime.Now.TimeOfDay);
                }
                catch
                {
                }
                return TimeSpan.FromSeconds(0);
            }
        }

        public TimeSpan WaitDuration
        {
            get { return _WaitDuration; }
        }

        public Lazy<WaitHandle> WaitHandler
        {
            get { return _waitHandler; }
        }

        public WaitOrTimerCallback CallBack
        {
            get { return _callBack; }
        }

        public object State
        {
            get { return state; }
            set { state = value; }
        }

        public bool IsCancel
        {
            get
            {
                try
                {
                    if (WaitHandler == null ||
                        (WaitHandler.IsValueCreated &&
                        (WaitHandler.Value.SafeWaitHandle.IsClosed ||
                        WaitHandler.Value.SafeWaitHandle.IsInvalid)))
                        return true;
                    else
                        return false;
                }
                catch { }
                return true;
            }
        }

        public bool IsInvokeRequire
        {
            get
            {
                try
                {
                    if (IsCancel || TriggerTime < DateTime.Now.TimeOfDay)
                        return false;
                    else
                        return true;
                }
                catch { }
                return false;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// BaseWaitToken Constructor To Initialize WaitToken
        /// </summary>
        /// <param name="_triggerTime"></param>
        /// <param name="_waitTime"></param>
        public BaseWaitToken(TimeSpan _triggerTimeLocal, Lazy<WaitHandle> _waitHandlerLocal,
            WaitOrTimerCallback CallBackLocal, Object stateLocal = null)
        {
            try
            {
                if (_triggerTimeLocal <= MinTimeOutValue || _triggerTimeLocal >= MaxTimeOutValue ||
                        _waitHandlerLocal == null || CallBackLocal == null)
                {
                    throw new ArgumentException("BaseWaitToken Invalid Argument to init");
                }
                _WaitDuration = _triggerTimeLocal;
                ///Next Triggered Time
                _triggerTime = DateTime.Now.Add(WaitDuration).TimeOfDay;
                _waitHandler = _waitHandlerLocal;
                _callBack = CallBackLocal;
                state = stateLocal;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("BaseWaitToken unable to init", ex);
            }
        }

        /// <summary>
        /// Copy Constructor BaseWaitToken
        /// </summary>
        /// <param name="WtToken"></param>
        public BaseWaitToken(BaseWaitToken WtToken, System.Func<WaitHandle> ValFactory)
        {
            try
            {
                _WaitDuration = WtToken._WaitDuration;
                ///Next Triggered Time
                _triggerTime = DateTime.Now.Add(WtToken.WaitDuration).TimeOfDay;
                _waitHandler = new Lazy<WaitHandle>(ValFactory, true);
                _callBack = WtToken.CallBack;
                state = WtToken.State;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("BaseWaitToken unable to init", ex);
            }
        }

        #endregion

        /// <summary>
        /// Delegate function to be register with thread pool
        /// </summary>
        /// <param name="state"></param>
        /// <param name="timedOut"></param>
        internal void WaitOrTimerCallbackLocal(object state, bool timedOut)
        {
            try
            {
                if (CallBack != null)
                    CallBack.Invoke(state, timedOut);
            }
            catch { }
            finally
            {
                this.Dispose();
            }
        }

        public void Dispose()
        {
            try
            {
                if (Wt_Handler != null && _waitHandler != null)
                    Wt_Handler.Unregister(_waitHandler.Value);
                if (_waitHandler != null && _waitHandler.IsValueCreated)
                    _waitHandler.Value.Close();
            }
            catch { }
        }

        ~BaseWaitToken()
        {
            Dispose();
            _waitHandler = null;
            state = null;
            _callBack = null;
        }
    }

    #endregion
}
