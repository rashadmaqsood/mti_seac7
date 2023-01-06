using DatabaseManager.Database;
using SharedCode.Comm.DataContainer;
using SharedCode.Comm.HelperClasses;
using SharedCode.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Communicator.MTI_MDC
{
    public class DataProcessingLogger : IDisposable
    {
        #region Data Members

        FileStream Log_File;
        StreamWriter Log_StreamWriter;
        FileStream Err_File;
        StreamWriter Err_StreamWriter;
        string Directory_Obj;
        string Log_FullPath;
        string Err_FullPath;
        public string FileName;
        private List<struct_DataProcessingLogger> DataProcessLogging_List;
        private ArrayList ErrorList;

        public struct struct_DataProcessingLogger
        {
            public string Lable;
            public DateTime DateTime;
            public string Detail;
            public bool ContainsException;
        }

        public string IP_Port;
        public bool isInitialized = false;
        private bool isSaveToDb;
        private DatabaseController _DB_Controller;
        public bool IsException;

        public bool IsSaveToDb
        {
            get { return isSaveToDb; }
            set { isSaveToDb = value; }
        }

        public int ErrorListCount
        {
            get { return ErrorList.Count; }
        }

        public int LogListCount
        {
            get { return DataProcessLogging_List.Count; }
        }

        public long SendBytes { get; set; }
        public long ReceiveBytes { get; set; }

        public DatabaseController DB_Controller
        {
            get { return _DB_Controller; }
            set { _DB_Controller = value; }
        }

        #endregion

        #region Constructor
        public DataProcessingLogger()
        {
            try
            {
                DataProcessLogging_List = new List<struct_DataProcessingLogger>();
                ErrorList = ArrayList.Synchronized(new ArrayList());
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Member Functions

        #region Logs

        public void InitLogging(string fileName, IOConnection IOConn)
        {
            IP_Port = IOConn.IOStream.ToString();
            FileName = fileName;
            Directory_Obj = string.Format(@"{0}\Logs\", LocalCommon.GetApplicationConfigsDirectory());
            Log_FullPath = Directory_Obj + "DataProcess_" + FileName + ".txt";

            //              try
            //              {
            //                  struct_DataProcessingLogger struct_DPLogger = new struct_DataProcessingLogger();

            //                  #region Connection Logging
            //  #if Enable_Abstract_Log
            //                  struct_DPLogger.Lable = string.Format("{0,21} ..... {1,10}: {2}", IOConn.ConnectionTime.AddSeconds(1), "IIP", IP_Port);
            //  #endif
            //  #if !Enable_Abstract_Log
            //                  struct_DPLogger.Lable = string.Format("{0,21} ..... {1}: {2}", IOConn.ConnectionTime.AddSeconds(1), "Incoming Connection", IP_Port);
            //  #endif

            //                  #endregion

            //                  DataProcessLogging_List.Add(struct_DPLogger);
            //              }
            //              catch (Exception)
            //              {

            //                  throw;
            //              }
        }

        public void StartLogging()
        {
            try
            {
                if (!Directory.Exists(Directory_Obj))
                    Directory.CreateDirectory(Directory_Obj);
                if (!File.Exists(Log_FullPath))
                {
                    Log_File = new FileStream(Log_FullPath, FileMode.CreateNew);
                }
                else
                    Log_File = File.Open(Log_FullPath, FileMode.Append);
                Log_StreamWriter = new StreamWriter(Log_File);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InsertLog(string lable, bool DT_Flag)
        {
            try
            {
                struct_DataProcessingLogger struct_DPLogger = new struct_DataProcessingLogger();
                struct_DPLogger.Lable = lable;
                if (DT_Flag)
                    struct_DPLogger.DateTime = DateTime.Now;
                DataProcessLogging_List.Add(struct_DPLogger);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void InsertLog(string lable, DateTime DT)
        {
            try
            {
                struct_DataProcessingLogger struct_DPLogger = new struct_DataProcessingLogger();
                struct_DPLogger.Lable = lable;
                struct_DPLogger.DateTime = (DT != null) ? DT : DateTime.Now;
                DataProcessLogging_List.Add(struct_DPLogger);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void InsertLog(string lable, string detail, DateTime DT)
        {
            try
            {
                struct_DataProcessingLogger struct_DPLogger = new struct_DataProcessingLogger();
                struct_DPLogger.Lable = lable;
                struct_DPLogger.DateTime = (DT == null) ? DateTime.Now : DT;
                struct_DPLogger.Detail = detail;
                DataProcessLogging_List.Add(struct_DPLogger);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void InsertLog(string lable, string detail)
        {
            try
            {
                struct_DataProcessingLogger struct_DPLogger = new struct_DataProcessingLogger();
                struct_DPLogger.Lable = lable;
                struct_DPLogger.DateTime = DateTime.Now;
                struct_DPLogger.Detail = detail;
                DataProcessLogging_List.Add(struct_DPLogger);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void InsertLog(string lable, string detail, bool DT_Flag)
        {
            try
            {
                struct_DataProcessingLogger struct_DPLogger = new struct_DataProcessingLogger();
                struct_DPLogger.Lable = lable;
                if (DT_Flag)
                    struct_DPLogger.DateTime = DateTime.Now;
                struct_DPLogger.Detail = detail;
                DataProcessLogging_List.Add(struct_DPLogger);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void endLogging()
        {
            try
            {
                Log_StreamWriter.Close();
                Log_StreamWriter.Dispose();
                Log_File.Close();
                Log_File.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string LoadProfileData(string MSN, DateTime Session, Load_Profile Data)
        {
            try
            {
                StringBuilder StatusPrint = new StringBuilder(75);
                StatusPrint.AppendFormat(String.Format("\r\n\t\tMSN = {0}, SessionDateTime = {1}\r\nData: ", MSN, Session));
                if (Data.loadData.Count > 0)
                {
                    foreach (L_Data item in Data.loadData)
                        StatusPrint.AppendFormat(String.Format("Counter {0}, TimeStamp {1}, Channel_1 {2}, Channel_2 {3}, Channel_3 {4}, Channel_4 {5}\r\n", item.counter, item.timeStamp, item.value[0], item.value[1], item.value[2], item.value[3]));
                }
                else
                    StatusPrint.Append("None");
                return StatusPrint.ToString();
            }
            catch (Exception)
            {
                return "Error!!!";
            }
        }

        public string status(MeterStatus _status)
        {
            try
            {
                StringBuilder StatusPrint = new StringBuilder(75);
                StatusPrint.AppendFormat(String.Format("\r\nTBE1 = {0}, TBE2 = {1} \r\n Occurred Major Alarms: ", _status.TBE1, _status.TBE2));
                if (_status.OMA.Count > 0)
                {
                    foreach (int i in _status.OMA)
                        StatusPrint.AppendFormat(String.Format("{0}; ", ((MeterEvent)i)));
                }
                else
                    StatusPrint.Append("None");
                return StatusPrint.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Request(Request Request_Obj)
        {
            try
            {
                struct_DataProcessingLogger struct_DPLogger = new struct_DataProcessingLogger();
                StringBuilder RequestPrint = new StringBuilder(75);
                RequestPrint.Append("\r\nRequest for: ".PadRight(20) + Request_Obj.MSN);
                RequestPrint.Append("\r\n------------------------------------------");
                RequestPrint.Append("\r\nInstantaneous Data".PadRight(20) + Request_Obj.QuantitiesToRead.Instantaneous.ToString());
                RequestPrint.Append("\r\nCumulative Billing".PadRight(20) + Request_Obj.QuantitiesToRead.CommBilling.ToString());
                RequestPrint.Append("\r\nLoad Profile".PadRight(20) + Request_Obj.QuantitiesToRead.LoadProfile.ToString());
                RequestPrint.Append("\r\nMonthly Billing".PadRight(20) + Request_Obj.QuantitiesToRead.MonthlyBilling.ToString());
                RequestPrint.Append("\r\nEvents".PadRight(20) + Request_Obj.QuantitiesToRead.EventLog.ToString());
                // RequestPrint.Append("\r\nEvents to be read: ");
                // // if (Request_Obj.QuantitiesToRead.eventLog != null)
                // // {
                // //     foreach (var log in Request_Obj.QuantitiesToRead.EventLog)
                // //         RequestPrint.AppendFormat(String.Format("{0}; ", (MeterEvent)(log)));
                // // }
                // // else
                // //     RequestPrint.Append("None");

                RequestPrint.Append("\r\n------------------------------------------");

                struct_DPLogger.Lable = RequestPrint.ToString();
                DataProcessLogging_List.Add(struct_DPLogger);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool SaveLoggToFile()
        {
            try
            {
                StartLogging();
                StringBuilder Logging = new StringBuilder(75);

                Logging.Append(Assembly.GetExecutingAssembly().GetName().Name + " v" + Assembly.GetExecutingAssembly().GetName().Version + "\r\n");
                foreach (var item in DataProcessLogging_List)
                {

                    if (!String.IsNullOrEmpty(item.Lable))
                        Logging.Append(item.Lable);
                    if (item.DateTime != DateTime.MinValue)
                        Logging.Append("," + item.DateTime);

                    if (!String.IsNullOrEmpty(item.Detail))
                        Logging.AppendLine(item.Detail);
                    Logging.AppendLine();
                }
                Log_StreamWriter.Write(Logging.ToString());

                endLogging();
                return true;
            }
            catch (Exception)
            {
                Directory_Obj = "";
                return false;
            }
        }

        public bool SaveLoggToDB(string msn, DateTime sessionDateTime, TimeSpan connectionLifeTime)
        {
            try
            {
                MDC_Log log = new MDC_Log();
                var now = DateTime.Now;
                StringBuilder Logging = new StringBuilder(75);
                // Append MDC version in Log
                Logging.AppendLine(string.Format("{0,-21} .. {1,-8}{2,-2}", now, "VER", Assembly.GetExecutingAssembly().GetName().Name + " v" + Assembly.GetExecutingAssembly().GetName().Version));

                // Append All IP with Listening Port configured on Server
                var ips = GetMDCIpsForLog().Trim(',').Split(',');
                foreach (var item in ips)
                {
                    Logging.AppendLine(string.Format("{0,-21} .. {1,-8}{2,-2}", now, "IPE", item));
                }

                // Append Transactional Log
                foreach (var item in DataProcessLogging_List)
                {
                    if (item.DateTime != DateTime.MinValue)
                        // Logging.AppendLine(String.Format("{0,21} ..... {1}{2,2}", item.DateTime, String.IsNullOrEmpty(item.Lable) ? "--" : item.Lable.PadRight(15-item.Lable.Length), String.IsNullOrEmpty(item.Detail) ? "  " : item.Detail));
                        Logging.Append(String.Format("{0,-21} .. ", item.DateTime));
                    if (!String.IsNullOrEmpty(item.Lable))
                        Logging.Append(String.Format("{0,-8}", item.Lable));
                    if (!String.IsNullOrEmpty(item.Detail))
                        Logging.Append(String.Format("{0,-2}", item.Detail));
                    Logging.Append("\n");
                }

                log.IsException = this.IsException;
                log.ConnectionLife = connectionLifeTime;
                log.msn = msn;
                log.session_dt = sessionDateTime;
                log.log_string = Logging.ToString();
                // add 44 bytes for connection making tcp sync
                log.SentBytes = SendBytes + 44;
                log.ReceiveBytes = ReceiveBytes + 44;
                if (!log.msn.StartsWith("1"))
                    DB_Controller.insert_mdc_log(log);
                DB_Controller.insert_mdc_log_live(log);
                return true;
            }
            catch (Exception)
            {
                Directory_Obj = "";
                return false;
            }
        }

        public void ClearLogger()
        {
            try
            {
                if (DataProcessLogging_List != null)
                    DataProcessLogging_List.Clear();
                if (ErrorList != null)
                    ErrorList.Clear();

                IP_Port = null;
                FileName = String.Empty;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private string GetMDCIpsForLog()
        {
            try
            {
                var mdcPort = Communicator.Properties.Settings.Default["Port"].ToString();
                var ips = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
                StringBuilder tempIps = new StringBuilder();
                for (int i = 0; i < ips.Length; i++)
                {
                    if (ips[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6 || ips[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        tempIps.AppendFormat(",{0}:{1}", ips[i], mdcPort);
                }

                return tempIps.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion

        #region Errors
        //Adding Error to ErrorList
        public void InsertError(string error)
        {
            ErrorList.Add(String.Format("{0}, {1}", error, DateTime.Now));
        }

        //Adding Error to ErrorList
        public void InsertError(string error, DateTime SessionDateTime)
        {
            ErrorList.Add(String.Format("{0}, {1}", error, SessionDateTime));
        }
        //Error for IOConnection With MSNs
        public void SaveErrors(string MSN)
        {
            try
            {
                if (ErrorList.Count > 0)
                {
                    Directory_Obj = string.Format(@"{0}\Errors\", LocalCommon.GetApplicationConfigsDirectory());
                    if (!Directory.Exists(Directory_Obj))
                        Directory.CreateDirectory(Directory_Obj);
                    Err_FullPath = Directory_Obj + "Error_" + MSN + ".txt";
                    if (!File.Exists(Err_FullPath))
                    {
                        Err_File = new FileStream(Err_FullPath, FileMode.CreateNew);
                    }
                    else
                        Err_File = File.Open(Err_FullPath, FileMode.Append);
                    Err_StreamWriter = new StreamWriter(Err_File);
                    Err_StreamWriter.WriteLine("================================================");
                    if (!String.IsNullOrEmpty(IP_Port))
                        Err_StreamWriter.WriteLine(IP_Port);

                    Err_StreamWriter.WriteLine();
                    foreach (var item in ErrorList)
                    {
                        Err_StreamWriter.WriteLine(item);
                    }

                    Err_StreamWriter.Close();
                    Err_File.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void SaveErrorsToDB(string MSN, DateTime sessionDateTime)
        {
            try
            {
                if (ErrorList.Count > 0)
                {

                    if (ErrorList.Count > 0)
                    {
                        MDC_Log errorlog = new MDC_Log();
                        StringBuilder Logging = new StringBuilder(75);
                        foreach (var item in ErrorList)
                        {
                            Logging.AppendLine(item.ToString());

                            Logging.Append("\n");
                        }
                        Logging.Append("===============================================\n");
                        string errorString = Logging.ToString();
                        errorString.Replace('\'', ' ');
                        errorlog.msn = MSN;
                        errorlog.session_dt = sessionDateTime;
                        errorlog.log_string = Logging.ToString();
                        DB_Controller.insert_mdc_Errorlog(errorlog);
                        ErrorList.Clear();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Error for IOConnection Without MSNs
        public void SaveErrors()
        {
            try
            {
                if (ErrorList.Count > 0)
                {

                    Directory_Obj = string.Format(@"{0}\Errors\", LocalCommon.GetApplicationConfigsDirectory());
                    if (!Directory.Exists(Directory_Obj))
                        Directory.CreateDirectory(Directory_Obj);
                    Err_FullPath = Directory_Obj + "Error_without_MSNs.txt";
                    if (!File.Exists(Err_FullPath))
                    {
                        Err_File = new FileStream(Err_FullPath, FileMode.CreateNew);
                    }
                    else
                        Err_File = File.Open(Err_FullPath, FileMode.Append);
                    Err_StreamWriter = new StreamWriter(Err_File);
                    Err_StreamWriter.WriteLine("================================================");
                    if (!String.IsNullOrEmpty(IP_Port))
                        Err_StreamWriter.WriteLine("Error : " + IP_Port + "_" + DateTime.Now.ToString());
                    Err_StreamWriter.WriteLine();
                    foreach (var item in ErrorList)
                    {
                        Err_StreamWriter.WriteLine(item);
                    }

                    Err_StreamWriter.Close();
                    Err_File.Close();

                    ErrorList.Clear();
                }
            }
            catch (Exception)
            { }
        }

        #endregion

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                if (DataProcessLogging_List != null)
                {
                    #region // Clear Logging List

                    try
                    {
                        DataProcessLogging_List.Clear();
                        DataProcessLogging_List = null;
                    }
                    catch (Exception)
                    { }

                    #endregion
                    #region // Clear Error List

                    if (ErrorList != null)
                    {
                        ErrorList.Clear();
                        ErrorList = null;
                    }

                    #endregion
                }

            }
            catch (Exception) { }
        }

        #endregion

        ~DataProcessingLogger()
        {
            try
            {
                Dispose();
            }
            catch (Exception)
            {
            }
        }
    }
}
