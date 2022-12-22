using System;
using System.Diagnostics;
using System.IO.Ports;
using _HDLC;
using System.IO;
using SharedCode.Comm.Param;
using _HDLC.Mode_E;

namespace SharedCode.Controllers
{
    public class OpticalPortController
    {
        #region DataMembers
        private SerialPort serialPort;
        private ModeE modeEController;
        private HDLC HDLCConnection;
        private byte[] buffer;
        private int portBufferSize = 1024;
        private int offset, length;
        private bool IsModeE;
        #endregion

        #region Properties

        public ModeE ModeEController
        {
            get { return modeEController; }
        }
        public HDLC HDLCConnection_
        {
            get { return HDLCConnection; }
        }
        public int PortBufferSize
        {
            get { return portBufferSize; }
            set { portBufferSize = value; }
        }
        #endregion

        #region Constructor
        public OpticalPortController()
        {
            modeEController = new ModeE();
            HDLCConnection = new HDLC();
            ///ModeE Parameters
            HDLCConnection_.HDLCDisconnected += new Action(HDLCConnection__HDLCDisconnected);
            HDLCConnection_.TransmitFrame += new TransmitData(TransmitData);
        }
        #endregion

        #region Pubilc Methods
        public void InitPort(String ComPort)
        {
            if(serialPort == null || !serialPort.PortName.ToLower().Equals(ComPort.ToLower()) || !serialPort.IsOpen)
            {
                serialPort = new SerialPort(ComPort);
                ///SerialPort Default Configs
                serialPort.ReceivedBytesThreshold = 1;
                serialPort.ReadBufferSize = PortBufferSize;
                ///serialPort.CtsHolding = true;
                serialPort.RtsEnable = true; //this was true
                serialPort.DtrEnable = true; //temp change by ahmed...this wasnt here
                serialPort.Open();
            }
        }
        public bool EstablishModeE(String ComPort)
        {
            try
            {
                InitPort(ComPort);
                ///Params To Establish HDLC Over MODE_E
                modeEController.BaudRate = BaudRate._300;

                modeEController.Protocol = ProtocolControl._HDLCMode;
                IsModeE = modeEController.EstablishModeE(serialPort);
                return IsModeE;
            }
            catch (Exception ex)
            {
                if (serialPort != null)
                {
                    serialPort.Close();
                    serialPort.Dispose();
                }
                throw new Exception("Unable to establis Mode E", ex);
            }
            finally
            {
                if (!IsModeE)
                {
                    serialPort.Dispose();
                }
            }
        }

        public bool ConnectHdlc()
        {
            try
            {
                if (!IsModeE)
                    throw new Exception("Unable To Connect,Mode E Not Establish");
                if (!HDLCConnection_.Connected)  ///Attach Serial Port Handlers
                {
                    serialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
                    buffer = new byte[PortBufferSize];
                    offset = length = 0;
                }
                else
                    throw new Exception("Already Connected In HDLC Mode");
                HDLCConnection_.Connect();
                return true;
            }
            catch (Exception ex)
            {
                IsModeE = false;
                serialPort.Dispose();
                throw ex;
            }
        }

        public bool ConnectDirectHDLC(String ComPort, int baudRate)
        {
            try
            {
                serialPort = new SerialPort(ComPort);
                ///SerialPort Default Configs
                serialPort.ReceivedBytesThreshold = 1;
                serialPort.ReadBufferSize = 1024;
                serialPort.DataBits = 8;
                serialPort.Parity = Parity.None;
                serialPort.StopBits = StopBits.One;
                serialPort.RtsEnable = true;
                serialPort.DtrEnable = true;
                serialPort.Open();

                if (!HDLCConnection_.Connected)  ///Attach Serial Port Handlers
                {
                    serialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
                    buffer = new byte[PortBufferSize];
                    offset = length = 0;
                }
                else
                    throw new Exception("Already Connected In HDLC Mode");
                HDLCConnection_.Connect();
                return true;
            }
            catch (Exception ex)
            {
                IsModeE = false;
                serialPort.Dispose();
                throw ex;
            }
        }

