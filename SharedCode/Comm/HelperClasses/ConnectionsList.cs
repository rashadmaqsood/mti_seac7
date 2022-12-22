using _HDLC;
using DLMS;
using SharedCode.Comm.DataContainer;
using SharedCode.Common;
using SharedCode.TCP_Communication;
/// #define Enable_IO_Logging

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Threading;
using System.Threading.Tasks;

namespace SharedCode.Comm.HelperClasses
{

    public class ConnectionsList : ConcurrentDictionary<MeterSerialNumber, IOConnection>
    {
        #region Data_Members
        private int MaxIOConnectionAllowed;
        public static int MaxRetry = 10;
        #endregion

        #region Properties

        public int MaxIOConnections
        {
            get { return MaxIOConnectionAllowed; }
            set { MaxIOConnectionAllowed = value; }
        }

        public IOConnection this[int i]
        {
            get
            {
                if (i >= this.Keys.Count) return null;
                KeyValuePair<MeterSerialNumber, IOConnection> TVal = this.ElementAt<KeyValuePair<MeterSerialNumber, IOConnection>>(i);
                return TVal.Value;


            }
        }

        public IOConnection this[String MSN]
        {
            get
            {
                //var msn = MeterSerialNumber.ConvertFrom(MSN);
                //IOConnection con = new IOConnection();
                //if (this.TryGetValue(msn, out con)) return null;
                //return con;

                //modifyed by furqan 12.24.2014
                var val = this.FirstOrDefault(x => x.Value.MSN == MSN);
                if (val.Value != null)
                    return val.Value;
                return null;
            }
        }

