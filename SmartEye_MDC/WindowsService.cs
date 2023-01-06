// #define Enable_License

using Communicator.MTI_MDC;
using Communicator.Properties;
using DatabaseManager.Database;
using DLMS;
using LogSystem.Shared.Common.Enums;
using SharedCode.Comm.DataContainer;
using SharedCode.Common;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;

namespace Communicator
{
    public class WindowsService : ServiceBase
    {
        public static bool StartServer = true;
        string Start_Server_Text = null;
        private TimeSpan MAX_SHUTDOWN_TIME = TimeSpan.FromSeconds(120);

        private MDC mdcServer = null;
        DateTime startTime = DateTime.Now;
        DateTime stopTime;
        DatabaseController DBController;
        Stats stats_obj = null;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int SW_MINIMIZE = 6;
        private const int SW_MAXIMIZE = 3;
        private const int SW_RESTORE = 9;

        #region Properties

        public MDC MDCServer
        {
            get { return mdcServer; }
            set { mdcServer = value; }
        }

        #endregion

        /// <summary>
        /// Public Constructor for WindowsService
        /// Put all of your Initialization code here
        /// </summary>
        public WindowsService()
        {
            try
            {
                this.ServiceName = "[MDC] Smart_Meter_Data_Collector";
                this.EventLog.Log = "Application";

                // Set Current Directory Path
                string Exec_current_Working_Directory = typeof(MDC).Assembly.Location;
                FileInfo Exec_URL = new FileInfo(Exec_current_Working_Directory);
                var current_Working_Directory = Exec_URL.Directory;

                if (!string.IsNullOrEmpty(Exec_current_Working_Directory) &&
                    current_Working_Directory.Exists)
                    Directory.SetCurrentDirectory(current_Working_Directory.FullName);

                // Object Initial Work
                mdcServer = new MDC();
                DBController = new DatabaseController();

                // These Flags set whether or not to handle that specific
                // type of event. Set to true if you need it, false otherwise
                this.CanHandlePowerEvent = false;
                this.CanHandleSessionChangeEvent = false;
                this.CanPauseAndContinue = true;
                this.CanShutdown = true;
                this.CanStop = true;

                Commons.AppName = Application.ProductName + " Output";
                Commons.AppVersion = Application.ProductVersion;
                Commons.AppLastBuild = GetBuildDateTime(Assembly.GetEntryAssembly());
            }
            catch (Exception ex)
            {
                // Log Exceptional Message
                if (MDCServer.Activity_DataLogger != null)
                {
                    MDCServer.Activity_DataLogger.WriteError(ex);
                }
                else
                {
                    MDC.Default_DataLogger.WriteError(ex, LogDestinations.EventLog | LogDestinations.TextFile);
                }

                this.EventLog.WriteEntry(ex.Message, EventLogEntryType.FailureAudit);
            }
        }

        /// <summary>
        /// The Main Thread: This is where your Service is Run.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            bool isDebugMode = false;

            try
            {
                if (args != null && args.Length > 0 &&
                   args.Contains<string>("DebugService"))
                    isDebugMode = true;

#if DEBUG
                isDebugMode = true;
#endif

                if (isDebugMode)
                {
                    Communicator.MTI_MDC.Program.Main();
                }
                else
                {
                    #region License Verification

                    try
                    {

#if Enable_License

                        if (ProductValidationEngine.Current.Verify())
                        {
#endif

                        ServiceBase.Run(new WindowsService());

#if Enable_License
                        }
                        else
                        {
                            // Raise License Verification Error
                            throw new SecurityException(String.Format("Application startup sequence failed.\r\n {0} {1}", ProductValidationEngine.Current.LastError,
                            Application.ProductName));
                        }
#endif
                    }
                    catch
                    {
                        throw;
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                // Log Exceptional Message
                MDC.Default_DataLogger.WriteError(ex, LogDestinations.EventLog | LogDestinations.TextFile | LogDestinations.Console);
            }
        }


        /// <summary>
        /// Dispose of objects that need it here.
        /// </summary>
        /// <param name="disposing">Whether
        ///    or not disposing is going on.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (MDCServer != null)
                MDCServer.Dispose();
        }

