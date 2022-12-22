using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO.Ports;

namespace ReadSMS_AT_CS20
{
    public class GSMModemHandler:IDisposable
    {
        private AutoResetEvent receiveNow;
        private SerialPort port;
        private String portName;
        public const int DefaultBaudRate = 4800;
        private int baudRate;
        
        public String PortName
        {
            get { return portName; }
            set { portName = value; }
        }

        public int BaudRate
        {
            get { return baudRate; }
            set { baudRate = value; }
        }

        public GSMModemHandler()
        {
            port = null;
            receiveNow = new AutoResetEvent(false);
            PortName =  "COM4";
            BaudRate = DefaultBaudRate;
        }
    
        public bool InitModem(int retries)
        {
            for (int i = 0; i < retries; i++)
            {
                try
                {
                    port = OpenPort(PortName);
                    if (SendAT(5))
                        return true;
                    else
                        throw new Exception("Modem Initialze not working,Please check the cables and make sure the modem is in auto baudrate mode");
                }
                catch (Exception ex)
                {
                    if (port.IsOpen)
                        port.Close();
                    if (i == retries-1)
                        throw new Exception("GSM Modem is not initialized", ex);
                }
            }
            return false;
        }

        /// <summary>
        /// Method Tranfer the command on GSM Modem,
        /// </summary>
        /// <param name="command"></param>
        /// <param name="responseTimeout"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public string ExecCommand(string command, int responseTimeout, string errorMessage)
        {
            try
            {
                string s = command.Substring(0,3);
                port.DiscardOutBuffer();
                port.DiscardInBuffer();
                receiveNow.Reset();
                port.Write(command + "\r");
                //string input = ReadResponse(responseTimeout);
                Thread.Sleep(responseTimeout);
                string input = port.ReadExisting();
                if (input.Length == 0)
                    //throw new ApplicationException("No success message was received.");
                    return "";
                return input;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(errorMessage, ex);
            }
        }

        public string ExecCommand(string command, string errorMessage)
        {
            try
            {
                port.DiscardOutBuffer();
                port.DiscardInBuffer();
                receiveNow.Reset();
                port.Write(command + "\r");
                return command;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(errorMessage, ex);
            }
        }

        private bool SendAT(int Reties)
        {
            string s;
            int i;
            for (i = 0; i < Reties; i++)
            {
                s = ExecCommand("ATE0", 300, "Baud Rate Problem");
                if (s.Equals("\r\n" + "OK" + "\r\n"))
                    return true;
                s = ExecCommand("AT", 300, "Baud Rate Problem");
                if (s.Equals("\r\n" + "OK" + "\r\n"))
                    return true;
            }
            return false;
        }

        #region  Modem Serial Communication
        private SerialPort OpenPort(string portName)
        {
            return OpenPort(portName, DefaultBaudRate);
        }

        private SerialPort OpenPort(String portName, int BaudRate)
        { 
            SerialPort port = new SerialPort();
            port.PortName = portName;
            port.BaudRate = BaudRate;
            port.DataBits = 8;
            port.StopBits = StopBits.One;
            port.Parity = Parity.None;
            port.ReadTimeout = 500;
            port.WriteTimeout = 500;
            port.Encoding = Encoding.GetEncoding("iso-8859-1");
            port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
            port.Open();
            port.DtrEnable = true;
            port.RtsEnable = true;
            return port;
        }

        private void ClosePort(SerialPort port)
        {
            port.Close();
            port.DataReceived -= new SerialDataReceivedEventHandler(port_DataReceived);
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (e.EventType == SerialData.Chars)
                receiveNow.Set();
        }

        private string ReadResponse(int timeout)
        {
            int retries = 0;
            string buffer = string.Empty;
            
                retries++;
                if (receiveNow.WaitOne(timeout, false))
                {
                    string t = port.ReadExisting();
                    //Thread.Sleep(500);
                    buffer += t;
                }
                else
                {
                    if (buffer.Length > 0)
                        throw new ApplicationException("Response received is incomplete.");
                    //else
                    //    throw new ApplicationException("No data received from phone.");
                }
                return buffer;
            //while (!buffer.EndsWith("\r\nOK\r\n") && !buffer.EndsWith("\r\nERROR\r\n") && retries < 2);
           
        }

        
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            try 
            {
                port.Close();
                port = null;
            }
            catch (Exception ex)
            { 
                
            }
        }
        #endregion
    }
}
