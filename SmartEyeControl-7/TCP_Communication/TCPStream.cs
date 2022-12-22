#define Enable_DEBUG_ECHO
#define Enable_Error_Logging

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using DLMS;
using SmartEyeControl_7.Common;

namespace TCP_Communication
{
    public class TCPStream : NetworkStream, IComparable<TCPStream>
    {
        #region Data_Members

        public static TimeSpan LastIOMaxLimit = new TimeSpan(0, 0, 45);
        private WrapperLayer tcpWrapper;
        private HeartBeat heartBeatPacket;
        private Exception InnerException;
        private TimeSpan _InActiveTime = new TimeSpan(0, 0, 30, 0, 0);
        private TimeSpan lastActivityTime = DateTime.Now.TimeOfDay;

        #region TCPStream_State

        private byte streamState = NotReceive;
        private byte lastStreamState = NotReceive;
        
        public static readonly byte NotReceive = 0;
        public static readonly byte WrapperReceive = 1;
        public static readonly byte HeartBeatReceive = 2;
        public static readonly byte DataAPDUReceive = 3;
        public static readonly TimeSpan DefaultInactiveTimeOut = new TimeSpan(0, 20, 0);

        #endregion

        /// <summary>
        /// Read Method Arguments
        /// </summary>
        private CommunicationMode _CommunicationMode;
        public int BufferLength = 512;
        public static readonly int IdleMeterBufferLength = 128;

        public event Action<HeartBeat> HeartBeatReceived = delegate { };
        public event Action<DateTime> InActivityTimeOut = delegate { };
        public event Action StreamDisconnected = delegate { };

        private Exception internalException;
        private byte[] localBuffer;
        private ArraySegment<byte> localBufferArg;
        private EndPoint remortPort = null;

        #endregion

        #region Constructors

        public TCPStream(Socket socket) :
            this(socket, FileAccess.ReadWrite, true, new WrapperLayer())
        {

        }

        public TCPStream(Socket socket, bool ownsSocket) :
            this(socket, FileAccess.ReadWrite, ownsSocket, new WrapperLayer())
        {
        }

        public TCPStream(Socket socket, FileAccess access)
            : this(socket, access, true, new WrapperLayer())
        {
        }

        public TCPStream(Socket socket, FileAccess access, bool ownsSocket) :
            this(socket, access, ownsSocket, new WrapperLayer())
        {

        }

        public TCPStream(Socket socket, FileAccess access, bool ownsSocket, WrapperLayer tcpWrapper)
            : base(socket, access, ownsSocket)
        {
            try
            {
                remortPort = socket.RemoteEndPoint;
                //WriterLockingObject = new object();
                if (!IsClientSocketConnected(socket))
                    throw new IOException("Connection is disconnected" + this.ToString());
                //Set Buffer For Idle Meter Connection
                base.Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendBuffer, IdleMeterBufferLength);
                base.Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, IdleMeterBufferLength);
                _CommunicationMode = CommunicationMode.IdleAliveMode;

                if (tcpWrapper == null)
                    TcpWrapper = new WrapperLayer();
                else
                    TcpWrapper = tcpWrapper;
                HeartBeatPacket = new HeartBeat();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Properties

        public EndPoint RemortPort
        {
            get { return remortPort; }
            private set { remortPort = value; }
        }

        public WrapperLayer TcpWrapper
        {
            get { return tcpWrapper; }
            set { tcpWrapper = value; }
        }

        public TimeSpan InActiviteTimeOut
        {
            get { return _InActiveTime; }
            set { _InActiveTime = value; }
        }

        public HeartBeat HeartBeatPacket
        {
            get { return heartBeatPacket; }
            set { heartBeatPacket = value; }
        }

        public bool IsSyncComplete
        {
            get
            {
                #region Getter

                return true;

                #endregion
            }
            set
            {
                #region Setter

                #endregion
            }
        }

        public TimeSpan LastActivityTime
        {
            get { return lastActivityTime; }
            private set { lastActivityTime = value; }
        }

        public TimeSpan LastActivityDuration
        {
            get
            {
                try
                {
                    return DateTime.Now.TimeOfDay.Subtract(LastActivityTime);
                }
                catch (Exception)
                {
                    return TimeSpan.MaxValue;
                }
            }
        }

