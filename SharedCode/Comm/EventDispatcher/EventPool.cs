using SharedCode.Comm.EventDispatcher.Contracts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace SharedCode.Comm.EventDispatcher
{
    public class EventPool : IEventPool
    {
        private bool _disposed;
        private ConcurrentDictionary<int, IPoolableEvent> _applicationEvents;
        private ConcurrentDictionary<Type, LinkedList<IEvent>> _Events;

        public readonly int MAX_PoolableCount = 500;
        private Random randomIndexer = null;

        public EventPool()
        {
            _applicationEvents = new ConcurrentDictionary<int, IPoolableEvent>(100, MAX_PoolableCount);
            _Events = new ConcurrentDictionary<Type, LinkedList<IEvent>>(100, MAX_PoolableCount);
            randomIndexer = new Random();
            _disposed = false;
        }

        public bool TryAdd<TEvent>(TEvent @event) where TEvent : class, IEvent
        {
            IPoolableEvent @poolEvent = null;

            if (@event == null) throw new ArgumentNullException("event");


            // Raise No Error
            try
            {
                if (_disposed)
                    return false;

                if (@event is IPoolableEvent)
                {
                    @poolEvent = (IPoolableEvent)@event;

                    if (_applicationEvents.ContainsKey(@poolEvent.HashCode)) return false;
                    _applicationEvents.TryAdd(@poolEvent.HashCode, @poolEvent);

                    return true;
                }
                else
                {
                    Type eventType = typeof(TEvent);
                    LinkedList<IEvent> PoolEventStore = null;
                    // Try Add 
                    if (!_Events.ContainsKey(eventType))
                    {
                        PoolEventStore = new LinkedList<IEvent>();
                        _Events.TryAdd(eventType, PoolEventStore);
                    }
                    else if (_Events.TryGetValue(eventType, out PoolEventStore)) ; // Do-Nothing

                    if (PoolEventStore != null &&
                        PoolEventStore.Count < MAX_PoolableCount)
                    {
                        // Sync Lock
                        lock (PoolEventStore)
                        {
                            PoolEventStore.AddLast(@event);
                        }

                        return true;
                    }
                }
            }
            catch
            {
            }

            return false;
        }

        public bool TryGet<TEvent>(int hashCode, out TEvent @event) where TEvent : class, IEvent
        {
            bool eventFound = false;
            @event = null;
            Type eventType = typeof(TEvent);

            try
            {
                if (_disposed)
                    return false;

                if (eventType is IPoolableEvent)
                {
                    IPoolableEvent @poolEvent = null;
                    if (_applicationEvents.TryGetValue(hashCode, out @poolEvent))
                    {
                        @event = @poolEvent as TEvent;
                    }

                    eventFound = (@event != null);
                    return eventFound;
                }
                else
                {
                    LinkedList<IEvent> PoolEventStore = null;
                    // Try Add
                    if (!_Events.ContainsKey(eventType))
                    {
                        eventFound = false;
                    }
                    else if (_Events.TryGetValue(eventType, out PoolEventStore)) ; // Do-Nothing

                    if (PoolEventStore != null &&
                        PoolEventStore.Count > 0)
                    {
                        // Sync Lock
                        lock (PoolEventStore)
                        {
                            int indexer = randomIndexer.Next(0, PoolEventStore.Count);
                            @event = PoolEventStore.ElementAt<IEvent>(indexer) as TEvent;
                        }

                        return true;
                    }
                }
            }
            catch
            {
                // Debug.WriteLine("Error Message");
            }

            return false;
        }

        public bool TryRemove<TEvent>(out TEvent @event) where TEvent : class, IEvent
        {
            bool eventFound = false;
            @event = null;
            Type eventType = typeof(TEvent);

            try
            {
                if (_disposed)
                    return false;

                if (eventType is IPoolableEvent)
                {
                    int indexer = randomIndexer.Next(0, _applicationEvents.Count);
                    var dicNode = _applicationEvents.ElementAt<KeyValuePair<int, IPoolableEvent>>(indexer);
                    IPoolableEvent @poolEvent = null;

                    if (dicNode.Value != null)
                        @event = dicNode.Value as TEvent;

                    _applicationEvents.TryRemove(dicNode.Key, out @poolEvent);

                    eventFound = (@poolEvent != null);
                    return eventFound;
                }
                else
                {
                    LinkedList<IEvent> PoolEventStore = null;
                    // Try Add
                    if (!_Events.ContainsKey(eventType))
                    {
                        eventFound = false;
                    }
                    else if (_Events.TryGetValue(eventType, out PoolEventStore)) ; // Do-Nothing

                    if (PoolEventStore != null &&
                        PoolEventStore.Count > 0)
                    {
                        // Sync Lock
                        lock (PoolEventStore)
                        {
                            @event = PoolEventStore.First.Value as TEvent;
                            PoolEventStore.RemoveFirst();
                        }

                        return true;
                    }
                }
            }
            catch
            {
                // Debug.WriteLine("Error Message");
            }

            return false;
        }

        public void Clear()
        {
            if (_applicationEvents != null)
                _applicationEvents.Clear();
            if (_Events != null)
                _Events.Clear();
        }

        ~EventPool()
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
                if (_applicationEvents != null)
                    _applicationEvents.Clear();
                _applicationEvents = null;
                if (_Events != null)
                    _Events.Clear();
                _Events = null;
            }

            _disposed = true;
        }
    }
}
