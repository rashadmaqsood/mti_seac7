using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using comm;
using DatabaseManager.Database;
using Communicator.MTI_MDC;
using System.Collections;
using SharedCode.Comm.HelperClasses;

namespace Communicator.MeterConnManager
{
    public class Statistics : IDisposable
    {
        #region Data members

        private PhysicalConnectionType connType;
        private bool isSuccessful = false;
        private string MSN_Obj;
        private long max_Allocated_Count;
        private long max_Active_Session_Count;
        private DataProcessingLogger _DP_Logger;
        private DateTime startSessionDateTime;
        private TimeSpan duration;
        private DatabaseController _DBController;
        private List<string> List_TempLog = null;

        #endregion

        #region Properties

        public DateTime StartSessionDateTime
        {
            get { return startSessionDateTime;  }
            set { startSessionDateTime = value; }
        }
        public DatabaseController DBController
        {
            get { return _DBController; }
            set { _DBController = value; }
        }
        public string MeterSerialNumber
        {
            get { return MSN_Obj; }
            set { MSN_Obj = value; }
        }
        public long Max_Allocated_Count
        {
            get { return max_Allocated_Count; }
            set { max_Allocated_Count = value; }
        }
        public long Max_Active_Session_Count
        {
            get { return max_Active_Session_Count; }
            set { max_Active_Session_Count = value; }
        }
        public DataProcessingLogger DP_Logger
        {
            get { return _DP_Logger; }
            set { _DP_Logger = value; }
        }
        public TimeSpan Duration
        {
            get { return duration; }
            set { duration = value; }
        }
        public bool IsSuccessful
        {
            get { return isSuccessful; }
            set { isSuccessful = value; }
        }
        public PhysicalConnectionType ConnectionType
        {
            get { return connType; }
            set { connType = value; }
        }
        #endregion

        #region Constructor
        public Statistics()
        {

            StartSessionDateTime = DateTime.Now;
            DP_Logger = new DataProcessingLogger();
            List_TempLog = new List<string>();
            connType = PhysicalConnectionType.NonKeepAlive;
            duration = TimeSpan.Zero;
            IsSuccessful = false;
            MeterSerialNumber = null;
        }
        #endregion

        #region Member Functions

        public void SaveStatictics()
        {
            try
            {
                DBController.DBConnect.OpenConnection();
                ///double tempDuration = duration.Hours * 3600 + duration.Minutes * 60 + duration.Seconds + (duration.Milliseconds / 1000.0);
                if (!_DBController.insert_Statistics(MeterSerialNumber, (byte)connType, StartSessionDateTime,
                        Max_Allocated_Count, Max_Active_Session_Count, Duration, IsSuccessful))
                    InsertLog("statistics not saved successfully");
            }
            catch (Exception)
            {
                //Program.WriteLine("Saving Statistics failed");
                throw new Exception("Saving Statistics failed");
            }
            finally
            {
                DBController.DBConnect.CloseConnection();
            }
        }

        public void SaveLogging(TimeSpan connectionlife)
        {
            try
            {
                DBController.DBConnect.OpenConnection();
                if (DP_Logger.IsSaveToDb)
                {
                    DP_Logger.DB_Controller = DBController;

                    if (!String.IsNullOrEmpty(MeterSerialNumber))
                    {
                        DP_Logger.SaveLoggToDB(MeterSerialNumber, StartSessionDateTime, connectionlife);
                    }
                }
                else
                {
                    if (DP_Logger.isInitialized)
                        DP_Logger.SaveLoggToFile();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Saving Log failed", ex);
            }
            finally
            {
                DBController.DBConnect.CloseConnection();
            }
        }

        public void SaveErrors()
        {
            try
            {
                DBController.DBConnect.OpenConnection();
                if (DP_Logger.IsSaveToDb)
                {
                    DP_Logger.DB_Controller = DBController;

                    if (!String.IsNullOrEmpty(MeterSerialNumber))
                    {
                        DP_Logger.SaveErrorsToDB(MeterSerialNumber, StartSessionDateTime);
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(MeterSerialNumber))
                        DP_Logger.SaveErrors(MeterSerialNumber);
                    else
                        DP_Logger.SaveErrors();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Saving Errors failed", ex);
            }
            finally
            {
                DBController.DBConnect.CloseConnection();
            }
        }

        public void clearStatistics()
        {
            if (List_TempLog != null)
                List_TempLog.Clear();
            MeterSerialNumber = String.Empty;
            IsSuccessful = false;
            startSessionDateTime = DateTime.MinValue;
            duration = TimeSpan.MinValue;
            DP_Logger.IsException = false;
            DP_Logger.ClearLogger();//Clear DataProcessing_Logger

        }
        public void InsertLog_Temp(string tempLog)
        {
            try
            {
                List_TempLog.Add(tempLog + "_" + DateTime.Now.ToString());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void TempLogToActualLog()
        {
            foreach (var item in List_TempLog)
            {
                DP_Logger.InsertLog(item, false);
            }
        }

        public void InitLogging(string FileName, IOConnection IP_Port)
        {
            DP_Logger.InitLogging(FileName, IP_Port);
            DP_Logger.isInitialized = true;
            TempLogToActualLog();
        }

        public void InsertLog(string Log)
        {
            try
            {
                DP_Logger.InsertLog(Log, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertLog(string Log, string Detail)
        {
            try
            {
                DP_Logger.InsertLog(Log, Detail,true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertLog(string Log, bool IsEx)
        {
            try
            {
                DP_Logger.IsException = IsEx;
                DP_Logger.InsertLog(Log,true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertLog(long SentBytes, long ReceiveBytes)
        {
            try
            {
                DP_Logger.SendBytes = SentBytes;
                DP_Logger.ReceiveBytes = ReceiveBytes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertError(string Error)
        {
            try
            {
                DP_Logger.InsertError(Error);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertError(Exception Info, DateTime SessionDateTime, int InnerExceptionMessagePrintLevel = 4)
        {
            //saveErrors(ex, IOConn);
            try
            {
                StringBuilder InnerMessages = new StringBuilder(255);
                Exception _ex = Info;
                InnerMessages.AppendLine(Info.Message);
                for (int count = 1; count <= InnerExceptionMessagePrintLevel; count++)
                {
                    if (_ex.InnerException != null)
                        _ex = _ex.InnerException;
                    else
                        break;
                    InnerMessages.AppendFormat("{0}\r\n", _ex.Message);
                }
                //Common.WriteError(error);
                DP_Logger.InsertError(InnerMessages.ToString(), SessionDateTime);
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
                if (_DP_Logger != null)
                {
                    _DP_Logger.Dispose();
                    _DP_Logger = null;
                }
                _DBController = null;
                if (List_TempLog != null)
                {
                    List_TempLog.Clear();
                    List_TempLog = null;
                }
            }
            catch (Exception) { }
        }

        #endregion
    }
}
