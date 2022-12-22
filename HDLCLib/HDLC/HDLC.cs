using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _HDLC;
using System.Runtime.CompilerServices;
using System.Threading;

namespace _HDLC
{
    public delegate void TransmitData(byte[] frame);

    public partial class HDLC : IDisposable
    {
        #region Event & Delegate

        internal TransmitData _TransmitFrame = delegate { };
        internal TransmitData _ReceiveEventNotification = delegate { };
        internal Action _HDLCDisconnected = delegate { };

        /// <summary>
        /// Public Event To Notify HDLC Disconnection
        /// </summary>
        public event Action HDLCDisconnected
        {
            add
            {
                List<Delegate> InvokeList = null;
                if (_HDLCDisconnected != null)
                    InvokeList = new List<Delegate>(_HDLCDisconnected.GetInvocationList());

                if (_HDLCDisconnected == null ||
                    !InvokeList.Contains(value))
                {
                    _HDLCDisconnected += value;
                }
            }
            remove
            {
                if (_HDLCDisconnected != null)
                    _HDLCDisconnected -= value;
            }
        }

        /// <summary>
        /// Public Event To Notify Event Notification Packet Handler
        /// </summary>
        public event TransmitData ReceiveEventNotification
        {
            add
            {
                List<Delegate> InvokeList = null;
                if (_ReceiveEventNotification != null)
                    InvokeList = new List<Delegate>(_ReceiveEventNotification.GetInvocationList());

                if (_ReceiveEventNotification == null ||
                    !InvokeList.Contains(value))
                {
                    _ReceiveEventNotification += value;
                }
            }
            remove
            {
                if (_ReceiveEventNotification != null)
                    _ReceiveEventNotification -= value;
            }
        }

        /// <summary>
        /// Events
        /// Public Event To Notify TransmitFrame Packet Handler
        /// </summary>
        public event TransmitData TransmitFrame
        {
            add
            {
                List<Delegate> InvokeList = null;
                if (_TransmitFrame != null)
                    InvokeList = new List<Delegate>(_TransmitFrame.GetInvocationList());

                if (_TransmitFrame == null ||
                    !InvokeList.Contains(value))
                {
                    _TransmitFrame += value;
                }
            }
            remove
            {
                if (_TransmitFrame != null)
                    _TransmitFrame -= value;
            }
        }

        #endregion

        // Test Condition Delegate
        internal Func<bool> ResponseCompCondition = null;
        internal Func<bool> FrameReceiveCompCondition = null;
        internal Func<bool> IsNotIOBusyCondition = null;

        #region DataMembers

        private ushort _maxInfoBufTransmit;
        private ushort _maxInfoBufReceive;
        private ushort _transmitWinSize;
        private ushort _receiveWinSize;

        public const ushort DefaultMaxTryResend = 03;
        private ushort _maxTryResend = DefaultMaxTryResend;

        private HDLCTimers _timers;
        private uint _destinationAddr;
        private uint _sourceAddr;

        private byte[] _RawDestinationAddress = null;
        private byte[] _RawSourceAddress = null;

        private readonly HDLCStream _BaseStream;

        /// <summary>
        /// Protocol Status
        /// </summary>
        private bool _connected;
        private HDLCState _hdlcStatus;
        private ushort _transmitWindowCount;
        private ushort _receivingWindowCount;
        private List<byte> ReceiveBuffer = null;
        private List<byte> TransmitBuffer = null;
        private List<byte> UIReceiveBuffer = null;
        private bool isRightToSend;

        /// <summary>
        /// Supporting Variables
        /// </summary>
        private List<byte> receivedRawData;
        private int expectedByteCount;
        private ushort maxFrameSize;

        private bool frameReceived = false;
        private bool responseComplete = false;
        private List<HDLCFrame> framesReceived = null;

        // Information Frame Received
        private HDLCFrame controlFrame;
        private HDLCFrame TransmitedFrame;

        private int frameUnAckNo = 0;
        private int frameAck;
        private bool isError;
        private bool isErrorUI;
        private bool isBusy;
        private bool writeReadInProcess;
        private string ErrorString = null;
        private bool isSkipLoginParameter = false;

        #endregion

        #region Constructors

