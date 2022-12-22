using SharedCode.Comm.Param;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccurateOptocomSoftware.ApplicationGUI.GUI
{
    public partial class frmDummy : Form
    {
        public frmDummy()
        {
            InitializeComponent();


            #region -- EnergyMizer Dynamic Data From Enums --

            //foreach(var item in Enum.GetNames(typeof(ModulationType)) ) 
            //    this.cmbModulationType.Items.Add(item);

            //foreach (var item in Enum.GetNames(typeof(USB_Parameter))) 
            //    this.cmbUSBParams.Items.Add(item);  

            //foreach (var item in Enum.GetNames(typeof(PacketEncoding))) 
            //    this.cmbPacketEncoding.Items.Add(item);

            //foreach (var item in Enum.GetNames(typeof(PacketMode)))
            //    this.cmbPacketMode.Items.Add(item);

            //for (short i = -18; i <= 13; i++) 
            //    this.cmbRFPower.Items.Add(i.ToString("00")); 

            //#endregion // -- Dynamic Data From Enums --

            //#region Energymizer Parameters
            //this.cmbModulationType.SelectedIndex =
            //this.cmbUSBParams.SelectedIndex =
            //this.cmbPacketEncoding.SelectedIndex =

            //this.cmbAddressFiltering.SelectedIndex =
            //this.cmbNodeAddress.SelectedIndex =
            //this.cmbBrodcastAddress.SelectedIndex =
            //this.cmbAESEncryption.SelectedIndex =
            //this.cmbBuzzerSettings.SelectedIndex =
            //this.cmbReadHumidity.SelectedIndex =
            //this.cmbTemperatureSettings.SelectedIndex =
            //this.cmbPacketMode.SelectedIndex = 
            //this.cmbRFPower.SelectedIndex = 0; 
            #endregion

        }

        #region Tab  - Energy Mizer

        //#region ___ Energy Mizer Configuration ___

        //#region -- RF Configuration Controls --

        //#region --01 RF_Channels 

        //private void btnRFChannelsSet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_RFChannel(Convert.ToUInt32(txtRFChannels.Text));
        //            if (result == Data_Access_Result.Success)
        //                notifier = new Notification("Success", "RFChannelsSet Successfully");
        //            else
        //                throw new Exception(result.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "Error occurred while Setting RFChannelsSet, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void btnRFChannelsGet_Click(object sender, EventArgs e)
        //{
        //    Notification notification = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notification = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            uint ch = Application_Controller.Param_Controller.GET_Param_RFChannel();
        //            txtRFChannels.Text = ch.ToString();
        //            notification = new Notification("Success", "GET_Param_RFChannel Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Notification("Error", "GET_Param_RFChannel Read Error," + Environment.NewLine + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void txtRFChannels_TextChanged(object sender, EventArgs e)
        //{
        //    if (LocalCommon.TextBox_validation(0, 100, txtRFChannels))
        //        this.btnRFChannelsSet.Enabled = true;
        //    else
        //        this.btnRFChannelsSet.Enabled = false;
        //}

        //#endregion  // --01 RF_Channels

        //#region --02 Channel_Filter_BW 

        //private void btnChannelFilterBWSet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Channel_Filter_BW(Convert.ToUInt32(txtChannelFilterBW.Text));
        //            if (result == Data_Access_Result.Success)
        //                notifier = new Notification("Success", "Set_Param_Channel_Filter_BW Successfully");
        //            else
        //                throw new Exception(result.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "Error occurred while Setting Set_Param_Channel_Filter_BW, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void btnChannelFilterBWGet_Click(object sender, EventArgs e)
        //{
        //    Notification notification = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notification = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            double ch = Application_Controller.Param_Controller.GET_Param_Channel_Filter_BW();
        //            //ch = ch / 1000;  // Bcz of Kilo unit. 10^3 (1,000)
        //            txtChannelFilterBW.Text = ch.ToString();
        //            notification = new Notification("Success", "GET_Param_Channel_Filter_BW Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Notification("Error", "GET_Param_Channel_Filter_BW Read Error, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void txtChannelFilterBW_TextChanged(object sender, EventArgs e)
        //{
        //    if (LocalCommon.TextBox_validation(2.6, 500, txtChannelFilterBW))
        //    {
        //        this.btnChannelFilterBWSet.Enabled = true;
        //    }
        //    else
        //    {
        //        this.btnChannelFilterBWSet.Enabled = false;
        //    }
        //}

        //#endregion  // --02 Channel_Filter_BW

        //#region --03 Transmit_Carrier_Frequency 

        //private void btnTransCarrierFreqSet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Transmit_Carrier_Frequency(Convert.ToDouble(txtTransCarrierFreq.Text));
        //            if (result == Data_Access_Result.Success)
        //                notifier = new Notification("Success", "Set_Param_Transmit_Carrier_Frequency Successfully");
        //            else
        //                throw new Exception(result.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "Error occurred while Setting Set_Param_Transmit_Carrier_Frequency, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void btnTransCarrierFreqGet_Click(object sender, EventArgs e)
        //{
        //    Notification notification = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notification = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            double ch = Application_Controller.Param_Controller.GET_Param_Transmit_Carrier_Frequency();
        //            //ch = ch / 10000;  // Bcz of Mega unit. 10^6 (1,000,000)
        //            txtTransCarrierFreq.Text = ch.ToString();
        //            notification = new Notification("Success", "GET_Param_Transmit_Carrier_Frequency Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Notification("Error", "GET_Param_Transmit_Carrier_Frequency Read Error, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void txtTransCarrierFreq_TextChanged(object sender, EventArgs e)
        //{
        //    if (LocalCommon.TextBox_validation(424, 510, txtTransCarrierFreq))
        //        this.btnTransCarrierFreqSet.Enabled = true;
        //    else
        //        this.btnTransCarrierFreqSet.Enabled = false;
        //}

        //#endregion  // --03 Transmit_Carrier_Frequency 

        //#region --04 Receive_Carrier_Frequency 

        //private void btnReceiveCarrierFreqSet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Receive_Carrier_Frequency(Convert.ToDouble(txtReceiveCarrierFreq.Text));
        //            if (result == Data_Access_Result.Success)
        //                notifier = new Notification("Success", "Set_Param_Receive_Carrier_Frequency Successfully");
        //            else
        //                throw new Exception(result.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "Error occurred while Setting Set_Param_Receive_Carrier_Frequency, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void btnReceiveCarrierFreqGet_Click(object sender, EventArgs e)
        //{
        //    Notification notification = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notification = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            double ch = Application_Controller.Param_Controller.GET_Param_Receive_Carrier_Frequency();
        //            //ch = ch / 10000;  // Bcz of Mega unit. 10^6 (1,000,000)
        //            txtReceiveCarrierFreq.Text = ch.ToString();
        //            notification = new Notification("Success", "GET_Param_Receive_Carrier_Frequency Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Notification("Error", "GET_Param_Receive_Carrier_Frequency Read Error, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void txtReceiveCarrierFreq_TextChanged(object sender, EventArgs e)
        //{
        //    if (LocalCommon.TextBox_validation(424, 510, txtReceiveCarrierFreq))
        //        this.btnReceiveCarrierFreqSet.Enabled = true;
        //    else
        //        this.btnReceiveCarrierFreqSet.Enabled = false;
        //}

        //#endregion  // --04 Receive_Carrier_Frequency 

        //#region --05 RF_Baud_Rate

        //private void btnRFBaudRateSet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_RF_Baud_Rate(Convert.ToUInt32(txtRFBaudRate.Text));
        //            if (result == Data_Access_Result.Success)
        //                notifier = new Notification("Success", "Set_Param_RF_Baud_Rate Successfully");
        //            else
        //                throw new Exception(result.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "Error occurred while Setting Set_Param_RF_Baud_Rate, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void btnRFBaudRateGet_Click(object sender, EventArgs e)
        //{
        //    Notification notification = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notification = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            double ch = Application_Controller.Param_Controller.GET_Param_RF_Baud_Rate();
        //            //ch = ch / 1000;  // Bcz of Kilo unit. 10^3 (1,000)
        //            txtRFBaudRate.Text = ch.ToString();
        //            notification = new Notification("Success", "GET_Param_RF_Baud_Rate Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Notification("Error", "GET_Param_RF_Baud_Rate Read Error, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void txtRFBaudRate_TextChanged(object sender, EventArgs e)
        //{
        //    if (LocalCommon.TextBox_validation(1.2, 300.00, txtRFBaudRate))
        //        this.btnRFBaudRateSet.Enabled = true;
        //    else
        //        this.btnRFBaudRateSet.Enabled = false;
        //}

        //#endregion  // --05 RF_Baud_Rate

        //#region --06 RF_Power

        //private void btnRFPowerSet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_RF_Power(cmbRFPower.SelectedIndex);
        //            if (result == Data_Access_Result.Success)
        //                notifier = new Notification("Success", "Set_Param_RF_Power Successfully");
        //            else
        //                throw new Exception(result.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "Error occurred while Setting Set_Param_RF_Power, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void btnRFPowerGet_Click(object sender, EventArgs e)
        //{
        //    Notification notification = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notification = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            int ch = Application_Controller.Param_Controller.GET_Param_RF_Power();
        //            cmbRFPower.SelectedIndex = ch;
        //            notification = new Notification("Success", "GET_Param_RF_Power Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Notification("Error", "GET_Param_RF_Power Read Error, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //#endregion  // --06 RF_Power

        //#region --07 Packet_Mode 

        //private void btnPacketModeSet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Packet_Mode(Convert.ToByte(cmbPacketMode.SelectedIndex));
        //            if (result == Data_Access_Result.Success)
        //                notifier = new Notification("Success", "Set_Param_Packet_Mode Successfully");
        //            else
        //                throw new Exception(result.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "Error occurred while Setting Set_Param_Packet_Mode, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void btnPacketModeGet_Click(object sender, EventArgs e)
        //{
        //    Notification notification = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notification = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            PacketMode ch = Application_Controller.Param_Controller.GET_Param_Packet_Mode();
        //            cmbPacketMode.SelectedIndex = (byte)ch;
        //            notification = new Notification("Success", "GET_Param_Packet_Mode Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Notification("Error", "GET_Param_Packet_Mode Read Error, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //#endregion  // --07 Packet_Mode 

        //#region --08 Frequency_Deviation 

        //private void btnFrequencyDeviationSet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Frequency_Deviation(Convert.ToUInt32(txtFrequencyDeviation.Text));
        //            if (result == Data_Access_Result.Success)
        //                notifier = new Notification("Success", "Set_Param_Frequency_Deviation Successfully");
        //            else
        //                throw new Exception(result.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "Error occurred while Setting Set_Param_Frequency_Deviation, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void btnFrequencyDeviationGet_Click(object sender, EventArgs e)
        //{
        //    Notification notification = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notification = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            double ch = Application_Controller.Param_Controller.GET_Param_Frequency_Deviation();
        //            //ch = ch / 1000;  // Bcz of Kilo unit. 10^3 (1,000)
        //            txtFrequencyDeviation.Text = ch.ToString();
        //            notification = new Notification("Success", "GET_Param_Frequency_Deviation Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Notification("Error", "GET_Param_Frequency_Deviation Read Error, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void txtFrequencyDeviation_TextChanged(object sender, EventArgs e)
        //{
        //    if (LocalCommon.TextBox_validation(0.6, 300, txtFrequencyDeviation))
        //        this.btnFrequencyDeviationSet.Enabled = true;
        //    else
        //        this.btnFrequencyDeviationSet.Enabled = false;
        //}

        //#endregion  // --08 Frequency_Deviation 

        //#region --09 Receiver_Bandwidth 

        //private void btnReceiverBandwidthSet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Receiver_Bandwidth(Convert.ToUInt32(txtReceiverBandwidth.Text));
        //            if (result == Data_Access_Result.Success)
        //                notifier = new Notification("Success", "Set_Param_Receiver_Bandwidth Successfully");
        //            else
        //                throw new Exception(result.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "Error occurred while Setting Set_Param_Receiver_Bandwidth, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void btnReceiverBandwidthGet_Click(object sender, EventArgs e)
        //{
        //    Notification notification = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notification = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            double ch = Application_Controller.Param_Controller.GET_Param_Receiver_Bandwidth();
        //            //ch = ch / 1000;  // Bcz of Kilo unit. 10^3 (1,000)
        //            txtReceiverBandwidth.Text = ch.ToString();
        //            notification = new Notification("Success", "GET_Param_Receiver_Bandwidth Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Notification("Error", "GET_Param_Receiver_Bandwidth Read Error, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void txtReceiverBandwidth_TextChanged(object sender, EventArgs e)
        //{
        //    //???  receiver Bandwidth? *** Bit Rate < 2x RxBw ***
        //    //if (LocalCommon.TextBox_validation(1, 50, txtReceiverBandwidth))
        //    //    this.btnReceiverBandwidthSet.Enabled = true;
        //    //else
        //    //    this.btnReceiverBandwidthSet.Enabled = false;
        //}

        //#endregion  // --09 Receiver_Bandwidth 

        //#region --10 Preamble 

        //private void btnPreambleSet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Preamble(Convert.ToUInt32(txtPreamble.Text));
        //            if (result == Data_Access_Result.Success)
        //                notifier = new Notification("Success", "Set_Param_Preamble Successfully");
        //            else
        //                throw new Exception(result.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "Error occurred while Setting Set_Param_Preamble, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void btnPreambleGet_Click(object sender, EventArgs e)
        //{
        //    Notification notification = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notification = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            uint ch = Application_Controller.Param_Controller.GET_Param_Preamble();
        //            txtPreamble.Text = ch.ToString();
        //            notification = new Notification("Success", "GET_Param_Preamble Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Notification("Error", "GET_Param_Preamble Read Error, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void txtPreamble_TextChanged(object sender, EventArgs e)
        //{
        //    if (LocalCommon.TextBox_validation(4, 170, txtPreamble))
        //        this.btnPreambleSet.Enabled = true;
        //    else
        //        this.btnPreambleSet.Enabled = false;
        //}

        //#endregion  // --10 Preamble 

        //#region --11 Sync_Word 

        //private void btnSyncWordSet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_SyncWord(Convert.ToUInt32(txtSyncWord.Text));
        //            if (result == Data_Access_Result.Success)
        //                notifier = new Notification("Success", "Set_Param_SyncWord Successfully");
        //            else
        //                throw new Exception(result.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "Error occurred while Setting Set_Param_SyncWord, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void btnSyncWordGet_Click(object sender, EventArgs e)
        //{
        //    Notification notification = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notification = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            uint ch = Application_Controller.Param_Controller.GET_Param_SyncWord();
        //            txtSyncWord.Text = ch.ToString();
        //            notification = new Notification("Success", "GET_Param_SyncWord Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Notification("Error", "GET_Param_SyncWord Read Error, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void txtSyncWord_TextChanged(object sender, EventArgs e)
        //{
        //    if (LocalCommon.TextBox_validation(2, 11732, txtSyncWord))
        //    {
        //        this.btnSyncWordSet.Enabled = true;
        //    }
        //    else
        //    {
        //        this.btnSyncWordSet.Enabled = false;
        //    }
        //}

        //#endregion  // --11 Sync_Word 

        //#region --12 Address_Filtering 

        //private void btnAddressFilteringSet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Address_Filtering(Convert.ToBoolean(cmbAddressFiltering.SelectedIndex));
        //            if (result == Data_Access_Result.Success)
        //                notifier = new Notification("Success", "Set_Param_Address_Filtering Successfully");
        //            else
        //                throw new Exception(result.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "Error occurred while Setting Set_Param_Address_Filtering, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void btnAddressFilteringGet_Click(object sender, EventArgs e)
        //{
        //    Notification notification = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notification = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            bool ch = Application_Controller.Param_Controller.GET_Param_Address_Filtering();
        //            cmbAddressFiltering.SelectedIndex = (ch) ? 1 : 0; // Enable, if true. Else, Disable...
        //            notification = new Notification("Success", "GET_Param_Address_Filtering Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Notification("Error", "GET_Param_Address_Filtering Read Error, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //#endregion  // --12 Address_Filtering 

        //#region --13 Node_Address 

        //private void btnNodeAddressSet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Node_Address(Convert.ToUInt32(cmbNodeAddress.SelectedIndex));
        //            if (result == Data_Access_Result.Success)
        //                notifier = new Notification("Success", "Set_Param_Node_Address Successfully");
        //            else
        //                throw new Exception(result.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "Error occurred while Setting Set_Param_Node_Address, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void btnNodeAddressGet_Click(object sender, EventArgs e)
        //{
        //    Notification notification = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notification = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            uint ch = Application_Controller.Param_Controller.GET_Param_Node_Address();
        //            cmbNodeAddress.SelectedIndex = Convert.ToInt32(ch); // (ch); ? 1 : 0; // Enable, if true. Else, Disable...
        //            notification = new Notification("Success", "GET_Param_Node_Address Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Notification("Error", "GET_Param_Node_Address Read Error, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //#endregion  // --13 Node_Address 

        //#region --14 Broadcast_Address

        //private void btnBroadcastAddressSet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Broadcast_Address(Convert.ToUInt32(cmbBrodcastAddress.SelectedIndex));
        //            if (result == Data_Access_Result.Success)
        //                notifier = new Notification("Success", "Set_Param_Broadcast_Address Successfully");
        //            else
        //                throw new Exception(result.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "Error occurred while Setting Set_Param_Broadcast_Address, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void btnBroadcastAddressGet_Click(object sender, EventArgs e)
        //{
        //    Notification notification = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notification = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            uint ch = Application_Controller.Param_Controller.GET_Param_Broadcast_Address();
        //            cmbBrodcastAddress.SelectedIndex = Convert.ToInt32(ch);// (ch) ? 1 : 0; // Enable, if true. Else, Disable...
        //            notification = new Notification("Success", "GET_Param_Broadcast_Address Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Notification("Error", "GET_Param_Broadcast_Address Read Error, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //#endregion  // --14 Broadcast_Address

        //#region --15 AES_Encryption 

        //private void btnAESEncryptionSet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_AES_Encryption(Convert.ToBoolean(cmbAESEncryption.SelectedIndex));
        //            if (result == Data_Access_Result.Success)
        //                notifier = new Notification("Success", "Set_Param_AES_Encryption Successfully");
        //            else
        //                throw new Exception(result.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "Error occurred while Setting Set_Param_AES_Encryption, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void btnAESEncryptionGet_Click(object sender, EventArgs e)
        //{
        //    Notification notification = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notification = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            bool ch = Application_Controller.Param_Controller.GET_Param_AES_Encryption();
        //            cmbAESEncryption.SelectedIndex = (ch) ? 1 : 0; // Enable, if true. Else, Disable...
        //            notification = new Notification("Success", "GET_Param_AES_Encryption Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Notification("Error", "GET_Param_AES_Encryption Read Error, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //#endregion  // --15 AES_Encryption 

        //#region --16 RF_Command_Delay 

        //private void btnRFCommandDelaySet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_RF_Command_Delay(Convert.ToUInt32(txtRFCommandDelay.Text));
        //            if (result == Data_Access_Result.Success)
        //                notifier = new Notification("Success", "Set_Param_RF_Command_Delay Successfully");
        //            else
        //                throw new Exception(result.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "Error occurred while Setting Set_Param_RF_Command_Delay, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void btnRFCommandDelayGet_Click(object sender, EventArgs e)
        //{
        //    Notification notification = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notification = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            double ch = Application_Controller.Param_Controller.GET_Param_RF_Command_Delay();
        //            txtRFCommandDelay.Text = ch.ToString();
        //            notification = new Notification("Success", "GET_Param_RF_Command_Delay Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Notification("Error", "GET_Param_RF_Command_Delay Read Error, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void txtRFCommandDelay_TextChanged(object sender, EventArgs e)
        //{
        //    if (LocalCommon.TextBox_validation(10, 255, txtRFCommandDelay))
        //    {
        //        this.btnRFCommandDelaySet.Enabled = true;
        //    }
        //    else
        //    {
        //        this.btnRFCommandDelaySet.Enabled = false;
        //    }
        //}

        //#endregion  // --16 RF_Command_Delay 

        //#region --17 RF_Command_Timeout

        //private void btnRFCmdTimeoutSet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_RF_Command_Timeout(Convert.ToUInt32(txtRFCmdTimeout.Text));
        //            if (result == Data_Access_Result.Success)
        //                notifier = new Notification("Success", "Set_Param_RF_Command_Timeout Successfully");
        //            else
        //                throw new Exception(result.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "Error occurred while Setting Set_Param_RF_Command_Timeout, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void btnRFCmdTimeoutGet_Click(object sender, EventArgs e)
        //{
        //    Notification notification = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notification = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            double ch = Application_Controller.Param_Controller.GET_Param_RF_Command_Timeout();
        //            txtRFCmdTimeout.Text = ch.ToString();
        //            notification = new Notification("Success", "GET_Param_RF_Command_Timeout Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Notification("Error", "GET_Param_RF_Command_Timeout Read Error, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void txtRFCmdTimeout_TextChanged(object sender, EventArgs e)
        //{
        //    if (LocalCommon.TextBox_validation(10, 255, txtRFCmdTimeout))
        //        this.btnRFCmdTimeoutSet.Enabled = true;
        //    else
        //        this.btnRFCmdTimeoutSet.Enabled = false;
        //}

        //#endregion  // --17 RF_Command_Timeout

        //#region --18 Modulation_Type

        //private void btnModulationTypeSet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Modulation_Type(Convert.ToByte(cmbModulationType.SelectedIndex));
        //            if (result == Data_Access_Result.Success)
        //                notifier = new Notification("Success", "Set_Param_Modulation_Type Successfully");
        //            else
        //                throw new Exception(result.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "Error occurred while Setting Set_Param_Modulation_Type, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void btnModulationTypeGet_Click(object sender, EventArgs e)
        //{
        //    Notification notification = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notification = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            ModulationType ch = Application_Controller.Param_Controller.GET_Param_Modulation_Type();
        //            cmbModulationType.SelectedIndex = (byte)ch;
        //            notification = new Notification("Success", "GET_Param_Modulation_Type Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Notification("Error", "GET_Param_Modulation_Type Read Error, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //#endregion  // --18 Modulation_Type

        //#region --19 Packet_Encoding 

        //private void btnPacketEncodingSet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Packet_Encoding(Convert.ToBoolean(cmbPacketEncoding.SelectedIndex));
        //            if (result == Data_Access_Result.Success)
        //                notifier = new Notification("Success", "Set_Param_Packet_Encoding Successfully");
        //            else
        //                throw new Exception(result.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "Error occurred while Setting Set_Param_Packet_Encoding, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void btnPacketEncodingGet_Click(object sender, EventArgs e)
        //{
        //    Notification notification = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notification = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            bool ch = Application_Controller.Param_Controller.GET_Param_Packet_Encoding();
        //            cmbPacketEncoding.SelectedIndex = (ch) ? 1 : 0; //  Whitening, if true. Else Manchester,
        //            notification = new Notification("Success", "GET_Param_Packet_Encoding Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Notification("Error", "GET_Param_Packet_Encoding Read Error, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //#endregion  // --19 Packet_Encoding 

        //#region --20 Cost_Parameters

        //private void btnCostParametersSet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Cost_Parameters(Convert.ToDouble(txtCostParameters.Text));
        //            if (result == Data_Access_Result.Success)
        //                notifier = new Notification("Success", "Set_Param_Cost_Parameters Successfully");
        //            else
        //                throw new Exception(result.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "Error occurred while Setting Set_Param_Cost_Parameters, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void btnCostParametersGet_Click(object sender, EventArgs e)
        //{
        //    Notification notification = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notification = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            double ch = Application_Controller.Param_Controller.GET_Param_Cost_Parameters();
        //            txtCostParameters.Text = ch.ToString();
        //            notification = new Notification("Success", "GET_Param_Cost_Parameters Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Notification("Error", "GET_Param_Cost_Parameters Read Error, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void txtCostParameters_TextChanged(object sender, EventArgs e)
        //{
        //    if (LocalCommon.TextBox_validation(0.01, 635.00, txtCostParameters))
        //        this.btnCostParametersSet.Enabled = true;
        //    else
        //        this.btnCostParametersSet.Enabled = false;
        //}

        //#endregion  // --20 Cost_Parameters

        //#region --21 Temperature (GET)

        //private void btnTemperatureGet_Click(object sender, EventArgs e)
        //{
        //    Notification notification = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notification = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            uint ch = Application_Controller.Param_Controller.GET_Param_Temperature();
        //            txtTemperature.Text = ch.ToString();
        //            notification = new Notification("Success", "GET_Param_Temperature Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Notification("Error", "GET_Param_Temperature Read Error, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //#endregion  // --21 Temperature (GET)


        //#endregion // RF Configuration Controls

        //#region -- WiFi Configurations Controls --

        //#region --1 WiFi_Web_server_Configuration_IP

        //private void btnWiFiWebServerConfigurationIPSet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_WiFi_IP_client_TCP_Server_or_WEB_Server(IPAddress.Parse(txtWiFiWebServerConfigurationIP.Text));
        //            if (result == Data_Access_Result.Success)
        //                notifier = new Notification("Success", "Set_Param_WiFi_IP_client_TCP_Server_or_WEB_Server Successfully");
        //            else
        //                throw new Exception(result.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "Error occurred while Setting Set_Param_WiFi_IP_client_TCP_Server_or_WEB_Server, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void btnWiFiWebServerConfigurationIPGet_Click(object sender, EventArgs e)
        //{
        //    Notification notification = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notification = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            IPAddress ch = Application_Controller.Param_Controller.GET_Param_WiFi_IP_client_TCP_Server_or_WEB_Server();
        //            txtWiFiWebServerConfigurationIP.Text = ch.ToString();
        //            notification = new Notification("Success", "GET_Param_WiFi_IP_client_TCP_Server_or_WEB_Server Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Notification("Error", "GET_Param_WiFi_IP_client_TCP_Server_or_WEB_Server Read Error, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void txtWiFiWebServerConfigurationIP_TextChanged(object sender, EventArgs e)
        //{
        //    if (LocalCommon.ValidateIPv4(txtWiFiWebServerConfigurationIP))
        //        this.btnWiFiWebServerConfigurationIPSet.Enabled = true;
        //    else
        //        this.btnWiFiWebServerConfigurationIPSet.Enabled = false;
        //}

        //#endregion  // WiFi_Web_server_Configuration_IP

        //#region --2 WiFi_Web_server_Configuration_Port

        //private void btnWiFiWebServerConfigurationPortSet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_WiFi_Web_Server_Port(Convert.ToUInt32(txtWiFiWebServerConfigurationPort.Text));
        //            if (result == Data_Access_Result.Success)
        //                notifier = new Notification("Success", "Set_Param_WiFi_Web_Server_Port Successfully");
        //            else
        //                throw new Exception(result.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "Error occurred while Setting Set_Param_WiFi_Web_Server_Port, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void btnWiFiWebServerConfigurationPortGet_Click(object sender, EventArgs e)
        //{
        //    Notification notification = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notification = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            uint ch = Application_Controller.Param_Controller.GET_Param_WiFi_Web_Server_Port();
        //            txtWiFiWebServerConfigurationIP.Text = ch.ToString();
        //            notification = new Notification("Success", "GET_Param_WiFi_Web_Server_Port Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Notification("Error", "GET_Param_WiFi_Web_Server_Port Read Error, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void txtWiFiWebServerConfigurationPort_TextChanged(object sender, EventArgs e)
        //{
        //    if (LocalCommon.TextBox_validation(1, 65535, txtWiFiWebServerConfigurationPort))
        //        this.btnWiFiWebServerConfigurationPortSet.Enabled = true;
        //    else
        //        this.btnWiFiWebServerConfigurationPortSet.Enabled = false;
        //}

        //#endregion  // WiFi_Web_server_Configuration_Port

        //#region --3 WiFi_Server_IP

        //private void btnWiFiServerIPSet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_WiFi_IP_client_TCP_Server_or_WEB_Server(IPAddress.Parse(txtWiFiServerIP.Text));
        //            if (result == Data_Access_Result.Success)
        //                notifier = new Notification("Success", "Set_Param_WiFi_IP_client_TCP_Server_or_WEB_Server Successfully");
        //            else
        //                throw new Exception(result.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "Error occurred while Setting Set_Param_WiFi_IP_client_TCP_Server_or_WEB_Server, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void btnWiFiServerIPGet_Click(object sender, EventArgs e)
        //{
        //    Notification notification = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notification = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            IPAddress ch = Application_Controller.Param_Controller.GET_Param_WiFi_IP_client_TCP_Server_or_WEB_Server();
        //            txtWiFiServerIP.Text = ch.ToString();
        //            notification = new Notification("Success", "GET_Param_WiFi_IP_client_TCP_Server_or_WEB_Server Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Notification("Error", "GET_Param_WiFi_IP_client_TCP_Server_or_WEB_Server Read Error, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void txtWiFiServerIP_TextChanged(object sender, EventArgs e)
        //{
        //    if (LocalCommon.ValidateIPv4(txtWiFiServerIP))
        //        this.btnWiFiServerIPSet.Enabled = true;
        //    else
        //        this.btnWiFiServerIPSet.Enabled = false;
        //}

        //#endregion  // WiFi_Server_IP

        //#region --4 WiFi_Server_Port

        //private void btnWiFiServerPortSet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_WiFi_Server_Port(Convert.ToUInt16(txtWiFiServerPort.Text));
        //            if (result == Data_Access_Result.Success)
        //                notifier = new Notification("Success", "Set_Param_WiFi_Server_Port Successfully");
        //            else
        //                throw new Exception(result.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "Error occurred while Setting Set_Param_WiFi_Server_Port, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void btnWiFiServerPortGet_Click(object sender, EventArgs e)
        //{
        //    Notification notification = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notification = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            ushort ch = Application_Controller.Param_Controller.GET_Param_WiFi_Server_Port();
        //            txtWiFiServerPort.Text = ch.ToString();
        //            notification = new Notification("Success", "GET_Param_WiFi_Server_Port Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Notification("Error", "GET_Param_WiFi_Server_Port Read Error, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void txtWiFiServerPort_TextChanged(object sender, EventArgs e)
        //{
        //    if (LocalCommon.TextBox_validation(1, 65535, txtWiFiServerPort))
        //        this.btnWiFiServerPortSet.Enabled = true;
        //    else
        //        this.btnWiFiServerPortSet.Enabled = false;
        //}

        //#endregion  // WiFi_Server_Port

        //#region --5 WiFi_Basic_Configuration

        //private void btnWiFiBasicConfigurationSet_Click(object sender, EventArgs e)
        //{
        //    //Notification notifier = null;
        //    //try
        //    //{
        //    //    if (!(Application_Process.Is_Association_Developed))
        //    //        MessageBox.Show("Create Association to Meter");
        //    //    else
        //    //    {
        //    //        Application_Controller.IsIOBusy = true;
        //    //        Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_(Convert.ToInt32(txt.Text));
        //    //        if (result == Data_Access_Result.Success)
        //    //          notifier = new Notification("Success", "NNNNNNNNNNNNNN Successfully");
        //    //    else
        //    //    throw new Exception(result.ToString());
        //    //}
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    notifier = new Notification("Error", "Error occurred while Setting NNNNNNNNNNNNNN, \n" + ex.Message);
        //    //}
        //    //finally
        //    //{
        //    //    Application_Controller.IsIOBusy = false;
        //    //}
        //}

        //private void btnWiFiBasicConfigurationGet_Click(object sender, EventArgs e)
        //{
        //    //Notification notification = null;
        //    //try
        //    //{
        //    //    if (!(Application_Process.Is_Association_Developed))
        //    //        notification = new Notification("Error", "Create Association to Meter");
        //    //    else
        //    //    {
        //    //        Application_Controller.IsIOBusy = true;
        //    //        int ch = Application_Controller.Param_Controller.GET_Param_();
        //    //        txt.Text = ch.ToString();
        //    //        notification = new Notification("Success", "NNNNNNNNNNN Read Successful");
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    notification = new Notification("Error", "NNNNNNNNNNNNN Read Error, \n" + ex.Message);
        //    //}
        //    //finally
        //    //{
        //    //    Application_Controller.IsIOBusy = false;
        //    //}
        //}

        //#endregion  // --5 WiFi_Basic_Configuration

        //#region --6 WiFi_Client_IP

        //private void btnWiFiClientIPSet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_WiFi_IP_client_TCP_Server_or_WEB_Server(IPAddress.Parse(txtWiFiClientIP.Text));
        //            if (result == Data_Access_Result.Success)
        //                notifier = new Notification("Success", "Set_Param_WiFi_IP_client_TCP_Server_or_WEB_Server Successfully");
        //            else
        //                throw new Exception(result.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "Error occurred while Setting Set_Param_WiFi_IP_client_TCP_Server_or_WEB_Server, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void btnWiFiClientIPGet_Click(object sender, EventArgs e)
        //{
        //    Notification notification = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notification = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            IPAddress ch = Application_Controller.Param_Controller.GET_Param_WiFi_IP_client_TCP_Server_or_WEB_Server();
        //            txtWiFiClientIP.Text = ch.ToString();
        //            notification = new Notification("Success", "GET_Param_WiFi_IP_client_TCP_Server_or_WEB_Server Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Notification("Error", "GET_Param_WiFi_IP_client_TCP_Server_or_WEB_Server Read Error, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void txtWiFiClientIP_TextChanged(object sender, EventArgs e)
        //{
        //    if (LocalCommon.ValidateIPv4(txtWiFiClientIP))
        //        this.btnWiFiClientIPSet.Enabled = true;
        //    else
        //        this.btnWiFiClientIPSet.Enabled = false;
        //}

        //#endregion  // WiFi_Client_IP

        //#region --7 WiFi_Client_Port

        //private void btnWiFiClientPortSet_Click(object sender, EventArgs e)
        //{
        //    //Notification notifier = null;
        //    //try
        //    //{
        //    //    if (!(Application_Process.Is_Association_Developed))
        //    //        MessageBox.Show("Create Association to Meter");
        //    //    else
        //    //    {
        //    //        Application_Controller.IsIOBusy = true;
        //    //        Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_wifi(Convert.ToInt32(txt.Text));
        //    //        if (result == Data_Access_Result.Success)
        //    //              notifier = new Notification("Success", "NNNNNNNNNNNNNN Successfully");
        //    //    else
        //    //    throw new Exception(result.ToString());
        //    //}
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    notifier = new Notification("Error", "Error occurred while Setting NNNNNNNNNNNNNN, \n" + ex.Message);
        //    //}
        //    //finally
        //    //{
        //    //    Application_Controller.IsIOBusy = false;
        //    //}
        //}

        //private void btnWiFiClientPortGet_Click(object sender, EventArgs e)
        //{
        //    //Notification notification = null;
        //    //try
        //    //{
        //    //    if (!(Application_Process.Is_Association_Developed))
        //    //        notification = new Notification("Error", "Create Association to Meter");
        //    //    else
        //    //    {
        //    //        Application_Controller.IsIOBusy = true;
        //    //        int ch = Application_Controller.Param_Controller.GET_Param_();
        //    //        txt.Text = ch.ToString();
        //    //        notification = new Notification("Success", "NNNNNNNNNNN Read Successful");
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    notification = new Notification("Error", "NNNNNNNNNNNNN Read Error, \n" + ex.Message);
        //    //}
        //    //finally
        //    //{
        //    //    Application_Controller.IsIOBusy = false;
        //    //}
        //}

        //private void txtWiFiClientPort_TextChanged(object sender, EventArgs e)
        //{
        //    //if (LocalCommon.TextBox_validation(1, 65535, txtRFChannels))
        //    //    this.btnWiFiClientPortSet.Enabled = true;
        //    //else
        //    //    this.btnWiFiClientPortSet.Enabled = false;
        //}

        //#endregion  // --7 WiFi_Client_Port

        //#endregion // -- WiFi Configurations Control --

        //#region -- Display Configuration Controls --

        //#region --1 Serial Number

        //private void btnSerialNumberSet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;

        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Serial_Number(Convert.ToUInt32(txtSerialNumber.Text));
        //            if (result == Data_Access_Result.Success)
        //                notifier = new Notification("Success", "Set_Param_Serial_Number Successfully");
        //            else
        //                throw new Exception(result.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "Error occurred while Setting Set_Param_Serial_Number, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void btnSerialNumberGet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notifier = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            MeterSerialNumber meterSerialNumber = application_Controller.ConnectionController.GetMeterSerialNumber();
        //            txtSerialNumber.Text = meterSerialNumber.ToString();
        //            notifier = new Notification("Success", "GET_Param_Serial_Number Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "GET_Param_Serial_Number Read Error, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void txtSerialNumber_TextChanged(object sender, EventArgs e)
        //{
        //    if ((LocalCommon.isValidStringNumbers(10, txtSerialNumber))
        //        && (LocalCommon.TextBox_validation(0, 4294967295, txtSerialNumber)))
        //        this.btnSerialNumberSet.Enabled = true;
        //    else
        //        this.btnSerialNumberSet.Enabled = false;
        //}

        //#endregion  // --1 Serial Number

        //#region --2 LCD_Contrast 

        //private void btnLCDContrastSet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_LCD_Contrast(Convert.ToUInt32(txtLCDContrast.Text));
        //            if (result == Data_Access_Result.Success)
        //                notifier = new Notification("Success", "Set_Param_LCD_Contrast Successfully");
        //            else
        //                throw new Exception(result.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "Error occurred while Setting Set_Param_LCD_Contrast, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void btnLCDContrastGet_Click(object sender, EventArgs e)
        //{
        //    Notification notification = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notification = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            uint ch = Application_Controller.Param_Controller.GET_Param_LCD_Contrast();
        //            txtLCDContrast.Text = ch.ToString();
        //            notification = new Notification("Success", "GET_Param_LCD_Contrast Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Notification("Error", "GET_Param_LCD_Contrast Read Error, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void txtLCDContrast_TextChanged(object sender, EventArgs e)
        //{
        //    if (LocalCommon.TextBox_validation(10, 100, txtLCDContrast))
        //        this.btnLCDContrastSet.Enabled = true;
        //    else
        //        this.btnLCDContrastSet.Enabled = false;
        //}


        //#endregion  // --2 LCD_Contrast 

        //#region --3 Modem_Parameter 

        //private void btnModemParameterSet_Click(object sender, EventArgs e)
        //{
        //    //Notification notifier = null;
        //    //try
        //    //{
        //    //    if (!(Application_Process.Is_Association_Developed))
        //    //        MessageBox.Show("Create Association to Meter");
        //    //    else
        //    //    {
        //    //        Application_Controller.IsIOBusy = true;
        //    //        Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_(Convert.ToInt32(txt.Text));
        //    //        if (result == Data_Access_Result.Success)
        //    //          notifier = new Notification("Success", "NNNNNNNNNNNNNN Successfully");
        //    //    else
        //    //    throw new Exception(result.ToString());
        //    //}
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    notifier = new Notification("Error", "Error occurred while Setting NNNNNNNNNNNNNN, \n" + ex.Message);
        //    //}
        //    //finally
        //    //{
        //    //    Application_Controller.IsIOBusy = false;
        //    //}
        //}

        //private void btnModemParameterGet_Click(object sender, EventArgs e)
        //{
        //    //Notification notification = null;
        //    //try
        //    //{
        //    //    if (!(Application_Process.Is_Association_Developed))
        //    //        notification = new Notification("Error", "Create Association to Meter");
        //    //    else
        //    //    {
        //    //        Application_Controller.IsIOBusy = true;
        //    //        int ch = Application_Controller.Param_Controller.GET_Param_();
        //    //        txt.Text = ch.ToString();
        //    //        notification = new Notification("Success", "NNNNNNNNNNN Read Successful");
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    notification = new Notification("Error", "NNNNNNNNNNNNN Read Error, \n" + ex.Message);
        //    //}
        //    //finally
        //    //{
        //    //    Application_Controller.IsIOBusy = false;
        //    //}
        //}

        //private void txtModemParameter_TextChanged(object sender, EventArgs e)
        //{
        //    //if (LocalCommon.TextBox_validation(txt))
        //    //    this.btn.Enabled = true;
        //    //else
        //    //    this.btn.Enabled = false;
        //}

        //#endregion  // --3 Modem_Parameter 

        //#region --4 Meter_To_Read

        //private void btnMeterToReadSet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to Meter");
        //        else
        //        {
        //            byte type = Convert.ToByte(Convert.ToInt16(txtMeterToReadType.Text));
        //            byte[] msn = DLMS_Common.PrintableStringToByteArray(txtMeterToRead.Text);

        //            byte[] type_n_msn = DLMS_Common.Append_to_Start(msn, type); // adding meter type at startup

        //            Application_Controller.IsIOBusy = true;
        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Meter_To_Read(type_n_msn);
        //            if (result == Data_Access_Result.Success)
        //                notifier = new Notification("Success", "Set_Param_Meter_To_Read Successfully");
        //            else
        //                throw new Exception(result.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "Error occurred while Setting Set_Param_Meter_To_Read, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void btnMeterToReadGet_Click(object sender, EventArgs e)
        //{
        //    Notification notification = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notification = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            byte[] ch = Application_Controller.Param_Controller.GET_Param_Meter_To_Read();

        //            string mix = DLMS_Common.ArrayToPrintableString(ch);

        //            txtMeterToReadType.Text = ch[0].ToString(); //Convert.ToInt16(mix.Substring(0, 1)).ToString();//, System.Globalization.NumberStyles.HexNumber).ToString(); 
        //            txtMeterToRead.Text = mix.Substring(1, (mix.Length - 1));

        //            notification = new Notification("Success", "GET_Param_Meter_To_Read Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Notification("Error", "GET_Param_Meter_To_Read Read Error, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void txtMeterToRead_TextChanged(object sender, EventArgs e)
        //{
        //    if (LocalCommon.isValidStringNumbers(11, txtMeterToRead))
        //        this.btnMeterToReadSet.Enabled = true;
        //    else
        //        this.btnMeterToReadSet.Enabled = false;
        //}

        //private void txtMeterToReadType_TextChanged(object sender, EventArgs e)
        //{
        //    if (LocalCommon.TextBox_validation(1, 99, txtMeterToReadType))
        //        this.btnMeterToReadSet.Enabled = true;
        //    else
        //        this.btnMeterToReadSet.Enabled = false;
        //}

        //#endregion  // --4 Meter_To_Read

        //#region --5 Meter_Password 

        //private void btnMeterPasswordSet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to Meter");
        //        else
        //        {
        //            byte[] password = DLMS_Common.PrintableStringToByteArray(txtMeterPassword.Text);
        //            Application_Controller.IsIOBusy = true;
        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Meter_Password(password);
        //            if (result == Data_Access_Result.Success)
        //                notifier = new Notification("Success", "Set_Param_Meter_Password Successfully");
        //            else
        //                throw new Exception(result.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "Error occurred while Setting Set_Param_Meter_Password, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void btnMeterPasswordGet_Click(object sender, EventArgs e)
        //{
        //    Notification notification = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notification = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            byte[] ch = Application_Controller.Param_Controller.GET_Param_Meter_Password();
        //            txtMeterPassword.Text = DLMS_Common.ArrayToPrintableString(ch);
        //            notification = new Notification("Success", "GET_Param_Meter_Password Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Notification("Error", "GET_Param_Meter_Password Read Error, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void txtMeterPassword_TextChanged(object sender, EventArgs e)
        //{
        //    //??? what will pasword verification
        //    //if (LocalCommon.TextBox_validation(txt))
        //    //    this.btn.Enabled = true;
        //    //else
        //    //    this.btn.Enabled = false;
        //}

        //#endregion  // --5 Meter_Password 

        //#region --6 Data_To_read 

        //private void btnDataToReadSet_Click(object sender, EventArgs e)
        //{
        //    //Notification notifier = null;
        //    //try
        //    //{
        //    //    if (!(Application_Process.Is_Association_Developed))
        //    //        MessageBox.Show("Create Association to Meter");
        //    //    else
        //    //    {
        //    //        Application_Controller.IsIOBusy = true;
        //    //        Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Data_To_read(Convert.ToInt32(txtDataToRead.Text));
        //    //        if (result == Data_Access_Result.Success)
        //    //        notifier = new Notification("Success", "Set_Param_Data_To_read Successfully");
        //    //    else
        //    //    throw new Exception(result.ToString());
        //    //}
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    notifier = new Notification("Error", "Error occurred while Setting Set_Param_Data_To_read, \n" + ex.Message);
        //    //}
        //    //finally
        //    //{
        //    //    Application_Controller.IsIOBusy = false;
        //    //}
        //}

        //private void btnDataToReadGet_Click(object sender, EventArgs e)
        //{
        //    //Notification notification = null;
        //    //try
        //    //{
        //    //    if (!(Application_Process.Is_Association_Developed))
        //    //        notification = new Notification("Error", "Create Association to Meter");
        //    //    else
        //    //    {
        //    //        Application_Controller.IsIOBusy = true;
        //    //        int ch = Application_Controller.Param_Controller.GET_Param_Data_To_read();
        //    //        txtDataToRead.Text = ch.ToString();
        //    //        notification = new Notification("Success", "GET_Param_Data_To_read Read Successful");
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    notification = new Notification("Error", "GET_Param_Data_To_read Read Error, \n" + ex.Message);
        //    //}
        //    //finally
        //    //{
        //    //    Application_Controller.IsIOBusy = false;
        //    //}
        //}

        //private void txtDataToRead_TextChanged(object sender, EventArgs e)
        //{
        //    //if (LocalCommon.TextBox_validation(txtDataToRead))
        //    //    this.btnDataToReadSet.Enabled = true;
        //    //else
        //    //    this.btnDataToReadSet.Enabled = false;
        //}

        //#endregion  // --6 Data_To_read  

        //#region --7 Display_Windows 

        //private void btnDisplayWindowsSet_Click(object sender, EventArgs e)
        //{
        //    //Notification notifier = null;
        //    //try
        //    //{
        //    //    if (!(Application_Process.Is_Association_Developed))
        //    //        MessageBox.Show("Create Association to Meter");
        //    //    else
        //    //    {
        //    //        Application_Controller.IsIOBusy = true;
        //    //        Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_D(Convert.ToInt32(txt.Text));
        //    //        if (result == Data_Access_Result.Success)
        //    //        notifier = new Notification("Success", "NNNNNNNNNNNNNN Successfully");
        //    //    else
        //    //           throw new Exception(result.ToString());
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    notifier = new Notification("Error", "Error occurred while Setting NNNNNNNNNNNNNN, \n" + ex.Message);
        //    //}
        //    //finally
        //    //{
        //    //    Application_Controller.IsIOBusy = false;
        //    //}
        //}

        //private void btnDisplayWindowsGet_Click(object sender, EventArgs e)
        //{
        //    //Notification notification = null;
        //    //try
        //    //{
        //    //    if (!(Application_Process.Is_Association_Developed))
        //    //        notification = new Notification("Error", "Create Association to Meter");
        //    //    else
        //    //    {
        //    //        Application_Controller.IsIOBusy = true;
        //    //        int ch = Application_Controller.Param_Controller.GET_Param_();
        //    //        txt.Text = ch.ToString();
        //    //        notification = new Notification("Success", "NNNNNNNNNNN Read Successful");
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    notification = new Notification("Error", "NNNNNNNNNNNNN Read Error, \n" + ex.Message);
        //    //}
        //    //finally
        //    //{
        //    //    Application_Controller.IsIOBusy = false;
        //    //}
        //}

        //private void txtDisplayWindows_TextChanged(object sender, EventArgs e)
        //{
        //    //if (LocalCommon.TextBox_validation(txt))
        //    //    this.btn.Enabled = true;
        //    //else
        //    //    this.btn.Enabled = false;
        //}

        //#endregion  // --7 Display_Windows  

        //#region --8 Buzzer_Settings

        //private void btnBuzzerSettingsSet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Buzzer_Settings(Convert.ToBoolean(cmbBuzzerSettings.SelectedIndex));
        //            if (result == Data_Access_Result.Success)
        //                notifier = new Notification("Success", "Set_Param_Buzzer_Settings Successfully");
        //            else
        //                throw new Exception(result.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "Error occurred while Setting Set_Param_Buzzer_Settings, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void btnBuzzerSettingsGet_Click(object sender, EventArgs e)
        //{
        //    Notification notification = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notification = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            bool ch = Application_Controller.Param_Controller.GET_Param_Buzzer_Settings();
        //            cmbBuzzerSettings.SelectedIndex = (ch) ? 1 : 0; // Enable, if true. Else, Disable...
        //            notification = new Notification("Success", "GET_Param_Buzzer_Settings Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Notification("Error", "GET_Param_Buzzer_Settings Read Error, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //#endregion  // --8 Buzzer_Settings

        //#region --9 Read_Humidity

        //private void btnReadHumiditySet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Humidity(Convert.ToBoolean(cmbReadHumidity.SelectedIndex));
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

        //private void btnReadHumidityGet_Click(object sender, EventArgs e)
        //{
        //    Notification notification = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notification = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            bool ch = Application_Controller.Param_Controller.GET_Param_Humidity();
        //            cmbReadHumidity.SelectedIndex = (ch) ? 1 : 0; // Enable, if true. Else, Disable...
        //            notification = new Notification("Success", "GET_Param_Humidity Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Notification("Error", "GET_Param_Humidity Read Error, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //#endregion  // --9 Read_Humidity

        //#region --10 Temperature_Settings 

        //private void btnTempratureSettingsSet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_Temperature_setting(Convert.ToByte(cmbTemperatureSettings.SelectedIndex));
        //            if (result == Data_Access_Result.Success)
        //                notifier = new Notification("Success", "Set_Param_Temperature_setting Successfully");
        //            else
        //                throw new Exception(result.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "Error occurred while Setting Set_Param_Temperature_setting, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //private void btnTempratureSettingsGet_Click(object sender, EventArgs e)
        //{
        //    Notification notification = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notification = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            byte ch = Application_Controller.Param_Controller.GET_Param_Temperature_setting();
        //            cmbTemperatureSettings.SelectedIndex = ch;// (ch) ? 1 : 0; // Enable, if true. Else, Disable...
        //            notification = new Notification("Success", "GET_Param_Temperature_setting Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Notification("Error", "GET_Param_Temperature_setting Read Error, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        //#endregion  // --10 Temperature_Settings 

        //#region -- USB_Parameters 

        //private void btnUSBParamsSet_Click(object sender, EventArgs e)
        //{
        //    Notification notifier = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            MessageBox.Show("Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_USB_Parameters(Convert.ToByte(cmbUSBParams.SelectedIndex));
        //            if (result == Data_Access_Result.Success)
        //                notifier = new Notification("Success", "Set_Param_USB_Parameters Successfully");
        //            else
        //                throw new Exception(result.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notifier = new Notification("Error", "Error occurred while Setting Set_Param_USB_Parameters, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}


        //private void btnUSBParamsGet_Click(object sender, EventArgs e)
        //{
        //    Notification notification = null;
        //    try
        //    {
        //        if (!(Application_Process.Is_Association_Developed))
        //            notification = new Notification("Error", "Create Association to Meter");
        //        else
        //        {
        //            Application_Controller.IsIOBusy = true;
        //            USB_Parameter ch = Application_Controller.Param_Controller.GET_Param_USB_Parameters();
        //            cmbUSBParams.SelectedIndex = (byte)ch;// (ch) ? 1 : 0; // Enable, if true. Else, Disable...
        //            notification = new Notification("Success", "GET_Param_USB_Parameters Read Successful");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification = new Notification("Error", "GET_Param_USB_Parameters Read Error, \n" + ex.Message);
        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}



        //#endregion  // 

        //#endregion // Display Configuration Controls
        
        //internal static void ShowToGUI_EnergyMizerParams(Param_Energy_Mizer param_Energy_Mizer)
        //{
        //    if (param_Energy_Mizer != null)
        //    {
        //        //txtRFChannels.Text = param_Energy_Mizer.RFChannel.ToString();
        //        //txtChannelFilterBW.Text = param_Energy_Mizer.ChannelFilterWB.ToString();
        //        //txtCostParameters.Text = param_Energy_Mizer.CostParameters.ToString();
        //    }
        //}

        //#endregion // Energy Mizer Configuration

        #endregion
    }
}