        public int KeepAliveCount
        {
            get
            {
                try
                {
                    return this.Count(x => x.Value.ConnectionInfo.ConnectionType == PhysicalConnectionType.KeepAlive);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public int NonKeepAliveCount
        {
            get
            {
                try
                {
                    return this.Count(x => x.Value.ConnectionInfo.ConnectionType == PhysicalConnectionType.NonKeepAlive);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }


        #endregion

        #region Constructurs
        public ConnectionsList()
            : base()
        {

        }
        public ConnectionsList(ConnectionsList ConnList)
        {
            if (ConnList != null)
            {
                foreach (MeterSerialNumber key in ConnList.Keys)
                {
                    this.TryAdd(key, ConnList[key]);
                }
            }
        }
        #endregion

        #region Support_Syn Methods

        public bool IsConnectionDuplicated(IOConnection ConnInfo)
        {
            try
            {
                return this.Any<KeyValuePair<MeterSerialNumber, IOConnection>>((x) => x.Value != null && ConnInfo != null &&
                               ((IComparable<IOConnection>)ConnInfo).CompareTo((IOConnection)x.Value) == 0);
            }
            catch
            { }
            return false;
        }

        public void Remove(IOConnection conn)
        {
            try
            {
                IOConnection _T = null;
                // conn.Disconnect();
                conn.CleanUpBuffer();
                conn.Dispose();
                conn.IOBufferCreater = null;
                // Dispose-off/Disconnect
                for (int retry = 0; retry < MaxRetry; retry++)
                {
                    if (this.TryGetValue(conn.MeterSerialNumberObj, out _T) &&
                        _T == conn)
                        if (this.TryRemove(conn.MeterSerialNumberObj, out _T))
                            break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { }
        }

        public void UpdateIOConnection(IOConnection conn, MeterSerialNumber OldVal = null)
        {
            IOConnection _T = null;
            try
            {
                MeterSerialNumber MSN = conn.MeterSerialNumberObj;
                // MSN Already Updated List
                if (MSN != null && (this.TryGetValue(MSN, out _T) ||
                    this.TryGetValue(OldVal, out _T)))
                {
                    if (_T != null && _T != conn)
                        TryUpdate(MSN, conn, _T);
                    else if (OldVal != MSN && _T != null)
                    {
                        TryRemove(OldVal, out _T);
                        TryAdd(MSN, conn);
                    }
                    else
                        _T = null;
                }
                // MSN To be updated
                if (MSN != null && _T == null)
                {
                    KeyValuePair<MeterSerialNumber, IOConnection> oldEntry = this.First<KeyValuePair<MeterSerialNumber, IOConnection>>((x) => x.Value == conn);
                    if (oldEntry.Key != null && oldEntry.Value != null)
                    {
                        base.TryRemove(oldEntry.Key, out _T);
                        this.TryAdd(conn.MeterSerialNumberObj, conn);
                    }
                }
            }
            catch
            {
            }

        }

        public void Clear()
        {
            try
            {
                ((ICollection<KeyValuePair<MeterSerialNumber, IOConnection>>)this).Clear();
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
                    int count_T = Count;
                    int randIndex = 0;
                    Random RandNum = new Random();
                    int maxRetry = count_T / 2;
                    if (count_T < 150)
                        maxRetry = count_T;
                    int retry_Count = 0;

                    if (count_T > 0)
                    {
                        while (retry_Count < maxRetry)
                        {
                            try
                            {
                                randIndex = RandNum.Next(count_T);
                                IOConnection Conn_T = (IOConnection)this[randIndex];
                                if (Conn_T != null && Conn_T.IsConnected)
                                {
                                    isConnected = true;
                                    break;
                                }
                            }
                            catch { }
                            retry_Count++;
                        }
                    }
                }
                catch (Exception)
                { }
                return isConnected;
            }
        }

        public void Disconnect()
        {
            try
            {
                foreach (var conn in this)
                {
                    try
                    {
                        //*** Commented ConnStatus
                        // conn.InsertLogMessage("Disconnect_ConnectionsList");
                        conn.Value.Disconnect();
                        conn.Value.Dispose();
                    }
                    catch
                    {
                    }
                }
                this.Clear();
                GC.Collect();
            }
            catch (Exception)
            { }
        }

        public void CleanUpBuffer()
        {
            try
            {
                foreach (var connection in this)
                {
                    connection.Value.CleanUpBuffer();
                }
                GC.Collect();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

    public delegate IOConnection IOConnectionGetter(MeterSerialNumber SrNumber);

    public class IOConnectionCache : IDisposable
    {
        #region DataMembers

        private CacheItemPolicy _ConnectionCacheItemPolicy = null;
        private ObjectCache sharedObjectCache = null;
        private IOConnectionGetter _GetIOConnectionFactory;

        #endregion

        #region Constructor

        public IOConnectionCache(TimeSpan minAge, TimeSpan maxAge)
        {
            try
            {
                sharedObjectCache = new MemoryCache("MyConnectionObjectCacheTester");
                ((MemoryCache)sharedObjectCache).Trim(100);
                ConnectionCacheItemPolicy = new CacheItemPolicy();
                ConnectionCacheItemPolicy.SlidingExpiration = minAge;
                ///ConnectionCacheItemPolicy.SlidingExpiration = maxAge;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IOConnectionCache(TimeSpan minAge, TimeSpan maxAge, IOConnectionGetter IOConnectionGetterDlg)
            : this(minAge, maxAge)
        {
            try
            {
                GetIOConnectionFactory = new IOConnectionGetter(IOConnectionGetterDlg);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Properties

        public ObjectCache SharedObjectCache
        {
            get { return sharedObjectCache; }
            set { sharedObjectCache = value; }
        }

        public CacheItemPolicy ConnectionCacheItemPolicy
        {
            get { return _ConnectionCacheItemPolicy; }
            set { _ConnectionCacheItemPolicy = value; }
        }

        public IOConnectionGetter GetIOConnectionFactory
        {
            get { return _GetIOConnectionFactory; }
            set { _GetIOConnectionFactory = value; }
        }
        #endregion

        #region Member Methods

        public IOConnection GetIOConection(String SrNumber)
        {
            IOConnection isConnected = null;
            try
            {
                MeterSerialNumber SrToMatch = MeterSerialNumber.ConvertFrom(SrNumber);
                isConnected = GetIOConection(SrToMatch);
                return isConnected;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error occurred while find Connection_IOConnectionCache_{0}_IO", SrNumber), ex);
            }
        }

        public IOConnection GetIOConection(MeterSerialNumber SrNumber)
        {
            IOConnection obj = null;
            try
            {
                try
                {
                    ///Synchronization Monitor Lock
                    if (Monitor.TryEnter(SrNumber, Commons.ReadLOCKLow_TimeOut))
                    {
                        try
                        {
                            obj = SharedObjectCache[SrNumber.ToString()] as IOConnection;
                        }
                        finally
                        {
                            Monitor.Exit(SrNumber);
                        }
                    }
                }
                catch (Exception)
                {
                    obj = null;
                }
                ///Update IOConnection Here
                if (obj == null || !obj.IsChannelConnected)
                {
                    obj = GetIOConnectionFactory.Invoke(SrNumber);
                    if (obj != null && Monitor.TryEnter(SrNumber, Commons.WriteLOCK_TimeOut))
                    {
                        try
                        {
                            ///Remove Previous IOConnection
                            SharedObjectCache.Remove(SrNumber.ToString());
                            SharedObjectCache.Add(new CacheItem(SrNumber.ToString(), obj), ConnectionCacheItemPolicy); // By Sha
                        }
                        finally
                        {
                            Monitor.Exit(SrNumber);
                        }
                    }
                    return obj;
                }
                else
                    return obj;

            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to IOConnection object for {0}", SrNumber), ex);
            }
        }

        public void TryRemoveIOConnection(MeterSerialNumber SrNumber)
        {
            try
            {
                ///Synchronization Monitor Lock
                if (Monitor.TryEnter(SrNumber, Commons.WriteLOCK_TimeOut))
                {
                    try
                    {
                        SharedObjectCache.Remove(SrNumber.ToString());
                    }
                    finally
                    {
                        Monitor.Exit(SrNumber);
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region Support_Methods


        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                if (SharedObjectCache != null && SharedObjectCache is IDisposable)
                {
                    ((IDisposable)SharedObjectCache).Dispose();
                }
                SharedObjectCache = null;
                ConnectionCacheItemPolicy = null;
            }
            catch (Exception) { }
        }

        #endregion
    }

    // Live Log for each Connection delegate 12/19/2014
    public delegate void OnChangeStatus(string message, MeterSerialNumber msn);

    public class IOConnection : IComparable<IOConnection>, IConnection, INotifyPropertyChanged
    {
        #region Current Meter Log Event

        event OnChangeStatus _currentMeterLog;
        //Event To Get Current Meter Working Live LOG
        public event OnChangeStatus CurrentMeterLog
        {
            add
            {
                lock (msn)
                {

                    Delegate[] Handlers = null;
                    bool alreadyReg = false;
                    if (_currentMeterLog != null)
                    {
                        Handlers = _currentMeterLog.GetInvocationList();
                        foreach (Delegate item in Handlers)
                        {
                            if (item.Target == value.Target)
                            {
                                alreadyReg = true;
                                break;
                            }
                        }
                    }
                    if (!alreadyReg)
                        _currentMeterLog += value;
                }
            }
            remove
            {
                lock (msn)
                {
                    Delegate[] Handlers = null;
                    bool alreadyReg = false;
                    if (_currentMeterLog != null)
                    {
                        Handlers = _currentMeterLog.GetInvocationList();
                        foreach (Delegate item in Handlers)
                        {
                            if (item.Target == value.Target)
                            {
                                alreadyReg = true;
                                break;
                            }
                        }
                    }
                    if (alreadyReg)
                        _currentMeterLog -= value;
                }
            }
        }

        #endregion

        #region Data_Members

        public event Action<IOConnection, String> IOChannelDisconnect = delegate { };
        // public event Action<IOConnection, IOActivityType> IOActivity = delegate { };
        public event Action<IOConnection, HeartBeat> HeartReceived = null; ///= delegate { };

        /// <summary>
        /// Make Rome For Event Notification Packet
        /// </summary>
        private AsyncCallback callBack = null;
        /// Modification
        private WrapperLayer _TCPWrapper;

        private int MaxReadBuf = 512;
        private TimeSpan dataReadTimeOut;
        private TimeSpan lastIOCmdTime;
        private DateTime connectionTime;
        private HeartBeat heartBeatReceived;
        private byte[] readBuf;
        private CrateIOBuffer ioBufferCreater;
        private Exception _ex;
        private ConnectionInfo connectionInfo;
        private DLMSLogger logger;
        private Stream ioStream;
        private Int32 isAssigned = -1;
        private MeterSerialNumber msn;
        private string _meterLiveLog = string.Empty;
        public volatile bool CancelationRequest = false;

        // Wrapper Stream Object Over ioStream
        private Stream _overlayStream;

        private IOConnectionMonitor connectionMonitor;
        private List<HeartBeat> heartBeats;

        #endregion

        #region Properties

        public IOConnectionMonitor ConnectionMonitor
        {
            get { return connectionMonitor; }
            set { connectionMonitor = value; }
        }

        public List<HeartBeat> HeartBeats
        {
            get { return heartBeats; }
            set { heartBeats = value; }
        }

        public bool IsHeartBeatReceive
        {
            get;
            set;
        }

        public Stream IOStream
        {
            get { return ioStream; }
            set { ioStream = value; }
        }

        public Stream OverlayStream
        {
            get { return _overlayStream; }
            set
            {
                _overlayStream = value;
                NotifyPropertyChanged("OverlayStream");
            }
        }

        public TCPStream IOStreamLocal
        {
            get
            {
                return (TCPStream)ioStream;
            }
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
        public TCPOverSerial TCPOverWrapperStream
        {
            get
            {
                TCPOverSerial InnerStream = null;
                try
                {
                    InnerStream = (TCPOverSerial)IOStream;
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
                if (IOStream != null)
                {
                    if (IOStream is TCPStream)
                        ((TCPStream)IOStream).TcpWrapper = value;
                    else if (IOStream is TCPStreamOverSerial)
                        ((TCPStreamOverSerial)IOStream).TcpWrapper = value;
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
                else
                    return PhysicalConnectionType.NonKeepAlive;
            }
            set
            {
                if (connectionInfo != null)
                    connectionInfo.ConnectionType = value;
            }
        }

        public string MSN
        {
            get
            {
                if (MeterSerialNumberObj != null)
                    return MeterSerialNumberObj.ToString("S");
                else
                    return null;
            }
            set
            {
                try
                {
                    MeterSerialNumberObj = MeterSerialNumber.ConvertFrom(value);
                }
                catch (Exception ex)
                {
                    throw new Exception(String.Format("Invalid Meter Serial Number Format {0}", value));
                }
            }
        }

        public MeterSerialNumber MeterSerialNumberObj
        {
            get { return msn; }
            set
            {
                bool IsValUpdated = false;
                if (value != null)
                {
                    if (msn == null || !msn.Equals(value))
                    {
                        IsValUpdated = true;
                    }
                    MeterSerialNumber OldVal = msn;
                    msn = value;
                    // Condition Event Raising On Value Modification
                    if (IsValUpdated)
                    {
                        PropertyChangedEventArgs<MeterSerialNumber> ChangeEventArg = new
                            PropertyChangedEventArgs<MeterSerialNumber>("MeterSerialNumberObj", OldVal, msn);
                        NotifyPropertyChanged("MeterSerialNumberObj", ChangeEventArg);
                    }
                }
            }
        }

        // live meter Log furqan 12/19/2014
        public string MeterLiveLog
        {
            get { return _meterLiveLog; }
            set
            {
                if (_currentMeterLog != null &&
                    MeterSerialNumberObj != null &&
                    !_meterLiveLog.Equals(value))
                {
                    _currentMeterLog(value, MeterSerialNumberObj);
                }
                _meterLiveLog = value;
            }
        }

        public object Tag { get; set; }

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

        public TimeSpan LastIOActivityDuration
        {
            get
            {
                TimeSpan T = TimeSpan.MaxValue;
                try
                {
                    if (IOStream is TCPStream)
                    {
                        T = ((TCPStream)IOStream).LastRawIODuration;
                    }
                }
                catch (Exception)
                { }
                return T;
            }
        }

        public DateTime ConnectionTime
        {
            get { return connectionTime; }
            set { connectionTime = value; }
        }

        public TimeSpan ConnectionDuration
        {
            get
            {
                TimeSpan T_Val = TimeSpan.MinValue;
                try
                {
                    if (ConnectionTime == DateTime.MinValue || ConnectionTime == DateTime.MaxValue)
                        return T_Val;
                    else
                        T_Val = DateTime.Now.Subtract(ConnectionTime);
                }
                catch (Exception)
                {
                    T_Val = TimeSpan.MinValue;
                }
                return T_Val;
            }
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
                    else if (IOStream is TCPOverSerial)
                        return ((TCPOverSerial)IOStream).Connected;
                    else
                        return false;

                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            //get
            //{
            //    try
            //    {
            //        // if (IOStream is _HDLC.HDLCStream)
            //        // {
            //        //   return (((_HDLC.HDLCStream)IOStream).HdlcProtocol.Connected);
            //        // }
            //        // else 
            //        if (IOStream is TCPStream)
            //        {
            //            return (((TCPStream)IOStream).Connected);
            //        }
            //        // else if (IOStream is TCPStreamOverSerial)
            //        //   return ((TCPStreamOverSerial)IOStream).Connected;
            //        else
            //            return false;
            //    }
            //    catch (Exception ex)
            //    {
            //        return false;
            //    }
            //}
        }

        public bool IsOverlayChannelConnected
        {
            get
            {
                try
                {
                    if (OverlayStream is _HDLC.HDLCStream)
                    {
                        return (((_HDLC.HDLCStream)OverlayStream).HdlcProtocol.Connected);
                    }
                    else
                        if (OverlayStream is TCPStream)
                    {
                        return (((TCPStream)OverlayStream).Connected);
                    }
                    else
                        return false;
                }
                catch
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
                if (IOStream != null && IOStream is TCPStream)
                {
                    isBusy = ((TCPStream)IOStream).IsSyncComplete;
                }
                return isBusy;
            }
            set
            {
                bool isBusy = value;
                if (IOStream != null && IOStream is TCPStream)
                {
                    ((TCPStream)IOStream).IsSyncComplete = value;
                }
                // NotifyPropertyChanged("IsBusy");
            }
        }

        public CommunicationMode CommunicationMode
        {
            get
            {
                var _CommunicationMode = CommunicationMode.ActiveIOSessionMode;

                if (IOStream != null &&
                    IOStream is TCPStream)
                {
                    _CommunicationMode = ((TCPStream)IOStream).CommunicationMode;
                }

                return _CommunicationMode;
            }
            set
            {
                if (IOStream != null &&
                    IOStream is TCPStream)
                {
                    ((TCPStream)IOStream).CommunicationMode = value;
                }
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

        /// <summary>
        /// This Delegate function would be called if DLMS Notification Data Received { Event Notification + Data Notification }
        /// The Encoded APDU from physical layer received needs to be decoded from DLMS Application Layer
        /// </summary>
        /// <remarks>
        /// This Method receives Encoded APDU from physical layer and after removing encapsulation (AL + COM Wrapper) 
        /// transmit it to the above layer Asynchronously
        ///</remarks>
        ///<exception cref="IOException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <param name="ArraySegment<byte>">Raw IO byte array receive from physical channel</param>        
        /// <returns>void</returns>
        public Action<ArraySegment<byte>> ReceiveDataFromPhysicalLayerASync
        {
            
            get
            {
                Action<ArraySegment<byte>> Delegate_Func = null;

                if (IOStream != null && IOStream is TCPStream)
                {
                    Delegate_Func = ((TCPStream)IOStream)._ReceiveDataFromPhysicalLayerASync;
                }

                return Delegate_Func;
            }
            set
            {
                if (IOStream != null && IOStream is TCPStream)
                {
                    ((TCPStream)IOStream)._ReceiveDataFromPhysicalLayerASync = value;
                }
                //else
                //    throw new Exception("Unable to set Delegate Method,IOConnection not initialized");
            }
        }

        public bool IsOverlayStreamEnable
        {
            get
            {
                return OverlayStream != null;
            }
        }

        public HeartBeat LastHeartBeat
        {
            get
            {
                return heartBeatReceived;
                // if (HeartBeats != null && HeartBeats.Count > 0 && HeartBeats[HeartBeats.Count - 1] != null)
                // {
                //     return HeartBeats[HeartBeats.Count - 1];
                // }
                // else
                // return null;
            }
        }

        public DeviceType CurrentDeviceType { get; set; }

        public MeterSerialNumber GatewaySerialNumber { get; set; }

        #endregion

        #region Constructor

        public IOConnection()
        {
            // _TCPWrapper = new WrapperLayer(0x01, 0x10);           
            // Meter Management Logical Device,Client PUBLIC SAP

            CurrentConnection = PhysicalConnectionType.NonKeepAlive;
            DataReadTimeOut = new TimeSpan(0, 1, 30);
            ConnectionTime = DateTime.Now;

            ConnectionMonitor = new IOConnectionMonitor();
            heartBeats = new List<HeartBeat>(5);

            // Init Other Objects
            ConnectionInfo = new ConnectionInfo();

            CurrentDeviceType = DeviceType.MeterDevice;
        }

        public IOConnection(Stream IOStream)
            : this()
        {
            this.IOStream = IOStream;
            if (IOStream is TCPStream)
            {
                ((TCPStream)IOStream).StreamDisconnected += new Action(IOConnection_StreamDisconnected);
                ((TCPStream)IOStream).HeartBeatReceived += new Action<HeartBeat>(IOConnection_HeartBeatReceived);

                //((TCPStream)IOStream).InActivityTimeOut += new Action<DateTime>(IOConnection_InActivityTimeOut);
                //Init TCPStream With Default TCPWrapper

                ((TCPStream)IOStream).TcpWrapper = WrapperLayer.Default;

                CurrentDeviceType = DeviceType.MeterDevice;
            }
            else if (IOStream is HDLCStream)
            {
                ((_HDLC.HDLCStream)IOStream).HdlcProtocol.HDLCDisconnected += new Action(HdlcProtocol_HDLCDisconnected);
            }
            else if(IOStream is TCPOverSerial)
            {
               
                    ((TCPOverSerial)IOStream).StreamDisconnected += new Action(IOConnection_StreamDisconnected);

                    //((TCPStream)IOStream).InActivityTimeOut += new Action<DateTime>(IOConnection_InActivityTimeOut);
                    //Init TCPStream With Default TCPWrapper

                    ((TCPOverSerial)IOStream).TcpWrapper = WrapperLayer.Default;

                    CurrentDeviceType = DeviceType.MeterDevice;
            }

            OverlayStream = null;
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

        /// <summary>
        /// Locate Stream Object of Current Connected Connection
        /// </summary>
        /// <returns></returns>
        public Stream GetOverlayStream()
        {
            try
            {
                if (OverlayStream != null &&
                    OverlayStream is TCPStream)
                {
                    TCPStream IPIOStream = ((TCPStream)OverlayStream);
                    IPIOStream.ResetStream();
                }

                return IOStream;
            }
            catch (Exception ex)
            {
                throw new IOException("Error getting Overlay IO Stream Object", ex);
            }
        }

        public void Disconnect()
        {
            Stream LocalIOStream = null;

            try
            {
                // Current Stream Object
                if (IsOverlayStreamEnable)
                    LocalIOStream = this.OverlayStream;
                else
                    LocalIOStream = IOStream;

                if (LocalIOStream != null)
                {
                    if (LocalIOStream is HDLCStream)
                    {
                        if (((_HDLC.HDLCStream)LocalIOStream).HdlcProtocol.Connected)
                            ((_HDLC.HDLCStream)LocalIOStream).HdlcProtocol.Disconnect();

                        LocalIOStream.Dispose();
                    }
                    else
                    {
                        bool isConnected = IsChannelConnected;
                        string str = "";

                        if (isConnected)
                        {
                            if (IOChannelDisconnect != null)
                                IOChannelDisconnect.Invoke(this, "TCP client disconnected " + str);
                        }
                        LocalIOStream.Close();

                        if (CancelationRequest)
                            MeterLiveLog = string.Format("{0,-8}{1,-2}", "FCL", "S");
                    }

                }

                if (IOStream is TCPStream)
                    (IOStream as TCPStream)._ReceiveDataFromPhysicalLayerASync = null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while closing physical connection", ex);
            }
        }

        #region Support_Method

        public void CleanUpBuffer()
        {
            try
            {
                ReadBuffer = null;
                if (IOStream != null && IOStream is TCPStream)
                {
                    ((TCPStream)IOStream).CleanUpBuffer();
                }
                //else if (IOStream != null && IOStream is _HDLC.HDLCStream)
                //{
                //   ((_HDLC.HDLCStream)IOStream).CleanUpBuffer();
                //}

            }
            catch
            {
            }
        }

        public void InitBuffer(int maxReadBuffer, int maxWriteBuffer, GetDataReaderBuffer GetDataReaderBuffer = null)
        {
            try
            {
                MaxReadBufferSize = maxReadBuffer;
                readBuf = null;
                if (IOStream is TCPStream)
                {
                    TCPStream InnerStream = TCPWrapperStream;
                    if (InnerStream != null)
                    {
                        // Init Inner NetSocket Buffers
                        InnerStream.InitBuffer(maxReadBuffer, maxWriteBuffer, GetDataReaderBuffer);
                    }

                    // Init Data Read TimeOuts
                    if (DataReadTimeOut.Ticks > 0)
                    {
                        // Implement Data Read/Write Time Out Here
                        InnerStream.WriteTimeout = Convert.ToInt32(DataReadTimeOut.TotalMilliseconds);
                        InnerStream.ReadTimeout = Convert.ToInt32(DataReadTimeOut.TotalMilliseconds);
                    }
                }
                else if(IOStream is TCPOverSerial)
                {
                    TCPOverSerial InnerStream = TCPOverWrapperStream;
                    if (InnerStream != null)
                    {
                        // Init Inner NetSocket Buffers
                        InnerStream.InitBuffer(maxReadBuffer, maxWriteBuffer, GetDataReaderBuffer);
                    }

                    // Init Data Read TimeOuts
                    if (DataReadTimeOut.Ticks > 0)
                    {
                        // Implement Data Read/Write Time Out Here
                        InnerStream.WriteTimeout = Convert.ToInt32(DataReadTimeOut.TotalMilliseconds);
                        InnerStream.ReadTimeout = Convert.ToInt32(DataReadTimeOut.TotalMilliseconds);
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while Initializing IO_Buffers", ex);
            }
        }

        public void DeInitBufferKeepAlive()
        {
            try
            {
                if (CurrentConnection == PhysicalConnectionType.KeepAlive)
                {
                    TCPStream InnerStream = TCPWrapperStream;
                    if (InnerStream != null)
                    {
                        readBuf = null;
                        ///Reset Inner NetSocket Buffers
                        InnerStream.DeInitBuffer();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while De_Initializing IO_Buffers", ex);
            }
        }

        #endregion

        #region Event Handlers

        internal void IOConnection_HeartBeatReceived(HeartBeat obj)
        {
            try
            {
                if (obj == null)
                    return;
                heartBeatReceived = obj;

                if (GatewaySerialNumber == null ||
                    !GatewaySerialNumber.IsMSN_Valid)
                {
                    GatewaySerialNumber = MeterSerialNumber.ConvertFrom(obj.MeterSerialNo, 0, obj.MeterSerialNo.Length);
                }

                if (MeterSerialNumberObj == null ||
                    !MeterSerialNumberObj.IsMSN_Valid)
                {
                    MeterSerialNumberObj = MeterSerialNumber.ConvertFrom(obj.MeterSerialNo, 0, obj.MeterSerialNo.Length);
                }

                if (HeartReceived != null)
                    HeartReceived.Invoke(this, LastHeartBeat);

                IsHeartBeatReceive = true;
            }
            catch
            { }
        }

        internal void IOConnection_StreamDisconnected()
        {
            try
            {
                // InsertLogMessage(String.Format("Connection Stream Passive Disconnect {0} {1}", ToString(), IOStream));
                if (IOChannelDisconnect != null)
                {
                    Delegate[] Dlg_List = IOChannelDisconnect.GetInvocationList();
                    foreach (var invk_Dlg in Dlg_List)
                    {
                        if (invk_Dlg != null)
                            ((Action<IOConnection, String>)invk_Dlg).BeginInvoke(this, String.Format("TCP client disconnected {0}", IOStream), null, this);
                    }
                }
                IOStream.Close();
                // Release All Resources
                IOStream.Dispose();
            }
            catch
            {
            }
        }

        public void HdlcProtocol_HDLCDisconnected()
        {
            Stream LocalIOStream = null;

            try
            {
                // Current Stream Object
                if (IsOverlayStreamEnable)
                {
                    LocalIOStream = this.OverlayStream;

                    // if (this.IOStream != null &&
                    //     this.IOStream is TCPStream)
                    //     (this.IOStream as TCPStream).OverlayMode = false;
                    // this.OverlayStream = null;
                }
                else
                    LocalIOStream = IOStream;

                if (IOChannelDisconnect != null &&
                    LocalIOStream != null &&
                    LocalIOStream is _HDLC.HDLCStream)
                {
                    IOChannelDisconnect.Invoke(this, "HDLC Connection disconnected");
                }

                LocalIOStream.Close();
                // Release All Resource
                LocalIOStream.Dispose();
            }
            catch
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

                // Already Connected & Log In Meter
                if (this.IsConnected)
                {
                    IOChannelDisconnect.Invoke(this, String.Format("TCP Client disconnected,Inactivity Period Elapsed {0}", IOStream));
                    // Release All Resources
                    IOStream.Dispose();
                    if (OverlayStream != null)
                        OverlayStream.Dispose();
                }
                // In Keep Alive Mode (Check Last Heart Beat Exceeds InActivityTimeOut)
                else
                {
                    if (LastHeartBeat != null)
                    {
                        TimeSpan lastHeartBeatDur = DateTime.Now.Subtract(LastHeartBeat.DateTimeStamp);
                        if (lastHeartBeatDur > InActivityTimeOut)
                        {
                            IOChannelDisconnect.Invoke(this, String.Format("TCP client disconnected,inactivity period elapsed {0}", IOStream));
                            // Release All Resources
                            IOStream.Dispose();
                        }
                        else // Heart Beat Received Before Inactivity
                        {
                            return;
                        }
                    }
                    else   // Disconnect Channel No Heart Beat Received
                    {
                        IOChannelDisconnect.Invoke(this, String.Format("TCP client disconnected,inactivity period elapsed {0}", IOStream));
                        // Release All Resources
                        IOStream.Dispose();

                        if (OverlayStream != null)
                            OverlayStream.Dispose();
                    }
                }
            }
            catch
            {
            }
            finally
            {

            }
        }

        #endregion

        #region IO_Functions Syn_&_ASync

        public int ReceiveDataFromPhysicalLayer(byte[] Encoded_Packet, int offsetTBF, int countTBF,
            ref byte[] Received_Packet, int offSetRBF, int CountRBF)
        {
            _ex = null;
            lastIOCmdTime = DateTime.Now.TimeOfDay;
            Stream IOChannel = null;
            IsSyncComplete = true;
            int length = 0;
            try
            {
                // Current Stream Object
                if (IsOverlayStreamEnable)
                    IOChannel = this.OverlayStream;
                else
                    IOChannel = IOStream;

                #region // Initial Work Here

                if (Received_Packet == null ||
                    Received_Packet.Length < CountRBF)
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
                        if (DataReadTimeOut.Ticks > 0 && IOStream != null && IOStream.CanTimeout)
                        {
                            IOStream.ReadTimeout = Convert.ToInt32(DataReadTimeOut.TotalMilliseconds);
                            IOStream.WriteTimeout = Convert.ToInt32(DataReadTimeOut.TotalMilliseconds);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Perform Notify Close Connection
                        throw new IOException("unable to get IO stream channel ", ex);  ///throw IOException
                    }
                }
                IOChannel.Write(Encoded_Packet, offsetTBF, countTBF);
                #region Debugging & Logging
#if Enable_IO_Logging

                IOTrafficLogger.LogALTraffic(Encoded_Packet, offsetTBF, countTBF, DataStauts.Write);

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
                    IOTrafficLogger.LogALTraffic(Received_Packet, offSetRBF, length, DataStauts.Read);
#endif
                    #endregion
                }
                else
                    throw new IOException("Unable to Read from physical connection");
                return length;
            }
            catch (IOException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        public void SendRequestFromPhysicalLayer(byte[] Encoded_Packet, int offsetTBF, int countTBF)
        {
            lastIOCmdTime = DateTime.Now.TimeOfDay;
            Stream IOChannel = null;
            try
            {
                // Current Stream Object
                if (IsOverlayStreamEnable)
                    IOChannel = this.OverlayStream;
                else
                    IOChannel = IOStream;

                if (!IsConnected)
                {
                    throw new IOException("Physical Channel is disconnected");
                }
                if (DataReadTimeOut.Ticks > 0 && IOStream != null && IOStream.CanTimeout)
                {
                    IOStream.ReadTimeout = Convert.ToInt32(DataReadTimeOut.TotalMilliseconds);
                    IOStream.WriteTimeout = Convert.ToInt32(DataReadTimeOut.TotalMilliseconds);
                }
                if (IOChannel == null)
                    throw new IOException("Unable to get IO stream channel ");  //throw IOException
                #region Debugging & Logging
#if Enable_IO_Logging

                IOTrafficLogger.LogALTraffic(Encoded_Packet, offsetTBF, countTBF, DataStauts.Write);

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

                // Current Stream Object
                if (IsOverlayStreamEnable)
                    IOChannel = this.OverlayStream;
                else
                    IOChannel = IOStream;

                /// Perform Notify Close Connection
                if (IOChannel == null)
                    throw new IOException("unable to get IO stream channel ");
                if (!IsConnected)
                {
                    throw new IOException("Physical Channel is disconnected");
                }
                /// throw IOException
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
                    IOTrafficLogger.LogALTraffic(Received_Packet, offSetRBF, byteRead, DataStauts.Read);
                    ///Log AL IO Traffic Message    
                    ///IOLog.Invoke(Encoded_Packet, DataStauts.Write);
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
                // Current Stream Object
                if (IsOverlayStreamEnable)
                    IOChannel = this.OverlayStream;
                else
                    IOChannel = IOStream;

                if (IOChannel == null)
                    throw new IOException("Unable to get IO stream channel ");  ///throw IOException
                if (!IsConnected)
                {
                    throw new IOException("Physical Connection is not connected");
                }
                #region Debugging & Logging
#if Enable_IO_Logging
                IOTrafficLogger.LogALTraffic(Encoded_Packet, offsetTBF, countTBF, DataStauts.Write);
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

        public Task<int> ReceiveResponseFromPhysicalLayerASync(byte[] Received_Packet, int offSetRBF, int CountRBF)
        {
            Task<int> tk_Read = null;
            int byteRead = -1;
            try
            {
                lastIOCmdTime = DateTime.Now.TimeOfDay;
                Stream IOChannel = null;

                // Current Stream Object
                if (IsOverlayStreamEnable)
                    IOChannel = this.OverlayStream;
                else
                    IOChannel = IOStream;

                // Perform Notify Close Connection
                if (IOChannel == null)
                    throw new IOException("Unable to get IO stream channel");

                //throw IOException
                if (IOChannel.CanRead)
                {
                    TCPStream localStream = TCPWrapperStream;
                    //int read_RetryCount = 0;
                    //repeat till Complete Packet Received
                    TimeSpan timeOutLimit = TimeSpan.MaxValue;
                    if (localStream.CanTimeout)
                        timeOutLimit = DateTime.Now.TimeOfDay + DataReadTimeOut;     //maxTimeOutLimit

                    while (true)//(read_RetryCount < 500)
                    {
                        tk_Read = Task<int>.Factory.StartNew(() => IOChannel.Read(Received_Packet, offSetRBF, CountRBF));

                        //Handle DataReadTimeout
                        if (!tk_Read.Wait(DataReadTimeOut))
                        {
                            throw new IOException(String.Format("IO Operation Connection timed out or remote host has failed to respond {0}", IOChannel));
                        }
                        byteRead = tk_Read.Result;
                        if (byteRead > 0)
                        {
                            #region Debugging & Logging
#if Enable_IO_Logging
                                IOTrafficLogger.LogALTraffic(Received_Packet, offSetRBF, byteRead, DataStauts.Read);
#endif
                            #endregion
                            return tk_Read;
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
                #region Debugging & Logging
#if Enable_IO_Logging
                IOTrafficLogger.LogALTraffic(Received_Packet, offSetRBF, byteRead, DataStauts.Read);
                //Log AL IO Traffic Message    
                //IOLog.Invoke(Encoded_Packet, DataStauts.Write);
#endif
                #endregion
                //return byteRead;
                return tk_Read;
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
                #region // Remove IOChannelDisconnect Event Handlers

                if (IOChannelDisconnect != null)
                {
                    Handlers = IOChannelDisconnect.GetInvocationList();
                    foreach (Delegate item in Handlers)
                    {
                        IOChannelDisconnect -= (Action<IOConnection, String>)item;
                    }
                }

                #endregion
                #region // Remove PropertyChanged Event Handlers

                if (HeartReceived != null)
                {
                    Handlers = HeartReceived.GetInvocationList();
                    foreach (Delegate item in Handlers)
                    {
                        HeartReceived -= (Action<IOConnection, HeartBeat>)item;
                    }
                }

                #endregion#region
                #region // Remove PropertyChanged Event Handlers

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
                    // IOStream.Dispose();
                }
                if (OverlayStream != null)
                {
                    OverlayStream.Close();
                    OverlayStream = null;
                }

                callBack = null;
                heartBeatReceived = null;
                readBuf = null;
                logger = null;
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
                int res_Conn = 0, res_Conn_1 = 0;
                ///Compare MeterSerialNumber
                #region MeterSerialNumber
                try
                {
                    if (this.MeterSerialNumberObj == null || other.MeterSerialNumberObj == null)
                        res_Conn_1 = -1;
                    else
                        res_Conn_1 = ((IComparable<MeterSerialNumber>)this.MeterSerialNumberObj).CompareTo(other.MeterSerialNumberObj);
                }
                catch
                {
                    res_Conn_1 = -1;
                }
                if (res_Conn_1 == 0)
                    return res_Conn_1;
                #endregion
                //Compare Connections Remote Socket
                #region Compare_Connections_Remote_Socket

                try
                {
                    res_Conn = ((IComparable<TCPStream>)this.IOStream).CompareTo((TCPStream)other.IOStream);

                }
                catch (Exception)
                {
                    res_Conn = -1;

                }
                return res_Conn;

                #endregion
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
            bool isConnected = false;

            try
            {
                if (IsOverlayStreamEnable)
                    isConnected = IsChannelConnected && IsOverlayChannelConnected;
                else
                    isConnected = IsChannelConnected;
            }
            catch
            {
                isConnected = false;
            }

            return isConnected;
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
                throw new IOException(string.Format("Error occurred while resetting TCP Stream Error Code:{0}", (int)MDCErrors.Ph_Reset_Stream));
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
                if (MSN != null)
                    str = MSN;
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

        private void NotifyPropertyChanged(String info, PropertyChangedEventArgs PropertyChanngedEventArgs)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, PropertyChanngedEventArgs);
            }
        }

        #endregion

    }

    public class IConnection_ConnectionTimeSortHelper : IComparer
    {
        #region IComparer<IOConnection> Members

        public int Compare(IOConnection x, IOConnection y)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IComparer Members

        // Summary:
        //     Compares two objects and returns a value indicating whether one is less than,
        //     equal to, or greater than the other.
        //
        // Parameters:
        //   x:
        //     The first object to compare.
        //
        //   y:
        //     The second object to compare.
        //
        // Returns:
        //     A signed integer that indicates the relative values of x and y, as shown
        //     in the following table.Value Meaning Less than zero x is less than y. Zero
        //     x equals y. Greater than zero x is greater than y.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     Neither x nor y implements the System.IComparable interface.-or- x and y
        //     are of different types and neither one can handle comparisons with the other.

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
