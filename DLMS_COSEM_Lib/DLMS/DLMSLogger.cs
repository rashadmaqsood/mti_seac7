using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections;
using System.ComponentModel;
using DLMS.Comm;

namespace DLMS
{
    public delegate void LogMessage(string identifier, String msg, byte[] IODump, DateTime dtTimeStamp, LogType LogMsgType);

    public class DLMSLogger : IDisposable, INotifyPropertyChanged
    {
        #region Data_Members

        private LogMessage invokeLogger = delegate { };
        private Action<string, byte[], DataStatus, DateTime> IOLog = delegate { };
        private ArrayList taskList;
        
        private bool enableProcessInfoLog = false;
        private bool enableErrorLog = false;
        private bool enableIOLog = false;
        private string identifier = null;

        #endregion

        #region Property
        
        public ArrayList TaskList
        {
            get { return taskList; }
            set { taskList = value; }
        }

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
        
        public LogMessage InvokeLoggerDlg
        {
            get { return invokeLogger; }
            set { invokeLogger = value; }
        }
        
        public Action<string, byte[], DataStatus, DateTime> IOLogDlg
        {
            get { return IOLog; }
            set { IOLog = value; }
        }

        public string Identifier
        {
            get { return identifier; }
            set
            { 
                identifier = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Identifier"));
            }
        }

        #endregion

        #region Singleton Pattern

        private static DLMSLogger logger;
        
        internal static DLMSLogger GetInstance()
        {
            if (logger == null)
            {
                logger = new DLMSLogger();
            }
            return logger;
        }

        internal DLMSLogger()
        {/// 
            taskList = new ArrayList();
            taskList = ArrayList.Synchronized(taskList);
            identifier = "";
            //loggerThread = new Thread(this.LoggerHelper);
            //loggerThread.Name = "DLMS_Logger_Thread";
            //loggerThread.Priority = ThreadPriority.Normal;
            //loggerThread.Start();
        }
        #endregion

        #region LOGALMessages
        
        public void LogALMessage(String message, ALMessageType MsgType, PacketType PackType)
        {
            if (!EnableProcessInfoLog)
                return;

            // Debugger.Invoke(String.Format("AL_{0}_{1}_{2}", PackType, MsgType, message), LoggingMessage.Debugger);
            LoggingParameterList paraList = new LoggingParameterList();
            paraList.Paramters.Add(Identifier);
            paraList.Paramters.Add(message);
            paraList.Paramters.Add(MsgType);
            paraList.Paramters.Add(PackType);
            paraList.Paramters.Add(DateTime.Now);

            LoggerJob loggerJob = new LoggerJob(paraList, new WaitCallback(LogALMessage_Helper));
            EnqueLoggerJob(loggerJob);
            // ThreadPool.QueueUserWorkItem(new WaitCallback(LoggerHelper));
        }

        /// String message,ALMessageType MsgType,PacketType PackType
        internal void LogALMessage_Helper(Object ParaList)
        {
            try
            {
                string identifier = (string)((LoggingParameterList)ParaList).Paramters[0];
                String message = (String)((LoggingParameterList)ParaList).Paramters[1];
                ALMessageType MsgType = (ALMessageType)((LoggingParameterList)ParaList).Paramters[2];
                PacketType PackType = (PacketType)((LoggingParameterList)ParaList).Paramters[3];
                DateTime dtTimeStamp = (DateTime)((LoggingParameterList)ParaList).Paramters[4];

                // May Include Filtering Work/Info
                string msg = String.Format("AL_{0}_{1}_{2}", PackType, MsgType, message);
                if (InvokeLoggerDlg != null)
                    InvokeLoggerDlg.Invoke(identifier, msg, null, dtTimeStamp,LogType.ProcessInfoLog);
                // Debugger.Invoke(String.Format("AL_{0}_{1}_{2}", PackType, MsgType, message), LoggingMessage.Debugger);
            }
            catch (Exception ex)
            {
                // Debugger.Invoke("Error Displaying Application Layer Debugger Message", LoggingMessage.Debugger);
                // Logger.Invoke("Error Displaying Application Layer Logger Message", DateTime.Now);
            }
        }
        
