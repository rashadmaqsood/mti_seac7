using System;
using System.Net;
using System.Xml.Serialization;

namespace SharedCode.Comm.Param
{
    public class InitTCPParams
    {
        private int _ServerPort;
        private IPAddress _serverIP;
        private bool isTimeOut;

        
        private TimeSpan TCPTimeOut;

        
        [XmlIgnore()]
        public IPAddress ServerIP
        {
            get { return _serverIP; }
            set { _serverIP = value; }
        }

        [XmlElement("ServerPort", Type = typeof(int))]
        public int ServerPort
        {
            get { return _ServerPort; }
            set { _ServerPort = value; }
        }


        /// <summary>
        /// _ServerIP represent Raw server IP 
        /// </summary>
        [XmlArray("RawServerIP")]
        public byte[] _ServerIP
        {
            get 
            {
                if (ServerIP == null)
                    return null;
                else
                    return ServerIP.GetAddressBytes();
            }
            set
            {
                IPAddress t = new IPAddress(value);
                ServerIP = t;
            }
        }

        [XmlElement("TCPIPTimeOut", Type = typeof(bool))]
        public bool IsTCPTimeOut
        {
            get { return isTimeOut; }
            set { isTimeOut = value; }
        }

        [XmlIgnore()]
        public TimeSpan TCPIPTimeOut
        {
            get { return TCPTimeOut; }
            set { TCPTimeOut = value; }
        }

        [XmlElement("TCPIPTimeOutRaw", Type = typeof(long))]
        public long TCPIPTimeOutRaw
        {
            get { return TCPTimeOut.Ticks; }
            set {  TCPTimeOut = new TimeSpan( value); }
        }
    }
}