        public HDLC()
        {
            MaxInfoBufReceive = 0x80;
            MaxInfoBufTransmit = 0x80;
            MaxUIFrameSize = 0x80;
            TransmitWinSize = 7;
            ReceiveWinSize = 7;

            _timers = new HDLCTimers();
            _timers.SendRRInActivityTimeOut += new Action<DateTime>(_timers_SendRRInActivityTimeOut);
            _timers.InActivityTimeOut += new Action<DateTime>(_timers_InActivityTimeOut);

            // Test Conditions
            this.ResponseCompCondition = new Func<bool>(() => _timers.IsResponseTimeOut ||
                                                              responseComplete || isError);

            this.FrameReceiveCompCondition = new Func<bool>(() => _timers.IsResponseTimeOut || frameReceived || isError);

            this.IsNotIOBusyCondition = new Func<bool>(() => !IsIOBusy);

            TransmitWindowCount = 0;
            ReceivingWindowCount = 0;
            receivedRawData = new List<byte>(MaxInfoBufReceive + 200);
            ReceiveBuffer = new List<byte>(1500);
            TransmitBuffer = new List<byte>(1500);
            UIReceiveBuffer = new List<byte>();

            _TransmitFrame = delegate { };
            framesReceived = new List<HDLCFrame>(10);
            setHDLCStatus(HDLCState.Ready);
            expectedByteCount = int.MinValue;
            setFrameReceived(false);
            _BaseStream = new HDLCStream(this);
        }

        #endregion

        #region Properties

        public HDLCStream BaseStream
        {
            get { return _BaseStream; }
        }

        public ushort MaxInfoBufTransmit
        {
            get { return _maxInfoBufTransmit; }
            set { _maxInfoBufTransmit = value; }
        }

        public ushort MaxInfoBufReceive
        {
            get { return _maxInfoBufReceive; }
            set { _maxInfoBufReceive = value; }
        }

        public ushort MaxReceiveSize
        {
            get
            {
                return 2030;
            }
        }

        public ushort MaxUIFrameSize
        {
            get { return maxFrameSize; }
            set
            {
                if (value > 2048)
                    throw new HDLCErrorException("Maximum UI Frame Size Exceeds Upper Limit");
                maxFrameSize = value;
            }
        }

        public ushort TransmitWinSize
        {
            get { return _transmitWinSize; }
            set { _transmitWinSize = value; }
        }

        public ushort ReceiveWinSize
        {
            get { return _receiveWinSize; }
            set { _receiveWinSize = value; }
        }

        public ushort MaxRetrySend
        {
            get { return _maxTryResend; }
            set { _maxTryResend = value; }
        }

        public bool IsEnableRetrySend
        {
            get { return (_maxTryResend > 0); }
            set
            {
                if (value)
                {
                    if (_maxTryResend <= 0)
                        _maxTryResend = DefaultMaxTryResend;
                }
                else
                    _maxTryResend = 0;
            }
        }


        public TimeSpan InactivityTimeOut
        {
            get { return _timers.InactivityTimeOut; }
            set { _timers.InactivityTimeOut = value; }
        }

        public TimeSpan ReqResTimeOut
        {
            get { return _timers.ReqResTimeOut; }
            set { _timers.ReqResTimeOut = value; }
        }

        public bool Connected
        {
            get { return _connected; }
            set
            {
                if (!value)
                {
                    if (_connected)
                    {
                        _connected = false;
                        _HDLCDisconnected.Invoke();
                    }
                }
                _connected = value;
            }
        }

        public HDLCState HdlcStatus
        {
            get { return _hdlcStatus; }
        }

        private ushort TransmitWindowCount
        {
            get { return _transmitWindowCount; }
            set
            {
                _transmitWindowCount = value;
                _transmitWindowCount %= 8;
            }
        }

        private ushort ReceivingWindowCount
        {
            get { return _receivingWindowCount; }
            set
            {
                _receivingWindowCount = value;
                _receivingWindowCount %= 8;
            }
        }

        public uint SourceAddress
        {
            get { return _sourceAddr; }
            set { _sourceAddr = value; }
        }

        public AddressLength DestinationAddressLength
        {
            get;
            set;
        }

        public uint DestinationAddress
        {
            get { return _destinationAddr; }
            set { _destinationAddr = value; }
        }

        public ushort DestinationSAP
        {
            get
            {
                return (ushort)((DestinationAddress >> 16) & 0xFFFF);
            }
            set
            {
                uint t = DestinationAddress;
                t = t & 0x0000FFFF;
                t = t | (uint)(value << 16);
                DestinationAddress = t;
            }
        }

        public bool AvoidInactivityTimeOut
        {
            get { return _timers.AvoidInactivityTimeOut; }
            set { _timers.AvoidInactivityTimeOut = value; }
        }

        public bool IsIOBusy
        {
            get { return isBusy; }
        }

        public bool IsWriteReadInProcess
        {
            get { return writeReadInProcess; }
        }

