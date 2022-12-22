using System;

namespace SharedCode.Comm.EventDispatcher.Contracts
{
    public interface IEventDispatcher : IDisposable
    {
        void AddListener<TEvent>(EventHandlerDelegate<TEvent> handler) where TEvent : IEvent;
        void RemoveListener<TEvent>(EventHandlerDelegate<TEvent> handler) where TEvent : IEvent;
        void Dispatch<TEvent>(TEvent @event) where TEvent : IEvent;

        void Dispatch<TEvent>(TEvent @event, byte AsyncEveDispatch) where TEvent : IEvent;
    }
}