        #endregion

        #region LogAPDecodingError
        
        internal void LogAPDecodingError(DLMSDecodingException ex,byte [] IODump,int offSet,int Count , StOBISCode OBIS_Code)
        {
            try
            {
                if (!EnableErrorLog)
                    return;

                // May Include Filter Work Base On OBIS_Code ETC
                byte[] TArray = CopyIODump(IODump, offSet, Count);
                // Debugger.Invoke(String.Format("AP_{0}_{1}_{2}",OBIS_Code,ex.Message,ex.DecoderMethod), LoggingMessage.Debugger);  
                LoggingParameterList paraList = new LoggingParameterList();
                paraList.Paramters.Add(Identifier);
                paraList.Paramters.Add(ex);
                paraList.Paramters.Add(TArray);
                paraList.Paramters.Add(OBIS_Code);
                paraList.Paramters.Add(DateTime.Now);

                LoggerJob loggerJob = new LoggerJob(paraList, new WaitCallback(LogAPDecodingError_Helper));
                EnqueLoggerJob(loggerJob);
                // ThreadPool.QueueUserWorkItem(new WaitCallback(LoggerHelper));
            }
            catch (Exception)
            {
                // Debugger.Invoke("Error Displaying Application Layer Debugger Message", LoggingMessage.Debugger);
                // Logger.Invoke("Error Displaying Application Layer Logger Message", DateTime.Now);
            }
        }

        /// String DLMSDecodingException,Get_Index
        private void LogAPDecodingError_Helper(Object ParaList)
        {
            try
            {
                string identifier = (string)((LoggingParameterList)ParaList).Paramters[0];
                DLMSDecodingException ex = (DLMSDecodingException)((LoggingParameterList)ParaList).Paramters[1];
                byte[] IODump = (byte[])((LoggingParameterList)ParaList).Paramters[2];
                StOBISCode OBIS_Code = (StOBISCode)((LoggingParameterList)ParaList).Paramters[3];
                DateTime dtTimeStamp = (DateTime)((LoggingParameterList)ParaList).Paramters[4];

                string msg = String.Format("AP_{0}({1})_{2}_{3}", OBIS_Code.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode)
                    , OBIS_Code.OBISIndex, ex.Message, ex.DecoderMethod);

                if (InvokeLoggerDlg != null)
                    InvokeLoggerDlg.Invoke(identifier, msg, IODump, dtTimeStamp, LogType.ErrorLog);

                // May Include Filtering Work/Info
                // Debugger.Invoke(String.Format("AP_{0}_{1}_{2}", OBIS_Code, ex.Message, ex.DecoderMethod), LoggingMessage.Debugger);
                // Logger.Invoke(String.Format("AP_{0}_{1}_{2}", OBIS_Code, ex.Message, ex.DecoderMethod), DateTime.Now);
            }
            catch (Exception ex)
            {
                // Debugger.Invoke("Error Displaying Application Layer Debugger Message", LoggingMessage.Debugger);
                // Logger.Invoke("Error Displaying Application Layer Logger Message", DateTime.Now);
            }
        } 
        
        #endregion

        #region LogAPEncodingError
        
        //internal void LogAPEncodingError(DLMSEncodingException ex, Get_Index OBIS_Code)
        //{
        //    ///May Include Filter Work Base On OBIS_Code ETC
        //    Debugger.Invoke(String.Format("AP_{0}_{1}_{2}", OBIS_Code, ex.Message, ex.EncoderMethod), LoggingMessage.Debugger);
        //}

