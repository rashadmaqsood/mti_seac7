using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using DLMS;
using GUI;
using SmartEyeControl_7;
using SmartEyeControl_7.ApplicationGUI.ucCustomControl;
using System.Drawing;
using SharedCode.Controllers;
using SharedCode.Comm.HelperClasses;
using AccurateOptocomSoftware.comm;
using SharedCode.Comm.DataContainer;
using DLMS.Comm;
using SharedCode.Comm.Param;
using SEAC.Common;
using SharedCode.Common;
using System.Net;
using System.Text.RegularExpressions;

namespace ucCustomControl
{
    public partial class pnlDebugging : UserControl
    {
        public TextBox txtMessageViewer;
        public ListBox lbxObisCodesViewer;
        public TextBox txtCommViewer;

        // public Button btnReadObisCodes;
        // public Button btnC2Coutput;
        // public Button btnC2CResponse;
        // public Button btnClearOutput;
        private ApplicationController application_Controller;

        public Button btnReadObisCodes;
        public Button btnC2Coutput;
        public Button btnC2CResponse;
        public Button btnClearOutput;

        public TreeView trvObisCodesViewer;
        private DataGridView dgv_TestOutput;
        int listItemsCount = 0; int currentRow = 0;
        //================================================================
        //================================================================
        private DLMS_Application_Process Application_Process;
        private ConnectionManager connectionManager;
        private ConnectionController ConnController;
        private ApplicationProcess_Controller AP_Controller;

        private ParameterController Param_Controller;
        private InstantaneousController Instantanous_Controller;

        private BackgroundWorker BckWorkerThread;
        private ProgressDialog progressDialog;

        private Testing_Debug DebugClass = new Testing_Debug();
        private Instantaneous_Class Instantaneous_Class_obj = new Instantaneous_Class();

        private bool isValidateed = false;

        private Param_St_EEPRawRead Current_Memory_Reference = null;
        private Param_St_EEPRawRead Last_Memory_Reference = null;
        private EEPRawReadData Current_DataRead = null;

        private List<Param_St_EEPRawRead> MemoryMAP_List = null;
        private List<EEPRawReadData> DataRead_List = null;
        private Fuison_IO_StateList IO_StateList = null;
        
        int getCount = 1;
        int currentCount = 0;

        public bool IsMemoryReadCompleted { get; set; }
        /// <summary>
        /// Testing Class 
        /// </summary>
        public ApplicationController Application_Controller
        {
            get
            {
                // if (application_Controller == null)
                //     throw new Exception("Application not Initialized properly");
                return application_Controller;
            }
            set
            {
                if (value != application_Controller)
                {
                    application_Controller = value;
                    Application_Controller.PropertyChanged += new PropertyChangedEventHandler(Application_Controller_PropertyChanged);
                }
            }
        }

        internal bool WordWrapp_FormatOnly
        {
            get;
            set;
        }

        //================================================================
        //================================================================
        public pnlDebugging()
        {
            InitializeComponent();
            ApplicationLookAndFeel.UseTheme(this);

            txtMessageViewer = txtOutPutWindow;
            lbxObisCodesViewer = lbxObisCodes;
            txtCommViewer = txtICommViewer;
            btnReadObisCodes = btnReadObis;
            btnC2Coutput = btnIC2Coutput;
            btnC2CResponse = btnIC2CComm;
            btnClearOutput = btnIClearOutput;
            trvObisCodesViewer = trvIObisCodes;

            Current_Memory_Reference = new Param_St_EEPRawRead();
            Show_Current_MemoryReference(Current_Memory_Reference);

            MemoryMAP_List = new List<Param_St_EEPRawRead>();
            DataRead_List = new List<EEPRawReadData>();
            IO_StateList = new Fuison_IO_StateList();
            ShowToGUIIOStates(IO_StateList);
            cmpProtocol.Items.AddRange(Enum.GetNames(typeof(Protocol_Gate)));
            cmpProtocol.SelectedIndex = 0;

        }

        private void lbxObisCodes_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void btnIClearcomm_Click(object sender, EventArgs e)
        {
            txtICommViewer.Text = "";
        }

        private void btnC2CDecodedOP_Click(object sender, EventArgs e)
        {
            LocalCommon.TextCopy2Clipboard(txtIDecodedOutput);
            txtIDecodedOutput.Select(0, 0);
        }

        private void btnIClrDecodedOP_Click(object sender, EventArgs e)
        {
            txtIDecodedOutput.Text = "";
        }