        public bool IsSkipLoginParameter
        {
            get { return isSkipLoginParameter; }
            set { isSkipLoginParameter = value; }
        }

        #endregion

        #region Methods

        public void Connect()
        {
            try
            {
                setFrameReceived(false);
                setError(false);
                setWriteReadInProcess(true);
                _timers.IsInactivityEnable = false;

                HDLCFrame SNRMFrame = new HDLCFrame(FrameType.SNRM);

                SNRMFrame.P_F = true;
                SNRMFrame.IsSkipAPDU = IsSkipLoginParameter;

                SNRMFrame.APDU_Buf_Transmit = MaxInfoBufTransmit;
                SNRMFrame.APDU_Buf_Receive = MaxInfoBufReceive;
                SNRMFrame.WinSizeTransmit = (byte)TransmitWinSize;
                SNRMFrame.WinSizeReceive = (byte)ReceiveWinSize;

                // Setup HDLC Destination Address
                SNRMFrame.DestinationAddressLength = DestinationAddressLength;
                SNRMFrame.UpperHDLCDestAddress = (ushort)((DestinationAddress >> 16) & 0xFFFF);
                SNRMFrame.LowerHDLCDestAddress = (ushort)(DestinationAddress & 0xFFFF);
                SNRMFrame.ClientHDLCSourceAddress = (byte)((SourceAddress) & 0xFFFF);

                // Encoded Raw Destination Address
                if (SNRMFrame.DestinationHDLCAddress != null &&
                    SNRMFrame.DestinationHDLCAddress.Length > 0)
                    this._RawDestinationAddress = (byte[])SNRMFrame.DestinationHDLCAddress.Clone();
                // Encoded Raw Source Address
                if (SNRMFrame.SourceHDLCAddress != null &&
                    SNRMFrame.SourceHDLCAddress.Length > 0)
                    this._RawSourceAddress = (byte[])SNRMFrame.SourceHDLCAddress.Clone();

                byte[] _frame = SNRMFrame.ToByteArray();
                TransmitedFrame = SNRMFrame;
                _TransmitFrame.Invoke(_frame);

                setFrameReceived(false);
                setHDLCStatus(HDLCState.Ready);
                setExpectedByteCount();

                _timers.IsResponseTimeOut = false;
                _timers.IsResponseTimeOut = HDLC.HDLCTimers.PreciseDelayUntil(FrameReceiveCompCondition, ReqResTimeOut);

                if (controlFrame._FrameType == FrameType.UA && !isError)
                {
                    HDLCFrame fUA = controlFrame;

                    MaxInfoBufTransmit = (ushort)((fUA.APDU_Buf_Receive <= 0) ? _maxInfoBufTransmit : fUA.APDU_Buf_Receive);
                    MaxInfoBufReceive = (ushort)((fUA.APDU_Buf_Transmit <= 0) ? _maxInfoBufReceive : fUA.APDU_Buf_Transmit);
                    TransmitWinSize = (byte)((fUA.WinSizeReceive <= 0) ? _transmitWinSize : fUA.WinSizeReceive);
                    ReceiveWinSize = (byte)((fUA.WinSizeTransmit <= 0) ? _receiveWinSize : fUA.WinSizeTransmit);

                    MaxInfoBufTransmit -= 2; // AHMED

                    // Reset Window Counters
                    TransmitWindowCount = 0;
                    ReceivingWindowCount = 0;
                    _connected = true;
                }
                else
                {
                    if (isError)
                        throw new HDLCErrorException("Unable To Connect," + ErrorString);

                    if (_timers.IsResponseTimeOut && !frameReceived)
                        throw new HDLCErrorException("Unable To Connect,No valid Frame Received");
                    else if (_timers.IsResponseTimeOut)
                        throw new HDLCErrorException("Unable To Connect,Request Response Timeout");
                }
            }
            catch (Exception ex)
            {
                if (isError)
                    throw new HDLCErrorException("Unable To Connect," + ErrorString);
                throw ex;
            }
            finally
            {
                setWriteReadInProcess(false);
                // Enable InActivity Procedure
                if (_connected)
                {
                    _timers.IsInactivityEnable = true;
                    _timers.InActivityTimerEnable = true;
                }
            }
        }

