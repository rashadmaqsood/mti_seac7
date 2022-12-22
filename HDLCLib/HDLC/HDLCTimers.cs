using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace _HDLC
{
    public partial class HDLC
    {
        #region Member Methods

        public void WriteUnAck(byte[] Array, int index, long length)
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
                setError(false);
                setUIError(false);
                List<byte> LTBuf = new List<byte>();
                for (int Index = index; Array != null && Index < Array.Length && Index < length; Index++)  ///Copy Data To Local Buffer
                    LTBuf.Add(Array[Index]);
                int dataSize = LTBuf.Count;
                int TotalFrames = (int)Math.Ceiling(dataSize / MaxUIFrameSize * 1.0d);
                if (dataSize == 0)  ///Transmit empty buf
                {
                    HDLCFrame Frame = new HDLCFrame(FrameType.UI);
                    Frame.APDU = null;
                    Frame.DestinationAddressLength = DestinationAddressLength;
                    Frame.UpperHDLCDestAddress = (ushort)((DestinationAddress >> 16) & 0xFFFF);
                    Frame.LowerHDLCDestAddress = (ushort)(DestinationAddress & 0xFFFF);
                    Frame.ClientHDLCSourceAddress = (byte)((SourceAddress) & 0xFFFF);
                    byte[] _frameBytes = Frame.ToByteArray();
                    _TransmitFrame.Invoke(_frameBytes);
                    _timers.InActivityTimerEnable = true;
                }
                else
                {
                    int frameIndex = 0;
                    int FrameNo = 0;
                    while (FrameNo < TotalFrames && frameIndex < dataSize)
                    {
                        frameIndex = FrameNo * MaxInfoBufTransmit;
                        int fsize = (dataSize - frameIndex >= MaxUIFrameSize) ? MaxUIFrameSize : dataSize - frameIndex;
                        byte[] _apdu = new byte[fsize];
                        int indexT = 0;
                        for (int indeX = frameIndex; indeX < (frameIndex + fsize); indeX++, indexT++) ///Copy Data From Transmit Buffer
                            _apdu[indexT] = LTBuf[indeX];
                        HDLCFrame Frame = new HDLCFrame(FrameType.UI);
                        if ((dataSize - frameIndex) > MaxInfoBufTransmit)
                        {
                            Frame.IsSegmented = true;
                            Frame.P_F = false;
                        }
                        else
                        {
                            Frame.IsSegmented = false;
                            Frame.P_F = true;
                        }
                        if (FrameNo > 0)
                        {
                            ///Do Not Attach APDU Wrapper
                            Frame.IsInfoFirstFrame = false;
                        }
                        Frame.APDU = _apdu;
                        Frame.DestinationAddressLength = DestinationAddressLength;
                        Frame.UpperHDLCDestAddress = (ushort)((DestinationAddress >> 16) & 0xFFFF);
                        Frame.LowerHDLCDestAddress = (ushort)(DestinationAddress & 0xFFFF);
                        Frame.ClientHDLCSourceAddress = (byte)((SourceAddress) & 0xFFFF);
                        byte[] _frameBytes = Frame.ToByteArray();
                        _TransmitFrame.Invoke(_frameBytes);
                        _timers.InActivityTimerEnable = true;
                        TransmitedFrame = Frame;
                        FrameNo++;
                        System.Threading.Thread.Sleep(20);       ///Second Station Takes Time
                    }
                }
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

                // Enable InActivity Proceduer
                _timers.IsInactivityEnable = true;
                _timers.InActivityTimerEnable = true;

            }
        }

        private void ProcessEventNotification(HDLCFrame UIFrame)
        {
            try
            {
                if (UIFrame._FrameType == FrameType.UI)     // Process UI Frames
                {
                    if (UIFrame.IsInfoFirstFrame)
                    {
                        setUIError(false);
                        this.UIReceiveBuffer.Clear();
                        UIReceiveBuffer.AddRange(UIFrame.APDU);
                    }
                    else
                        UIReceiveBuffer.AddRange(UIFrame.APDU);
                    if (UIFrame.P_F && !UIFrame.IsSegmented) // Final UI Frame Notify Event
                    {
                        if (!isErrorUI) // No Error Occcurred Receiving UI Frames Now Notify
                        {
                            System.Threading.ThreadPool.QueueUserWorkItem(this.NotifyEvent, UIReceiveBuffer.ToArray());
                            UIReceiveBuffer.Clear();
                        }
                    }
                }
                else
                {
                    throw new HDLCErrorException("Unable To Process Event Notification,Not UI Frame Type");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void NotifyEvent(object buffer)
        {
            if (buffer.GetType() == typeof(Array))
                _ReceiveEventNotification.Invoke((byte[])buffer);
        }

        private void _timers_InActivityTimeOut(DateTime obj)
        {
            bool isConnected = _connected;

            // Console.Out.WriteLine(obj.ToString("T"));
            lock (this)
            {
                _connected = false;
            }

            if (isConnected)
                _HDLCDisconnected.Invoke();

            ResetHDLC();
        }

        private void _timers_SendRRInActivityTimeOut(DateTime obj)
        {
            // Console.Out.WriteLine(obj.ToString("T"));
            SendRRForTimeOut();
            // Console.Out.WriteLine("SEND RR Now Time" + DateTime.Now.ToString("T"));
        }

        public void SendRRForTimeOut()
        {
            try
            {
                if (Connected && isRightToSend &&
                    !IsWriteReadInProcess)
                {
                    TransmitedFrame = SendRR();
                }
            }
            catch
            {
            }
        }

        public HDLCFrame SendRR()
        {
            bool isIOBusy = IsIOBusy;
            HDLCFrame frameRR = new HDLCFrame();

            try
            {
                if (Connected && isRightToSend)
                {
                    setIOBusy(true);
                    frameRR = new HDLCFrame(FrameType.RR);
                    frameRR.P_F = true;

                    frameRR.DestinationAddressLength = DestinationAddressLength;
                    frameRR.ReceiveWindowNo = (byte)(ReceivingWindowCount % 8);
                    // frameRR.TransmitWindowNo = (byte)(TransmitWindowCount % 8);

                    // Set HDLC Frame Address
                    if (_RawDestinationAddress != null && _RawDestinationAddress.Length > 0 &&
                        _RawSourceAddress != null && _RawSourceAddress.Length > 0)
                    {
                        frameRR.DestinationHDLCAddress = _RawDestinationAddress;
                        frameRR.SourceHDLCAddress = _RawSourceAddress;
                    }
                    else
                    {
                        frameRR.UpperHDLCDestAddress = (ushort)((DestinationAddress >> 16) & 0xFFFF);
                        frameRR.LowerHDLCDestAddress = (ushort)(DestinationAddress & 0xFFFF);
                        frameRR.ClientHDLCSourceAddress = (byte)((SourceAddress) & 0xFFFF);
                    }

                    _TransmitFrame(frameRR.ToByteArray());

                    setHDLCStatus(HDLCState.Ready);
                    setExpectedByteCount();
                }
                else
                    throw new HDLCErrorException("Unable To Proceed Write,HDLC Disconnected");

                return frameRR;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                setIOBusy(isIOBusy);
            }
        }

        #endregion

        public partial class HDLCTimers : IDisposable
        {
            public static readonly TimeSpan MinInternalTimerTick = TimeSpan.FromMilliseconds(1000d);
            public static readonly TimeSpan MaxInternalTimerTick = TimeSpan.FromSeconds(10d);
            public static int Max_Internal_Ticks = 10;

            #region DataMember

            public event Action<DateTime> InActivityTimeOut = delegate { };
            public event Action<DateTime> SendRRInActivityTimeOut = delegate { };
            private TimeSpan _inactivityTimeOut;
            private TimeSpan _inactivitySubTimeOut;
            private TimeSpan _reqResTimeOut;

            private bool avoidInactivityTimeOut;
            private bool isResponseTimeOut;
            private bool isInactivityTimeOut;

            private Timer InactivityTimerInternal;

            private Stopwatch InactivityTimer;
            private Stopwatch InactivityTimerSub;

            #endregion

            public HDLCTimers()
            {
                InactivityTimerInternal = new Timer();
                InactivityTimerInternal.Elapsed += new ElapsedEventHandler(InactivityTimerInternal_Elapsed);

                InactivityTimeOut = new TimeSpan(0, 0, 0, 20, 0);  // Default One-Second
                ReqResTimeOut = new TimeSpan(0, 0, 0, 3, 0);      // Default 150 Milli-Seconds

                InactivityTimer = new Stopwatch();
                InactivityTimerSub = new Stopwatch();

                IsInactivityEnable = true;
            }

            #region Properties

            public TimeSpan InactivityTimeOut
            {
                get { return _inactivityTimeOut; }
                set
                {
                    if (value.TotalMilliseconds >= 1000f)
                        _inactivityTimeOut = value;
                    else
                        throw new HDLCErrorException("Inactivity TimeOut Can't be less than one second");
                }
            }

            public TimeSpan ReqResTimeOut
            {
                get { return _reqResTimeOut; }
                set
                {
                    _reqResTimeOut = value;
                }
            }

            public bool AvoidInactivityTimeOut
            {
                get { return avoidInactivityTimeOut; }
                set { avoidInactivityTimeOut = value; }
            }

            public bool IsInactivityTimeOut
            {
                get { return isInactivityTimeOut; }
            }

            public bool InActivityTimerEnable
            {
                get
                {
                    try
                    {
                        return InactivityTimerInternal.Enabled;
                    }
                    catch { }
                    return false;
                }
                set
                {
                    // master Inactivity Enable Flag
                    if (value && IsInactivityEnable)
                        ResetInActivityTimer();
                    else
                    {
                        InactivityTimerInternal.Stop();

                        // Stop Running StopWatch
                        InactivityTimer.Stop();
                        InactivityTimerSub.Stop();
                    }
                }
            }

            public bool IsInactivityEnable
            {
                get;
                internal set;
            }

            public bool IsResponseTimeOut
            {
                get
                {
                    return isResponseTimeOut;
                }
                internal set { isResponseTimeOut = value; }
            }

            #endregion

            #region Timer Handlers

            /// <summary>
            /// Event Fires On Inactivity Timers Elapsed,Notify Event Occurrence & Set InactivityTimerOut True
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            [MethodImpl(MethodImplOptions.Synchronized)]
            private void InactivityTimerInternal_Elapsed(object sender, ElapsedEventArgs e)
            {
                bool isTimerEnable = false;

                try
                {
                    // Console.Out.WriteLine(string.Format("{0} InactivityTimerInternal_Elapsed", DateTime.Now.ToString()));

                    isTimerEnable = InactivityTimerInternal.Enabled;
                    InactivityTimerInternal.Enabled = false;

                    if (IsInactivityEnable)
                    {
                        // Inactivity Timer Elapsed
                        if (InactivityTimer.IsRunning &&
                            InactivityTimer.ElapsedMilliseconds >= InactivityTimeOut.TotalMilliseconds)
                        {
                            InactivityTimer_Elapsed(sender, e);
                        }
                        else if (InactivityTimerSub.IsRunning &&
                                 InactivityTimerSub.ElapsedMilliseconds >= _inactivitySubTimeOut.TotalMilliseconds)
                        {
                            InactivityTimerSub_Elapsed(sender, e);
                        }
                    }
                }
                catch
                {
                    // Donot Raise Error
                }
                finally
                {
                    // Re-Enable Internal Basic Tick Timer
                    if (InactivityTimerInternal != null)
                        InactivityTimerInternal.Enabled = isTimerEnable
                                                          && (InactivityTimer.IsRunning ||
                                                             InactivityTimerSub.IsRunning);
                }
            }

            /// <summary>
            /// Event Fires On Inactivity Timers Elapsed,Notify Event Occurrence & Set InactivityTimerOut True
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void InactivityTimer_Elapsed(object sender, ElapsedEventArgs e)
            {
                lock (this)
                {
                    InactivityTimerInternal.Stop();

                    // Stop Running Stop Watch
                    InactivityTimer.Stop();
                    InactivityTimerSub.Stop();
                }

                isInactivityTimeOut = true;
                // Console.Out.WriteLine("Inactivity Timer Elapsed {0}", DateTime.Now.ToString("T"));
                InActivityTimeOut.Invoke(e.SignalTime);
            }

            /// <summary>
            /// Event Fires On RequestResponseSub Timer Elapsed,Send RR Request & Reset InactivityTimers
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void InactivityTimerSub_Elapsed(object sender, ElapsedEventArgs e)
            {
                // Disable Send RR For Inactivity
                InactivityTimerSub.Stop();
                SendRRInActivityTimeOut.Invoke(e.SignalTime);
            }

            #endregion

            public void ResetInActivityTimer()
            {
                isInactivityTimeOut = false;

                TimeSpan Internal_Tick = TimeSpan.Zero;
                Internal_Tick = TimeSpan.FromMilliseconds(Convert.ToDouble(InactivityTimeOut.TotalMilliseconds / Max_Internal_Ticks));

                InactivityTimerInternal.Stop();

                if (Internal_Tick < MinInternalTimerTick)
                    InactivityTimerInternal.Interval = MinInternalTimerTick.TotalMilliseconds;
                else if (Internal_Tick > MaxInternalTimerTick)
                    InactivityTimerInternal.Interval = MaxInternalTimerTick.TotalMilliseconds;
                else if (Internal_Tick > MinInternalTimerTick && Internal_Tick <= MaxInternalTimerTick)
                    InactivityTimerInternal.Interval = Internal_Tick.TotalMilliseconds;
                else
                    InactivityTimerInternal.Interval = MinInternalTimerTick.TotalMilliseconds;

                // InactivityTimerInternal.Interval = InternalTimerTick.TotalMilliseconds;
                InactivityTimerInternal.AutoReset = true;

                InactivityTimer.Reset();
                InactivityTimerSub.Reset();

                if (avoidInactivityTimeOut) // Reset SubTimer
                {
                    if (InactivityTimeOut.TotalMilliseconds >= 1000d &&
                        InactivityTimeOut.TotalMilliseconds < 2000)
                    {
                        _inactivitySubTimeOut = InactivityTimeOut - TimeSpan.FromMilliseconds(250d);
                    }
                    else if (InactivityTimeOut.TotalMilliseconds >= 20000d)
                    {
                        _inactivitySubTimeOut = InactivityTimeOut - TimeSpan.FromMilliseconds(10000d);
                    }
                    else if (InactivityTimeOut.TotalMilliseconds >= 10000d)
                    {
                        _inactivitySubTimeOut = InactivityTimeOut - TimeSpan.FromMilliseconds(5000d);
                    }
                    else if (InactivityTimeOut.TotalMilliseconds > 2000d)
                    {
                        _inactivitySubTimeOut = InactivityTimeOut - TimeSpan.FromMilliseconds(1500d);
                    }
                    else
                    {
                        _inactivitySubTimeOut = InactivityTimeOut - TimeSpan.FromMilliseconds(250d);
                    }
                }

                InactivityTimerInternal.Enabled = true;

                InactivityTimer.Start();
                if (avoidInactivityTimeOut)
                    InactivityTimerSub.Start();
            }

            #region PreciseDelay

            /// <summary>
            /// Make Thread Delay Duration Of Provided Delay
            /// It has significant Performance Penalty
            /// </summary>
            /// <param name="durationTicks">Milli-Second Thread Duration</param>
            public static bool PreciseDelayUntil(Func<bool> Criteria, double durationTicks)
            {
                // Static method to initialize 
                // and start stopwatch
                var sw = Stopwatch.StartNew();

                try
                {

                    while (sw.ElapsedMilliseconds < durationTicks
                           && !Criteria.Invoke())
                    {
                        // Wait For Thread 
                        // System.Threading.Thread.SpinWait(05);
                        System.Threading.Thread.Sleep(05);
                    }
                }
                // Donot Raise Error
                catch
                {
                }

                // Either Time Out Value Occurred
                return (sw.ElapsedMilliseconds >= durationTicks);
            }

            /// <summary>
            /// Make Thread Delay Duration Of Provided Delay
            /// It has significant Performance Penalty
            /// </summary>
            /// <param name="durationTicks">Milli-Second Thread Duration</param>
            public static bool PreciseDelayUntil(Func<bool> Criteria, TimeSpan duration)
            {
                return PreciseDelayUntil(Criteria, duration.TotalMilliseconds);
            }

            /// <summary>
            /// Make Thread Delay Duration Of Provided Delay
            /// It has significant Performance Penalty
            /// </summary>
            /// <param name="durationTicks">Milli-Second Thread Duration</param>
            public static void PreciseDelay(long durationTicks)
            {
                HDLCTimers.PreciseDelay(Convert.ToDouble(durationTicks));
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
                    // System.Threading.Thread.SpinWait(05);
                    System.Threading.Thread.Sleep(05);
                }
            }

            /// <summary>
            /// Make Thread Delay Duration Of Provided Delay
            /// It has significant Performance Penalty
            /// </summary>
            /// <param name="duration">Thread Duration Delay</param>
            public static void PreciseDelay(TimeSpan duration)
            {
                HDLCTimers.PreciseDelay(duration.TotalMilliseconds);
            }

            #endregion

            public void Dispose()
            {
                try
                {
                    if (InactivityTimerInternal != null)
                        InactivityTimerInternal.Dispose();
                    InactivityTimerInternal = null;

                    #region // Remove InActivityTimeOut Event Handlers

                    Delegate[] Handlers = null;
                    if (InActivityTimeOut != null)
                    {
                        Handlers = InActivityTimeOut.GetInvocationList();
                        foreach (Delegate item in Handlers)
                        {
                            InActivityTimeOut -= (Action<DateTime>)item;
                        }
                    }

                    #endregion
                    #region // Remove SendRRInActivityTimeOut Event Handlers

                    Handlers = null;
                    if (SendRRInActivityTimeOut != null)
                    {
                        Handlers = SendRRInActivityTimeOut.GetInvocationList();
                        foreach (Delegate item in Handlers)
                        {
                            SendRRInActivityTimeOut -= (Action<DateTime>)item;
                        }
                    }

                    #endregion
                }
                catch { }
            }
        }
    }

    public enum HDLCState : byte
    {
        Ready = 1,
        Receive = 3,
        TimeOut = 4
    }

    public class HDLCErrorException : Exception
    {
        #region Constructors

        public HDLCErrorException() : base("Invalid HDLC Frame") { }
        public HDLCErrorException(String Message) : base(Message) { }
        public HDLCErrorException(String Message, Exception InnerException) : base(Message, InnerException) { }

        #endregion
    }

}