        public SerialPort ConnectIPOverSerialLink(String ComPort, int baudRate)
        {
            SerialPort serialPort = null;
            try
            {
                serialPort = new SerialPort(ComPort);
                ///SerialPort Default Configs
                serialPort.BaudRate = baudRate;
                serialPort.ReceivedBytesThreshold = 1;
                serialPort.ReadBufferSize = 1024;
                serialPort.DataBits = 8;
                serialPort.Parity = Parity.None;
                serialPort.StopBits = StopBits.One;
                serialPort.RtsEnable = true;
                serialPort.DtrEnable = true;
                serialPort.Open();
                return serialPort;
            }
            catch (Exception ex)
            {
                throw new IOException(String.Format("Error occurred while open serial port link {0} at baud rate {1}", ComPort, baudRate));
            }

        }

        public void InitHDLCParams(InitHDLCParam hdlcParam)
        {
            try
            {
                HDLCConnection_.AvoidInactivityTimeOut = hdlcParam.AvoidInActivityTimeout;
                HDLCConnection_.DestinationAddress = hdlcParam.DeviceAddress;

                HDLCConnection_.IsEnableRetrySend = hdlcParam.IsEnableRetrySend;
                HDLCConnection.DestinationAddressLength = hdlcParam.HDLCAddressLength;
                HDLCConnection_.InactivityTimeOut = hdlcParam.InactivityTimeout;
                HDLCConnection_.ReqResTimeOut = hdlcParam.RequestResponseTimeout;
                HDLCConnection_.MaxInfoBufReceive = hdlcParam.MaxInfoBufSizeReceive;
                HDLCConnection_.MaxInfoBufTransmit = hdlcParam.MaxInfoBufSizeTransmit;
                HDLCConnection_.TransmitWinSize = hdlcParam.TransmitWinSize;
                HDLCConnection_.ReceiveWinSize = hdlcParam.ReceiveWinSize;
            }
            catch (Exception ex) { throw ex; }
        }

        public bool DisConnectHdlc()
        {
            try
            {

                if (HDLCConnection_.Connected)  ///Attach Serial Port Handlers
                {
                    HDLCConnection_.Disconnect();
                    IsModeE = false;
                    serialPort.Dispose();
                    buffer = null;
                    offset = length = 0;
                }
                return true;
            }
            catch (Exception ex)
            {
                IsModeE = false;
                serialPort.Dispose();
                throw ex;
            }
        }

        #endregion

        #region Private_Utility_Methods

        private void SerialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                if (serialPort.BytesToRead > 0)
                {
                    int byteToRead = serialPort.BytesToRead;
                    if (offset + length + byteToRead > this.buffer.Length)
                    {
                        offset = 0;
                        length = 0;
                    }
                    offset += length;
                    serialPort.Read(buffer, offset, byteToRead);
                    length = byteToRead;       ///No of bytes beyond offset 
                    HDLCConnection_.ReceiveRawData(buffer, offset, length);

                }
            }
            catch (Exception ex)
            {

                ///throw;  ///LOG ERROR
                offset = 0;
                length = 0;
                //Console.Out.WriteLine("Error Occurred Receiving Data On Serial Port" + ex.Message);
                Debug.WriteLine(ex.Message, "Serial Data Receiving Thread");
                //throw ex;
            }

        }
        private void TransmitData(byte[] data)
        {
            try
            {
                if (data != null)
                {
                    if (!serialPort.IsOpen)
                        serialPort.Open();
                    serialPort.Write(data, 0, data.Length);
                    serialPort.BaseStream.Flush();
                }
            }
            catch (_HDLC.HDLCErrorException ex)
            {
                Console.Out.WriteLine("Error Transmitting Data On Channel," + ex.Message);
                Debug.WriteLine("Error Transmitting Data On Channel," + ex.Message);
            }
            catch (HDLCInValidFrameException ex)
            {
                Console.Out.WriteLine("Error Transmitting Data On Channel," + ex.Message);
                Debug.WriteLine("Error Transmitting Data On Channel," + ex.Message);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Error Transmitting Data On Channel," + ex.Message);
                Debug.WriteLine("Error Transmitting Data On Channel," + ex.Message);
            }
        }
        private void HDLCConnection__HDLCDisconnected()
        {
            try
            {
                IsModeE = false;
                buffer = null;
                serialPort.Dispose();
            }
            catch
            { }
        }

        #endregion

    }

}
