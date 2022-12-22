using DLMS;
using ServerToolkit.BufferManagement;
using SharedCode.Comm.DataContainer;
using SharedCode.Controllers;
using SharedCode.TCP_Communication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using SharedCode.Common;
using System.Linq;

namespace SharedCode.Comm.HelperClasses
{
    public delegate byte[] CrateIOBuffer(int MaxBufSize);

    public delegate IBuffer GetDataReaderBuffer(int BufferLength);
    public delegate void ConnectionCreated(IOConnection con);

    public class ConnectionManager : IDisposable
    {
        #region Data_Members

        public static readonly TimeSpan MinConnnectionAllocateTime = TimeSpan.FromSeconds(30);
        public static readonly TimeSpan DefaultTCPInactivityTimeOut = TimeSpan.FromMinutes(1f);
        public static readonly TimeSpan MaxKAConnectionCacheResetDuration = TimeSpan.FromHours(06);
        public static readonly ParallelOptions ConnStatusRunnerDefault = new ParallelOptions { MaxDegreeOfParallelism = 15 };

        public event Action<IOConnection, String> IOChannelDisconnect = delegate { };
        public event Action<IOConnection, IOActivityType> IOActivity = delegate { };
        public event Action<IOConnection> ConnectionReceived = delegate { };
        public event Action<IOConnection, HeartBeat> HeartBeatReceived = delegate { };

        public event ConnectionCreated NewConnectionAdded = delegate { };

        private TCPConController TCPIPConnController_Obj;
        private ConnectionsList ConnectionList;

        private ConnectionsList disconnectedList;
        private IOConnectionCache _connectionCahce;

        private WrapperLayer _TCPWrapper;
        private TimeSpan _LastKAConnectionCacheReset = DateTime.Now.TimeOfDay;
        private TimeSpan _TCPInactivityDuration = new TimeSpan(0, 0, 3, 0, 0); //2:30 Min default inactivity
        private bool enable_TCPInactivity = true;
        private short maxDataReaderThreadCount = 100;
        private int maxWriteBuf = 1024;
        private int maxReadBuf = 1024;
        private int maxIOConnection = 5000;
        private int maxDisconnected = 20;

        private TimeSpan dataReadTimeOut = new TimeSpan(0, 0, 2, 30, 0);
        private TimeSpan tcpListenerIdleTime = new TimeSpan(0, 0, 0, 0, 100);

        private HeartBeat heartBeatReceived;
        private ConnectionInfo connectionInfo;
        private ConnectionInfo lastConnectionInfo;

        private Thread _ConnectionStatusRunner = null;

        private GetDataReaderBuffer _CreateDataReaderBuffer;
        private OpticalPortController opticalPortConn;
        private IOConnectionMonitor connectionMonitor;
        private DLMSLogger logger;
        private TCPConController _tcpConnectionController;

        #endregion

        #region Properties

        public TCPConController TCP_CONController
        {
            get { return _tcpConnectionController; }
            set { _tcpConnectionController = value; }
        }

        public DLMSLogger IOTrafficLogger
        {
            get { return logger; }
            set { logger = value; }
        }

        public IOConnectionMonitor ConnectionMonitor
        {
            get { return connectionMonitor; }
            set { connectionMonitor = value; }
        }

        public OpticalPortController OpticalPortConnection
        {
            get { return opticalPortConn; }
        }

        public TCPConController TCPIPConnController_OBJ
        {
            get { return TCPIPConnController_Obj; }
            set { TCPIPConnController_Obj = value; }
        }

        public WrapperLayer TCPWrapper
        {
            get { return _TCPWrapper; }
            set
            {
                _TCPWrapper = value;
            }
        }

        public TimeSpan DataReadTimeOut
        {
            get { return dataReadTimeOut; }
            set
            {
                dataReadTimeOut = value;
                if (ConnectionList != null && ConnectionList.Count > 0)
                {
                    foreach (var item in ConnectionList)
                    {
                        item.Value.DataReadTimeOut = dataReadTimeOut;
                    }
                }
            }
        }


        /// <summary>
        /// Checks Is there any single connected physical connection available
        /// </summary>
        public bool IsConnected
        {
            get
            {
                bool isConnected = false;
                try
                {
                    isConnected = ConnectionList.IsConnected;
                }
                catch
                { }
                return isConnected;
            }
        }

        public HeartBeat LastHeartBeatReceived
        {
            get { return heartBeatReceived; }
            set { heartBeatReceived = value; }
        }

        public ConnectionInfo ConnectionInfo
        {
            get { return connectionInfo; }
            set { connectionInfo = value; }
        }

        public ConnectionInfo LastConnectionInfo
        {
            get { return lastConnectionInfo; }
            set { lastConnectionInfo = value; }
        }

        public int MaxReadBuffer
        {
            get { return maxReadBuf; }
            set { maxReadBuf = value; }
        }

        public int MaxWriteBuffer
        {
            get { return maxWriteBuf; }
            set { maxWriteBuf = value; }
        }

        public int MaxIOConnection
        {
            get { return maxIOConnection; }
            set { maxIOConnection = value; }
        }

        public int MaxDisconnected
        {
            get { return maxDisconnected; }
            set { maxDisconnected = value; }
        }

        public ConnectionsList DisconnectedIOList
        {
            get { return disconnectedList; }
            set { disconnectedList = value; }
        }

        public IOConnectionCache ConnectionCahce
        {
            get { return _connectionCahce; }
            private set { _connectionCahce = value; }
        }

        public TimeSpan TcpListenerProcessIdleTime
        {
            get { return tcpListenerIdleTime; }
            set { tcpListenerIdleTime = value; }
        }

        public short MaxDataReaderThreadCount
        {
            get { return maxDataReaderThreadCount; }
            set { maxDataReaderThreadCount = value; }
        }

        public TimeSpan TCPInactivityDuration
        {
            get { return _TCPInactivityDuration; }
            set
            {
                if (_TCPInactivityDuration < DefaultTCPInactivityTimeOut)
                    _TCPInactivityDuration = DefaultTCPInactivityTimeOut;
                else
                    _TCPInactivityDuration = value;
            }
        }

        public bool Enable_TCPInactivity
        {
            get { return enable_TCPInactivity; }
            set { enable_TCPInactivity = value; }
        }

        public ConnectionsList IOConnectionsList
        {
            get { return ConnectionList; }
        }

        public GetDataReaderBuffer CreateDataReaderBuffer
        {
            get { return _CreateDataReaderBuffer; }
            set { _CreateDataReaderBuffer = value; }
        }

        #endregion

        public ConnectionManager()
        {
            TCP_CONController = new TCPConController();
            TCP_CONController.TCPClientConnected += new Action<Socket>(TCP_CONController_TCPClientConnected);
            ConnectionList = new ConnectionsList();

            TCP_CONController.TCPServer.ConnectionList = ConnectionList;
            ConnectionMonitor = new IOConnectionMonitor();
            opticalPortConn = new OpticalPortController();
            ConnectionList = new ConnectionsList();
            DisconnectedIOList = new ConnectionsList();
            _connectionCahce = new IOConnectionCache(TimeSpan.FromMinutes(05), TimeSpan.FromMinutes(30),
                new IOConnectionGetter(GetConnectedIOConnection_Helper));
            TCPWrapper = new WrapperLayer(0x01, 0x10);
            if(Commons.IsMDC)
            ResetConnectionStatusRunner();

            // Donot use ASyncTimeOuts
            // _DataReadWriteTimeOuts = new AsyncTimeOuts();
            // RegisterWaitHandlerDelegate = new RegisterWaitHandle(RegisterWaitHandle);
            // UnRegisterWaitHandlerDelegate = new UnRegisterWaitHandle(UnRegisterWaitHandle);

        }

