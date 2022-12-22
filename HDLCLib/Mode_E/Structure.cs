using System;
using System.IO.Ports;

namespace _HDLC.Mode_E
{
    
    public struct SerialPortInit
    {
        private BaudRate baudRate;
        private ushort dataBits;
        private StopBits stopBit;
        private System.IO.Ports.Parity parity;
        
        public BaudRate BaudRate
        {
            get { return baudRate; }
            set { baudRate = value; }
        }
        public ushort DataBits
        {
            get { return dataBits; }
            set { dataBits = value; }
        }
        public StopBits StopBit
        {
            get { return stopBit; }
            set { stopBit = value; }
        }
        public System.IO.Ports.Parity Parity
        {
            get { return parity; }
            set { parity = value; }
        } 

    }

    public enum BaudRate:byte
    {
        _300 = 0x30,                        ///0
        _600 = 0x31,                        ///1    
        _1200 = 0x32,                       ///2
        _2400 = 0x33,                       ///3
        _4800 = 0x34,                       ///4
        _9600 = 0x35,                       ///5
        _19200 = 0x36,                      ///6            
        
    }

    public enum ProtocolControl:byte
    { 
        _DataReadOutMode = 0x30,        /// '0'
        _ProgrammingMode = 0x31,        /// '1'
        _HDLCMode = 0x32,               /// '2'
    }

}
