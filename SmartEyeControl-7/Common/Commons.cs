#define Enable_DEBUG_ECHO
#define Enable_Error_Logging
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using comm;
using DLMS;
using AccurateOptocomSoftware.Properties;

namespace Comm
{
    public class Commons
    {
//        //Added by Azeem Inayat
//        //v10.0.14 for AOS
//        public static string ACT34G_Meter = "ACT34G";

//        //End

//        public static readonly TimeSpan ReadLOCKLow_TimeOut = TimeSpan.FromSeconds(05);
//        public static readonly TimeSpan ReadLOCK_TimeOut = TimeSpan.FromSeconds(25);
//        public static readonly TimeSpan WriteLOCK_TimeOut = TimeSpan.FromSeconds(45);
//        public static readonly TimeSpan WriteLOCKLow_TimeOut = TimeSpan.FromSeconds(05);

//        public static readonly float KiloByteDivider = 1024.0f;
//        public static readonly float MegaByteDivider = 1048576.0f;
//        public static readonly float GigaByteDivider = 1073741824.0f;

//        public static bool EnableEcho = false;
//        public static Random NextRandomNum = null;

//        #region Out_Put_Class

//        /// <summary>
//        /// The main entry point for the application.
//        /// </summary>
//        public static FileStream fs = null;
//        public static StreamWriter wr = null;
//        public static BufferedStream _ConsoleStream = null;
//        public static readonly int ExceptionMaxLevel = 50;
//        public static readonly string LogSourceName = "MySource";
//        /// <summary>
//        /// Static Constructur
//        /// </summary>
//        static Commons()
//        {
//            Stream stdOut = Console.OpenStandardOutput();
//            _ConsoleStream = new BufferedStream(stdOut, 2048);
//            string LoadedAssemblyInfo = Assembly.GetExecutingAssembly().GetName().Version.ToString();
//            LogSourceName = "MySource" + LoadedAssemblyInfo;
//            NextRandomNum = new Random();
//            ///Init Variables
//            //ManagementDevice = new SAPConfig();
//            //ManagementDevice.SAP = new SAP_Object("Management", 0x01);
//            //ManagementDevice.RequestAccessRightsRead = false;
//            //ManagementDevice.FaceName = "Management";
//            //ManagementDevice.DefaultPassword = "mtiDLMScosem";

//            //Public = new SAP_Object("Public", 0x10);
//            //Management_Client = new SAP_Object("Management", 0x01);

//        }

//        #region Event_Logger

//        public static EventLog GetEventLogger()
//        {
//            try
//            {
//                if (!EventLog.SourceExists(LogSourceName))
//                {
//                    //An event log source should not be created and immediately used. 
//                    //There is a latency time to enable the source, it should be created 
//                    //prior to executing the application that uses the source. 
//                    //Execute this sample a second time to use the new source.
//                    EventLog.CreateEventSource(LogSourceName, "Application");
//                    // The source is created.  Exit the application to allow it to be registered. 
//                }
//                EventLog _logger = new EventLog("MTI_MDC_EventLog", ".", LogSourceName);
//                return _logger;
//            }
//            catch (Exception ex)
//            {

//                throw new Exception("Error occured while Getting Event Logger", ex);
//            }
//        }

//        public static void LogApplicationException(Exception ex, int curLevel = 10)
//        {
//            try
//            {
//                StringBuilder builder = BuildExceptionLog(ex, curLevel);
//                EventLog systemEventLogger = GetEventLogger();
//                systemEventLogger.WriteEntry(builder.ToString(), EventLogEntryType.FailureAudit);
//            }
//            catch (Exception) { }
//        }

//        #endregion

//        #region Save Error Exceptions

//        public static StreamWriter ExceptionsInit(string relativeDirPath, string fileName)
//        {
//            try
//            {
//                FileStream fs = null;
//                StreamWriter wr = null;

//                string dir = relativeDirPath;
//                if (!Directory.Exists(dir))
//                    Directory.CreateDirectory(dir);
//                string path = dir + String.Format("\\{0}", fileName);
//                if (!File.Exists(path))
//                    fs = new FileStream(path, FileMode.CreateNew);
//                else
//                    fs = File.Open(path, FileMode.Append);
//                wr = new StreamWriter(fs);
//                return wr;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public static void SaveException(Exception ex, string fileURL, int curLevel = 14)
//        {
//            Exception _ex = ex;
//            try
//            {
//                FileInfo FileINfo = new FileInfo(fileURL);
//                string fileName = FileINfo.Name;
//                string dirName = FileINfo.DirectoryName;

//                ///File Lenght Exceeds 20K 
//                if (FileINfo.Exists && FileINfo.Length > 20480)
//                    BackUpFileSameLoc(fileURL);

//                using (StreamWriter wr = ExceptionsInit(dirName, fileName))
//                {
//                    StringBuilder builder = BuildExceptionLog(ex, curLevel);
//                    wr.WriteLine(builder.ToString());
//                }
//            }
//            catch (Exception) { }
//        }

//        public static StringBuilder BuildExceptionLog(Exception ex, int curLevel = 14)
//        {
//            StringBuilder builder = new StringBuilder(512);

//            builder.AppendFormat("Unhandled Error{0} {1},details\r\n", DateTime.Now, ex.Message);
//            Exception _ex = ex;

//            while (_ex != null && curLevel < ExceptionMaxLevel)
//            {
//                _ex = _ex.InnerException;
//                if (_ex == null)
//                    break;
//                builder.AppendFormat("{0}\r\n", _ex.Message);
//                curLevel++;
//            }
//            if (ex != null)
//                builder.AppendLine(ex.StackTrace);
//            return builder;
//        }

//        public static void SaveApplicationException(Exception ex, int curLevel = 5)
//        {
//            try
//            {
//                string fileUrl = String.Format(@"{0}\Exceptions\Exception_{1}.txt", Commons.GetApplicationErrorDirectory(), DateTime.Now.ToFileTime());
//                SaveException(ex, fileUrl);
//            }
//            catch (Exception) { }
//        }


//        public static void BackUpFileSameLoc(string fileUrl)
//        {
//            try
//            {
//                FileInfo FileINfo = new FileInfo(fileUrl);
//                if (FileINfo.Exists)
//                {
//                    String NewFileName = FileINfo.Name.Replace(FileINfo.Extension, "");
//                    NewFileName = String.Format("{0}_{1}_{2}", NewFileName, DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString());
//                    ///Process New File Name
//                    NewFileName = NewFileName.Replace(" ", "");
//                    NewFileName = NewFileName.Replace(".", "");
//                    NewFileName = NewFileName.Replace("/", "");
//                    NewFileName = NewFileName.Replace(":", "");
//                    NewFileName = String.Format(@"{0}\{1}{2}", FileINfo.Directory, NewFileName, FileINfo.Extension);
//                    File.Move(fileUrl, NewFileName);
//                    File.Delete(fileUrl);
//                }
//            }
//            catch
//            {
//            }
//        }

//        public static void SaveApplicationLogMessage(StringBuilder BuilderMsg, string fileURL)
//        {

//            try
//            {
//                FileInfo FileINfo = new FileInfo(fileURL);
//                ///File Lenght Exceeds 10K 
//                if (FileINfo.Exists && FileINfo.Length > 10240)
//                    BackUpFileSameLoc(fileURL);

//                string fileName = FileINfo.Name;
//                string dirName = FileINfo.DirectoryName;

//                using (StreamWriter wr = ExceptionsInit(dirName, fileName))
//                {
//                    wr.WriteLine(BuilderMsg);
//                }
//            }
//            catch (Exception) { }
//        }

//        #endregion

//        public static void WriteLine(string text)
//        {
//            Write(text + "\r\n-------\t\t\t--------\t\t\t--------\r\n");
//        }

//        public static void Write(string text)
//        {
//            try
//            {
//                Console.Out.Write(text);
//                //byte[] textAsBytes = Encoding.UTF8.GetBytes(text);
//                //_ConsoleStream.Write(textAsBytes, 0, textAsBytes.Length);
//            }
//            catch (Exception)
//            { }
//        }

//        public static void WriteError(string text)
//        {
//            try
//            {
//                Console.ForegroundColor = ConsoleColor.Red;
//                Console.Out.WriteLine("\t" + text);
//                Console.ForegroundColor = ConsoleColor.White;
//            }
//            catch (Exception)
//            { }
//        }

//        public static void WriteSuccess(string text)
//        {
//            try
//            {
//                Console.ForegroundColor = ConsoleColor.Green;
//                Console.Out.WriteLine("\t" + text);
//                Console.ForegroundColor = ConsoleColor.White;
//            }
//            catch (Exception)
//            { }
//        }
//        public static void WriteAlert(string text)
//        {
//            try
//            {
//                Console.ForegroundColor = ConsoleColor.DarkCyan;
//                Console.Out.WriteLine("\t" + text);
//                Console.ForegroundColor = ConsoleColor.White;
//            }
//            catch (Exception)
//            { }
//        }

//        #endregion

//        public static string DecimalPoint_toGUI(byte value)
//        {
//            string temp;
//            int upper;
//            int lower;
//            upper = value / 16;
//            lower = value % 16;
//            temp = "";
//            temp = temp.PadLeft(upper, '0') + "." + temp.PadRight(lower, '0');
//            //temp =;
//            return temp;
//        }
//        /// <summary>
//        /// Removes blank spaces and new line chars from the string
//        /// </summary>
//        /// <param name="passed"></param>
//        /// <returns></returns>
//        private static string Remove_White_Spaces(string passed)
//        {

//            passed = passed.Replace(" ", "");
//            passed = passed.Replace("\x0d", "");
//            passed = passed.Replace("\x0a", "");

//            return passed;
//        }

//        //==========================================================================
//        //==========================================================================
//        /// <summary>
//        /// Converts byte array to string in hex format
//        /// </summary>
//        /// <param name="Hex_Array"></param>
//        /// <returns></returns>
//        static public string ArrayToHexString(byte[] Hex_Array)
//        {
//            string Return_String = "";
//            foreach (byte Value in Hex_Array) Return_String += (Value.ToString("X2") + " ");
//            return Return_String;
//        }

//        //==========================================================================
//        //==========================================================================
//        public static byte[] String_to_Hex_array(string passed)
//        {
//            passed = Remove_White_Spaces(passed);

//            passed = passed.ToUpper();

//            char[] temp = passed.ToCharArray();

//            byte[] to_return = new byte[(temp.Length) / 2];

//            for (byte i = 0; i < to_return.Length; i++)
//            {
//                if (temp[2 * i] >= '0' && temp[2 * i] <= '9') to_return[i] |= (byte)((temp[2 * i] - 0x30) << 4);
//                else if (temp[2 * i] >= 'A' && temp[2 * i] <= 'F') to_return[i] |= (byte)((temp[2 * i] - 0x37) << 4);

//                if (temp[(2 * i) + 1] >= '0' && temp[(2 * i) + 1] <= '9') to_return[i] |= (byte)(temp[(2 * i) + 1] - 0x30);
//                else if (temp[(2 * i) + 1] >= 'A' && temp[(2 * i) + 1] <= 'F') to_return[i] |= (byte)(temp[(2 * i) + 1] - 0x37);
//            }


//            return to_return;
//        }
//        //==========================================================================
//        #region Array Functions


//        static public byte[] Append_to_End(byte[] First_Array, byte[] Second_Array)
//        {
//            byte[] Appended = new byte[First_Array.Length + Second_Array.Length];

//            System.Buffer.BlockCopy(First_Array, 0, Appended, 0, First_Array.Length);
//            System.Buffer.BlockCopy(Second_Array, 0, Appended, First_Array.Length, Second_Array.Length);

//            return Appended;
//        }
//        //==========================================================================

//        //==========================================================================
//        /// <summary>
//        /// Appends second_byt at the end of first array
//        /// </summary>
//        /// <param name="First_Array"></param>
//        /// <param name="second_byte"></param>
//        /// <returns></returns>
//        static public byte[] Append_to_End(byte[] First_Array, byte second_byte)
//        {
//            byte[] Second_Array = new byte[1] { second_byte };
//            byte[] Appended = new byte[First_Array.Length + Second_Array.Length];

//            System.Buffer.BlockCopy(First_Array, 0, Appended, 0, First_Array.Length);
//            System.Buffer.BlockCopy(Second_Array, 0, Appended, First_Array.Length, Second_Array.Length);

//            return Appended;
//        }
//        //==========================================================================

//        //==========================================================================
//        /// <summary>
//        /// Appends second_long at the end of First_Array
//        /// </summary>
//        /// <param name="First_Array"></param>
//        /// <param name="second_long"></param>
//        /// <returns></returns>
//        static public byte[] Append_to_End(byte[] First_Array, ulong second_long)
//        {
//            byte[] Second_Array = new byte[4] 
//            { (byte)((ulong)second_long >> 24 & 0xff), (byte)((ulong)second_long >> 16 & 0xff),  
//             (byte)((ulong)second_long>>8 & 0xff), (byte)((ulong)second_long & 0xff) };
//            byte[] Appended = new byte[First_Array.Length + Second_Array.Length];

//            System.Buffer.BlockCopy(First_Array, 0, Appended, 0, First_Array.Length);
//            System.Buffer.BlockCopy(Second_Array, 0, Appended, First_Array.Length, Second_Array.Length);

//            return Appended;
//        }
//        //==========================================================================

//        //==========================================================================
//        /// <summary>
//        /// Takes two bytes and appends them in a byte array
//        /// </summary>
//        /// <param name="First_Byte"></param>
//        /// <param name="second_byte"></param>
//        /// <returns></returns>
//        static public byte[] Append_to_End(byte First_Byte, byte second_byte)
//        {
//            byte[] First_Array = new byte[1] { First_Byte };
//            byte[] Second_Array = new byte[1] { second_byte };
//            byte[] Appended = new byte[First_Array.Length + Second_Array.Length];

//            System.Buffer.BlockCopy(First_Array, 0, Appended, 0, First_Array.Length);
//            System.Buffer.BlockCopy(Second_Array, 0, Appended, First_Array.Length, Second_Array.Length);

//            return Appended;
//        }
//        //==========================================================================

//        //==========================================================================
//        /// <summary>
//        /// Appends second_short at the end of First_Array
//        /// </summary>
//        /// <param name="First_Array"></param>
//        /// <param name="second_short"></param>
//        /// <returns></returns>
//        static public byte[] Append_to_End(byte[] First_Array, UInt16 second_short)
//        {
//            byte[] Second_Array = new byte[2] { (byte)((short)second_short >> 8 & 0xff), (byte)((short)second_short & 0xff) };

//            byte[] Appended = new byte[First_Array.Length + Second_Array.Length];

//            System.Buffer.BlockCopy(First_Array, 0, Appended, 0, First_Array.Length);
//            System.Buffer.BlockCopy(Second_Array, 0, Appended, First_Array.Length, Second_Array.Length);

//            return Appended;
//        }


//        //==========================================================================
//        /// <summary>
//        /// Appends second array at the start of first array
//        /// </summary>
//        /// <param name="First_Array"></param>
//        /// <param name="Second_Array"></param>
//        /// <returns></returns>
//        static public byte[] Append_to_Start(byte[] First_Array, byte[] Second_Array)
//        {
//            byte[] Appended = new byte[First_Array.Length + Second_Array.Length];

//            System.Buffer.BlockCopy(Second_Array, 0, Appended, 0, Second_Array.Length);
//            System.Buffer.BlockCopy(First_Array, 0, Appended, Second_Array.Length, First_Array.Length);

//            return Appended;
//        }
//        //==========================================================================

//        //==========================================================================
//        static public byte[] Append_to_Start(byte[] First_Array, byte second_byte)
//        {
//            byte[] Second_Array = new byte[1] { second_byte };
//            byte[] Appended = new byte[First_Array.Length + Second_Array.Length];

//            System.Buffer.BlockCopy(Second_Array, 0, Appended, 0, Second_Array.Length);
//            System.Buffer.BlockCopy(First_Array, 0, Appended, Second_Array.Length, First_Array.Length);

//            return Appended;
//        }
//        //==========================================================================

//        //==========================================================================
//        static public byte[] Append_to_Start(byte[] First_Array, UInt16 second_short)
//        {
//            byte[] Second_Array = new byte[2] { (byte)((short)second_short >> 8 & 0xff), (byte)((short)second_short & 0xff) };

//            byte[] Appended = new byte[First_Array.Length + Second_Array.Length];

//            System.Buffer.BlockCopy(Second_Array, 0, Appended, 0, Second_Array.Length);
//            System.Buffer.BlockCopy(First_Array, 0, Appended, Second_Array.Length, First_Array.Length);

//            return Appended;
//        }
//        #endregion

//        #region Default_Application_Directory

//        public static string GetApplicationConfigsDirectory()
//        {
//            try
//            {
//                String fileDirectoryPath = (String)Settings.Default["ApplicationConfigsDirectory"];
//                if (!Directory.Exists(fileDirectoryPath))
//                {
//                    fileDirectoryPath = Environment.CurrentDirectory + @"\Application_Configs";
//                    if (!Directory.Exists(fileDirectoryPath))
//                        Directory.CreateDirectory(fileDirectoryPath);
//                    Settings.Default["ApplicationConfigsDirectory"] = fileDirectoryPath;
//                    Settings.Default.Save();
//                }
//                return fileDirectoryPath;
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }

//        }

//        public static string GetApplicationLogsDirectory()
//        {
//            try
//            {
//                String fileDirectoryPath = (String)Settings.Default["ApplicationConfigsDirectory"];

//                fileDirectoryPath = Environment.CurrentDirectory + @"\Application_IOLogs";
//                if (!Directory.Exists(fileDirectoryPath))
//                    Directory.CreateDirectory(fileDirectoryPath);

//                return fileDirectoryPath;
//            }
//            catch (Exception ex)
//            {
//                return Environment.CurrentDirectory;
//            }
//        }

//        public static string GetApplicationErrorDirectory()
//        {
//            try
//            {
//                String fileDirectoryPath = (String)Settings.Default["ApplicationConfigsDirectory"];

//                fileDirectoryPath = Environment.CurrentDirectory + @"\Application_Errors";
//                if (!Directory.Exists(fileDirectoryPath))
//                    Directory.CreateDirectory(fileDirectoryPath);

//                return fileDirectoryPath;
//            }
//            catch (Exception ex)
//            {
//                return Environment.CurrentDirectory;
//            }
//        }

//        public static AppDomain GetCurrentAppDomain()
//        {
//            AppDomain domain = null;
//            try
//            {
//                ///Load Current Application Domain
//                AppDomain root = AppDomain.CurrentDomain;
//                AppDomainSetup setup = new AppDomainSetup();
//                domain = AppDomain.CreateDomain(GetApplicationConfigsDirectory(), null, setup);
//            }
//            catch
//            {
//                domain = AppDomain.CurrentDomain;
//            }
//            return domain;
//        }


//        #endregion

//        public static String GetAccessRightsFilePath(string associationName)
//        {
//            try
//            {
//                if (String.IsNullOrEmpty(associationName))
//                    throw new Exception("Invalid Association Name");
//                String fileName = String.Format(@"{0}\{1}.xml", GetApplicationConfigsDirectory(), associationName);
//                return fileName;

//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//        }

//        public static string[] Directory_GetFiles(string path, string searchPattern, SearchOption searchOption)
//        {
//            string[] searchPatterns = searchPattern.Split('|');
//            List<string> files = new List<string>();
//            foreach (string sp in searchPatterns)
//                files.AddRange(System.IO.Directory.GetFiles(path, sp, searchOption));
//            files.Sort();
//            return files.ToArray();
//        }

//        public static string value_to_string(double value, bool Format)
//        {
//            if (double.IsNaN(value))
//                return "Not Access";
//            else if (double.IsPositiveInfinity(value))
//                return "Not Exist";
//            else if (double.IsNegativeInfinity(value))
//                return "Meter BUG";
//            else return value.ToString("f3");
//        }
//        public static string value_to_string(double value)
//        {
//            if (double.IsNaN(value))
//                return "Not Access";
//            else if (double.IsPositiveInfinity(value))
//                return "Not Exist";
//            else if (double.IsNegativeInfinity(value))
//                return "Meter BUG";
//            else return value.ToString("");
//        }

//        public static bool IsThreadRunning(Thread ThRunner)
//        {
//            bool IsRunning = false;
//            try
//            {
//                if (ThRunner != null)
//                {
//                    #region Test Allocater Thread Status

//                    if (ThRunner.ThreadState == System.Threading.ThreadState.Unstarted ||
//                                        ThRunner.ThreadState == System.Threading.ThreadState.StopRequested ||
//                                       ThRunner.ThreadState == System.Threading.ThreadState.Stopped ||
//                                       ThRunner.ThreadState == System.Threading.ThreadState.SuspendRequested ||
//                                        ThRunner.ThreadState == System.Threading.ThreadState.Suspended ||
//                                        ThRunner.ThreadState == System.Threading.ThreadState.AbortRequested ||
//                                        ThRunner.ThreadState == System.Threading.ThreadState.Aborted)
//                        IsRunning = false;
//                    else if (ThRunner.IsAlive)
//                        IsRunning = true;

//                    #endregion
//                }
//            }
//            catch
//            { }
//            return IsRunning;
//        }

//        #region Process_Info

//        public static int Processor_CoreCount
//        {
//            get
//            {
//                int coreCount = 0;
//                try
//                {

//                    foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_Processor").Get())
//                    {
//                        coreCount += int.Parse(item["NumberOfCores"].ToString());
//                    }
//                }
//                catch
//                {
//                    coreCount = -1;
//                }
//                return coreCount;
//            }
//        }

//        public static int ProcessorCount
//        {
//            get
//            {
//                int processorCount = 0;
//                try
//                {
//                    foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
//                    {
//                        processorCount += int.Parse(item["NumberOfProcessors"].ToString());
//                    }

//                }
//                catch
//                {
//                    processorCount = -1;
//                }
//                return processorCount;
//            }
//        }

//        public static int CPU_LogicalCoreCount
//        {
//            get
//            {
//                int coreCount = 0;
//                try
//                {

//                    foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
//                    {
//                        coreCount += int.Parse(item["NumberOfLogicalProcessors"].ToString());
//                    }
//                }
//                catch
//                {
//                    coreCount = -1;
//                }
//                return coreCount;
//            }
//        }

//        public static float TotalPhysicalMemory_GigaByte
//        {
//            get
//            {
//                float GigaByteMemory = 0.0f;
//                long memoryBytes = 0;
//                try
//                {
//                    foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
//                    {
//                        memoryBytes += long.Parse(item["TotalPhysicalMemory"].ToString());
//                    }
//                    GigaByteMemory = memoryBytes / GigaByteDivider;
//                }
//                catch
//                {
//                    memoryBytes = 0;
//                }
//                return GigaByteMemory;
//            }
//        }

//        public static int Processor_LogicalCoreCount
//        {
//            get
//            {
//                try
//                {
//                    return Environment.ProcessorCount;
//                }
//                catch
//                { }
//                return -1;
//            }
//        }

//        public static float WorkingSetGBytes
//        {
//            get
//            {
//                float WorkingSetInMBytes = -1.0f;
//                try
//                {
//                    WorkingSetInMBytes = Environment.WorkingSet / (GigaByteDivider);
//                }
//                catch
//                {
//                    WorkingSetInMBytes = -1.0f;
//                }
//                return WorkingSetInMBytes;
//            }
//        }

//        public static long ActiveThreadCount
//        {
//            get
//            {
//                try
//                {
//                    return ((IEnumerable)System.Diagnostics.Process.GetCurrentProcess().Threads)
//           .OfType<System.Diagnostics.ProcessThread>()
//           .Where(t => t.ThreadState == System.Diagnostics.ThreadState.Running)
//           .Count();
//                }
//                catch { }
//                return -1;
//            }
//        }

//        public static long ThreadCount
//        {
//            get
//            {
//                try
//                {
//                    return System.Diagnostics.Process.GetCurrentProcess().Threads.Count;
//                }
//                catch { }
//                return 0;
//            }
//        }

//        public static float WorkingSet64GBytes
//        {
//            get
//            {
//                float WorkingSetInMBytes = -1.0f;
//                try
//                {
//                    WorkingSetInMBytes = System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / (GigaByteDivider);
//                }
//                catch
//                {
//                    WorkingSetInMBytes = -1.0f;
//                }
//                return WorkingSetInMBytes;
//            }
//        }

//        public static float PrivateByte64GBytes
//        {
//            get
//            {
//                float PrivateBytetInMBytes = -1.0f;
//                try
//                {
//                    PrivateBytetInMBytes = System.Diagnostics.Process.GetCurrentProcess().PrivateMemorySize64 / (GigaByteDivider);
//                }
//                catch
//                {
//                    PrivateBytetInMBytes = -1.0f;
//                }
//                return PrivateBytetInMBytes;
//            }
//        }

//        public static float VirtualByte64GBytes
//        {
//            get
//            {
//                float VirtualSetInMBytes = -1.0f;
//                try
//                {
//                    VirtualSetInMBytes = System.Diagnostics.Process.GetCurrentProcess().VirtualMemorySize64 / (GigaByteDivider);
//                }
//                catch
//                {
//                    VirtualSetInMBytes = -1.0f;
//                }
//                return VirtualSetInMBytes;
//            }
//        }

//        #endregion

//        public class ProcessInfo
//        {

//            #region Limit Variable

//            /// <summary>
//            /// Modify Variable To Change Max Limit Variable 
//            /// </summary>
//            internal static readonly int MaxWorkerThreadPerCore = 250;
//            internal static readonly int MaxIOCompThreadPerCore = 1000;
//            internal static readonly float MinPrivateByteMemoryRatio = 0.75f;  ///Orignal 0.75f
//            internal static readonly float MaxPrivateByteMemoryRatio = 0.50f;  ///Orignal 0.50f
//            internal static readonly float MinMemoryLimit = 4.0f;
//            internal static readonly float LowMinMomoryLimit = 0.45f;          ///Orignal 0.35
//            internal static readonly float MaxToMinThreadRatio = 0.10f;
//            public static readonly float MinThreadCountStepDown = 0.25f;
//            public static readonly int MaxSampleCount = 10;


//            #endregion

//            #region Data_Members

//            public static readonly int Processor_Count = 1;
//            public static readonly int Processor_Core_Count = 1;

//            public static readonly float Total_AvailPhysicalMemory = 1.0f;

//            public static readonly int MaxWorkerThreadCount = 25;
//            public static readonly int DefaultMinWorkerThreadCount = 25;
//            public static readonly int FinalMinWorkerThreadCount = 25;
//            public static readonly int MaxIOCompelitionThreadCount = 1000;
//            public static readonly int DefaultMinIOCompelitionTheadCount = 1000;
//            public static readonly int FinalMinIOCompelitionTheadCount = 1000;

//            public static readonly float MaxMemoryUsageLimitGB = 1.5f;
//            public static readonly float MinMemoryUsageLimitGB = 0.7f;

//            internal static readonly object ProcessInfo_MemLock = null;
//            #endregion

//            #region Instance Data_Members

//            private int _CurMinWorkerThreadCount = 25;
//            private int _CurMinIOComplitionThreadCount = 1000;
//            private int _CurMaxWorkerThreadCount = 25;
//            private int _CurMaxIOComplitionThreadCount = 1000;
//            private Queue<ProcessInfoSample> ProcessSampleCollected = null;

//            #endregion

//            #region Constructur

//            /// <summary>
//            /// Static Constructur To Init Class Variables
//            /// </summary>
//            static ProcessInfo()
//            {
//                try
//                {
//                    ///Init Physical Processor Count
//                    Processor_Count = ProcessorCount;
//                    ///Init Processor_Core_Count
//                    Processor_Core_Count = Processor_CoreCount;
//                    ///Init System Available Physical Memory Limit
//                    Total_AvailPhysicalMemory = TotalPhysicalMemory_GigaByte;
//                    if (Processor_Core_Count > 0)
//                    {
//                        ///Init Max Worker Thread Counts
//                        MaxWorkerThreadCount = Processor_Core_Count * MaxWorkerThreadPerCore;
//                        FinalMinWorkerThreadCount = Convert.ToInt32(Math.Round(MaxWorkerThreadCount * MaxToMinThreadRatio));
//                        ///Init MaxIOComplitionThreadCount
//                        MaxIOCompelitionThreadCount = Processor_Core_Count * MaxIOCompThreadPerCore;
//                        FinalMinIOCompelitionTheadCount = Convert.ToInt32(Math.Round(MaxIOCompelitionThreadCount * MaxToMinThreadRatio));
//                    }
//                    if (Total_AvailPhysicalMemory > 0)
//                    {
//                        if (Total_AvailPhysicalMemory <= MinMemoryLimit)
//                        {
//                            MaxMemoryUsageLimitGB = Total_AvailPhysicalMemory * MinPrivateByteMemoryRatio;
//                        }
//                        else
//                        {
//                            MaxMemoryUsageLimitGB = Total_AvailPhysicalMemory * MaxPrivateByteMemoryRatio;
//                        }
//                        MinMemoryUsageLimitGB = Total_AvailPhysicalMemory * LowMinMomoryLimit;
//                    }
//                    ProcessInfo_MemLock = new object();
//                }
//                catch (Exception ex)
//                {
//                    throw new Exception("Error occured while initialize ProcessInfo_Common", ex);
//                }
//            }

//            public ProcessInfo()
//            {
//                try
//                {
//                    ///Init Instance Variable
//                    ProcessSampleCollected = new Queue<ProcessInfoSample>();
//                    _CurMaxWorkerThreadCount = MaxWorkerThreadCount;
//                    _CurMaxIOComplitionThreadCount = MaxIOCompelitionThreadCount;
//                    _CurMinWorkerThreadCount = FinalMinWorkerThreadCount;
//                    _CurMinIOComplitionThreadCount = FinalMinIOCompelitionTheadCount;
//                }
//                catch (Exception ex)
//                {
//                    throw new Exception("Error occured while initialize ProcessInfo_Common", ex);
//                }
//            }

//            #endregion

//            #region Property

//            public static float WorkingSetGBytes
//            {
//                get
//                {
//                    return Commons.WorkingSetGBytes;
//                }
//            }

//            public static long ActiveThreadCount
//            {
//                get
//                {
//                    return Commons.ActiveThreadCount;
//                }
//            }

//            public static long ThreadCount
//            {
//                get
//                {
//                    return Commons.ThreadCount;
//                }
//            }

//            public static float WorkingSet64GBytes
//            {
//                get
//                {
//                    return Commons.WorkingSet64GBytes;
//                }
//            }

//            public static float PrivateByte64GBytes
//            {
//                get
//                {
//                    return Commons.PrivateByte64GBytes;
//                }
//            }

//            public static float VirtualByte64GBytes
//            {
//                get
//                {
//                    return Commons.VirtualByte64GBytes;
//                }
//            }

//            #endregion

//            #region Property_Instance

//            public int CurrentMinWorkerThreadCount
//            {
//                get { return _CurMinWorkerThreadCount; }
//                set
//                {
//                    if (value >= DefaultMinWorkerThreadCount &&
//                        value <= FinalMinWorkerThreadCount &&
//                        value <= CurrentMaxWorkerThreadCount)
//                        _CurMinWorkerThreadCount = value;
//                }
//            }

//            public int CurrentMinIOCompelitionThreadCount
//            {
//                get { return _CurMinIOComplitionThreadCount; }
//                set
//                {
//                    if (value >= DefaultMinIOCompelitionTheadCount &&
//                        value <= FinalMinIOCompelitionTheadCount &&
//                        value <= CurrentMaxIOCompelitionThreadCount)
//                        _CurMinIOComplitionThreadCount = value;
//                }
//            }

//            public int CurrentMaxWorkerThreadCount
//            {
//                get { return _CurMaxWorkerThreadCount; }
//                set
//                {
//                    if ((value >= CurrentMinWorkerThreadCount &&
//                        value <= MaxWorkerThreadCount) ||
//                        value == DefaultMinWorkerThreadCount)
//                        _CurMaxWorkerThreadCount = value;
//                }
//            }

//            public int CurrentMaxIOCompelitionThreadCount
//            {
//                get { return _CurMaxIOComplitionThreadCount; }
//                set
//                {
//                    if ((value >= CurrentMinIOCompelitionThreadCount &&
//                        value <= MaxIOCompelitionThreadCount) ||
//                        value == DefaultMinIOCompelitionTheadCount)
//                        _CurMaxIOComplitionThreadCount = value;
//                }
//            }

//            public bool IsMaxPrivateMemoryUsageLimitExceed
//            {
//                get
//                {
//                    try
//                    {
//                        foreach (var procSample in ProcessSampleCollected)
//                        {
//                            if (procSample.PrivateByte64GBytes >= MaxMemoryUsageLimitGB)
//                                continue;
//                            else
//                                return false;
//                        }
//                        if (ProcessSampleCollected.Count < MaxSampleCount)
//                            return false;
//                        else
//                            return true;
//                    }
//                    catch { }
//                    return false;
//                }
//            }

//            public bool IsMaxPrivateMemoryUsageLimitBelowMaxUsage
//            {
//                get
//                {
//                    try
//                    {
//                        foreach (var procSample in ProcessSampleCollected)
//                        {
//                            if (procSample.PrivateByte64GBytes <= MaxMemoryUsageLimitGB)
//                                continue;
//                            else
//                                return false;
//                        }
//                        if (ProcessSampleCollected.Count < MaxSampleCount)
//                            return false;
//                        else
//                            return true;
//                    }
//                    catch { }
//                    return false;
//                }
//            }

//            public float IdleThreadRatio
//            {
//                get
//                {
//                    long InactiveThreadCount = 0;
//                    long ThreadCount = 0;
//                    int sampleCount = 0;
//                    float avgRatio = 0.0f;
//                    try
//                    {

//                        foreach (var procSample in ProcessSampleCollected)
//                        {
//                            ThreadCount += procSample.ThreadCount;
//                            InactiveThreadCount += (procSample.ThreadCount - procSample.AcitveThreadCount);
//                            avgRatio += (InactiveThreadCount / ThreadCount);
//                            sampleCount++;
//                        }
//                        avgRatio /= sampleCount;
//                    }
//                    catch
//                    {
//                        avgRatio = 0.0f;
//                    }
//                    return avgRatio;
//                }
//            }

//            public bool IsThreadCountersUpdated
//            {
//                get
//                {
//                    try
//                    {
//                        int maxWKThreadsCount = 0, maxIOThreadsCount = 0;
//                        int minWKThreadsCount = 0, minIOThreadsCount = 0;
//                        try
//                        {
//                            ThreadPool.GetMaxThreads(out maxWKThreadsCount, out maxIOThreadsCount);
//                            ThreadPool.GetMinThreads(out minWKThreadsCount, out minIOThreadsCount);
//                        }
//                        catch { }
//                        if (maxWKThreadsCount != CurrentMaxWorkerThreadCount ||
//                            maxIOThreadsCount != CurrentMaxIOCompelitionThreadCount ||
//                            minWKThreadsCount != CurrentMinWorkerThreadCount ||
//                            minIOThreadsCount != CurrentMinIOCompelitionThreadCount)
//                        {
//                            return false;
//                        }
//                        else
//                            return true;
//                    }
//                    catch
//                    { }
//                    return false;
//                }
//            }

//            #endregion

//            #region Member Methods

//            public int StepDownCurrentCount(int CurrentCounter)
//            {
//                int latestCounter = CurrentCounter - Convert.ToInt32(Math.Round(CurrentCounter * MinThreadCountStepDown));
//                return latestCounter;
//            }

//            public bool StepDownThreadCounters()
//            {
//                try
//                {
//                    ///Init Current Thread Counters
//                    int curMaxWThCounterBef = CurrentMaxWorkerThreadCount;
//                    int curMaxIOThCounterBef = CurrentMaxIOCompelitionThreadCount;
//                    int curMinWThCounterBef = CurrentMinWorkerThreadCount;
//                    int curMinIOThCounterBef = CurrentMinIOCompelitionThreadCount;
//                    ///Step Down Thread Counters
//                    int curMaxWThCounterAft = StepDownCurrentCount(CurrentMaxWorkerThreadCount);
//                    int curMaxIOThCounterAft = StepDownCurrentCount(CurrentMaxIOCompelitionThreadCount);
//                    int curMinWThCounterAft = StepDownCurrentCount(CurrentMinWorkerThreadCount);
//                    int curMinIOThCounterAft = StepDownCurrentCount(CurrentMinIOCompelitionThreadCount);
//                    ///Update Counter Variable
//                    CurrentMaxWorkerThreadCount = curMaxWThCounterAft;
//                    CurrentMaxIOCompelitionThreadCount = curMaxIOThCounterAft;
//                    CurrentMinWorkerThreadCount = curMinWThCounterAft;
//                    curMinIOThCounterAft = CurrentMinIOCompelitionThreadCount;
//                    ///Validates All Values Updated
//                    if (CurrentMaxWorkerThreadCount != curMaxWThCounterAft ||
//                        CurrentMaxIOCompelitionThreadCount != curMaxIOThCounterAft ||
//                        CurrentMinWorkerThreadCount != curMinWThCounterAft ||
//                        curMinIOThCounterAft != CurrentMinIOCompelitionThreadCount)
//                    {
//                        ///Restore All Previouse Values
//                        CurrentMaxWorkerThreadCount = curMaxWThCounterBef;
//                        CurrentMaxIOCompelitionThreadCount = curMaxIOThCounterBef;
//                        CurrentMinWorkerThreadCount = curMinWThCounterBef;
//                        CurrentMinIOCompelitionThreadCount = curMinIOThCounterBef;
//                        return false;
//                    }
//                    else
//                        return true;
//                }
//                catch
//                {
//                }
//                return false;
//            }

//            public bool LowerThreadCounters()
//            {
//                try
//                {
//                    _CurMaxWorkerThreadCount = DefaultMinWorkerThreadCount;
//                    _CurMaxIOComplitionThreadCount = DefaultMinIOCompelitionTheadCount;
//                    _CurMinWorkerThreadCount = DefaultMinWorkerThreadCount;
//                    _CurMinIOComplitionThreadCount = DefaultMinIOCompelitionTheadCount;
//                    return true;
//                }
//                catch
//                {
//                }
//                return false;
//            }

//            public void ResetCounters()
//            {
//                _CurMaxWorkerThreadCount = MaxWorkerThreadCount;
//                _CurMaxIOComplitionThreadCount = MaxIOCompelitionThreadCount;
//                _CurMinWorkerThreadCount = FinalMinWorkerThreadCount;
//                _CurMinIOComplitionThreadCount = FinalMinIOCompelitionTheadCount;
//            }

//            public void ResetProcessSample()
//            {
//                ///Reset All Samples Stored
//                ProcessSampleCollected.Clear();
//            }

//            public void TakeProcessCounterSample()
//            {
//                try
//                {
//                    ProcessInfoSample ProcessInfoSample =
//                            new ProcessInfoSample(ActiveThreadCount, ThreadCount, PrivateByte64GBytes, VirtualByte64GBytes);
//                    if (ProcessSampleCollected.Count > MaxSampleCount)
//                        ProcessSampleCollected.Dequeue();
//                    ///Insert New ProcessSample
//                    ProcessSampleCollected.Enqueue(ProcessInfoSample);
//                }
//                catch (Exception ex)
//                {
//                    throw new Exception("Error occured to take process info sample", ex);
//                }
//            }

//            public bool TryUpdateThreadCounts()
//            {
//                bool IsUpdated = false;
//                try
//                {
//                    int curMaxWorkTh = CurrentMaxWorkerThreadCount, curMaxIOTh = CurrentMaxIOCompelitionThreadCount;
//                    int curMinWorkTh = CurrentMinWorkerThreadCount, curMinIOTh = CurrentMinIOCompelitionThreadCount;

//                    lock (this)
//                    {
//                        System.Threading.ThreadPool.GetMinThreads(out curMinWorkTh, out curMinIOTh);
//                        System.Threading.ThreadPool.GetMaxThreads(out curMaxWorkTh, out curMaxIOTh);

//                        ///Reset Connection Thread Counts Logic
//                        if (curMaxWorkTh <= CurrentMinWorkerThreadCount || curMaxIOTh <= CurrentMinIOCompelitionThreadCount)
//                        {
//                            IsUpdated = System.Threading.ThreadPool.SetMaxThreads(CurrentMaxWorkerThreadCount, CurrentMaxIOCompelitionThreadCount);
//                            if (!IsUpdated)
//                                return IsUpdated;
//                            IsUpdated = System.Threading.ThreadPool.SetMinThreads(CurrentMinWorkerThreadCount, CurrentMinIOCompelitionThreadCount);
//                        }
//                        else
//                        {
//                            ///Settings.Default.WorkerThreadPoolSize, Settings.Default.IOThreadPoolSize);
//                            IsUpdated = System.Threading.ThreadPool.SetMinThreads(CurrentMinWorkerThreadCount, CurrentMinIOCompelitionThreadCount);
//                            if (!IsUpdated)
//                                return IsUpdated;
//                            IsUpdated = System.Threading.ThreadPool.SetMaxThreads(CurrentMaxWorkerThreadCount, CurrentMaxIOCompelitionThreadCount);
//                        }
//                    }
//                    return IsUpdated;
//                }
//                catch { }
//                finally
//                {
//                    #region Debugging && Logging Message
//#if Enable_DEBUG_ECHO
//                    if (IsUpdated)
//                    {
//                        Commons.WriteAlert(String.Format("ThreadPool MaxWorkerThreadCount:{0} MaxIOThreadCount:{1} ", CurrentMaxWorkerThreadCount,
//                            CurrentMaxIOCompelitionThreadCount));
//                        Commons.WriteAlert(String.Format("ThreadPool MinWorkerThreadCount:{0} MinIOThreadCount:{1} ", CurrentMinWorkerThreadCount,
//                            CurrentMinIOCompelitionThreadCount));
//                    }
//#endif
//                    #endregion
//                }
//                return IsUpdated;
//            }

//            public static bool UpdateThreadCounts()
//            {
//                bool IsUpdated = false;
//                try
//                {
//                    int curMaxWorkTh = MaxWorkerThreadCount, curMaxIOTh = MaxIOCompelitionThreadCount;
//                    int curMinWorkTh = FinalMinWorkerThreadCount, curMinIOTh = FinalMinIOCompelitionTheadCount;

//                    lock (ProcessInfo_MemLock)
//                    {
//                        System.Threading.ThreadPool.GetMinThreads(out curMinWorkTh, out curMinIOTh);
//                        System.Threading.ThreadPool.GetMaxThreads(out curMaxWorkTh, out curMaxIOTh);

//                        ///Reset Connection Thread Counts Logic
//                        if (curMaxWorkTh <= FinalMinWorkerThreadCount || curMaxIOTh <= FinalMinIOCompelitionTheadCount)
//                        {
//                            IsUpdated = System.Threading.ThreadPool.SetMaxThreads(MaxWorkerThreadCount, MaxIOCompelitionThreadCount);
//                            if (!IsUpdated)
//                                return IsUpdated;
//                            IsUpdated = System.Threading.ThreadPool.SetMinThreads(FinalMinWorkerThreadCount, FinalMinIOCompelitionTheadCount);
//                        }
//                        else
//                        {
//                            ///Settings.Default.WorkerThreadPoolSize, Settings.Default.IOThreadPoolSize);
//                            IsUpdated = System.Threading.ThreadPool.SetMinThreads(MaxWorkerThreadCount, MaxIOCompelitionThreadCount);
//                            if (!IsUpdated)
//                                return IsUpdated;
//                            IsUpdated = System.Threading.ThreadPool.SetMaxThreads(MaxWorkerThreadCount, MaxIOCompelitionThreadCount);
//                        }
//                    }
//                    return IsUpdated;
//                }
//                catch { }
//                finally
//                {
//                    #region Debugging && Logging Message
//#if Enable_DEBUG_ECHO
//                    if (IsUpdated)
//                    {
//                        //Commons.WriteAlert(String.Format("ThreadPool MaxWorkerThreadCount:{0} MaxIOThreadCount:{1} ", CurrentMaxWorkerThreadCount,
//                        //    CurrentMaxIOCompelitionThreadCount));
//                        //Commons.WriteAlert(String.Format("ThreadPool MinWorkerThreadCount:{0} MinIOThreadCount:{1} ", CurrentMinWorkerThreadCount,
//                        //    CurrentMinIOCompelitionThreadCount));
//                    }
//#endif
//                    #endregion
//                }
//                return IsUpdated;
//            }

//            #endregion

//        }

//        public struct ProcessInfoSample
//        {
//            public long AcitveThreadCount;
//            public long ThreadCount;
//            public float PrivateByte64GBytes;
//            public float VirtualByte64GBytes;

//            public ProcessInfoSample(long AcitveThreadCount = 0,
//            long ThreadCount = 0,
//            float PrivateByte64GBytes = 0.0f,
//            float VirtualByte64GBytes = 0.0f)
//            {
//                ///Init ProcessInfoSample
//                this.AcitveThreadCount = AcitveThreadCount;
//                this.ThreadCount = ThreadCount;
//                this.PrivateByte64GBytes = PrivateByte64GBytes;
//                this.VirtualByte64GBytes = VirtualByte64GBytes;
//            }
//        }

    }
}
