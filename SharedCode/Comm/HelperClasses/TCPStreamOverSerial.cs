using DLMS;
using SharedCode.Common;
using SharedCode.TCP_Communication;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using System.Threading;

namespace SharedCode.Comm.HelperClasses
{
    public class TCPStreamOverSerial : Stream, IComparable<TCPStreamOverSerial>
    {
        #region Data_Members

        private WrapperLayer tcpWrapper;
        private HeartBeat heartBeatPacket;
        private List<byte> readBuffer;
        private Exception InnerException;
        private TimeSpan _InActiveTime = new TimeSpan(0, 0, 30, 0, 0);
        private TimeSpan lastActivityTime = new TimeSpan();
        private bool isbusyReceive = false;
        private bool isInactivity = false;
        private SerialPort SPort;
        /// <summary>
        /// Read Method Arguments
        /// </summary>
        private ArrayList MethodCallParameters;
        public int BufferLength = 1024;
        #region TCP_IPStream_State
        private int streamState = NotReceive;
        private int lastStreamState = NotReceive;
        public static readonly int NotReceive = 0;
        public static readonly int WrapperReceive = 1;
        public static readonly int HeartBeatReceive = 2;
        public static readonly int DataAPDUReceive = 3;
        public static readonly TimeSpan DefaultInactiveTimeOut = new TimeSpan(0, 20, 0);
        #endregion
        public event Action<HeartBeat> HeartBeatReceived = delegate { };
        public event Action<DateTime> InActivityTimeOut = delegate { };
        public event Action StreamDisconnected = delegate { };

        #endregion

        #region Constructurs

