using SharedCode.Comm.HelperClasses;
using System;
using System.Threading;

namespace SharedCode.TCP_Communication
{
    #region Internal_Class

    public class TCPStreamResult : IAsyncResult, IDisposable
    {
        #region Data_Members

        private byte[] readBufferMethod;
        private int readBufOffSet;
        private int readBufSize;

        private AsyncCallback readMethodCallBck;
        private object state = null;
        private bool isCompleteSync;
        private bool isCompleted;
        private Exception innerException;
        private int lateInvokeTry;
        private readonly static int MaxInvokeRetry = 10;
        private BaseWaitToken WaitToken = null;

#if Enable_DEBUG_ECHO

        private int instanceId = 0;
        protected static int StaticInstanceId = 0;

#endif

        #endregion

        #region Property

        public int OffSet
        {
            get { return readBufOffSet; }
            set { readBufOffSet = value; }
        }

        public byte[] Buffer
        {
            get { return readBufferMethod; }
            set { readBufferMethod = value; }
        }

        public int Size
        {
            get { return readBufSize; }
            set { readBufSize = value; }
        }

        public AsyncCallback ASynCallBack
        {
            get { return readMethodCallBck; }
            set { readMethodCallBck = value; }
        }

        public Object AsyncState
        {
            get
            {
                try
                {
                    return state;
                }
                catch (Exception ex)
                {
                    throw new NullReferenceException("Unable to take ASyncState_TCPStreamResult", ex);
                }
            }
            internal set
            {
                state = value;
            }
        }

        public ManualResetEvent ASyncWaitHandler
        {
            get
            {
                try
                {
                    if (IsAsyncWaitHandleValid && WaitHanlderToken.WaitHandler.Value is ManualResetEvent)
                        return (ManualResetEvent)WaitHanlderToken.WaitHandler.Value;
                    else
                        throw new InvalidCastException("Unable to take ASyncWaitHandler");
                }
                catch (Exception ex)
                {
                    throw new InvalidCastException("Unable to take ASyncWaitHandler_TCPStreamResult", ex);
                }
            }
        }

        public Exception InnerException
        {
            get { return innerException; }
            set { innerException = value; }
        }

        public WaitHandle AsyncWaitHandle
        {
            get
            {
                try
                {
                    if (IsAsyncWaitHandleValid)
                        return WaitHanlderToken.WaitHandler.Value;
                    else
                        throw new NullReferenceException("Unable to take AsyncWaitHandle");
                }
                catch (Exception ex)
                {
                    throw new InvalidCastException("Unable to take ASyncWaitHandler_TCPStreamResult", ex);
                }
            }
        }

