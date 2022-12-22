namespace SharedCode.Comm.EventDispatcher.Contracts
{
    public delegate void EventHandlerDelegate<in TEvent>(TEvent @event) where TEvent : IEvent;
}