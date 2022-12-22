namespace SharedCode.Comm.EventDispatcher.Contracts
{
    public interface IEventPool
    {
        bool TryAdd<TEvent>(TEvent @event) where TEvent : class, IEvent;
        bool TryGet<TEvent>(int hashCode, out TEvent @event) where TEvent : class, IEvent;

        bool TryRemove<TEvent>(out TEvent @event) where TEvent : class, IEvent;
        void Clear();
    }
}