        private void chkEnableSelectiveAccess_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkEnableSelectiveAccess.Checked)
            {
                txtFromEntry.Enabled = false;
                txtToEntry.Enabled = false;
                txtFromSelectedVal.Enabled = false;
                txtToSelectedVal.Enabled = false;
            }
            else
            {
                txtFromEntry.Enabled = true;
                txtToEntry.Enabled = true;
                txtFromSelectedVal.Enabled = true;
                txtToSelectedVal.Enabled = true;
            }
        }

        private void pnlDebugging_Load(object sender, EventArgs e)
        {
            try
            {
                if (Application_Controller != null)
                {
                    // Initialize Debug Pager
                    //================================================================
                    //================================================================
                    AP_Controller = Application_Controller.Applicationprocess_Controller;
                    Application_Process = Application_Controller.Applicationprocess_Controller.ApplicationProcess;
                    ConnController = Application_Controller.ConnectionController;
                    Instantanous_Controller = Application_Controller.InstantaneousController;
                    Param_Controller = Application_Controller.Param_Controller;
                    connectionManager = Application_Controller.ConnectionManager;
                    //================================================================
                    //================================================================
                }

                String OBISName = string.Empty;
                StOBISCode OBISCode = Get_Index.Dummy;
                var OBISCodeValues = Enum.GetValues(typeof(Get_Index)) as ulong[];

                for (int index = 0; index < OBISCodeValues.Length; index++)
                {
                    OBISCode.OBIS_Value = OBISCodeValues[index];

                    if (OBISCode.IsValidate)
                    {
                        string TxtToAdd = Get_OBISStringLabel(OBISCode);
                        if (!list_possible.Items.Contains(TxtToAdd))
                            list_possible.Items.Add(TxtToAdd);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine(string.Format("{0} OBIS Code Not Valid", OBISCode.ToString()));
                    }
                }

                List<string> Lst_DatdaTypes = new List<string>();
                DataTypes x = DataTypes._A00_Null;
                byte[] a = (byte[])Enum.GetValues(x.GetType());

                foreach (byte b in a)
                {
                    Lst_DatdaTypes.Add(((DataTypes)b).ToString());
                }
                foreach (string s in Lst_DatdaTypes)
                {
                    cbxDataTypesWR.Items.Add(s);
                }
                cbxDataTypesWR.SelectedIndex = 0;
                chkEnableSelectiveAccess.Checked = false;

                try
                {
                    getCount = Convert.ToInt32(txt_getCount.Text);
                }
                catch (Exception ex)
                {
                    getCount = 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Initializing Debug Interface Properly", ex);
            }
        }


        //private void btn_getLoadProfile_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Application_Controller.IsIOBusy = true;
        //        LoadProfileController LPController = new LoadProfileController();
        //        LoadProfileData loadProfileData = new LoadProfileData();
        //        List<LoadProfileItem> loadItem = new List<LoadProfileItem>();

        //        loadProfileData = LPController.Get_LoadProfileData();
        //        loadItem = loadProfileData.ChannelsInstances;

        //        foreach (var item in loadItem)
        //        {
        //            MessageBox.Show(item.ToString());
        //        }

        //    }
        //    finally
        //    {
        //        Application_Controller.IsIOBusy = false;
        //    }
        //}

        private void btn_debugString_Click(object sender, EventArgs e)
        {
            try
            {
                if (Application_Process.Is_Association_Developed)
                {
                    Application_Controller.IsIOBusy = true;
                    txt_debugString.Text = "";
                    lbl_DebugStr_lenght.Text = "---";
                    Application.DoEvents();
                    byte[] Byte_Array = new byte[400];
                    Byte_Array = Instantanous_Controller.GETArray_Any(Get_Index.Debugging, 2);
                    lbl_DebugStr_lenght.Text = Byte_Array.Length.ToString();
                    if (rbASCII.Checked) txt_debugString.Text = LocalCommon.Byte_Arrayto_PlainString(Byte_Array);
                    else txt_debugString.Text = DLMS_Common.ArrayToHexString(Byte_Array);

                }
                else
                {
                    MessageBox.Show("Create Association First");
                }
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btn_Get_major_alram_counter_Click(object sender, EventArgs e)
        {

            bgw_GETAlarmCounter.RunWorkerAsync();
            pb1.Visible = true;
        }

        private void btn_Set_major_alram_counter_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to Meter-Management via Management");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    if (LocalCommon.TextBox_validation(0, 65000, txt_major_alarm_profile))
                    {
                        double x = Convert.ToDouble(txt_major_alarm_profile.Text);
                        Data_Access_Result result = Param_Controller.SET_any_class1(x, Get_Index.Major_Alarm_Counter, DataTypes._A12_long_unsigned, 2);
                        MessageBox.Show(result.ToString());
                    }
                }
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }

        }

        private void Application_Controller_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                // Okay IsIOBusy Status
                if ("IsIOBusy".Equals(e.PropertyName) && sender is ApplicationController)
                {
                    UpdateReadWriteStatus(Application_Controller.IsIOBusy);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void UpdateReadWriteStatus(bool IsReadWriteBusy)
        {
            #region Functions_Body

            try
            {
                // Disable IO Read/Write Button 
                if (IsReadWriteBusy)
                {
                    List<object> Controls = new List<object>();
                    foreach (var item in this.Controls)
                    {
                        Controls.Add(item);
                    }
                    while (Controls.Count > 0)
                    {
                        Control control = (Control)Controls[0];
                        Controls.Remove(control);
                        foreach (var item in control.Controls)
                        {
                            Controls.Add(item);
                        }

                        if (control is KryptonButton)
                        {
                            ((KryptonButton)control).Enabled = false;
                        }
                        else if (control is Button)
                        {
                            ((Button)control).Enabled = false;
                        }
                        else if (control is RadioButton)
                        {
                            ((RadioButton)control).Enabled = false;
                        }
                        else if (control is CheckBox)
                        {
                            ((CheckBox)control).Enabled = false;
                        }

                    }

                }
                // Enable Read Write btns
                else
                {
                    List<object> Controls = new List<object>();
                    foreach (var item in this.Controls)
                    {
                        Controls.Add(item);
                    }
                    while (Controls.Count > 0)
                    {
                        Control control = (Control)Controls[0];
                        Controls.Remove(control);
                        foreach (var item in control.Controls)
                        {
                            Controls.Add(item);
                        }
                        if (control is KryptonButton)
                        {
                            // Keep Disable these Buttons

                            // if (control == btn_Read_Memory_MAP ||
                            //     control == btn_Add_MemRef ||
                            //     control == btn_Export_File ||
                            //     control == btn_Import_File)
                            // {
                            //     continue;
                            // }
                            // else

                            ((KryptonButton)control).Enabled = true;
                        }
                        else if (control is Button)
                        {
                            ((Button)control).Enabled = true;
                        }
                        else if (control is RadioButton)
                        {
                            ((RadioButton)control).Enabled = true;
                        }
                        else if (control is CheckBox)
                        {
                            ((CheckBox)control).Enabled = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                {
                    List<object> Controls = new List<object>();
                    foreach (var item in this.Controls)
                    {
                        Controls.Add(item);
                    }
                    while (Controls.Count > 0)
                    {
                        Control control = (Control)Controls[0];
                        Controls.Remove(control);
                        foreach (var item in control.Controls)
                        {
                            Controls.Add(item);
                        }
                        if (control is KryptonButton)
                        {
                            //Keep Disabled these Buttons

                            // if (control == btn_Read_Memory_MAP ||
                            //     control == btn_Add_MemRef ||
                            //     control == btn_Export_File ||
                            //     control == btn_Import_File)
                            // {
                            //     continue;
                            // }
                            // else

                            ((KryptonButton)control).Enabled = true;
                        }
                        else if (control is Button)
                        {
                            ((Button)control).Enabled = true;
                        }
                        else if (control is RadioButton)
                        {
                            ((RadioButton)control).Enabled = true;
                        }
                        else if (control is CheckBox)
                        {
                            ((CheckBox)control).Enabled = true;
                        }
                    }

                }
            }
            finally
            {
                btn_StopProcess.Enabled = true;
            }

            #endregion
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process.Start(@"D:\Wakeup Generator\bin\Debug\Wakeup Generator.exe");
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Wakeup Generator.exe");
        }



        private void bgw_GETAlarmCounter_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                {
                    Notification notifier = new Notification("Association Error", "Create Association to Meter-Management via Management");
                }
                else
                {
                    Application_Controller.IsIOBusy = true;
                    txt_major_alarm_profile.Text = Param_Controller.GETDouble_Any(Get_Index.Major_Alarm_Counter, 2).ToString();
                }
            }
            finally
            {

            }
        }

        private void bgw_GETAlarmCounter_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Application_Controller.IsIOBusy = false;
            pb1.Visible = false;

        }

        private void btn_readErrors_Click(object sender, EventArgs e)
        {
            try
            {
                if (Application_Process.Is_Association_Developed)
                {
                    Application_Controller.IsIOBusy = true;
                    txt_debugString.Text = "";
                    lbl_DebugStr_lenght.Text = "---";
                    Application.DoEvents();
                    byte[] Byte_Array = new byte[400];
                    Byte_Array = Instantanous_Controller.GETArray_Any(Get_Index.Read_errors, 2);
                    lbl_DebugStr_lenght.Text = Byte_Array.Length.ToString();
                    txt_debugString.Text = DLMS_Common.ArrayToHexString(Byte_Array);

                }
                else
                {
                    MessageBox.Show("Create Association First");
                }
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btn_readCautions_Click(object sender, EventArgs e)
        {
            try
            {
                if (Application_Process.Is_Association_Developed)
                {
                    Application_Controller.IsIOBusy = true;
                    txt_debugString.Text = "";
                    lbl_DebugStr_lenght.Text = "---";
                    Application.DoEvents();
                    byte[] Byte_Array = new byte[400];
                    Byte_Array = Instantanous_Controller.GETArray_Any(Get_Index.Read_Cautions, 2);
                    lbl_DebugStr_lenght.Text = Byte_Array.Length.ToString();
                    txt_debugString.Text = DLMS_Common.ArrayToHexString(Byte_Array);

                }
                else
                {
                    MessageBox.Show("Create Association First");
                }
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btn_T1_Click(object sender, EventArgs e)
        {
            foreach (string item in list_Selected.Items)
            {
                list_possible.Items.Add(item);
            }

            list_Selected.Items.Clear();
            foreach (string item in list_possible.Items)
            {
                if (item.Contains("Cumulative_Tariff1"))
                {
                    list_Selected.Items.Add(item);
                }
            }

            foreach (string item in list_Selected.Items)
            {
                list_possible.Items.Remove(item);
            }
        }

        private void btn_T2_Click(object sender, EventArgs e)
        {
            foreach (string item in list_Selected.Items)
            {
                list_possible.Items.Add(item);
            }

            list_Selected.Items.Clear();
            foreach (string item in list_possible.Items)
            {
                if (item.Contains("Cumulative_Tariff2"))
                {
                    list_Selected.Items.Add(item);
                }
            }
            foreach (string item in list_Selected.Items)
            {
                list_possible.Items.Remove(item);
            }
        }

        private void btn_T3_Click(object sender, EventArgs e)
        {
            foreach (string item in list_Selected.Items)
            {
                list_possible.Items.Add(item);
            }

            list_Selected.Items.Clear();
            foreach (string item in list_possible.Items)
            {
                if (item.Contains("Cumulative_Tariff3"))
                {
                    list_Selected.Items.Add(item);
                }
            }

            foreach (string item in list_Selected.Items)
            {
                list_possible.Items.Remove(item);
            }
        }

        private void btn_T4_Click(object sender, EventArgs e)
        {
            foreach (string item in list_Selected.Items)
            {
                list_possible.Items.Add(item);
            }

            list_Selected.Items.Clear();
            foreach (string item in list_possible.Items)
            {
                if (item.Contains("Cumulative_Tariff4"))
                {
                    list_Selected.Items.Add(item);
                }
            }
            foreach (string item in list_Selected.Items)
            {
                list_possible.Items.Remove(item);
            }
        }

        private void btn_TL_Click(object sender, EventArgs e)
        {
            foreach (string item in list_Selected.Items)
            {
                list_possible.Items.Add(item);
            }
            list_Selected.Items.Clear();
            foreach (string item in list_possible.Items)
            {
                if (item.Contains("Cumulative_TariffTL"))
                {
                    list_Selected.Items.Add(item);
                }
            }
            foreach (string item in list_Selected.Items)
            {
                list_possible.Items.Remove(item);
            }
        }

        private void btn_Get_major_alram_counter_Click_1(object sender, EventArgs e)
        {

            bgw_GETAlarmCounter.RunWorkerAsync();
            pb1.Visible = true;

            /* get
             * 
             * 
             */
        }

        private void btn_Set_major_alram_counter_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to Meter-Management via Management");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    if (LocalCommon.TextBox_validation(0, 65000, txt_major_alarm_profile))
                    {
                        double x = Convert.ToDouble(txt_major_alarm_profile.Text);
                        Data_Access_Result result = Param_Controller.SET_any_class1(x, Get_Index.Major_Alarm_Counter, DataTypes._A12_long_unsigned, 2);
                        MessageBox.Show(result.ToString());
                    }
                }
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btn_ClearGrid_Click_1(object sender, EventArgs e)
        {
            timer_testing.Enabled = false;
            initializeGrid();
        }

        private void btn_getFirmwareInfo_Click(object sender, EventArgs e)
        {
            try
            {
                #region Commented Code_Section

                //byte[] byte_Array = new byte[68];
                //FirmwareInfo info_firmware = new FirmwareInfo();
                //byte_Array = Instantanous_Controller.GET_Any_ByteArray(Get_Index.Firmware_ID_2, 0, 1);
                //for (int i = 0; i < byte_Array.Length; i++)
                //{
                //    byte_Array[i] = (byte)(byte_Array[i] ^ (0x55));
                //}
                //int traverser = 0;

                //info_firmware.modelID[0] = (byte_Array[traverser]);
                //traverser++;


                //for (int i = 0; i < info_firmware.model.Length; i++)
                //{
                //    info_firmware.model[i] = byte_Array[traverser + i];

                //}
                //traverser += info_firmware.model.Length;


                //for (int i = 0; i < info_firmware.company_code.Length; i++)
                //{
                //    info_firmware.company_code[i] = byte_Array[traverser + i];

                //}
                //traverser += info_firmware.company_code.Length;


                //for (int i = 0; i < info_firmware.serial_no.Length; i++)
                //{
                //    info_firmware.serial_no[i] = byte_Array[traverser + i];
                //}
                //traverser += info_firmware.serial_no.Length;


                //for (int i = 0; i < info_firmware.date.Length; i++)
                //{
                //    info_firmware.date[i] = byte_Array[traverser + i];

                //}
                //traverser += info_firmware.date.Length;


                //for (int i = 0; i < info_firmware.time.Length; i++)
                //{
                //    info_firmware.time[i] = byte_Array[traverser + i];

                //}
                //traverser += info_firmware.time.Length;

                //for (int i = 0; i < info_firmware.release_id.Length; i++)
                //{
                //    info_firmware.release_id[i] = byte_Array[traverser + i];

                //}
                //traverser += info_firmware.release_id.Length;


                //for (int i = 0; i < info_firmware.version.Length; i++)
                //{
                //    info_firmware.version[i] = byte_Array[traverser + i];

                //}
                //traverser += info_firmware.version.Length;


                //for (int i = 0; i < info_firmware.info_1.Length; i++)
                //{
                //    info_firmware.info_1[i] = byte_Array[traverser + i];

                //}
                //traverser += info_firmware.info_1.Length;

                //for (int i = 0; i < info_firmware.info_2.Length; i++)
                //{
                //    info_firmware.info_2[i] = byte_Array[traverser + i];

                //}
                //traverser += info_firmware.info_2.Length;


                //for (int i = 0; i < info_firmware.rfu.Length; i++)
                //{
                //    info_firmware.rfu[i] = byte_Array[traverser + i];

                //}
                //traverser += info_firmware.rfu.Length;

                //for (int i = 0; i < info_firmware.features.Length; i++)
                //{
                //    info_firmware.features[i] = byte_Array[traverser + i];

                //}
                //traverser += info_firmware.features.Length;

                //for (int i = 0; i < info_firmware.dlms_version.Length; i++)
                //{
                //    info_firmware.dlms_version[i] = byte_Array[traverser + i];
                //} 

                #endregion

                St_FirmwareInfo info_firmware = Param_Controller.GetStFirmwareInfo();

                // txt_firmareInfo.Text += "Model ID: " + Encoding.ASCII.GetString(info_firmware.modelID) + "\r\n";
                txt_firmareInfo.Text += "Model ID: " + info_firmware.Model_ID + "\r\n";
                txt_firmareInfo.Text += "Model: " + info_firmware.Model + "\r\n";
                txt_firmareInfo.Text += "Company Code: " + info_firmware.CompanyCode + "\r\n";
                txt_firmareInfo.Text += "Serial Number: " + info_firmware.MSN_Info.MSN + "\r\n";
                txt_firmareInfo.Text += "Compile Date Time: " + info_firmware.ReleaseDateTime + "\r\n";

                // txt_firmareInfo.Text += "Time: " + info_firmware.ReleaseDateTime + "\r\n";
                txt_firmareInfo.Text += "Release ID: " + info_firmware.Release_ID + "\r\n";
                txt_firmareInfo.Text += "Version: " + info_firmware.Version + "\r\n";
                txt_firmareInfo.Text += "RFU: " + info_firmware.RFU + "\r\n";
                txt_firmareInfo.Text += "DLMS Version: " + info_firmware.DLMS_Version + "\r\n";

                Application.DoEvents();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void rbASCII_CheckedChanged(object sender, EventArgs e)
        {
            if (rbASCII.Checked) txt_debugString.Text = LocalCommon.Byte_Arrayto_PlainString(LocalCommon.String_to_Hex_array(txt_debugString.Text));
        }

        private void rbHEX_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHEX.Checked) txt_debugString.Text = LocalCommon.ArrayToHexString(Encoding.ASCII.GetBytes(txt_debugString.Text));
        }

        #region RAW_Data_Viewer

        private void Raw_Data_Viewer_Enter(object sender, EventArgs e)
        {
            try
            {
                //string Dir = string.Empty;
                //Dir = LocalCommon.GetApplicationConfigsDirectory();

                //string file_URL = Dir + @"\MemoryReferences\Memory_Ref.xml";
                //FileInfo info = new FileInfo(file_URL);
                //if ((MemoryMAP_List == null || MemoryMAP_List.Count <= 0) &&
                //    info.Exists)
                //    btn_Import_File_Click(btn_Import_File, new EventArgs());
            }
            catch
            {
            }
        }

        private void cmb_EMP_No_TextChanged(object sender, EventArgs e)
        {
            isValidateed = false;
            try
            {
                byte EPM_Num = 0;

                isValidateed = byte.TryParse(cmb_EMP_No.Text, out EPM_Num);

                if (isValidateed)
                    Current_Memory_Reference.EPM_Number = EPM_Num;

            }
            finally { }
        }

        private void cmb_EMP_No_SelectedValueChanged(object sender, EventArgs e)
        {
            isValidateed = false;
            try
            {
                Current_Memory_Reference.EPM_Number = Convert.ToByte(cmb_EMP_No.SelectedIndex);
                isValidateed = true;
            }
            finally { }
        }


        private void txt_BaseAddress_TextChanged(object sender, EventArgs e)
        {
            ushort base_Address = 0;
            isValidateed = false;
            try
            {
                //Parse Number In Hex Format
                if (ushort.TryParse(txt_BaseAddress.Text, System.Globalization.NumberStyles.HexNumber, Application.CurrentCulture,
                    out base_Address))
                {
                    if (base_Address >= 0 && base_Address <= 0xFFFF)
                    {
                        Current_Memory_Reference.Address = base_Address;
                        isValidateed = true;
                    }
                    else
                        throw new FormatException("Number is not in Valid Range [0000-FFFF]");
                }
                else
                    throw new FormatException("Number is not in Valid Format or range [0000-FFFF]");
            }
            catch (Exception ex)
            {

                Notification notifier = new Notification("Incorrect ROM Base Address",
                                        String.Format("Memory Base Address is not valid,", ex.Message));

                errorProvider.SetError(txt_BaseAddress, "Incorrect ROM Base Address");
            }
            finally
            {
                if (isValidateed)
                {
                    errorProvider.SetError(txt_BaseAddress, string.Empty);
                }
            }
        }


        private void txt_Length_TextChanged(object sender, EventArgs e)
        {
            ushort length = 0;
            isValidateed = false;
            try
            {
                //Parse Number In Decimal Number Format
                if (ushort.TryParse(txt_Length.Text, System.Globalization.NumberStyles.Integer, Application.CurrentCulture,
                    out length))
                {

                    if (length >= 0 && length <= ushort.MaxValue)
                    {
                        isValidateed = true;
                        Current_Memory_Reference.Length = length;
                    }
                    else
                        throw new FormatException("RAW Data Length not in Valid Range [0-1024] Byte");

                }
                //Parse Number In Hex Number Format
                else if (ushort.TryParse(txt_Length.Text, System.Globalization.NumberStyles.HexNumber, Application.CurrentCulture,
                    out length))
                {

                    if (length >= 0 && length <= 0x0400)
                    {
                        isValidateed = true;
                        Current_Memory_Reference.Length = length;
                    }
                    else
                        throw new FormatException("RAW Data Length not in Valid Range [0000-0400]");

                }
                else
                    throw new FormatException("RAW Data Length not in Valid Range [0-1024] Byte");
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Incorrect Raw Data Length",
                                        String.Format("Incorrect Raw Data Length to read,", ex.Message));

                errorProvider.SetError(txt_BaseAddress, "Incorrect Raw Data Length");
            }
            finally
            {
                if (isValidateed)
                {
                    errorProvider.SetError(txt_BaseAddress, string.Empty);
                }
            }
        }


        private void txt_TAG_TextChanged(object sender, EventArgs e)
        {
            string TAG_STR = String.Empty;
            isValidateed = false;
            try
            {
                TAG_STR = txt_TAG.Text;
                //Validate TAG STR here
                if (String.IsNullOrEmpty(TAG_STR) || (TAG_STR.Length > 0 && TAG_STR.Length <= 20))
                {
                    isValidateed = true;
                    Current_Memory_Reference.TAG = TAG_STR;
                }
                else
                    throw new FormatException("Descriptive TAG String not in valid text range");
            }
            catch (Exception ex)
            {

                Notification notifier = new Notification("Incorrect TAG",
                                        String.Format("TAG should be Max 20 character long", ex.Message));

                errorProvider.SetError(txt_BaseAddress, "Incorrect TAG");
            }
            finally
            {
                if (isValidateed)
                {
                    errorProvider.SetError(txt_BaseAddress, string.Empty);
                }
            }
        }


        private void chk_EditorFormat_CheckedChanged(object sender, EventArgs e)
        {
            WordWrapp_FormatOnly = chk_EditorFormat.Checked;
        }


        private void chk_ALL_CheckedChanged(object sender, EventArgs e)
        {
            bool Current_State = chk_ALL.Checked;
            for (int index = 0; index < chk_Mem_References.Items.Count; index++)
            {
                chk_Mem_References.SetItemChecked(index, Current_State);
            }
        }


        private void btn_Set_CurrentMemRef_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to Meter Management via Management");
                else
                {
                    Application_Controller.IsIOBusy = true;

                    if (true)
                    {
                        if (Current_Memory_Reference == null ||
                           !(Current_Memory_Reference.Address >= 0 && Current_Memory_Reference.Address <= ushort.MaxValue) ||
                           !(Current_Memory_Reference.Length > 0 && Current_Memory_Reference.Length <= 1024))
                            throw new FormatException("Current Memory Reference Not Initialized Correctly");

                        Data_Access_Result Result = Application_Controller.Param_Controller.Set_Param_StEEPRawRead(Current_Memory_Reference);
                        Last_Memory_Reference = new Param_St_EEPRawRead(Current_Memory_Reference);

                        if (Result == Data_Access_Result.Success)
                            notifier = new Notification("Success", "Current Memory Reference Set Successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Parameterization Error", "Error occurred while Parameterizing Param_St_EEPRawRead," + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }


        private void btn_GET_CurrentMemRef_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to Meter Management via Management");
                else
                {
                    Application_Controller.IsIOBusy = true;

                    if (true)
                    {
                        if (Current_Memory_Reference == null)
                            Current_Memory_Reference = new Param_St_EEPRawRead();

                        Current_Memory_Reference = Application_Controller.Param_Controller.Get_Param_StEEPRawRead(Current_Memory_Reference);

                        Show_Current_MemoryReference(Current_Memory_Reference);
                        notifier = new Notification("Success", "Current Memory Reference read Successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Parameterization Error", "Error occurred while reading Parameter Param_St_EEPRawRead," + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }


        private void Show_Current_MemoryReference(Param_St_EEPRawRead _Param_St_EEPRawRead)
        {
            try
            {
                //Select Current Memory Module
                cmb_EMP_No.SelectedIndex =
                            (_Param_St_EEPRawRead.EPM_Number >= 0 &&
                            _Param_St_EEPRawRead.EPM_Number < cmb_EMP_No.Items.Count) ? _Param_St_EEPRawRead.EPM_Number : -1;

                txt_BaseAddress.Text = _Param_St_EEPRawRead.Address.ToString("X4");
                txt_Length.Text = _Param_St_EEPRawRead.Length.ToString();
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error", "Error displaying Current Memory Reference," + ex.Message);
            }
        }


        private void btn_GET_RawData_Click(object sender, EventArgs e)
        {
            //BillingController con = new BillingController();
            //con.AP_Controller = application_Controller.Applicationprocess_Controller;
            //SortMethod method = new BillingController().Get_Billing_SortMethod();
            Notification notifier = null;
            try
            {
                this.UpdateLabels();
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to Meter Management via Management");
                else
                {
                    if (!bgw.IsBusy)
                    {
                        //#region //Initialize Progress BAR

                        //progressBar.Style = ProgressBarStyle.Marquee;
                        //progressBar.Visible = true;
                        //progressBar.Enabled = true;

                        //progressBar.Value = 0;
                        //progressBar.Step = 1;

                        //#endregion

                        // Attach Event Handlers
                        bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
                        bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);

                        bgw.RunWorkerAsync();
                    }
                    else
                        throw new Exception("Internal Read Process is already in Continue");
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Parameterization Error", "Error occurred while reading Parameter Param_St_EEPRawRead," + ex.Message);
            }
        }


        private void btn_Read_Memory_MAP_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                this.UpdateLabels();
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to Meter Management via Management");
                else
                {
                    if (MemoryMAP_List == null)
                        MemoryMAP_List = new List<Param_St_EEPRawRead>();

                    if (MemoryMAP_List == null || MemoryMAP_List.Count <= 0)
                        throw new Exception("Error occurred while reading EEPROM data,Incorrect Memory Reference");

                    if (!bgw.IsBusy)
                    {
                        //#region //Initialize Progress BAR
                        //progressBar.Style = ProgressBarStyle.Continuous;
                        //progressBar.Visible = true;
                        //progressBar.Enabled = true;

                        //progressBar.Value = 0;
                        //progressBar.Maximum = MemoryMAP_List.Count;
                        //progressBar.Step = 1;
                        //#endregion
                        // Attach Event Handlers
                        bgw.DoWork += new DoWorkEventHandler(bgw_All_DoWork);
                        bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_Run_AllWorkerCompleted);

                        bgw.RunWorkerAsync();
                    }
                    else
                        throw new Exception("Internal Read Process is already in Continue");
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Parameterization Error", "Error occurred while reading Parameter Param_St_EEPRawRead," + ex.Message);
            }
        }


        private void btn_Clear_Editor_Click(object sender, EventArgs e)
        {

            DialogResult OutResult = MessageBox.Show("Are you sure want to delete data",
                        "Delete Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (OutResult != DialogResult.OK)
                return;

            rtb_RAW_Data.Clear();
        }


        private void btn_copy_Clip_Board_Click(object sender, EventArgs e)
        {
            try
            {
                string txt = rtb_RAW_Data.Text;

                if (!String.IsNullOrEmpty(rtb_RAW_Data.SelectedText))
                {
                    txt = rtb_RAW_Data.SelectedText;
                }

                if (!String.IsNullOrEmpty(txt))
                    Clipboard.SetText(txt, TextDataFormat.UnicodeText);
            }
            catch
            {
                Notification notifier = new Notification("Error Copy", "Error occurred while copying data on System ClipBoard");
            }
        }


        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            Application_Controller.IsIOBusy = true;
            IsMemoryReadCompleted = false;

            try
            {
                if (Current_Memory_Reference == null)
                    Current_Memory_Reference = new Param_St_EEPRawRead();

                if (Last_Memory_Reference == null || !Last_Memory_Reference.Equals(Current_Memory_Reference))
                { }

                var mem_Ref = new Param_St_EEPRawRead(Current_Memory_Reference);
                //Application_Controller.Param_Controller.Set_Param_StEEPRawRead(mem_Ref);
                ushort maxLength = 1024;
                ushort actualLengthToRead = mem_Ref.Length;
                double chunks = Math.Ceiling(((double)actualLengthToRead / maxLength));

                lblEpNo.Invoke(
                                 new MethodInvoker(() =>
                                 {
                                     lblEpNo.Text = mem_Ref.EPM_Number.ToString();
                                 }
                                 ));
                lblTotalChunks.Invoke(
                      new MethodInvoker(() =>
                      {
                          lblTotalChunks.Text = chunks.ToString();
                      }
                      ));

                mem_Ref.Length = (actualLengthToRead > maxLength) ? maxLength : actualLengthToRead;

                //#region //Initialize Progress BAR

                //progressBar.Style = ProgressBarStyle.Continuous;
                //progressBar.Visible = true;
                //progressBar.Enabled = true;

                //progressBar.Value = 0;
                //progressBar.Maximum = (int)chunks;
                //progressBar.Step = 1;

                //#endregion

                for (int i = 0; i < chunks; i++)
                {
                    int currChunkNo = i + 1;
                    lblCurrentChunk.Invoke(
                      new MethodInvoker(() =>
                      {
                          lblCurrentChunk.Text = currChunkNo.ToString();
                      }
                      ));
                    lblRemainingChunks.Invoke(
                      new MethodInvoker(() =>
                      {
                          lblRemainingChunks.Text = (chunks - currChunkNo).ToString();
                      }
                      ));

                    //Read RAW Data
                    Current_DataRead = Application_Controller.Param_Controller.GET_EEPRawData(mem_Ref);
                    //Last_Memory_Reference = new Param_St_EEPRawRead(mem_Ref);


                    InsertDataInRTB();


                    actualLengthToRead -= mem_Ref.Length;
                    mem_Ref.Address += mem_Ref.Length;

                    mem_Ref.Length = (actualLengthToRead > maxLength) ? maxLength : actualLengthToRead;
                    progressBar.PerformStep();
                }
                //Application_Controller.Param_Controller.Set_Param_StEEPRawRead(Current_Memory_Reference);

                // Read RAW Data
                //Current_DataRead = Application_Controller.Param_Controller.GET_EEPRawData(Current_Memory_Reference);
                Last_Memory_Reference = new Param_St_EEPRawRead(Current_Memory_Reference);
                IsMemoryReadCompleted = true;
            }
            catch(Exception ex)
            {
                ShowDataInRTB(ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }


        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Notification notifier = null;

            try
            {

                if (Current_DataRead != null && IsMemoryReadCompleted && Current_DataRead.Capture_Stamp != DateTime.MinValue)
                {
                    //String txt_OutPut = Get_Current_RAW_Data(Current_DataRead);
                    //rtb_RAW_Data.AppendText(txt_OutPut);
                    notifier = new Notification("Success", "Current Memory Reference read Successfully");
                }
                else { } 
                    //System.Diagnostics.Debugger.Break();

            }
            catch (Exception ex)
            {
                notifier = new Notification("Parameterization Error", "Error occurred while reading Parameter Param_St_EEPRawRead," + ex.Message);
            }
            finally
            {
                //Dettach Event Handlers
                bgw.DoWork -= bgw_DoWork;
                bgw.RunWorkerCompleted -= bgw_RunWorkerCompleted;

                //progressBar.Enabled = false;
                //progressBar.Visible = false;
            }
        }


        private void bgw_All_DoWork(object sender, DoWorkEventArgs e)
        {
            Application_Controller.IsIOBusy = true;
            IsMemoryReadCompleted = false;

            try
            {
                if (MemoryMAP_List == null)
                    MemoryMAP_List = new List<Param_St_EEPRawRead>();

                if (MemoryMAP_List == null || MemoryMAP_List.Count <= 0)
                    throw new Exception("Error occurred while reading EEPROM data,Incorrect Memory Reference");

                if (DataRead_List == null)
                    DataRead_List = new List<EEPRawReadData>();
                DataRead_List.Clear();

                if (chk_Mem_References.CheckedItems.Count > 0)
                {
                    Param_St_EEPRawRead[] chk_Items = new Param_St_EEPRawRead[chk_Mem_References.CheckedItems.Count];
                    chk_Mem_References.CheckedItems.CopyTo(chk_Items, 0);

                    foreach (Param_St_EEPRawRead chk_Item in chk_Items)
                    {
                        if (chk_Item != null)
                        {
                            var mem_Ref = new Param_St_EEPRawRead(chk_Item);
                            //Application_Controller.Param_Controller.Set_Param_StEEPRawRead(mem_Ref);
                            ushort maxLength = 1024;
                            ushort actualLengthToRead = mem_Ref.Length;
                            double chunks = Math.Ceiling(((double)actualLengthToRead / maxLength));
                            lblEpNo.Invoke(
                                  new MethodInvoker(() =>
                                  {
                                      lblEpNo.Text = mem_Ref.EPM_Number.ToString();
                                  }
                                  ));
                            lblTotalChunks.Invoke(
                                  new MethodInvoker(() =>
                                  {
                                      lblTotalChunks.Text = chunks.ToString();
                                  }
                                  ));
                            mem_Ref.Length = (actualLengthToRead > maxLength) ? maxLength : actualLengthToRead;

                            //#region //Initialize Progress BAR

                            //progressBar.Style = ProgressBarStyle.Continuous;
                            //progressBar.Visible = true;
                            //progressBar.Enabled = true;

                            //progressBar.Value = 0;
                            //progressBar.Maximum = (int)chunks;
                            //progressBar.Step = 1;

                            //#endregion

                            for (int i = 0; i < chunks; i++)
                            {
                                int currChunkNo = i + 1;
                                lblCurrentChunk.Invoke(
                                  new MethodInvoker(() =>
                                  {
                                      lblCurrentChunk.Text = currChunkNo.ToString();
                                  }
                                  ));
                                lblRemainingChunks.Invoke(
                                  new MethodInvoker(() =>
                                  {
                                      lblRemainingChunks.Text = ( chunks - currChunkNo ).ToString();
                                  }
                                  ));
                                //Read RAW Data
                                Current_DataRead = Application_Controller.Param_Controller.GET_EEPRawData(mem_Ref);
                                Last_Memory_Reference = new Param_St_EEPRawRead(mem_Ref);
                                DataRead_List.Add(Current_DataRead);

                              
                                      InsertDataInRTB();
                                  

                                actualLengthToRead -= mem_Ref.Length;
                                mem_Ref.Address += mem_Ref.Length;

                                mem_Ref.Length = (actualLengthToRead > maxLength) ? maxLength : actualLengthToRead;
                                //progressBar.PerformStep();
                            }
                            
                        }
                    }
                    IsMemoryReadCompleted = true;
                }
            }
            catch(Exception ex)
            {
                ShowDataInRTB(ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void InsertDataInRTB()
        {
            string data = Get_Current_RAW_Data(Current_DataRead);
            ShowDataInRTB(data);
        }
        private void ShowDataInRTB(string message)
        {
            if(rtb_RAW_Data.InvokeRequired)
            {
                rtb_RAW_Data.Invoke(
                                 new MethodInvoker(() =>
                                 {
                                     rtb_RAW_Data.AppendText(message);
                                     // set the current caret position to the end
                                     rtb_RAW_Data.SelectionStart = rtb_RAW_Data.Text.Length;
                                     // scroll it automatically
                                     rtb_RAW_Data.ScrollToCaret();
                                 }
                                 ));
              
            }
        }

        private void bgw_Run_AllWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Notification notifier = null;

            try
            {
                if (DataRead_List != null && DataRead_List.Count > 0 && IsMemoryReadCompleted)
                {
                    //foreach (var Current_DataRead in DataRead_List)
                    //{
                    //    String txt_OutPut = Get_Current_RAW_Data(Current_DataRead);
                    //    rtb_RAW_Data.AppendText(txt_OutPut);
                    //}
                    notifier = new Notification("Success", "Data read Successfully");
                }
                else
                    System.Diagnostics.Debugger.Break();

            }
            catch (Exception ex)
            {
                notifier = new Notification("Parameterization Error", "Error occurred while reading Parameter Param_St_EEPRawRead," + ex.Message);
            }
            finally
            {
                //Dettach Event Handlers
                bgw.DoWork -= bgw_All_DoWork;
                bgw.RunWorkerCompleted -= bgw_Run_AllWorkerCompleted;

                //progressBar.Enabled = false;
                //progressBar.Visible = false;
            }
        }


        private void btn_Add_MemRef_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {

                if (Current_Memory_Reference == null)
                    Current_Memory_Reference = new Param_St_EEPRawRead();

                if (!isValidateed)
                    throw new Exception("Invalid Current Memory Reference");

                var New_Current_Memory_Reference = new Param_St_EEPRawRead(Current_Memory_Reference);
                if (!Add_NewMemoryReference(New_Current_Memory_Reference))
                    throw new Exception("Already Exist Current Memory Reference");
                chk_Mem_References.Items.Add(New_Current_Memory_Reference);
                chk_Mem_References.SelectedItem = New_Current_Memory_Reference;

                if (chk_Mem_References.SelectedIndex != -1)
                    chk_Mem_References.SetItemChecked(chk_Mem_References.SelectedIndex, true);

                Show_Current_MemoryReference(New_Current_Memory_Reference);
                notifier = new Notification("Success", "Current Memory Reference added Successfully");

            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Adding Memory Reference" + ex.Message);
            }
        }


        private void btn_Clear_All_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (chk_Mem_References.CheckedItems.Count > 0)
                {
                    DialogResult OutResult = MessageBox.Show("Are you sure want to delete Items",
                        "Delete Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (OutResult != DialogResult.OK)
                        return;
                    UpdateLabels();
                    Param_St_EEPRawRead[] chk_Items = new Param_St_EEPRawRead[chk_Mem_References.CheckedItems.Count];
                    chk_Mem_References.CheckedItems.CopyTo(chk_Items, 0);

                    foreach (Param_St_EEPRawRead chk_Item in chk_Items)
                    {
                        if (chk_Item != null)
                        {
                            Remove_MemoryReference(chk_Item);
                            chk_Mem_References.Items.Remove(chk_Item);
                        }
                    }
                }
                //MemoryMAP_List.Clear();
                //chk_Mem_References.Items.Clear();
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Remove Current Memory Reference" + ex.Message);
            }
        }

        private void UpdateLabels()
        {
            this.lblRemainingChunks.Text = "--";
            this.lblEpNo.Text = "--";
            this.lblCurrentChunk.Text = "--";
            this.lblTotalChunks.Text = "--";
        }

        private void btn_Export_File_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (MemoryMAP_List == null || MemoryMAP_List.Count <= 0)
                    throw new Exception("Initialize Memory References MAP");

                string Dir = string.Empty;
                SaveFileDialog svDlg = new SaveFileDialog();
                svDlg.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
                if (DialogResult.OK == svDlg.ShowDialog())
                {
                    Dir = svDlg.FileName;
                }
                // Dir = LocalCommon.GetApplicationConfigsDirectory();

                Save_MemoryReferences(Dir, MemoryMAP_List);

                notifier = new Notification("Success", "Current Memory References MAP saved Successfully");

            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while writing Memory References" + ex.Message);
            }
        }


        private void btn_Import_File_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (MemoryMAP_List == null)
                    MemoryMAP_List = new List<Param_St_EEPRawRead>();

                string Dir = string.Empty;
                //Dir = LocalCommon.GetApplicationConfigsDirectory();
                OpenFileDialog opDlg = new OpenFileDialog();
                opDlg.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
                if (DialogResult.OK == opDlg.ShowDialog())
                {
                    Dir = opDlg.FileName;
                }
                var Mem_MAP = Load_MemoryReferences(Dir);
                if (Mem_MAP != null && Mem_MAP.Count > 0)
                {
                    MemoryMAP_List = Mem_MAP;

                    chk_Mem_References.SuspendLayout();
                    chk_Mem_References.Items.Clear();
                    chk_Mem_References.Items.AddRange(MemoryMAP_List.ToArray());
                    chk_ALL.Checked = true;
                    Application.DoEvents();
                    chk_Mem_References.ResumeLayout();

                    notifier = new Notification("Success", "Current Memory References MAP Loaded Successfully");
                }
                else
                    throw new Exception("Invalid Memory References MAP read");
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while writing Memory References" + ex.Message);
            }
            finally
            {
                chk_Mem_References.ResumeLayout();
            }
        }


        #region Support_Methods

        private string Get_Current_RAW_Data(EEPRawReadData RAWData)
        {
            StringBuilder txtBuilder = new StringBuilder(256);
            try
            {
                txtBuilder.AppendLine();

                if (RAWData != null &&
                    RAWData.RawData != null &&
                    RAWData.Capture_Stamp != DateTime.MinValue)
                {
                    int byte_Count = (RAWData.RawData != null) ? RAWData.RawData.Length : 0;

                    txtBuilder.AppendFormat("{0}--{1}   {2} {3}\r\n", RAWData.Param_St_EEPRawRead.ToString(1),
                                                                      byte_Count, RAWData.Capture_Stamp,
                                                                      RAWData.Param_St_EEPRawRead.TAG);
                    txtBuilder.AppendLine();

                    int Count = 1;
                    int Octect_Count = 1;

                    foreach (byte dt_Byte in RAWData.RawData)
                    {
                        txtBuilder.AppendFormat("{0:X2}", dt_Byte);

                        Count++;
                        if (WordWrapp_FormatOnly)
                        {
                            txtBuilder.AppendFormat(" ");
                        }
                        else
                        {
                            //Format On 8 Byte
                            if (Count > 8)
                            {
                                txtBuilder.AppendFormat(" ");
                                Count = 1;

                                Octect_Count++;
                            }
                            else
                                txtBuilder.AppendFormat(" ");

                            //Format On 3 Octet
                            if (Octect_Count > 2)
                            {
                                txtBuilder.AppendLine();

                                Octect_Count = 1;
                            }
                        }
                    }
                }
                txtBuilder.AppendLine();

                return txtBuilder.ToString();
            }
            catch (Exception)
            {

            }
            return "Invalid Format Error";
        }

        private bool Add_NewMemoryReference(Param_St_EEPRawRead Current_Memory_ReferenceArg)
        {
            bool isSuccessUpdate = false;
            try
            {
                if (!MemoryMAP_List.Exists((x) => x != null && x.Equals(Current_Memory_Reference)))
                {

                    MemoryMAP_List.Add(Current_Memory_ReferenceArg);

                    MemoryMAP_List.Sort((x, y) =>
                    {
                        if (x == null && y == null)
                            return 0;
                        else if (x == null)
                            return -1;
                        else if (y == null)
                            return 1;
                        else
                            return x.Compare(x, y);
                    });
                    isSuccessUpdate = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Adding Current Memory Reference", ex);
            }
            return isSuccessUpdate;
        }

        private void Remove_MemoryReference(Param_St_EEPRawRead Current_Memory_ReferenceArg)
        {
            try
            {
                MemoryMAP_List.Remove(Current_Memory_ReferenceArg);

                MemoryMAP_List.Sort((x, y) =>
                {
                    if (x == null && y == null)
                        return 0;
                    else if (x == null)
                        return -1;
                    else if (y == null)
                        return 1;
                    else
                        return x.Compare(x, y);
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Error removing Current Memory Reference", ex);
            }
        }

        public List<Param_St_EEPRawRead> Load_MemoryReferences(string Dir)
        {
            List<Param_St_EEPRawRead> MemoryMAP_List = null;
            try
            {
                //Dir += "\\MemoryReferences\\";                 /// Path for saving test files.
                //Directory.CreateDirectory(Dir);
                //string FILE = "Memory_Ref.xml";     // For saving the items.
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(List<Param_St_EEPRawRead>));

                using (FileStream new_File = new FileStream(Dir, FileMode.OpenOrCreate))
                {
                    MemoryMAP_List = (List<Param_St_EEPRawRead>)x.Deserialize(new_File);
                    new_File.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return MemoryMAP_List;
        }

        public void Save_MemoryReferences(string Dir, List<Param_St_EEPRawRead> MemoryMAP_List)
        {
            try
            {
                if (MemoryMAP_List == null || MemoryMAP_List.Count <= 0)
                    throw new ArgumentException("MemoryMAP_List");
                if (String.IsNullOrEmpty(Dir))
                    throw new ArgumentException("MemoryMAP_List");

                //Dir += "\\MemoryReferences\\";
                //Directory.CreateDirectory(Dir);
                //Path for saving test files.
                //string FILE = "Memory_Ref.xml";     // For saving the items.
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(List<Param_St_EEPRawRead>));

                using (FileStream new_File = new FileStream(Dir, FileMode.Create))
                {
                    x.Serialize(new_File, MemoryMAP_List);
                    new_File.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        private void tp1_Click(object sender, EventArgs e)
        {

        }

        #endregion

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txt_firmareInfo.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Param_ModemStatus status = Param_Controller.GetModemStatus();
            txt_firmareInfo.Text = status.ToString();
        }

        #region Add/Remove Object Selection

        private void btn_AddSelectedObject_Click(object sender, EventArgs e)
        {
            DeviceAssociation AssociationDetail = null;
            List<StOBISCode> OBISList = null;

            try
            {
                ConfigsHelper Configurator = new ConfigsHelper(application_Controller.Configurations);
                if (application_Controller != null &&
                   application_Controller.ConnectionController.SelectedMeter != null)
                    AssociationDetail = application_Controller.ConnectionController.CurrentAssociation;

                if (AssociationDetail != null)
                {
                    List<OBISCodeRights> OBISCodeRightsList = null;
                    List<KeyValuePair<StOBISCode, StOBISCode>> OBISCodeMapper = null;
                    Dictionary<StOBISCode, StOBISCode> OBISCodeMapperDictionary = null;

                    OBISCodeRightsList = Configurator.GetCurrentSAPOBISRightsIOP(AssociationDetail.RightGroupId, AssociationDetail.DeviceId);
                    OBISCodeMapper = Configurator.GetOBISCodesMapByDeviceId(AssociationDetail.DeviceId);
                    OBISCodeMapperDictionary = new Dictionary<StOBISCode, StOBISCode>(OBISCodeMapper.Count);

                    foreach (var keyVal in OBISCodeMapper)
                    {
                        if (!OBISCodeMapperDictionary.ContainsKey(keyVal.Value))
                            OBISCodeMapperDictionary.Add(keyVal.Value, keyVal.Key);
                    }

                    if (OBISCodeRightsList != null &&
                        OBISCodeRightsList.Count > 0)
                    {
                        StOBISCode OBISKEY = Get_Index.Dummy;
                        StOBISCode OBISValue = Get_Index.Dummy;
                        OBISList = new List<StOBISCode>(OBISCodeRightsList.Count);

                        foreach (var obisRight in OBISCodeRightsList)
                        {
                            OBISKEY = obisRight.OBISIndex;
                            OBISValue = Get_Index.Dummy;
                            if (OBISCodeMapperDictionary.ContainsKey(OBISKEY))
                                OBISValue = OBISCodeMapperDictionary[OBISKEY];

                            if (OBISValue.OBISIndex != Get_Index.Dummy && !OBISList.Contains(OBISValue))
                                OBISList.Add(OBISValue);
                        }
                    }
                }

                if (OBISList == null || OBISList.Count <= 0)
                {
                    throw new Exception("No DLMS Object found in OBISCode List");
                }

                foreach (StOBISCode obisCodeToAdd in OBISList)
                {
                    if (obisCodeToAdd.IsValidate)
                        AddObject_Selected(obisCodeToAdd);
                }

            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error Add DLMS Object in Current Software Login", ex.Message);
            }
        }

        private void btn_AddAllObject_Click(object sender, EventArgs e)
        {
            DeviceAssociation AssociationDetail = null;
            List<StOBISCode> OBISList = null;

            try
            {
                ConfigsHelper Configurator = new ConfigsHelper(application_Controller.Configurations);
                if (application_Controller != null &&
                   application_Controller.ConnectionController.SelectedMeter != null)
                    AssociationDetail = application_Controller.ConnectionController.CurrentAssociation;

                if (AssociationDetail != null)
                {
                    List<KeyValuePair<StOBISCode, StOBISCode>> OBISCodeMapper = null;
                    OBISCodeMapper = Configurator.GetOBISCodesMapByDeviceId(AssociationDetail.DeviceId);

                    StOBISCode OBISKEY = Get_Index.Dummy;
                    StOBISCode OBISValue = Get_Index.Dummy;

                    if (OBISCodeMapper != null &&
                        OBISCodeMapper.Count > 0)
                    {
                        OBISList = new List<StOBISCode>(OBISCodeMapper.Count);
                        foreach (KeyValuePair<StOBISCode, StOBISCode> OBISCodeMAP in OBISCodeMapper)
                        {
                            OBISKEY = OBISCodeMAP.Key;
                            OBISValue = OBISCodeMAP.Value;

                            if (OBISKEY.OBISIndex == Get_Index.Dummy || OBISValue.OBISIndex == Get_Index.Dummy)
                                continue;

                            OBISList.Add(OBISKEY);
                        }
                    }
                }

                if (OBISList == null || OBISList.Count <= 0)
                {
                    throw new Exception("No DLMS Object found in OBISCode List");
                }

                foreach (StOBISCode obisCodeToAdd in OBISList)
                {
                    if (obisCodeToAdd.IsValidate)
                        AddObject_Selected(obisCodeToAdd);
                }
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error Add DLMS Object in Current Device", ex.Message);
            }
        }

        private void btn_RemoveAllObject_Click(object sender, EventArgs e)
        {
            DeviceAssociation AssociationDetail = null;
            List<StOBISCode> OBISList = null;

            try
            {
                DialogResult Result = MessageBox.Show("Are you sure want to remove Selected Objects?", "Confirm Remove");
                if (Result != DialogResult.OK)
                {
                    return;
                }

                ConfigsHelper Configurator = new ConfigsHelper(application_Controller.Configurations);
                if (application_Controller != null &&
                   application_Controller.ConnectionController.SelectedMeter != null)
                    AssociationDetail = application_Controller.ConnectionController.CurrentAssociation;

                if (AssociationDetail != null)
                {
                    List<KeyValuePair<StOBISCode, StOBISCode>> OBISCodeMapper = null;
                    OBISCodeMapper = Configurator.GetOBISCodesMapByDeviceId(AssociationDetail.DeviceId);

                    StOBISCode OBISKEY = Get_Index.Dummy;
                    StOBISCode OBISValue = Get_Index.Dummy;

                    if (OBISCodeMapper != null &&
                        OBISCodeMapper.Count > 0)
                    {
                        OBISList = new List<StOBISCode>(OBISCodeMapper.Count);
                        foreach (KeyValuePair<StOBISCode, StOBISCode> OBISCodeMAP in OBISCodeMapper)
                        {
                            OBISKEY = OBISCodeMAP.Key;
                            OBISValue = OBISCodeMAP.Value;

                            if (OBISKEY.OBISIndex == Get_Index.Dummy || OBISValue.OBISIndex == Get_Index.Dummy)
                                continue;

                            OBISList.Add(OBISKEY);
                        }
                    }
                }

                if (OBISList == null || OBISList.Count <= 0)
                {
                    throw new Exception("No DLMS Object found in OBISCode List");
                }

                foreach (StOBISCode obisCodeToRemove in OBISList)
                {
                    if (obisCodeToRemove.IsValidate)
                        RemoveObject_Selected(obisCodeToRemove);
                }
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error Remove DLMS Object in Current Device", ex.Message);
            }
        }

        private void btn_RemoveSelectedObject_Click(object sender, EventArgs e)
        {
            DeviceAssociation AssociationDetail = null;
            List<StOBISCode> OBISList = null;

            try
            {
                DialogResult Result = MessageBox.Show("Are you sure want to remove Selected Objects?", "Confirm Remove");
                if (Result != DialogResult.OK)
                {
                    return;
                }

                ConfigsHelper Configurator = new ConfigsHelper(application_Controller.Configurations);
                if (application_Controller != null &&
                   application_Controller.ConnectionController.SelectedMeter != null)
                    AssociationDetail = application_Controller.ConnectionController.CurrentAssociation;

                if (AssociationDetail != null)
                {
                    List<OBISCodeRights> OBISCodeRightsList = null;
                    List<KeyValuePair<StOBISCode, StOBISCode>> OBISCodeMapper = null;
                    Dictionary<StOBISCode, StOBISCode> OBISCodeMapperDictionary = null;

                    OBISCodeRightsList = Configurator.GetCurrentSAPOBISRightsIOP(AssociationDetail.RightGroupId, AssociationDetail.DeviceId);
                    OBISCodeMapper = Configurator.GetOBISCodesMapByDeviceId(AssociationDetail.DeviceId);
                    OBISCodeMapperDictionary = new Dictionary<StOBISCode, StOBISCode>(OBISCodeMapper.Count);

                    foreach (var keyVal in OBISCodeMapper)
                    {
                        if (!OBISCodeMapperDictionary.ContainsKey(keyVal.Value))
                            OBISCodeMapperDictionary.Add(keyVal.Value, keyVal.Key);
                    }

                    if (OBISCodeRightsList != null &&
                        OBISCodeRightsList.Count > 0)
                    {
                        StOBISCode OBISKEY = Get_Index.Dummy;
                        StOBISCode OBISValue = Get_Index.Dummy;
                        OBISList = new List<StOBISCode>(OBISCodeRightsList.Count);

                        foreach (var obisRight in OBISCodeRightsList)
                        {
                            OBISKEY = obisRight.OBISIndex;
                            OBISValue = Get_Index.Dummy;
                            if (OBISCodeMapperDictionary.ContainsKey(OBISKEY))
                                OBISValue = OBISCodeMapperDictionary[OBISKEY];

                            if (OBISValue.OBISIndex != Get_Index.Dummy)
                                OBISList.Add(OBISValue.OBISIndex);
                        }
                    }
                }

                if (OBISList == null || OBISList.Count <= 0)
                {
                    throw new Exception("No DLMS Object found in OBISCode List");
                }

                foreach (StOBISCode obisCodeToAdd in OBISList)
                {
                    if (obisCodeToAdd.IsValidate)
                        RemoveObject_Selected(obisCodeToAdd);
                }

            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error Remove DLMS Object in Current Software Login", ex.Message);
            }
        }

        #endregion

        #region list_possible

        private void list_possible_Click(object sender, EventArgs e)
        {
            StOBISCode OBISCode = Get_Index.Dummy;
            string selected_TXT = string.Empty;
            string OBISCodeSTR = string.Empty;

            try
            {
                if (list_possible.SelectedItem != null)
                {
                    selected_TXT = list_possible.SelectedItem.ToString();
                    string ObjectLabel = string.Empty;
                    OBISCode = Get_OBISFromStringLabel(selected_TXT, ref ObjectLabel);
                    #region Commented Code_Section

                    // if (!string.IsNullOrEmpty(selected_TXT))
                    // {
                    //     OBISCodeSTR = selected_TXT.Substring(1, selected_TXT.IndexOf(" ] "));
                    //     OBISCode = StOBISCode.ConvertFrom(OBISCodeSTR);
                    // }

                    // Get_Index arg = (Get_Index)Enum.Parse(typeof(Get_Index), list_possible.SelectedItem.ToString().
                    //                                       Substring(list_possible.SelectedItem.ToString().IndexOf(" ] ") + 3));

                    // StOBISCode obis = new StOBISCode();
                    // obis = arg;
                    // lbl_OBIS.Text = obis.ToString(); 

                    #endregion
                }
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error exec list_possible_Click", ex.Message);
            }
            finally
            {
                // initializeGrid();
            }
        }

        private void list_possible_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                list_Selected.Items.Add(list_possible.SelectedItem);
                listItemsCount = list_Selected.Items.Count;
                lbl_totalItemsSelected.Text = listItemsCount.ToString();
                list_Search.Items.Remove(list_possible.SelectedItem);
                list_possible.Items.Remove(list_possible.SelectedItem);
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error exec list_possible_DoubleClick", ex.Message);
            }
            finally
            {
                // initializeGrid();
            }
        }

        private void list_possible_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #endregion


        public static StOBISCode Get_OBISFromStringLabel(string OBISSTR, ref string ObjectLabel)
        {
            StOBISCode OBISValue = Get_Index.Dummy;
            string OBISSTRLOCAL = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(OBISSTR))
                {
                    int indexerLoc = OBISSTR.IndexOf(" ] ");

                    OBISSTRLOCAL = OBISSTR.Substring(0, indexerLoc);
                    ObjectLabel = OBISSTR.Substring(indexerLoc);

                    if (!string.IsNullOrEmpty(ObjectLabel))
                        ObjectLabel = ObjectLabel.Trim(" []".ToCharArray());

                    if (!string.IsNullOrEmpty(OBISSTRLOCAL))
                        OBISSTRLOCAL = OBISSTRLOCAL.Trim(" []".ToCharArray());

                    OBISValue = StOBISCode.ConvertFrom(OBISSTRLOCAL);
                }

            }
            catch (Exception)
            {
                OBISValue = Get_Index.Dummy;
            }
            return OBISValue;
        }

        public static string Get_OBISStringLabel(StOBISCode OBISValue)
        {
            string TxtToAdd = string.Empty;
            try
            {
                string txtLabel = OBISValue.OBISIndex.ToString();

                if (!string.IsNullOrEmpty(txtLabel))
                {
                    txtLabel = txtLabel.Trim(" _".ToCharArray());
                    txtLabel = txtLabel.Replace("_", " ");
                }

                TxtToAdd = " [ " + OBISValue.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode) + " ] " + txtLabel;

            }
            catch (Exception)
            {
                TxtToAdd = string.Empty;
            }
            return TxtToAdd;
        }

        private void AddObject_Selected(StOBISCode ObisCode)
        {
            try
            {
                string TxtToAdd = Get_OBISStringLabel(ObisCode);
                if (!list_Selected.Items.Contains(TxtToAdd))
                    list_Selected.Items.Add(TxtToAdd);
                listItemsCount = list_Selected.Items.Count;
                lbl_totalItemsSelected.Text = listItemsCount.ToString();
                list_Search.Items.Remove(TxtToAdd);
                list_possible.Items.Remove(TxtToAdd);
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error Add Selected DLMS Object", ex.Message);
            }
        }

        private void RemoveObject_Selected(StOBISCode ObisCode)
        {
            try
            {
                string TxtToAdd = Get_OBISStringLabel(ObisCode);

                list_Selected.Items.Remove(TxtToAdd);
                listItemsCount = list_Selected.Items.Count;
                lbl_totalItemsSelected.Text = listItemsCount.ToString();
                // list_Search.Items.Remove(TxtToAdd);
                if (!list_possible.Items.Contains(TxtToAdd))
                    list_possible.Items.Add(TxtToAdd);
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error Remove Selected DLMS Object", ex.Message);
            }
        }

        
        public void initializeGrid()
        {
            Application.DoEvents();

            dgv_TestOutput.DataSource = null;
            dgv_TestOutput.Show();

            Application.DoEvents();
        }

        private void list_Selected_DoubleClick(object sender, EventArgs e)
        {
            deSelectList();
        }

        private void dgv_TestOutput_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Testing_Debug.Test_DataObject Current_TestDataRow = null;

            try
            {
                if (e.ColumnIndex == 7) // if ur link columnIndex (complain_no ColumnIndex) is zero 
                {
                    var Current_DataRow = (sender as DataGridView).CurrentRow;

                    if (Current_DataRow != null && Current_DataRow.DataBoundItem is Testing_Debug.Test_DataObject)
                        Current_TestDataRow = Current_DataRow.DataBoundItem as Testing_Debug.Test_DataObject;

                    if (Current_TestDataRow == null)
                        throw new InvalidOperationException("No Test Data Object available to display raw data");

                    if ((Current_TestDataRow.TestStatus & Testing_Debug.TestStatus.ReadSuccess) == 0 ||
                         Current_TestDataRow.Value_Array == null || Current_TestDataRow.Value_Array.Count <= 0)
                    {
                        throw new InvalidOperationException("No raw data available to display");
                    }

                    var RawTestData = Current_TestDataRow.Value_Array;
                    string rawTextData = LocalCommon.ArrayToHexString(RawTestData.ToArray(), true, 16);

                    DialogResult result = MsgBox.Show(rawTextData, "RAW Data!", MsgBox.Buttons.YesNo, MsgBox.Icon.Info, MsgBox.AnimateStyle.FadeIn);

                    // MessageBox.Show(rawTextData, "RAW Data");
                    // Notification notifier = new Notification("Raw Data Viewer", "Success");
                    // e.RowIndex

                    // Display your data here
                }

            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error Display Raw Data", ex.Message);
            }
        }

        private void btn_GetParams_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Application_Process.Is_Association_Developed)
                {
                    throw new Exception("Application Not Connected");
                }

                currentCount = 0;
                int.TryParse(txt_getCount.Text, out currentCount);

                if (currentCount <= 0)
                    throw new Exception("No Item Selected");

                #region Initiailze dgv_TestOutput

                StOBISCode OBISCode = Get_Index.Dummy;
                string OBISCodeTXT = string.Empty;
                List<StOBISCode> SelectOBISCodeLst = new List<StOBISCode>(150);
                List<String> SelectObjectLabels = new List<string>(150);

                foreach (object item in list_Selected.Items)
                {
                    OBISCodeTXT = item.ToString();
                    string ObjectLabel = string.Empty;
                    OBISCode = Get_OBISFromStringLabel(OBISCodeTXT, ref ObjectLabel);

                    if (SelectOBISCodeLst.Contains(OBISCode) ||
                        SelectObjectLabels.Contains(ObjectLabel))
                        continue;

                    SelectOBISCodeLst.Add(OBISCode);
                    SelectObjectLabels.Add(ObjectLabel);
                }

                DebugClass.AddTestObject(SelectOBISCodeLst, SelectObjectLabels);

                DebugClass.ResetTestObjects();
                DebugClass.SortTestDataItems();

                // Display Test Output Objects
                // clearGrid(dgv_TestOutput, false);
                DebugClass.showList(dgv_TestOutput);

                var ln_15CLS = SelectOBISCodeLst.Find((x) => x.ClassId == 15);
                var ln_7CLS = SelectOBISCodeLst.Find((x) => x.ClassId == 07);

                #region Confirm_Read_OPT

                DialogResult result = DialogResult.OK;
                DialogResult ln15_result = DialogResult.OK;
                DialogResult ProfileGen7_result = DialogResult.OK;

                if (SelectOBISCodeLst.Count > 15)
                    result = MessageBox.Show("Are you sure want to read OBIS Codes for Selected Items", "Confirm Read OPT", MessageBoxButtons.OKCancel);

                if (ln_15CLS != null &&
                    ln_15CLS.ClassId == 15)
                    ln15_result = MessageBox.Show("Are you sure want to read Object_List AttributeId 2 for Association Objects", "Confirm Read OPT", MessageBoxButtons.OKCancel);

                if (ln_7CLS != null &&
                    ln_7CLS.ClassId == 07)
                    ProfileGen7_result = MessageBox.Show("Are you sure want to read Buffer for All Generic Profile Objects", "Confirm Read OPT", MessageBoxButtons.OKCancel);

                #endregion

                Testing_Debug.Test_DataObject ItemObj;

                for (int indexer = 0; indexer < DebugClass.ObjectsList.Count;)
                {
                    ItemObj = DebugClass.ObjectsList[indexer];

                    if (!SelectOBISCodeLst.Contains(ItemObj.OBIS_Code))
                    {
                        DebugClass.ObjectsList.Remove(ItemObj);
                        continue;
                    }
                    indexer++;

                    #region // Attribute Id 1

                    if (ItemObj.AttributeId == 1 &&
                            ItemObj.Label.Contains("Logical_Name"))
                    {
                        if (result != DialogResult.OK)
                        {
                            ItemObj.TestStatus = ItemObj.TestStatus | Testing_Debug.TestStatus.UserCancel;
                        }
                    }

                    #endregion
                    #region // Class Id 07

                    if (ItemObj.OBIS_Code.ClassId == 07 && ItemObj.AttributeId == 0x02)
                    {
                        if (ProfileGen7_result != DialogResult.OK)
                        {
                            ItemObj.TestStatus = ItemObj.TestStatus | Testing_Debug.TestStatus.UserCancel;
                        }
                    }

                    #endregion
                    #region // Class Id 15

                    if (ItemObj.OBIS_Code.ClassId == 15 && ItemObj.AttributeId == 0x02)
                    {
                        if (ln15_result != DialogResult.OK)
                        {
                            ItemObj.TestStatus = ItemObj.TestStatus | Testing_Debug.TestStatus.UserCancel;
                        }
                    }

                    #endregion
                }

                #endregion

                var COSEM_Obj_ToRead = DebugClass.GetTestObjects(SelectOBISCodeLst[0]);
                DebugClass.ReadWriteOPT = false;

                if (bgw_DebugTest != null)
                {
                    if (bgw_DebugTest.IsBusy)
                        bgw_DebugTest.CancelAsync();
                    bgw_DebugTest.Dispose();
                }

                // Attach bgw_DebugTest Event Handlers
                bgw_DebugTest = new BackgroundWorker() { WorkerSupportsCancellation = true, WorkerReportsProgress = true };
                bgw_DebugTest.DoWork += new DoWorkEventHandler(bgw_DebugTest_DoWork);
                bgw_DebugTest.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_DebugTest_RunWorkerCompleted);
                bgw_DebugTest.ProgressChanged += new ProgressChangedEventHandler(bgw_DebugTest_ProgressChanged);

                // Start Async Operation
                bgw_DebugTest.RunWorkerAsync();

                pb1.Style = ProgressBarStyle.Continuous;
                pb1.Enabled = true;
                pb1.Visible = true;
                pb1.Minimum = 0;
                pb1.Maximum = 100;
                pb1.Value = 0;

                Application.DoEvents();
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error Reading Data",
                                                         "Error occurred while reading Selected Data Item" + ex.Message);
            }
        }

        private void btn_SetParams_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Application_Process.Is_Association_Developed)
                {
                    throw new Exception("Application Not Connected");
                }

                currentCount = 0;
                int.TryParse(txt_getCount.Text, out currentCount);

                if (currentCount <= 0 ||
                    DebugClass.ObjectsList == null ||
                    DebugClass.ObjectsList.Count <= 0)
                    throw new Exception("No Item Selected");

                #region Initiailze dgv_TestOutput

                StOBISCode OBISCode = Get_Index.Dummy;
                string OBISCodeTXT = string.Empty;
                List<StOBISCode> SelectOBISCodeLst = new List<StOBISCode>(150);
                List<String> SelectObjectLabels = new List<string>(150);

                foreach (object item in list_Selected.Items)
                {
                    OBISCodeTXT = item.ToString();
                    string ObjectLabel = string.Empty;
                    OBISCode = Get_OBISFromStringLabel(OBISCodeTXT, ref ObjectLabel);
                    SelectOBISCodeLst.Add(OBISCode);
                    SelectObjectLabels.Add(ObjectLabel);
                }

                DebugClass.AddTestObject(SelectOBISCodeLst, SelectObjectLabels);

                // DebugClass.ResetTestObjects();
                DebugClass.SortTestDataItems();

                // Display Test Output Objects
                // clearGrid(dgv_TestOutput, false);
                DebugClass.showList(dgv_TestOutput);

                var ln_15CLS = SelectOBISCodeLst.Find((x) => x.ClassId == 15);
                var ln_7CLS = SelectOBISCodeLst.Find((x) => x.ClassId == 07);

                #region Confirm_Read_OPT

                DialogResult resultWritable = DialogResult.OK;
                DialogResult result = DialogResult.OK;
                DialogResult ln15_result = DialogResult.OK;
                DialogResult ProfileGen7_result = DialogResult.OK;

                // if (SelectOBISCodeLst.Count > 15)
                result = MessageBox.Show("Are you sure want to Write OBIS Codes for Selected Items", "Confirm Write OPT", MessageBoxButtons.OKCancel);

                resultWritable = MessageBox.Show("Are you sure want to parameterize Only Writable Attributes of Selected Items", "Writable Attribute Only", MessageBoxButtons.OKCancel);

                if (resultWritable != DialogResult.OK)
                    DebugClass.OnlyWritable = false;

                if (ln_7CLS != null &&
                    ln_7CLS.ClassId == 07)
                    ProfileGen7_result = MessageBox.Show("Are you sure want to Write Capute Objects & Buffer Attribute for All Generic Profile Objects", "Confirm Read OPT", MessageBoxButtons.OKCancel);

                if (ln_15CLS != null &&
                    ln_15CLS.ClassId == 15)
                    ln15_result = MessageBox.Show("Are you sure want to Write Object_List AttributeId 2 for Association Objects", "Confirm Read OPT", MessageBoxButtons.OKCancel);

                #endregion

                Testing_Debug.Test_DataObject ItemObj;

                for (int indexer = 0; indexer < DebugClass.ObjectsList.Count;)
                {
                    ItemObj = DebugClass.ObjectsList[indexer];

                    if (!SelectOBISCodeLst.Contains(ItemObj.OBIS_Code))
                    {
                        DebugClass.ObjectsList.Remove(ItemObj);
                        continue;
                    }
                    indexer++;

                    #region // Attribute Id 1

                    if (ItemObj.AttributeId == 1 &&
                            ItemObj.Label.Contains("Logical_Name"))
                    {
                        if (result != DialogResult.OK)
                        {
                            ItemObj.TestStatus = ItemObj.TestStatus | Testing_Debug.TestStatus.UserCancel;
                        }
                    }

                    #endregion
                    #region // Class Id 07

                    if (ItemObj.OBIS_Code.ClassId == 07 &&
                        (ItemObj.AttributeId == 0x02 || ItemObj.AttributeId == 0x03))
                    {
                        if (ProfileGen7_result != DialogResult.OK)
                        {
                            ItemObj.TestStatus = ItemObj.TestStatus | Testing_Debug.TestStatus.UserCancel;
                        }
                    }

                    #endregion
                    #region // Class Id 15

                    if (ItemObj.OBIS_Code.ClassId == 15 && ItemObj.AttributeId == 0x02)
                    {
                        if (ln15_result != DialogResult.OK)
                        {
                            ItemObj.TestStatus = ItemObj.TestStatus | Testing_Debug.TestStatus.UserCancel;
                        }
                    }

                    #endregion
                }

                #endregion

                var COSEM_Obj_ToRead = DebugClass.GetTestObjects(SelectOBISCodeLst[0]);
                DebugClass.ReadWriteOPT = true;

                if (bgw_DebugTest != null)
                {
                    if (bgw_DebugTest.IsBusy)
                        bgw_DebugTest.CancelAsync();
                    bgw_DebugTest.Dispose();
                }

                // Attach bgw_DebugTest Event Handlers
                bgw_DebugTest = new BackgroundWorker() { WorkerSupportsCancellation = true, WorkerReportsProgress = true };
                bgw_DebugTest.DoWork += new DoWorkEventHandler(bgw_DebugTest_DoWork);
                bgw_DebugTest.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_DebugTest_RunWorkerCompleted);
                bgw_DebugTest.ProgressChanged += new ProgressChangedEventHandler(bgw_DebugTest_ProgressChanged);

                // Start Async Operation
                bgw_DebugTest.RunWorkerAsync();

                pb1.Style = ProgressBarStyle.Continuous;
                pb1.Enabled = true;
                pb1.Visible = true;
                pb1.Minimum = 0;
                pb1.Maximum = 100;
                pb1.Value = 0;

                Application.DoEvents();
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error Writing Data",
                                                         "Error occurred while writing Selected Data Item" + ex.Message);
            }
        }

        public void GetTestObjectData(List<Testing_Debug.Test_DataObject> Current_COSEM_Obj = null)
        {
            Base_Class ObjectToRead = null;
            StOBISCode OBIS_Code = Get_Index.Dummy;
            String Object_Label = string.Empty;

            Testing_Debug.Test_DataObject Logical_Name = null;
            Testing_Debug.Test_DataObject Current_Object = null;
            List<byte> RawCOMSEM_Object = null;
            byte[] RawCOSEM_ObjectData = null;
            int array_traverse = 0;
            Data_Access_Result AccessResult = Data_Access_Result.Object_Unavailable;
            Testing_Debug.TestStatus testData;

            try
            {
                Logical_Name = Current_COSEM_Obj.Find((x) => x.AttributeId == 01 &&
                                       string.Equals("Logical_Name", x.Label));
                OBIS_Code = Current_COSEM_Obj[0].OBIS_Code;
                ObjectToRead = AP_Controller.GetSAPEntry(OBIS_Code);

                #region // Request Logical_Name

                if (Logical_Name != null &&
                   (Logical_Name.TestStatus & Testing_Debug.TestStatus.UserCancel) == 0)
                {
                    testData = Logical_Name.TestStatus;

                    // Rest Read Status
                    testData &= ~Testing_Debug.TestStatus.ReadNotPerform;
                    testData &= ~Testing_Debug.TestStatus.ReadSuccess;
                    testData &= ~Testing_Debug.TestStatus.ReadFailure;

                    AccessResult = AP_Controller.TryGETObject(OBIS_Code, Logical_Name.AttributeId, out RawCOMSEM_Object);
                    Logical_Name.TimeStamp = DateTime.Now;
                    Logical_Name.Value_Array = RawCOMSEM_Object;
                    Logical_Name.String_Value = string.Empty;
                    Logical_Name.Value_Type = Testing_Debug.TypeOfValue.byte_array;

                    if (AccessResult == Data_Access_Result.Success)
                    {
                        testData |= Testing_Debug.TestStatus.ReadSuccess;
                        Logical_Name.TestStatus = testData;

                        // Decode Logical Name
                        RawCOSEM_ObjectData = null;
                        RawCOSEM_ObjectData = RawCOMSEM_Object.ToArray();
                        byte[] Obis_code_received = null;

                        if (Obis_code_received == null)              // Instantiate OBIS-Code Array
                            Obis_code_received = new byte[6];
                        array_traverse = 0;
                        Obis_code_received = BasicEncodeDecode.Decode_OctectString(RawCOSEM_ObjectData, ref array_traverse, RawCOSEM_ObjectData.Length);
                        StOBISCode CodeReceived = StOBISCode.ConvertFrom(Obis_code_received);
                        CodeReceived.ClassId = OBIS_Code.ClassId;

                        Logical_Name.String_Value = CodeReceived.ToString(StOBISCode.FormatSpecifier.CompleteDecimalMode);
                        Logical_Name.Value_Type = Testing_Debug.TypeOfValue.string_value;
                    }
                    else
                        testData |= Testing_Debug.TestStatus.ReadFailure;

                    Logical_Name.TestStatus = testData;
                }

                #endregion
                #region // GET Object Attribute

                for (int index = 1; index < Current_COSEM_Obj.Count; index++)
                {
                    Current_Object = Current_COSEM_Obj[index];

                    if (Current_Object == null ||
                        (Current_Object.TestStatus & Testing_Debug.TestStatus.UserCancel) != 0)
                        continue;

                    // Initial Attribute Read Procedure
                    RawCOMSEM_Object = null;
                    RawCOSEM_ObjectData = null;

                    testData = Current_Object.TestStatus;
                    // Reset Read Status
                    testData &= ~Testing_Debug.TestStatus.ReadNotPerform;
                    testData &= ~Testing_Debug.TestStatus.ReadSuccess;
                    testData &= ~Testing_Debug.TestStatus.ReadFailure;

                    AccessResult = AP_Controller.TryGETObject(OBIS_Code, Current_Object.AttributeId, out RawCOMSEM_Object);
                    Current_Object.TimeStamp = DateTime.Now;
                    Current_Object.Value_Array = RawCOMSEM_Object;
                    Current_Object.String_Value = string.Empty;
                    Current_Object.Value_Type = Testing_Debug.TypeOfValue.byte_array;

                    if (AccessResult == Data_Access_Result.Success)
                    {
                        testData |= Testing_Debug.TestStatus.ReadSuccess;
                    }
                    else
                        testData |= Testing_Debug.TestStatus.ReadFailure;

                    Current_Object.TestStatus = testData;
                }

                #endregion
                DebugClass.TryDecode_TestDebugData(Current_COSEM_Obj, ObjectToRead);
            }
            catch
            {
                throw;
            }
        }

        public void SetTestObjectData(List<Testing_Debug.Test_DataObject> Current_COSEM_Obj = null, bool setOnlyWritable = true)
        {
            Base_Class ObjectToWrite = null;
            StOBISCode OBIS_Code = Get_Index.Dummy;
            String Object_Label = string.Empty;

            Testing_Debug.Test_DataObject Logical_Name = null;
            Testing_Debug.Test_DataObject Current_Object = null;
            List<byte> RawCOMSEM_Object = null;
            byte[] RawCOSEM_ObjectData = null;
            Data_Access_Result AccessResult = Data_Access_Result.Object_Unavailable;
            Testing_Debug.TestStatus testData;

            try
            {
                Logical_Name = Current_COSEM_Obj.Find((x) => x.AttributeId == 01 &&
                                       string.Equals("Logical_Name", x.Label));
                OBIS_Code = Current_COSEM_Obj[0].OBIS_Code;
                ObjectToWrite = AP_Controller.GetSAPEntry(OBIS_Code);

                #region // Write Logical_Name

                if (Logical_Name != null &&
                    (Logical_Name.TestStatus & Testing_Debug.TestStatus.UserCancel) == 0)
                {
                    testData = Logical_Name.TestStatus;
                    RawCOSEM_ObjectData = BasicEncodeDecode.Encode_OctetString(OBIS_Code.OBISCode, DataTypes._A09_octet_string);

                    // Rest Write Status
                    testData &= ~Testing_Debug.TestStatus.WriteNotPerform;
                    testData &= ~Testing_Debug.TestStatus.WriteSuccess;
                    testData &= ~Testing_Debug.TestStatus.WriteFailure;

                    AccessResult = AP_Controller.TrySETObject(ObjectToWrite, Logical_Name.AttributeId, RawCOSEM_ObjectData);

                    Logical_Name.TimeStamp = DateTime.Now;

                    if (AccessResult == Data_Access_Result.Success)
                    {
                        testData |= Testing_Debug.TestStatus.WriteSuccess;
                        Logical_Name.TestStatus = testData;
                    }
                    else
                        testData |= Testing_Debug.TestStatus.WriteFailure;

                    Logical_Name.TestStatus = testData;
                }

                #endregion
                #region // SET Object Attribute

                for (int index = 1; index < Current_COSEM_Obj.Count; index++)
                {
                    Current_Object = Current_COSEM_Obj[index];

                    if (Current_Object == null ||
                        (Current_Object.TestStatus & Testing_Debug.TestStatus.UserCancel) != 0)
                        continue;
                    // Skip Attribute If Not Writeable
                    else if (Current_Object != null && setOnlyWritable &&
                             !ObjectToWrite.IsAttribWritable(Current_Object.AttributeId))
                    {
                        Current_Object.TestStatus &= ~Testing_Debug.TestStatus.WriteFailure;
                        Current_Object.TestStatus &= ~Testing_Debug.TestStatus.WriteSuccess;

                        Current_Object.TestStatus |= Testing_Debug.TestStatus.WriteNotPerform;
                        continue;
                    }
                    // Skip An Attribute If Not ReadSuccess
                    else if ((Current_Object.TestStatus & Testing_Debug.TestStatus.ReadSuccess) == 0 ||
                               Current_Object.Value_Array == null || Current_Object.Value_Array.Count <= 0)
                    {
                        Current_Object.TestStatus &= ~Testing_Debug.TestStatus.WriteNotPerform;
                        Current_Object.TestStatus &= ~Testing_Debug.TestStatus.WriteSuccess;

                        Current_Object.TestStatus |= Testing_Debug.TestStatus.WriteFailure;
                        continue;
                    }


                    // Initial Attribute Write Procedure
                    RawCOMSEM_Object = null;
                    RawCOSEM_ObjectData = null;

                    testData = Current_Object.TestStatus;
                    // Reset Write Status
                    testData &= ~Testing_Debug.TestStatus.WriteNotPerform;
                    testData &= ~Testing_Debug.TestStatus.WriteSuccess;
                    testData &= ~Testing_Debug.TestStatus.WriteFailure;

                    RawCOMSEM_Object = Current_Object.Value_Array;
                    RawCOSEM_ObjectData = RawCOMSEM_Object.ToArray();

                    AccessResult = AP_Controller.TrySETObject(ObjectToWrite, Current_Object.AttributeId, RawCOSEM_ObjectData);

                    Current_Object.TimeStamp = DateTime.Now;

                    if (AccessResult == Data_Access_Result.Success)
                    {
                        testData |= Testing_Debug.TestStatus.WriteSuccess;
                    }
                    else
                        testData |= Testing_Debug.TestStatus.WriteFailure;

                    Current_Object.TestStatus = testData;
                }

                #endregion
            }
            catch
            {
                throw;
            }
        }

        public void clearGridRow(DataGridView grid1)
        {
            grid1.Visible = false;
            grid1.DataSource = null;

            int totalRows = grid1.Rows.Count;
            if (grid1.Rows.Count > 0)
            {
                for (int i = totalRows - 1; i >= 0; i--)
                {
                    grid1.Rows.RemoveAt(i);
                }
            }
        }

        public void clearGridColumn(DataGridView grid1)
        {
            grid1.Visible = false;
            grid1.DataSource = null;

            int totalCols = grid1.Columns.Count;
            if (grid1.Columns.Count > 0)
            {
                for (int i = totalCols - 1; i >= 0; i--)
                {
                    grid1.Columns.RemoveAt(i);
                }
            }
        }


        private void timer_testing_Tick(object sender, EventArgs e)
        {
            try
            {
                if (currentCount < getCount)
                {
                    Application_Controller.IsIOBusy = true;
                    timer_testing.Enabled = false;
                    timer_testing.Stop();

                    if (Application_Process.Is_Association_Developed)
                    {
                        // dgv_TestOutput.Rows.Add(); currentRow++;
                        // GetParamValues(currentRow);
                        Application.DoEvents();
                        // dgv_TestOutput.Rows[dgv_TestOutput.Rows.Count - 1].Selected = true;
                        // dgv_TestOutput.Focus();
                    }
                    timer_testing.Enabled = true;

                    currentCount++;
                    timer_testing.Start();
                }
            }
            catch (Exception ex)
            {
                timer_testing.Stop();
            }
            finally
            {
                if (!timer_testing.Enabled)
                    Application_Controller.IsIOBusy = false;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                Application.DoEvents();

                if (bgw_DebugTest != null &&
                    bgw_DebugTest.IsBusy)
                {
                    bgw_DebugTest.CancelAsync();
                }

                timer_testing.Enabled = false;
                btn_ClearSelectedList.Enabled = true;

                Application.DoEvents();
            }
            catch
            {
            }
        }

        public void clearGrid(DataGridView grid1, bool clearcolum = true)
        {
            // if (clearcolum)
            //     clearGridColumn(grid1);
            // clearGridRow(grid1);
            grid1.DataSource = null;
        }

        private void btn_ListSearch_Click(object sender, EventArgs e)
        {
            list_Search.Items.Clear();
        }

        private void list_Search_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                list_Selected.Items.Add(list_Search.SelectedItem);
                lbl_totalItemsSelected.Text = list_Selected.Items.Count.ToString();
                list_possible.Items.Remove(list_Search.SelectedItem);
                list_Search.Items.Remove(list_Search.SelectedItem);

                initializeGrid();
            }
            catch (Exception)
            {
            }
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            if (txt_search.Text != "")
            {
                string toSearch = txt_search.Text;
                string item = "";
                list_Search.Items.Clear();
                foreach (object obj in list_possible.Items)
                {
                    item = obj.ToString();
                    // MessageBox.Show(item.Substring(item.IndexOf(" ] ") + 3));
                    if (item.ToUpper().StartsWith(" [ " + toSearch.ToUpper()) ||
                        item.Substring(item.IndexOf(" ] ") + 3).ToUpper().StartsWith(toSearch.ToUpper()))
                    {
                        list_Search.Items.Add(obj);
                    }

                    Application.DoEvents();
                }
            }
            else
            {
                list_Search.Items.Clear();
            }
        }

        private void btn_ListAddAll_Click(object sender, EventArgs e)
        {
            for (int i = list_Search.Items.Count - 1; i >= 0; i--)
            {
                list_Selected.Items.Add(list_Search.Items[i]);
                listItemsCount = list_Selected.Items.Count;
                lbl_totalItemsSelected.Text = listItemsCount.ToString();
                list_Selected.SelectedItem = list_Search.Items[i];
                list_possible.Items.Remove(list_Search.Items[i]);
                list_Search.Items.Remove(list_Search.Items[i]);
                Application.DoEvents();
            }
            initializeGrid();
        }

        private void btn_ClearSelectedList_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Result = MessageBox.Show("Are you sure want to remove Selected Objects?", "Confirm Remove");
                if (Result != DialogResult.OK)
                {
                    return;
                }

                foreach (object obj in list_Selected.Items)
                {
                    if (!list_possible.Items.Contains(obj))
                        list_possible.Items.Add(obj);
                }

                list_Selected.Items.Clear();
                lbl_totalItemsSelected.Text = "0";
                initializeGrid();
            }
            catch (Exception)
            {
                lbl_totalItemsSelected.Text = list_Selected.Items.Count.ToString();
            }
        }

        private void btn_ClearGrid_Click(object sender, EventArgs e)
        {
        }

        /*
         * 
         */

        private void deSelectList()
        {
            try
            {
                list_possible.Items.Add(list_Selected.SelectedItem);
                list_Selected.Items.Remove(list_Selected.SelectedItem);
                listItemsCount = list_Selected.Items.Count;
                lbl_totalItemsSelected.Text = listItemsCount.ToString();
                initializeGrid();
            }
            catch (Exception)
            {
            }
        }

        private void bgw_DebugTest_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Application_Controller.IsIOBusy = true;

                if (application_Controller == null || application_Controller.Applicationprocess_Controller == null ||
                   !application_Controller.Applicationprocess_Controller.IsConnected)
                    throw new InvalidOperationException("Connect Application to proceed Read/Write");

                if (DebugClass.ObjectsList == null || DebugClass.ObjectsList.Count < 0)
                    throw new InvalidOperationException("No Object Selected to Read/Write");

                var All_TestObjs = DebugClass.GetAllTestObjects();

                if (All_TestObjs == null || All_TestObjs.Count < 0)
                    throw new InvalidOperationException("No Object Selected to Read/Write");

                float percent_Complete = 0.0f;
                float current_Object_Complete = 0.0f;
                float total_Object_To_ReadWrite = All_TestObjs.Count;


                foreach (var curent_DataObj in All_TestObjs)
                {
                    // Canel OPT Request
                    if (e.Cancel || (sender as BackgroundWorker).CancellationPending)
                        throw new OperationCanceledException("Debug Data Read Operation Cancel by User");

                    try
                    {
                        // Data Read OPT
                        if (!DebugClass.ReadWriteOPT)
                        {
                            GetTestObjectData(curent_DataObj);
                        }
                        // Paramters Write OPT
                        else
                        {
                            SetTestObjectData(curent_DataObj, DebugClass.OnlyWritable);
                        }

                        current_Object_Complete++;
                        percent_Complete = (current_Object_Complete / total_Object_To_ReadWrite) * 100;
                        // Report Work Complete Progress
                        (sender as BackgroundWorker).ReportProgress(Convert.ToInt32(percent_Complete));

                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Error read in Debug Test Data");
                    }
                    finally
                    {
                        if (application_Controller == null || application_Controller.Applicationprocess_Controller == null ||
                            !application_Controller.Applicationprocess_Controller.IsConnected)
                            throw new InvalidOperationException("Connect Application to proceed Read/Write");
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void bgw_DebugTest_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                if (pb1 != null && pb1.Enabled)
                {
                    if (e.ProgressPercentage >= pb1.Minimum && e.ProgressPercentage <= pb1.Maximum)
                        pb1.Value = e.ProgressPercentage;
                }
            }
            catch
            {
                if (pb1 != null)
                {
                    pb1.Enabled = false;
                    pb1.Visible = false;
                }
            }
        }

        private void bgw_DebugTest_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                    throw e.Error;
                else if (e.Cancelled)
                    throw new OperationCanceledException("Debug Data Read Operation Cancel by User");

                if (!DebugClass.ReadWriteOPT)
                {
                    Notification notifier = new Notification("Completed", "Data Read Operation Completed Successfuly");
                }
                else
                {
                    Notification notifier = new Notification("Completed", "Data Write Operation Completed Successfuly");
                }
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error", ex.Message);
            }
            finally
            {
                pb1.Enabled = false;
                pb1.Visible = false;
            }
        }

        #region Door Open

        private void btnDoorOpenSet_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Application Association");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    char x = 'A';
                    Data_Access_Result result = Param_Controller.SET_any_class1(x, Get_Index.Activate_Door_Open, DataTypes._A11_unsigned, 2);
                    Notification notifier = new Notification("Door Open Set", result.ToString());
                }
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }


        #endregion // Door Open


        internal void Reset_State()
        {
            try
            {
                // Clear All Communication Console's
                btnIClearOutput.PerformClick();
                txtOutPutWindow.Text = "";
                btnIClearcomm.PerformClick();
                txtICommViewer.Text = "";
                txtIDecodedOutput.Text = "";
                btnIClrDecodedOP.PerformClick();
                txtIWroteResp.Text = "";
                button2.PerformClick();
                btn_ClearSelectedList.PerformClick();
                //btn_ClearSelectedList_Click(null, null);
                txt_debugString.Text = "";
                timer_testing.Enabled = false;
                clearGrid(dgv_TestOutput, false);
            }
            catch (Exception ex)
            {
            }
        }

        private void btnCalibrationModeSet_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Application Association");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    char x = 'A';
                    Data_Access_Result result = Param_Controller.SET_any_class1(x, Get_Index.Calibraion_Mode, DataTypes._A11_unsigned, 2);
                    Notification notifier = new Notification("Calibration Mode Active", result.ToString());
                }
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Application Association");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    char x = 'D';
                    Data_Access_Result result = Param_Controller.SET_any_class1(x, Get_Index.Calibraion_Mode, DataTypes._A11_unsigned, 2);
                    Notification notifier = new Notification("Calibration Mode Deactive", result.ToString());
                }
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnSetWatchDog_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Application Association");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    byte x = Convert.ToByte(chkWatchDogReset.Checked);
                    Data_Access_Result result = Param_Controller.SET_any_class1(x, Get_Index.Watch_Dog_Reset, DataTypes._A03_boolean, 2);
                    Notification notifier = new Notification("Set Watch Dog Reset", result.ToString());
                }
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Set Watch Dog Reset", ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnGetWatchDogReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                {
                    Notification notifier = new Notification("Association Error", "Create Association to Meter");
                }
                else
                {
                    Application_Controller.IsIOBusy = true;
                    chkWatchDogReset.Checked = Convert.ToBoolean(Param_Controller.GETDouble_Any(Get_Index.Watch_Dog_Reset, 2));
                }
            }
            catch (Exception ex)
            {
                Notification notif = new Notification("Get Watch Dog Reset", ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnPowerUpResetSet_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Application Association");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    byte x = Convert.ToByte(chkPowerUpReset.Checked);
                    Data_Access_Result result = Param_Controller.SET_any_class1(x, Get_Index.Power_Up_Reset, DataTypes._A03_boolean, 2);
                    Notification notifier = new Notification("Set Power Up Reset", result.ToString());
                }
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Set Power Up Reset", ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnPowerUpResetGet_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                {
                    Notification notifier = new Notification("Association Error", "Create Association to Meter");
                }
                else
                {
                    Application_Controller.IsIOBusy = true;
                    chkPowerUpReset.Checked = Convert.ToBoolean(Param_Controller.GETDouble_Any(Get_Index.Power_Up_Reset, 2));
                }
            }
            catch (Exception ex)
            {
                Notification notif = new Notification("Get Power Up Reset", ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnSetTimerReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Application Association");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    TimerReset_SinglePhase timer_reset_1p = new TimerReset_SinglePhase();
                    timer_reset_1p.Enable = chkEnableTimerReset.Checked;
                    timer_reset_1p.Hour = Convert.ToByte(cmbHour.Text);
                    timer_reset_1p.Minute = Convert.ToByte(cmbMinute.Text);
                    Data_Access_Result result = Param_Controller.SET_TimerReset_SP(timer_reset_1p);
                    Notification notifier = new Notification("Set Timer Reset", result.ToString());
                }
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Set Timer Reset", ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnGetTimerReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                {
                    Notification notifier = new Notification("Association Error", "Create Association to Meter");
                }
                else
                {
                    Application_Controller.IsIOBusy = true;
                    TimerReset_SinglePhase timerReset_1p = new TimerReset_SinglePhase();
                    Param_Controller.GET_TimerReset_Parameters_SP(ref timerReset_1p);

                    chkEnableTimerReset.Checked = timerReset_1p.Enable;
                    cmbHour.Text = (timerReset_1p.Hour).ToString();
                    cmbMinute.Text = (timerReset_1p.Minute).ToString();
                }
            }
            catch (Exception ex)
            {
                Notification notif = new Notification("Get Timer Reset", ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void dataGridView1_Paint(object sender, PaintEventArgs e)
        {
            List<string> MergedColumns = new List<string> { "IN", "OUT", "Direction", "Selection" };
            List<int> StartIndexesToMerge = new List<int> { 4, 7, 10, 13 };

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                if (StartIndexesToMerge.Contains(i))
                {
                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(i, -1, true);
                    rect.Width += dataGridView1.GetCellDisplayRectangle(i + 1, -1, true).Width + dataGridView1.GetCellDisplayRectangle(i + 1, -1, true).Width;
                    rect.Height /= 2;
                    e.Graphics.FillRectangle(new SolidBrush(dataGridView1.ColumnHeadersDefaultCellStyle.BackColor), rect);
                    StringFormat format = new StringFormat()
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };

                    e.Graphics.DrawString(MergedColumns[StartIndexesToMerge.IndexOf(i)], dataGridView1.ColumnHeadersDefaultCellStyle.Font, new SolidBrush(dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor), rect, format);
                    i += 2;
                }
            }


        }

        private void ShowToGUIIOStates(Fuison_IO_StateList statesList)
        {
            dataGridView1.DataSource = null;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = statesList.IOStateList;

            Application.DoEvents();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].DataBoundItem != null && dataGridView1.Columns[e.ColumnIndex].DataPropertyName.Contains("."))
            {
                e.Value = LocalCommon.BindProperty(dataGridView1.Rows[e.RowIndex].DataBoundItem, dataGridView1.Columns[e.ColumnIndex].DataPropertyName);
            }
        }



        private void btnreadState_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Association to Meter Management via Management");
                else
                {
                    Application_Controller.IsIOBusy = true;


                    Application_Controller.Param_Controller.GetIOStates(ref IO_StateList);

                    //if (Result == Data_Access_Result.Success)
                    notifier = new Notification("Success", "IO Status Read Successfully");
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Reading IO State," + ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
                ShowToGUIIOStates(IO_StateList);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfDialog = new SaveFileDialog();
            sfDialog.Filter = "Excel File|*.xlsx";
            if (sfDialog.ShowDialog() == DialogResult.OK)
            {
                LocalCommon.SaveGridToExcel(dataGridView1, sfDialog.FileName);
            }
        }

        private void btnSetProtocol_GateWay_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Application Association");
                else if (cmpProtocol.SelectedIndex <= 0)
                    MessageBox.Show("Please Select Protocol");
                else
                {
                    Application_Controller.IsIOBusy = true;

                    if (cmpProtocol.SelectedIndex >= 0)
                    {
                        Data_Access_Result result = Param_Controller.SetProtocolGate((byte)(cmpProtocol.SelectedIndex + 1));
                        Notification notifier = new Notification("Set Protocol Gate", result.ToString());
                    }
                    else
                        throw new InvalidCastException("GateWay Protocol");
                }
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Error Set Protocol Gate", ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnGetProtocol_GateWay_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Application Association");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    var GP_Prot = Param_Controller.GetProtocolGate();

                    if (cmpProtocol.Items.Count >= 03)
                        // Update GUI
                        switch (GP_Prot)
                        {
                            case Protocol_Gate.PG_TCP:
                                cmpProtocol.SelectedIndex = 0;
                                break;
                            case Protocol_Gate.PG_HDLC:
                                cmpProtocol.SelectedIndex = 1;
                                break;
                            case Protocol_Gate.PG_BOTH:
                                cmpProtocol.SelectedIndex = 2;
                                break;
                            default:
                                cmpProtocol.SelectedIndex = 2;
                                break;
                        }
                    else
                        cmpProtocol.SelectedIndex = -1;

                }
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Get Protocol Gate", ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnSetIccGate_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Application Association");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    Data_Access_Result result = Param_Controller.SetIcclGate(!chkICCGate.Checked);
                    if (result == Data_Access_Result.Success)
                        notifier = new Notification("Set ICC Gate", result.ToString());
                }
            }
            catch (Exception ex)
            {
                notifier = new Notification("Error Set ICC Gate", ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }
        }

        private void btnGetIccGate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(Application_Process.Is_Association_Developed))
                    MessageBox.Show("Create Application Association");
                else
                {
                    Application_Controller.IsIOBusy = true;
                    chkICCGate.Checked = !Param_Controller.GetIccGate();
                }
            }
            catch (Exception ex)
            {
                Notification notifier = new Notification("Get ICC Gate", ex.Message);
            }
            finally
            {
                Application_Controller.IsIOBusy = false;
            }

        }

        private void btnSavePackets_Click(object sender, EventArgs e)
        {
            Notification notifier = null;
            try
            {
                if (string.IsNullOrEmpty(rtb_RAW_Data.Text))
                    notifier = new Notification("Error", "No Data Available in Output Window");

                string Dir = string.Empty;
                SaveFileDialog svDlg = new SaveFileDialog();
                svDlg.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                if (DialogResult.OK == svDlg.ShowDialog())
                {
                    Dir = svDlg.FileName;
                }
                File.WriteAllText(Dir, rtb_RAW_Data.Text);
                notifier = new Notification("Success", "Data Save Successfully");

            }
            catch (Exception ex)
            {
                notifier = new Notification("Error", "Error occurred while Saving Data" + ex.Message);
            }
        }

        

    }
}













