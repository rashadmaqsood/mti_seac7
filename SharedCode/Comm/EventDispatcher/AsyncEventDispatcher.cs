using System;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Threading.Tasks;
using SharedCode.Comm.EventDispatcher.Contracts;
using SharedCode.Common;

namespace SharedCode.Comm.EventDispatcher
{
    public class AsyncEventDispatcher : IEventDispatcher
    {
        private bool _disposed;
        private Dictionary<Type, Delegate> _applicationEventHandlers;
        private LinkedList<IEvent> _cachedEvents;
        private Thread ASyncThread = null;
        private bool isThreadRunning = false;

        public static readonly TimeSpan MaxThreadIdleSuspendTime = TimeSpan.FromSeconds(300);

        public AsyncEventDispatcher()
        {
            _applicationEventHandlers = new Dictionary<Type, Delegate>();
            _cachedEvents = new LinkedList<IEvent>();
        }


        public void AddListener<TEvent>(EventHandlerDelegate<TEvent> handler)
            where TEvent : IEvent
        {
            Delegate @delegate;
            if (_applicationEventHandlers.TryGetValue(typeof(TEvent), out @delegate))
            {
                _applicationEventHandlers[typeof(TEvent)] = Delegate.Combine(@delegate, handler);
            }
            else
            {
                _applicationEventHandlers[typeof(TEvent)] = handler;
            }
        }

        public void RemoveListener<TEvent>(EventHandlerDelegate<TEvent> handler)
            where TEvent : IEvent
        {
            Delegate @delegate;
            if (_applicationEventHandlers.TryGetValue(typeof(TEvent), out @delegate))
            {
                Delegate currentDel = Delegate.Remove(@delegate, handler);

                if (currentDel == null)
                {
                    _applicationEventHandlers.Remove(typeof(TEvent));
                }
                else
                {
                    _applicationEventHandlers[typeof(TEvent)] = currentDel;
                }
            }
        }

        public void Dispatch<TEvent>(TEvent @event) where TEvent : IEvent
        {
            if (@event == null) throw new ArgumentNullException("event");
            if (_disposed) throw new ObjectDisposedException("Cannot dispatch and event when disposed! ");

            Delegate @delegate;
            if (_applicationEventHandlers.TryGetValue(typeof(TEvent), out @delegate))
            {
                EventHandlerDelegate<TEvent> callback = @delegate as EventHandlerDelegate<TEvent>;
                if (callback != null)
                {
                    callback(@event);
                }
            }
        }

        public void Dispatch<TEvent>(TEvent @event, byte AsyncEveDispatch) where TEvent : IEvent
        {
            if (@event == null) throw new ArgumentNullException("event");
            if (_disposed) throw new ObjectDisposedException("Cannot dispatch and event when disposed! ");

            // ASync Invoke
            if (Convert.ToBoolean(AsyncEveDispatch))
            {
                // Add Event To Be Invoke Later
                lock (_cachedEvents)
                {
                    _cachedEvents.AddLast(@event);
                }

                bool isRunner = false;
                GetThreadState(out isRunner);
                if (!isRunner)
                {
                    lock (this)
                    {
                        GetThreadState(out isRunner);
                        if (isRunner)
                            return;
                        StartAsyncHelperThread();
                    }
                }
            }
            else
            {
                Delegate @delegate;
                if (_applicationEventHandlers.TryGetValue(typeof(TEvent), out @delegate))
                {
                    EventHandlerDelegate<TEvent> callback = @delegate as EventHandlerDelegate<TEvent>;
                    if (callback != null)
                    {
                        callback(@event);
                    }
                }
            }
        }