        #region Event_Handlers

        private byte[] CrateIOBuffer(int MaxBufSize)
        {
            try
            {
                byte[] Tarray = null;
                try
                {
                    Tarray = (byte[])Array.CreateInstance(typeof(Byte), MaxBufSize);
                    return Tarray;
                }
                catch (Exception ex)
                {

                }
                ///If Exception raise then clear Up buffers
                ConnectionList.CleanUpBuffer();
                Tarray = (byte[])Array.CreateInstance(typeof(Byte), MaxBufSize);
                return Tarray;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// New Client Connection Is Received Notification
        /// </summary>
        public void TCP_CONController_TCPClientConnected(Socket tcpClientSocket)
        {
            try
            {
                ///Enforce TCP Socket Connection Later Policy
                // Single Remote IP Per Connection
                // Allow Max Number of Connections
                // Frequently Disconnected/Disconnected IPs

                if (ConnectionList.Count < MaxIOConnection)
                {
                    TCPStream tcpStream = new TCPStream(tcpClientSocket, true);
                    tcpStream.BufferLength = MaxReadBuffer;

                    IOConnection IOConn = GetIOConnection(tcpStream);
                    IOConn.CurrentConnection = PhysicalConnectionType.NonKeepAlive;
                    /// IOConn.DataReadTimeOut = DataReadTimeOut;

                    // Check Either Already Socket Already Added
                    if (!ConnectionList.IsConnectionDuplicated(IOConn))
                        AddIOConnection(IOConn);
                    else
                    {

#if   Enable_DEBUG_ECHO

                        //Modification Comment Message
                        //Common.WriteLine(String.Format("*******Connection Duplicated {0}*******", IOConn));

#endif
                        return;
                    }

                    #region Invoke Event Asyn

                    ///ConnectionReceived.Invoke(IOConn);
                    //Delegate[] InvokeList = ConnectionReceived.GetInvocationList();
                    //foreach (Action<IOConnection> eventHandler in InvokeList)
                    //{
                    //    var _Action = new Action<IOConnection>(eventHandler);
                    //    _Action.BeginInvoke(IOConn, null, IOConn);
                    //} 

                    #endregion

                    // Notification Notifier = new Notification("TCP Connection Received", tcpClientSocket.RemoteEndPoint.ToString());
                    // logger.LogAPMessage("TCP Connection Received " + tcpClientSocket.RemoteEndPoint.ToString(), PacketType.UNKNOWN);
                }
                else
                {
                    // TCP_CONController.AllowTCPConnection = false;
                    tcpClientSocket.Close();
                    tcpClientSocket.Dispose();
                }
            }
            catch (Exception ex)
            {
#if Enable_DEBUG_ECHO

                // TCP_CONController.AllowTCPConnection = false;
                Commons.WriteLine(String.Format("Error Adding...Connection {0}_{1}", tcpClientSocket, ex.Message));

#endif
            }
            finally
            {
                try
                {
                    #region Debugging & Logging

#if Enable_DEBUG_ECHO

                    Console.Out.WriteLine(String.Format("TCP Connection Received {0}", tcpClientSocket));

#endif

#if Enable_Transactional_Logging

                    string path = Commons.GetApplicationConfigsDirectory() + @"AllocationLogs\ConnectionReceivedLogs.txt";
                    StringBuilder strBuilder = new StringBuilder("TCP Conn Received");
                    strBuilder.AppendFormat("IOConn:{0} TimeStamp:{1} \r\n", tcpClientSocket.RemoteEndPoint, DateTime.Now.TimeOfDay);
                    Commons.SaveApplicationLogMessage(strBuilder, path);

#endif

                    #endregion
                }
                catch
                { }
            }
        }

        internal void IOConn_IOActivity(IOConnection arg1, IOActivityType obj)
        {
            try
            {
                IOActivity.Invoke(arg1, obj);
            }
            catch (Exception ex)
            { }
        }

        internal void IOConn_HeartReceived(IOConnection arg1, HeartBeat arg2)
        {
            try
            {
                LastHeartBeatReceived = arg2;
                string hbeatStr = String.Format("MSN:{0},Event Count{1},TimeStamp:{2}",
                    (MeterSerialNumber.ConvertFrom(arg2.MeterSerialNo, 0, 4)).ToString("S"),
                     arg2.EventCounter, arg2.DateTimeStamp);
                // logger.LogAPMessage(String.Format("Heart Beat received {0}", hbeatStr),  PacketType.UNKNOWN);
            }
            catch
            { }
        }

        internal void IOConn_IOChannelDisconnect(IOConnection arg1, string arg2)
        {
            try
            {
                //if (ConnectionList.ContainsKey(arg1.MeterSerialNumberObj))
                //    ConnectionList.Remove(arg1);
                // Remove IOConnection Object from IOConnectionCache
                if (arg1.ConnectionInfo != null &&
                    arg1.MeterSerialNumberObj != null)
                    ConnectionCahce.TryRemoveIOConnection(arg1.MeterSerialNumberObj);     // by Sha

                // DisconnectedIOList.Remove(arg1);
                // if (DisconnectedIOList.Count > MaxDisconnected && MaxDisconnected > 0)
                // {
                //     DisconnectedIOList.RemoveAt(DisconnectedIOList.Count - 1);
                // }
                // DisconnectedIOList.Insert(0, arg1);

                #region // Invoke Events Asyn

                Delegate[] EventHandlers = IOChannelDisconnect.GetInvocationList();
                foreach (Action<IOConnection, String> handler in EventHandlers)
                {
                    var Action = new Action<IOConnection, String>(handler);
                    Action.BeginInvoke(arg1, arg2, null, arg1);
                }

                #endregion
                arg1.Dispose();
            }
            catch
            { }
        }

        #endregion

        #region Member_Methods

        public void ConnectHDLC_ModeE(String selectedComPort, ushort ServerSAP, ushort ClientSAP)
        {
            try
            {
                if (String.IsNullOrEmpty(selectedComPort))
                    throw new Exception("Invalid COM Port Selected");
                if (!opticalPortConn.HDLCConnection_.Connected)             ///Connect If Not Already
                {
                    opticalPortConn.ModeEController.Retry = 2;
                    opticalPortConn.ModeEController.RegisterCode = "MTI";
                    if (opticalPortConn.EstablishModeE(selectedComPort))    ///If True Mode E Established
                    {
                        //TO DO: //For Model, Device Address should be Use
                        //opticalPortConn.ModeEController.DeviceAddress
                        opticalPortConn.HDLCConnection_.DestinationSAP = ServerSAP;
                        opticalPortConn.HDLCConnection_.SourceAddress = ClientSAP;
                        opticalPortConn.ConnectHdlc();
                    }
                }
                else
                    throw new Exception("Unable To Connect,already in HDLC Mode");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConnectDirectHDLC(String selectedComPort, int baudRate, ushort ServerSAP, ushort ClientSAP)
        {
            try
            {
                if (String.IsNullOrEmpty(selectedComPort))
                    throw new Exception("Invalid COM Port Selected");
                if (!opticalPortConn.HDLCConnection_.Connected)             ///Connect If Not Already
                {
                    opticalPortConn.HDLCConnection_.DestinationSAP = ServerSAP;
                    opticalPortConn.HDLCConnection_.SourceAddress = ClientSAP;
                    opticalPortConn.ConnectDirectHDLC(selectedComPort, baudRate);
                }
                else
                    throw new Exception("Unable To Connect,HDLC already connected");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool GetIsConnected(MeterSerialNumber mtrSerial)
        {
            bool isConnected = false;
            try
            {
                IOConnection Conn = GetConnectedIOConnection(mtrSerial);
                if (Conn != null && Conn.IsChannelConnected)
                    isConnected = true;
                return isConnected;
            }
            catch
            {
                return false;
            }
        }

        public IOConnection GetConnectedIOConnection_Helper(MeterSerialNumber mtrSerial)
        {
            IOConnection isConnected = null;
            try
            {
                IOConnection conn = IOConnectionsList[mtrSerial];
                if (conn != null && conn.IsChannelConnected)
                {
                    isConnected = conn;
                }
                return isConnected;
            }
            catch
            {
                return isConnected;
            }
        }

        public IOConnection GetConnectedIOConnection(MeterSerialNumber mtrSerial)
        {
            IOConnection isConnected = null;
            try
            {
                isConnected = ConnectionCahce.GetIOConection(mtrSerial);
                return isConnected;
            }
            catch
            {
                return isConnected;
            }
        }

        public IOConnection GetConnectedIOConnection(string mtrSerial)
        {
            IOConnection isConnected = null;
            try
            {
                MeterSerialNumber SrToMatch = MeterSerialNumber.ConvertFrom(mtrSerial);
                isConnected = ConnectionCahce.GetIOConection(SrToMatch);
                return isConnected;
            }
            catch
            {
                return isConnected;
            }
        }

        public void Disconnect()
        {
            try
            {
                if (ConnectionList != null)
                {
                    IOConnectionsList.Disconnect();
                    IOConnectionsList.Clear();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConnectionStatusRunner()
        {
            try
            {
                while (true)
                {
                    TimeSpan T1 = DateTime.Now.TimeOfDay;
                    var ConnectedMeters = ConnectionList.Keys;
                    try
                    {
                        if (Enable_TCPInactivity)
                            //for (int index = 0; index < ConnectionList.Count; index++)
                            //TODO:Modification
                            Parallel.ForEach(ConnectedMeters, ConnStatusRunnerDefault,
                        (meter) =>
                        {
                            IOConnection Conn = ConnectionList[meter];
                            if (Conn != null)
                            {
                                // Only Check UnAssigned Meters Only
                                if (!Conn.IsAssigned ||
                                    (Conn.IsAssigned &&
                                     ((Conn.TCPWrapperStream.OverlayMode) ||   // KeepAlive Meter HeartBeatProcess
                                       Conn.TCPWrapperStream.CommunicationMode == CommunicationMode.IdleAliveMode)))    // Overlay TCP_Stream Process
                                {
                                    // Process HeartBeat Here
                                    if (Conn.IsChannelConnected &&
                                        ((Conn.TCPWrapperStream.OverlayMode) ||  // Overlay TCP_Stream Process KeepAlive Meter HeartBeatProcess
                                          Conn.TCPWrapperStream.CommunicationMode == CommunicationMode.IdleAliveMode))
                                    {
                                        #region Process Data Available On Channel

                                        if (Conn.IOStream is TCPStream &&
                                            Conn.TCPWrapperStream.DataAvailable)
                                        {
                                            Conn.TCPWrapperStream.TryProcessReceiveDataASync();
                                        }

                                        #endregion
                                    }
                                    // Test TCP InActivity Here
                                    else if (Conn.LastIOActivityDuration >= TCPInactivityDuration &&
                                        Conn.LastIOActivityDuration != TimeSpan.MaxValue)
                                    {
                                        // Conn.Disconnect();
                                        ConnectionList.Remove(Conn);
                                    }
                                    else if (!Conn.IsChannelConnected)
                                    {
                                        Conn.Disconnect();
                                        ConnectionList.Remove(Conn);
                                    }
                                }
                            }
                        }
                        );
                    }
                    catch (Exception) { }
                    #region Sleep Idle Thread

                    if (DateTime.Now.TimeOfDay.Subtract(T1) < MinConnnectionAllocateTime)
                        Thread.Sleep(TimeSpan.FromSeconds(2.5));

                    #endregion
                }
            }
            catch (ThreadAbortException ex)
            {
                throw ex;
                //string path = LocalCommon.GetApplicationErrorDirectory() + @"\Exceptions\ConnectionStatusRunner.txt";
                //Commons.SaveException(ex, path);
            }
            catch (Exception ex)
            {
                //string path = Commons.GetApplicationErrorDirectory() + @"\Exceptions\ConnectionStatusRunner.txt";
                //Commons.SaveException(ex, path);
                ResetConnectionStatusRunner();
                throw ex;
            }
        }

        #endregion

        #region Support_Methods

        /// <summary>
        /// Create New IOConnection Based Upon Configuration In Connection Manager
        /// </summary>
        /// <param name="IOStream"></param>
        /// <returns></returns>
        public IOConnection GetIOConnection(Stream IOStream)
        {
            try
            {
                // Init Connection Based On Settings
                IOConnection conn = new IOConnection(IOStream);
                conn.DataReadTimeOut = this.DataReadTimeOut;
                conn.MaxReadBufferSize = MaxReadBuffer;
                conn.IOStream = IOStream;
                //conn.IOTrafficLogger = this.IOTrafficLogger;
                conn.IOTrafficLogger = null;
                conn.TCPWrapper = this.TCPWrapper;
                conn.IOBufferCreater = new CrateIOBuffer(CrateIOBuffer);

                #region Set_ReadBufferSize

                PropertyInfo prop = IOStream.GetType().GetProperty("ReadBufferSize", BindingFlags.Public | BindingFlags.Instance);
                if (null != prop && prop.CanWrite)
                {
                    prop.SetValue(IOStream, MaxReadBuffer, null);
                }

                #endregion

                return conn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddIOConnection(IOConnection conn)
        {
            try
            {
                ///Assign Random MeterSerialNumber
                MeterSerialNumber MSN = null;
                if (conn.MSN == null)
                    do
                    {

                        MSN = ConnectionInitUtil.GetRandomSerialNumber();
                        if (!ConnectionList.ContainsKey(MSN))
                        {
                            break;
                        }
                        else
                            continue;

                    } while (true);

                if (conn.MeterSerialNumberObj == null)
                    conn.MeterSerialNumberObj = MSN;
                int tmpBufferSize = 1024;
                conn.InitBuffer(tmpBufferSize, tmpBufferSize, this.CreateDataReaderBuffer);
                ConnectionList.TryAdd(conn.MeterSerialNumberObj, conn);
                if (NewConnectionAdded != null) NewConnectionAdded(conn);

                // Attach Event Handlers
                conn.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ConnectionInfo_PropertyChanged);
                conn.ConnectionInfo.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ConnectionInfo_PropertyChanged);
                conn.IOChannelDisconnect += new Action<IOConnection, string>(IOConn_IOChannelDisconnect);

                // Register Wait Handlers Here
                // conn.RegisterWaitHandleDelegate = RegisterWaitHandlerDelegate;
                // conn.UnRegisterWaitHandleDelegate = UnRegisterWaitHandlerDelegate;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConnectionInfo_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                // Check Exists IP Connection With Duplicate Serial Number
                if (sender is IOConnection && e.PropertyName.Equals("ConnectionInfo", StringComparison.OrdinalIgnoreCase))
                {
                    IOConnection Conn = (IOConnection)sender;
                    if (Conn != null && Conn.ConnectionInfo != null)
                    {
                        Conn.ConnectionInfo.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ConnectionInfo_PropertyChanged);
                        // if (Conn.MeterSerialNumberObj != null && Conn.MeterSerialNumberObj.IsMSN_Valid)
                        //     RemoveDuplicateConnections(IOConnectionsList, Conn.MeterSerialNumberObj, true);
                    }
                }
                else if (sender is IOConnection && (e.PropertyName.Equals("MeterSerialNumberObj", StringComparison.OrdinalIgnoreCase)
                    || e.PropertyName.Equals("MeterSerialNumberObj", StringComparison.OrdinalIgnoreCase)))
                {
                    IOConnection ConInfo = (IOConnection)sender;
                    if (ConInfo.MeterSerialNumberObj != null && ConInfo.MeterSerialNumberObj.IsMSN_Valid)
                        RemoveDuplicateConnections(IOConnectionsList, ConInfo.MeterSerialNumberObj, true);
                    MeterSerialNumber OldVal = null;
                    if (e is PropertyChangedEventArgs<MeterSerialNumber>)
                    {
                        OldVal = ((PropertyChangedEventArgs<MeterSerialNumber>)e).PreviousValue;
                    }
                    // Try To Update MeterSerialNumber Key
                    ConnectionList.UpdateIOConnection(ConInfo, OldVal);
                }

                #region Commented Code Section

                // else if (sender is IOConnection &&
                //         (e.PropertyName.Equals("OverlayStream", StringComparison.OrdinalIgnoreCase)
                //         || e.PropertyName.Equals("OverlayStream", StringComparison.OrdinalIgnoreCase)))
                // {
                //     IOConnection ConInfo = (IOConnection)sender;

                //     if (ConInfo != null)
                //     {
                //         var HDLC_Disconnect = new Action(ConInfo.HdlcProtocol_HDLCDisconnected);
                //         // Register HDLC Disconnect Event
                //         if (ConInfo.OverlayStream != null && ConInfo.OverlayStream is _HDLC.HDLCStream)
                //             ((_HDLC.HDLCStream)ConInfo.OverlayStream).HdlcProtocol.HDLCDisconnected += new Action(ConInfo.HdlcProtocol_HDLCDisconnected);
                //     }
                // } 

                #endregion
            }
            catch (Exception ex)
            {

            }
        }

        public void RemoveDuplicateConnections(ConnectionsList ConnList, MeterSerialNumber MeterSerialNumberObj, bool ConnStatus)
        {
            try
            {
                //Extract IOConnections From Owner.ConnectedMeterList
                var Iterator = from Conn in ConnList
                               where Conn.Value != null &&
                                //!Conn.Value.IsAssigned &&
                                (Conn.Value.MeterSerialNumberObj != null &&
                                ((IComparable<MeterSerialNumber>)Conn.Value.MeterSerialNumberObj).CompareTo(MeterSerialNumberObj) == 0)
                               orderby Conn.Value.ConnectionTime ascending
                               select Conn.Value;
                List<IOConnection> srConn = Iterator.ToList<IOConnection>();

                //List<IOConnection> srConn = ConnList.All((x) => (x.ConnectionInfo != null && x.MeterSerialNumberObj != null) ?
                //    ((IComparable<MeterSerialNumber>)x.MeterSerialNumberObj).CompareTo(MeterSerialNumberObj) == 0 : false);

                ///Detect Duplicates & Remove
                if (srConn != null && srConn.Count > 1)
                {
                    srConn.Sort((x, y) => x.ConnectionTime.CompareTo(y.ConnectionTime));
                    bool disconn = false;
                    for (int index = srConn.Count - 1; index >= 0; index--)
                    {
                        if (srConn[index].IsConnected == ConnStatus && !disconn)
                        {
                            disconn = true;
                            continue;
                        }
                        else if (srConn[index].IsConnected != ConnStatus)
                        {
                            //Remove Disconnected
                            IOConnectionsList.Remove(srConn[index]);
                            //Commented ConnStatus
                            //srConn[index].InsertLogMessage("RemoveDuplicateConnections_ConnectionManager");
                            srConn[index].Disconnect();
                            srConn[index].Dispose();
                        }
                        if (disconn)
                        {
                            IOConnectionsList.Remove(srConn[index]);
                            srConn[index].Disconnect();
                            srConn[index].Dispose();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error removing connections", ex);
            }
        }



        #endregion

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ResetConnectionStatusRunner()
        {
            try
            {
                if (_ConnectionStatusRunner != null)
                {
                    ///Dispose Old Thread
                    _ConnectionStatusRunner.Abort();
                    _ConnectionStatusRunner = null;
                }
                GC.Collect();

                _ConnectionStatusRunner = new Thread(ConnectionStatusRunner);
                _ConnectionStatusRunner.Priority = ThreadPriority.AboveNormal;
                _ConnectionStatusRunner.Start();
            }
            catch (Exception ex)
            { }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void StopConnectionStatusRunner()
        {
            try
            {
                if (_ConnectionStatusRunner != null)
                {
                    //Dispose Old Thread
                    _ConnectionStatusRunner.Abort();
                    _ConnectionStatusRunner = null;
                }
                GC.Collect();
            }
            catch
            { }
        }

        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                StopConnectionStatusRunner();



                if (IOConnectionsList != null)
                {
                    IOConnectionsList.Disconnect();
                    ConnectionList = null;
                }
                DisconnectedIOList = null;
                if (_connectionCahce != null)
                {
                    _connectionCahce.Dispose();
                    _connectionCahce = null;
                }
                //Remove All Event Handlers
                #region //Remove ConnectionReceived Event Handlers

                Delegate[] Handlers = null;
                if (this.ConnectionReceived != null)
                {
                    Handlers = ConnectionReceived.GetInvocationList();
                    foreach (Delegate item in Handlers)
                    {
                        ConnectionReceived -= (Action<IOConnection>)item;
                    }
                }

                #endregion
                #region //Remove IOActivity Event Handlers

                Handlers = null;
                if (this.IOActivity != null)
                {
                    Handlers = IOActivity.GetInvocationList();
                    foreach (Delegate item in Handlers)
                    {
                        IOActivity -= (Action<IOConnection, IOActivityType>)item;
                    }
                }

                #endregion
                #region //Remove IOChannelDisconnect Event Handlers

                Handlers = null;
                if (this.IOChannelDisconnect != null)
                {
                    Handlers = IOChannelDisconnect.GetInvocationList();


                    foreach (Delegate item in Handlers)
                    {
                        IOChannelDisconnect -= (Action<IOConnection, String>)item;
                    }
                }

                #endregion
                #region //Remove HeartBeatReceived Event Handlers

                Handlers = null;
                if (this.HeartBeatReceived != null)
                {
                    Handlers = HeartBeatReceived.GetInvocationList();
                    foreach (Delegate item in Handlers)
                    {
                        HeartBeatReceived -= (Action<IOConnection, HeartBeat>)item;
                    }
                }

                #endregion
            }
            catch
            {
            }
        }

        #endregion

        ~ConnectionManager()
        {
            try
            {
                Dispose();
            }
            catch
            { }
        }
    }

    //public delegate byte[] CrateIOBuffer(int MaxBufSize);
    //public delegate void RegisterWaitHandle(BaseWaitToken WaitToken);
    //public delegate void UnRegisterWaitHandle(BaseWaitToken WaitToken);
    //public delegate IBuffer GetDataReaderBuffer(int BufferLength);

    //public class ConnectionManager_o : IDisposable
    //{
    //    #region Data_Members

    //    public event Action<IOConnection, String> IOChannelDisconnect = delegate { };
    //    public event Action<IOConnection, IOActivityType> IOActivity = delegate { };
    //    public event Action<IOConnection> ConnectionReceived = delegate { };
    //    public event Action<IOConnection, HeartBeat> HeartBeatReceived = delegate { };

    //    private OpticalPortController opticalPortConn;
    //    private TCPConController _tcpConnectionController;
    //    private ConnectionsList ConnectionList;
    //    private ConnectionsList disconnectedList;
    //    private WrapperLayer _TCPWrapper;
    //    private int maxReadBuf = 1024;
    //    private short maxDataReaderThreadCount = 75;
    //    private int maxWriteBuf = 1024;
    //    private int maxIOConnection = 50000;
    //    private int maxDisconnected = 20;

    //    private TimeSpan dataReadTimeOut = new TimeSpan(0, 0, 2, 0, 0);
    //    private TimeSpan tcpListenerIdleTime = new TimeSpan(0, 0, 0, 0, 100);

    //    private HeartBeat heartBeatReceived;
    //    private ConnectionInfo connectionInfo;
    //    private ConnectionInfo lastConnectionInfo;
    //    private IOConnectionMonitor connectionMonitor;
    //    private DLMSLogger logger;

    //    private Thread _DataAvailPollThread = null;

    //    private AsyncTimeOuts _DataReadWriteTimeOuts = null;
    //    readonly internal RegisterWaitHandle RegisterWaitHandlerDelegate = null;
    //    readonly internal UnRegisterWaitHandle UnRegisterWaitHandlerDelegate = null;

    //    #endregion

    //    #region Properties

    //    public WrapperLayer TCPWrapper
    //    {
    //        get { return _TCPWrapper; }
    //        set
    //        {
    //            _TCPWrapper = value;
    //            TCP_CONController.TcpWrapper = value;
    //        }
    //    }
    //    public OpticalPortController OpticalPortConnection
    //    {
    //        get { return opticalPortConn; }
    //    }
    //    public TCPConController TCP_CONController
    //    {
    //        get { return _tcpConnectionController; }
    //        set { _tcpConnectionController = value; }
    //    }
    //    public TimeSpan DataReadTimeOut
    //    {
    //        get { return dataReadTimeOut; }
    //        set
    //        {
    //            dataReadTimeOut = value;
    //            if (ConnectionList != null && ConnectionList.Count > 0)
    //            {
    //                foreach (var item in ConnectionList)
    //                {
    //                    item.DataReadTimeOut = dataReadTimeOut;
    //                }
    //            }
    //        }
    //    }
    //    /// <summary>
    //    /// Checks Is there any single connected physical connection available
    //    /// </summary>
    //    public bool IsConnected
    //    {
    //        get
    //        {
    //            bool isConnected = false;
    //            try
    //            {
    //                isConnected = ConnectionList.IsConnected;
    //            }
    //            catch (Exception ex)
    //            { }
    //            return isConnected;
    //        }
    //    }
    //    public DLMSLogger IOTrafficLogger
    //    {
    //        get { return logger; }
    //        set { logger = value; }
    //    }
    //    public HeartBeat LastHeartBeatReceived
    //    {
    //        get { return heartBeatReceived; }
    //        set { heartBeatReceived = value; }
    //    }
    //    public ConnectionInfo ConnectionInfo
    //    {
    //        get { return connectionInfo; }
    //        set { connectionInfo = value; }
    //    }
    //    public ConnectionInfo LastConnectionInfo
    //    {
    //        get { return lastConnectionInfo; }
    //        set { lastConnectionInfo = value; }
    //    }
    //    public IOConnectionMonitor ConnectionMonitor
    //    {
    //        get { return connectionMonitor; }
    //        set { connectionMonitor = value; }
    //    }
    //    public int MaxReadBuffer
    //    {
    //        get { return maxReadBuf; }
    //        set { maxReadBuf = value; }
    //    }
    //    public int MaxWriteBuffer
    //    {
    //        get { return maxWriteBuf; }
    //        set { maxWriteBuf = value; }
    //    }
    //    public int MaxIOConnection
    //    {
    //        get { return maxIOConnection; }
    //        set { maxIOConnection = value; }
    //    }
    //    public int MaxDisconnected
    //    {
    //        get { return maxDisconnected; }
    //        set { maxDisconnected = value; }
    //    }
    //    public ConnectionsList IOConnectionsList
    //    {
    //        get { return ConnectionList; }
    //        set { ConnectionList = value; }
    //    }
    //    public ConnectionsList DisconnectedIOList
    //    {
    //        get { return disconnectedList; }
    //        set { disconnectedList = value; }
    //    }
    //    public TimeSpan TcpListenerProcessIdleTime
    //    {
    //        get { return tcpListenerIdleTime; }
    //        set { tcpListenerIdleTime = value; }
    //    }
    //    public short MaxDataReaderThreadCount
    //    {
    //        get { return maxDataReaderThreadCount; }
    //        set { maxDataReaderThreadCount = value; }
    //    }

    //    internal AsyncTimeOuts DataReadWriteTimeOuts
    //    {
    //        get { return _DataReadWriteTimeOuts; }
    //    }

    //    #endregion

    //    public ConnectionManager_o()
    //    {
    //        opticalPortConn = new OpticalPortController();
    //        TCP_CONController = new TCPConController();
    //        TCP_CONController.TCPClientConnected += new Action<Socket>(TCP_CONController_TCPClientConnected);
    //        ConnectionList = new ConnectionsList(MaxIOConnection);
    //        DisconnectedIOList = new ConnectionsList(MaxDisconnected);

    //        TCP_CONController.TCPServer.ConnectionList = ConnectionList;

    //        TCPWrapper = new WrapperLayer(0x01, 0x10);      ///Meter Management Logical Device,Client PUBLIC SAP 

    //        opticalPortConn.ModeEController.Retry = 1;
    //        opticalPortConn.ModeEController.RegisterCode = "ACC";

    //        ConnectionMonitor = new IOConnectionMonitor();

    //        _DataReadWriteTimeOuts = new AsyncTimeOuts();
    //        RegisterWaitHandlerDelegate = new RegisterWaitHandle(RegisterWaitHandle);
    //        UnRegisterWaitHandlerDelegate = new UnRegisterWaitHandle(UnRegisterWaitHandle);
    //    }

    //    public void ConnectHDLC_ModeE(String selectedComPort, ushort ServerSAP, ushort ClientSAP)
    //    {
    //        try
    //        {
    //            if (String.IsNullOrEmpty(selectedComPort))
    //                throw new Exception("Invalid COM Port Selected");
    //            if (!opticalPortConn.HDLCConnection_.Connected)             ///Connect If Not Already
    //            {
    //                opticalPortConn.ModeEController.Retry = 2;
    //                opticalPortConn.ModeEController.RegisterCode = "MTI";
    //                if (opticalPortConn.EstablishModeE(selectedComPort))    ///If True Mode E Established
    //                {
    //                    //TO DO: //For Model, Device Address should be Use
    //                    //opticalPortConn.ModeEController.DeviceAddress
    //                    opticalPortConn.HDLCConnection_.DestinationSAP = ServerSAP;
    //                    opticalPortConn.HDLCConnection_.SourceAddress = ClientSAP;
    //                    opticalPortConn.ConnectHdlc();
    //                }
    //            }
    //            else
    //                throw new Exception("Unable To Connect,already in HDLC Mode");

    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    public void ConnectDirectHDLC(String selectedComPort, int baudRate, ushort ServerSAP, ushort ClientSAP)
    //    {
    //        try
    //        {
    //            if (String.IsNullOrEmpty(selectedComPort))
    //                throw new Exception("Invalid COM Port Selected");
    //            if (!opticalPortConn.HDLCConnection_.Connected)             ///Connect If Not Already
    //            {
    //                opticalPortConn.HDLCConnection_.DestinationSAP = ServerSAP;
    //                opticalPortConn.HDLCConnection_.SourceAddress = ClientSAP;
    //                opticalPortConn.ConnectDirectHDLC(selectedComPort, baudRate);
    //            }
    //            else
    //                throw new Exception("Unable To Connect,HDLC already connected");
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    #region Event_Handlers

    //    private byte[] CrateIOBuffer(int MaxBufSize)
    //    {
    //        try
    //        {
    //            byte[] Tarray = null;
    //            try
    //            {
    //                Tarray = (byte[])Array.CreateInstance(typeof(Byte), MaxBufSize);
    //                return Tarray;
    //            }
    //            catch (Exception ex)
    //            {

    //            }
    //            ///If Exception raise then clear Up buffers
    //            ConnectionList.CleanUpBuffer();
    //            Tarray = (byte[])Array.CreateInstance(typeof(Byte), MaxBufSize);
    //            return Tarray;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    /// <summary>
    //    /// New Client Connection Is Received Notification
    //    /// </summary>
    //    public void TCP_CONController_TCPClientConnected(Socket tcpClientSocket)
    //    {
    //        try
    //        {
    //            ///Enforce TCP Socket Connection Lator Policy
    //            ///Single Remote IP Per Connection
    //            ///Allow Max Number of Connections
    //            ///Frequently Disconnected IPs
    //            if (ConnectionList.Count < MaxIOConnection)
    //            {
    //                //TCP_CONController.AllowTCPConnection = true;
    //                ///Set Socket Level Options
    //                ///tcpClientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
    //                tcpClientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendBuffer, MaxWriteBuffer);
    //                tcpClientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, MaxReadBuffer);

    //                TCPStream tcpStream = new TCPStream(tcpClientSocket, true);
    //                //tcpStream.InActiviteTimeOut = _tcpConnectionController.TCPTimeOut;

    //                IOConnection IOConn = GetIOConnection(tcpStream);
    //                IOConn.CurrentConnection = PhysicalConnectionType.NonKeepAlive;
    //                AddIOConnection(IOConn);
    //                #region Invoke Event Asyn
    //                ConnectionReceived.Invoke(IOConn);
    //                //Delegate[] InvokeList = ConnectionReceived.GetInvocationList();
    //                //foreach (Action<IOConnection> eventHandler in InvokeList)
    //                //{
    //                //    var _Action = new Action<IOConnection>(eventHandler);
    //                //    _Action.BeginInvoke(IOConn, null, IOConn);
    //                //} 
    //                #endregion
    //                //PCL Notification Notifier = new Notification("TCP Connection Received", tcpClientSocket.RemoteEndPoint.ToString());
    //                logger.LogAPMessage("TCP Connection Received " + tcpClientSocket.RemoteEndPoint.ToString(), PacketType.UNKNOWN);
    //            }
    //            else
    //            {
    //                //TCP_CONController.AllowTCPConnection = false;
    //            }
    //            if (_DataAvailPollThread == null || !_DataAvailPollThread.IsAlive)
    //                ResetAvailableDataPolling();
    //        }
    //        catch (Exception ex)
    //        {
    //            //TCP_CONController.AllowTCPConnection = false;
    //            Console.Out.WriteLine(String.Format("Error Adding...Connection {0}", tcpClientSocket));
    //        }

    //    }

    //    internal void IOConn_IOActivity(IOConnection arg1, IOActivityType obj)
    //    {
    //        try
    //        {
    //            //ConnectionMonitor = arg1.ConnectionMonitor;
    //            IOActivity.Invoke(arg1, obj);
    //        }
    //        catch (Exception ex)
    //        { }
    //    }

    //    internal void IOConn_HeartReceived(IOConnection arg1, HeartBeat arg2)
    //    {
    //        try
    //        {
    //            LastHeartBeatReceived = arg2;
    //            string hbeatStr = String.Format("MSN:{0},Event Count{1},TimeStamp:{2}",
    //                (MeterSerialNumber.ConvertFrom(arg2.MeterSerialNo, 0, 4)).ToString("S"),
    //                arg2.EventCounter, arg2.DateTimeStamp);
    //            ///logger.LogAPMessage(String.Format("Heart Beat received {0}", hbeatStr),  PacketType.UNKNOWN);

    //            //Notification Notifier = new Notification(String.Format("MSN {0}", (arg1 != null && arg1.ConnectionInfo != null)?arg1.ConnectionInfo.MSN:"")
    //            //    , String.Format("Heart Beat received {0}", HeartBeatReceived.ToString()));
    //        }
    //        catch (Exception ex)
    //        { }
    //    }

    //    internal void IOConn_IOChannelDisconnect(IOConnection arg1, string arg2)
    //    {
    //        ///throw new NotImplementedException();
    //        try
    //        {
    //            ConnectionList.Remove(arg1);
    //            DisconnectedIOList.Remove(arg1);
    //            if (DisconnectedIOList.Count > MaxDisconnected && MaxDisconnected > 0)
    //            {
    //                DisconnectedIOList.RemoveAt(DisconnectedIOList.Count - 1);
    //            }
    //            DisconnectedIOList.Insert(0, arg1);

    //            #region ///Invoke Events Asyn
    //            Delegate[] EventHandlers = IOChannelDisconnect.GetInvocationList();
    //            foreach (Action<IOConnection, String> handler in EventHandlers)
    //            {
    //                var Action = new Action<IOConnection, String>(handler);
    //                Action.BeginInvoke(arg1, arg2, null, arg1);
    //            }
    //            #endregion
    //            //logger.LogAPMessage(String.Format("TCP Connection disconnected {0},Connected Count {1}", 
    //            //    (arg1.IOStream != null)? arg1.IOStream.ToString():"",ConnectionList.Count),PacketType.UNKNOWN);
    //            //if (arg1 != null && arg1.IsChannelConnected)
    //            //    arg1.Disconnect();
    //            arg1.Dispose();

    //        }
    //        catch (Exception ex)
    //        { }
    //    }

    //    #endregion

    //    #region Member_Methods

    //    public bool GetIsConnected(MeterSerialNumber mtrSerial)
    //    {
    //        bool isConnected = false;
    //        try
    //        {
    //            IOConnection Conn = GetConnectedIOConnection(mtrSerial);
    //            if (Conn != null && Conn.IsChannelConnected)
    //                isConnected = true;
    //            return isConnected;
    //        }
    //        catch (Exception ex)
    //        {
    //            return false;
    //        }
    //    }

    //    public IOConnection GetConnectedIOConnection(MeterSerialNumber mtrSerial)
    //    {
    //        IOConnection isConnected = null;
    //        try
    //        {
    //            if (mtrSerial != null)
    //            {
    //                IOConnection conn = IOConnectionsList.Find((x) => x != null && x.ConnectionInfo != null && x.ConnectionInfo != null
    //                    && mtrSerial.Equals(x.ConnectionInfo.MeterSerialNumberObj));
    //                if (conn != null && conn.IsChannelConnected)
    //                {
    //                    isConnected = conn;
    //                }
    //            }
    //            return isConnected;
    //        }
    //        catch (Exception ex)
    //        {
    //            return isConnected;
    //        }
    //    }

    //    public void Disconnect()
    //    {
    //        try
    //        {
    //            if (ConnectionList != null)
    //            {
    //                IOConnectionsList.Disconnect();
    //                IOConnectionsList.Clear();
    //            }
    //            ///OpticalPortConnection.DisConnectHdlc();
    //            ///TCP_CONController.DisConnectClient();
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    #endregion

    //    #region Support_Methods

    //    /// <summary>
    //    /// Create New IOConnection Based Upon Configuration In Connection Manager
    //    /// </summary>
    //    /// <param name="IOStream"></param>
    //    /// <returns></returns>
    //    public IOConnection GetIOConnection(Stream IOStream)
    //    {
    //        try
    //        {
    //            ///Init Connection Based On Settings
    //            IOConnection conn = new IOConnection(IOStream);
    //            conn.DataReadTimeOut = this.DataReadTimeOut;

    //            conn.IOStream = IOStream;
    //            //conn.IOTrafficLogger = this.IOTrafficLogger;
    //            conn.IOTrafficLogger = null;
    //            conn.TCPWrapper = this.TCPWrapper;
    //            conn.IOBufferCreater = new CrateIOBuffer(CrateIOBuffer);
    //            conn.MaxReadBufferSize = MaxReadBuffer;

    //            #region Set_ReadBufferSize

    //            PropertyInfo prop = IOStream.GetType().GetProperty("ReadBufferSize", BindingFlags.Public | BindingFlags.Instance);
    //            if (null != prop && prop.CanWrite)
    //            {

    //                prop.SetValue(IOStream, MaxReadBuffer, null);
    //            }

    //            #endregion

    //            return conn;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    public void AddIOConnection(IOConnection conn)
    //    {
    //        try
    //        {
    //            ConnectionList.Add(conn);
    //            ///Attach Event Handlers
    //            conn.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ConnectionInfo_PropertyChanged);
    //            conn.ConnectionInfo.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ConnectionInfo_PropertyChanged);
    //            conn.IOChannelDisconnect += new Action<IOConnection, string>(IOConn_IOChannelDisconnect);
    //            //conn.HeartReceived += new Action<IOConnection, HeartBeat>(IOConn_HeartReceived);
    //            //conn.IOActivity += new Action<IOConnection, IOActivityType>(IOConn_IOActivity);

    //            //Set Either Synchronouse Functions Can TimeOut
    //            //if (DataReadTimeOut.Ticks > 0)
    //            //{
    //            //    conn.IOStream.ReadTimeout = Convert.ToInt32(DataReadTimeOut.TotalMilliseconds / 2);
    //            //    conn.IOStream.WriteTimeout = Convert.ToInt32(DataReadTimeOut.TotalMilliseconds / 2);
    //            //}
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    public void ConnectionInfo_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    //    {
    //        try
    //        {
    //            ///Check Exists IP Connection With Duplicate Serial Number
    //            if (sender is IOConnection && e.PropertyName.Equals("ConnectionInfo", StringComparison.OrdinalIgnoreCase))
    //            {
    //                IOConnection Conn = (IOConnection)sender;
    //                if (Conn != null && Conn.ConnectionInfo != null)
    //                {
    //                    Conn.ConnectionInfo.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ConnectionInfo_PropertyChanged);
    //                    if (Conn.ConnectionInfo.MeterSerialNumberObj != null)
    //                        RemoveDuplicateConnections(IOConnectionsList, Conn.ConnectionInfo.MeterSerialNumberObj, true);
    //                }
    //            }
    //            else if (sender is ConnectionInfo && (e.PropertyName.Equals("MeterSerialNumberObj", StringComparison.OrdinalIgnoreCase)
    //                || e.PropertyName.Equals("MeterSerialNumberObj", StringComparison.OrdinalIgnoreCase)))
    //            {
    //                ConnectionInfo ConInfo = (ConnectionInfo)sender;
    //                if (ConInfo.MeterSerialNumberObj != null)
    //                    RemoveDuplicateConnections(IOConnectionsList, ConInfo.MeterSerialNumberObj, true);
    //            }
    //        }
    //        catch (Exception)
    //        { }
    //    }

    //    [MethodImpl(MethodImplOptions.Synchronized)]
    //    public void RemoveDuplicateConnections(ConnectionsList ConnList, MeterSerialNumber MeterSerialNumberObj, bool ConnStatus)
    //    {
    //        try
    //        {
    //            List<IOConnection> srConn = ConnList.FindAll((x) => (x.ConnectionInfo != null && x.ConnectionInfo.MeterSerialNumberObj != null) ?
    //                ((IComparable<MeterSerialNumber>)x.ConnectionInfo.MeterSerialNumberObj).CompareTo(MeterSerialNumberObj) == 0 : false);
    //            ///Detect Duplicates & Remove
    //            if (srConn != null && srConn.Count > 1)
    //            {
    //                srConn.Sort((x, y) => x.ConnectionTime.CompareTo(y.ConnectionTime));
    //                bool disconn = false;
    //                for (int index = srConn.Count - 1; index >= 0; index--)
    //                {
    //                    if (srConn[index].IsConnected == ConnStatus && !disconn)
    //                    {
    //                        disconn = true;
    //                        continue;
    //                    }
    //                    else if (srConn[index].IsConnected != ConnStatus)
    //                    {
    //                        ///Remove Disconnected
    //                        IOConnectionsList.Remove(srConn[index]);
    //                        //srConn[index].InsertLogMessage("RemoveDuplicateConnections_ConnectionManager");
    //                        srConn[index].Disconnect();
    //                        srConn[index].Dispose();
    //                    }
    //                    if (disconn)
    //                    {
    //                        IOConnectionsList.Remove(srConn[index]);
    //                        srConn[index].Disconnect();
    //                        srConn[index].Dispose();
    //                    }
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception("Error removing connections", ex);
    //        }
    //    }

    //    #region AvailableDataPolling

    //    public void AvailableDataPolling()
    //    {
    //        try
    //        {
    //            //Thread Main forever loop
    //            while (true)
    //            {
    //                try
    //                {
    //                    ///To Improve Data Reading Responsiveness
    //                    int till = IOConnectionsList.Count;
    //                    IOConnection[] TArray = IOConnectionsList.ToArray();

    //                    //System.Threading.Tasks.Parallel.For(0, till, (int indexT, System.Threading.Tasks.ParallelLoopState st) =>

    //                    for (int indexT = 0; indexT < till; indexT++)
    //                    {
    //                        IOConnection connInfo = null;
    //                        try
    //                        {
    //                            #region Copy_Selected

    //                            connInfo = null;
    //                            if (indexT < TArray.Length && TArray[indexT] != null)
    //                                connInfo = TArray[indexT];
    //                            else
    //                                continue;

    //                            #endregion

    //                            if (connInfo.CurrentConnection == PhysicalConnectionType.OpticalPort)
    //                                continue;

    //                            if (connInfo.IOStream is TCPStream &&
    //                                connInfo.TCPWrapperStream.CommunicationMode == CommunicationMode.IdleAliveMode &&
    //                                connInfo.IsChannelConnected)
    //                            {
    //                                #region Process Data Available On Channel

    //                                //TCPStream IOSteram = (TCPStream)(connInfo.IOStream);
    //                                //Data Available On TCP Stream Enqueue data read work
    //                                //if (IOSteram.IsbusyReceive && IOSteram.DataAvailable)
    //                                //{
    //                                //    IOSteram.IsbusyReceive = false;
    //                                //}

    //                                if (connInfo.IOStream is TCPStream && connInfo.TCPWrapperStream.DataAvailable)
    //                                {
    //                                    //Try Send HeartBeat Response
    //                                    connInfo.TCPWrapperStream.TryProcessHeartBeat();
    //                                    continue;
    //                                }

    //                                #endregion
    //                                #region Signal IOInactivity

    //                                else if (connInfo.IOStream is TCPStream && !connInfo.TCPWrapperStream.DataAvailable)
    //                                {
    //                                    TimeSpan lastRawDuration = connInfo.TCPWrapperStream.LastRawIODuration;
    //                                    //Software TCPStream Inactivity Occurred
    //                                    if (lastRawDuration != TimeSpan.MaxValue && lastRawDuration > connInfo.TCPWrapperStream.InActiviteTimeOut)
    //                                    {
    //                                        string strMsg = String.Format("TCP Channel Inactivity {0}",
    //                                        (connInfo.IOStream != null) ? connInfo.IOStream.ToString() : null);
    //                                        if (ConnectionList.Contains(connInfo))
    //                                        {
    //                                            IOConn_IOChannelDisconnect(connInfo, strMsg);
    //                                        }
    //                                    }
    //                                }

    //                                #endregion
    //                            }
    //                            #region Signal Connection Disconnect

    //                            else if (!connInfo.IsChannelConnected)
    //                            {
    //                                bool flag = connInfo.IsChannelConnected;
    //                                if (!flag)
    //                                {
    //                                    string strMsg = String.Format("TCP Connection disconnected {0}",
    //                                        (connInfo.IOStream != null) ? connInfo.IOStream.ToString() : null);
    //                                    if (ConnectionList.Contains(connInfo))
    //                                    {
    //                                        IOConn_IOChannelDisconnect(connInfo, strMsg);
    //                                    }
    //                                }
    //                            }

    //                            #endregion

    //                        }
    //                        catch (Exception ex)
    //                        {
    //                            #region Report_Error

    //                            try
    //                            {
    //                                if (connInfo != null && connInfo.IOStream is TCPStream)
    //                                {
    //                                    Exception _InnerException = new IOException("Error occurred while processing received data", ex);
    //                                    ((TCPStream)connInfo.IOStream).InternalException = _InnerException;
    //                                    //((TCPStream)connInfo.IOStream).Signal();
    //                                    continue;
    //                                }
    //                            }
    //                            finally
    //                            {
    //                            }

    //                            #endregion
    //                        }
    //                    }

    //                    //Keep Thread In Sleeping State
    //                    //if (IOConnectionsList.Count >= 50)
    //                    //{
    //                    //    Console.Out.WriteLine(String.Format("Active Thread Pool Size {0},Current Thread Count {1} ",
    //                    //        dataReaderThPool.ActiveThreads, dataReaderThPool.ConcurrentThreads));
    //                    //}

    //                    Thread.Sleep(tcpListenerIdleTime);
    //                }
    //                catch (Exception)
    //                {
    //                    break;
    //                }
    //            }
    //        }
    //        catch (ThreadAbortException ex) { }
    //        catch (Exception ex)
    //        {
    //            Console.Out.WriteLine("" + ex.Message);
    //            ///throw new Exception("Error Polling data on available data connections", ex);
    //        }
    //        finally
    //        {
    //        }
    //    }

    //    public void DataReaderThreadExceptionHandler(Exception ex)
    //    {
    //        try
    //        {
    //            #region //Don't Report Either Exceptions

    //            //if (ex is IOException)
    //            //    return;
    //            //else if (ex is ThreadAbortException)
    //            //{
    //            //    return;
    //            //}
    //            //else if (ex is ThreadInterruptedException)
    //            //{
    //            //    return;
    //            //}
    //            //else if (ex is ObjectDisposedException)
    //            //{
    //            //    return;
    //            //}
    //            //else if (ex is Sonic.Net.ManagedIOCPException)
    //            //{
    //            //    return;
    //            //}

    //            #endregion
    //            //else
    //            {

    //                //Reset Thread Only If The Available Data Pooling Thread Closed or
    //                //Communicator.MTI_MDC.Program.WriteLine("********************Print IO Exception Details***************************");
    //                //Communicator.MTI_MDC.Program.WriteLine(string.Format("{0}", ex));

    //                //if (DataAvailPollThread == null || !DataAvailPollThread.IsAlive || dataReaderThPool == null || !dataReaderThPool.IsValid)
    //                //{
    //                //    ///ResetAvailableDataPolling();
    //                //    Communicator.MTI_MDC.Program.WriteLine("Restart Okay");
    //                //}
    //                //Communicator.MTI_MDC.Program.WriteLine("***************************");

    //            }
    //        }
    //        catch (Exception ex_)
    //        {

    //        }
    //    }

    //    [MethodImpl(MethodImplOptions.Synchronized)]
    //    public void ResetAvailableDataPolling()
    //    {
    //        try
    //        {

    //            if (_DataAvailPollThread != null)
    //            {
    //                ///Dispose Old Thread
    //                while (_DataAvailPollThread.IsAlive)
    //                {
    //                    _DataAvailPollThread.Abort();
    //                }
    //                _DataAvailPollThread = null;
    //            }
    //            ///DataReaderThreadFactory
    //            //dataReaderFactory = new DataReadingTaskFactory();
    //            //if (_DataAvailPollThread != null)
    //            //{
    //            //    _DataAvailPollThread.Close();
    //            //    _DataAvailPollThread = null;

    //            //}
    //            GC.Collect();
    //            //dataReaderThPool = new Sonic.Net.ThreadPool(MaxDataReaderThreadCount, Convert.ToInt16(Math.Ceiling(MaxDataReaderThreadCount / 2.0)),
    //            //  new Sonic.Net.ThreadPool.ThreadPoolThreadExceptionHandler(DataReaderThreadExceptionHandler));

    //            //for (int index = 0; index < pollingTasks.Count; index++)
    //            //{
    //            //    System.Threading.Tasks.Task _t = pollingTasks[index];
    //            //    _t = System.Threading.Tasks.Task.Factory.StartNew(new Action(AvailableDataPolling));
    //            //    pollingTasks[index] = _t;
    //            //}

    //            _DataAvailPollThread = new Thread(AvailableDataPolling);
    //            _DataAvailPollThread.Priority = ThreadPriority.AboveNormal;
    //            ///***Code Modification
    //            _DataAvailPollThread.Start();
    //        }
    //        catch (Exception ex)
    //        { }
    //    }

    //    [MethodImpl(MethodImplOptions.Synchronized)]
    //    public void StopAvailableDataPolling()
    //    {
    //        try
    //        {
    //            if (_DataAvailPollThread != null)
    //            {
    //                ///Dispose Old Thread
    //                _DataAvailPollThread.Abort();
    //                _DataAvailPollThread = null;
    //            }
    //            //DataReaderThreadFactory
    //            //dataReaderFactory = new DataReadingTaskFactory();
    //            //if (dataReaderThPool != null)
    //            //{
    //            //    dataReaderThPool.Close();
    //            //    dataReaderThPool = null;
    //            //}
    //            GC.Collect();
    //        }
    //        catch (Exception ex)
    //        { }
    //    }

    //    #endregion

    //    internal void RegisterWaitHandle(BaseWaitToken WaitToken)
    //    {
    //        try
    //        {
    //            DataReadWriteTimeOuts.RegisterWaitHandle(WaitToken);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    internal void UnRegisterWaitHandle(BaseWaitToken WaitToken)
    //    {
    //        try
    //        {
    //            DataReadWriteTimeOuts.UnRegisterWaitHandle(WaitToken);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    #endregion

    //    #region IDisposable Members

    //    public void Dispose()
    //    {
    //        try
    //        {
    //            if (_tcpConnectionController != null)
    //                _tcpConnectionController.Dispose();
    //            _tcpConnectionController = null;
    //            StopAvailableDataPolling();
    //            if (IOConnectionsList != null)
    //            {
    //                IOConnectionsList.Disconnect();
    //                IOConnectionsList = null;
    //            }
    //            DisconnectedIOList = null;
    //            if (_DataReadWriteTimeOuts != null)
    //                _DataReadWriteTimeOuts.Dispose();

    //        }
    //        catch (Exception)
    //        {
    //        }
    //    }

    //    #endregion

    //    ~ConnectionManager()
    //    {
    //        try
    //        {
    //            Dispose();
    //        }
    //        catch (Exception)
    //        {
    //        }
    //    }
    //}

}
