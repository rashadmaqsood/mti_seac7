using DLMS.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SharedCode.Comm.Param
{
    [XmlInclude(typeof(Param_HDLCSetup))]
    [Serializable]
    public class Param_HDLCSetup
    {
        #region Data_Members

        private CommSpeed _Comm_Speed = CommSpeed._4800;
        private ushort _maxInfoBufSizeTransmit;
        private ushort _maxInfoBufSizeReceive;
        private ushort _TransmitWinSize;
        private ushort _ReceiveWinSize;
        private ushort _DeviceAddress;
        private TimeSpan _InactivityTimeout;
        private TimeSpan _InterOctetInactivityTimeout;

        #endregion

        public Param_HDLCSetup()
        {
            _maxInfoBufSizeTransmit = _maxInfoBufSizeReceive = 128;
            _TransmitWinSize = _ReceiveWinSize = 1;
            _DeviceAddress = 17;

            _InactivityTimeout = TimeSpan.Zero;
            _InterOctetInactivityTimeout = TimeSpan.Zero;
        }

        public CommSpeed Comm_Speed
        {
            get { return _Comm_Speed; }
            set { _Comm_Speed = value; }
        }

        public ushort MaxInfoBufSizeTransmit
        {
            get { return _maxInfoBufSizeTransmit; }
            set { _maxInfoBufSizeTransmit = value; }
        }

        public ushort MaxInfoBufSizeReceive
        {
            get { return _maxInfoBufSizeReceive; }
            set { _maxInfoBufSizeReceive = value; }
        }

        public ushort TransmitWinSize
        {
            get { return _TransmitWinSize; }
            set { _TransmitWinSize = value; }
        }

        public ushort ReceiveWinSize
        {
            get { return _ReceiveWinSize; }
            set { _ReceiveWinSize = value; }
        }

        public ushort DeviceAddress
        {
            get { return _DeviceAddress; }
            set { _DeviceAddress = value; }
        }

        [XmlIgnore]
        public TimeSpan InactivityTimeout
        {
            get { return _InactivityTimeout; }
            set { _InactivityTimeout = value; }
        }

        public long _InactivityTimeout_
        {
            get { return InactivityTimeout.Ticks; }
            set { InactivityTimeout = new TimeSpan(value); }
        }

        [XmlIgnore]
        public TimeSpan InterOctetInactivityTimeout
        {
            get { return _InterOctetInactivityTimeout; }
            set { _InterOctetInactivityTimeout = value; }
        }

        public long _InterOctetInactivityTimeout_
        {
            get { return _InterOctetInactivityTimeout.Ticks; }
            set { _InterOctetInactivityTimeout = new TimeSpan(value); }
        }

    }
}
