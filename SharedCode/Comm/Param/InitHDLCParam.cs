using System;
using System.Xml.Serialization;
using _HDLC;

namespace SharedCode.Comm.Param
{
    [XmlInclude(typeof(_HDLC.AddressLength))]
    [Serializable]
    public class InitHDLCParam
    {
        private ushort _maxInfoBufSizeTransmit;
        private ushort _maxInfoBufSizeReceive;
        private ushort _TransmitWinSize;
        private ushort _ReceiveWinSize;
        private ushort _DeviceAddress;
        private TimeSpan _InactivityTimeout;
        private TimeSpan _RequestResponseTimeout;
        private bool _AvoidInActivityTimeout;
        
        private bool _IsEnableRetrySend;
        private AddressLength _hdlcAddressLength;

        private bool _skipLoginParams;

        public bool IsSkipLoginParameter
        {
            get { return _skipLoginParams; }
            set { _skipLoginParams = value; }
        }

        public AddressLength HDLCAddressLength
        {
            get { return _hdlcAddressLength; }
            set { _hdlcAddressLength = value; }
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
        

        public TimeSpan InactivityTimeout
        {
            get { return _InactivityTimeout; }
            set { _InactivityTimeout = value; }
        }

        public long _InactivityTimeout_
        {
            get { return InactivityTimeout.Ticks; }
            set { InactivityTimeout  = new TimeSpan( value); }
        }

        public TimeSpan RequestResponseTimeout
        {
            get { return _RequestResponseTimeout; }
            set { _RequestResponseTimeout = value; }
        }

        public long _RequestResponseTimeout_
        {
            get { return RequestResponseTimeout.Ticks; }
            set { RequestResponseTimeout = new TimeSpan( value); }
        }

        public bool AvoidInActivityTimeout
        {
            get { return _AvoidInActivityTimeout; }
            set { _AvoidInActivityTimeout = value; }
        }

        public bool IsEnableRetrySend
        {
            get { return _IsEnableRetrySend; }
            set { _IsEnableRetrySend = value; }
        }

        public InitHDLCParam()
        {
            _hdlcAddressLength = AddressLength.Four_Byte;
            _AvoidInActivityTimeout = true;
            _IsEnableRetrySend = true;
        }

    }

    


}
