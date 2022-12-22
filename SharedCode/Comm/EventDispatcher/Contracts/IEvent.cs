using System;
namespace SharedCode.Comm.EventDispatcher.Contracts
{
    public interface IEvent
    {
        DateTime OccurrenceTimeStamp
        {
            get;
            set;
        }

        DateTime ReceptionTimeStamp
        {
            get;
            set;
        }

        void Init();
    }
}