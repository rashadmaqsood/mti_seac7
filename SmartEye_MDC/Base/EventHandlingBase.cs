using System;
using SharedCode.Comm.HelperClasses;
using SharedCode.Comm.EventDispatcher.Contracts;

namespace SmartEyeControl_7.Base
{
    public interface IEventHandlingBase : IDisposable
    {
        IEventDispatcher ApplicationEventDispatcher
        {
            get;
            set;
        }

        bool Disposed { get; set; }
        void Dispose();

        void UnRegisterEventHandlers();
        void RegisterEventHandlers();
    }

    public abstract class EventHandlingBase : IEventHandlingBase
    {
        protected IEventDispatcher _applicationEventDispatcher;

        protected EventHandlingBase()
        {
            // Init With Null Event Dispatcher
            _applicationEventDispatcher = null;
        }

        protected EventHandlingBase(IEventDispatcher applicationEventDispatcher)
        {
            if (applicationEventDispatcher == null)
                throw new ArgumentNullException("applicationEventDispatcher");
            _applicationEventDispatcher = applicationEventDispatcher;
        }

        public IEventDispatcher ApplicationEventDispatcher
        {
            get { return _applicationEventDispatcher; }
            set { _applicationEventDispatcher = value; }
        }

        public bool Disposed { get; set; }

        protected abstract void Dispose(bool disposing);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void UnRegisterEventHandlers() { }
        public void RegisterEventHandlers() { }
    }
}