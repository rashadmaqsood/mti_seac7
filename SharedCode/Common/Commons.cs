#define Enable_DEBUG_ECHO
#define Enable_Error_Logging
using DLMS;
using DLMS.Comm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using SharedCode.Comm.HelperClasses;
using SharedCode.Properties;
using comm;
using LogSystem.Shared.Common.Enums;
using LogSystem.Shared.Common;
using System.Runtime.InteropServices;
using SharedCode.Comm.DataContainer;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace SharedCode.Common
{
    public enum HeartBeatType
    {
        MTI             = 0,
        SHENZHEN_CLOU   = 1,
    }
    public static class Commons
    {
        public static readonly string Key_ConStr = @"`13fg`1234254h32kl5hj67j8hj0h9kj-h80jkh=-0-jhvjhxc32vb1sd65f49aw8e7r98we7r98w4tg56bn56165z4d564aw89r7et987eryh89s4bh56c41v65af8awr89ty7et98u7ouop98;4.56g4h1m6s54we6a54qwerzqwe5qw4r65qw4e89q7ery98tj4564kio89p7654jb6nm465v4b65zc46s48q89we7tw89r7y9i89tyi4p65io1;3][4[56/1.4,v564hn6sd5f4ga654sdf6a84W98ER798WE7RY98ER7TU98T4J6544s65dv4sd32b1df6gj4f89iup78tu9f4ywsr4a6QWE4f65d4sv63541vb65Xg54dqwe65t4et6u4ouiplui5g4k,2j2m1hnm2.0cv23b1sd6f5gh3a2s1df65w4et65er1h4nhc6m4ft8u7k/7uyk8t4u1d23fn0gjhn5dty89jndtn10f35ry93dhsh1";
        //public static readonly HeartBeatType HeartBeatCompany = (HeartBeatType)Settings.Default.HeartBeatType; // HeartBeatType.SHENZHEN_CLOU;

        public static readonly TimeSpan ReadLOCKLow_TimeOut = TimeSpan.FromSeconds(05);
        public static readonly TimeSpan ReadLOCK_TimeOut = TimeSpan.FromSeconds(25);
        public static readonly TimeSpan WriteLOCK_TimeOut = TimeSpan.FromSeconds(45);
        public static readonly TimeSpan WriteLOCKLow_TimeOut = TimeSpan.FromSeconds(05);

        public static readonly float KiloByteDivider = 1024.0f;
        public static readonly float MegaByteDivider = 1048576.0f;
        public static readonly float GigaByteDivider = 1073741824.0f;
        public static bool IsMDC => Application_Type == ApplicationType.MDC;
        public static bool IsQc => Application_Type == ApplicationType.QC;
        public static bool IsSeac => Application_Type == ApplicationType.SEAC;

        public static ApplicationType Application_Type;

        public static Dictionary<UInt32, UInt32> MaskValues = new Dictionary<UInt32, UInt32>();

        public const int IOBufferLength = 1024;

        public static string Default_Meter = "R326";

        private static ConcurrentDictionary<string, object> singletonInstaces = null;
        public static string DateTimeFormat = "dd/MM/yyyy HH:mm:ss";

        #region PreciseDelay

        /// <summary>
        /// Make Thread Delay Duration Of Provided Delay
        /// It has significant Performance Penalty
        /// </summary>
        /// <param name="durationTicks">Milli-Second Thread Duration</param>
        public static void PreciseDelayUntil(Func<bool> Criteria, long durationTicks)
        {
            // Static method to initialize 
            // and start stopwatch
            var sw = Stopwatch.StartNew();

            while (sw.ElapsedMilliseconds < durationTicks
                   && !Criteria.Invoke())
            {
                // Wait For Thread 
                System.Threading.Thread.SpinWait(05);
                // System.Threading.Thread.Sleep(10);
            }
        }

        /// <summary>
        /// Make Thread Delay Duration Of Provided Delay
        /// It has significant Performance Penalty
        /// </summary>
        /// <param name="durationTicks">Milli-Second Thread Duration</param>
        public static void PreciseDelay(long durationTicks)
        {
            // Static method to initialize 
            // and start stopwatch
            var sw = Stopwatch.StartNew();

            while (sw.ElapsedMilliseconds < durationTicks)
            {
                // Wait For Thread 
                System.Threading.Thread.SpinWait(05);
                // System.Threading.Thread.Sleep(10);
            }
        }

        /// <summary>
        /// Make Thread Delay Duration Of Provided Delay
        /// It has significant Performance Penalty
        /// </summary>
        /// <param name="durationTicks">Milli-Second Thread Duration</param>
        public static void PreciseDelay(double durationTicks)
        {
            // Static method to initialize 
            // and start stopwatch
            var sw = Stopwatch.StartNew();

            while (sw.ElapsedMilliseconds < durationTicks)
            {
                // Wait For Thread 
                System.Threading.Thread.SpinWait(05);
                // System.Threading.Thread.Sleep(10);
            }
        }

        /// <summary>
        /// Make Thread Delay Duration Of Provided Delay
        /// It has significant Performance Penalty
        /// </summary>
        /// <param name="duration">Thread Duration Delay</param>
        public static void PreciseDelay(TimeSpan duration)
        {
            // Static method to initialize 
            // and start stopwatch
            Stopwatch sw;
            sw = Stopwatch.StartNew();

            while (sw.ElapsedMilliseconds < duration.TotalMilliseconds)
            {
                // Wait For Thread 
                System.Threading.Thread.SpinWait(05);
                // System.Threading.Thread.Sleep(10);
            }
        }

        #endregion

        #region Delay

        /// <summary>
        /// Make Thread Delay Duration Of Provided Delay
        /// It has significant Performance Penalty
        /// </summary>
        /// <param name="millisecdelay">Milli-Second Thread Duration</param>
        public static void DelayUntil(Func<bool> Criteria, long millisecdelay)
        {
            // Static method to initialize 
            // and start stopwatch
            var sw = Stopwatch.StartNew();

            while (sw.ElapsedMilliseconds < millisecdelay
                   && !Criteria.Invoke())
            {
                // Wait For Thread 
                // System.Threading.Thread.SpinWait(05);
                System.Threading.Thread.Sleep(250);
            }
        }


        /// <summary>
        /// Make Thread Delay Duration Of Provided Delay
        /// </summary>
        /// <param name="millisecdelay">Milli-Second Thread Duration</param>
        public static void Delay(long millisecdelay)
        {
            // Static method to initialize 
            // and TimeStamp
            TimeSpan currentTimeStamp = DateTime.Now.TimeOfDay;

            do
            {
                // Wait For Thread 
                // System.Threading.Thread.SpinWait(10000);
                System.Threading.Thread.Sleep(250);
            }
            while (DateTime.Now.TimeOfDay.Subtract(currentTimeStamp).
                TotalMilliseconds < millisecdelay);
        }

        /// <summary>
        /// Make Thread Delay Duration Of Provided Delay
        /// </summary>
        /// <param name="millisecdelay">Milli-Second Thread Duration</param>
        public static void Delay(double millisecdelay)
        {
            // Static method to initialize 
            // and TimeStamp
            TimeSpan currentTimeStamp = DateTime.Now.TimeOfDay;

            do
            {
                // Wait For Thread 
                // System.Threading.Thread.SpinWait(10000);
                System.Threading.Thread.Sleep(50);
            }
            while (DateTime.Now.TimeOfDay.Subtract(currentTimeStamp).
                   TotalMilliseconds < millisecdelay);
        }

        /// <summary>
        /// Make Thread Delay Duration Of Provided Delay
        /// </summary>
        /// <param name="duration">Thread Duration Delay</param>
        public static void Delay(TimeSpan duration)
        {
            // Static method to initialize 
            // and TimeStamp
            TimeSpan currentTimeStamp = DateTime.Now.TimeOfDay;

            do
            {
                // Wait For Thread 
                // System.Threading.Thread.SpinWait(10000);
                System.Threading.Thread.Sleep(50);
            }
            while (DateTime.Now.TimeOfDay.Subtract(currentTimeStamp).
                TotalMilliseconds < duration.TotalMilliseconds);
        }

        #endregion

        #region Console Position
        const int SWP_NOSIZE = 0x0001;

        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetConsoleWindow();

        public static string ProductName = "";
        public static string ProductVersion = "";
        #region Console Position and size
        public static void ChangeConsolePositionAndSize()
        {
            try
            {
                if (ChangeConsoleResolution)
                {
                    AllocConsole();
                    IntPtr myConsole = GetConsoleWindow();
                    const int xpos = 0;
                    const int ypos = 18;
                    SetWindowPos(myConsole, 0, xpos, ypos, 0, 0, SWP_NOSIZE);
                    Console.Title = String.Format("{0} {1}", ProductName, ProductVersion);
                    Console.WindowLeft = 0;
                    //Change the Console appearance and redisplay
                    Console.SetWindowSize(99, 52);
                    Console.BufferHeight = 440;
                    Console.BufferWidth = 470;
                    Console.CursorVisible = false;
                    ChangeConsoleResolution = false;
                }
            }
            catch
            { }
        }
        #endregion
        #endregion

        #region Console Hide Show

        public static bool EnableEcho = true;
        public static bool ChangeConsoleResolution = true;
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        static bool HideConsole;
        public static string AppName = "";
        public static string AppVersion = "";
        public static DateTime AppLastBuild;
        public static string ConsoleTitle = "Smart Eye MDC Server";
        public static void HideConsoleWindow()
        {
            try
            {
                Console.Title = ConsoleTitle;
                if (HideConsole)
                {
                    ConsoleTitle = String.Format("{0} v({1}), Build Date: {2}", AppName, AppVersion, AppLastBuild);
                }
                HideConsole = true;
                System.Threading.Thread.Sleep(100);
                IntPtr h = FindWindow(null, ConsoleTitle);
                ShowWindow(h, 0); // 0 = hide
            }
            catch
            { }

        }
        public static void ShowConsoleWindow()
        {
            try
            {
                Console.Title = ConsoleTitle;
                IntPtr h = FindWindow(null, ConsoleTitle);
                ConsoleTitle = String.Format("{0} v({1}), Build Date: {2}", AppName, AppVersion, AppLastBuild);
                ChangeConsolePositionAndSize();
                ShowWindow(h, 1); // 1 = show
            }
            catch
            { }

        }

        #endregion

        #region Out_Put_Class

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static FileStream fs = null;
        public static StreamWriter wr = null;
        public static BufferedStream _ConsoleStream = null;
        public static readonly int ExceptionMaxLevel = 6;
        public static readonly string LogSourceName = "MySource";
        /// <summary>
        /// Static Constructor
        /// </summary>
        static Commons()
        {
            Stream stdOut = Console.OpenStandardOutput();
            _ConsoleStream = new BufferedStream(stdOut, 2048);

            if (singletonInstaces == null)
                singletonInstaces = new ConcurrentDictionary<string, object>();
        }

        #region Event_Logger

        public static EventLog GetEventLogger()
        {
            try
            {
                if (!EventLog.SourceExists(LogSourceName))
                {
                    //An event log source should not be created and immediately used. 
                    //There is a latency time to enable the source, it should be created 
                    //prior to executing the application that uses the source. 
                    //Execute this sample a second time to use the new source.
                    EventLog.CreateEventSource(LogSourceName, "Application");
                    // The source is created.  Exit the application to allow it to be registered. 
                }
                EventLog _logger = new EventLog("MTI_MDC_EventLog", ".", LogSourceName);
                return _logger;
            }
            catch (Exception ex)
            {

                throw new Exception("Error occurred while Getting Event Logger", ex);
            }
        }

        public static void LogApplicationException(Exception ex, int curLevel = 10)
        {
            try
            {
                StringBuilder builder = BuildExceptionLog(ex, curLevel);
                EventLog systemEventLogger = GetEventLogger();
                systemEventLogger.WriteEntry(builder.ToString(), EventLogEntryType.FailureAudit);
            }
            catch (Exception) { }
        }

        public static List<MeterSerialNumber> ConvertSTRToMSNList(string _LogsMeterIdsFilter)
        {
            List<MeterSerialNumber> _MSNFilter = null;
            bool isValueProcessed = false;

            try
            {
                if (string.IsNullOrEmpty(_LogsMeterIdsFilter))
                    throw new ArgumentException("Logs MSN Filter STR");

                string split_Chars = @",'""/\|<>.%#";

                try
                {
                    string[] msnSTR_Splits = _LogsMeterIdsFilter.Split(split_Chars.ToCharArray());
                    _MSNFilter = new List<MeterSerialNumber>(msnSTR_Splits.Length);

                    foreach (var tk in msnSTR_Splits)
                    {
                        if (string.IsNullOrEmpty(tk))
                            continue;
                        else
                        {
                            MeterSerialNumber SerialNumber = null;
                            try
                            {
                                SerialNumber = MeterSerialNumber.ConvertFrom(tk);
                            }
                            catch
                            {
                                SerialNumber = null;
                            }

                            if (SerialNumber == null || !SerialNumber.IsMSN_Valid)
                                continue;

                            _MSNFilter.Add(SerialNumber);
                        }
                    }

                    // IsvalueProcessed
                    isValueProcessed = _MSNFilter != null && _MSNFilter.Count > 0;
                }
                catch
                {
                    isValueProcessed = false;
                }

                // Validate Value Read from Configurations
                if (_LogsMeterIdsFilter == null || !isValueProcessed)
                {
                    _MSNFilter = null;
                    // throw new Exception("Error occurred while Loading/Save Logger Port Address Directory Configuration");
                }

                return _MSNFilter;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Processing MSN List STR", ex);
            }
        }

        public static string ConvertMSNListToSTR(List<MeterSerialNumber> SRList)
        {
            try
            {
                StringBuilder msnList = new StringBuilder();
                string MSN_STR = "";

                foreach (var msn in SRList)
                {
                    if (msn == null || !msn.IsMSN_Valid)
                        continue;
                    MSN_STR = msn.ToString();
                    msnList.AppendFormat("{0},", MSN_STR);
                }

                return msnList.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Processing MSN List", ex);
            }
        }

        #endregion

        #region Save Error Exceptions

        public static StreamWriter ExceptionsInit(string relativeDirPath, string fileName)
        {
            try
            {
                FileStream fs = null;
                StreamWriter wr = null;

                string dir = relativeDirPath;
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                string path = dir + String.Format("\\{0}", fileName);
                if (!File.Exists(path))
                    fs = new FileStream(path, FileMode.CreateNew);
                else
                    fs = File.Open(path, FileMode.Append);
                wr = new StreamWriter(fs);
                return wr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static StringBuilder BuildExceptionLog(Exception ex, int curLevel = 14)
        {
            StringBuilder builder = new StringBuilder(512);

            builder.AppendFormat("Unhandled Error{0} {1},details\r\n", DateTime.Now, ex.Message);
            Exception _ex = ex;

            while (_ex != null && curLevel < ExceptionMaxLevel)
            {
                _ex = _ex.InnerException;
                if (_ex == null)
                    break;
                builder.AppendFormat("{0}\r\n", _ex.Message);
                curLevel++;
            }
            if (ex != null)
                builder.AppendLine(ex.StackTrace);
            return builder;
        }



        public static void BackUpFileSameLoc(string fileUrl)
        {
            try
            {
                FileInfo FileINfo = new FileInfo(fileUrl);
                if (FileINfo.Exists)
                {
                    String NewFileName = FileINfo.Name.Replace(FileINfo.Extension, "");
                    NewFileName = String.Format("{0}_{1}_{2}", NewFileName, DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString());
                    ///Process New File Name
                    NewFileName = NewFileName.Replace(" ", "");
                    NewFileName = NewFileName.Replace(".", "");
                    NewFileName = NewFileName.Replace("/", "");
                    NewFileName = NewFileName.Replace(":", "");
                    NewFileName = String.Format(@"{0}\{1}{2}", FileINfo.Directory, NewFileName, FileINfo.Extension);
                    File.Move(fileUrl, NewFileName);
                    File.Delete(fileUrl);
                }
            }
            catch
            {
            }
        }

        public static void SaveApplicationLogMessage(StringBuilder BuilderMsg, string fileURL)
        {

            try
            {
                FileInfo FileINfo = new FileInfo(fileURL);
                ///File Lenght Exceeds 10K 
                if (FileINfo.Exists && FileINfo.Length > 10240)
                    BackUpFileSameLoc(fileURL);

                string fileName = FileINfo.Name;
                string dirName = FileINfo.DirectoryName;

                using (StreamWriter wr = ExceptionsInit(dirName, fileName))
                {
                    wr.WriteLine(BuilderMsg);
                }
            }
            catch (Exception) { }
        }

        #endregion

        public static void WriteLine(ILogWriter logger, string text)
        {
            // Write(text + "\r\n---------\t\t-----------\t\t\t--------\r\n");
            // Write(text + "\r\n\r\n");
            if (logger != null)
                logger.WriteWarning(text, LogDestinations.Console | LogDestinations.TextFile | LogDestinations.UDP);
        }

        public static void WriteError(ILogWriter logger, string text)
        {
            try
            {
                if (logger != null)
                    logger.WriteError("\t" + text + "\r\n-------\t\t\t--------\t\t\t--------\r\n");

                // Console.ForegroundColor = ConsoleColor.Red;
                // Console.Out.WriteLine("\t" + text + "\r\n-------\t\t\t--------\t\t\t--------\r\n");
                // Console.ForegroundColor = ConsoleColor.White;
            }
            catch (Exception)
            { }
        }

        public static void WriteSuccess(ILogWriter logger, string text)
        {
            try
            {
                // Console.ForegroundColor = ConsoleColor.Green;
                if (logger != null)
                    logger.WriteError("\t" + text + "\r\n-------\t\t\t--------\t\t\t--------\r\n");
                // Console.Out.WriteLine("\t" + text + "\r\n-------\t\t\t--------\t\t\t--------\r\n");
                // Console.ForegroundColor = ConsoleColor.White;
            }
            catch (Exception)
            { }
        }

        public static void WriteAlert(ILogWriter logger, string text)
        {
            try
            {
                if (logger != null)
                    logger.WriteError("\t" + text + "\r\n-------\t\t\t--------\t\t\t--------\r\n");

                // Console.ForegroundColor = ConsoleColor.DarkCyan;
                // Console.Out.WriteLine("\t" + text + "\r\n-------\t\t\t--------\t\t\t--------\r\n");
                // Console.ForegroundColor = ConsoleColor.White;
            }
            catch (Exception)
            { }
        }


        #endregion

        public static byte[] GetRandomOctectString(int DataStringLength = 08)
        {
            Random random = new Random((int)DateTime.Now.Ticks);


            byte[] Challenge_STR = new byte[DataStringLength];

            for (int index = 0; index < Challenge_STR.Length; index++)
            {
                Challenge_STR[index] = Convert.ToByte(random.Next(0, 255));
            }


            return Challenge_STR;
        }

        public static string DecimalPoint_toGUI(byte value)
        {
            int upper = value / 16;
            int lower = value % 16;
            string temp = "";
            temp = temp.PadLeft(upper, '0') + "." + temp.PadRight(lower, '0');
            //temp =;
            return temp;
        }

        /// <summary>
        /// Removes blank spaces and new line chars from the string
        /// </summary>
        /// <param name="passed"></param>
        /// <returns></returns>
        private static string Remove_White_Spaces(string passed)
        {
            passed = passed.Replace(" ", "");
            passed = passed.Replace("\x0d", "");
            passed = passed.Replace("\x0a", "");

            return passed;
        }

        //==========================================================================
        //==========================================================================
        /// <summary>
        /// Converts byte array to string in hex format
        /// </summary>
        /// <param name="Hex_Array"></param>
        /// <returns></returns>
        static public string ArrayToHexString(byte[] Hex_Array)
        {
            StringBuilder Return_String = new StringBuilder();
            foreach (byte Value in Hex_Array) Return_String.AppendFormat("{0:X2} ", Value);
            return Return_String.ToString();
        }

        /// <summary>
        /// Converts byte array to string in hex format
        /// </summary>
        /// <param name="Hex_Array"></param>
        /// <returns></returns>
        static public string ArrayToHexString(byte[] Hex_Array, int OffSet, int Count)
        {
            StringBuilder Return_String = new StringBuilder();
            byte Value = 0;
            for (int index = OffSet; OffSet < (OffSet + Count); index++)
            {
                Value = Hex_Array[index];
                Return_String.AppendFormat("{0:X2} ", Value);
            }
            return Return_String.ToString();
        }


        //==========================================================================
        //==========================================================================
        public static byte[] String_to_Hex_array(string passed)
        {
            passed = Remove_White_Spaces(passed);

            passed = passed.ToUpper();

            char[] temp = passed.ToCharArray();

            byte[] to_return = new byte[(temp.Length) / 2];

            for (byte i = 0; i < to_return.Length; i++)
            {
                if (temp[2 * i] >= '0' && temp[2 * i] <= '9') to_return[i] |= (byte)((temp[2 * i] - 0x30) << 4);
                else if (temp[2 * i] >= 'A' && temp[2 * i] <= 'F') to_return[i] |= (byte)((temp[2 * i] - 0x37) << 4);

                if (temp[(2 * i) + 1] >= '0' && temp[(2 * i) + 1] <= '9') to_return[i] |= (byte)(temp[(2 * i) + 1] - 0x30);
                else if (temp[(2 * i) + 1] >= 'A' && temp[(2 * i) + 1] <= 'F') to_return[i] |= (byte)(temp[(2 * i) + 1] - 0x37);
            }


            return to_return;
        }
        //==========================================================================

        public static char[] HexStringToBinary(string hexString, int bitStringLength)
        {
            //long longValue = Convert.ToInt64(hexString, 16);
            //string binRepresentation = Convert.ToString(longValue,2);

            string binRepresentation = String.Join(String.Empty,
            hexString.Select(
              c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
            )
          );

            while (binRepresentation.Length < bitStringLength)
            {
                binRepresentation = String.Concat("0", binRepresentation);
            }
            return binRepresentation.ToCharArray();
        }
        public static string BinaryToHexString(string binary, int hexLength)
        {
            if (binary.Length > hexLength * 8) throw new ArgumentOutOfRangeException("Invalid Hex Length");

            StringBuilder result = new StringBuilder(hexLength);
            // pad to length multiple of 8
            binary = binary.PadRight(hexLength * 8, '0');

            for (int i = 0; i < binary.Length; i += 8)
            {
                string eightBits = binary.Substring(i, 8);
                result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }

            return result.ToString();
        }
        //======================================================
        public static BitArray GetSubscripotionArray(string subscription)
        {
            if (string.IsNullOrEmpty(subscription) || string.IsNullOrWhiteSpace(subscription))
                throw new Exception("Invalid Subscription value to convert in BitArray");
            return new BitArray(Enumerable.Range(0, subscription.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(subscription.Substring(x, 2), 16)).ToArray());
        }
        //==========================================================================
        #region Array Functions

        public static void Append_to_Start(ref byte[] First_Array, ref int offSet_Dest, ref int size_Dest,
                                           byte[] SecondArray, int offSet_Src, int size_Src)
        {
            try
            {
                if (First_Array.Length >= (offSet_Dest + size_Dest + size_Src))
                {
                    ///Append room on Start
                    if (offSet_Dest >= size_Src)
                    {
                        Buffer.BlockCopy(SecondArray, offSet_Src, First_Array, (offSet_Dest - size_Src), size_Src);
                        offSet_Dest = (offSet_Dest - size_Src);
                    }
                    else
                    {
                        ///Shift Elements
                        for (int index = offSet_Dest + size_Dest; index >= 0 && index >= offSet_Dest; index--)
                            First_Array[index + size_Src] = First_Array[index];
                        ///Copy Second Array
                        Buffer.BlockCopy(SecondArray, offSet_Src, First_Array, offSet_Dest, size_Src);
                    }
                    size_Dest = size_Dest + size_Src;
                }
                else
                {
                    byte[] tLargeBuf = new byte[size_Dest + size_Src];
                    Buffer.BlockCopy(tLargeBuf, 0, SecondArray, 0, size_Src);
                    Buffer.BlockCopy(tLargeBuf, size_Src, First_Array, offSet_Dest, size_Dest);
                    First_Array = tLargeBuf;
                    offSet_Dest = 0;
                    size_Dest = tLargeBuf.Length;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while coping data on destination", ex);
            }
        }

        static public byte[] Append_to_End(byte[] First_Array, byte[] Second_Array)
        {
            byte[] Appended = new byte[First_Array.Length + Second_Array.Length];

            System.Buffer.BlockCopy(First_Array, 0, Appended, 0, First_Array.Length);
            System.Buffer.BlockCopy(Second_Array, 0, Appended, First_Array.Length, Second_Array.Length);

            return Appended;
        }
        //==========================================================================

        //==========================================================================
        /// <summary>
        /// Appends second_byt at the end of first array
        /// </summary>
        /// <param name="First_Array"></param>
        /// <param name="secondByte"></param>
        /// <returns></returns>
        static public byte[] Append_to_End(byte[] First_Array, byte secondByte)
        {
            var secondArray = new byte[1] { secondByte };
            var appended = new byte[First_Array.Length + secondArray.Length];

            Buffer.BlockCopy(First_Array, 0, appended, 0, First_Array.Length);
            Buffer.BlockCopy(secondArray, 0, appended, First_Array.Length, secondArray.Length);

            return appended;
        }
        //==========================================================================

        //==========================================================================
        /// <summary>
        /// Appends second_long at the end of First_Array
        /// </summary>
        /// <param name="First_Array"></param>
        /// <param name="second_long"></param>
        /// <returns></returns>
        static public byte[] Append_to_End(byte[] First_Array, ulong second_long)
        {
            byte[] Second_Array = new byte[4]
            { (byte)((ulong)second_long >> 24 & 0xff), (byte)((ulong)second_long >> 16 & 0xff),
             (byte)((ulong)second_long>>8 & 0xff), (byte)((ulong)second_long & 0xff) };
            byte[] Appended = new byte[First_Array.Length + Second_Array.Length];

            System.Buffer.BlockCopy(First_Array, 0, Appended, 0, First_Array.Length);
            System.Buffer.BlockCopy(Second_Array, 0, Appended, First_Array.Length, Second_Array.Length);

            return Appended;
        }
        //==========================================================================

        //==========================================================================
        /// <summary>
        /// Takes two bytes and appends them in a byte array
        /// </summary>
        /// <param name="firstByte"></param>
        /// <param name="secondByte"></param>
        /// <returns></returns>
        static public byte[] Append_to_End(byte firstByte, byte secondByte)
        {
            var firstArray = new byte[1] { firstByte };
            var secondArray = new byte[1] { secondByte };
            var appended = new byte[firstArray.Length + secondArray.Length];

            System.Buffer.BlockCopy(firstArray, 0, appended, 0, firstArray.Length);
            System.Buffer.BlockCopy(secondArray, 0, appended, firstArray.Length, secondArray.Length);

            return appended;
        }
        //==========================================================================

        //==========================================================================
        /// <summary>
        /// Appends second_short at the end of First_Array
        /// </summary>
        /// <param name="First_Array"></param>
        /// <param name="second_short"></param>
        /// <returns></returns>
        static public byte[] Append_to_End(byte[] First_Array, UInt16 second_short)
        {
            var Second_Array = new byte[2] { (byte)((short)second_short >> 8 & 0xff), (byte)((short)second_short & 0xff) };

            var Appended = new byte[First_Array.Length + Second_Array.Length];

            Buffer.BlockCopy(First_Array, 0, Appended, 0, First_Array.Length);
            Buffer.BlockCopy(Second_Array, 0, Appended, First_Array.Length, Second_Array.Length);

            return Appended;
        }

        //==========================================================================
        /// <summary>
        /// Appends second array at the start of first array
        /// </summary>
        /// <param name="First_Array"></param>
        /// <param name="Second_Array"></param>
        /// <returns></returns>
        static public byte[] Append_to_Start(byte[] First_Array, byte[] Second_Array)
        {
            byte[] Appended = new byte[First_Array.Length + Second_Array.Length];

            System.Buffer.BlockCopy(Second_Array, 0, Appended, 0, Second_Array.Length);
            System.Buffer.BlockCopy(First_Array, 0, Appended, Second_Array.Length, First_Array.Length);

            return Appended;
        }
        //==========================================================================

        //==========================================================================
        static public byte[] Append_to_Start(byte[] First_Array, byte second_byte)
        {
            byte[] Second_Array = new byte[1] { second_byte };
            byte[] Appended = new byte[First_Array.Length + Second_Array.Length];

            System.Buffer.BlockCopy(Second_Array, 0, Appended, 0, Second_Array.Length);
            System.Buffer.BlockCopy(First_Array, 0, Appended, Second_Array.Length, First_Array.Length);

            return Appended;
        }
        //==========================================================================

        //==========================================================================
        static public byte[] Append_to_Start(byte[] First_Array, UInt16 second_short)
        {
            byte[] Second_Array = new byte[2] { (byte)((short)second_short >> 8 & 0xff), (byte)((short)second_short & 0xff) };

            byte[] Appended = new byte[First_Array.Length + Second_Array.Length];

            System.Buffer.BlockCopy(Second_Array, 0, Appended, 0, Second_Array.Length);
            System.Buffer.BlockCopy(First_Array, 0, Appended, Second_Array.Length, First_Array.Length);

            return Appended;
        }
        #endregion

        public static string value_to_string(double value, bool Format)
        {
            if (double.IsNaN(value))
                return "Not Access";
            else if (double.IsPositiveInfinity(value))
                return "Not Exist";
            else if (double.IsNegativeInfinity(value))
                return "Meter BUG";
            else return value.ToString("f3");
        }
        public static string value_to_string(double value)
        {
            if (double.IsNaN(value))
                return "Not Access";
            else if (double.IsPositiveInfinity(value))
                return "Not Exist";
            else if (double.IsNegativeInfinity(value))
                return "Meter BUG";
            else return value.ToString("");
        }


        // furqan code 11/20/2014
        public static byte[] BitArrayToByteArray(BitArray bitArr)
        {
            int numBytes = bitArr.Count / 8;
            if (bitArr.Count % 8 != 0) numBytes++;

            byte[] bytes = new byte[numBytes];
            int byteIndex = 0, bitIndex = 0;

            for (int i = 0; i < bitArr.Count; i++)
            {
                if (bitArr[i])
                    bytes[byteIndex] |= (byte)(1 << (7 - bitIndex));

                bitIndex++;
                if (bitIndex == 8)
                {
                    bitIndex = 0;
                    byteIndex++;
                }
            }

            return bytes;
        }

        public static byte DecimalPoint_validation(string value, int left, int right, int total)
        {
            try
            {
                try
                {
                    double val = Convert.ToDouble(value);
                }
                catch
                {
                    return 0;
                }

                int before_decimal;
                int after_decimal;
                before_decimal = value.IndexOf('.');
                if (before_decimal == -1)
                {
                    before_decimal = 0;
                    after_decimal = 0;
                    return 0;
                }
                else
                {
                    after_decimal = value.Length - before_decimal - 1;
                    if (before_decimal > 16 || after_decimal > 16 || value == "" ||
                        after_decimal > right || before_decimal > left || (after_decimal + before_decimal) > total)
                    {
                        return 0;
                    }
                    return (byte)(before_decimal * 16 + after_decimal);
                }
            }
            catch
            {
                return 0;
            }
        }
        //Ends

        public static bool ValidateNetworkAddress(string sourceIP, IPAddress allowedIP, UInt32 subnetMask)
        {
            try
            {
                var Source = IPAddress.Parse(sourceIP.Split(':')[0]);
                var s1 = Source.GetAddressBytes();
                var s2 = allowedIP.GetAddressBytes();
                UInt32 _IPSource = ((UInt32)(s1[3] + (s1[2] << 8) + (s1[1] << 16) + (s1[0] << 24)));
                UInt32 _IPAllowed = ((UInt32)(s2[3] + (s2[2] << 8) + (s2[1] << 16) + (s2[0] << 24)));
                UInt32 maskValue = GetNetMaskValues()[subnetMask];
                _IPSource = _IPSource | maskValue;
                _IPAllowed = _IPAllowed | maskValue;
                return (_IPSource == _IPAllowed);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static object Validate_BillData(double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                return "NULL";
            else
                return value;
        }

        public static Dictionary<UInt32, UInt32> GetNetMaskValues()
        {
            try
            {
                if (MaskValues != null && MaskValues.Count < 33)
                {
                    lock (MaskValues)
                    {
                        MaskValues.Clear();
                        MaskValues.Add(0, 4294967295);
                        MaskValues.Add(1, 2147483647);
                        MaskValues.Add(2, 1073741823);
                        MaskValues.Add(3, 536870911);
                        MaskValues.Add(4, 268435455);
                        MaskValues.Add(5, 134217727);
                        MaskValues.Add(6, 67108863);
                        MaskValues.Add(7, 33554431);
                        MaskValues.Add(8, 16777215);
                        MaskValues.Add(9, 8388607);
                        MaskValues.Add(10, 4194303);
                        MaskValues.Add(11, 2097151);
                        MaskValues.Add(12, 1048575);
                        MaskValues.Add(13, 524287);
                        MaskValues.Add(14, 262143);
                        MaskValues.Add(15, 131071);
                        MaskValues.Add(16, 65535);
                        MaskValues.Add(17, 32767);
                        MaskValues.Add(18, 16383);
                        MaskValues.Add(19, 8191);
                        MaskValues.Add(20, 4095);
                        MaskValues.Add(21, 2047);
                        MaskValues.Add(22, 1023);
                        MaskValues.Add(23, 511);
                        MaskValues.Add(24, 255);
                        MaskValues.Add(25, 127);
                        MaskValues.Add(26, 63);
                        MaskValues.Add(27, 31);
                        MaskValues.Add(28, 15);
                        MaskValues.Add(29, 7);
                        MaskValues.Add(30, 3);
                        MaskValues.Add(31, 1);
                        MaskValues.Add(32, 0);
                    }

                }
                return MaskValues;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// ORM this method is intended to surve as Object Relation Model
        /// From Data Reader to particular object
        /// </summary>
        /// <typeparam name="T">Target Type</typeparam>
        /// <param name="_reader">IDataReader Object</param>
        /// <returns>Target Type</returns>
        // Author By Muhammad Furqan Khan

        public static List<T> MapReaderToObject<T>(IDataReader _reader)
        where T : class, new()
        {
            var objType = typeof(T);
            var propList = objType.GetProperties();
            if (propList.Count() != _reader.FieldCount)
                throw new InvalidCastException("Number Of Fields Doesn't Match or Invalid Object To Map");
            var entities = new List<T>();

            while (_reader.Read())
            {
                var obj = new T();
                for (int i = 0; i < _reader.FieldCount; i++)
                {
                    if (propList[i] != null && propList[i].CanWrite)
                    {

                        if (_reader[i].GetType() != propList[i].PropertyType)
                        {
                            var converter = System.ComponentModel.TypeDescriptor.GetConverter(propList[i].PropertyType);
                            var value = converter.ConvertFrom(_reader[i].ToString());
                            propList[i].SetValue(obj, value, null);
                        }
                        else
                            propList[i].SetValue(obj, _reader[i], null);


                    }
                }
                entities.Add(obj);
            }
            return entities;
        }

        public static Security_Data DefaultSecurityData()
        {
            try
            {
                Security_Data _Security_Data = new Security_Data();

                _Security_Data.SecurityControl = (SecurityControl)(3 * 0x10);
                _Security_Data.SystemTitle = new List<byte>(DLMS_Common.PrintableStringToByteArray("MTIwaves"));
                _Security_Data.ServerSystemTitle = _Security_Data.SystemTitle;
                string ak = "C1CA4472EFE30A2668CC10A64DCCCED7";
                string key = ak.PadLeft(32, '0');

                Key Authen_KEY = new Key(DLMS_Common.String_to_Hex_array(key), 1, 1,
                                                    KEY_ID.AuthenticationKey);


                string ek = "F2CE6D0BC7E53DA0B23FCCEE9736D617";
                key = ek.PadLeft(32, '0');
                string ic = "123456";
                uint counter = Convert.ToUInt32(ic);
                Key Encrypt_KEY = new Key(DLMS_Common.String_to_Hex_array(key), counter, 0,
                                                     KEY_ID.GLOBAL_Unicast_EncryptionKey);

                Key KEK_KEY = new Key(new byte[]{0xAA, 0x55, 0x33, 0xCC,0xAA, 0x55, 0x33, 0xCC,
                                                     0xAA, 0x55, 0x33, 0xCC,0xAA, 0x55, 0x33, 0xCC}, 02, 02,
                                                 KEY_ID.MasterKey);

                _Security_Data.AuthenticationKey = Authen_KEY;
                _Security_Data.EncryptionKey = Encrypt_KEY;
                _Security_Data.MasterEncryptionKey = KEK_KEY;
                return _Security_Data;
            }
            catch (Exception)
            {
                throw new Exception("Error While Initializing Security Data.");
            }
        }

        public static MeterConfig GetDefaultMeterSetting()
        {
            MeterConfig meterSet = new MeterConfig();
            meterSet.Device = new Device();
            meterSet.Manufacturer = new Manufacturer();
            meterSet.Device_Configuration = new Configuration();
            return meterSet;

        }

        public static string GET_ErrorCodes(Exception Info, int InnerExceptionMessagePrintLevel = 14)
        {
            /// saveErrors(ex, IOConn);
            try
            {
                Regex RegEx_ErrorCodes = new Regex(@"\(Error Code:(\d+)\)", RegexOptions.Compiled);
                StringBuilder InnerMessages = new StringBuilder(255);

                string error_String = string.Empty;
                Exception _ex = Info;
                InnerMessages.AppendLine(Info.Message);

                for (int count = 1; count <= InnerExceptionMessagePrintLevel; count++)
                {
                    if (_ex.InnerException != null)
                        _ex = _ex.InnerException;
                    else
                        break;

                    InnerMessages.AppendFormat("{0} \r\n", _ex.Message);
                }

                error_String = InnerMessages.ToString();

                var errorCodes = RegEx_ErrorCodes.Matches(error_String);
                error_String = string.Empty;

                foreach (Match errTK in errorCodes)
                {
                    if (errTK.Success)
                        error_String += errTK.Value;
                }

                return error_String;
            }
            catch (Exception ex)
            {
                return String.Empty;
            }
        }
        public static string GET_ErrorCodes(string error_String)
        {
            Regex RegEx_ErrorCodes = new Regex(@"\(Error Code:(\d+)\)", RegexOptions.Compiled);
            var errorCodes = RegEx_ErrorCodes.Matches(error_String);

            error_String = string.Empty;

            foreach (Match errTK in errorCodes)
            {
                if (errTK.Success)
                    error_String += errTK.Value;
            }
            return error_String;
        }

        static public string ArrayToHexString(byte[] Hex_Array, int LineLength = 16, bool insertNewLine = true)
        {
            StringBuilder Return_String = new StringBuilder(Hex_Array.Length * 3 + 25);
            int byte_Counter = 0;

            foreach (byte Value in Hex_Array)
            {
                Return_String.AppendFormat("{0:X2} ", Value);
                byte_Counter++;

                // Insert New Line Break
                if (byte_Counter % LineLength == 0)
                {
                    if (insertNewLine)
                        Return_String.AppendLine();
                    else
                        // double space
                        Return_String.AppendFormat("    ");
                }
                // Insert New Extra Spaces 
                // After Each Octet
                else if (byte_Counter % 8 == 0)
                    Return_String.AppendFormat("  ");
            }

            return Return_String.ToString();
        }

        public static App_Validation_Info AppValidationInfo
        {
            get
            {
                App_Validation_Info validationInfo = null;
                try
                {

                    validationInfo = (App_Validation_Info)Get_CurrentInstance("App_Validation_Info_");
                    if (validationInfo == null)
                    {
                        ///Should be init on Application_Startup
                        ///default Constructor Object
                        validationInfo = new App_Validation_Info();
                        Set_CurrentInstance("App_Validation_Info_", validationInfo);
                    }
                    return validationInfo;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred while taking Object From Dictionary", ex);
                }
            }
            internal set
            {
                App_Validation_Info validationInfo = null;
                if (value == null)
                    validationInfo = new App_Validation_Info();
                else
                    validationInfo = value;
                Set_CurrentInstance("App_Validation_Info_", validationInfo);
            }
        }

        #region Get_CurrentInstance

        public static object Get_CurrentInstance(string keyBase)
        {
            try
            {
                string key = Get_CurrentKey(keyBase);
                object obj_val = null;
                int maxTryCount = 10;
                if (singletonInstaces.ContainsKey(key))
                    while (maxTryCount > 0)
                    {
                        if (singletonInstaces.TryGetValue(key, out obj_val))
                            break;
                        maxTryCount--;
                    }
                if (maxTryCount <= 0)
                    throw new Exception("maxTryCount <= 0");
                return obj_val;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while taking object from Singleton Cache", ex);
            }
        }

        public static object Set_CurrentInstance(string keyBase, Object currentInstance)
        {
            try
            {
                string key = Get_CurrentKey(keyBase);
                object obj_val = null;
                obj_val = singletonInstaces.AddOrUpdate(key, currentInstance, (_key, oldValue) => oldValue = currentInstance);
                return obj_val;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while taking object from Singleton Cache", ex);
            }
        }

        public static string Get_CurrentKey(string keyBase)
        {
            try
            {
                System.Diagnostics.Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();
                String current_key = String.Format("{0}_{1:D5}", keyBase, currentProcess.Id);
                return current_key;
            }
            catch
            {
            }
            return keyBase;
        }
        #endregion

        public static ushort Calculate_PPM(double Xtal_feq, ref bool isNegative)
        {
            App_Validation_Info _ValidationInfo = AppValidationInfo;
            ushort ppm_calc = 0;
            try
            {
                if (!(Xtal_feq < _ValidationInfo.Xtal_freq_max && Xtal_feq > _ValidationInfo.Xtal_freq_min))
                    throw new ArgumentOutOfRangeException("Xtal_feq");
                ///formula to convert XTAL into PPM
                double ppm = ((Xtal_feq - App_Validation_Info.Xtal_freq_Const) / App_Validation_Info.Xtal_freq_Const) * 1000000 * 10;
                isNegative = (ppm < 0);
                ppm_calc = Convert.ToUInt16(Math.Abs(ppm));
            }
            catch (Exception ex)
            {
                throw new OverflowException("PPM_Max", ex);
            }
            return ppm_calc;
        }

        public static double Calculate_XTALFrequency(ushort ppm_, bool isNegative)
        {
            App_Validation_Info _ValidationInfo = AppValidationInfo;
            double Xtal_freq = 0;
            double ppm_Calc = 0;
            try
            {
                if (isNegative)
                    ppm_Calc = ppm_ * -1;

                Xtal_freq = (((ppm_Calc * App_Validation_Info.Xtal_freq_Const) / 1000000 / 10) + App_Validation_Info.Xtal_freq_Const);

                if (!(Xtal_freq < _ValidationInfo.Xtal_freq_max && Xtal_freq > _ValidationInfo.Xtal_freq_min))
                    throw new ArgumentOutOfRangeException("Xtal_feq");
            }
            catch (Exception ex)
            {
                throw new OverflowException("ppm_", ex);
            }
            return Xtal_freq;
        }

        #region DLMS_Debugger_Event_Handlers

        public static void Debugger_Logger(string identifier, string msg, byte[] IODump, DateTime dtTimeStamp, LogType LogMessageType)
        {
            try
            {
                if (String.IsNullOrEmpty(identifier))
                    identifier = "";
                else
                {
                    identifier = identifier.Replace(".", "_");
                    identifier = identifier.Replace(":", "_");
                }

                string hexDt = "";
                if (IODump != null)
                    hexDt = Commons.ArrayToHexString(IODump);
                string msgtxt = String.Format("ID:{0},TimeStamp:{1}\r\n\r\n{2}\r\nIODump:{3},", identifier, dtTimeStamp, msg, hexDt);
                string dir = string.Empty;//See Later//string.Format(@"{0}\Logs\", Commons.GetApplicationConfigsDirectory());
                DirectoryInfo dirInfo = new DirectoryInfo(dir);
                if (!dirInfo.Exists)
                    dirInfo.Create();
                string filename = null;
                if (LogMessageType == LogType.ErrorLog)
                    filename = string.Format("{0}{1}_Error.txt", dir, identifier);
                else
                    filename = string.Format("{0}{1}.txt", dir, identifier);
                Write_IOLog(filename, msgtxt);
            }
            catch
            { }
        }

        public static void Debugger_IOLog(string identifier, byte[] IODump, DataStatus ReadStatus, DateTime dtTimeStamp)
        {
            try
            {
                string hexDt = "";
                if (IODump != null)
                    hexDt = Commons.ArrayToHexString(IODump);
                string msg = String.Format("{0} TimeStamp:{1}\t {2}>>>>{3},", identifier, dtTimeStamp, ReadStatus, hexDt);
                string dir = string.Empty;//See Later// string.Format(@"{0}\Logs\", Commons.GetApplicationConfigsDirectory());
                DirectoryInfo dirInfo = new DirectoryInfo(dir);
                if (!dirInfo.Exists)
                    dirInfo.Create();
                string filename = string.Format("{0}{1}_IO.txt", dir, identifier);
                Write_IOLog(filename, msg);
            }
            catch { }
        }

        public static void Write_IOLog(string fileName, string IOLog)
        {
            try
            {
                using (StreamWriter wt = new StreamWriter(new FileStream(fileName, FileMode.Append, FileAccess.Write)))
                {
                    wt.WriteLine(IOLog);
                    wt.WriteLine();
                }
            }
            catch { }
        }

        #endregion

        public static bool IsThreadRunning(System.Threading.Thread ThRunner)
        {
            bool IsRunning = false;
            try
            {
                if (ThRunner != null)
                {
                    #region Test Allocator Thread Status

                    if (ThRunner.ThreadState == System.Threading.ThreadState.Unstarted ||
                        ThRunner.ThreadState == System.Threading.ThreadState.StopRequested ||
                        ThRunner.ThreadState == System.Threading.ThreadState.Stopped ||
                        ThRunner.ThreadState == System.Threading.ThreadState.SuspendRequested ||
                        ThRunner.ThreadState == System.Threading.ThreadState.Suspended ||
                        ThRunner.ThreadState == System.Threading.ThreadState.AbortRequested ||
                        ThRunner.ThreadState == System.Threading.ThreadState.Aborted)
                        IsRunning = false;
                    else if (ThRunner.IsAlive)
                        IsRunning = true;

                    #endregion
                }
            }
            catch (Exception)
            { }
            return IsRunning;
        }

        public static bool IsTCP_Connected(Exception ex)
        {
            Exception ex_Internal = null;
            try
            {
                ex_Internal = ex;

                while (ex_Internal != null &&
                    (ex_Internal.GetType() != typeof(SocketException) &&
                    ex_Internal.GetType() != typeof(IOException) &&
                    ex_Internal.GetType() != typeof(ObjectDisposedException)))
                {
                    ex_Internal = ex_Internal.InnerException;

                }
                if (ex_Internal == null)
                {
                    return true;
                }
                else if (ex_Internal.GetType() == typeof(ObjectDisposedException)) return false;

                while (ex_Internal != null)
                {
                    if (ex_Internal.GetType() == typeof(SocketException))
                        break;
                    ex_Internal = ex_Internal.InnerException;
                }
                if (ex_Internal == null)
                {
                    return true;
                }

                SocketException ex_ = (SocketException)ex_Internal;

                switch (ex_.SocketErrorCode)
                {
                    #region Custom_Socket_ErrorMessages
                    case SocketError.IsConnected:
                        {
                            return true;
                        }
                    //10060 WSAETIMEDOUT
                    case SocketError.TimedOut:
                    //10036 WSAEINPROGRESS
                    case SocketError.AlreadyInProgress:
                    //10051 WSAENETUNREACH
                    case SocketError.NetworkUnreachable:
                    //10052 WSAENETRESET
                    case SocketError.NetworkReset:
                    case SocketError.NetworkDown:
                    //10054 WSAECONNRESET
                    case SocketError.ConnectionReset:
                    //10053 WSAECONNABORTED
                    case SocketError.ConnectionAborted:
                    //10057 WSAENOTCONN
                    case SocketError.NotConnected:
                    //10058 WSAESHUTDOWN
                    case SocketError.Disconnecting:
                    default:
                        {
                            return false;
                        }

                        #endregion
                }



            }
            catch
            { }
            //Error Case
            return true;
        }

        public static byte[] ConvertFromValidNumberString(String phoneTxt, bool isStandard = false)
        {
            try
            {
                char AppendChar = '?'; // 0x3F // Char.ConvertFromUtf32(63).ToCharArray()[0];
                bool isAlphaNumeric = false;
                phoneTxt = phoneTxt.TrimEnd("+-".ToCharArray());
                foreach (var char_Val in phoneTxt)
                {
                    if (!char.IsDigit(char_Val))
                    {
                        isAlphaNumeric = true;
                        break;
                    }
                }
                if (!isStandard)
                {
                    if (isAlphaNumeric)
                        AppendChar = Convert.ToChar(0x0F);
                    else
                        AppendChar = '?';
                    phoneTxt = phoneTxt.PadRight(16, AppendChar);
                }

                //byte[] byte_array = new byte[16] { 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63 };
                byte[] _tArray = Encoding.ASCII.GetBytes(phoneTxt);

                if (!isStandard)
                {
                    if (!isAlphaNumeric)
                    {
                        ///byte_array = DLMS_Common.Append_to_End(_tArray, byte_array);
                        for (int i = 0; i < _tArray.Length; i++)
                        {
                            _tArray[i] = (byte)(_tArray[i] - 48);
                        }
                    }
                    Array.Resize(ref _tArray, 16);
                }
                else
                {
                    Array.Resize(ref _tArray, phoneTxt.Length);
                }
                return _tArray;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool IsCellNull(object v)
        {
            if (v == DBNull.Value)
            {
                return true;
            }
            return false;

        }

        public static double ApplyMultiplier(double val, short multiplier)
        {
            try
            {
                double rawVal = val;
                if (multiplier < 0)
                {
                    rawVal = val / (Math.Pow(10, Math.Abs(multiplier)));
                }
                else if (multiplier > 0)
                {
                    rawVal = val * (Math.Pow(10, Math.Abs(multiplier)));
                }
                return rawVal;
            }
            catch (Exception ex)
            {
                return double.PositiveInfinity;
            }
        }
    }
}
