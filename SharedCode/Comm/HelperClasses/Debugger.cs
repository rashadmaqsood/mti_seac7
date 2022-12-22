using System;
using System.Collections;
using DLMS;
using System.Threading;
using LogSystem.Shared.Common;
using SharedCode.Common;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace SharedCode.Comm.HelperClasses
{
    public class Debugger : IDisposable
    {
        #region Data_Member

        private Action<ArrayList> GetDLMSLoggers;
        public event LogMessage Logger = delegate { };
        public event Action<string, byte[], DataStatus, DateTime> IOLog = delegate { };

        private bool enableProcessInfoLog = true;
        private bool enableIOLog = true;
        private bool enableErrorLog = true;

        private ConcurrentDictionary<string, bool> _enableProcessInfoLog;
        private ConcurrentDictionary<string, bool> _enableIOLog;
        private ConcurrentDictionary<string, bool> _enableErrorLog;

        private int maxLogInvoke = 5;
        private Thread DebuggerThread = null;

        private ILogWriter _RegisterActivityLogger = null;

        #endregion

        #region Properties

        public bool EnableProcessInfoLog
        {
            get { return enableProcessInfoLog; }
            set { enableProcessInfoLog = value; }
        }

        public bool EnableErrorLog
        {
            get { return enableErrorLog; }
            set { enableErrorLog = value; }
        }

        public bool EnableIOLog
        {
            get { return enableIOLog; }
            set { enableIOLog = value; }
        }

        public ConcurrentDictionary<string, bool> EnableProcessingInfoLog
        {
            get { return _enableProcessInfoLog; }
            set { _enableProcessInfoLog = value; }
        }

        public ConcurrentDictionary<string, bool> EnableIOFlowLogs
        {
            get { return _enableIOLog; }
            set { _enableIOLog = value; }
        }

        public ConcurrentDictionary<string, bool> EnableErrorInfoLog
        {
            get { return _enableErrorLog; }
            set { _enableErrorLog = value; }
        }

        public Action<ArrayList> GetDLMSLoggersDlg
        {
            get { return GetDLMSLoggers; }
            set { GetDLMSLoggers = value; }
        }

        public int MaxLogInvoke
        {
            get { return maxLogInvoke; }
            set { maxLogInvoke = value; }
        }

        public DLMSLogger Register
        {
            set
            {
                // Handlers & Default Logging Options
                value.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(value_PropertyChanged);
                value.InvokeLoggerDlg = new LogMessage(DataLogger);
                value.IOLogDlg = new Action<string, byte[], DataStatus, DateTime>(DataIOLogger);
                // Apply Default Logger Options &  Per Identifier Options
                ApplyLoggerRights(value);
            }
        }

        public ILogWriter RegisterActivityLogger
        {
            get
            {
                return _RegisterActivityLogger;
            }
            set
            {
                _RegisterActivityLogger = value;
            }
        }

        #endregion

        public Debugger()
        {
            _enableProcessInfoLog = new ConcurrentDictionary<string, bool>();
            _enableIOLog = new ConcurrentDictionary<string, bool>();
            _enableErrorLog = new ConcurrentDictionary<string, bool>();

            // Register Debugger Activity Logger Events
            Logger += Debugger_ActivityLogger;
            IOLog += Debugger_ActivityLogger_IOLog;
        }

        internal void DebuggrRunnerThread()
        {
            ArrayList DLMSLoggers = ArrayList.Synchronized(new ArrayList());
            try
            {
                bool processPending = false;

                while (true)
                {
                    processPending = false;
                    GetDLMSLoggersDlg.Invoke(DLMSLoggers);
                    if (DLMSLoggers.Count > 0)
                        Parallel.For(0, DLMSLoggers.Count, (j) =>
                        {
                            try
                            {
                                if (DLMSLoggers.Count <= 0)
                                    return;
                                DLMSLogger dlmsLogger = (DLMSLogger)DLMSLoggers[0];
                                DLMSLoggers.RemoveAt(0);
                                DLMSLogger _log = (DLMSLogger)dlmsLogger;

                                if (dlmsLogger != null &&
                                    dlmsLogger.TaskList.Count > 0)
                                {
                                    if (dlmsLogger.TaskList.Count > MaxLogInvoke)
                                        processPending = true;
                                    ProcessLog(dlmsLogger, MaxLogInvoke);
                                }
                            }
                            finally
                            {
                            }
                        });

                    if (!processPending)
                        Commons.PreciseDelay(5000);
                }
            }
            catch { }
        }

        public void Reset_Debugger()
        {
            try
            {
                _enableProcessInfoLog.Clear();
                _enableIOLog.Clear();
                _enableErrorLog.Clear();
                if (DebuggerThread != null)
                {
                    try
                    {
                        DebuggerThread.Abort();
                        DebuggerThread = null;
                    }
                    catch (Exception)
                    { }
                }

                DebuggerThread = new Thread(DebuggrRunnerThread);
                DebuggerThread.Start();
            }
            catch { }
        }

        public void ProcessLog(DLMSLogger logger, int maxInvoke)
        {
            try
            {
                do
                {
                    int count = 0;
                    {
                        DLMS.DLMSLogger.LoggerJob loggerTk = logger.DequeueLoggerJob();
                        if (loggerTk == null)
                            return;
                        loggerTk.LoggerHandler.Invoke(loggerTk.ParamsList);
                        count++;
                    }
                } while (logger != null);
            }
            catch (Exception ex) { }
        }

        public void DataLogger(string identifier, String msg, byte[] IODump, DateTime dtTimeStamp, LogType LogType)
        {
            try
            {
                if (Logger != null)
                    Logger.Invoke(identifier, msg, IODump, dtTimeStamp, LogType);
            }
            catch (Exception)
            {
            }
        }

        public void DataIOLogger(string identifier, byte[] IODump, DataStatus RWStatus, DateTime dtTimeStamp)
        {
            try
            {
                if (IOLog != null)
                    IOLog.Invoke(identifier, IODump, RWStatus, dtTimeStamp);
            }
            catch (Exception)
            {
            }
        }

        #region DLMS_Debugger_Event_Handlers

        private void Debugger_ActivityLogger(string identifier, string msg, byte[] IODump, DateTime dtTimeStamp, LogType LogMessageType)
        {
            try
            {
                if (_RegisterActivityLogger == null)
                    return;

                if (string.IsNullOrEmpty(identifier))
                    identifier = string.Empty;
                else
                {
                    identifier = identifier.Replace(".", "_");
                    identifier = identifier.Replace(":", "_");
                }

                string hexDt = string.Empty;
                if (IODump != null)
                    hexDt = Commons.ArrayToHexString(IODump, 16, false);

                string msgtxt = String.Format(" {0,10}\t{1,-8}{2,-2}", identifier, msg, hexDt);

                if (_RegisterActivityLogger != null &&
                    LogMessageType == LogType.ErrorLog)
                    _RegisterActivityLogger.LogErrorMessage(msgtxt);
                else
                    _RegisterActivityLogger.LogMessage(msgtxt);
            }
            catch
            { }
        }

        private void Debugger_ActivityLogger_IOLog(string identifier, byte[] IODump, DataStatus ReadStatus, DateTime dtTimeStamp)
        {
            try
            {
                if (_RegisterActivityLogger == null)
                    return;

                string hexDt = "";
                if (IODump != null)
                    hexDt = Commons.ArrayToHexString(IODump, 16, false);


                string msg = String.Format(" {0,10}\t{1,-8}{2,-2}", identifier, ReadStatus, hexDt);

                if (_RegisterActivityLogger != null)
                    _RegisterActivityLogger.LogMessage(msg);
            }
            catch { }
        }


        #endregion

        /// <summary>
        /// Response If Identifier Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void value_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                // Respond to Identifier Change Event 
                if (sender != null && sender is DLMSLogger &&
                    "Identifier".Equals(e.PropertyName))
                {
                    DLMSLogger Logger = (DLMSLogger)sender;
                    ApplyLoggerRights(Logger);
                }
            }
            catch { }
        }

        private void ApplyLoggerRights(DLMSLogger Logger)
        {
            try
            {
                if (Logger != null)
                {
                    // Disable Logs
                    if (String.IsNullOrEmpty(Logger.Identifier) ||
                        String.IsNullOrWhiteSpace(Logger.Identifier))
                    {
                        Logger.EnableErrorLog = EnableErrorLog;
                        Logger.EnableIOLog = false;
                        Logger.EnableProcessInfoLog = false;
                        return;
                    }
                    Logger.EnableErrorLog = EnableErrorLog;
                    Logger.EnableIOLog = EnableIOLog;
                    Logger.EnableProcessInfoLog = EnableProcessInfoLog;

                    // Apply Specific Meter Connection
                    String identifier = Logger.Identifier;
                    if (identifier != null)
                    {
                        if (EnableProcessingInfoLog.Keys.Contains(identifier))
                        {
                            bool val = EnableProcessingInfoLog[identifier];
                            Logger.EnableProcessInfoLog = val;
                        }
                        if (EnableIOFlowLogs.Keys.Contains(identifier))
                        {
                            bool val = EnableIOFlowLogs[identifier];
                            Logger.EnableIOLog = val;
                        }
                        if (EnableErrorInfoLog.Keys.Contains(identifier))
                        {
                            bool val = EnableErrorInfoLog[identifier];
                            Logger.EnableErrorLog = val;
                        }
                    }
                }
            }
            catch { }
        }

        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                if (DebuggerThread != null)
                {
                    try
                    {
                        DebuggerThread.Abort();
                        DebuggerThread = null;
                    }
                    catch (Exception)
                    { }
                    Delegate[] Handlers = null;

                    #region // Remove Logger Event Handlers

                    Handlers = null;
                    if (Logger != null)
                    {
                        Handlers = Logger.GetInvocationList();
                        foreach (Delegate item in Handlers)
                        {
                            Logger -= (LogMessage)item;
                        }
                    }

                    #endregion
                    #region // Remove Logger Event Handlers

                    Handlers = null;
                    if (IOLog != null)
                    {
                        Handlers = IOLog.GetInvocationList();
                        foreach (Delegate item in Handlers)
                        {
                            IOLog -= (Action<string, byte[], DataStatus, DateTime>)item;
                        }
                    }

                    #endregion
                }
            }
            catch (Exception ex) { }
        }

        #endregion
    }
}