        internal void LogAPEncodingError(DLMSEncodingException ex,byte []IODump,int offSet,int Count ,StOBISCode OBIS_Code)
        {
            try
            {
                ///May Include Filter Work Base On OBIS_Code ETC
                //Debugger.Invoke(String.Format("AP_{0}_{1}_{2}",OBIS_Code,ex.Message,ex.DecoderMethod), LoggingMessage.Debugger);  
                if (!EnableErrorLog)
                    return;
                byte[] TArray = CopyIODump(IODump, offSet, Count);
                LoggingParameterList paraList = new LoggingParameterList();
                paraList.Paramters.Add(Identifier);
                paraList.Paramters.Add(ex);
                paraList.Paramters.Add(IODump);
                paraList.Paramters.Add(OBIS_Code);
                paraList.Paramters.Add(DateTime.Now);

                LoggerJob loggerJob = new LoggerJob(paraList, new WaitCallback(LogAPEncodingError_Helper));
                EnqueLoggerJob(loggerJob);
                //ThreadPool.QueueUserWorkItem(new WaitCallback(LoggerHelper));
            }
            catch (Exception)
            {
                //Debugger.Invoke("Error Displaying Application Layer Debugger Message", LoggingMessage.Debugger);
                //Logger.Invoke("Error Displaying Application Layer Logger Message", DateTime.Now);
            }
        }

