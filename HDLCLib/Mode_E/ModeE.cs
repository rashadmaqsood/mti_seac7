using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.IO.Ports;
using System.Threading;

namespace _HDLC.Mode_E
{
    public class ModeE
    {
        #region DataMembers

        private System.Timers.Timer timer;
        private bool isTimeOut;
        private int retry;
        private int state;
        private BaudRate baudRate;
        private ProtocolControl protocol;
        private String strData;
        private SerialPortInit initSerialPort;
        private string deviceAddress;
        private string registerCode;

        #endregion
        #region Public Properties

        public int Retry
        {
            get { return retry; }
            set { retry = value; }
        }
        public BaudRate BaudRate
        {
            get { return baudRate; }
            set { baudRate = value; }
        }
        public ProtocolControl Protocol
        {
            get { return protocol; }
            set { protocol = value; }
        }
        public String StrData
        {
            get { return strData; }
            set { strData = value; }
        }
        public string DeviceAddress
        {
            get { return deviceAddress; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    if (value.Length <= 32)
                        deviceAddress = value;
                    else
                        throw new Exception("Invalid Device Address");
                }
                else
                    throw new Exception("Invalid Device Address");
            }
        }
        public string RegisterCode
        {
            get { return registerCode; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    if (value.Length <= 3)
                        registerCode = value;
                    else
                        throw new Exception("Invalid Device Address");
                }
                else
                    throw new Exception("Invalid Device Address");
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void setTimeOut(bool flag)
        {
            isTimeOut = flag;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        private bool IsTimeOut()
        {
            return isTimeOut;
        }

        #endregion
        #region Constructur

        public ModeE()
        {
            timer = new System.Timers.Timer(1000);  ///Initial One Second TimeOut
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            Retry = 5;
            state = 0;
            BaudRate = _HDLC.Mode_E.BaudRate._300;
            Protocol = ProtocolControl._HDLCMode;
            RegisterCode = "MTI";
        }

        #endregion
        #region Member Methods

        public bool EstablishModeE(SerialPort serialPort)
        {
            try
            {
                // Init Mode E Settings
                int ServerTimeOut = 0;
                state = 0;
                BaudRate = _HDLC.Mode_E.BaudRate._300;
                List<byte> frame = new List<byte>(100);
                int retryCount = 0;
                while (retryCount < Retry)
                {
                    #region State_0

                    if (state == 0)
                    {
                        frame.Clear();
                        setTimeOut(false);
                        // Init (300,7,E,1) 
                        BaudRate = _HDLC.Mode_E.BaudRate._300;
                        initSerialPort.BaudRate = _HDLC.Mode_E.BaudRate._300;
                        initSerialPort.DataBits = 7;
                        initSerialPort.Parity = Parity.Even;
                        initSerialPort.StopBit = StopBits.One;
                        InitSerialPort(serialPort, initSerialPort);
                        /// Build Sign Up Request        </><?><Device Address(32 max)><!><CR><Line Feed>
                        frame.Add((byte)'/');
                        frame.Add((byte)'?');
                        // if (DeviceAddress != null)
                        // {
                        //     foreach (char ch in DeviceAddress)
                        //     {
                        //         frame.Add((byte)ch);
                        //     }
                        // }
                        frame.Add((byte)'!');
                        frame.Add((byte)'\r');
                        frame.Add((byte)'\n');
                        serialPort.Write(frame.ToArray(), 0, frame.Count);      ///Write Data On Serial
                        serialPort.BaseStream.Flush();
                        timer.Stop();
                        timer.Interval = 5000;
                        timer.Start();
                        setTimeOut(false);
                        state = 1;
                    }

                    #endregion
                    #region State_1

                    else if (state == 1)
                    {
                        // Expected Frame /xxxZ\2DDDDD<CR><LineFeed>
                        // Scan Frame Start Character /
                        bool isVerified = true;
                        frame.Clear();
                        int SbyteCount = 0;
                        while (!isTimeOut)
                        {
                            if (serialPort.BytesToRead > 0)
                            {
                                byte frmStr = (byte)serialPort.ReadByte();
                                if (frmStr == (byte)'/')    ///Frame Start Char
                                {
                                    frame.Add(frmStr);
                                    SbyteCount = 1;
                                }
                                else if (frmStr == (byte)'\n' && SbyteCount > 0)
                                {
                                    frame.Add(frmStr);
                                    timer.Stop();
                                    break;
                                }
                                else if (SbyteCount > 0)
                                {
                                    frame.Add(frmStr);
                                    SbyteCount++;
                                }
                            }
                            else
                                System.Threading.Thread.Sleep(1);
                        }

                        if (isTimeOut)  ///No Valid Frame Data Received
                        {
                            isVerified = false;
                            state = 0;
                            retryCount++;
                            Thread.Sleep(250);
                            continue;
                        }
                        IEnumerator<byte> Iterator = frame.GetEnumerator();
                        // Verify FrameStart
                        if (Iterator.MoveNext() && Iterator.Current != (byte)'/')
                            isVerified = false;
                        // Read Registeration Code
                        String regCode = "";
                        if (Iterator.MoveNext() && isVerified)
                        {
                            regCode += (char)Iterator.Current;
                            Iterator.MoveNext();
                            regCode += (char)Iterator.Current;
                            Iterator.MoveNext();
                            regCode += (char)Iterator.Current;
                            if (String.Compare(RegisterCode, regCode, false) == 0 || true)
                            {
                                bool flag = true;
                                foreach (char ch in regCode)
                                    if (!char.IsUpper(ch))
                                    {
                                        flag = false;
                                        break;
                                    }
                                if (flag)
                                    ServerTimeOut = 200;    /// Server TimeOut 200Ms
                                else                            
                                    ServerTimeOut = 20;     /// Server TimeOut 20Ms
                            }
                            else
                                isVerified = false;
                        }
                        else
                            isVerified = false;
                        // Read Baud Rate
                        if (Iterator.MoveNext() && isVerified)
                        {
                            try
                            {
                                BaudRate =  (_HDLC.Mode_E.BaudRate)Iterator.Current;
                            }
                            catch (Exception)
                            {
                                isVerified = false;
                            }
                        }
                        // Read Protocol Mode E Identifier
                        if (Iterator.MoveNext() && isVerified && Iterator.Current == (byte)'\\')
                        {
                            //if (Iterator.Current == (byte)'\\')
                            //    isVerified = true;
                            //else
                            //    isVerified = false;
                            if (Iterator.MoveNext() && Iterator.Current == (byte)'2')
                                isVerified = true;
                            else
                                isVerified = false;

                        }
                        // Read Device Address
                        if (isVerified)
                        {
                            StringBuilder deviceAddress = new StringBuilder(14);
                            bool flagEnd = false;
                            for (int byteCount = 0; Iterator.MoveNext() && byteCount <= 14; byteCount++)
                            {
                                byte dt = Iterator.Current;
                                if (dt != (byte)'\r' && dt != (byte)'\n')
                                {
                                    deviceAddress = deviceAddress.Append(((char)dt));
                                }
                                else
                                {
                                    DeviceAddress = deviceAddress.ToString();
                                    flagEnd = true;
                                    break;
                                }
                                if (byteCount == 13)
                                {
                                    DeviceAddress = deviceAddress.ToString();
                                    flagEnd = true;
                                }
                            }
                            isVerified = flagEnd;
                        }
                        if (Iterator.MoveNext() && isVerified)
                            if (Iterator.Current != (byte)'\n')
                                isVerified = false;
                        if (!isVerified)
                        {
                            // Send Nack
                            byte[] buf = { 0x15 };
                            serialPort.Write(buf, 0, 1);
                            state = 0;
                        }
                        else
                            state = 2;
                    }
                     
                    #endregion
                    #region State_2

                    else if (state == 2)
                    {
                        // BaudRate = Mode_E.BaudRate._2400;
                        if (BaudRate > baudRate)
                            BaudRate = baudRate;
                        byte[] AckFrame = null;
                        bool isVerified = true;
                        frame.Clear();      ///Resend ACK At (300,E,1)
                        frame.Add(0x06);    ///ACK
                        frame.Add((byte)Protocol);
                        frame.Add((byte)BaudRate);
                        frame.Add((byte)Protocol);
                        frame.Add((byte)'\r');
                        frame.Add((byte)'\n');
                        // frame.Add((byte)'\n');

                        AckFrame = frame.ToArray();
                        setTimeOut(false);
                        timer.Stop();
                        timer.Interval = (ServerTimeOut == 250 || ServerTimeOut == 20) ? ServerTimeOut : 250;
                        timer.Start();
                        
                        serialPort.Write(AckFrame, 0, frame.Count);
                        serialPort.BaseStream.Flush();

                        #region 250 Ms-Wait Delay
                        
                        do { Thread.Sleep(1); } while (!isTimeOut);
                        timer.Stop();
                        
                        #endregion

                        switch (Protocol)
                        {
                            #region HDLCMode

                            case ProtocolControl._HDLCMode:

                                ///Init (z,7,E,1)
                                initSerialPort.BaudRate = BaudRate;
                                initSerialPort.DataBits = 7;
                                initSerialPort.Parity = Parity.Even;
                                initSerialPort.StopBit = StopBits.One;
                                InitSerialPort(serialPort, initSerialPort);
                                ///Receive Same Ack On Improved Baud Rate
                                setTimeOut(false);
                                timer.Stop();
                                timer.Interval = 3500;
                                timer.Start();
                                byte[] frmReceived = new byte[AckFrame.Length];
                                for (int index = 0; index < AckFrame.Length && !isTimeOut; )
                                {
                                    if (serialPort.BytesToRead > 0)
                                    {
                                        int _dt = serialPort.ReadByte();
                                        if (_dt == -1)
                                            break;
                                        else
                                        {
                                            frmReceived[index] = (byte)_dt;
                                            index++;
                                        }
                                    }
                                    else
                                        Thread.Sleep(1);
                                }
                                timer.Stop();
                                ///Read Should be complete
                                if (isTimeOut)
                                {
                                    ///no data received
                                    isVerified = false;
                                }
                                else
                                {
                                    ///Should read Same Packet as if last ACK sent
                                    isVerified = true;
                                    for (int index = 0; index < frmReceived.Length; index++)
                                        if (frmReceived[index] != AckFrame[index])
                                        {
                                            isVerified = false;
                                            break;
                                        }
                                }

                                if (isVerified)
                                {
                                    ///Init (z,8,N,1)  
                                    initSerialPort.BaudRate = BaudRate;
                                    initSerialPort.DataBits = 8;
                                    initSerialPort.Parity = Parity.None;
                                    initSerialPort.StopBit = StopBits.One;
                                    InitSerialPort(serialPort, initSerialPort);
                                    state = 3;
                                }
                                else
                                    state = 0;
                                break;

                            #endregion
                            case ProtocolControl._DataReadOutMode:
                                #region DataReadOutMode

                                ///<STX-02><DATA><!><CR><LF><ETX-03><BCC>
                                setTimeOut(false);
                                timer.Interval = 5000;
                                ///Init (z,7,E,1) 
                                initSerialPort.BaudRate = BaudRate;
                                initSerialPort.DataBits = 7;
                                initSerialPort.Parity = Parity.Even;
                                initSerialPort.StopBit = StopBits.One;
                                ///InitSerialPort(serialPort, initSerialPort);
                                timer.Enabled = true;
                                frame.Clear();
                                bool flag = false;
                                int count = 4;
                                while (!isTimeOut)
                                {
                                    if (serialPort.BytesToRead > 0)
                                    {
                                        byte _byte = (byte)serialPort.ReadByte();
                                        if (_byte == (byte)'\r')
                                        {
                                            flag = true;
                                        }
                                        frame.Add(_byte);
                                        if (count > 0 && flag)
                                            count--;
                                        else if (count <= 0)
                                        {
                                            break;
                                        }
                                        timer.Enabled = false;
                                        timer.Enabled = true;
                                    }
                                    else
                                    {
                                        Thread.Sleep(ServerTimeOut);
                                        isVerified = false;
                                    }
                                }
                                if (flag)
                                    isVerified = true;
                                IEnumerator<byte> Iterator = frame.GetEnumerator();
                                ///Verify Frame
                                if (!isVerified || !Iterator.MoveNext() || Iterator.Current != 0x02)
                                    isVerified = false;
                                StringBuilder data = new StringBuilder(10);
                                int BCC = 0;
                                while (isVerified && !Iterator.MoveNext())
                                {
                                    if (Iterator.Current == (byte)'!')
                                        break;
                                    else
                                        data.Append(Iterator.Current);
                                }
                                if (!isVerified || Iterator.Current != (byte)'!')
                                    isVerified = false;
                                if (!isVerified || !Iterator.MoveNext() || Iterator.Current != (byte)'\r')
                                    isVerified = false;
                                if (!isVerified || !Iterator.MoveNext() || Iterator.Current != (byte)'\n')
                                    isVerified = false;
                                if (!isVerified || !Iterator.MoveNext() || Iterator.Current != 0x03)        ///ETX 0x03
                                    isVerified = false;
                                if (!isVerified || !Iterator.MoveNext())                                    ///BCC 
                                    isVerified = false;
                                else
                                    BCC = Iterator.Current;
                                ///Compute BCC! & Verify 
                                int _BCC = 0;
                                if (isVerified)
                                    foreach (char ch in data.ToString())
                                    {
                                        _BCC ^= (int)ch;
                                    }
                                if (BCC != _BCC)
                                    isVerified = false;
                                if (isVerified)
                                {
                                    strData = data.ToString();
                                }
                                return isVerified;

                                #endregion
                            case ProtocolControl._ProgrammingMode:
                                #region Programming Mode

                                ///Not Implemented Yet
                                return false;

                                #endregion
                                break;
                        }
                    }

                    #endregion
                    #region State_3

                    else if (state == 3)
                    {
                        return true;
                    }

                    #endregion
                    if (state == 0)
                    {
                        retryCount++;
                        Thread.Sleep(5);     ///Wait next trystate_0 
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                ///Log Error
                return false;
            }
        }

        
        private void InitSerialPort(SerialPort port, _HDLC.Mode_E.SerialPortInit init)
        {
            port.DiscardInBuffer();
            port.DiscardOutBuffer();

            ///port.Close();
            switch (init.BaudRate)
            {
                #region BaudRates

                case _HDLC.Mode_E.BaudRate._300:
                    port.BaudRate = 300;
                    break;
                case _HDLC.Mode_E.BaudRate._600:
                    port.BaudRate = 600;
                    break;
                case _HDLC.Mode_E.BaudRate._1200:
                    port.BaudRate = 1200;
                    break;
                case _HDLC.Mode_E.BaudRate._2400:
                    port.BaudRate = 2400;
                    break;
                case _HDLC.Mode_E.BaudRate._4800:
                    port.BaudRate = 4800;
                    break;
                case _HDLC.Mode_E.BaudRate._9600:
                    port.BaudRate = 9600;
                    break;
                case _HDLC.Mode_E.BaudRate._19200:
                    port.BaudRate = 19200;
                    break;

                #endregion
            }

            port.DataBits = init.DataBits;
            port.Parity = init.Parity;
            port.StopBits = init.StopBit;
            
            //port.RtsEnable = true;
            //port.DtrEnable = true;// ahmed
            ///port.Open();
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            setTimeOut(true);
        }

        #endregion
    }
}
