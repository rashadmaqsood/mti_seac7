using SharedCode.Comm.DataContainer;
using SharedCode.Comm.EventDispatcher.Contracts;
using System;

namespace SharedCode.Comm.HelperClasses
{
    public class MSN_Notification : IEvent
    {
        private MeterSerialNumber _msn;
        private int _hashCode;

        public MSN_Notification()
        {
            _msn = null;
            _hashCode = -1;
        }

        public MSN_Notification(uint msnArg)
        {
            _msn = new MeterSerialNumber() { MSN = msnArg };
            _hashCode = GetHashCode();
        }

        public MeterSerialNumber MSN
        {
            get { return _msn; }
            set { _msn = value; }
        }

        public DateTime OccurrenceTimeStamp
        {
            get;
            set;
        }

        public DateTime ReceptionTimeStamp
        {
            get;
            set;
        }

        public void Init()
        {
            _msn = null;
            _hashCode = -1;
        }

        public int HashCode
        {
            get
            {
                if (_hashCode <= 0)
                {
                    _hashCode = GetHashCode();
                }
                return _hashCode;
            }
        }

        private new int GetHashCode()
        {
            // Ref: http://stackoverflow.com/a/263416/254215
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 23 + GetType().ToString().GetHashCode();
                hash = hash * 23 + _msn.GetHashCode();

                if (OccurrenceTimeStamp != null &&
                    OccurrenceTimeStamp != DateTime.MinValue)
                    hash = hash * 23 + OccurrenceTimeStamp.GetHashCode();

                if (ReceptionTimeStamp != null &&
                    ReceptionTimeStamp != DateTime.MinValue)
                    hash = hash * 23 + ReceptionTimeStamp.GetHashCode();

                return hash;
            }
        }

        public override string ToString()
        {
            String ToStringMsg = "MSN Notification \r\n";

            try
            {
                ToStringMsg += string.Format("MSN:{0}\r\n Process Stamp {1} \r\n", MSN, ReceptionTimeStamp);
            }
            catch
            {
                ToStringMsg += "Error";
            }

            return ToStringMsg;
        }
    }
}