        public void Disconnect()
        {
            try
            {
                setWriteReadInProcess(true);

                setFrameReceived(false);
                _timers.IsInactivityEnable = false;

                HDLCFrame DiscFrame = new HDLCFrame(FrameType.DISC);
                DiscFrame.P_F = true;

                // DiscFrame.DestinationAddressLength = DestinationAddressLength;
                // DiscFrame.UpperHDLCDestAddress = (ushort)((DestinationAddress >> 16) & 0xFFFF);
                // DiscFrame.LowerHDLCDestAddress = (ushort)(DestinationAddress & 0xFFFF);
                // DiscFrame.ClientHDLCSourceAddress = (byte)((SourceAddress >> 16) & 0xFFFF);

                DiscFrame.DestinationAddressLength = DestinationAddressLength;
                // Set HDLC Frame Address
                if (_RawDestinationAddress != null && _RawDestinationAddress.Length > 0 &&
                    _RawSourceAddress != null && _RawSourceAddress.Length > 0)
                {
                    DiscFrame.DestinationHDLCAddress = _RawDestinationAddress;
                    DiscFrame.SourceHDLCAddress = _RawSourceAddress;
                }
                else
                {
                    DiscFrame.UpperHDLCDestAddress = (ushort)((DestinationAddress >> 16) & 0xFFFF);
                    DiscFrame.LowerHDLCDestAddress = (ushort)(DestinationAddress & 0xFFFF);
                    DiscFrame.ClientHDLCSourceAddress = (byte)((SourceAddress) & 0xFFFF);
                }

                byte[] _frame = DiscFrame.ToByteArray();
                _TransmitFrame.Invoke(_frame);

                //_timers.IsResponseTimeOut = false;
                //_timers.IsResponseTimeOut = HDLC.HDLCTimers.PreciseDelayUntil(FrameReceiveCompCondition, ReqResTimeOut);
                Thread.Sleep(20);
                if (Connected)
                {
                    ResetHDLC();
                    _HDLCDisconnected.Invoke();
                }
                ResetHDLC();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                setWriteReadInProcess(false);
                //ResetHDLC(); //Azeem [july change]
            }
        }

