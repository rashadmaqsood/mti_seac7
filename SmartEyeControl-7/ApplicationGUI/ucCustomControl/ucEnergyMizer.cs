using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Microsoft.ReportingServices.Interfaces;
using DLMS;
using SharedCode.Controllers;
using ComponentFactory.Krypton.Toolkit;
using SEAC.Common;
using SharedCode.Comm.DataContainer;
using System.Net;
using SharedCode.Comm.Param;
using SmartEyeControl_7.ApplicationGUI.ucCustomControl;
using SharedCode.Comm.HelperClasses;

namespace AccurateOptocomSoftware.ApplicationGUI.ucCustomControl
{
    public partial class ucEnergyMizer : UserControl
    {
        private DLMS_Application_Process Application_Process = null;
        private ApplicationController application_Controller;
        private ParameterController Param_Controller = null;

        
        public ApplicationController Application_Controller
        {
            get
            {
                return application_Controller;
            }
            set
            {
                if (value != application_Controller)
                {
                    application_Controller = value;
                    Param_Controller = application_Controller.Param_Controller;
                    Application_Process = application_Controller.Applicationprocess_Controller.ApplicationProcess;
                    //Application_Controller.PropertyChanged += new PropertyChangedEventHandler(Application_Controller_PropertyChanged);
                }
            }
        }

        public ucEnergyMizer()
        {
            InitializeComponent();
            InitComboBoxesWithEnums();
        }

        
        private void InitComboBoxesWithEnums()
        {
            #region -- EnergyMizer Dynamic Data From Enums --

            foreach (var item in Enum.GetNames(typeof(PacketMode)))
                this.cmbPacketMode.Items.Add(item);

            foreach (var item in Enum.GetNames(typeof(ModulationType)))
                this.cmbModulationType.Items.Add(item);

            foreach (var item in Enum.GetNames(typeof(USB_Parameter)))
                this.cmbUSBParams.Items.Add(item);
            
            for (short i = -18; i <= 13; i++)
                this.cmbRFPower.Items.Add(i.ToString("00"));

            foreach (var item in Enum.GetNames(typeof(WiFi_Mode)))
                this.cmbWiFiMode.Items.Add(item);

            foreach (var item in Enum.GetNames(typeof(WiFi_Modem_Mode)))
                this.cmbWiFiModemMode.Items.Add(item);

            foreach (var item in Enum.GetNames(typeof(PacketFormat)))
                this.cmbPacketFormat.Items.Add(item);

            foreach (var item in Enum.GetNames(typeof(Temperature_Settings)))
                this.cmbTemperatureSettings.Items.Add(item);

            foreach (var item in Enum.GetNames(typeof(Disable_Enable))) // Adding Disable/Enable
            {
                this.cmbPacketEncoding.Items.Add(item);
                this.cmbDataWhitening.Items.Add(item);
                this.cmbAddressFiltering.Items.Add(item);
                this.cmbNodeAddress.Items.Add(item);
                this.cmbBrodcastAddress.Items.Add(item);
                this.cmbAESEncryption.Items.Add(item);
                this.cmbBuzzerSettings.Items.Add(item);
                this.cmbReadHumidity.Items.Add(item);
                this.cmbWiFiDhcp.Items.Add(item);
            }

            #endregion // End -- EnergyMizer Dynamic Data From Enums --

            this.cmbPacketMode.SelectedIndex = (this.cmbPacketMode.Items.Count > 0) ? 0 : -1;
            this.cmbModulationType.SelectedIndex = (this.cmbModulationType.Items.Count > 0) ? 0 : -1;
            this.cmbUSBParams.SelectedIndex = (this.cmbUSBParams.Items.Count > 0) ? 0 : -1;
            this.cmbPacketEncoding.SelectedIndex = (this.cmbPacketEncoding.Items.Count > 0) ? 0 : -1;
            this.cmbRFPower.SelectedIndex = (this.cmbRFPower.Items.Count > 0) ? 0 : -1;
            this.cmbAddressFiltering.SelectedIndex = (this.cmbAddressFiltering.Items.Count > 0) ? 0 : -1;
            this.cmbNodeAddress.SelectedIndex = (this.cmbNodeAddress.Items.Count > 0) ? 0 : -1;
            this.cmbBrodcastAddress.SelectedIndex = (this.cmbBrodcastAddress.Items.Count > 0) ? 0 : -1;
            this.cmbAESEncryption.SelectedIndex = (this.cmbAESEncryption.Items.Count > 0) ? 0 : -1;
            this.cmbBuzzerSettings.SelectedIndex = (this.cmbBuzzerSettings.Items.Count > 0) ? 0 : -1;
            this.cmbReadHumidity.SelectedIndex = (this.cmbReadHumidity.Items.Count > 0) ? 0 : -1;
            this.cmbTemperatureSettings.SelectedIndex = (this.cmbTemperatureSettings.Items.Count > 0) ? 0 : -1;

            this.cmbWiFiMode.SelectedIndex = (this.cmbWiFiMode.Items.Count > 0) ? 0 : -1;
            this.cmbWiFiModemMode.SelectedIndex = (this.cmbWiFiModemMode.Items.Count > 0) ? 0 : -1;
            this.cmbWiFiDhcp.SelectedIndex = (this.cmbWiFiDhcp.Items.Count > 0) ? 0 : -1;
            this.cmbPacketFormat.SelectedIndex = (this.cmbPacketFormat.Items.Count > 0) ? 0 : -1;
            this.cmbDataWhitening.SelectedIndex = (this.cmbDataWhitening.Items.Count > 0) ? 0 : -1;

        }

        #region ___ Energy Mizer Configuration ___

        #region -- RF Configuration Controls --

        #region --01 RF_Channels 