        public TCPStreamOverSerial(SerialPort IOStream)
        {
            try
            {
                readBuffer = new List<byte>(BufferLength);
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

        public TCPStreamOverSerial(SerialPort IOStream, bool ownsSocket)
        {
            try
            {
                readBuffer = new List<byte>(BufferLength);
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

        public bool IsbusyReceive
        {
            get { return isbusyReceive; }
            set
            {
                SetIsBusyReceiverStatus(value);
            }
        }

        public bool IsInActivity
        {
            get { return isInactivity; }
            set
            {
                if (value)
                {
                    isInactivity = value;
                    if (InActivityTimeOut != null)
                        InActivityTimeOut.Invoke(DateTime.Now);
                }
                SetIsInActivity(value);
            }
        }

        public TimeSpan LastActivityTime
        {
            get { return lastActivityTime; }
            set { lastActivityTime = value; }
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

        public int LastStreamState
        {
            get { return lastStreamState; }
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
        #endregion

        #region Member Methods

        public override void Write(byte[] buffer, int offset, int size)
        {
            try
            {
                this.TcpWrapper.PacketLength = (ushort)size;
                byte[] t = TcpWrapper.EncodeWrapper();
                byte[] tLargeBuf = new byte[(size - offset) + t.Length];
                Buffer.BlockCopy(t, 0, tLargeBuf, 0, t.Length);
                Buffer.BlockCopy(buffer, offset, tLargeBuf, 8, (size - offset));
                ///base.Write(t, 0, 8);
                Thread.Sleep(50);
                InternalStream.Write(tLargeBuf, 0, tLargeBuf.Length);
                //foreach (var dt in tLargeBuf)
                //{
                //    InternalStream.WriteByte(dt);
                //    Thread.Sleep(5);
                //}
                InternalStream.Flush();
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
                this.TcpWrapper.PacketLength = (ushort)size;
                byte[] t = TcpWrapper.EncodeWrapper();
                byte[] tLargeBuf = new byte[(size - offset) + t.Length];
                Buffer.BlockCopy(t, 0, tLargeBuf, 0, t.Length);
                Buffer.BlockCopy(buffer, offset, tLargeBuf, 8, (size - offset));
                Thread.Sleep(50);
                //foreach (var dt in tLargeBuf)
                //{
                //    InternalStream.WriteByte(dt);
                //    Thread.Sleep(05);
                //}
                IAsyncResult toRet = InternalStream.BeginWrite(tLargeBuf, 0, tLargeBuf.Length, callback, state);
                InternalStream.Flush();
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
                InternalStream.EndWrite(asyncResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
        {
            try
            {
                #region Check Initial Condition
                if (buffer == null)
                    throw new ArgumentNullException("Null Buffer Passed");
                if (offset < 0 || size < 0)
                    throw new ArgumentOutOfRangeException();
                if (offset + size > buffer.Length)
                    throw new ArgumentException("Buffer Length is smaller than passed in");
                ///____ ++++ ____
                TCPStreamResult Result = new TCPStreamResult();

                ///Init ASynResult
                Result.ASynCallBack = callback;
                Result.Buffer = buffer;
                Result.Size = size;
                Result.OffSet = offset;

                if (this == state || true)
                    Result.AsyncState = this;
                else
                    Result.AsyncState = state;

                if (streamState == TCPStream.NotReceive && InnerException != null)
                {
                    Exception ex = InnerException;
                    InnerException = null;
                    throw ex;
                }
                #endregion
                //Thread tcpListenerThread_CalBck = new Thread(ASyncCallBckMethodThread);
                //tcpListenerThread_CalBck.Start(Result);
                MethodCallParameters.Add(Result);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override int EndRead(IAsyncResult asyncResult)
        {
            TCPStreamResult asyncResultT = null;
            try
            {
                asyncResultT = (TCPStreamResult)asyncResult;
                byte[] readBufferMethod = asyncResultT.Buffer;
                int readBufOffSet = asyncResultT.OffSet;
                int readBufSize = asyncResultT.Size;

                ///Read Data From Internal Buffer
                int i = 0;
                int count_read = 0;
                Interlocked.Exchange(ref count_read, readBuffer.Count);
                if (count_read >= 0 && InnerException == null)
                {
                    for (int index = readBufOffSet; (i < readBufSize) && (readBufOffSet + readBufSize <= readBufferMethod.Length) && (i < count_read); index++, i++)
                    {
                        readBufferMethod[index] = readBuffer[i];
                    }
                    lock (readBuffer)
                    {
                        readBuffer.RemoveRange(0, i);
                    }
                }
                else if (InnerException != null)
                {
                    asyncResultT.InnerException = InnerException;
                    InnerException = null;
                    throw asyncResultT.InnerException;
                }
                return i;
            }
            catch (Exception ex)
            {
                if (readBuffer != null)
                    readBuffer.Clear();
                throw ex;
            }
            finally
            {
                if (asyncResultT != null)
                {
                    asyncResultT.IsCompleted = true;
                    asyncResultT.CompletedSynchronously = false;
                    MethodCallParameters.Remove(asyncResultT);
                }
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            try
            {
                int bytesRead = -1;

                if (buffer == null)
                    throw new ArgumentNullException("Null Buffer Passed");
                if (offset < 0 || count < 0)
                    throw new ArgumentOutOfRangeException();
                if (offset + count > buffer.Length)
                    throw new ArgumentException("Buffer Length is smaller than passed in");
                //for(int i=0;i<10;i++)
                DataReceiverThread();
                return readBuffer.Count;

                //TCPStreamResult result = (TCPStreamResult)this.BeginRead(buffer, offset, size, null, this);
                //if (this.CanTimeout)
                //    result.AsyncWaitHandle.WaitOne(this.ReadTimeout, false);
                //else
                //    result.AsyncWaitHandle.WaitOne(Timeout.Infinite, false);
                //int byteCount = this.EndRead(result);
                //result.CompletedSynchronously = true;
                //int byteCount = SPort.Read(buffer, offset, size);
                //return byteCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }


            /////return base.Read(buffer, offset, size);
            //try
            //{
            //    #region Check Initial Conditions
            //    if (buffer == null)
            //        throw new ArgumentNullException("Null Buffer Passed");
            //    if (offset < 0 || size < 0)
            //        throw new ArgumentOutOfRangeException();
            //    if (offset + size > buffer.Length)
            //        throw new ArgumentException("Buffer Length is smaller than passed in");

            //    if (streamState == TCPStream.NotReceive && InnerException != null)
            //    {
            //        Exception ex = InnerException;
            //        InnerException = null;
            //        throw ex;
            //    }
            //    #endregion
            //    this.SyncWaitHandle.Reset();
            //    ///Do Wait Work Here
            //    try
            //    {
            //        ///this.WaitHandle = new  base.Cre
            //        if (base.CanTimeout)
            //            SyncWaitHandle.WaitOne(base.ReadTimeout);
            //        else
            //            SyncWaitHandle.WaitOne(-1);
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new IOException("Data read time out occurred");
            //    }


            //    int i = 0;
            //    if (readBuffer.Count > 0 && InnerException != null)
            //    {
            //        for (int index = offset; (i < size) && (index + size < buffer.Length) && (i < readBuffer.Count); index++, i++)
            //        {
            //            buffer[index] = readBuffer[i];
            //        }
            //        readBuffer.RemoveRange(0, i + 1);
            //    }
            //    else
            //    {
            //        throw InnerException;
            //    }
            //    return i + 1;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

        }

        public override int ReadByte()
        {
            //return 0;
            throw new NotSupportedException("ReadByte method not supported on stream");
            //return base.ReadByte();
        }

        public override void WriteByte(byte value)
        {
            ///base.WriteByte(value);
            throw new NotSupportedException("WriteByte method not supported on stream");
        }

        #endregion

        #region Support_Methods
        #region Data_ReceiverThread

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DataReceiverThread()
        {
            try
            {
                IsbusyReceive = true;
                streamState = NotReceive;
                lastStreamState = NotReceive;
                int PacketLengthReceive = 0;
                int currentPosition = 0;
                List<byte> TBuffer = new List<byte>(255);
                byte[] TArray = new byte[HeartBeat.HeartBeatMaxLength + 20];

                if (Connected)
                {
                    ///Read Available Data From Stream
                    if (SPort.BytesToRead > 0)
                    {
                        while (SPort.BytesToRead > 0)
                        {
                            int tLastByte = 0;
                            ///Read Single Byte
                            if (streamState == NotReceive)
                            {
                                tLastByte = InternalStream.Read(TArray, currentPosition, 1);
                                ///TArray[currentPosition] = (byte)tLastByte;
                                ///Change State
                                if (tLastByte == -1)
                                {
                                    currentPosition = 0;
                                    for (int index = 0; TArray != null && index < TArray.Length; index++)
                                    {
                                        TArray[index] = 0;
                                    }
                                    streamState = NotReceive;
                                    InnerException = new IOException("Packet not verified");
                                    InvokeResponse();
                                }
                                else
                                {
                                    tLastByte = TArray[currentPosition];
                                    currentPosition++;
                                }
                                ///Complete Data received Verify either Wrapper or Heart Beat Received
                                if (currentPosition >= WrapperLayer.WrapperHeaderLength && streamState == NotReceive)
                                {
                                    TcpWrapper.DecodeWrapper(TArray, 0, TArray.Length);
                                    HeartBeat hbeat = null;

                                    hbeat = ObjectFactory.GetHeartBeatObject();
                                    hbeat.DecodeHeartBeat(TArray, 0, currentPosition);
                                    ///TCP Wrapper Received SET State To TCPWrapper & Packet Length > 0
                                    if (TcpWrapper.IsVerified)
                                    {
                                        ///Reset Stream Header
                                        currentPosition = 0;

                                        streamState = NotReceive;
                                        if (TcpWrapper.PacketLength > 0)
                                        {
                                            streamState = TCPStream.WrapperReceive;
                                            PacketLengthReceive = this.TcpWrapper.PacketLength;
                                            TArray = new byte[PacketLengthReceive];
                                        }
                                        ///Reset Stream If Wrapper of Length Zero received
                                        else
                                        {
                                            streamState = NotReceive;
                                            InnerException = new IOException("TCP Wrapper Verified,Invalid Packet Length 0");
                                            InvokeResponse();
                                            ///ResetStream();
                                        }
                                        for (int index = 0; TArray != null && index < TArray.Length; index++)
                                        {
                                            TArray[index] = 0;
                                        }
                                    }
                                    ///Heart Beat Received SET HBeat Status
                                    else if (hbeat.IsVerifited)
                                    {
                                        ///Reset Stream Header
                                        currentPosition = 0;
                                        lastStreamState = TCPStream.HeartBeatReceive;
                                        for (int index = 0; TArray != null && index < TArray.Length; index++)
                                        {
                                            TArray[index] = 0;
                                        }
                                        streamState = NotReceive;

                                        streamState = TCPStream.HeartBeatReceive;
                                        this.HeartBeatPacket = hbeat;
                                    }
                                    else if ((!hbeat.IsVerifited && hbeat.ExpectedPacketLength > currentPosition))
                                    {
                                        continue;
                                    }
                                    ///Reset Stream State to Not Receive After Flush All Data
                                    else
                                    {
                                        ///Reset Stream Header
                                        currentPosition = 0;
                                        for (int index = 0; TArray != null && index < TArray.Length; index++)
                                        {
                                            TArray[index] = 0;
                                        }
                                        streamState = NotReceive;
                                        InnerException = new IOException("TCP Wrapper Not Verified");
                                        InvokeResponse();
                                        ResetStream();
                                    }
                                }
                            }
                            if (streamState == TCPStream.WrapperReceive)
                            {
                                lastStreamState = TCPStream.WrapperReceive;
                                byte[] tArray = null;
                                int dataAvailLength = 0;
                                int offset = 0;
                                int tlength = 0;
                                while ((PacketLengthReceive - tlength) > 0)
                                {
                                    //IAsyncResult res = InternalStream.BeginRead(tArray, offset, (PacketLengthReceive - tlength), null, InternalStream);
                                    //TimeSpan t = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(20 * tArray.Length));
                                    //bool signal = res.AsyncWaitHandle.WaitOne(t, false);
                                    //int len = InternalStream.EndRead(res);

                                    int len = InternalStream.Read(TArray, offset, 1);
                                    tlength += len;
                                    offset += len;
                                    dataAvailLength = SPort.BytesToRead;
                                    ///Stil Data to read
                                    if ((PacketLengthReceive - tlength) > 0 &&
                                        (dataAvailLength > 0))// || signal))
                                    {
                                        continue;
                                    }
                                    else
                                        break;
                                }
                                //Console.Out.WriteLine("Data Receiver Thread Check Point_2");
                                if (PacketLengthReceive == tlength)    ///Read Data Available On Stream
                                {
                                    lock (readBuffer)
                                    {
                                        readBuffer.AddRange(TArray);
                                    }
                                    InnerException = null;
                                }
                                else
                                {
                                    this.InnerException = new IOException("Packet not completly received,invalid packet length");
                                    InvokeResponse();
                                    ResetStream();
                                }
                                streamState = TCPStream.NotReceive;
                                lastStreamState = TCPStream.DataAPDUReceive;
                                InvokeResponse();
                                ///break;
                                //Console.Out.WriteLine("Data Receiver Thread Check Point_3");
                                ///Check Here That Complete Packet Received Here
                                ///Store Last Activity Time
                                LastActivityTime = DateTime.Now.TimeOfDay;
                            }
                            if (streamState == TCPStream.HeartBeatReceive)
                            {
                                byte[] t = HeartBeat.EncodeHeartBeat();
                                InternalStream.Write(t, 0, t.Length);
                                streamState = TCPStream.NotReceive;
                                ///Invoke HeartBeat Received
                                HeartBeatReceived.Invoke(HeartBeatPacket);
                            }
                        }
                        ///Verifty Either Some Packet Received Here
                        if (LastStreamState == NotReceive)
                        {
                            Exception _InnerException = new IOException("Error reading data,may be Packet not completly received");
                            InnerException = _InnerException;
                            InvokeResponse();
                            ///to abort Current thread
                            ///throw _InnerException;
                        }
                        /////Check Here That Complete Packet Received Here
                        /////Store Last Activity Time
                        //LastActivityTime = DateTime.Now.TimeOfDay;
                    }
                    //else if (IsInActivity)
                    //{
                    //   InvokeResponse();
                    //}
                    ///Data Not Available Here
                    else
                    {
                        Exception _InnerException = new IOException("Error reading data,may be connection timeout");
                        InnerException = _InnerException;
                        InvokeResponse();
                        ///to abort Current thread
                        /// throw _InnerException;
                    }
                }
                ///IO Channel Not Connected
                else
                {
                    Exception _InnerException = new IOException("Error reading data IO connection disconnected");
                    InnerException = _InnerException;
                    if (StreamDisconnected != null)
                        StreamDisconnected.Invoke();
                    InvokeResponse();
                    ///to abort Current thread
                    /// throw _InnerException;
                }
            }
            catch (Exception ex)
            {
                streamState = NotReceive;
                InnerException = ex;
                ///ResetStream();
                InvokeResponse();
                ///throw InnerException;
            }
            finally
            {
                IsbusyReceive = false;
            }
        }


        public void DataReceiverThread(Object StateInfo)
        {
            //DataReceiverThread();
        }

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

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void InvokeResponse()
        {
            try
            {
                ///Try to loop over Thread
                //if (LastStreamState == HeartBeatReceive)
                //{

                //}
                //else 
                if ((LastStreamState == WrapperReceive || LastStreamState == TCPStream.DataAPDUReceive)
                    && InnerException == null)
                {
                    if (MethodCallParameters.Count > 0)
                    {
                        TCPStreamResult InvokeParam = (TCPStreamResult)MethodCallParameters[0];
                        MethodCallParameters.RemoveAt(0);
                        if (!InvokeParam.IsCompleted)
                        {
                            InvokeParam.IsCompleted = true;
                            if (InvokeParam.ASyncWaitHandler != null)
                            {
                                InvokeParam.ASyncWaitHandler.Set();
                            }
                            if (InvokeParam.ASynCallBack != null)
                            {
                                InvokeParam.InnerException = null;
                                InvokeParam.ASynCallBack.Invoke(InvokeParam);
                            }
                        }
                        MethodCallParameters.Remove(InvokeParam);
                    }
                    //else
                    //{
                    //    throw new Exception("Error Invoking Response On TCP Stream");
                    //}
                }
                else
                {
                    if (MethodCallParameters.Count > 0)
                    {
                        TCPStreamResult InvokeParam = (TCPStreamResult)MethodCallParameters[0];
                        MethodCallParameters.RemoveAt(0);
                        if (!InvokeParam.IsCompleted)
                        {
                            InvokeParam.IsCompleted = true;
                            if (InvokeParam.ASyncWaitHandler != null)
                            {
                                InvokeParam.ASyncWaitHandler.Set();
                            }
                            if (InvokeParam.ASynCallBack != null)
                            {
                                InvokeParam.InnerException = InnerException;
                                InvokeParam.ASynCallBack.Invoke(InvokeParam);
                            }
                        }
                        MethodCallParameters.Remove(InvokeParam);
                    }

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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
                        readBuffer.Clear();
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

        public void ASyncCallBckMethodThread(Object IAsyncResult)
        {
            try
            {
                TCPStreamResult result = (TCPStreamResult)IAsyncResult;
                TCPStream state = (TCPStream)result.AsyncState;
                if (result != null)
                {
                    if (state.CanTimeout)
                        result.ASyncWaitHandler.WaitOne(state.ReadTimeout, false);
                    else
                        result.ASyncWaitHandler.WaitOne(Timeout.Infinite, false);
                }
                if (result.ASynCallBack != null)
                    result.ASynCallBack.Invoke(result);
            }
            catch (Exception ex)
            {
                Thread.CurrentThread.Abort();
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SignalAll()
        {
            try
            {
                if (MethodCallParameters == null && MethodCallParameters.Count <= 0)
                    return;
                foreach (var Param in MethodCallParameters)
                {
                    try
                    {
                        if (Param != null)
                        {
                            TCPStreamResult Result = (TCPStreamResult)Param;
                            if (Result != null && Result.ASyncWaitHandler != null)
                            {
                                Result.ASyncWaitHandler.Set();
                                Result.IsCompleted = true;
                                Result.InnerException = InnerException;
                                if (Result.ASynCallBack != null)
                                    Result.ASynCallBack.Invoke(Result);
                            }
                        }
                    }
                    catch (Exception)
                    { }
                }
            }
            catch (Exception ex) { }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Signal()
        {
            try
            {
                if (MethodCallParameters == null && MethodCallParameters.Count <= 0)
                    return;
                InvokeResponse();
            }
            catch (Exception ex) { }
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                base.Dispose(disposing);
                readBuffer = null;
                if (MethodCallParameters != null)
                {
                    InnerException = new ObjectDisposedException("TCPStreamOverSerial", "Object is already disposed off");
                    new Action(SignalAll).BeginInvoke(null, this);
                }
                ///Remove All Relevant Event Handlers
                #region ///Remove HeartBeatReceived Event Handlers
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
                #region ///Remove InActivityTimeOut Event Handlers
                if (InActivityTimeOut != null)
                {
                    Handlers = InActivityTimeOut.GetInvocationList();
                    foreach (Delegate item in Handlers)
                    {
                        InActivityTimeOut -= (Action<DateTime>)item;
                    }
                }
                #endregion
                #region ///Remove StreamDisconnected Event Handlers
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
            {

            }

        }

        public void CleanUpBuffer()
        {
            try
            {
                if (readBuffer != null && streamState == NotReceive && readBuffer.Count > BufferLength)
                {
                    ResetStream();
                    readBuffer.Clear();
                    readBuffer.Capacity = BufferLength;
                }
            }
            catch (Exception)
            {
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SetIsBusyReceiverStatus(bool status)
        {
            isbusyReceive = status;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SetIsInActivity(bool status)
        {
            isInactivity = status;
        }

        #endregion

        #region IComparable<TCPStream> Members

        int IComparable<TCPStreamOverSerial>.CompareTo(TCPStreamOverSerial other)
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
            return base.ToString();
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