        public void ResetHDLC()
        {
            try
            {
                _connected = false;
                _hdlcStatus = HDLCState.Ready;
                _transmitWindowCount = 0x00;
                _receivingWindowCount = 0x00;
                _RawDestinationAddress = null;
                _RawSourceAddress = null;

                ReceiveBuffer = new List<byte>();
                TransmitBuffer = new List<byte>();
                UIReceiveBuffer = new List<byte>();
                receivedRawData = new List<byte>();

                expectedByteCount = int.MinValue;
                frameReceived = false;
                responseComplete = false;

                framesReceived = new List<HDLCFrame>();      ///Information Frame Received
                controlFrame = new HDLCFrame();
                TransmitedFrame = new HDLCFrame();

                frameUnAckNo = 0;
                frameAck = 0;
                isError = false;
                isErrorUI = false;
                isBusy = false;
                writeReadInProcess = false;
                ErrorString = null;

                // Disable Inactivity Timers
                _timers.InActivityTimerEnable = false;
                _timers.IsInactivityEnable = true;
                // _timers.RequestResponseTimerEnable = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Write(byte[] Array, int index, long length)
        {
            try
            {
                if (!_connected)
                    throw new HDLCErrorException("HDLC Disconnected");

                #region // Busy Waiting or TimeOut

                bool isWaitTimeOut = false;
                if (isBusy)
                    isWaitTimeOut = HDLC.HDLCTimers.PreciseDelayUntil(IsNotIOBusyCondition, ReqResTimeOut);

                if (isWaitTimeOut)
                    throw new HDLCErrorException("IO Busy,unable to write"); 

                #endregion

                _timers.IsInactivityEnable = false;
                setIOBusy(true);
                setWriteReadInProcess(true);

                TransmitBuffer.Clear();
                ReceiveBuffer.Clear();
                setFrameResponseComplete(false);
                setError(false);

                long lastIndex = ((index + length) < Array.Length) ? (index + length) : length;
                for (int Index = index; Index < Array.Length && Index < lastIndex; Index++)  ///Copy Data To Local Buffer
                    TransmitBuffer.Add(Array[Index]);

                int dataSize = TransmitBuffer.Count;
                int TotalFrames = (int)Math.Ceiling(dataSize / (MaxInfoBufTransmit * 1.0f));
                int maxDataNACK = TransmitWinSize * MaxInfoBufTransmit;

                int FrameNo = 0;
                int frameIndex = 0;
                int resendTries = 0;
                bool _pf = false;


                while (FrameNo < TotalFrames && frameIndex < dataSize)
                {
                    frameIndex = FrameNo * MaxInfoBufTransmit;
                    int fsize = (dataSize - frameIndex >= MaxInfoBufTransmit) ? MaxInfoBufTransmit : dataSize - frameIndex;
                    byte[] _apdu = new byte[fsize];
                    int indexT = 0;

                    for (int indeX = frameIndex; indeX < (frameIndex + fsize); indeX++, indexT++) /// Copy Data From Transmit Buffer
                        _apdu[indexT] = TransmitBuffer[indeX];

                    HDLCFrame Frame = new HDLCFrame(FrameType.I);
                    if ((dataSize - frameIndex) > MaxInfoBufTransmit)
                        Frame.IsSegmented = true;

                    _pf = (maxDataNACK - ((frameIndex % maxDataNACK) + MaxInfoBufTransmit) <= 0) ? true : false;

                    if (FrameNo > 0)
                    {
                        // Do Not Attach APDU Wrapper
                        Frame.IsInfoFirstFrame = false;
                    }
                    Frame.P_F = _pf;
                    Frame.APDU = _apdu;

                    Frame.DestinationAddressLength = DestinationAddressLength;

                    // Set HDLC Frame Address
                    if (_RawDestinationAddress != null && _RawDestinationAddress.Length > 0 &&
                        _RawSourceAddress != null && _RawSourceAddress.Length > 0)
                    {
                        Frame.DestinationHDLCAddress = _RawDestinationAddress;
                        Frame.SourceHDLCAddress = _RawSourceAddress;
                    }
                    else
                    {
                        Frame.UpperHDLCDestAddress = (ushort)((DestinationAddress >> 16) & 0xFFFF);
                        Frame.LowerHDLCDestAddress = (ushort)(DestinationAddress & 0xFFFF);
                        Frame.ClientHDLCSourceAddress = (byte)((SourceAddress) & 0xFFFF);
                    }

                    Frame.ReceiveWindowNo = (byte)(ReceivingWindowCount % 8);
                    Frame.TransmitWindowNo = (byte)(TransmitWindowCount % 8);

                    setFrameReceived(false);
                    byte[] _frameBytes = Frame.ToByteArray();
                    _TransmitFrame.Invoke(_frameBytes);

                    setHDLCStatus(HDLCState.Ready);
                    setExpectedByteCount();

                    _timers.InActivityTimerEnable = true;
                    TransmitWindowCount++;
                    TransmitedFrame = Frame;

                    // Now Check If We Should Wait For RR
                    if (_pf && Frame.IsSegmented)
                    {
                        _timers.IsResponseTimeOut = false;
                        _timers.IsResponseTimeOut = HDLC.HDLCTimers.PreciseDelayUntil(FrameReceiveCompCondition, ReqResTimeOut);

                        if (isError || !frameReceived)
                        {
                            // Notify Error
                            throw new HDLCErrorException("Error During IO," + ErrorString);
                        }

                        // Check RR or RNR Received
                        if (controlFrame._FrameType == FrameType.RR)
                        {
                            // Compute No Of Frame Acknowledge
                            if (controlFrame.ReceiveWindowNo == ((TransmitWindowCount) % 8)) /// All UnAck accepted
                            {
                                FrameNo++;
                            }
                            else
                            {
                                int factor = controlFrame.ReceiveWindowNo - TransmitWindowCount;
                                if (factor < 0)
                                    factor += 8;
                                frameAck += factor;
                                FrameNo -= TransmitWindowCount - frameAck;
                                TransmitWindowCount = (ushort)frameAck;
                                resendTries++;
                                if (resendTries > TotalFrames * 3)
                                    throw new HDLCErrorException("Unable To Transmit Frames");
                            }
                        }
                        else if (controlFrame._FrameType == FrameType.DM ||
                                controlFrame._FrameType == FrameType.FRMR)
                        {
                            _connected = false;
                            throw new HDLCErrorException("Unable To Transmit Data,HDLC Layer Disconnected");
                        }
                    }
                    else
                    {
                        FrameNo++;
                    }
                }

                #region // Waiting For Response Completion

                ushort retryResend_Count = 1;
            // bool test_Condition = false;

            Repeat_Loop:
                {
                    _timers.IsResponseTimeOut = false;
                    _timers.IsResponseTimeOut = HDLC.HDLCTimers.PreciseDelayUntil(ResponseCompCondition, ReqResTimeOut);
                }

                if (_timers.IsResponseTimeOut || !responseComplete || isError)
                {
                    if (isError)
                    {
                        // Notify Error
                        throw new HDLCErrorException("Error During IO," + ErrorString);
                    }
                    // Retry Resend Request
                    else if (retryResend_Count <= MaxRetrySend &&
                             MaxRetrySend > 0)
                    {
                        SendRR();
                        retryResend_Count++;
                        goto Repeat_Loop;
                    }
                    else
                    {
                        throw new HDLCErrorException("IO Error,Request Response Timeout");
                    }
                }

                #endregion

            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(HDLCErrorException))
                {
                    throw ex;
                }
                else
                    throw new HDLCErrorException("Error Occurred Writing Data", ex);
            }
            finally
            {
                setIOBusy(false);
                setWriteReadInProcess(false);

                // Enable InActivity Procedure
                if (_connected)
                {
                    _timers.IsInactivityEnable = true;
                    _timers.InActivityTimerEnable = true;
                }
            }
        }

        public void Write(byte[] Array)
        {
            try
            {
                Write(Array, 0, Array.Length);
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(HDLCErrorException))
                {
                    throw ex;
                }
                else
                    throw new HDLCErrorException("Error Occurred Writing Data", ex);
                ///throw;
            }
        }