        private void btnRFChannelsSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_RFChannel(Convert.ToUInt32(txtRFChannels.Text));
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "RFChannelsSet Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting RFChannelsSet, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnRFChannelsGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    uint ch = Application_Controller.Param_Controller.GET_Param_RFChannel();
                    txtRFChannels.Text = ch.ToString();
                    notification = new Notification("Success", "GET_Param_RFChannel Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_RFChannel Read Error," + Environment.NewLine + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void txtRFChannels_TextChanged(object sender, EventArgs e)
        {
            if (LocalCommon.TextBox_validation(0, 100, txtRFChannels))
                this.btnRFChannelsSet.Enabled = true;
            else
                this.btnRFChannelsSet.Enabled = false;
        }

        #endregion  // --01 RF_Channels

        #region --02 Channel_Filter_BW 

        private void btnChannelFilterBWSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    ulong inKiloUnit = Convert.ToUInt64( Convert.ToDouble(txtChannelFilterBW.Text) * 1000 );
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Channel_Filter_BW(inKiloUnit);
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_Channel_Filter_BW Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_Channel_Filter_BW, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnChannelFilterBWGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    ulong ch = Application_Controller.Param_Controller.GET_Param_Channel_Filter_BW();
                    double inKiloUnit = (double)ch / 1000;  // Bcz of Kilo unit. 10^3 (1,000)
                    txtChannelFilterBW.Text = inKiloUnit.ToString();
                    notification = new Notification("Success", "GET_Param_Channel_Filter_BW Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_Channel_Filter_BW Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void txtChannelFilterBW_TextChanged(object sender, EventArgs e)
        {
            if (LocalCommon.TextBox_validation(2.6, 500, txtChannelFilterBW))
            {
                this.btnChannelFilterBWSet.Enabled = true;
            }
            else
            {
                this.btnChannelFilterBWSet.Enabled = false;
            }
        }

        #endregion  // --02 Channel_Filter_BW

        #region --03 Transmit_Carrier_Frequency 

        private void btnTransCarrierFreqSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    ulong unitInMega = Convert.ToUInt64( Convert.ToDouble(txtTransCarrierFreq.Text) * 1000000 );
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Transmit_Carrier_Frequency(unitInMega);
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_Transmit_Carrier_Frequency Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_Transmit_Carrier_Frequency, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnTransCarrierFreqGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    ulong ch = Application_Controller.Param_Controller.GET_Param_Transmit_Carrier_Frequency();
                    double inMegaUnit = (double)ch / 1000000;  // Bcz of Mega unit. 10^6 (1,000,000)
                    txtTransCarrierFreq.Text = inMegaUnit.ToString();
                    notification = new Notification("Success", "GET_Param_Transmit_Carrier_Frequency Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_Transmit_Carrier_Frequency Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void txtTransCarrierFreq_TextChanged(object sender, EventArgs e)
        {
            if (LocalCommon.TextBox_validation(424, 510, txtTransCarrierFreq))
                this.btnTransCarrierFreqSet.Enabled = true;
            else
                this.btnTransCarrierFreqSet.Enabled = false;
        }

        #endregion  // --03 Transmit_Carrier_Frequency 

        #region --04 Receive_Carrier_Frequency 

        private void btnReceiveCarrierFreqSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    ulong unitInMega = Convert.ToUInt64( Convert.ToDouble(txtReceiveCarrierFreq.Text) * 1000000 );
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Receive_Carrier_Frequency(unitInMega);
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_Receive_Carrier_Frequency Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_Receive_Carrier_Frequency, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnReceiveCarrierFreqGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    ulong ch = Application_Controller.Param_Controller.GET_Param_Receive_Carrier_Frequency();
                    double inMegaUnit = (double)ch / 1000000;  // Bcz of Mega unit. 10^6 (1,000,000)
                    txtReceiveCarrierFreq.Text = inMegaUnit.ToString();
                    notification = new Notification("Success", "GET_Param_Receive_Carrier_Frequency Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_Receive_Carrier_Frequency Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void txtReceiveCarrierFreq_TextChanged(object sender, EventArgs e)
        {
            if (LocalCommon.TextBox_validation(424, 510, txtReceiveCarrierFreq))
                this.btnReceiveCarrierFreqSet.Enabled = true;
            else
                this.btnReceiveCarrierFreqSet.Enabled = false;
        }

        #endregion  // --04 Receive_Carrier_Frequency 

        #region --05 RF_Baud_Rate

        private void btnRFBaudRateSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    uint unitInKilo = Convert.ToUInt32( Convert.ToDouble(txtRFBaudRate.Text) * 1000 );
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_RF_Baud_Rate(unitInKilo);
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_RF_Baud_Rate Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_RF_Baud_Rate, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnRFBaudRateGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    uint ch = Application_Controller.Param_Controller.GET_Param_RF_Baud_Rate();
                    double inKiloUnit = (double) ch / 1000;  // Bcz of Kilo unit. 10^3 (1,000)
                    txtRFBaudRate.Text = inKiloUnit.ToString();
                    notification = new Notification("Success", "GET_Param_RF_Baud_Rate Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_RF_Baud_Rate Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void txtRFBaudRate_TextChanged(object sender, EventArgs e)
        {
            if (LocalCommon.TextBox_validation(1.2, 300, txtRFBaudRate))
                this.btnRFBaudRateSet.Enabled = true;
            else
                this.btnRFBaudRateSet.Enabled = false;
        }

        #endregion  // --05 RF_Baud_Rate

        #region --06 RF_Power

        private void btnRFPowerSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_RF_Power(cmbRFPower.SelectedIndex);
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_RF_Power Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_RF_Power, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnRFPowerGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    int ch = Application_Controller.Param_Controller.GET_Param_RF_Power();
                    cmbRFPower.SelectedIndex = ch;
                    notification = new Notification("Success", "GET_Param_RF_Power Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_RF_Power Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        #endregion  // --06 RF_Power

        #region --07 Packet_Mode 

        private void btnPacketModeSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Packet_Mode(Convert.ToByte(cmbPacketMode.SelectedIndex));
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_Packet_Mode Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_Packet_Mode, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnPacketModeGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    PacketMode ch = Application_Controller.Param_Controller.GET_Param_Packet_Mode();
                    cmbPacketMode.SelectedIndex = (byte)ch;
                    notification = new Notification("Success", "GET_Param_Packet_Mode Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_Packet_Mode Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }
        
        #endregion  // --07 Packet_Mode 

        #region --08 Frequency_Deviation 

        private void btnFrequencyDeviationSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    uint unitInKilo = Convert.ToUInt32(Convert.ToDouble(txtFrequencyDeviation.Text) * 1000 );
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Frequency_Deviation(unitInKilo);
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_Frequency_Deviation Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_Frequency_Deviation, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnFrequencyDeviationGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    uint ch = Application_Controller.Param_Controller.GET_Param_Frequency_Deviation();
                    double inKiloUnit = (double) ch / 1000;  // Bcz of Kilo unit. 10^3 (1,000)
                    txtFrequencyDeviation.Text = inKiloUnit.ToString();
                    notification = new Notification("Success", "GET_Param_Frequency_Deviation Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_Frequency_Deviation Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void txtFrequencyDeviation_TextChanged(object sender, EventArgs e)
        {
            if (LocalCommon.TextBox_validation(0.6, 300, txtFrequencyDeviation))
                this.btnFrequencyDeviationSet.Enabled = true;
            else
                this.btnFrequencyDeviationSet.Enabled = false;
        }

        #endregion  // --08 Frequency_Deviation 

        #region --09 Receiver_Bandwidth 

        private void btnReceiverBandwidthSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    ulong unitInKilo = Convert.ToUInt64( Convert.ToDouble(txtReceiverBandwidth.Text) * 1000 );
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Receiver_Bandwidth(unitInKilo);
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_Receiver_Bandwidth Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_Receiver_Bandwidth, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnReceiverBandwidthGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    ulong ch = Application_Controller.Param_Controller.GET_Param_Receiver_Bandwidth();
                    double InUnitKilo = (double) ch / 1000;  // Bcz of Kilo unit. 10^3 (1,000)
                    txtReceiverBandwidth.Text = InUnitKilo.ToString();
                    notification = new Notification("Success", "GET_Param_Receiver_Bandwidth Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_Receiver_Bandwidth Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void txtReceiverBandwidth_TextChanged(object sender, EventArgs e)
        {
            //???  receiver Bandwidth? *** Bit Rate < 2x RxBw ***
            //if (LocalCommon.TextBox_validation(1, 50, txtReceiverBandwidth))
            //    this.btnReceiverBandwidthSet.Enabled = true;
            //else
            //    this.btnReceiverBandwidthSet.Enabled = false;
        }

        #endregion  // --09 Receiver_Bandwidth 

        #region --10 Preamble 

        private void btnPreambleSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    ushort valueInShort = Convert.ToUInt16(txtPreamble.Text, 16);
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Preamble(valueInShort);
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_Preamble Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_Preamble, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnPreambleGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    ushort ch = Application_Controller.Param_Controller.GET_Param_Preamble();
                    txtPreamble.Text = ch.ToString("X");
                    notification = new Notification("Success", "GET_Param_Preamble Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_Preamble Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void txtPreamble_TextChanged(object sender, EventArgs e)
        {
            if (LocalCommon.isValidHexString(txtPreamble))
                this.btnPreambleSet.Enabled = true;
            else
                this.btnPreambleSet.Enabled = false;
        }

        #endregion  // --10 Preamble 

        #region --11 Sync_Word 

        private void btnSyncWordSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    uint valueInInt = Convert.ToUInt32(txtSyncWord.Text, 16);
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_SyncWord( valueInInt );
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_SyncWord Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_SyncWord, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnSyncWordGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    uint ch = Application_Controller.Param_Controller.GET_Param_SyncWord();
                    txtSyncWord.Text = ch.ToString("X");
                    notification = new Notification("Success", "GET_Param_SyncWord Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_SyncWord Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void txtSyncWord_TextChanged(object sender, EventArgs e)
        {
            if (LocalCommon.isValidHexString(txtSyncWord))
                this.btnSyncWordSet.Enabled = true;
            else
                this.btnSyncWordSet.Enabled = false;
        }

        #endregion  // --11 Sync_Word 

        #region --12 Address_Filtering 

        private void btnAddressFilteringSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Address_Filtering(Convert.ToByte(cmbAddressFiltering.SelectedIndex));
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_Address_Filtering Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_Address_Filtering, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnAddressFilteringGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Disable_Enable ch = Application_Controller.Param_Controller.GET_Param_Address_Filtering();
                    cmbAddressFiltering.SelectedIndex = (byte)ch; // (ch) ? 1 : 0; // Enable, if true. Else, Disable...
                    notification = new Notification("Success", "GET_Param_Address_Filtering Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_Address_Filtering Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        #endregion  // --12 Address_Filtering 

        #region --13 Node_Address 

        private void btnNodeAddressSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Node_Address(Convert.ToUInt32(cmbNodeAddress.SelectedIndex));
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_Node_Address Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_Node_Address, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnNodeAddressGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    uint ch = Application_Controller.Param_Controller.GET_Param_Node_Address();
                    cmbNodeAddress.SelectedIndex = Convert.ToInt32(ch) > 0 ? 1 : 0; // Enable, if true. Else, Disable...
                    notification = new Notification("Success", "GET_Param_Node_Address Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_Node_Address Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        #endregion  // --13 Node_Address 

        #region --14 Broadcast_Address

        private void btnBroadcastAddressSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Broadcast_Address(Convert.ToUInt32(cmbBrodcastAddress.SelectedIndex));
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_Broadcast_Address Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_Broadcast_Address, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnBroadcastAddressGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    uint ch = Application_Controller.Param_Controller.GET_Param_Broadcast_Address();
                    cmbBrodcastAddress.SelectedIndex = Convert.ToInt32(ch) > 0 ? 1 : 0; // Enable, if true. Else, Disable...
                    notification = new Notification("Success", "GET_Param_Broadcast_Address Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_Broadcast_Address Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        #endregion  // --14 Broadcast_Address

        #region --15 AES_Encryption 

        private void btnAESEncryptionSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_AES_Encryption(Convert.ToByte(cmbAESEncryption.SelectedIndex));
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_AES_Encryption Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_AES_Encryption, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnAESEncryptionGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Disable_Enable ch = Application_Controller.Param_Controller.GET_Param_AES_Encryption();
                    cmbAESEncryption.SelectedIndex = (byte)ch; // (ch) ? 1 : 0; // Enable, if true. Else, Disable...
                    notification = new Notification("Success", "GET_Param_AES_Encryption Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_AES_Encryption Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        #endregion  // --15 AES_Encryption 

        #region --16 RF_Command_Delay 

        private void btnRFCommandDelaySet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_RF_Command_Delay(Convert.ToUInt16(txtRFCommandDelay.Text));
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_RF_Command_Delay Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_RF_Command_Delay, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnRFCommandDelayGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    ushort ch = Application_Controller.Param_Controller.GET_Param_RF_Command_Delay();
                    txtRFCommandDelay.Text = ch.ToString();
                    notification = new Notification("Success", "GET_Param_RF_Command_Delay Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_RF_Command_Delay Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void txtRFCommandDelay_TextChanged(object sender, EventArgs e)
        {
            if (LocalCommon.TextBox_validation(7, 240, txtRFCommandDelay))
            {
                this.btnRFCommandDelaySet.Enabled = true;
            }
            else
            {
                this.btnRFCommandDelaySet.Enabled = false;
            }
        }

        #endregion  // --16 RF_Command_Delay 

        #region --17 RF_Command_Timeout

        private void btnRFCmdTimeoutSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_RF_Command_Timeout(Convert.ToUInt16(txtRFCmdTimeout.Text));
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_RF_Command_Timeout Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_RF_Command_Timeout, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnRFCmdTimeoutGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    ushort ch = Application_Controller.Param_Controller.GET_Param_RF_Command_Timeout();
                    txtRFCmdTimeout.Text = ch.ToString();
                    notification = new Notification("Success", "GET_Param_RF_Command_Timeout Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_RF_Command_Timeout Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void txtRFCmdTimeout_TextChanged(object sender, EventArgs e)
        {
            if (LocalCommon.TextBox_validation(10, 255, txtRFCmdTimeout))
                this.btnRFCmdTimeoutSet.Enabled = true;
            else
                this.btnRFCmdTimeoutSet.Enabled = false;
        }

        #endregion  // --17 RF_Command_Timeout

        #region --18 Modulation_Type

        private void btnModulationTypeSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Modulation_Type(Convert.ToByte(cmbModulationType.SelectedIndex));
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_Modulation_Type Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_Modulation_Type, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnModulationTypeGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    ModulationType ch = Application_Controller.Param_Controller.GET_Param_Modulation_Type();
                    cmbModulationType.SelectedIndex = (byte)ch;
                    notification = new Notification("Success", "GET_Param_Modulation_Type Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_Modulation_Type Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        #endregion  // --18 Modulation_Type

        #region --19 Packet_Encoding 

        private void btnPacketEncodingSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Packet_Encoding(Convert.ToByte(cmbPacketEncoding.SelectedIndex));
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_Packet_Encoding Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_Packet_Encoding, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnPacketEncodingGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Disable_Enable ch = Application_Controller.Param_Controller.GET_Param_Packet_Encoding();
                    cmbPacketEncoding.SelectedIndex = (byte)ch;  // (ch) ? 1 : 0; // Enable, if true. Else False.  //Whitening, if true. Else Manchester,
                    notification = new Notification("Success", "GET_Param_Packet_Encoding Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_Packet_Encoding Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        #endregion  // --19 Packet_Encoding 
        
        #region --20 Packet_Format

        private void btnPacketFormatGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    PacketFormat ch = Application_Controller.Param_Controller.GET_Param_Packet_format();
                    cmbPacketFormat.SelectedIndex = (byte)ch; // (ch) ? 1 : 0; // Enable, if true. Else, Disable...
                    notification = new Notification("Success", "GET_Param_Packet_format Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_Packet_format Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        #endregion  // --20 Packet_Format

        #region --21 Packet_Length

        private void btnPacketLengthSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Packet_length(Convert.ToUInt16(txtPacketLength.Text));
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_Packet_length Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_Packet_length, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnPacketLengthGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    ushort ch = Application_Controller.Param_Controller.GET_Param_Packet_length();
                    txtPacketLength.Text = ch.ToString();
                    notification = new Notification("Success", "GET_Param_Packet_length Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_Packet_length Read Error," + Environment.NewLine + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void txtPacketLength_TextChanged(object sender, EventArgs e)
        {
            if (LocalCommon.TextBox_validation(0, 255, txtPacketLength))
                this.btnPacketLengthSet.Enabled = true;
            else
                this.btnPacketLengthSet.Enabled = false;
        }

        #endregion  // --21 Packet_Length
        
        #region --22 Data_Whitening

        private void btnDataWhiteningSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Data_Whitening(Convert.ToBoolean(cmbDataWhitening.SelectedIndex));
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_Data_Whitening Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_Data_Whitening, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnDataWhiteningGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    bool ch = Application_Controller.Param_Controller.GET_Param_Data_Whitening();
                    cmbDataWhitening.SelectedIndex = (ch) ? 1 : 0; // Enable, if true. Else False.  //Whitening, if true. Else Manchester,
                    notification = new Notification("Success", "GET_Param_Data_Whitening Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_Data_Whitening Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        #endregion  // --22 Data_Whitening

        #region --23 AES_Encryption_Key_Size

        private void btnAESEncryptionKeySizeSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;

                    byte[] key = DLMS_Common.String_to_Hex_array(txtAESEncryptionKeySize.Text);

                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_AES_Encryption_Key_size(key);
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_AES_Encryption_Key_size Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_AES_Encryption_Key_size, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnAESEncryptionKeySizeGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    byte[] ch = Application_Controller.Param_Controller.GET_Param_AES_Encryption_Key_size();
                    txtAESEncryptionKeySize.Text = DLMS_Common.ArrayToHexString(ch);
                    notification = new Notification("Success", "GET_Param_AES_Encryption_Key_size Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_AES_Encryption_Key_size Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void txtAESEncryptionKeySize_TextChanged(object sender, EventArgs e)
        {
            if (LocalCommon.isValidHexString(txtAESEncryptionKeySize))
                this.btnAESEncryptionKeySizeSet.Enabled = true;
            else
                this.btnAESEncryptionKeySizeSet.Enabled = false;
        }

        #endregion  // --23 AES_Encryption_Key_Size


        #endregion // RF Configuration Controls

        #region -- Display Configuration Controls --

        #region --1 Serial Number

        private void btnSerialNumberSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;

                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Serial_Number(Convert.ToUInt32(txtSerialNumber.Text));
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_Serial_Number Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_Serial_Number, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnSerialNumberGet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notifier = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    MeterSerialNumber meterSerialNumber = application_Controller.ConnectionController.GetMeterSerialNumber();
                    txtSerialNumber.Text = meterSerialNumber.ToString();
                    notifier = new Notification("Success", "GET_Param_Serial_Number Read Successful");
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "GET_Param_Serial_Number Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void txtSerialNumber_TextChanged(object sender, EventArgs e)
        {
            if ((LocalCommon.isValidStringNumbers(10, txtSerialNumber))
                && (LocalCommon.TextBox_validation(0, 4294967295, txtSerialNumber)))
                this.btnSerialNumberSet.Enabled = true;
            else
                this.btnSerialNumberSet.Enabled = false;
        }

        #endregion  // --1 Serial Number

        #region --2 LCD_Contrast 

        private void btnLCDContrastSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_LCD_Contrast(Convert.ToByte(txtLCDContrast.Text));
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_LCD_Contrast Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_LCD_Contrast, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnLCDContrastGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    byte ch = Application_Controller.Param_Controller.GET_Param_LCD_Contrast();
                    txtLCDContrast.Text = ch.ToString();
                    notification = new Notification("Success", "GET_Param_LCD_Contrast Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_LCD_Contrast Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void txtLCDContrast_TextChanged(object sender, EventArgs e)
        {
            if (LocalCommon.TextBox_validation(10, 100, txtLCDContrast))
                this.btnLCDContrastSet.Enabled = true;
            else
                this.btnLCDContrastSet.Enabled = false;
        }


        #endregion  // --2 LCD_Contrast 

        #region --3 Meter_To_Read

        private void btnMeterToReadSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    byte type = Convert.ToByte(Convert.ToInt16(txtMeterToReadType.Text));
                    byte[] msn = DLMS_Common.PrintableStringToByteArray(txtMeterToRead.Text);

                    byte[] type_n_msn = DLMS_Common.Append_to_Start(msn, type); // adding meter type at startup

                    Application_Controller.IsIOBusy = true;
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Meter_To_Read(type_n_msn);
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_Meter_To_Read Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_Meter_To_Read, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnMeterToReadGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    byte[] ch = Application_Controller.Param_Controller.GET_Param_Meter_To_Read();

                    string mix = DLMS_Common.ArrayToPrintableString(ch);

                    txtMeterToReadType.Text = ch[0].ToString(); //Convert.ToInt16(mix.Substring(0, 1)).ToString();//, System.Globalization.NumberStyles.HexNumber).ToString(); 
                    txtMeterToRead.Text = mix.Substring(1, (mix.Length - 1));

                    notification = new Notification("Success", "GET_Param_Meter_To_Read Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_Meter_To_Read Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void txtMeterToRead_TextChanged(object sender, EventArgs e)
        {
            if (LocalCommon.isValidStringNumbers(11, txtMeterToRead))
                this.btnMeterToReadSet.Enabled = true;
            else
                this.btnMeterToReadSet.Enabled = false;
        }

        private void txtMeterToReadType_TextChanged(object sender, EventArgs e)
        {
            if (LocalCommon.TextBox_validation(1, 99, txtMeterToReadType))
                this.btnMeterToReadSet.Enabled = true;
            else
                this.btnMeterToReadSet.Enabled = false;
        }

        #endregion  // --3 Meter_To_Read

        #region --4 Meter_Password 

        private void btnMeterPasswordSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    byte[] password = DLMS_Common.PrintableStringToByteArray(txtMeterPassword.Text);
                    Application_Controller.IsIOBusy = true;
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Meter_Password(password);
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_Meter_Password Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_Meter_Password, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnMeterPasswordGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    byte[] ch = Application_Controller.Param_Controller.GET_Param_Meter_Password();
                    txtMeterPassword.Text = DLMS_Common.ArrayToPrintableString(ch);
                    notification = new Notification("Success", "GET_Param_Meter_Password Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_Meter_Password Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void txtMeterPassword_TextChanged(object sender, EventArgs e)
        {
            if (LocalCommon.isValidDisplayableString(txtMeterPassword))
                this.btnMeterPasswordSet.Enabled = true;
            else
                this.btnMeterPasswordSet.Enabled = false;
        }

        #endregion  // --4 Meter_Password 

        #region --5 Data_To_read 

        private void btnDataToReadSet_Click(object sender, EventArgs e)
        {
            //Notification notifier = null;
            //try
            //{
            //    if (!(Application_Process.Is_Association_Developed))
            //        MessageBox.Show("Create Association to 'Energy Mizer'");
            //    else
            //    {
            //        Application_Controller.IsIOBusy = true;
            //        Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Data_To_read(Convert.ToInt32(txtDataToRead.Text));
            //        if (result == Data_Access_Result.Success)
            //        notifier = new Notification("Success", "Set_Param_Data_To_read Successfully");
            //    else
            //    throw new Exception(result.ToString());
            //}
            //}
            //catch (Exception ex)
            //{
            //    notifier = new Notification("Error", "Error occurred while Setting Set_Param_Data_To_read, \n" + ex.Message);
            //}
            //finally
            //{
            //    Application_Controller.IsIOBusy = false;
            //}
        }

        private void btnDataToReadGet_Click(object sender, EventArgs e)
        {
            //Notification notification = null;
            //try
            //{
            //    if (!(Application_Process.Is_Association_Developed))
            //        notification = new Notification("Error", "Create Association to 'Energy Mizer'");
            //    else
            //    {
            //        Application_Controller.IsIOBusy = true;
            //        int ch = Application_Controller.Param_Controller.GET_Param_Data_To_read();
            //        txtDataToRead.Text = ch.ToString();
            //        notification = new Notification("Success", "GET_Param_Data_To_read Read Successful");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Notification("Error", "GET_Param_Data_To_read Read Error, \n" + ex.Message);
            //}
            //finally
            //{
            //    Application_Controller.IsIOBusy = false;
            //}
        }

        private void txtDataToRead_TextChanged(object sender, EventArgs e)
        {
            //if (LocalCommon.TextBox_validation(txtDataToRead))
            //    this.btnDataToReadSet.Enabled = true;
            //else
            //    this.btnDataToReadSet.Enabled = false;
        }

        #endregion  // --5 Data_To_read  

        #region --6 Buzzer_Settings

        private void btnBuzzerSettingsSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Buzzer_Settings(Convert.ToByte(cmbBuzzerSettings.SelectedIndex));
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_Buzzer_Settings Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_Buzzer_Settings, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnBuzzerSettingsGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Disable_Enable ch = Application_Controller.Param_Controller.GET_Param_Buzzer_Settings();
                    cmbBuzzerSettings.SelectedIndex = (byte) ch ;// (ch) ? 1 : 0; // Enable, if true. Else, Disable...
                    notification = new Notification("Success", "GET_Param_Buzzer_Settings Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_Buzzer_Settings Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        #endregion  // --6 Buzzer_Settings

        #region --7 Read_Humidity

        //private void btnReadHumiditySet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to 'Energy Mizer'");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Read_humidity_setting(Convert.ToBoolean(cmbReadHumidity.SelectedIndex));
        //            if (result == Data_Access_Result.Success)
        //                notifier = new Notification("Success", "Set_Param_Humidity Successfully");
        //            else
        //                throw new Exception(result.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "Error occurred while Setting Set_Param_Humidity, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        private void btnReadHumidityGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    bool ch = Application_Controller.Param_Controller.GET_Param_Read_humidity_setting();
                    cmbReadHumidity.SelectedIndex = (ch) ? 1 : 0; // Enable, if true. Else, Disable...
                    notification = new Notification("Success", "GET_Param_Humidity Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_Humidity Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        #endregion  // --7 Read_Humidity

        #region --8 Temperature_Settings 

        private void btnTempratureSettingsSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Temperature_setting(Convert.ToByte(cmbTemperatureSettings.SelectedIndex));
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_Temperature_setting Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_Temperature_setting, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnTempratureSettingsGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Temperature_Settings ch = Application_Controller.Param_Controller.GET_Param_Temperature_setting();
                    cmbTemperatureSettings.SelectedIndex = (byte) ch; // (ch) ? 1 : 0; // Enable, if true. Else, Disable...
                    notification = new Notification("Success", "GET_Param_Temperature_setting Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_Temperature_setting Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        #endregion  // --8 Temperature_Settings 

        #region --9 USB_Parameters 

        private void btnUSBParamsSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_USB_Parameters(Convert.ToByte(cmbUSBParams.SelectedIndex));
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_USB_Parameters Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_USB_Parameters, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnUSBParamsGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    USB_Parameter ch = Application_Controller.Param_Controller.GET_Param_USB_Parameters();
                    cmbUSBParams.SelectedIndex = (byte)ch;// (ch) ? 1 : 0; // Enable, if true. Else, Disable...
                    notification = new Notification("Success", "GET_Param_USB_Parameters Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_USB_Parameters Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        #endregion  // --9 USB_Parameters 

        #region --10 Cost_Parameters

        private void btnCostParametersSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Cost_Parameters( Convert.ToUInt32(Convert.ToDouble(txtCostParameters.Text) * 100) );
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_Cost_Parameters Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_Cost_Parameters, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnCostParametersGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    double ch = Application_Controller.Param_Controller.GET_Param_Cost_Parameters();
                    txtCostParameters.Text = ch.ToString();
                    notification = new Notification("Success", "GET_Param_Cost_Parameters Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_Cost_Parameters Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void txtCostParameters_TextChanged(object sender, EventArgs e)
        {
            if (LocalCommon.TextBox_validation(0.01, 100.00, txtCostParameters))
                this.btnCostParametersSet.Enabled = true;
            else
                this.btnCostParametersSet.Enabled = false;
        }

        #endregion  // --10 Cost_Parameters

        #region --11 Temperature (GET)

        private void btnTemperatureGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    uint ch = Application_Controller.Param_Controller.GET_Param_Temperature();
                    txtTemperature.Text = ch.ToString();
                    notification = new Notification("Success", "GET_Param_Temperature Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_Temperature Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        #endregion  // --11 Temperature (GET)


        #endregion // Display Configuration Controls

        #region -- WiFi Configurations Controls --

        #region --1 WiFi_Web_server_Configuration_IP

        private void btnWiFiWebServerConfigurationIPSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_WiFi_IP_client_TCP_Server_or_WEB_Server(IPAddress.Parse(txtWiFiWebServerConfigurationIP.Text));
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_WiFi_IP_client_TCP_Server_or_WEB_Server Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_WiFi_IP_client_TCP_Server_or_WEB_Server, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnWiFiWebServerConfigurationIPGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    IPAddress ch = Application_Controller.Param_Controller.GET_Param_WiFi_IP_client_TCP_Server_or_WEB_Server();
                    txtWiFiWebServerConfigurationIP.Text = ch.ToString();
                    notification = new Notification("Success", "GET_Param_WiFi_IP_client_TCP_Server_or_WEB_Server Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_WiFi_IP_client_TCP_Server_or_WEB_Server Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void txtWiFiWebServerConfigurationIP_TextChanged(object sender, EventArgs e)
        {
            if (LocalCommon.ValidateIPv4(txtWiFiWebServerConfigurationIP))
                this.btnWiFiWebServerConfigurationIPSet.Enabled = true;
            else
                this.btnWiFiWebServerConfigurationIPSet.Enabled = false;
        }

        #endregion  // WiFi_Web_server_Configuration_IP

        #region --2 WiFi_Web_server_Configuration_Port

        private void btnWiFiWebServerConfigurationPortSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_WiFi_Web_Server_Port(Convert.ToUInt32(txtWiFiWebServerConfigurationPort.Text));
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_WiFi_Web_Server_Port Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_WiFi_Web_Server_Port, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnWiFiWebServerConfigurationPortGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    uint ch = Application_Controller.Param_Controller.GET_Param_WiFi_Web_Server_Port();
                    txtWiFiWebServerConfigurationPort.Text = ch.ToString();
                    notification = new Notification("Success", "GET_Param_WiFi_Web_Server_Port Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_WiFi_Web_Server_Port Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void txtWiFiWebServerConfigurationPort_TextChanged(object sender, EventArgs e)
        {
            if (LocalCommon.TextBox_validation(1, 65535, txtWiFiWebServerConfigurationPort))
                this.btnWiFiWebServerConfigurationPortSet.Enabled = true;
            else
                this.btnWiFiWebServerConfigurationPortSet.Enabled = false;
        }

        #endregion  // WiFi_Web_server_Configuration_Port

        #region --3 WiFi_Server_IP

        private void btnWiFiServerIPSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_WiFi_IP_client_TCP_Server_or_WEB_Server(IPAddress.Parse(txtWiFiServerIP.Text));
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_WiFi_IP_client_TCP_Server_or_WEB_Server (WiFi Server IP) Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_WiFi_IP_client_TCP_Server_or_WEB_Server (WiFi Server IP), \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnWiFiServerIPGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    IPAddress ch = Application_Controller.Param_Controller.GET_Param_WiFi_IP_client_TCP_Server_or_WEB_Server();
                    txtWiFiServerIP.Text = ch.ToString();
                    notification = new Notification("Success", "GET_Param_WiFi_IP_client_TCP_Server_or_WEB_Server Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_WiFi_IP_client_TCP_Server_or_WEB_Server Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void txtWiFiServerIP_TextChanged(object sender, EventArgs e)
        {
            if (LocalCommon.ValidateIPv4(txtWiFiServerIP))
                this.btnWiFiServerIPSet.Enabled = true;
            else
                this.btnWiFiServerIPSet.Enabled = false;
        }

        #endregion  // WiFi_Server_IP

        #region --4 WiFi_Server_Port

        private void btnWiFiServerPortSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_WiFi_Server_Port(Convert.ToUInt16(txtWiFiServerPort.Text));
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_WiFi_Server_Port Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_WiFi_Server_Port, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnWiFiServerPortGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    ushort ch = Application_Controller.Param_Controller.GET_Param_WiFi_Server_Port();
                    txtWiFiServerPort.Text = ch.ToString();
                    notification = new Notification("Success", "GET_Param_WiFi_Server_Port Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_WiFi_Server_Port Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void txtWiFiServerPort_TextChanged(object sender, EventArgs e)
        {
            if (LocalCommon.TextBox_validation(1, 65535, txtWiFiServerPort))
                this.btnWiFiServerPortSet.Enabled = true;
            else
                this.btnWiFiServerPortSet.Enabled = false;
        }

        #endregion  // WiFi_Server_Port

        #region --5 WiFi_Basic_Configuration

        private void btnWiFiBasicConfigurationSet_Click(object sender, EventArgs e)
        {
            //Notification notifier = null;
            //try
            //{
            //    if (!(Application_Process.Is_Association_Developed))
            //        MessageBox.Show("Create Association to 'Energy Mizer'");
            //    else
            //    {
            //        Application_Controller.IsIOBusy = true;
            //        Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_(Convert.ToInt32(txt.Text));
            //        if (result == Data_Access_Result.Success)
            //          notifier = new Notification("Success", "NNNNNNNNNNNNNN Successfully");
            //    else
            //    throw new Exception(result.ToString());
            //}
            //}
            //catch (Exception ex)
            //{
            //    notifier = new Notification("Error", "Error occurred while Setting NNNNNNNNNNNNNN, \n" + ex.Message);
            //}
            //finally
            //{
            //    Application_Controller.IsIOBusy = false;
            //}
        }

        private void btnWiFiBasicConfigurationGet_Click(object sender, EventArgs e)
        {
            //Notification notification = null;
            //try
            //{
            //    if (!(Application_Process.Is_Association_Developed))
            //        notification = new Notification("Error", "Create Association to 'Energy Mizer'");
            //    else
            //    {
            //        Application_Controller.IsIOBusy = true;
            //        int ch = Application_Controller.Param_Controller.GET_Param_();
            //        txt.Text = ch.ToString();
            //        notification = new Notification("Success", "NNNNNNNNNNN Read Successful");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Notification("Error", "NNNNNNNNNNNNN Read Error, \n" + ex.Message);
            //}
            //finally
            //{
            //    Application_Controller.IsIOBusy = false;
            //}
        }

        private void txtWiFiBasicConfiguration_TextChanged(object sender, EventArgs e)
        {
            //if (LocalCommon.TextBox_validation(1, 65535, txtWiFiBasicConfiguration))
            //    this.btnWiFiBasicConfigurationSet.Enabled = true;
            //else
            //    this.btnWiFiBasicConfigurationSet.Enabled = false;
        }

        #endregion  // --5 WiFi_Basic_Configuration

        #region --6 WiFi_Client_IP

        private void btnWiFiClientIPSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_WiFi_IP_client_TCP_Server_or_WEB_Server(IPAddress.Parse(txtWiFiClientIP.Text));
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_WiFi_IP_client_TCP_Server_or_WEB_Server Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_WiFi_IP_client_TCP_Server_or_WEB_Server, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnWiFiClientIPGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    IPAddress ch = Application_Controller.Param_Controller.GET_Param_WiFi_IP_client_TCP_Server_or_WEB_Server();
                    txtWiFiClientIP.Text = ch.ToString();
                    notification = new Notification("Success", "GET_Param_WiFi_IP_client_TCP_Server_or_WEB_Server Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_WiFi_IP_client_TCP_Server_or_WEB_Server Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void txtWiFiClientIP_TextChanged(object sender, EventArgs e)
        {
            if (LocalCommon.ValidateIPv4(txtWiFiClientIP))
                this.btnWiFiClientIPSet.Enabled = true;
            else
                this.btnWiFiClientIPSet.Enabled = false;
        }

        #endregion  // WiFi_Client_IP

        #region --7 WiFi_Client_Port

        private void btnWiFiClientPortSet_Click(object sender, EventArgs e)
        {
            //Notification notifier = null;
            //try
            //{
            //    if (!(Application_Process.Is_Association_Developed))
            //        MessageBox.Show("Create Association to 'Energy Mizer'");
            //    else
            //    {
            //        Application_Controller.IsIOBusy = true;
            //        Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_wifi(Convert.ToInt32(txt.Text));
            //        if (result == Data_Access_Result.Success)
            //              notifier = new Notification("Success", "NNNNNNNNNNNNNN Successfully");
            //    else
            //    throw new Exception(result.ToString());
            //}
            //}
            //catch (Exception ex)
            //{
            //    notifier = new Notification("Error", "Error occurred while Setting NNNNNNNNNNNNNN, \n" + ex.Message);
            //}
            //finally
            //{
            //    Application_Controller.IsIOBusy = false;
            //}
        }

        private void btnWiFiClientPortGet_Click(object sender, EventArgs e)
        {
            //Notification notification = null;
            //try
            //{
            //    if (!(Application_Process.Is_Association_Developed))
            //        notification = new Notification("Error", "Create Association to 'Energy Mizer'");
            //    else
            //    {
            //        Application_Controller.IsIOBusy = true;
            //        int ch = Application_Controller.Param_Controller.GET_Param_();
            //        txt.Text = ch.ToString();
            //        notification = new Notification("Success", "NNNNNNNNNNN Read Successful");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Notification("Error", "NNNNNNNNNNNNN Read Error, \n" + ex.Message);
            //}
            //finally
            //{
            //    Application_Controller.IsIOBusy = false;
            //}
        }

        private void txtWiFiClientPort_TextChanged(object sender, EventArgs e)
        {
            //if (LocalCommon.TextBox_validation(1, 65535, txtWiFiClientPort))
            //    this.btnWiFiClientPortSet.Enabled = true;
            //else
            //    this.btnWiFiClientPortSet.Enabled = false;
        }

        #endregion  // --7 WiFi_Client_Port

        #region --8 WiFi_SSID

        private void btnWiFiSSIDSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;

                    byte[] ssid = DLMS_Common.PrintableStringToByteArray(txtWiFiSSID.Text);

                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Wifi_SSID(ssid);
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_Wifi_SSID Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_Wifi_SSID, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnWiFiSSIDGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    byte[] ch = Application_Controller.Param_Controller.GET_Param_Wifi_SSID();
                    txtWiFiSSID.Text = DLMS_Common.ArrayToPrintableString(ch);
                    notification = new Notification("Success", "GET_Param_Wifi_SSID Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_Wifi_SSID Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void txtWiFiSSID_TextChanged(object sender, EventArgs e)
        {
            if (LocalCommon.isValidDisplayableString(txtWiFiSSID))
                this.btnWiFiSSIDSet.Enabled = true;
            else
                this.btnWiFiSSIDSet.Enabled = false;
        }

        #endregion  // --8 WiFi_SSID

        #region --9 WiFi_Password

        private void btnWiFiPasswordSet_Click(object sender, EventArgs e)
        {
            //Notification notifier = null;
            //try
            //{
            //    if (!(Application_Process.Is_Association_Developed))
            //        MessageBox.Show("Create Association to 'Energy Mizer'");
            //    else
            //    {
            //        Application_Controller.IsIOBusy = true;
            //        Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_wifi(Convert.ToInt32(txt.Text));
            //        if (result == Data_Access_Result.Success)
            //              notifier = new Notification("Success", "NNNNNNNNNNNNNN Successfully");
            //    else
            //    throw new Exception(result.ToString());
            //}
            //}
            //catch (Exception ex)
            //{
            //    notifier = new Notification("Error", "Error occurred while Setting NNNNNNNNNNNNNN, \n" + ex.Message);
            //}
            //finally
            //{
            //    Application_Controller.IsIOBusy = false;
            //}
        }

        private void btnWiFiPasswordGet_Click(object sender, EventArgs e)
        {
            //Notification notification = null;
            //try
            //{
            //    if (!(Application_Process.Is_Association_Developed))
            //        notification = new Notification("Error", "Create Association to 'Energy Mizer'");
            //    else
            //    {
            //        Application_Controller.IsIOBusy = true;
            //        int ch = Application_Controller.Param_Controller.GET_Param_();
            //        txt.Text = ch.ToString();
            //        notification = new Notification("Success", "NNNNNNNNNNN Read Successful");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Notification("Error", "NNNNNNNNNNNNN Read Error, \n" + ex.Message);
            //}
            //finally
            //{
            //    Application_Controller.IsIOBusy = false;
            //}
        }

        private void txtWiFiPassword_TextChanged(object sender, EventArgs e)
        {
            if (LocalCommon.isValidDisplayableString(txtWiFiPassword))
                this.btnWiFiPasswordSet.Enabled = true;
            else
                this.btnWiFiPasswordSet.Enabled = false;
        }

        #endregion  // --9 WiFi_Password

        #region --10 WiFi_Mode

        private void btnWiFiModeSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Settings_Default_Wifi_Mode(Convert.ToByte(cmbWiFiMode.SelectedIndex));
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_Settings_Default_Wifi_Mode Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_Settings_Default_Wifi_Mode, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnWiFiModeGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    WiFi_Mode ch = Application_Controller.Param_Controller.GET_Param_Settings_Default_Wifi_Mode();
                    cmbWiFiMode.SelectedIndex = (byte)ch;// (ch) ? 1 : 0; // Enable, if true. Else, Disable...
                    notification = new Notification("Success", "GET_Param_Settings_Default_Wifi_Mode Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_Settings_Default_Wifi_Mode Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        #endregion  // --10 WiFi_Mode

        #region --11 WiFi_Modem_Mode

        private void btnWiFiModemModeSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Wifi_Modem_Mode(Convert.ToByte(cmbWiFiModemMode.SelectedIndex));
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_Wifi_Modem_Mode Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_Wifi_Modem_Mode, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnWiFiModemModeGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    WiFi_Modem_Mode ch = Application_Controller.Param_Controller.GET_Param_Wifi_Modem_Mode();
                    cmbWiFiModemMode.SelectedIndex = (byte)ch;// (ch) ? 1 : 0; // Enable, if true. Else, Disable...
                    notification = new Notification("Success", "GET_Param_Wifi_Modem_Mode Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_Wifi_Modem_Mode Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        #endregion  // --11 WiFi_Modem_Mode
        
        #region --12 WiFi_DHCP

        private void btnWiFiDhcpSet_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Settings_Wifi_DHCP(Convert.ToBoolean(cmbWiFiDhcp.SelectedIndex));
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_Settings_Wifi_DHCP Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_Settings_Wifi_DHCP, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnWiFiDhcpGet_Click(object sender, EventArgs e)
        {
            Notification notification = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notification = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    bool ch = Application_Controller.Param_Controller.GET_Param_Settings_Wifi_DHCP();
                    cmbWiFiDhcp.SelectedIndex = (ch) ? 1 : 0; // Enable, if true. Else, Disable...
                    notification = new Notification("Success", "GET_Param_Settings_Wifi_DHCP Read Successful");
                }
            }
            catch (Exception ex)
            {
                notification = new Notification("Error", "GET_Param_Settings_Wifi_DHCP Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        #endregion  // --12 WiFi_DHCP


        #endregion // -- WiFi Configurations Control --


        //TODO: LoadParamsFromFile  -> implement showToGui() funciton
        internal void ShowToGUI_EnergyMizerParams(Param_Energy_Mizer param_Energy_Mizer)
        {
            if (param_Energy_Mizer != null)
            {
                //TODO:??? Yahan GUI mein Show karo
                txtRFChannels.Text = param_Energy_Mizer.RFChannel.ToString();
                txtChannelFilterBW.Text = param_Energy_Mizer.ChannelFilterWB.ToString();
                txtTransCarrierFreq.Text = param_Energy_Mizer.TransmitCarrierFreq.ToString();
                txtReceiveCarrierFreq.Text = param_Energy_Mizer.ReceiveCarrierFreq.ToString();
                txtRFBaudRate.Text = param_Energy_Mizer.RFBaudRate.ToString();
                cmbRFPower.SelectedIndex = param_Energy_Mizer.RFPower;
                cmbPacketMode.SelectedIndex = param_Energy_Mizer.PacketMode;

                txtFrequencyDeviation.Text = param_Energy_Mizer.FrequencyDeviation.ToString();
                txtReceiverBandwidth.Text = param_Energy_Mizer.ReceiverBandwidth.ToString();
                txtCostParameters.Text = param_Energy_Mizer.CostParameters.ToString();

                txtPreamble.Text = param_Energy_Mizer.Preamble.ToString();
                txtSyncWord.Text = param_Energy_Mizer.SyncWord.ToString();
                cmbAddressFiltering.SelectedIndex = (param_Energy_Mizer.AddressFiltering) ? 1 : 0;
                cmbNodeAddress.SelectedIndex = (int)param_Energy_Mizer.NodeAddress;
                cmbBrodcastAddress.SelectedIndex = (int)param_Energy_Mizer.BroadcastAddress;
                cmbAESEncryption.SelectedIndex = (param_Energy_Mizer.AESEncryption) ? 1 : 0;
                txtRFCommandDelay.Text = param_Energy_Mizer.RFCommandDelay.ToString();
                txtRFCmdTimeout.Text = param_Energy_Mizer.RFCommandTimeout.ToString();
                cmbModulationType.SelectedIndex = param_Energy_Mizer.ModulationType;
                cmbPacketEncoding.SelectedIndex = (param_Energy_Mizer.PacketEncoding) ? 1 : 0;
                txtTemperature.Text = param_Energy_Mizer.Temperature.ToString();

                txtSerialNumber.Text = param_Energy_Mizer.SerialNumber.ToString();
                txtLCDContrast.Text = param_Energy_Mizer.LCDContrast.ToString();
                txtMeterToReadType.Text = param_Energy_Mizer.MeterToReadType.ToString();
                txtMeterToRead.Text = DLMS_Common.ArrayToPrintableString(param_Energy_Mizer.MeterToRead);
                txtMeterPassword.Text = DLMS_Common.ArrayToPrintableString(param_Energy_Mizer.MeterPassword);
                cmbBuzzerSettings.SelectedIndex = (param_Energy_Mizer.BuzzerSettings) ? 1 : 0;
                cmbReadHumidity.SelectedIndex = (param_Energy_Mizer.ReadHumidity) ? 1 : 0;
                cmbTemperatureSettings.SelectedIndex = param_Energy_Mizer.TempratureSettings;
                cmbUSBParams.SelectedIndex = param_Energy_Mizer.USBParams;

                txtWiFiWebServerConfigurationIP.Text = (param_Energy_Mizer.WiFiWebServerConfigurationIP == null) ? "" : param_Energy_Mizer.WiFiWebServerConfigurationIP.ToString();
                txtWiFiWebServerConfigurationPort.Text = param_Energy_Mizer.WiFiWebServerConfigurationPort.ToString();
                txtWiFiServerIP.Text = (param_Energy_Mizer.WiFiServerIP == null) ? "" : param_Energy_Mizer.WiFiServerIP.ToString();
                txtWiFiServerPort.Text = param_Energy_Mizer.WiFiServerPort.ToString();
                txtWiFiClientIP.Text = (param_Energy_Mizer.WiFiClientIP == null) ? "" : param_Energy_Mizer.WiFiClientIP.ToString();
                txtWiFiClientPort.Text = param_Energy_Mizer.WiFiClientPort.ToString();
                //txtWiFiBasicConfiguration.Text          = param_Energy_Mizer..ToString();
            }
        }

        internal Param_Energy_Mizer GetParams()
        {
            //TODO:04 DataType Should Change effect For EnergyMizer

            Param_Energy_Mizer EnergyMizerParameters = new Param_Energy_Mizer() ;

            EnergyMizerParameters.RFChannel                          = Convert.ToUInt32(txtRFChannels.Text);
            EnergyMizerParameters.ChannelFilterWB                    = Convert.ToUInt32(txtChannelFilterBW.Text);
            EnergyMizerParameters.TransmitCarrierFreq                = Convert.ToDouble(txtTransCarrierFreq.Text);
            EnergyMizerParameters.ReceiveCarrierFreq                 = Convert.ToDouble(txtTransCarrierFreq.Text);
            EnergyMizerParameters.RFBaudRate                         = Convert.ToUInt32(txtRFBaudRate.Text);
            EnergyMizerParameters.RFPower                            = cmbRFPower.SelectedIndex;
            EnergyMizerParameters.PacketMode                         = Convert.ToByte(cmbPacketMode.SelectedIndex);
            EnergyMizerParameters.FrequencyDeviation                 = Convert.ToUInt32(txtFrequencyDeviation.Text);
            EnergyMizerParameters.ReceiverBandwidth                  = Convert.ToUInt32(txtReceiverBandwidth.Text);
            EnergyMizerParameters.Preamble                           = Convert.ToUInt32(txtPreamble.Text);
            EnergyMizerParameters.SyncWord                           = Convert.ToUInt32(txtSyncWord.Text);
            EnergyMizerParameters.AddressFiltering                   = Convert.ToBoolean(cmbAddressFiltering.SelectedIndex);
            EnergyMizerParameters.NodeAddress                        = Convert.ToUInt32(cmbNodeAddress.SelectedIndex);
            EnergyMizerParameters.BroadcastAddress                   = Convert.ToUInt32(cmbBrodcastAddress.SelectedIndex);
            EnergyMizerParameters.AESEncryption                      = Convert.ToBoolean(cmbAESEncryption.SelectedIndex);
            EnergyMizerParameters.RFCommandDelay                     = Convert.ToUInt32(txtRFCommandDelay.Text);
            EnergyMizerParameters.RFCommandTimeout                   = Convert.ToUInt32(txtRFCmdTimeout.Text);
            EnergyMizerParameters.ModulationType                     = Convert.ToByte(cmbModulationType.SelectedIndex);
            EnergyMizerParameters.PacketEncoding                     = Convert.ToBoolean(cmbPacketEncoding.SelectedIndex);
            EnergyMizerParameters.CostParameters                     = Convert.ToDouble(txtCostParameters.Text);
            EnergyMizerParameters.Temperature                        = Convert.ToUInt32(txtTemperature.Text);

            EnergyMizerParameters.WiFiWebServerConfigurationIP       = (txtWiFiWebServerConfigurationIP.Text.Trim().Length > 0)? IPAddress.Parse(txtWiFiWebServerConfigurationIP.Text) : null;
            EnergyMizerParameters.WiFiWebServerConfigurationPort     = Convert.ToUInt32(txtWiFiWebServerConfigurationPort.Text);
            EnergyMizerParameters.WiFiServerIP                       = (txtWiFiServerIP.Text.Trim().Length > 0)? IPAddress.Parse(txtWiFiServerIP.Text) : null;
            EnergyMizerParameters.WiFiServerPort                     = Convert.ToUInt16(txtWiFiServerPort.Text);
            //EnergyMizerParameters.                                 = For Basic Configuration ;
            EnergyMizerParameters.WiFiClientIP                       = (txtWiFiClientIP.Text.Trim().Length > 0)? IPAddress.Parse(txtWiFiClientIP.Text) : null;
            EnergyMizerParameters.WiFiClientPort                     = Convert.ToUInt32(txtWiFiClientPort.Text) ;

            EnergyMizerParameters.SerialNumber                       = Convert.ToUInt32(txtSerialNumber.Text);
            EnergyMizerParameters.LCDContrast                        = Convert.ToUInt32(txtLCDContrast.Text);
            EnergyMizerParameters.MeterToReadType                    = Convert.ToByte(Convert.ToInt16(txtMeterToReadType.Text));
            EnergyMizerParameters.MeterToRead                        = DLMS_Common.PrintableStringToByteArray(txtMeterToRead.Text);
            EnergyMizerParameters.MeterPassword                      = DLMS_Common.PrintableStringToByteArray(txtMeterPassword.Text);
            //EnergyMizerParameters. = For Data To Read ;
            EnergyMizerParameters.BuzzerSettings                     = Convert.ToBoolean(cmbBuzzerSettings.SelectedIndex);
            EnergyMizerParameters.ReadHumidity                       = Convert.ToBoolean(cmbReadHumidity.SelectedIndex);
            EnergyMizerParameters.TempratureSettings                 = Convert.ToByte(cmbTemperatureSettings.SelectedIndex);
            EnergyMizerParameters.USBParams                          = Convert.ToByte(cmbUSBParams.SelectedIndex);

            EnergyMizerParameters.WiFiSSID                           = DLMS_Common.PrintableStringToByteArray(txtWiFiSSID.Text);
            EnergyMizerParameters.WiFiPassword                       = DLMS_Common.PrintableStringToByteArray(txtWiFiPassword.Text);

            //??? Yahan new add karo...

            return EnergyMizerParameters;
        }


        #endregion // Energy Mizer Configuration



        #region AccessControlMethods

        public bool ApplyAccessRights(List<AccessRights> Rights)
        {
            bool isAnyVisible = false;
            try
            {
                this.SuspendLayout();
                var AccessRight = Rights.Find((x) => String.Equals(x.QuantityName, Misc.EnergyParam.ToString(),
                    StringComparison.OrdinalIgnoreCase));

                if (Rights != null)
                {
                    foreach (var item in Rights)
                    {
                        _HelperAccessRights((EnergyMizer)Enum.Parse(item.QuantityType, item.QuantityName), item.Read, item.Write);
                        if (!isAnyVisible && (item.Read || item.Write))
                            isAnyVisible = true;
                    }
                }
                else
                    return false;

            }
            finally
            {
                this.ResumeLayout();
            }
            return isAnyVisible;
        }

        private void _HelperAccessRights(EnergyMizer qty, bool read, bool write)
        {
            switch (qty)
            {
                case EnergyMizer.RFConfiguration:
                    gbRFConfigs.Visible = (read || write);
                    gbRFConfigs.Enabled = write;
                    break;
                case EnergyMizer.WifiConfiguration:
                    gbWiFiConfigs.Visible = (read || write);
                    gbWiFiConfigs.Enabled = write;
                    break;
                case EnergyMizer.DisplayConfiguration:
                    gbDisplayConfigs.Visible = (read || write);
                    gbDisplayConfigs.Enabled = write;
                    break;
                default:
                    break;
            }
        }

        #endregion

        private sbyte OffSetMinRange = -100;
        private sbyte OffSetMaxRange = 100;
        private byte MinRange_0 = 0;
        private byte MaxRange_255 = 255;

        private void btnSetLdrSetting_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to 'Energy Mizer'");
                else
                {
                    Application_Controller.IsIOBusy = true;

                    Param_Ldr_Setting param = new Param_Ldr_Setting();
                    sbyte _offset = Convert.ToSByte(tbOffset_LdrSetting.Text.Trim());
                    //GetControlValue(out sbyte offset, tbOffset_LdrSetting, OffSetMinRange, OffSetMaxRange);
                    GetControlValue(out byte divider, tbDivider_LdrSetting, MinRange_0, MaxRange_255);
                    GetControlValue(out byte max, tbMax_LdrSetting, MinRange_0, MaxRange_255);
                    GetControlValue(out byte min, tbMin_LdrSetting, MinRange_0, MaxRange_255);
                    GetControlValue(out byte rfu, tbRfu_LdrSetting, MinRange_0, MaxRange_255);

                    param.IsEnabled = cbIsEnabled_LdrSetting.Checked;
                    param.Offset = _offset;
                    param.Divider = (byte)divider;
                    param.Max = (byte)max;
                    param.Min = (byte)min;
                    param.RFU = (byte)rfu;

                    Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_LDR_Setting(param);
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Success", "Set_Param_LDR Successfully");
                    else
                        throw new Exception(result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Setting Set_Param_LDR, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnGetLdrSetting_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    notifier = new Notification("Error", "Create Association to 'Energy Mizer'");
                else
                {
                    Param_Ldr_Setting param = new Param_Ldr_Setting();
                    Application_Controller.IsIOBusy = true;
                    application_Controller.Param_Controller.Get_Param_LDR_Setting(ref param);

                    cbIsEnabled_LdrSetting.Checked = param.IsEnabled;
                    tbOffset_LdrSetting.Text = param.Offset.ToString();
                    //SetControlValue(param.Offset, tbOffset_LdrSetting, OffSetMinRange, OffSetMaxRange);
                    SetControlValue(param.Divider, tbDivider_LdrSetting, MinRange_0, MaxRange_255);
                    SetControlValue(param.Max, tbMax_LdrSetting, MinRange_0, MaxRange_255);
                    SetControlValue(param.Min, tbMin_LdrSetting, MinRange_0, MaxRange_255);
                    SetControlValue(param.RFU, tbRfu_LdrSetting, MinRange_0, MaxRange_255);

                    notifier = new Notification("Success", "Param_LDR Read Successful");
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Param_LDR Read Error, \n" + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            SetControlValue(-4, tbOffset_LdrSetting, OffSetMinRange, OffSetMaxRange);
            SetControlValue(40, tbDivider_LdrSetting, MinRange_0, MaxRange_255);
            SetControlValue(80, tbMax_LdrSetting,MinRange_0, MaxRange_255);
            SetControlValue(0, tbMin_LdrSetting, MinRange_0, MaxRange_255);
            SetControlValue(0, tbRfu_LdrSetting, MinRange_0, MaxRange_255);
        }

        private void SetControlValue(short val, TextBox tb, short minRange, short maxRange)
        {
            try
            {
                if (val > maxRange || val < minRange)
                    throw new Exception();
                tb.ForeColor = Color.Black;
            }
            catch (Exception)
            {
                tb.ForeColor = Color.Red;
            }
            finally
            {
                tb.Text = val.ToString();
            }
        }
        private void GetControlValue(out byte val, TextBox tb, short minRange, short maxRange)
        {
            try
            {
                val = Convert.ToByte(tb.Text.Trim());
                if (val > maxRange || val < minRange)
                    throw new Exception();
                tb.ForeColor = Color.Black;
            }
            catch (Exception)
            {
                tb.ForeColor = Color.Red;
                throw;
            }
        }
    }
}