        public bool IsAsyncWaitHandleValid
        {
            get
            {
                try
                {
                    if (WaitHanlderToken != null && WaitHanlderToken.WaitHandler != null)
                        if (!WaitHanlderToken.WaitHandler.IsValueCreated ||
                            (WaitHanlderToken.WaitHandler.Value != null && !WaitHanlderToken.WaitHandler.Value.SafeWaitHandle.IsInvalid &&
                            !WaitHanlderToken.WaitHandler.Value.SafeWaitHandle.IsClosed))
                            return true;
                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool IsWaitHandlerCreated
        {
            get
            {
                if (IsAsyncWaitHandleValid && WaitHanlderToken.WaitHandler.IsValueCreated)
                    return true;
                else
                    return false;
            }
        }

        public int LateInvokeTry
        {
            get { return lateInvokeTry; }
            set { lateInvokeTry = value; }
        }

        public bool IsLateInvokeExpired
        {
            get
            {
                if (LateInvokeTry > MaxInvokeRetry)
                    return true;
                else
                    return false;
            }
        }

        public bool IsMaxInvokeRetry
        {
            get
            {
                if (lateInvokeTry >= MaxInvokeRetry)
                    return true;
                else
                    return false;
            }
        }

        public BaseWaitToken WaitHanlderToken
        {
            get { return WaitToken; }
            internal set
            {
                if (value == null || value.IsCancel)
                    throw new ArgumentException("BaseWaitHandlerToken null Ref");
                WaitToken = value;
            }
        }

        #endregion

        #region Constructor

        public TCPStreamResult()
        {
            lateInvokeTry = 0;
        }

        public TCPStreamResult(byte[] readBufferMethod, int readBufOffSet, int readBufSize,
            AsyncCallback readMethodCallBck, Object State, BaseWaitToken WaitHanlderToken = null)
        {
            lateInvokeTry = 0;
            //Init Variables
            this.WaitToken = WaitHanlderToken;
            this.readMethodCallBck = readMethodCallBck;
            this.state = State;
            this.readBufferMethod = readBufferMethod;
            this.readBufOffSet = readBufOffSet;
            this.readBufSize = readBufSize;
        }

        public TCPStreamResult(AsyncCallback readMethodCallBck, Object State,
            BaseWaitToken WaitHanlderToken = null) :
            this(null, 0, 0, readMethodCallBck, State, WaitHanlderToken)
        { }

        #endregion

        #region Wait_TimeOut

        //        [MethodImpl(MethodImplOptions.Synchronized)]
        //        public void AsynWait(Object State, int TimeOut, WaitOrTimerCallback WaitCallBackExtArg = null)
        //        {
        //            try
        //            {
        //                if (asyncWaitHandler == null) throw new ArgumentNullException("waitHandle");
        //                if (WtHandle != null)
        //                    CancelAsyncWait();
        //                if (WtHandle == null)
        //                {
        //#if Enable_DEBUG_ECHO
        //                            Commons.WriteLine(String.Format("AsyncWait_TCPStreamResult {0}", this));
        //#endif
        //                    if (asyncWaitHandler == null || asyncWaitHandler.SafeWaitHandle.IsClosed)
        //                    {
        //                        if (asyncWaitHandler != null)
        //                            asyncWaitHandler.Close();
        //                        if (asyncWaitHandle != null)
        //                            asyncWaitHandle.Close();
        //                        //Set Manual Reset Wait Handler Again To Reset State
        //                        asyncWaitHandler = new ManualResetEvent(false);
        //                        asyncWaitHandle = new MyWaitHandle(asyncWaitHandler);
        //                    }
        //                    WtHandle = ThreadPool.RegisterWaitForSingleObject(asyncWaitHandler, WaitOrTimeOutHandler, State, TimeOut, true);
        //                    WaitCallBackExt = WaitCallBackExtArg;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Exception _Ex = new Exception("Error Occured while Setting Async Wait_TCPSteramResult", ex);
        //#if Enable_DEBUG_ECHO
        //                Commons.WriteLine(String.Format("{0} {1}", _Ex.Message, this.ToString()));
        //#endif
        //                throw _Ex;
        //            }
        //        }

        //        [MethodImpl(MethodImplOptions.Synchronized)]
        //        public void CancelAsyncWait()
        //        {
        //            try
        //            {
        //                if (WtHandle != null)
        //                {
        //#if Enable_DEBUG_ECHO
        //                   Commons.WriteLine(String.Format("AsyncWait_CancelAsyncWait {0} ", this));
        //#endif
        //                    WtHandle.Unregister(asyncWaitHandler);
        //                    if (asyncWaitHandler != null)
        //                        asyncWaitHandler.Close();
        //                }
        //                WtHandle = null;

        //            }
        //            catch (Exception ex)
        //            {
        //                Exception _Ex = new Exception(String.Format("Error Occured while Un_registering Async Wait_TCPStream {0}", ex.StackTrace), ex);
        //#if Enable_DEBUG_ECHO
        //                Commons.WriteLine(String.Format("{0} {1}", _Ex.Message, this));
        //#endif
        //                throw _Ex;
        //            }
        //        }

        //        /// <summary>
        //        /// Thread Based function will be executed if WaitHanlde is signaled
        //        /// or TimeOut Occured
        //        /// </summary>
        //        /// <param name="State"></param>
        //        /// <param name="IsTimeOut"></param>
        //        protected void WaitOrTimeOutHandler(Object State, bool IsTimeOut)
        //        {
        //            try
        //            {
        //                CancelAsyncWait();
        //                ///is Last Read Operation Timeout
        //                if (IsTimeOut)
        //                {
        //                    ///Invoke Error Response Here
        //                    if (WaitCallBackExt != null)
        //                        WaitCallBackExt.Invoke(State, IsTimeOut);
        //                    else if (ASynCallBack != null)
        //                    {
        //                        InnerException = new IOException("Last data read operation timeout");
        //#if Enable_DEBUG_ECHO
        //                        Commons.WriteError("Last Read Opertaion Time Out _TCPStreamResult_Chk1" + this.ToString());
        //#endif
        //                        IsCompleted = true;
        //                        InnerException = InnerException;
        //                        ASynCallBack.Invoke(this);
        //                    }
        //                }
        //            }
        //            catch (Exception)
        //            {
        //            }
        //            finally
        //            {
        //                ///Unregister Wait Handle Here
        //                try
        //                {
        //#if Enable_DEBUG_ECHO
        //                    Commons.WriteError(String.Format("Opertaion Time Out_TCPStreamResult_Chk2_{0}_{1}",
        //                        DateTime.Now.ToLongTimeString(), instanceId));
        //#endif

        //                }
        //                catch (Exception)
        //                {
        //                }
        //            }
        //        }

        #endregion

        #region Async Wait Functions

        ///// <summary>
        ///// Provide ASync Wait For Specified Duration Of Time (DelayDuration)
        ///// </summary>
        ///// <param name="DelayDuration"></param>
        ///// <returns></returns>
        //public async Task<bool> WaitAsync(TimeSpan DelayDuration)
        //{
        //    TaskCompletionSource<bool> tcs = null;
        //    RegisteredWaitHandle REgWt_Handler = null;
        //    ManualResetEvent Wt_Handle = null;
        //    try
        //    {
        //        tcs = new TaskCompletionSource<bool>();
        //        Wt_Handle = new ManualResetEvent(false);

        //        AsynWait(out REgWt_Handler, Wt_Handle, (Wt_handle, isTimeOut) =>
        //        {
        //            try
        //            {
        //                tcs.SetResult(isTimeOut);
        //            }
        //            catch (OperationCanceledException) { tcs.TrySetCanceled(); }
        //            catch (Exception exc) { tcs.TrySetException(exc); }
        //            finally
        //            {

        //            }

        //        }, Wt_Handle, DelayDuration);

        //        return await tcs.Task;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(String.Format("Error occured Awaiting Async\r\nStack Trace: {0}", ex.StackTrace), ex);
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            try
        //            {
        //                CancelAsyncWait(REgWt_Handler, (WaitHandle)Wt_Handle);
        //            }
        //            catch (Exception)
        //            {
        //            }
        //            if (tcs != null)
        //                tcs.Task.Dispose();
        //            if (Wt_Handle != null)
        //                Wt_Handle.Dispose();
        //            REgWt_Handler = null;
        //        }
        //        catch (Exception)
        //        { }
        //    }
        //}

        //protected void AsynWait(out RegisteredWaitHandle NativeHandler, WaitHandle WaitHandle,
        //    WaitOrTimerCallback TimeOutCallBack, Object State, TimeSpan TimeOut)
        //{
        //    try
        //    {
        //        if (WaitHandle == null) throw new ArgumentNullException("waitHandle");
        //        NativeHandler = ThreadPool.RegisterWaitForSingleObject(WaitHandle, TimeOutCallBack, State,
        //            Convert.ToInt32(TimeOut.TotalMilliseconds), true);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error Occured while Setting Async_Wait_ApplicationController", ex);
        //    }
        //}

        //protected void CancelAsyncWait(RegisteredWaitHandle NativeHandler, WaitHandle WaitHandle)
        //{
        //    try
        //    {
        //        if (NativeHandler == null) throw new ArgumentNullException("waitHandle");
        //        NativeHandler.Unregister(WaitHandle);
        //        WaitHandle.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error Occured while Un_registering Async_Wait_ApplicationController", ex);
        //    }
        //}

        #endregion

        #region IAsyncResult Members

        public bool CompletedSynchronously
        {
            get { return isCompleteSync; }
            set { isCompleteSync = value; }
        }

        public bool IsCompleted
        {
            get
            {
                return Interlocked.Equals(isCompleted, true);
            }
            set
            {
                try
                {
                    isCompleted = value;
                }
                catch
                { }
            }
        }

        #endregion

        ~TCPStreamResult()
        {
            try
            {
                Dispose();
                readBufferMethod = null;
                readMethodCallBck = null;
                if (WaitHanlderToken != null)
                {
                    WaitHanlderToken.Dispose();
                    WaitHanlderToken = null;
                }
                innerException = null;
            }
            catch
            { }
        }

        public void Dispose()
        {
            try
            {
                if (WaitHanlderToken != null)
                {
                    WaitHanlderToken.Dispose();
                    WaitHanlderToken = null;
                }
                readMethodCallBck = null;
                readBufferMethod = null;
            }
            catch { }
        }
    }

    #endregion

    public class MyWaitHandle : WaitHandle
    {
        internal ManualResetEvent InternalEvent { get; private set; }
        internal MyWaitHandle(ManualResetEvent val)
        {
            InternalEvent = val;
        }

        public override bool WaitOne()
        {
            return InternalEvent.WaitOne(Timeout.Infinite, false);
        }

        public override bool WaitOne(int millisecondsTimeout, bool exitContext)
        {
            return InternalEvent.WaitOne(millisecondsTimeout, exitContext);
        }

        public override bool WaitOne(TimeSpan timeout, bool exitContext)
        {
            return InternalEvent.WaitOne(timeout, false);
        }

        ~MyWaitHandle()
        {
            try
            {
                if (InternalEvent != null)
                {
                    InternalEvent.Close();
                }
                InternalEvent = null;
            }
            catch (Exception) { }
        }
    }
}