        public byte[] Read()
        {
            //&&
            //ReceiveBuffer.Count > 0
            if (ReceiveBuffer != null )
                return ReceiveBuffer.ToArray();
            else
                return null;
        }

        /// <summary>
        /// Separate Thread Method To Analyze Received Frames
        /// </summary>
        public void ProcessFrames(HDLCFrame frameReceived)
        {
            try
            {
                if (frameReceived.P_F)
                    setRightToSend(true);
                else
                    setRightToSend(false);

                if (frameReceived._FrameType == FrameType.FRMR ||
                    frameReceived._FrameType == FrameType.DM)
                {
                    bool raiseDisconnected = Connected;
                    _connected = false;
                    // Raise HDLC Disconnected
                    if (raiseDisconnected)
                        _HDLCDisconnected.Invoke();
                }

                if (TransmitedFrame._FrameType == FrameType.RR || TransmitedFrame._FrameType == FrameType.RNR)
                {
                    if (frameReceived._FrameType == FrameType.RR ||
                        frameReceived._FrameType == FrameType.RNR ||
                        frameReceived._FrameType == FrameType.FRMR ||
                         frameReceived._FrameType == FrameType.DM)                     /// Response Received RR|RNR
                    {
                        setFrameReceived(true);
                        setIOBusy(false);
                    }
                }

                if (TransmitedFrame._FrameType == FrameType.SNRM ||
                    TransmitedFrame._FrameType == FrameType.DISC)
                {
                    controlFrame = frameReceived;
                    setFrameReceived(true);
                }
                else if (TransmitedFrame._FrameType == FrameType.I)
                {
                    if (frameReceived._FrameType == FrameType.RR ||
                       frameReceived._FrameType == FrameType.FRMR ||
                        frameReceived._FrameType == FrameType.DM)                     /// Response Received RR|RNR
                    {
                        controlFrame = frameReceived;
                        setFrameReceived(true);
                        if (frameReceived._FrameType == FrameType.FRMR ||
                            TransmitedFrame.P_F && !TransmitedFrame.IsSegmented)     /// Process Waiting For Response
                        {
                            ErrorString = "Error During IO,HDLC Disconnected";
                            setError(true);
                        }
                    }
                    else if (frameReceived._FrameType == FrameType.I)
                    {
                        // Verify Transmit & Receiving Window Counter && APDU Size
                        bool flagSendRR = false;
                        bool notVerified = false;
                        if (frameReceived.APDU == null || frameReceived.APDU.Length > MaxInfoBufReceive)
                        {
                            ErrorString = "Receiving Frame APDU exceeds max limits";
                            setError(true);
                            notVerified = true;
                        }
                        if (frameReceived.ReceiveWindowNo != TransmitWindowCount)
                        {
                            ErrorString = "Invalid receiving window count";
                            setError(true);
                            notVerified = true;

                        }
                        if (frameReceived.TransmitWindowNo != ReceivingWindowCount)
                        {
                            ErrorString = "Invalid transmit window count";
                            setError(true);
                            flagSendRR = true;
                            notVerified = true;
                        }
                        if (!notVerified)
                        {
                            this.ReceiveBuffer.AddRange(frameReceived.APDU);
                            ReceivingWindowCount++;
                        }
                        if (frameReceived.P_F &&
                            frameReceived.IsSegmented || flagSendRR)
                        {
                            // Set Request Response Complete Flag
                            _timers.IsResponseTimeOut = false;
                            frameUnAckNo = 0;
                            frameAck = frameReceived.ReceiveWindowNo;
                            HDLC.HDLCTimers.PreciseDelay(250);

                            SendRR();

                            _timers.InActivityTimerEnable = true;
                        }
                        else if (!frameReceived.P_F && frameReceived.IsSegmented && !notVerified)
                        {
                            frameUnAckNo++;
                            if (frameUnAckNo > ReceiveWinSize)
                            {
                                ErrorString = "Error Window Size";
                                setError(true);
                                throw new HDLCErrorException("Error Window Size");
                            }
                        }
                        else if (frameReceived.P_F && !frameReceived.IsSegmented && !notVerified)
                        {
                            ///Set Response Complete
                            frameUnAckNo = 0;
                            setFrameResponseComplete(true);
                        }
                        controlFrame = frameReceived;
                        setFrameReceived(true);
                    }
                    else if (frameReceived._FrameType == FrameType.UI)
                    {
                        ///Do Work For Request Response Timer Out
                        ProcessEventNotification(frameReceived);
                    }
                }
                if (TransmitedFrame._FrameType == FrameType.UI)
                {
                    if (frameReceived._FrameType == FrameType.UI)
                    {
                        ProcessEventNotification(frameReceived);
                    }
                }
            }
            catch (HDLCErrorException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                ///Log Error
                throw new HDLCErrorException("Unknown Error Processing HDLC Frame");
            }
        }

