using DLMS;
using ServerToolkit.BufferManagement;
using SharedCode.Common;
using SharedCode.TCP_Communication;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharedCode.Comm.HelperClasses
{
    public class TCPOverSerial : Stream, IComparable<TCPOverSerial>
    {
        public static readonly WrapperLayer DefaultTcpWrapper = new WrapperLayer(0x01, 0x01);
        public static readonly WrapperLayer DefaultTcpWrapper_Public = new WrapperLayer(0x01, 0x10);

        #region Event & Delegate

        internal Action<ArraySegment<byte>> _ReceiveDataFromPhysicalLayerASync = delegate { };

        #endregion

        #region Data_Members

        public static TimeSpan LastIOMaxLimit = new TimeSpan(0, 0, 45);
        private WrapperLayer tcpWrapper;
        private HeartBeat heartBeatPacket;
        private Exception InnerException;
        private TimeSpan _InActiveTime = new TimeSpan(0, 0, 30, 0, 0);
        private TimeSpan lastActivityTime = DateTime.Now.TimeOfDay;
        private long _totalBytesSent;
        private long _totalBytesReceived;

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

        public static readonly int MaxMeterBufferLength = 1024;
        public static readonly int IdleMeterBufferLength = 512;

        public event Action<HeartBeat> HeartBeatReceived = delegate { };
        public event Action<DateTime> InActivityTimeOut = delegate { };
        public event Action StreamDisconnected = delegate { };

        private Exception internalException;
        private ArraySegment<byte> localBuffer;
        private ArraySegment<byte> localBufferArg;

        private IBuffer readBuffer;
        private GetDataReaderBuffer _GetDataReaderBuffer = null;

        private EndPoint remortPort = null;
        private SerialPort SPort;
        private ArrayList MethodCallParameters;

        #endregion

        void SPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                //DataReceiverThread();
            }
            catch (Exception ex)
            { }
        }


        #region Constructurs

        public TCPOverSerial(SerialPort IOStream)
        {
            try
            {
                //readBuffer = new Memoryb(); //new List<byte>(BufferLength);
                TcpWrapper = new WrapperLayer();

                HeartBeatPacket = ObjectFactory.GetHeartBeatObject();

                MethodCallParameters = new ArrayList();
                MethodCallParameters = ArrayList.Synchronized(MethodCallParameters);
                SPort = IOStream;
                if (!SPort.IsOpen)
                {
                    SPort.Open();
                }
                SPort.DataReceived += new SerialDataReceivedEventHandler(SPort_DataReceived);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCPOverSerial(SerialPort IOStream, bool ownsSocket)
        {
            try
            {
                //readBuffer = new List<byte>(BufferLength);
                TcpWrapper = new WrapperLayer();

                HeartBeatPacket = ObjectFactory.GetHeartBeatObject();
                MethodCallParameters = new ArrayList();
                MethodCallParameters = ArrayList.Synchronized(MethodCallParameters);
                SPort = IOStream;
                if (!SPort.IsOpen)
                {
                    SPort.Open();
                }
                SPort.DataReceived += new SerialDataReceivedEventHandler(SPort_DataReceived);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Properties

        public long TotalBytesSent
        {
            get { return _totalBytesSent; }
            set
            {
                _totalBytesSent = value + 40;
            }
        }

        public long TotalBytesReceived
        {
            get { return _totalBytesReceived; }
            set
            {
                _totalBytesReceived = value + 40;
            }
        }

        protected IBuffer ReadBuffer
        {
            get { return readBuffer; }
            set { readBuffer = value; }
        }

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
        public Stream InternalStream
        {
            get
            {
                try
                {
                    return SPort.BaseStream;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting internal IO Stream related to COM Port", ex);
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
                    if (SPort != null && SPort.IsOpen)
                        return true;
                    else
                        return false;
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
            set { _CommunicationMode = value; }
        }

        internal ArraySegment<byte> LocalBuffer
        {
            get
            {
                try
                {
                    IBuffer localReadBuffer = null;
                    localReadBuffer = ReadBuffer;
                    InitReadBuffer();
                    ArraySegment<byte> localArraySegment = localBuffer;
                    if (localReadBuffer != ReadBuffer || localArraySegment == null)
                    {
                        localArraySegment = readBuffer.GetSegments()[0];
                        if (localArraySegment != localBuffer)
                            localBuffer = localArraySegment;
                    }
                    return localBuffer;
                }
                catch (Exception ex)
                {
                    throw new Exception("Invalid IO IBuffer supplied", ex);
                }
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
        public event Action<ArraySegment<byte>> ReceiveDataFromPhysicalLayerASync
        {
            add
            {
                List<Delegate> InvokeList = null;
                if (_ReceiveDataFromPhysicalLayerASync != null)
                    InvokeList = new List<Delegate>(_ReceiveDataFromPhysicalLayerASync.GetInvocationList());

                if (_ReceiveDataFromPhysicalLayerASync == null ||
                    !InvokeList.Contains(value))
                {
                    _ReceiveDataFromPhysicalLayerASync += value;
                }
            }
            remove
            {
                if (_ReceiveDataFromPhysicalLayerASync == null)
                    _ReceiveDataFromPhysicalLayerASync -= value;
                // return _ReceiveDataFromPhysicalLayerASync;
            }
        }

        public bool OverlayMode
        {
            get;
            set;
        }

        #endregion

        #region Member_Methods

        public bool TryProcessHeartBeat()
        {
            bool Process_HeartBeat = false;
            try
            {
                //check read is Non-block
                if (CommunicationMode == TCP_Communication.CommunicationMode.IdleAliveMode)// && DataAvailable)
                {
                    int length = InternalStream.Read(LocalBuffer.Array, 0, LocalBuffer.Array.Length);
                    int byteLengthParsed = DataReceiverThread(LocalBuffer.Array, 0, length);
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

        public override void Write(byte[] buffer, int offset, int size)
        {
            try
            {
                var Local_Array = LocalBuffer;

                // By Pass Wrapper Encode
                if (OverlayMode)
                {
                    // Copy Encoded Data On Write Buffer 
                    Buffer.BlockCopy(buffer, offset, Local_Array.Array, Local_Array.Offset, size);
                }
                else
                {
                    this.TcpWrapper.PacketLength = (ushort)size;
                    byte[] t = TcpWrapper.EncodeWrapper();

                    // Copy Encoded Data On Write Buffer 
                    Buffer.BlockCopy(t, 0, Local_Array.Array, Local_Array.Offset, t.Length);
                    Buffer.BlockCopy(buffer, offset, Local_Array.Array, Local_Array.Offset + t.Length, size);

                    size = size + t.Length;
                }

                var Buffer_Arg = new ArraySegment<byte>(Local_Array.Array, Local_Array.Offset, size);
                InternalStream.Write(Buffer_Arg.Array, Buffer_Arg.Offset, Buffer_Arg.Count);
                InternalStream.Flush();
                LastActivityTime = DateTime.Now.TimeOfDay;
                // count transfer bandwidth
                TotalBytesSent += size;
            }
            catch (Exception ex)
            {
                Exception Cust_Exception = null;
                int error_code = -1;
                if (Process_IO_OperationError(ex, ref Cust_Exception, ref error_code))
                    if (Cust_Exception != null)
                        throw Cust_Exception;
                    else
                        throw ex;
                else
                    throw ex;
            }
        }

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
        {
            IAsyncResult toRet = null;


            try
            {
                InternalException = null;

                #region Initial Condition Check

                if (buffer == null || buffer.Length <= 0)
                    throw new ArgumentNullException("buffer");
                if (offset < 0 || offset >= buffer.Length)
                    throw new ArgumentException("offset");
                if (size <= 0)
                    throw new ArgumentException("size");
                if (offset + size > buffer.Length)
                    throw new ArgumentException("Invalid offset and size argument");

                #endregion

                var Local_Array = LocalBuffer;

                // By Pass Wrapper Encode
                if (OverlayMode)
                {
                    // Copy Encoded Data On Write Buffer 
                    Buffer.BlockCopy(buffer, offset, Local_Array.Array, Local_Array.Offset, size);
                }
                else
                {
                    this.TcpWrapper.PacketLength = (ushort)size;
                    byte[] t = TcpWrapper.EncodeWrapper();

                    // Copy Encoded Data On Write Buffer 
                    Buffer.BlockCopy(t, 0, Local_Array.Array, Local_Array.Offset, t.Length);
                    Buffer.BlockCopy(buffer, offset, Local_Array.Array, Local_Array.Offset + t.Length, size);

                    size = (size + t.Length);
                }

                var Buffer_Arg = new ArraySegment<byte>(Local_Array.Array, Local_Array.Offset, size);

                toRet = base.BeginWrite(Buffer_Arg.Array, Buffer_Arg.Offset, Buffer_Arg.Count, callback, state);

                LastActivityTime = new TimeSpan();
                TotalBytesSent += size;
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
                ArraySegment<byte> _localBuffer = LocalBuffer;

                // Check Physical Channel Condition
                if (!Connected || InternalException != null)
                {
                    if (InternalException == null)
                        throw new IOException("Physical Channel is disconnected");
                    else
                        throw InternalException;
                }
                var IAsyncResult = base.BeginRead(_localBuffer.Array, _localBuffer.Offset, _localBuffer.Count, callback, state);

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
                ArraySegment<byte> _localBuffer = LocalBuffer;
                int byteLengthParsed = DataReceiverThread(_localBuffer.Array, _localBuffer.Offset, _localBuffer.Count);

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
                /// Error Case
                TotalBytesReceived += length;
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
                if ((offset + size) > buffer.Length)
                    throw new ArgumentException("Invalid offset and size argument");

                #endregion

                // int read_RetryCount = 0;
                // repeat till Complete Packet Received
                TimeSpan timeOutLimit = TimeSpan.MaxValue;
                if (CanTimeout)
                    timeOutLimit = DateTime.Now.TimeOfDay + TimeSpan.FromMilliseconds(ReadTimeout);     // maxTimeOutLimit

                while (true)
                {
                    localBufferArg = new ArraySegment<byte>(buffer, offset, size);
                    ArraySegment<byte> _localBuffer = LocalBuffer;
                    int length = InternalStream.Read(_localBuffer.Array, _localBuffer.Offset, _localBuffer.Count);
                    int byteLengthParsed = DataReceiverThread(_localBuffer.Array, _localBuffer.Offset, length);

                    // Count total bandwidth usage
                    TotalBytesReceived += length;
                    if (LastStreamState == DataAPDUReceive)
                        return localBufferArg.Count;
                    else if (LastStreamState == HeartBeatReceive)
                    {
                        if (DateTime.Now.TimeOfDay < timeOutLimit)
                            continue;
                        else
                            break;
                    }
                    else
                        break;
                }

                if (LastStreamState != DataAPDUReceive &&
                    InternalException != null)
                {
                    throw InternalException;
                }

                if (LastStreamState != DataAPDUReceive)
                    throw new IOException(String.Format("IO Operation Connection timed out or remote host has failed to respond {0}", ToString()));
                // error case
                return -1;
            }
            catch (Exception ex)
            {
                Exception Cust_Exception = null;
                int error_code = -1;
                if (Process_IO_OperationError(ex, ref Cust_Exception, ref error_code))
                    if (Cust_Exception != null)
                        throw Cust_Exception;
                    else
                        throw ex;
                else
                    throw ex;
            }
            finally
            {
                LastActivityTime = DateTime.Now.TimeOfDay;
                InternalException = null;
            }
        }

        public override int ReadByte()
        {
            TotalBytesReceived += 1;
            return base.ReadByte();
        }

        public override void WriteByte(byte value)
        {
            ///base.WriteByte(value);
            InternalStream.Write(new byte[] { value }, 0, 1);
            TotalBytesSent += 1;
        }

        #endregion

        #region Support_Methods

        #region Data_ReceiverThread

        [MethodImpl(MethodImplOptions.Synchronized)]
        public int DataReceiverThread(byte[] arrBuf, int Offset, int length)
        {
            bool isHeartBeatRe = false;
            InternalException = null;
            byte[] TArray = new byte[8];
            int PacketLengthReceive = 0;
            int offset = Offset;
            HeartBeat hbeat = null;


                hbeat = ObjectFactory.GetHeartBeatObject();

            try
            {
                while (arrBuf != null && offset < arrBuf.Length && (offset - Offset) < length)
                {
                    #region Try_Parsing_HeaderWrapper

                    try
                    {
                        int dtLength = 0;
                        {
                            int dataLength = (length <= arrBuf.Length) ? length : arrBuf.Length;
                            dtLength = (Offset + dataLength) - offset;
                        }

                        if (this.TcpWrapper != null)
                            this.TcpWrapper.IsVerified = false;

                        if (WrapperLayer.WrapperHeaderLength <= dtLength ||
                            HeartBeat.HeartBeatMaxLength <= dtLength)
                        {
                            dtLength = TArray.Length;
                            Buffer.BlockCopy(arrBuf, offset, TArray, 0, dtLength);

                            // Complete Data received Verify either Wrapper Received
                            if (!OverlayMode)
                            {
                                int packetLengthLocal = 0;
                                TcpWrapper.DecodeWrapper(TArray, 0, dtLength);

                                // Try DefaultTcpWrapper
                                if (!TcpWrapper.IsVerified)
                                {
                                    if (TCPStream.DefaultTcpWrapper.TryDecodeWrapper(TArray, 0, dtLength, out packetLengthLocal))
                                    {
                                        TcpWrapper.PacketLength = (ushort)packetLengthLocal;
                                        TcpWrapper.IsVerified = true;
                                    }
                                }
                                // Try DefaultTcpWrapper_Public
                                if (!TcpWrapper.IsVerified)
                                {
                                    if (TCPStream.DefaultTcpWrapper_Public.TryDecodeWrapper(TArray, 0, dtLength, out packetLengthLocal))
                                    {
                                        TcpWrapper.PacketLength = (ushort)packetLengthLocal;
                                        TcpWrapper.IsVerified = true;
                                    }
                                }
                            }

                            hbeat.DecodeHeartBeat(TArray, 0, dtLength);
                            // Increment array OffSet
                            if (tcpWrapper.IsVerified)
                            {
                                offset += WrapperLayer.WrapperHeaderLength;
                            }
                            else if (hbeat.IsVerifited)
                            {
                                offset += HeartBeat.HeartBeatMaxLength;
                            }
                        }
                        else if (OverlayMode && dtLength > 0)
                        {
                            // don't raise error
                            InternalException = null;
                        }
                        else // if (dtLength <= 0)
                        {
                            offset = -1;
                            InternalException = new IOException("IO Packet is not verified DataReceiverThread TCP Over Serial Stream");
                            throw InternalException;
                        }
                    }
                    catch (Exception)
                    {
                        StreamState = NotReceive;
                        LastStreamState = StreamState;
                        throw;
                    }

                    #endregion

                    // TCP Wrapper Received 
                    // SET State To TCPWrapper & Packet Length > 0
                    if (TcpWrapper.IsVerified)
                    {
                        // Reset Stream Header
                        if (TcpWrapper.PacketLength > 0)
                        {
                            StreamState = TCPStream.WrapperReceive;
                            PacketLengthReceive = this.TcpWrapper.PacketLength;
                        }
                        // Reset Stream If Wrapper of Length Zero received
                        else
                        {
                            StreamState = NotReceive;
                            InternalException = new IOException("TCP Wrapper is Verified,Invalid Packet Length 0,DataReceiverThread_TCPStream");
                        }
                        LastStreamState = StreamState;
                    }
                    // Heart Beat Received SET HBeat Status
                    else if (hbeat.IsVerifited)
                    {
                        StreamState = TCPStream.HeartBeatReceive;
                        LastStreamState = StreamState;
                        this.HeartBeatPacket = hbeat;
                    }
                    else if (OverlayMode)
                    {
                        StreamState = NotReceive;
                        LastStreamState = StreamState;
                    }
                    // Reset Stream State to Not Receive After Flush All Data
                    else
                    {
                        // Reset Stream Header
                        StreamState = NotReceive;
                        LastStreamState = StreamState;
                        InternalException = InternalException = new IOException("TCP Wrapper is not Verified " + ToString()
                            + " DataReceiverThread_TCPStream");
                        break;
                    }

                    if (StreamState == TCPStream.WrapperReceive)
                    {
                        InternalException = null;
                        StreamState = TCPStream.NotReceive;
#if Enable_DEBUG_ECHO
                        // Console.Out.WriteLine("Data Receiver Thread Check Point_1");
#endif
                        int dataAvailLength = length - (offset - Offset);
                        int tLength = 0;

                        if (dataAvailLength >= PacketLengthReceive)
                            tLength = PacketLengthReceive;  // tArray = new byte[PacketLengthReceive];
                        else
                            tLength = dataAvailLength;      // tArray = new byte[dataAvailLength];

                        InternalException = null;
                        // InvokeResponse(arrBuf, offset, ref tLength);
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
                            // Try Read Data From Internal Buffer
                            for (int index = readBufOffSet;
                                    (index <= last_readBufferIndex) &&
                                    (i < (offset + tLength));
                                    index++, i++)
                            {
                                readBufferMethod[index] = arrBuf[i];
                                count_read++;
                            }
                            tLength = count_read;
                            // Update localBufferArg
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
                            String Error_Message = String.Format("Packet is not completely received,invalid packet length {0} DataReceiverThread_TCPStream", ToString());

                            this.InternalException = new IOException(Error_Message);
                            throw InternalException;
                        }
                        LastStreamState = TCPStream.DataAPDUReceive;
                        LastActivityTime = DateTime.Now.TimeOfDay;
                        // break Communication On TCP_Wrapper Receive
                        if (CommunicationMode == CommunicationMode.ActiveIOSessionMode)
                            break;
                    }
                    if (StreamState == TCPStream.HeartBeatReceive)
                    {
                        InternalException = null;
                        if (!isHeartBeatRe &&
                            CommunicationMode != CommunicationMode.ActiveIOSessionMode)
                        {
                            TryWriteHeartBeatResponse();
                        }
                        isHeartBeatRe = true;
                        StreamState = TCPStream.NotReceive;
                        // Invoke HeartBeat Received
                        HeartBeatReceived.Invoke(HeartBeatPacket);
                    }
                    else if (OverlayMode &&
                             streamState == TCPStream.NotReceive)
                    {
                        InternalException = null;
                        streamState = TCPStream.NotReceive;
                        lastStreamState = TCPStream.DataAPDUReceive;

                        int dataAvailLength = length - (offset - Offset);
                        int tLength = dataAvailLength;

                        try
                        {
                            // data read in localBufferArg
                            if (localBufferArg.Array != null &&
                                arrBuf == localBufferArg.Array)
                            {
                                localBufferArg = new ArraySegment<byte>(arrBuf, offset, tLength);
                            }
                            else
                            {
                                // Copy_Data_Receive Into buffer Argument
                                #region Copy_Data

                                if (localBufferArg == null ||
                                    localBufferArg.Offset + localBufferArg.Count < tLength)
                                    throw new ArgumentNullException("buffer");

                                byte[] readBufferMethod = localBufferArg.Array;
                                int readBufOffSet = localBufferArg.Offset;
                                int readBufSize = localBufferArg.Count;
                                int last_readBufferIndex = (readBufOffSet + readBufSize);

                                int i = offset;
                                int count_read = 0;
                                // Try Read Data From Internal Buffer
                                for (int index = readBufOffSet;
                                        (index <= last_readBufferIndex) &&
                                        (i < (offset + tLength));
                                        index++, i++)
                                {
                                    readBufferMethod[index] = arrBuf[i];
                                    count_read++;
                                }
                                tLength = count_read;
                                // Update localBufferArg
                                localBufferArg = new ArraySegment<byte>(readBufferMethod, readBufOffSet, count_read);

                                #endregion
                            }
                        }
                        catch (Exception ex)
                        {
                            InnerException = new Exception("Error Occurred while copying data read buffer", ex);
                            throw InnerException;
                        }

                        offset += tLength;
                        lastStreamState = TCPStream.DataAPDUReceive;
                        LastActivityTime = DateTime.Now.TimeOfDay;

                        // terminate Header Parsing Loop
                        break;
                    }
                }

                // Verify Either Some Packet Received Here
                if (LastStreamState == NotReceive ||
                    (LastStreamState != DataAPDUReceive &&
                     InternalException != null))
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
                ResetStream();
                throw;
            }
            return offset;
        }

        #endregion

        public void ResetStream()
        {
            try
            {
                streamState = NotReceive;
                lastStreamState = NotReceive;
                InnerException = null;
                if (readBuffer != null)
                {
                    lock (readBuffer)
                    {
                        readBuffer = null;
                    }
                }
                if (MethodCallParameters != null)
                {
                    this.MethodCallParameters.Clear();
                }
                else
                    MethodCallParameters = ArrayList.Synchronized(new ArrayList());
                ///Discard All TCP Serial Port Receive And transmit Buffers
                if (SPort != null && SPort.IsOpen)
                {
                    SPort.DiscardOutBuffer();
                    SPort.DiscardInBuffer();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InitBuffer(int maxWriteBuffer = 512, int maxReadBuffer = 512,
                       GetDataReaderBuffer DataReaderBufferFactoryArg = null)
        {
            try
            {
                CommunicationMode = TCP_Communication.CommunicationMode.ActiveIOSessionMode;
                BufferLength = maxReadBuffer;

                //// Increase Default Buffer Size On Condition Only
                //if (MaxMeterBufferLength < BufferLength)
                //{
                //    base.Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendBuffer, BufferLength);
                //    base.Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, BufferLength);
                //}

                if (_GetDataReaderBuffer == null &&
                    DataReaderBufferFactoryArg == null)
                    throw new ArgumentNullException("Null Argument DataReaderBufferFactory");
                _GetDataReaderBuffer = DataReaderBufferFactoryArg;
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
                readBuffer = null;

                BufferLength = IdleMeterBufferLength;

                /// Set Buffer For Idle Meter Connection
                /// base.Socket.ReceiveBufferSize = IdleMeterBufferLength;
                /// base.Socket.SendBufferSize = IdleMeterBufferLength;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while DeInitBufferKeepAlive_TCPStream", ex);
            }
        }

        private void InitReadBuffer()
        {
            if (readBuffer == null ||
                readBuffer.IsDisposed ||
                readBuffer.Size < BufferLength)
            {
                if (readBuffer != null)
                    readBuffer.Dispose();
                readBuffer = _GetDataReaderBuffer.Invoke(BufferLength);
            }
        }

        public void CleanUpBuffer()
        {
            try
            {
                readBuffer = null;
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

        public bool TryProcessReceiveDataASync()
        {
            bool Process_HeartBeat = false;
            try
            {
                // Check Read is Non-block
                if ((CommunicationMode == CommunicationMode.IdleAliveMode || OverlayMode))// &&  DataAvailable)
                {
                    ArraySegment<byte> _LocalBuffer = LocalBuffer;
                    localBufferArg = _LocalBuffer;
                    int length = InternalStream.Read(_LocalBuffer.Array, _LocalBuffer.Offset, _LocalBuffer.Count);
                    int byteLengthParsed = DataReceiverThread(_LocalBuffer.Array, _LocalBuffer.Offset, length);
                    // Try Send Heart Beat Response    
                    TotalBytesReceived += length;
                    if (LastStreamState == HeartBeatReceive)
                    {
                        Process_HeartBeat = true;
                    }
                    else if (LastStreamState == DataAPDUReceive &&
                             localBufferArg.Array != null &&
                             localBufferArg.Count > 0)
                    {
                        if (_ReceiveDataFromPhysicalLayerASync != null)
                            _ReceiveDataFromPhysicalLayerASync.Invoke(localBufferArg);
                    }
                }
            }
            catch
            {
            }
            return Process_HeartBeat;
        }

        public bool TryWriteHeartBeatResponse()
        {
            try
            {
                InternalStream.Write(HeartBeat.HeartBeatResponse, 0, HeartBeat.HeartBeatResponse.Length);
                InternalStream.Flush();
                return true;
            }
            catch (Exception) { }
            return false;
        }

        public static bool Process_IO_OperationError(Exception ex, ref Exception CusException, ref int WSA_ErrorCode)
        {
            Exception ex_Internal = null;
            Type Ex_Type = typeof(Exception);
            try
            {
                ex_Internal = ex;
                if (ex != null)
                {
                    Ex_Type = typeof(IOException);
                    CusException = null;
                }
                //while (ex_Internal != null && ex_Internal.GetType() != typeof(SocketException))
                //{
                //    ex_Internal = ex_Internal.InnerException;
                //}
                //Not SocketException
                //if (ex_Internal == null || ex_Internal.GetType() != typeof(SocketException))
                //{
                //    CusException = null;
                //    WSA_ErrorCode = -1;
                //    return false;
                //}
                //else
                {
                    String Error_Message = "";

                    Error_Message = ex_Internal.Message;
                    CusException = (Exception)Activator.CreateInstance(Ex_Type, new object[] { Error_Message, ex });
                    //SocketException ex_ = (SocketException)ex_Internal;
                    //WSA_ErrorCode = ex_.NativeErrorCode;
                    //switch (ex_.SocketErrorCode)
                    //{
                    //    #region Custom_Socket_ErrorMessages

                    //    //10060 WSAETIMEDOUT
                    //    case SocketError.TimedOut:
                    //        {
                    //            Error_Message = "IO Operation Connection timed out or remote host has failed to respond";
                    //            CusException = (Exception)Activator.CreateInstance(Ex_Type, new object[] { Error_Message, ex });
                    //            break;
                    //        }
                    //    //10036 WSAEINPROGRESS
                    //    case SocketError.AlreadyInProgress:
                    //        {
                    //            Error_Message = "already an IO Operation is currently executing";
                    //            CusException = (Exception)Activator.CreateInstance(Ex_Type, new object[] { Error_Message, ex });
                    //            break;
                    //        }
                    //    //10051 WSAENETUNREACH
                    //    case SocketError.NetworkUnreachable:
                    //    //10052 WSAENETRESET
                    //    case SocketError.NetworkReset:
                    //    case SocketError.NetworkDown:
                    //    //10054 WSAECONNRESET
                    //    case SocketError.ConnectionReset:
                    //        {
                    //            Error_Message = "IO Channel is disconnected";
                    //            CusException = (Exception)Activator.CreateInstance(Ex_Type, new object[] { Error_Message, ex });
                    //            break;
                    //        }
                    //    //10053 WSAECONNABORTED
                    //    case SocketError.ConnectionAborted:
                    //    //10057 WSAENOTCONN
                    //    case SocketError.NotConnected:
                    //    //10058 WSAESHUTDOWN
                    //    case SocketError.Disconnecting:
                    //        {
                    //            Error_Message = "IO Channel is disconnected by local-host";
                    //            CusException = (Exception)Activator.CreateInstance(Ex_Type, new object[] { Error_Message, ex });
                    //            break;
                    //        }
                    //    default:
                    //        {
                    //            Error_Message = ex_.Message;
                    //            CusException = (Exception)Activator.CreateInstance(Ex_Type, new object[] { Error_Message, ex });
                    //            break;
                    //        }

                    //        #endregion
                    //}
                    if (CusException == null && Error_Message != null)
                        CusException = new Exception(Error_Message, ex);
                }
                return true;
            }
            catch
            { }
            //Error Case
            CusException = null;
            WSA_ErrorCode = -1;
            return false;
        }

        #region IComparable<TCPStream> Members

        int IComparable<TCPOverSerial>.CompareTo(TCPOverSerial other)
        {
            try
            {
                string str = SPort.PortName;
                string str_1 = other.SPort.PortName;
                return str.CompareTo(str_1);
            }
            catch (Exception ex)
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
                readBuffer = null;
                _GetDataReaderBuffer = null;
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
                #region // Remove _ReceiveDataFromPhysicalLayerASync Event Handlers

                Handlers = null;
                if (_ReceiveDataFromPhysicalLayerASync != null)
                {
                    Handlers = _ReceiveDataFromPhysicalLayerASync.GetInvocationList();
                    foreach (Delegate item in Handlers)
                    {
                        ReceiveDataFromPhysicalLayerASync -= (Action<ArraySegment<byte>>)item;
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
            catch
            { }
        }

        #endregion

        public override void Close()
        {
            base.Close();
            if (SPort != null)
            {
                this.SPort.Close();
            }
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

        #region Parent Class Over Loads
        public override bool CanRead
        {
            get { return SPort != null && SPort.IsOpen; }
        }

        public override bool CanSeek
        {
            get { throw new NotImplementedException(); }
        }

        public override bool CanWrite
        {
            get { return SPort != null && SPort.IsOpen; }
        }

        public override void Flush()
        {
            InternalStream.Flush();
        }

        public override long Length
        {
            get { throw new NotImplementedException(); }
        }

        public override long Position
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return 0;
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
