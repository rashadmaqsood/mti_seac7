using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Net;
using SharedCode.TCP_Communication;
using SharedCode.Comm.Param;
using SharedCode.Others;
using _HDLC;
//using SharedCode;

namespace GUI
{
    public partial class Configuration_parameters : Form
    {
        private InitHDLCParam _initHdlcParams;
        //private InitHDLCParam _initDirectHdlcParams;
        private InitTCPParams _initTCPParams;
        private InitCommuincationParams _initCommuincationParams;
        private SMS_Params _initSMSParams;
        private InitParamsHelper InitParam;

        public Configuration_parameters()
        {
            InitializeComponent();
            InitParam = new InitParamsHelper();
        }

        private void ConfigParams_Load(object sender, EventArgs e)
        {
            Get_COMM_Ports();
            ///Loading HDLC Parameters
            try
            {
                _initHdlcParams = InitParam.LoadHDLCParams();        // load into RAM
                PopulateGUI_HDLCParams(_initHdlcParams);
            }
            catch (Exception ex)
            {
                _initHdlcParams = InitParam.GetDefaultHDLCParams();  // else shoe the default values
                PopulateGUI_HDLCParams(_initHdlcParams);
            }
            ///Loading TCPIP Parameters
            try
            {
                _initTCPParams = InitParam.LoadTCPParams();
                PopulateGUI_TCPParams(_initTCPParams);

            }
            catch (Exception ex)
            {
                _initTCPParams = InitParam.GetDefaultTCPParams();
                PopulateGUI_TCPParams(_initTCPParams);
            }
            ///Loading Communication Parameters
            try
            {
                _initCommuincationParams = InitParam.LoadCommunicationParams();
                PopulateGUI_CommuincationParams(_initCommuincationParams);
            }
            catch (Exception)
            {
                 _initCommuincationParams = InitParam.GetDefaultCommunicationParams();
                PopulateGUI_CommuincationParams(_initCommuincationParams);
            }
            ///Loading Wake UP SMS Parameters
            try
            {
                _initSMSParams = InitParam.LoadWakeupSmsParams();
                PopulateGUI_SMSParams(_initSMSParams);
            }
            catch (Exception)
            {
                try
                {
                    _initSMSParams = InitParam.GetDefaultWakeUpSmsParams();
                   PopulateGUI_SMSParams(_initSMSParams);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        private void PopulateGUI_HDLCParams(InitHDLCParam hdlcParams)
        {
            txt_maxInfoBufSizeTransmit.Text = hdlcParams.MaxInfoBufSizeTransmit.ToString();
            txt_maxInfoBufSizeReceive.Text = hdlcParams.MaxInfoBufSizeReceive.ToString();
            txt_ReceiveWinSize.Text = hdlcParams.ReceiveWinSize.ToString();
            txt_TransmitWinSize.Text = hdlcParams.TransmitWinSize.ToString();
            txt_DeviceAddress.Text = hdlcParams.DeviceAddress.ToString();
            
            txt__InactivityTimeout.Value = txt__InactivityTimeout.Value.Subtract(txt__InactivityTimeout.Value.TimeOfDay);
            txt__InactivityTimeout.Value = txt__InactivityTimeout.Value.Add(hdlcParams.InactivityTimeout);

            txt__RequestResponseTimeout.Value = txt__RequestResponseTimeout.Value.Subtract(txt__RequestResponseTimeout.Value.TimeOfDay);
            txt__RequestResponseTimeout.Value = txt__RequestResponseTimeout.Value.Add(hdlcParams.RequestResponseTimeout);  
            
            check__AvoidInActivityTimeout.Checked = hdlcParams.AvoidInActivityTimeout;
            chk_EnableRetrySend.Checked = hdlcParams.IsEnableRetrySend;
            chk_SkipParams.Checked = hdlcParams.IsSkipLoginParameter;

            switch (hdlcParams.HDLCAddressLength)
            {
                case _HDLC.AddressLength.One_Byte:
                    cmbAddressLength.SelectedIndex = 0;
                    break;
                case _HDLC.AddressLength.Two_Byte:
                    cmbAddressLength.SelectedIndex = 1;
                    break;
                case _HDLC.AddressLength.Four_Byte:
                    cmbAddressLength.SelectedIndex = 2;
                    break;
                default:
                    cmbAddressLength.SelectedIndex = 2;
                    break;
            }
        }

        private InitHDLCParam SaveGUI_HDLCParams()
        {
            InitHDLCParam _initHdlcParams = new InitHDLCParam();
            _initHdlcParams.InactivityTimeout = txt__InactivityTimeout.Value.TimeOfDay;
           _initHdlcParams.RequestResponseTimeout =  txt__RequestResponseTimeout.Value.TimeOfDay;
           _initHdlcParams.MaxInfoBufSizeTransmit = ushort.Parse(txt_maxInfoBufSizeTransmit.Text);
           _initHdlcParams.MaxInfoBufSizeReceive = ushort.Parse(txt_maxInfoBufSizeReceive.Text);
           _initHdlcParams.ReceiveWinSize = ushort.Parse(txt_ReceiveWinSize.Text);
           _initHdlcParams.TransmitWinSize = ushort.Parse(txt_TransmitWinSize.Text);
           _initHdlcParams.DeviceAddress = ushort.Parse( txt_DeviceAddress.Text);
           _initHdlcParams.AvoidInActivityTimeout  = check__AvoidInActivityTimeout.Checked;
            _initHdlcParams.IsEnableRetrySend = chk_EnableRetrySend.Checked;
            _initHdlcParams.IsSkipLoginParameter = chk_SkipParams.Checked;

            _initHdlcParams.HDLCAddressLength = (_HDLC.AddressLength)(Convert.ToByte(cmbAddressLength.SelectedItem.ToString()));
            //_initHdlcParams.HDLCAddressLength = AddressLength.Four_Byte;

           return _initHdlcParams;
        }

        private SMS_Params SaveGUI_SMSParams()
        {
            SMS_Params Param_sms = new SMS_Params();
            IPAddress ip = IPAddress.Parse(txt_IP.Text);
            Param_sms.IP = ip;
            Param_sms.Password = txt_password.Text;
            Param_sms.IP_Port = Convert.ToUInt16(txt_IP_port.Text);
            Param_sms.Password_FLAG0 = ckb_password.Checked;
            Param_sms.IP_and_Port_FLAG3 = ckb_IP.Checked;
            Param_sms.TCP_UDP_FLAG2 = ckb_TCP.Checked;
            Param_sms.Use_Backup_IP_FLAG4 = ckb_backup.Checked;
            Param_sms.TCP_UDP = (byte)cbx_TCP.SelectedIndex;
            Param_sms.HDLC_TCP = (byte)cbx_HDLC.SelectedIndex;
            return Param_sms;
        }

        private void PopulateGUI_SMSParams(SMS_Params SmsParam)
        {
            txt_IP.Text = SmsParam.IP.ToString();
            txt_password.Text = SmsParam.Password;
            txt_IP_port.Text = SmsParam.IP_Port.ToString();
            cbx_TCP.SelectedIndex = SmsParam.TCP_UDP;
            cbx_HDLC.SelectedIndex = SmsParam.HDLC_TCP;
            ckb_password.Checked = false;   ///Default Load
            ckb_IP.Checked  = false;
            ckb_TCP.Checked = false;
            ckb_backup.Checked = false;
            ckb_password.Checked = SmsParam.Password_FLAG0;
            ckb_IP.Checked = SmsParam.IP_and_Port_FLAG3; 
            ckb_TCP.Checked  = SmsParam.TCP_UDP_FLAG2;
            ckb_backup.Checked = SmsParam.Use_Backup_IP_FLAG4;
            
        }

        private void PopulateGUI_TCPParams(InitTCPParams TCPParams)
        {
            txt_ServerPort.Text = TCPParams.ServerPort.ToString();
            IPAddress[] localServerIps = Dns.GetHostAddresses(Dns.GetHostName());
            cmbServerIP.Items.Clear();
            foreach (var item in localServerIps)
            {
                if (item.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork &&
                    !IPAddress.IsLoopback(item)
                    )
                {
                    cmbServerIP.Items.Add(item);
                }
            }
            cmbServerIP.Items.Add(IPAddress.Any);
            cmbServerIP.SelectedItem = TCPParams.ServerIP;
            tcpIPTimeout.Value = tcpIPTimeout.Value.Subtract(tcpIPTimeout.Value.TimeOfDay);
            tcpIPTimeout.Value = tcpIPTimeout.Value.Add(TCPParams.TCPIPTimeOut);
            chk_AvoidTCP_TimeOut.Checked = TCPParams.IsTCPTimeOut;
        }

        private InitTCPParams SaveGUI_TCPParams()
        {
            InitTCPParams _InitTCPParams = new InitTCPParams();
            _InitTCPParams.ServerPort = int.Parse(txt_ServerPort.Text);
            _InitTCPParams.ServerIP = (IPAddress)cmbServerIP.SelectedItem;
            _InitTCPParams.TCPIPTimeOut = tcpIPTimeout.Value.TimeOfDay;
            _InitTCPParams.IsTCPTimeOut = chk_AvoidTCP_TimeOut.Checked;
            return _InitTCPParams;
        }

        private void PopulateGUI_CommuincationParams(InitCommuincationParams CommuincationParams)
        {
            txt_IRCOMPort.Text = CommuincationParams.IRCOMPort;
            cmbBaudRate.Text = CommuincationParams.IRCOMBaudRate.ToString();
            txt_ModemCOMPort.Text = CommuincationParams.ModemCOMPort;
            txt_ModemBaudRate.Text = CommuincationParams.ModemBaudRate.ToString();
            txt_MobNumber.Text = CommuincationParams.MobileNumber.ToString();
            
            txt_DataReadTimeout.Value = txt_DataReadTimeout.Value.Subtract(txt_DataReadTimeout.Value.TimeOfDay);
            txt_DataReadTimeout.Value = txt_DataReadTimeout.Value.Add(CommuincationParams.DataReadTimeout);

            txt_TCPInConnectionTimeout.Value = txt_TCPInConnectionTimeout.Value.Subtract(txt_TCPInConnectionTimeout.Value.TimeOfDay);
            txt_TCPInConnectionTimeout.Value = txt_TCPInConnectionTimeout.MinDate.Add(CommuincationParams.TCPInConnectionTimeout);
        }

        private InitCommuincationParams SaveGUI_CommunicationParams()
        {
            InitCommuincationParams _InitCommParams = new InitCommuincationParams();
            _InitCommParams.IRCOMPort = txt_IRCOMPort.Text;
            _InitCommParams.IRCOMBaudRate = Int32.Parse(cmbBaudRate.Text);
            _InitCommParams.ModemCOMPort = txt_ModemCOMPort.Text;
            _InitCommParams.ModemBaudRate = int.Parse(txt_ModemBaudRate.Text);
            _InitCommParams.MobileNumber = txt_MobNumber.Text;
            _InitCommParams.DataReadTimeout = txt_DataReadTimeout.Value.TimeOfDay;
            _InitCommParams.TCPInConnectionTimeout = txt_TCPInConnectionTimeout.Value.TimeOfDay;
            return _InitCommParams;
        }

       
        private void Get_COMM_Ports()
        {
            string[] IR_ports = SerialPort.GetPortNames();
            for (int i = 0; i < IR_ports.Length; i++)
            {
                txt_IRCOMPort.Items.Add(IR_ports[i]);
            }

            string[] Modem_ports = SerialPort.GetPortNames();
            for (int i = 0; i < Modem_ports.Length; i++)
            {
                txt_ModemCOMPort.Items.Add(Modem_ports[i]);
            }
           
        }

        private void btn_Configuration_Save_Click(object sender, EventArgs e)
        {
            try
            {
                //if (cmd_HDLC_Mode1.Checked)
                //{
                //    _initHdlcParams = SaveGUI_HDLCParams();
                //    InitParam.SaveHDLCParams(_initHdlcParams, false);
                //}
                //else if (cmd_HDLC_Mode2.Checked)
                //{
                //    _initDirectHdlcParams = SaveGUI_HDLCParams();
                //    InitParam.SaveHDLCParams(_initDirectHdlcParams, true);
                //}

                _initHdlcParams = SaveGUI_HDLCParams();
                _initTCPParams = SaveGUI_TCPParams();
                _initCommuincationParams = SaveGUI_CommunicationParams();
                _initSMSParams = SaveGUI_SMSParams();
                InitParam.SaveCommuincationParams(_initCommuincationParams);
                InitParam.SaveHDLCParams(_initHdlcParams);
                InitParam.SaveTCPParams(_initTCPParams);
                InitParam.SaveWakeupSmsParams(_initSMSParams);
                MessageBox.Show("Saved Successfully");
            }
            catch
            {
                MessageBox.Show("Saved Un-Successfully");
            }
        }

        private void btn_Configuration_Load_Default_Click(object sender, EventArgs e)
        {
            PopulateGUI_CommuincationParams(_initCommuincationParams = InitParam.GetDefaultCommunicationParams());
            PopulateGUI_HDLCParams(_initHdlcParams = InitParam.GetDefaultHDLCParams());
            PopulateGUI_TCPParams(_initTCPParams = InitParam.GetDefaultTCPParams());
            PopulateGUI_SMSParams(_initSMSParams = InitParam.GetDefaultWakeUpSmsParams());
        }


        private void ckb_IP_CheckedChanged(object sender, EventArgs e)
        {
            txt_IP.Enabled = ckb_IP.Checked;
            txt_IP_port.Enabled = ckb_IP.Checked;
        }

        private void ckb_HDLC_CheckedChanged(object sender, EventArgs e)
        {
            cbx_HDLC.Enabled = ckb_HDLC.Checked;
        }

        private void ckb_password_CheckedChanged(object sender, EventArgs e)
        {
            txt_password.Enabled = ckb_password.Checked;
        }

        private void ckb_TCP_CheckedChanged(object sender, EventArgs e)
        {
            cbx_TCP.Enabled = ckb_TCP.Checked;
        }

        private void cmd_HDLC_Mode1_CheckedChanged(object sender, EventArgs e)
        {
            //if (cmd_HDLC_Mode1.Checked)
            //    PopulateGUI_HDLCParams(_initHdlcParams);
            //else if (cmd_HDLC_Mode2.Checked)
            //    PopulateGUI_HDLCParams(_initDirectHdlcParams);
        }
    }
}