        public void ProcessFrame(List<byte> rawData)
        {
            try
            {
                byte[] _rawdata = rawData.ToArray();
                int index = Array.IndexOf(_rawdata, HDLCFrame.FrameTag); ///Verify Frame
                ushort flength;
                byte[] frame = null;

                if (index != -1 && index + 2 < _rawdata.Length)
                {
                    flength = _rawdata[index + 1];
                    flength = (ushort)(flength & 0x07);
                    flength = (ushort)(flength << 8);
                    flength = (ushort)(flength | _rawdata[index + 2]);

                    int endTagIndex = flength + index + 1;
                    byte[] t = rawData.ToArray();
                    if (flength < t.Length && t[endTagIndex] == HDLCFrame.FrameTag)   ///locating End 0x7E Tag
                    {
                        frame = new byte[flength + 2];
                        Array.Copy(t, index, frame, 0, frame.Length);
                    }
                    else if (flength > t.Length)
                    {
                        rawData.Clear();
                        throw new HDLCErrorException(String.Format("Invalid frameLength received {0}", flength));
                    }
                    rawData.Clear();
                }

                if (frame != null)
                {
                    HDLCFrame Frame = new HDLCFrame(FrameType.I);
                    try
                    {
                        bool isValidated = false;
                        if (this.Connected)
                        {
                            isValidated = Frame.Parse(frame, _RawDestinationAddress, _RawSourceAddress);
                            if (!isValidated)
                            {
                                // HDLC Address Not Match
                                // Raise HDLC Error
                                throw new InvalidOperationException("Invalid Source Or Dstination HDLC Address");
                            }
                        }
                        else
                        {
                            isValidated = Frame.Parse(frame);
                        }

                        ProcessFrames(Frame);
                    }
                    catch (HDLCInValidFrameException ex)
                    {
                        ErrorString = String.Format("Error Parsing HDLC Frame,{0}", ex.Message);
                        setError(true);
                        setUIError(true);
                        throw ex;
                    }
                }
                else
                {
                    ErrorString = "Null data received";
                    setError(true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [MethodImplAttribute(MethodImplOptions.Synchronized)]
        public void ReceiveRawData(byte[] dt, int offset, int length)
        {
            try
            {
                _timers.InActivityTimerEnable = true; // Reset Inactivity Timer On Receiving Data
                // Process Received Bytes
                int tagIndex = 0;
                for (int index = offset; (index < (offset + length) && index < dt.Length); index++)  /// Look for first 0x7E
                {
                    if (HdlcStatus == HDLCState.Ready)  /// Look For Frame Start TAG
                    {
                        if (dt[index] == HDLCFrame.FrameTag &&
                            (expectedByteCount != -1 || expectedByteCount <= int.MinValue))
                        {
                            /// expectedByteCount = -1;
                            setExpectedByteCount(-1);
                            receivedRawData.Add((byte)HDLCFrame.FrameTag);
                            tagIndex = receivedRawData.Count - 1;
                        }
                        else if (HdlcStatus == HDLCState.Ready && expectedByteCount == -1)          /// Store Remain g Data Bytes
                        {
                            receivedRawData.Add(dt[index]);
                            if (receivedRawData.Count - tagIndex >= 3)                              /// Process Length
                            {
                                ushort tLength = 0;
                                tLength = receivedRawData[tagIndex + 1];
                                tLength = (ushort)(tLength << 8);
                                tLength = (ushort)(tLength | receivedRawData[tagIndex + 2]);
                                tLength = (ushort)(tLength & 0x07FF);                               /// 11 byte length in Frame Format

                                if (tLength >= MaxReceiveSize)
                                {
                                    throw new HDLCErrorException(String.Format("Invalid Data Length {0} received to be parsed", tLength));
                                }
                                /// expectedByteCount = tLength - 2;
                                setExpectedByteCount(tLength - 2);
                                setHDLCStatus(HDLCState.Receive);
                            }
                        }
                    }
                    else if (HdlcStatus == HDLCState.Receive)
                    {
                        if (expectedByteCount >= 0)
                        {
                            // expectedByteCount--;
                            setExpectedByteCount(expectedByteCount - 1);

                            // end of frame
                            if (expectedByteCount == -1 &&
                                dt[index] == HDLCFrame.FrameTag)
                            {
                                receivedRawData.Add(HDLCFrame.FrameTag);
                                setHDLCStatus(HDLCState.Ready);
                                /// expectedByteCount = int.MinValue;
                                setExpectedByteCount();
                                ProcessFrame(receivedRawData);
                            }
                            else
                                receivedRawData.Add(dt[index]);
                        }
                        else
                        {
                            throw new HDLCErrorException("Invalid Data Length received to be parsed");
                        }
                    }
                    else
                    {
                        throw new HDLCErrorException("Invalid HDLC Receiving State");
                    }
                }
            }

            #region Catch & Finally

            catch (Exception ex)
            {
                ErrorString = String.Format("Unable To Receive HDLC Frame,Details {0}", ex.Message);
                setError(true);
                receivedRawData.Clear();

                setHDLCStatus(HDLCState.Ready);
                setExpectedByteCount();

                if (ex.GetType() == typeof(HDLCInValidFrameException)
                    || ex.GetType() == typeof(HDLCErrorException))
                    throw ex;
                else
                    throw new HDLCErrorException(ErrorString, ex);
            }
            finally
            {
                if (HdlcStatus == HDLCState.Ready && expectedByteCount != -1)
                    setExpectedByteCount();
            }

            #endregion
        }

        #region Boolean Protocol Status Updater

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void setIOBusy(bool flag)
        {
            isBusy = flag;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void setWriteReadInProcess(bool flag)
        {
            writeReadInProcess = flag;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void setUIError(bool flag)
        {
            isErrorUI = flag;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void setRightToSend(bool flag)
        {
            isRightToSend = flag;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void setError(bool flag)
        {
            isError = flag;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void setHDLCStatus(HDLCState state)
        {
            _hdlcStatus = state;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void setExpectedByteCount(int byteCount = int.MinValue)
        {
            expectedByteCount = byteCount;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void setFrameReceived(bool flag)
        {
            frameReceived = flag;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void setFrameResponseComplete(bool flag)
        {
            responseComplete = flag;
        }

        #endregion

        #endregion

        #region IDisposable Members

        ~HDLC()
        {
            disposeOff();
        }

        private void disposeOff()
        {
            try
            {
                this._connected = false;
                this.framesReceived = null;
                this.receivedRawData = null;
                this.ReceiveBuffer = null;
                this.TransmitBuffer = null;

                if (_timers != null)
                    _timers.Dispose();
                _timers = null;



                #region // Remove TransmitFrame Event Handlers

                Delegate[] Handlers = null;
                if (_TransmitFrame != null)
                {
                    Handlers = _TransmitFrame.GetInvocationList();
                    foreach (Delegate item in Handlers)
                    {
                        TransmitFrame -= (TransmitData)item;
                    }
                }

                #endregion
                #region // Remove ReceiveEventNotification Event Handlers

                Handlers = null;
                if (_ReceiveEventNotification != null)
                {
                    Handlers = _ReceiveEventNotification.GetInvocationList();
                    foreach (Delegate item in Handlers)
                    {
                        ReceiveEventNotification -= (TransmitData)item;
                    }
                }

                #endregion
                #region // Remove HDLCDisconnected Event Handlers

                Handlers = null;
                if (_HDLCDisconnected != null)
                {
                    Handlers = _HDLCDisconnected.GetInvocationList();
                    foreach (Delegate item in Handlers)
                    {
                        HDLCDisconnected -= (Action)item;
                    }
                }

                #endregion

                ResponseCompCondition = null;
                FrameReceiveCompCondition = null;
            }
            catch
            {
                // Do Nothing
            }
        }

        public void Dispose()
        {
            disposeOff();
        }

        #endregion

    }

}