        public TimeSpan LastRawIODuration
        {
            get
            {
                try
                {
                    if (LastActivityDuration < LastHeartBeatDuration)
                        return LastActivityDuration;
                    else
                        return LastHeartBeatDuration;
                }
                catch (Exception)
                {
                    return TimeSpan.MaxValue;
                }
            }
        }

        public TimeSpan LastHeartBeatDuration
        {
            get
            {
                try
                {
                    if (HeartBeatPacket != null)
                    {
                        TimeSpan _t = DateTime.Now.Subtract(HeartBeatPacket.DateTimeStamp);
                        return _t;
                    }
                    return TimeSpan.MaxValue;
                }
                catch (Exception ex)
                {
                    return TimeSpan.MaxValue;
                }
            }
        }

        public bool Connected
        {
            get
            {
                try
                {
                    if (!base.Socket.Connected || base.Socket == null)
                        return false;
                    else if (LastRawIODuration <= TCPStream.LastIOMaxLimit)
                        return base.Socket.Connected;
                    else
                        return IsClientSocketConnected(base.Socket);
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public Exception InternalException
        {
            get { return InnerException; }
            set { InnerException = value; }
        }

        public byte StreamState
        {
            get
            {
                return streamState;
            }
            private set
            {
                try
                {
                    if (value == NotReceive || value == WrapperReceive || value == DataAPDUReceive || value == HeartBeatReceive)
                        streamState = value;
                    else
                        throw new Exception("Invalid Stream State Value");
                }
                finally
                {

                }
            }
        }

        public byte LastStreamState
        {
            get
            {
                return lastStreamState;
            }
            private set
            {
                try
                {
                    if (value == NotReceive || value == WrapperReceive || value == DataAPDUReceive || value == HeartBeatReceive)
                        lastStreamState = value;
                    else
                        throw new Exception("Invalid StreamState Value");
                }
                finally
                {

                }
            }
        }

        public CommunicationMode CommunicationMode
        {
            get { return _CommunicationMode; }
            private set { _CommunicationMode = value; }
        }

        internal byte[] LocalBuffer
        {
            get
            {
                if (localBuffer == null || localBuffer.Length != BufferLength)
                {
                    localBuffer = new byte[BufferLength];
                }
                return localBuffer;
            }
        }

        public int ReadBufferSize
        {

            get { return BufferLength; }
            set { BufferLength = value; }
        }

        #endregion

        #region Member_Methods

        public override void Write(byte[] buffer, int offset, int size)
        {
            try
            {
                this.TcpWrapper.PacketLength = (ushort)size;
                byte[] t = TcpWrapper.EncodeWrapper();
                //Copy Encoded Data On Write Buffer    
                byte[] target = buffer;
                App_Common.Append_to_Start(ref target, ref offset, ref size, t, 0, t.Length);
                //base.Write(t, 0, t.Length);
                base.Write(target, offset, size);
                base.Flush();
                LastActivityTime = DateTime.Now.TimeOfDay;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
        {
            try
            {
                #region Initial Condition Check

                if (buffer == null || buffer.Length <= 0)
                    throw new ArgumentNullException("buffer");
                if (offset < 0 || offset >= buffer.Length)
                    throw new ArgumentException("offset");
                if (size <= 0)
                    throw new ArgumentException("size");
                if (offset + size > buffer.Length)
                    throw new ArgumentException("Invalid offset and size argument");
                if (!Connected || InternalException != null)
                {
                    if (InternalException == null)
                        throw new IOException(String.Format("Physical Channel is disconnected {0}", RemortPort));
                    else
                        throw InternalException;
                }

                #endregion
                this.TcpWrapper.PacketLength = (ushort)size;
                byte[] t = TcpWrapper.EncodeWrapper();
                //Copy Encoded Data On Write Buffer    
                byte[] target = buffer;
                App_Common.Append_to_Start(ref target, ref offset, ref size, t, 0, t.Length);
                IAsyncResult toRet = null;

                InternalException = null;
                toRet = base.BeginWrite(target, offset, size, callback, state);
                LastActivityTime = new TimeSpan();

                return toRet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void EndWrite(IAsyncResult asyncResult)
        {
            try
            {
                if (InternalException != null)
                    throw InternalException;
                base.EndWrite(asyncResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                LastActivityTime = DateTime.Now.TimeOfDay;
            }
        }

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
        {
            try
            {
                #region Check Initial Condition

                if (buffer == null || buffer.Length <= 0)
                    throw new ArgumentNullException("buffer");
                if (offset < 0 || offset >= buffer.Length)
                    throw new ArgumentException("offset");
                if (size <= 0)
                    throw new ArgumentException("size");
                if (offset + size > buffer.Length)
                    throw new ArgumentException("Invalid offset and size argument");

                #endregion
                localBufferArg = new ArraySegment<byte>(buffer, offset, size);
                //Check Physical Channel Condition
                if (!Connected || InternalException != null)
                {
                    if (InternalException == null)
                        throw new IOException("Physical Channel is disconnected");
                    else
                        throw InternalException;
                }
                var IAsyncResult = base.BeginRead(LocalBuffer, 0, LocalBuffer.Length, callback, state);
                return IAsyncResult;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public override int EndRead(IAsyncResult asyncResult)
        {
            int APDU_length = -1;
            try
            {
                int length = base.EndRead(asyncResult);
                int byteLengthParsed = DataReceiverThread(LocalBuffer, 0, length);
                if (LastStreamState == DataAPDUReceive)
                    APDU_length = localBufferArg.Count;
                else if (LastStreamState == HeartBeatReceive)
                {
                    APDU_length = -1;
                }
                if (LastStreamState != DataAPDUReceive && InternalException != null)
                {
                    APDU_length = -1;
                    throw InternalException;
                }
                //error case
                return APDU_length;
            }
            catch (ThreadAbortException) { return -1; }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                LastActivityTime = DateTime.Now.TimeOfDay;
                InternalException = null;
            }
        }

        public override int Read(byte[] buffer, int offset, int size)
        {
            try
            {
                #region Check Initial Condition

                if (buffer == null || buffer.Length <= 0)
                    throw new ArgumentNullException("buffer");
                if (offset < 0 || offset >= buffer.Length)
                    throw new ArgumentException("offset");
                if (size <= 0)
                    throw new ArgumentException("size");
                if (offset + size > buffer.Length)
                    throw new ArgumentException("Invalid offset and size argument");

                if (StreamState == TCPStream.NotReceive && InternalException != null)
                {
                    Exception ex = InternalException;
                    InternalException = null;
                    throw ex;
                }

                #endregion
                //int read_RetryCount = 0;
                //repeat till Complete Packet Received
                TimeSpan timeOutLimit = TimeSpan.MaxValue;
                if (CanTimeout)
                    timeOutLimit = DateTime.Now.TimeOfDay + TimeSpan.FromMilliseconds(ReadTimeout);     //maxTimeOutLimit
                while (true)//(read_RetryCount < 500)
                {
                    localBufferArg = new ArraySegment<byte>(buffer, offset, size);
                    int length = base.Read(LocalBuffer, 0, LocalBuffer.Length);
                    int byteLengthParsed = DataReceiverThread(LocalBuffer, 0, length);
                    if (LastStreamState == DataAPDUReceive)
                        return localBufferArg.Count;
                    else if (LastStreamState == HeartBeatReceive)
                    {
                        //read_RetryCount++;
                        if (DateTime.Now.TimeOfDay < timeOutLimit)
                            continue;
                        else
                            break;
                    }
                    else
                        break;
                }
                if (LastStreamState != DataAPDUReceive && InternalException != null)
                {
                    throw InternalException;
                }
                if (LastStreamState != DataAPDUReceive)
                    throw new IOException(String.Format("Data Receive Time Out {0}", ToString()));
                //error case
                return -1;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                LastActivityTime = DateTime.Now.TimeOfDay;
                InternalException = null;
            }
        }

        public override int ReadByte()
        {
            return base.ReadByte();
        }

        public override void WriteByte(byte value)
        {
            ///base.WriteByte(value);
            base.Write(new byte[] { value }, 0, 1);
        }

        #endregion

        #region Support_Methods

        #region Data_ReceiverThread

        public int DataReceiverThread(byte[] arrBuf, int Offset, int length)
        {
            bool isHeartBeatRe = false;
            InternalException = null;
            byte[] TArray = new byte[8];
            int PacketLengthReceive = 0;
            int offset = Offset;
            HeartBeat hbeat = new HeartBeat();
            try
            {
                while (arrBuf != null && offset < arrBuf.Length && (offset - Offset) < length)
                {
                    #region Try_Parsing_HeaderWrapper
                    try
                    {
                        int dtLength = (arrBuf.Length - offset);
                        if (TArray.Length <= dtLength)
                        {
                            dtLength = TArray.Length;
                            Buffer.BlockCopy(arrBuf, offset, TArray, 0, dtLength);
                            //Complete Data received Verify either Wrapper or Heart Beat Received
                            TcpWrapper.DecodeWrapper(TArray, 0, dtLength);
                            hbeat.DecodeHeartBeat(TArray, 0, dtLength);
                            //Increment array OffSet
                            if (tcpWrapper.IsVerified)
                            {
                                offset += WrapperLayer.WrapperHeaderLength;
                            }
                            else if (hbeat.IsVerifited)
                            {
                                offset += hbeat.ExpectedPacketLength;
                            }
                        }
                        else if (dtLength <= 0)
                        {
                            //ResetStream();
                            InternalException = new IOException("IO Packet is not verified DataReceiverThread_TCPStream");
                            throw InternalException;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    #endregion
                    //TCP Wrapper Received SET State To TCPWrapper & Packet Length > 0
                    if (TcpWrapper.IsVerified)
                    {
                        //Reset Stream Header
                        if (TcpWrapper.PacketLength > 0)
                        {
                            StreamState = TCPStream.WrapperReceive;
                            PacketLengthReceive = this.TcpWrapper.PacketLength;
                        }
                        //Reset Stream If Wrapper of Length Zero received
                        else
                        {
                            StreamState = NotReceive;
                            InternalException = new IOException("TCP Wrapper is Verified,Invalid Packet Length 0,DataReceiverThread_TCPStream");
                        }
                    }
                    //Heart Beat Received SET HBeat Status
                    else if (hbeat.IsVerifited)
                    {
                        LastStreamState = TCPStream.HeartBeatReceive;
                        StreamState = TCPStream.HeartBeatReceive;
                        this.HeartBeatPacket = hbeat;
                    }
                    //Reset Stream State to Not Receive After Flush All Data
                    else
                    {
                        ///Reset Stream Header
                        StreamState = NotReceive;
                        InternalException = InternalException = new IOException("TCP Wrapper is not Verified " + ToString()
                            + " DataReceiverThread_TCPStream");
                        break;
                    }
                    if (StreamState == TCPStream.WrapperReceive)
                    {
                        InternalException = null;
                        StreamState = TCPStream.NotReceive;
                        LastStreamState = TCPStream.WrapperReceive;
#if Enable_DEBUG_ECHO
                        // Console.Out.WriteLine("Data Receiver Thread Check Point_1");
#endif
                        int dataAvailLength = length - offset;
                        int tLength = 0;

                        if (dataAvailLength >= PacketLengthReceive)
                            tLength = PacketLengthReceive;  //tArray = new byte[PacketLengthReceive];
                        else
                            tLength = dataAvailLength;      //tArray = new byte[dataAvailLength];
                        InternalException = null;
                        //InvokeResponse(arrBuf, offset, ref tLength);
                        #region Copy_Data_Receive

                        try
                        {
                            if (localBufferArg == null || localBufferArg.Offset + localBufferArg.Count < tLength)
                                throw new ArgumentNullException("buffer");

                            byte[] readBufferMethod = localBufferArg.Array;
                            int readBufOffSet = localBufferArg.Offset;
                            int readBufSize = localBufferArg.Count;
                            int last_readBufferIndex = (readBufOffSet + readBufSize);

                            int i = offset;
                            int count_read = 0;
                            ///Try Read Data From Internal Buffer
                            for (int index = readBufOffSet;
                                    (index <= last_readBufferIndex) &&
                                    (i < (offset + tLength));
                                    index++, i++)
                            {
                                readBufferMethod[index] = arrBuf[i];
                                count_read++;
                            }
                            tLength = count_read;
                            //Update localBufferArg
                            localBufferArg = new ArraySegment<byte>(readBufferMethod, readBufOffSet, count_read);
                        }
                        catch (Exception ex)
                        {
                            InnerException = new Exception("Error Occurred while copying data read buffer", ex);
                            throw InnerException;
                        }

                        #endregion
                        offset += tLength;
                        if (PacketLengthReceive > tLength)
                        {
                            this.InternalException = new IOException("Packet is not completely received,invalid packet length" + ToString()
                                + " DataReceiverThread_TCPStream");
                            //ResetStream();
                            //InvokeResponse();
                            throw InternalException;
                        }
                        LastStreamState = TCPStream.DataAPDUReceive;
                        LastActivityTime = DateTime.Now.TimeOfDay;
                    }
                    if (StreamState == TCPStream.HeartBeatReceive)
                    {
                        InternalException = null;
                        if (!isHeartBeatRe &&
                            CommunicationMode != TCP_Communication.CommunicationMode.ActiveIOSessionMode)
                        {
                            TryWriteHeartBeatResponse();
                        }
                        isHeartBeatRe = true;
                        StreamState = TCPStream.NotReceive;
                        //Invoke HeartBeat Received
                        HeartBeatReceived.Invoke(HeartBeatPacket);
                    }
                }
                //Verify Either Some Packet Received Here
                if (LastStreamState == NotReceive || (LastStreamState != DataAPDUReceive && InternalException != null))
                {
                    if (InternalException == null)
                    {
                        Exception _InnerException = new IOException("Error reading data,may be Packet is not completely received"
                            + ToString() + " DataReceiverThread_TCPStream");
                        InternalException = _InnerException;
                    }
                    throw InternalException;
                }
            }
            catch (Exception)
            {
                StreamState = NotReceive;
                throw;
            }
            return offset;
        }

        #endregion

        private bool IsClientSocketConnected(Socket clientSocket)
        {
            bool blockingState = clientSocket.Blocking;
            try
            {
                if (clientSocket == null || !clientSocket.Connected)
                    return false;
                else
                {
                    using (NetworkStream st = new NetworkStream(clientSocket, false))
                    {
                        clientSocket.Blocking = false;
                        byte[] t = new byte[1];
                        clientSocket.Send(t, 0, 0);
                        clientSocket.Blocking = true;
                        ///clientSocket.Receive(t, 0, 0, SocketFlags.None);
                        if (clientSocket.Poll(1, SelectMode.SelectRead))
                        {
                            if (clientSocket.Receive(t, SocketFlags.Peek) == 0)
                                return false;
                        }
                        return clientSocket.Connected;
                    }
                }
            }
            catch (SocketException e)
            {
                // 10035 == WSAEWOULDBLOCK
                if (e.NativeErrorCode.Equals(10035))
                    return true;
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (clientSocket != null)
                {
                    try
                    {
                        clientSocket.Blocking = blockingState;
                    }
                    catch (Exception) { }
                }
            }
        }

        public void ResetStream()
        {
            bool IsSynReceived = IsSyncComplete;
            try
            {
                StreamState = NotReceive;
                ///lastStreamState = NotReceive;
                InternalException = null;
                try
                {
                    if (base.Socket.Connected)
                    {
                        ///Reset Internal Buffers & Protocol State
                        int tReceiveBufferLen = base.Socket.ReceiveBufferSize;
                        int tTransmietBufferLen = base.Socket.SendBufferSize;
                        ///Discard All TCP Receive And transmit Buffers
                        byte[] t = new byte[base.Socket.Available];
                        if (t.Length > 0)
                        {
                            base.Read(t, 0, t.Length);
                        }
                        base.Socket.ReceiveBufferSize = 0;
                        base.Socket.SendBufferSize = 0;
                        base.Socket.ReceiveBufferSize = tReceiveBufferLen;
                        base.Socket.SendBufferSize = tTransmietBufferLen;
                    }
                    IsSyncComplete = false;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                IsSyncComplete = IsSynReceived;
            }
        }

        public void InitBuffer(int maxWriteBuffer = 512, int maxReadBuffer = 512)
        {
            try
            {
                try
                {
                    CommunicationMode = TCP_Communication.CommunicationMode.ActiveIOSessionMode;
                    BufferLength = maxReadBuffer;

                    //Set Buffer For Idle Meter Connection
                    base.Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendBuffer, maxWriteBuffer);
                    base.Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, maxReadBuffer);

                    //base.Socket.ReceiveBufferSize = maxReadBuffer;
                    //base.Socket.SendBufferSize = maxWriteBuffer;
                }
                finally
                {
                    //Monitor.Exit(WriterLockingObject);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while InitBufferKeepAlive_TCPStream", ex);
            }
        }

        public void DeInitBuffer()
        {
            try
            {
                CommunicationMode = TCP_Communication.CommunicationMode.IdleAliveMode;
                InternalException = null;
                localBuffer = null;

                BufferLength = IdleMeterBufferLength;
                ///Set Buffer For Idle Meter Connection
                base.Socket.ReceiveBufferSize = IdleMeterBufferLength;
                base.Socket.SendBufferSize = IdleMeterBufferLength;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while DeInitBufferKeepAlive_TCPStream", ex);
            }
        }

        public void CleanUpBuffer()
        {
            try
            {
                localBuffer = null;
            }
            catch (Exception)
            {
            }
        }

        public bool IsTCPInactivity(TimeSpan MaxInactivityDuration)
        {
            if (LastActivityDuration >= MaxInactivityDuration &&
                MaxInactivityDuration != TimeSpan.MinValue)
                return true;
            else
                return false;
        }

        public bool TryProcessHeartBeat()
        {
            bool Process_HeartBeat = false;
            try
            {   
                //check read is Non-block
                if (CommunicationMode == TCP_Communication.CommunicationMode.IdleAliveMode && DataAvailable)
                {
                    int length = base.Read(LocalBuffer, 0, LocalBuffer.Length);
                    int byteLengthParsed = DataReceiverThread(LocalBuffer, 0, length);
                    //Try Send Heart Beat Response    
                    if (LastStreamState == HeartBeatReceive)
                    {
                        //read_RetryCount++;
                        Process_HeartBeat = true;
                    }
                }
            }
            catch (Exception)
            { 
               
            }
            return Process_HeartBeat;
        }

        public bool TryWriteHeartBeatResponse()
        {
            try
            {
                WriteByte(HeartBeat.HeartBeatResponse);
                base.Flush();
                return true;
            }
            catch (Exception) { }
            return false;
        }

        #region IComparable<TCPStream> Members

        int IComparable<TCPStream>.CompareTo(TCPStream other)
        {
            try
            {
                if (Socket.RemoteEndPoint.Equals(other.Socket.RemoteEndPoint))
                    return 0;
                else
                {
                    int comVal_Add = ((IPEndPoint)Socket.RemoteEndPoint).Address.Address.CompareTo(((IPEndPoint)other.Socket.RemoteEndPoint).Address.Address);
                    if (comVal_Add == 0)
                        return ((IPEndPoint)Socket.RemoteEndPoint).Port.CompareTo(((IPEndPoint)other.Socket.RemoteEndPoint).Port);
                    else
                        return comVal_Add;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            try
            {
                try
                {
                    base.Dispose(disposing);
                }
                catch
                { }
                localBuffer = null;
                internalException = new ObjectDisposedException(String.Format("TCP Stream Object is already disposed {0}", remortPort));
                #region //Remove HeartBeatReceived Event Handlers

                Delegate[] Handlers = null;
                if (HeartBeatReceived != null)
                {
                    Handlers = HeartBeatReceived.GetInvocationList();
                    foreach (Delegate item in Handlers)
                    {
                        HeartBeatReceived -= (Action<HeartBeat>)item;
                    }
                }

                #endregion
                #region //Remove InActivityTimeOut Event Handlers

                //if (InActivityTimeOut != null)
                //{
                //    Handlers = InActivityTimeOut.GetInvocationList();
                //    foreach (Delegate item in Handlers)
                //    {
                //        InActivityTimeOut -= (Action<DateTime>)item;
                //    }
                //}

                #endregion
                #region //Remove StreamDisconnected Event Handlers

                if (StreamDisconnected != null)
                {
                    Handlers = StreamDisconnected.GetInvocationList();
                    foreach (Delegate item in Handlers)
                    {
                        StreamDisconnected -= (Action)item;
                    }
                }

                #endregion
            }
            catch (Exception)
            { }
        }

        #endregion

        public override void Close()
        {
            try
            {
                if (Socket != null && Socket.Connected)
                {
                    this.Socket.Shutdown(SocketShutdown.Both);
                    this.Socket.Close();
                }
                base.Close();
                Dispose(true);
            }
            catch
            { }
        }

        public override string ToString()
        {
            try
            {
                return RemortPort.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    public enum CommunicationMode : byte
    {
        IdleAliveMode = 0,
        ActiveIOSessionMode = 1
    }
}
