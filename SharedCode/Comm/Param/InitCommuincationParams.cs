using System;
using System.Xml.Serialization;

namespace SharedCode.Comm.Param
{
    public class InitCommuincationParams
    {
        #region Date Members

        private String _IRCOMPort;
        private int _IRCOMBaudRate;
        private String _ModemCOMPort;
        private int _ModemBaudRate;
        private String _MobNumber;
        private TimeSpan _DataReadTimeout;
        private TimeSpan _TCPInConnectionTimeout; 

        #endregion

        [XmlElement("IRCOMPort", Type = typeof(String))]
        public String IRCOMPort
        {
            get { return _IRCOMPort; }
            set { _IRCOMPort = value; }
        }
        
        [XmlElement("COMBaudRate", Type = typeof(int))]
        public int IRCOMBaudRate
        {
            get { return _IRCOMBaudRate; }
            set { _IRCOMBaudRate = value; }
        }

        [XmlElement("ModemCOMPort", Type = typeof(String))]
        public String ModemCOMPort
        {
            get { return _ModemCOMPort; }
            set { _ModemCOMPort = value; }
        }
        [XmlElement("ModemBaudRate", Type = typeof(int))]
        public int ModemBaudRate
        {
            get { return _ModemBaudRate; }
            set { _ModemBaudRate = value; }
        }
        [XmlElement("MobileNumber", Type = typeof(String))]
        public String MobileNumber
        {
            get { return _MobNumber; }
            set { _MobNumber = value; }
        }
        [XmlIgnore()]
        public TimeSpan DataReadTimeout
        {
            get { return _DataReadTimeout; }
            set { _DataReadTimeout = value; }
        }
        [XmlIgnore()]
        public TimeSpan TCPInConnectionTimeout
        {
            get { return _TCPInConnectionTimeout; }
            set { _TCPInConnectionTimeout = value; }
        }
        [XmlElement("DateReadTimeOut",Type = typeof(long))]
        public long _DataReadTimeout_
        {
            get 
            {
                return DataReadTimeout.Ticks;
            }
            set
            {
                DataReadTimeout = new TimeSpan(value);
            }
        }
        [XmlElement("TCPInConnectionTimeOut", Type = typeof(long))]
        public long _TCPInConnectionTimeout_
        {
            get
            {
                return TCPInConnectionTimeout.Ticks;
            }
            set
            {
                TCPInConnectionTimeout = new TimeSpan(value);
            }
        }
    }

    
}
