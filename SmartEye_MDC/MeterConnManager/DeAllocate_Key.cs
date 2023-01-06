using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using comm;
using SharedCode.Comm.HelperClasses;

namespace Communicator.MeterConnManager
{
    public class DeAllocate_Key : IComparer, IComparable, IEqualityComparer<DeAllocate_Key>, IEquatable<DeAllocate_Key>
    {
        #region Data_Members

        private IOConnection Conn;
        internal DateTime DisconnectionTime;

        #endregion

        #region Constructor

        public DeAllocate_Key(IOConnection IOCOnn, DateTime DateTimeStamp)
        {
            Conn = IOCOnn;
            DisconnectionTime = DateTimeStamp;
        }

        public DeAllocate_Key(IOConnection IOConn) : this(IOConn, DateTime.Now) { }

        #endregion

        #region Public Properties

        public IOConnection IOConnection
        {
            get { return Conn; }
        }

        public DateTime DeallocateTime
        {
            get { return DisconnectionTime; }
        }

        #endregion

        #region IComparable Members

        int IComparable.CompareTo(object obj)
        {
            try
            {
                return Compare(this, obj);
            }
            catch (Exception)
            { }
            return -1;
        }

        public int CompareTo(object obj)
        {
            try
            {
                return Compare(this, obj);
            }
            catch (Exception)
            { }
            return -1;
        }

        public int Compare(object x, object y)
        {
            try
            {
                if (x == null || !(x is DeAllocate_Key))
                    return -1;
                else if (y == null || !(y is DeAllocate_Key))
                    return 1;
                else
                    return ((DeAllocate_Key)x).DeallocateTime.CompareTo(((DeAllocate_Key)y).DeallocateTime);
            }
            catch (Exception)
            { }
            return -1;
        }

        #endregion

        #region IEqualityComparer<DeAllocate_Key> Members

        public bool Equals(DeAllocate_Key x, DeAllocate_Key y)
        {
            try
            {
                return x == y || (((IComparable<IOConnection>)x.IOConnection).CompareTo(y.IOConnection) == 0);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int GetHashCode(DeAllocate_Key obj)
        {
            return IOConnection.GetHashCode();
        }

        public bool Equals(DeAllocate_Key other)
        {
            return Equals(this, other);
        }

        public override bool Equals(object obj)
        {
            try
            {
                return this.Equals(((DeAllocate_Key)this), ((DeAllocate_Key)obj));
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        public override string ToString()
        {
            return IOConnection.ToString();
        }
    }

    public class DeAllocate_KeyComparer : EqualityComparer<DeAllocate_Key>
    {
        public override int GetHashCode(DeAllocate_Key bx)
        {
            return bx.GetHashCode(bx);
        }

        public override bool Equals(DeAllocate_Key b1, DeAllocate_Key b2)
        {
            return b1.Equals(b1, b2);
        }
    }

}