        private void AsyncDispatchHelper()
        {
            List<IEvent> localLst = new List<IEvent>();
            IEvent @event = null;

            Func<bool> IsEventCached = new Func<bool>(() => !_disposed &&
                                                             _cachedEvents != null &&
                                                             _cachedEvents.Count > 0);

            Action<IEvent, ParallelLoopState> ParalLoopAction = new Action<IEvent, ParallelLoopState>((@evntLocal, loopState) =>
                        {
                            try
                            {
                                Type T = @evntLocal.GetType();

                                Delegate @delegate;
                                if (_applicationEventHandlers.TryGetValue(T, out @delegate))
                                {
                                    EventHandlerDelegate<IEvent> callback = @delegate as EventHandlerDelegate<IEvent>;
                                    if (callback != null)
                                    {
                                        callback(@evntLocal);
                                    }
                                }
                            }
                            catch
                            {
                                loopState.Break();
                            }
                        });

            try
            {
                // Continue ASync Dispatch Event
                while (!_disposed)
                {
                    localLst.Clear();

                    while (!_disposed &&
                            _cachedEvents != null &&
                            _cachedEvents.Count > 0 &&
                            localLst.Count <= 10)
                    {
                        lock (_cachedEvents)
                        {
                            var lnkNode = _cachedEvents.First;
                            _cachedEvents.RemoveFirst();
                            if (lnkNode != null &&
                                lnkNode.Value != null)
                            {
                                @event = lnkNode.Value;
                            }
                        }

                        if (@event == null)
                            continue;
                        else
                            localLst.Add(@event);
                    }

                    // Invoke Event In Parallel
                    Parallel.ForEach<IEvent>(localLst, ParalLoopAction);
                    localLst.Clear();

                    if (IsEventCached.Invoke())
                        continue;
                    Commons.DelayUntil(IsEventCached, MaxThreadIdleSuspendTime.Ticks);
                    // break On MaxThreadIdleTime Elapsed
                    if (!IsEventCached.Invoke())
                        break;
                }
            }
            catch (Exception ex)
            {
                // Log Error Message
                Debug.WriteLine("Error AsyncDispatchHelper: " + ex.Message);
            }
            finally
            {
                SetThreadState(false);
            }
        }

        private void RemoveAllListeners()
        {
            var handlerTypes = new Type[_applicationEventHandlers.Keys.Count];
            _applicationEventHandlers.Keys.CopyTo(handlerTypes, 0);

            foreach (Type handlerType in handlerTypes)
            {
                Delegate[] delegates = _applicationEventHandlers[handlerType].GetInvocationList();
                foreach (Delegate @delegate1 in delegates)
                {
                    var handlerToRemove = Delegate.Remove(_applicationEventHandlers[handlerType], @delegate1);
                    if (handlerToRemove == null)
                    {
                        _applicationEventHandlers.Remove(handlerType);
                    }
                    else
                    {
                        _applicationEventHandlers[handlerType] = handlerToRemove;
                    }
                }
            }
        }

        #region Support_Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isRunner"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void StartAsyncHelperThread()
        {
            try
            {
                SetThreadState(true);
                if (ASyncThread != null)
                    ASyncThread.Abort();
            }
            catch (Exception ex)
            {
                // Debug.WriteLine("Error:" + ex.Message);
            }

            try
            {
                ASyncThread = new System.Threading.Thread(AsyncDispatchHelper) { Priority = ThreadPriority.AboveNormal };
                ASyncThread.Start();
                SetThreadState(true);
            }
            catch
            {
                SetThreadState(false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isRunner"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void SetThreadState(bool isRunner)
        {
            isThreadRunning = isRunner;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isRunner"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void GetThreadState(out bool isRunner)
        {
            isRunner = isThreadRunning;
        }

        #endregion

        ~AsyncEventDispatcher()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                // free other managed objects 
                // that implement IDisposable only
                RemoveAllListeners();
            }

            // release any unmanaged objects
            // set the object references to null
            _applicationEventHandlers = null;

            try
            {
                if (ASyncThread != null)
                    ASyncThread.Abort();
            }
            catch
            {
            }
            _disposed = true;
        }
    }
}