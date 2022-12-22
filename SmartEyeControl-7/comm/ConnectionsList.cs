/// #define Enable_IO_Logging

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using DLMS;
using TCP_Communication;
using System.Text;
using _HDLC;
using System.Threading.Tasks;
using System.Collections;

namespace comm
{
    public class ConnectionsList : List<IOConnection>
    {
        #region Data_Members
        private int MaxIOConnectionAllowed;
        #endregion

        #region Properties
        public int MaxIOConnections
        {
            get { return MaxIOConnectionAllowed; }
            set { MaxIOConnectionAllowed = value; }
        }

        new public int Count
        {
            get
            {
                lock (this)
                {
                    return base.Count;
                }
            }
        }

        new public IOConnection this[int i]
        {
            get
            {
                return base[i];
            }
            set
            {
                lock (this)
                {
                    this[i] = value;
                }
            }
        }
        #endregion

        #region Constructors

        public ConnectionsList()
            : base()
        {

        }

        public ConnectionsList(int capacity) : base(capacity) { }

        public ConnectionsList(IEnumerable<IOConnection> ConnList) : base(ConnList) { }

        #endregion

        #region Support_Syn Methods

        public bool IsConnectionDuplicated(IOConnection ConnInfo)
        {
            try
            {
                int index_0 = -1;
                index_0 = FindIndex((x) => x != null && ConnInfo != null && ((IComparable<IOConnection>)ConnInfo).CompareTo((IOConnection)x) == 0);


                //if (ConnInfo != null && ConnInfo.IOStream != null && ConnInfo.IOStream is TCPStreamModified)
                //{
                //    index_0 = FindIndex((x) => (x != null) && (((IComparable<TCPStreamModified>)ConnInfo.IOStream).CompareTo(((TCPStream)x.IOStream)) == 0 || (ConnInfo.ConnectionInfo != null && x.ConnectionInfo != null && x.ConnectionInfo.MeterSerialNumberObj != null && x.ConnectionInfo.MeterSerialNumberObj.Equals(ConnInfo.ConnectionInfo.MeterSerialNumberObj))));
                //}
                //else
                //{
                //    index_0 = FindIndex((x) => (x != null && x.IOStream != null) && (x.IOStream.Equals(ConnInfo.IOStream) || (ConnInfo.ConnectionInfo != null && x.ConnectionInfo != null && x.ConnectionInfo.MeterSerialNumberObj != null && x.ConnectionInfo.MeterSerialNumberObj.Equals(ConnInfo.ConnectionInfo.MeterSerialNumberObj))));
                //}
                if (index_0 != -1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        new public void Add(IOConnection conn)
        {
            try
            {
                lock (this)
                {
                    base.Add(conn);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        new public void Insert(int index, IOConnection conn)
        {
            try
            {
                lock (this)
                {
                    ///Remove Duplicate IOConnection OBject
                    base.Insert(index, conn);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        new public void AddRange(IEnumerable<IOConnection> connections)
        {
            foreach (var item in connections)
            {
                Add(item);
            }
        }

        new public void RemoveAt(int index)
        {
            try
            {
                lock (this)
                {
                    base.RemoveAt(index);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        new public void Remove(IOConnection conn)
        {
            try
            {
                lock (this)
                {
                    ///Remove Duplicate IOConnection OBject
                    if (conn != null)
                        base.Remove(conn);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        new public void RemoveAll(Predicate<IOConnection> match)
        {
            try
            {
                lock (this)
                {
                    base.RemoveAll(match);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        new public void Clear()
        {
            try
            {
                lock (this)
                {
                    ///Remove Duplicate IOConnection OBject
                    base.Clear();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        new public void Sort()
        {
            try
            {
                lock (this)
                {
                    ///Remove Duplicate IOConnection OBject
                    base.Sort();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        new public void Sort(IComparer<IOConnection> ComparableDlg)
        {
            try
            {
                lock (this)
                {
                    base.Sort(ComparableDlg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        new public void Sort(int indexStart, int indexEnd, IComparer<IOConnection> ComparableDlg)
        {
            try
            {
                lock (this)
                {
                    base.Sort(indexStart, indexEnd, ComparableDlg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        new public void Sort(Comparison<IOConnection> ComparableDlg)
        {
            try
            {
                lock (this)
                {
                    base.Sort(ComparableDlg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        /// <summary>
        /// Check either any single connected connection exists
        /// </summary>
        public bool IsConnected
        {
            get
            {
                bool isConnected = false;
                try
                {
                    foreach (var conn in this)
                    {
                        if (conn.IsConnected)
                        {
                            isConnected = true;
                            break;
                        }
                    }
                }
                catch (Exception Exception)
                { }
                return isConnected;
            }
        }

        public void Disconnect()
        {
            try
            {
                IOConnection[] connnList = this.ToArray<IOConnection>();
                foreach (var conn in connnList)
                {
                    try
                    {
                        ///***Commented ConnStatus
                        ///conn.InsertLogMessage("Disconnect_ConnectionsList");
                        conn.Disconnect();
                        conn.Dispose();
                    }
                    catch (Exception)
                    {
                    }
                }
                this.Clear();
            }
            catch (Exception ex)
            {

            }
        }

        public void CleanUpBuffer()
        {
            try
            {
                foreach (var connection in this)
                {
                    connection.CleanUpBuffer();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class IOConnection : IComparable<IOConnection>, IConnection, INotifyPropertyChanged
    {
        #region Data_Members

        public event Action<IOConnection, String> IOChannelDisconnect = delegate { };
        //public event Action<IOConnection, IOActivityType> IOActivity = delegate { };
        public event Action<IOConnection, HeartBeat> HeartReceived = delegate { };

        private AsyncCallback callBack = null;

        /// <summary>
        /// Make Rome For Event Notification Packet
        /// </summary>
        private WrapperLayer _TCPWrapper;
        private int MaxReadBuf = 512;
        private TimeSpan dataReadTimeOut;
        private TimeSpan lastIOCmdTime;
        private DateTime connectionTime;
        private List<HeartBeat> heartBeatReceived;
        private byte[] readBuf;
        private CrateIOBuffer ioBufferCreater;
        private Exception _ex;
        private ConnectionInfo connectionInfo;
        private IOConnectionMonitor connectionMonitor;
        private DLMSLogger logger;
        private Stream ioStream;
        private Int32 isAssigned = -1;

        #endregion

        #region Properties

        public Stream IOStream
        {
            get { return ioStream; }
            set { ioStream = value; }
        }

        public TCPStream TCPWrapperStream
        {
            get
            {
                TCPStream InnerStream = null;
                try
                {
                    InnerStream = (TCPStream)IOStream;
                }
                catch (Exception)
                { }
                return InnerStream;
            }
        }

        public WrapperLayer TCPWrapper
        {
            get { return _TCPWrapper; }
            set
            {
                _TCPWrapper = value;
                if (IOStream != null && IOStream is TCPStream)
                {
                    ((TCPStream)IOStream).TcpWrapper = value;
                }
                else if (IOStream is HDLCStream)
                {
                    ;
                }
                else
                    throw new Exception("Unable to set TCP Wrapper,IOConnection not initialized");

            }
        }

        public PhysicalConnectionType CurrentConnection
        {
            get
            {
                if (ConnectionInfo != null)
                    return connectionInfo.ConnectionType;
                ////                 if (IOStream is _HDLC.HDLCStream)
                ////                     return PhysicalConnectionType.OpticalPort;
                ////else 
                //if (IOStream is TCPStreamModified)
                //    return PhysicalConnectionType.NonKeepAlive;
                ////                 else if (IOStream is TCPStreamModifiedOverSerial)
                ////                     return PhysicalConnectionType.OpticalPort;
                else
                    throw new Exception("Unknown Connection Type");

            }
            set
            {
                if (connectionInfo != null)
                    connectionInfo.ConnectionType = value;
            }
        }

        public TimeSpan DataReadTimeOut
        {
            get { return dataReadTimeOut; }
            set { dataReadTimeOut = value; }
        }

        public TimeSpan LastIOActivityTime
        {
            get { return lastIOCmdTime; }
            set { lastIOCmdTime = value; }
        }

        public DateTime ConnectionTime
        {
            get { return connectionTime; }
            set { connectionTime = value; }
        }

        public bool IsConnected
        {
            get
            {
                return IsChannelConnected;
            }
        }

        public bool IsChannelConnected
        {
            get
            {
                try
                {
                    if (IOStream is _HDLC.HDLCStream)
                    {
                        return (((_HDLC.HDLCStream)IOStream).HdlcProtocol.Connected);
                    }
                    else
                        if (IOStream is TCPStream)
                        {

                            return (((TCPStream)IOStream).Connected);
                        }
                        else if (IOStream is TCPStreamOverSerial)
                            return ((TCPStreamOverSerial)IOStream).Connected;
                        else
                            return false;

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public DLMSLogger IOTrafficLogger
        {
            get { return logger; }
            set { logger = value; }
        }

        public ConnectionInfo ConnectionInfo
        {
            get { return connectionInfo; }
            set
            {
                connectionInfo = value;
                NotifyPropertyChanged("ConnectionInfo");
            }
        }

        public IOConnectionMonitor ConnectionMonitor
        {
            get { return connectionMonitor; }
            set { connectionMonitor = value; }
        }

        public byte[] ReadBuffer
        {
            get { return readBuf; }
            set { readBuf = value; }
        }

        public int MaxReadBufferSize
        {
            get { return MaxReadBuf; }
            set { MaxReadBuf = value; }
        }

        public CrateIOBuffer IOBufferCreater
        {
            get { return ioBufferCreater; }
            set { ioBufferCreater = value; }
        }

        public bool IsSyncComplete
        {
            get
            {
                bool isBusy = false;
                if (IOStream is TCPStream)
                {
                    isBusy = ((TCPStream)IOStream).IsSyncComplete;
                }
                else if (IOStream is HDLCStream)
                {
                    isBusy = ((HDLCStream)IOStream).IsSyncComplete;
                }
                return isBusy;
            }
            set
            {
                bool isBusy = value;
                if (IOStream is TCPStream)
                {
                    ((TCPStream)IOStream).IsSyncComplete = value;
                }
                else if (IOStream is HDLCStream)
                {
                    ((HDLCStream)IOStream).IsSyncComplete = value;
                }
                //NotifyPropertyChanged("IsBusy");
            }
        }

        public bool IsDataReadTimeOut
        {
            get
            {
                try
                {
                    if (IsChannelConnected)
                    {
                        TimeSpan tt = DateTime.Now.TimeOfDay.Subtract(lastIOCmdTime);
                        if (tt > DataReadTimeOut)
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public bool IsAssigned
        {
            get
            {
                return Interlocked.Equals(isAssigned, 1);
            }
            set
            {
                Interlocked.Exchange(ref isAssigned, Convert.ToInt32(value));
            }
        }

        public List<HeartBeat> HeartBeats
        {
            get { return heartBeatReceived; }
            set { heartBeatReceived = value; }
        }

        public HeartBeat LastHeartBeat
        {
            get
            {
                if (HeartBeats != null && HeartBeats.Count > 0 && HeartBeats[HeartBeats.Count - 1] != null)
                {
                    return HeartBeats[HeartBeats.Count - 1];
                }
                else
                    return null;
            }
        }

        #endregion

        #region Constructor

        public IOConnection()
        {
            _TCPWrapper = new WrapperLayer(0x01, 0x10);   //Meter Management Logical Device,Client PUBLIC SAP 
            CurrentConnection = PhysicalConnectionType.NonKeepAlive;
            DataReadTimeOut = new TimeSpan(0, 1, 30);
            ConnectionTime = DateTime.Now;
            ConnectionMonitor = new IOConnectionMonitor();
            heartBeatReceived = new List<HeartBeat>(5);
            ///Init Other Objects
            ConnectionInfo = new ConnectionInfo();
        }

        public IOConnection(Stream IOStream)
            : this()
        {
            this.IOStream = IOStream;
            if (IOStream.GetType() == typeof(TCPStream))
            {
                ((TCPStream)IOStream).StreamDisconnected += new Action(IOConnection_StreamDisconnected);
                ((TCPStream)IOStream).HeartBeatReceived += new Action<HeartBeat>(IOConnection_HeartBeatReceived);
                ((TCPStream)IOStream).InActivityTimeOut += new Action<DateTime>(IOConnection_InActivityTimeOut);
            }
            else if (IOStream is HDLCStream)
            {
                ((_HDLC.HDLCStream)IOStream).HdlcProtocol.HDLCDisconnected += new Action(HdlcProtocol_HDLCDisconnected);
            }
        }

        #endregion

        /// <summary>
        /// Locate Stream Object of Current Connected Connection
        /// </summary>
        /// <returns></returns>
        public Stream GetStream()
        {
            try
            {
                if (IOStream != null && IOStream is TCPStream)
                {
                    TCPStream IPIOStream = ((TCPStream)IOStream);
                    IPIOStream.ResetStream();
                }

                return IOStream;
            }
            catch (Exception ex)
            {
                throw new IOException("Error getting IO Stream Object", ex);
            }
        }

        public void Disconnect()
        {
            try
            {
                if (IOStream != null)
                {
                    if (IOStream is HDLCStream)
                    {
                        ((_HDLC.HDLCStream)IOStream).HdlcProtocol.Disconnect();
                        IOStream.Dispose();
                    }
                    else
                    {
                        ///***Commented ConnStatus
                        ///InsertLogMessage(String.Format("Connection Active Disconnect {0} {1}", ToString(), IOStream));
                        bool isConnected = IsChannelConnected;
                        string str = "";

                        if (isConnected)
                        {
                            if (IOChannelDisconnect != null)
                                IOChannelDisconnect.Invoke(this, "TCP client disconnected " + str);
                        }
                        IOStream.Close();
                        ///IOStream.Dispose();
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while closing physical connection", ex);
            }
        }

        public void CleanUpBuffer()
        {
            try
            {
                ReadBuffer = null;
                if (IOStream != null && IOStream is TCPStream)
                {
                    ((TCPStream)IOStream).CleanUpBuffer();
                }
                else if (IOStream != null && IOStream is _HDLC.HDLCStream)
                {
                    ((_HDLC.HDLCStream)IOStream).CleanUpBuffer();
                }
                GC.Collect();

            }
            catch (Exception ex)
            { }
        }

        #region Event Handlers

        internal void HdlcProtocol_HDLCDisconnected()
        {
            try
            {
                if (ConnectionInfo != null)
                    ConnectionInfo.IsConnected = false;
                if (IOChannelDisconnect != null)
                    IOChannelDisconnect.Invoke(this, "IR Port is disconnected");
                IOStream.Close();
                ///Release All Resources
                IOStream.Dispose();
            }
            catch (Exception ex)
            {
            }
        }

        internal void IOConnection_InActivityTimeOut(DateTime obj)
        {
            try
            {
                TimeSpan InActivityTimeOut = new TimeSpan(0, 0, 7, 0, 0);
                if (IOStream is TCPStream)
                {
                    InActivityTimeOut = ((TCPStream)IOStream).InActiviteTimeOut;
                }
                /// Already Connected & Log In Meter
                if (this.IsConnected)
                {
                    IOChannelDisconnect.Invoke(this, String.Format("TCP client disconnected,inactivity period elapsed {0}", IOStream));
                    ///Release All Resources
                    IOStream.Dispose();
                }
                /// In Keep Alive Mode (Check Last Heart Beat Exceeds InActivityTimeOut)
                else
                {
                    if (LastHeartBeat != null)
                    {
                        TimeSpan lastHeartBeatDur = DateTime.Now.Subtract(LastHeartBeat.DateTimeStamp);
                        if (lastHeartBeatDur > InActivityTimeOut)
                        {
                            IOChannelDisconnect.Invoke(this, String.Format("TCP client disconnected,inactivity period elapsed {0}", IOStream));
                            ///Release All Resources
                            IOStream.Dispose();
                        }
                        else    ///Heart Beat Received Before Inactivity
                        {
                            return;
                        }
                    }
                    else   ///Disconnect Channel No Heart Beat Received
                    {
                        IOChannelDisconnect.Invoke(this, String.Format("TCP client disconnected,inactivity period elapsed {0}", IOStream));
                        ///Release All Resources
                        IOStream.Dispose();
                    }
                }
            }
            catch (Exception ex)
            { }
            finally
            {

            }
        }

        internal void IOConnection_HeartBeatReceived(HeartBeat obj)
        {
            try
            {
                if (heartBeatReceived != null)
                {
                    heartBeatReceived.Add(obj);
                    if (heartBeatReceived.Count > 5)
                        heartBeatReceived.RemoveAt(0);

                    if (ConnectionInfo != null && ConnectionInfo.MeterSerialNumberObj == null)
                    {
                        ConnectionInfo.MeterSerialNumberObj = MeterSerialNumber.ConvertFrom(obj.MeterSerialNo, 0, obj.MeterSerialNo.Length);
                    }
                }
                ///Mark Connection Type Keep Alive
                if (ConnectionInfo != null)
                    ConnectionInfo.ConnectionType = PhysicalConnectionType.KeepAlive;
                if (HeartBeats != null)
                    HeartReceived.Invoke(this, obj);

            }
            catch (Exception ex)
            { }
        }

        internal void IOConnection_StreamDisconnected()
        {
            try
            {
                ///***Commented ConnStatus
                ///InsertLogMessage(String.Format("Connection Stream Passive Disconnect {0} {1}", ToString(), IOStream));
                if (IOChannelDisconnect != null)
                    IOChannelDisconnect.Invoke(this, String.Format("TCP client disconnected {0}", IOStream));
                IOStream.Close();
                ///Release All Resources
                IOStream.Dispose();
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        #region IO_Functions Syn_&_ASync

        public int ReceiveDataFromPhysicalLayer(byte[] Encoded_Packet, int offsetTBF, int countTBF, ref byte[] Received_Packet, int offSetRBF, int CountRBF)
        {
            _ex = null;
            lastIOCmdTime = DateTime.Now.TimeOfDay;
            Stream IOChannel = null;
            IsSyncComplete = true;
            int length = 0;
            try
            {
                #region ///Init Work Here

                if (Received_Packet == null || Received_Packet.Length < CountRBF)
                {
                    ReadBuffer = null;
                    ReadBuffer = IOBufferCreater.Invoke(MaxReadBufferSize);
                    Received_Packet = ReadBuffer;
                    offSetRBF = 0;
                    CountRBF = ReadBuffer.Length;
                }
                else if (Received_Packet != null)
                {
                    ReadBuffer = Received_Packet;
                }

                #endregion
                if (!IsConnected)
                {
                    throw new IOException("Physical Connection is not connected");
                }
                else
                {
                    try
                    {
                        IOChannel = IOStream;
                        if (DataReadTimeOut.Ticks > 0 && IOChannel != null && IOChannel.CanTimeout)
                        {
                            IOChannel.ReadTimeout = Convert.ToInt32(DataReadTimeOut.TotalMilliseconds);
                            IOChannel.WriteTimeout = Convert.ToInt32(DataReadTimeOut.TotalMilliseconds);
                        }
                    }
                    catch (Exception ex)
                    {
                        ///Perform Notify Close Connection
                        throw new IOException("unable to get IO stream channel ", ex);  ///throw IOException
                    }
                }
                IOChannel.Write(Encoded_Packet, offsetTBF, countTBF);
                #region Debugging & Logging
#if Enable_IO_Logging

                IOTrafficLogger.LogALTraffic(Encoded_Packet, offsetTBF, countTBF, DataStatus.Write);

#endif
                #endregion
                IOChannel.Flush();
                ///Property Indicates Async Operation Completed Successfully
                IAsyncResult res = null;
                if (IOChannel.CanRead)
                {
                    length = IOChannel.Read(Received_Packet, offSetRBF, CountRBF);
                    #region Debugging & Logging
#if Enable_IO_Logging
                    //Okay Data Written On Channel
                    IOTrafficLogger.LogALTraffic(Received_Packet, offSetRBF, length, DataStatus.Read);
#endif
                    #endregion
                }
                else
                    throw new IOException("Unable to Read from physical connection");
                return length;
            }
            catch (IOException ex)
            {
                //Debug & Log IOERRORs  & Close Current Client
                //Disconnect();
                //IOChannelDisconnect.Invoke("IO Channel is disconnected_" + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                //Debug & Log IOERRORs  & Close Current Client
                throw ex;
            }
            finally
            {
                #region Reset_TimeOut

                try
                {
                    //if (IOChannel != null && IOChannel.CanTimeout)
                    //{
                    //    //IOStream.ReadTimeout = Timeout.Infinite;
                    //    //IOStream.WriteTimeout = Timeout.Infinite;
                    //}
                }
                catch (Exception)
                { }

                #endregion
            }
        }

        public void SendRequestFromPhysicalLayer(byte[] Encoded_Packet, int offsetTBF, int countTBF)
        {
            lastIOCmdTime = DateTime.Now.TimeOfDay;
            Stream IOChannel = null;
            try
            {
                IOChannel = IOStream;
                if (!IsConnected)
                {
                    throw new IOException("Physical Connection is not connected");
                }
                if (DataReadTimeOut.Ticks > 0 && IOChannel != null && IOChannel.CanTimeout)
                {
                    IOChannel.ReadTimeout = Convert.ToInt32(DataReadTimeOut.TotalMilliseconds);
                    IOChannel.WriteTimeout = Convert.ToInt32(DataReadTimeOut.TotalMilliseconds);
                }
                if (IOChannel == null)
                    throw new IOException("Unable to get IO stream channel ");  /// throw IOException
                #region Debugging & Logging
#if Enable_IO_Logging

                IOTrafficLogger.LogALTraffic(Encoded_Packet, offsetTBF, countTBF, DataStatus.Write);

#endif
                #endregion
                IOChannel.Write(Encoded_Packet, offsetTBF, countTBF);
            }
            catch (IOException ex)
            {
                #region Debugging & Logging
#if Enable_DEBUG_ECHO
                Commons.Write(ex.Message);
#endif
                #endregion
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ReceiveResponseFromPhysicalLayer(ref byte[] Received_Packet, int offSetRBF, int CountRBF)
        {
            int byteRead = -1;
            try
            {
                lastIOCmdTime = DateTime.Now.TimeOfDay;
                Stream IOChannel = null;
                IOChannel = IOStream;
                ///Perform Notify Close Connection
                if (IOChannel == null)
                    throw new IOException("unable to get IO stream channel ");
                ///throw IOException
                if (IOChannel.CanRead)
                {
                    ///ConnectionMonitor.InflowDataBegin();
                    if (Received_Packet == null)
                    {
                        readBuf = IOBufferCreater.Invoke(this.MaxReadBuf);
                        Received_Packet = readBuf;
                        offSetRBF = 0;
                        CountRBF = Received_Packet.Length;
                    }
                    else
                        readBuf = Received_Packet;
                    byteRead = IOChannel.Read(Received_Packet, offSetRBF, CountRBF);
                    #region Debugging & Logging

#if Enable_IO_Logging
                    IOTrafficLogger.LogALTraffic(Received_Packet, offSetRBF, byteRead, DataStatus.Read);
                    ///Log AL IO Traffic Message    
                    ///IOLog.Invoke(Encoded_Packet, DataStatus.Write);
#endif

                    #endregion
                }
                else
                    throw new IOException(String.Format("Unable to Read from physical connection {0}", IOStream));

                return byteRead;
            }
            catch (Exception ex)
            {
#if Enable_DEBUG_ECHO
                Commons.WriteError(ex.Message);
#endif
                throw ex;
            }
        }

        public Task SendRequestFromPhysicalLayerASync(byte[] Encoded_Packet, int offsetTBF, int countTBF)
        {
            lastIOCmdTime = DateTime.Now.TimeOfDay;
            Stream IOChannel = null;
            try
            {
                IOChannel = IOStream;
                if (IOChannel == null)
                    throw new IOException("Unable to get IO stream channel ");  ///throw IOException
                if (!IsConnected)
                {
                    throw new IOException("Physical Connection is not connected");
                }
                #region Debugging & Logging
#if Enable_IO_Logging
                IOTrafficLogger.LogALTraffic(Encoded_Packet, offsetTBF, countTBF, DataStatus.Write);
#endif
                #endregion
                Task WriteTask = IOChannel.WriteAsync(Encoded_Packet, offsetTBF, countTBF);
                return WriteTask;
            }
            catch (IOException ex)
            {
                #region Debugging & Logging
#if Enable_DEBUG_ECHO
                Commons.Write(ex.Message);
#endif
                #endregion
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> ReceiveResponseFromPhysicalLayerASync(byte[] Received_Packet, int offSetRBF, int CountRBF)
        {
            Task<int> tk_Read = null;
            int byteRead = -1;
            try
            {
                lastIOCmdTime = DateTime.Now.TimeOfDay;
                Stream IOChannel = null;
                IOChannel = IOStream;
                //Perform Notify Close Connection
                if (IOChannel == null)
                    throw new IOException("Unable to get IO stream channel");
                //throw IOException
                if (IOChannel.CanRead)
                {
                    if (CurrentConnection == PhysicalConnectionType.OpticalPort)
                    {
                        tk_Read = IOChannel.ReadAsync(Received_Packet, offSetRBF, CountRBF);
                        byteRead = await tk_Read;
                    }
                    else
                    {
                        TCPStream localStream = TCPWrapperStream;
                        //int read_RetryCount = 0;
                        //repeat till Complete Packet Received
                        TimeSpan timeOutLimit = TimeSpan.MaxValue;
                        if (localStream.CanTimeout)
                            timeOutLimit = DateTime.Now.TimeOfDay + DataReadTimeOut;     //maxTimeOutLimit
                        while (true) //(read_RetryCount < 500)
                        {
                            tk_Read = TCPWrapperStream.ReadAsync(Received_Packet, offSetRBF, CountRBF);
                            //Handle DataReadTimeout
                            if (!tk_Read.Wait(DataReadTimeOut))
                            {
                                throw new IOException(String.Format("Data Receive Time Out {0}", localStream));
                            }
                            byteRead = await tk_Read;
                            if (byteRead > 0)
                            {
                                #region Debugging & Logging
#if Enable_IO_Logging
                                IOTrafficLogger.LogALTraffic(Received_Packet, offSetRBF, byteRead, DataStatus.Read);
#endif
                                #endregion
                                return byteRead;
                            }
                            else
                            {
                                if (byteRead == -1 && localStream.LastStreamState == TCPStream.HeartBeatReceive)
                                {
                                    //read_RetryCount++;
                                    if (DateTime.Now.TimeOfDay < timeOutLimit)
                                        continue;
                                    else
                                        break;
                                }
                                else if (localStream.LastStreamState != TCPStream.DataAPDUReceive && localStream.InternalException != null)
                                {
                                    throw localStream.InternalException;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        //end WhileLoop
                        if (localStream.LastStreamState != TCPStream.DataAPDUReceive)
                            throw new IOException(String.Format("Data Receive Time Out {0}", localStream));
                    }
                }
                #region Debugging & Logging
#if Enable_IO_Logging
                IOTrafficLogger.LogALTraffic(Received_Packet, offSetRBF, byteRead, DataStatus.Read);
                //Log AL IO Traffic Message    
                //IOLog.Invoke(Encoded_Packet, DataStatus.Write);
#endif
                #endregion
                return byteRead;
            }
            catch (Exception ex)
            {
#if Enable_DEBUG_ECHO

                Commons.WriteError(ex.Message);

#endif
                throw ex;
            }
            finally
            {
                try
                {
                    if (tk_Read != null)
                        tk_Read.Dispose();
                }
                catch
                { }
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                Delegate[] Handlers = null;
                #region ///Remove IOActivity Event Handlers
                
                //Delegate[] Handlers = null;
                //if (IOActivity != null)
                //{
                //    Handlers = IOActivity.GetInvocationList();
                //    foreach (Delegate item in Handlers)
                //    {
                //        IOActivity -= (Action<IOConnection, IOActivityType>)item;
                //    }
                //}

                #endregion
                #region ///Remove IOChannelDisconnect Event Handlers
                
                if (IOChannelDisconnect != null)
                {
                    Handlers = IOChannelDisconnect.GetInvocationList();
                    foreach (Delegate item in Handlers)
                    {
                        IOChannelDisconnect -= (Action<IOConnection, String>)item;
                    }
                }

                #endregion
                #region ///Remove PropertyChanged Event Handlers
                
                if (HeartReceived != null)
                {
                    Handlers = HeartReceived.GetInvocationList();
                    foreach (Delegate item in Handlers)
                    {
                        HeartReceived -= (Action<IOConnection, HeartBeat>)item;
                    }
                }

                #endregion#region
                #region //Remove PropertyChanged Event Handlers
                
                Handlers = null;
                if (PropertyChanged != null)
                {
                    Handlers = PropertyChanged.GetInvocationList();
                    foreach (Delegate item in Handlers)
                    {
                        PropertyChanged -= (System.ComponentModel.PropertyChangedEventHandler)item;
                    }
                }

                #endregion
                
                if (IOStream != null)
                {
                    IOStream.Close();
                }

            }
            catch (Exception)
            { }
        }

        #endregion

        #region IComparable<IOConnection> Members

        int IComparable<IOConnection>.CompareTo(IOConnection other)
        {
            try
            {
                int res_conInfo = 0;
                int res_Conn = 0;
                try
                {
                    res_Conn = ((IComparable<TCPStream>)this.IOStream).CompareTo((TCPStream)other.IOStream);
                }
                catch (Exception)
                {
                    res_Conn = -1;
                }
                if (res_Conn == 0)
                    return res_Conn;
                else
                {
                    res_conInfo = ((IComparable<ConnectionInfo>)this.ConnectionInfo).CompareTo(other.ConnectionInfo);
                    if (res_conInfo == 0)
                        return res_conInfo;
                    else
                    {
                        res_conInfo = (res_conInfo < 0) ? res_conInfo - 1 : ((res_conInfo > 0) ? res_conInfo + 1 : 0);
                        return res_conInfo.CompareTo(res_Conn);
                    }
                }

            }
            catch (Exception ex)
            {
                return -1;
            }
        }


        public override bool Equals(object obj)
        {
            ///return ((IComparable<IOConnection>)this).CompareTo((IOConnection)obj) == 0;
            return base.Equals(obj);
        }
        #endregion

        #region IConnection Members

        bool IConnection.IsConnected()
        {
            return IsChannelConnected;
        }

        public void ResetStream()
        {
            try
            {
                if (IOStream != null && IOStream is TCPStream)
                {
                    TCPStream IPIOStream = ((TCPStream)IOStream);
                    IPIOStream.ResetStream();
                }

            }
            catch (Exception ex)
            {
                throw new IOException("Error occurred while resetting TCP Stream");
            }
        }

        #endregion

        public override string ToString()
        {
            try
            {
                string str = base.ToString();
                if (IOStream != null)
                    str = IOStream.ToString();
                if (ConnectionInfo != null && ConnectionInfo.MeterSerialNumberObj != null)
                    str = ConnectionInfo.MSN;
                return str;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion
    }

    #region Supported Enumeration Types

    public enum PhysicalConnectionType
    {
        OpticalPort,
        TCPIP,
        KeepAlive,
        NonKeepAlive
    }

    public enum IOActivityType : byte
    {
        Inflow = 1,
        OutFlow = 2
    }

    #endregion

    public class IConnection_ConnectionTimeSortHelper : IComparer
    {
        #region IComparer<IOConnection> Members

        public int Compare(IOConnection x, IOConnection y)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IComparer Members

        /// <summary>
        /// Compares two objects and returns a value indicating whether one is less than,
        //  equal to, or greater than the other.
        /// </summary>
        /// <param name="x">x:The first object to compare.</param>
        /// <param name="y">y:The second object to compare</param>
        /// <returns> A signed integer that indicates the relative values of x and y, as shown
        ///     in the following table.Value Meaning Less than zero x is less than y. Zero
        ///     x equals y. Greater than zero x is greater than y.
        ///</returns>
        ///<exception cref="System.ArgumentException"> Neither x nor y implements the System.IComparable interface.-or- x and y
        ///are of different types and neither one can handle comparisons with the other
        ///</exception>
        public int Compare(object x, object y)
        {
            int ret_value = -1;
            try
            {
                if (x == null || !(x is IOConnection))
                    return -1;
                else if (y == null || !(y is IOConnection))
                    return 1;
                else
                {
                    IOConnection x_IConn = (IOConnection)x;
                    IOConnection y_IConn = (IOConnection)y;
                    return x_IConn.ConnectionTime.CompareTo(y_IConn.ConnectionTime);
                }
            }
            catch (Exception)
            {
                return ret_value;
            }
        }

        #endregion
    }

    public class PropertyChangedEventArgs<T> : PropertyChangedEventArgs
    {
        public T PreviousValue { get; private set; }

        public T CurrentValue { get; private set; }

        public PropertyChangedEventArgs(string propertyName, T previousValue, T currentValue)
            : base(propertyName)
        {
            this.PreviousValue = previousValue;
            this.CurrentValue = currentValue;
        }
    }
}