        /// <summary>
        /// OnStart(): Put startup code here
        ///  - Start threads, get initial data, etc.
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            try
            {
                bool isDebugMode = false;

                if (args != null && args.Length > 0 &&
                   args.Contains<string>("DebugService"))
                    isDebugMode = true;

                if (!isDebugMode)
                    base.OnStart(args);

                ResumeStart();

                this.EventLog.WriteEntry(string.Format("MDC Server Successfully Started {0}", DateTime.Now),
                                         System.Diagnostics.EventLogEntryType.SuccessAudit);
            }
            catch (Exception ex)
            {
                // Log Exceptional Message
                if (MDCServer.Activity_DataLogger != null)
                {
                    MDCServer.Activity_DataLogger.WriteError(ex);
                }
                else
                {
                    MDC.Default_DataLogger.WriteError(ex,
                              LogDestinations.EventLog | LogDestinations.TextFile);
                }

                this.EventLog.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.FailureAudit);
            }
        }

        /// <summary>
        /// OnStop(): Put your stop code here
        /// - Stop threads, set final data, etc.
        /// </summary>
        protected override void OnStop()
        {
            try
            {
                ShutDown();
                base.OnStop();

                this.EventLog.WriteEntry(string.Format("MDC Server Stop {0}", DateTime.Now),
                                         System.Diagnostics.EventLogEntryType.SuccessAudit);
            }
            catch (Exception ex)
            {
                // Log Exceptional Message
                if (MDCServer.Activity_DataLogger != null)
                {
                    MDCServer.Activity_DataLogger.WriteError(ex);
                }
                else
                {
                    MDC.Default_DataLogger.WriteError(ex,
                              LogDestinations.EventLog | LogDestinations.TextFile);
                }

                this.EventLog.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.FailureAudit);
            }
        }

        /// <summary>
        /// OnPause: Put your pause code here
        /// - Pause working threads, etc.
        /// </summary>
        protected override void OnPause()
        {
            try
            {
                ShutDown();
                base.OnPause();

                this.EventLog.WriteEntry(string.Format("MDC Server Paused  {0}", DateTime.Now),
                                         System.Diagnostics.EventLogEntryType.SuccessAudit);
            }
            catch (Exception ex)
            {
                // Log Exceptional Message
                if (MDCServer.Activity_DataLogger != null)
                {
                    MDCServer.Activity_DataLogger.WriteError(ex);
                }
                else
                {
                    MDC.Default_DataLogger.WriteError(ex,
                              LogDestinations.EventLog | LogDestinations.TextFile);
                }

                this.EventLog.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.FailureAudit);
            }
        }

        /// <summary>
        /// OnContinue(): Put your continue code here
        /// - Un-pause working threads, etc.
        /// </summary>
        protected override void OnContinue()
        {
            try
            {
                base.OnContinue();
                ResumeStart();

                this.EventLog.WriteEntry(string.Format("MDC Server Re-Started Successfully {0}", DateTime.Now),
                                         System.Diagnostics.EventLogEntryType.SuccessAudit);
            }
            catch (Exception ex)
            {
                // Log Exceptional Message
                if (MDCServer.Activity_DataLogger != null)
                {
                    MDCServer.Activity_DataLogger.WriteError(ex);
                }
                else
                {
                    MDC.Default_DataLogger.WriteError(ex,
                              LogDestinations.EventLog | LogDestinations.TextFile);
                }

                this.EventLog.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.FailureAudit);
            }
        }

        /// <summary>
        /// OnShutdown(): Called when the System is shutting down
        /// - Put code here when you need special handling
        ///   of code that deals with a system shutdown, such
        ///   as saving special data before shutdown.
        /// </summary>
        protected override void OnShutdown()
        {
            try
            {
                StopServerForcefully();
                base.OnShutdown();

                this.EventLog.WriteEntry(string.Format("MDC Server Shut Down {0}", DateTime.Now),
                                         System.Diagnostics.EventLogEntryType.SuccessAudit);
            }
            catch (Exception ex)
            {
                // Log Exceptional Message
                if (MDCServer.Activity_DataLogger != null)
                {
                    MDCServer.Activity_DataLogger.WriteError(ex);
                }
                else
                {
                    MDC.Default_DataLogger.WriteError(ex,
                              LogDestinations.EventLog | LogDestinations.TextFile);
                }

                this.EventLog.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.FailureAudit);
            }
        }

        /// <summary>
        /// OnCustomCommand(): If you need to send a command to your
        ///   service without the need for Remoting or Sockets, use
        ///   this method to do custom methods.
        /// </summary>
        /// <param name="command">Arbitrary Integer between 128 & 256</param>
        protected override void OnCustomCommand(int command)
        {
            // A custom command can be sent to a service by using this method:
            // #  int command = 128; //Some Arbitrary number between 128 & 256
            // #  ServiceController sc = new ServiceController("NameOfService");
            // #  sc.ExecuteCommand(command);

            base.OnCustomCommand(command);
        }

        /// <summary>
        /// OnPowerEvent(): Useful for detecting power status changes,
        ///   such as going into Suspend mode or Low Battery for laptops.
        /// </summary>
        /// <param name="powerStatus">The Power Broadcast Status
        /// (BatteryLow, Suspend, etc.)</param>
        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            return base.OnPowerEvent(powerStatus);
        }

        /// <summary>
        /// OnSessionChange(): To handle a change event
        /// from a Terminal Server session.
        /// Useful if you need to determine
        /// when a user logs in remotely or logs off,
        /// or when someone logs into the console.
        /// </summary>
        /// <param name="changeDescription">The Session Change
        /// Event that occurred.</param>
        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            base.OnSessionChange(changeDescription);
        }

        #region Member Methods

        public void ResumeStart()
        {
            try
            {
                Start_Server();
            }
            catch (Exception ex)
            {
                // Log Exceptional Message
                if (MDCServer.Activity_DataLogger != null)
                {
                    MDCServer.Activity_DataLogger.WriteError(ex);
                }
                else
                {
                    MDC.Default_DataLogger.WriteError(ex,
                              LogDestinations.EventLog | LogDestinations.TextFile);
                }
            }
        }

        #endregion

        #region Support Method

        #region Gets the build date and time (by reading the COFF header)

        // http://msdn.microsoft.com/en-us/library/ms680313

        struct _IMAGE_FILE_HEADER
        {
            public ushort Machine;
            public ushort NumberOfSections;
            public uint TimeDateStamp;
            public uint PointerToSymbolTable;
            public uint NumberOfSymbols;
            public ushort SizeOfOptionalHeader;
            public ushort Characteristics;
        };

        static DateTime GetBuildDateTime(Assembly assembly)
        {
            if (File.Exists(assembly.Location))
            {
                var buffer = new byte[Math.Max(Marshal.SizeOf(typeof(_IMAGE_FILE_HEADER)), 4)];
                using (var fileStream = new FileStream(assembly.Location, FileMode.Open, FileAccess.Read))
                {
                    fileStream.Position = 0x3C;
                    fileStream.Read(buffer, 0, 4);
                    fileStream.Position = BitConverter.ToUInt32(buffer, 0); // COFF header offset
                    fileStream.Read(buffer, 0, 4); // "PE\0\0"
                    fileStream.Read(buffer, 0, buffer.Length);
                }
                var pinnedBuffer = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                try
                {
                    var coffHeader = (_IMAGE_FILE_HEADER)Marshal.PtrToStructure(pinnedBuffer.AddrOfPinnedObject(), typeof(_IMAGE_FILE_HEADER));

                    return TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1) + new TimeSpan(coffHeader.TimeDateStamp * TimeSpan.TicksPerSecond));
                }
                finally
                {
                    pinnedBuffer.Free();
                }
            }
            return new DateTime();
        }

        #endregion

        public void clearLogs()
        {
            try
            {
                string dir = string.Format(@"{0}\Errors", LocalCommon.GetApplicationConfigsDirectory());
                if (Directory.Exists(dir))
                    Directory.Delete(dir, true);

                dir = string.Format(@"{0}\Logs", LocalCommon.GetApplicationConfigsDirectory());
                if (Directory.Exists(dir))
                    Directory.Delete(dir, true);

                dir = string.Format(@"{0}\Exceptions", LocalCommon.GetApplicationConfigsDirectory());
                if (Directory.Exists(dir))
                    Directory.Delete(dir, true);

                // string file = string.Format(@"{0}\ConnectionLog.txt", Commons.GetApplicationConfigsDirectory());
                // if (File.Exists(file))
                //     File.Delete(file);

                // file = string.Format(@"{0}\Schedule_Errors.txt", Commons.GetApplicationConfigsDirectory());
                // if (File.Exists(file))
                //     File.Delete(file);
            }
            catch (Exception)
            {
                throw new Exception("Error clearing log");
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void SET_Server_Run_Status(string Status)
        {
            Start_Server_Text = Status;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private string GET_Server_Run_Status()
        {
            return Start_Server_Text;
        }

        #endregion

        #region Events

        private void Start_Server()
        {
            try
            {
                // Set Current Directory Path
                string Exec_current_Working_Directory = typeof(MDC).Assembly.Location;
                FileInfo Exec_URL = new FileInfo(Exec_current_Working_Directory);
                var current_Working_Directory = Exec_URL.Directory;

                if (!string.IsNullOrEmpty(Exec_current_Working_Directory) &&
                    current_Working_Directory.Exists)
                    Directory.SetCurrentDirectory(current_Working_Directory.FullName);

                if (MDCServer == null)
                {
                    MDCServer = new MDC();
                }

                if (DBController == null)
                    DBController = new DatabaseController();

                if (MDCServer.IsServerRunning ||
                    (!String.IsNullOrEmpty(GET_Server_Run_Status()) &&
                     string.CompareOrdinal(GET_Server_Run_Status(), "Server Start") == 0))
                    StopServerForcefully();

                // Start Server
                MDCServer.Start();
                startTime = startTime.Add(DateTime.Now.Subtract(stopTime));
                if (Settings.Default.SaveMDCSessions)
                {
                    DBController.insert_mdc_session(Application.ProductVersion, startTime, DateTime.MaxValue);
                }

                if (MDCServer.IsServerRunning)
                {
                    if (MDCServer.Activity_DataLogger != null)
                    {
                        MDCServer.Activity_DataLogger.LogMessage(string.Format("{0} Server Started,Listening On TCP Port {1}",
                                                                 Settings.Default.Instance, Settings.Default.Port));
                    }
                    else
                    {
                        MDC.Default_DataLogger.WriteInformation(string.Format("{0} Server Started,Listening On TCP Port {1}",
                                                                     Settings.Default.Instance, Settings.Default.Port),
                                                                     LogDestinations.EventLog | LogDestinations.TextFile);
                    }

                    SET_Server_Run_Status("Server Start");
                }
            }
            catch (Exception ex)
            {
                // Log Exceptional Message
                if (MDCServer.Activity_DataLogger != null)
                {
                    MDCServer.Activity_DataLogger.WriteError(ex);
                }
                else
                {
                    MDC.Default_DataLogger.WriteError(ex, LogDestinations.EventLog | LogDestinations.TextFile);
                }
            }
            finally
            {
            }
        }

        private void ShutDown()
        {
            try
            {
                ShutDownGracefully();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ShutDownGracefully()
        {

            if (MDCServer.TCPConController.TCPServer.ConnectionList.Count > 0)
            {
                MDCServer.TCPConController.TCPServer.Disconnect_Server();
                MDCServer.TCPConController.TCPServer.IsShutDownInitiated = true;
                SpinWait.SpinUntil(() => { return MDCServer.TCPConController.TCPServer.ConnectionList.Count == 0; }, MAX_SHUTDOWN_TIME);
            }
            StopServerForcefully();
        }

        private void StopServerForcefully()
        {
            MDCServer.Stop();

            if (Settings.Default.SaveMDCSessions)
                DBController.update_mdc_session(DateTime.Now, startTime);
            try
            {
                // Console.Clear();
                if (!MDCServer.IsServerRunning)
                {
                    if (MDCServer.Activity_DataLogger != null)
                    {
                        MDCServer.Activity_DataLogger.LogMessage(string.Format("{0} Server Stopped", Settings.Default.Instance));
                    }
                    else
                    {
                        MDC.Default_DataLogger.WriteInformation(string.Format("{0} Server Stopped", Settings.Default.Instance),
                                                                      LogDestinations.EventLog | LogDestinations.TextFile);
                    }

                    stopTime = DateTime.Now;
                }

                MDCServer.Dispose();
                MDCServer = null;

                DBController.Dispose();
                DBController = null;
            }
            catch (Exception ex)
            {
                // Log Exceptional Message
                if (MDCServer.Activity_DataLogger != null)
                {
                    MDCServer.Activity_DataLogger.WriteError(ex);
                }
                else
                {
                    MDC.Default_DataLogger.WriteError(ex, LogDestinations.EventLog | LogDestinations.TextFile);
                }
            }
            finally
            {
                SET_Server_Run_Status("Shut Down");
            }
        }

        // private void timer_statistics_Tick(object sender, EventArgs e)
        // {
        //     try
        //     {
        //         timer_statistics.Enabled = false;
        //         Show_MDC_Status_to_GUI();
        //         Update_MDC_Status();
        //     }
        //     catch (Exception)
        //     {
        //         throw;
        //     }
        //     finally
        //     {
        //         timer_statistics.Interval = 120000; //every 2 Minutes
        //         timer_statistics.Enabled = true; 
        //     }
        // }


        private void clearStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DBController.clearStatistics();
        }

        private void openErrorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = LocalCommon.GetApplicationConfigsDirectory() + @"\errors";
            if (Directory.Exists(path))
                Process.Start(path);
        }

        private void openLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = LocalCommon.GetApplicationConfigsDirectory() + @"\logs";
            if (Directory.Exists(path))
                Process.Start(path);
        }

        #endregion


    }
}
