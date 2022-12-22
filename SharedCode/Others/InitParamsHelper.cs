using DLMS;
using SharedCode.Comm.Param;
using SharedCode.Common;
using SharedCode.Properties;
using SharedCode.TCP_Communication;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SharedCode.Others
{
    public class InitParamsHelper
    {
        public InitTCPParams GetDefaultTCPParams()
        {
            try
            {
                InitTCPParams initTCPParams = new InitTCPParams();
                initTCPParams.ServerPort = Properties.Settings.Default.Port;
                initTCPParams.ServerIP = IPAddress.Any;
                initTCPParams.IsTCPTimeOut = false;
                initTCPParams.TCPIPTimeOut = new TimeSpan(0, 06, 30);
                return initTCPParams;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public InitCommuincationParams GetDefaultCommunicationParams()
        {
            try
            {
                InitCommuincationParams initCommParam = new InitCommuincationParams();
                initCommParam.DataReadTimeout = new TimeSpan(0, 1, 0);
                initCommParam.TCPInConnectionTimeout = new TimeSpan(0, 5, 0);
                string[] ComPorts = SerialPort.GetPortNames();
                Array.Sort<String>(ComPorts);
                initCommParam.IRCOMPort = ComPorts[0];
                initCommParam.IRCOMBaudRate = 4800;
                initCommParam.ModemCOMPort = ComPorts[ComPorts.Length - 1];
                initCommParam.MobileNumber = "00";
                initCommParam.ModemBaudRate = 4800;
                return initCommParam;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public InitHDLCParam GetDefaultHDLCParams()
        {
            try
            {
                InitHDLCParam hdlcParam = new InitHDLCParam();

                hdlcParam.HDLCAddressLength = _HDLC.AddressLength.Four_Byte;
                hdlcParam.DeviceAddress = 0x0011;    // Init HDLC Lower Destination Device Address
                hdlcParam.MaxInfoBufSizeTransmit = 0x80;
                hdlcParam.MaxInfoBufSizeReceive = 0x80;
                hdlcParam.TransmitWinSize = 1;
                hdlcParam.ReceiveWinSize = 1;
                hdlcParam.InactivityTimeout = new TimeSpan(0, 0, 0, 30, 0);
                hdlcParam.RequestResponseTimeout = new TimeSpan(0, 0, 0, 30, 0);
                hdlcParam.AvoidInActivityTimeout = true;
                hdlcParam.IsEnableRetrySend = true;

                return hdlcParam;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SMS_Params GetDefaultWakeUpSmsParams()
        {
            try
            {
                SMS_Params defaultSMSParams = new SMS_Params();
                defaultSMSParams.IP = IPAddress.Any;
                ///Define Default IP Params
                String localHostName = Dns.GetHostName();
                IPAddress[] localIPs = Dns.GetHostAddresses(localHostName);
                foreach (var item in localIPs)
                {
                    if (!IPAddress.IsLoopback(item) && item.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        defaultSMSParams.IP = item;
                        break;
                    }
                }
                defaultSMSParams.IP_Port = 4059;    ///default TCP DLMS_COSEM Port
                defaultSMSParams.Password = "123456789";
                defaultSMSParams.HDLC_TCP_FLAG1 = false;
                defaultSMSParams.IP_and_Port_FLAG3 = true;
                defaultSMSParams.TCP_UDP = 0x01;
                defaultSMSParams.Use_Backup_IP_FLAG4 = false;
                defaultSMSParams.Password_FLAG0 = true;
                return defaultSMSParams;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveWakeupSmsParams(SMS_Params WakeUpSmsParams)
        {
            try
            {
                Settings.Default.IP = WakeUpSmsParams.IP.ToString();
                Settings.Default.IP_Port = WakeUpSmsParams.IP_Port;
                Settings.Default.Password = WakeUpSmsParams.Password;
                Settings.Default.HDLC_TCP_FLAG1 = WakeUpSmsParams.HDLC_TCP_FLAG1;
                Settings.Default.IP_and_Port_FLAG3 = WakeUpSmsParams.IP_and_Port_FLAG3;
                Settings.Default.TCP_UDP = WakeUpSmsParams.TCP_UDP;
                Settings.Default.Use_Backup_IP_FLAG4 = WakeUpSmsParams.Use_Backup_IP_FLAG4;
                Settings.Default.Password_FLAG0 = WakeUpSmsParams.Password_FLAG0;

                Settings.Default.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SMS_Params LoadWakeupSmsParams()
        {
            try
            {
                SMS_Params SmsParams = new SMS_Params();
                IPAddress temp = null;
                bool ValidIP = IPAddress.TryParse(Settings.Default.IP, out temp);
                if (ValidIP)
                    SmsParams.IP = temp;
                else
                    SmsParams.IP = IPAddress.Any;
                SmsParams.IP_Port = Settings.Default.IP_Port;
                SmsParams.Password = Settings.Default.Password;
                SmsParams.HDLC_TCP_FLAG1 = Settings.Default.HDLC_TCP_FLAG1;
                SmsParams.IP_and_Port_FLAG3 = Settings.Default.IP_and_Port_FLAG3;
                SmsParams.TCP_UDP = Settings.Default.TCP_UDP;
                SmsParams.Use_Backup_IP_FLAG4 = Settings.Default.Use_Backup_IP_FLAG4;
                SmsParams.Password_FLAG0 = Settings.Default.Password_FLAG0;
                return SmsParams;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SaveTCPParams(InitTCPParams InitTCPParams)
        {
            try
            {
                Settings.Default.ServerIP = InitTCPParams.ServerIP.ToString();
                Settings.Default.Port = InitTCPParams.ServerPort;
                Settings.Default.isTCPTimeOut = InitTCPParams.IsTCPTimeOut;

                Settings.Default.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public InitTCPParams LoadTCPParams()
        {
            try
            {
                InitTCPParams TcpParams = new InitTCPParams();
                IPAddress temp = null;
                bool ValidIP = IPAddress.TryParse(Settings.Default.ServerIP, out temp);
                if (ValidIP)
                    TcpParams.ServerIP = temp;
                else
                    TcpParams.ServerIP = IPAddress.Any;
                TcpParams.ServerPort = Settings.Default.Port;
                TcpParams.TCPIPTimeOut = Settings.Default.TCPTimeOut;
                TcpParams.IsTCPTimeOut = Settings.Default.isTCPTimeOut;
                return TcpParams;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveCommuincationParams(InitCommuincationParams InitCommParams)
        {
            try
            {
                Settings.Default.DataReadTimeout = InitCommParams.DataReadTimeout;
                Settings.Default.TCPInConnectionTimeout = InitCommParams.TCPInConnectionTimeout;
                Settings.Default.IRCOMPort = InitCommParams.IRCOMPort.ToString();
                Settings.Default.IRCOMBaudRate = InitCommParams.IRCOMBaudRate;
                Settings.Default.ModemCOMPort = InitCommParams.ModemCOMPort;
                Settings.Default.MobileNumber = InitCommParams.MobileNumber;
                Settings.Default.ModemBaudRate = InitCommParams.ModemBaudRate;

                Settings.Default.Save();
                //Settings.Default.Reload();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public InitCommuincationParams LoadCommunicationParams()
        {

            InitCommuincationParams initCommParam = null;
            try
            {
                // Direct HDLC Mode Check
                initCommParam = new InitCommuincationParams();

                initCommParam.DataReadTimeout = Settings.Default.DataReadTimeout;
                initCommParam.TCPInConnectionTimeout = Settings.Default.TCPInConnectionTimeout;


                initCommParam.IRCOMPort = Settings.Default.IRCOMPort;
                initCommParam.IRCOMBaudRate = Settings.Default.IRCOMBaudRate;
                initCommParam.ModemCOMPort = Settings.Default.ModemCOMPort;
                initCommParam.MobileNumber = Settings.Default.MobileNumber;
                initCommParam.ModemBaudRate = Settings.Default.ModemBaudRate;

                return initCommParam;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveHDLCParams(InitHDLCParam hdlcParams, bool isDirectHDLC = false)
        {
            try
            {
                //string fileName = "_hdlcParams";
                //string directModefileName = "_DirectHdlcParams";
                //string hdlcParamsFile = string.Empty;

                //// Direct HDLC Mode Check
                //if (isDirectHDLC)
                //{
                //    hdlcParamsFile = String.Format(@"{0}{1}{2}_{3}.xml", App_Common.GetApplicationConfigsDirectory(), Path.DirectorySeparatorChar, MeterString, directModefileName);
                //}
                //else
                //    hdlcParamsFile = String.Format(@"{0}{1}{2}_{3}.xml", App_Common.GetApplicationConfigsDirectory(), Path.DirectorySeparatorChar, MeterString, fileName);

                //using (TextWriter WriteFileStream = new StreamWriter(hdlcParamsFile, false))
                //{
                //    XmlSerializer xmlSerl = new XmlSerializer(typeof(InitHDLCParam));
                //    xmlSerl.Serialize(WriteFileStream, hdlcParams);
                //}

                Settings.Default.HDLCAddressLength = Convert.ToUInt16(hdlcParams.HDLCAddressLength);
                Settings.Default.MaxInfoBufferTransmit = hdlcParams.MaxInfoBufSizeTransmit;
                Settings.Default.MaxInfoBufferReceive = hdlcParams.MaxInfoBufSizeReceive;
                Settings.Default.WinSizeTransmit = hdlcParams.TransmitWinSize;
                Settings.Default.WinSizeReceive = hdlcParams.ReceiveWinSize;
                Settings.Default.DeviceAddress = hdlcParams.DeviceAddress;
                Settings.Default.ResponseTimeOut = hdlcParams.RequestResponseTimeout;
                Settings.Default.InActivityTimeOut = hdlcParams.InactivityTimeout;
                Settings.Default.IsKAEnable = hdlcParams.AvoidInActivityTimeout;
                Settings.Default.IsEnableRetry = hdlcParams.IsEnableRetrySend;
                Settings.Default.IsSkipLoginParam = hdlcParams.IsSkipLoginParameter;

                Settings.Default.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public InitHDLCParam LoadHDLCParams()
        {
            InitHDLCParam hdlcParams = null;
            string parm_STR = string.Empty;

            try
            {
                // Direct HDLC Mode Check
                hdlcParams = new InitHDLCParam();

                bool lgc_Val = false;
                ushort buf_Val = 0;
                TimeSpan time_Val;

                ushort.TryParse(Settings.Default.HDLCAddressLength.ToString(), out buf_Val);

                switch (buf_Val)
                {
                    case 1:
                        hdlcParams.HDLCAddressLength = _HDLC.AddressLength.One_Byte;
                        break;
                    case 2:
                        hdlcParams.HDLCAddressLength = _HDLC.AddressLength.Two_Byte;
                        break;
                    case 4:
                        hdlcParams.HDLCAddressLength = _HDLC.AddressLength.Four_Byte;
                        break;
                    default:
                        hdlcParams.HDLCAddressLength = _HDLC.AddressLength.Four_Byte;
                        break;
                }

                ushort.TryParse(Settings.Default.MaxInfoBufferTransmit.ToString(), out buf_Val);
                hdlcParams.MaxInfoBufSizeTransmit = buf_Val;

                ushort.TryParse(Settings.Default.MaxInfoBufferReceive.ToString(), out buf_Val);
                hdlcParams.MaxInfoBufSizeReceive = buf_Val;

                ushort.TryParse(Settings.Default.WinSizeTransmit.ToString(), out buf_Val);
                if (buf_Val >= 1 && buf_Val <= 7)
                    hdlcParams.TransmitWinSize = buf_Val;
                else
                    hdlcParams.TransmitWinSize = 1;

                ushort.TryParse(Settings.Default.WinSizeReceive.ToString(), out buf_Val);
                if (buf_Val >= 1 && buf_Val <= 7)
                    hdlcParams.ReceiveWinSize = buf_Val;
                else
                    hdlcParams.ReceiveWinSize = 1;

                ushort.TryParse(Settings.Default.DeviceAddress.ToString(), out buf_Val);
                hdlcParams.DeviceAddress = buf_Val;

                TimeSpan.TryParse(Settings.Default.ResponseTimeOut.ToString(), out time_Val);
                if (time_Val >= TimeSpan.FromSeconds(05.0d) &&
                    time_Val <= TimeSpan.FromMinutes(05.0d))
                {
                    hdlcParams.RequestResponseTimeout = time_Val;
                    // hdlcParams._RequestResponseTimeout_ = Convert.ToInt64(time_Val.TotalMilliseconds);
                }
                else
                {
                    hdlcParams.RequestResponseTimeout = TimeSpan.FromSeconds(10.0d);
                    // hdlcParams._RequestResponseTimeout_ = Convert.ToInt64(hdlcParams.RequestResponseTimeout.TotalMilliseconds);
                }

                TimeSpan.TryParse(Settings.Default.InActivityTimeOut.ToString(), out time_Val);
                if (time_Val >= TimeSpan.FromSeconds(05.0d) && time_Val <= TimeSpan.FromMinutes(15.0d))
                {
                    hdlcParams.InactivityTimeout = time_Val;
                    // hdlcParams._InactivityTimeout_ = Convert.ToInt64(time_Val.TotalMilliseconds);
                }
                else
                {
                    hdlcParams.InactivityTimeout = time_Val;
                    // hdlcParams._InactivityTimeout_ = Convert.ToInt64(time_Val.TotalMilliseconds);
                }

                bool.TryParse(Settings.Default.IsKAEnable.ToString(), out lgc_Val);
                hdlcParams.AvoidInActivityTimeout = lgc_Val;

                bool.TryParse(Settings.Default.IsEnableRetry.ToString(), out lgc_Val);
                hdlcParams.IsEnableRetrySend = lgc_Val;

                bool.TryParse(Settings.Default.IsSkipLoginParam.ToString(), out lgc_Val);
                hdlcParams.IsSkipLoginParameter = lgc_Val;


                return hdlcParams;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