        /// String DLMSDecodingException,Get_Index
        private void LogAPEncodingError_Helper(Object ParaList)
        {
            try
            {
                string identifier = (string)((LoggingParameterList)ParaList).Paramters[0];
                DLMSEncodingException ex = (DLMSEncodingException)((LoggingParameterList)ParaList).Paramters[1];
                byte[] IODump = (byte[])((LoggingParameterList)ParaList).Paramters[2];
                StOBISCode OBIS_Code = (StOBISCode)((LoggingParameterList)ParaList).Paramters[3];
                DateTime dtTimeStamp = (DateTime)((LoggingParameterList)ParaList).Paramters[4];

                string msg = String.Format("AP_{0}({1})_{2}_{3}", OBIS_Code.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode)
                    , OBIS_Code.OBISIndex, ex.Message, ex.EncoderMethod);

                if (InvokeLoggerDlg != null)
                    InvokeLoggerDlg.Invoke(identifier, msg, IODump, dtTimeStamp, LogType.ErrorLog);

            }
            catch (Exception ex)
            {
                //Debugger.Invoke("Error Displaying Application Layer Debugger Message", LoggingMessage.Debugger);
                //Logger.Invoke("Error Displaying Application Layer Logger Message", DateTime.Now);
            }

        }
        #endregion

        #region LogAPDecodingError
        
        //internal void LogAPError(DLMSException ex, Get_Index OBIS_Code, PacketType packetType)
        //{
        //    ///May Include Filter Work Base On OBIS_Code ETC
        //    Debugger.Invoke(String.Format("AP_{0}_{1}_{2}", OBIS_Code, packetType, ex.Message), LoggingMessage.Debugger);
        //}

        internal void LogAPError(DLMSException ex, byte []IODump,int offSet,int Count , StOBISCode OBIS_Code, PacketType packetType)
        {
            try
            {
                if (!EnableErrorLog)
                    return;
                ///May Include Filter Work Base On OBIS_Code ETC
                //Debugger.Invoke(String.Format("AP_{0}_{1}_{2}",OBIS_Code,ex.Message,ex.DecoderMethod), LoggingMessage.Debugger);  
                byte[] TArray = CopyIODump(IODump, offSet, Count);
                LoggingParameterList paraList = new LoggingParameterList();
                paraList.Paramters.Add(Identifier);
                paraList.Paramters.Add(ex);
                paraList.Paramters.Add(TArray);
                paraList.Paramters.Add(OBIS_Code);
                paraList.Paramters.Add(packetType);
                paraList.Paramters.Add(DateTime.Now);

                LoggerJob loggerJob = new LoggerJob(paraList, new WaitCallback(LogAPError_Helper));
                EnqueLoggerJob(loggerJob);
                //ThreadPool.QueueUserWorkItem(new WaitCallback(LoggerHelper));
            }
            catch (Exception)
            {
                //Debugger.Invoke("Error Displaying Application Layer Debugger Message", LoggingMessage.Debugger);
                //Logger.Invoke("Error Displaying Application Layer Logger Message", DateTime.Now);
            }
        }

        /// String DLMSDecodingException,Get_Index
        private void LogAPError_Helper(Object ParaList)
        {
            try
            {
                string identifier = (string)((LoggingParameterList)ParaList).Paramters[0];
                DLMSException ex = (DLMSException)((LoggingParameterList)ParaList).Paramters[1];
                byte[] IODump = (byte[])((LoggingParameterList)ParaList).Paramters[2];
                StOBISCode OBIS_Code = (StOBISCode)((LoggingParameterList)ParaList).Paramters[3];
                PacketType packetType = (PacketType)((LoggingParameterList)ParaList).Paramters[4];
                DateTime dtTimeStamp = (DateTime)((LoggingParameterList)ParaList).Paramters[5];

                string msg = String.Format("AP_{0}({1})_{2}_{3}_Details:{4}", OBIS_Code.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode)
                    , OBIS_Code.OBISIndex, packetType, ex.Message,(ex.InnerException != null)?ex.InnerException.Message:"NIL");

                if (InvokeLoggerDlg != null)
                    InvokeLoggerDlg.Invoke(identifier, msg, IODump, dtTimeStamp, LogType.ErrorLog);

                ///May Include Filtering Work/Info
                //Debugger.Invoke(String.Format("AP_{0}_{1}_{2}", OBIS_Code, packetType, ex.Message), LoggingMessage.Debugger);
                //Logger.Invoke(String.Format("AP_{0}_{1}_{2}", OBIS_Code, packetType, ex.Message), DateTime.Now);
            }
            catch (Exception ex)
            {
                //Debugger.Invoke("Error Displaying Application Layer Debugger Message", LoggingMessage.Debugger);
                //Logger.Invoke("Error Displaying Application Layer Logger Message", DateTime.Now);
            }

        }
        #endregion

        #region LOGAPMessage
        
        public void LogAPMessage(String Message, PacketType packType)
        {
            if (!EnableProcessInfoLog)
                return;
            //Debugger.Invoke(String.Format("AP_{0}_{1}", packType, Message), LoggingMessage.Debugger);
            LoggingParameterList paraList = new LoggingParameterList();
            paraList.Paramters.Add(Identifier);
            paraList.Paramters.Add(Message);
            paraList.Paramters.Add(packType);
            paraList.Paramters.Add(DateTime.Now);

            LoggerJob loggerJob = new LoggerJob(paraList, new WaitCallback(LogAPMessage_Helper));
            EnqueLoggerJob(loggerJob);
        }

        ///String Message, PacketType packType
        private void LogAPMessage_Helper(Object paraList)
        {
            try
            {
                string identifier = (string)((LoggingParameterList)paraList).Paramters[0];
                String Message = (String)((LoggingParameterList)paraList).Paramters[1];
                PacketType packType = (PacketType)((LoggingParameterList)paraList).Paramters[2];
                DateTime dtTimeStamp = (DateTime)((LoggingParameterList)paraList).Paramters[3];
                ///May Include Filter Work Base On OBIS_Code ETC
                string detail_msg = String.Format("AP_{0}_{1}", packType, Message);
                if(InvokeLoggerDlg != null)
                    InvokeLoggerDlg.Invoke(identifier, detail_msg, null, dtTimeStamp, LogType.ProcessInfoLog);
                //Debugger.Invoke(String.Format("AP_{0}_{1}", packType, Message), LoggingMessage.Debugger);
                //Logger.Invoke(String.Format("AP_{0}_{1}", packType, Message), DateTime.Now);
            }
            catch (Exception ex)
            {
                ///Logger.Invoke("Error Displaying Application Process Logger Message", DateTime.Now);
            }
        }

        #endregion

        #region LOGAP_OBISCodeDecoded
        
        internal void LogAP_OBISCodeDecoded(Base_Class arg)
        {
            if (!EnableProcessInfoLog)
                return;

            LoggingParameterList paraList = new LoggingParameterList();
            paraList.Paramters.Add(Identifier);
            paraList.Paramters.Add(arg);
            paraList.Paramters.Add(DateTime.Now);

            LoggerJob loggerJob = new LoggerJob(paraList, new WaitCallback(LogAP_OBISCodeDecoded_Helper));
            EnqueLoggerJob(loggerJob);
            
        }
        
        /// <summary>
        /// Private Scope Helper Method To Make Data & Display On Form
        /// </summary>
        /// <param name="paraList"></param>
        private void LogAP_OBISCodeDecoded_Helper(Object paraList)
        {
            ///May Include Filter Work Base On OBIS_Code ETC
            StringBuilder txt = new StringBuilder();
            try
            {
                String identifier = (String)((LoggingParameterList)paraList).Paramters[0];
                Base_Class arg = (Base_Class)((LoggingParameterList)paraList).Paramters[1];
                DateTime dtTimeStamp = (DateTime)((LoggingParameterList)paraList).Paramters[2];

                try
                {
                    String StrVal = arg.ToString();
                    String[] Splits = StrVal.Split(",".ToCharArray());
                    foreach (string val in Splits)
                        txt.AppendLine(val);
                }
                catch (Exception)
                {
                    txt.Append("AP_<<Error>>displaying decoded information");
                }

                if (InvokeLoggerDlg != null)
                    InvokeLoggerDlg.Invoke(identifier, txt.ToString(), null, dtTimeStamp,LogType.ProcessInfoLog);
            }
            catch (Exception ex)
            {  
            }
            // String _txt = txt.ToString();
            // Debugger.Invoke(_txt, LoggingMessage.Debugger);
            // Logger.Invoke(_txt, DateTime.Now);
        }

        #endregion

        #region LOGIOTrafficMessages
        
        public void LogALTraffic(byte[] dataPacket,int offSet,int Count, DataStatus status)
        {
            if (!EnableIOLog)
                return;
            // IOLog.Invoke((byte[])Encoded_Packet, DataStauts.Write);
            byte[] TArray = CopyIODump(dataPacket, offSet, Count);
            LoggingParameterList paraList = new LoggingParameterList();
            paraList.Paramters.Add(Identifier);
            paraList.Paramters.Add(TArray);
            paraList.Paramters.Add(status);
            paraList.Paramters.Add(DateTime.Now);

            LoggerJob loggerJob = new LoggerJob(paraList, new WaitCallback(LogALTraffic_Helper));
            EnqueLoggerJob(loggerJob);
            // ThreadPool.QueueUserWorkItem(new WaitCallback(LoggerHelper));
        }

        public void LogALTraffic(ArraySegment<byte> dataPacket, DataStatus status)
        {
            if (!EnableIOLog)
                return;
            //IOLog.Invoke((byte[])Encoded_Packet, DataStauts.Write);
            byte[] TArray = CopyIODump(dataPacket.Array, dataPacket.Offset, dataPacket.Count);
            LoggingParameterList paraList = new LoggingParameterList();
            paraList.Paramters.Add(Identifier);
            paraList.Paramters.Add(TArray);
            paraList.Paramters.Add(status);
            paraList.Paramters.Add(DateTime.Now);

            LoggerJob loggerJob = new LoggerJob(paraList, new WaitCallback(LogALTraffic_Helper));
            EnqueLoggerJob(loggerJob);
            // ThreadPool.QueueUserWorkItem(new WaitCallback(LoggerHelper));
        }


        private byte[] CopyIODump(byte[] dataPacket, int offSet, int Count)
        {
            try
            {
                #region Block_Copy

                int length = Count - offSet;
                byte[] TArray = null;
                if (length <= 0 || dataPacket == null || dataPacket.Length < length || offSet < 0 || offSet > dataPacket.Length)
                {
                    TArray = null;
                }
                else
                {
                    TArray = new byte[length];
                    Buffer.BlockCopy(dataPacket, offSet, TArray, 0, TArray.Length);
                }
                return TArray;

                #endregion
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// String message,ALMessageType MsgType,PacketType PackType
        private void LogALTraffic_Helper(Object ParaList)
        {
            try
            {
                string identifier = (string)((LoggingParameterList)ParaList).Paramters[0];
                byte[] dataPacket = (byte[])((LoggingParameterList)ParaList).Paramters[1];
                DataStatus dtStatus = (DataStatus)((LoggingParameterList)ParaList).Paramters[2];
                DateTime dtTimeStamp = (DateTime)((LoggingParameterList)ParaList).Paramters[3];
                if (IOLogDlg != null)
                    IOLogDlg.Invoke(identifier, dataPacket, dtStatus, dtTimeStamp);
                ///May Include Filtering Work/Info
                //IOLog.Invoke(dataPacket, dtStatus);
            }
            catch (Exception ex)
            {
                //Debugger.Invoke("Error Displaying Application Layer Debugger Message", LoggingMessage.Debugger);
                //Logger.Invoke("Error Displaying Application Layer Logger Message", DateTime.Now);
            }
        }

        #endregion
        
        #region Logger Supporting CODE SECTION
        
        public class LoggingParameterList
        {
            private List<Object> paramters;

            public List<Object> Paramters
            {
                get { return paramters; }
                set { paramters = value; }
            }

            public LoggingParameterList()
            {
                paramters = new List<object>(3);
            }

            public LoggingParameterList(List<Object> paraList)
            {
                paramters = paraList;
            }
        }

        private void LoggerHelper(Object dummy)
        {
            while (true)
            {
                try
                {
                    LoggerJob jobx = DequeueLoggerJob();
                    if (jobx != null &&
                       jobx.LoggerHandler != null &&
                       jobx.ParamsList != null &&
                       jobx.ParamsList.Paramters.Count > 0)
                    {
                        jobx.LoggerHandler.Invoke(jobx.ParamsList);
                    }
                    else
                        Thread.Sleep(100);
                }
                catch (ThreadAbortException ex)
                {
                    break;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void EnqueLoggerJob(LoggerJob job)
        {

            //lock (taskList)
            //{
                try
                {
                    taskList.Add(job);
                }
                catch (Exception)
                {

                }
            //}
        }
        
        public LoggerJob DequeueLoggerJob()
        {
            //lock (taskList)
            //{
                try
                {
                    if (taskList.Count > 0)
                    {
                        LoggerJob jobx = (LoggerJob)taskList[0];
                        taskList.RemoveAt(0);
                        return jobx;
                    }
                    else
                        return null;
                }
                catch (Exception)
                {
                    return null;
                }
            //}
        }

        public class LoggerJob
        {
            private LoggingParameterList paramsList;
            private WaitCallback loggerHandler;

            public LoggerJob()
            {

            }

            public LoggerJob(LoggingParameterList paramList, WaitCallback callback)
            {
                this.paramsList = paramList;
                this.loggerHandler = callback;
            }

            public LoggingParameterList ParamsList
            {
                get { return paramsList; }
                set { paramsList = value; }
            }

            public WaitCallback LoggerHandler
            {
                get { return loggerHandler; }
                set { loggerHandler = value; }
            }

        }

        #endregion

        #region ~DLMSLogger
        ~DLMSLogger()
        {
            Dispose();
        } 
        #endregion

        #region IDisposable Members
        
        public void Dispose()
        {
            try
            {
                //Detach_Handlers();
                //if (loggerThread != null)
                //{
                //    loggerThread.Abort();
                //    loggerThread = null;
                //}
                if (taskList != null)
                {
                    taskList.Clear();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion
    }

    public enum PacketType : byte
    {
        GET,
        SET,
        EventNotification,
        Action,
        ARQ,
        ARE,
        RLRQ,
        ExcpetionResponse,
        ConfirmedServiceError,
        UNKNOWN,
    }

    public enum ALMessageType : byte
    {
        Encode,
        Decode,
        PacketDrop,
        FatalError,
    }

    public enum DataStatus : byte
    {
        Read,
        Write
    }

    public enum LogType : byte
    {
        ProcessInfoLog,
        ErrorLog,
        IOLog
    }
}