//#region --1

//private void SSSSSSSSSSSSSSSSS(object sender, EventArgs e)
//{
//    Notification notifier = null;
//    try
//    {
//        if (!(Application_Process.Is_Association_Developed))
//            MessageBox.Show("Create Association to Meter");
//        else
//        {
//            Application_Controller.IsIOBusy = true;
//            Data_Access_Result result = Application_Controller.Param_Controller.Set_Param_(Convert.ToInt32(txt.Text));
//            if (result == Data_Access_Result.Success)
//                  notifier = new Notification("Success", "NNNNNNNNNNNNNN Successfully");
//            else
//                        throw new Exception(result.ToString());
//          }
//    }
//    catch (Exception ex)
//    {
//        notifier = new Notification("Error", "Error occurred while Setting NNNNNNNNNNNNNN, \n" + ex.Message);
//    }
//    finally
//    {
//        Application_Controller.IsIOBusy = false;
//    }
//}

//private void GGGGGGGGGGGGGGGGG(object sender, EventArgs e)
//{
//    Notification notification = null;
//    try
//    {
//        if (!(Application_Process.Is_Association_Developed))
//            notification = new Notification("Error", "Create Association to Meter");
//        else
//        {
//            Application_Controller.IsIOBusy = true;
//            int ch = Application_Controller.Param_Controller.GET_Param_();
//            txt.Text = ch.ToString();
//            notification = new Notification("Success", "NNNNNNNNNNN Read Successful");
//        }
//    }
//    catch (Exception ex)
//    {
//        notification = new Notification("Error", "NNNNNNNNNNNNN Read Error");
//    }
//    finally
//    {
//        Application_Controller.IsIOBusy = false;
//    }
//}

//private void TTTTTTTTTTTTTTTTT(object sender, EventArgs e)
//{
//    if (LocalCommon.TextBox_validation(txt))
//        this.btn.Enabled = true;
//    else
//        this.btn.Enabled = false;
//}

//#endregion  // 

